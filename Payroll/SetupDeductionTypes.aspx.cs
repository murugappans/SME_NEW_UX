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
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace SMEPayroll.Payroll
{
    public partial class SetupDeductionTypes : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int comp_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            sqlSelectDeductionType.ConnectionString = Session["ConString"].ToString();
            int count;
            string strSQL = "";
            //kumar change to select top 40 to select
            strSQL = "SELECT top 40 ID,[desc],[used] " +            
                     " FROM   Deductions_Types  " +            
                     " WHERE  ((Company_ID = " + comp_id + ")OR (upper(isShared)='YES' AND Company_ID!=-1 )) " +   
                     " AND Used = 1 ORDER  BY [desc]";
            DataSet dsCheck = new DataSet();
            dsCheck = DataAccess.FetchRS(CommandType.Text,strSQL, null);
            count = dsCheck.Tables[0].Rows.Count;
           
            string check = Request.QueryString["type"].ToString();
            if (check == "true")
            {
                if (count > 0)
                {
                    Response.Redirect("../Payroll/Emp_BulkDed.aspx", false);
                }
            }
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < gvDeduction_Types.MasterTableView.Items.Count; i++)
                {
                    bool isChecked = ((CheckBox)gvDeduction_Types.MasterTableView.Items[i].FindControl("checkbox_select")).Checked;
                    GridDataItem dataitem = (GridDataItem)gvDeduction_Types.MasterTableView.Items[i];
                    int ID = Convert.ToInt32(dataitem["ID"].Text);
                    string strSQL = "";
                    strSQL = "Update Deductions_Types set Used='" + isChecked + "' where id=" + ID + "";
                    DataAccess.ExecuteNonQuery(strSQL, null);
                }

                Response.Redirect("../Payroll/Emp_BulkDed.aspx", false);

            }
            catch (Exception ex) { }
        }
    }
}
