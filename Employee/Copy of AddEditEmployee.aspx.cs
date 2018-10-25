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
using System.Xml;
using Telerik.Web.UI.Calendar;
using Telerik.Web.UI.Calendar.View;
using System.Collections.Generic;

namespace SMEPayroll.employee
{
    public partial class AddEditEmployee : System.Web.UI.Page
    {

        private DateTime startDate;
        private DateTime endDate;
        private bool IsCalculate = true;
        /// <summary>
        /// ///kumar , sandi added on 3/7/2014(Double-Clickable Calendar)
        /// </summary>
        /// 
        private int c;
        private int controlName;
        private string Full_Off;
        private static List<DateTime> list_off = new List<DateTime>();
        private static List<DateTime> list_full = new List<DateTime>();
        private static List<DateTime> list_holiday = new List<DateTime>();

        protected void clickcalender_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    Full_Off = Request.Params.Get("__EVENTARGUMENT2");
                    controlName = Convert.ToInt32(Request.Params.Get("__EVENTARGUMENT1"));
                    if (Full_Off == "F")
                    {
                        foreach (DateTime dHOff in list_off)
                        {
                            if (list_off.Contains(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName)))
                            {
                                list_off.Remove(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName));
                                break;
                            }
                        }
                    }
                    if (Full_Off == "H")
                    {
                        if (!list_off.Contains(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName)))
                            list_off.Add(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName));
                    }
                    if (Full_Off == "H")
                    {
                        foreach (DateTime dHFull in list_full)
                        {
                            if (list_full.Contains(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName)))
                            {
                                list_full.Remove(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName));
                                break;
                            }
                        }
                    }
                    if (Full_Off == "F")
                    {
                        if (!list_full.Contains(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName)))
                            list_full.Add(new DateTime(click_calender.VisibleDate.Year, click_calender.VisibleDate.Month, controlName));
                    }
                }

                Session["list_offdays"] = list_off;
                Session["list_fulldays"] = list_full;

                Label lb = new Label();

                lb.Text = "lb";
                e.Cell.Controls.Add(lb);

                if (list_holiday.Contains(e.Day.Date))
                {
                    e.Cell.BackColor = System.Drawing.Color.Red;
                }
                else if (list_full.Contains(e.Day.Date))
                {
                    e.Cell.BackColor = System.Drawing.Color.Green;
                }
                else if (list_off.Contains(e.Day.Date))
                {
                    e.Cell.BackColor = System.Drawing.Color.Yellow;
                }

                e.Cell.Text = string.Format("<a title=\"{1}\" ondblclick= dbclickfunction(this)   onclick= clickfunction(this)>{0}</a>", e.Day.Date.Day.ToString(), string.Format("{0:m}", e.Day.Date), e.SelectUrl);
            }
            catch (Exception ex) { }
        }
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
            obj2.ToolbarButtonClick(e, RadGrid6, Utility.ToString(Session["Username"]));

        }
        protected void clickcalender_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["list_offdays"] != null)
                {
                    List<DateTime> newList = (List<DateTime>)Session["list_offdays"];

                    foreach (DateTime dt in newList)
                    {
                        click_calender.SelectedDates.Add(dt);
                    }
                    list_off.Clear();
                }
                if (Session["list_fulldays"] != null)
                {
                    List<DateTime> List_f = (List<DateTime>)Session["list_fulldays"];

                    foreach (DateTime dt1 in List_f)
                    {
                        click_calender.SelectedDates.Add(dt1);
                    }

                    list_full.Clear();
                }
            }
            catch (Exception ex) { }
        }
        protected void clickcalender_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            //Added by Sandi on 14/03/2014
            try
            {
                //string message;
                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //if (IsPostBack)
                //{
                //    if (list_off.Count > 0)
                //    {
                //        if (list_off.Count != Load_Off_Days_FullandHalf("H").Count)
                //        {
                //            message = "Do you want to move to next month without saving?";

                //            sb.Append("return confirm('");

                //            sb.Append(message);

                //            sb.Append("');");

                //            ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
                //        }
                //    }
                //    if (list_full.Count > 0)
                //    {
                //        if (list_full.Count != Load_Off_Days_FullandHalf("F").Count)
                //        {
                //            message = "Do you want to move to next month without saving?";

                //            sb.Append("return confirm('");

                //            sb.Append(message);

                //            sb.Append("');");

                //            ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
                //        }
                //    }
                //}
               
                if (ValidatePayrollSubmitted() == true)
                {
                    btnsavecal.Enabled = true;
                    btnClearOff.Enabled = true;
                }
                else
                {
                    btnsavecal.Enabled = false;
                    btnClearOff.Enabled = false;
                }
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// //kumar, sandi added on 3/7/2014(Double-Clickable Calendar)
        /// </summary>

        string Cost_Businessunit, Cost_Region, Cost_Category;
        DataSet dsnew;
        string ismaster = "";
        string ismastertempemp = "";
        string EmpName = "";
        protected HtmlGenericControl btnsave;
        protected int iCountOld = 0;
        bool bln;

        string strMessage = "";
        static string varFileName = "";
        static int s = 0;
        string compcode = "", EmpCode = "", emp_code = "";
        int compid;
        int activeSuperAdminCount;
        protected bool bViewSalAllowed = false;
        DataSet sqlLeaveDs = null;

        # region Style
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        # endregion Style

        # region Datafill

        protected void Page_Load(object sender, EventArgs e)
        {

             Session["mobilescancode"] = "False";

            if (Session["Country"].ToString() == "383")
            {
                IsCalculate = false;
                               }
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (drpPayFrequency.Value == "H")
            {
                //custxtPayRate.Text = "0";
            }
            cmbprobation.Attributes.Add("onChange", "ShowNoticePeriod()");
            //dubai

            //if (Session["Country"].ToString() == "383")
            //{

               // this.RadGrid6.Columns[7].Visible = false;
           // }

            lblEmp.Visible = false;
            lblEmpName.Visible = false;
            tabPageLeaveHistory.Enabled = true;
            Session.LCID = 2057;
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            SqlDataSource7.ConnectionString = Session["ConString"].ToString();
            SqlDataSource8.ConnectionString = Session["ConString"].ToString();
            SqlDataSource9.ConnectionString = Session["ConString"].ToString();
            SqlDataSource10.ConnectionString = Session["ConString"].ToString();
            SqlDataSource11.ConnectionString = Session["ConString"].ToString();
            SqlDataSource12.ConnectionString = Session["ConString"].ToString();
            SqlDataSource13.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource_ActiveInactive.ConnectionString = Session["ConString"].ToString();
            for (int i = 0; i < tbsEmp.Tabs.Count; i++)
            {
                tbsEmp.Tabs[i].Enabled = false;
            }

            AjaxPro.Utility.RegisterTypeForAjax(typeof(AddEditEmployee));
            trv1.Visible = false;
            trv4.Visible = false;

            trv2.Visible = false;
            trv5.Visible = false;

            trv3.Visible = false;
            trv6.Visible = false;

            trv7.Visible = false;
            trv8.Visible = false;
            btnLeaveUpdate.Visible = false;

            radListPayType.SelectedIndexChanged += new EventHandler(radListPayType_SelectedIndexChanged);
            //if (Session["V1Formula"].ToString() != "0")
            //{
            trv1.Visible = true;
            trv4.Visible = true;
            //  lblv1rate.Text = Session["V1Text"].ToString();
            lblv1rate.Text = Convert.ToString(Session["V1Text"]);
            //}
            // if (Session["V2Formula"].ToString() != "0")
            //{
            trv2.Visible = true;
            trv5.Visible = true;
            //lblv2rate.Text = Session["V2Text"].ToString();
            lblv2rate.Text = Convert.ToString(Session["V2Text"]);
            //}
            // if (Session["V3Formula"].ToString() != "0")
            //{
            trv3.Visible = true;
            trv6.Visible = true;
            //lblv3rate.Text = Session["V3Text"].ToString();
            lblv3rate.Text = Convert.ToString(Session["V3Text"]);
            //}
            // if (Session["V4Formula"].ToString() != "0")
            //{
            trv7.Visible = true;
            trv8.Visible = true;
            //lblv4rate.Text = Session["V4Text"].ToString();
            lblv4rate.Text = Convert.ToString(Session["V4Text"]);
            //}

            bViewSalAllowed = Utility.AllowedAction1(Utility.ToString(Session["Username"]), "View Salary");

            if (!bViewSalAllowed)
            {
                //SalaryPanel.Visible = false;
            }

            compcode = Session["CompCode"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            EmpCode = Session["EmpCode"].ToString();
            //rdWPExpDate.MinDate = DateTime.Now.AddDays(0);
            //rdcsoc.MinDate = DateTime.Now.AddDays(0);
            //rdinsurance.MinDate = DateTime.Now.AddDays(0);
            //rdpassport.MinDate = DateTime.Now.AddDays(0);
            //rdppissuedate.MaxDate = DateTime.Now;
            rdPRDate.MinDate = DateTime.Now.AddYears(-100);
            emp_code = Request.QueryString["empcode"];

            //if (emp_code != null) { tbstraining.Enabled = true; tbssafety.Enabled = true; }
            //else { tbstraining.Enabled = false; tbssafety.Enabled = false; }

            RadUpload1.Visible = false;

            if (emp_code != null)
            {
                SqlDataSource2.SelectCommand = " Select EM.Emp_Code,DM.ID,DC.Category_Name,DM.Document_Name,DM.FileName From DocumentMappedToEmployee DM Inner Join DocumentCategory DC On DM.Category_ID=DC.ID Inner Join Employee EM On DM.Emp_ID = Em.Emp_Code Where DM.Emp_ID=" + Convert.ToString(emp_code);
            }
            else
            {
                SqlDataSource2.SelectCommand = " Select EM.Emp_Code,DM.ID,DC.Category_Name,DM.Document_Name,DM.FileName From DocumentMappedToEmployee DM Inner Join DocumentCategory DC On DM.Category_ID=DC.ID Inner Join Employee EM On DM.Emp_ID = Em.Emp_Code Where DM.Emp_ID=-111";
                if (radListPayType.SelectedValue == "")
                {
                    radListPayType.SelectedValue = "0";
                }
            }
            //Added by Sandi on 13/3/2014
            if (emp_code != null)
            {
                string sqlEmployee = "select CustomizedCal from employee where Company_Id =" + compid + " and emp_code='" + EmpCode + "'";
                SqlDataReader drEmp = DataAccess.ExecuteReader(CommandType.Text, sqlEmployee, null);
                string valCustomizeCal = "";
                while (drEmp.Read())
                {
                    valCustomizeCal = (string)drEmp.GetValue(0).ToString();
                }
                RadTab CalendarTab = tbsEmp.FindTabByText("Employee Calendar");
                if (valCustomizeCal == "default" || valCustomizeCal == "d" || valCustomizeCal == "")
                {
                    tbsEmp.Tabs[17].Visible = false; //Added by Sandi on 27/3/2014
                    CalendarTab.Visible = false;
                    tbsempcalendar.Enabled = false;
                }
                else if (valCustomizeCal == "half")
                {
                    tbsEmp.Tabs[17].Visible = false; //Added by Sandi on 27/3/2014
                    CalendarTab.Visible = false;
                    tbsempcalendar.Enabled = false;
                }
                else if (valCustomizeCal == "full")
                {
                    tbsEmp.Tabs[17].Visible = true; //Added by Sandi on 27/3/2014
                    CalendarTab.Visible = true;
                    tbsempcalendar.Enabled = true;
                }
            }
            else //Added by Sandi on 06/05/2014
            {
                string sqlEmployee = "select CustomizedCal from company where Company_Id =" + compid + "";
                SqlDataReader drComp = DataAccess.ExecuteReader(CommandType.Text, sqlEmployee, null);
                string valCustomizeCal = "";
                while (drComp.Read())
                {
                    valCustomizeCal = (string)drComp.GetValue(0).ToString();
                }
                RadTab CalendarTab = tbsEmp.FindTabByText("Employee Calendar");
                if (valCustomizeCal == "default" || valCustomizeCal == "d" || valCustomizeCal == "")
                {
                    tbsEmp.Tabs[17].Visible = false; 
                    CalendarTab.Visible = false;
                    tbsempcalendar.Enabled = false;
                }
                else if (valCustomizeCal == "half" || valCustomizeCal == "h")
                {
                    tbsEmp.Tabs[17].Visible = false; 
                    CalendarTab.Visible = false;
                    tbsempcalendar.Enabled = false;
                }
                else if (valCustomizeCal == "full" || valCustomizeCal == "f")
                {
                    tbsEmp.Tabs[17].Visible = true; 
                    CalendarTab.Visible = true;
                    tbsempcalendar.Enabled = true;
                }
            }
            //End Added
            if (radListPayType.SelectedValue == "0")
            {
                drpCurrBank1.Enabled = false;
                drpCurrBank2.Enabled = false;
                drpPaymentMode.Enabled = false;
                drpPaymentMode1.Enabled = false;
            }
            else
            {
                drpCurrBank1.Enabled = true;
                drpCurrBank2.Enabled = true;
                lblPerc.Text = "";
                lblPercSB2.Text = "";
                drpPaymentMode.Enabled = true;
                drpPaymentMode1.Enabled = true;
            }
            if (!IsPostBack)
            {
                click_calender.VisibleDate = DateTime.Today;

                #region Yeardropdown
                drpYear.DataBind();
                #endregion
                int mc = 0;


                string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid;
                SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

                while (drcon.Read())
                {
                    if (drcon.GetValue(1) == null || drcon.GetValue(1).ToString() == "")
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
                    drpCurrency.Enabled = false;
                }

                //Currecny Data binding
                DataSet dscurr = new DataSet();
                string sqlCurr = "Select id, Currency + ':-->' + Symbol Curr from currency";
                dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
                drpCurrency.DataSource = dscurr.Tables[0];
                drpCurrency.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
                drpCurrency.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
                drpCurrency.DataBind();


                drpCurrBank1.DataSource = dscurr.Tables[0];
                drpCurrBank1.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
                drpCurrBank1.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
                drpCurrBank1.DataBind();


                drpCurrBank2.DataSource = dscurr.Tables[0];
                drpCurrBank2.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
                drpCurrBank2.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
                drpCurrBank2.DataBind();
                LoadFormValues();
                drpCurrBank1.Enabled = false;
                drpCurrBank2.Enabled = false;

                string val5 = cmbPayMode.Value;
                if (val5.Trim() == "-select-")
                {
                    val5 = "-1";
                }
                string sqlCurr3 = "Select CurrencyId from girobanks Where id=" + val5;

                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlCurr3, null);

                int curr = 0;

                while (sqldr.Read())
                {
                    if (sqldr.GetValue(0) != null && sqldr.GetValue(0).ToString() != "")
                    {
                        curr = Convert.ToInt32(sqldr.GetValue(0).ToString());
                    }
                }
                if (curr == 0)
                {
                    drpCurrBank1.SelectedIndex = 0;
                }
                else
                {
                    drpCurrBank1.SelectedValue = curr.ToString();
                }



                DataSet dscurrEP = new DataSet();
                string sqlCurrEP = "Select * from EmpHistoryRecordMaster where company_id=" + compid;

                dscurrEP = DataAccess.FetchRS(CommandType.Text, sqlCurrEP, null);
                drpProgReason.DataSource = dscurrEP.Tables[0];
                drpProgReason.DataTextField = dscurrEP.Tables[0].Columns["HistoryDetails"].ColumnName.ToString();
                drpProgReason.DataValueField = dscurrEP.Tables[0].Columns["Id"].ColumnName.ToString();
                drpProgReason.DataBind();
                //Select * from EmpHistoryRecordMaster
                //ku  for custom hr rate
                
                string sqlCurr1 = "select CurrencyID from company where Company_Id =" + compid;
                SqlDataReader drcurr = DataAccess.ExecuteReader(CommandType.Text, sqlCurr1, null);
                string val = "";
                while (drcurr.Read())
                {
                    val = (string)drcurr.GetValue(0).ToString();
                }

                drpCurrency.SelectedValue = Utility.ToString(val.ToString());               
            }
            else if (oHidden.Value == "Save") EmpSave();

            if (emp_code != null)
            {

                string customHrsql = "select isnull(max(h.Value),0) as hr  from HrRateAssignToEmployee e inner join HrRate h on e.HrRateId= h.Id where e.Emp_id=" + emp_code;
                SqlDataReader cushour_reader = DataAccess.ExecuteReader(CommandType.Text, customHrsql, null);

                decimal hvalue = 0.00m;
                decimal _basic_rate = 0.00m;
                while (cushour_reader.Read())
                {
                    hvalue = (decimal)Utility.ToDouble(cushour_reader.GetValue(0).ToString());
                }

                if (hvalue <= 0)
                {
                    this.hourtextbox.Visible = false;
                    this.hourrate.Visible = false;
                    this.customRate.Text = "";
                    this.CustomHour.Text = "";
                }

                _basic_rate = (decimal)Utility.ToDouble(this.txtPayRate.Text);
                this.hourtextbox.Text = hvalue.ToString();
                if (_basic_rate > 0 && hvalue > 0)
                {
                    decimal hrrate = Math.Round(((_basic_rate * 12) / (52 * hvalue)), 2);
                    this.hourrate.Text = hrrate.ToString();
                }
            }


            //foreach (CalendarView view in calEmpOff.CalendarView.ChildViews)
            //{
            //    ((MonthView)view).TitleFormat = "MMM yy";
            //}

            if (!IsPostBack)
            {
                startDate = Convert.ToDateTime(GetFirstDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());
                endDate = Convert.ToDateTime(GetLastDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());

                Session["startDate"] = startDate;
                Session["endDate"] = endDate;

                Load_Public_Holiday();

                //Load_Public_Holiday();
                //Load_Off_Days();

                list_off = Load_Off_Days_FullandHalf("H");
                list_full = Load_Off_Days_FullandHalf("F");

                //Added by Sandi on 14/03/2014
                if (ValidatePayrollSubmitted() == true)
                {
                    btnsavecal.Enabled = true;
                    btnClearOff.Enabled = true;                   
                }
                else
                {
                    btnsavecal.Enabled = false;
                    btnClearOff.Enabled = false;                  
                }
            }

            #region Load costing dropdown

           // if (Convert.ToString(Request.QueryString["empcode"]) != null || Convert.ToString(Request.QueryString["empcode"]) != "")
            if(!string.IsNullOrEmpty(Request.QueryString["empcode"]))
            {

                string SQL = "select  [Cost_Businessunit],[Cost_Region],[Cost_Category] from employee where Emp_Code=" + Convert.ToString(Request.QueryString["empcode"]);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                while (dr.Read())
                {
                    Cost_Businessunit = Utility.ToString(dr.GetValue(0));
                    Cost_Region = Utility.ToString(dr.GetValue(1));
                    Cost_Category = Utility.ToString(dr.GetValue(2));
                }

                cmbbusinessunit.Value = Cost_Businessunit;
                cmbRegion.Value = Cost_Region;
                cmbCategory.Value = Cost_Category;
            }
            #endregion
            if (!string.IsNullOrEmpty(this.hourtextbox.Text))
            {

                decimal val = (decimal)Utility.ToDouble(this.hourtextbox.Text);
                if (val > 0)
                {
                    this.custxtHourlyRate.Disabled = true;
                    this.drpHourlyMode.Disabled = true;
                }
                else
                {
                    this.custxtHourlyRate.Disabled = false;
                    this.drpHourlyMode.Disabled = false;
                }
            }
         








            //km
          //this.Page.Unload += new EventHandler(Page_Unload);
            this.click_calender.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.clickcalender_DayRender);
            this.click_calender.SelectionChanged += new EventHandler(this.clickcalender_SelectionChanged);          
        }

        # region Employee Calendar       
        // ---------------------- Added By Su Mon ----------------------      
        //protected void calEmpOff_PreRender(object sender, System.EventArgs e)
        //{
            //foreach (CalendarView view in calEmpOff.CalendarView.ChildViews)
            //{
            //    ((MonthView)view).TitleFormat = "MMM yy";


            //}

            //startDate = ((MonthView)calEmpOff.CalendarView).MonthStartDate;
            //endDate = ((MonthView)calEmpOff.CalendarView).MonthEndDate;
            //Session["startDate"] = startDate;
            //Session["endDate"] = endDate;
            //Load_Public_Holiday();
            //Load_Off_Days();
        //}
        //protected void CustomizeDates(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        //{
        //    DateTime CurrentDate = e.Day.Date;
        //    DataTable dtDates = new DataTable();
        //    dtDates = (DataTable)Session["dtHoliday"];
        //    for (int i = 0; i < dtDates.Rows.Count; i++)
        //    {
        //        DateTime dt = Convert.ToDateTime(dtDates.Rows[i]["holiday_date"].ToString());
        //        if (CurrentDate.Date == dt.Date)
        //        {
        //            TableCell currentCell = e.Cell;
        //            currentCell.Style["background-color"] = "#b6e3f4";
        //            currentCell.Text = dt.Date.Day.ToString();
        //        }
        //    }
        //}     
        //protected void Load_Off_Days()
        //{
        //    try
        //    {
        //        EmpCode = Session["EmpCode"].ToString();

        //        DataSet dsOffDays = new DataSet();
        //        string strSQL = "";
        //        strSQL = "select off_date from emp_calender where emp_code=" + emp_code + " and convert(datetime,off_date,103) between convert(datetime,'" + startDate + "',103) and convert(datetime,'" + endDate + "',103)";
        //        dsOffDays = DataAccess.FetchRS(CommandType.Text, strSQL, null);

        //        calEmpOff.SelectedDates.Clear();
        //        calEmpOff.SpecialDays.Clear();

        //        RadDate RadDt = new RadDate();
        //        DateTime dt = new DateTime();
        //        RadDate[] dates = new RadDate[dsOffDays.Tables[0].Rows.Count];
        //        RadCalendarDay[] rcd = new RadCalendarDay[dsOffDays.Tables[0].Rows.Count];
        //        int i = 0;

        //        try
        //        {
        //            foreach (DataRow dr in dsOffDays.Tables[0].Rows)
        //            {
        //                dt = Convert.ToDateTime(dr["off_date"].ToString(), format);
        //                dates[i] = new RadDate(dt);
        //                rcd[i] = new RadCalendarDay();
        //                rcd[i].Date = dt;
        //                rcd[i].IsSelectable = true;
        //                rcd[i].IsDisabled = true;
        //                //rcd[i].ItemStyle.BackColor = System.Drawing.Color.Gray;
        //                rcd[i].ItemStyle.CssClass = "SelectedDaysCss";
        //                i += 1;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Response.Write("File Creation failed. Reason is as follows : " + i.ToString());
        //        }

        //        calEmpOff.SelectedDates.AddRange(dates);
        //        //calEmpOff.SpecialDays.AddRange(rcd);
        //    }
        //    catch (Exception ex) { }
        //}
        //private void get_SelectedDates()
        //{
        //    try
        //    {
        //        DataTable dtSelectedDates = new DataTable();
        //        DataRow dr;
        //        dtSelectedDates.Columns.Add("Dates");
        //        for (int i = 0; i < calEmpOff.SelectedDates.Count; i++)
        //        {
        //            dr = dtSelectedDates.NewRow();
        //            dr[0] = calEmpOff.SelectedDates[i].Date.ToString();
        //            dtSelectedDates.Rows.Add(dr);
        //        }

        //        Session["dtSelectedDates"] = dtSelectedDates;
        //    }
        //    catch (Exception ex) { }
        //}

        //protected void NavigationChanged(object sender, DefaultViewChangedEventArgs e)
        //{
        //    try
        //    {
        //        startDate = ((MonthView)calEmpOff.CalendarView).MonthStartDate;
        //        endDate = ((MonthView)calEmpOff.CalendarView).MonthEndDate;
        //        Session["startDate"] = startDate;
        //        Session["endDate"] = endDate;
        //        Load_Public_Holiday();
        //        Load_Off_Days();
        //    }
        //    catch (Exception ex) { }
        //}
        // ---------------------- End Adding --------------------------
        protected void Load_Public_Holiday()//added by Sandi on 3/8/2014
        {
            try
            {
                compid = Utility.ToInteger(Session["Compid"]);
                DataSet dsHoliday = new DataSet();
                string strSQL = "";
                strSQL = "select * from public_holidays where (companyid=-1 or companyid=" + compid + ")" ;
                
                //and convert(datetime,holiday_date,103) between convert(datetime,'" + startDate + "',103) and convert(datetime,'" + endDate + "',103)";

                dsHoliday = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                Session["dtHoliday"] = dsHoliday.Tables[0];

                for (int i = 0; i < dsHoliday.Tables[0].Rows.Count; i++)
                {
                    DateTime dt = Convert.ToDateTime(dsHoliday.Tables[0].Rows[i]["holiday_date"].ToString());
                    list_holiday.Add(dt);
                }
            }
            catch (Exception ex) { }
        }

        IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

        protected List<DateTime> Load_Off_Days_FullandHalf(string R)//added by Sandi on 3/8/2014
        {
            List<DateTime> list_offday = new List<DateTime>();
            try
            {               

                EmpCode = Session["EmpCode"].ToString();
                DateTime dt = new DateTime();
                DataSet dsOffDays = new DataSet();
                string strSQL = "";
                strSQL = "select off_date,remark from emp_calender where emp_code=" + emp_code + " and remark='" + R + "'";

                    //+ "' and convert(datetime,off_date,103) between convert(datetime,'" + DateTime.Today.Date + "',103) 
                    //+ " and convert(datetime,'" + (DateTime.Today.Date+30) + "',103)";

                dsOffDays = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                int i = 0;

                try
                {
                    foreach (DataRow dr in dsOffDays.Tables[0].Rows)
                    {
                        dt = Convert.ToDateTime(dr["off_date"].ToString(), format);
                        list_offday.Add(dt);
                        i += 1;
                    }                    
                }
                catch (Exception e)
                {
                    Response.Write("File Creation failed. Reason is as follows : " + i.ToString());
                }               
            }
            catch (Exception ex) { }

            return list_offday;
        }
        
        private DateTime GetFirstDayOfMonth(int iMonth) //added by Sandi on 8/3/2014
        {
            // set return value to the last day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtFrom = new DateTime(DateTime.Now.Year, iMonth, 1);

            // remove all of the days in the month
            // except the first day and set the
            // variable to hold that date
            dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1));

            // return the first day of the month
            return dtFrom;
        }
        private DateTime GetLastDayOfMonth(int iMonth) //added by Sandi on 3/8/2014
        {
            // set return value to the last day of the month
            // for any date passed in to the method

            // create a datetime variable set to the passed in date
            DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);

            // overshoot the date by a month
            dtTo = dtTo.AddMonths(1);

            // remove all of the days in the next month
            // to get bumped down to the last day of the 
            // previous month
            dtTo = dtTo.AddDays(-(dtTo.Day));

            // return the last day of the month
            return dtTo;
        }
        private bool ValidatePayrollSubmitted() //added by Sandi on 3/14/2014
        {
            bool check = false;
            try
            {                
                string strSQL = "";
                EmpCode = Session["EmpCode"].ToString();
                DataSet dsPayroll = new DataSet();

                DateTime stDate = Convert.ToDateTime(GetFirstDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());
                DateTime enDate = Convert.ToDateTime(GetLastDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());

                strSQL = "select [status],* from prepare_payroll_detail where emp_id= " + emp_code + " and trx_id in(select MAX(trx_id) from prepare_payroll_hdr where created_by= " + emp_code + " and Convert(datetime,start_period,103) between Convert(datetime,'" + stDate + "',103) and Convert(datetime,'" + enDate + "',103))";

                dsPayroll = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                if (dsPayroll.Tables[0].Rows.Count == 0)
                {
                    check = true;
                }
                {
                    for (int i = 0; i < dsPayroll.Tables[0].Rows.Count; i++)
                    {
                        string pStatus = dsPayroll.Tables[0].Rows[i]["status"].ToString();

                        if (pStatus == "P" || pStatus == "A" || pStatus == "G")
                        {
                            check= false;
                        }
                        else if (pStatus == "R")
                        {
                            check= true;
                        }
                    }
                }
            }
            catch (Exception ex) { } 
            return check;                       
        }
        protected void btnClearOff_Click(object sender, System.EventArgs e)//added by Sandi on 3/8/2014
        {
            try
            {
                string strSQL = "";
                EmpCode = Session["EmpCode"].ToString();

                DateTime stDate = Convert.ToDateTime(GetFirstDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());
                DateTime enDate = Convert.ToDateTime(GetLastDayOfMonth(Convert.ToInt32(click_calender.VisibleDate.Month.ToString())).ToShortDateString());

                strSQL = "DELETE FROM emp_calender where emp_code=" + emp_code+" and Convert(datetime,off_date,103) between Convert(datetime,'" + stDate + "',103) and Convert(datetime,'" + enDate + "',103)";
                DataAccess.ExecuteNonQuery(strSQL, null);

                list_off = Load_Off_Days_FullandHalf("H");
                list_full = Load_Off_Days_FullandHalf("F");
            }
            catch (Exception ex) { }
            ShowMessageBox("Successfully clear off day from database for this month");
        }
        protected void btnsavecal_Click(object sender, System.EventArgs e)//added by Sandi on 3/8/2014
        {
            List<DateTime> listoffdays = (List<DateTime>)Session["list_offdays"];
            List<DateTime> listfulldays = (List<DateTime>)Session["list_fulldays"];
            try
            {      
                string strSQL = "";
                EmpCode = Session["EmpCode"].ToString();
               
                try
                {
                    strSQL = "DELETE FROM emp_calender where emp_code=" + emp_code;
                        
                    DataAccess.ExecuteNonQuery(strSQL, null);
                
                    foreach (DateTime dtHOff in listoffdays)
                    {
                        strSQL = "Insert into emp_calender values(" + emp_code + ", Convert(datetime,'" + dtHOff + "',103), 'H');";
                        DataAccess.ExecuteNonQuery(strSQL, null);
                    }
                    foreach (DateTime dtFull in listfulldays)
                    {
                        strSQL = "Insert into emp_calender values(" + emp_code + ",Convert(datetime,'" + dtFull + "',103), 'F');";
                        DataAccess.ExecuteNonQuery(strSQL, null);
                    }
                }
                catch (Exception ex) { }
                           
                ShowMessageBox("Successfully save in database");
            }
            catch (Exception ex) { }
        }
        #endregion

        void cmbPayMode_ServerChange(object sender, EventArgs e)
        {
            string sqlCurr = "Select CurrencyId from girobanks Where id=" + cmbPayMode.Value;

            SqlDataReader sqldr;
            sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlCurr, null);

            int curr = 0;

            while (sqldr.Read())
            {
                if (sqldr.GetValue(0) != null)
                {
                    curr = Convert.ToInt32(sqldr.GetValue(0).ToString());
                }
            }
            if (curr == 0)
            {
                drpCurrBank1.SelectedIndex = 0;
            }
            else
            {
                drpCurrBank1.SelectedValue = curr.ToString();
            }
        }
        void radListPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radListPayType.SelectedValue == "0")
            {
                txtSBperct.Disabled = false;
                drpCurrBank1.Enabled = false;
                drpCurrBank2.Enabled = false;
                drpPaymentMode1.Enabled = false;
                drpPaymentMode.Enabled = false;
            }
            else
            {
                txtSBperct.Disabled = true;
                drpCurrBank1.Enabled = true;
                drpCurrBank2.Enabled = true;
                drpPaymentMode1.Enabled = true;
                drpPaymentMode.Enabled = true;
                lblPerc.Text = "";
                lblPercSB2.Text = "";
            }
        }
        void Page_Unload(object sender, EventArgs e)
        {
            //Session["TCID"] = null;
        }

        private void LoadBankGrid()
        {
            if (emp_code.ToString().Length > 0)
            {
                DataSet ds = new DataSet();
                string str = " Select E.ID,B1.[Desc] FromBank,G1.Bank_AccountNo FromBankAcNo, B2.[Desc] ToBank, E.Giro_Acct_Number,E.Giro_Branch,E.Giro_Acc_Name,E.Percentage,E.Remarks  From EmployeeBankInfo E Inner Join GiroBanks G1 On E.Payment_From = G1.ID Inner Join Bank B1 On B1.ID = G1.Bank_ID Inner Join Bank B2 On E.Giro_Bank_ID = B2.ID Where E.Emp_ID=" + emp_code;
                str = str + "; Select Emp_ID, (Sum(Percentage)) as Percentage From EmployeeBankInfo E  Where Emp_ID=" + emp_code + " Group By Emp_ID";
                ds = getDataSet(str);
                RadGrid10.DataSource = ds;
                RadGrid10.MasterTableView.DataSource = ds;
                RadGrid10.DataBind();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    //ds.Tables[1].Rows[0]["Percent"].ToString()
                    lblPerc.Text = Convert.ToString((100 - Convert.ToDouble(ds.Tables[1].Rows[0]["Percentage"].ToString())));
                    lblPercSB2.Text = Convert.ToString(Convert.ToDouble(ds.Tables[1].Rows[0]["Percentage"].ToString()));
                    txtPerceSB.Value = Convert.ToString(ds.Tables[1].Rows[0]["Percentage"].ToString());
                }
                else
                {
                    lblPerc.Text = "100";
                    lblPercSB2.Text = "0";
                    txtPerceSB.Value = "0";
                }
            }
        }

        //new
        private int NumberDecimalPlaces(decimal dec)
        {
            string testdec = Convert.ToString(dec);
            int s = (testdec.IndexOf(".") + 1); // the first numbers plus decimal point
            return ((testdec.Length) - s);     //total length minus beginning numbers and decimal = number of decimal points
        }

       

      
      
        private void LoadFormValues()
        {

            int returnval;

            int j = 0;
            SqlParameter[] param = new SqlParameter[2];
            param[j++] = new SqlParameter("@CompanyID", Utility.ToInteger(compid));
            param[j++] = new SqlParameter("@EmployeeID", Utility.ToString(emp_code));
            int retVal = 0;
            string sSQL1 = "sp_CheckSuperAdminCount";


            //int returnval = Convert.ToInt32(DataAccess.ExecuteSPScalar(sSQL1, param));
            // if (!string.IsNullOrEmpty(DataAccess.ExecuteSPScalar(sSQL1, param)))
            if (DataAccess.ExecuteSPScalar(sSQL1, param) != "")
            {
                returnval = Convert.ToInt32(DataAccess.ExecuteSPScalar(sSQL1, param));
            }
            else
            {
                returnval = 0;
            }



            btnBulkInsert.Visible = false;
            RadGrid7.Visible = false;
            RadGrid8.Visible = false;
            if (Request.QueryString["empcode"] != null)
            {
                emp_code = Request.QueryString["empcode"];
                txtEmpCode.Value = compcode + emp_code;
                txtEmpCodeHdn.Value = emp_code;

                LoadBankGrid();
            }
            else
            {
                txtEmpCode.Value = compcode + " *** ";
                //txtEmpCodeHdn.Value = txtEmpCode.Value;
                txtEmpCodeHdn.Value = "0";
                txtEmpCode.Disabled = true;
            }

            if (txtEmpCode.Value.Contains("*") == false)
            {
               
                RadGrid4.DataSourceID = "SqlDataSource3";
                RadGrid5.DataSourceID = "SqlDataSource4";
                //RadGrid6.DataSourceID = "SqlDataSource6";
                RadGrid1.DataSourceID = "SqlDataSource10";
                RadGrid2.DataSourceID = "SqlDataSource10";
                RadGrid4.MasterTableView.DataSourceID = "SqlDataSource3";
                RadGrid5.MasterTableView.DataSourceID = "SqlDataSource4";
                //RadGrid6.MasterTableView.DataSourceID = "SqlDataSource6";
                RadGrid1.MasterTableView.DataSourceID = "SqlDataSource10";
                RadGrid2.MasterTableView.DataSourceID = "SqlDataSource12";
                this.tbsEmpPayHistory.Enabled = true;
                btnAddHistory.Visible = true;
                RadGrid6.Visible = true;
                if (bViewSalAllowed == false)
                {
                    this.tbsEmpPayHistory.Enabled = false;
                    btnAddHistory.Visible = false;
                    RadGrid6.Visible = false;
                }
                else
                {
                    dsnew = new DataSet();
                    string strSQL = "Select Ep.HistoryDetails ,c.Currency,EH.ID, EH.Emp_ID, SUBSTRING(CONVERT(VARCHAR(11), EH.FromDate, 103), 4, 8) FromDate, EH.ToDate, EH.ConfirmationDate, EH.DepartmentID, EH.DesignationID, OT_Entitlement=Case When EH.OT_Entitlement='Y' Then 'Yes' Else 'No' End,CPF_Entitlement=Case When EH.CPF_Entitlement='Y' Then 'Yes' Else 'No' End,EH.OT1Rate, EH.OT2Rate, Pay_Frequency=Case When EH.Pay_Frequency='M' Then 'Monthly' Else 'Hourly' End,  EH.WDays_Per_Week, convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), EH.payrate)) as payrate,  Hourly_Rate_Mode=Case When EH.Hourly_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Hourly_Rate,  Daily_Rate_Mode=Case When EH.Daily_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Daily_Rate ,CONVERT(VARCHAR, FromDate, 106)FromDateCopy,  Convert(varchar,ToDate,106) ToDateCopy,  Convert(varchar,ConfirmationDate,103) ConfirmationDateCopy,  DE.Designation, DP.DeptName From EmployeePayHistory EH Left Outer Join Designation DE On EH.DesignationID = DE.ID Left Outer Join Department  DP On EH.DepartmentID=DP.ID Left Outer Join Currency C on  C.id =EH.CurrencyID  Left Outer Join EmpHistoryRecordMaster Ep  On Ep.id=EH.PayHistoryID Where EH.Emp_ID= " + txtEmpCodeHdn.Value.ToString() + " And EH.SoftDelete=0 Order By EH.CreatedDate+EH.FromDate Desc";
                    dsnew = getDataSet(strSQL);
                    RadGrid6.DataSource = dsnew.Tables[0];
                    RadGrid6.DataBind();
                }
                AddEmployee_DataBinding();
                s = 1;
                DataSet EmpSet = new DataSet();
                DataSet EmpHis = new DataSet();
                DataSet EmpRos = new DataSet();


                string Str = "select PaymentPart,PaymentType,CurrencyBank,emp_code,emp_name,emp_lname,empcpftype,emp_alias,emp_clsupervisor,emp_type,ic_pp_number,wp_exp_date,pr_date,e.address,pay_frequency,localaddress2,foreignaddress1,foreignaddress2,foreignpostalcode,pp_issue_date,time_card_no,";
                Str += "convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,e.postal_code,e.phone,hand_phone,e.email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,";
                Str += "joining_date,Leave_supervisor,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,";
                Str += "mbmf_fund,groupid,sinda_fund,ecf_fund,cchest_fund,e.email_payslip,wh_tax_pct,wh_tax_amt,remarks,'../' + 'Documents' + '/' + Convert( varchar(50),e.company_id) + '/' + Convert(varchar(50),emp_code) + '/' + 'Picture' + '/' + images as images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, ";
                Str += "'EmpCPF' ,worker_levy,wp_application_date,";
                Str += "'EmployerCPF',ScanCode ";
                /* GENERAL CLIENTS */
                Str += " ,leave_carry_forward,giro_acc_name,(select sum(isnull(leave_remaining,0)) from leaves_annual where leave_year <= year(getdate()) and emp_id = e.emp_code) leaves_remaining,hourly_rate,hourly_rate_mode,daily_rate_mode,daily_rate,wdays_per_week,emp_ref_no,fund_optout,emp_category,v1rate,v2rate,v3rate,v4rate,batch_no,wp_issue_date,photo_no,wp_number,shipyard_quota,block_no,street_name,level_no,unit_no,ot1rate,ot2rate,eme_cont_per,eme_cont_per_rel,eme_cont_per_ph1,eme_cont_per_ph2,eme_cont_per_add,eme_cont_per_rem,bloodgroup,agent_id,mye_cert_id,wp_arrival_date,pay_supervisor, ";
                //Str += " Leave_Model_Txt = Case  When  c.leave_model =1 Then 'Fixed Yearly-Normal' When  c.leave_model =7 Then 'Fixed Yearly-Prorated' When  c.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  c.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  c.leave_model =3 Then 'Year of Service-Normal' When  c.leave_model =8 Then 'Year of Service-Prorated' When  c.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  c.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END, ";
                Str += " Leave_Model_Txt = Case  When  c.leave_model =1 Then 'Fixed Yearly-Normal' When  c.leave_model =7 Then 'Fixed Yearly-Prorated' When  c.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  c.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  c.leave_model =3 Then 'Year of Service-Normal' When  c.leave_model =8 Then 'Year of Service-Prorated' When  c.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  c.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' When  c.leave_model =9 Then 'Hybrid' END, ";
                Str += " c.Leave_Model,CONVERT(VARCHAR(11) ,e.joining_date, 106) joining_date_txt,c.mobileTimeSheet, CONVERT(VARCHAR(11), e.confirmation_date, 106) confirmation_date_txt,trade_id,e.payrollType,e.ComputeCPFFH,e.timesupervisor,e.ComputeFundFH,e.HalfSalary,e.CustomizedCal from employee e ";
                Str += " Left Outer Join Company c on e.company_id=c.company_id where e.emp_code ='" + emp_code + "'";

                EmpSet = getDataSet(Str);

                EmpHis = getDataSet("Select Top 1 *,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrateCopy From EmployeePayHistory Where Emp_ID=" + emp_code + " Order By CreatedDate Desc");

                EmpRos = getDataSet("Select R.Roster_Name From EmployeeAssignedToRoster E Inner Join Roster R On E.Roster_ID = R.ID Where Emp_ID=" + emp_code);

                drpYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;

                if (EmpRos.Tables[0].Rows.Count > 0)
                {
                    txtRosterAssigned.Text = EmpRos.Tables[0].Rows[0]["Roster_Name"].ToString();
                }

                //Disable all history on Edit.
                cmbDesignation.Disabled = true;
                cmbTrade.Disabled = true;
                cmbDepartment.Disabled = true;
                cmbOTEntitled.Disabled = true;
                cmbCPFEntitlement.Disabled = true;
                rdoMannualDailyRate.Disabled = true;
                rdoManualHourlyRate.Disabled = true;
                rdoMOMDailyRate.Disabled = true;
                rdoMOMHourlyRate.Disabled = true;
                txtOT1Rate.ReadOnly = true;
                txtOT2Rate.ReadOnly = true;
                cmbworkingdays.Disabled = true;
                txtHourlyRate.Disabled = true;
                txtDailyRate.Disabled = true;
                txtMannualDailyRate.Disabled = true;
                txtMannualHourlyRate.Disabled = true;
                txtPayRate.ReadOnly = true;
                rbpayfrequency.Enabled = false;

                if (!string.IsNullOrEmpty(EmpSet.Tables[0].Rows[0]["mobileTimeSheet"].ToString()))
                {

                    if (EmpSet.Tables[0].Rows[0]["mobileTimeSheet"].ToString() == "True")
                    {
                        Session["mobilescancode"]= "True";
                    }
                }



                if (EmpSet.Tables[0].Rows[0]["payrollType"].ToString() == "")
                {
                    ddlPayrollType.Items.FindItemByValue("1").Selected = true;
                }
                else
                {
                    ddlPayrollType.Items.FindItemByValue(EmpSet.Tables[0].Rows[0]["payrollType"].ToString()).Selected = true;
                }

                if (drpYear.SelectedItem.Value != "0")
                {
                    //if (Session["Leave_Model"].ToString() == "1")
                    //{
                    BindleavepolicyNormal();
                    //}
                }

                if (EmpHis.Tables[0].Rows.Count > 0)
                {
                    if (Utility.ToString(EmpHis.Tables[0].Rows[0]["FromDate"]) != "")
                    {
                        rdFrom.DbSelectedDate = Utility.ToString(EmpHis.Tables[0].Rows[0]["FromDate"]);
                    }
                    //if (Utility.ToString(EmpHis.Tables[0].Rows[0]["ToDate"]) != "")
                    //{
                    //    rdEnd.DbSelectedDate = Utility.ToString(EmpHis.Tables[0].Rows[0]["ToDate"]);
                    //}
                    //if (Utility.ToString(EmpHis.Tables[0].Rows[0]["ConfirmationDate"]) != "")
                    //{
                    //    rdConfirmation.DbSelectedDate = Utility.ToString(EmpHis.Tables[0].Rows[0]["ConfirmationDate"]);
                    //}
                    drpPayFrequency.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["Pay_Frequency"]);
                    drpDepartment.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["DepartMentID"]);
                    drpDesignation.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["DesignationID"]);
                    drpOTEntitled.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["OT_entitlement"]);
                    drpCPFEntitlement.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["CPF_entitlement"]);
                    drpHourlyMode.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["hourly_rate_mode"]);
                    drpDailyMode.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["daily_rate_mode"]);
                    custxtOT1Rate.Text = Utility.ToString(EmpHis.Tables[0].Rows[0]["OT1Rate"]);
                    if (EmpSet.Tables[0].Rows[0]["PaymentPart"] != DBNull.Value)
                    {
                        drpPaymentMode.SelectedValue = EmpSet.Tables[0].Rows[0]["PaymentPart"].ToString();
                    }
                    else
                    {
                        drpPaymentMode.SelectedIndex = 0;
                    }
                    if (EmpSet.Tables[0].Rows[0]["PaymentType"] != DBNull.Value)
                    {
                        if (EmpSet.Tables[0].Rows[0]["PaymentType"].ToString() == "0")
                        {
                            radListPayType.SelectedValue = "0";
                            txtSBperct.Disabled = false;

                            txtSBperct.Disabled = false;
                            drpCurrBank1.Enabled = false;
                            drpCurrBank2.Enabled = false;
                            drpPaymentMode1.Enabled = false;
                            drpPaymentMode.Enabled = false;

                        }
                        else
                        {
                            radListPayType.SelectedValue = "1";
                            txtSBperct.Disabled = true;

                            txtSBperct.Disabled = true;
                            drpCurrBank1.Enabled = true;
                            drpCurrBank2.Enabled = true;
                            drpPaymentMode1.Enabled = true;
                            drpPaymentMode.Enabled = true;
                            lblPerc.Text = "";
                            lblPercSB2.Text = "";


                        }
                    }
                    else
                    {
                        radListPayType.SelectedValue = "0";
                        txtSBperct.Disabled = false;

                        txtSBperct.Disabled = false;
                        drpCurrBank1.Enabled = false;
                        drpCurrBank2.Enabled = false;
                        drpPaymentMode1.Enabled = false;
                        drpPaymentMode.Enabled = false;
                    }
                    if (EmpSet.Tables[0].Rows[0]["CurrencyBank"] != DBNull.Value)
                    {
                        //if (EmpSet.Tables[0].Rows[0]["CurrencyBank"].ToString() == "0")
                        //{
                        drpCurrBank1.SelectedValue = EmpSet.Tables[0].Rows[0]["CurrencyBank"].ToString();
                        //}
                        //else
                        // {
                        //     radListPayType.SelectedValue = "1";
                        // }
                    }
                    else
                    {
                        //radListPayType.SelectedValue = "0";
                        drpCurrBank1.SelectedIndex = 0;

                    }

                    //new
                    //OT1
                    decimal OT1value;
                    if ((EmpHis.Tables[0].Rows[0]["hourly_rate"].ToString() != "") && EmpHis.Tables[0].Rows[0]["OT1Rate"].ToString() != "")
                    {
                        OT1value = (Convert.ToDecimal(EmpHis.Tables[0].Rows[0]["hourly_rate"]) * Convert.ToDecimal(EmpHis.Tables[0].Rows[0]["OT1Rate"])) + .01m;



                        decimal TempOT1 = Convert.ToDecimal(OT1value.ToString("#.##"));
                        TempOT1 = TempOT1 - Math.Floor(TempOT1);
                        if (TempOT1 == .01M)
                        {
                            txtManualOT1.Text = Convert.ToString(Math.Floor(OT1value));
                        }
                        else
                        {
                            OT1value = OT1value - .01m;
                            txtManualOT1.Text = OT1value.ToString("#.##");
                        }
                    }

                    //OT2
                    decimal OT2value;
                    if ((EmpHis.Tables[0].Rows[0]["hourly_rate"].ToString() != "") && EmpHis.Tables[0].Rows[0]["OT2Rate"].ToString() != "")
                    {
                        OT2value = (Convert.ToDecimal(EmpHis.Tables[0].Rows[0]["hourly_rate"]) * Convert.ToDecimal(EmpHis.Tables[0].Rows[0]["OT2Rate"])) + .01m;


                        decimal TempOT2 = Convert.ToDecimal(OT2value.ToString("#.##"));
                        TempOT2 = TempOT2 - Math.Floor(TempOT2);
                        if (TempOT2 == .01M)
                        {
                            txtManualOT2.Text = Convert.ToString(Math.Floor(OT2value));
                        }
                        else
                        {
                            OT2value = OT2value - .01m;
                            txtManualOT2.Text = OT2value.ToString("#.##");
                        }
                    }
                    //

                    custxtOT2Rate.Text = Utility.ToString(EmpHis.Tables[0].Rows[0]["OT2Rate"]);
                    drpworkingdays.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["wdays_per_week"]);
                    txtHiddenPayRate.Value = Utility.ToString(EmpHis.Tables[0].Rows[0]["payrateCopy"]);
                    if (bViewSalAllowed)
                    {
                        custxtPayRate.Text = Utility.ToString(EmpHis.Tables[0].Rows[0]["payrateCopy"]);
                    }
                    custxtHourlyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["hourly_rate"]);
                    custxtDailyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["daily_rate"]);
                }
                if (EmpSet.Tables[0].Rows[0]["leaves_remaining"] != DBNull.Value)
                {
                    txtRemainingLeaves.Disabled = true;
                }
                if (EmpSet.Tables[0].Rows[0]["joining_date_txt"] != DBNull.Value)
                {
                    lblJoining.Text = EmpSet.Tables[0].Rows[0]["joining_date_txt"].ToString();
                }
                if (EmpSet.Tables[0].Rows[0]["confirmation_date_txt"] != DBNull.Value)
                {
                    lblConfirm.Text = EmpSet.Tables[0].Rows[0]["confirmation_date_txt"].ToString();
                }
                if (EmpSet.Tables[0].Rows[0]["wdays_per_week"] != DBNull.Value)
                {
                    lblWorkdays.Text = EmpSet.Tables[0].Rows[0]["wdays_per_week"].ToString();
                }
                txtEmpCode.Disabled = true;
                txtLeaveModel.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["Leave_Model"]);
                txtRemainingLeaves.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["leaves_remaining"]);
                txthdnleaves.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["leaves_remaining"]);
                txtEmpName.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_name"]);
                txtlastname.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_lname"]);
                txtEmpAlias.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_alias"]);
                this.ScanCode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["ScanCode"]);
                txtinsurance.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["Insurance_number"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["insurance_expiry"]) != "")
                    rdinsurance.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["insurance_expiry"]);
                txtcsoc.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["CSOC_number"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["CSOC_expiry"]) != "")
                    rdcsoc.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["CSOC_expiry"]);
                txtpassport.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["passport"]);

                if (EmpSet.Tables[0].Rows[0]["passport_expiry"].ToString() != "")
                    rdpassport.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["passport_expiry"]);

                cmbempcpfgroup.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["empcpftype"]);
                cmbEmployerCPFAcctNumber.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["employer_cpf_acct"]);

                cmbNationality.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["nationality_id"]);
                cmbEmpType.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_type"]);
                txtICPPNumber.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["ic_pp_number"]);
                txtnricno.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["ic_pp_number"]);
                txt_Nric_H.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["ic_pp_number"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_exp_date"]) != "")
                    rdWPExpDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_exp_date"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["pr_date"]) != "")
                    rdPRDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["pr_date"]);
                txtAddress.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["address"]);
                cmbCountry.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["country_id"]);
                txtPostalCode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["postal_code"]);
                txtPhone.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["phone"]);
                txtHandPhone.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["hand_phone"]);
                txtEmail.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["email"]);

                cmbSex.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["sex"]);
                cmbReligion.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["religion_id"]);
                cmbRace.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["race_id"]);
                cmbMaritalStatus.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["marital_status"]);
                cmbbirthplace.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["place_of_birth"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["date_of_birth"]) != "")
                    rdDOB.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["date_of_birth"]);
                txtIncomeTaxID.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["income_taxid"]);
                txtEmployeeCPFAcctNumber.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["employee_cpf_acct"]);
                txtpassport.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["passport"]);
                txtgirobankname.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_bank"]);
                txtgirobranch.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_branch"]);
                txtGIROAccountNo.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_acct_number"]);
                cmbCPFEntitlement.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["cpf_entitlement"]);
                txtEmployerCPF.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["cpf_employer"]);
                txtEmployeeCPF.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["cpf_employee"]);
                cmbDepartment.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["dept_id"]);
                cmbDesignation.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["desig_id"]);
                cmbTrade.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["trade_id"]);
                cmbsupervisor.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_supervisor"]);
                cmbLeaveApproval.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["Leave_supervisor"]);

                //if (cmbLeaveApproval.Value!="-select-")
                //{
                //    cmbsupervisor.Value = "0";
                //}

                //if (cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text != "-select-")
                //{
                // supervisor = cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text;
                //}

                cmbclaimsupervisor.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_clsupervisor"]);
                cmbtimesheetsupervisor.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["timesupervisor"]);
                cmbpayrollsupervisor.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["pay_supervisor"]);
                cmbLeaveApproval.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["Leave_supervisor"]);
                cmbAgent.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["Agent_ID"]);
                cmbMYE.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["mye_cert_id"]);
                cmbeducation.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["education"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["joining_date"]) != "")
                {
                    rdJoiningDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["joining_date"]);
                    txtJoiningDt.Value = Convert.ToDateTime(EmpSet.Tables[0].Rows[0]["joining_date"].ToString()).ToString("dd/MM/yyyy");
                }

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["confirmation_date"]) != "")
                    rdConfirmationDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["confirmation_date"]);
                cmbEmployeeGroup.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_group_id"]);
                cmbOTEntitled.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["ot_entitlement"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["termination_date"]) != "")
                    rdTerminationDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["termination_date"]);
                txtterminreason.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["termination_reason"]);
                rbpayfrequency.SelectedValue = Utility.ToString(EmpSet.Tables[0].Rows[0]["pay_frequency"]);
                txtPayRate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["payrate"]);
                cmbPayMode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["payment_mode"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_bank"]) != "")
                {
                    cmbbranchcode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_bank"]);
                }

                string HourlyRateMode = Utility.ToString(EmpSet.Tables[0].Rows[0]["hourly_rate_mode"]);
                if (HourlyRateMode == "A")
                {
                    rdoMOMHourlyRate.Checked = true;
                    txtHourlyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["hourly_rate"]);
                }
                if (HourlyRateMode == "M")
                {
                    rdoManualHourlyRate.Checked = true;
                    txtMannualHourlyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["hourly_rate"]);
                }
                txtOT1Rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["ot1rate"]);
                txtOT2Rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["ot2rate"]);

                txtEmeConPer.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per"]);
                txtEmeConPerRel.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per_rel"]);
                txtEmeConPerPh1.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per_ph1"]);
                txtEmeConPerPh2.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per_ph2"]);
                txtEmeConPerAdd.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per_add"]);
                txtEmeConPerRem.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["eme_cont_per_rem"]);
                txtBloodGroup.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["bloodgroup"]);


                string DailyRateMode = Utility.ToString(EmpSet.Tables[0].Rows[0]["daily_rate_mode"]);
                if (DailyRateMode == "A")
                {
                    rdoMOMDailyRate.Checked = true;
                    txtDailyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["daily_rate"]);
                }
                if (DailyRateMode == "M")
                {
                    rdoMannualDailyRate.Checked = true;
                    txtMannualDailyRate.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["daily_rate"]);
                }
                cmbEmpCategory.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["emp_category"]);
                cmbworkingdays.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["wdays_per_week"]);

                //if (EmpSet.Tables[0].Rows[0]["CustomizedCal"].ToString() != "full" || EmpSet.Tables[0].Rows[0]["CustomizedCal"].ToString() != "")
                //{                   
                //    tbsempcalendar.Enabled = false;
                //}
                //else
                //{                  
                //    tbsempcalendar.Enabled = true;
                //}

                cmbEmailPaySlip.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["email_payslip"]);
                txtFWLCode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["fw_code"]);
                txtMonthlyLevy.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["fw_levy"]);
                txtv1rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["v1rate"]);
                txtv2rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["v2rate"]);
                txtv3rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["v3rate"]);
                txtv4rate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["v4rate"]);
                chkSDFRequired.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["sdf_required"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["sdf_required"]) == "1")
                    chkSDFRequired.Checked = true;
                else
                    chkSDFRequired.Checked = false;
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["cdac_fund"]) == "-1.0000")
                    txtCDAC.Value = "";
                else
                    txtCDAC.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["cdac_fund"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["mbmf_fund"]) == "-1.0000")
                    txtMBMF.Value = "";
                else
                    txtMBMF.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["mbmf_fund"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["sinda_fund"]) == "-1.0000")
                    txtSINDA.Value = "";
                else
                    txtSINDA.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["sinda_fund"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["ecf_fund"]) == "-1.0000")
                    txtECF.Value = "";
                else
                    txtECF.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["ecf_fund"]);

                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["cchest_fund"]) == "-1.0000")
                    txtCCHEST.Value = "";
                else
                    txtCCHEST.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["cchest_fund"]);

                if (Utility.ToInteger(EmpSet.Tables[0].Rows[0]["fund_optout"]) == 1)
                    chkoptfund.Checked = true;

                if (Utility.ToInteger(EmpSet.Tables[0].Rows[0]["ComputeCPFFH"]) == 1)
                    chkcomputecpffh.Checked = true;

                if (Utility.ToInteger(EmpSet.Tables[0].Rows[0]["ComputeFundFH"]) == 1)
                    chkFUNDRequired.Checked = true;

                //                if (Utility.ToInteger(EmpSet.Tables[0].Rows[0]["HalfSalary"]) == 1)
                //                    chkboxhalf.Checked = true;
                ddlHalfSal.SelectedValue = Utility.ToInteger(EmpSet.Tables[0].Rows[0]["HalfSalary"]).ToString();

                txtRemarks.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["remarks"]);
                Image1.Src = Utility.ToString(EmpSet.Tables[0].Rows[0]["images"]);
                if (Image1.Src.ToString() == "../Documents/" + compid.ToString() + "/" + emp_code + "/Picture/" || Image1.Src.ToString() == "../Documents/" + compid.ToString() + "/" + emp_code + "/Picture/employee.png" || Image1.Src.ToString() == "")
                {
                    string uploadpath = "../Frames/Images/Employee/employee.png";
                    Image1.Src = uploadpath;
                }
                txtPhotoExt_H.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["images"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["probation_period"]) == "")
                {
                    cmbprobation.SelectedValue = "-1";
                }
                else
                {
                    cmbprobation.SelectedValue = Utility.ToString(EmpSet.Tables[0].Rows[0]["probation_period"]);
                }
                txtgiroaccountname.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["giro_acc_name"]);

                tbsEmp.SelectedIndex = 0;
                tbsEmp.SelectedTab.Enabled = true;
                tbsEmp12.SelectedIndex = 0;
                cmbUserGroup.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["groupid"]);
                txtlocal2.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["localaddress2"]);
                txtblock.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["block_no"]);
                txtstreet.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["street_name"]);
                txtunit.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["unit_no"]);
                txtlevel.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["level_no"]);
                txtforeign1.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["foreignaddress1"]);
                txtforeign2.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["foreignaddress2"]);
                txttimecardno.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["time_card_no"]);
                Session["TCID"] = Utility.ToString(EmpSet.Tables[0].Rows[0]["time_card_no"]);
                txtforeignpostalcode.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["foreignpostalcode"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["pp_issue_date"]) != "")
                    rdppissuedate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["pp_issue_date"]);

                cmblevy.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["worker_levy"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_application_date"]) != "")
                    rdwpappdate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_application_date"]);

                if (EmpSet.Tables[0].Rows[0]["Leave_Model_Txt"] != DBNull.Value)
                {
                    lblLeaveModel.Text = EmpSet.Tables[0].Rows[0]["Leave_Model_Txt"].ToString();

                    if (txtLeaveModel.Value.ToString() == "3" || txtLeaveModel.Value.ToString() == "4" || txtLeaveModel.Value.ToString() == "6" || txtLeaveModel.Value.ToString() == "8")
                    {
                        RadGrid9.Visible = true;
                        btnYOSUpdate.Visible = true;
                    }
                    else
                    {
                        RadGrid9.Visible = false;
                        btnYOSUpdate.Visible = false;
                    }

                }

                ////ram(only for testing hybrid)
                //RadGrid9.Visible = true;
                //btnYOSUpdate.Visible = true;
                ////

                if (EmpSet.Tables[0].Rows[0]["emp_type"].ToString() == "SC" || EmpSet.Tables[0].Rows[0]["emp_type"].ToString() == "SPR")
                {
                    cmbEmpRefType.Value = "1";
                }
                else
                {
                    cmbEmpRefType.Value = "2";
                }

                if (bViewSalAllowed)
                {
                    txtPayRate.Text = Utility.ToString(EmpSet.Tables[0].Rows[0]["payrate"]);
                    if (txtPayRate.Text == "-999999999")
                        txtPayRate.Text = "";
                }
                else
                {
                    //SalaryPanel.Visible = false;
                }



                if (returnval > 1 || returnval < 0 )
                {
                    cmbUserGroup.Disabled = false;
                }
                else
                {
                    cmbUserGroup.Disabled = true;
                }

                /* USE BELOW SECTION FROM CLAVON */
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_issue_date"]) != "")
                    rdwpissuedate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_issue_date"]);
                if (Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_arrival_date"]) != "")
                    rdWPArrDate.DbSelectedDate = Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_arrival_date"]);
                txtWPNumber.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["wp_number"]);
                txtBatchNo.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["batch_no"]);
                txtPhotoNo.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["photo_no"]);
                txtShipyardQuota.Value = Utility.ToString(EmpSet.Tables[0].Rows[0]["shipyard_quota"]);

                if (emp_code.ToString().Length > 0)
                {
                    int stryear = DateTime.Now.Year - 1;
                    //cmbIR8A_year.Items.FindByText(stryear.ToString()).Selected = true;
                    DataSet ds_ir8a = new DataSet();
                    ds_ir8a = getDataSet("select *,CONVERT(VARCHAR(10), CONVERT(datetime,dateofcessation, 105), 103)  [dateofcessationconv],CONVERT(VARCHAR(10), CONVERT(datetime,dateofcommencement, 105), 103)  [dateofcommencementconv] from employee_ir8a where emp_id =" + emp_code + " and ir8a_year='" + cmbIR8A_year.Items[cmbIR8A_year.SelectedIndex].Text.ToString() + "'");
                    if (ds_ir8a.Tables[0].Rows.Count > 0)
                    {
                        object obj1_tax_borne_employer = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer"];
                        object obj2_tax_borne_employer_options = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"];
                        object obj3_tax_borne_employer_amount = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_amount"];
                        object obj4_pension_out_singapore = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore"];
                        object obj5_pension_out_singapore_amount = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore_amount"];
                        object obj6_excess_voluntary_cpf_employer = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer"];
                        object obj7_excess_voluntary_cpf_employer_amount = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer_amount"];
                        object obj8_stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"];
                        object obj9_stock_options_amount = ds_ir8a.Tables[0].Rows[0]["stock_options_amount"];
                        object obj10_benefits_in_kind = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"];
                        object obj11_benefits_in_kind_amount = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind_amount"];
                        object obj12_retirement_benefits = ds_ir8a.Tables[0].Rows[0]["retirement_benefits"];
                        object obj13_retirement_benefits_fundName = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_fundName"];
                        object obj14_retirement_benefits_amount = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_amount"];
                        object obj15_s45_tax_on_directorFee = ds_ir8a.Tables[0].Rows[0]["s45_tax_on_directorFee"];
                        object obj16_cessation_provision = ds_ir8a.Tables[0].Rows[0]["cessation_provision"];
                        object obj17_addr_type = ds_ir8a.Tables[0].Rows[0]["addr_type"];
                        object obj18_dateofcessationconv = ds_ir8a.Tables[0].Rows[0]["dateofcessationconv"];
                        object obj19_dateofcommencementconv = ds_ir8a.Tables[0].Rows[0]["dateofcommencementconv"];

                        if (obj1_tax_borne_employer != DBNull.Value)
                        {
                            cmbtaxbornbyemployer.Items.FindByValue(obj1_tax_borne_employer.ToString()).Selected = true;
                        }
                        if (obj2_tax_borne_employer_options != DBNull.Value)
                        {
                            cmbtaxbornbyemployerFPHN.Items.FindByValue(obj2_tax_borne_employer_options.ToString()).Selected = true;
                        }
                        if (obj3_tax_borne_employer_amount != DBNull.Value)
                        {
                            txttaxbornbyempamt.Value = obj3_tax_borne_employer_amount.ToString();
                        }
                        if (obj4_pension_out_singapore != DBNull.Value)
                        {
                            cmbpensionoutsing.Items.FindByText(obj4_pension_out_singapore.ToString()).Selected = true;
                        }
                        if (obj5_pension_out_singapore_amount != DBNull.Value)
                        {
                            txtpensionoutsing.Value = obj5_pension_out_singapore_amount.ToString();
                        }
                        if (obj6_excess_voluntary_cpf_employer != DBNull.Value)
                        {
                            cmbexcessvolcpfemp.Items.FindByText(obj6_excess_voluntary_cpf_employer.ToString()).Selected = true;
                        }
                        if (obj7_excess_voluntary_cpf_employer_amount != DBNull.Value)
                        {
                            txtexcessvolcpfemp.Value = obj7_excess_voluntary_cpf_employer_amount.ToString();
                        }
                        if (obj8_stock_options != DBNull.Value)
                        {
                            cmbstockoption.Items.FindByText(obj8_stock_options.ToString()).Selected = true;
                        }
                        if (obj9_stock_options_amount != DBNull.Value)
                        {
                            txtstockoption.Value = obj9_stock_options_amount.ToString();
                        }
                        if (obj10_benefits_in_kind != DBNull.Value)
                        {
                            cmbbenefitskind.Items.FindByText(obj10_benefits_in_kind.ToString()).Selected = true;
                        }
                        if (obj11_benefits_in_kind_amount != DBNull.Value)
                        {
                            txtbenefitskind.Value = obj11_benefits_in_kind_amount.ToString();
                        }
                        if (obj12_retirement_benefits != DBNull.Value)
                        {
                            cmbretireben.Items.FindByText(obj12_retirement_benefits.ToString()).Selected = true;
                        }
                        if (obj13_retirement_benefits_fundName != DBNull.Value)
                        {
                            txtretirebenfundname.Value = obj13_retirement_benefits_fundName.ToString();
                        }
                        if (obj14_retirement_benefits_amount != DBNull.Value)
                        {
                            txtbretireben.Value = obj14_retirement_benefits_amount.ToString();
                        }
                        if (obj15_s45_tax_on_directorFee != DBNull.Value)
                        {
                            staxondirector.Items.FindByText(obj15_s45_tax_on_directorFee.ToString()).Selected = true;
                        }
                        if (obj16_cessation_provision != DBNull.Value)
                        {
                            cmbcessprov.Items.FindByText(obj16_cessation_provision.ToString()).Selected = true;
                        }
                        if (obj17_addr_type != DBNull.Value)
                        {
                            if (cmbaddress.Items.Count > 0)
                            {
                                cmbaddress.Items.FindByValue(obj17_addr_type.ToString()).Selected = true;
                            }
                        }

                        if (obj18_dateofcessationconv != DBNull.Value)
                        {
                            dtcessdate.Value = obj18_dateofcessationconv.ToString();
                        }
                        if (obj19_dateofcommencementconv != DBNull.Value)
                        {
                            dtcommdate.Value = obj19_dateofcommencementconv.ToString();
                        }
                    }
                    else
                    {

                    }

                }

                lblEmp.Visible = true;
                lblEmpName.Visible = true;
                lblEmpName.Text = txtEmpName.Value + " " + txtlastname.Value;
            }
            else
            {
                if (Session["GroupName"].ToString().ToUpper() == "SUPER ADMIN")
                {
                    DataSet EmpBulk = new DataSet();
                    EmpBulk = getDataSet("Select Count(ID) From EmployeeBulkImport Where Deleted=3 And CompanyID=" + compid);

                    if (EmpBulk.Tables[0].Rows.Count > 0)
                    {
                        if (Utility.ToInteger(EmpBulk.Tables[0].Rows[0][0]) > 0)
                        {
                            btnBulkInsert.Visible = true;
                            btnBulkInsert.Text = "Import " + Utility.ToString(EmpBulk.Tables[0].Rows[0][0]) + " Employees";
                        }
                    }
                }

                tabPageLeaveHistory.Enabled = false;
                tabPageCertificate.Enabled = false;
                tbsItemIss.Enabled = false;
                tbsFileUpload.Enabled = false;
                tbsEmpPayHistory.Enabled = false;
                tabPageFamily.Enabled = false;
                tbsIR8A.Enabled = false;
                btnSB.Enabled = false;
                string uploadpath = "../Frames/Images/Employee/employee.png";
                Image1.Src = uploadpath;
                cmbworkingdays.Value = Session["WorkingDays"].ToString();
                AddEmployee_DataBinding();
                s = 0;
                tbsIR8A.Enabled = false;
            }
        }

        # endregion Datafill

        private void InitializeComponent()
        {
            this.PreRender += new System.EventHandler(this.AddEditEmployee_PreRender);
            this.Load += new System.EventHandler(this.Page_Load);

        }

        # region DataBinding
        private object _dataItem = null;

        public object Dataitem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }


        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        void AddEmployee_DataBinding()
        {
            /* Binding Country Drop down list*/
            DataSet ds_paysupervisor = new DataSet();
            ds_paysupervisor = getDataSet("Select WL.ID, WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName  From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN (Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=1) And Company_ID=" + compid + " ) WF Inner Join EmployeeWorkFlowLevel WL On WF.ID=WL.WorkFlowID Order By WF.WorkFlowName, WL.RowID");
            cmbpayrollsupervisor.DataSource = ds_paysupervisor.Tables[0];
            cmbpayrollsupervisor.DataTextField = ds_paysupervisor.Tables[0].Columns["WorkFlowName"].ColumnName.ToString();
            cmbpayrollsupervisor.DataValueField = ds_paysupervisor.Tables[0].Columns["ID"].ColumnName.ToString();
            cmbpayrollsupervisor.DataBind();
            cmbpayrollsupervisor.Items.Insert(0, "-select-");
            /* Binding Leaves Drop down list*/
            //cmbLeaveApproval

            ds_paysupervisor = getDataSet("Select WL.ID, WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName  From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN (Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) And Company_ID=" + compid + " ) WF Inner Join EmployeeWorkFlowLevel WL On WF.ID=WL.WorkFlowID Order By WF.WorkFlowName, WL.RowID");
            cmbLeaveApproval.DataSource = ds_paysupervisor.Tables[0];
            cmbLeaveApproval.DataTextField = ds_paysupervisor.Tables[0].Columns["WorkFlowName"].ColumnName.ToString();
            cmbLeaveApproval.DataValueField = ds_paysupervisor.Tables[0].Columns["ID"].ColumnName.ToString();
            cmbLeaveApproval.DataBind();
            cmbLeaveApproval.Items.Insert(0, "-select-");

            string paysupid = Utility.ToString(DataBinder.Eval(Dataitem, "ID"));
            if (paysupid != "")
                cmbpayrollsupervisor.Value = paysupid.ToString();

            /* Binding Country Drop down list*/
            DataSet ds_agent = new DataSet();
            ds_agent = getDataSet("select Id, Agent_Name from EmployeeAgent Where Company_ID=" + compid + " ORDER BY Agent_Name");
            cmbAgent.DataSource = ds_agent.Tables[0];
            cmbAgent.DataTextField = ds_agent.Tables[0].Columns["Agent_Name"].ColumnName.ToString();
            cmbAgent.DataValueField = ds_agent.Tables[0].Columns["ID"].ColumnName.ToString();
            cmbAgent.DataBind();
            cmbAgent.Items.Insert(0, "-select-");
            string agentid = Utility.ToString(DataBinder.Eval(Dataitem, "ID"));
            if (agentid != "")
                cmbAgent.Value = agentid.ToString();

            /* Binding Country Drop down list*/
            DataSet ds_mye = new DataSet();
            ds_mye = getDataSet("select Id, CertificateNo from MYECertificate Where Company_ID=" + compid + " ORDER BY CertificateNo");
            cmbMYE.DataSource = ds_mye.Tables[0];
            cmbMYE.DataTextField = ds_mye.Tables[0].Columns["CertificateNo"].ColumnName.ToString();
            cmbMYE.DataValueField = ds_mye.Tables[0].Columns["ID"].ColumnName.ToString();
            cmbMYE.DataBind();
            cmbMYE.Items.Insert(0, "-select-");
            string myeid = Utility.ToString(DataBinder.Eval(Dataitem, "ID"));
            if (myeid != "")
                cmbMYE.Value = myeid.ToString();


            DataSet ds_nationality = new DataSet();
            ds_nationality = getDataSet("select Id Nationality_ID ,Nationality from Nationality order by 2");
            cmbNationality.DataSource = ds_nationality.Tables[0];
            cmbNationality.DataTextField = ds_nationality.Tables[0].Columns["Nationality"].ColumnName.ToString();
            cmbNationality.DataValueField = ds_nationality.Tables[0].Columns["Nationality_ID"].ColumnName.ToString();
            cmbNationality.DataBind();
            cmbNationality.Items.Insert(0, "-select-");
            string nationalityValue = Utility.ToString(DataBinder.Eval(Dataitem, "Nationality_ID"));
            if (nationalityValue != "")
                cmbNationality.Value = nationalityValue.ToString();

            DataSet ds_country = new DataSet();
            //ds_country = getDataSet("select Id Country_ID ,Country from country order by 2");
            ds_country = getDataSet("select Id Country_ID ,Country from country");
            cmbCountry.DataSource = ds_country.Tables[0];
            cmbCountry.DataTextField = ds_country.Tables[0].Columns["Country"].ColumnName.ToString();
            cmbCountry.DataValueField = ds_country.Tables[0].Columns["Country_ID"].ColumnName.ToString();
            cmbCountry.DataBind();
            string countryValue = Utility.ToString(DataBinder.Eval(Dataitem, "Country_ID"));
            if (countryValue != "")
                cmbCountry.Value = countryValue.ToString();

            DataSet ds_usergroup = new DataSet();
            //ds_usergroup = getDataSet("select GroupId,GroupName from usergroups where company_id=" + compid + " ORDER BY GROUPNAME");
            ds_usergroup = getDataSet(" select GroupId,GroupName from usergroups where company_id=" + compid + " ORDER BY CASE WHEN GROUPNAME = 'Employee' THEN 'A' ELSE GROUPNAME  END");
           
            cmbUserGroup.DataSource = ds_usergroup.Tables[0];
            cmbUserGroup.DataTextField = ds_usergroup.Tables[0].Columns["GroupName"].ColumnName.ToString();
            cmbUserGroup.DataValueField = ds_usergroup.Tables[0].Columns["GroupId"].ColumnName.ToString();
            cmbUserGroup.DataBind();
            string userid = Utility.ToString(DataBinder.Eval(Dataitem, "GroupId"));
            if (userid != "")
                cmbUserGroup.Value = userid.ToString();


            DataSet ds_Religion = new DataSet();
            ds_Religion = getDataSet("select Id Religion_ID , Religion from religion ORDER BY RELIGION");
            cmbReligion.DataSource = ds_Religion.Tables[0];
            cmbReligion.DataTextField = ds_Religion.Tables[0].Columns["Religion"].ColumnName.ToString();
            cmbReligion.DataValueField = ds_Religion.Tables[0].Columns["Religion_ID"].ColumnName.ToString();
            cmbReligion.DataBind();
            cmbReligion.Items.Insert(0, "-select-");
            string religionValue = Utility.ToString(DataBinder.Eval(Dataitem, "Religion_ID"));
            if (religionValue != "")
            {
                cmbReligion.Value = religionValue.ToString();
            }

            DataSet ds_CPF = new DataSet();
            ds_CPF = getDataSet("select ID, CSN From CPFFiles where company_id=" + compid + " ORDER BY CSN");
            cmbEmployerCPFAcctNumber.DataSource = ds_CPF.Tables[0];
            cmbEmployerCPFAcctNumber.DataTextField = ds_CPF.Tables[0].Columns["CSN"].ColumnName.ToString();
            cmbEmployerCPFAcctNumber.DataValueField = ds_CPF.Tables[0].Columns["CSN"].ColumnName.ToString();
            cmbEmployerCPFAcctNumber.DataBind();
            cmbEmployerCPFAcctNumber.Items.Insert(0, "-select-");
            string cpfValue = Utility.ToString(DataBinder.Eval(Dataitem, "CSN"));
            if (cpfValue != "")
            {
                cmbEmployerCPFAcctNumber.Value = cpfValue.ToString();
            }


            DataSet ds_Race = new DataSet();
            ds_Race = getDataSet("select Id Race_ID , Race from race Order By Race");
            cmbRace.DataSource = ds_Race.Tables[0];
            cmbRace.DataTextField = ds_Race.Tables[0].Columns["Race"].ColumnName.ToString();
            cmbRace.DataValueField = ds_Race.Tables[0].Columns["Race_ID"].ColumnName.ToString();
            cmbRace.DataBind();
            cmbRace.Items.Insert(0, new ListItem("-select-", "-1"));
            string raceValue = Utility.ToString(DataBinder.Eval(Dataitem, "Race_ID"));
            if (raceValue != "")
            {
                cmbRace.Value = raceValue.ToString();
            }


            DataSet ds_department = new DataSet();
            ds_department = getDataSet("select id Dept_id , DeptName from department where Company_id=" + compid + " ORDER BY DeptName");
            cmbDepartment.DataSource = ds_department.Tables[0];
            cmbDepartment.DataTextField = ds_department.Tables[0].Columns["DeptName"].ColumnName.ToString();
            cmbDepartment.DataValueField = ds_department.Tables[0].Columns["Dept_id"].ColumnName.ToString();
            cmbDepartment.DataBind();
            cmbDepartment.Items.Insert(0, "-select-");
            string DeptValue = Utility.ToString(DataBinder.Eval(Dataitem, "Dept_id"));
            if (DeptValue != "")
            {
                cmbDepartment.Value = DeptValue.ToString();
            }
            drpDepartment.DataSource = ds_department.Tables[0];
            drpDepartment.DataTextField = ds_department.Tables[0].Columns["DeptName"].ColumnName.ToString();
            drpDepartment.DataValueField = ds_department.Tables[0].Columns["Dept_id"].ColumnName.ToString();
            drpDepartment.DataBind();
            drpDepartment.Items.Insert(0, "-select-");
            if (DeptValue != "")
            {
                drpDepartment.Value = DeptValue.ToString();
            }

            DataSet ds_paymode = new DataSet();
            ds_paymode = getDataSet("SELECT a.id, BANK_ID,B.[DESC],A.BANK_ACCOUNTNO,B.[DESC]+'-'+BANK_ACCOUNTNO 'BANK' FROM GIROBANKS A,BANK B WHERE  a.company_id=" + compid + " and A.BANK_ID=B.ID ORDER BY B.[desc]; select [desc], bank_code from bank where [desc]!='DBS DISKETTE'  Order By [desc];select [desc], id from bank Order By [desc]");
            cmbPayMode.DataSource = ds_paymode.Tables[0];
            cmbPayMode.DataTextField = ds_paymode.Tables[0].Columns["BANK"].ColumnName.ToString();
            cmbPayMode.DataValueField = ds_paymode.Tables[0].Columns["id"].ColumnName.ToString();
            cmbPayMode.EnableViewState = true;
            cmbPayMode.DataBind();
            cmbPayMode.Items.Add(new ListItem("Cash", "-1"));
            cmbPayMode.Items.Add(new ListItem("Cheque", "-2"));
            cmbPayMode.Items.Insert(0, "-select-");

            cmbSBPayMode.DataSource = ds_paymode.Tables[0];
            cmbSBPayMode.DataTextField = ds_paymode.Tables[0].Columns["BANK"].ColumnName.ToString();
            cmbSBPayMode.DataValueField = ds_paymode.Tables[0].Columns["id"].ColumnName.ToString();
            cmbSBPayMode.DataBind();
            cmbSBPayMode.Items.Insert(0, "-select-");

            cmbbranchcode.DataSource = ds_paymode.Tables[1];
            cmbbranchcode.DataTextField = ds_paymode.Tables[1].Columns["desc"].ColumnName.ToString();
            cmbbranchcode.DataValueField = ds_paymode.Tables[1].Columns["bank_code"].ColumnName.ToString();
            cmbbranchcode.DataBind();
            cmbbranchcode.Items.Insert(0, "-select-");

            cmbSBbranchcode.DataSource = ds_paymode.Tables[2];
            cmbSBbranchcode.DataTextField = ds_paymode.Tables[2].Columns["desc"].ColumnName.ToString();
            cmbSBbranchcode.DataValueField = ds_paymode.Tables[2].Columns["id"].ColumnName.ToString();
            cmbSBbranchcode.DataBind();
            cmbSBbranchcode.Items.Insert(0, "-select-");

            string payValue = Utility.ToString(DataBinder.Eval(Dataitem, "id"));
            if (payValue != "")
            {
                cmbPayMode.Value = payValue.ToString();
            }

            DataSet ds_birthplace = new DataSet();
            ds_birthplace = getDataSet("select Id Country_ID ,Country from country order by 2");
            cmbbirthplace.DataSource = ds_birthplace.Tables[0];
            cmbbirthplace.DataTextField = ds_birthplace.Tables[0].Columns["Country"].ColumnName.ToString();
            cmbbirthplace.DataValueField = ds_birthplace.Tables[0].Columns["Country"].ColumnName.ToString();
            cmbbirthplace.DataBind();
            cmbbirthplace.Items.Insert(0, new ListItem("-select-", ""));
            string placeValue = Utility.ToString(DataBinder.Eval(Dataitem, "Country"));
            if (placeValue != "")
                cmbbirthplace.Value = placeValue.ToString();

            DataSet ds_designation = new DataSet();
            ds_designation = getDataSet("select id Desig_id , Designation from designation where Company_id=" + compid + " Order By Designation");
            cmbDesignation.DataSource = ds_designation.Tables[0];
            cmbDesignation.DataTextField = ds_designation.Tables[0].Columns["Designation"].ColumnName.ToString();
            cmbDesignation.DataValueField = ds_designation.Tables[0].Columns["Desig_id"].ColumnName.ToString();
            cmbDesignation.DataBind();
            cmbDesignation.Items.Insert(0, "-select-");
            string DesigValue = Utility.ToString(DataBinder.Eval(Dataitem, "Desig_id"));
            if (DesigValue != "")
            {
                cmbDesignation.Value = DesigValue.ToString();
            }

            DataSet ds_trade = new DataSet();
            ds_trade = getDataSet("select id Trade_id , Trade from Trade where Company_id=" + compid + " Order By Trade");
            cmbTrade.DataSource = ds_trade.Tables[0];
            cmbTrade.DataTextField = ds_trade.Tables[0].Columns["Trade"].ColumnName.ToString();
            cmbTrade.DataValueField = ds_trade.Tables[0].Columns["Trade_id"].ColumnName.ToString();
            cmbTrade.DataBind();
            cmbTrade.Items.Insert(0, "-select-");
            string TradeValue = Utility.ToString(DataBinder.Eval(Dataitem, "Trade_id"));
            if (TradeValue != "")
            {
                cmbTrade.Value = TradeValue.ToString();
            }

            drpDesignation.DataSource = ds_designation.Tables[0];
            drpDesignation.DataTextField = ds_designation.Tables[0].Columns["Designation"].ColumnName.ToString();
            drpDesignation.DataValueField = ds_designation.Tables[0].Columns["Desig_id"].ColumnName.ToString();
            drpDesignation.DataBind();
            drpDesignation.Items.Insert(0, "-select-");
            if (DesigValue != "")
            {
                drpDesignation.Value = DesigValue.ToString();
            }

            drpTrade.DataSource = ds_trade.Tables[0];
            drpTrade.DataTextField = ds_trade.Tables[0].Columns["Trade"].ColumnName.ToString();
            drpTrade.DataValueField = ds_trade.Tables[0].Columns["Trade_id"].ColumnName.ToString();
            drpTrade.DataBind();
            drpTrade.Items.Insert(0, "-select-");
            if (TradeValue != "")
            {
                drpTrade.Value = TradeValue.ToString();
            }


            DataSet ds_empgroup = new DataSet();
            ds_empgroup = getDataSet("select id EmpGroup_id , EmpGroupName from emp_group where Company_id=" + compid + " Order By EmpGroupName");
            cmbEmployeeGroup.DataSource = ds_empgroup.Tables[0];
            cmbEmployeeGroup.DataTextField = ds_empgroup.Tables[0].Columns["EmpGroupName"].ColumnName.ToString();
            cmbEmployeeGroup.DataValueField = ds_empgroup.Tables[0].Columns["EmpGroup_id"].ColumnName.ToString();
            cmbEmployeeGroup.DataBind();
            string EmpGroupValue = Utility.ToString(DataBinder.Eval(Dataitem, "EmpGroup_id"));
            if (EmpGroupValue != "")
            {
                cmbEmployeeGroup.Value = EmpGroupValue.ToString();
            }

            DataSet ds_supervisor = new DataSet();
            string sSQL = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') emp_name from employee where company_id = {0} AND emp_name<>'' and termination_date is null ORDER BY emp_name ";
            sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
            ds_supervisor = getDataSet(sSQL);
            cmbsupervisor.DataSource = ds_supervisor.Tables[0];
            cmbsupervisor.DataTextField = ds_supervisor.Tables[0].Columns["emp_name"].ColumnName.ToString();
            cmbsupervisor.DataValueField = ds_supervisor.Tables[0].Columns["emp_code"].ColumnName.ToString();
            cmbsupervisor.DataBind();
            cmbsupervisor.Items.Insert(0, "");
            string supervisorid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
            if (supervisorid != "")
            {
                cmbsupervisor.Value = supervisorid.ToString();
            }
            //Claim Supervisor(Added By Raja 19/11/2008)
            DataSet ds_csupervisor = new DataSet();

            string sSQLclaim = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') emp_name from employee where company_id = {0} AND emp_name<>'' and termination_date is null ORDER BY emp_name ";
            sSQLclaim = string.Format(sSQLclaim, Utility.ToInteger(Session["Compid"]));
            ds_csupervisor = getDataSet(sSQLclaim);
            cmbclaimsupervisor.DataSource = ds_csupervisor.Tables[0];
            cmbclaimsupervisor.DataTextField = ds_csupervisor.Tables[0].Columns["emp_name"].ColumnName.ToString();
            cmbclaimsupervisor.DataValueField = ds_csupervisor.Tables[0].Columns["emp_code"].ColumnName.ToString();
            cmbclaimsupervisor.DataBind();
            cmbclaimsupervisor.Items.Insert(0, "");
            string csupervisorid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
            if (csupervisorid != "")
            {
                cmbclaimsupervisor.Value = csupervisorid.ToString();
            }

            DataSet ds_tsupervisor = new DataSet();

            string strSql = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') emp_name from employee where company_id = {0} AND emp_name<>'' and termination_date is null ORDER BY emp_name ";
            strSql = string.Format(strSql, Utility.ToInteger(Session["Compid"]));
            ds_tsupervisor = getDataSet(strSql);
            cmbtimesheetsupervisor.DataSource = ds_tsupervisor.Tables[0];
            cmbtimesheetsupervisor.DataTextField = ds_tsupervisor.Tables[0].Columns["emp_name"].ColumnName.ToString();
            cmbtimesheetsupervisor.DataValueField = ds_tsupervisor.Tables[0].Columns["emp_code"].ColumnName.ToString();
            cmbtimesheetsupervisor.DataBind();
            cmbtimesheetsupervisor.Items.Insert(0, "");
            string tsupervisorid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
            if (tsupervisorid != "")
            {
                cmbtimesheetsupervisor.Value = tsupervisorid.ToString();
            }


            #region Costing -dropdown

                #region Business unit dropdown
                    DataSet ds_businessunit = new DataSet();
                    string sSQL_businessunit = "select Bid,Businessunit from Cost_BusinessUnit where company_Id={0}";
                    sSQL_businessunit = string.Format(sSQL_businessunit, Utility.ToInteger(Session["Compid"]));
                    ds_businessunit = getDataSet(sSQL_businessunit);
                    cmbbusinessunit.DataSource = ds_businessunit.Tables[0];
                    cmbbusinessunit.DataTextField = ds_businessunit.Tables[0].Columns["Businessunit"].ColumnName.ToString();
                    cmbbusinessunit.DataValueField = ds_businessunit.Tables[0].Columns["Bid"].ColumnName.ToString();
                    cmbbusinessunit.DataBind();
                    cmbbusinessunit.Items.Insert(0, "");
                #endregion

                #region region dropdown
                    DataSet ds_region = new DataSet();
                    string sSQL_region = "select Bid,Businessunit from Cost_Region where company_Id={0}";
                    sSQL_region = string.Format(sSQL_region, Utility.ToInteger(Session["Compid"]));
                    ds_region = getDataSet(sSQL_region);
                    cmbRegion.DataSource = ds_region.Tables[0];
                    cmbRegion.DataTextField = ds_region.Tables[0].Columns["Businessunit"].ColumnName.ToString();
                    cmbRegion.DataValueField = ds_region.Tables[0].Columns["Bid"].ColumnName.ToString();
                    cmbRegion.DataBind();
                    cmbRegion.Items.Insert(0, "");
                #endregion

                #region region dropdown
                    DataSet ds_category = new DataSet();
                    string sSQL_category = "select Bid,Businessunit from Cost_Ccategory where company_Id={0}";
                    sSQL_category = string.Format(sSQL_category, Utility.ToInteger(Session["Compid"]));
                    ds_category = getDataSet(sSQL_category);
                    cmbCategory.DataSource = ds_category.Tables[0];
                    cmbCategory.DataTextField = ds_category.Tables[0].Columns["Businessunit"].ColumnName.ToString();
                    cmbCategory.DataValueField = ds_category.Tables[0].Columns["Bid"].ColumnName.ToString();
                    cmbCategory.DataBind();
                    cmbCategory.Items.Insert(0, "");
                #endregion

      
            #endregion

        }


        #endregion DataBinding

        #region RadUpload

        protected void buttonDelete_Click(object sender, ImageClickEventArgs e)
        {
            string targetFolder = Server.MapPath("~/Documents/" + compid.ToString() + "/" + emp_code + "/Picture/");
            DirectoryInfo targetDir = new DirectoryInfo(targetFolder);

            foreach (FileInfo file in targetDir.GetFiles())
            {
                if ((file.FullName == varFileName) && ((file.Attributes & FileAttributes.ReadOnly) == 0))
                {
                    file.Delete();
                }
            }
        }

        protected void ButtonAdd_Click(object sender, ImageClickEventArgs e)
        {
            RadUpload1.Visible = true;
        }

        #endregion RadUpload

        #region Insert&Update

        protected void btnsave5_Click1(object sender, ImageClickEventArgs e)
        {
            EmpSave();
        }

        /// <summary>
        /// Check 
        /// </summary>
        /// <returns></returns>
        protected string AllowTermination()
        {
            string Message = "";

            string sql1 = "";

            //sql1 = sql1 + " SELECT  e.emp_code, e.emp_name + '' + e.emp_lname AS EmpName, " ;
            //sql1 = sql1 + " CASE  WHEN  e.Leave_supervisor	IS NULL  THEN 'TRUE'	WHEN Leave_supervisor =-1  THEN 'TRUE' WHEN Leave_supervisor=0  THEN 'TRUE'    ELSE  'FALSE' END AS LEAVWWS,";
            //sql1 = sql1 + " CASE  WHEN e.pay_supervisor	IS NULL THEN 'TRUE' 	WHEN e.pay_supervisor=-1  THEN 'TRUE' WHEN e.pay_supervisor=0  THEN 'TRUE'  ELSE  'FALSE' END AS PAYS,";
            //sql1 = sql1 + " CASE WHEN e.emp_clsupervisor IS NULL THEN 'TRUE' 	WHEN  e.emp_clsupervisor=-1  THEN 'TRUE' WHEN  e.emp_clsupervisor=0  THEN 'TRUE'  ELSE  'FALSE' END AS CLAIMSS,";
            //sql1 = sql1 + " CASE WHEN e.timesupervisor	IS NULL THEN 'TRUE'	WHEN  e.timesupervisor=-1  THEN 'TRUE' WHEN  e.timesupervisor=0  THEN 'TRUE'  ELSE  'FALSE' END AS TIMES,";
            //sql1 = sql1 + " CASE WHEN e.emp_supervisor	IS NULL THEN 'TRUE'	WHEN e.emp_supervisor=-1  THEN 'TRUE' WHEN e.emp_supervisor=0  THEN 'TRUE'  ELSE  'FALSE' END AS LEAVESS ";
            //sql1 = sql1 + " FROM employee e WHERE e.emp_code=" + Utility.ToInteger(emp_code);


            //Ram
            sql1 = @"SELECT  e.emp_code, e.emp_name + '' + e.emp_lname AS EmpName," +
                   "CASE  WHEN  e.Leave_supervisor	IS NULL  THEN 'TRUE' WHEN Leave_supervisor =-1  THEN 'TRUE' WHEN Leave_supervisor=0  THEN 'TRUE'  WHEN  e.Leave_supervisor IS NOT NULL AND (SELECT emp_name from Employee where emp_code =e.Leave_supervisor) IS NULL    THEN 'TRUE'  ELSE  'FALSE' END AS LEAVWWS," +
                   "CASE  WHEN e.pay_supervisor	IS NULL THEN 'TRUE' WHEN e.pay_supervisor=-1  THEN 'TRUE' WHEN  e.pay_supervisor IS NOT NULL AND (SELECT emp_name from Employee where emp_code =e.pay_supervisor) IS NULL    THEN 'TRUE' WHEN e.pay_supervisor=0  THEN 'TRUE'  ELSE  'FALSE' END AS PAYS, " +
                   "CASE WHEN e.emp_clsupervisor IS NULL THEN 'TRUE' WHEN  e.emp_clsupervisor=-1  THEN 'TRUE' WHEN  e.emp_clsupervisor=0  THEN 'TRUE'WHEN  e.emp_clsupervisor IS NOT NULL AND (SELECT emp_name from Employee where emp_code =e.emp_clsupervisor) IS NULL    THEN 'TRUE' ELSE  'FALSE' END AS CLAIMSS," +
                   "CASE WHEN e.timesupervisor	IS NULL THEN 'TRUE'	WHEN  e.timesupervisor=-1  THEN 'TRUE' WHEN  e.timesupervisor=0  THEN 'TRUE'WHEN  e.timesupervisor IS NOT NULL AND (SELECT emp_name from Employee where emp_code =e.timesupervisor) IS NULL    THEN 'TRUE'ELSE  'FALSE' END AS TIMES, " +
                   "CASE WHEN e.emp_supervisor	IS NULL THEN 'TRUE'	WHEN e.emp_supervisor=-1  THEN 'TRUE' WHEN e.emp_supervisor=0  THEN 'TRUE' WHEN  e.emp_supervisor IS NOT NULL AND (SELECT emp_name from Employee where emp_code =e.emp_supervisor) IS NULL    THEN 'TRUE' ELSE  'FALSE' END AS LEAVESS  " +
                   "FROM employee e WHERE e.emp_code=" + Utility.ToInteger(emp_code);



            string sql2 = "";

            sql2 = " SELECT e.emp_name + '  ' + e.emp_lname AS EmpName ";
            sql2 = sql2 + " FROM employee e  WHERE  e.Leave_supervisor =" + Utility.ToInteger(emp_code);
            sql2 = sql2 + "     OR e.pay_supervisor =" + Utility.ToInteger(emp_code);
            sql2 = sql2 + "  OR e.emp_clsupervisor =" + Utility.ToInteger(emp_code);
            sql2 = sql2 + "  OR e.timesupervisor =" + Utility.ToInteger(emp_code);
            sql2 = sql2 + " OR e.emp_supervisor =" + Utility.ToInteger(emp_code);

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            ds = DataAccess.FetchRS(CommandType.Text, sql1, null);
            ds1 = DataAccess.FetchRS(CommandType.Text, sql2, null);

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr["LEAVWWS"].ToString() == "FALSE")
                    {
                        Message = Message + "Work Flow Leave Supervisor  ,";
                    }

                    if (dr["PAYS"].ToString() == "FALSE")
                    {
                        Message = Message + "Pay Roll Supervisor  , ";
                    }

                    if (dr["CLAIMSS"].ToString() == "FALSE")
                    {
                        Message = Message + "Claims Supervisor  , ";
                    }

                    if (dr["TIMES"].ToString() == "FALSE")
                    {
                        Message = Message + "Time Sheet Supervisor ,  ";
                    }

                    if (dr["LEAVESS"].ToString() == "FALSE")
                    {
                        Message = Message + " Leave Supervisor , ";
                    }
                }
            }

            string Message2 = "";

            if (ds1.Tables.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    Message2 = Message2 + dr[0].ToString() + " , ";
                }
            }

            if (Message.Length > 0)
            {
                Message = " ****************** Please Remove Supervisor For Current Employee ********************  " + Message;
            }

            if (Message2.Length > 0)
            {
                Message = Message + " ****************** Current Employee Is Either Leave , Payroll , WorkFlow ,Claims , Times Supervisor For Employee  ******************  " + Message2;
            }


            //Check Employee is assigned to Acomodation
            string SQL_Accom = "select * from EmpAccomadationDetails where Emp_code='" + Utility.ToInteger(emp_code) + "'and EffectiveCheckOutDate IS NULL";
            SqlDataReader dr_Accom = DataAccess.ExecuteReader(CommandType.Text, SQL_Accom, null);
            if (dr_Accom.HasRows)
            {
                Message = Message + " ****************** Current Employee Is Assigned to Accomodation  ******************  " + Message2;
            }
            //


            return Message;
        }

        protected void EmpSave()
        {
            #region costing
            SaveCosting();
            #endregion


            double leaves_remainings = 0;

            //#region readAgefromXML

            //string xmlpath = Server.MapPath("xmldata.xml");
            //XmlTextReader reader = new XmlTextReader(Server.MapPath("xmldata.xml"));
            //while (reader.Read())
            //{
            //    string str = reader.NodeType.ToString();
            //    if (str == "Element")
            //    {
            //        string age = XmlNodeType.Text.ToString();
            //    }
            //}

            //#endregion


            //Check IF Termination Date is NULL Or Not 
            if (rdTerminationDate.IsEmpty == false)
            {


                string msgTermination = AllowTermination();

                if (msgTermination.Length > 0)
                {
                    //Ram -Calling Pop-up
                    string sMsg = " Employee Can Not Terminate  " + msgTermination;
                    Session["Message"] = sMsg;
                    /*
                    Type cstype = this.GetType();
                    String csname1 = "PopupScript";
                    ClientScriptManager cs = Page.ClientScript;
                    String cstext1 = "OpenWindowTerminate('" + Utility.ToInteger(emp_code) + "','" + compid + "')";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    */
                    Response.Write("<script language='javascript'>");
                    string url = "Terminate.aspx?emp_code=" + Utility.ToInteger(emp_code) + "&TerminationDate=" + rdTerminationDate.SelectedDate.Value.ToString();
                    Response.Write(" window.open('" + url + "', 'myWindow', 'status = 1, height = 700, width = 700, resizable = 1,scrollbars=1');");
                    // Response.Write("return false;");
                    Response.Write("<" + "/script>");

                    return;


                    Session["Message"] = sMsg;
                    /*  
                      string sMsg = " Employee Can Not Terminate  " +  msgTermination;                    
                      sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                      Response.Write(sMsg);
                      string ErrMsg = "<font color = 'Red'> " + msgTermination + "</font>";
                      //lblerror.Text = ErrMsg;
                      return;
                   
                     */
                }
            }

            if (s == 0)
            {

                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI"))
                {

                    int intChkEmpTcExsist = 0;

                    string strchkTc = "SELECT count(*) FROM employee ep WHERE ep.time_card_no='" + txttimecardno.Value + "'";
                    intChkEmpTcExsist = DataAccess.ExecuteScalar(strchkTc, null);
                    if (intChkEmpTcExsist > 0)
                    {
                        string sMsg;
                        //string sMsg = "Employee Time card Already Exists";
                        //string sMsg = "Following Fields are missing: </br> 66Personal Info - Time/Card/Swipe/Punch ID";
                        sMsg = "<SCRIPT language='Javascript'>alert('Following Fields are missing: Personal Info - Time/Card/Swipe/Punch ID');</SCRIPT>";
                        Response.Write(sMsg);

                        //Page.RegisterStartupScript("myScript", "<script language=JavaScript>msg();</script>");
                        return;
                    }

                    //Check Employee Data Exists in Actattek log then Do not Allow to change the time card.
                }

                EmpName = txtEmpName.Value.Trim();
                if (EmpName != "" && EmpCode != "")
                {
                    //New Logic Applied To Read Licese --Code is Not in Use now ....
                    //if (!SMEPayrollLicensing.CanAddNewEmployee())
                    //{
                    //    string sMsg = "Your current license does not allow you create more employees. Please contact the vendor to enhance the license.";
                    //    sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                    //    Response.Write(sMsg);
                    //    return;
                    //}
                    varFileName = "";
                    string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture";

                    if (RadUpload1.UploadedFiles.Count != 0)
                    {
                        if (Directory.Exists(Server.MapPath(uploadpath)))
                        {
                            if (File.Exists(Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName()))
                            {
                                string sMsg = "File Already Exist";
                                sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                                Response.Write(sMsg);
                                return;
                            }
                            else
                            {
                                varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                                RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                                varFileName = RadUpload1.UploadedFiles[0].GetName();
                            }
                        }
                        else

                            Directory.CreateDirectory(Server.MapPath(uploadpath));
                        varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                        RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                        varFileName = RadUpload1.UploadedFiles[0].GetName();
                    }



                    string strtxtEmeConPer = txtEmeConPer.Text.ToString().Trim();
                    string strtxtEmeConPerRel = txtEmeConPerRel.Text.ToString().Trim();
                    string strtxtEmeConPerPh1 = txtEmeConPerPh1.Text.ToString().Trim();
                    string strtxtEmeConPerPh2 = txtEmeConPerPh2.Text.ToString().Trim();
                    string strtxtEmeConPerAdd = txtEmeConPerAdd.Text.ToString().Trim();
                    string strtxtEmeConPerRem = txtEmeConPerRem.Text.ToString().Trim();
                    string strtxtBloodGroup = txtBloodGroup.Text.ToString().Trim();
                    string strmye_cert_id = "";
                    string stragent_id = "";

                    if (cmbAgent.Value != "-select-")
                    {
                        stragent_id = cmbAgent.Value;
                    }
                    if (cmbMYE.Value != "-select-")
                    {
                        strmye_cert_id = cmbMYE.Value;
                    }

                    string EmpAlias = txtEmpAlias.Value;
                    
                    string Nationality = cmbNationality.Value;
                    string companycode = compcode;
                    string emptype = cmbEmpType.Value;
                    string ICPPNumber = txtnricno.Text;
                    string WPExpDate = "";
                    if (rdWPExpDate.IsEmpty == false)
                        WPExpDate = rdWPExpDate.SelectedDate.Value.ToString();
                    string PRDate = "";
                    if (rdPRDate.IsEmpty == false)
                        PRDate = rdPRDate.SelectedDate.Value.ToString();
                    string Phone = txtPhone.Value;
                    string HandPhone = txtHandPhone.Value;
                    string Address = txtAddress.Value;
                    string Country = cmbCountry.Value;
                    string PostalCode = txtPostalCode.Value;
                    string Email = txtEmail.Value;
                    string Sex = cmbSex.Value;
                    string block = txtblock.Value;
                    string unit = txtunit.Value;
                    string level = txtlevel.Value;
                    string street = txtstreet.Value;
                    string Religion = cmbReligion.Value;
                    string Race = cmbRace.Value;
                    string MaritalStatus = cmbMaritalStatus.Value;
                    string PlaceofBirth = cmbbirthplace.Value;


                    if (cmbEmpRefType.SelectedIndex == 0)
                    {
                        txtEmployeeCPFAcctNumber.Value = txtnricno.Text;
                        txtIncomeTaxID.Value = txtnricno.Text;
                    }
                    if (cmbEmpRefType.SelectedIndex == 1)
                    {
                        txtIncomeTaxID.Value = txtnricno.Text;
                        txtEmployeeCPFAcctNumber.Value = "";
                    }



                    string IncomeTaxID = txtIncomeTaxID.Value;
                    string DateofBirth = rdDOB.SelectedDate.Value.ToString();
                    string EmployeeCPFAcctNumber = txtEmployeeCPFAcctNumber.Value;
                    string EmployerCPFAcctNumber = cmbEmployerCPFAcctNumber.Items[cmbEmployerCPFAcctNumber.SelectedIndex].Text.ToString();
                    string GIROBank = txtgirobankname.Value;
                    string GIROCode = "0";
                    string GIROBranch = txtgirobranch.Value;
                    string GIROAccountNo = txtGIROAccountNo.Value;
                    string CPFEntitlement = cmbCPFEntitlement.Value;

                    string Department = cmbDepartment.Value;
                    string EmployerCPF = txtEmployerCPF_H.Value;
                    string EmployeeCPF = txtEmployeeCPF_H.Value;
                    string Designation = cmbDesignation.Value;

                    string Trade = cmbTrade.Value;
                    string supervisor = cmbsupervisor.Value;

                    //if (cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text != "-select-")
                    //{
                    //    supervisor = cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text;
                    //}
                    string clsupervisor = cmbclaimsupervisor.Value;
                    string pay_supervisor = cmbpayrollsupervisor.Value;
                    string leave_supervisor;// = cmbLeaveApproval.Value;

                    if (cmbLeaveApproval.Value == "-select-")
                    {
                        leave_supervisor = "-1";
                    }
                    else
                    {
                        leave_supervisor = cmbLeaveApproval.Value.ToString();

                    }

                    string tssupervisor = cmbtimesheetsupervisor.Value;
                    string Education = cmbeducation.Value;
                    string JoiningDate = "";
                    if (rdJoiningDate.IsEmpty == false)
                        JoiningDate = rdJoiningDate.SelectedDate.Value.ToString();

                    string ConfirmationDate = "";
                    if (rdConfirmationDate.IsEmpty == false)
                        ConfirmationDate = rdConfirmationDate.SelectedDate.Value.ToString();

                    string EmployeeGroup = cmbEmployeeGroup.Value;
                    string OTEntitled = cmbOTEntitled.Value;
                    string TerminationDate = "";
                    if (rdTerminationDate.IsEmpty == false)
                        TerminationDate = rdTerminationDate.SelectedDate.Value.ToString();
                    string terminreason = txtterminreason.Value;
                    string PayFrequency = rbpayfrequency.SelectedValue;
                    string PayRate = "";
                    //if (bViewSalAllowed)
                    //{
                    //    PayRate = txtPayRate.Text;
                    //}
                    //else
                    //{
                    //    PayRate = "0";
                    //}
                    PayRate = Convert.ToString(txtPayRate.Text);

                    string HourlyRate = "";
                    string HourlyRateMode = "";
                    if (rdoMOMHourlyRate.Checked)
                    {
                        HourlyRate = txtHourlyRate.Value;
                        HourlyRateMode = "A";

                    }
                    if (rdoManualHourlyRate.Checked)
                    {
                        HourlyRate = txtMannualHourlyRate.Value;
                        HourlyRateMode = "M";
                    }


                    string DailyRate = "";
                    string DailyRateMode = "";
                    if (rdoMOMDailyRate.Checked)
                    {
                        DailyRate = txtDailyRate.Value;
                        DailyRateMode = "A";
                    }
                    if (rdoMannualDailyRate.Checked)
                    {
                        DailyRate = txtMannualDailyRate.Value;
                        DailyRateMode = "M";
                    }

                    string wday_per_week = cmbworkingdays.Value;
                    string PayMode = cmbPayMode.Value;
                    string EmailPaySlip = cmbEmailPaySlip.Value;

                    string FWLCode = txtFWLCode.Value;
                    string MonthlyLevy = txtMonthlyLevy.Value;
                    string WithHoldTaxPct = "";
                    string WithHoldTaxAmt = "";
                    string SDFRequired = "";
                    if (chkSDFRequired.Checked == true) SDFRequired = "1"; else SDFRequired = "2";

                    string CDAC = "-1";
                    string MBMF = "-1";
                    string SINDA = "-1";
                    string ECF = "-1";
                    string CCHEST = "-1";
                    //dubai
                    if (IsCalculate)
                    {
                        if (txtFundType.Value == "CDAC")
                            CDAC = Utility.ToString(txtFundAmount.Value);
                        if (txtFundType.Value == "MBMF")
                            MBMF = Utility.ToString(txtFundAmount.Value);
                        if (txtFundType.Value == "SINDA")
                            SINDA = Utility.ToString(txtFundAmount.Value);
                        if (txtFundType.Value == "ECF")
                            ECF = Utility.ToString(txtFundAmount.Value);
                    }
                    int optout = 0;
                    int cpffh = 0;
                    int fundfh = 0;
                    int salhalf = 0;
                    if (IsCalculate)
                    {
                        if (chkoptfund.Checked)
                            optout = 1;
                        if (chkcomputecpffh.Checked)
                            cpffh = 1;
                        if (chkFUNDRequired.Checked)
                            fundfh = 1;
                        salhalf = Convert.ToInt32(ddlHalfSal.SelectedValue.ToString());
                    }
                    //if (chkboxhalf.Checked)
                   
                    string Remarks = txtRemarks.Value;
                    string Img = varFileName;

                    string emplname = txtlastname.Value;
                    string empcpfgroup = cmbempcpfgroup.Value;
                    string insurance = txtinsurance.Value;
                    string insurance_exp = "";
                    if (rdinsurance.IsEmpty == false)
                        insurance_exp = rdinsurance.SelectedDate.Value.ToString();
                    string csoc = txtcsoc.Value;

                    string csoc_exp = "";
                    if (rdcsoc.IsEmpty == false)
                        csoc_exp = rdcsoc.SelectedDate.Value.ToString();
                    string passport = txtpassport.Value;

                    string passport_exp = "";
                    if (rdpassport.IsEmpty == false)
                        passport_exp = rdpassport.SelectedDate.Value.ToString();
                    string probation = cmbprobation.SelectedValue;
                    string giroaccname = txtgiroaccountname.Value;
                    string usergroupid = cmbUserGroup.Value;
                    string localadd2 = txtlocal2.Value;
                    string foreignadd1 = txtforeign1.Value;
                    string foreignadd2 = txtforeign2.Value;
                    string TimeCardNo = txttimecardno.Value;
                    string fpostalcode = txtforeignpostalcode.Value;
                    //Added By Raja(24/11/2008)
                    string v1rate = txtv1rate.Text;
                    string v2rate = txtv2rate.Text;
                    string v3rate = txtv3rate.Text;
                    string v4rate = txtv4rate.Text;

                    string ppissuedate = "";
                    if (rdppissuedate.IsEmpty == false)
                        ppissuedate = rdppissuedate.SelectedDate.Value.ToString();
                    string wpappdate = "";
                    if (rdwpappdate.IsEmpty == false)
                        wpappdate = rdwpappdate.SelectedDate.Value.ToString();

                    double ot1rate = Utility.ToDouble(txtOT1Rate.Text);
                    double ot2rate = Utility.ToDouble(txtOT2Rate.Text);
                    double leaves_remaining = Utility.ToDouble(txtRemainingLeaves.Value);
                    string emp_ref_type = Utility.ToString(cmbEmpRefType.Value);
                    int emp_category = Utility.ToInteger(cmbEmpCategory.Value);

                    /* USE BELOW SECTION FOR CLAVON */
                    int batch_no = Utility.ToInteger(txtBatchNo.Value);
                    string wp_issuedate = "";
                    string wp_arrivaldate = "";
                    if (rdwpissuedate.IsEmpty == false)
                        wp_issuedate = rdwpissuedate.SelectedDate.Value.ToString();
                    if (rdWPArrDate.IsEmpty == false)
                        wp_arrivaldate = rdWPArrDate.SelectedDate.Value.ToString();
                    string shipyard_quota = Utility.ToString(txtShipyardQuota.Value);
                    int photo = Utility.ToInteger(txtPhotoNo.Value);
                    string wpNumber = Utility.ToString(txtWPNumber.Value);

                    if(EmpAlias != "" || EmpAlias != null)
                    {
                        string strSQLUser = "Select UserName from Users where Company_Id=" + Utility.ToInteger(compid) + "";
                        DataSet dsUser = new DataSet();
                        dsUser = DataAccess.FetchRS(CommandType.Text, strSQLUser, null);

                        DataRow[] foundRows;
                        foundRows = dsUser.Tables[0].Select("UserName='" + EmpAlias + "'");

                        if (foundRows.Length > 0)
                        {
                            ShowMessageBox("Employee Alias already have in Users, please define another.");
                            return;
                        }
                    }


                    
                    //if (DateofBirth.ToString() != "")
                    //{
                    //    DateTime dtDOB = DateTime.Parse(DateofBirth);
                    //    DateTime today = DateTime.Now;

                    //    TimeSpan ts = today.Subtract(dtDOB);
                    //    double days = (double)ts.TotalHours / (24);
                    //    double months = Math.Round(days / 30.4, 2);
                    //    int years = (int)months / 12;

                    //    if (years < 13)
                    //    {
                    //        ShowMessageBox("Job Info-Employee should not be below 13 years.\n");
                    //        string script = "alert('Job Info-Employee should not be below 13 years.\n');";
                    //        ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                    //        return;
                    //    }

                    //}
                    string _ScanCode = "";
                    if(!string.IsNullOrEmpty(this.ScanCode.Value.ToString()))
                    {
                        _ScanCode=this.ScanCode.Value.ToString();
                    }
                   
                    int i = 0;
                    int icount = 128;
                    SqlParameter[] parms = new SqlParameter[icount]; // Parms = 86 (GENERAL), Parms = 93 (Clavon)
                    parms[i++] = new SqlParameter("@compcode", Utility.ToString(companycode));
                    parms[i++] = new SqlParameter("@emp_name", Utility.ToString(EmpName));
                    parms[i++] = new SqlParameter("@emp_alias", Utility.ToString(EmpAlias));
                    parms[i++] = new SqlParameter("@ScanCode", Utility.ToString(_ScanCode));
                    parms[i++] = new SqlParameter("@nationality", Utility.ToInteger(Nationality));
                    parms[i++] = new SqlParameter("@emp_type", Utility.ToString(emptype));
                    parms[i++] = new SqlParameter("@emp_lname", Utility.ToString(emplname));
                    parms[i++] = new SqlParameter("@empcpftype", Utility.ToInteger(empcpfgroup));
                    parms[i++] = new SqlParameter("@Insurance_number", Utility.ToString(insurance));
                    parms[i++] = new SqlParameter("@Insurance_expiry", Utility.ToString(insurance_exp));
                    parms[i++] = new SqlParameter("@CSOC_number", Utility.ToString(csoc));
                    parms[i++] = new SqlParameter("@CSOC_expiry", Utility.ToString(csoc_exp));
                    parms[i++] = new SqlParameter("@passport", Utility.ToString(passport));
                    parms[i++] = new SqlParameter("@passport_expiry", Utility.ToString(passport_exp));
                    parms[i++] = new SqlParameter("@ic_pp_number", Utility.ToString(ICPPNumber));
                    parms[i++] = new SqlParameter("@wp_exp_date", Utility.ToString(WPExpDate));
                    parms[i++] = new SqlParameter("@pr_date", Utility.ToString(PRDate));
                    parms[i++] = new SqlParameter("@address", Utility.ToString(Address));
                    parms[i++] = new SqlParameter("@country", Utility.ToInteger(Country));
                    parms[i++] = new SqlParameter("@postal_code", Utility.ToString(PostalCode));
                    parms[i++] = new SqlParameter("@phone", Utility.ToString(Phone));
                    parms[i++] = new SqlParameter("@hand_phone", Utility.ToString(HandPhone));
                    parms[i++] = new SqlParameter("@email", Utility.ToString(Email));
                    parms[i++] = new SqlParameter("@sex", Utility.ToString(Sex));
                    parms[i++] = new SqlParameter("@religion", Utility.ToInteger(Religion));
                    if (IsCalculate)
                    {
                        parms[i++] = new SqlParameter("@race", Utility.ToInteger(Race));
                        parms[i++] = new SqlParameter("@employer_cpf_acct", Utility.ToString(EmployerCPFAcctNumber));
                        parms[i++] = new SqlParameter("@employee_cpf_acct", Utility.ToString(EmployeeCPFAcctNumber));
                    }
                    else
                    {
                        parms[i++] = new SqlParameter("@race", 4);
                        parms[i++] = new SqlParameter("@employer_cpf_acct", "1234567");
                        parms[i++] = new SqlParameter("@employee_cpf_acct","1234567");
                    }
                    parms[i++] = new SqlParameter("@marital_status", Utility.ToString(MaritalStatus));
                    parms[i++] = new SqlParameter("@place_of_birth", Utility.ToString(PlaceofBirth));
                    parms[i++] = new SqlParameter("@date_of_birth", Utility.ToString(DateofBirth));

                    parms[i++] = new SqlParameter("@income_taxid", Utility.ToString(IncomeTaxID));
                    //parms[i++] = new SqlParameter("@employee_cpf_acct", Utility.ToString(EmployeeCPFAcctNumber));


                  
                    parms[i++] = new SqlParameter("@giro_bank", Utility.ToString(GIROBank));
                    parms[i++] = new SqlParameter("@giro_code", Utility.ToString(GIROCode));
                    parms[i++] = new SqlParameter("@giro_branch", Utility.ToString(GIROBranch));
                    parms[i++] = new SqlParameter("@giro_acct_number", Utility.ToString(GIROAccountNo));


                    //dubai
                    if (IsCalculate)
                    {

                        parms[i++] = new SqlParameter("@cpf_entitlement", Utility.ToString(CPFEntitlement));
                        
                    }
                    else
                    {
                        
                        parms[i++] = new SqlParameter("@cpf_entitlement","N");
                    }

                    parms[i++] = new SqlParameter("@cpf_employer", Utility.ToDouble(EmployerCPF));
                    parms[i++] = new SqlParameter("@department", Utility.ToInteger(Department));
                    parms[i++] = new SqlParameter("@cpf_employee", Utility.ToDouble(EmployeeCPF));
                    parms[i++] = new SqlParameter("@designation", Utility.ToInteger(Designation));
                    parms[i++] = new SqlParameter("@trade", Utility.ToInteger(Trade));
                    parms[i++] = new SqlParameter("@emp_supervisor", Utility.ToInteger(supervisor));
                    parms[i++] = new SqlParameter("@emp_clsupervisor", Utility.ToInteger(clsupervisor));
                    parms[i++] = new SqlParameter("@education", Utility.ToString(Education));
                    parms[i++] = new SqlParameter("@joining_date", Utility.ToString(JoiningDate));
                    parms[i++] = new SqlParameter("@confirmation_date", Utility.ToString(ConfirmationDate));
                    parms[i++] = new SqlParameter("@empgroup", Utility.ToInteger(EmployeeGroup));
                    parms[i++] = new SqlParameter("@ot_entitlement", Utility.ToString(OTEntitled));
                    parms[i++] = new SqlParameter("@termination_date", Utility.ToString(TerminationDate));
                    parms[i++] = new SqlParameter("@termination_reason", Utility.ToString(terminreason));
                    parms[i++] = new SqlParameter("@pay_frequency", Utility.ToString(PayFrequency));
                    parms[i++] = new SqlParameter("@payrate", PayRate);
                    parms[i++] = new SqlParameter("@payment_mode", Utility.ToString(PayMode));
                    parms[i++] = new SqlParameter("@hourlyRate", Utility.ToDouble(HourlyRate));
                    parms[i++] = new SqlParameter("@hourlyRateMode", Utility.ToString(HourlyRateMode));
                    parms[i++] = new SqlParameter("@dailyRateMode", Utility.ToString(DailyRateMode));
                    parms[i++] = new SqlParameter("@dailyRate", Utility.ToDouble(DailyRate));
                    parms[i++] = new SqlParameter("@wday_per_week", Utility.ToDouble(wday_per_week));
                    parms[i++] = new SqlParameter("@email_payslip", Utility.ToString(EmailPaySlip));
                    parms[i++] = new SqlParameter("@fw_code", Utility.ToString(FWLCode));
                    parms[i++] = new SqlParameter("@fw_levy", Utility.ToDouble(MonthlyLevy));
                    parms[i++] = new SqlParameter("@wh_tax_pct", Utility.ToDouble(WithHoldTaxPct));
                    parms[i++] = new SqlParameter("@wh_tax_amt", Utility.ToDouble(WithHoldTaxAmt));
                    parms[i++] = new SqlParameter("@sdf_required", SDFRequired);
                    parms[i++] = new SqlParameter("@cdac_fund", Utility.ToDouble(CDAC));
                    parms[i++] = new SqlParameter("@mbmf_fund", Utility.ToDouble(MBMF));
                    parms[i++] = new SqlParameter("@sinda_fund", Utility.ToDouble(SINDA));
                    parms[i++] = new SqlParameter("@ecf_fund", Utility.ToDouble(ECF));
                    parms[i++] = new SqlParameter("@cchest_fund", Utility.ToDouble(CCHEST));
                    parms[i++] = new SqlParameter("@fund_optout", Utility.ToInteger(optout));
                    parms[i++] = new SqlParameter("@remarks", Utility.ToString(Remarks));
                    parms[i++] = new SqlParameter("@Images", Utility.ToString(Img));
                    parms[i++] = new SqlParameter("@Company_Id", Utility.ToInteger(compid));
                    parms[i++] = new SqlParameter("@probation_period", Utility.ToInteger(probation));
                    parms[i++] = new SqlParameter("@leaveCarryForward", "");
                    parms[i++] = new SqlParameter("@giro_acc_name", Utility.ToString(giroaccname));
                    parms[i++] = new SqlParameter("@groupid", Utility.ToString(usergroupid));
                    parms[i++] = new SqlParameter("@localaddress2", Utility.ToString(localadd2));
                    parms[i++] = new SqlParameter("@block_no", Utility.ToString(block));
                    parms[i++] = new SqlParameter("@unit_no", Utility.ToString(unit));
                    parms[i++] = new SqlParameter("@level_no", Utility.ToString(level));
                    parms[i++] = new SqlParameter("@street_name", Utility.ToString(street));
                    parms[i++] = new SqlParameter("@foreignaddress1", Utility.ToString(foreignadd1));
                    parms[i++] = new SqlParameter("@foreignaddress2", Utility.ToString(foreignadd2));
                    parms[i++] = new SqlParameter("@time_card_no", Utility.ToString(TimeCardNo));
                    parms[i++] = new SqlParameter("@foreignpostalcode", Utility.ToString(fpostalcode));
                    parms[i++] = new SqlParameter("@pp_issue_date", Utility.ToString(ppissuedate));
                    parms[i++] = new SqlParameter("@worker_levy", cmblevy.Value);
                    parms[i++] = new SqlParameter("@wp_application_date", wpappdate);
                    parms[i++] = new SqlParameter("@emp_ref_type", Utility.ToInteger(emp_ref_type));
                    parms[i++] = new SqlParameter("@emp_category", Utility.ToInteger(emp_category));
                    parms[i++] = new SqlParameter("@v1rate", Utility.ToDouble(v1rate));
                    parms[i++] = new SqlParameter("@v2rate", Utility.ToDouble(v2rate));
                    parms[i++] = new SqlParameter("@v3rate", Utility.ToDouble(v3rate));
                    parms[i++] = new SqlParameter("@v4rate", Utility.ToDouble(v4rate));
                    parms[i++] = new SqlParameter("@batch_no", Utility.ToInteger(batch_no));
                    parms[i++] = new SqlParameter("@wp_issue_date", Utility.ToString(wp_issuedate));
                    parms[i++] = new SqlParameter("@shipyard_quota", Utility.ToString(shipyard_quota));
                    parms[i++] = new SqlParameter("@photo_no", Utility.ToInteger(photo));
                    parms[i++] = new SqlParameter("@wp_number", Utility.ToString(wpNumber));
                    parms[i++] = new SqlParameter("@ot1rate", Utility.ToDouble(ot1rate));
                    parms[i++] = new SqlParameter("@ot2rate", Utility.ToDouble(ot2rate));
                    parms[i++] = new SqlParameter("@EmeConPer", Utility.ToString(strtxtEmeConPer));
                    parms[i++] = new SqlParameter("@EmeConPerRel", Utility.ToString(strtxtEmeConPerRel));
                    parms[i++] = new SqlParameter("@EmeConPerPh1", Utility.ToString(strtxtEmeConPerPh1));
                    parms[i++] = new SqlParameter("@EmeConPerPh2", Utility.ToString(strtxtEmeConPerPh2));
                    parms[i++] = new SqlParameter("@EmeConPerAdd", Utility.ToString(strtxtEmeConPerAdd));
                    parms[i++] = new SqlParameter("@EmeConPerRem", Utility.ToString(strtxtEmeConPerRem));
                    parms[i++] = new SqlParameter("@bloodgroup", Utility.ToString(strtxtBloodGroup));
                    parms[i++] = new SqlParameter("@agent_id", Utility.ToString(stragent_id));
                    parms[i++] = new SqlParameter("@mye_cert_id", Utility.ToString(strmye_cert_id));
                    parms[i++] = new SqlParameter("@wp_arrival_date", Utility.ToString(wp_arrivaldate));
                    parms[i++] = new SqlParameter("@pay_supervisor", Utility.ToInteger(pay_supervisor));
                    parms[i++] = new SqlParameter("@safetypass_id", Utility.ToInteger("0"));
                    parms[i++] = new SqlParameter("@safetypass_sno", Utility.ToString(""));
                    parms[i++] = new SqlParameter("@safetypass_expiry", Utility.ToString(""));
                    parms[i++] = new SqlParameter("@payrolltype", Utility.ToInteger(ddlPayrollType.SelectedValue.ToString()));
                    parms[i++] = new SqlParameter("@compcpffh", Utility.ToInteger(cpffh));
                    parms[i++] = new SqlParameter("@tssupervisor", Utility.ToInteger(tssupervisor));
                    parms[i++] = new SqlParameter("@compfundfh", Utility.ToInteger(fundfh));
                    parms[i++] = new SqlParameter("@halfsalary", Utility.ToInteger(salhalf));
                    parms[i++] = new SqlParameter("@Leave_supervisor", Utility.ToInteger(leave_supervisor));

                    parms[i++] = new SqlParameter("@PaymentType", Utility.ToInteger(radListPayType.SelectedValue));

                    string sqlCurr4 = "Select CurrencyId from girobanks Where id=" + cmbPayMode.Value;

                    SqlDataReader sqldr4 = null;
                    sqldr4 = DataAccess.ExecuteReader(CommandType.Text, sqlCurr4, null);

                    int curr4 = 0;

                    while (sqldr4.Read())
                    {
                        if (sqldr4.GetValue(0) != null && sqldr4.GetValue(0).ToString() != "")
                        {
                            curr4 = Convert.ToInt32(sqldr4.GetValue(0).ToString());
                        }
                    }
                    if (curr4 == 0)
                    {
                        //drpCurrBank1.SelectedIndex = 0;
                    }
                    else
                    {
                        drpCurrBank1.SelectedValue = curr4.ToString();
                    }

                    parms[i++] = new SqlParameter("@CurrencyBank", Utility.ToInteger(drpCurrBank1.SelectedValue));

                    parms[i++] = new SqlParameter("@PaymentPart", Utility.ToInteger(drpPaymentMode.SelectedValue));
                    if (HttpContext.Current.Session["CurrentCompany"].ToString() != "1")
                    {
                        string sql = "select isMaster, isMasterEmpTemp From Company Where Company_ID=1";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        if (dr.Read())
                        {
                            ismaster = dr[0].ToString();
                            ismastertempemp = dr[1].ToString();
                            if (ismaster == "True" && ismastertempemp == "True")
                            {
                                parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("3"));
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                            }
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                        }
                    }
                    else
                    {
                        parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                    }
                    parms[i] = new SqlParameter("@UCode", SqlDbType.Int);
                    parms[i].Direction = ParameterDirection.Output;

                    string sSQL = "sp_emp_add";
                    try
                    {
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal >= 1)
                        {
                            lblerror.ForeColor = System.Drawing.Color.Green;
                            lblerror.Text = "Information Added Successfully.";
                            int ucode = Utility.ToInteger(parms[i].Value);
                            string strEmpCode = Utility.ToString(parms[i].Value);
                            string password1 = companycode + "_" + ucode;
                            string password = encrypt.SyEncrypt(password1);

                            SqlParameter[] parms1 = new SqlParameter[2];
                            parms1[0] = new SqlParameter("@password", Utility.ToString(password));
                            parms1[1] = new SqlParameter("@ucode", Utility.ToString(ucode));

                            int returnval = DataAccess.ExecuteStoreProc("sp_password_update", parms1);

                            double leaves_remaining1 = Utility.ToDouble(txtRemainingLeaves.Value);
                            double hleaves = Utility.ToDouble(txthdnleaves.Value);
                            if (leaves_remaining1 == 0)
                            {
                                leaves_remainings = leaves_remaining1;
                            }
                            else
                            {

                                if (hleaves < leaves_remaining1)
                                {
                                    string ssqlb = "Select * From leaves_annual  Where emp_id='" + ucode.ToString() + "' Order By Leave_Year";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    if (ds.Tables[0].Rows.Count <= 0)
                                    {
                                        ssqlb = "Insert Into leaves_annual (emp_id, leave_remaining, leave_year) values ('" + ucode.ToString() + "'," + leaves_remaining1 + ",'" + DateTime.Now.Year.ToString() + "')";
                                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    }

                                }
                            }

                            string strrdEndDate = "";
                            string strrdFromDate = System.DateTime.Today.Date.ToString("MM/dd/yyyy");


                            if (Department == "-select-")
                            {
                                Department = "";
                            }
                            if (Designation == "-select-")
                            {
                                Designation = "";
                            }
                            if (Trade == "-select-")
                            {
                                Trade = "";
                            }
                            //string ssqlb1 = "Update EmployeePayHistory Set ToDate =Convert(datetime,'" + rdFrom.SelectedDate.Value.AddDays(-1).ToShortDateString() + "',103) Where Emp_ID=" + emp_code + " And ToDate is null";
                            string ssqlb1 = "Insert Into EmployeePayHistory (Emp_ID, FromDate, DepartmentID, DesignationID, TradeID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate) Values ('" + strEmpCode + "','" + strrdFromDate + "','" + Department + "','" + Designation + "','" + Trade + "','" + OTEntitled + "','" + CPFEntitlement + "','" + ot1rate + "','" + ot2rate + "','" + PayFrequency + "'," + wday_per_week + ",EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + PayRate + "'),'" + HourlyRateMode + "','" + HourlyRate + "','" + DailyRateMode + "','" + DailyRate + "')";
                            DataAccess.FetchRS(CommandType.Text, ssqlb1, null);

                            if (returnval == 0)
                            {
                                lblerror.ForeColor = System.Drawing.Color.Red;
                                lblerror.Text = "Unable to create the password";
                            }
                            else
                            {
                                if (ismaster == "True")
                                {
                                    if (HttpContext.Current.Session["CurrentCompany"].ToString() != "1")
                                    {
                                        sendemail();
                                    }
                                }
                                Session["ErrorMessage"] = "Employee Added Successfully.";
                                Response.Redirect("Employee.aspx?msg=Employee Added Successfully.");
                            }
                        }
                        else
                        {
                            if (retVal == -1)
                            {
                                lblerror.ForeColor = System.Drawing.Color.Red;
                                lblerror.Text = "Employee Alias Name already exist in the company. Please choose a different Alias name";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        lblerror.ForeColor = System.Drawing.Color.Red;
                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                        {
                            ErrMsg = "<font color = 'Red'>Username/Alias/Login ID/NRIC/FIN already Exist, Please Enter some other Username</font>";
                        }
                        lblerror.Text = ErrMsg;
                    }
                }
        #endregion Insert
            }
            #region update
            else
            {

                string strMsg = "";
                string companycode = compcode;
                string employeecode = txtEmpCode.Value.Substring(companycode.Trim().Length, txtEmpCode.Value.Trim().Length - companycode.Trim().Length);
                string TerminationDate = "";



                if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS") || HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI")
                {
                    int intChkEmpTcExsist = 0;

                    //check it has same value
                    string strchkTc_new = "select time_card_no from employee WHERE  emp_code='" + employeecode + "'";
                    int intChkEmpTcExsist_new = DataAccess.ExecuteScalar(strchkTc_new, null);

                    if (txttimecardno.Value == "")
                    {
                        string sMsg1 = "<SCRIPT language='Javascript'>alert('Following Fields are missing: Personal Info - Time/Card/Swipe/Punch ID');</SCRIPT>";
                        Response.Write(sMsg1);
                        return;
                    }
                    else if (intChkEmpTcExsist_new.ToString() != txttimecardno.Value)
                    {

                        string strchkTc = "SELECT count(*) FROM employee ep WHERE  ep.time_card_no='" + txttimecardno.Value + "' AND emp_code!='" + employeecode + "'";
                        intChkEmpTcExsist = DataAccess.ExecuteScalar(strchkTc, null);
                        if (intChkEmpTcExsist > 0)
                        {
                            string sMsg;
                            //string sMsg = "--Employee Time card Already Exists";
                            //string sMsg = "Following Fields are missing: </br>  Personal Info - Time/Card/Swipe/Punch ID";
                            //sMsg = "<SCRIPT language='Javascript'>alert('Following Fields are missing: <br/> Personal Info - Time/Card/Swipe/Punch ID');</SCRIPT>";

                            //Page.RegisterStartupScript("myScript", "<script language=JavaScript>msg();</script>");

                            //sMsg = "<SCRIPT language='Javascript'>alert('Following Fields are missing: Personal Info - Time/Card/Swipe/Punch ID');</SCRIPT>";
                            sMsg = "<SCRIPT language='Javascript'>alert('Time/Card/Swipe/Punch ID Already Exist');</SCRIPT>";
                            Response.Write(sMsg);
                            return;
                        }
                    }

                    if (Session["TCID"].ToString() != txttimecardno.Value.ToString())
                    {
                        //Check if Data for employee Exists in ACTATEK_LOGS_PROXY if yes do not allow to change time card id 
                        string sql1 = "SELECT count(*) FROM ACTATEK_LOGS_PROXY WHERE userID='" + Session["TCID"].ToString() + "'";
                        intChkEmpTcExsist = DataAccess.ExecuteScalar(sql1, null);
                        if (intChkEmpTcExsist > 0)
                        {

                            string sMsg = "Employee Time card ID can not change : Employee Time sheet with time card ID " + Session["TCID"].ToString() + " already processed";
                            sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                            Response.Write(sMsg);
                            return;
                        }
                    }
                }



                if (rdTerminationDate.IsEmpty == false)
                    TerminationDate = rdTerminationDate.SelectedDate.Value.ToString();

                //string strSql = "Select Count(*) Cnt From (Select Emp_ID EmpCode From EmployeeAssignedToPayrollGroup Union Select emp_supervisor EmpCode  from Employee Union Select emp_clsupervisor EmpCode  from Employee) D Where EmpCode=" + employeecode;
                //R --not checking in EmployeeAssignedToPayrollGroup Table
                string strSql = "Select Count(*) Cnt From ( Select emp_supervisor EmpCode  from Employee Union Select emp_clsupervisor EmpCode  from Employee) D Where EmpCode=" + employeecode;

                string TerminateEmpMessage = null;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, strSql, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0 && TerminationDate.ToString().Length > 1)
                    {
                        #region If he is having supervisor but terminated (manually in DB)

                        string sSQLEmpCode = "select * from employee where emp_clsupervisor='" + employeecode + "' or emp_supervisor=" + employeecode;
                        int TerminateEmpReference = DataAccess.ExecuteScalar(sSQLEmpCode, null);

                        string sSQLEmpCode1 = "Select emp_name from Employee where emp_code=" + TerminateEmpReference;

                        DataSet dsEmp = new DataSet();
                        dsEmp = getDataSet(sSQLEmpCode1);
                        foreach (DataRow theRow in dsEmp.Tables[0].Rows)
                        {
                            TerminateEmpMessage = "Check with Employee:" + theRow["emp_name"].ToString();

                        }

                        #endregion


                        strMsg = "Employee in use as Leave/Claim/Payroll Supervisor cannot be terminated.";

                        //Ram
                        // lblerror.Text = strMsg;

                        /* Type cstype = this.GetType();
                         String csname1 = "PopupScript";
                         ClientScriptManager cs = Page.ClientScript;
                         String cstext1 = "OpenWindowTerminate('" + Utility.ToInteger(emp_code) + "','" + compid + "')";
                         cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                         */
                        Response.Write("<script language='javascript'>");
                        string url = "Terminate.aspx?emp_code=" + Utility.ToInteger(emp_code);
                        Response.Write(" window.open('" + url + "', 'myWindow', 'status = 1, height = 700, width = 700, resizable = 1,scrollbars=1');");
                        //Response.Write("return false;");
                        Response.Write("<" + "/script>");


                        Session["Message"] = strMsg + TerminateEmpMessage;
                        Session["EmpCode"] = Convert.ToInt16(dr[0].ToString());
                    }
                    else
                    {
                        if ((cmbclaimsupervisor.Value != "" && cmbsupervisor.Value != "" && cmbpayrollsupervisor.Value != "") && TerminationDate.ToString().Length > 1)
                        {
                            strMsg = "Before terminating, please Un-select Employees Supervisor for Leave/Claim/Payroll.";

                            //Ram
                            //lblerror.Text = strMsg;

                            /* Type cstype = this.GetType();
                             String csname1 = "PopupScript";
                             ClientScriptManager cs = Page.ClientScript;
                             String cstext1 = "OpenWindowTerminate('" + Utility.ToInteger(emp_code) + "','" + compid + "')";
                             cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                             */

                            Response.Write("<script language='javascript'>");
                            string url = "Terminate.aspx?emp_code=" + Utility.ToInteger(emp_code);
                            Response.Write(" window.open('" + url + "', 'myWindow', 'status = 1, height = 700, width = 700, resizable = 1,scrollbars=1');");
                            // Response.Write("return false;");
                            Response.Write("<" + "/script>");



                            Session["Message"] = strMsg;


                        }
                        else
                        {
                            varFileName = "";
                            string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Picture";
                            if (RadUpload1.UploadedFiles.Count != 0)
                            {
                                if (Directory.Exists(Server.MapPath(uploadpath)))
                                {
                                    if (File.Exists(Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName()))
                                    {
                                        File.Delete(Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName());
                                        varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                                        RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                                        varFileName = RadUpload1.UploadedFiles[0].GetName();
                                    }
                                    else
                                    {
                                        varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                                        RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                                        varFileName = RadUpload1.UploadedFiles[0].GetName();
                                    }
                                }
                                else
                                {
                                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                                    varFileName = Server.MapPath(uploadpath) + "/" + RadUpload1.UploadedFiles[0].GetName();
                                    RadUpload1.UploadedFiles[0].SaveAs(varFileName);
                                    varFileName = RadUpload1.UploadedFiles[0].GetName();
                                }
                            }
                            double ot1rate = Utility.ToDouble(txtOT1Rate.Text);
                            double ot2rate = Utility.ToDouble(txtOT2Rate.Text);

                            string strtxtEmeConPer = txtEmeConPer.Text.ToString().Trim();
                            string strtxtEmeConPerRel = txtEmeConPerRel.Text.ToString().Trim();
                            string strtxtEmeConPerPh1 = txtEmeConPerPh1.Text.ToString().Trim();
                            string strtxtEmeConPerPh2 = txtEmeConPerPh2.Text.ToString().Trim();
                            string strtxtEmeConPerAdd = txtEmeConPerAdd.Text.ToString().Trim();
                            string strtxtEmeConPerRem = txtEmeConPerRem.Text.ToString().Trim();
                            string strtxtBloodGroup = txtBloodGroup.Text.ToString().Trim();
                            string strmye_cert_id = "";
                            string stragent_id = "";
                            if (cmbAgent.Value != "-select-")
                            {
                                stragent_id = cmbAgent.Value;
                            }
                            if (cmbMYE.Value != "-select-")
                            {
                                strmye_cert_id = cmbMYE.Value;
                            }

                            string EmpName = txtEmpName.Value;

                            string EmpAlias = txtEmpAlias.Value;
                            string Nationality = cmbNationality.Value;
                            string emptype = cmbEmpType.Value;
                            string ICPPNumber = txtnricno.Text;
                            string WPExpDate = "";
                            if (rdWPExpDate.IsEmpty == false)
                                WPExpDate = rdWPExpDate.SelectedDate.Value.ToString();
                            string PRDate = "";
                            if (rdPRDate.IsEmpty == false)
                                PRDate = rdPRDate.SelectedDate.Value.ToString();
                            string Phone = txtPhone.Value;
                            string HandPhone = txtHandPhone.Value;
                            string Address = txtAddress.Value;
                            string Country = cmbCountry.Value;
                            string PostalCode = txtPostalCode.Value;
                            string Email = txtEmail.Value;
                            string Sex = cmbSex.Value;
                            string Religion = cmbReligion.Value;
                            string Race = cmbRace.Value;
                            string MaritalStatus = cmbMaritalStatus.Value;
                            string PlaceofBirth = cmbbirthplace.Value;

                            if (cmbEmpRefType.SelectedIndex == 0)
                            {
                                txtEmployeeCPFAcctNumber.Value = txtnricno.Text;
                                txtIncomeTaxID.Value = txtnricno.Text;
                            }
                            if (cmbEmpRefType.SelectedIndex == 1)
                            {
                                txtIncomeTaxID.Value = txtnricno.Text;
                                txtEmployeeCPFAcctNumber.Value = "";
                            }


                            string IncomeTaxID = txtIncomeTaxID.Value;
                            string DateofBirth = rdDOB.SelectedDate.Value.ToString();
                            string EmployeeCPFAcctNumber = txtEmployeeCPFAcctNumber.Value;
                            string EmployerCPFAcctNumber = cmbEmployerCPFAcctNumber.Items[cmbEmployerCPFAcctNumber.SelectedIndex].Text.ToString();
                            string GIROBank = txtgirobankname.Value;
                            string GIROCode = "0";
                            string GIROBranch = txtgirobranch.Value;
                            string GIROAccountNo = txtGIROAccountNo.Value;
                            string CPFEntitlement = cmbCPFEntitlement.Value;

                            string Department = cmbDepartment.Value;
                            string EmployerCPF = txtEmployerCPF_H.Value;
                            string EmployeeCPF = txtEmployeeCPF_H.Value;
                            string Designation = cmbDesignation.Value;
                            string Trade = cmbTrade.Value;
                            string supervisor = cmbsupervisor.Value;


                            //if (cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text != "-select-")
                            //{
                            //    supervisor =cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Value;
                            //}
                            string clsupervisor = cmbclaimsupervisor.Value;
                            string pay_supervisor = cmbpayrollsupervisor.Value;
                            string leave_supervisor;
                            if (cmbLeaveApproval.Value == "-select-")
                            {
                                leave_supervisor = "-1";
                            }
                            else
                            {

                                leave_supervisor = cmbLeaveApproval.Value;

                            }

                            string tssupervisor = cmbtimesheetsupervisor.Value;
                            string Education = cmbeducation.Value;
                            string JoiningDate = "";
                            if (rdJoiningDate.IsEmpty == false)
                                JoiningDate = rdJoiningDate.SelectedDate.Value.ToString();

                            string ConfirmationDate = "";
                            if (rdConfirmationDate.IsEmpty == false)
                                ConfirmationDate = rdConfirmationDate.SelectedDate.Value.ToString();

                            string EmployeeGroup = cmbEmployeeGroup.Value;
                            string OTEntitled = cmbOTEntitled.Value;
                            if (TerminationDate != "")
                            {
                                string sSQL1 = "sp_CheckSuperAdminCount";
                                int j = 0;
                                SqlParameter[] param = new SqlParameter[2];
                                param[j++] = new SqlParameter("@CompanyID", Utility.ToInteger(compid));
                                param[j++] = new SqlParameter("@EmployeeID", Utility.ToInteger(emp_code));
                                activeSuperAdminCount = Convert.ToInt32(DataAccess.ExecuteSPScalar(sSQL1, param));

                                if (activeSuperAdminCount > 1)
                                {
                                    string ssqlterm = "update employee set statusid='2' where emp_code='" + emp_code + "' ";
                                    DataAccess.FetchRS(CommandType.Text, ssqlterm, null);
                                }
                            }

                            string terminreason = txtterminreason.Value;
                            string PayFrequency = rbpayfrequency.SelectedValue;
                            string PayRate = "";

                            //if (bViewSalAllowed)
                            //{
                            //    PayRate = txtPayRate.Text;
                            //}
                            //else
                            //{
                            //    PayRate = "0";
                            //}
                            PayRate = Convert.ToString(txtHiddenPayRate.Value);
                            string HourlyRate = "";
                            string HourlyRateMode = "";
                            if (rdoMOMHourlyRate.Checked)
                            {
                                HourlyRate = txtHourlyRate.Value;
                                HourlyRateMode = "A";

                            }
                            if (rdoManualHourlyRate.Checked)
                            {
                                HourlyRate = txtMannualHourlyRate.Value;
                                HourlyRateMode = "M";
                            }

                            string DailyRate = "";
                            string DailyRateMode = "";
                            if (rdoMOMDailyRate.Checked)
                            {
                                DailyRate = txtDailyRate.Value;
                                DailyRateMode = "A";
                            }
                            if (rdoMannualDailyRate.Checked)
                            {
                                DailyRate = txtMannualDailyRate.Value;
                                DailyRateMode = "M";
                            }

                            string wday_per_week = cmbworkingdays.Value;
                            string PayMode = cmbPayMode.Value;
                            string EmailPaySlip = cmbEmailPaySlip.Value;

                            string FWLCode = txtFWLCode.Value;
                            string MonthlyLevy = txtMonthlyLevy.Value;
                            string WithHoldTaxPct = "";
                            string WithHoldTaxAmt = "";
                            string SDFRequired = "";
                            if (chkSDFRequired.Checked == true) SDFRequired = "1"; else SDFRequired = "2";

                            string CDAC = "-1";
                            string MBMF = "-1";
                            string SINDA = "-1";
                            string ECF = "-1";
                            string CCHEST = "-1";
                            if (txtFundType.Value == "CDAC")
                                CDAC = Utility.ToString(txtFundAmount.Value);
                            if (txtFundType.Value == "MBMF")
                                MBMF = Utility.ToString(txtFundAmount.Value);
                            if (txtFundType.Value == "SINDA")
                                SINDA = Utility.ToString(txtFundAmount.Value);
                            if (txtFundType.Value == "ECF")
                                ECF = Utility.ToString(txtFundAmount.Value);

                            int optout = 0;
                            int cpffh = 0;
                            int fundfh = 0;
                            int salhalf = 0;


                        if (IsCalculate)
                        {
                                if (chkoptfund.Checked)
                                    optout = 1;
                                if (chkcomputecpffh.Checked)
                                    cpffh = 1;
                                if (chkFUNDRequired.Checked)
                                    fundfh = 1;
                                salhalf = Convert.ToInt32(ddlHalfSal.SelectedValue.ToString());
                            }
                            //if (chkboxhalf.Checked)

                           

                            string Remarks = txtRemarks.Value;
                            string Img = "";
                            if (RadUpload1.UploadedFiles.Count > 0)
                            {
                                Img = varFileName;
                            }
                            else
                            {
                                int intindexlastof = Image1.Src.ToString().LastIndexOf("/");
                                if (intindexlastof > 0)
                                {
                                    if (Image1.Src.ToString().Length > (intindexlastof + 1))
                                    {
                                        if (Image1.Src != "../Frames/Images/Employee/employee.png")
                                        {
                                            Img = Image1.Src.ToString().Substring(intindexlastof + 1);
                                        }
                                        else
                                        {
                                            Img = "";
                                        }
                                    }
                                    else
                                    {
                                        Img = "";
                                    }
                                }
                            }

                            string emplname = txtlastname.Value;
                            string empcpfgroup = cmbempcpfgroup.Value;
                            string insurance = txtinsurance.Value;
                            string insurance_exp = "";
                            if (rdinsurance.IsEmpty == false)
                                insurance_exp = rdinsurance.SelectedDate.Value.ToString();
                            string csoc = txtcsoc.Value;

                            string csoc_exp = "";
                            if (rdcsoc.IsEmpty == false)
                                csoc_exp = rdcsoc.SelectedDate.Value.ToString();
                            string passport = txtpassport.Value;

                            string passport_exp = "";
                            if (rdpassport.IsEmpty == false)
                                passport_exp = rdpassport.SelectedDate.Value.ToString();
                            string probation = cmbprobation.SelectedValue;
                            string giroaccname = txtgiroaccountname.Value;
                            string usergroupid = cmbUserGroup.Value;
                            string localadd2 = txtlocal2.Value;
                            string block = txtblock.Value;
                            string unit = txtunit.Value;
                            string level = txtlevel.Value;
                            string street = txtstreet.Value;
                            string foreignadd1 = txtforeign1.Value;
                            string foreignadd2 = txtforeign2.Value;
                            string TimeCardNo = txttimecardno.Value;
                            string fpostalcode = txtforeignpostalcode.Value;
                            string ppissuedate = "";
                            if (rdppissuedate.IsEmpty == false)
                                ppissuedate = rdppissuedate.SelectedDate.Value.ToString();
                            string wpappdate = "";
                            if (rdwpappdate.IsEmpty == false)
                                wpappdate = rdwpappdate.SelectedDate.Value.ToString();

                            double leaves_remaining1 = Utility.ToDouble(txtRemainingLeaves.Value);
                            double hleaves = Utility.ToDouble(txthdnleaves.Value);
                            if (leaves_remaining1 == 0)
                            {
                                leaves_remainings = leaves_remaining1;
                            }
                            else
                            {

                                if (hleaves < leaves_remaining1)
                                {
                                    string ssqlb = "Select * From leaves_annual  Where emp_id='" + emp_code + "' Order By Leave_Year";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    if (ds.Tables[0].Rows.Count > 0)
                                    {
                                        ssqlb = "update leaves_annual set leave_remaining='" + leaves_remaining1 + "' where leave_year = (Select max(leave_year) from leaves_annual where emp_id='" + emp_code + "') and emp_id='" + emp_code + "'";
                                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    }
                                    else
                                    {
                                        ssqlb = "Insert Into leaves_annual (emp_id, leave_remaining, leave_year) values ('" + emp_code + "'," + leaves_remaining1 + ",'" + DateTime.Now.Year.ToString() + "')";
                                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    }

                                }
                                else if (hleaves > leaves_remaining1)
                                {
                                    string ssqlb = "Select * From leaves_annual  Where emp_id='" + emp_code + "' Order By Leave_Year";
                                    DataSet dsLeaves = new DataSet();
                                    dsLeaves = DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                    double cntleaveavail = hleaves - leaves_remaining1;
                                    foreach (DataRow theRow in dsLeaves.Tables[0].Rows)
                                    {
                                        if (cntleaveavail > 0)
                                        {
                                            int intleave = Utility.ToInteger(theRow["leave_remaining"].ToString());
                                            if (cntleaveavail <= intleave)
                                            {
                                                double updateleave = intleave - cntleaveavail;
                                                string sqlupdate = "update leaves_annual set leave_remaining=" + updateleave + " where row_id =" + theRow["row_id"].ToString();
                                                DataAccess.FetchRS(CommandType.Text, sqlupdate, null);
                                                cntleaveavail = 0;
                                            }
                                            if (cntleaveavail > intleave)
                                            {
                                                double updateleave = cntleaveavail - intleave;
                                                cntleaveavail = updateleave;
                                                string sqlupdate = "update leaves_annual set leave_remaining=0 where row_id =" + theRow["row_id"];
                                                DataAccess.FetchRS(CommandType.Text, sqlupdate, null);
                                            }
                                        }
                                    }
                                }
                            }

                            string emp_ref_type = "";
                            if ((cmbEmpType.Value == "SPR") || (cmbEmpType.Value == "SDPR") || (cmbEmpType.Value == "SC"))
                            {

                                // string emp_ref_type = Utility.ToString(cmbEmpRefType.Value);

                                emp_ref_type = Utility.ToString(1);
                            }
                            else if ((cmbEmpType.Value == "WP") || (cmbEmpType.Value == "EP") || (cmbEmpType.Value == "SP") || (cmbEmpType.Value == "DP") || (cmbEmpType.Value == "NA") || (cmbEmpType.Value == "OT"))
                            {
                                emp_ref_type = Utility.ToString(2);

                            }
                            int emp_category = Utility.ToInteger(cmbEmpCategory.Value);
                            //Added By Raja(24/11/2008)
                            string v1rate = txtv1rate.Text;
                            string v2rate = txtv2rate.Text;
                            string v3rate = txtv3rate.Text;
                            string v4rate = txtv4rate.Text;
                            /* USE BELOW SECTION FOR CLAVON */
                            int batch_no = Utility.ToInteger(txtBatchNo.Value);
                            string wp_issuedate = "";
                            string wp_arrivaldate = "";
                            if (rdwpissuedate.IsEmpty == false)
                                wp_issuedate = rdwpissuedate.SelectedDate.Value.ToString();
                            if (rdWPArrDate.IsEmpty == false)
                                wp_arrivaldate = rdWPArrDate.SelectedDate.Value.ToString();
                            string shipyard_quota = Utility.ToString(txtShipyardQuota.Value);
                            int photo = Utility.ToInteger(txtPhotoNo.Value);
                            string wpNumber = Utility.ToString(txtWPNumber.Value);

                            object obj1_tax_borne_employer = cmbtaxbornbyemployer.Value;
                            object obj2_tax_borne_employer_options = cmbtaxbornbyemployerFPHN.Value;
                            object obj3_tax_borne_employer_amount = txttaxbornbyempamt.Value;
                            object obj4_pension_out_singapore = cmbpensionoutsing.Value;
                            object obj5_pension_out_singapore_amount = txtpensionoutsing.Value;
                            object obj6_excess_voluntary_cpf_employer = cmbexcessvolcpfemp.Value;
                            object obj7_excess_voluntary_cpf_employer_amount = txtexcessvolcpfemp.Value;
                            object obj8_stock_options = cmbstockoption.Value;
                            object obj9_stock_options_amount = txtstockoption.Value;
                            object obj10_benefits_in_kind = cmbbenefitskind.Value;
                            object obj11_benefits_in_kind_amount = txtbenefitskind.Value;
                            object obj12_retirement_benefits = cmbretireben.Value;
                            object obj13_retirement_benefits_fundName = txtretirebenfundname.Value;
                            object obj14_retirement_benefits_amount = txtbretireben.Value;
                            object obj15_s45_tax_on_directorFee = staxondirector.Value;
                            object obj16_cessation_provision = cmbcessprov.Value;
                            object obj17_addr_type = cmbaddress.SelectedItem.Value;
                            object obj18_dateofcessationconv = dtcessdate.Value;
                            object obj19_dateofcommencementconv = dtcommdate.Value;


                            //if (DateofBirth.ToString() != "")
                            //{
                            //    DateTime dtDOB = DateTime.Parse(DateofBirth);
                            //    DateTime today = DateTime.Now;

                            //    TimeSpan ts = today.Subtract(dtDOB);
                            //    double days = (double)ts.TotalHours / (24);
                            //    double months = Math.Round(days / 30.4, 2);
                            //    int years = (int)months / 12;

                            //    if (years < 13)
                            //    {
                            //        ShowMessageBox("Job Info-Employee should not be below 13 years.\n");
                            //        //string script = "alert('Job Info-Employee should not be below 13 years.\n');";
                            //        //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                            //        return;
                            //    }

                            //}

                            if (EmpAlias != "" || EmpAlias != null)
                            {
                                string strSQLUser = "Select UserName from Users where Company_Id=" + Utility.ToInteger(compid) + "";
                                DataSet dsUser = new DataSet();
                                dsUser = DataAccess.FetchRS(CommandType.Text, strSQLUser, null);

                                DataRow[] foundRows;
                                foundRows = dsUser.Tables[0].Select("UserName='" + EmpAlias + "'");

                                if (foundRows.Length > 0)
                                {
                                    ShowMessageBox("Employee Alias already have in Users, please define another name.");
                                    return;
                                }
                            }
                            string _ScanCode = "";
                            if (!string.IsNullOrEmpty(this.ScanCode.Value.ToString()))
                            {
                                _ScanCode = this.ScanCode.Value.ToString();
                            }

                            int i = 0;
                            SqlParameter[] parms = new SqlParameter[143];
                            parms[i++] = new SqlParameter("@emp_code", Utility.ToInteger(employeecode));
                            parms[i++] = new SqlParameter("@emp_name", Utility.ToString(EmpName));
                            parms[i++] = new SqlParameter("@emp_alias", Utility.ToString(EmpAlias));
                            parms[i++] = new SqlParameter("@nationality", Utility.ToInteger(Nationality));
                            parms[i++] = new SqlParameter("@emp_type", Utility.ToString(emptype));
                            parms[i++] = new SqlParameter("@ScanCode", Utility.ToString(_ScanCode));
                            parms[i++] = new SqlParameter("@emp_lname", Utility.ToString(emplname));
                            parms[i++] = new SqlParameter("@empcpftype", Utility.ToInteger(empcpfgroup));
                            parms[i++] = new SqlParameter("@Insurance_number", Utility.ToString(insurance));
                            parms[i++] = new SqlParameter("@Insurance_expiry", Utility.ToString(insurance_exp));
                            parms[i++] = new SqlParameter("@CSOC_number", Utility.ToString(csoc));
                            parms[i++] = new SqlParameter("@CSOC_expiry", Utility.ToString(csoc_exp));
                            parms[i++] = new SqlParameter("@passport", Utility.ToString(passport));
                            parms[i++] = new SqlParameter("@passport_expiry", Utility.ToString(passport_exp));
                            parms[i++] = new SqlParameter("@ic_pp_number", Utility.ToString(ICPPNumber));
                            parms[i++] = new SqlParameter("@wp_exp_date", Utility.ToString(WPExpDate));
                            parms[i++] = new SqlParameter("@pr_date", Utility.ToString(PRDate));
                            parms[i++] = new SqlParameter("@address", Utility.ToString(Address));
                            parms[i++] = new SqlParameter("@country", Utility.ToInteger(Country));
                            parms[i++] = new SqlParameter("@postal_code", Utility.ToString(PostalCode));
                            parms[i++] = new SqlParameter("@phone", Utility.ToString(Phone));
                            parms[i++] = new SqlParameter("@hand_phone", Utility.ToString(HandPhone));
                            parms[i++] = new SqlParameter("@email", Utility.ToString(Email));
                            parms[i++] = new SqlParameter("@sex", Utility.ToString(Sex));
                            parms[i++] = new SqlParameter("@religion", Utility.ToInteger(Religion));
                            parms[i++] = new SqlParameter("@race", Utility.ToInteger(Race));
                            parms[i++] = new SqlParameter("@marital_status", Utility.ToString(MaritalStatus));
                            parms[i++] = new SqlParameter("@place_of_birth", Utility.ToString(PlaceofBirth));
                            parms[i++] = new SqlParameter("@date_of_birth", Utility.ToString(DateofBirth));
                            parms[i++] = new SqlParameter("@income_taxid", Utility.ToString(IncomeTaxID));

                            parms[i++] = new SqlParameter("@employee_cpf_acct", Utility.ToString(EmployeeCPFAcctNumber));
                            parms[i++] = new SqlParameter("@employer_cpf_acct", Utility.ToString(EmployerCPFAcctNumber));
                            parms[i++] = new SqlParameter("@giro_bank", Utility.ToString(GIROBank));
                            parms[i++] = new SqlParameter("@giro_code", Utility.ToString(GIROCode));
                            parms[i++] = new SqlParameter("@giro_branch", Utility.ToString(GIROBranch));
                            parms[i++] = new SqlParameter("@giro_acct_number", Utility.ToString(GIROAccountNo));
                            parms[i++] = new SqlParameter("@cpf_entitlement", Utility.ToString(CPFEntitlement));
                            parms[i++] = new SqlParameter("@cpf_employer", Utility.ToDouble(EmployerCPF));
                            parms[i++] = new SqlParameter("@department", Utility.ToInteger(Department));
                            parms[i++] = new SqlParameter("@cpf_employee", Utility.ToDouble(EmployeeCPF));
                            parms[i++] = new SqlParameter("@designation", Utility.ToInteger(Designation));
                            parms[i++] = new SqlParameter("@trade", Utility.ToInteger(Trade));
                            parms[i++] = new SqlParameter("@emp_supervisor", Utility.ToInteger(supervisor));
                            parms[i++] = new SqlParameter("@emp_clsupervisor", Utility.ToInteger(clsupervisor));
                            parms[i++] = new SqlParameter("@education", Utility.ToString(Education));
                            parms[i++] = new SqlParameter("@joining_date", Utility.ToString(JoiningDate));
                            parms[i++] = new SqlParameter("@confirmation_date", Utility.ToString(ConfirmationDate));
                            parms[i++] = new SqlParameter("@empgroup", Utility.ToInteger(EmployeeGroup));
                            parms[i++] = new SqlParameter("@ot_entitlement", Utility.ToString(OTEntitled));
                            parms[i++] = new SqlParameter("@termination_date", Utility.ToString(TerminationDate));
                            parms[i++] = new SqlParameter("@termination_reason", Utility.ToString(terminreason));
                            parms[i++] = new SqlParameter("@pay_frequency", Utility.ToString(PayFrequency));
                            parms[i++] = new SqlParameter("@payrate", PayRate);
                            parms[i++] = new SqlParameter("@payment_mode", Utility.ToString(PayMode));
                            parms[i++] = new SqlParameter("@hourlyRate", Utility.ToDouble(HourlyRate));
                            parms[i++] = new SqlParameter("@hourlyRateMode", Utility.ToString(HourlyRateMode));
                            parms[i++] = new SqlParameter("@dailyRateMode", Utility.ToString(DailyRateMode));
                            parms[i++] = new SqlParameter("@dailyRate", Utility.ToDouble(DailyRate));
                            parms[i++] = new SqlParameter("@wday_per_week", Utility.ToDouble(wday_per_week));
                            parms[i++] = new SqlParameter("@email_payslip", Utility.ToString(EmailPaySlip));
                            parms[i++] = new SqlParameter("@fw_code", Utility.ToString(FWLCode));
                            parms[i++] = new SqlParameter("@fw_levy", Utility.ToDouble(MonthlyLevy));
                            parms[i++] = new SqlParameter("@wh_tax_pct", Utility.ToDouble(WithHoldTaxPct));
                            parms[i++] = new SqlParameter("@wh_tax_amt", Utility.ToDouble(WithHoldTaxAmt));
                            parms[i++] = new SqlParameter("@sdf_required", SDFRequired);
                            parms[i++] = new SqlParameter("@cdac_fund", Utility.ToDouble(CDAC));
                            parms[i++] = new SqlParameter("@mbmf_fund", Utility.ToDouble(MBMF));
                            parms[i++] = new SqlParameter("@sinda_fund", Utility.ToDouble(SINDA));
                            parms[i++] = new SqlParameter("@ecf_fund", Utility.ToDouble(ECF));
                            parms[i++] = new SqlParameter("@cchest_fund", Utility.ToDouble(CCHEST));
                            parms[i++] = new SqlParameter("@fund_optout", Utility.ToInteger(optout));
                            parms[i++] = new SqlParameter("@remarks", Utility.ToString(Remarks));
                            parms[i++] = new SqlParameter("@Images", Utility.ToString(Img));
                            parms[i++] = new SqlParameter("@probation_period", Utility.ToInteger(probation));
                            parms[i++] = new SqlParameter("@leaveCarryForward", "");
                            parms[i++] = new SqlParameter("@giro_acc_name", Utility.ToString(giroaccname));
                            parms[i++] = new SqlParameter("@groupid", Utility.ToString(usergroupid));
                            parms[i++] = new SqlParameter("@localaddress2", Utility.ToString(localadd2));
                            parms[i++] = new SqlParameter("@block_no", Utility.ToString(block));
                            parms[i++] = new SqlParameter("@unit_no", Utility.ToString(unit));
                            parms[i++] = new SqlParameter("@level_no", Utility.ToString(level));
                            parms[i++] = new SqlParameter("@street_name", Utility.ToString(street));
                            parms[i++] = new SqlParameter("@foreignaddress1", Utility.ToString(foreignadd1));
                            parms[i++] = new SqlParameter("@foreignaddress2", Utility.ToString(foreignadd2));
                            parms[i++] = new SqlParameter("@time_card_no", Utility.ToString(TimeCardNo));
                            parms[i++] = new SqlParameter("@foreignpostalcode", Utility.ToString(fpostalcode));
                            parms[i++] = new SqlParameter("@pp_issue_date", Utility.ToString(ppissuedate));
                            parms[i++] = new SqlParameter("@leaves_remaining", leaves_remainings);
                            parms[i++] = new SqlParameter("@worker_levy", cmblevy.Value);
                            parms[i++] = new SqlParameter("@wp_application_date", wpappdate);
                            parms[i++] = new SqlParameter("@emp_ref_type", Utility.ToInteger(emp_ref_type));
                            parms[i++] = new SqlParameter("@emp_category", Utility.ToInteger(emp_category));
                            parms[i++] = new SqlParameter("@v1rate", Utility.ToString(v1rate));
                            parms[i++] = new SqlParameter("@v2rate", Utility.ToString(v2rate));
                            parms[i++] = new SqlParameter("@v3rate", Utility.ToString(v3rate));
                            parms[i++] = new SqlParameter("@v4rate", Utility.ToString(v4rate));
                            parms[i++] = new SqlParameter("@batch_no", Utility.ToInteger(batch_no));
                            parms[i++] = new SqlParameter("@wp_issue_date", Utility.ToString(wp_issuedate));
                            parms[i++] = new SqlParameter("@shipyard_quota", Utility.ToString(shipyard_quota));
                            parms[i++] = new SqlParameter("@photo_no", Utility.ToInteger(photo));
                            parms[i++] = new SqlParameter("@wp_number", Utility.ToString(wpNumber));
                            parms[i++] = new SqlParameter("@tax_borne_employer", Utility.ToString(obj1_tax_borne_employer));
                            parms[i++] = new SqlParameter("@tax_borne_employer_options", Utility.ToString(obj2_tax_borne_employer_options));
                            parms[i++] = new SqlParameter("@tax_borne_employer_amount", Utility.ToString(obj3_tax_borne_employer_amount));
                            parms[i++] = new SqlParameter("@pension_out_singapore", Utility.ToString(obj4_pension_out_singapore));
                            parms[i++] = new SqlParameter("@pension_out_singapore_amount", Utility.ToString(obj5_pension_out_singapore_amount));
                            parms[i++] = new SqlParameter("@excess_voluntary_cpf_employer", Utility.ToString(obj6_excess_voluntary_cpf_employer));
                            parms[i++] = new SqlParameter("@excess_voluntary_cpf_employer_amount", Utility.ToString(obj7_excess_voluntary_cpf_employer_amount));
                            parms[i++] = new SqlParameter("@stock_options", Utility.ToString(obj8_stock_options));
                            parms[i++] = new SqlParameter("@stock_options_amount", Utility.ToString(obj9_stock_options_amount));
                            parms[i++] = new SqlParameter("@benefits_in_kind", Utility.ToString(obj10_benefits_in_kind));
                            parms[i++] = new SqlParameter("@benefits_in_kind_amount", Utility.ToString(obj11_benefits_in_kind_amount));
                            parms[i++] = new SqlParameter("@retirement_benefits", Utility.ToString(obj12_retirement_benefits));
                            parms[i++] = new SqlParameter("@retirement_benefits_fundName", Utility.ToString(obj13_retirement_benefits_fundName));
                            parms[i++] = new SqlParameter("@retirement_benefits_amount", Utility.ToString(obj14_retirement_benefits_amount));
                            parms[i++] = new SqlParameter("@s45_tax_on_directorFee", Utility.ToString(obj15_s45_tax_on_directorFee));
                            parms[i++] = new SqlParameter("@cessation_provision", Utility.ToString(obj16_cessation_provision));
                            parms[i++] = new SqlParameter("@addr_type", Utility.ToString(obj17_addr_type));
                            parms[i++] = new SqlParameter("@dateofcessationconv", Utility.ToString(obj18_dateofcessationconv));
                            parms[i++] = new SqlParameter("@dateofcommencementconv", Utility.ToString(obj19_dateofcommencementconv));
                            parms[i++] = new SqlParameter("@ir8a_year", Utility.ToString(cmbIR8A_year.Value));
                            parms[i++] = new SqlParameter("@ot1rate", Utility.ToDouble(ot1rate));
                            parms[i++] = new SqlParameter("@ot2rate", Utility.ToDouble(ot2rate));
                            parms[i++] = new SqlParameter("@EmeConPer", Utility.ToString(strtxtEmeConPer));
                            parms[i++] = new SqlParameter("@EmeConPerRel", Utility.ToString(strtxtEmeConPerRel));
                            parms[i++] = new SqlParameter("@EmeConPerPh1", Utility.ToString(strtxtEmeConPerPh1));
                            parms[i++] = new SqlParameter("@EmeConPerPh2", Utility.ToString(strtxtEmeConPerPh2));
                            parms[i++] = new SqlParameter("@EmeConPerAdd", Utility.ToString(strtxtEmeConPerAdd));
                            parms[i++] = new SqlParameter("@EmeConPerRem", Utility.ToString(strtxtEmeConPerRem));
                            parms[i++] = new SqlParameter("@bloodgroup", Utility.ToString(strtxtBloodGroup));
                            parms[i++] = new SqlParameter("@agent_id", Utility.ToString(stragent_id));
                            parms[i++] = new SqlParameter("@mye_cert_id", Utility.ToString(strmye_cert_id));
                            parms[i++] = new SqlParameter("@wp_arrival_date", Utility.ToString(wp_arrivaldate));
                            parms[i++] = new SqlParameter("@pay_supervisor", Utility.ToInteger(pay_supervisor));
                            parms[i++] = new SqlParameter("@payrolltype", Utility.ToInteger(ddlPayrollType.SelectedValue.ToString()));
                            parms[i++] = new SqlParameter("@compcpffh", Utility.ToInteger(cpffh));
                            parms[i++] = new SqlParameter("@tssupervisor", Utility.ToInteger(tssupervisor));
                            parms[i++] = new SqlParameter("@compfundfh", Utility.ToInteger(fundfh));
                            parms[i++] = new SqlParameter("@halfsalary", Utility.ToInteger(salhalf));
                            parms[i++] = new SqlParameter("@Leave_supervisor", Utility.ToInteger(leave_supervisor));

                            parms[i++] = new SqlParameter("@PaymentType", Utility.ToInteger(radListPayType.SelectedValue));
                            parms[i++] = new SqlParameter("@CurrencyBank", Utility.ToInteger(drpCurrBank1.SelectedValue));

                            parms[i++] = new SqlParameter("@PaymentPart", Utility.ToInteger(drpPaymentMode.SelectedValue));
                            string sSQL = "sp_emp_update";

                            int retVal = 0;
                            try
                            {
                                strMsg = "";
                                if (activeSuperAdminCount > 1 && TerminationDate != "")
                                {
                                    retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                                    if (retVal >= 1)
                                    {
                                        strMsg = "Employee Information Updated Successfully.";
                                    }
                                }
                                else if (activeSuperAdminCount == 1 && TerminationDate != "")
                                {
                                    strMsg = "Termination Date cannot be updated for the ONLY SuperAdmin user in the system.";
                                }
                                else
                                {
                                    retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                                    if (retVal != -1)
                                    {
                                        strMsg = "Employee Information Updated Successfully.";
                                    }
                                    else
                                    {
                                        strMsg = "Employee Alias already exists. Please choose a different alias name.";
                                    }
                                }
                                lblerror.ForeColor = System.Drawing.Color.Green;
                                Response.Redirect("Employee.aspx?msg=" + strMsg);
                            }
                            catch (Exception ex)
                            {
                                lblerror.ForeColor = System.Drawing.Color.Red;
                                string ErrMsg = ex.Message;
                                lblerror.Text = ErrMsg;
                            }
                        }
                    }
                }
            }
        }

        private void SaveCosting()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["empcode"]))
            {
                string sSQL = "update employee set Cost_Businessunit='" + cmbbusinessunit.Value + "' ,Cost_Region='" + cmbRegion.Value + "',[Cost_Category]='" + cmbCategory.Value + "' where emp_code=" + Convert.ToString(Request.QueryString["empcode"]);
                DataAccess.ExecuteStoreProc(sSQL);
            }
        }




            #endregion Insert&Update

        [AjaxPro.AjaxMethod]
        public string CPFChange(string cpf, string PRDate, int empgroup, string dob)
        {
            string valResult = ",";

            if (PRDate != null)
            {
                if (cpf == "Y")
                {
                    string SQL = "select dbo.GetEmployerCPFPCT (" + empgroup + ", datediff(mm,convert(varchar(15),'" + Utility.ToDate(dob) + "',108),getdate())/12,dbo.fn_getPRYears_Emp(convert(varchar(15),'" + Utility.ToDate(PRDate) + "',108)))";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                    if (dr.Read())
                    {
                        valResult = dr[0].ToString();
                    }
                    dr.Close();
                    SQL = "select dbo.GetEmployeeCPFPCT (" + empgroup + ", datediff(mm,convert(varchar(15),'" + Utility.ToDate(dob) + "',108),getdate())/12,dbo.fn_getPRYears_Emp(convert(varchar(15),'" + Utility.ToDate(PRDate) + "',108)))";
                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                    if (dr1.Read())
                    {
                        valResult += "," + dr1[0].ToString();

                    }
                    dr1.Close();
                }
            }
            return Utility.ToString(valResult);
        }

        [AjaxPro.AjaxMethod]
        public string calculate_fund(string payfrequency, string emptype, int payrate, string race)
        {
            string valResult = "";
            if (payfrequency == "M" && (emptype == "SPR" || emptype == "SDPR" || emptype == "SC"))
            {
                string SQL = "";
                if (payrate.ToString() != "")
                {
                    string sRaceVal = "";
                    if (race.ToString().Trim() == "CHINESE")
                        sRaceVal = "CDAC";
                    if (race.ToString().Trim() == "INDIAN")
                        sRaceVal = "SINDA";
                    if (race.ToString().Trim() == "MALAY" || race.ToString().Trim() == "MALAYSIAN")
                        sRaceVal = "MBMF";
                    if (race.ToString().Trim() == "EURASIAN")
                        sRaceVal = "ECF";

                    SQL = "select dbo.GetEmpFundContribution (" + payrate + ",'" + sRaceVal + "')";
                }
                else
                {
                    SQL = "select dbo.fnGetFundAmt (" + 0.0000 + ",'" + race + "')";
                }
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
                if (dr1.Read())
                    valResult = dr1[0].ToString();
                dr1.Close();
            }
            return Utility.ToString(valResult);
        }
        [AjaxPro.AjaxMethod]
        public string calculate_DailyRate(float payrate, float workingdays)
        {
            string valResult = "";
            string SQL = "select dbo.fn_GetDailyRate(" + payrate + "," + workingdays + ")";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                valResult = Utility.ToString(dr.GetValue(0));
            }
            return Utility.ToString(valResult);
        }
        [AjaxPro.AjaxMethod]
        public   string NRIC_Check(string nricType, string nricVal)
        {
            nricVal = nricVal.ToUpper();
            string retVal = "no";
            if (nricType == "5")
            {
                retVal = "yes";
                return retVal;
            }
            else
            {
                if (nricType == "1")
                {
                    if (nricVal.Substring(0, 1) != "S" && nricVal.Substring(0, 1) != "T")
                    {
                        retVal = "no";
                        return retVal;
                    }

                }
                if (nricType == "2" || nricType == "3" || nricType == "4")
                {
                    if (nricVal.Substring(0, 1) != "F" && nricVal.Substring(0, 1) != "G")
                    {
                        retVal = "no";
                        return retVal;
                    }
                }
                string sSQL = "sp_NRIC_Check";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@sname", Utility.ToString(nricVal));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
                while (dr.Read())
                {
                    retVal = Utility.ToString(dr.GetValue(0));
                }
                return retVal;
            }
        }


        protected void nric_change(object source, EventArgs e)
        {
            if (txtnricno.Text != null)
            {
                txtEmployeeCPFAcctNumber.Value = txtnricno.Text;
                txtIncomeTaxID.Value = txtnricno.Text;
                txtICPPNumber.Value = txtnricno.Text;
            }
        }

        void EmpAutoAddANB()
        {
            bln = true;
            if (EmpCode != "")
            {
                double ot1rate = Utility.ToDouble(txtOT1Rate.Text);
                double ot2rate = Utility.ToDouble(txtOT2Rate.Text);

                string strtxtEmeConPer = txtEmeConPer.Text.ToString().Trim();
                string strtxtEmeConPerRel = txtEmeConPerRel.Text.ToString().Trim();
                string strtxtEmeConPerPh1 = txtEmeConPerPh1.Text.ToString().Trim();
                string strtxtEmeConPerPh2 = txtEmeConPerPh2.Text.ToString().Trim();
                string strtxtEmeConPerAdd = txtEmeConPerAdd.Text.ToString().Trim();
                string strtxtEmeConPerRem = txtEmeConPerRem.Text.ToString().Trim();
                string strtxtBloodGroup = txtBloodGroup.Text.ToString().Trim();
                string strmye_cert_id = "";
                string stragent_id = "";

                if (cmbAgent.Value != "-select-")
                {
                    stragent_id = cmbAgent.Value;
                }
                if (cmbMYE.Value != "-select-")
                {
                    strmye_cert_id = cmbMYE.Value;
                }
                string wp_arrivaldate = "";
                if (rdWPArrDate.IsEmpty == false)
                    wp_arrivaldate = rdWPArrDate.SelectedDate.Value.ToString();

                string pay_supervisor = cmbpayrollsupervisor.Value;
                string leave_supervisor;

                if (cmbLeaveApproval.Value == "-select-")
                {
                    leave_supervisor = "-1";
                }
                else
                {

                    leave_supervisor = cmbLeaveApproval.Value;

                }

                string EmpAlias = txtEmpAlias.Value;
                string Nationality = cmbNationality.Value;
                string companycode = compcode;
                string emptype = cmbEmpType.Value;
                string ICPPNumber = txtnricno.Text;
                string WPExpDate = "";
                if (rdWPExpDate.IsEmpty == false)
                    WPExpDate = rdWPExpDate.SelectedDate.Value.ToString();
                string PRDate = "";
                if (rdPRDate.IsEmpty == false)
                    PRDate = rdPRDate.SelectedDate.Value.ToString();
                string Phone = txtPhone.Value;
                string HandPhone = txtHandPhone.Value;
                string Address = txtAddress.Value;
                string Country = cmbCountry.Value;
                string PostalCode = txtPostalCode.Value;
                string Email = txtEmail.Value;
                string Sex = cmbSex.Value;
                string block = txtblock.Value;
                string unit = txtunit.Value;
                string level = txtlevel.Value;
                string street = txtstreet.Value;
                string Religion = cmbReligion.Value;
                string Race = cmbRace.Value;
                string MaritalStatus = cmbMaritalStatus.Value;
                string PlaceofBirth = cmbbirthplace.Value;
                string IncomeTaxID = txtIncomeTaxID.Value;
                string EmployeeCPFAcctNumber = txtEmployeeCPFAcctNumber.Value;
                string EmployerCPFAcctNumber = cmbEmployerCPFAcctNumber.Items[cmbEmployerCPFAcctNumber.SelectedIndex].Text.ToString();
                string GIROBank = txtgirobankname.Value;
                string GIROCode = "0";
                string GIROBranch = txtgirobranch.Value;
                string GIROAccountNo = txtGIROAccountNo.Value;
                string CPFEntitlement = cmbCPFEntitlement.Value;

                string Department = cmbDepartment.Value;
                string EmployerCPF = txtEmployerCPF_H.Value;
                string EmployeeCPF = txtEmployeeCPF_H.Value;
                string Designation = cmbDesignation.Value;
                string Trade = cmbTrade.Value;
                string supervisor = cmbsupervisor.Value;

                //if (cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Text != "-select-")
                //{
                //    supervisor = cmbLeaveApproval.Items[cmbLeaveApproval.SelectedIndex].Value;
                //}

                string clsupervisor = cmbclaimsupervisor.Value;
                string tssupervisor = cmbtimesheetsupervisor.Value;
                string Education = cmbeducation.Value;
                string JoiningDate = "";
                if (rdJoiningDate.IsEmpty == false)
                    JoiningDate = rdJoiningDate.SelectedDate.Value.ToString();

                string ConfirmationDate = "";
                if (rdConfirmationDate.IsEmpty == false)
                    ConfirmationDate = rdConfirmationDate.SelectedDate.Value.ToString();

                string EmployeeGroup = cmbEmployeeGroup.Value;
                string OTEntitled = cmbOTEntitled.Value;
                string TerminationDate = "";
                if (rdTerminationDate.IsEmpty == false)
                    TerminationDate = rdTerminationDate.SelectedDate.Value.ToString();
                string terminreason = txtterminreason.Value;
                string PayFrequency = rbpayfrequency.SelectedValue;
                string PayRate = txtPayRate.Text;
                string HourlyRate = "";
                if (rdoMOMHourlyRate.Checked)
                {
                    HourlyRate = txtHourlyRate.Value;
                }
                if (rdoManualHourlyRate.Checked)
                {
                    HourlyRate = txtMannualHourlyRate.Value;
                }


                string DailyRate = "";
                if (rdoMOMDailyRate.Checked)
                {
                    DailyRate = txtDailyRate.Value;
                }
                if (rdoMannualDailyRate.Checked)
                {
                    DailyRate = txtMannualDailyRate.Value;
                }
                string wday_per_week = cmbworkingdays.Value;
                string PayMode = cmbPayMode.Value;
                string EmailPaySlip = cmbEmailPaySlip.Value;
                if (PayMode == "-select-")
                {
                    PayMode = "-2";
                }

                string FWLCode = txtFWLCode.Value;
                string MonthlyLevy = txtMonthlyLevy.Value;
                string WithHoldTaxPct = "";
                string WithHoldTaxAmt = "";
                string SDFRequired = "";
                if (chkSDFRequired.Checked == true) SDFRequired = "1"; else SDFRequired = "0";

                string CDAC = "-1";
                string MBMF = "-1";
                string SINDA = "-1";
                string ECF = "-1";
                string CCHEST = "-1";
                if (txtFundType.Value == "CDAC")
                    CDAC = Utility.ToString(txtFundAmount.Value);
                if (txtFundType.Value == "MBMF")
                    MBMF = Utility.ToString(txtFundAmount.Value);
                if (txtFundType.Value == "SINDA")
                    SINDA = Utility.ToString(txtFundAmount.Value);
                if (txtFundType.Value == "ECF")
                    ECF = Utility.ToString(txtFundAmount.Value);

                int optout = 0;
                int cpffh = 0;
                int fundfh = 0;
                int salhalf = 0;
                if (chkoptfund.Checked)
                    optout = 1;
                if (chkcomputecpffh.Checked)
                    cpffh = 1;
                if (chkFUNDRequired.Checked)
                    fundfh = 1;
                //if (chkboxhalf.Checked)
                //  salhalf = 1;
                salhalf = Convert.ToInt32(ddlHalfSal.SelectedValue.ToString());
                string Remarks = txtRemarks.Value;
                string Img = varFileName;

                string emplname = txtlastname.Value;
                string empcpfgroup = cmbempcpfgroup.Value;
                string insurance = txtinsurance.Value;
                string insurance_exp = "";
                if (rdinsurance.IsEmpty == false)
                    insurance_exp = rdinsurance.SelectedDate.Value.ToString();
                string csoc = txtcsoc.Value;

                string csoc_exp = "";
                if (rdcsoc.IsEmpty == false)
                    csoc_exp = rdcsoc.SelectedDate.Value.ToString();
                string passport = txtpassport.Value;

                string passport_exp = "";
                if (rdpassport.IsEmpty == false)
                    passport_exp = rdpassport.SelectedDate.Value.ToString();
                string probation = cmbprobation.SelectedValue;
                string giroaccname = txtgiroaccountname.Value;
                string usergroupid = cmbUserGroup.Value;
                string localadd2 = txtlocal2.Value;
                string foreignadd1 = txtforeign1.Value;
                string foreignadd2 = txtforeign2.Value;
                string TimeCardNo = txttimecardno.Value;
                string fpostalcode = txtforeignpostalcode.Value;
                //Added By Raja(24/11/2008)
                string v1rate = txtv1rate.Text;
                string v2rate = txtv2rate.Text;
                string v3rate = txtv3rate.Text;
                string v4rate = txtv4rate.Text;

                string ppissuedate = "";
                if (rdppissuedate.IsEmpty == false)
                    ppissuedate = rdppissuedate.SelectedDate.Value.ToString();
                string wpappdate = "";
                if (rdwpappdate.IsEmpty == false)
                    wpappdate = rdwpappdate.SelectedDate.Value.ToString();

                double leaves_remaining = Utility.ToDouble(txtRemainingLeaves.Value);

                string emp_ref_type = "";
                if ((cmbEmpType.Value == "SPR") || (cmbEmpType.Value == "SDPR") || (cmbEmpType.Value == "SC"))
                {

                    // string emp_ref_type = Utility.ToString(cmbEmpRefType.Value);

                    emp_ref_type = Utility.ToString(1);
                }
                else if ((cmbEmpType.Value == "WP") || (cmbEmpType.Value == "EP") || (cmbEmpType.Value == "SP") || (cmbEmpType.Value == "DP") || (cmbEmpType.Value == "NA") || (cmbEmpType.Value == "OT"))
                {
                    emp_ref_type = Utility.ToString(2);

                }

                int emp_category = Utility.ToInteger(cmbEmpCategory.Value);



                /* USE BELOW SECTION FOR CLAVON */
                int batch_no = Utility.ToInteger(txtBatchNo.Value);
                string wp_issuedate = "";
                if (rdwpissuedate.IsEmpty == false)
                    wp_issuedate = rdwpissuedate.SelectedDate.Value.ToString();
                string shipyard_quota = Utility.ToString(txtShipyardQuota.Value);
                int photo = Utility.ToInteger(txtPhotoNo.Value);
                string wpNumber = Utility.ToString(txtWPNumber.Value);
                int iCountSuccess = 0;
                int iCountFail = 0;
                DataSet dsauto = new DataSet();
                dsauto = getDataSet("Select *,RA.ID RaceID, Re.ID ReligionID, Na.ID NationalityID,Ta.ID TradeID,Da.ID DeptID, De.ID DesgID,Sa.ID SafeID  from EmployeeBulkImport  E Left Outer Join Race Ra On upper(Ra.Race) = Upper(E.Race) Left Outer Join Religion Re On upper(Re.Religion) = Upper(E.Religion) Left Outer Join nationality Na On upper(Na.nationality) = Upper(E.nationality) Left Outer Join Trade Ta On upper(Ta.Trade) = Upper(E.Trade) Left Outer Join Department Da On upper(Da.DeptName) = Upper(E.Department) Left Outer Join Designation De On upper(De.Designation) = Upper(E.Designation) Left Outer Join Safety_Pass Sa ON Upper(Sa.Safety_Type) = Upper(E.Safetytype1) Where E.Deleted=3 And E.CompanyID= " + compid + " And ((Ta.Company_ID= " + compid + "  Or Ta.Company_ID is null) And (Da.Company_ID= " + compid + "  Or Da.Company_ID is null)  And (De.Company_ID= " + compid + "  Or De.Company_ID is null) And (Sa.CompanyID= " + compid + "  Or Sa.CompanyID is null)) ");
                if (dsauto.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow theRow in dsauto.Tables[0].Rows)
                    {
                        int i = 0;
                        //SqlParameter[] parms = new SqlParameter[124]; // Parms = 86 (GENERAL), Parms = 93 (Clavon)
                        SqlParameter[] parms = new SqlParameter[127];
                        parms[i++] = new SqlParameter("@emp_name", Utility.ToString(theRow["FirstName"].ToString()));
                        parms[i++] = new SqlParameter("@emp_alias", Utility.ToString(""));
                        parms[i++] = new SqlParameter("@nationality", Utility.ToInteger(theRow["NationalityID"].ToString()));
                        parms[i++] = new SqlParameter("@emp_type", Utility.ToString(theRow["EmpType"].ToString()));
                        parms[i++] = new SqlParameter("@emp_lname", Utility.ToString(theRow["LastName"].ToString()));
                        parms[i++] = new SqlParameter("@empcpftype", Utility.ToInteger(empcpfgroup));
                        parms[i++] = new SqlParameter("@Insurance_number", Utility.ToString(insurance));
                        parms[i++] = new SqlParameter("@Insurance_expiry", Utility.ToString(insurance_exp));
                        parms[i++] = new SqlParameter("@CSOC_number", Utility.ToString(csoc));
                        parms[i++] = new SqlParameter("@CSOC_expiry", Utility.ToString(csoc_exp));
                        parms[i++] = new SqlParameter("@passport", Utility.ToString(theRow["passportno"].ToString()));
                        parms[i++] = new SqlParameter("@passport_expiry", Utility.ToString(theRow["passportexpiry"].ToString()));
                        if (theRow["EmpType"].ToString() == "SC" || theRow["EmpType"].ToString() == "SPR")
                        {
                            parms[i++] = new SqlParameter("@ic_pp_number", Utility.ToString(theRow["NRIC"].ToString()));
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@ic_pp_number", Utility.ToString(theRow["FIN"].ToString()));
                        }
                        parms[i++] = new SqlParameter("@wp_exp_date", Utility.ToString(theRow["wpexpdate"].ToString()));
                        parms[i++] = new SqlParameter("@pr_date", Utility.ToString(theRow["PRDate"].ToString()));
                        parms[i++] = new SqlParameter("@address", Utility.ToString(Address));
                        parms[i++] = new SqlParameter("@country", Utility.ToInteger(theRow["COUNTRYOFRESI"].ToString()));//COUNTRYOFRESI
                        parms[i++] = new SqlParameter("@postal_code", Utility.ToString(theRow["PostalCode"].ToString()));
                        parms[i++] = new SqlParameter("@phone", Utility.ToString(theRow["Phone"].ToString()));
                        parms[i++] = new SqlParameter("@hand_phone", Utility.ToString(theRow["HandPhone"].ToString()));
                        parms[i++] = new SqlParameter("@email", Utility.ToString(theRow["Email"].ToString()));
                        parms[i++] = new SqlParameter("@sex", Utility.ToString(theRow["Sex"].ToString()));
                        parms[i++] = new SqlParameter("@religion", Utility.ToInteger(theRow["ReligionID"].ToString()));
                        parms[i++] = new SqlParameter("@race", Utility.ToInteger(theRow["RaceID"].ToString()));
                        parms[i++] = new SqlParameter("@marital_status", Utility.ToString(theRow["maritalstatus"].ToString()));
                        parms[i++] = new SqlParameter("@place_of_birth", Utility.ToString(theRow["PLACEOFBIRTH"].ToString()));//
                        parms[i++] = new SqlParameter("@date_of_birth", Utility.ToString(theRow["dateofbirth"].ToString()));
                        parms[i++] = new SqlParameter("@income_taxid", Utility.ToString(IncomeTaxID));
                        parms[i++] = new SqlParameter("@employee_cpf_acct", Utility.ToString(EmployeeCPFAcctNumber));
                        parms[i++] = new SqlParameter("@employer_cpf_acct", Utility.ToString(EmployerCPFAcctNumber));
                        parms[i++] = new SqlParameter("@giro_bank", Utility.ToString(theRow["GIROBANK"].ToString()));//GIROBANK
                        parms[i++] = new SqlParameter("@giro_code", Utility.ToString(GIROCode));
                        parms[i++] = new SqlParameter("@giro_branch", Utility.ToString(theRow["GBRANCHCODE"].ToString()));//GBRANCHCODE
                        parms[i++] = new SqlParameter("@giro_acct_number", Utility.ToString(theRow["GIROACCNO"].ToString()));//GIROACCNO
                        parms[i++] = new SqlParameter("@cpf_entitlement", Utility.ToString(theRow["CpfEntitlement"].ToString()));
                        parms[i++] = new SqlParameter("@cpf_employer", Utility.ToDouble(EmployerCPF));
                        parms[i++] = new SqlParameter("@department", Utility.ToInteger(theRow["DeptID"].ToString()));
                        parms[i++] = new SqlParameter("@cpf_employee", Utility.ToDouble(EmployeeCPF));
                        parms[i++] = new SqlParameter("@designation", Utility.ToInteger(theRow["DesgID"].ToString()));
                        parms[i++] = new SqlParameter("@trade", Utility.ToInteger(theRow["TradeID"].ToString()));
                        parms[i++] = new SqlParameter("@emp_supervisor", Utility.ToInteger(supervisor));
                        parms[i++] = new SqlParameter("@emp_clsupervisor", Utility.ToInteger(clsupervisor));
                        parms[i++] = new SqlParameter("@education", Utility.ToString(Education));
                        parms[i++] = new SqlParameter("@joining_date", Utility.ToString(theRow["JoiningDate"].ToString()));
                        parms[i++] = new SqlParameter("@probation_period", Utility.ToInteger(theRow["PROBPERIOD"].ToString()));//PROBPERIOD
                        parms[i++] = new SqlParameter("@confirmation_date", Utility.ToString(theRow["ConfirmationDate"].ToString()));
                        parms[i++] = new SqlParameter("@empgroup", Utility.ToInteger(theRow["EmpGroupID"].ToString()));
                        parms[i++] = new SqlParameter("@ot_entitlement", Utility.ToString(theRow["OTEntilement"].ToString()));
                        parms[i++] = new SqlParameter("@termination_date", Utility.ToString(TerminationDate));
                        parms[i++] = new SqlParameter("@termination_reason", Utility.ToString(terminreason));
                        parms[i++] = new SqlParameter("@pay_frequency", Utility.ToString(theRow["payfrequency"].ToString()));
                        parms[i++] = new SqlParameter("@payrate", theRow["Salary"].ToString());
                        parms[i++] = new SqlParameter("@hourlyRate", Utility.ToDouble(theRow["hourlyRate"].ToString()));
                        parms[i++] = new SqlParameter("@hourlyRateMode", Utility.ToString(theRow["hourlyRateMode"].ToString()));
                        parms[i++] = new SqlParameter("@dailyRateMode", Utility.ToString(theRow["dailyRateMode"].ToString()));
                        parms[i++] = new SqlParameter("@dailyRate", Utility.ToDouble(theRow["dailyRate"].ToString()));
                        parms[i++] = new SqlParameter("@wday_per_week", Utility.ToDouble(theRow["wdayperweek"].ToString()));
                        parms[i++] = new SqlParameter("@payment_mode", Utility.ToString(theRow["PAYMODE"].ToString()));//PAYMODE
                        parms[i++] = new SqlParameter("@email_payslip", Utility.ToString(EmailPaySlip));
                        parms[i++] = new SqlParameter("@fw_code", Utility.ToString(FWLCode));
                        parms[i++] = new SqlParameter("@fw_levy", Utility.ToDouble(theRow["MONTHLYLEVY"].ToString()));//MONTHLYLEVY
                        parms[i++] = new SqlParameter("@wh_tax_pct", Utility.ToDouble(WithHoldTaxPct));
                        parms[i++] = new SqlParameter("@wh_tax_amt", Utility.ToDouble(WithHoldTaxAmt));
                        parms[i++] = new SqlParameter("@sdf_required", SDFRequired);
                        parms[i++] = new SqlParameter("@cdac_fund", Utility.ToDouble(CDAC));
                        parms[i++] = new SqlParameter("@mbmf_fund", Utility.ToDouble(MBMF));
                        parms[i++] = new SqlParameter("@sinda_fund", Utility.ToDouble(SINDA));
                        parms[i++] = new SqlParameter("@ecf_fund", Utility.ToDouble(ECF));
                        parms[i++] = new SqlParameter("@cchest_fund", Utility.ToDouble(CCHEST));
                        parms[i++] = new SqlParameter("@remarks", Utility.ToString(Remarks));
                        parms[i++] = new SqlParameter("@Images", Utility.ToString(Img));
                        parms[i++] = new SqlParameter("@Company_Id", Utility.ToInteger(compid));
                        parms[i++] = new SqlParameter("@compcode", Utility.ToString(companycode));
                        parms[i++] = new SqlParameter("@leaveCarryForward", "");
                        parms[i++] = new SqlParameter("@giro_acc_name", Utility.ToString(theRow["GIROACCNAME"].ToString()));//GIROACCNAME
                        parms[i++] = new SqlParameter("@groupid", Utility.ToString(theRow["GroupID"].ToString()));
                        parms[i++] = new SqlParameter("@localaddress2", Utility.ToString(localadd2));
                        parms[i++] = new SqlParameter("@foreignaddress1", Utility.ToString(foreignadd1));
                        parms[i++] = new SqlParameter("@foreignaddress2", Utility.ToString(foreignadd2));
                        parms[i++] = new SqlParameter("@time_card_no", Utility.ToString(theRow["TimeCardNo"].ToString()));
                        parms[i++] = new SqlParameter("@foreignpostalcode", Utility.ToString(fpostalcode));
                        parms[i++] = new SqlParameter("@pp_issue_date", Utility.ToString(theRow["ppissuedate"].ToString()));
                        parms[i++] = new SqlParameter("@worker_levy", Utility.ToString(theRow["MONTHLYLEVY"].ToString()));
                        parms[i++] = new SqlParameter("@wp_application_date", Utility.ToString(theRow["wpapplicationdate"].ToString()));
                        parms[i++] = new SqlParameter("@emp_ref_type", Utility.ToInteger(emp_ref_type));
                        parms[i++] = new SqlParameter("@fund_optout", Utility.ToInteger(optout));
                        parms[i++] = new SqlParameter("@emp_category", Utility.ToInteger(emp_category));
                        parms[i++] = new SqlParameter("@v1rate", Utility.ToDouble(v1rate));
                        parms[i++] = new SqlParameter("@v2rate", Utility.ToDouble(v2rate));
                        parms[i++] = new SqlParameter("@v3rate", Utility.ToDouble(v3rate));
                        parms[i++] = new SqlParameter("@v4rate", Utility.ToDouble(v4rate));
                        parms[i++] = new SqlParameter("@batch_no", Utility.ToInteger(batch_no));
                        parms[i++] = new SqlParameter("@wp_issue_date", Utility.ToString(theRow["wpissuedate"].ToString()));
                        parms[i++] = new SqlParameter("@photo_no", Utility.ToInteger(photo));
                        parms[i++] = new SqlParameter("@wp_number", Utility.ToString(theRow["wpnumber"].ToString()));
                        parms[i++] = new SqlParameter("@shipyard_quota", Utility.ToString(shipyard_quota));
                        parms[i++] = new SqlParameter("@block_no", Utility.ToString(theRow["blockno"].ToString()));
                        parms[i++] = new SqlParameter("@street_name", Utility.ToString(theRow["streetname"].ToString()));
                        parms[i++] = new SqlParameter("@unit_no", Utility.ToString(theRow["unitno"].ToString()));
                        parms[i++] = new SqlParameter("@level_no", Utility.ToString(theRow["levelno"].ToString()));
                        parms[i++] = new SqlParameter("@ot1rate", Utility.ToDouble(ot1rate));
                        parms[i++] = new SqlParameter("@ot2rate", Utility.ToDouble(ot2rate));
                        parms[i++] = new SqlParameter("@EmeConPer", Utility.ToString(strtxtEmeConPer));
                        parms[i++] = new SqlParameter("@EmeConPerRel", Utility.ToString(strtxtEmeConPerRel));
                        parms[i++] = new SqlParameter("@EmeConPerPh1", Utility.ToString(strtxtEmeConPerPh1));
                        parms[i++] = new SqlParameter("@EmeConPerPh2", Utility.ToString(strtxtEmeConPerPh2));
                        parms[i++] = new SqlParameter("@EmeConPerAdd", Utility.ToString(strtxtEmeConPerAdd));
                        parms[i++] = new SqlParameter("@EmeConPerRem", Utility.ToString(strtxtEmeConPerRem));
                        parms[i++] = new SqlParameter("@bloodgroup", Utility.ToString(strtxtBloodGroup));
                        parms[i++] = new SqlParameter("@agent_id", Utility.ToString(stragent_id));
                        parms[i++] = new SqlParameter("@mye_cert_id", Utility.ToString(strmye_cert_id));
                        parms[i++] = new SqlParameter("@wp_arrival_date", Utility.ToString(wp_arrivaldate));
                        parms[i++] = new SqlParameter("@pay_supervisor", Utility.ToInteger(pay_supervisor));
                        parms[i++] = new SqlParameter("@safetypass_id", Utility.ToInteger(theRow["SafeID"].ToString()));
                        parms[i++] = new SqlParameter("@safetypass_sno", Utility.ToString(theRow["safetypassno1"].ToString()));
                        parms[i++] = new SqlParameter("@safetypass_expiry", Utility.ToString(theRow["safetypassexp1"].ToString()));
                        parms[i++] = new SqlParameter("@payrolltype", Utility.ToInteger(ddlPayrollType.SelectedValue.ToString()));
                        parms[i++] = new SqlParameter("@compcpffh", Utility.ToInteger(cpffh));
                        parms[i++] = new SqlParameter("@tssupervisor", Utility.ToInteger(tssupervisor));
                        parms[i++] = new SqlParameter("@compfundfh", Utility.ToInteger(fundfh));

                        parms[i++] = new SqlParameter("@Leave_supervisor", Utility.ToInteger(leave_supervisor));//Ram Added
                        //r
                        parms[i++] = new SqlParameter("@PaymentType", Utility.ToInteger(radListPayType.SelectedValue));
                        parms[i++] = new SqlParameter("@halfsalary", Utility.ToInteger(salhalf));

                        ismaster = "";
                        if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                        {
                            string sql = "select isMaster From Company Where Company_ID=1";
                            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr.Read())
                            {
                                ismaster = dr[0].ToString();
                                if (ismaster == "True")
                                {
                                    parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("3"));
                                }
                                else
                                {
                                    parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                                }
                            }
                            else
                            {
                                parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                            }
                        }
                        else
                        {
                            parms[i++] = new SqlParameter("@empstatid", Utility.ToInteger("1"));
                        }


                        //r
                        parms[i++] = new SqlParameter("@CurrencyBank", Utility.ToInteger(drpCurrBank1.SelectedValue));

                        parms[i++] = new SqlParameter("@PaymentPart", Utility.ToInteger(drpPaymentMode.SelectedValue));
                        //


                        parms[i] = new SqlParameter("@UCode", SqlDbType.Int);
                        parms[i].Direction = ParameterDirection.Output;

                        string sSQL = "sp_emp_add";
                        try
                        {
                            int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                            if (retVal >= 1)
                            {
                                iCountSuccess = iCountSuccess + 1;
                                lblerror.ForeColor = System.Drawing.Color.Green;
                                lblerror.Text = "Employees Imported Successfully.";
                                int ucode = Utility.ToInteger(parms[i].Value);
                                string password1 = companycode + "_" + ucode;
                                string password = encrypt.SyEncrypt(password1);

                                SqlParameter[] parms1 = new SqlParameter[2];
                                parms1[0] = new SqlParameter("@password", Utility.ToString(password));
                                parms1[1] = new SqlParameter("@ucode", Utility.ToString(ucode));

                                int returnval = DataAccess.ExecuteStoreProc("sp_password_update", parms1);
                            }
                        }

                        catch (Exception ex)
                        {
                            iCountFail = iCountFail + 1;
                            string ErrMsg = ex.Message;
                            lblerror.ForeColor = System.Drawing.Color.Red;
                            if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                            {
                                ErrMsg = "<font color = 'Red'>Username already Exist, Please Enter some other Username</font>";
                            }
                            lblerror.Text = ErrMsg;
                        }
                    }

                    lblerror.Text = "";
                    if (iCountSuccess > 0)
                    {
                        lblerror.ForeColor = System.Drawing.Color.Green;
                        lblerror.Text = Convert.ToString(iCountSuccess.ToString()) + " Employees Imported Successfully.<br/>";
                        
                    }
                    if (iCountFail > 0)
                    {
                        lblerror.ForeColor = System.Drawing.Color.Green;
                        lblerror.Text = lblerror.Text + Convert.ToString(iCountFail.ToString()) + " Employees Failed to Import.";
                    }
                    ShowMessageBox(lblerror.Text);

                    //if (iCountSuccess == 0)
                    //{
                    //    lblerror.ForeColor = System.Drawing.Color.Red;
                    //    lblerror.Text = "None of the Employees Imported.";
                    //}
                }
                else
                {
                }
            }
            bln = false;
            btnBulkInsert.Visible = false;
            DataSet EmpBulk = new DataSet();
            EmpBulk = getDataSet("Select Count(ID) From EmployeeBulkImport Where Deleted=3 And CompanyID=" + compid);

            if (EmpBulk.Tables[0].Rows.Count > 0)
            {
                if (Utility.ToInteger(EmpBulk.Tables[0].Rows[0][0]) > 0)
                {
                    btnBulkInsert.Visible = true;
                    btnBulkInsert.Text = "Import " + Utility.ToString(EmpBulk.Tables[0].Rows[0][0]) + " Employees";
                }
            }
        }


        protected void btnBulkInsert_Click(object sender, EventArgs e)
        {
            if (bln == false)
            {
                EmpAutoAddANB();
            }
        }

        protected void btnSB_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlCurr = "Select CurrencyId from girobanks Where id=" + cmbSBPayMode.Value;

                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlCurr, null);

                int curr = 0;

                while (sqldr.Read())
                {
                    if (sqldr.GetValue(0) != null && sqldr.GetValue(0).ToString() != "")
                    {
                        curr = Convert.ToInt32(sqldr.GetValue(0).ToString());
                    }
                }
                if (curr == 0)
                {
                    drpCurrBank2.SelectedIndex = 0;
                }
                else
                {
                    drpCurrBank2.SelectedValue = curr.ToString();
                }


                string ssqlb = "";
                if (radListPayType.SelectedValue == "0")
                {
                    ssqlb = "Insert Into EmployeeBankInfo (Emp_ID,Payment_From,Giro_Bank_ID,Giro_Acct_Number,Giro_Branch,Giro_Acc_Name,Percentage,Remarks,CurrencyBank,PaymentPart) values (" + emp_code + "," + cmbSBPayMode.Value.ToString() + "," + cmbSBbranchcode.Value.ToString() + ",'" + txtSBGIROAccountNo.Value.ToString().Trim() + "','" + txtSBgirobranch.Value.ToString() + "','" + txtSBgiroaccountname.Value.ToString().Trim() + "'," + txtSBperct.Value.ToString() + ",'" + txtSBRemarks.Value.ToString().Trim() + "'," + drpCurrBank2.SelectedValue + "," + drpPaymentMode1.SelectedValue + ")";
                }
                else
                {
                    ssqlb = "Insert Into EmployeeBankInfo (Emp_ID,Payment_From,Giro_Bank_ID,Giro_Acct_Number,Giro_Branch,Giro_Acc_Name,Percentage,Remarks,CurrencyBank,PaymentPart) values (" + emp_code + "," + cmbSBPayMode.Value.ToString() + "," + cmbSBbranchcode.Value.ToString() + ",'" + txtSBGIROAccountNo.Value.ToString().Trim() + "','" + txtSBgirobranch.Value.ToString() + "','" + txtSBgiroaccountname.Value.ToString().Trim() + "',0,'" + txtSBRemarks.Value.ToString().Trim() + "'," + drpCurrBank2.SelectedValue + "," + drpPaymentMode1.SelectedValue + ")";
                }
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                LoadBankGrid();
            }
            catch (Exception ex)
            {
            }
        }

        private void AddEditEmployee_PreRender(object sender, EventArgs e)
        {
            for (int i = 0; i < tbsEmp.Tabs.Count; i++)
            {
                tbsEmp.Tabs[i].Enabled = true;
                if (tbsEmp.Tabs[i].Text.ToString() == "Item Iss<u>u</u>ed Info")
                {
                    if (HttpContext.Current.Session["ANBPRODUCT"] != null)
                    {
                        if (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "SME")
                        {
                            tbsEmp.Tabs[i].Visible = false;
                        }
                    }
                }
            }


            if (strMessage.Length > 0)
            {
                ShowMessageBox(strMessage);
                strMessage = "";
            }
        }

        protected void buttonSubmit_Click(object sender, System.EventArgs e)
        {
            BindResults();
        }

        protected void RadGrid3_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                string sSQL = "DELETE FROM [DocumentMappedToEmployee] WHERE [ID] =" + id;
                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>Document is Deleted Successfully."));

                }
                else
                {
                    RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Document."));
                }

                RadGrid3.DataBind();
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }


        private void BindResults()
        {
            string strFileName = "";
            string objFileName = "";
            string uploadpath = "../" + "Documents" + "/" + compid + "/" + emp_code + "/" + "Files";
            if (txtDocumentName.Text.ToString().Trim().Length <= 0)
            {
                strMessage = "Please Key In the Document Name";
            }
            if (file1.UploadedFiles.Count != 0)
            {
                if (Directory.Exists(Server.MapPath(uploadpath)))
                {
                    if (File.Exists(Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName()))
                    {
                        strMessage = " File Already Exist with this name";
                    }
                    else
                    {
                        objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                        file1.UploadedFiles[0].SaveAs(objFileName);
                        objFileName = file1.UploadedFiles[0].GetName();

                        string ssqlb = "Insert Into DocumentMappedToEmployee (Category_ID, Emp_ID, Document_Name, FileName) values (" + ddlCategory.SelectedItem.Value + "," + emp_code + ",'" + txtDocumentName.Text.ToString().Trim() + "','" + objFileName + "')";
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        RadGrid3.DataBind();
                    }
                }
                else
                {
                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    objFileName = Server.MapPath(uploadpath) + "/" + file1.UploadedFiles[0].GetName();
                    file1.UploadedFiles[0].SaveAs(objFileName);
                    objFileName = file1.UploadedFiles[0].GetName();

                    string ssqlb = "Insert Into DocumentMappedToEmployee (Category_ID, Emp_ID, Document_Name, FileName) values (" + ddlCategory.SelectedItem.Value + "," + emp_code + ",'" + txtDocumentName.Text.ToString().Trim() + "','" + objFileName + "')";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid3.DataBind();
                }
            }
            else
            {
                strMessage = "Please Select Document to Upload";
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
        }


        protected void RadGrid3_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string empid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Emp_Code"]);
                    string strFileName = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["FileName"]);
                    HyperLink hlFileName = (HyperLink)e.Item.FindControl("hlnFile");

                    if (hlFileName != null)
                    {
                        //string sLAttachmentPath = Server.MapPath("../Documents/" + compid + "/" + empid + "/Files/" + strFileName);
                        hlFileName.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empid + "/" + "Files" + "/" + strFileName;
                        //hlFileName.NavigateUrl = sLAttachmentPath;
                        //hlFileName.Attributes.Add("onClick", "javascript:window.open('" + sLAttachmentPath+ "')");
                    }

                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                    RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                    e.Canceled = true;
                }
            }
        }


        protected void RadGrid4_ItemUpdated(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string strrdDOB = "null";
            string strrdDOM = "null";
            GridEditableItem editedItem = e.Item as GridEditFormItem;
            GridEditManager editMan = editedItem.EditManager;

            string strID = Utility.ToString(this.RadGrid4.MasterTableView.Items[e.Item.ItemIndex].GetDataKeyValue("id"));
            string strtxtName = ((TextBox)e.Item.FindControl("txtName")).Text.ToString().Trim();
            GridDropDownListColumnEditor drpRelation = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnRelation"));

            if (((RadDatePicker)e.Item.FindControl("rdDateOfBirth")).SelectedDate != null)
            {
                strrdDOB = "'" + ((RadDatePicker)e.Item.FindControl("rdDateOfBirth")).SelectedDate.Value.ToString("MM/dd/yyyy") + "'";
            }
            if (((RadDatePicker)e.Item.FindControl("rdMarriage_Date")).SelectedDate != null)
            {
                strrdDOM = "'" + ((RadDatePicker)e.Item.FindControl("rdMarriage_Date")).SelectedDate.Value.ToString("MM/dd/yyyy") + "'";
            }
            GridDropDownListColumnEditor drpSex = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnSex"));
            string strtxtPhone = ((TextBox)e.Item.FindControl("txtPhone")).Text.ToString().Trim();
            string strtxtUIDN = ((TextBox)e.Item.FindControl("txtUIDN")).Text.ToString().Trim();
            GridDropDownListColumnEditor drpStatus = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnStatus"));
            if (strtxtName.Length <= 0)
            {
                strMessage = "Please Enter Name";
            }
            else
            {
                try
                {
                    string ssqlb = "Update Family Set Name='" + strtxtName + "',dateofbirth=" + strrdDOB + ",Sex='" + drpSex.SelectedValue.ToString() + "',Relation=" + drpRelation.SelectedValue.ToString() + ",Marriage_Date=" + strrdDOM.ToString() + ",Phone='" + strtxtPhone + "',UIDN='" + strtxtUIDN.ToString() + "',Status='" + drpStatus.SelectedValue.ToString() + "' Where ID=" + strID;
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid4.Rebind();
                    strMessage = "Family Info updated Successfully.";
                    e.Item.Edit = false;
                }
                catch (Exception ee)
                {
                    string ErrMsg = ee.Message;
                    RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>" + ErrMsg + "</font>"));
                }
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Certificate Category")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Training. This Training is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Training_details] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            strMessage = strMessage + "<br/>" + "Training is Deleted Successfully.";
                        }
                        else
                        {
                            strMessage = strMessage + "<br/>" + "Unable to delete the Training.";
                        }


                        if (strMessage.Length > 0)
                        {
                            ShowMessageBox(strMessage);
                            strMessage = "";
                            e.Canceled = true;
                        }
                    }
                    RadGrid1.DataBind();
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid5.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                string strresult = ((TextBox)editedItem["result"].Controls[0]).Text.ToString();
                string strvenue = ((TextBox)editedItem["venue"].Controls[0]).Text.ToString();

                if (strresult.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Result.";
                }
                if (strvenue.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Venue.";
                }
                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    e.Canceled = true;
                }
                else
                {
                    if (e.CommandName == "PerformInsert")
                    {
                        strMessage = "Training Added successfully.";
                    }
                    else
                    {
                        strMessage = "Training updated successfully.";
                    }
                    ShowMessageBox(strMessage);
                    strMessage = "";
                }
            }
        }

        protected void RadGrid2_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Certificate Category")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
        protected void RadGrid2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Safety Pass. This Safety Pass is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Safetypass_details] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            strMessage = strMessage + "<br/>" + "Safety Pass is Deleted Successfully.";
                        }
                        else
                        {
                            strMessage = strMessage + "<br/>" + "Unable to delete the Safety Pass.";
                        }


                        if (strMessage.Length > 0)
                        {
                            ShowMessageBox(strMessage);
                            strMessage = "";
                            e.Canceled = true;
                        }
                    }
                    RadGrid2.DataBind();
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid5.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }
        protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                string strsafetypass_sno = ((TextBox)editedItem["safetypass_sno"].Controls[0]).Text.ToString();
                //string strsafetypass_expiry = ((GridDateTimeColumn)editedItem["safetypass_expiry"].Controls[0])...ToString();

                if (strsafetypass_sno.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Safety Pass No.";
                }
                //if (strsafetypass_expiry.ToString().Length <= 0)
                //{
                //    strMessage = strMessage + "<br/>" + "Please enter Safety Pass Expiry Date.";
                //}
                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    e.Canceled = true;
                }
                else
                {
                    if (e.CommandName == "PerformInsert")
                    {
                        strMessage = "Safety Pass Added successfully.";
                    }
                    else
                    {
                        strMessage = "Safety Pass updated successfully.";
                    }
                    ShowMessageBox(strMessage);
                    strMessage = "";
                }
            }
        }


        protected void RadGrid5_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Certificate Category")) == false)
            {
                RadGrid5.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid5.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid5.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
        protected void RadGrid5_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid5.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Certificate. This Certificate is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeCertificate] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            strMessage = strMessage + "<br/>" + "Certificate is Deleted Successfully.";
                        }
                        else
                        {
                            strMessage = strMessage + "<br/>" + "Unable to delete the Certificate.";
                        }


                        if (strMessage.Length > 0)
                        {
                            ShowMessageBox(strMessage);
                            strMessage = "";
                            e.Canceled = true;
                        }
                    }

                    RadGrid5.DataBind();
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid5.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }
        protected void RadGrid5_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                string strNumber = ((TextBox)editedItem["CertificateNumber"].Controls[0]).Text.ToString();
                //DateInput strNumber = ((DateInput)editedItem["TestDate"].Controls[0]).DateInput;
                RadDatePicker testDate = new RadDatePicker();
                RadDatePicker issueDate = new RadDatePicker();
                RadDatePicker expiryDate = new RadDatePicker();

                testDate.DbSelectedDate = ((RadDatePicker)editedItem["TestDate"].Controls[0]).DbSelectedDate;
                issueDate.DbSelectedDate = ((RadDatePicker)editedItem["IssueDate"].Controls[0]).DbSelectedDate;
                expiryDate.DbSelectedDate = ((RadDatePicker)editedItem["ExpiryDate"].Controls[0]).DbSelectedDate;

                if (strNumber.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Certificate Number.";
                }

                if (issueDate.DbSelectedDate != null && expiryDate.DbSelectedDate != null)
                {
                    if (((System.DateTime)(expiryDate.DbSelectedDate)).Date < ((System.DateTime)(issueDate.DbSelectedDate)).Date)
                    {
                        strMessage = strMessage + "<br/>" + "Issue  Date Can not be greater than expiry Date.";
                    }
                }

                if (testDate.DbSelectedDate != null && expiryDate.DbSelectedDate != null)
                {
                    if (((System.DateTime)(testDate.DbSelectedDate)).Date > ((System.DateTime)(expiryDate.DbSelectedDate)).Date)
                    {
                        strMessage = strMessage + "<br/>" + "Test Date Can not greater than Expiry Date.";
                    }
                }

                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    e.Canceled = true;
                }
                else
                {
                    if (e.CommandName == "PerformInsert")
                    {
                        strMessage = "Certificate Added successfully.";
                    }
                    else
                    {
                        strMessage = "Certificate updated successfully.";
                    }
                    ShowMessageBox(strMessage);
                    strMessage = "";
                }
            }
        }

        protected void RadGrid4_ItemInserted(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string strrdDOB = "null";
            string strrdDOM = "null";
            GridEditableItem editedItem = e.Item as GridEditFormItem;
            GridEditManager editMan = editedItem.EditManager;

            //string strID = ((Label)e.Item.FindControl("lblID1")).Text.ToString().Trim();
            string strtxtName = ((TextBox)e.Item.FindControl("txtName")).Text.ToString().Trim();
            GridDropDownListColumnEditor drpRelation = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnRelation"));

            if (((RadDatePicker)e.Item.FindControl("rdDateOfBirth")).SelectedDate != null)
            {
                strrdDOB = "'" + ((RadDatePicker)e.Item.FindControl("rdDateOfBirth")).SelectedDate.Value.ToString("MM/dd/yyyy") + "'";
            }
            if (((RadDatePicker)e.Item.FindControl("rdMarriage_Date")).SelectedDate != null)
            {
                strrdDOM = "'" + ((RadDatePicker)e.Item.FindControl("rdMarriage_Date")).SelectedDate.Value.ToString("MM/dd/yyyy") + "'";
            }
            GridDropDownListColumnEditor drpSex = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnSex"));
            string strtxtPhone = ((TextBox)e.Item.FindControl("txtPhone")).Text.ToString().Trim();
            string strtxtUIDN = ((TextBox)e.Item.FindControl("txtUIDN")).Text.ToString().Trim();
            GridDropDownListColumnEditor drpStatus = (GridDropDownListColumnEditor)(editMan.GetColumnEditor("GridDropDownColumnStatus"));
            if (strtxtName.Length <= 0)
            {
                strMessage = "Please Enter Name";
            }
            else
            {
                try
                {
                    RadGrid4.MasterTableView.IsItemInserted = false;
                    e.Canceled = true;
                    string ssqlb = "Insert Into Family (Emp_ID, Name, DateOfBirth, Sex, Relation, Marriage_Date, Phone, UIDN, Status) values (" + emp_code + ",'" + strtxtName + "'," + strrdDOB + ",'" + drpSex.SelectedValue + "'," + drpRelation.SelectedValue + "," + strrdDOM + ",'" + strtxtPhone + "','" + strtxtUIDN + "','" + drpStatus.SelectedValue + "')";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    RadGrid4.Rebind();
                    strMessage = "Family Info added Successfully.";
                }
                catch (Exception ee)
                {
                    string ErrMsg = ee.Message;
                    RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>" + ErrMsg + "</font>"));
                }
            }
        }

        protected void btnAddHistory_Click(object sender, EventArgs e)
        {
            if (rdFrom.DbSelectedDate == null)
            {
                strMessage = strMessage + "<br/>" + "Please enter From Date.";
            }
            //if (rdConfirmation.DbSelectedDate == null)
            //{
            //    strMessage = strMessage + "<br/>" + "Please enter Confirmation Date.";
            //}
            if (custxtHourlyRate.Value.ToString().Trim().Length <= 0)
            {
                strMessage = strMessage + "<br/>" + "Please enter Hourly Rate.";
            }
            if (custxtDailyRate.Value.ToString().Trim().Length <= 0)
            {
                strMessage = strMessage + "<br/>" + "Please enter Daily Rate.";
            }
            if (drpOTEntitled.Value == "Y")
            {
                if (custxtOT1Rate.Text.ToString().Trim().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter OT-1 Rate.";
                }
                if (custxtOT2Rate.Text.ToString().Trim().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter OT-2 Rate.";
                }
            }

            //if (rdEnd.SelectedDate != null && rdFrom.SelectedDate != null)
            //{
            //    if (rdEnd.SelectedDate < rdFrom.SelectedDate)
            //    {
            //        strMessage = strMessage + "<br/>" + "End Date should be greater than From Date.";
            //    }
            //}

            if (strMessage.Length > 0)
            {
                ShowMessageBox(strMessage);
                strMessage = "";
            }
            else
            {
                double dblpay = 0;
                double dblpaynew = 0;
                if (bViewSalAllowed)
                {
                    dblpay = Utility.ToDouble(custxtPayRate.Text.ToString().Trim());
                    dblpaynew = Utility.ToDouble(custxtPayRate.Text.ToString().Trim());
                }
                else
                {
                    dblpay = Utility.ToDouble(txtHiddenPayRate.Value);
                    dblpaynew = Utility.ToDouble(custxtPayRate.Text.ToString().Trim());
                }
                double dblot1rate = Utility.ToDouble(custxtOT1Rate.Text.ToString().Trim());
                double dblot2rate = Utility.ToDouble(custxtOT2Rate.Text.ToString().Trim());
                
                double dblhrrate = Utility.ToDouble(custxtHourlyRate.Value.ToString().Trim());
                double dbldarate = Utility.ToDouble(custxtDailyRate.Value.ToString().Trim());

                if ((Utility.ToDouble(hourrate.Text)) > 0)
                {
                    dblhrrate = Utility.ToDouble(hourrate.Text);
                }

                if (dblpay < 0)
                {
                    dblpay = 0;
                }
                if (dblot1rate < 0)
                {
                    dblot1rate = 0;
                }
                if (dblot2rate < 0)
                {
                    dblot2rate = 0;
                }


                string strrdConfDate = "";
                string strrdEndDate = "";
                string strrdFromDate = rdFrom.SelectedDate.Value.ToString("MM/dd/yyyy");
                string strdept = "";
                string strdesg = "";
                string strtrade = "";
                if (drpDepartment.Value == "-select-")
                {
                    strdept = "";
                }
                else
                {
                    strdept = drpDepartment.Value;
                }
                if (drpDesignation.Value == "-select-")
                {
                    strdesg = "";
                }
                else
                {
                    strdesg = drpDesignation.Value;
                }
                if (drpTrade.Value == "-select-")
                {
                    strtrade = "";
                }
                else
                {
                    strtrade = drpTrade.Value;
                }
                //if (rdEnd.SelectedDate != null)
                //{
                //    strrdEndDate = rdEnd.SelectedDate.Value.Date.ToString("MM/dd/yyyy");
                //}
                //if (rdConfirmation.SelectedDate != null)
                //{
                //    strrdConfDate = rdConfirmation.SelectedDate.Value.Date.ToString("MM/dd/yyyy");
                //}

                string ssqlb2 = "";
                string ssqlb1 = "Update EmployeePayHistory Set ToDate = Convert(datetime,'" + rdFrom.SelectedDate.Value.AddDays(-1).ToShortDateString() + "',103) Where Emp_ID=" + emp_code + " And ToDate is null";
                int progressionreason = -1;
                if (drpProgReason.Text == "")
                {
                    progressionreason = -1;
                }
                else
                {
                    progressionreason = Convert.ToInt32(drpProgReason.SelectedValue);
                }
                if (strrdEndDate != "")
                {
                    if (drpCurrency.Enabled == true)
                    {
                        ssqlb2 = "Insert Into EmployeePayHistory (Emp_ID, FromDate, ToDate, DepartmentID, DesignationID, TradeID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate,CurrencyID,PayHistoryID) Values (" + emp_code + ",'" + strrdFromDate + "','" + strrdEndDate + "','" + strdept + "','" + strdesg + "','" + strtrade + "','" + drpOTEntitled.Value + "','" + drpCPFEntitlement.Value + "','" + dblot1rate.ToString() + "','" + dblot2rate.ToString() + "','" + drpPayFrequency.Value + "'," + drpworkingdays.Value + ",EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + dblpaynew.ToString() + "'),'" + drpHourlyMode.Value + "'," + dblhrrate.ToString() + ",'" + drpDailyMode.Value + "'," + dbldarate.ToString() + "," + drpCurrency.SelectedValue + "," + progressionreason + ")";
                    }
                    else
                    {
                        ssqlb2 = "Insert Into EmployeePayHistory (Emp_ID, FromDate, ToDate, DepartmentID, DesignationID, TradeID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate,CurrencyID,PayHistoryID) Values (" + emp_code + ",'" + strrdFromDate + "','" + strrdEndDate + "','" + strdept + "','" + strdesg + "','" + strtrade + "','" + drpOTEntitled.Value + "','" + drpCPFEntitlement.Value + "','" + dblot1rate.ToString() + "','" + dblot2rate.ToString() + "','" + drpPayFrequency.Value + "'," + drpworkingdays.Value + ",EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + dblpaynew.ToString() + "'),'" + drpHourlyMode.Value + "'," + dblhrrate.ToString() + ",'" + drpDailyMode.Value + "'," + dbldarate.ToString() + "," + drpCurrency.SelectedValue + "," + progressionreason + ")";
                    }
                }
                else
                {
                    if (drpCurrency.Enabled == true)
                    {
                        ssqlb2 = "Insert Into EmployeePayHistory (Emp_ID, FromDate, DepartmentID, DesignationID, TradeID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate,CurrencyID,PayHistoryID) Values (" + emp_code + ",'" + strrdFromDate + "','" + strdept + "','" + strdesg + "','" + strtrade + "','" + drpOTEntitled.Value + "','" + drpCPFEntitlement.Value + "','" + dblot1rate.ToString() + "','" + dblot2rate.ToString() + "','" + drpPayFrequency.Value + "'," + drpworkingdays.Value + ",EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + dblpaynew.ToString() + "'),'" + drpHourlyMode.Value + "'," + dblhrrate.ToString() + ",'" + drpDailyMode.Value + "'," + dbldarate.ToString() + "," + drpCurrency.SelectedValue + "," + progressionreason + ")";
                    }
                    else
                    {
                        ssqlb2 = "Insert Into EmployeePayHistory (Emp_ID, FromDate, DepartmentID, DesignationID, TradeID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate,CurrencyID,PayHistoryID) Values (" + emp_code + ",'" + strrdFromDate + "','" + strdept + "','" + strdesg + "','" + strtrade + "','" + drpOTEntitled.Value + "','" + drpCPFEntitlement.Value + "','" + dblot1rate.ToString() + "','" + dblot2rate.ToString() + "','" + drpPayFrequency.Value + "'," + drpworkingdays.Value + ",EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + dblpaynew.ToString() + "'),'" + drpHourlyMode.Value + "'," + dblhrrate.ToString() + ",'" + drpDailyMode.Value + "'," + dbldarate.ToString() + "," + drpCurrency.SelectedValue + "," + progressionreason + ")";
                    }
                }

                string ssqlb3 = "";
                ssqlb3 = "Update Employee Set Dept_id='" + strdept + "',desig_id='" + strdesg + "',trade_id='" + strtrade + "',ot_entitlement='" + drpOTEntitled.Value + "',CPF_entitlement='" + drpCPFEntitlement.Value + "',OT1Rate='" + dblot1rate.ToString() + "',OT2Rate='" + dblot2rate.ToString() + "',Pay_Frequency='" + drpPayFrequency.Value + "',WDays_Per_Week=" + drpworkingdays.Value + ",PayRate=EncryptByAsymKey(AsymKey_ID('AsymKey'),'" + dblpaynew.ToString() + "'),Hourly_Rate_Mode='" + drpHourlyMode.Value + "',Hourly_Rate='" + dblhrrate.ToString() + "',Daily_Rate_Mode='" + drpDailyMode.Value + "',Daily_Rate='" + Convert.ToString(dbldarate.ToString()) + "' Where Emp_Code=" + emp_code;
                DataAccess.FetchRS(CommandType.Text, ssqlb1, null);
                DataAccess.FetchRS(CommandType.Text, ssqlb2, null);
                DataAccess.FetchRS(CommandType.Text, ssqlb3, null);

                //ku
                string customHrsql = "select isnull(max(h.Value),0) as hr  from HrRateAssignToEmployee e inner join HrRate h on e.HrRateId= h.Id where e.Emp_id=" + emp_code;
                SqlDataReader cushour_reader = DataAccess.ExecuteReader(CommandType.Text, customHrsql, null);

                decimal hvalue = 0.00m;
                decimal _basic_rate = 0.00m;
                while (cushour_reader.Read())
                {
                    hvalue = (decimal)Utility.ToDouble(cushour_reader.GetValue(0).ToString());
                }

                _basic_rate = (decimal)Utility.ToDouble(this.txtPayRate.Text);
                this.hourtextbox.Text = hvalue.ToString();
                if (_basic_rate > 0 && hvalue > 0)
                {
                    decimal hrrate = Math.Round(((_basic_rate * 12) / (52 * hvalue)), 2);
                    this.hourrate.Text = hrrate.ToString();
                }
                else
                {
                    this.hourrate.Visible = false;
                    this.hourtextbox.Visible = false;
                }
              

                LoadFormValues();
                strMessage = strMessage + "<br/>" + "Pay Records Updated Successfully.";
                ShowMessageBox(strMessage);
                strMessage = "";
            }

        }

        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpYear.SelectedItem.Value != "0")
            {
                //if (Session["Leave_Model"].ToString() == "1")
                //{
                BindleavepolicyNormal();
                //}
            }
        }

        void BindleavepolicyNormal()
        {
            string sSQL = "sp_GetEmployeeLeavePolicy";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@empid", Utility.ToInteger(emp_code));
            parms[1] = new SqlParameter("@year", Utility.ToString(drpYear.SelectedItem.Value));
            parms[2] = new SqlParameter("@applydateon", System.DateTime.Today);
            parms[3] = new SqlParameter("@filter", 0);
            DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid8.Visible = false;
            RadGrid7.Visible = true;
            RadGrid7.DataSource = ds;
            RadGrid7.MasterTableView.DataSource = ds;
            RadGrid7.DataBind();
            btnLeaveUpdate.Visible = true;
            btnLeaveUpdate.Enabled = false;

            DataSet EmpAll = new DataSet();
            //EmpAll = getDataSet("select ID, Leave_Year From EmployeeLeavesAllowed Where  Emp_ID=" + emp_code);
            EmpAll = getDataSet("select ID, Leave_Year From EmployeeLeavesAllowed Where leave_type = 8 and Emp_ID=" + emp_code); // Updated by SU MON
            DataRow[] rows = EmpAll.Tables[0].Select("Leave_Year > '" + drpYear.SelectedItem.Value + "'");
            if (EmpAll.Tables[0].Rows.Count > 0)
            {
                if (rows.Length > 0)
                {
                    btnLeaveUpdate.Enabled = false;
                }
                else
                {
                    btnLeaveUpdate.Enabled = true;
                }
            }
            else
            {
                btnLeaveUpdate.Enabled = true;
            }
            //Check If leave model is Year of Service-Prorated(Ceiling)
            DataSet Empyos = new DataSet();
            Empyos = getDataSet("select Leave_Model = Case  When  c.leave_model =1 Then 'Fixed Yearly-Normal' When  c.leave_model =7 Then 'Fixed Yearly-Prorated' When  c.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  c.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  c.leave_model =3 Then 'Year of Service-Normal' When  c.leave_model =8 Then 'Year of Service-Prorated' When  c.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  c.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END  from employee a   left Outer join employee b On a.emp_supervisor=b.emp_code  Left Outer Join Company c on a.company_id=c.company_id where a.emp_code=" + emp_code);

            DataSet empLeavesAllowed = new DataSet();
            empLeavesAllowed = getDataSet("select * from leaves_allowed where group_id in (select emp_group_id from employee where emp_code =" + emp_code + ") and leave_year=" + Utility.ToString(drpYear.SelectedItem.Value));

            if (Empyos.Tables.Count > 0)
            {
                if (Empyos.Tables[0].Rows.Count > 0)
                {
                    //If YOS Model
                    if (Utility.ToInteger(Empyos.Tables[0].Rows[0][0].ToString().IndexOf("Year of Service")) >= 0)
                    {
                        //If button in enabled false
                        if (btnLeaveUpdate.Enabled == false)
                        {
                            //Check if Leaves Allowed in next year
                            if (empLeavesAllowed.Tables.Count > 0)
                            {
                                if (empLeavesAllowed.Tables[0].Rows.Count > 0)
                                {

                                    btnLeaveUpdate.Enabled = true;
                                }
                            }

                        }
                    }
                }
            }

            //r
            //for hybrid leave enable the update button
            if (Convert.ToInt32(Session["Leave_Model"]) == 9 || Convert.ToInt32(Session["Leave_Model"]) == 10)
            {
                try {
                    string SQL = " SELECT * FROM LeavesAllowedInYears where Years=" + int.Parse(drpYear.SelectedItem.Value) + " and Emp_ID = " + emp_code + "";
                    DataSet dshybrid = DataAccess.FetchRS(CommandType.Text, SQL, null);
                    if (dshybrid.Tables[0].Rows.Count > 0)
                    {
                        btnLeaveUpdate.Enabled = false;
                    }
                    else
                    {
                        btnLeaveUpdate.Enabled = true;
                    }
                }
                catch (Exception ex) { }
            }
            if (Convert.ToInt32(Session["Leave_Model"]) == 10)
            {
                lblLeaveModel.Text = "Hybrid - Normal";
            }

        }


        void BindChildleavepolicyNormal(int leavetype, double compolicyleave, double currentleaveearned)
        {
            string sSQL = "";
            if (txtLeaveModel.Value.ToString() == "1" || txtLeaveModel.Value.ToString() == "2" || txtLeaveModel.Value.ToString() == "5" || txtLeaveModel.Value.ToString() == "7")
            {
                sSQL = "sp_GetEmployeeLeavePolicyByMonthly";
            }
            else
            {
                sSQL = "sp_GetEmployeeLeavePolicyByMonthly";
            }

            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@empid", Utility.ToInteger(emp_code));
            parms[1] = new SqlParameter("@year", Utility.ToString(drpYear.SelectedItem.Value));
            parms[2] = new SqlParameter("@leavetype", leavetype);
            parms[3] = new SqlParameter("@compolicyleave", compolicyleave);
            parms[4] = new SqlParameter("@currentleaveearned", currentleaveearned);
            DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid7.Visible = false;
            RadGrid8.Visible = true;
            RadGrid8.DataSource = ds;
            RadGrid8.MasterTableView.DataSource = ds;
            RadGrid8.DataBind();
        }

        protected void btnYOSUpdate_Click(object sender, EventArgs e)
        {
            int year = Utility.ToInteger(drpYear.SelectedItem.Value);
            DataSet ds = new DataSet();
            int iCount = 0;
            foreach (GridItem item in RadGrid9.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    string strssqlb = "";
                    GridDataItem dataItem = (GridDataItem)item;
                    int id = Utility.ToInteger(this.RadGrid9.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                    double txtBoxValue1 = Utility.ToDouble(((TextBox)dataItem["LAColumn"].Controls[1]).Text.ToString());
                    double txtBoxValue2 = Utility.ToDouble(((TextBox)dataItem["LYColumn"].Controls[1]).Text.ToString());
                    if ((txtBoxValue1 + txtBoxValue2) >= 0)
                    {
                        strssqlb = "Update YOSLeavesAllowed Set LeavesAllowed=" + txtBoxValue1 + ",LY_Leaves_Bal=" + txtBoxValue2 + " Where ID=" + id;
                        if ((txtBoxValue1 + txtBoxValue2) > 0)
                        {
                            iCount++;
                        }
                    }
                    else
                    {
                        strssqlb = "Update YOSLeavesAllowed Set LeavesAllowed=0 Where ID=" + id;
                    }
                    if (iCount <= 1)
                    {
                        DataAccess.FetchRS(CommandType.Text, strssqlb, null);
                    }
                }
            }

            if (iCount > 1)
            {
                strMessage = "Only One Record Can be updated for YOS with value more than zero.";
                RadGrid9.DataBind();
            }
            else
            {
                strMessage = "Records Updated Successfully.";
                BindleavepolicyNormal();
                RadGrid9.DataBind();
            }

        }
        protected void btnLeaveUpdate_Click(object sender, EventArgs e)
        {
            if (btnLeaveUpdate.Text.ToString() == "Update")
            {
                int year = Utility.ToInteger(drpYear.SelectedItem.Value);
                DataSet ds = new DataSet();
                foreach (GridItem item in RadGrid7.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        string strssqlb = "";
                        GridDataItem dataItem = (GridDataItem)item;
                        int leavetype = Utility.ToInteger(this.RadGrid7.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        double leaveallowed = Utility.ToDouble(this.RadGrid7.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("LeavesAllowed"));
                        double txtBoxValue1 = Utility.ToDouble(((TextBox)dataItem["LAColumn"].Controls[1]).Text.ToString());
                        double txtBoxValue2 = Utility.ToDouble(((TextBox)dataItem["LYColumn"].Controls[1]).Text.ToString());
                        if (leaveallowed > 0 && txtBoxValue1 == 0)
                        {
                            //strssqlb = "Delete From EmployeeLeavesAllowed Where Emp_ID=" + emp_code + " And Leave_Type=" + leavetype + " And Leave_Year=" + year;
                        }
                        if (leaveallowed <= 0 && txtBoxValue1 >= 0)
                        {
                            strssqlb = "Select isnull(Count(ID),0) CntLeave From EmployeeLeavesAllowed Where Emp_ID=" + emp_code + " And Leave_Type=" + leavetype + " And Leave_Year=" + year;
                            ds = getDataSet(strssqlb);
                            if (Convert.ToInt16(ds.Tables[0].Rows[0]["CntLeave"].ToString()) >= 1)
                            {
                                if (((TextBox)dataItem["LAColumn"].Controls[1]).Text.Trim().ToString().Length > 0)
                                {
                                    strssqlb = "Update EmployeeLeavesAllowed Set Leaves_Allowed=" + txtBoxValue1 + ",LY_Leaves_Bal=" + txtBoxValue2 + " Where Emp_ID=" + emp_code + " And Leave_Type=" + leavetype + " And Leave_Year=" + year;
                                    strMessage = "Records Updated Successfully.";
                                }
                                else
                                {
                                    strssqlb = "Delete From EmployeeLeavesAllowed Where Emp_ID=" + emp_code + " And Leave_Type=" + leavetype + " And Leave_Year=" + year;
                                    strMessage = "Records Updated Successfully.";
                                }
                            }
                            else
                            {
                                if (((TextBox)dataItem["LAColumn"].Controls[1]).Text.Trim().ToString().Length > 0)
                                {
                                    strssqlb = "Insert Into EmployeeLeavesAllowed (Emp_ID, Leave_Type, LY_Leaves_Bal, Leaves_Allowed, Leave_Year) Values (" + emp_code + "," + leavetype + "," + txtBoxValue2 + "," + txtBoxValue1 + "," + year + ")";
                                    strMessage = "Records Updated Successfully.";
                                }
                            }
                        }
                        
                        if (leaveallowed > 0 && txtBoxValue1 >= 0)
                        {
                            strssqlb = "Update EmployeeLeavesAllowed Set Leaves_Allowed=" + txtBoxValue1 + ",LY_Leaves_Bal=" + txtBoxValue2 + " Where Emp_ID=" + emp_code + " And Leave_Type=" + leavetype + " And Leave_Year=" + year;
                            strMessage = "Records Updated Successfully.";
                        }
                        
                        if (strssqlb.Length > 0)
                        {
                            DataAccess.FetchRS(CommandType.Text, strssqlb, null);
                        }

                        if (leavetype == 8)
                        {
                            string yosupdate = "update YOSLeavesAllowed set leavesallowed='" + txtBoxValue1 + "' , LY_Leaves_bal='" + txtBoxValue2 + "' where emp_id='" + emp_code + "' and GETDATE() between startdate and DATEADD(dd, 1, enddate)";
                            DataAccess.FetchRS(CommandType.Text, yosupdate, null);
                        }
                        //

                    }
                }
                BindleavepolicyNormal();
            }
            else
            {
                btnLeaveUpdate.Text = "Update";
                BindleavepolicyNormal();
            }
        }

        protected void RadGrid7_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "History")
            {
                btnLeaveUpdate.Enabled = true;
                btnLeaveUpdate.Visible = true;
                btnLeaveUpdate.Text = "Go Back";
                GridDataItem dataItem = (GridDataItem)e.Item;
                int leavetype = Utility.ToInteger(this.RadGrid7.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                double CLColumn = Utility.ToDouble(((Label)dataItem["CLColumn"].Controls[1]).Text.ToString());
                double LAColumn = Utility.ToDouble(((TextBox)dataItem["LAColumn"].Controls[1]).Text.ToString());
                BindChildleavepolicyNormal(leavetype, CLColumn, LAColumn);
            }
        }
        protected void RadGrid7_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
          
        }

        int leave_model;
        protected void RadGrid7_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //r (if "hybrid" leave model  -hide the CAL  column
            #region (if "hybrid" leave model  -hide the CAL  column)
            //string ssql91 = "select leave_model from Company where Company_Id='"+ compid +"'";
            //DataSet ds1 = new DataSet();
            //ds1 = DataAccess.FetchRS(CommandType.Text, ssql91, null);

            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    leave_model = Convert.ToInt32(ds1.Tables[0].Rows[0]["leave_model"]);
            //}



            //if (leave_model >= 9)
            if (Convert.ToInt32(Session["Leave_Model"]) == 9)
            {
                //RadGrid7.MasterTableView.GetColumn("CLColumn").Visible = false;
            }

            #endregion


            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                    if (id == "12")
                    {
                        //((TextBox)e.Item.FindControl("txtLA")).Visible = false;
                        //((Label)e.Item.FindControl("lblChildCare")).Visible = true;
                    }
                    else
                    {
                        //((TextBox)e.Item.FindControl("txtLA")).Visible = true;
                        //((Label)e.Item.FindControl("lblChildCare")).Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                        ErrMsg = "<font color = 'Red'>Record can not be viewed</font>";
                    RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to view record. Reason:</font> " + ErrMsg));
                    e.Canceled = true;
                }

                try
                {

                    if (Convert.ToInt32(Session["Leave_Model"]) == 9 || Convert.ToInt32(Session["Leave_Model"]) == 10)
                    {
                        this.RadGrid7.Columns[3].Visible = false;
                    }
                    
                }
                catch (Exception ex)
                { }
                

            }
        }

        protected void RadGrid8_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                

                try
                {
                    if (Convert.ToInt32(Session["Leave_Model"]) == 9 || Convert.ToInt32(Session["Leave_Model"]) == 10)
                    {
                        this.RadGrid8.Columns[3].Visible = false;
                    }
                }
                catch (Exception ex)
                { }


            }
        }


        protected void radItemIssued_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Item Issued")) == false)
            //{
            //    radItemIssued.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    radItemIssued.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //    radItemIssued.MasterTableView.GetColumn("EditColumn").Visible = false;
            //}
        }
        protected void RadGrid4_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Employee Where 1 =2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Family which is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Family] WHERE [id] =" + id;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Family History is Deleted Successfully."));

                        }
                        else
                        {
                            RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Family History."));
                        }

                    }
                    RadGrid4.DataBind();
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
        }

        protected void RadGrid10_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Employee Where 1 =2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid10.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Bank Info which is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeBankInfo] WHERE [id] =" + id;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            RadGrid10.Controls.Add(new LiteralControl("<font color = 'Red'>Bank Info History is Deleted Successfully."));

                        }
                        else
                        {
                            RadGrid10.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Bank Info History."));
                        }

                    }
                    LoadBankGrid();
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
        }
        protected void RadGrid6_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                    editedItem["DeleteColumn"].Visible = true;
                    if (iCountOld > 0)
                    {
                        editedItem["DeleteColumn"].Visible = false;
                    }
                    iCountOld++;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                }
            }


        }


        protected void CustomizeDay(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {

            if (e.Day.Date.Day > 1)
            {
                // if you are using the skin bundled as a webresource("Default"), the Skin property returns empty string
                string calendarSkin = rdFrom.Calendar.Skin != "" ? rdFrom.Calendar.Skin : "Default";
                string otherMonthCssClass = "rcOutOfRange";

                // clear the default cell content (anchor tag) as we need to disable the hover effect for this cell
                e.Cell.Text = "";
                e.Cell.CssClass = otherMonthCssClass; //set new CssClass for the disabled calendar day cells (e.g. look like other month days here)

                // render a span element with the processed calendar day number instead of the removed anchor -- necessary for the calendar skinning mechanism 
                Label label = new Label();
                label.Text = e.Day.Date.Day.ToString();
                e.Cell.Controls.Add(label);

                // disable the selection for the specific day
                RadCalendarDay calendarDay = new RadCalendarDay();
                calendarDay.Date = e.Day.Date;
                calendarDay.IsSelectable = false;
                calendarDay.ItemStyle.CssClass = otherMonthCssClass;
                rdFrom.Calendar.SpecialDays.Add(calendarDay);
            }

        }


        protected void RadGrid6_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Employee Where 1 =2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid6.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Progression which is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeePayHistory] WHERE [id] =" + id;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            iCountOld = 0;
                            RadGrid6.DataSource = new string[] { };

                            dsnew = new DataSet();
                            string strSQL = "Select C.Currency,EH.ID, EH.Emp_ID, SUBSTRING(CONVERT(VARCHAR(11), EH.FromDate, 113), 4, 8) FromDate, EH.ToDate, EH.ConfirmationDate, EH.DepartmentID, EH.DesignationID, OT_Entitlement=Case When EH.OT_Entitlement='Y' Then 'Yes' Else 'No' End,CPF_Entitlement=Case When EH.CPF_Entitlement='Y' Then 'Yes' Else 'No' End,EH.OT1Rate, EH.OT2Rate, Pay_Frequency=Case When EH.Pay_Frequency='M' Then 'Monthly' Else 'Hourly' End,  EH.WDays_Per_Week, convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), EH.payrate)) as payrate,  Hourly_Rate_Mode=Case When EH.Hourly_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Hourly_Rate,  Daily_Rate_Mode=Case When EH.Daily_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Daily_Rate ,SUBSTRING(CONVERT(VARCHAR(11), FromDate, 113), 4, 8) FromDateCopy,  Convert(varchar,ToDate,103) ToDateCopy,  Convert(varchar,ConfirmationDate,103) ConfirmationDateCopy,  DE.Designation, DP.DeptName From EmployeePayHistory EH Left Outer Join Designation DE On EH.DesignationID = DE.ID Left Outer Join Department  DP On EH.DepartmentID=DP.ID Left Outer Join Currency C on  C.Id =EH.CurrencyID Where EH.Emp_ID= " + txtEmpCodeHdn.Value.ToString() + " And EH.SoftDelete=0 Order By EH.CreatedDate+EH.FromDate Desc";
                            dsnew = getDataSet(strSQL);
                            RadGrid6.DataSource = dsnew.Tables[0];
                            RadGrid6.DataBind();

                            RadGrid6.Controls.Add(new LiteralControl("<font color = 'Red'>Progression History is Deleted Successfully."));

                        }
                        else
                        {
                            RadGrid6.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete Progression History."));
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }
        protected void radItemIssued_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Employee Where 1 =2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Issued which is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeItemIssued] WHERE [id] =" + id;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Item Issued is Deleted Successfully."));

                        }
                        else
                        {
                            radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Issued."));
                        }

                    }
                    radItemIssued.DataBind();
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                radItemIssued.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }
        protected void radItemIssued_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_EmployeeItemIssuedSerial"))
                    ErrMsg = "Item Serial Number ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'SerialNumber'"))
                    ErrMsg = "Please Enter Item Serial Number";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Quantity'"))
                    ErrMsg = "Please Enter Quantity";

                DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            }
            else
            {
                DisplayMessage("Project added successfully.");
            }
        }
        protected void radItemIssued_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_EmployeeItemIssuedSerial"))
                    ErrMsg = "Item Serial Number ID already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'SerialNumber'"))
                    ErrMsg = "Please Enter Item Serial Number";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Quantity'"))
                    ErrMsg = "Please Enter Quantity";

                DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            }
            else
            {
                DisplayMessage("Project updated successfully.");
            }
        }
        private void DisplayMessage(string text)
        {
            radItemIssued.Controls.Add(new LiteralControl(text));
        }

        protected void sendemail()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string emailreq = "";
            int SMTPPORT = 25;
            string body = "";
            string cc = "";

            string SQL = "select email from employee where Emp_Code=" + EmpCode;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                from = Utility.ToString(dr.GetValue(0));
            }
            SQL = "select email_sender_domain from company where company_id=1";
            dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                to = Utility.ToString(dr.GetValue(0));
            }
            string sSQL = "sp_submit_email1";
            SqlParameter[] parm = new SqlParameter[1];
            parm[0] = new SqlParameter("@comp_id", Utility.ToInteger(Session["compid"]));
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parm);
            while (dr1.Read())
            {
                SMTPserver = Utility.ToString(dr1.GetValue(0));
                SMTPUser = Utility.ToString(dr1.GetValue(1));
                SMTPPass = Utility.ToString(dr1.GetValue(2));
                emailreq = Utility.ToString(dr1.GetValue(5)).ToLower();
                SMTPPORT = Utility.ToInteger(dr1.GetValue(4));
                body = Utility.ToString(dr1.GetValue(3));
                cc = Utility.ToString(dr1.GetValue(8));
            }
            if (emailreq == "yes")
            {
                string subject = "Employee, " + Utility.ToString(EmpName) + " Added in Company " + Session["CompanyName"].ToString();
                body = subject;

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Utility.ToInteger(Session["Compid"]));

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                        Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                    else
                        Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");

                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;
                }
            }
        }
    }
}

