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
using Telerik.Web.UI;

namespace SMEPayroll.Users
{
    public partial class ShowUsers : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        #region Dataset command
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
        }

        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {
                RadGrid1.ClientSettings.ActiveRowIndex = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);
                this.RadGrid1.MasterTableView.Rebind();
            }
        }
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet UserDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "select a.UserName, a.Password,b.GroupName,c.Status, a.Email from employee a, usergroups b,userstatus c where (a.groupid *= b.groupid and a.statusid*=c.statusid)";
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.UserDetails;
        }
        #endregion Dataset command


        #region Update command
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["UserName"];
            string User = id.ToString();
            string sSQL = "sp_user_update";
            string password = (userControl.FindControl("txtpwd") as TextBox).Text;
            string conpwd = (userControl.FindControl("txtconpwd") as TextBox).Text;
            string email = (userControl.FindControl("txtEmail") as TextBox).Text;
            string status = (userControl.FindControl("drpUserStatus") as DropDownList).SelectedItem.Value;
            string group = (userControl.FindControl("drpUserGrp") as DropDownList).SelectedItem.Value;
            int i = 0;
            SqlParameter[] parms = new SqlParameter[5];
            parms[i++] = new SqlParameter("@UserName", Utility.ToString(User));
            parms[i++] = new SqlParameter("@Password", Utility.ToString(password));
            parms[i++] = new SqlParameter("@GroupId", Utility.ToInteger(group));
            parms[i++] = new SqlParameter("@StatusId", Utility.ToInteger(status));
            parms[i++] = new SqlParameter("@Email", Utility.ToString(email));
            int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
            if (retVal == 1)
            {
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>User record has been updated."));
            }
        }
        #endregion Update command

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Users")) == false)
            {
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }

        }

    }
}
