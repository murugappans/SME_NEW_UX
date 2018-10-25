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

namespace SMEPayroll.Payroll
{
    public partial class ClaimsStatusExt : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string varEmpCode = "", sgroupname = "";
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.LCID = 2057;
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            imgbtnfetch.Click += new ImageClickEventHandler(imgbtnfetch_Click);
            radGridApproved.ClientSettings.Selecting.AllowRowSelect = true;
            radGridApproved.ItemDataBound += new GridItemEventHandler(radGridApproved_ItemDataBound);
            
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                compid = Utility.ToInteger(Session["Compid"]); 
                varEmpCode = Session["EmpCode"].ToString();
                //r
                #region Emp dropdown
                DataSet ds_employee = new DataSet();
                sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin" )
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " order by emp_name");
                }
                else
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_code='" + varEmpCode + "' and company_id=" + compid + " order by emp_name");
                }
                DropDownList1.DataSource = ds_employee.Tables[0];
                DropDownList1.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                DropDownList1.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                DropDownList1.DataBind();
                #endregion

               
                if ((string)varEmpCode != "0")//checking whether user is login in the table.
                {
                    DropDownList1.SelectedValue = varEmpCode;
                   
                }
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                string SQLQuery;
                SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                        //DropDownList1.Enabled = true;
                    }
                    else
                    {
                       // DropDownList1.Enabled = false;
                    }
                }
            }
        }

        void radGridApproved_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {

                string empcode = Convert.ToString(e.Item.Cells[3].Text).ToString();
                string navUrl = "";
                if (empcode != "&nbsp;" && empcode != "")
                {
                    if (e.Item.Cells.Count >= 11)
                    {
                        if (e.Item.Cells[11].Text != "&nbsp;" && e.Item.Cells[11].Text != "")
                        {
                            navUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + e.Item.Cells[11].Text.Trim();
                            if (e.Item.Cells[11].Text != "")
                            {
                                HyperLink hyp = new HyperLink();
                                hyp.Text = e.Item.Cells[11].Text;
                                hyp.NavigateUrl = navUrl.Trim();
                                e.Item.Cells[11].Controls.Add(hyp);
                            }
                            else
                            {
                                e.Item.Cells[11].Text = "No Document";
                            }
                            //img.Attributes.Add("onclick", "javascript:window.open('http://www.myshoppingCart.no/OpenShopPackageTracker.aspx?installationID=21140000023&orderNumber=" + ProductId.Value.ToString() & "'); return false;")

                        }

                    }
                }
            }
        }

        void imgbtnfetch_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
        }

        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void bindgrid(object sender, ImageClickEventArgs e)
        {

            RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[11].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    hl.NavigateUrl = "../" + "Documents" + "/" + Utility.ToInteger(Session["Compid"]) + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";
                }
                else
                {
                    hl.Text = "No Document";
                }
            }
        }

        protected void radGrid_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.Width = Unit.Percentage(100);
        }

        protected void radGridApproved_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int trasnId = 0;
                trasnId = Convert.ToInt32(e.Item.Cells[4].Text);

                string strDelete = "Delete from ClaimsExt where TransId =" + trasnId;
                strDelete = strDelete + ";" + " Delete from emp_additions where TransId =" + trasnId;

                int cnt = 0;
                try
                {
                    cnt = DataAccess.ExecuteNonQuery(strDelete, null);

                    if (cnt > 0)
                    {
                        Response.Write("<script language = 'Javascript'>alert('Record Delete Successfully.');</script>");
                        BindData();
                    }

                }catch(Exception ex)
                {
                    Response.Write("<script language = 'Javascript'>alert('Record Not Delete Successfully');</script>");
                }
            }
        }



        protected void radGrid_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = CreateDataSet();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    radGridApproved.DataSource = ds;
                    radGridApproved.Enabled = true;
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable.TableName = "Employee";
                    dataTable.Columns.Add("TransId");
                    dataTable.Columns.Add("ClaimStatus");
                    ds.Tables.Add(dataTable);
                    radGridApproved.DataSource = ds;
                    radGridApproved.Enabled = false;
                }
            }
        }

        protected void BindData()
        {
            DataSet ds = new DataSet();
            ds = CreateDataSet();
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    radGridApproved.DataSource = ds;
                    radGridApproved.DataBind();
                    radGridApproved.Enabled = true;
                }
                else
                {
                    DataTable dataTable = new DataTable();
                    dataTable.TableName = "Employee";
                    dataTable.Columns.Add("TransId");
                    dataTable.Columns.Add("ClaimStatus");
                    ds.Tables.Add(dataTable);                   
                    radGridApproved.DataSource = ds;
                    radGridApproved.DataBind();
                    radGridApproved.Enabled = false;
                }
            }
            
        }

        private DataSet CreateDataSet()
        {

            string sqlApprovedClaims = @"SELECT " +
                "ic_pp_number,(select Designation from Designation where id=b.desig_id)as Designation, b.emp_type,b.time_card_no as TimeCardId,(select Nationality from nationality where id=b.nationality_id)as Nationality,(select trade from trade where id=b.Trade_id) as Trade," +
                " e.[trx_id],  a.[id],a.[desc] As [Claim Type] ,e.[trx_amount] As Amount,convert(char(2),datepart(mm,e.[trx_period]))+'/'+convert(char(4),datepart(yy,e.[trx_period])) As Period, e.[emp_code]," +
                " b.emp_name+' '+b.emp_lname 'Employee Name',(select DeptName from department where id=b.dept_id) As Department,e.remarks As Remarks,isnull(e.recpath,'') 'Attached Document',status,claimstatus As ClaimStatus,e.TransId FROM [emp_additions] e, additions_types a, employee b " +
                " WHERE e.trx_type = a.id and e.emp_code = b.emp_code and (claimstatus='Open'or claimstatus='Approved'Or claimstatus='Rejected') and e.emp_code=" + DropDownList1.SelectedValue + " and year(trx_period)=" + cmbYear.SelectedValue + " and upper(a.optionselection) like '%CLAIM%'";

            DataSet datasetemp = new DataSet();

            datasetemp = DataAccess.FetchRS(CommandType.Text, sqlApprovedClaims, null);

            //Employee Name Claim Type Department Amount Period ClaimStatus Remarks Nationality Trade Pass Type Designation IC/FIN Number 
            //Attached Document ,TransId


            DataSet dataset = new DataSet();

            if (datasetemp.Tables.Count > 0)
            {
                if (datasetemp.Tables[0].Rows.Count > 0)
                {
                    ///Employeess
                    DataTable dataTable = new DataTable();
                    dataTable.TableName = "Employee";
                    dataTable.Columns.Add("TransId");
                    dataTable.Columns.Add("ClaimStatus");

                    string[] columnname = new string[2];
                    columnname[0] = "TransId";
                    columnname[1] = "ClaimStatus";
                    

                    DataTable distinctTable = new DataTable();
                    if (datasetemp.Tables.Count > 0)
                    {
                        distinctTable = datasetemp.Tables[0].DefaultView.ToTable(true, columnname);
                    }

                    foreach (DataRow dr in distinctTable.Rows)
                    {
                        dataTable.Rows.Add(new object[] { dr["TransId"].ToString(), dr["ClaimStatus"].ToString()});
                    }

                    DataColumn[] keys = new DataColumn[1];
                    keys[0] = dataTable.Columns["TransId"];
                    dataTable.PrimaryKey = keys;
                    dataset.Tables.Add(dataTable);

                    ////Claim Type Department Amount Period Attached Document 

                    ////Actual Claims Data

                    dataTable = new DataTable();
                    dataTable.TableName = "ClaimsDetail";
                    dataTable.Columns.Add("Employee Name");
                    dataTable.Columns.Add("emp_code");
                    dataTable.Columns.Add("trx_id");
                    dataTable.Columns.Add("Claim Type");
                    dataTable.Columns.Add("Department");
                    dataTable.Columns.Add("Amount");
                    dataTable.Columns.Add("Period");
                    dataTable.Columns.Add("ClaimStatus");
                    dataTable.Columns.Add("Remarks");
                    dataTable.Columns.Add("Attached Document");
                    dataTable.Columns.Add("TransId");

                    ////Claim Type Department Amount Period Attached Document 
                    foreach (DataRow dr in datasetemp.Tables[0].Rows)
                    {
                        dataTable.Rows.Add(new object[] { dr["Employee Name"].ToString(), dr["emp_code"].ToString(), dr["trx_id"].ToString(), dr["Claim Type"].ToString(), dr["Department"].ToString(), dr["Amount"].ToString(), dr["Period"].ToString(), dr["ClaimStatus"].ToString(), dr["Remarks"].ToString(), dr["Attached Document"].ToString(), dr["TransId"].ToString() });
                    }
                    keys = new DataColumn[1];
                    keys[0] = dataTable.Columns["trx_id"];
                    dataTable.PrimaryKey = keys;
                    dataset.Tables.Add(dataTable);

                    DataRelation CustomersOrdersRelation =
                            new DataRelation("EmployeeClaims", dataset.Tables["Employee"].Columns["TransId"], dataset.Tables["ClaimsDetail"].Columns["TransId"]);
                    ///DataRelation OrdersEmployees = new DataRelation("OrdersEmployees", dataset.Tables["Orders"].Columns["EmployeeID"], dataset.Tables["Employees"].Columns["EmployeeID"]);
                    dataset.Relations.Add(CustomersOrdersRelation);
                }
            }

            return dataset;
        }
    }
}
