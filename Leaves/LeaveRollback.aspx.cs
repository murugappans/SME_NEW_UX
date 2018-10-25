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

namespace SMEPayroll.Leaves
{
    public partial class LeaveRollback : System.Web.UI.Page
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
                Button2.Click += new EventHandler(Button2_Click);
            }
            double previousyear = Convert.ToDouble(cmbYear.SelectedValue) - 1;
            string currentyear = cmbYear.SelectedValue;

            lblCurrYr.Text = "* Current Year :" + currentyear;
            lblPY.Text = "* Last Year        :" + previousyear.ToString(); 
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

        protected void bindgrid(object sender, EventArgs e)
        {
            if (Session["Leave_Model"].ToString() == "9" || Session["Leave_Model"].ToString() == "10")
            {
                Hybrid_Leave_Binding(); 
            }


            RadGrid1.DataBind();
        }

        protected void Hybrid_Leave_Binding()
        {
            try
            {
                compid = Utility.ToInteger(Session["Compid"].ToString());

                DataSet ds_Emp = new DataSet();
                string strEmpHybrid = "";
                int lastYear = 0;
                lastYear = int.Parse(cmbYear.SelectedValue.ToString()) - 1;

                strEmpHybrid = " select DISTINCT e.emp_code, e.emp_name, el.leaves_allowed [leaves_allowed_CY], el.LY_Leaves_Bal [Leaves_CF_LY1], lf.leave_forefited [leaves_elapsed], " +
                             " la.LeavesAvailable [leaves_allowed_LY], la.LY_Leaves_Bal [leaves_CF_LY] " +
                             " from Employee e Inner Join LeavesAllowedInYears l on l.emp_id=e.emp_code " +
                             " Inner Join EmployeeLeavesAllowed el on el.emp_id=e.emp_code " +
                             " Inner Join leaves_forefited lf on lf.emp_id=e.emp_code " +
                             " Inner Join LeavesAllowedInYears la on la.emp_id = e.emp_code " +
                             " where e.emp_group_id=" + cmbEmpgroup.SelectedValue + " and e.Company_Id = " + compid + " " +
                             " and el.leave_year=" + cmbYear.SelectedValue + " and el.leave_type=8 " +
                             " and lf.leave_year = " + lastYear + " and la.Years = " + lastYear + " ";

                ds_Emp = DataAccess.FetchRS(CommandType.Text, strEmpHybrid, null);

                string SQLcheckLeave = "";
                DataSet dsEmp_Leave = new DataSet();
                string emp_code = "";
                for (int i = 0; i < ds_Emp.Tables[0].Rows.Count; i++)
                {
                    if (emp_code == "")
                    {
                        emp_code = ds_Emp.Tables[0].Rows[i]["emp_code"].ToString();
                    }
                    else
                    {
                        emp_code = emp_code + "," + ds_Emp.Tables[0].Rows[i]["emp_code"].ToString();
                    }                 
           
                }
                SQLcheckLeave = "SELECT * FROM [emp_leaves] where emp_id IN (" + emp_code + ") and leave_type=8 and Year([start_date]) = " + int.Parse(cmbYear.SelectedValue.ToString()) + " and [status] <> 'Rejected'";
                dsEmp_Leave = DataAccess.FetchRS(CommandType.Text, SQLcheckLeave, null);

                for (int i = ds_Emp.Tables[0].Rows.Count - 1; i >= 0; i--)
                {
                    DataRow[] drEmpLeave;

                    DataRow dr11 = ds_Emp.Tables[0].Rows[i];

                    drEmpLeave = dsEmp_Leave.Tables[0].Select("emp_id=" + int.Parse(ds_Emp.Tables[0].Rows[i]["Emp_code"].ToString()) + "");
                    if (drEmpLeave.Length > 0)
                    {
                        dr11.Delete();
                    }
                    
                }

                RadGrid1.DataSourceID = "";
                RadGrid1.DataSource = ds_Emp.Tables[0];
                RadGrid1.DataBind();
            }
            catch (Exception ex) { }
        }

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
            string ssql1 = "";
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
                        double LeavesAllowCy = Utility.ToDouble(dataItem.Cells[5 + i].Text);
                        double LeavesAllowLy = Utility.ToDouble(dataItem.Cells[8+ i].Text);

                        double leave_allowed = 0.0;
                        double LY_Leaves_Bal = 0.0;

                        DataSet ds1 = new DataSet();
                        DataSet ds2 = new DataSet();

                        string sql8 ="SELECT LeavesAvailable,LY_Leaves_Bal from LeavesAllowedInYears WHERE emp_id=" + empcode + " AND Years=" + previousyear + "AND LeaveType IN(1,7)";

                        if (Session["Leave_Model"].ToString() == "9")
                        {
                            sql8 = "SELECT LeavesAvailable,LY_Leaves_Bal from LeavesAllowedInYears WHERE emp_id=" + empcode + " AND Years=" + previousyear + "AND LeaveType IN(1,7,9)";
                        }
                        

                        string sql9="SELECT leaves_allowed FROM leaves_allowed where group_id="+cmbEmpgroup.SelectedValue +" and leave_year= " + previousyear +" and leave_type=8";

                        ds1 = DataAccess.FetchRS(CommandType.Text, sql8);
                        ds2 = DataAccess.FetchRS(CommandType.Text, sql9);


                        double dblLeaveavailable=0; //= ds1.Tables[0].Rows[0][0];
                        double dblLeavesallowed =0;//= ds2.Tables[0].Rows[0][0];
                        if (ds1.Tables.Count>0)
                        {
                            if (ds1.Tables[0].Rows.Count>0)
                            {
                                if (ds1.Tables[0].Rows[0][0].ToString() != "")
                                {
                                    leave_allowed = Convert.ToDouble(ds1.Tables[0].Rows[0][0]);
                                    
                                }

                                if (ds1.Tables[0].Rows[0][1].ToString() != "")
                                {
                                    LY_Leaves_Bal = Convert.ToDouble(ds1.Tables[0].Rows[0][1]);
                                    //LY_Leaves_Bal = dblLeaveavailable - dblLeavesallowed;
                                    //leave_allowed = dblLeavesallowed;
                                }

                            }                        
                        }
                        //if (ds2.Tables.Count > 0)
                        //{
                        //    if (ds2.Tables[0].Rows.Count>0)
                        //    {
                        //        if (ds2.Tables[0].Rows[0][0].ToString() != "")
                        //        {
                        //            dblLeavesallowed = Convert.ToDouble(ds2.Tables[0].Rows[0][0]);
                        //        }
                        //    }
                        //}
                        //if (dblLeaveavailable > dblLeavesallowed)
                        //{
                        //    LY_Leaves_Bal = dblLeaveavailable - dblLeavesallowed;
                        //    leave_allowed = dblLeavesallowed;
                        //}
                        //else
                        //{
                        //    LY_Leaves_Bal = 0;
                        //    leave_allowed = dblLeavesallowed;
                        //}
                        

                        //DELETE FROM LeavesAllowedInYears  where emp_id='1045' and YEARS='2010' And LeaveType=1 
                        //DELETE   FROM EmployeeLeavesAllowed WHERE emp_id=1045 AND leave_year=2011 AND Leave_Type=8

                        //INSERT INTO EmployeeLeavesAllowed (emp_id,leave_type,Leaves_allowed,leave_year,LY_Leaves_Bal)
                        //(SELECT emp_id,8,leave_allowed,leave_year,NULL  FROM leaves_forefited WHERE emp_id=1045 and leave_year=2010)

                        //DELETE  FROM leaves_forefited  WHERE emp_id=1045 and leave_year=2010
                        
                        leavesallowed = 1;
                        try
                        {
                            int intmsg = 0;
                            if (leavesallowed > 0)
                            {
                                intmsg = intmsg + 1;
                               // ssql1 = "DELETE FROM LeavesAllowedInYears  where   LeaveType=1  AND emp_id='" + empcode + "' and YEARS='" + previousyear + "'";
                                ssql1 = "DELETE FROM LeavesAllowedInYears  where emp_id='" + empcode + "' and YEARS='" + previousyear + "'";
                                DataAccess.FetchRS(CommandType.Text, ssql1);


                                if (Session["Leave_Model"].ToString() == "9")
                                {
                                    ssql1 = "UPDATE EmployeeLeavesAllowed SET LY_Leaves_Bal= -1 where Leave_Type=8 AND  emp_id=" + empcode + " and leave_year=" + currentyear;
                                    DataAccess.FetchRS(CommandType.Text, ssql1);
                                }
                                else
                                {
                                    ssql1 = "DELETE   FROM EmployeeLeavesAllowed WHERE Leave_Type=8 AND  emp_id=" + empcode + " and leave_year=" + currentyear;
                                    DataAccess.FetchRS(CommandType.Text, ssql1);
                                }
                                

                                //ssql1 = "delete from EmployeeLeavesAllowed where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8";
                                ssql1 = "INSERT INTO EmployeeLeavesAllowed (emp_id,leave_type,Leaves_allowed,leave_year,LY_Leaves_Bal)VALUES";
                                ssql1 = ssql1 + "(" + empcode + ",8," + leave_allowed + "," + previousyear + "," + LY_Leaves_Bal + ")"; 
                                DataAccess.FetchRS(CommandType.Text, ssql1);

                                ssql1 = "DELETE FROM leaves_forefited  WHERE emp_id="+ empcode +" and leave_year=" + previousyear;
                                DataAccess.FetchRS(CommandType.Text, ssql1);
                            }
                            var _actionMessage = "";
                            lblmsg.Visible = true;
                            if (intmsg > 0)
                            {
                                lblmsg.Text = "Leave Rollback Successfully.";
                                _actionMessage = "Success|Leave Rollback Successfully.";

                            }
                            else
                            {
                                lblmsg.Text = "Leave cannot be Rollback.";
                                _actionMessage = "Leave cannot be Rollback.";

                            }
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        catch (Exception ex)
                        {
                            string ErrMsg = ex.Message;
                            if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                            {
                                ErrMsg = "<font color = 'Red'>Unable to Rollback the Leave.Please Try again!</font>";
                            }
                        }
                    }

                }
            }

            if (Session["Leave_Model"].ToString() == "9" || Session["Leave_Model"].ToString() == "10")
            {
                Hybrid_Leave_Binding();
            }

            RadGrid1.DataBind();
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
                    //chkBox.Enabled = false;
                }
                else
                {
                    //chkBox.Enabled = true;
                }
            }
        }
    }
}
