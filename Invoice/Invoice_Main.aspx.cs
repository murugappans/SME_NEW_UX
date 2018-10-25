using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SMEPayroll.Invoice
{
    public partial class Invoice_Main : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        public int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {
                BindGrid();
            }

        }

       

        

        protected void RadGrid2_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string sSQL = "SELECT [IID],[InvoiceNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreateDate],[company_id]  FROM [dbo].[invoice_info] Q where Q.[confirm]='1' and [company_id]='" + compid + "' ORDER BY [IId] DESC";
            DataSet ds = GetDataSet(sSQL);
            RadGrid2.DataSource = ds;
        }

        #region Grid
        protected void BindGrid()
            {
                string sSQL = "SELECT [IID],[InvoiceNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreateDate],[company_id]  FROM [dbo].[invoice_info] Q where Q.[confirm]='1' and  [company_id]='" + compid + "' ORDER BY [IId] DESC";
                DataSet ds = GetDataSet(sSQL);
                RadGrid2.DataSource = ds;
                RadGrid2.DataBind();
            }

        #endregion
        #region Utility
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
       #endregion


       

        protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");

                Session["Invoice"] = dataItem["InvoiceNo"].Text;

                Response.Redirect("EditInvoice.aspx?InvoiceNo=" + dataItem["InvoiceNo"].Text);
            }
        }

    }
}
