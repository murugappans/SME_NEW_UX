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
using System.Xml;
using efdata;
using Ionic.Zip;
using System.Collections.Generic;
using System.Data.SqlClient;
using FluentScheduler;
using System.IO;

namespace SMEPayroll
{
    public partial class Login_soft : System.Web.UI.Page
    {
        string comp_id;
        string compid = "", sSQL = "", empcode = "";
        //public IGetData getdata { get; set; }

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {
            //var text = this.getdata.GetMessage();
            //Label1.Text = "";
            //    SMEPayroll.BusinessRule.LoginInfo.SmeConnectionString = Constants.CONNECTION_STRING;
            ViewState["actionMessage"] = "";
            if (HttpContext.Current.Session["ANBPRODUCT"] != null)
            {
                // lblyear.Text = "Current Year : " + DateTime.Now.Year.ToString();
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

                    }
                    else
                    {
                        drpcompany.Visible = true;

                    }
                    Utility.FillDropDownCompany(drpcompany, sSQL);

                    if (Request.QueryString["sessiontimeout"] == "true")
                    {
                        //ViewState["LastLoginID"] = ViewState["LastLoginIDHidden"]; //GetSetLastLogin("", "Get");
                        ViewState["LastLoginState"] = "1";
                    }
                }
            }
            else
            {
                Response.Redirect("Index.aspx");
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
                  

            if (drpcompany.Visible == true)
            {
                if (string.IsNullOrWhiteSpace(txtUserName.Value) || string.IsNullOrWhiteSpace(txtPwd.Value)|| drpcompany.SelectedIndex.ToString() == "0")
                {
                    string _message = "Following fields are missing<br>";
                    if (string.IsNullOrWhiteSpace(txtUserName.Value))
                        _message += "User name<br>";
                    if (string.IsNullOrWhiteSpace(txtPwd.Value))
                        _message += "Password<br>";
                    if (drpcompany.SelectedIndex.ToString() == "0")
                        _message += "Company name";

                    ViewState["actionMessage"] = "Warning|" + _message;

                }
               else if (drpcompany.SelectedIndex.ToString() == "0")
                {
                    //Label1.Visible = true;
                    //Label1.Text = "Please select the Company";
                    //alert_show.show_alert(this, "002");TODO



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
                                    string script = "localStorage.setItem('LastLoginID'," + "'"+txtUserName.Value.ToString()+"')";
                                    ScriptManager.RegisterStartupScript(this,this.GetType(), "key", script, true);

                                    //GetSetLastLogin(txtUserName.Value.ToString(), "Set");
                                    ViewState["LastLoginIDHidden"] = txtUserName.Value.ToString();


                                    try
                                    {
                                        LoadCommonData();
                                        LoadSettingSession();
                                    }
                                    catch (Exception ex)
                                    {

                                        throw ex;
                                    }

                                   

                                    Response.Redirect("main/home.aspx");


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
                                        if (messege_id != null)
                                        {
                                            message = messege_id.Attributes[2].Value.ToString();
                                            message_type = messege_id.Attributes[1].Value.ToString();
                                        }
                                        else
                                        {
                                            message = "";
                                            message_type = "";
                                        }
                                 ViewState["actionMessage"] = "Warning|Either user name or password is incorrect.";

                                      
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
                                alert_show.show_alert(this, "010");
                            }
                            else if (fileCount == "2")
                            {
                                //this.Label1.Visible = true;
                                // Label1.Text = "License Files tampered.";
                                alert_show.show_alert(this, "010");
                            }
                        }
                        else
                        {
                            //this.Label1.Visible = true;
                            //Label1.Text = "License Files Path is incorrect";
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
                            bool Login_OK = Utility.GetLoginOK_withoutcompanyid(txtUserName.Value.ToString(), txtPwd.Value.ToString());
                            if (Login_OK == true)
                            {
                                //SMEPayroll.BusinessRule.LoginInfo.SmeUserName  = txtUserName.Value.ToString();
                                //SMEPayroll.BusinessRule.LoginInfo.SmeEmpPassword = txtPwd.Value.ToString();
                              Utility.setAllrights(txtUserName.Value.ToString(), HttpContext.Current.Session["Compid"].ToString());

                                try
                                {
                                    LoadCommonData();
                                    LoadSettingSession();
                                }
                                catch (Exception ex)
                                {

                                    throw ex;
                                }

                               


                                Response.Redirect("main/home.aspx");



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
                            //this.Label1.Visible = true;
                            //Label1.Text = "License File Missing";
                        }
                        else if (fileCount == "2")
                        {
                            //this.Label1.Visible = true;
                            //Label1.Text = "License Files tampered.";
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




        private void LoadSettingSession()
        {
            string strWF = "";
            string strWfGroup = "";
            string strNamePayroll = "";

            //Get The Details for If TSProcess
            string strProcess = "Select AppTSProcess,AppLeaveProcess,AppClaimsProcess from Company  WHERE Company_Id=" + Utility.ToInteger(Session["Compid"]);
            DataSet dsprocess = DataAccess.FetchRS(CommandType.Text, strProcess);

            if (dsprocess.Tables.Count > 0)
            {
                if (dsprocess.Tables[0].Rows.Count > 0)
                {
                    if (Session["processPayroll"] == null)
                    {
                        if (dsprocess.Tables[0].Rows[0]["AppTSProcess"] == DBNull.Value)
                        {
                            Session["processPayroll"] = "1";
                        }
                        else
                        {
                            Session["processPayroll"] = dsprocess.Tables[0].Rows[0]["AppTSProcess"].ToString();
                        }
                    }

                    if (Session["processLeave"] == null)
                    {
                        if (dsprocess.Tables[0].Rows[0]["AppLeaveProcess"] == DBNull.Value)
                        {
                            Session["processLeave"] = "1";
                        }
                        else
                        {
                            Session["processLeave"] = dsprocess.Tables[0].Rows[0]["AppLeaveProcess"].ToString();
                        }
                    }

                    if (Session["processClaim"] == null)
                    {
                        if (dsprocess.Tables[0].Rows[0]["AppClaimsProcess"] == DBNull.Value)
                        {
                            Session["processClaim"] = "1";
                        }
                        else
                        {
                            Session["processClaim"] = dsprocess.Tables[0].Rows[0]["AppClaimsProcess"].ToString();
                        }
                    }
                }
            }



            if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
            {
                if (Session["strWF"] == null)
                {
                    string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + Utility.ToInteger(Session["Compid"]);
                    DataSet dsWF = new DataSet();
                    dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                    if (dsWF.Tables.Count > 0)
                    {
                        if (dsWF.Tables[0].Rows.Count > 0)
                        {
                            strWF = dsWF.Tables[0].Rows[0][0].ToString();
                            Session["strWF"] = strWF;

                            if (dsWF.Tables[0].Rows[0]["WFPAY"] != DBNull.Value)
                            {
                                strNamePayroll = dsWF.Tables[0].Rows[0]["WFPAY"].ToString();
                                Session["PayrollWF"] = strNamePayroll;
                            }
                            else
                            {
                                strNamePayroll = "";
                                Session["PayrollWF"] = "";
                            }

                            if (strWF == "2")
                            {
                                if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                                {
                                    string strEmpSup = "SELECT * FROM EmployeeAssignedToWorkFlow2Sup ES INNER JOIN EmployeeWorkFlow  EW ON ES.PayrollGroupID=EW.ID WHERE ES.Emp_ID=" + Session["EmpCode"].ToString();
                                    DataSet dsEmpSup = new DataSet();
                                    dsEmpSup = DataAccess.FetchRS(CommandType.Text, strEmpSup, null);

                                    if (dsEmpSup.Tables.Count > 0)
                                    {
                                        if (dsEmpSup.Tables[0].Rows.Count > 0)
                                        {
                                            Session["dsEmpSup"] = dsEmpSup;
                                            strWfGroup = dsEmpSup.Tables[0].Rows[0]["PayrollGroupID"].ToString();
                                        }
                                    }

                                    if (Session["dsEmpSup"] != null)
                                    {
                                        string strEmpGrp = "SELECT * FROM EmployeeAssignedToWorkFlow2Emp WHERE PayrollGroupID=" + strWfGroup;
                                        DataSet dsEmpWF = new DataSet();
                                        dsEmpWF = DataAccess.FetchRS(CommandType.Text, strEmpGrp, null);
                                        Session["dsEmpWF"] = dsEmpWF;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    strWF = (string)Session["strWF"];
                }
            }
        }


        private  void LoadCommonData()
        {
            if (Session["Certificationinfo"] == null)
            {
                string TargetDirectory = Utility.ToString(ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());
                string filePath_cer = "";
                string zipFileName = "";
                string TargetDirectory_cer = Utility.ToString(ConfigurationSettings.AppSettings["TARGET_DIRECTORY"].ToString());
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

                    //...Read Data From TextFile and show data in data grid for Certification...
                    DataSet Certificationinfo_cer = Utility.GetDataSetFromTextFile(filePath_cer);
                    Session["Certificationinfo"] = Certificationinfo_cer;
                    //RadGridCertification.DataBind();
                    //Delete Files Once Data Gets in Session
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


            if (Session["Certificationinfo"] != null)
            {
                LicenceInfo licenceInfo = new LicenceInfo();

                int UsedLicence = 0;


                sSQL = "SELECT count(DISTINCT ic_pp_number) FROM employee WHERE company_id <> 1 and termination_date is null";
                System.Data.SqlClient.SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                while (dr.Read())
                {
                    UsedLicence = Utility.ToInteger(dr.GetValue(0));
                }



                DataSet info = (DataSet)Session["Certificationinfo"];
                string RowsAllowed = info.Tables[0].Rows[12][1].ToString().Trim();

                if (info.Tables[0].Rows[14][1] != null)
                {
                    var cerDate = info.Tables[0].Rows[14][1].ToString();
                    licenceInfo.ExpireDate = Convert.ToDateTime(cerDate);
                }

                if (info.Tables[0].Rows[14][1] != null)
                {
                    licenceInfo.IssuDate = Convert.ToDateTime(info.Tables[0].Rows[14][1].ToString().Trim()); ;
                }


                licenceInfo.NoOfLicence = Convert.ToInt32(RowsAllowed);
                licenceInfo.NoOfLiceceUsed = UsedLicence;

                //kumar-26-6-2017

                var commondata = new CommonData(int.Parse(drpcompany.SelectedItem.Value), txtUserName.Value.ToString(), licenceInfo);




                if (Session["commandata"] != null)
                {
                    Session["commandata"] = null;
                }
                if (Session["commandata"] == null)
                {
                    Session["commandata"] = commondata;
                }

                //kumar
                // buildmenu
                BuildMenu(commondata.LoginUser.UserName, commondata.CompanyExt.CompanyId);

                JobManager.Initialize(new SMERegistry(commondata.CompanyExt.CompanyId));

            }
        }



        private void BuildMenu(string UserName,int CompanyId)
        {
            MenuRepository menuBuilder = new MenuRepository();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = false;
            xmlDoc.Load(Server.MapPath("~/XML/Menu.xml"));

            //muru

            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.PreserveWhitespace = false;
            xmlDoc2.Load(Server.MapPath("~/XML/Menu2.xml"));

            //----------


            var menudatalist = new List<MenuData>()
                                     {
                                          new MenuData()
                                          {
                                             // RightId=40,
                                              RightId=304,
                                              Value=GetPendingClaims()
                                          },
                                          new MenuData()
                                          {
                                              //RightId=26,
                                              RightId=204,
                                              Value=GetPendingLeaves()
                                          },
                                          new MenuData()
                                          {
                                              //RightId=61,
                                              RightId=607,
                                              Value=GetApprovePayroll()
                                          }, new MenuData()
                                          {
                                             // RightId=62,
                                             RightId=608,
                                              Value=GetGeneratePayroll()
                                          },new MenuData()
                                          {
                                             // RightId=63,
                                              RightId=138,
                                              Value=GetPrintandUnlockPayroll()
                                          },
                                          new MenuData()
                                          {
                                              //RightId=64,
                                              RightId=45,
                                              Value=GetPrintandUnlockPayroll()
                                          }

                                     };


            var Navmenu = menuBuilder.BuildMenu(UserName, CompanyId,
              HttpContext.Current.Session["ANBPRODUCT"].ToString(), false, xmlDoc, menudatalist);
//muru
            var Navmenu2 = menuBuilder.BuildMenu(UserName, CompanyId,
              HttpContext.Current.Session["ANBPRODUCT"].ToString(), false, xmlDoc2, menudatalist);

            if (Session["NAVMENU"] != null)
            {
                Session["NAVMENU"] = null;
            }


            if (Session["NAVMENU"] == null)
            {
                Session["NAVMENU"] = Navmenu;
                Session["NAVMENU2"] = Navmenu2;
            }

            if (Session["Notification"] != null)
            {
                Session["Notification"] = null;
            }


            }



        protected string GetPendingClaims()
        {
            var count = "";
            string sgroupname = Utility.ToString(Session["GroupName"]);
            compid = Session["Compid"].ToString();
            empcode = Session["EmpCode"].ToString();
            if (sgroupname == "Super Admin")
            {
                sSQL = "select * FROM[emp_additions] e , additions_types a, employee b " +
                       "WHERE  e.trx_type = a.id AND e.emp_code = b.emp_code AND " +
                       "e.claimstatus NOT IN('Rejected', 'Approved', 'Open') AND b.company_id = '" + compid + "' ";
            }
            else
            {
                sSQL = "select * FROM[emp_additions] e , additions_types a, employee b " +
                       "WHERE  e.trx_type = a.id AND e.emp_code = b.emp_code AND " +
                       "e.claimstatus NOT IN('Rejected', 'Approved', 'Open') and b.emp_supervisor = '" + empcode + "' and b.Company_Id = '" + compid + "' ";
            }
            DataSet dcount5;
            dcount5 = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            if (dcount5.Tables[0].Rows.Count >= 0)
            {
                count = Convert.ToString(dcount5.Tables[0].Rows.Count);
            }
            return count;
        }
        protected string GetPendingLeaves()
        {
            var count = "";
            string sgroupname = Utility.ToString(Session["GroupName"]);
            compid = Session["Compid"].ToString();
            empcode = Session["EmpCode"].ToString();
            if (sgroupname == "Super Admin")
            {
                sSQL = "select * FROM[emp_leaves] a,leave_types b, employee c " +
                       "WHERE a.emp_id = c.emp_code AND leave_type = b.id AND " +
                       "[status] NOT IN ('Rejected', 'Approved') AND company_id = '" + compid + "' ";
            }
            else
            {
                sSQL = "select * FROM[emp_leaves] a,leave_types b, employee c " +
                      "WHERE a.emp_id = c.emp_code AND leave_type = b.id AND " +
                      "[status] NOT IN ('Rejected', 'Approved') and c.emp_supervisor = '" + empcode + "' and c.Company_Id = '" + compid + "' ";
            }
            DataSet dcount5;
            dcount5 = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            if (dcount5.Tables[0].Rows.Count >= 0)
            {
                count = Convert.ToString(dcount5.Tables[0].Rows.Count);
            }
            return count;
        }
        protected string GetApprovePayroll()
        {
            var count = "";
            comp_id = Session["Compid"].ToString();
            if (Session["ROWID"] != null)
            {
                DataSet monthDs;
                DataTable dtFilterFound;
                int i = 0;
                string ssql = "sp_GetPayrollMonth";
                SqlParameter[] parms = new SqlParameter[3];
                parms[i++] = new SqlParameter("@ROWID", "0");
                parms[i++] = new SqlParameter("@YEARS", 0);
                parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                dtFilterFound = new DataTable();
                dtFilterFound = monthDs.Tables[0].Clone();
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Session["ROWID"].ToString());
                foreach (DataRow dr in drResults)
                {
                    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                    Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                }
                string sSQLApprove = "";
                if (Convert.ToString(Session["GroupName"]) == "Super Admin")
                {
                    sSQLApprove =
                                       "Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                       "Inner Join  " +
                                       "( " +
                                       "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                       "	Inner Join prepare_payroll_detail pd " +
                                       "	On ph.Trx_ID = pd.Trx_ID " +
                                       "	Where pd.[Status] = 'P' And " +
                                       "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                       ") pd " +
                                       "On Em.Emp_Code = pd.Emp_ID " +
                                       "Where Em.Company_id = " + comp_id;
                }
                else
                {
                    sSQLApprove =
                                         "Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                         "Inner Join  " +
                                         "( " +
                                         "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                         "	Inner Join prepare_payroll_detail pd " +
                                         "	On ph.Trx_ID = pd.Trx_ID " +
                                         "	Where pd.[Status] = 'P' And " +
                                         "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                         ") pd " +
                                         "On Em.Emp_Code = pd.Emp_ID " +
                                         "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";
                }
                string sSQL = sSQLApprove;
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                count = ds.Tables[0].Rows[0][0].ToString();
                
            }
            return count;
        }
        protected string GetGeneratePayroll()
        {
            var count = "";
            comp_id = Session["Compid"].ToString();
            if (Session["ROWID"] != null)
            {
                DataSet monthDs;
                DataTable dtFilterFound;
                int i = 0;
                string ssql = "sp_GetPayrollMonth";
                SqlParameter[] parms = new SqlParameter[3];
                parms[i++] = new SqlParameter("@ROWID", "0");
                parms[i++] = new SqlParameter("@YEARS", 0);
                parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                dtFilterFound = new DataTable();
                dtFilterFound = monthDs.Tables[0].Clone();
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Session["ROWID"].ToString());
                foreach (DataRow dr in drResults)
                {
                    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                    Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                }
                string sSQLGenerate = "";
                if (Convert.ToString(Session["GroupName"]) == "Super Admin")
                {

                    sSQLGenerate =
                                       ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                       "Inner Join  " +
                                       "( " +
                                       "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                       "	Inner Join prepare_payroll_detail pd " +
                                       "	On ph.Trx_ID = pd.Trx_ID " +
                                       "	Where pd.[Status] = 'A' And " +
                                       "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                       ") pd " +
                                       "On Em.Emp_Code = pd.Emp_ID " +
                                       "Where Em.Company_id = " + comp_id;
                }
                else
                {
                    sSQLGenerate =
                                       ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                                       "Inner Join  " +
                                       "( " +
                                       "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                                       "	Inner Join prepare_payroll_detail pd " +
                                       "	On ph.Trx_ID = pd.Trx_ID " +
                                       "	Where pd.[Status] = 'A' And " +
                                       "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                                       ") pd " +
                                       "On Em.Emp_Code = pd.Emp_ID " +
                                       "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";
                }
                string sSQL = sSQLGenerate;
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                count = ds.Tables[0].Rows[0][0].ToString();

            }
            return count;
        }
        protected string GetPrintandUnlockPayroll()
        {
            var count = "";
            comp_id = Session["Compid"].ToString();
            if (Session["ROWID"] != null)
            {
                DataSet monthDs;
                DataTable dtFilterFound;
                int i = 0;
                string ssql = "sp_GetPayrollMonth";
                SqlParameter[] parms = new SqlParameter[3];
                parms[i++] = new SqlParameter("@ROWID", "0");
                parms[i++] = new SqlParameter("@YEARS", 0);
                parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
                monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                dtFilterFound = new DataTable();
                dtFilterFound = monthDs.Tables[0].Clone();
                DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Session["ROWID"].ToString());
                foreach (DataRow dr in drResults)
                {
                    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                    Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                }
                string sSQLUnlock = "";
                if (Convert.ToString(Session["GroupName"]) == "Super Admin")
                {

                    sSQLUnlock =
                               ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                               "Inner Join  " +
                               "( " +
                               "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                               "	Inner Join prepare_payroll_detail pd " +
                               "	On ph.Trx_ID = pd.Trx_ID " +
                               "	Where pd.[Status] = 'G' And " +
                               "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                               ") pd " +
                               "On Em.Emp_Code = pd.Emp_ID " +
                               "Where Em.Company_id = " + comp_id;
                }
                else
                {
                    sSQLUnlock =
                           ";Select isnull(Count(Emp_Code),0) CntEmpID From Employee Em " +
                           "Inner Join  " +
                           "( " +
                           "	Select pd.Emp_ID Emp_ID From prepare_payroll_hdr ph " +
                           "	Inner Join prepare_payroll_detail pd " +
                           "	On ph.Trx_ID = pd.Trx_ID " +
                           "	Where pd.[Status] = 'G' And " +
                           "  (Convert(datetime,ph.start_period,103) >=Convert(datetime,'" + Session["PaySubStartDate"].ToString() + "',103) And Convert(datetime,ph.end_period,103) <= Convert(datetime,'" + Session["PaySubEndDate"].ToString() + "',103)) " +
                           ") pd " +
                           "On Em.Emp_Code = pd.Emp_ID " +
                           "Where Em.Company_id = " + comp_id + " and Em.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR Em.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")";
                }
                string sSQL = sSQLUnlock;
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                count = ds.Tables[0].Rows[0][0].ToString();

            }
            return count;
        }


        private string GetSetLastLogin(string _userid, string _type)
        {
            var _userID = "";
            if (_type == "Set")
                using (StreamWriter objWriter = new StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/assets/LastLogin.txt")))
                {

                    objWriter.Write(_userid);


                    objWriter.Close();

                }
            else

                using (StreamReader objReader = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/assets/LastLogin.txt")))
                {

                    _userID = objReader.ReadLine();

                    objReader.Close();

                }

            return _userID;
        }

    }
}

