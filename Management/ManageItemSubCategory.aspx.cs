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
    public partial class ManageItemSubCategory : System.Web.UI.Page
    {
        private object _dataItem = null;
        string strMessage = "";
        protected string sHeadingColor  = Constants.HEADING_COLOR;
        protected string sBorderColor   = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor  = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor   = Constants.ODD_ROW_COLOR;
        protected string sBaseColor     = Constants.BASE_COLOR;
        string _actionMessage = "";

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

            //SessionDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor ItemSubCategoryID = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemSubCategoryID");
        //        GridTextBoxColumnEditor ItemSubCategoryName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemSubCategoryName");

        //        ItemSubCategoryID.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemSubCategoryID('" + ItemSubCategoryID.TextBoxControl.ClientID + "')");
        //        ItemSubCategoryName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemSubCategoryName('" + ItemSubCategoryName.TextBoxControl.ClientID + "')");
        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Item Sub Category")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(*) CNT From Item Where ItemSubCatID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Sub Category which is in use."));
                        _actionMessage = "Warning|Unable to delete the Item Sub Category.This Item Sub Category is in use..";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [ItemSubCategory] WHERE [id] =" + id;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Item Sub Category is Deleted Successfully."));
                            _actionMessage = "success|Item Sub Category Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item Sub Category."));
                            _actionMessage = "Warning|Unable to delete the Item Sub Category";
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
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            ///////Added By Jammu Office//////////
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Parent_ItemCategoryID'"))
                    ErrMsg = "Please Select Item Category ID";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Item Sub Category added successfully.");
                _actionMessage = "Success|Item Sub Category added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            ///////Added By Jammu Office ends//////////
            //if (e.Exception != null)
            //{
            //    string ErrMsg = e.Exception.Message;
            //    e.ExceptionHandled = true;
            //    if (e.Exception.Message.Contains("IX_Sub_Project_ID"))
            //        ErrMsg = "Project ID already Exists";
            //    if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_Name'"))
            //        ErrMsg = "Please Enter Sub Project Name";
            //    if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_ID'"))
            //        ErrMsg = "Please Enter Sub Project ID";

            //    DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            //}
            //else
            //{
            //    DisplayMessage("Project added successfully.");
            //}
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    string ErrMsg = e.Exception.Message;
            //    e.ExceptionHandled = true;
            //    if (e.Exception.Message.Contains("IX_Sub_Project_ID"))
            //        ErrMsg = "Project ID already Exists";
            //    if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_Name'"))
            //        ErrMsg = "Please Enter Sub Project Name";
            //    if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'Sub_Project_ID'"))
            //        ErrMsg = "Please Enter Sub Project ID";

            //    DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            //}
            //else
            //{
            //    DisplayMessage("Project updated successfully.");
            //}
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of UNIQUE KEY"))
                    ErrMsg = "Item Sub Category already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Item Sub Category, Empty Item Sub Category can not be updated";
                //DisplayMessage("<font color = 'red'> Nationality can not be updated. Reason: " + ErrMsg + ".</font>");   
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Nationality updated successfully.");
                _actionMessage = "Success|Item Sub Category updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
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

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string strICID = Utility.ToString(((TextBox)editedItem["ItemSubCategoryID"].Controls[0]).Text.ToString().Trim().ToUpper());
                string strICName = Utility.ToString(((TextBox)editedItem["ItemSubCategoryName"].Controls[0]).Text.ToString().Trim().ToUpper());
                if (strICID.Length <= 0 || strICName.Length <= 0)
                {
                    if (strICID.Length <= 0)
                    {
                        strMessage = strMessage + "<br/>" + "Item Sub Category ID Cannot Remain Blank";
                    }
                    if (strICName.Length <= 0)
                    {
                        strMessage = strMessage + "<br/>" + "Item Sub Category Name Cannot Remain Blank";
                    }
                    if (strMessage.Length > 0)
                    {
                        ShowMessageBox(strMessage);
                        strMessage = "";
                    }
                    //RadGrid1.MasterTableView.IsItemInserted = true;
                    e.Canceled = true;
                }
                else
                {
                    int i = 0;
                    string sSqlCatID = "";
                    string sSqlCatName = "";

                    if (e.CommandName == "Update")
                    {
                        GridEditableItem editit = e.Item as GridEditableItem;
                        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["id"]);
                        sSqlCatID = "Select Count(ID) From ItemSubCategory Where Upper(ItemSubCategoryID)='" + strICID + "' And ID != " + id;
                        sSqlCatName = "Select Count(ID) From ItemSubCategory Where Upper(ItemSubCategoryName)='" + strICName + "' And ID != " + id;
                    }
                    else
                    {
                        sSqlCatID = "Select Count(ID) From ItemSubCategory Where Upper(ItemSubCategoryID)='" + strICID + "'";
                        sSqlCatName = "Select Count(ID) From ItemSubCategory Where Upper(ItemSubCategoryName)='" + strICName + "'";
                    }
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            i = 1;
                            strMessage = strMessage + "<br/>" + "Item Sub Category ID already exist either in current company/other Company or might be attached with other Item Category";
                        }
                    }
                    SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
                    if (drnew.Read())
                    {
                        if (Convert.ToInt16(drnew[0].ToString()) > 0)
                        {
                            i = 2;
                            strMessage = strMessage + "<br/>" + "Item Sub Category Name already exist either in current company/other Company or might be attached with other Item Category";
                        }
                    }
                    if (i >= 1)
                    {
                        //RadGrid1.MasterTableView.IsItemInserted = true;
                        e.Canceled = true;
                    }
                    else
                    {
                        if (e.CommandName == "Update")
                        {
                            strMessage = "Item Sub Category updated successfully.";
                        }
                        else
                        {
                            strMessage = "Item Sub Category added successfully.";
                        }
                    }
                    DisplayMessage(strMessage);
                    if (strMessage.Length > 0)
                    {
                        ShowMessageBox(strMessage);
                        strMessage = "";
                    }
                }
            }
        }
       
        //****************Created by Jammu Office
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
        protected void drpcat_DataBound(object sender, EventArgs e)
        {
            binddrpcat(sender);
        }
        protected void binddrpcat(object sender)
        {
            DropDownList _drpdrpcat = sender as DropDownList;
            if (_drpdrpcat.DataSource == null)
            {
                string sSQL;
                DataSet ds_cat = new DataSet();
                sSQL = "Select P.Location_ID,L.Location_Name From Project P Inner Join Location L On P.Location_ID = L.ID";
                ds_cat = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                _drpdrpcat.DataSource = ds_cat.Tables[0];
                _drpdrpcat.DataTextField = ds_cat.Tables[0].Columns["Location_Name"].ColumnName.ToString();
                _drpdrpcat.DataValueField = ds_cat.Tables[0].Columns["Location_ID"].ColumnName.ToString();
                _drpdrpcat.DataBind();
                object addition_type = DataBinder.Eval(DataItem, "id");
                if (addition_type != DBNull.Value)
                {
                    //drplocname.SelectedValue = addition_type.ToString();
                }
            }
        }
        //****************Created by Jammu office


    }
}
