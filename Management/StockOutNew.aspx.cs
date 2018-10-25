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
    public partial class StockOutNew : System.Web.UI.Page
    {
        string strMessage = "";
        string strStore = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string payrollType = string.Empty;
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (!e.IsFromDetailTable)
            {
                // RadGrid1.DataSource = GetDataTable("select TransId,TransDate,OrderNumber,TransactionRemarks,StoreId,SupplierId from TransactionMaster");
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
                        string sSQL = "select s.MasterTransID,s.TransSubId,Case When EmpId is Null Then '--' Else Emp_Name + ' '+ emp_lname End As 'Employee',EmpId,Case When ProjectId is Null Then '--' Else Sub_Project_Name End As 'Project',ProjectId,cast(StoreId as Int)As StoreId,Cast(ItemId as Int) As ItemId,s.Quantity,Case I.IssueType When '1' Then 'Issue' Else 'Sell' End As IssueType from StockOutDetails S Left Outer Join Employee e On e.emp_code=s.empId Left Outer Join SubProject P On S.ProjectId = P.Id Left Outer Join IssueDetails I On  S.TransSubID=I.TransSubId where s.MasterTransId='" + ID + "'  Order By s.TransSubId ";
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
                    dr = DataAccess.ExecuteReader(CommandType.Text, " Select count(*) from StockOutDetails Where  MasterTransId=" + IDParent, null);
                }
                else
                {
                    
                    sSQL = "sp_DeleteItemsFromStockOut";

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
                    sSQL = "select Barcodeid from ItemHistory Where RecentSubTransId= @IdChildTransId";
                    parms[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                    string BcodeIds = "0";
                    dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, parms);
                    while (dr.Read())
                    {
                        BcodeIds = BcodeIds + "," + dr["Barcodeid"].ToString();
                    }

                    sSQL = "SELECT count(*)As NoOf FROM ItemHistory Where BarcOdeId in (" + BcodeIds + ") and StockStatus in ('ESO','PSO')";


                    int noOfRecords = DataAccess.ExecuteScalar(sSQL, null);
                    sSQL = "SELECT count(*)As NoOf FROM ItemHistory Where BarcOdeId in (" + BcodeIds + ") and StockStatus in ('ESR','PSR')";
                    int noOfRecords2 = DataAccess.ExecuteScalar(sSQL, null);

                    if ( noOfRecords2==0)
                    {
                        sSQL = "sp_DeleteItemsFromStockOut";
                        SqlParameter[] parmas = new SqlParameter[4];
                        parmas[0] = new SqlParameter("@IdChildTransId", IdChildTransId);
                        parmas[1] = new SqlParameter("@ItemCode", ItemCode);
                        parmas[2] = new SqlParameter("@empCode", empId);
                        parmas[3] = new SqlParameter("@prjCode", prjId);
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parmas);
                        if (retVal == 1 || retVal > 0)
                        {
                            DisplayMessage(strmsg);
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> " + strmsg + " is Deleted Successfully."));
                        }
                        else
                        {
                            DisplayMessage(strmsg);
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the " + strmsg + " ."));
                        }
                    }
                    else
                    {
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
            if (e.CommandName == "Print")
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                GridEditableItem editit = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editit.ItemIndex]["TransID"]);
                Response.Redirect("../Reports/InventoryPrintReport.aspx?QS=SO~transId|241");
            }
            if (e.CommandName == "PerformInsert")
            {
                IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);
                if (e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl03" || e.Item.UniqueID == "RadGrid1$ctl00$ctl02$ctl03")
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    if (((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate != null)
                    {
                        strTranDate = ((RadDatePicker)e.Item.FindControl("rdTransactionDate")).SelectedDate.Value.ToShortDateString();
                    }
                    string sSql = "";
                    strRemarks = Utility.ToString(((TextBox)editedItem["TransactionRemarks"].Controls[0]).Text.ToString().Trim().ToUpper());
                    strOrdNum = Utility.ToString(((TextBox)editedItem["OrderNumber"].Controls[0]).Text.ToString().Trim().ToUpper());
                    strMessage = "";

                    string newTransDate = Convert.ToDateTime(strTranDate.ToString()).ToString("yyyy/MM/dd", culture); ;
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
                        sSql = "Select count(*)as TotalItems from BarcodeDetails B Inner join (select I.BarCodeID ,UpdatedDate,StockStatus,ItemCode,StoreId,ProjectId from ItemHistory I Inner join (select BarCodeId,Max(HistoryId)as HistoryId From ItemHistory group by BarcodeId)H on I.HistoryId=H.HistoryId)I On B.BarcodeId=I.BarcodeiD Where I.StockStatus in ('ESR','SI','PSR','ST')  and UPDATEDDATE <= '" + newTransDate + "'";
                        int noOfRecs = DataAccess.ExecuteScalar(sSql, null);
                        if (noOfRecs <= 0)
                        {
                            strMessage = strMessage + "<br/>" + "Invalid Transaction Date Selected,No Items Available In Inventory On This Date";
                            ShowMessageBox(strMessage);
                            e.Canceled = true;
                        }
                        else
                        {
                            strMessage = "Transaction added successfully. Please enter the items to complete the transaction.";
                            string sqlQuery = "Insert Into TransactionMaster (transdate,ordernumber,Transactionremarks,TransType)values('" + newTransDate + "','" + strOrdNum + "','" + strRemarks + "',1)";
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
                    //IssueType,DiscountType,DeductionType,Price,DiscountAmount,TotalAmount

                    string IssueType = (userControl.FindControl("dlIssueType") as DropDownList).SelectedValue;
                    string DiscountType = (userControl.FindControl("rdDisCountType1") as RadioButton).Checked.ToString();
                    string DiscountType2 = (userControl.FindControl("rdDisCountType2") as RadioButton).Checked.ToString();
                    string DeductionType = (userControl.FindControl("rdpayType") as RadioButtonList).SelectedValue;
                    string Price = (userControl.FindControl("txtPrice") as TextBox).Text;
                    string DiscountPercent = (userControl.FindControl("txtDiscPercentage") as TextBox).Text;
                    string DiscountLumsum = (userControl.FindControl("txtDiscLumsum") as TextBox).Text;
                    decimal TotalAmount = Convert.ToDecimal((userControl.FindControl("txtTotalPrice") as TextBox).Text);
                    decimal discamt = 0;

                    string sSql = "";


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
                        cmdParams[1] = new SqlParameter("@type", 1);
                        cmdParams[2] = new SqlParameter("@itmId", itemCode);
                        cmdParams[3] = new SqlParameter("@empId", empCode);

                    }
                    else
                    {
                        cmdParams[1] = new SqlParameter("@type", 2);
                        cmdParams[2] = new SqlParameter("@itmId", itemCode);
                        cmdParams[3] = new SqlParameter("@empId", ProjectId);

                    }
                    
                    int noTotal = Utility.ToInteger(DataAccess.ExecuteSPScalar(sSql, cmdParams));

                    if (noTotal > 0)
                    {
                        ShowMessageBox("Invalid Transaction");
                        e.Canceled = true;

                    }
                    else
                    {

                        string newTransDate;

                        //   newTransDate = Convert.ToDateTime(strTranDate);


                        newTransDate = Convert.ToDateTime(strTranDate.ToString()).ToString("yyyy/MM/dd", culture); ;
                        sSql = "select payrolltype  from company where company_id= " + compid;
                        SqlDataReader dr  = DataAccess.ExecuteReader(CommandType.Text, sSql);
                        while (dr.Read())
                        {
                            payrollType = dr[0].ToString();
                        }

                        sSql = "sp_GetPayrollMonth";
                        string year = System.DateTime.Today.Year.ToString();
                        string sDate = String.Empty;
                        string eDate = String.Empty;
                        cmdParams = new SqlParameter[3];
                        cmdParams[0] = new SqlParameter("@ROWID", "0");
                        cmdParams[1] = new SqlParameter("@YEARS", year);
                        cmdParams[2] = new SqlParameter("@PAYTYPE", payrollType);
                        string sMonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(strTranDate.Split('/')[1]));
                        string sMonth = Convert.ToInt16(strTranDate.Split('/')[1]).ToString();
                        DataSet sDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSql, cmdParams);
                        DataRow[] foundRows = sDs.Tables[0].Select("Month = '" + sMonth + "' AND MonthName='" + sMonthName + "'");
                        foreach (DataRow drow in foundRows)
                        {
                            sDate = drow["PaySubStartDate"].ToString();
                            eDate = drow["PaySubEndDate"].ToString();
                        }
                        sSql = "sp_InsertStockOutDetails";
                        cmdParams = new SqlParameter[7];
                        cmdParams[0] = new SqlParameter("@MTransId", ID.Trim());
                        cmdParams[1] = new SqlParameter("@empCode", empCode.Trim());
                        cmdParams[2] = new SqlParameter("@prjCode", ProjectId.Trim());
                        cmdParams[3] = new SqlParameter("@StId", storeId);
                        cmdParams[4] = new SqlParameter("@ItemId", itemCode.Trim());
                        cmdParams[5] = new SqlParameter("@Qty", quantity.Trim());
                        cmdParams[6] = new SqlParameter("@type", emp_proj.Trim());

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

                            cmdParams = new SqlParameter[9];
                            cmdParams[0] = new SqlParameter("@TransSubId", IdChildTransId.Trim());
                            cmdParams[1] = new SqlParameter("@MasterTransId", ID.Trim());
                            cmdParams[2] = new SqlParameter("@IssueType", IssueType.Trim());

                            decimal Tamt = 0;

                            if (IssueType == "2")
                            {
                                if (DiscountType.ToString() == "true" || DiscountType.ToString() == "True")
                                {
                                  
                                    Decimal percent = Decimal.Divide(Convert.ToDecimal(DiscountPercent), 100) * (Convert.ToDecimal(quantity) * Convert.ToDecimal(Price));
                                    int places = 2;
                                    String strplaces = new String('0', places);
                                    if (places > 0)
                                    {
                                        strplaces = "." + strplaces;
                                    }
                                    string retval = percent.ToString("#" + strplaces);
                                    
                                    cmdParams[3] = new SqlParameter("@DiscountType", 1);
                                    cmdParams[4] = new SqlParameter("@DeductionType", DeductionType.Trim());//
                                    cmdParams[5] = new SqlParameter("@Quantity", quantity.Trim());
                                    cmdParams[6] = new SqlParameter("@Price", Price);
                                    discamt = (Convert.ToDecimal(quantity) * Convert.ToDecimal(Price)) - Convert.ToDecimal(TotalAmount);
                                    cmdParams[7] = new SqlParameter("@DiscountAmount", discamt.ToString().Trim());
                                    cmdParams[8] = new SqlParameter("@TotalAmount", TotalAmount);
                                    Tamt = TotalAmount;
                                }
                                else if (DiscountType2.ToString() == "true" || DiscountType2.ToString() == "True")
                                {
                                   
                                    cmdParams[3] = new SqlParameter("@DiscountType", 2);
                                    cmdParams[4] = new SqlParameter("@DeductionType", DeductionType.Trim());//
                                    cmdParams[5] = new SqlParameter("@Quantity", quantity.Trim());
                                    cmdParams[6] = new SqlParameter("@Price", Price);
                                    discamt = (Convert.ToDecimal(quantity) * Convert.ToDecimal(Price)) - Convert.ToDecimal(DiscountLumsum);
                                    cmdParams[7] = new SqlParameter("@DiscountAmount", discamt.ToString().Trim());
                                    cmdParams[8] = new SqlParameter("@TotalAmount", TotalAmount);
                                    Tamt = TotalAmount;
                                }
                                else 
                                {
                                    //TotalAmount = Convert.ToDecimal(DiscountAmo);
                                    DiscountLumsum = "0";
                                    cmdParams[3] = new SqlParameter("@DiscountType", DiscountLumsum);
                                    cmdParams[4] = new SqlParameter("@DeductionType", DeductionType.Trim());//
                                    cmdParams[5] = new SqlParameter("@Quantity", quantity.Trim());
                                    cmdParams[6] = new SqlParameter("@Price", Price);
                                    discamt = (Convert.ToDecimal(quantity) * Convert.ToDecimal(Price)) - Convert.ToDecimal(0);
                                    cmdParams[7] = new SqlParameter("@DiscountAmount", DiscountLumsum);
                                    cmdParams[8] = new SqlParameter("@TotalAmount", discamt);
                                    Tamt = discamt;
                                }
                            }
                            else
                            {
                                DiscountLumsum = "0";
                                cmdParams[3] = new SqlParameter("@DiscountType", DiscountLumsum);
                                cmdParams[4] = new SqlParameter("@DeductionType", DiscountLumsum);
                                cmdParams[5] = new SqlParameter("@Quantity", quantity.Trim());
                                cmdParams[6] = new SqlParameter("@Price", Price);
                                cmdParams[7] = new SqlParameter("@DiscountAmount", DiscountLumsum);
                                cmdParams[8] = new SqlParameter("@TotalAmount", DiscountLumsum);
                            }

                            sSql = " Insert Into IssueDetails (TransSubId,MasterTransId,IssueType,DiscountType,DeductionType,Quantity,Price,DiscountAmount,TotalAmount)Values(@TransSubId,@MasterTransId,@IssueType,@DiscountType,@DeductionType,@Quantity,@Price,@DiscountAmount,@TotalAmount)";
                            sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                            sqlCmd.CommandType = CommandType.Text;
                            foreach (SqlParameter param in cmdParams)
                                sqlCmd.Parameters.Add(param);

                            recSt = sqlCmd.ExecuteNonQuery();
                            if (IssueType == "2")
                            {
                                sSql = " insert Into emp_deductions(trx_type,trx_amount,trx_period,created_on,modified_on,emp_code,Status) values (14," + Tamt + ",'" + sDate + "','" + sDate + "','" + sDate + "'," + empCode.Trim() + ",'U')";
                                sqlCmd = new SqlCommand(sSql, sqlcon, stockTrans);
                                sqlCmd.CommandType = CommandType.Text;
                                int status = sqlCmd.ExecuteNonQuery();

                            }
                            
                           

                            sSql = "sp_updateNitemBarcode";
                            cmdParams = new SqlParameter[11];
                            cmdParams[0] = new SqlParameter("@qty", quantity.Trim());
                            cmdParams[1] = new SqlParameter("@rid", ID.Trim());
                            cmdParams[2] = new SqlParameter("@itId", itemCode.Trim());
                            cmdParams[3] = new SqlParameter("@stId", storeId.Trim());
                            cmdParams[4] = new SqlParameter("@type", emp_proj.Trim());
                            cmdParams[5] = new SqlParameter("@newstId", 0);
                            cmdParams[6] = new SqlParameter("@UpdatedDate", newTransDate.Trim());
                            cmdParams[7] = new SqlParameter("@EmployeeID", empCode.Trim());
                            cmdParams[8] = new SqlParameter("@ProjectId", ProjectId.Trim());
                            if (IssueType == "2")
                            {
                                if (emp_proj == "1")
                                {
                                    cmdParams[9] = new SqlParameter("@saleType", "ESS");
                                }
                                else {
                                    cmdParams[9] = new SqlParameter("@saleType", "ESS");
                                }
                            }
                            else {
                                cmdParams[9] = new SqlParameter("@saleType", "0");
                            }
                            cmdParams[10] = new SqlParameter("@recSubId", IdChildTransId);
                            //sp_updateNitemBarcode
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
