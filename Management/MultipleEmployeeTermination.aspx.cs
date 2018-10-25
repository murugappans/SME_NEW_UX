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
using System.Threading;

namespace SMEPayroll.Management
{
    public partial class MultipleEmployeeTermination : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        protected int comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            comp_id = Utility.ToInteger(Session["Compid"]);
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();

            if (!IsPostBack)
            {
                string ssqld = "Delete from [Temp_Emp]";
                DataAccess.FetchRS(CommandType.Text, ssqld, null);
            }
         
        }

        
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Project Assignment")) == false)
            //{
            //    RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //    RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
            //}
        }
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
           
        }
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        private void DisplayMessage(string text)
        {
            RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid2.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid2.DataBind();
        }
        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            string strAction = ((Button)sender).Text;
            RadGrid rd = new RadGrid();
            if (strAction == "Assign")
              
            {
                int c = 0;
                //loop the grid and insert in temp table
                foreach (Telerik.Web.UI.GridItem item in RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        
                        if (chkBox.Checked == true)
                        {
                            c = c + 1;
                            int empid = Utility.ToInteger(this.RadGrid2.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                            string Sql_check = "Select * from Temp_Emp where Emp_code='" + empid + "'";
                            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, Sql_check, null); 
                            if (!dr.HasRows)
                            {
                                string ssqlb = "INSERT INTO [dbo].[Temp_Emp]([Emp_code], [Company_id])  VALUES('" + empid + "','" + comp_id + "')";
                                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                               

                            }
                        }
                        
                    }
                }
                if (c == 0)
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please select the employee...";
                }
                else
                {
                    lblError.Text = "";  
                }
                //
                
               
            }
            if (strAction == "Un-Assign")
            {
                int c = 0;
                //loop the grid and insert in temp table
                foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                       
                        if (chkBox.Checked == true)
                        {
                            c = c + 1;
                            int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                            string ssqld = "Delete from [Temp_Emp] where Emp_code='" + empid + "'";
                            DataAccess.FetchRS(CommandType.Text, ssqld, null);
                        }
                    }
                }
                if (c == 0)
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please select the employee...";
                }
                else
                {
                    lblError.Text = "";                      
                    
                }
            }
            SqlDataSource1.SelectCommand = "Select Time_Card_No, TE.Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID='" + comp_id + "'  Order By Emp_Name";
            SqlDataSource2.SelectCommand = "Select Time_Card_No, Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From Employee where StatusID=1 And Termination_Date is null And Company_ID='" + comp_id + "' AND Emp_Code NOT IN (select EMP_code from [Temp_Emp] where Company_ID='" + comp_id + "' ) Order By Emp_Name";
            RadGrid1.Rebind();
            RadGrid2.Rebind();
            
            
                  
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void btnTerminate_Click(object sender, EventArgs e)
        {
            string Sql_check = "Select * from Temp_Emp";
            int countemp = 0;
            int effectrec = 0;
            int count_effrect = 0;
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, Sql_check, null);
            if (dr.HasRows)
            {
                if (rdStart.SelectedDate.HasValue)
                {
                    string terdate = Convert.ToDateTime(rdStart.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    //--------------murugan
                    foreach (Telerik.Web.UI.GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                countemp = countemp + 1;
                                int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_Code"));
                                bool status = RemoveEmployeeSupervisor(empid.ToString() , comp_id);
                                if (status)
                                {
                                    #region Update Termination Date in Employee Table
                                   // string Sql_Tdate = "update Employee set termination_date=CONVERT(DATETIME, '" + terdate + "', 103) , StatusId =2 where emp_code='" + empid.ToString() + "'";
                                    string Sql_Tdate = "update Employee set termination_date=CONVERT(DATETIME, '" + terdate + "', 103) , StatusId =2 where emp_code='" + empid.ToString() + "' and joining_date<=CONVERT(DATETIME, '" + terdate + "', 103)";
                                   // DataAccess.FetchRS(CommandType.Text, Sql_Tdate, null);
                                    effectrec= DataAccess.ExecuteNonQuery(Sql_Tdate, null);
                                    #endregion
                                    lblError.ForeColor = System.Drawing.Color.Green;
                                   // lblError.Text = "Resigned Sucessfully";
                                    ViewState["actionMessage"] = "Success| Terminated/Resigned Sucessfully";


                                    #region remove the terminated Employee from grid
                                    if (effectrec > 0)
                                    {
                                        string ssqld = "Delete from [Temp_Emp] where Emp_code='" + empid.ToString() + "'";
                                        DataAccess.FetchRS(CommandType.Text, ssqld, null);
                                        count_effrect = count_effrect + 1;
                                    }
                                    else
                                    {

                                    }
                                    //SqlDataSource1.SelectCommand = "Select Time_Card_No, TE.Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID='" + comp_id + "'  Order By Emp_Name";
                                    //RadGrid1.Rebind();
                                    #endregion

                                }
                            }
                        }
                    }
                    if (countemp == 0)
                    {
                        lblError.ForeColor = System.Drawing.Color.Red ;
                        lblError.Text = "Please Select Assigned Employee.. ";
                    }
                    else if (countemp >count_effrect)
                    {
                        lblError.ForeColor = System.Drawing.Color.Red;
                        lblError.Text = count_effrect + " Employee(s) Not Terminated/Resigned due to Wrong Resigning-Date";
                        SqlDataSource1.SelectCommand = "Select Time_Card_No, TE.Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID='" + comp_id + "'  Order By Emp_Name";
                        RadGrid1.Rebind();
                    }


                    else
                    {
                        SqlDataSource1.SelectCommand = "Select Time_Card_No, TE.Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID='" + comp_id + "'  Order By Emp_Name";
                        RadGrid1.Rebind();
                    }
                    //-----------
                    //validate Employee is supervisor
                    //DataSet ds_CPF = new DataSet();
                    //ds_CPF = getDataSet("select [Emp_code] From Temp_Emp where company_id=" + comp_id);

                    // DataTable dTable = ds_CPF.Tables[0];
                    // foreach (DataRow dRow in dTable.Rows)
                    // {
                    //     bool status = RemoveEmployeeSupervisor(dRow["Emp_code"].ToString(), comp_id);
                    //     if (status)
                    //     {
                    //         #region Update Termination Date in Employee Table
                    //         string Sql_Tdate = "update Employee set termination_date=CONVERT(DATETIME, '" + terdate + "', 103) , StatusId =1 where emp_code='" + dRow["Emp_code"].ToString() + "'";
                    //             DataAccess.FetchRS(CommandType.Text, Sql_Tdate, null);
                    //         #endregion
                    //           lblError.Text = "Terminated Sucessfully";

                    //           #region remove the terminated Employee from grid
                    //               string ssqld = "Delete from [Temp_Emp]";
                    //               DataAccess.FetchRS(CommandType.Text, ssqld, null);
                    //               SqlDataSource1.SelectCommand = "Select Time_Card_No, TE.Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID='" + comp_id + "'  Order By Emp_Name";
                    //               RadGrid1.Rebind();
                    //           #endregion
                          
                    //     }
                    // }
                }
                else
                {
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Text = "Please Enter Terminate Date";
                }
            }
            else
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = "Please Assign employee to Terminate";
            }
        }

        private bool RemoveEmployeeSupervisor(string emp_code, object compid)
        {
            bool status;
            try
            {
                #region removing Leave supervisor for  employee
                string ssqlb = "update EMPLOYEE set EMP_SUPERVISOR=0 where EMP_SUPERVISOR ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                #endregion

                #region removing Claim supervisor for  employee
                string ssqlclaim = "update EMPLOYEE set EMP_CLSUPERVISOR=0 where EMP_CLSUPERVISOR ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                DataAccess.FetchRS(CommandType.Text, ssqlclaim, null);
                #endregion

                #region removing Timesheet supervisor for  employee
                string ssqlTimesheet = "update EMPLOYEE set timesupervisor=0 where timesupervisor ='" + emp_code + "' AND TERMINATION_DATE IS NULL AND COMPANY_ID ='" + comp_id + "'  ";
                DataAccess.FetchRS(CommandType.Text, ssqlTimesheet, null);
                #endregion

                #region removing Workflow supervisor for  employee
                string ssqlWorkflow = "update employee set pay_supervisor=0 where emp_code in (select Emp_ID from employeeassignedtopayrollgroup where payrollgroupid=(select PayRollGroupID from EmployeeWorkFlowLevel where id=(select pay_supervisor from employee where emp_code='" + emp_code + "' )))";
                DataAccess.FetchRS(CommandType.Text, ssqlWorkflow, null);
                #endregion

                #region (Clear -some one who is supervisor for him)
                string sql = "update employee set pay_supervisor=0 where emp_code='" + emp_code + "'"
                            + "update employee set emp_clsupervisor=0 where emp_code='" + emp_code + "'"
                            + "update employee set timesupervisor=0 where emp_code='" + emp_code + "'"
                            + "update employee set emp_supervisor=0 where emp_code='" + emp_code + "'";
                DataAccess.FetchRS(CommandType.Text, sql, null);
                #endregion

             

                status = true;
            }
            catch(Exception)
            {
                status = false;
            }
            return status;
        }



    }
}
