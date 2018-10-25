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
    public partial class ManageSupplier : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        string Module;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }

            Module = Convert.ToString(Request.QueryString["module"]);

            if (!Page.IsPostBack)
            {
            }
            //SessionDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor SupplierID = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("SupplierID");
                GridTextBoxColumnEditor SupplierName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("SupplierName");

                SupplierID.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_SupplierID('" + SupplierID.TextBoxControl.ClientID + "')");
                SupplierName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_SupplierName('" + SupplierName.TextBoxControl.ClientID + "')");
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Supplier")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Supplier. This Supplier is in use."));
                        _actionMessage = "Warning|Unable to delete the Supplier. This Supplier is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Supplier] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Supplier is Deleted Successfully."));
                            _actionMessage = "Success|Supplier is Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Supplier."));
                            _actionMessage = "Warning|Unable to delete the Supplier.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Some error occured. Try again later.";
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

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {

        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            //if (e.Exception != null)
            //{
            //    string ErrMsg = e.Exception.Message;
            //    e.KeepInEditMode = true;
            //    e.ExceptionHandled = true;
            //    DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            //}
            //else
            //{
            //    DisplayMessage("Item Category updated successfully.");
            //}
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string strICID = Utility.ToString(((TextBox)editedItem["SupplierID"].Controls[0]).Text.ToString().Trim().ToUpper());
                string strICName = Utility.ToString(((TextBox)editedItem["SupplierName"].Controls[0]).Text.ToString().Trim().ToUpper());
                if (strICID.Length <= 0 || strICName.Length <= 0)
                {
                    if (strICID.Length <= 0)
                    {
                        strMessage = strMessage + "<br/>" + "Supplier Code Cannot Remain Blank";
                    }
                    if (strICName.Length <= 0)
                    {
                        strMessage = strMessage + "<br/>" + "Supplier Name Cannot Remain Blank";
                    }
                    if (strMessage.Length > 0)
                    {
                        //ShowMessageBox(strMessage);
                        _actionMessage = "Warning|" + strMessage;
                        ViewState["actionMessage"] = _actionMessage;
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
                        sSqlCatID = "Select Count(ID) From Supplier Where Upper(SupplierID)='" + strICID + "' And ID != " + id;
                        sSqlCatName = "Select Count(ID) From Supplier Where Upper(SupplierName)='" + strICName + "' And ID != " + id;
                    }
                    else
                    {
                        sSqlCatID = "Select Count(ID) From Supplier Where Upper(SupplierID)='" + strICID + "'";
                        sSqlCatName = "Select Count(ID) From Supplier Where Upper(SupplierName)='" + strICName + "'";
                    }
                    //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
                    //if (dr.Read())
                    //{
                    //    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    //    {
                    //        i = 1;
                    //        strMessage = strMessage + "<br/>" + "Supplier Code already exist either in current company/other Company";
                    //    }
                    //}
                    //SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
                    //if (drnew.Read())
                    //{
                    //    if (Convert.ToInt16(drnew[0].ToString()) > 0)
                    //    {
                    //        i = 2;
                    //        strMessage = strMessage + "<br/>" + "Supplier Name already exist either in current company/other Company";
                    //    }
                    //}
                    //if (i >= 1)
                    //{
                    //    //RadGrid1.MasterTableView.IsItemInserted = true;
                    //    e.Canceled = true;
                    //}
                    //else
                    {
                        if (e.CommandName == "Update")
                        {
                            //strMessage = "Supplier Information updated successfully.";
                            _actionMessage = "Success|Supplier Information updated successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            //strMessage = "Supplier Information added successfully.";
                            _actionMessage = "Success|Supplier Information added successfully.";
                            ViewState["actionMessage"] = _actionMessage;
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
    }
}
