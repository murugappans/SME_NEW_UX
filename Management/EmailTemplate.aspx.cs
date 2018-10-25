using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll.Management
{
    public partial class EmailTemplate : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");


           


            
            try
            {
                if (!Page.IsPostBack)
                {

                    //string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                    //ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);



               DataSet dsEmailTemplate = new DataSet();
            //CPF Changes
            string Str = "select [Id],[TemplateName] from EmailTemplate  union  select '-1' as Id,'Select Template' as Templatename";

            dsEmailTemplate = DataAccess.FetchRS(CommandType.Text, Str, null);

           


            this.TemplateName.DataSource = dsEmailTemplate.Tables[0];
            this.TemplateName.DataValueField = dsEmailTemplate.Tables[0].Columns["Id"].ColumnName.ToString();
            this.TemplateName.DataTextField = dsEmailTemplate.Tables[0].Columns["TemplateName"].ColumnName.ToString();

            this.TemplateName.DataBind();


                    int compid = Utility.ToInteger(Session["Compid"]);
                    Load_Template();
                }
                
            }
            catch (Exception ex) { }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           // System.Threading.Thread.Sleep(5000);
            string id = "";

            id = this.TemplateName.SelectedValue;
            if(id != "-1")
            Load_EmailTemplate(id);
        }


        protected void Load_Template()
        {
            try
            {
                int compid = Utility.ToInteger(Session["Compid"]);
                DataSet dsCompset = new DataSet();
                //CPF Changes
                string Str = " select * from company where Company_Id=" + compid + "";

                dsCompset = DataAccess.FetchRS(CommandType.Text, Str, null);
                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_sender_name"]) != "")
                {
                    EditorLevReq.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_sender_name"]);
                }
                else
                {
                    EditorLevReq.Content = "Greetings,"
                                           + " Leave application submitted by: @emp_name."
                                            + "Type of leave applied:@leave_type."
                                            + "Leave balance as of today: @leave_balance."
                                            + "Period of leave application: @from_date to @to_date."
                                           + " Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves."
                                           + " AM or PM (applicable only for 0.5 day leave): @timesession"

                                           + " Thanks and Regards"
                                           + " Advanced & Best Technologies Pte Ltd"
                                           + " Office: 6837 2336 | 6223 7996 Fax: 6220 4532"
                                           + " www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_address"]) != "")
                {
                    Editortxtemail_replyaddress.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_address"]);
                }
                else
                {
                    Editortxtemail_replyaddress.Content = "Greetings, @approver has @status your applied leaves from @from_date to @to_date <br />REMARKS: @reason;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]) != "")
                {
                    Editortxtemail_leavedel.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]);
                }
                else
                {
                    Editortxtemail_leavedel.Content = "Greetings,  Leave Applied Deleted of: @emp_name. Type of Leave Applied:@leave_type. Period of Leave Application: @from_date to @to_date. Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.Status: @status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";

                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]) != "")
                {
                    Editortxtemail_leavedel.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]);
                }
                else
                {
                    Editortxtemail_leavedel.Content = "Greetings,  Leave Applied Deleted of: @emp_name. Type of Leave Applied:@leave_type. Period of Leave Application: @from_date to @to_date. Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.Status: @status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";

                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_name"]) != "")
                {
                    Editortxtemail_replyname.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_name"]);
                }
                else
                {
                    Editortxtemail_replyname.Content = "Greetings, Payroll for the period  @month / @year has been submitted  by @hr for your appropal.Please review the payroll and update the status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_sender_name"]) != "")
                {
                    Editortxtclaim_sendername.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_sender_name"]);
                }
                else
                {
                    Editortxtclaim_sendername.Content = "Greetings,@emp_name has requested claim for the month of  @month @year; Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_reply_name"]) != "")
                {
                    Editortxtemailclaim_replyname.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_reply_name"]);
                }
                else
                {
                    Editortxtemailclaim_replyname.Content = "Greetings, @approver has @status your applied claim for the month of @month @year;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_payslip"]) != "")
                {
                    EditorPayslip.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_payslip"]);
                }
                else
                {
                    EditorPayslip.Content = "Your payroll has been processed for the month of  @month/@year . Please find attached epayslip.";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_login"]) != "")
                {
                    EditorEmployee.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_login"]);
                }
                else
                {
                    EditorEmployee.Content = "Greetings @emp_name; Following are your Login particulars. -##- User ID:@user_name -##- Password:@password " ;
                }
                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_timesheet"]) != "")
                {
                    EditorTimesheet.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_timesheet"]);
                }
                else
                {
                    EditorTimesheet.Content = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br><br>";
                }
                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["Reminder_template"]) != "")
                {
                    this.ReminderEditor.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["Reminder_template"]);
                }
                else
                {
                    ReminderEditor.Content = @"Greetings
                      Please find attached the reminders generated for Company Name:@CompanyName
                      Date:@Date";

                }
            }
            catch (Exception ex) { }
            
        }

        protected void Load_EmailTemplate(string id)
        {
            try
            {
                
                DataSet dsCompset = new DataSet();
                //CPF Changes
                string Str = " select * from EmailTemplate where Id='"+id+"'";

                dsCompset = DataAccess.FetchRS(CommandType.Text, Str, null);

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_sender_name"]) != "")
                {
                    EditorLevReq.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_sender_name"]);
                }
                else
                {
                    EditorLevReq.Content = "Greetings,"
                                           + " Leave application submitted by: @emp_name."
                                            + "Type of leave applied:@leave_type."
                                            + "Leave balance as of today: @leave_balance."
                                            + "Period of leave application: @from_date to @to_date."
                                           + " Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves."
                                           + " AM or PM (applicable only for 0.5 day leave): @timesession"

                                           + " Thanks and Regards"
                                           + " Advanced & Best Technologies Pte Ltd"
                                           + " Office: 6837 2336 | 6223 7996 Fax: 6220 4532"
                                           + " www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_address"]) != "")
                {
                    Editortxtemail_replyaddress.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_address"]);
                }
                else
                {
                    Editortxtemail_replyaddress.Content = "Greetings, @approver has @status your applied leaves from @from_date to @to_date <br />REMARKS: @reason;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]) != "")
                {
                    Editortxtemail_leavedel.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]);
                }
                else
                {
                    Editortxtemail_leavedel.Content = "Greetings,  Leave Applied Deleted of: @emp_name. Type of Leave Applied:@leave_type. Period of Leave Application: @from_date to @to_date. Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.Status: @status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";

                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]) != "")
                {
                    Editortxtemail_leavedel.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_leave_delete"]);
                }
                else
                {
                    Editortxtemail_leavedel.Content = "Greetings,  Leave Applied Deleted of: @emp_name. Type of Leave Applied:@leave_type. Period of Leave Application: @from_date to @to_date. Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.Status: @status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";

                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_name"]) != "")
                {
                    Editortxtemail_replyname.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_reply_name"]);
                }
                else
                {
                    Editortxtemail_replyname.Content = "Greetings, Payroll for the period  @month / @year has been submitted  by @hr for your appropal.Please review the payroll and update the status.Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_sender_name"]) != "")
                {
                    Editortxtclaim_sendername.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_sender_name"]);
                }
                else
                {
                    Editortxtclaim_sendername.Content = "Greetings,@emp_name has requested claim for the month of  @month @year ; Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_reply_name"]) != "")
                {
                    Editortxtemailclaim_replyname.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_claim_reply_name"]);
                }
                else
                {
                    Editortxtemailclaim_replyname.Content = "Greetings, @approver has @status your applied claim for the month of @month @year;Thanks and Regards Advanced & Best Technologies Pte Ltd Office: 6837 2336 | 6223 7996 Fax: 6220 4532 www.anbgroup.com";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_payslip"]) != "")
                {
                    EditorPayslip.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_payslip"]);
                }
                else
                {
                    EditorPayslip.Content = "Your payroll has been processed for the month of  @month/@year . Please find attached epayslip.";
                }

                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_login"]) != "")
                {
                    EditorEmployee.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_login"]);
                }
                else
                {
                    EditorEmployee.Content = "Greetings @emp_name; Following are your Login particulars. -##- User ID:@user_name -##- Password:@password ";
                }
                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["email_timesheet"]) != "")
                {
                    EditorTimesheet.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["email_timesheet"]);
                }
                else
                {                           
                    EditorTimesheet.Content = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br><br>";
                }
                if (Utility.ToString(dsCompset.Tables[0].Rows[0]["Reminder_template"]) != "")
                {
                    this.ReminderEditor.Content = Utility.ToString(dsCompset.Tables[0].Rows[0]["Reminder_template"]);
                }
                else
                {
                    ReminderEditor.Content = @"Greetings
                      Please find attached the reminders generated for Company Name:@CompanyName
                      Date:@Date";

                }

            }
            catch (Exception ex) { 

            }

        }



        protected void ChangeTemplate(object sender ,EventArgs e)
        {

            System.Threading.Thread.Sleep(5000);
            string id = "";

            id = this.TemplateName.SelectedValue;

            Load_EmailTemplate(id);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try {
                string email_replyaddress = Editortxtemail_replyaddress.Content;
                
                string email_replyname = Editortxtemail_replyname.Content;

                string email_leavedel = Editortxtemail_leavedel.Content;

                string email_sendername = EditorLevReq.Content;

                string email_claim_sender_name = Editortxtclaim_sendername.Content;

                string email_claim_reply_name = Editortxtemailclaim_replyname.Content;
                string reminder_message = ReminderEditor.Content;

                //parms[24] = new SqlParameter("@email_reply_address", Utility.ToString(email_replyaddress));
                //parms[25] = new SqlParameter("@email_reply_name", Utility.ToString(email_replyname));
                //parms[26] = new SqlParameter("@email_leavedel", Utility.ToString(email_leavedel));
                //parms[28] = new SqlParameter("@email_sender_name", Utility.ToString(email_sendername));
                //parms[40] = new SqlParameter("@email_claim_sender_name", Utility.ToString(email_claim_sender_name));
                //parms[41] = new SqlParameter("@email_claim_reply_name", Utility.ToString(email_claim_reply_name));
                string email_payslip = EditorPayslip.Content;
                
                string email_login = EditorEmployee.Content;
                string email_timesheet = EditorTimesheet.Content;
                int compid = Utility.ToInteger(Session["Compid"]);               

                string strUpdate = "";
                strUpdate = "Update Company set email_reply_address='" + email_replyaddress.Replace("'", "''") + "', email_reply_name='" + email_replyname.Replace("'", "''") + "', " +
                    "email_leave_delete='" + email_leavedel.Replace("'", "''") + "', email_sender_name='" + email_sendername.Replace("'", "''") + "', email_claim_sender_name='" + email_claim_sender_name.Replace("'", "''") + "', " +
                    "email_claim_reply_name='" + email_claim_reply_name.Replace("'", "''") + "',email_payslip='" + email_payslip.Replace("'", "''") + "',email_login='" + email_login.Replace("'", "''") + "' , email_timesheet = '" + email_timesheet.Replace("'", "''") + "' , Reminder_template = '" + reminder_message.Replace("'", "''") + "'  where Company_Id=" + compid + "";

                //strUpdate = "Update EmailTemplate set email_reply_address='" + email_replyaddress.Replace("'", "''") + "', email_reply_name='" + email_replyname.Replace("'", "''") + "', " +
                //    "email_leave_delete='" + email_leavedel.Replace("'", "''") + "', email_sender_name='" + email_sendername.Replace("'", "''") + "', email_claim_sender_name='" + email_claim_sender_name.Replace("'", "''") + "', " +
                //    "email_claim_reply_name='" + email_claim_reply_name.Replace("'", "''") + "',email_payslip='" + email_payslip.Replace("'", "''") + "',email_login='" + email_login.Replace("'", "''") + "' where Id=" + 2+ "";

                
                
                
                DataAccess.FetchRS(CommandType.Text, strUpdate, null);

                Load_Template();
            }
            catch (Exception ex) {
          //km  
         Response.Redirect("../Payroll/EmailErrorPayroll.aspx?Err=" + Server.UrlEncode(ex.Message));
            
            
            }
        }
    }
}
