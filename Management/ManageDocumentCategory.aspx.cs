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
    public partial class ManageDocumentCategory : System.Web.UI.Page
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

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor Category_Name = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Category_Name");
        //        Category_Name.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Category_Name('" + Category_Name.TextBoxControl.ClientID + "')");
        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Document Category")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Company_ID"]);
                if (strSystemDefined == "-1")
                {
                    e.Item.Cells[6].Controls[0].Visible = false;
                    e.Item.Cells[7].Controls[0].Visible = false;
                }
            }
         }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Company_ID"]);

                if (strSystemDefined == "-1")
                {
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Document Category."));
                }
                else
                {
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From [DocumentMappedToEmployee] Where Category_ID =" + id, null);
                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Document Category. This Document Category is in use."));
                            _actionMessage = "Warning|Unable to delete the Document Category.This Document Category is in use.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            string sSQL = "DELETE FROM DocumentCategory WHERE [id] =" + id;

                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Document Category is Deleted Successfully."));
                                _actionMessage = "Success|Document Category Deleted Successfully.";
                                ViewState["actionMessage"] = _actionMessage;

                            }
                            else
                            {
                                // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Document Category."));
                                _actionMessage = "Warning|Unable to delete the Document Category.";
                                ViewState["actionMessage"] = _actionMessage;
                            }

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
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_CatName"))
                    ErrMsg = "Document Category Name already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Document Category Name";
                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
               // DisplayMessage("Document Category added successfully.");
                _actionMessage = "Success|Document Category added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_CatName"))
                    ErrMsg = "Document Category Name already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Document Category";
                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
               // DisplayMessage("Document Category updated successfully.");
                _actionMessage = "Success|Document Category updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
    }
}

