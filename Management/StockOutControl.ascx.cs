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
    public partial class StockOutControl : System.Web.UI.UserControl
    {
        string sqlQuery = null;
        DataSet sqlDs = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            int compid = Utility.ToInteger(Session["Compid"]);
            txtTotalPrice.Attributes.Add("OnChanged", "javascript:fnUpdateChange();");
            txtTotalPrice.Attributes.Add("readonly", "readonly");


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
                // rdDisCountType.Enabled = false;

                rdpayType.Enabled = false;
            }

        }
        protected bool IsUserControlPostBack
        {
            get
            {
                return this.ViewState["IsUserControlPostBack"] != null;
            }
        }
        protected void dlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery = "sp_GetStockQuantity";
            SqlParameter[] Param = new SqlParameter[1];
            Param[0] = new SqlParameter("@storeId", Utility.ToInteger(dlStore.SelectedValue.ToString()));
            sqlDs = DataAccess.ExecuteSPDataSet(sqlQuery, Param);
            dlItem.DataSource = sqlDs.Tables[0];
            dlItem.DataTextField = "ItemName";
            dlItem.DataValueField = "Id";
            dlItem.DataBind();
        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "1")
            {
                dlProject.Enabled = false;
                dlEmployee.Enabled = true;
            }
            else
            {
                dlEmployee.Enabled = false;
                dlProject.Enabled = true;
                rdpayType.Enabled = false;
                rdDisCountType1.Enabled = false;
                rdDisCountType2.Enabled = false;
                txtDiscLumsum.Enabled = false;
                txtDiscPercentage.Enabled = false;
            }
        }
        protected void dlIssuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlIssueType.SelectedValue.ToString() == "1")
            {
                rdDisCountType1.Enabled = false;
                rdDisCountType2.Enabled = false;
                rdpayType.Enabled = false;
                txtDiscLumsum.Enabled = false;
                txtDiscPercentage.Enabled = false;
            }
            else if (dlIssueType.SelectedValue.ToString() == "2")
            {

                if (RadioButtonList1.SelectedValue == "2")
                {
                    rdpayType.Enabled = false;
                    rdDisCountType1.Enabled = false;
                    rdDisCountType2.Enabled = false;
                    txtDiscLumsum.Enabled = false;
                    txtDiscPercentage.Enabled = false;
                }
                else
                {
                    rdDisCountType1.Enabled = true;
                    rdDisCountType2.Enabled = true;
                    rdpayType.Enabled = true;
                    txtDiscLumsum.Enabled = true;
                    txtDiscPercentage.Enabled = true;
                }
            }
        }
        protected void dlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemCode = Convert.ToInt16(dlItem.SelectedValue);
            string sSql = "select  [dbo].[fn_getItemPrice] (@ItCode) ";
            SqlParameter[] sqlparam = new SqlParameter[1];
            sqlparam[0] = new SqlParameter("@ItCode", itemCode);
            SqlDataReader sqldr;
            sqldr = DataAccess.ExecuteReader(CommandType.Text, sSql, sqlparam);
            string price = "";
            while (sqldr.Read())
            {
                price = Utility.ToString(sqldr.GetValue(0));
            }
            txtPrice.Text = price.Trim();

        }
       
    }
}