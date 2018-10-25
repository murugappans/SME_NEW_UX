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
    public partial class JobTitle : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int compID;
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor cid = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("cat_id");
        //        cid.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_cat_id('" + cid.TextBoxControl.ClientID + "')");

        //        GridTextBoxColumnEditor ctitle = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("cat_title");
        //        ctitle.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_cat_title('" + ctitle.TextBoxControl.ClientID + "')");
        //    }
        //}

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Designation")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                // SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where desig_id=" + id, null);
                // if (dr.Read())
                // {
                //  if (dr[0].ToString() != "0" || id == "13") // ID == 13 CONDITION IS ONLY FOR COFFEESHOP
                //  {
                //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the designation.This designation is in use."));
                //  }
                //  else
                //  {
                string sSQL = "DELETE FROM [JobTitle] WHERE [id] =" + id;

                int retVal = DataAccess.ExecuteStoreProc(sSQL);

                if (retVal == 1)
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'> Job Title is Deleted Successfully."));
                    _actionMessage = "Success|Job Title Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;

                }
                else
                {
                    //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Job Title."));
                    _actionMessage = "Warning|Unable to delete the Job Title.";
                    ViewState["actionMessage"] = _actionMessage;

                }

                // }
                //}

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }
        //protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        e.ExceptionHandled = true;
        //        if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
        //            DisplayMessage("<font color = 'red'> Job Category can not be added. Reason: Job Category already Exists.</font>");
        //        else
        //            DisplayMessage("<font color = 'red'> Job Category can not be added. Reason: " + e.Exception.Message + "</font>");
        //    }
        //    else
        //    {
        //        DisplayMessage("Job Category added successfully.");
        //    }
        //}
        //protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        e.KeepInEditMode = true;
        //        e.ExceptionHandled = true;
        //        if (e.Exception.Message.Contains("Violation of PRIMARY KEY"))
        //            DisplayMessage("<font color = 'red'> Job Category can not be updated. Reason: Job Category already Exists.</font>");
        //        else
        //            DisplayMessage("<font color = 'red'> Job Category can not be updated. Reason: " + e.Exception.Message + "</font>");
        //    }
        //    else
        //    {
        //        DisplayMessage("Job Category updated successfully.");
        //    }
        //}
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }
        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"];

            string drpAddId = (userControl.FindControl("drpVariable") as DropDownList).SelectedItem.Value;
            string txtbox = (userControl.FindControl("txtTitle") as TextBox).Text;
            //Validation 
            string sql = "select * from JobTitle where cat_title='" + txtbox + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            //
            if (dr.HasRows)
            {
                //string ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                //RadGrid1.Controls.Add(new LiteralControl(ErrMsg));
                _actionMessage = "Warning|Job Title Already Exist.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
               // string ssqlb = "UPDATE  [JobTitle] SET [cat_id] = " + Convert.ToInt32(drpAddId) + ", [cat_title]='"+txtbox +"' WHERE [id]='" + id + "'";
                string ssqlb = "UPDATE  [JobTitle] SET [cat_title]='" + txtbox + "' WHERE [id]='" + id + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                _actionMessage = "Success|Job Title Updated Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            //RadGrid1.DataBind();
            //userControl.Visible = false;
            //RadGrid1.MasterTableView.ClearEditItems();
            
        }


        protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);


            string drpAddId = (userControl.FindControl("drpVariable") as DropDownList).SelectedItem.Value;
            string txtbox = (userControl.FindControl("txtTitle") as TextBox).Text;
            //Validation 
            string sql = "select * from JobTitle where cat_title='" + txtbox +"'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            //
            if (dr.HasRows)
            {
                //string ErrMsg = "<font color = 'Red'>Already Exist.</font>";
                //RadGrid1.Controls.Add(new LiteralControl(ErrMsg));
                _actionMessage = "Warning| Job Title Already Exist.";
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
            else
            {
                string ssqlb = "Insert into  [JobTitle]  ([cat_id],[cat_title])values(" + Convert.ToInt32(drpAddId) + ",'" + txtbox + "')";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                _actionMessage = "Success|Job Title Inserted Successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
            //userControl.Visible = false;
           // RadGrid1.DataBind();
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
