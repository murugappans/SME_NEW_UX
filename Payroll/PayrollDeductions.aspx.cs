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
using AuditLibrary;//Added by Jammu Office
using efdata;
using System.Linq;


namespace SMEPayroll.Payroll
{
    public partial class PayrollDeductions : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int LoginEmpcode = 0;//Added by Jammu Office

        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            //SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
               
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();
              
                    string ssql = "sp_emppay_deduc";
                    SqlParameter[] parms = new SqlParameter[3];
                    parms[0] = new SqlParameter("@empcode","0");
                    parms[1] = new SqlParameter("@empmonth", "130");
                    parms[2] = new SqlParameter("@empyear", "2050");
                    RadGrid1.DataSource = DataAccess.ExecuteSPDataSet(ssql, parms);

                RadGrid1.DataBind();
            }


            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                RadGrid1.ExportSettings.FileName = "Payroll_Deductions_Details";
            }

        }
        private object _dataItem = null;
        public object Dataitem
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

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            GridData();
            RadGrid1.DataBind();
        }

        private void GridData()
        {
            //if (Session["emp_code"] != null)
            if (RadComboBoxPrjEmp.SelectedValue.ToString() != "")
            {
                string ssql = "sp_emppay_deduc";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@empcode", Session["emp_code"].ToString());
                parms[1] = new SqlParameter("@empmonth", DropDownList1.SelectedValue);
                parms[2] = new SqlParameter("@empyear", cmbYear.SelectedValue);
                RadGrid1.DataSource = DataAccess.ExecuteSPDataSet(ssql, parms);
            }
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"];
            string TrxId = id.ToString();
            string sSQL = "sp_empdeduc_update";
            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            string amount = (userControl.FindControl("txtamt") as TextBox).Text;

            string strCurrency = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            int converison = 0;

            string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null)
                {
                    converison = 1;
                }
                else
                {
                    if (drcon.GetValue(0).ToString() != "")
                    {
                        converison = Convert.ToInt32(drcon.GetValue(0).ToString());
                    }
                }
            }

            // As per exchange rate change the Amount
            double exchangeRate = 1.0;
            if (converison == 2 || converison == 4)
            {
                string sd, ed = "";

                sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + strCurrency + " and [Date]<='" + sd + "'  Order by [Date] desc";

                SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                while (drex.Read())
                {
                    if (drex.GetValue(0) == null)
                    {
                        exchangeRate = 1.0;
                    }
                    else
                    {
                        exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                    }
                }

                //Response.Write("<script language = 'Javascript'>alert('You can not update Record.Please delete it and add again.');</script>");
                _actionMessage = "Warning|You can not update Record.Please delete it and add again.";
                ViewState["actionMessage"] = _actionMessage;
            }

            else
            {

                DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
                int i = 0;
                SqlParameter[] parms = new SqlParameter[7];
                parms[i++] = new SqlParameter("@trx_id", Utility.ToString(TrxId));
                parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount) * exchangeRate);
                parms[i++] = new SqlParameter("@trx_type", Utility.ToInteger(addtype));
                parms[i++] = new SqlParameter("@trx_period", Utility.ToString(transperiod1));
                parms[i++] = new SqlParameter("@currid", Utility.ToString(strCurrency));
                parms[i++] = new SqlParameter("@ConversionOpt", converison);
                parms[i++] = new SqlParameter("@ExchangeRate", exchangeRate);
                /* Check Status for Lock Record */
                string sSQLCheck = "select status from emp_deductions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    //Added by Jammu Office
                    #region Audit
                    int trxId = Convert.ToInt32(TrxId);
                    var oldrecord = new EmpDeduction();
                    using (var _context = new AuditContext())
                    {
                        oldrecord = _context.EmpDeductions.Where(m => m.TrxId == trxId).FirstOrDefault();
                    }


                    var newrecord = new EmpDeduction
                    {
                        TrxId = trxId,
                        EmpCode = empcode,
                        TrxType = Utility.ToInteger(addtype),
                        TrxPeriod = transperiod1,
                        TrxAmount = Convert.ToDecimal(Utility.ToDouble(amount) * exchangeRate),
                        CurrencyId = Utility.ToInteger(strCurrency),
                        //Claimstatus = clmstatus,
                        ConversionOpt = converison,
                        ExchangeRate = Convert.ToDecimal(exchangeRate),
                    };

                    var AuditRepository = new AuditRepository();
                    AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, trxId, oldrecord, newrecord);
                    #endregion
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal == 1)
                    {
                        //Response.Write("<script language = 'Javascript'>alert('Information Updated Successfully.');</script>");
                        _actionMessage = "Success|Information Updated Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                else
                {
                    //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                    _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }

        }

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            DateTime transperiod2 = (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate;
            string strCurrency = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            //string sSQL1 = "sp_getLockDeduction";
            //SqlParameter[] parms1 = new SqlParameter[6];
            //parms1[0] = new SqlParameter("@month1",Utility.ToInteger(transperiod1.Month));
            //parms1[1] = new SqlParameter("@month2",Utility.ToInteger(transperiod2.Month));
            //parms1[2] = new SqlParameter("@year1", Utility.ToInteger(transperiod1.Year));
            //parms1[3] = new SqlParameter("@year2", Utility.ToInteger(transperiod2.Year));
            //parms1[4] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
            //parms1[5] = new SqlParameter("@compid", compid);

            int converison = 0;

            string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
            SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            while (drcon.Read())
            {
                if (drcon.GetValue(0) == null)
                {
                    converison = 1;
                }
                else
                {
                    if (drcon.GetValue(0).ToString() != "")
                    {
                        if (drcon.GetValue(0).ToString() != "")
                        {
                            converison = Convert.ToInt32(drcon.GetValue(0).ToString());
                        }
                    }
                }
            }


            // As per exchange rate change the Amount
            double exchangeRate = 1.0;
            if (converison == 2 || converison == 4)
            {
                string sd, ed = "";

                sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + strCurrency + " and [Date]<='" + sd + "'  Order by [Date] desc";

                SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                while (drex.Read())
                {
                    if (drex.GetValue(0) == null)
                    {
                        exchangeRate = 1.0;
                    }
                    else
                    {
                        exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                    }
                }
            }

            string sSQL1 = "sp_GetPayrollProcessOn";
            SqlParameter[] parms1 = new SqlParameter[3];
            parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(empcode));
            parms1[1] = new SqlParameter("@compid", compid);
            parms1[2] = new SqlParameter("@trxdate", transperiod1.ToString("dd/MM/yyyy"));
            int conLock = 0;
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
            while (dr1.Read())
            {
                conLock = Utility.ToInteger(dr1.GetValue(0));
            }
            if (conLock <= 0)
            {
                string amount = (userControl.FindControl("txtamt") as TextBox).Text;

                int i = 0;
                SqlParameter[] parms = new SqlParameter[9];
                parms[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
                parms[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
                parms[i++] = new SqlParameter("@trx_period1", transperiod1);
                parms[i++] = new SqlParameter("@trx_period2", transperiod2);
                parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount) * exchangeRate);
                parms[i++] = new SqlParameter("@compid", compid);
                parms[i++] = new SqlParameter("@currid", Utility.ToInteger(strCurrency));
                parms[i++] = new SqlParameter("@ConversionOpt", Utility.ToInteger(converison));
                parms[i++] = new SqlParameter("@ExchangeRate", exchangeRate);
                string sSQL = "sp_empdeduc_add";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new EmpDeduction();

                var newrecord = new EmpDeduction()
                {
                    TrxId = 0,
                    EmpCode = empcode,
                    TrxType = Utility.ToInteger(addtype),
                    TrxPeriod = transperiod1,
                    TrxAmount = Convert.ToDecimal(Utility.ToDouble(amount) * exchangeRate),
                    CurrencyId = Utility.ToInteger(strCurrency),
                    ConversionOpt = converison,
                    ExchangeRate = Convert.ToDecimal(exchangeRate),
                };


                #endregion
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    string sSQLCheck = "SELECT MAX(trx_id) AS LargestId FROM emp_additions ";//Added by Jammu Office
                    int NeRecordId = DataAccess.ExecuteScalar(sSQLCheck, null);//Added by Jammu Office
                    var AuditRepository = new AuditRepository();//Added by Jammu Office
                    AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, NeRecordId, oldrecord, newrecord);//Added by Jammu Office
                    _actionMessage = "Success|Record Added Successfully!!";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Record already exixt";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                    } 
                     _actionMessage = "Warning|Unable to add record. Reason: "+ ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                }

            }
            else
            {
                //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                ViewState["actionMessage"] = _actionMessage;
            }
         
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string TrxId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                string sSQLCheck = "select status from emp_deductions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    string sSQL = "DELETE FROM emp_deductions where trx_id = {0}";
                    sSQL = string.Format(sSQL, TrxId);
                    //Added by Jammu Office
                    #region Audit
                    int trxId = Convert.ToInt32(TrxId);
                    var oldrecord = new EmpDeduction();
                    using (var _context = new AuditContext())
                    {
                        oldrecord = _context.EmpDeductions.Where(m => m.TrxId == trxId).FirstOrDefault();
                    }


                    var newrecord = new EmpDeduction();

                    var AuditRepository = new AuditRepository();
                    AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, trxId, oldrecord, newrecord);
                    #endregion
                    int i = DataAccess.ExecuteStoreProc(sSQL);
                    _actionMessage = "Success|Record Deleted Successfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                    _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " ));
                _actionMessage = "Warning|Unable to delete record. Reason: "+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Payroll Deductions")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                string TrxId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                string sSQLCheck = "select status from emp_deductions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "L")
                {
                    e.Item.Cells[10].Text = "Locked";
                    e.Item.Cells[11].Controls[0].Visible = false;
                    e.Item.Cells[12].Controls[0].Visible = false;
                    
                }
                

            }
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadComboBoxPrjEmp.ClearSelection();
            //DataSet ds_employee = new DataSet();
            //ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where (termination_date is null or year(termination_date)='" + cmbYear.SelectedValue + "') and company_id=" + compid + " order by emp_name");
            //drpemp.DataSource = ds_employee.Tables[0];
            //drpemp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            //drpemp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            //drpemp.DataBind();
        }

        protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlSelectCommand = "";
            RadComboBox rd = new RadComboBox();

            RadGrid1.MasterTableView.ShowGroupFooter = true;
            rd = RadComboBoxPrjEmp;
            //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
            //if (Utility.ToString(Session["GroupName"]) == "Super Admin")
            //{
            //    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
            //}
            //else
            //{
            //    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 and emp_code='" + Session["EmpCode"].ToString() + "' And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
            //}

            //Senthil for Group Management
            if (Utility.ToString(Session["GroupName"]) == "Super Admin")
            {
                sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
            }
            else
            {
                //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 and emp_code='" + Session["EmpCode"].ToString() + "' And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")  And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                if (Utility.GetGroupStatus(Convert.ToInt32(compid)) == 1)
                {

                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE emp_name is not null and (termination_date is null  or termination_date >= getdate()) And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ")  And StatusID=1  ORDER BY [Emp_Name]";

                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE emp_name is not null and (termination_date is null  or termination_date >= getdate()) And Company_ID=" + compid + " And   Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }

            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
            adapter.SelectCommand.Parameters.AddWithValue("@text", e.Text);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = Convert.ToString(dataRow["Emp_Name"]);
                item.Value = Convert.ToString(dataRow["Emp_Code"].ToString());

                string Time_Card_No = Convert.ToString(dataRow["Time_Card_No"]);
                string ic_pp_number = Convert.ToString(dataRow["ic_pp_number"]);

                item.Attributes.Add("Time_Card_No", Time_Card_No.ToString());
                item.Attributes.Add("ic_pp_number", ic_pp_number.ToString());

                rd.Items.Add(item);

                item.DataBind();
            }
        }


        protected void RadComboBoxPrjEmp_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //string selectedValue = e.Value;
            Session["emp_code"] = e.Value;
        }

        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            GridData();

            if (RadComboBoxPrjEmp.SelectedValue.ToString() != "")
            {
                GridData();
            }
            else
            {

                string ssql = "sp_emppay_deduc";
                SqlParameter[] parms = new SqlParameter[3];
                parms[0] = new SqlParameter("@empcode", "0");
                parms[1] = new SqlParameter("@empmonth", "130");
                parms[2] = new SqlParameter("@empyear", "2050");
                RadGrid1.DataSource = DataAccess.ExecuteSPDataSet(ssql, parms);

            }
        }

        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;// UniqueName="DeleteColumn"
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("101", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e, tbRecord);

        }



        #endregion
        //Toolbar End
    }
}
