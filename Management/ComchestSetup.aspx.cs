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
    public partial class ComchestSetup : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int compid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
        }
        protected void RadGrid4_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid4.DataSource = this.CSNDetails;
        }



        protected void RadGrid4_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //string company_roc = (userControl.FindControl("txtROC") as TextBox).Text.Trim();
            //string company_type = (userControl.FindControl("txtType") as TextBox).Text.Trim();
            //string company_srn = (userControl.FindControl("txtSlNo") as TextBox).Text.Trim();
           //string csn = company_roc + "-" + company_type + "-" + company_srn;
            string csn = (userControl.FindControl("ddlCSN") as DropDownList).SelectedItem.Text;
            string formulaId = (userControl.FindControl("dlOption") as DropDownList).SelectedValue.ToString();
            string formulaType = (userControl.FindControl("dlFormulaType") as DropDownList).SelectedValue.ToString();
            string roundOption = (userControl.FindControl("dlRounding") as DropDownList).SelectedValue.ToString();
            //if (formulaId == "1" || formulaId == "3")
            //{
            //    formulaType = "0";
            //}
            string formula = (userControl.FindControl("txtFormula") as TextBox).Text.Trim();
            if (formula == "")
            {
                formula = "0";
            }
            string sSQL = "";
            sSQL = "SELECT COUNT(CSN) FROM ComchestSetup WHERE UPPER(CSN) = UPPER('" + csn + "')";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            if (dr.Read())
            {
                if (Utility.ToInteger(dr[0].ToString()) == 0)
                {
                    sSQL = "";
                    int retVal = 0;
                    sSQL = "Insert Into ComchestSetup (CSN,FormulaId,FormulaType,Formula,Comp_id,AmtRound) Values('"+csn+"','"+formulaId+"','"+ formulaType +"','"+ formula +"','"+ compid +"',"+roundOption+")";
                    //sSQL = "Insert Into ComchestSetup (CSN,FormulaId,FormulaType,Formula,Amount,Comp_id) Values(@CSN,@FormulaType,@Formula,@Amount,@compid)";
                    //SqlParameter[] sqlparam = new SqlParameter[6];
                    //sqlparam[0] = new SqlParameter("@CSN", csn);
                    //sqlparam[1] = new SqlParameter("@FormulaId", formulaId);
                    //sqlparam[2] = new SqlParameter("@FormulaType", formulaType);
                    //sqlparam[3] = new SqlParameter("@Formula", formula);
                    //sqlparam[4] = new SqlParameter("@Amount", '0');
                    //sqlparam[5] = new SqlParameter("@compid", compid);
                    
                    
                    try
                    {
                        //retVal = DataAccess.ExecuteNonQuery(sSQL, sqlparam);
                        retVal = DataAccess.ExecuteNonQuery(sSQL);
                        _actionMessage = "Success|Inserted Successfully";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = ex.Message;
                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                        {
                            ErrMsg = "Unable to add the record.Please try again.";
                            //ErrMsg = "<font color = 'Red'>Unable to add the record.Please try again.</font>"; 
                        }
                            //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
                            _actionMessage = "Warning|Unable to add record. Reason:"+ ErrMsg;
                            ViewState["actionMessage"] = _actionMessage;
                            e.Canceled = true;
                       
                    }
                    RadGrid4.Rebind();
                }
                else
                {
                    //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>CSN for the compnay already exists. Please choose a different CSN."));
                    _actionMessage = "Warning|CSN for the compnay already exists. Please choose a different CSN.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }

        }

        protected void RadGrid4_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string csnid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

            string company_csn = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CSN"]);
            string formulaId = (userControl.FindControl("dlOption") as DropDownList).SelectedValue.ToString();
            string formulaType = (userControl.FindControl("dlFormulaType") as DropDownList).SelectedValue.ToString();
            string roundOption = (userControl.FindControl("dlRounding") as DropDownList).SelectedValue.ToString();
            if (formulaId == "2")
            {
                formulaType = "0";
            }
            string formula = (userControl.FindControl("txtFormula") as TextBox).Text.Trim();
            if (formula == "")
            {
                formula = "0";
            }
            string sSQL = "";
            sSQL = "SELECT COUNT(CSN) FROM ComchestSetup WHERE UPPER(CSN) = UPPER('" + company_csn + "')" + "AND id !=" + Convert.ToInt32(csnid);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

            if (dr.Read())
            {
                if (Utility.ToInteger(dr[0].ToString()) == 0)
                {
                    sSQL = "update ComchestSetup set CSN='" + company_csn + "' , FormulaId = '" + formulaId.Trim() + "',FormulaType='" + formulaType.Trim() + "',Formula='" + formula + "',AmtRound='" + roundOption.Trim() + "'  where id=" + csnid;
                    int retVal = DataAccess.ExecuteStoreProc(sSQL);
                    if (retVal == 1)
                    {
                        //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Information updated successfully"));
                        _actionMessage = "Success|Information updated successfully";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to update the record"));
                        _actionMessage = "Warning|Unable to update the record";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    this.RadGrid4.DataSource = this.CSNDetails;
                    RadGrid4.Rebind();
                }

                else
                {
                    //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>CSN for the compnay already exists. Please choose a different CSN."));
                    _actionMessage = "Warning|CSN for the compnay already exists. Please choose a different CSN.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
        }


        protected void RadGrid4_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string csnid = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                string company_csn = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CSN"]);

                string SQL1 = "select count(*) from AmcAssignedEmployee Where AssignedAMCID= '" + csnid + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL1, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                        //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete this record.CSN is in use."));
                        _actionMessage = "Warning|Unable to delete this record.CSN is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM ComchestSetup where id ='{0}'";
                        sSQL = string.Format(sSQL, csnid);
                        int i = DataAccess.ExecuteStoreProc(sSQL);
                        _actionMessage = "Success|Deleted Successfully";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|>Unable to delete the User. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        private DataSet CSNDetails
        {
            get
            {
                string sSQL = "";
                DataSet ds = new DataSet();
                if (compid == null)
                {
                    compid = 0;
                }
                sSQL = "Select ID,CSN, Case when FormulaId=1 then 'Percentage'  Else 'Variable Amount' End As FormulaId,Case  FormulaType   When '1' Then LTRIM(RTRIM(cast(Formula as Nvarchar))) +'% Of Basic Salary' Else '-' End As FormulaType,Formula,Case when AmtRound='1' Then 'Drop Decimals' when AmtRound='2' Then 'Round' else '--' End As AmtRound from ComchestSetup where comp_id=" + compid;
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                return ds;
            }
        }
        protected void RadGrid4_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid4.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid4.MasterTableView.ClearEditItems();
            }
        }
    }
}

