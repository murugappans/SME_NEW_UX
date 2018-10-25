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

namespace SMEPayroll.Cost
{
    public partial class OtherCostUC : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected int comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }

         protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(drpTrade_DataBinding1);
            comp_id = Utility.ToInteger(Session["Compid"]);
          
        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected void drpTrade_DataBinding1(object sender, EventArgs e)
        {
            #region Supplier dropdown
          
                string sSQL = "select ID,SupplierName from Supplier where Company_id='" + comp_id + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                drpSupplier.Items.Clear();
                drpSupplier.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

                while (dr.Read())
                {
                    drpSupplier.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
            #endregion

            #region Category dropdown
                 string sSQL_Cat = "select Cid,Category from Cost_Category where Company_id='" + comp_id + "'";
                 SqlDataReader dr_cat = DataAccess.ExecuteReader(CommandType.Text, sSQL_Cat, null);
                drpCategory.Items.Clear();
                drpCategory.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

                while (dr_cat.Read())
                {
                    drpCategory.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr_cat.GetValue(1)), Utility.ToString(dr_cat.GetValue(0))));
                }
            #endregion

            #region Project dropdown
                string sSQL_Pro = "select ID,Sub_Project_Name from SubProject where Parent_Project_ID in (select ID from Project where Company_ID='" + comp_id + "')";
                SqlDataReader dr_Pro = DataAccess.ExecuteReader(CommandType.Text, sSQL_Pro, null);
                drpProject.Items.Clear();
                drpProject.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));

                while (dr_Pro.Read())
                {
                    drpProject.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr_Pro.GetValue(1)), Utility.ToString(dr_Pro.GetValue(0))));
                }
            #endregion


             //Vendor InvoiceNo
             object VendorInvoiceNo = DataBinder.Eval(DataItem, "VendorInvoiceNo");
             txtVendorInvoiceNo.Text = Convert.ToString(VendorInvoiceNo);


             //Invoice Date
             object InvoiceDate = DataBinder.Eval(DataItem, "InvoiceDate");

             if (InvoiceDate.ToString()!="")
             {
                 datePicker_Invoice.SelectedDate = Convert.ToDateTime(InvoiceDate);
             }
            //supplier select item
             object Supplier = DataBinder.Eval(DataItem, "SupplierId");
             if (Convert.ToString(Supplier) != "")
             {
                 drpSupplier.SelectedValue = Convert.ToString(Supplier);
             }

            //Desc
             object Desc = DataBinder.Eval(DataItem, "Description");
             TxtDescr.Text = Convert.ToString(Desc);

            //Amount
             object Amount = DataBinder.Eval(DataItem, "Amount");
             TxtAmount.Text = Convert.ToString(Amount);

            //Cheque
             object ChequeNo = DataBinder.Eval(DataItem, "ChequeNo");
             TxtCheque.Text = Convert.ToString(ChequeNo);


             //Cheque
             object ChequeDate = DataBinder.Eval(DataItem, "ChequeDate");
             if (ChequeDate.ToString() != "")
             {
                 RadDatePicker_cheq.SelectedDate = Convert.ToDateTime(ChequeDate);
             }

            //project selected item
             object SubProjectID = DataBinder.Eval(DataItem, "SubProjectID");
             if (Convert.ToString(SubProjectID) != "")
             {
                 drpProject.SelectedValue = Convert.ToString(SubProjectID);
             }

            //Quotation No
             object QuotationNo = DataBinder.Eval(DataItem, "QuotationNo");
             TxtQuoation.Text = Convert.ToString(QuotationNo);


            //Internal Invoice
             object InternalInvoice = DataBinder.Eval(DataItem, "InternalInvoice");
             TxtIntInvoice.Text = Convert.ToString(InternalInvoice);

             //Category
             object Category = DataBinder.Eval(DataItem, "CategoryId");
             if (Convert.ToString(Category) != "")
             {
                 drpCategory.SelectedValue = Convert.ToString(Category);
             }

        }

    }
}