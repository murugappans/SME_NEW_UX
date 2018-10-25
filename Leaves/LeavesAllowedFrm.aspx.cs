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
using System.Text;

namespace SMEPayroll.Leaves
{
    public partial class LeavesAllowedFrm : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        int leavemodel;
        string sSQL = "";
        string _actionMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["actionMessage"] = "";
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger(Session["Compid"].ToString());
            lblerror.Text = "";
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {
                string sSQL = "select distinct id,empgroupname from emp_group where company_id={0} ORDER BY empgroupname";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                cmbEmpgroup.Items.Clear();
                cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem("--Select--", ""));
                while (dr.Read())
                {
                    cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr.GetValue(1)), Utility.ToString(dr.GetValue(0))));
                }

                cmbEmpgroup.SelectedValue = "";
                cmbLeaveYear.SelectedValue = System.DateTime.Today.Year.ToString();
       
            }

            DataSet Compset = new DataSet();
            string Str = "Select Leave_model From Company Where Company_ID=" + compid;
            Compset = DataAccess.FetchRS(CommandType.Text, Str, null);
            if (Compset.Tables[0].Rows.Count > 0)
            {
                leavemodel = Utility.ToInteger(Compset.Tables[0].Rows[0][0].ToString());
            }


            //if (!IsPostBack)
            //{
            //    RadGrid1.DataSource = SqlDataSource1;
            //    RadGrid1.DataBind();
            //}
            //RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
        }

        //void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        //{
        //    RadGrid1.DataSource = SqlDataSource1;

        //}



        protected void bindgrid(object sender, EventArgs e)
        {

            RadGrid1.DataSource = SqlDataSource1;
            RadGrid1.DataBind();

            if (cmbEmpgroup.SelectedItem.Text == "--Select--")
            {
                //ShowMessageBox("Employee Group Name is not selected!");
                _actionMessage = "Warning|Employee Group Name is not selected!";
                ViewState["actionMessage"] = _actionMessage;

            }
            else
            {             
                //}

                //Added By Raja(19/12/2008)
                //string sSQLCheck = "select count(leave_model) as model from company where company_id='" + compid + "' and leave_model in(1,2,5,7)";
                string sSQLCheck = "select count(leave_model) as model from company where company_id='" + compid + "' and leave_model in(1,2,5)";
                DataSet ds = new DataSet();
                ds = DataAccess.FetchRS(CommandType.Text, sSQLCheck, null);
                if (Convert.ToInt16(ds.Tables[0].Rows[0]["model"]).ToString() == "0")
                {
                    RadGrid2.Visible = true;
                }
                else
                {
                    RadGrid2.Visible = false;

                }
                RadGrid1.Visible = true;
                gvCopyToNextYear.Visible = false;
            }

            //if (chkCopy.Checked == true)
            //{
            //    SqlParameter[] parms = new SqlParameter[3];
            //    parms[0] = new SqlParameter("@groupid", Utility.ToInteger(cmbEmpgroup.SelectedValue));
            //    parms[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            //    parms[2] = new SqlParameter("@leave_year", Utility.ToInteger(cmbLeaveYear.SelectedValue) - 1);

            //    DataSet ds1 = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_allowed_leaves", parms);
            //    //exec sp_allowed_leaves @groupid=8,@compid=3,@leave_year=N'2011'
            //    if (ds1.Tables.Count > 0)
            //    {
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {

            //            RadGrid1.DataSource = ds1;
            //            RadGrid1.DataBind();
            //        }
            //    }
            //}
            //else
            //{
            
        }
        protected void gvCopyToNextYear_ItemDataBound(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridHeaderItem) 
            {
                int NextYear = int.Parse(cmbLeaveYear.SelectedValue.ToString())+1;
                GridHeaderItem header = (GridHeaderItem)e.Item;
                header["leaves_allowed"].Text = "Leaves Allowed for Selected Year (" + cmbLeaveYear.SelectedValue.ToString() + ")";
                header["NextYearLeavesAllowed"].Text = "Leaves Allowed for Next Year (" + NextYear + ")";
            }
        }
        protected void gvCopyToNextYear_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "SaveToNextYear")
            {
                foreach (GridItem item in gvCopyToNextYear.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int typeid = Utility.ToInteger(gvCopyToNextYear.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("typeid"));
                        //int id = Utility.ToInteger(dataItem["id"].Text); //Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        int id = Utility.ToInteger(gvCopyToNextYear.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        int groupid = Utility.ToInteger(gvCopyToNextYear.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("group_id"));
                        double leaves = Utility.ToDouble(gvCopyToNextYear.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("leaves_allowed"));

                        TextBox txtbox = (TextBox)dataItem.FindControl("txtleaves");

                        double i = Utility.ToDouble(txtbox.Text);

                        int NextYear = Utility.ToInteger(cmbLeaveYear.SelectedValue) + 1;

                        string strSQL = "";
                        strSQL = "Select * from leaves_allowed where leave_year = " + NextYear + " and leave_type=" + typeid + " and group_id= " + groupid + "";
                        DataSet ds = new DataSet();
                        ds = DataAccess.FetchRS(CommandType.Text, strSQL, null);


                        sSQL = "";

                        if (ds.Tables[0].Rows.Count < 1)
                        {
                            sSQL = "Insert into leaves_allowed (group_id,leave_type,leaves_allowed,leave_year) values(" + groupid + "," + typeid + "," + i + "," + NextYear + " )";
                        }
                        else
                        {
                            //sSQL = "Delete from leaves_allowed where id=" + id;

                            sSQL = "Update leaves_allowed set leaves_allowed=" + i + " where leave_year = " + NextYear + " and leave_type=" + typeid + " and group_id= " + groupid + "";
                        }                       
         
                        try
                        {
                            if (sSQL != "")
                            {
                                DataAccess.ExecuteStoreProc(sSQL);
                                //lblerror.ForeColor = System.Drawing.Color.Green;
                                //lblerror.Text = "Leaves Allowed Updated sucessfully";
                                _actionMessage = "sc|Leaves Allowed Updated sucessfully";
                                ViewState["actionMessage"] = _actionMessage;


                            }
                        }
                        catch (Exception msg)
                        {
                            //lblerror.ForeColor = System.Drawing.Color.Red;
                            //lblerror.Text = msg.Message.ToString();
                            _actionMessage = "Warning|"+msg.Message.ToString();
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }

                }
                ShowInCopyGrid();
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
                        int typeid = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("typeid"));
                        //int id = Utility.ToInteger(dataItem["id"].Text); //Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        int groupid = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("group_id"));
                        double leaves = Utility.ToDouble(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("leaves_allowed"));

                        TextBox txtbox = (TextBox)dataItem.FindControl("txtleaves");

                        double i = Utility.ToDouble(txtbox.Text);

                        sSQL = "";
                        if ((id == 0) && (i >= 0))
                        {
                            if (txtbox.Text.ToString().Length > 0)
                            {

                                sSQL = "Insert into leaves_allowed (group_id,leave_type,leaves_allowed,leave_year) values(" + cmbEmpgroup.SelectedValue + "," + typeid + "," + i + "," + Utility.ToInteger(cmbLeaveYear.SelectedValue) + " )";
                            }
                        }
                        else if (id != 0)
                        {
                            if (i <= 0 && txtbox.Text.ToString().Length == 0)
                            {
                                sSQL = "Delete from leaves_allowed where id=" + id;
                            }
                            else
                            {
                                sSQL = "Update leaves_allowed set leaves_allowed=" + i + " where id=" + id;
                            }
                        }
                        try
                        {
                            if (sSQL != "")
                            {
                                DataAccess.ExecuteStoreProc(sSQL);
                                //lblerror.Text = "Leaves Allowed Updated sucessfully";
                                _actionMessage = "sc|Leaves Allowed Updated sucessfully";
                                ViewState["actionMessage"] = _actionMessage;


                            }
                        }
                        catch (Exception msg)
                        {
                            //lblerror.Text = msg.Message.ToString();
                            _actionMessage = "Warning|"+msg.Message.ToString();
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }

                }

            }
            else if (e.CommandName == "UpdateDataEmp")
            {
                 string strSQL = "select leave_model from company where company_id='" + compid + "'";
                 DataSet ds = new DataSet();
                 ds = DataAccess.FetchRS(CommandType.Text, strSQL, null);
                 string leave_model = ds.Tables[0].Rows[0][0].ToString();

                foreach (GridItem item in RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int typeid = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("typeid"));
                        int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                        int groupid = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("group_id"));
                        double leaves = Utility.ToDouble(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("leaves_allowed"));

                        TextBox txtbox = (TextBox)dataItem.FindControl("txtleaves");
                        double i = Utility.ToDouble(txtbox.Text);
                        //string ssql1 = "delete from EmployeeLeavesAllowed where emp_id='" + empcode + "' and leave_year='" + previousyear + "' And Leave_Type=8";
                        try
                        {
                            if (typeid != 8)//need to change 4 AL update, murugan
                            {
                                string ssql1 = "Delete from EmployeeLeavesAllowed where emp_id IN ( SELECT emp_code FROM employee  WHERE emp_group_id IN(" + cmbEmpgroup.SelectedValue + ") AND termination_date IS NULL) and leave_year='" + cmbLeaveYear.SelectedValue + "' And Leave_Type=" + typeid;
                                DataAccess.FetchRS(CommandType.Text, ssql1);

                                string sql2 = "SELECT emp_code FROM employee  WHERE emp_group_id IN(" + cmbEmpgroup.SelectedValue + ") AND termination_date IS NULL";

                                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql2, null);
                                string ssql2 = "";
                                //While Loop Starts
                                while (dr1.Read())
                                {
                                    //cmbEmpgroup.Items.Add(new System.Web.UI.WebControls.ListItem(Utility.ToString(dr1.GetValue(1)), Utility.ToString(dr1.GetValue(0))));
                                    if (ssql2.Length == 0)
                                    {
                                        if (i > 0)
                                        {
                                            ssql2 = "insert into EmployeeLeavesAllowed (emp_id,leave_year,leave_type,leaves_allowed, LY_Leaves_Bal) values ('" + Utility.ToInteger(dr1.GetValue(0)) + "','" + cmbLeaveYear.SelectedValue + "'," + typeid + ",'" + i + "','" + 0 + "');";
                                        }
                                    }
                                    else
                                    {
                                        if (i > 0)
                                        {
                                            ssql2 = ssql2 + "insert into EmployeeLeavesAllowed (emp_id,leave_year,leave_type,leaves_allowed, LY_Leaves_Bal) values ('" + Utility.ToInteger(dr1.GetValue(0)) + "','" + cmbLeaveYear.SelectedValue + "'," + typeid + ",'" + i + "','" + 0 + "');";
                                        }
                                    }

                                    
                                }
                                //While Loop Ends
                                if (ssql2 != "")
                                {
                                    DataAccess.FetchRS(CommandType.Text, ssql2);
                                }
                            }                          
                            
                            //lblerror.Text = "Leaves copied sucessfully";
                            _actionMessage = "sc|Leaves copied sucessfully";
                            ViewState["actionMessage"] = _actionMessage;

                        }
                        catch (Exception ex)
                        {
                            //lblerror.Text = ex.ToString();
                            _actionMessage = "Warning|"+ex.ToString();
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }

                }
                // Added by SU MON
                // ---------------------------------- For Hybird Leave Model ------------------------------------

                string sqlEmp = "SELECT emp_code FROM employee  WHERE emp_group_id IN(" + cmbEmpgroup.SelectedValue + ") AND termination_date IS NULL";

                SqlDataReader drEmp = DataAccess.ExecuteReader(CommandType.Text, sqlEmp, null);
                int emp_id = 0;
                try
                {
                    if (int.Parse(leave_model) == 9 || int.Parse(leave_model) == 10)
                    {
                        string ssql1 = "Delete from EmployeeLeavesAllowed where LY_Leaves_Bal = -1 and emp_id IN ( SELECT emp_code FROM employee  WHERE emp_group_id IN(" + cmbEmpgroup.SelectedValue + ") AND termination_date IS NULL) and leave_year='" + cmbLeaveYear.SelectedValue + "' And Leave_Type=8";
                        DataAccess.FetchRS(CommandType.Text, ssql1);

                        while (drEmp.Read())
                        {
                            emp_id = Utility.ToInteger(drEmp.GetValue(0));
                            SqlParameter[] pars = new SqlParameter[3];
                            pars[0] = new SqlParameter("@emp_id", emp_id);
                            pars[1] = new SqlParameter("@year", Utility.ToInteger(cmbLeaveYear.SelectedValue));
                            pars[2] = new SqlParameter("@status", "Insert" );
                            try {
                                int check = DataAccess.ExecuteStoreProc("Sp_hybrid_leave_insert", pars);
                            }
                            catch (Exception ex) { }                            
                        }

                    }
                }
                catch (Exception ex) { }

                
                // ---------------------------------- End Hybrid Leave Model ----------------------------------------

            }
            //else if (e.CommandName == "UpdateLastYear")
            //{
            //    //Copy Leaves for Last year 
            //    SqlParameter[] parms = new SqlParameter[3];
            //    parms[0] = new SqlParameter("@groupid", Utility.ToInteger(cmbEmpgroup.SelectedValue));
            //    parms[1] = new SqlParameter("@compid", Utility.ToInteger(compid));
            //    parms[2] = new SqlParameter("@leave_year", Utility.ToInteger(cmbLeaveYear.SelectedValue) - 1);

            //    DataSet ds1 = DataAccess.FetchRS(CommandType.StoredProcedure, "sp_allowed_leaves", parms);
            //    //exec sp_allowed_leaves @groupid=8,@compid=3,@leave_year=N'2011'
            //    if (ds1.Tables.Count > 0)
            //    {
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {

            //            RadGrid1.DataSource = ds1;
            //            RadGrid1.DataBind();
            //        }
            //    }
            //}

            RadGrid1.DataSource = SqlDataSource1;
            RadGrid1.DataBind();
        }


        protected void RadGrid2_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "UpdateAll")
            {
                foreach (GridItem item in RadGrid2.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        int comp_id = Utility.ToInteger(RadGrid2.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("comp_id"));
                        int groupid = Utility.ToInteger(RadGrid2.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("group_id"));
                        int year = Utility.ToInteger(RadGrid2.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("year_of_service"));
                        double leaves = Utility.ToDouble(RadGrid2.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("leaves_allowed"));

                        TextBox txtleaves = (TextBox)dataItem.FindControl("txtleavesallowed");

                        double j = Utility.ToDouble(txtleaves.Text);
                        sSQL = "";

                        sSQL = "Update [prorated_leaves] set [leaves_allowed]=" + j + " where [comp_id]=" + comp_id + " and [group_id]=" + groupid + " and[year_of_service]=" + year;

                        try
                        {
                            if (sSQL != "")
                                DataAccess.ExecuteStoreProc(sSQL);
                            _actionMessage = "sc|Updated Successfully";
                            ViewState["actionMessage"] = _actionMessage;
                        }
                        catch (Exception msg)
                        {
                            //lblerror.Text = msg.Message.ToString();
                            _actionMessage = "Warning|"+msg.Message.ToString();
                            ViewState["actionMessage"] = _actionMessage;
                        }
                    }

                }

            }

            RadGrid2.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem dataItem = e.Item as GridDataItem;
                if (leavemodel == 3 || leavemodel == 4 || leavemodel == 6 || leavemodel == 8)
                {

                    int typeid = Utility.ToInteger(dataItem.Cells[5].Text);
                    if (typeid == 8)
                    {
                        TextBox txtbox = (TextBox)dataItem.FindControl("txtleaves");
                        txtbox.Enabled = false;
                    }
                }

            }
            try
            {
                if (gvCopyToNextYear.Visible == true)
                {
                    if (e.Item is GridHeaderItem)
                    {
                        int NextYear = int.Parse(cmbLeaveYear.SelectedValue.ToString()) + 1;
                        GridHeaderItem header = (GridHeaderItem)e.Item;
                        header["leaves_allowed"].Text = "Leave Allowed for Selected Year (" + cmbLeaveYear.SelectedValue.ToString() + ")";
                        header["NextYearLeavesAllowed"].Text = "Leaves Allowed for Next Year (" + NextYear + ")";
                    }
                }
            }
            catch (Exception ex) { }
            
            
        }

        protected void CopyToNextYear(object sender, EventArgs e)
        {
            ShowInCopyGrid();
        }

        protected void ShowInCopyGrid()
        {
            try
            {
                DataTable dtLeavesgroup = new DataTable();
                string sSQL = "select distinct id,empgroupname from emp_group where company_id={0} ORDER BY empgroupname";
                sSQL = string.Format(sSQL, Utility.ToInteger(Session["Compid"]));
                DataSet ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                dtLeavesgroup = ds.Tables[0];

                DataTable dtTemp = new DataTable();
                DataTable dtFinal = new DataTable();

                for (int i = 0; i < dtLeavesgroup.Rows.Count; i++)
                {
                    dtTemp.Clear();
                    string sqlAllowLeaves = "sp_allowed_leaves";
                    SqlParameter[] pars = new SqlParameter[3];

                    pars[0] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                    pars[1] = new SqlParameter("@groupid", Utility.ToInteger(dtLeavesgroup.Rows[i]["id"].ToString()));
                    pars[2] = new SqlParameter("@leave_year", Convert.ToString(cmbLeaveYear.SelectedValue.ToString()));
                    DataSet dsResult = DataAccess.FetchRS(CommandType.StoredProcedure, sqlAllowLeaves, pars);

                    dtTemp = dsResult.Tables[0];

                    try
                    {
                        dtTemp.Columns.Add("Group_Name");
                        dtTemp.Columns.Add("NextYearLeavesAllowed");
                    }
                    catch (Exception ex) { }

                    for (int l = 0; l <= dtTemp.Rows.Count - 1; l++)
                    {
                        dtTemp.Rows[l]["group_id"] = dtLeavesgroup.Rows[i]["id"].ToString();
                        dtTemp.Rows[l]["Group_Name"] = dtLeavesgroup.Rows[i]["empgroupname"].ToString();
                    }


                    int next_year = int.Parse(cmbLeaveYear.SelectedValue) + 1;

                    SqlParameter[] parsNextY = new SqlParameter[3];

                    parsNextY[0] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));
                    parsNextY[1] = new SqlParameter("@groupid", Utility.ToInteger(dtLeavesgroup.Rows[i]["id"].ToString()));
                    parsNextY[2] = new SqlParameter("@leave_year", Convert.ToString(next_year));
                    DataSet dsNextYear = DataAccess.FetchRS(CommandType.StoredProcedure, sqlAllowLeaves, parsNextY);

                    for (int n = 0; n < dsNextYear.Tables[0].Rows.Count; n++)
                    {
                        for (int o = 0; o < dtTemp.Rows.Count; o++)
                        {
                            if (dtTemp.Rows[o]["typeid"].ToString() == dsNextYear.Tables[0].Rows[o]["typeid"].ToString())
                            {
                                //dtTemp.Rows[o]["NextYearLeavesAllowed"] = dsNextYear.Tables[0].Rows[o]["leaves_allowed"];
                                dtTemp.Rows[o]["NextYearLeavesAllowed"] = dtTemp.Rows[o]["leaves_allowed"];
                            }
                        }
                    }


                    dtFinal.Merge(dtTemp);
                    Session["dtFinal"] = dtFinal;
                }

                gvCopyToNextYear.DataSource = dtFinal;
                gvCopyToNextYear.DataBind();

                string sSQLCheck = "select count(leave_model) as model from company where company_id='" + compid + "' and leave_model in(1,2,5)";
                DataSet ds1 = new DataSet();
                ds1 = DataAccess.FetchRS(CommandType.Text, sSQLCheck, null);
                if (Convert.ToInt16(ds1.Tables[0].Rows[0]["model"]).ToString() == "0")
                {
                    RadGrid2.Visible = true;
                }
                else
                {
                    RadGrid2.Visible = false;

                }

                RadGrid1.Visible = false;
                RadGrid2.Visible = false;
                gvCopyToNextYear.Visible = true;
            }
            catch (Exception ex) { }
        }

        protected void gvCopyToNextYear_GroupsChanging(object source, GridGroupsChangingEventArgs e)
        {

            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["dtFinal"];

                gvCopyToNextYear.DataSource = dt;
                gvCopyToNextYear.DataBind();

            }
            catch (Exception ex) { }
        }


        public static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder(50);
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            HttpContext.Current.Response.Write(sbScript);
        }
       
    }
}
