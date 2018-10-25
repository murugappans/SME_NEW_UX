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

namespace SMEPayroll
{
    public partial class Employee_Groups : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }


            if (!IsPostBack)
            {
                RadGrid1.ExportSettings.FileName = "Employee_Groups";
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            else
            {
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Employee Groups")) == false)
                {
                    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
                }
            }
        }

        protected void RadGrid1_Delete(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
               
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string EmpCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where emp_group_id=" + EmpCode, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the employee group.This employee group is in use in Employee Info."));
                        _actionMessage = "Warning|Unable to delete the employee group.This employee group is in use in Employee Info.";
                    }
                    else
                    {
                        string sSQL = "DELETE FROM emp_group where id ={0}";
                        sSQL = string.Format(sSQL, EmpCode);
                        int i = DataAccess.ExecuteStoreProc(sSQL);
                        _actionMessage = "Success|Deleted successfully.";
                    }
                }
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("FK_leaves_allowed_leave_types", 1) > 0)
                {
                   // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This record is in Use in Allowed Leaves.</font> "));
                    _actionMessage = "Warning|This record is in Use in Allowed Leaves";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                   // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the employee group .This employee group is in use.</font> "));
                    _actionMessage = "Warning|>Unable to delete the employee group .This employee group is in use.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                e.Canceled = true;
            }
        }

     


        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;// UniqueName="DeleteColumn"
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e, tbRecord);
        }
        //Added By Jammu Office ////////////
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert duplicate key in object 'dbo.emp_group"))
                    ErrMsg = "Employee Group already Exist";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Employee Group, Empty Employee Group can not be added";
               // DisplayMessage("<font color = 'red'> Employee Group can not be added. Reason: " + ErrMsg + ".</font>");
                _actionMessage = "Warning|"+ ErrMsg ;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Employee Group added successfully.");
                _actionMessage = "success|Employee Group added successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
        //Added By Jammu Office ends ////////////
        #endregion
        //Toolbar End
        // murugan
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert duplicate key in object 'dbo.emp_group"))
                    ErrMsg = "Employee Group already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Employee Group, Empty Employee Group can not be added";
                // DisplayMessage("<font color = 'red'> Employee Group can not be added. Reason: " + ErrMsg + ".</font>");
                _actionMessage = "Warning|employee Group can not be added. Reason:" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
                //DisplayMessage("Employee Group added successfully.");
                _actionMessage = "success|Employee Group Updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.EditCommandName)
            {
                RadGrid1.MasterTableView.IsItemInserted = false;
            }
            if (e.CommandName == RadGrid.InitInsertCommandName)
            {
                RadGrid1.MasterTableView.ClearEditItems();
            }
        }
    }
}
