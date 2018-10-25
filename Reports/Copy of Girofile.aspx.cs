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
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Drawing;
using System.IO;
using System.Text;

namespace SMEPayroll.Reports
{
    public partial class CopyGirofile : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        string sSQL = "";
        int intcnt;
        int compid = 0;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private object _dataItem = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger( Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            btnExport.Click += new EventHandler(btnExport_Click);
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            
            if (!IsPostBack)
            {
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text, "SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC");
                cmbYear.DataTextField = "id";
                cmbYear.DataValueField = "id";
                cmbYear.DataSource = ds;
                cmbYear.DataBind();

                cmbYear.SelectedValue = System.DateTime.Today.Year.ToString();

                bindMonth();
                radDtInput.SelectedDate = DateTime.Now.Date;                
                //DataSet ds_bank = new DataSet();
                //sSQL = "SELECT [id], [desc],code FROM bank where Code Is Not Null and id in(Select distinct bank_id From girobanks)";
                //ds_bank = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                //drpbank.DataSource = ds_bank.Tables[0];
                //drpbank.DataTextField = ds_bank.Tables[0].Columns["desc"].ColumnName.ToString();
                //drpbank.DataValueField = ds_bank.Tables[0].Columns["id"].ColumnName.ToString();
                //drpbank.DataBind();
                //drpbank.Items.Insert(0, new ListItem("-select-", "-1"));
                ////drpbank.Items.Insert(ds_bank.Tables[0].Rows.Count+1, new ListItem("ChkPro", "-5"));
                //drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));

                //lblHash.Visible = false;
                //chkHash.Visible = false;
            }
            RadGrid1.ExcelMLExportRowCreated += new Telerik.Web.UI.GridExcelBuilder.GridExcelMLExportRowCreatedEventHandler(RadGrid1_ExcelMLExportRowCreated);
            RadGrid1.GridExporting += new OnGridExportingEventHandler(RadGrid1_GridExporting);
        }

        void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            
        }

        void RadGrid1_ExcelMLExportRowCreated(object source, Telerik.Web.UI.GridExcelBuilder.GridExportExcelMLRowCreatedArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            e.Worksheet.Name = "Sheet1";
            
        }

        void btnExport_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");     
            //RadGrid1.ExportSettings.ExportOnlyData = true;
            //RadGrid1.ExportSettings.IgnorePaging = true;
            
            //Code
            //RadGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.ExcelML;
            //RadGrid1.ExportSettings.ExportOnlyData = true;
            //RadGrid1.ExportSettings.IgnorePaging = true;
            //RadGrid1.ExportSettings.OpenInNewWindow = true;
            //RadGrid1.MasterTableView.ExportToExcel();



            //RadGrid1.MasterTableView.ExportToExcel();

              //    @year       INT,    
              //    @month      INT,    
              //    @bank       INT,  
              //    @Date  Datetime  
            DataSet ds = new DataSet();

            SqlParameter[] parms = new SqlParameter[5]; // Parms = 86 (GENERAL), Parms = 93 (Clavon)
            parms[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));
            parms[1] = new SqlParameter("@year", Convert.ToInt32(cmbYear.SelectedValue.ToString()));
            parms[2] = new SqlParameter("@month", Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
            parms[3] = new SqlParameter("@bank", Convert.ToInt32(drpbank.SelectedValue));
            parms[4] = new SqlParameter("@Date", radDtInput.SelectedDate);


            ds = DataAccess.FetchRS(CommandType.StoredProcedure, "Sp_get_Cheque_CashInfo_Rpt", parms);

            DataTable data = new DataTable();
            data = ds.Tables[0];
            // render the DataGrid control to a file

            
            
            //string sTemp = "test.xls";
            //string strFileName = Request.PhysicalApplicationPath + "Documents\\CPF\\" + sTemp;

            //using (StreamWriter sw = new StreamWriter(strFileName))
            //{
            //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            //    {
            //        RadGrid1.RenderControl(hw);
            //    }
            //}


            HttpContext context = HttpContext.Current;


            context.Response.Clear();
            //   context.Response.Charset = System.Text.UTF8Encoding.UTF8.EncodingName.ToString();
            //   context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");

            context.Response.ContentType = "text/csv";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + "Sheet1" + ".csv");

            //rite column header names
            for (int i = 0; i < data.Columns.Count; i++)
            {
                if (i > 0)
                {
                    context.Response.Write(",");
                }
                context.Response.Write(data.Columns[i].ColumnName);
            }
            context.Response.Write(Environment.NewLine);

            //Write data
            foreach (DataRow row in data.Rows)
            {

                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        context.Response.Write(",");
                    }
                    context.Response.Write(row.ItemArray[i].ToString());
                }
                context.Response.Write(Environment.NewLine);
            }
            context.Response.End();






            
        }

        private void bindMonth()
        {
            MonthFill();
        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            string emp_list = "";
            string sFileName = "";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_id"));
                        emp_list = emp_list + empid + ",";
                    }
                }
            }

            //if (emp_list == "")
            //{
            //    Response.Write("<SCRIPT language='Javascript'>alert('Please select at least one employee.');</SCRIPT>");
            //    return;
            //}

            sFileName = "";
            if (drpbank.SelectedValue == Utility.ToString(Session["OCBC"]))
            {                
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\OCBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_OCBC();
                sFileName = "../Documents/Girofiles/OCBC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["HABIB"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.isHash = chkHash.Checked;
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\HABIB\\";
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + "01.txt";
                sme.GenerateGiroFile_HABIB();
                sFileName = "../Documents/Girofiles/HABIB/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["UOB"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.isHash = chkHash.Checked;
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\UOB\\";
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + "01.txt";
                sme.GenerateGiroFile_UOB();
                sFileName = "../Documents/Girofiles/UOB/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["FAREAST"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\FAREAST\\";
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + "01.txt";
                sme.GenerateGiroFile_UOB();
                sFileName = "../Documents/Girofiles/FAREAST/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["NOR"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\NORDEA\\";
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + "01.txt";
                sme.GenerateGiroFile_UOB();
                sFileName = "../Documents/Girofiles/NORDEA/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["DBS"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\DBS\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_DBS();
                sFileName = "../Documents/Girofiles/DBS/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["DB"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\Deutsche\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_Deutsche();
                sFileName = "../Documents/Girofiles/Deutsche/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["SC"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\SC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_SC();
                sFileName = "../Documents/Girofiles/SC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["MIZ"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\Mizuho\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_Mizuho();
                sFileName = "../Documents/Girofiles/Mizuho/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["SMBC"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\SMBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".csv";
                sme.GenerateGiroFile_SMBC();
                sFileName = "../Documents/Girofiles/SMBC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["CITI"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\CITI\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_CITI();
                sFileName = "../Documents/Girofiles/CITI/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["ABN"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\ABN\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_ABN();
                sFileName = "../Documents/Girofiles/ABN/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["HSBC"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\HSBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_HSBC();
                sFileName = "../Documents/Girofiles/HSBC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["MAY"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\MAY\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_MAY();
                sFileName = "../Documents/Girofiles/MAY/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["BTMU"]))
            {
                SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BTMU\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BTMU();
                sFileName = "../Documents/Girofiles/BTMU/" + sme.LogFileName;
            } 
            if (sFileName == "")
            {
                //Response.Write("<SCRIPT language='Javascript'>alert('The feature is not supported for the selected bank.');</SCRIPT>");
                _actionMessage = "Warning|The feature is not supported for the selected bank.";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }
            else
            {                
                Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
            }
        }
        protected void bindgrid(object sender, EventArgs e)
        {
            RadGrid1.DataBind();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
       
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }


        protected void drpbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds_bankacc = new DataSet();
            //sSQL = "SELECT bank_accountno FROM girobanks where company_id="+ compid +" and bank_id=" + drpbank.SelectedValue;
            //ds_bankacc = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            //drpaccno.DataSource = ds_bankacc.Tables[0];
            //drpaccno.DataTextField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();
            //drpaccno.DataValueField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();          
            //drpaccno.DataBind();
            //drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));
            RadGrid1.DataBind();

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

        void MonthFill()
        {
            CallBeforeMonthFill();
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
                foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            SetControlDate();
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
                    CallBeforeMonthFill();
                }
                else
                {
                    if (IsPostBack == true)
                    {
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                SetControlDate();
            }

            //if (drpbank.SelectedValue == Utility.ToString(Session["UOB"]))
            //{
            //    //lblHash.Visible = true;
            //    chkHash.Visible = true;
            //}
            //else
            //{
            //    //lblHash.Visible = false;
            //    chkHash.Visible = false;
            //}
        }

        void SetControlDate()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
            }
        }

    }
}
