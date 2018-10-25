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
    public partial class Quotation : System.Web.UI.Page
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
               // IfNotSaveDeleteQuotation();
                BindGrid();
            }

            RadGrid2.ItemCommand += new GridCommandEventHandler(RadGrid2_ItemCommand);
            RadGrid2.PageIndexChanged+=new GridPageChangedEventHandler(RadGrid2_PageIndexChanged);
            RadGrid2.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid2_NeedDataSource);
            RadGrid2.DeleteCommand+=new GridCommandEventHandler(RadGrid2_DeleteCommand);
        }

        private void IfNotSaveDeleteQuotation()
        {
            string sSQL1 = "DELETE FROM [Quotation_Info] WHERE [QuotationNo] IN (select QuotationNo from Quotation_info where ClientId='0');DELETE FROM [QuoationMaster_hourly] WHERE [QuotationNo] IN (select QuotationNo from Quotation_info where ClientId='0');DELETE FROM [QuoationMaster_Monthly] WHERE [QuotationNo] IN(select QuotationNo from Quotation_info where ClientId='0')";
            int retVal = DataAccess.ExecuteStoreProc(sSQL1);
        }

        #region Grid
        /// <summary>
        /// Binds the grid.
        /// </summary>
        /// <remarks></remarks>
        private void BindGrid()
        {
            string sSQL = "SELECT [IID],(Prefix+''+CONVERT(varchar,[QuotationNo])) as Quotation,[QuotationNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreatedDate],[Remark],[SalesRep],[company_id]  FROM [dbo].[Quotation_Info] Q where [company_id]='"+ compid +"' ORDER BY [QuotationNo] DESC";
            DataSet ds = GetDataSet(sSQL);
            RadGrid2.DataSource = ds;
            RadGrid2.DataBind();
        }


        void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("AddEditQuotation.aspx?Quotation=" + dataItem["QuotationNo"].Text);
            }
        }

        protected void RadGrid2_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
                RadGrid2.CurrentPageIndex = e.NewPageIndex;
                BindGrid();
        }

        void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sSQL = "SELECT [IID],(Prefix+''+CONVERT(varchar,[QuotationNo])) as Quotation,[QuotationNo],(select ClientName from clientdetails where ClientID=Q.[ClientId]) as Client,[CreatedDate],[Remark],[SalesRep],[company_id]  FROM [dbo].[Quotation_Info] Q where [company_id]='" + compid + "' ORDER BY [QuotationNo] DESC";
            DataSet ds = GetDataSet(sSQL);
            RadGrid2.DataSource = ds;
        }
        protected void RadGrid2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string QuotationNo = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["QuotationNo"]);

                string sSQL1 = "DELETE FROM [Quotation_Info] WHERE [QuotationNo] ='" + QuotationNo + "';DELETE FROM [QuoationMaster_hourly] WHERE [QuotationNo] ='" + QuotationNo + "';DELETE FROM [QuoationMaster_Monthly] WHERE [QuotationNo] ='" + QuotationNo + "'";

                int retVal = DataAccess.ExecuteStoreProc(sSQL1);

                if (retVal == 1)
                {
                    //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Addition type  is deleted."));

                }
                else
                {
                    //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the addition type."));
                }

           }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
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
    }
}
