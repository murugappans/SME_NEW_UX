using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.IO;

namespace SMEPayroll.Model
{
    public class ANBMailer
    {
        private int iCompID = 0;
        public string To = "";
        public string ToName = "";
        public string From = "";
        public string FromName = "";
        public string Cc = "";
        public string CcName = "";
        public string Subject = "";
        public string MailBody = "";
        public string SMTPHost = "";
        public string SMTPUserID = "";
        public string SMTPPassword = "";
        public string ssl_required = "";

        private Chilkat.MailMan mailman = new Chilkat.MailMan();
        private Chilkat.Email email = new Chilkat.Email();

        public ANBMailer(int comp_id)
        {
            try
            {
                int smtp_port = 0;
                iCompID = comp_id;

                string sSQL = "sp_submit_email1";
                SqlParameter[] parm = new SqlParameter[1];
                parm[0] = new SqlParameter("@comp_id", iCompID);
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parm);


                //email_SMTP_server, email_username, email_password, email_reply_name, email_smtp_port, email_payalert,
                //email_sender_domain, sslrequired, ccmail

                while (dr1.Read())
                {                    
                     mailman.SmtpHost = Utility.ToString(dr1.GetValue(0));
                     mailman.SmtpUsername = Utility.ToString(dr1.GetValue(1));                    
                     mailman.SmtpPassword = Utility.ToString(dr1.GetValue(2));
                    //kumar change
                     email.FromName = Utility.ToString(dr1.GetValue(6));
                     smtp_port = Utility.ToInteger(dr1.GetValue(4));
                    //Added By Raja--(19/12/2008)
                    ssl_required = Utility.ToString(dr1.GetValue(7));


                    if (ssl_required == "" || ssl_required == "No")
                    {
                        mailman.SmtpSsl = false;
                    }
                    else if (ssl_required == "TSL")
                    {
                        mailman.StartTLS = true;

                    }
                    else if (ssl_required == "Yes")
                    {
                        mailman.SmtpSsl = true;
                    }


                     if (smtp_port > 0)
                        mailman.SmtpPort = smtp_port;                     

                     if (email.From == "")
                        email.From = Utility.ToString(dr1.GetValue(1));
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message.ToString(); // to be rendered later
            }
        }

        public ANBMailer(string smtp_server, string smtp_uid, string smtp_pwd, string s_from, string s_from_name, int smtp_port, string ssl)
        {
            mailman.SmtpHost = smtp_server;
            mailman.SmtpUsername = smtp_uid;
            mailman.SmtpPassword = smtp_pwd;
            //mailman.StartTLS = true;

            if (ssl == "" || ssl == "No")
            {
                mailman.SmtpSsl = false;
            }
            else if (ssl == "TSL")
            {
                mailman.StartTLS = true;
              

            }
            else if (ssl == "Yes")
            {
                mailman.SmtpSsl = true;
            }

            
            email.From = s_from;
            email.FromName = s_from_name;
            
            if (smtp_port > 0)
                mailman.SmtpPort = smtp_port;
        }

        public string Attachment = "";
        public ArrayList alAttachment = null;

        public void CleanMail()
        {
            this.DropAttachment();
            this.ClearAllToCCBCC();
        }

        public void DropAttachment()
        {
            email.DropAttachments();
        }
        public void ClearAllToCCBCC()
        {
            email.ClearBcc();
            email.ClearCC();
            email.ClearTo();
        }

        public string SendMail()
        {
            try
            {                
                string contentType = "";
                bool success = mailman.UnlockComponent("ADVANCMAILQ_uoDUcdqd6Z7D");

                if (this.From == "")
                    return "FROM parameter is not set.";

                if (this.To == "")
                    return "TO parameter is not set.";
                if (this.FromName == "")
                    this.FromName = this.From;

                if (this.ToName == "")
                    this.ToName = this.To;

                if (this.CcName == "")
                    this.CcName = this.Cc;

                email.From = this.From;
                email.FromName = this.FromName;
                email.AddTo(this.ToName, this.To);
                if (this.Cc != "")
                {
                    email.AddCC(this.CcName, this.Cc);
                }
                email.Subject = this.Subject;
                //email.Body = this.MailBody;
                email.SetHtmlBody(this.MailBody);

                email.ReplyTo = email.From;

                mailman.SmtpAuthMethod = "LOGIN";

                //mailman.SmtpSsl = true;     // for Vishal only.

                // attach single file
                if (this.Attachment != "")
                {
                    contentType = email.AddFileAttachment(this.Attachment);
                    if (contentType == null)
                        return "Can not attach file. " + this.Attachment;
                }

                // attach multiple files
                if (this.alAttachment != null)
                {
                    for (int i = 0; i < alAttachment.Count - 1; i++)
                    {
                        contentType = email.AddFileAttachment(alAttachment[i].ToString());
                        if (contentType == null)
                            return "Can not attach file. " + alAttachment[i].ToString();
                    }
                }
                
                success = mailman.SendEmail(email);


                //if (success == false)
                //    CreateLog(mailman.LastErrorText);

                if (success != true)
                    return mailman.LastErrorText;

                success = mailman.CloseSmtpConnection();
                if (success != true)
                    return mailman.LastErrorText;



                //if (success)
                //    return "success";
                

                return "";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
               //CreateLog(ex.Message.ToString());
            }
        }


        //New Sendmail function which record the error and status in "EmailTrackerNew"
        public string  SendMail(string Module,string emp_name,string from_date, string to_date, string Reason)
        {

            LogWriter log = new LogWriter();
            try
            {
                string success_fail="";
                string contentType = "";
                bool success = mailman.UnlockComponent("ADVANCMAILQ_uoDUcdqd6Z7D");

                if (this.From == "")
                    return "FROM parameter is not set.";

                if (this.To == "")
                    return "TO parameter is not set.";
                if (this.FromName == "")
                    this.FromName = this.From;

                if (this.ToName == "")
                    this.ToName = this.To;

                if (this.CcName == "")
                    this.CcName = this.Cc;



                log.LogWrite("Email Parameter from: " +this.From+" To :"+ this.To);



                email.From = this.From;
                email.FromName = this.FromName;
                email.AddTo(this.ToName, this.To);
                if (this.Cc != "")
                {
                    email.AddCC(this.CcName, this.Cc);
                }
                email.Subject = this.Subject;
                //email.Body = this.MailBody;
                email.SetHtmlBody(this.MailBody);

                email.ReplyTo = email.From;

                mailman.SmtpAuthMethod = "LOGIN";

                //mailman.SmtpSsl = true;     // for Vishal only.

                // attach single file
                if (this.Attachment != "")
                {
                    contentType = email.AddFileAttachment(this.Attachment);
                    if (contentType == null)
                        return "Can not attach file. " + this.Attachment;
                }

                // attach multiple files
                if (this.alAttachment != null)
                {
                    for (int i = 0; i < alAttachment.Count - 1; i++)
                    {
                        contentType = email.AddFileAttachment(alAttachment[i].ToString());
                        if (contentType == null)
                            return "Can not attach file. " + alAttachment[i].ToString();
                    }
                }

                success = mailman.SendEmail(email);


                if (success == false)
                {
                    log.LogWrite("Email Error: " + mailman.LastErrorText);
                    CreateLog(Module, emp_name, from_date, to_date, "FAIL", Reason, mailman.LastErrorText);
                    success_fail = "FAIL";
                }

                if (success != true)
                    return mailman.LastErrorText;

                success = mailman.CloseSmtpConnection();
                if (success != true)
                    return mailman.LastErrorText;


                //r
                if (success)
                {
                   // CreateLog(Module, emp_name, from_date, to_date, "SUCCESS", Reason, "");
                    success_fail = "SUCCESS";
                }
                 return success_fail;

            }
            catch (Exception ex)
            {
                //CreateLog(Module, emp_name, from_date, to_date, status, ex.Message.ToString());
                return "Fail";
            }
        }

        #region Log
        public void CreateLog(string Module,string emp_name,string from_date, string to_date, string status,string reason,string remark)
            {
                string SqlString = "INSERT INTO [dbo].[EmailTrackerNew]([Emp_Name],[Module],[From],[To],[Status],[reason],[Remark],[company_Id]) VALUES ('" + emp_name + "','" + Module + "','" + from_date + "','" + to_date + "','" + status + "','" + reason + "','" + remark + "','" + Utility.ToInteger(HttpContext.Current.Session["Compid"]) + "')";
                DataAccess.ExecuteStoreProc(SqlString);


            /*
                string path = @"c:\temp\MyTest.txt";

                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string str = "*******************************************************************************************";
                    sw.WriteLine(str);
                    string txt = "Apply Date:" + System.DateTime.Now + " From: " + this.From + " Subject:" + this.Subject + " To:" + this.To + "Body:" + this.MailBody;
                    if (Err.Length > 0)
                    {
                        txt = txt +" Error Message - "+ Err.ToString();
                    }
                    sw.WriteLine(txt); 
                    sw.WriteLine(str);
                }
             */
            }
        #endregion




           
        }
}
