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
    public partial class DeductionType : System.Web.UI.Page
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
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            /* To disable Grid filtering options  */
            compid = Utility.ToInteger(Session["Compid"].ToString());
            GridFilterMenu menu = RadGrid1.FilterMenu;
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }            
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Display = false;

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Deductions")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
            else
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["company_id"]);
                    string desc = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["desc"]);
                    if (strSystemDefined == "-1")
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        dataItem.Cells[7].Controls[0].Visible = false;
                        dataItem.Cells[8].Controls[0].Visible = false;
                    }
                    if (desc == "Lateness")
                    {
                        GridDataItem dataItem1 = e.Item as GridDataItem;
                        // RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false; //muru

                        //dataItem.Cells[7].Controls[0].Visible = false;
                        // dataItem1.Cells[10].Controls[0].Visible = false;
                        //dataItem1.Cells[dataItem1.ItemIndex].Controls[0].Visible = false;
                    }
                    //else
                    //{
                    //   // RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = true;
                    //}
                }
            }
        }

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string cpf = (userControl.FindControl("drpcpf") as DropDownList).SelectedItem.Value;
            string isshared = (userControl.FindControl("drpShared") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("txtaddtype") as TextBox).Text;
            string accountcode = (userControl.FindControl("txtAccountCode") as TextBox).Text;
         
            string Formula = (userControl.FindControl("drpfor") as DropDownList).SelectedItem.Value;
            string prorated = (userControl.FindControl("ProratedDrop") as DropDownList).SelectedItem.Value;
            string FormulaValue = (userControl.FindControl("txtFormulaCalc") as TextBox).Text;
            DropDownList drpleaveDeductList = (userControl.FindControl("leaveDeductList") as DropDownList);
            DropDownList drpSDL = (userControl.FindControl("SDLDropDownList") as DropDownList);
            DropDownList drpFundDeductList = (userControl.FindControl("FundDropDownList") as DropDownList);
            DropDownList ddlActive = (userControl.FindControl("ddlActive") as DropDownList);
            //validation to check whether the addition and deduction with same name exist
            string SQLLastOrderNo = "select [desc] from [deductions_types] where [desc]='" + addtype + "' and company_id='" + compid + "' union select [desc] from [additions_types] where [desc]='" + addtype + "'and company_id='" + compid + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLLastOrderNo, null);
            if (dr.HasRows)
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Deduction Type Exist Already in Detection or Addition</font> "));
                _actionMessage = "Warning|Deduction Type Already exists";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
            //

                //validation -to check if '% of Gross' is selected then there should be value
                if (Formula == "1" && FormulaValue =="")
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please Enter Formula Value</font> "));
                    _actionMessage = "Warning|Please Enter Formula Value";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
                //


                SqlParameter[] parms = new SqlParameter[13];
                parms[0] = new SqlParameter("@desc", Utility.ToString(addtype));
                parms[1] = new SqlParameter("@cpf", Utility.ToString(cpf));
                parms[2] = new SqlParameter("@company_id", compid);
                parms[3] = new SqlParameter("@typeshared", isshared);
                parms[4] = new SqlParameter("@accountcode", accountcode);

                parms[5] = new SqlParameter("@Formula", Convert.ToInt32(Formula));
                if (FormulaValue == "")
                {
                    FormulaValue = "0";
                }
                parms[6] = new SqlParameter("@FormulaValue",Convert.ToDecimal(FormulaValue));


                //tax deduction
                string tax = (userControl.FindControl("drpTax") as DropDownList).SelectedItem.Value;
                parms[7] = new SqlParameter("@Tax", Utility.ToString(tax));
                parms[8] = new SqlParameter("@Prorated", Utility.ToString(prorated));
                parms[9] = new SqlParameter("@leaveDeduct", Convert.ToInt32(drpleaveDeductList.SelectedItem.Value.ToString()));
                parms[10] = new SqlParameter("@IsFund", Convert.ToInt32(drpFundDeductList.SelectedItem.Value.ToString()));
                parms[11] = new SqlParameter("@IsSDL", Convert.ToInt32(drpSDL.SelectedItem.Value.ToString()));
                parms[12] = new SqlParameter("@Active", Convert.ToInt32(ddlActive.SelectedItem.Value.ToString()));
                string sSQL = "sp_dedtype_add";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    _actionMessage = "Success|Deduction Type Added Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }

                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Deduction Type already Exists.";
                    }
                        //ErrMsg = "<font color = 'Red'>Deduction Type already Exists.</font>";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add Deduction Type. Reason:</font> " + ErrMsg)); 
                        _actionMessage = "Warning|" + ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;

                  
                }
            }
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string cpf = (userControl.FindControl("drpcpf") as DropDownList).SelectedItem.Value;
            string isshared = (userControl.FindControl("drpShared") as DropDownList).SelectedItem.Value;
            string addtype = (userControl.FindControl("txtaddtype") as TextBox).Text;
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];
            string accountcode = (userControl.FindControl("txtAccountCode") as TextBox).Text;
            string Prorated = (userControl.FindControl("ProratedDrop") as DropDownList).SelectedItem.Value;
            string Formula = (userControl.FindControl("drpfor") as DropDownList).SelectedItem.Value;
            string FormulaValue = (userControl.FindControl("txtFormulaCalc") as TextBox).Text;
            DropDownList drpleaveDeductList = (userControl.FindControl("leaveDeductList") as DropDownList);
            DropDownList drpSDL = (userControl.FindControl("SDLDropDownList") as DropDownList);
            DropDownList drpFundDeductList = (userControl.FindControl("FundDropDownList") as DropDownList);
            DropDownList ddlActive = (userControl.FindControl("ddlActive") as DropDownList);
            //validation to check whether the addition and deduction with same name exist
            //string SQLLastOrderNo = "select [desc] from [deductions_types] where [desc]='" + addtype + "' and company_id='" + compid + "' union select [desc] from [additions_types] where [desc]='" + addtype + "'and company_id='" + compid + "'";
            string SQLLastOrderNo = "select [desc] from [deductions_types] where [desc]='" + addtype + "' and company_id='" + compid + "' and id!='" + id + "' union select [desc] from [additions_types] where [desc]='" + addtype + "'and company_id='" + compid + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLLastOrderNo, null);
            if (dr.HasRows)
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Deduction Type Exist Already in Detection or Addition</font> "));
                _actionMessage = "Warning|Deduction Type Already Exists";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
             //
            
                //validation -to check if '% of Gross' is selected then there should be value
                if (Formula == "1" && FormulaValue == "")
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please Enter Formula Value</font> "));
                    _actionMessage = "Warning|Please Enter Formula Value";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
                //


                if (FormulaValue == "")
                {
                    FormulaValue = "0.00";
                }
              


                SqlParameter[] parms = new SqlParameter[13];
                parms[0] = new SqlParameter("@desc", Utility.ToString(addtype));
                parms[1] = new SqlParameter("@cpf", Utility.ToString(cpf));
                parms[2] = new SqlParameter("@id", id);
                parms[3] = new SqlParameter("@typeshared", isshared);
                parms[4] = new SqlParameter("@accountcode", accountcode);

                parms[5] = new SqlParameter("@Formula", Formula);
                parms[6] = new SqlParameter("@FormulaValue", Convert.ToDecimal(FormulaValue));

                //tax deduction
                string tax = (userControl.FindControl("drpTax") as DropDownList).SelectedItem.Value;
                parms[7] = new SqlParameter("@Tax", Utility.ToString(tax));
                parms[8] = new SqlParameter("@Prorated", Utility.ToString(Prorated));
                parms[9] = new SqlParameter("@LeaveDeduct", Convert.ToInt32(drpleaveDeductList.SelectedItem.Value.ToString()));
                parms[10] = new SqlParameter("@IsSDL", Convert.ToInt32(drpSDL.SelectedItem.Value.ToString()));
                parms[11] = new SqlParameter("@IsFund", Convert.ToInt32(drpFundDeductList.SelectedItem.Value.ToString()));
                parms[12] = new SqlParameter("@Active", Convert.ToInt32(ddlActive.SelectedItem.Value.ToString()));
                string sSQL = "sp_dedtype_update";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    _actionMessage = "Success|Deduction Type Updated Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        ErrMsg = "Deduction Type already Exists.";
                        //ErrMsg = "<font color = 'Red'>Deduction Type already Exists.</font>";
                    }
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update Deduction Type. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Unable to update Deduction Type. Reason:"+ ErrMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                   
                }
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_deductions where trx_type=" + id, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the deduction type.This deduction type is in use."));
                        _actionMessage = "Warning|Unable to delete the deduction type.This deduction type is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {                        
                            string sSQL = "DELETE FROM [deductions_types] WHERE [id] =" + id;

                            int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Deduction type is Deleted Successfully."));
                            _actionMessage = "Success|Deduction type Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the deduction type."));
                            _actionMessage = "Warning|Unable to delete the deduction type.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        }

                    }
                }
            
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
  //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                 _actionMessage = "Warning|"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                    //DisplayMessage("<font color = 'red'> Deduction Type can not be added. Reason: Deduction Type already Exists.</font>");
                {
                    _actionMessage = "Warning|Deduction Type can not be added. Reason: Deduction Type already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    _actionMessage = "Warning|Deduction Type can not be added. Reason:"+ e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> Deduction Type can not be added. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                //DisplayMessage("Deduction Type added successfully.");
                _actionMessage = "Success|Deduction Type added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                {
                    _actionMessage = "Warning|Deduction Type can not be updated. Reason: Deduction Type already Exists.";
                    ViewState["actionMessage"] = _actionMessage;
                }

                    //DisplayMessage("<font color = 'red'> Deduction Type can not be updated. Reason: Deduction Type already Exists.</font>");
                else
                {
                    _actionMessage = "Warning|Deduction Type can not be updated. Reason:"+ e.Exception.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }
                    //DisplayMessage("<font color = 'red'> Deduction Type can not be updated. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                //DisplayMessage("Deduction Type updated successfully.");
                _actionMessage = "Success|Deduction Type updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.DeductionDetails;
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet DeductionDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "";
                int compID = Utility.ToInteger(Session["Compid"].ToString());
                if (compID == 1)
                    sSQL = "SELECT  Replace ([desc],'''' ,'') [desc], [id],cpf,company_id,isShared,AccountCode,Formula,FormulaValue,Tax,Prorated,LeaveDeduct,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,IsFund=case when (cast(IsFund as bit)=1)then 'Yes' Else 'No' End,[Active] =case when (cast([Active] as int)=1 or [Active] is null ) then 'Yes' Else 'No' End FROM [deductions_types] where company_id= " + compID + " Order by [Desc]";
                else
                    sSQL = "SELECT  Replace ([desc],'''' ,'') [desc], [id],cpf,company_id,isShared,AccountCode,Formula,FormulaValue,Tax,Prorated,LeaveDeduct,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,IsFund=case when (cast(IsFund as bit)=1) then 'Yes' Else 'No' End,[Active] =case when (cast([Active] as int)=1 or [Active] is null ) then 'Yes' Else 'No' End FROM [deductions_types] where company_id= " + compID + "  Order by [Desc]";

                ds = GetDataSet(sSQL);
                return ds;
            }
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("107", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
    }
}
