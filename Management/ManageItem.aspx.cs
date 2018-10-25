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
    public partial class ManageItem  : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor  = Constants.HEADING_COLOR;
        protected string sBorderColor   = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor  = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor   = Constants.ODD_ROW_COLOR;
        protected string sBaseColor     = Constants.BASE_COLOR;
        int compid;
        string _actionMessage = "";

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                RadGrid1.DataSource = GetDataTable("Select * From (Select U.ID UOM, I.ID IDParent, I.ItemID, I.ItemName, I.Company_ID   From Item I Inner Join Unit U  On I.UOM = U.ID) D Where D.Company_ID=" + compid + " Or D.Company_ID=-1");
            }
        }
        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Parameters":
                    {
                        string ID = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["IDParent"].ToString();
                        DataSet ds = new DataSet();
                        string sSQL = "Select ID, ItemID IDChild,ParameterID, ParameterVar From ItemParameter Where ItemID = '" + ID + "'";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                        e.DetailTableView.DataSource = ds.Tables[0];
                        break;
                    }
            }
        }

        public DataTable GetDataTable(string query)
        {
            String ConnString = Session["ConString"].ToString();
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, conn);

            DataTable myDataTable = new DataTable();

            conn.Open();
            try
            {
                adapter.Fill(myDataTable);
            }
            finally
            {
                conn.Close();
            }

            return myDataTable;
        }

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
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor ItemID = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemID");
        //        GridTextBoxColumnEditor ItemName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("ItemName");
                
        //        ItemID.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemID('" + ItemID.TextBoxControl.ClientID + "')");
        //        ItemName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_ItemName('" + ItemName.TextBoxControl.ClientID + "')");
                

        //    }
        //}
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Item")) == false)
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
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string IDParent = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["IDParent"]);
                string IDChild = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
                string strmsg = "";
                SqlDataReader dr;
                if (IDParent != "")
                {
                    strmsg = "Item";
                    sSQL = "DELETE FROM [Item] WHERE [id] =" + IDParent;
                    dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From ItemParameter Where ItemID=" + IDParent, null);
                }
                else
                {
                    strmsg = "Item Parameter";
                    sSQL = "DELETE FROM [ItemParameter] WHERE [id] =" + IDChild;
                    dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                }


                if (e.CommandName == "Delete")
                {

                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " which is in use."));
                            _actionMessage = "Warning|Unable to delete the " + strmsg + " which is in use.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> " + strmsg + " is Deleted Successfully."));
                                _actionMessage = "success|" + strmsg + " Deleted Successfully.";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " ."));
                                _actionMessage = "Warning|Unable to delete the " + strmsg + ".";
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
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Item_Code"))
                    ErrMsg = "Item Code already Exists";
                if (e.Exception.Message.Contains("IX_Item_Name"))
                    ErrMsg = "Item Name already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'ItemID'"))
                    ErrMsg = "Please Enter Item Code";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'ItemName'"))
                    ErrMsg = "Please Enter Item Name";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage(strMessage);
                //if (strMessage.Length > 0)
                //{
                //    ShowMessageBox(strMessage);
                //    strMessage = "";
                //}
                _actionMessage = "Success|Item added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
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
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_Item_Code"))
                    ErrMsg = "Item Code already Exists";
                if (e.Exception.Message.Contains("IX_Item_Name"))
                    ErrMsg = "Item Name already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'ItemID'"))
                    ErrMsg = "Please Enter Item Code";
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'ItemName'"))
                    ErrMsg = "Please Enter Item Name";

                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                _actionMessage = "Success|Item Updated successfully.";
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

        //protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        //{
        //    int i = 0;
        //    string sSqlCatID = "";
        //    string sSqlCatName = "";
        //    string sSqlID = "";

        //    if (e.CommandName == "Update")
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        if (editedItem.KeyValues.Contains("IDParent") == true)
        //        {
        //            string strICID = Utility.ToString(((TextBox)editedItem["ItemID"].Controls[0]).Text.ToString().Trim().ToUpper());
        //            string strICName = Utility.ToString(((TextBox)editedItem["ItemName"].Controls[0]).Text.ToString().Trim().ToUpper());
        //            if (strICID.Length <= 0 || strICName.Length <= 0)
        //            {
        //                if (strICID.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Item Code Cannot Remain Blank";
        //                }
        //                if (strICName.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Item Name Cannot Remain Blank";
        //                }
        //                //if (strMessage.Length > 0)
        //                //{
        //                //    ShowMessageBox(strMessage);
        //                //    strMessage = "";
        //                //}
        //                e.Canceled = true;
        //            }
        //            else
        //            {
        //                if (e.CommandName == "Update")
        //                {
        //                    GridEditableItem editit = e.Item as GridEditableItem;
        //                    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["IDParent"]);
        //                    sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "' And ID != " + id;
        //                    sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "' And ID != " + id;
        //                }
        //                else
        //                {
        //                    sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "'";
        //                    sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "'";
        //                }
        //                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
        //                if (dr.Read())
        //                {
        //                    if (Convert.ToInt16(dr[0].ToString()) > 0)
        //                    {
        //                        i = 1;
        //                        strMessage = strMessage + "<br/>" + "Item Code already exist either in current company/other Company";
        //                    }
        //                }
        //                SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
        //                if (drnew.Read())
        //                {
        //                    if (Convert.ToInt16(drnew[0].ToString()) > 0)
        //                    {
        //                        i = 2;
        //                        strMessage = strMessage + "<br/>" + "Item Name already exist either in current company/other Company";
        //                    }
        //                }
        //                if (i >= 1)
        //                {
        //                    //if (strMessage.Length > 0)
        //                    //{
        //                    //    ShowMessageBox(strMessage);
        //                    //    strMessage = "";
        //                    //} 
        //                    e.Canceled = true;
        //                }
        //                else
        //                {
        //                    strMessage = "Item updated successfully.";
        //                }
        //            }
        //        }
        //        if (editedItem.KeyValues.Contains("IDChild") == true)
        //        {
        //            string strParID = Utility.ToString(((RadComboBox)editedItem["ParameterID"].Controls[0]).SelectedValue.ToString());
        //            string strParName = Utility.ToString(((TextBox)editedItem["ParameterVar"].Controls[0]).Text.ToString().Trim().ToUpper());
        //            if (strParID.Length <= 0 || strParName.Length <= 0)
        //            {
        //                if (strParID.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Parameter for Item Cannot Remain Blank";
        //                }
        //                if (strParName.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Parameter Remarks for Item Cannot Remain Blank";
        //                }
        //                //if (strMessage.Length > 0)
        //                //{
        //                //    ShowMessageBox(strMessage);
        //                //    strMessage = "";
        //                //}
        //                e.Canceled = true;
        //            }
        //            else
        //            {
        //                string IDChild = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["IDChild"]);
        //                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
        //                sSqlID = "Select Count(ID) From ItemParameter Where ParameterID=" + strParID + " And ID != " + id + " And ItemID =" + IDChild;
        //                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
        //                if (dr.Read())
        //                {
        //                    if (Convert.ToInt16(dr[0].ToString()) > 0)
        //                    {
        //                        i = 1;
        //                        strMessage = strMessage + "<br/>" + "Item Parameter already exist with current Item";
        //                    }
        //                }
        //                if (i >= 1)
        //                {
        //                    //if (strMessage.Length > 0)
        //                    //{
        //                    //    ShowMessageBox(strMessage);
        //                    //    strMessage = "";
        //                    //} 
        //                    e.Canceled = true;
        //                }
        //                else
        //                {
        //                    string sSql = "Update ItemParameter Set ParameterID=" + strParID + ", ParameterVar='" + strParName + "' Where ID=" + id;
        //                    SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSql, null);
        //                    strMessage = "Item updated successfully.";
        //                }
        //            }
        //        }
        //    }

        //    if (e.CommandName == "PerformInsert")
        //    {
        //        if (e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl01" || e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl02")
        //        {
        //            strMessage = "Item added successfully.";
        //        }
        //        else
        //        {
        //            string ID = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["IDParent"].ToString();
        //            GridEditFormInsertItem editedItem = e.Item as GridEditFormInsertItem;

        //            string strParID = Utility.ToString(((RadComboBox)editedItem["ParameterID"].Controls[0]).SelectedValue.ToString());
        //            string strParName = Utility.ToString(((TextBox)editedItem["ParameterVar"].Controls[0]).Text.ToString().Trim().ToUpper());
        //            if (strParID.Length <= 0 || strParName.Length <= 0)
        //            {
        //                if (strParID.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Parameter for Item Cannot Remain Blank";
        //                }
        //                if (strParName.Length <= 0)
        //                {
        //                    strMessage = strMessage + "<br/>" + "Parameter Remarks for Item Cannot Remain Blank";
        //                }
        //                //if (strMessage.Length > 0)
        //                //{
        //                //    ShowMessageBox(strMessage);
        //                //    strMessage = "";
        //                //}
        //                e.Canceled = true;
        //            }
        //            else
        //            {
        //                sSqlID = "Select Count(ID) From ItemParameter Where ParameterID=" + strParID + " And ItemID =" + ID;
        //                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
        //                if (dr.Read())
        //                {
        //                    if (Convert.ToInt16(dr[0].ToString()) > 0)
        //                    {
        //                        i = 1;
        //                        strMessage = strMessage + "<br/>" + "Item Parameter already exist with current Item";
        //                    }
        //                }
        //                if (i >= 1)
        //                {
        //                    //if (strMessage.Length > 0)
        //                    //{
        //                    //    ShowMessageBox(strMessage);
        //                    //    strMessage = "";
        //                    //} 
        //                    e.Canceled = true;
        //                }
        //                else
        //                {
        //                    string sSql = "Insert Into ItemParameter (ItemID, ParameterID, ParameterVar) Values (" + ID + "," + strParID + ",'" + strParName + "')";
        //                    SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSql, null);
        //                    strMessage = "Item Parameter added successfully.";
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
