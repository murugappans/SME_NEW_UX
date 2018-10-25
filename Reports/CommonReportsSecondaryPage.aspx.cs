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
using System.Data.SqlClient;
using Telerik.Web.UI;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;
using System.Linq;


namespace SMEPayroll.Reports
{
    public partial class CommonReportsSecondaryPage : System.Web.UI.Page
    {
        int templateId = 0;
        string compid = "";
        DataSet rptDs;
        protected string sUserName = "", sgroupname = "";
        string tname="";
        string optiontype = "";
        string catname = "";
        string _actionMessage = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SqlDataSource8.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
               
                tname = Request.QueryString[0].ToString();
                
                optiontype = Request.QueryString[1].ToString();

                string sql1 = "select top(1)  TemplateID,TemplateName,CategoryName,CategoryId from CustomTemplates where TemplateName='" + tname + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr.Read())
                {
                    Session["CategoryName"] = dr["CategoryName"].ToString();
                    Session["CategoryId"] = dr["CategoryId"].ToString();
                    Session["TemplateId"] = dr["TemplateID"].ToString();
                    Session["TemplateName"] = dr["TemplateName"].ToString();
                    Session["optiontype"] = optiontype.ToString();

                    catname = dr["CategoryName"].ToString();

                }

               

                //btnCurrentMonth.Enabled = false;
                //btnPreviousMonth.Enabled = false;
                //btnThreeMonth.Enabled = false;
                //btnSixMonth.Enabled = false;
                //btnOneYear.Enabled = false;
                //btnCustom.Enabled = false;

                btnCurrentMonth.Visible = false;
                btnPreviousMonth.Visible = false;
                btnThreeMonth.Visible = false;
                btnSixMonth.Visible = false;
                btnOneYear.Visible = false;
                btnCustom.Visible = false;
                btncommon.Visible = false;
                expirydate.Visible = false;
                pvr_div.Visible = false;
                yps_div.Visible = false;
                dept_div.Visible = false;
                variance_div.Visible = false;
                if (catname == "pvr")
                {
                    
                    pvr_div.Visible = true;
                    btncommon.Visible = true;

                    ddlPaymentVariance.SelectedValue = optiontype;
                    bindgrid();
                }
                else if (catname != "pvr" && catname != "yps")
                {
                    dept_div.Visible = true;
                }
                if (catname == "yps")
                {
                    yps_div.Visible = true;
                    btncommon.Visible = true;

                    ddlYearlySummaryReport.SelectedValue = optiontype;
                    bindgrid2();
                }
                else if (catname != "pvr" && catname != "yps")
                {
                    dept_div.Visible = true;
                }
                if (catname == "wfa")
                {
                    btncommon.Visible = true;
                    dept_div.Visible = false;
                }
                if (catname == "variance")
                {
                    dept_div.Visible = false;
                    btncommon.Visible = true;
                    variance_div.Visible = true;
                }


                //--for heading
                btnCurrentMonth.Text = DateTime.Now.ToString("MMM")+" "+ DateTime.Now.ToString("yy")+"";
                btnPreviousMonth .Text = DateTime.Now.AddMonths(-1).ToString("MMM")+" "+ DateTime.Now.AddMonths(-1).ToString("yy")+"";
                btnThreeMonth.Text = DateTime.Now.AddMonths(-2).ToString("MMM")+" "+ DateTime.Now.AddMonths(-2).ToString("yy") + " - " + DateTime.Now.ToString("MMM")+" "+ DateTime.Now.ToString("yy")+""; 
                btnSixMonth.Text = DateTime.Now.AddMonths(-5).ToString("MMM")+" " + DateTime.Now.AddMonths(-5).ToString("yy") + " - " + DateTime.Now.ToString("MMM") + " " + DateTime.Now.ToString("yy") + "";
                btnOneYear.Text= DateTime.Now.AddMonths(-11).ToString("MMM") + " " + DateTime.Now.AddMonths(-11).ToString("yy") + " - " + DateTime.Now.ToString("MMM") + " " + DateTime.Now.ToString("yy") + "";


            }
            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
            compid = Utility.ToString(Utility.ToInteger(Session["Compid"].ToString()));
            //templateId = Convert.ToInt32(Request.QueryString["TemplateId"]);
            //Session["TemplateId"] = Convert.ToString(templateId.ToString());

        }
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            Label itemCategoryID = ((Label)e.Item.FindControl("lblCategoryID"));
            RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("rdEmployeeList");
            RadioButtonList1.DataSource = Utility.GetOtherCategoryTemplates(Convert.ToInt32(itemCategoryID.Text));
            RadioButtonList1.DataBind();

        }
        protected void DataList1_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            Label itemCategoryID = ((Label)e.Item.FindControl("lblCategoryID"));
            RadioButtonList RadioButtonList1 = (RadioButtonList)e.Item.FindControl("rdEmployeeList");
            RadioButtonList1.DataSource = Utility.GetOtherCategoryTemplates(Convert.ToInt32(itemCategoryID.Text));
            RadioButtonList1.DataBind();

        }
        protected void dlDept_databound(object sender, EventArgs e)
        {

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlDepartment")
            {

                ddlDepartment.Items.Insert(0, new ListItem("- Select -", "-2"));
                ddlDepartment.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }

        }

        protected void ButtonCustomDate_Click(object sender, System.EventArgs e)
        {
            bool datePeriodFlag = false;
            int startMonth = 0;
            int endMonth = 0;
            List<DateTime> listDates;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (((System.Web.UI.WebControls.LinkButton)sender).ID == "imgbtnfetch")
            {
                if (RadDatePicker9.SelectedDate.HasValue)
                {
                    if (RadDatePicker10.SelectedDate.HasValue)
                    {
                        string startDate = Convert.ToDateTime(RadDatePicker9.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        string endDate = Convert.ToDateTime(RadDatePicker10.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                      
                        startMonth = RadDatePicker9.SelectedDate.Value.Month;
                      
                        endMonth = RadDatePicker10.SelectedDate.Value.Month;
                        DateTime startCollDate = Convert.ToDateTime(RadDatePicker9.SelectedDate.Value);
                        DateTime endCollDate = Convert.ToDateTime(RadDatePicker10.SelectedDate.Value);
                        listDates = new List<DateTime>();
                        while (startCollDate < endCollDate)
                        {
                            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                            startCollDate = startCollDate.AddMonths(1);

                        }
                        if (startMonth == endMonth)
                        {
                            datePeriodFlag = false;
                        }
                        else
                        {
                             datePeriodFlag=true;
                        }
                        
                        
                        if (Session["CategoryId"].ToString() != "")
                        {
                            if (Session["TemplateId"].ToString() != "")
                            {
                                if (Session["CategoryName"].ToString() == "Leaves")
                                {
                                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                                }
                                else if (Session["CategoryName"].ToString() == "Expiry")
                                {
                                    GenerateCertificateExpiry( endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                                }
                                else
                                {
                                    GenerateCommonReportsByStartDate(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                                }
                                
                            }

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                        }
                    }
                }
            }
        }
        protected void GenerateCommonLeaveReport(DateTime startDate, DateTime endDate, int CategoryId, int TemplateID, List<DateTime> listDates)
        {
            Session["StartPeriod"] = startDate.ToString();
            Session["EndPeriod"] = endDate.ToString();
            string strMessage = "";
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns1 = "0";
            string sqlTrns2 = "0";
            string sqlTrnsTypeLeave = "0";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " order by seq_order";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];
            //get template name
            string tname = dtTable.Rows[0]["TemplateName"].ToString();


            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if(sortdr["sort_order"].ToString()=="1")
                     sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString()+" desc";
            }
            //-------

            // muru for grouping
            string selectSQL3 = "Select top(2) ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and group_order >0 order by group_order";
            SqlDataReader groupdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL3, null);
            string groupfield = "";
            int c = 0;
            while (groupdr.Read())
            {
                c = c + 1;
                if (c == 1)
                {
                    groupfield = "&g1=" + groupdr[0].ToString();
                }
                else if (c == 2)
                {
                    groupfield = groupfield + "&g2=" + groupdr[0].ToString();
                }

            }
            if (c == 1)
                groupfield = groupfield + "&g2=a";
            //------------------------------
            if (startDate != null && endDate != null)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    if (dtTable.Rows[i]["TableID"].ToString() == "7") // cross checking with dropdownlistitem to gridboundcolumn text
                    {

                        sqlTrnsTypeLeave = sqlTrnsTypeLeave + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                    }
                }
            }
            
            if (strMessage.Length <= 0)
            {
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            sqlTrns1 = sqlTrns1 + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }



                string sSQL = "sp_GetLeaveSumDet";
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@year", Utility.ToInteger(startDate.Year));
                parms[1] = new SqlParameter("@EmpID", sqlTrns1);
                parms[2] = new SqlParameter("@LeaveID", sqlTrnsTypeLeave);
                parms[3] = new SqlParameter("@ReportType", Session["optiontype"].ToString());
                parms[4] = new SqlParameter("@frommonth", Utility.ToInteger(startDate.Month));
                parms[5] = new SqlParameter("@endmonth", Utility.ToInteger(endDate.Month));
                //if (rdRepOption.SelectedItem.Value == "1")
                //{
                //    parms[5] = new SqlParameter("@endmonth", Utility.ToInteger("-1"));
                //}
                //else
                //{
                //    parms[5] = new SqlParameter("@endmonth", Utility.ToInteger(drpMonthEnd.SelectedItem.Value));
                //}
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

                DataTable dt = new DataTable();
                DataSet ds2 = new DataSet();
                dt = ds.Tables[0];
                
                //if (sortfield!="")
                //{
                //    dt.DefaultView.Sort = sortfield;
                //    dt = dt.DefaultView.ToTable(true);
                //    ds2.Tables.Add(dt);
                //    Session["rptDs"] = ds2;
                //}
                //else
                //{
                //    Session["rptDs"] = ds;
                //}
               
                              
                
               // Response.Redirect("../Reports/CustomReportNew.aspx?PageType=6");
                DataSet leaveds = new DataSet();
                if (sortfield != "" && groupfield != "")
                {

                    dt.DefaultView.Sort = sortfield;
                    dt = dt.DefaultView.ToTable(true);

                    leaveds.Tables.Add(dt);
                    Session["rptDs"] = leaveds;
                    Response.Redirect("../Reports/CommonReportGrouping.aspx?tname="+ tname+""+ groupfield);
                }
                else if (sortfield != "" && groupfield == "")
                {
                    dt.DefaultView.Sort = sortfield;
                    dt = dt.DefaultView.ToTable(true);
                    Session["rptDs"] = leaveds;
                    Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=26");

                }
                else if (sortfield == "" && groupfield != "")
                {
                    leaveds.Tables.Add(dt);
                    Session["rptDs"] = leaveds;
                    Response.Redirect("../Reports/CommonReportGrouping.aspx?tname=" + tname + "" + groupfield);


                }

                
                Session["rptDs"] = ds;
                Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=26");
            }
            else
            {
                if (strMessage.Length > 0)
                {
                    Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                    
                    strMessage = "";
                }
            }
        }
        protected void Generateemail(string CategoryId, string TemplateID, string CatName)
        {

            string strMessage = "";
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns1 = "0";
            string sqlTrns2 = "0";
            string sqlTrnsTypeLeave = "0";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + "";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];
            string colid = "";
            string mcat = "";


            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------

            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                {
                    if (colid == "")
                        colid = dtTable.Rows[i]["ColumnID"].ToString().Trim();
                    else
                        colid = colid + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();

                    mcat = dtTable.Rows[i]["MainCategory"].ToString().Trim();
                }
            }
           // string strEmployee = "";
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        if (strEmployee == "")
                            strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                        else
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }

            if (strMessage.Length <= 0)
            {
               

                sqlSelect = "Select Docid,E.Emp_Code,E.time_card_no,(select Deptname from department where id=E.dept_id)Department, isnull(E.emp_name,'')+' '+isnull(E.emp_lname,'') SentTo , EventView = Case When EventView=1 Then 'Email Payslip' Else '' End, Senton, isnull(Em.emp_name,'')+' '+isnull(Em.emp_lname,'') SentBy, [T.Status] = Case When T.Status=0 Then 'Success' Else 'Fail' End, T.Remarks From emailtrack T Inner Join Employee E On (T.Sentto = E.Emp_Code Or T.Sentto=0) Inner Join Employee Em On T.SentBy = Em.Emp_Code Where E.Emp_Code In (" + strEmployee + ") And T.MonthYear in (" + colid + ") And  EventView = '" + Session["optiontype"].ToString() + "'";
             
                DataSet ds = DataAccess.FetchRSDS(CommandType.Text,sqlSelect);

                DataTable dt = new DataTable();
                DataSet ds2 = new DataSet();
                dt = ds.Tables[0];

                if (sortfield != "")
                {
                    dt.DefaultView.Sort = sortfield;
                    dt = dt.DefaultView.ToTable(true);
                    ds2.Tables.Add(dt);
                    Session["rptDs"] = ds2;
                }
                else
                {
                    Session["rptDs"] = ds;
                }

                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=6");
            }
            else
            {
                if (strMessage.Length > 0)
                {
                    Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");

                    strMessage = "";
                }
            }
        }
        protected void Generatepvr(string CategoryId, string TemplateID, string CatName)
        {

            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strid = "0";
            string sqlSelect = "";
            string empconditin = "";
            string additionalcolumnname = "";
            string additionalcolumn = "";
            int grid1 = 0;
          //  lblerror.Text = "";
            string strPayrollOptions1 = "";
            string strPayrollOptions2 = "";
            foreach (GridItem item in RadGridPaymentVariance.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strid = strid + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }
            if (YearPaymentVarianceFirst.SelectedValue != "0")
            {
                if (YearPaymentVarianceSecond.SelectedValue != "0")
                {
                    if (ddlPaymentVariance.SelectedValue != "0")
                    {
                        if (ddlPaymentVariance.SelectedValue == "2")
                        {
                            empconditin = "e.emp_code";
                            additionalcolumn = "e.emp_name as Employee,";
                            additionalcolumnname = "e.emp_name";
                        }
                        else if (ddlPaymentVariance.SelectedValue == "3")
                        {
                            empconditin = "e.dept_id";
                            additionalcolumn = "d.DeptName as Department,";
                            additionalcolumnname = "d.DeptName";
                        }
                        else if (ddlPaymentVariance.SelectedValue == "4")
                        {
                            empconditin = "e.Company_Id";
                            additionalcolumn = "";
                            additionalcolumnname = "[Description]";
                        }

                        string SelectedYearFirst = YearPaymentVarianceFirst.SelectedValue;
                        string SelectedYearSecond = YearPaymentVarianceSecond.SelectedValue;
                        string FromMonth = FromMonthPaymentVariance.SelectedValue;
                        string ToMonth = ToMonthPaymentVariance.SelectedValue;
                        string dpt = ddlPaymentVariance.SelectedValue;
                        string FromMonthTxt = FromMonthPaymentVariance.Items[FromMonthPaymentVariance.SelectedIndex].Text;
                        string ToMonthTxt = ToMonthPaymentVariance.Items[ToMonthPaymentVariance.SelectedIndex].Text;
                        strPayrollOptions1 = "[Description]," + additionalcolumn + " " +
                                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYearFirst + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYearFirst + "], " +
                                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYearSecond + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYearSecond + "], " +
                                        "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYearSecond + " then value else 0 end) - " +
                                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYearFirst + " then value else 0 end),2) as Variance " +
                                        "FROM prepare_payroll_detail ppd " +
                                        "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                                        "inner join employee e on e.emp_code = ppd.emp_id " +
                                        "inner join department d on d.id = e.dept_id " +
                                        "CROSS APPLY " +
                                        "( ";
                        strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + compid + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                                        "group by [Description]," + additionalcolumnname + " ";
                        sqlSelect = "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Basic Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))))) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Earining', (NH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Worked', (NH_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OTI Amount', (OT1_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT1 Hours', (OT1_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Amount', (OT2_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Hours', (OT2_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'DH Earning', (DH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Working Days', (Wdays)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT vat.description as Description," + additionalcolumn + " " +
                                    "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYearFirst + " then ea.trx_amount else 0 end) AS [" + FromMonthTxt + "" + SelectedYearFirst + "], " +
                                    "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYearSecond + " then ea.trx_amount else 0 end) AS [" + ToMonthTxt + "" + SelectedYearSecond + "], " +
                                    "round(SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYearSecond + " then ea.trx_amount else 0 end)- " +
                                    "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYearFirst + " then ea.trx_amount else 0 end),2) as Variance " +
                                    "FROM ViewAdditionTypesDesc vat " +
                                    "inner join emp_additions ea on ea.trx_type = vat.id " +
                                    "inner join employee e on e.emp_code = ea.emp_code " +
                                    "inner join department d on d.id = e.dept_id " +
                                    "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + compid + " and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
                                    "GROUP BY vat.description," + additionalcolumnname + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Total Additions', (total_additions)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT vd.description as Description," + additionalcolumn + " " +
                                    "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYearFirst + " then ed.trx_amount else 0 end) AS [" + FromMonthTxt + "" + SelectedYearFirst + "], " +
                                    "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYearSecond + " then ed.trx_amount else 0 end) AS [" + ToMonthTxt + "" + SelectedYearSecond + "], " +
                                    "round(SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYearSecond + " then ed.trx_amount else 0 end)- " +
                                    "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYearFirst + " then ed.trx_amount else 0 end),2) as Variance " +
                                    "FROM ViewDeductions vd " +
                                    "inner join emp_deductions ed on ed.trx_type = vd.id " +
                                    "inner join employee e on e.emp_code = ed.emp_code " +
                                    "inner join department d on d.id = e.dept_id " +
                                    "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + compid + " and ed.status ='L' and e.StatusId ='1' " +
                                    "GROUP BY vd.description," + additionalcolumnname + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Total Deductions', (total_deductions)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Unpaid Amount', (unpaid_leaves_amount) UNION ALL SELECT 'Unpaid Leaves', (unpaid_leaves) ) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Total Gross', (total_gross)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Gross Amount', (CPFGrossAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Amount', (cpfAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employee Cont CPF', (empCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employer Cont CPF', (employerCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Fund Amount', (fund_amount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'SDL', (SDL)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Net Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))))) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'FWL', (FWL)) " +
                                     " " + strPayrollOptions2 + " ;";

                        
                        if (grid1 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                            DataTable dt = new DataTable();
                            DataSet ds2 = new DataSet();
                            dt = rptDs.Tables[0];

                            if (sortfield != "")
                            {
                                dt.DefaultView.Sort = sortfield;
                                dt = dt.DefaultView.ToTable(true);
                                ds2.Tables.Add(dt);
                                Session["rptDs"] = ds2;
                            }
                            else
                            {
                                Session["rptDs"] = rptDs;
                            }
                            // Response.Redirect("../Reports/ReportsNew.aspx?Category=" + dpt);
                            Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=71");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);
                            //lblerror.Text = "Please Select the option and at least one Name";
                           
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select the option');", true);
                        //  lblerror.Text = "Please Select the option";


                    }
                }
                else
                {
                     Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select the Second Year');", true);
                    //  lblerror.Text = "Please Select the Second Year";
                   
                }
            }
            else
            {
                 Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select the First Year');", true);
                // lblerror.Text = "Please Select the First Year";
               
            }

        }
        protected void Generatevariance(string CategoryId, string TemplateID, string CatName)
        {

            if (optiontype == "1")
            {
                #region CostCenter


                //Get Sql Data 
                DataSet dsVariance = new DataSet();
                DataSet dsVariance1 = new DataSet();
                DataSet dsMain = new DataSet();

                SqlParameter[] parms = new SqlParameter[4];


                // for (int i = 0; i <= cmbFromMonth.Items.Count - 1; i++)
                // {
                //@company_id=3,@month=51,@year=2011,@UserID=3
                parms[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));
                //parms[1] = new SqlParameter("@month", cmbFromMonth.SelectedValue);
                parms[1] = new SqlParameter("@month", cmbFromMonth.SelectedValue);
                parms[2] = new SqlParameter("@year", cmbYear.SelectedValue);
                parms[3] = new SqlParameter("@UserID", Session["EmpCode"].ToString());

                dsVariance = DataAccess.ExecuteSPDataSet("Sp_genledger_Rpt", parms);
                if (dsVariance.Tables[0].Rows.Count > 0)
                {
                    dsMain = dsVariance;
                }

                SqlParameter[] parms1 = new SqlParameter[4];

                parms1[0] = new SqlParameter("@company_id", Utility.ToInteger(Session["Compid"]));
                //parms[1] = new SqlParameter("@month", cmbFromMonth.SelectedValue);
                parms1[1] = new SqlParameter("@month", cmbToMonth.SelectedValue);
                parms1[2] = new SqlParameter("@year", cmbYear.SelectedValue);
                parms1[3] = new SqlParameter("@UserID", Session["EmpCode"].ToString());

                dsVariance1 = DataAccess.ExecuteSPDataSet("Sp_genledger_Rpt", parms1);



                if (dsMain.Tables[0].Rows.Count > 0)
                {
                    dsMain.Merge(dsVariance1, true, MissingSchemaAction.AddWithKey);
                }

                Session["rptDs"] = dsMain;

                if (dsMain.Tables[0].Rows.Count > 0)
                {
                    if (optiontype == "1")
                    {
                        Response.Redirect("../Reports/CustomReportNew.aspx?PageType=10&SM=" + cmbFromMonth.Items[cmbFromMonth.SelectedIndex].Text + "&EM=" + cmbToMonth.Items[cmbToMonth.SelectedIndex].Text);
                    }
                    //Response.Redirect("../Reports/CustomReportNew.aspx");
                    //string sFileName = "../Reports/CustomReportNew.aspx";
                    //Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
                }
                else
                {
                    //ShowMessageBox("No Records Found");

                }
                #endregion
            }
            else if (optiontype == "2")
            {
                //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                //string startdate = Convert.ToDateTime(RadDatePicker_From.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                //string enddate = Convert.ToDateTime(RadDatePicker_To.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                ////Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartMonth=" + cmbFromMonth.SelectedItem + "&EndMonth=" + cmbToMonth.SelectedItem + "&year=" + cmbYear.SelectedValue + ""); 
                //Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartDate=" + startdate + "&EndDate=" + enddate + ""); 
            }

        }
        protected void Generateyps(string CategoryId, string TemplateID, string CatName)
        {

            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------

            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strid = "0";
            string strselectedfrmmonths = "";
            string strselectedfrmmonthsAdditions = "";
            string strselectedfrmmonthsDeductions = "";
            string strselectedfrmmonthssum = "";
            string strselectedfrmmonthssumAdditions = "";
            string strselectedfrmmonthssumDeductions = "";
            string strselectedTomonths = "";
            string strselectedTomonthsAdditions = "";
            string strselectedTomonthsDeductions = "";
            string strselectedTomonthssum = "";
            string strselectedTomonthssumAdditions = "";
            string strselectedTomonthssumDeductions = "";
            string sqlSelect = "";
            string empconditin = "";
            string additionalcolumnname = "";
            string additionalcolumn = "";
            string strPayrollOptions1 = "";
            string strPayrollOptions2 = "";
            int grid1 = 0;
            int selectedfrmmonth = Int32.Parse(drpFrmMonthYrlSumReport.SelectedValue.ToString());
            int selectedfrmYear = Int32.Parse(drpFrmYearYrlSumReport.SelectedValue.ToString());
            int selectedfrmmonthstatic = Int32.Parse(drpFrmMonthYrlSumReport.SelectedValue.ToString());
            int selectedTomonth = Int32.Parse(drpToMonthYrlSumReport.SelectedValue.ToString()) - (Int32.Parse(drpToMonthYrlSumReport.SelectedValue.ToString()) - 1);
            int selectedToYear = Int32.Parse(drpToYearYrlSumReport.SelectedValue.ToString());
            int selectedTomonthstatic = Int32.Parse(drpToMonthYrlSumReport.SelectedValue.ToString());
           // lblerror.Text = "";
            foreach (GridItem item in RadGridYearlySummaryReport.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strid = strid + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }
            foreach (ListItem item in drpFrmMonthYrlSumReport.Items)
            {
                if (item is ListItem)
                {
                    ListItem dataItem = (ListItem)item;
                    if (selectedfrmmonth < 12)
                    {
                        selectedfrmmonth = selectedfrmmonth + 1;
                        strselectedfrmmonths = strselectedfrmmonths + "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) AS '" + selectedfrmmonth + "-" + drpFrmYearYrlSumReport.SelectedValue + "',";
                        strselectedfrmmonthsAdditions = strselectedfrmmonthsAdditions + "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) AS '" + selectedfrmmonth + "-" + drpFrmYearYrlSumReport.SelectedValue + "',";
                        strselectedfrmmonthsDeductions = strselectedfrmmonthsDeductions + "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) AS '" + selectedfrmmonth + "-" + drpFrmYearYrlSumReport.SelectedValue + "',";
                        strselectedfrmmonthssum = strselectedfrmmonthssum + "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) + ";
                        strselectedfrmmonthssumAdditions = strselectedfrmmonthssumAdditions + "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) + ";
                        strselectedfrmmonthssumDeductions = strselectedfrmmonthssumDeductions + "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) + ";
                    }
                }
            }
            foreach (ListItem item in drpToMonthYrlSumReport.Items)
            {
                if (item is ListItem)
                {
                    ListItem dataItem = (ListItem)item;
                    if (selectedTomonth < selectedTomonthstatic)
                    {
                        selectedTomonth = selectedTomonth + 1;
                        strselectedTomonths = strselectedTomonths + "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedTomonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedTomonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then value else 0 end) AS '" + selectedTomonth + "-" + drpToYearYrlSumReport.SelectedValue + "',";
                        strselectedTomonthsAdditions = strselectedTomonthsAdditions + "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedTomonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) AS '" + selectedTomonth + "-" + drpToYearYrlSumReport.SelectedValue + "',";
                        strselectedTomonthsDeductions = strselectedTomonthsDeductions + "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedTomonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) AS '" + selectedTomonth + "-" + drpToYearYrlSumReport.SelectedValue + "',";
                        strselectedTomonthssum = strselectedTomonthssum + "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedTomonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedTomonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then value else 0 end) + ";
                        strselectedTomonthssumAdditions = strselectedTomonthssumAdditions + "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedTomonth + " and Year(Convert(Datetime,ea.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) + ";
                        strselectedTomonthssumDeductions = strselectedTomonthssumDeductions + "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedTomonth + " and Year(Convert(Datetime,ed.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) + ";
                    }
                }
            }
            string dpt = ddlYearlySummaryReport.SelectedValue;
            if (ddlYearlySummaryReport.SelectedValue == "2")
            {
                empconditin = "e.emp_code";
                additionalcolumn = "e.emp_name as Employee,";
                additionalcolumnname = "e.emp_name";
            }
            else if (ddlYearlySummaryReport.SelectedValue == "3")
            {
                empconditin = "e.dept_id";
                additionalcolumn = "d.DeptName as Department,";
                additionalcolumnname = "d.DeptName";
            }
            else if (ddlYearlySummaryReport.SelectedValue == "4")
            {
                empconditin = "e.Company_Id";
                additionalcolumn = "";
                additionalcolumnname = "[Description]";
            }
            if (selectedfrmYear == selectedToYear)
            {
                strPayrollOptions1 = "[Description]," + additionalcolumn + "ad.cpf as [Attract CPF Payable Gross],ad.tax_payable as [Attract Taxable Gross],  " +
                            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonthstatic + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonths + "" + strselectedfrmmonthssum + " " +
                            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonthstatic + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) as Total " +
                            "FROM prepare_payroll_detail ppd " +
                            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                            "inner join employee e on e.emp_code = ppd.emp_id " +
                            "inner join department d on d.id = e.dept_id " +
                            "full join additions_types ad on ad.id = e.Company_Id " +
                            "CROSS APPLY " +
                            "( ";
                strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                                      "group by [Description]," + additionalcolumnname + ",ad.cpf,ad.tax_payable ";
            }
            else if (selectedfrmYear != selectedToYear)
            {
                strPayrollOptions1 = "[Description]," + additionalcolumn + "ad.cpf as [Attract CPF Payable Gross],ad.tax_payable as [Attract Taxable Gross],  " +
                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonthstatic + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonths + " " +
                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = 1 and Month(Convert(Datetime,pphd.end_period,103)) = 1 and Year(Convert(Datetime,pphd.end_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then value else 0 end) AS '1-" + drpToYearYrlSumReport.SelectedValue + "', " + strselectedTomonths + "" + strselectedfrmmonthssum + "" + strselectedTomonthssum + " " +
                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + selectedfrmmonthstatic + " and Month(Convert(Datetime,pphd.end_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,pphd.end_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then value else 0 end) + " +
                        "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = 1 and Month(Convert(Datetime,pphd.end_period,103)) = 1 and Year(Convert(Datetime,pphd.end_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then value else 0 end) as Total " +
                        "FROM prepare_payroll_detail ppd " +
                        "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        "inner join employee e on e.emp_code = ppd.emp_id " +
                        "inner join department d on d.id = e.dept_id " +
                        "full join additions_types ad on ad.id = e.Company_Id " +
                        "CROSS APPLY " +
                        "( ";
                strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                                      "group by [Description]," + additionalcolumnname + ",ad.cpf,ad.tax_payable ";
            }

            if (selectedfrmYear == selectedToYear)
            {
                sqlSelect = "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Basic Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))))) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Earining', (NH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Worked', (NH_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OTI Amount', (OT1_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT1 Hours', (OT1_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Amount', (OT2_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Hours', (OT2_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'DH Earning', (DH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Working Days', (Wdays)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                            "SELECT vat.description as Description," + additionalcolumn + "vat.cpf as [Attract CPF Payable Gross],vat.tax_payable as [Attract Taxable Gross], " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonthsAdditions + "" + strselectedfrmmonthssumAdditions + " " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) as Total " +
                            "FROM ViewAdditionTypesDesc vat " +
                            "inner join emp_additions ea on ea.trx_type = vat.id " +
                            "inner join employee e on e.emp_code = ea.emp_code " +
                            "inner join department d on d.id = e.dept_id " +
                            "WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
                            "GROUP BY vat.description," + additionalcolumnname + ",vat.cpf,vat.tax_payable " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Total Additions', (total_additions)) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                            "SELECT vd.description as Description," + additionalcolumn + "dt.cpf as [Attract CPF Payable Gross],dt.Tax as [Attract Taxable Gross], " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonthsDeductions + "" + strselectedfrmmonthssumDeductions + " " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) as Total " +
                            "FROM ViewDeductions vd  " +
                            "inner join emp_deductions ed on ed.trx_type = vd.id  " +
                            "inner join employee e on e.emp_code = ed.emp_code  " +
                            "inner join department d on d.id = e.dept_id " +
                            "inner join deductions_types dt on dt.id = vd.id " +
                            "WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ed.status ='L' and e.StatusId ='1' " +
                            "GROUP BY vd.description," + additionalcolumnname + ",dt.cpf,dt.Tax " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Total Deductions', (total_deductions)) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Unpaid Amount', (unpaid_leaves_amount) UNION ALL SELECT 'Unpaid Leaves', (unpaid_leaves) ) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                           "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Total Gross', (total_gross)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Gross Amount', (CPFGrossAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Amount', (cpfAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employee Cont CPF', (empCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employer Cont CPF', (employerCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Fund Amount', (fund_amount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'SDL', (SDL)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Net Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))))) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'FWL', (FWL)) " +
                                     " " + strPayrollOptions2 + " ;";
            }
            else if (selectedfrmYear != selectedToYear)
            {
                sqlSelect = "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Basic Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))))) " +
                            " " + strPayrollOptions2 + " " +
                             "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Earining', (NH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'NH Worked', (NH_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OTI Amount', (OT1_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT1 Hours', (OT1_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Amount', (OT2_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'OT2 Hours', (OT2_wh)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'DH Earning', (DH_e)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Working Days', (Wdays)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                            "SELECT vat.description as Description," + additionalcolumn + "vat.cpf as [Attract CPF Payable Gross],vat.tax_payable as [Attract Taxable Gross], " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonthsAdditions + " " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = 1 and Year(Convert(Datetime,ea.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) AS '1-" + drpToYearYrlSumReport.SelectedValue + "', " + strselectedTomonthsAdditions + "" + strselectedfrmmonthssumAdditions + "" + strselectedTomonthssumAdditions + " " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ea.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) + " +
                            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = 1 and Year(Convert(Datetime,ea.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ea.trx_amount else 0 end) as Total " +
                            "FROM ViewAdditionTypesDesc vat " +
                            "inner join emp_additions ea on ea.trx_type = vat.id " +
                            "inner join employee e on e.emp_code = ea.emp_code " +
                            "inner join department d on d.id = e.dept_id " +
                            "WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
                            "GROUP BY vat.description," + additionalcolumnname + ",vat.cpf,vat.tax_payable " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Total Additions', (total_additions)) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                            "SELECT vd.description as Description," + additionalcolumn + "dt.cpf as [Attract CPF Payable Gross],dt.Tax as [Attract Taxable Gross], " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) AS '" + selectedfrmmonthstatic + "-" + drpFrmYearYrlSumReport.SelectedValue + "', " + strselectedfrmmonthsDeductions + " " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = 1 and Year(Convert(Datetime,ed.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) AS '1-" + drpToYearYrlSumReport.SelectedValue + "', " + strselectedTomonthsDeductions + "" + strselectedfrmmonthssumDeductions + "" + strselectedTomonthssumDeductions + " " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + selectedfrmmonthstatic + " and Year(Convert(Datetime,ed.trx_period,103)) = " + drpFrmYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) + " +
                            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = 1 and Year(Convert(Datetime,ed.trx_period,103)) = " + drpToYearYrlSumReport.SelectedValue + " then ed.trx_amount else 0 end) as Total " +
                            "FROM ViewDeductions vd  " +
                            "inner join emp_deductions ed on ed.trx_type = vd.id  " +
                            "inner join employee e on e.emp_code = ed.emp_code  " +
                            "inner join department d on d.id = e.dept_id " +
                            "inner join deductions_types dt on dt.id = vd.id " +
                            "WHERE  e.Company_Id = " + compid + " and " + empconditin + " in (" + strid + ") and ed.status ='L' and e.StatusId ='1' " +
                            "GROUP BY vd.description," + additionalcolumnname + ",dt.cpf,dt.Tax " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Total Deductions', (total_deductions)) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                            "SELECT 'Unpaid Amount', (unpaid_leaves_amount) UNION ALL SELECT 'Unpaid Leaves', (unpaid_leaves) ) " +
                            " " + strPayrollOptions2 + " " +
                            "union all " +
                            "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Total Gross', (total_gross)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Gross Amount', (CPFGrossAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'CPF Amount', (cpfAmount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employee Cont CPF', (empCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Employer Cont CPF', (employerCPF)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Fund Amount', (fund_amount)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'SDL', (SDL)) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'Net Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))))) " +
                                    " " + strPayrollOptions2 + " " +
                                    "union all " +
                                    "SELECT " + strPayrollOptions1 + " " +
                                    "SELECT 'FWL', (FWL)) " +
                                     " " + strPayrollOptions2 + " ;";
            }
            if (selectedfrmYear == selectedToYear)
            {
                if (selectedfrmYear != 0)
                {
                    if (selectedToYear != 0)
                    {
                        if (selectedfrmYear == selectedToYear && selectedfrmmonthstatic == 1 && selectedTomonthstatic == 12)
                        {

                            if (grid1 > 0)
                            {
                                DataSet rptDs = new DataSet();
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                                DataTable dt = new DataTable();
                                DataSet ds2 = new DataSet();
                                dt = rptDs.Tables[0];

                                if (sortfield != "")
                                {
                                    dt.DefaultView.Sort = sortfield;
                                    dt = dt.DefaultView.ToTable(true);
                                    ds2.Tables.Add(dt);
                                    Session["rptDs"] = ds2;
                                }
                                else
                                {
                                    Session["rptDs"] = rptDs;
                                }
                                Response.Redirect("../Reports/ReportsNew.aspx?CategoryYearlyPayrollSummaryReport=" + dpt);
                            }
                            else
                            {
                              //  lblerror.Text = "Please Select the option and at least one Name";
                                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select the option and at least one Name');", true);

                            }
                        }
                        else
                        {
                         //   lblerror.Text = "Please select months from January to December for the same year";
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select months from January to December for the same year');", true);
                        }
                    }
                    else
                    {
                       // lblerror.Text = "Please Select To Year";
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select To Year');", true);
                    }
                }
                else
                {
                    //lblerror.Text = "Please Select From Year";
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select From Year');", true);
                }
            }
            else if (selectedfrmYear != selectedToYear)
            {
                if (selectedfrmYear != 0)
                {
                    if (selectedToYear != 0)
                    {
                        if (selectedfrmYear < selectedToYear)
                        {

                            if (grid1 > 0)
                            {
                                DataSet rptDs = new DataSet();
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                                Session["rptDs"] = rptDs;
                                //Response.Redirect("../Reports/ReportsNew.aspx?CategoryYearlyPayrollSummaryReport=" + dpt);
                                Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=72");
                            }
                            else
                            {
                               // lblerror.Text = "Please Select the option and at least one Name";
                                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select the option and at least one Name');", true);
                            }
                        }
                        else
                        {
                          //  lblerror.Text = "To Year Must be greater than From year";
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('To Year Must be greater than From year');", true);
                        }
                    }
                    else
                    {
                      //  lblerror.Text = "Please Select To Year";
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select To Year');", true);
                    }
                }
                else
                {
                  //  lblerror.Text = "Please Select From Year";
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select From Year');", true);
                }
            }
        }
        protected void Generatewfa(string CategoryId, string TemplateID, string CatName)
        {
            string strMessage = "";
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns1 = "0";
            string sqlTrns2 = "0";
            string sqlTrnsTypeLeave = "0";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string strWorkFlowName = "";
            string employeeworkflowlevel = "";
            string strWorkFlowNamedisplay = "WorkFlowGroup";
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " order by seq_order";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];
            int optiontype = 0;
            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------
            for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    if (dtTable.Rows[i]["TableID"].ToString() == "10") // cross checking with dropdownlistitem to gridboundcolumn text
                    {
                    
                        
                        strWorkFlowNamedisplay = strWorkFlowNamedisplay + "," + "[" + dtTable.Rows[i]["RELATION"].ToString() + "]";
                        strWorkFlowName = strWorkFlowName + "[" + dtTable.Rows[i]["RELATION"].ToString() + "],";
                  
                  
                    

                    
                    optiontype = Convert.ToInt16(dtTable.Rows[i]["option_type"].ToString());
                    }
                }

           
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            
           
            string dpt = optiontype.ToString();

            if (dpt == "1")
            {
                employeeworkflowlevel = "e.pay_supervisor";
            }
            if (dpt == "2")
            {
                employeeworkflowlevel = "e.Leave_supervisor";
            }
            if (dpt == "3")
            {
                employeeworkflowlevel = "e.CliamsupervicerMulitilevel";
            }
            if (dpt == "4")
            {
                employeeworkflowlevel = "e.TimesupervicerMulitilevel";
            }


            sqlSelect = "select " + strWorkFlowNamedisplay + " " +
                            "from " +
                            "( " +
                            "Select 'Approving Manager' as WorkFlowGroup,ea.emp_name + '-L'  + Cast(ewfl.RowID as varchar(5))  as ReportingManager , ewf.WorkFlowName,ROW_NUMBER() OVER(PARTITION BY WorkFlowName order by RowID asc) as row_no from employee ea " +
                            "inner join EmployeeAssignedToPayrollGroup eatp on eatp.Emp_ID = ea.emp_code " +
                            "inner join EmployeeWorkFlowLevel ewfl on ewfl.PayRollGroupID = eatp.PayrollGroupID " +
                            "inner join EmployeeWorkFlow ewf on ewf.ID = ewfl.WorkFlowID " +
                            "where  ewfl.FlowType = " + dpt + " and ewf.Company_ID = " + compid + " and eatp.WorkflowTypeID = " + dpt + ") as s  " +
                            "Pivot  " +
                            "( max(ReportingManager) FOR WorkFlowName IN (" + strWorkFlowName + "[additional]) " +
                            ")  " +
                            "AS pvt " +
                            "union all " +
                            " select " + strWorkFlowNamedisplay + " " +
                            "from " +
                            "( " +
                            "Select 'Reporting Employee' as WorkFlowGroup,e.emp_name + '-Reports to-' + ea.emp_name  as ReportingEmployee , ewf.WorkFlowName,ROW_NUMBER() OVER(PARTITION BY WorkFlowName order by e.emp_name asc) as row_no from employee e " +
                            "inner join EmployeeWorkFlowLevel ewfl on ewfl.ID = " + employeeworkflowlevel + " " +
                            "inner join EmployeeWorkFlow ewf on ewf.ID = ewfl.WorkFlowID " +
                            "inner join EmployeeAssignedToPayrollGroup eatp on eatp.PayrollGroupID = ewfl.PayRollGroupID " +
                            "inner join employee ea on ea.emp_code = eatp.Emp_ID " +
                            "where ewfl.FlowType = " + dpt + " and ewf.Company_ID = " + compid + " and eatp.WorkflowTypeID = " + dpt + ") as s " +
                            "Pivot  " +
                            "( max(ReportingEmployee) FOR WorkFlowName IN (" + strWorkFlowName + "[additional]) " +
                            ")  " +
                            "AS pvt ;";


            //sqlSelect = "select " + strWorkFlowNamedisplay + ",ReportingEmployee from " +
            //           "(select e.emp_name as ReportingEmployee, ea.emp_name as ReportingManager,'L' + + Cast(ewfl.RowID as varchar(5)) + ' Approving Manager' as WorkFlowGroup , ewf.WorkFlowName  from employee e " +
            //           "inner join EmployeeWorkFlowLevel ewfl on ewfl.ID = e.CliamsupervicerMulitilevel " +
            //           "inner join EmployeeAssignedToPayrollGroup eatp on eatp.PayrollGroupID = ewfl.PayRollGroupID " +
            //           "inner join employee ea on ea.emp_code = eatp.Emp_ID " +
            //           "inner join EmployeeWorkFlow ewf on ewf.ID =ewfl.WorkFlowID " +
            //           "where eatp.WorkflowTypeID = " + dpt + " and ewf.Company_ID = " + compid + ") as s " +
            //           "Pivot " +
            //           "( " +
            //           "max(ReportingManager) FOR WorkFlowName IN (" + strWorkFlowName + ",[additional]) " +
            //           ") " +
            //           "AS pvt ; ";


            DataSet rptDs = new DataSet();
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            DataTable dt = new DataTable();
            DataSet ds2 = new DataSet();
            dt = rptDs.Tables[0];

            if (sortfield != "")
            {
                dt.DefaultView.Sort = sortfield;
                dt = dt.DefaultView.ToTable(true);
                ds2.Tables.Add(dt);
                Session["rptDs"] = ds2;
            }
            else
            {
                Session["rptDs"] = rptDs;
            }
            // Response.Redirect("../Reports/ReportsNew.aspx?ReportType=WorkFlowAssignmentReport-" + dpt);
            Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=73");




        }
        protected void GenerateCertificateExpiry( DateTime endDate, int CategoryId, int TemplateID, List<DateTime> listDates)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; 
            string sqlTrnsTypeExpiry= "0";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " order by seq_order";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];

            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------

            if (endDate != null)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                    {
                        grid1++;
                        sqlTrnsTypeExpiry = sqlTrnsTypeExpiry + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                    }
                }
            }
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }

            if (endDate!=null)
            {
                string enddate = Convert.ToDateTime(endDate.ToShortDateString()).ToString("dd/MM/yyyy", format);

                string sqlStr = "Select  (select time_card_no from employee where emp_code=M.emp_code) TimeCardId,(select Deptname from department where id=M.dept_id)Department ,Convert(varchar,E.Testdate ,103)ApplicationDate, Convert(varchar,E.Issuedate ,103)Issuedate,(isnull(M.emp_name,'')+' '+isnull(M.emp_lname,'')) FullName,C.Category_Name, E.CertificateNumber,Convert(varchar,E.ExpiryDate ,103) ExpiryDate From EmployeeCertificate E Inner Join CertificateCategory C On E.CertificateCategoryID = C.ID Inner Join Employee M On E.Emp_ID=M.Emp_Code";
                sqlStr = sqlStr + " Where E.Emp_ID In (" + strEmployee + ") And C.ID in (" + sqlTrnsTypeExpiry + ") AND M.termination_date is null And Convert(Datetime,E.ExpiryDate,103) <= Convert(Datetime,'" + enddate.ToString() + "',103) order by E.ExpiryDate Asc  ";
                if (grid1 > 0)
                {
                    if (grid2 > 0)
                    {
                        DataSet rptDs = new DataSet();
                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlStr, null);

                        DataTable dt = new DataTable();
                        DataSet ds2 = new DataSet();
                        dt = rptDs.Tables[0];

                        if (sortfield != "")
                        {
                            dt.DefaultView.Sort = sortfield;
                            dt = dt.DefaultView.ToTable(true);
                            ds2.Tables.Add(dt);
                            Session["rptDs"] = ds2;
                        }
                        else
                        {
                            Session["rptDs"] = rptDs;
                        }

                        Response.Redirect("../Reports/CustomReportNew.aspx");
                    }
                    else
                    {
                        
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Employee ');", true);
                   
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select End Month');", true);
               
            }
        }
        protected void GenerateCertificate(int CategoryId, int TemplateID, List<DateTime> listDates)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0;
            string sqlTrnsTypeExpiry = "0";
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + "";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];

            // muru for ordering
            string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
            SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
            string sortfield = "";
            if (sortdr.Read())
            {
                if (sortdr["sort_order"].ToString() == "1")
                    sortfield = sortdr[1].ToString();
                else
                    sortfield = sortdr[1].ToString() + " desc"; sortfield = sortdr[0].ToString();
            }
            //-------
            for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    if (dtTable.Rows[i]["TableID"].ToString() == "11") // cross checking with dropdownlistitem to gridboundcolumn text
                    {
                        grid1++;
                        sqlTrnsTypeExpiry = sqlTrnsTypeExpiry + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                    }
                }
            
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }

            string sqlStr = "Select  (select time_card_no from employee where emp_code=M.emp_code) TimeCardId,(select Deptname from department where id=M.dept_id)Department ,Convert(varchar,E.Testdate ,103)ApplicationDate, Convert(varchar,E.Issuedate ,103)Issuedate,(isnull(M.emp_name,'')+' '+isnull(M.emp_lname,'')) FullName,C.Category_Name, E.CertificateNumber,Convert(varchar,E.ExpiryDate ,103) ExpiryDate From EmployeeCertificate E Inner Join CertificateCategory C On E.CertificateCategoryID = C.ID Inner Join Employee M On E.Emp_ID=M.Emp_Code";
                sqlStr = sqlStr + " Where E.Emp_ID In (" + strEmployee + ") And C.ID in (" + sqlTrnsTypeExpiry + ") AND M.termination_date is null";
                if (grid1 > 0)
                {
                    if (grid2 > 0)
                    {
                        DataSet rptDs = new DataSet();
                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlStr, null);

                    DataTable dt = new DataTable();
                    DataSet ds2 = new DataSet();
                    dt = rptDs.Tables[0];

                    if (sortfield != "")
                    {
                        dt.DefaultView.Sort = sortfield;
                        dt = dt.DefaultView.ToTable(true);
                        ds2.Tables.Add(dt);
                        Session["rptDs"] = ds2;
                    }
                    else
                    {
                        Session["rptDs"] = rptDs;
                    }

                    Response.Redirect("../Reports/CustomReportNew.aspx");
                    }
                    else
                    {

                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Employee ');", true);

                }
           
        }
        protected void GenerateCommonReportsByStartDate(string startDate, string endDate,bool periodFlag, int CategoryId, int TemplateID,List<DateTime> listDates)
        {
           
            string currentstartdate = startDate;
            string currentenddate = endDate;
            Session["StartPeriod"] = currentstartdate.ToString();
            Session["EndPeriod"] = currentenddate.ToString();
            if (currentstartdate != "" && currentenddate != "")
            {              
                string strEmployee = "0";             
                int grid1 = 0;
                int grid2 = 0;
                string sqlTrnsTypeAddition = "0";
                string sqlTrnsTypeDeduction = "0";
                string sqlTrnsTypeClaims = "0";
                
                string selectSQL = "";             
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                DataTable dtTableAdd = new DataTable();
                DataSet dsTableAdd = new DataSet();
                bool sqlSelect1 = false;
                bool sqlSelect2 = false;
                bool sqlSelect3 = false;
                selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + "";
                dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                dtTable = dsTable.Tables[0];
               
                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect1 = true;
                            sqlTrnsTypeAddition = sqlTrnsTypeAddition + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }

                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect2 = true;
                            sqlTrnsTypeDeduction = sqlTrnsTypeDeduction + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }
                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "5") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect3 = true;
                            sqlTrnsTypeClaims = sqlTrnsTypeClaims + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            grid1++;
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }

                Session["StringEmployee"] = Convert.ToString(strEmployee.ToString());

                string sqlQueryEmployee = "Select e1.emp_code as EmpCode,e1.emp_name as EmpName,e1.time_card_no as TimeCardNo,";
                string sqlQueryPayroll = "Select pv.emp_code,";
            
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    grid2++;
                    if (dtTable.Rows[i]["TableID"].ToString().Trim() == "1")
                    {
                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "time_card_no")
                        {
                            sqlQueryEmployee = sqlQueryEmployee + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                        }
                    }
                    else if (dtTable.Rows[i]["TableID"].ToString().Trim() == "2")
                    {
                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "time_card_no")
                        {


                            if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "BasicPay")
                            {
                                string str = "";
                                //str = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=  MONTH(Convert(Datetime,'" + currentstartdate + "',103))and YEAR(start_period)=YEAR(Convert(Datetime,'" + currentstartdate + "',103)) and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";
                                str = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView2 Where  emp_code=e1.emp_code AND  status='G' order by emp_code Desc)";
                                sqlQueryPayroll = sqlQueryPayroll + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                            }
                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "NetPay")
                            {
                                string str1 = "";
                                //str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)='" + Convert.ToInt32(sMonth) + "' and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";
                                str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))) from  PayRollView2 Where  emp_code=e1.emp_code AND  status='G' order by emp_code Desc)";
                                sqlQueryPayroll = sqlQueryPayroll + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                            }
                            else
                            {
                                sqlQueryPayroll = sqlQueryPayroll + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                            }

                          
                          
                        }
                    }
                    
                }

                if (grid1 > 0)
                {
                    if (grid2 > 0)
                    {
                        sqlQueryEmployee = sqlQueryEmployee.Remove(sqlQueryEmployee.Length - 1, 1);
                        sqlQueryEmployee = sqlQueryEmployee + " from Employee e1";
                        sqlQueryEmployee = sqlQueryEmployee + " where e1.emp_code in (" + strEmployee + ")";
                        sqlQueryEmployee = sqlQueryEmployee + " ORDER BY EMP_NAME;";
                        DataSet dsEmpResult = new DataSet();
                        DataSet dsPayResult = new DataSet();
                        DataSet dsReportResult = new DataSet("DataSet");
                        DataTable dtEmployee = new DataTable("Table1");

                        SqlDataReader sqlEmpReader = DataAccess.ExecuteReader(CommandType.Text, sqlQueryEmployee, null);
                        dtEmployee.Load(sqlEmpReader);

                        DataTable dtPayRollView = new DataTable("Table2");
                        sqlQueryPayroll = sqlQueryPayroll.Remove(sqlQueryPayroll.Length - 1, 1);
                        sqlQueryPayroll = sqlQueryPayroll + " from Employee e1 LEFT OUTER JOIN PayRollView2 pv on e1.emp_code=pv.emp_code";
                        if (periodFlag)
                        {
                            sqlQueryPayroll = sqlQueryPayroll + " where pv.start_period between Convert(Datetime,'" + currentstartdate + "',103) And Convert(Datetime,'" + currentenddate + "',103) ";
                            sqlQueryPayroll = sqlQueryPayroll + " AND pv.end_period between Convert(Datetime,'" + currentstartdate + "',103) And Convert(Datetime,'" + currentenddate + "',103) ";
                            sqlQueryPayroll = sqlQueryPayroll + " And pv.STATUS in ('G')";


                        }
                        else
                        {
                            sqlQueryPayroll = sqlQueryPayroll + " where pv.start_period = Convert(Datetime,'" + currentstartdate + "',103) And pv.end_period=Convert(Datetime,'" + currentenddate + "',103)";
                            sqlQueryPayroll = sqlQueryPayroll + " And pv.STATUS in ('G')";
                        }
                        SqlDataReader sqlPayReader = DataAccess.ExecuteReader(CommandType.Text, sqlQueryPayroll, null);
                        dtPayRollView.Load(sqlPayReader);

                        DataTable dtAdditions = new DataTable("Table3");
                        if (sqlSelect1)
                        {
                            string sAdditionSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsAdditions = new SqlParameter[8];
                            parmsAdditions[0] = new SqlParameter("@empcode", strEmployee);
                            parmsAdditions[1] = new SqlParameter("@trxtype", sqlTrnsTypeAddition);
                            parmsAdditions[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsAdditions[3] = new SqlParameter("@enddate", currentenddate);
                            parmsAdditions[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                            parmsAdditions[5] = new SqlParameter("@addtype", "ADD");
                            parmsAdditions[6] = new SqlParameter("@stattype", 'L');
                            parmsAdditions[7] = new SqlParameter("@claimstatus", 1);


                            DataSet rptAdditionsDs = new DataSet();
                            rptAdditionsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sAdditionSQL, parmsAdditions);
                            dtAdditions = rptAdditionsDs.Tables[0];
                        }

                        DataTable dtDeductions = new DataTable("Table4");
                        if (sqlSelect2)
                        {

                            string sDeductionSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsDeductions = new SqlParameter[8];
                            parmsDeductions[0] = new SqlParameter("@empcode", strEmployee);
                            parmsDeductions[1] = new SqlParameter("@trxtype", sqlTrnsTypeDeduction);
                            parmsDeductions[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsDeductions[3] = new SqlParameter("@enddate", currentenddate);
                            parmsDeductions[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                            parmsDeductions[5] = new SqlParameter("@addtype", "DED");
                            parmsDeductions[6] = new SqlParameter("@stattype", 'L');
                            parmsDeductions[7] = new SqlParameter("@claimstatus", 1);
                            DataSet rptDeductionDs = new DataSet();
                            rptDeductionDs = DataAccess.FetchRS(CommandType.StoredProcedure, sDeductionSQL, parmsDeductions);
                            dtDeductions = rptDeductionDs.Tables[0];

                        }
                        DataTable dtClaims = new DataTable("Table5");
                        if (sqlSelect3)
                        {

                            string sClaimsSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsClaims = new SqlParameter[8];
                            parmsClaims[0] = new SqlParameter("@empcode", strEmployee);
                            parmsClaims[1] = new SqlParameter("@trxtype", sqlTrnsTypeDeduction);
                            parmsClaims[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsClaims[3] = new SqlParameter("@enddate", currentenddate);
                            parmsClaims[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                            parmsClaims[5] = new SqlParameter("@addtype", "Claim");
                            parmsClaims[6] = new SqlParameter("@stattype", 'L');
                            parmsClaims[7] = new SqlParameter("@claimstatus", 1);
                            DataSet rptClaimsDs = new DataSet();
                            rptClaimsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sClaimsSQL, parmsClaims);

                            dtClaims = rptClaimsDs.Tables[0];

                        }
                        dtEmployee.PrimaryKey = new DataColumn[] { dtEmployee.Columns["EmpCode"] };
                        //dtPayRollView.PrimaryKey = new DataColumn[] { dtPayRollView.Columns["emp_code"] };
                        //dtAdditions.PrimaryKey = new DataColumn[] { dtAdditions.Columns["emp_code"] };
                        //dtDeductions.PrimaryKey = new DataColumn[] { dtDeductions.Columns["emp_code"] };
                        dsReportResult.Tables.Add(dtEmployee);
                        dsReportResult.Tables.Add(dtPayRollView);
                        //dsReportResult.Tables.Add(dtAdditions);
                        //dsReportResult.Tables.Add(dtDeductions);
                        // Loading data into dt1, dt2:
                        //DataRelation drel = new DataRelation("EquiJoin", dtEmployee.Columns["EmpCode"], dtPayRollView.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drel);
                        //DataRelation drelAdditions = new DataRelation("AddJoin", dtEmployee.Columns["emp_code"], dtAdditions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelAdditions);
                        //DataRelation drelDeductions = new DataRelation("DedJoin", dtEmployee.Columns["emp_code"], dtDeductions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelDeductions);
                        DataTable jt = new DataTable("Table5");
                        //jt = getSchemedTable(dtEmployee, dtPayRollView);
                        jt = merge(dtEmployee, dtPayRollView, dtAdditions, dtDeductions, dtClaims, sqlSelect1, sqlSelect2, sqlSelect3, currentstartdate, currentenddate, sqlQueryPayroll, sqlTrnsTypeAddition, sqlTrnsTypeDeduction, sqlTrnsTypeClaims, listDates);
                        
                        dsEmpResult.Tables.Add(jt);
                        Session["rptDs"] = dsEmpResult;
                        Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=26");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "monthyear", "alert('Please Select month and year');", true);
                }

            }

        }
        protected void btncommon_Click(object sender, System.EventArgs e)
        {
          //if( Session["CategoryName"].ToString ()=="Employee")
          //  {


          //  }

            Calendar c = new Calendar();
            bool datePeriodFlag = false;
            //int mDay = c.get(Calendar.DAY_OF_MONTH);
            int mYear = 0;
            int sMonth = 0;
            int eMonth = 0;
            List<DateTime> listDates;


            mYear = c.TodaysDate.Year;
            sMonth = c.TodaysDate.Month;
            eMonth = 0;
            datePeriodFlag = false;
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
            DateTime endCollDate = DateTime.Now;
            if (Session["CategoryName"].ToString() == "Expiry")
            {
                 endCollDate = Convert.ToDateTime(expirydp.SelectedDate.ToString());
            }
            listDates = new List<DateTime>();
            while (startCollDate < endCollDate)
            {
                listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                startCollDate = startCollDate.AddMonths(1);

            }
            if (Session["CategoryId"].ToString() != "")
                {
                if (Session["TemplateId"].ToString() != "")
                {
                    if (Session["CategoryName"].ToString() == "Employee")
                    {
                        GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                    }
                    else if (Session["CategoryName"].ToString() == "Grouping")
                    {
                        GenerateGrouping(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                    else if (Session["CategoryName"].ToString() == "Expiry")
                    {
                        GenerateCertificateExpiry(endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);

                    }
                    else if (Session["CategoryName"].ToString() == "Certificate")
                    {
                        GenerateCertificate(Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);

                    }
                    else if (Session["CategoryName"].ToString() == "email")
                    {
                       
                        Generateemail(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                    else if (Session["CategoryName"].ToString() == "pvr")
                    {

                        Generatepvr(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                    else if (Session["CategoryName"].ToString() == "variance")
                    {

                        Generatevariance(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                    else if (Session["CategoryName"].ToString() == "yps")
                    {

                        Generateyps(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                    else if (Session["CategoryName"].ToString() == "wfa")
                    {

                        Generatewfa(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                    }
                }
                
                else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }
            

        }
        protected void ButtonMonthSelection_Click(object sender, System.EventArgs e)
        {
            Calendar c = new Calendar();
            bool datePeriodFlag = false;
            //int mDay = c.get(Calendar.DAY_OF_MONTH);
            int mYear = 0;
            int sMonth = 0;
            int eMonth = 0;
            List<DateTime> listDates;
            if (((System.Web.UI.WebControls.Button)sender).ID == "btnCurrentMonth")
            {
              
                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month;
                eMonth = 0;
                datePeriodFlag = false;
                DateTime today = DateTime.Today;
                DateTime startDate = new DateTime(today.Year, today.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
                DateTime endCollDate = startDate.AddMonths(1).AddDays(-1);
                listDates = new List<DateTime>();
                while (startCollDate < endCollDate)
                {
                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                    startCollDate = startCollDate.AddMonths(1);

                }
                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        if (Session["CategoryName"].ToString() == "Leaves")
                        {
                            GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else if (Session["CategoryName"].ToString() == "Expiry")
                        {
                           // GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else if (Session["CategoryName"].ToString() == "Grouping")
                        {
                            GenerateGrouping(Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                        }
                        //else if (Session["CategoryName"].ToString() == "email")
                        //{
                        //    Generateemail(startCollDate, endCollDate, Session["CategoryId"].ToString(), Session["TemplateId"].ToString(), Session["CategoryName"].ToString());

                        //}

                        else
                        {
                            GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }

                       
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }

            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnPreviousMonth")
            {
                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month - 1;
                eMonth = 0;
                datePeriodFlag = false;
                DateTime today = DateTime.Now.AddMonths(-1);
                DateTime startDate = new DateTime(today.Year, today.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);
                DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
                DateTime endCollDate = startDate.AddMonths(1).AddDays(-1);
                listDates = new List<DateTime>();
                while (startCollDate < endCollDate)
                {
                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                    startCollDate = startCollDate.AddMonths(1);

                }
                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        if (Session["CategoryName"].ToString() == "Leaves")
                        {
                            GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else if (Session["CategoryName"].ToString() == "Expiry")
                        {
                            //GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else
                        {

                            GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnThreeMonth")
            {
                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month - 2;
                eMonth = c.TodaysDate.Month;
                datePeriodFlag = true;
                DateTime today = DateTime.Now.AddMonths(-3);
                DateTime startDate = new DateTime(today.Year, today.Month, 1);
                DateTime endDate = startDate.AddMonths(3).AddDays(-1);
                DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
                DateTime endCollDate = startDate.AddMonths(3).AddDays(-1);
                listDates = new List<DateTime>();
                while (startCollDate < endCollDate)
                {
                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                    startCollDate = startCollDate.AddMonths(1);

                }
                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        if (Session["CategoryName"].ToString() == "Leaves")
                        {
                            GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else if (Session["CategoryName"].ToString() == "Expiry")
                        {
                          //  GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else
                        {
                            GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnSixMonth")
            {
                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month - 5;
                eMonth = c.TodaysDate.Month;
                datePeriodFlag = true;
                DateTime today = DateTime.Now.AddMonths(-5);
                DateTime startDate = new DateTime(today.Year, today.Month, 1);
                DateTime endDate = startDate.AddMonths(6).AddDays(-1);
                DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
                DateTime endCollDate = startDate.AddMonths(6).AddDays(-1);
                //DateTime tdate = DateTime.Now;
               // DateTime firstOfNextMonth = new DateTime(tdate.Year, tdate.Month, 1).AddMonths(1);
               // DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

                //DateTime d6 = DateTime.Now.AddMonths(-5);

               // DateTime startCollDate = DateTime.Now.AddMonths(-5);
               // DateTime endCollDate = new DateTime(tdate.Year , tdate.Month, lastOfThisMonth.Day);
                
                listDates = new List<DateTime>();
                while (startCollDate < endCollDate)
                {
                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                    startCollDate = startCollDate.AddMonths(1);

                }
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        if (Session["CategoryName"].ToString() == "Leaves")
                        {
                           GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);

                        }
                        else if (Session["CategoryName"].ToString() == "Expiry")
                        {
                            //GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else
                        {
                            GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnOneYear")
            {
                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month - 11;
                eMonth = c.TodaysDate.Month;
                datePeriodFlag = true;
                DateTime today = DateTime.Now.AddMonths(-12);
                DateTime startDate = new DateTime(today.Year, today.Month, 1);
                DateTime endDate = startDate.AddMonths(12).AddDays(-1);
                DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
                DateTime endCollDate = startDate.AddMonths(12).AddDays(-1);
                listDates = new List<DateTime>();
                while (startCollDate < endCollDate)
                {
                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
                    startCollDate = startCollDate.AddMonths(1);

                }
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        if (Session["CategoryName"].ToString() == "Leaves")
                        {
                            GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else if (Session["CategoryName"].ToString() == "Expiry")
                        {
                            //GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                        else
                        {
                            GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
                }
            }

        }
        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            RadGrid1.DataBind();
        }
        protected void rdTemplateList_selectedIndexChanged(object sender, EventArgs e)
        {
            int itemIndex = 0;
            RadioButtonList radioList = (sender as RadioButtonList);
            
            //Get the Repeater Item reference
             DataListItem item = radioList.NamingContainer as DataListItem;

            //Get the repeater item index
               int index = item.ItemIndex;
               DataList dlCategoryList = Page.FindControl("dlCategory") as DataList;
               for (int i = 0; i < dlCategoryList.Items.Count; i++)
               {
                   if(index!=i)
                   {
                      RadioButtonList rdListItem = dlCategoryList.Items[i].FindControl("rdEmployeeList") as RadioButtonList;
                      rdListItem.ClearSelection();
                   }            
                 
               }
               RadioButtonList rdList = dlCategoryList.Items[index].FindControl("rdEmployeeList") as RadioButtonList;

               foreach (ListItem answer in rdList.Items)
                {
                    // rdList.SelectedIndex = -1;
                    bool isSelected = answer.Selected;
                    if (isSelected)
                    {
                        Label itemCategoryID = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryID")) as Label;
                        Label itemCategoryName = ((Label)dlCategoryList.Items[index].FindControl("lblCategoryName")) as Label;
                        Session["CategoryName"] = itemCategoryName.Text;
                        Session["CategoryId"] = itemCategoryID.Text;
                        Session["TemplateId"] = answer.Value;
                        Session["TemplateName"] = answer.Text;
                      
                    }
                }


        }
        protected void dlDepartment_selectedIndexChanged(object sender, EventArgs e)
        {

            string sqlSelect;
            DataSet empDs;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            sgroupname = Utility.ToString(Session["GroupName"]);
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlDepartment")
            {
                if (ddlDepartment.SelectedValue != "-2")
                {

                    //btnCurrentMonth.Enabled = true;
                    //btnPreviousMonth.Enabled = true;
                    //btnThreeMonth.Enabled = true;
                    //btnSixMonth.Enabled = true;
                    //btnOneYear.Enabled = true;
                    //btnCustom.Enabled = true;
                    //if (dlDept.SelectedValue == "-1")
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    if (ddlDepartment.SelectedValue == "-1")
                    //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (sgroupname == "Super Admin")
                        {
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no,(select Deptname from department where id=dept_id)Department,ic_pp_number  FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no,(select Deptname from department where id=dept_id)Department,(select Designation from designation where id=desig_id) design,(select Trade from trade where id=trade_id) trade FROM dbo.employee WHERE COMPANY_ID=" + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no,(select Deptname from department where id=dept_id)Department,(select Designation from designation where id=desig_id) design,(select Trade from trade where id=trade_id) trade FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";
                        }


                    }
                    else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no,(select Deptname from department where id=dept_id)Department,(select Designation from designation where id=desig_id) design,(select Trade from trade where id=trade_id) trade FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDepartment.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no,(select Deptname from department where id=dept_id)Department,(select Designation from designation where id=desig_id) design,(select Trade from trade where id=trade_id) trade FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDepartment.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }

                    }
                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid1.DataSource = empDs.Tables[0];
                        RadGrid1.DataBind();

                       
                        if (RadGrid1.Visible == false)
                        {
                            RadGrid1.Visible = true;
                            RadGrid1.MasterTableView.Visible = true;
                        }
                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {
                            item.Selected = true;
                        }
                        if (Session["CategoryName"].ToString() == "Employee" || Session["CategoryName"].ToString() == "Grouping" || Session["CategoryName"].ToString() == "Expiry" || Session["CategoryName"].ToString() == "Certificate" || Session["CategoryName"].ToString() == "email" || Session["CategoryName"].ToString() == "pvr")
                        {
                            dept_div.Visible = true;
                            btncommon.Visible = true;

                        }
                        else
                        {
                            dept_div.Visible = true;
                            btnCurrentMonth.Visible = true;
                            btnPreviousMonth.Visible = true;
                            btnThreeMonth.Visible = true;
                            btnSixMonth.Visible = true;
                            btnOneYear.Visible = true;
                            btnCustom.Visible = true;

                        }
                        if (Session["CategoryName"].ToString() == "Expiry")
                        {
                            dept_div.Visible = true;
                            expirydate.Visible = true;
                        }
                        if (Session["CategoryName"].ToString() == "pvr")
                        {
                            dept_div.Visible = false;
                            pvr_div.Visible = true;
                            RadGridPaymentVariance.Visible = true;
                        }
                        if (Session["CategoryName"].ToString() == "yps")
                        {
                            dept_div.Visible = false;
                            yps_div.Visible = true;
                            RadGridYearlySummaryReport.Visible = true;
                        }
                    }
                    else
                    {
                        dept_div.Visible = true;
                        btnCurrentMonth.Visible = false;
                        btnPreviousMonth.Visible = false;
                        btnThreeMonth.Visible = false;
                        btnSixMonth.Visible = false;
                        btnOneYear.Visible = false;
                        btnCustom.Visible = false;
                        btncommon.Visible = false;
                        expirydate.Visible = false;
                        pvr_div.Visible = false;
                        yps_div.Visible = false;
                    }
                }
                else
                {
                    dept_div.Visible = true;
                    RadGrid1.Visible = false;
                    RadGrid1.MasterTableView.Visible = false;

                    btnCurrentMonth.Visible = false;
                    btnPreviousMonth.Visible = false;
                    btnThreeMonth.Visible = false;
                    btnSixMonth.Visible = false;
                    btnOneYear.Visible = false;
                    btnCustom.Visible = false;
                    btncommon.Visible = false;
                    expirydate.Visible = false;
                    pvr_div.Visible = false;
                    yps_div.Visible = false;
                }
            }
            //string registerKey = "document.getElementById('tremployee').style.visibility = \"visible\";";
            //Page.ClientScript.RegisterClientScriptBlock(GetType(), "id", registerKey, true);

            


        }
        void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlSelect;
            DataSet empDs;
            //if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
            {
                if (ddlDepartment.SelectedValue != "-2")
                {
                    if (ddlDepartment.SelectedValue == "-1")

                    //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no  FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no  FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";
                        }

                    }
                    else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDepartment.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDepartment.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid1.DataSource = empDs.Tables[0];
                        //RadGrid1.DataBind();
                        if (RadGrid1.Visible == false)
                        {
                            RadGrid1.Visible = true;
                            RadGrid1.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid1.Visible = false;
                    RadGrid1.MasterTableView.Visible = false;
                }
                //string registerKey = "document.getElementById('tremployee').style.visibility = \"visible\";";
                //Page.ClientScript.RegisterClientScriptBlock(GetType(), "id", registerKey, true);
            }
        }
        protected void GenerateCommonReports(DateTime startDate, DateTime endDate, bool PeriodFlag, int CategoryId, int TemplateID, List<DateTime> listDates)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string currentstartdate = Convert.ToDateTime(startDate.ToShortDateString()).ToString("dd/MM/yyyy", format);
            string currentenddate = Convert.ToDateTime(endDate.ToShortDateString()).ToString("dd/MM/yyyy", format);
            Session["StartPeriod"] = currentstartdate.ToString();
            Session["EndPeriod"] = currentenddate.ToString();
            if (currentstartdate != "" && currentenddate != "")
            {
               
              
                string sqlQuery = "";
                string strEmployee = "0";
                string sqlSelect = "select e1.emp_code Emp_Code,(select time_card_no from employee where emp_code=e1.emp_code) TimeCardId,(select isnull(emp_name,'')+' '+isnull(emp_lname,'') from employee where emp_code=e1.emp_code)  Full_Name,";      
                int grid1 = 0;
                int grid2 = 0;
                string sqlTrnsTypeAddition = "0";
                string sqlTrnsTypeDeduction = "0";
                string sqlTrnsTypeClaims = "0";
                
                string selectSQL = "";
                string sqlStr = "";
                string sqlAdditionStr = "";
                string sqlPayStr = "";
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                DataTable dtTableAdd = new DataTable();
                DataSet dsTableAdd = new DataSet();
                bool sqlSelect1 = false;
                bool sqlSelect2 = false;
                bool sqlSelect3 = false;
                selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " order by seq_order";
                dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                dtTable = dsTable.Tables[0];
                //get template name
                string tname = dtTable.Rows[0]["TemplateName"].ToString();

                // muru for ordering
                string selectSQL2 = "Select top(1) sort_order,ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and sort_order>0";
                SqlDataReader sortdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL2, null);
                string sortfield = "";
                if(sortdr.Read())
                {
                    if (sortdr["sort_order"].ToString() == "1")
                        sortfield = sortdr["ALIAS_NAME"].ToString();
                    else
                        sortfield = sortdr["ALIAS_NAME"].ToString() + " desc";
                }
                // muru for grouping
                string selectSQL3 = "Select top(2) ALIAS_NAME  from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + " and group_order >0 order by group_order";
                SqlDataReader groupdr = DataAccess.ExecuteReader(CommandType.Text, selectSQL3, null);
                string groupfield = "";
                int c = 0;
                while (groupdr.Read())
                {
                    c = c + 1;
                    if (c == 1)
                    {
                        groupfield = "&g1="+ groupdr[0].ToString();
                    }
                    else if(c==2)
                    {
                        groupfield = groupfield+ "&g2=" + groupdr[0].ToString();
                    }
                    
                }
                if(c==1)
                    groupfield = groupfield + "&g2=a";
                // muru
                if (dtTable.Rows.Count == 1 && dtTable.Rows[0]["ColumnID"].ToString() == "0" && dtTable.Rows[0]["TableID"].ToString() == "3")
                    selectSQL = "select id as ColumnID,TableID from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES') or COMPANY_ID = -1 ) and OPTIONSELECTION in ('General','Variable')";
                if (dtTable.Rows.Count == 1 && dtTable.Rows[0]["ColumnID"].ToString() == "0" && dtTable.Rows[0]["TableID"].ToString() == "4")
                    selectSQL = "select  id as ColumnID,TableID from dbo.ViewDeductionsDed  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) ";
                if (dtTable.Rows.Count == 1 && dtTable.Rows[0]["ColumnID"].ToString() == "0" && dtTable.Rows[0]["TableID"].ToString() == "5")
                    selectSQL = "select id as ColumnID,TableID  from dbo.ViewAdditionTypesDescAdd WHERE (COMPANY_ID = " + compid + " OR(isShared = 'YES')) and OPTIONSELECTION = 'Claim'";
                if (dtTable.Rows.Count == 1 && dtTable.Rows[0]["ColumnID"].ToString() == "0" && dtTable.Rows[0]["TableID"].ToString() == "7")
                    selectSQL = "SELECT  id as ColumnID,7 TableID FROM Leave_Types WHERE CompanyID=-1 OR CompanyID="+ compid;



                    dtTable.Clear();
                dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                dtTable = dsTable.Tables[0];
                //-----

                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "3" ) // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect1 = true;
                            sqlTrnsTypeAddition = sqlTrnsTypeAddition + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }

                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect2 = true;
                            sqlTrnsTypeDeduction = sqlTrnsTypeDeduction + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }
                if (currentstartdate != "" && currentenddate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "5") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlSelect3 = true;
                            sqlTrnsTypeClaims = sqlTrnsTypeClaims + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        }
                    }
                }
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            grid1++;
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }
                Session["StringEmployee"] = Convert.ToString(strEmployee.ToString());

                string sqlQueryEmployee = "Select e1.emp_code as EmpCode,e1.emp_name as EmpName,e1.time_card_no as TimeCardNo,";
                string sqlQueryPayroll = "Select pv.emp_code,";

                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    grid2++;
                    if (dtTable.Rows[i]["TableID"].ToString().Trim() == "1")
                    {
                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "time_card_no")
                        {
                            sqlQueryEmployee = sqlQueryEmployee + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                           
                        }
                    }
                    else if (dtTable.Rows[i]["TableID"].ToString().Trim() == "2")
                    {
                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "time_card_no")
                        {
                            if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "BasicPay")
                            {
                                string str = "";
                                //str = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=  MONTH(Convert(Datetime,'" + currentstartdate + "',103))and YEAR(start_period)=YEAR(Convert(Datetime,'" + currentstartdate + "',103)) and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";
                                str = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code AND  status='G' order by emp_code Desc)";
                                sqlQueryPayroll = sqlQueryPayroll + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                            }
                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "NetPay")
                            {
                                string str1 = "";
                                //str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)='" + Convert.ToInt32(sMonth) + "' and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";
                                str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay))) from  PayRollView1 Where  emp_code=e1.emp_code AND  status='G' order by emp_code Desc)";
                                sqlQueryPayroll = sqlQueryPayroll + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                            }
                            else
                            {
                                sqlQueryPayroll = sqlQueryPayroll + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                            }
                          
                        }
                    }

                }

                if (grid1 > 0)
                {
                    if (grid2 > 0)
                    {
                        sqlQueryEmployee = sqlQueryEmployee.Remove(sqlQueryEmployee.Length - 1, 1);
                        sqlQueryEmployee = sqlQueryEmployee + " from Employee e1";
                        sqlQueryEmployee = sqlQueryEmployee + " where e1.emp_code in (" + strEmployee + ")";
                        sqlQueryEmployee = sqlQueryEmployee + " ORDER BY EMP_NAME;";                      
                        DataSet dsEmpResult = new DataSet();
                        DataSet dsPayResult = new DataSet();
                        DataSet dsReportResult = new DataSet("DataSet");
                        DataTable dtEmployee = new DataTable("Table1");

                        SqlDataReader sqlEmpReader = DataAccess.ExecuteReader(CommandType.Text, sqlQueryEmployee, null);
                        dtEmployee.Load(sqlEmpReader);

                        DataTable dtPayRollView = new DataTable("Table2");
                        sqlQueryPayroll = sqlQueryPayroll.Remove(sqlQueryPayroll.Length - 1, 1);
                        sqlQueryPayroll = sqlQueryPayroll + " from Employee e1 LEFT OUTER JOIN PayRollView2 pv on e1.emp_code=pv.emp_code";
                        if (PeriodFlag)
                        {
                            sqlQueryPayroll = sqlQueryPayroll + " where pv.start_period between Convert(Datetime,'" + currentstartdate + "',103) And Convert(Datetime,'" + currentenddate + "',103) ";
                            sqlQueryPayroll = sqlQueryPayroll + " AND pv.end_period between Convert(Datetime,'" + currentstartdate + "',103) And Convert(Datetime,'" + currentenddate + "',103) ";
                            sqlQueryPayroll = sqlQueryPayroll + " And pv.STATUS in ('G')";
                            
                            
                        }
                        else
                        {
                            sqlQueryPayroll = sqlQueryPayroll + " where pv.start_period = Convert(Datetime,'" + currentstartdate + "',103) And pv.end_period=Convert(Datetime,'" + currentenddate + "',103)";
                            sqlQueryPayroll = sqlQueryPayroll + " And pv.STATUS in ('G')";
                        }
                        
                       
                        
                        SqlDataReader sqlPayReader = DataAccess.ExecuteReader(CommandType.Text, sqlQueryPayroll, null);
                        dtPayRollView.Load(sqlPayReader);

                        DataTable dtAdditions = new DataTable("Table3");
                        if (sqlSelect1)
                        {
                            string option = Session["optiontype"].ToString();
                            string sAdditionSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsAdditions = new SqlParameter[8];
                            parmsAdditions[0] = new SqlParameter("@empcode", strEmployee);
                            parmsAdditions[1] = new SqlParameter("@trxtype", sqlTrnsTypeAddition);
                            parmsAdditions[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsAdditions[3] = new SqlParameter("@enddate", currentenddate);
                           // parmsAdditions[4] = new SqlParameter("@claimtype", 1);
                            parmsAdditions[4] = new SqlParameter("@claimtype", option);
                            parmsAdditions[5] = new SqlParameter("@addtype", "ADD");
                            parmsAdditions[6] = new SqlParameter("@stattype", 'L');
                            parmsAdditions[7] = new SqlParameter("@claimstatus", 1);

                            DataSet rptAdditionsDs = new DataSet();
                            rptAdditionsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sAdditionSQL, parmsAdditions);

                            dtAdditions = rptAdditionsDs.Tables[0];
                        }

                        DataTable dtDeductions = new DataTable("Table4");
                        if (sqlSelect2)
                        {
                            string option = Session["optiontype"].ToString();
                            string sDeductionSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsDeductions = new SqlParameter[8];
                            parmsDeductions[0] = new SqlParameter("@empcode", strEmployee);
                            parmsDeductions[1] = new SqlParameter("@trxtype", sqlTrnsTypeDeduction);
                            parmsDeductions[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsDeductions[3] = new SqlParameter("@enddate", currentenddate);
                            //parmsDeductions[4] = new SqlParameter("@claimtype", 1);
                            parmsDeductions[4] = new SqlParameter("@claimtype", option);
                            parmsDeductions[5] = new SqlParameter("@addtype", "DED");
                            parmsDeductions[6] = new SqlParameter("@stattype", 'L');
                            parmsDeductions[7] = new SqlParameter("@claimstatus", 1);
                            DataSet rptDeductionDs = new DataSet();
                            rptDeductionDs = DataAccess.FetchRS(CommandType.StoredProcedure, sDeductionSQL, parmsDeductions);

                            dtDeductions = rptDeductionDs.Tables[0];

                         }
                         DataTable dtClaims = new DataTable("Table5");
                         if (sqlSelect3)
                         {
                            string option = Session["optiontype"].ToString();
                            string sClaimsSQL = "Sp_getpivotclaimsadditionscommon";
                             SqlParameter[] parmsClaims = new SqlParameter[8];
                             parmsClaims[0] = new SqlParameter("@empcode", strEmployee);
                             parmsClaims[1] = new SqlParameter("@trxtype", sqlTrnsTypeClaims);
                             parmsClaims[2] = new SqlParameter("@startdate", currentstartdate);
                             parmsClaims[3] = new SqlParameter("@enddate", currentenddate);
                             parmsClaims[4] = new SqlParameter("@claimtype", option);
                             parmsClaims[5] = new SqlParameter("@addtype", "Claim");
                             parmsClaims[6] = new SqlParameter("@stattype", 'L');
                             parmsClaims[7] = new SqlParameter("@claimstatus", 1);
                             DataSet rptClaimsDs = new DataSet();
                             rptClaimsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sClaimsSQL, parmsClaims);

                             dtClaims = rptClaimsDs.Tables[0];

                         }
                        dtEmployee.PrimaryKey = new DataColumn[] { dtEmployee.Columns["EmpCode"] };
                        //dtPayRollView.PrimaryKey = new DataColumn[] { dtPayRollView.Columns["emp_code"] };
                        //dtAdditions.PrimaryKey = new DataColumn[] { dtAdditions.Columns["emp_code"] };
                        //dtDeductions.PrimaryKey = new DataColumn[] { dtDeductions.Columns["emp_code"] };
                        dsReportResult.Tables.Add(dtEmployee);
                        dsReportResult.Tables.Add(dtPayRollView);
                        //dsReportResult.Tables.Add(dtAdditions);
                        //dsReportResult.Tables.Add(dtDeductions);
                        //Loading data into dt1, dt2:
                        //DataRelation drel = new DataRelation("EquiJoin", dtEmployee.Columns["EmpCode"], dtPayRollView.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drel);
                        //DataRelation drelAdditions = new DataRelation("AddJoin", dtEmployee.Columns["emp_code"], dtAdditions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelAdditions);
                        //DataRelation drelDeductions = new DataRelation("DedJoin", dtEmployee.Columns["emp_code"], dtDeductions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelDeductions);
                        DataTable jt = new DataTable();
                       //jt = getSchemedTable(dtEmployee, dtPayRollView);
                        jt = merge(dtEmployee, dtPayRollView, dtAdditions, dtDeductions, dtClaims, sqlSelect1, sqlSelect2, sqlSelect3, currentstartdate, currentenddate, sqlQueryPayroll, sqlTrnsTypeAddition, sqlTrnsTypeDeduction, sqlTrnsTypeClaims, listDates);
                       
                        if (sortfield != "" && groupfield != "")
                        {
                            
                            jt.DefaultView.Sort = sortfield;
                            jt = jt.DefaultView.ToTable(true);

                            dsEmpResult.Tables.Add(jt);
                            Session["rptDs"] = dsEmpResult;
                            Response.Redirect("../Reports/CommonReportGrouping.aspx?tname="+tname+""+groupfield);
                        }
                        else if (sortfield != "" && groupfield == "")
                        {
                            jt.DefaultView.Sort = sortfield;
                            jt = jt.DefaultView.ToTable(true);
                            dsEmpResult.Tables.Add(jt);
                            Session["rptDs"] = dsEmpResult;
                            Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=26");

                        }
                        else if (sortfield == "" && groupfield != "")
                        {
                            dsEmpResult.Tables.Add(jt);
                            Session["rptDs"] = dsEmpResult;
                            Response.Redirect("../Reports/CommonReportGrouping.aspx?tname=" + tname + "" + groupfield);


                        }
                        dsEmpResult.Tables.Add(jt);
                      
                        Session["rptDs"] = dsEmpResult;
                        Response.Redirect("../Reports/CommonReportsPage.aspx?PageType=26");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "monthyear", "alert('Please Select month and year');", true);
                }

            }
        }

      

        public DataTable merge(DataTable fatherTable, DataTable sonTable, DataTable additionTable, DataTable deductionTable, DataTable claimsTable, bool sqlSelect1, bool sqlSelect2, bool sqlSelect3, string startDate, string endDate, string sqlQueryPayroll, string sqlAdditionTypes, string sqlDeductionTypes, string sqlClaimsTypes, List<DateTime> listDates)
        {

            DataTable adddedResult = getAddDedTable(sonTable, additionTable, deductionTable, claimsTable);
            DataTable result = getSchemedTable(fatherTable, adddedResult);
            string sqlJoinQuery = "";
            for (int i = 0; i < fatherTable.Rows.Count; i++)
            {
                DataTable sonMasterTable = new DataTable();
                DataRow FatherRow = fatherTable.Rows[i];

                        sqlJoinQuery = sqlQueryPayroll + "and pv.emp_code in (" + fatherTable.Rows[i]["EmpCode"] + ")";
                        SqlDataReader sqlPayReader = DataAccess.ExecuteReader(CommandType.Text, sqlJoinQuery, null);
                        sonMasterTable.Load(sqlPayReader);
                        DataTable dtAdditionsTable = new DataTable();
                        DataTable dtDeductionsTable = new DataTable();
                        DataTable dtClaimsTable = new DataTable();
                       if (sonMasterTable.Rows.Count > 0 )
                       {
                           
                           for (int j = 0; j < sonMasterTable.Rows.Count; j++)
                           {
                               DataRow sonRow = sonMasterTable.Rows[j];
                               DataRow addRow = dtAdditionsTable.NewRow();
                               DataRow dedRow = dtDeductionsTable.NewRow();
                               DataRow claimRow = dtClaimsTable.NewRow();
                               dtAdditionsTable = getAdditions(sqlSelect1, sonMasterTable.Rows[j]["emp_code"].ToString(), sqlAdditionTypes, Convert.ToString(listDates[j]), Convert.ToString(listDates[j]));


                                   if (dtAdditionsTable.Rows.Count > 0)
                                   {
                                       addRow = dtAdditionsTable.Rows[0];
                                   }
                                   else
                                   {
                                       addRow = dtAdditionsTable.NewRow();
                                   }
                                   dtDeductionsTable = getDeductions(sqlSelect2, sonMasterTable.Rows[j]["emp_code"].ToString(), sqlDeductionTypes, Convert.ToString(listDates[j]), Convert.ToString(listDates[j]));
                                   if (dtDeductionsTable.Rows.Count > 0)
                                   {
                                       dedRow = dtDeductionsTable.Rows[0];
                                   }
                                   else
                                   {
                                       dedRow = dtDeductionsTable.NewRow();
                                   }
                                   dtClaimsTable = getClaims(sqlSelect3, sonMasterTable.Rows[j]["emp_code"].ToString(), sqlClaimsTypes, Convert.ToString(listDates[j]), Convert.ToString(listDates[j]));
                                   if (dtClaimsTable.Rows.Count > 0)
                                   {
                                       claimRow = dtClaimsTable.Rows[0];
                                   }
                                   else
                                   {
                                       claimRow = dtClaimsTable.NewRow();
                                   }
                              
                               DataRow ADRow = adddedResult.NewRow();
                               adddedResult.Rows.Add(compinAddDedRows(sonRow, addRow, dedRow, claimRow, ADRow, sonMasterTable, dtAdditionsTable, dtDeductionsTable, dtClaimsTable));
                               
                           }
                            for (int l = 0; l < adddedResult.Rows.Count; l++)
                            {
                                   DataRow sonSubRow = adddedResult.Rows[l];
                                   DataRow RROW = result.NewRow();
                                   result.Rows.Add(compinTwoRows(FatherRow, sonSubRow, RROW, fatherTable, adddedResult)); 
                            }
                            adddedResult.Rows.Clear();   
                      }                               
                     else
                     {
                         DataRow sonRow = adddedResult.NewRow();                        
                         DataRow RROW = result.NewRow();
                         result.Rows.Add(compinTwoRows(FatherRow, sonRow, RROW, fatherTable, adddedResult));

                     }
                          
            }

            return result;

        }

        public DataTable getAdditions(bool sqlSelect1, string empCode, string sqlAdditionTypes, string startDate, string endDate)
        {
            DataTable dtMasterAdditions = new DataTable();
            if (sqlSelect1)
            {
                string sAdditionSQL = "Sp_getpivotclaimsadditionscommon";
                SqlParameter[] parmsAdditions = new SqlParameter[8];
                parmsAdditions[0] = new SqlParameter("@empcode", empCode);
                parmsAdditions[1] = new SqlParameter("@trxtype", sqlAdditionTypes);
                parmsAdditions[2] = new SqlParameter("@startdate", startDate);
                parmsAdditions[3] = new SqlParameter("@enddate", endDate);
                parmsAdditions[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                parmsAdditions[5] = new SqlParameter("@addtype", "ADD");
                parmsAdditions[6] = new SqlParameter("@stattype", 'L');
                parmsAdditions[7] = new SqlParameter("@claimstatus", 1);


                DataSet rptMasterAdditionsDs = new DataSet();
                rptMasterAdditionsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sAdditionSQL, parmsAdditions);

                dtMasterAdditions = rptMasterAdditionsDs.Tables[0];
            }
            return dtMasterAdditions;
        }

        public DataTable getDeductions(bool sqlSelect2, string empCode, string sqlDeductionTypes, string startDate, string endDate)
        {

            DataTable dtMasterDeduction = new DataTable();
            if (sqlSelect2)
            {

                string sDeductionSQL = "Sp_getpivotclaimsadditionscommon";
                SqlParameter[] parmsDeductions = new SqlParameter[8];
                parmsDeductions[0] = new SqlParameter("@empcode", empCode);
                parmsDeductions[1] = new SqlParameter("@trxtype", sqlDeductionTypes);
                parmsDeductions[2] = new SqlParameter("@startdate", startDate);
                parmsDeductions[3] = new SqlParameter("@enddate", endDate);
                parmsDeductions[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                parmsDeductions[5] = new SqlParameter("@addtype", "DED");
                parmsDeductions[6] = new SqlParameter("@stattype", 'L');
                parmsDeductions[7] = new SqlParameter("@claimstatus", 1);
                DataSet rptMasterDeductionDs = new DataSet();
                rptMasterDeductionDs = DataAccess.FetchRS(CommandType.StoredProcedure, sDeductionSQL, parmsDeductions);

                dtMasterDeduction = rptMasterDeductionDs.Tables[0];
            }
            return dtMasterDeduction;
        }
        public DataTable getClaims(bool sqlSelect3, string empCode, string sqlClaimsTypes, string startDate, string endDate)
        {

            DataTable dtMasterClaims = new DataTable();
            if (sqlSelect3)
            {

                string sClaimsSQL = "Sp_getpivotclaimsadditionscommon";
                SqlParameter[] parmsClaims = new SqlParameter[8];
                parmsClaims[0] = new SqlParameter("@empcode", empCode);
                parmsClaims[1] = new SqlParameter("@trxtype", sqlClaimsTypes);
                parmsClaims[2] = new SqlParameter("@startdate", startDate);
                parmsClaims[3] = new SqlParameter("@enddate", endDate);
                parmsClaims[4] = new SqlParameter("@claimtype", Session["optiontype"].ToString());
                parmsClaims[5] = new SqlParameter("@addtype", "Claim");
                parmsClaims[6] = new SqlParameter("@stattype", 'L');
                parmsClaims[7] = new SqlParameter("@claimstatus", 1);
                DataSet rptClaimsDs = new DataSet();
                rptClaimsDs = DataAccess.FetchRS(CommandType.StoredProcedure, sClaimsSQL, parmsClaims);

                dtMasterClaims = rptClaimsDs.Tables[0];
            }
            return dtMasterClaims;
        }
        public DataTable getSchemedTable(DataTable main, DataTable branch)
        {

            DataTable result = new DataTable();

            for (int i = 0; i < main.Columns.Count; i++)
            {

                result.Columns.Add(main.Columns[i].ColumnName);

            }

            for (int j = 1; j < branch.Columns.Count; j++)
            {

                result.Columns.Add(branch.Columns[j].ColumnName);

            }
           

            return result;

        }

        public DataTable getAddDedTable(DataTable branch, DataTable additionTable, DataTable deductionTable, DataTable claimsTable)
        {

            DataTable resultAddDed = new DataTable();

         
            for (int j = 0; j < branch.Columns.Count; j++)
            {

                resultAddDed.Columns.Add(branch.Columns[j].ColumnName);

            }
            for (int k = 1; k < additionTable.Columns.Count; k++)
            {

                resultAddDed.Columns.Add(additionTable.Columns[k].ColumnName);

            }
            for (int l = 1; l < deductionTable.Columns.Count; l++)
            {

                resultAddDed.Columns.Add(deductionTable.Columns[l].ColumnName);

            }
            for (int m = 1; m < claimsTable.Columns.Count; m++)
            {

                resultAddDed.Columns.Add(claimsTable.Columns[m].ColumnName);

            }

            return resultAddDed;

        }
        private DataRow compinAddDedRows(DataRow sonRow, DataRow addRow, DataRow dedRow, DataRow claimRow, DataRow AddDedRow, DataTable son, DataTable addTable, DataTable dedTable, DataTable claimTable)
        {

            string mainColumnName;

           
            for (int j = 0; j < sonRow.ItemArray.Length; j++)
            {

                mainColumnName = son.Columns[j].ToString();
                if (son.Rows.Count > 0)
                {
                    AddDedRow[mainColumnName] = sonRow[mainColumnName];
                }
                else
                {
                    AddDedRow[mainColumnName] = "";
                }


            }
            for (int k = 1; k < addRow.ItemArray.Length; k++)
            {

                mainColumnName = addTable.Columns[k].ToString();
                if (addTable.Rows.Count > 0)
                {
                    AddDedRow[mainColumnName] = addRow[mainColumnName];
                }
                else
                {
                    AddDedRow[mainColumnName] = "";
                }


            }
            for (int l = 1; l < dedRow.ItemArray.Length; l++)
            {

                mainColumnName = dedTable.Columns[l].ToString();
                if (dedTable.Rows.Count > 0)
                {
                    AddDedRow[mainColumnName] = dedRow[mainColumnName];
                }
                else
                {
                    AddDedRow[mainColumnName] = "";
                }


            }
            for (int m = 1; m < claimRow.ItemArray.Length; m++)
            {

                mainColumnName = claimTable.Columns[m].ToString();
                if (claimTable.Rows.Count > 0)
                {
                    AddDedRow[mainColumnName] = claimRow[mainColumnName];
                }
                else
                {
                    AddDedRow[mainColumnName] = "";
                }


            }
            return AddDedRow;

        }
        private DataRow compinTwoRows(DataRow mainRow, DataRow sonRow, DataRow RRow, DataTable Father, DataTable son)
        {

            string mainColumnName;

            for (int i = 0; i < mainRow.ItemArray.Length; i++)
            {

                mainColumnName = Father.Columns[i].ToString();

                RRow[mainColumnName] = mainRow[mainColumnName];

            }


            for (int j = 1; j < sonRow.ItemArray.Length; j++)
            {

                mainColumnName = son.Columns[j].ToString();
                if (son.Rows.Count > 0)
                {
                    RRow[mainColumnName] = sonRow[mainColumnName];
                }
                else
                {
                   RRow[mainColumnName] = "";
                }
                

            }
           
            return RRow;

        }

        //MURU
        protected void GenerateGrouping(string CategoryId, string TemplateID, string CatName)
        {
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId + "";
            dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
            dtTable = dsTable.Tables[0];
            string colid = "";
            string mcat = "";
            for (int i = 0; i < dtTable.Rows.Count; i++)
            {
                if (dtTable.Rows[i]["TableID"].ToString() == "6") // cross checking with dropdownlistitem to gridboundcolumn text
                {
                    if (dtTable.Rows[i]["MainCategory"].ToString() == "Employee Type")
                    {
                        if (colid == "")
                            colid ="'"+ dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim()+"'";
                        else
                            colid = colid + "," + "'" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "'";
                    }
                    else {
                        if (colid == "")
                            colid = dtTable.Rows[i]["ColumnID"].ToString().Trim();
                        else
                            colid = colid + "," +dtTable.Rows[i]["ColumnID"].ToString().Trim() ;

                    }

                    mcat = dtTable.Rows[i]["MainCategory"].ToString().Trim();
                }
            }
            string strEmployee = "";
            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        if (strEmployee == "")
                            strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                        else
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }

            string sqlStr = "";
            switch (mcat)
            {
                case "Nationality":
                    sqlStr = " SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Nationality  As Category From employee inner join Nationality on employee.Nationality_id = Nationality.id WHERE NATIONALITY_ID IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY nATIONALITY,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    // sqlStr = " SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Nationality  As Category From employee inner join Nationality on employee.Nationality_id = Nationality.id WHERE NATIONALITY_ID IN(" + sqlTrns + ") GROUP BY nATIONALITY,emp_name,EMP_LNAME";
                    break;
                case "Country":
                    sqlStr = " SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Country  As Category From employee inner join Country on employee.Country_id = Country.id   WHERE Country_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY Country,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = " SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Country  As Category From employee inner join Country on employee.Country_id = Country.id   WHERE Country_id IN(" + sqlTrns + ") GROUP BY Country,emp_name,EMP_LNAME";
                    break;
                case "Religion":
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Religion As Category From employee inner join Religion on employee.Religion_id = Religion.id  WHERE Religion_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY Religion,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Religion As Category From employee inner join Religion on employee.Religion_id = Religion.id  WHERE Religion_id IN(" + sqlTrns + ") GROUP BY Religion,emp_name,EMP_LNAME";
                    break;
                case "Race":
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Race As Category  From employee inner join Race on employee.race_id = Race.id  WHERE race_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY Race,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Race As Category  From employee inner join Race on employee.race_id = Race.id  WHERE race_id IN(" + sqlTrns + ") GROUP BY Race,emp_name,EMP_LNAME";
                    break;
                case "Designation":
                    sqlStr = " SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Designation.Designation As Category From employee inner join Designation on employee.Desig_id = Designation.id  WHERE Desig_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY Designation.Designation,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = " SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Designation.Designation As Category From employee inner join Designation on employee.Desig_id = Designation.id  WHERE Desig_id IN(" + sqlTrns + ") GROUP BY Designation.Designation,emp_name,EMP_LNAME";
                    break;
                case "Department":
                    sqlStr = " SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,DeptName  As Category From employee inner join Department on employee.Dept_id = Department.id  WHERE Dept_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY DeptName,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = " SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,DeptName  As Category From employee inner join Department on employee.Dept_id = Department.id  WHERE Dept_id IN(" + sqlTrns + ") GROUP BY DeptName,emp_name,EMP_LNAME";
                    break;
                case "Sex":
                    //sqlTrns = sqlTrns.Replace("0,", "");
                    // sqlTrns = sqlTrns.Replace("F", "'F'");
                    // sqlTrns = sqlTrns.Replace("M", "'M'");
                    //sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case sex when 'M' Then 'Male' else 'Female' end  As Category  From employee   WHERE SEX IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY SEX,emp_name,emp_lname,EMP_LNAME,Time_card_no,dept_id";
                   //colid= colid.Replace('1', 'M');
                   // colid= colid.Replace('2', 'F');
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case sex when 'M' Then 'Male' else 'Female' end  As Category  From employee   WHERE  emp_code in(" + strEmployee + ") GROUP BY SEX,emp_name,emp_lname,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case sex when 'M' Then 'Male' else 'Female' end  As Category  From employee   WHERE SEX IN(" + sqlTrns + ") GROUP BY SEX,emp_name,emp_lname,EMP_LNAME";
                    break;
                case "Marital Status":
                    //sqlTrns = sqlTrns.Replace("0,", "");
                    //sqlTrns = sqlTrns.Replace("S", "'S'");
                    //sqlTrns = sqlTrns.Replace("M", "'M'");
                    //sqlTrns = sqlTrns.Replace("D", "'D'");
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case Marital_Status when 'S' Then 'Single'  when 'D' then 'Divorce' else 'Married' end As Category  From employee  WHERE Marital_Status IN(" + sqlTrns + ") GROUP BY Marital_Status,emp_name,EMP_LNAME";
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case Marital_Status when 'S' Then 'Single'  when 'D' then 'Divorce' else 'Married' end As Category  From employee  WHERE Marital_Status is not null and  emp_code in(" + strEmployee + ") GROUP BY Marital_Status,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    break;
                case "Emp_Group":
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,EmpgroupName As Category From employee inner join Emp_Group E on emp_group_id = e.id  WHERE emp_group_id IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY EmpgroupName,emp_name,EMP_LNAME,Time_card_no,dept_id";
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,EmpgroupName As Category From employee inner join Emp_Group E on emp_group_id = e.id  WHERE emp_group_id IN(" + sqlTrns + ") GROUP BY EmpgroupName,emp_name,EMP_LNAME";
                    break;
                case "Employee Type":
                    sqlStr = "SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Emp_Type As Category  From employee  WHERE emp_tYPE IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY emp_name,EMP_LNAME,Emp_Type,Time_card_no,dept_id ";
                    //sqlStr = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Emp_Type As Category  From employee  WHERE emp_tYPE IN(" + sqlTrns + ") GROUP BY emp_name,EMP_LNAME,Emp_Type ";
                    break;
                case "Place of birth":
                    sqlStr = " SELECT Time_card_no,(select Deptname from department where id=dept_id)Department ,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Place_of_birth As Category From employee  WHERE Place_of_birth IN(" + colid + ") and emp_code in(" + strEmployee + ") GROUP BY emp_name,EMP_LNAME,Place_of_birth,Time_card_no,dept_id ";
                    //sqlStr = " SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Place_of_birth As Category From employee  WHERE Place_of_birth IN(" + sqlTrns + ") GROUP BY emp_name,EMP_LNAME,Place_of_birth ";
                    break;
            }
            //sqlSelect = " SELECT emp_name, ";
            //sqlSelect = sqlSelect + " where e.emp_code in (" + strEmployee + ")";
            //sqlSelect = sqlSelect + " AND trx_type in (" + sqlTrns + ") and optionselection = 'Claim' AND STATUS='L' AND  month(trx_period) BETWEEN  Month('" + RadDatePicker7.SelectedDate + "') AND  Month('" + RadDatePicker8.SelectedDate + "')   and   year(trx_period) BETWEEN year('" + RadDatePicker7.SelectedDate + "') AND year('" + RadDatePicker8.SelectedDate + "')";
            //sqlSelect = sqlSelect + "GROUP BY id, description,emp_name,trx_period";
            if (sqlStr.Length > 0)
            {
                DataSet rptDs = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                Session["rptDs"] = rptDs;
                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=55");
            }

        }
        protected void ddlPaymentVariance_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlStrPaymentVariance = "";
            if (ddlPaymentVariance.SelectedValue == "2")
            {
                sqlStrPaymentVariance = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + compid + ")  Order By emp_name";

            }

            else if (ddlPaymentVariance.SelectedValue == "3")
            {
                sqlStrPaymentVariance = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + compid + ")  Order By DeptName";

            }
            else if (ddlPaymentVariance.SelectedValue == "4")
            {
                sqlStrPaymentVariance = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + compid + ")  Order By Company_name";

            }
            if (sqlStrPaymentVariance.Length > 0)
            {
                RadGridPaymentVariance.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStrPaymentVariance, null);
                RadGridPaymentVariance.DataBind();
                if (RadGridPaymentVariance.Items.Count >0)
                {
                    RadGridPaymentVariance.Visible = true;
                    RadGridPaymentVariance.MasterTableView.Visible = true;
                    btncommon.Visible = true;
                }
                   
            }
        }
        protected void ddlYearlySummaryReport_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlYearlySummaryReport = "";
            if (ddlYearlySummaryReport.SelectedValue == "2")
            {
                sqlYearlySummaryReport = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + compid + ")  Order By emp_name";

            }

            else if (ddlYearlySummaryReport.SelectedValue == "3")
            {
                sqlYearlySummaryReport = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + compid + ")  Order By DeptName";

            }
            else if (ddlYearlySummaryReport.SelectedValue == "4")
            {
                sqlYearlySummaryReport = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + compid + ")  Order By Company_name";

            }
            if (sqlYearlySummaryReport.Length > 0)
            {
                RadGridYearlySummaryReport.DataSource = DataAccess.FetchRS(CommandType.Text, sqlYearlySummaryReport, null);
                RadGridYearlySummaryReport.DataBind();
                RadGridYearlySummaryReport.Visible = true;
                RadGridYearlySummaryReport.MasterTableView.Visible = true;
            }
        }
        private void bindgrid()
        {

            string sqlStrPaymentVariance = "";
            if (ddlPaymentVariance.SelectedValue == "2")
            {
                sqlStrPaymentVariance = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By emp_name";

            }

            else if (ddlPaymentVariance.SelectedValue == "3")
            {
                sqlStrPaymentVariance = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By DeptName";

            }
            else if (ddlPaymentVariance.SelectedValue == "4")
            {
                sqlStrPaymentVariance = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By Company_name";

            }
            if (sqlStrPaymentVariance.Length > 0)
            {
                RadGridPaymentVariance.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStrPaymentVariance, null);
                RadGridPaymentVariance.DataBind();
                if (RadGridPaymentVariance.Items.Count > 0)
                {
                    RadGridPaymentVariance.Visible = true;
                    RadGridPaymentVariance.MasterTableView.Visible = true;
                    btncommon.Visible = true;
                }

            }
        }
        private void bindgrid2()
        {
            string sqlYearlySummaryReport = "";
            if (ddlYearlySummaryReport.SelectedValue == "2")
            {
                sqlYearlySummaryReport = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By emp_name";

            }

            else if (ddlYearlySummaryReport.SelectedValue == "3")
            {
                sqlYearlySummaryReport = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By DeptName";

            }
            else if (ddlYearlySummaryReport.SelectedValue == "4")
            {
                sqlYearlySummaryReport = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + Utility.ToString(Utility.ToInteger(Session["Compid"].ToString())) + ")  Order By Company_name";

            }
            if (sqlYearlySummaryReport.Length > 0)
            {
                RadGridYearlySummaryReport.DataSource = DataAccess.FetchRS(CommandType.Text, sqlYearlySummaryReport, null);
                RadGridYearlySummaryReport.DataBind();
                RadGridYearlySummaryReport.Visible = true;
                RadGridYearlySummaryReport.MasterTableView.Visible = true;
            }
        }
        }
}