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
using System.Text.RegularExpressions;
using System.Threading;

namespace IRAS
{
    public partial class Manageir21info : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                cmbYear.Enabled = false;
                cmbYear.Items.FindByValue(HttpContext.Current.Session["IR8AYEAR"].ToString()).Selected = true;
            }
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
                Response.Redirect("ir21page.aspx?empcode=" + dataItem["EmpCode"].Text + "&year=" + cmbYear.SelectedValue + "&name=" + dataItem["emp_name"].Text);
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
                //SqlParameter[] parms1 = new SqlParameter[4];
                //parms1[0] = new SqlParameter("@company_id", varCompid);
                //parms1[1] = new SqlParameter("@show", "2");
                //parms1[2] = new SqlParameter("@UserID", Convert.ToInt16(Session["EmpCode"]));
                //parms1[3] = new SqlParameter("@LoginYear", Convert.ToInt16(Session["IR8AYEAR"]) + 1);

                ////string sSQL = "sp_GetEmployees";
                //string sSQL = "sp_GetEmployees_IRAS";
                string sSQL = "SELECT (select Nationality from nationality where id=nationality_id) as Nationality,(select trade from trade where id=Trade_id) as Trade,Pay_supervisor, emp_code, emp_name + ' ' + emp_lname AS emp_name,(SELECT DeptName FROM department WHERE id= dept_id) Department, time_card_no, empcpftype, emp_alias, emp_type, ic_pp_number, wp_exp_date,pr_date, address, pay_frequency, payrate, postal_code, phone, hand_phone, email,time_card_no, sex, marital_status, place_of_birth, date_of_birth, education,income_taxid, termination_reason, giro_bank, giro_code, giro_branch, giro_acct_number,joining_date, probation_period, confirmation_date, termination_date, cpf_entitlement,cpf_employer, cpf_employee, employee_cpf_acct, employer_cpf_acct, emp_supervisor,ot_entitlement, payment_mode, fw_code, fw_levy, sdf_required, cdac_fund, mbmf_fund,sinda_fund, ecf_fund, cchest_fund, email_payslip, wh_tax_pct, wh_tax_amt, remarks,images, Insurance_number, insurance_expiry, CSOC_number, CSOC_expiry, passport,passport_expiry, nationality_id, country_id, religion_id, race_id, desig_id, dept_id,emp_group_id, (SELECT empgroupname FROM emp_group WHERE [id]= emp_group_id) empgroupname, (SELECT Designation FROM Designation WHERE id= desig_id) Designation From Employee  Where Termination_Date is not null  And year(termination_date)="+ (Convert.ToInt16(Session["IR8AYEAR"])+1) +" and Company_ID="+ varCompid +" Order By Emp_Name";
                //ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                               
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
            //sQuery = "select * from employee_ir8a where ir8a_year='" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue) - Utility.ToInteger(1)) + "'";

            sQuery = "select * from employee_ir8a where ir8a_year='" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue)) + "'";

            sqlEmpIr8aDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);

            int count = sqlEmpIr8aDs.Tables[0].Rows.Count;
            //if (e.Item is GridDataItem)
            //{
            //    // GridCommandItem commandItem = (GridCommandItem)e.Item;
            //    if (count > 0)
            //    {
            //        ((Button)e.Item.FindControl("btnIr8a")).Enabled = true;
            //        ((Button)e.Item.FindControl("btnIr8aApepndixA")).Enabled = true;
            //        ((Button)e.Item.FindControl("btnIr8aApepndixB")).Enabled = true;
            //    }
            //    else
            //    {
            //        ((Button)e.Item.FindControl("btnIr8a")).Enabled = false;
            //        ((Button)e.Item.FindControl("btnIr8aApepndixA")).Enabled = false;
            //        ((Button)e.Item.FindControl("btnIr8aApepndixB")).Enabled = false;
            //    }

            //}


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

        }

        public void OnConfirm(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];
            if (confirmValue == "Yes")
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
            }
        }

        protected void checkboxclick(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (this.addressUpdate.Checked == true)
            {
                if (confirmValue == "Yes")
                {
                    this.addressUpdate.Checked = true;
                    this.cmbaddress.Enabled = true;
                }
                else
                {
                    this.cmbaddress.Enabled = false;
                    this.addressUpdate.Checked = false;
                }
            }


        }

        protected void btnUpdateIras_Click(object sender, EventArgs e)
        {

            string sql = @"select distinct 
     EMP_CODE AS EMPCODE,
    '1' AS RecordType, CASE EMP_REF_NO            
                                         WHEN '1' THEN '1'            
                                         WHEN '2' THEN '2'            
                                         WHEN '3' THEN '3'            
                                         WHEN '4' THEN '4'            
                                         WHEN '5' THEN '5'            
                                         WHEN '6' THEN '6'            
                                       END AS IDType,            
    --emp_type,            
                    IC_PP_NUMBER AS IDNo, EMP_NAME AS NameLine1, EMP_LNAME AS NameLine2, EIR.[ADDR_TYPE] AS AddressType,            
                    CASE [ADDR_TYPE]            
                      WHEN 'L' THEN BLOCK_NO            
                      ELSE ''            
                    END AS BlockNo, CASE [ADDR_TYPE]            
                                      WHEN 'L' THEN STREET_NAME            
                                      ELSE ''            
                                    END AS StName, CASE [ADDR_TYPE]            
                                                     WHEN 'L' THEN LEVEL_NO            
                                                     ELSE ''            
                                           END AS LevelNo, CASE [ADDR_TYPE]            
                    WHEN 'L' THEN UNIT_NO            
                                                                     ELSE ''            
                                                              END AS UnitNo, CASE [ADDR_TYPE]            
                                                                                    WHEN 'L' THEN POSTAL_CODE            
                                                                                    ELSE ''            
                                                                                  END AS PostalCode,            
                    CASE [ADDR_TYPE]            
                      WHEN 'F' THEN FOREIGNADDRESS1            
                      WHEN 'C' THEN FOREIGNADDRESS1            
                      ELSE ''            
                    END AS AddressLine1, CASE [ADDR_TYPE]            
                 WHEN 'F' THEN FOREIGNADDRESS2            
                                           WHEN 'C' THEN FOREIGNADDRESS2            
                                           ELSE ''            
                                         END AS AddressLine2, '' AS AddressLine3 ,[nationality_id] as Nationality,[date_of_birth],sex 
                    
                    from employee  E LEFT OUTER JOIN Employee_IR8a EIR ON E.EMP_CODE = EIR.EMP_ID
    where ( E.termination_date is  null  or year(E.termination_date) >= " + Utility.ToInteger(cmbYear.SelectedValue) + " ) AND year(E.JOINING_DATE) <= " + Utility.ToInteger(cmbYear.SelectedValue) + " and E.company_id= " + varCompid + " and E.EMP_NAME is not null";
            //and Eir.ir8a_year="+ Utility.ToInteger(cmbYear.SelectedValue)+"";

            string Empid = "";
            string rcordtype = "";
            string IDType = "";
            string IDNo = "";
            string NameLine1 = "";
            string NameLine2 = "";
            string addresstype = "";
            string year = Utility.ToString(cmbYear.SelectedValue);
            string Ad1 = "";
            string Ad2 = "";
            string Ad3 = "";
            string nationality = "";
            DateTime DateofBirth = DateTime.Now;
            string Sex = "M";


            DataSet sqlEmp = DataAccess.FetchRS(CommandType.Text, sql, null);
            DataTable table = sqlEmp.Tables[0];
            for (int i = 0; i < sqlEmp.Tables[0].Rows.Count; i++)
            {
                Empid = table.Rows[i]["EMPCODE"].ToString();
                rcordtype = table.Rows[i]["RecordType"].ToString();
                IDType = table.Rows[i]["IDType"].ToString();
                IDNo = table.Rows[i]["IDNo"].ToString();

                NameLine1 = table.Rows[i]["NameLine1"].ToString().Replace("'", string.Empty);
                NameLine2 = table.Rows[i]["NameLine2"].ToString().Replace("'", string.Empty); ;
                addresstype = table.Rows[i]["AddressType"].ToString().Replace("'", string.Empty); ;
                if (addresstype == "N" || string.IsNullOrEmpty(addresstype))
                {
                    Ad1 = "";
                    Ad2 = "";
                    Ad3 = "";
                }

                else if (addresstype == "F")
                {
                    Ad1 = table.Rows[i]["AddressLine1"].ToString();
                    Ad2 = table.Rows[i]["AddressLine2"].ToString();
                    Ad3 = table.Rows[i]["AddressLine3"].ToString();
                }
                else if (addresstype == "L")
                {
                    Ad1 = "BLK " + table.Rows[i]["BlockNo"].ToString();
                    Ad2 = table.Rows[i]["stName"].ToString();
                    Ad3 = "SIngapore " + table.Rows[i]["PostalCode"].ToString();
                }

                nationality = table.Rows[i]["Nationality"].ToString();
                DateofBirth = Utility.toDateTime(table.Rows[i]["date_of_birth"].ToString());
                Sex = table.Rows[i]["sex"].ToString();

                string ir8asql = @"exec generate_ira @emp_id=" + Empid + ",@year=" + year + ",@add_type=N'" + addresstype + "'";

                DataAccess.ExecuteNonQuery(ir8asql, null);



                string vSq = @"exec generate_appandixa @emp_id=" + Empid + ",@year=" + year + ",@RecodeType=N'" + rcordtype + "',@IDType=N'" + IDType + "',@IDNo=N'" + IDNo + "'," +
            "@NameLine1=N'" + NameLine1 + "',@NameLine2=N'" + NameLine2 + "',@PostalCode=N'',@AddressType=N'" + addresstype + "',@AddressLine1=N'" + Ad1 + "',@AddressLine2=N'" + Ad2 + "',@AddressLine3=N'" + Ad3 + "'";
                DataAccess.ExecuteNonQuery(vSq, null);




                string bsql = @"exec generate_appandixB @emp_id=" + Empid + ",@year=" + year + ",@RecodeType=N'" + rcordtype + "',@IDType=N'" + IDType + "'," +
 "@IDNo=N'" + IDNo + "',@NameLine1=N'" + NameLine1 + "',@NameLine2=N'" + NameLine2 + "',@Nationality=N'" + nationality + "',@Sex=N'" + Sex + "',@DateOfBirth=N'" + DateofBirth.ToString("ddMMyyyy") + "'";

                DataAccess.ExecuteNonQuery(bsql, null);




            }
            if (this.addressUpdate.Checked == true)
            {
                string _addressSelectedValue = this.cmbaddress.SelectedValue.ToString();

                string sqladdressUpdate = @"update employee_ir8a set addr_type = '" + _addressSelectedValue + "' where ir8a_year = " + year + "";

                DataAccess.ExecuteNonQuery(sqladdressUpdate, null);
            }


            //sQuery = "select emp_code from employee where ( termination_date is  null  or year(termination_date) >= " + Utility.ToInteger(cmbYear.SelectedValue) + " ) AND year(JOINING_DATE) <=" + Utility.ToInteger(cmbYear.SelectedValue) + " and company_id=" + varCompid;

            //DataSet sqlEmpDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            //DataSet sqlNewEmpDs;

            //sQuery = "select * from employee_ir8a where ir8a_year=" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue) - Utility.ToInteger(1));
            //sqlEmpIr8aDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            //sQuery = "select * from employee_ir8a where ir8a_year=" + Utility.ToInteger(Utility.ToInteger(cmbYear.SelectedValue)+1);
            //sqlNewEmpDs = DataAccess.FetchRS(CommandType.Text, sQuery, null);
            //DataTable dt = sqlEmpIr8aDs.Tables[0];
            //DataTable dtExisting = sqlNewEmpDs.Tables[0];
            //DataRow[] dr ;

            //for (int i = 0; i < sqlEmpDs.Tables[0].Rows.Count; i++)
            //{    

            //    DataRow[] drRows = dt.Select("emp_id=" + sqlEmpDs.Tables[0].Rows[i][0].ToString().Trim());
            //    DataRow[] drERows = dtExisting.Select("emp_id=" + sqlEmpDs.Tables[0].Rows[i][0].ToString().Trim());
            //    if(drERows.Length == 0)
            //    if (drRows.Length > 0)
            //    {
            //        sQuery = "  Insert into employee_ir8a(ir8a_year,emp_id,addr_type,tax_borne_employer,tax_borne_employer_options) values('" + (Utility.ToInteger(cmbYear.SelectedValue)) + "'," + sqlEmpDs.Tables[0].Rows[i][0].ToString() + ",'" + drRows[0].ItemArray[3].ToString() + "','No','N' ) ";
            //        DataAccess.ExecuteNonQuery(sQuery, null);
            //    }
            //    else
            //    {
            //        sQuery = "  Insert into employee_ir8a(ir8a_year,emp_id,addr_type,tax_borne_employer,tax_borne_employer_options) values('" + (Utility.ToInteger(cmbYear.SelectedValue)) + "'," + sqlEmpDs.Tables[0].Rows[i][0].ToString() + ",'N','No','N' ) ";
            //        DataAccess.ExecuteNonQuery(sQuery, null);
            //    }
            //    //Data Test
            //}

        }


    }
}

