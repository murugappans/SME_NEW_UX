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

namespace SMEPayroll.Management
{
    public partial class ManageIR8aInfo : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int s = 0, varCompid;
        protected string sMsg = "";
        protected string sQuery = null;
        DataSet sqlEmpIr8aDs = null;
        #region Dataset command
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varCompid = Utility.ToInteger(Session["Compid"]);
            sMsg = Utility.ToString(Request.QueryString["msg"]);
            if (!IsPostBack)
            {
                Session["s"] = 0;
            }

        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridDataItem && e.CommandName != "Delete")
            {
                GridDataItem dataItem = (GridDataItem)e.Item;
                //ImageButton btn = (ImageButton)dataItem["editHyperlink"].FindControl("btnedit");
                Response.Redirect("Ir8aSetup.aspx?empcode=" + dataItem["EmpCode"].Text + "&year=" + cmbYear.SelectedValue);
            }
        }

        protected void RadGrid1_PreRender(object sender, System.EventArgs e)
        {
            if (this.IsPostBack)
            {

                //RadGrid1.ClientSettings.ActiveRowData = String.Format("{0},{1}", RadGrid1.MasterTableView.ClientID, RadGrid1.Items[0].RowIndex);
                this.RadGrid1.DataSource = this.EmployeeDetails;
                this.RadGrid1.MasterTableView.Rebind();
            }
        }

        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet EmployeeDetails
        {
            get
            {
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", varCompid);
                parms1[1] = new SqlParameter("@show", "0");
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                //string sSQL = "sp_GetEmployees";
                //ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);

                //string sSQL = "SELECT EMP_CODE,emp_name + ' ' + emp_lname as emp_name, (SELECT DeptName FROM department WHERE id= dept_id   ) Department,ic_pp_number,emp_type,empgroupname from employee e INNER JOIN  EMPLOYEE_IR8A I ON  E.EMP_CODE = I.EMP_ID INNER JOIN  EMP_GROUP ON EMP_GROUP_ID =ID WHERE I.IR8A_YEAR=" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue) - Utility.ToInteger(1)) + "  order by EMP_NAME ";
                string sSQL = "SELECT EMP_CODE,emp_name + ' ' + emp_lname as emp_name, (SELECT DeptName FROM department WHERE id= dept_id   ) Department,ic_pp_number,emp_type,empgroupname from employee e INNER JOIN  EMPLOYEE_IR8A I ON  E.EMP_CODE = I.EMP_ID INNER JOIN  EMP_GROUP ON EMP_GROUP_ID =ID WHERE I.IR8A_YEAR=" + Utility.ToInteger(cmbYear.SelectedValue) + "  and (e.termination_date is null or year(e.termination_date) >=" + Utility.ToInteger(cmbYear.SelectedValue) + ") AND year(e.JOINING_DATE) <=" + Utility.ToInteger(cmbYear.SelectedValue) + " and e.company_id= " + varCompid + " group by emp_code,emp_name , emp_lname , ic_pp_number,emp_type,empgroupname,dept_id order by EMP_NAME ";
                
                ds = GetDataSet(sSQL);
                return ds;
            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            this.RadGrid1.DataSource = this.EmployeeDetails;
        }
        #endregion Dataset command

        #region Delete command

        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                GridEditableItem editedItem = e.Item as GridEditableItem;
                string EmpCode = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["emp_code"]);
                string sSQL = "sp_emp_delete";
                SqlParameter[] parms = new SqlParameter[1];
                parms[0] = new SqlParameter("@emp_code", Utility.ToInteger(EmpCode));
                string sql = "select a.username,b.company_code from employee a,company b where a.company_id=b.company_id and emp_code=" + EmpCode;
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                if (dr.Read())
                {
                    string username = dr[0].ToString();
                    string usernam1 = dr[1].ToString() + "Admin";
                    if (username == usernam1)
                    {
                        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>This superadmin employee record cannot be deleted. "));
                    }
                    else
                    {
                        int i = DataAccess.ExecuteStoreProc(sSQL, parms);
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrMsg = ex.Message;
                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the User. Reason:</font> " + ErrMsg));
                e.Canceled = true;
            }
        }
        #endregion Delete command

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           sQuery = "select * from employee_ir8a where ir8a_year='" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue) - Utility.ToInteger(1)) + "'";
           
            sqlEmpIr8aDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);

            int count = sqlEmpIr8aDs.Tables[0].Rows.Count;
            if (e.Item is GridDataItem)
            {
               // GridCommandItem commandItem = (GridCommandItem)e.Item;
                if (count > 0)
                {
                    ((Button)e.Item.FindControl("btnIr8a")).Enabled = true; 
                    ((Button)e.Item.FindControl("btnIr8aApepndixA")).Enabled = true;  
                    ((Button)e.Item.FindControl("btnIr8aApepndixB")).Enabled = true;  
                }
                else
                {
                    ((Button)e.Item.FindControl("btnIr8a")).Enabled = false;
                    ((Button)e.Item.FindControl("btnIr8aApepndixA")).Enabled = false;
                    ((Button)e.Item.FindControl("btnIr8aApepndixB")).Enabled = false;  
                }

            }
           
        
        }
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            
            RadGrid1.DataBind();
        }




       

        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

            if (Utility.ToInteger(Session["s"]) == 1)
            {
                //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
                //ds = GetDataSet(sSQL);
                RadGrid1.CurrentPageIndex = e.NewPageIndex;
                DataSet ds = new DataSet();
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@company_id", varCompid);
                parms1[1] = new SqlParameter("@show", "1");
                parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                string sSQL = "sp_GetEmployees";
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }

        }
        public void ExportToExcel(DataSet dSet, int TableIndex, HttpResponse Response, string FileName)
        {
            Response.Clear();
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=" + FileName + ".xls");
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
            GridView gv = new GridView();
            gv.DataSource = dSet.Tables[TableIndex];
            gv.DataBind();
            gv.RenderControl(hw);
            Response.Write(sw.ToString());
            Response.End();
        }


        protected void btnallemp_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string ssqlexcel = "sp_empall_details";
            SqlParameter[] parms = new SqlParameter[1];
            int i = 0;
            parms[i] = new SqlParameter("@companyid", Utility.ToInteger(varCompid));
            ds = DataAccess.FetchRS(CommandType.StoredProcedure, ssqlexcel, parms);
            Employee em = new Employee();


        }

        protected void btnUpdateIras_Click(object sender, EventArgs e)
        {
            sQuery = "select emp_code from employee where ( termination_date is  null  or year(termination_date) >= " + Utility.ToInteger(cmbYear.SelectedValue) + " ) AND year(JOINING_DATE) <=" + Utility.ToInteger(cmbYear.SelectedValue) + " and company_id=" + varCompid;

            DataSet sqlEmpDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            DataSet sqlNewEmpDs;

            sQuery = "select * from employee_ir8a where ir8a_year=" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue) - Utility.ToInteger(1));
            sqlEmpIr8aDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            sQuery = "select * from employee_ir8a where ir8a_year=" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue));
            sqlNewEmpDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            DataTable dt = sqlEmpIr8aDs.Tables[0];
            DataTable dtExisting = sqlNewEmpDs.Tables[0];
            DataRow[] dr ;
          
            for (int i = 0; i < sqlEmpDs.Tables[0].Rows.Count; i++)
            {
                DataRow[] drRows = dt.Select("emp_id=" + sqlEmpDs.Tables[0].Rows[i][0].ToString().Trim());
                DataRow[] drERows = dtExisting.Select("emp_id=" + sqlEmpDs.Tables[0].Rows[i][0].ToString().Trim());
                if(drERows.Length == 0)
                if (drRows.Length > 0)
                {
                    sQuery = "  Insert into employee_ir8a(ir8a_year,emp_id,addr_type) values('" + cmbYear.SelectedValue + "'," + sqlEmpDs.Tables[0].Rows[i][0].ToString() + ",'" + drRows[0].ItemArray[3].ToString() + "' ) ";
                    DataAccess.ExecuteNonQuery(sQuery, null);
                }
                else
                {
                    sQuery = "  Insert into employee_ir8a(ir8a_year,emp_id,addr_type) values('" + cmbYear.SelectedValue + "'," + sqlEmpDs.Tables[0].Rows[i][0].ToString() + ",'N' ) ";
                    DataAccess.ExecuteNonQuery(sQuery, null);
                }
            }

        }


    }
}

