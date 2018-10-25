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
    public partial class PayrollWorkflowAssignment : System.Web.UI.Page
    {
         protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int comp_id;
        string strWF = "";

        protected void Page_Load(object sender, EventArgs e)
        {
           
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }

            btnWF2ASsign.Click += new EventHandler(btnWF2ASsign_Click);
            btnWF2UnAssign.Click += new EventHandler(btnWF2ASsign_Click);

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();


            if (strWF == "1" || strWF == "-1")
            {
                //tblWF1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                //tblWF2.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //tb1rw2.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //tb1rw1.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                tr2.Visible = true;
                tr1.Visible = false;
                //radWF11.Visible = true;
                radWf11.Visible = true;
                radWf11second.Visible = true;
                radWf2.Visible = false;
                radWf2second.Visible = false;

                rw1.Visible = true;
                rw2.Visible = true;

                rw3.Visible = false;
                rw4.Visible = false;
            }
            else
            {
                tr1.Visible = true;
                tr2.Visible = false;
                //tblWF2.Style.Add(HtmlTextWriterStyle.Visibility, "visible");
                //tblWF1.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");

                //tb1rw1.Style.Add(HtmlTextWriterStyle.Visibility, "hidden");
                //tb1rw2.Style.Add(HtmlTextWriterStyle.Visibility, "visible");

                radWf2.Visible = true;
                radWf2second.Visible = true;
                radWf11.Visible = false;
                radWf11second.Visible = false;

                rw3.Visible = true;
                rw4.Visible = true;

                rw1.Visible = false;
                rw2.Visible = false;

                
              
            }
            

        }

        protected void drpWorkFlow_databound(object sender, EventArgs e)
        {
            drpWorkFlow.Items.Insert(0, new ListItem("-select-", "-1"));
        }

        protected void RadGrid3_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Workflow Employee Assignment")) == false)
            {
                RadGrid3.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid3.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }

        protected void RadGrid4_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Workflow Employee Assignment")) == false)
            {
                RadGrid4.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid4.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }

        protected void RadGrid3_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Child_ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Payroll Group Assigned.This Payroll Group Assigned is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeAssignedToPayrollGroup] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Payroll Group Assigned is Deleted Successfully."));
                            RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Payroll Group Assigned."));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }

        protected void RadGrid4_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Child_ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Payroll Group Assigned.This Payroll Group Assigned is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeAssignedToPayrollGroup] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Payroll Group Assigned is Deleted Successfully."));
                            RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Payroll Group Assigned."));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }

        protected void RadGrid3_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid3_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }

        protected void RadGrid4_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid4_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }

        protected void RadGrid3_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid3.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid3.DataBind();
        }

        protected void RadGrid4_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid4.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid4.DataBind();
        }



       

        protected void drpSubProjectID_databound(object sender, EventArgs e)
        {
            drpSubProjectID.Items.Insert(0, new ListItem("-select-", "-1"));
            
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Project Assignment")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //}
        }
        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);

                this.RadGrid1.MasterTableView.Rebind();
            }
        }
        protected void RadGrid2_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);

                this.RadGrid2.MasterTableView.Rebind();
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Child_ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project Assigned.This Payroll Group Assigned is in use."));
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeAssignedToProject] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Payroll Group Assigned is Deleted Successfully."));
                            RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Payroll Group Assigned."));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }

        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid2.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid2.DataBind();
        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            string strAction = ((Button)sender).Text;
            RadGrid rd = new RadGrid();
            if (strAction == "Assign")
            {
                rd = RadGrid2;
            }
            if (strAction == "Un-Assign")
            {
                rd = RadGrid1;
            }


            if (drpSubProjectID.SelectedItem.Value.ToString() != "-1")
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[6];
                string strEmployee = "0";

                foreach (GridItem item in rd.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }

                if (strEmployee.Length > 0 && strEmployee != "0")
                {
                    string strActionMsg = "";
                    parms[0] = new SqlParameter("@Company_ID", Utility.ToInteger(comp_id));
                    parms[1] = new SqlParameter("@SUPERVISOR_ID", Utility.ToInteger(drpSubProjectID.SelectedItem.Value));
                    parms[2] = new SqlParameter("@Remarks", Utility.ToString(""));
                    parms[3] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    if (strAction == "Assign")
                    {
                        strActionMsg = "Assign";
                        parms[4] = new SqlParameter("@Action", Utility.ToString("0"));
                    }
                    if (strAction == "Un-Assign")
                    {
                        strActionMsg = "Un-Assign";
                        parms[4] = new SqlParameter("@Action", Utility.ToString("1"));
                    }
                    parms[5] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[5].Direction = ParameterDirection.Output;
                    string sSQL = "SP_PayrollWorkflow_Assignment";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        rd.Controls.Add(new LiteralControl("<font color = 'Red'>Payroll Group " + strActionMsg + " Successfully."));
                        RadGrid2.Rebind();
                        RadGrid1.Rebind();
                    }
                    else
                    {
                        rd.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to " + strActionMsg + " Payroll Group."));
                    }
                }
                else
                {
                    rd.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Employee."));
                }

            }
            else
            {
                rd.Controls.Add(new LiteralControl("<font color = 'Red'>Please select Payroll Group."));
            }
        }

        void btnWF2ASsign_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");

            string strAction = ((Button)sender).Text;
            RadGrid rd = new RadGrid();
            if (strAction == "Assign")
            {
                rd = RadGrid3;
            }
            if (strAction == "Un-Assign")
            {
                rd = RadGrid4;
            }

            if (drpWorkFlow.SelectedItem.Value.ToString() != "-1")
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[6];
                string strEmployee = "0";


                foreach (GridItem item in rd.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }

                if (strEmployee.Length > 0)
                {
                    string strActionMsg = "";
                    parms[0] = new SqlParameter("@Company_ID", Utility.ToInteger(comp_id));
                    parms[1] = new SqlParameter("@PayrollGroupID", Utility.ToInteger(drpWorkFlow.SelectedItem.Value));
                    parms[2] = new SqlParameter("@Remarks", Utility.ToString(""));
                    parms[3] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    if (strAction == "Assign")
                    {
                        strActionMsg = "Assign";
                        parms[4] = new SqlParameter("@Action", Utility.ToString("0"));
                    }
                    if (strAction == "Un-Assign")
                    {
                        strActionMsg = "Un-Assign";
                        parms[4] = new SqlParameter("@Action", Utility.ToString("1"));
                    }
                    parms[5] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[5].Direction = ParameterDirection.Output;

                    string sSQL = "sp_PayrollGroup_Assigned_WF2_Emp";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        if (strAction == "Assign")
                        {
                            RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>WorkFlow Assigned Successfully."));
                            RadGrid3.Rebind();
                            RadGrid4.Rebind();
                        }
                        else
                        {
                            RadGrid3.Controls.Add(new LiteralControl("<font color = 'Red'>WorkFlow UnAssigned Successfully."));
                            RadGrid3.Rebind();
                            RadGrid4.Rebind();
                        }
                    }
                    else
                    {

                        RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Assign WorkFlow."));
                    }
                }
                else
                {
                    RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Employee."));
                }

            }
            else
            {
                RadGrid4.Controls.Add(new LiteralControl("<font color = 'Red'>Please select WorkFlow."));
            }





        }


    }

}

