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
using System.IO;
using System.Text;
using AuditLibrary;//Added by Jammu Office
using efdata;
using System.Linq;

namespace SMEPayroll.Payroll
{
    public partial class ClaimApproval : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string s = "";
        string varEmpName = "";
        string _actionMessage = "";
        int LoginEmpcode = 0;//Added by Jammu Office


        DataSet dsleaves;
        string email;
        public string WorkFlowName;
        int appLevel;
        int lcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            compid = Utility.ToInteger(Session["Compid"]);
            LoginEmpcode = Convert.ToInt32(Session["EmpCode"]);//Added by Jammu Office
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varEmpName = Session["Emp_Name"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "PENDING APPROVAL CLAIM FOR ALL")) == true)
                    TextBox1.Text = "approverAll";
                else if ((Utility.AllowedAction1(Session["Username"].ToString(), "PENDING APPROVAL FOR CLAIM")) == false)
                {
                    TextBox1.Text = "noapprover";
                }
                else
                {
                    TextBox1.Text = "approver";
                }
                s = Session["Username"].ToString();
                string strSql = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + s + "'";
                DataSet leaveset = new DataSet();

                if ((string)Session["EmpCode"] != "0")//if user login
                {
                    leaveset = getDataSet(strSql);
                    lblsuper.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_name"]);
                }
                else
                {
                    lblsuper.Text = "-";
                }




            }
        }

        private object _dataItem = null;

        protected void ToggleSelectedState(object sender, EventArgs e)
        {
            CheckBox headerCheckBox = (sender as CheckBox);
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                (dataItem.FindControl("CheckBox1") as CheckBox).Checked = headerCheckBox.Checked;
                dataItem.Selected = headerCheckBox.Checked;
            }
        }
        
        protected void remarkRadio_CheckedChanged(object sender, EventArgs e)
        {
         //   ApprovedLeaves();

            string remarks = txtremarks.Text;
            string ApprovalFlag = "";
            string empcode = "";
            int trxid = -1;
            ButtonApprove.Enabled = true;
            ButtonReject.Enabled = true;
            int k = 0;
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
                   // RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");



                    if (chkBox.Checked == true)
                    {

                        k = k + 1;

                        if (k > 1)
                        {
                            lblApprovalinfo1.Text = "";

                            return;
                        }


                        trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                        string strRemarks = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("remarks"));
                        ApprovalFlag = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].Cells[16].Text);
                        empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        //divRemarks.InnerText = strRemarks;
                        txtEmpRemarks.Text = strRemarks;
                    }
                }
            }
            //If Selected Row having Approver as Multilevel then Show Details till what level approval is done
            //    if (ApprovalFlag == "MultiLevel")
            //    {
            rowApp.Visible = true;

            string strSql = "Select WL.ID,WL.RowID, WorkFlowName ";

            strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
            strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=3) ";
            strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
            strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT CliamsupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
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
                }
            }

            if (maxApprovalleval != 0)
            {
                lblApprovalinfo1.Text = "";

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT [claimstatus] FROM emp_additions WHERE trx_id=" + trxid, null);
                int ApproveLvlcnt = 0;
                message = "Multi Level Approval Status :";
                while (dr1.Read())
                {
                    if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open" || Utility.ToString(dr1.GetValue(0)) == "Pending" || Utility.ToString(dr1.GetValue(0)) == "U") //Added last condition by Sandi
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
                    ButtonApprove.Enabled = false;
                    ButtonReject.Enabled = false;

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
            //------------------

            string sSQL1 = "sp_GetPayrollProcessOn";
            SqlParameter[] parms1 = new SqlParameter[3];
            parms1[0] = new SqlParameter("@empcode", Utility.ToInteger( empcode));
            parms1[1] = new SqlParameter("@compid", compid);
            DateTime dnow = DateTime.Now;

            parms1[2] = new SqlParameter("@trxdate", dnow.ToString("dd/MMM/yyyy"));
            int conLock = 0;
            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
            while (dr11.Read())
            {
                conLock = Utility.ToInteger(dr11.GetValue(0));
            }
            if (conLock > 0)
            {
                ButtonApprove.Enabled = false;
                ButtonReject.Enabled = false;

               // HttpContext.Current.Response.Write("<Script type='text/javascript'>alert('Payroll has been locked for Current Month. \\n Please take action in Next Month')</Script>");
                _actionMessage = "Warning|Payroll has been locked for Current Month. \\n Please take action in Next Month";
                ViewState["actionMessage"] = _actionMessage;


            }
            else {
                ButtonApprove.Enabled = true;
                ButtonReject.Enabled = true;
            }
            //-----------------
        }
        //protected void remarkRadio_CheckedChanged(object sender, EventArgs e)
        //{
        //    string remarks = txtremarks.Text;

        //    foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (item is GridItem)
        //        {
        //            GridDataItem dataItem = (GridDataItem)item;
        //            CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
        //            RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
        //            if (rad1.Checked == true)
        //            {
        //                int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
        //                string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
        //                string strRemarks = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("remarks"));

        //                txtEmpRemarks.Value = strRemarks;

        //            }
        //        }
        //    }

        //}
        public object Dataitem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
        }

        protected static DataSet getDataSet(string sSQL)
        {

            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {

            RadGrid1.DataBind();
        }

        

        string trx_period;
        protected void sendemail(int id, string emp_name, string empMultlevel, string wFlowName)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string approver = varEmpName;
           // string emp_name = "";
            string emailreq = "";
            string body = "";
            string month = "";
            string year = "";
            string status = "";
            string cc = "";
            string emp_names = "";
            string applieddate = "";
            string approveddate = "";
            string amount = "";
            string aaprover1 = "";

            string claimtype = "";
            string createddate = "";
            string trxamount = "";
            string remarks = "";
            string login_user = "";
            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr19.Read())
            {
                login_user = dr19[0].ToString();
            }

            string sql9 = "select datename(month,ea.trx_period) ,year(ea.trx_period),ea.trx_period, Convert(varchar(50),ea.created_on,103),ea.trx_amount,ea.remarks,at.[desc] from emp_additions ea inner join additions_types at on ea.trx_type=at.id where ea.trx_id='" + Utility.ToInteger(id) + "'";
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

            }
            string[] claimremarks = remarks.Split(':');


            if (empMultlevel.Length == 1)
            {
                appLevel = int.Parse(empMultlevel);
                //empMultlevel = "";
            }
            if (empMultlevel.Length <= 0)
            {




                string sSQLemail = "sp_email_claim";
                SqlParameter[] parmsemail = new SqlParameter[1];
                parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
                while (dr3.Read())
                {
                    to = Utility.ToString(dr3.GetValue(1));
                    SMTPserver = Utility.ToString(dr3.GetValue(2));
                    SMTPUser = Utility.ToString(dr3.GetValue(3));
                    SMTPPass = Utility.ToString(dr3.GetValue(4));
                    status = Utility.ToString(dr3.GetValue(5));
                    emailreq = Utility.ToString(dr3.GetValue(11)).ToLower();
                    from = Utility.ToString(dr3.GetValue(12));
                    month = Utility.ToString(dr3.GetValue(13));
                    year = Utility.ToString(dr3.GetValue(14));
                    body = Utility.ToString(dr3.GetValue(8));
                    cc = Utility.ToString(dr3.GetValue(15));  //Senthil-Added on 22/09/2015
                    emp_names = Utility.ToString(dr3.GetValue(16));
                    applieddate = Utility.ToString(dr3.GetValue(17));
                    approveddate = Utility.ToString(dr3.GetValue(19));
                    amount = Utility.ToString(dr3.GetValue(18));
                }
                if (appLevel == 1)
                {
                    string sql = "";
                    if (login_user == "Super Admin")
                    {

                        sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeAssignedToPayrollGroup].WorkflowTypeID=3 and [EmployeeWorkFlowLevel].rowid>=" + appLevel;
                        sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";

                    }
                    else {
                        sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeAssignedToPayrollGroup].WorkflowTypeID=3 and [EmployeeWorkFlowLevel].rowid>" + appLevel;
                        sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [ID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";

                    }
                    
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    string email;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (dr.Read())
                    {
                        email = dr[0].ToString() + ";";
                        strUpdateBuild.Append(email);
                    }

                    email = strUpdateBuild.ToString();
                  //  to = to + ";" + email;
                  if(to != "")
                   to = to + ";" + email;
                  else
                   to = email;


                }
                else {


                    
                        if (login_user  == "Super Admin")
                        {
                            string sql = "select email from employee where emp_code=(select emp_clsupervisor from employee where emp_name='" + emp_name + "')";
                            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                            if (dr11.Read())
                            {
                                to += dr11[0].ToString()+"; ";
                            }

                        }

                     }

            }
            else
            {
                string sSQLemail = "sp_email_claim";
                SqlParameter[] parmsemail = new SqlParameter[1];
                parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

                SqlDataReader dr5 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);




                if (empMultlevel.Length <= 0)
                {

                    string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
                    SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);

                    while (dr6.Read())
                    {
                        aaprover1 = Utility.ToString(dr6.GetValue(0));
                    }

                    //string sql = "SELECT email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
                   // sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>=" + appLevel;
                    string sql = "";
                    if (login_user == "Super Admin")
                    {
                         sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>=" + appLevel;
                        sql=sql+" and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE ID=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='"+ emp_name+"'))";
                    }
                    else {
                        sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>" + appLevel;
                        sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE ID=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";
                    }
                    

                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    string email;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (dr.Read())
                    {
                        email = dr[0].ToString() + ";";
                        strUpdateBuild.Append(email);
                    }

                    email = strUpdateBuild.ToString();

                    email = strUpdateBuild.ToString();
                    aaprover1 = aaprover1 + ";" + email;
                    //------------------------------
                }
                else
                {

                    string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
                    SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                    if (dr6.Read())
                    {
                       //aaprover1 = empMultlevel + dr6["email"].ToString();
                        aaprover1 = dr6["email"].ToString() + ";";
                    }

                    //-------------- murugan for 


                    //string sql = "SELECT email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
                    //sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]>=" + appLevel;
                    //string sql = "SELECT email from [employee],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlowLevel] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id and ";
                   // sql = sql + "[EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID] and [EmployeeWorkFlowLevel].[RowID]>" + appLevel;
                    string sql = "";
                    if (login_user == "Super Admin")
                    {
                        //sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        //sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>=" + appLevel;
                        //sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [PayRollGroupID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";
                        sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>=" + appLevel;
                        sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE ID=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";
                    }
                    else {

                        //sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        //sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>" + appLevel;
                        //sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE [PayRollGroupID]=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";
                        sql = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql = sql + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>" + appLevel;
                        sql = sql + " and [EmployeeWorkFlowLevel].WorkFlowID=(SELECT [WorkFlowID] FROM [EmployeeWorkFlowLevel] WHERE ID=(SELECT EMPLOYEE.[CliamsupervicerMulitilevel] FROM EMPLOYEE WHERE emp_name='" + emp_name + "'))";
                    }
                   
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    string email;
                    StringBuilder strUpdateBuild = new StringBuilder();
                    while (dr.Read())
                    {
                        email = dr[0].ToString() + ";";
                        strUpdateBuild.Append(email);
                    }

                    email = strUpdateBuild.ToString();

                    if (empMultlevel.Length != 1)
                    {
                        aaprover1 = empMultlevel + aaprover1 + email;
                    }
                    else {
                        aaprover1 =  aaprover1 +  email;
                    }
                    

                    //------------
                    int i = appLevel;
                }


                while (dr5.Read())
                {
                 //   to = Utility.ToString(dr5.GetValue(1));
                    to = aaprover1;
                    SMTPserver = Utility.ToString(dr5.GetValue(2));
                    SMTPUser = Utility.ToString(dr5.GetValue(3));
                    SMTPPass = Utility.ToString(dr5.GetValue(4));
                    status = Utility.ToString(dr5.GetValue(5));
                    emailreq = Utility.ToString(dr5.GetValue(11)).ToLower();
                    from = Utility.ToString(dr5.GetValue(12));
                    month = Utility.ToString(dr5.GetValue(13));
                    year = Utility.ToString(dr5.GetValue(14));
                    body = Utility.ToString(dr5.GetValue(8));
                    cc = Utility.ToString(dr5.GetValue(15));  //Senthil-Added on 22/09/2015
                    emp_names = Utility.ToString(dr5.GetValue(16));
                    applieddate = Utility.ToString(dr5.GetValue(17));
                    approveddate = Utility.ToString(dr5.GetValue(19));
                    amount = Utility.ToString(dr5.GetValue(18));
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
            if (emailreq == "yes")
            {
                string subject = "Claim status for the month of   " + month + "/" + year;


                body = body.Replace("@emp_name", emp_name);
                if (status.Length == 1)
                {
                    body = body.Replace("@status", "Level : "+status);
                }
                else {
                    body = body.Replace("@status", status);
                }
                
                body = body.Replace("@claimtype", claimtype);
                body = body.Replace("@approver", approver);
                body = body.Replace("@month", month);
                body = body.Replace("@year", year);
                body = body.Replace("@emp_name", emp_names);
                body = body.Replace("@applied_date", applieddate);
                body = body.Replace("@approved_date", approveddate);
                body = body.Replace("@amount", amount);
             //   body = body.Replace("@remarks", claimremarks[1].ToString());

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                try
                {
                   // string sRetVal = oANBMailer.SendMail(); murugan
                   // if (sRetVal == "")
                   // {
                        //   Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                        // else
                        //   Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");
                   // }
                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;
                }


                string sql91 = "select datename(month,trx_period),year(trx_period),trx_period from emp_additions where trx_id='" + Utility.ToInteger(id) + "'";
                SqlDataReader dr91 = DataAccess.ExecuteReader(CommandType.Text, sql91, null);
                while (dr9.Read())
                {
                    trx_period = Utility.ToString(dr9.GetValue(2));
                }


                try
                {
                  //  string sRetVal = oANBMailer.SendMail();
                    string sRetVal = oANBMailer.SendMail("Claim", emp_name, trx_period, trx_period, "Claims " + status + "");

                    if (sRetVal == "SUCCESS")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strPassMailMsg.Append("<br/>" + to + " <br/>And CC to : " + cc);
                            }
                            else
                            {
                                strPassMailMsg.Append("<br/>" + to);
                            }
                        }
                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + emp_name);
                    }
                }
                catch (Exception ex)
                {
                    strFailMailMsg.Append("<br/>" + emp_name);
                }
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ApproveCliamClick();
        }

        
        private void ApproveCliamClick()
        {
            string remarks = txtEmpRemarks.Text + " - " + txtremarks.Text;
            string workFlowName = "";
            string workflowId = "";
            int isApproveDate = 0;

            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();

         string   SQLQueryA = "select isApproveDate from company where company_id=" + compid;
            SqlDataReader drA = DataAccess.ExecuteReader(CommandType.Text, SQLQueryA, null);
            if (drA.Read())
            {
                if (Utility.ToInteger(drA[0].ToString()) > 0)
                {
                    isApproveDate = 1;

                   
                }
                else
                {
                    isApproveDate = 0;
                 
                }
            }




            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");

                    if (chkBox.Checked == true)
                    {
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                        string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
                        string empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        string sSQLCheck = "select claimstatus from emp_additions where trx_id = {0}";
                        sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                        string status = "";
                        double paid_leaves = 0;
                        double unpaid_leaves = 0;
                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                        while (dr.Read())
                        {
                            status = Utility.ToString(dr.GetValue(0));
                          
                        }
                        appLevel = 0;
                        //if ((status == "U") || (status == "L" && unpaid_leaves <= 0))
                        //{
                            bool flagUpdatefinal = true;
                            int appLeveldb = 0;

                            string strSql = "Select WF.ID,WL.RowID,WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName,WorkFlowName WName ";
                            strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                            strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=3) ";
                            strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                            strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT CliamsupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
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
                                    SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT claimstatus FROM emp_additions Where trx_id=" + trxid, null);
                                    while (dr1.Read())
                                    {
                                        if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open" || Utility.ToString(dr1.GetValue(0)) == "Pending"|| Utility.ToString(dr1.GetValue(0)) == "U") //Added last condition by Sandi
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
                                string sql="";
                            if (isApproveDate == 0)
                            {
                                //sql = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "' where trx_id=" + trxid;
                                sql = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "' where trx_id=" + trxid;
                            }
                            else
                            {
                                sql = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "',[trx_period] =' " + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MMM") + "/" + DateTime.Now.ToString("yyyy") + "' where trx_id=" + trxid;
                            }
                            //Added by Jammu Office
                            #region Audit
                            var oldrecord = new AuditLibrary.EmpAddition();
                            using (var _context = new AuditContext())
                            {
                                oldrecord = _context.EmpAdditions.Where(i => i.TrxId == trxid).SingleOrDefault();
                            }
                            DateTime dtTrxPeriod = DateTime.Now.Date;
                            var newrecord = new AuditLibrary.EmpAddition()
                            {
                                TrxId = oldrecord.TrxId,
                                TrxPeriod = dtTrxPeriod,                               
                                Claimstatus = "Approved",
                                Approver= varEmpName.Replace("'", ""),
                                Remarks= remarks,
                            };
                            var AuditRepository = new AuditRepository();
                            AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, oldrecord.TrxId, oldrecord, newrecord);

                            #endregion

                            try
                            {
                                    DataAccess.ExecuteStoreProc(sql);
                                    strSucSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                                    sendemail(trxid, emp_name, "", "");
                                }
                                catch (Exception ex)
                                {
                                    string ErrMsg = ex.Message;
                                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                    {
                                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                        strFailSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                                    }
                                    else
                                    {
                                        strFailMailMsg.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
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





                                string sql = "";
                                if (isApproveDate == 0)
                                {
                                   // sql = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "' where trx_id=" + trxid;
                                    sql = "Update emp_additions set claimstatus='" + appLevel.ToString() + "',approver='" + ApproverGroup.ToString() + "',remarks='" + remarks + "' where trx_id=" + trxid;
                                    
                                }
                                else
                                {
                                   // sql = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "',[trx_period] = '" + DateTime.Now.ToString() + "' where trx_id=" + trxid;
                                    sql = "Update emp_additions set claimstatus='" + appLevel.ToString() + "',approver='" + ApproverGroup.ToString() + "',remarks='" + remarks + "',[trx_period] = '" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MMM") + "/" + DateTime.Now.ToString("yyyy") + "' where trx_id=" + trxid;
                                }
                            //Added by Jammu Office
                            #region Audit
                            var oldrecord = new AuditLibrary.EmpAddition();
                            using (var _context = new AuditContext())
                            {
                                oldrecord = _context.EmpAdditions.Where(i => i.TrxId == trxid).SingleOrDefault();
                            }
                            DateTime dtTrxPeriod = DateTime.Now.Date;
                            var newrecord = new AuditLibrary.EmpAddition()
                            {
                                TrxId = oldrecord.TrxId,
                                TrxPeriod = dtTrxPeriod,
                                Claimstatus = "Approved",
                                Approver = appLevel.ToString(),
                                Remarks = remarks,
                            };
                            var AuditRepository = new AuditRepository();
                            AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, oldrecord.TrxId, oldrecord, newrecord);

                            #endregion
                            try
                            {

                                    DataAccess.ExecuteStoreProc(sql);
                                    strSucSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                                    //sendemail(trxid, emp_name, ""); //Comment by Sandi
                                    //Send an email to appLevel.ToString() -1  Approver 

                                    int arroverLevel = Convert.ToInt32(appLevel.ToString());
                                    if ((arroverLevel - 1) != 0)
                                    {

                                        //Get List Of All Employeees whose level is arroverLevel-1
                                        //string payrollGroup = "SELECT e.email,* FROM employee e WHERE e.emp_code IN (SELECT Ea.Emp_ID FROM PayrollGroup Pg INNER JOIN EmployeeAssignedToPayrollGroup Ea  ON Pg.ID=Ea.PayrollGroupID WHERE Pg.GroupName='L" + (arroverLevel - 1) + "')"; //Comment by Sandi                                       
                                        int level = appLevel - 1;


                                        string payrollGroup = "Select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where [WorkflowTypeID]=3 and payrollgroupId= (select PayRollGroupID from EmployeeWorkFlowLevel where RowID=" + level + " and WorkFlowID =" + workflowId + "))";
                                        SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, payrollGroup);
                                        StringBuilder strUpdateBuild = new StringBuilder();

                                        while (dr4.Read())
                                        {
                                            email = dr4[0].ToString() + ";";
                                            strUpdateBuild.Append(email);
                                        }
                                        sendemail(trxid, emp_name, email, workFlowName);

                                        if (appLevel == 1)
                                        {
                                            flagUpdatefinal = true;
                                        }
                                    }
                                    else
                                    {
                                        sendemail(trxid, emp_name, "", ""); //Added by Sandi
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string ErrMsg = ex.Message;
                                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                    {
                                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                        strFailSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                                    }
                                    else
                                    {
                                        strFailMailMsg.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                                    }
                                }
                            }
                        //}
                        //else
                        //{
                        //    Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                        //}
                    }
                }
            }

            if (strSucSubmit.Length > 0)
            {
                //ShowMessageBox("Claim Approved Successfully for: <br/>" + strSucSubmit.ToString());
                _actionMessage = "Fixed|Claim Approved Successfully for : "+ strSucSubmit.ToString();
               

                strMessage = "";
             //   rowApp.Visible = false;
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Claim Not Approved for: <br/>" + strFailSubmit.ToString());
                _actionMessage = "Fixed|Claim Not Approved for : "+ strFailSubmit.ToString();
               
                strFailMailMsg = new StringBuilder();
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                _actionMessage += "<br/>Email Send successfully to:" + strPassMailMsg.ToString();
               
                strPassMailMsg = new StringBuilder();
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += " <br/>Error While sending Email to:" + strFailMailMsg.ToString();
               
                strFailMailMsg = new StringBuilder();
                strMessage = "";
            }
            //ViewState["actionMessage"] = _actionMessage;
            ShowMessageBox(_actionMessage);
            RadGrid1.DataBind();
            txtremarks.Text = "";
        }

        


        protected void Button2_Click(object sender, EventArgs e)
        {
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            string remarks = txtEmpRemarks.Text + " - " + txtremarks.Text;

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                  //  RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    CheckBox rad1 = (CheckBox)dataItem.FindControl("CheckBox1");
                    if (rad1.Checked == true)
                    {
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string sSQLCheck = "select  status from emp_additions where trx_id = {0}";
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
                        string Sql = "Update emp_additions set claimstatus='Rejected',approver='" + varEmpName + "',remarks='" + remarks + "',[trx_period] =' " + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MMM") + "/" + DateTime.Now.ToString("yyyy") + "' where trx_id=" + trxid;
                        #region Audit
                        var oldrecord = new AuditLibrary.EmpAddition();
                        using (var _context = new AuditContext())
                        {
                            oldrecord = _context.EmpAdditions.Where(i => i.TrxId == trxid).SingleOrDefault();
                        }
                        DateTime dtTrxPeriod = DateTime.Now.Date;
                        var newrecord = new AuditLibrary.EmpAddition()
                        {
                            TrxId = oldrecord.TrxId,
                            TrxPeriod = dtTrxPeriod,
                            Claimstatus = "Rejected",
                            Approver = varEmpName,
                            Remarks = remarks,
                        };
                        var AuditRepository = new AuditRepository();
                        AuditRepository.CreateAuditTrail(AuditActionType.Update, LoginEmpcode, oldrecord.TrxId, oldrecord, newrecord);

                        #endregion

                        try
                        {
                            DataAccess.ExecuteStoreProc(Sql);
                            strSucSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");

                            string s = "";
                            if (Session["icount"] != null)
                            {
                                s = Session["icount"].ToString();
                            }
                            else {
                                s = "";
                            }
                                
                            sendemail(trxid, emp_name, s, "");
                        }
                        catch (Exception ex)
                        {
                            string ErrMsg = ex.Message;
                            if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                            {
                                //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                strFailSubmit.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
                            }
                            else
                            {
                                strFailMailMsg.Append("" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "");
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
                //ShowMessageBox("Claim Rejected Successfully for: <br/>" + strSucSubmit.ToString());
                _actionMessage = "Fixed|Claim Rejected Successfully for : "+ strSucSubmit.ToString();
              

                strMessage = "";
            }
            if (strFailSubmit.Length > 0)
            {
                //ShowMessageBox("Claim Not Rejected for: <br/>" + strFailSubmit.ToString());
                _actionMessage = "Fixed|Claim Not Rejected for : "+ strFailSubmit.ToString();
               
                strFailSubmit = new StringBuilder();
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                //ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                _actionMessage += "<br/>Email Send successfully to: " + strPassMailMsg.ToString();
               
                strPassMailMsg = new StringBuilder();
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                //ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                _actionMessage += "<br/>Error While sending Email to: " + strFailMailMsg.ToString();
              
                strFailMailMsg = new StringBuilder();
                strMessage = "";
            }
            ViewState["actionMessage"] = _actionMessage;
            RadGrid1.DataBind();
            txtremarks.Text = "";
        }







        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[4].Text).ToString();

                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";
                }
                else
                {
                    hl.Text = "No Document";
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

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            //GridSettingsPersister obj1 = new GridSettingsPersister();
            //obj1.ExportGridHeader("1", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }
    }
}





