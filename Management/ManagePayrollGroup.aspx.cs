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
using System.Collections.Generic;

namespace SMEPayroll.Management
{
    public partial class ManagePayrollGroup : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"].ToString());
            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }




            SqlDataSource1.ConnectionString = Session["ConString"].ToString();


        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                string sSQL = "DELETE FROM [PayrollGroup] WHERE [id] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Addition type  is deleted."));
                    _actionMessage = "success|Payroll Group Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the addition type."));
                    _actionMessage = "Warning|Unable to delete the Payroll Group.";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
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

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
                       GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string WorkflowtypedrpValue = (userControl.FindControl("Workflowtypedrp") as DropDownList).SelectedItem.Value;
            
            string id = (userControl.FindControl("IdLabel") as Label).Text;
            string groupName = (userControl.FindControl("GroupNameTextBox") as TextBox).Text;
            string sql = "insert into Payrollgroup ([GroupName],[Company_ID],WorkflowtyprID) values('" + groupName + "'," + compid + ","+WorkflowtypedrpValue+")";

            try
            {
                int retVal = DataAccess.ExecuteNonQuery(sql);
                _actionMessage = "success|Payroll Group added Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            string WorkflowtypedrpValue = (userControl.FindControl("Workflowtypedrp") as DropDownList).SelectedValue.ToString();
            string wfname = (userControl.FindControl("Workflowtypedrp") as DropDownList).SelectedItem.Text;

            string id = (userControl.FindControl("IdLabel") as Label).Text;
            string groupName = (userControl.FindControl("GroupNameTextBox") as TextBox).Text;


            if (wfname == "Payroll")
                WorkflowtypedrpValue = "1";
            else if(wfname == "Leave")
                WorkflowtypedrpValue = "2";
            else if (wfname == "Claims")
                WorkflowtypedrpValue = "3";
            else if (wfname == "TimeSheet")
                WorkflowtypedrpValue = "4";
            else if (wfname == "Appraisal")
                WorkflowtypedrpValue = "5";
            string sql = "Update PayrollGroup set GroupName = '" + groupName + "' ,WorkflowtyprID =" + WorkflowtypedrpValue + " where ID=" + id + "";
                     
                 
                    

            try
            {
                int retVal = DataAccess.ExecuteNonQuery(sql);
                _actionMessage = "success|Payroll Group updated Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }




        }
    }
}
