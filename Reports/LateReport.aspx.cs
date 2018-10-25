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
    public partial class LateReport : System.Web.UI.Page
    {
        DataTable dataTable;
        int compid;
        string strtranid;
        string strEmpCode = "";
        string varEmpCode = "";
        string sgroupname = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            varEmpCode = Session["EmpCode"] != null ? Session["EmpCode"].ToString() : "";
            compid = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");


            if (!IsPostBack)
            {
                RadGrid2.ExportSettings.FileName = "Lateness_Report";

            }



        }



        protected void bindgrid(object sender, EventArgs e)
        {
            bindgrid();
        }



        public void bindgrid()
        {
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            //dsEmpLeaves 
            //Get the data for 
          
            string sDate = rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year;
            string eDate = rdEmpPrjEnd.SelectedDate.Value.Day + "/" + rdEmpPrjEnd.SelectedDate.Value.Month + "/" + rdEmpPrjEnd.SelectedDate.Value.Year;
            string strLeaves = "";






          int  comp_id = Utility.ToInteger(Session["Compid"]);



            DataSet Lateness = new DataSet();

            string ssql = "sp_latenesstreport";
            SqlParameter[] parms = new SqlParameter[5];
            parms[0] = new SqlParameter("@timecardno", "");
            parms[1] = new SqlParameter("@sdate", sDate);
            parms[2] = new SqlParameter("@enddate", eDate);
            parms[3] = new SqlParameter("@companyId", comp_id);
            parms[4] = new SqlParameter("@depatid", Utility.ToInteger(deptID.SelectedValue));
            Lateness = DataAccess.ExecuteSPDataSet(ssql, parms);
            if (Lateness.Tables.Count > 0)
            {
                this.RadGrid2.DataSource = Lateness;
                this.RadGrid2.DataBind();
            }
            //strLeaves = "Select * from ApprovedTimeSheet  Where [Time_Card_No] = '" + strempcode + "'  and convert(DATETIME,TimeEntryStart,103) ";
            //strLeaves = strLeaves + " BETWEEN convert(DATETIME,'" + sDate + "',103) AND convert(DATETIME,'" + eDate + "',103) ";
            //strLeaves = strLeaves + " AND convert(DATETIME,TimeEntryEnd,103) BETWEEN convert(DATETIME,'" + sDate + "',103) AND  convert(DATETIME,'" + eDate + "',103)";

            //dsleavestemp = DataAccess.FetchRS(CommandType.Text, strLeaves, null);

        
        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.IgnorePaging = true;
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            RadGrid2.ExportSettings.ExportOnlyData = true;
            RadGrid2.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid2.Items[0].Cells.Count * 30)) + "mm");
            RadGrid2.ExportSettings.OpenInNewWindow = true;
            RadGrid2.MasterTableView.ExportToPdf();
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
