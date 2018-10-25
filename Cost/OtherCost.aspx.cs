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
using System.Net.Mail;
using System.IO;
using System.Text;

namespace SMEPayroll.Cost
{
    public partial class OtherCost : System.Web.UI.Page
    {
  
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        public int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
        }

        string VendorInvoiceNo, Supplier, Descr, Amount, Cheque, Project, Quoation, InternalInvoice, Category;
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                DateTime InvoiceDate =(DateTime) (userControl.FindControl("datePicker_Invoice") as RadDatePicker).SelectedDate;

                if ((userControl.FindControl("txtVendorInvoiceNo") as TextBox).Text != null)
                {
                    VendorInvoiceNo = Convert.ToString((userControl.FindControl("txtVendorInvoiceNo") as TextBox).Text);
                }

                Supplier = (userControl.FindControl("drpSupplier") as DropDownList).SelectedValue.ToString();

                if ((userControl.FindControl("TxtDescr") as TextBox).Text != null)
                {
                     Descr = Convert.ToString((userControl.FindControl("TxtDescr") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtAmount") as TextBox).Text != null)
                {
                     Amount = Convert.ToString((userControl.FindControl("TxtAmount") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtCheque") as TextBox).Text != null)
                {
                     Cheque = Convert.ToString((userControl.FindControl("TxtCheque") as TextBox).Text);
                }

                DateTime ChequeDate = (DateTime)(userControl.FindControl("RadDatePicker_cheq") as RadDatePicker).SelectedDate;

                 Project = (userControl.FindControl("drpProject") as DropDownList).SelectedValue.ToString();

                if ((userControl.FindControl("TxtQuoation") as TextBox).Text != null)
                {
                     Quoation = Convert.ToString((userControl.FindControl("TxtQuoation") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtIntInvoice") as TextBox).Text != null)
                {
                     InternalInvoice = Convert.ToString((userControl.FindControl("TxtIntInvoice") as TextBox).Text);
                }

                 Category = (userControl.FindControl("drpCategory") as DropDownList).SelectedValue.ToString();


                string ssqlb = "INSERT INTO [Cost_Others]([InvoiceDate],[SupplierId],[VendorInvoiceNo],[Amount],[ChequeNo],[ChequeDate],[SubProjectID],[QuotationNo],[InternalInvoice],[CategoryId],[Company_Id],[Description]) VALUES"
                                          + "( '" + InvoiceDate.Date.Month + "/" + InvoiceDate.Date.Day + "/" + InvoiceDate.Date.Year + "' ,'" + Supplier + "','" + VendorInvoiceNo + "','" + Amount + "','" + Cheque + "','" + ChequeDate.Date.Month + "/" + ChequeDate.Date.Day + "/" + ChequeDate.Date.Year + "','" + Project + "','" + Quoation + "','" + InternalInvoice + "','" + Category + "','" + compid + "','" + Descr + "')";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Inserted Sucessfully</font> "));
                _actionMessage = "success|Inserted Successfully";
                ViewState["actionMessage"] = _actionMessage;


            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Insert record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

                DateTime InvoiceDate = (DateTime)(userControl.FindControl("datePicker_Invoice") as RadDatePicker).SelectedDate;

                if ((userControl.FindControl("txtVendorInvoiceNo") as TextBox).Text != null)
                {
                    VendorInvoiceNo = Convert.ToString((userControl.FindControl("txtVendorInvoiceNo") as TextBox).Text);
                }

                Supplier = (userControl.FindControl("drpSupplier") as DropDownList).SelectedValue.ToString();

                if ((userControl.FindControl("TxtDescr") as TextBox).Text != null)
                {
                    Descr = Convert.ToString((userControl.FindControl("TxtDescr") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtAmount") as TextBox).Text != null)
                {
                    Amount = Convert.ToString((userControl.FindControl("TxtAmount") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtCheque") as TextBox).Text != null)
                {
                    Cheque = Convert.ToString((userControl.FindControl("TxtCheque") as TextBox).Text);
                }

                DateTime ChequeDate = (DateTime)(userControl.FindControl("RadDatePicker_cheq") as RadDatePicker).SelectedDate;

                Project = (userControl.FindControl("drpProject") as DropDownList).SelectedValue.ToString();

                if ((userControl.FindControl("TxtQuoation") as TextBox).Text != null)
                {
                    Quoation = Convert.ToString((userControl.FindControl("TxtQuoation") as TextBox).Text);
                }

                if ((userControl.FindControl("TxtIntInvoice") as TextBox).Text != null)
                {
                    InternalInvoice = Convert.ToString((userControl.FindControl("TxtIntInvoice") as TextBox).Text);
                }

                Category = (userControl.FindControl("drpCategory") as DropDownList).SelectedValue.ToString();


                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CID"];
                int cid = Convert.ToInt32(id);


                string ssql_Update = "UPDATE [Cost_Others]SET [InvoiceDate] = '" + InvoiceDate.Date.Month + "/" + InvoiceDate.Date.Day + "/" + InvoiceDate.Date.Year + "',[SupplierId] = '" + Supplier + "',[VendorInvoiceNo] = '" + VendorInvoiceNo + "',[Amount] ='" + Amount + "',[ChequeNo] = '" + Cheque + "',[ChequeDate] = '" + ChequeDate.Date.Month + "/" + ChequeDate.Date.Day + "/" + ChequeDate.Date.Year + "' ,[SubProjectID] = '" + Project + "',[QuotationNo] ='" + Quoation + "',[InternalInvoice] ='" + InternalInvoice + "' ,[CategoryId] = '" + Category + "',[Description] = '" + Descr + "' WHERE  [company_id]='" + compid + "' AND CID='" + cid + "' ";
                DataAccess.FetchRS(CommandType.Text, ssql_Update, null);
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Updated Sucessfully</font> "));
                _actionMessage = "success|Updated Successfully";
                ViewState["actionMessage"] = _actionMessage;

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Update record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }


        }


        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string QuotNo = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CID"]);

                string sSQL = "DELETE FROM Cost_Others where [company_id]='" + compid + "' AND CID = {0}";
                sSQL = string.Format(sSQL, Utility.ToInteger(QuotNo));
                int i = DataAccess.ExecuteStoreProc(sSQL);
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Deleted Sucessfully</font> "));
                _actionMessage = "success|Deleted Successfully";
                ViewState["actionMessage"] = _actionMessage;


            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

      
    }


}
