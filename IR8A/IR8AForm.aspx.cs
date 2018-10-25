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
using System.IO;
using System.Xml;

namespace SMEPayroll.IR8A
{
    public partial class IR8AForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFullName.Text = Request.QueryString["name"].ToString();
            txtDOB.Text = Request.QueryString["date_of_birth"].ToString();
            txtSex.Text = Request.QueryString["sex"].ToString();
            txtMaritalStatus.Text = Request.QueryString["marital_status"].ToString();
            txtDesig.Text = Request.QueryString["desig_id"].ToString();
            txtGrossSalary.Text = IfNullReturnZero(Request.QueryString["Gross"].ToString());
            txtResident.Text = Request.QueryString["address"].ToString().Replace('~', ' ');
            txtemployeetaxnric.Text = Request.QueryString["income_taxid"].ToString();
            txtemployeetaxnric2.Text = Request.QueryString["ic_pp_number"].ToString();
            txtbonus2.Text = IfNullReturnZero(Request.QueryString["bonus"].ToString());
            txtDirectorFee.Text = IfNullReturnZero(Request.QueryString["directorsfee"].ToString());
            txtPension.Text = IfNullReturnZero(Request.QueryString["pension"].ToString());
            txtGrossCommision.Text = IfNullReturnZero(Request.QueryString["GrossCommissionAmountAndOther"].ToString());
            txtDonations.Text = IfNullReturnZero(Request.QueryString["Donation"].ToString());
            txtTransport.Text = IfNullReturnZero(Request.QueryString["Transport"].ToString());
            txtEnterment.Text = IfNullReturnZero(Request.QueryString["Entertenment"].ToString());
            txtOther.Text = IfNullReturnZero(Request.QueryString["otherallowance"].ToString());
            Double TotalAlounce = Convert.ToDouble(txtTransport.Text) + Convert.ToDouble(txtEnterment.Text) + Convert.ToDouble(txtOther.Text);
            txtTotalAllowance.Text = IfNullReturnZero(TotalAlounce.ToString());
            txtBenefits.Text = IfNullReturnZero(Request.QueryString["benefits"].ToString());
            txtGratiaPayment.Text = IfNullReturnZero(Request.QueryString["gratuitynotice"].ToString());
            txtTaxBorneEmployeer.Text = IfNullReturnZero(Request.QueryString["tax_borne_employer_amount"].ToString());
            
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        private string IfNullReturnZero(string QString)
        {
            if (QString.Trim() == "" || QString == null)
                return "0.00";
            else
                return QString;
        }
    }

}