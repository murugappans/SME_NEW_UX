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
using System.IO;
using System.Text;

namespace SMEPayroll.Payroll
{
    public partial class ClaimsExtApproval : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string s = "";
        string varEmpName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varEmpName = Session["Emp_Name"].ToString();
            SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            //radGridClaimExt.ClientSettings.Selecting.AllowMultiColumnSelect = true;
            radGridClaimExt.ClientSettings.Selecting.AllowRowSelect = true;
            radGridClaimExt.ItemDataBound += new GridItemEventHandler(radGridClaimExt_ItemDataBound);
            //radGridClaimExt.ClientSettings.Selecting.UseClientSelectColumnOnly = true;
            if (!IsPostBack)
            {

                if ((Utility.AllowedAction1(Session["Username"].ToString(), "PENDING APPROVAL CLAIM FOR ALL")) == true)
                    TextBox1.Text = "approverAll";
                else if ((Utility.AllowedAction1(Session["Username"].ToString(), "PENDING APPROVAL FOR CLAIM")) == false)
                {
                    TextBox1.Text = "noapprover";
                }
                else
                {
                    TextBox1.Text = "approver";
                }
                s = Session["Username"].ToString();
                string strSql = "select emp_code,isnull(emp_name,'')+' '+isnull(emp_lname,'') 'emp_name',emp_supervisor from employee where UserName='" + s + "'";
                DataSet leaveset = new DataSet();

                if ((string)Session["EmpCode"] != "0")//if user login
                {
                    leaveset = getDataSet(strSql);
                    lblsuper.Text = Utility.ToString(leaveset.Tables[0].Rows[0]["emp_name"]);
                }
                else
                {
                    lblsuper.Text = "-";
                }




            }
        }

        void radGridClaimExt_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {

                string empcode = Convert.ToString(e.Item.Cells[3].Text).ToString();
                string navUrl = "";
                if (empcode != "&nbsp;" && empcode != "")
                {
                    if (e.Item.Cells.Count >= 9)
                    {
                        if (e.Item.Cells[9].Text != "&nbsp;" && e.Item.Cells[9].Text != "")
                        {
                            navUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + e.Item.Cells[9].Text.Trim();
                            if (e.Item.Cells[9].Text != "")
                            {
                                HyperLink hyp = new HyperLink();
                                hyp.Text = e.Item.Cells[9].Text;
                                hyp.NavigateUrl = navUrl.Trim();
                                e.Item.Cells[9].Controls.Add(hyp);
                            }
                            else
                            {
                                e.Item.Cells[9].Text = "No Document";
                            }
                            //img.Attributes.Add("onclick", "javascript:window.open('http://www.myshoppingCart.no/OpenShopPackageTracker.aspx?installationID=21140000023&orderNumber=" + ProductId.Value.ToString() & "'); return false;")

                        }

                    }
                }
           }
        }

     
       
        private object _dataItem = null;

        protected void remarkRadio_CheckedChanged(object sender, EventArgs e)
        {
            string remarks = txtremarks.Text;

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem.FindControl("CheckBox1");
                    RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
                    if (rad1.Checked == true)
                    {
                        int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
                        string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
                        string strRemarks = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("remarks"));

                        txtEmpRemarks.Value = strRemarks;

                    }
                }
            }

        }
        public object Dataitem
        {
            get
            {
                return this._dataItem;
            }
            set
            {
                this._dataItem = value;
            }
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            //string remarks = txtremarks.Text;
            string remarks = txtEmpRemarks.Value + " - " + Session["Username"].ToString() + ":" + txtremarks.Text;
            foreach (GridItem item in radGridClaimExt.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;

                    if (dataItem.Selected)
                    {
                        string emp_name = "";//= dataItem["emp_name"].Text;
                        if (dataItem.ChildItem.NestedTableViews[0].Items.Count > 0)
                        {
                            int count = dataItem.ChildItem.NestedTableViews[0].Items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                int trxid = Utility.ToInteger(dataItem.ChildItem.NestedTableViews[0].Items[i]["trx_id"].Text);
                                string type = Utility.ToString(dataItem.ChildItem.NestedTableViews[0].Items[i]["desc"].Text);
                                emp_name = Utility.ToString(dataItem.ChildItem.NestedTableViews[0].Items[i]["emp_name"].Text); ;
                                string sSQLCheck = "select status from emp_additions where trx_id = {0}";
                                string sSQLGet = "select ClaimExt from emp_additions where trx_id = {0}";
                                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                                sSQLGet = string.Format(sSQLGet, Utility.ToInteger(trxid));
                                string status = "";
                                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                                SqlDataReader drtrx = DataAccess.ExecuteReader(CommandType.Text, sSQLGet, null);

                                string strclaimext = "";

                                while (drtrx.Read())

                                {
                                    if (drtrx.GetValue(0) != null)
                                    {
                                        strclaimext = Utility.ToString(drtrx.GetValue(0));
                                    }
                                }
                                while (dr.Read())
                                {
                                    status = Utility.ToString(dr.GetValue(0));
                                }
                                if (status == "U")
                                {
                                    string Sql9 = "Update emp_additions set claimstatus='Approved',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "' where trx_id=" + trxid;
                                    Sql9 = Sql9 + ";Update claimsExt SET claimstatus='Approved' where SrNo=" + strclaimext;
                                    try
                                    {
                                        DataAccess.ExecuteStoreProc(Sql9);
                                        strSucSubmit.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        sendemail(trxid, emp_name);
                                    }
                                    catch (Exception ex)
                                    {
                                        string ErrMsg = ex.Message;
                                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                        {
                                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                            strFailSubmit.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        }
                                        else
                                        {
                                            strFailMailMsg.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        }
                                    }
                                }
                                else
                                {
                                    //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                                    lblerror.Text = "Payroll has been Processed, Action not allowed.";
                                }

                            }
                        }
                    }   
                }
            }

            if (strSucSubmit.Length > 0)
            {
                ShowMessageBox("Claims Approved Successfully for: <br/>" + strSucSubmit.ToString());
                strMessage = "";
               
            }
            if (strFailSubmit.Length > 0)
            {
                ShowMessageBox("Claims Not Approved for: <br/>" + strFailSubmit.ToString());
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                strMessage = "";
            }
            radGridClaimExt.DataSource = CreateDataSet();
            radGridClaimExt.DataBind();
        }

        protected void sendemail(int id, string emp_name)
        {
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            string approver = varEmpName;
            //string emp_name = "";
            string emailreq = "";
            string body = "";
            string month = "";
            string year = "";
            string status = "";
            string cc = "";

            string sSQLemail = "sp_email_claim";
            SqlParameter[] parmsemail = new SqlParameter[1];
            parmsemail[0] = new SqlParameter("@trx_id", Utility.ToInteger(id));

            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                to = Utility.ToString(dr3.GetValue(1));
                SMTPserver = Utility.ToString(dr3.GetValue(2));
                SMTPUser = Utility.ToString(dr3.GetValue(3));
                SMTPPass = Utility.ToString(dr3.GetValue(4));
                status = Utility.ToString(dr3.GetValue(5));
                emailreq = Utility.ToString(dr3.GetValue(11)).ToLower();
                from = Utility.ToString(dr3.GetValue(12));
                month = Utility.ToString(dr3.GetValue(13));
                year = Utility.ToString(dr3.GetValue(14));
                body = Utility.ToString(dr3.GetValue(8));
                cc = Utility.ToString(dr3.GetValue(15));
            }

            if (emailreq == "yes")
            {
                string subject = "Claim status for the month of   " + month + "/" + year;


                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@status", status);
                body = body.Replace("@approver", approver);
                body = body.Replace("@month", month);
                body = body.Replace("@year", year);

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(compid);

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

                //try
                //{
                //    string sRetVal = oANBMailer.SendMail();
                //    if (sRetVal == "")
                //        Response.Write("<Font color=green size=3> An email has been sent to " + to + "</Font> <BR />");
                //    else
                //        Response.Write("<Font color=red size=3> An error occurred: Details are as follows <BR />" + sRetVal + "</Font>");
                //}
                //catch (Exception ex)
                //{
                //    string errMsg = ex.Message;
                //}

                try
                {
                    string sRetVal = oANBMailer.SendMail();
                    if (sRetVal == "")
                    {
                        if (to.Length > 0)
                        {
                            if (cc.Length > 0)
                            {
                                strPassMailMsg.Append("<br/>" + emp_name);
                            }
                            else
                            {
                                strPassMailMsg.Append("<br/>" + emp_name);
                            }
                        }
                    }
                    else
                    {
                        strFailMailMsg.Append("<br/>" + emp_name);
                    }
                }
                catch (Exception ex)
                {
                    strFailMailMsg.Append("<br/>" + emp_name);
                }
            }

        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            //Already Given One Claim so rejecting This Claim and also abcdefghijklmnopqrstuvwxyz.
            string remarks = txtEmpRemarks.Value + " - " + Session["Username"].ToString() + ":" + txtremarks.Text;
            StringBuilder strSucSubmit = new StringBuilder();
            StringBuilder strFailSubmit = new StringBuilder();
            //string remarks = txtremarks.Text;
            foreach (GridItem item in radGridClaimExt.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    if (dataItem.Selected)
                    {
                        string emp_name;//= dataItem["emp_name"].Text;
                        if (dataItem.ChildItem.NestedTableViews[0].Items.Count > 0)
                        {
                            int count = dataItem.ChildItem.NestedTableViews[0].Items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                int trxid = Utility.ToInteger(dataItem.ChildItem.NestedTableViews[0].Items[i]["trx_id"].Text);
                                string type = Utility.ToString(dataItem.ChildItem.NestedTableViews[0].Items[i]["desc"].Text);
                                emp_name = Utility.ToString(dataItem.ChildItem.NestedTableViews[0].Items[i]["emp_name"].Text); ;
                                string sSQLCheck = "select status from emp_additions where trx_id = {0}";
                                string sSQLGet = "select ClaimExt from emp_additions where trx_id = {0}";
                                sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
                                sSQLGet = string.Format(sSQLGet, Utility.ToInteger(trxid));
                                string status = "";
                                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
                                SqlDataReader drtrx = DataAccess.ExecuteReader(CommandType.Text, sSQLGet, null);

                                string strclaimext = "";

                                while (drtrx.Read())
                                {
                                    if (drtrx.GetValue(0) != null)
                                    {
                                        strclaimext = Utility.ToString(drtrx.GetValue(0));
                                    }
                                }
                                while (dr.Read())
                                {
                                    status = Utility.ToString(dr.GetValue(0));
                                }
                                if (status == "U")
                                {
                                    string Sql9 = "Update emp_additions set claimstatus='Rejected',approver='" + varEmpName.Replace("'", "") + "',remarks='" + remarks + "' where trx_id=" + trxid;
                                     string Sql91 = "Update claimsExt SET claimstatus='Rejected' where SrNo=" + strclaimext;
                                    try
                                    {
                                        DataAccess.ExecuteStoreProc(Sql9);
                                        DataAccess.ExecuteStoreProc(Sql91);
                                        strSucSubmit.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        sendemail(trxid, emp_name);
                                    }
                                    catch (Exception ex)
                                    {
                                        string ErrMsg = ex.Message;
                                        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
                                        {
                                            //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
                                            strFailSubmit.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        }
                                        else
                                        {
                                            strFailMailMsg.Append("<br/>" + Utility.ToString(emp_name) + "<br/>");
                                        }
                                    }
                                }
                                else
                                {
                                    //Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
                                    lblerror.Text = "Payroll has been Processed, Action not allowed";
                                }

                            }
                        }
                    }

                }
            }
            txtEmpRemarks.Value = "";
            if (strSucSubmit.Length > 0)
            {
                ShowMessageBox("Claims Rejected Successfully for: <br/>" + strSucSubmit.ToString());
                strMessage = "";
            }
            if (strFailSubmit.Length > 0)
            {
                ShowMessageBox("Claims Not Rejected for: <br/>" + strFailSubmit.ToString());
                strMessage = "";
            }
            if (strPassMailMsg.Length > 0)
            {
                ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
                strMessage = "";
            }
            if (strFailMailMsg.Length > 0)
            {
                ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
                strMessage = "";
            }
            radGridClaimExt.DataSource = CreateDataSet();
            radGridClaimExt.DataBind();
            radGridClaimExt.Rebind();
            //foreach (GridItem item in RadGrid1.MasterTableView.Items)
            //{
            //    if (item is GridItem)
            //    {
            //        GridDataItem dataItem = (GridDataItem)item;
            //        RadioButton rad1 = (RadioButton)dataItem.FindControl("remarkRadio");
            //        if (rad1.Checked == true)
            //        {
            //            int trxid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("trx_id"));
            //            string type = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("type"));
            //            string emp_name = Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name"));
            //            string sSQLCheck = "select status from emp_additions where trx_id = {0}";
            //            sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(trxid));
            //            string status = "";
            //            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
            //            while (dr.Read())
            //            {
            //                status = Utility.ToString(dr.GetValue(0));
            //            }
            //            if (status == "U")
            //            {
            //                string Sql9 = "Update emp_additions set claimstatus='Rejected',approver='" + varEmpName + "',remarks='" + remarks + "' where trx_id=" + trxid;
            //                try
            //                {
            //                    DataAccess.ExecuteStoreProc(Sql9);
            //                    strSucSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
            //                    sendemail(trxid, emp_name);

            //                }
            //                catch (Exception ex)
            //                {
            //                    string ErrMsg = ex.Message;
            //                    if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
            //                    {
            //                        //ErrMsg = "<font color = 'Red'>Unable to update the status.Please Try again!</font>";
            //                        strFailSubmit.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
            //                    }
            //                    else
            //                    {
            //                        strFailMailMsg.Append("<br/>" + Utility.ToString(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_name")) + "<br/>");
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
            //            }
            //        }
            //    }
            //}
            //txtEmpRemarks.Value = "";
            //if (strSucSubmit.Length > 0)
            //{
            //    ShowMessageBox("Claims Rejected Successfully for: <br/>" + strSucSubmit.ToString());
            //    strMessage = "";
            //}
            //if (strFailSubmit.Length > 0)
            //{
            //    ShowMessageBox("Claims Not Rejected for: <br/>" + strFailSubmit.ToString());
            //    strMessage = "";
            //}
            //if (strPassMailMsg.Length > 0)
            //{
            //    ShowMessageBox("Email Send successfully to: <br/>" + strPassMailMsg.ToString());
            //    strMessage = "";
            //}
            //if (strFailMailMsg.Length > 0)
            //{
            //    ShowMessageBox("Error While sending Email to: <br/>" + strFailMailMsg.ToString());
            //    strMessage = "";
            //}

            //RadGrid1.DataBind();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                string empcode = Convert.ToString(e.Item.Cells[4].Text).ToString();
                HyperLink hl = (HyperLink)e.Item.FindControl("h1");
                if (hl.Text.ToString().Trim().Length > 0)
                {
                    hl.NavigateUrl = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims" + "/" + hl.Text;
                    hl.ToolTip = "Open Document";
                    hl.Text = "Open Document";
                }
                else
                {
                    hl.Text = "No Document";    
                }
            }
        }

        public  void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();
            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");
            //HttpContext.Current.Response.Write(sbScript);
            //char old = @'\\n';
            //char newc=' ';
            lblerror.Text = "<html>" + lblerror.Text.Replace("\\n", "") + " --- " + message.Replace("\\n", "") + "</html>";
            
        }

        protected void GridClaimExt_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            e.DetailTableView.Width = Unit.Percentage(100); 
            
        }



        protected void GridClaimExt_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
            //GridClientSelectColumn column = new GridClientSelectColumn();
            //radGridClaimExt.MasterTableView.Columns.Add(column);

            
            radGridClaimExt.DataSource = CreateDataSet();
        }

        protected void radGridClaimExt_PreRender(object sender, EventArgs e)
        {
            //foreach (GridColumn col in radGridClaimExt.Columns)
            //{
            //    if (col.HeaderText == "desc")
            //        col.HeaderText = "Claim Type";
            //}
            //radGridClaimExt.Rebind();
        }  


        private DataSet CreateDataSet()
        {

            DataSet dataset = new DataSet();
            ///Employeess
            DataTable dataTable = new DataTable();
            dataTable.TableName = "Employee";         
            dataTable.Columns.Add("TransId");
            
            //Get Data From Database
            SqlParameter[] parms1 = new SqlParameter[4];
            parms1[0] = new SqlParameter("@txt", TextBox1.Text);
            parms1[1] = new SqlParameter("@company_id", compid);
            parms1[2] = new SqlParameter("@approver", lblsuper.Text);
            parms1[3] = new SqlParameter("@UserID", 305);
           
            //sp_emppayclaim_add_EXT
            DataSet dsEmployee = new DataSet();
            dsEmployee = DataAccess.ExecuteSPDataSet("sp_emppendingclaim_add", parms1);
            string[] columnname= new string[1];
            columnname[0] = "TransId";

            DataTable distinctTable=new DataTable();
            if (dsEmployee.Tables.Count > 0)
            {
                distinctTable = dsEmployee.Tables[0].DefaultView.ToTable(true,columnname);
            }


            foreach (DataRow dr in distinctTable.Rows)
            {
                dataTable.Rows.Add(new object[] {dr["TransId"].ToString()});
            }

            DataColumn[] keys = new DataColumn[1];
            keys[0] = dataTable.Columns["TransId"];
            dataTable.PrimaryKey = keys;
            dataset.Tables.Add(dataTable);

            //Claim Type Department Amount Period Attached Document 

            //Actual Claims Data

            dataTable = new DataTable();
            dataTable.TableName = "ClaimsDetail";
            DataColumn coldec = new DataColumn();
            dataTable.Columns.Add("emp_name");
            dataTable.Columns.Add("emp_code");
            coldec.Caption = "Claim Type";
            coldec.ColumnName = "desc";           
            dataTable.Columns.Add(coldec);
            dataTable.Columns.Add("Department");
            dataTable.Columns.Add("trx_amount");
            dataTable.Columns.Add("trx_period");
            dataTable.Columns.Add("Currency");
            dataTable.Columns.Add("recPath");
         
            dataTable.Columns.Add("trx_id");
            dataTable.Columns.Add("TransId");
            dataTable.Columns.Add("Remarks");
            
            //Claim Type Department Amount Period Attached Document 
            foreach (DataRow dr in dsEmployee.Tables[0].Rows)
            {

                dataTable.Rows.Add(new object[] { dr["emp_name"].ToString(), dr["emp_code"].ToString(), dr["desc"].ToString(), dr["Department"].ToString(), dr["trx_amount"].ToString(), dr["trx_period"].ToString(), dr["Currency"].ToString(), dr["recPath"].ToString(), dr["trx_id"].ToString(), dr["TransId"].ToString(), dr["remarks"].ToString() });
            }

            keys = new DataColumn[1];
            keys[0] = dataTable.Columns["trx_id"];
            dataTable.PrimaryKey = keys;
            dataset.Tables.Add(dataTable);


            DataRelation CustomersOrdersRelation =
                    new DataRelation("EmployeeClaims", dataset.Tables["Employee"].Columns["TransId"], dataset.Tables["ClaimsDetail"].Columns["TransId"]);
            //DataRelation OrdersEmployees = new DataRelation("OrdersEmployees", dataset.Tables["Orders"].Columns["EmployeeID"], dataset.Tables["Employees"].Columns["EmployeeID"]);
            dataset.Relations.Add(CustomersOrdersRelation);

            return dataset;
        }

        
    }
}





