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
namespace SMEPayroll.employee
{
    public partial class PreviousYearWages : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            lblmessage.Text = "";
            if (!IsPostBack)
            {
                
            }

        }

        private DataSet EmployeeSet
        { 
            get
            {
                int compid = Utility.ToInteger(Session["Compid"]);
               // string sSQL = "SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , LYTotalOW,OWLastYear=CASE WHEN OWLastYear is NULL THEN YEAR(GETDATE()) ELSE OWLastYear End  FROM [employee] where termination_date is null and company_id=" + compid + " order by emp_name";
                //Senthil for Group Management
                string sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin")
                {
                    string sSQL = "SELECT time_card_no,(select deptname from department where id=E.dept_id)as Department,ic_pp_number,(select Designation from Designation where id=E.desig_id)as Designation, E.emp_type,E.time_card_no as TimeCardId,(select Nationality from nationality where id=E.nationality_id)as Nationality,(select trade from trade where id=E.Trade_id) as Trade,[emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , LYTotalOW,OWLastYear=CASE WHEN OWLastYear is NULL THEN YEAR(GETDATE())-1 ELSE OWLastYear End  FROM [employee] E where termination_date is null and company_id=" + compid + " order by emp_name";
                    ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                }
                else
                {
                    if (Utility.GetGroupStatus(compid) == 1)
                    {
                        string sSQL = "SELECT time_card_no,(select deptname from department where id=E.dept_id)as Department,ic_pp_number,(select Designation from Designation where id=E.desig_id)as Designation, E.emp_type,E.time_card_no as TimeCardId,(select Nationality from nationality where id=E.nationality_id)as Nationality,(select trade from trade where id=E.Trade_id) as Trade,[emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , LYTotalOW,OWLastYear=CASE WHEN OWLastYear is NULL THEN YEAR(GETDATE())-1 ELSE OWLastYear End  FROM [employee] E where termination_date is null and company_id=" + compid + " and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") order by emp_name";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                    }
                    else
                    {
                        string sSQL = "SELECT time_card_no,(select deptname from department where id=E.dept_id)as Department,ic_pp_number,(select Designation from Designation where id=E.desig_id)as Designation, E.emp_type,E.time_card_no as TimeCardId,(select Nationality from nationality where id=E.nationality_id)as Nationality,(select trade from trade where id=E.Trade_id) as Trade,[emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name' , LYTotalOW,OWLastYear=CASE WHEN OWLastYear is NULL THEN YEAR(GETDATE())-1 ELSE OWLastYear End  FROM [employee] E where termination_date is null and company_id=" + compid + " order by emp_name";
                        ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                    }

                }

                return ds;  
            }
        }
         
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.EmployeeSet;
        }

        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {   
            ds = EmployeeSet;
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                GridDataItem dtItem = e.Item as GridDataItem;
                string empid = dataItem.Cells[2].Text.ToString();
                string empdrpyear = dataItem.Cells[4].Text.ToString();



                TextBox txtbox = (TextBox)dataItem.FindControl("txtLYOW");
                DropDownList grddrop = (DropDownList)dataItem.FindControl("OWLastYear");
                grddrop.Items.FindByText(empdrpyear).Selected = true;
                
            }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAll")
            {
                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtLYOW");
                        DropDownList grddrop = (DropDownList)dataItem.FindControl("OWLastYear");
                        double LYTotalOW = Utility.ToDouble(txtbox.Text.ToString());
                        int OWLY = Utility.ToInteger(grddrop.SelectedItem.Value.ToString());
                        string sSQL1 = "Update Employee SET LYTotalOW = " + LYTotalOW + ",OWLastYear=" + OWLY + " WHERE emp_code = " + empcode;
                        int retval = DataAccess.ExecuteStoreProc(sSQL1);
                        if (retval > 0)
                        {
                            //lblmessage.Text = "Previous Year Wages Updated successfully";
                            ViewState["actionMessage"] = "Success|Previous Year Wages Updated successfully";

                            //lblmessage.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
        }
    }
}
