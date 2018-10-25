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

namespace SMEPayroll.Invoice
{

    public partial class PaymentTerms : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
       
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource_VarPref.ConnectionString = Session["ConString"].ToString();
        
        }


        #region Grid2-preference


        protected void RadGrid2_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Ip"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from invoice_info where PaymentTerms='" + id + "' AND Company_Id='" + compID + "'", null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete .Payment Terms  is in use."));
                        _actionMessage = "Warning|Unable to delete .Payment Terms  is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [PaymentTerms] WHERE [ip] ='" + id + "'";

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            // RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Deleted Successfully."));
                            _actionMessage = "success|Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete."));
                            _actionMessage = "Warning|Unable to delete.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " ));
                _actionMessage = "Warning|Unable to delete record. Reason:" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid2_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                {
                    _actionMessage = "Warning|Payment Terms can not be added. Reason: Payment Terms already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> Payment Terms can not be added. Reason: Payment Terms already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|Payment Terms can not be added. Reason:"+ e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> Payment Terms can not be added. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                //DisplayMessage("Payment Terms added successfully.");
                _actionMessage = "Warning|Payment Terms added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid2_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                {
                    _actionMessage = "Warning|Payment Terms can not be updated. Reason: Payment Terms already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }

                //DisplayMessage("<font color = 'red'> Payment Terms can not be updated. Reason: Payment Terms already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|Payment Terms can not be updated. Reason:"+ e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> Payment Terms can not be updated. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                //DisplayMessage("Payment Terms updated successfully.");
                _actionMessage = "success|Payment Terms updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            //RadGrid2.Controls.Add(new LiteralControl(text));
        }


        #endregion
    }

 
}
