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
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;


using iTextSharp.text.pdf.fonts;
using iTextSharp.text.html.simpleparser;


using System.Diagnostics;
using System.Text.RegularExpressions;
using Ionic.Zip;
using System.Net;
namespace SMEPayroll.Reports
{
    public partial class pdfMerge : System.Web.UI.Page
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
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        //static DataSet dsFill;
        DataSet dsFill;
        DateTime dtpayrollprocess;

        int intblock = 0;
        string str = "";
        string[] cid;
        string strWF = "";
        string strEmpvisible = "";
        RadGrid grid;
        DataSet dsHeaderFill;
        int c = 0;
        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
        System.Collections.Generic.List<string> pdflist = new System.Collections.Generic.List<string>();
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //string sortname = string.Empty;


            //if (grid.MasterTableView.SortExpressions.Count != 0)
            //{
            //    sortname = grid.MasterTableView.SortExpressions[0].FieldName;

            //}

            //DataView view = new DataView();
            //DataTable table;
            //string strEmpCodes = "";
            ////Remove Data From Dataset\
            //if (Session["EmpCodes"] != null)
            //{
            //    strEmpCodes = Session["EmpCodes"].ToString();
            //}

            //if (strEmpCodes != "")
            //{
            //    DataSet dsFill = (DataSet)Session["dsFill"];
            //    if (!string.IsNullOrEmpty(sortname))
            //    {
            //        string or = "ASC";
            //        if (grid.MasterTableView.SortExpressions[0].SortOrder.ToString() == "Descending")
            //        {
            //            or = "DESC";

            //        }
            //        dsFill.Tables[0].DefaultView.Sort = sortname + " " + or;
            //    }
            //    view = dsFill.Tables[0].DefaultView;
            //    view.RowFilter = "Emp_Code IN(" + strEmpCodes + ")";
            //    table = view.ToTable();
            //}
            //else
            //{
            //    DataSet dsFill = (DataSet)Session["dsFill"];
            //    if (!string.IsNullOrEmpty(sortname))
            //    {
            //        string or = "ASC";
            //        if (grid.MasterTableView.SortExpressions[0].SortOrder.ToString() == "Descending")
            //        {
            //            or = "DESC";

            //        }

            //        dsFill.Tables[0].DefaultView.Sort = sortname + " " + or;
            //    }
            //    view = dsFill.Tables[0].DefaultView;
            //    table = view.ToTable();
            //}

            //string strColName = "";
            //string employee_code = "";
            //string employee_grid = "";
            //try
            //{
            //    employee_code = table.Rows[e.Item.ItemIndex]["Emp_Code"].ToString();
            //    employee_grid = ((DataRowView)e.Item.DataItem).Row.ItemArray[0].ToString();

            //}

            //catch (Exception ex) { }

            //if (e.Item is GridDataItem)
            //{
            //    int i = 3;

            //    foreach (DataColumn dc in table.Columns)
            //    {
            //        string templateColumnName = dc.ColumnName.ToString().ToUpper();

            //        if (templateColumnName != "EMP_CODE" && templateColumnName != "FULLNAME" && templateColumnName != "TIME_CARD_NO" && templateColumnName != "TRADE")
            //        {
            //            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "Select [Desc] Title From Deductions_Types Where ID=" + templateColumnName.ToString(), null);
            //            if (dr1.Read())
            //            {
            //                strColName = dr1[0].ToString();
            //            }
            //            dr1.Close();
            //        }
            //        if (strColName.ToString().Length > 0)
            //        {
            //            TextBox textBox = new TextBox();
            //            textBox.ID = "txt" + templateColumnName;
            //            textBox.DataBinding += new EventHandler(textBox_DataBinding1);
            //            textBox.Width = Unit.Pixel(30);

            //            try
            //            {
            //                if (table.Rows[e.Item.ItemIndex][dc].ToString() != "0.00")
            //                {

            //                    textBox.Text = table.Rows[e.Item.ItemIndex][dc].ToString();//dsFill.Tables[0].Rows[e.Item.ItemIndex][dc].ToString();


            //                }
            //            }
            //            catch (Exception ex) { }

            //            //Validation to check whether there is alreadydata --change color
            //            if (chkId.Checked)
            //            {
            //                #region Load the existing dataset to show data already exist
            //                string sSQL = "sp_bulkdedtranspose";
            //                SqlParameter[] parms = new SqlParameter[4];
            //                parms[0] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
            //                parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
            //                parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
            //                parms[3] = new SqlParameter("@DeptId", deptID.SelectedValue);
            //                DataSet dsFill_Exist = DataAccess.ExecuteSPDataSet(sSQL, parms);
            //                string col_exist = dc.ToString();

            //                DataView view1 = new DataView();
            //                DataTable table1;
            //                if (strEmpCodes != "")
            //                {

            //                    view1 = dsFill_Exist.Tables[0].DefaultView;
            //                    view1.RowFilter = "Emp_Code IN(" + strEmpCodes + ")";
            //                    view1.Sort = "FullName asc";
            //                    table1 = view1.ToTable();
            //                }
            //                else
            //                {
            //                    view1 = dsFill_Exist.Tables[0].DefaultView;
            //                    view1.Sort = "FullName asc";
            //                    table1 = view1.ToTable();
            //                }
            //                #endregion

            //                #region Change colour if already there is data[compare two table which has same structure and data]
            //                //ku

            //                if (e.Item.ItemIndex < table1.Rows.Count)
            //                {
            //                    try
            //                    {
            //                        if (table1.Rows[e.Item.ItemIndex][col_exist].ToString() != "")
            //                        {
            //                            textBox.BackColor = Color.LightYellow;

            //                            textBox.ToolTip = table1.Rows[e.Item.ItemIndex][col_exist].ToString();
            //                            textBox.ForeColor = Color.Red;

            //                        }
            //                    }
            //                    catch (Exception ex) { }

            //                }
            //                #endregion

            //            }
            //            //
            //            textBox.Attributes.Add("onkeypress", "return isNumericKeyStrokeDecimal(event);");

            //            GridDataItem item = e.Item as GridDataItem;

            //            string strc = "Column" + i.ToString();
            //            item[strc].Controls.Add(textBox);

            //            if (strColName.Length > 10)
            //            {
            //                this.grid.Columns[i].HeaderText = strColName.ToString().Substring(0, 10);
            //            }
            //            else
            //            {
            //                this.grid.Columns[i].HeaderText = strColName.ToString();
            //            }
            //            i++;
            //        }
            //        else
            //        {
            //            if (this.grid.Columns[i].HeaderText == "")
            //            {
            //                this.grid.Columns[i].Visible = false;
            //            }
            //        }
            //    }
            //}
        }

       void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem dataItem = (GridDataItem)e.Item;
            //    CheckBox chkBox = (CheckBox)dataItem["Employeement Contract"].Controls[0];
            //    for (int i = 3; i < grid.Columns.Count - 1; i++)
            //    {
            //        CheckBox chkBox1 = (CheckBox)dataItem[grid.Columns[i].HeaderText.ToString()].Controls[0];
            //        if (chkBox1.Checked)
            //        {
            //            chkBox1.Enabled = true;
            //        }

            //    }


            //}



        }

        protected void Page_Load(object sender, EventArgs e)
        {
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
              
            }
           
            if (!IsPostBack)
            {

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
            bindgrid();
        }
        protected void deptID_databound(object sender, EventArgs e)
        {
            //deptID.Items.Insert(0, new ListItem("ALL", "-1"));
           // deptID.SelectedValue = "-1";
        }

        protected void btnAdd_Types_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Payroll/SetupDeductionTypes.aspx?type=false", false);

        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string strUpdateDelSQL = "";
        //    StringBuilder strDeleteAddition = new StringBuilder();
        //    StringBuilder strInsertAddition = new StringBuilder();
        //    IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        //    MonthFill();
        //    try
        //    {
        //        foreach (GridItem item in this.grid.MasterTableView.Items)
        //        {
        //            if (item is GridItem)
        //            {
        //                GridDataItem dataItem = (GridDataItem)item;

        //                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

        //                if (chkBox.Checked == true)
        //                {
        //                    string strEmpCode = this.grid.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
        //                    double amount = 0;
        //                    strDeleteAddition.Append("(Emp_Code='" + strEmpCode + "' And BulkDedInMonth='" + Session["ROWID"].ToString() + "') Or ");

        //                    SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
        //                    string query = "SELECT top 40 ID,[desc],[used]  FROM   Deductions_Types   WHERE  ((Company_ID =" + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 ORDER  BY [desc]";
        //                    SqlCommand cmd = new SqlCommand(query, conn);
        //                    conn.Open();
        //                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //                    DataTable dtTable = new DataTable();
        //                    dtTable.Load(dr);
        //                    for (int i = 0; i < dtTable.Rows.Count; i++)
        //                    {

        //                        string strcol = "Column" + Convert.ToInt32(dtTable.Rows[i]["ID"]);
        //                        if (this.grid.Items[dataItem.ItemIndex][strcol].Controls.Count > 0)
        //                        {

        //                            amount = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).Text.ToString());
        //                            //if amount is null and already entered
        //                            double Tooltipval = Utility.ToDouble(((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).ToolTip.ToString());
        //                            if (amount == 0 && Tooltipval > 0)
        //                            {
        //                                amount = Tooltipval;
        //                            }
        //                            //
        //                            if (amount > 0)
        //                            {
        //                                string strtype = ((TextBox)this.grid.Items[dataItem.ItemIndex][strcol].FindControl(strcol)).ID.ToString().Replace("Column", "");
        //                                strInsertAddition.Append("Insert Into Emp_Deductions (trx_type,trx_amount,amount, trx_period, emp_code, status, BulkDedInMonth) values ('" + strtype + "'," + amount.ToString() + "," + amount.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + strEmpCode + "','U','" + Session["ROWID"].ToString() + "')");
        //                            }
        //                        }

        //                    }

        //                }
        //            }
        //        }


        //        //validation 
        //        if (strDeleteAddition.ToString().Length == 0)
        //        {
        //            ShowMessageBox("Please Select the Employees to Update");
        //        }
        //        //

        //        if (strDeleteAddition.ToString().Length > 0)
        //        {
        //            strUpdateDelSQL = "Delete From Emp_Deductions Where (" + strDeleteAddition + " 1=0)";
        //            int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
        //        }
        //        if (strInsertAddition.ToString().Length > 0)
        //        {
        //            strUpdateDelSQL = "" + strInsertAddition + "";
        //            int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
        //            ShowMessageBox("Deductions Updated Successfully");
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        ShowMessageBox("Error in data " + err.Message.ToString());
        //    }
        //}

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
            DataTable dt2 = (DataTable)Session["dtc2"];
            string str = "";
            foreach (DataRow dr2 in dt2.Rows)
            {
                if (str == "")
                {
                    str = " document_name='" + dr2["document_name"].ToString()+"'";
                }
                else
                {
                    str = str + " or " + " document_name='" + dr2["document_name"].ToString()+"'";
                }

            }
           
          // string  sql = "SELECT Category_Name  FROM   DocumentCategory where company_Id=" + Convert.ToInt32(Session["Compid"]) + " and (" + str + ")";
            string sql = "SELECT distinct Document_Name  FROM   DocumentMappedToEmployee where " + str + "";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql);
            string cname = "";
            while (dr.Read())
            {
                if (cname == "")
                {
                    cname = "[" + dr["Document_Name"].ToString() + "]";
                }
                else
                {
                    cname = cname + "," + "[" + dr["Document_Name"].ToString() + "]";
                }

            }
            dsFill = new DataSet();
            string sSQL;
            //-------------

            dt2 = (DataTable)Session["dt2"];
            string str2 = "";
            foreach (DataRow dr2 in dt2.Rows)
            {
                if (str2 == "")
                {
                    str2 = dr2["emp_code"].ToString();
                }
                else
                {
                    str2 = str2 + "," + dr2["emp_code"].ToString();
                }

            }
          //  str2 = ViewState["enolist"].ToString();

            try
            {

                // sSQL ="select * from (SELECT emp_id,emp_name,filename FROM DocumentMappedToEmployee,employee where DocumentMappedToEmployee.Emp_ID=employee.emp_code ) s pivot(max(filename) for filename in("+cname +"))  p";
                //sSQL = "select * from (SELECT emp_code,ic_pp_number,emp_name,Category_Name FROM DocumentMappedToEmployee,employee,DocumentCategory where DocumentCategory.Company_Id=" + comp_id + " and DocumentMappedToEmployee.Emp_ID=employee.emp_code and DocumentMappedToEmployee.Category_ID=DocumentCategory.ID) s pivot(max(Category_Name) for Category_Name in(" + cname + "))  p";
               // sSQL = "select * from (SELECT emp_code,ic_pp_number,emp_name,Category_Name FROM DocumentMappedToEmployee,employee,DocumentCategory where DocumentCategory.Company_Id=" + comp_id + " and DocumentMappedToEmployee.Emp_ID=employee.emp_code and DocumentMappedToEmployee.Category_ID=DocumentCategory.ID) s pivot(max(Category_Name) for Category_Name in(" + cname + "))  p where emp_code in("+str2+")";
                sSQL = "select * from (SELECT emp_code,ic_pp_number,emp_name,Document_Name FROM DocumentMappedToEmployee,employee,DocumentCategory where (DocumentCategory.Company_Id=" + comp_id + " or DocumentCategory.Company_Id=-1) and DocumentMappedToEmployee.Emp_ID=employee.emp_code and DocumentMappedToEmployee.Category_ID=DocumentCategory.ID) s pivot(max(Document_Name) for Document_Name in (" + cname + "))  p where emp_code in(" + str2 + ")";
                dsFill = DataAccess.FetchRSDS(CommandType.Text, sSQL);

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                lblerror.ForeColor = System.Drawing.Color.Red;
                lblerror.Text = "Please Select Atlest One";
            }

            for (int i = 0; i < dsFill.Tables[0].Rows.Count; i++)
            {
                for (int j = 3; j < dsFill.Tables[0].Columns.Count; j++)
                {
                    if (dsFill.Tables[0].Rows[i][j] == null || dsFill.Tables[0].Rows[i][j].ToString().Length == 0)
                    {
                        //dsFill.Tables[0].Rows[i][j] = 0;
                        dsFill.Tables[0].Rows[i][j] = false ;
                    }
                    else
                    {
                        dsFill.Tables[0].Rows[i][j] = true ;
                    }
                }
            }
         //string test=   dsFill.Tables[0].Rows[0][3].ToString();
            return dsFill;
        }
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            //RadGrid rd = new RadGrid();
            //rd = ((RadGrid)source);
            //if (Utility.ToInteger(Session["s"]) == 1)
            //{
            //    string sSQL = "sp_bulkdedtranspose";
            //    SqlParameter[] parms = new SqlParameter[5];
            //    parms[0] = new SqlParameter("@compid", Utility.ToInteger(comp_id));
            //    parms[1] = new SqlParameter("@monthid", Utility.ToInteger(cmbMonth.SelectedItem.Value));
            //    parms[2] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedItem.Value));
            //    parms[3] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));//Senthil for GroupManagement
            //    parms[4] = new SqlParameter("@DeptId", deptID.SelectedValue);

            //    grid.CurrentPageIndex = e.NewPageIndex;
            //    dsFill = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            //    grid.DataSource = dsFill;
            //    grid.DataBind();

            //}

            //BuildTable();
            //grid.DataBind();
            //grid.CurrentPageIndex = e.NewPageIndex;
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
        protected void bindgrid()
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
           // grid.DataBind();

        }
           
        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            //    Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            //    bindMonth();
            //    Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        
      
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //if (Session["ROWID"] == null)
            //{
            //}
            //else
            //{
            //    if (intcnt == 1)
            //    {
            //        cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
            //        cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
            //    }
            //    else
            //    {
            //        if (IsPostBack == true)
            //        {
            //            MonthFill();
            //        }
            //        cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
            //        cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
            //        SetControlDate(cmbMonth.SelectedValue);
            //    }
            //}

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
            grid.PageSize = 50;

            grid.AlternatingItemStyle.Wrap = false;
            grid.Font.Size = 11;
            grid.GridExporting += RadGrid1_GridExporting;
            grid.ItemDataBound += RadGrid1_ItemDataBound;
            grid.Height = Unit.Percentage(80);
            grid.Width = Unit.Percentage(100);
            grid.EnableHeaderContextMenu = true;
            grid.HeaderContextMenu.ItemCreated += new RadMenuEventHandler(HeaderContextMenu_ItemCreated);

            string templateCheckBox = "SelectItem";
            GridClientSelectColumn templateCheckBoxColumn = new GridClientSelectColumn();
            templateCheckBoxColumn.UniqueName = "GridClientSelectColumn";
            templateCheckBoxColumn.HeaderText = templateCheckBox;
            templateCheckBoxColumn.HeaderStyle.Width = Unit.Pixel(100);
            grid.MasterTableView.Columns.Add(templateCheckBoxColumn);

            GridBoundColumn templateColumn1 = new GridBoundColumn();
            templateColumn1.DataField = "emp_code";
            templateColumn1.ShowFilterIcon = false;
            templateColumn1.AutoPostBackOnFilter = true;
            templateColumn1.CurrentFilterFunction = GridKnownFunction.StartsWith;
            templateColumn1.HeaderText = "emp_Code";
            templateColumn1.HeaderStyle.Width = Unit.Pixel(100);
            templateColumn1.Visible = false;
            grid.MasterTableView.Columns.Add(templateColumn1);

             templateColumn1 = new GridBoundColumn();
            templateColumn1.DataField = "ic_pp_number";
            templateColumn1.UniqueName = "ic_pp_number";
            templateColumn1.ShowFilterIcon = false;
            templateColumn1.AutoPostBackOnFilter = false ;
            templateColumn1.CurrentFilterFunction = GridKnownFunction.Contains;
            templateColumn1.HeaderText = "IC/PP No.";
            templateColumn1.HeaderStyle.Width = Unit.Pixel(100);
         
            grid.MasterTableView.Columns.Add(templateColumn1);

            GridBoundColumn templateColumn20 = new GridBoundColumn();
            templateColumn20.DataField = "emp_name";
            templateColumn20.ShowFilterIcon = false;
            templateColumn20.CurrentFilterFunction = GridKnownFunction.Contains;
            templateColumn20.AutoPostBackOnFilter = true;
            templateColumn20.HeaderText = "EmpName";
            templateColumn20.UniqueName = "emp_name";
            templateColumn20.HeaderStyle.Width = Unit.Pixel(200);
            grid.MasterTableView.Columns.Add(templateColumn20);

            //---murugan
            grid.ClientSettings.Resizing.ResizeGridOnColumnResize = true;
            grid.ClientSettings.Resizing.AllowColumnResize = true;
            grid.ClientSettings.Resizing.EnableRealTimeResize = true;

            DataTable dt2 = (DataTable)Session["dtc2"];
           
            foreach (DataRow dr2 in dt2.Rows)
            {
                if (str == "")
                {
                    str = " Document_Name='" + dr2["Document_Name"].ToString()+"'";
                }
                else
                {
                    str = str + " or " + " Document_Name='" + dr2["Document_Name"].ToString()+"'";
                }

            }

            SqlConnection conn = new SqlConnection(Constants.CONNECTION_STRING);
            // string query = "SELECT top 40 ID,[desc],[used]  FROM   Deductions_Types   WHERE  ((Company_ID = " + Convert.ToInt32(Session["Compid"]) + ")OR (upper(isShared)='YES' AND Company_ID!=-1 ))  AND Used = 1 ORDER  BY [desc]";
            //string query = "SELECT Category_Name  FROM   DocumentCategory where company_Id=" + Convert.ToInt32(Session["Compid"]) + " and (" + str + ")";
            string query = "SELECT distinct Document_Name  FROM   DocumentMappedToEmployee where " + str + "";
            //WHERE  Company_ID = " + Convert.ToInt32(Session["Compid"]);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            

            while (dr.Read())
            {
                //GridBoundColumn templateColumn11 = new GridBoundColumn();
                GridCheckBoxColumn templateColumn11 = new GridCheckBoxColumn();

                

                templateColumn11.DataField = Convert.ToString(dr["Document_Name"]);
                
                templateColumn11.DataType = typeof(System.Boolean);
                templateColumn11.ShowFilterIcon = false;
                //templateColumn11.CurrentFilterFunction = GridKnownFunction.Contains;
               // templateColumn11.AutoPostBackOnFilter = true;
                templateColumn11.AllowFiltering = false;
                templateColumn11.HeaderText = Convert.ToString(dr["Document_Name"]);
                templateColumn11.UniqueName = Convert.ToString(dr["Document_Name"]);
                templateColumn11.HeaderStyle.Width = Unit.Pixel(100);
                grid.MasterTableView.Columns.Add(templateColumn11);

            }



            //GridTemplateColumn tc = new GridTemplateColumn();
            //tc.UniqueName = "PrintTemplateColumn";
            //tc.AllowFiltering = false;
            //tc.ItemTemplate = new AddToItemTemplate();
            //tc.HeaderStyle.Width = Unit.Pixel(100);
            //tc.Visible = false;
            //grid.MasterTableView.Columns.Add(tc);

            //string templateCheckBox = "SelectItem";
            //GridClientSelectColumn templateCheckBoxColumn = new GridClientSelectColumn();
            //templateCheckBoxColumn.UniqueName = "GridClientSelectColumn";
            //templateCheckBoxColumn.HeaderText = templateCheckBox;
            //grid.MasterTableView.Columns.Add(templateCheckBoxColumn);
            PlaceHolder1.Controls.Add(grid);

        }


        public class AddToItemTemplate : ITemplate
        {

            public AddToItemTemplate() { }

            public void InstantiateIn(Control container)
            {
                ImageButton ib = new ImageButton();
                ib.ID = "imgprint";
                ib.CommandName = "Print";
                ib.ImageUrl = "../frames/images/toolbar/print.gif";
                container.Controls.Add(ib);
            }

            void ib_Command(object sender, CommandEventArgs e)
            {
                string s = e.CommandArgument.ToString();

            }


        }

         protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Print")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];
                int empid = Utility.ToInteger(id);

                //-------------------
                string str1 = "";
                cid = Request.QueryString[1].ToString().Split(',');
                foreach (string word in cid  )
                {
                    if (str1 == "")
                    {
                        str1=" id="+word;
                    }
                    else
                    {
                        str1 = str1 + " or id=" + word;
                    }
                    
                }
                str1 = "select filename from [DocumentMappedToEmployee] where emp_id=" + empid + " and (" + str1 + ")";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str1);
                System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
                while (dr.Read())
                {
                    list.Add("Documents/"+ comp_id  +"/"+ empid +"/Files/"+ dr[0].ToString());
                }


                
                // string[] lstFiles = new string[dr.];
                //lstFiles[0] = @"d:/pdf/1.pdf";
                //lstFiles[1] = @"d:/pdf/2.pdf";
                //lstFiles[2] = @"d:/pdf/3.pdf";

                PdfReader reader = null;
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage;
                string outputPdfPath = @"d:/pdf/new.pdf";


                sourceDocument = new Document();
                pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                //Open the output file
                sourceDocument.Open();

                try
                {
                   
                   
                    foreach (string  item in list)
                    {

                        int pages = get_pageCcount(item);

                        reader = new PdfReader(item);
                        //Add pages of current file
                        for (int i = 1; i <= pages; i++)
                        {
                            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                            pdfCopyProvider.AddPage(importedPage);
                        }

                        reader.Close();
                    }
                    //At the end save the output file
                    sourceDocument.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        void HeaderContextMenu_ItemCreated(object sender, RadMenuEventArgs e)
        {
            //if (e.Item.Value.Contains("GridClientSelectColumn") && e.Item.Controls.Count > 0)
            //    (e.Item.Controls[0] as CheckBox).Visible = false;
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
                    ShowMessageBox("Please Select File to be uploaded");
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
                    }
                }

            }
            lblerror.Text = strMsg;

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
                                string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                                SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr_empcode.Read())
                                {
                                    Empcode = dr_empcode["emp_code"].ToString();
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
                ShowMessageBox("Error for the Employee:" + Empcode + " Msg-" + e.Message.ToString());

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
        private int get_pageCcount(string file)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(file)))
            {
                Regex regex = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = regex.Matches(sr.ReadToEnd());

                return matches.Count;
            }
        }
        protected void btnPrintReport_Click(object sender, EventArgs e)
        {
            RadGrid rd = new RadGrid();
            rd = grid;
            string strempcode = "";
            string cid = "";
            string icno = "";
            string header = "";
            //list l1 = new List();
            
            //HttpContext.Current.Response.Buffer = true;
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.ClearContent();
            //HttpContext.Current.Response.ClearHeaders();
            //HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + "my_report.zip");
            //Response.ContentType = "application/zip";

            //using (ZipOutputStream zipStream = new ZipOutputStream(Response.OutputStream))
            //{
            c = 0;
            pdflist.Clear();
            string filepath = Server.MapPath("~\\documents\\" + comp_id);
            if (Directory.Exists(filepath))
            {
                foreach (string file in Directory.GetFiles(filepath))
                {
                    File.Delete(file);
                }
            }
                foreach (GridItem item in grid.MasterTableView.Items)
                {

                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                        strempcode = dataItem["emp_code"].Text.ToString();
                       icno = dataItem["ic_pp_number"].Text.ToString();
                       
                       
                        if (chkBox.Checked == true)
                        {
                            c = c + 1;
                            //----------
                            DataTable dt2 = (DataTable)Session["dtc2"];
                            string strh = "";
                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                // CheckBox c1=(CheckBox ) dataItem[dr2["Category_Name"].ToString()].Controls[0];
                                CheckBox c1 = (CheckBox)dataItem[dr2[0].ToString()].Controls[0];
                                if (c1.Checked)
                                {
                                    if (strh == "")
                                    {
                                        strh = " document_name='" + dr2["document_name"].ToString() + "'";
                                    }
                                    else
                                    {
                                        strh = strh + " or " + " document_name='" + dr2["document_name"].ToString() + "'";
                                    }

                                }
                            }

                           
                            byte[] f1 = pdf_merge(strempcode, strh);
                            //----------------------- add header 
                            header = "NAME : " + dataItem["emp_name"].Text.ToString() + "                 Fin No/Passport NO : " + icno;
                            
                          // f1=AddPageHeader(f1,header);

                            //----------------------

                            //System.IO.FileStream file = System.IO.File.Create(HttpContext.Current.Server.MapPath(".\\document\\temp\\"+c+".pdf "));
                           /// System.IO.FileStream file = System.IO.File.Create( Server.MapPath("~\\documents\\"+comp_id +"\\" + icno  + ".pdf "));
                          //  string[] Files = Directory.GetFiles("~\\");

                           // if(Files.Length>0)
                           // {
                           // }
                            
                            //System.IO.FileStream file = System.IO.File.Create(Server.MapPath("~\\" + icno + ".pdf "));
                            System.IO.FileStream file = System.IO.File.Create(Server.MapPath("~\\documents\\" + comp_id + "\\" + icno + ".pdf "));
                            pdflist.Add(icno);

                            file.Write(f1, 0, f1.Length);
                            file.Close();

                        }
                    }
                }
            //-----make header..
            //    PdfReader pr = new PdfReader(Server.MapPath("~\\documents\\temp\\" + pdflist[0].ToString() + ".pdf "));
            //PdfStamper ps=new PdfStamper(pr,new  FileOutputStream(Server.MapPath("~\\documents\\temp\\ok_" + pdflist[0].ToString() + ".pdf ")));
              


            //-------- all pdfs to zip


                using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
                {
                   
                   // for(int i=0;i<c;i++)
                   // {
                       // string filePath = Server.MapPath("~\\documents\\temp\\" + c + ".pdf ");
                           /// zip.AddFile(Server.MapPath("~\\documents\\"+ comp_id +"\\" + pdflist[i].ToString() + ".pdf "));
                       // zip.AddFile(Server.MapPath( "~\\"+pdflist[i].ToString() + ".pdf "));
                        
                       zip.AddSelectedFiles("*.pdf", Server.MapPath("~\\documents\\" + comp_id + "\\"), string.Empty, false);
                    ///zip.AddSelectedFiles("*.pdf", Server.MapPath("~\\"), string.Empty, false);
                   

                    Response.Buffer = false;
                   // Response.Clear();
                    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "Application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                   // HttpContext.Current.Response.BinaryWrite(arr);
                  //  zip.AddSelectedFiles("*", false);
                   
                    zip.Save(Response.OutputStream);

                   
                    
                  //  Response.Flush ();
                    Response.End();
                   
                    
                }
        }

                   
    protected byte[] pdf_merge(string empid,string str)
        {
            list.Clear();
           // str = str.Replace("id", "Category_ID");
          string   str1 = "select filename from [DocumentMappedToEmployee] where emp_id=" + empid + " and (" + str  + ")";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str1);
          //  System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            while (dr.Read())
            {
                list.Add(Server.MapPath("~\\Documents\\" + comp_id + "\\" + empid + "\\Files\\" + dr[0].ToString()));
               
            }
           
            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4,25,25,200,25))
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        
                        document.Open();
                        
                        
                       
                       
                       
                        //-------
                        for (int i = 0; i < list.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(list[i]);
                            //-------
                            //PdfWriter pw = PdfWriter.GetInstance(document, ms);
                            //PdfContentByte cb = pw.DirectContent;
                            //PdfImportedPage ip = pw.GetImportedPage(reader, 1);
                            //cb.AddTemplate(ip, 0, 0);
                            //cb.ShowText("testhead");

                           //--------------
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n; )
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));
                               
                            }
                        }
                       
                        
                    }
                  
                }

                mergedPdf = ms.ToArray();
            }
            //--------------------------
            //c = c + 1;
            
            return mergedPdf;
            
         
                
            
        }
        protected byte[] pdf_merge2(string empid, string str)
        {
            list.Clear();
            // str = str.Replace("id", "Category_ID");
            string str1 = "select emp_id,filename from [DocumentMappedToEmployee] where emp_id in(" + empid + ") and (" + str + ") order by emp_id";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str1);
            //  System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            while (dr.Read())
            {
                list.Add("\\Documents\\" + comp_id + "\\" + dr[0].ToString() + "\\Files\\" + dr[1].ToString());
            }

            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4, 25, 25, 200, 25))
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {

                        document.Open();

                        //-------
                        for (int i = 0; i < list.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(list[i]);
                           
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n; )
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));

                            }
                        }


                    }

                }

                mergedPdf = ms.ToArray();
            }
            //--------------------------
            //c = c + 1;

            return mergedPdf;
        }
        protected byte[] pdf_merge3(string empid, string str,string docs)
        {
            list.Clear();
            // str = str.Replace("id", "Category_ID");
            string str1 = "select filename from [DocumentMappedToEmployee] where emp_id=" + empid + " and (" + str + ")";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str1);
            //  System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
            while (dr.Read())
            {
                list.Add(Server.MapPath("~\\Documents\\" + comp_id + "\\" + empid + "\\Files\\" + dr[0].ToString()));
            }

            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4, 25, 25, 200, 25))
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        string ename = "";
                        string icno = "";
                        string desig = "";
                        document.Open();
                        //-----------get employee name & ic
                        string str2 = "select emp_name+' '+emp_lname emp_name, ic_pp_number,designation from employee,designation where emp_code=" + empid +" and designation.id=employee.desig_id";
                        SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, str2);
                        if (dr2.Read())
                        {
                            ename = dr2[0].ToString();
                            icno= dr2[1].ToString();
                            desig = dr2[2].ToString();
                        }

                        //---------------------------
                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            Document doc = new Document(PageSize.A4,20,20,20,20);
                            PdfWriter writer = PdfWriter.GetInstance(doc, ms1);
                            writer.CloseStream = false;
                            doc.Open();
                            doc.NewPage();

                            //-------
                            PdfPTable table = new PdfPTable(4);

                            string s = "Advanced & Best Construction Pte Ltd\nAdvanced & Best Engineering Pte Ltd\nAdvanced & Best Offshore Engineering Pte Ltd";
                            string s1 =  "2 Kallang Pudding, #05-11 Mactech Building, Singapore 349307\nMain:68372336/ 62237996 Fax:62204532";
                            PdfPCell cell1 = new PdfPCell(new Phrase(s, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED )));
                            cell1.Colspan = 3;
                            cell1.PaddingTop = 10;
                            cell1.Border = PdfPCell.NO_BORDER;
                            table.AddCell(cell1);

                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/anblogojpg.jpg"));
                            pic.Alignment = iTextSharp.text.Image.ALIGN_TOP;
                            cell1 = new PdfPCell(pic, true);
                            cell1.Rowspan = 2;
                            cell1.Border = PdfPCell.NO_BORDER;
                            cell1.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase(s1, FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell1.Colspan = 3;
                            
                            cell1.Border = PdfPCell.NO_BORDER;
                            table.AddCell(cell1);

                            doc.Add(table);
                            doc.Add(new Paragraph("              _________________________________________________________________________________", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)));
                            doc.Add(new Paragraph("\n\n"));


                            table = new PdfPTable(1);
                            cell1 = new PdfPCell(new Phrase("WORKER/EMPLOYEE NAME:", FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);


                            cell1 = new PdfPCell(new Phrase(ename, FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY ;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);
                            doc.Add(table);
                            doc.Add(new Paragraph("\n\n"));


                            table = new PdfPTable(1);
                            cell1 = new PdfPCell(new Phrase("WORKER/EMPLOYEE PERMIT:", FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);


                            cell1 = new PdfPCell(new Phrase(icno, FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);
                            doc.Add(table);
                            doc.Add(new Paragraph("\n\n"));

                            table = new PdfPTable(1);
                            cell1 = new PdfPCell(new Phrase("WORKER/EMPLOYEE TRADE:", FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);


                            cell1 = new PdfPCell(new Phrase(desig, FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);
                            doc.Add(table);
                            doc.Add(new Paragraph("\n"));

                            table = new PdfPTable(1);
                            cell1 = new PdfPCell(new Phrase("DOCUMENT DETAILS:", FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            cell1.BorderWidth =0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);

                            iTextSharp.text.Font zapfdingbats = new iTextSharp.text.Font();
                            Chunk bullet = new Chunk("\u2022", zapfdingbats);

                            string[] arr = docs.Split(',');
                            for (int i = 0; i < arr.Length; i++)
                            {
                                cell1 = new PdfPCell(new Phrase("   "+bullet+"  "+arr[i]));
                                cell1.BorderWidth = 0;
                                cell1.Padding = 2;
                                table.AddCell(cell1);
                            }
                            doc.Add(table);

                            copy.NewPage();
                            copy.Add(table);

                            //----------------
                            Paragraph p = new Paragraph("Workers Particulars");
                            

                            

                            //----------
                           //doc.Add(new Paragraph("MMMM  "));
                            
                            doc.Close();
                            writer.Close();
                            ms1.Seek(0, SeekOrigin.Begin);
                            PdfReader rd1 = new PdfReader(ms1);
                            for (int pageCounter = 1; pageCounter < rd1.NumberOfPages + 1; pageCounter++)
                            {
                                copy.AddPage(copy.GetImportedPage(rd1, pageCounter));
                            }   
                            rd1.Close();
                        }


                        
                      
                        

                        //-------
                        for (int i = 0; i < list.Count; ++i)
                        {
                            PdfReader reader = new PdfReader(list[i]);
                            
                            // loop over the pages in that document
                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n; )
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));

                            }
                        }
                       
                    }

                }

                mergedPdf = ms.ToArray();
            }
            //--------------------------
            //c = c + 1;

            return mergedPdf;




        }

        public static byte[] AddPageHeader(byte[] pdf ,string h1)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(pdf, 0, pdf.Length);
            // we create a reader for a certain document
            PdfReader reader = new PdfReader(pdf);
            // we retrieve the total number of pages
            int n = reader.NumberOfPages;
            // we retrieve the size of the first page
            iTextSharp.text.Rectangle psize = reader.GetPageSize(1);

            // step 1: creation of a document-object
            Document document = new Document(psize, 50, 50, 50, 50);
            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            // step 3: we open the document

            document.Open();
            // step 4: we add content
            PdfContentByte cb = writer.DirectContent;



            int p = 0;
           // --------------------------------------


            //-----------------------------------------
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                document.NewPage();
                p++;
                PdfImportedPage importedPage = writer.GetImportedPage(reader, page);
                

                cb.AddTemplate(importedPage, 0, 0);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                if (page != 1)
                {
                    ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase(h1, FontFactory.GetFont("Gabriola", 12, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.GRAY)), 50, 820, 0);
                   
                }
                else
                {
                    ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase("Workers Particulars", FontFactory.GetFont("Franklin Gothic Medium", 27, iTextSharp.text.Font.NORMAL , iTextSharp.text.BaseColor.BLACK)), 45, 550, 90);

                }
                ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase("___________________________________________________________________________", FontFactory.GetFont("Gabriola", 12, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.RED)), 50, 70, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase("Workers Particulars - Advanced & Best Group of Companies", FontFactory.GetFont("Gabriola", 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.GRAY)), 50, 50, 0);
                ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase(page.ToString(), FontFactory.GetFont("Gabriola", 8, iTextSharp.text.Font.ITALIC, iTextSharp.text.BaseColor.GRAY)), 550, 50, 0);
            }
            // step 5: we close the document

            document.Close();
            return ms.ToArray();
        }
        public static byte[] AddVPageHeader(byte[] pdf)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(pdf, 0, pdf.Length);
            // we create a reader for a certain document
            PdfReader reader = new PdfReader(pdf);
            // we retrieve the total number of pages
            int n = reader.NumberOfPages;
            // we retrieve the size of the first page
            iTextSharp.text.Rectangle psize = reader.GetPageSize(1);

            // step 1: creation of a document-object
            Document document = new Document(psize, 50, 50, 50, 50);
            // step 2: we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, ms);
            // step 3: we open the document

            document.Open();
            // step 4: we add content
            PdfContentByte cb = writer.DirectContent;
            
           
            // --------------------------------------


            //-----------------------------------------

            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                document.NewPage();
              
                PdfImportedPage importedPage = writer.GetImportedPage(reader, page);


                cb.AddTemplate(importedPage, 0, 0);

                BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                if (page == reader.NumberOfPages)
                {
                   
                    ColumnText.ShowTextAligned(cb, Element.ALIGN_TOP, new Phrase("Workers Particulars", FontFactory.GetFont("Franklin Gothic Medium", 27, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)), 45, 550, 90);

                }
                
            }
              
               
            // step 5: we close the document

            document.Close();
            return ms.ToArray();
        }

        protected void btnPrintReport2_Click(object sender, EventArgs e)
        {
            RadGrid rd = new RadGrid();
            rd = grid;
            string strempcode = "";
            string cid = "";
            string icno = "";
            string header = "";
            //list l1 = new List();

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + "my_report.pdf");
            HttpContext.Current.Response.ContentType = "Application/pdf";

            c = 0;
            string strh = "";
            string streno = "";
            pdflist.Clear();
            foreach (GridItem item in grid.MasterTableView.Items)
            {

                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    strempcode = dataItem["emp_code"].Text.ToString();
                    icno = dataItem["ic_pp_number"].Text.ToString();


                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                        if (streno == "")
                        {
                            streno = strempcode;
                        }
                        else
                        {
                            streno = streno + "," + strempcode;
                        }
                        //----------
                        DataTable dt2 = (DataTable)Session["dtc2"];
                        
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            // CheckBox c1=(CheckBox ) dataItem[dr2["Category_Name"].ToString()].Controls[0];
                            CheckBox c1 = (CheckBox)dataItem[dr2[2].ToString()].Controls[0];
                            if (c1.Checked)
                            {
                                if (strh == "")
                                {
                                    strh = " id=" + dr2["id"].ToString();
                                }
                                else
                                {
                                    strh = strh + " or " + " id=" + dr2["id"].ToString();
                                }

                            }
                        }



                    }
                }
            }

            byte[] f1 = pdf_merge2(streno, strh);

            //----------------------- add header 
            //header = "NAME : " + dataItem["emp_name"].Text.ToString() + "                 Fin No/Passport NO : " + icno;
            //f1 = AddPageHeader(f1, header);
            HttpContext.Current.Response.BinaryWrite(f1);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();

           
        }

        protected void btnPrintReport3_Click(object sender, EventArgs e)
        {
            RadGrid rd = new RadGrid();
            rd = grid;
            string strempcode = "";
            string cid = "";
            string icno = "";
            string header = "";
            string strsql = "";
            c = 0;
            
            string ename = "";
            string edate = "";
            string eno = "";
            DateTime dt;
            
            pdflist.Clear();

            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + "my_report.pdf");
            HttpContext.Current.Response.ContentType = "Application/pdf";


            foreach (GridItem item in grid.MasterTableView.Items)
            {

                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                    strempcode = dataItem["emp_code"].Text.ToString();
                    icno = dataItem["ic_pp_number"].Text.ToString();


                    if (chkBox.Checked == true)
                    {
                        c = c + 1;
                        //----------
                        DataTable dt2 = (DataTable)Session["dtc2"];
                        string strh = "";
                        string docs = "";
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            // CheckBox c1=(CheckBox ) dataItem[dr2["Category_Name"].ToString()].Controls[0];
                            CheckBox c1 = (CheckBox)dataItem[dr2[0].ToString()].Controls[0];
                            if (c1.Checked)
                            {
                                if (strh == "")
                                {
                                    strh = " document_name='" + dr2["document_name"].ToString()+"'";
                                    docs = dr2["document_name"].ToString();
                                }
                                else
                                {
                                    strh = strh + " or " + " document_name='" + dr2["document_name"].ToString()+"'";
                                    docs = docs + "," + dr2["document_name"].ToString();
                                }

                            }
                        }


                        byte[] f1 = pdf_merge3(strempcode, strh,docs);
                        //----------------------- add header 
                        header = "NAME : " + dataItem["emp_name"].Text.ToString() + "                 Fin No/Passport NO : " + icno;

                        f1 = AddPageHeader(f1, header);

                        //----------------------

                        //System.IO.FileStream file = System.IO.File.Create(HttpContext.Current.Server.MapPath(".\\document\\temp\\"+c+".pdf "));
                        System.IO.FileStream file = System.IO.File.Create(Server.MapPath("~\\documents\\"+ comp_id +"\\" + icno + ".pdf "));
                        pdflist.Add(icno);

                        file.Write(f1, 0, f1.Length);
                        file.Close();

                    }
                }
            }

            if (c == 0)
            {
                lblMsg.Text = "No Employees Selected";
                HttpContext.Current.Response.ClearHeaders();
                return ;
            }


            //-------- all merge to pdf
            byte[] mergedPdf = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document(PageSize.A4, 25, 25, 200, 25))
                {
                    using (PdfCopy copy = new PdfCopy(document, ms))
                    {
                        
                        document.Open();
                      
                        for (int i = 0; i < c; i++)
                        {
                            PdfReader reader = new PdfReader(Server.MapPath("~\\documents\\"+ comp_id +"\\" + pdflist[i].ToString() + ".pdf "));

                            int n = reader.NumberOfPages;
                            for (int page = 0; page < n; )
                            {
                                copy.AddPage(copy.GetImportedPage(reader, ++page));

                            }
                        }
                        //---------------- generate first details page
                        using (MemoryStream ms1 = new MemoryStream())
                        {
                            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
                            PdfWriter writer = PdfWriter.GetInstance(doc, ms1);
                            writer.CloseStream = false;
                            doc.Open();
                            doc.NewPage();

                            //-------
                            PdfPTable table = new PdfPTable(4);
                            
                            string s = "Advanced & Best Construction Pte Ltd\nAdvanced & Best Engineering Pte Ltd\nAdvanced & Best Offshore Engineering Pte Ltd";
                            string s1 = "2 Kallang Pudding, #05-11 Mactech Building, Singapore 349307\nMain:68372336/ 62237996 Fax:62204532";
                            PdfPCell cell1 = new PdfPCell(new Phrase(s, FontFactory.GetFont("Calibri", 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)));
                            cell1.Colspan = 3;
                            cell1.PaddingTop = 10;
                            cell1.Border = PdfPCell.NO_BORDER;
                            table.AddCell(cell1);

                            iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/anblogojpg.jpg"));
                            pic.Alignment = iTextSharp.text.Image.ALIGN_TOP;
                            cell1 = new PdfPCell(pic, true);
                            cell1.Rowspan = 2;
                            cell1.Border = PdfPCell.NO_BORDER;
                            cell1.VerticalAlignment = PdfPCell.ALIGN_TOP;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase(s1, FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                            cell1.Colspan = 3;

                            cell1.Border = PdfPCell.NO_BORDER;
                            table.AddCell(cell1);

                            doc.Add(table);
                            doc.Add(new Paragraph("              _________________________________________________________________________________", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.RED)));
                            doc.Add(new Paragraph("\n\n"));

                           // var color = System.Drawing.ColorTranslator.FromHtml("#FADBD8");
                            table = new PdfPTable(1);
                            cell1 = new PdfPCell(new Phrase("NUMBER OF WORKER/EMPLOYEE DETAILS ATTACHED:", FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);


                            cell1 = new PdfPCell(new Phrase(c.ToString(), FontFactory.GetFont("Calibri", 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                          //  cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#FADBD8"));
                            cell1.BorderWidth = 0;
                            cell1.Padding = 10;
                            table.AddCell(cell1);
                            doc.Add(table);
                            doc.Add(new Paragraph("\n\n"));

                            table = new PdfPTable(6);
                            float[] widths = new float[] { 25f, 130f, 50f, 40f, 40f, 40f };
                            table.SetWidths(widths);

                            cell1 = new PdfPCell(new Phrase("NO.", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                           // cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);
                            

                            cell1 = new PdfPCell(new Phrase("NAME", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase("IC/WP/EP", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase("ID EXPIRY \nDATE", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                            //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase("CSOC OR\nBCSS", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                           // cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);

                            cell1 = new PdfPCell(new Phrase("CSOC EXPIRY \nDATE", FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE )));
                           //cell1.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                            cell1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#F65F58"));
                            cell1.BorderWidth = 1;
                            cell1.Padding = 5;
                            table.AddCell(cell1);

                           

                            for(int i=0;i<pdflist.Count ;i++)
                            {
                               // strsql = "select emp_name+' '+emp_lname emp_name,wp_exp_date,emp_code from employee where ic_pp_number='" + pdflist[i].ToString() + "'";
                                strsql = "select emp_name+' '+emp_lname emp_name,EmployeeCertificate.ExpiryDate edate,emp_code from employee,EmployeeCertificate where ic_pp_number='" + pdflist[i].ToString() + "' AND (employee.emp_code=EmployeeCertificate.Emp_ID or employee.emp_code=-1) and EmployeeCertificate.CertificateCategoryID=(select id from CertificateCategory where Category_Name='WORK PERMIT' and (Company_Id=" + comp_id + " or Company_Id=-1))";
                                SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, strsql );
                                if (dr2.Read())
                                {
                                    ename = dr2[0].ToString();
                                    edate = dr2[1].ToString();
                                    eno = dr2[2].ToString();
                                }
                                else
                                {
                                    strsql = "select  emp_name+' '+emp_lname emp_namee from employee where ic_pp_number='" + pdflist[i].ToString() + "'";
                                    SqlDataReader dr33 = DataAccess.ExecuteReader(CommandType.Text, strsql);
                                    if (dr33.Read())
                                    {
                                        ename = dr33[0].ToString();
                                    }
                                    edate = "";
                                    eno = "0";

                                }

                                cell1 = new PdfPCell(new Phrase((i+1).ToString(), FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                
                                cell1.Padding = 5;
                                table.AddCell(cell1);

                                cell1 = new PdfPCell(new Phrase(ename , FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                cell1.Padding = 5;
                                table.AddCell(cell1);

                                cell1 = new PdfPCell(new Phrase(pdflist[i].ToString(), FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                cell1.Padding = 5;
                                table.AddCell(cell1);

                                if (edate == "")
                                {
                                    edate = "";
                                }
                                else{
                                    dt = Convert.ToDateTime(edate);
                                    edate = dt.ToString("dd/MM/yyyy");
                                }
                                
                                cell1 = new PdfPCell(new Phrase( edate, FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                cell1.Padding = 5;
                                table.AddCell(cell1);

                                strsql = "select Category_Name,ExpiryDate from CertificateCategory,EmployeeCertificate where Emp_ID="+eno+" and (Category_Name='CSOC' or Category_Name='BCSS') and  CertificateCategory.ID=EmployeeCertificate.CertificateCategoryID";
                                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, strsql);
                                if (dr3.Read())
                                {
                                    ename = dr3[0].ToString();
                                    if (ename == "CSOC")
                                    {
                                        dt = Convert.ToDateTime(dr3[1]);
                                        edate = dt.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        edate = "-";
                                    }
                                    
                                    
                                    
                                }
                                else
                                {
                                    ename = "-";
                                    edate= "-";
                                }
                               
                                                                

                                cell1 = new PdfPCell(new Phrase(ename, FontFactory.GetFont("Calibri", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                cell1.Padding = 5;
                                table.AddCell(cell1);

                                cell1 = new PdfPCell(new Phrase(edate, FontFactory.GetFont("Calibri", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                                cell1.BorderWidth = 1;
                                cell1.Padding = 5;
                                table.AddCell(cell1);


                               
                            }
                            doc.Add(table);
                          

                            doc.Close();
                            writer.Close();
                            ms1.Seek(0, SeekOrigin.Begin);
                            PdfReader rd1 = new PdfReader(ms1);
                            
                                copy.AddPage(copy.GetImportedPage(rd1, 1));
                          
                            rd1.Close();

                        }

                        //----------------

                    }

                }
                //--------------- add vertical header
                mergedPdf = AddVPageHeader(ms.ToArray());
                //-----------------------
               
                //mergedPdf = ms.ToArray();
            }

            HttpContext.Current.Response.BinaryWrite(mergedPdf);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
       
    }