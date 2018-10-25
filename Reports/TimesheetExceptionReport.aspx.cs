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

namespace SMEPayroll.Reports
{
    public partial class TimesheetExceptionReport : System.Web.UI.Page
    {
        DataSet dsResult = new DataSet();
        DataSet dsTimesheet = new DataSet();
        DataTable dtMain = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                AddItemToDropDown();
                if (Session["TSRFromDate"] == null)
                {
                    rdEmpPrjStart.DbSelectedDate = System.DateTime.Now.ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = System.DateTime.Now.ToShortDateString();

                    Session["TSRFromDate"] = System.DateTime.Now.ToShortDateString();
                    Session["TSRToDate"] = System.DateTime.Now.ToShortDateString();

                }
                else
                {
                    rdEmpPrjStart.DbSelectedDate = Convert.ToDateTime(Session["TSRFromDate"]).ToShortDateString();
                    rdEmpPrjEnd.DbSelectedDate = Convert.ToDateTime(Session["TSRToDate"]).ToShortDateString();
                }
            }
           
            
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void onListChanged(Object sender, EventArgs e)
        {
            AddItemToDropDown();
        }

        protected void AddItemToDropDown()
        {
            cmbExpReports.Items.Clear();
            if (chkList.SelectedValue == "Before")
            {
                cmbExpReports.Items.Add("No In Time");
                cmbExpReports.Items.Add("No Out Time");
                cmbExpReports.Items.Add("No In/Out Time");
                cmbExpReports.Items.Add("On Leave");
                cmbExpReports.Items.Add("Late In");
                cmbExpReports.Items.Add("Early Out");

            }
            else
            {
                cmbExpReports.Items.Add("On Leave");
                cmbExpReports.Items.Add("Late In");
                cmbExpReports.Items.Add("Early Out");
            }
        }

        private void bindgrid()
        {
            if (chkList.SelectedValue.ToString() == "After")
            {
                AfterApprove();
            }

            if (chkList.SelectedValue.ToString() == "Before")
            {
                BeforeApprove();
            }   

           
        }

        protected void AfterApprove()
        {
            string sDate = rdEmpPrjStart.SelectedDate.Value.Year + "-" + rdEmpPrjStart.SelectedDate.Value.Month + "-" + rdEmpPrjStart.SelectedDate.Value.Day;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Year + "-" + rdEmpPrjEnd.SelectedDate.Value.Month + "-" + rdEmpPrjEnd.SelectedDate.Value.Day;

            string strSQL = "";
            int compid = Utility.ToInteger(Session["Compid"]);
            

            if (cmbExpReports.SelectedItem.Text == "On Leave")
            {

                strSQL = "SELECT a.[Roster_ID],a.[Time_Card_No] [Time Card],s.Sub_Project_Name [Sub Project Name],(e.emp_name + ' ' + e.emp_lname) [Employee],e.emp_code,convert(char(10), TimeEntryEnd, 111) [End Date],convert(char(10), TimeEntryStart, 111) [Date],convert(char(5), TimeEntryStart, 108) [InTime],convert(char(5), TimeEntryEnd, 108) [OutTime]" +
                       "FROM  [ApprovedTimeSheet] as a INNER JOIN employee as e on e.time_card_no=a.Time_Card_No INNER JOIN SubProject s on s.Sub_Project_ID=a.Sub_Project_ID  " +
                       "where TimeEntryStart >= '" + sDate + "' and TimeEntryEnd <= '" + eDate + "' and e.Company_Id=" + compid + " " +
                       "and (convert(char(5), TimeEntryEnd, 108)<>'00:00' and convert(char(5), TimeEntryStart, 108)<>'00:00') order by TimeEntryStart, e.emp_name  ";
            }

            if (cmbExpReports.SelectedItem.Text == "Late In")
            {

                strSQL = "SELECT a.[Roster_ID],a.[Time_Card_No] [Time Card],s.Sub_Project_Name [Sub Project Name],(e.emp_name + ' ' + e.emp_lname) [Employee],e.emp_code,convert(char(10), TimeEntryEnd, 111) [End Date],convert(char(10), TimeEntryStart, 111) [Date],convert(char(5), TimeEntryStart, 108) [InTime],convert(char(5), TimeEntryEnd, 108) [OutTime]" +
                       "FROM  [ApprovedTimeSheet] as a INNER JOIN employee as e on e.time_card_no=a.Time_Card_No INNER JOIN SubProject s on s.Sub_Project_ID=a.Sub_Project_ID  " +
                       "where TimeEntryStart >= '" + sDate + "' and TimeEntryEnd <= '" + eDate + "' and e.Company_Id=" + compid + " " +
                       "and (convert(char(5), TimeEntryEnd, 108)<>'00:00' and convert(char(5), TimeEntryStart, 108)<>'00:00') order by TimeEntryStart, e.emp_name  ";
            }

            if (cmbExpReports.SelectedItem.Text == "Early Out")
            {

                strSQL = "SELECT a.[Roster_ID],a.[Time_Card_No] [Time Card],s.Sub_Project_Name [Sub Project Name],(e.emp_name + ' ' + e.emp_lname) [Employee],e.emp_code,convert(char(10), TimeEntryEnd, 111) [End Date],convert(char(10), TimeEntryStart, 111) [Date],convert(char(5), TimeEntryStart, 108) [InTime],convert(char(5), TimeEntryEnd, 108) [OutTime]" +
                       "FROM  [ApprovedTimeSheet] as a INNER JOIN employee as e on e.time_card_no=a.Time_Card_No INNER JOIN SubProject s on s.Sub_Project_ID=a.Sub_Project_ID  " +
                       "where TimeEntryStart >= '" + sDate + "' and TimeEntryEnd <= '" + eDate + "' and e.Company_Id=" + compid + " " +
                       "and (convert(char(5), TimeEntryEnd, 108)<>'00:00' and convert(char(5), TimeEntryStart, 108)<>'00:00') order by TimeEntryStart, e.emp_name  ";
            }


            dsResult = DataAccess.FetchRS(CommandType.Text, strSQL, null);
            Session["ds"] = dsResult;

            DataTable dtResult = new DataTable();
            dtResult = dsResult.Tables[0].Copy();



            if (cmbExpReports.SelectedItem.Text == "On Leave")
            {
                try
                {
                    dtResult.Rows.Clear();
                    strSQL = "Select emp_id, convert(char(10), leave_date, 111) leave_date from emp_leaves e Inner join emp_leaves_detail el on e.trx_id=el.trx_id " +
                             "where [start_date]>= '" + sDate + "' and [end_date]<= '" + eDate + "' and [status]='Approved' " +
                             "and el.halfday_leave=0";

                    DataSet dsLeave = new DataSet();
                    dsLeave = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                    for (int i = 0; i < dsLeave.Tables[0].Rows.Count; i++)
                    {
                        DataRow[] results = dsResult.Tables[0].Select("emp_code=" + int.Parse(dsLeave.Tables[0].Rows[i]["emp_id"].ToString()) + " and [Date]='" + dsLeave.Tables[0].Rows[i]["leave_date"].ToString() + "'");
                        if (results.Length > 0)
                        {
                            foreach (DataRow d in results)
                            {
                                dtResult.ImportRow(d);
                            }
                        }
                    }

                    dtResult.Columns.Remove("Roster_ID");
                    dtResult.Columns.Remove("emp_code");
                    dtResult.Columns.Remove("End Date");
                }
                catch (Exception e)
                { }
            }

            if (cmbExpReports.SelectedItem.Text == "Late In")
            {
                try
                {
                    dtResult.Rows.Clear();
                    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                    {
                        int roster_id = 0;
                        string roster_date = "";
                        string InTime = "";
                        string SQL = "";
                        DataTable dt = new DataTable();

                        roster_id = int.Parse(dsResult.Tables[0].Rows[i]["Roster_ID"].ToString());
                        roster_date = dsResult.Tables[0].Rows[i]["Date"].ToString();
                        InTime = dsResult.Tables[0].Rows[i]["InTime"].ToString();

                        SQL = "select InTime,LateInBy from RosterDetail where Roster_ID=" + roster_id + " and RosterType='Normal' and Roster_Date='" + roster_date + "'";
                        dt = DataAccess.FetchRS(CommandType.Text, SQL, null).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            string R_InTime = "";
                            string R_LateIn = "";

                            R_InTime = dt.Rows[0][0].ToString();
                            try
                            {
                                R_LateIn = dt.Rows[0][1].ToString();
                            }
                            catch (Exception e) { }

                            TimeSpan end;
                            TimeSpan start = TimeSpan.Parse(InTime);
                            if (R_LateIn == "")
                            {
                                end = TimeSpan.Parse(R_InTime);
                                R_LateIn = R_InTime;
                            }
                            else
                            {
                                end = TimeSpan.Parse(R_LateIn);
                            }


                            TimeSpan result = start - end;
                            string diff = result.Hours.ToString("00") + ":" + result.Minutes.ToString("00");
                            int intresult = TimeSpan.Compare(start, end);

                            if (intresult > 0)
                            {
                                try
                                {
                                    dtResult.Columns.Add("Late In By");
                                    dtResult.Columns.Add("Late Mins");
                                }
                                catch (Exception e)
                                { }
                                dtResult.ImportRow(dsResult.Tables[0].Rows[i]);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Late In By"] = R_LateIn;
                                dtResult.Rows[dtResult.Rows.Count - 1]["Late Mins"] = diff.ToString();
                            }

                        }
                    }

                    dtResult.Columns.Remove("Roster_ID");
                    dtResult.Columns.Remove("emp_code");
                    dtResult.Columns.Remove("End Date");
                    dtResult.Columns.Remove("OutTime");
                }
                catch (Exception e)
                { }

            }


            if (cmbExpReports.SelectedItem.Text == "Early Out")
            {
                try
                {
                    dtResult.Rows.Clear();
                    for (int i = 0; i < dsResult.Tables[0].Rows.Count; i++)
                    {
                        int roster_id = 0;
                        string roster_date = "";
                        string OutTime = "";
                        string SQL = "";
                        DataTable dt = new DataTable();

                        roster_id = int.Parse(dsResult.Tables[0].Rows[i]["Roster_ID"].ToString());
                        roster_date = dsResult.Tables[0].Rows[i]["Date"].ToString();
                        OutTime = dsResult.Tables[0].Rows[i]["OutTime"].ToString();

                        SQL = "select OutTime,EarlyOutBy from RosterDetail where Roster_ID=" + roster_id + " and RosterType='Normal' and Roster_Date='" + roster_date + "'";
                        dt = DataAccess.FetchRS(CommandType.Text, SQL, null).Tables[0];

                        if (dt.Rows.Count > 0)
                        {
                            string R_OutTime = "";
                            string R_EarlyOut = "";

                            R_OutTime = dt.Rows[0][0].ToString();
                            try
                            {
                                R_EarlyOut = dt.Rows[0][1].ToString();
                            }
                            catch (Exception e) { }

                            TimeSpan end;
                            TimeSpan start = TimeSpan.Parse(OutTime);
                            if (R_EarlyOut == "")
                            {
                                end = TimeSpan.Parse(R_OutTime);
                                R_EarlyOut = R_OutTime;
                            }
                            else
                            {
                                end = TimeSpan.Parse(R_EarlyOut);
                            }


                            TimeSpan result = end - start;
                            string diff = result.Hours.ToString("00") + ":" + result.Minutes.ToString("00");
                            int intresult = TimeSpan.Compare(start, end);

                            if (intresult < 0)
                            {
                                try
                                {
                                    dtResult.Columns.Add("Early Out By");
                                    dtResult.Columns.Add("Early Mins");
                                }
                                catch (Exception e)
                                { }
                                dtResult.ImportRow(dsResult.Tables[0].Rows[i]);
                                dtResult.Rows[dtResult.Rows.Count - 1]["Early Out By"] = R_EarlyOut;
                                dtResult.Rows[dtResult.Rows.Count - 1]["Early Mins"] = diff.ToString();
                            }

                        }
                    }

                    dtResult.Columns.Remove("Roster_ID");
                    dtResult.Columns.Remove("emp_code");
                    dtResult.Columns.Remove("End Date");
                    dtResult.Columns.Remove("InTime");
                }
                catch (Exception e)
                { }

            }

            
            Session["dtResult"] = dtResult;

            gvResult.DataSource = dtResult;
            gvResult.DataBind();

            Session["TSRFromDate"] = rdEmpPrjStart.DbSelectedDate;
            Session["TSRToDate"] = rdEmpPrjEnd.DbSelectedDate;
            
        }
        
        protected void BeforeApprove()
        {
            try {

                dtMain.Clear();

                int compid = Utility.ToInteger(Session["Compid"]);

                string sqlEmployee = "";
                sqlEmployee = "SELECT e.time_card_no,e.emp_code FROM EmployeeAssignedToRoster er INNER JOIN employee e on e.emp_code=er.Emp_ID " +
                         "where e.Company_Id="+ compid +"";

                DataSet dsEmployee = new DataSet();
                dsEmployee = DataAccess.FetchRS(CommandType.Text, sqlEmployee, null);

                DataTable dtEmployee = new DataTable();
                dtEmployee = dsEmployee.Tables[0];

                for (int i = 0; i < dtEmployee.Rows.Count; i++)
                {
                    int emp_code = 0;
                    emp_code = int.Parse(dtEmployee.Rows[i]["emp_code"].ToString());

                    DateTime dt1 = new DateTime();
                    DateTime dt2 = new DateTime();
                    dt1 = rdEmpPrjStart.SelectedDate.Value;
                    dt2 = rdEmpPrjEnd.SelectedDate.Value;



                    SqlParameter[] parms1 = new SqlParameter[10];
                    parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                    parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                    parms1[2] = new SqlParameter("@compid", compid);
                    parms1[3] = new SqlParameter("@isEmpty", "No");
                    parms1[4] = new SqlParameter("@empid", emp_code);
                    parms1[5] = new SqlParameter("@subprojid", Convert.ToString(-1));
                    parms1[6] = new SqlParameter("@sessid", 1);
                    parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(99));
                    parms1[8] = new SqlParameter("@subprojid_name ", "0");
                    parms1[9] = new SqlParameter("@NightShift", "N");


                    dsTimesheet.Clear();
                    dsTimesheet = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Manual_New", parms1);


                    DataSet dsCompany = new DataSet();
                    string Fifo = "";
                    string strCompany = "";
                    strCompany = "Select FIFO from company where Company_Id=" + compid + "";
                    dsCompany = DataAccess.FetchRS(CommandType.Text, strCompany, null);
                    Fifo = dsCompany.Tables[0].Rows[0][0].ToString();

                    DataSet dsTest = new DataSet();

                    //if (Fifo == "1")
                    //{
                    dtMain.Merge(dsTimesheet.Tables[0]);
                    //    //dsTest = CheckFIFOfromCompanyLevel(ds1);
                    //}
                    //else
                    //{
                    //    dtMain.Merge(CheckFristInLastOut(dsTimesheet).Tables[0]);
                    //}
                    

                }
        //        Session["dtMain"] = dtMain;

                DataView view = new DataView(dtMain);
                view.RowFilter = "Sub_project_id is null"; // MyValue here is a column name

                // Delete these rows.
                foreach (DataRowView row in view)
                {
                    row.Delete();
                }

                ReportType(dtMain);
               
            
            }
            catch (Exception ex)
            { }
            
        }

        protected void ReportType(DataTable dt)
        {
            try {

                string sDate = rdEmpPrjStart.SelectedDate.Value.Year + "-" + rdEmpPrjStart.SelectedDate.Value.Month + "-" + rdEmpPrjStart.SelectedDate.Value.Day;
                string eDate = rdEmpPrjEnd.SelectedDate.Value.Year + "-" + rdEmpPrjEnd.SelectedDate.Value.Month + "-" + rdEmpPrjEnd.SelectedDate.Value.Day;

                DataTable dtEmployee = new DataTable();
                dtEmployee = dt.DefaultView.ToTable(true, "Emp_code");

                dtEmployee.Columns.Add("Employee");
                dtEmployee.Columns.Add("Time Card");
                string strEmployee = "";
                for (int e = 0; e < dtEmployee.Rows.Count; e++)
                {
                    strEmployee = "Select (emp_name + ' ' + emp_lname) [Employee],time_card_no from employee where emp_code=" + int.Parse(dtEmployee.Rows[e][0].ToString()) + "";
                    DataSet temp = new DataSet();
                    temp = DataAccess.FetchRS(CommandType.Text, strEmployee, null);
                    dtEmployee.Rows[e][1] = temp.Tables[0].Rows[0][0].ToString();
                    dtEmployee.Rows[e][2] = temp.Tables[0].Rows[0][1].ToString();
                }

                DataTable dtSubProject = new DataTable();
                dtSubProject = dt.DefaultView.ToTable(true, "Sub_project_id");

                dtSubProject.Columns.Add("Sub Project Name");
                string strSubProject = "";
                for (int p = 0; p < dtSubProject.Rows.Count; p++)
                {
                    try
                    {
                        strSubProject = "Select Sub_Project_Name from SubProject where Sub_Project_ID='" + dtSubProject.Rows[p][0].ToString() + "'";
                        DataSet temp = new DataSet();
                        temp = DataAccess.FetchRS(CommandType.Text, strSubProject, null);
                        dtSubProject.Rows[p][1] = temp.Tables[0].Rows[0][0].ToString();
                    }
                    catch (Exception ex) { }
                    
                }

                DataTable dttemp = new DataTable();
                dttemp.Columns.Add("Time Card");
                dttemp.Columns.Add("Sub Project Name");
                dttemp.Columns.Add("Employee");
                dttemp.Columns.Add("Date");                
                

                if (cmbExpReports.SelectedItem.Text == "No In Time")
                {
                    dttemp.Clear();
                    dttemp.Columns.Add("Out Time");

                    DataTable dtResult = new DataTable();
                    dtResult = GetFilteredTable(dt, "Inshorttime is null");
                    //dt.DefaultView.RowFilter = "Inshorttime is null";
                    //dtResult = dt.DefaultView.ToTable();
                    //dtResult = dt.Copy();

                    //for (int i = dtResult.Rows.Count - 1; i >= 0; i--)
                    //{
                    //    DataRow dr11 = dtResult.Rows[i];
                    //    if (dr11["Inshorttime"].ToString() != "")
                    //        dr11.Delete();
                    //}

                    DataRow dr;
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        DataRow[] drEmployee;
                        DataRow[] drSubProject;

                        drEmployee = dtEmployee.Select("emp_code=" + int.Parse(dtResult.Rows[i]["Emp_code"].ToString()) + "");

                        drSubProject = dtSubProject.Select("Sub_Project_ID='" + dtResult.Rows[i]["Sub_project_id"].ToString() + "'");
                        
                        dr = dttemp.NewRow();
                        dr[0] = drEmployee[0][2].ToString();
                        dr[1] = drSubProject[0][0].ToString();
                        dr[2] = drEmployee[0][1].ToString();
                        dr[3] = dtResult.Rows[i]["Tsdate"].ToString();
                        if (dtResult.Rows[i]["Outshorttime"].ToString() == "")
                        {
                            dr[4] = "00:00";
                        }
                        else
                        {
                            dr[4] = dtResult.Rows[i]["Outshorttime"].ToString();
                        }   
                        
                        dttemp.Rows.Add(dr);
                    }                   
                }



                if (cmbExpReports.SelectedItem.Text == "No Out Time")
                {
                    dttemp.Clear();
                    dttemp.Columns.Add("In Time");

                    DataTable dtResult = new DataTable();
                    dtResult = dt.Copy();
                   dtResult = GetFilteredTable(dt, "Outshorttime is null");
                    //DataRow[] drr = dtResult.Select("Outshorttime is not null");

                    //for (int i = dtResult.Rows.Count - 1; i >= 0; i--)
                    //{
                    //    DataRow dr11 = dtResult.Rows[i];
                    //    if (dr11["Outshorttime"].ToString() != "")
                    //        dr11.Delete();
                    //}

                    DataRow dr;
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        DataRow[] drEmployee;
                        DataRow[] drSubProject;

                        drEmployee = dtEmployee.Select("emp_code=" + int.Parse(dtResult.Rows[i]["Emp_code"].ToString()) + "");

                        drSubProject = dtSubProject.Select("Sub_Project_ID='" + dtResult.Rows[i]["Sub_project_id"].ToString() + "'");

                        dr = dttemp.NewRow();
                        dr[0] = drEmployee[0][2].ToString();
                        dr[1] = drSubProject[0][0].ToString();
                        dr[2] = drEmployee[0][1].ToString();
                        dr[3] = dtResult.Rows[i]["Tsdate"].ToString();
                        if (dtResult.Rows[i]["Inshorttime"].ToString() == "")
                        {
                            dr[4] = "00:00";
                        }
                        else
                        {
                            dr[4] = dtResult.Rows[i]["Inshorttime"].ToString();
                        }

                        dttemp.Rows.Add(dr);
                    }
                }


                if (cmbExpReports.SelectedItem.Text == "No In/Out Time")
                {
                    dttemp.Clear();
                    DataTable dtResult = new DataTable();
                    dtResult = GetFilteredTable(dt, "Inshorttime is null and Outshorttime is null");

                    DataRow dr;
                    for (int i = 0; i < dtResult.Rows.Count; i++)
                    {
                        DataRow[] drEmployee;
                        DataRow[] drSubProject;

                        drEmployee = dtEmployee.Select("emp_code=" + int.Parse(dtResult.Rows[i]["Emp_code"].ToString()) + "");

                        drSubProject = dtSubProject.Select("Sub_Project_ID='" + dtResult.Rows[i]["Sub_project_id"].ToString() + "'");

                        dr = dttemp.NewRow();
                        dr[0] = drEmployee[0][2].ToString();
                        dr[1] = drSubProject[0][0].ToString();
                        dr[2] = drEmployee[0][1].ToString();
                        dr[3] = dtResult.Rows[i]["Tsdate"].ToString();
                        

                        dttemp.Rows.Add(dr);
                    }
                }


                if (cmbExpReports.SelectedItem.Text == "On Leave")
                {
                    dttemp.Clear();
                    try
                    {
                        DataTable dtResult = new DataTable();
                        dtResult = GetFilteredTable(dt, "Inshorttime is not null or Outshorttime is not null");

                        string strSQL = "";
                        dttemp.Columns.Add("In Time");
                        dttemp.Columns.Add("Out Time");

                        strSQL = "Select emp_id, CONVERT(VARCHAR(10), CONVERT(DATETIME, leave_date, 103), 103) leave_date from emp_leaves e Inner join emp_leaves_detail el on e.trx_id=el.trx_id " +
                                 "where [start_date]>= '" + sDate + "' and [end_date]<= '" + eDate + "' and [status]='Approved' " +
                                 "and el.halfday_leave=0";

                        DataSet dsLeave = new DataSet();
                        dsLeave = DataAccess.FetchRS(CommandType.Text, strSQL, null);

                        DataRow dr;
                        DataTable temp = new DataTable();

                        for (int i = 0; i < dsLeave.Tables[0].Rows.Count; i++)
                        {
                            
                            temp=GetFilteredTable(dtResult, "emp_code=" + int.Parse(dsLeave.Tables[0].Rows[i]["emp_id"].ToString()) + " and Tsdate='" + dsLeave.Tables[0].Rows[i]["leave_date"].ToString() + "'");
                            //DataRow[] results = dtResult.Select("emp_code=" + int.Parse(dsLeave.Tables[0].Rows[i]["emp_id"].ToString()) + " and Tsdate='" + dsLeave.Tables[0].Rows[i]["leave_date"].ToString() + "'");
                            if (temp.Rows.Count > 0)
                            {
                                for (int a = 0; a < temp.Rows.Count; a++)
                                {
                                    if (temp.Rows[a]["Outshorttime"].ToString() != "" || temp.Rows[a]["Outshorttime"].ToString() != "")
                                    {
                                        DataRow[] drEmployee;
                                        DataRow[] drSubProject;

                                        drEmployee = dtEmployee.Select("emp_code=" + int.Parse(temp.Rows[a]["Emp_code"].ToString()) + "");

                                        drSubProject = dtSubProject.Select("Sub_Project_ID='" + temp.Rows[a]["Sub_project_id"].ToString() + "'");

                                        dr = dttemp.NewRow();
                                        dr[0] = drEmployee[0][2].ToString();
                                        dr[1] = drSubProject[0][0].ToString();
                                        dr[2] = drEmployee[0][1].ToString();
                                        dr[3] = temp.Rows[a]["Tsdate"].ToString();
                                        dr[4] = temp.Rows[a]["Inshorttime"].ToString();
                                        dr[5] = temp.Rows[a]["Outshorttime"].ToString();

                                        dttemp.Rows.Add(dr);
                                    }
                                    
                                }
                            }
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                
                if (cmbExpReports.SelectedItem.Text == "Late In")
                {
                    dttemp.Clear();
                    try
                    {
                        dttemp.Columns.Add("In Time");

                        DataTable dtResult = new DataTable();
                        dtResult = dt.Copy();
                        dtResult = GetFilteredTable(dt, "Inshorttime is not null");

                        //for (int i = dtResult.Rows.Count - 1; i >= 0; i--)
                        //{
                        //    DataRow dr11 = dtResult.Rows[i];
                        //    if (dr11["Inshorttime"].ToString() == "")
                        //        dr11.Delete();
                        //}

                        DataRow dr;

                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            int roster_id = 0;
                            string roster_date = "";
                            string InTime = "";
                            string SQL = "";
                            DataTable dt2 = new DataTable();

                            roster_id = int.Parse(dtResult.Rows[i]["Roster_id"].ToString());
                            roster_date = dtResult.Rows[i]["Tsdate"].ToString();
                            InTime = dtResult.Rows[i]["Inshorttime"].ToString();

                            SQL = "select InTime,LateInBy from RosterDetail where Roster_ID=" + roster_id + " and RosterType='Normal' and CONVERT(VARCHAR(10), CONVERT(DATETIME, Roster_Date, 103), 103)='" + roster_date + "'";
                            dt2 = DataAccess.FetchRS(CommandType.Text, SQL, null).Tables[0];

                            if (dt2.Rows.Count > 0)
                            {
                                string R_InTime = "";
                                string R_LateIn = "";

                                R_InTime = dt2.Rows[0][0].ToString();
                                try
                                {
                                    R_LateIn = dt2.Rows[0][1].ToString();
                                }
                                catch (Exception e) { }

                                TimeSpan end;
                                TimeSpan start = TimeSpan.Parse(InTime);
                                if (R_LateIn == "")
                                {
                                    end = TimeSpan.Parse(R_InTime);
                                    R_LateIn = R_InTime;
                                }
                                else
                                {
                                    end = TimeSpan.Parse(R_LateIn);
                                }


                                TimeSpan result = start - end;
                                string diff = result.Hours.ToString("00") + ":" + result.Minutes.ToString("00");
                                int intresult = TimeSpan.Compare(start, end);

                                if (intresult > 0)
                                {
                                    try
                                    {
                                        dttemp.Columns.Add("Late In By");
                                        dttemp.Columns.Add("Late Mins");
                                    }
                                    catch (Exception e)
                                    { }                                  
                                   
                                    DataRow[] drEmployee;
                                    DataRow[] drSubProject;

                                    drEmployee = dtEmployee.Select("emp_code=" + int.Parse(dtResult.Rows[i]["Emp_code"].ToString()) + "");

                                    drSubProject = dtSubProject.Select("Sub_Project_ID='" + dtResult.Rows[i]["Sub_project_id"].ToString() + "'");

                                    dr = dttemp.NewRow();
                                    dr[0] = drEmployee[0][2].ToString();
                                    dr[1] = drSubProject[0][0].ToString();
                                    dr[2] = drEmployee[0][1].ToString();
                                    dr[3] = dtResult.Rows[i]["Tsdate"].ToString();
                                    dr[4] = dtResult.Rows[i]["Inshorttime"].ToString();
                                    dr[5] = R_LateIn;
                                    dr[6] = diff.ToString();

                                    dttemp.Rows.Add(dr);                               
 
                                }

                            }
                        }
                        
                    }
                    catch (Exception e)
                    { }

                }


                if (cmbExpReports.SelectedItem.Text == "Early Out")
                {
                    dttemp.Clear();
                    try
                    {
                        dttemp.Columns.Add("Out Time");

                        DataTable dtResult = new DataTable();
                        dtResult = dt.Copy();
                        //dtResult = GetFilteredTable(dt, "Outshorttime is not null");

                        for (int i = dtResult.Rows.Count - 1; i >= 0; i--)
                        {
                            DataRow dr11 = dtResult.Rows[i];
                            if (dr11["Outshorttime"].ToString() == "")
                                dr11.Delete();
                        }

                        DataRow dr;
                        for (int i = 0; i < dtResult.Rows.Count; i++)
                        {
                            int roster_id = 0;
                            string roster_date = "";
                            string OutTime = "";
                            string SQL = "";
                            DataTable dt2 = new DataTable();

                            roster_id = int.Parse(dtResult.Rows[i]["Roster_ID"].ToString());
                            roster_date = dtResult.Rows[i]["Tsdate"].ToString();
                            OutTime = dtResult.Rows[i]["Outshorttime"].ToString();

                            SQL = "select OutTime,EarlyOutBy from RosterDetail where Roster_ID=" + roster_id + " and RosterType='Normal' and CONVERT(VARCHAR(10), CONVERT(DATETIME, Roster_Date, 103), 103)='" + roster_date + "'";
                            dt2 = DataAccess.FetchRS(CommandType.Text, SQL, null).Tables[0];

                            if (dt2.Rows.Count > 0)
                            {
                                string R_OutTime = "";
                                string R_EarlyOut = "";

                                R_OutTime = dt2.Rows[0][0].ToString();
                                try
                                {
                                    R_EarlyOut = dt2.Rows[0][1].ToString();
                                }
                                catch (Exception e) { }

                                TimeSpan end;
                                TimeSpan start = TimeSpan.Parse(OutTime);
                                if (R_EarlyOut == "")
                                {
                                    end = TimeSpan.Parse(R_OutTime);
                                    R_EarlyOut = R_OutTime;
                                }
                                else
                                {
                                    end = TimeSpan.Parse(R_EarlyOut);
                                }


                                TimeSpan result = end - start;
                                string diff = result.Hours.ToString("00") + ":" + result.Minutes.ToString("00");
                                int intresult = TimeSpan.Compare(start, end);

                                if (intresult < 0)
                                {
                                    try
                                    {
                                        dttemp.Columns.Add("Early Out By");
                                        dttemp.Columns.Add("Early Mins");
                                    }
                                    catch (Exception e)
                                    { }

                                    DataRow[] drEmployee;
                                    DataRow[] drSubProject;

                                    drEmployee = dtEmployee.Select("emp_code=" + int.Parse(dtResult.Rows[i]["Emp_code"].ToString()) + "");

                                    drSubProject = dtSubProject.Select("Sub_Project_ID='" + dtResult.Rows[i]["Sub_project_id"].ToString() + "'");

                                    dr = dttemp.NewRow();
                                    dr[0] = drEmployee[0][2].ToString();
                                    dr[1] = drSubProject[0][0].ToString();
                                    dr[2] = drEmployee[0][1].ToString();
                                    dr[3] = dtResult.Rows[i]["Tsdate"].ToString();
                                    dr[4] = dtResult.Rows[i]["Outshorttime"].ToString();
                                    dr[5] = R_EarlyOut;
                                    dr[6] = diff.ToString();

                                    dttemp.Rows.Add(dr);
                                   
                                }

                            }
                        }

                     
                    }
                    catch (Exception e)
                    { }

                }


                DataView dv = dttemp.DefaultView;
                dv.Sort = "Date, Employee ASC";
                DataTable sortedDT = dv.ToTable();

                Session["dtResult"] = sortedDT;

                gvResult.DataSource = sortedDT;
                gvResult.DataBind();
            }
            catch (Exception ex) { }
        }

        public static DataTable GetFilteredTable(DataTable sourceTable, string selectFilter)
        {
            DataTable filteredTable = sourceTable.Clone();
            DataRow[] rows = sourceTable.Select(selectFilter);
            foreach (DataRow row in rows)
            {
                filteredTable.ImportRow(row);
            }
            return filteredTable;
        }

        protected DataSet CheckFristInLastOut(DataSet ds)
        {
            string Intime, Outtime;
            string IntimePK = "";
            string OuttimePK = "";

            DataSet dsTemp;
            dsTemp = ds.Copy();

            DataTable dtDate = new DataTable();
            dtDate = dsTemp.Tables[0].DefaultView.ToTable(true, "Tsdate");


            DataTable dtProject = new DataTable();
            dtProject = dsTemp.Tables[0].DefaultView.ToTable(true, "Sub_project_id");


            DataTable table = dsTemp.Tables[0];
            DataTable Mytable = table.Copy();
            Mytable.Clear();

            DataTable dtFinal = table.Copy();
            dtFinal.Clear();

            for (int d = 0; d < dtDate.Rows.Count; d++)
            {
                if (table.Rows.Count > 0)
                {
                    table.DefaultView.RowFilter = "Tsdate = '" + dtDate.Rows[d][0].ToString() + "' ";

                    DataTable dtTemp = new DataTable();
                    dtTemp = table.DefaultView.ToTable();

                    if (dtTemp.Rows.Count > 1)
                    {
                        for (int p = 0; p < dtProject.Rows.Count; p++)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp.DefaultView.RowFilter = "Sub_project_id = '" + dtProject.Rows[p][0].ToString() + "' ";
                            dtTemp1 = dtTemp.DefaultView.ToTable();

                            if (dtTemp1.Rows.Count > 1)
                            {
                                Intime = "";
                                Outtime = "";
                                dtFinal.Rows.Clear();
                                for (int In = 0; In < dtTemp1.Rows.Count; In++)
                                {
                                    IntimePK = dtTemp1.Rows[In]["PK"].ToString();
                                    IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));

                                    if (dtTemp1.Rows[In]["Inshorttime"].ToString() != "")
                                    {
                                        Intime = dtTemp1.Rows[In]["Inshorttime"].ToString();
                                        IntimePK = dtTemp1.Rows[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }

                                }
                                int Out = dtTemp1.Rows.Count - 1;
                                for (Out = dtTemp1.Rows.Count - 1; Out >= 0; Out--)
                                {
                                    int index = 0;

                                    OuttimePK = dtTemp1.Rows[Out]["PK"].ToString();
                                    index = OuttimePK.IndexOf(":");
                                    OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));

                                    if (dtTemp1.Rows[Out]["Outshorttime"].ToString() != "")
                                    {
                                        Outtime = dtTemp1.Rows[Out]["Outshorttime"].ToString();
                                        OuttimePK = dtTemp1.Rows[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));
                                        break;
                                    }

                                }
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["Inshorttime"] = Intime;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["Outshorttime"] = Outtime;
                                dtTemp1.Rows[dtTemp1.Rows.Count - 1]["PK"] = IntimePK + ":" + OuttimePK;

                                int count = dtTemp1.Rows.Count;

                                DataRow dr;
                                dr = dtFinal.NewRow();
                                for (int c = 0; c < dtTemp1.Columns.Count; c++)
                                {
                                    dr[c] = dtTemp1.Rows[count - 1][c].ToString();
                                }

                                dtFinal.Rows.Add(dr);
                                Mytable.Merge(dtFinal);

                            }
                            else
                            {
                                Mytable.Merge(dtTemp1);
                            }

                        }
                    }
                    else
                    {
                        Mytable.Merge(dtTemp);
                    }

                }


            }
            dsTemp.Tables.Clear();
            DataView dv = Mytable.DefaultView;
            dv.Sort = "TsDate, SrNo ASC";
            DataTable sortedDT = dv.ToTable();
            dsTemp.Tables.Add(sortedDT);
            return dsTemp;
        }


        protected DataSet CheckFIFOfromCompanyLevel(DataSet ds)
        {
            string Intime, Outtime;
            string IntimePK = "";
            string OuttimePK = "";

            DataSet dsTemp;
            dsTemp = ds.Copy();

            DataTable dtDate = new DataTable();
            dtDate = dsTemp.Tables[0].DefaultView.ToTable(true, "Tsdate");


            DataTable dtProject = new DataTable();
            dtProject = dsTemp.Tables[0].DefaultView.ToTable(true, "Sub_project_id");


            DataTable table = dsTemp.Tables[0];
            DataTable Mytable = table.Copy();
            Mytable.Clear();

            DataTable dtFinal = table.Copy();
            dtFinal.Clear();

            for (int d = 0; d < dtDate.Rows.Count; d++)
            {
                if (table.Rows.Count > 0)
                {
                    table.DefaultView.RowFilter = "Tsdate = '" + dtDate.Rows[d][0].ToString() + "' ";

                    DataTable dtTemp = new DataTable();
                    dtTemp = table.DefaultView.ToTable();

                    if (dtTemp.Rows.Count > 1)
                    {
                        for (int p = 0; p < dtProject.Rows.Count; p++)
                        {
                            DataTable dtTemp1 = new DataTable();
                            dtTemp.DefaultView.RowFilter = "Sub_project_id = '" + dtProject.Rows[p][0].ToString() + "' ";
                            dtTemp1 = dtTemp.DefaultView.ToTable();

                            DataView dvFirst = dtTemp.DefaultView;
                            dvFirst.Sort = "SrNo ASC";


                            Intime = "";
                            Outtime = "";

                            if (dvFirst.Count > 0)
                            {
                                dtFinal.Rows.Clear();

                                for (int In = 0; In < dvFirst.Count; In++)
                                {
                                    IntimePK = dvFirst[In]["PK"].ToString();
                                    IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));

                                    if (dvFirst[In]["Inshorttime"].ToString() != "")
                                    {
                                        Intime = dvFirst[In]["Inshorttime"].ToString();
                                        IntimePK = dvFirst[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }
                                    if (dvFirst[In]["Outshorttime"].ToString() != "")
                                    {
                                        Intime = dvFirst[In]["Outshorttime"].ToString();
                                        IntimePK = dvFirst[In]["PK"].ToString();
                                        IntimePK = IntimePK.Substring(0, IntimePK.IndexOf(":"));
                                        break;
                                    }

                                }

                                int Out = dvFirst.Count - 1;
                                for (Out = dvFirst.Count - 1; Out >= 0; Out--)
                                {
                                    int index = 0;

                                    OuttimePK = dvFirst[Out]["PK"].ToString();
                                    index = OuttimePK.IndexOf(":");
                                    OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));

                                    if (dvFirst[Out]["Outshorttime"].ToString() != "")
                                    {
                                        Outtime = dvFirst[Out]["Outshorttime"].ToString();
                                        OuttimePK = dvFirst[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));
                                        break;
                                    }
                                    if (dvFirst[Out]["Inshorttime"].ToString() != "")
                                    {
                                        Outtime = dvFirst[Out]["Inshorttime"].ToString();
                                        OuttimePK = dvFirst[Out]["PK"].ToString();

                                        index = OuttimePK.IndexOf(":");
                                        OuttimePK = OuttimePK.Substring(index + 1, OuttimePK.Length - (index + 1));
                                        break;
                                    }

                                }

                                dvFirst[dtTemp1.Rows.Count - 1]["Inshorttime"] = Intime;
                                dvFirst[dtTemp1.Rows.Count - 1]["Outshorttime"] = Outtime;
                                dvFirst[dtTemp1.Rows.Count - 1]["PK"] = IntimePK + ":" + OuttimePK;

                                int count = dvFirst.Count;

                                DataRow dr;
                                dr = dtFinal.NewRow();
                                for (int c = 0; c < dtTemp.Columns.Count; c++)
                                {
                                    dr[c] = dvFirst[count - 1][c].ToString();
                                }

                                dtFinal.Rows.Add(dr);
                                Mytable.Merge(dtFinal);

                            }
                            else
                            {
                                Mytable.Merge(dtTemp1);
                            }

                        }
                    }
                    else
                    {
                        Mytable.Merge(dtTemp);
                    }

                }


            }
            dsTemp.Tables.Clear();
            DataView dv = Mytable.DefaultView;
            dv.Sort = "TsDate, SrNo ASC";
            DataTable sortedDT = dv.ToTable();
            dsTemp.Tables.Add(sortedDT);
            return dsTemp;

        }
        
        protected void btnExportWord_click(object sender, EventArgs e)
        {
            gvResult.ExportSettings.IgnorePaging = true;
            gvResult.ExportSettings.ExportOnlyData = true;
            gvResult.ExportSettings.OpenInNewWindow = true;
            gvResult.MasterTableView.ExportToWord();
        }

        protected void gvResult_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {

            try {
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["dtResult"];

                    gvResult.DataSource = dt;
                    gvResult.DataBind();
                
            }
            catch (Exception ex) { }
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            gvResult.ExportSettings.ExportOnlyData = true;
            gvResult.ExportSettings.IgnorePaging = true;
            gvResult.ExportSettings.OpenInNewWindow = true;
            gvResult.MasterTableView.ExportToExcel();
        }

        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            gvResult.ExportSettings.ExportOnlyData = true;
            gvResult.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((gvResult.Items[0].Cells.Count * 30)) + "mm");
            gvResult.ExportSettings.OpenInNewWindow = true;
            gvResult.MasterTableView.ExportToPdf();
        }

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }

 
    }
}
