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
using Telerik.Web.UI;

namespace SMEPayroll.Reports
{
    public partial class CustomReportVariance_Employee : System.Web.UI.Page
    {
        #region oldcode
        
      
        //int compid, year;
        //public string StartMonth, EndMonth, FirstMonth, LastMonth, FirstMonth1, LastMonth1;
        //protected void Page_Load(object sender, EventArgs e)
        //{
      

        //        #region Load grid and  footer Total
        //        if (!IsPostBack)
        //        {
        //            LoadGrid();

        //            for (int i = 0; i < Variance_Employee.MasterTableView.AutoGeneratedColumns.Length; i++)
        //            {
        //                GridBoundColumn boundColumn = (Variance_Employee.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);
        //                //boundColumn.DataFormatString = "<nobr>{0}</nobr>";

        //                if (boundColumn != null)
        //                {
        //                    if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal" || boundColumn.DataType.Name == "Int32")
        //                    {

        //                        boundColumn.DataType = Type.GetType("System.Int32");
        //                        boundColumn.Aggregate = GridAggregateFunction.Sum;
        //                        boundColumn.DataFormatString = "{0:N2}";

        //                    }
        //                }
        //            }

        //            Variance_Employee.Rebind();
        //        }
                    
        //        #endregion

        // }

        //protected void LoadGrid()
        //{
        //    compid = Convert.ToInt32(Request.QueryString["company_Id"]);
        //    year = Convert.ToInt32(Request.QueryString["year"]);
        //    StartMonth = Request.QueryString["StartMonth"];
        //    EndMonth = Request.QueryString["EndMonth"];

        //    FirstMonth=GetFirstMonth(StartMonth,year);
        //    LastMonth = GetLastMonth(EndMonth, year);
         


        //    SqlParameter[] parms = new SqlParameter[5];
        //    parms[0] = new SqlParameter("@StartMonth", StartMonth);
        //    parms[1] = new SqlParameter("@EndMonth", EndMonth);
        //    parms[2] = new SqlParameter("@start_date", FirstMonth);
        //    parms[3] = new SqlParameter("@end_date", LastMonth);
        //    parms[4] = new SqlParameter("@company_Id", compid);

        //    string sSQL = "Sp_EmployeeReport";
        //    DataSet ds = new DataSet();
        //    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
        //    Variance_Employee.DataSource = ds;
        //    Variance_Employee.DataBind();
        //}

        //public string GetLastMonth(string EndMonth, int year)
        //{
        //    if (EndMonth == "January") { LastMonth1 = "" + year + "-01-01"; }//year-m-d
        //    else if (EndMonth == "February") { LastMonth1 = "" + year + "-02-01"; }
        //    else if (EndMonth == "March") { LastMonth1 = "" + year + "-03-01"; }
        //    else if (EndMonth == "April") { LastMonth1 = "" + year + "-04-01"; }
        //    else if (EndMonth == "May") { LastMonth1 = "" + year + "-05-01"; }
        //    else if (EndMonth == "June") { LastMonth1 = "" + year + "-06-01"; }
        //    else if (EndMonth == "July") { LastMonth1 = "" + year + "-07-01"; }
        //    else if (EndMonth == "August") { LastMonth1 = "" + year + "-08-01"; }
        //    else if (EndMonth == "September") { LastMonth1 = "" + year + "-09-01"; }
        //    else if (EndMonth == "October") { LastMonth1 = "" + year + "-10-01"; }
        //    else if (EndMonth == "November") { LastMonth1 = "" + year + "-11-01"; }
        //    else if (EndMonth == "December") { LastMonth1 = "" + year + "-12-01"; }

        //    return LastMonth1;
        //}

        //public string GetFirstMonth(string StartMonth,int year)
        //{
            
        //    if (StartMonth == "January") { FirstMonth1 = "" + year + "-01-01"; }//year-m-d
        //    else if (StartMonth == "February") { FirstMonth1 = "" + year + "-02-01"; }
        //    else if (StartMonth == "March") { FirstMonth1 = "" + year + "-03-01"; }
        //    else if (StartMonth == "April") { FirstMonth1 = "" + year + "-04-01"; }
        //    else if (StartMonth == "May") { FirstMonth1 = "" + year + "-05-01"; }
        //    else if (StartMonth == "June") { FirstMonth1 = "" + year + "-06-01"; }
        //    else if (StartMonth == "July") { FirstMonth1 = "" + year + "-07-01"; }
        //    else if (StartMonth == "August") { FirstMonth1 = "" + year + "-08-01"; }
        //    else if (StartMonth == "September") { FirstMonth1 = "" + year + "-09-01"; }
        //    else if (StartMonth == "October") { FirstMonth1 = "" + year + "-10-01"; }
        //    else if (StartMonth == "November") { FirstMonth1 = "" + year + "-11-01"; }
        //    else if (StartMonth == "December") { FirstMonth1 = "" + year + "-12-01"; }

        //    return FirstMonth1;
        //}

        //protected void btnExportWord_click(object sender, EventArgs e)
        //{
        //    Variance_Employee.ExportSettings.IgnorePaging = true;
        //    Variance_Employee.ExportSettings.ExportOnlyData = true;
        //    Variance_Employee.ExportSettings.OpenInNewWindow = true;
        //    Variance_Employee.MasterTableView.ExportToWord();
        //}

        //protected void btnExportExcel_click(object sender, EventArgs e)
        //{
        //    //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
        //    Variance_Employee.ExportSettings.ExportOnlyData = true;
        //    Variance_Employee.ExportSettings.IgnorePaging = true;
        //    Variance_Employee.ExportSettings.OpenInNewWindow = true;
        //    Variance_Employee.MasterTableView.ExportToExcel();
        //}
        //protected void btnExportPdf_click(object sender, EventArgs e)
        //{
        //    Variance_Employee.ExportSettings.ExportOnlyData = true;
        //    Variance_Employee.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((Variance_Employee.Items[0].Cells.Count * 30)) + "mm");
        //    Variance_Employee.ExportSettings.OpenInNewWindow = true;
        //    Variance_Employee.MasterTableView.ExportToPdf();
        //}



        //protected void Variance_Employee_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        //{
        //    GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;

        //    SqlDataSource2.ConnectionString = Session["ConString"].ToString();
        //    string Description = dataItem.GetDataKeyValue("Description").ToString();
        //    if (Description == "Active")
        //    {
        //        //SqlDataSource2.SelectCommand = "select emp_code,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) AS 'EMP_NAME',joining_date  from Employee where StatusID=1 And Termination_Date is null AND Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND emp_code NOT IN (select emp_code from Employee where StatusID=1 And Termination_Date is null And Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND joining_date<='" + GetFirstMonth((Request.QueryString["StartMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "' union select emp_code  from Employee where StatusID=1 And Termination_Date is null And Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND joining_date<='" + GetLastMonth((Request.QueryString["EndMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "')";
        //        SqlDataSource2.SelectCommand = "select emp_code,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) AS 'EMP_NAME',joining_date,company_id  from Employee where emp_code IN(select emp_code   FROM(select emp_code from Employee where StatusID=1 And Termination_Date is null And Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND joining_date<='" + GetFirstMonth((Request.QueryString["StartMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "' union all select emp_code  from Employee where StatusID=1 And Termination_Date is null And Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND joining_date<='" + GetLastMonth((Request.QueryString["EndMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "')Foo GROUP BY emp_code Having  count(*)=1)";
        //    }
        //    else if(Description=="Terminate")
        //    {
        //        SqlDataSource2.SelectCommand = "select emp_code,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) AS 'EMP_NAME',joining_date  from Employee where  Termination_Date is not null AND Company_ID='" + Convert.ToInt32(Request.QueryString["company_Id"]) + "' AND emp_code NOT IN ( select emp_code from employee where  datepart(month,Termination_Date)=datepart(month,'" + GetFirstMonth((Request.QueryString["StartMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "') AND datepart(year,Termination_Date)=datepart(year,'" + GetLastMonth((Request.QueryString["EndMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "') union select emp_code  from employee where  datepart(month,Termination_Date)=datepart(month,'" + GetFirstMonth((Request.QueryString["StartMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "') AND datepart(year,Termination_Date)=datepart(year,'" + GetLastMonth((Request.QueryString["EndMonth"]), (Convert.ToInt32(Request.QueryString["year"]))) + "'))";
        //    }
        //    e.DetailTableView.DataSource = SqlDataSource2.Select(new DataSourceSelectArguments());

        //}

        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
                LoadGrid_Terminate();
            }
        }

        protected void LoadGrid_Terminate()
        {
            try
            {
                int compid = Convert.ToInt32(Request.QueryString["company_Id"]);
                string StartDate = Request.QueryString["StartDate"];
                string EndDate = Request.QueryString["EndDate"];

                DataSet ds_CPF1 = new DataSet();
                ds_CPF1 = getDataSet("select (select time_card_no from employee where emp_code=e.emp_code) TimeCardId,(select Deptname from department where id=e.dept_id)Department ,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) AS 'EMP_NAME',Termination_Date,company_id from employee e where (Convert(Datetime,Termination_Date,103)>= Convert(Datetime,'" + StartDate + "',103) AND Convert(Datetime,Termination_Date,103)<= Convert(Datetime,'" + EndDate + "',103)) AND company_id='" + compid + "'ORDER BY e.Termination_Date Asc");
                Variance_Terminate.DataSource = ds_CPF1;
                Variance_Terminate.DataBind();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected void LoadGrid()
        {

            try
            {
                int compid = Convert.ToInt32(Request.QueryString["company_Id"]);
                string  StartDate = Request.QueryString["StartDate"];
                string EndDate = Request.QueryString["EndDate"];

                DataSet ds_CPF = new DataSet();
                ds_CPF = getDataSet("select (select time_card_no from employee where emp_code=e.emp_code) TimeCardId,(select Deptname from department where id=e.dept_id)Department ,(isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) AS 'EMP_NAME', joining_date from employee e where (Convert(Datetime,joining_date,103)>= Convert(Datetime,'" + StartDate + "',103) AND Convert(Datetime,joining_date,103)<= Convert(Datetime,'" + EndDate + "',103)) AND  StatusID=1 AND  Termination_Date is null And Company_ID='" + compid + "'  ORDER BY e.joining_date Asc");
                Variance_Join.DataSource = ds_CPF;
                Variance_Join.DataBind();
            }
            catch (Exception e)
            {
                throw;
            }
        }


        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;

        }


        #region oldcode
        protected void btnExportWord_click(object sender, EventArgs e)
        {
            Variance_Terminate.ExportSettings.IgnorePaging = true;
            Variance_Terminate.ExportSettings.ExportOnlyData = true;
            Variance_Terminate.ExportSettings.OpenInNewWindow = true;
            Variance_Terminate.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            Variance_Terminate.ExportSettings.ExportOnlyData = true;
            Variance_Terminate.ExportSettings.IgnorePaging = true;
            Variance_Terminate.ExportSettings.OpenInNewWindow = true;
            Variance_Terminate.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            Variance_Terminate.ExportSettings.ExportOnlyData = true;
            Variance_Terminate.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((Variance_Terminate.Items[0].Cells.Count * 30)) + "mm");
            Variance_Terminate.ExportSettings.OpenInNewWindow = true;
            Variance_Terminate.MasterTableView.ExportToPdf();
        }


        protected void btnExportWord1_click(object sender, EventArgs e)
        {
            Variance_Join.ExportSettings.IgnorePaging = true;
            Variance_Join.ExportSettings.ExportOnlyData = true;
            Variance_Join.ExportSettings.OpenInNewWindow = true;
            Variance_Join.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel1_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            Variance_Join.ExportSettings.ExportOnlyData = true;
            Variance_Join.ExportSettings.IgnorePaging = true;
            Variance_Join.ExportSettings.OpenInNewWindow = true;
            Variance_Join.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf1_click(object sender, EventArgs e)
        {
            Variance_Join.ExportSettings.ExportOnlyData = true;
            Variance_Join.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((Variance_Join.Items[0].Cells.Count * 30)) + "mm");
            Variance_Join.ExportSettings.OpenInNewWindow = true;
            Variance_Join.MasterTableView.ExportToPdf();
        }

        #endregion

    }
}
