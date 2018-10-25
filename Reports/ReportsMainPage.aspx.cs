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

namespace SMEPayroll.Reports
{
    public partial class ReportsMainPage : System.Web.UI.Page
    {
        static int s = 0;
        string compid;
        string cpfceil = "", annualceil = "";
        string basicroundoffdefault = "-1";
        string roundoffdefault = "2";
        int multiCurr = 0;

        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;

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
            tbsComp.Tabs[0].Visible = false;
            tbsComp.Tabs[1].Visible = false;
            tbsComp.Tabs[2].Visible = false;
            tbsComp.Tabs[3].Visible = false;
            //tbsComp.Tabs[4].Visible = false;
            //tbsComp.Tabs[5].Visible = false;
           // tbsComp.Tabs[6].Visible = false;
           // tbsComp.Tabs[7].Visible = false;
            //tbsComp.Tabs[8].Visible = false;
            tbsComp.Tabs[9].Visible = false;
            tbsComp.Tabs[10].Visible = false;
            tbsComp.Tabs[11].Visible = false;
            tbsComp.Tabs[12].Visible = false;
            tbsComp.Tabs[13].Visible = false;
            tbsComp.Tabs[14].Visible = false;
            tbsComp.Tabs[15].Visible = false;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            comp_id = Utility.ToInteger(Session["Compid"]);
            sgroupname = Utility.ToString(Session["GroupName"]);
            varEmpCode = Session["EmpCode"].ToString();

            RadGrid2.ItemDataBound += new GridItemEventHandler(RadGrid2_ItemDataBound);
            RadGrid2.ItemCreated += new GridItemEventHandler(RadGrid2_ItemCreated);
            grdSelectingList.SortCommand += new GridSortCommandEventHandler(RadGrid1_SortCommand);
            RadGrid4.ItemDataBound += new GridItemEventHandler(RadGrid4_ItemDataBound);

            //Check for Company Level... ... ... ...
            string strMC = "Select MultiCurr From company Where Company_Id=" + comp_id;
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, strMC, null);

            while (dr1.Read())
            {
                if (dr1.GetValue(0) != null && dr1.GetValue(0).ToString() != "")
                {
                    multiCurr = Convert.ToInt32(dr1.GetValue(0).ToString());
                }
            }
            if (multiCurr == 0)
            {
                drpCurrency.Enabled = false;
            }
            lblerror.Text = "";
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            SqlDataSource8.ConnectionString = Session["ConString"].ToString();
            SqlDataSource44.ConnectionString = Session["ConString"].ToString();
            SqlDataSource111.ConnectionString = Session["ConString"].ToString();
            SqlDataSource222.ConnectionString = Session["ConString"].ToString();
            SqlDataSource333.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource5.ConnectionString = Session["ConString"].ToString();
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            AdvCliamdataDource.ConnectionString = Session["ConString"].ToString();
            //Advance Claim Added By Jammu Offcie///////////////
            AdvCliamReportdataDource.ConnectionString = Session["ConString"].ToString();
            //Advance Claim ends By Jammu Offcie///////////////
            xmldtYear1.ConnectionString = Session["ConString"].ToString();

            compid = Utility.ToString(Utility.ToInteger(Session["Compid"].ToString()));
            btnCompliance.Click += new EventHandler(btnCompliance_Click);
            cmbYear.SelectedIndexChanged += new EventHandler(cmbYear_SelectedIndexChanged);

            drpFilter.SelectedIndexChanged += new EventHandler(drpFilter_SelectedIndexChanged);
            // drpSubProjectID.SelectedIndexChanged += new EventHandler(drpSubProjectID_SelectedIndexChanged);

            if (!Page.IsPostBack)
            {
                grdSelectedList.Enabled = false;
                dlCustomTemplates.Enabled = false;
                ddlCommon.Enabled = false;
                txtTemplateName.Enabled = false;
                btnCreate.Enabled = false;
                Button10.Enabled = false;
                Button12.Enabled = false;
                btnCreate.Enabled = false;
                cmbYear.SelectedValue = DateTime.Now.Date.Year.ToString();
                bindMonth();
                btnUpdate.Enabled = false;
                DataSet dsAllowance = new DataSet();


                string sql = "Select * From Categories where ActiveStatus=1";
                DataSet dsts = new DataSet();
                dsts = DataAccess.FetchRS(CommandType.Text, sql, null);
                ddlCommon.DataSource = dsts;
                ddlCommon.DataTextField = "CategoryName";
                ddlCommon.DataValueField = "CategoryID";
                ddlCommon.DataBind();
                ddlSelectCategory.DataSource = dsts;
                ddlSelectCategory.DataTextField = "CategoryName";
                ddlSelectCategory.DataValueField = "CategoryID";
                ddlSelectCategory.DataBind();

                string sqlAdd = "Select [desc],code  from additions_types Where code in ('v1','v2','v3','v4') and company_id=" + comp_id;
                dsAllowance = DataAccess.FetchRS(CommandType.Text, sqlAdd, null);

                if (dsAllowance != null)
                {
                    if (dsAllowance.Tables.Count > 0)
                    {
                        DataRow dr = dsAllowance.Tables[0].NewRow();
                        dr["desc"] = "All";
                        dr["code"] = "All";

                        DataRow dr2 = dsAllowance.Tables[0].NewRow();
                        dr2["desc"] = "Labour";
                        dr2["code"] = "Labour";
                        dsAllowance.Tables[0].Rows.Add(dr2);

                        dsAllowance.Tables[0].Rows.Add(dr);
                        radCmbTsPay.DataSource = dsAllowance;
                        radCmbTsPay.DataTextField = "desc";
                        radCmbTsPay.DataValueField = "code";
                        radCmbTsPay.DataBind();
                        //radCmbTsPay.SelectedIndex = 4;
                    }
                }

                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Employee")) == false)
                //{
                //    tbsEmp.Visible = false;
                //    tbsComp.Tabs[tbsEmp.Index].Visible = false;
                //}
                //else
                //{
                //    tbsEmp.Visible = true;
                //    tbsComp.Tabs[tbsEmp.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Payroll")) == false)
                //{
                //    tbsPay.Visible = false;
                //    tbsComp.Tabs[tbsPay.Index].Visible = false;
                //}
                //else
                //{
                //    tbsPay.Visible = true;
                //    tbsComp.Tabs[tbsPay.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Additions")) == false)
                //{
                //    tbsAdditions.Visible = false;
                //    tbsComp.Tabs[tbsAdditions.Index].Visible = false;
                //}
                //else
                //{
                //    tbsAdditions.Visible = true;
                //    tbsComp.Tabs[tbsAdditions.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Deductions")) == false)
                //{
                //    tbsDeductions.Visible = false;
                //    tbsComp.Tabs[tbsDeductions.Index].Visible = false;
                //}
                //else
                //{
                //    tbsDeductions.Visible = true;
                //    tbsComp.Tabs[tbsDeductions.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Claims")) == false)
                //{
                //    tbsClaims.Visible = false;
                //    tbsComp.Tabs[tbsClaims.Index].Visible = false;
                //}
                //else
                //{
                //    tbsClaims.Visible = true;
                //    tbsComp.Tabs[tbsClaims.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Grouping")) == false)
                //{
                //    tbsGroups.Visible = false;
                //    tbsComp.Tabs[tbsGroups.Index].Visible = false;
                //}
                //else
                //{
                //    tbsGroups.Visible = true;
                //    tbsComp.Tabs[tbsGroups.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Leaves")) == false)
                //{
                //    tbsLeaves.Visible = false;
                //    tbsComp.Tabs[tbsLeaves.Index].Visible = false;
                //}
                //else
                //{
                //    tbsLeaves.Visible = true;
                //    tbsComp.Tabs[tbsLeaves.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Timesheet")) == false)
                //{
                //    tbsTimesheet.Visible = false;
                //    tbsComp.Tabs[tbsTimesheet.Index].Visible = false;
                //}
                //else
                //{
                //    tbsTimesheet.Visible = true;
                //    tbsComp.Tabs[tbsTimesheet.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - EmailTrack")) == false)
                //{
                //    tbsEmail.Visible = false;
                //    tbsComp.Tabs[tbsEmail.Index].Visible = false;
                //}
                //else
                //{
                //    tbsEmail.Visible = true;
                //    tbsComp.Tabs[tbsEmail.Index].Visible = true;
                //}
                ////new rights for Expiry,variance and timesheetPayment
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Expiry")) == false)
                //{
                //    tbsExpiry.Visible = false;
                //    tbsComp.Tabs[tbsExpiry.Index].Visible = false;
                //}
                //else
                //{
                //    tbsExpiry.Visible = true;
                //    tbsComp.Tabs[tbsExpiry.Index].Visible = true;
                //}
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Variance")) == false)
                //{
                //    tbsCompliance.Visible = false;
                //    tbsComp.Tabs[tbsCompliance.Index].Visible = false;
                //}
                //else
                //{
                //    tbsCompliance.Visible = true;
                //    tbsComp.Tabs[tbsCompliance.Index].Visible = true;
                //}

                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - TimesheetPayment")) == false)
                //{
                //    tbsTsPay.Visible = false;
                //    tbsComp.Tabs[tbsTsPay.Index].Visible = false;
                //}
                //else
                //{
                //    tbsTsPay.Visible = true;
                //    tbsComp.Tabs[tbsTsPay.Index].Visible = true;
                //}
                ////Added by Sandi
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Certificate")) == false)
                //{
                //    tbsCertificate.Visible = false;
                //    tbsComp.Tabs[tbsCertificate.Index].Visible = false;
                //}
                //else
                //{
                //    tbsCertificate.Visible = true;
                //    tbsComp.Tabs[tbsCertificate.Index].Visible = true;
                //}
                ////End Added
                ////Added by Senthil
                //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Custom Reports - Common")) == false)
                //{
                //    tbsCommon.Visible = false;
                //    tbsComp.Tabs[tbsCommon.Index].Visible = false;
                //}
                //else
                //{
                //    tbsCommon.Visible = true;
                //    tbsComp.Tabs[tbsCommon.Index].Visible = true;
                //}
                ////End Added

                ////
                //lblStart.Text = "Month:";

                string sqlStr = "";
                // sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=1 Order By Alias_Name";
                sqlStr = "SELECT ID,ALIAS_NAME = Case when Alias_Name='Sex' Then 'Gender'  Else  [Alias_Name] End ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=1 Order By Alias_Name";
                RadGrid2.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid2.DataBind();

                //Check if MC is not there Basic/basic conversion /basic currecny disabled

                sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 and ALIAS_NAME<>'CPF Addition Ordinary' and ALIAS_NAME<>'CPF Addition Wages'  and ALIAS_NAME<>'CPF Net' union  select 100+1 as ID,Apcategory as alias_name, 'AdditionPay' as  relation from AdditionPay AP  inner join APCategory AC on AP.Eid=APcatId   where company_id='" + comp_id + "'";
                // sqlStr = "SELECT ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 and ALIAS_NAME<>'CPF Addition Ordinary' and ALIAS_NAME<>'CPF Addition Wages'  and ALIAS_NAME<>'CPF Net'  Order By Alias_Name";
                //sqlStr = "SELECT ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 Order By Alias_Name";
                //sqlStr = "SELECT ALIAS_NAME,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB  WHERE TABLEiD=2 UNION Select 'Funds Summary' as ALIAS_NAME,'' as RELATION UNION Select 'Funds Detail' as ALIAS_NAME,'' as RELATION Order By Alias_Name";
                // sqlStr = "SELECT ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 and ALIAS_NAME<>'CPF Addition Ordinary' and ALIAS_NAME<>'CPF Addition Wages'  and ALIAS_NAME<>'CPF Net'  Order By Alias_Name";
                //sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 Order By Alias_Name";
                //Updated by Su Mon
                DataTable dtPayroll = DataAccess.FetchRS(CommandType.Text, sqlStr, null).Tables[0];
                DataRow drPayroll;
                drPayroll = dtPayroll.NewRow();
                drPayroll[1] = "Basic_Pay_Conversion";
                drPayroll[2] = "B_P_Con";
                dtPayroll.Rows.Add(drPayroll);

                drPayroll = dtPayroll.NewRow();
                drPayroll[1] = "Basic_Pay_Currency";
                drPayroll[2] = "B_P_Cur";
                dtPayroll.Rows.Add(drPayroll);

                DataView dv = dtPayroll.DefaultView;
                dv.Sort = "ALIAS_NAME";
                DataTable sortedDT = dv.ToTable();

                RadGrid4.DataSource = dtPayroll;
                RadGrid4.DataBind();

                //sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
                //sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')";
                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')";
                RadGrid6.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid6.DataBind();

                //////////////////Payroll Costing Added By Jammu Office////////////
                sqlStr = "select Bid,BusinessUnit from Cost_Ccategory where Company_ID=" + comp_id + "";
                RadGridPayrollCostingCategory.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGridPayrollCostingCategory.DataBind();

                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')";
                RadGridPayrollCostingAdditions.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGridPayrollCostingAdditions.DataBind();

                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewDeductionsDed  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) ";
                RadGridPayrollCostingDeductions.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGridPayrollCostingDeductions.DataBind();

                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID  from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION='Claim' ";
                RadGridClaimType.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGridClaimType.DataBind();

                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID  from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION='Claim' ";
                RadGridClaimTypeReport.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGridClaimTypeReport.DataBind();

                RadGridPayrollCostingPayroll.ItemDataBound += new GridItemEventHandler(RadGridPayrollCostingPayroll_ItemDataBound);
                sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 and ALIAS_NAME<>'CPF Addition Ordinary' and ALIAS_NAME<>'CPF Addition Wages'  and ALIAS_NAME<>'CPF Net' union  select 100+1 as ID,Apcategory as alias_name, 'AdditionPay' as  relation from AdditionPay AP  inner join APCategory AC on AP.Eid=APcatId   where company_id='" + comp_id + "'";
                DataTable dtPayrollCostingPayroll = DataAccess.FetchRS(CommandType.Text, sqlStr, null).Tables[0];

                RadGridPayrollCostingPayroll.DataSource = dtPayrollCostingPayroll;
                RadGridPayrollCostingPayroll.DataBind();
                //////////////////Payroll Costing ends By Jammu Office////////////

                // sqlStr = "select id, description from dbo.ViewDeductions  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) And id in (select distinct trx_type from emp_deductions where status='L') ";
                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewDeductionsDed  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) ";
                RadGrid10.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid10.DataBind();

                //santy
                sqlStr = "select id, Category_Name description from CertificateCategory  WHERE COMPANY_ID=" + compid + " OR Company_ID='-1'";
                RadGrid17.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid17.DataBind();

                //murugan
                string str1 = "Select * From( select EA.ID Child_ID,EA.Sub_Project_ID,(Select Sub_Project_ID From SubProject  SP Where SP.id=-2) ID,(select Sub_Project_Name from SubProject where id=-2) ";
                str1 = str1 + "Sub_Project_Name,Emp_Id,[EmpName] = Case When termination_date is null  Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')+'[active]')  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End ";

                str1 = str1 + " from EmployeeAssignedToProject EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code where EM.Company_ID=4 AND 1=0";
                str1 = str1 + " Union Select Distinct 0 Child_ID,(Select ID From SubProject  SP Where SP.id=-2) ID,(Select top 1 Sub_Project_ID From SubProject  SP ";
                str1 = str1 + " Where SP.id=-2) Sub_Project_ID, (Select top 1 Sub_Project_Name From SubProject  SP Where SP.id=-2) Sub_Project_ID,";
                str1 = str1 + " (Select top 1 Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select top 1 [EmpName] = Case When termination_date is null  Then ";
                str1 = str1 + " (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From Employee";
                str1 = str1 + " Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) =(Select top 1 Sub_Project_ID From SubProject  SP Where SP.id=-2) And SOftDelete = 0) D Order By EmpName";

                SqlDataSource111.SelectCommand = str1;


                RadGrid111.DataBind();


                //grdSelectingList.DataSource = SelectingItems;
                //grdSelectingList.DataBind();
                //grdSelectedList.DataSource = SelectedItems;
                //grdSelectedList.DataBind();
                //sandi
                //DataSet CertificateDs;

                //sqlStr = "SELECT CC.id, [Category_Name], [Company_ID],CC.COLID FROM [CertificateCategory1] CC  where Company_Id in ('-1'," + compid + ")";
                //CertificateDs = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                //if (CertificateDs.Tables[0].Rows.Count > 0)
                //{
                //    RadGrid21.DataSource = CertificateDs.Tables[0];
                //    RadGrid21.DataBind();
                //    if (RadGrid21.Visible == false)
                //    {
                //        RadGrid21.Visible = true;
                //        RadGrid21.MasterTableView.Visible = true;
                //    }
                //}

                if (HttpContext.Current.Session["PAYTYPE"].ToString() == "0")
                {
                    sqlStr = "Select p.rowid, (P.MonthName+ ' - ' + cast(p.[Year] as varchar)) AliasMonth From Payrollmonthlydetail p inner join company c on p.paytype>=0 where company_id=" + compid + " and p.rowid in (select Distinct MonthYear from emailtrack) order by p.monthname";
                }
                else
                {
                    sqlStr = "Select p.rowid, (P.MonthName+ ' - ' + cast(p.[Year] as varchar)) AliasMonth From Payrollmonthlydetail p inner join company c on p.paytype=c.payrolltype where company_id=" + compid + " and p.rowid in (select Distinct MonthYear from emailtrack) order by p.monthname";
                }
                RadGrid15.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                RadGrid15.DataBind();

                dlDept.Items.Add(new ListItem("- Select -", "-1"));
                dlDept.Items.Add(new ListItem("-All-", "-1"));

                ddlPayDept.Items.Insert(0, "- Select -");
                ddlPayDept.Items.Insert(1, "-All-");

                dlAdditions.Items.Insert(0, "- Select -");
                dlAdditions.Items.Insert(1, "-All-");
                //santy
                dlExpiryDept.Items.Insert(0, "-Select-");
                dlExpiryDept.Items.Insert(1, "-All-");

                dlEmailDept.Items.Insert(0, "- Select -");
                dlEmailDept.Items.Insert(1, "-All-");


                //Advance Claim Added by Jammu Office//////
                ddlClaimReportDept.Items.Insert(0, "- Select -");
                ddlClaimReportDept.Items.Insert(1, "-All-");
                ////Advance Claim ends by Jammu Office//////

                //************Commented by jaspreet bcz shows null error
                //drpYear.DataSourceID = "xmldtYear";
               // drpYear.DataTextField = "text";
                //drpYear.DataValueField = "id";
                //drpYear.DataBind();
                //drpYear.Items.FindByText(System.DateTime.Today.Year.ToString()).Selected = true;
            }
            else
            {
            }

            rdVar.SelectedIndexChanged += new EventHandler(rdVar_SelectedIndexChanged);
            btnvVariance.Click += new EventHandler(btnvVariance_Click);

            RadGrid3.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid3_NeedDataSource);
            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
            RadGrid2.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid2_NeedDataSource);
            RadGrid4.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid4_NeedDataSource);
            RadGrid5.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid5_NeedDataSource);
            RadGrid6.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid6_NeedDataSource);
            //Payment Variance Report/////////////////
            RadGridEmployeePaymentVariace.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridEmployeePaymentVariace_NeedDataSource);
            //ends//////////////////////////////////
            RadGrid10.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid10_NeedDataSource);
            RadGrid14.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid14_NeedDataSource);
            RadGrid15.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid15_NeedDataSource);
            RadGrid16.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid16_NeedDataSource);
            RadGrid17.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid17_NeedDataSource);
            //RadGrid22.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid22_NeedDataSource);

            ///////////////Payroll Costing Added By Jammu Office////////
            RadGridPayrollCostingCategory.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridPayrollCostingCategory_NeedDataSource);
            RadGridPayrollCostingAdditions.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridPayrollCostingAdditions_NeedDataSource);
            RadGridPayrollCostingDeductions.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridPayrollCostingDeductions_NeedDataSource);
            RadGridPayrollCostingPayroll.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridPayrollCostingPayroll_NeedDataSource);
            ///////////////Payroll Costing ends By Jammu Office////////
            ///////////////Advance Claim Added By Jammu Office////////
            RadGridEmployeeClaimReport.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridEmployeeClaimReport_NeedDataSource);
            RadGridClaimType.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridClaimType_NeedDataSource);
            RadGridClaimTypeReport.NeedDataSource += new GridNeedDataSourceEventHandler(RadGridClaimTypeReport_NeedDataSource);
            ///////////////Advance Claim ends By Jammu Office////////

            //costing
            if (!IsPostBack)
            {
                LoadEmpGrid();
                LoadProject();
            }
            if (IsPostBack)
            {
                try
                {
                    if (RadDatePicker1.SelectedDate.Value.ToString() != "" && RadDatePicker2.SelectedDate.Value.ToString() != "")
                    {
                        dlAdditions.Enabled = true;
                    }
                }
                catch (Exception ex)
                { }
                try
                {
                    if (dtp1.SelectedDate.Value.ToString() != "" && dtp2.SelectedDate.Value.ToString() != "")
                    {
                        ddlPayDept.Enabled = true;
                    }
                }
                catch (Exception ex)
                { }
                //Advance Claim Added by Jammu Office//////////
                try
                {
                    if (ClaimReportDate1.SelectedDate.Value.ToString() != "" && ClaimReportDate2.SelectedDate.Value.ToString() != "")
                    {
                        ddlClaimReportDept.Enabled = true;
                    }
                }
                catch (Exception ex)
                { }
                //Advance Claim ends by Jammu Office//////////
            }
        }
        void RadGrid2_ItemCreated(object sender, GridItemEventArgs e)
        {

        }
        void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = (GridDataItem)e.Item;
                if (multiCurr == 0)
                {
                    //if (DataItem["ALIAS_NAME"].Text == "BasicPayConversion" || DataItem["ALIAS_NAME"].Text == "Exchange Rate" || DataItem["ALIAS_NAME"].Text == "Basic Pay Currency" || DataItem["ALIAS_NAME"].Text == "Basic Pay")
                    //updated by Su Mon
                    if (DataItem["ALIAS_NAME"].Text == "BasicPayConversion" || DataItem["ALIAS_NAME"].Text == "Exchange Rate" || DataItem["ALIAS_NAME"].Text == "Basic Pay Currency")
                    {
                        DataItem.Enabled = false;
                    }
                }
                else
                {
                    if (DataItem["ALIAS_NAME"].Text == "Exchange Rate")
                    {
                        DataItem.Selected = true;
                    }
                }
            }
        }

        void RadGrid4_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = (GridDataItem)e.Item;
                if (multiCurr == 0)
                {
                    //if (DataItem["ALIAS_NAME"].Text == "BasicPayConversion" || DataItem["ALIAS_NAME"].Text == "Exchange Rate" || DataItem["ALIAS_NAME"].Text == "Basic Pay Currency" || DataItem["ALIAS_NAME"].Text == "Basic Pay")
                    //updated by Su Mon
                    if (DataItem["ALIAS_NAME"].Text == "Basic_Pay_Conversion" || DataItem["ALIAS_NAME"].Text == "Basic_Pay_Currency")
                    {
                        DataItem.Enabled = false;
                    }
                }
                else
                {
                    if (DataItem["ALIAS_NAME"].Text == "Exchange Rate")
                    {
                        DataItem.Selected = true;
                    }
                }
            }
        }


        //Payroll Costing Added by Jammu Office///////
        void RadGridPayrollCostingPayroll_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem DataItem = (GridDataItem)e.Item;
                if (multiCurr == 0)
                {
                    if (DataItem["ALIAS_NAME"].Text == "Basic_Pay_Conversion" || DataItem["ALIAS_NAME"].Text == "Basic_Pay_Currency" || DataItem["ALIAS_NAME"].Text == "fund_type")
                    {
                        // DataItem.Enabled = false;
                        DataItem.Display = false;
                    }
                }
                else
                {
                    if (DataItem["ALIAS_NAME"].Text == "Exchange Rate")
                    {
                        DataItem.Selected = true;
                    }
                }
            }
        }

        //Payroll Costing ends by Jammu Office///////
        void RadGrid17_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "select id, Category_Name description from CertificateCategory  WHERE COMPANY_ID=" + compid + " OR Company_ID='-1'";
            RadGrid17.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        void RadGrid16_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlSelect;
            DataSet empDs;
            if (dlExpiryDept.SelectedValue != "-2")
            {
                if (dlExpiryDept.SelectedValue == "-1")
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";                         
                    }
                    else
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        // sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME"; 
                    }
                }
                else
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";                        
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                    else
                    {
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                }
                empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGrid16.DataSource = empDs.Tables[0];
                    // RadGrid16.DataBind();
                    if (RadGrid16.Visible == false)
                    {
                        RadGrid16.Visible = true;
                        RadGrid16.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGrid16.Visible = false;
                RadGrid16.MasterTableView.Visible = false;
            }
        }

        void RadGrid15_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr;
            if (HttpContext.Current.Session["PAYTYPE"].ToString() == "0")
            {
                sqlStr = "Select p.rowid, (P.MonthName+ ' - ' + cast(p.[Year] as varchar)) AliasMonth From Payrollmonthlydetail p inner join company c on p.paytype>=0 where company_id=" + compid + " and p.rowid in (select Distinct MonthYear from emailtrack) order by p.monthname";
            }
            else
            {
                sqlStr = "Select p.rowid, (P.MonthName+ ' - ' + cast(p.[Year] as varchar)) AliasMonth From Payrollmonthlydetail p inner join company c on p.paytype=c.payrolltype where company_id=" + compid + " and p.rowid in (select Distinct MonthYear from emailtrack) order by p.monthname";
            }
            RadGrid15.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        void RadGrid14_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlSelect;
            DataSet empDs;
            if (dlEmailDept.SelectedValue != "-2")
            {
                if (dlEmailDept.SelectedValue == "-1")
                //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME"; 
                    }
                    else
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";   
                    }
                }
                else
                // sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    }
                    else
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME"; 
                    }
                }
                empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGrid14.DataSource = empDs.Tables[0];
                    //RadGrid14.DataBind();
                    if (RadGrid14.Visible == false)
                    {
                        RadGrid14.Visible = true;
                        RadGrid14.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGrid14.Visible = false;
                RadGrid14.MasterTableView.Visible = false;
            }
        }
        //Payment Variance Report//////////////////////////////
        void RadGridEmployeePaymentVariace_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet empDs;
            string sqlSelect;
            if (dropdownDeptPaymentVariance.SelectedValue != "-2")
            {
                if (dropdownDeptPaymentVariance.SelectedValue == "-1")
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                    else
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                }
                else
                {
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]',Time_card_no End FROM dbo.employee WHERE DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]',Time_card_no End FROM dbo.employee WHERE DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                    else
                    {
                        if (sgroupname == "Super Admin")
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                        }
                    }
                }
                empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGridEmployeePaymentVariace.DataSource = empDs.Tables[0];
                    if (RadGridEmployeePaymentVariace.Visible == false)
                    {
                        RadGridEmployeePaymentVariace.Visible = true;
                        RadGridEmployeePaymentVariace.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGridEmployeePaymentVariace.Visible = false;
                RadGridEmployeePaymentVariace.MasterTableView.Visible = false;
            }
        }
        //ends///////////////////////////////////////////
        //Advance Claim Added By Jammu Office////////////////////////////////////////////
        void RadGridEmployeeClaimReport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet empDs;
            if (ddlClaimReportDept.SelectedValue != "-2")
            {
                SqlParameter[] parms1 = new SqlParameter[4];
                parms1[0] = new SqlParameter("@company_id", comp_id);
                parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                parms1[2] = new SqlParameter("@Type", "DEPART");
                parms1[3] = new SqlParameter("@TypeID", ddlClaimReportDept.SelectedValue.ToString());
                empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGridEmployeeClaimReport.DataSource = empDs.Tables[0];
                    if (RadGridEmployeeClaimReport.Visible == false)
                    {
                        RadGridEmployeeClaimReport.Visible = true;
                        RadGridEmployeeClaimReport.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGridEmployeeClaimReport.Visible = false;
                RadGridEmployeeClaimReport.MasterTableView.Visible = false;
            }
        }

        void RadGridClaimType_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION='Claim'  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
            RadGridClaimType.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
            RadGridClaimType.DataBind();
        }

        void RadGridClaimTypeReport_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION='Claim'  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
            RadGridClaimTypeReport.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
            RadGridClaimTypeReport.DataBind();
        }
        //Advance Claim ends By Jammu Office////////////////////////////////////////////
        void RadGrid10_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "select id, description from dbo.ViewDeductions  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) And id in (select distinct trx_type from emp_deductions where status='L') ";
            RadGrid10.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        void RadGrid6_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
            RadGrid6.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        /////////Payroll Costing Added By Jammu Office//////////////////
        void RadGridPayrollCostingCategory_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            sqlStr = "select Bid,BusinessUnit from Cost_Ccategory where Company_ID=" + comp_id + "";
            RadGridPayrollCostingCategory.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);

        }
        void RadGridPayrollCostingAdditions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            sqlStr = "select id, description from dbo.ViewAdditionTypesDesc  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')  And id in (select distinct trx_type from emp_additions where status='L' And ClaimStatus='Approved')";
            RadGridPayrollCostingAdditions.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        void RadGridPayrollCostingDeductions_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "select id, description from dbo.ViewDeductions  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) And id in (select distinct trx_type from emp_deductions where status='L') ";
            RadGridPayrollCostingDeductions.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }
        /////////Payroll Costing ends By Jammu Office//////////////////

        void RadGrid5_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet empDs;
            if (dlAdditions.SelectedValue != "-2")
            {
                SqlParameter[] parms1 = new SqlParameter[4];
                parms1[0] = new SqlParameter("@company_id", comp_id);
                parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                parms1[2] = new SqlParameter("@Type", "DEPART");
                parms1[3] = new SqlParameter("@TypeID", dlAdditions.SelectedValue.ToString());
                empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);

                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGrid5.DataSource = empDs.Tables[0];
                    //RadGrid5.DataBind();
                    if (RadGrid5.Visible == false)
                    {
                        RadGrid5.Visible = true;
                        RadGrid5.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGrid5.Visible = false;
                RadGrid5.MasterTableView.Visible = false;
            }
        }

        void RadGrid4_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 Order By Alias_Name";
            //sqlStr = "SELECT ALIAS_NAME,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB  WHERE TABLEiD=2 UNION Select 'Funds Summary' as ALIAS_NAME,'' as RELATION UNION Select 'Funds Detail' as ALIAS_NAME,'' as RELATION Order By Alias_Name";
            RadGrid4.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        /////////////////////Payroll Costing Added By Jammu Office////////////////
        void RadGridPayrollCostingPayroll_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 Order By Alias_Name";
            //sqlStr = "SELECT ALIAS_NAME,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB  WHERE TABLEiD=2 UNION Select 'Funds Summary' as ALIAS_NAME,'' as RELATION UNION Select 'Funds Detail' as ALIAS_NAME,'' as RELATION Order By Alias_Name";
            RadGridPayrollCostingPayroll.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        /////////////////////Payroll Costing ends By Jammu Office////////////////

        void RadGrid2_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlStr = "";
            // sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=1 Order By Alias_Name";
            sqlStr = "SELECT ID,ALIAS_NAME = Case when Alias_Name='Sex' Then 'Gender'  Else  [Alias_Name] End ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=1 Order By Alias_Name";
            RadGrid2.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
        }

        void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            string sqlSelect;
            DataSet empDs;
            //if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
            {
                if (dlDept.SelectedValue != "-2")
                {
                    if (dlDept.SelectedValue == "-1")

                    //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no  FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no  FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";                             
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";                            
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
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
            }
        }
        //void RadGrid22_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    string sqlSelect;
        //    DataSet empDs;
        //     //if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
        //    {
        //        if (ddlCommon.SelectedValue != "-2")
        //        {
        //            if (ddlCommon.SelectedValue == "-1")

        //            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            {
        //                if (chkExcludeTerminateEmp.Checked)
        //                {
        //                    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no  FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //                }
        //                else
        //                {
        //                    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //                }
        //            }
        //            else
        //            //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //            {
        //                if (chkExcludeTerminateEmp.Checked)
        //                {
        //                    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + ddlCommon.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //                }
        //                else
        //                {
        //                    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlCommon.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
        //                }
        //            }
        //            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
        //            if (empDs.Tables[0].Rows.Count > 0)
        //            {
        //                RadGrid22.DataSource = empDs.Tables[0];
        //                //RadGrid1.DataBind();
        //                if (RadGrid22.Visible == false)
        //                {
        //                    RadGrid22.Visible = true;
        //                    RadGrid22.MasterTableView.Visible = true;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            RadGrid22.Visible = false;
        //            RadGrid22.MasterTableView.Visible = false;
        //        }
        //    }
        //}

        void RadGrid3_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            DataSet empDs;
            //if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayDept")

            if (ddlPayDept.SelectedValue != "-2")
            {
                //if (ddlPayDept.SelectedValue == "-1")
                //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                //else
                //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + ddlPayDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                SqlParameter[] parms1 = new SqlParameter[4];
                parms1[0] = new SqlParameter("@company_id", comp_id);
                parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                parms1[2] = new SqlParameter("@Type", "DEPART");
                parms1[3] = new SqlParameter("@TypeID", ddlPayDept.SelectedValue.ToString());
                //empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);

                if (chkExcludeTerminateEmp.Checked)
                {
                    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll1", parms1);
                }
                else
                {
                    string terminate = "";
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        terminate = "YES";
                    }
                    else
                    {
                        terminate = "NO";
                    }

                    string fromdate = Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string todate = Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    SqlParameter[] parms_tenminated = new SqlParameter[7];
                    parms_tenminated[0] = new SqlParameter("@company_id", comp_id);
                    parms_tenminated[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    parms_tenminated[2] = new SqlParameter("@Type", "DEPART");
                    parms_tenminated[3] = new SqlParameter("@TypeID", ddlPayDept.SelectedValue.ToString());
                    parms_tenminated[4] = new SqlParameter("@Terminated", terminate);
                    parms_tenminated[5] = new SqlParameter("@FromDate", fromdate);
                    parms_tenminated[6] = new SqlParameter("@ToDate", todate);
                    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms_tenminated);
                }

                //empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                    RadGrid3.DataSource = empDs.Tables[0];
                    // RadGrid3.DataBind();
                    if (RadGrid3.Visible == false)
                    {
                        RadGrid3.Visible = true;
                        RadGrid3.MasterTableView.Visible = true;
                    }
                }
            }
            else
            {
                RadGrid3.Visible = false;
                RadGrid3.MasterTableView.Visible = false;
            }

        }

        protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            RadGrid1.DataBind();
        }

        //new
        protected void GenerateClaimRpt_Click(object sender, EventArgs e)
        {
            DataSet rptDs;
            string sqlSelect = "select Emp_Name,[From],[To],Reason,[Status],Remark from [EmailTrackerNew] where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and Module='Claim' order by [currentDateTime] desc";
            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            Session["rptDs"] = rptDs;
            Response.Redirect("../Reports/CustomReportNew.aspx");
        }

        protected void GenerateLoginEmailRpt_Click(object sender, EventArgs e)
        {
            DataSet rptDs;
            string sqlSelect = "select Emp_Name,[From],[To],Reason,[Status],Remark from [EmailTrackerNew] where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and Module='Login' order by [currentDateTime] desc";
            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            Session["rptDs"] = rptDs;
            Response.Redirect("../Reports/CustomReportNew.aspx");
        }

        protected void GenerateLeaveRpt_Click(object sender, EventArgs e)
        {
            DataSet rptDs;
            string sqlSelect = "select Emp_Name,[From],[To],Reason,[Status],Remark from [EmailTrackerNew] where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and Module='Leave' order by [currentDateTime] desc";
            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            Session["rptDs"] = rptDs;
            Response.Redirect("../Reports/CustomReportNew.aspx");
        }

        protected void GenerateSubmitPayrollEmailRpt_Click(object sender, EventArgs e)
        {
            DataSet rptDs;
            string sqlSelect = "select Emp_Name,Reason,[Status],Remark from [EmailTrackerNew] where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and Module='Payroll' order by [currentDateTime] desc";
            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            Session["rptDs"] = rptDs;
            Response.Redirect("../Reports/CustomReportNew.aspx");
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
                        sqlSelect = "Select Docid,E.Emp_Code,E.time_card_no,(select Deptname from department where id=E.dept_id)Department, isnull(E.emp_name,'')+' '+isnull(E.emp_lname,'') SentTo , EventView = Case When EventView=1 Then 'Email Payslip' Else '' End, Senton, isnull(Em.emp_name,'')+' '+isnull(Em.emp_lname,'') SentBy, [T.Status] = Case When T.Status=0 Then 'Success' Else 'Fail' End, T.Remarks From emailtrack T Inner Join Employee E On (T.Sentto = E.Emp_Code Or T.Sentto=0) Inner Join Employee Em On T.SentBy = Em.Emp_Code Where E.Emp_Code In (" + strEmployee + ") And T.MonthYear in (" + sqlTrns + ") And EventView = '" + rdOptionEmail.SelectedValue + "' and cast(t.docid as varchar) like '%" + txtDocNo.Text.ToString().Trim() + "%'";
                    }
                    else
                    {
                        sqlSelect = "Select Docid,E.Emp_Code,E.time_card_no,(select Deptname from department where id=E.dept_id)Department, isnull(E.emp_name,'')+' '+isnull(E.emp_lname,'') SentTo , EventView = Case When EventView=1 Then 'Email Payslip' Else '' End, Senton, isnull(Em.emp_name,'')+' '+isnull(Em.emp_lname,'') SentBy, [T.Status] = Case When T.Status=0 Then 'Success' Else 'Fail' End, T.Remarks From emailtrack T Inner Join Employee E On (T.Sentto = E.Emp_Code Or T.Sentto=0) Inner Join Employee Em On T.SentBy = Em.Emp_Code Where E.Emp_Code In (" + strEmployee + ") And T.MonthYear in (" + sqlTrns + ") And EventView = '" + rdOptionEmail.SelectedValue + "'";
                    }
                    DataSet rptDs;
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=55");
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
        //Added by Sandi
        protected void btnCertificate_Click(object sender, EventArgs e)
        {
            string sqlQuery = "";
            string strEmployee = "0";
            string strCategory = "0";

            string sqlSelect = "Select isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name From EmployeeCertificate EC Inner Join CertificateCategory CC On EC.CertificateCategoryID = CC.ID inner join Employee e on EC.Emp_ID=e.emp_code where EC.Emp_ID in(";
            string sqlSelect2 = "Select Category_Name,Emp_id,isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name,(SELECT DeptName FROM Department WHERE ID=e.Dept_ID and Dept_ID is not NULL) AS [Department],(select time_card_no from employee where emp_code=e.emp_code) TimeCardId,EC.ExpiryDate From EmployeeCertificate EC Inner Join CertificateCategory CC On EC.CertificateCategoryID = CC.ID inner join Employee e on EC.Emp_ID=e.emp_code where EC.Emp_ID in(";

            int grid1 = 0;
            int grid2 = 0;
            lblerror.Text = "";

            DataSet rptDs = new DataSet();
            DataSet rptDsAnd = new DataSet();

            foreach (GridItem item in RadGrid20.MasterTableView.Items)
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
            if (grid1 > 0)
            {
                sqlSelect = sqlSelect + strEmployee + ") and CertificateCategoryID in (";
                sqlSelect2 = sqlSelect2 + strEmployee + ") and CertificateCategoryID in (";
            }

            DataTable dtFinal = rptDs.Tables.Add("Certificate");
            DataTable dtAnd = rptDsAnd.Tables.Add("Certificate2");

            DataColumn cEmp = new DataColumn("Employee");
            dtFinal.Columns.Add(cEmp);
            DataColumn cEmp2 = new DataColumn("Employee");
            dtAnd.Columns.Add(cEmp2);

            ArrayList arryList1 = new ArrayList();

            foreach (GridItem item in RadGrid21.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        strCategory = strCategory + "," + dataItem.Cells[3].Text.ToString().Trim();

                        string CName = dataItem.Cells[5].Text.ToString().Trim();

                        arryList1.Add(CName);

                        //DataColumn c = new DataColumn(CName);
                        //c.DataType = System.Type.GetType("System.String");
                        //dtFinal.Columns.Add(CName, typeof(Image));
                        //dtFinal.Columns.Add(CName, typeof(bool));
                        //dtAnd.Columns.Add(CName, typeof(bool));
                        dtFinal.Columns.Add(CName, typeof(string));
                        dtAnd.Columns.Add(CName, typeof(string));
                    }
                }
            }
            if (grid2 > 0)
            {
                sqlSelect = sqlSelect + strCategory + ") group by isnull(emp_name,'')+' '+isnull(emp_lname,'') ,emp_id";
                sqlSelect2 = sqlSelect2 + strCategory + ")";
            }

            if (grid1 == 0)
            {
                lblerror.Text = "Please Select Atleast One Employee";
            }
            if (grid2 == 0)
            {
                lblerror.Text = "Please Select Atleast One Certificate";
            }
            else
            {
                DataSet rptDs2 = new DataSet();
                rptDs2 = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                DataSet rptDs3 = new DataSet();
                rptDs3 = DataAccess.FetchRS(CommandType.Text, sqlSelect2, null);

                if (rptDs2.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < rptDs2.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr1 = dtFinal.NewRow();
                        dr1["Employee"] = Convert.ToString(rptDs2.Tables[0].Rows[i].ItemArray[0].ToString());

                        foreach (GridItem item in RadGrid21.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    grid2++;
                                    strCategory = strCategory + "," + dataItem.Cells[3].Text.ToString().Trim();

                                    string CName = dataItem.Cells[5].Text.ToString().Trim();

                                    for (int j = 0; j < rptDs3.Tables[0].Rows.Count; j++)
                                    {
                                        if (Convert.ToString(rptDs2.Tables[0].Rows[i].ItemArray[0].ToString()) == Convert.ToString(rptDs3.Tables[0].Rows[j].ItemArray[2].ToString()) & CName == Convert.ToString(rptDs3.Tables[0].Rows[j].ItemArray[0].ToString().Trim()))
                                        {
                                            // dr1[CName] = rptDs3.Tables[0].Rows[j].ItemArray[5].ToString().Substring(0,10);  
                                            dr1[CName] = rptDs3.Tables[0].Rows[j].ItemArray[5].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        dtFinal.Rows.Add(dr1);
                    }
                }

                if (chkAndOr.Checked == true)
                {
                    bool andvalue = false;
                    string col = "";
                    int count = 0;

                    DataRow dr2 = dtAnd.NewRow();

                    for (int k = 0; k < dtFinal.Rows.Count; k++)
                    {
                        count = 0;
                        dr2["Employee"] = dtFinal.Rows[k]["Employee"].ToString();

                        for (int j = 0; j < arryList1.Count; j++)
                        {
                            col = arryList1[j].ToString();
                            bool boolvalue = false;
                            bool result = Boolean.TryParse(dtFinal.Rows[k][col].ToString(), out boolvalue);
                            if (dtFinal.Rows[k][col].ToString() == "")
                            {

                            }
                            else if (boolvalue == true)
                            {
                                count++;
                                dr2[col] = dtFinal.Rows[k][col].ToString();
                            }
                        }
                        if (count == arryList1.Count)
                        {
                            dtAnd.Rows.Add(dr2);
                        }
                    }
                }
                if (dtAnd.Rows.Count > 0 & chkAndOr.Checked == true)
                    Session["rptDs"] = rptDsAnd;
                else
                    Session["rptDs"] = rptDs;
                //Session["ColName"] = arryList1;
                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=27");
            }
        }
        //End Added
        IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);


        protected void GenerateRpt_Click(object sender, EventArgs e)
        {
            string sqlQuery = "";
            string strEmployee = "0";
            // string sqlSelect = "select emp_code Emp_Code,(select time_card_no from employee where emp_code=e1.emp_code) TimeCardId, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name , ";
            string sqlSelect = "select emp_code Emp_Code,(select time_card_no from employee where emp_code=e1.emp_code) TimeCardId, isnull(emp_name,'')+' '+isnull(emp_lname,'') Full_Name ,(select Businessunit from dbo.Cost_BusinessUnit where Bid=(select cost_Businessunit from employee where emp_code=e1.emp_code)) BusinessUnit,(select Businessunit from dbo.Cost_Region where Bid=(select cost_Region from employee where emp_code=e1.emp_code)) Region,(select Businessunit from dbo.Cost_Ccategory where Bid=(select cost_Category from employee where emp_code=e1.emp_code)) Category, ";
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
                        if (dataItem.Cells[5].Text.ToString().Trim() != "emp_code" && dataItem.Cells[5].Text.ToString().Trim() != "emp_name")
                        {
                            if (dataItem.Cells[5].Text.ToString().Trim() == "payment_mode")
                            {
                                //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + " AS [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                            }
                            else
                            {
                                string str = "";
                                if (dataItem.Cells[4].Text.ToString() == "BasicPayConversion")
                                {
                                    str = "CONVERT(numeric(10,2),(Select Top 1 ISNULL(Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))),0) " +
                                        " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                    sqlSelect = sqlSelect + " " + str + " AS [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                                else if (dataItem.Cells[4].Text.ToString() == "Exchange Rate")
                                {
                                    str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                    sqlSelect = sqlSelect + " " + str + " AS [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                                else
                                {
                                    sqlSelect = sqlSelect + " " + dataItem.Cells[5].Text.ToString().Trim() + " AS [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                            }
                        }
                    }
                }
            }

            if (grid1 > 0)
            {
                if (grid2 > 0)
                {
                    sqlSelect = sqlSelect.TrimEnd();
                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                    sqlSelect = sqlSelect + " from Employee e1  where e1.emp_code in (" + strEmployee + ")  ORDER BY EMP_NAME";
                    DataSet rptDs;
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    string basecurr = "";

                    //Check if MC is there or not 
                    int mc = 0;
                    string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid;
                    SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

                    while (drcon.Read())
                    {
                        if (drcon.GetValue(1) == null || drcon.GetValue(1).ToString() == "")
                        {
                            mc = 0;
                        }
                        else
                        {
                            mc = Convert.ToInt32(drcon.GetValue(1).ToString());
                        }
                    }
                    if (mc == 1)
                    {
                        if (drpCurrency.SelectedValue == "2")
                        {
                            foreach (DataRow dr in rptDs.Tables[0].Rows)
                            {
                                //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                while (drcurrb.Read())
                                {
                                    if (drcurrb.GetValue(0) != null)
                                    {
                                        basecurr = drcurrb.GetValue(0).ToString();
                                    }
                                }
                                if (basecurr == "1")
                                {
                                    basecurr = "SGD";
                                }
                                else
                                {
                                    basecurr = "UGD";
                                }

                                //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                //{

                                if (dr.Table.Columns["Basic Pay Currency"] != null)
                                {
                                    if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    {
                                        if (dr.Table.Columns["Basic Pay"] != null)
                                        {
                                            dr.BeginEdit();
                                            if (dr["Basic Pay"].ToString() != "")
                                            {
                                                if (dr.Table.Columns["BasicPayConversion"] != null)
                                                {
                                                    dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                                }
                                            }
                                            dr.AcceptChanges();
                                        }

                                        if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                        {
                                            dr.BeginEdit();
                                            if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                            {
                                                double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                                if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                                {
                                                    dr["Exchange Rate"] = val.ToString();
                                                }
                                            }
                                            dr.AcceptChanges();
                                        }
                                    }
                                    else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    {
                                        //dr.BeginEdit();
                                        //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //dr["BasicPayConversion"] = val.ToString();
                                        //dr.AcceptChanges();
                                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                        {
                                            dr.BeginEdit();
                                            if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                            {
                                                double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                                if (val.ToString() != "NaN")
                                                {
                                                    dr["Exchange Rate"] = val.ToString();
                                                    decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                                    decimal newValue = decimal.Round(value, 2);
                                                    dr["BasicPayConversion"] = Convert.ToString(newValue);
                                                }
                                                dr.AcceptChanges();
                                            }
                                        }
                                    }
                                }
                                //}
                            }
                        }
                        if (drpCurrency.SelectedValue == "1")
                        {
                            foreach (DataRow dr in rptDs.Tables[0].Rows)
                            {
                                //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                while (drcurrb.Read())
                                {
                                    if (drcurrb.GetValue(0) != null)
                                    {
                                        basecurr = drcurrb.GetValue(0).ToString();
                                    }
                                }
                                if (basecurr == "1")
                                {
                                    basecurr = "SGD";
                                }
                                else
                                {
                                    basecurr = "UGD";
                                }
                                //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                //{
                                if (dr.Table.Columns["Basic Pay Currency"] != null)
                                {
                                    if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    {
                                        dr.BeginEdit();
                                        if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        {
                                            if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                            {
                                                double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                                dr["BasicPayConversion"] = Math.Round(val, 2);
                                            }
                                            dr.AcceptChanges();
                                        }
                                    }
                                    if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    {
                                        if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        {
                                            dr.BeginEdit();
                                            double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                            dr["BasicPayConversion"] = val.ToString();
                                            dr.AcceptChanges();
                                        }
                                    }
                                    //}
                                }
                            }
                        }
                    }
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/CustomReportNew.aspx?PageType=26");
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
        protected void AddTemplate_Click(object sender, EventArgs e)
        {

            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();
            string strssqlb = "";
            int retVal = 0;
            int grid12 = 0;
            string strSQLQuery = "";
            string strQueryCheck = "";
            int templateID = 0;
            string strEmployeeVal = "0";
            int templateCount = 0;
            string strQueryCount = "";
            strSQLQuery = "Select ISNULL(max(TemplateID),0) as TemplateID from CustomTemplates Where CategoryId=" + ddlCommon.SelectedValue + "";
            templateID = DataAccess.ExecuteScalar(strSQLQuery);

            if (templateID == 0)
            {
                templateID = 0 + 1;
            }
            else
            {
                templateID = templateID + 1;
            }
            //strQueryCount = "Select ISNULL(max(TemplateID),0) as TemplateCount from CustomTemplates where  CategoryId=" + ddlCommon.SelectedValue + "";
            //templateCount = DataAccess.ExecuteScalar(strQueryCount);

            strQueryCheck = "Select * from CustomTemplates where TemplateName='" + txtTemplateName.Text + "' AND CategoryId=" + ddlCommon.SelectedValue + "";
            dsTable = DataAccess.FetchRS(CommandType.Text, strQueryCheck, null);
            dtTable = dsTable.Tables[0];

            if (dtTable.Rows.Count == 0)
            {
                if (grdSelectedList.Items.Count <= 50)
                {
                    foreach (GridItem item in grdSelectedList.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;

                            int ColumnID = Convert.ToInt32(this.grdSelectedList.Items[dataItem.ItemIndex]["ID"].Text.ToString());
                            string AliasName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["AliasName"].Text.ToString());
                            string RelationName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["RelationName"].Text.ToString());
                            string CategoryName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["CategoryName"].Text.ToString());
                            string replacedRelation = RelationName.Replace("'", "''");
                            int TableID = Convert.ToInt32(this.grdSelectedList.Items[dataItem.ItemIndex]["TableId"].Text.ToString());

                            strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + templateID + ",'" + txtTemplateName.Text + "'," + ColumnID + "," + ddlCommon.SelectedValue + ",'" + ddlCommon.SelectedItem.Text + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                            retVal += DataAccess.ExecuteStoreProc(strssqlb);

                        }
                    }
                }
                else
                {
                    SelectedItems.Clear();
                    grdSelectedList.DataSource = SelectedItems;
                    grdSelectedList.Rebind();

                    ShowMessageBox("You can add less than 50 columns only");
                }

            }
            else
            {
                SelectedItems.Clear();
                grdSelectedList.DataSource = SelectedItems;
                grdSelectedList.Rebind();
                ShowMessageBox("Template name is already exist");
            }



            if (retVal > 0)
            {
                SelectedItems.Clear();
                grdSelectedList.Rebind();
                grdSelectedList.Enabled = true;
                string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE TableID!=0 UNION SELECT  1 as ID,'All Deductions','All Deductions' AS RELATION,4,company_id,'Deduction' AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  2 as ID,'All Additions','All Additions' AS RELATION,3,company_id,'Addition' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION in ('General','Variable') UNION SELECT  3 as ID,'All Claims','All Claims' AS RELATION,5,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id, 'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION ='Claim' ORDER BY ALIAS_NAME";
                object obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                SelectingItems = (IList<Items>)obj;
                grdSelectingList.DataSource = SelectingItems;
                grdSelectingList.Rebind();
                ShowMessageBox("Template Items Added Successfully");
                ddlCommon.SelectedValue = "-1";

                txtTemplateName.Text = string.Empty;
            }
            else
            {
                string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE TableID!=0 UNION SELECT  1 as ID,'All Deductions','All Deductions' AS RELATION,4,company_id,'Deduction' AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  2 as ID,'All Additions','All Additions' AS RELATION,3,company_id,'Addition' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION in ('General','Variable') UNION SELECT  3 as ID,'All Claims','All Claims' AS RELATION,5,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id, 'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION ='Claim' ORDER BY ALIAS_NAME";
                object obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                SelectingItems = (IList<Items>)obj;
                grdSelectingList.DataSource = SelectingItems;
                grdSelectingList.Rebind();
                ShowMessageBox("Template is not added");
            }
            //grdSelectedList.Enabled = true;
            //dlCustomTemplates.DataSourceID = "SqlDataSource8";
            //dlCustomTemplates.DataBind();          
            btnCreate.Enabled = false;
            dlCustomTemplates.SelectedValue = "-1";

            Button10.Enabled = false;
            Button12.Enabled = false;

            grdSelectedList.Enabled = false;
            txtTemplateName.Enabled = false;
            ddlCommon.Enabled = false;
            ddlCommon.SelectedValue = "-1";
            btnCreate.Enabled = false;
        }
        protected void UpdateTemplate_Click(object sender, EventArgs e)
        {
            string strssqlb = "";
            int retVal = 0;
            int grid12 = 0;
            string strSQLQuery = "";
            int templateID = 0;
            int selectedValue = 0;
            string strEmployeeVal = "0";
            int countValue = 0;
            DataTable dtTable = new DataTable();
            DataSet dsTable = new DataSet();


            foreach (GridItem item in grdSelectedList.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    int ColumnID = Convert.ToInt32(this.grdSelectedList.Items[dataItem.ItemIndex]["ID"].Text.ToString());
                    string AliasName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["AliasName"].Text.ToString());
                    string RelationName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["RelationName"].Text.ToString());
                    string CategoryName = Convert.ToString(this.grdSelectedList.Items[dataItem.ItemIndex]["CategoryName"].Text.ToString());

                    string replacedRelation = RelationName.Replace("'", "''");
                    int TableID = Convert.ToInt32(this.grdSelectedList.Items[dataItem.ItemIndex]["TableId"].Text.ToString());
                    strSQLQuery = "Select * from CustomTemplates where TemplateID=" + dlCustomTemplates.SelectedValue + "AND ColumnID=" + ColumnID + " AND CategoryId=" + ddlSelectCategory.SelectedValue + "";
                    dsTable = DataAccess.FetchRS(CommandType.Text, strSQLQuery, null);
                    dtTable = dsTable.Tables[0];

                    if (dtTable.Rows.Count > 0) // cross checking with dropdownlistitem to gridboundcolumn text
                    {

                        strssqlb = "UPDATE CustomTemplates SET TemplateID=" + Convert.ToInt32(dlCustomTemplates.SelectedValue) + ",TemplateName='" + txtTemplateName.Text + "',ColumnID=" + ColumnID + ",CategoryId=" + ddlCommon.SelectedValue + ",CategoryName='" + ddlCommon.SelectedItem.Text + "',MainCategory='" + CategoryName + "',Company_Id=" + Utility.ToInteger(Session["Compid"]) + ",ALIAS_NAME='" + AliasName.Replace(" ", "") + "',RELATION='" + replacedRelation + "',TableID=" + TableID + ",AddedDate=GETDATE() WHERE TemplateID=" + Convert.ToInt32(dlCustomTemplates.SelectedValue) + " AND ColumnID=" + ColumnID + " AND CategoryId=" + ddlSelectCategory.SelectedValue + "";
                        retVal += DataAccess.ExecuteStoreProc(strssqlb);

                    }
                    else
                    {
                        strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + dlCustomTemplates.SelectedValue + ",'" + txtTemplateName.Text + "'," + ColumnID + "," + ddlCommon.SelectedValue + ",'" + ddlCommon.SelectedItem.Text + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                        retVal += DataAccess.ExecuteStoreProc(strssqlb);
                    }
                }
            }
            if (retVal > 0)
            {
                SelectedItems.Clear();
                grdSelectedList.Rebind();
                grdSelectedList.Enabled = true;
                string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE TableID!=0 UNION SELECT  1 as ID,'All Deductions','All Deductions' AS RELATION,4,company_id,'Deduction' AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  2 as ID,'All Additions','All Additions' AS RELATION,3,company_id,'Addition' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION in ('General','Variable') UNION SELECT  3 as ID,'All Claims','All Claims' AS RELATION,5,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id, 'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION ='Claim' ORDER BY ALIAS_NAME";
                object obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                SelectingItems = (IList<Items>)obj;
                grdSelectingList.DataSource = SelectingItems;
                grdSelectingList.Rebind();
                ShowMessageBox("Template Items Updated Successfully");
                ddlCommon.SelectedValue = "-1";
                txtTemplateName.Text = string.Empty;
            }
            else
            {
                string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE TableID!=0 UNION SELECT  1 as ID,'All Deductions','All Deductions' AS RELATION,4,company_id,'Deduction' AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  2 as ID,'All Additions','All Additions' AS RELATION,3,company_id,'Addition' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION in ('General','Variable') UNION SELECT  3 as ID,'All Claims','All Claims' AS RELATION,5,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id, 'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION ='Claim' ORDER BY ALIAS_NAME";
                object obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                SelectingItems = (IList<Items>)obj;
                grdSelectingList.DataSource = SelectingItems;
                grdSelectingList.Rebind();
                ShowMessageBox("Template is not added");
            }
            //grdSelectedList.Enabled = true;
            //dlCustomTemplates.DataSourceID = "SqlDataSource8";
            //dlCustomTemplates.DataBind();

            grdSelectedList.Enabled = false;
            btnUpdate.Enabled = false;
            Button10.Enabled = false;
            Button12.Enabled = false;
            btnNewTemplate.Enabled = true;
            dlCustomTemplates.SelectedValue = "-1";
            dlCustomTemplates.Enabled = false;
            ddlSelectCategory.SelectedValue = "-1";

        }
        protected void Button10_Click(object sender, System.EventArgs e)
        {
            if (dlCustomTemplates.SelectedValue == "-1")
            {
                txtTemplateName.Text = string.Empty;
                txtTemplateName.Enabled = true;
                ddlCommon.Enabled = true;
                ddlCommon.SelectedValue = "-1";
                btnCreate.Enabled = true;
            }

            grdSelectedList.Enabled = true;
            IList<Items> selectedListItems = SelectedItems;
            IList<Items> selectingListItems = SelectingItems;
            int destinationIndex = -1;
            if (grdSelectingList.SelectedItems.Count <= 50)
            {
                foreach (GridDataItem item in grdSelectingList.SelectedItems)
                {
                    Items tmpItems = GetItem(selectingListItems, (int)item.GetDataKeyValue("ID"));
                    CheckBox chkBoxMain = (CheckBox)item["TemplateSelection"].Controls[0];
                    if (tmpItems != null)
                    {
                        if (tmpItems.AliasName != "All Additions" && tmpItems.AliasName != "All Deductions" && tmpItems.AliasName != "All Claims")
                        {
                            if (!selectedListItems.Equals(tmpItems.AliasName))
                            {
                                if (destinationIndex > -1)
                                {

                                    destinationIndex += 1;

                                    selectedListItems.Insert(destinationIndex, tmpItems);
                                }
                                else
                                {
                                    selectedListItems.Add(tmpItems);
                                }

                            }

                            selectingListItems.Remove(tmpItems);
                        }
                        else if (tmpItems.AliasName == "All Additions" && chkBoxMain.Checked == true)
                        {
                            foreach (GridDataItem itemPaysAdd in grdSelectingList.MasterTableView.Items)
                            {
                                Items tmpItemsPaysAdd = GetItem(selectingListItems, (int)itemPaysAdd.GetDataKeyValue("ID"));
                                CheckBox chkBoxAdd = (CheckBox)itemPaysAdd["TemplateSelection"].Controls[0];
                                if (tmpItemsPaysAdd != null)
                                {

                                    if (tmpItemsPaysAdd.CategoryName == "Addition")
                                    {
                                        if (chkBoxAdd.Checked != true)
                                        {

                                            if (!selectedListItems.Equals(tmpItemsPaysAdd.AliasName))
                                            {
                                                if (destinationIndex > -1)
                                                {

                                                    destinationIndex += 1;

                                                    selectedListItems.Insert(destinationIndex, tmpItemsPaysAdd);
                                                }
                                                else
                                                {
                                                    selectedListItems.Add(tmpItemsPaysAdd);
                                                }

                                            }
                                        }
                                        selectingListItems.Remove(tmpItemsPaysAdd);
                                    }


                                }
                            }
                        }
                        else if (tmpItems.AliasName == "All Deductions" && chkBoxMain.Checked == true)
                        {
                            foreach (GridDataItem itemPaysDeduct in grdSelectingList.MasterTableView.Items)
                            {
                                Items tmpItemsPaysDeduct = GetItem(selectingListItems, (int)itemPaysDeduct.GetDataKeyValue("ID"));
                                CheckBox chkBoxDeduct = (CheckBox)itemPaysDeduct["TemplateSelection"].Controls[0];
                                if (tmpItemsPaysDeduct != null)
                                {


                                    if (tmpItemsPaysDeduct.CategoryName == "Deduction")
                                    {
                                        if (chkBoxDeduct.Checked != true)
                                        {


                                            if (!selectedListItems.Equals(tmpItemsPaysDeduct.AliasName))
                                            {
                                                if (destinationIndex > -1)
                                                {

                                                    destinationIndex += 1;

                                                    selectedListItems.Insert(destinationIndex, tmpItemsPaysDeduct);
                                                }
                                                else
                                                {
                                                    selectedListItems.Add(tmpItemsPaysDeduct);
                                                }

                                            }

                                        }
                                        selectingListItems.Remove(tmpItemsPaysDeduct);
                                    }

                                }
                            }
                        }
                        else if (tmpItems.AliasName == "All Claims" && chkBoxMain.Checked == true)
                        {
                            foreach (GridDataItem itemPaysClaim in grdSelectingList.MasterTableView.Items)
                            {
                                Items tmpItemsPaysClaims = GetItem(selectingListItems, (int)itemPaysClaim.GetDataKeyValue("ID"));
                                CheckBox chkBoxClaim = (CheckBox)itemPaysClaim["TemplateSelection"].Controls[0];
                                if (tmpItemsPaysClaims != null)
                                {

                                    if (tmpItemsPaysClaims.CategoryName == "Claim")
                                    {
                                        if (chkBoxClaim.Checked != true)
                                        {

                                            if (!selectedListItems.Equals(tmpItemsPaysClaims.AliasName))
                                            {
                                                if (destinationIndex > -1)
                                                {

                                                    destinationIndex += 1;

                                                    selectedListItems.Insert(destinationIndex, tmpItemsPaysClaims);
                                                }
                                                else
                                                {
                                                    selectedListItems.Add(tmpItemsPaysClaims);
                                                }

                                            }

                                        }
                                        selectingListItems.Remove(tmpItemsPaysClaims);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ShowMessageBox("You can add less than 50 columns only");
            }

            SelectedItems = selectedListItems;
            SelectingItems = selectingListItems;
            grdSelectingList.Rebind();
            grdSelectedList.Rebind();

        }

        protected void Button12_Click(object sender, System.EventArgs e)
        {
            string strssqlb = "";
            IList<Items> selectedItems = SelectedItems;
            IList<Items> selectingItems = SelectingItems;
            int destinationIndex = -1;
            foreach (GridDataItem item in grdSelectedList.SelectedItems)
            {

                Items tmpItem = GetItem(selectedItems, (int)item.GetDataKeyValue("ID"));

                if (tmpItem != null)
                {
                    selectedItems.Remove(tmpItem);
                    selectingItems.Add(tmpItem);
                    if (dlCustomTemplates.SelectedValue != "-1")
                    {
                        strssqlb = "DELETE FROM CustomTemplates WHERE TemplateID=" + Convert.ToInt32(dlCustomTemplates.SelectedValue) + " AND ColumnID=" + Convert.ToInt32(item.GetDataKeyValue("ID")) + "";
                        DataAccess.ExecuteStoreProc(strssqlb);
                    }

                }

            }

            SelectedItems = selectedItems;
            SelectingItems = selectingItems;
            grdSelectingList.DataSource = SelectingItems;
            grdSelectingList.Rebind();
            grdSelectedList.Rebind();
        }

        protected void RadGrid1_SortCommand(object source, GridSortCommandEventArgs e)
        {
            GridTableView tableView = e.Item.OwnerTableView;
            if (e.SortExpression == "AliasName")
            {
                e.Canceled = true;
                GridSortExpression expression = new GridSortExpression();
                expression.FieldName = "AliasName";
                if (tableView.SortExpressions.Count == 0 || tableView.SortExpressions[0].FieldName != "AliasName")
                {
                    expression.SortOrder = GridSortOrder.Descending;
                }
                else if (tableView.SortExpressions[0].SortOrder == GridSortOrder.Descending)
                {
                    expression.SortOrder = GridSortOrder.Ascending;
                }
                else if (tableView.SortExpressions[0].SortOrder == GridSortOrder.Ascending)
                {
                    expression.SortOrder = GridSortOrder.None;
                }
                grdSelectingList.MasterTableView.SortExpressions.AddSortExpression(expression);
                grdSelectingList.Rebind();

                //tableView.SortExpressions.AddSortExpression(expression);
                //tableView.Rebind();
            }
        }

        protected void RadGrid1_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column is GridBoundColumn)
            {
                e.Column.HeaderText = "Alias Name";
                e.Column.ShowFilterIcon = false;
            }
        }

        protected void RadGrid2_ColumnCreated(object sender, GridColumnCreatedEventArgs e)
        {
            if (e.Column is GridBoundColumn)
            {
                e.Column.HeaderText = "Alias Name";
                e.Column.ShowFilterIcon = false;
            }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlCustomTemplates.SelectedValue != "-1")
            {

                grdSelectedList.Enabled = true;
                btnUpdate.Enabled = true;
                Button10.Enabled = true;
                Button12.Enabled = true;
                btnCreate.Enabled = false;
                string selectSQL = "";
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                selectSQL = "Select ColumnID as ID, ALIAS_NAME,RELATION,TableID,CategoryId,CategoryName,TemplateName from CustomTemplates where CategoryId=" + ddlSelectCategory.SelectedValue + " AND TemplateID=" + dlCustomTemplates.SelectedValue + "";
                dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                dtTable = dsTable.Tables[0];
                object obj = GetItems(DataAccess.FetchRS(CommandType.Text, selectSQL, null));
                SelectedItems = (IList<Items>)obj;
                grdSelectedList.DataSource = SelectedItems;
                grdSelectedList.DataBind();
                ddlCommon.SelectedValue = dtTable.Rows[0]["CategoryId"].ToString();
                txtTemplateName.Text = dtTable.Rows[0]["TemplateName"].ToString();
                string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id ,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE  ID not in(Select Distinct CS.ColumnID From CustomTemplates CS  Where CS.TemplateID=" + dlCustomTemplates.SelectedValue + ") UNION  SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") AND ID not in(Select Distinct CP.ColumnID From CustomTemplates CP  Where CP.TemplateID=" + dlCustomTemplates.SelectedValue + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") AND OPTIONSELECTION in ('General','Variable') AND ID not in(Select Distinct CT.ColumnID From CustomTemplates CT  Where CT.TemplateID=" + dlCustomTemplates.SelectedValue + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") AND OPTIONSELECTION='Claim' AND ID not in(Select Distinct CT.ColumnID From CustomTemplates CT  Where CT.TemplateID=" + dlCustomTemplates.SelectedValue + ")  ORDER BY ALIAS_NAME";

                object obj1 = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                SelectingItems = (IList<Items>)obj1;
                grdSelectingList.DataSource = SelectingItems;
                grdSelectingList.Rebind();

            }
            else
            {
                ShowMessageBox("Please select any one template");
            }

        }
        protected void Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSelectCategory.SelectedValue != "-1")
            {
                dlCustomTemplates.Enabled = true;
                btnNewTemplate.Enabled = false;
                string sql = "Select Distinct TemplateID,TemplateName from CustomTemplates Where CategoryId=" + ddlSelectCategory.SelectedValue + "";
                DataSet dsts = new DataSet();
                dsts = DataAccess.FetchRS(CommandType.Text, sql, null);
                dlCustomTemplates.DataSource = dsts;
                dlCustomTemplates.DataTextField = "TemplateName";
                dlCustomTemplates.DataValueField = "TemplateID";
                dlCustomTemplates.DataBind();
            }
            else
            {
                ShowMessageBox("Please select any one Category");
            }


        }
        protected void btnNewTemplate_Click(object sender, EventArgs e)
        {
            if (((System.Web.UI.WebControls.Button)sender).ID == "btnNewTemplate")
            {

                btnCreate.Enabled = true;
                dlCustomTemplates.SelectedValue = "-1";
                btnUpdate.Enabled = false;
                Button10.Enabled = true;
                Button12.Enabled = true;
                SelectedItems.Clear();
                grdSelectedList.DataSource = SelectedItems;
                grdSelectedList.Rebind();
                grdSelectedList.Enabled = true;
            }
        }

        protected IList<Items> SelectingItems
        {
            get
            {
                try
                {
                    string sqlItemsQuery = "SELECT ID as ID, ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION, TableID," + Utility.ToInteger(Session["Compid"]) + " as Company_Id,CASE WHEN TableID=1 THEN 'Employeee'  WHEN TableID=2 THEN 'Payroll'  ELSE '' END AS CategoryName FROM TABLEOBJATTRIB WHERE TableID!=0 UNION SELECT  1 as ID,'All Deductions','All Deductions' AS RELATION,4,company_id,'Deduction' AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=4 THEN 'Deduction' ELSE '' END AS CategoryName FROM  ViewDeductionsDed WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  2 as ID,'All Additions','All Additions' AS RELATION,3,company_id,'Addition' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,TableID,company_id,CASE WHEN TableID=3 THEN 'Addition' ELSE '' END AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION in ('General','Variable') UNION SELECT  3 as ID,'All Claims','All Claims' AS RELATION,5,company_id,'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") UNION SELECT  ID as ID ,ALIAS_NAME,ALIAS_NAME AS RELATION,5 AS TableID,company_id, 'Claim' AS CategoryName FROM  ViewAdditionTypesDescAdd WHERE (COMPANY_ID=" + Utility.ToInteger(Session["Compid"]) + ") and OPTIONSELECTION ='Claim' ORDER BY ALIAS_NAME";
                    object obj = Session["SelectingItems"];
                    if (obj == null)
                    {
                        obj = GetItems(DataAccess.FetchRS(CommandType.Text, sqlItemsQuery, null));
                        if (obj != null)
                        {
                            Session["SelectingItems"] = obj;
                        }
                        else
                        {
                            obj = new List<Items>();
                        }
                    }
                    return (IList<Items>)obj;
                }
                catch
                {
                    Session["SelectingItems"] = null;
                }
                return new List<Items>();
            }
            set { Session["SelectingItems"] = value; }
        }

        protected IList<Items> SelectedItems
        {
            get
            {
                try
                {
                    object obj = Session["SelectedItems"];
                    if (obj == null)
                    {
                        Session["SelectedItems"] = obj = new List<Items>();
                    }
                    return (IList<Items>)obj;
                }
                catch
                {
                    Session["SelectedItems"] = null;
                }
                return new List<Items>();
            }
            set { Session["SelectedItems"] = value; }
        }

        protected void grdSelectingList_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelectingList.DataSource = SelectingItems;
        }
        protected void grdSelectedItems_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            grdSelectedList.DataSource = SelectedItems;
        }
        protected IList<Items> GetItems(DataSet dsItems)
        {
            IList<Items> results = new List<Items>();
            DataTable dtItems = dsItems.Tables[0];

            try
            {
                if (dtItems.Rows.Count > 0)
                {
                    for (int i = 0; i < dtItems.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(dtItems.Rows[i]["ID"].ToString());
                        string aliasName = dtItems.Rows[i]["ALIAS_NAME"].ToString();
                        string relationName = dtItems.Rows[i]["RELATION"].ToString();
                        int tableID = Convert.ToInt32(dtItems.Rows[i]["TableID"].ToString());
                        string categoryName = Convert.ToString(dtItems.Rows[i]["CategoryName"].ToString());
                        results.Add(new Items(id, aliasName, categoryName, relationName, tableID));

                    }
                }
            }
            catch
            {
                results.Clear();
            }

            return results;
        }
        protected void grdSelectingList_RowDrop(object sender, GridDragDropEventArgs e)
        {
            grdSelectedList.Enabled = true;
            if (string.IsNullOrEmpty(e.HtmlElement))
            {
                if (e.DraggedItems[0].OwnerGridID == grdSelectingList.ClientID)
                {
                    // items are dragged from pending to shipped grid 
                    if ((e.DestDataItem == null && SelectedItems.Count == 0) ||
                        e.DestDataItem != null && e.DestDataItem.OwnerGridID == grdSelectedList.ClientID)
                    {
                        IList<Items> selectedListItems = SelectedItems;
                        IList<Items> selectingListItems = SelectingItems;
                        int destinationIndex = -1;

                        if (e.DestDataItem != null)
                        {
                            Items items = GetItem(selectedListItems, (int)e.DestDataItem.GetDataKeyValue("ID"));
                            destinationIndex = (items != null) ? selectedListItems.IndexOf(items) : -1;
                        }

                        foreach (GridDataItem draggedItem in e.DraggedItems)
                        {
                            Items tmpItems = GetItem(selectingListItems, (int)draggedItem.GetDataKeyValue("ID"));

                            if (tmpItems != null)
                            {
                                if (destinationIndex > -1)
                                {
                                    if (e.DropPosition == GridItemDropPosition.Below)
                                    {
                                        destinationIndex += 1;
                                    }
                                    selectedListItems.Insert(destinationIndex, tmpItems);
                                }
                                else
                                {
                                    selectedListItems.Add(tmpItems);
                                }

                                selectingListItems.Remove(tmpItems);
                            }
                        }

                        SelectedItems = selectedListItems;
                        SelectingItems = selectingListItems;
                        grdSelectingList.Rebind();
                        grdSelectedList.Rebind();
                    }
                    else if (e.DestDataItem != null && e.DestDataItem.OwnerGridID == grdSelectingList.ClientID)
                    {
                        //reorder items in pending grid
                        IList<Items> selectingItems = SelectingItems;
                        Items Item = GetItem(selectingItems, (int)e.DestDataItem.GetDataKeyValue("ID"));
                        int destinationIndex = selectingItems.IndexOf(Item);

                        if (e.DropPosition == GridItemDropPosition.Above && e.DestDataItem.ItemIndex > e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex -= 1;
                        }
                        if (e.DropPosition == GridItemDropPosition.Below && e.DestDataItem.ItemIndex < e.DraggedItems[0].ItemIndex)
                        {
                            destinationIndex += 1;
                        }

                        List<Items> itemsToMove = new List<Items>();
                        foreach (GridDataItem draggedItem in e.DraggedItems)
                        {
                            Items tmpItem = GetItem(selectingItems, (int)draggedItem.GetDataKeyValue("ID"));
                            if (tmpItem != null)
                                itemsToMove.Add(tmpItem);
                        }

                        foreach (Items itemToMove in itemsToMove)
                        {
                            selectingItems.Remove(itemToMove);
                            selectingItems.Insert(destinationIndex, itemToMove);
                        }
                        SelectingItems = selectingItems;
                        grdSelectingList.Rebind();

                        int destinationItemIndex = destinationIndex - (grdSelectingList.PageSize * grdSelectingList.CurrentPageIndex);
                        e.DestinationTableView.Items[destinationItemIndex].Selected = true;
                    }
                }
            }
        }

        protected void RadGrid1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridDataItem dataItem = grdSelectingList.SelectedItems[0] as GridDataItem;
            GridDataItem dataItem = (GridDataItem)grdSelectingList.SelectedItems[grdSelectingList.SelectedItems.Count - 1];
            CheckBox checkBoxSelction = (CheckBox)dataItem["TemplateSelection"].Controls[0];
            if (dataItem != null)
            {
                TableCell cell = (TableCell)dataItem["AliasName"];
                if (checkBoxSelction.Checked == true)
                {
                    if (cell.Text == "All Additions")
                    {
                        foreach (GridDataItem item in grdSelectingList.MasterTableView.Items)
                        {
                            if (item != null)
                            {
                                TableCell cellAdd = (TableCell)item["CategoryName"];
                                CheckBox checkBoxAdd = (CheckBox)item["TemplateSelection"].Controls[0];
                                if (cellAdd.Text == "Addition" && checkBoxAdd.Checked == false)
                                {

                                    item.Enabled = false;
                                    if (checkBoxAdd.Checked == true)
                                    {
                                        checkBoxAdd.Checked = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (cell.Text == "All Deductions")
                    {
                        foreach (GridDataItem item in grdSelectingList.MasterTableView.Items)
                        {
                            if (item != null)
                            {
                                TableCell cellDed = (TableCell)item["CategoryName"];
                                CheckBox checkBoxDed = (CheckBox)item["TemplateSelection"].Controls[0];
                                if (cellDed.Text == "Deduction" && checkBoxDed.Checked == false)
                                {

                                    item.Enabled = false;
                                    if (checkBoxDed.Checked == true)
                                    {
                                        checkBoxDed.Checked = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (cell.Text == "All Claims")
                    {
                        foreach (GridDataItem item in grdSelectingList.MasterTableView.Items)
                        {
                            if (item != null)
                            {
                                TableCell cellClaim = (TableCell)item["CategoryName"];
                                CheckBox checkBoxClaims = (CheckBox)item["TemplateSelection"].Controls[0];
                                if (cellClaim.Text == "Claim" && checkBoxClaims.Checked == false)
                                {
                                    item.Enabled = false;
                                    if (checkBoxClaims.Checked == true)
                                    {
                                        checkBoxClaims.Checked = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static Items GetItem(IEnumerable<Items> itemsToSearchIn, int Id)
        {
            foreach (Items item in itemsToSearchIn)
            {
                if (item.ID == Id)
                {
                    return item;
                }
            }
            return null;
        }

        protected void grdSelectedItems_RowDrop(object sender, GridDragDropEventArgs e)
        {
            grdSelectedList.Enabled = true;
            if (!string.IsNullOrEmpty(e.HtmlElement) && e.HtmlElement == "trashCan")
            {
                IList<Items> selectedItems = SelectedItems;
                bool deleted = false;
                foreach (GridDataItem draggedItem in e.DraggedItems)
                {
                    Items tmpItem = GetItem(selectedItems, (int)draggedItem.GetDataKeyValue("ID"));

                    if (tmpItem != null)
                    {
                        selectedItems.Remove(tmpItem);
                        deleted = true;
                    }
                }
                if (deleted)
                {
                    //msg.Visible = true;
                }
                SelectedItems = selectedItems;
                grdSelectedList.Rebind();
            }
        }
        string Additionpay = "", Additionpaysum = "";
        protected void GeneratePayroll_Click(object sender, EventArgs e)
        {
            //check Manual employee contrubution
            string sql_c = "select * from dbo.AdditionPay where Company_id='" + comp_id + "'";
            SqlDataReader dr_c = DataAccess.ExecuteReader(CommandType.Text, sql_c, null);
            if (!dr_c.HasRows)
            {
                #region old code-before manual employee contribution

                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string sqlQuery = "";
                string strEmployee = "0";
                //string sqlSelect = "SELECT isnull(emp_name,'')+' '+isnull(emp_lname,'') as [Full_Name],DeptName as [Department],convert(nvarchar(10),start_period,103) As [Start_Period],convert(nvarchar(10),end_period,103) As [End_Period], ";
                string sqlSelect = "SELECT (select time_card_no from employee where emp_code=P.emp_code) time_card_no,(select emp_code from employee where emp_code=P.emp_code) EmpID,isnull(emp_name,'')+' '+isnull(emp_lname,'') as [Full_Name],DeptName as [Department],convert(nvarchar(10),start_period,103) As [Start_Period],convert(nvarchar(10),end_period,103) As [End_Period], ";

                //new
                sqlSelect = sqlSelect + "(select Businessunit from dbo.Cost_BusinessUnit where Bid=(select cost_Businessunit from employee where emp_code=P.emp_code)) BusinessUnit,";
                sqlSelect = sqlSelect + "(select Businessunit from dbo.Cost_Region where Bid=(select cost_Region from employee where emp_code=P.emp_code)) Region,";
                sqlSelect = sqlSelect + "(select Businessunit from dbo.Cost_Ccategory where Bid=(select cost_Category from employee where emp_code=P.emp_code)) Category,";

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
                            // UPDATED BY SU MON
                            if (dataItem.Cells[5].Text.ToString().Trim() == "basic_pay" || dataItem.Cells[5].Text.ToString().Trim() == "NetPay")
                            {
                                sqlSelect = sqlSelect + "  Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey')," + dataItem.Cells[5].Text.ToString().Trim() + "))) As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";

                                sqlGroup = sqlGroup + " " + dataItem.Cells[5].Text.ToString().Trim() + ",";
                            }
                            else if (dataItem.Cells[5].Text.ToString().Trim() == "B_P_Con")
                            {
                                if (multiCurr == 1)
                                {
                                    sqlSelect = sqlSelect + " '' As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                            }
                            else if (dataItem.Cells[5].Text.ToString().Trim() == "B_P_Cur")
                            {
                                if (multiCurr == 1)
                                {
                                    sqlSelect = sqlSelect + " '' As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                            } // END UPDATE
                            else
                            {
                                //sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + " As [" + dataItem.Cells[3].Text.ToString().Trim() + "] ,";
                                sqlSelect = sqlSelect + " " + dataItem.Cells[5].Text.ToString().Trim() + " As [" + dataItem.Cells[4].Text.ToString().Trim() + "] ,";
                                sqlGroup = sqlGroup + " " + dataItem.Cells[5].Text.ToString().Trim() + ",";
                            }
                        }
                    }
                }
                if (dtp1.SelectedDate.HasValue)
                {
                    if (dtp2.SelectedDate.HasValue)
                    {
                        sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                        sqlGroup = sqlGroup + "EMP_NAME,EMP_lname,DeptName,start_period,end_period,created_on ";
                        // string startdate = Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        // string enddate = Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        //---murugan
                        DateTime sd = new DateTime(Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).Year, Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).Month, 1);
                        int days = DateTime.DaysInMonth(Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).Year, Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).Month);


                        string startdate = sd.ToString("dd/MM/yyyy", format);

                        string enddate = days.ToString() + "/" + Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).Month + "/" + Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).Year;


                        sqlSelect = sqlSelect + " from PayRollView1 P where emp_code in (" + strEmployee + ")";
                        sqlSelect = sqlSelect + " and Convert(Datetime,start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                        sqlSelect = sqlSelect + " and Convert(Datetime,end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                        sqlSelect = sqlSelect + " And STATUS in ('G') ";
                        sqlSelect = sqlSelect + " " + sqlGroup + ",emp_code  ORDER BY EMP_NAME,DeptName,start_period";
                        int currencyID = 1;
                        string currency = "SGD";

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {
                                DataSet rptDs;
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                                if (multiCurr == 1)
                                {
                                    int emp_code = 0;
                                    for (int i = 0; i < rptDs.Tables[0].Rows.Count; i++)
                                    {
                                        string StrSQL = "Select emp_code from employee where emp_code='" + rptDs.Tables[0].Rows[i]["EmpID"].ToString() + "'";
                                        DataTable dtEmp = DataAccess.FetchRS(CommandType.Text, StrSQL, null).Tables[0];
                                        emp_code = int.Parse(dtEmp.Rows[0][0].ToString());

                                        string strCurrency = "select Top 1 CurrencyID,Currency from EmployeePayHistory EH " +
                                                            " INNER join currency C on  EH.CurrencyID = C.Id INNER JOIN exchangeRate er on er.Currency_id=EH.CurrencyID where emp_id=" + emp_code + " order by EH.id desc";


                                        DataTable dtCurrency = DataAccess.FetchRS(CommandType.Text, strCurrency, null).Tables[0];
                                        if (dtCurrency.Rows.Count > 0)
                                        {
                                            currencyID = int.Parse(dtCurrency.Rows[0][0].ToString());
                                            currency = dtCurrency.Rows[0][1].ToString();
                                        }
                                        string strSQLRate = "select Top 1 isnull(Rate,1) from exchangeRate " +
                                                            " where [Date] <= Convert(Datetime,(select Top 1 created_on from PayRollView1 where emp_code=" + emp_code + " and " +
                                                            " Convert(Datetime,start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) " +
                                                            " and Convert(Datetime,end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) And STATUS in ('G') order by created_on desc),103) " +
                                                            "and currency_id=" + currencyID + " order by ID";
                                        DataTable dtRate = DataAccess.FetchRS(CommandType.Text, strSQLRate, null).Tables[0];
                                        if (dtRate.Rows.Count == 0)
                                        {
                                            strSQLRate = " select Top 1 isnull(Rate,1) from exchangeRate  where currency_id=" + currencyID + " order by ID";
                                            dtRate = DataAccess.FetchRS(CommandType.Text, strSQLRate, null).Tables[0];
                                        }
                                        //  double rate = Convert.ToDouble(dtRate.Rows[0][0].ToString());


                                        if (currency != "SGD")
                                        {
                                            double basic_Conversion = Math.Round(Convert.ToDouble(rptDs.Tables[0].Rows[i]["Basic Pay"].ToString()), 2);

                                            string strBasic = "select Top 1 ISNULL(convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),PayRate)),0) As [Basic Pay]" +
                                                              " from EmployeePayHistory where emp_id=" + emp_code + " order by ID desc";
                                            DataTable dtBasic = DataAccess.FetchRS(CommandType.Text, strBasic, null).Tables[0];
                                            double basic = Convert.ToDouble(dtBasic.Rows[0][0].ToString());

                                            try
                                            {
                                                rptDs.Tables[0].Rows[i]["Basic_Pay_Conversion"] = basic_Conversion.ToString();
                                            }
                                            catch (Exception ex) { }
                                            try
                                            {
                                                rptDs.Tables[0].Rows[i]["Basic Pay"] = Math.Round(basic);
                                            }
                                            catch (Exception ex) { }


                                        }
                                        try
                                        {
                                            rptDs.Tables[0].Rows[i]["Basic_Pay_Currency"] = currency;
                                        }
                                        catch (Exception ex) { }

                                    }
                                }
                                Session["rptDs"] = rptDs;
                                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");
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

                #endregion
            }
            else
            {
                #region New code- with ManualEmployeeContribution
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string sqlQuery = "";
                string strEmployee = "0";
                string sqlSelect = " select * from  (  SELECT emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') as [Full_Name],DeptName as [Department],convert(nvarchar(10),start_period,103) As [Start_Period],convert(nvarchar(10),end_period,103) As [End_Period], ";
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
                        DataTable dt = new DataTable();
                        dt = (DataTable)RadGrid4.DataSource;

                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            grid2++;


                            // UPDATED BY SU MON
                            if (dataItem.Cells[5].Text.ToString().Trim() == "basic_pay" || dataItem.Cells[5].Text.ToString().Trim() == "NetPay")
                            {

                                sqlSelect = sqlSelect + "  Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey')," + dataItem.Cells[5].Text.ToString().Trim() + "))) As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";


                                sqlGroup = sqlGroup + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                            }
                            else if (dataItem.Cells[5].Text.ToString().Trim() == "B_P_Con")
                            {
                                if (multiCurr == 1)
                                {
                                    sqlSelect = sqlSelect + " '' As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                            }
                            else if (dataItem.Cells[5].Text.ToString().Trim() == "B_P_Cur")
                            {
                                if (multiCurr == 1)
                                {
                                    sqlSelect = sqlSelect + " '' As [" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                                }
                            } // END UPDATE

                            //if (dataItem.Cells[4].Text.ToString().Trim() == "basic_pay" || dataItem.Cells[4].Text.ToString().Trim() == "NetPay")
                            //{
                            //    sqlSelect = sqlSelect + "  Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey')," + dataItem.Cells[4].Text.ToString().Trim() + "))) As [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                            //    sqlGroup = sqlGroup + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                            //}
                            else if (dataItem.Cells[5].Text == "AdditionPay")
                            {
                                //Additionpay = Additionpay + " " + dataItem.Cells[3].Text.ToString().Trim() + " As [" + dataItem.Cells[3].Text.ToString().Trim() + "] ,";
                                Additionpay = Additionpay + " " + dataItem.Cells[4].Text.ToString().Trim() + ",";
                                Additionpaysum = Additionpaysum + " sum(" + dataItem.Cells[4].Text.ToString().Trim() + ") as " + dataItem.Cells[4].Text.ToString().Trim() + " ,";
                            }
                            else
                            {
                                //sqlSelect = sqlSelect + " " + dataItem.Cells[4].Text.ToString().Trim() + " As [" + dataItem.Cells[3].Text.ToString().Trim() + "] ,";
                                sqlSelect = sqlSelect + " " + dataItem.Cells[5].Text.ToString().Trim() + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                                sqlGroup = sqlGroup + " " + dataItem.Cells[5].Text.ToString().Trim() + ",";
                            }
                        }
                    }
                }
                if (dtp1.SelectedDate.HasValue)
                {
                    if (dtp2.SelectedDate.HasValue)
                    {
                        sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                        sqlGroup = sqlGroup + "EMP_NAME,EMP_lname,DeptName,start_period,end_period ";
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

                        //sqlSelect = sqlSelect + " from PayRollView  where emp_code in (" + strEmployee + ")";
                        sqlSelect = sqlSelect + " from PayRollView1  where emp_code in (" + strEmployee + ")";
                        sqlSelect = sqlSelect + " and Convert(Datetime,start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                        sqlSelect = sqlSelect + " and Convert(Datetime,end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)";
                        sqlSelect = sqlSelect + " And STATUS in ('G') ";
                        //  sqlSelect = sqlSelect + " " + sqlGroup + "  ORDER BY EMP_NAME,DeptName,start_period";
                        if (Additionpaysum == "") // Updated by Su Mon to solve World Sport Isssue
                        {
                            sqlSelect = sqlSelect + " And STATUS in ('G'))A order by A.emp_code ";
                        }
                        else
                        {
                            sqlSelect = sqlSelect + " And STATUS in ('G'))A  left join  (   select Emp_code,startdate,enddate,company_id," + Additionpaysum.Remove(Additionpaysum.Length - 1, 1) + " ";
                            sqlSelect = sqlSelect + " from ( select * from AdditionPay AP  inner join APCategory AC on AP.APType=APcatId ) T PIVOT (SUM(Amount) FOR ApCategory IN (" + Additionpay.Remove(Additionpay.Length - 1, 1) + ")) AS pvt";
                            sqlSelect = sqlSelect + " where emp_code in (" + strEmployee + ") and  Convert(Datetime,startdate,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)  and Convert(Datetime,enddate,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103)   and company_id='" + comp_id + "'   group by Emp_code,startdate,enddate,company_id ";
                            sqlSelect = sqlSelect + " ) B on A.emp_code=B.emp_code  and A.Start_Period=B.startdate   order by A.emp_code ";
                        }

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {
                                DataSet rptDs;
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                if (multiCurr == 1)
                                {
                                    int emp_code = 0;
                                    for (int i = 0; i < rptDs.Tables[0].Rows.Count; i++)
                                    {
                                        string StrSQL = "Select emp_code from employee where time_card_no='" + rptDs.Tables[0].Rows[i][0].ToString() + "'";
                                        try
                                        {

                                            DataTable dtEmp = DataAccess.FetchRS(CommandType.Text, StrSQL, null).Tables[0];
                                            emp_code = int.Parse(dtEmp.Rows[0][0].ToString());
                                        }
                                        catch (Exception ex)  // Updated by Su Mon to solve World Sport Isssue
                                        {
                                            emp_code = int.Parse(rptDs.Tables[0].Rows[i][0].ToString());
                                        }


                                        string strCurrency = "select Top 1 CurrencyID,Currency from EmployeePayHistory EH " +
                                                            " INNER join currency C on  EH.CurrencyID = C.Id INNER JOIN exchangeRate er on er.Currency_id=EH.CurrencyID where emp_id=" + emp_code + " order by EH.id desc";

                                        DataTable dtCurrency = DataAccess.FetchRS(CommandType.Text, strCurrency, null).Tables[0];

                                        string strSQLRate = "select Top 1 isnull(Rate,1) from exchangeRate " +
                                                            " where [Date] <= Convert(Datetime,(select created_on from PayRollView1 where emp_code=" + emp_code + " and " +
                                                            " Convert(Datetime,start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) " +
                                                            " and Convert(Datetime,end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) And STATUS in ('G')),103) " +
                                                            "and currency_id=" + int.Parse(dtCurrency.Rows[0][0].ToString()) + " order by ID";
                                        DataTable dtRate = DataAccess.FetchRS(CommandType.Text, strSQLRate, null).Tables[0];
                                        double rate = Convert.ToDouble(dtRate.Rows[0][0].ToString());


                                        if (dtCurrency.Rows[0][1].ToString().ToUpper() != "SGD")
                                        {
                                            double basic_Conversion = Math.Round(Convert.ToDouble(rptDs.Tables[0].Rows[i]["Basic Pay"].ToString()), 2);

                                            string strBasic = "select Top 1 ISNULL(convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),PayRate)),0) As [Basic Pay]" +
                                                              " from EmployeePayHistory where emp_id=" + emp_code + " order by ID desc";
                                            DataTable dtBasic = DataAccess.FetchRS(CommandType.Text, strBasic, null).Tables[0];
                                            double basic = Convert.ToDouble(dtBasic.Rows[0][0].ToString());

                                            try
                                            {
                                                rptDs.Tables[0].Rows[i]["Basic_Pay_Conversion"] = basic_Conversion.ToString();
                                            }
                                            catch (Exception ex) { }
                                            try
                                            {
                                                rptDs.Tables[0].Rows[i]["Basic Pay"] = Math.Round(basic);
                                            }
                                            catch (Exception ex) { }


                                        }
                                        try
                                        {
                                            rptDs.Tables[0].Rows[i]["Basic_Pay_Currency"] = dtCurrency.Rows[0][1].ToString();
                                        }
                                        catch (Exception ex) { }

                                    }
                                }

                                //remove the Emp_code,Emp_code1,startdate,enddate,company_id
                                rptDs = RemoveUnwantedColumn(rptDs);
                                Session["rptDs"] = rptDs;
                                Response.Redirect("../Reports/CustomReportNew.aspx?PageType=2");
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
                #endregion
            }
        }

        private DataSet RemoveUnwantedColumn(DataSet rptDs)
        {
            rptDs.Tables[0].Columns.Remove("emp_code");
            try
            { // Updated by Su Mon to solve World Sport Isssue
                rptDs.Tables[0].Columns.Remove("Emp_code1");
                rptDs.Tables[0].Columns.Remove("startdate");
                rptDs.Tables[0].Columns.Remove("enddate");
                rptDs.Tables[0].Columns.Remove("company_id");
            }
            catch (Exception ex) { }
            return rptDs;
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
            if (RadDatePicker1.SelectedDate.HasValue)
            {
                if (RadDatePicker2.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(RadDatePicker1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(RadDatePicker2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    string sSQL = "Sp_getpivotclaimsadditions";
                    SqlParameter[] parms = new SqlParameter[8];
                    parms[0] = new SqlParameter("@empcode", strEmployee);
                    parms[1] = new SqlParameter("@trxtype", sqlTrns);
                    parms[2] = new SqlParameter("@startdate", startdate);
                    parms[3] = new SqlParameter("@enddate", enddate);
                    parms[4] = new SqlParameter("@claimtype", Utility.ToInteger(rdAdditions.SelectedItem.Value.ToString()));
                    parms[5] = new SqlParameter("@addtype", "ADD");
                    if (rdAdditions.SelectedItem.Value == "1" || rdAdditions.SelectedItem.Value == "2")
                    {
                        parms[6] = new SqlParameter("@stattype", 'L');
                    }
                    else
                    {
                        parms[6] = new SqlParameter("@stattype", 'U');
                    }
                    parms[7] = new SqlParameter("@claimstatus", 1);

                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/CustomReportNew.aspx?PageType=52");

                            //if (rdDeductions.SelectedItem.Value == "1")
                            //{
                            //    DataSet rptDs = new DataSet();
                            //    rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                            //    Session["rptDs"] = rptDs;
                            //    Response.Redirect("../Reports/CustomReportNew.aspx");
                            //}
                            //else
                            //{
                            //    DataSet rptDs = new DataSet();
                            //    DataTable dt = Pivot(DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms), "Full_Name", "description", "Amount");
                            //    int i = 0;
                            //    foreach (DataRow dr in dt.Rows)
                            //    {
                            //        i = dr["Full_Name"].ToString().IndexOf("-");
                            //        dr["Full_Name"] = dr["Full_Name"].ToString().Substring(0, i - 1);
                            //    }
                            //    rptDs.Tables.Add(dt);
                            //    Session["rptDs"] = rptDs;
                            //    Response.Redirect("../Reports/");
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

        //////////////Payroll Costing Added by Jammu Offcie/////////////////
        protected void GeneratePayrollCosting_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strCategory = "0";
            string sqlSelect = "";
            string sqlSelectPayrollSum = "select ";
            string sqlSelectPayrollSumPercentage = "select ";
            string sqlSelectPayrollSumPercentagebybusinessunit = "select ";
            string sqlSelectPayrollSumPercentagebyRegion = "select ";
            string selectPayroll = "";
            string selectPercentagetype = "";
            string selectFirstLeftjoin = "";
            string selectSecondLeftjoin = "";
            string strAddition = "";
            string strAdditionsum = "";
            string strAdditiontype = "";
            string strDeduction = "";
            string strDeductionsum = "";
            string strDeductiontype = "";
            string selectnamebusinessunit = "";
            string selectnameAddDed = "";
            string selectbidtype = "";
            string selectPayrollSet = "";
            string selectPayrollSetEmployee = "";
            string selectasondatebusinessunit = "";
            int grid1 = 0;
            int grid2 = 0;
            int grid3 = 0;
            int grid4 = 0;
            lblerror.Text = "";
            foreach (GridItem item in RadGridPayrollCostingCategory.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strCategory = strCategory + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }

            foreach (GridItem item in RadGridPayrollCostingPayroll.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        if (dataItem.Cells[5].Text.ToString().Trim() == "basic_pay" || dataItem.Cells[5].Text.ToString().Trim() == "NetPay")
                        {
                            sqlSelect = sqlSelect + " " + ",PayrollSet." + dataItem.Cells[5].Text.ToString().Trim() + " As [" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                            sqlSelectPayrollSum = sqlSelectPayrollSum + " " + "sum(isnull(SUBSTRING(cb.BusinessUnit, 6, 3),100) * (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")))))/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentage = sqlSelectPayrollSumPercentage + " " + "sum(isnull(cbc.Percentage,100) * (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")))))/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentagebybusinessunit = sqlSelectPayrollSumPercentagebybusinessunit + " " + "sum(isnull(cbb.Percentage,100) * (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")))))/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentagebyRegion = sqlSelectPayrollSumPercentagebybusinessunit + " " + "sum(isnull(cbr.Percentage,100) * (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")))))/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                        }
                        else
                        {
                            sqlSelect = sqlSelect + " " + ",PayrollSet." + dataItem.Cells[5].Text.ToString().Trim() + " As [" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                            sqlSelectPayrollSum = sqlSelectPayrollSum + " " + "sum(isnull(SUBSTRING(cb.BusinessUnit, 6, 3),100) * ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentage = sqlSelectPayrollSumPercentage + " " + "sum(isnull(cbc.Percentage,100) * ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentagebybusinessunit = sqlSelectPayrollSumPercentagebybusinessunit + " " + "sum(isnull(cbb.Percentage,100) * ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                            sqlSelectPayrollSumPercentagebyRegion = sqlSelectPayrollSumPercentagebyRegion + " " + "sum(isnull(cbr.Percentage,100) * ppd." + dataItem.Cells[5].Text.ToString().Trim() + ")/100" + " As [" + dataItem.Cells[5].Text.ToString().Trim() + "] ,";
                        }

                    }
                }
            }

            foreach (GridItem item in RadGridPayrollCostingAdditions.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid3++;
                        strAddition = strAddition + ",deductionadditionset." + "[" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                        strAdditionsum = strAdditionsum + "Additionset." + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                        strAdditiontype = strAdditiontype + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                    }
                }
            }

            foreach (GridItem item in RadGridPayrollCostingDeductions.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid4++;
                        strDeduction = strDeduction + ",deductionadditionset." + "[" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                        strDeductionsum = strDeductionsum + " DeductionSet." + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                        strDeductiontype = strDeductiontype + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                    }
                }
            }
            if (RadioButtonPayrollCosting.SelectedValue == "1")
            {
                selectnamebusinessunit = "cb.BusinessUnit as BusinessUnit";
                selectnameAddDed = "cb.BusinessUnit";
                selectbidtype = "cb.Bid";
                selectPayrollSet = "PayrollSet.BusinessUnit";
                selectasondatebusinessunit = "cbb.AsDate";
                selectPayrollSetEmployee = "PayrollSet.BusinessUnit,PayrollSet.emp_name as Employee";
                selectPayroll = sqlSelectPayrollSumPercentagebybusinessunit;
                selectPercentagetype = "cbb.Percentage,100";
                selectFirstLeftjoin = "left join Cost_ByBusinessUnit cbb on cbb.Emp_code = e.emp_code ";
                selectSecondLeftjoin = "left join Cost_BusinessUnit cb on cb.Bid = cbb.BusinessUnitId ";

            }

            if (RadioButtonPayrollCosting.SelectedValue == "2")
            {
                selectnamebusinessunit = "cr.BusinessUnit as Region";
                selectnameAddDed = "cr.BusinessUnit";
                selectbidtype = "cr.Bid";
                selectPayrollSet = "PayrollSet.Region";
                selectasondatebusinessunit = "cbr.AsDate";
                selectPayrollSetEmployee = "PayrollSet.Region,PayrollSet.emp_name as Employee";
                selectPayroll = sqlSelectPayrollSumPercentagebyRegion;
                selectPercentagetype = "cbr.Percentage,100";
                selectFirstLeftjoin = "left join Cost_ByRegion cbr on cbr.Emp_code = e.emp_code ";
                selectSecondLeftjoin = "left join Cost_Region cr on cr.Bid = cbr.BusinessUnitId ";
            }

            if (RadioButtonPayrollCosting.SelectedValue == "3")
            {
                selectnamebusinessunit = "cc.BusinessUnit as Category";
                selectnameAddDed = "cc.BusinessUnit";
                selectbidtype = "cc.Bid";
                selectPayrollSet = "PayrollSet.Category";
                selectasondatebusinessunit = "cbc.AsDate";
                selectPayrollSetEmployee = "PayrollSet.Category,PayrollSet.emp_name as Employee";
                selectPayroll = sqlSelectPayrollSumPercentage;
                selectPercentagetype = "cbc.Percentage,100";
                selectFirstLeftjoin = "left join Cost_ByCcategory cbc on cbc.Emp_code = e.emp_code ";
                selectSecondLeftjoin = "left join Cost_Ccategory cc on cc.Bid = cbc.BusinessUnitId ";

            }


            if (dtpPayrollCosting1.SelectedDate.HasValue)
            {
                if (dtpPayrollCosting2.SelectedDate.HasValue)
                {
                    if (dtpasondatepercentage.SelectedDate.HasValue)
                    {
                        string startdate = Convert.ToDateTime(dtpPayrollCosting1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        string enddate = Convert.ToDateTime(dtpPayrollCosting2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        string asondatepercentage = Convert.ToDateTime(dtpasondatepercentage.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        if (CheckBoxshowemployees.Checked)
                        {
                            sqlSelect = "Select " + selectPayrollSetEmployee + sqlSelect + strAddition + strDeduction + " from " +
                           "(" + selectPayroll + selectnamebusinessunit + ",e.emp_name from prepare_payroll_detail ppd " +
                           "inner join employee e on e.emp_code = ppd.emp_id " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                           "where ppd.status='G' and Convert(Datetime,pphd.start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime,pphd.end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") group by " + selectnameAddDed + ",e.emp_name) as PayrollSet " +
                           "full outer join " +
                           "(select" + strDeductionsum + strAdditionsum + "Additionset.Category,Additionset.emp_name  from " +
                           "(SELECT * " +
                           "FROM " +
                           "(SELECT " + selectnameAddDed + " as Category,e.emp_name,sum(isnull(" + selectPercentagetype + ") *(isnull(ed.trx_amount,0)))/100 as DeductionAmount,vd.description as Deductiontype  from emp_deductions ed " +
                           "inner join employee e on e.emp_code = ed.emp_code " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join ViewDeductions vd on vd.id = ed.trx_type " +
                           "where ed.status = 'L' and Convert(Datetime,ed.trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") " +
                           "group by " + selectnameAddDed + " ,vd.description,e.emp_name " +
                           ") as s " +
                           "PIVOT " +
                           "( " +
                           "SUM(DeductionAmount) " +
                           "FOR Deductiontype IN (" + strDeductiontype + "[deduction]) " +
                           ")AS pvt) as DeductionSet " +
                           "full outer join " +
                           "(SELECT * " +
                           "FROM (SELECT " + selectnameAddDed + " as Category,e.emp_name,sum(isnull(" + selectPercentagetype + ") *(isnull(ea.trx_amount,0)))/100 as AdditionAmount,vat.description as AdditionType  from emp_additions ea " +
                           "inner join employee e on e.emp_code = ea.emp_code " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join ViewAdditionTypesDesc vat on vat.id = ea.trx_type " +
                           "where ea.status = 'L' and Convert(Datetime,ea.trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") " +
                           "group by " + selectnameAddDed + ",vat.description,e.emp_name " +
                           ") as s " +
                           "PIVOT " +
                           "( " +
                           "SUM(AdditionAmount) " +
                           "FOR AdditionType IN (" + strAdditiontype + "[addition]) " +
                           ")AS pvt) as Additionset " +
                           "on DeductionSet.emp_name =  Additionset.emp_name) as deductionadditionset " +
                           "on deductionadditionset.emp_name = PayrollSet.emp_name and deductionadditionset.Category = " + selectPayrollSet + ";";
                        }
                        else
                        {
                            sqlSelect = "Select " + selectPayrollSet + sqlSelect + strAddition + strDeduction + " from " +
                           "(" + selectPayroll + selectnamebusinessunit + " from prepare_payroll_detail ppd " +
                           "inner join employee e on e.emp_code = ppd.emp_id " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                           "where ppd.status='G' and Convert(Datetime,pphd.start_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime,pphd.end_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") group by " + selectnameAddDed + ") as PayrollSet " +
                           "full outer join " +
                           "(select" + strDeductionsum + strAdditionsum + "Additionset.Category from " +
                           "(SELECT * " +
                           "FROM " +
                           "(SELECT " + selectnameAddDed + " as Category,sum(isnull(" + selectPercentagetype + ") *(isnull(ed.trx_amount,0)))/100 as DeductionAmount,vd.description as Deductiontype  from emp_deductions ed " +
                           "inner join employee e on e.emp_code = ed.emp_code " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join ViewDeductions vd on vd.id = ed.trx_type " +
                           "where ed.status = 'L' and Convert(Datetime,ed.trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") " +
                           "group by " + selectnameAddDed + " ,vd.description " +
                           ") as s " +
                           "PIVOT " +
                           "( " +
                           "SUM(DeductionAmount) " +
                           "FOR Deductiontype IN (" + strDeductiontype + "[deduction]) " +
                           ")AS pvt) as DeductionSet " +
                           "full outer join " +
                           "(SELECT * " +
                           "FROM (SELECT " + selectnameAddDed + " as Category,sum(isnull(" + selectPercentagetype + ") *(isnull(ea.trx_amount,0)))/100 as AdditionAmount,vat.description as AdditionType  from emp_additions ea " +
                           "inner join employee e on e.emp_code = ea.emp_code " + selectFirstLeftjoin + selectSecondLeftjoin +
                           "inner join ViewAdditionTypesDesc vat on vat.id = ea.trx_type " +
                           "where ea.status = 'L' and Convert(Datetime,ea.trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and Convert(Datetime," + selectasondatebusinessunit + ",103) = Convert(Datetime,'" + asondatepercentage + "',103) and " + selectbidtype + " in (" + strCategory + ") " +
                           "group by " + selectnameAddDed + ",vat.description " +
                           ") as s " +
                           "PIVOT " +
                           "( " +
                           "SUM(AdditionAmount) " +
                           "FOR AdditionType IN (" + strAdditiontype + "[addition]) " +
                           ")AS pvt) as Additionset " +
                           "on DeductionSet.Category =  Additionset.Category) as deductionadditionset " +
                           "on deductionadditionset.Category = " + selectPayrollSet + ";";
                        }

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {
                                if (grid3 > 0)
                                {
                                    if (grid4 > 0)
                                    {
                                        DataSet rptDs = new DataSet();
                                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                                        Session["rptDs"] = rptDs;
                                        Response.Redirect("../Reports/ReportsNew.aspx?ReportName=PayrollCostingReport&PayrollStartPeriod=" + startdate + "&PayrollEndPeriod=" + enddate + "&PayrollCostingDate=" + asondatepercentage);
                                        //Response.Redirect("../Reports/CustomReportNew.aspx?PageType=57");
                                    }
                                    else
                                    {
                                        lblerror.Text = "Please Select Atleast One Deduction Type";
                                    }
                                }
                                else
                                {
                                    lblerror.Text = "Please Select Atleast One Addition Type";
                                }
                            }
                            else
                            {
                                lblerror.Text = "Please Select Atleast One Payroll Option";
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please Select Atleast One Option";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select Costing Percentage Date";
                    }
                }
                else
                {
                    lblerror.Text = "Please Select End Date";
                }
            }
            else
            {
                lblerror.Text = "Please Select From Date";
            }
        }
        //////////////Payroll Costing ends by Jammu Offcie/////////////////

        //Advance Claim Added By Jammu Office////////////////////////
        protected void GenerateClaimsReport_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGridEmployeeClaimReport.MasterTableView.Items)
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
            foreach (GridItem item in RadGridClaimType.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        sqlTrns = sqlTrns + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (ClaimReportDate1.SelectedDate.HasValue)
            {
                if (ClaimReportDate2.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(ClaimReportDate1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(ClaimReportDate2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string sSQL = "Sp_getpivotclaimsadditions";
                    SqlParameter[] parms = new SqlParameter[8];
                    parms[0] = new SqlParameter("@empcode", strEmployee);
                    parms[1] = new SqlParameter("@trxtype", sqlTrns);
                    parms[2] = new SqlParameter("@startdate", startdate);
                    parms[3] = new SqlParameter("@enddate", enddate);
                    parms[4] = new SqlParameter("@claimtype", Utility.ToInteger(rdClaimReport.SelectedItem.Value.ToString()));
                    parms[5] = new SqlParameter("@addtype", "Claim");
                    if (rdClaimReport.SelectedItem.Value == "1" || rdClaimReport.SelectedItem.Value == "2")
                    {
                        parms[6] = new SqlParameter("@stattype", 'L');
                    }
                    else
                    {
                        parms[6] = new SqlParameter("@stattype", 'U');
                    }
                    int ClaimStatus = ClaimReportdropdown.SelectedIndex + 1;
                    parms[7] = new SqlParameter("@claimstatus", ClaimStatus);
                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/CustomReportNew.aspx?PageType=53");
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

        protected void btnAdvanceClaimReportClaim_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strEmployee = "0";
            string strClaimTypeid = "0";
            string selectClaimPropertiesid = "0";
            string sqlSelect = "";
            string Claimstatus = "";
            string Claimstatusdetail = "";
            string Claimstatussummary = "";
            string selectClaimTypes = "";
            string selectClaimProperties = "[Time_Card_No],[Name],[ClaimType],[Department]";
            string selectClaimTypesDisplay = "[Time_Card_No],[Name],[Department]";
            int grid1 = 0;
            int grid2 = 0;
            int grid3 = 0;
            lblerror.Text = "";

            foreach (GridItem item in RadGridEmployeeClaimReport.MasterTableView.Items)
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

            foreach (GridItem item in RadGridClaimTypeReport.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid2++;
                        strClaimTypeid = strClaimTypeid + "," + dataItem.Cells[3].Text.ToString().Trim();
                        selectClaimTypesDisplay = selectClaimTypesDisplay + "," + " [" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                        selectClaimTypes = selectClaimTypes + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                    }
                }
            }

            foreach (GridItem item in RadGridClaimProperties.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid3++;
                        selectClaimProperties = selectClaimProperties + "," + " [" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                        selectClaimPropertiesid = selectClaimPropertiesid + "," + dataItem.Cells[3].Text.ToString().Trim();
                    }
                }
            }

            if (AdvanceClaimReportdropdown.SelectedValue == "1")
            {
                Claimstatus = "";
                Claimstatusdetail = "";
                Claimstatussummary = "";
            }
            if (AdvanceClaimReportdropdown.SelectedValue == "2")
            {
                Claimstatus = "where ClaimProperties.ClaimStatus = 'Pending'";
                Claimstatusdetail = "and ea.claimstatus = 'Pending'";
                Claimstatussummary = "where ClaimStatus = 'Pending'";
            }
            if (AdvanceClaimReportdropdown.SelectedValue == "3")
            {
                Claimstatus = "where ClaimProperties.ClaimStatus = 'Rejected'";
                Claimstatusdetail = "and ea.claimstatus = 'Rejected'";
                Claimstatussummary = "where ClaimStatus = 'Rejected'";
            }
            if (AdvanceClaimReportdropdown.SelectedValue == "4")
            {
                Claimstatus = "where ClaimProperties.ClaimStatus = 'Approved'";
                Claimstatusdetail = "and ea.claimstatus = 'Approved'";
                Claimstatussummary = "where ClaimStatus = 'Approved'";
            }


            if (ClaimReportDate1.SelectedDate.HasValue)
            {
                if (ClaimReportDate2.SelectedDate.HasValue)
                {
                    string startdate = Convert.ToDateTime(ClaimReportDate1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string enddate = Convert.ToDateTime(ClaimReportDate2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    if (rdAdvanceClaimReport.SelectedItem.Value == "1")
                    {
                        sqlSelect = "select " + selectClaimProperties + " from " +
                                    "(SELECT e.time_card_no as [Time_Card_No], e.emp_name as [Name], t.[Desc] AS[ClaimType],d.DeptName as [Department], c.[Description] AS[Description], r.Currency, " +
                                    "convert(varchar, c.created_on, 103) as [SubmissionDate], convert(varchar, c.IncurredDate, 103) IncurredDate, convert(varchar, (select top 1 modified_on " +
                                    "from emp_additions " +
                                    "where ClaimExt = c.SrNo ), 103)[ApprovedDate] ,(select top 1 Sub_Project_ID from SubProject where id = c.ProjectId )Project ,c.ReceiptNo ,c.Remarks ,(select top 1 claimstatus from emp_additions where ClaimExt= c.SrNo )  [ClaimStatus] ,c.ToatlWithGst as AppliedAmount " +
                                    ",convert(varchar, trx_period, 103) as [TrxDate] ,c.ToatlWithGst as [TotalWithGST] , round(c.ToatlBefGst, 2) as [TotalBeforeGST] ,c.GstAmnt as [GST Amt] ,g.[Desc] as [TaxCode] , " +
                                    "c.ExRate ,c.PayAmount ,c.TransId ,(select Cb.BusinessUnit from Cost_BusinessUnit as Cb where e.Cost_Businessunit = Cb.Bid) AS BusinessUnit ,t.AccountCode as [GL Code] , " +
                                    "( select CR.BusinessUnit from Cost_Region as CR where e.Cost_Region= CR.Bid) AS Region,(select CC.BusinessUnit from Cost_Ccategory as CC " +
                                    "where e.Cost_Category = CC.Bid) AS Category FROM[ClaimsExt] as c " +
                                    "left join GstMaster as g on c.GstCode=g.SrNo " +
                                    "left join Currency as r on r.Id=c.CurrencyID " +
                                    "left join employee e on e.emp_code= c.emp_code " +
                                    "left join Cost_BusinessUnit b on c.Bid=b.Bid " +
                                    "left join additions_types t on c.trx_type = t.id " +
                                    "left join department d on d.id = e.dept_id " +
                                    "where c.emp_code in(" + strEmployee + ") " +
                                    "and Convert(Datetime, trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and t.id in (" + strClaimTypeid + ") and c.CompanyID = " + compid + " ) as ClaimProperties " + Claimstatus + "; ";
                    }
                    if (rdAdvanceClaimReport.SelectedItem.Value == "2")
                    {
                        //sqlSelect = "SELECT e.time_card_no as [Time_Card_No],e.emp_name as [Name],t.[Desc] AS [ClaimType],d.Properties,value as [Description] " +
                        //            "FROM[ClaimsExt] as c " +
                        //            "left join GstMaster as g on c.GstCode = g.SrNo " +
                        //            "left join Currency as r on r.Id = c.CurrencyID " +
                        //            "left join employee e on e.emp_code = c.emp_code " +
                        //            "left join Cost_BusinessUnit b on c.Bid = b.Bid " +
                        //            "left join additions_types t on c.trx_type = t.id " +
                        //            "left join emp_additions ea on ea.ClaimExt = c.SrNo " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT  'Description', (c.[Description]), 1 UNION ALL SELECT 'Currency', (Currency),2 UNION ALL SELECT 'SubmissionDate', (convert(varchar, c.created_on, 103)) ,3 UNION ALL SELECT 'IncurredDate', (convert(varchar, c.IncurredDate, 103)) ,4 " +
                        //            "UNION ALL SELECT 'ApprovedDate', (convert(varchar, (select top 1 modified_on from emp_additions where ClaimExt = c.SrNo ), 103)),5 UNION ALL SELECT 'Project', ((select top 1 Sub_Project_ID from SubProject where id = c.ProjectId )) ,6 " +
                        //            "UNION ALL SELECT 'ReceiptNo', (c.ReceiptNo) ,7 UNION ALL SELECT 'claimstatus', (select claimstatus from emp_additions where ClaimExt= c.SrNo )  [ClaimStatus] ,8 UNION ALL SELECT 'AppliedAmount', ((convert(varchar, c.AppliedAmount))) ,9 UNION ALL SELECT 'TrxDate', (convert(varchar, c.trx_period, 103)) ,10 " +
                        //            "UNION ALL SELECT 'TotalWithGST', (convert(varchar, c.ToatlWithGst)) ,11 UNION ALL SELECT 'TotalBeforeGST', (convert(varchar, c.ToatlBefGst)) ,12 UNION ALL SELECT 'TaxCode', (g.[Desc]) ,13 " +
                        //            "UNION ALL SELECT 'ExRate', (convert(varchar, c.ExRate)) ,14 UNION ALL SELECT 'PayAmount', (convert(varchar, c.PayAmount)) ,15 UNION ALL SELECT 'TransId', (convert(varchar, c.TransId)) ,16  UNION ALL SELECT 'BusinessUnit', (b.BusinessUnit) , 17 " +
                        //            "UNION ALL SELECT 'Category', (select CC.BusinessUnit from Cost_Ccategory as CC where e.Cost_Category = CC.Bid) , 18) " +
                        //            "d([Properties], value,Id)  " +
                        //            "where c.emp_code in(" + strEmployee + ") " +
                        //            "and Convert(Datetime, c.trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and t.id in (" + strClaimTypeid + ")  and d.Id in (" + selectClaimPropertiesid + ") and c.CompanyID = " + compid + " " + Claimstatusdetail + "; ";
                        sqlSelect = "select " + selectClaimTypesDisplay + " from " +
                                    "( " +
                                    "SELECT e.time_card_no as [Time_Card_No], e.emp_name as [Name], t.[Desc] AS[ClaimType], d.DeptName as [Department], c.ToatlWithGst as AppliedAmount ,(select top 1 claimstatus from emp_additions where ClaimExt= c.SrNo )  [ClaimStatus] FROM[ClaimsExt] as c " +
                                    "left join GstMaster as g on c.GstCode= g.SrNo " +
                                    "left join Currency as r on r.Id= c.CurrencyID " +
                                    "left join employee e on e.emp_code= c.emp_code " +
                                    "left join Cost_BusinessUnit b on c.Bid= b.Bid " +
                                    "left join additions_types t on c.trx_type = t.id " +
                                    "left join department d on d.id = e.dept_id " +
                                    "where c.emp_code in(" + strEmployee + ") " +
                                    "and Convert(Datetime, trx_period,103) between Convert(Datetime,'" + startdate + "',103) And Convert(Datetime,'" + enddate + "',103) and t.id in (" + strClaimTypeid + ") and c.CompanyID = " + compid + ") " +
                                    "as s Pivot(sum(AppliedAmount) FOR ClaimType IN (" + selectClaimTypes + "[additional]) " +
                                    ") " +
                                    "AS pvt " +
                                    "" + Claimstatussummary + "; ";

                    }
                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {
                            if (grid3 > 0)
                            {
                                DataSet rptDs = new DataSet();
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                                Session["rptDs"] = rptDs;
                                // Response.Redirect("../Reports/CustomReportNew.aspx?PageType=201");
                                Response.Redirect("../Reports/ReportsNew.aspx?Advanceclaimreport=report");
                            }
                            else
                            {
                                lblerror.Text = "Please Select atleast One Property";
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please Select atleast One Claim Type";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select the Department and  atleast One Employee";
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
        //Advance Claim ends By Jammu Office////////////////////////
        //////////////Payment Variance Report/////////////////
        protected void ddlPaymentVariance_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlStrPaymentVariance = "";
            if (ddlPaymentVariance.SelectedValue == "2")
            {
                sqlStrPaymentVariance = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + comp_id + ")  Order By emp_name";

            }

            else if (ddlPaymentVariance.SelectedValue == "3")
            {
                sqlStrPaymentVariance = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + comp_id + ")  Order By DeptName";

            }
            else if (ddlPaymentVariance.SelectedValue == "4")
            {
                sqlStrPaymentVariance = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + comp_id + ")  Order By Company_name";

            }
            if (sqlStrPaymentVariance.Length > 0)
            {
                RadGridPaymentVariance.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStrPaymentVariance, null);
                RadGridPaymentVariance.DataBind();
                RadGridPaymentVariance.Visible = true;
                RadGridPaymentVariance.MasterTableView.Visible = true;
            }
        }
        protected void GeneratePaymentVariance_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strid = "0";
            string sqlSelect = "";
            string empconditin = "";
            string additionalcolumnname = "";
            string additionalcolumn = "";
            int grid1 = 0;
            lblerror.Text = "";
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
                        strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
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
                                    "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + comp_id + " and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
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
                                    "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + comp_id + " and ed.status ='L' and e.StatusId ='1' " +
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

                        //sqlSelect = "SELECT [Description]," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) - " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end),2) as Variance " +
                        //            "FROM prepare_payroll_detail ppd " +
                        //            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        //            "inner join employee e on e.emp_code = ppd.emp_id " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT 'Basic Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay)))) UNION ALL SELECT 'NH Earining', (NH_e) UNION ALL SELECT 'NH Worked', (NH_wh) UNION ALL SELECT 'OTI Amount', (OT1_e)  UNION ALL SELECT 'OT1 Hours', (OT1_wh) UNION ALL SELECT 'OT2 Amount', (OT2_e) UNION ALL SELECT 'OT2 Hours', (OT2_wh)  UNION ALL SELECT 'DH Earning', (DH_e) UNION ALL SELECT 'Working Days', (Wdays)) " +
                        //            "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                        //            "group by [Description]," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT vat.description as Description," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYear + " then ea.trx_amount else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYear + " then ea.trx_amount else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYear + " then ea.trx_amount else 0 end)- " +
                        //            "SUM(case when Month(Convert(Datetime,ea.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ea.trx_period,103)) = " + SelectedYear + " then ea.trx_amount else 0 end),2) as Variance " +
                        //            "FROM ViewAdditionTypesDesc vat " +
                        //            "inner join emp_additions ea on ea.trx_type = vat.id " +
                        //            "inner join employee e on e.emp_code = ea.emp_code " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + comp_id + " and ea.status ='L' and e.StatusId ='1' " +
                        //            "GROUP BY vat.description," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT [Description]," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) - " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end),2) as Variance " +
                        //            "FROM prepare_payroll_detail ppd " +
                        //            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        //            "inner join employee e on e.emp_code = ppd.emp_id " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT 'Total Additions', (total_additions)) " +
                        //            "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                        //            "group by [Description]," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT vd.description as Description," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYear + " then ed.trx_amount else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYear + " then ed.trx_amount else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + ToMonth + " and Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYear + " then ed.trx_amount else 0 end)- " +
                        //            "SUM(case when Month(Convert(Datetime,ed.trx_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,ed.trx_period,103)) = " + SelectedYear + " then ed.trx_amount else 0 end),2) as Variance " +
                        //            "FROM ViewDeductions vd " +
                        //            "inner join emp_deductions ed on ed.trx_type = vd.id " +
                        //            "inner join employee e on e.emp_code = ed.emp_code " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "where " + empconditin + " in (" + strid + ") and e.Company_Id = " + comp_id + " and ed.status ='L' and e.StatusId ='1' " +
                        //            "GROUP BY vd.description," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT [Description]," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) - " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end),2) as Variance " +
                        //            "FROM prepare_payroll_detail ppd " +
                        //            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        //            "inner join employee e on e.emp_code = ppd.emp_id " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT 'Total Deductions', (total_deductions)) " +
                        //            "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                        //            "group by [Description]," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT [Description]," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) - " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end),2) as Variance " +
                        //            "FROM prepare_payroll_detail ppd " +
                        //            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        //            "inner join employee e on e.emp_code = ppd.emp_id " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT 'Unpaid Amount', (unpaid_leaves_amount) UNION ALL SELECT 'Unpaid Leaves', (unpaid_leaves) ) " +
                        //            "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                        //            "group by [Description]," + additionalcolumnname + " " +
                        //            "union all " +
                        //            "SELECT [Description]," + additionalcolumn + " " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + FromMonthTxt + "" + SelectedYear + "], " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) AS [" + ToMonthTxt + "" + SelectedYear + "], " +
                        //            "round(SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + ToMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + ToMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end) - " +
                        //            "SUM(case when Month(Convert(Datetime,pphd.start_period,103)) = " + FromMonth + " and Month(Convert(Datetime,pphd.end_period,103)) = " + FromMonth + " and  Year(Convert(Datetime,pphd.end_period,103)) = " + SelectedYear + " then value else 0 end),2) as Variance " +
                        //            "FROM prepare_payroll_detail ppd " +
                        //            "inner join prepare_payroll_hdr pphd on pphd.trx_id = ppd.trx_id " +
                        //            "inner join employee e on e.emp_code = ppd.emp_id " +
                        //            "inner join department d on d.id = e.dept_id " +
                        //            "CROSS APPLY " +
                        //            "( " +
                        //            "SELECT 'Total Gross', (total_gross) UNION ALL SELECT 'CPF Gross Amount', (CPFGrossAmount) UNION ALL SELECT 'CPF Amount', (cpfAmount) UNION ALL SELECT 'Employee Cont CPF', (empCPF) UNION ALL SELECT 'Employer Cont CPF', (employerCPF) UNION ALL SELECT 'Fund Amount', (fund_amount) UNION ALL SELECT 'SDL', (SDL) UNION ALL SELECT 'Net Pay', (Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),NetPay)))) UNION ALL SELECT 'FWL', (FWL)) " +
                        //            "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and  " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
                        //            "group by [Description]," + additionalcolumnname + " ;";
                        if (grid1 > 0)
                        {
                            DataSet rptDs = new DataSet();
                            rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/ReportsNew.aspx?Category=" + dpt);
                        }
                        else
                        {
                            lblerror.Text = "Please Select the option and at least one Name";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select the option";
                    }
                }
                else
                {
                    lblerror.Text = "Please Select the Second Year";
                }
            }
            else
            {
                lblerror.Text = "Please Select the First Year";
            }
        }
        //////////////////////////ends Payment Variance Report//////////////////////////
        //WorkFlow Assignment Report//////////////////////
        protected void ddlWorkFlowType_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlStrWorkFlowType = "";
            if (ddlWorkFlowType.SelectedValue == "1")
            {
                sqlStrWorkFlowType = "select distinct(ewf.WorkFlowName) from EmployeeWorkFlow ewf inner join EmployeeWorkFlowLevel ewfl on ewfl.WorkFlowID = ewf.ID where ewfl.FlowType = 1 and ewf.Company_ID = " + comp_id + "";

            }

            else if (ddlWorkFlowType.SelectedValue == "2")
            {
                sqlStrWorkFlowType = "select distinct(ewf.WorkFlowName) from EmployeeWorkFlow ewf inner join EmployeeWorkFlowLevel ewfl on ewfl.WorkFlowID = ewf.ID where ewfl.FlowType = 2 and ewf.Company_ID = " + comp_id + "";

            }
            else if (ddlWorkFlowType.SelectedValue == "3")
            {
                sqlStrWorkFlowType = "select distinct(ewf.WorkFlowName) from EmployeeWorkFlow ewf inner join EmployeeWorkFlowLevel ewfl on ewfl.WorkFlowID = ewf.ID where ewfl.FlowType = 3 and ewf.Company_ID = " + comp_id + "";

            }
            else if (ddlWorkFlowType.SelectedValue == "4")
            {
                sqlStrWorkFlowType = "select distinct(ewf.WorkFlowName) from EmployeeWorkFlow ewf inner join EmployeeWorkFlowLevel ewfl on ewfl.WorkFlowID = ewf.ID where ewfl.FlowType = 4 and ewf.Company_ID = " + comp_id + "";

            }
            if (sqlStrWorkFlowType.Length > 0)
            {
                RadGridWorkFlowName.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStrWorkFlowType, null);
                RadGridWorkFlowName.DataBind();
                RadGridWorkFlowName.Visible = true;
                RadGridWorkFlowName.MasterTableView.Visible = true;
            }
        }
        protected void GenerateWorkFlowAssignment1_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strWorkFlowName = "";
            string strWorkFlowNamedisplay = "WorkFlowGroup";
            string sqlSelect = "";
            int grid1 = 0;
            lblerror.Text = "";
            foreach (GridItem item in RadGridWorkFlowName.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strWorkFlowNamedisplay = strWorkFlowNamedisplay + "," + "[" + dataItem.Cells[4].Text.ToString().Trim() + "]";
                        strWorkFlowName = strWorkFlowName + "[" + dataItem.Cells[4].Text.ToString().Trim() + "],";
                    }
                }
            }

            if (ddlWorkFlowType.SelectedValue != "0")
            {
                string dpt = ddlWorkFlowType.SelectedValue;
                sqlSelect = "select " + strWorkFlowNamedisplay + ",ReportingEmployee from " +
                           "(select e.emp_name as ReportingEmployee, ea.emp_name as ReportingManager,'L' + + Cast(ewfl.RowID as varchar(5)) + ' Approving Manager' as WorkFlowGroup , ewf.WorkFlowName  from employee e " +
                           "inner join EmployeeWorkFlowLevel ewfl on ewfl.ID = e.CliamsupervicerMulitilevel " +
                           "inner join EmployeeAssignedToPayrollGroup eatp on eatp.PayrollGroupID = ewfl.PayRollGroupID " +
                           "inner join employee ea on ea.emp_code = eatp.Emp_ID " +
                           "inner join EmployeeWorkFlow ewf on ewf.ID =ewfl.WorkFlowID " +
                           "where eatp.WorkflowTypeID = " + dpt + " and ewf.Company_ID = " + comp_id + ") as s " +
                           "Pivot " +
                           "( " +
                           "max(ReportingManager) FOR WorkFlowName IN (" + strWorkFlowName + "[additional]) " +
                           ") " +
                           "AS pvt ; ";

                if (grid1 > 0)
                {
                    DataSet rptDs = new DataSet();
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/ReportsNew.aspx?ReportType=WorkFlowAssignmentReport-" + dpt);
                }
                else
                {
                    lblerror.Text = "Please Select at least one WorkFlow Name";
                }
            }
            else
            {
                lblerror.Text = "Please Select the WorkFlow Type";
            }

        }
        protected void GenerateWorkFlowAssignment2_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strWorkFlowName = "";
            string strWorkFlowNamedisplay = "";
            string employeeworkflowlevel = "";
            string sqlSelect = "";
            int grid1 = 0;
            lblerror.Text = "";
            foreach (GridItem item in RadGridWorkFlowName.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strWorkFlowName = strWorkFlowName + "[" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                        strWorkFlowNamedisplay = strWorkFlowNamedisplay + "[" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                    }
                }
            }
            if (ddlWorkFlowType.SelectedValue == "1")
            {
                employeeworkflowlevel = "e.pay_supervisor";
            }
            if (ddlWorkFlowType.SelectedValue == "2")
            {
                employeeworkflowlevel = "e.Leave_supervisor";
            }
            if (ddlWorkFlowType.SelectedValue == "3")
            {
                employeeworkflowlevel = "e.CliamsupervicerMulitilevel";
            }
            if (ddlWorkFlowType.SelectedValue == "4")
            {
                employeeworkflowlevel = "e.TimesupervicerMulitilevel";
            }


            if (ddlWorkFlowType.SelectedValue != "0")
            {
                string dpt = ddlWorkFlowType.SelectedValue;
                sqlSelect = "select " + strWorkFlowNamedisplay + "ReportingEmployee from " +
                               "(select e.emp_name as ReportingEmployee, ea.emp_name + '-L' + Cast(ewfl.RowID as varchar(5)) + ' Approving Manager' as ReportingManager,'L' + + Cast(ewfl.RowID as varchar(5)) + ' Approving Manager' as WorkFlowGroup , ewf.WorkFlowName ,ROW_NUMBER() OVER(PARTITION BY WorkFlowName order by WorkFlowName Desc) as row_no from employee e " +
                               "full join EmployeeWorkFlowLevel ewfl on ewfl.ID = " + employeeworkflowlevel + " " +
                               "inner join EmployeeAssignedToPayrollGroup eatp on eatp.PayrollGroupID = ewfl.PayRollGroupID " +
                               "inner join employee ea on ea.emp_code = eatp.Emp_ID " +
                               "inner join EmployeeWorkFlow ewf on ewf.ID =ewfl.WorkFlowID " +
                               "where eatp.WorkflowTypeID = " + dpt + " and ewf.Company_ID = " + comp_id + ") as s " +
                               "Pivot " +
                               "( " +
                               "max(ReportingManager) FOR WorkFlowName IN (" + strWorkFlowName + "[additional]) " +
                               ") " +
                               "AS pvt ; ";

                if (grid1 > 0)
                {
                    DataSet rptDs = new DataSet();
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/ReportsNew.aspx?ReportType=WorkFlowAssignmentReport-" + dpt);
                }
                else
                {
                    lblerror.Text = "Please Select at least one WorkFlow Name";
                }
            }
            else
            {
                lblerror.Text = "Please Select the WorkFlow Type";
            }

        }
        protected void GenerateWorkFlowAssignment3_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string strWorkFlowName = "";
            string strWorkFlowNamedisplay = "WorkFlowGroup";
            string employeeworkflowlevel = "";
            string sqlSelect = "";
            int grid1 = 0;
            lblerror.Text = "";
            foreach (GridItem item in RadGridWorkFlowName.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        grid1++;
                        strWorkFlowNamedisplay = strWorkFlowNamedisplay + "," + "[" + dataItem.Cells[3].Text.ToString().Trim() + "]";
                        strWorkFlowName = strWorkFlowName + "[" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                    }
                }
            }
            if (ddlWorkFlowType.SelectedValue == "1")
            {
                employeeworkflowlevel = "e.pay_supervisor";
            }
            if (ddlWorkFlowType.SelectedValue == "2")
            {
                employeeworkflowlevel = "e.Leave_supervisor";
            }
            if (ddlWorkFlowType.SelectedValue == "3")
            {
                employeeworkflowlevel = "e.CliamsupervicerMulitilevel";
            }
            if (ddlWorkFlowType.SelectedValue == "4")
            {
                employeeworkflowlevel = "e.TimesupervicerMulitilevel";
            }

            if (ddlWorkFlowType.SelectedValue != "0")
            {
                string dpt = ddlWorkFlowType.SelectedValue;
                sqlSelect = "select " + strWorkFlowNamedisplay + " " +
                            "from " +
                            "( " +
                            "Select 'Approving Manager' as WorkFlowGroup,ea.emp_name + '-L'  + Cast(ewfl.RowID as varchar(5))  as ReportingManager , ewf.WorkFlowName,ROW_NUMBER() OVER(PARTITION BY WorkFlowName order by RowID asc) as row_no from employee ea " +
                            "inner join EmployeeAssignedToPayrollGroup eatp on eatp.Emp_ID = ea.emp_code " +
                            "inner join EmployeeWorkFlowLevel ewfl on ewfl.PayRollGroupID = eatp.PayrollGroupID " +
                            "inner join EmployeeWorkFlow ewf on ewf.ID = ewfl.WorkFlowID " +
                            "where  ewfl.FlowType = " + dpt + " and ewf.Company_ID = " + comp_id + " and eatp.WorkflowTypeID = " + dpt + ") as s  " +
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
                            "where ewfl.FlowType = " + dpt + " and ewf.Company_ID = " + comp_id + " and eatp.WorkflowTypeID = " + dpt + ") as s " +
                            "Pivot  " +
                            "( max(ReportingEmployee) FOR WorkFlowName IN (" + strWorkFlowName + "[additional]) " +
                            ")  " +
                            "AS pvt ;";

                if (grid1 > 0)
                {
                    DataSet rptDs = new DataSet();
                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    Session["rptDs"] = rptDs;
                    Response.Redirect("../Reports/ReportsNew.aspx?ReportType=WorkFlowAssignmentReport-" + dpt);
                }
                else
                {
                    lblerror.Text = "Please Select at least one WorkFlow Name";
                }
            }
            else
            {
                lblerror.Text = "Please Select the WorkFlow Type";
            }

        }

        //ends WorkFlow Assignment Reporrt/////////////////////////
        //Yearly Summary Report/////////////////////////
        protected void ddlYearlySummaryReport_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlYearlySummaryReport = "";
            if (ddlYearlySummaryReport.SelectedValue == "2")
            {
                sqlYearlySummaryReport = " SELECT emp_code as OptionId,emp_name As Category From employee Where emp_code In (Select Distinct emp_code From Employee Where Company_ID=" + comp_id + ")  Order By emp_name";

            }

            else if (ddlYearlySummaryReport.SelectedValue == "3")
            {
                sqlYearlySummaryReport = " SELECT id as OptionId,DeptName As Category From Department Where ID In (Select Distinct Dept_ID From Employee Where Company_ID=" + comp_id + ")  Order By DeptName";

            }
            else if (ddlYearlySummaryReport.SelectedValue == "4")
            {
                sqlYearlySummaryReport = " SELECT Company_Id as OptionId,Company_name As Category From Company Where Company_Id In (Select Distinct Company_Id From Employee Where Company_ID=" + comp_id + ")  Order By Company_name";

            }
            if (sqlYearlySummaryReport.Length > 0)
            {
                RadGridYearlySummaryReport.DataSource = DataAccess.FetchRS(CommandType.Text, sqlYearlySummaryReport, null);
                RadGridYearlySummaryReport.DataBind();
                RadGridYearlySummaryReport.Visible = true;
                RadGridYearlySummaryReport.MasterTableView.Visible = true;
            }
        }
        protected void GenerateYearlySummaryReport_Click(object sender, EventArgs e)
        {
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
            lblerror.Text = "";
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
                strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
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
                strPayrollOptions2 = "c([Description], value) WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ppd.status = 'G' and e.StatusId ='1' " +
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
                            "WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
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
                            "WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ed.status ='L' and e.StatusId ='1' " +
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
                            "WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ea.status ='L' and e.StatusId ='1' and ea.claimstatus = 'Approved' " +
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
                            "WHERE  e.Company_Id = " + comp_id + " and " + empconditin + " in (" + strid + ") and ed.status ='L' and e.StatusId ='1' " +
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
                                Session["rptDs"] = rptDs;
                                Response.Redirect("../Reports/ReportsNew.aspx?CategoryYearlyPayrollSummaryReport=" + dpt);
                            }
                            else
                            {
                                lblerror.Text = "Please Select the option and at least one Name";
                            }
                        }
                        else
                        {
                            lblerror.Text = "Please select months from January to December for the same year";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select To Year";
                    }
                }
                else
                {
                    lblerror.Text = "Please Select From Year";
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
                                Response.Redirect("../Reports/ReportsNew.aspx?CategoryYearlyPayrollSummaryReport=" + dpt);
                            }
                            else
                            {
                                lblerror.Text = "Please Select the option and at least one Name";
                            }
                        }
                        else
                        {
                            lblerror.Text = "To Year Must be greater than From year";
                        }
                    }
                    else
                    {
                        lblerror.Text = "Please Select To Year";
                    }
                }
                else
                {
                    lblerror.Text = "Please Select From Year";
                }
            }
        }
        //ends Yearly Summary Report/////////////////////////
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

        protected void GenerateExpiry_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string sqlQuery = "";
            string strEmployee = "0";
            string sqlSelect = "select ";
            string sqlTrns = "0";
            int grid1 = 0;
            int grid2 = 0; lblerror.Text = "";
            foreach (GridItem item in RadGrid16.MasterTableView.Items)
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
            foreach (GridItem item in RadGrid17.MasterTableView.Items)
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

            if (RadDatePicker4.SelectedDate.HasValue)
            {
                string enddate = Convert.ToDateTime(RadDatePicker4.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                string sqlStr = "Select  (select time_card_no from employee where emp_code=M.emp_code) TimeCardId,(select Deptname from department where id=M.dept_id)Department ,Convert(varchar,E.Testdate ,103)ApplicationDate, Convert(varchar,E.Issuedate ,103)Issuedate,(isnull(M.emp_name,'')+' '+isnull(M.emp_lname,'')) FullName,C.Category_Name, E.CertificateNumber,Convert(varchar,E.ExpiryDate ,103) ExpiryDate From EmployeeCertificate E Inner Join CertificateCategory C On E.CertificateCategoryID = C.ID Inner Join Employee M On E.Emp_ID=M.Emp_Code";
                sqlStr = sqlStr + " Where E.Emp_ID In (" + strEmployee + ") And C.ID in (" + sqlTrns + ") AND M.termination_date is null And Convert(Datetime,E.ExpiryDate,103) <= Convert(Datetime,'" + enddate.ToString() + "',103) order by E.ExpiryDate Asc  ";
                if (grid1 > 0)
                {
                    if (grid2 > 0)
                    {
                        DataSet rptDs = new DataSet();
                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                        Session["rptDs"] = rptDs;
                        Response.Redirect("../Reports/CustomReportNew.aspx?PageType=56");
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

        protected void rdOptionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdOptionList.SelectedValue == "1")
            {
                lblname.Text = "Project Name:";
                RadComboBoxEmpPrj.Visible = false;
                drpSubProjectID.Visible = true;
                drpFilter.Visible = false;

                RadGrid111.Visible = true;
                RadGrid222.Visible = false;
                SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID= 0  And SP.ID= 0  And EM.StatusID=1 Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID= 0) ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID= 0) Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID= 0) Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID= 0) And SOftDelete = 0) D Order By EmpName";
                RadGrid111.Rebind();
                RadGrid222.Visible = false;
                if (rdFrom.SelectedDate.ToString() == "" || rdTo.SelectedDate.ToString() == "")
                {
                    drpSubProjectID.Enabled = false;
                }
                else
                {
                    drpSubProjectID.Enabled = true;
                    drpSubProjectID.SelectedIndex = 0;
                }


            }
            else if (rdOptionList.SelectedValue == "2" || rdOptionList.SelectedValue == "3")
            {
                //lblname.Text = "Employee Name:";
                //RadComboBoxEmpPrj.Visible = true;
                //drpSubProjectID.Visible = false;
                //RadGrid222.Visible = false;
                //RadGrid111.Visible = true;

                lblname.Text = "Pay Mode:";
                RadComboBoxEmpPrj.Visible = false;
                drpSubProjectID.Visible = false;
                RadGrid222.Visible = true;

                RadGrid111.Visible = false;
                // RadGrid222.DataSource = new string[] { };
                // RadGrid222.Rebind();
                drpSubProjectID.Visible = false;
                drpFilter.Visible = true;
                if (rdFrom.SelectedDate.ToString() == "" || rdTo.SelectedDate.ToString() == "")
                {
                    drpFilter.Enabled = false;
                }
                else
                {
                    drpFilter.Enabled = true;
                    drpFilter.SelectedIndex = 0;
                }
            }
            // drpSubProjectID.SelectedIndex = -1;
            // SqlDataSource111.DataBind();

        }

        public void drpFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlSelectCommand = "";
            DateTime fdate = Convert.ToDateTime(rdFrom.SelectedDate.ToString());
            DateTime tdate = Convert.ToDateTime(rdTo.SelectedDate.ToString());
            if (comp_id == 1)// if login in demo company show all company emp
            {
                if (chkExcludeTerminateEmp.Checked)
                {

                    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_Code],[EmpName] = Case When termination_date is not null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End, Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0  " + FilterByPayMode() + "  ORDER BY [EmpName]";
                }
                else
                {
                    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [EmpName], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E where E.StatusID=1 inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE  Len([Time_Card_No]) > 0 And StatusID=1 " + FilterByPayMode() + "  ORDER BY [EmpName]";
                }

            }
            else
            {
                if (chkExcludeTerminateEmp.Checked)
                {

                    //sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_ID],[EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End, Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'  ORDER BY [EmpName]";
                    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_ID],[EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End, Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'  ORDER BY [EmpName]";

                }
                else
                {

                    // sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_ID], isnull(emp_name,'')+' '+isnull(emp_lname,'') [EmpName], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 And termination_date is  null and  StatusID=1 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'  ORDER BY [EmpName]";
                    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_ID], isnull(emp_name,'')+' '+isnull(emp_lname,'') [EmpName], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 And termination_date is  null and  StatusID=1 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'  ORDER BY [EmpName]";
                }

            }

            SqlDataSource222.SelectCommand = sqlSelectCommand;
            RadGrid222.DataBind();
            drpFilter.Enabled = true;
        }
        public void drpSubProjectID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlSelectCommand = "";
            DateTime fdate = Convert.ToDateTime(rdFrom.SelectedDate.ToString());
            DateTime tdate = Convert.ToDateTime(rdTo.SelectedDate.ToString());
            if (comp_id == 1)// if login in demo company show all company emp
            {
                if (chkExcludeTerminateEmp.Checked)
                {
                    SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is not null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=" + comp_id + "  And SP.ID=" + drpSubProjectID.SelectedValue + "  Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") And SOftDelete = 0) D Order By EmpName";
                }
                else
                {
                    SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=" + comp_id + "  And SP.ID=" + drpSubProjectID.SelectedValue + "  And EM.StatusID=1 Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") And SOftDelete = 0) D Order By EmpName";
                }

            }
            else
            {
                if (chkExcludeTerminateEmp.Checked)
                {
                    //SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is  null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=" + comp_id + "  And SP.ID=" + drpSubProjectID.SelectedValue + "  Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select  EmpName= Case When termination_date is  null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") And SOftDelete = 0) D Order By EmpName";
                    if (drpSubProjectID.SelectedValue.ToString() == "-1")
                    {
                        SqlDataSource111.SelectCommand = "Select distinct E.emp_code Emp_ID,EmpName = Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End     From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID   and  A.company_id=" + comp_id + " AND e.time_card_no=A.userID  and A.timeentry >='" + fdate.ToString("MM/dd/yyyy") + "' and A.timeentry <='" + tdate.ToString("MM/dd/yyyy") + "' order by EmpName";
                    }
                    else
                    {
                        SqlDataSource111.SelectCommand = "Select distinct E.emp_code Emp_ID,EmpName = Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End     From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID and S.ID='" + drpSubProjectID.SelectedValue + "'  and  A.company_id=" + comp_id + " AND e.time_card_no=A.userID  and A.timeentry >='" + fdate.ToString("MM/dd/yyyy") + "' and A.timeentry <='" + tdate.ToString("MM/dd/yyyy") + "' order by EmpName";
                    }


                }
                else
                {
                    if (drpSubProjectID.SelectedValue.ToString() == "-1")
                    {
                        SqlDataSource111.SelectCommand = "Select distinct E.emp_code Emp_ID, Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End   'EmpName'  From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID  and  A.company_id=" + comp_id + " AND e.time_card_no=A.userID  and A.timeentry >='" + fdate.ToString("MM/dd/yyyy") + "' and A.timeentry <='" + tdate.ToString("MM/dd/yyyy") + "' and termination_date is  null order by EmpName";
                    }
                    else
                    {
                        SqlDataSource111.SelectCommand = "Select distinct E.emp_code Emp_ID, Case When termination_date is  null Then (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,''))  Else (isnull(e.EMP_NAME,'') +' ' + isnull(e.EMP_LNAME,'')) + '[Terminated]' End   'EmpName'  From  SubProject S, ACTATEK_LOGS_PROXY A ,EMPLOYEE E where  A.terminalSN=S.Sub_Project_ID and S.ID='" + drpSubProjectID.SelectedValue + "'  and  A.company_id=" + comp_id + " AND e.time_card_no=A.userID  and A.timeentry >='" + fdate.ToString("MM/dd/yyyy") + "' and A.timeentry <='" + tdate.ToString("MM/dd/yyyy") + "' and termination_date is  null order by EmpName";
                    }
                    //SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=" + comp_id + "  And SP.ID=" + drpSubProjectID.SelectedValue + "  And EM.StatusID=1 Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID) Emp_ID,(Select (Emp_Name + ' ' + Emp_Lname) EmpName From Employee Where Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") And SOftDelete = 0) D Order By EmpName";  
                    // SqlDataSource111.SelectCommand = "Select * From(Select EA.ID Child_ID, EA.Sub_Project_ID, SP.Sub_Project_ID ID, SP.Sub_Project_Name, EA.Emp_ID, [EmpName] = Case When termination_date is  null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End  From EmployeeAssignedToProject EA Inner Join SubProject SP On  EA.Sub_Project_ID = SP.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code Where EM.Company_ID=" + comp_id + "  And SP.ID=" + drpSubProjectID.SelectedValue + " And EM.StatusID=1  Union Select Distinct 0 Child_ID, (Select ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") ID, (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID, (Select Sub_Project_Name From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") Sub_Project_ID,  (Select Emp_Code From Employee Where Time_Card_No=AL.UserID ) Emp_ID,(Select  EmpName= Case When termination_date is  null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End From Employee Where  Time_Card_No=AL.UserID) Emp_ID From ACTATEK_LOGS_PROXY AL Where rtrim(TerminalSN) = (Select Sub_Project_ID From SubProject  SP Where SP.ID=" + drpSubProjectID.SelectedValue + ") And SOftDelete = 0) D  where EmpName not like '%]' Order By EmpName";


                }

            }


            RadGrid111.Rebind();
            drpSubProjectID.Enabled = true;
        }
        string filertQuery;
        private string FilterByPayMode()
        {
            if (drpFilter.SelectedValue == "All")
            {
                filertQuery = " ";
            }
            else if (drpFilter.SelectedValue == "Daily")
            {
                filertQuery = "AND pay_frequency='D'";
            }
            else if (drpFilter.SelectedValue == "Hourly")
            {
                filertQuery = "AND pay_frequency='H'";
            }
            else if (drpFilter.SelectedValue == "Monthly")
            {
                filertQuery = "AND pay_frequency='M'";
            }
            else
            {
                filertQuery = "AND pay_frequency='O'";
            }
            return filertQuery;
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

        protected void dlDept_selectedIndexChanged(object sender, EventArgs e)
        {
            string sqlSelect;
            DataSet empDs;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
            {
                if (dlDept.SelectedValue != "-2")
                {
                    //if (dlDept.SelectedValue == "-1")
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    if (dlDept.SelectedValue == "-1")
                    //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                // sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                //   sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";


                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
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

                    }
                }
                else
                {

                    RadGrid1.Visible = false;
                    RadGrid1.MasterTableView.Visible = false;
                }
            }
            //Added by Sandi
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlDept")
            {
                if (ddlDept.SelectedValue != "-2")
                {
                    if (ddlDept.SelectedValue == "-1")
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    else
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid20.DataSource = empDs.Tables[0];
                        RadGrid20.DataBind();
                        if (RadGrid20.Visible == false)
                        {
                            RadGrid20.Visible = true;
                            RadGrid20.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid20.Visible = false;
                    RadGrid20.MasterTableView.Visible = false;
                }
            }
            //End Added

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayDept")
            {
                if (ddlPayDept.SelectedValue != "-2")
                {
                    //if (ddlPayDept.SelectedValue == "-1")
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + ddlPayDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //SqlParameter[] parms1 = new SqlParameter[4];
                    //parms1[0] = new SqlParameter("@company_id", comp_id);
                    //parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    //parms1[2] = new SqlParameter("@Type", "DEPART");
                    //parms1[3] = new SqlParameter("@TypeID", ddlPayDept.SelectedValue.ToString());
                    //if (chkExcludeTerminateEmp.Checked)
                    //{
                    //    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll1", parms1);
                    //}
                    //else
                    //{
                    //  empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);
                    //}

                    string terminate = "";
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        terminate = "YES";
                    }
                    else
                    {
                        terminate = "NO";
                    }

                    string fromdate = Convert.ToDateTime(dtp1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string todate = Convert.ToDateTime(dtp2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    SqlParameter[] parms1 = new SqlParameter[7];
                    parms1[0] = new SqlParameter("@company_id", comp_id);
                    parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    parms1[2] = new SqlParameter("@Type", "DEPART");
                    parms1[3] = new SqlParameter("@TypeID", ddlPayDept.SelectedValue.ToString());
                    parms1[4] = new SqlParameter("@Terminated", terminate);
                    parms1[5] = new SqlParameter("@FromDate", fromdate);
                    parms1[6] = new SqlParameter("@ToDate", todate);

                    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);

                    //empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid3.DataSource = empDs.Tables[0];
                        RadGrid3.DataBind();
                        if (RadGrid3.Visible == false)
                        {
                            RadGrid3.Visible = true;
                            RadGrid3.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid3.Visible = false;
                    RadGrid3.MasterTableView.Visible = false;
                }
            }

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlAdditions")
            {
                if (dlAdditions.SelectedValue != "-2")
                {
                    //if (dlAdditions.SelectedValue == "-1")
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlAdditions.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                    //SqlParameter[] parms1 = new SqlParameter[4];
                    //parms1[0] = new SqlParameter("@company_id", comp_id);
                    //parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    //parms1[2] = new SqlParameter("@Type", "DEPART");
                    //parms1[3] = new SqlParameter("@TypeID", dlAdditions.SelectedValue.ToString());
                    //empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);

                    string terminate = "";
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        terminate = "YES";
                    }
                    else
                    {
                        terminate = "NO";
                    }

                    string fromdate = Convert.ToDateTime(RadDatePicker1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string todate = Convert.ToDateTime(RadDatePicker2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    SqlParameter[] parms1 = new SqlParameter[7];
                    parms1[0] = new SqlParameter("@company_id", comp_id);
                    parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    parms1[2] = new SqlParameter("@Type", "DEPART");
                    parms1[3] = new SqlParameter("@TypeID", dlAdditions.SelectedValue.ToString());
                    parms1[4] = new SqlParameter("@Terminated", terminate);
                    parms1[5] = new SqlParameter("@FromDate", fromdate);
                    parms1[6] = new SqlParameter("@ToDate", todate);

                    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);

                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid5.DataSource = empDs.Tables[0];
                        RadGrid5.DataBind();
                        if (RadGrid5.Visible == false)
                        {
                            RadGrid5.Visible = true;
                            RadGrid5.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid5.Visible = false;
                    RadGrid5.MasterTableView.Visible = false;
                }
            }

            //santy
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlExpiryDept")
            {
                if (dlExpiryDept.SelectedValue != "-2")
                {
                    if (dlExpiryDept.SelectedValue == "-1")
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    else
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid16.DataSource = empDs.Tables[0];
                        RadGrid16.DataBind();
                        if (RadGrid16.Visible == false)
                        {
                            RadGrid16.Visible = true;
                            RadGrid16.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid16.Visible = false;
                    RadGrid16.MasterTableView.Visible = false;
                }
            }
            //Advance Claim Added By Jammu Offcie////////////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlClaimReportDept")
            {
                if (ddlClaimReportDept.SelectedValue != "-2")
                {
                    string terminate = "";
                    if (chkExcludeTerminateEmp.Checked)
                    {
                        terminate = "YES";
                    }
                    else
                    {
                        terminate = "NO";
                    }

                    string fromdate = Convert.ToDateTime(ClaimReportDate1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                    string todate = Convert.ToDateTime(ClaimReportDate2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                    SqlParameter[] parms1 = new SqlParameter[7];
                    parms1[0] = new SqlParameter("@company_id", comp_id);
                    parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                    parms1[2] = new SqlParameter("@Type", "DEPART");
                    parms1[3] = new SqlParameter("@TypeID", ddlClaimReportDept.SelectedValue.ToString());
                    parms1[4] = new SqlParameter("@Terminated", terminate);
                    parms1[5] = new SqlParameter("@FromDate", fromdate);
                    parms1[6] = new SqlParameter("@ToDate", todate);

                    empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGridEmployeeClaimReport.DataSource = empDs.Tables[0];
                        RadGridEmployeeClaimReport.DataBind();
                        if (RadGridEmployeeClaimReport.Visible == false)
                        {
                            RadGridEmployeeClaimReport.Visible = true;
                            RadGridEmployeeClaimReport.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGridEmployeeClaimReport.Visible = false;
                    RadGridEmployeeClaimReport.MasterTableView.Visible = false;
                }
            }

            //Advance Claim ends By Jammu Offcie////////////////
            //Payment Variance Report///////////////////////////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dropdownDeptPaymentVariance")
            {
                if (dropdownDeptPaymentVariance.SelectedValue != "-2")
                {
                    if (dropdownDeptPaymentVariance.SelectedValue == "-1")
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlExpiryDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }

                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {

                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }

                        }
                    }
                    else
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }

                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dropdownDeptPaymentVariance.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                        }
                    }

                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGridEmployeePaymentVariace.DataSource = empDs.Tables[0];
                        RadGridEmployeePaymentVariace.DataBind();
                        if (RadGridEmployeePaymentVariace.Visible == false)
                        {
                            RadGridEmployeePaymentVariace.Visible = true;
                            RadGridEmployeePaymentVariace.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGridEmployeePaymentVariace.Visible = false;
                    RadGridEmployeePaymentVariace.MasterTableView.Visible = false;
                }
            }
            //ends////////////////////////////////

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlEmailDept")
            {
                if (dlEmailDept.SelectedValue != "-2")
                {
                    if (dlEmailDept.SelectedValue == "-1")
                    //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            // sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    else
                    // sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    {
                        if (chkExcludeTerminateEmp.Checked)
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                        else
                        {
                            if (sgroupname == "Super Admin")
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                            }
                            else
                            {
                                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ORDER BY EMP_NAME";

                            }
                            //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + dlEmailDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                        }
                    }
                    empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
                    if (empDs.Tables[0].Rows.Count > 0)
                    {
                        RadGrid14.DataSource = empDs.Tables[0];
                        RadGrid14.DataBind();
                        if (RadGrid14.Visible == false)
                        {
                            RadGrid14.Visible = true;
                            RadGrid14.MasterTableView.Visible = true;
                        }
                    }
                }
                else
                {
                    RadGrid14.Visible = false;
                    RadGrid14.MasterTableView.Visible = false;
                }
            }
            //if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlCommon")
            //{
            //    if (ddlCommon.SelectedValue != "-2")
            //    {
            //        string textVal = ddlCommon.SelectedValue;
            //        //if (dlDept.SelectedValue == "-1")
            //        //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //        //else
            //        //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //        if (ddlCommon.SelectedValue == "-1")
            //        //sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //        {
            //            if (chkExcludeTerminateEmp.Checked)
            //            {
            //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //            }
            //            else
            //            {
            //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE  termination_date is null and COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //            }
            //        }
            //        else
            //        //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //        {
            //            if (chkExcludeTerminateEmp.Checked)
            //            {
            //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE DEPT_ID = " + ddlCommon.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //            }
            //            else
            //            {
            //                sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE termination_date is null and DEPT_ID = " + ddlCommon.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
            //            }
            //        }
            //        empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            //        if (empDs.Tables[0].Rows.Count > 0)
            //        {

            //            RadGrid22.DataSource = empDs.Tables[0];
            //            RadGrid22.DataBind();


            //            if (RadGrid22.Visible == false)
            //            {
            //                RadGrid22.Visible = true;
            //                RadGrid22.MasterTableView.Visible = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        RadGrid22.Visible = false;
            //        RadGrid22.MasterTableView.Visible = false;

            //    }
            //}




            //////Payroll Costing Added By Jammu Office/////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayrollCostingDept")
            {
                string terminate = "";
                if (chkExcludeTerminateEmp.Checked)
                {
                    terminate = "YES";
                }
                else
                {
                    terminate = "NO";
                }

                string fromdate = Convert.ToDateTime(dtpPayrollCosting1.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                string todate = Convert.ToDateTime(dtpPayrollCosting2.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);

                SqlParameter[] parms1 = new SqlParameter[7];
                parms1[0] = new SqlParameter("@company_id", comp_id);
                parms1[1] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"].ToString()));
                parms1[2] = new SqlParameter("@Type", "DEPART");
                parms1[4] = new SqlParameter("@Terminated", terminate);
                parms1[5] = new SqlParameter("@FromDate", fromdate);
                parms1[6] = new SqlParameter("@ToDate", todate);

                empDs = DataAccess.ExecuteSPDataSet("Sp_userrighttopayroll", parms1);
                if (empDs.Tables[0].Rows.Count > 0)
                {
                }

                ///////////////////Grid Payroll////////////////////////

                string sqlStr = "SELECT ID,ALIAS_NAME ,CASE WHEN TABLE_SOURCE IS NULL THEN FIELD_NAME WHEN LEN(TABLE_SOURCE) = 0 THEN FIELD_NAME ELSE TABLE_SOURCE END AS RELATION FROM TABLEOBJATTRIB WHERE TABLEiD=2 and ALIAS_NAME<>'CPF Addition Ordinary' and ALIAS_NAME<>'CPF Addition Wages'  and ALIAS_NAME<>'CPF Net' union  select 100+1 as ID,Apcategory as alias_name, 'AdditionPay' as  relation from AdditionPay AP  inner join APCategory AC on AP.Eid=APcatId   where company_id='" + comp_id + "'";
                DataTable dtPayroll = DataAccess.FetchRS(CommandType.Text, sqlStr, null).Tables[0];
                DataRow drPayroll;
                drPayroll = dtPayroll.NewRow();
                drPayroll[1] = "Basic_Pay_Conversion";
                drPayroll[2] = "B_P_Con";
                dtPayroll.Rows.Add(drPayroll);

                drPayroll = dtPayroll.NewRow();
                drPayroll[1] = "Basic_Pay_Currency";
                drPayroll[2] = "B_P_Cur";
                dtPayroll.Rows.Add(drPayroll);

                DataView dv = dtPayroll.DefaultView;
                dv.Sort = "ALIAS_NAME";
                /////////////////////////Grid Payroll/////////////////////////


                /////////////////////////Grid Additions/////////////////////////
                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewAdditionTypesDescAdd  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) and OPTIONSELECTION in ('General','Variable')";
                //RadGridPayrollCostingAdditions.DataSource = DataAccess.FetchRS(CommandType.Text, sqlStr, null);
                // RadGridPayrollCostingAdditions.DataBind();

                /////////////////////////Grid Additions/////////////////////////



                /////////////////////////Grid Deductions/////////////////////////
                sqlStr = "select id as ID, ALIAS_NAME,ALIAS_NAME AS RELATION,TableID from dbo.ViewDeductionsDed  WHERE (COMPANY_ID=" + compid + " OR (isShared='YES')) ";

                /////////////////////////Grid Deductions/////////////////////////
                {
                }
            }
            //////Payroll Costing ends By Jammu Office/////////





        }

        protected void dlDept_databound(object sender, EventArgs e)
        {
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlDept")
            {
                dlDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                dlDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlPayDept")
            {
                ddlPayDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                ddlPayDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }

            //Advance Claim Added by Jammu Office//////////////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlClaimReportDept")
            {
                ddlClaimReportDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                ddlClaimReportDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            //Advance Claim ends by Jammu Office//////////////////

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlAdditions")
            {
                dlAdditions.Items.Insert(0, new ListItem("- Select -", "-2"));
                dlAdditions.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            //Payment Variance Report//////////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dropdownDeptPaymentVariance")
            {
                dropdownDeptPaymentVariance.Items.Insert(0, new ListItem("- Select -", "-2"));
                dropdownDeptPaymentVariance.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            //ends//////////////////////////////
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlExpiryDept")
            {
                dlExpiryDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                dlExpiryDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlEmailDept")
            {
                dlEmailDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                dlEmailDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlDept")
            {
                ddlDept.Items.Insert(0, new ListItem("- Select -", "-2"));
                ddlDept.Items.Insert(1, new ListItem("- All Departments -", "-1"));
            }

            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "dlCustomTemplates")
            {
                dlCustomTemplates.Items.Insert(0, new ListItem("-- Select One--", "-1"));
            }
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlCommon")
            {

                ddlCommon.Items.Insert(0, new ListItem("-- Select --", "-1"));
            }
            if (((System.Web.UI.WebControls.DropDownList)sender).ID == "ddlSelectCategory")
            {

                ddlSelectCategory.Items.Insert(0, new ListItem("-- Select --", "-1"));
            }

        }




        protected void RadComboBoxEmpPrj_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            RadGrid222.Visible = false;
            RadGrid111.Visible = true;
            string sSQL = "";
            DataSet ds = new DataSet();
            if (RadComboBoxEmpPrj.SelectedValue != "-1")
            {
                //r
                sSQL = "Select S.ID,S.Sub_Project_Name From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where S.ID IN (Select distinct ID From EmployeeAssignedToProject EA Inner Join Employee EY On EA.Emp_ID = EY.Emp_Code Where EY.Emp_Code={0})UNION select ID,Sub_Project_Name from SubProject where Sub_Project_ID in(select Distinct(terminalSN) from ACTATEK_LOGS_PROXY where userID=(select  time_card_no  from Employee  where emp_code={0}))";

                //sSQL = "Select S.ID, S.Sub_Project_Name, S.ID Child_ID, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name  From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where S.ID IN (Select distinct ID From EmployeeAssignedToProject EA Inner Join Employee EY On EA.Emp_ID = EY.Emp_Code Where EY.Emp_Code={0})";
                sSQL = string.Format(sSQL, "'" + RadComboBoxEmpPrj.SelectedValue + "'");
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            }
            else if (RadComboBoxEmpPrj.SelectedItem.Value == "-1")
            {
                sSQL = "Select S.ID, S.Sub_Project_Name, P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID    From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID";

                //sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            }

            RadGrid111.DataSource = ds;
            RadGrid111.DataBind();
        }
        protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            string sqlSelectCommand = "";
            RadComboBox rd = new RadComboBox();
            rd = RadComboBoxEmpPrj;
            if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet for All") == true))
            {
                if (comp_id == 1)// if login in demo company show all company emp
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Len([Time_Card_No]) > 0 And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%')  and Company_Id='" + comp_id + "' ORDER BY [Emp_Name]";
                }
            }
            else
            {
                // sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Len([Time_Card_No]) > 0 and emp_code='" + varEmpCode + "' And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                if (comp_id == 1)// if login in demo company show all company emp
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Len([Time_Card_No]) > 0  and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
                }
                else
                {
                    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE Len([Time_Card_No]) > 0  And StatusID=1 And (upper([Emp_Name]) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%')  and Company_Id='" + comp_id + "'  and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") And StatusID=1 ORDER BY [Emp_Name]";
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
            adapter.SelectCommand.Parameters.AddWithValue("@text", e.Text);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            foreach (DataRow dataRow in dataTable.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Text = Convert.ToString(dataRow["Emp_Name"]);
                item.Value = Convert.ToString(dataRow["Emp_Code"].ToString());

                string Time_Card_No = Convert.ToString(dataRow["Time_Card_No"]);
                string ic_pp_number = Convert.ToString(dataRow["ic_pp_number"]);

                item.Attributes.Add("Time_Card_No", Time_Card_No.ToString());
                item.Attributes.Add("ic_pp_number", ic_pp_number.ToString());

                //item.Value += ":" + Time_Card_No;

                rd.Items.Add(item);
                item.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCompliance_Click(object sender, EventArgs e)
        {
            if (rdVar.SelectedValue == "CostCenter")
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
                    if (rdVar.SelectedValue == "CostCenter")
                    {
                        Response.Redirect("../Reports/CustomReportNew.aspx?PageType=10&SM=" + cmbFromMonth.Items[cmbFromMonth.SelectedIndex].Text + "&EM=" + cmbToMonth.Items[cmbToMonth.SelectedIndex].Text);
                    }
                    //Response.Redirect("../Reports/CustomReportNew.aspx");
                    //string sFileName = "../Reports/CustomReportNew.aspx";
                    //Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
                }
                else
                {
                    ShowMessageBox("No Records Found");

                }
                #endregion
            }
            else if (rdVar.SelectedValue == "Employee")
            {
                //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                //string startdate = Convert.ToDateTime(RadDatePicker_From.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                //string enddate = Convert.ToDateTime(RadDatePicker_To.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                ////Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartMonth=" + cmbFromMonth.SelectedItem + "&EndMonth=" + cmbToMonth.SelectedItem + "&year=" + cmbYear.SelectedValue + ""); 
                //Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartDate=" + startdate + "&EndDate=" + enddate + ""); 
            }
        }

        void rdVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdVar.SelectedValue == "CostCenter")
            {
                cost_var.Visible = true;
                Emp_var.Visible = false;
            }
            else
            {
                cost_var.Visible = false;
                Emp_var.Visible = true;
            }
        }


        void btnvVariance_Click(object sender, EventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            string startdate = Convert.ToDateTime(RadDatePicker_From.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
            string enddate = Convert.ToDateTime(RadDatePicker_To.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
            //Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartMonth=" + cmbFromMonth.SelectedItem + "&EndMonth=" + cmbToMonth.SelectedItem + "&year=" + cmbYear.SelectedValue + ""); 
            Response.Redirect("../Reports/CustomReportVariance_Employee.aspx?company_Id=" + comp_id + "&StartDate=" + startdate + "&EndDate=" + enddate + "");
        }
        void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonthFill();
        }
        private void bindMonth()
        {
            MonthFill();
        }
        void CallBeforeMonthFill()
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
        }
        void MonthFill()
        {
            CallBeforeMonthFill();
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Utility.ToInteger("1"));
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
                foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }

            cmbFromMonth.DataSource = dtFilterFound;
            cmbFromMonth.DataTextField = "MonthName";
            cmbFromMonth.DataValueField = "RowID";
            cmbFromMonth.DataBind();

            cmbToMonth.DataSource = dtFilterFound;
            cmbToMonth.DataTextField = "MonthName";
            cmbToMonth.DataValueField = "RowID";
            cmbToMonth.DataBind();
        }

        protected void btnTS_Click(object sender, EventArgs e)
        {
            string strMessage = "";

            if (radDtpckTsFrom.SelectedDate == null || radDtpckTsTo.SelectedDate == null)
            {
                strMessage = strMessage + "<br/>" + "Please Enter Start Date And End Date.";
            }

            if (strMessage.Length <= 0)
            {
                DataSet ds = new DataSet();

                string strStartDate = radDtpckTsFrom.SelectedDate.Value.Year + "-" + radDtpckTsFrom.SelectedDate.Value.Day + "-" + radDtpckTsFrom.SelectedDate.Value.Month;
                string strEndDate = radDtpckTsTo.SelectedDate.Value.Year + "-" + radDtpckTsTo.SelectedDate.Value.Day + "-" + radDtpckTsTo.SelectedDate.Value.Month;
                string sqlata = "";
                if (radCmbTsPay.SelectedValue == "All")
                {
                    sqlata = "select SubProject.Sub_Project_Name,sum(v1) v1,sum(v2) v2,sum(v3) v3,sum(v4) v4 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID";
                    sqlata = sqlata + " Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + strStartDate + "',103) ";
                    sqlata = sqlata + "And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + strEndDate + "',103))   ";
                    sqlata = sqlata + "Group By  SubProject.Sub_Project_Name";
                }
                else if (radCmbTsPay.SelectedValue.ToUpper() == "V1")
                {
                    sqlata = "select SubProject.Sub_Project_Name,sum(v1) v1 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID";
                    sqlata = sqlata + " Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + strStartDate + "',103) ";
                    sqlata = sqlata + "And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + strEndDate + "',103))   ";
                    sqlata = sqlata + "Group By  SubProject.Sub_Project_Name";
                }
                else if (radCmbTsPay.SelectedValue.ToUpper() == "V2")
                {
                    sqlata = "select SubProject.Sub_Project_Name,sum(v2) v2 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID";
                    sqlata = sqlata + " Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + strStartDate + "',103) ";
                    sqlata = sqlata + "And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + strEndDate + "',103))   ";
                    sqlata = sqlata + "Group By  SubProject.Sub_Project_Name";
                }
                else if (radCmbTsPay.SelectedValue.ToUpper() == "V3")
                {
                    sqlata = "select SubProject.Sub_Project_Name ,sum(v3) v3 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID";
                    sqlata = sqlata + " Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + strStartDate + "',103) ";
                    sqlata = sqlata + "And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + strEndDate + "',103))   ";
                    sqlata = sqlata + "Group By  SubProject.Sub_Project_Name";
                }
                else if (radCmbTsPay.SelectedValue.ToUpper() == "V4")
                {
                    sqlata = "select SubProject.Sub_Project_Name,sum(v4) v4 From ApprovedTimeSheet Inner Join SubProject ON SubProject.Sub_Project_ID=ApprovedTimeSheet.Sub_Project_ID";
                    sqlata = sqlata + " Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + strStartDate + "',103) ";
                    sqlata = sqlata + "And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + strEndDate + "',103))   ";
                    sqlata = sqlata + "Group By  SubProject.Sub_Project_Name";
                }

                ds = DataAccess.FetchRS(CommandType.Text, sqlata, null);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Response.Redirect("../Reports/CustomReportNew_TimeSheet.aspx");
                    //Response.Redirect("../Reports/CustomReportNew.aspx");
                    //string sFileName = "../Reports/CustomReportNew.aspx";
                    //Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");

                    string strV1 = "", strV2 = "", strV3 = "", strV4 = "";
                    string strQuery = "select [desc],code from additions_types where company_id=" + comp_id + " and code in ('V1','V2','V3','V4')order by code Asc ";
                    DataSet dsQuery = new DataSet();
                    dsQuery = DataAccess.FetchRS(CommandType.Text, strQuery, null);
                    foreach (DataRow dr in dsQuery.Tables[0].Rows)
                    {
                        if (dr["code"].ToString() == "V1")
                        {
                            strV1 = dr["desc"].ToString();
                        }
                        if (dr["code"].ToString() == "V2")
                        {
                            strV2 = dr["desc"].ToString();
                        }
                        if (dr["code"].ToString() == "V3")
                        {
                            strV3 = dr["desc"].ToString();
                        }
                        if (dr["code"].ToString() == "V4")
                        {
                            strV4 = dr["desc"].ToString();
                        }
                    }
                    if (radCmbTsPay.SelectedValue == "All")
                    {
                        ds.Tables[0].Columns["v1"].ColumnName = strV1.Replace(" ", "");
                        ds.Tables[0].Columns["v2"].ColumnName = strV2.Replace(" ", "");
                        ds.Tables[0].Columns["v3"].ColumnName = strV3.Replace(" ", "");
                        ds.Tables[0].Columns["v4"].ColumnName = strV4.Replace(" ", "");
                    }
                    if (radCmbTsPay.SelectedValue.ToUpper() == "V2")
                    {
                        ds.Tables[0].Columns["v2"].ColumnName = strV2.Replace(" ", "");
                    }

                    if (radCmbTsPay.SelectedValue.ToUpper() == "V3")
                    {
                        ds.Tables[0].Columns["v3"].ColumnName = strV3.Replace(" ", "");
                    }

                    if (radCmbTsPay.SelectedValue.ToUpper() == "V4")
                    {
                        ds.Tables[0].Columns["v4"].Caption = strV4.Replace(" ", "");
                    }

                    Session["rptDs"] = ds;

                    string sql;
                    #region New report
                    if (radCmbTsPay.SelectedValue.ToUpper() == "V1" || radCmbTsPay.SelectedValue.ToUpper() == "V2" || radCmbTsPay.SelectedValue.ToUpper() == "V3" || radCmbTsPay.SelectedValue.ToUpper() == "V4")
                    {
                        sql = "select [DESC],code from additions_types where company_id='" + comp_id + "' and code='" + radCmbTsPay.SelectedValue.ToString() + "'";
                    }
                    else if (radCmbTsPay.SelectedValue.ToUpper() == "Labour")
                    {
                        sql = "select 'Labour' [DESC],'Labour' code";
                    }
                    else
                    {
                        sql = " select 'Labour' [DESC],'Labour' code union select [DESC],code from additions_types where company_id='" + comp_id + "' and code in ('v1','v2','v3','v4')";
                    }
                    DataSet dsQuery1 = new DataSet();
                    dsQuery1 = DataAccess.FetchRS(CommandType.Text, sql, null);
                    Session["Selectedvariables"] = dsQuery1;
                    Session["stdate"] = strStartDate;
                    Session["eddate"] = strEndDate;
                    Response.Redirect("../Reports/CustomReportTimesheetPayment.aspx");
                    #endregion
                    // Response.Redirect("../Reports/CustomReportNew.aspx?PageType=20");
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

        protected void btnGo_Click(object sender, EventArgs e)
        {
            string strMessage = "";
            string strEmployee = "";
            string strprj = "-1";
            string eid = "";

            RadComboBox radcomb = new RadComboBox();
            DropDownList drp = new DropDownList();
            //Telerik.Web.UI.RadDatePicker rdst = new Telerik.Web.UI.RadDatePicker();
            //Telerik.Web.UI.RadDatePicker rden = new Telerik.Web.UI.RadDatePicker();

            Telerik.WebControls.RadDatePicker rdst = new Telerik.WebControls.RadDatePicker();
            Telerik.WebControls.RadDatePicker rden = new Telerik.WebControls.RadDatePicker();

            CheckBox chk = new CheckBox();
            RadGrid rd = new RadGrid();

            if (rdOptionList.SelectedValue == "1")
            {
                rd = RadGrid111;
                strprj = drpSubProjectID.SelectedItem.Value;


            }
            else if (rdOptionList.SelectedValue == "2")
            {
                rd = RadGrid222;
                //strEmployee = RadComboBoxEmpPrj.SelectedValue;
                //strEmployee ="";
                //rd = RadGrid222;
            }
            else if (rdOptionList.SelectedValue == "3")
            {
                rd = RadGrid222;
            }
            DateTime dt1 = new DateTime();
            DateTime dt2 = new DateTime();
            foreach (GridItem item in rd.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    eid = dataItem["Emp_ID"].Text.ToString();

                    if (chkBox.Checked == true)
                    {
                        if (strEmployee == "")
                        {
                            //strEmployee = dataItem.Cells[2].Text.ToString().Trim();
                            strEmployee = eid;
                        }
                        else
                        {
                            //strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                            strEmployee = strEmployee + "," + eid;
                        }

                    }
                    //  if (Request.QueryString["PageType"] == null)
                    //  {
                    // if (rd.ID.ToString() == "RadGrid111")
                    // {
                    //    strprj = strprj + "," + dataItem.Cells[2].Text.ToString().Trim();
                }
                //else
                // {
                //  if (dataItem.Cells[2].Text.ToString() != "&nbsp;")
                //  {

                //   }
                //}
                // }
                //else
                //{
                //    if (rd.ID.ToString() == "RadGrid111")
                //    {
                //        strprj = strprj + "," + dataItem.Cells[2].Text.ToString().Trim();
                //    }
                //    else
                //    {
                //        if (dataItem.Cells[2].Text.ToString() != "&nbsp;")
                //        {
                //            strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                //        }
                //    }
                //}
                //}

            }


            //if (comp_id == 1)// if login in demo company show all company emp
            //{
            //    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [EmpName], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 And StatusID=1 " + FilterByPayMode() + "  ORDER BY [EmpName]";
            //}
            //else
            //{
            //    sqlSelectCommand = "SELECT DISTINCT '0' as Child_ID,Emp_Code [Emp_ID], isnull(emp_name,'')+' '+isnull(emp_lname,'') [EmpName], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 And termination_date is null and  StatusID=1 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " ORDER BY [EmpName]";
            //}
            //if (strprj == "-1" && (rdOptionList.SelectedValue == "1"))
            //{
            //    strMessage = strMessage + "<br/>" + "Please Select Project.";
            //}

            if (strEmployee == "")
            {
                strMessage = strMessage + "<br/>" + "Please Select Employee.";
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
            //---murugan
            //string sql = "";
            //string empstr = "";
            //if (chkExcludeTerminateEmp.Checked)
            //{
            //    DateTime fdate = Convert.ToDateTime(rdFrom.SelectedDate.ToString());
            //    DateTime tdate = Convert.ToDateTime(rdTo.SelectedDate.ToString());
            //    DataSet ds2 = new DataSet();
            //    if (chkExcludeTerminateEmp.Checked)
            //    {
            //        //sql = "SELECT  EMP_CODE FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " and termination_date is not null and ( termination_date >='" + fdate.ToString("yyyy-MM-dd") + "' and termination_date <='" + tdate.ToString("yyyy-MM-dd") + "')";
            //     sql = "SELECT  EMP_CODE FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " and Len([Time_Card_No]) > 0 and termination_date is not null and  termination_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <'" + fdate.ToString("yyyy-MM-dd") + "'";
            //        //sql = "SELECT EMP_CODE from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'";
            //    }
            //    else
            //    {
            //        sql = "SELECT  EMP_CODE FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " and Len([Time_Card_No]) > 0 and termination_date is null and  termination_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <'" + fdate.ToString("yyyy-MM-dd") + "'";
            //       // sql = "SELECT EMP_CODE from [Employee] E inner join EmployeeAssignedToWorkersList EA on  E.emp_code=EA.Emp_ID WHERE Len([Time_Card_No]) > 0 And termination_date is  null and  StatusID=1 and Company_Id='" + comp_id + "' " + FilterByPayMode() + " and joining_date >='" + fdate.ToString("yyyy-MM-dd") + "' and joining_date <='" + tdate.ToString("yyyy-MM-dd") + "'";
            //    }
            //    ds2 = DataAccess.FetchRS(CommandType.Text, sql, null);
            //    if (ds2.Tables[0].Rows.Count > 0)
            //    {
            //        for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            //        {
            //            empstr = empstr + "," + ds2.Tables[0].Rows[i]["EMP_CODE"].ToString();
            //        }
            //        strEmployee = strEmployee + "," + empstr;
            //    }

            //}

            if (strMessage.Length <= 0)
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[6];
                DataSet ds = new DataSet();
                rdst = rdFrom;
                rden = rdTo;
                string strActionMsg = "";

                //if (rdOptionList.SelectedValue == "1")
                if (rdOptionList.SelectedValue == "4")
                {
                    string str = "";
                    SqlParameter[] parms1 = new SqlParameter[8];
                    parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                    parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                    parms1[2] = new SqlParameter("@compid", comp_id);
                    parms1[3] = new SqlParameter("@isEmpty", "No");
                    parms1[4] = new SqlParameter("@empid", strEmployee);
                    parms1[5] = new SqlParameter("@subprojid", Utility.ToString(strprj));
                    parms1[6] = new SqlParameter("@sessid", "-1");
                    parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(rdRepOptionTime.SelectedItem.Value));
                    try
                    {
                        ds = DataAccess.ExecuteSPDataSet("Sp_processtimesheet_rpt_New", parms1);
                    }
                    catch (Exception ex)
                    {
                        str = ex.ToString();
                    }
                }
                else
                {
                    string emp_id = "";
                    char sep = ',';
                    string[] emp = strEmployee.Split(sep);
                    DataSet ds1 = new DataSet();
                    for (int p = 0; p <= emp.Length - 1; p++)
                    {
                        if (emp[p].ToString() != "-1")
                        {
                            SqlParameter[] parms1 = new SqlParameter[8];
                            parms1[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                            parms1[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                            parms1[2] = new SqlParameter("@compid", comp_id);
                            parms1[3] = new SqlParameter("@isEmpty", "No");
                            parms1[4] = new SqlParameter("@empid", emp[p].ToString());
                            parms1[5] = new SqlParameter("@subprojid", Utility.ToString(-1));
                            parms1[6] = new SqlParameter("@sessid", "-1");
                            parms1[7] = new SqlParameter("@REPID", Utility.ToInteger(rdRepOptionTime.SelectedItem.Value));
                            try
                            {
                                ds1 = DataAccess.ExecuteSPDataSet("Sp_processtimesheet_rpt_New", parms1);
                            }
                            catch (Exception ex)
                            {
                                emp_id = ex.ToString();
                                emp_id = emp[p].ToString();
                            }
                        }
                        if (p == 0)
                        {
                            ds = ds1;
                        }
                        else
                        {
                            ds.Merge(ds1, true);
                        }
                    }
                }

                //------------------------------------------------------------------------------------
                //DataSet dsnew = new DataSet();
                //string strempty="";
                //string strproject = "";

                //if (rdOptionList.SelectedValue == "1")
                //{
                //    string strprj1 = "Select sub_project_ID   from subproject Where ID=" + strprj;                 
                //    SqlDataReader dr_subproj;
                //    dr_subproj = DataAccess.ExecuteReader(CommandType.Text, strprj1, null);

                //    while (dr_subproj.Read())
                //    {
                //        strproject = Convert.ToString(dr_subproj[0].ToString());
                //    }
                //}
                //else if (rdOptionList.SelectedValue == "2")
                //{
                //    //rd = RadGrid111;
                //    //strEmployee = RadComboBoxEmpPrj.SelectedValue;

                //    string strprj1 = "Select sub_project_ID   from subproject Where ID IN (" + strprj + ")";
                //    SqlDataReader dr_subproj;
                //    dr_subproj = DataAccess.ExecuteReader(CommandType.Text, strprj1, null);

                //    //while (dr_subproj.Read())
                //    //{
                //    //    if (strproject == "")
                //    //    {

                //    //        strproject = "'" + Convert.ToString(dr_subproj[0].ToString()) + "'" ;
                //    //    }
                //    //    else
                //    //    {
                //    //        strproject = strproject + ",'" + Convert.ToString(dr_subproj[0].ToString()) + "'";
                //    //    }
                //    //}
                //    strproject = "-1";
                //    //strproject = strprj;
                //}

                //SqlParameter[] parms2 = new SqlParameter[8];
                //parms2[0] = new SqlParameter("@start_date", dt1.ToString("dd/MM/yyyy"));
                //parms2[1] = new SqlParameter("@end_date", dt2.ToString("dd/MM/yyyy"));
                //parms2[2] = new SqlParameter("@compid", compid);
                //strempty = "ALL";
                //parms2[3] = new SqlParameter("@isEmpty", strempty);
                //parms2[4] = new SqlParameter("@empid", strEmployee);
                //parms2[5] = new SqlParameter("@subprojid", Convert.ToString(strproject));
                //parms2[6] = new SqlParameter("@sessid", -3);
                //parms2[7] = new SqlParameter("@REPID", Utility.ToInteger(99));

                //if (Session["PayAssign"].ToString() == "1")
                //{
                //    dsnew = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv", parms2);
                //    //ds = dsnew;
                //    //foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                //}
                //else
                //{
                //    dsnew = DataAccess.ExecuteSPDataSet("Sp_processtimesheetflexadv_Daily", parms2);
                //    //ds = dsnew;
                //}

                Session["SummaryDetail"] = Utility.ToInteger(rdRepOptionTime.SelectedItem.Value);
                Session["rptDs"] = ds;

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("../Reports/CustomReportNew_TimeSheet.aspx");
                    //Response.Redirect("../Reports/CustomReportNew.aspx");
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
            drpSubProjectID.Items.Insert(0, new ListItem("-All-", "-1"));
            drpSubProjectID.Items.Insert(0, new ListItem("-Select-", "-2"));
            //drpSubProjectID.Items.Insert(0, new ListItem( "-All-","0"));
        }
        protected void RadGrid222_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid222.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            // RadGrid222.DataBind();
        }
        protected void RadGrid111_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid111.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            // RadGrid111.DataBind();
        }
        #region Costing


        public void RadioCosting_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProject();
            LoadEmpGrid();
        }
        //////////Payroll Costing Added By Jammu Office///////////////////////////
        public void RadioPayrollCosting_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCosting();
        }

        string sqlSelectCosting;
        private void LoadCosting()
        {
            DataSet costingDs;
            if (RadioButtonPayrollCosting.SelectedValue == "1")
            {
                sqlSelectCosting = "select Bid,BusinessUnit from cost_businessunit where Company_ID=" + comp_id + "";
            }
            else if (RadioButtonPayrollCosting.SelectedValue == "2")
            {
                sqlSelectCosting = "select Bid,BusinessUnit from Cost_Region where Company_ID=" + comp_id + "";
            }
            else if (RadioButtonPayrollCosting.SelectedValue == "3")
            {

                sqlSelectCosting = "select Bid,BusinessUnit from Cost_Ccategory where Company_ID=" + comp_id + "";
            }

            costingDs = DataAccess.FetchRS(CommandType.Text, sqlSelectCosting, null);
            if (costingDs.Tables[0].Rows.Count > 0)
            {
                RadGridPayrollCostingCategory.DataSource = costingDs.Tables[0];
                RadGridPayrollCostingCategory.DataBind();
            }
        }
        //////////Payroll Costing ends By Jammu Office///////////////////////////
        string sqlSelect1;
        private void LoadProject()
        {
            DataSet empDs;
            if (RadioCosting.SelectedValue == "1")
            {
                sqlSelect1 = "select Bid,BusinessUnit from cost_businessunit where Company_ID=" + comp_id + "";
            }
            else if (RadioCosting.SelectedValue == "2")
            {
                sqlSelect1 = "select Bid,BusinessUnit from Cost_Region where Company_ID=" + comp_id + "";
            }
            else if (RadioCosting.SelectedValue == "3")
            {

                sqlSelect1 = "select Bid,BusinessUnit from Cost_Ccategory where Company_ID=" + comp_id + "";
            }

            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect1, null);
            if (empDs.Tables[0].Rows.Count > 0)
            {
                // RadGrid2.DataSource = empDs.Tables[0];
                RadGrid19.DataSource = empDs.Tables[0];
                RadGrid19.DataBind();
            }
        }

        string emp_table;
        private void LoadEmpGrid()
        {
            string sqlSelect;
            DataSet empDs;
            //if (chkExcludeTerminateEmp.Checked)
            //{

            //      sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " ORDER BY EMP_NAME";

            //}
            //else
            //{
            //    sqlSelect = "SELECT  EMP_CODE,isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'') as [NAME] FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " ORDER BY EMP_NAME";
            //}

            if (RadioCosting.SelectedValue == "3")
            {
                emp_table = "Cost_Category";

            }
            else if (RadioCosting.SelectedValue == "2")
            {
                emp_table = "Cost_Region";
            }
            else
            {
                emp_table = "Cost_Businessunit";
            }

            sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End,Time_card_no FROM dbo.employee WHERE COMPANY_ID= " + comp_id + " AND " + emp_table + " >=1 and emp_name is not null ORDER BY EMP_NAME";

            empDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
            if (empDs.Tables[0].Rows.Count > 0)
            {
                RadGrid18.DataSource = empDs.Tables[0];
                RadGrid18.DataBind();
            }
        }

        string sSQL, url_report;
        protected void GenerateCostingRpt_Click(object sender, EventArgs e)
        {
            #region Getting Emp_code
            string strEmployee = "0";
            foreach (GridItem item in RadGrid18.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        //grid1++;
                        strEmployee = strEmployee + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }
            #endregion

            #region getting SubProjectid
            string subprojectid = "0";
            foreach (GridItem item in RadGrid19.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        //grid1++;
                        subprojectid = subprojectid + "," + dataItem.Cells[2].Text.ToString().Trim();
                    }
                }
            }
            #endregion

            //validation
            if (strEmployee == "0")
            {
                Error.Text = "Please Select Employee";
                return;
            }
            if (subprojectid == "0")
            {
                Error.Text = "Please Select Project";
                return;
            }
            try
            {
                if (Convert.ToString(RadDatePicker3.SelectedDate.Value) == "")
                {
                    Error.Text = "Please Select Date";
                    return;
                }
            }
            catch (Exception exx)
            {
                Error.Text = "Please Select Date";
                return;
            }

            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(RadDatePicker3.SelectedDate.Value);
            int m = dt.Date.Month;
            int y = dt.Date.Year;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            if (RadioCosting.SelectedValue == "1")
            {
                sSQL = "sp_CostingByBusinessUnitReport";
                url_report = "../Cost/CustomCostingReportNew_BusinessUnit.aspx?cat=Cost_BusinessUnit";
            }
            else if (RadioCosting.SelectedValue == "2")
            {
                sSQL = "sp_CostingByRegionReport";
                url_report = "../Cost/CustomCostingReportNew_BusinessUnit.aspx?cat=Cost_Region";
            }
            else if (RadioCosting.SelectedValue == "3")
            {
                sSQL = "sp_CostingByCCategoryReport";
                url_report = "../Cost/CustomCostingReportNew_BusinessUnit.aspx?cat=Cost_Ccategory";
                //url_report = "../Cost/CostingByCategory.aspx?cat=Cost_Ccategory";
            }

            SqlParameter[] parms = new SqlParameter[6];
            parms[0] = new SqlParameter("@compid", comp_id);
            parms[1] = new SqlParameter("@month", m);
            parms[2] = new SqlParameter("@year", y);
            parms[3] = new SqlParameter("@AsDate", RadDatePicker3.SelectedDate.Value);
            parms[4] = new SqlParameter("@emp_code", strEmployee);
            parms[5] = new SqlParameter("@BusinessUnit", subprojectid);

            DataSet rptDs = new DataSet();
            rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);

            Session["CostingDataset1"] = rptDs;

            //Response.Redirect("../Cost/CustomCostingReportNew_BusinessUnit.aspx?cat=Cost_Region");
            Response.Redirect(url_report);
        }


        protected void ButtonTemplateCreate_Click(object sender, System.EventArgs e)
        {

            if (((System.Web.UI.WebControls.Button)sender).ID == "btnEmployeeCreate")
            {

                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                string strssqlb = "";
                int retVal = 0;
                int grid12 = 0;
                string strSQLQuery = "";
                string strQueryCheck = "";
                int templateID = 0;
                string strEmployeeVal = "0";
                int templateCount = 0;
                string strQueryCount = "";
                strSQLQuery = "Select ISNULL(max(TemplateID),0) as TemplateID from CustomTemplates Where CategoryId=1";
                templateID = DataAccess.ExecuteScalar(strSQLQuery);

                if (templateID == 0)
                {
                    templateID = 0 + 1;
                }
                else
                {
                    templateID = templateID + 1;
                }
                //strQueryCount = "Select ISNULL(max(TemplateID),0) as TemplateCount from CustomTemplates where  CategoryId=" + ddlCommon.SelectedValue + "";
                //templateCount = DataAccess.ExecuteScalar(strQueryCount);

                strQueryCheck = "Select * from CustomTemplates where TemplateName='" + txtEmpTemplateName.Text + "' AND CategoryId=1";
                dsTable = DataAccess.FetchRS(CommandType.Text, strQueryCheck, null);
                dtTable = dsTable.Tables[0];

                if (dtTable.Rows.Count == 0)
                {
                    foreach (GridItem item in RadGrid2.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                int ColumnID = Convert.ToInt32(this.RadGrid2.Items[dataItem.ItemIndex]["ID"].Text.ToString());
                                string AliasName = Convert.ToString(this.RadGrid2.Items[dataItem.ItemIndex]["ALIAS_NAME"].Text.ToString());
                                string RelationName = Convert.ToString(this.RadGrid2.Items[dataItem.ItemIndex]["RELATION"].Text.ToString());
                                string CategoryName = "Employee";
                                int CategoryId = 1;
                                string replacedRelation = RelationName.Replace("'", "''");
                                int TableID = 1;

                                strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + templateID + ",'" + txtEmpTemplateName.Text + "'," + ColumnID + "," + CategoryId + ",'" + CategoryName + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                                retVal += DataAccess.ExecuteStoreProc(strssqlb);
                            }
                        }
                    }

                }
                else
                {
                    ShowMessageBox("Template name is already exist");
                }
                if (retVal > 0)
                {
                    ShowMessageBox("Template Items Added Successfully");
                    txtEmpTemplateName.Text = string.Empty;
                    Response.Redirect("../Reports/CustomReportMainPage.aspx");
                }
                else
                {
                    ShowMessageBox("Template is not added");
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnPayCreate")
            {
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                string strssqlb = "";
                int retVal = 0;
                int grid12 = 0;
                string strSQLQuery = "";
                string strQueryCheck = "";
                int templateID = 0;
                string strEmployeeVal = "0";
                int templateCount = 0;
                string strQueryCount = "";
                strSQLQuery = "Select ISNULL(max(TemplateID),0) as TemplateID from CustomTemplates Where CategoryId=2";
                templateID = DataAccess.ExecuteScalar(strSQLQuery);

                if (templateID == 0)
                {
                    templateID = 0 + 1;
                }
                else
                {
                    templateID = templateID + 1;
                }
                //strQueryCount = "Select ISNULL(max(TemplateID),0) as TemplateCount from CustomTemplates where  CategoryId=" + ddlCommon.SelectedValue + "";
                //templateCount = DataAccess.ExecuteScalar(strQueryCount);

                strQueryCheck = "Select * from CustomTemplates where TemplateName='" + txtPayTemplateName.Text + "' AND CategoryId=2";
                dsTable = DataAccess.FetchRS(CommandType.Text, strQueryCheck, null);
                dtTable = dsTable.Tables[0];

                if (dtTable.Rows.Count == 0)
                {
                    foreach (GridItem item in RadGrid4.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                int ColumnID = Convert.ToInt32(this.RadGrid4.Items[dataItem.ItemIndex]["ID"].Text.ToString());
                                string AliasName = Convert.ToString(this.RadGrid4.Items[dataItem.ItemIndex]["ALIAS_NAME"].Text.ToString());
                                string RelationName = Convert.ToString(this.RadGrid4.Items[dataItem.ItemIndex]["RELATION"].Text.ToString());
                                string CategoryName = "Payroll";
                                int CategoryId = 2;
                                string replacedRelation = RelationName.Replace("'", "''");
                                int TableID = 2;

                                strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + templateID + ",'" + txtPayTemplateName.Text + "'," + ColumnID + "," + CategoryId + ",'" + CategoryName + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                                retVal += DataAccess.ExecuteStoreProc(strssqlb);
                            }
                        }
                    }

                }
                else
                {
                    ShowMessageBox("Template name is already exist");
                }
                if (retVal > 0)
                {
                    ShowMessageBox("Template Items Added Successfully");
                    txtPayTemplateName.Text = string.Empty;
                    Response.Redirect("../Reports/CustomReportMainPage.aspx");
                }
                else
                {
                    ShowMessageBox("Template is not added");
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnAddCreate")
            {
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                string strssqlb = "";
                int retVal = 0;
                int grid12 = 0;
                string strSQLQuery = "";
                string strQueryCheck = "";
                int templateID = 0;
                string strEmployeeVal = "0";
                int templateCount = 0;
                string strQueryCount = "";
                strSQLQuery = "Select ISNULL(max(TemplateID),0) as TemplateID from CustomTemplates Where CategoryId=3";
                templateID = DataAccess.ExecuteScalar(strSQLQuery);

                if (templateID == 0)
                {
                    templateID = 0 + 1;
                }
                else
                {
                    templateID = templateID + 1;
                }
                //strQueryCount = "Select ISNULL(max(TemplateID),0) as TemplateCount from CustomTemplates where  CategoryId=" + ddlCommon.SelectedValue + "";
                //templateCount = DataAccess.ExecuteScalar(strQueryCount);

                strQueryCheck = "Select * from CustomTemplates where TemplateName='" + txtAddTemplateName.Text + "' AND CategoryId=3";
                dsTable = DataAccess.FetchRS(CommandType.Text, strQueryCheck, null);
                dtTable = dsTable.Tables[0];

                if (dtTable.Rows.Count == 0)
                {
                    foreach (GridItem item in RadGrid6.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                int ColumnID = Convert.ToInt32(this.RadGrid6.Items[dataItem.ItemIndex]["ID"].Text.ToString());
                                string AliasName = Convert.ToString(this.RadGrid6.Items[dataItem.ItemIndex]["ALIAS_NAME"].Text.ToString());
                                string RelationName = Convert.ToString(this.RadGrid6.Items[dataItem.ItemIndex]["RELATION"].Text.ToString());
                                int TableID = Convert.ToInt32(this.RadGrid6.Items[dataItem.ItemIndex]["TableID"].Text.ToString());
                                string CategoryName = "Addition";
                                int CategoryId = 3;
                                string replacedRelation = RelationName.Replace("'", "''");


                                strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + templateID + ",'" + txtAddTemplateName.Text + "'," + ColumnID + "," + CategoryId + ",'" + CategoryName + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                                retVal += DataAccess.ExecuteStoreProc(strssqlb);
                            }
                        }
                    }

                }
                else
                {
                    ShowMessageBox("Template name is already exist");
                }
                if (retVal > 0)
                {
                    ShowMessageBox("Template Items Added Successfully");
                    txtAddTemplateName.Text = string.Empty;
                    Response.Redirect("../Reports/CustomReportMainPage.aspx");
                }
                else
                {
                    ShowMessageBox("Template is not added");
                }
            }
            else if (((System.Web.UI.WebControls.Button)sender).ID == "btnExpiryCreate")
            {
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                string strssqlb = "";
                int retVal = 0;
                int grid12 = 0;
                string strSQLQuery = "";
                string strQueryCheck = "";
                int templateID = 0;
                string strEmployeeVal = "0";
                int templateCount = 0;
                string strQueryCount = "";
                strSQLQuery = "Select ISNULL(max(TemplateID),0) as TemplateID from CustomTemplates Where CategoryId=9";
                templateID = DataAccess.ExecuteScalar(strSQLQuery);

                if (templateID == 0)
                {
                    templateID = 0 + 1;
                }
                else
                {
                    templateID = templateID + 1;
                }
                //strQueryCount = "Select ISNULL(max(TemplateID),0) as TemplateCount from CustomTemplates where  CategoryId=" + ddlCommon.SelectedValue + "";
                //templateCount = DataAccess.ExecuteScalar(strQueryCount);

                strQueryCheck = "Select * from CustomTemplates where TemplateName='" + txtExpiryTemplateName.Text + "' AND CategoryId=9";
                dsTable = DataAccess.FetchRS(CommandType.Text, strQueryCheck, null);
                dtTable = dsTable.Tables[0];

                if (dtTable.Rows.Count == 0)
                {
                    foreach (GridItem item in RadGrid17.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                int ColumnID = Convert.ToInt32(this.RadGrid17.Items[dataItem.ItemIndex]["id"].Text.ToString());
                                string AliasName = Convert.ToString(this.RadGrid17.Items[dataItem.ItemIndex]["description"].Text.ToString());
                                string RelationName = Convert.ToString(this.RadGrid17.Items[dataItem.ItemIndex]["description"].Text.ToString());
                                string CategoryName = "Leave";
                                int CategoryId = 9;
                                string replacedRelation = RelationName.Replace("'", "''");
                                int TableID = 9;

                                strssqlb = "INSERT INTO CustomTemplates(TemplateID,TemplateName,ColumnID,CategoryId,CategoryName,MainCategory,Company_Id,ALIAS_NAME,RELATION,TableID,AddedDate)VALUES(" + templateID + ",'" + txtExpiryTemplateName.Text + "'," + ColumnID + "," + CategoryId + ",'" + CategoryName + "','" + CategoryName + "'," + Utility.ToInteger(Session["Compid"]) + ",'" + AliasName.Replace(" ", "") + "','" + replacedRelation + "'," + TableID + ",GETDATE())";
                                retVal += DataAccess.ExecuteStoreProc(strssqlb);
                            }
                        }
                    }

                }
                else
                {
                    ShowMessageBox("Template name is already exist");
                }
                if (retVal > 0)
                {
                    ShowMessageBox("Template Items Added Successfully");
                    txtExpiryTemplateName.Text = string.Empty;
                    Response.Redirect("../Reports/CustomReportMainPage.aspx");

                }
                else
                {
                    ShowMessageBox("Template is not added");
                }
            }

        }
        #endregion
    }
}
