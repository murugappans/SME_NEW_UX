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
using System.Text;

namespace SMEPayroll.TimeSheet
{
    public partial class TimeSlot : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;

        bool blnns = false;
        int cntRec = 0;
        protected string val = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            Pattern_Name();
            
            if (!IsPostBack)
            {
                LoadvalueToChange();
            }
        }

        private void Pattern_Name()
        {
           


            this.patten.Text = "Pattern : " +Request.QueryString["pt"].ToString();
            this.project.Text = "Project Name : " + Request.QueryString["ot"].ToString();
           

        
        }

        private void LoadvalueToChange()
        {
            string commandString = "SELECT [ID],[PatternId],[Roster_Date],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[NightShift],[BreakTimeNHhr], [BreakTimeOThr],[RosterType],[FlexibleWorkinghr],[PullWorkTimein],[FIFO],cast([Rounding]as int)as Rounding,[BreakTimeAfter],[BreakTimeAftOtFlx] FROM [RosterDetail_Pattern] Where PatternId=" + Convert.ToInt32(Request.QueryString["Pattern"].ToString()) + "";

            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, commandString, null);
            if (dr1.Read())
            {
                rtpInTime.SelectedDate = Convert.ToDateTime(dr1["InTime"].ToString());
                rtpOutTime.SelectedDate = Convert.ToDateTime(dr1["OutTime"].ToString());
                rtpEarlyInTime.SelectedDate = Convert.ToDateTime(dr1["EarlyInBy"].ToString());
                rtpEarlyOutTime.SelectedDate = Convert.ToDateTime(dr1["EarlyOutBy"].ToString());
                rtpLateInTime.SelectedDate = Convert.ToDateTime(dr1["LateInBy"].ToString());
                rtpBreakTimeNH.SelectedDate = Convert.ToDateTime(dr1["BreakTimeNHhr"].ToString());
                rtpBreakTimeOT.SelectedDate = Convert.ToDateTime(dr1["BreakTimeOThr"].ToString());
                txtBreakTime.Text=  Convert.ToString(dr1["BreakTimeNH"].ToString());
                txtBreakTimeOT.Text= Convert.ToString(dr1["BreakTimeOT"].ToString());
                cmbRound.SelectedValue = dr1["Rounding"].ToString(); 
            }
            dr1.Close();
        } 


      
       
        protected void btnInsert_Click(object sender, EventArgs e)
        {

            string commandString = "SELECT [ID],[PatternId],[Roster_Date],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[NightShift],[BreakTimeNHhr], [BreakTimeOThr],[RosterType],[FlexibleWorkinghr],[PullWorkTimein],[FIFO],[Rounding],[BreakTimeAfter],[BreakTimeAftOtFlx] FROM [RosterDetail_Pattern] Where 1=0";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
            SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "RosterDetail_Pattern");
            DataRow newRosterdr = dataSet.Tables["RosterDetail_Pattern"].NewRow();
            /*************************************/

            string sqlAdv = "Select AdvTs from Company where Company_Id=" + Utility.ToInteger(Session["Compid"]);
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlAdv, null);

            while (dr1.Read())
            {
                val = Utility.ToString(dr1.GetValue(0));
            }
            if (val == "")
            {
                val = "0";
            }





            if (rtpOutTime.SelectedDate.Value <= rtpInTime.SelectedDate.Value)
            {
                blnns = true;
            }
            newRosterdr["PatternId"] = Convert.ToInt32(Request.QueryString["Pattern"].ToString());
            newRosterdr["Roster_Date"] = DateTime.Now;
            newRosterdr["InTime"] = rtpInTime.SelectedDate.Value.ToString("HH:mm");
            newRosterdr["OutTime"] = rtpOutTime.SelectedDate.Value.ToString("HH:mm");

            if (rtpEarlyInTime.Enabled == true)
            {
                newRosterdr["EarlyInBy"] = rtpEarlyInTime.SelectedDate.Value.ToString("HH:mm");
            }
            else
            {
                if (val == "0")
                {
                    newRosterdr["EarlyInBy"] = rtpInTime.SelectedDate.Value.ToString("HH:mm");
                }
                else
                {
                    TimeSpan ts = new TimeSpan(0, Convert.ToInt32(val), 0);
                    DateTime dt1 = rtpInTime.SelectedDate.Value.Subtract(ts);
                    newRosterdr["EarlyInBy"] = dt1.ToString("HH:mm");

                }
            }

            if (rtpLateInTime.Enabled == true)
            {
                newRosterdr["LateInBy"] = rtpLateInTime.SelectedDate.Value.ToString("HH:mm");
            }
            else
            {
                if (val == "0")
                {
                    newRosterdr["LateInBy"] = rtpInTime.SelectedDate.Value.ToString("HH:mm");
                }
                else
                {
                    DateTime dt1 = rtpInTime.SelectedDate.Value.AddMinutes(Convert.ToInt32(val));

                    newRosterdr["LateInBy"] = dt1.ToString("HH:mm");
                }
            }

            if (rtpEarlyOutTime.Enabled == true)
            {
                newRosterdr["EarlyOutBy"] = rtpEarlyOutTime.SelectedDate.Value.ToString("HH:mm");
            }
            else
            {
                if (val == "0")
                {
                    newRosterdr["EarlyOutBy"] = rtpOutTime.SelectedDate.Value.ToString("HH:mm");
                }
                else
                {
                    TimeSpan ts = new TimeSpan(0, Convert.ToInt32(val), 0);
                    DateTime dt1 = rtpOutTime.SelectedDate.Value.Subtract(ts);
                    newRosterdr["EarlyOutBy"] = dt1.ToString("HH:mm");
                }
            }


            newRosterdr["LateOutBy"] = "";
            newRosterdr["ClockInBefore"] =0;
            newRosterdr["ClockInAfter"] =0;
            newRosterdr["ClockOutBefore"] = 0;
            newRosterdr["ClockOutAfter"] = 0;
            newRosterdr["BreakTimeNH"] = Utility.ToInteger(txtBreakTime.Text.ToString());
            newRosterdr["BreakTimeOT"] = Utility.ToInteger(txtBreakTimeOT.Text.ToString());
            newRosterdr["BreakTimeNHhr"] = rtpBreakTimeNH.SelectedDate.Value.ToString("HH:mm");
            newRosterdr["BreakTimeOThr"] = rtpBreakTimeOT.SelectedDate.Value.ToString("HH:mm");
            newRosterdr["NightShift"] = blnns;
            newRosterdr["RosterType"] = "NORMAL";
            newRosterdr["FlexibleWorkinghr"] = 0;
            newRosterdr["PullWorkTimein"] = 0;
            newRosterdr["FIFO"] = 0;
            newRosterdr["Rounding"] =  Convert.ToInt32(cmbRound.SelectedValue.ToString());
            newRosterdr["BreakTimeAfter"] = 0;
         
            /***************************************/



            string sql_delete = "delete from dbo.RosterDetail_Pattern where PatternId=" + Convert.ToInt32(Request.QueryString["Pattern"].ToString()) + "";
            DataAccess.ExecuteStoreProc(sql_delete);

            //if (dataSet.Tables["RosterDetail_Pattern"].Select("PatternId=" + Convert.ToInt32(Request.QueryString["Pattern"].ToString())).Length > 0)
            
                //delete the old row 
               //DataSet dsdata=new DataSet();
               //dsdata = GetDataSet("select * from dbo.RosterDetail_Pattern where PatternId=" + Convert.ToInt32(Request.QueryString["Pattern"].ToString()) + "");
               //for (int i = dsdata.Tables[0].Rows.Count - 1; i >= 0; i--)
               // {
               //     DataRow dr = dsdata.Tables[0].Rows[i];
               //     if (Convert.ToInt32(dr["PatternId"]) == Convert.ToInt32(Request.QueryString["Pattern"].ToString()))
               //     {
               //         dr.Delete();
               //     }

               // }
               // dsdata.AcceptChanges();
           
                dataSet.Tables["RosterDetail_Pattern"].Rows.Add(newRosterdr);
                dataAdapter.Update(dataSet, "RosterDetail_Pattern");
                dataSet.AcceptChanges();

                ShowMessageBox("Saved");
                Response.Redirect(Page.ResolveClientUrl("../TimeSheet/Pattern.aspx"));
        }

        protected static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();


            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
        }




        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }


    }
}
