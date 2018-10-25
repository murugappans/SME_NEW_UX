using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Data.SqlClient;
using AuditLibrary; //Added by Jammu Office
using efdata;
using System.Linq;

namespace SMEPayroll.Leaves
{
    public partial class LeaveAddDed : System.Web.UI.Page
    {
        string compid = "";
        SqlParameter[] parms;
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int LoginEmpcode = 0; //Added by Jammu Office
        string sSQL = "";
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            /* To disable Grid filtering options  */
            compid = Session["Compid"].ToString();
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            lblerror.Text = "";

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                RadGridReport.ExportSettings.FileName = "Manage_Leave_Reports";
                RadGridDetails.ExportSettings.FileName = "Manage_Leave_Details_Reports";
                //imgbtnfetch.Enabled = false;
                btnsubmit.Visible = false;
                btnCalc.Visible = false;
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                //   binddata();
            }

            if (Session["TimeSheetApproved"].ToString() == "1")
            {
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Bottom;
            }
            else
            {
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Bottom;
            }


        }
        protected void bindgrid(object sender, EventArgs e)
        {
            this.RadGridReport.Visible = false;
            this.RadGrid1.Visible = true;
            RadGrid1.DataBind();
         

    
            //GridToolBar.Visible = true;
        }

        protected void cmbLeaveType_databound(object sender, EventArgs e)
        {
            cmbLeaveType.Items.Insert(0, new ListItem("-select-", "-1"));

        }
        protected void cmbLeaveType_selectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLeaveType.SelectedItem.Value != "-1")
                imgbtnfetch.Enabled = true;
            else
                imgbtnfetch.Enabled = false;
        }

        void UpdateCalculateLeaves(string strButton)
        {
            string sSQL = "sp_emp_leaves_adddeduc_update";
        string Guid=    DateTime.Now.ToString("MMyyyyHHss");
            parms = new SqlParameter[12];
            string ErrMsg = "";
            string strrowid = "";
            string strempid = "";
            if (strButton == "UpdateAll" || strButton == "CalculateAll")
            {
                if (cmbAddDeduct.SelectedValue == "0")
                {
                    ErrMsg = ErrMsg + "Please select Leave Type" + "<br/>";
                }
                if (cmbYear.SelectedValue == "0")
                {
                    ErrMsg = ErrMsg + "Please select Year " + "<br/>";
                }

                if (txtleaveaddded.Text.ToString().Length <= 0)
                {
                    ErrMsg = ErrMsg + "Please enter Leave Figure " + "<br/>";
                }
                if (ErrMsg.ToString().Length == 0)
                {
                    double intRoundNo = 0;
                    double intisSucess = 0;
                    int intleaveNo = Utility.ToInteger(cmbAddDeduct.SelectedValue);
                    int intleavetype = Utility.ToInteger(cmbLeaveType.SelectedValue);
                    int leaveyear = Utility.ToInteger(cmbYear.SelectedValue);
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        double intleaveupdate = 0;
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                int empId = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                int RowId = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("row_id"));
                                string strRemarks = "";
                                TextBox txtbox = (TextBox)dataItem.FindControl("txtRemarks");
                                strRemarks = Utility.ToString(txtbox.Text);
                                strrowid = Utility.ToString(empId);
                                strempid = "0";
                                //strempid = Utility.ToString(RowId);
                                if (strempid.ToString().Length > 0)
                                {
                                    try
                                    {
                                        double intleavereamining = Utility.ToDouble(txtleaveaddded.Text.ToString());
                                        double intleavecurrent = Utility.ToDouble(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("leave_remaining"));

                                        if (intleaveNo == 1)
                                        {
                                            intleaveupdate = intleavecurrent + intleavereamining;
                                        }
                                        else
                                        {
                                            intleaveupdate = intleavecurrent - intleavereamining;
                                            // Added by murugan
                                            if (intleaveupdate < 0)
                                                intleaveupdate = 0;
                                        }
                                       
                                        intRoundNo = Math.Abs(intleaveupdate % 2);
                                        //if (intRoundNo == 0 || intRoundNo == 1 || intRoundNo == 0.5 || intRoundNo == 1.5)
                                        //{
                                            if (strButton == "UpdateAll")
                                            {
                                                parms[0] = new SqlParameter("@rowid", strrowid);
                                                parms[1] = new SqlParameter("@leavereamining", intleaveupdate);
                                                parms[2] = new SqlParameter("@leavetype", intleavetype);
                                                parms[3] = new SqlParameter("@eid", empId);//murugan
                                                parms[4] = new SqlParameter("@Leave_Forefited", intleavereamining);
                                                parms[5] = new SqlParameter("@Remarks", strRemarks);

                                                parms[7] = new SqlParameter("@NoOfDays", intleavereamining);
                                                 parms[8] = new SqlParameter("@UpdatedBy", "Admin");
                                                 parms[9] = new SqlParameter("@LeaveYear", leaveyear);
                                                 parms[11] = new SqlParameter("@TranId", Guid);



                                                if (intleaveNo == 1)
                                                {
                                                    parms[6] = new SqlParameter("@Status", "1");
                                                    parms[10] = new SqlParameter("@Add_Dec", "Addition");
                                                }
                                                else
                                                {
                                                    parms[6] = new SqlParameter("@Status", "0");
                                                    parms[10] = new SqlParameter("@Add_Dec", "Deduction");
                                                }
//Added by Jammu Office
                                            #region Audit     


                                            var oldRecord = new EmployeeLeavesAllowed();
                                            using (var _auditContext = new AuditContext())
                                            {
                                                oldRecord = _auditContext.EmployeeLeavesAllowed.Where(m => m.Id == RowId).FirstOrDefault();
                                            }
                                            var NewRecord = new EmployeeLeavesAllowed()
                                            {
                                                Id = RowId,
                                                LeavesAllowed = intleaveupdate,
                                                LeaveType = intleavetype,
                                                LeaveYear = leaveyear,
                                                EmpId = empId
                                            };
                                            var NewLeaveForetifiedRecord = new LeavesForefited()
                                            {
                                                LeaveForefited = intleavereamining,
                                                Remarks = strRemarks,
                                                LeaveYear = leaveyear,
                                                TranId = Guid,
                                                LeaveAllowed = intleaveupdate,
                                                LeaveType = intleavetype,
                                                EmpId = empId,
                                                UpdateDate = DateTime.Now,
                                                Status = Convert.ToInt32(parms[6].Value),
                                                GolabalUpdate = 1

                                            };
                                            var AuditRepository = new AuditRepository();
                                            AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, RowId, oldRecord, NewRecord);
                                           AuditRepository.CreateAuditTrail(AuditActionType.Create, LoginEmpcode, 1, NewLeaveForetifiedRecord, NewLeaveForetifiedRecord);
                                            #endregion

                                            intisSucess= DataAccess.ExecuteStoreProc(sSQL, parms);
                                                if (intisSucess != 2)
                                                {
                                                    intisSucess = 1;
                                                }
                                            }
                                            else if (strButton == "CalculateAll")
                                            {
                                                //  dataItem.Cells[8].Text = intleaveupdate.ToString();
                                                dataItem.Cells[9].Text = intleaveupdate.ToString();
                                            }
                                        //}
                                        //else
                                        //{
                                        //    dataItem.Cells[8].Text = "Not Updated";
                                        //    ErrMsg = "Some Rows cannot be updated.";
                                        //}
                                    }
                                    catch (Exception ex)
                                    {
                                        intisSucess = 2;
                                        ErrMsg = ex.Message;
                                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                        {
                                            ErrMsg = "Unable to update the status.Please Try again!";
                                        }
                                        _actionMessage = "Warning|Leave Records Update Failed.  "+ErrMsg;
                                        ViewState["actionMessage"] = _actionMessage;
                                    }
                                }
                            }
                        }
                    }

                    if (intisSucess == 1)
                    {
                        binddata();
                        //msg.Value = "Leave Records Updated Sucessfully.";
                        _actionMessage = "Success|Leave Records Updated Sucessfully.";
                        ViewState["actionMessage"] = _actionMessage;
                    }
                    //if (intisSucess == 2)
                    //{
                    //   // msg.Value = "Leave Records Update Failed.";
                    //    _actionMessage = "Warning|Leave Records Update Failed.";
                    //    ViewState["actionMessage"] = _actionMessage;
                    //}
                }

               // lblerror.Text = ErrMsg;
            }
        }

        protected void Reprort_ItemCommand(object source, GridCommandEventArgs e)
        {
           // "RowClick"
            if (e.CommandName == "RowClick")
            {

                GridDataItem item = (GridDataItem)e.Item;
                 bindDetail_report(item["TRANSACTION ID"].Text);

                if (RadGridDetails.MasterTableView.Items.Count > 0)
                {
                    DetailRadToolBar.Visible = true;

                }
                else {
                    DetailRadToolBar.Visible = false;
                }

            }
        } 

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
          //  Boolean flag = false;
            string rem = ""; //murugan
            if (e.CommandName == "UpdateAll")
            {
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            //GridDataItem dataItem = (GridDataItem)item;
                            TextBox txtbox = (TextBox)dataItem.FindControl("txtRemarks");
                            string remarks = Utility.ToString(txtbox.Text);
                           // if (flag == false)
                           //     if (remarks != "")
                            //    {
                             //       flag = true;
                                    // Session["remarks"] = remarks;
                                    //murugan
                                    rem = remarks;
                            break;
                                   
                             //   }
                        }
                    }
                }
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            //GridDataItem dataItem = (GridDataItem)item;
                            TextBox txtbox = (TextBox)dataItem.FindControl("txtRemarks");
                            txtbox.Text = rem; //murugan
                        }
                    }
                }
            }
            //Export 
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName || e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName)
            {
                ConfigureExport();
            }
        }
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
        public void ConfigureExport()
        {
            //To ignore Paging,Exporting only data,
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            RadGrid1.MasterTableView.AllowFilteringByColumn = false;

            //To hide the add new button
            RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;

            //Column to hide
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("editHyperlink").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;
        }
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);                           
        }     
        protected void getData(object sender, EventArgs e)
        {
            this.RadGridReport.Visible = false;
            this.RadGridDetails.Visible = false;
            this.RecordToolBar.Visible = false;
            this.DetailRadToolBar.Visible = false;
            this.RadGrid1.Visible = true;
            btnsubmit.Visible = true;
            btnCalc.Visible = true;
            string ErrMsg = "";
            if (cmbLeaveType.SelectedItem.Value != "-1")
            {
                binddata();
             
            }
            else
            {
                ErrMsg = ErrMsg + "Please Select Leave Type " + "<br/>";
                lblerror.Text = ErrMsg;
            }
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            //binddata();
            
        }

        private void binddata()
        {
            DataSet ds = new DataSet();
            sSQL = "sp_emp_leaves_adddeduc";
            int intleaveNo = Utility.ToInteger(cmbAddDeduct.SelectedValue);
            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@company_id", Utility.ToInteger(compid));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@leaveType", Utility.ToInteger(cmbLeaveType.SelectedValue));
            if (intleaveNo == 1)
            {
                parms[3] = new SqlParameter("@Status", Utility.ToInteger("1"));
            }
            else
            {
                parms[3] = new SqlParameter("@Status", Utility.ToInteger("0"));
            }
            ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
            //GridToolBar.Visible = true;
        }
        private void binddata_report()
        {
            DataSet ds = new DataSet();
            sSQL = "Sp_emp_leaves_UpdateHistory";
            int intleaveNo = Utility.ToInteger(cmbAddDeduct.SelectedValue);
            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@company_id", Utility.ToInteger(compid));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@EmpId","ALL");
            parms[3] = new SqlParameter("@TranId", "TH");
            //parms[2] = new SqlParameter("@leaveType", Utility.ToInteger(cmbLeaveType.SelectedValue));


            ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
           // ds.Tables[0].Columns[0].ColumnName = "TransactionID";//Correnct the coulmn name...
            RadGridReport.DataSource = ds;
            RadGridReport.DataBind();


            //GridToolBar.Visible = true;
        }
        private void bindDetail_report(string TranId)
        {
            DataSet ds = new DataSet();
            sSQL = "Sp_emp_leaves_UpdateHistory";
            int intleaveNo = Utility.ToInteger(cmbAddDeduct.SelectedValue);
            parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@company_id", Utility.ToInteger(compid));
            parms[1] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[2] = new SqlParameter("@EmpId", "TRAN");
            parms[3] = new SqlParameter("@TranId", TranId);
            //parms[2] = new SqlParameter("@leaveType", Utility.ToInteger(cmbLeaveType.SelectedValue));


            ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);


             this.RadGridDetails.DataSource = ds;
             this.RadGridDetails.DataBind();
            //GridToolBar.Visible = true;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            this.RadGridReport.Visible = false;
            this.RadGridDetails.Visible = false;
            this.RecordToolBar.Visible = false;
            this.DetailRadToolBar.Visible = false;
            this.RadGrid1.Visible = true;
            UpdateCalculateLeaves("UpdateAll");
        }

        protected void GetReport(object sender, EventArgs e)
        {
            binddata_report();
            this.RadGridReport.Visible = true;
            this.RadGridDetails.Visible = true;
            this.RecordToolBar.Visible = true;
            this.DetailRadToolBar.Visible = true;
            this.RadGrid1.Visible = false;
            btnsubmit.Visible = false;
            btnCalc.Visible = false;

            if (RadGridDetails.MasterTableView.Items.Count > 0)
            {
                DetailRadToolBar.Visible = true;

            }
            else
            {
                DetailRadToolBar.Visible = false;
            }
        }
        protected void btnCalc_Click(object sender, EventArgs e)
        {
            this.RadGridReport.Visible = false;
            this.RadGrid1.Visible = true;
            UpdateCalculateLeaves("CalculateAll");
        }



        /////////kumar//

        protected void RadGridDetail_GridExporting(object source, GridExportingArgs e)
        {
            //GridHeader("10", e);
        }

        protected void DetailRadToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            //determine which button was clicked

            if (e.Item.Text == "Add")
            {
                RadGridDetails.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {
                ConfigureExport6();
                RadGridDetails.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport6();
                RadGridDetails.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport6();
                // RadGrid6.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid6.Items[0].Cells.Count * 24)) + "mm");
                RadGridDetails.ExportSettings.OpenInNewWindow = true;
                RadGridDetails.MasterTableView.ExportToPdf();
            }

           
        }

        protected void RecordToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            //determine which button was clicked

            if (e.Item.Text == "Add")
            {
                RadGridReport.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {
                ConfigureExport6();
                RadGridReport.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport6();
                RadGridReport.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport6();
                // RadGrid6.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid6.Items[0].Cells.Count * 24)) + "mm");
                RadGridReport.ExportSettings.OpenInNewWindow = true;
                RadGridReport.MasterTableView.ExportToPdf();
            }


        }
        public void ConfigureExport6()
        {
            //To ignore Paging,Exporting only data,
            RadGridDetails.ExportSettings.ExportOnlyData = true;
            RadGridDetails.ExportSettings.IgnorePaging = true;
            RadGridDetails.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            RadGridDetails.MasterTableView.AllowFilteringByColumn = true;


            //To hide the add new button
            RadGridDetails.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;


        }




    }
}
