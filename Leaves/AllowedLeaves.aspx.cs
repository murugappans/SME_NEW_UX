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

namespace SMEPayroll.Leaves
{
    public partial class AllowedLeaves : System.Web.UI.Page
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
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            int i;
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];
            string groupid = (userControl.FindControl("drpGroup") as DropDownList).SelectedItem.Value;
            string leave_type = (userControl.FindControl("drpLeaveType") as DropDownList).SelectedItem.Value;
            string leavesallowed = (userControl.FindControl("textbox1") as TextBox).Text;

            string sSQL = "";
            if (Utility.ToString(id) == "")
            {
                sSQL = "Insert into leaves_allowed (group_id,leave_type,leaves_allowed) values(" + groupid + "," + leave_type + "," + leavesallowed + ")";
            }
            else
               sSQL = "update leaves_allowed set leaves_allowed=" + leavesallowed + " where id="+id;
             try
              {
               i = DataAccess.ExecuteStoreProc(sSQL);
              }
             catch (Exception ex)
              {
              string ErrMsg = ex.Message;
              if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                 {
                   ErrMsg = "Unable to add the record.Please try again. ";
                }
 //ErrMsg = "<font color = 'Red'>Unable to add the record.Please try again.</font>";
                   //RadGrid1.Controls.Add(new LiteralControl(ErrMsg));
                    _actionMessage = "Warning|"+ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                
              }
        }

        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
                object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];
                string sSQL = "DELETE FROM leaves_allowed where id="+id;
                sSQL = string.Format(sSQL, id);
                int i = DataAccess.ExecuteStoreProc(sSQL);

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = " Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";

                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Leaves Allowed")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                  RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }
    }
}
