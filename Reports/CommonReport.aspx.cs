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
using SMEPayroll.employee;
using System.Data.SqlClient;

namespace SMEPayroll.Reports
{
    public partial class CommonReport : System.Web.UI.Page
    {
        private string PageName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                RadChart1.Skin = "Default";                
            }
            PageName = Request.QueryString["Page"]; 
            //ArrayList report = new ArrayList();
            //report.AddRange(new string[]{"Piechart","BarChart"});
            //rbReport.DataSource = report;
            //rbReport.DataBind();

            //rbReport.SelectedIndex = 0;
            //rbReport.SelectedIndexChanged += new EventHandler(rbReport_SelectedIndexChanged);            
            DataSet ds = new DataSet();
            //SMEPayroll.employee.Employee emp = new SMEPayroll.employee.Employee();
            ds=EmployeeDetails;

            RadChart1.DataSource = ds;
            RadChart1.DataBind();

            PopulateSkins();
        }

        private DataSet EmployeeDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", 3);
                parms1[1] = new SqlParameter("@show", Convert.ToInt16(3));
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(3));
                string sSQL = "sp_GetEmployees";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);

                //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname,(select Designation from Designation where id=desig_id) Designation from employee e where termination_date is null and Company_Id=" + varCompid + " ORDER BY emp_name ";
                //ds = GetDataSet(sSQL);
                return ds;
            }
        }

        //void rbReport_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rbReport.SelectedValue == "Piechart")
        //    {
        //        radPie1.Visible = true;
        //    }
        //    else
        //    {
        //        radBar.Visible = true;
        //    }
        //}

        
        private void PopulateSkins()
        {
            if (!Page.IsPostBack)
            {
                ArrayList skinsList = new ArrayList();
                skinsList.AddRange(new string[] { "Black", "Default", "Hay", "Inox", "Office2007", "Outlook", "Sunset", "Telerik", "Vista", "Web20", "WebBlue", "Marble", "Metal", "Wood", "BlueStripes", "DeepBlue", "DeepGray", "DeepGreen", "DeepRed", "GrayStripes", "GreenStripes", "LightBlue", "LightBrown", "LightGreen" });
                ThumbsList.DataSource = skinsList;                
                ThumbsList.DataBind();
            }
        }

       
        protected void ThumbsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadChart1.Skin = ThumbsList.SelectedValue;
        }


    }
}
