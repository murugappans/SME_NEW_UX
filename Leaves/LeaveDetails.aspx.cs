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

namespace SMEPayroll.Leaves
{
    public partial class LeaveDetails : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string s = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                s = Session["Username"].ToString();

                string strSql = "select e.emp_group_id,b.EmpGroupName from employee e,emp_group b where (e.emp_group_id=b.id)and e.UserName='" + s + "'";
                DataSet leaveset = new DataSet();
                leaveset = getDataSet(strSql);
                string gid = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_group_id"]);
                lblempgroup.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["EmpGroupName"]);

            }
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;

        }



    }


}
