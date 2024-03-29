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
    public partial class Girofile : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

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
           
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger( Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();

                DataSet ds_bank = new DataSet();
                sSQL = "SELECT [id], [desc],code FROM bank where Code Is Not Null and id in(Select distinct bank_id From girobanks)";
                ds_bank = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpbank.DataSource = ds_bank.Tables[0];
                drpbank.DataTextField = ds_bank.Tables[0].Columns["desc"].ColumnName.ToString();
                drpbank.DataValueField = ds_bank.Tables[0].Columns["id"].ColumnName.ToString();
                drpbank.DataBind();
                drpbank.Items.Insert(0, new ListItem("-select-", "-1"));
                //drpbank.Items.Insert(ds_bank.Tables[0].Rows.Count+1, new ListItem("ChkPro", "-5"));
                drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));

                //lblHash.Visible = false;
                //chkHash.Visible = false;
            }
        }

        private void bindMonth()
        {
            MonthFill();
        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {

            Session["ValueDate"] = Convert.ToDateTime(rddate.SelectedDate.Value.ToString());

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

            if (emp_list == "")
            {
                Response.Write("<SCRIPT language='Javascript'>alert('Please select at least one employee.');</SCRIPT>");
                return;
            }

            sFileName = "";
            SMEGiro sme = new SMEGiro();
            if (drpbank.SelectedValue == Utility.ToString(Session["OCBC"]))
            {                
               // SMEGiro sme = new SMEGiro();
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
            else if (drpbank.SelectedValue == Utility.ToString(Session["UOB"]))
            {
               // SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
               // SMEGiro sme = new SMEGiro();
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
               // SMEGiro sme = new SMEGiro();
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
                //sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                //sme.GenerateGiroFile_JPMorgan();
                sFileName = "../Documents/Girofiles/DBS/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["DB"]))
            {
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
                //SMEGiro sme = new SMEGiro();
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
            //new 
            else if (drpbank.SelectedValue == Utility.ToString(Session["DBS_DISK"]))
            {
                //SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\DBS_Disk\\";
                //sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.LogFileName = "DPAY2.txt";
                string res= sme.GenerateGiroFile_DBS_Diskete();
               
                sFileName = "../Documents/Girofiles/DBS_Disk/" + sme.LogFileName;
                if (res == "File Succesfully created!")
                {
                    //showing information file
                    Response.Write("<SCRIPT language='Javascript'>window.open('../Documents/Girofiles/DBS_Disk/Information.txt');</SCRIPT>");
                }
                else//showing error message
                {
                   // Response.Write("<SCRIPT language='Javascript'>alert('" + res + "');</SCRIPT>");
                    ShowMessageBox(res);
                    return;
                }
            }
            else if(drpbank.SelectedValue =="17" )//bnp paribas
            {
               
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BNP\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BNP();
                sFileName = "../Documents/Girofiles/BNP/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == "13")//Bank Of America
            {

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BNP\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BOA();
                sFileName = "../Documents/Girofiles/BNP/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == "28")// JPMorgan -- added by Su Mon
            {
                //SMEGiro sme = new SMEGiro();
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\JPMorgan\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                sme.GenerateGiroFile_JPMorgan();
                sFileName = "../Documents/Girofiles/JPMorgan/" + sme.LogFileName;
            }

            
            #region calling the log file
            try
            {
                //delete the old file
                string patch_log = @"..\\Documents\\GiroFiles\\Log.txt";
                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_log);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }


                SMEGiro sme1 = new SMEGiro();
                sme1.CompanyID = (short)compid;
                sme1.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme1.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme1.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme1.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme1.SenderAccountNo = drpaccno.Text;
                sme1.sEmployeeList = emp_list;
                sme1.LogFilePath = @"..\\Documents\\GiroFiles\\";
                sme1.LogFileName = "Log.txt";
                sme1.GenerateLog();
                Response.Write("<SCRIPT language='Javascript'>window.open('../Documents/Girofiles/Log.txt');</SCRIPT>");
            }
            catch (Exception err)
            {

            }
            #endregion
            


            if (sFileName == "")
            {
                Response.Write("<SCRIPT language='Javascript'>alert('The feature is not supported for the selected bank.');</SCRIPT>");
                return;
            }
            else
            {                
                Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
            }
        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            string cmbMonthstr = cmbMonth.SelectedValue.ToString();
            string strcmbYear = cmbYear.SelectedValue.ToString();
            string strdrpbank = drpbank.SelectedValue.ToString();
            string strdrpaccno = drpaccno.SelectedValue.ToString();
            string strdrpValueDate = drpValueDate.SelectedValue.ToString();

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


        protected void drpbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_bankacc = new DataSet();
            sSQL = "SELECT bank_accountno FROM girobanks where company_id="+ compid +" and bank_id=" + drpbank.SelectedValue;
            ds_bankacc = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drpaccno.DataSource = ds_bankacc.Tables[0];
            drpaccno.DataTextField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();
            drpaccno.DataValueField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();          
            drpaccno.DataBind();
            drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));


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
