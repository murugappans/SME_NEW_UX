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
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SMEPayroll.employee
{
    public partial class Employee : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int s=0,varCompid;
        string _actionMessage = "";
        protected string sMsg = "";
        #region Dataset command
        protected void Page_Load(object sender, EventArgs e)
        {
            

            ViewState["actionMessage"] = "";
            
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
          

            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            //if (HttpContext.Current.Session["CurrentCompany"].ToString() != "1")
            //{
            drpCompany.Visible = false;
            //}
            varCompid = Utility.ToInteger(Session["Compid"]);
            if (Session["ErrorMessage"] != null)
            {
                sMsg = Utility.ToString(Session["ErrorMessage"]);
            }
            else
            {
                sMsg = "";
            }
            if (!IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Employee_List";//muru
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
               
                drpShowAll.Items.Insert(0, new ListItem("Show Only Terminated Employee", "2"));
                drpShowAll.Items.Insert(0, new ListItem("Show All Employee", "3"));

                if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                {
                    string sql = "Select isMaster, isMasterEmpTemp From Company Where Company_ID=1";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    if (dr.Read())
                    {
                        string ismaster = dr[0].ToString();
                        string ismastertempemp = dr[1].ToString();

                        if (ismaster == "True" && ismastertempemp == "True")
                        {
                            drpShowAll.Items.Insert(0, new ListItem("Show Employee on Hold", "4"));
                        }
                    }
                }
                else
                {
                    string sql = "select isMaster, isMasterEmpTemp From Company Where Company_ID=1";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    if (dr.Read())
                    {
                        string ismaster = dr[0].ToString();
                        string ismastertempemp = dr[1].ToString();
                        if (ismaster == "True" && ismastertempemp == "True")
                        {
                            drpShowAll.Items.Insert(0, new ListItem("Show Employee on Hold", "4"));
                        }
                    }
                }
                drpShowAll.Items.Insert(0, new ListItem("Show Only Active Employee", "1"));
                SqlDataSource1.ConnectionString = Session["ConString"].ToString();
                Session["s"] = 0;
                bindgrid();                
            }
            else
            {
                Session["Message"] = "";
            }
            Session["ErrorMessage"] = null;
            lblMessage.InnerHtml = "";
        }

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                if (AddNewUser() == false)
                {
                    GridCommandItem commandItem = (GridCommandItem)e.Item;
                    commandItem.FindControl("HyperLink1").Visible = false;
                }
            }

            //for count in ToolBar
            //if (e.Item is GridPagerItem)
            //{
            //    string item;
            //    int count;
            //    GridPagerItem pager = (GridPagerItem)e.Item;
            //    item = pager.Paging.DataSourceCount.ToString();
            //    count = pager.Paging.DataSourceCount;

            //    //Finding the control inside the toolbar
            //    Label label = (Label)tbRecord.FindItemByText("Count").FindControl("Label_count");
            //    label.Text = "Count : " + count.ToString();

            //}

            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e,tbRecord);
            //


        }

      

      
        /// <summary>
        /// Show Number of Lice
        /// </summary>
        public Boolean AddNewUser()
        {
            int id = 0;            
            id = Utility.ToInteger(Session["Compid"].ToString());
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
            
            if (Session["Certificationinfo"] != null)
            {
                DataSet info = (DataSet)Session["Certificationinfo"];
                string RowsAllowed = info.Tables[0].Rows[12][1].ToString().Trim();
                iTotalEmployeesAllowed = Convert.ToInt32(RowsAllowed);
            }
            bool flag = true;
            string LicenseInfo = "";
            LicenseInfo = "License ";
            double NumberOfEmp = Convert.ToDouble(iTotalEmployeesAllowed - iTotalEmployeesInDB);
            int number;

            string strNegative = NumberOfEmp.ToString().Substring(0, 1);

            if (strNegative=="-")
            {
                flag = false;
            }
            return flag;
        }

        void bindgrid()
        {
            RadGrid1.DataSource = this.EmployeeDetails;
            RadGrid1.DataBind();
        }


        protected void RadGrid1_ItemDataBound1(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
               ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("AddEditEmployee.aspx?empcode=" + dataItem["EmpCode"].Text);
            }

            //Export
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||  e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
            }

            

        }
        

        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);

                //this.RadGrid1.MasterTableView.Rebind();
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet EmployeeDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", varCompid);
                parms1[1] = new SqlParameter("@show", Convert.ToInt16(drpShowAll.SelectedItem.Value.ToString()));
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                string sSQL = "sp_GetEmployees";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);

                //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname,(select Designation from Designation where id=desig_id) Designation from employee e where termination_date is null and Company_Id=" + varCompid + " ORDER BY emp_name ";
                //ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.EmployeeDetails;
        }
        #endregion Dataset command

        #region Delete command

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string EmpCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"]);
                string sSQL = "sp_emp_delete";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@emp_code", Utility.ToInteger(EmpCode));
                string sql = "select a.username,b.company_code from employee a,company b where a.company_id=b.company_id and emp_code=" + EmpCode;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if (dr.Read())
                {
                    string username = dr[0].ToString();
                    string usernam1 = dr[1].ToString() + "Admin";
                    if (username == usernam1)
                    {
                       // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This superadmin employee record cannot be deleted. "));
                        _actionMessage = "Warning|This superadmin employee record cannot be deleted. ";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        int i = DataAccess.ExecuteStoreProc(sSQL, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete the User. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        #endregion Delete command

        protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.GetLoginOKCompRunDB(drpCompany.SelectedValue.ToString(), "anbsysadmingroup");
            varCompid = Utility.ToInteger(Session["Compid"]);
            showemployees(drpShowAll.SelectedItem.Value.ToString());
        }
        protected void RadGrid1_ItemDataBound1(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Add Employee")) == true)
            {
                if (e.Item is GridCommandItem)
                {
                    if (HttpContext.Current.Session["CurrentCompany"].ToString() != "1")
                    {
                        string sql = "select isMaster, isMasterEmpTemp From Company Where Company_ID=1";
                        //string sql = "select isMaster, isMasterEmpTemp From Company Where Company_ID=" + Utility.ToInteger(Session["Compid"]);
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        if (dr.Read())
                        {
                            string ismaster = dr[0].ToString();
                            string ismastertempemp = dr[1].ToString();
                            if (ismaster == "True" && ismastertempemp == "True")
                            {
                                GridCommandItem commandItem = (GridCommandItem)e.Item;
                                HyperLink button = commandItem.FindControl("HyperLink1") as HyperLink;
                                button.Text = "Add New Temporary Employee";
                            }
                        }
                   } 
                }
            }
            else
            {
                if (e.Item is GridCommandItem)
                {
                    GridCommandItem commandItem = (GridCommandItem)e.Item;
                    HyperLink button = commandItem.FindControl("HyperLink1") as HyperLink;
                    button.Visible = false;
                }
            }
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Edit Employee")) == false)
            {
                RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
                RadGrid1.ClientSettings.EnablePostBackOnRowClick = false;
            }
            if (Request.QueryString.AllKeys.Length > 0)
            {
                if (Request.QueryString["msg"].ToString() == "Employee Information Updated Successfully.")
                {
                   // lblMessage.InnerHtml = "Employee Information Updated Successfully.";
                    _actionMessage = "success|Employee Information Updated Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    //ShowMessageBox("Employee Information Updated Successfully.");
                }
            }
            else
            {
                lblMessage.InnerHtml = "";
            
            }

            if (e.Item is GridEditableItem)
            {
                if (AddNewUser() == true)
                {
                    //hypLink.Visible = true;
                    //lblMessage.Visible = false;
                    
                }
                else
                {
                    //hypLink.Visible = false;
                    //lblMessage.Visible = true;            
                }
            }

            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //RadGrid1.ExportSettings.ExportOnlyData = true;
            //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
            //RadGrid1.ExportSettings.OpenInNewWindow = true;


            //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;

            ConfigureExport();
            RadGrid1.MasterTableView.ExportToExcel();
        }
        public void ConfigureExport()
        {
            //To ignore Paging,Exporting only data,
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            RadGrid1.MasterTableView.AllowFilteringByColumn = false;


            //To hide the add new button
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Column to hide
            //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;


            //RadGrid1.ExportSettings.ExportOnlyData = true;
            //RadGrid1.ExportSettings.IgnorePaging = CheckBox1.Checked;
            //RadGrid1.ExportSettings.OpenInNewWindow = true;

            ConfigureExport();

            RadGrid1.MasterTableView.ExportToWord(); 

        }

        protected void btnshowall_Click(object sender, EventArgs e)
        {
            //SqlParameter[] parms1 = new SqlParameter[3];
            //parms1[0] = new SqlParameter("@company_id", varCompid);
            //parms1[1] = new SqlParameter("@show", "1");
            //parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"])); 
            
            //string sSQL = "sp_GetEmployees";
            //try
            //{
            //    DataSet ds = new DataSet();
            //    ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
            //    s = 1;
            //    Session["s"] = s;
            //    //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //    //ds = GetDataSet(sSQL);
            //    RadGrid1.DataSource = ds;
            //    RadGrid1.DataBind();
            //    btnshowall.Enabled = false;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            
            if (Utility.ToInteger(Session["s"]) == 1)
            {
                //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
                //ds = GetDataSet(sSQL);
                RadGrid1.CurrentPageIndex = e.NewPageIndex;
                showemployees(drpShowAll.SelectedItem.Value.ToString());
            }
        }
        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }


        protected void btnallemp_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string ssqlexcel = "sp_empall_details";
            SqlParameter[] parms = new SqlParameter[1];
            int i = 0;
            parms[i] = new SqlParameter("@companyid", Utility.ToInteger(varCompid));
            ds = DataAccess.FetchRS(CommandType.StoredProcedure, ssqlexcel, parms);
            Employee em = new Employee();
            em.ExportToExcel(ds, 0, Response, "EmployeeInfo");

        }



        protected void drpShowAll_SelectedIndexChanged(object sender, EventArgs e)
        {
            showemployees(drpShowAll.SelectedItem.Value.ToString());
        }

        void showemployees(string strshow)
        {
            btnsubapprove.Visible = false;
            RadGrid1.Columns[13].Visible = false;

            SqlParameter[] parms1 = new SqlParameter[3];
            parms1[0] = new SqlParameter("@company_id", varCompid);
            parms1[1] = new SqlParameter("@show", Convert.ToInt16(strshow));
            parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));

            string sSQL = "sp_GetEmployees";
            try
            {
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                s = 1;
                Session["s"] = s;
                //RadGrid1.CurrentPageIndex = 0;
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }
            catch (Exception ex)
            {
            }
            if (strshow == "4")
            {
                if (HttpContext.Current.Session["CurrentCompany"].ToString() == "1")
                {
                    if (Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN")
                    {
                        RadGrid1.Columns[13].Visible = true;
                        btnsubapprove.Visible = true;
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            showemployees(drpShowAll.SelectedItem.Value.ToString());
            //if (Session["Compid"] == null)
            //{
            //}
            //else
            //{
            //    if (HttpContext.Current.Session["CurrentCompany"].ToString() == "1")
            //    {
            //        drpCompany.SelectedValue = Session["Compid"].ToString();
            //    }
            //}
        }

        protected void btnsubapprove_click(object sender, EventArgs e)
        {
            bool blnisrecsel = false;
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        try
                        {
                            string ssqlb = "Update Employee Set StatusID=1 Where Emp_Code=" + empid.ToString();
                            DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        }
                        catch (Exception ex)
                        {
                            string ErrMsg = ex.Message;
                        }
                        blnisrecsel = true;
                    }

                }
            }
            if (blnisrecsel == false)
            {
               // ShowMessageBox("Please Select Employees to Submit Payroll");
                _actionMessage = "Warning|Please Select Employees to Submit Payroll";
                ViewState["actionMessage"] = _actionMessage;
            }

            showemployees(drpShowAll.SelectedItem.Value.ToString());
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


        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
        }
        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }
            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

            if (e.Item.Text == "Graph")
            {
                //var scheme = Request.Url.Scheme; // will get http, https, etc.
                //var host = Request.Url.Host; // will get www.mywebsite.com
                //var port = Request.Url.Port; // will get the port
                //var path = Request.Url.AbsolutePath; //
                //string url = scheme +"://" + host +":"+ port + "/employee/EmployeeChart.aspx?CompID=" + varCompid;

                //Response.Cookies.Clear();
                //HttpCookie cookie = new HttpCookie("CompID");
                //cookie.Value = HttpUtility.HtmlEncode(varCompid.ToString());
                //Response.Cookies.Add(cookie);

                string strServer = System.Configuration.ConfigurationManager.AppSettings["DB_SERVER"].ToString();
                string strServerUser = System.Configuration.ConfigurationManager.AppSettings["DB_UID"];
                string strDB = System.Configuration.ConfigurationManager.AppSettings["DB_NAME"];
                string strDB_PWD = System.Configuration.ConfigurationManager.AppSettings["DB_PWD"];

                String pCompID = HttpUtility.UrlEncode(varCompid.ToString());
                String pServer = Server.UrlEncode(strServer);
                String pServerUser = Server.UrlEncode(strServerUser);
                String pDB = Server.UrlEncode(strDB);
                String pDB_PWD = Server.UrlEncode(strDB_PWD);

                string url = "http://localhost/DashBoradBrowserBased/DashBoradBrowserBased.xbap?Server=" + pServer + "&ServerUserID=" + pServerUser + "&DBName=" + pDB + "&DBPassword=" + pDB_PWD + "&CompID=" + pCompID;
                ///Response.Redirect();
                ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>openNewWin('" + url + "')</script>");

            }
           
           // GridSettingsPersister obj1 = new GridSettingsPersister();
           // obj1.ExportGridHeader("1", HttpContext.Current.Session["CompanyName"].ToString(), HttpContext.Current.Session["Emp_Name"].ToString(), e);
        }

       

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        

     
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }


        #endregion
        //Toolbar End







    }
}
