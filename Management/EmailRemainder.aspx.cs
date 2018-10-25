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

namespace SMEPayroll.Management
{
    public partial class EmailRemainder : System.Web.UI.Page
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
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor Designation = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Emails");
                Designation.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Emails('" + Designation.TextBoxControl.ClientID + "')");
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Designation Types")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                // SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where desig_id=" + id, null);
                // if (dr.Read())
                // {
                //  if (dr[0].ToString() != "0" || id == "13") // ID == 13 CONDITION IS ONLY FOR COFFEESHOP
                //  {
                //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the designation.This designation is in use."));
                //  }
                //  else
                //  {
                string sSQL = "DELETE FROM [EmailRemainder] WHERE [id] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>  Deleted Successfully."));
                    _actionMessage = "Success|Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;

                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete "));
                    _actionMessage = "Warning|Unable to delete ";
                    ViewState["actionMessage"] = _actionMessage;
                }

                // }
                //}

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
//ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                {
                    _actionMessage = "Warning|can not be added. Reason: Job Category already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> can not be added. Reason: Job Category already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|can not be added. Reason:" + e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> can not be added. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                //DisplayMessage("added successfully.");
                _actionMessage = "Warning|added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                {
                    _actionMessage = "Warning|can not be updated. Reason: Job Category already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> can not be updated. Reason: Job Category already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|can not be updated. Reason: "+ e.Exception.Message ;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> can not be updated. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                DisplayMessage("updated successfully.");
                _actionMessage = "sc|updated successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

    }
}
