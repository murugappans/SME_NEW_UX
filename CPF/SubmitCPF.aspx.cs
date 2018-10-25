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
using System.IO;
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Text;
using AjaxControlToolkit.Design;
using System.Data.OleDb;
using Telerik.Web.UI;

namespace SMEPayroll.CPF
{
    public partial class SubmitCPF : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected ArrayList alCpf;
        protected int alRowCount = 0;        
        string _actionMessage = "";
        int compid;


        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
           
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            // SqlDataSource1.InsertCommandType = SqlDataSourceCommandType.StoredProcedure;
            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            string sSQL1 = "";// "sp_cpf_detail";

            if (chkCPFOld.Checked)
            {
                sSQL1 = "Sp_cpf_select_New";
             
            }
            else
            {
                sSQL1 = "sp_cpf_select";
            }


            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@companyid", Utility.ToInteger(compid));
            parms[3] = new SqlParameter("@csnno", Utility.ToString(cmbEmployerCPFAcctNumber.Value));

            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL1, parms);
          
            ////Modify The gross amount for CPF ...
            ////exec sp_GetPayrollMonth @ROWID=N'0',@YEARS=2012,@PAYTYPE=N'0'
            //SqlParameter[] parms = new SqlParameter[3];
            //parms[0] = new SqlParameter("@ROWID", Utility.ToInteger(0));
            //parms[1] = new SqlParameter("@YEARS", Utility.ToInteger(cmbYear.SelectedValue));
            //parms[2] = new SqlParameter("@PAYTYPE", Utility.ToInteger(0));

            //DataSet dsrowid = new DataSet();
            //dsrowid = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_GetPayrollMonth", parms);

            //int month = cmbMonth.SelectedValue;
            //int year    = cmbYear.SelectedValue;
            //int rowid =0;
            //foreach (DataRow dr1 in dsrowid.Tables[0].Rows)
            //{ 
            //    if(dr1["Month"].ToString()==month.ToString())
            //    {
            //        rowid = Convert.ToInt32(dr1["ROWID"].ToString());
            //    }
            //}

            ////Now for each employee call the exec "UIC"."dbo"."sp_new_payslip_Leave";1 72, 2012, 5, '380', 4       Month ,year,companyid,emp_id,4

            //foreach (DataRow dr1 in ds.Tables[0].Rows)
            //{
            //    //Modify The gross amount for CPF ...
            //    //exec sp_GetPayrollMonth @ROWID=N'0',@YEARS=2012,@PAYTYPE=N'0'
            //    SqlParameter[] parms = new SqlParameter[3];
            //    parms[0] = new SqlParameter("@ROWID", Utility.ToInteger(0));
            //    parms[1] = new SqlParameter("@YEARS", Utility.ToInteger(cmbYear.SelectedValue));
            //    parms[2] = new SqlParameter("@PAYTYPE", Utility.ToInteger(0));
            
            //}

            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
           
            if (IsPostBack)
            {
              
                RadGrid1.ExportSettings.FileName = "CPF-Details";
                if (check.Value == "true")
                {
                    check.Value = "";
                    WriteCPF_ESubmissions();
                }
                else
                {
                    FetchData();
                }
            }
            else
            {

                DataSet ds_CPF = new DataSet();
                ds_CPF = getDataSet("select ID, CSN From CPFFiles where company_id=" + compid);
                cmbEmployerCPFAcctNumber.DataSource = ds_CPF.Tables[0];
                cmbEmployerCPFAcctNumber.DataTextField = ds_CPF.Tables[0].Columns["CSN"].ColumnName.ToString();
                cmbEmployerCPFAcctNumber.DataValueField = ds_CPF.Tables[0].Columns["CSN"].ColumnName.ToString();
                cmbEmployerCPFAcctNumber.DataBind();

                if (Session["CurrentMonth"] != null)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["CurrentMonth"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["CurrentYear"]);
                }
                else
                {
                    cmbMonth.SelectedValue = Utility.ToString(System.DateTime.Today.Month-1);
                    cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                    ChangeYearMonth();
                }
            }

            if (!IsPostBack)
            {
                int k;
                string l = "01";
                for (k = 2; k < 100; k = k + 1)
                {
                    if (k < 10)
                    {
                        l = "0" + k.ToString();
                    }
                    else
                    {
                        l = k.ToString();
                    }
                    this.ADVICE_NO.Items.Add(new ListItem(l, l));
                    
                }
            }


        }

        protected void ChangeYearMonth()
        {
            if (Session["CurrentMonth"] == null)
            {
                Session["CurrentMonth"] = cmbMonth.SelectedItem.Value;
                Session["CurrentYear"] = cmbYear.SelectedItem.Value;
            }
            else
            {
                Session["CurrentMonth"] = cmbMonth.SelectedItem.Value;
                Session["CurrentYear"] = cmbYear.SelectedItem.Value;
            }
        }


        string strempNamedis="";
        private void WriteCPF_ESubmissions()
        {
            int rowCount = 0;
            //Bug ID: 1
            //Fix By: Santy Kumar
            //Date  : June 5th 2009
            //Remark: Fixed for the CPF 9 Digit and 10 Digit case. When 9 digit then add space in CPF File Generation
            string[] ArrCPFNo = cmbEmployerCPFAcctNumber.Value.ToString().Trim().Split('-');
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
                strCPFRefNo = cmbEmployerCPFAcctNumber.Value.ToString().Replace("-", "");
            }
            //End : 1  
            string empRefNo = "";
            string strFiller = " ";
            string stradviceCode = this.ADVICE_NO.SelectedValue.ToString();
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
            //string sFileName = Request.PhysicalApplicationPath + "Documents\\CPF\\" + sTemp;
            string sFileName = Server.MapPath(@"..\\Documents\\CPF\\" + sTemp);

            StreamWriter objWriter = new StreamWriter(sFileName, false);
            objWriter.Close();
            StreamWriter objStrWriter1 = new StreamWriter(sFileName, true);
            rowCount++;
            objStrWriter1.WriteLine("F " + strCPFRefNo + strFiller + stradviceCode + strdate + strtime + strfileid + strFillers);

            /* Part - B */
            if (Utility.ToDouble(txtTotalCPF.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "01" + padStr(ConvertDotToZero(Utility.ToString(txtTotalCPF.Value)), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtCPFLatePayment.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "07" + padStr(ConvertDotToZero(Utility.ToString(txtCPFLatePayment.Value)), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtFWL.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "08" + padStr(ConvertDotToZero(Utility.ToString(txtFWL.Value)), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtFWLLatePayment.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "09" + padStr(ConvertDotToZero(Utility.ToString(txtFWLLatePayment.Value)), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtSDL.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "11" + padStr(ConvertDotToZero(Utility.ToString(txtSDL.Value)), 11) + "0000000";
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            // With Donner Count
            if (Utility.ToDouble(txtDonationComChest.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "10" + padStr(ConvertDotToZero(Utility.ToString(txtDonationComChest.Value)), 11) + padStr(Utility.ToString(txtDCDonationComChest.Value), 6);
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtMBMF.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "02" + padStr(ConvertDotToZero(Utility.ToString(txtMBMF.Value)), 11) + padStr(Utility.ToString(txtDCMBMF.Value), 6);
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtSINDA.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "03" + padStr(ConvertDotToZero(Utility.ToString(txtSINDA.Value)), 11) + padStr(Utility.ToString(txtDCSINDA.Value), 6);
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtCDAC.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "04" + padStr(ConvertDotToZero(Utility.ToString(txtCDAC.Value)), 11) + padStr(Utility.ToString(txtDCCDAC.Value), 6);
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }

            if (Utility.ToDouble(txtECF.Value) > 0)
            {
                rowCount++;
                strfundtypewithcpfcontranddc = "05" + padStr(ConvertDotToZero(Utility.ToString(txtECF.Value)), 11) + padStr(Utility.ToString(txtDCECF.Value), 6);
                objStrWriter1.WriteLine("F0" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strfundtypewithcpfcontranddc + MaskString(" ", 103, true));
            }



            string sSQL1 = "sp_cpf_detail";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@companyid", Utility.ToInteger(compid));
            parms[3] = new SqlParameter("@csnno", Utility.ToString(cmbEmployerCPFAcctNumber.Value));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms);


            while (dr.Read())
            {
                i = 0;
                string strpaymentCode = "01";
                string strotherContribution = "00";
                string strempName = Utility.ToString(dr.GetValue(i++));
                // validating if the text is greater
                if (Convert.ToInt32(strempName.Length) > 78)
                {
                    strempNamedis = strempNamedis + strempName +",";
                }

                string strcpfAccount = Utility.ToString(dr.GetValue(i++));
                string strcpfAmount = ConvertDotToZero(Utility.ToString(dr.GetValue(i++)));
                string strcdac = Utility.ToString(dr.GetValue(i++));
                string strsinda = Utility.ToString(dr.GetValue(i++));
                string strmbf = Utility.ToString(dr.GetValue(i++));
                string strECF = Utility.ToString(dr.GetValue(i++));
                string fundAmount = Utility.ToString(dr.GetValue(8));
                i++;
                i++;
                //string fundAmount = Utility.ToString(dr.GetValue(i++));
                string strjoiningdateyear = Utility.ToString(dr.GetValue(i++));
                string strjoiningdatemonth = MaskNumber(Utility.ToDouble(Utility.ToString(dr.GetValue(i++))), 2, true);
                string strterminatedateyear = Utility.ToString(dr.GetValue(i++));
                string strterminatedatemonth = MaskNumber(Utility.ToDouble(Utility.ToString(dr.GetValue(i++))), 2, true);
                int accountLength = strcpfAccount.Length;
                int sLength = strempName.Length;
                string strempstatus = "";
                string strjoin = strjoiningdateyear + strjoiningdatemonth;
                string strterm = strterminatedateyear + strterminatedatemonth;
                string strjointerm = stryear + strmonth;

                if (strterm == "00")
                {
                    if (strjointerm == strjoin) //if not terminated yet and joined same month and year
                    {
                        strempstatus = "N";
                    }
                    else
                    {
                        strempstatus = "E";
                    }
                }
                else
                {
                    if (strjointerm == strjoin && strjointerm == strterm) //if joined same year and month and terminated same year and maonth
                    {
                        strempstatus = "O";
                    }
                    else
                    {
                        strempstatus = "L";
                    }
                }

                if (strterminatedateyear == null) 
                if (sLength > 22)
                    strempName = strempName.Substring(0, 22);


                if (accountLength > 9)
                    strcpfAccount = strcpfAccount.Substring(0, 9);

                strcpfAccount = MaskString(strcpfAccount, 9, false);

                strline = "F1" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strpaymentCode + strcpfAccount + padStr(strcpfAmount, 11) + "0000000000" + "0000000000" + strempstatus + strempName;

                if (strcdac == "-1.0000" && strsinda == "-1.0000" && strmbf == "-1.0000" && strECF == "-1.0000")
                {
                    rowCount++;
                    objStrWriter1.WriteLine(MaskString(strline, 150, false));
                }
                else
                {
                    rowCount++;
                    objStrWriter1.WriteLine(MaskString(strline, 150, false));
                    if (strcdac != "-1.0000" && strcdac != "0.0000")
                    {
                        strpaymentCode = "04";
                        strotherContribution = strcdac;
                    }
                    else if (strsinda != "-1.0000" && strsinda != "0.0000")
                    {
                        strpaymentCode = "03";
                        strotherContribution = strsinda;
                    }
                    else if (strmbf != "-1.0000" && strmbf != "0.0000")
                    {
                        strpaymentCode = "02";
                        strotherContribution = strmbf;
                    }
                    else if (strECF != "-1.0000" && strECF != "0.0000")
                    {
                        strpaymentCode = "05";
                        strotherContribution = strECF;
                    }
                    else
                    {
                        strpaymentCode = "01";
                    }
                   
                    strline = "F1" + strCPFRefNo + strFiller + stradviceCode + stryear + strmonth + strpaymentCode + strcpfAccount + padStr(ConvertDotToZero(strotherContribution), 11) + "0000000000" + "0000000000" + " " + strempName;
                    if ( Utility.ToDouble(fundAmount) > 0)
                    {
                        objStrWriter1.WriteLine(MaskString(strline, 150, false));
                        rowCount++;
                    }
                    //objStrWriter1.WriteLine(MaskString(strline, 150, false));
                }
            }

            if (strempNamedis.Length > 0)
            {
                //WarningLbkl.Text = "Warning !! Following name is too big " + strempNamedis;
                _actionMessage = "Warning|Following name is too big "+ strempNamedis;
                ViewState["actionMessage"] = _actionMessage;
            }

            rowCount++;
            if (rowCount > 3)
            {
                strline = "F9" + strCPFRefNo + strFiller + stradviceCode + padStr(Utility.ToString(rowCount), 6) + padStr(ConvertDotToZero(Utility.ToString(txtGrandTotal.Value)), 14);
                objStrWriter1.WriteLine(MaskString(strline, 150, false));
                sFileName = "../Documents/CPF/" + sTemp;
                Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
              
            }
            else
            {
                //Response.Write("<SCRIPT language='Javascript'>alert('');</SCRIPT>");
                _actionMessage = "Warning|No Records Exist for CPF Contributions.";
                ViewState["actionMessage"] = _actionMessage;
            }

            objStrWriter1.Close();
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            FetchData();
            RadGrid1.Visible = true;
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

        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            //RadGrid10.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            //RadGrid10.MasterTableView.GetColumn("EditColumn").Visible = false;
            //RadGrid10.MasterTableView.GetColumn("DeleteColumn").Visible = false;// UniqueName="DeleteColumn"
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
         
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid10_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportCPFHeader(Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), cmbMonth.SelectedItem.Text, cmbYear.SelectedItem.Text, e);

        }

        //protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    GridSettingsPersister objCount = new GridSettingsPersister();
        //    objCount.RowCount(e, tbRecord);
        //}


        #endregion

        private void FetchData()
        {
            RadGrid1.Visible = false;
            btnSave.Disabled = true;
            //btnPaybillBack.Disabled = true;
            string sSQL = "";

            if (chkCPFOld.Checked)
            {
                sSQL = "Sp_cpf1_New"; 
            }
            else
            {
                sSQL = "sp_CPF1";
            }

            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@month", Utility.ToInteger(cmbMonth.SelectedValue));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@companyid", Utility.ToInteger(compid));
            parms[3] = new SqlParameter("@csnno", Utility.ToString(cmbEmployerCPFAcctNumber.Value));
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            while (dr.Read())
            {
                if (Utility.ToDouble(dr.GetValue(11)) > 0)
                {
                    RadGrid1.Visible = true;
                    btnSave.Disabled = false;
                    tbRecord.Enabled = true;
                    //btnPaybillBack.Disabled = false;
                    excelbtn.Enabled = true;
                    wordbtn.Enabled = true;
                    pdfbtn.Enabled = true;
                }
                string totalCPF = Utility.ToString(dr.GetValue(0));
                txtTotalCPF.Value = totalCPF;               
                string sinda = Utility.ToString(dr.GetValue(1));
                txtSINDA.Value = sinda;
                txtHSINDA.Value = sinda;
                string sindaDC = Utility.ToString(dr.GetValue(2));
                txtHDCSINDA.Value = sindaDC;
                txtDCSINDA.Value = sindaDC;
                string cdac = Utility.ToString(dr.GetValue(3));
                txtCDAC.Value = cdac;
                txtHCDAC.Value = cdac;
                string cdacDC = Utility.ToString(dr.GetValue(4));
                txtDCCDAC.Value = cdacDC;
                txtHDCCDAC.Value = cdacDC;
                string mbmf = Utility.ToString(dr.GetValue(5));
                txtMBMF.Value = mbmf;
                txtHMBMF.Value = mbmf;
                string mbmfDC = Utility.ToString(dr.GetValue(6));
                txtDCMBMF.Value = mbmfDC;
                txtHDCMBMF.Value = mbmfDC;
                string ecf = Utility.ToString(dr.GetValue(7));
                txtECF.Value = ecf;
                txtHECF.Value = ecf;
                string ecfDC = Utility.ToString(dr.GetValue(8));
                txtDCECF.Value = ecfDC;
                txtHDCECF.Value = ecfDC;
                string cchest = Utility.ToString(dr.GetValue(9));
                txtDonationComChest.Value = cchest;
                txtHDonationComChest.Value = cchest;
                txtHECF.Value = ecf;
                string cchestDC = Utility.ToString(dr.GetValue(10));
                txtDCDonationComChest.Value = cchestDC;
                txtHDCDonantionComChest.Value = cchestDC;
                txtGrandTotal.Value = Utility.ToString(dr.GetValue(11));
                txtSDL.Value = Utility.ToString(dr.GetValue(14));
                //Check IF FWL needs to include or not

                string sqlFOWL = "SELECT FOWL FROM Company WHERE Company_Id=" + Utility.ToInteger(compid);

                SqlDataReader drfowl;
                drfowl = DataAccess.ExecuteReader(CommandType.Text, sqlFOWL, null);
                int flow = 0;
                while (drfowl.Read())
                {
                    if (drfowl[0].ToString() == "1")
                    {
                        flow = 1;
                    }
                }

                if (flow == 1)
                {
                    txtFWL.Value = Utility.ToString(dr.GetValue(15));
                }
                else
                {
                    txtFWL.Value = Utility.ToString(0);
                }

                if (chkFWL.Checked == false)
                {
                    txtFWL.Value = Utility.ToString(0);
                    txtGrandTotal.Value = Utility.ToDouble( Utility.ToDouble(dr.GetValue(11)) - Utility.ToDouble(dr.GetValue(15))).ToString();
                }
                else
                {
                    txtFWL.Value = Utility.ToString(dr.GetValue(15));
                }

                
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

        }

        public class cCPF
    {
        public string empID;
        public string empName;
        public string cpfAmount;
        public string cpfAccount;
        public string cDac;
        public string mBF;
        public string sinda;
        public string ecf;

    }

}
