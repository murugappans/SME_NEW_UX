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
using efdata;
using System.Text;
using System.Data.OleDb;
namespace SMEPayroll.Payroll
{
    public partial class EmployeeDeduction : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy = "";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        protected string strendatedmy = "";
        int LoginEmpcode = 0;
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        //static DataSet dsFill;
        string _actionMessage = "";
        DataSet dsFill;
        DateTime dtpayrollprocess;

        int intblock = 0;


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
            //Remove Data From Dataset\
            if (Session["EmpCodes"] != null)
            {
                strEmpCodes = Session["EmpCodes"].ToString();
            }
            
            if (strEmpCodes != "")
            {
                DataSet dsFill = (DataSet)Session["dsFill"];
                if (!string.IsNullOrEmpty(sortname))
                {
                    string or = "ASC";
                    if (grid.MasterTableView.SortExpressions[0].SortOrder.ToString() == "Descending")
                    {
                        or = "DESC";

                    }
                    dsFill.Tables[0].DefaultView.Sort = sortname + " " + or;
                }
                view = dsFill.Tables[0].DefaultView;
                view.RowFilter = "Emp_Code IN(" + strEmpCodes + ")";
                table = view.ToTable();
            }
            else
            {
                DataSet dsFill = (DataSet)Session["dsFill"];
                if (!string.IsNullOrEmpty(sortname))
                {
                    string or = "ASC";
                    if (grid.MasterTableView.SortExpressions[0].SortOrder.ToString() == "Descending")
                    {
                        or = "DESC";

                    }

                    dsFill.Tables[0].DefaultView.Sort = sortname + " " + or;
                }
                view = dsFill.Tables[0].DefaultView;
                table = view.ToTable();
            }

            string strColName = "";
            string employee_code = "";
            string employee_grid = "";
            try
            {
                employee_code = table.Rows[e.Item.ItemIndex]["Emp_Code"].ToString();
                employee_grid = ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString();

            }

            catch (Exception ex) { }

            if (e.Item is GridDataItem)
            {
                int i = 3;

                foreach (DataColumn dc in table.Columns)
                {
                    string templateColumnName = dc.ColumnName.ToString().ToUpper();

                    if (templateColumnName != "EMP_CODE" && templateColumnName != "FULLNAME" && templateColumnName != "TIME_CARD_NO" && templateColumnName != "TRADE")
                    {
                        SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "Select [Desc] Title From Deductions_Types Where ID=" + templateColumnName.ToString(), null);
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
                        textBox.Width = Unit.Pixel(30);
                       
                        try
                        {
                            if (table.Rows[e.Item.ItemIndex][dc].ToString() != "0.00")
                            {

                                textBox.Text = table.Rows[e.Item.ItemIndex][dc].ToString();//dsFill.Tables[0].Rows[e.Item.ItemIndex][dc].ToString();
                                                    

                            }
                        }
                        catch (Exception ex) { }

                        //Validation to check whether there is alreadydata --change color
                        if (chkId.Checked)
                        {
                            #region Load the existing dataset to show data already exist
                            string sSQL = "sp_bulkdedtranspose";
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
                                view1.Sort = "FullName asc";
                                table1 = view1.ToTable();
                            }
                            else
                            {
                                view1 = dsFill_Exist.Tables[0].DefaultView;
                                view1.Sort = "FullName asc";
                                table1 = view1.ToTable();
                            }
                            #endregion

                            #region Change colour if already there is data[compare two table which has same structure and data]
                            //ku

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
                            #endregion

                        }
                        //
                        textBox.Attributes.Add("onkeypress", "return isNumericKeyStrokeDecimal(event);");

                        GridDataItem item = e.Item as GridDataItem;

                        string strc = "Column" + i.ToString();
                        item[strc].Controls.Add(textBox);

                        if (strColName.Length > 10)
                        {
                            this.grid.Columns[i].HeaderText = strColName.ToString().Substring(0, 10);
                        }
                        else
                        {
                            this.grid.Columns[i].HeaderText = strColName.ToString();
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

        void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {

                GridDataItem item = (GridDataItem)e.Item;
                string strEmpCode = item["Emp_Code"].Text.ToString();

                string sSQL1 = "sp_GetPayrollProcessOn";
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(strEmpCode));
                parms1[1] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                if (dtpayrollprocess.ToString("dd/MM/yyyy") == "01/01/0001")
                {
                    dtpayrollprocess = DateTime.Now;
                }
                parms1[2] = new SqlParameter("@trxdate", dtpayrollprocess.ToString("dd/MM/yyyy"));
                int conLock = 0;
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
                while (dr1.Read())
                {
                    conLock = Utility.ToInteger(dr1.GetValue(0));
                }
                if (conLock <= 0)
                {
                    item.Enabled = true;
                    item["GridClientSelectColumn"].Visible = true;
                }
                else
                {
                    item.ToolTip = "Payroll has been Processed";
                    //item["Emp_Code"].ToolTip = "Payroll has been Processed";                    
                    item.BackColor = Color.LightGray;
                    item["GridClientSelectColumn"].Controls[0].Visible = false;
                }


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
            //           chkBox.Visible = true;
                        
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

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource3.ConnectionString = Constants.CONNECTION_STRING;
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            //grid.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
            /* To disable Grid filtering options  */
           // AjaxPro.Utility.RegisterTypeForAjax(typeof(Emp_BulkDed));
            if (cmbMonth.Attributes["onchange"] == null) { cmbMonth.Attributes.Add("onchange", "javascript:ChangeMonth(this.value);"); }
            comp_id = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
            }
            // ku comments-2_3_2015
            if (!IsPostBack)
            {
                grid.ExportSettings.FileName = "Multi_Deductions";
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
        protected void deptID_databound(object sender, EventArgs e)
        {
            //deptID.Items.Insert(0, new ListItem("ALL", "-1"));
            deptID.SelectedValue = "-1";
        }

        protected void btnAdd_Types_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Payroll/SetupDeductionTypes.aspx?type=false", false);

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string strUpdateDelSQL = "";
            StringBuilder strDeleteAddition = new StringBuilder();
            StringBuilder strInsertAddition = new StringBuilder();
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            int trtype = 0;
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
                           // string s = dataItem["Emp_Code"].Text.ToString();

                            string s = Utility.ToString(this.grid.MasterTableView.Items[dataItem.ItemIndex]["Emp_Code"].Text);
                            string strEmpCode = this.grid.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();

                            double amount = 0;
                            strDeleteAddition.Append("(Emp_Code='" + strEmpCode + "' And BulkDedInMonth='" + Session["ROWID"].ToString() + "') Or ");

                            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
                            string query = "SELECT top 40 ID,[desc],[used]  FROM   Deductions_Types   WHERE  ((Company_ID =" + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 ORDER  BY [desc]";
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

                                    amount = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).Text.ToString());
                                    //if amount is null and already entered
                                    double Tooltipval = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).ToolTip.ToString());
                                    if (amount == 0 && Tooltipval > 0)
                                    {
                                        amount = Tooltipval;
                                    }
                                    //
                                    if (amount > 0)
                                    {
                                        string strtype = ((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).ID.ToString().Replace("Column", "");
                                        trtype = Convert.ToInt32(strtype);
                                        strInsertAddition.Append("Insert Into Emp_Deductions (trx_type,trx_amount,amount, trx_period, emp_code, status, BulkDedInMonth) values ('" + strtype + "'," + amount.ToString() +","+ amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','" + Session["ROWID"].ToString() + "')");
                                    }
                                    //Added by Jammu Office
                                    #region Audit
                                    var oldrecord = new AuditLibrary.EmpDeduction();
                                    DateTime dtTrxPeriod = Convert.ToDateTime(Session["PaySubStartDate"].ToString());
                                    var newrecord = new AuditLibrary.EmpDeduction()
                                    {
                                        TrxId = 0,
                                        EmpCode = strEmpCode,
                                        TrxType = trtype,
                                        TrxPeriod = dtTrxPeriod,
                                        TrxAmount = Convert.ToDecimal(amount),                                       
                                        Status = "U",
                                        BulkDedInMonth = Convert.ToInt32(Session["ROWID"])

                                    };
                                    var AuditRepository = new AuditRepository();
                                    AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, 1, oldrecord, newrecord);//NeRecordId

                                    #endregion
                                }

                            }
                          
                        }
                    }
                }

              
                //validation 
                //if (strDeleteAddition.ToString().Length == 0)
                //{
                //    ShowMessageBox("Please Select the Employees to Update");
                //}
                ////

                if (strDeleteAddition.ToString().Length > 0)
                {
                    strUpdateDelSQL = "Delete From Emp_Deductions Where (" + strDeleteAddition + " 1=0)";
                    int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                }
                if (strInsertAddition.ToString().Length > 0)
                {
                    strUpdateDelSQL = "" + strInsertAddition + "";
                    int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                    //ShowMessageBox("Deductions Updated Successfully");
                    _actionMessage = "Success|Deductions Updated Successfully";
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

        public int GetMonth(string month)
        {
            int mvalue = 1;
            switch (month)
            {
                case "January":
                    mvalue = 1;
                    break;
                case "February":
                    mvalue = 2;
                    break;
                case "March":
                    mvalue = 3;
                    break;
                case "April":
                    mvalue = 4;
                    break;
                case "May":
                    mvalue = 5;
                    break;
                case "June":
                    mvalue = 6;
                    break;
                case "July":
                    mvalue = 7;
                    break;
                case "August":
                    mvalue = 8;
                    break;
                case "September":
                    mvalue = 9;
                    break;
                case "October":
                    mvalue = 10;
                    break;
                case "November":
                    mvalue = 11;
                    break;
                case "December":
                    mvalue = 12;
                    break;
            }
            return mvalue;

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
                sSQL = "sp_bulkdedtranspose_Temp";
            }
            else
            {
                sSQL = "sp_bulkdedtranspose";
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
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RadGrid rd = new RadGrid();
            rd = ((RadGrid)source);
            if (Utility.ToInteger(Session["s"]) == 1)
            {
                string sSQL = "sp_bulkdedtranspose";
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
                parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
                parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
                parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));//Senthil for GroupManagement
                parms[4] = new SqlParameter("@DeptId", deptID.SelectedValue);

                grid.CurrentPageIndex = e.NewPageIndex;
                dsFill = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                grid.DataSource = dsFill;
                grid.DataBind();

            }
            
            BuildTable();
            grid.DataBind();
            grid.CurrentPageIndex = e.NewPageIndex;
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
        protected void Page_Init(object sender, System.EventArgs e)
        {         
           PopulateGridOnPageInit();
                    
        }
        void ExecSp(RadGrid rd)
        {

        }
        bool output;
        bool first = false;
        protected void bindgrid(object sender, EventArgs e)
        {
            first = true;
            Session["EmpCodes"] = "";
            Session["dsFill"] = "";
            if (chkId.Checked)
            {
                output = ExcelImport();
              
            }
            if (output || chkId.Checked == false)
            {

                int i = 0;
                
                intcnt = 1;
                cmbYear.Enabled = false;
                cmbMonth.Enabled = false;
                deptID.Enabled = false;
                Session["ROWID"] = cmbMonth.SelectedValue.ToString();
                Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
                grid.DataSource = BuildTable();
                           
                string strdate = "Select PaySubStartDate from PayrollMonthlyDetail  where ROWID=" + cmbMonth.SelectedValue + " AND Year=" + cmbYear.SelectedValue;

                SqlDataReader drdate;
                drdate = DataAccess.ExecuteReader(CommandType.Text, strdate, null);

                while (drdate.Read())
                {
                    if (drdate.GetValue(0) != null)
                    {
                        dtpayrollprocess = Convert.ToDateTime(drdate.GetValue(0));
                    }
                }
                grid.DataBind();
               
            }

            //muru
            if (grid.MasterTableView.Items.Count > 0)
                tbRecord.Visible = true;
            else
                tbRecord.Visible = false;
        }
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
        void CallBeforeMonthFill()
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
        }
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Boolean flag = false;
            DataRow[] drResults;
            int returnval = 0;
            if (e.CommandName == "UpdateAll")
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string empcode = "";
                string paySlipRemarks = "";
                foreach (GridItem item in grid.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;

                        //GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtRemarks");
                        paySlipRemarks = txtbox.Text.ToString();
                        empcode = this.grid.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code").ToString();

                        CallBeforeMonthFill();
                        drResults = monthDs.Tables[0].Select("RowId=" + cmbMonth.SelectedValue);
                        foreach (DataRow dr in drResults)
                        {
                            string sDate = Utility.ToDate1(dr["PaySubStartDate"].ToString());
                            string eDate = Utility.ToDate1(dr["PaySubEndDate"].ToString());
                            sDate = Convert.ToDateTime(sDate).ToString("MM/dd/yyyy", format);
                            eDate = Convert.ToDateTime(eDate).ToString("MM/dd/yyyy", format);
                            string SqlUpdate = "sp_UpdatePayslipRemarks";
                            int i = 0;
                            SqlParameter[] param = new SqlParameter[6];
                            param[i++] = new SqlParameter("@rowId", Utility.ToInteger(cmbMonth.SelectedValue));
                            param[i++] = new SqlParameter("@empCode", Utility.ToString(empcode));
                            param[i++] = new SqlParameter("@startDate", Utility.ToString(sDate));
                            param[i++] = new SqlParameter("@endDate", Utility.ToString(eDate));
                            param[i++] = new SqlParameter("@remarks", Utility.ToString(paySlipRemarks));
                            param[i++] = new SqlParameter("@remarksBy", Utility.ToString(Session["EmpCodes"].ToString()));

                            returnval = returnval + Convert.ToInt32(DataAccess.ExecuteStoreProc(SqlUpdate, param));
                        }
                        if (returnval > 0)
                            //lblerror.Text = "Employee Payslip Remarks Entered/Updated Successfully";
                        _actionMessage = "Success|Employee Payslip Remarks Entered/Updated Successfully";
                        ViewState["actionMessage"] = _actionMessage;

                    }
                }
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
            t.Height = Unit.Pixel(25);
            t.Width = Unit.Pixel(70);
            t.CssClass = "txtheight";
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if ((e.RebindReason != GridRebindReason.InitialLoad))
            {
                grid.DataSource = BuildTable();
            }
        }
     
        protected void PopulateGridOnPageInit()
        {
         
            grid = new RadGrid();         
            grid.ID = "RadGrid1";
            grid.HeaderContextMenu.Visible = false;
            grid.AllowSorting = true;
            grid.PagerStyle.Mode = GridPagerMode.NextPrevAndNumeric;
            grid.AllowPaging = false;           
            grid.Skin = "Outlook";
            grid.AllowFilteringByColumn = true;
            grid.EnableViewState = true;
            grid.MasterTableView.AutoGenerateColumns = false;
            
            grid.MasterTableView.EnableColumnsViewState = true;
            grid.NeedDataSource += RadGrid1_NeedDataSource;
            grid.ClientSettings.Selecting.AllowRowSelect = true;
            grid.ClientSettings.AllowColumnsReorder = true;
            grid.ClientSettings.ReorderColumnsOnClient = true;
           
            grid.ClientSettings.ColumnsReorderMethod = Telerik.Web.UI.GridClientSettings.GridColumnsReorderMethod.Reorder;
            grid.ClientSettings.ClientEvents.OnDataBound = "onDataBound";
            grid.ClientSettings.Scrolling.UseStaticHeaders = true;
            grid.ClientSettings.Scrolling.FrozenColumnsCount = 3;
            grid.ClientSettings.Scrolling.SaveScrollPosition = true;
            grid.ClientSettings.Scrolling.AllowScroll = true;
            grid.AllowMultiRowSelection = true;
            grid.PageSize =50;
          
            grid.AlternatingItemStyle.Wrap=false;
            grid.Font.Size = 11;
            grid.GridExporting += RadGrid1_GridExporting;        
            grid.ItemDataBound += RadGrid1_ItemDataBound;
            grid.Height = Unit.Percentage(80);
            grid.Width = Unit.Percentage(100);
            grid.EnableHeaderContextMenu = true;
            grid.HeaderContextMenu.ItemCreated += new RadMenuEventHandler(HeaderContextMenu_ItemCreated);
            GridBoundColumn templateColumn1 = new GridBoundColumn();
            templateColumn1.DataField = "Emp_Code";
           
            templateColumn1.ShowFilterIcon = false;
            templateColumn1.AutoPostBackOnFilter = true;
            templateColumn1.CurrentFilterFunction = GridKnownFunction.StartsWith;
            templateColumn1.HeaderText = "EmpCode";
            templateColumn1.FilterControlAltText = "numericonly";
            templateColumn1.UniqueName = "Emp_Code";
            templateColumn1.HeaderStyle.Width = Unit.Pixel(100);    
            grid.MasterTableView.Columns.Add(templateColumn1);

            GridBoundColumn templateColumn2 = new GridBoundColumn();
            templateColumn2.DataField = "FullName";
            templateColumn2.ShowFilterIcon = false;
            templateColumn2.CurrentFilterFunction = GridKnownFunction.Contains;
            templateColumn2.AutoPostBackOnFilter = true;
            templateColumn2.HeaderText = "FullName";
            templateColumn2.FilterControlAltText = "alphabetsonly";
            templateColumn2.UniqueName = "FullName";
            templateColumn2.HeaderStyle.Width = Unit.Pixel(300);    
            grid.MasterTableView.Columns.Add(templateColumn2);

            GridBoundColumn templateColumn3 = new GridBoundColumn();
            templateColumn3.DataField = "Time_Card_No";
            templateColumn3.ShowFilterIcon = false;
            templateColumn3.AutoPostBackOnFilter = true;
            templateColumn3.CurrentFilterFunction =GridKnownFunction.Contains; 
            templateColumn3.FilterControlAltText = "cleanstring";
            templateColumn3.HeaderText = "Time_Card_No";
            templateColumn3.HeaderStyle.Width = Unit.Pixel(100);    
            grid.MasterTableView.Columns.Add(templateColumn3);


            grid.ClientSettings.Resizing.ResizeGridOnColumnResize = true;
            grid.ClientSettings.Resizing.AllowColumnResize = true;
            grid.ClientSettings.Resizing.EnableRealTimeResize = true;
            //GridBoundColumn templateColumn5 = new GridBoundColumn();
            //templateColumn5.DataField = "Countvalue";
            //templateColumn5.HeaderText = "Countvalue";
            //templateColumn5.Visible = false;
            //grid.MasterTableView.Columns.Add(templateColumn5);

            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            string query = "SELECT top 40 ID,[desc],[used]  FROM   Deductions_Types   WHERE  ((Company_ID = " + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 ORDER  BY [desc]";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           
            while (dr.Read())
            {
                
                GridTemplateColumn templateColumn4 = new GridTemplateColumn();
                templateColumn4.ItemTemplate = new MyTemplate(Convert.ToString(dr["ID"]), Convert.ToInt32(dr["ID"]));
                templateColumn4.DataField = Convert.ToString(dr["ID"]);
                templateColumn4.ShowFilterIcon = false;
                templateColumn4.CurrentFilterFunction = GridKnownFunction.Contains;
                templateColumn4.AutoPostBackOnFilter = true;
                templateColumn4.UniqueName = "Column" + Convert.ToInt32(dr["ID"]);
                templateColumn4.HeaderText = Convert.ToString(dr["desc"]);
                //templateColumn4.HeaderStyle.Width = Unit.Pixel(150);
                templateColumn4.FilterControlAltText = "cleanstring";
                templateColumn4.AllowFiltering = false;


                if (templateColumn4.HeaderText == "Loan Repayment")
                {
                    templateColumn4.HeaderStyle.Width = Unit.Pixel(122);
                }
                else if (templateColumn4.HeaderText == "Make-Up Claim")
                {
                    templateColumn4.HeaderStyle.Width = Unit.Pixel(112);
                }
                else if (templateColumn4.HeaderText == "No Pay Leave Deduction")
                {
                    templateColumn4.HeaderStyle.Width = Unit.Pixel(168);
                }




                grid.MasterTableView.Columns.Add(templateColumn4);
               
            }
            string templateCheckBox = "SelectItem";
            GridClientSelectColumn templateCheckBoxColumn = new GridClientSelectColumn();
            templateCheckBoxColumn.UniqueName = "GridClientSelectColumn";
            templateCheckBoxColumn.HeaderText = templateCheckBox;
            templateCheckBoxColumn.HeaderStyle.Width = Unit.Pixel(35);
            //templateCheckBoxColumn.HeaderStyle.HorizontalAlign = "";
            grid.MasterTableView.Columns.Add(templateCheckBoxColumn);
            PlaceHolder1.Controls.Add(grid);
           
        }


      

        void HeaderContextMenu_ItemCreated(object sender, RadMenuEventArgs e)
        {
            if (e.Item.Value.Contains("GridClientSelectColumn") && e.Item.Controls.Count > 0)
                (e.Item.Controls[0] as CheckBox).Visible = false;
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
                        _actionMessage = "Warning|"+strMsg;
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }

            }
            //lblerror.Text = strMsg;

            return res;
        }
        string col, Empcode, ICNUMBER, Empcode1;
        decimal A1, Addition;
        string strEmpCodes = "";
        public void ImportExcelTosqlServer(string filename)
        {
            DataTable dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append("");
            try
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 1)//skip the first 2 column
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            col = dt.Columns[i].ToString();

                            //check whether IC number is present
                            //if yes--> get empno from that
                            //else --> check the empno directly
                            ICNUMBER = dr["IC NUMBER"].ToString();
                            if (ICNUMBER == "")
                            {
                                Empcode = dr["EMP ID"].ToString();
                            }
                            else
                            {
                                Empcode = null;
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


                            if (dr[dt.Columns[i].ToString()] == System.DBNull.Value)
                            {
                                A1 = 0;
                            }
                            else
                            {
                                A1 = Convert.ToDecimal(dr[dt.Columns[i].ToString()]);//A1 = dr["A1"].ToString();
                            }
                            #region converting A1 to mapped column id
                            string sql_emp = "SELECT Deductions_Id FROM [MapDeductions] where Company_id='" + comp_id + "' AND  MapVariable='" + dt.Columns[i].ToString() + "'";
                            SqlDataReader dr_addit = DataAccess.ExecuteReader(CommandType.Text, sql_emp, null);
                            if (dr_addit.Read())
                            {
                                if (dr_addit["Deductions_Id"] == System.DBNull.Value)
                                {
                                    Addition = 0;
                                }
                                else
                                {

                                    Addition = Convert.ToInt32(dr_addit["Deductions_Id"]);
                                }

                            }
                            #endregion

                            #region Convert the (Empcode)timecardId to Emp_code
                            string sql_timecard2empcode = " select emp_code from employee where time_card_no='" + Empcode + "'";
                            SqlDataReader dr_empcode_timecard2empcode = DataAccess.ExecuteReader(CommandType.Text, sql_timecard2empcode, null);
                            if (dr_empcode_timecard2empcode.HasRows)
                            {
                                if (dr_empcode_timecard2empcode.Read())
                                {
                                    Empcode1 = dr_empcode_timecard2empcode["emp_code"].ToString();
                                }
                            #endregion

                                //if (dr_addit.HasRows && Empcode!="")//if mapped then only insert
                                if (dr_addit.HasRows && Empcode != "" && Addition > 0 && A1 > 0)//if mapped then only insert
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
                                     SqlQuery.Append("INSERT INTO Temp_Emp_Deductions ([Emp_code],[trx_type],[trx_amount],[BulkDedInMonth]) VALUES(" + Empcode1 + ",'" + Addition + "','" + A1 + "','" + Convert.ToInt32(cmbMonth.SelectedValue) + "')");
                                
                                    //SqlQuery.Append("INSERT INTO Temp_Emp_Deductions ([Emp_code],[trx_type],[trx_amount],amount,[BulkDedInMonth]) VALUES(" + Empcode1 + ",'" +Addition + "','" + A1+","+A1 + "','" + Convert.ToInt32(cmbMonth.SelectedValue) + "')");
                                }
                            }
                        }
                    }
                }

                DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Deductions", null);
                DataAccess.FetchRS(CommandType.Text, SqlQuery.ToString(), null);
            }
            catch (Exception e)
            {
                DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Deductions", null);
                //ShowMessageBox("Error for the Employee:" + Empcode + " Msg-" + e.Message.ToString());
                _actionMessage = "Warning|Error for the Employee:"+ Empcode + " Msg-" + e.Message.ToString();
                ViewState["actionMessage"] = _actionMessage;

            }

        }
         //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
               
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/UploadAddDed/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                string SheetName = "Sheet1";//here enter sheet name        
                oledbconn.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + SheetName + "$]", oledbconn);
                OleDbDataAdapter oledbda = new OleDbDataAdapter();
                oledbda.SelectCommand = cmdSelect;
                oledbda.Fill(dt);
                oledbconn.Close();
                oledbda = null;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt;
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
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }

        #endregion
        //Toolbar End

    }
    public class MyTemplate : ITemplate
    {

        protected TextBox textBox;

        private string colname;
        private int value;
     
        public MyTemplate(string cName, int cValue)
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
    public class CheckTemplate : ITemplate
    {


        protected CheckBox boolValue;
        private string colname;

        public CheckTemplate(string cName)
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
