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
    public partial class RejectedClaims : System.Web.UI.Page
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
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                compid = Utility.ToInteger(Session["Compid"]);
                varEmpCode = Session["EmpCode"].ToString();

               
                #region Emp dropdown
                DataSet ds_employee = new DataSet();
                //Senthil for Group Management
                sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all") == true))
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " order by emp_name");
                }
                else
                {
                    if (Utility.GetGroupStatus(compid) == 1)
                    {
                        
                        ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name"); //Grouping
                    }
                    else
                    {
                       // ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_clsupervisor='" + varEmpCode + "' AND emp_code='" + varEmpCode + "' and company_id=" + compid + " order by emp_name");
                        ds_employee = getDataSet("SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_clsupervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "' ");
                        
                    }
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
                        //DropDownList1.Enabled = false;
                    }
                }
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

            RadGrid1.DataBind();
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[12].Text).ToString();
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


    }
}
