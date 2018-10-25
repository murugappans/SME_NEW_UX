using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;

namespace SMEPayroll.Leaves
{
    public partial class Leave_Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            string sqlStr = "";
            string emp_code = Session["EmpCode"].ToString();


        }
        [System.Web.Services.WebMethod]
        public static string GetLeaves(string selecteddays, object[] leavetypeschecked)
        {
            var selecteddaysbrakdown = "";
            if (selecteddays == null)
            {
                selecteddays = "30";
            }

            if (selecteddays == "30")
            {
                selecteddaysbrakdown = "Next 30 Days";
            }
            if (selecteddays == "60")
            {
                selecteddaysbrakdown = "Next 60 Days";
            }
            if (selecteddays == "90")
            {
                selecteddaysbrakdown = "Next 90 Days";
            }
            if (leavetypeschecked[0] == null)
            {
                leavetypeschecked[0] = "-1";
            }
            if (leavetypeschecked[1] == null)
            {
                leavetypeschecked[1] = "-1";
            }
            if (leavetypeschecked[2] == null)
            {
                leavetypeschecked[2] = "-1";
            }

            DataSet count = new DataSet();
            string Countapporoved = "CASE WHEN status in ('approved') ";
            string Countapplied = "CASE WHEN status in ('approved', 'pending', 'open', 'rejected') ";
            string Countpending = "CASE WHEN status in ('pending', 'open') ";
            string Countrejected = "CASE WHEN status in ('rejected') ";
            string Numberofleaves = "THEN paid_leaves+unpaid_leaves ELSE NULL END ";
            //string selecteddayquery = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + ", getdate())) THEN status ELSE NULL END) ";
            string firstleavetype = "and leave_type = " + leavetypeschecked[0] + " ";
            string secondleavetype = "and leave_type = " + leavetypeschecked[1] + " ";
            string thirdleavetype = "and leave_type = " + leavetypeschecked[2] + " ";
            //string ssql = "Select " + Countapplied + firstleavetype + "as Count_AppliedFirsttype," + Countapporoved + firstleavetype + "as Count_ApprovedFirsttype," + Countpending + firstleavetype + " as Count_PendingFirsttype," +
            //              Countrejected + firstleavetype + "as Count_RejectedFirsttype," + Countapplied + secondleavetype + "as Count_AppliedSecondtype," + Countapporoved + secondleavetype + "as Count_ApprovedSecondtype," +
            //              Countpending + secondleavetype + "as Count_PendingSecondtype," + Countrejected + secondleavetype + "as Count_RejectedSecondtype," + Countapplied + thirdleavetype + "as Count_Appliedthirdtype," +
            //              Countapporoved + thirdleavetype + "as Count_Approvedthirdtype," + Countpending + thirdleavetype + "as Count_Pendingthirdtype," + Countrejected + thirdleavetype + "as Count_Rejectedthirdtype from emp_leaves ;";
            string ssql = "SELECT isnull(sum(Count_AppliedFirsttype),0) as Count_AppliedFirsttype ,isnull(sum(Count_ApprovedFirsttype),0) as Count_ApprovedFirsttype,isnull(sum(Count_PendingFirsttype),0) as Count_PendingFirsttype,isnull(sum(Count_RejectedFirsttype),0) as Count_RejectedFirsttype,isnull(sum(Count_AppliedSecondtype),0) as Count_AppliedSecondtype,isnull(sum(Count_ApprovedSecondtype),0) as Count_ApprovedSecondtype,isnull(sum(Count_PendingSecondtype),0) as Count_PendingSecondtype,isnull(sum(Count_RejectedSecondtype),0) as Count_RejectedSecondtype,isnull(sum(Count_Appliedthirdtype),0) as Count_Appliedthirdtype,isnull(sum(Count_Approvedthirdtype),0) as Count_Approvedthirdtype,isnull(sum(Count_Pendingthirdtype),0) as Count_Pendingthirdtype,isnull(sum(Count_Rejectedthirdtype),0) as Count_Rejectedthirdtype " +
                          "FROM " +
                          "( " +
                          "SELECT " +
                          Countapplied + firstleavetype + Numberofleaves + "as Count_AppliedFirsttype," + Countapporoved + firstleavetype + Numberofleaves + "as Count_ApprovedFirsttype," + Countpending + firstleavetype + Numberofleaves + " as Count_PendingFirsttype," +
                          Countrejected + firstleavetype + Numberofleaves + "as Count_RejectedFirsttype," + Countapplied + secondleavetype + Numberofleaves + "as Count_AppliedSecondtype," + Countapporoved + secondleavetype + Numberofleaves + "as Count_ApprovedSecondtype," +
                          Countpending + secondleavetype + Numberofleaves + "as Count_PendingSecondtype," + Countrejected + secondleavetype + Numberofleaves + "as Count_RejectedSecondtype," + Countapplied + thirdleavetype + Numberofleaves + "as Count_Appliedthirdtype," +
                          Countapporoved + thirdleavetype + Numberofleaves + "as Count_Approvedthirdtype," + Countpending + thirdleavetype + Numberofleaves + "as Count_Pendingthirdtype," + Countrejected + thirdleavetype + Numberofleaves + "as Count_Rejectedthirdtype from emp_leaves " +
                          "where convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + " , getdate()))" +
                          ") x " +
                          "where Count_AppliedFirsttype is not null or Count_ApprovedFirsttype is not null or Count_PendingFirsttype is not null or Count_RejectedFirsttype is not null or Count_AppliedSecondtype is not null or Count_ApprovedSecondtype is not null or Count_PendingSecondtype is not null or Count_RejectedSecondtype is not null or Count_Appliedthirdtype is not null or Count_Approvedthirdtype is not null or Count_Pendingthirdtype is not null or Count_Rejectedthirdtype is not null ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { CountAppliedFirsttype = count.Tables[0].Rows[0]["Count_AppliedFirsttype"], CountApprovedFirsttype = count.Tables[0].Rows[0]["Count_ApprovedFirsttype"], CountPendingFirsttype = count.Tables[0].Rows[0]["Count_PendingFirsttype"], CountRejectedFirsttype = count.Tables[0].Rows[0]["Count_RejectedFirsttype"], CountAppliedSecondtype = count.Tables[0].Rows[0]["Count_AppliedSecondtype"], CountApprovedSecondtype = count.Tables[0].Rows[0]["Count_ApprovedSecondtype"], CountPendingSecondtype = count.Tables[0].Rows[0]["Count_PendingSecondtype"], CountRejectedSecondtype = count.Tables[0].Rows[0]["Count_RejectedSecondtype"], CountAppliedthirdtype = count.Tables[0].Rows[0]["Count_Appliedthirdtype"], CountApprovedthirdtype = count.Tables[0].Rows[0]["Count_Approvedthirdtype"], CountPendingthirdtype = count.Tables[0].Rows[0]["Count_Pendingthirdtype"], CountRejectedthirdtype = count.Tables[0].Rows[0]["Count_Rejectedthirdtype"], selecteddaysbrakdown = selecteddaysbrakdown });
        }

        [System.Web.Services.WebMethod]
        public static string GetapprovedLeaves(string selecteddays, object[] leavetypeschecked)
        {
            if (leavetypeschecked[0] == null)
            {
                leavetypeschecked[0] = "-1";
            }
            if (leavetypeschecked[1] == null)
            {
                leavetypeschecked[1] = "-1";
            }
            if (leavetypeschecked[2] == null)
            {
                leavetypeschecked[2] = "-1";
            }
            DataSet count = new DataSet();
            //string Countapporoved = "isnull(COUNT(CASE WHEN status in ('approved') ";
            //string Countapporoveddivision = "NULLIF(COUNT(CASE WHEN status in ('approved') ";
            //string selecteddayquery = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < =  convert(date,DATEADD(day, " + selecteddays + ", getdate())) and leave_type in (" + leavetypeschecked[0] + "," + leavetypeschecked[1] + "," + leavetypeschecked[2] + ") THEN status ELSE NULL END),0) ";
            //string selecteddayquerydivision = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + ", getdate())) and leave_type in (" + leavetypeschecked[0] + "," + leavetypeschecked[1] + "," + leavetypeschecked[2] + ") THEN status ELSE NULL END),0),0) ";
            //string firstleavetype = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + " , getdate()))  and leave_type = " + leavetypeschecked[0] + " THEN status ELSE NULL END) ";
            //string secondleavetype = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + " , getdate()))  and leave_type = " + leavetypeschecked[1] + " THEN status ELSE NULL END) ";
            //string thirdleavetype = "and convert(date,start_date) >= convert(date,GETDATE()) and convert(date,start_date) < = convert(date,DATEADD(day, " + selecteddays + " , getdate()))  and leave_type = " + leavetypeschecked[2] + " THEN status ELSE NULL END) ";
            //string ssql = "Select " + Countapporoved + selecteddayquery + "as ApprovedLeaves," + Countapporoved + firstleavetype + "*100 /" + Countapporoveddivision + selecteddayquerydivision + "as firstleavepercentage," +
            //              Countapporoved + secondleavetype + "*100 /" + Countapporoveddivision + selecteddayquerydivision + "as secondleavepercentage," + Countapporoved + thirdleavetype + "*100 /" + Countapporoveddivision + selecteddayquerydivision + "as thirdleavepercentage from emp_leaves ;";
            string ssql = "SELECT isnull(sum(ApprovedLeaves),0) as ApprovedLeaves , Cast(isnull(round(sum(ApprovedLeavesfirst)/sum(ApprovedLeaves),2),0) * 100 AS DECIMAL(18, 2)) as firstleavepercentage, isnull(sum(ApprovedLeavesfirst),0) as firstleavetotal, " +
                          "Cast(isnull(round(sum(ApprovedLeavessecond)/sum(ApprovedLeaves),2),0) * 100 AS DECIMAL(18, 2)) as secondleavepercentage, isnull(sum(ApprovedLeavessecond),0) as secondleavetotal,Cast(isnull(round(sum(ApprovedLeavesthird)/sum(ApprovedLeaves),2),0) * 100 AS DECIMAL(18, 2)) as thirdleavepercentage, isnull(sum(ApprovedLeavesthird),0) as thirdleavetotal " +
                          "FROM " +
                          "( " +
                          "SELECT " +
                          "CASE WHEN status in ('approved') and leave_type in (" + leavetypeschecked[0] + ", " + leavetypeschecked[1] + ", " + leavetypeschecked[2] + ") " +
                          "THEN paid_leaves+unpaid_leaves ELSE NULL END as ApprovedLeaves, " +
                          "CASE WHEN status in ('approved') and leave_type = " + leavetypeschecked[0] + " " +
                          "THEN paid_leaves+unpaid_leaves ELSE NULL END as ApprovedLeavesfirst, " +
                          "CASE WHEN status in ('approved') and leave_type = " + leavetypeschecked[1] + " " +
                          "THEN paid_leaves+unpaid_leaves ELSE NULL END as ApprovedLeavessecond, " +
                          "CASE WHEN status in ('approved') and leave_type = " + leavetypeschecked[2] + " " +
                          "THEN paid_leaves+unpaid_leaves ELSE NULL END as ApprovedLeavesthird " +
                          "FROM emp_leaves " +
                          "where convert(date, start_date) >= convert(date, GETDATE()) and convert(date, start_date) < = convert(date, DATEADD(day, " + selecteddays + ", getdate())) " +
                          ") x " +
                          "where ApprovedLeaves is not null or ApprovedLeavesfirst is not null or ApprovedLeavessecond is not null or ApprovedLeavesthird is not null ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { ApprovedLeaves = count.Tables[0].Rows[0]["ApprovedLeaves"], firstleavepercentage = count.Tables[0].Rows[0]["firstleavepercentage"], firstleavetotal= count.Tables[0].Rows[0]["firstleavetotal"], secondleavepercentage = count.Tables[0].Rows[0]["secondleavepercentage"], secondleavetotal = count.Tables[0].Rows[0]["secondleavetotal"], thirdleavepercentage = count.Tables[0].Rows[0]["thirdleavepercentage"], thirdleavetotal = count.Tables[0].Rows[0]["thirdleavetotal"] });
        }
        [System.Web.Services.WebMethod]
        public static string GetLeaveTypesselected(object[] leavetypeschecked)
        {
            if (leavetypeschecked[0] == null)
            {
                leavetypeschecked[0] = "-1";
            }
            if (leavetypeschecked[1] == null)
            {
                leavetypeschecked[1] = "-1";
            }
            if (leavetypeschecked[2] == null)
            {
                leavetypeschecked[2] = "-1";
            }

            DataSet count = new DataSet();
            string ssql = "SELECT ( " +
                          "SELECT Type FROM leave_types where id = " + leavetypeschecked[0] + ") AS FirstLeavetType, (" +
                          "SELECT Type FROM leave_types where id =" + leavetypeschecked[1] + ") AS SecpndLeavetType, ( " +
                          "SELECT Type FROM leave_types where id = " + leavetypeschecked[2] + ") AS ThirdLeavetType ;";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { FirstLeavetType = count.Tables[0].Rows[0]["FirstLeavetType"], SecpndLeavetType = count.Tables[0].Rows[0]["SecpndLeavetType"], ThirdLeavetType = count.Tables[0].Rows[0]["ThirdLeavetType"] });

        }
        [System.Web.Services.WebMethod]
        public static string GetLeaveTypes()
        {
            string ssl = "select * from leave_types";
            DataSet leavetype = new DataSet();
            leavetype = DataAccess.FetchRS(CommandType.Text, ssl);
            return JsonConvert.SerializeObject(new { leavetypes = leavetype.Tables[0] });

        }
        [System.Web.Services.WebMethod]
        public static string GetApprovedLeavesDaily(bool? NextMont)
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
            for (int i=1; i<= NoOfDays; i++)
            {
                sql = sql + " convert(numeric(10,1),isnull(sum(CASE WHEN status in ('approved') " +
                     "and (convert(date, '"+ Year + "/"+ month +"/"+i+"') = convert(date, start_date) " +
                     "or convert(date, '" + Year + "/" + month + "/" + i + "') = convert(date, end_date) " +
                     "or convert(date, '" + Year + "/" + month + "/" + i + "') between convert(date, start_date) and convert(date, end_date)) THEN paid_leaves+unpaid_leaves ELSE NULL END),0)) as '" + i + "'";
                if (i != NoOfDays)
                    sql +=", ";


            }
          
            string ssql = "Select " + sql + " from emp_leaves ";
            count = DataAccess.FetchRS(CommandType.Text, ssql, null);
            return JsonConvert.SerializeObject(new { today = count.Tables[0].Rows , NextMont });
        }

    }
}