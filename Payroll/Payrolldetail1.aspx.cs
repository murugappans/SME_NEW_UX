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

namespace SMEPayroll.Payroll
{
    public partial class Payrolldetail1 : System.Web.UI.Page
    {
        string compid = "", empcode = "", name = "", month = "", year = "";
        string netpay = "", additions = "", deductions = "";
        string payrate="",ot1="",ot2="",cpf="",fund_type="",otent="",cpfent="";
        string unpaid_leaves = "", unpaid_leaves_amount = "";
        double fund;



        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            compid = Session["Compid"].ToString();
            empcode = Request.QueryString["id"].ToString();
            additions = Request.QueryString["additions"].ToString();
            deductions = Request.QueryString["deductions"].ToString();
            netpay = Request.QueryString["netpay"].ToString();
            name = Request.QueryString["name"].ToString();
            month = Request.QueryString["month"].ToString();
            
            year = Request.QueryString["year"].ToString();
            payrate = Request.QueryString["payrate"].ToString();
            ot1 = Request.QueryString["ot1"].ToString();
            ot2 = Request.QueryString["ot2"].ToString();
            cpf = Request.QueryString["cpf"].ToString();
            fund = Utility.ToDouble(Request.QueryString["fund"]);
            fund_type = Request.QueryString["fundtype"].ToString();
            unpaid_leaves = Utility.ToString(Request.QueryString["unpaid_leaves"]);
            unpaid_leaves_amount = Utility.ToString(Request.QueryString["unpaid_leaves_amount"]);

            otent = Request.QueryString["otentitlement"].ToString();
            cpfent = Request.QueryString["cpfentitlement"].ToString();

            lbldeductions.Text = deductions;
            lbltotaladditions.Text = additions;
            lblnetpay.Text = netpay;
            lblpayto.Text = name;
            lblgrosspay.Text = payrate;
            lblUnpaidLeaves.Text = Utility.ToString(Utility.ToDouble(unpaid_leaves));
            double dblunpleavesamt = Utility.ToDouble(unpaid_leaves_amount);
            lblUnpaidLeavesAmount.Text = dblunpleavesamt.ToString("#0.00");

            if (otent.Trim() == "N")
            {
                lblot1.Text = "";
                lblot2.Text = "";
            }
            else
            {
                lblot1.Text = ot1;
                lblot2.Text = ot2;
            }

            if (cpfent.Trim() == "N")
            {
                lblcpf.Text = "";
                lblfund.Text = "";
            }
            else
            {
                lblcpf.Text = cpf;
                if (Utility.ToString(fund) == "0") lblfund.Text = ""; else lblfund.Text = fund.ToString();
                lblfundtype.Text = fund_type;
            }

            
        }
    }
}
