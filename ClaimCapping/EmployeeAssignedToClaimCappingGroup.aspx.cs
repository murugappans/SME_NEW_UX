using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using efdata;

namespace SMEPayroll.Management
{
    public partial class EmployeeAssignedToClaimCappingGroup : System.Web.UI.Page
    {
        protected string SHeadingColor = Constants.HEADING_COLOR;
        protected string SBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int CompanyId;
        string _actionMessage = "";
        private CliamRepository _cliamRepository;

        public EmployeeAssignedToClaimCappingGroup()
        {
            this._cliamRepository =  new CliamRepository();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            CompanyId = Utility.ToInteger(Session["Compid"]);
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");



           

            if (!IsPostBack)
            {
                this.ClaimGroupSelect.DataSource = _cliamRepository.GetClaimCappingGroups();

                ClaimGroupSelect.DataBind();
                
            }

        //    RadGridDataBind();


        }
        protected void RadGrid1_SortCommand(object source, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            //Default sort order Descending

            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
            RadGridDataBind();
        }


        protected void drpTypeID_databound(object sender, EventArgs e)
        {
            ClaimGroupSelect.Items.Insert(0, new ListItem("-select-", "-1"));
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Project Assignment")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //}
          
        }

        protected void RadGrid1_Prerender(object sender,  EventArgs e)
        {
            RadGridDataBind();
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project Assigned.This Project Assigned is in use."));
                        _actionMessage = "Warning|>Unable to delete the Project Assigned.This Project Assigned is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [Encashment] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                           // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Project Assigned is Deleted Successfully."));
                            _actionMessage = "Success|Project Assigned is Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                            RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project Assigned."));
                            _actionMessage = "Warning|>Unable to delete the Project Assigned.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
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
           // RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid2.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid2.DataBind();
        }

        //protected void buttonSave_click(object sender, EventArgs e)
        //{
          
        //    string strAction = ((Button)sender).Text;

      
        //        foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //        {
        //            if (item is GridItem)
        //            {
        //                GridDataItem dataItem = (GridDataItem)item;
        //                CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
        //                TextBox fixedAmounttxtbox = (TextBox)dataItem.FindControl("FixedAmount");
        //                TextBox NoOfDaystxtbox = (TextBox)dataItem.FindControl("NoOfDays");
        //                Label labelId = (Label)dataItem.FindControl("ID");

        //                double k = 0.0;
        //                if (Utility.ToDouble(fixedAmounttxtbox.Text) > 0)
        //                {
        //                    k = Utility.ToDouble(fixedAmounttxtbox.Text);
        //                }


                       
        //                    string sql = "update Encashment set FixedAmount=" + k + ",NoOfDays=" + Utility.ToDouble(NoOfDaystxtbox.Text) + " where id=" + Utility.ToInteger(labelId.Text);
        //                    DataAccess.ExecuteStoreProc(sql);
                        
        //            }
        //        }

            
        //}

        protected void buttonAdd_Click(object sender, EventArgs e)
        {

            if(ClaimGroupSelect.SelectedValue == "-1")
                return;


            if(CompanyId <= 0)
                return;

            List<EmployeeAssignedToClaimGroup> emplist= new List<EmployeeAssignedToClaimGroup>();
            
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


           
              
                foreach (GridItem item in rd.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem) item;
                        CheckBox chkBox = (CheckBox) dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked)
                        {
                            Label labelId = (Label) dataItem.FindControl("Emp_ID");
                            Label Id = (Label)dataItem.FindControl("ID");

                        emplist.Add( new EmployeeAssignedToClaimGroup()
                           {
                                Id= int.Parse(Id.Text),
                                CliamGroupId = int.Parse(ClaimGroupSelect.SelectedValue.ToString()),
                               CompanyId = CompanyId,
                               EmpId = int.Parse(labelId.Text)
                           });  
                            
                      
                        }
                    }


                }

            try
            {
                if (strAction == "Assign")
                {
                    _cliamRepository.AssignToClaimGroup(emplist);
                }
                if (strAction == "Un-Assign")
                {
                    _cliamRepository.RemoveFromClaimGroup(emplist);
                }


            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                RadGridDataBind();
            }

            
        }


        private void RadGridDataBind()
        {
            int groupId = Int32.Parse(ClaimGroupSelect.SelectedValue.ToString());
            if (groupId == -1)
            {
                //RadGrid1.DataSource = null;
                //RadGrid1.DataBind();

                //RadGrid2.DataSource = null;
                //RadGrid2.DataBind();
                RadGrid2.DataSource = _cliamRepository.GetUnAssigentList(CompanyId, groupId);
                RadGrid2.DataBind();

                RadGrid1.DataSource = _cliamRepository.GetAssigentList(CompanyId, groupId);
                RadGrid1.DataBind();
                return;
            }




            RadGrid2.DataSource = _cliamRepository.GetUnAssigentList(CompanyId, groupId);
            RadGrid2.DataBind();

            RadGrid1.DataSource = _cliamRepository.GetAssigentList(CompanyId, groupId);
            RadGrid1.DataBind();

        }

        protected void ClaimGroupSelect_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            RadGridDataBind();
        }
    }
}

