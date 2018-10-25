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
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace SMEPayroll
{
    /// <summary>

        
        /// Summary description for Utility
    /// </summary>
    public class Utility
    {
        public DataSet allRights;
        public Utility()
        {

        }
        public static string[] workSheetNames = new string[] { };   //List of Worksheet names 
        //Path and file name of the Excel Workbook
        private static string connectionString;


    



        public static int GetIso8601WeekOfYear(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        } 


        public static string ToString(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "";

            if (obj.ToString() == "0")
                return "";

            return obj.ToString().Trim();
        }
        public static string ToString_ku(object obj)
        {
            if (obj == null || obj == DBNull.Value)
                return "0";

            if (obj.ToString() == "0")
                return "0";

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
        public static double ToDouble_mozi(object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value || obj.ToString() == "" || obj.ToString() == "NaN")
                    return 0.00;
                return Double.Parse(obj.ToString(),NumberStyles.AllowDecimalPoint);
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


        public static bool IsPayrollCeiling(string comp)
        {
            string strcon = "select Count(*) from CeilingMaster where CeilingType in (1,2) and CompanyID=" + comp;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);
            int count = 0;
            while (drcon.Read())
            {
                if (drcon.GetValue(0) != null)
                {
                    count = Convert.ToInt32(drcon.GetValue(0).ToString());
                }                
            }
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsMultiCurrency(string comp)
        {
            //Get value from TextBox and show in label
            int COPT = 0; int mc = 0;
            string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + comp;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null || drcon.GetValue(0).ToString()=="")
                {
                    COPT = 1;
                }
                else
                {
                    COPT = Convert.ToInt32(drcon.GetValue(0).ToString());
                }
                if (drcon.GetValue(1) == null || drcon.GetValue(1).ToString()=="")
                {
                    mc = 0;
                }
                else
                {
                    mc = Convert.ToInt32(drcon.GetValue(1).ToString());
                }
            }
            if (mc == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        
        }
        public static int GetGroupStatus(int company_id)
        {
            int groupManage = 0;
            DataTable dtEmployee = new DataTable();
            string query = "Select GroupManage from Company where Company_Id=" + company_id;

            SqlDataReader myComm = DataAccess.ExecuteReader(CommandType.Text, query, null);
            dtEmployee.Load(myComm);
            if (dtEmployee.Rows.Count > 0)
            {
                if (dtEmployee.Rows[0].IsNull("GroupManage"))
                {
                    groupManage = 0;
                }
                else
                {
                    groupManage = Convert.ToInt32(dtEmployee.Rows[0]["GroupManage"].ToString());
                }



            }
            return groupManage;
        }

        public static int GetWFStatus(int company_id) //--created by murugan
        {
            int WFManage = 0;
            DataTable dtEmployee = new DataTable();
            string query = "Select WorkFlowID from Company where Company_Id=" + company_id;
            SqlDataReader myComm = DataAccess.ExecuteReader(CommandType.Text, query, null);
            dtEmployee.Load(myComm);
            if (dtEmployee.Rows.Count > 0)
            {
                if (dtEmployee.Rows[0].IsNull("WorkFlowID"))
                {
                    WFManage = 0;
                }
                else
                {
                    WFManage = Convert.ToInt32(dtEmployee.Rows[0]["WorkFlowID"].ToString());
                }



            }
            return WFManage;
        }
        public static bool IsAdvClaims(string comp)
        {
            //Get value from TextBox and show in label
            int ac = 0;
            string strcon = "Select AdvClaims from company  Where Company_Id=" + comp;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null || drcon.GetValue(0).ToString() == "0")
                {
                    ac = 0;
                }
                else if(drcon.GetValue(0).ToString() == "1")
                {
                    ac = Convert.ToInt32(drcon.GetValue(0).ToString());
                }
            }
            if (ac == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
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

        public static bool showweeklyroster()
        {

            bool flag = false;
            string sql = "select isnull(RosterType,1) as RosterType from  Company Where Company_ID=" + Utility.ToInteger(HttpContext.Current.Session["Compid"]);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                if (dr[0].ToString() == "2")
                    flag = true;
            }

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
        public static void FillDropDownCompany(DropDownList drp, string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            drp.DataSource = ds.Tables[0];

            drp.DataValueField = ds.Tables[0].Columns[0].ColumnName.ToString();
            drp.DataTextField = ds.Tables[0].Columns[1].ColumnName.ToString();
            drp.DataBind();
            drp.Items.Insert(0, new ListItem("-select Company-"));
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
            //km
            DataSet dsadd = new DataSet();
            string sSQL = "Select ID,substring([desc],1,15) [desc],formulatype,formulacalc,InTime,OutTime,IsNextDay from Additions_Types Where Company_Id='" + compid + "' And optionselection = 'Variable' AND Code NOT IN ('C1','C2','C3','C4','C5','C6','C7','C8')Order By Code";
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
                    HttpContext.Current.Session["V1InTime"] = dradd["InTime"].ToString();
                    HttpContext.Current.Session["V1OutTime"] = dradd["OutTime"].ToString();
                    HttpContext.Current.Session["V1IsNextDay"] = dradd["IsNextDay"].ToString();

                    dradd = dsadd.Tables[0].Rows[1];
                    HttpContext.Current.Session["V2ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V2Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V2Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V2FormulaCalc"] = dradd["formulacalc"].ToString();
                    HttpContext.Current.Session["V2InTime"] = dradd["InTime"].ToString();
                    HttpContext.Current.Session["V2OutTime"] = dradd["OutTime"].ToString();
                    HttpContext.Current.Session["V2IsNextDay"] = dradd["IsNextDay"].ToString();

                    dradd = dsadd.Tables[0].Rows[2];
                    HttpContext.Current.Session["V3ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V3Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V3Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V3FormulaCalc"] = dradd["formulacalc"].ToString();
                    HttpContext.Current.Session["V3InTime"] = dradd["InTime"].ToString();
                    HttpContext.Current.Session["V3OutTime"] = dradd["OutTime"].ToString();
                    HttpContext.Current.Session["V3IsNextDay"] = dradd["IsNextDay"].ToString();
                    dradd = dsadd.Tables[0].Rows[3];
                    HttpContext.Current.Session["V4ID"] = dradd["ID"].ToString();
                    HttpContext.Current.Session["V4Text"] = dradd["desc"].ToString();
                    HttpContext.Current.Session["V4Formula"] = dradd["formulatype"].ToString();
                    HttpContext.Current.Session["V4FormulaCalc"] = dradd["formulacalc"].ToString();
                    HttpContext.Current.Session["V4InTime"] = dradd["InTime"].ToString();
                    HttpContext.Current.Session["V4OutTime"] = dradd["OutTime"].ToString();
                    HttpContext.Current.Session["V4IsNextDay"] = dradd["IsNextDay"].ToString();
                }
            }

            
            sSQL = "Select ID,substring([desc],1,15) [desc],formulatype,formulacalc from Additions_Types Where Company_Id='" + compid + "' And optionselection = 'Variable' AND Code  IN ('C1','C2','C3','C4','C5','C6','C7','C8')Order By Code";
            dsadd = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            if (dsadd != null)
            {
                if (dsadd.Tables[0].Rows.Count > 3)
                {
                    DataRow dradd;

                    if (dsadd.Tables[0].Rows.Count > 0)
                    {
                        dradd = dsadd.Tables[0].Rows[0];
                        HttpContext.Current.Session["C1ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C1Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C1Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C1FormulaCalc"] = dradd["formulacalc"].ToString();
                    }
                    if (dsadd.Tables[0].Rows.Count > 1)
                    {
                        dradd = dsadd.Tables[0].Rows[1];
                        HttpContext.Current.Session["C2ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C2Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C2Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C2FormulaCalc"] = dradd["formulacalc"].ToString();
                    }
                    if (dsadd.Tables[0].Rows.Count > 2)
                    {
                        dradd = dsadd.Tables[0].Rows[2];
                        HttpContext.Current.Session["C3ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C3Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C3Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C3FormulaCalc"] = dradd["formulacalc"].ToString();
                    }
                    if (dsadd.Tables[0].Rows.Count > 3)
                    {
                        dradd = dsadd.Tables[0].Rows[3];
                        HttpContext.Current.Session["C4ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C4Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C4Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C4FormulaCalc"] = dradd["formulacalc"].ToString();
                    }

                    if (dsadd.Tables[0].Rows.Count > 4)
                    {
                        dradd = dsadd.Tables[0].Rows[4];
                        HttpContext.Current.Session["C5ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C5Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C5Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C5FormulaCalc"] = dradd["formulacalc"].ToString();
                    }


                    if (dsadd.Tables[0].Rows.Count > 5)
                    {
                        dradd = dsadd.Tables[0].Rows[5];
                        HttpContext.Current.Session["C6ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C6Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C6Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C6FormulaCalc"] = dradd["formulacalc"].ToString();
                    }


                    if (dsadd.Tables[0].Rows.Count > 6)
                    {
                        dradd = dsadd.Tables[0].Rows[6];
                        HttpContext.Current.Session["C7ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C7Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C7Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C7FormulaCalc"] = dradd["formulacalc"].ToString();
                    }


                    if (dsadd.Tables[0].Rows.Count > 7)
                    {
                        dradd = dsadd.Tables[0].Rows[7];
                        HttpContext.Current.Session["C8ID"] = dradd["ID"].ToString();
                        HttpContext.Current.Session["C8Text"] = dradd["desc"].ToString();
                        HttpContext.Current.Session["C8Formula"] = dradd["formulatype"].ToString();
                        HttpContext.Current.Session["C8FormulaCalc"] = dradd["formulacalc"].ToString();
                    }
                }
            }




        }
        public static void GetLoginOKCompRunDB(string compid, string username)
        {
            string SQLQuery = "";
            if (username.ToString().Trim() == "anbsysadmingroup")
            {
                SQLQuery = "select Top 1 UserName, Password,a.Company_Id,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname,b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,a.email,b.ismaster,b.istsremarks,b.ProjectAssign from employee a ,company b, usergroups c where a.statusid=1";
                SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid and c.groupname='Super Admin'";

                //new-to take the first super admin
                SQLQuery += "order by UserName desc";
            }
            else
            {
                if (HttpContext.Current.Session["isMaster"] != null)
                {
                    if (HttpContext.Current.Session["isMaster"].ToString() == "True")
                    {
                        SQLQuery = "select distinct(UserName), Password,d.CompanyId As Company_ID ,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',";
                        SQLQuery += " emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname, ";
                        SQLQuery += " b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,a.email,'True' As ismaster,";
                        SQLQuery += " b.istsremarks,b.ProjectAssign from employee  a ,company b, usergroups c ,MasterCompany_User d where a.statusid=1 ";
                        SQLQuery += " and (username ='" + username + "' OR emp_alias = '" + username + "') and ( b.Company_id=d.CompanyId )    and d.CompanyId='" + compid + "'  and a.groupid=c.groupid";
                    }

                }
                else
                {

                    SQLQuery = "select UserName, Password,a.Company_Id,emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname,b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,a.email,b.ismaster,b.istsremarks,b.ProjectAssign from employee a ,company b, usergroups c where a.statusid=1";
                    SQLQuery += " and (username ='" + username + "' OR emp_alias = '" + username + "')";
                    SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid";

                    //add union and join the user table
                    SQLQuery += " UNION ";
                    SQLQuery += "  select UserName, Password,a.Company_Id,'' as emp_code,[UserName] as  'emp_name','' emp_group_id,b.Company_Code,b.cpf_ref_no,b.no_work_days,b.timesheet_approve,c.groupname,b.day_hours,b.day_minute,b.leave_model, b.company_name,b.payrolltype,'' as email ,b.ismaster,b.istsremarks,b.ProjectAssign ";
                    SQLQuery += "from Users a ,company b, usergroups c  ";
                    SQLQuery += "where (username ='" + username + "') and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.RightId=c.groupid";
                }
            }

            if (SQLQuery.Length > 0)//To avoid error "User Message: ExecuteReader: CommandText property has not been initialized"
            {

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {

                    HttpContext.Current.Session["ConString"] = "Data Source=" + Constants.DB_SERVER + ";Initial Catalog=" + Constants.DB_NAME + ";User ID=" + Constants.DB_UID + ";Password=" + Constants.DB_PWD;
                    HttpContext.Current.Session["CompanyName"] = dr["Company_Name"].ToString().Trim();
                    HttpContext.Current.Session["Username"] = dr["UserName"].ToString();
                  //  HttpContext.Current.Session["Country"] = "383";
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
                    SqlParameter[] sqlParam = new SqlParameter[5];
                    sqlParam[0] = new SqlParameter("@userid", HttpContext.Current.Session["UserName"]);
                    sqlParam[1] = new SqlParameter("@compId", HttpContext.Current.Session["Compid"]);
                    sqlParam[2] = new SqlParameter("@ANBPRODUCT", HttpContext.Current.Session["ANBPRODUCT"].ToString());

                    if (HttpContext.Current.Session["isMaster"].ToString() == "True")
                    {
                        sqlParam[3] = new SqlParameter("@MasterComp", "1");
                    }
                    else
                    {
                        sqlParam[3] = new SqlParameter("@MasterComp", "0");
                    }
                    sqlParam[4] = new SqlParameter("@CountryID", HttpContext.Current.Session["Country"].ToString());
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
        }

        public static bool GetLoginOK_withoutcompanyid( string username, string pwd)
        {
            bool Login_OK = false;
            string compid = "";

            #region Check user is from user table(user not in Employee table)--ram

            //string SQL_usr = @"SELECT [UserName],[Password] ,[RightId],[Company_Id] FROM [dbo].[Users] where [UserName]='" + username + "' and [Password]='" + pwd + "' and Company_Id='" + compid + "'";

            //--------Protect From SQL Injection by murugan
            string SQL_usr = @"SELECT [UserName],[Password] ,[RightId],[Company_Id] FROM [dbo].[Users] where [UserName]=@username  and [Password]=@pwd  and Company_Id='" + compid + "'";
            SqlParameter[] param1 = new SqlParameter[2];
            param1[0] = new SqlParameter("@userName", username);
            param1[1] = new SqlParameter("@pwd", pwd);
            SqlDataReader dr_usr = DataAccess.ExecuteReader(CommandType.Text, SQL_usr, param1);
            //--------------

            //SqlDataReader dr_usr = DataAccess.ExecuteReader(CommandType.Text, SQL_usr, null);
            if (dr_usr.HasRows)
            {
                Login_OK = true;

                //call method to create rights
                GetLoginOKCompRunDB(compid, username);
                GetLoginOKCompRunXML(compid, username);
            }
            else
            {

            #endregion


                //r
                HttpContext.Current.Session["anbsysadmingroup"] = username;
                HttpContext.Current.Session["pwd"] = pwd;
                string SQLQuery;

                {
                    SQLQuery = "select UserName, Password, company_id  From Employee a  where a.statusid=1";
                    SQLQuery += " and (a.username ='" + username + "' OR a.emp_alias = '" + username + "')";
                   
                }

                SqlDataReader drpre = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                Login_OK = false;
                if (drpre.Read())
                {

                    {
                        string pwdenc = encrypt.SyDecrypt(drpre[1].ToString());
                         compid = drpre[2].ToString();
                        if (pwdenc == pwd)
                        {
                            Login_OK = true;
                        }
                    }


                    if (Login_OK == true)
                    {
                        GetLoginOKCompRunDB(compid, username);
                        GetLoginOKCompRunXML(compid, username);
                    }
                }
                drpre.Close();
            }
            return Login_OK;
        }



        public static bool GetLoginOK(string compid, string username, string pwd)
        {
            bool Login_OK=false;

            #region Check user is from user table(user not in Employee table)--ram

            string SQL_usr = @"SELECT [UserName],[Password] ,[RightId],[Company_Id] FROM [dbo].[Users] where [UserName]='" + username + "' and [Password]='" + pwd + "' and Company_Id='" + compid + "'";
            SqlDataReader dr_usr = DataAccess.ExecuteReader(CommandType.Text, SQL_usr, null);
            if (dr_usr.HasRows)
            {
                Login_OK = true;

                //call method to create rights
                GetLoginOKCompRunDB(compid, username);
                GetLoginOKCompRunXML(compid, username);
            }
            else
            {

            #endregion


                //r
                HttpContext.Current.Session["anbsysadmingroup"] = username;
                HttpContext.Current.Session["pwd"] = pwd;
                string SQLQuery;

                {
                    SQLQuery = "select UserName, Password From Employee a ,company b, usergroups c where a.statusid=1";
                    SQLQuery += " and (username ='" + username + "' OR emp_alias = '" + username + "')";
                    SQLQuery += " and a.Company_id=b.company_id and a.Company_id='" + compid + "'  and a.groupid=c.groupid";
                }

                SqlDataReader drpre = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                Login_OK = false;
                if (drpre.Read())
                {
                 
                    {
                        string pwdenc = encrypt.SyDecrypt(drpre[1].ToString());
                        if (pwdenc == pwd)
                        {
                            Login_OK = true;
                        }
                    }
            

                    if (Login_OK == true)
                    {
                        GetLoginOKCompRunDB(compid, username);
                        GetLoginOKCompRunXML(compid, username);
                    }
                }
                drpre.Close();
            }
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

            //loop each company and insert master record
            DataAccess.FetchRS(CommandType.StoredProcedure, "Sp_Remainder_company", null);
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

        /// <summary>
        /// Get Dataset from Text File
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// 






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
        public static DataTable GetOtherCategoryTemplates(int categoryryId)
        {
            string selectSQL = "";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            selectSQL = "Select DISTINCT TemplateID,TemplateName from CustomTemplates where (Company_Id=1 or Company_Id=" + Utility.ToInteger(HttpContext.Current.Session["Compid"]) + ") and  CategoryId=" + categoryryId + "";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];
            return dtTable;
        }
      
        public static bool CheckSuperAdmin(int UserID)
        {
            int iValue = 0;
            bool bValue = false;
            string selectSQL = "sp_SuperAdmin_Check";
            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@UserID", UserID);


            //iValue = DataAccess.FetchRS(CommandType.StoredProcedure, selectSQL, sqlParam);
            if (iValue == 1)
            {
                bValue = true;
            }
            else
            {
                bValue = false;
            }
            return bValue;
        }
    }
}
