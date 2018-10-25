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

namespace SMEPayroll.Management
{
    public partial class StockBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            sqlSelectBalance.ConnectionString = Session["ConString"].ToString();
        }

        protected void btnExportWord_click(object sender, EventArgs e)
        {
            gvStockBalance.ExportSettings.IgnorePaging = true;
            gvStockBalance.ExportSettings.ExportOnlyData = true;
            gvStockBalance.ExportSettings.OpenInNewWindow = true;
            gvStockBalance.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            gvStockBalance.ExportSettings.ExportOnlyData = true;
            gvStockBalance.ExportSettings.IgnorePaging = true;
            gvStockBalance.ExportSettings.OpenInNewWindow = true;
            gvStockBalance.MasterTableView.ExportToExcel();
        }

        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            gvStockBalance.ExportSettings.ExportOnlyData = true;
            gvStockBalance.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((gvStockBalance.Items[0].Cells.Count * 30)) + "mm");
            gvStockBalance.ExportSettings.OpenInNewWindow = true;
            gvStockBalance.MasterTableView.ExportToPdf();
        }
    }
}
