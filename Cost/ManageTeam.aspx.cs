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
    public partial class ManageTeam : System.Web.UI.Page
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
                int id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[e.Item.ItemIndex].GetDataKeyValue("Tmid"));
                if (id != 0)
                {
                    string TeamName = Utility.ToString(this.RadGrid1.MasterTableView.Items[e.Item.ItemIndex].GetDataKeyValue("TeamName"));
                    Response.Redirect("../Cost/AssignMember.aspx?id=" + id + "&Team=" + TeamName);
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
                string sSQL = "SELECT  [Tmid],[TeamName],( select isnull(emp_name,'')+' '+isnull(emp_lname,'') emp_name from employee where emp_code=CT.[TeamLead])as emp_Lead,[TeamLead] as LeadId  FROM [cost_Team] CT where company_id=" + id + " order by  [Tmid]";
                //string sSQL = "select GroupID, GroupName,'Rights' Rights from UserGroups  where company_id=" + id + " order by 1";
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

            string TeamName = (userControl.FindControl("txtTeamname") as TextBox).Text;
            string Leadid = (userControl.FindControl("drpTeamLead") as DropDownList).SelectedItem.Value;
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Tmid"];
   
            try
            {
                int Tid = Int32.Parse(id.ToString());
                string sSQL = "UPDATE [cost_Team] SET [TeamName] = '"+TeamName.ToString()+"' ,[TeamLead] = '"+Int32.Parse(Leadid.ToString())+"' WHERE [Tmid]=" + Tid +"";
                DataAccess.FetchRS(CommandType.Text, sSQL, null);
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record updated successfully</font> "));
                _actionMessage = "success|Team updated successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception Ex)
            {
              //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record not updated </font> "));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;
            }
           
        }
        #endregion Update command

        #region Insert command
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            string TeamName = (userControl.FindControl("txtTeamname") as TextBox).Text;
            string Leadid = (userControl.FindControl("drpTeamLead") as DropDownList).SelectedItem.Value;

            try
            {
                int Tid = Int32.Parse(id.ToString());
                string sSQL = "INSERT INTO [cost_Team] ([TeamName],[TeamLead] ,[Company_id])  VALUES ('" + TeamName.ToString() + "','" + Int32.Parse(Leadid.ToString()) + "','" + id + "')";
                DataAccess.FetchRS(CommandType.Text, sSQL, null);
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record Inserted successfully</font> "));
                _actionMessage = "success|Team Added successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception Ex)
            {
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record not Inserted </font> "));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;

            }
        }
        #endregion Insert command

        #region Delete command

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object Tid = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Tmid"];
            try
            {
                int Tid1 = Int32.Parse(Tid.ToString());
                string sSQL = "DELETE FROM [cost_Team] WHERE Tmid='" + Tid1 + "'";
                DataAccess.FetchRS(CommandType.Text, sSQL, null);
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record deleted successfully</font> "));
                _actionMessage = "success|Team deleted successfully";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception Ex)
            {
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Record not Deleted </font> "));
                _actionMessage = "Warning|Some error occured.Please try again later.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        #endregion Delete command

        


    }

}
