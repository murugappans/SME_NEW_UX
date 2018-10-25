
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
using Microsoft.VisualBasic;
using System.Drawing;
using System.Data.SqlClient;
using SMEPayroll.TextTemplate;
using System.IO;
using System.Text;
using AuditLibrary;
using efdata;//Added by Jammu Office
using System.Linq;
using System.Data.OleDb;

namespace SMEPayroll.Payroll
{
    public partial class Emp_BulkAdd2 : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy = "";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        string _actionMessage = "";
        protected string strendatedmy = "";
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        DataSet dsFill;
        DateTime dtpayrollprocess;
        string strWF = "";
        string strEmpvisible = "";
        RadGrid grid;
        DataSet dsHeaderFill;
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {                     
            string sortname = string.Empty;
            if (grid.MasterTableView.SortExpressions.Count != 0)
            {
                sortname = grid.MasterTableView.SortExpressions[0].FieldName;
                
            } 

                DataView view = new DataView();
                DataTable table;
                string strEmpCodes = "";
                
                if (Session["EmpCodes"] != null)
                {
                    strEmpCodes = Session["EmpCodes"].ToString();
                }
                
                DataSet dsFill = (DataSet)Session["dsFill"];
                view = dsFill.Tables[0].DefaultView;
                table = view.ToTable();

                string strColName = "";
                if (e.Item is GridDataItem)
                {
                    int i = 3;

                    foreach (DataColumn dc in table.Columns)
                    {
                        if (i <= table.Columns.Count)
                        {
                            string templateColumnName = dc.ColumnName.ToString().ToUpper();

                            if (templateColumnName != "EMP_CODE" && templateColumnName != "FULLNAME" && templateColumnName != "TIME_CARD_NO")
                            {

                                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "Select [Desc] Title From additions_types Where ID=" + templateColumnName.ToString(), null);

                                if (dr1.Read())
                                {
                                    strColName = dr1[0].ToString();
                                }
                                dr1.Close();
                            }
                            if (strColName.ToString().Length > 0)
                            {
                                TextBox textBox = new TextBox();
                                textBox.ID = "txt" + templateColumnName;
                                textBox.DataBinding += new EventHandler(textBox_DataBinding1);
                                textBox.Width = System.Web.UI.WebControls.Unit.Pixel(35);
                                textBox.Height = System.Web.UI.WebControls.Unit.Pixel(55);
                                
                             
                                    if (table.Rows[e.Item.ItemIndex][dc].ToString() != "0.00")
                                    {
                                        
                                        textBox.Text = table.Rows[e.Item.ItemIndex][dc].ToString();
                                      
                                    }
                                //Validation to check whether there is alreadydata --change color
                                if (chkId.Checked)
                                {

                                    string sSQL = "sp_bulkaddtranspose";
                                    SqlParameter[] parms = new SqlParameter[4];
                                    parms[0] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                                    parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
                                    parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
                                    parms[3] = new SqlParameter("@DeptId", deptID.SelectedValue);
                                    DataSet dsFill_Exist = DataAccess.ExecuteSPDataSet(sSQL, parms);
                                    string col_exist = dc.ToString();

                                    DataView view1 = new DataView();
                                    DataTable table1;
                                    if (strEmpCodes != "")
                                    {

                                        view1 = dsFill_Exist.Tables[0].DefaultView;
                                        view1.RowFilter = "Emp_Code IN(" + strEmpCodes + ")";
                                        table1 = view1.ToTable();
                                    }
                                    else
                                    {
                                        view1 = dsFill_Exist.Tables[0].DefaultView;
                                        table1 = view1.ToTable();
                                    }

                                   
                                    if (e.Item.ItemIndex < table1.Rows.Count)
                                    {
                                        try
                                        {
                                            if (table1.Rows[e.Item.ItemIndex][col_exist].ToString() != "")
                                            {
                                                textBox.BackColor = Color.LightYellow;
                                                                             
                                                textBox.ToolTip = table1.Rows[e.Item.ItemIndex][col_exist].ToString();
                                                textBox.ForeColor = Color.Red;
                                               
                                            }
                                        }
                                        catch (Exception ex) { }

                                    }

                                }
                                
                                GridDataItem item = e.Item as GridDataItem;

                                string strc = "Column" + i.ToString();
                                item[strc].Controls.Add(textBox);

                                if (strColName.Length > 10)
                                {
                                    string strColumnName = strColName.ToString().Substring(0, 10);
                                    this.grid.Columns[i].HeaderText = strColName.ToString().Substring(0, 10);
                                    this.grid.Columns[i].Visible = true;
                                }
                                else
                                {
                                    string strColumnName1 = strColName.ToString();
                                    this.grid.Columns[i].HeaderText = strColName.ToString();
                                    this.grid.Columns[i].Visible = true;

                                }
                                i++;
                            }
                            else
                            {
                                if (this.grid.Columns[i].HeaderText == "")
                                {
                                    this.grid.Columns[i].Visible = false;
                                }
                            }
                        }
                    }
                }
            
    
        }

        [AjaxPro.AjaxMethod]
        public string SetDate(string monthid)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataSet ds = new DataSet();
            sSQL = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@ROWID", monthid);
            parms[1] = new SqlParameter("@YEARS", 0);
            parms[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            Session["PayStartDay"] = ds.Tables[0].Rows[0]["PayStartDay"].ToString();
            Session["PayEndDay"] = ds.Tables[0].Rows[0]["PayEndDay"].ToString();
            Session["PaySubStartDay"] = ds.Tables[0].Rows[0]["PaySubStartDay"].ToString();
            Session["PaySubEndDay"] = ds.Tables[0].Rows[0]["PaySubEndDay"].ToString();
            Session["PaySubStartDate"] = ds.Tables[0].Rows[0]["PaySubStartDate"].ToString();
            Session["PaySubEndDate"] = ds.Tables[0].Rows[0]["PaySubEndDate"].ToString();
            return Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "," + Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
        }
        protected void Page_Init(object sender, System.EventArgs e)
        {
            this.PopulateGridOnPageInit();
            
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource3.ConnectionString = Constants.CONNECTION_STRING;
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            //grid.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);        
            /* To disable Grid filtering options  */
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(Emp_BulkAdd));
            if (cmbMonth.Attributes["onchange"] == null) { cmbMonth.Attributes.Add("onchange", "javascript:ChangeMonth(this.value);"); }

            lblerror.Text = "";

            foreach (GridColumn column in grid.Columns)
                {
                    if (column.UniqueName == "" || column.HeaderText == "")
                    {
                        column.Visible = false;
                    }
                }
          
            comp_id = Utility.ToInteger(Session["Compid"]);
            
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
               
            }

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }


            //Check for WorkFlow number 2
            if (strWF == "2" && Session["PayrollWF"] != null)
            {
                if (Session["PayrollWF"].ToString() == "1")
                {

                    if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                    {
                        strEmpvisible = "";
                        if (Session["dsEmpSup"] != null)
                        {
                            if (Session["dsEmpWF"] != null)
                            {
                                DataSet dstemp = new DataSet();
                                dstemp = (DataSet)Session["dsEmpWF"];
                                foreach (DataRow dr in dstemp.Tables[0].Rows)
                                {
                                    if (strEmpvisible == "")
                                    {
                                        strEmpvisible = dr["Emp_ID"].ToString();
                                    }
                                    else
                                    {
                                        strEmpvisible = strEmpvisible + "," + dr["Emp_ID"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
           
        }

        void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string strEmpCode = item["Emp_Code"].Text.ToString();

                try
                {
                    string sSQL1 = "sp_GetPayrollProcessOn";
                    SqlParameter[] parms1 = new SqlParameter[3];
                    parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(strEmpCode));
                    parms1[1] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                    parms1[2] = new SqlParameter("@trxdate", Session["dtPayRollProcess"].ToString());
                    int conLock = 0;
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
                    
                    while (dr1.Read())
                    {
                        conLock = Utility.ToInteger(dr1.GetValue(0));
                    }
                    
                    if (conLock <= 0)
                    {
                        
                        item["GridClientSelectColumn"].Visible = true;
                        item.Enabled = true;
                    }
                    else
                    {
                        
                       // item["GridClientSelectColumn"].Enabled    = false ;
                        item.ToolTip = "Payroll has been Processed";
                        //item["Emp_Code"].ToolTip = "Payroll has been Processed";                    
                        item.BackColor = Color.LightGray;
                        item["GridClientSelectColumn"].Controls[0].Visible = false;
                        
                        
                    }
                   
                   
                }
                catch (Exception ex) { }

            }

                      
            //foreach (GridItem item in this.grid.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;

            //        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
            //        int CountValue = Convert.ToInt32(this.grid.Items[dataItem.ItemIndex]["Countvalue"].Text.ToString());
            //        if (CountValue <= 0)
            //        {
            //            chkBox.Visible = true;

            //        }
            //        else
            //        {

            //            item.ToolTip = "Payroll has been Processed";
            //            item.BackColor = Color.LightGray;
            //            chkBox.Visible = false;
            //        }

            //    }
            //}
        }

        protected void btnAdd_Types_Click(object sender, EventArgs e)
        {            
            Response.Redirect("../Payroll/SetupAdditionTypes.aspx?type=false", false);
        
        }

        protected void deptID_databound(object sender, EventArgs e)
        {
            deptID.Items.Insert(0, new ListItem("ALL", "-1"));
            deptID.SelectedValue = "-1";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strUpdateDelSQL = "";
            StringBuilder strDeleteAddition = new StringBuilder();
            StringBuilder strInsertAddition = new StringBuilder();
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            int trxtype= 0; double amount = 0;
            string strEmpCode = "";
           MonthFill();          
           try
           {
            foreach (GridItem item in this.grid.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                         strEmpCode = this.grid.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
                         amount = 0;
                            
                        strDeleteAddition.Append("(Emp_Code='" + strEmpCode + "' And BulkAddInMonth='" + Session["ROWID"].ToString() + "' And ClaimStatus like '%Approved%' ) Or ");

                        SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
                        string query = "SELECT top 40 ID,[desc],[used]  FROM   additions_types   WHERE  ((Company_ID = " + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 AND tax_payable_options NOT IN ('8','9','10','11','12') ORDER  BY [desc]";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        DataTable dtTable = new DataTable();
                        dtTable.Load(dr);
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {

                            string strcol = "Column" + Convert.ToInt32(dtTable.Rows[i]["ID"]);
                            if (this.grid.Items[dataItem.ItemIndex][strcol].Controls.Count > 0)
                            {

                                amount = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].Controls[0]).Text.ToString());
                               //if amount is null and already entered
                                double Tooltipval = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].Controls[0]).ToolTip.ToString());
                                if (amount == 0 && Tooltipval>0 )
                                {
                                    amount = Tooltipval;
                                }
                             
                                if (amount > 0 )
                                {
                                   string strtype = ((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].Controls[0]).ID.ToString().Replace("Column", "");
                                        trxtype = Convert.ToInt32(strtype);
                                        //If imported from Excel--> insert "import" in recpath
                                        if (chkId.Checked)
                                        {
                                            //  strInsertAddition.Append("Insert Into Emp_Additions (trx_type,trx_amount,amount, trx_period, emp_code, status, claimstatus, basis_arriving_payment, service_length, iras_approval, additionsforyear, BulkAddInMonth,recpath) values ('" + strtype + "'," + amount.ToString()+","  + amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','Approved','',0,'No','" + Session["ROWYEAR"].ToString() + "','" + Session["ROWID"].ToString() + "','import')");
                                            strInsertAddition.Append("Insert Into Emp_Additions (trx_type,trx_amount,amount, trx_period, emp_code, status, claimstatus, basis_arriving_payment, service_length, iras_approval, additionsforyear, BulkAddInMonth,recpath,CurrencyID,ConversionOpt) values ('" + strtype + "'," + amount.ToString() + "," + amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','Approved','',0,'No','" + Session["ROWYEAR"].ToString() + "','" + Session["ROWID"].ToString() + "','import',1,1)");
                                        }
                                        else
                                        {
                                            // strInsertAddition.Append("Insert Into Emp_Additions (trx_type,trx_amount,amount, trx_period, emp_code, status, claimstatus, basis_arriving_payment, service_length, iras_approval, additionsforyear, BulkAddInMonth) values ('" + strtype + "'," + amount.ToString()+"," + amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','Approved','',0,'No','" + Session["ROWYEAR"].ToString() + "','" + Session["ROWID"].ToString() + "')");
                                            strInsertAddition.Append("Insert Into Emp_Additions (trx_type,trx_amount,amount, trx_period, emp_code, status, claimstatus, basis_arriving_payment, service_length, iras_approval, additionsforyear, BulkAddInMonth,CurrencyID,ConversionOpt) values ('" + strtype + "'," + amount.ToString() + "," + amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','Approved','',0,'No','" + Session["ROWYEAR"].ToString() + "','" + Session["ROWID"].ToString() + "',1,1)");

                                        }
                                        //Added by Jammu Office
                                        #region Audit
                                        var oldrecord = new AuditLibrary.EmpAddition();
                                        DateTime dtTrxPeriod = Convert.ToDateTime(Session["PaySubStartDate"].ToString());
                                        var newrecord = new AuditLibrary.EmpAddition()
                                        {
                                            TrxId = 0,
                                            EmpCode = strEmpCode,
                                            TrxType = trxtype,
                                            TrxPeriod = dtTrxPeriod,
                                            TrxAmount = Convert.ToDecimal(amount),
                                            BasisArrivingPayment = "0",
                                            ServiceLength = 0,
                                            //IrasApproval = Utility.ToString(strdrpiras_approval),
                                            //IrasApprovalDate= Convert.ToDateTime(strtxtiras_approval_date),
                                            Additionsforyear = Session["ROWYEAR"].ToString(),
                                            CurrencyId = 1,
                                            Claimstatus = "Approved",
                                            ConversionOpt = 1,
                                            Status = "U",
                                            //ExchangeRate = Convert.ToDecimal(exchangeRate),
                                            BulkAddInMonth = Convert.ToInt32(Session["ROWID"])

                                        };
                                        var AuditRepository = new AuditRepository();
                                        AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, 1, oldrecord, newrecord);//NeRecordId

                                        #endregion

                                    }
                                }
                        }
                    }
                }
            }

           
            //validation 
            if (strDeleteAddition.ToString().Length == 0)
            {
              //  ShowMessageBox("");
                    _actionMessage = "Warning|Please Select the Employees to Update";
                    ViewState["actionMessage"] = _actionMessage;

                }
            //

            if (strDeleteAddition.ToString().Length > 0)
            {
                strUpdateDelSQL = "Delete From Emp_Additions Where (" + strDeleteAddition + " 1=0)";
                int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
            }

                if (strInsertAddition.ToString().Length > 0)
                {
                    strUpdateDelSQL = "" + strInsertAddition + "";
                    int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                    //string sSQLCheck = "SELECT MAX(trx_id) AS LargestId FROM emp_additions ";
                    //int NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null);
                    
                    //ShowMessageBox("Additions Updated Successfully");
                    _actionMessage = "Success|Additions Updated Successfully";
                    ViewState["actionMessage"] = _actionMessage;

                }

        }
        catch (Exception err)
        {
            //ShowMessageBox("Error in data " + err.Message.ToString());
                _actionMessage = "Warning|Error in data "+ err.Message.ToString();
                ViewState["actionMessage"] = _actionMessage;
            }
        
        }


        protected void PopulateGridOnPageInit()
        {
            try
            {
                grid = new RadGrid();
                grid.ID = "RadGrid1";
                grid.HeaderContextMenu.Visible = false;
                // grid.AllowSorting = true;
                grid.ExportSettings.FileName = "MultiAdditions_Details";
                grid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
                grid.AllowPaging = false;
                grid.Skin = "Outlook";
                grid.AllowFilteringByColumn = true;
                grid.EnableViewState = true;
                grid.AllowSorting = true; //murugan
                grid.ClientSettings.Scrolling.UseStaticHeaders = true;
                grid.ClientSettings.Scrolling.FrozenColumnsCount = 3;
                grid.ClientSettings.Scrolling.SaveScrollPosition = true; ;
                grid.MasterTableView.AutoGenerateColumns = true;
                grid.MasterTableView.EnableColumnsViewState = true;
                grid.NeedDataSource += RadGrid1_NeedDataSource;
                grid.ClientSettings.Selecting.AllowRowSelect = true;
                grid.ClientSettings.AllowColumnsReorder = true;
                grid.ClientSettings.ReorderColumnsOnClient = true;
                grid.ClientSettings.ColumnsReorderMethod = Telerik.Web.UI.GridClientSettings.GridColumnsReorderMethod.Reorder;
                grid.ClientSettings.ClientEvents.OnDataBound = "onDataBound";
                grid.AllowMultiRowSelection = true;
                grid.ClientSettings.Scrolling.AllowScroll = true;
                grid.PageSize = 10000;
                grid.AlternatingItemStyle.Wrap = false;
                grid.Font.Size = 11;
                grid.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                grid.GridExporting += RadGrid1_GridExporting;
                grid.ItemDataBound += RadGrid1_ItemDataBound;
                grid.Height = System.Web.UI.WebControls.Unit.Percentage(100);
                grid.EnableHeaderContextMenu = true;
                grid.MasterTableView.AutoGenerateColumns = false;
                grid.HeaderContextMenu.ItemCreated += new RadMenuEventHandler(HeaderContextMenu_ItemCreated);

               
                GridBoundColumn templateColumn1 = new GridBoundColumn();
                templateColumn1.DataField = "Emp_Code";
                templateColumn1.ShowFilterIcon = false;
                templateColumn1.AutoPostBackOnFilter = true;
                templateColumn1.CurrentFilterFunction = GridKnownFunction.StartsWith; 
                templateColumn1.HeaderText = "EmpCode";
                templateColumn1.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(100);
                templateColumn1.SortExpression = "Emp_Code";
                templateColumn1.FilterControlAltText = "numericonly";
                grid.MasterTableView.Columns.Add(templateColumn1);


                GridBoundColumn templateColumn2 = new GridBoundColumn();
                templateColumn2.DataField = "FullName";
                templateColumn2.ShowFilterIcon = false;
                templateColumn2.AutoPostBackOnFilter = true;
                templateColumn2.CurrentFilterFunction = GridKnownFunction.Contains; 
                templateColumn2.FilterControlAltText = "alphabetsonly";
                templateColumn2.HeaderText = "FullName";
                templateColumn2.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(100);
                grid.MasterTableView.Columns.Add(templateColumn2);

                GridBoundColumn templateColumn3 = new GridBoundColumn();
                templateColumn3.DataField = "Time_Card_No";
                templateColumn3.ShowFilterIcon = false;
                templateColumn3.AutoPostBackOnFilter = true;
                templateColumn3.CurrentFilterFunction = GridKnownFunction.Contains; 
                templateColumn3.HeaderText = "Time_Card_No";
                templateColumn3.FilterControlAltText = "cleanstring";






                templateColumn3.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(100); 
                grid.MasterTableView.Columns.Add(templateColumn3);
                
                //---murugan for resize columns
                grid.ClientSettings.Resizing.ResizeGridOnColumnResize = true;
                grid.ClientSettings.Resizing.AllowColumnResize = true;
                grid.ClientSettings.Resizing.EnableRealTimeResize = true;

                
                //grid.ClientSettings.ClientEvents.OnGridCreated = "MYGridCreated";

                //GridBoundColumn templateColumn5 = new GridBoundColumn();
                //templateColumn5.DataField = "Countvalue";
                //templateColumn5.HeaderText = "Countvalue";
                //templateColumn5.Visible = false;
                //grid.MasterTableView.Columns.Add(templateColumn5);


                SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
                string query = "SELECT top 40 ID,[desc],[used]  FROM  additions_types   WHERE  ((Company_ID = " + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 and (Active=1 or Active is null) AND tax_payable_options NOT IN ('8','9','10','11','12') ORDER  BY [desc]";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

               

                while (dr.Read())
                {
                    
                    GridTemplateColumn templateColumn4 = new GridTemplateColumn();
                    templateColumn4.ItemTemplate = new TextTemplate(Convert.ToString(dr["ID"]), Convert.ToInt32(dr["ID"]));
                    templateColumn4.ShowFilterIcon = false;
                    templateColumn4.AutoPostBackOnFilter = true;                 
                    templateColumn4.CurrentFilterFunction = GridKnownFunction.Contains; 
                    templateColumn4.DataField = Convert.ToString(dr["ID"]);
                    templateColumn4.UniqueName = "Column" + Convert.ToInt32(dr["ID"]);
                    templateColumn4.HeaderText = Convert.ToString(dr["desc"]);
                    templateColumn4.FilterControlAltText = "cleanstring";
                    templateColumn4.AllowFiltering = false;

                    if (templateColumn4.HeaderText == "Advance Bonus" || templateColumn4.HeaderText == "Back Pay Salary")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(115);
                    }
                    else if (templateColumn4.HeaderText == "AWS" || templateColumn4.HeaderText == "Bonus" || templateColumn4.HeaderText == "Merit Bonus" || templateColumn4.HeaderText == "SITE" || templateColumn4.HeaderText == "Pension" || templateColumn4.HeaderText == "Taxi Claim")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(92);
                    }
                    else if (templateColumn4.HeaderText == "General Reimbursement" || templateColumn4.HeaderText == "Site Allowance - Oversea")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(170);
                    }
                    else if (templateColumn4.HeaderText == "Housing Allowance" || templateColumn4.HeaderText == "CPF Make-Up Pay")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(135);
                    }
                    else if (templateColumn4.HeaderText == "JMU_Cost_testing")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(125);
                    }
                    else if (templateColumn4.HeaderText == "Transport Allowance")
                    {
                        templateColumn4.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(145);
                    }

                    grid.MasterTableView.Columns.Add(templateColumn4);
                  
                }

                string templateCheckBox = "SelectItem";
                GridClientSelectColumn templateCheckBoxColumn = new GridClientSelectColumn();
                
                templateCheckBoxColumn.UniqueName = "GridClientSelectColumn";
                templateCheckBoxColumn.HeaderText = templateCheckBox;
                templateCheckBoxColumn.HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(35); 
                grid.MasterTableView.Columns.Add(templateCheckBoxColumn);
               
                PlaceHolder1.Controls.Add(grid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        void HeaderContextMenu_ItemCreated(object sender, RadMenuEventArgs e)
        {
            if (e.Item.Value.Contains("GridClientSelectColumn") && e.Item.Controls.Count > 0)
                (e.Item.Controls[0] as CheckBox).Visible = false;
        }
        private DataSet BuildTable()
        {

            if (Session["EmpCodes"] != null)
            {
                strEmpvisible = Session["EmpCodes"].ToString();
            }

            dsFill = new DataSet();
            string sSQL;


            if (chkId.Checked)
            {
                sSQL = "sp_bulkaddtranspose_Temp";
            }
            else
            {
                sSQL = "sp_bulkaddtranspose";
            }
            try
            {
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
                parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
                parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));//Senthil for GroupManagement
                parms[4] = new SqlParameter("@DeptId", deptID.SelectedValue);
                
                    dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);
                
                TabId.Visible = true;
                btnUpdate.Visible = true;             
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //lblerror.ForeColor = System.Drawing.Color.Red;                
                //lblerror.Text = "Please Select Atlest One";
                _actionMessage = "Warning|Please Select Atlest One";
                ViewState["actionMessage"] = _actionMessage;
            }
            return dsFill;
        }
        string sSQL_sp;
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RadGrid rd = new RadGrid();
            rd = ((RadGrid)source);
            if (Utility.ToInteger(Session["s"]) == 1)
            {
                if (chkId.Checked)
                {
                    sSQL_sp = "sp_bulkaddtranspose_Temp";
                }
                else
                {
                    sSQL_sp = "sp_bulkaddtranspose";
                }

                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
                parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
                parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));//Senthil for GroupManagement
                parms[4] = new SqlParameter("@DeptId", deptID.SelectedValue);

                grid.CurrentPageIndex = e.NewPageIndex;
                dsFill = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL_sp, parms);
                grid.DataSource = dsFill;
                grid.DataBind();

            }
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




        void ExecSp(RadGrid rd)
        {

        }
        bool output;
        string sQlClear, sQlClearAll;
        bool first = false;
        protected void bindgrid(object sender, EventArgs e)
        {
            first = true;
           
            Session["dsFill"] = "";
            Session["EmpCodes"] = "";

            if (chkId.Checked)
            {
                output = ExcelImport();        
            }

            if (output || chkId.Checked==false)
            {

                //check the clear in dropdown
                if (Drpclear.SelectedValue == "clearimport")
                {
                    if(deptID.SelectedValue=="-1")// if all dept
                    {
                        sQlClear = "delete EA from emp_additions EA inner join additions_types AT on AT.id=EA.trx_type  inner join employee E on EA.emp_code=E.emp_code  where recpath='import' and status='U' and MONTH(trx_period)=(select [Month] from PayrollMonthlyDetail where ROWID=" + cmbMonth.SelectedValue + " and [Year]=" + cmbYear.SelectedValue + ") and year(trx_period)=" + cmbYear.SelectedValue + "  and AT.optionselection='General'";
                    }
                    else
                    {
                     sQlClear = "delete EA from emp_additions EA inner join additions_types AT on AT.id=EA.trx_type  inner join employee E on EA.emp_code=E.emp_code  where recpath='import' and status='U' and MONTH(trx_period)=(select [Month] from PayrollMonthlyDetail where ROWID=" + cmbMonth.SelectedValue + " and [Year]=" + cmbYear.SelectedValue + ") and year(trx_period)=" + cmbYear.SelectedValue + " and E.dept_id=" + deptID.SelectedValue + " and AT.optionselection='General'";
                    }
                    DataAccess.ExecuteNonQuery(sQlClear, null);
                    lblerror.Text = "Cleared Sucessfully";
                   // return;
                }
                else if (Drpclear.SelectedValue == "clearall")
                {
                    if (deptID.SelectedValue == "-1")// if all dept
                    {
                        sQlClearAll = "delete EA from emp_additions EA inner join additions_types AT on AT.id=EA.trx_type  inner join employee E on EA.emp_code=E.emp_code  where status='U' and MONTH(trx_period)=(select [Month] from PayrollMonthlyDetail where ROWID=" + cmbMonth.SelectedValue + " and [Year]=" + cmbYear.SelectedValue + ") and year(trx_period)=" + cmbYear.SelectedValue + "  and AT.optionselection='General'";
                    }
                    else
                    {
                        sQlClearAll = "delete EA from emp_additions EA inner join additions_types AT on AT.id=EA.trx_type  inner join employee E on EA.emp_code=E.emp_code  where status='U' and MONTH(trx_period)=(select [Month] from PayrollMonthlyDetail where ROWID=" + cmbMonth.SelectedValue + " and [Year]=" + cmbYear.SelectedValue + ") and year(trx_period)=" + cmbYear.SelectedValue + " and E.dept_id=" + deptID.SelectedValue + " and AT.optionselection='General'";
                    }
                    DataAccess.ExecuteNonQuery(sQlClearAll, null);
                    lblerror.Text = "Cleared Sucessfully";
                   // return;
                }
                //

                int i = 0;
              
                intcnt = 1;
                cmbYear.Enabled = false;//ram
                cmbMonth.Enabled = false;
                deptID.Enabled = false;
                chkId.Enabled = false;
                FileUpload.Visible = false;
                Session["ROWID"] = cmbMonth.SelectedValue.ToString();
                Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
                grid.DataSource = BuildTable();

                //
                string strdate = "Select PaySubStartDate from PayrollMonthlyDetail  where ROWID=" + cmbMonth.SelectedValue + " AND Year=" + cmbYear.SelectedValue;

                SqlDataReader drdate;
                drdate = DataAccess.ExecuteReader(CommandType.Text, strdate, null);

                while (drdate.Read())
                {
                    if (drdate.GetValue(0) != null)
                    {
                        dtpayrollprocess = Convert.ToDateTime(drdate.GetValue(0));
                        Session["dtPayRollProcess"] = dtpayrollprocess.ToString("dd/MM/yyyy");
                    }               
                }
                grid.DataBind();

                //muru
                if (grid.MasterTableView.Items.Count > 0)
                    tbRecord.Visible = true;
                else
                    tbRecord.Visible = false;


            }
           
        }
        #region Excel Import
        bool res;
        protected bool ExcelImport()
        {
            
            string strMsg = "";
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType = FileUpload.PostedFile.ContentType;
                int IntFileSize = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                {
                    strMsg = "Please Select File to be uploaded";
                    //ShowMessageBox("Please Select File to be uploaded");
                    _actionMessage = "Warning|Please Select File to be uploaded";
                    ViewState["actionMessage"] = _actionMessage;
                    res = false;
                }

                else
                {
                    res = true;
                    int RandomNumber = 0;
                    RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

                    string strTranID = Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];
                    StrFileName = FileExt[0] + strTranID;
                    string stfilepath = Server.MapPath(@"..\\Documents\\UploadAddDed\" + StrFileName + strExtent);
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);

                        string filename = StrFileName + strExtent;
                        ImportExcelTosqlServer(filename);

                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                        _actionMessage = "Warning|"+ex.Message;
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }

            }
            //lblerror.Text = strMsg;

            return res;
        }
        string col, Empcode, ICNUMBER, Empcode1;
        decimal A1,Addition;
        string strEmpCodes = "";
        public void ImportExcelTosqlServer(string filename)
        {
            DataTable dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append("");
            try
            {
                string strEmpCodes = "";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 1)//skip the first 2 column
                    {
                        int c = dt.Rows.Count;
                        foreach (DataRow dr in dt.Rows)
                        {
                            col = dt.Columns[i].ToString();

                            //check whether IC number is present
                            //if yes--> get empno from that
                            //else --> check the empno directly
                            ICNUMBER = dr["IC NUMBER"].ToString();
                            if (ICNUMBER.Trim () == "")
                            {
                                Empcode = dr["EMP ID"].ToString();

                                if (ICNUMBER.Trim().Length  == 0 && Empcode.Trim().Length  == 0)
                                {
                                    break;
                                }
                            }
                            else
                            {
                                Empcode = null;
                                //  string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                                string sql = " select time_card_no from employee where ic_pp_number='" + ICNUMBER + "'";
                                SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr_empcode.Read())
                                {
                                    Empcode = dr_empcode["time_card_no"].ToString();
                                }
                            }
                            if (Empcode == null)
                            {
                                Empcode = dr["EMP ID"].ToString();
                            }
                            //
                            

                            //if (dr[dt.Columns[i].ToString()] == System.DBNull.Value || dr[dt.Columns[i].ToString()].ToString().Trim().Length ==0)
                            if (dr[dt.Columns[i].ToString()].ToString().Trim()  == "" || dr[dt.Columns[i].ToString()].ToString().Trim().Length == 0)
                            {
                                A1 = 0;
                            }
                            else
                            {
                                A1 = Convert.ToDecimal(dr[dt.Columns[i].ToString()]);//A1 = dr["A1"].ToString();
                            }
                            #region converting A1 to mapped column id
                            string sql_emp = "SELECT Additions_Id FROM [MapAdditions] where Company_id='"+comp_id+"' AND  MapVariable='" + dt.Columns[i].ToString() + "'";
                            SqlDataReader dr_addit = DataAccess.ExecuteReader(CommandType.Text, sql_emp, null);
                            if (dr_addit.Read())
                            {
                                if (dr_addit["Additions_Id"] == System.DBNull.Value || dr_addit["Additions_Id"].ToString().Trim().Length ==0)
                                {
                                    Addition = 0;
                                }
                                else
                                {

                                    Addition = Convert.ToInt32(dr_addit["Additions_Id"]);
                                }

                            }
                            #endregion

                            #region Convert the (Empcode)timecardId to Emp_code

                             string sql_timecard2empcode = " select emp_code from employee where time_card_no='" + Empcode + "'and termination_date is null and company_id=" + comp_id;
                           // string sql_timecard2empcode = " select time_card_no from employee where time_card_no='" + Empcode + "'and termination_date is null and company_id=" + comp_id;
                            SqlDataReader dr_empcode_timecard2empcode = DataAccess.ExecuteReader(CommandType.Text, sql_timecard2empcode, null);
                                if (dr_empcode_timecard2empcode.HasRows)
                                {
                                    if (dr_empcode_timecard2empcode.Read())
                                    {
                                      Empcode1 = dr_empcode_timecard2empcode["emp_code"].ToString();
                                   

                                }
                            #endregion

                                    //if (dr_addit.HasRows && Empcode!="")//if mapped then only insert
                                    if (dr_addit.HasRows && Empcode1 != "" && Addition > 0 && A1 > 0)//if mapped then only insert
                                    {
                                        if (strEmpCodes != "")
                                        {
                                            strEmpCodes = strEmpCodes + "," + Empcode1;
                                        }
                                        else
                                        {
                                            strEmpCodes = Empcode1;
                                        }
                                        Session["EmpCodes"] = strEmpCodes;

                                        SqlQuery.Append("INSERT INTO Temp_Emp_Additions ([Emp_code],[trx_type],[trx_amount],[BulkAddInMonth]) VALUES(" + Empcode1 + ",'" + Addition + "','" + A1 + "','" + Convert.ToInt32(cmbMonth.SelectedValue) + "')");
                                    }
                                }

                        }
                    }
                }         
                string str = SqlQuery.ToString();
                DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                DataAccess.FetchRS(CommandType.Text, SqlQuery.ToString(), null);
            }
            catch (Exception e)
            {
                DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                //ShowMessageBox("Error for the Employee:"+Empcode+" Msg-"+ e.Message.ToString());
                _actionMessage = "Warning|Error for the Employee:"+Empcode+" Msg-"+ e.Message.ToString();
                ViewState["actionMessage"] = _actionMessage;

            }

        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt1 = new DataTable();
            try
            {
                dt1.Clear();
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/UploadAddDed/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                string SheetName = "Sheet1";//here enter sheet name        
                oledbconn.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + SheetName + "$]", oledbconn);
                OleDbDataAdapter oledbda = new OleDbDataAdapter();
                oledbda.SelectCommand = cmdSelect;
                oledbda.Fill(dt1);
                oledbconn.Close();
                oledbda = null;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt1;
        }


        protected void chkId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                FileUpload.Visible = true;            
            }
            else
            {
                FileUpload.Visible = false;
              
            }
        }


        #endregion

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private void bindMonth()
        {
            MonthFill();
        }


        void MonthFill()
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();

            if (Session["ROWID"] == null)
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            else
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            if (Session["ROWID"] != null)
            {
                SetControlDate(Session["ROWID"].ToString());
            }
            else
            {
                SetControlDate(cmbMonth.SelectedValue);
            }
        }

        void SetControlDate(string mon)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + mon);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
                strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
                strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
                strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
            }
        }


        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            grid.MasterTableView.GetColumn("Additional Allowance").HeaderStyle.Width = System.Web.UI.WebControls.Unit.Pixel(500);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Session["ROWID"] == null)
            {
            }
            else
            {
                if (intcnt == 1)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                else
                {
                    if (IsPostBack == true)
                    {
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                    SetControlDate(cmbMonth.SelectedValue);
                }
            }

        }

        public void textBox_DataBinding1(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridDataItem container = (GridDataItem)t.NamingContainer;
            t.Height = System.Web.UI.WebControls.Unit.Pixel(17);
            t.Width = System.Web.UI.WebControls.Unit.Pixel(70);
            t.CssClass = "txtheight";
        }
   
        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {

            if ((e.RebindReason != GridRebindReason.InitialLoad))
            {
                grid.DataSource = BuildTable();
            }
        }

        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            grid.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
           
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, this.grid, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), this.grid);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("108", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }
        #endregion
        //Toolbar End



    }
    public class TextTemplate : ITemplate
    {

        protected TextBox textBox;

        private string colname;
        private int value;

        public TextTemplate(string cName, int cValue)
        {
            colname = cName;
            value = cValue;
        }
        public void InstantiateIn(System.Web.UI.Control container)
        {
            textBox = new TextBox();
            textBox.ID = "Column" + value.ToString();
            textBox.DataBinding += new EventHandler(textBox_DataBinding);
            container.Controls.Add(textBox);


        }
        public void textBox_DataBinding(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            GridDataItem container = (GridDataItem)txt.NamingContainer;
            txt.Text = ((DataRowView)container.DataItem)[colname].ToString();
        }
    }
    public class CheckBoxTemplate : ITemplate
    {


        protected CheckBox boolValue;
        private string colname;

        public CheckBoxTemplate(string cName)
        {
            colname = cName;

        }
        public void InstantiateIn(System.Web.UI.Control container)
        {

            boolValue = new CheckBox();
            boolValue.ID = colname;
            container.Controls.Add(boolValue);

        }
    }
}
