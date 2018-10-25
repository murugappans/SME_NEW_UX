using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SMEPayroll.Payroll
{
    public partial class Claim_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }
        [System.Web.Services.WebMethod]
        public static string GetClaims(object[] Claimtypeschecked)
        {

            if (Claimtypeschecked[0] == null)
            {
                Claimtypeschecked[0] = "-1";
            }
            if (Claimtypeschecked[1] == null)
            {
                Claimtypeschecked[1] = "-1";
            }
            if (Claimtypeschecked[2] == null)
            {
                Claimtypeschecked[2] = "-1";
            }
            DataSet count = new DataSet();
            string Countapporoved = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved')";
            string Countapplied = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus not in ('applied') ";
            string Countpending = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus not in ('approved', 'rejected') ";
            string Countrejected = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('rejected') ";
           // string selecteddayquery = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + ", getdate())) THEN status ELSE NULL END) ";
            string firstClaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[0] + " THEN trx_amount ELSE NULL END),0)) ";
            string secondClaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[1] + " THEN trx_amount ELSE NULL END),0)) ";
             string thirdClaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[2] + " THEN trx_amount ELSE NULL END),0)) ";
            string ssql = "Select " + Countapplied + firstClaimtype + "as Count_AppliedFirsttype," + Countapporoved + firstClaimtype + "as Count_ApprovedFirsttype," + Countpending + firstClaimtype + " as Count_PendingFirsttype," +
                          Countrejected + firstClaimtype + "as Count_RejectedFirsttype," + Countapplied + secondClaimtype + "as Count_AppliedSecondtype," + Countapporoved + secondClaimtype + "as Count_ApprovedSecondtype," +
                          Countpending + secondClaimtype + "as Count_PendingSecondtype," + Countrejected + secondClaimtype + "as Count_RejectedSecondtype," + Countapplied + thirdClaimtype + "as Count_AppliedThirdtype," + Countapporoved + thirdClaimtype + "as Count_ApprovedThirdtype," +
                          Countpending + thirdClaimtype + "as Count_PendingThirdtype," + Countrejected + thirdClaimtype + "as Count_RejectedThirdtype from emp_additions ";

            DataSet dt = EvaluatePercent(ssql);
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { CountAppliedFirsttype = count.Tables[0].Rows[0]["Count_AppliedFirsttype"], CountApprovedFirsttype = count.Tables[0].Rows[0]["Count_ApprovedFirsttype"], CountPendingFirsttype = count.Tables[0].Rows[0]["Count_PendingFirsttype"],
                CountRejectedFirsttype = count.Tables[0].Rows[0]["Count_RejectedFirsttype"],CountAppliedSecondtype = count.Tables[0].Rows[0]["Count_AppliedSecondtype"],
                CountApprovedSecondtype = count.Tables[0].Rows[0]["Count_ApprovedSecondtype"], CountPendingSecondtype = count.Tables[0].Rows[0]["Count_PendingSecondtype"],
                CountRejectedSecondtype = count.Tables[0].Rows[0]["Count_RejectedSecondtype"], CountAppliedThirdtype = count.Tables[0].Rows[0]["Count_AppliedThirdtype"],
                CountApprovedThirdtype = count.Tables[0].Rows[0]["Count_ApprovedThirdtype"], CountPendingThirdtype = count.Tables[0].Rows[0]["Count_PendingThirdtype"],
                CountRejectedThirdtype = count.Tables[0].Rows[0]["Count_RejectedThirdtype"], FirstclaimPercent = dt.Tables[0].Rows[0]["firstpercentage"],SecondclaimPercent = dt.Tables[0].Rows[0]["secondpercentage"],
                ThirdclaimpPercent = dt.Tables[0].Rows[0]["thirdpercentage"],
                ApprovedClaims = dt.Tables[0].Rows[0]["Fullamonunt"]
            });
        }

        private static DataSet EvaluatePercent(string sqlquery)
        {
            DataSet dt;
            string sql = "SET ARITHABORT OFF SET ANSI_WARNINGS OFF Select " +
                "convert(numeric(10, 2), isnull((Count_ApprovedSecondtype + Count_ApprovedThirdtype + Count_ApprovedFirsttype), 0)) as Fullamonunt,"+
             "convert(numeric(10, 2), isnull((Count_ApprovedFirsttype * 100) / (Count_ApprovedSecondtype + Count_ApprovedThirdtype + Count_ApprovedFirsttype), 0)) as firstpercentage," +
             "convert(numeric(10, 2), isnull((Count_ApprovedSecondtype * 100) / (Count_ApprovedSecondtype + Count_ApprovedThirdtype + Count_ApprovedFirsttype), 0)) as secondpercentage," +
             "convert(numeric(10, 2), isnull((Count_ApprovedThirdtype * 100) / (Count_ApprovedSecondtype + Count_ApprovedThirdtype + Count_ApprovedFirsttype), 0)) as thirdpercentage" +
             " from(";
            sql += sqlquery + ") as t";
            dt = DataAccess.FetchRS(CommandType.Text, sql, null);
             
            return dt;
        }

        [System.Web.Services.WebMethod]
        public static string GetapprovedClaims(object[] Claimtypeschecked)
        {
            if (Claimtypeschecked[0] == null)
            {
                Claimtypeschecked[0] = "-1";
            }
            if (Claimtypeschecked[1] == null)
            {
                Claimtypeschecked[1] = "-1";
            }
            DataSet count = new DataSet();
            string Countapporoved = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') ";
            string Countapporoveddivision = "NULLIF(sum(CASE WHEN claimstatus in ('approved') ";
            string selecteddayquery = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type in (" + Claimtypeschecked[0] + "," + Claimtypeschecked[1] + ") THEN trx_amount ELSE NULL END),0))  ";
            string selecteddayquerydivision = " and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type in (" + Claimtypeschecked[0] + "," + Claimtypeschecked[1] + ") THEN trx_amount ELSE NULL END),0),0)) ";
            string firstclaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[0] + " THEN trx_amount ELSE NULL END) ";
            string secondclaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[1] + " THEN trx_amount ELSE NULL END) ";
            string thirdclaimtype = "and year(trx_period) = year(getdate()) and month(trx_period) = month(getdate()) and trx_type = " + Claimtypeschecked[2] + " THEN trx_amount ELSE NULL END) ";
            string ssql = "Select " + Countapporoved + selecteddayquery + "as ApprovedClaims," + Countapporoved + firstclaimtype + "*100 /" + Countapporoveddivision + selecteddayquerydivision + "as firstclaimpercentage," +
                          Countapporoved + secondclaimtype + "*100 /" + Countapporoveddivision + selecteddayquerydivision + "as secondclaimpercentage,"+Countapporoved + thirdclaimtype + " * 100 / " + Countapporoveddivision + selecteddayquerydivision + 
                          " as thirdclaimpercentage, " + Countapporoved + firstclaimtype+ " ,0)) as firstclaimtypenumber, " + Countapporoved + firstclaimtype + " ,0)) as secondclaimtypenumber, " + Countapporoved + firstclaimtype + " ,0)) as thirdclaimtypenumber  from emp_additions;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { ApprovedClaims = count.Tables[0].Rows[0]["ApprovedClaims"],
                firstclaimpercentage = count.Tables[0].Rows[0]["firstclaimpercentage"],
                secondclaimpercentage = count.Tables[0].Rows[0]["secondclaimpercentage"],
                thirdclaimpercentage = count.Tables[0].Rows[0]["thirdclaimpercentage"],
                firstclaimtypenumber = count.Tables[0].Rows[0]["firstclaimtypenumber"],
                secondclaimtypenumber = count.Tables[0].Rows[0]["secondclaimtypenumber"],
                thirdclaimtypenumber = count.Tables[0].Rows[0]["thirdclaimtypenumber"]
            });
        }
        [System.Web.Services.WebMethod]
        public static string GetClaimTypesselected(object[] Claimtypeschecked)
        {
            if (Claimtypeschecked[0] == null)
            {
                Claimtypeschecked[0] = "-1";
            }
            if (Claimtypeschecked[1] == null)
            {
                Claimtypeschecked[1] = "-1";
            }
            DataSet count = new DataSet();
            string ssql = "SELECT ( " +
                          "SELECT [desc] FROM additions_types where id = " + Claimtypeschecked[0] + ") AS FirstClaimType, (" +
                          "SELECT [desc] FROM additions_types where id =" + Claimtypeschecked[1] + ") AS SecpndClaimType, (" +
                          "SELECT [desc] FROM additions_types where id =" + Claimtypeschecked[2] + ") AS ThirdClaimType ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { FirstClaimType = count.Tables[0].Rows[0]["FirstClaimType"], SecpndClaimType = count.Tables[0].Rows[0]["SecpndClaimType"] , ThirdClaimType = count.Tables[0].Rows[0]["ThirdClaimType"] });

        }
        [System.Web.Services.WebMethod]
        public static string GetClaimTypes()
        {
            string compid = HttpContext.Current.Session["Compid"].ToString();
            string ssl = "select * from additions_types where company_id = " + compid + " and optionselection = 'Claim'";
            DataSet claimtype = new DataSet();
            claimtype = DataAccess.FetchRS(CommandType.Text, ssl);
            return JsonConvert.SerializeObject(new { claimtypes = claimtype.Tables[0] });

        }

        [System.Web.Services.WebMethod]
        public static string GetCurrency(object[] Claimtypeschecked)
        {
            if (Claimtypeschecked[0] == null)
            {
                Claimtypeschecked[0] = "-1";
            }
            if (Claimtypeschecked[1] == null)
            {
                Claimtypeschecked[1] = "-1";
            }
            string ssl = "select distinct c.Currency + '(' + c.Symbol + ')' as Currency  from emp_additions ea " +
                         "inner join currency c on c.Id = ea.CurrencyID " +
                         "where ea.trx_type in (" + Claimtypeschecked[0] + "," + Claimtypeschecked[1]  + ");";
            DataSet count = new DataSet();
            count = DataAccess.FetchRS(CommandType.Text, ssl, null);
            return JsonConvert.SerializeObject(new { Currency = count.Tables[0].Rows[0]["Currency"] });

        }
        [System.Web.Services.WebMethod]
        public static string GetApprovedClaimsDaily(bool? NextMont)
        {
            string sql = "";
            int month = DateTime.Now.Month;
            int Year = DateTime.Now.Year;
            if (NextMont.Value)
            {
                if (month == 12)
                {
                    Year = Year + 1;
                    month = 1;
                }
                else
                    month = DateTime.Now.Month + 1;
            }

            int NoOfDays = 0;
            if (month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
                NoOfDays = 31;
            else if (month == 4 || month == 6 || month == 9 || month == 11)
                NoOfDays = 30;
            else
            {
                if (((Year % 4 == 0) && (Year % 100 != 0)) || (Year % 400 == 0))
                    NoOfDays = 29;
                else
                    NoOfDays = 29;
            }
            DataSet count = new DataSet();
            for (int i = 1; i <= NoOfDays; i++)
            {
                sql = sql + "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') " +
                     "and convert(date, '" + Year + "/" + month + "/" + i + "') = convert(date, trx_period) " +
                     " THEN trx_amount ELSE NULL END),0)) as '" + i + "'";
                if (i != NoOfDays)
                    sql += ", ";


            }

            string ssql = "Select " + sql + "  from emp_additions; ";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { today = count.Tables[0].Rows, NextMont });

            //***********************Abhi code start************************
            //string sqlpreviousdays = "";
            //string sqlnextdays = "";
            //string sqltoday= "";
            //DataSet count = new DataSet();
            //string currentmonth = DateTime.Now.Month.ToString();
            ////int currmonth = Convert.ToInt32(currentmonth);
            //string currentday = DateTime.Now.Day.ToString();
            //int currday = Convert.ToInt32(currentday);
            //int currdayclaim = currday - 1;
            //if (currentmonth == "9" || currentmonth == "4" || currentmonth == "6" || currentmonth == "11")
            //{
            //    for (int i = 0, o = 0; i < currday - 1; i++, o++)
            //    {
            //        sqlpreviousdays = sqlpreviousdays + "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') " +
            //             "and convert(date,trx_period) = DATEADD(month, DATEDIFF(month," + i + ", getdate()), " + i + ") THEN trx_amount ELSE NULL END),0)) as '" + o + "', ";
            //    }
            //    sqltoday = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') and convert(date,trx_period) = DATEADD(month, DATEDIFF(month," + currdayclaim + ", getdate()), " + currdayclaim + ") THEN trx_amount ELSE NULL END),0)) as '" + currdayclaim + "'";
            //    for (int i = (currday + 1) - currday, o = 29; i < 30 - (currday - 1); i++, o--)
            //    {
            //        sqlnextdays = sqlnextdays + "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') " +
            //             "and convert(date,trx_period) = DATEADD(month, DATEDIFF(month,-" + i + ", getdate()), -" + i + ") THEN trx_amount ELSE NULL END),0)) as '" + o + "', ";
            //    }
            //    string ssql = "select " + sqlpreviousdays + sqlnextdays + sqltoday + " from emp_additions;";
            //    count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            //    return JsonConvert.SerializeObject(new { today = count.Tables[0].Rows});
            //}
            //else
            //{
            //    for (int i = 0, o=0; i < currday - 1; i++,o++)
            //    {
            //        sqlpreviousdays = sqlpreviousdays + "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') " +
            //             "and convert(date,trx_period) = DATEADD(month, DATEDIFF(month," + i + ", getdate()), " + i + ") THEN trx_amount ELSE NULL END),0)) as '" + o + "', ";
            //    }
            //    sqltoday = "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') and convert(date,trx_period) = DATEADD(month, DATEDIFF(month," + currdayclaim + ", getdate()), " + currdayclaim + ") THEN trx_amount ELSE NULL END),0)) as '" + currdayclaim + "'";
            //    for (int i = (currday+1)- currday,o= 30; i < 31 - (currday-1); i++,o--)
            //    {
            //        sqlnextdays = sqlnextdays + "convert(numeric(10,2),isnull(sum(CASE WHEN claimstatus in ('approved') " +
            //             "and convert(date,trx_period) = DATEADD(month, DATEDIFF(month,-" + i + ", getdate()), -" + i + ") THEN trx_amount ELSE NULL END),0)) as '" + o + "', ";
            //    }
            //    string ssql = "select " + sqlpreviousdays + sqlnextdays + sqltoday + " from emp_additions;";
            //    count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            //    return JsonConvert.SerializeObject(new { today = count.Tables[0].Rows});
            //}

        }


    }
}