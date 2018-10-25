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
using System.Xml;
using efdata;
using Ionic.Zip;
using System.Collections.Generic;

namespace SMEPayroll.Main
{
    public partial class Lock_Screen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                string returnUrl = "";
                var EmployeeName = "";
                var username = "";
                var ANBPRODUCT = HttpContext.Current.Session["ANBPRODUCT"].ToString();
                var CountryID = HttpContext.Current.Session["Country"].ToString();
                if (Utility.ToString(Session["Username"]) == "")
                {                
                   EmployeeName = Session["Emp_Namelockscreen"].ToString();
                    username = Session["Usernamelockscreen"].ToString();
                }
                else
                {
                    EmployeeName = Session["Emp_Name"].ToString();
                    username = Utility.ToString(Session["Username"]);
                }
                LblEmployeeName.Text = EmployeeName;
                lblusername.Text = username;
                if (string.IsNullOrEmpty(returnUrl) && Request.UrlReferrer != null)
                    returnUrl = Request.UrlReferrer.PathAndQuery;
                Lblurl.Text = returnUrl;
                string sSQL = "Select C.Company_Id,case WHEN CA.AliasName<>'' THEN CA.AliasName  ELSE C.Company_Name END Company_Name  From Company C left join [Company_Alias]as CA on C.Company_id=CA.Company_id Order By Company_name";
                string sSQL_wc = "Select C.Company_Id,case WHEN CA.AliasName<>'' THEN CA.AliasName  ELSE C.Company_Name END Company_Name,loginWithOutComany  From Company C left join [Company_Alias]as CA on C.Company_id=CA.Company_id where C.Company_id=1 Order By Company_name";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL_wc, null);

                if (ds.Tables[0].Rows[0]["loginWithOutComany"].ToString() == "1")
                {
                    drpcompany.Visible = false;
                }
                else
                {
                    drpcompany.Visible = true;
                }
                Utility.FillDropDownCompany(drpcompany, sSQL);
                //Session.Remove("Username");
                //HttpContext.Current.Session.Clear();
                HttpContext.Current.Session["ANBPRODUCT"] = ANBPRODUCT;
                HttpContext.Current.Session["Country"] = CountryID;
                Session["Emp_Namelockscreen"] = EmployeeName;
                Session["Usernamelockscreen"] = username;
            }
        }
        protected void BtnLogin(object sender, EventArgs e)
        {
            if (drpcompany.Visible == true)
            {

                if (drpcompany.SelectedIndex.ToString() == "0")
                {
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
                                bool Login_OK = Utility.GetLoginOK(drpcompany.SelectedItem.Value, lblusername.Text.ToString(), txtPwd.Value.ToString());
                                if (Login_OK == true)
                                {


                                    if (Session["Certificationinfo"] == null)
                                    {
                                        string filePath_cer = "";
                                        string zipFileName = "";
                                        string TargetDirectory_cer = Utility.ToString(ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());
                                        foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory))
                                        {
                                            {
                                                zipFileName = fileName;
                                            }
                                        }
                                        if (System.IO.File.Exists(zipFileName))
                                        {
                                            using (ZipFile zip = ZipFile.Read(zipFileName))
                                            {
                                                zip.Password = "!Secret1";
                                                zip.ExtractAll(TargetDirectory_cer, ExtractExistingFileAction.OverwriteSilently);
                                            }

                                            foreach (string fileName in System.IO.Directory.GetFiles(TargetDirectory_cer + @"\CERTIFICATE"))
                                            {
                                                // filePath = fileName;

                                                if (fileName.Substring(fileName.Length - 3, 3) == "txt")//check text file
                                                {
                                                    filePath_cer = fileName;
                                                }
                                            }
                                            DataSet Certificationinfo_cer = Utility.GetDataSetFromTextFile(filePath_cer);
                                            Session["Certificationinfo"] = Certificationinfo_cer;
                                            try
                                            {
                                                foreach (string dirName in System.IO.Directory.GetDirectories(TargetDirectory_cer))
                                                {
                                                    if (dirName != zipFileName)
                                                    {
                                                        System.IO.Directory.Delete(dirName, true);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                    Utility.setAllrights(lblusername.Text.ToString(), drpcompany.SelectedItem.Value);
                                    var commondata = CommonData.InstanceCreation();
                                    commondata.SetCommonData(int.Parse(drpcompany.SelectedItem.Value), lblusername.Text.ToString());
                                    BuildMenu(commondata.LoginUser.UserName, commondata.CompanyExt.CompanyId);
                                    Response.Redirect(Lblurl.Text.ToString());
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
                                    alert_show.show_alert(this, "001");
                                }
                            }
                            else if (fileCount != "2")
                            {
                                alert_show.show_alert(this, "010");
                            }
                            else if (fileCount == "2")
                            {
                                alert_show.show_alert(this, "010");
                            }
                        }
                        else
                        {
                            alert_show.show_alert(this, "010");
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                }
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
                            bool Login_OK = Utility.GetLoginOK_withoutcompanyid(lblusername.Text.ToString(), txtPwd.Value.ToString());
                            if (Login_OK == true)
                            {
                                Utility.setAllrights(lblusername.Text.ToString(), HttpContext.Current.Session["Compid"].ToString());
                                var commondata = CommonData.InstanceCreation();
                                commondata.SetCommonData(int.Parse(drpcompany.SelectedItem.Value), lblusername.Text.ToString());
                                BuildMenu(commondata.LoginUser.UserName, commondata.CompanyExt.CompanyId);
                                Response.Redirect(Lblurl.Text.ToString());
                            }
                            else
                            {
                                alert_show.show_alert(this, "001");
                            }
                        }
                        else if (fileCount != "2")
                        {
                        }
                        else if (fileCount == "2")
                        {
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
            }
        }

        private void BuildMenu(string UserName, int CompanyId)
        {
            MenuRepository menuBuilder = new MenuRepository();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.Load(Server.MapPath("~/XML/Menu.xml"));


            var menudatalist = new List<MenuData>()
                                     {
                                  

                                     };



            var menu = menuBuilder.BuildNavMenu(UserName, CompanyId,
                HttpContext.Current.Session["ANBPRODUCT"].ToString(), false, xmlDoc, menudatalist);


            if (Session["SMEMENU"] != null)
            {
                Session["SMEMENU"] = null;
            }


            if (Session["SMEMENU"] == null)
            {
                Session["SMEMENU"] = menu;
            }

        }
    }
}