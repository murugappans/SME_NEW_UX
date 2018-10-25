using Newtonsoft.Json;
using SMEPayroll.Appraisal.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Http;
using System.Threading;

using System.Text;

namespace SMEPayroll.Appraisal
{
    public partial class ManagerAppraisal : System.Web.UI.Page
    {
        public static int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //int AppraisalId = Convert.ToInt32(HttpContext.Current.Request.QueryString["id"]);
            compid = Utility.ToInteger(Session["Compid"]);

        }

        [System.Web.Services.WebMethod]
        public static string GetAppraisalForm(int AppraisalId)
        {
            //int AppraisalId = 73;
            int Empcode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            string ssl = "";
            bool remarksDone=false;
           
            ssl += "Select distinct emp.emp_name,emp.emp_code, Ah.CommandsDate from employee emp inner join  Appraisal_History Ah on Ah.AppraisalApproverID = emp.emp_code where Fk_AppraisalID = " + AppraisalId+" order by Ah.CommandsDate";
            ssl += " Select aot.Id,ah.Commands,ah.Remark,ah.AppraisalApproverID,aot.ObjectiveType from Appraisal_History ah inner join AppraisalObjectiveTemplate aot on ah.Fk_ObjectiveID = aot.Id where Fk_AppraisalID = " + AppraisalId + " order by convert(date,CommandsDate) desc";
            ssl += " Select ap.* , emp.emp_name +' '+ emp.emp_lname as EmpName from Appraisal ap inner join employee emp on emp.emp_code = ap.EmpId where Id=" + AppraisalId;

            ssl += "select aot.Id,aot.ObjectiveName,aot.ObjectiveType from AppraisalObjectiveTemplate aot inner join Appraisal_Template_Objective_Mapping atom on aot.Id = atom.Objective_Id";
            ssl += " where atom.Template_Id = (select AppraisalTemplateID from Appraisal where Id = " + AppraisalId +")";

            DataSet dt = DataAccess.FetchRS(CommandType.Text, ssl);
            ssl = "Select Count(Id) from Appraisal_History  where Fk_AppraisalID =" + AppraisalId + " and AppraisalApproverID=" + Empcode;
            int count = DataAccess.ExecuteScalar(ssl);
            if (count > 0)
                remarksDone = true;

            if (dt.Tables.Count > 0 && dt.Tables[3].Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(new { dtNoOfPpl = dt.Tables[0], dtAppraisalHistory = dt.Tables[1], dtAppraisal = dt.Tables[2], dtObjectives = dt.Tables[3], remarksDone });
            }
            return "false";

        }


        [System.Web.Services.WebMethod]
        public static string FeedbackSend(AppraisalHistory[] EmployAppraisalRply, int AppraisalId)
        {
            string ssl = "", str = "",status = "", empcode = "",  workFlowName = "",workflowId = "", wflevel = "";
            bool mailsent = false;
            int appLevel = 0;
            int Empcode = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
            string dtToday = string.Format("{0:yyyy-MM-dd hh:mm}", DateTime.Now);
            foreach (AppraisalHistory item in EmployAppraisalRply)
            {
                item.Remark = item.Remark.Replace("'", "''");
                ssl += "INSERT INTO Appraisal_History(Fk_AppraisalID,Fk_ObjectiveID, AppraisalApproverID,Commands,CommandsDate,Remark)";
                ssl += "VALUES(" + AppraisalId + "," + item.ObjectiveId + "," + Empcode + ",'" + item.Comment + "','" + dtToday + "','" + item.Remark + "')";

            }

            int rly = DataAccess.ExecuteStoreProc(ssl);
            if (rly > 0)
            {
                //ssl = "UPDATE [dbo].[Appraisal] SET  [WFLevel] = 'level3' WHERE Id=" + AppraisalId;    //wflevel should be updated with the wflevel of the preasent emplyee
                //DataAccess.ExecuteStoreProc(ssl);
               // return 1;
                //----------murugan
                                    
                str = "select [Status],EmpId,WFLevel from Appraisal where [id] = " + AppraisalId.ToString();
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                    empcode = Utility.ToString(dr.GetValue(1));
                    wflevel = Utility.ToString(dr.GetValue(2));
                }
                appLevel = 0;

                bool flagUpdatefinal = true;
                int appLeveldb = 0;

                string strSql = "Select WF.ID,WL.RowID,WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName,WorkFlowName WName ";
                strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=5) ";
                strSql = strSql + "And Company_ID=" + compid + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT ApprisalApprover FROM dbo.Appraisal   WHERE id=" + AppraisalId.ToString() + ") ";
                strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

                DataSet dsapp = new DataSet();
                dsapp = DataAccess.FetchRS(CommandType.Text, strSql, null);

                if (dsapp.Tables.Count > 0)
                {
                    if (dsapp.Tables[0].Rows.Count > 0)
                    {
                        flagUpdatefinal = false;
                        int maxApprovalleval = Utility.ToInteger(dsapp.Tables[0].Rows[0][1]);
                        workFlowName = Utility.ToString(dsapp.Tables[0].Rows[0][3]);
                        workflowId = Utility.ToString(dsapp.Tables[0].Rows[0][0]);
                        //Approved Open
                        //If Approved and open status then appLevel =0

                        if (status == "InProgress")
                        {
                           

                            if (wflevel == "Level0")
                            { appLevel = maxApprovalleval;
                                status = "0";
                            }
                            else
                                appLevel = maxApprovalleval - 1;

                        }
                        else
                        {
                            appLeveldb = Utility.ToInteger(status);
                            appLevel = appLeveldb - 1;
                        }

                        if (Utility.ToInteger(status) == 1)
                        {
                            flagUpdatefinal = true;
                        }
                    }
                }

                if (Utility.ToInteger(status) == 1)
                {

                    ssl = "UPDATE [dbo].[Appraisal] SET Status='Complete' , [WFLevel] = 'Completed' WHERE Id=" + AppraisalId;    //wflevel should be updated with the wflevel of the preasent emplyee
                    DataAccess.ExecuteStoreProc(ssl);
                    // sendemail(trxid, emp_name, "", "");

                }
                else
                {

                    string str2 = "select wl.ID from  ((Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN (Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=5 and WorkFlowName='" + workFlowName + "')";
                    str2 = str2 + "And Company_ID = " + compid + ") WF Inner Join EmployeeWorkFlowLevel WL On WF.ID = WL.WorkFlowID) where rowid =" + appLevel;

                    SqlDataReader dr10 = DataAccess.ExecuteReader(CommandType.Text, str2, null);
                    int approverlevel = 0;
                    if (dr10.Read())
                    {
                        approverlevel = Utility.ToInteger(dr10[0].ToString());
                    }

                    ssl = "UPDATE [dbo].[Appraisal] SET  [Status] = '" + appLevel + "', WFLevel='Level" + appLevel + "', ApprisalApprover=" + approverlevel + " WHERE Id=" + AppraisalId;
                    // ssl = "UPDATE [dbo].[Appraisal] SET  WFLevel='Level" + appLevel + "', ApprisalApprover=" + approverlevel + " WHERE Id=" + AppraisalId;
                    DataAccess.ExecuteNonQuery(ssl, null);
                    ssl = "select email from employee where emp_code= (select emp_id from EmployeeAssignedToPayrollGroup where [PayrollGroupID] =(select payrollgroupid from EmployeeWorkFlowLevel where id=" + approverlevel + "))";
                    SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, ssl, null);
                    string appemail = "";
                    if (dr11.Read())
                    {
                        appemail = dr11[0].ToString();
                        int reply = sendemail(approverlevel.ToString(), appemail, AppraisalId, wflevel);
                        mailsent = true;

                    }
                    //else
                    //{
                    //    

                    //    ShowMessageBox("Invalid emial id..");
                    //    return 0;
                    //}




                }

                //end
                if(!mailsent)
                    return JsonConvert.SerializeObject(new { rply = "Your Appraisal Feedback has been Sent", done = true });
                                
                else
                return JsonConvert.SerializeObject(new { rply = "Your Appraisal Feedback and email to your Supervisior has been sent..", done = true });
            }
            else
            {
                return JsonConvert.SerializeObject(new { rply = "Your Appraisal Feedback has not been Sent.. Some error has occured. Please Try after sometime", done = false });
            }
        }

        [System.Web.Services.WebMethod]
        public static int sendemail(string nextapprover, string appemail, int AppraisalId, string wlevel)
        {


            // string strMessage = "";
            string code = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string from_date = "";
            string to_date = "";
            string emailreq = "";
            string body = "";
            string cc = "";
            int mailcount = 0;
            string sqlupdate = "";
            string wflevel = "";
            SqlDataReader emplevel;
            string sql = "select * from Appraisal where id=" + AppraisalId;

            SqlDataReader appdr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            while (appdr.Read())
            {

                from_date = string.Format("{0:yyyy-MM-dd}", appdr["ValidFrom"]);
                to_date = string.Format("{0:yyyy-MM-dd}", appdr["ValidEnd"]);

                code = appdr["EmpId"].ToString();


                string sSQLemail = "sp_send_email";
                SqlParameter[] parmsemail = new SqlParameter[2];
                parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
                parmsemail[1] = new SqlParameter("@compid", Appraisal.compid);
                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
                while (dr3.Read())
                {
                    from = Utility.ToString(dr3.GetValue(15));
                    to = Utility.ToString(dr3.GetValue(2));
                    SMTPserver = Utility.ToString(dr3.GetValue(6));
                    SMTPUser = Utility.ToString(dr3.GetValue(7));
                    SMTPPass = Utility.ToString(dr3.GetValue(8));
                    emp_name = Utility.ToString(dr3.GetValue(5));
                    body = Utility.ToString(dr3.GetValue(20));
                    SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                    emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                    cc = Utility.ToString(dr3.GetValue(17));

                }
                if (emailreq == "yes")
                {
                    if (to.ToString().Trim().Length <= 0)
                    {
                        to = cc;
                    }


                    string subject = "Appraisal Request By " + " " + emp_name;
                    body = body.Replace("@app_name", appdr["AppraisalName"].ToString());
                    if (wlevel == "Level0")
                        body = body.Replace("@emp_name", emp_name);
                    else
                        body = body.Replace("@emp_name", wlevel);

                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date);
                    // body = body.Replace("@leave_type", "Appraisal");
                    //body = body.Replace("@leave_balance", dblbalanceavail.ToString());
                    //body = body.Replace("@paid_leaves", pdleave.ToString());
                    //body = body.Replace("@unpaid_leaves", updleave.ToString());
                    //body = body.Replace("@timesession", strts.ToString());
                    // body = body.Replace("@reason", requestRemarks.ToString());



                    #region Get SSl required
                    string SSL = "";
                    string sql1 = "select sslrequired from company where Company_Id='" + Appraisal.compid + "'";
                    SqlDataReader dr_ssl = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                    if (dr_ssl.HasRows)
                    {
                        while (dr_ssl.Read())
                        {
                            SSL = Utility.ToString(dr_ssl.GetValue(0));
                        }
                    }
                    #endregion

                    SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(SMTPserver, SMTPUser, SMTPPass, from, SMTPUser, SMTPPORT, SSL);


                    oANBMailer.Subject = subject;
                    oANBMailer.From = from;



                    //--superadmin login

                    SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + code, null);
                    if (dr9.Read())
                    {
                        if (dr9[0].ToString() == "Super Admin")
                        {
                            string sql3 = "select email from employee where Emp_code=" + code;
                            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql3, null);
                            if (dr11.Read())
                            {
                                appemail = appemail + ";" + dr11[0].ToString();
                            }

                        }


                    }


                    oANBMailer.To = appemail;
                    oANBMailer.Cc = cc;
                    oANBMailer.MailBody = body;

                    if (appemail.Length == 0 || from.Length == 0)
                    {

                        //ShowMessageBox("Please check email address is not configured yet");
                        return 0;
                    }

                    try
                    {
                        //string sRetVal = oANBMailer.SendMail();
                        string sRetVal = oANBMailer.SendMail("Appraisal", emp_name, from_date, to_date, "Appraisal Request");


                        if (sRetVal == "SUCCESS")
                        {

                            mailcount = mailcount + 1;
                            //Thread.Sleep(10000);

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        //  strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";

                    }
                }
            }
            return mailcount;

        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            System.Text.StringBuilder sbScript = new System.Text.StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }
    }
}