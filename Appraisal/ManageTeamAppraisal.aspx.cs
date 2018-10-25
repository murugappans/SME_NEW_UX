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

namespace SMEPayroll.Appraisal
{
    public partial class ManageTeamAppraisal : System.Web.UI.Page
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
            //if (!Utility.showweeklyroster())
            //{
            //    this.AddEmpRoster.Visible = false;
            //}
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
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
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Roster")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //    RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            //}

            //if (Utility.showweeklyroster())
            //{
            //    RadGrid1.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = false;
            //}
            RadGrid1.MasterTableView.CommandItemSettings.ShowAddNewRecordButton = true;
        }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

                //Check If Roster Is Assigned To EMployee Then Wont Allow To Delete the Employee
                string sqlEas = "SELECT count(*) FROM TeamAppraisalAssigned  WHERE Team_ID=" + id;
                int retVal1 = DataAccess.ExecuteScalar(sqlEas, null);
                if (retVal1 > 0)
                {
                   // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Team.Please UnAssign The Employee from Team"));
                    _actionMessage = "Warning|Unable to delete the Team.  <br> Please UnAssign The Employee from Team.";
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }

              //  SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) From TeamDetail Where Team_ID=" + id, null);
               // if (dr.Read())
               // {
                    //if (Convert.ToInt16(dr[0].ToString()) > 0)
                    //{
                    //    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Team. This Team is in use."));
                    //}
                    //else
                    //{
                        string sSQL = "DELETE FROM [TeamAppraisal] WHERE [id] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                          //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Team is Deleted Successfully."));
                    _actionMessage = "Success|Team is Deleted Successfully..";

                }
                else
                        {
                           // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Team."));
                    _actionMessage = "Warning|Unable to delete the Team.";
                        }
                ViewState["actionMessage"] = _actionMessage;
                // }
                // }
            }
            catch (Exception ex)
            {
                string ErrMsg = "Some Error Occured";
                if (ex.Message.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
              //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void AddEmpClick(object sender, EventArgs e)
        {
            try
            {
                string sSQL = "sp_addTeamAppraisal";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));

                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                string sSQL1 = "sp_assignTeam";
                SqlParameter[] parms1 = new SqlParameter[1];
                parms1[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));

                DataSet ds1 = DataAccess.ExecuteSPDataSet(sSQL1, parms1);




                Response.Redirect(Request.RawUrl);

            }
            catch (Exception ex)
            {
                string ErrMsg = "Some Error Occured";

               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Error:</font> " + ErrMsg));
                _actionMessage = "Warning|Error: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = "Some Error Occured";
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_RosName"))
                    ErrMsg = "Appraisal Team already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Appraisal Team ";
               // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|" + ErrMsg;
            }
            else
            {
                //DisplayMessage("Appraisal Team  added successfully.");
                _actionMessage = "Success|Appraisal Team  added successfully.";
            }
            ViewState["actionMessage"] = _actionMessage;
        }

        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = "Some Error Occured";
                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("IX_RosName"))
                    ErrMsg = "Appraisal Team  already Exists";
                if (e.Exception.Message.Contains("Cannot insert the value NULL"))
                    ErrMsg = "Please Enter Appraisal Team ";
              //  DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|>" + ErrMsg;
            }
            else
            {
               // DisplayMessage("Appraisal Team  updated successfully.");
                _actionMessage = "Success|Appraisal Team  updated successfully.";
            }
            ViewState["actionMessage"] = _actionMessage;
        }

        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
    }
}

