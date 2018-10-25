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
using IRAS;
using System.Xml;
using Ionic.Zip;
namespace SMEPayroll
{
    public partial class Login_soft : System.Web.UI.Page
    {
       
 protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {
           

            //Label1.Text = "";
            //    SMEPayroll.BusinessRule.LoginInfo.SmeConnectionString = Constants.CONNECTION_STRING;

               // lblyear.Text = "Current Year : " + DateTime.Now.Year.ToString();
               
                   // lblyear.Text = "Current Year : " + DateTime.Now.Year.ToString();
                      HttpContext.Current.Session["Country"] = "301";
                    if (!IsPostBack)
                    {

                        


                        Session.LCID = 2057;
                        HttpContext.Current.Session.Clear();
                        string sSQL = "Select Company_Id, Company_Name From Company";
                        Utility.FillDropDown(drpcompany, sSQL);
                    }
                   
         



           
        }

        protected static void ShowMessageBox(string message)
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




        //protected void BtnLogin(object sender, EventArgs e)
        //{



        //    if (drpcompany.SelectedIndex.ToString() == "0")
        //    {
        //        //Label1.Visible = true;
        //        //Label1.Text = "Please select the Company";
        //        alert_show.show_alert(this, "002");

        //    }
        //    else
        //    {
        //        try
        //        {
        //            string filePath = "";
        //            string TargetDirectory = Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());

        //            string fileCount = "";

        //            if (System.IO.Directory.Exists(TargetDirectory))
        //            {
        //                foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory))
        //                {
        //                    filePath = fileName;
        //                }

        //                if (System.IO.Directory.GetFiles(TargetDirectory).Length > 1)
        //                {
        //                    fileCount = "2";
        //                }

        //                if ((filePath != "") && (fileCount == ""))
        //                {
        //                    bool Login_OK = Utility.GetLoginOK(drpcompany.SelectedItem.Value, txtUserName.Value.ToString(), txtPwd.Value.ToString());
        //                    if (Login_OK == true)
        //                    {
        //                        //SMEPayroll.BusinessRule.LoginInfo.SmeUserName  = txtUserName.Value.ToString();
        //                        //SMEPayroll.BusinessRule.LoginInfo.SmeEmpPassword = txtPwd.Value.ToString();
        //                        Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);


        //                        Response.Redirect("frames/default.aspx");


        //                        //Response.Redirect("Reports/Girofile_MidSalary.aspx");


        //                        //Response.Redirect("TimeSheet/Pattern.aspx");
        //                        //Response.Redirect("Management/DailyAttendance.aspx");


        //                        //Response.Redirect("Payroll/BulkAdd_Fast.aspx");//Multi addition fast import





        //                    }
        //                    else
        //                    {
        //                        //this.Label1.Visible = true;
        //                        //Label1.Text = "Invalid Login/Inactive User Account.";

        //                        alert_show.show_alert(this, "001");
        //                    }
        //                }
        //                else if (fileCount != "2")
        //                {
        //                    //this.Label1.Visible = true;
        //                    //Label1.Text = "License File Missing";
        //                }
        //                else if (fileCount == "2")
        //                {
        //                    //this.Label1.Visible = true;
        //                   // Label1.Text = "License Files tampered.";
        //                }
        //            }
        //            else
        //            {
        //                //this.Label1.Visible = true;
        //                //Label1.Text = "License Files Path is incorrect";
        //            }
        //        }
        //        catch (Exception exc)
        //        {
        //            throw exc;
        //        }
        //    }
        //}
        string country = "301";

        protected void BtnLogin_current(object sender, EventArgs e)
        {
           


            if (drpcompany.SelectedIndex.ToString() == "0")
            {
                //Label1.Visible = true;
                //Label1.Text = "Please select the company name and then try again";
            }
            else
            {
                try
                {
                    if (!ShowLicense())
                    {


                        bool Login_OK = Utility.GetLoginOK(drpcompany.SelectedItem.Value, txtUserName.Value.ToString(), txtPwd.Value.ToString());
                        if (Login_OK == true)
                        {

                            XmlDocument docXML = new XmlDocument();
                            docXML.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
                            XmlNodeList nodTitles = docXML.GetElementsByTagName("ANBProduct");
                            XmlNodeList nodcountry = docXML.GetElementsByTagName("country");
                            for (int i = 0; i < nodTitles.Count; i++)
                            {
                                HttpContext.Current.Session[((XmlElement)(nodTitles[i])).GetAttribute("id")] = ((XmlElement)(nodTitles[i])).GetAttribute("text");
                            }
                            for (int i = 0; i < nodcountry.Count; i++)
                            {
                                country = ((XmlElement)(nodcountry[i])).GetAttribute("id");
                            }

                            HttpContext.Current.Session["Country"] = country;



                            HttpContext.Current.Session["IR8AYEAR"] = cmbYear.SelectedItem.Text.ToString();
                            Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);
                            Response.Redirect("frames/default.aspx");
                        }
                        else
                        {
                            //this.Label1.Visible = true;
                            //Label1.Text = "Invalid Login (or) Inactive User Account. Please Try Again";
                        }
                    }
                    else
                    {
                        ShowMessageBox("Maintance Expired");
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }


        protected void BtnLogin(object sender, EventArgs e)
        {



            if (drpcompany.SelectedIndex.ToString() == "0")
            {
                //Label1.Visible = true;
                //Label1.Text = "Please select the Company";
                alert_show.show_alert(this, "002");

            }
            else
            {
                try
                {
                    string filePath = "";
                    string TargetDirectory = Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());

                    string fileCount = "";

                    if (System.IO.Directory.Exists(TargetDirectory))
                    {
                        foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory))
                        {
                            filePath = fileName;
                        }

                        if (System.IO.Directory.GetFiles(TargetDirectory).Length > 1)
                        {
                            fileCount = "2";
                        }

                        if ((filePath != "") && (fileCount == ""))
                        {
                            bool Login_OK = Utility.GetLoginOK(drpcompany.SelectedItem.Value, txtUserName.Value.ToString(), txtPwd.Value.ToString());
                            if (Login_OK == true)
                            {
                        
                                Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);


                               


                              

                                XmlDocument docXML = new XmlDocument();
                                docXML.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
                                XmlNodeList nodTitles = docXML.GetElementsByTagName("ANBProduct");
                                XmlNodeList nodcountry = docXML.GetElementsByTagName("country");
                                for (int i = 0; i < nodTitles.Count; i++)
                                {
                                    HttpContext.Current.Session[((XmlElement)(nodTitles[i])).GetAttribute("id")] = ((XmlElement)(nodTitles[i])).GetAttribute("text");
                                }
                                for (int i = 0; i < nodcountry.Count; i++)
                                {
                                    country = ((XmlElement)(nodcountry[i])).GetAttribute("id");
                                }

                                HttpContext.Current.Session["Country"] = country;



                                HttpContext.Current.Session["IR8AYEAR"] = cmbYear.SelectedValue.ToString();
                                Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);
                                Response.Redirect("frames/default.aspx");



                            }
                            else
                            {
                                string id = "101";
                                string message;
                                string message_type = "E";

                                XmlDocument xmlDoc = new XmlDocument();
                                xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/message.xml"));
                                XmlNode messege_id;
                                try
                                {
                                    messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Message/MessageId[@id='" + id.ToString().Trim() + "']");
                                    message = messege_id.Attributes[2].Value.ToString();
                                    message_type = messege_id.Attributes[1].Value.ToString();
                                }
                                catch (Exception ex)
                                {
                                    message = "Error";
                                }

                                //this.Label1.Visible = true;
                                //Label1.Text = "Invalid Login/Inactive User Account.";
                                alert_show.show_alert(this, "001");
                            }
                        }
                        else if (fileCount != "2")
                        {
                            //this.Label1.Visible = true;
                            //Label1.Text = "License File Missing";
                        }
                        else if (fileCount == "2")
                        {
                            //this.Label1.Visible = true;
                            // Label1.Text = "License Files tampered.";
                        }
                    }
                    else
                    {
                        //this.Label1.Visible = true;
                        //Label1.Text = "License Files Path is incorrect";
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }

        public bool ShowLicense()
        {
            bool expirelicence = true;

            int id = 0;
            //if (Utility.ToString(Session["Username"]) == "")
            //    Response.Redirect("../SessionExpire.aspx");
            //id = Utility.ToInteger(Session["Compid"].ToString());
            int iTotalEmployeesInDB = 0, iTotalEmployeesAllowed;
            string sSQL = "";

            string sKey = System.Configuration.ConfigurationManager.AppSettings["SYS_CONFIG"];

            string[] skey = new string[4];
            skey[0] = "0x59185499C345D05F92CED";
            skey[1] = "1FC2CF2BD2C8BCE8D3462EF";
            skey[2] = "0749EF3CDC4096C6EC516D5";
            skey[3] = "10115D05EA097524FB22C22";

            sKey = sKey.ToUpper().ToString();
            for (int i = 0; i <= 3; i++)
            {
                sKey = sKey.Replace(skey[i].ToUpper().ToString(), "");
            }
            sKey = sKey.Replace("X", "");

            iTotalEmployeesAllowed = Utility.ToInteger(sKey.Replace("X", ""));
            sSQL = "SELECT count(DISTINCT ic_pp_number) FROM employee WHERE company_id <> 1 and termination_date is null";
            System.Data.SqlClient.SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

            while (dr.Read())
            {
                iTotalEmployeesInDB = Utility.ToInteger(dr.GetValue(0));
            }

            if (Session["Certificationinfo"] == null)
            {
                string filePath = "";
                string zipFileName = "";
                string TargetDirectory = Utility.ToString(System.Configuration.ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());
                foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory))
                {
                    // zipFileName = fileName;

                    //if (fileName.Substring(fileName.Length - 3, 3) == "txt")//check text file
                    {
                        //filePath = fileName;
                        zipFileName = fileName;
                    }
                }
                if (System.IO.File.Exists(zipFileName))
                {
                    using (ZipFile zip = ZipFile.Read(zipFileName))
                    {
                        zip.Password = "!Secret1";
                        zip.ExtractAll(TargetDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }

                    foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory + @"\CERTIFICATE"))
                    {
                        // filePath = fileName;

                        if (fileName.Substring(fileName.Length - 3, 3) == "txt")//check text file
                        {
                            filePath = fileName;
                        }
                    }

                    //...Read Data From TextFile and show data in data grid for Certification...
                    DataSet Certificationinfo = Utility.GetDataSetFromTextFile(filePath);
                    Session["Certificationinfo"] = Certificationinfo;
                    //RadGridCertification.DataBind();
                    //Delete Files Once Data Gets in Session
                    try
                    {
                        foreach (string dirName in System.IO.Directory.GetDirectories(TargetDirectory))
                        {
                            if (dirName != zipFileName)
                            {
                                System.IO.Directory.Delete(dirName, true);
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            string LicenseInfo = "";
            if (Session["Certificationinfo"] != null)
            {
                DataSet info = (DataSet)Session["Certificationinfo"];
                string RowsAllowed = info.Tables[0].Rows[12][1].ToString().Trim();
                iTotalEmployeesAllowed = Convert.ToInt32(RowsAllowed);

                LicenseInfo = "License ";
               

                // LicenseInfo = LicenseRemaining + " { " + iTotalEmployeesAllowed.ToString() + " - " + iTotalEmployeesInDB.ToString() + " } ";

               // LicenseInfo = iTotalEmployeesAllowed.ToString() + " - " + iTotalEmployeesInDB.ToString() + "=" + LicenseRemaining;
                DateTime dt = new DateTime();
                string cerDate = "";

                if (info.Tables[0].Rows[14][1] != null)
                {
                    cerDate = info.Tables[0].Rows[14][1].ToString();
                    dt = Convert.ToDateTime(cerDate);
                }

                DateTime today = DateTime.Now;
                TimeSpan daysDifferenc = dt.Subtract(today);

                if (daysDifferenc.Days >= 0)
                {
                    expirelicence = false;
                }
                
               
            }
            return expirelicence;
        }
        }

        public class alert_show
        {
            public static void show_alert(Page type, string id)
            {
                string message;
                string message_type = "E";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/message.xml"));
                XmlNode messege_id;
                try
                {
                    messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Message/MessageId[@id='" + id.ToString().Trim() + "']");
                    message = messege_id.Attributes[2].Value.ToString();
                    message_type = messege_id.Attributes[1].Value.ToString();
                }
                catch (Exception ex)
                {
                    message = "Error";
                }



                StringBuilder sbScript = new StringBuilder();



                sbScript.Append("<script type='text/javascript' src='/assets/scripts/show.js'>" + Environment.NewLine);

                sbScript.Append(@"</script>");

                HttpContext.Current.Response.Write(sbScript);


                type.ClientScript.RegisterStartupScript(type.GetType(), "myScript", "AnotherFunction('" + message + "','" + message_type + "')", true);
            }
        }
        public class alert_msg
        {
            public static string show_alert(string id)
            {
                string message;
                string message_type = "E";

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/message.xml"));
                XmlNode messege_id;
                try
                {
                    messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Message/MessageId[@id='" + id.ToString().Trim() + "']");
                    message = messege_id.Attributes[2].Value.ToString();
                    message_type = messege_id.Attributes[1].Value.ToString();
                }
                catch (Exception ex)
                {
                    message = "Error";
                }



                //StringBuilder sbScript = new StringBuilder();

                //sbScript.Append("<script type='text/javascript' src='/scripts/show.js'>" + Environment.NewLine);

                //sbScript.Append(@"</script>");

                //HttpContext.Current.Response.Write(sbScript);


                //type.ClientScript.RegisterStartupScript(type.GetType(), "myScript", "AnotherFunction('" + message + "','" + message_type + "')", true);

                return message;

            }
        }

    }

