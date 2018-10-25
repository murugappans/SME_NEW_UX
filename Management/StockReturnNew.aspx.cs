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
    public partial class StockReturnNew : System.Web.UI.Page
    {
        string strMessage = "";
        string strStore = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;


        protected void RadGrid1_DetailTableDataBind(object source, Telerik.Web.UI.GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "Parameters":
                    {
                        string ID = dataItem.OwnerTableView.DataKeyValues[dataItem.ItemIndex]["TransId"].ToString();
                        DataSet ds = new DataSet();
                        string sSQL = "select MasterTransID,TransSubId,StoreId,ItemId,Quantity,Case When EmpId is Null Then '' else EmpId End as EmpId,Case When ProjectId is Null Then '' else ProjectId End as ProjectId from StockOutDetails where MasterTransId='" + ID + "' Order By TransSubId";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                        e.DetailTableView.DataSource = ds.Tables[0];

                        break;
                    }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }
            compid = Utility.ToInteger(Session["Compid"]);

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();

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
                string ItemCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ItemId"]);
                string IdChildTransId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["TransSubId"]);
                string empId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["EmpId"]);
                string prjId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ProjectId"]);
                string strmsg = "";
                if (empId == "")
                {
                    empId = "0";
                }
                if (prjId == "")
                {
                    prjId = "0";
                }
                SqlDataReader dr = null;
                if (IDParent != "")
                {

                    sSQL = "Delete from TransactionMaster Where TransId='" + IDParent + "'";
                    dr = DataAccess.ExecuteReader(CommandType.Text, " Select count(*) from StockOutDetails Where MasterTransId=" + IDParent, null);
                }
                else
                {

                    sSQL = "sp_DeleteItemsFromStockReturn";

                }
                if (e.CommandName == "Delete" && IDParent != "")
                {

                    if (dr.Read())
                    {
                        if (Convert.ToInt16(dr[0].ToString()) > 0)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item which is in use."));
                        }
                        else
                        {
                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                            if (retVal == 1)
                            {
                                DisplayMessage(strmsg);
                                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>  Item Order is Deleted Successfully."));

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
                    sSQL = "select top 1 Barcodeid from ItemHistory Where RecentSubTransId= @IdChildTransId";
                    parms[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                    string BcodeIds = "0";
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, parms);
                    while (dr.Read())
                    {
                        BcodeIds =  dr["Barcodeid"].ToString();
                    }
                    sSQL = "sp_DeleteItemsFromStockReturn";
                    SqlParameter[] parmas = new SqlParameter[5];
                    parmas[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                    parmas[1] = new SqlParameter("@ItemCode", ItemCode);
                    parmas[2] = new SqlParameter("@empCode", empId);
                    parmas[3] = new SqlParameter("@prjCode", prjId);
                    parmas[4] = new SqlParameter("@barCode", BcodeIds);
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parmas);
                    if (retVal == 1 || retVal > 0)
                    {
                        DisplayMessage(strmsg);
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> Item is Deleted Successfully."));
                    }
                    else
                    {
                        DisplayMessage(strmsg);
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Item ."));
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
            string empCode = "";
            string strTranDate = "";
            string strRemarks = "";
            string strOrdNum = "";

            SqlParameter[] cmdParams = null;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sSql = null;
            if (e.CommandName == "Print")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditableItem editit = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["TransID"]);
                Response.Redirect("../Reports/InventoryPrintReport.aspx?QS=SR~transId|241");
            }
            if (e.CommandName == "PerformInsert")
            {
                if (e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl03" || e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl03")
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    if (((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate != null)
                    {
                        strTranDate = ((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate.Value.ToShortDateString();
                    }

                    strRemarks = Utility.ToString(((TextBox)editedItem["TransactionRemarks"].Controls[0]).Text.ToString().Trim().ToUpper());
                    strOrdNum = Utility.ToString(((TextBox)editedItem["OrderNumber"].Controls[0]).Text.ToString().Trim().ToUpper());
                    strMessage = "";
                    if (strTranDate == "")
                    {

                        if (strTranDate == "")
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
                        strTranDate = Convert.ToDateTime(strTranDate).ToString("yyyy/MM/dd", format);
                        sSql = "Select count(*)as TotalItems from BarcodeDetails B Inner join (select I.BarCodeID ,UpdatedDate,StockStatus,ItemCode,StoreId,ProjectId from ItemHistory I Inner join (select BarCodeId,Max(HistoryId)as HistoryId From ItemHistory group by BarcodeId)H on I.HistoryId=H.HistoryId)I On B.BarcodeId=I.BarcodeiD Where I.StockStatus in ('ESO','PSO')  and UPDATEDDATE <= '" + strTranDate + "'";
                        int noOfRecs = DataAccess.ExecuteScalar(sSql, null);
                        if (noOfRecs <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Invalid Transaction Date Selected,No Stock Out In Inventory On This Date";
                            ShowMessageBox(strMessage);
                            e.Canceled = true;
                        }
                        else
                        {
                            strMessage = "Transaction added successfully. Please enter the items to complete the transaction.";
                            string sqlQuery = "Insert Into TransactionMaster (transdate,ordernumber,Transactionremarks,TransType)values('" + strTranDate + "','" + strOrdNum + "','" + strRemarks + "',3)";
                            int status = DataAccess.ExecuteNonQuery(sqlQuery, null);
                            if (status > 0)
                            {
                                ShowMessageBox(strMessage);
                            }
                            RadGrid1.DataBind();
                        }

                    }
                }
                else
                {
                    string ID = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["TransId"].ToString();
                    GridEditFormInsertItem editedItem = e.Item as GridEditFormInsertItem;

                    strTranDate = ((GridDataItem)e.Item.OwnerTableView.ParentItem).OwnerTableView.DataKeyValues[((GridDataItem)e.Item.OwnerTableView.ParentItem).ItemIndex]["TransDate"].ToString();
                    string itemCode = "";
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    itemCode = (userControl.FindControl("dlItem") as DropDownList).SelectedValue;
                    empCode = (userControl.FindControl("dlEmployee") as DropDownList).SelectedValue;
                    string quantity = (userControl.FindControl("txtQty") as TextBox).Text;
                    string storeId = (userControl.FindControl("dlStore") as DropDownList).SelectedValue;
                    string ProjectId = (userControl.FindControl("dlProject") as DropDownList).SelectedValue;
                    string emp_proj = (userControl.FindControl("RadioButtonList1") as RadioButtonList).SelectedValue;


                    if (emp_proj == "1")
                    {
                        ProjectId = "0";
                    }
                    else
                    {
                        empCode = "0";
                    }
                    sSql = "Sp_ValidateStockDetails";
                    cmdParams = new SqlParameter[4];
                    cmdParams[0] = new SqlParameter("@mid", ID);
                    if (emp_proj == "1")
                    {
                        emp_proj = "3";
                        cmdParams[1] = new SqlParameter("@type", 3);
                        cmdParams[2] = new SqlParameter("@itmId", itemCode.Trim());
                        cmdParams[3] = new SqlParameter("@empId", empCode.Trim());
                    }
                    else
                    {
                        emp_proj = "4";
                        cmdParams[1] = new SqlParameter("@type", 4);
                        cmdParams[2] = new SqlParameter("@itmId", itemCode.Trim());
                        cmdParams[3] = new SqlParameter("@empId", ProjectId.Trim());
                    }

                    int noTotal = Utility.ToInteger(DataAccess.ExecuteSPScalar(sSql, cmdParams));

                    if (noTotal > 0)
                    {
                        ShowMessageBox("Invalid Transaction Is Comiting");
                        e.Canceled = true;

                    }
                    else
                    {

                        string newTransDate;

                        //   newTransDate = Convert.ToDateTime(strTranDate);
                        IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

                        newTransDate = Convert.ToDateTime(strTranDate.ToString()).ToString("yyyy/MM/dd", culture); ;

                        sSql = "sp_InsertStockOutDetails";
                        cmdParams = new SqlParameter[7];
                        cmdParams[0] = new SqlParameter("@MTransId", ID);
                        cmdParams[1] = new SqlParameter("@empCode", empCode);
                        cmdParams[2] = new SqlParameter("@prjCode", ProjectId);
                        cmdParams[3] = new SqlParameter("@StId", storeId);
                        cmdParams[4] = new SqlParameter("@ItemId", itemCode);
                        cmdParams[5] = new SqlParameter("@Qty", quantity);
                        cmdParams[6] = new SqlParameter("@type", emp_proj);

                        string ConnString = Constants.CONNECTION_STRING;
                        SqlConnection sqlcon = new SqlConnection(ConnString);
                        sqlcon.Open();
                        SqlTransaction stockTrans;
                        stockTrans = sqlcon.BeginTransaction();
                        SqlCommand sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        try
                        {
                            foreach (SqlParameter param in cmdParams)
                                sqlCmd.Parameters.Add(param);
                            int recSt = sqlCmd.ExecuteNonQuery();
                            string IdChildTransId = "";
                            sSql = "SELECT @@IDENTITY";
                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.Text;
                            IdChildTransId = sqlCmd.ExecuteScalar().ToString();

                            if (Convert.ToInt16(empCode.Trim()) > 0)
                            {
                                sSql = "sp_UnAssignNitemBarcode";
                            }
                            else
                            {
                                sSql = "sp_UnAssignNitemBarcodeProject";
                            }
                            cmdParams = new SqlParameter[9];
                            cmdParams[0] = new SqlParameter("@qty", quantity.Trim());
                            cmdParams[1] = new SqlParameter("@rid", ID.Trim());
                            cmdParams[2] = new SqlParameter("@itId", itemCode.Trim());
                            cmdParams[3] = new SqlParameter("@stId", storeId.Trim());
                            cmdParams[4] = new SqlParameter("@type", emp_proj.Trim());
                            cmdParams[5] = new SqlParameter("@UpdatedDate", newTransDate.Trim());
                            cmdParams[6] = new SqlParameter("@EmployeeID", empCode.Trim());
                            cmdParams[7] = new SqlParameter("@ProjectId", ProjectId.Trim());
                            cmdParams[8] = new SqlParameter("@rChildId", IdChildTransId.Trim());

                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.StoredProcedure;
                            foreach (SqlParameter param in cmdParams)
                                sqlCmd.Parameters.Add(param);

                            recSt = sqlCmd.ExecuteNonQuery();

                            stockTrans.Commit();
                            strMessage = "Item  added successfully.";
                        }
                        catch (Exception ex)
                        {
                            stockTrans.Rollback();
                            strMessage = "Unable to add the Transaction .";
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
