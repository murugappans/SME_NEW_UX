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
using System.Text;

namespace SMEPayroll.Cost
{

    public partial class OtherCostInput : System.Web.UI.Page
    {
        public int compid;

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                BindGrid();
            }

            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
            
        }

        public void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
         
            if (e.Item is GridDataItem)  
            {
                //supplier Name dropdown
                GridDataItem item = (GridDataItem)e.Item;
                DropDownList ddl = (DropDownList)item["Supplier"].FindControl("drpSupplier");
                ddl.DataSourceID = "SqlDataSource1";
                ddl.DataTextField = "SupplierName";
                ddl.DataValueField = "ID";
                ddl.DataBind();

               // ddl.SelectedValue = item["SupplierId"].Text;
                //

                //supplier Name dropdown
                DropDownList ddl1 = (DropDownList)item["ProjectName"].FindControl("drpProjectName");
                ddl1.DataSourceID = "SqlDataSource2";
                ddl1.DataTextField = "ProjectName";
                ddl1.DataValueField = "SubProjectID";
                ddl.DataBind();

                //ddl1.SelectedValue = item["SubProjectID"].Text;
                //

                //category
                DropDownList ddl2 = (DropDownList)item["Category"].FindControl("drpCategory");
                ddl2.DataSourceID = "SqlDataSource3";
                ddl2.DataTextField = "Category";
                ddl2.DataValueField = "CategoryId";
                ddl.DataBind();

               // ddl2.SelectedValue = item["CategoryId"].Text;
                //
            }

           


        }


      



        private void BindGrid()
        {
            //string sqlup = @"SELECT [CID],[InvoiceDate],(select SupplierName from Supplier where ID=CO.[SupplierId]) Supplier,SupplierId,[VendorInvoiceNo],[Description],[Amount],[ChequeNo],[ChequeDate],"
             //              + "(select Sub_Project_Name from SubProject where ID=CO.[SubProjectID]) ProjectName,SubProjectID,(select Location_name from Location where Company_Id=" + compid + " and ID=(select Location_Id from Project where ID=(select Parent_Project_ID from SubProject where ID=CO.[SubProjectID]))) Location,[QuotationNo],[InternalInvoice],(select Category from Cost_Category where Cid=CO.[CategoryId]) Category,CategoryId  FROM [Cost_Others] CO where [company_id]=" + compid + "";

            SqlParameter[] parms1 = new SqlParameter[1];
            parms1[0] = new SqlParameter("@Company ", compid);
            DataSet ds_ExportGrid = new DataSet();
            ds_ExportGrid = DataAccess.ExecuteSPDataSet("Sp_Cost_ManageOtherCost", parms1);

            RadGrid1.DataSource = ds_ExportGrid;
            RadGrid1.DataBind();
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }


        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                if (e.CommandName == "UpdateAll")
                {
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string Company_Id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Company_Id"));
                                RadDatePicker datePicker_Invoice = (RadDatePicker)dataItem.FindControl("datePicker_Invoice");
                                DateTime dt = Convert.ToDateTime(datePicker_Invoice.SelectedDate);
                                TextBox VendorInvoiceNo = (TextBox)dataItem.FindControl("txtVendorInvoiceNo");
                                DropDownList Supplier = (DropDownList)dataItem.FindControl("drpSupplier");
                                TextBox Amount = (TextBox)dataItem.FindControl("txtAmount");
                                TextBox ChequeNo = (TextBox)dataItem.FindControl("txtChequeNo");
                                RadDatePicker ChequeDate = (RadDatePicker)dataItem.FindControl("datePicker_ChequeDate");
                                DateTime dt1 = Convert.ToDateTime(ChequeDate.SelectedDate);
                                DropDownList ProjectName = (DropDownList)dataItem.FindControl("drpProjectName");
                                TextBox QuotationNo = (TextBox)dataItem.FindControl("txtQuotationNo");
                                TextBox InternaInvoice = (TextBox)dataItem.FindControl("txtInternaInvoice");
                                DropDownList drpCategory = (DropDownList)dataItem.FindControl("drpCategory");

                                string ssqlb = "INSERT INTO [dbo].[Cost_Others]([InvoiceDate],[SupplierId],[VendorInvoiceNo],[Amount],[ChequeNo],[ChequeDate],[SubProjectID],[QuotationNo],[InternalInvoice],[CategoryId],[Company_Id],[Description])"
                                + "VALUES ('" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "','" + Convert.ToInt32(Supplier.SelectedValue) + "' ,'" + VendorInvoiceNo.Text + "' ,'" + Amount.Text + "' ,'" + ChequeNo.Text + "' ,'" + dt1.Date.Month + "/" + dt1.Date.Day + "/" + dt1.Date.Year + "' ,'" + ProjectName.SelectedValue + "'  ,'" + QuotationNo.Text + "' ,'" + InternaInvoice.Text + "','" + drpCategory.SelectedValue + "' ,'" + Company_Id + "' ,'')";

                                DataAccess.FetchRS(CommandType.Text, ssqlb, null);

                            }
                        }
                    }
                    //ShowMessageBox("Updated Sucessfully");
                    _actionMessage = "success|Updated Sucessfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }


        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }



    }


}
