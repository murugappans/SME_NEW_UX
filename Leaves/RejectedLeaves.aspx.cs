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
namespace SMEPayroll.Leaves
{
    public partial class RejectedLeaves : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string varEmpCode = "";
        int compid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            string sgroupname = Utility.ToString(Session["GroupName"]);
            if (!IsPostBack)
            {
                compid = Utility.ToInteger(Session["Compid"]);
                varEmpCode = Session["EmpCode"].ToString();

                //Senthil for Group Management 
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Leaves for all") == true))
                {
                    if (varEmpCode != "1" && varEmpCode != "0")//if not super admin and not a user
                    {
                        DropDownList1.SelectedValue = varEmpCode;
                    }
                    cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                    string SQLQuery;
                    SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";
                    SqlDataSource1.SelectCommand = "select -1 emp_code, '-select--' [emp_name] UNION SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                    if (dr.Read())
                    {
                        if (Utility.ToInteger(dr[0].ToString()) > 0 || varEmpCode == "1")//
                        {
                            DropDownList1.Enabled = true;
                        }
                        else
                        {
                            if (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Leaves for all") == true)
                            {
                                DropDownList1.Enabled = true;
                            }
                            else
                            {
                                DropDownList1.Enabled = false;
                            }
                        }
                    }
                   
                }
                else
                {
                    if (Utility.GetGroupStatus(compid) == 1)
                    {
                        SqlDataSource1.SelectCommand = "select -1 emp_code, '-select--' [emp_name] UNION SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                    }
                    else
                    {
                        SqlDataSource1.SelectCommand = "select -1 emp_code, '-select--' [emp_name] UNION SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where emp_code in (select emp_code from employee where emp_supervisor='" + varEmpCode + "') OR emp_code='" + varEmpCode + "'";
                    }
                }
            }

        }
       
        protected void bindgrid(object sender, EventArgs e)
        {
            //murugan
            SqlDataSource2.SelectCommand = "SELECT [Path],[trx_id], [emp_id], [leave_type],b.type,a.remarks,convert(varchar(15),[start_date],103)'start_date', convert(varchar(15),[end_date],103)'end_date',timesession  as Session,paid_leaves,unpaid_leaves,(paid_leaves + unpaid_leaves)'sumLeaves', [status], convert(varchar(15),getdate(),103)'Application Date', [approver]  FROM [emp_leaves] a,leave_types b WHERE leave_type=b.id and ([emp_id] ="+ DropDownList1.SelectedValue.ToString() + ") and year(start_date)="+ cmbYear.SelectedValue.ToString()+" and ([status]='Rejected') Order by 5";
            RadGrid1.DataBind();
        }
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[2].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    //--murugan
                    // hl.NavigateUrl = "../" + "Documents" + "/" + Utility.ToInteger(Session["Compid"]) + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.NavigateUrl = hl.Text;
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
