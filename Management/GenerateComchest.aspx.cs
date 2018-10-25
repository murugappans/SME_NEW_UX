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
using System.Globalization;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

namespace SMEPayroll.Management
{
    public partial class GenerateAMCScheme_Employee : System.Web.UI.Page
    {
        string compid = "";
        SqlParameter[] parms;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        static string empname = "";
        static int varEmpCode;
        int PayrollType = 0;
        string sql = null;
        DataSet monthDs;
        DataSet empDs;
        int testX = 0;
        string sDate = null;
        string eDate = null;
        string strVar = null;
        int butclick = 0;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            //compid = Session["Compid"].ToString();Comnmented
            compid = Session["Compid"] != null ? Session["Compid"].ToString() : "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            lblerror.Text = "";

            SqlDataSource1.ConnectionString = Constants.CONNECTION_STRING;
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            empname = Session["Emp_Name"].ToString();
            varEmpCode = Utility.ToInteger(Session["EmpCode"]);

            int comp_id = Utility.ToInteger(Session["Compid"]);
            sql = "select PayrollType  from company where company_id = " + comp_id;
            PayrollType = DataAccess.ExecuteScalar(sql, null);
            if (!Page.IsPostBack)
            {
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();
                cmbMonth.SelectedValue = System.DateTime.Now.Month.ToString();


            }
            else {
                RadGrid1.Rebind();
            }
            
        }

        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            bindMonth();
        }
        private void bindMonth()
        {
            cmbMonth.DataSource = getMonthDetails.Tables[0];
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "Month";
            cmbMonth.DataBind();
            cmbMonth.SelectedValue = System.DateTime.Now.Month.ToString();

        }
        protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {

                GridSortExpression sortExpr = new GridSortExpression();

                sortExpr.FieldName = e.SortExpression;

               sortExpr.SortOrder = GridSortOrder.Ascending;
               


                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);

            }

            binddata();
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {

            foreach (GridDataItem item in RadGrid1.MasterTableView.Items)
            {
                if (strVar == "Variable Amount") // Check for null  
                {
                    GridCommandItem commandItem = (GridCommandItem)RadGrid1.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                    Button btn = (Button)commandItem.FindControl("btnsubmit");
                    btn.Enabled = false;
                    break;
                }
            }
            if (butclick == 0)
            {
                binddata();

            }
            butclick = 0;
        }

        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                GridDataItem dtItem = e.Item as GridDataItem;
                int i = e.Item.ItemIndex;
                strVar = (dataItem.Cells[6]).Text;
                string s = dataItem.Cells[11].Text;
                if (strVar == "Percentage")
                {
                    ((TextBox)dataItem.Cells[12].Controls[1]).Enabled = false;
                    
                
                }
                else
                {
                    ((TextBox)dataItem.Cells[12].Controls[1]).Enabled = true;
                }

            }
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string strEmpCode = item["EmpId"].Text.ToString();
                string tdate = "01/" + cmbMonth.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString();
                try
                {

                    string sSQL1 = "sp_GetPayrollProcessOn";
                    SqlParameter[] parms1 = new SqlParameter[3];
                    parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(strEmpCode));
                    parms1[1] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                    parms1[2] = new SqlParameter("@trxdate", tdate);
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
                        item.BackColor =System .Drawing.Color.LightGray;
                       // item["GridClientSelectColumn"].Controls[0].Visible = false;
                        ((TextBox)item.Cells[12].Controls[1]).Enabled = false;
                        ((TextBox)item.Cells[13].Controls[1]).Enabled = false;

                    }


                }
                catch (Exception ex) { }

            }


        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;

                if (e.CommandName == "Compute")
                {
                    DataSet sqlDs;
                    monthDs = this.getMonthDetails;
                    DataRow[] foundRows = monthDs.Tables[0].Select("Month = '" + cmbMonth.SelectedValue.ToString().Trim() + "' AND MonthName='" + cmbMonth.SelectedItem.Text.ToString().Trim() + "'");
                    foreach (DataRow dr in foundRows)
                    {
                        sDate = dr["PaySubStartDate"].ToString();
                        eDate = dr["PaySubEndDate"].ToString();
                    }
                    IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                    sDate = Convert.ToDateTime(sDate.ToString()).ToString("dd/MM/yyyy", culture);
                    // sDate = newTransDate.ToShortDateString();
                    sqlDs = ComputepayrollDetails;

                    if (sqlDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid1.DataSource = sqlDs;
                    }
                    else
                    {
                        //lblerror.Text = "Payrol Is Not Generated for this Month";
                        //lblerror.Visible = true;
                        _actionMessage = "Warning|Payrol Is Not Generated for this Month";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    //if (sqlDs.Tables[0].Rows.Count > 0)
                    //{
                    //    lblerror.Text = "Payrol wad already Generated for this Month";
                    //    lblerror.Visible = true;
                        
                    //}
                    //else
                    //{
                    //    RadGrid1.DataSource = sqlDs;
                       
                    //}
                }
                if (e.CommandName == "Submit")
                {
                    int i = 0;
                    int recsIns = 0;
                    double emplyee_amt = 0;
                    double emplyer_amt = 0;
                    string sqlQuery = null;
                    int status = 0;
                    int countrec = 0;
                    //----- insert into comchetdetails table
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        emplyee_amt = 0;
                        emplyer_amt = 0;
                        if (item is GridItem)
                        {

                            GridDataItem dataItem = (GridDataItem)item;
                             CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                             if (chkBox.Checked == true)
                             {
                                countrec++;
                                 string strVar = (dataItem.Cells[13]).Text;
                                 // totamcs = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ComchestAMOUNT"));

                                 TextBox txtepm = (TextBox)dataItem.Cells[12].Controls[1];
                                 TextBox txtepmr = (TextBox)dataItem.Cells[13].Controls[1];
                                 if (txtepm.Text != "")
                                 {
                                     emplyee_amt = Convert.ToDouble(((TextBox)dataItem.Cells[12].Controls[1]).Text);
                                 }
                                 if (txtepmr.Text != "")
                                 {
                                     emplyer_amt = Convert.ToDouble(((TextBox)dataItem.Cells[13].Controls[1]).Text);
                                 }


                                 TextBox etext = (TextBox)dataItem.Cells[13].Controls[1];

                                 if ((emplyee_amt > 0 || emplyer_amt > 0) && etext.Enabled)
                                 {
                                     string empid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmpId"));
                                     string empname = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMpName"));
                                     string NRIC = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("NRIC"));
                                     string OptionSelected = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OptionSelected"));
                                     string Formula = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Formula"));
                                     string basicPay = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("basicPay"));
                                     if (basicPay.Trim() == "")
                                     {
                                         basicPay = "0";
                                     }

                                     string start_period = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("start_period"));
                                     string end_period = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("end_period"));
                                     IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                                     string sDate = Convert.ToDateTime(start_period.ToString()).ToString("dd/MM/yyyy", culture);
                                     string eDate = Convert.ToDateTime(end_period.ToString()).ToString("dd/MM/yyyy", culture);


                                     sqlQuery = "delete from [ComchestDetails] Where EMPID='" + empid + "' and Start_Period='" + start_period + "' and End_Period='" + end_period + "'";
                                     status = DataAccess.ExecuteNonQuery(sqlQuery, null);

                                     sqlQuery = "Insert Into ComchestDetails(EmpId,EMpName,NRIC,OptionSelected,Formula,BasicPay,Start_Period,End_Period,employee_Amt,employer_Amt)Values('" + empid + "','" + empname + "','" + NRIC + "','" + OptionSelected + "','" + Formula + "'," + basicPay + ",'" + start_period + "','" + end_period + "'," + emplyee_amt + "," + emplyer_amt + ")";
                                     status = DataAccess.ExecuteNonQuery(sqlQuery, null);
                                     if (status == 1)
                                     {
                                         recsIns = recsIns + 1;
                                     }
                                 }
                             }

                            if (countrec == 0)
                            {
                                _actionMessage = "Warning|Selct Employee First..";
                                ViewState["actionMessage"] = _actionMessage;
                                return;


                            }
                            }
                        }


                        if (recsIns == 0)
                        {
                            return;
                        }
                    //{
                    //    lblerror.Visible = true;
                    //    lblerror.Text = recsIns.ToString() + " Records Inserted Successfully..";
                    //    binddata();
                    //    //DataSet payDs = GenpayrollDetails;
                    //    //if (payDs.Tables[0].Rows.Count > 0)
                    //    //{
                    //    //    RadGrid1.DataSource = payDs;
                    //    //}
                    //}
                    //else
                    //{
                    //    lblerror.Visible = true;
                    //    lblerror.Text = " Records Inserted Failed..";
                    //}

                    //--------end

                    //-------------coding for Comchest add to deduction
                    string ccAmt = null;
                    
                    string sFileName = "";
                    
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                           // CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                           // if (chkBox.Checked == true)
                           // {

                                int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmpId"));

                                TextBox txtepm = (TextBox)dataItem.Cells[12].Controls[1];
                                TextBox txtepmr = (TextBox)dataItem.Cells[13].Controls[1];

                                string strVar = (dataItem.Cells[11]).Text;
                               // strVar  =dataItem.Cells[5].Text;
                                if (strVar == "Variable Amount")
                                {

                                    ccAmt = ((TextBox)dataItem.Cells[12].Controls[1]).Text;
                                }
                                else
                                {
                                    ccAmt = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ComchestAMOUNT"));
                                }
                                string sql = @" select * from emp_deductions where emp_code='" + empid + "' and trx_period=(select top 1 PayStartDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "' and status='L') ";

                                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr.Read())
                                {


                                }
                                else {
                                    string sql1 = @" select * from emp_deductions where emp_code='" + empid + "' and trx_period=(select top 1 PayStartDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "' and status='U') ";

                                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);

                                    if (dr1.HasRows)
                                    {
                                       
                                        string sqldelete = "delete from emp_deductions where emp_code='" + empid + "' and trx_period=(select top 1 PayStartDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "') ";
                                        DataAccess.FetchRS(CommandType.Text, sqldelete, null);

                                    }

                                    //save the deduction in deduction table
                                    string sqlstring = "INSERT INTO [dbo].[emp_deductions]([trx_type],[trx_amount],[trx_period],[created_on],[created_by] ,[modified_on],[modified_by],[emp_code],[status],[BulkDedInMonth],[FundType],[CurrencyID],[ConversionOpt],[ExchangeRate],[amount]) VALUES  ";
                                    sqlstring += "((select id from deductions_types where [desc]='ComChest Fund')," + ccAmt + ",(select top 1 PayStartDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "'),(select top 1 PayEndDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "'),NULL,(select top 1 PayEndDate from  dbo.PayrollMonthlyDetail where Month='" + cmbMonth.SelectedValue.ToString() + "' and Year='" + cmbYear.SelectedValue.ToString() + "'),NULL,'" + empid + "','U',NULL,0,1,1,1,'" + ccAmt + "')";
                                    DataAccess.FetchRS(CommandType.Text, sqlstring, null);

                                // lblerror.Visible = true;
                                //lblerror.Text = " Records Submited Successfully..";
                                _actionMessage = "Success|Records Submited Successfully";
                                ViewState["actionMessage"] = _actionMessage;
                            }

                                

                            
                        }
                    }
                    //----------------------------end 

                }

               // RadGrid1.DataBind();
            }

        }
        protected void getData(object sender, ImageClickEventArgs e)
        {

            binddata();
        }
        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            binddata();
        }
        protected void binddata()
        {
            btnGenerate.Visible = false;
            lblCPFText.Visible = false;
            lblCPF.Visible = false;

            monthDs = this.getMonthDetails;
            DataRow[] foundRows = monthDs.Tables[0].Select("Month = '" + cmbMonth.SelectedValue.ToString().Trim() + "' AND MonthName='" + cmbMonth.SelectedItem.Text.ToString().Trim() + "'");
            foreach (DataRow dr in foundRows)
            {
                sDate = dr["PaySubStartDate"].ToString();
                eDate = dr["PaySubEndDate"].ToString();
            }
            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
            sDate = Convert.ToDateTime(sDate.ToString()).ToString("dd/MM/yyyy", culture);
            eDate = Convert.ToDateTime(eDate.ToString()).ToString("dd/MM/yyyy", culture);




            DataSet payDs = GenpayrollDetails;
            if (payDs.Tables[0].Rows.Count > 0)
            {
                lblerror.Visible = true;
                RadGrid1.DataSource = payDs;
                RadGrid1.DataBind();
            }
            else
            {
                lblerror.Visible = true;
               // RadGrid1.DataSource = payDs;
               // RadGrid1.DataBind();
                //lblerror.Text = "Payroll was already Generated";
            }
            //-------------
            SqlDataReader sdr;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string eid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmpId"));
                    string speriod = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("start_period"));
                    string eperiod = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("end_period"));
                    string str="select * from  ComchestDetails where  empid='"+ eid + "' and Start_Period='" + speriod + "' and End_Period='" + eperiod + "'";
                    sdr = DataAccess.ExecuteReader(CommandType.Text, str, null);
                    if (sdr.Read())
                    {
                        TextBox t1 = (TextBox)dataItem.Cells[12].Controls[1];
                        t1.Text = Convert.ToInt16(sdr["employee_Amt"]).ToString();
                        TextBox t2 = (TextBox)dataItem.Cells[13].Controls[1];
                        t2.Text = Convert.ToInt16(sdr["employer_Amt"]).ToString();
                    }
                    //------ For lastpayroll month comchest details

                    else {
                        str = "select top 1 start_period,end_period from  [prepare_payroll_hdr],[prepare_payroll_detail] where [prepare_payroll_hdr].trx_id=[prepare_payroll_detail].trx_id and status='G' order by start_period desc";
                        sdr = DataAccess.ExecuteReader(CommandType.Text, str, null);
                        if (sdr.Read())
                        {
                            DateTime d1 = Convert.ToDateTime(sdr[0]);
                            DateTime d2 = Convert.ToDateTime(sdr[1]);
                            speriod = d1.ToString("dd/M/yyyy");
                            eperiod = d2.ToString("yyyy-MM-dd");
                            str = "select * from  ComchestDetails where  empid='" + eid + "' and Start_Period='" + speriod + "' and End_Period='" + eperiod + "'";
                            sdr = DataAccess.ExecuteReader(CommandType.Text, str, null);
                            if (sdr.Read())
                            {
                                TextBox t1 = (TextBox)dataItem.Cells[12].Controls[1];
                                t1.Text = Convert.ToInt16(sdr["employee_Amt"]).ToString();
                                TextBox t2 = (TextBox)dataItem.Cells[13].Controls[1];
                                t2.Text = Convert.ToInt16(sdr["employer_Amt"]).ToString();
                            }
                        
                        }
                    }

                }
            }
                   

           
        }
        private DataSet getMonthDetails
        {
            get
            {
                string ssql = "sp_GetPayrollMonth";// 0,2009,2
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@ROWID", "0");
                parms[1] = new SqlParameter("@YEARS", cmbYear.SelectedValue.ToString().Trim());
                parms[2] = new SqlParameter("@PAYTYPE", PayrollType);
                DataSet ds = DataAccess.ExecuteSPDataSet(ssql, parms);
                return ds;

            }

        }
        private DataSet GenpayrollDetails
        {
            get
            {
                string sSQL = "sp_GetComputeAMCDetails_Employee";
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@compId", Utility.ToInteger(Session["Compid"]));
                parms[1] = new SqlParameter("@type", "1");
                parms[2] = new SqlParameter("@mid", dlCSN.SelectedValue.ToString().Trim());
                parms[3] = new SqlParameter("@sYear", cmbYear.SelectedValue.ToString());
                parms[4] = new SqlParameter("@sMonth", cmbMonth.SelectedValue.ToString());
                parms[5] = new SqlParameter("@AMCSCSN", dlCSN.SelectedItem.Text.ToString());
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                return ds;
            }
        }
        private DataSet ComputepayrollDetails
        {
            get
            {
                string sSQL = "sp_GetComputeAMCDetails_Employee";
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@compId", Utility.ToInteger(Session["Compid"]));
                parms[1] = new SqlParameter("@type", "1");
                parms[2] = new SqlParameter("@mid", dlCSN.SelectedValue.ToString().Trim());
                parms[3] = new SqlParameter("@sPeriod", sDate);
                parms[4] = new SqlParameter("@AMCSCSN", dlCSN.SelectedItem.Text.ToString());
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                return ds;
            }
        }
        protected void bindgrid(object sender, EventArgs e)
        {
            this.ViewState["IsfetchClicked"] = true;
            this.dlCSN.Enabled = false;
            this.cmbMonth.Enabled = false;
            this.cmbYear.Enabled = false;
            binddata();
        }

        private void WriteCPF_ESubmissions()
        {
            monthDs = this.getMonthDetails;
            DataRow[] foundRows = monthDs.Tables[0].Select("Month = '" + cmbMonth.SelectedValue.ToString().Trim() + "' AND MonthName='" + cmbMonth.SelectedItem.Text.ToString().Trim() + "'");
            foreach (DataRow drnew in foundRows)
            {
                sDate = drnew["PaySubStartDate"].ToString();
                eDate = drnew["PaySubEndDate"].ToString();
            }
            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
            sDate = Convert.ToDateTime(sDate.ToString()).ToString("dd/MM/yyyy", culture);
            eDate = Convert.ToDateTime(eDate.ToString()).ToString("dd/MM/yyyy", culture);



            int rowCount = 0;
            //Bug ID: 1
            //Fix By: Santy Kumar
            //Date  : June 5th 2009
            //Remark: Fixed for the CPF 9 Digit and 10 Digit case. When 9 digit then add space in CPF File Generation
            string[] ArrCPFNo = dlCSN.SelectedItem.Text.ToString().Trim().Split('-');
            string strCPFRefNo = "";
            string strCPFNo = "";
            if (ArrCPFNo.Length > 0)
            {
                strCPFNo = ArrCPFNo[0];
            }
            if (strCPFNo.Length == 9)
            {
                strCPFRefNo = strCPFNo.ToString().Trim() + " ";

                if (ArrCPFNo.Length >= 1)
                {
                    strCPFRefNo = strCPFRefNo + ArrCPFNo[1];
                }
                if (ArrCPFNo.Length >= 2)
                {
                    strCPFRefNo = strCPFRefNo + ArrCPFNo[2];
                }
            }
            else if (strCPFNo.Length != 9)
            {
                strCPFRefNo = dlCSN.SelectedItem.Text.ToString().Replace("-", "");
            }
            //End : 1  
            string empRefNo = "";
            string strFiller = " ";
            string stradviceCode = "01";
            string strdate = DateTime.Today.ToString("yyyyMMdd");
            string strtime = DateTime.Now.ToString("HHmmss");
            string strfileid = "FTP.DTL";
            string strFillers = MaskString(" ", 109, true);
            string strmonth = MaskNumber(Utility.ToDouble(cmbMonth.SelectedValue.ToString()), 2, true);
            string stryear = cmbYear.SelectedValue;
            string strfundtypewithcpfcontranddc = "";
            string strline = "";
            int i = 0;

            string sTemp = strCPFRefNo + ".DTL";
            string sFileName = Request.PhysicalApplicationPath + "Documents\\CPF\\" + sTemp;

            StreamWriter objWriter = new StreamWriter(sFileName, false);
            objWriter.Close();
            StreamWriter objStrWriter1 = new StreamWriter(sFileName, true);
            rowCount++;
            objStrWriter1.WriteLine("F " + strCPFRefNo + strFiller + stradviceCode + strdate + strtime + strfileid + strFillers);

            /* Part - B */
            if (Utility.ToDouble(lblCPF.Text.ToString()) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "01" + padStr(ConvertDotToZero(Utility.ToString(lblCPF.Text.ToString())), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@EmpId", Utility.ToString(""));
            para[1] = new SqlParameter("@Start_Period", sDate);
            para[2] = new SqlParameter("@End_Period", eDate);
            para[3] = new SqlParameter("@AMCSCSN", dlCSN.SelectedItem.Text.ToString());
            para[4] = new SqlParameter("@Filter", Utility.ToString("2"));
            string sqlQuery = "sp_getIDAMCDetails ";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sqlQuery, para);


            while (dr.Read())
            {
                rowCount++;
                i = 0;
                string strpaymentCode = "01";
                string strempName = Utility.ToString(dr.GetValue(2));
                string strcpfAccount = Utility.ToString(dr.GetValue(12));
                string strcpfAmount = ConvertDotToZero(Utility.ToString(dr.GetValue(11)));
                int accountLength = strcpfAccount.Length;
                string strempstatus = "E";

                if (strempName.Length > 22)
                    strempName = strempName.Substring(0, 22);


                if (accountLength > 9)
                    strcpfAccount = strcpfAccount.Substring(0, 9);

                strcpfAccount = MaskString(strcpfAccount, 9, false);

                strline = "F1" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strpaymentCode + strcpfAccount + padStr(strcpfAmount, 11) + "0000000000" + "0000000000" + strempstatus + strempName;
                objStrWriter1.WriteLine(MaskString(strline, 150, false));
                i++;
            }

            rowCount++;
            strline = "F9" + strCPFRefNo + strFiller + stradviceCode + padStr(Utility.ToString(rowCount), 6) + padStr(ConvertDotToZero(Utility.ToString(lblCPF.Text.ToString())), 14);
            objStrWriter1.WriteLine(MaskString(strline, 150, false));
            objStrWriter1.Close();

            sFileName = "../Documents/CPF/" + sTemp;
            Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");

        }

        //protected void btnCompute_click(object sender, EventArgs e)
        //{
        //    DataSet sqlDs;
        //    monthDs = this.getMonthDetails;
        //    DataRow[] foundRows = monthDs.Tables[0].Select("Month = '" + cmbMonth.SelectedValue.ToString().Trim() + "' AND MonthName='" + cmbMonth.SelectedItem.Text.ToString().Trim() + "'");
        //    foreach (DataRow dr in foundRows)
        //    {
        //        sDate = dr["PaySubStartDate"].ToString();
        //        eDate = dr["PaySubEndDate"].ToString();
        //    }
        //    IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
        //    sDate = Convert.ToDateTime(sDate.ToString()).ToString("dd/MM/yyyy", culture);
        //    // sDate = newTransDate.ToShortDateString();
        //    sqlDs = ComputepayrollDetails;
        //    if (sqlDs.Tables[0].Rows.Count > 0)
        //    {
        //        lblerror.Visible = false;
        //        btnGenerate.Enabled = true;

        //        RadGrid1.DataSource = ComputepayrollDetails;
        //        RadGrid1.DataBind();
        //    }
        //    else
        //    {
        //        lblerror.Text = "Payrol Is Not Generated for this Month";
        //        lblerror.Visible = true;
        //        btnGenerate.Enabled = false;
        //    }

        //}
        protected bool IsfetchClicked
        {
            get
            {
                return this.ViewState["IsfetchClicked"] != null;
            }
        }

        private string padStr(string strVal, int padNum)
        {
            string sTemp = "0";
            int strLen = strVal.Length;
            for (int i = 0; i < padNum - strLen; i++)
            {
                sTemp = sTemp + "0";
            }
            sTemp = sTemp + strVal;
            return sTemp;
        }

        private string ConvertDotToZero(string strVal)
        {
            if (strVal.IndexOf('.') == -1)
            {
                strVal = strVal + "00";
                return strVal;
            }
            else
            {
                int index = strVal.IndexOf('.');
                int len = strVal.Length;
                if ((index + 2) == len)
                    strVal = strVal + "0";
                if ((index + 3) < len)
                    strVal = strVal.Substring(0, (index + 3));
            }
            string temp1 = strVal.Substring(0, strVal.IndexOf('.'));
            string temp2 = strVal.Substring((strVal.IndexOf('.') + 1), 2);
            strVal = temp1 + temp2;
            return Utility.ToString(strVal);
        }


        public string MaskString(string sSrc, int iLength, bool atstart)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(sSrc); i++)
            {
                sTemp = sTemp + " ";
            }
            if (!atstart)
            {
                sSrc = sSrc + sTemp;
            }
            else
            {
                sSrc = sTemp + sSrc;
            }
            return sSrc;
        }

        public string MaskNumber(double iNumber, int iLength, bool atstart)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(iNumber.ToString()); i++)
            {
                sTemp = sTemp + "0";
            }
            if (!atstart)
            {
                return iNumber + sTemp;
            }
            else
            {
                return sTemp + iNumber;
            }
        }

        public static String Replace(String strText, String strFind, String strReplace)
        {
            int iPos = strText.IndexOf(strFind);
            String strReturn = "";
            while (iPos != -1)
            {
                strReturn += strText.Substring(0, iPos) + strReplace;
                strText = strText.Substring(iPos + strFind.Length);
                iPos = strText.IndexOf(strFind);
            }
            if (strText.Length > 0)
                strReturn += strText;
            return strReturn;
        }

        public string ToStringConv(object sParam)
        {
            string functionReturnValue = null;
            if (sParam == null)
            {
                functionReturnValue = "";
            }
            else
            {
                functionReturnValue = Replace(Strings.Trim(sParam.ToString()), "'", "''");
            }
            return functionReturnValue;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            WriteCPF_ESubmissions();
        }

        protected Boolean checkPaymode(string ecode, DateTime d1, DateTime d2)
        {

            //----MURUGAN
            //string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and  start_period between '" + d1.ToString("yyyy-MM-dd") + "' and  '" + d2.ToString("yyyy-MM-dd") + "')";
            string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id in (select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and ('" + d1.ToString("yyyy-MM-dd") + "' between start_period and end_period) or  ('" + d2.ToString("yyyy-MM-dd") + "'  between start_period and end_period))";
            // string strSql = "select * from prepare_payroll_detail p where emp_id=" + ecode + " and status !='R' and  trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and (MONTH(trx_date) = MONTH(getdate()) AND (YEAR(trx_date) = YEAR(getdate()))) and end_period >= '" + d1.ToString("yyyy-MM-dd") + "')";
            // string strSql = "select * from prepare_payroll_detail  where emp_id=" + ecode + " and status ='G'";
            /// string strSql = "select * from prepare_payroll_detail p  where emp_id="+ecode+" and status !='R' and trx_id=(select trx_id from prepare_payroll_hdr where p.trx_id=trx_id and   ( (MONTH(trx_date) = MONTH(getdate()) AND (YEAR(trx_date) = YEAR(getdate())))))";
            DataSet leaveset = new DataSet();
            leaveset = getDataSet(strSql);
            int temp = leaveset.Tables[0].Rows.Count;
            if (temp == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        
        protected void btnEmprAmt_Click(object sender, EventArgs e)
        {
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    
                    TextBox tbox = (TextBox)dataItem.Cells[13].Controls[1];
                    if (tbox.Enabled)
                    {
                        tbox.Text = txtEmprAmt.Text;
                    }
                    
                }
            }
            butclick = 1;
                   
        }
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }


        
     }
}
