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
    public partial class stockReturnControl : System.Web.UI.UserControl
    {
        string sqlQuery = null;
        DataSet sqlDs = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            int compid = Utility.ToInteger(Session["Compid"]);
            if (!IsUserControlPostBack)
            {
               

                this.ViewState.Add("IsUserControlPostBack", true);
                sqlQuery = "sp_getAllEmployees";
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@compid", compid);
                sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, sqlParam);
                dlEmployee.DataSource = sqlDs.Tables[0];
                dlEmployee.DataTextField = "FullName";
                dlEmployee.DataValueField = "EMP_CODE";
                dlEmployee.DataBind();

                SqlParameter[] Parama = new SqlParameter[1];
                Parama[0] = new SqlParameter("@compId", compid);

                sqlQuery = "sp_getStoredetails";
                sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Parama);
                dlStore.DataSource = sqlDs.Tables[0];
                dlStore.DataTextField = "Store";
                dlStore.DataValueField = "Id";
                dlStore.DataBind();

                sqlQuery = "sp_GetStockQuantity";
                SqlParameter[] Param = new SqlParameter[1];

                Param[0] = new SqlParameter("@storeId", 0);

                sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Param);
                dlItem.DataSource = sqlDs.Tables[0];
                dlItem.DataTextField = "ItemName";
                dlItem.DataValueField = "Id";
                dlItem.DataBind();

                sqlQuery = "sp_GetProjectList";
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@compId", compid);
                sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Para);
                dlProject.DataSource = sqlDs.Tables[0];
                dlProject.DataTextField = "Sub_Project_Name";
                dlProject.DataValueField = "Id";
                dlProject.DataBind();
                
                dlProject.Enabled = false;

            }

        }
        protected bool IsUserControlPostBack
        {
            get
            {
                return this.ViewState["IsUserControlPostBack"] != null;
            }
        }
        protected void dlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery = "sp_GetItemIssueQuantity";
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@EmpCode", Utility.ToInteger(dlEmployee.SelectedValue.ToString()));
            sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Param);
            dlItem.DataSource = sqlDs.Tables[0];
            dlItem.DataTextField = "ItemName";
            dlItem.DataValueField = "ItemCode";
            dlItem.DataBind();
        }
        protected void dlProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery = "sp_GetItemIssueProjectQuantity";
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@PrjCode", Utility.ToInteger(dlProject.SelectedValue.ToString()));
            sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Param);
            dlItem.DataSource = sqlDs.Tables[0];
            dlItem.DataTextField = "ItemName";
            dlItem.DataValueField = "ItemCode";
            dlItem.DataBind();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                dlProject.Enabled = false;
                dlEmployee.Enabled = true;
            }
            else {
                dlEmployee.Enabled = false ;
                dlProject.Enabled = true;
            }
        }

    }
}