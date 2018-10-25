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

namespace SMEPayroll.Payroll
{
    public partial class TimeApproval : System.Web.UI.Page
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


        DataSet dsleaves;
        string email;
        public string WorkFlowName;
        int appLevel;
        bool ismultilevel = false;
        int lcount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
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
        string empcode = "";
        string refid = "";
        int id = 0;
        int maxApprovalleval = 0;
        protected void remarkRadio_CheckedChanged(object sender, EventArgs e)
        {



            string ApprovalFlag = "";

            ButtonApprove.Enabled = true;
            ButtonReject.Enabled = true;

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    //CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        refid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RefId"));
                        id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                        empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMPID"));

                    }
                }
            }


            string strSql = "Select WL.ID,WL.RowID, WorkFlowName ";

            strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
            strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=4) ";
            strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
            strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT TimesupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
            strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

            dsleaves = new DataSet();
            dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);
            string message = "";
            string message1 = "";
            string message2 = "";
            string message3 = "";
            string message4 = "";


            int appLeveldb = 0;
            if (dsleaves.Tables.Count > 0)
            {

                if (dsleaves.Tables[0].Rows.Count > 0)
                {
                    maxApprovalleval = Utility.ToInteger(dsleaves.Tables[0].Rows[0][1]);
                    ismultilevel = true;
                    Session["ismultilevel"] = true;
                    Session["maxApprovalleval"] = maxApprovalleval;
                    //r
                    WorkFlowName = Utility.ToString(dsleaves.Tables[0].Rows[0][2]);
                }
                else
                {
                    ismultilevel = false;
                    
                }
            }
            else
            {
                ismultilevel = false;
                Session["ismultilevel"] = false;
            }

            if (maxApprovalleval != 0)
            {
                lblApprovalinfo1.Text = "";

                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT [Status] FROM TimeSheetMangment WHERE RefId='" + refid + "'", null);
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
                string strSql1 = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',timesupervisor from employee where UserName='" + s + "'";
                DataSet leaveset = new DataSet();
                leaveset = getDataSet(strSql1);
                string employee_code = "";
                if (leaveset.Tables.Count > 0)
                {
                    employee_code = leaveset.Tables[0].Rows[0][0].ToString();
                }
                string level;

                appLevel = ApproveLvlcnt;
                Session["appLeavel"] = appLevel.ToString();
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
        }

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


        protected void Button3_Click(object sender, EventArgs e)
        {
            ApproveClick();
        }




        private void ApproveClick()
        {
            string remarks = txtEmpRemarks.Text + " - " + txtremarks.Text;
            string workFlowName = "";
            string workflowId = "";
            string wgid = "";

            ismultilevel = (bool)Session["ismultilevel"];
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
                        refid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RefId"));
                        id = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                        empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMPID"));
                        string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));

                        string status = "";
                        double paid_leaves = 0;
                        double unpaid_leaves = 0;

                        appLevel = 0;

                        bool flagUpdatefinal = true;
                        int appLeveldb = 0;

                        string strSql = "Select WF.ID,WL.RowID,WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName,WorkFlowName WName ";
                        strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                        strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=4) ";
                        strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                        strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT TimesupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + empcode + " ) ";
                        strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

                        dsleaves = new DataSet();
                        dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);

                        if (dsleaves.Tables.Count > 0)
                        {
                            if (dsleaves.Tables[0].Rows.Count > 0)
                            {
                                wgid = Utility.ToString(dsleaves.Tables[0].Rows[0][0]);
                                flagUpdatefinal = false;
                                int maxApprovalleval = Utility.ToInteger(dsleaves.Tables[0].Rows[0][1]);
                                workFlowName = Utility.ToString(dsleaves.Tables[0].Rows[0][3]); //Added by Sandi
                                workflowId = Utility.ToString(dsleaves.Tables[0].Rows[0][0]);

                                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, "SELECT Status FROM TimeSheetMangment Where RefId='" + refid + "'", null);
                                while (dr1.Read())
                                {
                                    if (Utility.ToString(dr1.GetValue(0)) == "Approved" || Utility.ToString(dr1.GetValue(0)) == "Open" || Utility.ToString(dr1.GetValue(0)) == "Pending" || Utility.ToString(dr1.GetValue(0)) == "U") //Added last condition by Sandi
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
                            string Sql = "Update TimeSheetMangment set Status='Approved',Approver='" + varEmpName.Replace("'", "") + "',Remarks='" + remarks + "' where refid='" + refid + "'";

                            try
                            {
                                DataAccess.ExecuteStoreProc(Sql);
                                strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                                sendemail(ismultilevel, appLevel, empcode, refid,wgid );
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





                            string Sql = "Update TimeSheetMangment set Status='" + appLevel.ToString() + "' , Approver ='" + ApproverGroup.ToString() + "',Remarks='" + remarks + "' where RefId='" + refid + "'";

                            try
                            {
                                DataAccess.ExecuteStoreProc(Sql);
                                strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");


                                int arroverLevel = Convert.ToInt32(appLevel.ToString());
                                if ((arroverLevel - 1) != 0)
                                {


                                    int level = appLevel - 1;


                                    string payrollGroup = "Select email from employee where Emp_code in (select Emp_ID from EmployeeAssignedToPayrollGroup where payrollgroupId= (select PayRollGroupID from EmployeeWorkFlowLevel where RowID=" + level + " and WorkFlowID =" + workflowId + "))";
                                    SqlDataReader dr4 = DataAccess.ExecuteReader(CommandType.Text, payrollGroup);
                                    StringBuilder strUpdateBuild = new StringBuilder();

                                    while (dr4.Read())
                                    {
                                        email = dr4[0].ToString() + ";";
                                        strUpdateBuild.Append(email);
                                    }
                                    sendemail(ismultilevel, appLevel, empcode, refid,wgid );

                                    if (appLevel == 1)
                                    {
                                        flagUpdatefinal = true;
                                    }
                                }
                                else
                                {
                                    sendemail(ismultilevel, appLevel, empcode, refid,wgid ); //Added by Sandi
                                }
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

                    }
                }
            }

            if (strSucSubmit.Length > 0)
            {
                ShowMessageBox("TimeSheet Approved Successfully for: <br/>" + strSucSubmit.ToString());
                strMessage = "";
                //   rowApp.Visible = false;
            }
            if (strFailSubmit.Length > 0)
            {
                ShowMessageBox("TimeSheet Not Approved for: <br/>" + strFailSubmit.ToString());
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                strMessage = "";
            }

            RadGrid1.DataBind();
            txtremarks.Text = "";
        }




        protected void Button2_Click(object sender, EventArgs e)
        {
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            string remarks = txtEmpRemarks.Text + " - " + txtremarks.Text;
            ismultilevel = (bool)Session["ismultilevel"];
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        string refid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RefId"));
                        string empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMPID"));
                        string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));


                        string Sql = "Update TimeSheetMangment set status='Rejected',approver='" + varEmpName + "',remarks='" + remarks + "' where refid=" + refid;

                        int rejectlevel = 0;

                        try
                        {
                            DataAccess.ExecuteStoreProc(Sql);
                            strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                            string s = Session["icount"].ToString();
                            appLevel = int.Parse(Session["appLeavel"].ToString());
                            //rejectlevel = appLevel + 1; //murugan

                            rejectlevel = appLevel;
                            if (maxApprovalleval >= rejectlevel)
                            {
                                rejectlevel = maxApprovalleval;
                            }


                            sendemail_rejected(ismultilevel, rejectlevel, empcode, refid);
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


                        string Sqldelete = "delete ApprovedTimeSheet  where refid='" + refid + "'; update TimeSheet18 set Status='O' where RefiD='" + refid + "'";
                        try
                        {
                            DataAccess.ExecuteStoreProc(Sqldelete);
                        }
                        catch (Exception ex)
                        {

                        }





                    }
                }
            }

            //if (strSucSubmit.Length > 0)
            //{
            //    ShowMessageBox("TimeSheet Rejected Successfully for: <br/>" + strSucSubmit.ToString());
            //    strMessage = "";
            //}
            //if (strFailSubmit.Length > 0)
            //{
            //    ShowMessageBox("TimeSheet Not Rejected for: <br/>" + strFailSubmit.ToString());
            //    strMessage = "";
            //}
            //if (strPassMailMsg.Length > 0)
            //{
            //    ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());

            //    strMessage = "";
            //}
            //if (strFailMailMsg.Length > 0)
            //{
            //    ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
            //    strMessage = "";
            //}

            ShowMessageBox(strMessage);
            RadGrid1.DataBind();
            txtremarks.Text = "";
        }
        string fromdate, todate, empname;

//        protected void sendemail_rejected(bool isMultlevel, int rejlevel, string empcode, string refid)
//        {
//            string strEmail = "";
//            string ename = "";
//            string sqlEmailEmp = "select e.email,e.emp_name + ' ' + e.emp_lname empname from employee e where e.emp_code='" + empcode + "'";
//            SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp, null);

//            while (dr2.Read())
//            {
//                if (dr2.GetValue(0) != null && dr2.GetValue(0).ToString() != "{}" && dr2.GetValue(0).ToString() != "")
//                {
//                    strEmail = (string)dr2.GetValue(0);
//                    ename = (string)dr2.GetValue(1);
//                }
//            }
//            if (ename == "")
//            {
//                string sqlEmailEmp1 = "select e.emp_name + ' ' + e.emp_lname empname from employee e where e.time_card_no='" + empcode + "'";
//                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp1, null);
//                while (dr3.Read())
//                {
//                    ename = (string)dr3.GetValue(0);
//                }
//            }
//            string aaprover1 = "";
//            string from = "";
//            string to = "";
//            string SMTPserver = "";
//            string SMTPUser = "";
//            string SMTPPass = "";
//            int SMTPPORT = 25;
//            string emp_name = "";
//            string emailreq = "";
//            string body = "";
//            string cc = "";
//            string bodyMain = "";
//            StringBuilder strEmailMessage = new StringBuilder();

//            string login_user = "";
//            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
//            if (dr19.Read())
//            {
//                login_user = dr19[0].ToString();
//            }


//            //static members are shared across all instances and 
//            string sSQLemail = "sp_sendtimesheet_email";
//            SqlParameter[] parmsemail = new SqlParameter[2];
//            parmsemail[0] = new SqlParameter("@empcode", empcode);
//            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
//            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
//            while (dr9.Read())
//            {
//                from = Utility.ToString(dr9.GetValue(15));
//                to = Utility.ToString(dr9.GetValue(9));
//                SMTPserver = Utility.ToString(dr9.GetValue(6));
//                SMTPUser = Utility.ToString(dr9.GetValue(7));
//                SMTPPass = Utility.ToString(dr9.GetValue(8));
//                emp_name = Utility.ToString(dr9.GetValue(5));
//                body = Utility.ToString(dr9.GetValue(19));
//                bodyMain = Utility.ToString(dr9.GetValue(19));
//                SMTPPORT = Utility.ToInteger(dr9.GetValue(13));
//                emailreq = "yes";
//                cc = Utility.ToString(dr9.GetValue(17));
//            }

//            if (emailreq == "yes")
//            {

//                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);


//                oANBMailer.Subject = "Timesheet Is Rejected ";





//                DataSet dt = new DataSet();




//                if (refid != null)
//                {
//                    string sql = @" select  [Time_Card_No]
//                                
//                                  , convert( varchar(10) ,[TimeEntryStart],103)
//                                  ,convert( varchar(10) ,[TimeEntryEnd],103)
//                                  ,[NH]
//                                  ,[OT1]
//                                  ,[OT2]
//                                  ,[TotalHrsWrk]
//                                  ,[Remarks]  ,[RefID],Sub_Project_ID from [ApprovedTimeSheet] where [RefID]= '" + refid.ToString() + "'";


//                    dt = DataAccess.FetchRS(CommandType.Text, sql, null);
//                }

//                if (dt.Tables.Count > 0)
//                {

//                    foreach (DataRow dr in dt.Tables[0].Rows)
//                    {



//                        body = body.Replace("@tsdate", dr[1].ToString());
//                        body = body.Replace("@pname", dr[9].ToString());
//                        body = body.Replace("@strInTime", dr[1].ToString());
//                        body = body.Replace("@strOutTime", dr[2].ToString());
//                        strEmailMessage.Append(body).AppendLine();
//                        body = bodyMain;




//                    }
//                }
//                //---------------------------------------------------------
//                // emp email
//                string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
//                SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
//                if (dr6.Read())
//                {

//                    aaprover1 = dr6["email"].ToString();
//                }

//                string sql1;

//                //string sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
//                //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid >" + appLevel;
//                //SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);

//                if (login_user == "Super Admin")
//                {

//                    sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
//                    sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid >=" + rejlevel;

//                }
//                else
//                {
//                    sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
//                    sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid >" + rejlevel;

//                }


//                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



//                string email;
//                StringBuilder strUpdateBuild = new StringBuilder();
//                while (dr1.Read())
//                {
//                    email = dr1[0].ToString();
//                    strUpdateBuild.Append(email);
//                }

//                email = strUpdateBuild.ToString();


//                if (email.Length > 0)
//                {
//                    aaprover1 = aaprover1 + ";" + email;

//                }





//                //--------------------------

//                oANBMailer.MailBody = strEmailMessage.ToString();

//                oANBMailer.From = from;


//                oANBMailer.To = to;

//                if (isMultlevel)
//                {
//                    oANBMailer.Subject = "Timesheet Is Rejected at Level " + rejlevel.ToString();
//                    oANBMailer.To = aaprover1;
//                }






//                oANBMailer.Cc = cc;
//                try
//                {
//                    string sRetVal = oANBMailer.SendMail();
//                    if (sRetVal == "")
//                    {
//                        if (to.Length > 0)
//                        {
//                            if (cc.Length > 0)
//                            {
//                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To + "," + cc;
//                            }
//                            else
//                            {
//                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
//                    }

//                    //lblMsg.Text = "";
//                    //lblMsg.Text = strMessage;
//                }
//                catch (Exception ex)
//                {
//                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
//                }
//            }









//        }

        protected void sendemail_rejected(bool isMultlevel, int rejlevel, string empcode, string refid)
        {
            string strEmail = "";
            string ename = "";
            string sqlEmailEmp = "select e.email,e.emp_name + ' ' + e.emp_lname empname from employee e where e.emp_code='" + empcode + "'";
            SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp, null);

            while (dr2.Read())
            {
                if (dr2.GetValue(0) != null && dr2.GetValue(0).ToString() != "{}" && dr2.GetValue(0).ToString() != "")
                {
                    strEmail = (string)dr2.GetValue(0);
                    ename = (string)dr2.GetValue(1);
                }
            }
            if (ename == "")
            {
                string sqlEmailEmp1 = "select e.emp_name + ' ' + e.emp_lname empname from employee e where e.time_card_no='" + empcode + "'";
                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp1, null);
                while (dr3.Read())
                {
                    ename = (string)dr3.GetValue(0);
                }
            }
            string aaprover1 = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string cc = "";
            string bodyMain = "";
            StringBuilder strEmailMessage = new StringBuilder();


            string details = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";
            string detailsMain = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";

            string login_user = "";
            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr19.Read())
            {
                login_user = dr19[0].ToString();
            }


            //static members are shared across all instances and 
            string sSQLemail = "sp_sendtimesheet_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", empcode);
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr9.Read())
            {
                from = Utility.ToString(dr9.GetValue(15));
                to = Utility.ToString(dr9.GetValue(9));
                SMTPserver = Utility.ToString(dr9.GetValue(6));
                SMTPUser = Utility.ToString(dr9.GetValue(7));
                SMTPPass = Utility.ToString(dr9.GetValue(8));
                emp_name = Utility.ToString(dr9.GetValue(5));
                body = Utility.ToString(dr9.GetValue(19));
                bodyMain = Utility.ToString(dr9.GetValue(19));
                SMTPPORT = Utility.ToInteger(dr9.GetValue(13));
                emailreq = "yes";
                cc = Utility.ToString(dr9["ccTimeSheet"]);
            }

            if (emailreq == "yes")
            {

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);


                oANBMailer.Subject = "Timesheet Is Canceled ";





                DataSet dt = new DataSet();




                if (refid != null)
                {
                    string sql = @" select  t.[Time_Card_No]
                                
                                  , convert( varchar(10) ,t.TimeEntryStart,103)
                                  ,convert( varchar(10) ,t.TimeEntryEnd,103)
                                  ,t.NH
                                  ,t.OT1
                                  ,t.OT2
                                  ,t.TotalHrsWrk
                                  ,t.Remarks  ,t.RefID,t.Sub_Project_ID, e.emp_name from [ApprovedTimeSheet] t inner join employee e on t.Time_Card_No = e.time_card_no where [RefID]='" + refid.ToString() + "'";



                    dt = DataAccess.FetchRS(CommandType.Text, sql, null);
                }

                if (dt.Tables.Count > 0)
                {

                    if (dt.Tables.Count > 0)
                    {
                        int iy = 0;

                        foreach (DataRow dr in dt.Tables[0].Rows)
                        {

                            if (iy == 0)
                            {
                                fromdate = dr[1].ToString();
                                empname = dr[10].ToString();
                            }

                            todate = dr[1].ToString();

                            iy = iy + 1;

                            details = details.Replace("@tsdate", dr[1].ToString());
                            details = details.Replace("@pname", dr[9].ToString());
                            details = details.Replace("@strInTime", dr[1].ToString());
                            details = details.Replace("@strOutTime", dr[2].ToString());
                            strEmailMessage.Append(details).AppendLine();
                            details = detailsMain;


                        }
                    }
                }
                //---------------------------------------------------------
                // emp email
                string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
                SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr6.Read())
                {

                    aaprover1 = dr6["email"].ToString();
                }

                string sql1;

                //string sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid >" + appLevel;
                //SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);

                if (login_user == "Super Admin")
                {

                    sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                    sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeWorkFlowLevel].[FlowType]=4 and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>=" + rejlevel;

                }
                else
                {
                    sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                    sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID  and [EmployeeWorkFlowLevel].[FlowType]=4 and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid>" + rejlevel;

                }


                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



                string email;
                StringBuilder strUpdateBuild = new StringBuilder();
                while (dr1.Read())
                {
                    email = dr1[0].ToString();
                    if (strUpdateBuild.Length == 0)
                    {
                        strUpdateBuild.Append(email);
                    }
                    else {
                        strUpdateBuild.Append(";");
                        strUpdateBuild.Append(email);
                    }
                    
                }

                email = strUpdateBuild.ToString();


                if (email.Length > 0)
                {
                    aaprover1 = aaprover1 + ";" + email;

                }





                //--------------------------

                oANBMailer.MailBody = strEmailMessage.ToString();

                oANBMailer.From = from;


                oANBMailer.To = to;

                if (isMultlevel)
                {
                    oANBMailer.Subject = "Timesheet Is Rejected at Level " + rejlevel.ToString();
                    oANBMailer.To = aaprover1;
                }

                if (ismultilevel)
                {
                    body = body.Replace("@fromDate", fromdate);
                    body = body.Replace("@toDate", todate);
                    body = body.Replace("@EmpName", empname);
                    body = body.Replace("@details", strEmailMessage.ToString());
                    body = body.Replace("@sub_approve", "Rejected");
                    oANBMailer.MailBody = body;
                }





                oANBMailer.Cc = cc;
                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To + "," + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To;
                            }
                        }
                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                    }

                    //lblMsg.Text = "";
                    //lblMsg.Text = strMessage;
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }









        }


        protected void sendemail(bool isMultlevel, int applevel, string empcode, string refid,string wgid)
        {

            int maxApprovalleval = Utility.ToInteger(Session["maxApprovalleval"].ToString());
            string strEmail = "";
            string ename = "";
            string sqlEmailEmp = "select e.email,e.emp_name + ' ' + e.emp_lname empname from employee e where e.emp_code='" + empcode + "'";
            SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp, null);

            while (dr2.Read())
            {
                if (dr2.GetValue(0) != null && dr2.GetValue(0).ToString() != "{}" && dr2.GetValue(0).ToString() != "")
                {
                    strEmail = (string)dr2.GetValue(0);
                    ename = (string)dr2.GetValue(1);
                }
            }
            if (ename == "")
            {
                string sqlEmailEmp1 = "select e.emp_name + ' ' + e.emp_lname empname from employee e where e.time_card_no='" + empcode + "'";
                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, sqlEmailEmp1, null);
                while (dr3.Read())
                {
                    ename = (string)dr3.GetValue(0);
                }
            }
            string aaprover1 = "";
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string cc = "";
            string bodyMain = "";

            string details = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";
            string detailsMain = "Entry Date :@tsdate For the Sub Project:@pname In Time: @strInTime and Out Time:@strOutTime <br /><br />";
            StringBuilder strEmailMessage = new StringBuilder();

            string login_user = "";
            SqlDataReader dr19 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr19.Read())
            {
                login_user = dr19[0].ToString();
            }


            //static members are shared across all instances and 
            string sSQLemail = "sp_sendtimesheet_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", empcode);
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr9.Read())
            {
                from = Utility.ToString(dr9.GetValue(15));
                to = Utility.ToString(dr9.GetValue(9));
                SMTPserver = Utility.ToString(dr9.GetValue(6));
                SMTPUser = Utility.ToString(dr9.GetValue(7));
                SMTPPass = Utility.ToString(dr9.GetValue(8));
                emp_name = Utility.ToString(dr9.GetValue(5));
                body = Utility.ToString(dr9.GetValue(19));
                bodyMain = Utility.ToString(dr9.GetValue(19));
                SMTPPORT = Utility.ToInteger(dr9.GetValue(13));
                emailreq = "yes";
                cc = Utility.ToString(dr9["ccTimeSheet"]);
            }

            if (emailreq == "yes")
            {

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);


                oANBMailer.Subject = "Timesheet Is Approved at Level " + applevel.ToString();





                DataSet dt = new DataSet();




                if (refid != null)
                {
                    string sql = @" select  t.[Time_Card_No]
                                
                                  , convert( varchar(10) ,t.TimeEntryStart,103)
                                  ,convert( varchar(10) ,t.TimeEntryEnd,103)
                                  ,t.NH
                                  ,t.OT1
                                  ,t.OT2
                                  ,t.TotalHrsWrk
                                  ,t.Remarks  ,t.RefID,t.Sub_Project_ID, e.emp_name from [ApprovedTimeSheet] t inner join employee e on t.Time_Card_No = e.time_card_no where [RefID]='" + refid.ToString() + "'";


                    dt = DataAccess.FetchRS(CommandType.Text, sql, null);
                }

                if (dt.Tables.Count > 0)
                {
                    int iy = 0;

                    foreach (DataRow dr in dt.Tables[0].Rows)
                    {

                        if (iy == 0)
                        {
                            fromdate = dr[1].ToString();
                            empname = dr[10].ToString();
                        }

                        todate = dr[1].ToString();

                        iy = iy + 1;

                        details = details.Replace("@tsdate", dr[1].ToString());
                        details = details.Replace("@pname", dr[9].ToString());
                        details = details.Replace("@strInTime", dr[1].ToString());
                        details = details.Replace("@strOutTime", dr[2].ToString());
                        strEmailMessage.Append(details).AppendLine();
                        details = detailsMain;
                        

                    }
                }
                //---------------------------------------------------------
                // emp email
                string sqlEmail1 = "SELECT c.email,c.emp_code From employee c Where c.emp_name='" + emp_name + "'";
                SqlDataReader dr6 = DataAccess.ExecuteReader(CommandType.Text, sqlEmail1, null);
                if (dr6.Read())
                {

                    aaprover1 = dr6["email"].ToString();
                }

                string sql1 = "";

                if (applevel == 1 && (login_user == "Super Admin"))
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid >= " + applevel + " ) and efl.FlowType=4 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wgid;

                }
                 else if (applevel == 1 && login_user != "Super Admin")
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid > " + applevel + " ) and efl.FlowType=4 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wgid;

                } 
                else if (applevel > 1 && login_user == "Super Admin")
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid = " + (applevel - 1) + " or efl.rowid >= " + applevel + " ) and efl.FlowType=4 and e.Company_Id=" + compid + " and efl.[WorkFlowID]=" + wgid;

                }
                else if (applevel > 1 && login_user != "Super Admin")
                {


                    sql1 = "SELECT distinct  e.EMAIL FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef";
                    sql1 = sql1 + " where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid = " + (applevel - 1) + " or efl.rowid > " + applevel + " ) and efl.FlowType=4 and e.Company_Id=" + compid + " and efl.[WorkFlowID]="+ wgid ;

                }



               








                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



                string email;
                StringBuilder strUpdateBuild = new StringBuilder();
                while (dr1.Read())
                {
                    if (strUpdateBuild.Length > 0) {
                        strUpdateBuild.Append(";");
                    }
                    email = dr1[0].ToString();
                    strUpdateBuild.Append(email);
                }

                email = strUpdateBuild.ToString();


                aaprover1 = aaprover1 + ";" + email;







                //--------------------------

                oANBMailer.MailBody = strEmailMessage.ToString();





                oANBMailer.From = from;


                oANBMailer.To = to;

                if (isMultlevel)
                {
                    oANBMailer.To = aaprover1;
                }


                if (ismultilevel)
                {
                    body = body.Replace("@fromDate", fromdate);
                    body = body.Replace("@toDate", todate);
                    body = body.Replace("@EmpName", empname);
                    body = body.Replace("@details", strEmailMessage.ToString());
                    body = body.Replace("@sub_approve", "Approved");
                    oANBMailer.MailBody = body;
                }

                oANBMailer.Cc = cc;
                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To + "," + cc;
                            }
                            else
                            {
                                strMessage = strMessage + "<br/>" + "An email has been sent to " + oANBMailer.To;
                            }
                        }
                    }
                    else
                    {
                        strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                    }

                    //lblMsg.Text = "";
                    //lblMsg.Text = strMessage;

                    ShowMessageBox("Email Send successfully to: <br/>" + strMessage.ToString());
                }
                catch (Exception ex)
                {
                    strMessage = strMessage + "<br/>" + "Error Occured While Sending Mail.";
                }
            }









        }

               




        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (dataItem["EMPID"].Text == "Mexico")
                    dataItem.CssClass = "MyMexicoRowClass";
            }



            if (e.Item.ItemType == GridItemType.Item)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                string empcode = dataItem["EMPID"].Text;
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    // hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "tsfile" + "/" + hl + ".pdf";
                    hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "tsfile" + "/" + hl.Text;
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





