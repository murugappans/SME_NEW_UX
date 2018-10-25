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

namespace SMEPayroll.Cost
{
    public partial class Region : System.Web.UI.Page
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
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            compID = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor BusinessUnit = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("BusinessUnit");
        //        BusinessUnit.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_BusinessUnit('" + BusinessUnit.TextBoxControl.ClientID + "')");
        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Trade Types")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            //}
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Bid"]);

                string sSQL = "DELETE FROM [Cost_Region] WHERE [Bid] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                  //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Region is Deleted Successfully."));
                    _actionMessage = "success|Region Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;

                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Region."));
                    _actionMessage = "Warning|Unable to delete the Region.";
                    ViewState["actionMessage"] = _actionMessage;

                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
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
                    _actionMessage = "Warning|Region can not be added. Reason: Region already Exists";
                    ViewState["actionMessage"] = _actionMessage;
                }
                   // DisplayMessage("<font color = 'red'> Region can not be added. Reason: Region already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|Region can not be added. Reason: " + e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                  //  DisplayMessage("<font color = 'red'> Region can not be added. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
               // DisplayMessage("Region added successfully.");
                _actionMessage = "Success|Region added successfully" ;
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
                    _actionMessage = "Warning|Region can not be updated. Reason: Region already Exists";
                    ViewState["actionMessage"] = _actionMessage;
                }

                // DisplayMessage("<font color = 'red'> Region can not be updated. Reason: Region already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|Region can not be updated. Reason: " + e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
              //  DisplayMessage("<font color = 'red'> Region can not be updated. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
               // DisplayMessage("Region updated successfully.");
                _actionMessage = "Success|Region updated successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
            _actionMessage = "Warning|"+text;
            ViewState["actionMessage"] = _actionMessage;

        }
    }


}
