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

namespace SMEPayroll.TimeSheet
{
    public partial class RosterReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                ds = GetDataSet("select (select time_card_no from employee where Emp_code=Roster_ID) as Emp_code,(select (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) from employee where Emp_code=Roster_ID) as Name ,(select wdays_per_week from employee where emp_code=Roster_ID  )DaysToWorks,count(Roster_ID) DaysAssigned, ((select wdays_per_week from employee where emp_code=Roster_ID) - count(Roster_ID)) as Balance  from RosterDetail RD where Roster_Date  between Convert(datetime,'" + Session["Fromdate"].ToString() + "',103) and Convert(datetime,'" + Session["Todate"].ToString() + "',103)group by Roster_ID");
                empResults.DataSource = ds;
                empResults.DataBind();
            }
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((empResults.Items[0].Cells.Count * 30)) + "mm");
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToPdf();
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
