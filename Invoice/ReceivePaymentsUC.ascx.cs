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

namespace SMEPayroll.Invoice
{
    public partial class ReceivePaymentsUC : System.Web.UI.UserControl
    {
        private object _dataItem = null;
        protected int comp_id;
        public string ClientId;


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            object client = DataBinder.Eval(DataItem, "ClientID");
            ClientId = Convert.ToString(client);

            this.DataBinding += new EventHandler(InnerGrid_DataBinding1);
            comp_id = Utility.ToInteger(Session["Compid"]);

        }

        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }
        protected void InnerGrid_DataBinding1(object sender, EventArgs e)
        {
            object client = DataBinder.Eval(DataItem, "ClientID");
            ClientId = Convert.ToString(client);

            Session["ClientId"] = ClientId;

            #region Inoice Detail grid
            string sSQL = "Sp_ClientDetailInvoiceAmount";
            SqlParameter[] parms = new SqlParameter[2];

            parms[0] = new SqlParameter("@compId", comp_id);
            parms[1] = new SqlParameter("@ClientID", ClientId);

            RadGrid_detail.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            RadGrid_detail.DataBind();
            #endregion

            #region History Grid
            // string history_sql = "SELECT [Cid],[ClientId],[PaymentMethod],[ChequeNo],CONVERT(VARCHAR(20),[Date],106) as [Date],[Amount] FROM [dbo].[ReceivePayment] where ClientId='" + ClientId + "'order by [cid] desc";
            string history_sql = @"select [Cid],[ClientId],[PaymentMethod],
                CASE  [ChequeNO/Mode]
                WHEN 'C'THEN (SELECT 'CASH')
                WHEN 'G'THEN (SELECT 'GIRO')
                ELSE
                [ChequeNO/Mode] END [ChequeNO/Mode],
                [Date],[Amount]
                from 
                (SELECT [Cid],[ClientId],[PaymentMethod],
                CASE WHEN [ChequeNo]='' THEN PaymentMethod ELSE 
                [ChequeNo]END [ChequeNO/Mode]
                ,CONVERT(VARCHAR(20),[Date],106) as [Date],[Amount]
                ";
            history_sql = history_sql + "FROM [dbo].[ReceivePayment] where ClientId='" + ClientId + "') V  order by [cid] desc";

            HistoryRepeater.DataSource = GetDataSet(history_sql);
            HistoryRepeater.DataBind();
            #endregion

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                string sql_insert = "INSERT INTO [ReceivePayment]([ClientId],[PaymentMethod],[ChequeNo],[Date],[Amount])VALUES ('" + Session["ClientId"].ToString() + "','" + cmbPayMethod.SelectedValue + "','" + TxtChequeNo.Text + "',getdate(),'" + TxtAmount.Text + "')";
                DataAccess.ExecuteNonQuery(sql_insert, null);
            }
            catch (Exception error)
            {
                Response.Write(error.Message.ToString());
            }
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }

    }
}