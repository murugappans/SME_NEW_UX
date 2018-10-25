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

namespace SMEPayroll.Management
{
    public partial class EmployeeAccomadationInfo : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int s = 0, varCompid;
        string _actionMessage = "";
        protected string sMsg = "";
        #region Dataset command
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            ViewState["actionMessage"] = "";

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varCompid = Utility.ToInteger(Session["Compid"]);
            sMsg = Utility.ToString(Request.QueryString["msg"]);
            if (!IsPostBack)
            {
                Session["s"] = 0;
            }

        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("AssignAccomadation.aspx?empcode=" + dataItem["EmpCode"].Text);
            }
        }

        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (!this.IsPostBack)
            {

                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);

                this.RadGrid1.MasterTableView.Rebind();
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet EmployeeDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[1];
                parms1[0] = new SqlParameter("@compId", varCompid);
                string sSQL = "sp_EmpAccomadationDetails";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                return ds;
            }
        }
        private DataSet DormetryDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[1];
                parms1[0] = new SqlParameter("@compId", varCompid);
                string sSQL = "sp_EmpAccomadationDetails";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.EmployeeDetails;
        }
        #endregion Dataset command

        #region Delete command

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string EmpCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"]);
                string sSQL = "sp_emp_delete";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@emp_code", Utility.ToInteger(EmpCode));
                string sql = "select a.username,b.company_code from employee a,company b where a.company_id=b.company_id and emp_code=" + EmpCode;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if (dr.Read())
                {
                    string username = dr[0].ToString();
                    string usernam1 = dr[1].ToString() + "Admin";
                    if (username == usernam1)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This superadmin employee record cannot be deleted. "));
                        _actionMessage = "Warning|This superadmin employee record cannot be deleted. ";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        int i = DataAccess.ExecuteStoreProc(sSQL, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete the User. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        #endregion Delete command

        protected void RadGrid1_ItemDataBound1(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Add Employee")) == false)
            {
                if (e.Item is GridCommandItem)
                {
                    GridCommandItem commandItem = (GridCommandItem)e.Item;
                    HyperLink button = commandItem.FindControl("HyperLink1") as HyperLink;
                    button.Visible = false;
                }
            }
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Edit Employee")) == false)
            {
                RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
                RadGrid1.ClientSettings.EnablePostBackOnRowClick = false;
            }
        }

        

        

        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

            if (Utility.ToInteger(Session["s"]) == 1)
            {
               RadGrid1.CurrentPageIndex = e.NewPageIndex;
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", varCompid);
                parms1[1] = new SqlParameter("@show", "1");
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                string sSQL = "sp_GetEmployees";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }

        }
       


        


    }
}
