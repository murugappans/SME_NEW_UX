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
using efdata;//Added by Jammu Office
using AuditLibrary;
using System.Linq;

namespace SMEPayroll.Leaves
{
    public partial class LeaveTypes : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int LoginEmpcode = 0;//Added by Jammu Office
        string _actionMessage = "";
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            SqlDataSource1.ConnectionString = Session["ConString"]!=null? Session["ConString"].ToString():"";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }

            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
           
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
              compid = Utility.ToInteger(Session["Compid"].ToString());
             // By Jammu Office RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please dont try change leave code from (8 to 16),Because system generated leave."));
            //RadGrid1.Controls.Add(new LiteralControl("<div class='panel-group accordion accordion-note no-margin' id='accordion3'><div class='panel panel-default shadow-none'><div class='panel-heading bg-color-none'><h4 class='panel-title'><a class='accordion-toggle  collapsed' data-toggle='collapse' data-parent='#accordion3' href='#collapse_3_1'><i class='icon-info'></i></a></h4></div><div id = 'collapse_3_1' class='panel-collapse collapse'><div class='panel-body border-top-none no-padding'><div class='note-custom note'>Please dont try change leave code from (8 to 16),Because system generated leave.</div></div></div></div></div>"));
             //RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);
        }   

        //void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridEditableItem && e.Item.IsInEditMode)
        //    {
        //        GridEditableItem item = e.Item as GridEditableItem;
        //        GridTextBoxColumnEditor type = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("type");
        //        type.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_type('" + type.TextBoxControl.ClientID + "')");
        //    }
        //}
       
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //check if the value is Null the uncheck the checkbox else check
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                CheckBox box = (CheckBox)item.FindControl("CheckBox_PaySlip");
                string payslipadd = Utility.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["InPayslip"]);
                if (payslipadd == "NULL" || payslipadd == "")
                {
                    box.Checked = false;
                }
                else
                {
                    box.Checked = true;
                }
            }
            // 
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                CheckBox box = (CheckBox)item.FindControl("CheckBox_SickleaveProrated");
                string SickleaveProrated = Utility.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SickleaveProrated"]);
                int iSsickLeave = Utility.ToInteger(item.OwnerTableView.DataKeyValues[item.ItemIndex]["id"]);

                if (iSsickLeave == 9)
                {

                    if (SickleaveProrated == "NULL" || SickleaveProrated == "")
                    {
                        box.Checked = false;
                    }
                    else
                    {
                        box.Checked = true;
                    }
                }
                else
                {
                    box.Visible = false;
                }
            }

            //-------murugan
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                CheckBox box = (CheckBox)item.FindControl("CheckBox_MomLeaveProrated");
                string MomLeaveProrated = Utility.ToString(item.OwnerTableView.DataKeyValues[item.ItemIndex]["MomLeaveProrated"]);
                int iSsickLeave = Utility.ToInteger(item.OwnerTableView.DataKeyValues[item.ItemIndex]["id"]);

                if (iSsickLeave == 9)
                {

                    if (MomLeaveProrated == "NULL" || MomLeaveProrated == "")
                    {
                        box.Checked = false;
                    }
                    else
                    {
                        box.Checked = true;
                    }
                }
                else
                {
                    box.Visible = false;
                }
            }

            //----------------------

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Leave Types")) == false)
            {                
                //RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                //RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
                //RadGrid1.MasterTableView.GetColumn("EditColumn").Visible = false;
            }
            else
            {
                if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string strSystemDefined = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["CompanyID"]);
                    if (strSystemDefined == "-1")
                    {
                        GridDataItem dataItem = e.Item as GridDataItem;
                        dataItem.Cells[5].Controls[0].Visible = false;
                        dataItem.Cells[6].Controls[0].Visible = false;
                    }
                }
            }
        }
     
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);
                int Id = Convert.ToInt32(id);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_leaves  where leave_type=" + id, null);
                if (dr.Read())
                {
                    if (dr[0].ToString() != "0")
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type.This leave type is in use."));
                        _actionMessage = "Warning|Unable to delete the leave type.This leave type is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [leave_types] WHERE [id] =" + id;
                        //Added by Jammu Office
                        #region Audit
                        var oldrecord = new LeaveType();
                        using (var _context = new AuditContext())
                        {
                            oldrecord = _context.LeaveTypes.Where(m => m.Id == Id).FirstOrDefault();
                        }
                        var newrecord = new LeaveType();
                       
                        var AuditRepository = new AuditRepository();
                        AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, Id, oldrecord, newrecord);
                        #endregion

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal == 1)
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Leave Type is Deleted Successfully."));
                            _actionMessage = "sc|Leave Type is Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        else
                        {
                           // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type."));
                            _actionMessage = "Warning|Unable to delete the leave type.";
                            ViewState["actionMessage"] = _actionMessage;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";
                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
                _actionMessage = "Warning|Unable to delete record. Reason: "+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }
        }

        protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem data = e.Item as GridEditFormItem;
                TextBox txt = data.FindControl("TextBox1") as TextBox;


                try
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);


                    string sql = "UPDATE [leave_types] SET [type] ='" + txt.Text + "' WHERE [id] = " + id + " AND [id] NOT IN(8,9,10,11,12,13,14,15,16)";
                    int recount = DataAccess.ExecuteNonQuery(sql, null);
                    if (recount == 1)
                    {
                        _actionMessage = "sc|Leave Type is Updated Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                        e.Item.Edit = false;
                    }
                    else
                    {

                        _actionMessage = "Warning|Unable to Update the leave type.";
                        ViewState["actionMessage"] = _actionMessage;
                    }


                }


                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("duplicate key", 1) > 0)
                        ErrMsg = "Record can not be update becuase of Duplicate Value.";

                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                    e.Canceled = true;
                }
            }
            }
        //Update in table 
        //whether it is need in paysip ornot
        protected void CheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
 int Id = Convert.ToInt32(box.ToolTip);
            if (box.Checked)
            {
                string ssqlb = "UPDATE [leave_types] SET [InPayslip] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new LeaveType();
                using (var _context = new AuditContext())
                {
                    oldrecord = _context.LeaveTypes.Where(m => m.Id == Id).FirstOrDefault();
                }
                var newrecord = new LeaveType() {
                    Id= Id,
                    Type= oldrecord.Type,
                    Companyid=oldrecord.Companyid,
                    Code=oldrecord.Code,
                    InPayslip=1,
                    SickleaveProrated=oldrecord.SickleaveProrated
                };

                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, Id, oldrecord, newrecord);
                #endregion

                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }
            else
            {
                string ssqlb = "UPDATE [leave_types] SET [InPayslip] = CAST(NULL AS INT)  WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new LeaveType();
                using (var _context = new AuditContext())
                {
                    oldrecord = _context.LeaveTypes.Where(m => m.Id == Id).FirstOrDefault();
                }
                var newrecord = new LeaveType()
                {
                    Id = Id,
                    Type = oldrecord.Type,
                    Companyid = oldrecord.Companyid,
                    Code = oldrecord.Code,
                    SickleaveProrated = oldrecord.SickleaveProrated
                };

                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, Id, oldrecord, newrecord);
                #endregion
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }

        }
        protected void SickleaveProratedCheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            int Id = Convert.ToInt32(box.ToolTip);
            if (box.Checked)
            {
                string ssqlb = "UPDATE [leave_types] SET [SickleaveProrated] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new LeaveType();
                using (var _context = new AuditContext())
                {
                    oldrecord = _context.LeaveTypes.Where(m => m.Id == Id).FirstOrDefault();
                }
                var newrecord = new LeaveType()
                {
                    Id = Id,
                    Type = oldrecord.Type,
                    Companyid = oldrecord.Companyid,
                    Code = oldrecord.Code,
                    InPayslip = oldrecord.InPayslip,
                    SickleaveProrated = 1
                };

                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, Id, oldrecord, newrecord);
                #endregion

                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                ssqlb = "UPDATE [leave_types] SET [MomLeaveProrated] = '0' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);

            }
            else
            {
                string ssqlb = "UPDATE [leave_types] SET [SickleaveProrated] = 0  WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new LeaveType();
                using (var _context = new AuditContext())
                {
                    oldrecord = _context.LeaveTypes.Where(m => m.Id == Id).FirstOrDefault();
                }
                var newrecord = new LeaveType()
                {
                    Id = Id,
                    Type = oldrecord.Type,
                    Companyid = oldrecord.Companyid,
                    Code = oldrecord.Code,
                    InPayslip = oldrecord.InPayslip,
                    SickleaveProrated = 0
                };

                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, Id, oldrecord, newrecord);
                #endregion
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }
            RadGrid1.Rebind();

        }
        protected void MomleaveProratedCheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;

            if (box.Checked)
            {
                string ssqlb = "UPDATE [leave_types] SET [MomLeaveProrated] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);

                ssqlb = "UPDATE [leave_types] SET [SickleaveProrated] = '0' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }
            else
            {
                string ssqlb = "UPDATE [leave_types] SET [MomLeaveProrated] = 0  WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
            }
            RadGrid1.Rebind();
        }

        protected bool GetPaySlip(object InPayslip)
        {
            if (Convert.ToString(InPayslip) =="NULL")
            {
                return false;
            }
            else
            {
                return false;
            }

        }
        protected bool GetSickleaveProrated(object SickleaveProrated)
        {
            if (Convert.ToString(SickleaveProrated) == "NULL")
            {
                return false;
            }
            else
            {
                return false;
            }

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

      

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {

            if (e.Exception != null)
            {
                string ErrMsg = e.Exception.Message;
                e.ExceptionHandled = true;

                if (e.Exception.Message.Contains("UNIQUE KEY constraint") || e.Exception.Message.Contains(" duplicate key"))
                {
                    ErrMsg = "Name Already Exists";


                    _actionMessage = "Warning|" + ErrMsg;
                    ViewState["actionMessage"] = _actionMessage;
                }
            }
            else
            {
                _actionMessage = "Success|Leave Type added successfully...";
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {

            _actionMessage = "Success|Leave Type Updated successfully...";
            ViewState["actionMessage"] = _actionMessage;
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
