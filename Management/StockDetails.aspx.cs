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

namespace SMEPayroll.Management
{
    public partial class StockDetails : System.Web.UI.Page
    {
        string strMessage = "";
        string strStore = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
        }
        //RadGrid1_PreRender
        public void RadGrid1_PreRender(object sender, EventArgs e)
        {
            GridGroupByField field = new GridGroupByField();
            field.FieldName = "ItemName";
            field.HeaderText = "ItemName";
            field.HeaderValueSeparator = "ItemName : ";

            //GridGroupByField fieldQty = new GridGroupByField();
            //fieldQty.FieldName = "Quantity";
            //fieldQty.HeaderText = " ";
            //fieldQty.HeaderValueSeparator = "Total :  ";
            //fieldQty.Aggregate = Telerik.Web.UI.GridAggregateFunction.Sum;

            GridGroupByExpression ex = new GridGroupByExpression();
            ex.GroupByFields.Add(field);
            ex.SelectFields.Add(field);
            //ex.SelectFields.Add(fieldQty);
           
            RadGrid1.MasterTableView.GroupByExpressions.Add(ex);

            RadGrid1.Rebind();
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
    }
}
