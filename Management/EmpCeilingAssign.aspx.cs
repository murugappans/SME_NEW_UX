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

namespace SMEPayroll.Payroll
{
    public partial class EmpCeilingAssign : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
       int compid;
        DataSet compCeiling;
        string _actionMessage = "";
        DataSet compCeilingReplica;
        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            ViewState["actionMessage"] = "";
            int i = 0;
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            string sgroupname = Utility.ToString(Session["GroupName"]);

            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
              compid = Utility.ToInteger(Session["Compid"].ToString());
             //RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Please dont try change leave code from (8 to 16),Because system generated leaves."));
             RadGrid1.ItemCreated += new GridItemEventHandler(RadGrid1_ItemCreated);

             if (!IsPostBack)
             {
                 //string strCriling = "Select  emp_code,emp_name, 0 Selected from employee where termination_date is null and company_id=" + compid;
                 //compCeiling = new DataSet();
                 //compCeiling = DataAccess.FetchRS(CommandType.Text, strCriling, null);
                 //RadGrid5.DataSource = compCeiling;
                 //RadGrid5.DataBind();
                 //Session["EmpCel"] = compCeiling; 
             }

             SqlDataSource1.ConnectionString = Session["ConString"].ToString();
             SqlDataSource2.ConnectionString = Session["ConString"].ToString();
             SqlDataSource3.ConnectionString = Session["ConString"].ToString();
             if (sgroupname == "Super Admin")
             {
                 SqlDataSource2.SelectCommand="Select  emp_code,emp_name from employee where termination_date is null and  Company_Id=@company_id AND emp_code not in ((Select empcode from CeilingEmployee  where CompanyId=@company_id) )Order By emp_name Asc";
             }
             else
             {
                 if (Utility.GetGroupStatus(compid) == 1)
                 {
                     SqlDataSource2.SelectCommand = "Select  emp_code,emp_name from employee where termination_date is null and  Company_Id=@company_id AND emp_code not in ((Select empcode from CeilingEmployee  where CompanyId=@company_id) ) and emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=@Emp_Code AND ValidFrom<=GETDATE()) OR emp_code IN(@Emp_Code)Order By emp_name Asc";
                 }
                 else
                 {
                     SqlDataSource2.SelectCommand = "Select  emp_code,emp_name from employee where termination_date is null and  Company_Id=@company_id AND emp_code not in ((Select empcode from CeilingEmployee  where CompanyId=@company_id) )Order By emp_name Asc";
                 }
             }
         }  

        void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            //{
            //    GridEditableItem item = e.Item as GridEditableItem;
            //    GridTextBoxColumnEditor type = (GridTextBoxColumnEditor)item.EditManager.GetColumnEditor("type");
            //    type.TextBoxControl.Attributes.Add("onblur", "OnFocusLost_type('" + type.TextBoxControl.ClientID + "')");
            //}
        }
       
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           //// check if the value is Null the uncheck the checkbox else check
            //if (e.Item is GridDataItem)
            //{
            //    GridDataItem item = e.Item as GridDataItem;

            //    if (item != null)
            //    {
            //        CheckBox chkSelected = (CheckBox)item.FindControl("chkSelected");

            //        if (chkSelected != null)
            //        {
            //            string strEmp = "Select * from CeilingEmployee where CompanyId=" + compid;                        
            //            DataSet ds = new DataSet();
            //            ds = DataAccess.FetchRS(CommandType.Text, strEmp, null);                        
            //            foreach (DataRow dr in ds.Tables[0].Rows)
            //            {
            //                if (item["emp_code"].Text.ToString() == dr["EmpCode"].ToString())
            //                {
            //                    chkSelected.Checked = true;
            //                }
            //            }
            //        }
            //    }
            //}
        }
     
        protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //try
            //{
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["id"]);

            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from emp_leaves  where leave_type=" + id, null);
            //    if (dr.Read())
            //    {
            //        if (dr[0].ToString() != "0")
            //        {
            //            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type.This leave type is in use."));
            //        }
            //        else
            //        {
            //            string sSQL = "DELETE FROM [leave_types] WHERE [id] =" + id;

            //            int retVal = DataAccess.ExecuteStoreProc(sSQL);

            //            if (retVal == 1)
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Leave Type is Deleted Successfully."));

            //            }
            //            else
            //            {
            //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the leave type."));
            //            }

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    string ErrMsg = ex.Message;
            //    if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
            //        ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
            //    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
            //    e.Canceled = true;
            //}

        }




        //Update in table 
        //whether it is need in paysip ornot
        protected void CheckChanged(Object sender, System.EventArgs e)
        {
            CheckBox box = (CheckBox)sender;
            string strval = "";

            int pk = -1;
            string para = "";
            char sep =':';
            string[] tooltip = box.ToolTip.Split(sep);
            if (tooltip.Length > 1)
            {
                pk =Convert.ToInt32(tooltip[0]);
                para = Convert.ToString (tooltip[1]);
            }

            if (box.ID == "chkSelected")
            {
                if (box.Checked)
                {
                    //string ssqlb = "UPDATE [Currency] SET [Selected] = '1' WHERE Id='" + Convert.ToInt32(box.ToolTip) + "'";
                    //DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                    strval = "INSERT [CeilingEmployee]([EmpCode],[CompanyId])VALUES(" + Convert.ToInt32(box.ToolTip) + "," + compid + ")";
                    DataAccess.FetchRS(CommandType.Text, strval, null);
                }
                else
                {
                    strval = "DELETE [CeilingEmployee] WHERE [EmpCode]=" + Convert.ToInt32(box.ToolTip);
                    DataAccess.FetchRS(CommandType.Text, strval, null);
                }
            }
            string strCriling = "Select  emp_code,emp_name, 0 Selected from employee where termination_date is null and company_id=" + compid;
            compCeiling = new DataSet();
            compCeiling = DataAccess.FetchRS(CommandType.Text, strCriling, null);
            RadGrid1.DataSource = compCeiling;
            RadGrid1.DataBind();

            Session["EmpCel"] = compCeiling; 
        }

        protected bool GetPaySlip(object InPayslip)
        {
            //if (Convert.ToString(InPayslip) =="NULL")
            //{
            //    return false;
            //}
            //else
            //{
            //    return false;
            //}

            return false;

        }


        //protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        //{
        //    if ((Utility.AllowedAction1(Session["Username"].ToString(), "Project Assignment")) == false)
        //    {
        //        RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
        //        RadGrid1.MasterTableView.GetColumn("DeleteColumn").Visible = false;
        //    }
        //}
        //protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        //{
        //    try
        //    {
        //        GridEditableItem editedItem = e.Item as GridEditableItem;
        //        string id = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["ID"]);

        //        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, "Select count(*) cnt from employee where 1=2", null);
        //        if (dr.Read())
        //        {
        //            if (Convert.ToInt16(dr[0].ToString()) > 0)
        //            {
        //                RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project Assigned.This Project Assigned is in use."));
        //            }
        //            else
        //            {
        //                string sSQL = "DELETE FROM [EmployeeAssignedToProject] WHERE [ID] =" + id;

        //                int retVal = DataAccess.ExecuteStoreProc(sSQL);

        //                if (retVal >= 1)
        //                {
        //                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Project Assigned is Deleted Successfully."));
        //                    RadGrid2.Rebind();
        //                    RadGrid1.Rebind();
        //                }
        //                else
        //                {
        //                    RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete the Project Assigned."));
        //                }

        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string ErrMsg = ex.Message;
        //        if (ErrMsg.IndexOf("REFERENCE constraint", 1) > 0)
        //            ErrMsg = "<font color = 'Red'>Record can not be deleted becuase of REFERENCE constraint. This record is called in other tables</font>";
        //        RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to delete record. Reason:</font> " + ErrMsg));
        //        e.Canceled = true;
        //    }

        //}
        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
        }
        protected void RadGrid1_ItemUpdated(object source, GridUpdatedEventArgs e)
        {
        }
        private void DisplayMessage(string text)
        {
            //RadGrid1.Controls.Add(new LiteralControl(text));
        }

        protected void RadGrid2_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid2.CurrentPageIndex = e.NewPageIndex;
            //DataSet ds = new DataSet();
            //string sSQL = "select emp_code,emp_name + ' ' + emp_lname as emp_name,(select DeptName from department where id=dept_id) Department,time_card_no,empcpftype,emp_alias,emp_type,ic_pp_number,wp_exp_date,pr_date,address,pay_frequency, payrate,postal_code,phone,hand_phone,email,time_card_no,sex,marital_status,place_of_birth,date_of_birth,education,income_taxid,termination_reason,giro_bank,giro_code,giro_branch,giro_acct_number,joining_date,probation_period,confirmation_date,termination_date,cpf_entitlement,cpf_employer,cpf_employee,employee_cpf_acct,employer_cpf_acct,emp_supervisor,ot_entitlement,payment_mode,fw_code,fw_levy,sdf_required,cdac_fund,mbmf_fund,sinda_fund,ecf_fund,cchest_fund,email_payslip,wh_tax_pct,wh_tax_amt,remarks,images,Insurance_number,insurance_expiry,CSOC_number, CSOC_expiry,passport,passport_expiry,nationality_id,country_id,religion_id,race_id,desig_id,dept_id,emp_group_id, (select empgroupname from emp_group where [id]=e.emp_group_id) empgroupname from employee e where Company_Id=" + varCompid + " ORDER BY emp_name ";
            //ds = GetDataSet(sSQL);
            RadGrid2.DataBind();
        }

        protected void drpTypeID_databound(object sender, EventArgs e)
        {
            //drpTypeID.Items.Insert(0, new ListItem("-select-", "-1"));
        }

        protected void buttonAdd_Click(object sender, EventArgs e)
        {
            string strAction = ((Button)sender).Text;
            RadGrid rd = new RadGrid();
            if (strAction == "Assign")
            {
                rd = RadGrid2;
            }
            if (strAction == "Un-Assign")
            {
                rd = RadGrid1;
            }

               int i = 0;
                SqlParameter[] parms = new SqlParameter[4];
                string strEmployee = "0";

                foreach (GridItem item in rd.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        CheckBox chkBox = (CheckBox)dataItem["Assigned"].Controls[0];
                        if (chkBox.Checked == true)
                        {
                            strEmployee = strEmployee + "," + dataItem.Cells[3].Text.ToString().Trim();
                        }
                    }
                }

                if (strEmployee.Length > 0 && strEmployee != "0")
                {
                    string strActionMsg = "";
                    parms[0] = new SqlParameter("@Company_ID", Utility.ToInteger(compid));
                    parms[1] = new SqlParameter("@EmpID", Utility.ToString(strEmployee));
                    if (strAction == "Assign")
                    {
                        strActionMsg = "Assigned";
                        parms[2] = new SqlParameter("@Action", Utility.ToString("0"));
                    }
                    if (strAction == "Un-Assign")
                    {
                        strActionMsg = "Un-Assigned";
                        parms[2] = new SqlParameter("@Action", Utility.ToString("1"));
                    }
                    parms[3] = new SqlParameter("@retval", SqlDbType.Int);
                    parms[3].Direction = ParameterDirection.Output;
                    string sSQL = "sp_employee_Ceil_Assign";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                    if (retVal >= 1)
                    {
                        //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Employee Assigned " + strActionMsg + " Successfully."));
                    _actionMessage = "sc|Employee Assigned "+ strActionMsg + " Successfully.";
                    ViewState["actionMessage"] = _actionMessage;
                    RadGrid2.Rebind();
                        RadGrid1.Rebind();
                    }
                    else
                    {
                        //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to " + strActionMsg + " Employee."));
                    _actionMessage = "Warning|Unable to "+ strAction + " Employee.";
                    ViewState["actionMessage"] = _actionMessage;
                }
                }
                else
                {
                    //rd.Controls.Add(new LiteralControl("<font color = 'Red'>Please Select Employee."));
                _actionMessage = "Warning|Please Select Employee.";
                ViewState["actionMessage"] = _actionMessage;
            }

           
        }

    }
}
