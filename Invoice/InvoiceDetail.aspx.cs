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

namespace SMEPayroll.Invoice
{
    public partial class InvoiceDetail : System.Web.UI.Page
    {
        public string Fromdate, Todate, ProjectId, Client, Trade, HourMonth;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!IsPostBack)
            {

                if (Request.QueryString["FromDate"] != null)
                {
                    Fromdate = Request.QueryString["FromDate"].ToString();
                }
                if (Request.QueryString["Todate"] != null)
                {
                    Todate = Request.QueryString["Todate"].ToString();
                }
                if (Request.QueryString["ProjectId"] != null)
                {
                    ProjectId = Request.QueryString["ProjectId"].ToString();
                }

                if (Request.QueryString["Trade"] != null)
                {
                    Trade = Request.QueryString["Trade"].ToString();
                }

                if (Request.QueryString["Client"] != null)
                {
                    Client = Request.QueryString["Client"].ToString();
                }
                

              

                string sp_sql = "Sp_Invoice_Details";
                SqlParameter[] parms = new SqlParameter[5];
                parms[0] = new SqlParameter("@FromDate", Fromdate);
                parms[1] = new SqlParameter("@ToDate", Todate);
                parms[2] = new SqlParameter("@ProjectID", ProjectId);
                parms[3] = new SqlParameter("@Trade", Trade);
                parms[4] = new SqlParameter("@Client", Client);
                DataSet ds = DataAccess.ExecuteSPDataSet(sp_sql, parms);

                RadGrid_InvoiceDetail.DataSource = ds;
                RadGrid_InvoiceDetail.MasterTableView.DataSource = ds;
                RadGrid_InvoiceDetail.DataBind();

            }
        }

        protected void btnExportPdf_Click(object sender, ImageClickEventArgs e)
        {
            RadGrid_InvoiceDetail.ExportSettings.ExportOnlyData = true;
            RadGrid_InvoiceDetail.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid_InvoiceDetail.Items[0].Cells.Count * 30)) + "mm");
            RadGrid_InvoiceDetail.ExportSettings.OpenInNewWindow = true;
            RadGrid_InvoiceDetail.MasterTableView.ExportToPdf();
        }
    }
}
