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
    public partial class StockInNew : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                RadGrid1.DataSource = GetDataTable("select TransId,TransDate,OrderNumber,TransactionRemarks,StoreId,SupplierId from TransactionMaster");
            }
        }
        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Parameters":
                    {
                        string ID = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["TransId"].ToString();
                        DataSet ds = new DataSet();
                        string sSQL = "select ChildTransID,TransSubId,ItemCode,Quantity,Price from Transactiondetails where TransSubId='" + ID + "'";
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
            //    SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            //    SqlDataSource6.ConnectionString = Session["ConString"].ToString();
        }
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
                string IDParent = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["TransId"]);
                string IdChildId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["TransSubId"]);
                string ItemCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ItemCode"]);
                string IdChildTransId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ChildTransID"]);
                string strmsg = "";

                SqlDataReader dr = null;
                if (IDParent != "")
                {

                    sSQL = "Delete from TransactionMaster Where TransId='" + IDParent + "'";
                    dr = DataAccess.ExecuteReader(CommandType.Text, " Select count(*) from Transactiondetails Where TransSubId=" + IDParent, null);
                }
               
                if (e.CommandName == "Delete" && IDParent != "")
                {

                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " which is in use."));
                        }
                        else
                        {
                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                DisplayMessage(strmsg);
                                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> " + strmsg + " Order is Deleted Successfully."));

                            }
                            else
                            {
                                DisplayMessage(strmsg);
                                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " ."));
                            }

                        }
                    }

                }
                else
                {
                    SqlParameter[] parms = new SqlParameter[1];
                    sSQL = "select Barcodeid from ItemHistory Where RecentSubTransId= @IdChildTransId";
                    parms[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                    string BcodeIds = "0";
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, parms);
                    while (dr.Read())
                    {
                        BcodeIds = BcodeIds + "," + dr["Barcodeid"].ToString();
                    }

                    sSQL = "SELECT count(*)As NoOf FROM ItemHistory Where BarcOdeId in (" + BcodeIds + ") and StockStatus in ('ESO','PSO','ESS','PSS','ST')";
                   
                    int noOfRecords = DataAccess.ExecuteScalar(sSQL, null);
                    if (noOfRecords == 0)
                    {
                        sSQL = "sp_DeleteItemsFromOrder";
                        parms = new SqlParameter[3];
                        parms[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                        parms[1] = new SqlParameter("@ItemCode", ItemCode);
                        parms[2] = new SqlParameter("@RecId", IdChildId);
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal > 0)
                        {
                            DisplayMessage(strmsg);
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>  Item is Deleted Successfully."));
                        }
                        else
                        {
                            DisplayMessage(strmsg);
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " ."));
                        }
                    }
                    else {
                        DisplayMessage(strmsg);
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Can Not Delete This Item,Item Is In Use ."));
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete Order. Reason:</font> " + ErrMsg));
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

                DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
            }
            else
            {
                DisplayMessage(strMessage);
                if (strMessage.Length > 0)
                {
                    ShowMessageBox(strMessage);
                    strMessage = "";
                }
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
            int i = 0;
            string sSqlCatID = "";
            string sSqlCatName = "";
            string sSqlID = "";
            string strTransDate = null;
            IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
            if (e.CommandName == "Print")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditableItem editit = e.Item as GridEditableItem;

                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["TransId"]);
                string IDParent = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["TransId"]);
                Response.Redirect("../Reports/InventoryPrintReport.aspx?QS=SI~transId|" + id +"~type|SI");
            }
            if (e.CommandName == "Update")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridCommandItem cmdItem = e.Item as GridCommandItem;
                if (editedItem.KeyValues.Contains("IDParent") == true)
                {
                    string strICID = Utility.ToString(((TextBox)editedItem["ItemID"].Controls[0]).Text.ToString().Trim().ToUpper());
                    string strICName = Utility.ToString(((TextBox)editedItem["ItemName"].Controls[0]).Text.ToString().Trim().ToUpper());
                    if (strICID.Length <= 0 || strICName.Length <= 0)
                    {
                        if (strICID.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Item Code Cannot Remain Blank";
                        }
                        if (strICName.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Item Name Cannot Remain Blank";
                        }

                        e.Canceled = true;
                    }
                    else
                    {
                        if (e.CommandName == "Update")
                        {
                            GridEditableItem editit = e.Item as GridEditableItem;
                            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["IDParent"]);
                            sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "' And ID != " + id;
                            sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "' And ID != " + id;
                        }
                        else
                        {
                            sSqlCatID = "Select Count(ID) From Item Where Upper(ItemID)='" + strICID + "'";
                            sSqlCatName = "Select Count(ID) From Item Where Upper(ItemName)='" + strICName + "'";
                        }
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlCatID, null);
                        if (dr.Read())
                        {
                            if (Convert.ToInt16(dr[0].ToString()) > 0)
                            {
                                i = 1;
                                strMessage = strMessage + "<br/>" + "Item Code already exist either in current company/other Company";
                            }
                        }
                        SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSqlCatName, null);
                        if (drnew.Read())
                        {
                            if (Convert.ToInt16(drnew[0].ToString()) > 0)
                            {
                                i = 2;
                                strMessage = strMessage + "<br/>" + "Item Name already exist either in current company/other Company";
                            }
                        }
                        if (i >= 1)
                        {
                            e.Canceled = true;
                        }
                        else
                        {
                            strMessage = "Item updated successfully.";
                        }
                    }
                }
                if (editedItem.KeyValues.Contains("IDChild") == true)
                {
                    string strParID = Utility.ToString(((RadComboBox)editedItem["ParameterID"].Controls[0]).SelectedValue.ToString());
                    string strParName = Utility.ToString(((TextBox)editedItem["ParameterVar"].Controls[0]).Text.ToString().Trim().ToUpper());
                    if (strParID.Length <= 0 || strParName.Length <= 0)
                    {
                        if (strParID.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Parameter for Item Cannot Remain Blank";
                        }
                        if (strParName.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Parameter Remarks for Item Cannot Remain Blank";
                        }
                        e.Canceled = true;
                    }
                    else
                    {
                        string IDChild = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["IDChild"]);
                        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);
                        sSqlID = "Select Count(ID) From ItemParameter Where ParameterID=" + strParID + " And ID != " + id + " And ItemID =" + IDChild;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
                        if (dr.Read())
                        {
                            if (Convert.ToInt16(dr[0].ToString()) > 0)
                            {
                                i = 1;
                                strMessage = strMessage + "<br/>" + "Item Parameter already exist with current Item";
                            }
                        }
                        if (i >= 1)
                        {
                            //if (strMessage.Length > 0)
                            //{
                            //    ShowMessageBox(strMessage);
                            //    strMessage = "";
                            //} 
                            e.Canceled = true;
                        }
                        else
                        {
                            string sSql = "Update ItemParameter Set ParameterID=" + strParID + ", ParameterVar='" + strParName + "' Where ID=" + id;
                            SqlDataReader drnew = DataAccess.ExecuteReader(CommandType.Text, sSql, null);
                            strMessage = "Item updated successfully.";
                        }
                    }
                }
            }

            if (e.CommandName == "PerformInsert")
            {
                string strRemarks = null;
                string strOrdNum = null;
                string storeId = null;
                string supplierId = null;

                if (e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl04" || e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl04")
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    if (((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate != null)
                    {
                        strTransDate = ((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate.Value.ToShortDateString();
                    }

                    strRemarks = Utility.ToString(((TextBox)editedItem["TransactionRemarks"].Controls[0]).Text.ToString().Trim().ToUpper());
                    strOrdNum = Utility.ToString(((TextBox)editedItem["OrderNumber"].Controls[0]).Text.ToString().Trim().ToUpper());
                    storeId = Utility.ToString(((RadComboBox)editedItem["storeId"].Controls[0]).SelectedItem.Value.ToString());
                    supplierId = Utility.ToString(((RadComboBox)editedItem["supId"].Controls[0]).SelectedItem.Value.ToString());
                    strMessage = "";
                    if (strTransDate == "")
                    {

                        if (strTransDate == "")
                        {
                            strMessage = strMessage + "<br/>" + "Please Select Transaction Date";
                        }
                        if (strMessage.Length > 0)
                        {
                            ShowMessageBox(strMessage);
                            strMessage = "";
                        }
                        e.Canceled = true;

                    }
                    else
                    {
                        strMessage = "Transaction added successfully. Please enter the items to complete the transaction.";
                        string newTransDate;



                        newTransDate = Convert.ToDateTime(strTransDate.ToString()).ToString("yyyy/MM/dd", culture); ;
                        string sqlQuery = "Insert into TransactionMaster (TransDate,OrderNumber,TransactionRemarks,StoreId,SupplierId,TransType)values (@TransDate,@OrderNumber,@TransactionRemarks,@StoreId,@SupplierId,@TransType)";

                        SqlParameter[] sqlp = new SqlParameter[6];
                        sqlp[0] = new SqlParameter("@TransDate", newTransDate);
                        sqlp[1] = new SqlParameter("@OrderNumber", strOrdNum);
                        sqlp[2] = new SqlParameter("@TransactionRemarks", strRemarks);
                        sqlp[3] = new SqlParameter("@StoreId", storeId);
                        sqlp[4] = new SqlParameter("@SupplierId", supplierId);
                        sqlp[5] = new SqlParameter("@TransType", "0");

                        int status = DataAccess.ExecuteNonQuery(sqlQuery, sqlp);
                        if (status > 0)
                        {
                            ShowMessageBox(strMessage);

                        }
                        RadGrid1.DataBind();

                    }

                    strMessage = "Item added successfully.";
                }
                else
                {
                    string ID = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["TransId"].ToString();
                    GridEditFormInsertItem editedItem = e.Item as GridEditFormInsertItem;
                    string itemCode = Utility.ToString(((RadComboBox)editedItem["ItemCode"].Controls[0]).SelectedValue.ToString());
                    string quantity = Utility.ToString(((TextBox)editedItem["Quantity"].Controls[0]).Text.ToString().Trim().ToUpper());
                    string price = Utility.ToString(((TextBox)editedItem["Price"].Controls[0]).Text.ToString().Trim().ToUpper());
                    storeId = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["storeId"].ToString();
                    strTransDate = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["TransDate"].ToString();
                    strTransDate = Convert.ToDateTime(strTransDate.ToString()).ToString("yyyy/MM/dd", culture); ;
                    if (itemCode.Length <= 0 || quantity.Length <= 0)
                    {
                        if (itemCode.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Parameter for Item Cannot Remain Blank";
                        }
                        if (quantity.Length <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Qunatity for Item Cannot Remain Blank";
                        }
                        if (strMessage.Length > 0)
                        {
                            ShowMessageBox(strMessage);
                            strMessage = "";
                        }
                        e.Canceled = true;
                    }
                    else
                    {
                        sSqlID = "select Count(*) from TransactionDetails Where ItemCode = " + itemCode + " And TransSubId =" + ID;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSqlID, null);
                        if (dr.Read())
                        {
                            if (Convert.ToInt16(dr[0].ToString()) > 0)
                            {
                                i = 1;
                                strMessage = strMessage + "<br/>" + "Item already exist with current Order";
                            }
                        }
                        if (i >= 1)
                        {
                            if (strMessage.Length > 0)
                            {
                                ShowMessageBox(strMessage);
                                strMessage = "";
                            }
                            e.Canceled = true;
                        }
                        else
                        {
                            SqlTransaction stockTrans = null;
                            try
                            {
                                string IdChildTransId = string.Empty;
                                string ConnString = Constants.CONNECTION_STRING;
                                SqlConnection sqlcon = new SqlConnection(ConnString);
                                sqlcon.Open();
                                
                                stockTrans = sqlcon.BeginTransaction();


                                string sSql = "Insert Into TransactionDetails (TransSubId,ItemCode,Quantity,Price) Values (" + ID + "," + itemCode + ",'" + quantity + "','" + price + "')";
                                SqlCommand sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                                sqlCmd.CommandType = CommandType.Text;
                                int rec = sqlCmd.ExecuteNonQuery();

                                sSql = "SELECT @@IDENTITY";
                                sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                                sqlCmd.CommandType = CommandType.Text;
                                IdChildTransId = sqlCmd.ExecuteScalar().ToString();

                                SqlParameter[] parms = new SqlParameter[5];
                                parms[0] = new SqlParameter("@transId", ID);
                                parms[1] = new SqlParameter("@itemCode", itemCode);
                                parms[2] = new SqlParameter("@count", quantity);
                                parms[3] = new SqlParameter("@storeId", storeId);
                                parms[4] = new SqlParameter("@recId", IdChildTransId);
                                sSql = "sp_InsertBarcodeDetails";
                                sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                                sqlCmd.CommandType = CommandType.StoredProcedure;

                                foreach (SqlParameter param in parms)
                                    sqlCmd.Parameters.Add(param);

                                int status = sqlCmd.ExecuteNonQuery();

                               

                                SqlParameter[] pars = new SqlParameter[8];
                                pars[0] = new SqlParameter("@qty", quantity);
                                pars[1] = new SqlParameter("@rid", ID);
                                pars[2] = new SqlParameter("@itId", itemCode);
                                pars[3] = new SqlParameter("@stId", storeId);
                                pars[4] = new SqlParameter("@type", 4);
                                pars[5] = new SqlParameter("@newstId", 0);
                                pars[6] = new SqlParameter("@UpdatedDate", strTransDate);
                                pars[7] = new SqlParameter("@recSubId", IdChildTransId);
                                sSql = "sp_updateNitemBarcode";
                                sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                                sqlCmd.CommandType = CommandType.StoredProcedure;

                                foreach (SqlParameter param in pars)
                                    sqlCmd.Parameters.Add(param);

                                 status = sqlCmd.ExecuteNonQuery();

                                strMessage = "Item  added successfully.";
                                stockTrans.Commit();
                            }
                            catch(Exception ex)
                            {
                                strMessage = "Unable To Add Items,Transaction Rollbacked";
                                stockTrans.Rollback();
                            }
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
