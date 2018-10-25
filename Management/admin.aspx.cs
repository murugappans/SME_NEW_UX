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

namespace SMEPayroll.Management
{
    public partial class admin : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                string compid = Utility.ToInteger(Session["Compid"]).ToString();
                string sql = "select MultiCurr from company where company_id='" + compid + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if (dr.Read()) {
                    if (dr["MultiCurr"].ToString() != "1")
                    {
                        img1.Visible = false;
                        mc1.Visible = false;
                        tt1.Visible = false;
                        
                      
                      
                    }
                   
                }



            }
            
        }
    }
}
