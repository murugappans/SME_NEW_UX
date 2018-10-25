using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Telerik.Web.UI;
using System.Text;


namespace SMEPayroll.Employee
{
    public partial class Terminate : System.Web.UI.Page
    {
        
        public int? emp_code=null;
        protected int comp_id;
        public string message,terminationDate;
        protected void Page_Load(object sender, EventArgs e)
        {

         comp_id = Utility.ToInteger(Session["Compid"]);
         emp_code=Convert.ToInt32(Request.QueryString["emp_code"]);
        // message = Convert.ToString(Request.QueryString["msg"]);
         terminationDate = Convert.ToString(Request.QueryString["TerminationDate"]);
         message = Session["Message"].ToString(); 
        lblMessage.Text = message;


            if (!IsPostBack)
            {
                if (emp_code.HasValue)
                {
                    Leaves();
                    ItemIssue();
                    LeaveSupervisor();
                    ClaimSupervisor();
                    TimeSheetSupervisor();
                    WorkflowSupervisor();
                }
            }
        }

        private void WorkflowSupervisor()
        {
            DataSet ds_WF = new DataSet();
            ds_WF = getDataSet("select EMP_CODE, emp_name as Name from employee where emp_code in (select Emp_ID from employeeassignedtopayrollgroup where payrollgroupid=(select PayRollGroupID from EmployeeWorkFlowLevel where id=(select pay_supervisor from employee where emp_code='" + emp_code + "')))");
            gridWorkflowSup.DataSource = ds_WF.Tables[0];
            gridWorkflowSup.DataBind();
        }

        private void TimeSheetSupervisor()
        {
            DataSet ds_TS = new DataSet();
            ds_TS = getDataSet("SELECT  EMP_CODE,EMP_NAME + ' ' + EMP_LNAME AS Name FROM EMPLOYEE WHERE TERMINATION_DATE IS NULL  AND timesupervisor ='" + emp_code + "' AND COMPANY_ID ='" + comp_id + "' ORDER BY EMP_NAME");
            gridTimeSheetSup.DataSource = ds_TS.Tables[0];
            gridTimeSheetSup.DataBind();
        }

        private void ClaimSupervisor()
        {

             DataSet ds_Cl = new DataSet();
             ds_Cl = getDataSet("SELECT  EMP_CODE,EMP_NAME + ' ' + EMP_LNAME AS Name FROM EMPLOYEE WHERE TERMINATION_DATE IS NULL  AND EMP_CLSUPERVISOR ='" + emp_code + "' AND COMPANY_ID ='" + comp_id + "' ORDER BY EMP_NAME");
             gridClaimSup.DataSource = ds_Cl.Tables[0];
             gridClaimSup.DataBind();
            
        }

        private void LeaveSupervisor()
        {
            DataSet ds1 = new DataSet();
            ds1 = getDataSet("SELECT  EMP_CODE,EMP_NAME + ' ' + EMP_LNAME AS Name FROM EMPLOYEE WHERE TERMINATION_DATE IS NULL  AND EMP_SUPERVISOR ='" + emp_code + "' AND COMPANY_ID ='" + comp_id + "' ORDER BY EMP_NAME");
            gridLeaveSup.DataSource = ds1.Tables[0];
            gridLeaveSup.DataBind();
           
        }

        private void ItemIssue()
        {
            DataSet ds = new DataSet();
            ds = getDataSet("Select EI.ID, EI.ItemID, ISC.ItemName, EI.SerialNumber, EI.Quantity, EI.Remarks,EI.ItemReturn From EmployeeItemIssued EI Inner Join Item ISC On EI.ItemID = ISC.ID Where EI.Emp_ID=" + emp_code);
            gridItem.DataSource = ds.Tables[0];
            gridItem.DataBind();
        }

        private void Leaves()
        {
            DateTime dt_Termination = Convert.ToDateTime(terminationDate);

            string sSQL = "sp_GetEmployeeLeavePolicy";
            SqlParameter[] parms = new SqlParameter[4];
            parms[0] = new SqlParameter("@empid", Utility.ToInteger(emp_code));
            parms[1] = new SqlParameter("@year", dt_Termination.Year);
            parms[2] = new SqlParameter("@applydateon", dt_Termination);
            parms[3] = new SqlParameter("@filter", 0);
            DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

            #region "Check Transfer"
            
            string SQL = "Select * from LeavesAllowedInYears where Years=" + dt_Termination.Year + " and Emp_ID=" + emp_code + "";
            DataSet ds_Transfer = DataAccess.FetchRS(CommandType.Text, SQL, null);
            if (ds_Transfer.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["Type"].ToString() == "Annual Leave")
                    {
                        ds.Tables[0].Rows[i]["leavesearned"] = ds_Transfer.Tables[0].Rows[0]["LeavesAvailable"];
                        ds.Tables[0].Rows[i]["LY_Leaves_Bal"] = ds_Transfer.Tables[0].Rows[0]["LY_Leaves_Bal"]; ;
                        //ActualLeavesAvailable
                    }
                }
            }
            #endregion

            try {
                
                comp_id = Utility.ToInteger(Session["Compid"]);
                string Leave_Model = "";
                double leave_earned = 0;
                double actual_leave_earned = 0;
                double leave_taken = 0;
                double leave_balance = 0;
                double LY_Leaves_Bal = 0;
                string strSQL = "select leave_model from company where company_id=" + comp_id + "";
                DataSet ds_Leave_Model = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                Leave_Model = ds_Leave_Model.Tables[0].Rows[0][0].ToString();
                if (Leave_Model == "1" || Leave_Model == "7" || Leave_Model == "9" )
                {
                    
                    int month = dt_Termination.Month;
                    DataRow[] rows = ds.Tables[0].Select("Type='Annual Leave'");

                    LY_Leaves_Bal = Convert.ToDouble(rows[0]["LY_Leaves_Bal"].ToString());
                    if (Leave_Model == "1")
                    {
                        leave_earned = Convert.ToDouble(rows[0]["leavesearned"].ToString());
                        actual_leave_earned = Math.Round((leave_earned / 12) * month, 2);
                    }
                    else
                    {
                        actual_leave_earned = Convert.ToDouble(rows[0]["leavesearned"].ToString());
                    }

                    actual_leave_earned = actual_leave_earned ;
                    leave_taken = Convert.ToDouble(rows[0]["paidleaves"].ToString());

                    leave_balance = (actual_leave_earned - leave_taken) + +LY_Leaves_Bal;

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["Type"].ToString() == "Annual Leave")
                        {
                            ds.Tables[0].Rows[i]["leavesearned"] = actual_leave_earned;
                            ds.Tables[0].Rows[i]["ActualLeavesAvailable"] = leave_balance;
                            //ActualLeavesAvailable
                        }
                    }
                }
            }
            catch (Exception ex) 
            { }
            gridLeave.Visible = true;
            gridLeave.DataSource = ds;
            gridLeave.MasterTableView.DataSource = ds;
            gridLeave.DataBind();
        }


        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }


        protected void gridLeaveSup_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int strIndex = gridLeaveSup.MasterTableView.CurrentPageIndex;

                Label lbl = e.Item.FindControl("lblSn") as Label;
                lbl.Text = Convert.ToString((strIndex * gridLeaveSup.PageCount) + e.Item.ItemIndex + 1);
            }
        }


        protected void gridClaimSup_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int strIndex = gridClaimSup.MasterTableView.CurrentPageIndex;

                Label lbl = e.Item.FindControl("lblSn") as Label;
                lbl.Text = Convert.ToString((strIndex * gridClaimSup.PageCount) + e.Item.ItemIndex + 1);
            }
        }

        protected void gridTimeSheetSup_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int strIndex = gridTimeSheetSup.MasterTableView.CurrentPageIndex;

                Label lbl = e.Item.FindControl("lblSn") as Label;
                lbl.Text = Convert.ToString((strIndex * gridTimeSheetSup.PageCount) + e.Item.ItemIndex + 1);
            }
        }

        protected void gridWorkflowSup_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                int strIndex = gridWorkflowSup.MasterTableView.CurrentPageIndex;

                Label lbl = e.Item.FindControl("lblSn") as Label;
                lbl.Text = Convert.ToString((strIndex * gridWorkflowSup.PageCount) + e.Item.ItemIndex + 1);
            }
        }

        protected void btnRemoveLeave_Click(object sender, EventArgs e)
        {
            try
            {
                #region Write removed information in remark column(Employee Table)
                string Msg = GetEmployeeTerminationInfo();
                string Sql_Tdate1 = "update Employee set remarks=remarks+'" + Msg + "' where emp_code='" + emp_code + "'";
                DataAccess.FetchRS(CommandType.Text, Sql_Tdate1, null);
                #endregion


                #region removing Leave supervisor for  employee
                    string ssqlb = "update EMPLOYEE set EMP_SUPERVISOR=0 where EMP_SUPERVISOR ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    LeaveSupervisor();
                    gridLeaveSup.Rebind();
                #endregion

                #region removing Claim supervisor for  employee
                    string ssqlclaim = "update EMPLOYEE set EMP_CLSUPERVISOR=0 where EMP_CLSUPERVISOR ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                    DataAccess.FetchRS(CommandType.Text, ssqlclaim, null);
                    ClaimSupervisor();
                    gridClaimSup.Rebind();
                #endregion

                #region removing Timesheet supervisor for  employee
                    string ssqlTimesheet = "update EMPLOYEE set timesupervisor=0 where timesupervisor ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                    DataAccess.FetchRS(CommandType.Text, ssqlTimesheet, null);
                    TimeSheetSupervisor();
                    gridTimeSheetSup.Rebind();
                #endregion

                #region removing Workflow supervisor for  employee
                    string ssqlWorkflow = "update employee set pay_supervisor=0 where emp_code in (select Emp_ID from employeeassignedtopayrollgroup where payrollgroupid=(select PayRollGroupID from EmployeeWorkFlowLevel where id=(select pay_supervisor from employee where emp_code='" + emp_code + "' )))";
                    DataAccess.FetchRS(CommandType.Text, ssqlWorkflow, null);
                    WorkflowSupervisor();
                    gridWorkflowSup.Rebind();
                #endregion

                #region (Clear -some one who is supervisor for him)
                    string sql = "update employee set pay_supervisor=0 where emp_code='" + emp_code + "'"
                                + "update employee set emp_clsupervisor=0 where emp_code='" + emp_code + "'"
                                + "update employee set timesupervisor=0 where emp_code='" + emp_code + "'"
                                + "update employee set emp_supervisor=0 where emp_code='" + emp_code + "'";
                    DataAccess.FetchRS(CommandType.Text, sql, null);
                #endregion

                #region Update Termination Date in Employee Table
                    string Sql_Tdate = "update Employee set termination_date=CONVERT(DATETIME, '" + terminationDate + "', 103) where emp_code='" + emp_code + "'";
                    DataAccess.FetchRS(CommandType.Text, Sql_Tdate, null);
                #endregion


                Response.Write("<script language=javascript> window.close();  </script>");
                Response.End();
            }
            catch (Exception)
            {
                throw;
            }
             
            }
        int i = 0;
        private string GetEmployeeTerminationInfo()
        {
            string sql=" select EMP_SUPERVISOR as LeaveSupervisor,EMP_CLSUPERVISOR as ClaimSupervisor,timesupervisor as Timesheetsupervisor,"
            + "(select  Emp_ID from employeeassignedtopayrollgroup where payrollgroupid=(select PayRollGroupID from EmployeeWorkFlowLevel where id=(select pay_supervisor from employee where emp_code=E.emp_code))) WorkflowSupervisor"
            +" from employee E where emp_code='"+emp_code+"'";

            DataSet ds = new DataSet();
            ds = GetDataSet(sql);

            StringBuilder msgtxt=new StringBuilder();

            // For each table in the DataSet, print the row values.
          
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        //Response.Write(row[column]);
                        if (row[column].ToString()!="")
                        {
                            if (Convert.ToInt32(row[column]) > 0)
                            {
                                i = i + 1;
                                if (i == 1)
                                {
                                    msgtxt.AppendFormat(Environment.NewLine + System.DateTime.Now + ": Before terminating Unassigned  ");
                                }
                                msgtxt.Append(column);
                                msgtxt.Append(",");
                            }
                            
                        }
                      
                    }
                }

                if (msgtxt.Length > 0)
                {
                    msgtxt.Remove(msgtxt.Length - 1, 1);// remove last comma
                    msgtxt.Append(".");
                }
             

            //He is supervisor for following Employee.
            string sql_sup = "select pay_supervisor,timesupervisor,emp_clsupervisor,emp_supervisor,emp_code  from employee where pay_supervisor='" + emp_code + "' OR  timesupervisor='" + emp_code + "' OR emp_clsupervisor='" + emp_code + "' OR emp_supervisor='" + emp_code + "'";
            SqlDataReader dr_sup = DataAccess.ExecuteReader(CommandType.Text, sql_sup, null);
            if (dr_sup.HasRows)
            {
                msgtxt.Append("Removed Employee who is under his Supervision.");
            }

           return msgtxt.ToString();
        }


        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
         

    }
}