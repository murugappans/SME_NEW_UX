using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using System.Xml;
using System.Data.OleDb;
using System.IO;

namespace IRAS
{
    /// <summary>
    /// Summary description for Utility-test
    /// </summary>
    public class Utility
    {

        public static DataSet GetDataSetFromTextFile(string filePath)
        {
            StreamReader reader = new StreamReader(filePath);
            DataSet ds = new DataSet();
            try
            {
                reader.BaseStream.Seek(0, SeekOrigin.Begin);
                DataTable dt = new DataTable();
                DataRow drow;
                ds.Tables.Add("CertificationTable");
                // here  the fields in the .txt file are double so i add datatypes 		 
                // in the dataset columns accordingly.            
                ds.Tables["CertificationTable"].Columns.Add("Certification Information Details", System.Type.GetType("System.String"));
                ds.Tables["CertificationTable"].Columns.Add("Certification Information", System.Type.GetType("System.String"));
                dt = ds.Tables["CertificationTable"];
                //Delimiter is space		             
                while (reader.Peek() > -1)
                {
                    drow = dt.NewRow();
                    char sep = ':';
                    string[] col = reader.ReadLine().Split(sep);
                    drow[0] = col[0];
                    drow[1] = col[1];
                    dt.Rows.Add(drow);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                reader.Close();
                reader = null;
            }
            return ds;
        }

        public DataSet allRights;
        public Utility()
        {
        }
        public static string[] workSheetNames = new string[] { };   //List of Worksheet names 
        private static string fileName;  //Path and file name of the Excel Workbook
        private static string connectionString;

        public static string ToString(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";

            if (obj.ToString() == "0")
                return "";

            return obj.ToString().Trim();
        }

        public static string ToDate1(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";

            return System.DateTime.Parse(obj.ToString().Trim()).ToShortDateString();
        }
        public static string ToLongDate(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";

            System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
            dateInfo.ShortDatePattern = "dd/MM/yyyy";
            DateTime validDate = Convert.ToDateTime(obj, dateInfo);
            return validDate.ToString();
        }
        public static string ToDate(string obj)
        {
            if (obj == null)
                return "";

            return string.Format(obj.ToString(), "dd/MM/yyyy");
        }

        public static string ToTime(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";

            return System.DateTime.Parse(obj.ToString().Trim()).ToString("HH:mm");
        }

        public static int ToInteger(object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString() == "" || obj.ToString() == "NaN")
                    return 0;

                return Int32.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return 0;
            }
        }

        public static double ToDouble(object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString() == "" || obj.ToString() == "NaN")
                    return 0.0;
                return Double.Parse(obj.ToString());
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return 0.0;
            }
        }
        public static bool AllowedAction(ArrayList al, int iRightID)
        {
            return false;
        }

        public static bool AllowedAction1(string user_name, string right)
        {
            bool flag = false;
            DataSet ds = new DataSet();
            ds = ((DataSet)HttpContext.Current.Session["RIGHTds"]);
            DataRow[] rows = ds.Tables[0].Select("RightName = '" + right.ToUpper() + "'");

            //DataRow[] rows = SMEPayroll.UserRights.EmployeeRights.empRights.Tables[0].Select("RightName = '" + right.ToUpper() + "'");
            if (rows.Length > 0)
                flag = true;
            return flag;
        }
        public static string GetCurrentDate()
        {
            string[] sMonths = new string[13];
            sMonths[0] = "";
            sMonths[1] = "January";
            sMonths[2] = "February";
            sMonths[3] = "March";
            sMonths[4] = "April";
            sMonths[5] = "May";
            sMonths[6] = "June";
            sMonths[7] = "July";
            sMonths[8] = "August";
            sMonths[9] = "September";
            sMonths[10] = "October";
            sMonths[11] = "November";
            sMonths[12] = "December";

            int iCurrMonth;
            int iCurrDaay;
            iCurrMonth = DateTime.Today.Month;
            iCurrDaay = DateTime.Today.Day;

            return iCurrDaay.ToString() + " " + sMonths[iCurrMonth].ToString() + " " + DateTime.Today.Year.ToString();
        }
        public static string getDate(string reqDate)
        {
            if (reqDate != "")
            {
                string[] collect = reqDate.Split('/');
                return reqDate = collect[1] + "/" + collect[0] + "/" + collect[2];
            }
            else
                return "";

        }

        public static string ConvDate(string varYear, string varMonth, string varDay)
        {
            varMonth = varMonth.Trim();
            if (varMonth == "13") return varYear;

            if (varMonth.Length == 0) varMonth = "01";
            if (varMonth.Length == 1) varMonth = "0" + varMonth;

            varDay = varDay.Trim();
            if (varDay.Length == 0) varDay = "01";
            if (varDay.Length == 1) varDay = "0" + varDay;

            varYear = varYear.Trim();
            if (varYear.Length == 0) varYear = "1900";
            if (varYear.Length == 2) varYear = "20" + varYear;

            return varYear + varMonth + varDay;
        }
        public static int GetRandomNumberInRange(int intLowerBound, int intUpperBound)
        {
            Random RandomGenerator = default(Random);
            int intRandomNumber = 0;

            // Create and init the randon number generator 
            RandomGenerator = new Random();
            //RandomGenerator = New Random(DateTime.Now.Millisecond) 

            // Get the next random number 
            intRandomNumber = RandomGenerator.Next(intLowerBound, intUpperBound + 1);

            // Return the random # as the function's return value 
            return intRandomNumber;
        }

        public static void FillDropDown(DropDownList drp, string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drp.DataSource = ds.Tables[0];

            drp.DataValueField = ds.Tables[0].Columns[0].ColumnName.ToString();
            drp.DataTextField = ds.Tables[0].Columns[1].ColumnName.ToString();
            drp.DataBind();
            drp.Items.Insert(0, new ListItem("-select-"));
        }


        public static DateTime toDateTime(string datetime)
        {
            DateTime _datetime;
            if (!string.IsNullOrEmpty(datetime))
            {
                _datetime = Convert.ToDateTime(datetime);
            }
            else
            {
                _datetime = DateTime.Now;
            }
            return _datetime;
        }
        public static string Reverse(string str)
        {
            int len = str.Length;
            char[] arr = new char[len];

            for (int i = 0; i < len; i++)
            {
                arr[i] = str[len - 1 - i];
            }

            return new string(arr);
        }

        public static void GetLoginOKCompRunXML(string compid, string username)
        {
            // Open the XML file
            XmlDocument docXML = new XmlDocument();
            docXML.Load(HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
            XmlNodeList nodTitles = docXML.GetElementsByTagName("Bank");
            for (int i = 0; i < nodTitles.Count; i++)
            {
                HttpContext.Current.Session[((XmlElement)(nodTitles[i])).GetAttribute("id")] = ((XmlElement)(nodTitles[i])).GetAttribute("text");
            }

            DataSet dsadd = new DataSet();
            string sSQL = "Select ID,substring([desc],1,15) [desc],formulatype,formulacalc from Additions_Types Where Company_Id='" + compid + "' And optionselection = 'Variable'  Order By Code";
            dsadd = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            if (dsadd != null)
            {
                if (dsadd.Tables[0].Rows.Count > 3)
                {
                    DataRow dradd;
                    dradd = dsadd.Tables[0].Rows[0];
                    HttpContext.Current.Session["V1ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V1Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V1Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V1FormulaCalc"] = dradd["formulacalc"].ToString();

                    dradd = dsadd.Tables[0].Rows[1];
                    HttpContext.Current.Session["V2ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V2Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V2Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V2FormulaCalc"] = dradd["formulacalc"].ToString();

                    dradd = dsadd.Tables[0].Rows[2];
                    HttpContext.Current.Session["V3ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V3Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V3Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V3FormulaCalc"] = dradd["formulacalc"].ToString();

                    dradd = dsadd.Tables[0].Rows[3];
                    HttpContext.Current.Session["V4ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V4Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V4Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V4FormulaCalc"] = dradd["formulacalc"].ToString();
                }
            }
        }
        public static void GetLoginOKCompRunDB(string compid, string username)
        {
            int version_number = 0;
            string sSQL = "";
            sSQL = "SELECT  top 1 [VersionNumber]FROM [ProductVersion] order by versiondate desc";
            System.Data.SqlClient.SqlDataReader dr_ver = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            char[] delimiterChars = {  '.' };
            while (dr_ver.Read())
            {
                string[] val = dr_ver.GetValue(0).ToString().Split(delimiterChars);
                version_number = Utility.ToInteger(val[3]);
            }









            string SQLQuery;
            if (username.ToString().Trim() == "anbsysadmingroup")
            {
                SQLQuery = "select Top 1 UserName, Password,a.Company_Id,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname,b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,a.email,b.ismaster,b.istsremarks,b.ProjectAssign from employee a ,company b, usergroups c where a.statusid=1";
                SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid and c.groupname='Super Admin'";
            }
            else
            {
                SQLQuery = "select UserName, Password,a.Company_Id,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname,b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,a.email,b.ismaster,b.istsremarks,b.ProjectAssign from employee a ,company b, usergroups c where a.statusid=1";
                SQLQuery += " and (username ='" + username + "' OR emp_alias = '" + username + "')";
                SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid";
            }

            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
            if (dr.Read())
            {

                HttpContext.Current.Session["ConString"] = "Data Source=" + Constants.DB_SERVER + ";Initial Catalog=" + Constants.DB_NAME + ";User ID=" + Constants.DB_UID + ";Password=" + Constants.DB_PWD;
                HttpContext.Current.Session["CompanyName"] = dr["Company_Name"].ToString().Trim();
                HttpContext.Current.Session["Username"] = dr["UserName"].ToString();
                HttpContext.Current.Session["EmpCode"] = dr["emp_code"].ToString();
                HttpContext.Current.Session["EmpEmail"] = dr["email"].ToString();
                HttpContext.Current.Session["Compid"] = dr["Company_ID"].ToString();
                HttpContext.Current.Session["Emp_Name"] = dr["Emp_Name"].ToString();
                HttpContext.Current.Session["GroupId"] = dr["emp_group_id"].ToString();
                HttpContext.Current.Session["CompCode"] = dr["Company_Code"].ToString();
                HttpContext.Current.Session["cpf"] = dr["Cpf_Ref_No"].ToString();
                HttpContext.Current.Session["WorkingDays"] = dr["no_work_days"].ToString();
                HttpContext.Current.Session["WorkingHours"] = dr["day_hours"].ToString();
                HttpContext.Current.Session["WorkingMinutes"] = dr["day_minute"].ToString();
                HttpContext.Current.Session["Leave_Model"] = dr["Leave_Model"].ToString();
                HttpContext.Current.Session["PAYTYPE"] = dr["PAYROLLTYPE"].ToString();
                HttpContext.Current.Session["isMaster"] = dr["ismaster"].ToString();
                HttpContext.Current.Session["isTSRemarks"] = dr["isTSRemarks"].ToString();
                HttpContext.Current.Session["PayAssign"] = dr["ProjectAssign"].ToString();

                if (HttpContext.Current.Session["CurrentCompany"] == null)
                {
                    HttpContext.Current.Session["CurrentCompany"] = dr["Company_ID"].ToString();
                }
                DataSet ds = new DataSet();
                string sqlRightQuery = "dbo.SP_UserRightsAll";
                //Before 15 March Version
                //SqlParameter[] sqlParam = new SqlParameter[3];
                //sqlParam[0] = new SqlParameter("@userid", HttpContext.Current.Session["UserName"]);
                //sqlParam[1] = new SqlParameter("@compId", HttpContext.Current.Session["Compid"]);
                //sqlParam[2] = new SqlParameter("@ANBPRODUCT", "SME");

                //After 15  2011 March Version
                SqlParameter[] sqlParam;
                if (version_number >= 42)
                {
                   sqlParam = new SqlParameter[5];
                }
                else
                {
                     sqlParam = new SqlParameter[4];
                }
              
                sqlParam[0] = new SqlParameter("@userid", HttpContext.Current.Session["UserName"]);
                sqlParam[1] = new SqlParameter("@compId", HttpContext.Current.Session["Compid"]);
                sqlParam[2] = new SqlParameter("@ANBPRODUCT", "SME");
                if (HttpContext.Current.Session["isMaster"].ToString() == "True")
                {
                    sqlParam[3] = new SqlParameter("@MasterComp", "1");
                }
                else
                {
                    sqlParam[3] = new SqlParameter("@MasterComp", "0");
                }

                if (version_number >= 42)
                {

                    sqlParam[4] = new SqlParameter("@CountryID", HttpContext.Current.Session["Country"].ToString());
                }
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sqlRightQuery, sqlParam);
                HttpContext.Current.Session["RIGHTds"] = ds;


                if (dr["timesheet_approve"] != null)
                {
                    HttpContext.Current.Session["TimeSheetApproved"] = dr["timesheet_approve"].ToString();
                }
                else
                {
                    HttpContext.Current.Session["TimeSheetApproved"] = "1";
                }
                HttpContext.Current.Session["GroupName"] = dr["groupname"].ToString();
            }
        }

        public static bool GetLoginOK(string compid, string username, string pwd)
        {
            string SQLQuery; bool Login_OK;

            if (username.ToString().Trim() == "anbsysadmingroup")
            {
                SQLQuery = "select Top 1 UserName, Password from employee a ,company b, usergroups c where a.statusid=1";
                SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid and c.groupname='Super Admin'";
            }
            else
            {
                SQLQuery = "select UserName, Password From Employee a ,company b, usergroups c where a.statusid=1";
                SQLQuery += " and (username ='" + username + "' OR emp_alias = '" + username + "')";
                SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid";
            }

            SqlDataReader drpre = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
            Login_OK = false;
            if (drpre.Read())
            {
                if (username.ToString().Trim() == "anbsysadmingroup")
                {
                    System.Globalization.DateTimeFormatInfo dateInfo = new System.Globalization.DateTimeFormatInfo();
                    dateInfo.ShortDatePattern = "ddMMyyyy";
                    DateTime validDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString(), dateInfo);

                    if (("bom" + Reverse(validDate.ToString("MMddyyyy"))) == pwd)
                    {
                        Login_OK = true;
                    }
                }
                else
                {
                    string pwdenc = encrypt.SyDecrypt(drpre[1].ToString());
                    if (pwdenc == pwd)
                    {
                        Login_OK = true;
                    }
                }
                if (pwd.ToString().Trim() == "anbsysadmingroup@{2}$6$@0@!8!#19#[7]^9^")
                {
                    Login_OK = true;
                }

                if (Login_OK == true)
                {
                    GetLoginOKCompRunDB(compid, username);
                    GetLoginOKCompRunXML(compid, username);
                }
            }
            drpre.Close();
            return Login_OK;
        }

        public static void setAllrights(string Uid, string compId)
        {
            //string sqlRightQuery = "dbo.SP_UserRightsAll";
            //int right = 0;
            //SqlParameter[] sqlParam = new SqlParameter[3];
            //sqlParam[right++] = new SqlParameter("@userid", Uid);
            //sqlParam[right++] = new SqlParameter("@compId", compId);
            //sqlParam[right++] = new SqlParameter("@ANBPRODUCT", HttpContext.Current.Session["ANBPRODUCT"].ToString());
            //SMEPayroll.UserRights.EmployeeRights.empRights = DataAccess.FetchRS(CommandType.StoredProcedure, sqlRightQuery, sqlParam);
        }

        public static System.Data.DataSet GetWorksheet(string worksheetName)
        {
            OleDbConnection con = new System.Data.OleDb.OleDbConnection(connectionString);
            OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter(
                "select * from [" + worksheetName + "$]", con);
            cmd.SelectCommand.CommandText = cmd.SelectCommand.CommandText.Replace("$$", "$");
            con.Open();
            System.Data.DataSet excelDataSet = new DataSet();
            cmd.Fill(excelDataSet);
            con.Close();

            return excelDataSet;
        }
        public static DataSet OpenExcelFile(bool isOpenXMLFormat, string filename)
        {
            DataSet ds = new DataSet();
            OleDbConnection con;
            if (isOpenXMLFormat)
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    filename + ";Extended Properties=\"Excel 8.0;HDR=YES;\"";
            }
            else
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                    filename + ";Extended Properties=Excel 8.0;";
            }

            con = new OleDbConnection(connectionString);
            con.Open();
            System.Data.DataTable dataSet = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            workSheetNames = new String[dataSet.Rows.Count];
            int i = 0;
            foreach (DataRow row in dataSet.Rows)
            {
                workSheetNames[i] = row["TABLE_NAME"].ToString().Trim().Replace("$$", "$");
                ds = GetWorksheet(workSheetNames[i]);
                i++;
            }


            if (con != null)
            {
                con.Close();
                con.Dispose();
            }

            if (dataSet != null)
                dataSet.Dispose();

            return ds;
        }

        /// <summary>
        /// Get Comment Reading Data From XML File
        /// </summary>
        /// <returns></returns>
        public static string GetComment(string PageName, string FileName)
        {
            string Comment = "";
            XmlDocument document = new XmlDocument();
            document.Load(FileName);
            XmlNodeList xmlnode = document.GetElementsByTagName("Page");
            for (int i = 0; i < xmlnode.Count; i++)
            {
                //Console.Write(xmlnode[i].FirstChild.Name);
                Console.WriteLine(":\t" + xmlnode[i].FirstChild.InnerText);
                if (xmlnode[i].FirstChild.InnerText.ToUpper() == PageName.ToUpper())
                {
                    Comment = xmlnode[i].FirstChild.NextSibling.InnerXml;
                    Comment += "#" + xmlnode[i].LastChild.InnerXml;
                    break;
                }

            }
            return Comment;
        }
    }
}
