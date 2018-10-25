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
using System.Text;
using System.Data.SqlClient;

namespace SMEPayroll
{
    public partial class forgot_password : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
               ViewState["actionMessage"] = "";
            //if (Session["ANBPRODUCT"].ToString() == "SME")
            //{
            //    TitleID.Text = "SME PAYROLL 10.0 WEB PORTAL";
            //}
            //else
            //{
            //    TitleID.Text = "Workers Management System 10.0";
            //}          
        }



        protected void sendemail(string userid, string pwd, int compid, string email)
        {
            string from = "Administrator";
            string to = email;
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string emailreq = "";
            int SMTPPORT = 25;


            string subject = "Password for User Id : " + userid;
            string Body = "Passport for the Account Of :" + userid;
            SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);
            oANBMailer.Subject = "Password for User Id ";
            oANBMailer.MailBody = "Your Password for Payroll : " + pwd;
            oANBMailer.From = from;
            oANBMailer.To = to;
            try
            {
                string sRetVal = oANBMailer.SendMail();


                if (sRetVal == "")
                {
                    //lblerr.Text = "Password has been sent succesfully to " + to.ToString();
                    //alert_show.show_alert(this, "006");


                }
                else
                {
                   // alert_show.show_alert(this, "008");
                }
            }
            catch (Exception ex)
            {
                //alert_show.show_alert(this, "001");
            }
        }


        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx");
        }



        protected void button_Click(object sender, EventArgs e)
        {

            string userId = txtEmailId.Value;
            if (Convert.ToString(userId) != "")
            {
                //string sqlQuery = "select email,password,Company_Id from employee where termination_date is  null and userName='" + userId + "'";

                //--------Protect From SQL Injection by murugan
                string sqlQuery = "select email,password,Company_Id from employee where termination_date is  null and userName=@userId";
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@userId", userId);
               //-----------------------------

                HttpContext.Current.Session["ConString"] = "Data Source=" + Constants.DB_SERVER + ";Initial Catalog=" + Constants.DB_NAME + ";User ID=" + Constants.DB_UID + ";Password=" + Constants.DB_PWD;
                //DataSet ds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                DataSet ds = DataAccess.FetchRS(CommandType.Text, sqlQuery,param1 );
                string userid = "";
                string pwd = "";
                string email = "";
                userid = userId;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    email = Utility.ToString(ds.Tables[0].Rows[0]["email"]);
                    pwd = encrypt.SyDecrypt(Utility.ToString(ds.Tables[0].Rows[0]["password"]));
                    int compid = Utility.ToInteger(ds.Tables[0].Rows[0]["Company_Id"]);
                    if (email != "")
                    {   
                          ViewState["actionMessage"] = "Success|Password has been sent to your registered email id succesfully";                   
                        sendemail(userid, pwd, compid, email);
                      
                        //lblerr.Text = "Password has been sent succesfully  ";
                    }
                    else
                    {
                        ViewState["actionMessage"] = "Warning|Email has not been set in your account.";
                        // alert_show.show_alert(this, "009");
                    }
                  
                }
                else
                {
                    ViewState["actionMessage"] = "Warning|Input valid user name.";
                    // lblerr.Text = "Please Enter Valid UserId";
                    // alert_show.show_alert(this, "005");
                }
            }
            else
            {
                //lblerr.Text = "Please Enter your UserId";
                //alert_show.show_alert(this, "004");
                ViewState["actionMessage"] = "Warning|Enter user name.";
            }
        }
    }

}