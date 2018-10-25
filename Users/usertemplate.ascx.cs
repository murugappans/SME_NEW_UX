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

namespace SMEPayroll.Users
{
    public partial class usertemplate : System.Web.UI.UserControl
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        private object _dataItem = null;
        # region DataBinding

        int id; string compid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            id = Utility.ToInteger(Session["Compid"].ToString());
            compid = Request.QueryString["compid"];            
        }
        public object DataItem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.DataBinding += new EventHandler(UserControl_DataBinding);
        }

        void UserControl_DataBinding(object sender, EventArgs e)
        {
            /* Binding Country Drop down list*/
            DataSet ds_usergroup = new DataSet();
            ds_usergroup = Usergroupdropdown();
            drpUserGrp.DataSource = ds_usergroup.Tables[0];
            drpUserGrp.DataTextField = ds_usergroup.Tables[0].Columns["GroupName"].ColumnName.ToString();
            drpUserGrp.DataValueField = ds_usergroup.Tables[0].Columns["GroupID"].ColumnName.ToString();
            drpUserGrp.DataBind();
            object UsergroupValue = DataBinder.Eval(DataItem, "GroupID");
            if ((UsergroupValue != DBNull.Value) && (UsergroupValue != null))
            {
                drpUserGrp.SelectedValue = UsergroupValue.ToString();
            }

            DataSet ds_userstatus = new DataSet();
            ds_userstatus = Userstatusdropdown();
            drpUserStatus.DataSource = ds_userstatus.Tables[0];
            drpUserStatus.DataTextField = ds_userstatus.Tables[0].Columns["Status"].ColumnName.ToString();
            drpUserStatus.DataValueField = ds_userstatus.Tables[0].Columns["StatusId"].ColumnName.ToString();
            drpUserStatus.DataBind();
            object UserstatusValue = DataBinder.Eval(DataItem, "StatusId");
            if ((UserstatusValue != DBNull.Value) && (UserstatusValue != null))
            {
                drpUserStatus.SelectedValue = UserstatusValue.ToString();
            }
        }
        #endregion DataBinding

        #region UserGroup Drop Down
        private DataSet Usergroupdropdown()
        {
            string sSQL = "select Groupid,GroupName from UserGroups where company_id=" + compid + "  order by GroupName";
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        #endregion UserGroup Drop Down

        #region UserStatus Drop Down
        private DataSet Userstatusdropdown()
        {
            string sSQL = "select StatusId, Status from UserStatus ";
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        #endregion UserStatus Drop Down

    }
}