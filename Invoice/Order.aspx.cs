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
    public partial class Order : System.Web.UI.Page
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
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            RadGrid1.PageIndexChanged += new GridPageChangedEventHandler(RadGrid1_PageIndexChanged);
            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
        }

        private void BindGrid()
        {
            string sSQL = "SELECT (Prefix+''+CONVERT(varchar,[OrderNo])) as [Order],[OrderNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreatedDate],[Remark],[SalesRep],[company_id],Documentpath  FROM [dbo].[Order_Info] Q where [company_id]='" + compid + "' ORDER BY [OrderNo] DESC";
            DataSet ds = GetDataSet(sSQL);
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }
        void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sSQL = "SELECT (Prefix+''+CONVERT(varchar,[OrderNo])) as [Order],[OrderNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreatedDate],[Remark],[SalesRep],[company_id],Documentpath  FROM [dbo].[Order_Info] Q where [company_id]='" + compid + "' ORDER BY [OrderNo] DESC";
            DataSet ds = GetDataSet(sSQL);
            RadGrid1.DataSource = ds;
        }
        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }
        void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("ConvertOrder.aspx?orderNo=" + dataItem["OrderNo"].Text);
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                //string empcode = Convert.ToString(e.Item.Cells[4].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    //hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.NavigateUrl = hl.Text.ToString(); 
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";

                }
                else
                {
                    hl.Text = "No Document";
                }
            }
        }

        #region Utility
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        #endregion
    }
}
