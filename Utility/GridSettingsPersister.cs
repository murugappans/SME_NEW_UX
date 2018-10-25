using System;
using System.Collections.Generic;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace SMEPayroll
{
    /// <summary>
    /// Imports and exports settings from a RadGrid instance.
    /// </summary>
    public  class GridSettingsPersister
    {
        #region Constructor
        public GridSettingsPersister()
        {

        }
        #endregion
       
        #region Custom code -Ram

            //Row count in Tootlbar
            public void RowCount(GridItemEventArgs e, RadToolBar ToolBarName)
            {
                if (e.Item is GridPagerItem)
                {
                    string item;
                    int count;
                    GridPagerItem pager = (GridPagerItem)e.Item;
                    item = pager.Paging.DataSourceCount.ToString();
                    count = pager.Paging.DataSourceCount;

                    //Finding the control inside the toolbar
                    Label label = (Label)ToolBarName.FindItemByText("Count").FindControl("Label_count");
                    label.Text = "Count : " + count.ToString();

                }
            }

            //Toolbar each button click
            public void ToolbarButtonClick(Telerik.Web.UI.RadToolBarEventArgs e, RadGrid GridId, string UserID)
            {
            //determine which button was clicked

            if (e.Item.Text == "Add")
            {
                GridId.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {
                ConfigureExport(GridId);
                //GridId.MasterTableView.ExportToExcel();
                GridId.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport(GridId);
                GridId.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport(GridId);
                GridId.ClientSettings.Scrolling.UseStaticHeaders = false;
                GridId.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((GridId.Items[0].Cells.Count * 24)) + "mm");
                GridId.ExportSettings.OpenInNewWindow = true;
                //RadGrid1.ExportSettings.ExportOnlyData = false; 
                GridId.MasterTableView.ExportToPdf();


            }
            else if (e.Item.Text == "UnGroup")
            {
                GridId.MasterTableView.GroupByExpressions.Clear();
                GridId.Rebind();
            }
            else if (e.Item.Text == "Clear Sorting")
            {
                GridId.MasterTableView.SortExpressions.Clear();
                GridId.Rebind();
            }
            else if (e.Item.Text == "Save Grid Changes")
                {

                    GridSettingsPersister SavePersister = new GridSettingsPersister(GridId);
                    string userpreference = SavePersister.SaveSettings();


                    //if username already exist then update
                    if (UserID != "")
                    {
                        string User_Grid_Page = UserID + "_RadGrid1_" + GridSettingsPersister.GetCurrentPageName();

                        string sSQLUser_Grid_Page = "select * from Grid_Persisting_Setting where Username_Grid_Page='" + User_Grid_Page + "'";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLUser_Grid_Page, null);
                      
                        //
                        if (dr.HasRows)
                        {
                            string ssqlb = "delete from [Grid_Persisting_Setting] where Username_Grid_Page='" + User_Grid_Page + "'";
                            DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        }
                        string conn=HttpContext.Current.Session["ConString"].ToString();
                        DataTable perDt = new DataTable();
                        perDt = CreateDataTableFromFile(User_Grid_Page, userpreference);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                        {
                            bulkCopy.DestinationTableName = "dbo.Grid_Persisting_Setting";
                            bulkCopy.BulkCopyTimeout = 1000000;
                            try
                            {
                                //// Set up the column mappings by name.
                                SqlBulkCopyColumnMapping col1 = new SqlBulkCopyColumnMapping("Username_Grid_Page", "Username_Grid_Page");
                                bulkCopy.ColumnMappings.Add(col1);

                                SqlBulkCopyColumnMapping col2 = new SqlBulkCopyColumnMapping("PersistingGridSettings", "PersistingGridSettings");
                                bulkCopy.ColumnMappings.Add(col2);

                                SqlBulkCopyColumnMapping col3 = new SqlBulkCopyColumnMapping("Date", "Date");
                                bulkCopy.ColumnMappings.Add(col3);

                                bulkCopy.WriteToServer(perDt);

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        //

                    }
                }
                else if (e.Item.Text == "Reset to Default")
                {
                    string User_Grid_Page1 = UserID + "_RadGrid1_" + GridSettingsPersister.GetCurrentPageName();
                    string sSQLUser_Grid_Page1 = "select * from Grid_Persisting_Setting where Username_Grid_Page='" + User_Grid_Page1 + "'";
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sSQLUser_Grid_Page1, null);
                    if (dr1.HasRows)
                    {
                        //delete the record in the database and reset to default
                        string sSQLUser_Grid_Page2 = "delete from [Grid_Persisting_Setting] where [Username_Grid_Page]='" + User_Grid_Page1 + "'";
                        DataAccess.FetchRS(CommandType.Text, sSQLUser_Grid_Page2, null);
                        //Server.Transfer("Employee.aspx");
                    }

                    GridId.MasterTableView.GroupByExpressions.Clear();
                    GridId.MasterTableView.SortExpressions.Clear();
                    GridId.MasterTableView.Rebind();
                    GridId.Rebind();

                    //Server.Transfer("Employee.aspx");
                   // Server.Transfer(GridSettingsPersister.GetCurrentPageName());
                    System.Web.HttpContext.Current.Response.Redirect(GridSettingsPersister.GetCurrentPageName());

                }
            }

        public void ToolbarButtonClick_Rpt(Telerik.Web.UI.RadToolBarEventArgs e, RadGrid GridId, string UserID, int fontsize)
        {
            //determine which button was clicked

            if (e.Item.Text == "Add")
            {
                GridId.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {
                ConfigureExport_Rpt(GridId, fontsize);
                GridId.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport_Rpt(GridId, fontsize);
                GridId.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport_Rpt(GridId, fontsize);
                GridId.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((GridId.Items[0].Cells.Count * 24)) + "mm");
                GridId.ExportSettings.OpenInNewWindow = true;
                //RadGrid1.ExportSettings.ExportOnlyData = false; 
                GridId.MasterTableView.ExportToPdf();
            }
            else if (e.Item.Text == "UnGroup")
            {
                GridId.MasterTableView.GroupByExpressions.Clear();
                GridId.Rebind();
            }
            else if (e.Item.Text == "Clear Sorting")
            {
                GridId.MasterTableView.SortExpressions.Clear();
                GridId.Rebind();
            }
            else if (e.Item.Text == "Save Grid Changes")
            {

                GridSettingsPersister SavePersister = new GridSettingsPersister(GridId);
                string userpreference = SavePersister.SaveSettings();


                //if username already exist then update
                if (UserID != "")
                {
                    string User_Grid_Page = UserID + "_RadGrid1_" + GridSettingsPersister.GetCurrentPageName();

                    string sSQLUser_Grid_Page = "select * from Grid_Persisting_Setting where Username_Grid_Page='" + User_Grid_Page + "'";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLUser_Grid_Page, null);

                    //
                    if (dr.HasRows)
                    {
                        string ssqlb = "delete from [Grid_Persisting_Setting] where Username_Grid_Page='" + User_Grid_Page + "'";
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    }
                    string conn = HttpContext.Current.Session["ConString"].ToString();
                    DataTable perDt = new DataTable();
                    perDt = CreateDataTableFromFile(User_Grid_Page, userpreference);
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = "dbo.Grid_Persisting_Setting";
                        bulkCopy.BulkCopyTimeout = 1000000;
                        try
                        {
                            //// Set up the column mappings by name.
                            SqlBulkCopyColumnMapping col1 = new SqlBulkCopyColumnMapping("Username_Grid_Page", "Username_Grid_Page");
                            bulkCopy.ColumnMappings.Add(col1);

                            SqlBulkCopyColumnMapping col2 = new SqlBulkCopyColumnMapping("PersistingGridSettings", "PersistingGridSettings");
                            bulkCopy.ColumnMappings.Add(col2);

                            SqlBulkCopyColumnMapping col3 = new SqlBulkCopyColumnMapping("Date", "Date");
                            bulkCopy.ColumnMappings.Add(col3);

                            bulkCopy.WriteToServer(perDt);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    //

                }
            }
            else if (e.Item.Text == "Reset to Default")
            {
                string User_Grid_Page1 = UserID + "_RadGrid1_" + GridSettingsPersister.GetCurrentPageName();
                string sSQLUser_Grid_Page1 = "select * from Grid_Persisting_Setting where Username_Grid_Page='" + User_Grid_Page1 + "'";
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sSQLUser_Grid_Page1, null);
                if (dr1.HasRows)
                {
                    //delete the record in the database and reset to default
                    string sSQLUser_Grid_Page2 = "delete from [Grid_Persisting_Setting] where [Username_Grid_Page]='" + User_Grid_Page1 + "'";
                    DataAccess.FetchRS(CommandType.Text, sSQLUser_Grid_Page2, null);
                    //Server.Transfer("Employee.aspx");
                }

                GridId.MasterTableView.GroupByExpressions.Clear();
                GridId.MasterTableView.SortExpressions.Clear();
                GridId.MasterTableView.Rebind();
                GridId.Rebind();

                //Server.Transfer("Employee.aspx");
                // Server.Transfer(GridSettingsPersister.GetCurrentPageName());
                System.Web.HttpContext.Current.Response.Redirect(GridSettingsPersister.GetCurrentPageName());

            }
        }

            private DataTable CreateDataTableFromFile(string User_Grid_Page, string userpreference)
            {
                DataTable dt = new DataTable();

                //first col
                DataColumn dc = new DataColumn("Username_Grid_Page", typeof(System.String));
                dt.Columns.Add(dc);

                //second col
                DataColumn dc1 = new DataColumn("PersistingGridSettings", typeof(System.String));
                dt.Columns.Add(dc1);

                //Third col
                DataColumn dc2 = new DataColumn("Date", typeof(System.DateTime));
                dt.Columns.Add(dc2);

                //insert the value
                DataRow newRow = dt.NewRow();
                newRow["Username_Grid_Page"] = User_Grid_Page;
                newRow["PersistingGridSettings"] = userpreference;
                newRow["Date"] = System.DateTime.Now;

                dt.Rows.Add(newRow);

                return dt;


            }

            public void ConfigureExport(RadGrid RadGrid1)
            {
                //To ignore Paging,Exporting only data,
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;

                //To hide filter texbox
                RadGrid1.MasterTableView.AllowFilteringByColumn = false;


                //To hide the add new button
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

                RadGrid1.MasterTableView.Font.Name = "Tahoma";
                RadGrid1.MasterTableView.Font.Size = 11;
          
                //Column to hide

            //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;


        }

            public void ConfigureExport_Rpt(RadGrid RadGrid1, int fontsize)
            {
                //To ignore Paging,Exporting only data,
                RadGrid1.ExportSettings.ExportOnlyData = true;
                RadGrid1.ExportSettings.IgnorePaging = true;
                RadGrid1.ExportSettings.OpenInNewWindow = true;

                //To hide filter texbox
                RadGrid1.MasterTableView.AllowFilteringByColumn = false;


                //To hide the add new button
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

                RadGrid1.MasterTableView.Font.Name = "Courier New";
                RadGrid1.MasterTableView.Font.Size = fontsize;
                //Column to hide

                //RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
                //RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
                //RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;


            }

            //Load grid preference-on page load
            public  string username1, userpreference;
            public  void GrabGridSettingsPersister(string Username, RadGrid GridId)
            {
                
                if (Username != "")
                {
                    string User_Grid_Page = Username + "_RadGrid1_" + GridSettingsPersister.GetCurrentPageName();
                    string SQL = "SELECT [Username_Grid_Page],[PersistingGridSettings]FROM [Grid_Persisting_Setting] where [Username_Grid_Page]='" + User_Grid_Page + "'";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                    while (dr.Read())
                    {
                        username1 = Utility.ToString(dr.GetValue(0));
                        userpreference = Utility.ToString(dr.GetValue(1));
                    }
                }

                string user = username1;
                GridSettingsPersister LoadPersister = new GridSettingsPersister(GridId);

                if (username1 == null)
                {

                    // StatusLabel.Text = "No saved settings for this user were found!";
                }
                else
                {
                    string settings = user;
                    LoadPersister.LoadSettings(userpreference);
                    GridId.Rebind();
                    //StatusLabel.Text = "Settings for " + user + " were restored successfully!";
                }
            }

            //Export Grid (excel ) header information
            public  string ReportName, Other, customHTML1, customHTML2, customHTML3,employeename;
            public  bool GenerateBy;
            public  void ExportGridHeader(string GridNo, string CompanyName, string Emp_Name, GridExportingArgs e)
            {

                #region Grab Info from DB
                string sql = "SELECT [GridNo],[ReportName],[Other],[GenerateBy] FROM [GridHeader] where GridNo='" + GridNo.ToString() + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if (dr.Read())
                {
                    ReportName = dr.GetString(dr.GetOrdinal("ReportName"));
                    Other = dr.GetString(dr.GetOrdinal("Other"));
                    GenerateBy = dr.GetBoolean(dr.GetOrdinal("GenerateBy"));
                }
            #endregion
            // var _css = "<link rel=\"stylesheet\" type=\"text/css\" href=\"../Style/general/style-grid.css\" />";BrowalliaUPC
            var _css = "";
            _css = "<style>"
                + "table thead th{background-color:#0a8cf0;text-decoration: none;text-underline-position:under;color:#fff;font-family: Clibri;font-size:11pt;} "
                + "#RadGrid1_ctl00 thead th, #RadGrid1_ctl00 tbody td {border: 1px solid #e4e4e4;} "
                  + "table tbody td  {text-align:left;font-family: Clibri;font-size:11pt;} "
                + "</style>";


            string customHTML = _css+ "<div width=\"100%\" style=\"text-align:center;font-size:8px;font-family:Tahoma;\">" +
                                    " <table width='100%'border='0'>" +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Company Name: </b>" + CompanyName + "</td></tr> " +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name: </b>" + ReportName + "</td></tr> " +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date: </b>" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> ";

                if (Other != "")
                {
                    customHTML1 = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Note: </b>" + Other + "</td></tr> ";
                }
                else
                {
                    customHTML1 = "";
                }

                if (GenerateBy)
                {
                    customHTML2 = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By: </b>" + Emp_Name + "</td></tr> ";
                }
                else
                {
                    customHTML2 = "";
                }


                customHTML3 = "</table>" +
                                    "</div>";
                e.ExportOutput = e.ExportOutput.Replace("<body>", String.Format("<body>{0}", customHTML + customHTML1 + customHTML2 + customHTML3));
            e.ExportOutput = e.ExportOutput.Replace("<a", "<span");
            e.ExportOutput = e.ExportOutput.Replace("</a", "</span");
        }
        public void ExportGridFamilyHeader(string GridNo, string CompanyName, string Emp_Name,string employeecode, GridExportingArgs e)
        {

            #region Grab Info from DB
            string sqlQuery = "SELECT (select isnull(emp_name,'')+' '+isnull(emp_lname,'') from employee where emp_code=em.emp_code)  Full_Name FROM Employee em where em.emp_code=" + Convert.ToInt32(employeecode) + "";
            SqlDataReader drEmp = DataAccess.ExecuteReader(CommandType.Text, sqlQuery, null);
            if (drEmp.Read())
            {
                employeename = drEmp.GetString(drEmp.GetOrdinal("Full_Name"));
               
            }
            string sql = "SELECT [GridNo],[ReportName],[Other],[GenerateBy] FROM [GridHeader] where GridNo='" + GridNo.ToString() + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                ReportName = dr.GetString(dr.GetOrdinal("ReportName"));
                Other = dr.GetString(dr.GetOrdinal("Other"));
                GenerateBy = dr.GetBoolean(dr.GetOrdinal("GenerateBy"));
            }
            #endregion


            string customHTML = "<div width=\"100%\" style=\"text-align:center;font-size:8px;font-family:Tahoma;\">" +
                                " <table width='100%'border='0'>" +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Company Name :</b>" + CompanyName + "</td></tr> " +
                                     "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Employee Name :</b>" + employeename + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b>" + ReportName + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date:</b>" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> ";

            if (Other != "")
            {
                customHTML1 = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Note :</b>" + Other + "</td></tr> ";
            }
            else
            {
                customHTML1 = "";
            }

            if (GenerateBy)
            {
                customHTML2 = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By :</b>" + Emp_Name + "</td></tr> ";
            }
            else
            {
                customHTML2 = "";
            }


            customHTML3 = "</table>" +
                                "</div>";
            e.ExportOutput = e.ExportOutput.Replace("<body>", String.Format("<body>{0}", customHTML + customHTML1 + customHTML2 + customHTML3));
        }

        public void ExportCPFHeader(string CompanyName, string Emp_Name, string month,string year, GridExportingArgs e)
        {

            string customHTML = "<div width=\"100%\" style=\"text-align:center;font-size:8px;font-family:Tahoma;\">" +
                                " <table width='100%'border='0'>" +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Company Name :</b>" + CompanyName + "</td></tr> " +                                  
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b>" + "CPF Report" + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Month:</b>" + month + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Year:</b>" + year + "</td></tr> ";


            if (Emp_Name!="")
            {
                customHTML2 = "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Generated By :</b>" + Emp_Name + "</td></tr> ";
            }
            else
            {
                customHTML2 = "";
            }


            customHTML3 = "</table>" +
                                "</div>";
            e.ExportOutput = e.ExportOutput.Replace("<body>", String.Format("<body>{0}", customHTML + customHTML1 + customHTML2 + customHTML3));
        }
         public static string GetCurrentPageName()
        {
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        public void ExportGridHeader_Rpt(string GridNo, string CompanyName, string Emp_Name, GridExportingArgs e, int fontzise, string reportName, string processPeriod, string Department)
        {

            #region Grab Info from DB
            string sql = "SELECT [GridNo],[ReportName],[Other],[GenerateBy] FROM [GridHeader] where GridNo='" + GridNo.ToString() + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                ReportName = dr.GetString(dr.GetOrdinal("ReportName"));
                Other = dr.GetString(dr.GetOrdinal("Other"));
                GenerateBy = dr.GetBoolean(dr.GetOrdinal("GenerateBy"));
            }
            #endregion


            string customHTML = "<div width=\"100%\" style=\"text-align:center;font-size:" + fontzise + "px;font-family:Courier New;\">" +
                                " <table width='100%'border='0'>" +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" >" + CompanyName + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" >" + reportName + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" >" + processPeriod + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" >" + Department + "</td></tr> " +
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" ><b>Date:</b>" + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr> ";

            if (Other != "")
            {
                customHTML1 = "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" ><b>Note :</b>" + Other + "</td></tr> ";
            }
            else
            {
                customHTML1 = "";
            }

            if (GenerateBy)
            {
                customHTML2 = "<tr><td colspan='7'  style=\"text-align:left;font-size:" + fontzise + "px;font-family:Courier New;\" ><b>Generated By :</b>" + Emp_Name + "</td></tr> ";
            }
            else
            {
                customHTML2 = "";
            }


            customHTML3 = "</table>" +
                                "</div>";
            e.ExportOutput = e.ExportOutput.Replace("<body>", String.Format("<body>{0}", customHTML + customHTML1 + customHTML2 + customHTML3));
        }

        #endregion
      

        #region Code for GridSettingsPersister(Telerik Code)
        
        
        /// <summary>
        /// Initializes an instance of GridSettingsPersister from a RadGrid instance
        /// </summary>
        /// <param name="grid">The RadGrid instance to import and exports settings</param>
        public GridSettingsPersister(RadGrid grid)
            : this(grid, GridSettingsType.All)
        {

        }

        /// <summary>
        /// Initializes an instance of GridSettingsPersister from a RadGrid instance
        /// and a collection GridSettingsType values
        /// </summary>
        /// <param name="grid">The RadGrid instance to import and exports settings</param>
        /// <param name="persistedSettings">
        /// A collection of GridSettingType values specifying the type of grid settings
        /// to import or export
        /// </param>
        public GridSettingsPersister(RadGrid grid, GridSettingsType persistedSettingFlags)
        {
            _grid = grid;
            _persistedSettingTypes = persistedSettingFlags;
            _settings = new GridSettingsCollection();
            _settings.ColumnSettings = new List<ColumnSettings>();
            _settings.AutoGeneratedColumnSettings = new List<ColumnSettings>();
        }

        private RadGrid _grid;
        private GridSettingsType _persistedSettingTypes;
        private GridSettingsCollection _settings;

        /// <summary>
        /// The underlyiong RadGrid instance to import or export settings from
        /// </summary>
        public RadGrid Grid
        {
            get { return _grid; }
        }

        /// <summary>
        /// Gets or sets the GridSettingType flags that specify the grid settings to 
        /// export or import
        /// </summary>
        public virtual GridSettingsType PersistedSettingTypes
        {
            get { return _persistedSettingTypes; }
            set { _persistedSettingTypes = value; }
        }

        protected virtual GridSettingsCollection Settings
        {
            get { return _settings; }
            set { _settings = value; }
        }

        /// <summary>
        /// Saves the current grid settings and returns the settings serilized to string
        /// </summary>
        public virtual string SaveSettings()
        {
            return GetSavedSettings().ToString();
        }

        /// <summary>
        /// Saves the current grid settings and retrieves the underlying
        /// GridSettingsCollection instance that contains the grid settings
        /// </summary>
        public virtual GridSettingsCollection GetSavedSettings()
        {
            if (Grid == null)
            {
                throw new NullReferenceException();
            }

            if (IsSettingSpecified(GridSettingsType.Paging)) SavePagingSettings();
            if (IsSettingSpecified(GridSettingsType.Grouping)) SaveGroupByExpressions();
            if (IsSettingSpecified(GridSettingsType.Sorting)) SaveSortExpressions();
            if (IsSettingSpecified(GridSettingsType.Filtering)) SaveFilterExpression();
            if (IsSettingSpecified(GridSettingsType.ColumnSettings)) SaveColumnSettings();

            return Settings;
        }

        protected bool IsSettingSpecified(GridSettingsType settingType)
        {
            return (PersistedSettingTypes & GridSettingsType.All) == GridSettingsType.All ||
                   (PersistedSettingTypes & settingType) == settingType;
        }

        protected virtual void SavePagingSettings()
        {
            Settings.PageSize = Grid.PageSize;
        }

        protected virtual void SaveGroupByExpressions()
        {
            Settings.GroupByExpressionsStates = new object[Grid.MasterTableView.GroupByExpressions.Count];
            for (int i = 0; i < Settings.GroupByExpressionsStates.Length; i++)
            {
                Settings.GroupByExpressionsStates[i] = ((IStateManager)Grid.MasterTableView.GroupByExpressions[i]).SaveViewState();
            }
        }

        protected virtual void SaveSortExpressions()
        {
            Settings.SortExpressionsState = ((IStateManager)Grid.MasterTableView.SortExpressions).SaveViewState();
        }

        protected virtual void SaveFilterExpression()
        {
            Settings.FilterExpression = Grid.MasterTableView.FilterExpression;
        }

        protected virtual void SaveColumnSettings()
        {
            Settings.ColumnSettings.Clear();
            foreach (GridColumn column in Grid.MasterTableView.Columns)
            {
                Settings.ColumnSettings.Add(GetColumnSettings(column));
            }

            Settings.AutoGeneratedColumnSettings.Clear();
            foreach (GridColumn column in Grid.MasterTableView.AutoGeneratedColumns)
            {
                Settings.AutoGeneratedColumnSettings.Add(GetColumnSettings(column));
            }
        }

        private ColumnSettings GetColumnSettings(GridColumn column)
        {
            ColumnSettings colSettings = new ColumnSettings();
            colSettings.UniqueName = column.UniqueName;
            colSettings.Width = column.HeaderStyle.Width;
            colSettings.Visible = column.Visible;
            colSettings.Display = column.Display;
            colSettings.OrderIndex = column.OrderIndex;
            colSettings.CurrentFilterFunction = column.CurrentFilterFunction;
            colSettings.CurrentFilterValue = column.CurrentFilterValue;

            return colSettings;
        }

        private void SetColumnSettings(ref GridColumn column, ColumnSettings setting)
        {
            column.Display = setting.Display;
            column.Visible = setting.Visible;
            column.HeaderStyle.Width = setting.Width;
            column.OrderIndex = setting.OrderIndex;
            column.CurrentFilterFunction = setting.CurrentFilterFunction;
            column.CurrentFilterValue = setting.CurrentFilterValue;
        }

        /// <summary>
        /// Loads grids settings from a serialized string
        /// </summary>
        /// <param name="value">The string that contains the serialized settings</param>
        public virtual void LoadSettings(string value)
        {
            LoadSettings(GridSettingsCollection.LoadFromSerializedData(value));
        }

        /// <summary>
        /// Loads grids settings from a byte array
        /// </summary>
        /// <param name="data">The byte array that contains the serialized grid settings</param>
        public virtual void LoadSettings(byte[] data)
        {
            LoadSettings(GridSettingsCollection.LoadFromSerializedData(data));
        }

        /// <summary>
        /// Loads grid settings from a GridSettingsCollection instance
        /// </summary>
        /// <param name="settings">The GridSettingsCollection instance to load settings from</param>
        public virtual void LoadSettings(GridSettingsCollection settings)
        {
            if (Grid == null || settings == null)
            {
                throw new NullReferenceException();
            }

            Settings = settings;

            if (IsSettingSpecified(GridSettingsType.Paging)) LoadPagingSettings();
            if (IsSettingSpecified(GridSettingsType.Grouping)) LoadGroupByExpressions();
            if (IsSettingSpecified(GridSettingsType.Sorting)) LoadSortExpressions();
            if (IsSettingSpecified(GridSettingsType.Filtering)) LoadFilterExpression();
            if (IsSettingSpecified(GridSettingsType.ColumnSettings)) LoadColumnSettings();
        }

        protected virtual void LoadPagingSettings()
        {
            if (Grid.AllowPaging && Settings.PageSize > 0)
            {
                Grid.PageSize = Settings.PageSize;
            }
        }

        protected virtual void LoadGroupByExpressions()
        {
            if (Settings.GroupByExpressionsStates == null) return;

            Grid.MasterTableView.GroupByExpressions.Clear();
            foreach (object expressionState in Settings.GroupByExpressionsStates)
            {
                GridGroupByExpression expression = new GridGroupByExpression();
                ((IStateManager)expression).LoadViewState(expressionState);
                Grid.MasterTableView.GroupByExpressions.Add(expression);
            }
        }

        protected virtual void LoadSortExpressions()
        {
            if (Settings.SortExpressionsState == null) return;

            ((IStateManager)Grid.MasterTableView.SortExpressions).LoadViewState(Settings.SortExpressionsState);
        }

        protected virtual void LoadFilterExpression()
        {
            Grid.MasterTableView.FilterExpression = Settings.FilterExpression;
        }

        protected virtual void LoadColumnSettings()
        {
            if (Settings.AutoGeneratedColumnSettings.Count > 0)
            {
                Grid.ColumnCreated += new GridColumnCreatedEventHandler(Grid_ColumnCreated);
            }

            foreach (ColumnSettings colSetting in Settings.ColumnSettings)
            {
                GridColumn column = Grid.MasterTableView.GetColumnSafe(colSetting.UniqueName);

                if (column != null)
                {
                    SetColumnSettings(ref column, colSetting);
                }
            }
        }

        private void Grid_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            ColumnSettings settings = Settings.AutoGeneratedColumnSettings.Find(delegate(ColumnSettings set)
            {
                return set.UniqueName == e.Column.UniqueName;
            });
            GridColumn column = e.Column;

            if (settings != null)
            {
                SetColumnSettings(ref column, settings);
            }
        }

        
    }

    /// <summary>
    /// Enumerates the types of grid settings that can be persisted
    /// </summary>
    [Flags]
    public enum GridSettingsType
    {
        Paging = 1,
        Sorting = 2,
        Filtering = 4,
        Grouping = 8,
        ColumnSettings = 16,
        All = 32
    }

    /// <summary>
    /// Represents a collection of grid settings
    /// </summary>
    [Serializable]
    public class GridSettingsCollection
    {
        private int _pageSize;
        private object[] _groupByExpressionsStates;
        private object _sortExpressionsState;
        private string _filterExpression;
        private List<ColumnSettings> _columnSettings;
        private List<ColumnSettings> _autoColumnSettings;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public object[] GroupByExpressionsStates
        {
            get { return _groupByExpressionsStates; }
            set { _groupByExpressionsStates = value; }
        }

        public object SortExpressionsState
        {
            get { return _sortExpressionsState; }
            set { _sortExpressionsState = value; }
        }

        public string FilterExpression
        {
            get { return _filterExpression; }
            set { _filterExpression = value; }
        }

        public List<ColumnSettings> ColumnSettings
        {
            get { return _columnSettings; }
            set { _columnSettings = value; }
        }

        public List<ColumnSettings> AutoGeneratedColumnSettings
        {
            get { return _autoColumnSettings; }
            set { _autoColumnSettings = value; }
        }

        /// <summary>
        /// Returns the serialized object as string
        /// </summary>
        public override string ToString()
        {
            LosFormatter formatter = new LosFormatter();
            StringWriter writer = new StringWriter();
            formatter.Serialize(writer, this);

            return writer.ToString();
        }

        /// <summary>
        /// Returns the serialized object as byte array
        /// </summary>
        public byte[] ToArray()
        {
            LosFormatter formatter = new LosFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);

            return stream.ToArray();
        }

        /// <summary>
        /// Gets the GridSettingsCollectionInstance from its serialized string data
        /// </summary>
        /// <param name="data">The object as serialized string data</param>
        public static GridSettingsCollection LoadFromSerializedData(string data)
        {
            LosFormatter formatter = new LosFormatter();
            return (GridSettingsCollection)formatter.Deserialize(data);
        }

        /// <summary>
        /// Gets the GridSettingsCollectionInstance from its serialized byte array
        /// </summary>
        /// <param name="data">The object as serialized byte array</param>
        public static GridSettingsCollection LoadFromSerializedData(byte[] data)
        {
            LosFormatter formatter = new LosFormatter();
            MemoryStream stream = new MemoryStream(data);

            return (GridSettingsCollection)formatter.Deserialize(stream);
        }
    }

    /// <summary>
    /// Represents a collection of grid column settings
    /// </summary>
    [Serializable]
    public class ColumnSettings
    {
        private string _uniqueName;
        private int _orderIndex;
        private Unit _width;
        private bool _visible;
        private bool _display;
        private GridKnownFunction _currentFilterFunction;
        private string _currentFilterValue;

        public string UniqueName
        {
            get { return _uniqueName; }
            set { _uniqueName = value; }
        }

        public int OrderIndex
        {
            get { return _orderIndex; }
            set { _orderIndex = value; }
        }

        public Unit Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }

        public bool Display
        {
            get { return _display; }
            set { _display = value; }
        }

        public GridKnownFunction CurrentFilterFunction
        {
            get { return _currentFilterFunction; }
            set { _currentFilterFunction = value; }
        }

        public string CurrentFilterValue
        {
            get { return _currentFilterValue; }
            set { _currentFilterValue = value; }
        }
    }

        #endregion



}