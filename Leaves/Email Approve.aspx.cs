using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;

using System.Net; 

//will fire when called from email
namespace SMEPayroll.Leaves
{
    public partial class Email_Approve : System.Web.UI.Page
    {
        string trx_id, status1, TabStatus, emp, compid,status = "";
        DataSet dsleaves;
        string email;
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        string s = "", varEmpName = "", emp_name;
        string _actionMessage = "";
        int ApproveNeeded;
        string PrimaryAddress, SecondaryAddress, DocPath, host;
        protected void Page_Load(object sender, EventArgs e)
        {

            trx_id = Request.QueryString["trx_id"];
            status1 = Request.QueryString["status"];
            emp = Request.QueryString["emp"];
            compid = Request.QueryString["comp_id"];
            ViewState["actionMessage"] = "";



            string sqlleave = "select * from emp_leaves where trx_id='" + trx_id + "'and emp_id='" + emp + "' ";
            SqlDataReader dr_leave = DataAccess.ExecuteReader(CommandType.Text, sqlleave, null);
            if (dr_leave.HasRows)
            {
                if (dr_leave.Read())
                {
                    TabStatus = dr_leave[6].ToString();
                }

                if (TabStatus != "Approved" && TabStatus != "Rejected")
                {
                    if (trx_id != "" && status1 == "approve")
                    {
                        // updateStatus(trx_id, status, emp);

                        bool validmultilevel = CheckMultilevelRights(trx_id, status1, emp, compid);//check he has rights to approve in multilevel
                        if (validmultilevel)
                        {
                            updateStatus_Approve(trx_id, status1, emp, compid);
                        }
                        else
                        {
                           // lblMsg.Text = "Processed or Dont have Permission";
                            _actionMessage = "Warning|Processed or Dont have Permission";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                    else if (trx_id != "" && status1 == "reject")
                    {
                        // updateStatus(trx_id, status, emp);
                        updateStatus_reject(trx_id, status1, emp, compid);
                    }
                }
                else
                {
                    //Response.Write("Not Saved..Something went wrong");
                    //lblMsg.Text = "Not Approved or Rejected..Something went wrong";
                    _actionMessage = "Warning|Not Approved or Rejected..Something went wrong";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            else
            {
                //Response.Write("Not Saved..Something went wrong");
                //lblMsg.Text = "Not Approved or Rejected..Something went wrong";
                _actionMessage = "Warning|Not Approved or Rejected..Something went wrong";
                ViewState["actionMessage"] = _actionMessage;
            }
         
        }

        bool HasRights;
        string appr, stat,validEmail;
        private bool CheckMultilevelRights(string trx_id, string status1, string emp_code, string compid)
        {
            HasRights = false;
           
            SqlDataReader dr_det = DataAccess.ExecuteReader(CommandType.Text, "select approver,status from emp_leaves where trx_id='" + trx_id + "'", null);
            while (dr_det.Read())
            {
                appr = Utility.ToString(dr_det.GetValue(0));
                stat = Utility.ToString(dr_det.GetValue(1));
            }

            if (appr == "MultiLevel")//check whether it is multilevel
            {
                int n;
                bool isNumeric = int.TryParse(stat, out n);
                if ((isNumeric == true) || (stat == "Open"))
                {
                    string email = Request.QueryString["email"];
                    string[] emailsplit = email.Split(';');

                    if (emailsplit.Length >2)
                    {
                        validEmail = emailsplit[1].ToString();
                    }
                    else
                    {
                        validEmail = emailsplit[0].ToString();
                    }

                    //  Response.Write(emailsplit[1].ToString());

                      if (stat == "Open")
                      {
                          //from email , check he belong to top level
                          string sqlvalid = "select top 1 emp_code from employee where email='" + validEmail + "'and emp_code in (select emp_id from  EmployeeAssignedToPayrollGroup  where PayrollGroupID in(select PayRollGroupID from  EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code='" + emp_code + "')))";
                          SqlDataReader drvalid = DataAccess.ExecuteReader(CommandType.Text, sqlvalid, null);
                          if (drvalid.HasRows)
                          {
                              HasRights = true;
                          }
                      }
                      else if (isNumeric)
                      {
                          int num = Convert.ToInt32(stat) - 1;
                          if (num > 0)
                          {

                              string sqlnum = "select emp_code from employee where email='" + validEmail + "'and emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where PayrollGroupID='" + num + "')";
                              //after approval of level2 -- if he is in level 1
                              SqlDataReader drnum = DataAccess.ExecuteReader(CommandType.Text, sqlnum, null);
                              if (drnum.HasRows)
                              {
                                  HasRights = true;
                              }
                          }
                          else
                          {
                              HasRights = false;
                          }
                      }
                      else
                      {
                          HasRights = false;
                      }

                }
                else
                {
                    HasRights = false;
                }
            }
            else
            {
                HasRights = true;
            }
            return HasRights;
        }

        private void updateStatus_reject(string trx_id, string status1, string emp_code, string compid)
        {
            #region get emp_name,approver
            SqlDataReader dr_det = DataAccess.ExecuteReader(CommandType.Text, "select E.emp_name,(select emp_name from Employee where emp_code=E.emp_supervisor) approver from employee E where emp_code='" + emp_code + "'", null);
            while (dr_det.Read())
            {
                emp_name = Utility.ToString(dr_det.GetValue(0));
                varEmpName = Utility.ToString(dr_det.GetValue(1));
            }
            #endregion

            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            string remarks = "Leave Rejected:"+emp_name;


                int trxid = Convert.ToInt32(trx_id);
                // string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                string empcode = emp_code;
                string sSQLCheck = "select payrollstatus, isnull(paid_leaves,0) paid_leaves, isnull(unpaid_leaves,0) unpaid_leaves from emp_leaves where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    string Sql = "Update emp_leaves set status='Rejected',approver='" + varEmpName + "',remarks='" + remarks + "' where trx_id=" + trxid;
                    try
                    {
                        DataAccess.ExecuteStoreProc(Sql);
                        strSucSubmit.Append(emp_name );
                        sendemail(trxid, emp_name, "");
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                        {
                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                            strFailSubmit.Append( emp_name );
                        }
                        else
                        {
                            strFailMailMsg.Append( emp_name);
                        }
                    }
                }
                else
                {
                    //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                ViewState["actionMessage"] = _actionMessage;
            }
                    
               

            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Leaves Rejected Successfully for: <br/>" + strSucSubmit.ToString());
                //lblMsg.Text = "Leaves Rejected Successfully for: " + strSucSubmit.ToString();
                _actionMessage = "Success|Leaves Rejected Successfully for: <br/>" + strSucSubmit.ToString();
               
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Leaves Not Rejected for: <br/>" + strFailSubmit.ToString());
                //lblErr.Text = "Leaves Not Rejected for: " + strFailSubmit.ToString();
                _actionMessage = "Warning|Leaves Not Rejected for:<br/>"+ strFailSubmit.ToString();
               

            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                //lblErr.Text = "Email Send successfully to: " + strPassMailMsg.ToString();
                _actionMessage += " <br/>Email Send successfully to concerned person";
               
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                //lblErr.Text = "Error While sending Email to: " + strFailMailMsg.ToString();
                _actionMessage += " <br/>Error While sending Email";
              
            }
            ViewState["actionMessage"] = _actionMessage;
        }

   
        private void updateStatus_Approve(string trx_id, string status1, string emp_code, string compid)
        {
            
            #region get emp_name,approver
            SqlDataReader dr_det = DataAccess.ExecuteReader(CommandType.Text, "select E.emp_name,(select emp_name from Employee where emp_code=E.emp_supervisor) approver from employee E where emp_code='" + emp_code + "'", null);
            while (dr_det.Read())
            {
                emp_name = Utility.ToString(dr_det.GetValue(0));
                varEmpName = Utility.ToString(dr_det.GetValue(1));
            }
            #endregion
            //string remarks = txtEmpRemarks.Value + " - " + Session["Username"].ToString() + ":" + txtremarks.Text;
            string remarks = "Please Approve Leave for : "+emp_name;
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();


            int trxid =Convert.ToInt32(trx_id);
           // string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
            string empcode = emp_code;
            string sSQLCheck = "select payrollstatus, isnull(paid_leaves,0) paid_leaves, isnull(unpaid_leaves,0) unpaid_leaves from emp_leaves where trx_id = {0}";
            sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
            
            double paid_leaves = 0;
            double unpaid_leaves = 0;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
            while (dr.Read())
            {
                status = Utility.ToString(dr.GetValue(0));
                paid_leaves = Utility.ToDouble(dr.GetValue(1));
                unpaid_leaves = Utility.ToDouble(dr.GetValue(2));
            }
            int appLevel = 0;
            if ((status == "U") || (status == "L" && unpaid_leaves <= 0))
            {
                bool flagUpdatefinal = true;
                int appLeveldb = 0;

                string strSql = "Select WL.ID,WL.RowID,WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName ";
                strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) ";
                strSql = strSql + "And Company_ID=" + compid + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT Leave_supervisor FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
                strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

                dsleaves = new DataSet();
                dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);

                if (dsleaves.Tables.Count > 0)
                {
                    if (dsleaves.Tables[0].Rows.Count > 0)
                    {
                        flagUpdatefinal = false;
                        int maxApprovalleval = Utility.ToInteger(dsleaves.Tables[0].Rows[0][1]);
                        //Approved Open
                        //If Approved and open status then appLevel =0
                        SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT [STATUS] FROM emp_leaves Where trx_id=" + trxid, null);
                        while (dr1.Read())
                        {
                            if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open")
                            {
                                appLevel = maxApprovalleval;
                            }
                            else
                            {
                                appLeveldb = Utility.ToInteger(dr1.GetValue(0));
                                appLevel = appLeveldb - 1;
                            }
                        }
                        if (appLevel == 1)
                        {
                            //
                            flagUpdatefinal = true;
                        }
                    }
                }

                if (flagUpdatefinal)
                {
                    string Sql = "Update emp_leaves set status='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "',ApproveDate='" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "' where trx_id=" + trxid;
                    //string Sql2 = "";
                    //Sql2 = Sql2 + " UPDATE leaves_annual SET leave_remaining = leave_remaining -  " + paid_leaves + " WHERE emp_id = (SELECT emp_id FROM emp_leaves WHERE trx_id = " + trxid + ") AND ";
                    //Sql2 = Sql2 + " leave_year = (select year(start_date) from emp_leaves WHERE trx_id = " + trxid + " ) ";

                    try
                    {
                        DataAccess.ExecuteStoreProc(Sql);
                        strSucSubmit.Append(emp_name);

                        //lblMsg.Text = "Approved Sucessfully";
                        _actionMessage = "sc|Approved Sucessfully";
                        ViewState["actionMessage"] = _actionMessage;

                        sendemail(trxid, emp_name, "");
                        //if (type == "Annual Leave")
                        //{
                        //DataAccess.ExecuteStoreProc(Sql2);
                        //}
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                        {
                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                            strFailSubmit.Append(emp_name);
                        }
                        else
                        {
                            strFailMailMsg.Append(emp_name);
                        }
                    }
                }
                else
                {
                    string Sql = "Update emp_leaves set status='" + appLevel.ToString() + "',approver='MultiLevel',remarks='" + remarks + "',ApproveDate='" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "' where trx_id=" + trxid;
                    //string Sql2 = "";
                    //Sql2 = Sql2 + " UPDATE leaves_annual SET leave_remaining = leave_remaining -  " + paid_leaves + " WHERE emp_id = (SELECT emp_id FROM emp_leaves WHERE trx_id = " + trxid + ") AND ";
                    //Sql2 = Sql2 + " leave_year = (select year(start_date) from emp_leaves WHERE trx_id = " + trxid + " ) ";

                    try
                    {
                        DataAccess.ExecuteStoreProc(Sql);

                        //lblMsg.Text = "Approved Sucessfully";
                        _actionMessage = "sc|Approved Sucessfully";
                        ViewState["actionMessage"] = _actionMessage;

                        //strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                        sendemail(trxid, emp_name, "");
                        //Send an email to appLevel.ToString() -1  Approver 

                        int arroverLevel = Convert.ToInt32(appLevel.ToString());
                        if ((arroverLevel - 1) != 0)
                        {

                            //Get List Of All Employeees whose level is arroverLevel-1
                            string payrollGroup = "SELECT e.email,* FROM employee e WHERE e.emp_code IN (SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.GroupName='L" + (arroverLevel - 1) + "')";

                            SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, payrollGroup);
                            StringBuilder strUpdateBuild = new StringBuilder();

                            while (dr4.Read())
                            {

                                email = dr4[0].ToString() + ";";
                                strUpdateBuild.Append(email);
                            }


                            sendemail(trxid, emp_name, strUpdateBuild.ToString());

                            if (appLevel == 1)
                            {
                                flagUpdatefinal = true;
                            }
                        }

                     
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                        {
                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                            strFailSubmit.Append(emp_name);
                        }
                        else
                        {
                            strFailMailMsg.Append(emp_name);
                        }
                    }

                }
            }
            else
            {
                //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                ViewState["actionMessage"] = _actionMessage;
            }
       



            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Leaves Approved Successfully for: <br/>" + strSucSubmit.ToString());
                //lblMsg.Text = "Leaves Approved Successfully for: " + strSucSubmit.ToString();
                _actionMessage = "Success|Leaves Approved Successfully for: <br/>" + strSucSubmit.ToString();
               
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Leaves Not Approved for: <br/>" + strFailSubmit.ToString());
                //lblErr.Text = "Leaves Not Approved for: " + strFailSubmit.ToString();
                _actionMessage = "Warning|Leaves Not Approved for:  <br/>"+ strFailSubmit.ToString();
               
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                //lblErr.Text = "Email Send successfully to: " + strPassMailMsg.ToString();
                _actionMessage += " <br/>Email Send successfully to concerned person ";
                
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                //lblErr.Text = "Error While sending Email to: " + strFailMailMsg.ToString();
                _actionMessage += " <br/>Error While sending Email";
              
            }
  ViewState["actionMessage"] = _actionMessage;
            //RadGrid1.DataBind();
            //txtremarks.Text = "";
        
        }
        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);

        }
        protected void sendemail(int id, string emp_name, string empMultlevel)
        {
            string applicationdate = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string status = "";
            string approver = "";
            string from_date = "";
            string to_date = "";
            string reason = "";
            string emailreq = "";
            int SMTPPORT = 25;
            string body = "";
            string cc = "";

            if (empMultlevel.Length <= 0)
            {
                string sSQLemail = "sp_send_email_status";
                SqlParameter[] parmsemail = new SqlParameter[1];
                parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
                while (dr3.Read())
                {
                    from = Utility.ToString(dr3.GetValue(18));
                    to = Utility.ToString(dr3.GetValue(15));
                    SMTPserver = Utility.ToString(dr3.GetValue(5));
                    SMTPUser = Utility.ToString(dr3.GetValue(6));
                    SMTPPass = Utility.ToString(dr3.GetValue(7));
                    approver = Utility.ToString(dr3.GetValue(2));
                    status = Utility.ToString(dr3.GetValue(3));
                    applicationdate = Utility.ToString(dr3.GetValue(4));
                    from_date = Utility.ToString(dr3.GetValue(0));
                    to_date = Utility.ToString(dr3.GetValue(1));
                    reason = Utility.ToString(dr3.GetValue(16));
                    body = Utility.ToString(dr3.GetValue(10));
                    SMTPPORT = Utility.ToInteger(dr3.GetValue(12));
                    emailreq = Utility.ToString(dr3.GetValue(17)).ToLower();
                    cc = Utility.ToString(dr3.GetValue(19));
                }
            }
            else
            {
                //string sqlEmail = "SELECT CONVERT(VARCHAR(10), [start_date], 103) [start_date], CONVERT(VARCHAR(10), end_date, 103) [end_date],approver, [status], CONVERT(VARCHAR(10), [Application date], 103) [Application date],b.email_SMTP_server, b.email_username, b.email_password, b.email_sender_domain, b.email_sender_name,b.email_reply_address, b.email_reply_name, b.email_smtp_port, b.email, c.company_id, c.email,a.remarks, b.email_leavealert, b.email_sender, b.ccalert_leaves FROM   emp_leaves a ,company b ,employee c Where c.emp_name='" + emp_name + "'";
                //SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail, null);


                string sSQLemail1 = "sp_send_email_status";
                SqlParameter[] parmsemail1 = new SqlParameter[1];
                parmsemail1[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

                SqlDataReader dr5 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail1, parmsemail1);


                //Decide To And From email :
                string aaprover1 = "";
                if (empMultlevel.Length <= 0)
                {
                    string sqlEmail1 = "SELECT c.email From employee c Where c.emp_name='" + emp_name + "'";
                    SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);

                    while (dr6.Read())
                    {
                        aaprover1 = Utility.ToString(dr6.GetValue(0));
                    }
                }
                else
                {
                    aaprover1 = empMultlevel;
                }



                while (dr5.Read())
                {

                    from = Utility.ToString(dr5.GetValue(18));
                    to = aaprover1;
                    SMTPserver = Utility.ToString(dr5.GetValue(5));
                    SMTPUser = Utility.ToString(dr5.GetValue(6));
                    SMTPPass = Utility.ToString(dr5.GetValue(7));
                    approver = Utility.ToString(dr5.GetValue(2));
                    status = Utility.ToString(dr5.GetValue(3));
                    applicationdate = Utility.ToString(dr5.GetValue(4));
                    from_date = Utility.ToString(dr5.GetValue(0));
                    to_date = Utility.ToString(dr5.GetValue(1));
                    reason = Utility.ToString(dr5.GetValue(16));
                    body = Utility.ToString(dr5.GetValue(10));
                    SMTPPORT = Utility.ToInteger(dr5.GetValue(12));
                    emailreq = Utility.ToString(dr5.GetValue(17)).ToLower();
                    cc = Utility.ToString(dr5.GetValue(19));
                }

            }

            SMEPayroll.Model.ANBMailer oANBMailer;

            if (emailreq == "yes")
            {
                string subject = "Leave Requested On " + " " + applicationdate;

                if (empMultlevel.Length > 0)
                {
                    body = body.Replace("@approver", approver);
                    body = body.Replace("@status", status);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date);
                    body = body.Replace("@reason", reason);

                    if (status != "Approved" && status != "Rejected")
                    {
                        #region Added accept and reject button below
                        //check whether needed approve and reject button
                        SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select top 1 [Enable],[PrimaryAddress],[SecondaryAddress] from EmailApproval where company_id='" + Utility.ToInteger(compid) + "' and [Enable]='1' ", null);
                        if (dr_chk.HasRows)
                        {
                            if (dr_chk.Read())
                            {
                                ApproveNeeded = Convert.ToInt32(dr_chk[0]);
                                PrimaryAddress = Convert.ToString(dr_chk[1]);
                                SecondaryAddress = Convert.ToString(dr_chk[2]);
                            }
                            if (ApproveNeeded == 1)
                            {
                                
                                if (PrimaryAddress != "")
                                {
                                    bool valid = RemoteFileExists(PrimaryAddress + "Index.aspx");
                                    if (valid)
                                    {
                                        host = PrimaryAddress;
                                    }
                                    else if (SecondaryAddress != "")
                                    {
                                        bool validsec = RemoteFileExists(SecondaryAddress + "Index.aspx");
                                        if (validsec)
                                        {
                                            host = SecondaryAddress;
                                        }
                                    }

                                }
                                //string host = "http://localhost:1379/";


                                string url = host + "Leaves/Email Approve.aspx?emp=" + emp + "&trx_id=";

                                //
                                string sqlhost = "select [path] from emp_leaves where emp_id='" + emp + "' and trx_id='" + id + "'";
                                SqlDataReader dr_trx = DataAccess.ExecuteReader(CommandType.Text, sqlhost, null);
                                if (dr_trx.Read())
                                {
                                    DocPath = dr_trx[0].ToString();
                                }
                                //

                                string url_approve = url + trx_id + "&status=approve&comp_id=" + compid + "";
                                string url_reject = url + trx_id + "&status=reject&comp_id=" + compid + "";
                                url_approve = url_approve + "&email=" + to;
                                if (host != null)
                                {
                                    body = body + "<br/><br/>\n\n  <a href=\"" + url_approve + "\">ACCEPT</ID></a> &nbsp;  or &nbsp;   \n\n    <a href=\"" + url_reject + "\">REJECT</ID></a>";
                                }
                            }
                        }
                        #endregion
                    }

                    oANBMailer = new SMEPayroll.Model.ANBMailer(Convert.ToInt32(compid));


                    #region Attachment
                        if (DocPath != "" && DocPath != null)
                        {
                            //oANBMailer.Attachment = @"C:\Temp\index.html";
                            oANBMailer.Attachment = Server.MapPath(DocPath);
                        }
                    #endregion

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;
                }
                else
                {
                    body = body.Replace("@approver", approver);
                    body = body.Replace("@status", status);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date);
                    body = body.Replace("@reason", reason);


                   
                    oANBMailer = new SMEPayroll.Model.ANBMailer(Convert.ToInt32(compid));

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;

                }

                //try
                //{
                //    string sRetVal = oANBMailer.SendMail();
                //    if (sRetVal == "")
                //        Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                //    else
                //        Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");

                //}
                //catch (Exception ex)
                //{
                //    string errMsg = ex.Message;
                //}

                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {

                        if (to.Length > 0)
                        {
                            // if (cc.Length > 0)
                            {
                                // strPassMailMsg.Append("<br/>" + emp_name);
                                strPassMailMsg.Append("<br/>" + to);
                            }
                            //else
                            //{
                            //   strPassMailMsg.Append("<br/>" + emp_name);

                            //}
                        }
                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + to);


                    }
                }
                catch (Exception ex)
                {
                    strFailMailMsg.Append(emp_name);

                }
            }
        }


        public bool RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest        
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.         
                request.Method = "HEAD";
                //Getting the Web Response.        
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TURE if the Status code == 200         
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                //Any exception will returns false.         
                return false;
            }
        } 

        //private void updateStatus(string trx_id, string status, string emp)
        //{
        //    try
        //    {
        //        string sqlleave = "select * from emp_leaves where trx_id='" + trx_id + "'and emp_id='" + emp + "' ";
        //        SqlDataReader dr_leave = DataAccess.ExecuteReader(CommandType.Text, sqlleave, null);
        //        if (dr_leave.HasRows)
        //        {
        //            if (dr_leave.Read())
        //            {
        //                TabStatus = dr_leave[6].ToString();
        //            }

        //            if (TabStatus == "Open")
        //            {
        //                if (status == "approve")
        //                {
        //                    string ssqlb = "Update emp_leaves set status='Approved' where trx_id='" + trx_id + "' and emp_id='" + emp + "'  ";
        //                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
        //                    //Response.Write("Approved Sucessfully");
                          
        //                }
        //                else if (status == "reject")
        //                {
        //                    string ssqlb = "Update emp_leaves set status='Rejected' where trx_id='" + trx_id + "' and emp_id='" + emp + "'  ";
        //                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
        //                    //Response.Write("Rejected Sucessfully");
        //                    lblMsg.Text = "Rejected Sucessfully";
        //                }
        //            }
        //            else if (TabStatus == "Approved" || TabStatus == "Rejected")
        //            {
        //                //Response.Write("Already Approved or Rejected.");
        //                lblMsg.Text = "Already Approved or Rejected.";
        //            }


        //        }
        //        else
        //        {
        //            lblMsg.Text = "Not Approved or Rejected..Something went wrong.";
        //            //Response.Write("Not Saved..Something went wrong.");
        //        }


        //       // sendemail(trx_id);
        //    }
        //    catch (Exception E)
        //    {
        //        //Response.Write("Error:" + E.ToString());
        //        string Err = "Error:" + E.ToString();
        //        lblMsg.Text = Err;
        //    }


        //}
      
       
       
    }
}