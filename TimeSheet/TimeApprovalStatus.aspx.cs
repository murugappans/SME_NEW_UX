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
    public partial class TimeApprovalStatus : System.Web.UI.Page
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
        protected string sUserName = "", sgroupname = "";
        string varEmpCode = "";
       
      
        DataSet dsleaves;
        string email;
        public string WorkFlowName;
        int appLevel;
        bool ismultilevel=false;
        int lcount = 0;
        DateTime f_date;
        DateTime t_date;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varEmpName = Session["Emp_Name"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                Session["superAdmin"] = "AS";

                varEmpCode = Session["EmpCode"].ToString();
                compid = Utility.ToInteger(Session["Compid"]);


            #region Emp dropdown
            DataSet ds_employee = new DataSet();
            //Senthil for Group Management

            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, " select groupname from employee,usergroups where usergroups.groupid=employee.groupid and employee.emp_code=" + Session["EmpCode"].ToString(), null);
            if (dr9.Read())
            {
                sgroupname = dr9[0].ToString();
                Session["superAdmin"] = sgroupname;

            }
               
            if (sgroupname == "Super Admin")
            {
                ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and Emp_Code in(Select Distinct Emp_ID From [EmployeeAssignedToWorkersList])  order by emp_name");
            }
            else
            {
               
                  //  ds_employee = getDataSet("SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_clsupervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "' ");



//                    ds_employee = getDataSet(@"select em.[emp_code],isNull(em.[emp_name],'')+' '+isnull(em.[emp_lname],'') as [emp_name]  from  employee em
//
//
//  where em.Emp_Code in(Select Distinct Emp_ID From [EmployeeAssignedToWorkersList] ) and  em.TimesupervicerMulitilevel in ( SELECT  CONVERT(VARCHAR,efl.ID) FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and ( efl.rowid >= 
//
//
//(SELECT top 1  efl1.RowID FROM EMPLOYEE e1,[EmployeeWorkFlowLevel] efl1,[EmployeeAssignedToPayrollGroup] ea1,[EmployeeWorkFlow] ef1 where e1.emp_code=ea1 .emp_id and ea1.[PayrollGroupID]=efl.[PayrollGroupID] and efl1.[WorkFlowID]=ef1.ID and ea1.[PayrollGroupID]=efl1.[PayRollGroupID]  and ( ea1.Emp_ID=" + varEmpCode + ") and efl1.FlowType=4 and e1.Company_Id=" + compid + ")) and efl.FlowType=4 and e.Company_Id=" + compid + ") union all   SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee el where el.timesupervisor=" + varEmpCode + " or el.emp_code=" + varEmpCode + ")");

                ds_employee = getDataSet(@" select  [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'') as [emp_name] from employee where [TimesupervicerMulitilevel] in(select CONVERT(VARCHAR,id)  from [EmployeeWorkFlowLevel] A where rowid >= ANY (select rowid from [EmployeeWorkFlowLevel] B where
   id in(( SELECT  efl.ID FROM EMPLOYEE e,[EmployeeWorkFlowLevel] efl,[EmployeeAssignedToPayrollGroup] ea,[EmployeeWorkFlow] ef 
  where e.emp_code=ea .emp_id and efl.[WorkFlowID]=ef.ID and ea.[PayrollGroupID]=efl.[PayRollGroupID]  and 
  ( efl.rowid >= (SELECT top 1  efl1.RowID FROM EMPLOYEE e1,[EmployeeWorkFlowLevel] efl1,[EmployeeAssignedToPayrollGroup] 
  ea1,[EmployeeWorkFlow] ef1 where e1.emp_code=ea1 .emp_id and ea1.[PayrollGroupID]=efl.[PayrollGroupID] and 
  efl1.[WorkFlowID]=ef1.ID and ea1.[PayrollGroupID]=efl1.[PayRollGroupID]  and ( ea1.Emp_ID= " + varEmpCode + " ) and efl1.FlowType=4 and  e1.Company_Id= " + compid + " )) and efl.FlowType=4 and e.Company_Id=" + compid + ")) AND B.[WorkFlowID]=A.[WorkFlowID] ))  union all      SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee el where el.[emp_clsupervisor]= " + varEmpCode + "  or el.emp_code=" + varEmpCode + ") order by emp_name");

                   

            }
            DropDownList1.DataSource = ds_employee.Tables[0];
            DropDownList1.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
            DropDownList1.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
            DropDownList1.DataBind();
            #endregion

            #region Yeardropdown
            cmbYear.DataBind();


            if ((string)varEmpCode != "0")//checking whether user is login in the table.
            {
                DropDownList1.SelectedValue = varEmpCode;
            }
            cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();
            #endregion 

        }

        }

        private object _dataItem = null;
        string empcode = "";
        string refid = "";
        int id = 0;
        int maxApprovalleval = 0;
       
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

        protected void bindgrid(object sender, EventArgs e)
        {

            RadGrid1.DataBind();
        }

        string wgid = "0";
      
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();


            if (e.CommandName == "Delete")
            {
                GridEditableItem dataItem = e.Item as GridEditableItem;

                string refid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RefId"));
                string emp_code = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMPID"));
                string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
                f_date =Convert.ToDateTime(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("FromDate"));
                t_date = Convert.ToDateTime(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Enddate"));
                string appstatus = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Status"));


                //string strSql = "Select WL.ID,WL.RowID, WorkFlowName "; //MURUGAN
                string strSql = "Select WL.[WorkFlowID],WL.RowID, WorkFlowName ";
                strSql = strSql + "From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN ";
                strSql = strSql + "(Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=4) ";
                strSql = strSql + "And Company_ID=" + Utility.ToInteger(Session["Compid"]).ToString() + ") WF Inner Join EmployeeWorkFlowLevel WL ";
                strSql = strSql + "On WF.ID=WL.WorkFlowID WHERE WL.ID IN(SELECT TimesupervicerMulitilevel FROM dbo.employee   WHERE emp_code=" + emp_code + " ) ";
                strSql = strSql + "Order By WF.WorkFlowName, WL.RowID";

                dsleaves = new DataSet();
                dsleaves = DataAccess.FetchRS(CommandType.Text, strSql, null);

                int appLeveldb = 0;

        

                if (dsleaves.Tables.Count > 0)
                {

                    if (dsleaves.Tables[0].Rows.Count > 0)
                    {
                      
                        ismultilevel = true;
                        wgid = dsleaves.Tables[0].Rows[0][0].ToString();
                     
                     
                    }
                    else
                    {
                        ismultilevel = false;

                    }
                }
                else
                {
                    ismultilevel = false;
               
                }


                int rejectlevel = 0;
                string Sqldelete = " delete TimeSheetMangment where refid= " + refid + " ; delete ApprovedTimeSheet  where refid= '" + refid + "';update TimeSheet18 set Status='O' where RefiD='" + refid + "'";

                try
                {
                    DataAccess.ExecuteStoreProc(Sqldelete);
                    strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");

                    int n;
                    bool isNumeric = int.TryParse(appstatus, out n);

                    if (isNumeric)
                    {
                        if (n > 1)
                        {
                            rejectlevel = n - 1;
                        }
                        else {
                            rejectlevel = n;
                        }
                        
                    }
                    else if (appstatus == "Approved")
                    {
                        rejectlevel = 1;

                    }
                    else {
                        rejectlevel = 0;
                    }
                    
                   // rejectlevel = appLevel + 1;

                    //if (maxApprovalleval >= rejectlevel)
                    //{
                    //    rejectlevel = maxApprovalleval;
                    //}


                    sendemail_rejected(ismultilevel, rejectlevel, emp_code, refid,wgid );
                }
                catch (Exception ex)
                {
                    string ErrMsg = ex.Message;
                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                    {
                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                        strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                        ShowMessageBox(strFailSubmit.ToString());
                    
                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
                        ShowMessageBox(strFailMailMsg.ToString());
                    }

                }



                       ShowMessageBox(strMessage);

       

                            

            }
        }
 
                 

      


        //protected void Button2_Click(object sender, EventArgs e)
        //{
           

        //    StringBuilder strSucSubmit = new StringBuilder();
        //    StringBuilder strFailSubmit = new StringBuilder();
          
        //    ismultilevel = (bool)Session["ismultilevel"];
        //    foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (item is GridItem)
        //        {
        //            GridDataItem dataItem = (GridDataItem)item;
        //            RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
        //            if (rad1.Checked == true)
        //            {
        //              string  refid = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("RefId"));
        //               string empcode = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("EMPID"));
        //                string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));

                      
        //                string Sql = "delete  TimeSheetMangment where refid=" + refid;

        //                int rejectlevel=0;

        //                try
        //                {
        //                    DataAccess.ExecuteStoreProc(Sql);
        //                    strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
        //                    string s = Session["icount"].ToString();

        //                   rejectlevel=appLevel+1;

        //                   if (maxApprovalleval >= rejectlevel)
        //                   {
        //                       rejectlevel = maxApprovalleval;
        //                   }


        //                   sendemail_rejected(ismultilevel, rejectlevel, empcode, refid);
        //                }
        //                catch (Exception ex)
        //                {
        //                    string ErrMsg = ex.Message;
        //                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
        //                    {
        //                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
        //                        strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
        //                    }
        //                    else
        //                    {
        //                        strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
        //                    }
        //                }


        //                string Sqldelete = "delete ApprovedTimeSheet  where refid=" + refid;
        //                try
        //                {
        //                    DataAccess.ExecuteStoreProc(Sqldelete);
        //                }
        //                catch (Exception ex)
        //                {

        //                }




                       
        //            }
        //        }
        //    }

        //    //if (strSucSubmit.Length > 0)
        //    //{
        //    //    ShowMessageBox("TimeSheet Rejected Successfully for: <br/>" + strSucSubmit.ToString());
        //    //    strMessage = "";
        //    //}
        //    //if (strFailSubmit.Length > 0)
        //    //{
        //    //    ShowMessageBox("TimeSheet Not Rejected for: <br/>" + strFailSubmit.ToString());
        //    //    strMessage = "";
        //    //}
        //    //if (strPassMailMsg.Length > 0)
        //    //{
        //    //    ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
              
        //    //    strMessage = "";
        //    //}
        //    //if (strFailMailMsg.Length > 0)
        //    //{
        //    //    ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
        //    //    strMessage = "";
        //    //}

        //    ShowMessageBox(strMessage);
        //    RadGrid1.DataBind();
            
        //}

        string fromdate, todate, empname;
        protected void sendemail_rejected(bool isMultlevel, int rejlevel, string empcode, string refid,string wgid)
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

                string sql1="";

                //string sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and [EmployeeWorkFlowLevel].rowid >" + appLevel;
                //SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);

                if (login_user == "Super Admin")
                {

                    //sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                    //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and  FlowType=4 and [EmployeeWorkFlowLevel].rowid<=" + rejlevel;
                    if (ismultilevel)
                    {
                        if (rejlevel == 0)
                        {
                            sql1 = "select email from employee where emp_code in (select emp_id from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID] in (select [PayrollGroupID] from [EmployeeWorkFlowLevel] where [WorkFlowID]=" + wgid + " and id=(select [TimesupervicerMulitilevel] from employee where emp_code=" + empcode + " ))) ";
                        }
                        else {

                            sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                            sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and  FlowType=4 and [EmployeeWorkFlowLevel].rowid>=" + rejlevel;
                        }
                    }
                    else
                    {

                        sql1 = "select email from employee where emp_code=(select [timesupervisor] from employee where emp_code=" + empcode + " )";
                    }

                }
                else if (empcode == Session["EmpCode"].ToString()) 
                {
                    //sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                    //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and  FlowType=4 and [EmployeeWorkFlowLevel].rowid<=" + rejlevel;

                    if (ismultilevel)
                    {

                        sql1 = "select email from employee where emp_code in (select emp_id from [EmployeeAssignedToPayrollGroup] where [PayrollGroupID] in (select [PayrollGroupID] from [EmployeeWorkFlowLevel] where [WorkFlowID]=" + wgid + " and id=(select [TimesupervicerMulitilevel] from employee where emp_code=" + empcode + " ))) ";
                    }
                    else {

                        sql1 = "select email from employee where emp_code=(select [timesupervisor] from employee where emp_code="+ empcode +" )";
                    }
                }
                    
                else
                {
                    //sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                    //sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and  FlowType=4 and [EmployeeWorkFlowLevel].rowid<" + rejlevel;
                    if (rejlevel != 0) {
                        sql1 = "SELECT  distinct EMAIL FROM EMPLOYEE,[EmployeeWorkFlowLevel],[EmployeeAssignedToPayrollGroup],[EmployeeWorkFlow] where employee.emp_code=[EmployeeAssignedToPayrollGroup].emp_id";
                        sql1 = sql1 + " and [EmployeeWorkFlowLevel].[WorkFlowID]=[EmployeeWorkFlow].ID and [EmployeeAssignedToPayrollGroup].[PayrollGroupID]=[EmployeeWorkFlowLevel].[PayRollGroupID]  and  FlowType=4 and [EmployeeWorkFlowLevel].rowid >=" + rejlevel;
                    
                    }
                    else {
                        sql1 = "select email from employee where emp_code=" + empcode;
                    }
                    
                }


                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);



                string email;
                StringBuilder strUpdateBuild = new StringBuilder();
                while (dr1.Read())
                {
                    email = dr1[0].ToString();
                    strUpdateBuild.Append(email);
                }

                email = strUpdateBuild.ToString();


                //if (email.Length > 0)
                //{
                //    aaprover1 = aaprover1 + ";" + email;

                //}

                if (login_user == "Super Admin" )
                {
                    //aaprover1 = aaprover1 + ";" + email;
                    email = aaprover1 + ";" + email;
                }
                else if (empcode != Session["EmpCode"].ToString() && rejlevel != 0)
                {

                    email = aaprover1 + ";" + email;
                }
                



                //--------------------------

                oANBMailer.MailBody = strEmailMessage.ToString();

                oANBMailer.From = from;


                //oANBMailer.To = to;
                oANBMailer.To = email;
                if (isMultlevel)
                {
                    oANBMailer.Subject = "Timesheet Is Cancelled ";
                   // oANBMailer.To = aaprover1;
                }

                if (ismultilevel)
                {
                    body = body.Replace("@fromDate",f_date.ToString("dd/MMM/yyyy" ));
                    body = body.Replace("@toDate", t_date.ToString("dd/MMM/yyyy"));
                    body = body.Replace("@EmpName", emp_name);
                    body = body.Replace("@details", strEmailMessage.ToString());
                    body = body.Replace("@sub_approve", "Cancelled");
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




        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {

          string  stringsql1 = @"select Emp_ID from EmployeeAssignedToPayrollGroup pg where pg.PayrollGroupID in(
SELECT
      [PayRollGroupID]
     
  FROM [EmployeeWorkFlowLevel]  where RowID=1 and FlowType=4) and pg.Emp_ID=" + Session["EmpCode"].ToString();



          SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, stringsql1, null);
            bool isFirstLevelApprover = false;

            while (dr1.Read())
            {
                isFirstLevelApprover = true;
            }


           
         


            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem1 = e.Item as GridDataItem;
                
                sgroupname = Session["superAdmin"].ToString();

               

                if (dataItem1["payrollstatus"].Text != "L")
                {



                    //if ((sgroupname != "Super Admin" && isFirstLevelApprover == false))
                    //{
                    //    //dataItem1["DeleteColumn"].Visible = false;

                    //    RadGrid1.MasterTableView.GetColumn("Deletecolumn").Display = false;  
                      
                    //}
                    //else if (sgroupname != "Super Admin" && dataItem1["Status"].Text != "Pending")
                    //{
                    //    RadGrid1.MasterTableView.GetColumn("Deletecolumn").Display = false;
                    //}
                    //else if (sgroupname != "Super Admin" ) 
                    //{
                    //    RadGrid1.MasterTableView.GetColumn("Deletecolumn").Display = false;
                    //}

                    //--murugan
                   
                        

                        if ((sgroupname != "Super Admin" && isFirstLevelApprover == false))
                        {
                            dataItem1.Cells[13].Controls[0].Visible = false;

                        }
                        if (sgroupname != "Super Admin" && dataItem1["Status"].Text != "Pending")
                        {
                            dataItem1.Cells[13].Controls[0].Visible = false;

                        }
                        if (sgroupname != "Super Admin" && dataItem1["Status"].Text == "Pending")
                        {
                            dataItem1.Cells[13].Controls[0].Visible = true;

                        }
                        if (isFirstLevelApprover == true && dataItem1["Status"].Text == "Pending")
                        {
                            dataItem1.Cells[13].Controls[0].Visible = true;

                        }
                        if (isFirstLevelApprover == true && dataItem1["Status"].Text.Length  == 1)
                        {
                            dataItem1.Cells[13].Controls[0].Visible = true;

                        }
                        if (isFirstLevelApprover == true && dataItem1["Status"].Text == "Approved")
                        {
                            dataItem1.Cells[13].Controls[0].Visible = true;

                        }
                }



                if (dataItem1["payrollstatus"].Text == "L")
                {


                    // dataItem1.Cells[11].Controls[0].Visible = false;

                   // RadGrid1.MasterTableView.GetColumn("Deletecolumn").Display = false;
                    dataItem1.Cells[13].Controls[0].Visible = false;

                }
              

            } 

    







            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
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





