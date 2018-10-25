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
using System.Data.SqlClient;
using Telerik.Web.UI;

namespace SMEPayroll.IR8A
{
    public partial class Ir8AControl : System.Web.UI.UserControl
    {
        int compid;
        string varEmpCode = "";
        string sSQL = "";
        string NRIC = "";
  
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Session["EmpCode"].ToString();
            DataSet ds_employee = new DataSet();
            if (!IsUserControlPostBack)
            {
                this.ViewState.Add("IsUserControlPostBack", true);
                
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , IC_PP_NUMBER  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
                ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpemployee.DataSource = ds_employee.Tables[0];
                drpemployee.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ToString();
                drpemployee.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ToString();
                drpemployee.DataBind();
               
            }
            else 
            {
                sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , IC_PP_NUMBER  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
                ds_employee = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                getEmpDetails();
            }
        }
        private DataSet AllowanceDetails
        {
            get
            {
                DataSet ds = new DataSet();

                if (compid == 1)
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=-1 or company_id=" + compid + " And (OptionSelection='Claim')";
                else
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=" + compid + " And (OptionSelection='Claim')";

                ds = GetDataSet(sSQL);
                return ds;
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        protected void drpemployee_databound(object sender, EventArgs e)
        {
            drpemployee.Items.Insert(0, new ListItem("- All Employees -", "-1"));
            drpemployee.Items.Insert(0, new ListItem("-select-", "-select-"));
        }
        protected void drpemployee_slectIndexChanged(object sender, EventArgs e)
        {

            DataSet sqldS;
            string sqlQuery = "SELECT Company_Name ,Company_Roc,IC_PP_NUMBER FROM EMPLOYEE E INNER JOIN COMPANY C ON E.COMPANY_ID=C.COMPANY_ID WHERE E.EMP_CODE = " + drpemployee.SelectedValue;
            sqldS = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            txtCompany.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Name"].ToString());
            txtComRoc.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Roc"].ToString());
            NRIC = Utility.ToString(sqldS.Tables[0].Rows[0]["IC_PP_NUMBER"].ToString());
           
        }
        protected void exemType_slectIndexChanged(object sender, EventArgs e)
        {

            if (Utility.ToString(exemType.SelectedValue.ToString()) != "0")
                lblSchemeType.Text = exemType.SelectedItem.ToString();
           
        }
        protected void empSection_slectIndexChanged(object sender, EventArgs e)
        {

            if (Utility.ToString(empSection.SelectedValue.ToString()) == "1")
            { 
                lblTaxExemptionFormula.Text = "(l)=(g-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)";
                txtGrossAmount.Text = "0";                
                txtNoTaxAmt.Text = Utility.ToString(((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)));
                txtGainAmt.Text = Utility.ToString(((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)));
            }else if (Utility.ToString(empSection.SelectedValue.ToString()) == "2")
            { 
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)+(l)";
                txtGrossAmount.Text = Utility.ToString((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text));
                txtNoTaxAmt.Text = Utility.ToString((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text));
                txtGainAmt.Text = Utility.ToString(Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text));
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "3")
            { 
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(j)+(l)";
                txtGrossAmount.Text = Utility.ToString((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text));
                txtNoTaxAmt.Text = Utility.ToString((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text));
                txtGainAmt.Text = Utility.ToString(Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text));
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "4")
            {   lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(k)+(l)";
                txtGrossAmount.Text = Utility.ToString((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text));
                txtNoTaxAmt.Text = Utility.ToString((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text));
                txtGainAmt.Text = Utility.ToString(Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text));
            }
            
        }
        protected bool IsUserControlPostBack
        {
            get
            {
                return this.ViewState["IsUserControlPostBack"] != null;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            string sqlQuery ="insert into dbo.IR8AppendixB (emp_code,nric,CompanyCode,TypeOfPlan,Dateofgrant,DateOfExcercise,ExcercisePrice,OpenMarketShareValueAtDateGrant,OpenMarketShareValue,NoOfSharesAcquired,GrossAmountQualifyingForIncomeTax,GrossAmountNotQualifyingForTaxExemption,GrossAmountOfGainsFromPlans,Year)Values";
            string emp_code = drpemployee.SelectedValue.ToString(); 
            
            string CompanyCode=Utility.ToString(Utility.ToInteger(Session["Compid"]));
            string TypeOfPlan = cmbPlan.SelectedIndex.ToString();
            string Dateofgrant=rdGrant.SelectedDate.ToString();
            string DateOfExcercise=rdExcercise.SelectedDate.ToString() ;
            string ExcercisePrice =txtExPrice.Text.ToString(); 
            string OpenMarketShareValueAtDateGrant=txtOpenPrice.Text.ToString();
            string OpenMarketShareValue =txtRefPrice.Text.ToString();
            string NoOfSharesAcquired=txtNoShares.Text.ToString() ;
            string GrossAmountQualifyingForIncomeTax = txtGrossAmount.Text.ToString() ;  
            string GrossAmountNotQualifyingForTaxExemption =txtNoTaxAmt.Text.ToString() ;  
            string GrossAmountOfGainsFromPlans =txtGainAmt.Text.ToString() ;
            string strYear = rdYear.Value;

            sqlQuery = sqlQuery + "( '" + varEmpCode + "',' " + NRIC + "','" + CompanyCode + "','" + TypeOfPlan + "',' " + Dateofgrant + "','" + DateOfExcercise + "','" + ExcercisePrice + "','" + OpenMarketShareValueAtDateGrant + "','" + OpenMarketShareValue + "','" + NoOfSharesAcquired + "','" + GrossAmountQualifyingForIncomeTax + "','" + GrossAmountNotQualifyingForTaxExemption + "','" + GrossAmountOfGainsFromPlans + "','" + strYear + "')";
            int result = DataAccess.ExecuteNonQuery(sqlQuery, null);

        }
        public void getEmpDetails()
        {

            DataSet sqldS;
            string sqlQuery = "SELECT Company_Name ,Company_Roc,IC_PP_NUMBER,emp_code FROM EMPLOYEE E INNER JOIN COMPANY C ON E.COMPANY_ID=C.COMPANY_ID WHERE E.EMP_CODE = " + drpemployee.SelectedValue;
            sqldS = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            txtCompany.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Name"].ToString());
            txtComRoc.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Roc"].ToString());
            NRIC = Utility.ToString(sqldS.Tables[0].Rows[0]["IC_PP_NUMBER"].ToString());
            varEmpCode = Utility.ToString(sqldS.Tables[0].Rows[0]["emp_code"].ToString());
        }
    }
}
