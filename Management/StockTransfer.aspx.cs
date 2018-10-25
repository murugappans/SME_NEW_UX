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
    public partial class StockTransfer : System.Web.UI.Page
    {
        string strStoreSource;
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
            
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            SqlDataSource7.ConnectionString = Session["ConString"].ToString();
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Stock Transfer")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (e.Item.OwnerTableView.IsItemInserted == false)
                {
                    strStoreSource = e.Item.Cells[4].Text;
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
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Stock Transfer. This Stock Transfer is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [StockTransfer] WHERE [ID] =" + ID;
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);
                        if (retVal == 1)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Stock Transfer is Deleted Successfully."));
                        }
                        else
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Stock Transfer."));
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
            DropDownList drpItem = ((DropDownList)((GridEditFormItem)drpStore.NamingContainer)["drpItem"].Controls[0]);
            drpItem.Items.Clear();
            DataSet ds;
            string sqlQuery = "sp_GetStockQuantity";
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@storeId", Utility.ToInteger(drpStore.SelectedItem.Value.ToString().Trim()));
            ds = DataAccess.ExecuteSPDataSet(sqlQuery, Param);
            int i = 0;
            foreach (DataRow dataRow in ds.Tables[0].Rows)
            {
                drpItem.Items.Insert(i, new ListItem(dataRow[3].ToString(), dataRow[1].ToString()));
                i++;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {

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
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string sSql = null;
                string strTranDate = "";
                string strItem = "";
                string strText = "";
                string strStoreSource = "";
                if (((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate != null)
                {
                    strTranDate = ((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate.Value.ToShortDateString();
                }
                if (((DropDownList)editedItem["drpStoreSource"].Controls[0]).SelectedItem != null)
                {
                    strStoreSource = ((DropDownList)editedItem["drpStoreSource"].Controls[0]).SelectedItem.Value.ToString();
                }
                string strStoreDestination = ((DropDownList)editedItem["drpStoreDestination"].Controls[0]).SelectedItem.Value.ToString();
                if (((DropDownList)editedItem["drpItem"].Controls[0]).SelectedItem != null)
                {
                    strItem = ((DropDownList)editedItem["drpItem"].Controls[0]).SelectedItem.Value.ToString();
                    strText = ((DropDownList)editedItem["drpItem"].Controls[0]).SelectedItem.Text.ToString();
                }
                string strQty = ((RadNumericTextBox)editedItem["Quantity"].Controls[0]).Text.ToString();
                string strRemarks = ((TextBox)editedItem["Remarks"].Controls[0]).Text.ToString();

                if (strTranDate.ToString().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Transaction Date.";
                }
                if (strQty.ToString().Trim().Length <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Please enter Quantity.";
                }

                if (strItem.ToString().Trim() == "0" || strItem.ToString().Trim() == "-Select-")
                {
                    strMessage = strMessage + "<br/>" + "Please Select Item.";
                }
                if (strQty.ToString().Trim().Length > 0)
                {
                    int itemQty = 0;

                    strText = strText.Remove(strText.Length - 5, 5);
                    string[] split = strText.Split('-');
                    itemQty = Utility.ToInteger(split[1]);
                    if (itemQty < Utility.ToInteger(strQty))
                    {
                        strMessage = strMessage + "<br/>" + "Invalid Item Quantity Entered.";
                    }
                }
                if (strStoreSource.ToString().Trim() == "0")
                {
                    strMessage = strMessage + "<br/>" + "Please Select Store Source.";
                }
                if (strStoreDestination.ToString().Trim() == "0")
                {
                    strMessage = strMessage + "<br/>" + "Please Select Store Destination.";
                }
                if (strStoreDestination.ToString().Trim() == strStoreSource.ToString().Trim())
                {
                    strMessage = strMessage + "<br/>" + "Store Soure And Store Destination Cannot be same.";
                }
                strTranDate = Convert.ToDateTime(strTranDate.ToString()).ToString("yyyy/MM/dd", format);
                sSql = "Select count(*) from BarcodeDetails B Inner join (select I.BarCodeID ,UpdatedDate,StockStatus,ItemCode,StoreId,ProjectId from ItemHistory I Inner join (select BarCodeId,Max(HistoryId)as HistoryId From ItemHistory group by BarcodeId)H on I.HistoryId=H.HistoryId)I On B.BarcodeId=I.BarcodeiD Where I.StockStatus in ('ESR','SI','PSR','ST') and B.StoreId='" + strStoreSource + "'and UPDATEDDATE <= '" + strTranDate + "'";
                int noOfRecs = DataAccess.ExecuteScalar(sSql, null);
                if (noOfRecs <= 0)
                {
                    strMessage = strMessage + "<br/>" + "Invalid Date Selected ,No Items Available in Selected Store ,Please Select Future Date";
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
                        string newTransDate;
                        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                        newTransDate = Convert.ToDateTime(strTranDate.ToString()).ToString("yyyy/MM/dd", culture); ;
                         sSql = "insert into TransactionMaster (TransDate,TransactionRemarks,TransType)Values('" + newTransDate + "','" + strRemarks + "',2)";
                        string ConnString = Constants.CONNECTION_STRING;
                        SqlConnection sqlcon = new SqlConnection(ConnString);
                        sqlcon.Open();
                        SqlTransaction stockTrans;
                        stockTrans = sqlcon.BeginTransaction();
                        SqlParameter[] cmdParams = null;
                        try
                        {
                            SqlCommand sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.Text;
                            sqlCmd.ExecuteScalar();

                            sSql = "Select @@identity";

                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.Text;
                            int mid = Utility.ToInteger(sqlCmd.ExecuteScalar());

                            sSql = "insert into stocktransferDetails(MasterTransId,ItemId,StoreIn,StoreOut,Quantity)Values(" + mid + "," + strItem.Trim() + "," + strStoreDestination.Trim() + "," + strStoreSource.Trim() + "," + strQty.Trim() + ")";
                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.Text;
                            Utility.ToInteger(sqlCmd.ExecuteScalar());

                            
                            sSql = "sp_updateNitemBarcode";
                            cmdParams = new SqlParameter[9];
                            cmdParams[0] = new SqlParameter("@qty", strQty.Trim());
                            cmdParams[1] = new SqlParameter("@rid", mid);
                            cmdParams[2] = new SqlParameter("@itId", strItem.Trim());
                            cmdParams[3] = new SqlParameter("@stId", strStoreSource.Trim());
                            cmdParams[4] = new SqlParameter("@type", 3);
                            cmdParams[5] = new SqlParameter("@newstId", strStoreDestination.Trim());
                            cmdParams[6] = new SqlParameter("@UpdatedDate", newTransDate.Trim());
                            cmdParams[7] = new SqlParameter("@EmployeeID", 0);
                            cmdParams[8] = new SqlParameter("@ProjectId", 0);
                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            foreach (SqlParameter param in cmdParams)
                                sqlCmd.Parameters.Add(param);
                            Utility.ToInteger(sqlCmd.ExecuteScalar());

                            strMessage = "Store Transfer Added successfully.";
                            stockTrans.Commit();
                        }
                        catch (Exception ex)
                        {
                            string sqlerr = ex.Message.ToString();
                            stockTrans.Rollback();
                        }
                    }
                    else
                    {

                        e.Item.Edit = false;

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
                DropDownList drpStore = (DropDownList)Item["drpStoreSource"].Controls[0];
                drpStore.AutoPostBack = true;
                drpStore.SelectedIndexChanged += new EventHandler(drpStore_SelectedIndexChanged);
                if (strStoreSource != null)
                {
                    DropDownList drpItem = ((DropDownList)((GridEditFormItem)drpStore.NamingContainer)["drpItem"].Controls[0]);
                    DataSet ds = new DataSet();
                    ds = getDataSet("Select I.ID, (I.ItemName + '-[' + Convert(varchar,S.Qty) + ' Qty]') ItemName From Item  I Inner Join (Select ItemID, Sum(Quantity) Qty From StockIn Where StoreID=" + strStoreSource + " And Company_ID=" + compid + " Group By ItemID) S On I.ID=S.ItemID Where I.Company_ID = " + compid);
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
