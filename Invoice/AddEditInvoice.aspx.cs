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
using System.Net.Mail;
using System.IO;
using System.Text;

namespace SMEPayroll.Invoice
{
    public partial class AddEditInvoice : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        public string connection;
        string _actionMessage = "";
        public int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            connection = Session["ConString"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource_VarPref.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                LoadClienDropdown();
                Loadpaymentterms();
                TodayDate();

                Session["dsTemp"] = null;
                Session["VariableDS"] = null;

                DeleteNonConfirmInvoice();
                LoadInvoiceTextFromPreference();
            }
            
           
        }
        protected void LoadInvoiceTextFromPreference()
        {
            string sql = "SELECT [RId],[Text] FROM [ReportText] where  [Desc] = 'Invoice' AND  [Company_Id]='" + compid + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                FooterEditor.Content = dr.GetString(dr.GetOrdinal("Text"));

            }
        }

        protected void DeleteNonConfirmInvoice()
        {
            string sql_delete = "delete from invoice_info where [Confirm]='0';delete from invoice_monthly where InvoiceNo in (select InvoiceNo from Invoice_info where [confirm]='0');delete from invoice_hourly where InvoiceNo in (select InvoiceNo from Invoice_info where [confirm]='0')";
            DataAccess.FetchRS(CommandType.Text, sql_delete, null);
        }

        public DataSet ds_Hor = new DataSet();
        public DataSet ds_Mon = new DataSet();
        public int ClientId;
        public string Fromdate, ToDate;
        int l = 0;
        DataSet dsTemp_H;

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
        int overlap;
        protected void btnView_Click(object sender, EventArgs e)
        {
            #region Input Parameter
            string sp_sql = "Sp_Invoice";
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(datePickerFrom.SelectedDate);
            int m = dt.Date.Month;
            int d = dt.Date.Day;
            int y = dt.Date.Year;
            Fromdate = d + "/" + m + "/" + y;

            DateTime dt1 = new DateTime();
            dt1 = Convert.ToDateTime(datePickerTo.SelectedDate);
            int m1 = dt1.Date.Month;
            int d1 = dt1.Date.Day;
            int y1 = dt1.Date.Year;
            ToDate = d1 + "/" + m1 + "/" + y1;

            if (cmbClient.SelectedValue != "")
            {
                ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            }
            else
            {
               // ShowMessageBox("Please Select the Client");
                _actionMessage = "Warning|Please Select the Client";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }

        

            #endregion

            

                #region Hourly
                //Getting Project which is checked in the dropdown
                foreach (RadComboBoxItem item in drpProject.Items)
                {
                    CheckBox chk = (CheckBox)item.FindControl("chk1");
                    if (chk.Checked)
                    {

                        //validation for overlap dates

                        string sSQL = "sp_OverlapDates";
                        SqlParameter[] parm5 = new SqlParameter[3];
                        parm5[0] = new SqlParameter("@FromDate", Fromdate);
                        parm5[1] = new SqlParameter("@ToDate", ToDate);
                        parm5[2] = new SqlParameter("@project", item.Text);
                        SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parm5);

                        if (dr1.Read())
                        {
                            overlap = Convert.ToInt16(dr1[0].ToString());
                        }
                        if (overlap > 0)
                        {
                            //ShowMessageBox("Invoice was already created for this  Date, Project:" + item.Text + "");
                        _actionMessage = "Warning|Invoice was already created for this  Date, Project:"+ item.Text ;
                        ViewState["actionMessage"] = _actionMessage;
                        return;
                        }
                        //


                        SqlParameter[] parms = new SqlParameter[5];
                        parms[0] = new SqlParameter("@FromDate", Fromdate);
                        parms[1] = new SqlParameter("@ToDate", ToDate);
                        parms[2] = new SqlParameter("@ProjectID", item.Value);
                        parms[3] = new SqlParameter("@ClientId", ClientId);
                        parms[4] = new SqlParameter("@Trade", "0");

                        ds_Hor = DataAccess.ExecuteSPDataSet(sp_sql, parms);

                        if (l == 0)//If first time copy the structure of the table
                        {
                            dsTemp_H = ds_Hor.Clone();
                            l++;


                            //adding auto increment column
                            DataColumn auto = new DataColumn("AutoId", typeof(System.Int32));
                            dsTemp_H.Tables[0].Columns.Add(auto);
                            auto.AutoIncrement = true;
                            auto.AutoIncrementSeed = 1;
                            auto.ReadOnly = true;
                            //
                        }

                        if (ds_Hor.Tables[0].Rows.Count > 0)//Loop through each trade
                        {
                            //copy Monthly trade to temp dataset
                            foreach (DataRow row in ds_Hor.Tables[0].Rows)
                            {
                                dsTemp_H.Tables[0].ImportRow(row);

                            }
                        }

                    }
                }

                if (l == 0)
                {
                    //ShowMessageBox("Please Select the Project");
                _actionMessage = "Warning|Please Select the Project";
                ViewState["actionMessage"] = _actionMessage;
            }

                //
                Session["HourlyDS"] = dsTemp_H;
                RadGrid_Hourly.DataSource = dsTemp_H;
                RadGrid_Hourly.DataBind();

                #endregion
           
            }

        DataSet Ds_Hr = new DataSet();
        int j=0,k=0,s=0;
        DataSet Ds_Mo = new DataSet();
        DataSet Ds_var = new DataSet();
        protected void CalculatePreview()
        {

            if (txtInvoiceNo.Text != "")
            {
                //delete all the records which we preview;
                string sql_delete = "delete from invoice_info where InvoiceNo='" + txtInvoiceNo.Text + "';delete from invoice_monthly where InvoiceNo='" + txtInvoiceNo.Text + "';delete from invoice_hourly where InvoiceNo='" + txtInvoiceNo.Text + "';delete from invoice_variable where InvoiceNo='" + txtInvoiceNo.Text + "'";
                DataAccess.FetchRS(CommandType.Text, sql_delete, null);

                //insert in Invoice_Info table
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(datePickerId.SelectedDate);
                string ssqlb_InvoiceInfo = "INSERT INTO [dbo].[Invoice_Info]([InvoiceNo],[ClientID],[PaymentTerms],[Company_Id],[confirm],[FooterText],[CreateDate]) VALUES ('" + txtInvoiceNo.Text + "', '" + Utility.ToInteger(cmbClient.SelectedValue) + "','" + cmbpaymentterms.SelectedValue + "','" + Utility.ToInteger(Session["Compid"].ToString()) + "',0,'" + FooterEditor.Content.Replace("'", "") + "','" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "')";
                DataAccess.FetchRS(CommandType.Text, ssqlb_InvoiceInfo, null);
            }


            #region Invoice_Hourly
            if (RadGrid_Hourly.MasterTableView.Items.Count > 0)
            {
                //Hourly checkbox Select
                DataSet HourlyDs_Temp = new DataSet();
                if (Session["HourlyDS"] != "")
                {
                    HourlyDs_Temp = (DataSet)Session["HourlyDS"];
                }

                int IdSelect;
                foreach (GridItem item in this.RadGrid_Hourly.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn_H"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            string id = this.RadGrid_Hourly.Items[dataItem.ItemIndex]["AutoId"].Text.ToString();
                            //IdSelect = IdSelect + id + ",";
                            IdSelect = Convert.ToInt32(id);

                            //copy to temp DS
                            if (j == 0)//If first time copy the structure of the table
                            {
                                Ds_Hr = HourlyDs_Temp.Clone();
                                j++;
                            }

                            //copy selected Hourly trade to temp dataset
                            foreach (DataRow row in HourlyDs_Temp.Tables[0].Select("AutoId='" + IdSelect + "'"))
                            {
                                Ds_Hr.Tables[0].ImportRow(row);

                            }


                        }
                    }
                }
              
                //



                //save in database
                //create Invoice_info,invoice_hourly,invoiec_monthly

                if (txtInvoiceNo.Text != "" && j > 0)
                {

                    //Adding Column to store InvoiceNo
                    if (Ds_Hr.Tables[0].Columns.Contains("InvoiceNo") == false)
                    {
                        DataColumn InvoiceNo = new DataColumn("InvoiceNo", typeof(System.String));
                        InvoiceNo.DefaultValue = txtInvoiceNo.Text;
                        Ds_Hr.Tables[0].Columns.Add(InvoiceNo);
                    }
                    //

                    //Adding Column to store FromDate
                    if (Ds_Hr.Tables[0].Columns.Contains("FromDate") == false)
                    {
                        DataColumn FromDate = new DataColumn("FromDate", typeof(System.String));
                        FromDate.DefaultValue = datePickerFrom.SelectedDate;
                        Ds_Hr.Tables[0].Columns.Add(FromDate);
                    }
                    //

                    //Adding Column to store FromDate
                    if (Ds_Hr.Tables[0].Columns.Contains("ToDate") == false)
                    {
                        DataColumn ToDate = new DataColumn("ToDate", typeof(System.String));
                        ToDate.DefaultValue = datePickerTo.SelectedDate;
                        Ds_Hr.Tables[0].Columns.Add(ToDate);
                    }
                    //


                    //Hourly
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.Invoice_Hourly";

                        try
                        {
                            //// Set up the column mappings by name.
                            SqlBulkCopyColumnMapping col1 = new SqlBulkCopyColumnMapping("InvoiceNo", "InvoiceNo");
                            bulkCopy.ColumnMappings.Add(col1);

                            SqlBulkCopyColumnMapping col2 = new SqlBulkCopyColumnMapping("Project", "Project");
                            bulkCopy.ColumnMappings.Add(col2);

                            SqlBulkCopyColumnMapping col3 = new SqlBulkCopyColumnMapping("Trade", "Trade");
                            bulkCopy.ColumnMappings.Add(col3);

                            SqlBulkCopyColumnMapping col4 = new SqlBulkCopyColumnMapping("Amount", "Amount");
                            bulkCopy.ColumnMappings.Add(col4);

                            SqlBulkCopyColumnMapping col5 = new SqlBulkCopyColumnMapping("FromDate", "FromDate");
                            bulkCopy.ColumnMappings.Add(col5);

                            SqlBulkCopyColumnMapping col6 = new SqlBulkCopyColumnMapping("ToDate", "ToDate");
                            bulkCopy.ColumnMappings.Add(col6);

                            bulkCopy.WriteToServer(Ds_Hr.Tables[0]);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    //


                    //Clear Session
                    //Session["HourlyDS"] = "";
                }
                }
            
            #endregion

            #region save in Invoice_monthly
            if (RadGrid1_Monthly.MasterTableView.Items.Count > 0)
            {

                DataSet MonthlyDs_Temp = new DataSet();
                if (Session["MonthlyDS"] != "")
                {
                    MonthlyDs_Temp = (DataSet)Session["MonthlyDS"];
                }

                int IdSelect_M;
                foreach (GridItem item in this.RadGrid1_Monthly.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn_M"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            string id = this.RadGrid1_Monthly.Items[dataItem.ItemIndex]["AutoId"].Text.ToString();
                            //IdSelect = IdSelect + id + ",";
                            IdSelect_M = Convert.ToInt32(id);

                            //copy to temp DS
                            if (k == 0)//If first time copy the structure of the table
                            {
                                Ds_Mo = MonthlyDs_Temp.Clone();
                                k++;
                            }

                            //copy selected Hourly trade to temp dataset
                            foreach (DataRow row in MonthlyDs_Temp.Tables[0].Select("AutoId='" + IdSelect_M + "'"))
                            {
                                Ds_Mo.Tables[0].ImportRow(row);

                            }


                        }
                    }
                }



                if (txtInvoiceNo.Text != "" && k>0)
                {
                    //Adding Column to store InvoiceNo
                    if (Ds_Mo.Tables[0].Columns.Contains("InvoiceNo") == false)
                    {
                        DataColumn InvoiceNo = new DataColumn("InvoiceNo", typeof(System.String));
                        InvoiceNo.DefaultValue = txtInvoiceNo.Text;
                        Ds_Mo.Tables[0].Columns.Add(InvoiceNo);
                    }
                    //

                    //Adding Column to store Month
                    if (Ds_Mo.Tables[0].Columns.Contains("Month") == false)
                    {
                        DataColumn Mon = new DataColumn("Month", typeof(System.Int32));
                        Mon.DefaultValue = Convert.ToInt32(cmbMonth.SelectedValue);
                        Ds_Mo.Tables[0].Columns.Add(Mon);
                    }
                    //

                    //Adding Column to store Year
                    if (Ds_Mo.Tables[0].Columns.Contains("Year") == false)
                    {
                        DataColumn Yer = new DataColumn("Year", typeof(System.Int32));
                        Yer.DefaultValue = Convert.ToInt32(cmbYear.SelectedValue);
                        Ds_Mo.Tables[0].Columns.Add(Yer);
                    }
                    //

                    //Monthly
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.Invoice_monthly";

                        try
                        {
                            //// Set up the column mappings by name.
                            SqlBulkCopyColumnMapping col1 = new SqlBulkCopyColumnMapping("InvoiceNo", "InvoiceNo");
                            bulkCopy.ColumnMappings.Add(col1);

                            SqlBulkCopyColumnMapping col2 = new SqlBulkCopyColumnMapping("Project", "Project");
                            bulkCopy.ColumnMappings.Add(col2);

                            SqlBulkCopyColumnMapping col3 = new SqlBulkCopyColumnMapping("Trade", "Trade");
                            bulkCopy.ColumnMappings.Add(col3);

                            SqlBulkCopyColumnMapping col4 = new SqlBulkCopyColumnMapping("Monthly", "Amount");
                            bulkCopy.ColumnMappings.Add(col4);

                            SqlBulkCopyColumnMapping col5 = new SqlBulkCopyColumnMapping("Empcode", "Empcode");
                            bulkCopy.ColumnMappings.Add(col5);

                            SqlBulkCopyColumnMapping col6 = new SqlBulkCopyColumnMapping("Month", "Month");
                            bulkCopy.ColumnMappings.Add(col6);

                            SqlBulkCopyColumnMapping col7 = new SqlBulkCopyColumnMapping("Year", "Year");
                            bulkCopy.ColumnMappings.Add(col7);

                            bulkCopy.WriteToServer(Ds_Mo.Tables[0]);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    //

                }
            }
            #endregion

            #region Save in Invoice_variable

            if (RadGrid3.MasterTableView.Items.Count > 0)
            {

                DataSet VariableDs_Temp = new DataSet();
                if (Session["VariableDS"] != "")
                {
                    VariableDs_Temp = (DataSet)Session["VariableDS"];
                }

                int IdSelect_V;
                foreach (GridItem item in this.RadGrid3.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn_V"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            string id = this.RadGrid3.Items[dataItem.ItemIndex]["AutoId"].Text.ToString();
                            IdSelect_V = Convert.ToInt32(id);

                            //copy to temp DS
                            if (s == 0)//If first time copy the structure of the table
                            {
                                Ds_var = VariableDs_Temp.Clone();
                                s++;
                            }

                            //copy selected Hourly trade to temp dataset
                            foreach (DataRow row in VariableDs_Temp.Tables[0].Select("AutoId='" + IdSelect_V + "'"))
                            {
                                Ds_var.Tables[0].ImportRow(row);

                            }

                        }
                    }
                }



                if (txtInvoiceNo.Text != "" && s > 0)
                {
                    //Adding Column to store InvoiceNo
                    if (Ds_var.Tables[0].Columns.Contains("InvoiceNo") == false)
                    {
                        DataColumn InvoiceNo = new DataColumn("InvoiceNo", typeof(System.String));
                        InvoiceNo.DefaultValue = txtInvoiceNo.Text;
                        Ds_var.Tables[0].Columns.Add(InvoiceNo);
                    }
                    //

                    //Variable
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.Invoice_variable";

                        try
                        {
                            //// Set up the column mappings by name.
                            SqlBulkCopyColumnMapping col1 = new SqlBulkCopyColumnMapping("InvoiceNo", "InvoiceNo");
                            bulkCopy.ColumnMappings.Add(col1);

                            SqlBulkCopyColumnMapping col2 = new SqlBulkCopyColumnMapping("VariableName", "variableName");
                            bulkCopy.ColumnMappings.Add(col2);

                            SqlBulkCopyColumnMapping col3 = new SqlBulkCopyColumnMapping("FinalAmount", "Amount");
                            bulkCopy.ColumnMappings.Add(col3);

                            SqlBulkCopyColumnMapping col4 = new SqlBulkCopyColumnMapping("Type", "AddSubType");
                            bulkCopy.ColumnMappings.Add(col4);

                            //SqlBulkCopyColumnMapping col5 = new SqlBulkCopyColumnMapping("Project", "Project");
                            //bulkCopy.ColumnMappings.Add(col5);

                            bulkCopy.WriteToServer(Ds_var.Tables[0]);

                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            //Clear Session
            //Session["VariableDS"] = "";
            #endregion

            #region Calculate Total and check atleast on value is selected
            if (k == 0 && j == 0 && s == 0)
            {
                string ssqlb_Delete = "delete from invoice_info where InvoiceNo='" + Convert.ToString(txtInvoiceNo.Text)+"'";
                DataAccess.FetchRS(CommandType.Text, ssqlb_Delete, null);  
            }

            else
            {

                if (txtInvoiceNo.Text != "")
                {
                    SqlParameter[] parms2 = new SqlParameter[1];
                    parms2[0] = new SqlParameter("@InvoiceNo ", Convert.ToString(txtInvoiceNo.Text));
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet("sp_Invoice_ReportDetail", parms2);

                    decimal add = 0, sub = 0, GST = 0, Total, SubTotal;
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (row["Type"].ToString() == "Addition")
                        {
                            add = add + Convert.ToDecimal(row["Amount"]);
                        }
                        if (row["Type"].ToString() == "Deduction")
                        {
                            sub = sub + Convert.ToDecimal(row["Amount"]);
                        }
                    }
                    SubTotal = add - sub;


                    //GST
                    if (chkGST.Checked == true)
                    {
                        GST = ((7 * SubTotal) / 100);
                    }
                    else
                    {
                        GST = 0;
                    }
                    //

                    Total = SubTotal + GST;


                    string ssqlb_Update = "UPDATE [Invoice_Info] SET [SubTotal]='" + SubTotal + "',[GST]='" + GST + "',[Total]='" + Total + "'where InvoiceNo='" + Convert.ToString(txtInvoiceNo.Text) + "'";
                    DataAccess.FetchRS(CommandType.Text, ssqlb_Update, null);


                }
            }
            #endregion
        }

        int i = 0;
        DataSet dsTemp;
        int overlap_Mon;
        protected void btnMonthlyView_Click(object sender, ImageClickEventArgs e)
        {

            if (cmbClient.SelectedValue != "")
            {
                ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            }
            else
            {
                //ShowMessageBox("Please Select the Client");
                _actionMessage = "Warning|Please Select the Client";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }

              Session["dsTemp"] = null;

              string checkedText = string.Empty;
              foreach (RadComboBoxItem item in drpProject.Items)//loop each project
              {
                  CheckBox chk = (CheckBox)item.FindControl("chk1");
                  if (chk.Checked)
                  {

                      //Validation to check whether invoice is created
                      string SQL_MonVal = "select count(*) as [Count] from invoice_info where Confirm='1' and InvoiceNo in (select InvoiceNo from invoice_monthly where [Month]='" + cmbMonth.SelectedValue + "' AND [Year]='" + cmbYear.SelectedValue + "' AND Project='" + item.Value + "')";
                          SqlDataReader dr_MonVal = DataAccess.ExecuteReader(CommandType.Text, SQL_MonVal, null);
                          if (dr_MonVal.Read())
                          {
                              overlap_Mon = Convert.ToInt32(dr_MonVal[0].ToString());
                          }

                          if (overlap_Mon > 0)
                          {
                              //ShowMessageBox("Invoice was already created for this  Date, Project:" + item.Text + "");
                        _actionMessage = "Warning|Invoice was already created for this  Date, Project:"+ item.Text;
                        ViewState["actionMessage"] = _actionMessage;
                        return;
                          }
                      //


                      DataSet monthDs = new DataSet();
                      string ssql = "sp_MonthlyInvoice";//'1','MS20111004','2011','10','3','0','1'
                      SqlParameter[] parms = new SqlParameter[5];
                      parms[0] = new SqlParameter("@ClientId", ClientId);
                      parms[1] = new SqlParameter("@Project", item.Value);
                      parms[2] = new SqlParameter("@year", cmbYear.SelectedValue);
                      parms[3] = new SqlParameter("@month", cmbMonth.SelectedValue);
                      parms[4] = new SqlParameter("@compId", compid);
                    
                      monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);

                      if (i == 0)//If first time copy the structure of the table
                      {
                          dsTemp = monthDs.Clone();
                          i++;


                          //adding auto increment column
                          DataColumn auto = new DataColumn("AutoId", typeof(System.Int32));
                          dsTemp.Tables[0].Columns.Add(auto);
                          auto.AutoIncrement = true;
                          auto.AutoIncrementSeed = 1;
                          auto.ReadOnly = true;
                          //
                      }


                      if (monthDs.Tables[0].Rows.Count > 0)//Loop through each trade
                      {
                          //copy Monthly trade to temp dataset
                          foreach (DataRow row in monthDs.Tables[0].Rows)
                          {
                              dsTemp.Tables[0].ImportRow(row);

                          }
                      }


                  }
              }

              if (i == 0)
              {
                  //ShowMessageBox("Please Select the Project");
                _actionMessage = "Warning|Please Select the Project";
                ViewState["actionMessage"] = _actionMessage;
            }
              Session["MonthlyDS"] = dsTemp;
              Session["dsTemp"] = dsTemp;
              FinalDataset();

        }

        int r = 0;
        DataSet dsTempvar;
        protected void btnVariableView_Click(object sender, ImageClickEventArgs e)
        {
            if (cmbClient.SelectedValue != "")
            {
                ClientId = Convert.ToInt32(cmbClient.SelectedValue);
            }
            else
            {
               // ShowMessageBox("Please Select the Client");
                _actionMessage = "Warning|Please Select the Client";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }
            string checkedText = string.Empty;

            



            foreach (RadComboBoxItem item in drpProject.Items)//loop each project
            {
                CheckBox chk = (CheckBox)item.FindControl("chk1");
                if (chk.Checked)
                {

                    DataSet VariableDs = new DataSet();
                    SqlParameter[] parms = new SqlParameter[6];
                    string ssql;
                    if (datePickerFrom.SelectedDate != null && datePickerTo.SelectedDate != null)
                    {
                        ssql = "sp_Invoice_Variable1";//'1','MS20111004','3'
                        //SqlParameter[] parms = new SqlParameter[6];
                        parms[0] = new SqlParameter("@FromDate", datePickerFrom.SelectedDate);//take from hourly date
                        parms[1] = new SqlParameter("@ToDate", datePickerTo.SelectedDate);
                        parms[2] = new SqlParameter("@ClientId", ClientId);
                        parms[3] = new SqlParameter("@Project", item.Value);
                        parms[4] = new SqlParameter("@compId", compid);
                        parms[5] = new SqlParameter("@ifDate", "1");
                    }
                    else//if no date dont show daily variable
                    {
                        ssql = "sp_Invoice_Variable1";//'1','MS20111004','3'
                        parms[0] = new SqlParameter("@FromDate", "");
                        parms[1] = new SqlParameter("@ToDate", "");
                        parms[2] = new SqlParameter("@ClientId", ClientId);
                        parms[3] = new SqlParameter("@Project", item.Value);
                        parms[4] = new SqlParameter("@compId", compid);
                        parms[5] = new SqlParameter("@ifDate", "0");
                    }

                    VariableDs = DataAccess.ExecuteSPDataSet(ssql, parms);

                    if (r == 0)//If first time copy the structure of the table
                    {
                        dsTempvar = VariableDs.Clone();
                        r++;


                        //adding auto increment column
                        DataColumn auto = new DataColumn("AutoId", typeof(System.Int32));
                        dsTempvar.Tables[0].Columns.Add(auto);
                        auto.AutoIncrement = true;
                        auto.AutoIncrementSeed = 1;
                        auto.ReadOnly = true;
                        //


                        DataColumn Type = new DataColumn("Type", typeof(System.String));
                        //Type.DefaultValue = "1";
                        dsTempvar.Tables[0].Columns.Add(Type);

                    }


                    if (VariableDs.Tables[0].Rows.Count > 0)//Loop through each trade
                    {
                        //copy Monthly trade to temp dataset
                        foreach (DataRow row in VariableDs.Tables[0].Rows)
                        {
                            dsTempvar.Tables[0].ImportRow(row);
                        }
                    }


                }
            }


           //update Type based on variableid
            if (dsTempvar != null )
            {
                foreach (DataRow row in dsTempvar.Tables[0].Rows)
                {
                    //get Addition or Deduction based on variable id
                    string sql = "SELECT [Type]  FROM [variable_type] where [VarId]=(SELECT [VariableId] FROM [Variable_Preference] where [Vid]='" + row["VariableId"] + "')";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    while (dr.Read())
                    {
                        row["Type"] = (Utility.ToString(dr.GetValue(0)) != "" ? Utility.ToString(dr.GetValue(0)) : "");
                    }
                    //
                }
            }

            if (r == 0)
            {
               // ShowMessageBox("Please Select the Project");
                _actionMessage = "Warning|Please Select the Project";
                ViewState["actionMessage"] = _actionMessage;
            }

            Session["VariableDS"] = dsTempvar;
            RadGrid3.DataSource = dsTempvar;
            RadGrid3.DataBind();
            
          

        }


        protected void RadGrid3_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if daily make default as selected
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (dataItem["DailyOneTime"].Text == "Daily")
                {
                    dataItem.Selected = true;

                    //hide the delete button
                    ImageButton imgBtn = (ImageButton)dataItem["DeleteColumn"].Controls[0];
                    imgBtn.Visible = false;
                    //hide the edit button
                    ImageButton imgBtn1 = (ImageButton)dataItem["EditColumn"].Controls[0];
                    imgBtn1.Visible = false;
                }
            }
        }
        protected void RadGrid3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (Session["VariableDS"] != "")
            {
                DataSet dsTemp_var = (DataSet)Session["VariableDS"];
                RadGrid3.DataSource = dsTemp_var;
            }
        }

        protected void RadGrid3_InsertCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridDropDownColumnEditor editor_VariableName = (GridDropDownColumnEditor)item.EditManager.GetColumnEditor("DropCol");
                // GridDropDownColumnEditor editor_Type = (GridDropDownColumnEditor)item.EditManager.GetColumnEditor("DropColType");
                GridTextBoxColumnEditor editor_Amount = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Amount");

                DataSet dsTemp_var = (DataSet)Session["VariableDS"];
                DataRow newRow = dsTemp_var.Tables[0].NewRow();
                newRow["variableId"] = editor_VariableName.SelectedValue;
                newRow["variableName"] = editor_VariableName.SelectedText;
                //get Addition or Deduction based on variable id
                string sql = "SELECT [Type]  FROM [variable_type] where [VarId]=(SELECT [VariableId] FROM [Variable_Preference] where [Vid]='" + editor_VariableName.SelectedValue + "')";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                while (dr.Read())
                {
                    newRow["Type"] = (Utility.ToString(dr.GetValue(0)) != "" ? Utility.ToString(dr.GetValue(0)) : "");
                }
                //
                newRow["Amount"] = editor_Amount.Text;
                newRow["FinalAmount"] = editor_Amount.Text;
                dsTemp_var.Tables[0].Rows.Add(newRow);
                dsTemp_var.AcceptChanges();

                Session["VariableDS"] = dsTemp_var;
            }
        }
        protected void RadGrid3_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int AutoId = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["AutoId"]);
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridDropDownColumnEditor editor_VariableName = (GridDropDownColumnEditor)item.EditManager.GetColumnEditor("DropCol");
               // GridDropDownColumnEditor editor_Type = (GridDropDownColumnEditor)item.EditManager.GetColumnEditor("DropColType");
                GridTextBoxColumnEditor editor_Amount = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Amount");

                DataSet dsTemp_var = (DataSet)Session["VariableDS"];

                DataRow[] updateRow = dsTemp_var.Tables[0].Select("AutoId = '" + AutoId + "'");
                updateRow[0]["variableId"] = editor_VariableName.SelectedValue;
                updateRow[0]["variableName"] = editor_VariableName.SelectedText;
                //get Addition or Deduction based on variable id
                string sql = "SELECT [Type]  FROM [variable_type] where [VarId]=(SELECT [VariableId] FROM [Variable_Preference] where [Vid]='" + editor_VariableName.SelectedValue + "')";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                while (dr.Read())
                {
                  updateRow[0]["Type"]= (Utility.ToString(dr.GetValue(0)) != "" ? Utility.ToString(dr.GetValue(0)) : "");
                }
                //
                updateRow[0]["Amount"] = editor_Amount.Text;
                updateRow[0]["FinalAmount"] = editor_Amount.Text;
                dsTemp_var.AcceptChanges();
                Session["VariableDS"] = dsTemp_var;

            }
        }
        protected void RadGrid3_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            int AutoId = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["AutoId"]);
            DataSet dsTemp_var = (DataSet)Session["VariableDS"];
            DataRow[] rows = dsTemp_var.Tables[0].Select("AutoId = '" + AutoId + "'");
            foreach (DataRow row in rows)
                row.Delete();

            dsTemp_var.AcceptChanges();

            Session["VariableDS"] = dsTemp_var;
        }

        protected void FinalDataset()
        {

            DataSet dsTemp1 = (DataSet)Session["dsTemp"];
            RadGrid1_Monthly.DataSource = dsTemp1;
            RadGrid1_Monthly.DataBind();
        }

        protected void RadGrid1_Monthly_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet dsTemp1 = (DataSet)Session["dsTemp"];
            RadGrid1_Monthly.DataSource = dsTemp1;
        }

        public int AuId;
        protected void RadGrid1_Monthly_UpdateCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem)
            {
                GridEditableItem item = (GridEditableItem)e.Item;

               // TextBox MonthlyTxt = (TextBox)item.FindControl("txtMonthly");
                TextBox DeductDaysTxt = (TextBox)item.FindControl("txtDeductDays");
                HiddenField DailyRatetxt= (HiddenField)item.FindControl("hdnDailyRate");

                object MonthlySal = item.OwnerTableView.DataKeyValues[item.ItemIndex]["MonthlyFixed"];

                decimal DaysDeduct = Convert.ToDecimal(DeductDaysTxt.Text);
                decimal TotalSalary = Convert.ToDecimal(MonthlySal);
                decimal DailyRate = Convert.ToDecimal(DailyRatetxt.Value);

                decimal SalaryDeduct = DailyRate * DaysDeduct;
                decimal FinalSalary = TotalSalary - SalaryDeduct;


                object AutoId = item.OwnerTableView.DataKeyValues[item.ItemIndex]["AutoId"];
               
                try
                {

                    //update Dataset
                    DataSet dsTemp_Upd = (DataSet)Session["dsTemp"];

                    DataRow[] UpdateRow = dsTemp_Upd.Tables[0].Select("AutoId = '"+AutoId+"'");
                    UpdateRow[0]["Monthly"] = FinalSalary;

                   
                    Session["dsTemp"] = dsTemp_Upd;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        //ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                        ErrMsg = "Already Exist.";
                        //RadGrid1_Monthly.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to add record. Reason:" + ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    }
                }
            }
        }

        protected void TodayDate()
        {
            DateTime date = DateTime.Now;
            date = Convert.ToDateTime(date);
            datePickerId.SelectedDate = date;
        }

        protected void drpClient_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadAddressForClient();

            //Project_Dropdown();
        }

        protected void LoadAddressForClient()
        {
            if (cmbClient.SelectedValue != "-1")
            {
                string sSQLClientAdd = "select [ContactPerson1],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[ClientName],[Phone1],[Phone2],[Fax],[Email],[ContactPerson2],[Remark] from clientdetails where clientID='" + cmbClient.SelectedValue + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLClientAdd, null);
                while (dr.Read())
                {
                    //Label1.Text = Utility.ToString(dr.GetValue(0));
                    //Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "BLOCK " + Utility.ToString(dr.GetValue(1)) + "" : "");
                    //Label3.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "# " + Utility.ToString(dr.GetValue(3)) + "-" : "") + (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "") + " " + (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                    //Label4.Text = (Utility.ToString(dr.GetValue(5)) != "" ? "SINGAPORE " + Utility.ToString(dr.GetValue(5)) + "" : "");

                    Label1.Text = Utility.ToString(dr.GetValue(0));
                    Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? Utility.ToString(dr.GetValue(1)) : "");
                    Label3.Text = (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                    Label4.Text = (Utility.ToString(dr.GetValue(3)) != "" ? Utility.ToString(dr.GetValue(3)) : "");
                    Label5.Text = (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "");
                    Label6.Text = (Utility.ToString(dr.GetValue(5)) != "" ? Utility.ToString(dr.GetValue(5)) : "");
                }
            }
        }
        
   
        public string GetUrl(object trade, object Project)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(datePickerFrom.SelectedDate);
            int m = dt.Date.Month;
            int d = dt.Date.Day;
            int y = dt.Date.Year;
            string Fromdate = d + "/" + m + "/" + y;

            DateTime dt1 = new DateTime();
            dt1 = Convert.ToDateTime(datePickerTo.SelectedDate);
            int m1 = dt1.Date.Month;
            int d1 = dt1.Date.Day;
            int y1 = dt1.Date.Year;
            string ToDate = d1 + "/" + m1 + "/" + y1;



            //http://localhost/SMEPayroll9.5_VSS_Test/Invoice/InvoiceDetail.aspx?Trade=Driver&FromDate=01/11/2011&Todate=22/11/2011&ProjectId=MS20111072&OrderNo=10009
            string url = "~/Invoice/InvoiceDetail.aspx?Trade=" + trade.ToString() + "&FromDate=" + Fromdate + "&Todate=" + ToDate + "&ProjectId=" + Project.ToString() + "&Client=" + cmbClient.SelectedValue + "";
            // string url = "~/Invoice/InvoiceDetail.aspx";
            return url;
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            CalculatePreview();
            btnConfirm.Enabled = true;

            Session["Invoice"] = Convert.ToString(txtInvoiceNo.Text);

            if (Session["Invoice"] != null)
            {
                string str = @"Invoice_Report.aspx";
                //string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            }

           
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string sqlUpdate = "UPDATE [dbo].[invoice_info] SET [Confirm] ='1' where InvoiceNo='" + Convert.ToString(txtInvoiceNo.Text)+"'";
            DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);

            Response.Redirect("Invoice_Main.aspx");
        }


        #region Common
        private void LoadClienDropdown()
        {
            try
            {

                DataSet ds_Client = new DataSet();
                string sSQLClient = "select ClientID,ClientName from [ClientDetails]where company_id=" + compid + " order by ClientID Desc";
                ds_Client = DataAccess.FetchRS(CommandType.Text, sSQLClient, null);
                cmbClient.DataSource = ds_Client.Tables[0];
                cmbClient.DataTextField = ds_Client.Tables[0].Columns["ClientName"].ColumnName.ToString();
                cmbClient.DataValueField = ds_Client.Tables[0].Columns["ClientID"].ColumnName.ToString();
                cmbClient.DataBind();
            }
            catch
            {
                throw;
            }
        }


        private void Loadpaymentterms()
        {
            try
            {

                DataSet ds_Client = new DataSet();
                string sSQLClient = "SELECT [Ip],[PaymentTerms],[Company_id]  FROM [PaymentTerms] where company_id='"+compid +"' order by 1";
                ds_Client = DataAccess.FetchRS(CommandType.Text, sSQLClient, null);
                cmbpaymentterms.DataSource = ds_Client.Tables[0];
                cmbpaymentterms.DataTextField = ds_Client.Tables[0].Columns["PaymentTerms"].ColumnName.ToString();
                cmbpaymentterms.DataValueField = ds_Client.Tables[0].Columns["Ip"].ColumnName.ToString();
                cmbpaymentterms.DataBind();
            }
            catch
            {
                throw;
            }
        }
      
        #endregion
    }
}
