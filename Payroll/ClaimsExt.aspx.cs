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
using System.Net.Mail;
using System.IO;
using System.Text;


namespace SMEPayroll.Payroll
{
    public partial class ClaimsExt : System.Web.UI.Page
    {
        string strMessage = "";
        StringBuilder strFailMailMsg = new StringBuilder();
        StringBuilder strPassMailMsg = new StringBuilder();
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        static string varFileName = "";
        SqlDataAdapter dataAdapterClaims;

        int compid;
        string varEmpCode = "";
        
        int ApproveNeeded, trx;
        string PrimaryAddress, SecondaryAddress, filename;
        string sgroupname = "";

        double valBefGst1;
        double valAmtGst;
        double payamount;
        bool flag;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            //SqlDataSource2.ConnectionString = Session["ConString"].ToString();
            compid = Utility.ToInteger(Session["Compid"]);
            RadGrid1.ItemCommand += new GridCommandEventHandler(RadGrid1_ItemCommand);
            RadGrid1.ItemDataBound += new GridItemEventHandler(RadGrid1_ItemDataBound);
            SqlDataSource6.ConnectionString = Session["ConString"].ToString();
            if (!IsPostBack)
            {

                #region Yeardropdown
                cmbYear.DataBind();
                #endregion  
                varEmpCode = Session["EmpCode"].ToString();
                DataSet ds_employee = new DataSet();
                
                //
                sgroupname = Utility.ToString(Session["GroupName"]);
                if (sgroupname == "Super Admin" || (Utility.AllowedAction1(Session["Username"].ToString(), "Apply Claim for all")==true ))
                {
                  ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null  and company_id=" + compid + " order by emp_name");
                }
                else
                {
                    ds_employee = getDataSet("SELECT [emp_code], isnull([emp_name],'')+' '+isnull([emp_lname],'') 'emp_name'  FROM [employee] where termination_date is null and emp_code='" + varEmpCode + "' and company_id=" + compid + " order by emp_name");
                }
                //

                drpemp.DataSource = ds_employee.Tables[0];
                drpemp.DataTextField = ds_employee.Tables[0].Columns["emp_name"].ColumnName.ToString();
                drpemp.DataValueField = ds_employee.Tables[0].Columns["emp_code"].ColumnName.ToString();
                drpemp.DataBind();
                string empid = Utility.ToString(DataBinder.Eval(Dataitem, "emp_code"));
                drpemp.SelectedValue = varEmpCode.ToString();
                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                string SQLQuery;
               // SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + " and GroupName='Super Admin')";
                SQLQuery = "select count(emp_code) from employee where company_id=" + compid + " and emp_code=" + varEmpCode + " and GroupId in(select GroupId from usergroups where company_id=" + compid + ")";

                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQLQuery, null);
                if (dr.Read())
                {
                    if (Utility.ToInteger(dr[0].ToString()) > 0)
                    {
                       // drpemp.Enabled = true;
                    }
                    else
                    {
                       // drpemp.Enabled = false;
                    }
                }
            }

        }

       protected void drpemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDropdown();
            lblerror.Text = "";
        }

       protected void FillDropdown()
        {
             
                if (radSelect.SelectedValue == "N")
                {
                    drpRejected.Enabled = false;
                    drpRejected.Items.Clear();
                }
                else
                {

                    string strRejected = "";
                    if (DropDownList1.SelectedValue == "13")
                    {
                        strRejected = "Select Distinct(TransId) FROM  ClaimsExt WHERE claimstatus='Rejected' and Year(trx_period)=" + cmbYear.SelectedValue + " AND emp_code=" + drpemp.SelectedValue;
                    }
                    else
                    {
                        strRejected = "Select Distinct(TransId) FROM  ClaimsExt WHERE claimstatus='Rejected' and Year(trx_period)=" + cmbYear.SelectedValue + " AND Month(trx_period) =" + DropDownList1.SelectedValue + " AND emp_code=" + drpemp.SelectedValue;
                    }

                    DataSet dsrej = new DataSet();

                    dsrej = DataAccess.FetchRS(CommandType.Text, strRejected, null);
                    if (dsrej != null)
                    {
                        if (dsrej.Tables.Count > 0)
                        {
                            if (dsrej.Tables[0].Rows.Count > 0)
                            {
                                drpRejected.Enabled = true;
                                drpRejected.DataSource = dsrej.Tables[0];
                                drpRejected.DataTextField = "TransId";
                                drpRejected.DataValueField = "TransId";
                                drpRejected.DataBind();
                            }
                            else
                            {
                                drpRejected.Items.Clear();
                                drpRejected.Enabled = false;

                            }
                        }
                        else
                        {
                            drpRejected.Enabled = false;
                            drpRejected.Items.Clear();

                        }
                    }
                    else
                    {
                        drpRejected.Enabled = false;
                        drpRejected.Items.Clear();
                    }

                }
           
            
        }

       protected void radSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblerror.Text = "";
            FillDropdown();
        }
         

      

        void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            //Session["DsClaim"]
            if (e.CommandName == "AddNew")
            {
                        flag = true;
                        DataSet dsAdd = new DataSet();
                        dsAdd = (DataSet)Session["DsClaim"];
                        //Update DS1 Session Value As we do postback
                        string strPk = "";
                        string strPK1 = "";
                        foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                        {
                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;
                                string pk1 = dataItem["SrNo"].Text.ToString();
                                DataRow[] dr1 = dsAdd.Tables[0].Select("SrNo='" + pk1 + "'");

                                //SrNo	id	Desc	cpf	AmtGst	BefGst	GstAmnt	trx_period	emp_code	emp_name	Department	
                                //recpath	Descriptions	Designation	emp_type	TimeCardId	Nationality	Trade	CurrencyID	
                                //ConversionOpt	GLAcc	created_on	created_by	modified_on	modified_by	recpath	CurrencyID	ConversionOpt	
                                //trx_type	CompanyID	ID	GstFlag	GstCode	ExRate	CurrencyIDComp	Remarks	claimstatus
                                //DropdownList drp=(DropdownList)item["tex_period"].FindControl(

                                DropDownList drptrxperiod  = (DropDownList)dataItem["trx_period"].FindControl("drptrxperiod");
                                DropDownList drptrx_type   =  (DropDownList)dataItem["trx_type1"].FindControl("drptrx_type");
                                DropDownList drpCurrencyID = (DropDownList)dataItem["CurrencyID1"].FindControl("drpCurrencyID");
                                DropDownList drpGstCode    = (DropDownList)dataItem["GstCode1"].FindControl("drpGstCode");
                                DropDownList drpBid = (DropDownList)dataItem["Bid1"].FindControl("drpBid");
                                //DropDownList drpGstFlag  = (DropDownList)dataItem["GstFlag1"].FindControl("drpGstFlag");
                                CheckBox chkGstFlag = (CheckBox)dataItem["GstFlag1"].FindControl("chkGst");

                                TextBox txtAmtGst = (TextBox)dataItem["ToatlWithGst1"].FindControl("txtAmtGst");
                                TextBox txtGstAmt = (TextBox)dataItem["GstAmnt1"].FindControl("txtGstAmt");
                                TextBox txtBefGst = (TextBox)dataItem["BefGst1"].FindControl("txtBefGst");
                                TextBox txtRemarks = (TextBox)dataItem["Remarks1"].FindControl("txtRemarks");
                                DropDownList drpReceipt = (DropDownList)dataItem["Recpyn1"].FindControl("drpReceipt");

                                dr1[0]["Recpyn"]        = drpReceipt.SelectedValue;
                                dr1[0]["trx_period"]    = drptrxperiod.SelectedValue;
                                dr1[0]["trx_period1"]   = drptrxperiod.SelectedValue;
                                dr1[0]["trx_type"]      = drptrx_type.SelectedValue;
                                dr1[0]["Bid"]           = drpBid.SelectedValue;
                                //if (dataItem["CurrencyID1"].Text != "")
                                //{
                                dr1[0]["CurrencyID"] = Convert.ToInt32(drpCurrencyID.SelectedValue);
                                //}
                                //else
                                //{
                                //    dr1[0]["CurrencyID"] = Convert.ToInt32(0);
                                //}
                                dr1[0]["GstCode"] = drpGstCode.SelectedValue;
                                dr1[0]["id"] = dataItem["id"].Text;
                                dr1[0]["emp_name"] = dataItem["emp_name"].Text;
                                dr1[0]["desc"] = dataItem["desc"].Text;
                                if (chkGstFlag.Checked)
                                {
                                    dr1[0]["GstFlag"] = "1";
                                }
                                else
                                {
                                    dr1[0]["GstFlag"] = "0";
                                }

                                dr1[0]["ToatlWithGst"] = Utility.ToString(Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text));
                                dr1[0]["GstAmnt"] = txtGstAmt.Text;
                                dr1[0]["BefGst"] = txtBefGst.Text;
                                if(dataItem["ExRate"].Text=="&nbsp;")
                                {
                                    dr1[0]["ExRate"]="1" ;
                                }
                                else
                                {
                                    dr1[0]["ExRate"]=dataItem["ExRate"].Text;
                                }
                                if (dataItem["CurrencyIDComp"].Text == "&nbsp;")
                                {
                                    dr1[0]["CurrencyIDComp"] = "1";
                                }
                                else
                                {
                                    dr1[0]["CurrencyIDComp"] = dataItem["CurrencyIDComp"].Text;
                                }
                                
                                dr1[0]["recpath"] = dataItem["recpath"].Text;
                                dr1[0]["Remarks"] = txtRemarks.Text;
                                dr1[0]["cpf"] = dataItem["cpf"].Text;
                                dr1[0]["emp_code"] = dataItem["emp_code"].Text;                                
                                dr1[0]["Department"] = dataItem["Department"].Text;
                                dr1[0]["Descriptions"] = dataItem["Descriptions"].Text;
                                dr1[0]["Designation"] = dataItem["Designation"].Text;
                                dr1[0]["emp_type"] = dataItem["emp_type"].Text;
                                dr1[0]["TimeCardId"] = dataItem["TimeCardId"].Text;
                                dr1[0]["Nationality"] = dataItem["Nationality"].Text;

                                if (dataItem["ConversionOpt"].Text == "&nbsp;")
                                {
                                    dr1[0]["ConversionOpt"] = "0";
                                }
                                else
                                {
                                    dr1[0]["ConversionOpt"] = dataItem["ConversionOpt"].Text;
                                }
                                dr1[0]["GLAcc"] = dataItem["GLAcc"].Text;
                                dr1[0]["created_on"] = dataItem["created_on"].Text;
                                dr1[0]["created_by"] = dataItem["created_by"].Text;
                                dr1[0]["modified_on"] = dataItem["modified_on"].Text;
                                dr1[0]["modified_by"] = dataItem["modified_by"].Text;
                                dr1[0]["CompanyID"] = dataItem["CompanyID"].Text;
                                dr1[0]["ID"] = dataItem["ID"].Text;
                                dr1[0]["claimstatus"] = dataItem["claimstatus"].Text;
                                dr1[0]["PayAmount"] = dataItem["PayAmount"].Text;
                                dr1[0]["TransId"] = dataItem["TransId"].Text;
                            }
                        }
                            dsAdd.AcceptChanges();
                            strPK1 = e.Item.Cells[4].Text;
                            //Add New Row In Dataset
                            
                            DataRow[] dt_Card;
                            dt_Card = dsAdd.Tables[0].Select("SrNo= "+ strPK1 +"");
                            if (dt_Card.Length > 0)
                            {
                                DataRow dr = dsAdd.Tables[0].NewRow();
                                Random rn = new Random();
                                dr["SrNo"] = -rn.Next();                                
                                dr["Recpyn"] = dt_Card[0]["Recpyn"];
                                dr["emp_name"] = dt_Card[0]["emp_name"];
                                dr["desc"] = dt_Card[0]["desc"];
                                dr["ExRate"] = dt_Card[0]["ExRate"];
                                dr["trx_type"] = dt_Card[0]["trx_type"];
                                dr["trx_period"] = dt_Card[0]["trx_period"];
                                dr["trx_period1"] = dt_Card[0]["trx_period1"];//dr1[0]["trx_period1"]
                                dr["CurrencyID"] = dt_Card[0]["CurrencyID"];
                                //dr["CurrencyID1"] = dt_Card[0]["CurrencyID"];
                                dr["GstCode"] = dt_Card[0]["GstCode"];
                                dr["Bid"] = dt_Card[0]["Bid"];
                                //dr["id"] = dt_Card[0]["id"];
                                //dr["emp_name"] = dt_Card[0]["emp_name"];
                                //dr["desc"] = dt_Card[0]["desc"];
                                dr["GstFlag"] = dt_Card[0]["GstFlag"];
                                dr["ToatlWithGst"] = dt_Card[0]["ToatlWithGst"];
                                dr["GstAmnt"] = dt_Card[0]["GstAmnt"];
                                dr["BefGst"] = dt_Card[0]["BefGst"];
                                dr["ExRate"] = dt_Card[0]["ExRate"];
                                dr["CurrencyIDComp"] = dt_Card[0]["CurrencyIDComp"];
                                dr["recpath"] = dt_Card[0]["recpath"];
                                dr["Remarks"] = dt_Card[0]["Remarks"];
                                dr["cpf"] = dt_Card[0]["cpf"];
                                dr["emp_code"] = dt_Card[0]["emp_code"];
                                dr["Department"] = dt_Card[0]["Department"];
                                dr["Descriptions"] = dt_Card[0]["Descriptions"];
                                dr["Designation"] = dt_Card[0]["Designation"];
                                dr["emp_type"] = dt_Card[0]["emp_type"];
                                dr["TimeCardId"] = dt_Card[0]["TimeCardId"];
                                dr["Nationality"] = dt_Card[0]["Nationality"];
                                dr["ConversionOpt"] = dt_Card[0]["ConversionOpt"];
                                dr["GLAcc"] = dt_Card[0]["GLAcc"];
                                dr["created_on"] = dt_Card[0]["created_on"];
                                dr["created_by"] = dt_Card[0]["created_by"];
                                dr["modified_on"] = dt_Card[0]["modified_on"];
                                dr["modified_by"] = dt_Card[0]["modified_by"];
                                dr["CompanyID"] = dt_Card[0]["CompanyID"];
                                dr["ID"] = dt_Card[0]["ID"];
                                dr["claimstatus"] = "Open";
                                dr["PayAmount"] = dt_Card[0]["PayAmount"];
                                dr["TransId"] = dt_Card[0]["TransId"];
                                //dr1[0]["TransId"] = dataItem["TransId"].Text;
                                //dr1[0]["PayAmount"]
                                dsAdd.Tables[0].Rows.Add(dr);
                            }

                            Session["DsClaim"] = dsAdd;
                            DataTable dstemp = new DataTable();
                            dstemp = dsAdd.Tables[0];
                            dstemp.DefaultView.Sort = "SrNo Desc";

                            RadGrid1.DataSource = dstemp;
                            RadGrid1.DataBind();
            }
            else if (e.CommandName == "Delete")
            {
                    flag = true;
                    DataSet dsDelete = new DataSet();
                    dsDelete = (DataSet)Session["DsClaim"];
                    string strPrimaryKeys ;
                    if (e.Item is GridDataItem)
                    {
                        if (dsDelete != null)
                        {
                            GridDataItem item = (GridDataItem)e.Item;
                            string pk = item["SrNo"].Text;
                            DataRow[] dr = dsDelete.Tables[0].Select("SrNo=" + pk);
                            if (dr.Length > 0)
                            {
                                //strPrimaryKeys.Append(pk);
                                dsDelete.Tables[0].Rows.Remove(dr[0]);
                                dsDelete.AcceptChanges();
                            }
                            Session["DsClaim"] = dsDelete;

                            DataTable dstemp = new DataTable();
                            dstemp = dsDelete.Tables[0];
                            dstemp.DefaultView.Sort = "SrNo Desc";

                            RadGrid1.DataSource = dstemp;
                            RadGrid1.DataBind();
                            //Delete the claims
                            if (Session["DeleteClaims"] != null)
                            {
                                strPrimaryKeys = (string)Session["DeleteClaims"];
                                strPrimaryKeys = strPrimaryKeys + "," + pk;
                            }
                            else
                            {
                                strPrimaryKeys=pk;
                            }
                            Session["DeleteClaims"] = strPrimaryKeys;
                        }

                    }
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                flag = true;
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
        private object _dataItem = null;

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
            string sSQL1 = "sp_GetPayrollProcessOn";

            SqlParameter[] parms1 = new SqlParameter[3];
            parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(drpemp.SelectedValue.ToString()));
            parms1[1] = new SqlParameter("@compid", compid);
            parms1[2] = new SqlParameter("@trxdate", "01/" + DropDownList1.SelectedValue.ToString() + "/" + cmbYear.SelectedValue.ToString());
            int conLock = 0;
            SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
            while (dr1.Read())
            {
                conLock = Utility.ToInteger(dr1.GetValue(0));
            }
            if (conLock <= 0)
            {
                this.errorMsg.Text = "";
            lblerror.Text = "";
            binddata();
             }
           
                 else
            {
               // Response.Write("<script type='Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
             this.errorMsg.Text="Payroll has been Processed, Action not allowed.";
            }
        }

        private  void binddata()
        {
            DataSet ds = new DataSet();
            SqlParameter[] parms1 = new SqlParameter[5];
            parms1[0] = new SqlParameter("@empcode", drpemp.SelectedValue);
            parms1[1] = new SqlParameter("@empmonth", DropDownList1.SelectedValue);
            parms1[2] = new SqlParameter("@empyear", cmbYear.SelectedValue);
           


            if (radSelect.SelectedValue == "N")
            {
                parms1[3] = new SqlParameter("@Flag", 1);
                parms1[4] = new SqlParameter("@transactionid", 1);
                ds = DataAccess.ExecuteSPDataSet("sp_emppayclaim_add_EXT", parms1);
                RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
               
            }
            else
            {
                if (drpRejected.Items.Count > 0)
                {
                    parms1[3] = new SqlParameter("@Flag", 2);
                    parms1[4] = new SqlParameter("@transactionid", Convert.ToInt32(drpRejected.SelectedValue));
                    ds = DataAccess.ExecuteSPDataSet("sp_emppayclaim_add_EXT", parms1);
                    RadGrid1.DataSource = ds;
                    RadGrid1.DataBind();
                }
            }
            //sp_emppayclaim_add_EXT
            
            //if (Session["DsClaim"] == null)
            //{
           
           
            Session["DsClaim"] = ds;
        
        }

        protected void RadGrid1_InsertCommand(object source, GridCommandEventArgs e)
        {
            //GridEditableItem editedItem = e.Item as GridEditableItem;
            //UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);

            //string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            //string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            //string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            //string approver = (userControl.FindControl("lblsupervisor") as Label).Text;
            //RadUpload uploader = (userControl.FindControl("RadUpload1")) as RadUpload;
            //string claimRemarks = (userControl.FindControl("claimRemark") as TextBox).Text; 
            //string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;
            //varFileName = "";
            //string uploadpath = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims";



            //if (uploader.UploadedFiles.Count != 0)
            //{
            //    if (Directory.Exists(Server.MapPath(uploadpath)))
            //    {
            //        if (File.Exists(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName()))
            //        {
            //            string sMsg = "File Already Exist";
            //            sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
            //            Response.Write(sMsg);
            //            return;
            //        }
            //        else
            //        {
            //            varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
            //            uploader.UploadedFiles[0].SaveAs(varFileName);
            //            varFileName = uploader.UploadedFiles[0].GetName();
            //        }
            //    }
            //    else

            //        Directory.CreateDirectory(Server.MapPath(uploadpath));
            //    varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
            //    uploader.UploadedFiles[0].SaveAs(varFileName);
            //    varFileName = uploader.UploadedFiles[0].GetName();
            //}

            //string claimstatus = "Open";

            //DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            //DateTime transperiod2 = (DateTime)(userControl.FindControl("RadDatePicker2") as RadDatePicker).SelectedDate;
            //int month = transperiod1.Month;
            //int year = transperiod2.Year;
            ////string sSQL1 = "sp_getLockAddition";
            ////SqlParameter[] parms1 = new SqlParameter[6];
            ////parms1[0] = new SqlParameter("@month1", Utility.ToInteger(transperiod1.Month));
            ////parms1[1] = new SqlParameter("@month2", Utility.ToInteger(transperiod2.Month));
            ////parms1[2] = new SqlParameter("@year1", Utility.ToInteger(transperiod1.Year));
            ////parms1[3] = new SqlParameter("@year2", Utility.ToInteger(transperiod2.Year));
            ////parms1[4] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
            ////parms1[5] = new SqlParameter("@compid", compid);
            //string sSQL1 = "sp_GetPayrollProcessOn";
            //SqlParameter[] parms1 = new SqlParameter[3];
            //parms1[0] = new SqlParameter("@empcode", Utility.ToInteger(empcode));
            //parms1[1] = new SqlParameter("@compid", compid);
            //parms1[2] = new SqlParameter("@trxdate", transperiod1.ToString("dd/MM/yyyy"));
            //int conLock = 0;
            //SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL1, parms1);
            //while (dr1.Read())
            //{
            //    conLock = Utility.ToInteger(dr1.GetValue(0));
            //}
            //if (conLock <= 0)
            //{
            //    int converison = 0;
            //    string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
            //    SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            //    while (drcon.Read())
            //    {
            //        if (drcon.GetValue(0) == null)
            //        {
            //            converison = 1;
            //        }
            //        else
            //        {
            //            if (drcon.GetValue(0).ToString() != "")
            //            {
            //                converison = Convert.ToInt32(drcon.GetValue(0).ToString());
            //            }
            //        }
            //    }
            //    // As per exchange rate change the Amount
            //    double exchangeRate = 1.0;
            //    if (converison == 2 || converison == 3)
            //    {
            //        string sd, ed = "";

            //        sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            //        //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
            //        string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + currencyID + " and [Date]<='" + sd + "'  Order by [Date] desc";

            //        SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
            //        while (drex.Read())
            //        {
            //            if (drex.GetValue(0) == null)
            //            {
            //                exchangeRate = 1.0;
            //            }
            //            else
            //            {
            //                exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
            //            }
            //        }
            //    }
            //    double claimamount=0.00;
            //    if(amount!="")
            //    {
            //        claimamount = Convert.ToDouble(amount) * exchangeRate;
            //    }
           
            //    int i = 0;
            //    SqlParameter[] parms = new SqlParameter[12];
            //    parms[i++] = new SqlParameter("@emp_code", Utility.ToInteger(empcode));
            //    parms[i++] = new SqlParameter("@trx_type", Utility.ToString(addtype));
            //    parms[i++] = new SqlParameter("@trx_period1", transperiod1);
            //    parms[i++] = new SqlParameter("@trx_period2", transperiod2);
            //    parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(claimamount));
            //    parms[i++] = new SqlParameter("@path", varFileName);
            //    parms[i++] = new SqlParameter("@approver", approver);
            //    parms[i++] = new SqlParameter("@claimstatus", claimstatus);
            //    parms[i++] = new SqlParameter("@compid", compid);
            //    parms[i++] = new SqlParameter("@claimRemark", claimRemarks.Replace("'",""));
            //    parms[i++] = new SqlParameter("@CurrencyID", Convert.ToInt32(currencyID));


            //    parms[i++] = new SqlParameter("@ConversionOpt", converison);

            //    string sSQL = "sp_empclaim_add";
            //    try
            //    {
            //        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
            //    }
            //    catch (Exception ex)
            //    {
            //        string ErrMsg = ex.Message;
            //        if (ErrMsg.IndexOf("PRIMARY KEY constraint", 1) > 0)
            //        {
            //            ErrMsg = "<font color = 'Red'>Unable to add the record.Please try again.</font>";
            //            RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Unable to add record. Reason:</font> " + ErrMsg));
            //            e.Canceled = true;
            //        }
            //    }
            //}
            //else
            //{
            //    Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
            //}
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblerror.Text = "";
            //Dataset
            DataSet dsfinal = new DataSet();
            dsfinal = (DataSet)Session["DsClaim"];
            string strpkdelete="";
            if (Session["DeleteClaims"] != null)
            {
                strpkdelete = (string)Session["DeleteClaims"];
            }
            Session["DsClaim"] = null;

            string addeditdelete = "";
            string strdelete = "";
            string stredit = "";
            string stradd = "";
            string strclaimadd = "";
            string strclaimedit = "";
            string claimdelete = "";

            string strMax = "Select Max(SrNo) from ClaimsExt";
            SqlDataReader dr1;
            int val = -1;

            dr1 = DataAccess.ExecuteReader(CommandType.Text, strMax, null);
            while (dr1.Read())
            {
                if (dr1[0] != null)
                {
                    if (val == -1)
                    {
                        if (dr1[0].ToString() == "")
                        {
                            val = 0;
                        }
                        else
                        {
                            val = Convert.ToInt32(dr1[0].ToString()) + 1;
                        }
                    }
                    else
                    {
                        val = val + 1;
                    }
                }
            }
            Random random = new Random();
            int TransId = random.Next();

            SqlConnection db = new SqlConnection(Constants.CONNECTION_STRING);
            SqlTransaction transaction;
            db.Open();
            transaction = db.BeginTransaction();

            varFileName = "";
            string uploadpath = "../" + "Documents" + "/" + compid + "/" + drpemp.SelectedValue + "/" + "Claims";

            //RadUpload uploader = radClaimsUpload;

            Random rnd = new Random();
            if (radClaimsUpload.UploadedFiles.Count != 0)
            {
                varFileName =  "_" + rnd.Next()+radClaimsUpload.UploadedFiles[0].GetName() ;
            }
            //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName() + "_" + rnd.Next();
           
            try
            {
                foreach (DataRow dr in dsfinal.Tables[0].Rows)
                {
                    string today = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
                    string strTrans = Convert.ToDateTime(dr["trx_period"].ToString()).Month + "/" + Convert.ToDateTime(dr["trx_period"].ToString()).Day + "/" + Convert.ToDateTime(dr["trx_period"].ToString()).Year;
                    
                    if (Convert.ToDouble(dr["SrNo"]) < 0)
                    {
                       
                       stradd = @"INSERT INTO ClaimsExt ( [emp_code],[Desc],[GLAcc],[created_on],[created_by],[modified_on],[modified_by],[recpath]" +
                            ",[CurrencyID],[ConversionOpt],[trx_type],[trx_period],[CompanyID],[ID],[ToatlWithGst]" +
                            ",[ToatlBefGst],[GstAmnt],[GstFlag],[GstCode],[ExRate],[CurrencyIDComp],[Remarks],[claimstatus],[PayAmount],[TransId],[Recpyn],[Bid])VALUES(" +
                            dr["emp_code"].ToString() + ",'" + dr["desc"].ToString().Replace("'","") + "','" + dr["GLAcc"].ToString() + "','" + today + "','" + Session["EmpCode"].ToString() + "','" + today + "','" +
                            Session["EmpCode"].ToString() + "','" + varFileName + "'," + dr["CurrencyID"].ToString() + "," + dr["ConversionOpt"].ToString() + "," + dr["trx_type"] + ",'" +
                            strTrans + "'," + dr["CompanyID"] + "," + dr["trx_type"] + "," + dr["ToatlWithGst"].ToString() + "," + dr["BefGst"].ToString() + "," + dr["GstAmnt"].ToString() + "," + dr["GstFlag"].ToString() + "," +
                            dr["GstCode"].ToString() + "," + dr["ExRate"].ToString() + "," + dr["CurrencyIDComp"].ToString() + ",'" + dr["Remarks"].ToString() + "','" + "Open" + "'," + dr["PayAmount"].ToString() + "," + TransId + "," + dr["Recpyn"].ToString() + "," + dr["Bid"].ToString() + ")";
                            //Recpyn
                            new SqlCommand(stradd, db, transaction).ExecuteNonQuery();
                            //Get New Value from database for ClaimsExt
//////////////// km 
                            val = val + 1;
                            strclaimadd = @"INSERT INTO [emp_additions] ([emp_code], [trx_type], [trx_period], [trx_amount], [recpath], [approver], [claimstatus],[remarks],[CurrencyID],[ConversionOpt],[ClaimExt],[amount])VALUES 
                                    (" + dr["emp_code"].ToString() + "," + dr["trx_type"] + ",'" + strTrans + "'," + dr["PayAmount"].ToString() + ",'" + "" + "','" + "1" + "','" + "Pending" + "','" + dr["Remarks"].ToString() + "'," + dr["CurrencyID"].ToString() + "," + dr["ConversionOpt"].ToString() + "," + val + "," + dr["PayAmount"].ToString() + ")";

                            //new SqlCommand(strclaimadd, db, transaction).ExecuteNonQuery();
////////////////////

                        //}
                        //                        //else
                        //                        {
                        //                            stradd = stradd + ";" + @"INSERT INTO ClaimsExt ( [emp_code],[Desc],[GLAcc],[created_on],[created_by],[modified_on],[modified_by],[recpath]" +
                        //                                    ",[CurrencyID],[ConversionOpt],[trx_type],[trx_period],[CompanyID],[ID],[ToatlWithGst]" +
                        //                                    ",[ToatlBefGst],[GstAmnt],[GstFlag],[GstCode],[ExRate],[CurrencyIDComp],[Remarks],[claimstatus],[PayAmount])VALUES(" +
                        //                                    dr["emp_code"].ToString() + ",'" + dr["desc"] + "','" + dr["GLAcc"].ToString() + "','" + today + "','" + Session["EmpCode"].ToString() + "','" + today + "','" +
                        //                                    Session["EmpCode"].ToString() + "','" + "Recepit" + "'," + dr["CurrencyID"].ToString() + "," + dr["ConversionOpt"].ToString() + "," + dr["trx_type"] + ",'" +
                        //                                    strTrans + "'," + dr["CompanyID"] + "," + dr["trx_type"] + "," + dr["ToatlWithGst"].ToString() + "," + dr["BefGst"].ToString() + "," + dr["GstAmnt"].ToString() + "," + dr["GstFlag"].ToString() + "," +
                        //                                    dr["GstCode"].ToString() + "," + dr["ExRate"].ToString() + "," + dr["CurrencyIDComp"].ToString() + "," + dr["Remarks"] + ",'" + "Open" + "'," + dr["PayAmount"].ToString() + ")";

                        //                            strclaimadd = @"INSERT INTO [emp_additions] ([emp_code], [trx_type], [trx_period], [trx_amount], [recpath], [approver], [claimstatus],[remarks],[CurrencyID],[ConversionOpt],[ClaimExt])VALUES 
                        //                                    (" + dr["emp_code"].ToString() + "," + dr["trx_type"] + ",'" + strTrans + "','" + dr["PayAmount"].ToString() + ",'" + "1" + "','" + "Open" + ",'" + dr["Remarks"].ToString() + "'," + dr["CurrencyID"].ToString() + "," + dr["ConversionOpt"].ToString() + "," + val + ")";
                        //                        }
                    }
                    else if (Convert.ToDouble(dr["SrNo"]) > 0)
                    {
                        ////Edit
                        //if (stredit == "")
                        //{
                            stredit = @"UPDATE [ClaimsExt] " +
                                      "SET [emp_code] = " + dr["emp_code"].ToString() +
                                      ",[Desc] = '" + dr["desc"].ToString().Replace("'", "") +
                                      "',[GLAcc] ='" + dr["GLAcc"].ToString() +
                                      "',[created_on] ='" + today +
                                      "',[created_by] ='" + Session["EmpCode"].ToString() +
                                      "',[modified_on]='" + today +
                                      "',[modified_by]  ='" + Session["EmpCode"].ToString() +
                                      "',[recpath] = '" + varFileName +
                                      "',[CurrencyID] = " + dr["CurrencyID"].ToString() +
                                      ",[ConversionOpt]=" + dr["ConversionOpt"].ToString() +
                                      ",[trx_type]=" + dr["trx_type"].ToString() +
                                      ",[trx_period] = '" + strTrans +
                                      "',[CompanyID] =" + dr["CompanyID"].ToString() +
                                      ",[ID]  =" + dr["ID"].ToString() +
                                      ",[ToatlWithGst]   =" + dr["ToatlWithGst"].ToString() +
                                      ",[ToatlBefGst] =" + dr["BefGst"].ToString() +
                                      ",[GstAmnt] =" + dr["GstAmnt"].ToString() +
                                      ",[GstFlag] =" + dr["GstFlag"].ToString() +
                                      ",[GstCode] =" + dr["GstCode"].ToString() +
                                      ",[ExRate]  =" + dr["ExRate"].ToString() +
                                      ",[CurrencyIDComp] =" + dr["CurrencyIDComp"].ToString() +
                                      ",[Remarks]  ='" + dr["Remarks"].ToString() +
                                      "',[claimstatus] ='" + "Pending" +
                                      "',[PayAmount] = " + dr["PayAmount"].ToString() +
                                      ",[TransId] = " + TransId +
                                      ",[Recpyn] = " + dr["Recpyn"].ToString() +
                                      ",[Bid] = " + dr["Bid"].ToString() +  
                                      " WHERE SrNo=" + dr["SrNo"].ToString();
                        
                            new SqlCommand(stredit, db, transaction).ExecuteNonQuery();

                            strclaimedit = @"UPDATE [emp_additions]" +
                                           " SET [trx_type] = " + dr["trx_type"].ToString() +
                                           " ,[trx_period] = '" + strTrans + "'" +
                                           " ,[approver]= '" + "1" + "'" +
                                           " ,[trx_amount] = " + dr["PayAmount"].ToString() +
                                             " ,[amount] = " + dr["PayAmount"].ToString() +
                                           " ,[recpath] = '" + varFileName +
                                           " ',[CurrencyID] = " + dr["CurrencyID"].ToString() +
                                           " ,[ConversionOpt] = " + dr["ConversionOpt"].ToString() +
                                            " ,[claimstatus] = '" + "Pending" +
                                           "',[TransId] = " + TransId + 
                                           " Where[ClaimExt] = " + dr["SrNo"].ToString();
                            new SqlCommand(strclaimedit, db, transaction).ExecuteNonQuery();
                        //}
                        //else
                        //{
                        //    stredit = stredit + ";" + @"UPDATE [ClaimsExt] " +
                        //              "SET [emp_code] = " + dr["emp_code"].ToString() +
                        //              ",[Desc] = '" + dr["desc"] +
                        //              "',[GLAcc] ='" + dr["GLAcc"].ToString() +
                        //              "',[created_on] ='" + today +
                        //              "',[created_by] ='" + Session["EmpCode"].ToString() +
                        //              "',[modified_on]='" + today +
                        //              "',[modified_by]  ='" + Session["EmpCode"].ToString() +
                        //              "',[recpath] = '" + "AA" +
                        //              "',[CurrencyID] = " + dr["CurrencyID"].ToString() +
                        //              ",[ConversionOpt]=" + dr["ConversionOpt"].ToString() +
                        //              ",[trx_type]=" + dr["trx_type"].ToString() +
                        //              ",[trx_period] = '" + strTrans +
                        //              "',[CompanyID] =" + dr["CompanyID"].ToString() +
                        //              ",[ID]  =" + dr["ID"].ToString() +
                        //              ",[ToatlWithGst]   =" + dr["ToatlWithGst"].ToString() +
                        //              ",[ToatlBefGst] =" + dr["BefGst"].ToString() +
                        //              ",[GstAmnt] =" + dr["GstAmnt"].ToString() +
                        //              ",[GstFlag] =" + dr["GstFlag"].ToString() +
                        //              ",[GstCode] =" + dr["GstCode"].ToString() +
                        //              ",[ExRate]  =" + dr["ExRate"].ToString() +
                        //              ",[CurrencyIDComp] =" + dr["CurrencyIDComp"].ToString() +
                        //              ",[Remarks]  ='" + dr["Remarks"].ToString() +
                        //              "',[claimstatus] ='" + "Open" +
                        //              "',[PayAmount] = " + dr["PayAmount"].ToString() +
                        //              " WHERE SrNo=" + dr["SrNo"].ToString();
                        //}
                    }
                    //Delete
                }
                char sep = ',';
                string[] data = strpkdelete.Split(sep);
                for (int ctr = 0; ctr <= data.Length-1; ctr++)
                {
                    //if (strdelete == "")
                    //{
                    if (data[ctr].ToString() != "")
                    {
                        strdelete = "DELETE FROM [ClaimsExt]  WHERE SrNo=" + data[ctr].ToString();
                        new SqlCommand(strdelete, db, transaction).ExecuteNonQuery();
                        string strdelete1 = "DELETE FROM [emp_additions]  WHERE claimext=" + data[ctr].ToString();
                        new SqlCommand(strdelete1, db, transaction).ExecuteNonQuery();
                    }
                    //}
                    //else
                    //{
                    //    strdelete = strdelete + ";" + "DELETE FROM [ClaimsExt]  WHERE SrNo=" + strpkdelete[ctr].ToString();
                    //}
                    //strpkdelete[ctr]
                }


                
                transaction.Commit();
                sendemail(TransId);

                binddata();
                lblerror.Text = "Claims Submited Successfully";

                if (radSelect.SelectedValue == "R")
                {
                    FillDropdown();
                }

                //File Upload


                varFileName = Server.MapPath(uploadpath) + "/" + varFileName;

                if (radClaimsUpload.UploadedFiles.Count != 0)
                {
                    if (Directory.Exists(Server.MapPath(uploadpath)))
                    {
                        if (File.Exists(Server.MapPath(uploadpath) + "/" + radClaimsUpload.UploadedFiles[0].GetName()))
                        {
                            string sMsg = "File Already Exist";
                            sMsg = "<SCRIPT language='Javascript'>alert('" + sMsg + "');</SCRIPT>";
                            Response.Write(sMsg);
                            return;
                        }
                        else
                        {
                            radClaimsUpload.UploadedFiles[0].SaveAs(varFileName);
                            //varFileName = radClaimsUpload.UploadedFiles[0].GetName();
                        }
                    }
                    else

                    Directory.CreateDirectory(Server.MapPath(uploadpath));
                    //varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
                    radClaimsUpload.UploadedFiles[0].SaveAs(varFileName);
                    //varFileName = radClaimsUpload.UploadedFiles[0].GetName();
                }
            }

            catch (SqlException ex)
            {
                transaction.Rollback();
                db.Close();
            }
            db.Close();
        }

        protected void sendemail(int trx_id)
        {
            bool returnresult;
            string code = drpemp.SelectedValue;
            int companyid = Utility.ToInteger(Session["Compid"]);
            string from = "";
            string to = "";
            string SMTPserver = "";
            string SMTPUser = "";
            string SMTPPass = "";
            int SMTPPORT = 25;
            string emp_name = "";
            string emailreq = "";
            string body = "";
            string month = "";
            string year = "";
            string cc = "";
            int transectionid=0;

            string sql9 = "select datename(month,trx_period),year(trx_period),trx_id from emp_additions where TransId='" + Utility.ToInteger(trx_id) + "'";
            SqlDataReader dr9 = DataAccess.ExecuteReader(CommandType.Text, sql9, null);
            while (dr9.Read())
            {
                month = Utility.ToString(dr9.GetValue(0));
                year = Utility.ToString(dr9.GetValue(1));
                transectionid = Utility.ToInteger(dr9.GetValue(2));
            }
          
            string sSQLemail = "sp_sendclaim_email";
            SqlParameter[] parmsemail = new SqlParameter[2];
            parmsemail[0] = new SqlParameter("@empcode", Utility.ToInteger(code));
            parmsemail[1] = new SqlParameter("@compid", Utility.ToInteger(companyid));
            SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQLemail, parmsemail);
            while (dr3.Read())
            {
                from = Utility.ToString(dr3.GetValue(15));
                to = Utility.ToString(dr3.GetValue(2));
                SMTPserver = Utility.ToString(dr3.GetValue(6));
                SMTPUser = Utility.ToString(dr3.GetValue(7));
                SMTPPass = Utility.ToString(dr3.GetValue(8));
                emp_name = Utility.ToString(dr3.GetValue(5));
                body = Utility.ToString(dr3.GetValue(10));
                SMTPPORT = Utility.ToInteger(dr3.GetValue(13));
                emailreq = Utility.ToString(dr3.GetValue(16)).ToLower();
                cc = Utility.ToString(dr3.GetValue(17));

            }
            if (emailreq == "yes")
            {
                string subject = "Claim Request By" + " " + emp_name;
                body = body.Replace("@emp_name", emp_name);
                body = body.Replace("@month", month);
                body = body.Replace("@year", year);

                #region Added accept and reject button below
                //check whether needed approve and reject button
                SqlDataReader dr_chk = DataAccess.ExecuteReader(CommandType.Text, "select top 1 [Enable],[PrimaryAddress],[SecondaryAddress] from EmailApproval where company_id='" + Utility.ToInteger(Session["Compid"]) + "' and ClaimsEnable='1'  ", null);
                if (dr_chk.HasRows)
                {
                    if (dr_chk.Read())
                    {
                        ApproveNeeded = Convert.ToInt32(dr_chk[0]);
                        PrimaryAddress = Convert.ToString(dr_chk[1]);
                        SecondaryAddress = Convert.ToString(dr_chk[2]);
                    }
                    if (ApproveNeeded == 1)
                    {
                        string host;
                        if (PrimaryAddress != "")
                        {
                            host = PrimaryAddress;
                        }
                        else
                        {
                            host = SecondaryAddress;
                        }
                        //string host = "http://localhost:1379/";


                        string url = host + "Leaves/EmailClaimApprove.aspx?emp=" + code + "&trx_id=";

                        //
                        trx = transectionid;
                        //

                        string url_approve = url + trx + "&status=approve";
                        string url_reject = url + trx + "&status=reject";

                        body = body + "<br/><br/>\n\n  <a href=\"" + url_approve + "\">ACCEPT</ID></a> &nbsp;  or &nbsp;   \n\n    <a href=\"" + url_reject + "\">REJECT</ID></a>";
                    }
                }
                #endregion

                SMEPayroll.Model.ANBMailer oANBMailer = new SMEPayroll.Model.ANBMailer(companyid);

                #region Attachment

                string ssql_path = "select recpath from  emp_additions where trx_id='" + transectionid + "'";
                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, ssql_path, null);
                    if (dr.Read())
                    {
                        
                        filename = dr[0].ToString();
                    }
                    string uploadpath = "../" + "Documents" + "/" + compid + "/" + code + "/" + "Claims/";
                    string repath = uploadpath + filename;
                    if (filename != "")
                    {
                        oANBMailer.Attachment = Server.MapPath(repath);
                    }
                #endregion

                oANBMailer.Subject = subject;
                oANBMailer.MailBody = body;
                oANBMailer.From = from;
                oANBMailer.To = to;
                oANBMailer.Cc = cc;

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


        protected void RadGrid1_UpdateCommand(object source, GridCommandEventArgs e)
        {
            //GridEditableItem editedItem = e.Item as GridEditableItem;
            //UserControl userControl = (UserControl)e.Item.FindControl(GridEditFormItem.EditFormUserControlID);
            //object id = editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"];
            //string TrxId = id.ToString();
            //string sSQL = "sp_empclaim_update";
            //string empcode = (userControl.FindControl("drpemployee") as DropDownList).SelectedItem.Value;
            //string addtype = (userControl.FindControl("drpaddtype") as DropDownList).SelectedItem.Value;
            //DateTime transperiod1 = (DateTime)(userControl.FindControl("RadDatePicker1") as RadDatePicker).SelectedDate;
            //string amount = (userControl.FindControl("txtamt") as TextBox).Text;
            //string approver = (userControl.FindControl("lblsupervisor") as Label).Text;
            //RadUpload uploader = (userControl.FindControl("RadUpload1")) as RadUpload;
            //varFileName = "";
            //string ssqlget = "select recpath from emp_additions where trx_id='" + TrxId + "'";
            //DataSet dsget = new DataSet();
            //dsget = DataAccess.FetchRS(CommandType.Text, ssqlget, null);
            //if (dsget.Tables[0].Rows.Count > 0)
            //{
            //    string s = dsget.Tables[0].Rows[0]["recpath"].ToString();
            //    varFileName = s;

            //}
            //string currencyID = (userControl.FindControl("drpCurrency") as DropDownList).SelectedItem.Value;

            //string uploadpath = "../" + "Documents" + "/" + compid + "/" + empcode + "/" + "Claims";
            //if (uploader.UploadedFiles.Count != 0)
            //{
            //    if (Directory.Exists(Server.MapPath(uploadpath)))
            //    {
            //        if (File.Exists(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName()))
            //        {
            //            File.Delete(Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName());
            //            varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
            //            uploader.UploadedFiles[0].SaveAs(varFileName);
            //            varFileName = uploader.UploadedFiles[0].GetName();
            //        }
            //        else
            //        {
            //            varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
            //            uploader.UploadedFiles[0].SaveAs(varFileName);
            //            varFileName = uploader.UploadedFiles[0].GetName();
            //        }
            //    }
            //    else
            //    {
            //        Directory.CreateDirectory(Server.MapPath(uploadpath));
            //        varFileName = Server.MapPath(uploadpath) + "/" + uploader.UploadedFiles[0].GetName();
            //        uploader.UploadedFiles[0].SaveAs(varFileName);
            //        varFileName = uploader.UploadedFiles[0].GetName();
            //    }
            //}
            //int converison = 0;
            //string strcon = "Select ConversionOpt FROM Company where company_id=" + compid;
            //SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

            //while (drcon.Read())
            //{
            //    if (drcon.GetValue(0) == null)
            //    {
            //        converison = 1;
            //    }
            //    else
            //    {
            //        if (drcon.GetValue(0).ToString() != "")
            //        {
            //            converison = Convert.ToInt32(drcon.GetValue(0).ToString());
            //        }
            //    }
            //}

            //// As per exchange rate change the Amount
            //double exchangeRate = 1.0;
            //if (converison == 2 || converison == 3)
            //{
            //    string sd, ed = "";

            //    sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
            //    //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
            //    string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + currencyID + " and [Date]<='" + sd + "'  Order by [Date] desc";

            //    SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
            //    while (drex.Read())
            //    {
            //        if (drex.GetValue(0) == null)
            //        {
            //            exchangeRate = 1.0;
            //        }
            //        else
            //        {
            //            exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
            //        }
            //    }
            //    Response.Write("<script language = 'Javascript'>alert('You can noy Update Record.Delete it and add again.');</script>");
            //}
            //else
            //{
            //    int i = 0;
            //    SqlParameter[] parms = new SqlParameter[8];
            //    parms[i++] = new SqlParameter("@trx_id", Utility.ToInteger(TrxId));
            //    parms[i++] = new SqlParameter("@trx_amount", Utility.ToDouble(amount));
            //    parms[i++] = new SqlParameter("@trx_type", Utility.ToInteger(addtype));
            //    parms[i++] = new SqlParameter("@trx_period", Utility.ToString(transperiod1));
            //    parms[i++] = new SqlParameter("@rec_path", Utility.ToString(varFileName));
            //    parms[i++] = new SqlParameter("@approver", Utility.ToString(approver));
            //    parms[i++] = new SqlParameter("@CurrencyID", Utility.ToInteger(currencyID));
            //    parms[i++] = new SqlParameter("@ConversionOpt", converison);
            //    /* Check Status for Lock Record */
            //    string sSQLCheck = "select status from emp_additions where trx_id = {0}";
            //    sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
            //    string status = "";
            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
            //    while (dr.Read())
            //    {
            //        status = Utility.ToString(dr.GetValue(0));
            //    }
            //    if (status == "U")
            //    {
            //        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
            //        if (retVal == 1)
            //        {
            //            Response.Write("<script language = 'Javascript'>alert('Information Updated Successfully.');</script>");
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
            //    }

            //}
        }


        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                DropDownList drptrxperiod = (DropDownList)item["trx_period"].FindControl("drptrxperiod");
                DropDownList drptrx_type = (DropDownList)item["trx_type1"].FindControl("drptrx_type");
                DropDownList drpCurrencyID = (DropDownList)item["CurrencyID1"].FindControl("drpCurrencyID");
                DropDownList drpGstCode = (DropDownList)item["GstCode1"].FindControl("drpGstCode");
                DropDownList drpBid = (DropDownList)item["Bid1"].FindControl("drpBid");
                
                CheckBox chkGstFlag= (CheckBox)item["GstFlag1"].FindControl("chkGst");

                TextBox txtAmtGst = (TextBox)item["ToatlWithGst1"].FindControl("txtAmtGst");
                TextBox txtGstAmt = (TextBox)item["GstAmnt1"].FindControl("txtGstAmt");
                TextBox txtBefGst = (TextBox)item["BefGst1"].FindControl("txtBefGst");
                TextBox txtRemarks = (TextBox)item["Remarks1"].FindControl("txtRemarks");

                DropDownList drpReceipt = (DropDownList)item["Recpyn1"].FindControl("drpReceipt");
                //txtAmtGst.Text = item["AmtGst"].Text;
                if (item["GstFlag"].Text == "1")
                {
                    chkGstFlag.Checked = true;
                }
                else
                {
                    chkGstFlag.Checked = false;
                }
                if (chkGstFlag.Checked == false)
                {
                    drpGstCode.Enabled = false;
                   // txtGstAmt.Enabled = false;
                }
                else
                {
                    drpGstCode.Enabled = true;
                    //txtGstAmt.Enabled = true;
                }

                string sSQL ="";
                string date = @" Case when  Len(CONVERT(CHAR(2), DATEPART(dd, [DateInYear])))<=1 THEN  '0' + CONVERT(CHAR(1), DATEPART(dd,[DateInYear]))  ELSE CONVERT(CHAR(2), DATEPART(dd, [DateInYear]))  END " 
			                +"+ '/' +Case when Len( CONVERT(CHAR(2), DATEPART(mm, [DateInYear])))<=1 Then '0' +   CONVERT(CHAR(1), DATEPART(mm, [DateInYear])) ELSE  CONVERT(CHAR(2), DATEPART(mm, [DateInYear])) END+ "
			                +" '/' + CONVERT(CHAR(4), DATEPART(yy,[DateInYear]))";
                if (DropDownList1.SelectedValue != "13")
                {
                    //	Case when  Len(CONVERT(CHAR(2), DATEPART(dd, e.[trx_period])))<=1 THEN  '0' + CONVERT(CHAR(1), DATEPART(dd, e.[trx_period]))  ELSE CONVERT(CHAR(2), DATEPART(dd, e.[trx_period]))  END  
					//+ '/' +Case when Len( CONVERT(CHAR(2), DATEPART(mm, e.[trx_period])))<=1 Then '0' +   CONVERT(CHAR(1), DATEPART(mm, e.[trx_period])) ELSE  CONVERT(CHAR(2), DATEPART(mm, e.[trx_period])) END 
					//+ '/' + CONVERT(CHAR(4), DATEPART(yy,e.[trx_period]))
                    sSQL = "SELECT " + date + " Tsdate   FROM  Dateinyear  Where year(DateInYear)=" + cmbYear.SelectedValue + " and month(DateInYear)=" + DropDownList1.SelectedValue;
                }
                else
                {
                    //sSQL = "SELECT CONVERT(CHAR(2), DATEPART(dd, [DateInYear])) + '/' + CONVERT(CHAR(2), DATEPART(mm, [DateInYear])) + '/' + CONVERT(CHAR(4), DATEPART(yy,[DateInYear])) Tsdate   FROM  Dateinyear  Where year(DateInYear)=" + cmbYear.SelectedValue;
                    sSQL = "SELECT "  +  date + " Tsdate   FROM  Dateinyear  Where year(DateInYear)=" + cmbYear.SelectedValue;
                }

                SqlDataSource6.SelectCommand = sSQL;

                //ku change
                drptrxperiod.Items.Clear();
                drptrxperiod.SelectedIndex = -1;
                drptrxperiod.SelectedValue = null;
                drptrxperiod.ClearSelection();  
                //

                drptrxperiod.DataSourceID = "SqlDataSource6";
                drptrxperiod.DataTextField = "Tsdate";
                drptrxperiod.DataValueField = "Tsdate";
                drptrxperiod.DataBind();

                drptrxperiod.SelectedValue = item["trx_period1"].Text.Trim();


                DataSet dscost = new DataSet();
                string sqlCost= "Select * from Cost_BusinessUnit   where company_id=" + compid;
                dscost = DataAccess.FetchRS(CommandType.Text, sqlCost, null);
                drpBid.DataSource = dscost.Tables[0];
                drpBid.DataTextField = dscost.Tables[0].Columns["BusinessUnit"].ColumnName.ToString();
                drpBid.DataValueField = dscost.Tables[0].Columns["Bid"].ColumnName.ToString();
                drpBid.DataBind();
                if (item["Bid"].Text == "0")
                {
                    if(dscost.Tables.Count>0)
                    {
                        item["Bid"].Text = dscost.Tables[0].Rows[0][0].ToString();
                    }                    
                }
                else
                {
                    drpBid.SelectedValue = item["Bid"].Text;
                }




                DataSet ds_additiontype = new DataSet();
                if (compid == 1)
                    sSQL = "SELECT [id], [desc] FROM [additions_types] where optionselection='Claim' and (company_id=-1 or company_id='" + compid + "')";
                else
                    sSQL = "SELECT [id], [desc] FROM [additions_types] where optionselection='Claim' and company_id=" + compid;
                ds_additiontype = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drptrx_type.DataSource = ds_additiontype.Tables[0];
                drptrx_type.DataTextField = ds_additiontype.Tables[0].Columns["desc"].ColumnName.ToString();
                drptrx_type.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
                drptrx_type.DataBind();

                if (item["trx_type"].Text == "0" || item["trx_type"].Text == "&nbsp;")
                {
                }
                else
                {
                    drptrx_type.SelectedValue = item["trx_type"].Text;
                }
                drpCurrencyID.Items.Clear();
                drpCurrencyID.SelectedIndex = -1;
                drpCurrencyID.SelectedValue = null;
                drpCurrencyID.ClearSelection();  
                DataSet dscurr = new DataSet();
                string sqlCurr = "Select id, Currency Curr from currency";
                dscurr = DataAccess.FetchRS(CommandType.Text, sqlCurr, null);
                drpCurrencyID.DataSource = dscurr.Tables[0];
                drpCurrencyID.DataTextField = dscurr.Tables[0].Columns["Curr"].ColumnName.ToString();
                drpCurrencyID.DataValueField = dscurr.Tables[0].Columns["id"].ColumnName.ToString();
                drpCurrencyID.DataBind();

                string compCurr = "select currency from company where Company_Id=" + compid;
                SqlDataReader drComp = DataAccess.ExecuteReader(CommandType.Text, compCurr, null);
                while (drComp.Read())
                {
                    if (drComp.GetValue(0) == null)
                    {
                        compCurr = "";
                    }
                    else 
                    {
                        compCurr = drComp.GetValue(0).ToString();                    
                    }
                }

               if (item["CurrencyID"].Text == "0")
                {
                    if (compCurr != "0" && compCurr != "" && compCurr != "{}")
                    {
                        drpCurrencyID.SelectedValue = compCurr.ToString();
                    }
                }
                else
                {
                    drpCurrencyID.SelectedValue = item["CurrencyID"].Text;
                }


                if (item["ToatlWithGst"].Text == "&nbsp;" || item["ToatlWithGst"].Text == "")
                {
                    txtAmtGst.Text = "0.00";
                }
                else
                {
                    txtAmtGst.Text = item["ToatlWithGst"].Text;
                }// Utility.ToString(Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text));                 
                if (item["GstAmnt"].Text == "&nbsp;" || item["GstAmnt"].Text == "")
                {
                    txtGstAmt.Text = "0.00";
                }
                else
                {
                    txtGstAmt.Text = item["GstAmnt"].Text;
                }
                if (item["BefGst"].Text == "&nbsp;" || item["BefGst"].Text == "")
                {
                    txtBefGst.Text = "0.00";
                }
                else
                {
                    txtBefGst.Text = item["BefGst"].Text;
                }
                if (item["Remarks"].Text == "" || item["Remarks"].Text == "&nbsp;")
                {
                    txtRemarks.Text ="";
                }
                else
                {
                    txtRemarks.Text = item["Remarks"].Text;
                }
                //select Code,Desc from GstMaster

                DataSet dsgstcode = new DataSet();
                string sqlGstcode = "select Code,SrNo from GstMaster Where CompanyID=" + compid;
                dsgstcode = DataAccess.FetchRS(CommandType.Text, sqlGstcode, null);
                drpGstCode.DataSource = dsgstcode.Tables[0];
                drpGstCode.DataTextField = dsgstcode.Tables[0].Columns["Code"].ColumnName.ToString();
                drpGstCode.DataValueField = dsgstcode.Tables[0].Columns["SrNo"].ColumnName.ToString();
                drpGstCode.DataBind();

                if (item["GstCode"].Text == "0" || item["GstCode"].Text == "&nbsp;")
                {
                
                }
                else
                {
                    drpGstCode.SelectedValue = item["GstCode"].Text;
                }
                if(item["Recpyn"].Text=="" || item["Recpyn"].Text == "&nbsp;")
                {
                    drpReceipt.SelectedValue = "0";
                }
                else
                {
                    drpReceipt.SelectedValue=item["Recpyn"].Text;
                }
                //item.Selected = true;
                //if (txtBefGst.Text != "&nbsp;" && txtBefGst.Text != "" )
                //{
                //    valBefGst1 = valBefGst1 + Convert.ToDouble(txtBefGst.Text);
                //}
                //if (txtAmtGst.Text != "&nbsp;" && txtAmtGst.Text != "" )
                //{
                //    valAmtGst = valAmtGst + Convert.ToDouble(txtAmtGst.Text);
                //}
                //if (item["PayAmount"].Text != "&nbsp;" && item["PayAmount"].Text != "" )
                //{
                //    payamount = payamount + Convert.ToDouble(item["PayAmount"].Text);
                //}

                if (item["claimstatus"].Text == "Rejected")
                {
                    item.BorderStyle = BorderStyle.Groove;
                    item.ForeColor = System.Drawing.Color.IndianRed;
                    //item.ToolTip = "Claim Status : Rejected";
                    item["emp_name"].ToolTip = "Claim Status : Rejected";
                }
                else
                {
                    //item.ToolTip = "Claim Status Open";
                    item["emp_name"].ToolTip = "Claim Status :Open";
                }

                //Get GST Percentagae amount 
                
                string sqlGstcode1 = "select GstPer from GstMaster WHERE SrNo =" + drpGstCode.SelectedValue;
                dsgstcode = DataAccess.FetchRS(CommandType.Text, sqlGstcode1, null);
                decimal gstPercentage = 0;
                if (dsgstcode.Tables.Count > 0)
                {
                    if (dsgstcode.Tables[0].Rows.Count > 0)
                    {
                        gstPercentage = Convert.ToDecimal(dsgstcode.Tables[0].Rows[0]["GstPer"].ToString());
                    }
                }
                if (chkGstFlag.Checked)
                {
                    item["GstAmnt1"].ToolTip = "{Gst Amount  =  " + Utility.ToDouble(txtBefGst.Text).ToString() + " X (" + Utility.ToDouble(gstPercentage).ToString() + " /100 ) = " + txtGstAmt.Text + " }";
                    item["GstAmnt"].ToolTip = "{Gst Amount  =  " + Utility.ToDouble(txtBefGst.Text).ToString() + " X (" + Utility.ToDouble(gstPercentage).ToString() + " /100 ) = " + txtGstAmt.Text + " }";
                }
                //flag = false;
            }

            if (e.Item is GridFooterItem)
            {
                //if (flag == true)
                //{
                //    valBefGst1 = valBefGst1 / 2.00;
                //}                
                valBefGst1 = 0.00;
                payamount = 0.00;
                valAmtGst = 0.00;
                foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
                {
                    if (item is GridItem)
                    {
                        GridDataItem dataItem = (GridDataItem)item;
                        TextBox txtBefGst = (TextBox)dataItem["BefGst1"].FindControl("txtBefGst");
                        TextBox txtAmtGst = (TextBox)dataItem["ToatlWithGst1"].FindControl("txtAmtGst");

                        if (txtBefGst.Text != "&nbsp;" && txtBefGst.Text != "")
                        {
                            valBefGst1 = valBefGst1 + Convert.ToDouble(txtBefGst.Text);
                        }

                        if (txtAmtGst.Text != "&nbsp;" && txtAmtGst.Text != "")
                        {
                            valAmtGst = valAmtGst + Convert.ToDouble(txtAmtGst.Text);
                        }
                        if (dataItem["PayAmount"].Text != "&nbsp;" && dataItem["PayAmount"].Text != "")
                        {
                            payamount = payamount + Convert.ToDouble(dataItem["PayAmount"].Text);
                        }

                    }
                }
                GridFooterItem footerItem = e.Item as GridFooterItem;
                footerItem["BefGst1"].Text = valBefGst1.ToString();
                footerItem["ToatlWithGst1"].Text = valAmtGst.ToString();
                footerItem["PayAmount"].Text = payamount.ToString();
            }
        }

        protected  void CurrencyIndexchange(object sender, EventArgs e)
        {
            DataSet dsAdd = new DataSet();
            dsAdd = (DataSet)Session["DsClaim"];
            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    string pk1 = dataItem["SrNo"].Text.ToString();
                   
                    //ku
                    pk1 = "-1000";
                    DataRow[] dr1 = dsAdd.Tables[0].Select("SrNo='" + pk1 + "'");
                    flag = true;
                    //SrNo	id	Desc	cpf	AmtGst	BefGst	GstAmnt	trx_period	emp_code	emp_name	Department	
                    //recpath	Descriptions	Designation	emp_type	TimeCardId	Nationality	Trade	CurrencyID	
                    //ConversionOpt	GLAcc	created_on	created_by	modified_on	modified_by	recpath	CurrencyID	ConversionOpt	
                    //trx_type	CompanyID	ID	GstFlag	GstCode	ExRate	CurrencyIDComp	Remarks	claimstatus
                    //DropdownList drp=(DropdownList)item["tex_period"].FindControl(

                    DropDownList drptrxperiod = (DropDownList)dataItem["trx_period"].FindControl("drptrxperiod");
                    DropDownList drptrx_type = (DropDownList)dataItem["trx_type1"].FindControl("drptrx_type");
                    DropDownList drpCurrencyID = (DropDownList)dataItem["CurrencyID1"].FindControl("drpCurrencyID");
                    DropDownList drpGstCode = (DropDownList)dataItem["GstCode1"].FindControl("drpGstCode");
                    DropDownList drpBid = (DropDownList)dataItem["Bid1"].FindControl("drpBid");
                    //DropDownList drpGstFlag  = (DropDownList)dataItem["GstFlag1"].FindControl("drpGstFlag");
                    CheckBox chkGstFlag = (CheckBox)dataItem["GstFlag1"].FindControl("chkGst");

                    TextBox txtAmtGst = (TextBox)dataItem["ToatlWithGst1"].FindControl("txtAmtGst");
                    TextBox txtGstAmt = (TextBox)dataItem["GstAmnt1"].FindControl("txtGstAmt");
                    TextBox txtBefGst = (TextBox)dataItem["BefGst1"].FindControl("txtBefGst");
                    TextBox txtRemarks = (TextBox)dataItem["Remarks1"].FindControl("txtRemarks");
                    DropDownList drpReceipt = (DropDownList)dataItem["Recpyn1"].FindControl("drpReceipt");
                    //Get GST Percentagae amount 
                    DataSet dsgstcode = new DataSet();
                    string sqlGstcode = "select GstPer from GstMaster WHERE SrNo =" + drpGstCode.SelectedValue;
                    dsgstcode = DataAccess.FetchRS(CommandType.Text, sqlGstcode, null);
                    decimal gstPercentage = 0;
                    if (dsgstcode.Tables.Count > 0)
                    {
                        if (dsgstcode.Tables[0].Rows.Count > 0)
                        {
                            gstPercentage = Convert.ToDecimal(dsgstcode.Tables[0].Rows[0]["GstPer"].ToString());
                        }
                    }
                    if (chkGstFlag.Checked == false)
                    {
                        drpGstCode.Enabled = false;
                       // txtGstAmt.Enabled = false;
                        txtGstAmt.Text = "0";
                        //km
                        txtBefGst.Text = txtAmtGst.Text;
                    }
                    else
                    {
                         drpGstCode.Enabled = true;
                            //km
                         double beforeGst = Math.Round(Utility.ToDouble(txtAmtGst.Text) * (100 / (100 + Utility.ToDouble(gstPercentage))), 2);

                         double Gst = Utility.ToDouble(txtAmtGst.Text) - beforeGst;

                         txtGstAmt.Text = Utility.ToString(Gst);

                         txtBefGst.Text = Utility.ToString(beforeGst);

                        // txtGstAmt.Text = Utility.ToString(Utility.ToDouble(txtBefGst.Text) * (Utility.ToDouble(gstPercentage) / 100));    
                    }
                    
                    txtAmtGst.Text = Utility.ToString(Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text));
                   
                  

                    if (dataItem["Bid"].Text != "")
                    {
                        dr1[0]["Bid"] = Convert.ToInt32(drpBid.SelectedValue);
                    }
                    else
                    {
                        dr1[0]["Bid"] = Convert.ToInt32(0);
                    }

                    dr1[0]["Recpyn"] = drpReceipt.SelectedValue;
                    dr1[0]["trx_period"] = drptrxperiod.SelectedValue.Trim();
                    dr1[0]["trx_period1"] = drptrxperiod.SelectedValue.Trim();
                    dr1[0]["trx_type"] = drptrx_type.SelectedValue;
                    if (dataItem["CurrencyID"].Text != "")
                    {
                        dr1[0]["CurrencyID"] = Convert.ToInt32(drpCurrencyID.SelectedValue);
                    }
                    else
                    {
                        dr1[0]["CurrencyID"] = Convert.ToInt32(0);
                    }
                    dr1[0]["GstCode"] = drpGstCode.SelectedValue;

                    dr1[0]["id"] ="205";

                    //     dr1[0]["id"] = dataItem["id"].Text;
                    dr1[0]["emp_name"] = dataItem["emp_name"].Text;
                    dr1[0]["desc"] = dataItem["desc"].Text;
                    if (chkGstFlag.Checked)
                    {
                        dr1[0]["GstFlag"] = "1";
                    }
                    else
                    {
                        dr1[0]["GstFlag"] = "0";
                    }
                    
                    dr1[0]["GstAmnt"] = txtGstAmt.Text;
                    dr1[0]["BefGst"] = txtBefGst.Text;
                    dr1[0]["TransId"] = dataItem["TransId"].Text;
                    //Get Exchange rate 
                    /////////////////////////////////////Exchange Rate Start /////////////////////////////////
                    //Get value from TextBox and show in label
                    int COPT = 0; int mc = 0;
                    string strcon = "Select ConversionOpt,MultiCurr FROM Company where company_id=" + compid;
                    SqlDataReader drcon = DataAccess.ExecuteReader(CommandType.Text, strcon, null);

                    while (drcon.Read())
                    {
                        if (drcon.GetValue(0) == null)
                        {
                            COPT = 1;
                        }
                        else
                        {
                            COPT = Convert.ToInt32(drcon.GetValue(0).ToString());
                        }
                        if (drcon.GetValue(1) == null)
                        {
                            mc = 0;
                        }
                        else
                        {
                            mc = Convert.ToInt32(drcon.GetValue(1).ToString());
                        }
                    }
                    double exchangeRate = 1;
                    if (mc == 1)
                    {
                        if (COPT == 2 || COPT == 3)
                        {
                            string sd, ed = "";
                            sd = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                            //ed = transperiod2.Date.Month.ToString() + "/" + transperiod2.Date.Day.ToString() + "/" + transperiod2.Date.Year.ToString();
                            string exrate = "select  top 1  Rate from dbo.ExchangeRate where Currency_id =" + drpCurrencyID.SelectedValue + " and [Date]<='" + sd + "'  Order by [Date] desc";
                            SqlDataReader drex = DataAccess.ExecuteReader(CommandType.Text, exrate, null);
                            while (drex.Read())
                            {
                                if (drex.GetValue(0) == null)
                                {
                                    exchangeRate = 1.0;
                                }
                                else
                                {
                                    exchangeRate = Convert.ToDouble(drex.GetValue(0).ToString());
                                }
                            }
                        }
                    }
                    /////////////////////////////////////Exchange Rate End /////////////////////////////////
                    dataItem["ExRate"].Text = exchangeRate.ToString();
                    if (dataItem["ExRate"].Text == "&nbsp;")
                    {
                        dr1[0]["ExRate"] = "1";
                    }
                    else
                    {
                        dr1[0]["ExRate"] = dataItem["ExRate"].Text;
                    }

                    if (chkGstFlag.Checked == false)
                    {
                        dr1[0]["ToatlWithGst"] = Utility.ToString(Utility.ToDouble(dr1[0]["BefGst"]));// Utility.ToString(Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text));
                        dr1[0]["PayAmount"] = Utility.ToString(Utility.ToDouble(dr1[0]["BefGst"]) * exchangeRate);
                    }
                    else
                    {
                        dr1[0]["ToatlWithGst"] = Utility.ToString((Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text)));
                        dr1[0]["PayAmount"] = Utility.ToString((Utility.ToDouble(txtGstAmt.Text) + Utility.ToDouble(txtBefGst.Text)) * exchangeRate);
                    }

                    if (dataItem["CurrencyIDComp"].Text == "&nbsp;")
                    {
                        dr1[0]["CurrencyIDComp"] = "1";
                    }
                    else
                    {
                        dr1[0]["CurrencyIDComp"] = dataItem["CurrencyIDComp"].Text;
                    }                   
                    dr1[0]["recpath"] = dataItem["recpath"].Text;
                    dr1[0]["Remarks"] = txtRemarks.Text;
                    dr1[0]["cpf"] = dataItem["cpf"].Text;
                    dr1[0]["emp_code"] = dataItem["emp_code"].Text;
                    dr1[0]["Department"] = dataItem["Department"].Text;
                    dr1[0]["Descriptions"] = dataItem["Descriptions"].Text;
                    dr1[0]["Designation"] = dataItem["Designation"].Text;
                    dr1[0]["emp_type"] = dataItem["emp_type"].Text;
                    dr1[0]["TimeCardId"] = dataItem["TimeCardId"].Text;
                    dr1[0]["Nationality"] = dataItem["Nationality"].Text;
                    if (dataItem["ConversionOpt"].Text == "&nbsp;")
                    {
                        dr1[0]["ConversionOpt"] = "0";
                    }
                    else
                    {
                        dr1[0]["ConversionOpt"] = dataItem["ConversionOpt"].Text;
                    }
                    dr1[0]["GLAcc"] = dataItem["GLAcc"].Text;
                    dr1[0]["created_on"] = dataItem["created_on"].Text;
                    dr1[0]["created_by"] = dataItem["created_by"].Text;
                    dr1[0]["modified_on"] = dataItem["modified_on"].Text;
                    dr1[0]["modified_by"] = dataItem["modified_by"].Text;
                    dr1[0]["CompanyID"] = dataItem["CompanyID"].Text;
                    dr1[0]["ID"] = dataItem["ID"].Text;
                    dr1[0]["claimstatus"] = dataItem["claimstatus"].Text;
                }
            }
            dsAdd.AcceptChanges();
            RadGrid1.DataSource = dsAdd.Tables[0];
            RadGrid1.DataBind();
            Session["DsClaim"] = dsAdd;
        }

        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            //try
            //{
            //    GridEditableItem editedItem = e.Item as GridEditableItem;
            //    string TrxId = Utility.ToString(editedItem.OwnerTableView.DataKeyValues[editedItem.ItemIndex]["trx_id"]);
            //    string sSQLCheck = "select status from emp_additions where trx_id = {0}";
            //    sSQLCheck = string.Format(sSQLCheck, Utility.ToInteger(TrxId));
            //    string status = "";
            //    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQLCheck, null);
            //    while (dr.Read())
            //    {
            //        status = Utility.ToString(dr.GetValue(0));
            //    }
            //    if (status == "U")
            //    {
            //        string sSQL = "DELETE FROM emp_additions where trx_id = {0}";
            //        sSQL = string.Format(sSQL, Utility.ToInteger(TrxId));
            //        int i = DataAccess.ExecuteStoreProc(sSQL);
            //    }
            //    else
            //    {
            //        Response.Write("<script language = 'Javascript'>alert('Payroll has been Processed, Action not allowed.');</script>");
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

        public static void ShowMessageBox(string message)
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
            HttpContext.Current.Response.Write(sbScript);
        }

    }
}