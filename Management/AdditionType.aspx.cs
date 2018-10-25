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
    public partial class AdditionType : System.Web.UI.Page
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
            compid = Utility.ToInteger(Session["Compid"].ToString());
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
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
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet AdditionDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "";
                int compID = Utility.ToInteger(Session["Compid"].ToString());
                if (compID == 1)
                    //sSQL = "SELECT  Replace ([desc],'''' ,'') [desc], [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options, TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash FROM [additions_types] where company_id= " + compID + " or company_id=-1  Order by [OptionSelection],[Desc]";
                    // sSQL = "SELECT  Replace ([desc],'''' ,'') [desc],additions_types.id [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options, TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash=case when (cast(ClaimCash as int)=2) then 'Yes' Else 'No' End,[Used],CONVERT(VARCHAR(8),[InTime],108)[InTime],CONVERT(VARCHAR(8),[OutTime],108)[OutTime],Prorated,IsAdditionalPayment,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,LeaveDeduct,IsFund=case when (cast(IsFund as bit)=1) then 'Yes' Else 'No' End ,isGrosspay=case when (cast(isGrosspay as bit)=1) then 'Yes' Else 'No' End  FROM [additions_types] left join CeilingMaster on CeilingMaster.Parameter=[Desc] where company_id= " + compID + " or  company_id=-1 and isnull(CeilingMaster.CeilingType,0)<>-1  order by [OptionSelection],[Desc]"; //Added by Sandi
                       sSQL = "SELECT  Replace ([desc],'''' ,'') [desc],additions_types.id [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options, TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash=case when (cast(ClaimCash as int)=2) then 'Yes' Else 'No' End,[Used],CONVERT(VARCHAR(8),[InTime],108)[InTime],CONVERT(VARCHAR(8),[OutTime],108)[OutTime],Prorated,IsAdditionalPayment,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,LeaveDeduct,IsFund=case when (cast(IsFund as bit)=1) then 'Yes' Else 'No' End ,isGrosspay=case when (cast(isGrosspay as bit)=1) then 'Yes' Else 'No' End,[Active] =case when (cast([Active] as int)=1  or [Active] is null ) then 'Yes' Else 'No' End  FROM [additions_types] left join CeilingMaster on CeilingMaster.Parameter=[Desc] where company_id= " + compID + " or  company_id=-1 and isnull(CeilingMaster.CeilingType,0)<>-1  order by [OptionSelection],[Desc]"; //Added by muru
                else
                    //sSQL = "SELECT  Replace ([desc],'''' ,'') [desc], [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options, TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash,[Used],[InTime],[OutTime] FROM [additions_types] where company_id= " + compID + "  Order by [OptionSelection],[Desc]"; Comment by Sandi
                    //sSQL = "SELECT  Replace ([desc],'''' ,'') [desc],additions_types.id [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options,TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash=case when (cast(ClaimCash as int)=2) then 'Yes' Else 'No' End,[Used],CONVERT(VARCHAR(8),[InTime],108)[InTime],CONVERT(VARCHAR(8),[OutTime],108)[OutTime],Prorated,IsAdditionalPayment,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,LeaveDeduct,IsFund=case when (cast(IsFund as bit)=1) then 'Yes' Else 'No' End , isGrosspay=case when (cast(isGrosspay as bit)=1) then 'Yes' Else 'No' End FROM [additions_types] left join CeilingMaster on CeilingMaster.Parameter=[Desc] where company_id= " + compID + " and isnull(CeilingMaster.CeilingType,0)<>-1  order by [OptionSelection],[Desc]"; //Added by Sandi
                    sSQL = "SELECT  Replace ([desc],'''' ,'') [desc],additions_types.id [id],cpf,company_id,isnull(type_of_wage,'O')typeofwage,optionselection,code,formulatype,formulacalc,tax_payable,tax_payable_options,TypeOfWageDesc=Case When (Type_Of_Wage is null Or Type_Of_Wage='O') Then 'Ordinary' When Type_Of_Wage='A' Then 'Additional' Else '' End,isShared,AccountCode,ClaimCash=case when (cast(ClaimCash as int)=2) then 'Yes' Else 'No' End,[Used],CONVERT(VARCHAR(8),[InTime],108)[InTime],CONVERT(VARCHAR(8),[OutTime],108)[OutTime],Prorated,IsAdditionalPayment,IsSDL=case when (cast(IsSDL as bit)=1) then 'Yes' Else 'No' End,LeaveDeduct,IsFund=case when (cast(IsFund as bit)=1) then 'Yes' Else 'No' End , isGrosspay=case when (cast(isGrosspay as bit)=1) then 'Yes' Else 'No' End,[Active] =case when (cast([Active] as int)=1  or [Active] is null ) then 'Yes' Else 'No' End FROM [additions_types] left join CeilingMaster on CeilingMaster.Parameter=[Desc] where company_id= " + compID + " and isnull(CeilingMaster.CeilingType,0)<>-1  order by [OptionSelection],[Desc]"; //Added by muru

                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.AdditionDetails;
        }
        protected void Sub1(object sender, System.EventArgs e)
        {

        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Additions")) == false)
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
                    if (strSystemDefined == "-1")
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        //dataItem.Cells[9].Controls[0].Visible = false;
                        //dataItem.Cells[10].Controls[0].Visible = false;
                    }
                }
            }

            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string strvar = dataItem["OptionSelection"].Text.ToString();

                if (Utility.ToString(strvar).ToUpper() == "VARIABLE")
                {
               //     dataItem.Cells[6].Controls[0].Visible = false;
                }
            }
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_additions where trx_type=" + id, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the addition type.This addition type is in use."));
                        _actionMessage = "Warning|Unable to delete the addition type.This addition type is in use..";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "select company_id from [additions_types] where [id]=" + id, null);
                        if (dr1.Read())
                        {
                            if (dr1[0].ToString() == "-1")
                            {
                                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the addition type.This is a fixed addition type and cannot be deleted."));
                                _actionMessage = "Warning|Unable to delete the addition type.This is a fixed addition type and cannot be deleted..";
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            else
                            {
                                string sSQL = "DELETE FROM [additions_types] WHERE [id] =" + id;

                                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                                if (retVal == 1)
                                {
                                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Addition type  is deleted."));
                                    _actionMessage = "success|Addition type Deleted Successfully.";
                                    ViewState["actionMessage"] = _actionMessage;
                                }
                                else
                                {
                                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the addition type."));
                                    _actionMessage = "Warning|Unable to delete the addition type..";
                                    ViewState["actionMessage"] = _actionMessage;
                                }
                            }
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
                _actionMessage = "Warning|Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.MasterTableView.ClearEditItems();
            }
        }
        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string cpf = (userControl.FindControl("drpcpf") as DropDownList).SelectedItem.Value;
            string type_of_wage = (userControl.FindControl("drpWage") as DropDownList).SelectedItem.Value;
            string optionselection = (userControl.FindControl("rbllist") as RadioButtonList).SelectedItem.Value;
            string strdrptax_payable = (userControl.FindControl("drptax_payable") as DropDownList).SelectedItem.Value;
            string strdrptax_payable_options = (userControl.FindControl("drptax_payable_options") as DropDownList).SelectedItem.Value;
            string accountcode = (userControl.FindControl("txtAccountCode") as TextBox).Text;
            DropDownList drp = (userControl.FindControl("drpShared") as DropDownList);
            DropDownList prorated = (userControl.FindControl("ProRatedDropDownList") as DropDownList);
            DropDownList drpCash = (userControl.FindControl("drpPCash") as DropDownList);
            DropDownList drpIsAdditionalPayment = (userControl.FindControl("drpIsAdditionalPayment") as DropDownList);
            DropDownList drpSDL = (userControl.FindControl("SDLDropDownList") as DropDownList);
            DropDownList drpleaveDeductList = (userControl.FindControl("leaveDeductList") as DropDownList);
             DropDownList drpFundDeductList = (userControl.FindControl("FundDropDownList") as DropDownList);
             DropDownList drpgrosspay = (userControl.FindControl("drpGrosspay") as DropDownList);
            DropDownList ddlActive = (userControl.FindControl("ddlActive") as DropDownList);
            // IF an addition is not CPF payable then type of wage would always be Ordinary
            if (cpf == "No")
                type_of_wage = "O";
            string addtype = (userControl.FindControl("txtaddtype") as TextBox).Text;
            int i = 0;
            #region Validation
            #endregion
            #region Validation New-check whether first 'select' is selected
            if (strdrptax_payable_options == "" && strdrptax_payable == "Yes")
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Tax Payble. </font> "));
                _actionMessage = "Warning|Please Select Tax Group If Taxable is Yes.."; //murugan
                (userControl.FindControl("drptax_payable") as DropDownList).SelectedValue = "No";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            #endregion

            {
                SqlParameter[] parms = new SqlParameter[17];
                parms[i++] = new SqlParameter("@desc", Utility.ToString(addtype));
                parms[i++] = new SqlParameter("@cpf", Utility.ToString(cpf));
                parms[i++] = new SqlParameter("@company_id", compid);
                parms[i++] = new SqlParameter("@type_of_wage", Utility.ToString(type_of_wage));
                parms[i++] = new SqlParameter("@optionselection", optionselection);
                parms[i++] = new SqlParameter("@tax_payable", strdrptax_payable);
                parms[i++] = new SqlParameter("@tax_payable_options", strdrptax_payable_options);
                parms[i++] = new SqlParameter("@accountcode", accountcode);

                if (drp == null)
                {
                    parms[i++] = new SqlParameter("@typeshared", "NO");
                }
                else
                {
                    parms[i++] = new SqlParameter("@typeshared", drp.SelectedItem.Value.ToString());
                }
                if (prorated == null)
                {
                    parms[i++] = new SqlParameter("@prorated", "NO");
                }
                else
                {
                    parms[i++] = new SqlParameter("@prorated", prorated.SelectedItem.Value.ToString());
                }





                parms[i++] = new SqlParameter("@ClaimCash", Convert.ToInt32(drpCash.SelectedItem.Value.ToString()));

                parms[i++] = new SqlParameter("@IsAdditionalPayment", Convert.ToInt32(drpIsAdditionalPayment.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@IsSDL", Convert.ToInt32(drpSDL.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@leaveDeduct", Convert.ToInt32(drpleaveDeductList.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@IsFund", Convert.ToInt32(drpFundDeductList.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@isGrosspay", Convert.ToInt32(drpgrosspay.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@Active", Convert.ToInt32(ddlActive.SelectedItem.Value.ToString()));
                string sSQL = "sp_addtype_add";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    _actionMessage = "Success|Addition Type Added Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                       // ErrMsg = "<font color = 'Red'>Addition Type already Exists.</font>";
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add Addition Type. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Addition Type already Exists.";
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                    }
                }
            }
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];

            string cpf = (userControl.FindControl("drpcpf") as DropDownList).SelectedItem.Value;
            string type_of_wage = (userControl.FindControl("drpWage") as DropDownList).SelectedItem.Value;
            string optionselection = (userControl.FindControl("rbllist") as RadioButtonList).SelectedItem.Value;
            string addtype = (userControl.FindControl("txtaddtype") as TextBox).Text;
            int intdrpfor = Convert.ToInt16((userControl.FindControl("drpfor") as DropDownList).SelectedItem.Value);
            string strtxtFormulaCalc = (userControl.FindControl("txtFormulaCalc") as TextBox).Text;
            string strtxtCode = (userControl.FindControl("txtCode") as TextBox).Text;

            string strdrptax_payable = (userControl.FindControl("drptax_payable") as DropDownList).SelectedItem.Value;
            string strdrptax_payable_options = (userControl.FindControl("drptax_payable_options") as DropDownList).SelectedItem.Value;
            DropDownList drpCash = (userControl.FindControl("drpPCash") as DropDownList);
            DropDownList drpIsAdditionalPayment = (userControl.FindControl("drpIsAdditionalPayment") as DropDownList);
            DropDownList drpSDL = (userControl.FindControl("SDLDropDownList") as DropDownList);
            DropDownList drpleaveDeductList = (userControl.FindControl("leaveDeductList") as DropDownList);
            DropDownList drpFundDeductList = (userControl.FindControl("FundDropDownList") as DropDownList);
            DropDownList drpgrosspay = (userControl.FindControl("drpGrosspay") as DropDownList);
            // IF an addition is not CPF payable then type of wage would always be Ordinary
            //if (cpf == "No")
            //    type_of_wage = "O";

            //Option Selection chanage
            if ((userControl.FindControl("txtaddtype") as TextBox).Enabled == true)
            {
                if (optionselection == "Claim")
                {
                    intdrpfor = 0;
                    string ssqlopt = "select trx_type from emp_additions where status='L' and trx_type=(select id from additions_types where Replace ([desc],'''' ,'')='" + addtype + "' and company_id='" + compid + "')";
                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, ssqlopt, null);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string sMsg = "Cannot Update record since  payroll has already been processed";
                        _actionMessage = "Warning|"+ sMsg ;
                        ViewState["actionMessage"] = _actionMessage;
                        //sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                        //Response.Write(sMsg);
                        return;
                    }
                }
                if (optionselection == "General")
                {
                    intdrpfor = 0;
                    string ssqlopt1 = "select trx_type from emp_additions where status='L' And trx_type=(select id from additions_types where Replace ([desc],'''' ,'')='" + addtype + "' and company_id='" + compid + "')";
                    DataSet ds1 = new DataSet();
                    ds1 = DataAccess.FetchRS(CommandType.Text, ssqlopt1, null);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        string sMsg = "Cannot Update record since  payroll has already been processed";
                        //sMsg = "<SCRIPT language='Javascript'>alert('"  + "');</SCRIPT>";
                        //Response.Write(sMsg);
                        _actionMessage = "Warning|"+ sMsg;
                        ViewState["actionMessage"] = _actionMessage;
                        return;
                    }
                }
            }
            if (optionselection == "Variable")
            {
                if (strtxtCode == "V1")
                {
                    Session["V1ID"] = id.ToString();
                    if (addtype.ToString().Length > 20)
                    {
                        Session["V1Text"] = addtype.ToString().Substring(1, 20);
                    }
                    else
                    {
                        Session["V1Text"] = addtype.ToString();
                    }
                    Session["V1Formula"] = intdrpfor.ToString();
                    Session["V1FormulaCalc"] = strtxtFormulaCalc.ToString();
                }
                if (strtxtCode == "V2")
                {
                    Session["V2ID"] = id.ToString();
                    if (addtype.ToString().Length > 20)
                    {
                        Session["V2Text"] = addtype.ToString().Substring(1, 20);
                    }
                    else
                    {
                        Session["V2Text"] = addtype.ToString();
                    }
                    Session["V2Formula"] = intdrpfor.ToString();
                    Session["V2FormulaCalc"] = strtxtFormulaCalc.ToString();
                }
                if (strtxtCode == "V3")
                {
                    Session["V3ID"] = id.ToString();
                    if (addtype.ToString().Length > 20)
                    {
                        Session["V3Text"] = addtype.ToString().Substring(1, 20);
                    }
                    else
                    {
                        Session["V3Text"] = addtype.ToString();
                    }
                    Session["V3Formula"] = intdrpfor.ToString();
                    Session["V3FormulaCalc"] = strtxtFormulaCalc.ToString();
                }
                if (strtxtCode == "V4")
                {
                    Session["V4ID"] = id.ToString();
                    if (addtype.ToString().Length > 20)
                    {
                        Session["V4Text"] = addtype.ToString().Substring(1, 20);
                    }
                    else
                    {
                        Session["V4Text"] = addtype.ToString();
                    }
                    Session["V4Formula"] = intdrpfor.ToString();
                    Session["V4FormulaCalc"] = strtxtFormulaCalc.ToString();
                }
            }

            DropDownList drp = (userControl.FindControl("drpShared") as DropDownList);
            DropDownList proratedupdate = (userControl.FindControl("ProRatedDropDownList") as DropDownList);
            string accountcode = (userControl.FindControl("txtAccountCode") as TextBox).Text;
            string intime = (userControl.FindControl("txtintime") as TextBox).Text;
            string outtime = (userControl.FindControl("textouttime") as TextBox).Text;
            DropDownList ddlActive2 = (userControl.FindControl("ddlActive") as DropDownList);
            DateTime _datetime;
            if (strtxtFormulaCalc == "Time")
            {
                if (!DateTime.TryParse(intime, out _datetime))
                {
                    string sMsg = "Please Enter Valid Time in HH:mm formate";
                    //sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                    //Response.Write(sMsg);
                    _actionMessage = "Warning|"+ sMsg;
                    ViewState["actionMessage"] = _actionMessage;
                    return;

                }
            }
            #region Validation

            #endregion
            #region Validation New-check whether first 'select' is selected
            if (strdrptax_payable_options == "" && strdrptax_payable == "Yes")
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Tax Payble. </font> "));
                _actionMessage = "Warning|Please Select Tax Payble. .";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            #endregion
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[22];
                parms[i++] = new SqlParameter("@desc", Utility.ToString(addtype));
                parms[i++] = new SqlParameter("@cpf", Utility.ToString(cpf));
                parms[i++] = new SqlParameter("@id", id);
                parms[i++] = new SqlParameter("@type_of_wage", Utility.ToString(type_of_wage));
                parms[i++] = new SqlParameter("@optionselection", optionselection);
                parms[i++] = new SqlParameter("@formulatype", intdrpfor);
                parms[i++] = new SqlParameter("@formulacalc", strtxtFormulaCalc);
                parms[i++] = new SqlParameter("@tax_payable", strdrptax_payable);
                parms[i++] = new SqlParameter("@tax_payable_options", strdrptax_payable_options);
                parms[i++] = new SqlParameter("@accountcode", accountcode);
                parms[i++] = new SqlParameter("@Intime", intime);
                parms[i++] = new SqlParameter("@Outtime", outtime);
                parms[i++] = new SqlParameter("@Isnextday", "0");


                if (drp == null)
                {
                    parms[i++] = new SqlParameter("@typeshared", "NO");
                }
                else
                {
                    parms[i++] = new SqlParameter("@typeshared", drp.SelectedItem.Value.ToString());
                }


                if (proratedupdate == null)
                {
                    parms[i++] = new SqlParameter("@prorated", "NO");
                }
                else
                {
                    parms[i++] = new SqlParameter("@prorated", proratedupdate.SelectedItem.Value.ToString());
                }


                parms[i++] = new SqlParameter("@ClaimCash", Convert.ToInt32(drpCash.SelectedItem.Value.ToString()));

                parms[i++] = new SqlParameter("@IsAdditionalPayment", Convert.ToInt32(drpIsAdditionalPayment.SelectedItem.Value.ToString()));


                parms[i++] = new SqlParameter("@IsSDL", Convert.ToInt32(drpSDL.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@leaveDeduct", Convert.ToInt32(drpleaveDeductList.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@IsFund", Convert.ToInt32(drpFundDeductList.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@isGrosspay", Convert.ToInt32(drpgrosspay.SelectedItem.Value.ToString()));
                parms[i++] = new SqlParameter("@Active", Convert.ToInt32(ddlActive2.SelectedItem.Value.ToString()));

                string sSQL = "sp_addtype_update";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    _actionMessage = "Success|Addition Type Updated Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        //ErrMsg = "<font color = 'Red'>Addition Type already Exists.</font>";
                        ErrMsg = "Addition Type already Exists.";
                    }
                       // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update Addition Type. Reason:</font> " + ErrMsg));
                        _actionMessage = "Warning|Addition Type already Exists.";
                        ViewState["actionMessage"] = _actionMessage;
                        e.Canceled = true;
                   
                }
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
