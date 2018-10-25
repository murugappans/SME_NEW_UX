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
using Microsoft.VisualBasic;
using System.Drawing;
using System.Data.SqlClient;
namespace SMEPayroll.employee
{
    public partial class emp_overtime_ceiling : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy = "";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        protected string strendatedmy = "";
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;

        string strWF = "";
        string strEmpvisible = "";


        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridCommandItem item = e.Item as GridCommandItem;
            if (item != null)
            {
                Button btn = item.FindControl("btnsubmit") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                btn = item.FindControl("btnCalcOverVar") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                btn = item.FindControl("btnApplyCeiling") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                btn = item.FindControl("btnReset") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");

            }


            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e, tbRecord);
        }

        [AjaxPro.AjaxMethod]
        public string SetDate(string monthid)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataSet ds = new DataSet();
            sSQL = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@ROWID", monthid);
            parms[1] = new SqlParameter("@YEARS", 0);
            parms[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());

            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

            Session["PayStartDay"] = ds.Tables[0].Rows[0]["PayStartDay"].ToString();
            Session["PayEndDay"] = ds.Tables[0].Rows[0]["PayEndDay"].ToString();
            Session["PaySubStartDay"] = ds.Tables[0].Rows[0]["PaySubStartDay"].ToString();
            Session["PaySubEndDay"] = ds.Tables[0].Rows[0]["PaySubEndDay"].ToString();
            Session["PaySubStartDate"] = ds.Tables[0].Rows[0]["PaySubStartDate"].ToString();
            Session["PaySubEndDate"] = ds.Tables[0].Rows[0]["PaySubEndDate"].ToString();
            return Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "," + Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            /* To disable Grid filtering options  */
            AjaxPro.Utility.RegisterTypeForAjax(typeof(emp_overtime));
            if (cmbMonth.Attributes["onchange"] == null) { cmbMonth.Attributes.Add("onchange", "javascript:ChangeMonth(this.value);"); }

            Telerik.Web.UI.GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;




            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            //lblerror.Text = "";
            comp_id = Utility.ToInteger(Session["Compid"]);
            RadGrid1.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid1_PageSizeChanged);
            RadGrid1.PageIndexChanged += new GridPageChangedEventHandler(RadGrid1_PageIndexChanged);
            RadGrid1.ColumnCreated += new GridColumnCreatedEventHandler(RadGrid1_ColumnCreated);
            RadGrid1.PreRender += new EventHandler(RadGrid1_PreRender);
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
            }

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }




            //RadGrid1.Columns[11].Visible = false;
            //RadGrid1.Columns[12].Visible = false;
            //RadGrid1.Columns[13].Visible = false;
            //RadGrid1.Columns[14].Visible = false;
            ////if (Session["V1Formula"].ToString() != "0")
            ////{
            ////RadGrid1.Columns[11].HeaderText = Session["V1text"].ToString();
            //RadGrid1.Columns[11].HeaderText = Convert.ToString(Session["V1text"]);
            //RadGrid1.Columns[11].Visible = true;
            ////}

            ////if (Session["V2Formula"].ToString() != "0")
            ////{
            ////RadGrid1.Columns[12].HeaderText = Session["V2text"].ToString();
            //RadGrid1.Columns[12].HeaderText = Convert.ToString(Session["V2text"]);
            //RadGrid1.Columns[12].Visible = true;
            ////}
            ////if (Session["V3Formula"].ToString() != "0")
            ////{
            ////RadGrid1.Columns[13].HeaderText = Session["V3text"].ToString();
            //RadGrid1.Columns[13].HeaderText = Convert.ToString(Session["V3text"]);
            //RadGrid1.Columns[13].Visible = true;
            ////}
            ////if (Session["V4Formula"].ToString() != "0")
            ////{
            ////RadGrid1.Columns[14].HeaderText = Session["V4text"].ToString();
            //RadGrid1.Columns[14].HeaderText = Convert.ToString(Session["V4text"]);
            //RadGrid1.Columns[14].Visible = true;
            //}


            string streColNames = "Select [desc],[Code] from additions_types where company_id=" + comp_id + " AND code IN ('C1','C2','C3','C4','C5','C6','C7','C8')";
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, streColNames, null);

            string colName = "";
            string Code = "";
            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null)
                {
                    colName = dr1.GetValue(0).ToString();
                    Code = dr1.GetValue(1).ToString();
                    foreach (GridColumn col in RadGrid1.Columns)
                    {
                        if (col.HeaderText == "NH" && Code == "C1")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "OT-1 Hrs" && Code == "C2")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "OT-2 Hrs" && Code == "C3")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "Days Work" && Code == "C4")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "V1" && Code == "C5")
                        {
                            col.HeaderText = colName;
                        }
                        if (col.HeaderText == "V2" && Code == "C6")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "V3" && Code == "C7")
                        {
                            col.HeaderText = colName;
                        }

                        if (col.HeaderText == "V4" && Code == "C8")
                        {
                            col.HeaderText = colName;
                        }
                    }
                }

            }

            if (Session["TimeSheetApproved"].ToString() == "1")
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.Bottom;
            }
            else
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            }

            if (!IsPostBack)
            {
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }


            //Check for WorkFlow number 2
            if (strWF == "2" && Session["PayrollWF"] != null)
            {
                if (Session["PayrollWF"].ToString() == "1")
                {
                    if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                    {
                        strEmpvisible = "";
                        if (Session["dsEmpSup"] != null)
                        {
                            if (Session["dsEmpWF"] != null)
                            {
                                DataSet dstemp = new DataSet();
                                dstemp = (DataSet)Session["dsEmpWF"];
                                foreach (DataRow dr in dstemp.Tables[0].Rows)
                                {
                                    if (strEmpvisible == "")
                                    {
                                        strEmpvisible = dr["Emp_ID"].ToString();
                                    }
                                    else
                                    {
                                        strEmpvisible = strEmpvisible + "," + dr["Emp_ID"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //strEmpvisible = "5,127";
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }


        }

        void RadGrid1_PreRender(object sender, EventArgs e)
        {

            //RadGrid1.Rebind();  

        }

        void RadGrid1_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {

        }



        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (e.RebindReason != GridRebindReason.InitialLoad)
            {
                if (strEmpvisible != "")
                {
                    Session["EmpPassID"] = strEmpvisible;
                }
                else
                {
                    Session["EmpPassID"] = "";
                }

                tbRecord.Visible = true;//toolbar

                intcnt = 1;

                //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

                int i = 0;

                DataSet ds = new DataSet();

                string ssql = "sp_emp_overtime";// 0,2009,2
                SqlParameter[] parms = new SqlParameter[4];
                parms[i++] = new SqlParameter("@month", cmbMonth.SelectedValue);
                parms[i++] = new SqlParameter("@year", cmbYear.SelectedValue);
                parms[i++] = new SqlParameter("@company_id", comp_id);
                parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(deptID.SelectedValue));
                ds = DataAccess.ExecuteSPDataSet(ssql, parms);


                if (strEmpvisible != "")
                {
                    DataView view = new DataView();

                    view = ds.Tables[0].DefaultView;
                    //ds.Tables[0].DefaultView.RowFilter = "emp_id IN(" + strEmpvisible + ")";


                    //view.Table = DataSet1.Tables["Suppliers"];
                    //view.AllowDelete = true;
                    //view.AllowEdit = true;
                    // view.AllowNew = true;
                    view.RowFilter = "empid IN(" + strEmpvisible + ")";
                    // Simple-bind to a TextBox control
                    Session["EmpPassID"] = strEmpvisible;
                    this.RadGrid1.DataSource = view;
                    //RadGrid1.DataBind();
                }
                else
                {
                    this.RadGrid1.DataSource = ds;
                    //RadGrid1.DataBind();
                }



                //RadGrid1.DataSource = ds;
                //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
                //foreach (DataRow dr in drResults)
                //{
                //    Session["PayStartDay"] = dr["PayStartDay"].ToString();
                //    Session["PayEndDay"] = dr["PayEndDay"].ToString();
                //    Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                //    Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                //    Session["CurrentMonth"] = dr["Month"].ToString();
                //    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                //    Session["PaySubEndDate"]= dr["PaySubEndDate"].ToString();
                //    strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
                //    strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
                //    strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
                //    strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
                //}
                //if (Session["PaySubStartDate"] != null)
                //{
                //}
                //RadGrid1.DataBind();
                Session["ROWID"] = cmbMonth.SelectedValue.ToString();
                Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();

            }
        }

        void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            bingrid1();
        }


        void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {


            bingrid1();
            //throw new Exception("The method or operation is not implemented.");
        }

        void bingrid1()
        {
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }

            tbRecord.Visible = true;//toolbar

            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            rdFrom.Enabled = false;
            rdEnd.Enabled = false;
            deptID.Enabled = false;
            //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            int i = 0;

            DataSet ds = new DataSet();

            string ssql = "sp_emp_overtime";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[4];
            parms[i++] = new SqlParameter("@month", cmbMonth.SelectedValue);
            parms[i++] = new SqlParameter("@year", cmbYear.SelectedValue);
            parms[i++] = new SqlParameter("@company_id", comp_id);
            parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(deptID.SelectedValue));
            ds = DataAccess.ExecuteSPDataSet(ssql, parms);

            if (ds.Tables.Count > 0)
            {
                ds.Tables[0].Columns.Add("CelFlag");
            }

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
            strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
            strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
            strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
            int empid = 0;

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                empid = Convert.ToInt32(dr["empid"].ToString());
                //Change Data Set with nw values to bring form Emp_additions table ...
                string chkv1 = "select CelFlag,trx_amount,trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and CelFlag IN (1,2,3,4,5,6,7,8) and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                DataSet dschk1 = new DataSet();
                dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                foreach (DataRow dr1 in dschk1.Tables[0].Rows)
                {
                    if (dr1["emp_code"].ToString() == empid.ToString())
                    {
                        DataRow dredit = dr;
                        dredit.BeginEdit();
                        if (dr1["CelFlag"].ToString() == "1")
                        {
                            dredit["NH_Work"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "1";
                        }
                        if (dr1["CelFlag"].ToString() == "2")
                        {
                            dredit["overtime1"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "2";
                        }
                        if (dr1["CelFlag"].ToString() == "3")
                        {
                            dredit["overtime2"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "3";
                        }
                        if (dr1["CelFlag"].ToString() == "4")
                        {
                            dredit["days_work"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "4";
                        }
                        if (dr1["CelFlag"].ToString() == "5")
                        {
                            dredit["v1"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "5";
                        }
                        if (dr1["CelFlag"].ToString() == "6")
                        {
                            dredit["v2"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "6";
                        }
                        if (dr1["CelFlag"].ToString() == "7")
                        {
                            dredit["v3"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "7";
                        }
                        if (dr1["CelFlag"].ToString() == "8")
                        {
                            dredit["v4"] = dr1["trx_amount"].ToString();
                            dredit["CelFlag"] = "8";
                        }
                        dredit.AcceptChanges();
                    }
                }
            }

            //
            if (strEmpvisible != "")
            {
                DataView view = new DataView();

                view = ds.Tables[0].DefaultView;
                //ds.Tables[0].DefaultView.RowFilter = "emp_id IN(" + strEmpvisible + ")";


                //view.Table = DataSet1.Tables["Suppliers"];
                //view.AllowDelete = true;
                //view.AllowEdit = true;
                // view.AllowNew = true;
                view.RowFilter = "empid IN(" + strEmpvisible + ")";
                // Simple-bind to a TextBox control
                Session["EmpPassID"] = strEmpvisible;
                this.RadGrid1.DataSource = view;
                RadGrid1.DataBind();
            }
            else
            {
                this.RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }

            //RadGrid1.DataSource = ds;
            //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            //foreach (DataRow dr in drResults)
            //{
            //    Session["PayStartDay"] = dr["PayStartDay"].ToString();
            //    Session["PayEndDay"] = dr["PayEndDay"].ToString();
            //    Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
            //    Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
            //    Session["CurrentMonth"] = dr["Month"].ToString();
            //    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
            //    Session["PaySubEndDate"]= dr["PaySubEndDate"].ToString();
            //    strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
            //    strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
            //    strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
            //    strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
            //}
            //if (Session["PaySubStartDate"] != null)
            //{
            RadGrid1.DataBind();
            //}
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();


        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            bingrid1();
        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private void bindMonth()
        {
            MonthFill();
        }
        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Overtime Payroll")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;

            }
            else
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.Bottom;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (Session["PaySubStartDate"] != null)
                {
                    GridItem dataItem = (GridItem)e.Item;
                    GridDataItem dtItem = e.Item as GridDataItem;
                    string empid = dataItem.Cells[4].Text.ToString();
                    TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                    TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                    TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                    TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                    TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                    TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                    TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                    TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                    TextBox txtot_entitlement = (TextBox)dataItem.FindControl("txtot_entitlement");



                    string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                    ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                    string status;
                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        status = "";
                    }
                    else
                    {
                        status = Convert.ToString(ds.Tables[0].Rows[0]["status"]).ToString();
                    }

                    if (dtItem["pay_frequency"].Text.Trim() != "D")
                    {
                        txtbox2.Enabled = false;
                        txtbox2.BackColor = Color.LightYellow;
                    }
                    //if (dtItem["pay_frequency"].Text.Trim() == "M" || dtItem["pay_frequency"].Text.Trim() == "H")
                    if (dtItem["pay_frequency"].Text.Trim() == "H")
                    {
                        txtbox3.Enabled = true;
                    }
                    else
                    {
                        txtbox3.Enabled = false;
                        txtbox3.BackColor = Color.LightYellow;
                    }

                    if (status == "G" || status == "P" || status == "A")
                    {
                        txtbox.Enabled = false;
                        txtbox1.Enabled = false;
                        txtbox2.Enabled = false;
                        txtbox3.Enabled = false;
                        txtv1.Enabled = false;
                        txtv2.Enabled = false;
                        txtv3.Enabled = false;
                        txtv4.Enabled = false;
                        dataItem.BackColor = Color.Azure;
                    }
                    else
                    {
                        if (txtot_entitlement.Text.ToString().Trim() == "N")
                        {
                            txtbox.Enabled = false;
                            txtbox1.Enabled = false;
                            txtbox.BackColor = Color.LightYellow;
                            txtbox1.BackColor = Color.LightYellow;
                        }

                        //if (Session["V1Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V1Formula"]) != "0")
                        {
                            string strRate = dataItem.Cells[19].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    //(Lab)dataItem.FindControl("txtovertime");
                                    Label lblv1 = (Label)dtItem["v1"].FindControl("lblv1");
                                    if (intrae <= 0)
                                    {
                                        ((TextBox)dataItem.Cells[13].Controls[1]).Enabled = false;
                                        ((TextBox)dataItem.Cells[13].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv1.Text.Length > 0)
                                        {
                                            //lblv1.Text = lblv1.Text + "------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            //lblv1.Text ="{" + intrae + " }";
                                        }
                                        //lblv1.ToolTip = lblv1.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                ((TextBox)dataItem.Cells[13].Controls[1]).Enabled = false;
                                ((TextBox)dataItem.Cells[13].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }
                        // if (Session["V2Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V2Formula"]) != "0")
                        {
                            string strRate = dataItem.Cells[20].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv2 = (Label)dtItem["v2"].FindControl("lblv2");
                                    if (intrae <= 0)
                                    {
                                        ((TextBox)dataItem.Cells[14].Controls[1]).Enabled = false;
                                        ((TextBox)dataItem.Cells[14].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv2.Text.Length > 0)
                                        {
                                            //lblv2.Text = lblv2.Text + "------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            //lblv2.Text ="{" + intrae + " }";
                                        }
                                        //lblv2.ToolTip = lblv2.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                ((TextBox)dataItem.Cells[14].Controls[1]).Enabled = false;
                                ((TextBox)dataItem.Cells[14].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }

                        // if (Session["V3Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V3Formula"]) != "0")
                        {
                            string strRate = dataItem.Cells[21].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv3 = (Label)dtItem["v3"].FindControl("lblv3");
                                    if (intrae <= 0)
                                    {
                                        ((TextBox)dataItem.Cells[15].Controls[1]).Enabled = false;
                                        ((TextBox)dataItem.Cells[15].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv3.Text.Length > 0)
                                        {
                                            //lblv3.Text = lblv3.Text + "--------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            //lblv3.Text = "{" + intrae + " }";
                                        }
                                        //lblv3.ToolTip = lblv3.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                ((TextBox)dataItem.Cells[15].Controls[1]).Enabled = false;
                                ((TextBox)dataItem.Cells[15].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }

                        //if (Session["V4Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V4Formula"]) != "0")
                        {
                            string strRate = dataItem.Cells[22].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv4 = (Label)dtItem["v4"].FindControl("lblv4");
                                    if (intrae <= 0)
                                    {
                                        ((TextBox)dataItem.Cells[16].Controls[1]).Enabled = false;
                                        ((TextBox)dataItem.Cells[16].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv4.Text.Length > 0)
                                        {
                                            //lblv4.Text = lblv4.Text + "------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            //lblv4.Text = "{" + intrae + " }";
                                        }
                                        //.ToolTip = lblv4.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {

                                }
                            }
                            else
                            {
                                ((TextBox)dataItem.Cells[16].Controls[1]).Enabled = false;
                                ((TextBox)dataItem.Cells[16].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }

                        if (dtItem["CelFlag"].Text == "3" || dtItem["CelFlag"].Text == "1" || dtItem["CelFlag"].Text == "2" || dtItem["CelFlag"].Text == "4" || dtItem["CelFlag"].Text == "5" || dtItem["CelFlag"].Text == "6" || dtItem["CelFlag"].Text == "7" || dtItem["CelFlag"].Text == "8")
                        {
                            txtbox.Enabled = false;
                            txtbox1.Enabled = false;
                            txtbox2.Enabled = false;
                            txtbox3.Enabled = false;
                            txtv1.Enabled = false;
                            txtv2.Enabled = false;
                            txtv3.Enabled = false;
                            txtv4.Enabled = false;
                        }
                    }
                }

            }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
            strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
            strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
            strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);

            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                ((Button)commandItem.FindControl("btnCalcOverVar")).Enabled = false;
                //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = false;

                if (e.CommandName == "ApplyCeiling")
                {
                    //Hours/Days 
                    string strHourCeiling = "Select *  from CeilingMaster Where CeilingType=1 And CompanyID=" + comp_id;
                    DataSet dsHourCeiling = DataAccess.FetchRS(CommandType.Text, strHourCeiling, null);

                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true && chkBox.Enabled == true)
                            {
                                bool flagCeil = false;
                                string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));

                                if (empcode == "")
                                {

                                    empcode = empid;
                                }

                                TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");

                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {
                                    //Get Employee Ceiling for Hours 
                                    //string strEmpCeilingDetails = "Select Top(Select count(*) from CeilingEmployee  Where CompanyId=" + comp_id + ") *  from EmployeeCeilingDetails Where EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year+ " ',103)";
                                    string strEmpCeilingDetails = "Select Top(1) *  from EmployeeCeilingDetails Where Emp_Code=" + empcode + " And EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year + " ',103)  Order By EffDate     Desc";
                                    DataSet dsEmpCeilingDetails = DataAccess.FetchRS(CommandType.Text, strEmpCeilingDetails, null);

                                    double i = Utility.ToDouble(txtbox.Text); //Ot1
                                    double j = Utility.ToDouble(txtbox1.Text);//Ot2
                                    double k = Utility.ToDouble(txtbox2.Text);//Days
                                    double l = Utility.ToDouble(txtbox3.Text);// NH
                                    double dblv1 = Utility.ToDouble(txtv1.Text);//V1
                                    double dblv2 = Utility.ToDouble(txtv2.Text);//V2
                                    double dblv3 = Utility.ToDouble(txtv3.Text);//V3
                                    double dblv4 = Utility.ToDouble(txtv4.Text);//V4

                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                            if (drNHHr.Length > 0)
                                            {
                                                if (dsHourCeiling.Tables.Count > 0)
                                                {
                                                    if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                                    {
                                                        DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("AddType='C2'");
                                                        if (drOT1.Length > 0)
                                                        {
                                                            if (i > Convert.ToDouble(drNHHr[0][4].ToString()) && Convert.ToDouble(drNHHr[0][4].ToString()) > 0 && txtbox.Enabled == true)
                                                            {
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                //lblCeilingOt1
                                                                lblCeilingOt1.Text = "{" + i.ToString() + "}";
                                                                lblCeilingOt1.ToolTip = lblCeilingOt1.Text;
                                                                i = Convert.ToDouble(drNHHr[0][4].ToString());
                                                                txtbox.Text = i.ToString();
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                //lblCeilingOt1
                                                                lblCeilingOt1.Text = "{" + i.ToString() + "}";
                                                                lblCeilingOt1.ToolTip = lblCeilingOt1.Text;
                                                                //i = Convert.ToDouble(drNHHr[0][4].ToString());
                                                                txtbox.Text = i.ToString();
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }
                                                        DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("AddType='C3'");
                                                        if (drOT2.Length > 0)
                                                        {
                                                            if (j > Convert.ToDouble(drNHHr[0][5].ToString()) && Convert.ToDouble(drNHHr[0][5].ToString()) > 0 && txtbox1.Enabled == true)
                                                            {
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                //lblCeilingOt1
                                                                lblCeilingOt2.Text = "{" + j.ToString() + "}";
                                                                lblCeilingOt2.ToolTip = "{" + j.ToString() + "}";
                                                                j = Convert.ToDouble(drNHHr[0][5].ToString());
                                                                txtbox1.Text = j.ToString();
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                //lblCeilingOt1
                                                                lblCeilingOt2.Text = "{" + j.ToString() + "}";
                                                                lblCeilingOt2.ToolTip = "{" + j.ToString() + "}";
                                                                //j = Convert.ToDouble(drNHHr[0][5].ToString());
                                                                txtbox1.Text = j.ToString();
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drNH = dsHourCeiling.Tables[0].Select("AddType='C1'");
                                                        if (drNH.Length > 0)
                                                        {
                                                            if (l > Convert.ToDouble(drNHHr[0][3].ToString()) && Convert.ToDouble(drNHHr[0][3].ToString()) > 0 && txtbox3.Enabled == true)
                                                            {
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                //lblCeilingOt1
                                                                lblCeilNH.Text = "{" + l.ToString() + "}";
                                                                lblCeilNH.ToolTip = "{" + l.ToString() + "}";
                                                                l = Convert.ToDouble(drNHHr[0][3].ToString());
                                                                txtbox3.Text = l.ToString();
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox3.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                //lblCeilingOt1
                                                                lblCeilNH.Text = "{" + l.ToString() + "}";
                                                                lblCeilNH.ToolTip = "{" + l.ToString() + "}";
                                                                //l = Convert.ToDouble(drNHHr[0][3].ToString());
                                                                txtbox3.Text = l.ToString();
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;

                                                            }
                                                        }

                                                        DataRow[] drDays = dsHourCeiling.Tables[0].Select("AddType='C4'");
                                                        if (drDays.Length > 0)
                                                        {
                                                            if (k > Convert.ToDouble(drNHHr[0][6].ToString()) && Convert.ToDouble(drNHHr[0][6].ToString()) > 0 && txtbox2.Enabled == true)
                                                            {
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                //lblCeilingOt1
                                                                lblDaysWork.Text = "{" + k.ToString() + "}";
                                                                lblDaysWork.ToolTip = "{" + k.ToString() + "}";
                                                                k = Convert.ToDouble(drNHHr[0][6].ToString());
                                                                txtbox2.Text = k.ToString();
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtbox2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                //lblCeilingOt1
                                                                lblDaysWork.Text = "{" + k.ToString() + "}";
                                                                lblDaysWork.ToolTip = "{" + k.ToString() + "}";
                                                                //k = Convert.ToDouble(drNHHr[0][6].ToString());
                                                                txtbox2.Text = k.ToString();
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drV1 = dsHourCeiling.Tables[0].Select("AddType='C5'");
                                                        if (drV1.Length > 0)
                                                        {
                                                            if (dblv1 > Convert.ToDouble(drNHHr[0][7].ToString()) && Convert.ToDouble(drNHHr[0][7].ToString()) > 0 && txtv1.Enabled == true)
                                                            {
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblCeilingOt1
                                                                lblV1Ceil.Text = "{" + dblv1.ToString() + "}";
                                                                lblV1Ceil.ToolTip = "{" + dblv1.ToString() + "}";
                                                                dblv1 = Convert.ToDouble(drNHHr[0][7].ToString());
                                                                txtv1.Text = dblv1.ToString();
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtv1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblCeilingOt1
                                                                lblV1Ceil.Text = "{" + dblv1.ToString() + "}";
                                                                lblV1Ceil.ToolTip = "{" + dblv1.ToString() + "}";
                                                                //dblv1 = Convert.ToDouble(drNHHr[0][7].ToString());
                                                                txtv1.Text = dblv1.ToString();
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;

                                                            }
                                                        }

                                                        DataRow[] drV2 = dsHourCeiling.Tables[0].Select("AddType='C6'");
                                                        if (drV2.Length > 0)
                                                        {
                                                            if (dblv2 > Convert.ToDouble(drNHHr[0][8].ToString()) && Convert.ToDouble(drNHHr[0][8].ToString()) > 0 && txtv2.Enabled == true)
                                                            {
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                //lblCeilingOt1
                                                                lblV2Ceil.Text = "{" + dblv2.ToString() + "}";
                                                                lblV2Ceil.ToolTip = "{" + dblv2.ToString() + "}";
                                                                dblv2 = Convert.ToDouble(drNHHr[0][8].ToString());
                                                                txtv2.Text = dblv2.ToString();
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtv2.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                //lblCeilingOt1
                                                                lblV2Ceil.Text = "{" + dblv2.ToString() + "}";
                                                                lblV2Ceil.ToolTip = "{" + dblv2.ToString() + "}";
                                                                //dblv2 = Convert.ToDouble(drNHHr[0][8].ToString());
                                                                txtv2.Text = dblv2.ToString();
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;

                                                            }

                                                        }

                                                        DataRow[] drV3 = dsHourCeiling.Tables[0].Select("AddType='C7'");
                                                        if (drV3.Length > 0)
                                                        {
                                                            if (dblv3 > Convert.ToDouble(drNHHr[0][9].ToString()) && Convert.ToDouble(drNHHr[0][9].ToString()) > 0 && txtv3.Enabled == true)
                                                            {
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                //lblCeilingOt1
                                                                lblV3Ceil.Text = "{" + dblv3.ToString() + "}";
                                                                lblV3Ceil.ToolTip = "{" + dblv3.ToString() + "}";
                                                                dblv3 = Convert.ToDouble(drNHHr[0][9].ToString());
                                                                txtv3.Text = dblv3.ToString();
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtv3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                //lblCeilingOt1
                                                                lblV3Ceil.Text = "{" + dblv3.ToString() + "}";
                                                                lblV3Ceil.ToolTip = "{" + dblv3.ToString() + "}";
                                                                //dblv3 = Convert.ToDouble(drNHHr[0][9].ToString());
                                                                txtv3.Text = dblv3.ToString();
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drV4 = dsHourCeiling.Tables[0].Select("AddType='C8'");
                                                        if (drV4.Length > 0)
                                                        {
                                                            if (dblv4 > Convert.ToDouble(drNHHr[0][10].ToString()) && Convert.ToDouble(drNHHr[0][10].ToString()) > 0 && txtv4.Enabled == true)
                                                            {
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                //lblCeilingOt1
                                                                lblV4Ceil.Text = "{" + dblv4.ToString() + "}";
                                                                lblV4Ceil.ToolTip = "{" + dblv4.ToString() + "}";
                                                                dblv4 = Convert.ToDouble(drNHHr[0][10].ToString());
                                                                txtv4.Text = dblv4.ToString();
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtv4.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                //lblCeilingOt1
                                                                lblV4Ceil.Text = "{" + dblv4.ToString() + "}";
                                                                lblV4Ceil.ToolTip = "{" + dblv4.ToString() + "}";
                                                                //dblv4 = Convert.ToDouble(drNHHr[0][10].ToString());
                                                                txtv4.Text = dblv4.ToString();
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("Parameter=OT1");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("Parameter=OT2");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drDays = dsHourCeiling.Tables[0].Select("Parameter=Days");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV1 = dsHourCeiling.Tables[0].Select("Parameter=V1");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV2 = dsHourCeiling.Tables[0].Select("Parameter=V2");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV3 = dsHourCeiling.Tables[0].Select("Parameter=V3");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV4 = dsHourCeiling.Tables[0].Select("Parameter=V4");
                                    //}
                                    //Check Value For Rates

                                    status = "U";
                                    double v1, v2, v3, v4;

                                    ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate from employee where emp_code='" + empid + "'";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        v1 = 0;
                                        v2 = 0;
                                        v3 = 0;
                                        v4 = 0;
                                    }
                                    else
                                    {
                                        v1 = Utility.ToDouble(ds.Tables[0].Rows[0]["v1rate"].ToString());
                                        v2 = Utility.ToDouble(ds.Tables[0].Rows[0]["v2rate"].ToString());
                                        v3 = Utility.ToDouble(ds.Tables[0].Rows[0]["v3rate"].ToString());
                                        v4 = Utility.ToDouble(ds.Tables[0].Rows[0]["v4rate"].ToString());
                                    }

                                    //Check for NH , OT1, OT2 , Days
                                    DateTime enddate = new DateTime();
                                    enddate = Convert.ToDateTime(strendatedmy);
                                    DataSet dsProgression = new DataSet();
                                    string strEndDate = enddate.Year.ToString() + "-" + enddate.Month.ToString() + "-" + enddate.Day.ToString() + " 00:00:00.000";
                                    string ssql = "Sp_GetProgressionData";// 0,2009,2
                                    SqlParameter[] parms = new SqlParameter[2];
                                    parms[0] = new SqlParameter("@emp_code", empid);
                                    parms[1] = new SqlParameter("@FromDate", strEndDate);
                                    dsProgression = DataAccess.ExecuteSPDataSet(ssql, parms);

                                    double nhRate = 0.00;
                                    double ot1Rate = 0.00;
                                    double ot2Rate = 0.00;
                                    double daysRate = 0.00;
                                    double hourlyRate = 0.00;

                                    foreach (DataRow drP in dsProgression.Tables[0].Rows)
                                    {
                                        nhRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = Convert.ToDouble(drP["OT1Rate"].ToString());
                                        ot2Rate = Convert.ToDouble(drP["OT2Rate"].ToString());
                                        daysRate = Convert.ToDouble(drP["Daily_Rate"].ToString());
                                        hourlyRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = ot1Rate * hourlyRate;
                                        ot2Rate = ot2Rate * hourlyRate;
                                    }



                                    //Hours/Days /NH/OT1/OT2/Days
                                    //Amount Parameter No need to Take in consideration
                                    string strRateCeiling = "Select *  from CeilingMaster Where CeilingType=2 And CompanyID=" + comp_id;
                                    DataSet dsRateCeiling = DataAccess.FetchRS(CommandType.Text, strRateCeiling, null);
                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                            if (drNHHr.Length > 0)
                                            {
                                                if (dsRateCeiling.Tables.Count > 0)
                                                {
                                                    if (dsRateCeiling.Tables[0].Rows.Count > 0)
                                                    {
                                                        DataRow[] drV1 = dsRateCeiling.Tables[0].Select("AddType='C5'");
                                                        if (drV1.Length > 0)
                                                        {
                                                            double V1Orignal = dblv1 * v1; //30 * 20 =600 
                                                            double V1New = Convert.ToDouble(drNHHr[0][7].ToString()); //300 
                                                            double dbv1or = dblv1;
                                                            if (V1Orignal > V1New && V1New > 0 && txtv1.Enabled == true)
                                                            {
                                                                //Val = 30/(600/300);        
                                                                //dblv1 =Convert.ToDouble(dblv1/Convert.ToDouble(V1Orignal / V1New));
                                                                txtv1.Text = V1New.ToString("#0.00");
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                lblV1Ceil.Text = V1Orignal.ToString() + " (" + dblv1.ToString() + "*" + v1.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv1.Text = V1Orignal.ToString("#0.00");
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                lblV1Ceil.Text = V1Orignal.ToString() + " (" + dblv1.ToString() + "*" + v1.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drV2 = dsRateCeiling.Tables[0].Select("AddType='C6'");

                                                        if (drV2.Length > 0)
                                                        {
                                                            double V2Orignal = dblv2 * v2;
                                                            double V2New = Convert.ToDouble(drNHHr[0][8].ToString());
                                                            double dbv2or = dblv2;
                                                            if (V2Orignal > V2New && V2New > 0 && txtv2.Enabled == true)
                                                            {
                                                                //dblv2= Convert.ToDouble(dblv2 / Convert.ToDouble(V2Orignal / V2New));
                                                                txtv2.Text = V2New.ToString("#0.00");
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                lblV2Ceil.Text = V2Orignal.ToString() + " (" + dblv2.ToString() + "*" + v2.ToString() + ")";
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv2.Text = V2Orignal.ToString("#0.00");
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                lblV2Ceil.Text = V2Orignal.ToString() + " (" + dblv2.ToString() + "*" + v2.ToString() + ")";
                                                            }
                                                        }



                                                        DataRow[] drV3 = dsRateCeiling.Tables[0].Select("AddType='C7'");
                                                        if (drV3.Length > 0)
                                                        {
                                                            double V3Orignal = dblv3 * v3;
                                                            double V3New = Convert.ToDouble(drNHHr[0][9].ToString());
                                                            double dbv3or = dblv3;
                                                            if (V3Orignal > V3New && V3New > 0 && txtv3.Enabled == true)
                                                            {
                                                                //dblv3 = Convert.ToDouble(dblv3 / Convert.ToDouble(V3Orignal / V3New));
                                                                txtv3.Text = V3New.ToString("#0.00");
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                lblV3Ceil.Text = V3Orignal.ToString() + " (" + dblv3.ToString() + "*" + v3.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv3.Text = V3Orignal.ToString("#0.00");
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                lblV3Ceil.Text = V3Orignal.ToString() + " (" + dblv3.ToString() + "*" + v3.ToString() + ")";
                                                            }
                                                        }

                                                        DataRow[] drV4 = dsRateCeiling.Tables[0].Select("AddType='C8'");
                                                        if (drV4.Length > 0)
                                                        {
                                                            double V4Orignal = dblv4 * v4;
                                                            double V4New = Convert.ToDouble(drNHHr[0][10].ToString());
                                                            double dbv4or = dblv4;
                                                            if (V4Orignal > V4New && V4New > 0 && txtv4.Enabled == true)
                                                            {
                                                                //dblv4 = Convert.ToDouble(dblv4 / Convert.ToDouble(V4Orignal / V4New));
                                                                txtv4.Text = V4New.ToString("#0.00");
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                lblV4Ceil.Text = V4Orignal.ToString() + " (" + dblv4.ToString() + "*" + v4.ToString() + ")";
                                                                flagCeil = true;
                                                                //txtv4.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv4.Text = V4Orignal.ToString("#0.00");
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                lblV4Ceil.Text = V4Orignal.ToString() + " (" + dblv4.ToString() + "*" + v4.ToString() + ")";
                                                            }
                                                        }


                                                        DataRow[] drNH = dsRateCeiling.Tables[0].Select("AddType='C1'");
                                                        if (drNH.Length > 0)
                                                        {
                                                            double nhOrignal = l * nhRate;
                                                            double nhNew = Convert.ToDouble(drNHHr[0]["NH"].ToString());
                                                            double nhOr = l;
                                                            if (nhOrignal > nhNew && nhNew > 0 && txtbox3.Enabled == true)
                                                            {
                                                                //l = Convert.ToDouble(l / Convert.ToDouble(nhOrignal / nhNew));
                                                                txtbox3.Text = nhNew.ToString("#0.00");
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                lblCeilNH.Text = nhOrignal + "(" + l.ToString() + " * " + nhRate.ToString() + ")";//dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox3.Text = nhOrignal.ToString("#0.00");
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                lblCeilNH.Text = nhOrignal + "(" + l.ToString() + " * " + nhRate.ToString() + ")";//dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                            }
                                                        }



                                                        DataRow[] drOt1 = dsRateCeiling.Tables[0].Select("AddType='C2'");
                                                        if (drOt1.Length > 0)
                                                        {
                                                            double ot1Orignal = i * ot1Rate;
                                                            double ot1New = Convert.ToDouble(drNHHr[0]["OT1"].ToString());
                                                            double ot1Or = i;
                                                            if (ot1Orignal > ot1New && ot1New > 0 && txtbox.Enabled == true)
                                                            {
                                                                //i = Convert.ToDouble(i / Convert.ToDouble(ot1Orignal / ot1New));
                                                                txtbox.Text = ot1New.ToString("#0.00");
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                lblCeilingOt1.Text = ot1Orignal + "(" + i.ToString() + " * " + ot1Rate.ToString() + ")";
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox.Text = ot1Orignal.ToString("#0.00");
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                lblCeilingOt1.Text = ot1Orignal + "(" + i.ToString() + " * " + ot1Rate.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drOt2 = dsRateCeiling.Tables[0].Select("AddType='C3'");
                                                        if (drOt2.Length > 0)
                                                        {
                                                            double ot2Orignal = j * ot2Rate;
                                                            double ot2New = Convert.ToDouble(drNHHr[0]["OT2"].ToString());
                                                            double ot2Or = j;
                                                            if (ot2Orignal > ot2New && ot2New > 0 && txtbox1.Enabled == true)
                                                            {
                                                                //j = Convert.ToDouble(j / Convert.ToDouble(ot2Orignal / ot2New));
                                                                txtbox1.Text = ot2New.ToString("#0.00");
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                lblCeilingOt2.Text = ot2Orignal + "(" + j.ToString() + " * " + ot2Rate.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox1.Text = ot2Orignal.ToString("#0.00");
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                lblCeilingOt2.Text = ot2Orignal + "(" + j.ToString() + " * " + ot2Rate.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drDays = dsRateCeiling.Tables[0].Select("AddType='C4'");
                                                        if (drDays.Length > 0)
                                                        {
                                                            double daysOrignal = k * daysRate;
                                                            double daysNew = Convert.ToDouble(drNHHr[0]["Days"].ToString());
                                                            double daysOr = k;
                                                            if (daysOrignal > daysNew && daysNew > 0 && txtbox2.Enabled == true)
                                                            {
                                                                //k = Convert.ToDouble(k / Convert.ToDouble(daysOrignal / daysNew));
                                                                txtbox2.Text = daysNew.ToString("#0.00");
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                lblDaysWork.Text = daysOrignal + "(" + k.ToString() + " * " + daysNew.ToString() + ")";
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox2.Text = daysOrignal.ToString("#0.00");
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                lblDaysWork.Text = daysOrignal + "(" + k.ToString() + " * " + daysRate.ToString() + ")";
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //string date = cmbMonth.SelectedValue + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                    string date = strstdatemdy;
                                    //string v1id = Session["V1Id"].ToString();
                                    //string v2id = Session["V2Id"].ToString();
                                    //string v3id = Session["V3Id"].ToString();
                                    //string v4id = Session["V4Id"].ToString();

                                    string v1id = Convert.ToString(Session["C5ID"]);
                                    string v2id = Convert.ToString(Session["C6ID"]);
                                    string v3id = Convert.ToString(Session["C7ID"]);
                                    string v4id = Convert.ToString(Session["C8ID"]);


                                    string C1id = Convert.ToString(Session["C1ID"]);
                                    string C2id = Convert.ToString(Session["C2ID"]);
                                    string C3id = Convert.ToString(Session["C3ID"]);
                                    string C4id = Convert.ToString(Session["C4ID"]);

                                    //Getting multiplication for rate and days
                                    double ratev1 = dblv1 * v1;
                                    double ratev2 = dblv2 * v2;
                                    double ratev3 = dblv3 * v3;
                                    double ratev4 = dblv4 * v4;

                                    string ssqlv1;
                                    string chkv1;
                                    //C5 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    DataSet dschk1 = new DataSet();
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (txtv1.Enabled)
                                    {
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev1 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v1id + "','" + date + "','" + txtv1.Text + "','" + empid + "','" + status + "'," + "'Approved',5)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev1 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set   CelFlag=5,trx_amount='" + txtv1.Text + "' where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtv1.Enabled = false;
                                        }
                                    }

                                    //C5 condition -END

                                    //C6 condition -START
                                    if (txtv2.Enabled)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev2 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v2id + "','" + date + "','" + txtv2.Text + "','" + empid + "','" + status + "'," + "'Approved',6)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev2 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + txtv2.Text + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set CelFlag=6,trx_amount='" + txtv2.Text + "' where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtv2.Enabled = false;
                                        }
                                    }
                                    //C6 condition -END

                                    //C7 condition -START
                                    if (txtv3.Enabled)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev3 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v3id + "','" + date + "','" + txtv3.Text + "','" + empid + "','" + status + "'," + "'Approved',7)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev3 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set CelFlag=7,trx_amount='" + txtv3.Text + "' where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtv3.Enabled = false;
                                        }
                                    }
                                    //C7 condition -END

                                    if (txtv4.Enabled)
                                    {
                                        //C8 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev4 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v4id + "','" + date + "','" + txtv4.Text + "','" + empid + "','" + status + "'," + "'Approved',8)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev4 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set CelFlag=8,trx_amount='" + txtv4.Text + "' where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtv4.Enabled = false;
                                        }
                                    }
                                    //C8 condition -END

                                    if (txtbox3.Enabled)
                                    {
                                        //C1 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + C1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev1 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C1id + "','" + date + "','" + txtbox3.Text + "','" + empid + "','" + status + "'," + "'Approved',1)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev1 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + C1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set   CelFlag=1,trx_amount='" + txtbox3.Text + "' where emp_code='" + empid + "' and trx_type='" + C1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtbox3.Enabled = false;
                                        }
                                    }
                                    //C1 condition -END

                                    //C2 condition -START
                                    if (txtbox.Enabled)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev1 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C2id + "','" + date + "','" + txtbox.Text + "','" + empid + "','" + status + "'," + "'Approved',2)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev1 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set   CelFlag=2,trx_amount='" + txtbox.Text + "' where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        //C2 condition -END
                                        if (flagCeil)
                                        {
                                            txtbox.Enabled = false;
                                        }
                                    }

                                    //C3 condition -START
                                    if (txtbox1.Enabled)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + C3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev1 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C3id + "','" + date + "','" + txtbox1.Text + "','" + empid + "','" + status + "'," + "'Approved',3)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev1 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set   CelFlag=3,trx_amount='" + txtbox1.Text + "' where emp_code='" + empid + "' and trx_type='" + C3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtbox1.Enabled = false;
                                        }
                                        if (flagCeil)
                                        {
                                            txtbox1.Enabled = false;
                                        }
                                    }
                                    //C3 condition -END

                                    if (txtbox2.Enabled)
                                    {
                                        //C4 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + C4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            if (ratev1 > 0)
                                            {
                                                ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C4id + "','" + date + "','" + txtbox2.Text + "','" + empid + "','" + status + "'," + "'Approved',4)";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                        }
                                        else
                                        {
                                            if (ratev1 <= 0)
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            ssqlv1 = "update emp_additions set   CelFlag=4,trx_amount='" + txtbox2.Text + "' where emp_code='" + empid + "' and trx_type='" + C4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        if (flagCeil)
                                        {
                                            txtbox2.Enabled = false;
                                        }
                                    }
                                    //C4 condition -END

                                    if (flagCeil)
                                    {
                                        //item.Enabled = false;
                                        //item.BackColor = Color.LightYellow;
                                        //item.ToolTip = "{CEILING APPLIED}";

                                    }

                                    sSQL = "";
                                    if ((empcode == "") && ((i >= 0) || (j >= 0) || (k >= 0) || (l >= 0)))
                                    {
                                        string newdate = strstdatemdy;
                                        int icnt = 0;
                                        //DataSet monthDs = new DataSet();
                                        //string ssql = "sp_GetPayrollMonth";// 0,2009,2
                                        //SqlParameter[] parms = new SqlParameter[3];
                                        //parms[icnt++] = new SqlParameter("@ROWID", "0");
                                        //parms[icnt++] = new SqlParameter("@YEARS", 0);
                                        //parms[icnt++] = new SqlParameter("@PAYTYPE", 0);
                                        //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                                        //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);

                                        //if (cmbMonth.SelectedItem.Text.ToString().ToLower().IndexOf("first") > 0)
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //else
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "16" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        {
                                            //sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + l + ",'" + newdate + "')";
                                            sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + 0 + "," + 0 + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + l + ",'" + newdate + "')";
                                        }
                                    }
                                    else if ((empcode != ""))  //&& ( (i != 0) || (j !=0) || (k != 0)))
                                    {
                                        //sSQL = "Update emp_overtime set NH_Work=" + l + ", overtime1=" + i + ",overtime2=" + j + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(dblv1) + ",v2=" + Utility.ToDouble(dblv2) + ",v3=" + Utility.ToDouble(dblv3) + ",v4=" + Utility.ToDouble(dblv4) + " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                        sSQL = "Update emp_overtime set NH_Work=" + 0 + ", overtime1=" + 0 + ",overtime2=" + 0 + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(0) + ",v2=" + Utility.ToDouble(0) + ",v3=" + Utility.ToDouble(0) + ",v4=" + Utility.ToDouble(0) + " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                        lblerror.Text = "Updated Sucessfully";
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();     //"Please click the go button and then insert the record for the corresponding month";
                                    }
                                }
                            }
                        }
                    }

                    //  ((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = false;
                }

                if (e.CommandName == "Reset")
                {

                    //Hours/Days 
                    string strHourCeiling = "Select *  from CeilingMaster Where CeilingType=1 And CompanyID=" + comp_id;
                    DataSet dsHourCeiling = DataAccess.FetchRS(CommandType.Text, strHourCeiling, null);

                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true && chkBox.Enabled == true)
                            {
                                bool flagCeil = false;
                                string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));

                                TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");


                                if (empcode == "")
                                {
                                    empcode = empid;
                                }

                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }


                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {
                                    //Get Employee Ceiling for Hours 
                                    //string strEmpCeilingDetails = "Select Top(Select count(*) from CeilingEmployee  Where CompanyId=" + comp_id + ") *  from EmployeeCeilingDetails Where EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year+ " ',103)";
                                    string strEmpCeilingDetails = "Select Top(1) *  from EmployeeCeilingDetails Where Emp_Code=" + empcode + " And EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year + " ',103)  Order By EffDate     Desc";
                                    DataSet dsEmpCeilingDetails = DataAccess.FetchRS(CommandType.Text, strEmpCeilingDetails, null);

                                    double i = Utility.ToDouble(txtbox.Text); //Ot1
                                    double j = Utility.ToDouble(txtbox1.Text);//Ot2
                                    double k = Utility.ToDouble(txtbox2.Text);//Days
                                    double l = Utility.ToDouble(txtbox3.Text);// NH
                                    double dblv1 = Utility.ToDouble(txtv1.Text);//V1
                                    double dblv2 = Utility.ToDouble(txtv2.Text);//V2
                                    double dblv3 = Utility.ToDouble(txtv3.Text);//V3
                                    double dblv4 = Utility.ToDouble(txtv4.Text);//V4

                                    # region Deleted
                                    /*
                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                            if (drNHHr.Length > 0)
                                            {
                                                if (dsHourCeiling.Tables.Count > 0)
                                                {
                                                    if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                                    {
                                                        DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("AddType='C2'");
                                                        if (drOT1.Length > 0)
                                                        {
                                                            if (i > Convert.ToDouble(drNHHr[0][4].ToString()) && Convert.ToDouble(drNHHr[0][4].ToString()) > 0 && txtbox.Enabled == true)
                                                            {
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                //lblCeilingOt1
                                                                lblCeilingOt1.Text = "{" + i.ToString() + "}";
                                                                lblCeilingOt1.ToolTip = lblCeilingOt1.Text;
                                                                i = Convert.ToDouble(drNHHr[0][4].ToString());
                                                                txtbox.Text = i.ToString();
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                //lblCeilingOt1
                                                                lblCeilingOt1.Text = "{" + i.ToString() + "}";
                                                                lblCeilingOt1.ToolTip = lblCeilingOt1.Text;
                                                                //i = Convert.ToDouble(drNHHr[0][4].ToString());
                                                                txtbox.Text = i.ToString();
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }
                                                        DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("AddType='C3'");
                                                        if (drOT2.Length > 0)
                                                        {
                                                            if (j > Convert.ToDouble(drNHHr[0][5].ToString()) && Convert.ToDouble(drNHHr[0][5].ToString()) > 0 && txtbox1.Enabled == true)
                                                            {
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                //lblCeilingOt1
                                                                lblCeilingOt2.Text = "{" + j.ToString() + "}";
                                                                lblCeilingOt2.ToolTip = "{" + j.ToString() + "}";
                                                                j = Convert.ToDouble(drNHHr[0][5].ToString());
                                                                txtbox1.Text = j.ToString();
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                //lblCeilingOt1
                                                                lblCeilingOt2.Text = "{" + j.ToString() + "}";
                                                                lblCeilingOt2.ToolTip = "{" + j.ToString() + "}";
                                                                //j = Convert.ToDouble(drNHHr[0][5].ToString());
                                                                txtbox1.Text = j.ToString();
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drNH = dsHourCeiling.Tables[0].Select("AddType='C1'");
                                                        if (drNH.Length > 0)
                                                        {
                                                            if (l > Convert.ToDouble(drNHHr[0][3].ToString()) && Convert.ToDouble(drNHHr[0][3].ToString()) > 0 && txtbox3.Enabled == true)
                                                            {
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                //lblCeilingOt1
                                                                lblCeilNH.Text = "{" + l.ToString() + "}";
                                                                lblCeilNH.ToolTip = "{" + l.ToString() + "}";
                                                                l = Convert.ToDouble(drNHHr[0][3].ToString());
                                                                txtbox3.Text = l.ToString();
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtbox3.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                //lblCeilingOt1
                                                                lblCeilNH.Text = "{" + l.ToString() + "}";
                                                                lblCeilNH.ToolTip = "{" + l.ToString() + "}";
                                                                //l = Convert.ToDouble(drNHHr[0][3].ToString());
                                                                txtbox3.Text = l.ToString();
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;

                                                            }
                                                        }

                                                        DataRow[] drDays = dsHourCeiling.Tables[0].Select("AddType='C4'");
                                                        if (drDays.Length > 0)
                                                        {
                                                            if (k > Convert.ToDouble(drNHHr[0][6].ToString()) && Convert.ToDouble(drNHHr[0][6].ToString()) > 0 && txtbox2.Enabled == true)
                                                            {
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                //lblCeilingOt1
                                                                lblDaysWork.Text = "{" + k.ToString() + "}";
                                                                lblDaysWork.ToolTip = "{" + k.ToString() + "}";
                                                                k = Convert.ToDouble(drNHHr[0][6].ToString());
                                                                txtbox2.Text = k.ToString();
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtbox2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                //lblCeilingOt1
                                                                lblDaysWork.Text = "{" + k.ToString() + "}";
                                                                lblDaysWork.ToolTip = "{" + k.ToString() + "}";
                                                                //k = Convert.ToDouble(drNHHr[0][6].ToString());
                                                                txtbox2.Text = k.ToString();
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drV1 = dsHourCeiling.Tables[0].Select("AddType='C5'");
                                                        if (drV1.Length > 0)
                                                        {
                                                            if (dblv1 > Convert.ToDouble(drNHHr[0][7].ToString()) && Convert.ToDouble(drNHHr[0][7].ToString()) > 0 && txtv1.Enabled == true)
                                                            {
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblCeilingOt1
                                                                lblV1Ceil.Text = "{" + dblv1.ToString() + "}";
                                                                lblV1Ceil.ToolTip = "{" + dblv1.ToString() + "}";
                                                                dblv1 = Convert.ToDouble(drNHHr[0][7].ToString());
                                                                txtv1.Text = dblv1.ToString();
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtv1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblCeilingOt1
                                                                lblV1Ceil.Text = "{" + dblv1.ToString() + "}";
                                                                lblV1Ceil.ToolTip = "{" + dblv1.ToString() + "}";
                                                                //dblv1 = Convert.ToDouble(drNHHr[0][7].ToString());
                                                                txtv1.Text = dblv1.ToString();
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;

                                                            }
                                                        }

                                                        DataRow[] drV2 = dsHourCeiling.Tables[0].Select("AddType='C6'");
                                                        if (drV2.Length > 0)
                                                        {
                                                            if (dblv2 > Convert.ToDouble(drNHHr[0][8].ToString()) && Convert.ToDouble(drNHHr[0][8].ToString()) > 0 && txtv2.Enabled == true)
                                                            {
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                //lblCeilingOt1
                                                                lblV2Ceil.Text = "{" + dblv2.ToString() + "}";
                                                                lblV2Ceil.ToolTip = "{" + dblv2.ToString() + "}";
                                                                dblv2 = Convert.ToDouble(drNHHr[0][8].ToString());
                                                                txtv2.Text = dblv2.ToString();
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                //txtv2.Enabled = false;
                                                            }
                                                            else
                                                            {

                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                //lblCeilingOt1
                                                                lblV2Ceil.Text = "{" + dblv2.ToString() + "}";
                                                                lblV2Ceil.ToolTip = "{" + dblv2.ToString() + "}";
                                                                //dblv2 = Convert.ToDouble(drNHHr[0][8].ToString());
                                                                txtv2.Text = dblv2.ToString();
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;

                                                            }

                                                        }

                                                        DataRow[] drV3 = dsHourCeiling.Tables[0].Select("AddType='C7'");
                                                        if (drV3.Length > 0)
                                                        {
                                                            if (dblv3 > Convert.ToDouble(drNHHr[0][9].ToString()) && Convert.ToDouble(drNHHr[0][9].ToString()) > 0 && txtv3.Enabled == true)
                                                            {
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                //lblCeilingOt1
                                                                lblV3Ceil.Text = "{" + dblv3.ToString() + "}";
                                                                lblV3Ceil.ToolTip = "{" + dblv3.ToString() + "}";
                                                                dblv3 = Convert.ToDouble(drNHHr[0][9].ToString());
                                                                txtv3.Text = dblv3.ToString();
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtv3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                //lblCeilingOt1
                                                                lblV3Ceil.Text = "{" + dblv3.ToString() + "}";
                                                                lblV3Ceil.ToolTip = "{" + dblv3.ToString() + "}";
                                                                //dblv3 = Convert.ToDouble(drNHHr[0][9].ToString());
                                                                txtv3.Text = dblv3.ToString();
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }

                                                        DataRow[] drV4 = dsHourCeiling.Tables[0].Select("AddType='C8'");
                                                        if (drV4.Length > 0)
                                                        {
                                                            if (dblv4 > Convert.ToDouble(drNHHr[0][10].ToString()) && Convert.ToDouble(drNHHr[0][10].ToString()) > 0 && txtv4.Enabled == true)
                                                            {
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                //lblCeilingOt1
                                                                lblV4Ceil.Text = "{" + dblv4.ToString() + "}";
                                                                lblV4Ceil.ToolTip = "{" + dblv4.ToString() + "}";
                                                                dblv4 = Convert.ToDouble(drNHHr[0][10].ToString());
                                                                txtv4.Text = dblv4.ToString();
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                flagCeil = true;
                                                                // txtv4.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                //lblCeilingOt1
                                                                lblV4Ceil.Text = "{" + dblv4.ToString() + "}";
                                                                lblV4Ceil.ToolTip = "{" + dblv4.ToString() + "}";
                                                                //dblv4 = Convert.ToDouble(drNHHr[0][10].ToString());
                                                                txtv4.Text = dblv4.ToString();
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }*/

                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("Parameter=OT1");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("Parameter=OT2");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drDays = dsHourCeiling.Tables[0].Select("Parameter=Days");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV1 = dsHourCeiling.Tables[0].Select("Parameter=V1");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV2 = dsHourCeiling.Tables[0].Select("Parameter=V2");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV3 = dsHourCeiling.Tables[0].Select("Parameter=V3");
                                    //}
                                    //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                    //{
                                    //    DataRow[] drV4 = dsHourCeiling.Tables[0].Select("Parameter=V4");
                                    //}
                                    //Check Value For Rates

                                    # endregion

                                    status = "U";
                                    double v1, v2, v3, v4;

                                    ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate from employee where emp_code='" + empid + "'";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        v1 = 0;
                                        v2 = 0;
                                        v3 = 0;
                                        v4 = 0;
                                    }
                                    else
                                    {
                                        v1 = Utility.ToDouble(ds.Tables[0].Rows[0]["v1rate"].ToString());
                                        v2 = Utility.ToDouble(ds.Tables[0].Rows[0]["v2rate"].ToString());
                                        v3 = Utility.ToDouble(ds.Tables[0].Rows[0]["v3rate"].ToString());
                                        v4 = Utility.ToDouble(ds.Tables[0].Rows[0]["v4rate"].ToString());
                                    }

                                    //Check for NH , OT1, OT2 , Days
                                    DateTime enddate = new DateTime();
                                    enddate = Convert.ToDateTime(strendatedmy);
                                    DataSet dsProgression = new DataSet();
                                    string strEndDate = enddate.Year.ToString() + "-" + enddate.Month.ToString() + "-" + enddate.Day.ToString() + " 00:00:00.000";
                                    string ssql = "Sp_GetProgressionData";// 0,2009,2
                                    SqlParameter[] parms = new SqlParameter[2];
                                    parms[0] = new SqlParameter("@emp_code", empid);
                                    parms[1] = new SqlParameter("@FromDate", strEndDate);
                                    dsProgression = DataAccess.ExecuteSPDataSet(ssql, parms);

                                    double nhRate = 0.00;
                                    double ot1Rate = 0.00;
                                    double ot2Rate = 0.00;
                                    double daysRate = 0.00;
                                    double hourlyRate = 0.00;

                                    foreach (DataRow drP in dsProgression.Tables[0].Rows)
                                    {
                                        nhRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = Convert.ToDouble(drP["OT1Rate"].ToString());
                                        ot2Rate = Convert.ToDouble(drP["OT2Rate"].ToString());
                                        daysRate = Convert.ToDouble(drP["Daily_Rate"].ToString());
                                        hourlyRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = ot1Rate * hourlyRate;
                                        ot2Rate = ot2Rate * hourlyRate;
                                    }


                                    #region deleted1
                                    /*
                                    //Hours/Days /NH/OT1/OT2/Days
                                    //Amount Parameter No need to Take in consideration
                                    string strRateCeiling = "Select *  from CeilingMaster Where CeilingType=2 And CompanyID=" + comp_id;
                                    DataSet dsRateCeiling = DataAccess.FetchRS(CommandType.Text, strRateCeiling, null);
                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                            if (drNHHr.Length > 0)
                                            {
                                                if (dsRateCeiling.Tables.Count > 0)
                                                {
                                                    if (dsRateCeiling.Tables[0].Rows.Count > 0)
                                                    {
                                                        DataRow[] drV1 = dsRateCeiling.Tables[0].Select("AddType='C5'");
                                                        if (drV1.Length > 0)
                                                        {
                                                            double V1Orignal = dblv1 * v1; //30 * 20 =600 
                                                            double V1New = Convert.ToDouble(drNHHr[0][7].ToString()); //300 
                                                            double dbv1or = dblv1;
                                                            if (V1Orignal > V1New && V1New > 0 && txtv1.Enabled == true)
                                                            {
                                                                //Val = 30/(600/300);        
                                                                //dblv1 =Convert.ToDouble(dblv1/Convert.ToDouble(V1Orignal / V1New));
                                                                txtv1.Text = V1New.ToString("#0.00");
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                lblV1Ceil.Text = V1Orignal.ToString() + " (" + dblv1.ToString() + "*" + v1.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv1.Text = V1Orignal.ToString("#0.00");
                                                                Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                lblV1Ceil.Text = V1Orignal.ToString() + " (" + dblv1.ToString() + "*" + v1.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drV2 = dsRateCeiling.Tables[0].Select("AddType='C6'");

                                                        if (drV2.Length > 0)
                                                        {
                                                            double V2Orignal = dblv2 * v2;
                                                            double V2New = Convert.ToDouble(drNHHr[0][8].ToString());
                                                            double dbv2or = dblv2;
                                                            if (V2Orignal > V2New && V2New > 0 && txtv2.Enabled == true)
                                                            {
                                                                //dblv2= Convert.ToDouble(dblv2 / Convert.ToDouble(V2Orignal / V2New));
                                                                txtv2.Text = V2New.ToString("#0.00");
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                lblV2Ceil.Text = V2Orignal.ToString() + " (" + dblv2.ToString() + "*" + v2.ToString() + ")";
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv2.Text = V2Orignal.ToString("#0.00");
                                                                Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                lblV2Ceil.Text = V2Orignal.ToString() + " (" + dblv2.ToString() + "*" + v2.ToString() + ")";
                                                            }
                                                        }



                                                        DataRow[] drV3 = dsRateCeiling.Tables[0].Select("AddType='C7'");
                                                        if (drV3.Length > 0)
                                                        {
                                                            double V3Orignal = dblv3 * v3;
                                                            double V3New = Convert.ToDouble(drNHHr[0][9].ToString());
                                                            double dbv3or = dblv3;
                                                            if (V3Orignal > V3New && V3New > 0 && txtv3.Enabled == true)
                                                            {
                                                                //dblv3 = Convert.ToDouble(dblv3 / Convert.ToDouble(V3Orignal / V3New));
                                                                txtv3.Text = V3New.ToString("#0.00");
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                lblV3Ceil.Text = V3Orignal.ToString() + " (" + dblv3.ToString() + "*" + v3.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtv3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv3.Text = V3Orignal.ToString("#0.00");
                                                                Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                lblV3Ceil.Text = V3Orignal.ToString() + " (" + dblv3.ToString() + "*" + v3.ToString() + ")";
                                                            }
                                                        }

                                                        DataRow[] drV4 = dsRateCeiling.Tables[0].Select("AddType='C8'");
                                                        if (drV4.Length > 0)
                                                        {
                                                            double V4Orignal = dblv4 * v4;
                                                            double V4New = Convert.ToDouble(drNHHr[0][10].ToString());
                                                            double dbv4or = dblv4;
                                                            if (V4Orignal > V4New && V4New > 0 && txtv4.Enabled == true)
                                                            {
                                                                //dblv4 = Convert.ToDouble(dblv4 / Convert.ToDouble(V4Orignal / V4New));
                                                                txtv4.Text = V4New.ToString("#0.00");
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                lblV4Ceil.Text = V4Orignal.ToString() + " (" + dblv4.ToString() + "*" + v4.ToString() + ")";
                                                                flagCeil = true;
                                                                //txtv4.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtv4.Text = V4Orignal.ToString("#0.00");
                                                                Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                lblV4Ceil.Text = V4Orignal.ToString() + " (" + dblv4.ToString() + "*" + v4.ToString() + ")";
                                                            }
                                                        }


                                                        DataRow[] drNH = dsRateCeiling.Tables[0].Select("AddType='C1'");
                                                        if (drNH.Length > 0)
                                                        {
                                                            double nhOrignal = l * nhRate;
                                                            double nhNew = Convert.ToDouble(drNHHr[0]["NH"].ToString());
                                                            double nhOr = l;
                                                            if (nhOrignal > nhNew && nhNew > 0 && txtbox3.Enabled == true)
                                                            {
                                                                //l = Convert.ToDouble(l / Convert.ToDouble(nhOrignal / nhNew));
                                                                txtbox3.Text = nhNew.ToString("#0.00");
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                lblCeilNH.Text = nhOrignal + "(" + l.ToString() + " * " + nhRate.ToString() + ")";//dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox3.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox3.Text = nhOrignal.ToString("#0.00");
                                                                Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                lblCeilNH.Text = nhOrignal + "(" + l.ToString() + " * " + nhRate.ToString() + ")";//dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                            }
                                                        }



                                                        DataRow[] drOt1 = dsRateCeiling.Tables[0].Select("AddType='C2'");
                                                        if (drOt1.Length > 0)
                                                        {
                                                            double ot1Orignal = i * ot1Rate;
                                                            double ot1New = Convert.ToDouble(drNHHr[0]["OT1"].ToString());
                                                            double ot1Or = i;
                                                            if (ot1Orignal > ot1New && ot1New > 0 && txtbox.Enabled == true)
                                                            {
                                                                //i = Convert.ToDouble(i / Convert.ToDouble(ot1Orignal / ot1New));
                                                                txtbox.Text = ot1New.ToString("#0.00");
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                lblCeilingOt1.Text = ot1Orignal + "(" + i.ToString() + " * " + ot1Rate.ToString() + ")";
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox.Text = ot1Orignal.ToString("#0.00");
                                                                Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                lblCeilingOt1.Text = ot1Orignal + "(" + i.ToString() + " * " + ot1Rate.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drOt2 = dsRateCeiling.Tables[0].Select("AddType='C3'");
                                                        if (drOt2.Length > 0)
                                                        {
                                                            double ot2Orignal = j * ot2Rate;
                                                            double ot2New = Convert.ToDouble(drNHHr[0]["OT2"].ToString());
                                                            double ot2Or = j;
                                                            if (ot2Orignal > ot2New && ot2New > 0 && txtbox1.Enabled == true)
                                                            {
                                                                //j = Convert.ToDouble(j / Convert.ToDouble(ot2Orignal / ot2New));
                                                                txtbox1.Text = ot2New.ToString("#0.00");
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                lblCeilingOt2.Text = ot2Orignal + "(" + j.ToString() + " * " + ot2Rate.ToString() + ")";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox1.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox1.Text = ot2Orignal.ToString("#0.00");
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                lblCeilingOt2.Text = ot2Orignal + "(" + j.ToString() + " * " + ot2Rate.ToString() + ")";

                                                            }
                                                        }

                                                        DataRow[] drDays = dsRateCeiling.Tables[0].Select("AddType='C4'");
                                                        if (drDays.Length > 0)
                                                        {
                                                            double daysOrignal = k * daysRate;
                                                            double daysNew = Convert.ToDouble(drNHHr[0]["Days"].ToString());
                                                            double daysOr = k;
                                                            if (daysOrignal > daysNew && daysNew > 0 && txtbox2.Enabled == true)
                                                            {
                                                                //k = Convert.ToDouble(k / Convert.ToDouble(daysOrignal / daysNew));
                                                                txtbox2.Text = daysNew.ToString("#0.00");
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                lblDaysWork.Text = daysOrignal + "(" + k.ToString() + " * " + daysNew.ToString() + ")";
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                                flagCeil = true;
                                                                //txtbox2.Enabled = false;
                                                            }
                                                            else
                                                            {
                                                                txtbox2.Text = daysOrignal.ToString("#0.00");
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                lblDaysWork.Text = daysOrignal + "(" + k.ToString() + " * " + daysRate.ToString() + ")";
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    */
                                    #endregion
                                    //string date = cmbMonth.SelectedValue + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                    string date = strstdatemdy;
                                    //string v1id = Session["V1Id"].ToString();
                                    //string v2id = Session["V2Id"].ToString();
                                    //string v3id = Session["V3Id"].ToString();
                                    //string v4id = Session["V4Id"].ToString();

                                    string v1id = Convert.ToString(Session["C5ID"]);
                                    string v2id = Convert.ToString(Session["C6ID"]);
                                    string v3id = Convert.ToString(Session["C7ID"]);
                                    string v4id = Convert.ToString(Session["C8ID"]);


                                    string C1id = Convert.ToString(Session["C1ID"]);
                                    string C2id = Convert.ToString(Session["C2ID"]);
                                    string C3id = Convert.ToString(Session["C3ID"]);
                                    string C4id = Convert.ToString(Session["C4ID"]);

                                    //Getting multiplication for rate and days
                                    double ratev1 = dblv1 * v1;
                                    double ratev2 = dblv2 * v2;
                                    double ratev3 = dblv3 * v3;
                                    double ratev4 = dblv4 * v4;

                                    string ssqlv1;
                                    string chkv1;
                                    //C5 condition -START
                                    DataSet dschk1 = new DataSet();
                                    if (v1id != "")
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";

                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    }
                                    if (txtv1.Enabled == false)
                                    {
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev1 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v1id + "','" + date + "','" + txtv1.Text + "','" + empid + "','" + status + "'," + "'Approved',5)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            // if (ratev1 <= 0)
                                            // {
                                            if (v1id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            // }
                                            //ssqlv1 = "update emp_additions set   CelFlag=5,trx_amount='" + txtv1.Text + "' where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            //int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtv1.Enabled = true;
                                        }
                                    }

                                    //C5 condition -END

                                    //C6 condition -START
                                    if (txtv2.Enabled == false)
                                    {
                                        if (v2id != "")
                                        {
                                            chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + v2id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        }
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev2 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v2id + "','" + date + "','" + txtv2.Text + "','" + empid + "','" + status + "'," + "'Approved',6)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            //if (ratev2 <= 0)
                                            // {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            // }
                                            // ssqlv1 = "update emp_additions set CelFlag=6,trx_amount='" + txtv2.Text + "' where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            // int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtv2.Enabled = true;
                                        }

                                    }
                                    //C6 condition -END

                                    //C7 condition -START
                                    if (txtv3.Enabled == false)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + v3id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev3 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v3id + "','" + date + "','" + txtv3.Text + "','" + empid + "','" + status + "'," + "'Approved',7)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            if (v3id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + v3id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            // ssqlv1 = "update emp_additions set CelFlag=7,trx_amount='" + txtv3.Text + "' where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            // int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtv3.Enabled = true;
                                        }
                                    }
                                    //C7 condition -END

                                    if (txtv4.Enabled == false)
                                    {
                                        //C8 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + v4id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev4 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + v4id + "','" + date + "','" + txtv4.Text + "','" + empid + "','" + status + "'," + "'Approved',8)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            if (v4id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + v4id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            //ssqlv1 = "update emp_additions set CelFlag=8,trx_amount='" + txtv4.Text + "' where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            //int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtv4.Enabled = true;
                                        }
                                    }
                                    //C8 condition -END

                                    if (txtbox3.Enabled == false)
                                    {
                                        //C1 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + C1id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            // //if (ratev1 > 0)
                                            //// {
                                            //     ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C1id + "','" + date + "','" + txtbox3.Text + "','" + empid + "','" + status + "'," + "'Approved',1)";
                                            //     int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            // }
                                        }
                                        else
                                        {
                                            if (C1id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + C1id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            // ssqlv1 = "update emp_additions set   CelFlag=1,trx_amount='" + txtbox3.Text + "' where emp_code='" + empid + "' and trx_type='" + C1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            // int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtbox3.Enabled = true;
                                        }
                                    }
                                    //C1 condition -END

                                    //C2 condition -START
                                    if (txtbox.Enabled == false)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + C2id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev1 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C2id + "','" + date + "','" + txtbox.Text + "','" + empid + "','" + status + "'," + "'Approved',2)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            if (C2id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + C2id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            // ssqlv1 = "update emp_additions set   CelFlag=2,trx_amount='" + txtbox.Text + "' where emp_code='" + empid + "' and trx_type='" + C2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            // int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtbox.Enabled = true;
                                        }
                                        //C2 condition -END                                        
                                    }

                                    //C3 condition -START
                                    if (txtbox1.Enabled == false)
                                    {
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + C3id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            //if (ratev1 > 0)
                                            //{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C3id + "','" + date + "','" + txtbox1.Text + "','" + empid + "','" + status + "'," + "'Approved',3)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            if (C3id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + C3id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            // ssqlv1 = "update emp_additions set   CelFlag=3,trx_amount='" + txtbox1.Text + "' where emp_code='" + empid + "' and trx_type='" + C3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            // int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtbox1.Enabled = true;
                                        }
                                    }
                                    //C3 condition -END

                                    if (txtbox2.Enabled == false)
                                    {
                                        //C4 condition -START
                                        chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type=" + C4id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        dschk1 = new DataSet();
                                        dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                        if (dschk1.Tables[0].Rows.Count == 0)
                                        {
                                            ////if (ratev1 > 0)
                                            ////{
                                            //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus,CelFlag) values('" + C4id + "','" + date + "','" + txtbox2.Text + "','" + empid + "','" + status + "'," + "'Approved',4)";
                                            //    int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            //}
                                        }
                                        else
                                        {
                                            if (C4id != "")
                                            {
                                                ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type=" + C4id + " and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            }
                                            //ssqlv1 = "update emp_additions set   CelFlag=4,trx_amount='" + txtbox2.Text + "' where emp_code='" + empid + "' and trx_type='" + C4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            //int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                            txtbox2.Enabled = true;
                                        }
                                    }
                                    //C4 condition -END

                                    if (flagCeil)
                                    {
                                        //item.Enabled = false;
                                        //item.BackColor = Color.LightYellow;
                                        //item.ToolTip = "{CEILING APPLIED}";

                                    }

                                    sSQL = "";
                                    if ((empcode == "") && ((i >= 0) || (j >= 0) || (k >= 0) || (l >= 0)))
                                    {
                                        string newdate = strstdatemdy;
                                        int icnt = 0;
                                        //DataSet monthDs = new DataSet();
                                        //string ssql = "sp_GetPayrollMonth";// 0,2009,2
                                        //SqlParameter[] parms = new SqlParameter[3];
                                        //parms[icnt++] = new SqlParameter("@ROWID", "0");
                                        //parms[icnt++] = new SqlParameter("@YEARS", 0);
                                        //parms[icnt++] = new SqlParameter("@PAYTYPE", 0);
                                        //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                                        //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);

                                        //if (cmbMonth.SelectedItem.Text.ToString().ToLower().IndexOf("first") > 0)
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //else
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "16" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        {
                                            //sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + l + ",'" + newdate + "')";
                                            //sSQL = "DELETE FRM  emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + 0 + "," + 0 + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + Utility.ToDouble(0) + "," + l + ",'" + newdate + "')";
                                        }
                                    }
                                    else if ((empcode != ""))  //&& ( (i != 0) || (j !=0) || (k != 0)))
                                    {
                                        //sSQL = "Update emp_overtime set NH_Work=" + l + ", overtime1=" + i + ",overtime2=" + j + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(dblv1) + ",v2=" + Utility.ToDouble(dblv2) + ",v3=" + Utility.ToDouble(dblv3) + ",v4=" + Utility.ToDouble(dblv4) + " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                        // sSQL = "DELETE FROM  emp_overtime  where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                        lblerror.Text = "Records Reset Successfully";

                                        txtbox.Text = "0";
                                        txtbox1.Text = "0";
                                        txtbox2.Text = "0";
                                        txtbox3.Text = "0";
                                        txtv1.Text = "0";
                                        txtv2.Text = "0";
                                        txtv3.Text = "0";
                                        txtv4.Text = "0";
                                        //bingrid1();
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();     //"Please click the go button and then insert the record for the corresponding month";
                                    }
                                }
                            }
                        }
                    }
                    //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = true;

                }
                if (e.CommandName == "UpdateAll")
                {
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));

                                TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");

                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {


                                    double i = Utility.ToDouble(txtbox.Text);
                                    double j = Utility.ToDouble(txtbox1.Text);
                                    double k = Utility.ToDouble(txtbox2.Text);
                                    double l = Utility.ToDouble(txtbox3.Text);
                                    double dblv1 = Utility.ToDouble(txtv1.Text);
                                    double dblv2 = Utility.ToDouble(txtv2.Text);
                                    double dblv3 = Utility.ToDouble(txtv3.Text);
                                    double dblv4 = Utility.ToDouble(txtv4.Text);


                                    status = "U";
                                    double v1, v2, v3, v4;

                                    ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate from employee where emp_code='" + empid + "'";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        v1 = 0;
                                        v2 = 0;
                                        v3 = 0;
                                        v4 = 0;
                                    }
                                    else
                                    {
                                        v1 = Utility.ToDouble(ds.Tables[0].Rows[0]["v1rate"].ToString());
                                        v2 = Utility.ToDouble(ds.Tables[0].Rows[0]["v2rate"].ToString());
                                        v3 = Utility.ToDouble(ds.Tables[0].Rows[0]["v3rate"].ToString());
                                        v4 = Utility.ToDouble(ds.Tables[0].Rows[0]["v4rate"].ToString());
                                    }

                                    //string date = cmbMonth.SelectedValue + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                    string date = strstdatemdy;
                                    //string v1id = Session["V1Id"].ToString();
                                    //string v2id = Session["V2Id"].ToString();
                                    //string v3id = Session["V3Id"].ToString();
                                    //string v4id = Session["V4Id"].ToString();

                                    string v1id = Convert.ToString(Session["C5ID"]);
                                    string v2id = Convert.ToString(Session["C6ID"]);
                                    string v3id = Convert.ToString(Session["C7ID"]);
                                    string v4id = Convert.ToString(Session["C8ID"]);


                                    //Getting multiplication for rate and days
                                    double ratev1 = dblv1 * v1;
                                    double ratev2 = dblv2 * v2;
                                    double ratev3 = dblv3 * v3;
                                    double ratev4 = dblv4 * v4;

                                    string ssqlv1;
                                    string chkv1;
                                    //variable1 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    DataSet dschk1 = new DataSet();
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev1 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v1id + "','" + date + "','" + ratev1 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev1 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev1 + "' where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable1 condition -END

                                    //variable2 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev2 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v2id + "','" + date + "','" + ratev2 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev2 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev2 + "' where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable2 condition -END

                                    //variable3 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev3 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v3id + "','" + date + "','" + ratev3 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev3 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev3 + "' where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable3 condition -END

                                    //variable4 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev4 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v4id + "','" + date + "','" + ratev4 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev4 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev4 + "' where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable4 condition -END

                                    sSQL = "";
                                    if ((empcode == "") && ((i >= 0) || (j >= 0) || (k >= 0) || (l >= 0)))
                                    {
                                        string newdate = strstdatemdy;
                                        int icnt = 0;
                                        //DataSet monthDs = new DataSet();
                                        //string ssql = "sp_GetPayrollMonth";// 0,2009,2
                                        //SqlParameter[] parms = new SqlParameter[3];
                                        //parms[icnt++] = new SqlParameter("@ROWID", "0");
                                        //parms[icnt++] = new SqlParameter("@YEARS", 0);
                                        //parms[icnt++] = new SqlParameter("@PAYTYPE", 0);
                                        //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                                        //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);

                                        //if (cmbMonth.SelectedItem.Text.ToString().ToLower().IndexOf("first") > 0)
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //else
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "16" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        {
                                            sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + l + ",'" + newdate + "')";
                                        }
                                    }
                                    else if ((empcode != ""))  //&& ( (i != 0) || (j !=0) || (k != 0)))
                                    {
                                        sSQL = "Update emp_overtime set NH_Work=" + l + ", overtime1=" + i + ",overtime2=" + j + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(dblv1) + ",v2=" + Utility.ToDouble(dblv2) + ",v3=" + Utility.ToDouble(dblv3) + ",v4=" + Utility.ToDouble(dblv4) + " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                        lblerror.Text = "Updated Sucessfully";
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();     //"Please click the go button and then insert the record for the corresponding month";
                                    }
                                }
                            }
                        }
                    }
                    // RadGrid1.DataBind();
                }
                if (e.CommandName == "CalcOverVar")
                {
                    string time_card_no = "";
                    string srSql;
                    string empcode;
                    string empid;
                    int id;
                    string strInTime = "";
                    string strOutTime = "";
                    string strOT = "";
                    double intot1;
                    double intotcalc;
                    int inthour;
                    int intmin;
                    double inttotmin;
                    double intworkinghours;
                    double intworkingmints;
                    double intworkingtotmin;
                    double intbalancemin = 0;
                    double intdaysworkedhrmin = 0;
                    double intdaysworked = 0;
                    double intdaysworkedhrsmon = 0;
                    double intdaysworkedminmon = 0;
                    //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = true;

                    //srSql = "Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105)) date,convert(varchar, timeentry, 8) timeentry From actatek_logs  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%IN%' and A.softdelete=0 and Month(CONVERT(datetime,timeentry, 105) )=" + cmbMonth.SelectedValue + " and year(CONVERT(datetime,timeentry, 105) )=" + cmbYear.SelectedValue + " Order By timeentry";
                    //srSql = srSql + ";Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105) ) date,convert(varchar, timeentry, 8) timeentry From actatek_logs  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%OUT%' and A.softdelete=0 and Month(CONVERT(datetime,timeentry, 105) )=" + cmbMonth.SelectedValue + " and year(CONVERT(datetime,timeentry, 105) )=" + cmbYear.SelectedValue + " Order By timeentry Desc";
                    //srSql = srSql + ";Select * From DaysInMonth where month=" + cmbMonth.SelectedValue + " and year=" + cmbYear.SelectedValue;
                    //srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where year(CONVERT(datetime,Res.[month], 103))= " + cmbYear.SelectedValue + " and Month(CONVERT(datetime,Res.[month], 103))= " + cmbMonth.SelectedValue + "  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where Month(TimeEntryStart)=" + cmbMonth.SelectedValue + " and Year(TimeEntryStart)=" + cmbYear.SelectedValue + ") D Group By Time_Card_No";
                    srSql = "Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105)) date,convert(varchar, timeentry, 8) timeentry From actatek_logs_proxy  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%IN%' and A.softdelete=0 and (CONVERT(datetime,timeentry, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,timeentry, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105))  Order By timeentry";
                    srSql = srSql + ";Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105) ) date,convert(varchar, timeentry, 8) timeentry From actatek_logs_proxy  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%OUT%' and A.softdelete=0 and (CONVERT(datetime,timeentry, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,timeentry, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105)) Order By timeentry Desc";
                    srSql = srSql + ";Select DATEDIFF(day,Convert(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103),Convert(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103)) calendar_days";
                    srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where (CONVERT(datetime,InTime, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103) And CONVERT(datetime,OutTime, 103)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103))  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryEnd,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";
                    srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";

                    string strsqlts1 = "Select (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103)) TimeEntryStart,Time_Card_No, NH,OT1,OT2,TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103)) And SoftDelete=0";
                    DataSet dsphdataset = new DataSet();
                    dsphdataset = DataAccess.FetchRS(CommandType.Text, strsqlts1, null);
                    decimal PHNH=0;
                    decimal nhpday = 0;
                    if (dsphdataset != null)
                    {
                        if (dsphdataset.Tables.Count > 0)
                        {
                            foreach (DataRow dr1 in dsphdataset.Tables[0].Rows)
                            {   
                                if(dr1["NH"].ToString()!="0")
                                {
                                    nhpday= Convert.ToDecimal(dr1["NH"].ToString());
                                }
                                if (dr1["NH"].ToString() == "0")
                                {
                                    if (nhpday == 0)
                                    {
                                        if (dsphdataset.Tables[0].Rows.Count > 1)
                                        {
                                            nhpday = Convert.ToDecimal(dsphdataset.Tables[0].Rows[1]["NH"].ToString());
                                        }
                                    }
                                    PHNH = PHNH + nhpday;    
                                }
                            }
                        }
                    }

                    int intbldcount = 5;

                    DataSet ds = new DataSet();
                    DataSet dsTS = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, srSql, null);
                    int intCountV1 = 0;
                    int intCountV2 = 0;
                    int intCountV3 = 0;
                    int intCountV4 = 0;
                    double v1, v2, v3, v4, wdayperweek;
                    int intdaysinmonth = 0;

                    if (ds.Tables.Count == intbldcount)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            intdaysinmonth = Convert.ToInt32(ds.Tables[2].Rows[0]["calendar_days"]);
                        }
                    }

                    //SqlParameter[] parms1 = new SqlParameter[7];
                    //parms1[0] = new SqlParameter("@start_date", rdFrom.SelectedDate.ToString("dd/MM/yyyy"));
                    //parms1[1] = new SqlParameter("@end_date", rdEnd.SelectedDate.ToString("dd/MM/yyyy"));
                    //parms1[2] = new SqlParameter("@compid", comp_id);
                    //parms1[3] = new SqlParameter("@isEmpty", "No");
                    //parms1[4] = new SqlParameter("@empid", "-1");
                    //parms1[5] = new SqlParameter("@subprojid", "-1");
                    //parms1[6] = new SqlParameter("@sessid", 0);
                    //dsTS = DataAccess.ExecuteSPDataSet("sp_ProcessTimesheet", parms1);



                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true && chkBox.Enabled == true)
                            {
                                intCountV1 = 0;
                                intCountV2 = 0;
                                intCountV3 = 0;
                                intCountV4 = 0;
                                srSql = "";
                                empcode = "";
                                empid = "";
                                empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                                time_card_no = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("time_card_no"));
                                TextBox txtot1 = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtot2 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                                TextBox txtdayswork = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtNHWork = (TextBox)dataItem.FindControl("txtNHWork");

                                if (empcode == "")
                                {

                                    empcode = empid;
                                }

                                strInTime = "";
                                strOutTime = "";
                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and month(h.start_period)='" + cmbMonth.SelectedValue + "' and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {
                                    if (ds.Tables.Count == intbldcount)
                                    {
                                        DataRow[] dr;
                                        DataRow[] drpubholday;

                                        ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate, ot_entitlement,pay_frequency, payrate,wdays_per_week from employee where emp_code='" + empid + "'";
                                        //here check if employee is Hourly then one need to just total the hours for each day....
                                        DataSet dsRate = new DataSet();
                                        dsRate = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                        if (dsRate.Tables[0].Rows.Count == 0)
                                        {
                                            v1 = 0;
                                            v2 = 0;
                                            v3 = 0;
                                            v4 = 0;
                                            wdayperweek = 0;
                                        }
                                        else
                                        {
                                            v1 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v1rate"].ToString());
                                            v2 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v2rate"].ToString());
                                            v3 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v3rate"].ToString());
                                            v4 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v4rate"].ToString());
                                            wdayperweek = Utility.ToDouble(dsRate.Tables[0].Rows[0]["wdays_per_week"].ToString());
                                            strOT = dsRate.Tables[0].Rows[0]["ot_entitlement"].ToString().Trim().ToUpper();
                                            //dataItem["v1"].Text = v1.ToString();
                                        }

                                        if ((strOT == "Y") || (txtdayswork.Enabled == true))
                                        {
                                            intdaysworkedhrmin = 0;
                                            intdaysworkedhrsmon = 0;
                                            intdaysworkedminmon = 0;
                                            txtot1.Text = "0";
                                            txtot2.Text = "0";
                                            intdaysworked = 0;
                                            intdaysworkedhrmin = Utility.ToDouble(Session["WorkingHours"]) + Utility.ToDouble(Session["WorkingMinutes"]);
                                            string NH = "0";

                                            if (time_card_no.ToString().Trim().Length > 0)
                                            {
                                                dr = ds.Tables[4].Select("Time_Card_No ='" + time_card_no + "'");                                            

                                                if (dr.Length > 0)
                                                {
                                                    decimal nhtemp = 0;
                                                    nhtemp = Convert.ToDecimal(dr[0]["NH"].ToString()) + PHNH; 
                                                    dataItem["NH"].Text = dr[0]["NH"].ToString();
                                                    //Check of PH is there and then transfer Data to 
                                                    NH = dr[0]["NH"].ToString();
                                                    NH = nhtemp.ToString();                                                 
                                                    if (dr[0]["NH"].ToString() == "0.00" || dr[0]["NH"].ToString() == "")
                                                    {
                                                        txtNHWork.Text = "";
                                                    }
                                                    else
                                                    {
                                                        if (txtNHWork.Enabled == true)
                                                        {
                                                            txtNHWork.Text = dr[0]["NH"].ToString();
                                                        }
                                                        else
                                                        {
                                                            txtNHWork.Text = "";
                                                        }

                                                        dr[0].BeginEdit();
                                                        dr[0]["NH"] = NH.ToString();
                                                        dr[0].AcceptChanges();

                                                        if (txtdayswork.Enabled)
                                                        {
                                                            if (Session["C4Formula"] != null)
                                                            {
                                                                if (Session["C4Formula"].ToString() != "0")
                                                                {
                                                                    if (dr[0]["NH"].ToString() != "0" && dr[0]["NH"].ToString() != "")
                                                                    {
                                                                        double hours = (Convert.ToDouble(dr[0]["NH"].ToString()) + Convert.ToDouble(PHNH)) / 8;
                                                                        txtdayswork.Text = hours.ToString();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }                                                    
                                                    if (dr[0]["OT1"].ToString() == "0.00" || dr[0]["OT1"].ToString() == "")
                                                    {
                                                        txtot1.Text = "";
                                                    }
                                                    else
                                                    {
                                                        txtot1.Text = dr[0]["OT1"].ToString();
                                                    }
                                                    if (dr[0]["OT2"].ToString() == "0.00" || dr[0]["OT2"].ToString() == "")
                                                    {
                                                        txtot2.Text = "";
                                                    }
                                                    else
                                                    {
                                                        txtot2.Text = dr[0]["OT2"].ToString();
                                                    }
                                                }
                                            }
                                            for (int i = 1; i <= intdaysinmonth; i++)
                                            {
                                                strInTime = "";
                                                strOutTime = "";
                                                dr = ds.Tables[0].Select("Emp_Code ='" + empid + "' And Date=" + i);

                                                drpubholday = ds.Tables[3].Select("Emp_Code ='" + empid + "' And Date=" + i);

                                                if (dr.Length > 0)
                                                {
                                                    strInTime = dr[0]["timeentry"].ToString();
                                                }

                                                dr = ds.Tables[1].Select("Emp_Code ='" + empid + "' And Date=" + i);

                                                if (dr.Length > 0)
                                                {
                                                    strOutTime = dr[0]["timeentry"].ToString();
                                                }

                                                if (strInTime != "" && strOutTime != "")
                                                {
                                                    #region "DELETED"
                                                    //if (Session["TimeSheetApproved"] != null)
                                                    //{
                                                    //    if (Session["TimeSheetApproved"].ToString() == "1") //Timesheet Approved
                                                    //    {
                                                    //        try
                                                    //        {
                                                    //            DateTime dtIn = DateTime.Parse(strInTime, format);
                                                    //            DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                    //            SMEDateTime timein = new SMEDateTime(dtIn.Hour, dtIn.Minute, dtIn.Second);
                                                    //            SMEDateTime timeout = new SMEDateTime(dtOut.Hour, dtOut.Minute, dtOut.Second);
                                                    //            SMEDateTime smetime = new SMEDateTime();

                                                    //            inthour = SMEDateTime.TimeDiff(timeout, timein).Hour;
                                                    //            intmin = SMEDateTime.TimeDiff(timeout, timein).Minute;
                                                    //            inttotmin = (inthour * 60) + intmin;

                                                    //            intworkinghours = Utility.ToDouble(Session["WorkingHours"]) * 60;
                                                    //            intworkingmints = Utility.ToDouble(Session["WorkingMinutes"]);
                                                    //            intworkingtotmin = intworkinghours + intworkingmints;
                                                    //            intbalancemin = 0;

                                                    //            if (inttotmin > intworkingtotmin) //worked hrs is more than the expected hours i.e ot
                                                    //            {
                                                    //                if (wdayperweek == 5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat" || dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For saturday and sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 5.5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - (intworkingtotmin / 2); // For saturday and half day as OT
                                                    //                    }
                                                    //                    else if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 6)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }
                                                    //                if (wdayperweek == 7)
                                                    //                {
                                                    //                    intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                }

                                                    //                if (strOT == "Y")
                                                    //                {
                                                    //                    if (intbalancemin > 0)
                                                    //                    {

                                                    //                        if (drpubholday.Length > 0)
                                                    //                        {
                                                    //                            if (drpubholday[0]["id"] != DBNull.Value)
                                                    //                            {
                                                    //                                // This is a Public Holdiday hence whole day worked should be counted
                                                    //                                intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                                intotcalc = Utility.ToDouble(inttotmin / 60) + intot1;
                                                    //                                txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                            }
                                                    //                            else
                                                    //                            {
                                                    //                                //This is a Sunday type of holiday
                                                    //                                intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                                intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                                txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                            }
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            intot1 = Utility.ToDouble(txtot1.Text);
                                                    //                            intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                            txtot1.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                    }
                                                    //                    //if OT entitled yes
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + (inttotmin - intbalancemin);
                                                    //                }
                                                    //                else
                                                    //                {
                                                    //                    //if OT Entit is no then all the hours will b treated in days hour worked
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + inttotmin;
                                                    //                }
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                //total time for the day is worked is less than the actual working time.
                                                    //                string eid = empid;
                                                    //                intbalancemin = Math.Abs(intbalancemin);

                                                    //                if (wdayperweek == 5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat" || dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For saturday and sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days is 0 since it is less 
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 5.5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat")
                                                    //                    {
                                                    //                        if (intbalancemin > (intworkingtotmin / 2)) // For saturday and half day as OT
                                                    //                        {
                                                    //                            intbalancemin = (intbalancemin - (intworkingtotmin / 2));
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            intbalancemin = 0;
                                                    //                        }

                                                    //                    }
                                                    //                    else if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        // For sunday worked hour which is already calcuated will be done.
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days is 0 since it is less 
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 6)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        // For sunday worked hour which is already calcuated will be done.
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }
                                                    //                if (wdayperweek == 7)
                                                    //                {
                                                    //                    intbalancemin = 0; //overtime hour to be charged for regular days
                                                    //                }
                                                    //                if (strOT == "Y")
                                                    //                {
                                                    //                    if (drpubholday.Length > 0)
                                                    //                    {
                                                    //                        if (drpubholday[0]["id"] != DBNull.Value)
                                                    //                        {
                                                    //                            // This is a Public Holdiday hence whole day worked should be counted
                                                    //                            intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                            intotcalc = Utility.ToDouble(inttotmin / 60) + intot1;
                                                    //                            txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            //This is a Sunday type of holiday
                                                    //                            intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                            intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                            txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intot1 = Utility.ToDouble(txtot1.Text);
                                                    //                        intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                        txtot1.Text = intotcalc.ToString("#0.00");
                                                    //                    }
                                                    //                    //if OT entitled yes
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + (inttotmin - intbalancemin);
                                                    //                }
                                                    //                else
                                                    //                {
                                                    //                    //if OT Entit is no then all the hours will b treated in days hour worked
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + inttotmin;
                                                    //                }
                                                    //            }


                                                    //        }
                                                    //        catch (Exception ex)
                                                    //        {
                                                    //            throw ex;
                                                    //        }
                                                    //    }
                                                    //}
                                                    #endregion

                                                    //If Variable Pay is Applicable...
                                                    if (txtv1.Enabled == true)
                                                    {
                                                        if (Session["C5Formula"] != null)
                                                        {
                                                            if (Session["C5Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV1 = intCountV1 + 1;
                                                                if (txtNHWork.Enabled == true || NH!="0")
                                                                {
                                                                    if (Session["C5FormulaCalc"].ToString() == "")
                                                                    {
                                                                        intCountV1 = Convert.ToInt32(Convert.ToDouble(NH) / 8);//dr[0]["NH"].ToString()
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Session["C5FormulaCalc"].ToString() != "0")
                                                                        {
                                                                            //intCountV1 = Convert.ToInt32(Convert.ToDouble(NH) / Convert.ToInt32(Session["C5FormulaCalc"].ToString()));
                                                                        }

                                                                    }
                                                                }

                                                            }
                                                            if (Session["C5Formula"].ToString() == "2") //TIME
                                                            {
                                                                DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["C5FormulaCalc"].ToString());
                                                                DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                {
                                                                    intCountV1 = intCountV1 + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    //If Variable Pay is Applicable...
                                                    if (txtv2.Enabled == true)
                                                    {
                                                        if (Session["C6Formula"] != null)
                                                        {
                                                            if (Session["C6Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV2 = intCountV2 + 1;
                                                                if (txtNHWork.Enabled == true || NH!="0")
                                                                {
                                                                    if (Session["C6FormulaCalc"].ToString() == "")
                                                                    {
                                                                        intCountV2 = Convert.ToInt32(Convert.ToDouble(NH) / 8);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Session["C6FormulaCalc"].ToString() != "0")
                                                                        {
                                                                            //intCountV2 = Convert.ToInt32(Convert.ToDouble(NH) / Convert.ToInt32(Session["C6FormulaCalc"].ToString()));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (Session["C6Formula"].ToString() == "2") //TIME
                                                            {
                                                                DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["C6FormulaCalc"].ToString());
                                                                DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                {
                                                                    intCountV2 = intCountV2 + 1;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    //If Variable Pay is Applicable...
                                                    if (txtv3.Enabled == true)
                                                    {
                                                        if (Session["C7Formula"] != null)
                                                        {
                                                            if (Session["C7Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV3 = intCountV3 + 1;
                                                                if (txtNHWork.Enabled == true || NH!="0")
                                                                {
                                                                    if (Session["C7FormulaCalc"].ToString() == "")
                                                                    {
                                                                        intCountV3 = Convert.ToInt32(Convert.ToDouble(NH) / 8);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Session["C7FormulaCalc"].ToString() != "0")
                                                                        {
                                                                            //intCountV3 = Convert.ToInt32(Convert.ToDouble(NH) / Convert.ToInt32(Session["C7FormulaCalc"].ToString()));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (Session["C7Formula"].ToString() == "2") //TIME
                                                            {
                                                                DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["C7FormulaCalc"].ToString());
                                                                DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                {
                                                                    intCountV3 = intCountV3 + 1;
                                                                }
                                                            }
                                                        }
                                                    }


                                                    //If Variable Pay is Applicable...
                                                    if (txtv4.Enabled == true)
                                                    {
                                                        if (Session["C8Formula"] != null)
                                                        {
                                                            if (Session["C8Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV4 = intCountV4 + 1;
                                                                if (txtNHWork.Enabled == true || NH!="0")
                                                                {
                                                                    if (Session["C8FormulaCalc"].ToString() == "")
                                                                    {
                                                                        intCountV4 = Convert.ToInt32(Convert.ToDouble(NH) / 8);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (Session["C8FormulaCalc"].ToString() != "0")
                                                                        {
                                                                            //intCountV4 = Convert.ToInt32(Convert.ToDouble(NH) / Convert.ToInt32(Session["C8FormulaCalc"].ToString()));
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            if (Session["C8Formula"].ToString() == "2") //TIME
                                                            {
                                                                DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["C8FormulaCalc"].ToString());
                                                                DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                {
                                                                    intCountV4 = intCountV4 + 1;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //Calculates only when intime and outime is presetn
                                                    intdaysworkedhrsmon = intdaysworkedminmon / 60;
                                                    intdaysworked = (intdaysworkedhrsmon / intdaysworkedhrmin);
                                                }
                                            }


                                            #region Deleted

                                            //if (intdaysworked > 0)
                                            //{
                                            //    txtdayswork.Text = intdaysworked.ToString("#0.00");
                                            //}
                                            #endregion
                                        }

                                        //If Variable Pay is Applicable...
                                        if (txtv1.Enabled == true)
                                        {
                                            txtv1.Text = Convert.ToString(intCountV1);
                                            //Check for Formula As Day or not 
                                        }
                                        if (txtv2.Enabled == true)
                                        {
                                            txtv2.Text = Convert.ToString(intCountV2);
                                        }
                                        if (txtv3.Enabled == true)
                                        {
                                            txtv3.Text = Convert.ToString(intCountV3);
                                        }
                                        if (txtv4.Enabled == true)
                                        {
                                            txtv4.Text = Convert.ToString(intCountV4);
                                        }
                                        //Check for Approved TimeSheet Data If data is there then 
                                        //Get Data From Approved TimeSheet and then show the data in the ApprovedTimeSheet Data 

                                        string srSqlTS = "Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk,sum(v1) v1,sum(v2) v2,sum(v3) v3,sum(v4) v4  From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk,v1,v2,v3,v4 From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";
                                        DataSet dsTs_New = new DataSet();

                                        dsTs_New = DataAccess.FetchRS(CommandType.Text, srSqlTS, null);
                                        if (dsTs_New != null)
                                        {
                                            if (dsTs_New.Tables.Count > 0)
                                            {
                                                DataRow[] drTS;
                                                drTS = dsTs_New.Tables[0].Select("Time_Card_No ='" + time_card_no + "'");

                                                if (drTS.Length > 0)
                                                {

                                                    if (drTS[0]["v1"].ToString() != "0" || drTS[0]["v1"].ToString() != "")
                                                    {
                                                        if (txtv1.Enabled == true && txtv1.Text == "" || txtv1.Enabled == true && txtv1.Text == "0")
                                                        {
                                                            txtv1.Text = Convert.ToString(drTS[0]["v1"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }
                                                    if (drTS[0]["v2"].ToString() != "0" || drTS[0]["v2"].ToString() != "")
                                                    {
                                                        if (txtv2.Enabled == true && txtv2.Text == "" || txtv2.Enabled == true && txtv2.Text == "0")
                                                        {
                                                            txtv2.Text = Convert.ToString(drTS[0]["v2"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }

                                                    if (drTS[0]["v3"].ToString() != "0" || drTS[0]["v3"].ToString() != "")
                                                    {
                                                        if (txtv3.Enabled == true && txtv3.Text == "" || txtv3.Enabled == true && txtv3.Text == "0")
                                                        {
                                                            txtv3.Text = Convert.ToString(drTS[0]["v3"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }

                                                    if (drTS[0]["v4"].ToString() != "0" || drTS[0]["v4"].ToString() != "")
                                                    {
                                                        if (txtv4.Enabled == true && txtv4.Text == "" || txtv4.Enabled == true && txtv4.Text == "0")
                                                        {
                                                            txtv4.Text = Convert.ToString(drTS[0]["v4"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = true;
                ((Button)commandItem.FindControl("btnCalcOverVar")).Enabled = true;
                //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = true;
            }


        }

        void MonthFill()
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();

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
                if (Session["ROWYEAR"] != null)
                {
                    foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                    {
                        dtFilterFound.ImportRow(dr);
                    }
                }
                else
                {
                    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                    {
                        dtFilterFound.ImportRow(dr);
                    }

                }
            }

            //if (PayrollType == 0)
            //{
            //    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
            //    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
            //    {
            //        dtFilterFound.ImportRow(dr);
            //    }
            //}
            //else
            //{
            //    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "' And PAYTYPE='" + PayrollType.ToString() + "'");
            //    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "' And PAYTYPE='" + PayrollType.ToString() + "'"))
            //    {
            //        dtFilterFound.ImportRow(dr);
            //    }
            //}

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            SetControlDate(cmbMonth.SelectedValue);
        }

        void SetControlDate(string mon)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + mon);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
                strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
                strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
                strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);

                rdFrom.DbSelectedDate = Utility.ToString(dr["PaySubStartDate"].ToString());
                rdEnd.DbSelectedDate = Utility.ToString(dr["PaySubEndDate"].ToString());
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Session["ROWID"] == null)
            {
            }
            else
            {
                if (intcnt == 1)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                else
                {
                    if (IsPostBack == true)
                    {
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                    SetControlDate(cmbMonth.SelectedValue);
                }
            }

        }




        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;// UniqueName="DeleteColumn"            
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("102", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }


        #endregion
        //Toolbar End

    }
}
