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
    public partial class SetupAdditionTypes : System.Web.UI.Page
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
            sqlSelectAdditionType.ConnectionString = Session["ConString"].ToString();
            int count;
            string strSQL = "";
            //kumar change select top 40 to select all
            strSQL=" SELECT top 40 ID,[desc],[used]  FROM   Additions_Types A " +   
                   " LEFT  JOIN MapAdditions M on A.Id=M.Additions_Id " +
                   " WHERE (isShared='Yes' OR A.Company_ID=" + comp_id + ") " +   
                   " AND (tax_payable_options NOT IN ('8','9','10','11','12') OR tax_payable_options IS NULL) " +
                   " AND A.Used = 1 AND (code NOT IN ('V1','V2','V3','V4') OR code IS NULL)  " +  
                   " ORDER BY CASE WHEN M.Aid Is NULL Then 1 Else 0 End, M.Aid";
            DataSet dsCheck = new DataSet();
            dsCheck = DataAccess.FetchRS(CommandType.Text,strSQL, null);
            count = dsCheck.Tables[0].Rows.Count;
            string check = Request.QueryString["type"].ToString();
            if (check == "true")
            {
                if (count > 0)
                {
                    Response.Redirect("../Payroll/Emp_BulkAdd.aspx", false);
                }
            }
            
            
        }

        protected void Update_Click(object sender, EventArgs e)
        {
            try {

                for (int i = 0; i < gvAddition_Types.MasterTableView.Items.Count; i++)
                {
                    bool isChecked = ((CheckBox)gvAddition_Types.MasterTableView.Items[i].FindControl("checkbox_select")).Checked;
                    GridDataItem dataitem = (GridDataItem)gvAddition_Types.MasterTableView.Items[i];
                    int ID = Convert.ToInt32(dataitem["ID"].Text);
                    string strSQL = "";
                    strSQL = "Update additions_types set Used='" + isChecked + "' where id=" + ID + "";
                    DataAccess.ExecuteNonQuery(strSQL, null);                   
                }

                Response.Redirect("../Payroll/Emp_BulkAdd.aspx",false);
                
            }
            catch (Exception ex) { }
        }
    }
}
