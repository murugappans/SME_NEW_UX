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
    public partial class AddEditQuotation : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        public string Prefix;
        public string OrderPrefix;
        public int QuotationStartNumber, OrderStartNumber;
        //public string Prefix = @"ABN\Sg\";
        //public string Prefix = System.Configuration.ConfigurationManager.AppSettings["QUOTATION_PREFIX"];
        //public string OrderPrefix = @"ORD\Sg\";
        //public string OrderPrefix = System.Configuration.ConfigurationManager.AppSettings["ORDER_PREFIX"];
        public string QuotationNo;
        public  int compid;
        public int lastQuotNo;

        public int lastOrdertNo;
       

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource_Var.ConnectionString = Session["ConString"].ToString();
            SqlDataSource_VarPref.ConnectionString = Session["ConString"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);

            #region Get Prefix and StartNumber

            string sql = "SELECT [SID],[Prefix],[StartNumber]  FROM [StartNumber] where [Desc]='Quotation' AND Company_Id='" + compid + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                Prefix = dr.GetString(dr.GetOrdinal("Prefix"));
                QuotationStartNumber = dr.GetInt32(dr.GetOrdinal("StartNumber"));

            }

            string sql_ord = "SELECT [SID],[Prefix],[StartNumber]  FROM [StartNumber] where [Desc]='Order' AND Company_Id='" + compid + "'";
            SqlDataReader dr_ord = DataAccess.ExecuteReader(CommandType.Text, sql_ord, null);
            if (dr_ord.Read())
            {
                OrderPrefix = dr_ord.GetString(dr_ord.GetOrdinal("Prefix"));
                OrderStartNumber = dr_ord.GetInt32(dr_ord.GetOrdinal("StartNumber"));

            }

            #endregion



            QuotationNo = Request.QueryString["Quotation"];
            if (!IsPostBack)
            {
                
                LoadClienDropdown();
                
                if (QuotationNo == null)//Create New Quotation
                {
                    CreateNewQuotation();
                }
                else
                {
                    OpenExistingQuotation(QuotationNo);
                }

               
                btnConvertOrder.Enabled = false;//disable the ConverOrder button- enable it after save
                btnPrint.Enabled = false;
                Project_Dropdown();
                LoadAddressForClient();
            }

            if (QuotationNo != null)//Create New Quotation
            {
                OpenExistingQuotation(QuotationNo);
            }
            lblError.Text = "";

        }

       


        protected void drpClient_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            LoadAddressForClient();

            Project_Dropdown(); 
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
                    //Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ?  "BLOCK " +Utility.ToString(dr.GetValue(1))+ "": "");
                    //Label3.Text = (Utility.ToString(dr.GetValue(1)) != "" ? "# " + Utility.ToString(dr.GetValue(3)) + "-" : "") + (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "")+" " + (Utility.ToString(dr.GetValue(2)) != "" ?  Utility.ToString(dr.GetValue(2))  : "");
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
        #endregion

        #region Create New Quotation
        private void CreateNewQuotation()
            {
               try
                {
                    DateTime date = DateTime.Now;
                    date = Convert.ToDateTime(date);
                    datePickerId.SelectedDate = date;

                    string SQLLastQuotationNo = " select  Top 1 QuotationNo  from [quotation_info] where company_id='"+compid+"' ORDER BY IId DESC";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLLastQuotationNo, null);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lastQuotNo = Utility.ToInteger(dr.GetValue(0));
                        }
                    }
                    else
                    {
                        //lastQuotNo = 10000;
                        //lastQuotNo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QUOTATION_START_NUMBER"]);
                        lastQuotNo = QuotationStartNumber;
                    }
                    lastQuotNo = lastQuotNo + 1;

                    //Inserting new quotation No
                    string ssqlb = "INSERT INTO [dbo].[Quotation_Info]([Prefix],[QuotationNo],[ClientId],[CreatedDate],[Remark],[SalesRep],[company_id])VALUES('" + Prefix + "'," + lastQuotNo + ",'',getdate(),'','',"+compid+")";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    lblQuot.Text = Prefix + Convert.ToSingle(lastQuotNo);
                    hdnQuot.Value = Convert.ToString(lastQuotNo);

                    LoadQuotationFromPreference();
                }
                catch
                {
                    throw;
                }
            }

        protected void LoadQuotationFromPreference()
        {
            string sql = "SELECT [RId],[Text] FROM [ReportText] where  [Desc] = 'Quotation' AND  [Company_Id]='" + compid + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                NewsEditor.Content = dr.GetString(dr.GetOrdinal("Text"));

            }
        }
          
        #endregion

        #region Open Existing Quotation
        private void OpenExistingQuotation(string QuotationNo)
            {
                try
                {
                    string sSQLQuot = "SELECT [Prefix],[QuotationNo],[ClientId],[CreatedDate],[Remark],[SalesRep],[Project],[Text] FROM [dbo].[Quotation_Info] where [QuotationNo]='" + QuotationNo + "' and [company_id]=" + compid + "";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLQuot, null);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lblQuot.Text =(Utility.ToString(dr.GetValue(0)))+(Utility.ToString(dr.GetValue(1)));
                                hdnQuot.Value = (Utility.ToString(dr.GetValue(1)));
                                if (Utility.ToInteger(dr.GetValue(2)) > 0)
                                {
                                    cmbClient.SelectedValue = Utility.ToString(dr.GetValue(2));
                                   
                                }
                                datePickerId.SelectedDate = (DateTime)dr.GetValue(3);
                                txtRemarks.Text = (Utility.ToString(dr.GetValue(4)) != null ? Utility.ToString(dr.GetValue(4)) : "");
                                txtSalesRep.Text = (Utility.ToString(dr.GetValue(5)) != null ? Utility.ToString(dr.GetValue(5)) : "");


                                string s = (Utility.ToString(dr.GetValue(6)) != null ? Utility.ToString(dr.GetValue(6)) : "");
                               //drpProject.SelectedValue = (Utility.ToString(dr.GetValue(6)) != null ? Utility.ToString(dr.GetValue(6)) : "");
                                cmbProject.SelectedValue = s;

                                //if (dr.GetValue(7).ToString() != "")
                                {
                                    NewsEditor.Content = dr.GetValue(7).ToString();
                                }
                                //else
                                //{
                                //    LoadQuotationFromPreference();
                                //}

                            }
                        }
                    
                }
                catch
                {
                    throw;
                }
            }


        #endregion
        
        #region RadGrid1
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                string Trade1 = (userControl.FindControl("drpTrade") as DropDownList).SelectedValue.ToString();
                int Trade = Convert.ToInt32(Trade1);


                double NH, OT1, OT2, Accom;
                if ((userControl.FindControl("txtNH") as TextBox).Text != null)
                {
                    NH = Convert.ToDouble((userControl.FindControl("txtNH") as TextBox).Text);
                }
                else
                {
                    NH = 0;
                }
                if ((userControl.FindControl("txtOT1") as TextBox).Text != "")
                {
                    OT1 = Convert.ToDouble((userControl.FindControl("txtOT1") as TextBox).Text);
                }
                else
                {
                    OT1 = 0;
                }
                if ((userControl.FindControl("txtOT2") as TextBox).Text != "")
                {
                    OT2 = Convert.ToDouble((userControl.FindControl("txtOT2") as TextBox).Text);
                }
                else
                {
                    OT2 = 0;
                }
               
                //Validate if the Trade already Exist
                DataSet ds_trade = new DataSet();
                string QuotationNo = Request.QueryString["Quotation"];
                string sSQL = "select * from Quoationmaster_hourly where Trade='" + Trade + "' AND  company_id='" + compid + "' AND QuotationNo='" + QuotationNo + "'";
                ds_trade = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                if (ds_trade.Tables[0].Rows.Count > 0)
                //
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Trade Exist already</font> "));
                    _actionMessage = "Warning|Trade Exist already";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    string ssqlb = "INSERT INTO [QuoationMaster_hourly]([QuotationNo],[Trade],[NH],[OT1],[OT2],[company_id])VALUES('" + hdnQuot.Value + "','" + Trade + "','" + NH + "','" + OT1 + "','" + OT2 + "','" + compid + "')";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Inserted Sucessfully</font> "));
                    _actionMessage = "success|Inserted Sucessfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
               
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Insert record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to Insert record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {

                GridEditableItem editedItem = e.Item as GridEditableItem;
                string QuotNo = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Qid"]);
                //string sSQLCheck = "select QuotationNo from QuoationMaster_hourly where [company_id]='"+compid+"' AND QuotationNo = {0}";
                //sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(QuotNo));
                //string status = "";
                //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                //while (dr.Read())
                //{
                //    status = Utility.ToString(dr.GetValue(0));
                //}
                //if (status != null)
                //{
                    string sSQL = "DELETE FROM QuoationMaster_hourly where [company_id]='" + compid + "' AND Qid = {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(QuotNo));
                    int i = DataAccess.ExecuteStoreProc(sSQL);
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Deleted Sucessfully</font> "));
                _actionMessage = "success|>Deleted Sucessfully";
                ViewState["actionMessage"] = _actionMessage;

                //}
                //else
                //{
                //    Response.Write("<script language = 'Javascript'>alert('Action not allowed.');</script>");
                //}

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> "  ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:" +ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                string Trade1 = (userControl.FindControl("drpTrade") as DropDownList).SelectedValue.ToString();
                int Trade = Convert.ToInt32(Trade1);


                double NH, OT1, OT2, Accom;
                if ((userControl.FindControl("txtNH") as TextBox).Text != null)
                {
                    NH = Convert.ToDouble((userControl.FindControl("txtNH") as TextBox).Text);
                }
                else
                {
                    NH = 0;
                }
                if ((userControl.FindControl("txtOT1") as TextBox).Text != "")
                {
                    OT1 = Convert.ToDouble((userControl.FindControl("txtOT1") as TextBox).Text);
                }
                else
                {
                    OT1 = 0;
                }
                if ((userControl.FindControl("txtOT2") as TextBox).Text != "")
                {
                    OT2 = Convert.ToDouble((userControl.FindControl("txtOT2") as TextBox).Text);
                }
                else
                {
                    OT2 = 0;
                }

                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Qid"];
                int QuotId = Convert.ToInt32(id);
              
                string ssql_Update = "UPDATE [QuoationMaster_hourly] SET [Trade] ='" + Trade + "',[NH] ='" + NH + "',[OT1] ='" + OT1 + "',[OT2] ='" + OT2 + "' where [Qid] ='" + QuotId + "' AND [company_id]='" + compid + "' ";
                DataAccess.FetchRS(CommandType.Text, ssql_Update, null);
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'></font> "));
                _actionMessage = "success|Updated Sucessfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Update record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to Update record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
           
        }
        #endregion


        #region RadGrid2
            protected void RadGrid2_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
            {
                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                    string Trade1 = (userControl.FindControl("drpTrade") as DropDownList).SelectedValue.ToString();
                    int Trade = Convert.ToInt32(Trade1);


                    double WorkingDays, OT1, OT2, Monthly, DailyRate;
                   

                    string drpEmpVal = (userControl.FindControl("drpEmp") as DropDownList).SelectedValue.ToString();
                    int drpEmp = Convert.ToInt32(drpEmpVal);
                    //Monthly
                    if ((userControl.FindControl("txtMonthly") as TextBox).Text != null)
                    {
                        Monthly = Convert.ToDouble((userControl.FindControl("txtMonthly") as TextBox).Text);
                    }
                    else
                    {
                        Monthly = 0;
                    }


                    //Working Days
                    if ((userControl.FindControl("drpWorkingdaysWeek") as DropDownList).SelectedValue != null)
                    {
                        WorkingDays = Convert.ToDouble((userControl.FindControl("drpWorkingdaysWeek") as DropDownList).SelectedValue);
                    }
                    else
                    {
                        WorkingDays = 0;
                    }

                    //DailyRate
                    if ((userControl.FindControl("txtdailyRate_hid") as HiddenField).Value != "")
                    {
                        DailyRate = Convert.ToDouble((userControl.FindControl("txtdailyRate_hid") as HiddenField).Value);
                    }
                    else
                    {
                        DailyRate = 0;
                    }
                   
                    //OT1
                    if ((userControl.FindControl("txtOT1_M") as TextBox).Text != "")
                    {
                        OT1 = Convert.ToDouble((userControl.FindControl("txtOT1_M") as TextBox).Text);
                    }
                    else
                    {
                        OT1 = 0;
                    }

                    //OT2
                    if ((userControl.FindControl("txtOT2_M") as TextBox).Text != "")
                    {
                        OT2 = Convert.ToDouble((userControl.FindControl("txtOT2_M") as TextBox).Text);
                    }
                    else
                    {
                        OT2 = 0;
                    }

                     //Validate if the Trade already Exist
                    DataSet ds_emp = new DataSet();
                    string QuotationNo = Request.QueryString["Quotation"];
                    string sSQL = "select * from Quoationmaster_Monthly where EmpCode='" + drpEmp + "' and company_Id='" + compid + "' and QuotationNo='" + QuotationNo + "'";
                    ds_emp = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                    if (ds_emp.Tables[0].Rows.Count > 0)
                    //
                    {
                        //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Employee Exist Already</font> "));
                    _actionMessage = "Warning|Employee Exist Already";
                    ViewState["actionMessage"] = _actionMessage;
                }
                    else
                    {
                        string ssqlb = "INSERT INTO [QuoationMaster_Monthly]([QuotationNo],[Trade],[EmpCode],[Monthly],[Workingdays],[DailyRate],[OT1],[OT2],[company_Id])VALUES('" + hdnQuot.Value + "','" + Trade + "','" + drpEmp + "','" + Monthly + "' ,'" + WorkingDays + "','" + DailyRate + "','" + OT1 + "' ,'" + OT2 + "','" + compid +"')";
                        DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Inserted Sucessfully</font> "));
                    _actionMessage = "Warning|Inserted Sucessfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
                  }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Insert record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to Insert record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
                }
            }
            protected void RadGrid2_DeleteCommand(object source, GridCommandEventArgs e)
            {
                try
                {

                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string QuotNo = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["HId"]);
                    //string sSQLCheck = "select QuotationNo from QuoationMaster_Monthly where QuotationNo = {0}";
                    //sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(QuotNo));
                    //string status = "";
                    //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                    //while (dr.Read())
                    //{
                    //    status = Utility.ToString(dr.GetValue(0));
                    //}
                    //if (status != null)
                    //{
                        string sSQL = "DELETE FROM QuoationMaster_Monthly where HId = {0}";
                        sSQL = string.Format(sSQL, Utility.ToInteger(QuotNo));
                        int i = DataAccess.ExecuteStoreProc(sSQL);
                        //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Deleted Sucessfully</font> "));
                _actionMessage = "Warning|Deleted Sucessfully";
                ViewState["actionMessage"] = _actionMessage;
                //}
                //else
                //{
                //    Response.Write("<script language = 'Javascript'>alert('Action not allowed.');</script>");
                //}

            }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                    //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
                }
            }
            protected void RadGrid2_UpdateCommand(object source, GridCommandEventArgs e)
                {
                    try
                    {
                        GridEditableItem editedItem = e.Item as GridEditableItem;
                        UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                        string Trade1 = (userControl.FindControl("drpTrade") as DropDownList).SelectedValue.ToString();
                        int Trade = Convert.ToInt32(Trade1);

                        

                        double WorkingDays, OT1, OT2, Monthly, DailyRate;


                        string drpEmpVal = (userControl.FindControl("drpEmp") as DropDownList).SelectedValue.ToString();
                        int drpEmp = Convert.ToInt32(drpEmpVal);

                        //Monthly
                        if ((userControl.FindControl("txtMonthly") as TextBox).Text != null)
                        {
                            Monthly = Convert.ToDouble((userControl.FindControl("txtMonthly") as TextBox).Text);
                        }
                        else
                        {
                            Monthly = 0;
                        }


                        //Working Days
                        if ((userControl.FindControl("drpWorkingdaysWeek") as DropDownList).SelectedValue != null)
                        {
                            WorkingDays = Convert.ToDouble((userControl.FindControl("drpWorkingdaysWeek") as DropDownList).SelectedValue);
                        }
                        else
                        {
                            WorkingDays = 0;
                        }

                        //DailyRate
                        if ((userControl.FindControl("txtdailyRate_hid") as HiddenField).Value != "")
                        {
                            DailyRate = Convert.ToDouble((userControl.FindControl("txtdailyRate_hid") as HiddenField).Value);
                        }
                        else
                        {
                            DailyRate = 0;
                        }

                        //OT1
                        if ((userControl.FindControl("txtOT1_M") as TextBox).Text != "")
                        {
                            OT1 = Convert.ToDouble((userControl.FindControl("txtOT1_M") as TextBox).Text);
                        }
                        else
                        {
                            OT1 = 0;
                        }

                        //OT2
                        if ((userControl.FindControl("txtOT2_M") as TextBox).Text != "")
                        {
                            OT2 = Convert.ToDouble((userControl.FindControl("txtOT2_M") as TextBox).Text);
                        }
                        else
                        {
                            OT2 = 0;
                        }

                   

                        object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["HId"];
                        int Hid = Convert.ToInt32(id);
              
                        string ssql_Update = "UPDATE [QuoationMaster_Monthly] SET [Trade] ='" + Trade + "' ,[EmpCode] ='" + drpEmp + "' ,[Monthly] ='" + Monthly + "' ,[Workingdays] ='" + WorkingDays + "' ,[DailyRate] ='" + DailyRate + "' ,[OT1] ='" + OT1 + "' ,[OT2] ='" + OT2 + "',[company_Id]='" + compid + "'  WHERE [HId] =" + Hid + "";
                        DataAccess.FetchRS(CommandType.Text, ssql_Update, null);
                        //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Updated Sucessfully</font> "));
                _actionMessage = "success|Updated Sucessfully";
                ViewState["actionMessage"] = _actionMessage;
            }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        //RadGrid2.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Update record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to Update record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
                    }
                }

            protected void RadGrid2_ItemCommand(object source, GridCommandEventArgs e)
            {
                Session["TradeId"] = "";
                if (e.CommandName == "Edit")
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string TradeId = editedItem["TradeID"].Text;
                    Session["TradeId"] = TradeId;
                   
                }
            }
               
        #endregion 

        #region Save
        protected void btnSave_Click(object sender, EventArgs e)
            {
                //btnSave.Enabled = false;
                UpdateQuotation_Info();

            }

        private void UpdateQuotation_Info()
        {
           
            try
            {
                DateTime dt = new DateTime();
                dt = Convert.ToDateTime(datePickerId.SelectedDate);
                //string sqlUpdate = "UPDATE [dbo].[Quotation_Info] SET [ClientId] =" + drpClient.SelectedValue + " ,[CreatedDate] ='" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "',[Remark] ='" + txtRemarks.Text + "',[SalesRep] ='" + txtSalesRep.Text + "',[Project]='"+drpProject.SelectedValue +"'   where QuotationNo=" + Convert.ToInt32(hdnQuot.Value) + "";
                string sqlUpdate = "UPDATE [dbo].[Quotation_Info] SET [ClientId] =" + cmbClient.SelectedValue + " ,[CreatedDate] ='" + dt.Date.Month + "/" + dt.Date.Day + "/" + dt.Date.Year + "',[Remark] ='" + txtRemarks.Text + "',[SalesRep] ='" + txtSalesRep.Text + "',[Project]='" + cmbProject.SelectedValue + "',[Text]='" + NewsEditor.Content.Replace("'","") + "'   where QuotationNo=" + Convert.ToInt32(hdnQuot.Value) + "";
                DataAccess.FetchRS(CommandType.Text, sqlUpdate, null);
                //lblError.Text = "Quotation Updated Successfully";
                _actionMessage = "success|Quotation Updated Successfully" ;
                ViewState["actionMessage"] = _actionMessage;

                btnConvertOrder.Enabled = true;
                btnPrint.Enabled = true;
            }
            catch
            {
                throw;
            }
            
        }


        #endregion

        #region Order
           
            protected void btnConvertOrder_Click(object sender, EventArgs e)
            {

                        //Getting New orderNo
                        int OrderNo = CreateNewOrder();


                        //inserting Quot in Order Table
                        try
                        {
                            SqlParameter[] parms1 = new SqlParameter[3];
                            parms1[0] = new SqlParameter("@OrderNo", Utility.ToInteger(OrderNo));
                            parms1[1] = new SqlParameter("@Prefix", Utility.ToString(OrderPrefix));
                            parms1[2] = new SqlParameter("@QuotNo", Utility.ToInteger(hdnQuot.Value));

                            DataAccess.ExecuteStoreProc("sp_invoice_order_insert", parms1);
                            Response.Redirect("ConvertOrder.aspx?From=" + QuotationNo + "&orderNo=" + OrderNo + "");
                        }
                        catch
                        {
                            throw;
                        }
                  

            }
            private int CreateNewOrder()
            {
                try
                {

                    string SQLLastOrderNo = "select  Top 1 OrderNo  from [Order_Info] where company_id='"+compid+"' ORDER BY Oid DESC";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLLastOrderNo, null);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lastOrdertNo = Utility.ToInteger(dr.GetValue(0));
                        }
                    }
                    else
                    {
                       //lastOrdertNo = 10000;
                       // lastOrdertNo = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ORDER_START_NUMBER"]);
                        lastOrdertNo = OrderStartNumber;
                    }
                    lastOrdertNo = lastOrdertNo + 1;

                    //lblorder.Text = Prefix + Convert.ToSingle(lastOrdertNo);
                    //hdnOrder.Value = Convert.ToString(lastOrdertNo);
                    return lastOrdertNo;
                }
                catch
                {
                    throw;
                }
              }



           
            #endregion


        protected void Project_Dropdown()
        {

            if (cmbClient.SelectedValue != "-1")
            {

                DataSet ds_Pro = new DataSet();
                string sSQL = "Select SP.ID ,SP.Sub_Project_ID as Sub_Project_Id ,SP.Sub_Project_Name as Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=" + compid.ToString() + "  OR LO.isShared='YES') AND SP.Active=1 AND SP.InvoiceClientId='"+cmbClient.SelectedValue+"'";
                ds_Pro = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                cmbProject.DataSource = ds_Pro.Tables[0];
                cmbProject.DataTextField = ds_Pro.Tables[0].Columns["Sub_Project_Name"].ColumnName.ToString();
                cmbProject.DataValueField = ds_Pro.Tables[0].Columns["Sub_Project_Id"].ColumnName.ToString();
                cmbProject.DataBind();
            }

        }


        protected void btnPrint_Click(object sender, EventArgs e)
        {


            if (hdnQuot.Value != null)
            {
                Session["Quotation"] = hdnQuot.Value;

                string str = @"QuotationReport.aspx";
                //string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            }
        }



        


        protected void RadGrid3_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor editor_ClientName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Amount");

                editor_ClientName.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Client('" + editor_ClientName.TextBoxControl.ClientID + "')");

            }
        }


       #region Grid3
       protected void RadGrid3_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                {
                    ErrMsg = "Please Enter Amount";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }

                if (e.Exception.Message.Contains("Input string was not in a correct format"))
                {
                    ErrMsg = "Please Enter Integer value";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|"+ ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }
                 
            }
            else
            {
                //DisplayMessage("variable added successfully.");
                _actionMessage = "success|variable added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid3_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Amount";
                //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;

                if (e.Exception.Message.Contains("Input string was not in a correct format"))
                {
                    ErrMsg = "Please Enter Integer value";
                    //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                    _actionMessage = "Warning|"+ ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
            else
            {
                //DisplayMessage("variable updated successfully.");
                _actionMessage = "successning|variable updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
          //  RadGrid3.Controls.Add(new LiteralControl("<font color = 'red'>"+text+".</font>"));
        }
        #endregion



    }
}
