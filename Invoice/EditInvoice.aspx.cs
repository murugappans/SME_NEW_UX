using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SMEPayroll.Invoice
{
    public partial class EditInvoice : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        public int compid;
        public string InvoiceNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"]);
            if (!IsPostBack)
            {

                BindGrid();
                LoadCompanyInfo();
            }
        }

        private void LoadCompanyInfo()
        {
            if (Session["Invoice"] != null)
            {
                InvoiceNo = (string)Session["Invoice"];
            }

            #region invoiceinfo
            DataSet ds = new DataSet();
            string Str = "select [ContactPerson1],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[ClientName],[Phone1],[Phone2],[Fax],[Email],[ContactPerson2],[Remark],[IId],[InvoiceNo],[CreateDate],(select paymentterms from paymentterms where ip=II.[PaymentTerms])as [PaymentTerms],[SubTotal],[GST],[Total],[Confirm] from Invoice_info II inner join clientdetails CD on II.ClientID=CD.ClientID where InvoiceNo='" + InvoiceNo + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, Str, null);
            while (dr.Read())
            {
                Label1.Text = Utility.ToString(dr.GetValue(0));
                Label2.Text = (Utility.ToString(dr.GetValue(1)) != "" ? Utility.ToString(dr.GetValue(1)) : "");
                Label3.Text = (Utility.ToString(dr.GetValue(2)) != "" ? Utility.ToString(dr.GetValue(2)) : "");
                Label4.Text = (Utility.ToString(dr.GetValue(3)) != "" ? Utility.ToString(dr.GetValue(3)) : "");
                Label5.Text = (Utility.ToString(dr.GetValue(4)) != "" ? Utility.ToString(dr.GetValue(4)) : "");
                Label6.Text = (Utility.ToString(dr.GetValue(5)) != "" ? Utility.ToString(dr.GetValue(5)) : "");

                lblClient.Text = (Utility.ToString(dr.GetValue(6)) != "" ? Utility.ToString(dr.GetValue(6)) : "");

               // lblCreateDate.Text = (Utility.ToString(dr.GetValue(15)) != "" ? Utility.ToString(dr.GetValue(15)) : "");
                if (Utility.ToString(dr.GetValue(15)) != "")
                {
                    DateTime d = Convert.ToDateTime(Utility.ToString(dr.GetValue(15)));
                    lblCreateDate.Text = d.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ"));
                }


                lblInvoice.Text = (Utility.ToString(dr.GetValue(14)) != "" ? Utility.ToString(dr.GetValue(14)) : "");
                lblPaymentTerms.Text = (Utility.ToString(dr.GetValue(16)) != "" ? Utility.ToString(dr.GetValue(16)) : "");
            }
            #endregion
        }

        private void BindGrid()
        {
            #region Hourly
            DataSet ds_hor = new DataSet();
            if (Session["Invoice"] != null)
            {
                InvoiceNo = (string)Session["Invoice"];
            }

            string sql_h = "SELECT (select ClientId from invoice_info where InvoiceNo=IH.InvoiceNo)as ClientId,(select Sub_Project_ID from subproject where Sub_Project_name=IH.[Project])as[Project_ID],[Trade] ,[Project],(select id from Trade where Trade=IH.[Trade])as TradeID,[Amount],[FromDate] ,[ToDate] FROM [Invoice_Hourly] IH where  [InvoiceNo]='" + InvoiceNo + "'";
            ds_hor = GetDataSet(sql_h);
            if (ds_hor.Tables[0].Rows.Count > 0)
            {
                RadGrid_Hourly.DataSource = ds_hor;
                RadGrid_Hourly.DataBind();
            }
            else
            {
                hourId.Visible = false;
                hourGridId.Visible=false;
            }
            #endregion

            #region Monthly
            DataSet ds_Mon = new DataSet();
            string sql_M = "SELECT [InvoiceNo],(select Sub_Project_name from subproject where Sub_Project_Id=I.[Project])ProjectName,(Select Trade from Trade where Id=I.[Trade])TradeName,(Select (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) 'EMP_NAME' from Employee where emp_code=I.[Empcode])EMP_NAME,[Month],[Year],[Amount]FROM [Invoice_Monthly] I where  [InvoiceNo]='" + InvoiceNo + "'";
             ds_Mon = GetDataSet(sql_M);
             if (ds_Mon.Tables[0].Rows.Count > 0)
             {
                 RadGrid1_Monthly.DataSource = ds_Mon;
                 RadGrid1_Monthly.DataBind();
             }
             else
             {
                 MonthId.Visible = false;
                 MonthGridId.Visible = false;
             }
            #endregion

             #region Variables
               DataSet ds_Var= new DataSet();
               string sql_var = "SELECT [variableName],[Amount],[AddSubType]as [Type],[Project] FROM [Invoice_variable] where  [InvoiceNo]='" + InvoiceNo + "'";
               ds_Var = GetDataSet(sql_var);
               if (ds_Var.Tables[0].Rows.Count > 0)
               {
                   RadGrid3.DataSource = ds_Var;
                   RadGrid3.DataBind();
               }
               else
               {
                   VarId.Visible = false;
                   VarGridId.Visible = false;
               }
             #endregion
         }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (Session["Invoice"] != null)
            {
                InvoiceNo = (string)Session["Invoice"];
            }
            Session["Invoice"] = InvoiceNo;

            if (Session["Invoice"] != null)
            {
                string str = @"Invoice_Report.aspx";
                //string popupScript = "<script language='javascript'>" + "window.open('', '" + str + "', " + "'width=1000, height=1000, menubar=yes, resizable=yes')" + "</script>";
                HttpContext.Current.Response.Write("<SCRIPT language='Javascript'>window.open('" + str + "');</SCRIPT>");
            }
        }



        public string GetUrl(object trade, object Project, object ClientId, object FromDate1, object ToDate1)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(FromDate1);
            int m = dt.Date.Month;
            int d = dt.Date.Day;
            int y = dt.Date.Year;
            string Fromdate = d + "/" + m + "/" + y;

            DateTime dt1 = new DateTime();
            dt1 = Convert.ToDateTime(ToDate1);
            int m1 = dt1.Date.Month;
            int d1 = dt1.Date.Day;
            int y1 = dt1.Date.Year;
            string ToDate = d1 + "/" + m1 + "/" + y1;



            ////http://localhost/SMEPayroll9.5_VSS_Test/Invoice/InvoiceDetail.aspx?Trade=Driver&FromDate=01/11/2011&Todate=22/11/2011&ProjectId=MS20111072&OrderNo=10009
            string url = "~/Invoice/InvoiceDetail.aspx?Trade=" + trade.ToString() + "&FromDate=" + Fromdate + "&Todate=" + ToDate + "&ProjectId=" + Project.ToString() + "&Client=" + ClientId + "";
            //string url = "~/Invoice/InvoiceDetail.aspx";
            return url;
        }

    }
}
