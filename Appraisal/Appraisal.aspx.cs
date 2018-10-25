using System;
using SMEPayroll.Appraisal.Model;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using System.Web;

using System.Net.Mail;
using System.Text;
using System.Data.SqlClient;
using System.Threading;
using FluentScheduler;

namespace SMEPayroll.Appraisal
{
    public partial class Appraisal : System.Web.UI.Page
    {


        // EmployeeType empType = (EmployeeType)Enum.Parse(typeof(EmployeeType), ddl.SelectedValue); 
        int saverec = 0;
        string strMessage = "";
        public static int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //dpAppraisalStatus.DataSource = Enum.GetNames(typeof(AppraisalStatus));
                //dpAppraisalStatus.DataBind();
                compid = Utility.ToInteger(Session["Compid"]);
            }
        }


        [System.Web.Services.WebMethod]
        public static string GetTemplates()
        {
            string ssl = "select * from AppraisalTemplate at where at.Id  in (select distinct Template_Id from Appraisal_Template_Objective_Mapping) and at.Active = 1";
            DataSet data = new DataSet();
            data = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { dataTemplates = data.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static string GetTemplateObjectives(int TempId)
        {
            string ssl = "select aot.Id, aot.ObjectiveName, Appcat.CategoryName  from AppraisalCategory Appcat Join AppraisalObjectiveTemplate aot on aot.CategoryID = Appcat.Id ";
            ssl += "LEFT join Appraisal_Template_Objective_Mapping t2 ON t2.Objective_Id = aot.Id where t2.Template_Id =" + TempId;
            DataSet datatb = new DataSet();
            datatb = DataAccess.FetchRS(CommandType.Text, ssl);

            return JsonConvert.SerializeObject(new { dataTemplates = datatb.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static string GetEmployees(string Type)
        {
            int ComId = Convert.ToInt32(HttpContext.Current.Session["Compid"]);
            string ssl = ""; string wfappraisal = "";
            DataSet datatb = new DataSet();
            string compsql = "select WFAppraisal from company where Company_id=" + compid;
            SqlDataReader compdr = DataAccess.ExecuteReader(CommandType.Text, compsql, null);
            if (compdr.Read())
            {
                wfappraisal = compdr[0].ToString();
            }

            // return JsonConvert.SerializeObject(new { rply = "Appraisal Workfolow disabled..", done = false });


            if (Type == "Departments")
            {
                ssl = "SELECT DeptName as EmpName ,ID as Id FROM dbo.DEPARTMENT WHERE COMPANY_ID= " + ComId;
                datatb = DataAccess.FetchRS(CommandType.Text, ssl);
            }
            else if (Type == "Teams")
            {
                ssl = "Select ID as Id, Team_Name as EmpName From TeamAppraisal Where Company_ID= " + ComId;
                datatb = DataAccess.FetchRS(CommandType.Text, ssl);
            }
            else if (Type == "Employees")
            {
                if (wfappraisal == "0")
                    ssl = "SELECT CONVERT(varchar(10),emp_code) +'      '+  emp_name+' '+emp_lname as EmpName,emp_code as Id FROM dbo.employee WHERE Company_Id= " + ComId;
                else
                    ssl = "SELECT CONVERT(varchar(10),emp_code) +'      '+  emp_name+' '+emp_lname as EmpName,emp_code as Id FROM dbo.employee WHERE Company_Id= " + ComId+ " and AppraisalsupervicerMulitilevel IS NOT NULL and AppraisalsupervicerMulitilevel <> -1";
                datatb = DataAccess.FetchRS(CommandType.Text, ssl);
            }
            else
            {
                ssl = "SELECT emp_name as EmpName,emp_code as Id FROM dbo.employee WHERE emp_code=0 ";
                datatb = DataAccess.FetchRS(CommandType.Text, ssl);
            }
            
           
            return JsonConvert.SerializeObject(new { dataEmployees = datatb.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static string SaveAppraisal(Model.EmployeeAppraisal Appraisal, NewTemplate NewTemplate)
        {
           
            string stend, stfrom, ssl = "", strDone=""; int rply = 0, NewId = 0, Trmplterply=0;
            DataSet dt = new DataSet();
            if (NewTemplate.ObjectiveId != null)
                if (NewTemplate.ObjectiveId.Length > 0)
                {
                    NewTemplate.TemplateName = NewTemplate.TemplateName.Replace("'", "''");
                    ssl = "Select Id from AppraisalTemplate where Name like '" + NewTemplate.TemplateName + "'";
                    dt = DataAccess.FetchRS(CommandType.Text, ssl);
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        NewId = Convert.ToInt32(dt.Tables[0].Rows[0]["Id"]);
                    }
                    else
                    {
                        ssl = "INSERT INTO AppraisalTemplate (Name,Active,CreatedBy,Cretaeddate)";

                        int creatdby = Convert.ToInt32(HttpContext.Current.Session["EmpCode"]);
                        string dtToday = String.Format("{0:yyyy-MM-dd hh:mm:ss}", DateTime.Now);
                        ssl += "Output Inserted.Id VALUES('" + NewTemplate.TemplateName + "',1," + creatdby + ",'" + dtToday + "')";
                        NewId = DataAccess.ExecuteScalar(ssl);
                        ssl = "";
                        foreach (int item in NewTemplate.ObjectiveId)
                        {
                            ssl += "INSERT INTO Appraisal_Template_Objective_Mapping (Objective_Id,Template_Id)";

                            ssl += " VALUES('" + item + "'," + NewId + ")";


                        }
                        Trmplterply = DataAccess.ExecuteStoreProc(ssl);
                    }
                }
            if (!Model.EmployeeAppraisal.AppraisalTitleExists(Appraisal.AppraisalName))
            {


                SqlDataReader empdr;
                int aprover = 0;
                string wflevel = "";


                
                    ssl = "";
                    foreach (int item in Appraisal.EmpId)
                    {
                        empdr = DataAccess.ExecuteReader(CommandType.Text, "select AppraisalsupervicerMulitilevel,emp_name+' '+emp_lname as EmpName from employee where emp_code=" + item, null);

                            if (empdr.Read())
                            {
                                
                                if (empdr[0] == null || empdr[0].ToString() == "NULL" || empdr[0] == DBNull.Value || empdr[0].ToString() =="-1")
                                {
                                     aprover = 0;

                                 

                                }
                                else
                                {

                                    aprover = Utility.ToInteger(empdr[0].ToString());
                                }

                            }
                      
                            wflevel = "Level0";
                            ssl += " INSERT INTO Appraisal(AppraisalName,EmpId,AppraisalTemplateID,Status,ApprisalApprover,WFLevel,EnbleToEmployee,DaysToApproveLevel,ValidFrom,ValidEnd,Period,AppraisalYear)";
                            Appraisal.AppraisalName = Appraisal.AppraisalName.Replace("'", "''");
                            stfrom = string.Format("{0:yyyy-MM-dd}", Appraisal.ValidFrom);
                            stend = string.Format("{0:yyyy-MM-dd}", Appraisal.ValidEnd);
                            Appraisal.AppraisalTemplateID = NewId != 0 ? NewId : Appraisal.AppraisalTemplateID;
                            ssl += " VALUES('" + Appraisal.AppraisalName + "'," + item + "," + Appraisal.AppraisalTemplateID + ",'Open'," + aprover + ",'" + wflevel + "'," + Appraisal.EnbleToEmployee + "," + Appraisal.DaysToApproveLevel + ",'" + stfrom + "','" + stend + "',1,"+ Appraisal.ValidFrom .Year+ ")";
                            if (strDone != "")
                                strDone += ", ";
                            strDone += empdr[1].ToString();
                      
                    }
                    if (ssl != "")
                        rply = DataAccess.ExecuteStoreProc(ssl);
                    else
                        rply = 0;
            }
            else
            {

                if(Trmplterply > 0)
                {
                    return JsonConvert.SerializeObject(new { rply = "Appraisal with same name already exixts please add this appraisal with some other name. And a New Template with the name '"+NewTemplate.TemplateName+"' has been saved that can be used again.", done = false });

                }
                else
                {
                    return JsonConvert.SerializeObject(new { rply = "Appraisal with same name already exixts please add this appraisal with some other name.", done = false });

                }
            }
             if (rply == 0)
                return JsonConvert.SerializeObject(new { rply = "Appraisal for all the selected employees has not been added as as their supervisiors are not assigned ", done = false });
            else
                return JsonConvert.SerializeObject(new { rply = "Appraisal for all the selected employees has been added for this session", done = true });

        }




        [System.Web.Services.WebMethod]
        public static string AppExists(Model.Employee[] Emp,  DateTime Validfrom)
        {
            Model.Employee[]  Ids = new Model.Employee[Emp.Length];
            int index = 0;
            bool Found = false;
            
            foreach (var item in Emp)
            {
                if (Model.EmployeeAppraisal.Exists(item.EmpId, string.Format("{0:yyyy-MM-dd}", Validfrom)))
                {
                    Ids[index] = new Model.Employee();
                    Ids[index] = item;
                    Found = true;
                    index++;
                }
            }


            return JsonConvert.SerializeObject(new { Ids = Ids, IsFound = Found });

        }


        [System.Web.Services.WebMethod]
        public static string WorkflowExist(int[] Emp, string Type)
        {
            string[] empnames = { "" };
            int index = 0;
            SqlDataReader empdr = null;
            string ssl = "", name = "", wfappraisal="";
            int id = 0; Model.Employee[] arrID=null;
            bool IsWFEnabled = false,Found=false;
            string compsql = "select WFAppraisal from company where Company_id=" + compid;
            SqlDataReader compdr = DataAccess.ExecuteReader(CommandType.Text, compsql, null);
            if (compdr.Read())
            {
                wfappraisal = compdr[0].ToString();
            }
            if (wfappraisal != "0")
            {
                IsWFEnabled = true;
            }
            else
            {
                IsWFEnabled = false;
            }


            if (Type == "Departments")
                ssl = "Select  emp.emp_name+' '+emp.emp_lname as EmpName,emp.emp_code as EmpId, dept.DeptName as TypeName from "
                + "employee as emp join department as dept on dept.id = emp.dept_id where ";
            else if (Type == "Teams")
                ssl = "Select t.Emp_ID as EmpId , e.emp_name+' '+e.emp_lname as EmpName, Appt.Team_Name  as TypeName "
                + "from TeamAppraisalAssigned as t inner join employee as e on t.Emp_ID=e.emp_code "
                + " inner join TeamAppraisal as Appt on t.Team_ID = Appt.ID where ";
            else
            {
                ssl = "SELECT emp_name+' '+emp_lname as EmpName,emp_code as EmpId, '' as TypeName FROM dbo.employee WHERE ";
                IsWFEnabled = false;
            }

            for (int i = 0; i < Emp.Length; i++)
            {
                if (i != 0)
                {

                    ssl += " or ";
                }
                if (Type == "Departments") { ssl += "dept_id = " + Emp[i]; }

                else if (Type == "Teams") { ssl += "t.Team_ID =" + Emp[i]; }

                else { ssl += "emp_code=" + Emp[i]; }
                    
            }
                DataSet dt = DataAccess.FetchRS(CommandType.Text, ssl);

                if (dt.Tables.Count > 0 && dt.Tables[0].Columns.Count > 0)
                {
                    arrID = new Model.Employee[dt.Tables[0].Rows.Count];
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        id = Convert.ToInt32(dt.Tables[0].Rows[i]["EmpId"]);
                        empdr = DataAccess.ExecuteReader(CommandType.Text, "select AppraisalsupervicerMulitilevel from employee where emp_code=" + id, null);
                        if (empdr.Read())
                        {
                        // string s = empdr[0].ToString();

                        if (empdr[0] == null || empdr[0].ToString() == "NULL" || empdr[0] == DBNull.Value || empdr[0].ToString() == "-1")
                        {
                            arrID[i] = new Model.Employee();
                            arrID[i].EmpId = id;
                            arrID[i].EmpName = dt.Tables[0].Rows[i]["EmpName"].ToString();
                            arrID[i].TypeName = dt.Tables[0].Rows[i]["TypeName"].ToString();
                            Found = true;
                        }


                        }
                    }
                }
                return JsonConvert.SerializeObject(new { NotSeletedIds = arrID, AllSelectedEmployee = dt.Tables[0], IsWFEnabled = IsWFEnabled , isFound = Found});
            

            

        }
        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }

    }





    public class SendEmailJob : IJob
    {
        public int CompanyId { get; set; }

        LogWriter log;
        public SendEmailJob()
        {
            log = new LogWriter();
        }

        public void Execute()
        {
            log.LogWrite("Email Service Start");
            try
            {
                sendemail();
            }
            catch (Exception ex)
            {

                log.LogWrite(ex.Message);
            }
        }

        public  int sendemail()
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
            string sql = @"select * from Appraisal where Status='Open' and  Day(ValidFrom) = day(GETDATE()) and Month(ValidFrom) = MONTH(GETDATE()) and YEAR(ValidFrom)= year(GETDATE())";

            SqlDataReader appdr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            while (appdr.Read())
            {

                log.LogWrite("Email:  "+appdr["EmpId"].ToString());
                from_date = string.Format("{0:yyyy-MM-dd}", appdr["ValidFrom"]);
                to_date = string.Format("{0:yyyy-MM-dd}", appdr["ValidEnd"]);

                code = appdr["EmpId"].ToString();


                string sSQLemail = "sp_send_email";
                SqlParameter[] parmsemail = new SqlParameter[2];
                parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
                parmsemail[1] = new SqlParameter("@compid", CompanyId);
                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
                while (dr3.Read())
                {

                   
                    from = Utility.ToString(dr3.GetValue(15));
                    to = Utility.ToString(dr3.GetValue(3));
                    SMTPserver = Utility.ToString(dr3.GetValue(6));
                    SMTPUser = Utility.ToString(dr3.GetValue(7));
                    SMTPPass = Utility.ToString(dr3.GetValue(8));
                    emp_name = Utility.ToString(dr3.GetValue(5));
                    body = Utility.ToString(dr3.GetValue(20));
                    SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                    emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                    cc = Utility.ToString(dr3.GetValue(17));

                    log.LogWrite("from: " + from);

                }
                if (emailreq == "yes")
                {
                    log.LogWrite("Email Required Yes");
                    if (to.ToString().Trim().Length <= 0)
                    {
                        to = cc;
                    }
                    string subject = "Appraisal Request By HR Dept.";
                    body = body.Replace("@emp_name", "HR Dept.");
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date);



                    body = body.Replace("@app_name", appdr["AppraisalName"].ToString());

                  

                    //body = body.Replace("@leave_balance", dblbalanceavail.ToString());
                    //body = body.Replace("@paid_leaves", pdleave.ToString());
                    //body = body.Replace("@unpaid_leaves", updleave.ToString());
                    //body = body.Replace("@timesession", strts.ToString());
                    // body = body.Replace("@reason", requestRemarks.ToString());


                    //r
                    //SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

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
                    //check if MultiLevel -ram
                    bool isMultiLevel = true; // NEED TO ASSIGN
                    if (isMultiLevel)
                    {
                        log.LogWrite("IsMultilevel: True");
                        //string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                        if (appdr["WFLevel"].ToString() != "Level0")
                        {


                            //string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                            // string sql2 = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select AppraisalsupervicerMulitilevel from employee where emp_code=" + code + ")))";
                            string sql2 = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=" + appdr["ApprisalApprover"].ToString() + "))";
                            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql2, null);

                            StringBuilder strUpdateBuild = new StringBuilder();
                            while (dr.Read())
                            {
                                to = dr[0].ToString();

                            }


                        }


                    }

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
                                to = to + ";" + dr11[0].ToString();
                            }

                        }


                    }
                    log.LogWrite("To: "+to);

                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;
                    oANBMailer.MailBody = body;

                    if (to.Length == 0 || from.Length == 0)
                    {

                        //ShowMessageBox("Please check email address is not configured yet");
                        return 0;
                    }

                    try
                    {
                        emplevel = DataAccess.ExecuteReader(CommandType.Text, "select rowid from EmployeeWorkFlowLevel where id=(select AppraisalsupervicerMulitilevel from employee where emp_code=" + code + ")", null);
                        if (emplevel.Read())
                        {
                            wflevel = "Level" + emplevel[0].ToString();

                        }
                        if (Utility.ToInteger(appdr["EnbleToEmployee"].ToString()) == 1)
                        {

                            sqlupdate = "update Appraisal set Status='InProgress' where id=" + appdr["id"].ToString();
                        }
                        else
                        {
                            sqlupdate = "update Appraisal set Status='InProgress',WFLevel='" + wflevel + "' where id=" + appdr["id"].ToString();
                        }
                        DataAccess.ExecuteNonQuery(sqlupdate, null);

                    }
                    catch (Exception ex)
                    {

                        throw;
                    }




                    try
                    {
                        //string sRetVal = oANBMailer.SendMail();
                        string sRetVal = oANBMailer.SendMail("Appraisal", emp_name, from_date, to_date, "Appraisal Request");
                        log.LogWrite("EmailSuccess: " + sRetVal);

                        if (sRetVal == "SUCCESS")
                        {

                            mailcount = mailcount + 1;
                            Thread.Sleep(10000);

                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        //  strMessage = strMessage + "<brtus/>" + "Error Occured While Sending Mail.";
                        log.LogWrite("from: " + ex.Message);
                    }
                }
            }
            return mailcount;

        }





        

    }
}
