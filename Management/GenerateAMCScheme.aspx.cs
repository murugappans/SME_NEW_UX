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
    public partial class GenerateAMCScheme : System.Web.UI.Page
    {
        string _actionMessage = "";
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
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            compid = Session["Compid"].ToString();
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
        }

        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                GridDataItem dtItem = e.Item as GridDataItem;

                strVar = (dataItem.Cells[12]).Text;
                if (strVar == "Variable Amount")
                {
                    ((TextBox)dataItem.Cells[13].Controls[1]).Enabled = true;
                }
                else
                {
                    ((TextBox)dataItem.Cells[13].Controls[1]).Enabled = false;
                }
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
                        // lblerror.Visible = true;
                        _actionMessage = "Warning|Payrol Is Not Generated for this Month.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                if (e.CommandName == "Submit")
                {
                    int i = 0;
                    int recsIns = 0;
                    double totamcs = 0;
                    string sqlQuery = null;
                    int status = 0;
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            string strVar = (dataItem.Cells[12]).Text;
                            totamcs = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AMCS_AMOUNT"));
                            if (totamcs >= 0)
                            {
                                string empid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EmpId"));
                                string empname = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMpName"));
                                string NRIC = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("NRIC"));
                                string OptionSelected = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("OptionSelected"));
                                string Formula = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Formula"));
                                string basicPay = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("basicPay"));
                                string netpay = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("netpay"));
                                string Total_gross = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Total_gross"));
                                string AmcsAmount = null;
                                if (strVar == "Variable Amount")
                                {
                                    AmcsAmount = ((TextBox)dataItem.Cells[13].Controls[1]).Text;
                                }
                                else
                                {
                                    AmcsAmount = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("AMCS_AMOUNT"));
                                }
                                string start_period = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("start_period"));
                                string end_period = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("end_period"));
                                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                                string sDate = Convert.ToDateTime(start_period.ToString()).ToString("dd/MM/yyyy", culture);
                                string eDate = Convert.ToDateTime(end_period.ToString()).ToString("dd/MM/yyyy", culture);
                                SqlParameter[] param = new SqlParameter[11];

                                param[0] = new SqlParameter("@EmpId", empid);
                                param[1] = new SqlParameter("@EMpName", empname);
                                param[2] = new SqlParameter("@NRIC", NRIC);
                                param[3] = new SqlParameter("@OptionSelected", OptionSelected);
                                param[4] = new SqlParameter("@Formula", Formula);
                                param[5] = new SqlParameter("@BasicPay", basicPay);
                                param[6] = new SqlParameter("@NetPay", netpay);
                                param[7] = new SqlParameter("@Total_gross", Total_gross);
                                param[8] = new SqlParameter("@Start_Period", sDate);
                                param[9] = new SqlParameter("@End_Period", eDate);
                                param[10] = new SqlParameter("@amcAmount", AmcsAmount);

                                SqlParameter[] para = new SqlParameter[5];
                                para[0] = new SqlParameter("@EmpId", empid);
                                para[1] = new SqlParameter("@Start_Period", sDate);
                                para[2] = new SqlParameter("@End_Period", eDate);
                                para[3] = new SqlParameter("@AMCSCSN", dlCSN.SelectedItem.Text.ToString());
                                para[4] = new SqlParameter("@Filter", Utility.ToString("1"));
                                sqlQuery = "sp_getIDAMCDetails ";

                                string existingId = "";
                                DataSet sqlds = DataAccess.FetchRS(CommandType.StoredProcedure, sqlQuery, para);
                                if (sqlds.Tables[0].Rows.Count > 0)
                                {
                                    sqlQuery = "delete from AMCDetails Where Id='" + sqlds.Tables[0].Rows[0][0].ToString() + "'";
                                    status = DataAccess.ExecuteNonQuery(sqlQuery, null);
                                }
                                sqlQuery = "Insert Into dbo.AMCDetails(EmpId,EMpName,NRIC,OptionSelected,Formula,BasicPay,NetPay,Total_gross,Start_Period,End_Period,AMCS_AMOUNT)Values(@EmpId,@EMpName,@NRIC,@OptionSelected,@Formula,@BasicPay,@NetPay,@Total_gross,@Start_Period,@End_Period,@amcAmount)";
                                status = DataAccess.ExecuteNonQuery(sqlQuery, param);
                                if (status > 0)
                                {
                                    recsIns += 1;
                                }
                            }
                        }
                    }

                    if (recsIns > 0)
                    {
                        //lblerror.Visible = true;
                        //lblerror.Text = recsIns.ToString() + " Records Inserted Successfully..";
                        _actionMessage = "Success|" + recsIns.ToString() + " Records Inserted Successfully";
                        ViewState["actionMessage"] = _actionMessage;
                        binddata();
                        //DataSet payDs = GenpayrollDetails;
                        //if (payDs.Tables[0].Rows.Count > 0)
                        //{
                        //    RadGrid1.DataSource = payDs;
                        //}
                    }
                    else
                    {
                        //lblerror.Visible = true;
                       // lblerror.Text = " Records Inserted Failed..";
                        _actionMessage = "Warning|Records Inserted Failed.";
                        ViewState["actionMessage"] = _actionMessage;
                    }

                }

                RadGrid1.DataBind();
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

            SqlParameter[] para = new SqlParameter[5];
            para[0] = new SqlParameter("@EmpId", Utility.ToString(""));
            para[1] = new SqlParameter("@Start_Period", sDate);
            para[2] = new SqlParameter("@End_Period", eDate);
            para[3] = new SqlParameter("@AMCSCSN", dlCSN.SelectedItem.Text.ToString());
            para[4] = new SqlParameter("@Filter", Utility.ToString("1"));
            string sqlQuery = "sp_getIDAMCDetails ";

            DataSet sqlds = DataAccess.FetchRS(CommandType.StoredProcedure, sqlQuery, para);
            if (Convert.ToInt16(sqlds.Tables[1].Rows[0][0]) > 0)
            {
                btnGenerate.Visible = true;
                lblCPFText.Visible = true;
                lblCPF.Visible = true;
                lblCPF.Text = Utility.ToString(sqlds.Tables[1].Rows[0][0]);
            }

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
                RadGrid1.DataSource = payDs;
                RadGrid1.DataBind();
                //lblerror.Text = "Payroll is not Generated";
                _actionMessage = "Warning|Payroll is not Generated.";
                ViewState["actionMessage"] = _actionMessage;

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
                string sSQL = "sp_GetComputeAMCDetails";
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@compId", Utility.ToInteger(Session["Compid"]));
                parms[1] = new SqlParameter("@type", "0");
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
                string sSQL = "sp_GetComputeAMCDetails";
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


    }
}
