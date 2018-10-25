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
using System.Net.Mail;
using System.IO;
using System.Text;

namespace SMEPayroll.Invoice
{
    public partial class Invoice_Report1 : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        public string InvoiceNo;
        decimal grdTotalAdd, grdTotalSub;
        public string CreateDate, PaymentTerms, SubTotal, GST, Total;
        string Phone1, Phone2, Fax, Email, ContactPerson1, ContactPerson2, Remark, gst;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
           
        }

        private void Bind()
        {


            if (Session["Invoice"] != null)
            {
                InvoiceNo = (string)Session["Invoice"];
            }

            SqlParameter[] parms1 = new SqlParameter[1];
            parms1[0] = new SqlParameter("@InvoiceNo ", InvoiceNo);
            DataSet ds = new DataSet();
            ds = DataAccess.ExecuteSPDataSet("sp_Invoice_ReportDetail", parms1);
            GridView1.DataSource=ds.Tables[0];
            GridView1.DataBind();

            Repeater1.DataSource = ds.Tables[0];
            Repeater1.DataBind();

            //Invoice Detail
            //invoice_info
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                // get the data
                DateTime d = Convert.ToDateTime(row["CreateDate"].ToString());
                lblCreateDate.Text = d.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ"));
                lblInvoiceNo.Text = row["InvoiceNo"].ToString();
                lblPamentterms.Text = row["PaymentTerms"].ToString();
                lblSubTotal.Text = row["SubTotal"].ToString();
                gst = row["GST"].ToString();
                lblTotal.Text = row["Total"].ToString();
                divid.InnerHtml = row["FooterText"].ToString();
            }

            if (gst == "0.00")
            {
                GSTTd.Visible = false;
            }
            else
            {
                GSTTd.Visible = true;
                lblGST.Text = gst;
            }
            //

            //Client Detail
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                // get the data
                Label1.Text = row["ClientName"].ToString();
                Label2.Text = (row["Block"].ToString() != "" ? "BLOCK " + row["Block"].ToString() + "" : "");
                Label3.Text = (row["Block"].ToString() != "" ? "# " + row["Level"].ToString() + "-" : "") + (row["Unit"].ToString() != "" ? row["Unit"].ToString() : "") + " " + (row["StreetBuilding"].ToString() != "" ? row["StreetBuilding"].ToString() : "");
                Label4.Text = (Convert.ToString(row["PostalCode"]) != "" ? "SINGAPORE " + Convert.ToString(row["PostalCode"]) + "" : "");



     
                Phone1 = row["Phone1"].ToString();
                Phone2 = row["Phone2"].ToString();
                Fax = row["Fax"].ToString();
                Email = row["Email"].ToString();
                ContactPerson1 = row["ContactPerson1"].ToString();
                ContactPerson2 = row["ContactPerson2"].ToString();
                Remark = row["Remark"].ToString();
            }
            //

            //Project List
            string sSQL = "Sp_ProjectsInInvoice";                     
            SqlParameter[] parms = new SqlParameter[1];
            parms[0] = new SqlParameter("@Invoice", InvoiceNo);
            SqlDataReader dr_invoice = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            while (dr_invoice.Read())
            {
                lblSubProject.Text=dr_invoice.GetValue(0).ToString();
            }
            //

        }

      
         
        protected void GridView1_RowDataBound (object sender, GridViewRowEventArgs e)
        {
            #region to find Total
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Type")) == "Addition")
            //    {
            //        decimal rowTotal = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //        grdTotalAdd = grdTotalAdd + rowTotal;
            //    }
            //    if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Type")) == "Deduction")
            //    {
            //        decimal rowTotal1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //        grdTotalSub = grdTotalSub + rowTotal1;
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lbl = (Label)e.Row.FindControl("lblTotal");
            //    decimal total = grdTotalAdd - grdTotalSub;
            //    lbl.Text = "Total=" + total.ToString();
            //}
            #endregion
            
        }




       

    }
}
