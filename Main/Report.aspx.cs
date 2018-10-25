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
using System.Globalization;
using Telerik.Web.UI;
using System.Data.SqlClient;

namespace SMEPayroll.Main
{
    public partial class Report : System.Web.UI.Page
    {

        //EMPLeave=1
        //Pass Expiring=2
        //CSOC Expiring=3
        //Employeebirthday=4
        //Certificate Expiring =5
        //PendingLeaveRequest=6
        //PassportExpiring=7
        //InsuranceExpiring=8
        //ProbationPeriodExpiring=9
        //YOS=10
        //Bacup=11

        public string leaveType = "";
        public string deptid = "";
        string SelectCommand = "";
        string querystring = "";
        int company_id;
        RadListBox radList;
        RadListBox radlistDept;
        RadListBox radioCulture;

        protected void Page_Load(object sender, EventArgs e)
        {
            SchedulerDataSource.ConnectionString =  Constants.CONNECTION_STRING;
            SqlDataSource2.ConnectionString      =  Constants.CONNECTION_STRING;
            SqlDataSource1.ConnectionString      =  Constants.CONNECTION_STRING;

            radList                         = (RadListBox)RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("radList");
            radlistDept                     = (RadListBox)RadPanelBar2.FindItemByValue("ctrlPanel6").FindControl("radlistDept");
            radioCulture                    = (RadListBox)RadPanelBar3.FindItemByValue("ctrlPanel8").FindControl("radioCulture");

            querystring                     = Request.QueryString[0].ToString();
            company_id                      = Convert.ToInt32(Request.QueryString[1].ToString());

            //Radlist
            radList.ItemCreated             += new Telerik.Web.UI.RadListBoxItemEventHandler(radList_ItemCreated);
            radList.ItemCheck               += new Telerik.Web.UI.RadListBoxItemEventHandler(radList_ItemCheck);
            radlistDept.ItemCreated         += new Telerik.Web.UI.RadListBoxItemEventHandler(radlistDept_ItemCreated);
            radlistDept.ItemCheck           += new Telerik.Web.UI.RadListBoxItemEventHandler(radlistDept_ItemCheck);
            radioCulture.ItemCreated        += new Telerik.Web.UI.RadListBoxItemEventHandler(radioCulture_ItemCreated);
            radioCulture.ItemCheck          += new Telerik.Web.UI.RadListBoxItemEventHandler(radioCulture_ItemCheck);

            radioCulture.SelectionMode      = Telerik.Web.UI.ListBoxSelectionMode.Multiple;
            //RadCal
            RadCalendar1.SelectionChanged += new Telerik.Web.UI.Calendar.SelectedDatesEventHandler(RadCalendar1_SelectionChanged);
            RadToolTipManager2.AjaxUpdate += new Telerik.Web.UI.ToolTipUpdateEventHandler(OnAjaxUpdate);

            

           
            if (!IsPostBack)
            {
               
                radList.CheckBoxes = true;
                RadCalendar1.SelectedDate = DateTime.Now.Date;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                if (querystring == "1")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Leave Types";
                    RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select id,Type from leave_types Where companyid In(-1," + company_id +")";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "id";
                    radList.DataValueField = "id";
                    radList.DataTextField = "Type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    
                    radlistDept.CheckBoxes = true;
                    SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    radlistDept.DataSourceID = "SqlDataSource2";
                    radlistDept.DataKeyField = "id";
                    radlistDept.DataValueField = "id";
                    radlistDept.DataTextField = "deptname";
                    SqlDataSource2.SelectCommand = SelectCommand;
                
                    leaveType = "(" + leaveType + ")";
                    //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,12,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=60  And status='Approved'";  // AND a.leave_type in " + leaveType;               
                    SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,12,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id+ "' )  And status='Approved'";  // AND a.leave_type in " + leaveType;               
                    RadCalendar1.SelectedDate = DateTime.Today;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;


                    string sqlSDate = "Select Top 1 convert(datetime,Start,103) from (";
                    sqlSDate = sqlSDate + "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,12,Convert(DATETIME,End_Date,103))[End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' ) And status='Approved' )A Order by Start Asc";

                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                      //  strDate = Convert.ToDateTime(startdate);
                    }

                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                    
                    
                }
                //Pass Exp Date
                if (querystring == "2")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;
                   
                    //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    //radlistDept.DataSourceID = "SqlDataSource2";
                    //radlistDept.DataKeyField = "id";
                    //radlistDept.DataValueField = "id";
                    //radlistDept.DataTextField = "deptname";
                    //SqlDataSource2.SelectCommand = SelectCommand;

                    string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                    sqlSDate = sqlSDate + " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) ) A Order by ExpDate Asc";

                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        //strDate = Convert.ToDateTime(startdate);
                    }

                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) ";
                      // Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'FIN/WP No', Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "' ) Order by [days] Asc";
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;

                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                }
                
                //------------added by murugan
                if (querystring == "10")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;


                    string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                    sqlSDate = sqlSDate + " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ) A Order by ExpDate Asc";

                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        //strDate = Convert.ToDateTime(startdate);
                    }

                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ";
                    
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;

                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                }

                //-----------
                if (querystring == "7")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    //radlistDept.DataSourceID = "SqlDataSource2";
                    //radlistDept.DataKeyField = "id";
                    //radlistDept.DataValueField = "id";
                    //radlistDept.DataTextField = "deptname";
                    //SqlDataSource2.SelectCommand = SelectCommand;

                    leaveType = "(" + leaveType + ")";

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand =SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand =SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand =SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  ";

                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;

                            string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                            sqlSDate = sqlSDate + SelectCommand + " )F order by ExpDate Asc";
                            //sqlSDate = sqlSDate + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) )F order by DOB Asc";

                            SqlDataReader sdr;
                            sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                            string startdate = "";
                            DateTime strDate = Convert.ToDateTime(DateTime.Now);
                            while (sdr.Read())
                            {
                                startdate = sdr[0].ToString();
                            }
                            if (startdate != "")
                            {
                                strDate = Convert.ToDateTime(startdate);
                            }                    
                            if (startdate != "")
                            {
                                RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                                RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                                RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                            }
                }
                //Insurance expiring
                if (querystring == "8")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    //radlistDept.DataSourceID = "SqlDataSource2";
                    //radlistDept.DataKeyField = "id";
                    //radlistDept.DataValueField = "id";
                    //radlistDept.DataTextField = "deptname";
                    //SqlDataSource2.SelectCommand = SelectCommand;

                    leaveType = "(" + leaveType + ")";

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID AND CC.ColID=6 ";
                   // SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " where  termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  ";
                                   // Select Top 5 EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Insurance No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =6 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + compid + "') Order by  ExpiryDate Asc
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    //////--MURUGAN

                    string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                    sqlSDate = sqlSDate + SelectCommand + ")A Order by convert(datetime,ExpDate,103) Asc";
                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        strDate = Convert.ToDateTime(startdate);
                    }

                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                }
                //Probation PeriodExpiring
                if (querystring == "9")
                {
                    //RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    //SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    //radList.DataSourceID = "SqlDataSource1";
                    //radList.DataKeyField = "emp_type";
                    //radList.DataValueField = "emp_type";
                    //radList.DataTextField = "emp_type";
                    //SqlDataSource1.SelectCommand = SelectCommand;
                    
                    
                    radlistDept.CheckBoxes = true;
                    SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    radlistDept.DataSourceID = "SqlDataSource2";
                    radlistDept.DataKeyField = "id";
                    radlistDept.DataValueField = "id";
                    radlistDept.DataTextField = "deptname";
                    SqlDataSource2.SelectCommand = SelectCommand;

                    leaveType = "(" + leaveType + ")";

                    SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
                    SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
                    SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + company_id;
                    SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + company_id + "' ) AND  ";
                    SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
                    SelectCommand = SelectCommand + " AND confirmation_date is null";

                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                }

                if (querystring == "3")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    //radlistDept.DataSourceID = "SqlDataSource2";
                    //radlistDept.DataKeyField = "id";
                    //radlistDept.DataValueField = "id";
                    //radlistDept.DataTextField = "deptname";
                    //SqlDataSource2.SelectCommand = SelectCommand;

                    leaveType = "(" + leaveType + ")";
                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 Order by " + company_id;

                    SelectCommand = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID AND CC.ColID=5 where  termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) ";
                   // = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) ";

                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Type";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;

                    //Set Initial Date
                    string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                    //sqlSDate = sqlSDate + " Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and ABS(datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate))<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) )A Order by convert(datetime,ExpDate,103) Asc";
                    sqlSDate = sqlSDate + " Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID AND CC.ColID=5 where termination_date is null and EY.company_id= " + company_id + "  and ABS(datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate))<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) )A Order by convert(datetime,ExpDate,103) Asc";
                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        strDate = Convert.ToDateTime(startdate);
                    }                   
                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                }
                //Other Certificate
                 if (querystring == "5")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                    //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "emp_type";
                    radList.DataValueField = "emp_type";
                    radList.DataTextField = "emp_type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    //radlistDept.DataSourceID = "SqlDataSource2";
                    //radlistDept.DataKeyField = "id";
                    //radlistDept.DataValueField = "id";
                    //radlistDept.DataTextField = "deptname";
                    //SqlDataSource2.SelectCommand = SelectCommand;

                    leaveType = "(" + leaveType + ")";
                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 Order by " + company_id;                    
                    //SelectCommand= "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 Order by " + company_id;

                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand =  SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON EC.CertificateCategoryID=CC.ID AND CC.ColID=9 where ";
                    SelectCommand = SelectCommand + " termination_date is null and EY.company_id= " + company_id;
                    //SelectCommand =  SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and abs(datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate))<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";


                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;


                    //Set Initial Date
                    string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                    sqlSDate = sqlSDate + SelectCommand + ")A Order by convert(datetime,ExpDate,103) Asc";

                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        strDate = Convert.ToDateTime(startdate);
                    }
                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }



                }


                //BD
                if (querystring == "4")
                {
                    RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    radlistDept.DataSourceID = "SqlDataSource2";
                    radlistDept.DataKeyField = "id";
                    radlistDept.DataValueField = "id";
                    radlistDept.DataTextField = "deptname";
                    SqlDataSource2.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    SelectCommand =" Select * from ";
                    SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";

                    //RadCalendar1.SelectedDate = DateTime.Today;
                    //RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                    //Get value for Start date

                    string sqlSDate = "Select Top 1 convert(datetime,DOB,103) from (";
                    sqlSDate = sqlSDate + " Select * from ";
                    sqlSDate = sqlSDate + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) )F order by DOB Asc";

                    SqlDataReader sdr ;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate= Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                        strDate = Convert.ToDateTime(startdate);
                    }
                    //Set value for 
                    RadScheduler1.DataEndField = "DOB1";
                    RadScheduler1.DataStartField = "DOB";
                    RadScheduler1.DataDescriptionField = "Name";
                    RadScheduler1.DataSubjectField = "Name";
                    RadScheduler1.DataKeyField = "id";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate= Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }
                }

                //Pending Leave Request
                if (querystring == "6")
                {
                    RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Leave Types";
                    RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                    RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                    SelectCommand = "Select id,Type from leave_types Where companyid In(-1," + company_id + ")";
                    radList.DataSourceID = "SqlDataSource1";
                    radList.DataKeyField = "id";
                    radList.DataValueField = "id";
                    radList.DataTextField = "Type";
                    SqlDataSource1.SelectCommand = SelectCommand;
                    radlistDept.CheckBoxes = true;

                    SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                    radlistDept.DataSourceID = "SqlDataSource2";
                    radlistDept.DataKeyField = "id";
                    radlistDept.DataValueField = "id";
                    radlistDept.DataTextField = "deptname";
                    SqlDataSource2.SelectCommand = SelectCommand;



                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";

                    leaveType = "(" + leaveType + ")";
                    //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected'"; // AND a.leave_type in " + leaveType;               
                    RadCalendar1.SelectedDate = DateTime.Today;
                    RadScheduler1.DataEndField = "Todate";
                    RadScheduler1.DataStartField = "FromDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;



                    SelectCommand = "Select Top 1 convert(datetime,FromDate,103) from ( select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + "))F Order By FromDate Asc";


                    string sqlSDate = SelectCommand;
                    //"Select Top 1 convert(datetime,DOB,103) from (";
                    //sqlSDate = sqlSDate + " Select * from ";
                    //sqlSDate = sqlSDate + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) )F order by DOB Asc";

                    SqlDataReader sdr;
                    sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                    string startdate = "";
                    DateTime strDate = Convert.ToDateTime(DateTime.Now);
                    while (sdr.Read())
                    {
                        startdate = sdr[0].ToString();
                    }
                    if (startdate != "")
                    {
                       // strDate = Convert.ToDateTime(startdate);
                    }

                    if (startdate != "")
                    {
                        RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                        RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    }

                }
            }
        }


        void radioCulture_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            string culture = "";
            foreach (Telerik.Web.UI.RadListBoxItem radi in radioCulture.Items)
            {
                if (radi.Checked == true)
                {
                    culture = radi.Value;
                }
            }           
            RadScheduler1.Culture = new CultureInfo(culture);
            RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            System.Threading.Thread.Sleep(1000);
        }

//        void radlistDept_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
//        {
//            foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
//            {
//                if (radi.Checked == true)
//                {
//                    if (deptid == "")
//                    {
//                        deptid = radi.Value;
//                    }
//                    else
//                    {
//                        deptid = deptid + "," + radi.Value;
//                    }
//                }
//            }

//            foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
//            {
//                if (radi.Checked == true)
//                {
//                    if (leaveType == "")
//                    {
//                        if (querystring == "2")
//                        {
//                            leaveType = "'" + radi.Value + "'";
//                        }
//                        else
//                        {

//                        }
//                    }
//                    else
//                    {
//                        if (querystring == "2")
//                        {
//                            leaveType = leaveType + ",'" + radi.Value + "'";
//                        }
//                        else
//                        {
//                            leaveType = leaveType + "," + radi.Value;
//                        }
//                    }
//                }
//            }

//            if (querystring == "-1")
//            {
//                    if (deptid != "")
//                    {
//                        deptid = "(" + deptid + ")";
//                        SelectCommand
//                        = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' )  and emp_type in " + deptid;
//                    }
//                    else
//                    {
//                        SelectCommand
//                        = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) ";
//                    }
                 
//                    RadScheduler1.DataEndField = "ExpDate1";
//                    RadScheduler1.DataStartField = "ExpDate";
//                    RadScheduler1.DataDescriptionField = "Subject";
//                    RadScheduler1.DataSubjectField = "Subject";
//                    RadScheduler1.DataKeyField = "ID";
//                    RadScheduler1.DataSourceID = "SchedulerDataSource";                    
//                    id1.Text = DateTime.Today.ToShortDateString();
//                    SchedulerDataSource.SelectCommand = SelectCommand;
//                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
//            }
//            else if (querystring == "6")
//            {

//                if (deptid != "" && leaveType != "")
//                {
//                    deptid = "(" + deptid + ")";
//                    leaveType = "(" + leaveType + ")";
//                    //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
//                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
//                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
//                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) and ";
//                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid ;

//                }

//                if (deptid != "" && leaveType == "")
//                {
//                    deptid = "(" + deptid + ")";
//                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
//                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
//                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) and ";
//                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid ;

//                }

//                if (deptid == "" && leaveType != "")
//                {
//                    leaveType = "(" + leaveType + ")";

//                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
//                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
//                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) and ";
//                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType ;

//                }

//                if (deptid == "" && leaveType == "")
//                {
//                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
//                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
//                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) and ";
//                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
//                }

//                RadScheduler1.DataEndField = "Todate";
//                RadScheduler1.DataStartField = "FromDate";
//                RadScheduler1.DataDescriptionField = "Subject";
//                RadScheduler1.DataSubjectField = "Subject";
//                RadScheduler1.DataKeyField = "ID";
//                RadScheduler1.DataSourceID = "SchedulerDataSource";
//                id1.Text = DateTime.Today.ToShortDateString();
//                SchedulerDataSource.SelectCommand = SelectCommand;
//                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

//            }
//            else if(querystring=="1")
//            {
//                if (deptid != "" && leaveType != "")
//                    {
//                        if (querystring == "1")
//                        {
//                            deptid = "(" + deptid + ")";
//                            leaveType = "(" + leaveType + ")";
//                            SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) And status='Approved'";

//                        }

//                    }

//                    if (deptid != "" && leaveType == "")
//                    {
//                        if (querystring == "1")
//                        {
//                            deptid = "(" + deptid + ")";
//                            SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) And status='Approved'";
////murugan
//                          //  SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,12,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )  And status='Approved'";  // AND a.leave_type in " + leaveType;               
//                        }
//                    }

//                    if (deptid == "" && leaveType != "")
//                    {
//                        if (querystring == "1")
//                        {
//                            leaveType = "(" + leaveType + ")";
//                            SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND a.leave_type " + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) And status='Approved'"; 
//                        }
//                    }

//                    if (deptid == "" && leaveType == "")
//                    {
//                        if (querystring == "1")
//                        {
//                            SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in (-1)" + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + company_id + "' ) And status='Approved'"; 
//                        }
//                    }
//                    //id1.Text = SelectCommand;
//                    if (querystring == "1")
//                    {
//                        SchedulerDataSource.SelectCommand = SelectCommand;
//                        RadScheduler1.DataEndField = "End";
//                        RadScheduler1.DataStartField = "Start";
//                        RadScheduler1.DataDescriptionField = "Description";
//                        RadScheduler1.DataSubjectField = "Subject";
//                        RadScheduler1.DataKeyField = "ID";
//                        RadScheduler1.DataSourceID = "SchedulerDataSource";
//                        RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
//                    }
//                }
//                else if (querystring == "9")
//                {

//                    if (deptid != "")
//                    {
//                        deptid = "(" + deptid + ")";
//                        //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid;
//                        SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
//                        SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
//                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + company_id;
//                        SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + company_id + "' ) AND  ";
//                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
//                        SelectCommand = SelectCommand + " AND confirmation_date is null and dept_id in " + deptid;

//                    }
//                    else
//                    {
//                        SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
//                        SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
//                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + company_id;
//                        SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + company_id + "' ) AND  ";
//                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
//                        SelectCommand = SelectCommand + " AND confirmation_date is null";

//                    }
//                    RadScheduler1.DataEndField = "ExpDate1";
//                    RadScheduler1.DataStartField = "ExpDate";
//                    RadScheduler1.DataDescriptionField = "Subject";
//                    RadScheduler1.DataSubjectField = "Subject";
//                    RadScheduler1.DataKeyField = "ID";
//                    RadScheduler1.DataSourceID = "SchedulerDataSource";
//                    id1.Text = DateTime.Today.ToShortDateString();
//                    SchedulerDataSource.SelectCommand = SelectCommand;
//                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

//                }
//                else if (querystring == "4") //BD
//                {
//                    if (deptid != "")
//                    {
//                        deptid = "(" + deptid + ")";
//                        SelectCommand = " Select * from ";
//                        SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department where dept_id in " + deptid + " )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";
//                    }
//                    else
//                    {
//                        SelectCommand = " Select * from ";
//                        SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department where dept_id in " + deptid + " )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";
//                    }

//                    //RadCalendar1.SelectedDate = DateTime.Today;
//                    //RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

//                    RadScheduler1.DataEndField = "DOB1";
//                    RadScheduler1.DataStartField = "DOB";
//                    RadScheduler1.DataDescriptionField = "Name";
//                    RadScheduler1.DataSubjectField = "Name";
//                    RadScheduler1.DataKeyField = "id";
//                    RadScheduler1.DataSourceID = "SchedulerDataSource";
//                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
//                    id1.Text = DateTime.Today.ToShortDateString();
//                    SchedulerDataSource.SelectCommand = SelectCommand;
//                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
//                }

//            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
//            {
//                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
//            }

//            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
//            {
//                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
//            }

//            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
//            {
//                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
//            }

//            System.Threading.Thread.Sleep(1000);
//        }
        void radlistDept_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
            {
                if (radi.Checked == true)
                {
                    if (deptid == "")
                    {
                        deptid = radi.Value;
                    }
                    else
                    {
                        deptid = deptid + "," + radi.Value;
                    }
                }
            }

            foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
            {
                if (radi.Checked == true)
                {
                    if (leaveType == "")
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = "'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = radi.Value;
                        }
                    }
                    else
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = leaveType + ",'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = leaveType + "," + radi.Value;
                        }
                    }
                }
            }


            if (querystring == "2")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "3")
            {

                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    SelectCommand
                           = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;

                }
                else
                {
                    SelectCommand
                       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "5")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 and emp_type in " + leaveType + " Order by " + company_id;
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "7")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "8")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) ";

                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "1")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                        deptid = "(" + deptid + ")";
                        leaveType = "(" + leaveType + ")";
                        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType + " and b.company_id=" + company_id; ;
                //    }
                //}
                

                //if (deptid != "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        deptid = "(" + deptid + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                //        leaveType = "(" + leaveType + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND a.leave_type in" + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in (-1)" + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                if (querystring == "1")
                {
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
            }
            else if (querystring == "6")
            {

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                    deptid = "(" + deptid + ")";
                    leaveType = "(" + leaveType + ")";
                    //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
                    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid;

                //}

                //if (deptid != "" && leaveType == "")
                //{
                //    deptid = "(" + deptid + ")";
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid;

                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    leaveType = "(" + leaveType + ")";

                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType;

                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
                //}

                RadScheduler1.DataEndField = "Todate";
                RadScheduler1.DataStartField = "FromDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            //BD
            if (querystring == "4")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                else
                {
                    deptid = "("+ deptid +")";
                }

                //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                //RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                //radlistDept.DataSourceID = "SqlDataSource2";
                //radlistDept.DataKeyField = "id";
                //radlistDept.DataValueField = "id";
                //radlistDept.DataTextField = "deptname";
                //SqlDataSource2.SelectCommand = SelectCommand;
                //radlistDept.CheckBoxes = true;

                SelectCommand = " Select * from ";
                SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In " + deptid + " AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";

                //RadCalendar1.SelectedDate = DateTime.Today;
                //RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                //Get value for Start date

                string sqlSDate = "Select Top 1 convert(datetime,DOB,103) from (";
                sqlSDate = sqlSDate + " Select * from ";
                sqlSDate = sqlSDate + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) )F order by DOB Asc";

                SqlDataReader sdr;
                sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                string startdate = "";
                DateTime strDate = Convert.ToDateTime(DateTime.Now);
                while (sdr.Read())
                {
                    startdate = sdr[0].ToString();
                }
                if (startdate != "")
                {
                    strDate = Convert.ToDateTime(startdate);
                }
                //Set value for 
                RadScheduler1.DataEndField = "DOB1";
                RadScheduler1.DataStartField = "DOB";
                RadScheduler1.DataDescriptionField = "Name";
                RadScheduler1.DataSubjectField = "Name";
                RadScheduler1.DataKeyField = "id";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                if (startdate != "")
                {
                    RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                }
            }
            if (querystring == "9")
            {
                //RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                //SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                //radList.DataSourceID = "SqlDataSource1";
                //radList.DataKeyField = "emp_type";
                //radList.DataValueField = "emp_type";
                //radList.DataTextField = "emp_type";
                //SqlDataSource1.SelectCommand = SelectCommand;


                //radlistDept.CheckBoxes = true;
                //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                //radlistDept.DataSourceID = "SqlDataSource2";
                //radlistDept.DataKeyField = "id";
                //radlistDept.DataValueField = "id";
                //radlistDept.DataTextField = "deptname";
                //SqlDataSource2.SelectCommand = SelectCommand;

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                else
                {
                    deptid = "(" + deptid + ")";
                }

                leaveType = "(" + leaveType + ")";

                SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
                SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
                SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where dept_id In"+ deptid +" And company_id=" + company_id;
                SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + company_id + "' ) AND  ";
                SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
                SelectCommand = SelectCommand + " AND confirmation_date is null";

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
            }

            if (querystring == "10")
            {
                RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                radList.DataSourceID = "SqlDataSource1";
                radList.DataKeyField = "emp_type";
                radList.DataValueField = "emp_type";
                radList.DataTextField = "emp_type";
                SqlDataSource1.SelectCommand = SelectCommand;
                radlistDept.CheckBoxes = true;


                string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                sqlSDate = sqlSDate + " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ) A Order by ExpDate Asc";

                SqlDataReader sdr;
                sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                string startdate = "";
                DateTime strDate = Convert.ToDateTime(DateTime.Now);
                while (sdr.Read())
                {
                    startdate = sdr[0].ToString();
                }
                if (startdate != "")
                {
                    strDate = Convert.ToDateTime(startdate);
                }

                leaveType = "(" + leaveType + ")";
                SelectCommand
                = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ";

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;

                if (startdate != "")
                {
                    RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                }


            }

         ///----------------------

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
            }
            System.Threading.Thread.Sleep(1000);
        }
        void radList_ItemCheck(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {

            foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
            {
                if (radi.Checked == true)
                {
                    if (deptid == "")
                    {
                        deptid = radi.Value;
                    }
                    else
                    {
                        deptid = deptid + "," + radi.Value;
                    }
                }
            }

            foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
            {
                if (radi.Checked == true)
                {
                    if (leaveType == "")
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = "'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = radi.Value;
                        }
                    }
                    else
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = leaveType + ",'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = leaveType + "," + radi.Value;
                        }
                    }
                }
            }


            if (querystring == "2")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "3")
            {

                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    SelectCommand
                           = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;

                }
                else
                {
                    SelectCommand
                       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "5")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 and emp_type in " + leaveType + " Order by " + company_id;
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "7")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "8")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) ";

                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            if (querystring == "10")
            {
                RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                radList.DataSourceID = "SqlDataSource1";
                radList.DataKeyField = "emp_type";
                radList.DataValueField = "emp_type";
                radList.DataTextField = "emp_type";
                SqlDataSource1.SelectCommand = SelectCommand;
                radlistDept.CheckBoxes = true;


                string sqlSDate = "Select Top 1 convert(datetime,ExpDate,103) from (";
                sqlSDate = sqlSDate + " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ) A Order by ExpDate Asc";

                SqlDataReader sdr;
                sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                string startdate = "";
                DateTime strDate = Convert.ToDateTime(DateTime.Now);
                while (sdr.Read())
                {
                    startdate = sdr[0].ToString();
                }
                if (startdate != "")
                {
                    strDate = Convert.ToDateTime(startdate);
                }

                leaveType = "(" + leaveType + ")";
                SelectCommand
                = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'License No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + company_id + "' ) ";

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;

                if (startdate != "")
                {
                    RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                }


            }

            else if (querystring == "1")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType + " and b.company_id=" + company_id; ;
                //    }
                //}


                //if (deptid != "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        deptid = "(" + deptid + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                //        leaveType = "(" + leaveType + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND a.leave_type in" + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in (-1)" + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                if (querystring == "1")
                {
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
            }
            else if (querystring == "6")
            {

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
                SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid;

                //}

                //if (deptid != "" && leaveType == "")
                //{
                //    deptid = "(" + deptid + ")";
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid;

                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    leaveType = "(" + leaveType + ")";

                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType;

                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
                //}

                RadScheduler1.DataEndField = "Todate";
                RadScheduler1.DataStartField = "FromDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }


            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
            }
            System.Threading.Thread.Sleep(1000);

        }

        

        void radList_ItemCreated(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            e.Item.Checked = true;
        }

        void radlistDept_ItemCreated(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            e.Item.Checked = true;
        }


        void radioCulture_ItemCreated(object sender, Telerik.Web.UI.RadListBoxItemEventArgs e)
        {
            e.Item.Checked = true;
        }

        void RadCalendar1_SelectionChanged(object sender, Telerik.Web.UI.Calendar.SelectedDatesEventArgs e)
        {
            try
            {
                foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
                {
                    if (radi.Checked == true)
                    {
                        if (leaveType == "")
                        {
                            if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                            {
                                leaveType = "'" + radi.Value + "'";
                            }
                            else
                            {
                                leaveType = radi.Value;
                            }
                        }
                        else
                        {
                            if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                            {
                                leaveType = leaveType + ",'" + radi.Value +"'";
                            }
                            else
                            {
                                leaveType = leaveType + "," + radi.Value;
                            }
                        }
                    }
                }

                foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
                {
                    if (radi.Checked == true)
                    {
                        if (deptid == "")
                        {
                            deptid = radi.Value;
                        }
                        else
                        {
                            deptid = deptid + "," + radi.Value;
                        }
                    }
                }

                if (querystring == "2")
                {
                    if (leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";
                        SelectCommand
                        = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                    }
                    else
                    {
                        SelectCommand
                        = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                    }              
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";                    
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
                else if(querystring=="7")
                {
                    if (leaveType != "")
                    {
                           leaveType = "(" + leaveType + ")";
                           
                            //SelectCommand
                            //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                            SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                            SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                            SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                            SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                            SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                            SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                    }
                    else
                    {
                        SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                        SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                        SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                        SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                        SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                        SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )";
                    }  

                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";                    
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
                else if (querystring == "8")
                {
                    if (leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";

                        //SelectCommand
                        //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                        SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                        SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                        SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                        SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                        SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                        SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                    }
                    else
                    {
                        SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                        SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                        SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                        SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                        SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                        SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) ";

                    }
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";                    
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
                else if (querystring == "9")
                {

                    if (deptid != "")
                    {
                        deptid = "(" + deptid + ")";
                        //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid;
                        SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
                        SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + company_id;
                        SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) AND  ";
                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
                        SelectCommand = SelectCommand + " AND confirmation_date is null and dept_id in " + deptid;

                    }
                    else
                    {
                        SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
                        SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + company_id;
                        SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) AND  ";
                        SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
                        SelectCommand = SelectCommand + " AND confirmation_date is null";

                    }
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

                }
                else if (querystring == "3")
                {

                    if (leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";
                        SelectCommand
                               = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;

                    }
                    else
                    {
                        SelectCommand
                           = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5 and Company_Id='" + company_id + "' ) ";
                    }

                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";                  
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
                //Certificate Expiring
                else if (querystring == "5")
                {
                    if (leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";
                        
                        //SelectCommand
                        //       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 and emp_type in " + leaveType + " Order by " + company_id;
                        SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                        SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                        SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                        SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                        SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;
                    }
                    else
                    {
                        SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                        SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                        SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                        SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                        SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )";
                    }                    
                    RadScheduler1.DataEndField = "ExpDate1";
                    RadScheduler1.DataStartField = "ExpDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";                   
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;                
                }
               //Leaves Approved
                else if (querystring == "1")
                {
                    if (leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";
                        //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND a.leave_type in " + leaveType + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=30";
                        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' ) And status='Approved'  AND a.leave_type in " + leaveType;
                    }
                    else
                    {
                        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and b.company_id=" + company_id + " and b.termination_date is null and abs(datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date)) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' ) And status='Approved'";  // AND a.leave_type in " + leaveType;
                    }
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

                }
                //BD
                else if (querystring == "4")
                {
                    if (deptid != "")
                    {
                        deptid = "(" + deptid + ")";
                        SelectCommand = " Select * from ";
                        SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department where dept_id in " + deptid + " )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";
                    }
                    else
                    {
                        SelectCommand = " Select * from ";
                        SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department where dept_id in " + deptid + " )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";
                    }

                    //RadCalendar1.SelectedDate = DateTime.Today;
                    //RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

                    RadScheduler1.DataEndField = "DOB1";
                    RadScheduler1.DataStartField = "DOB";
                    RadScheduler1.DataDescriptionField = "Name";
                    RadScheduler1.DataSubjectField = "Name";
                    RadScheduler1.DataKeyField = "id";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

                }
                else if (querystring == "6")
                {

                    if (deptid != "" && leaveType != "")
                    {
                        deptid = "(" + deptid + ")";
                        leaveType = "(" + leaveType + ")";
                        //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
                        SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                        SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                        SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid ;

                    }

                    if (deptid != "" && leaveType == "")
                    {
                        deptid = "(" + deptid + ")";
                        SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                        SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                        SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid ;

                    }

                    if (deptid == "" && leaveType != "")
                    {
                        leaveType = "(" + leaveType + ")";

                        SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                        SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                        SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType ;

                    }

                    if (deptid == "" && leaveType == "")
                    {
                        SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                        SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                        SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
                    }

                    RadScheduler1.DataEndField = "Todate";
                    RadScheduler1.DataStartField = "FromDate";
                    RadScheduler1.DataDescriptionField = "Subject";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    id1.Text = DateTime.Today.ToShortDateString();
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

                }

                if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
                {
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
                }

                if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
                {
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                }

                if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
                {
                    RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
                }

                System.Threading.Thread.Sleep(1000);

            }
            catch (Exception ex)
            {

            }
        }


        protected void OnAjaxUpdate(object sender, Telerik.Web.UI.ToolTipUpdateEventArgs e)
        {
            int aptId;
            Appointment apt;
            if (!int.TryParse(e.Value, out aptId))//The appoitnment is occurrence and FindByID expects a string 
                apt = RadScheduler1.Appointments.FindByID(e.Value);
            else //The appointment is not occurrence and FindByID expects an int 
                apt = RadScheduler1.Appointments.FindByID(aptId);

            //I changed this class name to match my project structure
            APPTP toolTip = (APPTP)LoadControl("APPTP.ascx");
            toolTip.TargetAppointment = apt;
            e.UpdatePanel.ContentTemplateContainer.Controls.Add(toolTip);
        }

        //protected void RadPanelBar1_ItemDataBound(object sender, Telerik.Web.UI.RadPanelBarEventArgs e)
        //{
        //    RadPanelItem item = (RadPanelItem)e.Item;
        //    int level = item.Level;
        //    if (level == 1)
        //    {
        //        CheckBox chk = new CheckBox();
        //        chk.CheckedChanged += new EventHandler(chk_CheckedChanged);
        //        item.Controls.Add(chk);
        //    }
        //}
        protected void chk_CheckChanged(object sender, EventArgs e)
        {

           
            CheckBox ck = (CheckBox)RadPanelBar1.FindItemByValue("rpitem1").FindControl("chk");
            if (ck.Checked == true)
            {

                foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
                {
                    radi.Checked = true;
                }

                

            }
            else
            {
                foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
                {
                    radi.Checked = false;
                }

            }
            
            //----------------


            foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
            {
                if (radi.Checked == true)
                {
                    if (deptid == "")
                    {
                        deptid = radi.Value;
                    }
                    else
                    {
                        deptid = deptid + "," + radi.Value;
                    }
                }
            }

            foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
            {
                if (radi.Checked == true)
                {
                    if (leaveType == "")
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = "'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = radi.Value;
                        }
                    }
                    else
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = leaveType + ",'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = leaveType + "," + radi.Value;
                        }
                    }
                }
            }


            if (querystring == "2")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "3")
            {

                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    SelectCommand
                           = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;

                }
                else
                {
                    SelectCommand
                       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "5")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 and emp_type in " + leaveType + " Order by " + company_id;
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "7")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "8")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) ";

                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "1")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType + " and b.company_id=" + company_id; ;
                //    }
                //}


                //if (deptid != "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        deptid = "(" + deptid + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                //        leaveType = "(" + leaveType + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND a.leave_type in" + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in (-1)" + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                if (querystring == "1")
                {
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
            }
            else if (querystring == "6")
            {

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
                SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid;

                //}

                //if (deptid != "" && leaveType == "")
                //{
                //    deptid = "(" + deptid + ")";
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid;

                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    leaveType = "(" + leaveType + ")";

                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType;

                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
                //}

                RadScheduler1.DataEndField = "Todate";
                RadScheduler1.DataStartField = "FromDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }


            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
            }
            System.Threading.Thread.Sleep(1000);
          //---------------------








        }
        protected void chk2_CheckChanged(object sender, EventArgs e)
        {


            CheckBox ck = (CheckBox)RadPanelBar2.FindItemByValue("rpitem2").FindControl("chk2");
            if (ck.Checked == true)
            {

                foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
                {
                    radi.Checked = true;
                }



            }
            else
            {
                foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
                {
                    radi.Checked = false;
                }

            }

            //----------------

            
            foreach (Telerik.Web.UI.RadListBoxItem radi in radlistDept.Items)
            {
                if (radi.Checked == true)
                {
                    if (deptid == "")
                    {
                        deptid = radi.Value;
                    }
                    else
                    {
                        deptid = deptid + "," + radi.Value;
                    }
                }
            }

            foreach (Telerik.Web.UI.RadListBoxItem radi in radList.Items)
            {
                if (radi.Checked == true)
                {
                    if (leaveType == "")
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = "'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = radi.Value;
                        }
                    }
                    else
                    {
                        if (querystring == "2" || querystring == "3" || querystring == "7" || querystring == "8" || querystring == "5")
                        {
                            leaveType = leaveType + ",'" + radi.Value + "'";
                        }
                        else
                        {
                            leaveType = leaveType + "," + radi.Value;
                        }
                    }
                }
            }


            if (querystring == "2")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand
                    = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' )";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "3")
            {

                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    SelectCommand
                           = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;

                }
                else
                {
                    SelectCommand
                       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;

            }
            else if (querystring == "5")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //       = "Select  emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'CSOC NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID where CertificateCategoryID =2 And termination_date is null and EY.company_id= " + company_id + "  and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30 and emp_type in " + leaveType + " Order by " + company_id;
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Certificate NO',Emp_type [Type], ";
                    SelectCommand = SelectCommand + " Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' ";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where ";
                    SelectCommand = SelectCommand + "(CertificateCategoryID > 6 Or CertificateCategoryID =5)  And termination_date is null and company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105)), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + company_id + "' ) ";
                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "7")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'PP NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',";
                    SelectCommand = SelectCommand + " datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days],DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1' from EmployeeCertificate EC  ";
                    SelectCommand = SelectCommand + " Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  ";
                    SelectCommand = SelectCommand + " EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null ";
                    SelectCommand = SelectCommand + " and EY.company_id= " + company_id + " and datediff(dd, convert(datetime,' " + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + company_id + "' ) ";
                }

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "8")
            {
                if (leaveType != "")
                {
                    leaveType = "(" + leaveType + ")";

                    //SelectCommand
                    //= "Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1',datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Description] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 6 And termination_date is null and EY.company_id= " + company_id + " and datediff(dd, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=30  and emp_type in " + leaveType + " Order by " + company_id;

                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' )  and emp_type in " + leaveType;
                }
                else
                {
                    SelectCommand = " Select emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'') Subject,CertificateNumber 'Insurance NO', ";
                    SelectCommand = SelectCommand + " Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'ExpDate',datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate) [Days] ,DATEADD(hour,12,Convert(Datetime, ExpiryDate,105)) 'ExpDate1'";
                    SelectCommand = SelectCommand + " from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code ";
                    SelectCommand = SelectCommand + " Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.COLID ";
                    SelectCommand = SelectCommand + " where CertificateCategoryID =1 And termination_date is null and EY.company_id= " + company_id;
                    SelectCommand = SelectCommand + " and datediff(dd, convert(datetime,'" + RadCalendar1.SelectedDate + "',105), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + company_id + "' ) ";

                }
                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
            }
            else if (querystring == "1")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType + " and b.company_id=" + company_id; ;
                //    }
                //}


                //if (deptid != "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        deptid = "(" + deptid + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in " + deptid + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    if (querystring == "1")
                //    {
                //        leaveType = "(" + leaveType + ")";
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND a.leave_type in" + leaveType + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    if (querystring == "1")
                //    {
                //        SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],DateAdd(hour,23,Convert(DATETIME,End_Date,103)) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id And status='Approved' AND b.dept_id in (-1)" + " and b.company_id=" + company_id + " and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + company_id + "' )";
                //    }
                //}

                if (querystring == "1")
                {
                    SchedulerDataSource.SelectCommand = SelectCommand;
                    RadScheduler1.DataEndField = "End";
                    RadScheduler1.DataStartField = "Start";
                    RadScheduler1.DataDescriptionField = "Description";
                    RadScheduler1.DataSubjectField = "Subject";
                    RadScheduler1.DataKeyField = "ID";
                    RadScheduler1.DataSourceID = "SchedulerDataSource";
                    RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                }
            }
            else if (querystring == "6")
            {

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                if (leaveType == "")
                {
                    leaveType = "('-1')";
                }

                //if (deptid != "" && leaveType != "")
                //{
                deptid = "(" + deptid + ")";
                leaveType = "(" + leaveType + ")";
                //SelectCommand = "select emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, Convert(DATETIME,Start_Date,103) [Start],Convert(DATETIME,End_Date,103) [End], NULL [RecurrenceRule],NULL [RecurrenceParentID],status [Description],1  [RoomID]from emp_leaves a,  employee b, leave_types c, emp_group d  where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND b.dept_id in " + deptid + " AND a.leave_type in " + leaveType;
                SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType + " AND  b.dept_id in " + deptid;

                //}

                //if (deptid != "" && leaveType == "")
                //{
                //    deptid = "(" + deptid + ")";
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")  AND  b.dept_id in " + deptid;

                //}

                //if (deptid == "" && leaveType != "")
                //{
                //    leaveType = "(" + leaveType + ")";

                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ") AND a.leave_type in " + leaveType;

                //}

                //if (deptid == "" && leaveType == "")
                //{
                //    SelectCommand = "select  emp_code ID,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Subject, c.Type Type,Convert(varchar(15),Start_Date,103) FromDate,DATEADD(hour,12,Convert(Datetime, End_Date,105)) ToDate,datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) [Days] ";
                //    SelectCommand = SelectCommand + " from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //    SelectCommand = SelectCommand + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' AND status <>'Approved' and b.termination_date is null and datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + company_id + "' ) and ";
                //    SelectCommand = SelectCommand + " b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = " + company_id + ")";
                //}

                RadScheduler1.DataEndField = "Todate";
                RadScheduler1.DataStartField = "FromDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;


            }
            //BD
            if (querystring == "4")
            {
                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                else
                {
                    deptid = "("+ deptid +")";
                }
                //RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                //RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                //radlistDept.DataSourceID = "SqlDataSource2";
                //radlistDept.DataKeyField = "id";
                //radlistDept.DataValueField = "id";
                //radlistDept.DataTextField = "deptname";
                //SqlDataSource2.SelectCommand = SelectCommand;
                //radlistDept.CheckBoxes = true;

                SelectCommand = " Select * from ";
                SelectCommand = SelectCommand + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In "+ deptid +" AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) ";

                //RadCalendar1.SelectedDate = DateTime.Today;
                //RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                //Get value for Start date

                string sqlSDate = "Select Top 1 convert(datetime,DOB,103) from (";
                sqlSDate = sqlSDate + " Select * from ";
                sqlSDate = sqlSDate + "(SELECT a.emp_code id,isnull(emp_name,'')+' '+isnull(emp_lname,'') AS [Name],DATENAME(month, a.date_of_birth) + ' ' + CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + ' ' + CONVERT(VARCHAR(100), YEAR(a.date_of_birth), 103)  'DateOfBirth',Convert(Datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103)  'DOB',DATEADD(hour,23, convert(datetime,CONVERT(VARCHAR(15), DAY(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), MONTH(a.date_of_birth), 103) + '/' + CONVERT(VARCHAR(15), YEAR(GetDate()), 103),103))'DOB1' FROM   employee AS a  Where a.dept_id In(Select id from department )AND a.termination_date IS NULL )A Where A.DOB Between convert(datetime,Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103) AND DATEADD(day,32,Convert(Datetime, Convert(Datetime,'" + RadCalendar1.SelectedDate + "',105),103)) )F order by DOB Asc";

                SqlDataReader sdr;
                sdr = DataAccess.ExecuteReader(CommandType.Text, sqlSDate, null);
                string startdate = "";
                DateTime strDate = Convert.ToDateTime(DateTime.Now);
                while (sdr.Read())
                {
                    startdate = sdr[0].ToString();
                }
                if (startdate != "")
                {
                    strDate = Convert.ToDateTime(startdate);
                }
                //Set value for 
                RadScheduler1.DataEndField = "DOB1";
                RadScheduler1.DataStartField = "DOB";
                RadScheduler1.DataDescriptionField = "Name";
                RadScheduler1.DataSubjectField = "Name";
                RadScheduler1.DataKeyField = "id";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
                if (startdate != "")
                {
                    RadScheduler1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.FocusedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                    RadCalendar1.SelectedDate = Convert.ToDateTime("1/" + strDate.Month + "/" + strDate.Year);
                }
            }

            if (querystring == "9")
            {
                //RadPanelBar1.FindItemByValue("ctrlPanel1").Text = "Employee Type";
                RadPanelBar2.FindItemByValue("ctrlPanel5").Text = "Department";
                RadPanelBar3.FindItemByValue("ctrlPanel7").Text = "Language";

                //SelectCommand = "Select Distinct(emp_type) from employee Where emp_type is not null";
                //radList.DataSourceID = "SqlDataSource1";
                //radList.DataKeyField = "emp_type";
                //radList.DataValueField = "emp_type";
                //radList.DataTextField = "emp_type";
                //SqlDataSource1.SelectCommand = SelectCommand;


                //radlistDept.CheckBoxes = true;
                //SelectCommand = "Select id,deptname from department Where company_id=" + company_id;
                //radlistDept.DataSourceID = "SqlDataSource2";
                //radlistDept.DataKeyField = "id";
                //radlistDept.DataValueField = "id";
                //radlistDept.DataTextField = "deptname";
                //SqlDataSource2.SelectCommand = SelectCommand;

                if (deptid == "")
                {
                    deptid = "('-1')";
                }
                else
                {
                    deptid = "(" + deptid + ")";
                }

                leaveType = "(" + leaveType + ")";

                SelectCommand = " select   emp_code ID,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Subject,convert(varchar(20),joining_date,103) 'Date Of Joining',";
                SelectCommand = SelectCommand + " convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'ExpDate',dateadd(hour,12,convert(datetime,dateadd(month,probation_period,joining_date),103))'ExpDate1',";
                SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date)) [Days] from employee where dept_id In" + deptid + " And company_id=" + company_id;
                SelectCommand = SelectCommand + " AND termination_date is null   AND datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + company_id + "' ) AND  ";
                SelectCommand = SelectCommand + " datediff(dd,convert(datetime,'" + RadCalendar1.SelectedDate + "',105),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1 ";
                SelectCommand = SelectCommand + " AND confirmation_date is null";

                RadScheduler1.DataEndField = "ExpDate1";
                RadScheduler1.DataStartField = "ExpDate";
                RadScheduler1.DataDescriptionField = "Subject";
                RadScheduler1.DataSubjectField = "Subject";
                RadScheduler1.DataKeyField = "ID";
                RadScheduler1.DataSourceID = "SchedulerDataSource";
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
                id1.Text = DateTime.Today.ToShortDateString();
                SchedulerDataSource.SelectCommand = SelectCommand;
            }



            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.DayView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.DayView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.MonthView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.MonthView;
            }

            if (RadScheduler1.SelectedView == Telerik.Web.UI.SchedulerViewType.WeekView)
            {
                RadScheduler1.SelectedView = Telerik.Web.UI.SchedulerViewType.WeekView;
            }
            System.Threading.Thread.Sleep(1000);
            //---------------------








        } 
    }
}
