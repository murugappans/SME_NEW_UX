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

namespace SMEPayroll.Management
{
    public partial class ClaimsAdditions : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            ViewState["actionMessage"] = "";
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            compid = Utility.ToInteger(Session["Compid"]);
            //if (Utility.ToString(Session["Username"]) == "")
            //    Response.Redirect("../SessionExpire.aspx");
            if (!IsPostBack)
            {


                DataSet ds_employee = new DataSet();
                ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name");
                drpemp.DataSource = ds_employee.Tables[0];
                drpemp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                drpemp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                drpemp.DataBind();
                string empid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
                if (empid != "")
                    drpemp.SelectedValue = empid.ToString();
                    cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();
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
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            RadGrid1.DataBind();
        }
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            string sSQL = "";
            Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"];
            string TrxId = id.ToString();
            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            string strtxtbasis_arriving_payment = (userControl.FindControl("txtbasis_arriving_payment") as TextBox).Text;
            string strtxtservice_length = (userControl.FindControl("txtservice_length") as TextBox).Text;
            string strdrpiras_approval = (userControl.FindControl("drpiras_approval") as DropDownList).SelectedItem.Value;
            string strtxtiras_approval_date = (userControl.FindControl("txtiras_approval_date") as TextBox).Text;
            string stradditionyear = (userControl.FindControl("drpAdditionYear") as DropDownList).SelectedItem.Value;

            sSQL = "update emp_additions set additionsforyear='" + stradditionyear + "'  where trx_id=" + TrxId;
            int retsuc = DataAccess.ExecuteStoreProc(sSQL);
            if (retsuc != 1)
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the record"));
                _actionMessage = "Warning|Unable to update the record";
                ViewState["actionMessage"] = _actionMessage;
            }

            char[] delimiterChars = { ',' };
            string[] str = addtype.Split(delimiterChars);
            addtype = str[0];
            sSQL = "sp_empadd_update";
            DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            int i = 0;
            SqlParameter[] parms = new SqlParameter[8];
            parms[i++] = new SqlParameter("@trx_id", Utility.ToInteger(TrxId));
            parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount));
            parms[i++] = new SqlParameter("@trx_type", Utility.ToInteger(addtype));
            parms[i++] = new SqlParameter("@trx_period", Utility.ToString(transperiod1));
            parms[i++] = new SqlParameter("@basis_arriving_payment", Utility.ToString(strtxtbasis_arriving_payment));
            parms[i++] = new SqlParameter("@service_length", Utility.ToString(strtxtservice_length));
            parms[i++] = new SqlParameter("@iras_approval", Utility.ToString(strdrpiras_approval));
            parms[i++] = new SqlParameter("@iras_approval_date", Utility.ToString(strtxtiras_approval_date));
            /* Check Status for Lock Record */
            string sSQLCheck = "select status from emp_additions where trx_id = {0}";
            sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
            string status = "";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
            while (dr.Read())
            {
                status = Utility.ToString(dr.GetValue(0));
            }
            e.Canceled = true;
            if (status == "U")
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                if (retVal == 1)
                {
                    //Response.Write("<script language = 'Javascript'>alert('Information Updated Successfully.');</script>");
                    _actionMessage = "suc|Information Updated Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            else
            {
                //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed. Only Additions in year can be updated.');</script>");
                _actionMessage = "Warning|Payroll has been Processed, Action not allowed. Only Additions in year can be updated.";
                ViewState["actionMessage"] = _actionMessage;
            }

        }
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
            string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            string strtxtbasis_arriving_payment = (userControl.FindControl("txtbasis_arriving_payment") as TextBox).Text;
            string strtxtservice_length = (userControl.FindControl("txtservice_length") as TextBox).Text;
            string strdrpiras_approval = (userControl.FindControl("drpiras_approval") as DropDownList).SelectedItem.Value;
            string strtxtiras_approval_date = (userControl.FindControl("txtiras_approval_date") as TextBox).Text;
            string stradditionyear = (userControl.FindControl("drpAdditionYear") as DropDownList).SelectedItem.Value;
            string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            DateTime transperiod2 = (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate;
            int month = transperiod1.Month;
            int year = transperiod2.Year;
            string clmstatus = "Approved";
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
                int i = 0;
                SqlParameter[] parms = new SqlParameter[12];
                parms[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
                parms[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
                parms[i++] = new SqlParameter("@trx_period1", transperiod1);
                parms[i++] = new SqlParameter("@trx_period2", transperiod2);
                parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount));
                parms[i++] = new SqlParameter("@basis_arriving_payment", Utility.ToString(strtxtbasis_arriving_payment));
                parms[i++] = new SqlParameter("@service_length", Utility.ToString(strtxtservice_length));
                parms[i++] = new SqlParameter("@iras_approval", Utility.ToString(strdrpiras_approval));
                parms[i++] = new SqlParameter("@iras_approval_date", Utility.ToString(strtxtiras_approval_date));
                parms[i++] = new SqlParameter("@additionsforyear", Utility.ToString(stradditionyear));
                parms[i++] = new SqlParameter("@compid", compid);
                parms[i++] = new SqlParameter("@claimstatus", clmstatus);
                string sSQL = "sp_empadd_add";
                e.Canceled = true;
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Unable to add the record.Please try again.";
                        //ErrMsg = "<font color = 'Red'>Unable to add the record.Please try again.</font>";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                    }
                        
                    _actionMessage = "Warning|Unable to add record. Reason:"+ ErrMsg;
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
                Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                string TrxId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                string sSQLCheck = "select status from emp_additions where trx_id = {0}";
                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
                string status = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                while (dr.Read())
                {
                    status = Utility.ToString(dr.GetValue(0));
                }
                if (status == "U")
                {
                    string sSQL = "DELETE FROM emp_additions where trx_id = {0}";
                    sSQL = string.Format(sSQL, Utility.ToInteger(TrxId));
                    int i = DataAccess.ExecuteStoreProc(sSQL);
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
//ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Payroll Additions")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.Item || e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem)
            {
                Telerik.Web.UI.GridEditableItem editedItem = e.Item as Telerik.Web.UI.GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(Telerik.Web.UI.GridEditFormItem.EditFormUserControlID);
                string addtype = e.Item.Cells[6].Text.ToString();
                if (addtype == Session["V1ID"].ToString() || addtype == Session["V2ID"].ToString() || addtype == Session["V3ID"].ToString() || addtype == Session["V4ID"].ToString())
                {
                    if (e.Item.Cells[16].Controls.Count > 0)
                    {
                        e.Item.Cells[16].Controls[0].Visible = false;
                        e.Item.Cells[17].Controls[0].Visible = false;
                    }
                }
            }

        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
           
            if (e.CommandName == RadGrid.PerformInsertCommandName)
            {
                e.Canceled = true;
            }
            if (e.CommandName == "exit")
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
                RadGrid1.Rebind();
            }
        }   

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_employee = new DataSet();
            ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where (termination_date is null or year(termination_date)='" + cmbYear.SelectedValue + "') and company_id=" + compid + " order by emp_name");
            drpemp.DataSource = ds_employee.Tables[0];
            drpemp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            drpemp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            drpemp.DataBind();
        }
    }
}
