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
using System.Globalization;

namespace SMEPayroll.Management
{
    public partial class DailyAttendanceReport : System.Web.UI.Page
    {
        int comp_id;

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Utility.ToInteger(Session["Compid"]);
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            DataSet ds = new DataSet();

            string sqlQuery = "";

            if (comp_id == 1)
            {

                sqlQuery = "select ROW_NUMBER() OVER(ORDER BY GETDATE()) AS SNO,*,(Total-Actual)as Balance from ( select (select Location_Name from Location where ID=P.Location_ID) Location,SP.ID ,Sub_Project_Name,DA.Total as Total,(Select count(Emp_Code)as count From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssignedEOY Where SubProjectID=DA.ProjectId And Convert(datetime,EntryDate,103)  <= Convert(datetime,'" + RadDatePicker1.SelectedDate + "',103))) as Actual,DA.Remark as Remark,DA.PIC,DA.Shift from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID";
                sqlQuery = sqlQuery + " inner join DailyAttendance DA on DA.ProjectId=SP.ID where SP.Active=1 and Date=(select Top 1 DATE from DailyAttendance where CONVERT(DATETIME, [Date], 103) < =CONVERT(DATETIME, '" + RadDatePicker1.SelectedDate + "' , 103)";
                sqlQuery = sqlQuery + " order by [Date] desc) )A";
            }
            else
            {
                sqlQuery = "select ROW_NUMBER() OVER(ORDER BY GETDATE()) AS SNO,*,(Total-Actual)as Balance from ( select (select Location_Name from Location where ID=P.Location_ID) Location,SP.ID ,Sub_Project_Name,DA.Total as Total,(Select count(Emp_Code)as count From Employee Where Emp_Code In(Select Emp_ID From MultiProjectAssignedEOY Where SubProjectID=DA.ProjectId And Convert(datetime,EntryDate,103)  <= Convert(datetime,'" + RadDatePicker1.SelectedDate + "',103)) And Company_ID = '" + comp_id + "' ) as Actual,DA.Remark as Remark,DA.PIC,DA.Shift from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID";
                sqlQuery = sqlQuery + " inner join DailyAttendance DA on DA.ProjectId=SP.ID where P.Company_ID='" + comp_id + "' and SP.Active=1 and Date=(select Top 1 DATE from DailyAttendance where CONVERT(DATETIME, [Date], 103) < =CONVERT(DATETIME, '" + RadDatePicker1.SelectedDate + "' , 103)";
                sqlQuery = sqlQuery + " order by [Date] desc) )A";
            }






             ds = GetDataSet(sqlQuery);

             RadGrid1.DataSource = ds;
             RadGrid1.DataBind();
        }


        protected void btnExportWord_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
            
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid1.Items[0].Cells.Count * 30)) + "mm");
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToPdf();
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            //GridSettingsPersister obj1 = new GridSettingsPersister();
            //obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
            ExportGridHeader(e);

        }

        string company_name, customHTML3;
        public void ExportGridHeader( GridExportingArgs e)
        {

            #region Grab Info from DB
            string sql = "";
            if (comp_id == 1)
            {
               sql = "select company_name from Company";
            }
            else
            {
                sql = "select company_name from Company where Company_Id='" + comp_id + "'";
            }
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                company_name = dr.GetString(dr.GetOrdinal("company_name"));
            }
            #endregion



            string customHTML = "<div width=\"100%\" style=\"text-align:center;font-size:8px;font-family:Tahoma;\">" +
                                " <table width='100%'border='0'>" +
                                    "<tr><td colspan='7'  style=\"text-align:center;font-size:12px;font-family:Tahoma;\"  ><b>" + company_name.ToUpper() + "</b></td></tr> "+
                                     "<tr><td colspan='7'  style=\"text-align:center;font-size:12px;font-family:Tahoma;\" ><b>DAILY ATTENDANCE REPORT</b></td></tr> "+
                                    "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>DAY:</b>" + Convert.ToDateTime(RadDatePicker1.SelectedDate).DayOfWeek.ToString() + "" +
                                     "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <b>DATE:" + Convert.ToDateTime(RadDatePicker1.SelectedDate).ToString("dd-MMM-yyyy") + "</b></td></tr> ";

           


            customHTML3 = "</table>" +
                                "</div>";
            e.ExportOutput = e.ExportOutput.Replace("<body>", String.Format("<body>{0}", customHTML + customHTML3));
        }


        #region Helper
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        #endregion

    }
}
