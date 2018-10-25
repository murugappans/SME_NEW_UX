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
    public partial class MECCategory : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;
        string _actionMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            ViewState["actionMessage"] = "";
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor ApCategory = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ApCategory");
        //        ApCategory.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Trade('" + ApCategory.TextBoxControl.ClientID + "')");
        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           // if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Trade Types")) == false)
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
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["APCatId"]);


                string sSQL = "DELETE FROM [APCategory] WHERE [APCatId] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Category is Deleted Successfully."));
                    ViewState["actionMessage"] = "Success|Category is Deleted Successfully.";

                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Category."));
                    ViewState["actionMessage"] = "Warning|Unable to delete the Category.";
                }
           

            }
            catch (Exception ex)
            {
                string ErrMsg ="Some error occured." ;
                if (ex.Message.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                ViewState["actionMessage"] = "Warning|"+ErrMsg;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            try
            {
                string sqlQry = "select * from APCategory where ApCategory='" + strtxtName.Trim() + "' ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Category already exists.";
                    e.Canceled = true;
                }
                else
                {
                    string ssql = "INSERT INTO [APCategory] ([ApCategory],companyid) VALUES ('" + strtxtName.Trim() + "'," + compID + ")";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Category added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'ApCategory'"))
                    ErrMsg = "Please Enter Category.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["APCatId"]);
            try
            {
                string sqlQry = "select * from APCategory where ApCategory='" + strtxtName.Trim() + "' ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Category already exists.";
                    e.Canceled = true;

                }
                else
                {
                    string ssql = "UPDATE [APCategory] SET [ApCategory] = '" + strtxtName.Trim() + "' WHERE [APCatId] = " + id + "";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Category Updated successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'ApCategory'"))
                    ErrMsg = "Please Enter Category.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
    }




}
