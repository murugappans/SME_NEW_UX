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
using System.IO;
using System.Text;
namespace SMEPayroll.Management
{
    public partial class ManageItemCategory : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int compid;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }
            if (!Page.IsPostBack)
            {
            }
            compid = Utility.ToInteger(Session["Compid"]);
            //SessionDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor ItemCategoryID = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemCategoryID");
        //        GridTextBoxColumnEditor ItemCategoryName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemCategoryName");
                
        //        ItemCategoryID.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemCategoryID('" + ItemCategoryID.TextBoxControl.ClientID + "')");
        //        ItemCategoryName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemCategoryName('" + ItemCategoryName.TextBoxControl.ClientID + "')");
        //    }
        //}

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Item Category")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From ItemSubCategory Where Parent_ItemCategoryID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Category. This Item Category is in use."));
                        _actionMessage = "Warning|Unable to delete the Item Category.This Item Category is in use..";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [ItemCategory] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Item Category is Deleted Successfully."));
                            _actionMessage = "success|Item Category Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Category."));
                            _actionMessage = "Warning|Unable to delete the Item Category.";
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
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
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
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            string strtxtName1 = ((TextBox)e.Item.FindControl("textbox1")).Text.ToString().Trim();
            string strtxtName2 = ((TextBox)e.Item.FindControl("textbox2")).Text.ToString().Trim();
            try
            {
                string sqlQry = "select * from ItemCategory where ItemCategoryID='" + strtxtName1.Trim() + "' or ItemCategoryName = '" + strtxtName2.Trim() + "' and Company_ID=" + compid + " ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Item Category ID/Item Category Name already exists.";
                    e.Canceled = true;
                }
                else
                {
                    string ssql = "INSERT INTO [ItemCategory] (Company_ID,ItemCategoryID,ItemCategoryName) VALUES (" + compid + ",'" + strtxtName1.Trim() + "','" + strtxtName2.Trim() + "')";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Item Category added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'ItemCategoryName'"))
                    ErrMsg = "Please Enter Item Category Name.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string strtxtName1 = ((TextBox)e.Item.FindControl("textbox1")).Text.ToString().Trim();
            string strtxtName2 = ((TextBox)e.Item.FindControl("textbox2")).Text.ToString().Trim();
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
            try
            {
                string sqlQry = "select * from ItemCategory where ItemCategoryID='" + strtxtName1.Trim() + "' or ItemCategoryName = '" + strtxtName2.Trim() + "' and Company_ID=" + compid + " ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Item Category ID/Item Category Name already exists.";
                    e.Canceled = true;
                }
                else
                {
                    string ssql = "UPDATE [ItemCategory] SET ItemCategoryID = '" + strtxtName1.Trim() + "',ItemCategoryName= '" + strtxtName2.Trim() + "' WHERE [id] = " + id + "";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Item Category Updated successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'ItemCategoryName'"))
                    ErrMsg = "Please Enter Item Category Name.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.MasterTableView.ClearEditItems();
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        //protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        string strICID = Utility.ToString(((TextBox)editedItem["ItemCategoryID"].Controls[0]).Text.ToString().Trim().ToUpper());
        //        string strICName = Utility.ToString(((TextBox)editedItem["ItemCategoryName"].Controls[0]).Text.ToString().Trim().ToUpper());
        //        if (strICID.Length <= 0 || strICName.Length <= 0)
        //        {
        //            if (strICID.Length <= 0)
        //            {
        //                strMessage = strMessage + "<br/>" + "Item Category ID Cannot Remain Blank";
        //            }
        //            if (strICName.Length <= 0)
        //            {
        //                strMessage = strMessage + "<br/>" + "Item Category Name Cannot Remain Blank";
        //            }
        //            if (strMessage.Length > 0)
        //            {
        //                ShowMessageBox(strMessage);
        //                strMessage = "";
        //            }
        //            //RadGrid1.MasterTableView.IsItemInserted = true;
        //            e.Canceled = true;
        //        }
        //        else
        //        {
        //            int i = 0;
        //            string sSqlCatID = "";
        //            string sSqlCatName = "";

        //            if (e.CommandName == "Update")
        //            {
        //                GridEditableItem editit = e.Item as GridEditableItem;
        //                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["id"]);
        //                sSqlCatID = "Select Count(ID) From ItemCategory Where Upper(ItemCategoryID)='" + strICID + "' And ID != " + id;
        //                sSqlCatName = "Select Count(ID) From ItemCategory Where Upper(ItemCategoryName)='" + strICName + "' And ID != " + id;
        //            }
        //            else
        //            {
        //                sSqlCatID = "Select Count(ID) From ItemCategory Where Upper(ItemCategoryID)='" + strICID + "'";
        //                sSqlCatName = "Select Count(ID) From ItemCategory Where Upper(ItemCategoryName)='" + strICName + "'";
        //            }
        //            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
        //            if (dr.Read())
        //            {
        //                if (Convert.ToInt16(dr[0].ToString()) > 0)
        //                {
        //                    i = 1;
        //                    strMessage = strMessage + "<br/>" + "Item Category ID already exist either in current company/other Company";
        //                }
        //            }
        //            SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
        //            if (drnew.Read())
        //            {
        //                if (Convert.ToInt16(drnew[0].ToString()) > 0)
        //                {
        //                    i = 2;
        //                    strMessage = strMessage + "<br/>" + "Item Category Name already exist either in current company/other Company";
        //                }
        //            }
        //            if (i >= 1)
        //            {
        //                //RadGrid1.MasterTableView.IsItemInserted = true;
        //                e.Canceled = true;
        //            }
        //            else
        //            {
        //                if (e.CommandName == "Update")
        //                {
        //                    strMessage = "Item Category updated successfully.";
        //                }
        //                else
        //                {
        //                    strMessage = "Item Category added successfully.";
        //                }
        //            }
        //            DisplayMessage(strMessage);
        //            if (strMessage.Length > 0)
        //            {
        //                ShowMessageBox(strMessage);
        //                strMessage = "";
        //            }
        //        }
        //    }
        //}
    }
}
