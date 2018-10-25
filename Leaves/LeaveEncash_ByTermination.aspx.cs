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
using System.Threading;

namespace SMEPayroll.Leaves
{
    public partial class LeaveEncash_ByTermination : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string _actionMessage = "";
        int compid, month;
        string strdate;
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";


            compid = Utility.ToInteger(Session["Compid"].ToString());
            lblmsg.Visible = false;
            Button2.Visible = true;
            RadGrid1.Visible = true;

            //SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            //SqlDataSource2.SelectCommand = "sp_trans_leave";
           
            if (!IsPostBack)
            {
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text, "SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC");
                cmbYear.DataTextField = "id";
                cmbYear.DataValueField = "id";
                cmbYear.DataSource = ds;
                cmbYear.DataBind();

                cmbYear.SelectedValue = System.DateTime.Today.Year.ToString();

                MonthFill();
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
            //else
            //{
            //    string L_Model = Session["Leave_Model"].ToString();
            //    if (Convert.ToInt32(Session["Leave_Model"]) != 9  )//check if leave model is hybrid 
            //    {
            //        if (!L_Model.Equals("10"))
            //        {
            //            DataSet ds = new DataSet();
            //            ds = getDataSet("select id from leaves_allowed where leave_type=8 and leave_year='" + cmbYear.SelectedValue.ToString() + "' AND Group_id=" + cmbEmpgroup.SelectedValue);
            //            if (ds.Tables[0].Rows.Count <= 0)
            //            {
            //                lblmsg.Visible = true;
            //                lblmsg.Text = "Please set the Leave Allowed for the Annual Leave";
            //                RadGrid1.Visible = false;
            //                Button2.Visible = false;
            //            }
            //        }

            //    }
            //}
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
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("emp_code");
                dt.Columns.Add("emp_name");
                dt.Columns.Add("LeaveAvailble");
                dt.Columns.Add("LeaveEncash");
                dt.Columns.Add("LeavesAfterTranfer");
                dt.Columns.Add("Formula");
                dt.Columns.Add("Amount");
                Session["dtLeaves"] = dt;

            }
            catch (Exception ex) { }
        }

        DataTable dtFilterFound;
        DataSet monthDs;
        DataRow[] foundRows;


        void CallBeforeMonthFill()
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            Session["monthDs"] = monthDs;
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();
        }
        void MonthFill()
        {
            CallBeforeMonthFill();
            if (Session["ROWID"] == null)
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            else
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            // SetControlDate();
        }

        void SetControlDate()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            monthDs = (DataSet)Session["monthDs"];
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                Session["IsDateCalculation"] = "1";

                month = Convert.ToInt32(dr["month"].ToString());
                //if (strEmpvisible != "")
                //{
                //    Session["EmpPassID"] = strEmpvisible;
                //}
                //else
                //{
                //    Session["EmpPassID"] = "";
                //}
            }

            //+ "&dept_id=" + deptID.SelectedValue.ToString()
        }





        protected void bindgrid(object sender, EventArgs e)
        {
            //if (Session["Leave_Model"].ToString() == "9" || Session["Leave_Model"].ToString() == "10")
            //{
            //    Hybrid_Leave_Binding();
            //}
            //else
            //{
            //    RadGrid1.DataBind();
            //}
            Hybrid_Leave_Binding();
        }
        //protected void cmbmonthchange(object sender, EventArgs e)
        //    {
        //        SetControlDate();
        //    }

        private void isFormulaAssigned()
        {
            //if(cmbEmpgroup.SelectedValue == "")
            //{
            //    ShowMessageBox("please select group");
            //}

            DataSet ds_Assign = new DataSet();


            string strEmpSelect = @" select * from( Select DISTINCT e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
                           " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
                           " where e.Company_Id = " + compid + " and e.termination_date is not null and year(e.termination_date )= " + Convert.ToInt32(cmbYear.SelectedValue) + " and month(e.termination_date)=" + month + " and el.leave_year="
                           + Convert.ToInt32(cmbYear.SelectedValue)
                           + " and Leave_Type=8 AND termination_date is  not null)A where not EXISTS (select * from Encashment b where a.emp_code=b.EmpCode)";



            ds_Assign = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);


            if (ds_Assign.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("../Leaves/FormulaAssign.aspx?compid=" + compid + "&year=" + Convert.ToInt32(cmbYear.SelectedValue) + "&month=" + month + "&group=-1&encashtype=1");
            }

        }




        protected void Hybrid_Leave_Binding()
        {

            SetControlDate();

            isFormulaAssigned();

            string strEmpSelect = "";

            DataSet ds_Emp = new DataSet();

            DataSet LeaveEncash = new DataSet();

            DataTable dtLeaves = new DataTable();

            DataRow dr;

            dtLeaves = (DataTable)Session["dtLeaves"];

            compid = Utility.ToInteger(Session["Compid"].ToString());



            //strEmpSelect = " Select DISTINCT e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
            //               " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
            //               " where e.Company_Id = " + compid + " and e.termination_date is  not null and el.leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + " and Leave_Type=8 AND termination_date is null and e.emp_group_id= " + cmbEmpgroup.SelectedValue.ToString();

            strEmpSelect = " Select DISTINCT e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
                           " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
                           " where e.Company_Id = " + compid + " and e.termination_date is not null and year(e.termination_date )= " + Convert.ToInt32(cmbYear.SelectedValue) + " and month(e.termination_date)=" + month + " and el.leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + " and Leave_Type=8 AND termination_date is  not null ";



            ds_Emp = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);



            for (int i = 0; i < ds_Emp.Tables[0].Rows.Count; i++)
            {
                double leaveAvilable = 0.0;
                double leaveencash = 0.0;
                double leaveafterEncash = 0.0;
                double actualleaveEncash = 0.0;

                try
                {


                    DateTime dt = DateTime.Now;
                    bool k = DateTime.TryParse(Session["PaySubEndDate"].ToString(), out dt);
                    //  string applyondate = "31/12/" + Convert.ToString(int.Parse(cmbYear.SelectedValue));

                    string applyondate = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + Convert.ToString(int.Parse(cmbYear.SelectedValue));
                    DataSet ds_LeaveInfo = new DataSet();
                    int emp_code = Convert.ToInt32(ds_Emp.Tables[0].Rows[i]["emp_code"].ToString());
                    string sSQL = "sp_GetEmployeeLeavePolicy";
                    SqlParameter[] parms = new SqlParameter[4];
                    parms[0] = new SqlParameter("@empid", emp_code);
                    parms[1] = new SqlParameter("@year", Utility.ToString(int.Parse(cmbYear.SelectedValue)));
                    parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(applyondate));
                    parms[3] = new SqlParameter("@filter", -1);
                    ds_LeaveInfo = DataAccess.ExecuteSPDataSet(sSQL, parms); //txtfwd
                    DataRow[] result = ds_LeaveInfo.Tables[0].Select("id=8");


                    //string strEmpLAllow = "select * from EmployeeLeavesAllowed where emp_id=" + emp_code + " and leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + "  and Leave_Type=8";
                    //DataSet ds_EmpLeaveAllow = new DataSet();
                    //ds_EmpLeaveAllow = DataAccess.FetchRS(CommandType.Text, strEmpLAllow, null);

                    if (result.Length == 0)
                    {
                        leaveAvilable = 0;
                        leaveencash = 0;
                        continue;
                    }
                    dr = dtLeaves.NewRow();
                    dr["emp_code"] = ds_Emp.Tables[0].Rows[i]["emp_code"].ToString();
                    dr["emp_name"] = ds_Emp.Tables[0].Rows[i]["emp_name"].ToString();
                    if (result.Length > 0)
                    {
                        //dr["lastyearleaves"] = "10";
                        leaveAvilable = Convert.ToDouble(result[0]["actualleavesavailable"].ToString());
                        // leaveencash = Convert.ToDouble(txtfwd.Text);
                        leaveencash = leaveAvilable;
                    }
                    else
                    {
                        leaveAvilable = 0;
                        leaveencash = 0;
                    }



                    leaveafterEncash = leaveAvilable - leaveencash;
                    if (leaveafterEncash >= 0)
                    {

                        actualleaveEncash = leaveencash;
                    }
                    else
                    {
                        leaveafterEncash = 0;
                        actualleaveEncash = leaveAvilable;
                    }




                    dr["LeaveAvailble"] = leaveAvilable;
                    dr["LeaveEncash"] = actualleaveEncash;
                    dr["LeavesAfterTranfer"] = leaveafterEncash;



                    if (actualleaveEncash > 0.0)
                    {
                        string sSQLquery = "sp_GetEmployeeLeaveEncash";
                        SqlParameter[] parmeras = new SqlParameter[13];
                        parmeras[0] = new SqlParameter("@company_id", compid);
                        //parmeras[1] = new SqlParameter("@month", cmbMonth.SelectedValue.ToString());
                        parmeras[1] = new SqlParameter("@month", cmbMonth.SelectedValue.ToString());
                        parmeras[2] = new SqlParameter("@year", cmbYear.SelectedValue.ToString());
                        parmeras[3] = new SqlParameter("@UserID", 304);
                        parmeras[4] = new SqlParameter("@EmpPassID", emp_code.ToString());
                        parmeras[5] = new SqlParameter("@stdatemonth", Session["PayStartDay"].ToString());
                        parmeras[6] = new SqlParameter("@endatemonth", Session["PayEndDay"].ToString());
                        parmeras[7] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                        parmeras[8] = new SqlParameter("@endatesubmonth", Session["PaySubEndDate"].ToString());
                        parmeras[9] = new SqlParameter("@monthidintbl", cmbMonth.SelectedValue.ToString());
                        parmeras[10] = new SqlParameter("@DeptId", "-1");
                        parmeras[11] = new SqlParameter("@LeaveEncash", actualleaveEncash);
                        parmeras[12] = new SqlParameter("@encashtype", 1);
                        LeaveEncash = DataAccess.ExecuteSPDataSet(sSQLquery, parmeras); //txtfwd
                        DataTable resultrow = LeaveEncash.Tables[0];

                        dr["Formula"] = resultrow.Rows[0]["Formula"].ToString();



                        dr["Amount"] = Math.Round(decimal.Parse(resultrow.Rows[0]["AmountTotal"].ToString()), 2);
                        dtLeaves.Rows.Add(dr);

                    }





                }
                catch (Exception ex)
                {
                    //ShowMessageBox(ex.Message);
                    _actionMessage = "Warning|"+ex.Message;
                    ViewState["actionMessage"] = _actionMessage;
                }

            }
            RadGrid1.DataSourceID = "";
            RadGrid1.DataSource = dtLeaves;
            RadGrid1.DataBind();
        }

        //protected void Hybrid_Leave_Binding()
        //{

        //    SetControlDate();

        //    isFormulaAssigned();




        //    string strEmpSelect = "";

        //    DataSet ds_Emp = new DataSet();

        //    DataSet ds_Assigen = new DataSet();

        //    DataSet LeaveEncash = new DataSet();

        //    DataTable dtLeaves = new DataTable();

        //    DataRow dr;

        //    dtLeaves = (DataTable)Session["dtLeaves"];

        //    compid = Utility.ToInteger(Session["Compid"].ToString());

        //    strEmpSelect = " Select DISTINCT e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
        //                   " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
        //                   " where e.Company_Id = " + compid + " and e.termination_date is not null and year(e.termination_date )= " + Convert.ToInt32(cmbYear.SelectedValue) + " and month(e.termination_date)=" + month+ " and el.leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + " and Leave_Type=8 AND termination_date is  not null and e.emp_group_id= " + cmbEmpgroup.SelectedValue.ToString();



        //    ds_Emp = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);










        //    for (int i = 0; i < ds_Emp.Tables[0].Rows.Count; i++)
        //    {
        //        double leaveAvilable = 0.0;
        //        double leaveencash = 0.0;
        //        double leaveafterEncash = 0.0;
        //        double actualleaveEncash = 0.0;

        //        try
        //        {
        //           string applyondate = "31/12/" + Convert.ToString(int.Parse(cmbYear.SelectedValue));

        //            //string applyondate = (DateTime)Session["PaySubEndDate"].ToString("dd/MM/yyyy");
        //            DataSet ds_LeaveInfo = new DataSet();
        //            int emp_code = Convert.ToInt32(ds_Emp.Tables[0].Rows[i]["emp_code"].ToString());
        //            string sSQL = "sp_GetEmployeeLeavePolicy";
        //            SqlParameter[] parms = new SqlParameter[4];
        //            parms[0] = new SqlParameter("@empid", emp_code);
        //            parms[1] = new SqlParameter("@year", Utility.ToString(int.Parse(cmbYear.SelectedValue)));
        //            parms[2] = new SqlParameter("@applydateon", Convert.ToDateTime(applyondate));
        //            parms[3] = new SqlParameter("@filter", -1);
        //            ds_LeaveInfo = DataAccess.ExecuteSPDataSet(sSQL, parms); //txtfwd
        //            DataRow[] result = ds_LeaveInfo.Tables[0].Select("id=8");


        //            //string strEmpLAllow = "select * from EmployeeLeavesAllowed where emp_id=" + emp_code + " and leave_year=" + Convert.ToInt32(cmbYear.SelectedValue) + "  and Leave_Type=8";
        //            //DataSet ds_EmpLeaveAllow = new DataSet();
        //            //ds_EmpLeaveAllow = DataAccess.FetchRS(CommandType.Text, strEmpLAllow, null);

        //            if (result.Length == 0)
        //            {
        //                leaveAvilable = 0;
        //                leaveencash = 0;
        //                continue;
        //            }
        //            dr = dtLeaves.NewRow();
        //            dr["emp_code"] = ds_Emp.Tables[0].Rows[i]["emp_code"].ToString();
        //            dr["emp_name"] = ds_Emp.Tables[0].Rows[i]["emp_name"].ToString();
        //            if (result.Length > 0)
        //            {
        //                //dr["lastyearleaves"] = "10";
        //                leaveAvilable = Convert.ToDouble(result[0]["actualleavesavailable"].ToString());
        //                leaveencash = leaveAvilable;
        //            }
        //            else
        //            {
        //                leaveAvilable = 0;
        //                leaveencash = 0;
        //            }



        //            leaveafterEncash = leaveAvilable - leaveencash;
        //            if (leaveafterEncash >= 0)
        //            {

        //                actualleaveEncash= leaveencash;
        //            }
        //            else
        //            {
        //                leaveafterEncash = 0;
        //                actualleaveEncash = leaveAvilable;
        //            }




        //             dr["LeaveAvailble"]=leaveAvilable;
        //             dr["LeaveEncash"] = leaveAvilable;
        //            dr["LeavesAfterTranfer"]=leaveafterEncash;



        //            if (actualleaveEncash > 0.0)
        //            {
        //                          string sSQLquery = "sp_GetEmployeeLeaveEncash";
        //                          SqlParameter[] parmeras = new SqlParameter[13];
        //                          parmeras[0] = new SqlParameter("@company_id", compid);
        //                          parmeras[1] = new SqlParameter("@month", cmbMonth.SelectedValue.ToString());
        //                          parmeras[2] = new SqlParameter("@year", cmbYear.SelectedValue.ToString());
        //                          parmeras[3] = new SqlParameter("@UserID", 304);
        //                          parmeras[4] = new SqlParameter("@EmpPassID", emp_code.ToString());
        //                          parmeras[5] = new SqlParameter("@stdatemonth", Session["PayStartDay"].ToString());
        //                          parmeras[6] = new SqlParameter("@endatemonth", Session["PayEndDay"].ToString());
        //                          parmeras[7] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
        //                          parmeras[8] = new SqlParameter("@endatesubmonth", Session["PaySubEndDate"].ToString());
        //                          parmeras[9] = new SqlParameter("@monthidintbl", cmbMonth.SelectedValue.ToString());
        //                          parmeras[10] = new SqlParameter("@DeptId", "-1");
        //                          parmeras[11] = new SqlParameter("@LeaveEncash", actualleaveEncash);
        //                          parmeras[12] = new SqlParameter("@encashtype", 1);
        //                          LeaveEncash = DataAccess.ExecuteSPDataSet(sSQLquery, parmeras); //txtfwd
        //                                   DataTable resultrow = LeaveEncash.Tables[0];

        //                                   dr["Formula"] = resultrow.Rows[0]["Formula"].ToString();



        //                                   dr["Amount"] =  Math.Round( decimal.Parse(resultrow.Rows[0]["AmountTotal"].ToString()),2);
        //                                 dtLeaves.Rows.Add(dr);

        //            }





        //        }
        //        catch (Exception ex)
        //        {

        //        }

        //    }
        //    RadGrid1.DataSourceID = "";
        //    RadGrid1.DataSource = dtLeaves;
        //    RadGrid1.DataBind();
        //}

        int leave_model;
        //protected void Button2_Click(object sender, EventArgs e)
        //{
        //    string sqltype = "select id from additions_types where [desc]='LEAVEENCASH'";

        //    int typeid = 0;
        //    SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqltype, null);

        //    while (dr2.Read())
        //    {
        //        if (dr2.GetValue(0) != System.DBNull.Value)
        //        {
        //            typeid = Utility.ToInteger(dr2.GetValue(0));

        //        }

        //    }

        //    if (typeid == 0)
        //    {
        //        ShowMessageBox("Please Add Addition Type of LEAVEENCASH");
        //        return;
        //    }

        //    foreach (GridItem item in RadGrid1.MasterTableView.Items)
        //    {
        //        if (item is GridItem)
        //        {

        //            GridDataItem dataItem = (GridDataItem)item;
        //            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
        //            if (chkBox.Checked == true)
        //            {
        //                int i = 1;
        //                double empcode = Convert.ToDouble(dataItem.Cells[3 + i].Text);
        //                double noofLeaveEncsh = Convert.ToDouble(dataItem.Cells[6 + i].Text);

        //                double amount = Convert.ToDouble(dataItem.Cells[9 + i].Text);

        //                imgbtnsave_Click(empcode.ToString(), noofLeaveEncsh, amount, typeid);
        //                Thread.Sleep(100);
        //            }

        //        }
        //    }

        //    ShowMessageBox("Leave Encashment Successful");


        //}
        private void imgbtnsave_Click(string empcode, double nofoleaveEncash, double amount, int addtype)
        {





            DateTime dt1 = DateTime.Now;
            bool kk = DateTime.TryParse(Session["PaySubStartDate"].ToString(), out dt1);
            DateTime dt = new DateTime(DateTime.Now.Year, dt1.Month, dt1.Day);
            //check payroll process
            string sSQL1 = "sp_GetPayrollProcessOn";
            SqlParameter[] parms1 = new SqlParameter[3];
            parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(empcode));
            parms1[1] = new SqlParameter("@compid", compid);
            parms1[2] = new SqlParameter("@trxdate", dt.ToString("dd/MMM/yyyy"));
            int conLock = 0;
            SqlDataReader dr11 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
            while (dr11.Read())
            {
                conLock = Utility.ToInteger(dr11.GetValue(0));
            }
            if (conLock > 0)
            {


               // HttpContext.Current.Response.Write("<Script type='text/javascript'>alert('Payroll has been locked for Selected Month.')</Script>");
                _actionMessage = "Warning|Payroll has been locked for Selected Month.";
                ViewState["actionMessage"] = _actionMessage;

                return;
            }

            //------



            while (dt.DayOfWeek == DayOfWeek.Saturday)
            {
                dt = dt.AddDays(1);
            }


            if (dt.DayOfWeek == DayOfWeek.Sunday)
            {
                dt = dt.AddDays(1);
            }

            // InsertInLeave_days();
            string status;

            string startdate = dt.ToString("dd/MMM/yyyy");


            for (int k = 0; k < nofoleaveEncash; k++)
            {
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {

                    k = k - 1;
                }

                dt = dt.AddDays(1);


            }



            string enddate = dt.ToString("dd/MMM/yyyy");
            string leavetype = "8";
            string approvername = Utility.ToString(Session["Username"]);


            string timesession = "";
            string leaveRemarks = "LEAVE ENCASH";
            string username = Utility.ToString(Session["Username"]);


            string requestRemarks = leaveRemarks;

            status = "Approved";


            SqlParameter[] parms = new SqlParameter[13];
            int i = 0;
            parms[0] = new SqlParameter("@emp_code", empcode);

            parms[1] = new SqlParameter("@start_date", startdate);
            parms[2] = new SqlParameter("@end_date", enddate);
            parms[3] = new SqlParameter("@leave_type", Utility.ToInteger(leavetype));
            parms[4] = new SqlParameter("@approver", approvername);
            parms[5] = new SqlParameter("@status", status);
            parms[6] = new SqlParameter("@paid_leaves", nofoleaveEncash);
            parms[7] = new SqlParameter("@unpaid_leaves", "0");

            parms[8] = new SqlParameter("@half_day", Utility.ToInteger(0));
            parms[9] = new SqlParameter("@timesession", "");

            // Adding remarks for employee Leave Request
            parms[10] = new SqlParameter("@remarks", leaveRemarks);
            parms[11] = new SqlParameter("@applyyear", Utility.ToInteger(cmbYear.SelectedValue.ToString()));
            parms[12] = new SqlParameter("@RETURN_trx_iD", SqlDbType.Int, 10);
            parms[12].Direction = ParameterDirection.Output;


            string SQL = "Sp_applyleave_encash";
            string trx_id = "";
            try
            {
                int retVal = DataAccess.ExecuteStoreProc(SQL, parms);
                trx_id = parms[12].Value.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            string clmstatus = "Approved";
            int converison = 0;
            i = 0;
            SqlParameter[] parmsAdd = new SqlParameter[16];
            //End : 3 
            parmsAdd[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
            parmsAdd[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
            parmsAdd[i++] = new SqlParameter("@trx_period1", startdate);
            parmsAdd[i++] = new SqlParameter("@trx_period2", startdate);
            parmsAdd[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount) * nofoleaveEncash);
            parmsAdd[i++] = new SqlParameter("@basis_arriving_payment", "0");
            parmsAdd[i++] = new SqlParameter("@service_length", "0");
            parmsAdd[i++] = new SqlParameter("@iras_approval", "No");
            parmsAdd[i++] = new SqlParameter("@iras_approval_date", "");
            parmsAdd[i++] = new SqlParameter("@additionsforyear", dt.Year);
            parmsAdd[i++] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
            parmsAdd[i++] = new SqlParameter("@CurrId", "1");
            parmsAdd[i++] = new SqlParameter("@claimstatus", clmstatus);
            parmsAdd[i++] = new SqlParameter("@ConversionOpt", converison);
            parmsAdd[i++] = new SqlParameter("@ExchangeRate", 1.0);
            parmsAdd[i++] = new SqlParameter("@Encash_addition_id", trx_id.ToString());

            string sSQLAdd = "sp_empadd_add_encash";
            try
            {
                int retValAdd = DataAccess.ExecuteStoreProc(sSQLAdd, parmsAdd);



                RadGrid1.DataSource = null;
                RadGrid1.DataBind();
                //ShowMessageBox("Leave Encashment Successful");
              var  _actionMessage = "Success|Leave Encashment Successful.";
                ViewState["actionMessage"] = _actionMessage;
            }



            catch (Exception ex)
            {

                //ShowMessageBox(ex.Message);
                var _actionMessage = "Warning|"+ ex.Message;
                ViewState["actionMessage"] = _actionMessage;
            }


        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string sqltype = "select id from additions_types where id=1001";

            int typeid = 0;
            SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sqltype, null);

            while (dr2.Read())
            {
                if (dr2.GetValue(0) != System.DBNull.Value)
                {
                    typeid = Utility.ToInteger(dr2.GetValue(0));

                }

            }

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

                        double leaveavailable = Convert.ToDouble(dataItem.Cells[5 + i].Text);

                        TextBox txtbox = (TextBox)dataItem.FindControl("LeaveEncash");
                        double noofLeaveEncsh = Convert.ToDouble(txtbox.Text);
                        if (noofLeaveEncsh > leaveavailable)
                        {
                            noofLeaveEncsh = leaveavailable;
                        }

                        double amount = Convert.ToDouble(dataItem.Cells[9 + i].Text);

                        imgbtnsave_Click(empcode.ToString(), noofLeaveEncsh, amount, typeid);
                        Thread.Sleep(100);
                    }

                }
            }


        }
        //private  void imgbtnsave_Click(string empcode,double nofoleaveEncash,double amount,int addtype)
        //{
        //    int year=  int.Parse(cmbYear.SelectedValue.ToString());

        //    DateTime dt = new DateTime(year,month, 1);


        //    while (dt.DayOfWeek == DayOfWeek.Saturday)
        //    {
        //        dt = dt.AddDays(1);
        //    }


        //    if (dt.DayOfWeek == DayOfWeek.Sunday)
        //    {
        //        dt = dt.AddDays(1);
        //    }

        //    // InsertInLeave_days();
        //    string status;

        //    string startdate = dt.ToString("dd/MM/yyyy");

        //    for (int k = 0; k < nofoleaveEncash; k++)
        //    {
        //       if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
        //        {

        //             k = k- 1;
        //        }

        //        dt = dt.AddDays(1);


        //    }



        //    string enddate = dt.ToString("dd/MM/yyyy");
        //    string leavetype = "8";
        //    string approvername = Utility.ToString(Session["Username"]);


        //    string timesession = "";
        //    string leaveRemarks = "LEAVE ENCASH";
        //    string username = Utility.ToString(Session["Username"]);


        //      string   requestRemarks = leaveRemarks;

        //     status = "Approved";


        //        SqlParameter[] parms = new SqlParameter[12];
        //        int i = 0;
        //        parms[i++] = new SqlParameter("@emp_code", empcode);

        //        parms[i++] = new SqlParameter("@start_date", startdate);
        //        parms[i++] = new SqlParameter("@end_date", enddate);
        //        parms[i++] = new SqlParameter("@leave_type", Utility.ToInteger(leavetype));
        //        parms[i++] = new SqlParameter("@approver", approvername);
        //        parms[i++] = new SqlParameter("@status", status);
        //        parms[i++] = new SqlParameter("@paid_leaves", nofoleaveEncash);
        //        parms[i++] = new SqlParameter("@unpaid_leaves", "0");

        //        parms[i++] = new SqlParameter("@half_day", Utility.ToInteger(0));
        //        parms[i++] = new SqlParameter("@timesession", "");

        //        // Adding remarks for employee Leave Request
        //        parms[i++] = new SqlParameter("@remarks", leaveRemarks);
        //        parms[i++] = new SqlParameter("@applyyear", Utility.ToInteger(cmbYear.SelectedValue.ToString()));
        //        string SQL = "Sp_ApplyLeave";
        //        try
        //        {
        //           int retVal = DataAccess.ExecuteStoreProc(SQL, parms);
        //        }
        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }
        //        string clmstatus = "Approved";
        //        int converison = 0;
        //        i = 0;
        //        SqlParameter[] parmsAdd = new SqlParameter[15];
        //        //End : 3 
        //        parmsAdd[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
        //        parmsAdd[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
        //        parmsAdd[i++] = new SqlParameter("@trx_period1", startdate);
        //        parmsAdd[i++] = new SqlParameter("@trx_period2", enddate);
        //        parmsAdd[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount) * nofoleaveEncash);
        //        parmsAdd[i++] = new SqlParameter("@basis_arriving_payment", "0");
        //        parmsAdd[i++] = new SqlParameter("@service_length", "0");
        //        parmsAdd[i++] = new SqlParameter("@iras_approval", "No");
        //        parmsAdd[i++] = new SqlParameter("@iras_approval_date", "");
        //        parmsAdd[i++] = new SqlParameter("@additionsforyear", dt.Year);
        //        parmsAdd[i++] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
        //        parmsAdd[i++] = new SqlParameter("@CurrId", "1");
        //        parmsAdd[i++] = new SqlParameter("@claimstatus", clmstatus);
        //        parmsAdd[i++] = new SqlParameter("@ConversionOpt", converison);
        //        parmsAdd[i++] = new SqlParameter("@ExchangeRate", 1.0);

        //        string sSQLAdd = "sp_empadd_add";
        //        try
        //        {
        //            int retValAdd = DataAccess.ExecuteStoreProc(sSQLAdd, parmsAdd);



        //            RadGrid1.DataSource = null;
        //            RadGrid1.DataBind();

        //        }


        //        catch (Exception ex)
        //        {

        //            throw ex;
        //        }


        //}







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
