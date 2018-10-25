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
using Telerik.Web.UI;
using System.Net.Mail;
using System.Text;
using System.Collections.Generic;

namespace SMEPayroll.Reports
{
    public partial class CommonReportsPage : System.Web.UI.Page
    {
        int compid;
       
        string strPage = "";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet sqlRptDs;
       
        int k = 1;
      
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        public static string GetConnectionString()
        {
            string connectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnection"].ToString();
            return connectionStr;
        }

        protected void RadGrid1_ColumnCreated(object sender, Telerik.Web.UI.GridColumnCreatedEventArgs e)
        {

        }
   
        protected void empResults_ItemDataBound(object sender, GridItemEventArgs e)
        {      
            string columnName1 = "";
            string columnName2 = "";
            string columnName3 = "";
            string columnName4 = "";

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (Request.QueryString.Count > 1)
                {
                    string startMonth = Request.QueryString["SM"].ToString();
                    string endMonth = Request.QueryString["EM"].ToString();

                    if (startMonth.Length > 0)
                    {
                        if (Session["DataTable"] != null)
                        {
                            DataTable dtnew = new DataTable();
                            dtnew = (DataTable)Session["DataTable"];
                            columnName1 = dtnew.Columns[1].Caption;
                            columnName3 = dtnew.Columns[2].Caption;
                            columnName4 = dtnew.Columns[3].Caption;
                            // dtnew.Columns[4].Caption;

                        }

                        string v1 = item[columnName1].Text;
                        string v3 = item[columnName3].Text;
                        string v4 = item[columnName4].Text;

                        if (v1 == "&nbsp;")
                        {
                            v1 = "0";
                        }
                        if (v3 == "&nbsp;")
                        {
                            v3 = "0";
                        }
                        if (v4 == "&nbsp;")
                        {
                            v4 = "0";
                        }

                        item[columnName4].Text = Convert.ToString(Convert.ToDecimal(v3) - Convert.ToDecimal(v1));
                        if ((Convert.ToDecimal(v3) - Convert.ToDecimal(v1)) < 0)
                        {

                            TableCell tableCell = (TableCell)item["Status"];
                            System.Web.UI.WebControls.Image img = new Image();

                            string imageUrl = "~/Frames/Images/REPORTS/Symbol Down 2.png";
                            //Add the Image Web Control to the cell                       
                            img.ImageUrl = imageUrl;
                            img.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                            tableCell.Wrap = false;
                            tableCell.Controls.Add(img);
                        }
                        if ((Convert.ToDecimal(v3) - Convert.ToDecimal(v1)) > 0)
                        {
                            TableCell tableCell = (TableCell)item["Status"];
                            System.Web.UI.WebControls.Image img = new Image();

                            string imageUrl = "~/Frames/Images/REPORTS/Symbol Up.png";
                            //Add the Image Web Control to the cell                       
                            img.ImageUrl = imageUrl;
                            img.Style.Add(HtmlTextWriterStyle.MarginLeft, "10px");
                            tableCell.Wrap = false;
                            tableCell.Controls.Add(img);

                        }
                    }
                }

            }
        }


        protected void RadGrid1_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {
            if (Request.QueryString.Count > 1)
            {
                string startMonth = Request.QueryString["SM"].ToString();
                string endMonth = Request.QueryString["EM"].ToString();

                if (startMonth.Length > 0)
                {

                }
                else
                {
                    sqlRptDs = (DataSet)Session["rptDs"];
                    empResults.DataSource = sqlRptDs;
                    empResults.DataBind();

                }
            }
            else
            {
                sqlRptDs = (DataSet)Session["rptDs"];
                empResults.DataSource = sqlRptDs;
                empResults.DataBind();

            }
        }
        protected void btnExportWord_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToWord();
        }

        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            ExportToExcel(sqlRptDs, 0, Response, Session["TemplateName"].ToString());
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.IgnorePaging = true;
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToExcel();
        }
        protected void btnExportPdf_click(object sender, EventArgs e)
        {
            empResults.ExportSettings.ExportOnlyData = true;
            empResults.ExportSettings.Pdf.PageWidth = Unit.Parse(Utility.ToString((empResults.Items[0].Cells.Count * 30)) + "mm");
            empResults.ExportSettings.OpenInNewWindow = true;
            empResults.MasterTableView.ExportToPdf();
        }

        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            
            string CompanyName = Session["CompanyName"].ToString();
            //string TemplateName = lblReportName.Text;
            string ReportName = FileName;
            
            string GenerateBy = Session["Emp_Name"].ToString(); ;
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            string customHTML = "<div width=\"100%\" style=\"text-align:center;font-size:8px;font-family:Tahoma;\">" +
                                    " <table width='100%'border='0'>" +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Company Name :</b>" + " " + CompanyName + "</td></tr> " +
                                        //  "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Template Name:</b>" + " " + TemplateName + "</td></tr> " +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Template Name:</b>" + " " + Session["TemplateName"].ToString() + "</td></tr> " +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Report Name:</b>" + " " + ReportName + "</td></tr> " +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Date:</b>" + " " + DateTime.Now.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("en-NZ")) + "</td></tr>" +
                                        "<tr><td colspan='7'  style=\"text-align:left;font-size:12px;font-family:Tahoma;\" ><b>Created By:</b>" + " " + GenerateBy + "</td></tr></table></div>";

            sw.WriteLine(customHTML);
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }
        void Page_Unload(object sender, EventArgs e)
        {
            if (Session["DataTable"] != null)
            {
                Session["DataTable"] = null;
            }
        }
        //protected void ButtonCustomDate_Click(object sender, System.EventArgs e)
        //{
        //    bool datePeriodFlag = false;
        //    int startMonth = 0;
        //    int endMonth = 0;
        //    List<DateTime> listDates;
        //    IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        //    if (((System.Web.UI.WebControls.ImageButton)sender).ID == "imgbtnfetch")
        //    {
        //        if (RadDatePicker9.SelectedDate.HasValue)
        //        {
        //            if (RadDatePicker10.SelectedDate.HasValue)
        //            {
        //                string startDate = Convert.ToDateTime(RadDatePicker9.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
        //                string endDate = Convert.ToDateTime(RadDatePicker10.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

        //                startMonth = RadDatePicker9.SelectedDate.Value.Month;

        //                endMonth = RadDatePicker10.SelectedDate.Value.Month;
        //                DateTime startCollDate = Convert.ToDateTime(RadDatePicker9.SelectedDate.Value);
        //                DateTime endCollDate = Convert.ToDateTime(RadDatePicker10.SelectedDate.Value);
        //                listDates = new List<DateTime>();
        //                while (startCollDate < endCollDate)
        //                {
        //                    listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //                    startCollDate = startCollDate.AddMonths(1);

        //                }
        //                if (startMonth == endMonth)
        //                {
        //                    datePeriodFlag = false;
        //                }
        //                else
        //                {
        //                    datePeriodFlag = true;
        //                }


        //                if (Session["CategoryId"].ToString() != "")
        //                {
        //                    if (Session["TemplateId"].ToString() != "")
        //                    {
        //                        if (Session["CategoryName"].ToString() == "Leaves")
        //                        {
        //                            GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                        }
        //                        else if (Session["CategoryName"].ToString() == "Expiry")
        //                        {
        //                            GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                        }
        //                        else
        //                        {
        //                            GenerateCommonReportsByStartDate(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                        }

        //                    }

        //                }
        //                else
        //                {
        //                    Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //                }
        //            }
        //        }
        //    }
        //}
        protected void GenerateCommonReportsByStartDate(string startDate, string endDate, bool periodFlag, int CategoryId, int TemplateID, List<DateTime> listDates)
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
                if (Session["StringEmployee"].ToString() != "")
                {
                    grid1++;
                    strEmployee = Session["StringEmployee"].ToString();

                }


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
                            parmsAdditions[4] = new SqlParameter("@claimtype", 1);
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
                            parmsDeductions[4] = new SqlParameter("@claimtype", 1);
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
                            parmsClaims[4] = new SqlParameter("@claimtype", 1);
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
                        DataRelation drel = new DataRelation("EquiJoin", dtEmployee.Columns["EmpCode"], dtPayRollView.Columns["emp_code"]);
                        dsReportResult.Relations.Add(drel);
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
        protected void GenerateCommonLeaveReport(DateTime startDate, DateTime endDate, int CategoryId, int TemplateID, List<DateTime> listDates)
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
            //if (rdRepOption.SelectedItem.Value == "2")
            //{
            //    if (Utility.ToInteger(startDate.Month) > Utility.ToInteger(endDate.Month))
            //    {
            //        strMessage = "Start Month Should be Greater than End Month.";
            //    }
            //    if (Utility.ToInteger(startDate.Month) == 0 || Utility.ToInteger(endDate.Month) == 0)
            //    {
            //        strMessage = strMessage + "<br/>" + "For Detail Report Need to select month in Start and End Month.";
            //    }
            //}

            if (strMessage.Length <= 0)
            {
                if (Session["StringEmployee"].ToString() != "")
                {

                    sqlTrns1 = Session["StringEmployee"].ToString();

                }



                string sSQL = "sp_GetLeaveSumDet";
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@year", Utility.ToInteger(startDate.Year));
                parms[1] = new SqlParameter("@EmpID", sqlTrns1);
                parms[2] = new SqlParameter("@LeaveID", sqlTrnsTypeLeave);
                parms[3] = new SqlParameter("@ReportType", Utility.ToInteger(1));
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
                Session["rptDs"] = ds;
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
        protected void GenerateCertificateExpiry(DateTime startDate, DateTime endDate, int CategoryId, int TemplateID, List<DateTime> listDates)
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

            if (startDate != null && endDate != null)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    if (dtTable.Rows[i]["TableID"].ToString() == "9") // cross checking with dropdownlistitem to gridboundcolumn text
                    {
                        grid1++;
                        sqlTrnsTypeExpiry = sqlTrnsTypeExpiry + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
                    }
                }
            }
            if (Session["StringEmployee"].ToString() != "")
            {
                grid1++;
                strEmployee = Session["StringEmployee"].ToString();

            }

            if (endDate != null)
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
                        Session["rptDs"] = rptDs;
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
        //protected void ButtonMonthSelection_Click(object sender, System.EventArgs e)
        //{
        //    Calendar c = new Calendar();
        //    bool datePeriodFlag = false;
        //    //int mDay = c.get(Calendar.DAY_OF_MONTH);
        //    int mYear = 0;
        //    int sMonth = 0;
        //    int eMonth = 0;
        //    List<DateTime> listDates;
        //    if (((System.Web.UI.WebControls.Button)sender).ID == "btnCurrentMonth")
        //    {

        //        mYear = c.TodaysDate.Year;
        //        sMonth = c.TodaysDate.Month;
        //        eMonth = 0;
        //        datePeriodFlag = false;
        //        DateTime today = DateTime.Today;
        //        DateTime startDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endDate = startDate.AddMonths(1).AddDays(-1);
        //        DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endCollDate = startDate.AddMonths(1).AddDays(-1);
        //        listDates = new List<DateTime>();
        //        while (startCollDate < endCollDate)
        //        {
        //            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //            startCollDate = startCollDate.AddMonths(1);

        //        }
        //        //int mDay = c.get(Calendar.DAY_OF_MONTH);
        //        if (Session["CategoryId"].ToString() != "")
        //        {
        //            if (Session["TemplateId"].ToString() != "")
        //            {
        //                if (Session["CategoryName"].ToString() == "Leaves")
        //                {
        //                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else if (Session["CategoryName"].ToString() == "Expiry")
        //                {
        //                    GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else
        //                {
        //                    GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }


        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //        }

        //    }
        //    else if (((System.Web.UI.WebControls.Button)sender).ID == "btnPreviousMonth")
        //    {
        //        mYear = c.TodaysDate.Year;
        //        sMonth = c.TodaysDate.Month - 1;
        //        eMonth = 0;
        //        datePeriodFlag = false;
        //        DateTime today = DateTime.Now.AddMonths(-1);
        //        DateTime startDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endDate = startDate.AddMonths(1).AddDays(-1);
        //        DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endCollDate = startDate.AddMonths(1).AddDays(-1);
        //        listDates = new List<DateTime>();
        //        while (startCollDate < endCollDate)
        //        {
        //            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //            startCollDate = startCollDate.AddMonths(1);

        //        }
        //        //int mDay = c.get(Calendar.DAY_OF_MONTH);
        //        if (Session["CategoryId"].ToString() != "")
        //        {
        //            if (Session["TemplateId"].ToString() != "")
        //            {
        //                if (Session["CategoryName"].ToString() == "Leaves")
        //                {
        //                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else if (Session["CategoryName"].ToString() == "Expiry")
        //                {
        //                    GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else
        //                {

        //                    GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //        }
        //    }
        //    else if (((System.Web.UI.WebControls.Button)sender).ID == "btnThreeMonth")
        //    {
        //        mYear = c.TodaysDate.Year;
        //        sMonth = c.TodaysDate.Month - 2;
        //        eMonth = c.TodaysDate.Month;
        //        datePeriodFlag = true;
        //        DateTime today = DateTime.Now.AddMonths(-3);
        //        DateTime startDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endDate = startDate.AddMonths(3).AddDays(-1);
        //        DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endCollDate = startDate.AddMonths(3).AddDays(-1);
        //        listDates = new List<DateTime>();
        //        while (startCollDate < endCollDate)
        //        {
        //            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //            startCollDate = startCollDate.AddMonths(1);

        //        }
        //        //int mDay = c.get(Calendar.DAY_OF_MONTH);
        //        if (Session["CategoryId"].ToString() != "")
        //        {
        //            if (Session["TemplateId"].ToString() != "")
        //            {
        //                if (Session["CategoryName"].ToString() == "Leaves")
        //                {
        //                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else if (Session["CategoryName"].ToString() == "Expiry")
        //                {
        //                    GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else
        //                {
        //                    GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //        }
        //    }
        //    else if (((System.Web.UI.WebControls.Button)sender).ID == "btnSixMonth")
        //    {
        //        mYear = c.TodaysDate.Year;
        //        sMonth = c.TodaysDate.Month - 5;
        //        eMonth = c.TodaysDate.Month;
        //        datePeriodFlag = true;
        //        DateTime today = DateTime.Now.AddMonths(-5);
        //        DateTime startDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endDate = startDate.AddMonths(6).AddDays(-1);
        //        DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endCollDate = startDate.AddMonths(6).AddDays(-1);
        //        listDates = new List<DateTime>();
        //        while (startCollDate < endCollDate)
        //        {
        //            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //            startCollDate = startCollDate.AddMonths(1);

        //        }
        //        if (Session["CategoryId"].ToString() != "")
        //        {
        //            if (Session["TemplateId"].ToString() != "")
        //            {
        //                if (Session["CategoryName"].ToString() == "Leaves")
        //                {
        //                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else if (Session["CategoryName"].ToString() == "Expiry")
        //                {
        //                    GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else
        //                {
        //                    GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //        }
        //    }
        //    else if (((System.Web.UI.WebControls.Button)sender).ID == "btnOneYear")
        //    {
        //        mYear = c.TodaysDate.Year;
        //        sMonth = c.TodaysDate.Month - 11;
        //        eMonth = c.TodaysDate.Month;
        //        datePeriodFlag = true;
        //        DateTime today = DateTime.Now.AddMonths(-12);
        //        DateTime startDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endDate = startDate.AddMonths(12).AddDays(-1);
        //        DateTime startCollDate = new DateTime(today.Year, today.Month, 1);
        //        DateTime endCollDate = startDate.AddMonths(12).AddDays(-1);
        //        listDates = new List<DateTime>();
        //        while (startCollDate < endCollDate)
        //        {
        //            listDates.Add(Convert.ToDateTime(startCollDate.ToString("dd/MM/yyyy")));
        //            startCollDate = startCollDate.AddMonths(1);

        //        }
        //        if (Session["CategoryId"].ToString() != "")
        //        {
        //            if (Session["TemplateId"].ToString() != "")
        //            {
        //                if (Session["CategoryName"].ToString() == "Leaves")
        //                {
        //                    GenerateCommonLeaveReport(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else if (Session["CategoryName"].ToString() == "Expiry")
        //                {
        //                    GenerateCertificateExpiry(startCollDate, endCollDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //                else
        //                {
        //                    GenerateCommonReports(startDate, endDate, datePeriodFlag, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()), listDates);
        //                }
        //            }
        //            else
        //            {
        //                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //            }
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please select any one template');", true);
        //        }
        //    }

        //}

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

                if (Session["StringEmployee"].ToString() != "")
                {
                    grid1++;
                    strEmployee = Session["StringEmployee"].ToString();

                }
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
                            string sAdditionSQL = "Sp_getpivotclaimsadditionscommon";
                            SqlParameter[] parmsAdditions = new SqlParameter[8];
                            parmsAdditions[0] = new SqlParameter("@empcode", strEmployee);
                            parmsAdditions[1] = new SqlParameter("@trxtype", sqlTrnsTypeAddition);
                            parmsAdditions[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsAdditions[3] = new SqlParameter("@enddate", currentenddate);
                            parmsAdditions[4] = new SqlParameter("@claimtype", 1);
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
                            parmsDeductions[4] = new SqlParameter("@claimtype", 1);
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
                            parmsClaims[1] = new SqlParameter("@trxtype", sqlTrnsTypeClaims);
                            parmsClaims[2] = new SqlParameter("@startdate", currentstartdate);
                            parmsClaims[3] = new SqlParameter("@enddate", currentenddate);
                            parmsClaims[4] = new SqlParameter("@claimtype", 1);
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
                        DataRelation drel = new DataRelation("EquiJoin", dtEmployee.Columns["EmpCode"], dtPayRollView.Columns["emp_code"]);
                        dsReportResult.Relations.Add(drel);
                        //DataRelation drelAdditions = new DataRelation("AddJoin", dtEmployee.Columns["emp_code"], dtAdditions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelAdditions);
                        //DataRelation drelDeductions = new DataRelation("DedJoin", dtEmployee.Columns["emp_code"], dtDeductions.Columns["emp_code"]);
                        //dsReportResult.Relations.Add(drelDeductions);
                        DataTable jt = new DataTable();
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
                if (sonMasterTable.Rows.Count > 0)
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
                parmsAdditions[4] = new SqlParameter("@claimtype", 1);
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
                parmsDeductions[4] = new SqlParameter("@claimtype", 1);
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
                parmsClaims[4] = new SqlParameter("@claimtype", 1);
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
        protected void Page_Load(object sender, System.EventArgs e)
        {
           
            compid = Utility.ToInteger(Session["Compid"]);
          //  lblReportName.Text = "Common Reports";
            
            //empResults.ItemDataBound+=new GridItemEventHandler(empResults_ItemDataBound);
            this.Page.Unload += new EventHandler(Page_Unload);
            sqlRptDs = (DataSet)Session["rptDs"];

            string startMonth = "";
            string endMonth = "";
            if (Request.QueryString["PageType"] != null)
            {
                strPage = Request.QueryString["PageType"].ToString();

                if (Request.QueryString.Count > 1)
                {
                    startMonth = Request.QueryString["SM"].ToString();
                    endMonth = Request.QueryString["EM"].ToString();
                }

            }
            lblTitleReportName.Text = Session["TemplateName"].ToString();
            if (strPage != "71" && strPage != "72" && strPage != "73")
            {
                lblReportPeriod.Text = Session["StartPeriod"].ToString() + " to " + Session["EndPeriod"].ToString();
            }

            if (!Page.IsPostBack)
            {
                if (Session["CategoryName"].ToString() == "Employee" || Session["CategoryName"].ToString() == "Grouping" || Session["CategoryName"].ToString() == "Certificate")
                {
                    reportlbl.Visible = false;
                }
               


                if (k == 1)
                {
                    empResults.ItemDataBound += new GridItemEventHandler(empResults_ItemDataBound);
                    k++;
                }
                if (strPage == "10")
                {
                    DataSet tempDS_1 = new DataSet();
                    tempDS_1 = sqlRptDs;

                    DataSet tempDs = new DataSet();

                    tempDs.Tables.Add("Desc");
                    tempDs.Tables[0].Columns.Add("CostCenter");

                    if (startMonth == "January" || endMonth == "January")
                    {
                        tempDs.Tables[0].Columns.Add("JAN");
                        tempDs.Tables[0].Columns.Add("JANV");
                    }
                    if (startMonth == "February" || endMonth == "February")
                    {

                        tempDs.Tables[0].Columns.Add("FEB");
                        tempDs.Tables[0].Columns.Add("FEBV");
                    }
                    if (startMonth == "March" || endMonth == "March")
                    {

                        tempDs.Tables[0].Columns.Add("MAR");
                        tempDs.Tables[0].Columns.Add("MARV");
                    }

                    if (startMonth == "April" || endMonth == "April")
                    {

                        tempDs.Tables[0].Columns.Add("APR");
                        tempDs.Tables[0].Columns.Add("APRV");
                    }

                    if (startMonth == "May" || endMonth == "May")
                    {

                        tempDs.Tables[0].Columns.Add("MAY");
                        tempDs.Tables[0].Columns.Add("MAYV");
                    }

                    if (startMonth == "June" || endMonth == "June")
                    {

                        tempDs.Tables[0].Columns.Add("JUN");
                        tempDs.Tables[0].Columns.Add("JUNV");
                    }

                    if (startMonth == "July" || endMonth == "July")
                    {

                        tempDs.Tables[0].Columns.Add("JUL");
                        tempDs.Tables[0].Columns.Add("JULV");
                    }

                    if (startMonth == "August" || endMonth == "August")
                    {

                        tempDs.Tables[0].Columns.Add("AUG");
                        tempDs.Tables[0].Columns.Add("AUGV");
                    }

                    if (startMonth == "September" || endMonth == "September")
                    {

                        tempDs.Tables[0].Columns.Add("SEP");
                        tempDs.Tables[0].Columns.Add("SEPV");
                    }

                    if (startMonth == "October" || endMonth == "October")
                    {
                        tempDs.Tables[0].Columns.Add("OCT");
                        tempDs.Tables[0].Columns.Add("OCTV");
                    }

                    if (startMonth == "November" || endMonth == "November")
                    {

                        tempDs.Tables[0].Columns.Add("NOV");
                        tempDs.Tables[0].Columns.Add("NOVV");
                    }

                    if (startMonth == "December" || endMonth == "December")
                    {

                        tempDs.Tables[0].Columns.Add("DEC");
                        tempDs.Tables[0].Columns.Add("DECV");
                    }


                    tempDs.Tables[0].Columns.Add("Status");

                    DataRow[] dr = null;
                    DataRow[] dr_end = null;

                    int month = 0;
                    int intendMonth = 0;

                    if (startMonth == "January")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=1");
                        month = 1;
                    }

                    if (startMonth == "February")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=2");
                        month = 2;
                    }

                    if (startMonth == "March")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=3");
                        month = 3;
                    }

                    if (startMonth == "April")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=4");
                        month = 4;
                    }

                    if (startMonth == "May")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=5");
                        month = 5;
                    }

                    if (startMonth == "June")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=6");
                        month = 6;
                    }

                    if (startMonth == "July")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=7");
                        month = 7;
                    }

                    if (startMonth == "August")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=8");
                        month = 8;
                    }

                    if (startMonth == "September")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=9");
                        month = 9;
                    }

                    if (startMonth == "October")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=10");
                        month = 10;
                    }

                    if (startMonth == "November")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=11");
                        month = 11;
                    }

                    if (startMonth == "December")
                    {
                        dr = tempDS_1.Tables[0].Select("Month=12");
                        month = 12;
                    }

                    //End Month 



                    if (endMonth == "January")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=1");
                        intendMonth = 1;
                    }

                    if (endMonth == "February")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=2");
                        intendMonth = 2;
                    }

                    if (endMonth == "March")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=3");
                        intendMonth = 3;
                    }

                    if (endMonth == "April")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=4");
                        intendMonth = 4;
                    }

                    if (endMonth == "May")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=5");
                        intendMonth = 5;
                    }

                    if (endMonth == "June")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=6");
                        intendMonth = 6;
                    }

                    if (endMonth == "July")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=7");
                        intendMonth = 7;
                    }

                    if (endMonth == "August")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=8");
                        intendMonth = 8;
                    }

                    if (endMonth == "September")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=9");
                        intendMonth = 9;
                    }

                    if (endMonth == "October")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=10");
                        intendMonth = 10;
                    }

                    if (endMonth == "November")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=11");
                        intendMonth = 11;
                    }

                    if (endMonth == "December")
                    {
                        dr_end = tempDS_1.Tables[0].Select("Month=12");
                        intendMonth = 12;
                    }

                    for (int i = 0; i <= dr.Length - 1; i++)
                    {
                        DataRow dr1 = tempDs.Tables[0].NewRow();

                        if (dr[i][2].ToString() == month.ToString())
                        {
                            dr1["CostCenter"] = dr[i]["Description"].ToString();


                            if (startMonth == "January" || endMonth == "January")
                            {
                                dr1["JAN"] = 0;
                                dr1["JANV"] = 0;
                            }

                            if (startMonth == "February" || endMonth == "February")
                            {
                                dr1["FEB"] = 0;
                                dr1["FEBV"] = 0;
                            }
                            if (startMonth == "March" || endMonth == "March")
                            {
                                dr1["MAR"] = 0;
                                dr1["MARV"] = 0;
                            }
                            if (startMonth == "April" || endMonth == "April")
                            {
                                dr1["APR"] = 0;
                                dr1["APRV"] = 0;
                            }
                            if (startMonth == "May" || endMonth == "May")
                            {
                                dr1["MAY"] = 0;
                                dr1["MAYV"] = 0;
                            }
                            if (startMonth == "June" || endMonth == "June")
                            {
                                dr1["JUN"] = 0;
                                dr1["JUNV"] = 0;
                            }

                            if (startMonth == "July" || endMonth == "July")
                            {
                                dr1["JUL"] = 0;
                                dr1["JULV"] = 0;
                            }
                            if (startMonth == "August" || endMonth == "August")
                            {
                                dr1["AUG"] = 0;
                                dr1["AUGV"] = 0;
                            }
                            if (startMonth == "September" || endMonth == "September")
                            {
                                dr1["SEP"] = 0;
                                dr1["SEPV"] = 0;
                            }
                            if (startMonth == "October" || endMonth == "October")
                            {
                                dr1["OCT"] = 0;
                                dr1["OCTV"] = 0;
                            }
                            if (startMonth == "November" || endMonth == "November")
                            {
                                dr1["NOV"] = 0;
                                dr1["NOVV"] = 0;
                            }

                            if (startMonth == "December" || endMonth == "December")
                            {
                                dr1["DEC"] = 0;
                                dr1["DECV"] = 0;
                            }

                        }
                        tempDs.Tables[0].Rows.Add(dr1);
                    }

                    //End month 
                    dr = dr_end;

                    ArrayList arryList1 = new ArrayList();

                    for (int cnt = 0; cnt <= dr.Length - 1; cnt++)
                    {
                        arryList1.Add(dr[cnt]["Description"].ToString());
                    }

                    //Remove From Array list Which are already there ...
                    for (int j = 0; j <= tempDs.Tables[0].Rows.Count - 1; j++)
                    {
                        string desc = tempDs.Tables[0].Rows[j]["CostCenter"].ToString();

                        for (int i = 0; i <= arryList1.Count - 1; i++)
                        {
                            if (arryList1[i].ToString() == desc)
                            {
                                arryList1.RemoveAt(i);
                            }
                        }
                    }

                    //Add new items to dataset 
                    for (int cnt1 = 0; cnt1 <= arryList1.Count - 1; cnt1++)
                    {
                        DataRow dr1 = tempDs.Tables[0].NewRow();
                        dr1["CostCenter"] = arryList1[cnt1].ToString();

                        if (startMonth == "January" || endMonth == "January")
                        {
                            dr1["JAN"] = 0;
                            dr1["JANV"] = 0;
                        }

                        if (startMonth == "February" || endMonth == "February")
                        {
                            dr1["FEB"] = 0;
                            dr1["FEBV"] = 0;
                        }
                        if (startMonth == "March" || endMonth == "March")
                        {
                            dr1["MAR"] = 0;
                            dr1["MARV"] = 0;
                        }
                        if (startMonth == "April" || endMonth == "April")
                        {
                            dr1["APR"] = 0;
                            dr1["APRV"] = 0;
                        }
                        if (startMonth == "May" || endMonth == "May")
                        {
                            dr1["MAY"] = 0;
                            dr1["MAYV"] = 0;
                        }
                        if (startMonth == "June" || endMonth == "June")
                        {
                            dr1["JUN"] = 0;
                            dr1["JUNV"] = 0;
                        }

                        if (startMonth == "July" || endMonth == "July")
                        {
                            dr1["JUL"] = 0;
                            dr1["JULV"] = 0;
                        }
                        if (startMonth == "August" || endMonth == "August")
                        {
                            dr1["AUG"] = 0;
                            dr1["AUGV"] = 0;
                        }
                        if (startMonth == "September" || endMonth == "September")
                        {
                            dr1["SEP"] = 0;
                            dr1["SEPV"] = 0;
                        }
                        if (startMonth == "October" || endMonth == "October")
                        {
                            dr1["OCT"] = 0;
                            dr1["OCTV"] = 0;
                        }
                        if (startMonth == "November" || endMonth == "November")
                        {
                            dr1["NOV"] = 0;
                            dr1["NOVV"] = 0;
                        }

                        if (startMonth == "December" || endMonth == "December")
                        {
                            dr1["DEC"] = 0;
                            dr1["DECV"] = 0;
                        }
                        tempDs.Tables[0].Rows.Add(dr1);
                    }


                    //if (dr[i]["Description"].ToString() != desc)
                    //{
                    DataRow[] drSM = tempDS_1.Tables[0].Select("Month=" + month);
                    DataRow[] drEM = tempDS_1.Tables[0].Select("Month=" + intendMonth);

                    //For Start Month
                    for (int h = 0; h <= drSM.Length - 1; h++)
                    {
                        string desc = drSM[h]["Description"].ToString();

                        for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                        {
                            if (desc == tempDs.Tables[0].Rows[cnt1]["CostCenter"].ToString())
                            {
                                DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                                dr1[1] = drSM[h]["AMT"].ToString();
                                tempDs.AcceptChanges();
                            }
                        }

                    }


                    for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                    {
                        DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                        dr1[3] = 0;
                        tempDs.AcceptChanges();

                    }

                    //For End Month
                    for (int h = 0; h <= drEM.Length - 1; h++)
                    {
                        string desc = drEM[h]["Description"].ToString();

                        for (int cnt1 = 0; cnt1 <= tempDs.Tables[0].Rows.Count - 1; cnt1++)
                        {
                            if (desc == tempDs.Tables[0].Rows[cnt1]["CostCenter"].ToString())
                            {
                                DataRow dr1 = tempDs.Tables[0].Rows[cnt1];
                                dr1[3] = drEM[h]["AMT"].ToString();
                                tempDs.AcceptChanges();
                            }

                        }

                    }
                    DataTable dtnew = new DataTable();

                    if (tempDs.Tables[0].Rows.Count > 0)
                    {
                        if (tempDs.Tables[0].Columns.Count >= 6)
                        {

                            tempDs.Tables[0].Columns.RemoveAt(2);

                            dtnew = tempDs.Tables[0];
                            dtnew.DefaultView.Sort = "CostCenter Asc";
                            dtnew.Columns[3].ColumnName = "Difference";
                            Session["DataTable"] = dtnew;
                            empResults.DataSource = dtnew;// tempDS_1.Tables[0].Select("Month=1");                        
                            empResults.DataBind();
                        }
                    }
                }
                else
                {

                    empResults.DataSource = sqlRptDs;
                    empResults.DataBind();
                }


                if (strPage != "")
                {
                    if (strPage == "2" || strPage == "3" || strPage == "4" || strPage == "5" || strPage == "6" || strPage == "22" || strPage == "10" || strPage == "20")
                    {
                        for (int i = 0; i < empResults.MasterTableView.AutoGeneratedColumns.Length; i++)
                        {
                            GridBoundColumn boundColumn = (empResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);
                            //boundColumn.DataFormatString = "<nobr>{0}</nobr>";

                            if (boundColumn != null)
                            {
                                if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                                {

                                    if (boundColumn.DataField.ToString() != "NH" & boundColumn.DataField.ToString() != "OT1" & boundColumn.DataField.ToString() != "OT2")//new condition for consolated Timesheet report
                                    {
                                        if (boundColumn.DataField.ToString() == "SDL")
                                        {
                                            boundColumn.DataType = Type.GetType("System.Double");
                                            boundColumn.Aggregate = GridAggregateFunction.Sum;
                                            boundColumn.DataFormatString = "{0:N5}";
                                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                            boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                                        }
                                        else
                                        {
                                            boundColumn.DataType = Type.GetType("System.Double");
                                            boundColumn.Aggregate = GridAggregateFunction.Sum;
                                            boundColumn.DataFormatString = "{0:N2}";
                                            boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                            boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;
                                        }
                                    }
                                }
                            }
                            if (strPage == "22")
                            {


                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < empResults.MasterTableView.AutoGeneratedColumns.Length; i++)
                        {
                            GridBoundColumn boundColumn = (empResults.MasterTableView.AutoGeneratedColumns[i] as GridBoundColumn);


                            if (boundColumn != null)
                            {
                                if (boundColumn.DataType.Name == "Double" || boundColumn.DataType.Name == "Decimal")
                                {

                                    boundColumn.DataType = Type.GetType("System.Double");
                                    boundColumn.Aggregate = GridAggregateFunction.Sum;
                                    boundColumn.DataFormatString = "{0:N2}";
                                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                                    boundColumn.FooterStyle.HorizontalAlign = HorizontalAlign.Right;

                                }

                            }

                        }

                    }

                }
                //empResults.Columns[0].Visible = false;
                //empResults.Columns[1].Visible = false;
                //empResults.Columns[2].Visible = false;
                //empResults.Columns[3].Visible = false;
                //empResults.Columns[4].Visible = false;

                //empResults.Columns[5].Visible = false;
                empResults.MasterTableView.Rebind();

          }                         
       }
    }  
}
