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

namespace SMEPayroll.Users
{
    public partial class ShowGroups : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int id;

        #region Dataset command
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            id = Utility.ToInteger(Session["Compid"].ToString());
        }

        protected void Radgrid1_Itemcommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete" && e.CommandName != "Edit")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                Button btn = (Button)dataItem["rights"].FindControl("btnrights");
                int id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[e.Item.ItemIndex].GetDataKeyValue("GroupId"));
                if (id != 0)
                {
                    Session["varGroupID"] = id.ToString();
                    Response.Redirect("../Management/AssignUserRights.aspx");
                }
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
        private DataSet UserDetails
        {
            get
            {
                DataSet ds = new DataSet();
                string sSQL = "select GroupID, GroupName,'Rights' Rights from UserGroups  where company_id=" + id + " order by 1";
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
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["GroupId"];
            object name = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["GroupName"];
            string groupname1 = name.ToString();
            if (groupname1 != "Super Admin")
            {
                int GroupId = Int32.Parse(id.ToString());
                string sSQL = "sp_Group_update";
                string groupname = (userControl.FindControl("txtgroupname") as TextBox).Text;
                int i = 0;
                SqlParameter[] parms = new SqlParameter[2];
                parms[i++] = new SqlParameter("@GroupID", GroupId);
                parms[i++] = new SqlParameter("@GroupName", Utility.ToString(groupname));

                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record updated successfully</font> "));
                    _actionMessage = "Success|GroupName Updated Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            else
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This name cannot be modified</font> "));
                _actionMessage = "Warning|GroupName cannot be modified.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        #endregion Update command

        #region Insert command
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string groupname = (userControl.FindControl("txtgroupname") as TextBox).Text;
            int i = 0;
            SqlParameter[] parms = new SqlParameter[2];
            parms[i++] = new SqlParameter("@company_id", Utility.ToString(id));
            parms[i++] = new SqlParameter("@GroupName", Utility.ToString(groupname));
            string sSQL = "sp_Group_add";
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                _actionMessage = "Success|GroupName Added Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                {
                    ErrMsg = "<font color = 'Red'>GroupName already Exist, Please Enter some other Groupname</font>";
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add group. Reason:</font> " + ErrMsg));
                    _actionMessage = "Warning|GroupName already Exist, Please Enter some other Groupname.";
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
            }
        }
        #endregion Insert command

        #region Delete command

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string GroupId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["GroupId"]);
                string GroupName = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["GroupName"]);
                if (GroupName == "Super Admin")
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Cannot delete the super admin rights" ));
                    _actionMessage = "Warning|Cannot delete the super admin rights.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where GroupID=" + GroupId, null);
                    if (dr.Read())
                    {
                        if (dr[0].ToString() != "0")
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the department.This department is in use."));
                            _actionMessage = "Warning|Unable to delete the Group.This Group is in use.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            string sSQL = "DELETE FROM UserGroups where GroupID = {0}";
                            sSQL = string.Format(sSQL, GroupId);
                            int i = DataAccess.ExecuteStoreProc(sSQL);
                            _actionMessage = "Success|GroupName Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Group can not be deleted becuase of REFERENCE constraint. This group is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete group. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Group can not be deleted becuase of REFERENCE constraint. This group is called in other tables";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        #endregion Delete command

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage User Groups")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("Rights").Visible = false;
                RadGrid1.ClientSettings.EnablePostBackOnRowClick = false;
            }
            if (!(Utility.AllowedAction1(Session["Username"].ToString(), "Manage User Groups")) || id == 1)
            {
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ShowUsers.aspx");
        }

    }
}
