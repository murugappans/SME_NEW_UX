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
    public partial class StockReturn : System.Web.UI.Page
    {
        string strStore;
        int compid;
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
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
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            SqlDataSource7.ConnectionString = Session["ConString"].ToString();
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Stock Return")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (e.Item.OwnerTableView.IsItemInserted == false)
                {
                    strStore = e.Item.Cells[4].Text;
                }
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string ID = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(Emp_CODE) From Employee Where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Stock Return. This Stock Return is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [StockReturn] WHERE [ID] =" + ID;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Stock Return is Deleted Successfully."));

                        }
                        else
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Stock Return."));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
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
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
            }
        }

        protected void drpStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpStore = (DropDownList)sender;
            //GridDataItem dataItem = (GridDataItem)drpStore.NamingContainer;
            DropDownList drpItem = ((DropDownList)((GridEditFormItem)drpStore.NamingContainer)["drpItem"].Controls[0]);
            DataSet ds = new DataSet();
            ds = getDataSet("Select I.ID, (I.ItemName + '-[' + Convert(varchar,S.Qty) + ' Qty]') ItemName From Item  I Inner Join (Select ItemID, Sum(Quantity) Qty From StockIn Where StoreID=" + drpStore.SelectedItem.Value.ToString() + " And Company_ID=" + compid + " Group By ItemID) S On I.ID=S.ItemID Where I.Company_ID = " + compid);
            drpItem.Items.Clear();
            drpItem.Items.Insert(0, "-Select-");
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                //drpItem.Items.Insert(Convert.ToInt32(dataRow[0].ToString()), dataRow[1].ToString());
                drpItem.Items.Add(new ListItem(dataRow[1].ToString(),dataRow[0].ToString()));
            }
            //drpItem.Items.Insert(0, "-select-");
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
            GridEditableItem editedItem = e.Item as GridEditableItem;
            if (e.CommandName == "PerformInsert" || e.CommandName == "Update")
            {
                string strTranDate = "";
                string strItem = "";
                string strStore = "";
                if (((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate != null)
                {
                    strTranDate = ((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate.Value.ToShortDateString();
                }
                if (((DropDownList)editedItem["drpStore"].Controls[0]).SelectedItem != null)
                {
                    strStore = ((DropDownList)editedItem["drpStore"].Controls[0]).SelectedItem.Value.ToString();
                }
                if (((DropDownList)editedItem["drpItem"].Controls[0]).SelectedItem != null)
                {
                    strItem = ((DropDownList)editedItem["drpItem"].Controls[0]).SelectedItem.Value.ToString();
                }
                string strQty = ((RadNumericTextBox)editedItem["Quantity"].Controls[0]).Text.ToString();
                string strEmployee  = ((DropDownList)editedItem["drpEmployee"].Controls[0]).SelectedItem.Value.ToString();
                string strSubProject= ((DropDownList)editedItem["drpSubProject"].Controls[0]).SelectedItem.Value.ToString();
                string strRemarks   = ((TextBox)editedItem["Remarks"].Controls[0]).Text.ToString();

                if (strTranDate.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Transaction Date.";
                }
                if (strQty.ToString().Trim().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Quantity.";
                }
                if (strEmployee.ToString().Trim() == "0" && strSubProject.ToString().Trim() == "0")
                {
                    strMessage = strMessage + "<br/>" + "Please Select Either Employee or Sub Project.";
                }
                if (strEmployee.ToString().Trim() != "0" && strSubProject.ToString().Trim() != "0")
                {
                    strMessage = strMessage + "<br/>" + "You cannot Select Employee and Sub Project at the same time.";
                }
                if (strItem.ToString().Trim() == "0" || strItem.ToString().Trim() == "-Select-")
                {
                    strMessage = strMessage + "<br/>" + "Please Select Item.";
                }

                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    e.Canceled = true;
                }
                else
                {
                    if (e.CommandName == "PerformInsert")
                    {
                        e.Canceled = true;
                        editedItem.OwnerTableView.IsItemInserted = false;
                        string ssqlb = "Insert Into StockReturn (TransactionDate, Remarks, StoreID, ItemID, Quantity, Emp_ID, ProjectID, Company_ID) Values ('" + strTranDate + "','" + strRemarks + "'," + strStore + "," + strItem + "," + strQty + "," + strEmployee + "," + strSubProject + "," + compid + ")";
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        strMessage = "Stock Return Added successfully.";
                    }
                    else
                    {
                        string ID = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
                        string ssqlb = "Update StockReturn Set TransactionDate='" + strTranDate + "',Remarks='" + strRemarks + "',StoreID=" + strStore + ",ItemID=" + strItem + ",Quantity=" + strQty + ",Emp_ID=" + strEmployee + ",ProjectID=" + strSubProject + " Where ID=" + ID;
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                        e.Item.Edit = false;
                        strMessage = "Stock Return updated successfully.";
                    }
                    ShowMessageBox(strMessage);
                    strMessage = "";
                    RadGrid1.Rebind();
                }
            }
        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                GridEditFormItem Item = (GridEditFormItem)e.Item;
                DropDownList drpStore = (DropDownList)Item["drpStore"].Controls[0];
                drpStore.AutoPostBack = true;
                drpStore.SelectedIndexChanged += new EventHandler(drpStore_SelectedIndexChanged);

                if (strStore != null)
                {
                    DropDownList drpItem = ((DropDownList)((GridEditFormItem)drpStore.NamingContainer)["drpItem"].Controls[0]);
                    DataSet ds = new DataSet();
                    ds = getDataSet("Select I.ID, (I.ItemName + '-[' + Convert(varchar,S.Qty) + ' Qty]') ItemName From Item  I Inner Join (Select ItemID, Sum(Quantity) Qty From StockIn Where StoreID=" + strStore + " And Company_ID=" + compid + " Group By ItemID) S On I.ID=S.ItemID Where I.Company_ID = " + compid);
                    drpItem.Items.Clear();
                    drpItem.Items.Insert(0, new ListItem("--Select--", "0"));
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        drpItem.Items.Add(new ListItem(dataRow[1].ToString(), dataRow[0].ToString()));
                    }
                }
                else
                {
                    if (Item.OwnerTableView.IsItemInserted == true)
                    {
                        DropDownList drpItem = ((DropDownList)((GridEditFormItem)drpStore.NamingContainer)["drpItem"].Controls[0]);
                        drpItem.Items.Clear();
                        drpItem.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
        }
    }
}
