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
using System.Net.Mail;
using System.Text;

namespace SMEPayroll.Reports
{
    public partial class CustomReportPayroll : System.Web.UI.Page
    {
        static int s = 0;
        string compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet sqlRptDs;

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.SelectCommand = Session["SesSql"].ToString();
            //sqlRptDs = (DataSet)Session["rptDs"];
            //if (!IsPostBack)
            //{
            //    empResults.DataSource = sqlRptDs;
            //    empResults.DataBind();
            //}
        }


        //protected void RadGrid7_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        //    {
        //        try
        //        {
        //            //GridEditableItem editedItem = e.Item as GridEditableItem;
        //            GridBoundColumn eitem = e.Item.DataItem as GridBoundColumn;
        //            eitem.DataFormatString = "<nobr>{0}</nobr>";
        //        }
        //        catch (Exception ex)
        //        {
        //            string ErrMsg = ex.Message;
        //        }
        //    }
        //}

        //protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        //{
        //    sqlRptDs = (DataSet)Session["rptDs"];
        //    empResults.DataSource = sqlRptDs;
        //    empResults.DataBind();
        //}
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

        protected void empResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            e.Item.Expanded = false;
        }
    }
}




