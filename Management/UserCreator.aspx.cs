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
    public partial class UserCreator : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
            //RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
        }

        //Event to hide the password 
        //void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
        //    {
        //        GridEditFormItem edititem = (GridEditFormItem)e.Item;
        //        TextBox txtpwd = (TextBox)edititem["Password"].Controls[0];
        //        txtpwd.TextMode = TextBoxMode.Password;
        //        txtpwd.Visible = true;
        //    }

        //}

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{

        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor editor_Category_Name = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Category_Name");
        //        editor_Category_Name.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_CategoryName('" + editor_Category_Name.TextBoxControl.ClientID + "')");
        //    }
        //}


        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("PK_UserName"))
                    ErrMsg = "User  already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter User Name";
                // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                ViewState["actionMessage"] = "Warning|"+ErrMsg;

            }
            else
            {
                //DisplayMessage("User added successfully.");
                ViewState["actionMessage"] = "Success|User added successfully.";

            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("PK_UserName"))
                    ErrMsg = "User  already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter User Name";
                // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                ViewState["actionMessage"] = "Warning|" + ErrMsg;

            }
            else
            {
                // DisplayMessage("User updated successfully.");
                ViewState["actionMessage"] = "Success|User updated successfully." ;

            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                GridDataItem item = (GridDataItem)e.Item;
                string value = item["Uid"].Text;
                string strSQL = "";
                strSQL = "Delete from Users WHERE [Uid] = " + int.Parse(value) + " ";
                int check = DataAccess.ExecuteNonQuery(strSQL, null);

                if (check == 1)
                {
                    //DisplayMessage("User deleted successfully.");
                    ViewState["actionMessage"] = "Success|User deleted successfully.";

                }

            }
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;


            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                //if (item.GetDataKeyValue("Uid").ToString() == "0")  //set your condition for hiding the row
                //{
                //    item.Display = false;  //hide the row
                //}
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

    }
}
