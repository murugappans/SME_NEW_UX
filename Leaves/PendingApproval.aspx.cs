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
using System.Net.Mail;
using System.IO;
using efdata;
using System.Text;
using AuditLibrary;//Added by Jammu Office
using System.Linq;

namespace SMEPayroll.Leaves
{
    //TODO: Guess
    public partial class PendingApproval : System.Web.UI.Page
    {
        string strMessage = ""; 
         StringBuilder strMailonotconfigured = new StringBuilder();
         StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string s = "", varEmpName = "";
        int compid;
        DataSet dsleaves;
        string email;
        public string WorkFlowName;
        int appLevel;
        int lcount = 0;
        string ecode;
        string loginEmpCode = "";
        string _actionMessage = "";
        int LoginEmpcode = 0;//Added by Jammu Office
        protected void Page_Load(object sender, EventArgs e)
        {
            RadGrid1.ExportSettings.FileName = "LeavePendingList";
            radApprovedLeave.ExportSettings.FileName = "Related_LeavePendingList";
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varEmpName = Session["Emp_Name"].ToString();
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString(); //Added by Sandi on 27/03/2014
            loginEmpCode = Utility.ToString(Session["EmpCode"]);
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office


            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all")) == false)
                {
                    TextBox1.Text = "approver";
                }
                else
                {
                    TextBox1.Text = "noapprover";
                }
                compid = Utility.ToInteger(Session["Compid"]);

                if ( (string)Session["EmpCode"] != "0")//if user login
                {
                    s = Session["Username"].ToString();
                    string strSql = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + s + "'";
                    DataSet leaveset = new DataSet();
                    leaveset = getDataSet(strSql);
                    lblsuper.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_name"]);
                }
                else
                {
                    lblsuper.Text = "-";
                }

            }

            //UpDate Database based up on the Request Send 
            string sql = "SELECT emp_id,[status],ApproveDate,em.Leave_supervisor,[Application Date],[ApproveDate],[trx_id] from emp_leaves A INNER JOIN employee em ";
            sql = sql + " ON A.emp_id= em.emp_code  WHERE A.trx_id  IN (SELECT [trx_id]FROM [emp_leaves] a ,leave_types b ,employee c ";
            sql = sql + " WHERE a.emp_id = c.emp_code AND  leave_type = b.id AND ([status] != 'Approved') AND company_id =" + compid + " AND a.approver ='MultiLevel' )";

            DataSet leaveInfo = new DataSet();
            leaveInfo = getDataSet(sql);

            sql = "";
            //Changed by Sandi
            sql = "Select WL.ID, 'L'+ Cast(WL.RowID as varchar(5)) RowID, WF.ID,WF.WorkFlowName,";
            sql = sql + "'FlowType'=Case When WL.FlowType=1 Then 'Payroll' When WL.FlowType=2 Then 'Leaves' ";
            sql = sql + "When WL.FlowType=2 Then 'Claims' End, PG.GroupName,WL.ACTION,WL.ExpiryDays From EmployeeWorkFlowLevel WL ";
            sql = sql + "Inner Join EmployeeWorkFlow WF On WL.WorkFlowID = WF.ID Inner Join PayrollGroup PG On WL.PayRollGroupID = PG.ID Where WL.FlowType=2 Order By WL.RowID";
            
            DataSet updateInfo = new DataSet();
            updateInfo = getDataSet(sql);

            //Update BasedUp on conditions 
            //1 -Get Values in Database for Current Status ...If 1,2,3,4,5 etc then get Values Based On ROWID
            //If Open Status then consider ApplicationDate Else Consider ApproveDate
            //If ApproveDate ---Get Action Ap-(Diff(Today - AppDate)>1)Status=Satus -1 If Status =1 Then Status Approved
            //If ApplicationDate ---Get Action Ap-(Diff(Today - AppDate)>1)Status=Satus -1 If Status =1 Then Status Approved

            if (leaveInfo.Tables.Count > 0)
            {
                DateTime ExDateTime;
                string status = "";
                string Action = "";
                int ExpiDay = 0;
                int LeaveSupId = 0;
                string Rid = "";
                int trxid1 = 0;

                for (int i = 0; i <= leaveInfo.Tables[0].Rows.Count - 1; i++)
                {
                    status = leaveInfo.Tables[0].Rows[i][1].ToString();

                    trxid1 = Utility.ToInteger(leaveInfo.Tables[0].Rows[i][6].ToString());
                    //Status Open Then Get 
                    if (status == "Open")
                    {
                        ExDateTime = Convert.ToDateTime(leaveInfo.Tables[0].Rows[i][4]);
                        LeaveSupId = Utility.ToInteger(leaveInfo.Tables[0].Rows[i][3]);
                        //Get ROWID,Action,ExpiryDays based upon the LeaveSupervisor
                        for (int j = 0; j <= updateInfo.Tables[0].Rows.Count - 1; j++)
                        {
                            if (LeaveSupId.ToString() == updateInfo.Tables[0].Rows[j][0].ToString())
                            {
                                Action = updateInfo.Tables[0].Rows[j][6].ToString();
                                ExpiDay = Utility.ToInteger(updateInfo.Tables[0].Rows[j][7]);
                                Rid = updateInfo.Tables[0].Rows[j][1].ToString();
                                ExDateTime = ExDateTime.AddDays(ExpiDay);                               
                                break;
                            }
                        }
                    }
                    else
                    {

                        ExDateTime = Convert.ToDateTime(leaveInfo.Tables[0].Rows[i][5]);                        
                        Rid = "L" + leaveInfo.Tables[0].Rows[i][1];  
                        //LeaveSupId = Utility.ToInteger(leaveInfo.Tables[0].Rows[i][3]); //Added by Sandi
                        for (int j = 0; j <= updateInfo.Tables[0].Rows.Count - 1; j++)
                        {
                            if (Rid.ToString() == updateInfo.Tables[0].Rows[j][1].ToString())
                            //if (LeaveSupId.ToString() == updateInfo.Tables[0].Rows[j][0].ToString())  //Added by Sandi
                            {
                                Action = updateInfo.Tables[0].Rows[j][6].ToString(); 
                                ExpiDay = Utility.ToInteger(updateInfo.Tables[0].Rows[j][7]);
                                ExDateTime = ExDateTime.AddDays(ExpiDay);                               
                                break;
                            }
                        }
                    }

                    //Check Based Upon Action and DateFifference Update Underlying Table
                    //If Action Approved ..Update For Approved Else Rejected else Deduct 1 level
                    //If Final Status is Level 1 then Make it as Approved ...
                    DateTime today = DateTime.Now.Date;

                    string finalStatusFlag = "";
                    int value = 0;

                    TimeSpan span = today.Subtract(ExDateTime.Date);
                    if (span.Days > 1)
                    {
                        if (Action == "Approved")
                        {
                            int status_1 = 0;
                            if (status == "Open")
                            {
                                if (Rid.Length > 0)
                                {
                                    status_1 = Utility.ToInteger(Rid.Substring(1, 1));
                                }
                                if (status_1 == 1)
                                {
                                    finalStatusFlag = "Approved";
                                }
                                else
                                {
                                    value = status_1 - 1;
                                    finalStatusFlag = value.ToString();
                                }
                            }
                            else
                            {
                                int status1 = Utility.ToInteger(status);
                                if (status1 == 1)
                                {
                                    finalStatusFlag = "Approved";
                                }
                                else
                                {
                                    value = status1 - 1;
                                    finalStatusFlag = value.ToString();
                                }
                            }
                        }
                        else if (Action == "Rejected")
                        {
                            finalStatusFlag = "Rejected";
                        }
                        //Update Statement For Pending records ...    
                //kumar comment start
                        //string Sql = "Update emp_leaves set status='" + finalStatusFlag.ToString() + "',approver='MultiLevel',remarks='Auto Approval',ApproveDate='" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "' where trx_id=" + trxid1;
                        //DataAccess.ExecuteStoreProc(Sql);
                        // kumar comment end
                    }
                }
            }            
        }       
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }      
        //Added by Sandi on 27/03/2014
        protected void ApprovedLeaves()
        {
            radApprovedLeave.DataSource = null;           
            string empcode = "";
            string emp_name = "";
            int trxid = -1;           
            int i = 0;
            DataSet leaveInfo = new DataSet();
            DataSet leaveInfo1 = new DataSet();

            string sgroupname = Utility.ToString(Session["GroupName"]);
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;                    
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[17].Text);

                        if (rdoDepartment.Checked == true)
                            Session["searchfilter"] = "bydepartment";
                        else
                            Session["searchfilter"] = "bycompany";
                        emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[5].Text);
                        lblLeaveApplyEmpName.Text = "<span>" + "<b>" + emp_name + " </b>applying leave from <b>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[8].Text) + "</b> to <b>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[9].Text) + "</b>." + "</span>";

                        Session["stSearchDate"] = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[8].Text);
                        Session["enSearchDate"] = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[9].Text);
                        Session["SearchDept"] = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[23].Text);
                        //Senthil for Group Management
                        if (sgroupname == "Super Admin")
                        {
                            if (Session["stSearchDate"].ToString() == Session["enSearchDate"].ToString())
                            {
                                //Added by Sandi on 08/05/2014
                                string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103) and end_date<= CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103)";

                                if (Session["searchfilter"] == "bydepartment")
                                    sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                leaveInfo = getDataSet(sql);

                                radApprovedLeave.DataSource = leaveInfo.Tables[0];
                                radApprovedLeave.DataBind();
                                //GridToolBar2.Visible = true;

                                i = 1;
                            }
                            else
                            {
                                //Added by Sandi on 25/04/2014
                                string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103)";

                                if (Session["searchfilter"] == "bydepartment")
                                    sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                leaveInfo1 = getDataSet(sql);

                                if (leaveInfo1.Tables[0].Rows.Count == 0)
                                {
                                    //Added by Sandi on 25/04/2014
                                    sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                    sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                    sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                    sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                    sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                    sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                    sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                    sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                    sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                    sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                    sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                    sql = sql + " And [end_date]>=CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103)";

                                    if (Session["searchfilter"] == "bydepartment")
                                        sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                    leaveInfo1 = getDataSet(sql);
                                }

                                radApprovedLeave.DataSource = leaveInfo1.Tables[0];
                                radApprovedLeave.DataBind();
                              //  GridToolBar2.Visible = true;

                                i = 1;
                            }
                        }

                        else
                        {
                            if (Utility.GetGroupStatus(Convert.ToInt32(compid)) == 1)
                            {
                                if (Session["stSearchDate"].ToString() == Session["enSearchDate"].ToString())
                                {
                                    //Added by Sandi on 08/05/2014
                                    string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                    sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                    sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                    sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                    sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                    sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                    sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                    sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                    sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                    sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                    sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + " and c.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE())";
                                    sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103) and end_date<= CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103)";

                                    if (Session["searchfilter"] == "bydepartment")
                                        sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                    leaveInfo = getDataSet(sql);

                                    radApprovedLeave.DataSource = leaveInfo.Tables[0];
                                    radApprovedLeave.DataBind();
                                   // GridToolBar2.Visible = true;

                                    i = 1;
                                }
                                else
                                {
                                    //Added by Sandi on 25/04/2014
                                    string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                    sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                    sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                    sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                    sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                    sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                    sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                    sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                    sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                    sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                    sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "and c.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE())";
                                    sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103)";

                                    if (Session["searchfilter"] == "bydepartment")
                                        sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                    leaveInfo1 = getDataSet(sql);

                                    if (leaveInfo1.Tables[0].Rows.Count == 0)
                                    {
                                        //Added by Sandi on 25/04/2014
                                        sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                        sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                        sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                        sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                        sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                        sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                        sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                        sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                        sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                        sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                        sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                        sql = sql + " And [end_date]>=CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103) and c.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE())";

                                        if (Session["searchfilter"] == "bydepartment")
                                            sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                        leaveInfo1 = getDataSet(sql);
                                    }

                                    radApprovedLeave.DataSource = leaveInfo1.Tables[0];
                                    radApprovedLeave.DataBind();
                                   // GridToolBar2.Visible = true;

                                    i = 1;
                                }
                            }
                            else
                            {
                                if (Session["stSearchDate"].ToString() == Session["enSearchDate"].ToString())
                                {
                                    //Added by Sandi on 08/05/2014
                                    string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                    sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                    sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                    sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                    sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                    sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                    sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                    sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                    sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                    sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                    sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                   sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103) and end_date<= CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103)";

                                    if (Session["searchfilter"] == "bydepartment")
                                        sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                    leaveInfo = getDataSet(sql);

                                    radApprovedLeave.DataSource = leaveInfo.Tables[0];
                                    radApprovedLeave.DataBind();
                                  //  GridToolBar2.Visible = true;

                                    i = 1;
                                }
                                else
                                {
                                    //Added by Sandi on 25/04/2014
                                    string sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                    sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                    sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                    sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                    sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                    sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                    sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                    sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                    sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                    sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                    sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                    sql = sql + " And [start_date]>= CONVERT(DATETIME,N'" + Session["stSearchDate"] + "',103)";

                                    if (Session["searchfilter"] == "bydepartment")
                                        sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                    leaveInfo1 = getDataSet(sql);

                                    if (leaveInfo1.Tables[0].Rows.Count == 0)
                                    {
                                        //Added by Sandi on 25/04/2014
                                        sql = "SELECT [Path],ic_pp_number,DeptName, Designation,c.emp_type,c.time_card_no as TimeCardId,Nationality,";
                                        sql = sql + "(select trade from trade where id=c.Trade_id) as Trade,";
                                        sql = sql + "ISNULL(c.emp_name, '') + ' ' + ISNULL(c.emp_lname, '') 'emp_name', [emp_id],";
                                        sql = sql + "[leave_type], b.type, CONVERT(VARCHAR(15), [start_date], 103) 'start_date', ";
                                        sql = sql + "CONVERT(VARCHAR(15), [end_date], 103) 'end_date', [status], ";
                                        sql = sql + "CONVERT(VARCHAR(15), [Application Date], 103) 'ApplicationDate', a.[remarks], ";
                                        sql = sql + "[attachment], [status], [approver], [trx_id], paid_leaves, unpaid_leaves,   ";
                                        sql = sql + "(paid_leaves + unpaid_leaves) 'sumLeaves', a.timesession ,c.emp_code ";
                                        sql = sql + " FROM [emp_leaves] a , leave_types b ,   employee c,department d,designation dg,nationality n";
                                        sql = sql + " WHERE a.emp_id = c.emp_code AND c.dept_id=d.id AND c.desig_id=dg.id AND c.nationality_id=n.id AND ";
                                        sql = sql + " a.leave_type = b.id AND [status] IN ('Approved') AND c.company_id = " + Utility.ToInteger(Session["Compid"]).ToString() + "";
                                        sql = sql + " And [end_date]>=CONVERT(DATETIME,N'" + Session["enSearchDate"] + "',103)";

                                        if (Session["searchfilter"] == "bydepartment")
                                            sql = sql + " AND DeptName='" + Session["SearchDept"] + "'";

                                        leaveInfo1 = getDataSet(sql);
                                    }

                                    radApprovedLeave.DataSource = leaveInfo1.Tables[0];
                                    radApprovedLeave.DataBind();
                                  //  GridToolBar2.Visible = true;

                                    i = 1;
                                }
                            }
                        }
                    }
                }                
            }
            if (i == 0)
            {
                //ShowMessageBox("Please select one employee from the list");
                _actionMessage = "Warning|Please select one employee from the list";
                ViewState["actionMessage"] = _actionMessage;
            }
        }
        protected void rdoDepartment_CheckedChanged(object sender, EventArgs e)
        {
            rdoEvery.Checked = false;
            rdoDepartment.Checked = true;
            ApprovedLeaves();
        }



        private bool checkemp(string _Approverflag, string _empcode, int _trxid)
        {
            bool result = false;
            if (_Approverflag == "MultiLevel")
            {
                string text = "Select WL.ID,WL.RowID, WorkFlowName ";
                text += "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                text += "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) ";
                text = text + "And Company_ID=" + Utility.ToInteger(this.Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                text = text + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT Leave_supervisor FROM dbo.employee   WHERE emp_code=" + _empcode + " ) ";
                text += "Order By WF.WorkFlowName, WL.RowID";
                this.dsleaves = new DataSet();
                this.dsleaves = DataAccess.FetchRS(CommandType.Text, text, null);
                int num = 0;
                if (this.dsleaves.Tables.Count > 0)
                {
                    if (this.dsleaves.Tables[0].Rows.Count > 0)
                    {
                        num = Utility.ToInteger(this.dsleaves.Tables[0].Rows[0][1]);
                        this.WorkFlowName = Utility.ToString(this.dsleaves.Tables[0].Rows[0][2]);
                    }
                }
                if (num != 0)
                {
                    SqlDataReader sqlDataReader = DataAccess.ExecuteReader(CommandType.Text, "SELECT [STATUS] FROM emp_leaves WHERE trx_id=" + _trxid, null);
                    int num2 = 0;
                    while (sqlDataReader.Read())
                    {
                        if (Utility.ToString(sqlDataReader.GetValue(0)) == "Approved" || Utility.ToString(sqlDataReader.GetValue(0)) == "Open" || Utility.ToString(sqlDataReader.GetValue(0)) == "")
                        {
                            num2 = num;
                            for (int i = num; i >= 1; i--)
                            {
                                if (i == 0)
                                {
                                }
                            }
                        }
                        else
                        {
                            int num3 = Utility.ToInteger(sqlDataReader.GetValue(0));
                            num2 = num3 - 1;
                        }
                    }
                    string str = this.Session["Username"].ToString();
                    string sSQL = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + str + "'";
                    DataSet dataSet = new DataSet();
                    dataSet = PendingApproval.getDataSet(sSQL);
                    string a = "";
                    if (dataSet.Tables.Count > 0)
                    {
                        a = dataSet.Tables[0].Rows[0][0].ToString();
                    }
                    string text2 = "L" + num2;
                    string text3 = "SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea ";
                    text3 += "  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.ID=(select payrollGroupid  from EmployeeWorkFlowLevel ";
                    text3 = string.Concat(new object[]
					{
						text3,
						"where rowid='",
						num2,
						"'  and workflowid=(select id from EmployeeWorkFlow where workflowname='",
						this.WorkFlowName,
						"' and Company_id='",
						Utility.ToInteger(this.Session["Compid"]).ToString(),
						"'))"
					});
                    text3 = text3 + "union select distinct userId from MasterCompany_User where companyid='" + Utility.ToInteger(this.Session["Compid"]).ToString() + "'";
                    SqlDataReader sqlDataReader2 = DataAccess.ExecuteReader(CommandType.Text, text3, null);
                    ArrayList arrayList = new ArrayList();
                    while (sqlDataReader2.Read())
                    {
                        arrayList.Add(sqlDataReader2.GetValue(0));
                    }
                    foreach (object current in arrayList)
                    {
                        if (a == current.ToString())
                        {
                            result = true;
                        }
                    }
                }
            }
            return result;
        }
      
        
        
        
        protected void rdoEvery_CheckedChanged(object sender, EventArgs e)
        {            
            rdoDepartment.Checked = false;
            rdoEvery.Checked = true;
            ApprovedLeaves();
        }
        //End Added
        protected void remarkRadio_CheckedChanged(object sender, EventArgs e)
        {
            ApprovedLeaves();

            string remarks = txtremarks.Text;
            string ApprovalFlag = "";
            string empcode = "";
            int trxid = -1;
            btnapprove.Enabled = true;
            btnreject.Enabled = true;

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                        string strRemarks = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("remarks"));
                        ApprovalFlag = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[16].Text);
                        empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[17].Text);
                        ViewState["ecode"] = empcode.ToString();
                        //divRemarks.InnerText = strRemarks;
                        txtEmpRemarks.Text = strRemarks; //muru
                    }
                }
            }
            //If Selected Row having Approver as Multilevel then Show Details till what level approval is done
            //    if (ApprovalFlag == "MultiLevel")
            //    {
            rowApp.Visible = true;

            string strSql = "Select WL.ID,WL.RowID, WorkFlowName ";

            strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
            strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) ";
            strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
            strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT Leave_supervisor FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
            strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

            dsleaves = new DataSet();
            dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            string message4 = "";

            int maxApprovalleval = 0;
            int appLeveldb = 0;
            if (dsleaves.Tables.Count > 0)
            {
                if (dsleaves.Tables[0].Rows.Count > 0)
                {
                    maxApprovalleval = Utility.ToInteger(dsleaves.Tables[0].Rows[0][1]);

                    //r
                    WorkFlowName = Utility.ToString(dsleaves.Tables[0].Rows[0][2]);
                    ViewState["wfid"] = Utility.ToString(dsleaves.Tables[0].Rows[0][0]);
                }
            }

            if (maxApprovalleval != 0)
            {
                lblApprovalinfo1.Text = "";

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT [STATUS] FROM emp_leaves WHERE trx_id=" + trxid, null);
                int ApproveLvlcnt = 0;
                message = "Multi Level Approval Status :";
                while (dr1.Read())
                {
                    if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open" || Utility.ToString(dr1.GetValue(0)) == "") //Added last condition by Sandi
                    {
                        appLeveldb = maxApprovalleval;
                        ApproveLvlcnt = maxApprovalleval;

                        for (int cnt = maxApprovalleval; cnt >= 1; cnt--)
                        {
                            if (cnt == 0)
                            { }
                            else
                            {
                                message1 = message1 + "[" + WorkFlowName + "-L" + cnt + ":Pending ]";
                                lblApprovalinfo1.Text = "<span style='color:Red'><b>" + message + message1 + "</span><b/>";
                            }
                        }
                    }
                    else
                    {
                        appLeveldb = Utility.ToInteger(dr1.GetValue(0));
                        ApproveLvlcnt = appLeveldb - 1;
                        for (int cnt = maxApprovalleval; cnt >= appLeveldb; cnt--)
                        {
                            if (cnt == 0)
                            { }
                            else
                            {
                                message2 = message2 + "[" + WorkFlowName + "-L" + cnt + ":Approved ]";
                                lblApprovalinfo1.Text = "<span style='color:Red'><b>" + message + "</span><b/>" + "<span style='color:Green'><b>" + message2 + "</span><b/>";
                            }
                        }

                        for (int cnt = appLeveldb - 1; cnt >= 1; cnt--)
                        {
                            if (cnt == 0)
                            { }
                            else
                            {
                                message3 = message3 + "[" + WorkFlowName + "-L" + cnt + ":Pending ] ";
                                lblApprovalinfo1.Text = "<span style='color:Red'><b>" + message + "</span><b/>" + "<span style='color:Green'><b>" + message2 + "</span><b/>" + "<span style='color:Red'><b>" + message3 + "</span><b/>";
                            }
                        }
                    }
                }
                ViewState["wfl"] = ApproveLvlcnt.ToString();
                string s = Session["Username"].ToString();
                string strSql1 = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + s + "'";
                DataSet leaveset = new DataSet();
                leaveset = getDataSet(strSql1);
                string employee_code = "";
                if (leaveset.Tables.Count > 0)
                {
                    employee_code = leaveset.Tables[0].Rows[0][0].ToString();
                }
                string level;
                level = "L" + ApproveLvlcnt;
                lcount = ApproveLvlcnt;
                Session["icount"] = ApproveLvlcnt;

                //L+appLeveldb Get Approval Level And Employess Assigned To IT
                string sql = "SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea ";

                sql = sql + "  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.ID=(select payrollGroupid  from EmployeeWorkFlowLevel ";
                sql = sql + "where rowid='" + ApproveLvlcnt + "'  and workflowid=(select id from EmployeeWorkFlow where workflowname='" + WorkFlowName + "' and Company_id='" + Utility.ToInteger(Session["Compid"]).ToString() + "'))";

                //new code to get master user
                sql = sql + "union select distinct userId from MasterCompany_User where companyid='" + Utility.ToInteger(Session["Compid"]).ToString() + "'";

                SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                System.Collections.ArrayList EmpId = new ArrayList();
                while (dr2.Read())
                {
                    EmpId.Add(dr2.GetValue(0));
                }

                //Check If Employee Logged In Having rights to Appove the leaves Or Not
                bool ApproveRights = false;




                if (Utility.ToString(Session["GroupName"]) == "Super Admin")
                {
                    ApproveRights = true;
                }



                foreach (object ob in EmpId)
                {
                    if (employee_code == ob.ToString())
                    {
                        ApproveRights = true;
                    }
                }
                if (ApproveRights == false)
                {
                    btnapprove.Enabled = false;
                    btnreject.Enabled = false;

                    message4 = message4 + "<br/> (" + Session["Emp_Name"].ToString() + ") Is Not Assigned To " + WorkFlowName + "-" + level + " as Approver";
                    lblApprovalinfo1.Text += "<span style='color:Red'><b>" + message4 + "</span><b/>";

                }
                string sqltooltip = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name' from employee where emp_code in(SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea ON Pg.ID=Ea.PayrollGroupID WHERE Pg.ID in(select payrollGroupid from EmployeeWorkFlowLevel where rowid <=" + maxApprovalleval + " and workflowid=(select id from EmployeeWorkFlow where workflowname='LEAVE LVL' and Company_id='3')) union select userId from MasterCompany_User where companyid='" + Utility.ToInteger(Session["Compid"]).ToString() + "')";
                DataSet leavetooltip = new DataSet();
                leavetooltip = getDataSet(sqltooltip);
                int l = 0;
                if (leavetooltip.Tables.Count > 0)
                {
                    for (int i = 0; i < leavetooltip.Tables[0].Rows.Count; i++)
                    {
                        l = maxApprovalleval - i;
                        lblApprovalinfo1.ToolTip += leavetooltip.Tables[0].Rows[i].ItemArray[1].ToString() + "(" + WorkFlowName + "-L" + l.ToString() + ")\n";
                    }
                }

            }
            else
            {
                lblApprovalinfo1.Text = "MultiLevel Approver -Please Select WorkFlow";
            }
            //}
            //else
            //{
            //    rowApp.Visible = false;
            //}  

        }
        protected void btnapprove_Click(object sender, EventArgs e)
        {        
            ApproveLeaveClick();
        }

        //new
        private void ApproveLeaveClick()
        {
            string remarks = txtEmpRemarks.Text + " - " + txtremarks.Text;
            string workFlowName = "";
            string workflowId = "";
            bool chkd = false;
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;                  
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        chkd = true;
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                        string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
                        string empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[17].Text);
                        string sSQLCheck = "select payrollstatus, isnull(paid_leaves,0) paid_leaves, isnull(unpaid_leaves,0) unpaid_leaves from emp_leaves where trx_id = {0}";
                        sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                        string status = "";
                        double paid_leaves = 0;
                        double unpaid_leaves = 0;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                        while (dr.Read())
                        {
                            status = Utility.ToString(dr.GetValue(0));
                            paid_leaves = Utility.ToDouble(dr.GetValue(1));
                            unpaid_leaves = Utility.ToDouble(dr.GetValue(2));
                        }
                        appLevel = 0;
                        if ((status == "U") || (status == "L" && unpaid_leaves <= 0))
                        {
                            bool flagUpdatefinal = true;
                            int appLeveldb = 0;
                            
                            string strSql = "Select WF.ID,WL.RowID,WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName,WorkFlowName WName ";
                            strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                            strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=2) ";
                            strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                            strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT Leave_supervisor FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
                            strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

                            dsleaves = new DataSet();
                            dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);

                            if (dsleaves.Tables.Count > 0)
                            {
                                if (dsleaves.Tables[0].Rows.Count > 0)
                                {
                                    flagUpdatefinal = false;
                                    int maxApprovalleval = Utility.ToInteger(dsleaves.Tables[0].Rows[0][1]);
                                    workFlowName = Utility.ToString(dsleaves.Tables[0].Rows[0][3]); //Added by Sandi
                                    workflowId = Utility.ToString(dsleaves.Tables[0].Rows[0][0]);
                                    //Approved Open
                                    //If Approved and open status then appLevel =0
                                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT [STATUS] FROM emp_leaves Where trx_id=" + trxid, null);
                                    while (dr1.Read())
                                    {
                                        if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open" || Utility.ToString(dr1.GetValue(0)) == "") //Added last condition by Sandi
                                        {
                                            appLevel = maxApprovalleval;
                                        }
                                        else
                                        {
                                            appLeveldb = Utility.ToInteger(dr1.GetValue(0));
                                            appLevel = appLeveldb - 1;
                                        }
                                    }
                                    if (appLevel == 1)
                                    {
                                        flagUpdatefinal = true;
                                    }
                                }
                            }

                            if (flagUpdatefinal)
                            {
                                string Sql = "Update emp_leaves set status='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "',ApproveDate='" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "' where trx_id=" + trxid;
                                #region Audit
                                var oldrecord = new EmpLeaf();
                                using (var _context = new AuditContext())
                                {
                                    oldrecord = _context.EmployeeLeaves.Where(m => m.TrxId == trxid).FirstOrDefault();
                                }
                                var newrecord = new EmpLeaf()
                                {
                                    TrxId = trxid,
                                    Status = appLevel.ToString(),
                                    Approver = varEmpName.Replace("'", ""),
                                    ApproveDate = DateTime.Now,
                                    Remarks = remarks

                                };
                                var AuditRepository = new AuditRepository();
                                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, trxid, oldrecord, newrecord);

                                #endregion
                                try
                                {
                                    DataAccess.ExecuteStoreProc(Sql);
                                    strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                   // sendemail(trxid, emp_name, "","");
                                    sendemail(trxid, empcode, workflowId.ToString(), appLevel.ToString(), emp_name);
                                }
                                catch (Exception ex)
                                {
                                    string ErrMsg = ex.Message;
                                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                    {
                                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                        strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                    }
                                    else
                                    {
                                        strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                    }
                                }
                            }
                            else
                            {
                                string ApproverGroup = "";

                                int rowid = appLevel - 1;

                                string wdsql = "select PayrollGroupID from [EmployeeWorkFlowLevel] WL where WL.RowID=" + rowid.ToString() + "and WorkFlowID =" + workflowId.ToString();

                                DataSet approverCode = new DataSet();
                                approverCode = getDataSet(wdsql);
                                int Cnt = approverCode.Tables[0].Rows.Count;
                                if (Cnt != 0)
                                {
                                    ApproverGroup = approverCode.Tables[0].Rows[0]["PayrollGroupID"].ToString();
                                }





                                string Sql = "Update emp_leaves set status='" + appLevel.ToString() + "',approver=+"+ApproverGroup+",remarks='" + remarks + "',ApproveDate='" + DateTime.Now.Date.Year + "-" + DateTime.Now.Date.Month + "-" + DateTime.Now.Date.Day + "' where trx_id=" + trxid;
                                //Added by Jammu Office
                                #region Audit
                                var oldrecord = new EmpLeaf();
                                using (var _context = new AuditContext())
                                {
                                    oldrecord = _context.EmployeeLeaves.Where(m => m.TrxId == trxid).FirstOrDefault();
                                }
                                var newrecord = new EmpLeaf()
                                {
                                    TrxId = trxid,
                                    Status = appLevel.ToString(),
                                    Approver = ApproverGroup,
                                    ApproveDate = DateTime.Now,
                                    Remarks = remarks

                                };
                                var AuditRepository = new AuditRepository();
                                AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, trxid, oldrecord, newrecord);
                                
                                #endregion
                                try
                                {
                                        DataAccess.ExecuteStoreProc(Sql);
                                        strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                        //sendemail(trxid, emp_name, ""); //Comment by Sandi
                                        //Send an email to appLevel.ToString() -1  Approver 

                                        int arroverLevel = Convert.ToInt32(appLevel.ToString());
                                        if ((arroverLevel - 1) != 0)
                                        {

                                            //Get List Of All Employeees whose level is arroverLevel-1
                                            //string payrollGroup = "SELECT e.email,* FROM employee e WHERE e.emp_code IN (SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.GroupName='L" + (arroverLevel - 1) + "')"; //Comment by Sandi                                       
                                            int level = appLevel - 1;


                                            string payrollGroup = "Select email,Leave_supervisor,* from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId= (select PayRollGroupID from EmployeeWorkFlowLevel where RowID=" + level + " and WorkFlowID =" + workflowId + "))";
                                            SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, payrollGroup);
                                            StringBuilder strUpdateBuild = new StringBuilder();

                                            while (dr4.Read())
                                            {
                                                email = dr4[0].ToString() + ";";
                                                strUpdateBuild.Append(email);
                                            }
                                        //sendemail(trxid, emp_name, email, workFlowName);
                                        sendemail(trxid, empcode, workflowId.ToString(), appLevel.ToString(), emp_name);
                                        if (appLevel == 1)
                                            {
                                                flagUpdatefinal = true;
                                            }
                                        }
                                        else
                                        {
                                        // sendemail(trxid, emp_name, "", ""); //Added by Sandi
                                        sendemail(trxid, empcode, workflowId.ToString(), appLevel.ToString(), emp_name);
                                    }
                                    }
                                    catch (Exception ex)
                                    {
                                        string ErrMsg = "Some error occured.";
                                        if (ex.Message.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                        {
                                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                            strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                        }
                                        else
                                        {
                                            strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                        }
                                    }
                            }
                        }
                        else
                        {
                            //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                            _actionMessage = "Warning|Payroll has been Processed, Action not allowed.";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }
                }
            }
            if(!chkd)
            {
                _actionMessage = "Warning|First Select any Employee .";
                ViewState["actionMessage"] = _actionMessage;
            }
           
            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Leave Approved Successfully for: <br/>" + strSucSubmit.ToString());
                _actionMessage = "Fixed|Leave Approved Successfully for:"+ strSucSubmit.ToString();               
                strMessage = "";
                lblLeaveApplyEmpName.Text = "";
                rowApp.Visible = false;
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Leave Not Approved for: <br/>" + strFailSubmit.ToString());
                _actionMessage = "Fixed|Leave Not Approved for: <br/>"+ strFailSubmit.ToString();                
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to:"  + strPassMailMsg.ToString());
                _actionMessage += "<br/>Email Send successfully to:" + strPassMailMsg.ToString();
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/>Error While sending Email to:  " + strFailMailMsg.ToString();                
                strMessage = "";
            }
            if (strMailonotconfigured.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/>"+strMailonotconfigured;
                strMessage = "";
            }
            RadGrid1.DataBind();
            txtremarks.Text = "";
           
            ViewState["actionMessage"] = _actionMessage;
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            string remarks = txtEmpRemarks.Text + " - "  + txtremarks.Text;

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string sSQLCheck = "select payrollstatus from emp_leaves where trx_id = {0}";
                        string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
                        sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                        string status = "";
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                        while (dr.Read())
                        {
                            status = Utility.ToString(dr.GetValue(0));
                        }
                        //if (status == "U")
                        //{
                            string Sql = "Update emp_leaves set status='Rejected',approver='" + varEmpName + "',remarks='" + remarks.Replace("'","") + "' where trx_id=" + trxid;
                        //Added by Jammu Office
                        #region Audit
                        var oldrecord = new EmpLeaf();
                        using (var _context = new AuditContext())
                        {
                            oldrecord = _context.EmployeeLeaves.Where(m => m.TrxId == trxid).FirstOrDefault();
                        }
                        var newrecord = new EmpLeaf()
                        {
                            TrxId = trxid,
                            Status = "Rejected",
                            Approver = varEmpName,
                            Remarks = remarks

                        };
                        var AuditRepository = new AuditRepository();
                        AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, trxid, oldrecord, newrecord);

                        #endregion
                        try
                        {
                                DataAccess.ExecuteStoreProc(Sql);
                                strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                string s = "";
                                if (Session["icount"] == null)
                                {
                                    s = "0";
                                }
                                else
                                {
                                    s = Session["icount"].ToString();
                                }

                            string wfl = "";
                            string wfid = "";
                            if (ViewState["wfl"] == null)
                            {
                                wfl = "0";
                            }
                            else
                            {
                                wfl = ViewState["wfl"].ToString();
                            }
                            if (ViewState["wfid"] == null)
                            {
                                wfid = "0";
                            }
                            else
                            {
                                wfid = ViewState["wfid"].ToString();
                            }

                            string ecode = ViewState["ecode"].ToString();

                            ///sendemail(trxid, emp_name,s ,"");
                            Rejectsendemail(trxid, ecode, wfid, wfl, emp_name);
                        }
                            catch (Exception ex)
                            {
                                string ErrMsg = ex.Message;
                                if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                {
                                    //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                    strFailSubmit.Append( Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                }
                                else
                                {
                                    strFailMailMsg.Append( Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                }
                            }
                        //}
                        //else
                        //{
                        //    Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                        //} //Senthil-10/08/15
                    }
                }
            }

            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Leave Rejected Successfully for: <br/>" + strSucSubmit.ToString());
                _actionMessage = "Fixed|Leave Rejected Successfully for: <br/>" + strSucSubmit.ToString();
                lblLeaveApplyEmpName.Text = "";
                strMessage = "";
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Leave Not Rejected for: <br/>" + strFailSubmit.ToString());
                _actionMessage = "Fixed|Leave Not Rejected for: <br/>"+ strFailSubmit.ToString();
     
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                _actionMessage += "<br/><br/>Email Send successfully to: <br/>" + strPassMailMsg.ToString();
 
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/><br/>Error While sending Email to: <br/>" + strFailMailMsg.ToString();

                strMessage = "";
            }
if (strMailonotconfigured.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/>" + strMailonotconfigured.ToString();

                strMessage = "";
            }
            ViewState["actionMessage"] = _actionMessage;
            RadGrid1.DataBind();
            txtremarks.Text = "";
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Display = false;
            if (e.Item is GridDataItem)
            {
                GridDataItem gridDataItem = (GridDataItem)e.Item;
                int trxid = Utility.ToInteger(gridDataItem.GetDataKeyValue("trx_id").ToString());
                string text = Utility.ToString(gridDataItem.GetDataKeyValue("Approver").ToString());
                string empcode = Utility.ToString(gridDataItem.GetDataKeyValue("emp_id").ToString());
                if (text == "MultiLevel")
                {
                    gridDataItem.Display = (this.checkemp(text, empcode, trxid));
                }
            }

            if (((Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves")) || (Utility.AllowedAction1(Session["Username"].ToString(), "Approve and Reject Leaves for all"))) == false)
            {
                RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            }
        }
        //protected void sendemail(int id, string emp_name, string empMultlevel,string wFlowName)
        //{
        //    string applicationdate = "";
        //    string from = "";
        //    string to = "";
        //    string SMTPserver = "";
        //    string SMTPUser = "";
        //    string SMTPPass = "";
        //    string status = "";
        //    string approver = "";
        //    string from_date = "";
        //    string to_date = "";
        //    string reason = "";
        //    string emailreq = "";
        //    int SMTPPORT = 25;
        //    string body = "";
        //    string cc = "";
        //    string leave_type = "";
        //    string applicant_name="";
        //    string leave_supervisor_one_to_one = ""; 
        //    if (empMultlevel.Length == 1) {
        //        appLevel = int.Parse(empMultlevel);
        //        empMultlevel = "";
        //    }

        //    if (empMultlevel.Length <= 0)
        //    {
        //        string sSQLemail = "sp_send_email_status";
        //        SqlParameter[] parmsemail = new SqlParameter[1];
        //        parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

        //        SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
        //        while (dr3.Read())
        //        {
        //            from = Utility.ToString(dr3.GetValue(18));
        //            to = Utility.ToString(dr3.GetValue(15));
        //            SMTPserver = Utility.ToString(dr3.GetValue(5));
        //            SMTPUser = Utility.ToString(dr3.GetValue(6));
        //            SMTPPass = Utility.ToString(dr3.GetValue(7));
        //            approver = Utility.ToString(dr3.GetValue(2));
        //            status = Utility.ToString(dr3.GetValue(3)); //Utility.ToString(dr3.GetValue(3)); Comment by Sandi
        //            applicationdate = Utility.ToString(dr3.GetValue(4));
        //            from_date = Utility.ToString(dr3.GetValue(0));
        //            to_date = Utility.ToString(dr3.GetValue(1));
        //            reason = Utility.ToString(dr3.GetValue(16));
        //            body = Utility.ToString(dr3.GetValue(10));
        //            SMTPPORT = Utility.ToInteger(dr3.GetValue(12));
        //            emailreq = Utility.ToString(dr3.GetValue(17)).ToLower();
        //            cc = Utility.ToString(dr3.GetValue(19));
        //            leave_type = Utility.ToString(dr3.GetValue(20));
        //            applicant_name = Utility.ToString(dr3.GetValue(21));
        //            leave_supervisor_one_to_one = Utility.ToString(dr3.GetValue(22));
        //        }
        //        if (to == ";")
        //            to = "";
        //        //kumar Added
        //        SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + loginEmpCode, null);
        //        if (dr9.Read())
        //        {
        //            if (dr9[0].ToString() == "Super Admin")
        //            {

        //                if (leave_supervisor_one_to_one.Length >0)
        //                {
        //                    to = to + ";" + leave_supervisor_one_to_one;
        //                }

        //            }


        //        }





        //        //kumar commented on 02-06-2017
        //       // //-------------- murugan
        //       //// string str = "select Leave_supervisor from employee where emp_name='" + emp_name + "'";
        //       // string str = "select emp_supervisor from employee where emp_name='" + emp_name + "'";
        //       // SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, str, null);
        //       // string sup = "";
        //       // if (dr.Read())
        //       // {
        //       //     sup = dr[0].ToString();
        //       // }
        //       // if(sup!="-1")
        //       // {
        //       //     string sql = "";

        //       //      SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
        //       //      if (dr9.Read())
        //       //      {
        //       //          if (dr9[0].ToString() == "Super Admin")
        //       //          {

        //       //              sql = "SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //       //              sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>=" + appLevel;
        //       //          }
        //       //          else {
        //       //              sql = "SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //       //              sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>" + appLevel;

        //       //          }
        //       //      }
        //       // SqlDataReader dr8 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
        //       // string email;
        //       // StringBuilder strUpdateBuild = new StringBuilder();
        //       // while (dr8.Read())
        //       // {
        //       //     email = dr8[0].ToString() + ";";
        //       //     strUpdateBuild.Append(email);
        //       // }

        //       // email = strUpdateBuild.ToString();
        //       // to = to+";"+email;
        //       // }
        //       // //------------
        //    }
        //    else
        //    {
        //        //string sqlEmail = "SELECT CONVERT(VARCHAR(10), [start_date], 103) [start_date], CONVERT(VARCHAR(10), end_date, 103) [end_date],approver, [status], CONVERT(VARCHAR(10), [Application date], 103) [Application date],b.email_SMTP_server, b.email_username, b.email_password, b.email_sender_domain, b.email_sender_name,b.email_reply_address, b.email_reply_name, b.email_smtp_port, b.email, c.company_id, c.email,a.remarks, b.email_leavealert, b.email_sender, b.ccalert_leaves FROM   emp_leaves a ,company b ,employee c Where c.emp_name='" + emp_name + "'";
        //        //SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail, null);

        //        string sSQLemail1 = "sp_send_email_status";
        //        SqlParameter[] parmsemail1 = new SqlParameter[1];
        //        parmsemail1[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

        //        SqlDataReader dr5 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail1, parmsemail1);

        //        //Decide To And From email :
        //        string aaprover1 = "";


        //        if (empMultlevel.Length <= 0)
        //        {

        //            string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
        //            SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);

        //            while (dr6.Read())
        //            {
        //                aaprover1 = Utility.ToString(dr6.GetValue(0));
        //            }
        //            //------------------murugan
        //            string sql = "";

        //             SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
        //             if (dr9.Read())
        //             {
        //                 if (dr9[0].ToString() == "Super Admin")
        //                 {
        //                     sql = "SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //                     sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>=" + appLevel;

        //                 }
        //                 else {
        //                     sql = "SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //                     sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>" + appLevel;

        //                 }

        //             }
        //           SqlDataReader    dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
        //            string email;
        //            StringBuilder strUpdateBuild = new StringBuilder();
        //            while (dr.Read())
        //            {
        //                email = dr[0].ToString() + ";";
        //                strUpdateBuild.Append(email);
        //            }

        //            email = strUpdateBuild.ToString();

        //            email = strUpdateBuild.ToString();
        //             aaprover1 =aaprover1+ ";"+ empMultlevel +";"+ email;
        //            //------------------------------
        //        }
        //        else
        //        {
        //            string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
        //            SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
        //            if (dr6.Read())
        //            {
        //                aaprover1 = empMultlevel + dr6["email"].ToString();

        //            }

        //            //-------------- murugan

        //            string sql = "";

        //             SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
        //             if (dr9.Read())
        //             {
        //                 if (dr9[0].ToString() == "Super Admin")
        //                 {

        //                      sql = " SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //                     sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>=" + appLevel;

        //                 }
        //                 else {

        //                      sql = " SELECT distinct email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
        //                     sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>" + appLevel;


        //                 }

        //             }
        //            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null); dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
        //            string email;
        //            StringBuilder strUpdateBuild = new StringBuilder();
        //            while (dr.Read())
        //            {
        //                email = dr[0].ToString() + ";";
        //                strUpdateBuild.Append(email);
        //            }

        //            email = strUpdateBuild.ToString();


        //            aaprover1 = aaprover1+";"+ email;

        //            //------------
        //            int i=appLevel;
        //        }


        //        while (dr5.Read())
        //        { 
        //            from = Utility.ToString(dr5.GetValue(18));
        //            to = aaprover1;
        //            SMTPserver = Utility.ToString(dr5.GetValue(5));
        //            SMTPUser = Utility.ToString(dr5.GetValue(6));
        //            SMTPPass = Utility.ToString(dr5.GetValue(7));
        //            approver = Utility.ToString(dr5.GetValue(2));
        //            applicant_name = Utility.ToString(dr5.GetValue(21));
        //            status = "Leave Approved For " + applicant_name + " by " + wFlowName + "-L" + Utility.ToString(dr5.GetValue(3)) + " Approver (" + varEmpName.Replace("'", "") + ")"; //Utility.ToString(dr5.GetValue(3)); Comment by Sandi
        //            applicationdate = Utility.ToString(dr5.GetValue(4));
        //            from_date = Utility.ToString(dr5.GetValue(0));
        //            to_date = Utility.ToString(dr5.GetValue(1));
        //            reason = Utility.ToString(dr5.GetValue(16)) + " Please Approve Leave";
        //            body = Utility.ToString(dr5.GetValue(10));
        //            SMTPPORT = Utility.ToInteger(dr5.GetValue(12));
        //            emailreq = Utility.ToString(dr5.GetValue(17)).ToLower();
        //            cc = Utility.ToString(dr5.GetValue(19));
        //            leave_type = Utility.ToString(dr5.GetValue(20));
        //            applicant_name = Utility.ToString(dr5.GetValue(21));
        //        }
        //    }
        //    if (to.Length == 0 || from.Length == 0)
        //    {

        //        //ShowMessageBox("Please check email address is not configured yet");
        //        _actionMessage = "Warning|Please check email address is not configured yet";
        //        ViewState["actionMessage"] = _actionMessage;
        //        return;
        //    }

        //    //--superadmin login

        //    //SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
        //    //if (dr9.Read())
        //    //{
        //    //    if (dr9[0].ToString() == "Super Admin")
        //    //    {
        //    //        string sql = "select email from employee where emp_code=(select emp_supervisor from employee where emp_name='" + emp_name + "')";
        //    //        SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
        //    //        if (dr11.Read())
        //    //        {
        //    //            to = to +  dr11[0].ToString();
        //    //        }

        //    //    }


        //    //}


        //    //
        //    SMEPayroll.Model.ANBMailer oANBMailer;

        //    if (emailreq == "yes")
        //    {
        //       // string subject = "Leave Requested On " + " " + applicationdate;
        //        string subject = "Leave Requested By " + " " + emp_name;

        //        //muru
        //        string multi_approver = "select  emp_name from employee where emp_code = (select Emp_id from EmployeeAssignedToPayrollGroup where PayrollGroupID = (select PayrollGroupID from EmployeeWorkFlowLevel where id =" + approver + "))";
        //        SqlDataReader appreader = DataAccess.ExecuteReader(CommandType.Text, multi_approver, null);
        //        if (appreader.Read())
        //            approver = appreader[0].ToString();




        //        if (empMultlevel.Length > 0)
        //        {
        //            body = body.Replace("@emp_name", emp_name);
        //            body = body.Replace("@approver", approver);
        //            body = body.Replace("@status", status);
        //            body = body.Replace("@from_date", from_date);
        //            body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type + ")");
        //            body = body.Replace("@reason", txtremarks.Text);

        //            compid = Utility.ToInteger(Session["Compid"]);
        //            oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

        //            oANBMailer.Subject = subject;
        //            oANBMailer.MailBody = body;
        //            oANBMailer.From = from;
        //            oANBMailer.To = to;
        //            oANBMailer.Cc = cc;
        //        }
        //        else
        //        {
        //            body = body.Replace("@emp_name", emp_name);
        //            body = body.Replace("@approver", approver);
        //            body = body.Replace("@status", status);
        //            body = body.Replace("@from_date", from_date);
        //            body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type+")");
        //            body = body.Replace("@reason", txtremarks.Text);

        //            compid = Utility.ToInteger(Session["Compid"]);
        //            oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

        //            oANBMailer.Subject = subject;
        //            oANBMailer.MailBody = body;
        //            oANBMailer.From = from;
        //            oANBMailer.To = to;
        //            oANBMailer.Cc = cc;
        //        }




        //        try
        //        { 
        //            string sRetVal = oANBMailer.SendMail("Leave", emp_name, from_date, to_date, "Leave " + status + "");

        //            if (sRetVal == "SUCCESS")
        //            {
        //                //--murugan
        //                if (to.Length > 0)
        //                {
        //                    if (cc.Length > 0)
        //                    {
        //                        strMessage = strMessage + "<br/>" + "An email has been sent to " + to + "And cc to : " + cc;
        //                    }
        //                    else
        //                    {
        //                        strMessage = strMessage + "<br/>" + "An email has been sent to " + to;
        //                    }
        //                }
        //                strPassMailMsg.Append(strMessage);
        //                //----end


        //                //if (to.Length > 0)
        //                //{    
        //                //    strPassMailMsg.Append("<br/>" + to);
        //                //}
        //            }
        //            else
        //            {
        //                strFailMailMsg.Append("<br/>" + to);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            strFailMailMsg.Append("<br/>" + emp_name);
        //        }
        //    }
        //}
        protected void sendemail(int id, string empcode, string wFlowNamestring, string empMultlevel, string emp_name)
        {
            string applicationdate = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string status = "";
            string approver = "";
            string from_date = "";
            string to_date = "";
            string reason = "";
            string emailreq = "";
            int SMTPPORT = 25;
            string body = "";
            string cc = "";
            string leave_type = "";
            string applicant_name = "";

            string login_user = "";
            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr19.Read())
            {
                login_user = dr19[0].ToString();
            }

            //string leave_supervisor_one_to_one = ""; 
            if (empMultlevel.Length == 1 && empMultlevel != "0")
            {
                appLevel = int.Parse(empMultlevel);

            }



            string sSQLemail = "sp_send_email_status";
            SqlParameter[] parmsemail = new SqlParameter[1];
            parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(18));
                to = Utility.ToString(dr3.GetValue(15));
                SMTPserver = Utility.ToString(dr3.GetValue(5));
                SMTPUser = Utility.ToString(dr3.GetValue(6));
                SMTPPass = Utility.ToString(dr3.GetValue(7));
                approver = Utility.ToString(dr3.GetValue(2));
                status = Utility.ToString(dr3.GetValue(3)); //Utility.ToString(dr3.GetValue(3)); Comment by Sandi
                applicationdate = Utility.ToString(dr3.GetValue(4));
                from_date = Utility.ToString(dr3.GetValue(0));
                to_date = Utility.ToString(dr3.GetValue(1));
                reason = Utility.ToString(dr3.GetValue(16));
                body = Utility.ToString(dr3.GetValue(10));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(12));
                emailreq = Utility.ToString(dr3.GetValue(17)).ToLower();
                cc = Utility.ToString(dr3.GetValue(19));
                leave_type = Utility.ToString(dr3.GetValue(20));
                applicant_name = Utility.ToString(dr3.GetValue(21));
                //leave_supervisor_one_to_one = Utility.ToString(dr3.GetValue(22));
            }

            if (to.Length == 0 || from.Length == 0)
            {

                // ShowMessageBox("Please check email address is not configured yet");
                strMailonotconfigured.Append( "Please check email address is not configured yet");
                return;
            }
            else
            {
                to += ";";
            }

            if (empMultlevel == "0")
            {
                //kumar Added
                SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + loginEmpCode, null);
                if (dr9.Read())
                {
                    if (dr9[0].ToString() == "Super Admin")
                    {
                        SqlDataReader dr10 = DataAccess.ExecuteReader(CommandType.Text, "select email from employee where emp_code=(select emp_supervisor from employee where emp_name='" + emp_name + "')", null);
                        if (dr10.Read())
                        {

                            to += dr10[0].ToString()+ ";";

                        }

                        //if (leave_supervisor_one_to_one.Length >0)
                        //{
                        //    to = to + ";" + leave_supervisor_one_to_one;
                        //}

                    }


                }

            }
            else
            {

                string aaprover1 = "";
                string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + empcode + "'";
                SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr6.Read())
                {
                    aaprover1 = empMultlevel + dr6["email"].ToString();

                }

                //-------------- murugan

                string sql1 = "";

                if (appLevel == 1 && (login_user == "Super Admin"))
                {

                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]   and ( efl.rowid >= " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + Utility.ToInteger(Session["Compid"]).ToString() + " and efl.[WorkFlowID]=" + wFlowNamestring;

                }
                else if (appLevel == 1 && login_user != "Super Admin")
                {

                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid > " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + Utility.ToInteger(Session["Compid"]).ToString() + " and efl.[WorkFlowID]=" + wFlowNamestring;

                }
                else if (appLevel > 1 && login_user == "Super Admin")
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid = " + (appLevel - 1) + " or efl.rowid >= " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + Utility.ToInteger(Session["Compid"]).ToString() + " and efl.[WorkFlowID]=" + wFlowNamestring;

                }
                else if (appLevel > 1 && login_user != "Super Admin")
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid = " + (appLevel - 1) + " or efl.rowid > " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + Utility.ToInteger(Session["Compid"]).ToString() + " and efl.[WorkFlowID]=" + wFlowNamestring;

                }

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



                string email;
                StringBuilder strUpdateBuild = new StringBuilder();
                while (dr1.Read())
                {
                    if (strUpdateBuild.Length > 0)
                    {
                        strUpdateBuild.Append(";");
                    }
                    email = dr1[0].ToString();
                    strUpdateBuild.Append(email);
                }

                email = strUpdateBuild.ToString();

                sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_code='" + empcode + "'";
                SqlDataReader dr66 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr66.Read())
                {

                    aaprover1 = dr66["email"].ToString();
                }

                aaprover1 = aaprover1 + ";" + email;
                to = aaprover1;
            }

            SMEPayroll.Model.ANBMailer oANBMailer;

            if (emailreq == "yes")
            {
                // string subject = "Leave Requested On " + " " + applicationdate;
                string subject = "Leave Requested By " + " " + emp_name;
                string app_name = "";
                SqlDataReader dr10 = DataAccess.ExecuteReader(CommandType.Text, "select emp_name from employee where emp_code=" + loginEmpCode, null);
                if (dr10.Read())
                {
                    app_name = dr10[0].ToString();

                }
                string status2 = "";
                if (status == "Approved")
                {
                    status2 = " Approved";
                }
                else
                {
                    status2 = "Level " + status + " Approved";
                }


                if (empMultlevel.Length > 0)
                {
                    body = body.Replace("@emp_name", emp_name);
                    //body = body.Replace("@approver", approver); 
                    body = body.Replace("@approver", app_name);
                    body = body.Replace("@status", status2);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type + ")");
                    body = body.Replace("@reason", txtremarks.Text);

                    compid = Utility.ToInteger(Session["Compid"]);
                    oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;
                }
                else
                {
                    body = body.Replace("@emp_name", emp_name);
                    body = body.Replace("@approver", approver);
                    body = body.Replace("@status", status);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type + ")");
                    body = body.Replace("@reason", txtremarks.Text);

                    compid = Utility.ToInteger(Session["Compid"]);
                    oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;
                }




                try
                {
                    // string sRetVal = oANBMailer.SendMail("Leave", emp_name, from_date, to_date, "Leave " + status + "");
                    string sRetVal = oANBMailer.SendMail("Leave", "", from_date, to_date, "Leave " + status + "");
                    if (sRetVal == "SUCCESS")
                    {
                        //--murugan
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + to + "<br/> And CC to : <br/>" + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>"  + to;
                            }
                        }
                        strPassMailMsg.Append(strMessage);

                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + to);
                    }
                }
                catch (Exception ex)
                {
                    strFailMailMsg.Append("<br/>" + "");
                }
            }
        }
        protected void Rejectsendemail(int id, string empcode, string wFlowNamestring, string empMultlevel, string emp_name)
        {
            string applicationdate = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string status = "";
            string approver = "";
            string from_date = "";
            string to_date = "";
            string reason = "";
            string emailreq = "";
            int SMTPPORT = 25;
            string body = "";
            string cc = "";
            string leave_type = "";
            string applicant_name = "";

            string login_user = "";
            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr19.Read())
            {
                login_user = dr19[0].ToString();
            }

            //string leave_supervisor_one_to_one = ""; 
            int mlvl = 0;
            if (int.TryParse(empMultlevel, out mlvl))
            {
                // appLevel = int.Parse(empMultlevel);
                appLevel = mlvl;
                //empMultlevel = "";
            }


            string sSQLemail = "sp_send_email_status";
            SqlParameter[] parmsemail = new SqlParameter[1];
            parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(18));
                to = Utility.ToString(dr3.GetValue(15));
                SMTPserver = Utility.ToString(dr3.GetValue(5));
                SMTPUser = Utility.ToString(dr3.GetValue(6));
                SMTPPass = Utility.ToString(dr3.GetValue(7));
                approver = Utility.ToString(dr3.GetValue(2));
                status = Utility.ToString(dr3.GetValue(3)); //Utility.ToString(dr3.GetValue(3)); Comment by Sandi
                applicationdate = Utility.ToString(dr3.GetValue(4));
                from_date = Utility.ToString(dr3.GetValue(0));
                to_date = Utility.ToString(dr3.GetValue(1));
                reason = Utility.ToString(dr3.GetValue(16));
                body = Utility.ToString(dr3.GetValue(10));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(12));
                emailreq = Utility.ToString(dr3.GetValue(17)).ToLower();
                cc = Utility.ToString(dr3.GetValue(19));
                leave_type = Utility.ToString(dr3.GetValue(20));
                applicant_name = Utility.ToString(dr3.GetValue(21));
                //leave_supervisor_one_to_one = Utility.ToString(dr3.GetValue(22));
            }

            if (to.Length == 0 || from.Length == 0)
            {

                strMailonotconfigured.Append( "Please check email address is not configured yet");
               // ShowMessageBox("Please check email address is not configured yet");
                return;
            }

            string aaprover1 = "";
            // if (empMultlevel.Length <= 0)
            if (wFlowNamestring == "0")
            {
                string sqlEmail1 = "SELECT email From employee  Where emp_code='" + empcode + "'";
                SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr6.Read())
                {
                    aaprover1 = dr6["email"].ToString();
                }
                if (aaprover1 != "")
                    aaprover1 += ";";
                //kumar Added
                SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + loginEmpCode, null);
                if (dr9.Read())
                {
                    if (dr9[0].ToString() == "Super Admin")
                    {
                        SqlDataReader dr10 = DataAccess.ExecuteReader(CommandType.Text, "select email from employee where emp_code=(select emp_supervisor from employee where emp_name='" + emp_name + "')", null);
                        if (dr10.Read())
                        {

                            // aaprover1 = aaprover1 + ";" + dr10[0].ToString();
                             aaprover1 += dr10[0].ToString() + ";";

                        }



                    }



                }

            }
            else
            {




                //-------------- murugan

                string sql1 = "";

                if (appLevel == 1 && (login_user == "Super Admin"))
                {

                    //sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    //sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID] and ea.WorkflowTypeID=2  and ( efl.rowid >= " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wFlowNamestring;
                    sql1 = "select email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID]";
                    sql1 = sql1 + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=2 and  rowid>=" + appLevel + "  and WorkFlowID = (SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[Leave_supervisor] FROM EMPLOYEE WHERE emp_code=" + empcode + "))))";

                }
                else if (appLevel == 1 && login_user != "Super Admin")
                {

                    //sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    //sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID] and ea.WorkflowTypeID=2 and ( efl.rowid > " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wFlowNamestring;
                    sql1 = "select email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID]";
                    sql1 = sql1 + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=2 and  rowid>" + appLevel + "  and WorkFlowID = (SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[Leave_supervisor] FROM EMPLOYEE WHERE emp_code=" + empcode + "))))";

                }
                else if (appLevel > 1 && login_user == "Super Admin")
                {


                    //sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    //sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID] and ea.WorkflowTypeID=2 and ( efl.rowid = " + (appLevel - 1) + " or efl.rowid >= " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wFlowNamestring;
                    sql1 = "select email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID]";
                    sql1 = sql1 + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=2 and  rowid>=" + appLevel + "  and WorkFlowID = (SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[Leave_supervisor] FROM EMPLOYEE WHERE emp_code=" + empcode + "))))";

                }
                else if (appLevel > 1 && login_user != "Super Admin")
                {


                    //  sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    // sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID] and ea.WorkflowTypeID=2 and ( efl.rowid = " + (appLevel - 1) + " or efl.rowid > " + appLevel + " ) and efl.FlowType=2 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wFlowNamestring;
                    sql1 = "select email from employee where emp_code in ( select emp_ID from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID]";
                    sql1 = sql1 + " in (select payRollGroupID from [EmployeeWorkFlowLevel] where [FlowType]=2 and  rowid > " + appLevel + " and WorkFlowID = (SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[Leave_supervisor] FROM EMPLOYEE WHERE emp_code=" + empcode + "))))";

                }

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



                string email;
                StringBuilder strUpdateBuild = new StringBuilder();
                while (dr1.Read())
                {
                    if (strUpdateBuild.Length > 0)
                    {
                        strUpdateBuild.Append(";");
                    }
                    email = dr1[0].ToString();
                    strUpdateBuild.Append(email);
                }

                email = strUpdateBuild.ToString();

                string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_code='" + empcode + "'";
                SqlDataReader dr66 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr66.Read())
                {

                    aaprover1 = dr66["email"].ToString();
                }
                if (aaprover1 != "")
                    aaprover1 += ";";
                // aaprover1 = aaprover1 + ";" + email;
                aaprover1 += email + ";";


            }

            SMEPayroll.Model.ANBMailer oANBMailer;
            to = aaprover1;
            if (emailreq == "yes")
            {
                // string subject = "Leave Requested On " + " " + applicationdate;
                string subject = "Leave Requested By " + " " + emp_name;


                //if (empMultlevel.Length > 0)
                if (wFlowNamestring != "0")
                {
                    body = body.Replace("@emp_name", emp_name);
                    body = body.Replace("@approver", approver);
                    body = body.Replace("@status", status);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type + ")");
                    body = body.Replace("@reason", txtremarks.Text);

                    compid = Utility.ToInteger(Session["Compid"]);
                    oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;

                    oANBMailer.Cc = cc;
                }
                else
                {
                    body = body.Replace("@emp_name", emp_name);
                    body = body.Replace("@approver", approver);
                    body = body.Replace("@status", status);
                    body = body.Replace("@from_date", from_date);
                    body = body.Replace("@to_date", to_date + " ( Leave Type : " + leave_type + ")");
                    body = body.Replace("@reason", txtremarks.Text);

                    compid = Utility.ToInteger(Session["Compid"]);
                    oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                    oANBMailer.Subject = subject;
                    oANBMailer.MailBody = body;
                    oANBMailer.From = from;
                    oANBMailer.To = to;
                    oANBMailer.Cc = cc;
                }




                try
                {
                    // string sRetVal = oANBMailer.SendMail("Leave", emp_name, from_date, to_date, "Leave " + status + "");
                    string sRetVal = oANBMailer.SendMail("Leave", "", from_date, to_date, "Leave " + status + "");
                    if (sRetVal == "SUCCESS")
                    {
                        //--murugan
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>"  + to + "</br/> And CC to : <br/>" + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>"  + to;
                            }
                        }
                        strPassMailMsg.Append(strMessage);

                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + to);
                    }
                }
                catch (Exception ex)
                {
                    strFailMailMsg.Append("<br/>" + "");
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
        protected void RadCalendar1_DayRender(object sender, Telerik.Web.UI.Calendar.DayRenderEventArgs e)
        {
           
        }
        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
        protected void radApprovedLeave_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("2", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
        protected void DetailRadToolBar_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            //determine which button was clicked

            if (e.Item.Text == "Add")
            {
                radApprovedLeave.MasterTableView.InsertItem();
            }
            else if (e.Item.Text == "Excel")
            {
                ConfigureExport6();
                radApprovedLeave.MasterTableView.ExportToExcel();
            }
            else if (e.Item.Text == "Word")
            {
                ConfigureExport6();
                radApprovedLeave.MasterTableView.ExportToWord();
            }
            else if (e.Item.Text == "PDF")
            {
                ConfigureExport6();
                // RadGrid6.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((RadGrid6.Items[0].Cells.Count * 24)) + "mm");
                radApprovedLeave.ExportSettings.OpenInNewWindow = true;
                radApprovedLeave.MasterTableView.ExportToPdf();
            }


        }
        public void ConfigureExport6()
        {
            //To ignore Paging,Exporting only data,
            radApprovedLeave.ExportSettings.ExportOnlyData = true;
            radApprovedLeave.ExportSettings.IgnorePaging = true;
            radApprovedLeave.ExportSettings.OpenInNewWindow = true;

            //To hide filter texbox
            radApprovedLeave.MasterTableView.AllowFilteringByColumn = true;


            //To hide the add new button
            radApprovedLeave.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;


        }

        protected void radApprovedLeave_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            ApprovedLeaves();
            radApprovedLeave.DataBind();
        }
    }
}


