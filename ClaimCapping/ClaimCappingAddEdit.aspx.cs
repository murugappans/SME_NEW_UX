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
using efdata;
using AuditLibrary;
using System.Linq;

namespace SMEPayroll.Management
{
    public partial class ClaimCappingAddEdit : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int LoginEmpcode = 0;//Added by Jammu Office
        string _actionMessage = "";
        int compid = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
            {
                Response.Redirect("../SessionExpire.aspx");
            }


            

            if (!Page.IsPostBack)
            {
            }

            //SessionDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Workflow")) == false)
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

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select Count(ID) CNT From EmployeeWorkFlowLevel Where WorkflowID=" + id, null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                      //  RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Workflow which is in use."));
                        _actionMessage = "Warning|Unable to delete the Claim Capping Group which is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [ClaimCappingGroup] WHERE [id] =" + id;
                        var oldrecord = new ClaimCaping();
                        int ID = Convert.ToInt32(id);
                        using (var _context = new AuditContext())
                        {
                           // oldrecord = _context.ClaimCapings.Where(m => m.Id == ID).FirstOrDefault();
                        }
                        int retVal = DataAccess.ExecuteStoreProc(sSQL);


                        if (retVal == 1)
                        {
                            //Added by Jammu Office
                            #region Audit
                            
                            var newrecord = new ClaimCaping();
                            var AuditRepository = new AuditRepository();
                            if(oldrecord != null)
                            {
                                AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, ID, oldrecord, newrecord);
                            }
                          

                            #endregion
                           // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>[ClaimCappingGroup] is Deleted Successfully."));
                            _actionMessage = "Success|Claim Capping Group is Deleted Successfully";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                            // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the [ClaimCappingGroup]."));
                            _actionMessage = "Warning|Unable to delete the Claim Capping Group.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = "Some error occured. Try again later.";
                if (ex.Message.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
               // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason: " + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }
        //protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        //{
        //    if (e.Exception != null)
        //    {
        //        string ErrMsg = e.Exception.Message;
        //        e.ExceptionHandled = true;
        //        if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'CliamGroupName'"))
        //            ErrMsg = "Please Enter Cliam Group Name";

        //       // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
        //        _actionMessage = "Warning|"+ ErrMsg;
        //        ViewState["actionMessage"] = _actionMessage;
        //    }
        //    else
        //    {
        //        _actionMessage = "Success|Claim Capping Group added successfully";
        //        ViewState["actionMessage"] = _actionMessage;
        //       // DisplayMessage("Claim Capping Group added successfully.");
        //    }
        //}
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;
                if (e.Exception.Message.Contains("Cannot insert the value NULL into column 'CliamGroupName'"))
                    ErrMsg = "Please Enter Cliam Group Name";

               // DisplayMessage("<font color = 'red'>" + ErrMsg + ".</font>");
                _actionMessage = "Warning|"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
            else
            {
               // DisplayMessage("ClaimCapping Group updated successfully.");
                _actionMessage = "Success|Claim Capping Group updated successfully.";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            string strtxtName = ((TextBox)e.Item.FindControl("TextBox1")).Text.ToString().Trim();
            try
            {
                string sqlQry = "select * from ClaimCappingGroup where CliamGroupName='" + strtxtName.Trim() + "' and Company_ID = "+compid;
                SqlDataReader sqldr;
                sqldr = DataAccess.ExecuteReader(CommandType.Text, sqlQry, null);
                if (sqldr.Read())
                {
                    ViewState["actionMessage"] = "Warning|Claim Capping Group already exists.";

                }
                else
                {
                    string ssql = "INSERT INTO [ClaimCappingGroup] (CliamGroupName,Company_ID) VALUES ('" + strtxtName.Trim() + "'," + compid + ")";
                    DataAccess.ExecuteNonQuery(ssql, null);
                    _actionMessage = "Success|Claim Capping Group added successfully";
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            catch (Exception ex)
            {

                string ErrMsg = "Some Error Occured. Try again later.";
               
                if (ex.Message.Contains("Cannot insert the value NULL into column 'CliamGroupName'"))
                    ErrMsg = "Please Enter Cliam Group Name.";

                
                _actionMessage = "Warning|" + ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
            }
        }
    }
}
