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
    public partial class CommonReports : System.Web.UI.Page
    {
        int templateId = 0;
        string compid = "";
        DataSet rptDs;
        protected string sUserName = "", sgroupname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            SqlDataSource8.ConnectionString = Session["ConString"].ToString();
            SqlDataSource4.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
               
                btnCurrentMonth.Enabled = false;
                btnPreviousMonth.Enabled = false;
                btnThreeMonth.Enabled = false;
                btnSixMonth.Enabled = false;
                btnOneYear.Enabled = false;
                Session["TemplateName"] = "";
                Session["TemplateId"] = "";
                Session["CategoryId"] = "";
                string sql = "Select * From Categories where ActiveStatus=1";
                DataSet dsts = new DataSet();
                dsts = DataAccess.FetchRS(CommandType.Text, sql, null);
                dlCategory.DataSource = dsts;
                dlCategory.DataBind();

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
        protected void DataList1_ItemCommand(object sender, DataListCommandEventArgs  e)
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
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (((System.Web.UI.WebControls.ImageButton)sender).ID == "imgbtnfetch")
            {
                if (RadDatePicker9.SelectedDate.HasValue)
                {
                    if (RadDatePicker10.SelectedDate.HasValue)
                    {
                        string startDate = Convert.ToDateTime(RadDatePicker9.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        string endDate = Convert.ToDateTime(RadDatePicker10.SelectedDate.Value.ToShortDateString()).ToString("dd/MM/yyyy", format);
                        if (Session["CategoryId"].ToString() != "")
                        {
                            if (Session["TemplateId"].ToString() != "")
                            {
                                //Response.Redirect("../Reports/CommonReportSample.aspx?PageType=26");
                               GenerateCommonReportsByStartDate(startDate, endDate, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));
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
        protected void GenerateCommonReportsByStartDate(string startDate, string endDate,int CategoryId,int TemplateID)
        {
            Calendar cr =new  Calendar();
            int sMonth=0;
            int sYear=0;
            int eMonth=0;
            int eYear=0;
            sMonth =RadDatePicker9.SelectedDate.Value.Month;
            sYear = RadDatePicker9.SelectedDate.Value.Year;
            eMonth = RadDatePicker10.SelectedDate.Value.Month;
            eYear = RadDatePicker10.SelectedDate.Value.Year;
            if (startDate !="" && endDate != "")
            {
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                string sqlQuery = "";
                string strEmployee = "0";
                string sqlSelect = "select e1.emp_code Emp_Code,(select time_card_no from employee where emp_code=e1.emp_code) TimeCardId, isnull(e1.emp_name,'')+' '+isnull(e1.emp_lname,'') Full_Name, ";
                
                int grid1 = 0;
                int grid2 = 0;
                string sqlTrnsType = "0";
                string sqlPayStr = "";
                string sqlAdditionStr = "";
                string selectSQL = "";
                string sqlStr = "";
                DataTable dtTable = new DataTable();
                DataSet dsTable = new DataSet();
                DataTable dtTableAdd = new DataTable();
                DataSet dsTableAdd = new DataSet();
                DataTable dtEmpResult = new DataTable();
                DataSet dsEmpResult = new DataSet();
                selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + "";
                dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                dtTable = dsTable.Tables[0];
                sqlStr = "Sp_getpivotclaimsadditionscustom";

                sqlPayStr = "Select Count(*) from PayRollView1 WHERE Convert(Datetime,start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) AND Convert(Datetime,end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) AND STATUS in ('G')";
                int payCount = DataAccess.ExecuteScalar(sqlPayStr, null);

                sqlAdditionStr = "Select Count(*) from ViewEmployeeAdditionsDeductionClaims  WHERE Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And Status='L'";
                int additionCount = DataAccess.ExecuteScalar(sqlAdditionStr, null);
                if (startDate != "" && endDate != "")
                {
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        if (dtTable.Rows[i]["TableID"].ToString() == "3" || dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                        {
                            sqlTrnsType = sqlTrnsType + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
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

                if (payCount > 0)
                {
                    if (additionCount > 0)
                    {

                        string pivotQuery = " ";
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            grid2++;
                            if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                            {
                                if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                                {
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                    {
                                        //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        // str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(startDate) + " and YEAR(start_period)=" + Convert.ToInt32(startDate) + " and MONTH(end_period)=" + Convert.ToInt32(endDate) + " and YEAR(end_period)=" + Convert.ToInt32(endDate) + " AND status='G' order by emp_code Desc)";
                                        str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)='" + Convert.ToInt32(sMonth) + "' and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else
                                    {
                                        string str = "";
                                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                                " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                        {

                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                        {
                                            str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                        {
                                            str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                            pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                        {
                                            str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                        {
                                            str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                    }

                                }
                            }
                        }

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {

                                bool sqlSelect1 = true;
                                bool sqlSelect2 = false;
                                bool sqlSelect3 = false;
                                bool sqlSelect4 = false;


                                sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                                string tempQuery = ", VE.ALIAS_NAME as ALIAS_NAME,SUM(VE.TRX_AMOUNT) AS Amount INTO TempTable";

                                if (startDate != "" && endDate != "")
                                {
                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + tempQuery;
                                            break;
                                        }
                                    }

                                }

                                sqlSelect = sqlSelect + " from Employee e1";
                                if (startDate != "" && endDate != "")
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect1 = true;
                                            break;
                                        }
                                    }
                                }

                                if (startDate != "" && endDate != "")
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + " INNER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                            sqlSelect2 = true;
                                            break;
                                        }
                                    }
                                }

                                if (startDate != "" && endDate != "")
                                {

                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + " INNER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                            sqlSelect3 = true;

                                            break;
                                        }
                                    }

                                }

                                if (startDate != "" && endDate != "")
                                {

                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect4 = true;
                                            break;
                                        }
                                    }
                                }

                                if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";

                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {
                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        sqlSelect = sqlSelect + " And Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        //  sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                    //sqlSelect = sqlSelect + " ORDER BY e1.EMP_NAME; ";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;;";
                                }

                                if (sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    SqlParameter[] parms = new SqlParameter[11];
                                    parms[0] = new SqlParameter("@empcode", strEmployee);
                                    parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                    parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                    parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                    parms[4] = new SqlParameter("@claimtype", 1);
                                    parms[5] = new SqlParameter("@addtype", "ALL");
                                    parms[6] = new SqlParameter("@stattype", 'L');
                                    parms[7] = new SqlParameter("@claimstatus", 1);
                                    parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                    parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                    parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                    rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                                }
                                else
                                {
                                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                }

                                // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                    //if (drpCurrency.SelectedValue == "2")
                                    //{
                                    //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //    {
                                    //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //        while (drcurrb.Read())
                                    //        {
                                    //            if (drcurrb.GetValue(0) != null)
                                    //            {
                                    //                basecurr = drcurrb.GetValue(0).ToString();
                                    //            }
                                    //        }
                                    //        if (basecurr == "1")
                                    //        {
                                    //            basecurr = "SGD";
                                    //        }
                                    //        else
                                    //        {
                                    //            basecurr = "UGD";
                                    //        }

                                    //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //        //{

                                    //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //        {
                                    //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //            {
                                    //                if (dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                        {
                                    //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }

                                    //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }
                                    //            }
                                    //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //            {
                                    //                //dr.BeginEdit();
                                    //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                //dr["BasicPayConversion"] = val.ToString();
                                    //                //dr.AcceptChanges();
                                    //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                    //                            decimal newValue = decimal.Round(value, 2);
                                    //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                    //                        }
                                    //                        dr.AcceptChanges();
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}

                                    //foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //{
                                    //    //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //    string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //    SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //    while (drcurrb.Read())
                                    //    {
                                    //        if (drcurrb.GetValue(0) != null)
                                    //        {
                                    //            basecurr = drcurrb.GetValue(0).ToString();
                                    //        }
                                    //    }
                                    //    if (basecurr == "1")
                                    //    {
                                    //        basecurr = "SGD";
                                    //    }
                                    //    else
                                    //    {
                                    //        basecurr = "UGD";
                                    //    }
                                    //    //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //    //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //    //{
                                    //    if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //    {
                                    //        if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //        {
                                    //            dr.BeginEdit();
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                    //                {
                                    //                    double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                    dr["BasicPayConversion"] = Math.Round(val, 2);
                                    //                }
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //        {
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                dr.BeginEdit();
                                    //                double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                dr["BasicPayConversion"] = val.ToString();
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}
                                }
                            }
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Employee Name');", true);

                        }
                    }
                    else
                    {
                        string pivotQuery = " ";
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            grid2++;
                            if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                            {
                                if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                                {
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                    {
                                        //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        // str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(startDate) + " and YEAR(start_period)=" + Convert.ToInt32(startDate) + " and MONTH(end_period)=" + Convert.ToInt32(endDate) + " and YEAR(end_period)=" + Convert.ToInt32(endDate) + " AND status='G' order by emp_code Desc)";
                                        str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)='" + Convert.ToInt32(sMonth) + "' and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else
                                    {
                                        string str = "";
                                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                                " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                        {

                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                        {
                                            str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                        {
                                            str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                            pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                        {
                                            str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                        {
                                            str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                    }

                                }
                            }

                        }

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {

                                bool sqlSelect1 = true;
                                bool sqlSelect2 = false;
                                bool sqlSelect3 = false;
                                bool sqlSelect4 = false;


                                sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                                string tempQuery = ", (SELECT VE.ALIAS_NAME from ViewEmployeeAdditionsDeductionClaims VE where Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L') as ALIAS_NAME   , (SELECT SUM(VE.TRX_AMOUNT) from ViewEmployeeAdditionsDeductionClaims VE where Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L') AS Amount INTO TempTable";

                                if (startDate != "" && endDate != "")
                                {
                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + tempQuery;
                                            break;
                                        }
                                    }

                                }

                                sqlSelect = sqlSelect + " from Employee e1";
                                if (startDate != "" && endDate != "")
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect1 = true;
                                            break;
                                        }
                                    }
                                }

                                if (startDate != "" && endDate != "")
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + " INNER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                            sqlSelect2 = true;
                                            break;
                                        }
                                    }
                                }

                                if (startDate != "" && endDate != "")
                                {

                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                           // sqlSelect = sqlSelect + " INNER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                            sqlSelect3 = true;

                                            break;
                                        }
                                    }

                                }

                                if (startDate != "" && endDate != "")
                                {

                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect4 = true;
                                            break;
                                        }
                                    }
                                }

                                if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";

                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {
                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        //sqlSelect = sqlSelect + " And Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        //  sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                    //sqlSelect = sqlSelect + " ORDER BY e1.EMP_NAME; ";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode ORDER BY e1.EMP_NAME;;";
                                }

                                if (sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    SqlParameter[] parms = new SqlParameter[11];
                                    parms[0] = new SqlParameter("@empcode", strEmployee);
                                    parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                    parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                    parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                    parms[4] = new SqlParameter("@claimtype", 1);
                                    parms[5] = new SqlParameter("@addtype", "ALL");
                                    parms[6] = new SqlParameter("@stattype", 'L');
                                    parms[7] = new SqlParameter("@claimstatus", 1);
                                    parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                    parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                    parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                    rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                                }
                                else
                                {
                                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                }

                                // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                    //if (drpCurrency.SelectedValue == "2")
                                    //{
                                    //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //    {
                                    //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //        while (drcurrb.Read())
                                    //        {
                                    //            if (drcurrb.GetValue(0) != null)
                                    //            {
                                    //                basecurr = drcurrb.GetValue(0).ToString();
                                    //            }
                                    //        }
                                    //        if (basecurr == "1")
                                    //        {
                                    //            basecurr = "SGD";
                                    //        }
                                    //        else
                                    //        {
                                    //            basecurr = "UGD";
                                    //        }

                                    //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //        //{

                                    //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //        {
                                    //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //            {
                                    //                if (dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                        {
                                    //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }

                                    //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }
                                    //            }
                                    //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //            {
                                    //                //dr.BeginEdit();
                                    //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                //dr["BasicPayConversion"] = val.ToString();
                                    //                //dr.AcceptChanges();
                                    //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                    //                            decimal newValue = decimal.Round(value, 2);
                                    //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                    //                        }
                                    //                        dr.AcceptChanges();
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}

                                    //foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //{
                                    //    //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //    string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //    SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //    while (drcurrb.Read())
                                    //    {
                                    //        if (drcurrb.GetValue(0) != null)
                                    //        {
                                    //            basecurr = drcurrb.GetValue(0).ToString();
                                    //        }
                                    //    }
                                    //    if (basecurr == "1")
                                    //    {
                                    //        basecurr = "SGD";
                                    //    }
                                    //    else
                                    //    {
                                    //        basecurr = "UGD";
                                    //    }
                                    //    //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //    //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //    //{
                                    //    if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //    {
                                    //        if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //        {
                                    //            dr.BeginEdit();
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                    //                {
                                    //                    double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                    dr["BasicPayConversion"] = Math.Round(val, 2);
                                    //                }
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //        {
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                dr.BeginEdit();
                                    //                double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                dr["BasicPayConversion"] = val.ToString();
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}
                                }
                            }
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Employee Name');", true);

                        }
                    }

                }
                else
                {
                    string pivotQuery = " ";
                    for (int i = 0; i < dtTable.Rows.Count; i++)
                    {
                        grid2++;
                        if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                        {
                            if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                            {
                                if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                {
                                    //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                    sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                }
                                else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                {
                                    pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    string str1 = "";
                                    // str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(startDate) + " and YEAR(start_period)=" + Convert.ToInt32(startDate) + " and MONTH(end_period)=" + Convert.ToInt32(endDate) + " and YEAR(end_period)=" + Convert.ToInt32(endDate) + " AND status='G' order by emp_code Desc)";
                                    str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)='" + Convert.ToInt32(sMonth) + "' and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(eMonth) + " and YEAR(end_period)=" + Convert.ToInt32(eYear) + " AND status='G' order by emp_code Desc)";

                                    sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                }
                                else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                {
                                    pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    string str1 = "";
                                    str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                    sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                }
                                else
                                {
                                    string str = "";
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                    {

                                        str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                            " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                    {

                                        str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }

                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }

                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                    {

                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                    {
                                        str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                    {
                                        str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                        pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                    {
                                        str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                    {
                                        str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                    {
                                        str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                    {
                                        str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                    }
                                }

                            }
                        }
                    }

                    if (grid1 > 0)
                    {
                        if (grid2 > 0)
                        {

                            bool sqlSelect1 = true;
                            bool sqlSelect2 = false;
                            bool sqlSelect3 = false;
                            bool sqlSelect4 = false;


                            sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                            string tempQuery = ", (SELECT VE.ALIAS_NAME from ViewEmployeeAdditionsDeductionClaims VE where Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L') as ALIAS_NAME   , (SELECT SUM(VE.TRX_AMOUNT) from ViewEmployeeAdditionsDeductionClaims VE where Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L') AS Amount INTO TempTable";

                            if (startDate != "" && endDate != "")
                            {
                                for (int j = 0; j < dtTable.Rows.Count; j++)
                                {
                                    if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                    {
                                        sqlSelect = sqlSelect + tempQuery;
                                        break;
                                    }
                                }

                            }

                            sqlSelect = sqlSelect + " from Employee e1";
                            if (startDate != "" && endDate != "")
                            {
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                    {
                                        sqlSelect1 = true;
                                        break;
                                    }
                                }
                            }

                            if (startDate != "" && endDate != "")
                            {
                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                    {
                                        sqlSelect = sqlSelect + " INNER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                        sqlSelect2 = true;
                                        break;
                                    }
                                }
                            }

                            if (startDate != "" && endDate != "")
                            {

                                for (int j = 0; j < dtTable.Rows.Count; j++)
                                {
                                    if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                    {
                                        //sqlSelect = sqlSelect + " INNER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                        sqlSelect3 = true;

                                        break;
                                    }
                                }

                            }

                            if (startDate != "" && endDate != "")
                            {

                                for (int i = 0; i < dtTable.Rows.Count; i++)
                                {
                                    if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                    {
                                        sqlSelect4 = true;
                                        break;
                                    }
                                }
                            }

                            if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                            {

                                sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";

                            }
                            else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                            {

                                sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                            }
                            else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                            {
                                if (sqlTrnsType.Length != 1)
                                {
                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    //sqlSelect = sqlSelect + " And Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                    //  sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                }
                            }
                            else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                            {
                                if (sqlTrnsType.Length != 1)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                    //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                }
                            }
                            else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                            {
                                if (sqlTrnsType.Length != 1)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.start_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " and Convert(Datetime,pv.end_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103)";
                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.CPFGrossAmount,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh,pv.start_period, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                    //sqlSelect = sqlSelect + " ORDER BY pv.EMP_NAME;";

                                }
                            }
                            else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                            {
                                sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                //sqlSelect = sqlSelect + " AND Convert(Datetime,trx_period,103) between Convert(Datetime,'" + startDate + "',103) And Convert(Datetime,'" + endDate + "',103) And VE.Status='L'";
                                //sqlSelect = sqlSelect + " ORDER BY e1.EMP_NAME; ";
                                sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode ORDER BY e1.EMP_NAME;;";
                            }

                            if (sqlSelect3 == true || sqlSelect4 == true)
                            {
                                SqlParameter[] parms = new SqlParameter[11];
                                parms[0] = new SqlParameter("@empcode", strEmployee);
                                parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                parms[4] = new SqlParameter("@claimtype", 1);
                                parms[5] = new SqlParameter("@addtype", "ALL");
                                parms[6] = new SqlParameter("@stattype", 'L');
                                parms[7] = new SqlParameter("@claimstatus", 1);
                                parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                            }
                            else
                            {
                                rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                            }

                            // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                //if (drpCurrency.SelectedValue == "2")
                                //{
                                //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                //    {
                                //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                //        while (drcurrb.Read())
                                //        {
                                //            if (drcurrb.GetValue(0) != null)
                                //            {
                                //                basecurr = drcurrb.GetValue(0).ToString();
                                //            }
                                //        }
                                //        if (basecurr == "1")
                                //        {
                                //            basecurr = "SGD";
                                //        }
                                //        else
                                //        {
                                //            basecurr = "UGD";
                                //        }

                                //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                //        //{

                                //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                //        {
                                //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                //            {
                                //                if (dr.Table.Columns["Basic Pay"] != null)
                                //                {
                                //                    dr.BeginEdit();
                                //                    if (dr["Basic Pay"].ToString() != "")
                                //                    {
                                //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                //                        {
                                //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                //                        }
                                //                    }
                                //                    dr.AcceptChanges();
                                //                }

                                //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                //                {
                                //                    dr.BeginEdit();
                                //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                //                    {
                                //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                //                        {
                                //                            dr["Exchange Rate"] = val.ToString();
                                //                        }
                                //                    }
                                //                    dr.AcceptChanges();
                                //                }
                                //            }
                                //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                //            {
                                //                //dr.BeginEdit();
                                //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                //                //dr["BasicPayConversion"] = val.ToString();
                                //                //dr.AcceptChanges();
                                //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                //                {
                                //                    dr.BeginEdit();
                                //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                //                    {
                                //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                //                        if (val.ToString() != "NaN")
                                //                        {
                                //                            dr["Exchange Rate"] = val.ToString();
                                //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                //                            decimal newValue = decimal.Round(value, 2);
                                //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                //                        }
                                //                        dr.AcceptChanges();
                                //                    }
                                //                }
                                //            }
                                //        }
                                //        //}
                                //    }
                                //}

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
                        Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Employee Name');", true);

                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "monthyear", "alert('Please Select month and year');", true);
            }

        }
        protected void ButtonMonthSelection_Click(object sender, System.EventArgs e)
        {
            Calendar c = new Calendar();

            //int mDay = c.get(Calendar.DAY_OF_MONTH);
            int mYear = 0;
            int sMonth = 0;
            int eMonth = 0;
           
            if (((System.Web.UI.WebControls.Button)sender).ID == "btnCurrentMonth")
            {

                mYear = c.TodaysDate.Year;
                sMonth = c.TodaysDate.Month;
                eMonth = 0;
                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        GenerateCommonReports(sMonth, eMonth, mYear, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));
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
                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        GenerateCommonReports(sMonth, eMonth, mYear, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));
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

                //int mDay = c.get(Calendar.DAY_OF_MONTH);
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        GenerateCommonReports(sMonth, eMonth, mYear, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));
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
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        GenerateCommonReports(sMonth, eMonth, mYear, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));
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
                if (Session["CategoryId"].ToString() != "")
                {
                    if (Session["TemplateId"].ToString() != "")
                    {
                        GenerateCommonReports(sMonth, eMonth, mYear, Convert.ToInt32(Session["CategoryId"].ToString()), Convert.ToInt32(Session["TemplateId"].ToString()));                  
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
            DataList dlCategoryList = Page.FindControl("dlCategory") as DataList;
            foreach (DataListItem question in dlCategoryList.Items)
            {
                RadioButtonList rdList = question.FindControl("rdEmployeeList") as RadioButtonList;             
                foreach (ListItem answer in rdList.Items)
                {
                   // rdList.SelectedIndex = -1;
                    bool isSelected = answer.Selected;
                    if (isSelected)
                    {
                        Label itemCategoryID = ((Label)question.FindControl("lblCategoryID")) as Label;
                        Session["CategoryId"] = itemCategoryID.Text;
                        Session["TemplateId"] = answer.Value;
                        Session["TemplateName"] = answer.Text;
                        rdList.ClearSelection();
                    }
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

                    btnCurrentMonth.Enabled = true;
                    btnPreviousMonth.Enabled = true;
                    btnThreeMonth.Enabled = true;
                    btnSixMonth.Enabled = true;
                    btnOneYear.Enabled = true;
                    //if (dlDept.SelectedValue == "-1")
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
                    //else
                    //    sqlSelect = "SELECT  EMP_CODE,[NAME] = Case When termination_date is null Then (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,''))  Else (isnull(EMP_NAME,'') +' ' + isnull(EMP_LNAME,'')) + '[Terminated]' End FROM dbo.employee WHERE DEPT_ID = " + dlDept.SelectedValue + " AND COMPANY_ID= " + compid + " ORDER BY EMP_NAME";
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
        protected void GenerateCommonReports(int sMonth,int eMonth,int sYear,int CategoryId,int TemplateID)
        {
         
           
            if (sMonth!= 0 && sYear!=0)
            {
                    IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                    string sqlQuery = "";
                    string strEmployee = "0";
  
                    string sqlSelect = "select e1.emp_code Emp_Code,(select time_card_no from employee where emp_code=e1.emp_code) TimeCardId,(select isnull(emp_name,'')+' '+isnull(emp_lname,'') from employee where emp_code=e1.emp_code)  Full_Name,";
                   
                    int grid1 = 0;
                    int grid2 = 0;
                    string sqlTrnsType = "0";
                    string selectSQL = "";
                    string sqlStr = "";
                    string sqlAdditionStr = "";
                    string sqlPayStr = "";
                  
                    DataTable dtTable = new DataTable();
                    DataSet dsTable = new DataSet();
                    DataTable dtTableAdd = new DataTable();
                    DataSet dsTableAdd = new DataSet();
                    DataTable dtEmpResult = new DataTable();
                    DataSet dsEmpResult = new DataSet();
                    selectSQL = "Select * from CustomTemplates WHERE TemplateID=" + TemplateID + " AND CategoryId=" + CategoryId +"";
                    dsTable = DataAccess.FetchRS(CommandType.Text, selectSQL, null);
                    dtTable = dsTable.Tables[0];
                    sqlStr = "Sp_getpivotclaimsadditionscustom";

                    sqlPayStr = "Select Count(*)   from PayRollView1 WHERE month(start_period)=" + Convert.ToInt32(sMonth) + " AND Year(start_period)=" + Convert.ToInt32(sYear) + " AND STATUS in ('G')";
                    int payCount = DataAccess.ExecuteScalar(sqlPayStr, null);
                    

                    sqlAdditionStr = "Select Count(*) from ViewEmployeeAdditionsDeductionClaims  WHERE month(trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(trx_period)=" + Convert.ToInt32(sYear) + " And Status='L'";
                    int additionCount = DataAccess.ExecuteScalar(sqlAdditionStr, null);
                  
                   if (sMonth!= 0 && sYear!=0)
                    {
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {                        
                            if (dtTable.Rows[i]["TableID"].ToString() == "3" || dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                            {
                                sqlTrnsType = sqlTrnsType + "," + dtTable.Rows[i]["ColumnID"].ToString().Trim();
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

                    if (payCount > 0)
                    {
                        if (additionCount > 0)
                        {
                            string pivotQuery = " ";
                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                grid2++;
                                if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                                {
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                                    {
                                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                        {
                                            //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            string str1 = "";
                                            str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(sMonth) + " and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(sMonth) + " and YEAR(end_period)=" + Convert.ToInt32(sYear) + " AND status='G' order by emp_code Desc)";

                                            sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            //pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            string str1 = "";
                                            str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                            sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else
                                        {
                                            string str = "";
                                            if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                            {

                                                str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                                    " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                            {
                                                
                                                str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }

                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }

                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                            {

                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                            {
                                                str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                            {
                                                str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                                pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                            {
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                            {
                                                str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                            {
                                                str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                            {
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                        }

                                    }
                                }

                            }

                            if (grid1 > 0)
                            {
                                if (grid2 > 0)
                                {

                                    bool sqlSelect1 = true;
                                    bool sqlSelect2 = false;
                                    bool sqlSelect3 = false;
                                    bool sqlSelect4 = false;

                                   
                                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                                    string tempQuery = ", VE.ALIAS_NAME as ALIAS_NAME,SUM(VE.TRX_AMOUNT) AS Amount INTO TempTable";
                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int j = 0; j < dtTable.Rows.Count; j++)
                                        {
                                            if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect = sqlSelect + tempQuery;

                                                break;
                                            }
                                        }

                                    }

                                    sqlSelect = sqlSelect + " from Employee e1";
                                    if (sMonth != 0 && sYear != 0)
                                    {
                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {

                                                sqlSelect1 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {
                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect = sqlSelect + " LEFT OUTER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                                sqlSelect2 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int j = 0; j < dtTable.Rows.Count; j++)
                                        {
                                            if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect = sqlSelect + " LEFT OUTER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                                sqlSelect3 = true;

                                                break;
                                            }
                                        }

                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                if (sqlSelect3 != true)
                                                {
                                                    sqlSelect = sqlSelect + " LEFT OUTER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                                }
                                                sqlSelect4 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email] order by empname;";

                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                        }

                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount] order by empname;";
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {
                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " AND Year(trx_period)=" + Convert.ToInt32(sYear) + " and month(trx_period)  between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + " And VE.Status='L'";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L'";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";

                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {

                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " AND Year(trx_period)=" + Convert.ToInt32(sYear) + " and month(trx_period)  between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + " And VE.Status='L'";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L'";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";
                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {

                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " AND Year(trx_period)=" + Convert.ToInt32(sYear) + " and month(trx_period)  between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + " And VE.Status='L'";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " AND  month(trx_period)=" + Convert.ToInt32(sMonth) + " AND Year(trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L'";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.start_period,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY e1.EMP_NAME;";
                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                                    {
                                        sqlSelect = sqlSelect + "  where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and  Year(trx_period)=" + Convert.ToInt32(sYear) + " and month(trx_period)  between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + " And VE.Status='L'";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L'";
                                        }
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,VE.ALIAS_NAME,VE.TRX_AMOUNT ORDER BY EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],ALIAS_NAME,Amount order by empname;";
                                    }
                                    if (sqlSelect3 == true || sqlSelect4 == true)
                                    {
                                        SqlParameter[] parms = new SqlParameter[11];
                                        parms[0] = new SqlParameter("@empcode", strEmployee);
                                        parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                        parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                        parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                        parms[4] = new SqlParameter("@claimtype", 1);
                                        parms[5] = new SqlParameter("@addtype", "ALL");
                                        parms[6] = new SqlParameter("@stattype", 'L');
                                        parms[7] = new SqlParameter("@claimstatus", 1);
                                        parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                        parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                        parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                        rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                                    }
                                    else
                                    {
                                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                    }

                                    // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                        //if (drpCurrency.SelectedValue == "2")
                                        //{
                                        //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                        //    {
                                        //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                        //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                        //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                        //        while (drcurrb.Read())
                                        //        {
                                        //            if (drcurrb.GetValue(0) != null)
                                        //            {
                                        //                basecurr = drcurrb.GetValue(0).ToString();
                                        //            }
                                        //        }
                                        //        if (basecurr == "1")
                                        //        {
                                        //            basecurr = "SGD";
                                        //        }
                                        //        else
                                        //        {
                                        //            basecurr = "UGD";
                                        //        }

                                        //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                        //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                        //        //{

                                        //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                        //        {
                                        //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                        //            {
                                        //                if (dr.Table.Columns["Basic Pay"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr["Basic Pay"].ToString() != "")
                                        //                    {
                                        //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                        //                        {
                                        //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                        //                        }
                                        //                    }
                                        //                    dr.AcceptChanges();
                                        //                }

                                        //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                        //                    {
                                        //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                        //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                        //                        {
                                        //                            dr["Exchange Rate"] = val.ToString();
                                        //                        }
                                        //                    }
                                        //                    dr.AcceptChanges();
                                        //                }
                                        //            }
                                        //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                        //            {
                                        //                //dr.BeginEdit();
                                        //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                //dr["BasicPayConversion"] = val.ToString();
                                        //                //dr.AcceptChanges();
                                        //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //                    {
                                        //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                        //                        if (val.ToString() != "NaN")
                                        //                        {
                                        //                            dr["Exchange Rate"] = val.ToString();
                                        //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                        //                            decimal newValue = decimal.Round(value, 2);
                                        //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                        //                        }
                                        //                        dr.AcceptChanges();
                                        //                    }
                                        //                }
                                        //            }
                                        //        }
                                        //        //}
                                        //    }
                                        //}

                                        //foreach (DataRow dr in rptDs.Tables[0].Rows)
                                        //{
                                        //    //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                        //    string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                        //    SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                        //    while (drcurrb.Read())
                                        //    {
                                        //        if (drcurrb.GetValue(0) != null)
                                        //        {
                                        //            basecurr = drcurrb.GetValue(0).ToString();
                                        //        }
                                        //    }
                                        //    if (basecurr == "1")
                                        //    {
                                        //        basecurr = "SGD";
                                        //    }
                                        //    else
                                        //    {
                                        //        basecurr = "UGD";
                                        //    }
                                        //    //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                        //    //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                        //    //{
                                        //    if (dr.Table.Columns["Basic Pay Currency"] != null)
                                        //    {
                                        //        if (dr["Basic Pay Currency"].ToString() != "SGD")
                                        //        {
                                        //            dr.BeginEdit();
                                        //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //            {
                                        //                if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                        //                {
                                        //                    double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                    dr["BasicPayConversion"] = Math.Round(val, 2);
                                        //                }
                                        //                dr.AcceptChanges();
                                        //            }
                                        //        }
                                        //        if (dr["Basic Pay Currency"].ToString() == "SGD")
                                        //        {
                                        //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //            {
                                        //                dr.BeginEdit();
                                        //                double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                dr["BasicPayConversion"] = val.ToString();
                                        //                dr.AcceptChanges();
                                        //            }
                                        //        }
                                        //        //}
                                        //    }
                                        //}
                                    }
                                }
                                Session["rptDs"] = rptDs;
                                Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);

                            }
                        }
                        else
                        {
                            string pivotQuery = " ";
                            for (int i = 0; i < dtTable.Rows.Count; i++)
                            {
                                grid2++;
                                if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                                {
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                                    {
                                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                        {
                                            //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            string str1 = "";
                                            str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(sMonth) + " and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(sMonth) + " and YEAR(end_period)=" + Convert.ToInt32(sYear) + " AND status='G' order by emp_code Desc)";

                                            sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            //pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            string str1 = "";
                                            str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                            sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else
                                        {
                                            string str = "";
                                            if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                            {

                                                str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                                    " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                            {

                                                str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }

                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }

                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                            {

                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                            {
                                                str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                            {
                                                str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                                pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                            {
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                            {
                                                str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                            {
                                                str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                            else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                            {
                                                str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            }
                                            else
                                            {
                                                pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                                sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                            }
                                        }

                                    }
                                }

                            }

                            if (grid1 > 0)
                            {
                                if (grid2 > 0)
                                {

                                    bool sqlSelect1 = true;
                                    bool sqlSelect2 = false;
                                    bool sqlSelect3 = false;
                                    bool sqlSelect4 = false;


                                    sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                                    string tempQuery = ", (SELECT VE.ALIAS_NAME from ViewEmployeeAdditionsDeductionClaims VE where month(VE.trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(VE.trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L') as ALIAS_NAME   , (SELECT SUM(VE.TRX_AMOUNT) from ViewEmployeeAdditionsDeductionClaims VE where  month(VE.trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(VE.trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L') AS Amount INTO TempTable";
                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int j = 0; j < dtTable.Rows.Count; j++)
                                        {
                                            if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect = sqlSelect + tempQuery;

                                                break;
                                            }
                                        }

                                    }

                                    sqlSelect = sqlSelect + " from Employee e1";
                                    if (sMonth != 0 && sYear != 0)
                                    {
                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {

                                                sqlSelect1 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {
                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect = sqlSelect + " LEFT OUTER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                                sqlSelect2 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int j = 0; j < dtTable.Rows.Count; j++)
                                        {
                                            if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                //sqlSelect = sqlSelect + " LEFT OUTER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                                sqlSelect3 = true;

                                                break;
                                            }
                                        }

                                    }

                                    if (sMonth != 0 && sYear != 0)
                                    {

                                        for (int i = 0; i < dtTable.Rows.Count; i++)
                                        {
                                            if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                            {
                                                sqlSelect4 = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email] order by empname;";
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + " ";
                                        }

                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount] order by empname;";
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {
                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + " ";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {

                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                    {
                                        if (sqlTrnsType.Length != 1)
                                        {

                                            sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                            }

                                            sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                            if (sMonth != 0 && eMonth != 0)
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            else
                                            {
                                                sqlSelect = sqlSelect + "";
                                            }
                                            sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.start_period,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                            //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                        }
                                    }
                                    else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                                    {
                                        sqlSelect = sqlSelect + "  where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode ORDER BY EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],ALIAS_NAME,Amount order by empname;";
                                    }

                                    if (sqlSelect3 == true || sqlSelect4 == true)
                                    {
                                        SqlParameter[] parms = new SqlParameter[11];
                                        parms[0] = new SqlParameter("@empcode", strEmployee);
                                        parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                        parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                        parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                        parms[4] = new SqlParameter("@claimtype", 1);
                                        parms[5] = new SqlParameter("@addtype", "ALL");
                                        parms[6] = new SqlParameter("@stattype", 'L');
                                        parms[7] = new SqlParameter("@claimstatus", 1);
                                        parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                        parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                        parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                        rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                                    }
                                    else
                                    {
                                        rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                    }
                                    // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                        //if (drpCurrency.SelectedValue == "2")
                                        //{
                                        //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                        //    {
                                        //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                        //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                        //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                        //        while (drcurrb.Read())
                                        //        {
                                        //            if (drcurrb.GetValue(0) != null)
                                        //            {
                                        //                basecurr = drcurrb.GetValue(0).ToString();
                                        //            }
                                        //        }
                                        //        if (basecurr == "1")
                                        //        {
                                        //            basecurr = "SGD";
                                        //        }
                                        //        else
                                        //        {
                                        //            basecurr = "UGD";
                                        //        }

                                        //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                        //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                        //        //{

                                        //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                        //        {
                                        //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                        //            {
                                        //                if (dr.Table.Columns["Basic Pay"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr["Basic Pay"].ToString() != "")
                                        //                    {
                                        //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                        //                        {
                                        //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                        //                        }
                                        //                    }
                                        //                    dr.AcceptChanges();
                                        //                }

                                        //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                        //                    {
                                        //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                        //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                        //                        {
                                        //                            dr["Exchange Rate"] = val.ToString();
                                        //                        }
                                        //                    }
                                        //                    dr.AcceptChanges();
                                        //                }
                                        //            }
                                        //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                        //            {
                                        //                //dr.BeginEdit();
                                        //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                //dr["BasicPayConversion"] = val.ToString();
                                        //                //dr.AcceptChanges();
                                        //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                        //                {
                                        //                    dr.BeginEdit();
                                        //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //                    {
                                        //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                        //                        if (val.ToString() != "NaN")
                                        //                        {
                                        //                            dr["Exchange Rate"] = val.ToString();
                                        //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                        //                            decimal newValue = decimal.Round(value, 2);
                                        //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                        //                        }
                                        //                        dr.AcceptChanges();
                                        //                    }
                                        //                }
                                        //            }
                                        //        }
                                        //        //}
                                        //    }
                                        //}

                                        //foreach (DataRow dr in rptDs.Tables[0].Rows)
                                        //{
                                        //    //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                        //    string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                        //    SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                        //    while (drcurrb.Read())
                                        //    {
                                        //        if (drcurrb.GetValue(0) != null)
                                        //        {
                                        //            basecurr = drcurrb.GetValue(0).ToString();
                                        //        }
                                        //    }
                                        //    if (basecurr == "1")
                                        //    {
                                        //        basecurr = "SGD";
                                        //    }
                                        //    else
                                        //    {
                                        //        basecurr = "UGD";
                                        //    }
                                        //    //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                        //    //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                        //    //{
                                        //    if (dr.Table.Columns["Basic Pay Currency"] != null)
                                        //    {
                                        //        if (dr["Basic Pay Currency"].ToString() != "SGD")
                                        //        {
                                        //            dr.BeginEdit();
                                        //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //            {
                                        //                if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                        //                {
                                        //                    double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                    dr["BasicPayConversion"] = Math.Round(val, 2);
                                        //                }
                                        //                dr.AcceptChanges();
                                        //            }
                                        //        }
                                        //        if (dr["Basic Pay Currency"].ToString() == "SGD")
                                        //        {
                                        //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                        //            {
                                        //                dr.BeginEdit();
                                        //                double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                        //                dr["BasicPayConversion"] = val.ToString();
                                        //                dr.AcceptChanges();
                                        //            }
                                        //        }
                                        //        //}
                                        //    }
                                        //}
                                    }
                                }
                                Session["rptDs"] = rptDs;
                                Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);
                            }
                        }
                    }
                    else
                    {
                        string pivotQuery = " ";
                        for (int i = 0; i < dtTable.Rows.Count; i++)
                        {
                            grid2++;
                            if (dtTable.Rows[i]["TableID"].ToString().Trim() != "3" && dtTable.Rows[i]["TableID"].ToString().Trim() != "4" && dtTable.Rows[i]["TableID"].ToString().Trim() != "5")
                            {
                                if (dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_code" && dtTable.Rows[i]["RELATION"].ToString().Trim() != "emp_name")
                                {
                                    if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "payment_mode")
                                    {
                                        //sqlSelect = sqlSelect + " case  " + dataItem.Cells[4].Text.ToString().Trim() + " WHEN -1 THEN 'Cash' ELSE 'Cheque' END AS [" + dataItem.Cells[3].Text.ToString().Trim() + "],";
                                        sqlSelect = sqlSelect + " " + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        str1 = "(Select TOP 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),basic_pay))) from  PayRollView1 Where  emp_code=e1.emp_code and MONTH(start_period)=" + Convert.ToInt32(sMonth) + " and YEAR(start_period)=" + Convert.ToInt32(sYear) + " and MONTH(end_period)=" + Convert.ToInt32(sMonth) + " and YEAR(end_period)=" + Convert.ToInt32(sYear) + " AND status='G' order by emp_code Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Basic Pay Rate")
                                    {
                                        pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        //pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        string str1 = "";
                                        str1 = "(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc)";

                                        sqlSelect = sqlSelect + " " + str1 + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                    }
                                    else
                                    {
                                        string str = "";
                                        if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "BasicPayConversion")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 Convert(numeric(10,2), convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'),Payrate))) " +
                                                " from  EmployeePayHistory Where  Emp_ID=e1.emp_code order by ID Desc) *(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Id From Currency Where Currency='USD') and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103)   Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["RELATION"].ToString().Trim() == "Exchange Rate")
                                        {

                                            str = "CONVERT(numeric(10,2),(Select Top 1 rate From exchangeRate Where Currency_id IN (Select Top 1 C.Id from EmployeePayHistory EH INNER join currency C on  EH.CurrencyID = C.Id  Where  EH.Emp_ID=e1.emp_code order by EH.ID Desc) and CONVERT(Date,[Date],103) < CONVERT(Date,GETDATE(),103) Order by  Date Desc),2)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot1rate) From Employee where emp_code = e1.emp_code )";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime 2")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select SUM(ot2rate) From Employee where emp_code =  e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Sex")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select case  sex When 'M' Then 'Male' Else 'Female' End  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Agent name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Agent_Name From EmployeeAgent A Where ID = e1.agent_id)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Alias Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Race")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RACE FROM RACE WHERE ID=e1.RACE_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Religion")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT RELIGION FROM RELIGION WHERE ID=e1.RELIGION_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Marital Status")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT marital_status FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Place of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT place_of_birth FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Date of birth")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),date_of_birth,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Income Tax ID")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT income_taxid FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Nationality")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT NATIONALITY FROM NATIONALITY WHERE emp_code=e1.NATIONALITY_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "IC / FIN Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT ic_pp_number FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),pr_date,103) FROM Employee WHERE emp_code=e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Country")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT COUNTRY FROM COUNTRY WHERE ID=e1.country_ID)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Email Address")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT email FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Type")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT emp_type FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "PR Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT postal_code FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Phone Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Mobile Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT hand_phone FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }

                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT [desc] FROM Bank WHERE bank_code=e1.giro_bank)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_bank FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Branch Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_branch FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Giro Bank Account Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Designation")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Department")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT giro_acct_number FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Joining Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),joining_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Probation Period")
                                        {

                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT Case probation_period when -1 then 0 else probation_period End FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Confirmation Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),confirmation_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT convert(nvarchar(10),termination_date,103) FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "CPF Entitiled")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employee Group")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(SELECT cpf_entitlement FROM Employee WHERE emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Leave Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Overtime Entitlement")
                                        {
                                            str = "(SELECT ot_entitlement FROM Employee WHERE emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Payment Mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreignworker Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select fw_code From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Eamil Payslip")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select email_payslip From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Termination Reason")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select termination_reason From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Pay Frequency")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Remarks")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select remarks From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Employer Giro Account Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Giro_acc_name From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 1")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignaddress1 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Line 2")
                                        {
                                            str = "(Select foreignaddress2 From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Foreign Address Postal Code")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select foreignpostalcode From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit application Date")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Hourly Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select hourly_rate From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate mode")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select Replace(daily_rate_mode,'A','Auto')  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Daily Rate")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select daily_rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Block Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select block_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Street Name")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select street_name  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Level Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select level_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Unit Number")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select unit_no  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Working days per week")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select wdays_per_week  From Employee  Where emp_code = e1.emp_code)";

                                            pivotQuery = pivotQuery + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 1 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v1rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 2 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v2rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 3 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v3rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Varibale 4 Value")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            str = "(Select v4rate  From Employee  Where emp_code = e1.emp_code)";

                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Claim Supervisor")
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Issue Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Work Permit Number")
                                        {
                                            str = "(Select wp_number  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Blood Group")
                                        {
                                            str = "(Select bloodgroup  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                        else if (dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() == "Worker Arrival Date")
                                        {
                                            str = "(Select " + dtTable.Rows[i]["RELATION"].ToString().Trim() + "  From Employee  Where emp_code = e1.emp_code)";
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + str + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                        }
                                        else
                                        {
                                            pivotQuery = pivotQuery + " [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";
                                            sqlSelect = sqlSelect + " " + dtTable.Rows[i]["RELATION"].ToString().Trim() + " AS [" + dtTable.Rows[i]["ALIAS_NAME"].ToString().Trim() + "],";

                                        }
                                    }

                                }
                            }
                        }

                        if (grid1 > 0)
                        {
                            if (grid2 > 0)
                            {

                                bool sqlSelect1 = true;
                                bool sqlSelect2 = false;
                                bool sqlSelect3 = false;
                                bool sqlSelect4 = false;
                                sqlSelect = sqlSelect.Remove(sqlSelect.Length - 1, 1);
                                string tempQuery = ", (SELECT VE.ALIAS_NAME from ViewEmployeeAdditionsDeductionClaims VE where month(VE.trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(VE.trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L') as ALIAS_NAME , (SELECT SUM(VE.TRX_AMOUNT) from ViewEmployeeAdditionsDeductionClaims VE where  month(VE.trx_period)=" + Convert.ToInt32(sMonth) + "  AND Year(VE.trx_period)=" + Convert.ToInt32(sYear) + " And VE.Status='L') AS Amount INTO TempTable";
                                if (sMonth != 0 && sYear != 0)
                                {

                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3" || dtTable.Rows[j]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + tempQuery;

                                            break;
                                        }
                                    }

                                }

                                sqlSelect = sqlSelect + " from Employee e1";
                                if (sMonth != 0 && sYear != 0)
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "1") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {

                                            sqlSelect1 = true;
                                            break;
                                        }
                                    }
                                }

                                if (sMonth != 0 && sYear != 0)
                                {
                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "2") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect = sqlSelect + " LEFT OUTER JOIN PayRollView1 pv on e1.emp_code=pv.emp_code";
                                            sqlSelect2 = true;
                                            break;
                                        }
                                    }
                                }

                                if (sMonth != 0 && sYear != 0)
                                {

                                    for (int j = 0; j < dtTable.Rows.Count; j++)
                                    {
                                        if (dtTable.Rows[j]["TableID"].ToString() == "3") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            //sqlSelect = sqlSelect + " LEFT OUTER JOIN ViewEmployeeAdditionsDeductionClaims VE on VE.emp_code=e1.emp_code";
                                            sqlSelect3 = true;

                                            break;
                                        }
                                    }

                                }

                                if (sMonth != 0 && sYear != 0)
                                {

                                    for (int i = 0; i < dtTable.Rows.Count; i++)
                                    {
                                        if (dtTable.Rows[i]["TableID"].ToString() == "4") // cross checking with dropdownlistitem to gridboundcolumn text
                                        {
                                            sqlSelect4 = true;
                                            break;
                                        }
                                    }
                                }

                                if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    sqlSelect = sqlSelect + " ORDER BY EMP_NAME;";
                                    //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email] order by empname;";
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == false && sqlSelect4 == false)
                                {

                                    sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                    if (sMonth != 0 && eMonth != 0)
                                    {
                                        sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                    }
                                    else
                                    {
                                        sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + " ";
                                    }

                                    sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.basic_pay,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                    //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount] order by empname;";
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == false)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {
                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + " ";
                                        }

                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.CPFGrossAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.basic_pay,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";

                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                        }

                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.start_period,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.basic_pay,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                    }
                                }
                                else if (sqlSelect1 == false && sqlSelect2 == true && sqlSelect3 == true && sqlSelect4 == true)
                                {
                                    if (sqlTrnsType.Length != 1)
                                    {

                                        sqlSelect = sqlSelect + " where e1.emp_code in (" + strEmployee + ")";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + " and Year(pv.start_period)=" + Convert.ToInt32(sYear) + " and month(pv.start_period) between " + Convert.ToInt32(sMonth) + "  AND " + Convert.ToInt32(eMonth) + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + " and month(pv.start_period)=" + Convert.ToInt32(sMonth) + "  AND Year(pv.start_period)=" + Convert.ToInt32(sYear) + "";
                                        }

                                        sqlSelect = sqlSelect + " And pv.STATUS in ('G')";
                                        if (sMonth != 0 && eMonth != 0)
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        else
                                        {
                                            sqlSelect = sqlSelect + "";
                                        }
                                        sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode,pv.emp_name,pv.emp_lname,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.start_period,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.CPFGrossAmount,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.basic_pay,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,pv.end_period,pv.unpaid_leaves_amount ORDER BY e1.EMP_NAME;";
                                        //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,pv.NH_wh,pv.NHRate,pv.NH_e,pv.DH_e,pv.DHRate,pv.OT1_e,pv.OT2_e,pv.Wdays,pv.NetPay,pv.total_additions,pv.total_deductions,pv.cpfAdd_Ordinary,pv.cpfAdd_Additional,pv.cpfNet,pv.empCPF,pv.employerCPF,pv.cpfAmount,pv.unpaid_leaves,pv.total_gross,pv.SDL,pv.FWL,pv.OT1Rate,pv.OT2Rate,pv.OT1_wh,pv.OT2_wh, pv.fund_type,pv.fund_amount,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],[NH Worked],[NH Rate],[NH Earning],[DH Earning],[DH Rate],[OT1 Amount],[OT2 Amount],[Working Days],[Net pay],[Total Additions],[Total Deductions],[CPF Addition Ordinary],[CPF Addition Wages],[CPF Net],[Employee Cont CPF],[Employer Cont CPF],[CPF Amount],[Unpaid Leaves],[Total Gross],[SDL],[FWL],[OT1 Rate],[OT2 Rate],[OT1 Hours],[OT2 Hours],[fund_type],[fund_amount],ALIAS_NAME,Amount order by empname;";

                                    }
                                }
                                else if (sqlSelect1 == true && sqlSelect2 == false && sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    sqlSelect = sqlSelect + "  where e1.emp_code in (" + strEmployee + ")";
                                    if (sMonth != 0 && eMonth != 0)
                                    {
                                        sqlSelect = sqlSelect + "";
                                    }
                                    else
                                    {
                                        sqlSelect = sqlSelect + "";
                                    }
                                    sqlSelect = sqlSelect + " group by e1.emp_code,e1.emp_name,e1.emp_lname,e1.emp_alias,e1.emp_type,e1.time_card_no,e1.trade_id,e1.eme_cont_per,e1.eme_cont_per_rel,e1.eme_cont_per_ph1,e1.religion_id,e1.eme_cont_per_ph2,e1.eme_cont_per_add,e1.eme_cont_per_rem,e1.NATIONALITY_ID,e1.ic_pp_number,e1.pr_date,e1.country_id, e1.postal_code,e1.phone,e1.hand_phone,e1.email,e1.sex,e1.race_id,e1.marital_status,e1.place_of_birth,e1.date_of_birth,e1.income_taxid,e1.giro_bank,e1.giro_branch,e1.giro_acct_number,e1.desig_id,e1.dept_id,e1.joining_date,e1.probation_period,e1.confirmation_date,e1.termination_date,e1.cpf_entitlement,e1.emp_group_id,e1.emp_supervisor,e1.ot_entitlement,e1.fw_code,e1.fw_levy,e1.email_payslip,e1.termination_reason,e1.pay_frequency,e1.remarks,e1.Giro_acc_name,e1.foreignaddress1,e1.foreignaddress2,e1.foreignpostalcode,e1.wp_application_date,e1.hourly_rate_mode,e1.hourly_rate,e1.daily_rate_mode,e1.daily_rate,e1.block_no,e1.street_name,e1.level_no,e1.unit_no,e1.wdays_per_week,e1.v1rate,e1.v2rate,e1.v3rate,e1.v4rate,e1.emp_clsupervisor,e1.wp_issue_date,e1.wp_number,e1.ot1rate,e1.ot2rate,e1.bloodgroup,e1.agent_id,e1.wp_arrival_date,e1.emp_supervisor,e1.payment_mode ORDER BY EMP_NAME;";
                                    //sqlSelect = sqlSelect + " group by el.emp_code,el.emp_name,el.emp_lname,el.emp_type,el.time_card_no,el.trade_id,el.eme_cont_per,el.eme_cont_per_rel,el.eme_cont_per_ph1,el.religion_id,el.eme_cont_per_ph2,el.eme_cont_per_add,el.eme_cont_per_rem,el.NATIONALITY_ID,el.ic_pp_number,el.pr_date,el.country_id, el.postal_code,el.phone,el.hand_phone,el.email,el.sex,el.race_id,el.marital_status,el.place_of_birth,el.date_of_birth,el.income_taxid,el.giro_bank,el.giro_branch,el.giro_acct_number,el.desig_id,el.dept_id,el.joining_date,el.probation_period,el.confirmation_date,el.termination_date,el.cpf_entitlement,el.emp_group_id,el.emp_supervisor,el.ot_entitlement,el.fw_code,el.fw_levy,el.email_payslip,el.termination_reason,el.pay_frequency,el.remarks,el.Giro_acc_name,el.foreignaddress1,el.foreignaddress2,el.foreignpostalcode,el.wp_application_date,el.hourly_rate_mode,el.hourly_rate,el.daily_rate_mode,el.daily_rate,el.block_no,el.street_name,el.level_no,el.unit_no,el.wdays_per_week,el.v1rate,el.v2rate,el.v3rate,el.v4rate,el.emp_clsupervisor,el.wp_issue_date,el.wp_number,el.ot1rate,el.ot2rate,el.bloodgroup,el.agent_id,el.wp_arrival_date,el.emp_supervisor,VE.ALIAS_NAME,VE.TRX_AMOUNT) as all_employees group by empcode,empname,emplname,emptype,TimeCardNo,BusinessUnit,Region,[Basic Pay],[Basic Pay Currency],[BasicPayConversion],[Exchange Rate],[Trade],[Emergency Contact Name],[Emergency Relationship],[Emergency Phone 1],[Emergency Phone 2],[Emergency Address],[Emergency Remarks],[Nationality],[IC / FIN Number],[PR Date],[Country],[Postal Code],[Phone Number],[Mobile Number],[Email Address],[Sex],[Race],[Marital Status],[Place of birth],[Date of birth],[Income Tax ID],[Giro Bank Name],[Giro Bank Code],[Giro Branch Number],[Giro Bank Account Number],[Religion],[Designation],[Department],[Joining Date],[Probation Period],[Confirmation Date],[Termination Date],[CPF Entitiled],[Employee Group],[Leave Supervisor],[Overtime Entitlement],[Foreignworker Code],[Foreignworker Levy],[Eamil Payslip],[Termination Reason],[Pay Frequency],[Remarks],[Employer Giro Account Name],[Foreign Address Line 1],[Foreign Address Line 2],[Foreign Address Line 3],[Work Permit application Date],[Hourly Rate mode],[Hourly Rate],[Daily Rate mode],[Daily Rate],[Block Number],[Street Name],[Level Number],[Unit Number],[Working days per week],[Varibale 1 Value],[Varibale 2 Value],[Varibale 3 Value],[Varibale 4 Value],[Claim Supervisor],[Work Permit Issue Date],[Work Permit Number],[Overtime 1],[Overtime 2],[Blood Group],[Agent name],[Worker Arrival Date],[Supervisor Email],ALIAS_NAME,Amount order by empname;";
                                }

                                if (sqlSelect3 == true || sqlSelect4 == true)
                                {
                                    SqlParameter[] parms = new SqlParameter[11];
                                    parms[0] = new SqlParameter("@empcode", strEmployee);
                                    parms[1] = new SqlParameter("@trxtype", sqlTrnsType);
                                    parms[2] = new SqlParameter("@startdate", Convert.ToInt32(DateTime.Now.Month));
                                    parms[3] = new SqlParameter("@enddate", Convert.ToInt32(DateTime.Now.Year));
                                    parms[4] = new SqlParameter("@claimtype", 1);
                                    parms[5] = new SqlParameter("@addtype", "ALL");
                                    parms[6] = new SqlParameter("@stattype", 'L');
                                    parms[7] = new SqlParameter("@claimstatus", 1);
                                    parms[8] = new SqlParameter("@selectquery", sqlSelect);
                                    parms[9] = new SqlParameter("@pivotquery", pivotQuery);
                                    parms[10] = new SqlParameter("@companyid", Utility.ToInteger(Session["Compid"].ToString()));
                                    rptDs = DataAccess.FetchRS(CommandType.StoredProcedure, sqlStr, parms);
                                }
                                else
                                {
                                    rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);

                                }
                                // rptDs = DataAccess.FetchRS(CommandType.Text, sqlSelect, null);
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
                                    //if (drpCurrency.SelectedValue == "2")
                                    //{
                                    //    foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //    {
                                    //        //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //        string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //        SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //        while (drcurrb.Read())
                                    //        {
                                    //            if (drcurrb.GetValue(0) != null)
                                    //            {
                                    //                basecurr = drcurrb.GetValue(0).ToString();
                                    //            }
                                    //        }
                                    //        if (basecurr == "1")
                                    //        {
                                    //            basecurr = "SGD";
                                    //        }
                                    //        else
                                    //        {
                                    //            basecurr = "UGD";
                                    //        }

                                    //        //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //        //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //        //{

                                    //        if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //        {
                                    //            if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //            {
                                    //                if (dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                        {
                                    //                            dr["BasicPayConversion"] = dr["Basic Pay"].ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }

                                    //                if (dr.Table.Columns["BasicPayConversion"] != null && dr.Table.Columns["Basic Pay"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr["BasicPayConversion"].ToString() != "" && dr["Basic Pay"].ToString() != "")
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (dr.Table.Columns["Exchange Rate"] != null && val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                        }
                                    //                    }
                                    //                    dr.AcceptChanges();
                                    //                }
                                    //            }
                                    //            else if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //            {
                                    //                //dr.BeginEdit();
                                    //                //double val = Convert.ToDouble(dr["Basic Pay"].ToString()) / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                //dr["BasicPayConversion"] = val.ToString();
                                    //                //dr.AcceptChanges();
                                    //                if (dr.Table.Columns["BasicPayConversion"] != null)
                                    //                {
                                    //                    dr.BeginEdit();
                                    //                    if (dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //                    {
                                    //                        double val = Convert.ToDouble(dr["BasicPayConversion"].ToString()) / Convert.ToDouble(dr["Basic Pay"].ToString());
                                    //                        if (val.ToString() != "NaN")
                                    //                        {
                                    //                            dr["Exchange Rate"] = val.ToString();
                                    //                            decimal value = Convert.ToDecimal(dr["Basic Pay"]) / Convert.ToDecimal(dr["Exchange Rate"]);
                                    //                            decimal newValue = decimal.Round(value, 2);
                                    //                            dr["BasicPayConversion"] = Convert.ToString(newValue);
                                    //                        }
                                    //                        dr.AcceptChanges();
                                    //                    }
                                    //                }
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}

                                    //foreach (DataRow dr in rptDs.Tables[0].Rows)
                                    //{
                                    //    //string strbasic = "Select  CurrencyID  From company Where Company_Id=" + comp_id;
                                    //    string strbasic = "Select CurrencyID  from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + " and ID IN (Select MAX(ID) from EmployeePayHistory Where emp_id =" + dr["Emp_Code"].ToString() + ")";
                                    //    SqlDataReader drcurrb = DataAccess.ExecuteReader(CommandType.Text, strbasic, null);

                                    //    while (drcurrb.Read())
                                    //    {
                                    //        if (drcurrb.GetValue(0) != null)
                                    //        {
                                    //            basecurr = drcurrb.GetValue(0).ToString();
                                    //        }
                                    //    }
                                    //    if (basecurr == "1")
                                    //    {
                                    //        basecurr = "SGD";
                                    //    }
                                    //    else
                                    //    {
                                    //        basecurr = "UGD";
                                    //    }
                                    //    //if (rptDs.Tables[0].Columns.Contains("Basic Pay Currency") && rptDs.Tables[0].Columns.Contains("Basic Pay") &&
                                    //    //    rptDs.Tables[0].Columns.Contains("Exchange Rate") && rptDs.Tables[0].Columns.Contains("BasicPayConversion"))
                                    //    //{
                                    //    if (dr.Table.Columns["Basic Pay Currency"] != null)
                                    //    {
                                    //        if (dr["Basic Pay Currency"].ToString() != "SGD")
                                    //        {
                                    //            dr.BeginEdit();
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["Exchange Rate"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                if (dr["Basic Pay"].ToString() != "" && dr["Exchange Rate"].ToString() != "")
                                    //                {
                                    //                    double val = (Convert.ToDouble(dr["Basic Pay"].ToString()) * Convert.ToDouble(dr["Exchange Rate"].ToString()));// / Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                    dr["BasicPayConversion"] = Math.Round(val, 2);
                                    //                }
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        if (dr["Basic Pay Currency"].ToString() == "SGD")
                                    //        {
                                    //            if (dr.Table.Columns["Basic Pay"] != null && dr.Table.Columns["BasicPayConversion"] != null)
                                    //            {
                                    //                dr.BeginEdit();
                                    //                double val = Convert.ToDouble(dr["Basic Pay"].ToString());//*Convert.ToDouble(dr["Exchange Rate"].ToString());
                                    //                dr["BasicPayConversion"] = val.ToString();
                                    //                dr.AcceptChanges();
                                    //            }
                                    //        }
                                    //        //}
                                    //    }
                                    //}
                                }
                            }
                            Session["rptDs"] = rptDs;
                            Response.Redirect("../Reports/CommonReportView.aspx?PageType=26");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "field", "alert('Please Select Atleast One Field Name');", true);

                        }

                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "monthyear", "alert('Please Select month and year');", true);
                }

        }   
    }       
}
