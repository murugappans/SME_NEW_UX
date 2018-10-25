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

namespace SMEPayroll.Management
{
    public partial class ShowDropdowns : System.Web.UI.Page
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

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            bool flag = false;
            DataSet ds = new DataSet();
            ds = ((DataSet)HttpContext.Current.Session["RIGHTds"]);
            string rights = "";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (rights == "")
                {
                    rights = "'" + ds.Tables[0].Rows[i]["RightName"].ToString() + "'";
                }
                else
                {
                    rights = rights + ",'" + ds.Tables[0].Rows[i]["RightName"].ToString() + "'";
                }

            }
            string str = "SELECT [ID], [dropdown],[Navigate] FROM [dropdowns] where dropdown in(" + rights + ") order by DropDown";
            SqlDataSource1.SelectCommand = str;
            RadGrid1.DataSource = SqlDataSource1;
            RadGrid1.DataBind();


        }

        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            int itemCount = (sender as RadGrid).MasterTableView.GetItems(GridItemType.Item).Length + (sender as RadGrid).MasterTableView.GetItems(GridItemType.AlternatingItem).Length;
            foreach (GridItem item in (sender as RadGrid).Items)
            {
                if (item is GridDataItem && item.ItemIndex < itemCount - 1)
                {
                    ((item as GridDataItem)["ID"] as TableCell).Controls.Add(new LiteralControl("<table style='display:none;'><tr><td>"));
                }
            }
        }

    }
}
