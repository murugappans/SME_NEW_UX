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
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace SMEPayroll.Management
{
    public partial class EmployeeWorkFlowLevel : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet ds;
        protected int comp_id;
        string _actionMessage = "";
        string strMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                string WFLevel = Request.QueryString["WFLevel"];
                string WfName = Request.QueryString["WorkFlowName"];
                string Wftype = Request.QueryString["WorkFlowType"];
                if (WFLevel == "inserted")
                {
                    drpWorkFlowID.DataBind();
                    drpType.SelectedValue = Wftype;
                    drpWorkFlowID.SelectedValue = WfName;
                    bindgriddrop();
                    _actionMessage = "Success|Workflow Level Added Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                else
                {
                    ViewState["actionMessage"] = "";
                }
            }

        }
        protected void drpWorkFlowID_databound(object sender, EventArgs e)
        {
            drpWorkFlowID.Items.Insert(0, new ListItem("-select-", "-1"));
            //drpWorkFlowID.Items.FindByValue("-1").Selected = true;
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Workflow Level")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
            else
            {
                if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
                {
                    GridEditableItem editedItem = e.Item as GridEditableItem;
                    string RowID = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["RowID"]);
                    int icnt = Utility.ToInteger(RowID.ToString().Substring(2, RowID.ToString().Length - 2));
                    if (icnt != ds.Tables[0].Rows.Count)
                    {
                        editedItem["DeleteColumn"].Visible = false;
                    }
                }
            }
         }

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

                //Ram added 
                #region Unassign Employee from Payroll Group
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "select Count(Emp_ID) from employeeassignedtopayrollgroup where payrollgroupid=(select PayRollGroupID from EmployeeWorkFlowLevel where id='"+id.ToString() +"')", null);
                if (dr1.Read())
                {
                    if (Convert.ToInt16(dr1[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Workflow Level.Unassign Employee from Payroll Group."));
                        _actionMessage = "Warning|Unable to delete the Workflow Level.Unassign Employee from Payroll Group.";
                        ViewState["actionMessage"] = _actionMessage;
                        return;
                    }
                }
                #endregion

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select  Count(Emp_Code) From Employee Where Pay_Supervisor= " + id.ToString(), null);
                if (dr.Read())
                {
                    if (Convert.ToInt16(dr[0].ToString()) > 0)
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Workflow Level.This Workflow Level is in use."));
                        _actionMessage = "Warning|Unable to delete the Workflow Level.This Workflow Level is in use.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {
                        string sSQL = "DELETE FROM [EmployeeWorkFlowLevel] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                            bindgriddrop();
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Workflow Level is Deleted Successfully."));
                            _actionMessage = "sc|Workflow Level is Deleted Successfully.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        else
                        {
                            //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Workflow Level."));
                            _actionMessage = "Warning|Unable to delete the Workflow Level.";
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
                _actionMessage = "Warning|Unable to delete record. Reason:"+ ErrMsg;
                ViewState["actionMessage"] = _actionMessage;
                e.Canceled = true;
            }

        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }

        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }

        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        string sStringSql()
        {
            string strtype = "0";
            string strwfid = "0";
            if (drpType.SelectedItem != null)
            {
                if (drpType.SelectedItem.Value != "-select-")
                {
                    strtype = drpType.SelectedItem.Value.ToString();
                }
            }
            if (drpWorkFlowID.SelectedItem != null)
            {
                if (drpWorkFlowID.SelectedItem.Value != "-select-")
                {
                    strwfid = drpWorkFlowID.SelectedItem.Value.ToString();
                }
            }
            string sSQL = "Select WL.ID, 'L-'+ Cast(WL.RowID as varchar(5)) RowID, WF.ID,WF.WorkFlowName,'FlowType'=Case When WL.FlowType=1 Then 'Payroll' When WL.FlowType=2 Then 'Leave' When WL.FlowType=3 Then 'Claims'  When WL.FlowType=4 Then 'TimeSheet' When WL.FlowType=5 Then 'Appraisal' End, PG.GroupName,WL.ACTION,WL.ExpiryDays From EmployeeWorkFlowLevel WL Inner Join EmployeeWorkFlow WF On WL.WorkFlowID = WF.ID Inner Join PayrollGroup PG On WL.PayRollGroupID = PG.ID Where WF.ID=" + strwfid + " And WL.FlowType=" + strtype + " Order By WL.RowID";
            return sSQL;
        }

        string sStringSql1()
        {
            string strtype = "1";
            string strwfid = "0";
            strtype = drpType.SelectedItem.Value.ToString();

           
             if (drpType.SelectedItem != null)
            {
                if (drpType.SelectedItem.Value != "-select-")
                {
                    strtype = drpType.SelectedItem.Value.ToString();
                }
            }
            if (drpWorkFlowID.SelectedItem != null)
            {
                if (drpWorkFlowID.SelectedItem.Value != "-select-")
                {
                    strwfid = drpWorkFlowID.SelectedItem.Value.ToString();
                }
            }
            string sSQL = "Select ID, GroupName from PayrollGroup Where Company_ID=" + comp_id.ToString() + " And ID not in (Select PayrollGroupID From EmployeeWorkFlowLevel Where WorkFlowID=" + strwfid + " And FlowType=" + strtype + ")and WorkflowtyprID=" + strtype + "";
            return sSQL;
        }


        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue.ToString() == "2")
            {
                row1.Visible = true;
                row2.Visible = true;
                row4.Visible = true;
                row5.Visible = true;
            }
            else
            {
                row1.Visible = false;
                row2.Visible = false;
                row4.Visible = false;
                row5.Visible = false;
            }
            bindgriddrop();
        }

        protected void drpWorkFlowID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = drpWorkFlowID.SelectedItem.Value.ToString();
            if (ID != "-1")
            {
                bindgriddrop();
            }
        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }

        void bindgriddrop()
        {
            ds = new DataSet();
            string sSQL = sStringSql();
            sSQL = sSQL +";"+ sStringSql1();            
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            RadGrid1.Visible = true;
            RadGrid1.DataSource = ds.Tables[0];
            RadGrid1.DataBind();

            drpPayrollGroup.DataSource = ds.Tables[1];
            drpPayrollGroup.DataTextField = ds.Tables[1].Columns["GroupName"].ColumnName.ToString();
            drpPayrollGroup.DataValueField = ds.Tables[1].Columns["ID"].ColumnName.ToString();
            drpPayrollGroup.DataBind();
            drpPayrollGroup.Items.Insert(0, "-select-");
            drpPayrollGroup.Items.FindByValue("-select-").Selected = true;

            if (drpType.SelectedValue.ToString() == "2")
            {
                RadGrid1.MasterTableView.Columns[5].Visible = true;
                RadGrid1.MasterTableView.Columns[6].Visible = true;
            }
            else 
            {
                RadGrid1.MasterTableView.Columns[5].Visible = false;
                RadGrid1.MasterTableView.Columns[6].Visible = false;
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string sSQL = "";
            if (drpWorkFlowID.SelectedItem != null)
            {
                if (drpWorkFlowID.SelectedItem.Value == "-1")
                {
                    strMessage = strMessage + "<br/>" + "Work Flow Name Cannot Remain Blank";
                }
            }
            else
            {
                strMessage = strMessage + "<br/>" + "Work Flow Name Cannot Remain Blank";
            }
            if (drpPayrollGroup.SelectedItem != null)
            {
                if (drpPayrollGroup.SelectedItem.Value == "-select-")
                {
                    strMessage = strMessage + "<br/>" + "Payroll Group Name Cannot Remain Blank";
                }
            }
            else
            {
                strMessage = strMessage + "<br/>" + "Payroll Group Name Cannot Remain Blank";
            }


            if (strMessage.Length > 0)
            {
                //ShowMessageBox();
                _actionMessage = "Warning|"+strMessage;
                ViewState["actionMessage"] = _actionMessage;
                strMessage = "";
            }
            else
            {
                DataSet dsnew = new DataSet();
                sSQL = "Select WL.ID, 'L-'+ Cast(WL.RowID as varchar(5)) RowID, WF.ID,WF.WorkFlowName, PG.GroupName From EmployeeWorkFlowLevel WL Inner Join EmployeeWorkFlow WF On WL.WorkFlowID = WF.ID Inner Join PayrollGroup PG On WL.PayRollGroupID = PG.ID Where WF.ID='" + drpWorkFlowID.SelectedItem.Value + "'  And FlowType!=" + drpType.SelectedItem.Value;
                dsnew = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    strMessage = strMessage + "<br/>" + "This Workflow Already  exist with the other Flow Type";
                    //ShowMessageBox();
                    _actionMessage = "Warning|"+strMessage;
                    ViewState["actionMessage"] = _actionMessage;
                    strMessage = "";
                }
                else
                {
                    SqlParameter[] parms = new SqlParameter[6];
                    sSQL = "sp_EmployeeWorkflowLevel";
                    parms[0] = new SqlParameter("@WorkflowID", Utility.ToInteger(drpWorkFlowID.SelectedItem.Value.ToString()));
                    parms[1] = new SqlParameter("@PayrollGroupID", Utility.ToInteger(drpPayrollGroup.SelectedItem.Value.ToString()));
                    parms[2] = new SqlParameter("@FlowType", Utility.ToInteger(drpType.SelectedItem.Value.ToString()));

                    parms[3] = new SqlParameter("@ExpiryDays", Utility.ToInteger(txtExpirayDays.Text));
                    parms[4] = new SqlParameter("@Action", Utility.ToString(drpAction.SelectedItem.Text));

                    parms[5] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[5].Direction = ParameterDirection.Output;
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        bindgriddrop();
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Workflow Level Added Successfully."));
                        _actionMessage = "sc|Workflow Level Added Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                        Response.Redirect("EmployeeWorkFlowLevel.aspx?WFLevel=inserted&WorkFlowName=" + drpWorkFlowID.SelectedValue.ToString() + "&WorkFlowType=" + drpType.SelectedItem.Value.ToString());
                    }
                    else
                    {
                        //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Assign Workflow Level."));
                        _actionMessage = "Warning|Unable to Assign Workflow Level.";
                        ViewState["actionMessage"] = _actionMessage;
                    }

                }
            }
        }
    }
}

