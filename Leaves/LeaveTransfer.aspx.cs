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
using System.Text;

namespace SMEPayroll.Leaves
{
    public partial class LeaveTransfer : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string strdate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            if (Session["Leave_Model"].ToString() == "9" || Session["Leave_Model"].ToString() == "10")
            {
                //SqlDataSource2.SelectCommand = "sp_trans_leave_Hybrid";
                //SqlDataSource2.SelectCommand = "sp_trans_leave";
            }
            else
            {
                SqlDataSource2.SelectCommand = "sp_trans_leave";
            }
            //



            rdTrdate.Enabled = false;
            if (Session["Leave_Model"].ToString() == "3" || Session["Leave_Model"].ToString() == "4" || Session["Leave_Model"].ToString() == "6" || Session["Leave_Model"].ToString() == "8")
            {
                Response.Redirect("LeaveTransferYOS.aspx");
                //rdTrdate.Enabled = true;
            }
            else
            {
                strdate = "31/12/" + cmbYear.SelectedItem.Value.ToString();
                rdTrdate.DbSelectedDate = Convert.ToDateTime(strdate);
            }


            compid = Utility.ToInteger(Session["Compid"].ToString());
            lblmsg.Visible = false;
            Button2.Visible = true;
            RadGrid1.Visible = true;

            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                rdTrdate.SelectedDate = System.DateTime.Now;
                string sSQL = "select distinct id,empgroupname from emp_group where company_id={0} order by empgroupname";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                cmbEmpgroup.Items.Clear();
                cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                while (dr.Read())
                {
                    cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }
                cmbEmpgroup.SelectedValue = "";
                cmbYear.SelectedValue = System.DateTime.Today.Year.ToString();
            }
            else
            {
                string L_Model = Session["Leave_Model"].ToString();
                if (Convert.ToInt32(Session["Leave_Model"]) != 9  )//check if leave model is hybrid 
                {
                    if (!L_Model.Equals("10"))
                    {
                        DataSet ds = new DataSet();
                        ds = getDataSet("select id from leaves_allowed where leave_type=8 and leave_year='" + cmbYear.SelectedValue.ToString() + "' AND Group_id=" + cmbEmpgroup.SelectedValue);
                        if (ds.Tables[0].Rows.Count <= 0)
                        {
                            lblmsg.Visible = true;
                            //lblmsg.Text = "Please set the Leave Allowed for the Annual Leave";
                            ViewState["actionMessage"]= "Warning|Please set the Leave Allowed for the Annual Leave";
                            RadGrid1.Visible = false;
                            Button2.Visible = false;
                        }
                    }
                    
                }
            }
            CreateTable();
        }

        private object _dataItem = null;

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

        private void CreateTable()
        {
            try {
                DataTable dt = new DataTable();
                dt.Columns.Add("emp_code");
                dt.Columns.Add("emp_name");
                dt.Columns.Add("lastyearleaves");
                dt.Columns.Add("company_policy");
                dt.Columns.Add("TotalLastYrLeaves");
                dt.Columns.Add("CurrentYear");
                dt.Columns.Add("TotalAvailLeaves");
                Session["dtLeaves"] = dt;

            }
            catch (Exception ex) { }            
        }

        protected void bindgrid(object sender, EventArgs e)
        {
            if (Session["Leave_Model"].ToString() == "9" || Session["Leave_Model"].ToString() == "10")
            {
                Hybrid_Leave_Binding();
            }
            else
            {
                RadGrid1.DataBind();
            }
            
        }

        protected void Hybrid_Leave_Binding()
        {
            string strEmpSelect = "";

            DataSet ds_Emp = new DataSet();

            DataTable dtLeaves = new DataTable();

            DataRow dr;

            dtLeaves = (DataTable)Session["dtLeaves"];

            compid = Utility.ToInteger(Session["Compid"].ToString());

            strEmpSelect = " Select DISTINCT e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
                           " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
                           " where e.Company_Id = " + compid + " and e.termination_date is null and el.leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + " and Leave_Type=8 and LY_Leaves_Bal = -1 and  emp_group_id= " + int.Parse(cmbEmpgroup.SelectedValue) + " AND termination_date is null";
            ds_Emp = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);

            for (int i = 0; i < ds_Emp.Tables[0].Rows.Count; i++) //murugan 
            {
                try
                {
                    string applyondate = "31/12/" + Convert.ToString(int.Parse(cmbYear.SelectedValue) - 1);
                    DataSet ds_LeaveInfo = new DataSet();
                    int emp_code = Convert.ToInt32(ds_Emp.Tables[0].Rows[i]["emp_code"].ToString());
                    string sSQL = "sp_GetEmployeeLeavePolicy";
                    SqlParameter[] parms = new SqlParameter[4];
                    parms[0] = new SqlParameter("@empid", emp_code);
                    parms[1] = new SqlParameter("@year", Utility.ToString(int.Parse(cmbYear.SelectedValue) - 1));
                    parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(applyondate));
                    parms[3] = new SqlParameter("@filter", -1);
                    ds_LeaveInfo = DataAccess.ExecuteSPDataSet(sSQL, parms); //txtfwd
                    DataRow[] result = ds_LeaveInfo.Tables[0].Select("id=8");


                    string strEmpLAllow = "select * from EmployeeLeavesAllowed where emp_id=" + emp_code + " and leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + "  and Leave_Type=8";
                    DataSet ds_EmpLeaveAllow = new DataSet();
                    ds_EmpLeaveAllow = DataAccess.FetchRS(CommandType.Text, strEmpLAllow, null);


                    dr = dtLeaves.NewRow();
                    dr["emp_code"] = ds_Emp.Tables[0].Rows[i]["emp_code"].ToString();
                    dr["emp_name"] = ds_Emp.Tables[0].Rows[i]["emp_name"].ToString();
                    if (result.Length > 0)
                    {
                        //dr["lastyearleaves"] = "10";
                        dr["lastyearleaves"] = Convert.ToDouble( result[0]["actualleavesavailable"].ToString());
                    }
                    else
                    {
                        dr["lastyearleaves"] = 0;
                    }                   
                    
                    dr["company_policy"] = txtfwd.Text;

                    double totalLastYLeave = 0;
                    totalLastYLeave = Convert.ToDouble(dr["lastyearleaves"].ToString()) - Convert.ToDouble(dr["company_policy"].ToString());
                    if (totalLastYLeave >= 0)
                    {
                        dr["TotalLastYrLeaves"] = txtfwd.Text;
                    }
                    else
                    {
                        dr["TotalLastYrLeaves"] = dr["lastyearleaves"].ToString();
                    }
                    dr["CurrentYear"] = ds_EmpLeaveAllow.Tables[0].Rows[0]["Leaves_Allowed"].ToString();
                    dr["TotalAvailLeaves"] = Convert.ToDouble(dr["TotalLastYrLeaves"].ToString()) + Convert.ToDouble(dr["CurrentYear"].ToString());
                    dtLeaves.Rows.Add(dr);

                }
                catch (Exception ex)
                {

                }

            }
            RadGrid1.DataSourceID = "";
            RadGrid1.DataSource = dtLeaves;
            RadGrid1.DataBind();
        }

        int leave_model;
        protected void Button2_Click(object sender, EventArgs e)
        {
            double previousyear = Convert.ToDouble(cmbYear.SelectedValue) - 1;
            string currentyear = cmbYear.SelectedValue;



            double leavesallowed = 0;
            Button2.Visible = true;
            //string ssql9 = "select leaves_allowed from leaves_allowed where leave_year='" + currentyear + "' and leave_type=8 and group_id='" + cmbEmpgroup.SelectedItem.Value.ToString() + "'";
            //DataSet ds = new DataSet();
            //ds = DataAccess.FetchRS(CommandType.Text, ssql9, null);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    leavesallowed = Convert.ToInt16(ds.Tables[0].Rows[0]["leaves_allowed"]);
            //}

           

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int i = 1;
                        double empcode = Convert.ToDouble(dataItem.Cells[3 + i].Text);
                        double TotalLastYrLeaves = Utility.ToDouble(dataItem.Cells[7 + i].Text);
                        double intrem = Convert.ToDouble(dataItem.Cells[6].Text.ToString()) - Convert.ToDouble(dataItem.Cells[7].Text.ToString());
                        double CurrentYrLeaves = Utility.ToDouble(dataItem.Cells[8 + i].Text.ToString());
                        string RemLastYrLeaves = "";

                        string emp_name = "";
                        string strEmpName = "select emp_name from employee where emp_code=" + empcode + "";
                        DataSet dsEMP = new DataSet();
                        dsEMP = DataAccess.FetchRS(CommandType.Text, strEmpName, null);
                        emp_name = dsEMP.Tables[0].Rows[0][0].ToString();

                        leavesallowed = Convert.ToDouble(dataItem.Cells[10].Text.ToString());
                        if (intrem >= 0)
                        {
                            RemLastYrLeaves = Convert.ToString(intrem);
                        }
                        else
                        {
                            RemLastYrLeaves = "0";
                        }
                        double leavesallowed1 = 0;
                        string ssql1 = "";
                        string ssql2 = "";
                        string ssql3 = "";

                        //hybrid leave transfer
                        #region  check whether it is Hybrid leave model

                        string ssqlhybrid = "select leave_model from Company where Company_Id='" + compid + "'";
                        DataSet ds_hybrid = new DataSet();
                        ds_hybrid = DataAccess.FetchRS(CommandType.Text, ssqlhybrid, null);
                        if (ds_hybrid.Tables[0].Rows.Count > 0)
                        {
                            leave_model = Convert.ToInt32(ds_hybrid.Tables[0].Rows[0]["leave_model"]);
                        }
                        #endregion
                        if (leave_model >= 9)
                        {
                            #region Hybrid(refered Sp_yos_update-stored procedure)

                            //string ssql_0 = "Delete From LeavesAllowedInYears where emp_id= '" + empcode + "' and Years= '" + cmbYear.SelectedValue + "'";
                            //DataAccess.FetchRS(CommandType.Text, ssql_0);

                            //string ssql_1 = "Insert Into LeavesAllowedInYears (Years,Emp_ID,LeavesAvailable, LY_Leaves_Bal,LeaveType) Select Top 1 Actual_YOS, Emp_ID, LeavesAllowed, LY_Leaves_Bal,'" + leave_model + "' From YOSLeavesAllowed Where Emp_ID = '" + empcode + "' Order By Actual_YOS";
                            //DataAccess.FetchRS(CommandType.Text, ssql_1);

                            //string ssql_hyb1 = "Delete From YOSLeavesAllowed Where ID In  (Select Top 1 ID From YOSLeavesAllowed Where Emp_ID = '" + empcode + "'  And YEAR(enddate)='" + cmbYear.SelectedValue + "')";
                            //DataAccess.FetchRS(CommandType.Text, ssql_hyb1);

                            //string ssql_hyb2 = "Update YOSLeavesAllowed Set YOS=1, LeavesAllowed='" + CurrentYrLeaves + "', LY_Leaves_Bal='" + TotalLastYrLeaves + "' Where Emp_ID='" + empcode + "'";
                            //DataAccess.FetchRS(CommandType.Text, ssql_hyb2);

                            //string ssql_hyb3 = "Insert Into YOSLEavesAllowed (yosyear, emp_id, yos, actual_yos, startdate, enddate,  leavesallowed, CreatedDate, LY_Leaves_Bal)Select Year(EndDate), Emp_ID, 2, (Actual_YOS+1), Dateadd(yy,1,startdate),Dateadd(yy,1,enddate),0,getdate(),0 From YOSLeavesAllowed Where Emp_ID = '" + empcode + "'";
                            //DataAccess.FetchRS(CommandType.Text, ssql_hyb3);

                            //string ssql_hyb4 = "Insert Into YOSEmpTransfered values ('" + empcode + "', '" + cmbYear.SelectedValue + "', getdate(), 0)";
                            //DataAccess.FetchRS(CommandType.Text, ssql_hyb4);
                            
                            string ssql91 = "select (isnull(leaves_allowed,0)+ isnull(LY_Leaves_Bal,0)) leaves_allowed from EmployeeLeavesAllowed where leave_year='" + previousyear + "' and leave_type=8 And Emp_ID='" + empcode + "'";
                            DataSet ds1 = new DataSet();
                            ds1 = DataAccess.FetchRS(CommandType.Text, ssql91, null);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                leavesallowed1 = Convert.ToDouble(ds1.Tables[0].Rows[0]["leaves_allowed"]);
                            }

                            try
                            {
                                int intmsg = 0;

                                DataSet ds_leave = new DataSet();

                                string sSQL = "sp_GetEmployeeLeavePolicy";
                                SqlParameter[] parms = new SqlParameter[4];
                                parms[0] = new SqlParameter("@empid", Utility.ToInteger(empcode));
                                parms[1] = new SqlParameter("@year", Utility.ToString(previousyear));
                                parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(System.DateTime.Today.Date.ToShortDateString()));
                                parms[3] = new SqlParameter("@filter", -1);
                                ds_leave = DataAccess.ExecuteSPDataSet(sSQL, parms);

                                double pendingLeave = 0;

                                if (ds_leave.Tables[0].Rows.Count > 0)
                                {
                                    for (int l = 0; l < ds_leave.Tables[0].Rows.Count; l++)
                                    {
                                        if (ds_leave.Tables[0].Rows[l]["TYPE"].ToString().ToLower() == "annual leave")
                                        {
                                            pendingLeave = Convert.ToDouble(ds_leave.Tables[0].Rows[l]["pendingleaves"].ToString());
                                        }
                                    }
                                }


                                if (pendingLeave == 0)
                                {
                                    if (leavesallowed > 0)
                                    {
                                        intmsg = intmsg + 1;
                                        ssql1 = "delete from LeavesAllowedInYears where emp_id='" + empcode + "' and Years='" + previousyear + "'";
                                        DataAccess.FetchRS(CommandType.Text, ssql1);

                                        ssql2 = "Insert Into LeavesAllowedInYears (Years,Emp_ID,LeavesAvailable, LY_Leaves_Bal,LeaveType) " + 
                                                " (Select  Leave_Year,Emp_ID,Leaves_Allowed,LY_Leaves_Bal,'" + Session["Leave_Model"].ToString() + "' From EmployeeLeavesAllowed  where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8)";
                                        DataAccess.FetchRS(CommandType.Text, ssql2);

                                        ssql1 = "delete from EmployeeLeavesAllowed where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8";
                                        DataAccess.FetchRS(CommandType.Text, ssql1);

                                        ssql2 = "update EmployeeLeavesAllowed set LY_Leaves_Bal='" + TotalLastYrLeaves + "' where leave_type=8 and emp_id='" + empcode + "'";
                                        DataAccess.FetchRS(CommandType.Text, ssql2);
                                    }
                                    if (leavesallowed1 > 0)
                                    {
                                        intmsg = intmsg + 1;
                                        ssql3 = "insert into leaves_forefited (emp_id,leave_year,leave_forefited,leave_allowed,leave_forward) values " + 
                                                " ('" + empcode + "','" + previousyear + "','" + RemLastYrLeaves + "','" + leavesallowed1 + "','" + TotalLastYrLeaves + "')";
                                        DataAccess.FetchRS(CommandType.Text, ssql3);
                                    }
                                }
                                else
                                {
                                    ShowMessageBox(emp_name + " has pending leave(s) so cannot transfer leave.");
                                }


                                lblmsg.Visible = true;
                                var _actionMessage = "";
                                if (intmsg > 0)
                                {
                                    lblmsg.Text = "Leaves Transfered Successfully.";
                                   _actionMessage = "Success|Leaves Transfered Successfully.";
                                  
                                }
                                else
                                {
                                    lblmsg.Text = "Leaves cannot be transfered.";
                                    _actionMessage = "Warning|Leaves cannot be transfered.";
                                }
                                ViewState["actionMessage"] = _actionMessage;
                            }
                            catch (Exception ex)
                            {
                                string ErrMsg = ex.Message;
                                if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                {
                                    ErrMsg = "<font color = 'Red'>Unable to Transfer the Leaves.Please Try again!</font>";
                                }
                            }
                            

                            #endregion
                        }
                        else
                        {
                            #region Not linked with Hybrid Model
                            string ssql91 = "select (isnull(leaves_allowed,0)+ isnull(LY_Leaves_Bal,0)) leaves_allowed from EmployeeLeavesAllowed where leave_year='" + previousyear + "' and leave_type=8 And Emp_ID='" + empcode + "'";
                            DataSet ds1 = new DataSet();
                            ds1 = DataAccess.FetchRS(CommandType.Text, ssql91, null);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                leavesallowed1 = Convert.ToDouble(ds1.Tables[0].Rows[0]["leaves_allowed"]);
                            }

                            try
                            {
                                int intmsg = 0;

                                DataSet ds_leave = new DataSet();

                                string sSQL = "sp_GetEmployeeLeavePolicy";
                                SqlParameter[] parms = new SqlParameter[4];
                                parms[0] = new SqlParameter("@empid", Utility.ToInteger(empcode));
                                parms[1] = new SqlParameter("@year", Utility.ToString(previousyear));
                                parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(System.DateTime.Today.Date.ToShortDateString()));
                                parms[3] = new SqlParameter("@filter", -1);
                                ds_leave = DataAccess.ExecuteSPDataSet(sSQL, parms);

                                double pendingLeave =0;

                               if(ds_leave.Tables[0].Rows.Count >0)
                               {
                                   for (int l = 0; l < ds_leave.Tables[0].Rows.Count;l++ )
                                   {
                                       if (ds_leave.Tables[0].Rows[l]["TYPE"].ToString().ToLower() == "annual leave")
                                       {
                                           pendingLeave = Convert.ToDouble(ds_leave.Tables[0].Rows[l]["pendingleaves"].ToString());
                                       }
                                   }
                               }


                               if (pendingLeave == 0)
                               {
                                   if (leavesallowed > 0)
                                   {
                                       intmsg = intmsg + 1;
                                       ssql1 = "delete from LeavesAllowedInYears where emp_id='" + empcode + "' and Years='" + previousyear + "'";
                                       DataAccess.FetchRS(CommandType.Text, ssql1);

                                       ssql2 = "Insert Into LeavesAllowedInYears (Years,Emp_ID,LeavesAvailable, LY_Leaves_Bal,LeaveType)(Select  Leave_Year,Emp_ID,Leaves_Allowed,LY_Leaves_Bal,'" + Session["Leave_Model"].ToString() + "' From EmployeeLeavesAllowed  where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8)";
                                       DataAccess.FetchRS(CommandType.Text, ssql2);

                                       ssql1 = "delete from EmployeeLeavesAllowed where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8";
                                       DataAccess.FetchRS(CommandType.Text, ssql1);

                                       ssql2 = "insert into EmployeeLeavesAllowed (emp_id,leave_year,leave_type,leaves_allowed, LY_Leaves_Bal) values ('" + empcode + "','" + currentyear + "',8,'" + CurrentYrLeaves + "','" + TotalLastYrLeaves + "')";
                                       DataAccess.FetchRS(CommandType.Text, ssql2);
                                   }
                                   if (leavesallowed1 > 0)
                                   {
                                       intmsg = intmsg + 1;
                                       ssql3 = "insert into leaves_forefited (emp_id,leave_year,leave_forefited,leave_allowed,leave_forward) values ('" + empcode + "','" + previousyear + "','" + RemLastYrLeaves + "','" + leavesallowed1 + "','" + TotalLastYrLeaves + "')";
                                       DataAccess.FetchRS(CommandType.Text, ssql3);
                                   }
                               }
                               else
                               {
                                   ShowMessageBox(emp_name + " has pending leave(s) so cannot transfer leave.");
                               }
                                

                                lblmsg.Visible = true;
                                var _actionMessage = "";
                                if (intmsg > 0)
                                {
                                    lblmsg.Text = "Leaves Transfered Successfully.";
                                    _actionMessage = "Success|Leaves Transfered Successfully.";
                                }
                                else
                                {
                                    lblmsg.Text = "Leaves cannot be transfered.";
                                    _actionMessage = "Leaves cannot be transfered.";
                                }

                                ViewState["actionMessage"] = _actionMessage;
                            }
                            catch (Exception ex)
                            {
                                string ErrMsg = ex.Message;
                                if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                {
                                    ErrMsg = "<font color = 'Red'>Unable to Transfer the Leaves.Please Try again!</font>";
                                }
                            }
                            #endregion
                        }

                    }

                }
            }
            if (leave_model >= 9)
            {
                Hybrid_Leave_Binding();
            }
            else
            {
                RadGrid1.DataBind();
            }
            
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                int empid = Convert.ToInt16(dataItem.Cells[4].Text);

                string ssql = "select leave_year from EmployeeLeavesAllowed where leave_year='" + cmbYear.SelectedValue + "' and emp_id='" + empid + "' And Leave_Type=8";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, ssql, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // chkBox.Enabled = false;
                }
                else
                {
                    //chkBox.Enabled = true;
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

    }
}
