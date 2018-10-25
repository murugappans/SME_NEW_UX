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
using System.IO;
using System.Text;
using efdata;//Added by Jammu Office
using AuditLibrary;
using System.Linq;

namespace SMEPayroll.Leaves
{
    public partial class ViewAppliedLeaves : System.Web.UI.Page
    {
        string strMessage = "";
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string sUserName = "", sgroupname = "";
        string Approver,payrollstatus;
        int LoginEmpcode = 0;//Added by Jammu Office
        string varEmpCode="";
        string _actionMessage = "";
        int compid;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            ViewState["actionMessage"] = "";
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
                string sgroupname = Utility.ToString(Session["GroupName"]);
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                cmbYear.SelectedValue = System.DateTime.Today.Year.ToString();
                compid = Utility.ToInteger(Session["Compid"]);
                varEmpCode = Session["EmpCode"].ToString();

                //new
                if (varEmpCode != "1" && varEmpCode != "0")//if not super admin and not a user
                {
                    //cmbEmployee.SelectedValue = varEmpCode;
                }
                //
                //cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                string SQLQuery;

                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Leaves for all") == true))
                {
                    SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";
                    SqlDataSource1.SelectCommand = "SELECT -1 as [emp_code], '-select-' as [emp_name] Union SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name ";
                    
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                    if (dr.Read())
                    {
                        if (Utility.ToInteger(dr[0].ToString()) > 0)
                        {
                            cmbEmployee.Enabled = true;
                        }
                        else
                        {
                            if (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Leaves for all") == true)
                            {
                                cmbEmployee.Enabled = true;
                            }
                            else
                            {
                                cmbEmployee.Enabled = false;
                            }
                        }
                    }

                   

                }
                else
                {
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "select WFLEAVE from [Company] where company_id=" + compid, null);
                    int wfleave = 0;
                    if (dr.Read())
                    {
                        if (dr[0].ToString() == "1")
                            wfleave = 1;
                        else
                            wfleave = 0;
                    }

                    // if (Utility.GetGroupStatus(compid) == 1 && wfleave == 1)
                    if (Utility.GetGroupStatus(compid) == 0 && wfleave == 1)
                    {
                        //SqlDataSource1.SelectCommand = "SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_supervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "'";
                        string str = "select  [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'') as [emp_name] from employee ";
                        str = str + "where [Leave_supervisor] in(select CONVERT(VARCHAR,id)  from [EmployeeWorkFlowLevel] A where rowid >= ANY (select rowid from [EmployeeWorkFlowLevel] B ";
                        str = str + " where id in(( SELECT  efl.ID FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef ";
                        str = str + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ";
                        str = str + " ( efl.rowid >= (SELECT top 1  efl1.RowID FROM EMPLOYEE e1,[EmployeeWorkFlowLevel] efl1,[EmployeeAssignedToPayrollGroup]";
                        str = str + " ea1,[EmployeeWorkFlow] ef1 where e1.emp_code=ea1 .emp_id and ea1.[PayrollGroupID]=efl.[PayrollGroupID] and";
                        str = str + " efl1.[WorkFlowID]=ef1.ID and ea1.[PayrollGroupID]=efl1.[PayRollGroupID]  and ( ea1.Emp_ID=" + varEmpCode + " ) and efl1.FlowType=2 and ";
                        str = str + " e1.Company_Id=" + compid + ")) and efl.FlowType=2 and e.Company_Id=" + compid + ")) AND B.[WorkFlowID]=A.[WorkFlowID] ))  union all     ";
                        str = str + " SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] ";
                        str = str + " where emp_code in (select emp_code from employee el where el.[emp_supervisor]=" + varEmpCode + " or el.emp_code=" + varEmpCode + ")";

                        // SqlDataSource1.SelectCommand = "SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_supervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "'";

                        SqlDataSource1.SelectCommand = str;

                      
                    }

                    else
                    {
                        SqlDataSource1.SelectCommand = "SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                        // SqlDataSource1.SelectCommand = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and [CliamsupervicerMulitilevel] in (select  WorkFlowID from [EmployeeWorkFlowLevel] where [PayRollGroupID] in  (select  [PayrollGroupID] from [EmployeeAssignedToPayrollGroup] where Emp_ID =" + Utility.ToInteger(Session["EmpCode"]) + " and [WorkflowTypeID]=2)) order by emp_name";
                    }


                }
                //if "Cancel Approved Leave By Supervisor" is selected show all emp under his supervison  and enable the dropdown
                if (Utility.AllowedAction1(Session["Username"].ToString(), "Cancel Approved Leave By Supervisor") == true)
                //if (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves") == true)
                {
                    cmbEmployee.Enabled = true;
                    SqlDataSource1.SelectCommand = "SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_supervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "' order by emp_name";
                }

            }
        }
        
       
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
        protected void bindgrid(object sender, EventArgs e)
        {
            // by murugan
            SqlDataSource2.SelectCommand = "SELECT [Path],[trx_id], [emp_id], [leave_type] ,b.type,[start_date] as 'start_date', [end_date] as 'end_date',timesession  as Session,paid_leaves,unpaid_leaves,(paid_leaves + unpaid_leaves)'sumLeaves', [status],payrollstatus, [Application Date] as 'Application Date',a.Remarks,Leave_Model = Case When  a.leave_model =1 Then 'Fixed Yearly-Normal' When  a.leave_model =7 Then 'Fixed Yearly-Prorated' When  a.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  a.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  a.leave_model =3 Then 'Year of Service-Normal' When  a.leave_model =8 Then 'Year of Service-Prorated' When  a.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  a.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END FROM [emp_leaves] a Inner Join Leave_Types b On a.leave_type=b.id Inner Join Employee e On a.Emp_ID = e.emp_code Where (a.emp_id = "+ cmbEmployee.SelectedValue.ToString() +") And Year(a.start_date)="+ cmbYear.SelectedValue.ToString() +" order by [Application Date],[start_date], [end_date]  asc";
            RadGrid1.DataBind();
        }
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string sSQL = "";
                GridEditableItem editedItem = e.Item as GridEditableItem;
                object id = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
                string status = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["status"]).ToUpper();
                int leave_type = Utility.ToInteger(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["leave_type"]);

                string ltype = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["type"]);
                string stdate = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["start_date"]);
                string endate = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["end_date"]);
                double paid = Convert.ToDouble(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["paid_leaves"]);
                double unpaid = Convert.ToDouble(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["unpaid_leaves"]);
                string empid = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_id"]);
                string remark = Convert.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["Remarks"]);


                //if(!string.IsNullOrEmpty(remark) && remark =="LEAVE ENCASH")
                //{
                //    sSQL = " UPDATE EmployeeLeavesAllowed SET LY_Leaves_Bal =LY_Leaves_Bal+" + paid + ", LEAVEeNCASH= lEAVEeNCASH-" + paid + " WHERE lEAVE_TYPE=8 AND EMP_id=" + empid + " AND LEAVE_YEAR= YEAR('" + stdate + "')";

                //}


                string status_type = "";
                if (status.ToString() == "OPEN")
                {
                    status_type = "OPEN"; //MURUGAN
                }
                else
                {
                    if (leave_type == 8)
                    {
                        sSQL = sSQL + "; Update Leaves_annual Set Leave_Remaining = Leave_Remaining + (select distinct(e.paid_leaves) from emp_leaves e,emp_leaves_detail eld where eld.trx_id=e.trx_id and year(leave_date)=" + cmbYear.SelectedValue + " and e.leave_type=" + leave_type.ToString() + " and e.status='Approved' And e.trx_id=" + Utility.ToString(id) + ") Where Emp_Id =" + cmbEmployee.SelectedItem.Value.ToString() + " and leave_year =" + cmbYear.SelectedValue + ";";
                    }
                }

                //
                string trx_id = Utility.ToString(id);
                sendemail(ltype, empid, stdate, endate, paid, unpaid, status, trx_id);
                //

                //string sSQL = " Update Leaves_annual Set Leave_Remaining = Leave_Remaining + isnull((Select sum(Eld.Leavecnt) From Emp_Leaves El Inner Join  (Select trx_id, leavecnt=Case When Halfday_leave=0 Then 1 Else 0.5 End From emp_leaves_detail Where unpaid_leave=0 And trx_id=" + Utility.ToString(id) + " And year(Leave_Date) =" + cmbYear.SelectedValue + ") eld  On el.trx_id=eld.trx_id Where el.[Status]='Approved' And el.Leave_Type=8),0) Where Emp_Id = " + cmbEmployee.SelectedItem.Value.ToString() + " and leave_year = " + cmbYear.SelectedValue;
                sSQL = sSQL + "Delete from Emp_Leaves          where [trx_id]= " + Utility.ToString(id);
                sSQL = sSQL + ";Delete from Emp_Leaves_Detail   where [trx_id]= " + Utility.ToString(id);

                sSQL = sSQL + ";Delete from emp_additions   where remarks= '" + Utility.ToString(id)+"'";
                //Added by Jammu Office
                #region Audit
                var oldrecord = new EmpLeaf();
                var newrecord = new EmpLeaf();
                int trxid = Convert.ToInt32(id);
                using (var _context = new AuditContext())
                {
                    oldrecord = _context.EmployeeLeaves.Where(m => m.TrxId == trxid).FirstOrDefault();
                }
                var AuditRepository = new AuditRepository();
                AuditRepository.CreateAuditTrail(AuditActionType.Delete, LoginEmpcode, trxid, oldrecord, newrecord);
                #endregion
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
                if (retVal >= 1)
                {
                    //lblMsg.Text = "Leaves Deleted Successfully.";
                    strMessage = status + " Leave is Deleted Successfully. <br/>" + strMessage;
                    _actionMessage = "Fixed|"+strMessage;
                   
                }
                else
                {
                    strMessage = status + " Leaves Deletion Failed  <br/>" + strMessage;
                    //lblMsg.Text = "Leaves Deletion Failed.";
                    _actionMessage = "Fixed|"+strMessage;
                   
                }

               // string trx_id = Utility.ToString(id);

                //sendemail(ltype, empid, stdate, endate, paid, unpaid, status, trx_id);
               
                if (strMessage.Length > 0)
                {
                    //ShowMessageBox(strMessage);
                   
                    ViewState["actionMessage"] = _actionMessage;
                    strMessage = "";
                }
                SqlDataSource2.SelectCommand = "SELECT [Path],[trx_id], [emp_id], [leave_type] ,b.type,[start_date] as 'start_date', [end_date] as 'end_date',timesession  as Session,paid_leaves,unpaid_leaves,(paid_leaves + unpaid_leaves)'sumLeaves', [status],payrollstatus, [Application Date] as 'Application Date',a.Remarks,Leave_Model = Case When  a.leave_model =1 Then 'Fixed Yearly-Normal' When  a.leave_model =7 Then 'Fixed Yearly-Prorated' When  a.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  a.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  a.leave_model =3 Then 'Year of Service-Normal' When  a.leave_model =8 Then 'Year of Service-Prorated' When  a.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  a.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END FROM [emp_leaves] a Inner Join Leave_Types b On a.leave_type=b.id Inner Join Employee e On a.Emp_ID = e.emp_code Where (a.emp_id = " + cmbEmployee.SelectedValue.ToString() + ") And Year(a.start_date)=" + cmbYear.SelectedValue.ToString() + " order by [Application Date],[start_date], [end_date]  asc";
                RadGrid1.DataBind();
            }
        }
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string enddateday = dataItem["end_date"].Text.Substring(0, 2);
                string enddatemonth = dataItem["end_date"].Text.Substring(3, 2);
                string enddateyear = dataItem["end_date"].Text.Substring(6, 4);

                sUserName = Utility.ToString(Session["Username"]);
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "SELECT GroupManage FROM company where company_id=" + Utility.ToInteger(Session["Compid"]) , null);
                string mnggroup = "";
                if (dr.Read())
                {
                    mnggroup = dr[0].ToString();
                }

                sgroupname = Utility.ToString(Session["GroupName"]);

                if (mnggroup == "1")
                { 
                    if (dataItem["status"].Text == "Approved" && sgroupname != "Super Admin" && Utility.AllowedAction1(Session["Username"].ToString(), "Remove Approved Leaves") == false)
                    {
                        dataItem["DeleteColumn"].Visible = false;
                    }
                //new rights
                if (Utility.AllowedAction1(Session["Username"].ToString(), "Cancel Approved Leave By Emp") == true)
                {
                    dataItem["DeleteColumn"].Visible = true;
                }

                if (Utility.AllowedAction1(Session["Username"].ToString(), "Cancel Approved Leave By Supervisor") == true)
                {
                    dataItem["DeleteColumn"].Visible = true;
                }
               }
                //


                if (dataItem["payrollstatus"].Text == "L" && dataItem["status"].Text == "Rejected" && sgroupname == "Super Admin")
                {
                    dataItem["DeleteColumn"].Visible = false;
                }

                if ((dataItem["payrollstatus"].Text == "L" && dataItem["status"].Text == "Approved"))
                {
                    dataItem["DeleteColumn"].Visible = false;
                }

                int n;
                if (dataItem["payrollstatus"].Text == "U" && (dataItem["status"].Text == "Approved" || int.TryParse(dataItem["status"].Text,out n)) && sgroupname != "Super Admin")
                {
                    dataItem["DeleteColumn"].Visible = false;
                }

                //new logic(jun-04) if payroll is processed and leave is approved --dont allow to delete
                //string sssql = "select status from prepare_payroll_hdr A inner join prepare_payroll_detail B on A.trx_id=B.trx_id where B.emp_id='" + dataItem["emp_id"].Text + "'and MONTH(start_period)='" + Convert.ToDateTime(dataItem["start_date"].Text.ToString()).Month + "' and YEAR(start_period)='" + Convert.ToDateTime(dataItem["start_date"].Text.ToString()).Year + "' and status='G'";
                //SqlDataReader dr1_l = DataAccess.ExecuteReader(CommandType.Text, sssql, null);
                //if (dr1_l.Read())
                //{
                //    payrollstatus = dr1_l[0].ToString();
                //}
                //dr1_l.Close();

                //if ((payrollstatus == "G" && dataItem["status"].Text == "Approved"))
                //{
                //    dataItem["DeleteColumn"].Visible = false;
                //}
                //else
                //{
                //    dataItem["DeleteColumn"].Visible = true;
                //}
                //

            }
        }

        protected void sendemail(string leavtype, string empcode, string fromdate, string todate, double paidleaves, double unpaidleaves, string status, string trx_id)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            //string from_date = (RadDatePicker1.DbSelectedDate.ToString().Substring(0, 10));
            //string to_date = (RadDatePicker2.DbSelectedDate.ToString().Substring(0, 10));
            string emailreq = "";
            string body = "";
            string cc = "";


            string sSQLemail = "sp_send_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(empcode));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            string supnext_email = "";
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                to = Utility.ToString(dr3.GetValue(3));

                supnext_email = Utility.ToString(dr3.GetValue(2));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                emp_name = Utility.ToString(dr3.GetValue(5));
                body = Utility.ToString(dr3.GetValue(18));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                cc = Utility.ToString(dr3.GetValue(17));
            }
            if (emailreq == "yes")
            {
                if (to.ToString().Trim().Length <= 0)
                {
                    to = cc;
                }
                string subject = "Leave with " + status.ToString().Trim() + " status of " + emp_name.ToString().Trim() + " has been Deleted";
                //body = "Approved @leave_type of Employee @emp_name @paid_leaves Paid Leaves and @unpaid_leaves Un-Paid Leaves From @from_date to @to_date has been deleted";
                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@from_date", fromdate.Substring(0, 10));
                body = body.Replace("@to_date", todate.Substring(0, 10));
                body = body.Replace("@leave_type", leavtype);
                body = body.Replace("@paid_leaves", paidleaves.ToString());
                body = body.Replace("@unpaid_leaves", unpaidleaves.ToString());
                body = body.Replace("@status", status);

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(Utility.ToInteger(Session["Compid"]));

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;

                //check if MultiLevel -ram


                string sql1 = @" select approver from emp_leaves where trx_id='" + trx_id + "'";
                
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr1.Read())
                    Approver = dr1[0].ToString();
                dr1.Close();
                
                
                int id=0;
                bool ismultilevel = int.TryParse(Approver, out id);
                if(ismultilevel && id !=0)
                {
                //if (Approver == "MultiLevel")
                //if (Approver.ToString().Length > 0)
              

                    //string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + empcode + "))) union select email from employee where emp_code=" + empcode + "";
                    string sql = @"select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId=(select PayRollGroupID from EmployeeWorkFlowLevel where ID=(select Leave_supervisor from employee where emp_code=" + empcode + "))) union select email from employee where emp_name='" + Approver + "'";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    string email;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (dr.Read())
                    {
                        email = dr[0].ToString() + ";";
                        strUpdateBuild.Append(email);
                    }

                    email = strUpdateBuild.ToString();
                    to = email;
                    //to = "Shashank@anbgroup.com";
                }
                else 
                { 
                string emp_supervisor = "";
                varEmpCode = Session["EmpCode"].ToString();

                string sql4 = "select emp_supervisor from employee where emp_code=" + empcode;
                SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, sql4, null);
                if (dr19.Read())
                {
                    emp_supervisor = (dr19[0] == null) ? "0" : dr19[0].ToString();

                }
                if (emp_supervisor != empcode)//kumar added on 02_jun_2017
                {
                           if (supnext_email.Length > 0)
                           {
                               to = to + ";" + supnext_email;
                           }
                }
                  //kumar comment on 02-jun_2017
                //if (v == varEmpCode)
                //{
                //    string sql3 = "select email from employee where emp_code=" + empcode ;
                //    SqlDataReader dr20 = DataAccess.ExecuteReader(CommandType.Text, sql3, null);
                //    if (dr20.Read())
                //    {
                //        to = dr20[0].ToString();
                //    }


                //}
                
                }
                
                             
                

                    //--superadmin login

                    //SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
                    //if (dr9.Read())
                    //{
                    //    if (dr9[0].ToString() == "Super Admin")
                    //    {
                    //        string  sql2 = "select email from employee where emp_code=" + empcode;
                    //        SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql2, null);
                    //        if (dr11.Read())
                    //        {
                    //            to = to + dr11[0].ToString();
                    //        }
                    //        if (supnext_email.Length > 0)
                    //        {
                    //            to = to + ";" + supnext_email;
                    //        }

                    //    }


                    //}

              
                //
                //if (status != "OPEN")
                //{

                //    to = to + supnext_email;
                //}
                //---murugan
                if (to.Length == 0 || from.Length == 0)
                {

                  //  ShowMessageBox("Please check email address is not configured yet");
                    _actionMessage = "Warning|Please check email address is not configured yet";
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }
                //------


                oANBMailer.To = to;
                oANBMailer.Cc = cc;


                try
                {
                    //string sRetVal = oANBMailer.SendMail();
                    string sRetVal = oANBMailer.SendMail("Leave", emp_name, fromdate, todate, "Leave Deleted");

                    if (sRetVal == "SUCCESS")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to :<br/> "+to+"<br/> And cc to : "+cc ;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to :<br/> " + to  ;
                            }
                        }
                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail to : <br/>"+ emp_name;
                    }
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail to : <br/>"+ emp_name;
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
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }
        protected void grid_SortCommand(object source, GridSortCommandEventArgs e)
        {
            SqlDataSource2.SelectCommand = "SELECT [Path],[trx_id], [emp_id], [leave_type] ,b.type,[start_date] as 'start_date', [end_date] as 'end_date',timesession  as Session,paid_leaves,unpaid_leaves,(paid_leaves + unpaid_leaves)'sumLeaves', [status],payrollstatus, [Application Date] as 'Application Date',a.Remarks,Leave_Model = Case When  a.leave_model =1 Then 'Fixed Yearly-Normal' When  a.leave_model =7 Then 'Fixed Yearly-Prorated' When  a.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  a.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  a.leave_model =3 Then 'Year of Service-Normal' When  a.leave_model =8 Then 'Year of Service-Prorated' When  a.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  a.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END FROM [emp_leaves] a Inner Join Leave_Types b On a.leave_type=b.id Inner Join Employee e On a.Emp_ID = e.emp_code Where (a.emp_id = " + cmbEmployee.SelectedValue.ToString() + ") And Year(a.start_date)=" + cmbYear.SelectedValue.ToString() + " order by [Application Date],[start_date], [end_date]  asc";
            RadGrid1.DataBind();
        }

    }
}
