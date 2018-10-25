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

namespace SMEPayroll.Reports
{
    public partial class CustomConMainPage : System.Web.UI.Page
    {
        static int s = 0;
        string compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string varEmpCode = "";
        string strRepType;
        string sgroupname = "";
        protected int comp_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            comp_id = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            varEmpCode = Session["EmpCode"].ToString();

            lblerror.Text = "";
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource333.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            compid = Utility.ToString(Utility.ToInteger(Session["Compid"].ToString()));
            if (!Page.IsPostBack)
            {
                FillCompany();

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Employee")) == false)
                {
                    tbsComp.Tabs[tbsEmp.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Payroll")) == false)
                {
                    tbsComp.Tabs[tbsPay.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Additions")) == false)
                {
                    tbsComp.Tabs[tbsAdditions.Index].Visible = false;
                }
                else
                {
                    tbsAdditions.Selected = true;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Deductions")) == false)
                {
                    tbsComp.Tabs[tbsDeductions.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Claims")) == false)
                {
                    tbsComp.Tabs[tbsClaims.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Grouping")) == false)
                {
                    tbsComp.Tabs[tbsGroups.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Leaves")) == false)
                {
                    tbsComp.Tabs[tbsLeaves.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Consolidate Reports - Timesheet")) == false)
                {
                    tbsComp.Tabs[tbsTimesheet.Index].Visible = false;
                }
                if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - EmailTrack1")) == false)
                {
                    tbsComp.Tabs[tbsEmail.Index].Visible = false;
                }

                lblStart.Text = "Month:";

                string sqlStr = "";
                sqlStr = "SELECT ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=1 Order By Alias_Name";
                RadGrid2.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid2.DataBind();

                sqlStr = "SELECT ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 Order By Alias_Name";
                RadGrid4.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid4.DataBind();

                sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE Company_ID != 1000 And (OPTIONSELECTION in ('General','Variable'))  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
                RadGrid6.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid6.DataBind();

                sqlStr = "Select * From (select Alias_Name,Case when len(Group_Source) = 0 then Alias_Name else Group_Source end as Group_Source  from dbo.TABLEOBJATTRIB  where GroupColumn=1) as D Where (group_source != 'Agent name')";
                ddlCategory.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                ddlCategory.DataTextField = "Alias_Name";
                ddlCategory.DataValueField = "Group_Source";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, "Select");

                sqlStr = "select id, description from dbo.ViewDeductions Where id in (select distinct trx_type from emp_deductions where status='L') ";
                RadGrid10.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid10.DataBind();

                sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE Company_ID != 1000 And (OPTIONSELECTION in ('Claim'))  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
                RadGrid12.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid12.DataBind();

                sqlStr = "Select p.rowid, (P.MonthName+ ' - ' + cast(p.[Year] as varchar)) AliasMonth From Payrollmonthlydetail p inner join company c on p.paytype=c.payrolltype where company_id=" + compid + " and p.rowid in (select Distinct MonthYear from emailtrack) order by p.monthname";
                RadGrid15.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid15.DataBind();


                //dlDept.Items.Add(new ListItem("- Select -", "-1"));
                //dlDept.Items.Add(new ListItem("-All-", "-1"));

                //ddlPayDept.Items.Insert(0, "- Select -");
                //ddlPayDept.Items.Insert(1, "-All-");

                //dlAdditions.Items.Insert(0, "- Select -");
                //dlAdditions.Items.Insert(1, "-All-");

                //dlDeptDeductions.Items.Insert(0, "- Select -");
                //dlDeptDeductions.Items.Insert(1, "-All-");

                //dlClaimsDept.Items.Insert(0, "- Select -");
                //dlClaimsDept.Items.Insert(1, "-All-");

                //dlLeavesDept.Items.Insert(0, "- Select -");
                //dlLeavesDept.Items.Insert(1, "-All-");

                //dlEmailDept.Items.Insert(0, "- Select -");
                //dlEmailDept.Items.Insert(1, "-All-");

                drpYear.DataSourceID = "xmldtYear";
                drpYear.DataTextField = "text";
                drpYear.DataValueField = "id";
                drpYear.DataBind();
                drpYear.Items.FindByText(System.DateTime.Today.Year.ToString()).Selected = true;

            }
            else
            {



            }
        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            RadGrid1.DataBind();
        }

        protected void GenerateRptEmail_Click(object sender, EventArgs e)
        {
            string sqlTrns = "0";
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "";
            int grid1 = 0;
            int grid2 = 0;
            lblerror.Text = "";
            foreach (GridItem item in RadGrid14.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid15.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        // sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        sqlTrns = sqlTrns + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (grid1 > 0)
            {
                if (grid2 > 0)
                {
                    //sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                    //sqlSelect = sqlSelect + " from Employee e1  where e1.emp_code in (" + strEmployee + ")  ORDER BY EMP_NAME";

                    if (txtDocNo.Text.ToString().Trim().Length > 0)
                    {
                        sqlSelect = "Select Docid,E.Emp_Code,E.time_card_no, isnull(E.emp_name,'')+' '+isnull(E.emp_lname,'') SentTo , EventView = Case When EventView=1 Then 'Email Payslip' Else '' End, Senton, isnull(Em.emp_name,'')+' '+isnull(Em.emp_lname,'') SentBy, [T.Status] = Case When T.Status=0 Then 'Success' Else 'Fail' End, T.Remarks From emailtrack T Inner Join Employee E On T.Sentto = E.Emp_Code Inner Join Employee Em On T.SentBy = Em.Emp_Code Where E.Emp_Code In (" + strEmployee + ") And T.MonthYear in (" + sqlTrns + ") And EventView = '" + rdOptionEmail.SelectedValue + "' and cast(t.docid as varchar) like '%" + txtDocNo.Text.ToString().Trim() +"%'";
                    }
                    else
                    {
                        sqlSelect = "Select Docid,E.Emp_Code,E.time_card_no, isnull(E.emp_name,'')+' '+isnull(E.emp_lname,'') SentTo , EventView = Case When EventView=1 Then 'Email Payslip' Else '' End, Senton, isnull(Em.emp_name,'')+' '+isnull(Em.emp_lname,'') SentBy, [T.Status] = Case When T.Status=0 Then 'Success' Else 'Fail' End, T.Remarks From emailtrack T Inner Join Employee E On T.Sentto = E.Emp_Code Inner Join Employee Em On T.SentBy = Em.Emp_Code Where E.Emp_Code In (" + strEmployee + ") And T.MonthYear in (" + sqlTrns + ") And EventView = '" + rdOptionEmail.SelectedValue + "'";
                    }
                    DataSet rptDs;
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/CustomReportNew.aspx");
                }
                else
                {
                    lblerror.Text = "Please Select Atleast One Field Name";

                }
            }
            else
            {
                lblerror.Text = "Please Select Atleast One Employee ";
            }

        }

        protected void GenerateRpt_Click(object sender, EventArgs e)
        {
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select Company_ID, emp_code Emp_Code, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name , ";
            int grid1 = 0;
            int grid2 = 0;
            lblerror.Text = "";
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
            foreach (GridItem item in RadGrid2.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        if (dataItem.Cells[4].Text.ToString().Trim() != "emp_code" && dataItem.Cells[4].Text.ToString().Trim() != "emp_name")
                        {
                            if (dataItem.Cells[4].Text.ToString().Trim() == "payment_mode")
                            {
                                sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                            }
                            else
                            {
                                sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + " AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                            }
                        }
                    }
                }
            }

            if (grid1 > 0)
            {
                if (grid2 > 0)
                {
                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                    sqlSelect = "Select c1.Company_Name,e1.* From (" + sqlSelect + " from Employee e1) e1  Inner Join Company c1 On e1.Company_ID = c1.Company_ID Where c1.Company_ID in (" + strEmployee + ")  ORDER BY Company_Name, Full_Name";
                    DataSet rptDs;
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/CustomReportNew.aspx");
                }
                else
                {
                    lblerror.Text = "Please Select Atleast One Field Name";
                }
            }
            else
            {
                lblerror.Text = "Please Select Atleast One Comapny ";
            }

        }


        protected void GeneratePayroll_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "SELECT Company_Name, isnull(emp_name,'')+' '+isnull(emp_lname,'') as [Full_Name],convert(nvarchar(10),start_period,103) As [Start_Period],convert(nvarchar(10),end_period,103) As [End_Period], ";
            string sqlGroup = "GROUP BY ";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGrid3.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid4.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        if (dataItem.Cells[4].Text.ToString().Trim() == "basic_pay" || dataItem.Cells[4].Text.ToString().Trim() == "NetPay")
                        {
                            sqlSelect = sqlSelect + "  Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey')," + dataItem.Cells[4].Text.ToString().Trim() + "))) As [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                            sqlGroup = sqlGroup + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        }
                        else
                        {
                            sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + " As [" + dataItem.Cells[3].Text.ToString().Trim() + "] ,";
                            sqlGroup = sqlGroup + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        }
                    }
                }
            }

            if (dtp1.SelectedDate.HasValue)
            {
                if (dtp2.SelectedDate.HasValue)
                {
                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                    sqlGroup = sqlGroup + "Company_Name,EMP_NAME,EMP_lname,start_period,end_period ";
                    string startdate = Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    //string[] dateSplit = startdate.Split('/');
                    //startdate = dateSplit[0] + "/1/" + dateSplit[2];
                    //dateSplit = enddate.Split('/');
                    //if (dateSplit[0] == "2")
                    //{
                    //    dateSplit[1] = "28";
                    //}
                    //else if (dateSplit[0] == "4" || dateSplit[0] == "6" || dateSplit[0] == "9" || dateSplit[0] == "11")
                    //{
                    //    dateSplit[1] = "30";
                    //}
                    //else
                    //{
                    //    dateSplit[1] = "31";
                    //}
                    //enddate = dateSplit[0] + "/" + dateSplit[1] + "/" + dateSplit[2];

                    sqlSelect = sqlSelect + " from PayRollView  where Company_ID in (" + strEmployee + ")";
                    sqlSelect = sqlSelect + " and Convert(Datetime,start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                    sqlSelect = sqlSelect + " and Convert(Datetime,end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                    sqlSelect = sqlSelect + " And STATUS in ('G') ";
                    sqlSelect = sqlSelect + " " + sqlGroup + "  ORDER BY Company_Name, EMP_NAME,start_period";
                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs;
                            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                            rptDs.DataSetName = "tblPayroll";
                            Session["rptDs"] = rptDs;
                            if (RadioButtonList1.SelectedItem.Value.ToString() == "2")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx");
                            }
                            else
                            {
                                Session.Add("SesSql", sqlSelect);
                                Response.Redirect("../Reports/CustomReportPayroll.aspx");
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please Select Atleast One Field Name";

                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select Atleast One Comapany ";
                    }
                }

                else
                {
                    lblerror.Text = "Please Select End Month";
                }
            }
            else
            {
                lblerror.Text = "Please Select Start Month";
            }
        }


        protected void GenerateAddtions_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGrid5.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid6.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        // sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        sqlTrns = sqlTrns + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (sqlTrns.Length > 1)
            {
                Session["SesSql"] = sqlTrns;
            }
            if (RadDatePicker1.SelectedDate.HasValue)
            {
                if (RadDatePicker2.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(RadDatePicker1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(RadDatePicker2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    //string[] dateSplit = startdate.Split('/');
                    //startdate = dateSplit[0] + "/1/" + dateSplit[2];
                    //dateSplit = enddate.Split('/');
                    //if (dateSplit[0] == "2")
                    //{
                    //    dateSplit[1] = "28";
                    //}
                    //else if (dateSplit[0] == "4" || dateSplit[0] == "6" || dateSplit[0] == "9" || dateSplit[0] == "11")
                    //{
                    //    dateSplit[1] = "30";
                    //}
                    //else
                    //{
                    //    dateSplit[1] = "31";
                    //}
                    //enddate = dateSplit[0] + "/" + dateSplit[1] + "/" + dateSplit[2];

                    string strlock = "";
                    if (RadioButtonList2.SelectedItem.Value == "2")
                    {
                        strlock = "L";
                    }
                    else if (RadioButtonList2.SelectedItem.Value == "1") //SUMMARY PROCESS
                    {
                        strlock = "L";
                    }
                    else if (RadioButtonList2.SelectedItem.Value == "3")
                    {
                        strlock = "U";
                    }
                    else if (RadioButtonList2.SelectedItem.Value == "4")
                    {
                        strlock = "U";
                    }

                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                    sqlSelect = " SELECT company.company_name,isnull(emp_name,'')+' '+isnull(emp_lname,'') + ' ' +  datename(month,trx_period) Full_Name,datename(month,trx_period)AS Month,DESCRIPTION,SUM(TRX_AMOUNT)AS Amount from Emp_additions  E INNER JOIN ViewAdditionTypesDesc V ON E.TRX_TYPE=V.ID  inner join employee on e.emp_code=employee.emp_code   inner join company on employee.company_id = company.company_id ";
                    sqlSelect = sqlSelect + " where company.company_id in (" + strEmployee + ")";
                    sqlSelect = sqlSelect + " AND trx_type in (" + sqlTrns + ") and OptionSelection ='General' ";
                    sqlSelect = sqlSelect + " and Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                    sqlSelect = sqlSelect + " and Status='" + strlock + "' ";
                    sqlSelect = sqlSelect + "GROUP BY company.company_name,id, description,emp_name,trx_period,emp_lname   ORDER BY EMP_NAME";
                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            DataTable dt = Pivot(DataAccess.ExecuteReader(CommandType.Text, sqlSelect, null), "Full_Name", "description", "Amount");
                            rptDs.Tables.Add(dt);
                            Session["rptDs"] = rptDs;

                            if (RadioButtonList2.SelectedItem.Value == "2")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=L");
                            }
                            else if (RadioButtonList2.SelectedItem.Value == "1") //SUMMARY PROCESS
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=L");
                            }
                            else if (RadioButtonList2.SelectedItem.Value == "3")
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=U");
                            }
                            else if (RadioButtonList2.SelectedItem.Value == "4")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=U");
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please Select Atleast One Field Name";

                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select Atleast One Employee ";
                    }
                }

                else
                {
                    lblerror.Text = "Please Select End Month";
                }
            }
            else
            {
                lblerror.Text = "Please Select Start Month";
            }
        }

        public static DataTable Pivot(IDataReader dataValues, string keyColumn, string pivotNameColumn, string pivotValueColumn)
        {
            DataTable tmp = new DataTable();
            DataRow r;
            string LastKey = "//dummy//";
            int i, pValIndex, pNameIndex;
            string s;
            bool FirstRow = true;
            pValIndex = dataValues.GetOrdinal(pivotValueColumn);
            pNameIndex = dataValues.GetOrdinal(pivotNameColumn);
            for (i = 0; i <= dataValues.FieldCount - 1; i++)
                if (i != pValIndex && i != pNameIndex)
                    tmp.Columns.Add(dataValues.GetName(i), dataValues.GetFieldType(i));
            r = tmp.NewRow();
            while (dataValues.Read())
            {
                if (dataValues[keyColumn].ToString() != LastKey)
                {
                    if (!FirstRow)
                        tmp.Rows.Add(r);
                    r = tmp.NewRow();
                    FirstRow = false;
                    for (i = 0; i <= dataValues.FieldCount - 3; i++)
                        r[i] = dataValues[tmp.Columns[i].ColumnName];
                    LastKey = dataValues[keyColumn].ToString();
                }

                s = dataValues[pNameIndex].ToString();
                if (!tmp.Columns.Contains(s))
                    tmp.Columns.Add(s, dataValues.GetFieldType(pValIndex));
                r[s] = dataValues[pValIndex];
            }
            tmp.Rows.Add(r);
            dataValues.Close();
            return tmp;
        }

        protected void GenerateDeductions_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGrid9.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid10.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        // sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        sqlTrns = sqlTrns + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (sqlTrns.Length > 1)
            {
                Session["SesSql"] = sqlTrns;
            }

            if (RadDatePicker5.SelectedDate.HasValue)
            {
                if (RadDatePicker6.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(RadDatePicker5.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(RadDatePicker6.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string strlock="";
                    if (RadioButtonList3.SelectedItem.Value == "2")
                    {
                        strlock= "L";
                    }
                    else if (RadioButtonList3.SelectedItem.Value == "1") //SUMMARY PROCESS
                    {
                        strlock = "L";
                    }
                    else if (RadioButtonList3.SelectedItem.Value == "3")
                    {
                        strlock = "U";
                    }
                    else if (RadioButtonList3.SelectedItem.Value == "4")
                    {
                        strlock = "U";
                    }

                    sqlSelect = " SELECT company.company_name, isnull(emp_name,'')+' '+isnull(emp_lname,'') + ' ' +  datename(month,trx_period) Full_Name,datename(month,trx_period)AS Month,DESCRIPTION,SUM(TRX_AMOUNT)AS Amount from emp_deductions  E INNER JOIN ViewDeductions V ON E.TRX_TYPE=V.ID  inner join employee on e.emp_code=employee.emp_code  inner join company on employee.company_id = company.company_id ";
                    sqlSelect = sqlSelect + " Where company.company_id in (" + strEmployee + ")";
                    sqlSelect = sqlSelect + " AND trx_type in (" + sqlTrns + ") ";
                    sqlSelect = sqlSelect + " And Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                    sqlSelect = sqlSelect + " and Status='" + strlock + "' ";
                    sqlSelect = sqlSelect + " GROUP BY company.company_name, id, description,emp_name,trx_period,emp_lname   ORDER BY EMP_NAME";

                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            DataTable dt = Pivot(DataAccess.ExecuteReader(CommandType.Text, sqlSelect, null), "Full_Name", "description", "Amount");
                            rptDs.Tables.Add(dt);
                            Session["rptDs"] = rptDs;
                            if (RadioButtonList3.SelectedItem.Value == "2")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=L");
                            }
                            else if (RadioButtonList3.SelectedItem.Value == "1") //SUMMARY PROCESS
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=L");
                            }
                            else if (RadioButtonList3.SelectedItem.Value == "3")
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=U");
                            }
                            else if (RadioButtonList3.SelectedItem.Value == "4")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=U");
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please Select Atleast One Field Name";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select Atleast One Employee ";
                    }

                }
                else
                {
                    lblerror.Text = "Please Select End Month";
                }
            }
            else
            {
                lblerror.Text = "Please Select Start Month";
            }
        }
        protected void GenerateClaims_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGrid11.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid12.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        // sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        sqlTrns = sqlTrns + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (sqlTrns.Length > 1)
            {
                Session["SesSql"] = sqlTrns;
            }

            if (RadDatePicker7.SelectedDate.HasValue)
            {
                if (RadDatePicker8.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(RadDatePicker7.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(RadDatePicker8.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string strlock = "";
                    if (RadioButtonList4.SelectedItem.Value == "2")
                    {
                        strlock = "L";
                    }
                    else if (RadioButtonList4.SelectedItem.Value == "1") //SUMMARY PROCESS
                    {
                        strlock = "L";
                    }
                    else if (RadioButtonList4.SelectedItem.Value == "3")
                    {
                        strlock = "U";
                    }
                    else if (RadioButtonList4.SelectedItem.Value == "4")
                    {
                        strlock = "U";
                    }
                    sqlSelect = " SELECT company.company_name, isnull(emp_name,'')+' '+isnull(emp_lname,'') + ' ' +  datename(month,trx_period) Full_Name,datename(month,trx_period)AS Month,DESCRIPTION,SUM(TRX_AMOUNT)AS Amount from Emp_additions  E INNER JOIN ViewAdditionTypesDesc V ON E.TRX_TYPE=V.ID  inner join employee on e.emp_code=employee.emp_code  inner join company on employee.company_id = company.company_id ";
                    sqlSelect = sqlSelect + " where company.company_id in (" + strEmployee + ")";
                    sqlSelect = sqlSelect + " AND trx_type in (" + sqlTrns + ") and optionselection = 'Claim' ";
                    sqlSelect = sqlSelect + " And Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                    sqlSelect = sqlSelect + " and Status='" + strlock + "' ";
                    sqlSelect = sqlSelect + "GROUP BY company.company_name, id, description,emp_name,trx_period,emp_lname   ORDER BY EMP_NAME";
                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            DataTable dt = Pivot(DataAccess.ExecuteReader(CommandType.Text, sqlSelect, null), "Full_Name", "description", "Amount");
                            rptDs.Tables.Add(dt);
                            Session["rptDs"] = rptDs;
                            if (RadioButtonList4.SelectedItem.Value == "2")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=L");
                            }
                            else if (RadioButtonList4.SelectedItem.Value == "1") //SUMMARY PROCESS
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=L");
                            }
                            else if (RadioButtonList4.SelectedItem.Value == "3")
                            {
                                Response.Redirect("../Reports/CustomReportAdditions.aspx?ID=U");
                            }
                            else if (RadioButtonList4.SelectedItem.Value == "4")
                            {
                                Response.Redirect("../Reports/CustomReportNew.aspx?ID=U");
                            }

                        }
                        else
                        {
                            lblerror.Text = "Please Select Atleast One Field Name";

                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select Atleast One Employee ";
                    }
                }
                else
                {
                    lblerror.Text = "Please Select End Month";
                }
            }
            else
            {
                lblerror.Text = "Please Select Start Month";
            }
        }



        protected void RadioButtonList2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }
        protected void ddlCategory_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlStr = "";
            switch (ddlCategory.SelectedValue.ToString())
            {

                case "Nationality":
                    sqlStr = " SELECT id as OptionId,Nationality As Category From " + ddlCategory.SelectedValue.ToString() + " Where ID In (Select Distinct Nationality_ID From Employee) Order By Nationality";
                    break;
                case "Country":
                    sqlStr = " SELECT id as OptionId,Country As Category From " + ddlCategory.SelectedValue.ToString() + " Where ID In (Select Distinct Country_ID From Employee)  Order By Country";
                    break;
                case "Sex":
                    sqlStr = "SELECT 'M' as OptionId,'Male'  As Category union  SELECT 'F' as OptionId,'Female'  As Category ";
                    break;
                case "Marital Status":
                    sqlStr = "SELECT 'S' as OptionId,'Single'  As Category union  SELECT 'M' as OptionId,'Married'  As Category union  SELECT 'D' as OptionId,'Divorce'  As Category  ";
                    break;
                case "Race":
                    sqlStr = " SELECT id as OptionId,Race As Category From " + ddlCategory.SelectedValue.ToString() + "  Where ID In (Select Distinct Race_ID From Employee)  Order By Race";
                    break;
                case "Religion":
                    sqlStr = " SELECT id as OptionId,Religion As Category From " + ddlCategory.SelectedValue.ToString() + "  Where ID In (Select Distinct Religion_ID From Employee)  Order By Religion";
                    break;
                case "Designation":
                    sqlStr = " SELECT id as OptionId,Designation As Category From " + ddlCategory.SelectedValue.ToString() + " Where ID In (Select Distinct Desig_ID From Employee)  Order By Designation";
                    break;
                case "Department":
                    sqlStr = " SELECT id as OptionId,DeptName As Category From " + ddlCategory.SelectedValue.ToString() + " Where ID In (Select Distinct Dept_ID From Employee)  Order By DeptName";
                    break;
                case "Emp_Group":
                    sqlStr = " SELECT id as OptionId,EmpGroupName As Category From " + ddlCategory.SelectedValue.ToString() + " Where ID In (Select Distinct Emp_Group_ID From Employee)  Order By EmpGroupName"; ;
                    break;
                case "Employee Type":
                    sqlStr = " SELECT DISTINCT EMP_TYPE As Category ,EMP_TYPE As OptionId FROM EMPLOYEE WHERE EMP_TYPE IS NOT NULL Order By Emp_Type";
                    break;
                case "Place of birth":
                    sqlStr = " SELECT Country as OptionId,Country As Category From Country  Where ID In (Select Distinct Country_ID From Employee)   Order By Country";
                    break;
            }

            if (sqlStr.Length > 0)
            {
                RadGrid8.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid8.DataBind();
                RadGrid8.Visible = true;
                RadGrid8.MasterTableView.Visible = true;
            }


        }
        protected void btnGenLeaveRep_Click(object sender, EventArgs e)
        {
            string strMessage = "";
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns1 = "0";
            string sqlTrns2 = "0";

            if (rdRepOption.SelectedItem.Value == "2")
            {

                if (Utility.ToInteger(drpMonthStart.SelectedItem.Value) > Utility.ToInteger(drpMonthEnd.SelectedItem.Value))
                {
                    strMessage = "Start Month Should be Greater than End Month.";
                }
                if (Utility.ToInteger(drpMonthStart.SelectedItem.Value) == 0 || Utility.ToInteger(drpMonthEnd.SelectedItem.Value) == 0)
                {
                    strMessage = strMessage + "<br/>" + "For Detail Report Need to select month in Start and End Month.";
                }
            }
            if (strMessage.Length <= 0)
            {
                foreach (GridItem item in RadGrid7.MasterTableView.Items)
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

                foreach (GridItem item in RadGrid13.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            sqlTrns2 = sqlTrns2 + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }

                string sSQL = "Sp_getleavesumdetcomp";
                SqlParameter[] parms = new SqlParameter[6];
                parms[0] = new SqlParameter("@year", Utility.ToInteger(drpYear.SelectedItem.Value));
                parms[1] = new SqlParameter("@CompID", sqlTrns1);
                parms[2] = new SqlParameter("@LeaveID", sqlTrns2);
                parms[3] = new SqlParameter("@ReportType", Utility.ToInteger(rdRepOption.SelectedItem.Value));
                parms[4] = new SqlParameter("@frommonth", Utility.ToInteger(drpMonthStart.SelectedItem.Value));
                if (rdRepOption.SelectedItem.Value == "1")
                {
                    parms[5] = new SqlParameter("@endmonth", Utility.ToInteger("-1"));
                }
                else
                {
                    parms[5] = new SqlParameter("@endmonth", Utility.ToInteger(drpMonthEnd.SelectedItem.Value));
                }
                DataSet ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                Session["rptDs"] = ds;
                Response.Redirect("../Reports/CustomReportNew.aspx");
            }
            else
            {
                if (strMessage.Length > 0)
                {
                    //Response.Write("<SCRIPT>alert('" + strMessage + "');</SCRIPT>");
                    ShowMessageBox(strMessage);
                    strMessage = "";
                }
            }

        }
        protected void rd_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (RadioButtonList1.SelectedItem.Value.ToString() == "1")
            {
                foreach (GridItem item in RadGrid4.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        
                        chkBox.Checked = true;
                    }
                }
                RadGrid4.Enabled = false;
            }
            else
            {
                RadGrid4.Enabled = true;
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
        protected void rdRepOption_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rdRepOption.SelectedItem.Value == "1")
            {
                lblStart.Text = "Month:";
                lblEnd.Text = "End:";
                lblEnd.Visible = false;
                drpMonthEnd.Visible = false;
            }
            else
            {
                lblStart.Text = "Start:";
                lblEnd.Text = "End:";
                lblEnd.Visible = true;
                drpMonthEnd.Visible = true;
            }
        }

        protected void GenerateGrouping_Click(object sender, EventArgs e)
        {
            string sqlQuery = "";
            string strCompany = "0";
            string sqlSelect = "select ";
            string sqlTrns = "'0'";

            foreach (GridItem item in RadGrid8.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        // sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                        sqlTrns = sqlTrns + ",'" + dataItem.Cells[3].Text.ToString().Trim() + "'";
                    }
                }
            }
            foreach (GridItem item in RadGrid16.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        strCompany = strCompany + ",'" + dataItem.Cells[2].Text.ToString().Trim() + "'";
                    }
                }
            }
            string sqlStr = "";
            switch (ddlCategory.SelectedValue.ToString())
            {

                case "Nationality":
                    sqlStr = " SELECT Company_Name, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Nationality  As Category From employee inner join Nationality on employee.Nationality_id = Nationality.id inner join Company on employee.Company_ID = Company.Company_ID WHERE NATIONALITY_ID IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name, nATIONALITY,emp_name,EMP_LNAME";
                    break;
                case "Country":
                    sqlStr = " SELECT Company_Name, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Country  As Category From employee inner join Country on employee.Country_id = Country.id inner join Company on employee.Company_ID = Company.Company_ID    WHERE Country_id IN(" + sqlTrns + ")  And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,Country,emp_name,EMP_LNAME";
                    break;
                case "Religion":
                    sqlStr = " SELECT Company_Name, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Religion As Category From employee inner join Religion on employee.Religion_id = Religion.id inner join Company on employee.Company_ID = Company.Company_ID   WHERE Religion_id IN(" + sqlTrns + ")  And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,Religion,emp_name,EMP_LNAME";
                    break;
                case "Race":
                    sqlStr = " SELECT Company_Name, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Race As Category  From employee inner join Race on employee.race_id = Race.id inner join Company on employee.Company_ID = Company.Company_ID   WHERE race_id IN(" + sqlTrns + ")  And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,Race,emp_name,EMP_LNAME";
                    break;
                case "Designation":
                    sqlStr = " SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Designation.Designation As Category From employee inner join Designation on employee.Desig_id = Designation.id  inner join Company on employee.Company_ID = Company.Company_ID WHERE Desig_id IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,Designation.Designation,emp_name,EMP_LNAME";
                    break;
                case "Department":
                    sqlStr = " SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,DeptName  As Category From employee inner join Department on employee.Dept_id = Department.id inner join Company on employee.Company_ID = Company.Company_ID  WHERE Dept_id IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,DeptName,emp_name,EMP_LNAME";
                    break;
                case "Sex":
                    sqlTrns = sqlTrns.Replace("0,", "");
                    sqlTrns = sqlTrns.Replace("F", "'F'");
                    sqlTrns = sqlTrns.Replace("M", "'M'");

                    sqlStr = " SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case sex when 'M' Then 'Male' else 'Female' end  As Category  From employee inner join Company on employee.Company_ID = Company.Company_ID    WHERE SEX IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,SEX,emp_name,emp_lname,EMP_LNAME";
                    break;
                case "Marital Status":
                    sqlTrns = sqlTrns.Replace("0,", "");
                    sqlTrns = sqlTrns.Replace("S", "'S'");
                    sqlTrns = sqlTrns.Replace("M", "'M'");
                    sqlTrns = sqlTrns.Replace("D", "'D'");
                    sqlStr = " SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Case Marital_Status when 'S' Then 'Single'  when 'D' then 'Divorce' else 'Married' end As Category  From employee inner join Company on employee.Company_ID = Company.Company_ID   WHERE Marital_Status IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,Marital_Status,emp_name,EMP_LNAME";
                    break;
                case "Emp_Group":
                    sqlStr = "SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,EmpgroupName As Category From employee inner join Emp_Group E on emp_group_id = e.id inner join Company on employee.Company_ID = Company.Company_ID  WHERE emp_group_id IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,EmpgroupName,emp_name,EMP_LNAME";
                    break;
                case "Employee Type":
                    sqlStr = "SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Emp_Type As Category  From employee  inner join Company on employee.Company_ID = Company.Company_ID   WHERE emp_tYPE IN(" + sqlTrns + ") And Company.Company_ID In (" + strCompany + ")  GROUP BY Company_Name,emp_name,EMP_LNAME,Emp_Type ";
                    break;
                case "Place of birth":
                    sqlStr = " SELECT Company_Name,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,Place_of_birth As Category From employee inner join Company on employee.Company_ID = Company.Company_ID   WHERE Place_of_birth IN(" + sqlTrns + ")  And Company.Company_ID In (" + strCompany + ") GROUP BY Company_Name,emp_name,EMP_LNAME,Place_of_birth ";
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
                Response.Redirect("../Reports/CustomReportNew.aspx");
            }
        }

        void FillCompany()
        {
            string sqlSelect = "";
            sqlSelect = "SELECT  Company_ID, Company_Name From Company ORDER BY Company_Name";

            DataSet ds = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                RadGrid1.DataSource = ds.Tables[0];
                RadGrid1.DataBind();
                RadGrid1.Visible = true;
                RadGrid1.MasterTableView.Visible = true;

                RadGrid3.DataSource = ds.Tables[0];
                RadGrid3.DataBind();
                RadGrid3.Visible = true;
                RadGrid3.MasterTableView.Visible = true;

                RadGrid5.DataSource = ds.Tables[0];
                RadGrid5.DataBind();
                RadGrid5.Visible = true;
                RadGrid5.MasterTableView.Visible = true;

                RadGrid7.DataSource = ds.Tables[0];
                RadGrid7.DataBind();
                RadGrid7.Visible = true;
                RadGrid7.MasterTableView.Visible = true;

                RadGrid9.DataSource = ds.Tables[0];
                RadGrid9.DataBind();
                RadGrid9.Visible = true;
                RadGrid9.MasterTableView.Visible = true;

                RadGrid11.DataSource = ds.Tables[0];
                RadGrid11.DataBind();
                RadGrid11.Visible = true;
                RadGrid11.MasterTableView.Visible = true;

                RadGrid16.DataSource = ds.Tables[0];
                RadGrid16.DataBind();
                RadGrid16.Visible = true;
                RadGrid16.MasterTableView.Visible = true;

                RadGrid17.DataSource = ds.Tables[0];
                RadGrid17.DataBind();
                RadGrid17.Visible = true;
                RadGrid17.MasterTableView.Visible = true;
            }
            else
            {
                RadGrid1.Visible = false;
                RadGrid1.MasterTableView.Visible = false;

                RadGrid3.Visible = false;
                RadGrid3.MasterTableView.Visible = false;

                RadGrid5.Visible = false;
                RadGrid5.MasterTableView.Visible = false;

                RadGrid9.Visible = false;
                RadGrid9.MasterTableView.Visible = false;

                RadGrid11.Visible = false;
                RadGrid11.MasterTableView.Visible = false;

                RadGrid16.Visible = false;
                RadGrid16.MasterTableView.Visible = false;

                RadGrid17.Visible = false;
                RadGrid17.MasterTableView.Visible = false;
            }

        }

        //protected void dlDept_selectedIndexChanged(object sender, EventArgs e)
        //{
        //    string sqlSelect;
        //    DataSet empDs;
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
        //    {
        //        if (dlDept.SelectedValue != "-2")
        //        {
        //            if (dlDept.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid1.DataSource = empDs.Tables[0];
        //                RadGrid1.DataBind();
        //                if (RadGrid1.Visible == false)
        //                {
        //                    RadGrid1.Visible = true;
        //                    RadGrid1.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid1.Visible = false;
        //            RadGrid1.MasterTableView.Visible = false;
        //        }

        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayDept")
        //    {
        //        if (ddlPayDept.SelectedValue != "-2")
        //        {
        //            if (ddlPayDept.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + ddlPayDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid3.DataSource = empDs.Tables[0];
        //                RadGrid3.DataBind();
        //                if (RadGrid3.Visible == false)
        //                {
        //                    RadGrid3.Visible = true;
        //                    RadGrid3.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid3.Visible = false;
        //            RadGrid3.MasterTableView.Visible = false;
        //        }

        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlAdditions")
        //    {
        //        if (dlAdditions.SelectedValue != "-2")
        //        {
        //            if (dlAdditions.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlAdditions.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid5.DataSource = empDs.Tables[0];
        //                RadGrid5.DataBind();
        //                if (RadGrid5.Visible == false)
        //                {
        //                    RadGrid5.Visible = true;
        //                    RadGrid5.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid5.Visible = false;
        //            RadGrid5.MasterTableView.Visible = false;
        //        }
        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDeptDeductions")
        //    {
        //        if (dlDeptDeductions.SelectedValue != "-2")
        //        {
        //            if (dlDeptDeductions.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDeptDeductions.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid9.DataSource = empDs.Tables[0];
        //                RadGrid9.DataBind();
        //                if (RadGrid9.Visible == false)
        //                {
        //                    RadGrid9.Visible = true;
        //                    RadGrid9.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid9.Visible = false;
        //            RadGrid9.MasterTableView.Visible = false;
        //        }
        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlClaimsDept")
        //    {
        //        if (dlClaimsDept.SelectedValue != "-2")
        //        {
        //            if (dlClaimsDept.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlClaimsDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid11.DataSource = empDs.Tables[0];
        //                RadGrid11.DataBind();
        //                if (RadGrid11.Visible == false)
        //                {
        //                    RadGrid11.Visible = true;
        //                    RadGrid11.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid11.Visible = false;
        //            RadGrid11.MasterTableView.Visible = false;
        //        }
        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlLeavesDept")
        //    {
        //        if (dlLeavesDept.SelectedValue != "-2")
        //        {
        //            if (dlLeavesDept.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlLeavesDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid7.DataSource = empDs.Tables[0];
        //                RadGrid7.DataBind();
        //                if (RadGrid7.Visible == false)
        //                {
        //                    RadGrid7.Visible = true;
        //                    RadGrid7.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid7.Visible = false;
        //            RadGrid7.MasterTableView.Visible = false;
        //        }
        //    }

        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlEmailDept")
        //    {
        //        if (dlEmailDept.SelectedValue != "-2")
        //        {
        //            if (dlEmailDept.SelectedValue == "-1")
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            else
        //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid14.DataSource = empDs.Tables[0];
        //                RadGrid14.DataBind();
        //                if (RadGrid14.Visible == false)
        //                {
        //                    RadGrid14.Visible = true;
        //                    RadGrid14.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid14.Visible = false;
        //            RadGrid14.MasterTableView.Visible = false;
        //        }
        //    }

        //}

        //protected void dlDept_databound(object sender, EventArgs e)
        //{
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
        //    {
        //        dlDept.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayDept")
        //    {
        //        ddlPayDept.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        ddlPayDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlAdditions")
        //    {
        //        dlAdditions.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlAdditions.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDeptDeductions")
        //    {
        //        dlDeptDeductions.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlDeptDeductions.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlClaimsDept")
        //    {
        //        dlClaimsDept.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlClaimsDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlLeavesDept")
        //    {
        //        dlLeavesDept.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlLeavesDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //    if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlEmailDept")
        //    {
        //        dlEmailDept.Items.Insert(0, new ListItem("- Select -", "-2"));
        //        dlEmailDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
        //    }
        //}


        protected void btnGo_Click(object sender, EventArgs e)
        {
            string strMessage = "";
            string strEmployee = "-1";
            string strprj = "-1";

            RadComboBox radcomb = new RadComboBox();
            DropDownList drp = new DropDownList();
            Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();
            CheckBox chk = new CheckBox();

            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            foreach (GridItem item in RadGrid17.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        if (Request.QueryString["PageType"] == null)
                        {
                            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                        }
                    }
                }
            }

            if (strEmployee == "-1")
            {
                strMessage = strMessage + "<br/>" + "Please Select Company.";
            }

            if (rdFrom.SelectedDate == null || rdTo.SelectedDate == null)
            {
                strMessage = strMessage + "<br/>" + "Please Enter Start Date And End Date.";
            }
            else
            {
                dt1 = rdFrom.SelectedDate.Value;
                dt2 = rdTo.SelectedDate.Value;
            }

            if (rden.SelectedDate < rdst.SelectedDate)
            {
                strMessage = strMessage + "<br/>" + "End Date should be greater than Start Date.";
            }

            if (strMessage.Length <= 0)
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[6];
                DataSet ds = new DataSet();
                rdst = rdFrom;
                rden = rdTo;
                string strActionMsg = "";

                SqlParameter[] parms1 = new SqlParameter[8];
                parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                parms1[2] = new SqlParameter("@compid", strEmployee);
                parms1[3] = new SqlParameter("@isEmpty", "No");
                parms1[4] = new SqlParameter("@empid", "-1");
                parms1[5] = new SqlParameter("@subprojid", Utility.ToString("-1"));
                parms1[6] = new SqlParameter("@sessid", "-1");
                parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(rdRepOptionTime.SelectedItem.Value));
                ds = DataAccess.ExecuteSPDataSet("sp_processtimesheetforcomp", parms1);
                Session["rptDs"] = ds;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Response.Redirect("../Reports/CustomReportNew.aspx");
                    //r
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");
                    
                    //string sFileName = "../Reports/CustomReportNew.aspx";
                    //Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
                }
                else
                {
                    ShowMessageBox("No Records Found");
                    strMessage = "";
                }
            }
            else
            {
                ShowMessageBox(strMessage);
                strMessage = "";
            }
        }

        protected void drpSubProjectID_databound(object sender, EventArgs e)
        {
            //drpSubProjectID.Items.Insert(0, new ListItem("-select-", "-1"));
        }
    }
}

 
