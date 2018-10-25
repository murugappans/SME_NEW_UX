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
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SMEPayroll.Management
{
    public partial class RosterSettings : System.Web.UI.Page
    {
        protected string strMessage = ""; 
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        protected string val = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();

            

            if (!IsPostBack)
            {
                string sSQL = "Select ID, Roster_Name From Roster Where Company_ID={0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                ddlRoster.Items.Clear();
                ddlRoster.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
                while (dr.Read())
                {
                    ddlRoster.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                ddlRoster.Items.FindByValue("-1");

               // Button btnCopy = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");
                //Check If Advance TimeSheet or not

                //btnCopy.Click += new EventHandler(btnCopy_Click);

                string sqlAdv = "Select AdvTs from Company where Company_Id=" + Utility.ToInteger(Session["Compid"]);
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlAdv, null);
                
                while (dr1.Read())
                {
                    val = Utility.ToString(dr1.GetValue(0));
                }

               
                RadTimePicker rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpInTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";



                //rtp.TimeView.Attributes.Add("onblur", "ClientTimeSelected();");
                RadDatePicker raddtpicker = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadDatePicker1");
                raddtpicker.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(raddtpicker_SelectedDateChanged);

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpOutTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";




                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyInTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                if (val == "-1")
                {
                    rtp.Enabled = true;
                }
                else
                {
                    rtp.Enabled = false;
                }

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyOutTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                if (val == "-1")
                {
                    rtp.Enabled = true;
                }
                else
                {
                    rtp.Enabled = false;
                }

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateInTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                if (val == "-1")
                {
                    rtp.Enabled = true;
                }
                else
                {
                    rtp.Enabled = false;
                }

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateOutTime");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeNH");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                rtp = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeOT");
                rtp.TimeView.Interval = new TimeSpan(0, int.Parse("30"), 0);
                rtp.TimeView.StartTime = new TimeSpan(int.Parse("0"), 0, 0);
                rtp.TimeView.EndTime = new TimeSpan(int.Parse("23") + 1, 0, 0);
                rtp.TimeView.RenderDirection = RepeatDirection.Vertical;
                rtp.TimeView.Columns = 4;
                rtp.TimeView.TimeFormat = "HH:mm";
                rtp.DateInput.DateFormat = "HH:mm";
                rtp.DateInput.DisplayDateFormat = "HH:mm";

                RadNumericTextBox rdFlexHours = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdFlexHours")));
                rdFlexHours.Enabled = false;

                RosterTypeChanged();
            }


            if (!IsPostBack)
            {
                Telerik.Web.UI.RadDatePicker rdp1 = (Telerik.Web.UI.RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("rdStartDate");
                Telerik.Web.UI.RadDatePicker rdp2 = (Telerik.Web.UI.RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("rdEndDate");

                if (Session["RSFromDate"] == null)
                {
                    rdp1.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdp2.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                    Session["RSFromDate"] = System.DateTime.Now.ToShortDateString();
                    Session["RSToDate"] = System.DateTime.Now.ToShortDateString();
                }
                else
                {
                    rdp1.DbSelectedDate = Convert.ToDateTime(Session["RSFromDate"]).ToShortDateString();
                    rdp2.DbSelectedDate = Convert.ToDateTime(Session["RSToDate"]).ToShortDateString();
                }
            }
            
          
            

            //if (val != "-1")
            //{
            //    TimeSpan ts = new TimeSpan(0, Convert.ToInt32(val), 0);
            //    rtp1.SelectedDate = rtp.SelectedDate.Value.Subtract(ts);
            //}   
   

        }

        
      
        void raddtpicker_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            
            
        }

      
       
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearCont();
        }

        void ClearCont()
        {

            RadNumericTextBox txtrdFlexHours = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdFlexHours");
            RadNumericTextBox txtBoxClockInBef = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockInBef");
            RadNumericTextBox txtBoxClockOutBef = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockOutBef");
            RadNumericTextBox txtBoxClockInAft = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockInAft");
            RadNumericTextBox txtBoxClockOutAft = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockOutAft");
            RadNumericTextBox txtBreakTime = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBreakTime");
            RadNumericTextBox txtBreakTimeOT = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBreakTimeOT");
            RadTimePicker rtpInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpInTime");
            RadTimePicker rtpOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpOutTime");
            RadTimePicker rtpEarlyInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyInTime");
            RadTimePicker rtpEarlyOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyOutTime");
            RadTimePicker rtpLateInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateInTime");
            RadTimePicker rtpLateOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateOutTime");
            RadTimePicker rtpBreakTimeNH = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeNH");
            RadTimePicker rtpBreakTimeOT = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeOT");

            

            txtrdFlexHours.Text = "";
            txtBoxClockInBef.Text = "";
            txtBoxClockOutBef.Text = "";
            txtBoxClockInAft.Text = "";
            txtBoxClockOutAft.Text = "";
            txtBreakTime.Text = "";
            txtBreakTimeOT.Text = "";
            rtpInTime.Clear();
            rtpOutTime.Clear();
            rtpEarlyInTime.Clear();
            rtpInTime.Clear();
            rtpEarlyOutTime.Clear();
            rtpLateInTime.Clear();
            rtpLateOutTime.Clear();
            rtpBreakTimeNH.Clear();
            rtpBreakTimeOT.Clear();

            RadCalendar RadCal = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadCalendar1");
            RadCal.SelectedDates.Clear();

            Label lblMsg = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");
            lblMsg.Text = "";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int iCnt = 0;
            int retVal = 0;
            RadGrid RadSelect = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("RadGrid1");
            StringBuilder strUpdateBuildIn = new StringBuilder();
            foreach (GridItem item in RadSelect.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    String strID = dataItem["ID"].Text;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        iCnt = 1;
                        strUpdateBuildIn.Append(strID + ",");
                    }
                }
            }
            if (iCnt == 0)
            {
                strMessage = "Please select record to delete.";
            }
            else
            {
                retVal = DataAccess.ExecuteStoreProc("Delete From RosterDetail Where ID In(" + strUpdateBuildIn + "0)");
                bindgrid();
            }

        }


        protected void btnCopy_Click(object sender, EventArgs e)
        {
            RadDatePicker raddtpicker = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadDatePicker1");
            if (raddtpicker.SelectedDate != null)
            {
                //Check if dataexisti for same month in database 
                
                Button btncopy1 = (Button)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("btnCopy");

                string strSql = "Select count(*) from RosterDetail Where Roster_ID=" + Convert.ToInt32(ddlRoster.SelectedItem.Value) + " AND year(Convert(DATETIME, Roster_Date ,103)) =" + raddtpicker.SelectedDate.Value.Year + " AND month(Convert(DATETIME, Roster_Date ,103)) =" + raddtpicker.SelectedDate.Value.Month;
                int count = DataAccess.ExecuteScalar(strSql, null);
                bool flag = false;
                Label lblMsg = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");
                if (count > 0)
                {
                    lblMsg.Text = "Roster Data For Selected Date Already exists";
                    btncopy1.Enabled = false;

                }
                else
                {
                    lblMsg.Text = "";
                    btncopy1.Enabled = true;
                    flag = true;
                }

                if (flag)
                {
                    RadDatePicker raddtp1 = (RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadDatePicker1");

                    string commandString = "SELECT [ID],[Roster_ID],[Roster_Date],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[NightShift],[BreakTimeNHhr], [BreakTimeOThr],[RosterType],[FlexibleWorkinghr],[PullWorkTimein],[FIFO],[Rounding] FROM [RosterDetail] Where Roster_ID=" + Convert.ToInt32(ddlRoster.SelectedItem.Value);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
                    SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "RosterDetail");

                    DateTime dselscted = new DateTime();
                    dselscted = (DateTime)raddtp1.DateInput.SelectedDate;

                    //Check for ROster If existing data is arleady there or not
                    //[ID],[Roster_ID],[Roster_Date],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[NightShift],[BreakTimeNHhr], [BreakTimeOThr],[RosterType],[FlexibleWorkinghr],[PullWorkTimein],[FIFO],[Rounding] FROM [RosterDetail] Where 1=0
                    for (int i = 1; i <= DateTime.DaysInMonth(dselscted.Year, dselscted.Month); i++)
                    {
                        DateTime dt = new DateTime(dselscted.Year, dselscted.Month, i);
                        DataRow newRosterdr = dataSet.Tables["RosterDetail"].NewRow();


                        newRosterdr["Roster_ID"] = dataSet.Tables[0].Rows[0][1].ToString();
                        newRosterdr["Roster_Date"] = dt.ToShortDateString();
                        newRosterdr["InTime"] = dataSet.Tables[0].Rows[0]["InTime"].ToString();
                        newRosterdr["OutTime"] = dataSet.Tables[0].Rows[0]["OutTime"].ToString();
                        newRosterdr["EarlyInBy"] = dataSet.Tables[0].Rows[0]["EarlyInBy"].ToString();
                        newRosterdr["LateInBy"] = dataSet.Tables[0].Rows[0]["LateInBy"].ToString();
                        newRosterdr["EarlyOutBy"] = dataSet.Tables[0].Rows[0]["EarlyOutBy"].ToString();
                        newRosterdr["LateOutBy"] = "";
                        newRosterdr["ClockInBefore"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["ClockInBefore"].ToString());
                        newRosterdr["ClockInAfter"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["ClockInAfter"].ToString());
                        newRosterdr["ClockOutBefore"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["ClockOutBefore"].ToString());
                        newRosterdr["ClockOutAfter"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["ClockOutAfter"].ToString());
                        newRosterdr["BreakTimeNH"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["BreakTimeNH"].ToString());
                        newRosterdr["BreakTimeOT"] = Utility.ToInteger(dataSet.Tables[0].Rows[0]["BreakTimeOT"].ToString());
                        newRosterdr["BreakTimeNHhr"] = dataSet.Tables[0].Rows[0]["BreakTimeNHhr"].ToString();
                        newRosterdr["BreakTimeOThr"] = dataSet.Tables[0].Rows[0]["BreakTimeOThr"].ToString();
                        newRosterdr["NightShift"] = 0;
                        newRosterdr["RosterType"] = dataSet.Tables[0].Rows[0]["RosterType"].ToString();
                        newRosterdr["FlexibleWorkinghr"] = dataSet.Tables[0].Rows[0]["FlexibleWorkinghr"].ToString();
                        newRosterdr["PullWorkTimein"] = dataSet.Tables[0].Rows[0]["PullWorkTimein"].ToString();
                        newRosterdr["FIFO"] = Convert.ToInt32(dataSet.Tables[0].Rows[0]["FIFO"]);
                        newRosterdr["Rounding"] = Convert.ToInt32(dataSet.Tables[0].Rows[0]["Rounding"]);
                        dataSet.Tables["RosterDetail"].Rows.Add(newRosterdr);

                    }

                    dataAdapter.Update(dataSet, "RosterDetail");
                    dataSet.AcceptChanges();
                    bindgrid();

                    Label lblMsg1 = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");
                    lblMsg1.Text = "Roster Copied SuccessFully";
                }
            }
            else
            {
                Label lblMsg2 = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");
                lblMsg2.Text = "Please select Month to copy";
            
            }
        }



        protected void btnInsert_Click(object sender, EventArgs e)
        {
            string strHead = "";
            string strRostID = ddlRoster.SelectedItem.Value.ToString();
            Label lblMsg = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");

            if (strRostID != "-1")
            {

                //Update Roster With Rounding Data As Well As FIFO

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

                string strNoRoster = "";                

                DropDownList drpNoRoster = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpNoRoster"));
                strNoRoster = drpNoRoster.SelectedValue;
                string sqlRUpdate = "Update Roster SET NoRoster='" + strNoRoster + "' Where ID=" +  Convert.ToUInt32(strRostID);
                int valRet = DataAccess.ExecuteNonQuery(sqlRUpdate, null);

                
                DropDownList drpSunday = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpSunday"));                
                DropDownList drpROund = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpROund"));
                DropDownList drpPH = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpPH"));

                // ((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo"))   

                RadNumericTextBox txtBoxClockInBef = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockInBef");
                RadNumericTextBox txtBoxClockOutBef = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockOutBef");
                RadNumericTextBox txtBoxClockInAft = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockInAft");
                RadNumericTextBox txtBoxClockOutAft = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBoxClockOutAft");
                RadNumericTextBox txtBreakTime = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBreakTime");
                RadNumericTextBox txtBreakTimeOT = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("txtBreakTimeOT");
                RadNumericTextBox txtrdFlexHours = (RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdFlexHours");
                RadTimePicker rtpInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpInTime");
                RadTimePicker rtpOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpOutTime");
                RadTimePicker rtpEarlyInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyInTime");
                RadTimePicker rtpEarlyOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyOutTime");
                RadTimePicker rtpLateInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateInTime");
                RadTimePicker rtpLateOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateOutTime");
                RadTimePicker rtpBreakTimeNH = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeNH");
                RadTimePicker rtpBreakTimeOT = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeOT");

                RadNumericTextBox rdBreakTimeFlexi = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("radBreakTimeAfterFlexi")));
                RadNumericTextBox rdBreakTimeFlexiOT = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("radBreakTimeAfterFlexiOT")));

                int intBRKFALLNEXTDAY = 0;
                if (((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("brktimefallnextday")).SelectedValue == "NEXTDAY")
                {
                    intBRKFALLNEXTDAY = 1;
                }

                //int FLEXBRKTIMEINCLUDE = 0;
                //if (((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("flexBrktimeInclude")).SelectedValue == "FLEXBRKTIMEINCLUDE")
                //{
                //    FLEXBRKTIMEINCLUDE = 1;
                //}

                if (rtpInTime.IsEmpty == true && rtpInTime.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "In Time Cannot Remain Blank";
                }
                if (rtpOutTime.IsEmpty == true && rtpOutTime.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "Out Time Cannot Remain Blank";
                }
                if (rtpEarlyInTime.IsEmpty == true && rtpEarlyInTime.Enabled == true  )
                {
                    strMessage = strMessage + "<br/>" + "Early In By Time Cannot Remain Blank";
                }
                if (rtpEarlyOutTime.IsEmpty == true && rtpEarlyOutTime.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "Early Out By Time Cannot Remain Blank";
                }
                if (rtpLateInTime.IsEmpty == true && rtpLateInTime.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "Late In By Time Cannot Remain Blank";
                }
                //if (rtpLateOutTime.IsEmpty == true)
                //{
                //    strMessage = strMessage + "<br/>" + "Late Out By Time Cannot Remain Blank";
                //}
                if (rtpBreakTimeNH.IsEmpty == true && rtpBreakTimeNH.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "Break Time NH After Cannot Remain Blank";
                }
                if (rtpBreakTimeOT.IsEmpty == true && rtpBreakTimeOT.Enabled == true)
                {
                    strMessage = strMessage + "<br/>" + "Break Time OT After Cannot Remain Blank";
                }

                //Check if PH OR SUNDAY is not selected then at leats Noroster assign is selected
                if (drpSunday.SelectedValue == "0" || drpPH.SelectedValue == "0")
                {
                    if (drpNoRoster.SelectedValue == "0")
                    {
                        //strMessage = strMessage + "<br/>" + "Please Select Value for NoRoster Assign Day";
                    }
                }    

                if (strMessage.Length <= 0)
                {
                    int intaddouttime = 0;
                    int intaddlateintime = 0;
                    int intaddearlyoutintime = 0;
                    int intaddbreaknhtime = 0;
                    int intaddbreakottime = 0;

                    if (((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpFlex")).SelectedItem.Value == "0")
                    {

                        if (rtpOutTime.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                        {
                            intaddouttime = 1;
                        }
                        if (rtpLateInTime.Enabled == true)
                        {
                            if (rtpLateInTime.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                            {
                                intaddlateintime = 1;
                            }
                        }

                        if (rtpEarlyOutTime.Enabled == true)
                        {
                            if (rtpEarlyOutTime.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                            {
                                intaddearlyoutintime = 1;
                            }
                        }

                        if (rtpBreakTimeNH.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                        {
                            intaddbreaknhtime = 1;
                        }

                        if (rtpBreakTimeOT.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                        {
                            intaddbreakottime = 1;
                        }

                        if (rtpEarlyInTime.Enabled == true)
                        {
                            if (rtpEarlyInTime.SelectedDate.Value > rtpInTime.SelectedDate.Value)
                            {
                                strMessage = strMessage + "<br/>" + "Early In By Time Should be Less than In Time";
                            }
                        }

                        if (rtpEarlyOutTime.Enabled == true)
                        {
                            if (rtpEarlyOutTime.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                            {
                                if (rtpEarlyOutTime.SelectedDate.Value.AddDays(intaddearlyoutintime) >= rtpInTime.SelectedDate.Value && rtpEarlyOutTime.SelectedDate.Value.AddDays(intaddearlyoutintime) <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                                {
                                }
                                else
                                {
                                    strMessage = strMessage + "<br/>" + "Early Out By Time Should be Between In Time And Out Time";
                                }
                            }
                            else
                            {
                                if (rtpEarlyOutTime.SelectedDate.Value >= rtpInTime.SelectedDate.Value && rtpEarlyOutTime.SelectedDate.Value <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                                {
                                }
                                else
                                {
                                    strMessage = strMessage + "<br/>" + "Early Out By Time Should be Between In Time And Out Time";
                                }
                            }
                        }


                        if (rtpLateInTime.Enabled == true)
                        {
                            if (rtpLateInTime.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                            {
                                if (rtpLateInTime.SelectedDate.Value.AddDays(intaddlateintime) >= rtpInTime.SelectedDate.Value && rtpLateInTime.SelectedDate.Value.AddDays(intaddlateintime) <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                                {
                                }
                                else
                                {
                                    strMessage = strMessage + "<br/>" + "Late In By Time Should be Between In Time And Out Time";
                                }
                            }
                            else
                            {
                                if (rtpLateInTime.SelectedDate.Value >= rtpInTime.SelectedDate.Value && rtpLateInTime.SelectedDate.Value <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                                {
                                }
                                else
                                {
                                    strMessage = strMessage + "<br/>" + "Late In By Time Should be Between In Time And Out Time";
                                }
                            }
                        }


                        if (rtpBreakTimeNH.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                        {
                            if (rtpBreakTimeNH.SelectedDate.Value.AddDays(intaddbreaknhtime) >= rtpInTime.SelectedDate.Value && rtpBreakTimeNH.SelectedDate.Value.AddDays(intaddbreaknhtime) <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                            {
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "Break Time NH After Should be Between In Time And Out Time";
                            }
                        }
                        else
                        {
                            if (rtpBreakTimeNH.SelectedDate.Value >= rtpInTime.SelectedDate.Value && rtpBreakTimeNH.SelectedDate.Value <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                            {
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "Break Time NH After Should be Between In Time And Out Time";
                            }
                        }

                        //if (rtpBreakTimeOT.SelectedDate.Value < rtpInTime.SelectedDate.Value)
                        //{
                        //    if (rtpBreakTimeOT.SelectedDate.Value.AddDays(intaddbreakottime) >= rtpInTime.SelectedDate.Value && rtpBreakTimeOT.SelectedDate.Value.AddDays(intaddbreakottime) <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                        //    {
                        //        strMessage = strMessage + "<br/>" + "Break Time OT Cannot be between OT In Time and Out Time";
                        //    }
                        //    else
                        //    {
                        //    }
                        //}
                        //else
                        //{
                        //    if (rtpBreakTimeOT.SelectedDate.Value >= rtpInTime.SelectedDate.Value && rtpBreakTimeOT.SelectedDate.Value <= rtpOutTime.SelectedDate.Value.AddDays(intaddouttime))
                        //    {
                        //        strMessage = strMessage + "<br/>" + "Break Time OT Cannot be between OT In Time and Out Time";
                        //    }
                        //    else
                        //    {
                        //    }
                        //}
                    }

                    if (strMessage.Length <= 0)
                    {
                        string commandString = "SELECT [ID],[Roster_ID],[Roster_Date],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[NightShift],[BreakTimeNHhr], [BreakTimeOThr],[RosterType],[FlexibleWorkinghr],[PullWorkTimein],[FIFO],[Rounding],[BreakTimeAfter],[BreakTimeAftOtFlx],[BRKNEXTDAY] FROM [RosterDetail] Where 1=0";
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(commandString, Constants.CONNECTION_STRING);
                        SqlCommandBuilder sqlCb = new SqlCommandBuilder(dataAdapter);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet, "RosterDetail");
                        //RadGrid RadEmp = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadEmployee");
                        RadCalendar RadCal = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadCalendar1");
                        DropDownList drpTransfer = (DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpTransfer");
                        int cntRec = 0;
                        bool blnns = false;

                        //foreach (GridItem item in RadEmp.MasterTableView.Items)
                        //{
                        //    if (item is GridItem)
                        //    {
                        //        GridDataItem dataItem = (GridDataItem)item;
                        //        String strEmpID = dataItem["Emp_Code"].Text;
                        //        String strEmpName = dataItem["Emp_Name"].Text;
                        //        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];

                        //        if (chkBox.Checked == true)
                        //        {
                        if (RadCal.SelectedDates.Count > 0)
                        {
                            foreach (RadDate dateTime in RadCal.SelectedDates)
                            {
                                cntRec = 0;
                                int PullWorkTimein = 0;
                                int intFIFL = 0;
                                if (((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo")).SelectedValue == "FILO")
                                {
                                    intFIFL = 1;
                                }
                                SqlDataReader drCheck = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From RosterDetail Where Roster_ID="+ strRostID +" And Roster_Date=Convert(datetime,'" + dateTime.Date.ToString("dd/MM/yyyy", format) + "',103)", null);
                                if (drCheck.Read())
                                {
                                    cntRec = Convert.ToInt32(drCheck[0].ToString());
                                }
                                if (cntRec <= 0)
                                {

                                    PullWorkTimein =Convert.ToInt32(drpTransfer.SelectedValue);
                                    //Check Date if it is Public holiday or Sunday and then apply TransferIn as per that 
                                    //also Add FIFO as well as rounding values 
                                    //Check if it is Public holiday or not
                                    string strPH = "SELECT Count(*) FROM  public_holidays PH WHERE PH.companyid IN( -1," + Utility.ToInteger(Session["Compid"]) + ") AND Convert(VARCHAR(100),PH.holiday_date,103)='" + dateTime.Date.ToShortDateString()+"'";
                                    //string strPH = "SELECT Count(*) FROM  public_holidays PH WHERE Convert(VARCHAR(100),PH.holiday_date,103)='" + dateTime.Date.ToShortDateString() + "'";
                                    int phcnt = DataAccess.ExecuteScalar(strPH, null);

                                    if (phcnt > 0)
                                    {
                                        PullWorkTimein = Convert.ToInt32(drpPH.SelectedValue);
                                    }
                                    //Check if it is sunday
                                    
                                    string strSunday = "Select DateName(dw,Convert(datetime,' " + dateTime.Date.ToShortDateString() + "',103)) Roster_Day";
                                    DataSet dsnew = new DataSet();
                                    dsnew=DataAccess.FetchRS(CommandType.Text, strSunday, null);

                                    if (dsnew.Tables.Count > 0)
                                    {
                                        if (dsnew.Tables[0].Rows.Count > 0)
                                        {
                                            strSunday = dsnew.Tables[0].Rows[0][0].ToString();                                        
                                        }
                                    }

                                    if (strSunday.ToUpper() == "SUNDAY")
                                    {
                                        PullWorkTimein = Convert.ToInt32(drpSunday.SelectedValue); 
                                    }

                                    DataRow newRosterdr = dataSet.Tables["RosterDetail"].NewRow();
                                    if (((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpFlex")).SelectedItem.Value == "0")
                                    {
                                        if (rtpOutTime.SelectedDate.Value <= rtpInTime.SelectedDate.Value)
                                        {
                                            blnns = true;
                                        }
                                        newRosterdr["Roster_ID"] = strRostID;
                                        newRosterdr["Roster_Date"] = dateTime.Date.ToShortDateString();
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
                                                TimeSpan ts = new TimeSpan(0, Convert.ToInt32(val),0);
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
                                        newRosterdr["ClockInBefore"] = Utility.ToInteger(txtBoxClockInBef.Text.ToString());
                                        newRosterdr["ClockInAfter"] = Utility.ToInteger(txtBoxClockInAft.Text.ToString());
                                        newRosterdr["ClockOutBefore"] = Utility.ToInteger(txtBoxClockOutBef.Text.ToString());
                                        newRosterdr["ClockOutAfter"] = Utility.ToInteger(txtBoxClockOutAft.Text.ToString());
                                        newRosterdr["BreakTimeNH"] = Utility.ToInteger(txtBreakTime.Text.ToString());
                                        newRosterdr["BreakTimeOT"] = Utility.ToInteger(txtBreakTimeOT.Text.ToString());
                                        newRosterdr["BreakTimeNHhr"] = rtpBreakTimeNH.SelectedDate.Value.ToString("HH:mm");
                                        newRosterdr["BreakTimeOThr"] = rtpBreakTimeOT.SelectedDate.Value.ToString("HH:mm");
                                        newRosterdr["NightShift"] = blnns;
                                        newRosterdr["RosterType"] = "NORMAL";
                                        newRosterdr["FlexibleWorkinghr"] = 0;
                                        newRosterdr["PullWorkTimein"] = PullWorkTimein;
                                        newRosterdr["FIFO"] = intFIFL;
                                        newRosterdr["BRKNEXTDAY"] = intBRKFALLNEXTDAY;
                                       // newRosterdr["FLEXBRKTIMEINCLUDE"] = FLEXBRKTIMEINCLUDE;
                                        newRosterdr["Rounding"] = drpROund.SelectedValue;
                                        newRosterdr["BreakTimeAfter"] = 0;
                                        newRosterdr["BreakTimeAftOtFlx"] = 0;
                                    }
                                    else
                                    {
                                        newRosterdr["Roster_ID"] = strRostID;
                                        newRosterdr["Roster_Date"] = dateTime.Date.ToShortDateString();
                                        newRosterdr["BreakTimeNH"] = Utility.ToInteger(txtBreakTime.Text.ToString());
                                        newRosterdr["BreakTimeOT"] = Utility.ToInteger(txtBreakTimeOT.Text.ToString());
                                        newRosterdr["NightShift"] = blnns;
                                        newRosterdr["RosterType"] = "FLEXIBLE";
                                        newRosterdr["FlexibleWorkinghr"] = Utility.ToInteger(txtrdFlexHours.Text.ToString());
                                        //newRosterdr["PullWorkTimein"] = Utility.ToInteger(drpTransfer.SelectedItem.Value);
                                        newRosterdr["PullWorkTimein"] = PullWorkTimein;
                                        newRosterdr["FIFO"] = intFIFL;
                                        newRosterdr["BRKNEXTDAY"] = intBRKFALLNEXTDAY;
                                       // newRosterdr["FLEXBRKTIMEINCLUDE"] = FLEXBRKTIMEINCLUDE;
                                        newRosterdr["Rounding"] = drpROund.SelectedValue;
                                        if (rdBreakTimeFlexi.Text != "")
                                        {
                                            newRosterdr["BreakTimeAfter"] = rdBreakTimeFlexi.Text;
                                        }
                                        else
                                        {
                                            newRosterdr["BreakTimeAfter"] = 0;
                                        }

                                        if (rdBreakTimeFlexiOT.Text != "")
                                        {
                                            newRosterdr["BreakTimeAftOtFlx"] = rdBreakTimeFlexiOT.Text;
                                        }
                                        else
                                        {
                                            newRosterdr["BreakTimeAftOtFlx"] = 0;
                                        }
                                        
                                    }
                                    
                                    dataSet.Tables["RosterDetail"].Rows.Add(newRosterdr);
                                }
                                else
                                {
                                    strHead = "RecordExist";
                                    strMessage = strMessage + "<br/>" + " On Dated : " + dateTime.Date.ToShortDateString();
                                }
                            }
                        }
                        else
                        {
                            strMessage = strMessage + "<br/>" + " Please select Date";
                        }
                        //        }
                        //    }
                        //}

                        if (strMessage.Length <= 0)
                        {
                            dataAdapter.Update(dataSet, "RosterDetail");
                            dataSet.AcceptChanges();
                            bindgrid();
                        }
                        else
                        {
                            if (strHead == "RecordExist")
                            {
                                strMessage = "Roster Record Already Exist :" + strMessage;
                            }
                            dataSet.RejectChanges();
                            bindgrid();
                        }
                    }
                }

            }
            else
            {
                strMessage = "Please Select Roster Name";
            }
            lblMsg.Text = strMessage + "<br/>";
        }


        void Page_PreRender(Object sender, EventArgs e)
        {
            if (strMessage.Length > 0)
            {
                //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                ShowMessageBox(strMessage);
                strMessage = "";
            }
        }


        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
            //// Gets the executing web page            
            //Page currentPage = HttpContext.Current.CurrentHandler as Page;
            //// Checks if the handler is a Page and that the script isn't already on the Page            
            //if (currentPage != null && !currentPage.ClientScript.IsStartupScriptRegistered("ShowMessageBox"))
            //{
            //    currentPage.ClientScript.RegisterStartupScript(typeof(Alert), "ShowMessageBox", sbScript.ToString());
            //}
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            bindgrid();
            //bindgridDelete();
        }

        protected void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {
            bindgrid();
            //bindgridDelete();
        }

        protected void RadGrid1_SortCommand1(object source, GridSortCommandEventArgs e)
        {
            bindgrid();
            //bindgridDelete();
        }

        protected void RadGrid1_NeedDataSource1(object source, GridNeedDataSourceEventArgs e)
        {
        
        }

        protected void drpFlex_SelectedIndexChanged(object sender, EventArgs e)
        {
            RosterTypeChanged();
        }

        protected void RosterTypeChanged()
        {
            ClearCont();
            RadTimePicker rtpInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpInTime");
            RadTimePicker rtpOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpOutTime");
            RadTimePicker rtpEarlyInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyInTime");
            RadTimePicker rtpEarlyOutTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyOutTime");
            RadTimePicker rtpLateInTime = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpLateInTime");
            RadTimePicker rtpBreakTimeNH = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeNH");
            RadTimePicker rtpBreakTimeOT = (RadTimePicker)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpBreakTimeOT");
            RadNumericTextBox rdFlexHours = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rdFlexHours")));

            RadNumericTextBox rdBreakTimeFlexi = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("radBreakTimeAfterFlexi")));
            RadNumericTextBox rdBreakTimeFlexiOT = (((RadNumericTextBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("radBreakTimeAfterFlexiOT")));



            DropDownList drpTransfer = (DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpTransfer");
            DropDownList drpSunday = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpSunday"));
            DropDownList drpNoRoster = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpNoRoster"));
            DropDownList drpROund = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpROund"));
            DropDownList drpPH = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpPH"));

            // ((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo"))

            if (((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpFlex")).SelectedItem.Value == "0")
            {
                rdFlexHours.Enabled = false;
                rtpInTime.Enabled = true;
                rtpOutTime.Enabled = true;
                rtpEarlyInTime.Enabled = true;
                rtpEarlyOutTime.Enabled = true;
                rtpLateInTime.Enabled = true;
                rtpBreakTimeNH.Enabled = true;
                rtpBreakTimeOT.Enabled = true;
                rdBreakTimeFlexi.Enabled = false;
                rdBreakTimeFlexiOT.Enabled = false;

                ((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo")).Enabled = true;
                drpTransfer.Enabled = true;
                drpSunday.Enabled = true;
                drpNoRoster.Enabled = false;
                drpROund.Enabled = true;
                drpPH.Enabled = true;

            }
            else
            {
                rdFlexHours.Enabled = true;
                rtpInTime.Enabled = false;
                rtpOutTime.Enabled = false;
                rtpEarlyInTime.Enabled = false;
                rtpEarlyOutTime.Enabled = false;
                rtpLateInTime.Enabled = false;
                rtpBreakTimeNH.Enabled = false;
                rtpBreakTimeOT.Enabled = false;
                rdBreakTimeFlexi.Enabled = true;
                rdBreakTimeFlexiOT.Enabled = true;

                ((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo")).Enabled = true;
                drpTransfer.Enabled = true;
                drpSunday.Enabled = true;
                drpNoRoster.Enabled = false;
                drpROund.Enabled = true;
                drpPH.Enabled = true;
            }
        }

        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (e.Action == GridGroupsChangingAction.Group)
            {
            }
            if (e.Action == GridGroupsChangingAction.Ungroup)
            {
            }
            bindgrid();
        }

        protected void bindgrid()
        {
            string strRostID = ddlRoster.SelectedItem.Value.ToString();
            Label lblMsg = (Label)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("lblMsg");
            lblMsg.Text = "";

            RadGrid RadGrid1 = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("RadGrid1");
            string sSQL = "SELECT [ID],[Roster_ID],Convert(varchar,Roster_Date,103) [Roster_Date],[InTime],";
            sSQL =sSQL + "  [OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter]," ;
            sSQL = sSQL + " [ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeAftOtFlx],";
            sSQL=sSQL + "   FlexibleWorkinghr,DateName(dw,Convert(datetime,Roster_Date,103)) Roster_Day, ";
            sSQL = sSQL + " TransferIn=Case When PullWorkTimein=0 Then 'NONE' When PullWorkTimein=1 Then 'NH To OT1' When PullWorkTimein=2 Then 'NH To OT2' When PullWorkTimein=3 Then 'OT1 To NH' When PullWorkTimein=4 Then 'OT1 To OT2' When PullWorkTimein=5 Then 'OT2 To NH' " ; 
            sSQL =sSQL + " When PullWorkTimein=6 Then 'OT2 To OT1' When PullWorkTimein=7 Then 'NH+OT1 To NH' When PullWorkTimein=8 Then 'NH+OT1 To OT1' When PullWorkTimein=9 Then 'NH+OT1 To OT2' When PullWorkTimein=10 Then 'NH'When PullWorkTimein=11 Then 'NH+OT2 To OT2' When PullWorkTimein=12 Then 'NH+OT2 To NH' When PullWorkTimein=13 Then 'OT1+OT2 To NH' When PullWorkTimein=14 Then 'OT1+OT2 To OT1' When PullWorkTimein=15 Then 'OT1+OT2 To OT2' When PullWorkTimein=16 Then 'NH+OT1+OT2 To NH' When PullWorkTimein=17 Then 'NH+OT1+OT2 To OT1' When PullWorkTimein=18 Then 'NH+OT1+OT2 To OT2' End," ;
            sSQL = sSQL + " FIFO = CASE WHEN FIFO=1 THEN 'YES' WHEN FIFO=0 THEN 'NO' ELSE 'NO' END ,Rounding= CASE WHEN Rounding >0 THEN Rounding ELSE 0 END,BreakTimeAfter FROM RosterDetail RD Where RD.Roster_ID=" + strRostID + " Order By Convert(datetime,Roster_Date,103) Desc";


            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

            RadCalendar RadCal = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadCalendar1");
            RadCal.SelectedDates.Clear();
            RadCal.SpecialDays.Clear();
            RadDate RadDt = new RadDate();
            DateTime dt = new DateTime();
            RadDate[] dates = new RadDate[ds.Tables[0].Rows.Count];
            RadCalendarDay[] rcd = new RadCalendarDay[ds.Tables[0].Rows.Count];
            int i = 0;

            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    dt = Convert.ToDateTime(dr["Roster_Date"].ToString(), format);
                    dates[i] = new RadDate(dt);
                    rcd[i] = new RadCalendarDay();
                    rcd[i].Date = dt;
                    rcd[i].IsSelectable = false;
                    rcd[i].IsDisabled = true;
                    //rcd[i].ItemStyle.BackColor = System.Drawing.Color.Gray;
                    rcd[i].ItemStyle.CssClass = "SelectedDaysCss";
                    i += 1;
                }
            }
            catch (Exception e)
            {
                Response.Write("File Creation failed. Reason is as follows : " + i.ToString());
            }


            RadCal.SelectedDates.AddRange(dates);
            RadCal.SpecialDays.AddRange(rcd);
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }
        protected void bindgridDelete()
        {
            try {
                    string strRostID = ddlRoster.SelectedItem.Value.ToString();

                   string startdate = ((Telerik.Web.UI.RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("rdStartDate")).SelectedDate.ToString();
                    Telerik.Web.UI.RadDatePicker rdp2 = (Telerik.Web.UI.RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("rdEndDate");
                    Telerik.Web.UI.RadDatePicker rdp1 = (Telerik.Web.UI.RadDatePicker)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("rdStartDate");

                    string sDate = rdp1.SelectedDate.Value.Year + "-" + rdp1.SelectedDate.Value.Month + "-" + rdp1.SelectedDate.Value.Day;
                    string eDate = rdp2.SelectedDate.Value.Year + "-" + rdp2.SelectedDate.Value.Month + "-" + rdp2.SelectedDate.Value.Day;

                    RadGrid RadGrid1 = (RadGrid)RadPanelBar1.FindItemByValue("ctrlPanel2").FindControl("RadGrid1");
                    string sSQL = "SELECT [ID],[Roster_ID],Convert(varchar,Roster_Date,103) [Roster_Date],[InTime],";
                    sSQL = sSQL + "  [OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],";
                    sSQL = sSQL + " [ClockOutBefore],[ClockOutAfter],[BreakTimeNH],[BreakTimeOT],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeAftOtFlx],";
                    sSQL = sSQL + "   FlexibleWorkinghr,DateName(dw,Convert(datetime,Roster_Date,103)) Roster_Day, ";
                    sSQL = sSQL + " TransferIn=Case When PullWorkTimein=0 Then 'NONE' When PullWorkTimein=1 Then 'NH To OT1' When PullWorkTimein=2 Then 'NH To OT2' When PullWorkTimein=3 Then 'OT1 To NH' When PullWorkTimein=4 Then 'OT1 To OT2' When PullWorkTimein=5 Then 'OT2 To NH' ";
                    sSQL = sSQL + " When PullWorkTimein=6 Then 'OT2 To OT1' When PullWorkTimein=7 Then 'NH+OT1 To NH' When PullWorkTimein=8 Then 'NH+OT1 To OT1' When PullWorkTimein=9 Then 'NH+OT1 To OT2' When PullWorkTimein=10 Then 'NH+OT2 To OT1'When PullWorkTimein=11 Then 'NH+OT2 To OT2' When PullWorkTimein=12 Then 'NH+OT2 To NH' When PullWorkTimein=13 Then 'OT1+OT2 To NH' When PullWorkTimein=14 Then 'OT1+OT2 To OT1' When PullWorkTimein=15 Then 'OT1+OT2 To OT2' When PullWorkTimein=16 Then 'NH+OT1+OT2 To NH' When PullWorkTimein=17 Then 'NH+OT1+OT2 To OT1' When PullWorkTimein=18 Then 'NH+OT1+OT2 To OT2' End,";
                    sSQL = sSQL + " FIFO = CASE WHEN FIFO=1 THEN 'YES' WHEN FIFO=0 THEN 'NO' ELSE 'NO' END ,Rounding= CASE WHEN Rounding >0 THEN Rounding ELSE 0 END,BreakTimeAfter FROM RosterDetail RD Where RD.Roster_ID=" + strRostID + " and Roster_Date >='" + sDate + "' and Roster_Date <= '" + eDate + "' Order By Convert(datetime,Roster_Date,103) Desc";


                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                    //RadCalendar RadCal = (RadCalendar)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("RadCalendar1");
                    //RadCal.SelectedDates.Clear();
                    //RadCal.SpecialDays.Clear();
                    //RadDate RadDt = new RadDate();
                    //DateTime dt = new DateTime();
                    //RadDate[] dates = new RadDate[ds.Tables[0].Rows.Count];
                    //RadCalendarDay[] rcd = new RadCalendarDay[ds.Tables[0].Rows.Count];
                    //int i = 0;

                    //try
                    //{
                    //    foreach (DataRow dr in ds.Tables[0].Rows)
                    //    {
                    //        dt = Convert.ToDateTime(dr["Roster_Date"].ToString(), format);
                    //        dates[i] = new RadDate(dt);
                    //        rcd[i] = new RadCalendarDay();
                    //        rcd[i].Date = dt;
                    //        rcd[i].IsSelectable = false;
                    //        rcd[i].IsDisabled = true;
                    //        //rcd[i].ItemStyle.BackColor = System.Drawing.Color.Gray;
                    //        rcd[i].ItemStyle.CssClass = "SelectedDaysCss";
                    //        i += 1;
                    //    }
                    //}
                    //catch (Exception e)
                    //{
                    //    Response.Write("File Creation failed. Reason is as follows : " + i.ToString());
                    //}


                    //RadCal.SelectedDates.AddRange(dates);
                    //RadCal.SpecialDays.AddRange(rcd);

                    RadGrid1.DataSource = ds;
                    RadGrid1.DataBind();

                    Session["RSFromDate"] = rdp1.SelectedDate;
                    Session["RSToDate"] = rdp2.SelectedDate;
                
            }
            catch (Exception e) { }
        }

        protected void bindgridDelete(object sender, EventArgs e)
        {
            bindgridDelete();
        }


        protected void ddlRoster_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearCont();
            bindgrid();

            //Check Company Details and Set the PH,Sunday, NRA,Rounding and FIFO details
            string sqlCompany = "SELECT  c.TsPublicH,c.Sunday,c.NoRoster,c.FIFO,c.Rounding  FROM Company c Where c.Company_Id=" + Utility.ToInteger(Session["Compid"]);
            DataSet dsCompany = new DataSet();
            dsCompany = DataAccess.FetchRS(CommandType.Text, sqlCompany, null);
            if (dsCompany.Tables.Count > 0)
            {
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    DropDownList drpPH = (DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpPH");
                    DropDownList drpSunday = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpSunday"));
                    DropDownList  drpNoRoster = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpNoRoster"));
                    DropDownList drpROund = ((DropDownList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("drpROund"));
                    if (dsCompany.Tables[0].Rows[0]["TsPublicH"].ToString() != "")
                    {
                        drpPH.SelectedValue = dsCompany.Tables[0].Rows[0]["TsPublicH"].ToString();
                    }
                    else
                    {
                        drpPH.SelectedIndex = 0;
                    }

                    if (dsCompany.Tables[0].Rows[0]["Sunday"].ToString() != "")
                    {
                        drpSunday.SelectedValue = dsCompany.Tables[0].Rows[0]["Sunday"].ToString();
                    }
                    else
                    {
                        drpSunday.SelectedIndex = 0;
                    }
                    if (dsCompany.Tables[0].Rows[0]["NoRoster"].ToString() != "")
                    {
                        drpNoRoster.SelectedValue = dsCompany.Tables[0].Rows[0]["NoRoster"].ToString();
                    }
                    else
                    {
                        drpNoRoster.SelectedIndex = 0;
                    }

                    if (dsCompany.Tables[0].Rows[0]["Rounding"].ToString() != "")
                    {
                        drpROund.SelectedValue = dsCompany.Tables[0].Rows[0]["Rounding"].ToString();
                    }
                    else
                    {
                        drpROund.SelectedIndex = 0;
                    }

                    if (dsCompany.Tables[0].Rows[0]["FIFO"].ToString() == "1")
                    {
                        ((CheckBoxList)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("chkFiFo")).SelectedIndex = 0;
                    }
                }
            }

           
        }
    }
}


