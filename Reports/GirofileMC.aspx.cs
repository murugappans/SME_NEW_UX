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
    public partial class GirofileMC : System.Web.UI.Page
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

            RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
        }

        void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //DropDownList ddlEmp = (DropDownList)item["Employee"].FindControl("drpEmp");
                string paymentType = item["PaymentType"].Text;
                string currencyBank = item["CurrencyBank"].Text;
                string paymentPart  = item["PaymentPart"].Text;
                string empid        = item["emp_id"].Text;
                string SD = item["SD"].Text;
                string ED = item["ED"].Text;
                string processDate = item["created_on"].Text;
                DateTime createdOn=new DateTime();
                if(processDate!="")
                {
                    createdOn = Convert.ToDateTime(processDate);
                }
                
                /// Go and get the currency converion
                if (paymentType == "1")
                {
                    //Payment Part 1-ALL,2-Basic,3-Other
                    if (paymentPart == "2")
                    { 
                        //Get actual Basic for this employee
                        string strBasic="SELECT created_on,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') as [Full_Name],DeptName as [Department],convert(nvarchar(10),start_period,103) As [Start_Period],";
		                       strBasic=strBasic +"convert(nvarchar(10),end_period,103) As [End_Period],   Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) ";
		                       strBasic=strBasic +"As [Basic Pay], cpfAdd_Ordinary As [cpfAdd_Ordinary] , cpfAdd_Additional As [cpfAdd_Additional] , cpfAmount As [cpfAmount] ,";
                               strBasic=strBasic +"cpfNet As [cpfNet] , empCPF As [empCPF] , employerCPF As [employerCPF] , fund_amount As [fund_amount] , fund_type As [fund_type] , FWL As [FWL] , ";
		                       strBasic=strBasic + "Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))) As [Net Pay], OT1_e As [OT1_e] , OT1_wh As [OT1_wh] , ";
		                       strBasic=strBasic + "OT1Rate As [OT1Rate] , OT2_e As [OT2_e] , OT2_wh As [OT2_wh] , OT2Rate As [OT2Rate] , SDL As [SDL] , total_additions As [total_additions] ,";
		                       strBasic=strBasic +"total_deductions As [total_deductions] , total_gross As [total_gross] , unpaid_leaves_amount As [unpaid_leaves_amount] , unpaid_leaves As [unpaid_leaves] , ";
		                       strBasic=strBasic +"Wdays As [Wdays] from PayRollView1  ";
                               strBasic = strBasic + "where emp_code in (" + empid + ")and Convert(Datetime,start_period,103) between Convert(Datetime,'" + SD + "',103) And Convert(Datetime,'" + ED  +"',103)";
                               strBasic = strBasic + "and Convert(Datetime,end_period,103) between Convert(Datetime,'" + SD + "',103) And Convert(Datetime,'" +ED + "',103) ";
		                       strBasic=strBasic + "And STATUS in ('G')";
		                       strBasic=strBasic + "GROUP BY  emp_code,basic_pay, cpfAdd_Ordinary, cpfAdd_Additional, cpfAmount, cpfNet, empCPF, employerCPF, created_on,";
		                       strBasic=strBasic + "fund_amount, fund_type, FWL, NetPay, OT1_e, OT1_wh, OT1Rate, OT2_e, OT2_wh, OT2Rate, SDL,";
		                       strBasic=strBasic + "total_additions, total_deductions, total_gross, unpaid_leaves_amount, unpaid_leaves, Wdays,EMP_NAME,EMP_lname,DeptName,start_period,end_period ";
		                       strBasic=strBasic + "ORDER BY EMP_NAME,DeptName,start_period";

                               double basicpay = 0.0;
                               double upaidleaves = 0.0;
                               double deductions = 0.0;
                               DataSet dsBasic = DataAccess.FetchRS(CommandType.Text,strBasic,null);  
                                
                                //Conversion Rate
                                DateTime dtmax=new DateTime();
			                    string sqler="Select  top 1 [Date]   from ( Select  *  from dbo.ExchangeRate Where company_id ";
                                sqler=sqler +" IN ( Select Company_id from employee  Where emp_code ="+ empid  +")";
                                sqler = sqler + " and ([Date] <='" + createdOn.Month.ToString() + "/" + createdOn.Day.ToString() + "/" + createdOn.Year.ToString() + "')  )A order by A.[date] desc";
                    			 
                                SqlDataReader drdate = DataAccess.ExecuteReader(CommandType.Text,sqler,null);
                                while(drdate.Read())
                                {
                                    if(drdate.GetValue(0)!=null)
                                    {
                                        dtmax =Convert.ToDateTime(drdate.GetValue(0).ToString());
                                    }
                                }

                                string excrate ="Select  Rate  from dbo.ExchangeRate Where company_id ";
			                    excrate = excrate + " IN ( Select Company_id from employee  Where emp_code =" +empid  + ")";
                                excrate = excrate + " and ([Date] ='" + dtmax.Month.ToString() + "/" + dtmax.Day.ToString() + "/" + dtmax.Year.ToString() + "') and Currency_id=" + currencyBank + "  order by [date] desc";

                                double exRate = 0.0;

                                SqlDataReader drRate = DataAccess.ExecuteReader(CommandType.Text, excrate, null);
                                while (drRate.Read())
                                {
                                    if (drRate.GetValue(0) != null)
                                    {
                                        exRate = Convert.ToDouble(drRate.GetValue(0).ToString());
                                    }
                                }

                                if (dsBasic != null)
                                {
                                    if (dsBasic.Tables.Count > 0)
                                    {
                                        basicpay = Convert.ToDouble(dsBasic.Tables[0].Rows[0]["Basic Pay"].ToString());
                                        upaidleaves = Convert.ToDouble(dsBasic.Tables[0].Rows[0]["unpaid_leaves_amount"].ToString());
                                        deductions = Convert.ToDouble(item["total_deductions"].Text);
                                    }
                                }
                                basicpay = Convert.ToDouble(basicpay / exRate);
                                upaidleaves = Convert.ToDouble(upaidleaves / exRate);
                                //deductions = Convert.ToDouble(deductions / exRate);
                                basicpay = basicpay - upaidleaves - deductions;
                                
                                item["netpay"].Text = basicpay.ToString("0.00");
                                item["GrossPay"].Text = basicpay.ToString("0.00");

                                double payrate = Convert.ToDouble(item["payrate"].Text);
                                if (item["payrate"].Text != "")
                                {
                                    payrate=Convert.ToDouble(item["payrate"].Text) / exRate;
                                }
                                item["total_additions"].Text = "--";
                                //item["total_deductions"].Text = deductions.ToString();
                                item["payrate"].Text = payrate.ToString("0.00");
                                item["Percentage"].Text = "---";                        
                    }
                    //All
                    if (paymentPart == "1")
                    {
                        //item["Percentage"].Text = "";

                        //double netPay = Convert.ToDouble(item["netpay"].Text);
                        ////Conversion Rate
                        //DateTime dtmax = new DateTime();
                        //string sqler = "Select  top 1 [Date]   from ( Select  *  from dbo.ExchangeRate Where company_id ";
                        //sqler = sqler + " IN ( Select Company_id from employee  Where emp_code =" + empid + ")";
                        //sqler = sqler + " and ([Date] <='" + createdOn.Month.ToString() + "/" + createdOn.Day.ToString() + "/" + createdOn.Year.ToString() + "')  )A order by A.[date] desc";

                        //SqlDataReader drdate = DataAccess.ExecuteReader(CommandType.Text, sqler, null);
                        //while (drdate.Read())
                        //{
                        //    if (drdate.GetValue(0) != null)
                        //    {
                        //        dtmax = Convert.ToDateTime(drdate.GetValue(0).ToString());
                        //    }
                        //}

                        //string excrate = "Select  Rate  from dbo.ExchangeRate Where company_id ";
                        //excrate = excrate + " IN ( Select Company_id from employee  Where emp_code =" + empid + ")";
                        //excrate = excrate + " and ([Date] ='" + dtmax.Month.ToString() + "/" + dtmax.Day.ToString() + "/" + dtmax.Year.ToString() + "') and Currency_id=" + currencyBank + "  order by [date] desc";

                        //double exRate = 0.0;

                        //SqlDataReader drRate = DataAccess.ExecuteReader(CommandType.Text, excrate, null);
                        //while (drRate.Read())
                        //{
                        //    if (drRate.GetValue(0) != null)
                        //    {
                        //        exRate = Convert.ToDouble(drRate.GetValue(0).ToString());
                        //    }
                        //}
                        //netPay = Convert.ToDouble(netPay / exRate);
                        //item["netpay"].Text = netPay.ToString("0.00");
                        //item["GrossPay"].Text = netPay.ToString("0.00");

                        //double payrate = Convert.ToDouble(item["payrate"].Text);
                        //if (item["payrate"].Text != "")
                        //{
                        //    payrate = Convert.ToDouble(item["payrate"].Text) / exRate;
                        //}
                        ////item["total_additions"].Text = "---";
                        ////item["total_deductions"].Text = "---";
                        //item["payrate"].Text = payrate.ToString("0.00");
                        //item["Percentage"].Text = "---";       

                    }
                    //Others
                    if (paymentPart == "3")
                    {
                        double netPay = 0.0;
                        double grossPay = 0.0;
                        double additions = 0.0;
                        double deductions = 0.0;
                        if (item["total_additions"].Text != "")
                        {
                            additions = Convert.ToDouble(item["total_additions"].Text);
                        }
                        //if (item["total_deductions"].Text != "")
                        //{
                        //    deductions = Convert.ToDouble(item["total_deductions"].Text);
                        //}
                        //netPay = additions - deductions;
                        //if (netPay <= 0)
                        //{
                        //    netPay = 0;
                            
                        //}
                        grossPay = netPay;
                        item["Percentage"].Text = "";
                        item["netpay"].Text = additions.ToString();// item["total_additions"].Text;
                        item["GrossPay"].Text = additions.ToString();// item["total_additions"].Text;
                        item["payrate"].Text = "---";
                        item["total_deductions"].Text = "---";
                    }
                }
            }

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

            if (emp_list == "")
            {
                Response.Write("<SCRIPT language='Javascript'>alert('Please select at least one employee.');</SCRIPT>");
                return;
            }

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

                //Get Data For Basic Salary ...
                DataSet dsInfor = new DataSet();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("EmpId");
                dt1.Columns.Add("Basic");

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("EmpId");
                dt2.Columns.Add("Currency");

                dsInfor.Tables.Add(dt1);
                dsInfor.Tables.Add(dt2);

                double netpay = 0.0;
                string currency = "";
                int currId = 0;
                int empid = 0;
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;

                        System.Web.UI.WebControls.CheckBox chkBox = (System.Web.UI.WebControls.CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {

                            if (Utility.ToInteger(dataItem["PaymentType"].Text) == 1)
                            {
                                //Basic //All
                                if (Utility.ToInteger(dataItem["PaymentPart"].Text) == 2 || Utility.ToInteger(dataItem["PaymentPart"].Text) == 1)
                                {
                                    empid  =  Convert.ToInt32(dataItem["emp_id"].Text);
                                    netpay =  Utility.ToDouble(dataItem["GrossPay"].Text);
                                    currId =  Utility.ToInteger(dataItem["CurrencyBank"].Text);//CurrencyBank

                                    string cuusql = "Select Currency From Currency Where Id=" + currId;
                                    SqlDataReader dr ;
                                    dr = DataAccess.ExecuteReader(CommandType.Text, cuusql, null);
                                    while (dr.Read())
                                    { 
                                        if(dr.GetValue(0)!=null)
                                        {
                                            currency = Convert.ToString(dr.GetValue(0).ToString());
                                        }
                                    }

                                    DataRow dr2 = dsInfor.Tables[0].NewRow();
                                    dr2["EmpId"] = empid.ToString();
                                    dr2["Basic"] = netpay.ToString();
                                    dsInfor.Tables[0].Rows.Add(dr2);

                                    DataRow dr1 = dsInfor.Tables[1].NewRow();
                                    dr1["EmpId"] = empid.ToString();
                                    dr1["Currency"] = currency.ToString();
                                    dsInfor.Tables[1].Rows.Add(dr1);

                                }
                            }
                            //emp_list = emp_list + empid + ",";
                        }
                    }
                }
                string girobank = "SELECT currencyID  FROM girobanks where company_id=" + compid + " and bank_id=" + drpbank.SelectedValue + " and bank_accountno='" + drpaccno.SelectedValue +"'";
                
                SqlDataReader drbank = DataAccess.ExecuteReader(CommandType.Text, girobank, null);
                int currid = 0;
                while (drbank.Read())
                {
                    if (drbank.GetValue(0) != null)
                    {
                        currid = Convert.ToInt32(drbank.GetValue(0).ToString());
                    }
                }
                sme.GenerateGiroFile_CITI(dsInfor, currid);
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
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\CITI\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";

                //Get Data For Basic Salary ...
                DataSet dsInfor = new DataSet();
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("EmpId");
                dt1.Columns.Add("Basic");

                DataTable dt2 = new DataTable();
                dt2.Columns.Add("EmpId");
                dt2.Columns.Add("Currency");

                dsInfor.Tables.Add(dt1);
                dsInfor.Tables.Add(dt2);

                double netpay = 0.0;
                string currency = "";
                int currId = 0;
                int empid = 0;
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;

                        System.Web.UI.WebControls.CheckBox chkBox = (System.Web.UI.WebControls.CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {

                            if (Utility.ToInteger(dataItem["PaymentType"].Text) == 1)
                            {
                                //Basic //All
                                if (Utility.ToInteger(dataItem["PaymentPart"].Text) == 2 || Utility.ToInteger(dataItem["PaymentPart"].Text) == 1)
                                {
                                    empid = Convert.ToInt32(dataItem["emp_id"].Text);
                                    netpay = Utility.ToDouble(dataItem["GrossPay"].Text);
                                    currId = Utility.ToInteger(dataItem["CurrencyBank"].Text);//CurrencyBank

                                    string cuusql = "Select Currency From Currency Where Id=" + currId;
                                    SqlDataReader dr;
                                    dr = DataAccess.ExecuteReader(CommandType.Text, cuusql, null);
                                    while (dr.Read())
                                    {
                                        if (dr.GetValue(0) != null)
                                        {
                                            currency = Convert.ToString(dr.GetValue(0).ToString());
                                        }
                                    }

                                    DataRow dr2 = dsInfor.Tables[0].NewRow();
                                    dr2["EmpId"] = empid.ToString();
                                    dr2["Basic"] = netpay.ToString();
                                    dsInfor.Tables[0].Rows.Add(dr2);

                                    DataRow dr1 = dsInfor.Tables[1].NewRow();
                                    dr1["EmpId"] = empid.ToString();
                                    dr1["Currency"] = currency.ToString();
                                    dsInfor.Tables[1].Rows.Add(dr1);

                                }
                            }
                            //emp_list = emp_list + empid + ",";
                        }
                    }
                }
                string girobank = "SELECT currencyID  FROM girobanks where company_id=" + compid + " and bank_id=" + drpbank.SelectedValue + " and bank_accountno='" + drpaccno.SelectedValue + "'";

                SqlDataReader drbank = DataAccess.ExecuteReader(CommandType.Text, girobank, null);
                int currid = 0;
                while (drbank.Read())
                {
                    if (drbank.GetValue(0) != null)
                    {
                        currid = Convert.ToInt32(drbank.GetValue(0).ToString());
                    }
                }
                sme.GenerateGiroFile_HSBC(dsInfor, currid);
                sFileName = "../Documents/Girofiles/CITI/" + sme.LogFileName;

                /* old code */
                /*
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
                */
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

            //Get Currency of bank---

            string girobank = "SELECT currencyID  FROM girobanks where company_id=" + compid + " and bank_id=" + drpbank.SelectedValue + " and bank_accountno='" + drpaccno.SelectedValue+"'";

            SqlDataReader drbank = DataAccess.ExecuteReader(CommandType.Text, girobank, null);
            int currid = 0;
            while (drbank.Read())
            {
                if (drbank.GetValue(0) != null && drbank.GetValue(0).ToString()!="")
                {
                    currid = Convert.ToInt32(drbank.GetValue(0).ToString());
                }
            }

            //sSQL = "SELECT bank_accountno FROM girobanks where company_id="+ compid +" and bank_id=" + drpbank.SelectedValue;
            //SELECT currencyID  FROM girobanks
            //RadGrid1.DataBind();
            string sSQL = "sp_get_giro_emp_MC";
            SqlParameter[] parms = new SqlParameter[7];
            parms[0] = new SqlParameter("@company_id", compid);
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
            parms[3] = new SqlParameter("@bank", Utility.ToInteger(drpbank.SelectedValue));
            parms[4] = new SqlParameter("@bankaccno", Utility.ToString(drpaccno.SelectedValue));
            parms[5] = new SqlParameter("@valuedate", Utility.ToInteger(drpValueDate.SelectedValue));
            parms[6] = new SqlParameter("@currency1", Utility.ToInteger(currid));
            
            DataSet dsGiro  = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            
            //Change the Value for

            if (dsGiro.Tables.Count > 0)
            {
                RadGrid1.DataSource = dsGiro.Tables[0];
                RadGrid1.DataBind();
            }
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
