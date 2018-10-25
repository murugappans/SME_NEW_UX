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
    public partial class SafetyPass : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";

        int compID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compID = Utility.ToInteger(Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            //RadGrid1.ItemInserted += new GridInsertedEventHandler(RadGrid1_ItemInserted);
            //RadGrid1.ItemUpdated += new GridUpdatedEventHandler(RadGrid1_ItemUpdated);
        }

        //void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        string ErrMsg = e.Exception.Message;
        //        e.KeepInEditMode = true;
        //        e.ExceptionHandled = true;
        //        if (e.Exception.Message.Contains("IX_safety_type"))
        //            ErrMsg = "Safety Pass already Exists";
        //        if (e.Exception.Message.Contains("Cannot insert the value NULL"))
        //            ErrMsg = "Please Enter Safety Pass";
        //        //DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
        //        _actionMessage = "Warning|" + ErrMsg;
        //        ViewState["actionMessage"] = _actionMessage;
        //    }
        //    else
        //    {
        //       // DisplayMessage("Safety Pass updated successfully.");
        //        _actionMessage = "Success|Safety Pass Type updated successfully.";
        //        ViewState["actionMessage"] = _actionMessage;
        //    }
        //}

        //void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        string ErrMsg = e.Exception.Message;
        //        e.ExceptionHandled = true;
        //        if (e.Exception.Message.Contains("IX_safety_type"))
        //            ErrMsg = "Safety Pass already Exists";
        //        if (e.Exception.Message.Contains("Cannot insert the value NULL"))
        //            ErrMsg = "Please Enter Safety Pass Name";
        //        // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
        //        _actionMessage = "Warning|" + ErrMsg;
        //        ViewState["actionMessage"] = _actionMessage;
        //    }
        //    else
        //    {
        //        //DisplayMessage("Safety Pass added successfully.");
        //        _actionMessage = "Success|Safety Pass Type added successfully.";
        //        ViewState["actionMessage"] = _actionMessage;
        //    }
        //}

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Safety Pass")) == false)
            {

                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
        }

        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from  safetypass_details where safetypass_id=" + id, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the safety pass type.This safety pass type is in use."));
                        _actionMessage = "Warning|Unable to delete the safety pass type.This safety pass type is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [safety_pass] WHERE [id] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Safety Pass type is Deleted Successfully."));
                            _actionMessage = "Success|Safety Pass type Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the safety pass type."));
                            _actionMessage = "Warning|Unable to delete the safety pass type.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
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
        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            try
            {
                string sqlQry = "select * from [safety_pass] where safety_type='" + strtxtName.Trim() + "' and companyid=" + compID +" ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Safety Pass Type already exists.";
                }
                else
                {
                    string ssql = "INSERT INTO [safety_pass] ([safety_type],[companyid]) VALUES ('" + strtxtName.Trim() + "'," + compID + ")";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Safety Pass Type added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'safety_type'"))
                    ErrMsg = "Please Enter Safety Pass Type.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
            try
            {
                string sqlQry = "select * from [safety_pass] where safety_type='" + strtxtName.Trim() + "' and companyid=" + compID + " ";
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Safety Pass Type already exists.";
                }
                else
                {
                    string ssql = "UPDATE [safety_pass] SET safety_type = '" + strtxtName.Trim() + "' WHERE [id] = " + id + "";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Safety Pass Type Updated successfully";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid1.Rebind();
                    //Response.Redirect(Request.RawUrl + "?Country=inserted");
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";

                if (ex.Message.Contains("Cannot insert the value NULL into column 'safety_type'"))
                    ErrMsg = "Please Enter Safety Pass Type.";


                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
    }
}
