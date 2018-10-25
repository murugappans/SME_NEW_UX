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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing;
using iTextSharp.text.pdf.fonts;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Text;
using System.Diagnostics;


namespace SMEPayroll.Reports
{
    public partial class Ket_Form : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        int compid;
        SqlConnection conn;
        SqlConnection conn2;
        SqlCommand cmd;
        SqlCommand cmd2, cmd3, cmd4;
        SqlDataReader dr, dr2, dr3, dr4;
        DateTime jd;
        Int32 pp;
        Int32 cid;
        Int32 did;
        string empid;
        Int32 payrolltype;
        double ot1rate;
        double ot2rate;
        double hrate;
        double drate;
        double mrate;
        double wrate;
        double frate;
        string pf;
        string opf;
        string sal_del_text;

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            if (!IsPostBack)
            {
                empid = Request.QueryString["empcode"].ToString();
                Session["emp_code"] = empid;

                //DataSet ds_employee = new DataSet();
                //ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name");

                //RadComboBoxPrjEmp.DataSource = ds_employee.Tables[0];
                //RadComboBoxPrjEmp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                //RadComboBoxPrjEmp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                //RadComboBoxPrjEmp.DataBind();
                //RadComboBoxPrjEmp.Text = "Select Employee";
                //-----------------------------------------
                DataSet ds_jobcat = new DataSet();
                ds_jobcat = getDataSet("SELECT [cat_id], [cat_name] FROM [jobCategory] order by cat_name");
                ddJobCategory.DataSource = ds_jobcat.Tables[0];

                ddJobCategory.DataTextField = ds_jobcat.Tables[0].Columns["cat_name"].ColumnName.ToString();
                ddJobCategory.DataValueField = ds_jobcat.Tables[0].Columns["cat_id"].ColumnName.ToString();
                ddJobCategory.DataBind();

                ddJobCategory.Text = "Select Job Category";
                //--------------------------------------------
                DataSet ds_types = new DataSet();
                ds_types = getDataSet("SELECT id,[desc] 'adesc' FROM [additions_types] where optionselection='General' order by adesc");
                ddAllowancetypes.DataSource = ds_types.Tables[0];

                ddAllowancetypes.DataTextField = ds_types.Tables[0].Columns["adesc"].ColumnName.ToString();
                ddAllowancetypes.DataValueField = ds_types.Tables[0].Columns["id"].ColumnName.ToString();
                ddAllowancetypes.DataBind();
                ddAllowancetypes.Text = "Select Types";
                // --------------------------------------------
                //  ds_types = new DataSet();
                // ds_types = getDataSet("SELECT id,[desc] 'adesc' FROM [additions_types] where optionselection='General' order by adesc");
                // // ddAllowancetypes.DataSource = ds_types.Tables[0];
                //ddcomponents.DataSource = ds_types.Tables[0];
                // ddcomponents.DataTextField = ds_types.Tables[0].Columns["adesc"].ColumnName.ToString();
                // ddcomponents.DataValueField = ds_types.Tables[0].Columns["id"].ColumnName.ToString();
                // ddcomponents.DataBind();
                // ddcomponents.Text = "Select Types";
                // DropDownList ddl5 = (DropDownList)ddAllowancetypes;
                // FillDropDownList(ddl5);    
                
            }
            if (!Page.IsPostBack)
            {
                Calendar2.SelectedDate = DateTime.Today; //murugan


                for (int i = 1; i <= 31; i++)
                {
                    ddSalarydate.Items.Add(i.ToString());
                    ddOTdate.Items.Add(i.ToString());

                }

                SetInitialRow();
                // FetchDetails();
                DDLduration_todate.SelectedIndex = 0;
                FetchEmployee();
                SetPreviousData();
                
            }
            // RadComboBoxPrjEmp.EmptyMessage = "Select Employee ";
            //  FetchEmployee();
        }

        //public object Dataitem
        //{
        //    get
        //    {
        //        return this._dataItem;
        //    }
        //    set
        //    {
        //        this._dataItem = value;
        //    }
        //}

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        //protected void RadComboBoxEmpPrj_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    RadComboBoxPrjEmp.ClearSelection();
        ////    string sqlSelectCommand = "";
        ////    RadComboBox rd = new RadComboBox();

        ////   // RadGrid1.MasterTableView.ShowGroupFooter = true;
        ////    rd = RadComboBoxPrjEmp;
        ////    // sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////    //if (Utility.ToString(Session["GroupName"]) == "Super Admin")
        ////    //{
        ////    //    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////    //}
        ////    //else
        ////    //{
        ////    //    sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 and emp_code='" + Session["EmpCode"].ToString() + "' And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////    //}

        ////    //Senthil for Group Management
        ////    if (Utility.ToString(Session["GroupName"]) == "Super Admin")
        ////    {
        ////        sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////    }
        ////    else
        ////    {
        ////        //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 and emp_code='" + Session["EmpCode"].ToString() + "' And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////        //sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////        if (Utility.GetGroupStatus(Convert.ToInt32(compid)) == 1)
        ////        {
        ////            sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom <=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") And StatusID=1 ORDER BY [Emp_Name]";
        ////        }
        ////        else
        ////        {
        ////            sqlSelectCommand = "SELECT Emp_Code [Emp_Code], isnull(emp_name,'')+' '+isnull(emp_lname,'') [Emp_Name], Time_Card_No [Time_Card_No], ic_pp_number  [ic_pp_number] from [Employee] WHERE termination_date is null and Company_ID=" + compid + " And  Len([Time_Card_No]) >= 0 And StatusID=1 And (upper(isnull(emp_name,''))+' '+upper(isnull(emp_lname,'')) LIKE + '%' + upper(@text) + '%' Or upper([ic_pp_number]) LIKE + '%' +  upper(@text) + '%' Or upper([Time_Card_No]) LIKE + '%' + @text + '%') ORDER BY [Emp_Name]";
        ////        }

        ////    }

        ////    SqlDataAdapter adapter = new SqlDataAdapter(sqlSelectCommand, Constants.CONNECTION_STRING);
        ////    adapter.SelectCommand.Parameters.AddWithValue("@text", e.Text);
        ////    DataTable dataTable = new DataTable();
        ////    adapter.Fill(dataTable);

        ////    foreach (DataRow dataRow in dataTable.Rows)
        ////    {
        ////        RadComboBoxItem item = new RadComboBoxItem();

        ////        item.Text = Convert.ToString(dataRow["Emp_Name"]);
        ////        item.Value = Convert.ToString(dataRow["Emp_Code"].ToString());



        ////        string Time_Card_No = Convert.ToString(dataRow["Time_Card_No"]);
        ////        string ic_pp_number = Convert.ToString(dataRow["ic_pp_number"]);

        ////        item.Attributes.Add("Time_Card_No", Time_Card_No.ToString());
        ////        item.Attributes.Add("ic_pp_number", ic_pp_number.ToString());

        ////        rd.Items.Add(item);

        ////        item.DataBind();
        ////    }
        //}

        protected void ddAllowancetypes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
        }

        protected void DDLduration_todate_SelectedIndexChanged(object o, EventArgs e)
        {
            if (DDLduration_todate.SelectedIndex == 0)
            {
                txtDurationend.Text = DDLduration_todate.SelectedItem.ToString();
                Calendar1.Visible = false;
            }
            else
            {
                Calendar1.Visible = true;
            }
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDurationend.Text = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            Calendar1.Visible = false ;
        }
        protected void rHourly_CheckedChanged(object sender, EventArgs e)
        {
            txt_weekly_fortnightly.Visible = false;
            txtSalary.Text = ViewState["hrate"].ToString ();
            txtsal_period_details.Text = "( per Hourly)";
        }
        protected void rDaily_CheckedChanged(object sender, EventArgs e)
        {
            txt_weekly_fortnightly.Visible = false;
            txtSalary.Text = ViewState["drate"].ToString();
            txtsal_period_details.Text = "( per Daily)";
        }
        protected void rWeekly_CheckedChanged(object sender, EventArgs e)
        {
            txt_weekly_fortnightly.Visible = true;
            txtSalary.Text = "";
            txtsal_period_details.Text = "( per Weekly)";
            if (ViewState["wrate"] == null)
            {
                txt_weekly_fortnightly .Text="";
            }
            else
            {
            txt_weekly_fortnightly .Text=ViewState["wrate"].ToString ();
            }
           

        }
        protected void rForntnightly_CheckedChanged(object sender, EventArgs e)
        {
            txt_weekly_fortnightly.Visible = true;
            txtSalary.Text = "";
            txtsal_period_details.Text = "( per Fortnightly)";
            if (ViewState["frate"] == null)
            {
                txt_weekly_fortnightly.Text = "";
            }
            else
            {
                txt_weekly_fortnightly.Text = ViewState["frate"].ToString();
            }


        }
        protected void rMonthly_CheckedChanged(object sender, EventArgs e)
        {
            txt_weekly_fortnightly.Visible = false;
            txtSalary.Text = ViewState["mrate"].ToString();
            txtsal_period_details.Text = "( per Monthly)";
        }
        protected void ddJobCategory_SelectedIndexChanged(object o, EventArgs e)
        {

            DataSet ds_jobtitle = new DataSet();
            if (ddJobCategory.SelectedValue =="Select")
            {
                ddJobTitle.Items.Clear();
                ddJobTitle.Items.Add("Select");
                ddJobTitle.SelectedIndex = -1;
                
            }
            else{
                ds_jobtitle = getDataSet("SELECT [id], [cat_title] FROM [JobTitle] where cat_id=" + ddJobCategory.SelectedValue + " order by cat_title");
                ddJobTitle.DataSource = ds_jobtitle.Tables[0];
                ddJobTitle.DataTextField = ds_jobtitle.Tables[0].Columns["cat_title"].ColumnName.ToString();
                ddJobTitle.DataValueField = ds_jobtitle.Tables[0].Columns["id"].ColumnName.ToString();
                
                ddJobTitle.DataBind();
                
               }
            
            //   ddJobTitle.Text = "select Title";

        }
        protected void ddJobTitle_SelectedIndexChanged(object o, EventArgs e)
        {

        }
        //protected void RadComboBoxPrjEmp_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    SetInitialRow();
        //    Session["emp_code"] = e.Value;

        //    //string ecode = Request.QueryString["empcode"].ToString();
        //    string ecode = e.Value;
        //    conn = new SqlConnection(Session["ConString"].ToString());
        //    conn.Open();
        //    cmd = new SqlCommand("select employee.desig_id,employee.emp_name,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.Company_name,company.Company_id from employee,company where emp_code=" + ecode + " and employee.Company_id=company.Company_id", conn);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        did = Convert.ToInt32(dr["desig_id"]);
        //        cid = Convert.ToInt32(dr["Company_id"]);
        //        //Details of employment section
        //        txtEmpName.Text = dr["emp_name"].ToString();
        //        txtCompanyName.Text = dr["Company_name"].ToString();
        //        txtPlace.Text = dr["Address"].ToString();
        //        txtEmpId.Text = dr["ic_pp_number"].ToString();
        //        txtStartDate.Text = dr["joining_date"].ToString ().Substring(0,10);
        //        txtDurationstart.Text  = dr["joining_date"].ToString().Substring(0, 10);

        //        //working Hours and Rest days section


        //        //others section
        //        pp = Convert.ToInt32(dr["probation_period"]);
        //        txtProlength.Text = dr["probation_period"].ToString();
        //        txtProsdate.Text = dr["joining_date"].ToString().Substring(0,10);
        //        jd = Convert.ToDateTime(dr["joining_date"]);
        //        txtProedate.Text = jd.AddMonths(pp).ToShortDateString();
        //        //txtProedate.Text = Convert.ToDateTime(dr["joining_date"]).AddMonths(pp).ToShortDateString();
        //    }
        //    dr.Close();
        //    cmd = new SqlCommand("select Leave_Type,Leaves_Allowed From EmployeeLeavesAllowed Where (leave_type = 8 or leave_type = 9 or leave_type=13) and Emp_ID=" + ecode + " and leave_year=2016", conn);
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        if (Convert.ToInt16(dr["Leave_Type"]) == 8)
        //        {
        //            txtAnnualLeave.Text = dr["Leaves_Allowed"].ToString();
        //        }
        //        else if (Convert.ToInt16(dr["Leave_Type"]) == 9)
        //        {
        //            txtSickLeave.Text = dr["Leaves_Allowed"].ToString();
        //        }
        //        else if (Convert.ToInt16(dr["Leave_Type"]) == 13)
        //        {
        //            txtHospLeave.Text = dr["Leaves_Allowed"].ToString();
        //        }

        //    }
        //    dr.Close();
        //    cmd = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,CPF_Entitlement,Hourly_Rate from EmployeePayHistory where Emp_ID=" + ecode, conn);
        //    dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        txtSalary.Text = dr["payrate"].ToString();
        //        txtOTrate.Text = dr["Hourly_Rate"].ToString();
        //        txtWorkDays.Text = dr["wdays_per_week"].ToString();
        //        Double rday = 7 - Convert.ToDouble(dr["wdays_per_week"]);
        //        if (rday == 2)
        //        {
        //            dayDatails.Text = "Saturday & Sunday";
        //        }
        //        else if (rday == 1.5)
        //        {
        //            dayDatails.Text = "1/2day Saturday & Sunday";
        //        }
        //        else
        //        {
        //            dayDatails.Text = "Sunday";
        //        }
        //        txtRestDay.Text = rday.ToString();


        //        if (dr["CPF_Entitlement"].ToString() == "Y")
        //        {
        //            ckCpf.Checked = true;
        //        }
        //        txtOtot.Text = dr["OT1Rate"].ToString();

        //        rMonthly.Checked = true;
        //        if (dr["Pay_Frequency"].ToString() == "M")
        //        {
        //            ckCpf.Checked = true;
        //        }

        //    }
        //    //---
        //    dr.Close();
        //    cmd = new SqlCommand("select Designation from designation where id=" + did, conn);
        //    dr = cmd.ExecuteReader();

        //    while (dr.Read())
        //    {
        //        txtJobTittle.Text = dr["Designation"].ToString();


        //    }
        //    dr.Close();
        //    //------------check in Ket_Manual, record exists or not
        //    cmd = new SqlCommand("select * from Ket_Manual where emp_code=" + ecode, conn);
        //    dr = cmd.ExecuteReader();
        //    int titleid = 0;
        //    int catid = 0;
        //    string title = "";
        //    if (dr.Read())
        //    {
        //        printbutton.Enabled = true;
        //        titleid=Convert.ToInt32(dr["job_title_id"]);
        //        //title = dr["cat_title"].ToString();

        //        if (dr["ot_pay_period"].ToString() == "H")
        //        {
        //            rHourly.Checked = true;
        //        }
        //        else if (dr["ot_pay_period"].ToString() == "D")
        //        {
        //            rDaily.Checked = true;
        //        }
        //        else if (dr["ot_pay_period"].ToString() == "W")
        //        {
        //            rWeekly.Checked = true;
        //        }
        //        else if (dr["ot_pay_period"].ToString() == "F")
        //        {
        //            rForntnightly.Checked = true;
        //        }
        //        else if (dr["ot_pay_period"].ToString() == "M")
        //        {
        //            rMonthly.Checked = true;
        //        }
        //        ddSalarydate.SelectedValue = dr["date_of_payment"].ToString();
        //        ddOTdate.SelectedValue = dr["date_of_ot"].ToString();
        //        txtOtherMedical_benefit.Text = dr["other_medical_benefit"].ToString();
        //        txtdet_working_hrs.Text = dr["working_hrs_details"].ToString();

        //    }
        //    else
        //    {
        //        printbutton.Enabled = false;
        //        rMonthly.Checked = true;
        //        ddSalarydate.SelectedValue = "1";
        //        ddOTdate.SelectedValue = "1";
        //       // txtOtherMedical_benefits.Value = "NIL";
        //        txtdet_working_hrs.Text = "";
        //    }
        //    //------- find job category
        //    dr.Close();
        //    cmd = new SqlCommand("select * from  JobTitle where id="+titleid , conn);
        //    dr = cmd.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        catid = Convert.ToInt32( dr["cat_id"]);
        //        title = dr["cat_title"].ToString();
        //    }
        //   // ddJobCategory.SelectedValue = catid.ToString();

        //   // ddJobTitle.Text = title;


        //    //-------------------------------check for record in allowance
        //    dr.Close();
        //    cmd = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + ecode, conn);
        //    //dr = cmd.ExecuteReader();
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + ecode, conn);
        //    da.Fill(ds);
        //    //=================

        //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //    DataRow drCurrentRow = null;
        //    // if (dtCurrentTable.Rows.Count > 0)
        //    for (int i = 0; i < ds.Tables[0].Rows.Count - 1; i++)
        //    {
        //        drCurrentRow = dtCurrentTable.NewRow();
        //        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

        //        //add new row to DataTable   
        //        dtCurrentTable.Rows.Add(drCurrentRow);

        //    }

        //    //Rebind the Grid with the current data to reflect changes   
        //    Gridview1.DataSource = dtCurrentTable;
        //    Gridview1.DataBind();


        //    //=================

        //    int rowIndex = 0;
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        // DataTable dt = (DataTable)ViewState["CurrentTable"];

        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            DropDownList ddl1 = (DropDownList)Gridview1.Rows[rowIndex].Cells[1].FindControl("DropDownList1");
        //            TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");

        //            //Fill the DropDownList with Data   
        //            FillDropDownList(ddl1);

        //            //  if (i < ds.Tables[0].Rows.Count - 1)
        //            //  {

        //            //Assign the value from DataTable to the TextBox   
        //            // box1.Text = dt.Rows[i]["Column2"].ToString();
        //            box1.Text = ds.Tables[0].Rows[i]["amount"].ToString();
        //            //Set the Previous Selected Items on Each DropDownList  on Postbacks   
        //            ddl1.ClearSelection();
        //            ddl1.Items.FindByText(ds.Tables[0].Rows[i]["type_desc"].ToString()).Selected = true;


        //            //  }

        //            rowIndex++;
        //        }

        //    }
        //    //-------end of allowance fetch records

        //    //-------------------------------check for record in deduction
        //    dr.Close();
        //    // cmd = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + ecode, conn);
        //    DataSet ds2 = new DataSet();
        //    SqlDataAdapter da2 = new SqlDataAdapter("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + ecode, conn);
        //    da2.Fill(ds2);
        //    if (ds2.Tables[0].Rows.Count > 0)
        //    {
        //        //=================

        //        DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
        //        DataRow drCurrentRow2 = null;
        //        // if (dtCurrentTable.Rows.Count > 0)
        //        for (int i = 0; i < ds2.Tables[0].Rows.Count - 1; i++)
        //        {
        //            drCurrentRow2 = dtCurrentTable2.NewRow();
        //            drCurrentRow2["RowNumber"] = dtCurrentTable2.Rows.Count + 1;

        //            //add new row to DataTable   
        //            dtCurrentTable2.Rows.Add(drCurrentRow2);

        //        }

        //        //Rebind the Grid with the current data to reflect changes   
        //        Gridview2.DataSource = dtCurrentTable2;
        //        Gridview2.DataBind();


        //        //=================

        //        rowIndex = 0;
        //        if (ds2.Tables[0].Rows.Count > 0)
        //        {
        //            // DataTable dt = (DataTable)ViewState["CurrentTable"];

        //            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
        //            {
        //                DropDownList ddl1 = (DropDownList)Gridview2.Rows[rowIndex].Cells[1].FindControl("DropDownList2");
        //                TextBox box1 = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");

        //                //Fill the DropDownList with Data   
        //                FillDropDownList2(ddl1);

        //                //  if (i < ds.Tables[0].Rows.Count - 1)
        //                //  {

        //                //Assign the value from DataTable to the TextBox   
        //                // box1.Text = dt.Rows[i]["Column2"].ToString();
        //                box1.Text = ds.Tables[0].Rows[i]["amount"].ToString();
        //                //Set the Previous Selected Items on Each DropDownList  on Postbacks   
        //                ddl1.ClearSelection();
        //                ddl1.Items.FindByText(ds2.Tables[0].Rows[i]["type_desc"].ToString()).Selected = true;


        //                //  }

        //                rowIndex++;
        //            }

        //        }

        //        //-------end of deduction fetch records

        //    }
        //    //ddJobCategory.Text = "Managers";

        //    int index = ddJobCategory.FindItemIndexByValue(catid.ToString());
        //    ddJobCategory.SelectedIndex = index;



        //  //  ddJobTitle.SelectedValue = "'"+titleid+"'";
        //}
        private ArrayList GetDummyData()
        {
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd = new SqlCommand("select * from additions_types where (optionselection='General' OR optionselection='Claim') AND company_id=" + compid, conn);
            dr = cmd.ExecuteReader();
            ArrayList arr = new ArrayList();
            while (dr.Read())
            {
                arr.Add(new System.Web.UI.WebControls.ListItem(dr["desc"].ToString(), dr["id"].ToString()));
            }


            return arr;
        }
        private ArrayList GetDummyData2()
        {
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd = new SqlCommand("select * from deductions_types where  company_id=" + compid, conn);
            dr = cmd.ExecuteReader();
            ArrayList arr = new ArrayList();
            while (dr.Read())
            {
                arr.Add(new System.Web.UI.WebControls.ListItem(dr["desc"].ToString(), dr["id"].ToString()));
            }


            return arr;
        }
        private ArrayList GetDummyData3()
        {
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd = new SqlCommand("select * from additions_types where (optionselection='General' OR optionselection='Claim')  AND company_id=" + compid, conn);
            dr = cmd.ExecuteReader();
            ArrayList arr = new ArrayList();
            while (dr.Read())
            {
                arr.Add(new System.Web.UI.WebControls.ListItem(dr["desc"].ToString(), dr["id"].ToString()));
            }


            return arr;
        }

        private void FillDropDownList(DropDownList ddl)
        {
            ArrayList arr = GetDummyData();

            foreach (System.Web.UI.WebControls.ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }
        private void FillDropDownList2(DropDownList ddl)
        {
            ArrayList arr = GetDummyData2();

            foreach (System.Web.UI.WebControls.ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }
        private void FillDropDownList3(DropDownList ddl)
        {
            ArrayList arr = GetDummyData3();

            foreach (System.Web.UI.WebControls.ListItem item in arr)
            {
                ddl.Items.Add(item);
            }
        }

        private void SetInitialRow()
        {

            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));//for DropDownList selected item   
            dt.Columns.Add(new DataColumn("Column2", typeof(string)));//for TextBox value   
            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            //dr["Column1"] = string.Empty;
            // dr["Column2"] = string.Empty;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["CurrentTable"] = dt;

            //Bind the Gridview   
            Gridview1.DataSource = dt;
            Gridview1.DataBind();

            //After binding the gridview, we can then extract and fill the DropDownList with Data   
            DropDownList ddl1 = (DropDownList)Gridview1.Rows[0].Cells[1].FindControl("DropDownList1");
            FillDropDownList(ddl1);
            //--------------
            DataTable dt2 = new DataTable();
            DataRow dr2 = null;

            dt2.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt2.Columns.Add(new DataColumn("Column1", typeof(string)));//for DropDownList selected item   
            dt2.Columns.Add(new DataColumn("Column2", typeof(string)));//for TextBox value   
            dr2 = dt2.NewRow();
            dr2["RowNumber"] = 1;
            //dr["Column1"] = string.Empty;
            // dr["Column2"] = string.Empty;
            dt2.Rows.Add(dr2);

            //Store the DataTable in ViewState for future reference   
            ViewState["CurrentTable2"] = dt2;

            //Bind the Gridview   
            Gridview2.DataSource = dt2;
            Gridview2.DataBind();

            DropDownList ddl2 = (DropDownList)Gridview2.Rows[0].Cells[1].FindControl("DropDownList2");
            FillDropDownList2(ddl2);
            //---------------------------
            DataTable dt3 = new DataTable();
            DataRow dr3 = null;

            dt3.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt3.Columns.Add(new DataColumn("Column1", typeof(string)));//for DropDownList selected item   
            dt3.Columns.Add(new DataColumn("Column2", typeof(string)));//for TextBox value   
            dr3 = dt3.NewRow();
            dr3["RowNumber"] = 1;
            //   dr["Column1"] = string.Empty;
            //    dr["Column2"] = string.Empty;
            dt3.Rows.Add(dr3);

            //Store the DataTable in ViewState for future reference   
            ViewState["CurrentTable3"] = dt3;

            //Bind the Gridview   
            Gridview3.DataSource = dt3;
            Gridview3.DataBind();

            DropDownList ddl3 = (DropDownList)Gridview3.Rows[0].Cells[1].FindControl("DropDownList3");
            FillDropDownList3(ddl3);

        }

        private void AddNewRowToGrid()
        {

            int c = Gridview1.Rows.Count - 1;
            double number2;
            TextBox box100 = (TextBox)Gridview1.Rows[c].Cells[2].FindControl("TextBox1");
            DropDownList ddl100 = (DropDownList)Gridview1.Rows[c].Cells[1].FindControl("DropDownList1");
            Label errlable = (Label)Gridview1.FooterRow.Cells[1].FindControl("err_label");
            errlable.Text = "";
            if (!((box100.Text == "") || (ddl100.Text == "-1")))
            {

                if (double.TryParse(box100.Text, out number2))
                {

                    if (number2 <= 0)
                    {
                        box100.Text = "";
                        errlable.Text = "Negative value not allowed..";
                        goto exit10;
                    }
                    else
                    {
                        box100.Text = number2.ToString();
                    }

                }
                else
                {
                    box100.Text = "";
                    errlable.Text = "Special char not allowed..";
                    goto exit10;
                }

                if (ViewState["CurrentTable"] != null)
                {

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        //add new row to DataTable   
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        //Store the current data to ViewState for future reference   

                        ViewState["CurrentTable"] = dtCurrentTable;


                        for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                        {

                            //extract the TextBox values   

                            TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");


                            dtCurrentTable.Rows[i]["Column2"] = box1.Text;

                            //extract the DropDownList Selected Items   

                            DropDownList ddl1 = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");


                            // Update the DataRow with the DDL Selected Items   

                            dtCurrentTable.Rows[i]["Column1"] = ddl1.SelectedItem.Text;

                        }

                        //Rebind the Grid with the current data to reflect changes   
                        Gridview1.DataSource = dtCurrentTable;
                        Gridview1.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");

                }
                //Set Previous Data on Postbacks   
                SetPreviousData();
            }
            else
            {
                errlable.Text = "Please Enter/Select value..";
                goto exit10;
            }

        exit10:
            {

                box100.Focus();
            }

        }

        private void SetPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddl1 = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");
                        TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
                       
                        //Fill the DropDownList with Data   
                        FillDropDownList(ddl1);

                        if (i < dt.Rows.Count - 1)
                        {

                            //Assign the value from DataTable to the TextBox  
                            string s = dt.Rows[i]["Column2"].ToString();
                            //double  str1=double.Parse(s);
                            // box1.Text =string.Format("{0:##0.00}",str1);
                            box1.Text = s;
                            box1.CssClass = "aligntext";
                           // box1.ReadOnly = true;
                           
                            //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                            ddl1.ClearSelection();
                            string str = dt.Rows[i]["Column1"].ToString();
                            ddl1.Items.FindByText(str).Selected = true;
                        }

                        rowIndex++;
                    }
                }
            }
        }


        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void Gridview1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton1");
                if (lb != null)
                {
                    if (dt.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt.Rows.Count - 1)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                }
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        // dt.Rows.Remove(dt.Rows[rowID+1]);
                        dt.Rows.Remove(dt.Rows[rowID]);
                        //ResetRowID(dt);

                        int rowNumber = 1;
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                row[0] = rowNumber;
                                rowNumber++;
                            }
                        }

                        //

                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable"] = dt;
                //-------------
                ////for (int i = 0; i < dt.Rows.Count - 1; i++)
                ////{

                ////    //extract the TextBox values   

                ////    TextBox box1 = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");


                ////    dt.Rows[i]["Column2"] = box1.Text;

                ////    //extract the DropDownList Selected Items   

                ////    DropDownList ddl1 = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");


                ////    // Update the DataRow with the DDL Selected Items   

                ////    dt.Rows[i]["Column1"] = ddl1.SelectedItem.Text;

                ////}

                //Re bind the GridView for the updated data  
                Gridview1.DataSource = dt;
                Gridview1.DataBind();
            }

            //Set Previous Data on Postbacks  
            SetPreviousData();
        }

        private void ResetRowID(DataTable dtt)
        {
            int rowNumber = 1;
            if (dtt.Rows.Count > 0)
            {
                foreach (DataRow row in dtt.Rows)
                {
                    row[0] = rowNumber;
                    rowNumber++;
                }
            }
        }
        ///-----------grid2 events
        protected void ButtonAdd2_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid2();
        }
        private void AddNewRowToGrid2()
        {

            int c = Gridview2.Rows.Count - 1;
            double number2;
            TextBox box100 = (TextBox)Gridview2.Rows[c].Cells[2].FindControl("TextBox2");
            DropDownList ddl100 = (DropDownList)Gridview2.Rows[c].Cells[1].FindControl("DropDownList2");
            Label errlable = (Label)Gridview2.FooterRow.Cells[1].FindControl("err_label");
            errlable.Text = "";
            if (((box100.Text != "") && (ddl100.Text != "-1")))
            {
                if (double.TryParse(box100.Text, out number2))
                {
                    if (number2 <= 0)
                    {
                        box100.Text = "";
                        errlable.Text = "Negative value not allowed";
                        goto exit10;
                    }
                    else
                    {
                        box100.Text = number2.ToString();
                    }
                   

                }
                else
                {
                    box100.Text = "";
                    errlable.Text = "Special char not allowed..";
                    goto exit10;
                }

                if (ViewState["CurrentTable2"] != null)
                {

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
                    DataRow drCurrentRow = null;

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        //add new row to DataTable   
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        //Store the current data to ViewState for future reference   

                        ViewState["CurrentTable2"] = dtCurrentTable;


                        for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                        {

                            //extract the TextBox values   

                            TextBox box2 = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");


                            dtCurrentTable.Rows[i]["Column2"] = box2.Text;


                            //extract the DropDownList Selected Items   

                            DropDownList ddl2 = (DropDownList)Gridview2.Rows[i].Cells[1].FindControl("DropDownList2");


                            // Update the DataRow with the DDL Selected Items   

                            dtCurrentTable.Rows[i]["Column1"] = ddl2.SelectedItem.Text;


                        }

                        //Rebind the Grid with the current data to reflect changes   
                        Gridview2.DataSource = dtCurrentTable;
                        Gridview2.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");

                }
                //Set Previous Data on Postbacks   
                SetPreviousData2();
            }
            else
            {
                errlable.Text = "Please Enter/Select value..";
                goto exit10;
            }

        exit10:
            {

                box100.Focus();
            }
        }
        private void SetPreviousData2()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable2"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable2"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddl2 = (DropDownList)Gridview2.Rows[rowIndex].Cells[1].FindControl("DropDownList2");
                        TextBox box2 = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");

                        //Fill the DropDownList with Data   
                        FillDropDownList2(ddl2);

                        if (i < dt.Rows.Count - 1)
                        {


                            //Assign the value from DataTable to the TextBox   
                            box2.Text = string.Format("{0:##0.00}", double.Parse(dt.Rows[i]["Column2"].ToString()));
                            box2.CssClass = "aligntext";

                            //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                            ddl2.ClearSelection();
                            ddl2.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;


                        }

                        rowIndex++;
                    }
                }
            }
        }
        protected void Gridview2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt2 = (DataTable)ViewState["CurrentTable2"];
                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton2");
                if (lb != null)
                {
                    if (dt2.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt2.Rows.Count - 1)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                }
            }
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable2"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable2"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable2"] = dt;
                //----------
                //for (int i = 0; i < dt.Rows.Count - 1; i++)
                //{

                //    //extract the TextBox values   

                //    TextBox box2 = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");


                //    dt.Rows[i]["Column2"] = box2.Text;


                //    //extract the DropDownList Selected Items   

                //    DropDownList ddl2 = (DropDownList)Gridview2.Rows[i].Cells[1].FindControl("DropDownList2");


                //    // Update the DataRow with the DDL Selected Items   

                //    dt.Rows[i]["Column1"] = ddl2.SelectedItem.Text;


                //}
                //Re bind the GridView for the updated data  
                Gridview2.DataSource = dt;
                Gridview2.DataBind();
            }

            //Set Previous Data on Postbacks  
            SetPreviousData2();
        }
        //----------------33
        protected void ButtonAdd3_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid3();
        }
        private void AddNewRowToGrid3()
        {
            int c = Gridview3.Rows.Count - 1;
            double number2;
            TextBox box100 = (TextBox)Gridview3.Rows[c].Cells[2].FindControl("TextBox3");
            DropDownList ddl100 = (DropDownList)Gridview3.Rows[c].Cells[1].FindControl("DropDownList3");
            Label errlable = (Label)Gridview3.FooterRow.Cells[1].FindControl("err_label");
            errlable.Text = "";
            if (((box100.Text != "") && (ddl100.Text != "-1")))
            {
                if (double.TryParse(box100.Text, out number2))
                {
                    if (number2 <= 0)
                    {
                        box100.Text = "";
                        errlable.Text = "Negative value not allowed";
                        goto exit10;
                    }
                    else
                    {
                        box100.Text = number2.ToString();
                    }
                    

                }
                else
                {
                    box100.Text = "";
                    errlable.Text = "Special char not allowed..";
                    goto exit10;
                }
                if (ViewState["CurrentTable3"] != null)
                {

                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable3"];
                    DataRow drCurrentRow = null;

                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                        //add new row to DataTable   
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        //Store the current data to ViewState for future reference   

                        ViewState["CurrentTable3"] = dtCurrentTable;


                        for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                        {

                            //extract the TextBox values   

                            TextBox box3 = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");


                            dtCurrentTable.Rows[i]["Column2"] = box3.Text;


                            //extract the DropDownList Selected Items   

                            DropDownList ddl3 = (DropDownList)Gridview3.Rows[i].Cells[1].FindControl("DropDownList3");


                            // Update the DataRow with the DDL Selected Items   

                            dtCurrentTable.Rows[i]["Column1"] = ddl3.SelectedItem.Text;


                        }

                        //Rebind the Grid with the current data to reflect changes   
                        Gridview3.DataSource = dtCurrentTable;
                        Gridview3.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");

                }
                //Set Previous Data on Postbacks   
                SetPreviousData3();
            }
            else
            {
                errlable.Text = "Please Enter/Select value..";
                goto exit10;
            }

        exit10:
            {

                box100.Focus();
            }
        }
        private void SetPreviousData3()
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable3"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable3"];
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddl3 = (DropDownList)Gridview3.Rows[rowIndex].Cells[1].FindControl("DropDownList3");
                        TextBox box3 = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");

                        //Fill the DropDownList with Data   
                        FillDropDownList3(ddl3);

                        if (i < dt.Rows.Count - 1)
                        {

                            //Assign the value from DataTable to the TextBox  
                            box3.Text = string.Format("{0:##0.00}", double.Parse(dt.Rows[i]["Column2"].ToString()));
                            box3.CssClass = "aligntext";


                            //Set the Previous Selected Items on Each DropDownList  on Postbacks   
                            ddl3.ClearSelection();
                            ddl3.Items.FindByText(dt.Rows[i]["Column1"].ToString()).Selected = true;


                        }

                        rowIndex++;
                    }
                }
            }
        }
        protected void Gridview3_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt3 = (DataTable)ViewState["CurrentTable3"];
                LinkButton lb = (LinkButton)e.Row.FindControl("LinkButton3");
                if (lb != null)
                {
                    if (dt3.Rows.Count > 1)
                    {
                        if (e.Row.RowIndex == dt3.Rows.Count - 1)
                        {
                            lb.Visible = false;
                        }
                    }
                    else
                    {
                        lb.Visible = false;
                    }
                }
            }
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["CurrentTable3"] != null)
            {

                DataTable dt = (DataTable)ViewState["CurrentTable3"];
                if (dt.Rows.Count > 1)
                {
                    if (gvRow.RowIndex < dt.Rows.Count - 1)
                    {
                        //Remove the Selected Row data and reset row number  
                        dt.Rows.Remove(dt.Rows[rowID]);
                        ResetRowID(dt);
                    }
                }

                //Store the current data in ViewState for future reference  
                ViewState["CurrentTable3"] = dt;
                //-------------
                //for (int i = 0; i < dt.Rows.Count - 1; i++)
                //{

                //    //extract the TextBox values   

                //    TextBox box3 = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");


                //    dt.Rows[i]["Column2"] = box3.Text;


                //    //extract the DropDownList Selected Items   

                //    DropDownList ddl3 = (DropDownList)Gridview3.Rows[i].Cells[1].FindControl("DropDownList3");


                //    // Update the DataRow with the DDL Selected Items   

                //    dt.Rows[i]["Column1"] = ddl3.SelectedItem.Text;


                //}

                //Re bind the GridView for the updated data  
                Gridview3.DataSource = dt;
                Gridview3.DataBind();
            }

            //Set Previous Data on Postbacks  
            SetPreviousData3();
        }
        //---
        private void FetchDetails()
        {
            //- - - - -- 
            dr.Close();
            // string ecode = Request.QueryString["empcode"].ToString();
            // string ecode = RadComboBoxPrjEmp.SelectedValue.ToString();
            //conn = new SqlConnection(Session["ConString"].ToString());
            // conn.Open();
            cmd = new SqlCommand("select employee.desig_id,employee.emp_name,employee.emp_lname,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.Company_name,company.Company_id from employee,company where emp_code=" + empid + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                did = Convert.ToInt32(dr["desig_id"]);
                cid = Convert.ToInt32(dr["Company_id"]);
                //Details of employment section
                txtEmpName.Text = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
                txtCompanyName.Text = dr["Company_name"].ToString();
                txtPlace.Text = dr["Address"].ToString();
                txtEmpId.Text = dr["ic_pp_number"].ToString();
                txtStartDate.Text = dr["joining_date"].ToString();

                //working Hours and Rest days section


                //others section
                pp = Convert.ToInt32(dr["probation_period"]);
                txtProlength.Text = dr["probation_period"].ToString();
                txtProsdate.Text = dr["joining_date"].ToString();
                jd = Convert.ToDateTime(dr["joining_date"]);
                txtProedate.Text = jd.AddMonths(pp).ToString();

            }
            dr.Close();
            cmd = new SqlCommand("select Leave_Type,Leaves_Allowed From EmployeeLeavesAllowed Where (leave_type = 8 or leave_type = 9 or leave_type=13) and Emp_ID=" + empid + " and leave_year=2016", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt16(dr["Leave_Type"]) == 8)
                {
                    txtAnnualLeave.Text = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 9)
                {
                    txtSickLeave.Text = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 13)
                {
                    txtHospLeave.Text = dr["Leaves_Allowed"].ToString();
                }

            }
            dr.Close();
            cmd = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,CPF_Entitlement,Hourly_Rate from EmployeePayHistory where Emp_ID=" + empid, conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtSalary.Text = dr["payrate"].ToString();
                txtOTrate.Text = dr["Hourly_Rate"].ToString();
                txtWorkDays.Text = dr["wdays_per_week"].ToString();
                Double rday = 7 - Convert.ToDouble(dr["wdays_per_week"]);
                if (rday == 2)
                {
                    dayDatails.Text = "Saturday & Sunday";
                }
                else if (rday == 1.5)
                {
                    dayDatails.Text = "1/2day Saturday & Sunday";
                }
                else
                {
                    dayDatails.Text = "Sunday";
                }
                txtRestDay.Text = rday.ToString();


                if (dr["CPF_Entitlement"].ToString() == "Y")
                {
                    ckCpf.Checked = true;
                }
                else
                {
                    ckCpf.Checked = false;
                }
                txtOtot.Text = dr["OT1Rate"].ToString();

                // rMonthly.Checked = true;
                if (dr["Pay_Frequency"].ToString() == "M")
                {
                    ckCpf.Checked = true;
                }

            }
            //---
            dr.Close();
            cmd = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtJobTittle.Text = dr["Designation"].ToString();


            }

            //- - - - - -- -
        }

        protected void savebutton_Click(object sender, EventArgs e)
        {
            if (saveEmployee())
            {
                Response.Redirect("../employee/KetForm.aspx");
            }
            
            
            //  string sql = "";
            //  string otperiod = "";

            ////  int count_emp;
            //  //SqlTransaction transaction;
            //  conn2 = new SqlConnection(Session["ConString"].ToString());
            //  conn2.Open();
            //  //transaction = conn2.BeginTransaction("t1");

            //  //-----check for record have r not
            //  sql = "select * from Ket_Manual where emp_code=" + Session["emp_code"].ToString();
            //  cmd2 = new SqlCommand(sql, conn2);
            //  //cmd2.Transaction = transaction;
            //  SqlDataReader dr = cmd2.ExecuteReader();
            ////  transaction.Commit();
            //  if (dr.Read ())
            //  {
            //      dr.Close();
            //      sql = "delete from Ket_Manual where emp_code=" + Session["emp_code"].ToString();
            //      cmd2.CommandText = sql;
            //      cmd2.ExecuteNonQuery();
            //  }
            //  if (txtdet_working_hrs.Text.Length == 0)
            //  {
            //      txtdet_working_hrs.Text = "NIL";
            //  }
            //  sql = "insert into Ket_Manual(emp_code,working_hrs_details,ot_pay_period,date_of_payment,date_of_ot,other_medical_benefit,other_type_of_leaves,paid_medical_exam_fee,job_title_id,duration_end_date,other_salary_related_comp,osrc_amount,job_duties)values(" + Session["emp_code"].ToString() + ",'" + txtdet_working_hrs.Text + "'";
            //      if (rHourly.Checked)
            //      {
            //          otperiod = "H";
            //      }
            //      else if (rDaily.Checked)
            //      {
            //          otperiod = "D";
            //      }
            //      else if (rWeekly.Checked)
            //      {
            //          otperiod = "W";
            //      }
            //      else if (rForntnightly.Checked)
            //      {
            //          otperiod = "F";
            //      }
            //      else if (rMonthly.Checked)
            //      {
            //          otperiod = "M";
            //      }
            //   string examfee="";
            //  if(txtPaidmefee.Checked )
            //  {
            //  examfee ="Y";

            //  }
            // else 
            //  {
            //      examfee ="N";
            //  }
            //  if (txtOtherMedical_benefit.Text.Length == 0)
            //  {
            //      txtOtherMedical_benefit.Text = "NIL";
            //  }

            //  if (txtOthertypeLeave.Text.Length == 0)
            //  {
            //      txtOthertypeLeave.Text = "NIL";
            //  }

            //  if (txtDurationend.Text.Length == 0)
            //  {
            //      txtDurationend.Text = "NIL";
            //  }
            // // if (txtComponents.Text .Length == 0)
            // // {
            // //     txtComponents.Text  ="0";
            // // }
            //  int atype = 0;
            //  //if (ddcomponents.SelectedIndex ==-1)
            //  //{
            //  //    atype = -1;
            //  //}
            //  //else
            //  //{
            //  //    atype = Convert.ToInt32(ddcomponents.SelectedValue);
            //  //}

            //  if (txtDurationend.Text.Length == 0)
            //  {
            //      txtDurationend.Text = "NIL";
            //  }
            //  int jtitle = 0;
            //  if (ddJobTitle.SelectedValue =="")
            //  {
            //      jtitle = 0;
            //  }
            //  else
            //  {
            //      jtitle =Convert.ToInt32 ( ddJobTitle.SelectedValue);
            //  }
            //  string jduties = null;
            //  if (txtJobTittle.Text.Length == 0)
            //  {
            //      jduties = null;
            //  }
            //  else
            //  {
            //      jduties = txtJobTittle.Text;
            //  }
            //      sql = sql + ",'" + otperiod + "'," + ddSalarydate.SelectedValue.ToString() + "," + ddOTdate.SelectedValue.ToString() + ",'" + txtOtherMedical_benefit.Text  + "','"+txtOthertypeLeave.Text +"','"+ examfee +"',"+jtitle +",'"+txtDurationend.Text +"',"+atype  +","+ 0  +",'"+jduties +"')";

            //     // cmd2 = new SqlCommand(sql, conn2);
            //      dr.Close();
            //      cmd2.CommandText = sql;
            //      cmd2.ExecuteNonQuery();
            //  ///-------------------------------------- check existing
            // sql = "select * from  fixed_allowance_deduction where emp_code=" + Session["emp_code"].ToString();
            //  cmd2 = new SqlCommand(sql, conn2);
            //  //cmd2.Transaction = transaction;
            //  SqlDataReader dr1 = cmd2.ExecuteReader();

            //  if (dr1.Read())
            //  {
            //      dr1.Close();
            //      sql = "delete from fixed_allowance_deduction where emp_code=" + Session["emp_code"].ToString();
            //      cmd2.CommandText = sql;
            //      cmd2.ExecuteNonQuery();
            //  }
            //  dr1.Close();
            //  //-----------------------------

            //      for (int i = 0; i < Gridview1.Rows.Count; i++)
            //      {
            //          DropDownList ddlv = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");

            //          TextBox boxv = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
            //          if (ddlv.SelectedItem .ToString()  != "Select")
            //          {
            //              sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','A'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
            //            //  cmd2 = new SqlCommand(sql, conn2);
            //              cmd2.CommandText = sql;
            //              cmd2.ExecuteNonQuery();
            //          }

            //      }
            //  //-------------------------
            //      for (int i = 0; i < Gridview2.Rows.Count; i++)
            //      {
            //          DropDownList ddlv = (DropDownList)Gridview2.Rows[i].Cells[1].FindControl("DropDownList2");

            //          TextBox boxv = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");
            //          if (ddlv.SelectedItem.Value != "-1")
            //          {
            //              sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','D'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
            //              //cmd2 = new SqlCommand(sql, conn2);
            //              cmd2.CommandText = sql;
            //              cmd2.ExecuteNonQuery();
            //          }

            //      }
            //  //-----------------------------------------------
            //      for (int i = 0; i < Gridview3.Rows.Count; i++)
            //      {
            //          DropDownList ddlv = (DropDownList)Gridview3.Rows[i].Cells[1].FindControl("DropDownList3");
            //          TextBox boxv = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");
            //          if (ddlv.SelectedItem.ToString() != "Select")
            //          {
            //              sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','O'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
            //              //  cmd2 = new SqlCommand(sql, conn2);
            //              cmd2.CommandText = sql;
            //              cmd2.ExecuteNonQuery();
            //          }

            //      }

            //      Response.Redirect("../employee/KetForm.aspx");

            //      //cmd2.Transaction.Commit();
            //     // transaction.Commit();
            //    //  transaction.Commit();


        }


        protected void printbutton_Click(object sender, EventArgs e)
        {

            saveEmployee();
            //Create new PDF document
            string txtDesignation = "";
            string txtAnnualLeave = "";
            string txtSickLeave = "";
            string txtHospLeave = "";


            string txtSalary = "";
            string txtOTrate = "";
            string txtWorkDays = "";
            string wdays_per_week = "";
            Double rday = 0.0;
            string dayDatails = "";
            string txtOtot = "";
            string txtRestDay = "";
            string txtComponents = "";
            string txtOT1 = "";

            string txtSalarydate = "";
            string txtOTdate = "";
            string txtOtherMedical_benefit = "";
            string txtOtherTypeOfLeave = "";
            string txtdet_working_hrs = "";
            string txtJobTittle = "";
            string txtAllowancetypes = "";
            string txtcomponents = "";
            double atotal = 0.0;
            double dtotal = 0.0;
            string paid_mediacal_exam_fee = "";

            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd4 = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr4 = cmd4.ExecuteReader();
            if (dr4.Read())
            {
                txtDesignation = dr4["Designation"].ToString();

            }
            else
            {
                txtDesignation = "NIL";
            }

            dr4.Close();
            // ---------------------------------------------------------
            SqlCommand cmd2 = new SqlCommand("select leave_type,leaves_allowed from employeeleavesallowed where (leave_type = 8 or leave_type = 9 or leave_type=13) and emp_id=" + Session["emp_code"].ToString() + " and leave_year=2016", conn);
            dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {

                if (Convert.ToInt32(dr2["leave_type"]) == 8)
                {
                    txtAnnualLeave = dr2["leaves_allowed"].ToString();
                }
                else if (Convert.ToInt32(dr2["leave_type"]) == 9)
                {
                    txtSickLeave = dr2["leaves_allowed"].ToString();
                }
                else if (Convert.ToInt32(dr2["leave_type"]) == 13)
                {
                    txtHospLeave = dr2["leaves_allowed"].ToString();
                }
            }

            //---------------------------------------------------------
            dr2.Close();
            string txtCPFpayabl = "";
            SqlCommand cmd5 = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,OT2Rate,CPF_Entitlement,Daily_Rate,Hourly_Rate from EmployeePayHistory where Emp_ID=" + Session["emp_code"].ToString(), conn);
            SqlDataReader dr3 = cmd5.ExecuteReader();
            while (dr3.Read())
            {
                ot1rate = Convert.ToDouble(dr3["OT1Rate"]);
                ot2rate = Convert.ToDouble(dr3["OT2Rate"]);
                hrate = Convert.ToDouble(dr3["Hourly_Rate"]);
                drate = Convert.ToDouble(dr3["Daily_Rate"]);

                mrate = Convert.ToDouble(dr3["payrate"].ToString().Length ==0 ? 0:dr3["payrate"]);

                txtSalary = dr3["payrate"].ToString();
                double d = ot1rate * Convert.ToDouble(dr3["Hourly_Rate"]);
                txtOTrate = d.ToString("#0.00");
                txtWorkDays = dr3["wdays_per_week"].ToString();
                wdays_per_week = dr3["wdays_per_week"].ToString();
                rday = 7 - Convert.ToDouble(wdays_per_week);

                txtOT1 = dr3["OT1Rate"].ToString();

                if (rday == 2)
                {
                    dayDatails = "Saturday & Sunday";
                }
                else if (rday == 1.5)
                {
                    dayDatails = "1/2day Saturday & Sunday";
                }
                else
                {
                    dayDatails = "Sunday";
                }
                txtRestDay = rday.ToString();

                if (dr3["CPF_Entitlement"].ToString() == "Y")
                {
                    txtCPFpayabl = "Y";
                }
                else
                {
                    txtCPFpayabl = "N";
                }
            }

            dr3.Close();

            //---------------------------
            cmd = new SqlCommand("select employee.pay_frequency,employee.desig_id,employee.emp_name,employee.emp_lname,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.address2,company.postal_code,company.Company_name,company.Company_id from employee,company where emp_code=" + Session["emp_code"].ToString() + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            dr.Read();
            pf = dr["pay_frequency"].ToString().Trim();
            opf = dr["pay_frequency"].ToString().Trim();
            string emp_name = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
            string ic_pp_number = dr["ic_pp_number"].ToString();
            string joining_date = dr["joining_date"].ToString().Substring(0, 10);
            string Address = dr["Address"].ToString();
            string Address2 = dr["address2"].ToString();
            string Address3 = "SINGAPORE :"+dr["postal_code"].ToString();
            string Company_name = dr["Company_name"].ToString();
            string pedate = "";
            jd = Convert.ToDateTime(joining_date);
            pp = Convert.ToInt32(dr["probation_period"]);
            if (pp <= 0)
            {
                pp = 0;
                pedate = "";
            }
            else
            {
                pedate = jd.AddMonths(pp).ToShortDateString();
            }
            string plength = pp.ToString();
            string psdate = dr["joining_date"].ToString().Substring(0, 10);
            jd = Convert.ToDateTime(dr["joining_date"]);

            // string job_duties = dr["job_duties"].ToString();
            string duration_end_date = "";
            dr.Close();
            //------------check in Ket_Manual, record exists or not
            dr3.Close();
            cmd = new SqlCommand("select * from Ket_Manual where emp_code=" + Session["emp_code"].ToString(), conn);
            dr3 = cmd.ExecuteReader();
            int titleid = 0;
            int catid = 0;
            string title = "";
            string txtnoticeperiod = "";
            string idate = "";
            
            if (dr3.Read())
            {
                DateTime dt = Convert.ToDateTime(dr3["issue_date"]);
                idate = dt.ToString("dd/MM/yyyy");
                pf = dr3["salary_period"].ToString();
                printbutton.Enabled = true;
                titleid = Convert.ToInt32(dr3["job_title_id"]);
                txtComponents = dr3["osrc_amount"].ToString();
                duration_end_date = dr3["duration_end_date"].ToString();
                duration_end_date = duration_end_date.Trim();
                txtOtherTypeOfLeave = dr3["other_type_of_leaves"].ToString();

               
                 txtnoticeperiod = dr3["notice_period"].ToString();
              

                if (duration_end_date == "NA" && duration_end_date == "")
                {
                    duration_end_date = "Continue";
                }

                if (pf == "W")
                {
                    txtSalary = dr3["payrate"].ToString();
                    sal_del_text = "( per Weekly)";
                   
                }
                else if (pf == "F")
                {
                    txtSalary = dr3["payrate"].ToString();
                    sal_del_text = "( per Fortnightly)";
                }
                else if (pf == "H")
                {
                    txtSalary = hrate.ToString ();
                    sal_del_text = "( per Hourly)";
                }
                else if (pf == "D")
                {
                    txtSalary = drate.ToString();
                    sal_del_text = "( per Daily)";
                }
                else if (pf == "M")
                {
                    txtSalary = mrate.ToString();
                    sal_del_text = "( per Monthly)";
                }

                opf = dr3["ot_pay_period"].ToString();
                txtSalarydate = dr3["date_of_payment"].ToString();
                txtOTdate = dr3["date_of_ot"].ToString();
                txtOtherMedical_benefit = dr3["other_medical_benefit"].ToString();
                txtdet_working_hrs = dr3["working_hrs_details"].ToString();


                if (dr3["job_duties"].ToString() == "NIL")
                {
                    txtJobTittle = "";
                }
                else
                {
                    txtJobTittle = dr3["job_duties"].ToString();
                }
                if ((dr3["other_salary_related_comp"].ToString() == "0"))
                {

                }
                else
                {
                    txtAllowancetypes = dr3["other_salary_related_comp"].ToString();
                    txtcomponents = dr3["other_salary_related_comp"].ToString();
                }
                if ((dr3["paid_medical_exam_fee"].ToString() == "Y"))  //murugan17032017
                {
                    //txtPaidmefee.Checked = true;
                    paid_mediacal_exam_fee = "Y";
                }
                else
                {
                    //txtPaidmefee.Checked = false;
                    paid_mediacal_exam_fee = "N";
                }
            }
            else
            {

                //rMonthly.Checked = true;
                //ddSalarydate.SelectedValue = "1";
                // ddOTdate.SelectedValue = "1";
                // txtOtherMedical_benefits.Value = "NIL";
                //txtdet_working_hrs.Text = "";

            }
            //------- find job category
            dr3.Close();
            if (titleid == 0)
            {

            }
            else
            {
                cmd = new SqlCommand("select * from  JobTitle where id=" + titleid, conn);
                dr = cmd.ExecuteReader();
                string cname = "";
                if (dr.Read())
                {
                    catid = Convert.ToInt32(dr["cat_id"]);
                    cname = dr["cat_title"].ToString();

                }
                dr.Close();
                SqlCommand sc = new SqlCommand("select * from JobCategory where cat_id=" + catid, conn);
                SqlDataReader sdr = sc.ExecuteReader();
                if (sdr.Read())
                {
                    txtJobTittle = sdr["cat_name"].ToString() + "," + cname + "\n" + txtJobTittle;
                }
                
                sdr.Close();
            }


            //------------------------------
            SqlCommand cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + Session["emp_code"].ToString(), conn);
            SqlDataReader dr11 = cmd11.ExecuteReader();
            PdfPTable all_table = new PdfPTable(6);
            PdfPCell acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            all_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Allowance($)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            all_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {

                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.Colspan = 4;
                all_table.AddCell(acell);
                acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                acell.Colspan = 2;
                all_table.AddCell(acell);
                atotal = atotal + Convert.ToDouble(dr11["amount"].ToString());
            }
            acell = new PdfPCell(new Phrase("Total Fixed Allowance", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            all_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(atotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            all_table.AddCell(acell);

            dr11.Close();
            //-----------------------------------------------------------------

            cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + Session["emp_code"].ToString(), conn);
            dr11 = cmd11.ExecuteReader();
            PdfPTable ded_table = new PdfPTable(6);
            acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Deduction($)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {

                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.Colspan = 4;
                ded_table.AddCell(acell);
                acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                acell.Colspan = 2;
                ded_table.AddCell(acell);
                dtotal = dtotal + Convert.ToDouble(dr11["amount"].ToString());
            }
            acell = new PdfPCell(new Phrase("Total Fixed Deduction", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            ded_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(dtotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            ded_table.AddCell(acell);
            dr11.Close();


            //-----------------------------------

            cmd11 = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='O' and  emp_code=" + Session["emp_code"].ToString(), conn);
            dr11 = cmd11.ExecuteReader();
            PdfPTable other_table = new PdfPTable(6);
            acell = new PdfPCell(new Phrase("Item", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            acell = new PdfPCell(new Phrase("Allowance($)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            atotal = 0.0;
            dtotal = 0.0;
            while (dr11.Read())
            {
                acell = new PdfPCell(new Phrase(dr11["type_desc"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.Colspan = 4;
                other_table.AddCell(acell);

                acell = new PdfPCell(new Phrase(dr11["amount"].ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                acell.Colspan = 2;
                other_table.AddCell(acell);
                dtotal = dtotal + Convert.ToDouble(dr11["amount"].ToString());
            }
            acell = new PdfPCell(new Phrase("Total Salary Related Components", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 4;
            acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            other_table.AddCell(acell);
            acell = new PdfPCell(new Phrase(dtotal.ToString("##0.00"), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
            acell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
            acell.Padding = 3;
            acell.Colspan = 2;
            acell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            other_table.AddCell(acell);
            dr11.Close();
            //-----------------------------------

            Document document = new Document(PageSize.A4, 88f, 88f, 15f, 15f);

            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                PdfPTable table2 = null;
                PdfPTable table3 = null;
                //-- set for heading

                //--set for sub headding

                //--set for details


                document.Open();
                //---------create image cellq
                table2 = new PdfPTable(12);
                // iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));

                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Full-Time Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 11;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                //---------create image cell
                table3 = new PdfPTable(12);
                iTextSharp.text.Image pic2 = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic2, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("Part-Time Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 11;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table3.AddCell(cell);

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                
               
                table.LockedWidth = true;
                //----------------------top heading
                PdfPTable head_table1 = new PdfPTable(5);

                PdfPTable head_table = new PdfPTable(1);

                acell = new PdfPCell(new Phrase("Key Employment Terms", FontFactory.GetFont("Franklin Gothic Medium", 25, iTextSharp.text.Font.BOLD , iTextSharp.text.BaseColor.BLACK)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth  = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(new Phrase("All fields are mandatory, unless they are not applicable", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(head_table);
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 0;
                acell.Colspan = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table1.AddCell(acell);


                head_table = new PdfPTable(2);
                
                acell = new PdfPCell(new Phrase("Issued on:", FontFactory.GetFont("Franklin Gothic Medium", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);

                acell = new PdfPCell(new Phrase(idate , FontFactory.GetFont("Franklin Gothic Medium", 15, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY )));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);


                acell = new PdfPCell(new Phrase("All information accurate as of Issuance date", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY )));
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.Colspan = 2;
                acell.BorderWidth = 0;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table.AddCell(acell);



                acell = new PdfPCell(head_table);
                acell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                acell.Padding = 3;
                acell.Colspan = 2;
                acell.BorderWidth = 1;
                acell.BorderColor = iTextSharp.text.BaseColor.GRAY;
                acell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                head_table1.AddCell(acell);

                PdfPCell head_cell = new PdfPCell(head_table1);
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.WHITE ;
                head_cell.Padding = 3;
                head_cell.BorderWidth = 0;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);
                 
                
               
                //------------------
                //PdfPCell head_cell = new PdfPCell(new Phrase("Key Employment Terms", FontFactory.GetFont("Franklin Gothic Medium", 25, iTextSharp.text.Font.NORMAL , iTextSharp.text.BaseColor.BLACK)));
                //head_cell.Colspan = 2;
                //head_cell.BackgroundColor = iTextSharp.text.BaseColor.WHITE;
                //head_cell.BorderWidth = 0;
                //head_cell.Padding = 10;
                //head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                //table.AddCell(head_cell);


                //section A
                //cell = new PdfPCell(new Phrase("Section A | Details of Employment",head_font ));

                head_cell = new PdfPCell(new Phrase("Section A | Details of Employment", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                PdfPCell subhead_cell = new PdfPCell(new Phrase("Company Name", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Job Title, Main Duties and Responsibilities", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                PdfPCell del_cell = new PdfPCell(new Phrase(Company_name, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase(txtJobTittle, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;

                // del_cell.BorderColorTop = iTextSharp.text.BaseColor.LIGHT_GRAY ;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Employee Name", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                table.AddCell(table2);


                del_cell = new PdfPCell(new Phrase(emp_name, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                table.AddCell(table3);

                subhead_cell = new PdfPCell(new Phrase("Employee NRIC/FIN", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);
                subhead_cell = new PdfPCell(new Phrase("Duration of Employment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase(ic_pp_number, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase(joining_date + "-" + duration_end_date, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Employment Start Date", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);
                subhead_cell = new PdfPCell(new Phrase("Place of Work", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase(joining_date, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase(Address+"\n"+Address2 +"\n"+Address3 , FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));

                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //section B-----------------------------------------------------

                head_cell = new PdfPCell(new Phrase("Section B | Working Hours and Rest Days", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Details of Working Hours", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Number of Working Days Per Week", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(txtdet_working_hrs.ToString(), FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.Rowspan = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase(txtWorkDays + " days per week", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Rest Day Per Week", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(rday + " day per week (" + dayDatails + ") ", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                //---------------------------------------------------------------------------------------------------
                //section C
                head_cell = new PdfPCell(new Phrase("Section C | Salary", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Salary Period   1st to 31st", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                subhead_cell = new PdfPCell(new Phrase("Date(s) of Salary Payment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //-----------salary period
                //---------create image cellq
                table2 = new PdfPTable(12);
                //pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                if (pf == "H")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Hourly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (pf == "D")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Daily", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (pf == "W")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Weekly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (pf == "F")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fortnightly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (pf == "M")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monthly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                subhead_cell = new PdfPCell(table2);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.Rowspan = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;


                table.AddCell(subhead_cell);
                //-----------------------

                del_cell = new PdfPCell(new Phrase(txtSalarydate + " of every calendar month", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                subhead_cell = new PdfPCell(new Phrase("Date(s) of Overtime Payment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase(txtOTdate + " of every calendar month", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                //--------------------------------------------------
                subhead_cell = new PdfPCell(new Phrase("Overtime Payment Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                subhead_cell = new PdfPCell(new Phrase("Basic Salary (Per Period)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                del_cell = new PdfPCell(new Phrase("(only if different from salary period)", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                // del_cell.Rowspan = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.BorderWidthBottom = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase("$" + txtSalary + sal_del_text, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);
                //-------overtime check boxes
                table2 = new PdfPTable(12);
                //pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                if (opf == "H")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                //pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Hourly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "D")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Daily", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "W")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Weekly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "F")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fortnightly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                if (opf == "M")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
               // pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Monthly", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 5;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                subhead_cell = new PdfPCell(table2);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.Rowspan = 2;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;


                table.AddCell(subhead_cell);
                //-----------------------


                subhead_cell = new PdfPCell(new Phrase("Overtime Rate of Pay", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                del_cell = new PdfPCell(new Phrase("$" + txtOTrate + " (per hour)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                subhead_cell = new PdfPCell(new Phrase("Fixed Allowances Per Salary Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                subhead_cell = new PdfPCell(new Phrase("Fixed Deductions Per Salary Period", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                subhead_cell = new PdfPCell(all_table);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //------------------------allowance table
                subhead_cell = new PdfPCell(ded_table);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //----------------------

                subhead_cell = new PdfPCell(new Phrase("Other Salary-Related Components", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //---------create image cellq
                table2 = new PdfPTable(11);

                if (txtCPFpayabl == "Y")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }

                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("CPF Contributions Payable", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);

                del_cell = new PdfPCell(other_table);
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase("(subject to prevailing CPF contribution rates)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //section D-----------------------------------------------------------
                document.Add(table);
                document.Add(new Phrase(Chunk.NEXTPAGE));

                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;

                head_cell = new PdfPCell(new Phrase("Section D | Leave and Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Types of Leave", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                subhead_cell = new PdfPCell(new Phrase("Other Types of Leave", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                del_cell = new PdfPCell(new Phrase("(applicable if service is at least 3 months)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);


                del_cell = new PdfPCell(new Phrase("(e.g. Paid Maternity Leave)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Paid Annual Leave \nPer Year                         :" + txtAnnualLeave + " (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BorderColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);
                table.AddCell(table2);

                del_cell = new PdfPCell(new Phrase(txtOtherTypeOfLeave, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLUE)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.Rowspan = 2;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                del_cell = new PdfPCell(new Phrase("(for 1st year of service)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.GRAY)));
                del_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                del_cell.Padding = 3;
                del_cell.BorderWidthTop = 0;
                del_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                del_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(del_cell);

                //----------------
                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Paid Outpatient Sick \nLeave Per Year              :" + txtSickLeave + " (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);
                //-----------------
                table2 = new PdfPTable(11);
                if (paid_mediacal_exam_fee == "Y")
                {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                }
                else {
                    pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/uncheck1.jpg"));
                }
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);
               
                cell = new PdfPCell(new Phrase("Paid Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                            
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);

                //-----------
                table2 = new PdfPTable(11);
                pic = iTextSharp.text.Image.GetInstance(Server.MapPath("~/frames/images/HOME/check1.jpg"));
                pic.ScaleAbsolute(9, 9);
                cell = new PdfPCell(pic, true);
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.BorderWidth = 0;
                cell.Padding = 5;
                cell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
                cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                cell.Border = PdfPCell.NO_BORDER;
                table2.AddCell(cell);


                cell = new PdfPCell(new Phrase("Paid Hospitalisation \nLeave Per Year              :" + txtHospLeave + " (days)", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 10;
                cell.Border = PdfPCell.NO_BORDER;
                cell.BorderWidth = 0;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table2.AddCell(cell);

                table.AddCell(table2);
                //--------------------------------
                subhead_cell = new PdfPCell(new Phrase("Other Medical Benefits", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.BorderColorBottom = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                subhead_cell = new PdfPCell(new Phrase("(Note that paid hospitalisation per year is inclusive of paid outpatient sick leave. Leave entitlement for part-time employees may be pro-rated based on hours.)", FontFactory.GetFont("Franklin Gothic Medium", 8, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //subhead_cell = new PdfPCell(new Phrase("(optional,to specify)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY )));
                subhead_cell = new PdfPCell(new Phrase(txtOtherMedical_benefit, FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 0;
                subhead_cell.BorderColorTop = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);

                //section E-----------------------------------------------------------

                // Phrase page = new Phrase(Chunk.NEWPAGE);

                head_cell = new PdfPCell(new Phrase("Section E | Others", FontFactory.GetFont("Franklin Gothic Medium", 14, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.WHITE)));
                head_cell.Colspan = 2;
                head_cell.BackgroundColor = iTextSharp.text.BaseColor.GRAY;
                head_cell.Padding = 3;
                head_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                table.AddCell(head_cell);


                subhead_cell = new PdfPCell(new Phrase("Length of Probation : " + plength + " month(s)\n\nProbation Start Date :" + psdate + "\n\nProbation End Date :" + pedate, FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 1;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);


                Phrase p = new Phrase("Notice Period for Termination of \nEmployment", FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK));
                subhead_cell.AddElement(p);
                p = new Phrase("(Initiated by either party whereby the \nlength shall be the same)", FontFactory.GetFont("Franklin Gothic Medium", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.GRAY));
                subhead_cell.AddElement(p);
                p = new Phrase(txtnoticeperiod, FontFactory.GetFont("Franklin Gothic Medium", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE));
                subhead_cell.AddElement(p);
                subhead_cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                subhead_cell.Padding = 3;
                subhead_cell.BorderWidthBottom = 1;
                subhead_cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                subhead_cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                table.AddCell(subhead_cell);



                document.Add(table);


                //Separater Line
                // color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                // DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, Color.Black );
                // DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, Color.Black );
                // document.Add(table);

                document.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();


            }


        }

        
        protected void GenerateReport(object sender, EventArgs e)
        {
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            //  cmd = new SqlCommand("select employee.desig_id,employee.emp_name,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.Company_name,company.Company_id from employee,company where emp_code=" + Session["emp_code"].ToString() + " and employee.Company_id=company.Company_id", conn);
            //  dr = cmd.ExecuteReader();
            // dr.Read();

            Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
            //  iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL, Color.Black );
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                //Color color = null;

                document.Open();

                //Header Table
                table = new PdfPTable(2);
                table.TotalWidth = 500f;
                table.LockedWidth = true;
                //table.SetWidths(new float[] { 0.5f, 0.5f });

                //section A
                cell = new PdfPCell(new Phrase("Section A | Details of Employment", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.Colspan = 2;
                cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Company Name\n\nCompany name", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("Job Tittle, Main Duties and Responsibilities", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK)));
                cell.BorderColorBottom = iTextSharp.text.BaseColor.WHITE;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("@txtCompany Name", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell.BorderColorTop = iTextSharp.text.BaseColor.WHITE;

                table.AddCell(cell);
                cell = new PdfPCell(new Phrase("@txtJob", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK)));
                cell.BorderColorTop = iTextSharp.text.BaseColor.WHITE;

                table.AddCell(cell);
                //----------

                //----------

                document.Add(table);

                //Separater Line
                // color = new Color(System.Drawing.ColorTranslator.FromHtml("#A9A9A9"));
                // DrawLine(writer, 25f, document.Top - 79f, document.PageSize.Width - 25f, document.Top - 79f, Color.Black );
                // DrawLine(writer, 25f, document.Top - 80f, document.PageSize.Width - 25f, document.Top - 80f, Color.Black );
                // document.Add(table);

                document.Close();

                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=Employee.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Close();
            }
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            // contentByte.SetColorStroke(color  );
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }
        private static PdfPCell PhraseCell(Phrase phrase, int align)
        {
            PdfPCell cell = new PdfPCell(phrase);
            cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 2f;
            cell.PaddingTop = 0f;
            return cell;
        }
        protected void GenerateReport2(object sender, EventArgs e)
        {
            string Designation = "";
            string AnnualLeave = "";
            string SickLeave = "";
            string HospLeave = "";


            string txtSalary = "";
            string txtOTrate = "";
            string txtWorkDays = "";
            string wdays_per_week = "";
            Double rday = 0.0;
            string dayDatails = "";
            string txtOtot = "";
            string txtRestDay = "";

            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd4 = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr4 = cmd4.ExecuteReader();
            if (dr4.Read())
            {
                Designation = dr4["Designation"].ToString();

            }
            else
            {
                Designation = "NIL";
            }

            dr4.Close();
            //---------------------------------------------------------
            cmd2 = new SqlCommand("select Leave_Type,Leaves_Allowed From EmployeeLeavesAllowed Where (leave_type = 8 or leave_type = 9 or leave_type=13) and Emp_ID=" + Session["emp_code"].ToString() + " and leave_year=2016", conn);
            dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                if (Convert.ToInt16(dr["Leave_Type"]) == 8)
                {
                    AnnualLeave = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 9)
                {
                    SickLeave = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 13)
                {
                    HospLeave = dr["Leaves_Allowed"].ToString();
                }
            }
            dr2.Close();
            //---------------------------------------------------------

            cmd3 = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,CPF_Entitlement,Hourly_Rate from EmployeePayHistory where Emp_ID=" + Session["emp_code"].ToString(), conn);
            dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                txtSalary = dr3["payrate"].ToString();
                txtOTrate = dr3["Hourly_Rate"].ToString();
                txtWorkDays = dr3["wdays_per_week"].ToString();
                wdays_per_week = dr3["wdays_per_week"].ToString();
                rday = 7 - Convert.ToDouble(wdays_per_week);
                if (rday == 2)
                {
                    dayDatails = "Saturday & Sunday";
                }
                else if (rday == 1.5)
                {
                    dayDatails = "1/2day Saturday & Sunday";
                }
                else
                {
                    dayDatails = "Sunday";
                }
                txtRestDay = rday.ToString();


                //if (dr["CPF_Entitlement"].ToString() == "Y")
                //{
                //    ckCpf.Checked = true;
                //}
                //txtOtot.Text = dr["OT1Rate"].ToString();

                //rMonthly.Checked = true;
                //if (dr["Pay_Frequency"].ToString() == "M")
                //{
                //    ckCpf.Checked = true;
                //}

            }
            dr3.Close();
            //---------------------------

            cmd = new SqlCommand("select employee.desig_id,employee.emp_name,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.Company_name,company.Company_id from employee,company where emp_code=" + Session["emp_code"].ToString() + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            dr.Read();






            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {


                    StringBuilder sb = new StringBuilder();

                    //---------------------------------A
                    sb.Append("<table border='1'>");
                    sb.Append("<th colspan ='2' bgcolor='gray' >Section A | Details of Employment</th></tr>");
                    sb.Append("<tr><td style='width: 489px'>Company Name<br />" + dr["Company_name"].ToString());
                    sb.Append("<td style='width: 462px'>Job Title,Main Duties and Responsibilitites<br />" + Designation + "</tr>");
                    sb.Append("<tr><td style='width: 489px'>Employee Name<br /><br />" + dr["emp_name"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Full-Time Employment<br /></td></tr>");
                    sb.Append("<tr><td style='width: 489px'>Employee NRIC/FIN<br />" + dr["ic_pp_number"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Duration of Employment<br />" + "period" + "</dr></tr>");
                    sb.Append("<tr><td style='width: 489px'>Employment Start Date<br />" + dr["joining_date"].ToString() + "</td>");
                    sb.Append("<td style='width: 462px'>Place of Work<br />" + dr["Address"].ToString() + "</td></tr></table>");

                    //-----------------------------------B
                    sb.Append("<table border='1'>");
                    sb.Append("<th colspan ='2' bgcolor='gray' >Section B | Working Hours and Rest Days</th></tr>");
                    sb.Append("<tr><td rowspan='2' style='width: 489px'>Details of Working Hours<br />" + "---------" + "</dr>");
                    sb.Append("<td style='width: 462px'>Number of Working Days Per Week <br />" + txtWorkDays + " days per week</td></tr>");
                    sb.Append("<tr><td style='width: 489px'>Rest Day Per Week<br /><br />" + rday.ToString() + " day(s) per week(" + dayDatails + ")</td></tr></table>");
                    //-----------------------------------C
                    sb.Append("<table border='1'>");
                    sb.Append("<th colspan ='2' bgcolor='gray' >Section C | Salary</th></tr>");
                    sb.Append("<tr><td rowspan='2' style='width: 489px'>Salary Period  1st to 31st<br /><input name='hr' type='radio' checked />Hourly</td>");

                    sb.Append("<td style='width: 462px'>Date(s) of Salary Payment<br />" + ddSalarydate.SelectedValue + " of every calendar month</td></tr>");
                    sb.Append("<tr><td style='width: 489px'>Date(s) of Overtime Payment<br />" + ddOTdate.SelectedValue + " of every calendar month</td></tr></table>");


                    sb.Append("\r");
                    //----------------------------
                    StringReader sr = new StringReader(sb.ToString());
                    Document pdfdoc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                    HTMLWorker htmlparser = new HTMLWorker((pdfdoc));
                    //
                    //--------------------------


                    PdfWriter pdfw = PdfWriter.GetInstance(pdfdoc, Response.OutputStream);
                    pdfdoc.Open();

                    htmlparser.Parse(sr);
                    // pdfdoc.NewPage();
                    // htmlparser.Parse(sr);


                    pdfdoc.Close();


                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Session["emp_code"].ToString() + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfdoc);


                    //  System.Diagnostics.Process.Start("C:\\Users\\sangu\\Downloads\\" + ono + ".pdf");
                    // Response.End();
                }
            }


        }
        private  bool saveEmployee()
        {
            
            double number2;
            for (int i = 0; i < Gridview1.Rows.Count-1; i++)
            {
                DropDownList ddlv = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");

                TextBox boxv = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
                if (ddlv.SelectedItem.ToString() != "Select")
                {
                    if (double.TryParse(boxv.Text, out number2))
                    {
                        if (number2 <= 0)
                        {
                            goto exit10;
                        }
                        else
                        {
                            boxv.Text = number2.ToString();
                        }
                     }
                    else
                    {
                        goto exit10;
                    }


                }

            }
        //------------------------------------------------
            for (int i = 0; i < Gridview2.Rows.Count - 1; i++)
            {
                DropDownList ddlv = (DropDownList)Gridview2.Rows[i].Cells[1].FindControl("DropDownList2");

                TextBox boxv = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");
                if (ddlv.SelectedItem.ToString() != "Select")
                {
                    if (double.TryParse(boxv.Text, out number2))
                    {
                        if (number2 <= 0)
                        {
                            goto exit11;
                        }
                        else
                        {
                            boxv.Text = number2.ToString();
                        }
                    }
                    else
                    {
                        goto exit11;
                    }


                }

            }

        //-------------------------------------------------------
            for (int i = 0; i < Gridview3.Rows.Count - 1; i++)
            {
                DropDownList ddlv = (DropDownList)Gridview3.Rows[i].Cells[1].FindControl("DropDownList3");

                TextBox boxv = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");
                if (ddlv.SelectedItem.ToString() != "Select")
                {
                    if (double.TryParse(boxv.Text, out number2))
                    {
                        if (number2 <= 0)
                        {
                            goto exit12;
                        }
                        else
                        {
                            boxv.Text = number2.ToString();
                        }
                    }
                    else
                    {
                        goto exit12;
                    }


                }

            }
            //-------------------------
                string sql = "";
                string otperiod = "";

                //  int count_emp;
                //SqlTransaction transaction;
                conn2 = new SqlConnection(Session["ConString"].ToString());
                conn2.Open();
                //transaction = conn2.BeginTransaction("t1");

                //-----check for record have r not
                sql = "select * from Ket_Manual where emp_code=" + Session["emp_code"].ToString();
                cmd2 = new SqlCommand(sql, conn2);
                //cmd2.Transaction = transaction;
                SqlDataReader dr = cmd2.ExecuteReader();
                //  transaction.Commit();
                if (dr.Read())
                {
                    dr.Close();
                    sql = "delete from Ket_Manual where emp_code=" + Session["emp_code"].ToString();
                    cmd2.CommandText = sql;
                    cmd2.ExecuteNonQuery();
                }
                if (txtdet_working_hrs.Text.Length == 0)
                {
                    txtdet_working_hrs.Text = "NIL";
                }
                sql = "insert into Ket_Manual(emp_code,working_hrs_details,ot_pay_period,date_of_payment,date_of_ot,other_medical_benefit,other_type_of_leaves,paid_medical_exam_fee,job_title_id,duration_end_date,other_salary_related_comp,osrc_amount,job_duties,pay_frequency,salary_period,payrate,notice_period,issue_date)values(" + Session["emp_code"].ToString() + ",'" + txtdet_working_hrs.Text + "'";
                if (rHourlyot.Checked)
                {
                    otperiod = "H";
                }
                else if (rDailyot.Checked)
                {
                    otperiod = "D";
                }
                else if (rWeeklyot.Checked)
                {
                    otperiod = "W";
                }
                else if (rFortnightlyot.Checked)
                {

                    otperiod = "F";
                }
                else if (rMonthlyot.Checked)
                {
                    otperiod = "M";
                }
                string examfee = "";

                if (txtPaidmefee.Checked)
                {
                    examfee = "Y";

                }
                else
                {
                    examfee = "N";
                }

                if (txtOtherMedical_benefit.Text.Length == 0)
                {
                    txtOtherMedical_benefit.Text = "NIL";
                }

                if (txtOthertypeLeave.Text.Length == 0)
                {
                    txtOthertypeLeave.Text = "NIL";
                }

                if (txtDurationend.Text.Length == 0)
                {
                    txtDurationend.Text = "NIL";
                }
                // if (txtComponents.Text .Length == 0)
                // {
                //     txtComponents.Text  ="0";
                // }
                int atype = 0;
                //if (ddcomponents.SelectedIndex ==-1)
                //{
                //    atype = -1;
                //}
                //else
                //{
                //    atype = Convert.ToInt32(ddcomponents.SelectedValue);
                //}

                if (txtDurationend.Text.Length == 0)
                {
                    txtDurationend.Text = "NIL";
                }
                int jtitle = 0;
                if (ddJobTitle.SelectedValue == "")
                {
                    jtitle = 0;
                }
                else
                {
                    jtitle = Convert.ToInt32(ddJobTitle.SelectedValue);
                }
                string jduties = null;
                string pf = null;
                string salary_period = null;
                string ot_salary_period = null;
                string payrate = null;

                if (txtJobTittle.Text.Length == 0)
                {
                    jduties = null;
                }
                else
                {
                    jduties = txtJobTittle.Text;
                }
                //if (FullTime.Checked)
                //{
                //    pf = "F";
                //}
                //else
                //{
                //    pf = "P";
                //}
                payrate = "0";
                if (rWeekly.Checked)
                {
                    salary_period = "W";
                    if (txt_weekly_fortnightly.Text.Length == 0)
                    {
                        payrate = "0";
                    }
                    else
                    {
                        payrate = txt_weekly_fortnightly.Text;
                    }
                }
                else if (rForntnightly.Checked)
                {
                    salary_period = "F";
                    if (txt_weekly_fortnightly.Text.Length == 0)
                    {
                        payrate = "0";
                    }
                    else
                    {
                        payrate = txt_weekly_fortnightly.Text;
                    }
                    
                }
                else if(rMonthly .Checked )
                {
                    salary_period = "M";
                    

                }
                else if(rHourly.Checked )
                {
                    salary_period = "H";


                }
                else if (rDaily.Checked )
                {
                    salary_period = "D";


                }
                DateTime issuedate = DateTime.Now;
                if (issue_date.Text.Length > 10)
                {
                    issuedate = Convert.ToDateTime(issue_date.Text);
                }
                

                sql = sql + ",'" + otperiod + "'," + ddSalarydate.SelectedValue.ToString() + "," + ddOTdate.SelectedValue.ToString() + ",'" + txtOtherMedical_benefit.Text + "','" + txtOthertypeLeave.Text + "','" + examfee + "'," + jtitle + ",'" + txtDurationend.Text + "'," + atype + "," + 0 + ",'" + jduties + "','" + pf + "','" + salary_period + "',"+payrate +",'"+ txtNoticeperiod.Text  +"','"+issuedate.ToString("yyyy/MM/dd")+"')";

                // cmd2 = new SqlCommand(sql, conn2);
                dr.Close();
                cmd2.CommandText = sql;
                cmd2.ExecuteNonQuery();
                ///-------------------------------------- check existing
                sql = "select * from  fixed_allowance_deduction where emp_code=" + Session["emp_code"].ToString();
                cmd2 = new SqlCommand(sql, conn2);
                //cmd2.Transaction = transaction;
                SqlDataReader dr1 = cmd2.ExecuteReader();

                if (dr1.Read())
                {
                    dr1.Close();
                    sql = "delete from fixed_allowance_deduction where emp_code=" + Session["emp_code"].ToString();
                    cmd2.CommandText = sql;
                    cmd2.ExecuteNonQuery();
                }
                dr1.Close();
                //-----------------------------

                for (int i = 0; i < Gridview1.Rows.Count; i++)
                {
                    DropDownList ddlv = (DropDownList)Gridview1.Rows[i].Cells[1].FindControl("DropDownList1");

                    TextBox boxv = (TextBox)Gridview1.Rows[i].Cells[2].FindControl("TextBox1");
                    if (ddlv.SelectedItem.ToString() != "Select")
                    {
                        sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','A'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
                        //  cmd2 = new SqlCommand(sql, conn2);
                        cmd2.CommandText = sql;
                        cmd2.ExecuteNonQuery();
                    }

                }
                //-------------------------
                for (int i = 0; i < Gridview2.Rows.Count; i++)
                {
                    DropDownList ddlv = (DropDownList)Gridview2.Rows[i].Cells[1].FindControl("DropDownList2");

                    TextBox boxv = (TextBox)Gridview2.Rows[i].Cells[2].FindControl("TextBox2");
                    if (ddlv.SelectedItem.Value != "-1")
                    {
                        sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','D'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
                        //cmd2 = new SqlCommand(sql, conn2);
                        cmd2.CommandText = sql;
                        cmd2.ExecuteNonQuery();
                    }

                }
                //-----------------------------------------------
                for (int i = 0; i < Gridview3.Rows.Count; i++)
                {
                    DropDownList ddlv = (DropDownList)Gridview3.Rows[i].Cells[1].FindControl("DropDownList3");
                    TextBox boxv = (TextBox)Gridview3.Rows[i].Cells[2].FindControl("TextBox3");
                    if (ddlv.SelectedItem.ToString() != "Select")
                    {
                        sql = "insert into fixed_allowance_deduction(type_id,type_desc,allowance_deduction,amount,emp_code)values(" + ddlv.SelectedItem.Value + ",'" + ddlv.SelectedItem.Text + "','O'," + boxv.Text + "," + Session["emp_code"].ToString() + ")";
                        //  cmd2 = new SqlCommand(sql, conn2);
                        cmd2.CommandText = sql;
                        cmd2.ExecuteNonQuery();
                    }

                }
                return true;
                //  Response.Redirect("../employee/KetForm.aspx");

                //cmd2.Transaction.Commit();
                // transaction.Commit();
                //  transaction.Commit();

            exit10:
                
                {
                    Label errlable = (Label)Gridview1.FooterRow.Cells[1].FindControl("err_label");
                    errlable.Text = "Invalid amount value ..";
                    return false;
                }
            exit11:
                {
                    Label errlable = (Label)Gridview2.FooterRow.Cells[1].FindControl("err_label");
                    errlable.Text = "Invalid amount value ..";
                    return false;
                }
            exit12:
                {
                    Label errlable = (Label)Gridview3.FooterRow.Cells[1].FindControl("err_label");
                    errlable.Text = "Invalid amount value ..";
                    return false;
                }
        }
        private void FetchEmployee()
        {
            //  SetInitialRow();
            //Session["emp_code"] = e.Value;

            //string ecode = Request.QueryString["empcode"].ToString();
            string pf = "";
            string ecode = empid;
            int rowIndex = 0;
            conn = new SqlConnection(Session["ConString"].ToString());
            conn.Open();
            cmd = new SqlCommand("select employee.pay_frequency,employee.payrolltype ,employee.desig_id,employee.emp_name,employee.emp_lname,employee.ic_pp_number,employee.joining_date,employee.probation_period,company.Address,company.address2,company.postal_code,company.Company_name,company.Company_id from employee,company where emp_code=" + empid + " and employee.Company_id=company.Company_id", conn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                pf = dr["pay_frequency"].ToString().Trim();
                payrolltype = Convert.ToInt32(dr["payrolltype"].ToString());
                did = Convert.ToInt32(dr["desig_id"]);
                cid = Convert.ToInt32(dr["Company_id"]);
                //Details of employment section
                txtEmpName.Text = dr["emp_name"].ToString() + " " + dr["emp_lname"].ToString();
                txtCompanyName.Text = dr["Company_name"].ToString();
                txtPlace.Text = dr["Address"].ToString();
                txtPlace2.Text = dr["address2"].ToString();
                txtPlace3.Text ="SINGAPORE "+ dr["postal_code"].ToString();
                txtEmpId.Text = dr["ic_pp_number"].ToString();
                txtStartDate.Text = dr["joining_date"].ToString().Substring(0, 10);
                jd = Convert.ToDateTime(dr["joining_date"]);
                txtDurationstart.Text = dr["joining_date"].ToString().Substring(0, 10);

                //working Hours and Rest days section
                if (dr["pay_frequency"].ToString().Trim() == "M")
                {
                    FullTime.Checked = true;
                }
                else
                {
                    PartTime.Checked = true;
                }

                //others section
                pp = Convert.ToInt32(dr["probation_period"]);
                if (pp <= 0)
                {
                    pp = 0;
                    txtProedate.Text = "";
                }
                else
                {
                    txtProedate.Text = jd.AddMonths(pp).ToShortDateString();
                }
                txtProlength.Text = pp.ToString();
                txtProsdate.Text = dr["joining_date"].ToString().Substring(0, 10);
                

                //txtProedate.Text = Convert.ToDateTime(dr["joining_date"]).AddMonths(pp).ToShortDateString();
            }
            dr.Close();
            cmd = new SqlCommand("select Leave_Type,Leaves_Allowed From EmployeeLeavesAllowed Where (leave_type = 8 or leave_type = 9 or leave_type=13) and Emp_ID=" + empid + " and leave_year=2016", conn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (Convert.ToInt16(dr["Leave_Type"]) == 8)
                {
                    txtAnnualLeave.Text = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 9)
                {
                    txtSickLeave.Text = dr["Leaves_Allowed"].ToString();
                }
                else if (Convert.ToInt16(dr["Leave_Type"]) == 13)
                {
                    txtHospLeave.Text = dr["Leaves_Allowed"].ToString();
                }

            }
            dr.Close();
            //cmd = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,CPF_Entitlement,Hourly_Rate from EmployeePayHistory where Emp_ID=" + empid +" order by id desc", conn);
            cmd = new SqlCommand("select wdays_per_week,convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), payrate)) as payrate,Pay_Frequency,OT1Rate,OT2Rate,CPF_Entitlement,Hourly_Rate,Daily_Rate from EmployeePayHistory where Emp_ID=" + empid + " order by id desc", conn);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                ot1rate = Convert.ToDouble(dr["OT1Rate"]);
                ot2rate = Convert.ToDouble(dr["OT2Rate"]);
                hrate = Convert.ToDouble(dr["Hourly_Rate"]);
                drate = Convert.ToDouble(dr["Daily_Rate"]);
                if (dr["payrate"].ToString().Length == 0)
                {
                    mrate = 0;
                }
                else
                {
                    mrate = Convert.ToDouble(dr["payrate"].ToString());
                }


                ViewState["ot1rate"] = ot1rate.ToString();
                ViewState["ot2rate"] = ot2rate.ToString();
                ViewState["hrate"] = hrate.ToString();
                ViewState["drate"] = drate.ToString();
                ViewState["mrate"] = mrate.ToString();
                ViewState["wrate"] = null;
                ViewState["frate"] = null;

                

                //txtSalary.Text = dr["payrate"].ToString();
                double d = (ot1rate * Convert.ToDouble(dr["Hourly_Rate"]));
                txtOTrate.Text = d.ToString("#0.00");
                txtWorkDays.Text = dr["wdays_per_week"].ToString();
                Double rday = 7 - Convert.ToDouble(dr["wdays_per_week"]);

                if (rday == 2)
                {
                    dayDatails.Text = "Saturday & Sunday";
                }
                else if (rday == 1.5)
                {
                    dayDatails.Text = "1/2day Saturday & Sunday";
                }
                else
                {
                    dayDatails.Text = "Sunday";
                }
                txtRestDay.Text = rday.ToString();


                if (dr["CPF_Entitlement"].ToString() == "Y")
                {
                    ckCpf.Checked = true;
                }
                else
                {
                    ckCpf.Checked = false;
                }
                txtOtot.Text = dr["OT1Rate"].ToString();


                if (dr["Pay_Frequency"].ToString() == "M")
                {
                    ckCpf.Checked = true;
                }
                //------------------
                if (pf == "M")
                {
                    rWeekly.Checked = false;
                    rHourly.Checked = false;
                    rDaily.Checked = false;
                    rForntnightly.Checked = false;
                    rMonthly.Checked = true;

                    rHourlyot.Checked = false;
                    rDailyot.Checked = false;
                    rWeeklyot.Checked = false;
                    rFortnightlyot.Checked = false;
                    rMonthlyot.Checked = true;
                    txtsal_period_details.Text = "( per Monthly)";
                    txtSalary.Text = mrate.ToString();
                }
                else if(pf == "D")
                {
                    rWeekly.Checked = false;
                    rHourly.Checked = false;
                    rDaily.Checked = true;
                    rForntnightly.Checked = false;
                    rMonthly.Checked = false;

                    rHourlyot.Checked = false;
                    rDailyot.Checked = false;
                    rWeeklyot.Checked = true;
                    rFortnightlyot.Checked = false;
                    rMonthlyot.Checked = false;
                    txtsal_period_details.Text = "( per Daily)";
                    txtSalary.Text = drate.ToString();
                }
                else if (pf == "H")
                {
                    rWeekly.Checked = false;
                    rHourly.Checked = true;
                    rDaily.Checked = false;
                    rForntnightly.Checked = false;
                    rMonthly.Checked = false;

                    rHourlyot.Checked = false;
                    rDailyot.Checked = true;
                    rWeeklyot.Checked = false;
                    rFortnightlyot.Checked = false;
                    rMonthlyot.Checked = false;
                    txtsal_period_details.Text = "( per Hourly)";
                    txtSalary.Text = hrate.ToString();
                }

                //-----------------
            }
            //---
            dr.Close();
            cmd = new SqlCommand("select Designation from designation where id=" + did, conn);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtJobTittle.Text = dr["Designation"].ToString();


            }
            dr.Close();
            //------------check in Ket_Manual, record exists or not
            cmd = new SqlCommand("select * from Ket_Manual where emp_code=" + ecode, conn);
            dr = cmd.ExecuteReader();
            int titleid = 0;
            int catid = 0;
            string title = "";
            string notice_period = "";
            DateTime  issuedate;
            if (dr.Read())
            {
                if(dr["issue_date"] == null)
                {
                    issue_date.Text = "";
                }
                else
                {
                    issuedate = Convert.ToDateTime(dr["issue_date"]);
                    issue_date.Text = issuedate.ToString("dd/MM/yyyy");
                }

               

                
                printbutton.Enabled = true;
                titleid = Convert.ToInt32(dr["job_title_id"]);
                // txtComponents.Text  = dr["osrc_amount"].ToString();
                //title = dr["cat_title"].ToString();
                rHourlyot.Checked = false;
                rDailyot.Checked = false;
                rWeeklyot.Checked = false;
                rFortnightlyot.Checked = false;
                rMonthlyot.Checked = false;

                //rHourly.Checked = false;
                //rDaily.Checked = false;
                //rWeekly.Checked = false;
                //rForntnightly.Checked = false;
                //rMonthly.Checked = false;
                if (dr["notice_period"].ToString() != null)
                {
                    txtNoticeperiod.Text = dr["notice_period"].ToString();
                }
                

                if (dr["ot_pay_period"].ToString() == "H")
                {
                    rHourlyot.Checked = true;
                }
                else if (dr["ot_pay_period"].ToString() == "D")
                {
                    rDailyot.Checked = true;
                }
                else if (dr["ot_pay_period"].ToString() == "W")
                {
                    rWeeklyot.Checked = true;
                }
                else if (dr["ot_pay_period"].ToString() == "F")
                {
                    rFortnightlyot.Checked = true;
                }
                else if (dr["ot_pay_period"].ToString() == "M")
                {
                    rMonthlyot.Checked = true;
                }

                //-----------new added
                if (dr["salary_period"].ToString() == "W")
                {
                    
                    txtSalary.Text = "";
                    rWeekly.Checked = true;
                    rMonthly.Checked = false;
                    rHourly.Checked = false;
                    rForntnightly.Checked = false;
                    rDaily.Checked = false;

                    txt_weekly_fortnightly.Visible = true;
                    txt_weekly_fortnightly.Text = dr["payrate"].ToString();
                    txtsal_period_details.Text ="( per Weekly)";
                    wrate = Convert.ToDouble(dr["payrate"]);
                    ViewState["wrate"] = Convert.ToDouble(dr["payrate"]);

                }
                else if (dr["salary_period"].ToString() == "F")
                {
                    txtSalary.Text = "";
                    rWeekly.Checked = false;
                    rMonthly.Checked = false;
                    rHourly.Checked = false;
                    rForntnightly.Checked = true;
                    rDaily.Checked = false;

                    txt_weekly_fortnightly.Visible = true;
                    txt_weekly_fortnightly.Text = dr["payrate"].ToString();
                    txtsal_period_details.Text = "( per Fortnightly)";
                    frate = Convert.ToDouble(dr["payrate"]);
                    ViewState["frate"] = Convert.ToDouble(dr["payrate"]);
                }
                else if (dr["salary_period"].ToString() == "H")
                {
                    txtSalary.Text = hrate.ToString ();
                    rWeekly.Checked = false;
                    rMonthly.Checked = false;
                    rHourly.Checked = true;
                    rForntnightly.Checked =false;
                    rDaily.Checked = false;

                    txt_weekly_fortnightly.Visible = false;
                    txtsal_period_details.Text = "( per Hourly)";
                }
                else if (dr["salary_period"].ToString() == "D")
                {
                    txtSalary.Text = drate.ToString ();
                    rWeekly.Checked = false;
                    rMonthly.Checked = false;
                    rHourly.Checked = false;
                    rForntnightly.Checked = false;
                    rDaily.Checked = true;

                    txt_weekly_fortnightly.Visible = false;
                    txtsal_period_details.Text = "( per Daily)";
                }
                else if (dr["salary_period"].ToString() == "M")
                {
                    txtSalary.Text = mrate.ToString ();
                    rWeekly.Checked = false;
                    rMonthly.Checked = true;
                    rHourly.Checked = false;
                    rForntnightly.Checked = false;
                    rDaily.Checked = false;

                    txt_weekly_fortnightly.Visible = false;
                    txtsal_period_details.Text = "( per Monthly)";
                }
                //-------------------------------

                //if (dr["pay_frequency"].ToString() == "F")
                //{
                //    FullTime.Checked = true;
                //}
                //else
                //{
                //    PartTime.Checked = true;
                //}

                txtOthertypeLeave.Text = dr["other_type_of_leaves"].ToString();
                ddSalarydate.SelectedValue = dr["date_of_payment"].ToString();
                ddOTdate.SelectedValue = dr["date_of_ot"].ToString();
                txtOtherMedical_benefit.Text = dr["other_medical_benefit"].ToString();
                txtdet_working_hrs.Text = dr["working_hrs_details"].ToString();
                if (dr["job_duties"].ToString() == "NIL")
                {
                    txtJobTittle.Text = "";
                }
                else
                {
                    txtJobTittle.Text = dr["job_duties"].ToString();
                }
                if ((dr["other_salary_related_comp"].ToString() == "0"))
                {
                    ddAllowancetypes.SelectedIndex = -1;
                }
                else
                {
                    ddAllowancetypes.SelectedValue = dr["other_salary_related_comp"].ToString();
                    // ddcomponents.SelectedValue = dr["other_salary_related_comp"].ToString();
                }
                if ((dr["paid_medical_exam_fee"].ToString() == "Y"))
                {
                    txtPaidmefee.Checked = true;
                }
                else
                {
                    txtPaidmefee.Checked = false;
                }
                if ((dr["duration_end_date"].ToString().Trim() == "Continue"))
                {
                    txtDurationend.Text = "Continue";
                    DDLduration_todate.SelectedIndex = 0;

                }
                else
                {
                    txtDurationend.Text = dr["duration_end_date"].ToString();
                    DDLduration_todate.SelectedIndex = 1;
                }
            }
            else
            {
                txtDurationend.Text = "Continue";
                DDLduration_todate.SelectedIndex = 0;
               // printbutton.Enabled = false;
                rMonthly.Checked = true;
                ddSalarydate.SelectedValue = "1";
                ddOTdate.SelectedValue = "1";
                // txtOtherMedical_benefits.Value = "NIL";
                //txtdet_working_hrs.Text = "";

            }
            //------- find job category
            dr.Close();
            if (titleid == 0)
            {
                // ddJobCategory.SelectedIndex = -1;
                // ddJobTitle.SelectedIndex = -1;
                ddJobCategory.Items.Insert(0, "Select");
                ddJobCategory.SelectedIndex = -1;
                //  ddJobCategory.Text = "Select..";
                ddJobTitle.Text = "Select..";
            }
            else
            {
                cmd = new SqlCommand("select * from  JobTitle where id=" + titleid, conn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    catid = Convert.ToInt32(dr["cat_id"]);
                    // title = dr["cat_title"].ToString();
                    //  int index = ddJobCategory.FindItemIndexByValue(catid.ToString());
                    // ddJobCategory.SelectedIndex = index;
                    ddJobCategory.SelectedValue = catid.ToString();

                    DataSet ds_jobtitle = new DataSet();
                    ds_jobtitle = getDataSet("SELECT [id], [cat_title] FROM [JobTitle] where cat_id=" + ddJobCategory.SelectedValue + " order by cat_title");
                    ddJobTitle.DataSource = ds_jobtitle.Tables[0];
                    ddJobTitle.DataTextField = ds_jobtitle.Tables[0].Columns["cat_title"].ColumnName.ToString();
                    ddJobTitle.DataValueField = ds_jobtitle.Tables[0].Columns["id"].ColumnName.ToString();
                    ddJobTitle.DataBind();
                    ddJobTitle.SelectedValue = titleid.ToString();
                }
            }

            // ddJobCategory.SelectedValue = catid.ToString();

            // ddJobTitle.Text = title;


            //-------------------------------check for record in allowance
            dr.Close();
            // cmd = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + empid , conn);
            //dr = cmd.ExecuteReader();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("select * from fixed_allowance_deduction where allowance_deduction='A' and  emp_code=" + empid, conn);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {

                //=================

                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                // if (dtCurrentTable.Rows.Count > 0)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

                    //add new row to DataTable   
                    dtCurrentTable.Rows.Add(drCurrentRow);

                }

                //Rebind the Grid with the current data to reflect changes 

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // DataTable dt = (DataTable)ViewState["CurrentTable"];

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dtCurrentTable.Rows[i]["Column1"] = ds.Tables[0].Rows[i]["type_desc"].ToString();
                        dtCurrentTable.Rows[i]["Column2"] = ds.Tables[0].Rows[i]["amount"].ToString();

                    }
                    //-----------------
                    Gridview1.DataSource = dtCurrentTable;
                    Gridview1.DataBind();
                    ViewState["CurrentTable"] = dtCurrentTable;
                    SetPreviousData();

                }

            }
            //-------end of allowance fetch records

            //-------------------------------check for record in deduction
            dr.Close();
            // cmd = new SqlCommand("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + ecode, conn);
            DataSet ds2 = new DataSet();
            SqlDataAdapter da2 = new SqlDataAdapter("select * from fixed_allowance_deduction where allowance_deduction='D' and  emp_code=" + empid, conn);
            da2.Fill(ds2);
            if (ds2.Tables[0].Rows.Count > 0)
            {
                DataTable dtCurrentTable2 = (DataTable)ViewState["CurrentTable2"];
                DataRow drCurrentRow2 = null;
                // if (dtCurrentTable.Rows.Count > 0)
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    drCurrentRow2 = dtCurrentTable2.NewRow();
                    drCurrentRow2["RowNumber"] = dtCurrentTable2.Rows.Count + 1;

                    //add new row to DataTable   
                    dtCurrentTable2.Rows.Add(drCurrentRow2);

                }

                //Rebind the Grid with the current data to reflect changes   
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    // DataTable dt = (DataTable)ViewState["CurrentTable"];

                    for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                    {
                        dtCurrentTable2.Rows[i]["Column1"] = ds2.Tables[0].Rows[i]["type_desc"].ToString();
                        dtCurrentTable2.Rows[i]["Column2"] = ds2.Tables[0].Rows[i]["amount"].ToString();

                    }
                    //-----------------
                    Gridview2.DataSource = dtCurrentTable2;
                    Gridview2.DataBind();
                    ViewState["CurrentTable2"] = dtCurrentTable2;
                    SetPreviousData2();

                }
            }
            //-------end of deduction fetch records

            //-------------------------------check for record in other components
            dr.Close();
            DataSet ds3 = new DataSet();
            SqlDataAdapter da3 = new SqlDataAdapter("select * from fixed_allowance_deduction where allowance_deduction='O' and  emp_code=" + empid, conn);
            da3.Fill(ds3);
            if (ds3.Tables[0].Rows.Count > 0)
            {
                //=================

                DataTable dtCurrentTable3 = (DataTable)ViewState["CurrentTable3"];
                DataRow drCurrentRow3 = null;

                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    drCurrentRow3 = dtCurrentTable3.NewRow();
                    drCurrentRow3["RowNumber"] = dtCurrentTable3.Rows.Count + 1;

                    //add new row to DataTable   
                    dtCurrentTable3.Rows.Add(drCurrentRow3);

                }

                //Rebind the Grid with the current data to reflect changes  
                if (ds3.Tables[0].Rows.Count > 0)
                {
                    // DataTable dt = (DataTable)ViewState["CurrentTable"];

                    for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                    {
                        dtCurrentTable3.Rows[i]["Column1"] = ds3.Tables[0].Rows[i]["type_desc"].ToString();
                        dtCurrentTable3.Rows[i]["Column2"] = ds3.Tables[0].Rows[i]["amount"].ToString();

                    }
                    //-----------------
                    Gridview3.DataSource = dtCurrentTable3;
                    Gridview3.DataBind();
                    ViewState["CurrentTable3"] = dtCurrentTable3;
                    SetPreviousData3();

                }

            }

        }
        protected void ImageButton1_Click(object sender, EventArgs e)
        {

            if (Calendar2.Visible == false)
            {
                Calendar2.Visible = true;
                Calendar2.SelectedDate = DateTime.Now.Date;
            }
            else
            {
                Calendar2.Visible = false;
            }

        }
        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            issue_date.Text = Calendar2.SelectedDate.ToString("dd/MM/yyyy");
            Calendar2.Visible = false;
        }
    }


}