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
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace SMEPayroll.TimeSheet
{
    public partial class RetailRoaster : System.Web.UI.Page
    {
        protected int comp_id;
        protected ArrayList EmployeeListAray;

        string sSQL, sSQL_date;
        protected void Page_Load(object sender, EventArgs e)
        {
          

            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }
            comp_id = Utility.ToInteger(Session["Compid"]);

         //  postvalue = this.Request.Params.Get("__EVENTARGUMENT");

            //comp_id = 4;// remove this /////////////////////////////////////////////////
            if (!IsPostBack)
            {
                //load Project Dropdown
                LoadSubProjectDropdown();

                if (lblFrom.Text.ToString() != "-")//page load 
                {
                    TableHeader.Visible = false;

                    //load roaster table
                    LoadRoaster();

                    //load employe table
                    loadEmployee();
                }
               
            }

            rdPRDate.SelectedDateChanged += new Telerik.Web.UI.Calendar.SelectedDateChangedEventHandler(rdPRDate_SelectedDateChanged);
            
        

            if (rdPRDate.SelectedDate.ToString() != "" )
            {
                LoadFromAndToDate();

           
            }

            //if we call from javascript (__doPostBack('form1', '')
            if (lblFrom.Text.ToString() != "-" && drpProject.SelectedValue.ToString()!= "")//page load 
            {
                TableHeader.Visible = true;

                //load roaster table
                LoadRoaster();
            }


        }
        [System.Web.Services.WebMethod]
        public static string insert_remark(string _remark ,string date,string outlet)
        {
            DateTime dtNow = System.DateTime.Parse(date);
            int delta = Convert.ToInt32(dtNow.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;
            DateTime moday = dtNow.AddDays(1 - delta);
            DateTime sunday = dtNow.AddDays(7 - delta);

            string MONDAY = moday.ToString("dd MMM yy");
            string SUNDAY = sunday.ToString("dd MMM yy");
         

            string sql = "update RosterRemark set Remark ='" +_remark + "'where FromDate=CONVERT(VARCHAR,'" + MONDAY + "',106)and  OutletId='" + outlet + "' IF @@ROWCOUNT = 0 insert into RosterRemark (FromDate,EndDate,OutletId,Remark) values(CONVERT(VARCHAR,'" + MONDAY + "',106),CONVERT(VARCHAR,'" + SUNDAY + "',106)," + outlet + ",'" + _remark + "')";

            DataAccess.ExecuteReader(CommandType.Text, sql, null);

            return "Updated";
        }


        
        //public void update_button_ServerClick(object sender, EventArgs e)
        //{

        //    DateTime dtNow = System.DateTime.Parse(rdPRDate.SelectedDate.ToString());
        //    int delta = Convert.ToInt32(dtNow.DayOfWeek);
        //    delta = delta == 0 ? delta + 7 : delta;
        //    DateTime moday = dtNow.AddDays(1 - delta);
        //    DateTime sunday = dtNow.AddDays(7 - delta);

        //    string MONDAY = moday.ToString("dd MMM yy");
        //    string SUNDAY = sunday.ToString("dd MMM yy");
        //    string outlet = drpProject.SelectedValue.ToString();

        //    string sql = "update RosterRemark set Remark ='" + this.remark.Text + "'where FromDate=CONVERT(VARCHAR,'" + MONDAY + "',106)and  OutletId='" + outlet + "' IF @@ROWCOUNT = 0 insert into RosterRemark (FromDate,EndDate,OutletId,Remark) values(CONVERT(VARCHAR,'" + MONDAY + "',106),CONVERT(VARCHAR,'" + SUNDAY + "',106),"+outlet+",'"+this.remark.Text+"')";

        //    DataAccess.ExecuteReader(CommandType.Text, sql, null);

        //}
        string _Monday, _Sunday;
        void rdPRDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            LoadFromAndToDate();
        }

        private void LoadFromAndToDate()
        {
           
            DateTime dtNow = System.DateTime.Parse(rdPRDate.SelectedDate.ToString());
            int delta = Convert.ToInt32(dtNow.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;
            DateTime moday = dtNow.AddDays(1 - delta);
            DateTime sunday = dtNow.AddDays(7 - delta);
            lblFrom.Text = moday.ToString("dd/MM/yyyy");
            lblTo.Text = sunday.ToString("dd/MM/yyyy");

          
        }


        protected void bindgrid1(object sender, ImageClickEventArgs e)
        {

            //Store dropdown value in session
            Session["outlet"] = drpProject.SelectedValue.ToString();

            //load employe table
            loadEmployee();

            //load roaster table
            LoadRoaster();
        }


       



        private void LoadSubProjectDropdown()
        {
            DataSet dsproj = new DataSet();
            string sSQL = "Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + comp_id.ToString() + "  OR LO.isShared='YES') AND SP.Active=1";
            dsproj = GetDataSet(sSQL);
            drpProject.DataSource = dsproj.Tables[0];
            drpProject.DataValueField = dsproj.Tables[0].Columns["ID"].ColumnName.ToString();
            drpProject.DataTextField = dsproj.Tables[0].Columns["Sub_Project_Name"].ColumnName.ToString();
            drpProject.DataBind();//Project dropdown
            drpProject.Items.Insert(0, "");
        }

       



        #region Load Grid
        public void loadEmployee()
        {
            DateTime dtNow = System.DateTime.Parse(rdPRDate.SelectedDate.ToString());
            int week_no = Utility.GetIso8601WeekOfYear(dtNow);

            TableHeader.Visible = true;
            //cbx15Times.Visible = true;

            //bind reapeter
            DataSet ds = new DataSet();

            sSQL = "select emp_code,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) as name from employee where company_id=" + comp_id + " and termination_date is null and emp_code in ( select Emp_ID from EmployeeAssignedToWorkersList) ";
            //km
            //sSQL = "select emp_code,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) as name from employee where company_id=" + comp_id + "and termination_date is null or DATEPART(WEEK,termination_date)=" + week_no + "  and emp_code in ( select Emp_ID from EmployeeAssignedToWorkersList) ";
            
            ds = GetDataSet(sSQL);
            repeater1.DataSource = ds;
            repeater1.DataBind();
            //
        }
        bool isOtHr = false;
        public void LoadRoaster()
        {

           

            string sql1 = @"select WeekRosterOtMode from Company where Company_id=" + comp_id;
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
            if (dr1.Read())
            {
                isOtHr = Convert.ToBoolean(dr1[0].ToString());
            }

            if (isOtHr)
            {
                cbx15Times.Visible = true;
            }


            
                        //load from DB
            DataSet ds = new DataSet();
            sSQL = "select Rid,R.Pattern as Pattern from Roaster_Pattern AS R INNER JOIN RosterDetail_Pattern AS D ON R.Rid=D.PatternId where subprojectid='" + drpProject.SelectedValue.ToString() + "' and  company_id='" + comp_id + "'and Rid in (select PatternId from RosterDetail_Pattern) ORDER BY D.InTime ";//check with company name
            
            ds = GetDataSet(sSQL);

            //load header date
            DataSet ds_date = new DataSet();
            //sSQL_date = "SELECT thedate FROM dbo.ExplodeDates('2013/07/01','2013/07/07') as Bdate";//change from selection

            DateTime dtNow = System.DateTime.Parse(rdPRDate.SelectedDate.ToString());
            int delta = Convert.ToInt32(dtNow.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;
            DateTime moday = dtNow.AddDays(1 - delta);
            DateTime sunday = dtNow.AddDays(7 - delta);

            sSQL_date = "SELECT thedate FROM dbo.ExplodeDates('" + moday.ToString("yyyy/MM/dd") + "','" + sunday.ToString("yyyy/MM/dd") + "'  ) as Bdate";//change from selection
            ds_date = GetDataSet(sSQL_date);


            //Building table in run time
            string tablestring = "";
            tablestring = tablestring + "<table width=\"100%\" id=\"tblPage\" runat=\"server\"  cellpadding=\"2px\"  >";

            //single row to show wile print
            tablestring = tablestring + "<tr><td  style=\"text-align:left\"  class=\"mark\" style=\"background-color:#2452B0\" colspan=\"10\"   > <font class=\"colheading\" height=\"10px\"><b>" + drpProject.SelectedItem.Text.ToString() + " - ( " + lblFrom.Text.ToString() + " To " + lblTo.Text.ToString() + " ) </b></font></td>";

            //header
            tablestring = tablestring + "<tr><td class=\"mark\" style=\"background-color:#2452B0\"   > <font class=\"colheading\" height=\"10px\"><b>Pattern  </b></font></td>";
            foreach (DataRow dr_date in ds_date.Tables[0].Rows)
            {
                tablestring = tablestring + "<td class=\"mark\" style=\"background-color:#2452B0\"  >" + Convert.ToDateTime(dr_date[0].ToString()).ToString("dd") + " - " + Convert.ToDateTime(dr_date[0].ToString()).ToString("ddd") + "</td>";
            }
            tablestring = tablestring + "</tr>";
            //End Header

            //data
            foreach (DataRow drrr in ds.Tables[0].Rows)//Roaster_Pattern
            {
                tablestring = tablestring + "<tr><td class=\"mark\" style=\"background-color:#2452B0\"  ><font class=\"colheading\" height=\"10px\"><b>" + drrr["Pattern"].ToString() + "</b></font></td>";
                foreach (DataRow dr_date in ds_date.Tables[0].Rows)//date
                {
                    //tablestring = tablestring + " <td id=\"td" + drrr["Rid"].ToString() + "\" >  <div class=\"drag t3\" id=\"18\" >Drag and drop me!</div>   </td>";
                    //EmployeeList Emp = GetEmployee(drrr["Rid"].ToString(), Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString()));
                    List<EmployeeList> _empList = GetEmployee(drrr["Rid"].ToString(), Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString()));

                    //format the date
                    string bindDate = Convert.ToString(Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString())).Replace("00:00:00", "").Replace("/", "");

                    StringBuilder divv = new StringBuilder();

                    if (_empList.Count > 0)
                    {
                        foreach (EmployeeList item in _empList)
                        {
                            divv.Append(" <div style=\"padding: 1px; margin: 3px; " + ChangeColorBased(item.Emp_code.ToString(), Convert.ToDateTime(((System.DateTime)(dr_date.ItemArray[0])).Date.Date.ToString())) + "   \" class=\"drag t3\" id=\"" + item.Emp_code.ToString() + "\" > " + item.Name.ToString() + "   </div>");

                            if (!string.IsNullOrEmpty(item.Remark))
                            {
                                this.remark.Text = item.Remark;
                            }
                        
                        }

                        tablestring = tablestring + " <td id=\"" + drrr["Rid"].ToString() + "_" + bindDate + "\"  style=\"height:100px\"   > " + divv.ToString() + "  </td>";
                    }
                    else
                    {
                        tablestring = tablestring + " <td id=\"" + drrr["Rid"].ToString() + "_" + bindDate + "\" style=\"height:100px\"  >  </td>";
                    }


                }
                tablestring = tablestring + "</tr>";
               
            }

            //data end

            #region leave row
            tablestring = tablestring + "<tr><td class=\"mark\">Leave</td>";
            StringBuilder divv_leave = new StringBuilder();

            foreach (DataRow dr_date_leave in ds_date.Tables[0].Rows)//date
            {
                List<EmployeeList> _empLeave = GetEmployeeOnLeave(Convert.ToDateTime(((System.DateTime)(dr_date_leave.ItemArray[0])).Date.Date.ToString()));
                if (_empLeave.Count > 0)
                {
                    foreach (EmployeeList itemLeave in _empLeave)
                    {
                        divv_leave.Append(" <div style=\"padding: 1px; margin: 3px; \"   > " + itemLeave.Name.ToString() + "   </div>");
                    }
                }
                tablestring = tablestring + " <td class=\"mark\"> " + divv_leave.ToString() + "  </td>";
                divv_leave.Length=0;// clear the string builder
            }
           
            tablestring = tablestring + "</tr>";
            #endregion

            //trash row
            //class=\"trash\"
            tablestring = tablestring + "<tr> <td colspan=\"50\" class=\"trash\"  height=\"50px\" id=\"unasignemployee\" >drag here to Unassign!  </td>  </td>";
            //


            tablestring = tablestring + "</table>";



            TableDiv.InnerHtml = tablestring;
           

            _Monday = moday.ToString("dd MMM yy");
            _Sunday = sunday.ToString("DD MMM yy");

            string remarksql = "select Remark from RosterRemark where FromDate=CONVERT(VARCHAR,'" + _Monday + "',106)and  OutletId='" + drpProject.SelectedValue.ToString()+ "'";
            this.remark.Text = "";
           SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, remarksql, null);
            while (dr2.Read())
            {
                this.remark.Text = dr2["Remark"].ToString();

            }
            dr2.Close();

        }

        private List<EmployeeList> GetEmployeeOnLeave(DateTime dateTime)
        {
            List<EmployeeList> _list = new List<EmployeeList>();
            string returnval = "";
            string EmployeeName;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sql1 = "select emp_id,(select (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) from employee where emp_code=EL.emp_id )as Name from emp_leaves EL inner join employee E on EL.emp_id=E.emp_code  where ([start_date]  =Convert(datetime,'" + dateTime + "',103)  or  [end_date]  =Convert(datetime,'" + dateTime + "',103) or  Convert(datetime,'" + dateTime + "',103)   between [start_date] and end_date) and [status]='Approved'and emp_id in ( select Emp_ID from EmployeeAssignedToWorkersList)  and Company_Id='" + comp_id + "'  ";

            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
            while (dr1.Read())
            {
                EmployeeList _el = new EmployeeList();
                _el.Emp_code = Utility.ToInteger(dr1["emp_id"].ToString());
                _el.Name = Utility.ToString(dr1["Name"].ToString());
                _list.Add(_el);
            }
            dr1.Close();

            return _list;
        }

        //if 1.5 times change color
        private string ChangeColorBased(string Emp, DateTime dateTime)
        {
            string returnval="";
            string sql1 = @"select * from RosterDetail where PullWorkTimein='8' and Roster_ID='" + Emp + "' and Roster_Date=Convert(datetime,'" + dateTime + "',103)";
             SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
             if (dr1.HasRows)
             {
                 returnval = "background-color:red;color:White;";
             }
             else
             {
                 returnval="";
             }

             return returnval;
            
        }



        // Employee with in the TD cell
        public List<EmployeeList> GetEmployee(string pattern, DateTime bDate)
        {

            List<EmployeeList> _list = new List<EmployeeList>();
            string returnval = "";
            string EmployeeName;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            //string sql1 = "select Roster_ID as emp_code,(select emp_name from employee where emp_code=R.Roster_ID) Name from RosterDetail R  where Roster_Date= + Convert(datetime,'" + bDate.Date.ToString("dd/MM/yyyy", format) + "',103)  and Pattern='" + pattern + "'";
            string sql1 = "select Roster_ID as emp_code,(select (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) from employee where emp_code=R.Roster_ID) Name,Remark from RosterDetail R  where Roster_Date= + Convert(datetime,'" + bDate.Date.ToString("dd/MM/yyyy", format) + "',103)  and Pattern='" + pattern + "' and Roster_ID in (select Emp_ID from MultiProjectAssigned where SubProjectID='" + drpProject.SelectedValue.ToString() + "' and  EntryDate=Convert(datetime,'" + bDate.Date.ToString("dd/MM/yyyy", format) + "',103))";
            
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
            while (dr1.Read())
            {
                EmployeeList _el = new EmployeeList();
                _el.Emp_code = Utility.ToInteger(dr1["emp_code"].ToString());
                _el.Name = Utility.ToString(dr1["Name"].ToString());
                _el.Remark = Utility.ToString(dr1["Remark"].ToString());
                _list.Add(_el);
            }
            dr1.Close();

            return _list;
        }

        #endregion

        #region Utility
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        #endregion

        #region javascript method-After drop update in database
        //http://www.aspsnippets.com/Articles/Calling-server-side-methods-using-JavaScript-and-JQuery-in-ASP.Net.aspx
        [System.Web.Services.WebMethod]
        public static string UpdateDB(string Source, string Emp, string target, string Checkboxval,string Remark)
        {
            //source 1_01072013 (pattern_date)
            if (Source != "FromEmpTable")
            {
                DeleteInDB(Source, Emp);
            }

            //source 1_01072013 (split pattern and date)
            SplitInfo _si = SplitPatternAndDate(target);

            //update database

            //string date = "01/08/2008";
            //string date = Convert.ToString(_si.month) + "/" + Convert.ToString(_si.day) + "/" + Convert.ToString(_si.year);
            string strMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_si.month);
            string date = Convert.ToString(_si.day) + "/" + strMonthName + "/" + Convert.ToString(_si.year);
            DateTime dt = Convert.ToDateTime(date);

            string dateString = dt.ToString("dd/MMM/yyyy");
            //if we drag from employee table then the emp is is 18co insterd of 18
            try
            {

                int Emp_temp = Convert.ToInt32(Emp);
            }
            catch
            {
                Emp = Emp.Substring(0, Emp.Length - 2);
            }


            string status = "init";
            string sql1 = @"select * from RosterDetail where Roster_ID='" + Emp + "' and Roster_Date='" + dateString + "'and Pattern='" + _si.Pattern + "'";
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
            if (dr1.HasRows)
            {
                status = "update in db";// no need because we are deleting the previous record.

            }
            else
            {
                status = "Insert Sucessfully";
                // string sqlinsert = "INSERT INTO [dbo].[RosterDetail]   ([Roster_ID] ,[Roster_Date],[FIFO],[Rounding],[BreakTimeAfter],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeNH],[BreakTimeOT],[RosterType],[PullWorkTimein],[FlexibleWorkinghr],[NightShift],[CreateDate],[BreakTimeAftOtFlx],[Pattern])   VALUES";
                // string sqlinsert = "INSERT INTO [dbo].[RosterDetail]([Roster_ID] ,[Roster_Date],[FIFO],[Rounding],[BreakTimeAfter],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeNH],[BreakTimeOT],[RosterType],[PullWorkTimein],[FlexibleWorkinghr],[NightShift],[BreakTimeAftOtFlx],[Pattern])   VALUE (" + Emp + ",'" + dt + "','0',0.00,09:00,18:00,09:00,09:00,18:00,0,0,0,0,09:00,18:30,0,0,'NORMAL',0,0,0,NULL,"+_si.Pattern+" )";


                
                //string sqlinsert = "insert into [dbo].[RosterDetail]([Roster_ID] ,[Roster_Date],[pattern]) values("c",Convert(datetime,'" + dt + "',103)," + _si.Pattern + " )";
                string sqlinsert = "INSERT INTO [dbo].[RosterDetail]   ([Roster_ID] ,[Roster_Date],[FIFO],[Rounding],[BreakTimeAfter],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeNH],[BreakTimeOT],[RosterType],[PullWorkTimein],[FlexibleWorkinghr],[NightShift],[CreateDate],[BreakTimeAftOtFlx],[Pattern],Remark)";
                //sqlinsert += "SELECT " + Emp + " as  [PatternId],Convert(datetime,'" + dt + "',103) as [Roster_Date],[FIFO],[Rounding],[BreakTimeAfter],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeNH],[BreakTimeOT],[RosterType],[PullWorkTimein] ,[FlexibleWorkinghr],[NightShift],[CreateDate],[BreakTimeAftOtFlx]," + _si.Pattern + " as [Pattern] FROM [RosterDetail_Pattern]  where [PatternId]=" + _si.Pattern + "";
                sqlinsert += "SELECT " + Emp + " as  [PatternId],Convert(datetime,'" + dt + "',103) as [Roster_Date],[FIFO],[Rounding],[BreakTimeAfter],[InTime],[OutTime],[EarlyInBy],[LateInBy],[EarlyOutBy],[LateOutBy],[ClockInBefore],[ClockInAfter],[ClockOutBefore],[ClockOutAfter],[BreakTimeNHhr],[BreakTimeOThr],[BreakTimeNH],[BreakTimeOT],[RosterType],'" + Checkboxval + "' as [PullWorkTimein] ,[FlexibleWorkinghr],[NightShift],[CreateDate],[BreakTimeAftOtFlx]," + _si.Pattern + " as [Pattern],'"+Remark+"' FROM [RosterDetail_Pattern]  where [PatternId]=" + _si.Pattern + "";
                
                DataAccess.ExecuteStoreProc(sqlinsert);

                //store in MultiProjectAssigned
                string sqlProject = "INSERT INTO [dbo].[MultiProjectAssigned] ([SubProjectID],[EntryDate],[Emp_ID] ,[CreatedDate])  VALUES     (" + System.Web.HttpContext.Current.Session["outlet"].ToString() + ",Convert(datetime,'" + dt + "',103)," + Emp + ",getdate())";
                DataAccess.ExecuteStoreProc(sqlProject);


            }

            // return "patern=" + _si.Pattern + " date=" + _si.day+"month="+_si.month+"year="+_si.year;
            return status;
        }
        #endregion


        #region JavaScript Method- while move delete the source cell in db
        [System.Web.Services.WebMethod]
        public static string DeleteInDB(string Source, string Emp)
        {
            //source 1_01072013 (split pattern and date)
            SplitInfo _si = SplitPatternAndDate(Source);

            //update database

            //string date = "01/08/2008";
            string date = Convert.ToString(_si.day) + "/" + Convert.ToString(_si.month) + "/" + Convert.ToString(_si.year);
            DateTime dt = Convert.ToDateTime(date);


            //if we drag from employee table then the emp is is 18co insterd of 18
            try
            {

                int Emp_temp = Convert.ToInt32(Emp);
            }
            catch
            {
                Emp = Emp.Substring(0, Emp.Length - 2);
            }


            try
            {
                string DelSQL = "delete from RosterDetail where Roster_ID='" + Emp + "' and Roster_Date=Convert(datetime,'" + dt + "',103)  and Pattern='" + _si.Pattern + "'";
                DataAccess.ExecuteStoreProc(DelSQL);

                //store in MultiProjectAssigned
                //string sqlProject = "INSERT INTO [dbo].[MultiProjectAssigned] ([SubProjectID],[EntryDate],[Emp_ID] ,[CreatedDate])  VALUES     (" + System.Web.HttpContext.Current.Session["outlet"].ToString() + ",Convert(datetime,'" + dt + "',103)," + Emp + ",getdate())";
                string sqlProject_delete = "delete from  [dbo].[MultiProjectAssigned] where Emp_ID='" + Emp + "' and  [EntryDate]=Convert(datetime,'" + dt + "',103) and [SubProjectID]=" + System.Web.HttpContext.Current.Session["outlet"].ToString() + "";
                DataAccess.ExecuteStoreProc(sqlProject_delete);

            }
            catch (Exception e)
            {
                //string strUpdateDelSQL = "INSERT INTO [dbo].[AccomodationMasterTable] ( [AccomodationName]  )  VALUES  (" + e.Message.ToString() + ")";//replace error table
                //DataAccess.ExecuteStoreProc(strUpdateDelSQL);
            }

            // return "patern=" + _si.Pattern + " date=" + _si.day+"month="+_si.month+"year="+_si.year;
            return "deleted Sucessfully";
        }


        #endregion

        #region javaScript Method -validation
        //validate if same employee is assign already in some other pattern
        [System.Web.Services.WebMethod]
        public static string validateWhetherAssignAlready(string Source, string Emp, string SourceTable)
        {
            string msg = "";
            string statusCode ;
            string status_code="";
            //source 1_01072013 (split pattern and date)
            SplitInfo _si = SplitPatternAndDate(Source);

            //update database

            //string date = "01/08/2008";
            //string date = Convert.ToString(_si.month) + "/" + Convert.ToString(_si.day) + "/" + Convert.ToString(_si.year);
            string date = Convert.ToString(_si.day) + "/" + Convert.ToString(_si.month) + "/" + Convert.ToString(_si.year);
            DateTime dt = Convert.ToDateTime(date);

            //if we drag from employee table then the emp is is 18co insterd of 18
            try
            {

                int Emp_temp = Convert.ToInt32(Emp);
            }
            catch
            {
                Emp = Emp.Substring(0, Emp.Length - 2);
            }


            string VSql;
            if (SourceTable == "FromEmpTable")
            {
                VSql = "select * from RosterDetail where Roster_ID='" + Emp + "' and Roster_Date=Convert(datetime,'" + dt + "',103) ";
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, VSql, null);
                if (dr1.HasRows)
                {
                    statusCode = "100%"+"100";
                }
                else
                {
                    statusCode = "101%"+"101";
                }
            }
            else//with in same roaster table
            {
            
                statusCode = "101%"+"101";
            }

         
            //check whether he is assigned to different project on same day
            //
            SplitInfo _si_1 = SplitPatternAndDate(SourceTable);
            string date1 = Convert.ToString(_si_1.day) + "/" + Convert.ToString(_si_1.month) + "/" + Convert.ToString(_si_1.year);
            DateTime dt1 = Convert.ToDateTime(date1);

            //string Sql_AssignToDifferentProject = "select * from MultiProjectAssigned where Emp_id='" + Emp + "' and EntryDate=Convert(datetime,'" + dt1 + "',103)  ";
            string Sql_AssignToDifferentProject = " select b.Sub_Project_Name as Project from MultiProjectAssigned as a inner join SubProject as b on a.SubProjectID=b.Parent_Project_ID where Emp_id='" + Emp + "' and EntryDate=Convert(datetime,'" + dt1 + "',103)  ";
           
            //ku
           // string Sql_AssignToDifferentProject = " select b.Sub_Project_Name as Project from MultiProjectAssigned as a inner join SubProject as b on a.SubProjectID=b.Sub_Project_ID where Emp_id='" + Emp + "' and EntryDate=Convert(datetime,'" + dt1 + "',103)  ";
           
        

            SqlDataReader dr1_diffPro = DataAccess.ExecuteReader(CommandType.Text, Sql_AssignToDifferentProject, null);


            while (dr1_diffPro.Read())
            {
                msg = dr1_diffPro["Project"].ToString();
            }


            if (dr1_diffPro.HasRows)
            {
                statusCode = "102%"+msg;
                status_code="102%102";
            }
            //


            //employee in leave
            string Sql_leave = "select * from emp_leaves where  ([start_date]  =Convert(datetime,'" + dt1 + "',103)  or  [end_date]  =Convert(datetime,'" + dt1 + "',103)   or Convert(datetime,'" + dt1 + "',103)   between [start_date] and end_date)  and emp_id=" + Emp + " and [status]='Approved'";
            SqlDataReader dr1_Sql_leave = DataAccess.ExecuteReader(CommandType.Text, Sql_leave, null);
            if (dr1_Sql_leave.HasRows)
                {
                    statusCode = "103%" + "103";
                }
            //


            //Check whether he is terminated
                string Sql_ter = "select * from Employee where  [termination_date]<= Convert(datetime,'" + dt1 + "',103) and   emp_code='"+ Emp+"'";
            SqlDataReader dr1_Sql_Ter = DataAccess.ExecuteReader(CommandType.Text, Sql_ter, null);
            if (dr1_Sql_Ter.HasRows)
            {
                statusCode = "104%" + "104";
            }



            // Check no of days work in given week

            int delta = Convert.ToInt32(dt1.DayOfWeek);
            delta = delta == 0 ? delta + 7 : delta;

            DateTime start_date = dt1.AddDays(1 - delta);
            DateTime end_date = dt1.AddDays(7 - delta);

            string no_of_days_worked_in_given_week = "SELECT  count(SubProjectID) as totaldays  FROM [dbo].[MultiProjectAssigned]  where  EntryDate >= Convert(datetime,'" + start_date + "',103)  and  EntryDate <=Convert(datetime,'" + end_date+ "',103) and emp_id=" + Emp + "";

            double no_of_days=0.0;
          double no_of_days_can_work=0.0;

            SqlDataReader noOfDays = DataAccess.ExecuteReader(CommandType.Text, no_of_days_worked_in_given_week, null);

            string sql_weekdayswork = "select wdays_per_week from Employee where emp_code='" + Emp + "'";

            SqlDataReader weekdayadapter = DataAccess.ExecuteReader(CommandType.Text, sql_weekdayswork, null);
            while (weekdayadapter.Read())
            {
                no_of_days_can_work = Utility.ToDouble(weekdayadapter["wdays_per_week"].ToString());
            }



            while (noOfDays.Read())
            {
                no_of_days = Utility.ToDouble(noOfDays["totaldays"].ToString());
            }

            if (no_of_days >= no_of_days_can_work && status_code != "102%102" && statusCode != "103%103" && statusCode != "104%104")
            {
                statusCode = "105%105";
            }

            //if (no_of_days == 5.0 && no_of_days_can_work == 5.5 && status_code != "102%102" && statusCode != "103%103" && statusCode != "104%104")
            //{
            //    statusCode = "106%106";
            //}
           



           
            return statusCode;

        }
    
    


        //Validate birthday
        [System.Web.Services.WebMethod]
        public static string ValidateBirthday(string Source, string Emp, string target)
        {
         
            //source 1_01072013 (split pattern and date)
            SplitInfo _si = SplitPatternAndDate(target);

            //update database

            //string date = "01/08/2008";
            //string date = Convert.ToString(_si.month) + "/" + Convert.ToString(_si.day) + "/" + Convert.ToString(_si.year);
            string date = Convert.ToString(_si.day) + "/" + Convert.ToString(_si.month) + "/" + Convert.ToString(_si.year);
            DateTime dt = Convert.ToDateTime(date);

            //if we drag from employee table then the emp is is 18co insterd of 18
            try
            {

                int Emp_temp = Convert.ToInt32(Emp);
            }
            catch
            {
                Emp = Emp.Substring(0, Emp.Length - 2);
            }


            string status = "init";
            string sql1 = @"select  *  from employee where  month(date_of_birth)=month(Convert(datetime,'" + dt + "',103)) and day(date_of_birth)=day(Convert(datetime,'" + dt + "',103)) and emp_code='"+Emp+"'";
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
            if (dr1.HasRows)
            {
                status = "YES%yes";

            }
            else
            {
                status = "NO%no";
               
            }

       
            return status;
            

        }



        #endregion


        #region helper
        //split pattern,day,month,year
        private static SplitInfo SplitPatternAndDate(string Source)
        {
            SplitInfo _si = new SplitInfo();
            try
            {
                string[] split = Source.Split('_');

                _si.Pattern = Convert.ToInt32(split[0].ToString());
                _si.day = Convert.ToInt32(split[1].Substring(0, 2).ToString());
                _si.month = Convert.ToInt32(split[1].Substring(2, 2).ToString());
                _si.year = Convert.ToInt32(split[1].Substring(4, 4).ToString()); ;

            }
            catch (Exception e)
            {
                //string  strUpdateDelSQL = "INSERT INTO [dbo].[AccomodationMasterTable] ( [AccomodationName]  )  VALUES  ("+e.Message.ToString()+")";//replace error table
                // DataAccess.ExecuteStoreProc(strUpdateDelSQL); 
            }

            return _si;
        }
        #endregion



        protected void btnReport_Click(object sender, EventArgs e)
        {
            if (lblFrom.Text != "-")
            {
                Session["Fromdate"] = lblFrom.Text;
                Session["Todate"] = lblTo.Text;

                string str = @"RosterReport.aspx";
                //string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            }
            else
            {
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>alert('Please select the Week');</SCRIPT>");
            }
        }

    }

    #region Class
    public class EmployeeList
    {
        public int Emp_code;
        public string Name;
        public string Remark;
    }

    public class SplitInfo
    {
        public int Pattern;
        public int day;
        public int month;
        public int year;
    }

    #endregion
}
