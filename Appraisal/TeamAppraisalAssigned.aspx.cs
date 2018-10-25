﻿using System;
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


namespace SMEPayroll.Appraisal
{
    public partial class TeamAppraisalAssigned : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected int comp_id;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);
            lblMsg.Text = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                string sSQL = "Select ID, Team_Name From TeamAppraisal Where Company_ID={0}";
                sSQL = string.Format(sSQL, comp_id);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                ddlRoster.Items.Clear();
                ddlRoster.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", "-1"));
                while (dr.Read())
                {
                    ddlRoster.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                ddlRoster.Items.FindByValue("-1");
               // murugan
                // rdEmpPrjStart.SelectedDate = DateTime.Now.Date;

            }
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();

        }
        protected void drpSubProjectID_databound(object sender, EventArgs e)
        {
            ddlRoster.Items.Insert(0, new ListItem("-select-", "-1"));
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Roster Assignment")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
                RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            }
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

                //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
                //if (dr.Read())
                //{
                //    if (Convert.ToInt16(dr[0].ToString()) > 0)
                //    {
                //        lblMsg.ForeColor = System.Drawing.Color.Red;

                //        lblMsg.Text = "Unable to delete the Roster Assigned.This Roster Assigned is in use.";
                //    }
                //    else
                //    {
                        string sSQL = "DELETE FROM [TeamAppraisalAssigned] WHERE [ID] =" + id;

                        int retVal = DataAccess.ExecuteStoreProc(sSQL);

                        if (retVal >= 1)
                        {
                           // lblMsg.ForeColor = System.Drawing.Color.Green;
                          //  lblMsg.Text = "Employee Assigned with Team is Deleted Successfully.";
                    _actionMessage = "Success|Employee Assigned with Team is Deleted Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid2.Rebind();
                            RadGrid1.Rebind();
                        }
                        else
                        {
                            //lblMsg.ForeColor = System.Drawing.Color.Red;
                            //lblMsg.Text = "Unable to delete the Employee Assigned.";
                    _actionMessage = "Warning|Unable to delete the Employee Assigned.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                   // }
               // }
            }
            catch (Exception ex)
            {
                string ErrMsg = "Some Error Occured";
                if (ex.Message.IndexOf("REFERENCE constraint", 1) > 0)
                    ErrMsg = "Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables";

                    //ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
                //lblMsg.ForeColor = System.Drawing.Color.Red;
                //lblMsg.Text = "Unable to delete record. Reason: " + ErrMsg;
                _actionMessage = "Warning|Unable to delete record. Reason: " + ErrMsg;
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
           // RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            string strActionMsg = "";
            string strempid = "";
            string strRosterID = ddlRoster.SelectedItem.Value.ToString();
            string strEmployee = "0";
            string strAction = ((Button)sender).Text;
            string strMessage = "";
            string strUpdateRoster = "";

            RadGrid rd = new RadGrid();
            int i = 0;
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@RosterID", Utility.ToInteger(strRosterID));

            if (strAction == "Assign")
            {
                rd = RadGrid2;
            }
            if (strAction == "Un-Assign")
            {
                rd = RadGrid1;
            }

            if (strRosterID != "-1")
            {
                if (strAction == "Assign")
                {
                    strActionMsg = "Assigned";
                    parms[2] = new SqlParameter("@Action", Utility.ToString("0"));

                    DataSet dsnew = new DataSet();

                    SqlParameter[] parmsnew = new SqlParameter[1];
                    parmsnew[0] = new SqlParameter("@RosterCurID", Utility.ToInteger(strRosterID));
                    string sSQL = "sp_GetRosterLockedDate";
                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parmsnew);


                    bool blnfound = false;
                    foreach (GridItem item in rd.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            strempid = dataItem.Cells[2].Text.ToString().Trim();

                            if (chkBox.Checked == true)
                            {
                                //if (ds.Tables[0].Rows.Count > 0)
                                //{
                                //    string expression;
                                //    expression = "Emp_ID =" + strempid;
                                //    DataRow[] foundRows;
                                //    foundRows = ds.Tables[0].Select(expression);
                                //    if (foundRows.Length > 0)
                                //    {
                                //        blnfound = true;
                                //        strMessage = strMessage + "<br/>" + foundRows[0][0].ToString();
                                //    }
                                //    else
                                //    {

                                //    }
                                //}
                                //else
                                //{
                                    strEmployee = strEmployee + "," + strempid;
                                //}

                                strEmployee = strEmployee + "," + strempid;
                                //murugan
                                //if (strUpdateRoster == "")
                                //{
                                //    strUpdateRoster = "UPDATE ACTATEK_LOGS_PROXY  SET Roster_ID =" + ddlRoster.SelectedValue + " WHERE convert(DATETIME,timeentry,103) >=convert(DATETIME,'" + rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year + " ',103)  AND userID='" + dataItem["Time_Card_NO"].Text + "' AND Roster_ID IS NULL";
                                //}
                                //else
                                //{
                                //    strUpdateRoster = strUpdateRoster + ";" + "UPDATE ACTATEK_LOGS_PROXY  SET Roster_ID =" + ddlRoster.SelectedValue + " WHERE convert(DATETIME,timeentry,103) >=convert(DATETIME,'" + rdEmpPrjStart.SelectedDate.Value.Day + "/" + rdEmpPrjStart.SelectedDate.Value.Month + "/" + rdEmpPrjStart.SelectedDate.Value.Year + " ',103)  AND userID='" + dataItem["Time_Card_NO"].Text + "' AND Roster_ID IS NULL";
                                //}
                            }
                        }
                    }
                    if (blnfound == true)
                    {
                        strMessage = "Time Conflicts with Other Rosters For Employee:" + strMessage;

                    }
                }
                if (strAction == "Un-Assign")
                {
                    foreach (GridItem item in rd.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            strempid = dataItem.Cells[2].Text.ToString().Trim();
                            if (chkBox.Checked == true)
                            {
                                strEmployee = strEmployee + "," + strempid;
                            }
                        }
                    }
                    strActionMsg = "Un-Assigned";
                    parms[2] = new SqlParameter("@Action", Utility.ToString("1"));
                }

            }
            else
            {
                strMessage = strMessage + "<br/>" + "Please Select Appraisal Team.";
            }
            if (strMessage.Length == 0)
            {

                if (strEmployee.Length > 0 && strEmployee != "0")
                {
                    parms[1] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    parms[3] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[3].Direction = ParameterDirection.Output;

                    string sSQL = "sp_Team_Appraisal_Assigned";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Green;
                        //lblMsg.Text = "Employee " + strActionMsg + " To Team Successfully.";
                        _actionMessage = "Success|Employee(s) " + strActionMsg + " To Team Successfully.";
                        ViewState["actionMessage"] = _actionMessage;
                        RadGrid2.Rebind();
                        RadGrid1.Rebind();

                        if (strUpdateRoster != "")
                        {
                            int cnt = DataAccess.ExecuteNonQuery(strUpdateRoster, null);
                        }
                    }
                    else
                    {
                        //lblMsg.ForeColor = System.Drawing.Color.Red;
                        //lblMsg.Text = "Unable to " + strActionMsg + " Employees to Team.";
                        _actionMessage = "Warning|Unable to " + strAction + " Employee(s) to Team.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
                else
                {
                    if (rd.MasterTableView.Items.Count <= 0)
                    {
                         strMessage = strMessage + "<br/>" + "No Employee(s) to assign..";
                       
                        _actionMessage = "Warning|" + strMessage;
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    else
                    {

                        // lblMsg.ForeColor = System.Drawing.Color.Red;
                        strMessage = strMessage + "<br/>" + "Please Select Employee.";
                        // lblMsg.Text = strMessage;
                        _actionMessage = "Warning|" + strMessage;
                        ViewState["actionMessage"] = _actionMessage;
                    }
                }
            }
            else
            {
                lblMsg.Text = strMessage;
            }
        }

        protected void ddlRoster_SelectedIndexChanged(object sender, EventArgs e)
        {

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
        protected void btnPrintReport_Click(object sender, EventArgs e)
        {
            string sqlSelect = "sp_GetRosterDetails";
            DataSet rptDs;
            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            Session["rptDs"] = rptDs;
            Response.Redirect("../Management/RosterReportView.aspx?PageType=26");

        }
    }
}

