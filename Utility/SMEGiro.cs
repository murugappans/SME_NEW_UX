using System;
using System.Data;
using Microsoft.VisualBasic;
using System.Text;
using System.IO;
using System.Web;
using System.Data.SqlClient;
using System.Globalization;
using System.Xml;

namespace SMEPayroll
{
    public class SMEGiro
    {
        public int SalMonth;
        public int SalYear;
        public int CompanyID;
        public int GiroBankID;
        public string BatchNo;
        public int SenderBank;
        public string SenderAccountNo;
        public string LogFileName;
        public string LogFilePath;
        public string sEmployeeList;
        public bool StillExecuting;
        public bool isHash = false;
        public int ValueDate;
        public DateTime dtJPMorgan;
        public bool ISnewformate = false;
        public bool ISg3format = false;
        private int noofdays;
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        #region Deleted
        //private string GetReceivingAccountName(int EmployeeID, int intMask)
        //{
        //    string functionReturnValue = null;
        //    string sSQL = null;
        //    string sRetVal = null;
        //    DataSet ds = new DataSet();
        //    sSQL = "SELECT giro_acc_name FROM employee WHERE emp_code = " + EmployeeID;
        //    ds = GetDataSet(sSQL);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["giro_acc_name"].ToString().Trim());

        //        if (functionReturnValue != "")
        //        {
        //            if (intMask > 0)
        //            {
        //                functionReturnValue = MaskString(functionReturnValue, intMask);
        //            }
        //        }
        //        else
        //        {
        //            functionReturnValue = "Account Name not found in the database.";
        //        }
        //    }
        //    return functionReturnValue;
        //}

        //private string GetReceivingAccountNo(int EmployeeID, int intMask)
        //{
        //    string functionReturnValue = null;
        //    string sSQL = null;
        //    string sRetVal = null;
        //    DataSet ds = new DataSet();
        //    sSQL = "SELECT giro_acct_number FROM employee WHERE emp_code = " + EmployeeID;
        //    ds = GetDataSet(sSQL);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["giro_acct_number"].ToString().Trim());

        //        if (functionReturnValue != "")
        //        {
        //            if (intMask > 0)
        //            {
        //                functionReturnValue = MaskString(functionReturnValue, intMask);
        //            }
        //        }
        //        else
        //        {
        //            functionReturnValue = "Account No not found in the database.";
        //        }
        //    }
        //    return functionReturnValue;
        //}

        //private string GetReceivingBranchNo(int EmployeeID, int intMask)
        //{
        //    string functionReturnValue = null;
        //    string sSQL = null;
        //    string sRetVal = null;
        //    DataSet ds = new DataSet();
        //    sSQL = "SELECT giro_branch FROM employee WHERE emp_code = " + EmployeeID;
        //    ds = GetDataSet(sSQL);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["giro_branch"].ToString().Trim());

        //        if (functionReturnValue != "")
        //        {
        //            if (intMask > 0)
        //            {
        //                functionReturnValue = MaskString(functionReturnValue, intMask);
        //            }
        //        }
        //        else
        //        {
        //            functionReturnValue = "Giro Branch No not found in the database.";
        //        }
        //    }
        //    return functionReturnValue;
        //}

        //private string GetReceivingBankNo(int EmployeeID, int intMask)
        //{
        //    string functionReturnValue = null;
        //    string sSQL = null;
        //    string sRetVal = null;
        //    DataSet ds = new DataSet();
        //    sSQL = "SELECT giro_bank FROM employee WHERE emp_code = " + EmployeeID;
        //    ds = GetDataSet(sSQL);
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {

        //        functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["giro_bank"].ToString().Trim());

        //        if (functionReturnValue != "")
        //        {
        //            if (intMask > 0)
        //            {
        //                functionReturnValue = MaskString(functionReturnValue, intMask);
        //            }
        //        }
        //        else
        //        {
        //            functionReturnValue = "Bank Code not found in the database.";
        //        }
        //    }
        //    return functionReturnValue;
        //}
        #endregion Deleted

        private double GetAccountHash(string sReceivingAccountNo)
        {
            string sOrigLeft = null;
            string sOrigRight = null;
            string sRecLeft = null;
            string sReRight = null;
            double sA;
            double sB;

            string sOriginatingAccountNo = null;

            sOriginatingAccountNo = Replace(SenderAccountNo, "-", "");
            sReceivingAccountNo = Replace(sReceivingAccountNo, "-", "");

            sOriginatingAccountNo = sOriginatingAccountNo + Strings.Space(11 - Strings.Len(Strings.Left(sOriginatingAccountNo.ToString().Trim(), 11)));
            sReceivingAccountNo = sReceivingAccountNo + Strings.Space(11 - Strings.Len(Strings.Left(sReceivingAccountNo.ToString().Trim(), 11)));

            sOriginatingAccountNo = Replace(sOriginatingAccountNo, " ", "0");
            sReceivingAccountNo = Replace(sReceivingAccountNo, " ", "0");

            sOrigLeft = Strings.Left(sOriginatingAccountNo, 6);
            sOrigRight = Strings.Right(sOriginatingAccountNo, 5);

            sRecLeft = Strings.Left(sReceivingAccountNo, 6);
            sReRight = Strings.Right(sReceivingAccountNo, 5);

            sA = Utility.ToDouble(sRecLeft) - Utility.ToDouble(sReRight);
            sB = Utility.ToDouble(sOrigLeft) - Utility.ToDouble(sOrigRight);

            return System.Math.Abs(sA - sB);
        }

        private string GetOriginatingAccountNumber(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT bank_accountno FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["bank_accountno"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "Bank Account No not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string GetOriginatingBankCode(int intMask)
        {
            string functionReturnValue = null;
            string bankname = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT bank_code,[desc] FROM Bank Where id=" + SenderBank;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["bank_code"].ToString().Trim());
                bankname = ToStringConv(ds.Tables[0].Rows[0]["desc"].ToString().Trim());
                if (bankname == "DBS DISKETTE")
                {
                    functionReturnValue = "7171";
                }
                if (functionReturnValue != "")
                {
                    functionReturnValue = MaskNumber(Utility.ToDouble(functionReturnValue), 3);
                }
                else
                {
                    functionReturnValue = "Bank branch Code not found in the database.";
                }
            }
            return functionReturnValue;
        }

        //----------murugan
        private string GetOriginatingBICcode(string bank_code)
        {
            string functionReturnValue = null;
            // string bankname = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT BIC_code FROM Bank Where bank_code=" + bank_code;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["BIC_code"].ToString().Trim());
                return functionReturnValue;
            }
            else
            {
                return " ";
            }

        }
        private string GetOriginatingBankname(string bank_code)
        {
            string functionReturnValue = null;
            // string bankname = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT [desc] FROM Bank Where bank_code=" + bank_code;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["desc"].ToString().Trim());
                return functionReturnValue;
            }
            else
            {
                return " ";
            }

        }
        // ---------

        private string GetOriginatingBranchNumber(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT bank_branch FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["bank_branch"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    functionReturnValue = MaskNumber(Utility.ToInteger (functionReturnValue), 3);
                }
                else
                {
                    functionReturnValue = "Bank branch not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string GetOriginatorName(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT giro_acc_name FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["giro_acc_name"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "Originator Name not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string GetApproverCode(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT approvercode FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["approvercode"].ToString().Trim());
                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "Approver Code not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string GetOperatorCode(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT operatorcode FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["operatorcode"].ToString().Trim());
                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "Operator Code not found in the database.";
                }
            }
            return functionReturnValue;
        }



        private string GetSenderCompanyID(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT company_bankcode FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["company_bankcode"].ToString().Trim());
                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "GIRO Company ID not found in the database.";
                }
            }
            return functionReturnValue;
        }
        //----------murugan
        private string GetGirobranch(int ecode)
        {
            string functionReturnValue = null;
            string sSQL = null;

            DataSet ds = new DataSet();
            sSQL = "SELECT giro_branch  FROM employee  WHERE emp_code = " + ecode;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                functionReturnValue = ds.Tables[0].Rows[0]["giro_branch"].ToString().Trim();
            }
            else
            {
                functionReturnValue = "";
            }
            return functionReturnValue;
        }
        private string GetCompanyName(int intMask)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT company_name FROM company WHERE company_id = " + this.CompanyID;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["company_name"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    if (intMask > 0)
                    {
                        functionReturnValue = MaskString(functionReturnValue, intMask);
                    }
                }
                else
                {
                    functionReturnValue = "Company Name not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string GetformateDate(string strFormatFor, string stringdate)
        {
            string functionReturnValue = null;
            //string sSQL = null;
            //DataSet ds = new DataSet();
            //sSQL = "SELECT value_date FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            //ds = GetDataSet(sSQL);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //if (Strings.Len(ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString())) == 1)
            //{
            //    functionReturnValue = "0" + ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString().Trim());
            //}
            //else
            //{
            //   functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString().Trim());
            //}


            //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt16(functionReturnValue));

            DateTime dtt = Convert.ToDateTime(stringdate);
            DateTime dt = new DateTime(dtt.Year, dtt.Month, dtt.Day);



            if (strFormatFor == "1")
            {
                functionReturnValue = dt.ToString("yyMMdd");
            }
            else if (strFormatFor == "2")
            {
                functionReturnValue = dt.ToString("dd/MM/yyyy");
            }
            else if (strFormatFor == "3")
            {
                functionReturnValue = dt.ToString("yyyy-MM-dd");
            }
            else if (strFormatFor == "4")
            {
                functionReturnValue = dt.ToString("yyyyMMdd");
            }
            else if (strFormatFor == "5")
            {
                functionReturnValue = dt.ToString("ddMMyyyy");
            }
            else
            {
                functionReturnValue = dt.ToString("dd/MM/yyyy");
            }




            return functionReturnValue;
        }



        private string GetValueDate(string strFormatFor)
        {
            string functionReturnValue = null;
            //string sSQL = null;
            //DataSet ds = new DataSet();
            //sSQL = "SELECT value_date FROM girobanks WHERE bank_id = " + SenderBank + " AND bank_accountno = '" + SenderAccountNo + "' And Company_ID=" + CompanyID.ToString();
            //ds = GetDataSet(sSQL);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //if (Strings.Len(ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString())) == 1)
            //{
            //    functionReturnValue = "0" + ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString().Trim());
            //}
            //else
            //{
            //   functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["value_date"].ToString().Trim());
            //}
            if (Strings.Len(ToStringConv(this.ValueDate.ToString())) == 1)
            {
                functionReturnValue = "0" + ToStringConv(this.ValueDate.ToString().Trim());
            }
            else
            {
                functionReturnValue = ToStringConv(this.ValueDate.ToString().Trim());
            }

            if (functionReturnValue != "")
            {
                //DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt16(functionReturnValue));

                DateTime dtt = (DateTime)HttpContext.Current.Session["ValueDate"];
                DateTime dt = new DateTime(dtt.Year, dtt.Month, dtt.Day);

                if (dt.DayOfWeek == DayOfWeek.Friday)
                {
                    dt.AddDays(3);
                }
                else if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    dt.AddDays(3);
                }
                else
                {
                    dt.AddDays(1);
                }
                if (strFormatFor == "1")
                {
                    functionReturnValue = dt.ToString("yyMMdd");
                }
                else if (strFormatFor == "2")
                {
                    functionReturnValue = dt.ToString("dd/MM/yyyy");
                }
                else if (strFormatFor == "3")
                {
                    functionReturnValue = dt.ToString("yyyy-MM-dd");
                }
                else if (strFormatFor == "4")
                {
                    functionReturnValue = dt.ToString("yyyyMMdd");
                }
                else if (strFormatFor == "5")
                {
                    functionReturnValue = dt.ToString("ddMMyyyy");
                }
                else if (strFormatFor == "6")
                {
                    functionReturnValue = dt.ToString("dd-MM-yyyy");
                }
                else
                {
                    functionReturnValue = dt.ToString("dd/MM/yyyy");
                }


            }
            else
            {
                functionReturnValue = "Value Date not found in the database.";
            }

            //}
            return functionReturnValue;
        }

        public string MaskString(string sSrc, int iLength)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(sSrc); i++)
            {
                sTemp = sTemp + " ";
            }
            return sSrc + sTemp;
        }

        public string MaskStringForward(string sSrc, int iLength, string strValue)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(sSrc); i++)
            {
                sTemp = strValue + sTemp;
            }
            return sTemp + sSrc;
        }


        public string MaskNumber(double iNumber, int iLength)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(iNumber.ToString()); i++)
            {
                sTemp = sTemp + "0";
            }
            return sTemp + iNumber;
        }

        public string MaskNumber_mozi(string iNumber, int iLength)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";

            double d = double.Parse(iNumber, CultureInfo.InvariantCulture);

            string s = string.Format("{0:0.00}", d);
            for (i = 1; i <= iLength - Strings.Len(s); i++)
            {
                sTemp = sTemp + "0";
            }
            return sTemp + s;
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

        public string MaskNumberBack(double iNumber, int iLength)
        {
            string sTemp = null;
            int i = 0;
            sTemp = "";
            for (i = 1; i <= iLength - Strings.Len(iNumber.ToString()); i++)
            {
                sTemp = sTemp + "0";
            }
            return iNumber + sTemp;
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

        public int convertAscii(string value)
        {
            int sum = 0;
            foreach (char c in value)
            {

                sum = System.Convert.ToInt32(c);
            }

            return sum;
        }

        public int sumNumber(string value)
        {
            int sum = 0;
           for(int i=0;i<=value.Length-1;i++)
            {

                sum = sum+ System.Convert.ToInt32(value.Substring(i,1));
            }

            return sum;
        }
       
        private Int32 convertAscii2(string valuestr)
        {
            int totcol = valuestr.Length;

            char[] chararray = valuestr.ToCharArray();


            Int32 asc_sum = 0;
            Int32 sum = 0;
            foreach (char ch in chararray)
            {
                int ascic = (int)ch;
                sum = sum + ascic;
               
            }

            //for (int i = 0; i <= totcol; i++)
            //{
            //    //asc_sum = Strings.Asc(valuestr.Substring(i - 1, 1));
            //    int asci= 
            //    sum += i * asc_sum;
            //}
            return sum;
        }

        public string GenerateGiroFile_Mizuho_newformat()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(20), 20);
                string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(11);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";

                if (strGetOriginatingAccountNumber.Length > 6)
                {
                    strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                }
                else
                {
                    strCustAcNo = strGetOriginatingAccountNumber.ToString().Trim();
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());
                // fp.WriteLine("HOH 01" + strGetValueDate + strGetCompanyName + strGetOriginatingAccountNumber + "SGD" + strGetOperatorCode + strGetApproverCode);

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string striTotal;
                            int intA;
                            int intB;
                            int intC;
                            int intD;
                            int intE;
                            int inthashvalue = 0;
                            int count = 0;
                            string strCon = "";
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    count = count + 1;
                                    intB = convertAscii(row["account_name"].ToString());
                                    intC = convertAscii(row["giro_acct_number"].ToString());
                                    intD = convertAscii("SGD");
                                    intE = convertAscii(System.Convert.ToInt32(Utility.ToDouble(row["netpay"].ToString())).ToString());

                                    intA = intB + intC + intD + intE;
                                    inthashvalue = inthashvalue + intA;

                                    string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                    string strgiroaccountno = row["giro_acct_number"].ToString();
                                    string strreceivingname = row["account_name"].ToString();
                                    string strCustGetValueDate = GetValueDate("1");
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());

                                    string strNetPay = Strings.Left(MaskNumber_mozi(row["netpay"].ToString(), 15), 15);


                                    //if (strNetPay.IndexOf(".") > 0)
                                    //{
                                    //    if (strNetPay.IndexOf(".") == 7)
                                    //    {
                                    //        if ((strNetPay.Length - (strNetPay.IndexOf(".") + 1)) == 1)
                                    //        {
                                    //            strNetPay = "00" + strNetPay + "0";
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        strNetPay = "000" + strNetPay;
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    strNetPay = strNetPay + ".00";
                                    //}


                                    strCon = strCon + "\"" + "D" + "\"" + "," + "\"" + "$" + "\"" + "," + "B01" + "\"" + "," + "\"\"" + "," + strCustAcNo + "," +
                                       "\"\"" + "," + "\"" + "SGD" + "\"" + "," + "\"" + strNetPay + "\"" + "," + "\"\"" + "," + "\"" + strreceivingname + "\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + strgiroaccountno + "\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + "SALA" + "\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + row["BIC_code"].ToString() + "\"" + "," +
                                      "\"\"" + "," + "\"\"" + "," + "\"\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," +
                                     "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "@";




                                }
                            }
                            inthashvalue = inthashvalue + count;
                            strCon = strCon.Replace("@", System.Environment.NewLine);
                            strCon = strCon.Replace("$", inthashvalue.ToString());
                            fp.WriteLine(strCon + "                        ");

                            //striTotal = Strings.Left(MaskNumber(iTotal, 13), 13);
                            //if (striTotal.IndexOf(".") > 0)
                            //{
                            //    striTotal = "000" + striTotal;
                            //}
                            //else
                            //{
                            //    striTotal = striTotal + ".00";
                            //}
                            //fp.WriteLine("T" + MaskNumber(Utility.ToDouble(ds.Tables[0].Rows.Count), 4) + striTotal + Convert.ToString(inthashvalue));
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_Mizuho_G3format()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(20), 20);
                string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(11);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";


                if (strGetOriginatingAccountNumber.Length > 6)
                {
                    strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                }
                else
                {
                    strCustAcNo = strGetOriginatingAccountNumber.ToString();
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());
                                
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string striTotal;
                            int intA;
                            int intB;
                            int intC;
                            int intD;
                            int intE;
                            int inthashvalue = 0;
                            int count = 0;
                            string strCon = "";
                           
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    count = count + 1;
                                    

                                    
                                    

                                    string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                    string strgiroaccountno = "";
                                    if (strgirobank == "7232" || strgirobank == "7339" || strgirobank == "7791")
                                    {
                                        strgiroaccountno = row["Branch_Number"].ToString() + row["giro_acct_number"].ToString();
                                    }
                                    else
                                    {
                                        strgiroaccountno = row["giro_acct_number"].ToString();
                                    }
                                    

                                    string strreceivingname = row["account_name"].ToString();
                                    string strCustGetValueDate = GetValueDate("1");
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());

                                    string strNetPay = row["netpay"].ToString();

                                    intB = convertAscii2(row["account_name"].ToString());
                                    intC = sumNumber(strgiroaccountno);
                                    intD = convertAscii2("SGD");
                                    intE = sumNumber(((Utility.ToDouble(row["netpay"].ToString())) * 100).ToString());
                                    intA = intB + intC + intD + intE;
                                    inthashvalue = inthashvalue + intA;

                                    strCon = strCon + "\"" + "D" + "\"" + "," + "\"" + "$" + "\"" + ",\"" + "B01" + "\"" + "," + "\"\"" + ",\"" + strGetOriginatingAccountNumber + "\"," +
                                       "\"\"" + "," + "\"" + "SGD" + "\"" + "," + "\"" + strNetPay + "\"" + "," + "\"\"" + "," + "\"" + strreceivingname + "\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + strgiroaccountno + "\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + "SALA" + "\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"" + row["BIC_code"].ToString() + "\"" + "," +"\"\"" + "," + "\"\"" + "," + "\"\"" +
                                      "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," +
                                     "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + "MONTHLY SALARY" + "\"" +
                                     "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" +
                                     "," + "\"" + strGetValueDate  + "\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" +",\"N\""+
                                     "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" +count +"\""+ "@";

                                    
                                }
                            }
                            inthashvalue = inthashvalue + count;
                            strCon = Strings.Left(strCon, strCon.Length - 1);
                            strCon = strCon.Replace("@", System.Environment.NewLine);
                            strCon = strCon.Replace("$", inthashvalue.ToString());
                            fp.Write(strCon);

                          }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_Mizuho()
        {
            string rt;
            if (ISnewformate)
            {
                rt = GenerateGiroFile_Mizuho_newformat();
            }
            else if (ISg3format)
            {
                rt = GenerateGiroFile_Mizuho_G3format();
            }
            else
            {


                StreamWriter fp = default(StreamWriter);

                try
                {
                    string strGetValueDate = GetValueDate("4");
                    string strGetCompanyName = Strings.Left(GetCompanyName(20), 20);
                    string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(11);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    string strCustAcNo = "";

                    if (strGetOriginatingAccountNumber.Length > 6)
                    {
                        strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                    }
                    else
                    {
                        strCustAcNo = strGetOriginatingAccountNumber.ToString().Trim();
                    }
                    string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                    string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    fp.WriteLine("HOH 01" + strGetValueDate + strGetCompanyName + strGetOriginatingAccountNumber + "SGD" + strGetOperatorCode + strGetApproverCode);

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                    //string sSQL = "sp_online_giro";
                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }

                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                        //update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                        {
                            ds = ChangeMidMonthSalNetPay(ds);
                        }
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double iTotal = 0;
                                string striTotal;
                                double intA;
                                double intB;
                                double intC;
                                double intD;
                                double intE;
                                double inthashvalue = 0;
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        intA = Utility.ToDouble(row["giro_bank"].ToString());
                                        intB = Utility.ToDouble(row["branch_number"].ToString());
                                        intC = Utility.ToDouble(Strings.Left(row["giro_acct_number"].ToString(), 1));
                                        intD = Utility.ToDouble(Strings.Right(row["giro_acct_number"].ToString(), 1));
                                        intE = Utility.ToDouble(row["netpay"].ToString());
                                        inthashvalue = inthashvalue + ((intA + intB + intC + intD) * intE);


                                        string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                        string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                        string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString(), 11), 11);
                                        string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString(), 20), 20);
                                        string strCustGetValueDate = GetValueDate("1");
                                        iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                        string strNetPay = Strings.Left(MaskNumber(Utility.ToDouble(row["netpay"].ToString()), 9), 9);
                                        if (strNetPay.IndexOf(".") > 0)
                                        {
                                            if (strNetPay.IndexOf(".") == 7)
                                            {
                                                if ((strNetPay.Length - (strNetPay.IndexOf(".") + 1)) == 1)
                                                {
                                                    strNetPay = "00" + strNetPay + "0";
                                                }
                                            }
                                            else
                                            {
                                                strNetPay = "000" + strNetPay;
                                            }
                                        }
                                        else
                                        {
                                            strNetPay = strNetPay + ".00";
                                        }
                                        string strCon = "D" + strCustAcNo + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strCustGetValueDate + strNetPay + "C";
                                        fp.WriteLine(strCon + "                        ");

                                    }
                                }
                                striTotal = Strings.Left(MaskNumber(iTotal, 13), 13);
                                if (striTotal.IndexOf(".") > 0)
                                {
                                    striTotal = "000" + striTotal;
                                }
                                else
                                {
                                    striTotal = striTotal + ".00";
                                }
                                fp.WriteLine("T" + MaskNumber(Utility.ToDouble(ds.Tables[0].Rows.Count), 4) + striTotal + Convert.ToString(inthashvalue));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }

                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }



        public string GenerateGiroFile_SMBC()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(20), 20);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(8), 8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";

                if (strGetOriginatingAccountNumber.Length > 6)
                {
                    strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                }
                else
                {
                    strCustAcNo = strGetOriginatingAccountNumber.ToString().Trim();
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine("HOG 01" + strGetValueDate + strGetCompanyName + strGetOriginatingAccountNumber + "SGD" + strGetOperatorCode + strGetApproverCode);
                fp.WriteLine("ENV,,,,PR,PR,8806,,,CA,00," + strGetOriginatingAccountNumber + ",,SGD,CA,00," + strGetOriginatingAccountNumber + ",,SGD,TA,," + strGetValueDate + ",,SGD,,,,");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string striTotal;
                            double intA;
                            double intB;
                            double intC;
                            double intD;
                            double intE;
                            double inthashvalue = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    intA = Utility.ToDouble(row["giro_bank"].ToString());
                                    intB = Utility.ToDouble(row["branch_number"].ToString());
                                    intC = Utility.ToDouble(Strings.Left(row["giro_acct_number"].ToString(), 1));
                                    intD = Utility.ToDouble(Strings.Right(row["giro_acct_number"].ToString(), 1));
                                    intE = Utility.ToDouble(row["netpay"].ToString());
                                    inthashvalue = inthashvalue + ((intA + intB + intC + intD) * intE);


                                    string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString(), 11), 11);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString(), 20), 20);
                                    string strreceivingbankname = "";
                                    string strCustGetValueDate = GetValueDate("1");
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                    string strNetPay = Utility.ToDouble(row["netpay"].ToString()).ToString();
                                    if (strNetPay.IndexOf(".") > 0)
                                    {
                                        if (strNetPay.IndexOf(".") == 7)
                                        {
                                            if ((strNetPay.Length - (strNetPay.IndexOf(".") + 1)) == 1)
                                            {
                                                strNetPay = "00" + strNetPay + "0";
                                            }
                                        }
                                        else
                                        {
                                            if ((strNetPay.Length - strNetPay.IndexOf(".")) == 2)
                                            {
                                                strNetPay = "00" + strNetPay + "0";
                                            }
                                            else
                                            {
                                                strNetPay = "000" + strNetPay;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        strNetPay = strNetPay + ".00";
                                    }
                                    //string strCon = "D" + strCustAcNo + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strCustGetValueDate + strNetPay + "C";
                                    string strCon = "INS,,," + strreceivingname.Trim() + ",,,,,,,,,,,,," + strreceivingbankname.Trim() + ",," + strgirobank.Trim() + strbranchnumber.Trim() + ",,,SINGAPORE," + strgiroaccountno.Trim() + ",,,,,," + strNetPay + ",,,,,,,,22,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
                                    fp.WriteLine(strCon);

                                }
                                striTotal = Strings.Left(MaskNumber(iTotal, 13), 13);
                                if (striTotal.IndexOf(".") > 0)
                                {
                                    striTotal = "000" + striTotal;
                                }
                                else
                                {
                                    striTotal = striTotal + ".00";
                                }
                            }
                            //fp.WriteLine("T" + MaskNumber(Utility.ToDouble(ds.Tables[0].Rows.Count), 4) + striTotal + Convert.ToString(inthashvalue));
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        private DataSet ChangeMidMonthSalNetPay(DataSet ds)
        {
            //change the netpay amount to the fixed amount(from textbox)
            //string mid_moth = (string)HttpContext.Current.Session["mid-month"].ToString();

            //try
            //{
            //    if ((string)HttpContext.Current.Session["mid-month"].ToString() != null)
            //    {

            //        foreach (DataRow row in ds.Tables[0].Rows)
            //        {


            //            row["NetPay"] = (string)HttpContext.Current.Session["mid-month"].ToString();
            //        }
            //    }
            //    return ds;
            //}
            //catch (Exception ex)
            //{
            //    return ds;
            //}
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string empId = row["emp_id"].ToString();
                if (HttpContext.Current.Session[empId].ToString() != null)
                {
                    row["NetPay"] = (string)HttpContext.Current.Session[empId].ToString();
                }

            }

            //try
            //{
            //    if ((string)HttpContext.Current.Session["TransId"].ToString() != null)
            //    {
            //        string sql = string.Format("select * from Giro_History where TransectionId='{0}'", (string)HttpContext.Current.Session["TransId"].ToString());

            //        DataSet Giro_History = new DataSet();
            //        Giro_History = DataAccess.FetchRSDS(CommandType.Text, sql);



            //        foreach (DataRow row in ds.Tables[0].Rows)
            //        {
            //            string emp_id = row["emp_id"].ToString();
            //            DataTable tblMEN = Giro_History.Tables[0];
            //            DataRow[] results = tblMEN.Select(string.Format("EmpId ='{0}'", emp_id));

            //            row["NetPay"] = results[0]["GiroAmount"].ToString();
            //        }
            //    }
            //    return ds;
            //}
            //catch (Exception ex)
            //{
            //    return ds;
            //}
            return ds;
        }

        public string GenerateGiroFile_HABIB()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";
            string scr = "";

            try
            {
                string strGetValueDate = GetValueDate("3");
                //string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                //string strGetOriginatingAccountNumber = MaskString(Replace(Strings.Left(GetOriginatingAccountNumber(0), 9), "-", ""), 9);
                //string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                //string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine("OVERSEA-CHINESE BANKING CORP GROUP" + Strings.Space(24) + "&&&&&&&&&&&&&&&&&&&&&&&&");
                //fp.WriteLine(strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + DateTime.Today.ToString("yyyyMMdd") + "001" + strGetOriginatorName + GetValueDate("4") + Strings.Space(31));

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";

                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        ds = ChangeMidMonthSalNetPay(ds);

                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string _startdate = "";
                            string _enddate = "";
                            string _Unpaidleave = "";
                            string _emp_id = "";
                            string _company_bankcode = "";
                            DateTime st = new DateTime();
                            DateTime et = new DateTime();
                            int count_record = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    _emp_id = MaskNumber(Convert.ToDouble(row["Emp_ID"].ToString().Trim()), 14);
                                    strgirobank = row["giro_bank"].ToString().Trim();//employer bank code
                                    strbranchnumber = row["branch_number"].ToString().Trim();//employe agent code
                                    strgiroaccountno = row["giro_acct_number"].ToString().Trim();//employe Ac no
                                    _startdate = GetformateDate("3", row["StartDate"].ToString().Trim());
                                    _enddate = GetformateDate("3", row["EndDate"].ToString().Trim());
                                    _Unpaidleave = row["unpaid_leaves"].ToString().Trim();
                                    _company_bankcode = row["company_bankcode"].ToString().Trim();//employer code provided by mol
                                    strreceivingname = row["account_name"].ToString().Trim();//
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());//Total Cumulative Salary
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());//indivitual net pay
                                    count_record = count_record + 1;
                                    st = Convert.ToDateTime(_startdate);
                                    et = Convert.ToDateTime(_enddate);

                                    TimeSpan ts = et - st;
                                    string noofdays = ts.TotalDays.ToString();


                                    sTrx = "EDR" + Strings.Space(2) + "," + _emp_id + Strings.Space(2) + "," + strbranchnumber + Strings.Space(2) + "," + strgiroaccountno + Strings.Space(2) + "," +
                                        _startdate + Strings.Space(2) + "," + _enddate + Strings.Space(2) + "," + noofdays + Strings.Space(2) + "," + iTotal.ToString() + Strings.Space(2) + "," + '0' + Strings.Space(2) + "," + _Unpaidleave;


                                    fp.WriteLine(sTrx);
                                }

                            }
                            scr = "SCR" + Strings.Space(2) + "," + _company_bankcode + Strings.Space(2) + "," + "802420101" + Strings.Space(2) + "," + DateTime.Now.ToString("yyy-mm-dd") + Strings.Space(2) + "," +
                                DateTime.Now.ToString("HHmm") + Strings.Space(2) + st.ToString("mmyyyy") + Strings.Space(2) + "," + count_record.ToString() + Strings.Space(2) + "," + dTotalAmt.ToString() + Strings.Space(2) + "," + "AED" + Strings.Space(2) + "," + "TEST";
                            fp.WriteLine(scr);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_CITI(DataSet dsInfor, int currid)
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            try
            {
                string currency1 = "select Currency  from Currency where id =" + currid;
                SqlDataReader drcurr = DataAccess.ExecuteReader(CommandType.Text, currency1, null);
                string currecny = "";
                while (drcurr.Read())
                {
                    if (drcurr.GetValue(0) != null)
                    {
                        currecny = Convert.ToString(drcurr.GetValue(0).ToString());
                    }
                }

                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '#', '%', '$', '^', '&', '*', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                //char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                if (strGetOriginatingAccountNumber.Length > 6)
                {
                    strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                }
                else
                {
                    strCustAcNo = strGetOriginatingAccountNumber.ToString().Trim();
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[8];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
                parms[i++] = new SqlParameter("@currency1", Utility.ToInteger(currid));

                string sSQL = "Sp_online_giro_mc1";
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {

                            double iTotal = 0;
                            string striTotal;
                            double intA;
                            double intB;
                            double intC;
                            double intD;
                            double intE;
                            double inthashvalue = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {

                                    double netpay = 0.0;
                                    double grossPay = 0.0;
                                    double additions = 0.0;
                                    double deductions = 0.0;




                                    if (row["PaymentType"].ToString() == "1")
                                    {
                                        if (row["PaymentPart"].ToString() == "3")
                                        {
                                            netpay = Utility.ToDouble(row["Total_Additions"].ToString());
                                        }

                                        if (row["PaymentPart"].ToString() == "2" || row["PaymentPart"].ToString() == "1")
                                        {
                                            foreach (DataRow dr1 in dsInfor.Tables[0].Rows)
                                            {
                                                if (row["emp_id"].ToString() == dr1["EmpId"].ToString())
                                                {
                                                    netpay = Convert.ToDouble(dr1["Basic"].ToString());
                                                    //currecny = dr1["Currency"].ToString();
                                                }
                                            }

                                            foreach (DataRow dr1 in dsInfor.Tables[1].Rows)
                                            {
                                                if (row["emp_id"].ToString() == dr1["EmpId"].ToString())
                                                {
                                                    //netpay = Convert.ToDouble(dr1["Basic"].ToString());
                                                    //currecny = dr1["Currency"].ToString();
                                                }
                                            }
                                        }
                                    }
                                    //if (netpay == 0 || row["PaymentPart"].ToString() == "3")
                                    //{
                                    //    netpay = Utility.ToDouble(row["netpay"].ToString());
                                    //    if (row["total_additions"].ToString() != "")
                                    //    {
                                    //        additions = Convert.ToDouble(row["total_additions"].ToString());
                                    //    }
                                    //    if (row["total_deductions"].ToString() != "")
                                    //    {
                                    //        deductions = Convert.ToDouble(row["total_deductions"].ToString());
                                    //    }
                                    //    netpay = additions - deductions;
                                    //    if (netpay <= 0)
                                    //    {
                                    //        netpay = 0;

                                    //    }
                                    //    grossPay = netpay;
                                    //}

                                    intA = Utility.ToDouble(row["giro_bank"].ToString());
                                    intB = Utility.ToDouble(row["branch_number"].ToString());
                                    intC = Utility.ToDouble(Strings.Left(row["giro_acct_number"].ToString(), 1));
                                    intD = Utility.ToDouble(Strings.Right(row["giro_acct_number"].ToString(), 1));
                                    intE = Utility.ToDouble(row["netpay"].ToString());
                                    inthashvalue = inthashvalue + ((intA + intB + intC + intD) * intE);
                                    pos = 0;
                                    string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString(), 11), 11);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString(), 20), 20);
                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    {
                                        strreceivingname = strreceivingname.Remove(pos, 1);
                                    }

                                    string strreceivingbankname = "";
                                    string strCustGetValueDate = GetValueDate("1");
                                    iTotal = iTotal + netpay;  //Utility.ToDouble(row["netpay"].ToString());
                                    string strNetPay = netpay.ToString(); // Utility.ToDouble(row["netpay"].ToString()).ToString();
                                    string strCon = "";
                                    if (Utility.ToString(row["PaymentType"].ToString()) == "0")
                                    {
                                        strCon = "PLP@SG@" + strGetOriginatingAccountNumber.Trim() + "@" + currecny + "@" + strNetPay.Trim() + "@@" + strGetValueDate + "@@@@@@@" + strGetCompanyName.Trim() + "@@@@@@" + strreceivingname.Trim() + "@@@@@" + strgiroaccountno.Trim() + "@@@@@@@" + strgirobank.Trim() + strbranchnumber.Trim() + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@22@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
                                    }
                                    else if (Utility.ToString(row["PaymentType"].ToString()) == "1")
                                    {
                                        strCon = "PLP@SG@" + strGetOriginatingAccountNumber.Trim() + "@" + currecny + "@" + strNetPay.Trim() + "@@" + strGetValueDate + "@@@@@@@" + strGetCompanyName.Trim() + "@@@@@@" + strreceivingname.Trim() + "@@@@@" + strgiroaccountno.Trim() + "@@@@@@@" + strgirobank.Trim() + strbranchnumber.Trim() + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@22@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
                                    }
                                    else
                                    {
                                        strCon = "PLP@SG@" + strGetOriginatingAccountNumber.Trim() + "@" + currecny + "@" + strNetPay.Trim() + "@@" + strGetValueDate + "@@@@@@@" + strGetCompanyName.Trim() + "@@@@@@" + strreceivingname.Trim() + "@@@@@" + strgiroaccountno.Trim() + "@@@@@@@" + strgirobank.Trim() + strbranchnumber.Trim() + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@22@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
                                    }
                                    fp.WriteLine(strCon);

                                }
                                striTotal = Strings.Left(MaskNumber(iTotal, 13), 13);
                                if (striTotal.IndexOf(".") > 0)
                                {
                                    striTotal = "000" + striTotal;
                                }
                                else
                                {
                                    striTotal = striTotal + ".00";
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_CITI()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '#', '%', '$', '^', '&', '*', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                //char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                if (strGetOriginatingAccountNumber.Length > 6)
                {
                    strCustAcNo = Strings.Right(strGetOriginatingAccountNumber.ToString().Trim(), 6);
                }
                else
                {
                    strCustAcNo = strGetOriginatingAccountNumber.ToString().Trim();
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                // string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string striTotal;
                            double intA;
                            double intB;
                            double intC;
                            double intD;
                            double intE;
                            double inthashvalue = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    intA = Utility.ToDouble(row["giro_bank"].ToString());
                                    intB = Utility.ToDouble(row["branch_number"].ToString());
                                    intC = Utility.ToDouble(Strings.Left(row["giro_acct_number"].ToString(), 1));
                                    intD = Utility.ToDouble(Strings.Right(row["giro_acct_number"].ToString(), 1));
                                    intE = Utility.ToDouble(row["netpay"].ToString());
                                    inthashvalue = inthashvalue + ((intA + intB + intC + intD) * intE);
                                    pos = 0;

                                    string strgirobank = Strings.Left(MaskString(row["giro_bank"].ToString(), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString(), 11), 11);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString(), 20), 20);
                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    {
                                        strreceivingname = strreceivingname.Remove(pos, 1);
                                    }

                                    string strreceivingbankname = "";
                                    string strCustGetValueDate = GetValueDate("1");
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                    string strNetPay = Utility.ToDouble(row["netpay"].ToString()).ToString();
                                    string strCon = "PLP@SG@" + strGetOriginatingAccountNumber.Trim() + "@SGD@" + strNetPay.Trim() + "@@" + strGetValueDate + "@@@@@@@" + strGetCompanyName.Trim() + "@@@@@@" + strreceivingname.Trim() + "@@@@@" + strgiroaccountno.Trim() + "@@@@@@@" + strgirobank.Trim() + strbranchnumber.Trim() + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@22@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@";
                                    fp.WriteLine(strCon);

                                }
                                striTotal = Strings.Left(MaskNumber(iTotal, 13), 13);
                                if (striTotal.IndexOf(".") > 0)
                                {
                                    striTotal = "000" + striTotal;
                                }
                                else
                                {
                                    striTotal = striTotal + ".00";
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        //bic
        public string GenerateGiroFile_CIMB_oldformate()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strspace = "";
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 12);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                // string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    pos = 0;
                                    string strbankbranch = Strings.Left(MaskString(row["bank_branch"].ToString().Trim(), 3), 3);
                                    string strgirobank = MaskString(Strings.Left(MaskString(row["giro_bank"].ToString().Trim(), 4), 4), 12);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString().Trim(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString().Trim(), 15), 15);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString().Trim(), 20), 20);
                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    {
                                        strreceivingname = strreceivingname.Remove(pos, 1);
                                    }

                                    strreceivingname = MaskString(strreceivingname.Trim(), 42);
                                    string strCustGetValueDate = GetValueDate("1");
                                    string strNetPay = MaskNumber(Utility.ToInteger(row["netpay"].ToString().Replace(".", "")), 15);
                                    string strCon = strbankbranch + strGetOriginatingAccountNumber.Trim() + strgirobank + strbranchnumber + strgiroaccountno + "DA14SGD" + strNetPay + "SAL" + MaskString(strspace, 18) + strreceivingname + strGetValueDate + MaskString(strspace, 22);
                                    fp.WriteLine(strCon);
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 5);
                                string strtotpay = MaskNumber(Utility.ToInteger(iTotal.ToString().Replace(".", "")), 15);
                                fp.WriteLine(MaskString(strspace, 45) + "TT" + strreccount + strtotpay + MaskString(strspace, 93));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        // public string GenerateGiroFile_CIMB()
        //{
        //    if (!ISnewformate)
        //    {
        //        GenerateGiroFile_CIMB_oldformate();
        //    }
        //    string rt;
        //    StreamWriter fp = default(StreamWriter);

        //    try
        //    {
        //        string strspace = "";
        //        string strGetValueDate = GetValueDate("4");
        //        string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);

        //        string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
        //        strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 10);

        //        string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

        //        int pos = 0;
        //        char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };

        //        while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
        //        {
        //            strGetCompanyName = strGetCompanyName.Remove(pos, 1);
        //        }

        //        string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);

        //        string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);

        //        fp = File.CreateText(strPath + this.LogFileName.ToString());
        //        if (ISnewformate)
        //        {

        //            fp.WriteLine(strGetOriginatingAccountNumber + "," + strGetCompanyName + "," + "SGD" + "," + "@TAM" + ",Monthly Payment" + "," + "@TRC" + "," + strGetValueDate);

        //        }
        //        int i = 0;
        //        SqlParameter[] parms = new SqlParameter[7];
        //        parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //        parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //        parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //        parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //        parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //        parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //        parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

        //        //string sSQL = "sp_online_giro";
        //        string sSQL;
        //        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //        {
        //            sSQL = "sp_online_giro_MidMonth";
        //        }
        //        else
        //        {
        //            sSQL = "sp_online_giro";
        //        }

        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

        //            //update ds for mid month salary
        //            if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //            {
        //                ds = ChangeMidMonthSalNetPay(ds);
        //            }
        //            if (ds != null)
        //            {
        //                if (ds.Tables.Count > 0)
        //                {
        //                    double iTotal = 0;
        //                    int count = 0;
        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {
        //                        if (row["giro_bank"].ToString() != "")
        //                        {
        //                            pos = 0;
        //                            string strbankbranch = Strings.Left(MaskString(row["Branch_Number"].ToString().Trim(), 3), 3);
        //                            string strgirobank = Strings.Left(row["giro_bank"].ToString().Trim(),4);

        //                            string strbranchnumber = Strings.Left(row["branch_number"].ToString().Trim(),3);
        //                            string strgiroaccountno = Strings.Left(row["giro_acct_number"].ToString().Trim(), 11);
        //                            string strreceivingname = Strings.Left(row["account_name"].ToString().Trim(), 20);

        //                            char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
        //                            //while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
        //                            //{
        //                            //    strreceivingname = strreceivingname.Remove(pos, 1);
        //                            //}

        //                            //strreceivingname = MaskString(strreceivingname.Trim(), 40);

        //                         //   string strCustGetValueDate = GetValueDate("1");

        //                            string strNetPay = row["netpay"].ToString();
        //                            string strCon="";

        //                            //mmx(13Feb2015) - 
        //                            if (!ISnewformate)
        //                            {
        //                                while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
        //                                {
        //                                    strreceivingname = strreceivingname.Remove(pos, 1);
        //                                }

        //                                strreceivingname = MaskString(strreceivingname.Trim(), 40);

        //                                string strCustGetValueDate = GetValueDate("1");
        //                                strCon = strgiroaccountno + "," + strreceivingname + "," + "SGD" + "," + strNetPay + "," + "MOnthly Payment" + "," + strgirobank + "," +

        //                                    strbranchnumber + "," + "N";
        //                            }
        //                            else
        //                            {
        //                                strCon = strgiroaccountno + "," + strreceivingname + "," + "SGD" + "," + strNetPay + "," + "Monthly Payment" + "," + strgirobank + "," +
        //                                    strbranchnumber.TrimStart('0');
        //                            }

        //                            fp.WriteLine(strCon);
        //                            iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
        //                            count = count + 1;

        //                        }
        //                    }
        //                    fp.Close();
        //                    String strFile = File.ReadAllText(strPath + this.LogFileName.ToString());

        //                    strFile = strFile.Replace("@TAM", iTotal.ToString());
        //                    strFile = strFile.Replace("@TRC", count.ToString());

        //                    File.WriteAllText(strPath + this.LogFileName.ToString(), strFile);


        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + e.Message;
        //        }
        //        finally
        //        {
        //        }


        //        //fp.WriteLine("D" + strCustAcNo +  );
        //        rt = "File Succesfully created!";
        //       // fp.Close();
        //    }
        //    catch (Exception err)
        //    {
        //        rt = "File Creation failed. Reason is as follows : " + err.Message;
        //    }
        //    finally
        //    {
        //    }
        //    return rt;
        //}
        public string GenerateGiroFile_CIMB()
        {
            if (!ISnewformate)
            {
                GenerateGiroFile_CIMB_oldformate();
            }
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strspace = "";
                string strGetValueDate = GetValueDate("5");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 140);

                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 10);

                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };

                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }

                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);

                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                if (ISnewformate)
                {

                    fp.WriteLine("3," + strGetOriginatingAccountNumber + "," + strGetCompanyName + "," + "SGD" + "," + "@TAM" + "," + "@TRC" + ",B,C," + strGetValueDate);

                }
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            int count = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    pos = 0;
                                    string strbankbranch = Strings.Left(MaskString(row["Branch_Number"].ToString().Trim(), 3), 3);
                                    string strgirobank = row["giro_bank"].ToString().Trim();

                                    string strbranchnumber = Strings.Left(row["branch_number"].ToString().Trim(), 3);
                                    string strgirobranch = GetGirobranch(Convert.ToInt32(row["emp_id"]));
                                    string strgiroaccountno = "";
                                    string strreceivingname = Strings.Left(row["account_name"].ToString().Trim(), 140);

                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    //while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    //{
                                    //    strreceivingname = strreceivingname.Remove(pos, 1);
                                    //}

                                    //strreceivingname = MaskString(strreceivingname.Trim(), 40);

                                    //   string strCustGetValueDate = GetValueDate("1");
                                    string strBICcode = GetOriginatingBICcode(strgirobank);
                                    if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                    {
                                     //   strgiroaccountno = Strings.Left(strgirobranch + row["giro_acct_number"].ToString().Trim(), 34);
                                        strgiroaccountno = Strings.Left(strbranchnumber + row["giro_acct_number"].ToString().Trim(), 34);
                                    }
                                    else
                                    {
                                        strgiroaccountno = Strings.Left(row["giro_acct_number"].ToString().Trim(), 34);
                                    }



                                    string strNetPay = row["netpay"].ToString();
                                    string strCon = "";

                                    //mmx(13Feb2015) - 
                                    if (!ISnewformate)
                                    {
                                        while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                        {
                                            strreceivingname = strreceivingname.Remove(pos, 1);
                                        }

                                        strreceivingname = MaskString(strreceivingname.Trim(), 40);

                                        string strCustGetValueDate = GetValueDate("1");
                                        strCon = strgiroaccountno + "," + strreceivingname + "," + "SGD" + "," + strNetPay + "," + "MOnthly Payment" + "," + strgirobank + "," +

                                            strbranchnumber + "," + "N";
                                    }
                                    else
                                    {
                                        //strCon = strgiroaccountno + "," + strreceivingname + "," + strNetPay + ",SGD," + strgirobank + ",SALA,Monthly Payment";

                                        strCon = strgiroaccountno + "," + strreceivingname + "," + strNetPay + ",SGD," + strBICcode + ",SALA,Monthly Payment,,";
                                    }


                                    fp.WriteLine(strCon);
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                    count = count + 1;

                                }
                            }
                            fp.Close();
                            String strFile = File.ReadAllText(strPath + this.LogFileName.ToString());

                            strFile = strFile.Replace("@TAM", iTotal.ToString("#0.00###"));
                            strFile = strFile.Replace("@TRC", count.ToString());

                            File.WriteAllText(strPath + this.LogFileName.ToString(), strFile);


                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                // fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_ABN()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strspace = "";
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 12);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    pos = 0;
                                    string strbankbranch = Strings.Left(MaskString(row["bank_branch"].ToString().Trim(), 3), 3);
                                    string strgirobank = MaskString(Strings.Left(MaskString(row["giro_bank"].ToString().Trim(), 4), 4), 12);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString().Trim(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString().Trim(), 15), 15);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString().Trim(), 20), 20);
                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    {
                                        strreceivingname = strreceivingname.Remove(pos, 1);
                                    }

                                    strreceivingname = MaskString(strreceivingname.Trim(), 42);
                                    string strCustGetValueDate = GetValueDate("1");
                                    string strNetPay = MaskNumber(Utility.ToInteger(row["netpay"].ToString().Replace(".", "")), 15);
                                    string strCon = strbankbranch + strGetOriginatingAccountNumber.Trim() + strgirobank + strbranchnumber + strgiroaccountno + "DA14SGD" + strNetPay + "SAL" + MaskString(strspace, 18) + strreceivingname + strGetValueDate + MaskString(strspace, 22);
                                    fp.WriteLine(strCon);
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 5);
                                string strtotpay = MaskNumber(Utility.ToInteger(iTotal.ToString().Replace(".", "")), 15);
                                fp.WriteLine(MaskString(strspace, 45) + "TT" + strreccount + strtotpay + MaskString(strspace, 93));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_HSBC(DataSet dsInfor, int currid)
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            try
            {
                string currency1 = "select Currency  from Currency where id =" + currid;
                SqlDataReader drcurr = DataAccess.ExecuteReader(CommandType.Text, currency1, null);
                string currecny = "";
                while (drcurr.Read())
                {
                    if (drcurr.GetValue(0) != null)
                    {
                        currecny = Convert.ToString(drcurr.GetValue(0).ToString());
                    }
                }
                string strspace = "";
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
             
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strCustAcNo = "";
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                //char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }

                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[8];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
                parms[i++] = new SqlParameter("@currency1", Utility.ToInteger(currid));

                string sSQL = "Sp_online_giro_mc1";
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double netpay = 0.0;
                            double iSingleTotal = 0;
                            double iTotal = 0;
                            double iRowCount = 0;



                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 6);
                                string strtotpay = MaskStringForward(iTotal.ToString("#0.00"), 17, "0");
                                string strCon = "1SGHSBC" + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + "E01" + strreccount + strtotpay + strGetValueDate + "APO" + MaskString(strspace, 78) + "1";
                                fp.WriteLine(strCon);
                            }

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {

                                    if (row["PaymentType"].ToString() == "1")
                                    {
                                        if (row["PaymentPart"].ToString() == "3")
                                        {
                                            netpay = Utility.ToDouble(row["Total_Additions"].ToString());
                                        }

                                        if (row["PaymentPart"].ToString() == "2" || row["PaymentPart"].ToString() == "1")
                                        {
                                            foreach (DataRow dr1 in dsInfor.Tables[0].Rows)
                                            {
                                                if (row["emp_id"].ToString() == dr1["EmpId"].ToString())
                                                {
                                                    netpay = Convert.ToDouble(dr1["Basic"].ToString());
                                                    //currecny = dr1["Currency"].ToString();
                                                }
                                            }

                                            foreach (DataRow dr1 in dsInfor.Tables[1].Rows)
                                            {
                                                if (row["emp_id"].ToString() == dr1["EmpId"].ToString())
                                                {
                                                    //netpay = Convert.ToDouble(dr1["Basic"].ToString());
                                                    //currecny = dr1["Currency"].ToString();
                                                }
                                            }
                                        }
                                    }

                                    //hsbc code


                                    iRowCount = iRowCount + 1;
                                    pos = 0;
                                    string strbankbranch = Strings.Left(MaskString(row["bank_branch"].ToString().Trim(), 3), 3);
                                    string strgirobank = MaskString(Strings.Left(MaskString(row["giro_bank"].ToString().Trim(), 4), 4), 4);
                                    string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString().Trim(), 3), 3);
                                    string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString().Trim(), 20), 20);
                                    string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString().Trim(), 20), 20);
                                    char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                    while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                    {
                                        strreceivingname = strreceivingname.Remove(pos, 1);
                                    }
                                    //iSingleTotal = Utility.ToDouble(row["netpay"].ToString());
                                    iSingleTotal = iTotal + netpay;
                                    strreceivingname = MaskString(strreceivingname.Trim(), 42);
                                    string strCustGetValueDate = GetValueDate("1");
                                    //string strnetpay = MaskStringForward(iSingleTotal.ToString("#0.00"), 16, "0");
                                    string strnetpay = MaskStringForward(netpay.ToString("#0.00"), 16, "0");
                                    string strCon = "2" + MaskString(iRowCount.ToString(), 12) + MaskString(strspace, 4) + strnetpay + MaskString(strspace, 8) + strgirobank + MaskString(strspace, 4) + strgiroaccountno + MaskString(strspace, 8) + strreceivingname + MaskString(strspace, 16);
                                    fp.WriteLine(strCon);

                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }


        public string GenerateGiroFile_HSBC()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strspace = "";
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
                string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
                strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 12);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iSingleTotal = 0;
                            double iTotal = 0;
                            double iRowCount = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 6);
                                string strtotpay = MaskStringForward(iTotal.ToString("#0.00"), 17, "0");
                                string strCon = "1SGHSBC" + strGetOriginatingAccountNumber + "E01" + strreccount + strtotpay + strGetValueDate + "APO" + MaskString(strspace, 78) + "1";
                                fp.WriteLine(strCon);
                            }

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iRowCount = iRowCount + 1;
                                    pos = 0;
                                    try
                                    {
                                        //string strbankbranch = Strings.Left(MaskString(row["Branch_Number"].ToString().Trim(), 3), 3);
                                        string strgirobank = MaskString(Strings.Left(MaskString(row["giro_bank"].ToString().Trim(), 4), 4), 4);
                                        string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString().Trim(), 3), 3);
                                        string strgiroaccountno = Strings.Left(MaskString(row["giro_acct_number"].ToString().Trim(), 20), 20);
                                        strgiroaccountno = strbranchnumber + strgiroaccountno;
                                        // strgiroaccountno = MaskNumber(Utility.ToDouble(strgiroaccountno), 20);   
                                        strgiroaccountno = Strings.Left(MaskString(strgiroaccountno.Trim(), 20), 20);
                                        string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString().Trim(), 20), 20);
                                        char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                        while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)
                                        {
                                            strreceivingname = strreceivingname.Remove(pos, 1);
                                        }
                                        iSingleTotal = Utility.ToDouble(row["netpay"].ToString());
                                        strreceivingname = MaskString(strreceivingname.Trim(), 42);
                                        string strCustGetValueDate = GetValueDate("1");
                                        string strnetpay = MaskStringForward(iSingleTotal.ToString("#0.00"), 16, "0");
                                        string strCon = "2" + MaskString(iRowCount.ToString(), 12) + MaskString(strspace, 4) + strnetpay + MaskString(strspace, 8) + strgirobank + MaskString(strspace, 4) + strgiroaccountno + MaskString(strspace, 8) + strreceivingname + MaskString(strspace, 16);
                                        fp.WriteLine(strCon);


                                    }
                                    catch (Exception ex) { }

                                }
                            }


                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_HSBC_SG()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);

            try
            {
                string strspace = "";
                string strGetValueDate = GetValueDate("4");
                string strGetCompanyName = Strings.Left(GetCompanyName(0), 35);
               //string strGetOriginatingAccountNumber = Strings.Left(GetOriginatingAccountNumber(0), 35);
               string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 35);
                strGetOriginatingAccountNumber = MaskNumber(Utility.ToDouble(strGetOriginatingAccountNumber), 9);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                int pos = 0;
                char[] trimstr = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                while ((pos = strGetCompanyName.IndexOfAny(trimstr)) >= 0)
                {
                    strGetCompanyName = strGetCompanyName.Remove(pos, 1);
                }
                string strGetOperatorCode = Strings.Left(GetOperatorCode(14), 14);
                string strGetApproverCode = Strings.Left(GetApproverCode(14), 14);
                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iSingleTotal = 0;
                            double iTotal = 0;
                            double iRowCount = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iTotal = iTotal + Utility.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                //string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 6);
                                string strreccount = MaskNumber(Utility.ToInteger(ds.Tables[0].Rows.Count.ToString()), 6);
                                string strtotpay = MaskStringForward(iTotal.ToString("#0.00"), 17, "0");
                                string strCon = "1SGHSBC" + GetOriginatingBranchNumber(3) + strGetOriginatingAccountNumber + "E01" + strreccount+ strtotpay + strGetValueDate + "APO" + MaskString(strspace, 78) + "1";
                               
                                fp.WriteLine(strCon);
                            }

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iRowCount = iRowCount + 1;
                                    pos = 0;
                                    try
                                    {
                                        //string strbankbranch = Strings.Left(MaskString(row["Branch_Number"].ToString().Trim(), 3), 3);
                                        string strgirobank = MaskString(Strings.Left(MaskString(row["giro_bank"].ToString().Trim(), 4), 4), 4);
                                        string strbranchnumber = Strings.Left(MaskString(row["branch_number"].ToString().Trim(), 3), 3);

                                        string strgiroaccountno = row["giro_acct_number"].ToString().Trim();
                                        strgiroaccountno = Replace(strgiroaccountno, "-", "");
                                         //   Strings.Left(MaskString(row["giro_acct_number"].ToString().Trim(), 34), 34);
                                        if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                        {
                                            strgiroaccountno = strbranchnumber + strgiroaccountno;
                                        }
                                        strgiroaccountno = MaskString(strgiroaccountno, 34);
                                        
                                     
                                        // strgiroaccountno = MaskNumber(Utility.ToDouble(strgiroaccountno), 20);   
                                        //strgiroaccountno = Strings.Left(MaskString(strgiroaccountno.Trim(), 20), 20);
                                        string strreceivingname = Strings.Left(MaskString(row["account_name"].ToString().Trim(), 20), 20);
                                        char[] trim = { '=', '@', '.', ';', ':', '[', ']', '{', '}', '-', '#', '%', '$', '^', '&', '*', '(', ')', '+', ',', '/', '>', '<', '~', '`', '_', '?', '!', '|', '"' };
                                        while ((pos = strreceivingname.IndexOfAny(trim)) >= 0)

                                        {
                                            strreceivingname = strreceivingname.Remove(pos, 1);
                                        }
                                        iSingleTotal = Utility.ToDouble(row["netpay"].ToString());
                                        strreceivingname = MaskString(strreceivingname.Trim(), 140);
                                        string strCustGetValueDate = GetValueDate("1");
                                        string strnetpay = MaskStringForward(iSingleTotal.ToString("#0.00"), 16, "0");
                                        string strCon = "2" + MaskNumber(Utility.ToInteger(iRowCount.ToString()), 3) +MaskString(strspace, 9)+ MaskString(strspace, 4) + strnetpay + MaskString(strspace, 8) + GetOriginatingBICcode(strgirobank) + strgiroaccountno + MaskString(strspace, 8) + strreceivingname + MaskString(strspace, 35) + "SALA" + MaskString(strspace, 140);
                                        fp.WriteLine(strCon);


                                    }
                                    catch (Exception ex) { }

                                }
                            }


                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_SC_old()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";


            try
            {
                string strGetValueDate = GetValueDate("2");
                string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(0);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                fp.WriteLine("H,P,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }


                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = row["giro_bank"].ToString();
                                    strbranchnumber = row["branch_number"].ToString();
                                    strgiroaccountno = row["giro_acct_number"].ToString();
                                    strreceivingname = row["account_name"].ToString();
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());


                                    iTotal = Utility.ToDouble(row["netpay"].ToString());

                                    sTrx = "P,PAY,BA,,,,SG,SIN,@debit_ac_no,@payment_date,@payee_name,,,,,,@payee_bank_code,@payee_branch_code,,@payee_acc_no,,,,,,,,,,,,,,,,,,SGD, @amount ,C,,,,,,,,,,,,,,,,,,,,,,";
                                    sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(strgirobank.Trim().ToString(), 1, 4));
                                    if (ISnewformate)
                                    {
                                        sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(row["BIC_code"].ToString(), 1, 11));
                                    }
                                    sTrx = Replace(sTrx, "@payee_branch_code", Strings.Mid(strbranchnumber.Trim().ToString(), 1, 3));

                                    sTrx = Replace(sTrx, "@payee_acc_no", Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));
                                    sTrx = Replace(sTrx, "@payee_name", Strings.Mid(strreceivingname.Trim().ToString(), 1, 24));
                                    sTrx = Replace(sTrx, "@payment_date", strGetValueDate.Trim().ToString());
                                    sTrx = Replace(sTrx, "@debit_ac_no", Strings.Mid(Replace(strGetOriginatingAccountNumber.Trim().ToString(), "-", ""), 1, 35));
                                    sTrx = Replace(sTrx, "@amount", row["netpay"].ToString());

                                    fp.WriteLine(sTrx);
                                }

                            }
                            strTrailer = "T,@total_records,@total_amount,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
                            strTrailer = Replace(strTrailer, "@total_records", ds.Tables[0].Rows.Count.ToString());
                            strTrailer = Replace(strTrailer, "@total_amount", Convert.ToString(dTotalAmt));
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_SC()
        {
            string rt;
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_SC_old();
                return rt;
            }
            else
            {


                StreamWriter fp = default(StreamWriter);
                string sTrx = "";


                try
                {
                    string strGetValueDate = GetValueDate("2");
                    string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(0);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    fp.WriteLine("H,P,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,");

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }


                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                        //update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                        {
                            ds = ChangeMidMonthSalNetPay(ds);
                        }
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strTrailer = "";
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string bic = "";
                                string payee_id="";
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        SqlDataReader dr= DataAccess.ExecuteReader(CommandType.Text,"SELECT ic_pp_number from employee where emp_code='"+ row["emp_id"].ToString ()+"'",null);
                                        if(dr.Read())
                                        {
                                        payee_id =dr[0].ToString ();
                                        }
                                        strgirobank = row["giro_bank"].ToString();
                                        strbranchnumber = row["branch_number"].ToString();
                                        strgiroaccountno = row["giro_acct_number"].ToString();
                                        strreceivingname = row["account_name"].ToString();
                                        dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                        
                                        iTotal = Utility.ToDouble(row["netpay"].ToString());

                                        //sTrx = "P,PAY,BA,,,,SG,SIN,@debit_ac_no,@payment_date,@payee_name,,,,,,@payee_bank_code,@payee_branch_code,,@payee_acc_no,,,,,,,,,,,,,,,,,,SGD, @amount ,C,,,,,,,,,,,,,,,,,,,,,,";
                                        sTrx = "P,PAY,BA,,@debit_ac_no,SGD,SCBLSGSGXXX,@payment_date,@payee_id,@payee_name,,,,,,@bic,,@payee_branch_code,,@payee_acc_no,SGD,@amount,,,,,,,,,,,,,,,,,,,,,C,P,,,,,,,,,,,,,,,,,,";
                                        sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(strgirobank.Trim().ToString(), 1, 4));
                                        if (ISnewformate)
                                        {
                                            sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(row["BIC_code"].ToString(), 1, 11));
                                        }
                                        bic = GetOriginatingBICcode(Strings.Mid(strgirobank.Trim().ToString(), 1, 4));
                                        sTrx = Replace(sTrx, "@payee_branch_code", Strings.Mid(strbranchnumber.Trim().ToString(), 1, 3));

                                        if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                        {

                                            sTrx = Replace(sTrx, "@payee_acc_no", strbranchnumber + Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));
                                        }
                                        else
                                        {
                                            sTrx = Replace(sTrx, "@payee_acc_no", Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));
                                        }
                                        //sTrx = Replace(sTrx, "@payee_acc_no", Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));

                                        sTrx = Replace(sTrx, "@payee_name", Strings.Mid(strreceivingname.Trim().ToString(), 1, 24));
                                        sTrx = Replace(sTrx, "@payment_date", strGetValueDate.Trim().ToString());
                                        sTrx = Replace(sTrx, "@debit_ac_no", Strings.Mid(Replace(strGetOriginatingAccountNumber.Trim().ToString(), "-", ""), 1, 35));
                                        sTrx = Replace(sTrx, "@amount", row["netpay"].ToString());
                                        sTrx = Replace(sTrx, "@bic", bic);
                                        sTrx = Replace(sTrx, "@payee_id", payee_id );
                                        fp.WriteLine(sTrx);
                                    }

                                }
                                strTrailer = "T,@total_records,@total_amount,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,";
                                strTrailer = Replace(strTrailer, "@total_records", ds.Tables[0].Rows.Count.ToString());
                                strTrailer = Replace(strTrailer, "@total_amount", dTotalAmt.ToString("##.00"));
                                fp.WriteLine(strTrailer);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
                return rt;
            }
        }
        //public string GenerateGiroFile_SC()
        //{
        //    string rt;
        //    if (!ISnewformate)
        //    {
        //     rt=  GenerateGiroFile_SC_old();
        //     return rt;
        //    }
        //    else
        //    {

               
        //        StreamWriter fp = default(StreamWriter);
        //        string sTrx = "";


        //        try
        //        {
        //            string strGetValueDate = GetValueDate("2");
        //            string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(0);
        //            string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

        //            fp = File.CreateText(strPath + this.LogFileName.ToString());
        //            fp.WriteLine("H,P");

        //            int i = 0;
        //            SqlParameter[] parms = new SqlParameter[7];
        //            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //            parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //            parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //            parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //            parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //            parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //            parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

        //            string sSQL;
        //            if (HttpContext.Current.Session["mid-month"] != null)
        //            {
        //                sSQL = "sp_online_giro_MidMonth";
        //            }
        //            else
        //            {
        //                sSQL = "sp_online_giro";
        //            }


        //            try
        //            {
        //                DataSet ds = new DataSet();
        //                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

        //                //update ds for mid month salary
        //                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //                {
        //                    ds = ChangeMidMonthSalNetPay(ds);
        //                }
        //                if (ds != null)
        //                {
        //                    if (ds.Tables.Count > 0)
        //                    {
        //                        double dTotalAmt = 0;
        //                        double iTotal = 0;
        //                        string strTrailer = "";
        //                        string strgirobank = "";
        //                        string strbranchnumber = "";
        //                        string strgiroaccountno = "";
        //                        string strreceivingname = "";
        //                        string bic = "";
        //                        foreach (DataRow row in ds.Tables[0].Rows)
        //                        {
        //                            if (row["giro_bank"].ToString() != "")
        //                            {
        //                                strgirobank = row["giro_bank"].ToString();
        //                                strbranchnumber = row["branch_number"].ToString();
        //                                strgiroaccountno = row["giro_acct_number"].ToString();
        //                                strreceivingname = row["account_name"].ToString();
        //                                dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());


        //                                iTotal = Utility.ToDouble(row["netpay"].ToString());

        //                                //sTrx = "P,PAY,BA,,,,SG,SIN,@debit_ac_no,@payment_date,@payee_name,,,,,,@payee_bank_code,@payee_branch_code,,@payee_acc_no,,,,,,,,,,,,,,,,,,SGD, @amount ,C,,,,,,,,,,,,,,,,,,,,,,";
        //                                sTrx = "P,PAY,BA,01,@debit_ac_no,SGD,SCBLSGSGXXX,@payment_date,@payee_acc_no,@payee_name,,,,,,@bic,,,@payee_branch_code,@payee_acc_no,SGD,@amount,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,SAL,,,,,AM,,,,,,,,,,,,,,,,,SAL,,,,,,,";
        //                                sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(strgirobank.Trim().ToString(), 1, 4));
        //                                if (ISnewformate)
        //                                {
        //                                    sTrx = Replace(sTrx, "@payee_bank_code", Strings.Mid(row["BIC_code"].ToString(), 1, 11));
        //                                }
        //                                bic = GetOriginatingBICcode(Strings.Mid(strgirobank.Trim().ToString(), 1, 4));
        //                                sTrx = Replace(sTrx, "@payee_branch_code", Strings.Mid(strbranchnumber.Trim().ToString(), 1, 3));

        //                                if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
        //                                {
                                            
        //                                    sTrx = Replace(sTrx, "@payee_acc_no", strbranchnumber+Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));
        //                                }
        //                                else {
        //                                    sTrx = Replace(sTrx, "@payee_acc_no", Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));
        //                                }
        //                                //sTrx = Replace(sTrx, "@payee_acc_no", Strings.Mid(Replace(strgiroaccountno.Trim().ToString(), "-", ""), 1, 11));

        //                                sTrx = Replace(sTrx, "@payee_name", Strings.Mid(strreceivingname.Trim().ToString(), 1, 24));
        //                                sTrx = Replace(sTrx, "@payment_date", strGetValueDate.Trim().ToString());
        //                                sTrx = Replace(sTrx, "@debit_ac_no", Strings.Mid(Replace(strGetOriginatingAccountNumber.Trim().ToString(), "-", ""), 1, 35));
        //                                sTrx = Replace(sTrx, "@amount", row["netpay"].ToString());
        //                                sTrx = Replace(sTrx, "@bic", bic);
        //                                fp.WriteLine(sTrx);
        //                            }

        //                        }
        //                        strTrailer = "T,@total_records,@total_amount";
        //                        strTrailer = Replace(strTrailer, "@total_records", ds.Tables[0].Rows.Count.ToString());
        //                        strTrailer = Replace(strTrailer, "@total_amount",dTotalAmt.ToString("##.00"));
        //                        fp.WriteLine(strTrailer);
        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                rt = "File Creation failed. Reason is as follows : " + e.Message;
        //            }
        //            finally
        //            {
        //            }


        //            //fp.WriteLine("D" + strCustAcNo +  );
        //            rt = "File Succesfully created!";
        //            fp.Close();
        //        }
        //        catch (Exception err)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + err.Message;
        //        }
        //        finally
        //        {
        //        }
        //        return rt;
        //    }
        //}
        public string GenerateGiroFile_Deutsche_oldformat()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";


            try
            {
                string strGetValueDate = GetValueDate("3");
                string strGetOriginatingAccountNumber = Strings.Mid(MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11), 1, 35);
                string strCustGetOriginatingAccountNumber = Replace(strGetOriginatingAccountNumber, ";", "");
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strempname = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = Strings.Left(row["giro_bank"].ToString().Trim(), 4);
                                    strbranchnumber = Strings.Left(row["branch_number"].ToString().Trim(), 3);
                                    strgiroaccountno = Strings.Left(row["giro_acct_number"].ToString().Trim(), 11);
                                    strreceivingname = Strings.Left(row["account_name"].ToString().Trim(), 24);
                                    strempname = Strings.Mid(row["emp_name"].ToString().Trim(), 1, 12);


                                    iTotal = Utility.ToDouble(row["netpay"].ToString());

                                    sTrx = strgirobank + ";" + strbranchnumber + ";" + strgiroaccountno + ";" + strreceivingname + ";" + strGetValueDate + ";" + "15;" + "SGD;" + strGetOriginatingAccountNumber + ";SGD;" + strGetOriginatingAccountNumber + ";" + iTotal.ToString() + ";SGD;22;" + strempname;
                                    fp.WriteLine(sTrx);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_Deutsche()
        {
            if (!ISnewformate)
            {
                GenerateGiroFile_Deutsche_oldformat();
            }

            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";


            try
            {
                string strGetValueDate = GetValueDate("5");
                string strGetOriginatingAccountNumber = Replace(GetOriginatingAccountNumber(0), "-", "");
                string strCustGetOriginatingAccountNumber = Replace(strGetOriginatingAccountNumber, ";", "");
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double iTotal = 0;
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strempname = "";
                            string strBICcode = "";
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = Strings.Left(row["giro_bank"].ToString().Trim(), 4);
                                    strbranchnumber = Strings.Left(row["branch_number"].ToString().Trim(), 3);
                                    strgiroaccountno = Strings.Left(row["giro_acct_number"].ToString().Trim(), 11);
                                    strreceivingname = Strings.Left(row["account_name"].ToString().Trim(), 24);
                                    strempname = Strings.Mid(row["emp_name"].ToString().Trim(), 1, 12);
                                    strBICcode = GetOriginatingBICcode(strgirobank);

                                    if (strgirobank == "7232" || strgirobank == "7339" || strgirobank == "7791")
                                    {
                                        strgiroaccountno = row["Branch_Number"].ToString() + row["giro_acct_number"].ToString();
                                    }


                                    iTotal = Utility.ToDouble(row["netpay"].ToString());

                                    //sTrx = strgirobank + ";" + strbranchnumber + ";" + strgiroaccountno + ";" + strreceivingname + ";" + strGetValueDate + ";" + "15;" + "SGD;" + strGetOriginatingAccountNumber + ";SGD;" + strGetOriginatingAccountNumber + ";" + iTotal.ToString() + ";SGD;22;" + strempname;
                                    sTrx = strCustGetOriginatingAccountNumber + ";SGD;" + strCustGetOriginatingAccountNumber + ";SGD;GIR;15;;;SGD;" + strGetValueDate + ";;;;;;Y;;" + iTotal.ToString("#0.00") + ";;;;;;;;;" + strreceivingname + ";" + strgiroaccountno + ";;;;;;;;;;" + strBICcode + ";;;;;;;;;;;;;;;;;;;;;;;;;SALA;;;;";
                                    fp.WriteLine(sTrx);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_UOB_oldformate()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {

                string strGetValueDate = GetValueDate("2");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);


                fp = File.CreateText(strPath + this.LogFileName.ToString());


               // string strUOBHashType = "2";
                double htotal1 = 0;
                double stotal = 0;
                double hsum1 = 0;
                double hsum2 = 0;
                double hsum3 = 0;

                string hsan = Convert.ToString(MaskNumber(Utility.ToDouble(Replace(GetOriginatingAccountNumber(0), "-", "")), 11));
                //ram
                //old
               // string hsbn = Convert.ToString(MaskNumberBack(Utility.ToDouble(Strings.Left(GetOriginatingBranchNumber(3), 3)), 3));
               // new
                string hsbn = Convert.ToString(MaskNumber(Utility.ToDouble(Strings.Left(GetOriginatingBranchNumber(3), 3)), 3));

                string hsbc = Convert.ToString(MaskNumberBack(Utility.ToDouble(Strings.Left(GetOriginatingBankCode(4), 4)), 4));

                if (isHash == true)
                {
                    hsum1 = (Convert.ToDouble(hsbc.Substring(0, 2)) * 2) + (Convert.ToDouble(hsbn.Substring(0, 2)) * 3) + (Convert.ToDouble(hsan.Substring(0, 2)) * 4) + (Convert.ToDouble(hsan.Substring(4, 2)) * 5) + (Convert.ToDouble(hsan.Substring(8, 2)) * 6);
                    hsum2 = (Convert.ToDouble(hsbc.Substring(2, 2)) * 9) + (Convert.ToDouble(hsbn.Substring(2, 1)) * 8) + (Convert.ToDouble(hsan.Substring(2, 2)) * 7) + (Convert.ToDouble(hsan.Substring(6, 2)) * 6) + (Convert.ToDouble(hsan.Substring(10, 1)) * 5);
                    hsum3 = hsum1 * hsum2;
                    htotal1 = hsum3;

                    fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + "2" + Strings.Space(9));
                }
                else
                {
                    fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + Strings.Space(1) + Strings.Space(9));
                }

                //working on UOB
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                // string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskStringForward(Utility.ToString(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3, "0");
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);

                                    double dtotal1 = 0;
                                    double dtotal2 = 0;
                                    double dsum1 = 0;
                                    double dsum2 = 0;
                                    double dsum3 = 0;

                                    string dsan = Convert.ToString(MaskString(Utility.ToString(Replace(row["giro_acct_number"].ToString().Trim(), "-", "")), 11));
                                    string dsbn = strbranchnumber;
                                    string dsbc = Convert.ToString(MaskNumberBack(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4));
                                    string sttotalamt = strnetpay; //Convert.ToString(MaskNumber(dTotalAmt,11));
                                    dsan = dsan.Replace(" ", "0");

                                    if (isHash == true)
                                    {
                                        dsum1 = (Utility.ToDouble(dsbc.Substring(0, 2)) * 1) + (Utility.ToDouble(dsbn.Substring(0, 2)) * 2) + (Utility.ToDouble(dsan.Substring(0, 2)) * 3) + (Utility.ToDouble(dsan.Substring(4, 2)) * 4) + (Utility.ToDouble(dsan.Substring(8, 2)) * 5) + (Utility.ToDouble("2") * 6) + (Utility.ToDouble(sttotalamt.Substring(0, 2)) * 7) + (Utility.ToDouble(sttotalamt.Substring(4, 2)) * 8) + (Utility.ToDouble(sttotalamt.Substring(8, 2)) * 9);
                                        dsum2 = (Utility.ToDouble(dsbc.Substring(2, 2)) * 9) + (Utility.ToDouble(dsbn.Substring(2, 1)) * 8) + (Utility.ToDouble(dsan.Substring(2, 2)) * 7) + (Utility.ToDouble(dsan.Substring(6, 2)) * 6) + (Utility.ToDouble(dsan.Substring(10, 1)) * 5) + (Utility.ToDouble("2") * 4) + (Utility.ToDouble(sttotalamt.Substring(2, 2)) * 3) + (Utility.ToDouble(sttotalamt.Substring(6, 2)) * 2) + (Utility.ToDouble(sttotalamt.Substring(10, 1)) * 1);
                                        dsum3 = (dsum1 * dsum2);

                                        stotal = stotal + dsum3;
                                        sTrx = "2" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(12) + Strings.Space(12) + Strings.Space(4);
                                    }
                                    else
                                    {
                                        sTrx = "2" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(12) + Strings.Space(12) + Strings.Space(4);
                                    }
                                    fp.WriteLine(sTrx);

                                }
                            }
                            if (isHash == true)
                            {
                                double eventot = stotal + htotal1;
                                //ram
                                //old
                                strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                                //new 
                                strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + "0000000000000" + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                            }
                            else
                            {
                                strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Strings.Space(13) + Strings.Space(13);
                            }
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows " + e.Message;
                }
                finally
                {
                }


               // fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        //private Int32 FieldSummaryTotal(string valuestr)
        //{
        //    int totcol = valuestr.Length;
        //    Int32 asc_sum = 0;
        //    Int32 sum = 0;
        //    for (int i = 1; i <= totcol; i++)
        //    {
        //        asc_sum = Strings.Asc(valuestr.Substring(i - 1, 1));
        //        sum += i * asc_sum;
        //    }
        //    return sum;
        //}
        public string GenerateGiroFile_UOB()
        {
            string rt;
            if (!ISnewformate)
            {
              rt=  GenerateGiroFile_UOB_oldformate();

            }
            else
            {

            
            //string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {

                string strGetValueDate = GetValueDate("2");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 34);
                string strGetOriginatorName = MaskString(GetOriginatorName(0), 140);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strGetOriginatingBICcode = GetOriginatingBICcode(strGetOriginatingBankCode);

                string strBlukCustomerRefernce = Strings.Left(strGetOriginatorName, 16);
                string EndtoEndID = MaskString("PAY" + DateTime.Today.ToString("yyyyMMdd"), 35);
                //string strfilename = "UGBI" + DateTime.Today.ToString("ddMM") + "01";
                isHash = true;

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                //-----------hash calculating header part
                Int32 total1, total2, sum1, sum2, sum3, sum4, sum5, sum6, sum7, ptype;
                // sum1 = FieldSummaryTotal("UOVBSGSGXXX");
                sum1 = FieldSummaryTotal(strGetOriginatingBICcode);
                sum2 = FieldSummaryTotal(strGetOriginatingAccountNumber);
                sum3 = FieldSummaryTotal(strGetOriginatorName);
                total1 = sum1 + sum2 + sum3;
                total2 = 0;

                if (isHash == true)
                {
                    ///fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + "2" + Strings.Space(9));
                    fp.WriteLine("1" + this.LogFileName.ToString().Substring(0, 10) + "R" + "NORMAL    " + "B" + Strings.Space(12) + strGetOriginatingBICcode + "SGD" + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(140) + strBlukCustomerRefernce + Strings.Space(10) + Strings.Space(210));
                }
                else
                {
                    // fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + Strings.Space(1) + Strings.Space(9));
                    fp.WriteLine("1" + this.LogFileName.ToString().Substring(0, 10) + "R" + "NORMAL    " + "B" + Strings.Space(12) + strGetOriginatingBICcode + "SGD" + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(140) + strBlukCustomerRefernce + Strings.Space(10) + Strings.Space(210));
                }

                //working on UOB
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                // string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            string strBICcode = "";
                            int hashcode = 0;
                            int c = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    c++;
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);

                                    strbranchnumber = MaskStringForward(Utility.ToString(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3, "0");
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 34);
                                    strreceivingname = MaskString(row["account_name"].ToString().Trim(), 140);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 18);
                                    strBICcode = GetOriginatingBICcode(strgirobank);


                                    string dsan = Convert.ToString(MaskString(Utility.ToString(Replace(row["giro_acct_number"].ToString().Trim(), "-", "")), 11));
                                    string dsbn = strbranchnumber;
                                    string dsbc = Convert.ToString(MaskNumberBack(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4));
                                    ///  string sttotalamt = strnetpay; //Convert.ToString(MaskNumber(dTotalAmt,11));

                                    ///dsan = dsan.Replace(" ", "0");

                                    if (isHash == true)
                                    {
                                        ///-----------hash calculating details part
                                        if (hashcode == 9)
                                        {
                                            hashcode = 1;
                                        }
                                        else
                                        {
                                            hashcode = hashcode + 1;
                                        }

                                        sum1 = FieldSummaryTotal(strBICcode);
                                        sum2 = FieldSummaryTotal(strgiroaccountno) * hashcode;
                                        sum3 = FieldSummaryTotal(strreceivingname) * hashcode;
                                        // ptype = 22 * c;
                                        sum4 = 429;
                                        // int ss = FieldSummaryTotal("000000000000120000");
                                        sum5 = FieldSummaryTotal(strnetpay);
                                        sum6 = FieldSummaryTotal("SALA");
                                        sum7 = sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + (22 * hashcode);

                                        total2 = total2 + sum7;




                                        sTrx = "2" + strBICcode + strgiroaccountno + strreceivingname + "SGD" + strnetpay + EndtoEndID + Strings.Space(35) + "SALA" + Strings.Space(140) + Strings.Space(140) + Strings.Space(16) + Strings.Space(38);
                                    }
                                    else
                                    {
                                        //sTrx = "2" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(12) + Strings.Space(12) + Strings.Space(4);
                                        sTrx = "2" + strBICcode + strgiroaccountno + strreceivingname + "SGD" + strnetpay + EndtoEndID + Strings.Space(35) + "SALA" + Strings.Space(140) + Strings.Space(140) + Strings.Space(16) + Strings.Space(38);
                                    }
                                    fp.WriteLine(sTrx);

                                }
                            }
                            if (isHash == true)
                            {
                                Int64 eventot = total1 + total2;
                                //ram
                                //old
                                //strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                                //new 
                                // strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + "0000000000000" + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                                strTrailer = "9" + MaskNumber((dTotalAmt * 100), 18) + MaskNumber(c, 7) + Utility.ToString(MaskNumber(eventot, 16)) + Strings.Space(573);
                            }
                            else
                            {
                                // strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Strings.Space(13) + Strings.Space(13);
                                strTrailer = "9" + MaskNumber((dTotalAmt * 100), 18) + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(16) + Strings.Space(573);
                            }
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
        }
            return rt;
        
        }

        private Int32 FieldSummaryTotal(string valuestr)
        {
            int totcol = valuestr.Length;

            char[] chararray = valuestr.ToCharArray();


            Int32 asc_sum = 0;
            Int32 sum = 0;
            int i = 1;
            foreach (char ch in chararray)
            {
                int ascic = (int)ch;
                sum = sum + (i * ascic);
                i = i + 1;
            }

            //for (int i = 0; i <= totcol; i++)
            //{
            //    //asc_sum = Strings.Asc(valuestr.Substring(i - 1, 1));
            //    int asci= 
            //    sum += i * asc_sum;
            //}
            return sum;
        }

        public string GenerateGiroFile_NOR()
        {
            string rt;
            if (!ISnewformate)
            {
              rt=  GenerateGiroFile_UOB_oldformate();
            }
            else{

            //string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {

                string strGetValueDate = GetValueDate("2");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 34);
                string strGetOriginatorName = MaskString(GetOriginatorName(0), 140);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                string strGetOriginatingBICcode = GetOriginatingBICcode(strGetOriginatingBankCode);

                string strBlukCustomerRefernce = Strings.Left(strGetOriginatorName, 16);
                string EndtoEndID = MaskString("PAY" + DateTime.Today.ToString("yyyyMMdd"), 35);
                //string strfilename = "UGBI" + DateTime.Today.ToString("ddMM") + "01";
                isHash = true;

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                //-----------hash calculating header part
                Int32 total1, total2, sum1, sum2, sum3, sum4, sum5, sum6, sum7, ptype;
                // sum1 = FieldSummaryTotal("UOVBSGSGXXX");
                sum1 = FieldSummaryTotal(strGetOriginatingBICcode);
                sum2 = FieldSummaryTotal(strGetOriginatingAccountNumber);
                sum3 = FieldSummaryTotal(strGetOriginatorName);
                total1 = sum1 + sum2 + sum3;
                total2 = 0;

                if (isHash == true)
                {
                    ///fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + "2" + Strings.Space(9));
                    fp.WriteLine("1" + this.LogFileName.ToString().Substring(0, 10) + "R" + "NORMAL    " + "B" + Strings.Space(12) + strGetOriginatingBICcode + "SGD" + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(140) + strBlukCustomerRefernce + Strings.Space(10) + Strings.Space(210));
                }
                else
                {
                    // fp.WriteLine("1IBGINORM  " + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(5) + Strings.Space(1) + Strings.Space(9));
                    fp.WriteLine("1" + this.LogFileName.ToString().Substring(0, 10) + "R" + "NORMAL    " + "B" + Strings.Space(12) + strGetOriginatingBICcode + "SGD" + strGetOriginatingAccountNumber + strGetOriginatorName + DateTime.Today.ToString("yyyyMMdd") + GetValueDate("4") + Strings.Space(140) + strBlukCustomerRefernce + Strings.Space(10) + Strings.Space(210));
                }

                //working on UOB
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                // string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            string strBICcode = "";
                            int hashcode = 0;
                            int c = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    c++;
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);

                                    strbranchnumber = MaskStringForward(Utility.ToString(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3, "0");
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 34);
                                    strreceivingname = MaskString(row["account_name"].ToString().Trim(), 140);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 18);
                                    strBICcode = GetOriginatingBICcode(strgirobank);


                                    string dsan = Convert.ToString(MaskString(Utility.ToString(Replace(row["giro_acct_number"].ToString().Trim(), "-", "")), 11));
                                    string dsbn = strbranchnumber;
                                    string dsbc = Convert.ToString(MaskNumberBack(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4));
                                    ///  string sttotalamt = strnetpay; //Convert.ToString(MaskNumber(dTotalAmt,11));

                                    ///dsan = dsan.Replace(" ", "0");

                                    if (isHash == true)
                                    {
                                        ///-----------hash calculating details part
                                        if (hashcode == 9)
                                        {
                                            hashcode = 1;
                                        }
                                        else
                                        {
                                            hashcode = hashcode + 1;
                                        }

                                        sum1 = FieldSummaryTotal(strBICcode);
                                        sum2 = FieldSummaryTotal(strgiroaccountno) * hashcode;
                                        sum3 = FieldSummaryTotal(strreceivingname) * hashcode;
                                        // ptype = 22 * c;
                                        sum4 = 429;
                                        // int ss = FieldSummaryTotal("000000000000120000");
                                        sum5 = FieldSummaryTotal(strnetpay);
                                        sum6 = FieldSummaryTotal("SALA");
                                        sum7 = sum1 + sum2 + sum3 + sum4 + sum5 + sum6 + (22 * hashcode);

                                        total2 = total2 + sum7;




                                        sTrx = "2" + strBICcode + strgiroaccountno + strreceivingname + "SGD" + strnetpay + EndtoEndID + Strings.Space(35) + "SALA" + Strings.Space(140) + Strings.Space(140) + Strings.Space(16) + Strings.Space(38);
                                    }
                                    else
                                    {
                                        //sTrx = "2" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(12) + Strings.Space(12) + Strings.Space(4);
                                        sTrx = "2" + strBICcode + strgiroaccountno + strreceivingname + "SGD" + strnetpay + EndtoEndID + Strings.Space(35) + "SALA" + Strings.Space(140) + Strings.Space(140) + Strings.Space(16) + Strings.Space(38);
                                    }
                                    fp.WriteLine(sTrx);

                                }
                            }
                            if (isHash == true)
                            {
                                Int64 eventot = total1 + total2;
                                //ram
                                //old
                                //strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                                //new 
                                // strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + "0000000000000" + Utility.ToString(MaskNumber(Math.Round(eventot, 0), 13)) + Strings.Space(13);
                                strTrailer = "9" + MaskNumber((dTotalAmt * 100), 18) + MaskNumber(c, 7) + Utility.ToString(MaskNumber(eventot, 16)) + Strings.Space(573);
                            }
                            else
                            {
                                // strTrailer = "90000000000000" + MaskNumber((dTotalAmt * 100), 13) + "0000000" + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(13) + Strings.Space(13) + Strings.Space(13);
                                strTrailer = "9" + MaskNumber((dTotalAmt * 100), 18) + MaskNumber(ds.Tables[0].Rows.Count, 7) + Strings.Space(16) + Strings.Space(573);
                            }
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
        }
            return rt;
        }
        public string GenerateGiroFile_DBS_oldformate()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;
                //muru
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                fp = File.CreateText(strPath + this.LogFileName.ToString());
                fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + MaskNumber(Convert.ToDouble(this.BatchNo), 5) + strGetSenderCompanyID + Strings.Space(9) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));

                                    sTrx = strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(38) + Strings.Space(12) + Strings.Space(12) + "1";
                                    fp.WriteLine(sTrx);
                                }
                            }
                            strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        //public string GenerateGiroFile_ANZ()
        //{
        //    string rt;
        //    StreamWriter fp = default(StreamWriter);
        //    string sTrx = "";

        //    try
        //    {
        //        string strGetValueDate = GetValueDate("5");
        //        DateTime dtt = (DateTime)HttpContext.Current.Session["ValueDate"];
        //        DateTime valuedate = new DateTime(dtt.Year, dtt.Month, dtt.Day);


        //        string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
        //        string strGetOriginatingAccountNumber = MaskString(Replace(Strings.Left(GetOriginatingAccountNumber(0), 14), "-", ""), 14);
        //        string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
        //        string strGetSenderCompanyID = GetSenderCompanyID(8);
        //        string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
        //        double AccountHashTotal = 0;

        //        fp = File.CreateText(strPath + this.LogFileName.ToString());

        //        int i = 0;
        //        SqlParameter[] parms = new SqlParameter[7];
        //        parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //        parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //        parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //        parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //        parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //        parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //        parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

        //        //string sSQL = "sp_online_giro";
        //        string sSQL;
        //        if (HttpContext.Current.Session["mid-month"] != null)
        //        {
        //            sSQL = "sp_online_giro_MidMonth";
        //        }
        //        else
        //        {
        //            sSQL = "sp_online_giro";
        //        }

        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

        //            //update ds for mid month salary
        //            if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //            {
        //                ds = ChangeMidMonthSalNetPay(ds);
        //            }

        //            if (ds != null)
        //            {
        //                if (ds.Tables.Count > 0)
        //                {
        //                    string strTrailer = "";
        //                    double dTotalAmt = 0;
        //                    double iTotal = 0;
        //                    string strgirobank = "";
        //                    string strbranchnumber = "";
        //                    string strgiroaccountno = "";
        //                    string strreceivingname = "";
        //                    string strnetpay = "";
        //                    string _company_bankcode = "";
        //                    int kl = 0;
        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {

        //                        if (row["giro_bank"].ToString() != "")
        //                        {
        //                            _company_bankcode = row["company_bankcode"].ToString().Trim();
        //                            if (kl == 0)
        //                            {
        //                                fp.WriteLine("H" + MaskString(_company_bankcode, 10) + MaskString("PAYROLL", 10) + Strings.Space(1) + MaskString("SGPAYRLSGD", 10) + MaskString(strGetOriginatingAccountNumber, 20) + strGetValueDate + MaskString("PAYROLL", 20) + Strings.Space(160));
        //                            }
        //                            kl = kl + 1;
        //                            strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
        //                            strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
        //                            strgiroaccountno = MaskString(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11), 11);
        //                            strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
        //                            dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());

        //                            iTotal = Utility.ToDouble(row["netpay"].ToString());
        //                            strnetpay = MaskNumber(iTotal * 100, 15);
        //                            AccountHashTotal = AccountHashTotal + GetAccountHash(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""));


        //                            sTrx = "D" + Strings.Space(20) + MaskString("5", 20) + Strings.Space(20) + MaskString(strreceivingname, 140) + Strings.Space(275) + MaskString(strgirobank + strbranchnumber, 11) +
        //                                                                      Strings.Space(271) + MaskString(strgiroaccountno, 34) + "SGD" + MaskString("SINGAPORE", 150) + MaskString("SG", 1347) + MaskString(SenderAccountNo, 20) + "SGD" + Strings.Space(11) + "P"
        //                                                                     + strnetpay + Strings.Space(152) + MaskString("SALARY " + valuedate.ToString("MMM yyyy"), 40) + Strings.Space(1907);

        //                            fp.WriteLine(sTrx);
        //                        }


        //                    }
        //                    strTrailer = "T" + MaskNumber(ds.Tables[0].Rows.Count, 5) + MaskNumber((dTotalAmt * 100), 15) + Strings.Space(3479);

        //                    fp.WriteLine(strTrailer);
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + e.Message;
        //        }
        //        finally
        //        {
        //        }


        //        //fp.WriteLine("D" + strCustAcNo +  );
        //        rt = "File Succesfully created!";
        //        fp.Close();
        //    }
        //    catch (Exception err)
        //    {
        //        rt = "File Creation failed. Reason is as follows : " + err.Message;
        //    }
        //    finally
        //    {
        //    }
        //    return rt;
        //}

        public string GenerateGiroFile_DBS()
        {
            string rt;
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_DBS_oldformate();

            }
            else
            {
                StreamWriter fp = default(StreamWriter);
                string sTrx = "";

                try
                {
                    string strGetValueDate = GetValueDate("1");
                    string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                    string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                    string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                    string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                    string strGetSenderCompanyID = GetSenderCompanyID(8);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    double AccountHashTotal = 0;

                    if (!Directory.Exists(strPath)) 
                    {
                        Directory.CreateDirectory(strPath);
                    }

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + MaskNumber(Convert.ToDouble(this.BatchNo), 5) + strGetSenderCompanyID + Strings.Space(9) + "0");

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                    //string sSQL = "sp_online_giro";
                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }

                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                        //update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null)
                        {
                            ds = ChangeMidMonthSalNetPay(ds);
                        }
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strTrailer = "";
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string strnetpay = "";

                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                        strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                        strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                        strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                        dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                        iTotal = Utility.ToDouble(row["netpay"].ToString());
                                        strnetpay = MaskNumber(iTotal * 100, 11);
                                        AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));

                                        sTrx = strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(38) +"SAL  MONTHLY"+ Strings.Space(12) + "1";
                                        fp.WriteLine(sTrx);
                                    }
                                }
                                strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                                fp.WriteLine(strTrailer);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }

        public string GenerateGiroFile_ANZ()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("5");
                DateTime dtt = (DateTime)HttpContext.Current.Session["ValueDate"];
                DateTime valuedate = new DateTime(dtt.Year, dtt.Month, dtt.Day);


                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingAccountNumber = Replace(Strings.Left(GetOriginatingAccountNumber(0), 14), "-", "");
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            string strTrailer = "";
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            string _company_bankcode ="";
                            string strbiccode="";
                            int kl = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                if (row["giro_bank"].ToString() != "")
                                {
                                    _company_bankcode = row["company_bankcode"].ToString().Trim();
                                    if (kl == 0)
                                    {
                                        fp.WriteLine("H" + "|" + _company_bankcode + "|" + "PAYROLL" + "||" + "SGPG3PYSGD" + "|" + strGetOriginatingAccountNumber + "|" + strGetValueDate + "|" + "PAYROLL" + "|||||");
                                    }
                                    kl = kl + 1;
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbiccode=GetOriginatingBICcode(strgirobank);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = Replace(row["giro_acct_number"].ToString().Trim(), "-", "");
                                    if (strgirobank == "7232" || strgirobank == "7339" || strgirobank == "7791")
                                    {
                                        strgiroaccountno = row["Branch_Number"].ToString() + row["giro_acct_number"].ToString();
                                    }
                                    
                                    strreceivingname = row["account_name"].ToString().Trim();
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());

                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = (iTotal*100).ToString ();
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""));
                                                                        
                                    sTrx = "D" + "|||||" + strreceivingname + "|||" + strbiccode + "|||||" + "SG" + "|" + strgiroaccountno + "|||" + "SG" + "|||||||||||||||||||||||||||||||||||||||" + "SGD" + "|||" + "P" + "|" + strnetpay + "|||||||||||||||||||" + "SALA" + "|||||||||||||||||||";
                                        
                                    fp.WriteLine(sTrx);
                                }


                            }
                           
                            strTrailer = "T" + "|" + ds.Tables[0].Rows.Count + "|" + dTotalAmt*100 + "||";
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_OCBC_OLD()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingAccountNumber = MaskString(Replace(Strings.Left(GetOriginatingAccountNumber(0), 9), "-", ""), 9);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                fp.WriteLine("OVERSEA-CHINESE BANKING CORP GROUP" + Strings.Space(24) + "&&&&&&&&&&&&&&&&&&&&&&&&");
                fp.WriteLine(strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + DateTime.Today.ToString("yyyyMMdd") + MaskNumber(Convert.ToInt16(BatchNo), 3) + strGetOriginatorName + GetValueDate("4") + Strings.Space(31));

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);

                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""));

                                    sTrx = "00001" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + Strings.Space(3) + Strings.Space(9) + Strings.Space(12) + strnetpay + Strings.Space(2);
                                    fp.WriteLine(sTrx);
                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_OCBC()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_OCBC_OLD();
            }
            else
            {
                try
                {
                    string strGetValueDate = GetValueDate("1");
                    string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                    string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 31);
                    string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                    string strGetSenderCompanyID = GetSenderCompanyID(8);
                    string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    double AccountHashTotal = 0;

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    fp.WriteLine("30" + MaskNumber(Convert.ToInt16(BatchNo), 3) + DateTime.Today.ToString("yyyyMMdd") + GetOriginatingBICcode(strGetOriginatingBankCode) + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(147) + "GIRO" + Strings.Space(16) + GetValueDate("5") + Strings.Space(767));

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                    //string sSQL = "sp_online_giro";
                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }

                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                        //update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                        {
                            ds = ChangeMidMonthSalNetPay(ds);

                        }
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string strnetpay = "";
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                        strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                        strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 34);

                                        bankname = row["bank_Name"].ToString();

                                        if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                        {
                                            strgiroaccountno = strbranchnumber + strgiroaccountno;
                                            strgiroaccountno = Strings.Left(strgiroaccountno, 34);
                                        }

                                        strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 140);
                                        dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                        iTotal = Utility.ToDouble(row["netpay"].ToString());
                                        strnetpay = MaskNumber(iTotal * 100, 17);
                                        AccountHashTotal = AccountHashTotal + GetAccountHash(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""));

                                        sTrx = GetOriginatingBICcode(strgirobank) + strgiroaccountno + strreceivingname + "SGD" + strnetpay + Strings.Space(35) + "SALA" + Strings.Space(756);
                                        fp.WriteLine(sTrx);
                                        // sTrx = Strings.Space(100);
                                        //fp.WriteLine(sTrx);
                                    }

                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }

        //public string GenerateGiroFile_MAY()
        //{
        //    string rt;
        //    StreamWriter fp = default(StreamWriter);
        //    string sTrx = "";

        //    try
        //    {
        //        string strGetValueDate = GetValueDate("1");
        //        string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
        //        string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
        //        string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
        //        string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
        //        string strGetSenderCompanyID = GetSenderCompanyID(8);
        //        string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
        //        double AccountHashTotal = 0;

        //        fp = File.CreateText(strPath + this.LogFileName.ToString());
        //        //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

        //        int i = 0;
        //        SqlParameter[] parms = new SqlParameter[7];
        //        parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //        parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //        parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //        parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //        parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //        parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //        parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
        //        //string sSQL = "sp_online_giro";
        //        string sSQL;
        //        if (HttpContext.Current.Session["mid-month"] != null)
        //        {
        //            sSQL = "sp_online_giro_MidMonth";
        //        }
        //        else
        //        {
        //            sSQL = "sp_online_giro";
        //        }
        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


        //            //update ds for mid month salary
        //            if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //            {
        //                ds = ChangeMidMonthSalNetPay(ds);
        //            }

        //            if (ds != null)
        //            {
        //                if (ds.Tables.Count > 0)
        //                {
        //                    double dTotalAmt = 0;
        //                    double iTotal = 0;
        //                    string strTrailer = "";
        //                    string strgirobank = "";
        //                    string strbranchnumber = "";
        //                    string strgiroaccountno = "";
        //                    string strreceivingname = "";
        //                    string strnetpay = "";

        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {
        //                        if (row["giro_bank"].ToString() != "")
        //                        {
        //                            strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
        //                            strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
        //                            strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
        //                            strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
        //                            dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
        //                            iTotal = Utility.ToDouble(row["netpay"].ToString());

        //                            sTrx = strgirobank + "," + strbranchnumber + "," + strgiroaccountno + "," + iTotal.ToString("#0.00") + "," + strreceivingname.ToString().Trim() + ",-";
        //                            fp.WriteLine(sTrx);

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + e.Message;
        //        }
        //        finally
        //        {
        //        }


        //        //fp.WriteLine("D" + strCustAcNo +  );
        //        rt = "File Succesfully created!";
        //        fp.Close();
        //    }
        //    catch (Exception err)
        //    {
        //        rt = "File Creation failed. Reason is as follows : " + err.Message;
        //    }
        //    finally
        //    {
        //    }
        //    return rt;
        //}
        public string GenerateGiroFile_MAY()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            string strbiccode = "";
                            string strbankname = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbiccode = GetOriginatingBICcode(strgirobank);
                                    strbankname = GetOriginatingBankname(strgirobank);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                   // strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strgiroaccountno = Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""),34);
                                   // strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    strreceivingname = Strings.Left(row["account_name"].ToString().Trim(), 140);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());


                                   // sTrx = strgirobank + "," + strbranchnumber + "," + strgiroaccountno + "," + iTotal.ToString("#0.00") + "," + strreceivingname.ToString().Trim() + ",-";
                                    sTrx = strbiccode + ",," + strgiroaccountno + "," + iTotal.ToString("#0.00") + "," + strreceivingname + "," +Strings.Left("salary pay to " + strbankname,35);
                                    fp.WriteLine(sTrx);

                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        //bic
        public string GenerateGiroFile_BTMU_oldformate()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            double iRowCount = 0;
                            string strreccount = "";
                            string strtotpay = "";
                            iTotal = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iTotal = iTotal + Convert.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                strreccount = MaskStringForward(Utility.ToString(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 3, "0");
                                strtotpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 13, "0");
                                string strCon = "FD000000001   DMA000SALD 511" + strGetOriginatingAccountNumber + strGetValueDate + strGetValueDate + "001               " + strreccount + strtotpay + "SGD" + Strings.Space(45);
                                fp.WriteLine(strCon);
                            }

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iRowCount = iRowCount + 1;
                                    strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString());
                                    iTotal = Convert.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");
                                    sTrx = "FD000000001   DMB" + MaskStringForward(iRowCount.ToString(), 3, "0") + strreceivingname + strnetpay + strgiroaccountno + strgirobank + strbranchnumber + Strings.Space(59);
                                    fp.WriteLine(sTrx);

                                }
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                iRowCount = ds.Tables[0].Rows.Count + 1;
                                strreccount = MaskStringForward(Utility.ToString(iRowCount.ToString().Replace(".", "")), 3, "0");
                                sTrx = "FD000000001   DMC999" + strtotpay + Strings.Space(95);
                                fp.WriteLine(sTrx);
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        //public string GenerateGiroFile_BTMU()
        //{
        //    string rt;
        //    if (!ISnewformate)
        //    {
        //        rt = GenerateGiroFile_BTMU_oldformate();
        //    }
        //    else
        //    {
        //        StreamWriter fp = default(StreamWriter);
        //        string sTrx = "";

        //        try
        //        {
        //            string strGetValueDate = GetValueDate("4");
        //            string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
        //            string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
        //            string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
        //            string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
        //            string strGetSenderCompanyID = GetSenderCompanyID(8);
        //            string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
        //            double AccountHashTotal = 0;

        //            fp = File.CreateText(strPath + this.LogFileName.ToString());
        //            //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

        //            int i = 0;
        //            SqlParameter[] parms = new SqlParameter[7];
        //            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //            parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //            parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //            parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //            parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //            parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //            parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
        //            //string sSQL = "sp_online_giro";
        //            string sSQL;
        //            if (HttpContext.Current.Session["mid-month"] != null)
        //            {
        //                sSQL = "sp_online_giro_MidMonth";
        //            }
        //            else
        //            {
        //                sSQL = "sp_online_giro";
        //            }
        //            try
        //            {
        //                DataSet ds = new DataSet();
        //                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


        //                //update ds for mid month salary
        //                if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //                {
        //                    ds = ChangeMidMonthSalNetPay(ds);
        //                }
        //                if (ds != null)
        //                {
        //                    if (ds.Tables.Count > 0)
        //                    {
        //                        double dTotalAmt = 0;
        //                        double iTotal = 0;
        //                        string strTrailer = "";
        //                        string strgirobank = "";
        //                        string strbranchnumber = "";
        //                        string strgiroaccountno = "";
        //                        string strreceivingname = "";
        //                        string strnetpay = "";
        //                        double iRowCount = 0;
        //                        string strreccount = "";
        //                        string strtotpay = "";
        //                        iTotal = 0;
        //                        foreach (DataRow row in ds.Tables[0].Rows)
        //                        {
        //                            if (row["giro_bank"].ToString() != "")
        //                            {
        //                                iTotal = iTotal + Convert.ToDouble(row["netpay"].ToString());
        //                            }
        //                        }

        //                        //if (ds.Tables[0].Rows.Count > 0)
        //                        //{

        //                        //    strreccount = MaskStringForward(Utility.ToString(ds.Tables[0].Rows.Count.ToString().Replace(".", "")),3,"0");
        //                        //    strtotpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 13, "0");
        //                        //    string strCon = "FD000000001   DMA000SALD 511" + strGetOriginatingAccountNumber + strGetValueDate + strGetValueDate + "001               " + strreccount + strtotpay + "SGD" + Strings.Space(45);
        //                        //    fp.WriteLine(strCon);
        //                        //}

        //                        foreach (DataRow row in ds.Tables[0].Rows)
        //                        {
        //                            if (row["giro_bank"].ToString() != "")
        //                            {
        //                                iRowCount = iRowCount + 1;
        //                                strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
        //                                strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
        //                                strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
        //                                strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
        //                                dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString());
        //                                iTotal = Convert.ToDouble(row["netpay"].ToString());
        //                                //strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");
        //                                strnetpay = Utility.ToString(iTotal.ToString("#0.00"));
        //                                // sTrx = "FD000000001   DMB" + MaskStringForward(iRowCount.ToString(), 3, "0") + strreceivingname + strnetpay + strgiroaccountno + strgirobank + strbranchnumber + Strings.Space(59);
        //                                // fp.WriteLine(sTrx);

        //                                sTrx = "\"\"" + "," + "\"GI\"" + "," + "\"SALA\"" + "," + "\"T\"" + "," + "\"" + strGetValueDate + "\"" + "," + "\"" + strGetOriginatingAccountNumber + "\"" + "," + "\"SGD\"" + ","
        //                                     + "\"" + strnetpay + "\"" + "," + "\"" + strreceivingname.TrimEnd() + "\"" + ","
        //                                     + "\"\"" + "," + "\"\"" + "," + "\"\"" + ","

        //                                    + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + ","

        //                                    + "\"" + strgiroaccountno.Trim() + "\"" + ","
        //                                    + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + row["BIC_code"].ToString() + "\"" + "," +
        //                                    "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\""
        //                                    + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"";

        //                                fp.WriteLine(sTrx);

        //                            }
        //                        }
        //                        //if (ds.Tables[0].Rows.Count > 0)
        //                        //{
        //                        //    iRowCount = ds.Tables[0].Rows.Count + 1;
        //                        //    strreccount = MaskStringForward(Utility.ToString(iRowCount.ToString().Replace(".", "")), 3, "0");
        //                        //    sTrx = "FD000000001   DMC999" + strtotpay + Strings.Space(95);
        //                        //    fp.WriteLine(sTrx);
        //                        //}

        //                    }
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                rt = "File Creation failed. Reason is as follows : " + e.Message;
        //            }
        //            finally
        //            {
        //            }


        //            //fp.WriteLine("D" + strCustAcNo +  );
        //            rt = "File Succesfully created!";
        //            fp.Close();
        //        }
        //        catch (Exception err)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + err.Message;
        //        }
        //        finally
        //        {
        //        }
        //    }
        //    return rt;
        //}


        //new dbs diskette giro
        //Changes only in header
        public string GenerateGiroFile_BTMU()
        {
            string rt;
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_BTMU_oldformate();
            }
            else
            {
                StreamWriter fp = default(StreamWriter);
                string sTrx = "";

                try
                {
                    string strGetValueDate = GetValueDate("4");
                    string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                    string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                    string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                    string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                    string strGetSenderCompanyID = GetSenderCompanyID(8);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    double AccountHashTotal = 0;

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));
                    //string sSQL = "sp_online_giro";
                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }
                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                        //update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                        {
                            ds = ChangeMidMonthSalNetPay(ds);
                        }
                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strTrailer = "";
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string strnetpay = "";
                                double iRowCount = 0;
                                string strreccount = "";
                                string strtotpay = "";
                                iTotal = 0;
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        iTotal = iTotal + Convert.ToDouble(row["netpay"].ToString());
                                    }
                                }

                                //if (ds.Tables[0].Rows.Count > 0)
                                //{

                                //    strreccount = MaskStringForward(Utility.ToString(ds.Tables[0].Rows.Count.ToString().Replace(".", "")),3,"0");
                                //    strtotpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 13, "0");
                                //    string strCon = "FD000000001   DMA000SALD 511" + strGetOriginatingAccountNumber + strGetValueDate + strGetValueDate + "001               " + strreccount + strtotpay + "SGD" + Strings.Space(45);
                                //    fp.WriteLine(strCon);
                                //}

                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        iRowCount = iRowCount + 1;
                                        strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                        strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                        strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                        strreceivingname = row["account_name"].ToString().Trim();
                                        dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString());
                                        iTotal = Convert.ToDouble(row["netpay"].ToString());
                                        //strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");
                                        strnetpay = Utility.ToString(iTotal.ToString("#0.00"));
                                        // sTrx = "FD000000001   DMB" + MaskStringForward(iRowCount.ToString(), 3, "0") + strreceivingname + strnetpay + strgiroaccountno + strgirobank + strbranchnumber + Strings.Space(59);
                                        // fp.WriteLine(sTrx);

                                        sTrx = "\"\"" + "," + "\"GI\"" + "," + "\"SALA\"" + "," + "\"T\"" + "," + "\"" + strGetValueDate + "\"" + "," + "\"" + GetOriginatingAccountNumber(8).Trim() + "\"" + "," + "\"SGD\"" + ","
                                             + "\"" + strnetpay + "\"" + "," + "\"" + strreceivingname + "\"" + ","
                                             + "\"\"" + "," + "\"\"" + "," + "\"\"" + ","

                                            + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + ","

                                            + "\"" + strgiroaccountno.Trim() + "\"" + ","
                                            + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"" + row["BIC_code"].ToString() + "\"" + "," +
                                            "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\""
                                            + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"" + "," + "\"\"";

                                        fp.WriteLine(sTrx);

                                    }
                                }
                                //if (ds.Tables[0].Rows.Count > 0)
                                //{
                                //    iRowCount = ds.Tables[0].Rows.Count + 1;
                                //    strreccount = MaskStringForward(Utility.ToString(iRowCount.ToString().Replace(".", "")), 3, "0");
                                //    sTrx = "FD000000001   DMC999" + strtotpay + Strings.Space(95);
                                //    fp.WriteLine(sTrx);
                                //}

                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }
        double dTotalAmt = 0;
        DataSet ds = new DataSet();
        string Girodate, GiroEnddate;
        public string GenerateGiroFile_DBS_Diskete()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                //string strGetSenderCompanyID = GetSenderCompanyID(8);
                //new
                string strGetSenderCompanyID = GetSenderCompanyID(5);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(12) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }
                try
                {
                    //DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            // double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));

                                    sTrx = strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(17) + Strings.Space(6) + Strings.Space(15) + Strings.Space(12) + Strings.Space(12) + "1";



                                    fp.WriteLine(sTrx);

                                }
                            }
                            strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();



                #region Generating Information file

                string sql11 = "SELECT  convert(varchar, PaySubStartDate, 105),convert(varchar, PaySubEndDate, 105)    FROM   PayrollMonthlyDetail  WHERE  ROWID ='" + this.SalMonth + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql11, null);
                if (dr.Read())
                {
                    GiroEnddate = dr[1].ToString();
                    Girodate = dr[1].ToString();

                }


                StreamWriter fp1 = default(StreamWriter);
                fp1 = File.CreateText(strPath + "Information.Txt");
                fp1.WriteLine(GetOriginatorName(0) + Strings.Space(30) + "Page: 1");
                fp1.WriteLine(" ");
                fp1.WriteLine("Banker Order List for the Period Ending " + GiroEnddate);
                fp1.WriteLine(" ");
                fp1.WriteLine("DEVELOPMENT BANK OF  SPORE (" + strGetSenderCompanyID + ")");
                fp1.WriteLine(" ");

                fp1.WriteLine(Strings.Space(10) + "************************ Control Summary ***********************");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(Strings.Space(25) + "Company's name :" + GetOriginatorName(0));
                fp1.WriteLine(Strings.Space(25) + "Account No     :" + strGetOriginatingAccountNumber);
                fp1.WriteLine(Strings.Space(25) + "Company's Code :" + strGetSenderCompanyID);
                fp1.WriteLine(Strings.Space(25) + "No. Of Records :" + ds.Tables[0].Rows.Count);
                fp1.WriteLine(Strings.Space(25) + "Total Amount   :" + dTotalAmt);
                fp1.WriteLine(Strings.Space(25) + "A/C Hash Total :" + MaskNumber(AccountHashTotal, 11));
                fp1.WriteLine(Strings.Space(25) + "Batch Number   :" + "00001");
                fp1.WriteLine(Strings.Space(25) + "Value Date     :" + GetValueDate("2"));
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(" ");
                fp1.WriteLine(Strings.Space(25) + Strings.Space(25) + "--------------------");
                fp1.WriteLine(Strings.Space(25) + Strings.Space(25) + "Authorised Signature");
                fp1.Close();
                #endregion

            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        //public string GenerateGiroFile_SEB()  //Senthil Added On 24/11/2015
        //  {
        //    string rt;
        //    StreamWriter fp = default(StreamWriter);
        //    string sTrx = "";

        //    try
        //    {
        //        string strGetValueDate = GetValueDate("1");
        //        string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
        //        string strGetOriginatingAccountNumber = MaskString(Replace(Strings.Left(GetOriginatingAccountNumber(0), 9), "-", ""), 9);
        //        string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
        //        string strGetSenderCompanyID = GetSenderCompanyID(8);
        //        string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
        //        double AccountHashTotal = 0;

        //        fp = File.CreateText(strPath + this.LogFileName.ToString());
        //        fp.WriteLine("SKANDINAVISKA ENSKILDA BANKEN" + Strings.Space(24) + "&&&&&&&&&&&&&&&&&&&&&&&&");
        //        fp.WriteLine(strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + DateTime.Today.ToString("yyyyMMdd") + "001" + strGetOriginatorName + GetValueDate("4") + Strings.Space(31));

        //        int i = 0;
        //        SqlParameter[] parms = new SqlParameter[7];
        //        parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
        //        parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
        //        parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
        //        parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
        //        parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
        //        parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
        //        parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

        //        //string sSQL = "sp_online_giro";
        //        string sSQL;
        //        if (HttpContext.Current.Session["mid-month"] != null)
        //        {
        //            sSQL = "sp_online_giro_MidMonth";
        //        }
        //        else
        //        {
        //            sSQL = "sp_online_giro";
        //        }

        //        try
        //        {
        //            DataSet ds = new DataSet();
        //            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

        //            //update ds for mid month salary
        //            if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
        //            {
        //                ds = ChangeMidMonthSalNetPay(ds);

        //            }
        //            if (ds != null)
        //            {
        //                if (ds.Tables.Count > 0)
        //                {
        //                    double dTotalAmt = 0;
        //                    double iTotal = 0;
        //                    string strgirobank = "";
        //                    string strbranchnumber = "";
        //                    string strgiroaccountno = "";
        //                    string strreceivingname = "";
        //                    string strnetpay = "";
        //                    foreach (DataRow row in ds.Tables[0].Rows)
        //                    {
        //                        if (row["giro_bank"].ToString() != "")
        //                        {
        //                            strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
        //                            strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
        //                            strgiroaccountno = MaskString(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11), 11);
        //                            strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
        //                            dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
        //                            iTotal = Utility.ToDouble(row["netpay"].ToString());
        //                            strnetpay = MaskNumber(iTotal * 100, 11);
        //                            AccountHashTotal = AccountHashTotal + GetAccountHash(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""));

        //                            sTrx = "00001" + strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + Strings.Space(3) + Strings.Space(9) + Strings.Space(12) + strnetpay + Strings.Space(2);
        //                            fp.WriteLine(sTrx);
        //                        }

        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            rt = "File Creation failed. Reason is as follows : " + e.Message;
        //        }
        //        finally
        //        {
        //        }


        //        //fp.WriteLine("D" + strCustAcNo +  );
        //        rt = "File Succesfully created!";
        //        fp.Close();
        //    }
        //    catch (Exception err)
        //    {
        //        rt = "File Creation failed. Reason is as follows : " + err.Message;
        //    }
        //    finally
        //    {
        //    }
        //    return rt;
        //}

        public string GenerateGiroFile_SEB()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("4");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }


                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            decimal dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            fp = File.CreateText(strPath + this.LogFileName.ToString());
                            fp.WriteLine(":E1:" + "SEBDOMESTIC/D");
                            fp.WriteLine(":E2:" + "ESSESGSGXXX");
                            fp.WriteLine(":E3:" + "N" + this.SenderAccountNo);
                            fp.WriteLine(":E4:" + strGetValueDate);
                            fp.WriteLine(":E5:" + "Remitter reference");
                            int j = 1;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {

                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Decimal.Parse(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));

                                    fp.WriteLine(":32:" + row["netpay"].ToString());
                                    fp.WriteLine(":57A:" + "/" + row["giro_acct_number"].ToString().Trim());
                                    fp.WriteLine("//" + row["BIC_code"].ToString());
                                    fp.WriteLine(":59:" + strreceivingname);
                                    fp.WriteLine(":70:" + "Payroll for" + " " + Convert.ToDateTime(row["PayrollDate"].ToString()).ToString("MMM-yy"));
                                    fp.WriteLine(":72:" + row["PaymentType"].ToString().Trim());

                                }
                                j++;
                            }
                            fp.WriteLine(":Z1:" + ds.Tables[0].Rows.Count);
                            fp.WriteLine(":Z2:" + dTotalAmt);

                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }






        public void generateXMLData()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement xmlElmPerson = xmlDoc.CreateElement("Person");
            xmlDoc.AppendChild(xmlElmPerson);

            XmlElement xmlElmName = xmlDoc.CreateElement("Name");
            xmlElmPerson.AppendChild(xmlElmName);
            xmlElmName.InnerText = "Fred";

            XmlElement xmlElmDOB = xmlDoc.CreateElement("DateOfBirth");
            xmlElmPerson.AppendChild(xmlElmDOB);
            xmlElmDOB.InnerText = "1978-06-26";

            XmlElement xmlElmAddress = xmlDoc.CreateElement("Address");
            xmlElmPerson.AppendChild(xmlElmAddress);

            XmlElement xmlElmHouseNo = xmlDoc.CreateElement("HouseNo");
            xmlElmAddress.AppendChild(xmlElmHouseNo);
            xmlElmHouseNo.InnerText = "7";

            XmlElement xmlElmPostCode = xmlDoc.CreateElement("PostCode");
            xmlElmAddress.AppendChild(xmlElmPostCode);
            xmlElmPostCode.InnerText = "WV6 6JY";

            XmlElement xmlElmCarRA = xmlDoc.CreateElement("Car");
            xmlElmPerson.AppendChild(xmlElmCarRA);

            XmlElement xmlElmRAModel = xmlDoc.CreateElement("Make");
            xmlElmCarRA.AppendChild(xmlElmRAModel);
            xmlElmRAModel.InnerText = "Ford";

            XmlElement xmlElmRAMake = xmlDoc.CreateElement("Model");
            xmlElmCarRA.AppendChild(xmlElmRAMake);
            xmlElmRAMake.InnerText = "Escort";

            XmlElement xmlElmCarToy = xmlDoc.CreateElement("Car");
            xmlElmPerson.AppendChild(xmlElmCarToy);

            XmlElement xmlElmToyModel = xmlDoc.CreateElement("Make");
            xmlElmCarToy.AppendChild(xmlElmToyModel);
            xmlElmToyModel.InnerText = "Lotus";

            XmlElement xmlElmToyMake = xmlDoc.CreateElement("Model");
            xmlElmCarToy.AppendChild(xmlElmToyMake);
            xmlElmToyMake.InnerText = "Elise";

            using (StringWriter sr = new StringWriter())
            {
                XmlWriterSettings xws = new XmlWriterSettings();
                xws.Indent = true;

                using (XmlWriter xtw = XmlTextWriter.Create(sr, xws))
                {
                    xmlDoc.WriteTo(xtw);
                }

            }
        }
        string Period, bankname;
        public string GenerateLog()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";


            string sql11 = "select (convert(varchar,MonthName)+' '+convert(varchar,[Year]))as Period from PayrollMonthlyDetail  WHERE  ROWID ='" + this.SalMonth + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql11, null);
            if (dr.Read())
            {
                Period = dr[0].ToString();
            }


            string sql12 = "select [desc] from bank where id='" + SenderBank + "'";
            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql12, null);
            if (dr11.Read())
            {
                bankname = dr11[0].ToString();
            }

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                //string strGetSenderCompanyID = GetSenderCompanyID(8);
                //new
                string strGetSenderCompanyID = GetSenderCompanyID(5);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                fp.WriteLine("Company Name:" + GetOriginatorName(0));
                fp.WriteLine("");
                fp.WriteLine("Company Bank :" + bankname);
                fp.WriteLine("");
                fp.WriteLine("Company Bank Branch:" + strGetOriginatingBranchNumber);
                fp.WriteLine("");
                fp.WriteLine("Bank Account:" + strGetOriginatingAccountNumber);
                fp.WriteLine("");
                fp.WriteLine("Payroll Month:" + Period);
                fp.WriteLine("");
                //fp.WriteLine("IC            Emp Name            " + Strings.Space(5) + "BankCode" + Strings.Space(5) + "branch code" + Strings.Space(5) + " AccountNo " + Strings.Space(0) +"  Transfer Amount");
                fp.WriteLine("IC            Emp Name                                 " + Strings.Space(5) + "BankCode" + Strings.Space(5) + "branch code" + Strings.Space(5) + " AccountNo " + Strings.Space(0) + "  Transfer Amount");
                fp.WriteLine("*************************************************************************************************************************");
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(12) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                string sSQL = "sp_online_giro";
                try
                {
                    //DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            // double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    //strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 40), 40);

                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));


                                    string emp_code = row["emp_id"].ToString();
                                    string SQL_timecard = "select ic_pp_number from employee where emp_code='" + emp_code + "'";
                                    string ppno = "-";
                                    SqlDataReader dr_pp = DataAccess.ExecuteReader(CommandType.Text, SQL_timecard, null);
                                    if (dr_pp.HasRows)
                                    {
                                        while (dr_pp.Read())
                                        {
                                            ppno = Utility.ToString(dr_pp.GetValue(0));
                                        }
                                    }



                                    double net = Utility.ToDouble(row["netpay"].ToString());

                                    //Emp_Name, Emp_BankCode,branch code, Emp_AccountNo and TransferAmt
                                    //sTrx = ppno + Strings.Space(5) + strreceivingname + Strings.Space(5) + strgirobank + Strings.Space(10) + strbranchnumber + Strings.Space(12) + strgiroaccountno + Strings.Space(5) + net.ToString("F2"); 

                                    string val = Convert.ToString(net.ToString("F2"));
                                    sTrx = ppno + Strings.Space(5) + strreceivingname + Strings.Space(5) + strgirobank + Strings.Space(10) + strbranchnumber + Strings.Space(12) + strgiroaccountno + Strings.Space(5) + Strings.Space(10 - (val.Length)) + val;



                                    fp.WriteLine(sTrx);

                                }
                            }
                            // strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                            //fp.WriteLine("****************************************************************************************************");
                            fp.WriteLine("*************************************************************************************************************************");
                            fp.WriteLine(strTrailer);
                            fp.WriteLine("Total Transfer Amount: " + dTotalAmt);
                            fp.WriteLine("");
                            fp.WriteLine("Total Head Count: " + ds.Tables[0].Rows.Count);
                        }
                    }
                }
                catch (Exception e)
                {

                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                    fp.WriteLine(rt);
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();





            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateLog_mc()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";


            string sql11 = "select (convert(varchar,MonthName)+' '+convert(varchar,[Year]))as Period from PayrollMonthlyDetail  WHERE  ROWID ='" + this.SalMonth + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql11, null);
            if (dr.Read())
            {
                Period = dr[0].ToString();
            }


            string sql12 = "select [desc] from bank where id='" + SenderBank + "'";
            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql12, null);
            if (dr11.Read())
            {
                bankname = dr11[0].ToString();
            }

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Replace(GetOriginatingAccountNumber(0), "-", ""), 11);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                //string strGetSenderCompanyID = GetSenderCompanyID(8);
                //new
                string strGetSenderCompanyID = GetSenderCompanyID(5);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());

                fp.WriteLine("Company Name:" + GetOriginatorName(0));
                fp.WriteLine("");
                fp.WriteLine("Company Bank :" + bankname);
                fp.WriteLine("");
                fp.WriteLine("Company Bank Branch:" + strGetOriginatingBranchNumber);
                fp.WriteLine("");
                fp.WriteLine("Bank Account:" + strGetOriginatingAccountNumber);
                fp.WriteLine("");
                fp.WriteLine("Payroll Month:" + Period);
                fp.WriteLine("");
                //fp.WriteLine("IC            Emp Name            " + Strings.Space(5) + "BankCode" + Strings.Space(5) + "branch code" + Strings.Space(5) + " AccountNo " + Strings.Space(0) +"  Transfer Amount");
                fp.WriteLine("IC            Emp Name                                 " + Strings.Space(5) + "BankCode" + Strings.Space(5) + "branch code" + Strings.Space(5) + " AccountNo " + Strings.Space(0) + "  Transfer Amount");
                fp.WriteLine("*************************************************************************************************************************");
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(12) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                string sSQL = "Sp_online_giro_midmonth";
                try
                {
                    //DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    ds = ChangeMidMonthSalNetPay(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            // double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";

                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    strgirobank = MaskNumber(Utility.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Utility.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    //strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 40), 40);

                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));


                                    string emp_code = row["emp_id"].ToString();
                                    string SQL_timecard = "select ic_pp_number from employee where emp_code='" + emp_code + "'";
                                    string ppno = "-";
                                    SqlDataReader dr_pp = DataAccess.ExecuteReader(CommandType.Text, SQL_timecard, null);
                                    if (dr_pp.HasRows)
                                    {
                                        while (dr_pp.Read())
                                        {
                                            ppno = Utility.ToString(dr_pp.GetValue(0));
                                        }
                                    }


                                    double net = Utility.ToDouble(row["netpay"].ToString());

                                    //Emp_Name, Emp_BankCode,branch code, Emp_AccountNo and TransferAmt
                                    //sTrx = ppno + Strings.Space(5) + strreceivingname + Strings.Space(5) + strgirobank + Strings.Space(10) + strbranchnumber + Strings.Space(12) + strgiroaccountno + Strings.Space(5) + net.ToString("F2"); 

                                    string val = Convert.ToString(net.ToString("F2"));
                                    sTrx = ppno + Strings.Space(5) + strreceivingname + Strings.Space(5) + strgirobank + Strings.Space(10) + strbranchnumber + Strings.Space(12) + strgiroaccountno + Strings.Space(5) + Strings.Space(10 - (val.Length)) + val;



                                    fp.WriteLine(sTrx);

                                }
                            }
                            // strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                            //fp.WriteLine("****************************************************************************************************");
                            fp.WriteLine("*************************************************************************************************************************");
                            fp.WriteLine(strTrailer);
                            fp.WriteLine("Total Transfer Amount: " + dTotalAmt);
                            fp.WriteLine("");
                            fp.WriteLine("Total Head Count: " + ds.Tables[0].Rows.Count);
                        }
                    }
                }
                catch (Exception e)
                {

                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                    fp.WriteLine(rt);
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();





            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        //bic
        public string GenerateGiroFile_BNP_oldformate()
        {
            string rt, bankname;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                string sSQL = "sp_online_giro";
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                    ds = ChangeMidMonthSalNetPay(ds);


                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            double iRowCount = 0;
                            string strreccount = "";
                            string strtotpay = "";


                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iRowCount = iRowCount + 1;
                                    strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                    dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString());
                                    iTotal = Convert.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");
                                    //sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";" + GetCompanyName1() + ";SGD;" + iTotal.ToString("#0.00") + ";" + GetCompanyName() + ";" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";SG;" + CheckDBS(row["bank_Name"].ToString()) + ";SINGAPORE;SG;" + GetValueDate("2") + ";13;LC";
                                    //new
                                    /*Reference
                                    Swift address of ordering party - BNPASGSG 
                                    Ordering party account (debiting account) - 00001-012345-001-23-SGD 
                                    Currency - SGD 
                                    Payment Amount - 1,888.00 
                                    Beneficiary country code - SG 
                                    Beneficiary account no. - 73395012345678 
                                    Beneficiary name - HAREEN DONG 
                                    Beneficiary bank's name - OCBC BANK 
                                    Beneficiary bank's country - SINGAPORE 
                                    Execution date - 28/01/2013 
                                    */
                                    //                                    sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";SGD;" + iTotal.ToString("#0.00") + ";" + "SG;" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";" + row["account_name"].ToString() + ";" + CheckDBS(row["bank_Name"].ToString()) + ";SINGAPORE;" + GetValueDate("2");


                                    bankname = row["bank_Name"].ToString();
                                    if (bankname == "DBS/POSB")
                                    {
                                        if (Replace(row["giro_acct_number"].ToString().Trim(), "-", "").Length == 9)
                                        {
                                            bankname = "POSB";
                                        }
                                        else
                                        {
                                            bankname = "DBS";
                                        }
                                    }


                                    //sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";SGD;" + iTotal.ToString("0,0.00", CultureInfo.InvariantCulture) + ";" + "SG;" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";" + row["account_name"].ToString() + ";" + CheckDBS(row["bank_Name"].ToString()) + ";SINGAPORE;" + GetValueDate("2");
                                    sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";SGD;" + iTotal.ToString("0,0.00", CultureInfo.InvariantCulture) + ";" + "SG;" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";" + row["account_name"].ToString() + ";" + bankname + ";SINGAPORE;" + GetValueDate("2");
                                    fp.WriteLine(sTrx);

                                }
                            }


                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                    fp.WriteLine(rt);
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

        public string GenerateGiroFile_BNP()
        {



            string rt, bankname;
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_BNP_oldformate();
            }
            else
            {

                StreamWriter fp = default(StreamWriter);
                string sTrx = "";

                try
                {
                    string strGetValueDate = GetValueDate("1");
                    string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                    string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                    string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                    string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                    string strGetSenderCompanyID = GetSenderCompanyID(8);
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    double AccountHashTotal = 0;

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                    string sSQL = "sp_online_giro";
                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                        //  update ds for mid month salary
                        if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                        {
                            ds = ChangeMidMonthSalNetPay(ds);
                        }

                        //  update ds for mid month salary



                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strTrailer = "";
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string strnetpay = "";
                                double iRowCount = 0;
                                string strreccount = "";
                                string strtotpay = "";


                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {
                                        iRowCount = iRowCount + 1;
                                        strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                        strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                        strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                        strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 20), 20);
                                        dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString());
                                        iTotal = Convert.ToDouble(row["netpay"].ToString());
                                        strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");
                                        //sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";" + GetCompanyName1() + ";SGD;" + iTotal.ToString("#0.00") + ";" + GetCompanyName() + ";" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";SG;" + CheckDBS(row["bank_Name"].ToString()) + ";SINGAPORE;SG;" + GetValueDate("2") + ";13;LC";
                                        //new
                                        /*Reference
                                        Swift address of ordering party - BNPASGSG 
                                        Ordering party account (debiting account) - 00001-012345-001-23-SGD 
                                        Currency - SGD 
                                        Payment Amount - 1,888.00 
                                        Beneficiary country code - SG 
                                        Beneficiary account no. - 73395012345678 
                                        Beneficiary name - HAREEN DONG 
                                        Beneficiary bank's name - OCBC BANK 
                                        Beneficiary bank's country - SINGAPORE 
                                        Execution date - 28/01/2013 
                                        */
                                        //if (!ISnewformate)
                                        //{
                                        //    sTrx = "BNPASGSG;00001-" + GetOriginatingAccountNumber(0) + ";SGD;" + iTotal.ToString("#0.00") + ";" + "SG;" + row["giro_bank"].ToString() + row["branch_number"].ToString() + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + ";" + row["account_name"].ToString() + ";" + CheckDBS(row["bank_Name"].ToString()) + ";SINGAPORE;" + GetValueDate("2");
                                        //}



                                        bankname = row["bank_Name"].ToString();

                                        if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                        {
                                            strgiroaccountno = strbranchnumber + strgiroaccountno;
                                        }

                                        if (bankname == "DBS/POSB")
                                        {
                                            if (Replace(row["giro_acct_number"].ToString().Trim(), "-", "").Length == 9)
                                            {
                                                bankname = "POSB";
                                            }
                                            else
                                            {
                                                bankname = "DBS";
                                            }
                                        }




                                        sTrx = "BNPASGSG;00001" + GetOriginatingAccountNumber(0).Replace("-", "").Trim() + ";SGD;" + iTotal.ToString("0,0.00", CultureInfo.InvariantCulture) + ";" + row["account_name"].ToString() + ";" + "SG;" +
                                            Replace(strgiroaccountno.Trim(), "-", "") + ";" + row["BIC_code"].ToString() + ";" + GetValueDate("6") + ";" + "" + ";" + DateTime.Now.ToString("ddMMyyyy") + BatchNo + ";SALA";


                                        fp.WriteLine(sTrx);

                                    }
                                }


                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                        fp.WriteLine(rt);
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }



       // Bank Of America
        public string GenerateGiroFile_BOA_old()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strGetValueDate = GetValueDate("1");
                string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 35), 35);
                string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                string sSQL = "sp_online_giro";
                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                    //update ds for mid month salary
                   // ds = ChangeMidMonthSalNetPay(ds);
                    if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                    {
                        ds = ChangeMidMonthSalNetPay(ds);
                    }

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            double iRowCount = 0;
                            string strreccount = "";
                            string strtotpay = "";
                            iTotal = 0;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iTotal = iTotal + Convert.ToDouble(row["netpay"].ToString());
                                }
                            }

                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                strreccount = MaskStringForward(Utility.ToString(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 3, "0");
                                strtotpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 13, "0");
                                //Header
                                string strCon = "\"BULKCSV\" ,\"" + GetCompanyName(0) + "\"," + "\"LEGOAU07\"" + "," + "\"NHTP\",\"" + GetValueDate("4") + "\"," + "\"FILE.TXT\",\"PROD\"";
                                fp.WriteLine(strCon);
                            }


                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {
                                    iRowCount = iRowCount + 1;
                                    strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                    strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                    strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                    strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 70), 70);
                                    dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString().Replace(".", ""));
                                    iTotal = Convert.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");


                                    #region bank_Name - to check (posb or DBS)
                                    bankname = row["bank_Name"].ToString();
                                    if (bankname == "DBS/POSB")
                                    {
                                        if (Replace(row["giro_acct_number"].ToString().Trim(), "-", "").Length == 9)
                                        {
                                            bankname = "POSB";
                                        }
                                        else
                                        {
                                            bankname = "DBS";
                                        }
                                    }
                                    #endregion

                                    #region main Record

                                    //PAYMENT TYPE, ORIGINATING ACCOUNT AND INSTRUCTIONS (TRN.0-TRN.14)
                                    sTrx = "\"TRN\",\"PAYROL\"," + "\"\",\"\",\"6212\",\"57951018\",\"\",\"SGD\",\"PAY\",\"C\",\"C\",\"SGPayroll" + strGetValueDate + "\",\"1\",\"" + GetRandomNumber(100, 10000000) + "\"";

                                    //Order party   
                                    sTrx = sTrx + ",\"\",\"" + GetCompanyName() + "\",\"\",\"" + GetCompanyInfo("address") + " \",\"\",\"SINGAPORE\",\"\",\"" + GetCompanyInfo("postal_code") + "\",\"SG\",";
                                    //Receiving Party
                                    sTrx = sTrx + "\"" + Strings.Left(row["account_name"].ToString().Trim(), 70) + "\",\"\",\"\",\"\",\"Singapore\",\"\",\"\",\"SG\",";

                                    //TRANSACTION DETAILS AND CHARGES  
                                    sTrx = sTrx + "\"" + GetValueDate("4") + "\",\"\",\"SGD\",\"" + Convert.ToDouble(row["netpay"].ToString()) + "\",\"OUR\",\"\",";
                                    //RECEIVING BANK DETAILS
                                    sTrx = sTrx + "\"" + bankname + "\",\"\",\"" + strgirobank + strbranchnumber + "\",\"\",\"" + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + "\",\"\",\"SINGAPORE\",\"SG\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",";
                                    //COUNTRY SPECIAL INFORMATION
                                    sTrx = sTrx + "\"REF\",\"SALARY\",";
                                    ////ALTERNATIVE REMITTANCE ADDRESS  
                                    sTrx = sTrx + "\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"\",\"" + strGetValueDate + "PAYROLL\"";

                                    fp.WriteLine(sTrx);

                                    #endregion


                                    #region next Line
                                    sTrx = "\"RDF\",\"FTX\",\"SALARY\"";
                                    fp.WriteLine(sTrx);
                                    #endregion

                                }
                            }
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                iRowCount = ds.Tables[0].Rows.Count + 1;
                                strreccount = MaskStringForward(Utility.ToString(iRowCount.ToString().Replace(".", "")), 3, "0");
                                sTrx = "\"BULKCSVTRAILER\",\"" + ds.Tables[0].Rows.Count + "\",\"" + Convert.ToString(dTotalAmt).Replace(".", "") + "\"";
                                fp.WriteLine(sTrx);
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }

       
        public string GenerateGiroFile_BOA()
        {
         
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";
              string rt, bankname;
              if (!ISnewformate)
              {
                  rt = GenerateGiroFile_BOA_old();
              }
              else
              {
                  try
                  {
                      string strGetValueDate = GetValueDate("1");
                      string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                      string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                      string strGetOriginatingAccountNumber = MaskString(Strings.Left(Replace(GetOriginatingAccountNumber(0), "-", ""), 6), 6);
                      string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 35), 35);
                      string strGetSenderCompanyID = GetSenderCompanyID(8);
                      string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                      double AccountHashTotal = 0;

                      fp = File.CreateText(strPath + this.LogFileName.ToString());
                      //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");

                      int i = 0;
                      SqlParameter[] parms = new SqlParameter[7];
                      parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                      parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                      parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                      parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                      parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                      parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                      parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));

                      string sSQL = "sp_online_giro";
                      try
                      {
                          DataSet ds = new DataSet();
                          ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


                          //update ds for mid month salary
                          // ds = ChangeMidMonthSalNetPay(ds);
                          if (HttpContext.Current.Session["mid-month"] != null) //Senthil-Added On-2/11/2015
                          {
                              ds = ChangeMidMonthSalNetPay(ds);
                          }

                          if (ds != null)
                          {
                              if (ds.Tables.Count > 0)
                              {
                                  double dTotalAmt = 0;
                                  double iTotal = 0;
                                  string strTrailer = "";
                                  string strgirobank = "";
                                  string strbranchnumber = "";
                                  string strgiroaccountno = "";
                                  string strreceivingname = "";
                                  string strnetpay = "";
                                  double iRowCount = 0;
                                  string strreccount = "";
                                  string strtotpay = "";
                                  iTotal = 0;
                                  foreach (DataRow row in ds.Tables[0].Rows)
                                  {
                                      if (row["giro_bank"].ToString() != "")
                                      {
                                          iTotal = iTotal + Convert.ToDouble(row["netpay"].ToString());
                                      }
                                  }

                                  if (ds.Tables[0].Rows.Count > 0)
                                  {

                                      strreccount = MaskStringForward(Utility.ToString(ds.Tables[0].Rows.Count.ToString().Replace(".", "")), 3, "0");
                                      strtotpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 13, "0");
                                      //Header
                                      string strCon = "\"BULKCSV\",\"" + GetCompanyName(0) + "\"," + "\"LEGOAU07\"" + "," + "\"NHTP\",\"" + GetValueDate("4") + "\"," + "\"FILE.TXT\",\"PROD\"";
                                      fp.WriteLine(strCon);
                                  }


                                  foreach (DataRow row in ds.Tables[0].Rows)
                                  {
                                      if (row["giro_bank"].ToString() != "")
                                      {
                                          iRowCount = iRowCount + 1;
                                          strgirobank = MaskNumber(Convert.ToDouble(Strings.Left(row["giro_bank"].ToString().Trim(), 4)), 4);
                                          strbranchnumber = MaskNumber(Convert.ToDouble(Strings.Left(row["branch_number"].ToString().Trim(), 3)), 3);
                                          strgiroaccountno = MaskString(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11);
                                          strreceivingname = MaskString(Strings.Left(row["account_name"].ToString().Trim(), 70), 70);
                                          dTotalAmt = dTotalAmt + Convert.ToDouble(row["netpay"].ToString().Replace(".", ""));
                                          iTotal = Convert.ToDouble(row["netpay"].ToString());
                                          strnetpay = MaskStringForward(Utility.ToString(iTotal.ToString("#0.00").Replace(".", "")), 11, "0");



                                          #region bank_Name - to check (posb or DBS)
                                          bankname = row["bank_Name"].ToString();
                                          if (bankname == "DBS/POSB")
                                          {
                                              if (Replace(row["giro_acct_number"].ToString().Trim(), "-", "").Length == 9)
                                              {
                                                  bankname = "POSB";
                                              }
                                              else
                                              {
                                                  bankname = "DBS";
                                              }
                                          }
                                          #endregion
                                          if (strgirobank == "7339" || strgirobank == "9548" || strgirobank == "7791")
                                          {
                                              strgiroaccountno = strbranchnumber + strgiroaccountno;
                                          }
                                          #region main Record

                                          //PAYMENT TYPE, ORIGINATING ACCOUNT AND INSTRUCTIONS (TRN.0-TRN.14)
                                          sTrx = "\"TRN\",\"PAYROL\"," + ",,\"6212\",\"57951018\",,\"SGD\",\"PAY\",\"C\",\"C\",\"SGPayroll\"" + ",\"1\",\"" + GetRandomNumber(100, 10000000) + "\",,";

                                          //Order party   
                                          sTrx = sTrx + "\"" + GetCompanyName() + "\",,\"" + GetCompanyInfo("address") + " \",,\"SINGAPORE\",,\"" + GetCompanyInfo("postal_code") + "\",\"SG\",";
                                          //Receiving Party
                                          sTrx = sTrx + "\"" + Strings.Left(row["account_name"].ToString().Trim(), 70) + "\",,,,\"SINGAPORE\",,,\"SG\",";

                                          //TRANSACTION DETAILS AND CHARGES  
                                          double npay = Convert.ToDouble(row["netpay"]);
                                          sTrx = sTrx + "\"" + GetValueDate("4") + "\",,\"SGD\",\"" + npay.ToString("#0.00") + "\",\"OUR\",,";
                                          //RECEIVING BANK DETAILS
                                          sTrx = sTrx + "\"" + bankname + "\",,\"" + strgirobank + strbranchnumber + "\",\"" + GetOriginatingBICcode(strgirobank) + "\",\"" + Replace(row["giro_acct_number"].ToString().Trim(), "-", "") + "\",,\"SINGAPORE\",\"SG\",,,,,,,,,,,";
                                          //COUNTRY SPECIAL INFORMATION
                                          sTrx = sTrx + "\"REF\",\"SALARY\",";
                                          ////ALTERNATIVE REMITTANCE ADDRESS  
                                          sTrx = sTrx + ",,,,,,,,,,,,,,,,,";

                                          fp.WriteLine(sTrx);

                                          #endregion


                                          #region next Line
                                          sTrx = "\"RDF\",\"FTX\",\"SALARY\"";
                                          fp.WriteLine(sTrx);
                                          #endregion

                                      }
                                  }
                                  if (ds.Tables[0].Rows.Count > 0)
                                  {
                                      iRowCount = ds.Tables[0].Rows.Count + 1;
                                      strreccount = MaskStringForward(Utility.ToString(iRowCount.ToString().Replace(".", "")), 3, "0");
                                      sTrx = "\"BULKCSVTRAILER\",\"" + ds.Tables[0].Rows.Count + "\",\"" + Convert.ToString(dTotalAmt).Replace(".", "") + "\"";
                                      fp.WriteLine(sTrx);
                                  }

                              }
                          }
                      }
                      catch (Exception e)
                      {
                          rt = "File Creation failed. Reason is as follows : " + e.Message;
                      }
                      finally
                      {
                      }


                      //fp.WriteLine("D" + strCustAcNo +  );
                      rt = "File Succesfully created!";
                      fp.Close();
                  }
                  catch (Exception err)
                  {
                      rt = "File Creation failed. Reason is as follows : " + err.Message;
                  }
                  finally
                  {
                  }
              }
            return rt;
        }
        public string GenerateGiroFile_JPMorgan_oldformate()
        {
            string rt;
            StreamWriter fp = default(StreamWriter);
            string sTrx = "";

            try
            {
                string strFH = "FH";
                string strClientID = "1234567890";
                string strDate = DateTime.Now.Date.Year + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
                string strTime = DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                string strVersion = "01100";
                //string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                //string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(0);
                //string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                //string strGetSenderCompanyID = GetSenderCompanyID(8);
                string strTR = "";
                string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                double AccountHashTotal = 0;

                string strHeader = strFH + "," + strClientID + "," + strDate + "," + strTime + "," + strVersion;

                fp = File.CreateText(strPath + this.LogFileName.ToString());
                //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");
                fp.WriteLine(strHeader);
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                //string sSQL = "sp_online_giro";
                string sSQL;
                if (HttpContext.Current.Session["mid-month"] != null)
                {
                    sSQL = "sp_online_giro_MidMonth";
                }
                else
                {
                    sSQL = "sp_online_giro";
                }

                try
                {
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                    //update ds for mid month salary
                    ds = ChangeMidMonthSalNetPay(ds);

                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            double dTotalAmt = 0;
                            double iTotal = 0;
                            string strTrailer = "";
                            string strgirobank = "";
                            string strbranchnumber = "";
                            string strgiroaccountno = "";
                            string strreceivingname = "";
                            string strnetpay = "";
                            double count = 1;
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (row["giro_bank"].ToString() != "")
                                {


                                    strgirobank = row["giro_bank"].ToString();
                                    strbranchnumber = row["branch_number"].ToString();
                                    strgiroaccountno = row["giro_acct_number"].ToString();
                                    strreceivingname = row["account_name"].ToString();
                                    dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                    iTotal = Utility.ToDouble(row["netpay"].ToString());
                                    strnetpay = MaskNumber(iTotal * 100, 11);
                                    AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));


                                    string strRefNo = "Ref" + MaskNumber(count, 13);
                                    string strValueDate = dtJPMorgan.Date.Year + dtJPMorgan.ToString("MM") + dtJPMorgan.ToString("dd");
                                    string strDestCountryCode = "SG";
                                    string strDestBankBIC = "";
                                    string strDestBankID = strgirobank + strbranchnumber;
                                    string strDestAC = row["giro_acct_number"].ToString().Replace(@"-", "");
                                    string strTrxAmount = Convert.ToString(iTotal * 100);
                                    string strCurrency = "SGD";
                                    string strPaymentMethod = "GIR";
                                    string strTrxType = "01";
                                    string strTrxCategory = "22";
                                    string strOriginatorAC = strGetOriginatingAccountNumber;
                                    string strBankCharges = "R";
                                    string strCorrespCharges = "B";
                                    string strDestACName = strreceivingname;
                                    string strOptionFields = ",,,,,,,,,,,,,,,,,,,,,,";
                                    //string strAdvisingMedia = "EM";
                                    //string strAdvisingValue = "";

                                    //string strSQL = "Select email from employee where emp_code=" + row["emp_id"].ToString() + "";
                                    //DataSet dsEmp = new DataSet();
                                    //dsEmp = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                                    //strAdvisingMedia = dsEmp.Tables[0].Rows[0][0].ToString();

                                    //sTrx = "TR," + strRefNo + "," + strValueDate + "," + strDestCountryCode + "," + strDestBankBIC + "," + strDestBankID + "," + strDestAC + ",," + strTrxAmount + "," + strCurrency + "," + strPaymentMethod + "," +
                                    //    strPaymentMethod + "," + strTrxType + "," + strTrxCategory + "," + strOriginatorAC + "," + strBankCharges + "," + strCorrespCharges + "," + strreceivingname + strOptionFields + "EM" + "," + strAdvisingValue;

                                    sTrx = "TR," + strRefNo + "," + strValueDate + "," + strDestCountryCode + "," + strDestBankBIC + "," + strDestBankID + "," + strDestAC + ",," + strTrxAmount + "," + strCurrency + "," + strPaymentMethod + "," +
                                         strTrxType + "," + strTrxCategory + "," + strOriginatorAC + "," + strBankCharges + "," + strCorrespCharges + "," + strreceivingname + strOptionFields;



                                    //sTrx = strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(38) + Strings.Space(12) + Strings.Space(12) + "1";
                                    fp.WriteLine(sTrx);

                                    count = count + 1;

                                }
                            }
                            count = count - 1;
                            strTrailer = "FT," + count + "," + (count + 2) + "," + (dTotalAmt * 100);
                            // strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                            fp.WriteLine(strTrailer);
                        }
                    }
                }
                catch (Exception e)
                {
                    rt = "File Creation failed. Reason is as follows : " + e.Message;
                }
                finally
                {
                }


                //fp.WriteLine("D" + strCustAcNo +  );
                rt = "File Succesfully created!";
                fp.Close();
            }
            catch (Exception err)
            {
                rt = "File Creation failed. Reason is as follows : " + err.Message;
            }
            finally
            {
            }
            return rt;
        }
        public string GenerateGiroFile_JPMorgan()
        {
            string rt;
            if (!ISnewformate)
            {
                rt = GenerateGiroFile_JPMorgan_oldformate();

            }
            else
            {


                StreamWriter fp = default(StreamWriter);
                string sTrx = "";

                try
                {
                    string strFH = "FH";
                    string strClientID = "1234567890";
                    string strDate = DateTime.Now.Date.Year + DateTime.Now.ToString("MM") + DateTime.Now.ToString("dd");
                    string strTime = DateTime.Now.ToString("hh") + DateTime.Now.ToString("mm") + DateTime.Now.ToString("ss");
                    string strVersion = "01100";
                    //string strGetOriginatingBranchNumber = MaskString(Strings.Left(GetOriginatingBranchNumber(3), 3), 3);
                    //string strGetOriginatingBankCode = MaskString(Strings.Left(GetOriginatingBankCode(4), 4), 4);
                    string strGetOriginatingAccountNumber = GetOriginatingAccountNumber(0);
                    //string strGetOriginatorName = MaskString(Strings.Left(GetOriginatorName(0), 20), 20);
                    //string strGetSenderCompanyID = GetSenderCompanyID(8);
                    string strTR = "";
                    string strPath = System.Web.HttpContext.Current.Server.MapPath(this.LogFilePath);
                    double AccountHashTotal = 0;

                    string strHeader = strFH + "," + strClientID + "," + strDate + "," + strTime + "," + strVersion;

                    fp = File.CreateText(strPath + this.LogFileName.ToString());
                    //fp.WriteLine(strGetValueDate + Strings.Space(45) + strGetOriginatingBankCode + strGetOriginatingBranchNumber + strGetOriginatingAccountNumber + Strings.Space(2) + strGetOriginatorName + "00001" + strGetSenderCompanyID + Strings.Space(9) + "0");
                    fp.WriteLine(strHeader);
                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(this.CompanyID));
                    parms[i++] = new SqlParameter("@month", Utility.ToInteger(this.SalMonth));
                    parms[i++] = new SqlParameter("@year", Utility.ToInteger(this.SalYear));
                    parms[i++] = new SqlParameter("@bank", Utility.ToInteger(this.SenderBank));
                    parms[i++] = new SqlParameter("@bankaccno", Utility.ToString(this.SenderAccountNo));
                    parms[i++] = new SqlParameter("@emp_list", Utility.ToString(this.sEmployeeList));
                    parms[i++] = new SqlParameter("@valuedate", Utility.ToInteger(this.ValueDate));


                    //string sSQL = "sp_online_giro";
                    string sSQL;
                    if (HttpContext.Current.Session["mid-month"] != null)
                    {
                        sSQL = "sp_online_giro_MidMonth";
                    }
                    else
                    {
                        sSQL = "sp_online_giro";
                    }

                    try
                    {
                        DataSet ds = new DataSet();
                        ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                        //update ds for mid month salary
                        ds = ChangeMidMonthSalNetPay(ds);

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                double dTotalAmt = 0;
                                double iTotal = 0;
                                string strTrailer = "";
                                string strgirobank = "";
                                string strbranchnumber = "";
                                string strgiroaccountno = "";
                                string strreceivingname = "";
                                string strnetpay = "";
                                string bankBICCode = "";
                                double count = 1;
                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (row["giro_bank"].ToString() != "")
                                    {


                                        strgirobank = row["giro_bank"].ToString();
                                        strbranchnumber = row["branch_number"].ToString();
                                        strgiroaccountno = row["giro_acct_number"].ToString();
                                        strreceivingname = row["account_name"].ToString();
                                        bankBICCode = row["BIC_code"].ToString();
                                        dTotalAmt = dTotalAmt + Utility.ToDouble(row["netpay"].ToString());
                                        iTotal = Utility.ToDouble(row["netpay"].ToString());
                                        strnetpay = MaskNumber(iTotal * 100, 11);
                                        AccountHashTotal = AccountHashTotal + GetAccountHash(Strings.Left(Replace(row["giro_acct_number"].ToString().Trim(), "-", ""), 11));


                                        string strRefNo = "Ref" + MaskNumber(count, 13);
                                        string strValueDate = dtJPMorgan.Date.Year + dtJPMorgan.ToString("MM") + dtJPMorgan.ToString("dd");
                                        string strDestCountryCode = "SG";

                                        string strDestBankID = strgirobank + strbranchnumber;
                                        string strDestAC = row["giro_acct_number"].ToString().Replace(@"-", "");
                                        string strTrxAmount = Convert.ToString(iTotal * 100);
                                        string strCurrency = "SGD";
                                        string strPaymentMethod = "GIR";
                                        string strTrxType = "01";
                                        string strTrxCategory = "22";
                                        string strOriginatorAC = strGetOriginatingAccountNumber;
                                        string strBankCharges = "R";
                                        string strCorrespCharges = "B";
                                        string strDestACName = strreceivingname;
                                        string strOptionFields = ",,,,,,,,,,,,,,";
                                        //string strAdvisingMedia = "EM";
                                        //string strAdvisingValue = "";

                                        //string strSQL = "Select email from employee where emp_code=" + row["emp_id"].ToString() + "";
                                        //DataSet dsEmp = new DataSet();
                                        //dsEmp = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                                        //strAdvisingMedia = dsEmp.Tables[0].Rows[0][0].ToString();

                                        //sTrx = "TR," + strRefNo + "," + strValueDate + "," + strDestCountryCode + "," + strDestBankBIC + "," + strDestBankID + "," + strDestAC + ",," + strTrxAmount + "," + strCurrency + "," + strPaymentMethod + "," +
                                        //    strPaymentMethod + "," + strTrxType + "," + strTrxCategory + "," + strOriginatorAC + "," + strBankCharges + "," + strCorrespCharges + "," + strreceivingname + strOptionFields + "EM" + "," + strAdvisingValue;



                                        sTrx = "TR," + strRefNo + "," + strValueDate + "," + strDestCountryCode + "," + bankBICCode + ",,"
                                            + strDestAC + ",," + strTrxAmount + ","
                                            + strCurrency + "," + strPaymentMethod + "," +
                                             strTrxType + "," + strTrxCategory + "," + strOriginatorAC + "," + strBankCharges
                                             + "," + strCorrespCharges + "," + strreceivingname + strOptionFields + "SALA" + ",,,,,,,,,,,,,,,,,,,,,,,,,";



                                        //sTrx = strgirobank + strbranchnumber + strgiroaccountno + strreceivingname + "22" + strnetpay + Strings.Space(38) + Strings.Space(12) + Strings.Space(12) + "1";



                                        fp.WriteLine(sTrx);

                                        count = count + 1;

                                    }
                                }
                                count = count - 1;
                                strTrailer = "FT," + count + "," + (count + 2) + "," + (dTotalAmt * 100);
                                // strTrailer = MaskNumber(ds.Tables[0].Rows.Count, 8) + MaskNumber((dTotalAmt * 100), 11) + Strings.Space(5) + "0000000000000000000" + Strings.Space(26) + MaskNumber(AccountHashTotal, 11) + Strings.Space(33) + "9";
                                fp.WriteLine(strTrailer);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        rt = "File Creation failed. Reason is as follows : " + e.Message;
                    }
                    finally
                    {
                    }


                    //fp.WriteLine("D" + strCustAcNo +  );
                    rt = "File Succesfully created!";
                    fp.Close();
                }
                catch (Exception err)
                {
                    rt = "File Creation failed. Reason is as follows : " + err.Message;
                }
                finally
                {
                }
            }
            return rt;
        }


        //Generic method to get company information
        private string GetCompanyInfo(string val)
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT " + val + " FROM company WHERE company_id = " + this.CompanyID;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                functionReturnValue = ds.Tables[0].Rows[0][val].ToString().Trim();

                if (functionReturnValue != "")
                {
                    // if (intMask > 0)
                    {
                        functionReturnValue = functionReturnValue;
                    }
                }
                else
                {
                    functionReturnValue = val + " not found in the database.";
                }
            }
            return functionReturnValue;
        }

        #region Generate random Number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        #endregion



        private string GetCompanyName()
        {
            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT company_name FROM company WHERE company_id = " + this.CompanyID;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["company_name"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    // if (intMask > 0)
                    {
                        functionReturnValue = functionReturnValue;
                    }
                }
                else
                {
                    functionReturnValue = "Company Name not found in the database.";
                }
            }
            return functionReturnValue;
        }

        private string CheckDBS(string bankname)
        {
            if (bankname == "DBS/POSB")
            {
                return "DBS";
            }
            else
                return bankname;
        }

        private string GetCompanyName1()
        {

            string functionReturnValue = null;
            string sSQL = null;
            //string sRetVal = null;
            DataSet ds = new DataSet();
            sSQL = "SELECT company_code FROM company WHERE company_id = " + this.CompanyID;
            ds = GetDataSet(sSQL);
            if (ds.Tables[0].Rows.Count > 0)
            {

                functionReturnValue = ToStringConv(ds.Tables[0].Rows[0]["company_code"].ToString().Trim());

                if (functionReturnValue != "")
                {
                    //if (intMask > 0)
                    {
                        functionReturnValue = functionReturnValue;
                    }
                }
                else
                {
                    functionReturnValue = "Company Name not found in the database.";
                }
            }
            return functionReturnValue;

        }




    }
}
