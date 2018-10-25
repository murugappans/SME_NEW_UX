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
using System.Data;

namespace SMEPayroll 
{
    public partial class forgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(Session["ANBPRODUCT"].ToString() == "SME")
           {
               TitleID.Text="SME PAYROLL 10.0 WEB PORTAL";
           }
           else
           {
               TitleID.Text = "Workers Management System 10.0";
           }

        }
       
        protected void sendemail(string userid, string pwd, int compid,string email)
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
                    if (sRetVal != "")
                    {
                        lblerr.Text  = "Password has been sent succesfully to " + to.ToString();
                    }                       
                }
                catch (Exception ex)
                {
                    
                }
            }

        protected void button_Click(object sender, EventArgs e)
        {

            string userId = txtEmailId.Value;
            if (Convert.ToString(userId) != "")
            {
                string sqlQuery = "select email,password,Company_Id from employee where userName='" + userId + "'";
                HttpContext.Current.Session["ConString"]= "Data Source=" + Constants.DB_SERVER + ";Initial Catalog=" + Constants.DB_NAME + ";User ID=" + Constants.DB_UID + ";Password=" + Constants.DB_PWD;
                DataSet ds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);

                string userid = "";
                string pwd = "";
                string email = "";
                userid = userId;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    email = Utility.ToString(ds.Tables[0].Rows[0]["email"]);
                    pwd = encrypt.SyDecrypt(Utility.ToString(ds.Tables[0].Rows[0]["password"]));
                    int compid = Utility.ToInteger(ds.Tables[0].Rows[0]["Company_Id"]);
                    sendemail(userid, pwd, compid, email);
                    lblerr.Text = "Password has been sent succesfully  " ;
                }
                else {
                    lblerr.Text = "Please Enter Valid UserId";
                }
            }
            else 
            {
                lblerr.Text = "Please Enter your UserId";
            }
        }
        }
    }

