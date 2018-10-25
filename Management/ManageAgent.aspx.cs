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
    public partial class ManageAgent : System.Web.UI.Page
    {
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
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated); 
          
        }

        void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode )
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor editor_AgentName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Agent_Name");
        //        GridTextBoxColumnEditor editor_Phone1 = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Phone1");
        //        GridTextBoxColumnEditor editor_Phone2 = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Phone2");
        //        //GridTextBoxColumnEditor editor_Address = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Address");

        //        editor_AgentName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_AGentName('" + editor_AgentName.TextBoxControl.ClientID + "')");
        //        editor_Phone1.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Phone1('" + editor_Phone1.TextBoxControl.ClientID + "')");
        //        editor_Phone2.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Phone2('" + editor_Phone2.TextBoxControl.ClientID + "')");
        //        //editor_Address.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_AGentName('" + editor.TextBoxControl.ClientID + "')");

        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Agent")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_Code) From Employee Where Agent_ID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Agent. This Agent is in use."));
                        _actionMessage = "Warning|Unable to delete the Agent. This Agent is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeAgent] WHERE [id] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                           // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Agent is Deleted Successfully."));
                            _actionMessage = "Success|Agent Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Agent."));
                            _actionMessage = "Warning|Unable to delete the Agent.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }
        protected void RadGrid1_ItemInserted(object source, Telerik.Web.UI.GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_AgentName"))
                    ErrMsg = "Agent already Exists";         
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Agent Name";
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            }
            else
            {
               // DisplayMessage("Agent added successfully.");
                _actionMessage = "Success|Agent added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_AgentName"))
                    ErrMsg = "Agent already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Agent";
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            }
            else
            {
                //DisplayMessage("Agent updated successfully.");
                _actionMessage = "Success|Agent updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
    }
}

