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

namespace SMEPayroll.Cost
{
    public partial class ReportCategory : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int id;
        DataSet ds = new DataSet();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            id = Utility.ToInteger(Session["Compid"].ToString());
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            CategoryResults.ItemDataBound += new GridItemEventHandler(CategoryResults_ItemDataBound);

        }

        protected void CategoryResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                foreach (GridColumn col in CategoryResults.MasterTableView.AutoGeneratedColumns)
                {
                    if (col.UniqueName != "Category")
                    {
                        if (item[col.UniqueName].Text == "&nbsp;")
                        {
                            item[col.UniqueName].Text = "0";
                        }
                    }
                }


                //Salary
                foreach (GridColumn col in CategoryResults.MasterTableView.AutoGeneratedColumns)
                {
                    if (col.UniqueName != "Category")
                    {
                        if (item[col.UniqueName].Text == "Salary")
                        {
                            item[col.UniqueName].Text = "00";
                        }
                    }
                }
                //


            }

        }

        protected void bindgrid(object sender, EventArgs e)
        {

            string sSQL = "SP_Team_Pivot";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@Company_ID", id);
            parms[1] = new SqlParameter("@month",Convert.ToInt32(drpMonth.SelectedValue));
            parms[2] = new SqlParameter("@year", Convert.ToInt32(drpYear.SelectedValue));
            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

            CategoryResults.DataSource = ds.Tables[0];
            CategoryResults.DataBind();

            #region MyRegion
            for (int i = 0; i < CategoryResults.MasterTableView.AutoGeneratedColumns.Length; i++)
            {
                GridBoundColumn boundColumn = (CategoryResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);

                if (boundColumn != null)
                {
                    if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                    {
                        boundColumn.DataType = Type.GetType("System.Double");
                        boundColumn.Aggregate = GridAggregateFunction.Sum;
                        boundColumn.DataFormatString = "{0:N2}";
                    }
                }

            }

            CategoryResults.MasterTableView.Rebind();
            #endregion
        }

      
      


 




    }

}