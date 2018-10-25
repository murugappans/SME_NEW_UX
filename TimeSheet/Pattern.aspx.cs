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

namespace SMEPayroll.TimeSheet
{

    public partial class Pattern : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
        }

       
        void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete" && e.CommandName != "Edit")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                Button btn = (Button)dataItem["Rid"].FindControl("btnTimeSlot");
               
                int id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[e.Item.ItemIndex].GetDataKeyValue("Rid"));
              string OUTLET = dataItem["GridDropDownColumn"].Text;
              string pattern = dataItem["Pattern"].Text;
           
                
                if (id != 0)
                {
                   // Session["varGroupID"] = id.ToString();
                    Response.Redirect("../TimeSheet/TimeSlot.aspx?Pattern="+id+"&ot="+OUTLET+"&pt="+pattern+"");
                }
            }
        }
        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor Pattern = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Pattern");
                Pattern.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_Patern('" + Pattern.TextBoxControl.ClientID + "')");
            }
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Trade Types")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            //}
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Rid"]);

                string sSQL = "DELETE FROM [Roaster_Pattern] WHERE [Rid] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Patern is Deleted Successfully."));

                }
                else
                {
                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Patern."));
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                    DisplayMessage("<font color = 'red'> Patern can not be added. Reason: Patern already Exists.</font>");
                else
                    DisplayMessage("<font color = 'red'> Patern can not be added. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                DisplayMessage("Patern added successfully.");
            }
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
                    DisplayMessage("<font color = 'red'> Patern can not be updated. Reason: Patern already Exists.</font>");
                else
                    DisplayMessage("<font color = 'red'> Patern can not be updated. Reason: " + e.Exception.Message + "</font>");
            }
            else
            {
                DisplayMessage("Patern updated successfully.");
            }
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }
    }




}
