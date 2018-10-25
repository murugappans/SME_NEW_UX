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

namespace SMEPayroll.Invoice
{
    public partial class ManageReportText : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);

            if(!IsPostBack)
            {
              LoadText();
            }
           
        }

        protected void LoadText()
        {
            //Quotation
            string sql = "SELECT [RId],[Text] FROM [ReportText] where  [Desc] = 'Quotation' AND  [Company_Id]='" + compID + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                QuotationEditor.Content = dr.GetString(dr.GetOrdinal("Text"));

            }

            //Invoice
            string sql_i = "SELECT [RId],[Text] FROM [ReportText] where  [Desc] = 'Invoice' AND  [Company_Id]='" + compID + "'";
            SqlDataReader dr_i = DataAccess.ExecuteReader(CommandType.Text, sql_i, null);
            if (dr_i.Read())
            {
                InvoiceEditor.Content = dr_i.GetString(dr_i.GetOrdinal("Text"));

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ssqlb = "UPDATE [ReportText] SET [Text] = '" + QuotationEditor.Content.Replace("'", "") + "'  WHERE [Desc] = 'Quotation' AND [Company_Id] = '" + compID + "'";
            DataAccess.FetchRS(CommandType.Text, ssqlb, null);

        }


        protected void Invoicebtn_Click(object sender, EventArgs e)
        {
            string ssqlb = "UPDATE [ReportText] SET [Text] = '" + InvoiceEditor.Content.Replace("'", "") + "'  WHERE [Desc] = 'Invoice' AND [Company_Id] = '" + compID + "'";
            DataAccess.FetchRS(CommandType.Text, ssqlb, null);

        }
    }
}
