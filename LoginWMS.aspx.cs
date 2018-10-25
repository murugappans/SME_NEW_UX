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

namespace SMEPayroll
{
    public partial class LoginWMS : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {

            
            
            Label1.Text = "";
        //    SMEPayroll.BusinessRule.LoginInfo.SmeConnectionString = Constants.CONNECTION_STRING;
            if (HttpContext.Current.Session["ANBPRODUCT"] != null)
            {
                lblyear.Text = "Current Year : " + DateTime.Now.Year.ToString();
                if (!IsPostBack)
                {
                   // string sSQL = "Select Company_Id, Company_Name From Company Order By Company_name";

                    //if alise name then show alias name
                    string sSQL = "Select C.Company_Id,case WHEN CA.AliasName<>'' THEN CA.AliasName  ELSE C.Company_Name END Company_Name  From Company C left join [Company_Alias]as CA on C.Company_id=CA.Company_id Order By Company_name";
                    string sSQL_wc = "Select C.Company_Id,case WHEN CA.AliasName<>'' THEN CA.AliasName  ELSE C.Company_Name END Company_Name,loginWithOutComany  From Company C left join [Company_Alias]as CA on C.Company_id=CA.Company_id where C.Company_id=1 Order By Company_name";

                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, sSQL_wc, null);

                    if (ds.Tables[0].Rows[0]["loginWithOutComany"].ToString() == "1")
                    {
                        drpcompany.Visible = false;
                        com_lable.Visible = false;
                        btnLogin.Visible = false;
                        btnLogin1.Visible = true;
                    }
                    else
                    {
                        drpcompany.Visible = true;
                        com_lable.Visible = true;
                        btnLogin.Visible = true;
                        btnLogin1.Visible = false;
                    }

                    Utility.FillDropDown(drpcompany, sSQL);
                }
            }
            else
            {
                Response.Write("Please Close Browser and restart browser");
                Response.End();
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

                   


        protected void BtnLogin(object sender, EventArgs e)
        {


                if (drpcompany.SelectedIndex.ToString() == "0")
                {
                    //Label1.Visible = true;
                    //Label1.Text = "Please select the Company";
                    //alert_show.show_alert(this, "002");

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
                                    //SMEPayroll.BusinessRule.LoginInfo.SmeUserName  = txtUserName.Value.ToString();
                                    //SMEPayroll.BusinessRule.LoginInfo.SmeEmpPassword = txtPwd.Value.ToString();
                                    Utility.setAllrights(txtUserName.Value.ToString(), drpcompany.SelectedItem.Value);


                                    Response.Redirect("frames/default.aspx");


                                    //Response.Redirect("Reports/Girofile_MidSalary.aspx");


                                    //Response.Redirect("TimeSheet/Pattern.aspx");
                                    //Response.Redirect("Management/DailyAttendance.aspx");


                                    //Response.Redirect("Payroll/BulkAdd_Fast.aspx");//Multi addition fast import





                                }
                                else
                                {
                                    //this.Label1.Visible = true;
                                    //Label1.Text = "Invalid Login/Inactive User Account.";

                                    //alert_show.show_alert(this, "001");
                                }
                            }
                            else if (fileCount != "2")
                            {
                                this.Label1.Visible = true;
                                Label1.Text = "License File Missing";
                            }
                            else if (fileCount == "2")
                            {
                                this.Label1.Visible = true;
                                Label1.Text = "License Files tampered.";
                            }
                        }
                        else
                        {
                            this.Label1.Visible = true;
                            Label1.Text = "License Files Path is incorrect";
                        }
                    }
                    catch (Exception exc)
                    {
                        throw exc;
                    }
                }
           
        }



        protected void BtnLogin_withoutcomaony(object sender, EventArgs e)
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
                            bool Login_OK = Utility.GetLoginOK_withoutcompanyid( txtUserName.Value.ToString(), txtPwd.Value.ToString());
                            if (Login_OK == true)
                            {
                                //SMEPayroll.BusinessRule.LoginInfo.SmeUserName  = txtUserName.Value.ToString();
                                //SMEPayroll.BusinessRule.LoginInfo.SmeEmpPassword = txtPwd.Value.ToString();
                                Utility.setAllrights(txtUserName.Value.ToString(), HttpContext.Current.Session["Compid"].ToString());


                                Response.Redirect("frames/default.aspx");


                                //Response.Redirect("Reports/Girofile_MidSalary.aspx");


                                //Response.Redirect("TimeSheet/Pattern.aspx");
                                //Response.Redirect("Management/DailyAttendance.aspx");


                                //Response.Redirect("Payroll/BulkAdd_Fast.aspx");//Multi addition fast import





                            }
                            else
                            {
                                //this.Label1.Visible = true;
                                //Label1.Text = "Invalid Login/Inactive User Account.";

                                alert_show.show_alert(this, "001");
                            }
                        }
                        else if (fileCount != "2")
                        {
                            this.Label1.Visible = true;
                            Label1.Text = "License File Missing";
                        }
                        else if (fileCount == "2")
                        {
                            this.Label1.Visible = true;
                            Label1.Text = "License Files tampered.";
                        }
                    }
                    else
                    {
                        this.Label1.Visible = true;
                        Label1.Text = "License Files Path is incorrect";
                    }
                }
                catch (Exception exc)
                {
                    throw exc;
                }
           
        }
    }
}
