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
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Text;
using efdata;//Added by Jammu Office
using AuditLibrary;
using System.Linq;

namespace SMEPayroll.Payroll
{
    public partial class AppliedClaims : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string sUserName = "";
        string sgroupname = "";
        string varEmpCode = "";
        int compid;
        int LoginEmpcode = 0;//Added by Jammu Office
        private CliamRepository _claimRepository;
        private CommonData _commondata;
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            if (Session["commandata"] != null)
            {
                _commondata = (CommonData)Session["commandata"];
            }

           


            _claimRepository = new CliamRepository();
            if (!IsPostBack)
            {
                varEmpCode = Session["EmpCode"].ToString();
                compid = Utility.ToInteger(Session["Compid"]);
                #region Emp dropdown
                DataSet ds_employee = new DataSet();
                //Senthil for Group Management
                sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " order by emp_name");
                }
                else
                {

                    SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, "select [WFCLAIM] from [Company] where company_id=" + compid, null);
                    int wfclaim = 0;
                    if (dr2.Read())
                    {
                        if (dr2[0].ToString() == "1")
                            wfclaim = 1;
                        else
                            wfclaim = 0;
                    }

                    if (Utility.GetGroupStatus(compid) == 1 && wfclaim == 1)
                    {
                        //ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name"); //Grouping
                        ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and [CliamsupervicerMulitilevel] in (select  WorkFlowID from [EmployeeWorkFlowLevel] where [PayRollGroupID] in  (select  [PayrollGroupID] from [EmployeeAssignedToPayrollGroup] where Emp_ID =" + Utility.ToInteger(Session["EmpCode"]) + " and [WorkflowTypeID]=3)) order by emp_name");
                    }
                    else
                    {



                        //                            ds_employee = getDataSet(@"select  [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'') as [emp_name] from employee where CliamsupervicerMulitilevel in(select CONVERT(VARCHAR,id)  from [EmployeeWorkFlowLevel] A where rowid >= ANY (select rowid from [EmployeeWorkFlowLevel] B where
                        //   id in(( SELECT  efl.ID FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef 
                        //  where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and 
                        //  ( efl.rowid >= (SELECT top 1  efl1.RowID FROM EMPLOYEE e1,[EmployeeWorkFlowLevel] efl1,[EmployeeAssignedToPayrollGroup] 
                        //  ea1,[EmployeeWorkFlow] ef1 where e1.emp_code=ea1 .emp_id and ea1.[PayrollGroupID]=efl.[PayrollGroupID] and 
                        //  efl1.[WorkFlowID]=ef1.ID and ea1.[PayrollGroupID]=efl1.[PayRollGroupID]  and ( ea1.Emp_ID=" + varEmpCode + " ) and efl1.FlowType=3 and  e1.Company_Id=" + compid + ")) and efl.FlowType=3 and e.Company_Id=" + compid + ")) AND B.[WorkFlowID]=A.[WorkFlowID] ))  union all      SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee el where el.[emp_clsupervisor]=" + varEmpCode + " or el.emp_code=" + varEmpCode + ")");
                        ds_employee = getDataSet("SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_clsupervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "' order by emp_name");





                        //  ds_employee = getDataSet("SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_clsupervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "' ");
                    }
                }
                DropDownList1.DataSource = ds_employee.Tables[0];
                DropDownList1.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                DropDownList1.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "-select-");
                #endregion

                #region Yeardropdown
                cmbYear.DataBind();
                cmbYear.Items.Insert(0, "-select-");
                #endregion


                if ((string)varEmpCode != "0")//checking whether user is login in the table.
                {
                    DropDownList1.SelectedValue = varEmpCode;
                }
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                string SQLQuery;
                SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                        // DropDownList1.Enabled = true;
                    }
                    else
                    {
                        //  DropDownList1.Enabled = false;
                    }
                }
            }
            RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);

        }


        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditableItem item = e.Item as GridEditableItem;
                GridTextBoxColumnEditor EmpName = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("emp_name");
                GridTextBoxColumnEditor Department = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("Department");
                GridTextBoxColumnEditor Amount = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("trx_amount");
                GridTextBoxColumnEditor status = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("status");
                GridTextBoxColumnEditor claimstatus = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("claimstatus");
                GridTextBoxColumnEditor remarks = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("remarks");
                GridTextBoxColumnEditor emp_code = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("emp_code");

                EmpName.TextBoxControl.Enabled = false;
                Department.TextBoxControl.Enabled = false;
                Amount.TextBoxControl.Enabled = false;
                status.TextBoxControl.Enabled = false;
                claimstatus.TextBoxControl.Enabled = false;
                remarks.TextBoxControl.Enabled = false;
                emp_code.TextBoxControl.Enabled = false;
            }
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;

                sUserName = Utility.ToString(Session["Username"]);
                //    //SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "SELECT GroupName FROM UserGroups ug, Employee emp WHERE emp.GroupID = ug.GroupID AND emp.UserName = '" + sUserName + "' ", null);
                //    //if (dr.Read())
                //    //{
                //    //    sgroupname = Utility.ToString(dr.GetValue(0));
                //    //}
                sgroupname = Utility.ToString(Session["GroupName"]);

                //    if (dataItem["claimstatus"].Text == "Approved" && sgroupname != "Super Admin")
                //    {
                //        dataItem["DeleteColumn"].Visible = false;
                //    }


                //    if (dataItem["status"].Text == "L" && dataItem["claimstatus"].Text == "Rejected" && sgroupname == "Super Admin")
                //    {
                //        dataItem["DeleteColumn"].Visible = false;
                //    }

                //if (dataItem["status"].Text == "L" && dataItem["claimstatus"].Text == "Approved" && sgroupname == "Super Admin")
                //{
                //    dataItem["DeleteColumn"].Visible = false;
                //}

                //----------check for first level approver
                string stringsql1 = @"select Emp_ID from EmployeeAssignedToPayrollGroup pg where pg.PayrollGroupID in(
SELECT
      [PayRollGroupID]
     
  FROM [EmployeeWorkFlowLevel]  where RowID=1 and FlowType=3) and pg.Emp_ID=" + Session["EmpCode"].ToString();



                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, stringsql1, null);
                bool isFirstLevelApprover = false;

                while (dr1.Read())
                {
                    isFirstLevelApprover = true;
                }
                //------------------



                if (dataItem["status"].Text == "L" && dataItem["claimstatus"].Text == "Approved")
                {
                    GridDataItem dataItem1 = e.Item as GridDataItem;
                    dataItem1.Cells[16].Controls[0].Visible = false;
                    dataItem1.Cells[15].Controls[0].Visible = false;
                    // dataItem["DeleteColumn"].Visible = false;
                }
                else
                {

                    string s = Utility.ToString(Session["GroupName"]);
                    if ((s == "Super Admin" || isFirstLevelApprover == true))
                    {
                        //if (dataItem["claimstatus"].Text == "Pending")//--murugan
                        //{
                        GridDataItem dataItem1 = e.Item as GridDataItem;
                        dataItem1.Cells[15].Controls[0].Visible = false;
                        dataItem1.Cells[16].Controls[0].Visible = true;
                        // dataItem["DeleteColumn"].Visible = false;
                    }
                    else
                    {
                        if (dataItem["claimstatus"].Text == "Pending")
                        {
                            GridDataItem dataItem1 = e.Item as GridDataItem;
                            dataItem1.Cells[16].Controls[0].Visible = true;
                            dataItem1.Cells[15].Controls[0].Visible = false;

                        }
                        else
                        {

                            GridDataItem dataItem1 = e.Item as GridDataItem;
                            dataItem1.Cells[16].Controls[0].Visible = false;
                            dataItem1.Cells[15].Controls[0].Visible = false;

                        }



                    }

                }
                string status = dataItem["claimstatus"].Text.ToString();
                int n;
                bool isnumberic = int.TryParse(status, out n);
                if (isnumberic)
                    dataItem["claimstatus"].Text = "Level-" + status;
            }




            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[4].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    hl.NavigateUrl = "../" + "Documents" + "/" + Utility.ToInteger(Session["Compid"]) + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";
                }
                else
                {
                    hl.Text = "No Document";
                }
            }



        }
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var successfullmessage = "";
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                int trxid = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                int emp_code = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"]);
                string emp_name = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_name"]).ToUpper();

                //delete claims




                try
                {
                    //Added by Jammu Office
                    #region Audit
                    var oldrecord = new AuditLibrary.EmpAddition();
                    using (var _context = new AuditContext())
                    {
                        oldrecord = _context.EmpAdditions.Where(i => i.TrxId == trxid).SingleOrDefault();
                    }
                    DateTime dtTrxPeriod = DateTime.Now.Date;
                    var newrecord = new AuditLibrary.EmpAddition();
                    var AuditRepository = new AuditRepository();
                    AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, oldrecord.TrxId, oldrecord, newrecord);

                    #endregion

                    _claimRepository.DeleteAdditionClaim(trxid);
                }
                catch (Exception ex)
                {

                    throw;
                }






                StringBuilder strSucSubmit = new StringBuilder();
                StringBuilder strFailSubmit = new StringBuilder();
                strSucSubmit.Append("<br/>" + emp_name + "<br/>");

                sendemail(trxid, emp_code);

                if (strSucSubmit.Length > 0)
                {
                    //ShowMessageBox("Claim has been cancelled successfully for: <br/>" + strSucSubmit.ToString());
                    _actionMessage = "Fixed|Claim has been deleted successfully for: " + strSucSubmit.ToString();
                   
                    strMessage = "";
                }
                //if (strFailSubmit.Length > 0)
                //{
                //    ShowMessageBox("Claims Not Submitted for: <br/>" + strFailSubmit.ToString());
                //    strMessage = "";
                //}
                if (strPassMailMsg.Length > 0)
                {
                    //ShowMessageBox("Email has been sent successfully to: <br/>" + strPassMailMsg.ToString());
                    _actionMessage += "<br/>Email has been sent successfully to: <br/>" + strPassMailMsg.ToString();
                      strMessage = "";
                }
                if (strFailMailMsg.Length > 0)
                {
                    //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                    _actionMessage += "<br/> Error While sending Email to: <br/>" + strFailMailMsg.ToString();
                    
                    strMessage = "";
                }
                ViewState["actionMessage"] = _actionMessage;
            }
        }

        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string trxid = (editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]).ToString();
                string date1 = (e.Item.FindControl("txtperiod") as TextBox).Text;
                string ssqlupdate;
                ssqlupdate = "update emp_additions set trx_period='" + date1 + "' where trx_id='" + trxid + "'";
                int i = DataAccess.ExecuteStoreProc(ssqlupdate);
                _actionMessage = "Success|Record Updated Successfully ";
                ViewState["actionMessage"] = _actionMessage;
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to Update record.</font> "));
                _actionMessage = "Warning|Unable to Update record.. Reason : " +ErrMsg ;
                ViewState["actionMessage"] = _actionMessage;
            }

        }

        string trx_period;
        int appLevel;
        protected void sendemail(int id, int empcode)
        {
            string code = empcode.ToString();
            int companyid = Utility.ToInteger(Session["Compid"]);
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string month = "";
            string year = "";
            string cc = "";
            string claimtype = "";
            string createddate = "";
            string trxamount = "";
            string remarks = "";
            string sup_name = "";
            bool isMultiLevel = false;
            int outresult;
            string claimstatus = "";


            string sql9 = "select datename(month,ea.trx_period) ,year(ea.trx_period),ea.trx_period, Convert(varchar(50),ea.created_on,103),ea.trx_amount,ea.remarks,at.[desc],approver,claimstatus from emp_additions ea inner join additions_types at on ea.trx_type=at.id where ea.trx_id='" + Utility.ToInteger(id) + "'";
            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, sql9, null);
            while (dr9.Read())
            {
                month = Utility.ToString(dr9.GetValue(0));
                year = Utility.ToString(dr9.GetValue(1));
                trx_period = Utility.ToString(dr9.GetValue(2));
                createddate = Utility.ToString(dr9.GetValue(3));
                trxamount = Utility.ToString(dr9.GetValue(4));
                remarks = Utility.ToString(dr9.GetValue(5));
                claimtype = Utility.ToString(dr9.GetValue(6));

                //isMultiLevel = int.TryParse(Utility.ToString(dr9.GetValue(7)), out outresult);
                if (Utility.ToString(dr9.GetValue(7)).Length > 0 && Utility.ToString(dr9.GetValue(7)) != "-1")
                {
                    isMultiLevel = true;
                }
                else
                {
                    isMultiLevel = false;
                }
                claimstatus = Utility.ToString(dr9.GetValue(8));

            }




            string sSQLemail = "sp_sendclaim_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(companyid));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                to = Utility.ToString(dr3.GetValue(2));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                emp_name = Utility.ToString(dr3.GetValue(5));
                body = Utility.ToString(dr3.GetValue(11));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                cc = Utility.ToString(dr3.GetValue(17));
                sup_name = Utility.ToString(dr3["supervisor"]);

            }
            if (emailreq == "yes")
            {

                //body = body.Replace("@emp_name", emp_name);
                //body = body.Replace("@claimtype", claimtype);
                //body = body.Replace("@month", month);
                //body = body.Replace("@year", year);
                //body = body.Replace("@applied_date", createddate);
                //body = body.Replace("@amount", trxamount);
                //body = body.Replace("@remarks", remarks);
                string app = "";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select emp_name from employee where emp_code=" + Session["EmpCode"].ToString(), null);
                if (dr.Read())
                {
                    app = dr[0].ToString();
                }


                if (_commondata.IsSuperAdmin)
                {
                    // app = "Super Admin";
                    app = string.IsNullOrEmpty(_commondata.LoginUser.EmpLname) ? "Super Admin" : _commondata.LoginUser.EmpName ;

                }



                DateTime todate = DateTime.Now;
                string tdate = todate.ToString("dd/MM/yyyy");
                string status1 = "Cancelled";
                string subject = "Claim Status for" + " " + emp_name;
                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@approver", app);
                body = body.Replace("@status", status1);
                body = body.Replace("@approved_date", tdate);
                body = body.Replace("@applied_date", createddate);
                body = body.Replace("@amount", trxamount);

                if (isMultiLevel)
                {

                    // string sqld = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + code + "))) union select email from employee where emp_code=" + code + "";
                    // string sqld = "select email from employee where Emp_code = (select emp_id from [EmployeeAssignedToPayrollGroup]  where [PayrollGroupID]=(select [CliamsupervicerMulitilevel] from employee where emp_code=" + code + "))";
                    string sqld = "select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select [CliamsupervicerMulitilevel] from employee where emp_code=" + code + ")))";
                    SqlDataReader drd = DataAccess.ExecuteReader(CommandType.Text, sqld, null);
                    string emaild;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (drd.Read())
                    {
                        emaild = drd[0].ToString() + ";";
                        strUpdateBuild.Append(emaild);
                    }

                    emaild = strUpdateBuild.ToString();
                    to = emaild;
                    //to = "Shashank@anbgroup.com";

                    //--superadmin login

                    SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
                    if (dr19.Read())
                    {
                        if (dr19[0].ToString() == "Super Admin")
                        {
                            string sql = "select email from employee where Emp_code=" + code;
                            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr11.Read())
                            {
                                to = to + dr11[0].ToString();
                            }

                        }
                        else if (Session["EmpCode"].ToString() != code)
                        {

                            if (claimstatus == "Pending")
                            {

                                string sql = "select email from employee where Emp_code=" + code;
                                SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr11.Read())
                                {
                                    to = dr11[0].ToString();
                                }

                            }
                            else if (claimstatus == "Approved")
                            {
                                to = "";
                                //string sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                                //sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>=1";
                                //sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_code='" + code + "'))";

                                string sql = "select distinct email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID] ";
                                sql = sql + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=3 and  rowid>1  and WorkFlowID =";
                                sql = sql + "(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_code='" + code + "'))))";
                                SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                while (dr11.Read())
                                {
                                    if (to == "")
                                    {
                                        to = dr11[0].ToString();
                                    }
                                    else
                                    {
                                        to = to + ";" + dr11[0].ToString();
                                    }

                                }
                                sql = "select email from employee where Emp_code=" + code;
                                SqlDataReader dr12 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr12.Read())
                                {
                                    to = to + ";" + dr12[0].ToString();
                                }


                            }
                            else if (claimstatus == "Rejected")
                            {

                                string sql = "select email from employee where Emp_code=" + code;
                                SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr11.Read())
                                {
                                    to = dr11[0].ToString();
                                }

                            }
                            else
                            {
                                to = "";
                                string sql = "select email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID] ";
                                sql = sql + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=3 and  rowid>=" + claimstatus + " and WorkFlowID =";
                                sql = sql + "(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_code='" + code + "'))))";
                                SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                while (dr11.Read())
                                {
                                    if (to == "")
                                    {
                                        to = dr11[0].ToString();
                                    }
                                    else
                                    {
                                        to = to + ";" + dr11[0].ToString();
                                    }

                                }
                                sql = "select email from employee where Emp_code=" + code;
                                SqlDataReader dr12 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr12.Read())
                                {
                                    to = to + ";" + dr12[0].ToString();
                                }

                            }




                        }


                    }


                    //

                }
                else
                {
                    to = "";
                    string v = "";
                    varEmpCode = Session["EmpCode"].ToString();

                    string sql = "select emp_clsupervisor from employee where emp_code=" + code;
                    SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    if (dr19.Read())
                    {
                        v = (dr19[0] == null) ? "0" : dr19[0].ToString();

                    }

                    if (v == varEmpCode)
                    {
                        sql = "select email from employee where emp_code=" + code;
                        SqlDataReader dr20 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                        if (dr20.Read())
                        {
                            to = dr20[0].ToString();
                        }


                    }
                    else
                    {
                        dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + varEmpCode, null);
                        if (dr19.Read())
                        {
                            if (dr19[0].ToString() == "Super Admin")
                            {

                                sql = "select email from employee where emp_code=(select emp_clsupervisor from employee where emp_code=" + code + ") union select email from employee where emp_code=" + code + "";

                                SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);

                                while (dr11.Read())
                                {
                                    if (to == "")
                                    {
                                        to = dr11[0].ToString();
                                    }
                                    else
                                    {
                                        to = to + dr11[0].ToString() + ";";
                                    }
                                }

                            }


                        }
                        else
                        {

                            sql = "select email from employee where emp_code=(select emp_clsupervisor from employee where emp_code=" + code + ")";
                            SqlDataReader dr20 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr20.Read())
                            {
                                to = dr20[0].ToString();
                            }

                        }



                    }

                }

                //---murugan
                if (to.Length == 0 || from.Length == 0)
                {

                    //ShowMessageBox("Please check email address is not configured yet");
                    _actionMessage = "Warning|Please check email address is not configured yet";
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }
                //------

                //  }



                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(companyid);

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                try
                {
                    //string sRetVal = oANBMailer.SendMail();
                    string sRetVal = oANBMailer.SendMail("Claim", emp_name, trx_period, trx_period, "Apply Claims");

                    if (sRetVal == "SUCCESS")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                //strPassMailMsg.Append("<br/>" + sup_name + "<br/> CC To :"+cc);
                                strPassMailMsg.Append("<br/>" + to + "<br/> CC To :" + cc);
                            }
                            else
                            {
                                // strPassMailMsg.Append("<br/>" + sup_name );
                                strPassMailMsg.Append("<br/>" + to);
                            }
                        }
                    }
                    else
                    {
                        // strFailMailMsg.Append("<br/>" + sup_name );
                        strFailMailMsg.Append("<br/>" + to);
                    }
                }
                catch (Exception ex)
                {
                    // strFailMailMsg.Append("<br/>" + sup_name );
                    strFailMailMsg.Append("<br/>" + to);
                }
            }


        }

        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }


    }
}
