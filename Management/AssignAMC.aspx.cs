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
    public partial class AssignAMC : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        protected int comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
           string sgroupname = Utility.ToString(Session["GroupName"]);
            if (sgroupname == "Super Admin")//Senthil-Added-26/08/2015
            {
                SqlDataSource2.SelectCommand = "SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID IS NULL )And e.Company_id=@company_id and (termination_date is Null OR MONTH(termination_date)=MONTH(getdate())) AND Emp_type in ('SPR','SC') Order By Emp_name";
            }
            else
            {
                if (Utility.GetGroupStatus(comp_id) == 1)
                {
                    SqlDataSource2.SelectCommand = "SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID IS NULL ) And e.Company_id=" + comp_id + " and E.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR E.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") and (termination_date is Null OR MONTH(termination_date)=MONTH(getdate())) AND Emp_type in ('SPR','SC') Order By Emp_name";
                }
                else
                {
                    SqlDataSource2.SelectCommand = "SELECT e.emp_code As EmpId,Emp_name + '' + Emp_lname as Name from employee E Left outer join AmcAssignedEmployee AE on e.emp_code=AE.EmpCode where (AE.AssignedAMCID IS NULL )And e.Company_id=@company_id and (termination_date is Null OR MONTH(termination_date)=MONTH(getdate())) AND Emp_type in ('SPR','SC') Order By Emp_name";
                }
            }
        }
        protected void drpSubProjectID_databound(object sender, EventArgs e)
        {
            drpSubProjectID.Items.Insert(0, new ListItem("-select-", "-1"));
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
                SqlParameter[] parms = new SqlParameter[5];
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
                    parms[0] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    parms[1] = new SqlParameter("@AmcId", Utility.ToInteger(drpSubProjectID.SelectedItem.Value));
                    parms[2] = new SqlParameter("@compId", Utility.ToString(comp_id));
                    if (strAction == "Assign")
                    {
                        strActionMsg = "Assign";
                        parms[3] = new SqlParameter("@Action", Utility.ToString("0"));
                    }
                    if (strAction == "Un-Assign")
                    {
                        strActionMsg = "Un-Assign";
                        parms[3] = new SqlParameter("@Action", Utility.ToString("1"));
                    }
                    parms[4] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[4].Direction = ParameterDirection.Output;
                    string sSQL = "sp_AMCAssigned";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Department " + strActionMsg + " Successfully."));
                        _actionMessage = "sucess|Employee "+ strActionMsg + " Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                        RadGrid2.Rebind();
                        RadGrid1.Rebind();
                    }
                    else
                    {
                        //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to " + strActionMsg + " Departments."));
                        _actionMessage = "Warning|Unable to "+ strActionMsg + " Employee.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                else
                {
                    //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Employee."));
                    _actionMessage = "Warning|Please Select Employee.";
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
            else
            {
                //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Please select  Departments."));
                _actionMessage = "Warning|Please select  Departments.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
    }
}
