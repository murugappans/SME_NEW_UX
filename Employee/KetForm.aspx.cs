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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using iTextSharp.text.pdf.fonts;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;


namespace SMEPayroll.Employee
{
    public partial class KetForm : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int s = 0, varCompid;
        protected string sMsg = "";
        int compid;
        SqlConnection conn;
        SqlConnection conn2;
        SqlCommand cmd;
        SqlCommand cmd2, cmd3, cmd4;
        SqlDataReader dr, dr2, dr3, dr4;
        DateTime jd;
        Int32 pp;
        Int32 cid;
        Int32 did;
        string empid;
        string stremp;
        StringBuilder sb;
        Document document;
        PdfWriter writer;
        byte[] bytes;
        MemoryStream memoryStream;
        string _actionMessage = "";

        double ot1rate;
        double ot2rate;
        double hrate;
        double drate;
        double mrate;
        double wrate;
        double frate;
        string pf;
        string opf;
        string sal_del_text;
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
                RadGrid1.ExportSettings.FileName = "KET_EmployeeList";
                   
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                drpShowAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Show Terminated Employee", "2"));
                drpShowAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Show All Employee", "3"));

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
                            drpShowAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Show Employee on Hold", "4"));
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
                            drpShowAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Show Employee on Hold", "4"));
                        }
                    }
                }
                drpShowAll.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Show Active Employee", "1"));
                SqlDataSource1.ConnectionString = Session["ConString"].ToString();
                Session["s"] = 0;
                bindgrid();
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
            objCount.RowCount(e, tbRecord);
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

            if (strNegative == "-")
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
                Response.Redirect("../Reports/Ket_Form.aspx?empcode=" + dataItem["EmpCode"].Text);
            }

            //Export
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
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
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This superadmin employee record cannot be deleted. "));
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
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
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
           // RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
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
            KetForm  em = new KetForm ();
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
           // RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
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



        protected void PrintKet_Click(object sender, EventArgs e)
        {
            
           stremp = "";
            bool flag = false;
            string empId = "";
            sb = new StringBuilder();
            document = new Document(PageSize.A4, 88f, 88f, 15f, 15f);
            memoryStream = new System.IO.MemoryStream();
            writer = PdfWriter.GetInstance(document, memoryStream);
            document.Open();
            
            
            //HTMLWorker htmlparser = new HTMLWorker(pdfdoc);
            foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn2"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        stremp = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        Print_multiemployee(stremp);
                        document.Add(Chunk.NEXTPAGE);
                        
                        flag = true;
                    }
                }
            }

            if (flag == false)
            {
                // lblMessage.Text = "Please Select At least one employee";

            }
            else
            {
                //----------------------------
                //StringReader sr = new StringReader(sb.ToString());
                //Document pdfdoc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfdoc);

                //--------------------------


                //PdfWriter pdfw = PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
                //pdfdoc.Open();
                //htmlparser.Parse(sr);
                //htmlparser.NewPage();
                //htmlparser.Parse(sr);

                document.Close();
                bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Ket.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();

               
            }
            
           
        }
        protected void PrintKet_print()
        {
            string Designation = "";
            string AnnualLeave = "";
            string SickLeave = "";
            string HospLeave = "";


            string txtSalary = "";
            string txtOTrate = "";
            string txtWorkDays = "";
            string wdays_per_week = "";
            Double rday = 0.0;
            string dayDatails = "";
            string txtOtot = "";
            string txtRestDay = "";

            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd4 = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr4 = cmd4.ExecuteReader();
            if (dr4.Read())
            {
                Designation = dr4["Designation"].ToString();

            }
            else
            {
                Designation = "NIL";
            }

            dr4.Close();
            
            cmd = new SqlCommand("select employee.desig_id,employee.emp_name,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.Company_name,company.Company_id from employee,company where emp_code=" + stremp  + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            dr.Read();

                       


            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                     
                    
                 
                    //---------------------------------A
                    sb.Append("<table border='1' style='page-break-before: always; page-break-after: always;'  >");
                    sb.Append("<th colspan ='2' bgcolor='gray' >Section A | Details of Employment</th></tr>");
                    sb.Append("<tr><td style='width: 489px'>Company Name<br />" + dr["Company_name"].ToString());
                    sb.Append("<td style='width: 462px'>Job Title,Main Duties and Responsibilitites<br />" + Designation + "</tr>");
                    sb.Append("<tr><td style='width: 489px'>Employee Name<br /><br />" + dr["emp_name"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Full-Time Employment<br /></td></tr>");
                    sb.Append("<tr><td style='width: 489px'>Employee NRIC/FIN<br />" + dr["ic_pp_number"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Duration of Employment<br />" + "period" + "</dr></tr>");
                    sb.Append("<tr><td style='width: 489px'>Employment Start Date<br />" + dr["joining_date"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Place of Work<br />" + dr["Address"].ToString() + "</td></tr></table>");

                    //-----------------------------------B
                    sb.Append("<table border='1'>");
                    sb.Append("<th colspan ='2' bgcolor='gray' >Section B | Working Hours and Rest Days</th></tr>");
                    sb.Append("<tr><td rowspan='2' style='width: 489px'>Details of Working Hours<br />" + "---------" + "</dr>");
                    sb.Append("<td style='width: 462px'>Number of Working Days Per Week <br />" + txtWorkDays + " days per week</td></tr>");
                    sb.AppendLine ("<tr><td style='width: 489px'>Rest Day Per Week<br /><input type=\"checkbox\" Nname= \"CheckDel\"   /><br />" + rday.ToString() + " day(s) per week(" + dayDatails + ")</td></tr></table>");
                   // sb.Append("<input type=\"checkbox\" Nname= \"CheckDel\"   />");
                    
                    //-----------------------------------C
                    //sb.Append("<table border='1'>");
                    //sb.Append("<th colspan ='2' bgcolor='gray' >Section C | Salary</th></tr>");
                    //sb.Append("<tr><td rowspan='2' style='width: 489px'>Salary Period  1st to 31st<br /><input name='hr' type='radio' checked />Hourly</td>");

                  //  sb.Append("<td style='width: 462px'>Date(s) of Salary Payment<br />" + ddSalarydate.SelectedValue + " of every calendar month</td></tr>");
                   // sb.Append("<tr><td style='width: 489px'>Date(s) of Overtime Payment<br />" + ddOTdate.SelectedValue + " of every calendar month</td></tr></table>");




                    ////----------------------------
                    //StringReader sr = new StringReader(sb.ToString());
                    //Document pdfdoc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                    //HTMLWorker htmlparser = new HTMLWorker(pdfdoc);

                    ////--------------------------


                    //PdfWriter pdfw = PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
                    //pdfdoc.Open();
                    //htmlparser.Parse(sr);
                    //htmlparser.NewPage();
                    //htmlparser.Parse(sr);
                    //pdfdoc.Close();


                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=" +stremp  + ".pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfdoc);

                   
                    //  System.Diagnostics.Process.Start("C:\\Users\\sangu\\Downloads\\" + ono + ".pdf");
                    // Response.End();
                }
            }

        }
        protected void Print_multiemployee( string emp_code)
        {
           

            
            //Create new PDF document
            string txtDesignation = "";
            string txtAnnualLeave = "";
            string txtSickLeave = "";
            string txtHospLeave = "";


            string txtSalary = "";
            string txtOTrate = "";
            string txtWorkDays = "";
            string wdays_per_week = "";
            Double rday = 0.0;
            string dayDatails = "";
            string txtOtot = "";
            string txtRestDay = "";
            string txtComponents = "";
            string txtOT1 = "";

            string txtSalarydate = "";
            string txtOTdate = "";
            string txtOtherMedical_benefit = "";
            string txtOtherTypeOfLeave = "";
            string txtdet_working_hrs = "";
            string txtJobTittle = "";
            string txtAllowancetypes = "";
            string txtcomponents = "";
            double atotal = 0.0;
            double dtotal = 0.0;
            string paid_mediacal_exam_fee = "";
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd4 = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr4 = cmd4.ExecuteReader();
            if (dr4.Read())
            {
                txtDesignation = dr4["Designation"].ToString();

            }
            else
            {
                txtDesignation = "NIL";
            }

            dr4.Close();
            // ---------------------------------------------------------
            SqlCommand cmd2 = new SqlCommand("select leave_type,leaves_allowed from employeeleavesallowed where (leave_type = 8 or leave_type = 9 or leave_type=13) and emp_id=" + emp_code  + " and leave_year=2016", conn);
            dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {

                if (Convert.ToInt32(dr2["leave_type"]) == 8)
                {
                    txtAnnualLeave = dr2["leaves_allowed"].ToString();
                }
                else if (Convert.ToInt32(dr2["leave_type"]) == 9)
                {
                    txtSickLeave = dr2["leaves_allowed"].ToString();
                }
                else if (Convert.ToInt32(dr2["leave_type"]) == 13)
                {
                    txtHospLeave = dr2["leaves_allowed"].ToString();
                }
            }

            //---------------------------------------------------------
            dr2.Close();
            string txtCPFpayabl = "";
           // string sqlstr="select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,OT2Rate,CPF_Entitlement,Daily_Rate,Hourly_Rate from EmployeePayHistory where Emp_ID=" + Session["emp_code"].ToString();
            SqlCommand cmd5 = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,OT2Rate,CPF_Entitlement,Hourly_Rate,Daily_Rate from EmployeePayHistory where Emp_ID=" + emp_code, conn);
           // SqlCommand cmd5 = new SqlCommand(sqlstr , conn);
            SqlDataReader dr3 = cmd5.ExecuteReader();
            while (dr3.Read())
            {
                ot1rate = Convert.ToDouble(dr3["OT1Rate"]);
                ot2rate = Convert.ToDouble(dr3["OT2Rate"]);
                hrate = Convert.ToDouble(dr3["Hourly_Rate"]);
                drate = Convert.ToDouble(dr3["Daily_Rate"]);
                mrate = Convert.ToDouble(dr3["payrate"].ToString().Trim().Length  == 0 ? 0 : dr3["payrate"]);

                txtSalary = dr3["payrate"].ToString();
                double d = ot1rate * Convert.ToDouble(dr3["Hourly_Rate"]);
                txtOTrate = d.ToString("#0.00");
                txtWorkDays = dr3["wdays_per_week"].ToString();
                wdays_per_week = dr3["wdays_per_week"].ToString();
                rday = 7 - Convert.ToDouble(wdays_per_week);
                
                txtOT1 = dr3["OT1Rate"].ToString();
                if (rday == 2)
                {
                    dayDatails = "Saturday & Sunday";
                }
                else if (rday == 1.5)
                {
                    dayDatails = "1/2day Saturday & Sunday";
                }
                else
                {
                    dayDatails = "Sunday";
                }
                txtRestDay = rday.ToString();

                if (dr3["CPF_Entitlement"].ToString() == "Y")
                {
                    txtCPFpayabl = "Y";
                }
                else
                {
                    txtCPFpayabl = "N";
                }
            }

            dr3.Close();

            //---------------------------
            cmd = new SqlCommand("select employee.pay_frequency,employee.desig_id,employee.emp_name,employee.emp_lname,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.address2,company.postal_code,company.Company_name,company.Company_id from employee,company where emp_code=" + emp_code + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            pf = dr["pay_frequency"].ToString().Trim();
            opf = dr["pay_frequency"].ToString().Trim();

            string emp_name = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
            string ic_pp_number = dr["ic_pp_number"].ToString();
            string joining_date = dr["joining_date"].ToString ().Substring(0,10);
            string Address = dr["Address"].ToString();
            string Address2 = dr["address2"].ToString();
            string Address3 ="SINGAPORE "+ dr["postal_code"].ToString();
            string Company_name = dr["Company_name"].ToString();
            string pedate = "";
            jd = Convert.ToDateTime(joining_date);
            pp = Convert.ToInt32(dr["probation_period"]);
            if (pp <= 0)
            {
                pp = 0;
                pedate = "";
            }
            else
            {
                pedate = jd.AddMonths(pp).ToShortDateString();
            }
            string plength = pp.ToString();
            string psdate = dr["joining_date"].ToString().Substring(0, 10);
            jd = Convert.ToDateTime(dr["joining_date"]);
           
           // string job_duties = dr["job_duties"].ToString();
            string duration_end_date = "";
            dr.Close();
            //------------check in Ket_Manual, record exists or not
            dr3.Close();
            cmd = new SqlCommand("select * from Ket_Manual where emp_code=" + emp_code , conn);
            dr3 = cmd.ExecuteReader();
            int titleid = 0;
            int catid = 0;
            string title = "";
            string txtnoticeperiod = "";
            string idate = "";
            if (dr3.Read())
            {
                DateTime dt = Convert.ToDateTime(dr3["issue_date"]);
                idate = dt.ToString("dd/MM/yyyy");
                pf = dr3["salary_period"].ToString();
               // printbutton.Enabled = true;
                titleid = Convert.ToInt32(dr3["job_title_id"]);
                txtComponents = dr3["osrc_amount"].ToString();
                duration_end_date = dr3["duration_end_date"].ToString();
                txtOtherTypeOfLeave = dr3["other_type_of_leaves"].ToString();
                duration_end_date = duration_end_date.Trim();

                txtnoticeperiod = dr3["notice_period"].ToString();

                if (duration_end_date  == "NA" && duration_end_date=="")
                {
                    duration_end_date = "Continue";
                }

                if (duration_end_date == "NA" && duration_end_date == "")
                {
                    duration_end_date = "Continue";
                }

                if (pf == "W")
                {
                    txtSalary = dr3["payrate"].ToString();
                    sal_del_text = "( per Weekly)";

                }
                else if (pf == "F")
                {
                    txtSalary = dr3["payrate"].ToString();
                    sal_del_text = "( per Fortnightly)";
                }
                else if (pf == "H")
                {
                    txtSalary = hrate.ToString();
                    sal_del_text = "( per Hourly)";
                }
                else if (pf == "D")
                {
                    txtSalary = drate.ToString();
                    sal_del_text = "( per Daily)";
                }
                else if (pf == "M")
                {
                    txtSalary = mrate.ToString();
                    sal_del_text = "( per Monthly)";
                }

                opf = dr3["ot_pay_period"].ToString();
                txtSalarydate = dr3["date_of_payment"].ToString();
                txtOTdate = dr3["date_of_ot"].ToString();
                txtOtherMedical_benefit = dr3["other_medical_benefit"].ToString();
                txtdet_working_hrs = dr3["working_hrs_details"].ToString();
                

                if (dr3["job_duties"].ToString() == "NIL")
                {
                    txtJobTittle = "";
                }
                else
                {
                    txtJobTittle = dr3["job_duties"].ToString();
                }
                if ((dr3["other_salary_related_comp"].ToString() == "0"))
                {

                }
                else
                {
                    txtAllowancetypes = dr3["other_salary_related_comp"].ToString();
                    txtcomponents = dr3["other_salary_related_comp"].ToString();
                }
                if ((dr3["paid_medical_exam_fee"].ToString() == "Y"))  //murugan17032017
                {
                    //txtPaidmefee.Checked = true;
                    paid_mediacal_exam_fee = "Y";
                }
                else
                {
                    //txtPaidmefee.Checked = false;
                    paid_mediacal_exam_fee = "N";
                }
            }
            else
            {

                //rMonthly.Checked = true;
                //ddSalarydate.SelectedValue = "1";
                // ddOTdate.SelectedValue = "1";
                // txtOtherMedical_benefits.Value = "NIL";
                //txtdet_working_hrs.Text = "";

            }
            //------- find job category
            dr3.Close();
            if (titleid == 0)
            {
               
            }
            else
            {
                cmd = new SqlCommand("select * from  JobTitle where id=" + titleid, conn);
                dr = cmd.ExecuteReader();
                string cname = "";
                if (dr.Read())
                {
                    catid = Convert.ToInt32(dr["cat_id"]);
                    cname = dr["cat_title"].ToString();
                                   
                }
                dr.Close();
                SqlCommand sc = new SqlCommand("select * from JobCategory where cat_id=" + catid, conn);
                SqlDataReader sdr = sc.ExecuteReader();
                if (sdr.Read())
                {
                    txtJobTittle = sdr["cat_name"].ToString() + "," + cname + "\n" + txtJobTittle;
                }
                
                sdr.Close();
            }
            
            
            //------------------------------
            SqlCommand cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + emp_code , conn);
            SqlDataReader  dr11 = cmd11.ExecuteReader();
            PdfPTable all_table=new PdfPTable(6);
            PdfPCell  acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            all_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Allowance($)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            all_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {
                
                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE )));
                acell.Colspan = 4;
                all_table.AddCell(acell);
                 acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE )));
                 acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                 acell.Colspan = 2;
                all_table.AddCell(acell);
                atotal = atotal + Convert .ToDouble ( dr11["amount"].ToString ());
            }
            acell = new PdfPCell(new Phrase("Total Fixed\nAllowance", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE )));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY  ;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
           
            all_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(atotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY  ;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT ;
            all_table.AddCell(acell);

            dr11.Close ();
            //-----------------------------------------------------------------

            cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + emp_code , conn);
            dr11 = cmd11.ExecuteReader();
            PdfPTable  ded_table = new PdfPTable(6);
            acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Deduction($)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {
                
                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.Colspan = 4;
                ded_table.AddCell(acell);
                acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                acell.Colspan = 2;
                ded_table.AddCell(acell);
                dtotal = dtotal + Convert .ToDouble ( dr11["amount"].ToString());
            }
            acell = new PdfPCell(new Phrase("Total Fixed\nDeduction", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY ;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(dtotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY ;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT ;
            ded_table.AddCell(acell);
            dr11.Close();
            
            
            //-----------------------------------
            
            cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='O' and  emp_code=" + emp_code , conn);
            dr11 = cmd11.ExecuteReader();
            PdfPTable other_table = new PdfPTable(6);
            acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Allowance($)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {

                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.Colspan = 4;
                other_table.AddCell(acell);
                acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                acell.Colspan = 2;
                other_table.AddCell(acell);
                dtotal = dtotal + Convert.ToDouble(dr11["amount"].ToString());
            }
            acell = new PdfPCell(new Phrase("Total Salary Related\nComponents", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(dtotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            other_table.AddCell(acell);
            dr11.Close();
            //-----------------------------------

            //Document document = new Document(PageSize.A4, 88f, 88f, 15f, 15f);

           // using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
           // {
               // PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                PdfPTable table2 = null;
                PdfPTable table3 = null;
                //-- set for heading

                //--set for sub headding

                //--set for details


              //  document.Open();
                //---------create image cellq
                table2 = new PdfPTable(12);
                iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Full-Time Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 11;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                //---------create image cell
                table3 = new PdfPTable(12);
               iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic2, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("Part-Time Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 11;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table3.AddCell(cell);

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
            //------------------
                PdfPTable head_table1 = new PdfPTable(5);

                PdfPTable head_table = new PdfPTable(1);

                acell = new PdfPCell(new Phrase("Key Employment Terms", FontFactory.GetFont("Franklin Gothic Medium", 25, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(new Phrase("All fields are mandatory, unless they are not applicable", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(head_table);
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 0;
                acell.Colspan = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table1.AddCell(acell);


                head_table = new PdfPTable(2);

                acell = new PdfPCell(new Phrase("Issued on:", FontFactory.GetFont("Franklin Gothic Medium", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(new Phrase(idate , FontFactory.GetFont("Franklin Gothic Medium", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);


                acell = new PdfPCell(new Phrase("All information accurate as of Issuance date", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.Colspan = 2;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);



                acell = new PdfPCell(head_table);
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.Colspan = 2;
                acell.BorderWidth = 1;
                acell.BorderColor = iTextSharp.text.BaseColor.GRAY;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table1.AddCell(acell);

                PdfPCell head_cell = new PdfPCell(head_table1);
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                head_cell.Padding = 3;
                head_cell.BorderWidth = 0;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);



            //----------------
                //----------------------top heading
                //PdfPCell head_cell = new PdfPCell(new Phrase("Key Employment Terms", FontFactory.GetFont("Franklin Gothic Medium", 18, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                //head_cell.Colspan = 2;
                //head_cell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                //head_cell.BorderWidth = 0;
                //head_cell.Padding = 3;
                //head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //table.AddCell(head_cell);
                //section A
                //cell = new PdfPCell(new Phrase("Section A | Details of Employment",head_font ));

                head_cell = new PdfPCell(new Phrase("Section A | Details of Employment", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                PdfPCell subhead_cell = new PdfPCell(new Phrase("Company Name", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Job Title, Main Duties and Responsibilities", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                PdfPCell del_cell = new PdfPCell(new Phrase(Company_name, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase(txtJobTittle , FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;

                // del_cell.BorderColorTop = iTextSharp.text.BaseColor.LIGHT_GRAY ;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Employee Name", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                table.AddCell(table2);


                del_cell = new PdfPCell(new Phrase(emp_name, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                table.AddCell(table3);

                subhead_cell = new PdfPCell(new Phrase("Employee NRIC/FIN", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);
                subhead_cell = new PdfPCell(new Phrase("Duration of Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase(ic_pp_number, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase(joining_date+"-"+duration_end_date , FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Employment Start Date", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);
                subhead_cell = new PdfPCell(new Phrase("Place of Work", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase(joining_date, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase(Address + "\n" + Address2 + "\n" + Address3, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //section B-----------------------------------------------------

                head_cell = new PdfPCell(new Phrase("Section B | Working Hours and Rest Days", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Details of Working Hours", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Number of Working Days Per Week", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(txtdet_working_hrs.ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.Rowspan = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase(txtWorkDays + " days per week", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Rest Day Per Week", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(rday + " day per week (" + dayDatails + ") ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                //---------------------------------------------------------------------------------------------------
                //section C
                head_cell = new PdfPCell(new Phrase("Section C | Salary", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Salary Period   1st to 31st", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                subhead_cell = new PdfPCell(new Phrase("Date(s) of Salary Payment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //-----------salary period
                //---------create image cellq
                table2 = new PdfPTable(12);
                //pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                if (pf == "H")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }


                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Hourly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);
                if (pf == "D")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Daily", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);
                if (pf == "W")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Weekly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);
                if (pf == "F")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fortnightly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (pf == "M")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monthly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                subhead_cell = new PdfPCell(table2);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.Rowspan = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;


                table.AddCell(subhead_cell);
                //-----------------------

                del_cell = new PdfPCell(new Phrase(txtSalarydate + " of every calendar month", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Date(s) of Overtime Payment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(txtOTdate + " of every calendar month", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                //--------------------------------------------------
                subhead_cell = new PdfPCell(new Phrase("Overtime Payment Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                subhead_cell = new PdfPCell(new Phrase("Basic Salary (Per Period)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase("(only if different from salary period)", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                // del_cell.Rowspan = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.BorderWidthBottom = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase("$" + txtSalary + sal_del_text, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);
                //-------overtime check boxes
                table2 = new PdfPTable(12);
                //pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                if (opf == "H")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Hourly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "D")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Daily", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "W")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Weekly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "F")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

              // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fortnightly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "M")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monthly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                subhead_cell = new PdfPCell(table2);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.Rowspan = 2;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;


                table.AddCell(subhead_cell);
                //-----------------------


                subhead_cell = new PdfPCell(new Phrase("Overtime Rate of Pay", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase("$"+txtOTrate+" (per hour)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                subhead_cell = new PdfPCell(new Phrase("Fixed Allowances Per Salary Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                               
                subhead_cell = new PdfPCell(new Phrase("Fixed Deductions Per Salary Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                subhead_cell = new PdfPCell(all_table);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //------------------------allowance table
                subhead_cell = new PdfPCell(ded_table);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //----------------------

                subhead_cell = new PdfPCell(new Phrase("Other Salary-Related Components", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //---------create image cellq
                table2 = new PdfPTable(11);

                if (txtCPFpayabl == "Y")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("CPF Contributions Payable", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);

                del_cell = new PdfPCell(other_table);
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase("(subject to prevailing CPF contribution rates)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //section D-----------------------------------------------------------
                document.Add(table);
                document.Add(new Phrase(Chunk.NEXTPAGE));

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;

                head_cell = new PdfPCell(new Phrase("Section D | Leave and Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Types of Leave", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Other Types of Leave", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                del_cell = new PdfPCell(new Phrase("(applicable if service is at least 3 months)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase("(e.g. Paid Maternity Leave)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Paid Annual Leave \nPer Year                         :" + txtAnnualLeave + " (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);
                table.AddCell(table2);

                del_cell = new PdfPCell(new Phrase(txtOtherTypeOfLeave, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE )));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.Rowspan = 2;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase("(for 1st year of service)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //----------------
                table2 = new PdfPTable(11);
                 pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Paid Outpatient Sick \nLeave Per Year              :" + txtSickLeave + " (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);
                //-----------------
                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Paid Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);

                //-----------
                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

               
                cell = new PdfPCell(new Phrase("Paid Hospitalisation \nLeave Per Year              :" + txtHospLeave+" (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);
                //--------------------------------
                subhead_cell = new PdfPCell(new Phrase("Other Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.BorderColorBottom = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                subhead_cell = new PdfPCell(new Phrase("(Note that paid hospitalisation per year is inclusive of paid outpatient sick leave. Leave entitlement for part-time employees may be pro-rated based on hours.)", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

               // subhead_cell = new PdfPCell(new Phrase("(optional,to specify)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY )));
                subhead_cell = new PdfPCell(new Phrase(txtOtherMedical_benefit, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.BorderColorTop  = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //section E-----------------------------------------------------------

               // Phrase page = new Phrase(Chunk.NEWPAGE);
                
                head_cell = new PdfPCell(new Phrase("Section E | Others", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Length of Probation : "+plength +" month(s)\n\nProbation Start Date :"+psdate +"\n\nProbation End Date :"+pedate , FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 1;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


               Phrase  p=new Phrase("Notice Period for Termination of \nEmployment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK));
                subhead_cell.AddElement (p);
                p = new Phrase("(Initiated by either party whereby the \nlength shall be the same)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY ));
                subhead_cell.AddElement(p);
                p = new Phrase("1 month notice or 1 month salary\nin lieu of Notice", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE ));
                subhead_cell.AddElement(p);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 1;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

               

                document.Add(table);


                //Separater Line
                // color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                // DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, Color.Black );
                // DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, Color.Black );
                // document.Add(table);

               // document.Close();

               // bytes = memoryStream.ToArray();
              //  memoryStream.Close();
                //Response.Clear();
                //Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                //Response.ContentType = "application/pdf";
                //Response.Buffer = true;
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(bytes);
                //Response.End();
                //Response.Close();


           // }


        
        }
    }
   
}
