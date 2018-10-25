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
    public partial class FormulaAssign : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

       string compid, year, month, group,encashtype;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToString(Request.QueryString["compid"]);
            year = Utility.ToString(Request.QueryString["year"]);
            month = Utility.ToString(Request.QueryString["month"]);
             group = Utility.ToString(Request.QueryString["group"]);
             encashtype = Utility.ToString(Request.QueryString["encashtype"]);


             if (encashtype == "1")
             {
                 isFormulaAssigned();
             }
             else if (encashtype == "2")
             {
                 Assignformulafortranfer();
             }
        }

        private void CreateTable()
        {
            try
            {

                DataRow dr;
                DataTable dt = new DataTable();
                dt.Columns.Add("emp_code");
                dt.Columns.Add("emp_name");

                dt.Columns.Add("Formula");
                dr = dt.NewRow();
                dr["emp_code"] = "306";
                dr["emp_name"] = "Kumar";
                dr["Formula"] = 1;
                dt.Rows.Add(dr);
                this.RadGrid1.DataSource = dt;

            }
            catch (Exception ex) { }
        }


        private void isFormulaAssigned()
        {
            DataSet ds_Assign = new DataSet();


            string strEmpSelect = @" select * from( Select  e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
                           " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
                           " where e.Company_Id = " + compid + " and e.termination_date is not null and year(e.termination_date )= " + year + " and month(e.termination_date)=" + month + " and el.leave_year="
                           + year
                           + " and Leave_Type=8 AND termination_date is  not null and e.emp_group_id= "
                           + group + ")A where not EXISTS (select * from Encashment b where a.emp_code=b.EmpCode)";



            ds_Assign = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);

            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("emp_code");
            dt.Columns.Add("emp_name");

            dt.Columns.Add("Formula");
            dt.Columns.Add("Amount");
           





            for (int i = 0; i < ds_Assign.Tables[0].Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["emp_code"] = ds_Assign.Tables[0].Rows[i]["emp_code"].ToString();
                dr["emp_name"] = ds_Assign.Tables[0].Rows[i]["emp_name"].ToString();

                dr["Formula"] = 1;
                dr["Amount"] = 0;
                dt.Rows.Add(dr);
            }
            this.RadGrid1.DataSource = dt;
        }

        private void Assignformulafortranfer()
        {
            DataSet ds_Assign = new DataSet();


            string strEmpSelect = @" select * from( Select  e.emp_code, e.emp_name ,el.leave_year,el.Leaves_Allowed from employee as e " +
                           " INNER JOIN EmployeeLeavesAllowed el on e.emp_code=el.emp_id " +
                           " where e.Company_Id = " + compid + " and e.termination_date is  null  and el.leave_year="
                           + year
                           + " and Leave_Type=8 AND termination_date is   null and e.emp_group_id= "
                           + group + ")A where not EXISTS (select * from Encashment b where a.emp_code=b.EmpCode)";



            ds_Assign = DataAccess.FetchRS(CommandType.Text, strEmpSelect, null);

            DataRow dr;
            DataTable dt = new DataTable();
            dt.Columns.Add("emp_code");
            dt.Columns.Add("emp_name");

            dt.Columns.Add("Formula");
            dt.Columns.Add("Amount");






            for (int i = 0; i < ds_Assign.Tables[0].Rows.Count; i++)
            {
                dr = dt.NewRow();
                dr["emp_code"] = ds_Assign.Tables[0].Rows[i]["emp_code"].ToString();
                dr["emp_name"] = ds_Assign.Tables[0].Rows[i]["emp_name"].ToString();

                dr["Formula"] = 1;
                dr["Amount"] = 0;
                dt.Rows.Add(dr);
            }
            this.RadGrid1.DataSource = dt;
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {




            int i = 0;
            SqlParameter[] parms = new SqlParameter[7];
            string strEmployee = "0";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                   // CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                   // if (chkBox.Checked == true)
                   //{


                    Label labelId = (Label)dataItem.FindControl("emp_code");


                    strEmployee = labelId.Text.ToString().Trim();



                    DropDownList ddl = (DropDownList)dataItem["Formula"].FindControl("FormulaDRP");

                    TextBox txtbox = (TextBox)dataItem.FindControl("Amount");

                    if (txtbox.Text == "")
                        txtbox.Text = "0";
                    double noofLeaveEncsh = Convert.ToDouble(txtbox.Text);

                    if (noofLeaveEncsh == null)
                    {
                        noofLeaveEncsh = 0.0;
                    }

                    if (strEmployee.Length > 0 && strEmployee != "0")
                    {
                        string strActionMsg = "";
                        parms[0] = new SqlParameter("@Company_ID", Utility.ToInteger(4));
                        parms[1] = new SqlParameter("@TypeID", encashtype);
                        parms[2] = new SqlParameter("@FormulaID", ddl.SelectedValue.ToString());
                        parms[3] = new SqlParameter("@Remarks", Utility.ToString(""));
                        parms[4] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));

                        int Actioninsert = 0;





                        parms[5] = new SqlParameter("@Action", Actioninsert.ToString());

                        parms[6] = new SqlParameter("@retval", SqlDbType.Int);
                        parms[6].Direction = ParameterDirection.Output;
                        string sSQL = "sp_Encash_Assigned";
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);


                        if (noofLeaveEncsh > 0)
                        {
                            string sql = "update Encashment set FixedAmount=" + noofLeaveEncsh + " where EmpCode=" + strEmployee + " and FormulaType=" + ddl.SelectedValue.ToString() + " and EncashType=" + encashtype;
                            DataAccess.ExecuteStoreProc(sql);
                        }

                        }
                       

                    }
                }
                        if (encashtype == "1")
                        {
                            Response.Redirect("../Leaves/LeaveEncash_ByTermination.aspx");
                        }
                        else
                        {
                            Response.Redirect("../Leaves/LeaveEncash_ByTransfer.aspx");
                        }

            }

        }
    }

