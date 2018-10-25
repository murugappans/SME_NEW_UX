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
using System.Data.SqlClient;
using System.Text;
using System.Data.OleDb;
using System.Xml;
using System.Globalization;
using System.Text.RegularExpressions;
namespace SMEPayroll.employee
{
    public partial class emp_overtime : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy = "";
        protected string strendatemdy = "";
        protected string strstdatedmy = "";
        protected string strendatedmy = "";
        int intcnt;
        int comp_id;
        string sSQL = "";
        string ssqle = "";
        string sql = null;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;

        string strWF = "";
        string strEmpvisible = "";
        string _actionMessage = "";



        //bool isValidTimeSpan(string str)
        //{
        //    TimeSpan interval;
        //    if (TimeSpan.TryParse(value, out interval)) return true;
        //    return false;
        //}


        public bool IsValidTime(string thetime)
{
    
string time = thetime;
//    string time = "254:00";
//    if (thetime.Length > 0 && thetime.Length < 3)
//    {
//        TimeSpan result = TimeSpan.FromHours(double.Parse(thetime));
//        time = result.Hours + ":" + result.Minutes;
//    }

////Regex checktime =
// //new Regex(@"^(20|21|22|23|[01]d|d)(([:][0-5]d){1,2})$");
////new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
////    bool RESULT=checktime.IsMatch(thetime);
//DateTime outtime;
//bool resulting = DateTime.TryParse(time, out outtime);
//return resulting;
    if(string.IsNullOrEmpty(time))
        return true;


string[] words = time.Split(':');

if (words.Length == 2)
{
    TimeSpan interval;
    if (TimeSpan.TryParse(words[1], out interval))
        return true;
}
else
{   int outtime;
if (int.TryParse(time, out outtime))
    return true;
}
    return false;


} 


        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridCommandItem item = e.Item as GridCommandItem;
            if (item != null)
            {
                Button btn = item.FindControl("btnsubmit") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                btn = item.FindControl("btnCalcOverVar") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                btn = item.FindControl("btnApplyCeiling") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");

            }


            GridSettingsPersister objCount = new GridSettingsPersister();
            objCount.RowCount(e, tbRecord);
        }
        [AjaxPro.AjaxMethod]
        public string SetDate(string monthid)
        {
            IFormatProvider provider = new CultureInfo("en-GB", true);
            DataSet dataSet = new DataSet();
            this.sSQL = "sp_GetPayrollMonth";
            SqlParameter[] cmdParams = new SqlParameter[]
			{
				new SqlParameter("@ROWID", monthid),
				new SqlParameter("@YEARS", SqlDbType.BigInt),
				new SqlParameter("@PAYTYPE", this.Session["PAYTYPE"].ToString())
			};
            dataSet = DataAccess.ExecuteSPDataSet(this.sSQL, cmdParams);
            this.Session["PayStartDay"] = dataSet.Tables[0].Rows[0]["PayStartDay"].ToString();
            this.Session["PayEndDay"] = dataSet.Tables[0].Rows[0]["PayEndDay"].ToString();
            this.Session["PaySubStartDay"] = dataSet.Tables[0].Rows[0]["PaySubStartDay"].ToString();
            this.Session["PaySubEndDay"] = dataSet.Tables[0].Rows[0]["PaySubEndDay"].ToString();
            this.Session["PaySubStartDate"] = dataSet.Tables[0].Rows[0]["PaySubStartDate"].ToString();
            this.Session["PaySubEndDate"] = dataSet.Tables[0].Rows[0]["PaySubEndDate"].ToString();
            return Convert.ToDateTime(dataSet.Tables[0].Rows[0]["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", provider) + "," + Convert.ToDateTime(dataSet.Tables[0].Rows[0]["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", provider);
        }
        //[AjaxPro.AjaxMethod]
        //public string SetDate(string monthid)
        //{
        //    IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
        //    DataSet ds = new DataSet();
        //    sSQL = "sp_GetPayrollMonth";// 0,2009,2
        //    SqlParameter[] parms = new SqlParameter[3];
        //    parms[0] = new SqlParameter("@ROWID", monthid);
        //    parms[1] = new SqlParameter("@YEARS", 0);
        //    parms[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());

        //    ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

        //    Session["PayStartDay"] = ds.Tables[0].Rows[0]["PayStartDay"].ToString();
        //    Session["PayEndDay"] = ds.Tables[0].Rows[0]["PayEndDay"].ToString();
        //    Session["PaySubStartDay"] = ds.Tables[0].Rows[0]["PaySubStartDay"].ToString();
        //    Session["PaySubEndDay"] = ds.Tables[0].Rows[0]["PaySubEndDay"].ToString();
        //    Session["PaySubStartDate"] = ds.Tables[0].Rows[0]["PaySubStartDate"].ToString();
        //    Session["PaySubEndDate"] = ds.Tables[0].Rows[0]["PaySubEndDate"].ToString();
        //    return Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "," + Convert.ToDateTime(ds.Tables[0].Rows[0]["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
        //}

        public int GetMonth(int Month)
        {
            int month;
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataSet ds = new DataSet();
            sSQL = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@ROWID", Month);
            parms[1] = new SqlParameter("@YEARS", 0);
            parms[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());

            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);

          month = Convert.ToInt32(ds.Tables[0].Rows[0]["Month"].ToString());

          return month;
            
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            int id = 01;
            string message = "false";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/XML/xmldata.xml"));
            
            XmlNode messege_id;
           
            
            try
            {
                messege_id = xmlDoc.SelectSingleNode("SMEPayroll/Otceiling/ceiling[@id='" + id.ToString().Trim() + "']");
                message = messege_id.Attributes[1].Value.ToString();
            }
            catch (Exception ex)
            {
                message = "Error";
            }


            if (message == "true")
            {
               this.btnCopy.Visible = true;
            }


            /* To disable Grid filtering options  */
            //AjaxPro.Utility.RegisterTypeForAjax(typeof(emp_overtime));
            //if (cmbMonth.Attributes["onchange"] == null) { cmbMonth.Attributes.Add("onchange", "javascript:ChangeMonth(this.value);"); }

          
            
            Telerik.Web.UI.GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
            btnCopy.Click += new EventHandler(btnCopy_Click);



            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }

            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            SqlDataSource3.ConnectionString = Session["ConString"].ToString();
            //lblerror.Text = "";
            comp_id = Utility.ToInteger(Session["Compid"]);
            RadGrid1.PageSizeChanged += new GridPageSizeChangedEventHandler(RadGrid1_PageSizeChanged);
            RadGrid1.PageIndexChanged += new GridPageChangedEventHandler(RadGrid1_PageIndexChanged);
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();
            }

            if (Session["strWF"] == null)
            {
                string sqlWF = "Select WorkFlowID,WFPAY,WFLEAVE,WFEMP,WFCLAIM,WFReport,WFTimeSheet from company WHERE Company_Id=" + comp_id;
                DataSet dsWF = new DataSet();
                dsWF = DataAccess.FetchRS(CommandType.Text, sqlWF, null);

                if (dsWF.Tables.Count > 0)
                {
                    if (dsWF.Tables[0].Rows.Count > 0)
                    {
                        strWF = dsWF.Tables[0].Rows[0][0].ToString();
                        Session["strWF"] = strWF;
                    }
                }
            }
            else
            {
                strWF = (string)Session["strWF"];
            }




            RadGrid1.Columns[11].Visible = false;
            RadGrid1.Columns[12].Visible = false;
            RadGrid1.Columns[13].Visible = false;
            RadGrid1.Columns[14].Visible = false;
            //if (Session["V1Formula"].ToString() != "0")
            //{
            //RadGrid1.Columns[11].HeaderText = Session["V1text"].ToString();
            RadGrid1.Columns[11].HeaderText = Convert.ToString(Session["V1text"]);
            RadGrid1.Columns[11].Visible = true;
            //}

            //if (Session["V2Formula"].ToString() != "0")
            //{
            //RadGrid1.Columns[12].HeaderText = Session["V2text"].ToString();
            RadGrid1.Columns[12].HeaderText = Convert.ToString(Session["V2text"]);
            RadGrid1.Columns[12].Visible = true;
            //}
            //if (Session["V3Formula"].ToString() != "0")
            //{
            //RadGrid1.Columns[13].HeaderText = Session["V3text"].ToString();
            RadGrid1.Columns[13].HeaderText = Convert.ToString(Session["V3text"]);
            RadGrid1.Columns[13].Visible = true;
            //}
            //if (Session["V4Formula"].ToString() != "0")
            //{
            //RadGrid1.Columns[14].HeaderText = Session["V4text"].ToString();
            RadGrid1.Columns[14].HeaderText = Convert.ToString(Session["V4text"]);
            RadGrid1.Columns[14].Visible = true;
            //}

            //if (Session["TimeSheetApproved"].ToString() == "1")
            //{
            RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.Bottom;
            //}
            //else
            //{   RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;
            //}

            if (!IsPostBack)
            { 
                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
            }


            //Check for WorkFlow number 2
            if (strWF == "2" && Session["PayrollWF"] != null)
            {
                if (Session["PayrollWF"].ToString() == "1")
                {
                    if (Session["GroupName"].ToString().ToUpper() != "SUPER ADMIN")
                    {
                        strEmpvisible = "";
                        if (Session["dsEmpSup"] != null)
                        {
                            if (Session["dsEmpWF"] != null)
                            {
                                DataSet dstemp = new DataSet();
                                dstemp = (DataSet)Session["dsEmpWF"];
                                foreach (DataRow dr in dstemp.Tables[0].Rows)
                                {
                                    if (strEmpvisible == "")
                                    {
                                        strEmpvisible = dr["Emp_ID"].ToString();
                                    }
                                    else
                                    {
                                        strEmpvisible = strEmpvisible + "," + dr["Emp_ID"].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //strEmpvisible = "5,127";
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }


        }
        void btnCopy_Click(object sender, EventArgs e)
        {
            Session["copy"] = "true";

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {

                    GridDataItem dataItem = (GridDataItem)item;
                    dataItem.Selected = true;
                }
            }
        }



        protected void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            if (e.RebindReason != GridRebindReason.InitialLoad)
            {
                if (strEmpvisible != "")
                {
                    Session["EmpPassID"] = strEmpvisible;
                }
                else
                {
                    Session["EmpPassID"] = "";
                }

                tbRecord.Visible = true;//toolbar

                intcnt = 1;

                //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

                int i = 0;

                DataSet ds = new DataSet();

                string ssql = "sp_emp_overtime";// 0,2009,2
                SqlParameter[] parms = new SqlParameter[5];
                parms[i++] = new SqlParameter("@month", cmbMonth.SelectedValue);
                parms[i++] = new SqlParameter("@year", cmbYear.SelectedValue);
                parms[i++] = new SqlParameter("@company_id", comp_id);
                parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));
                parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(deptID.SelectedValue));
                ds = DataAccess.ExecuteSPDataSet(ssql, parms);


                if (strEmpvisible != "")
                {
                    DataView view = new DataView();

                    view = ds.Tables[0].DefaultView;
                    //ds.Tables[0].DefaultView.RowFilter = "emp_id IN(" + strEmpvisible + ")";


                    //view.Table = DataSet1.Tables["Suppliers"];
                    //view.AllowDelete = true;
                    //view.AllowEdit = true;
                    // view.AllowNew = true;
                    view.RowFilter = "empid IN(" + strEmpvisible + ")";
                    // Simple-bind to a TextBox control
                    Session["EmpPassID"] = strEmpvisible;
                    this.RadGrid1.DataSource = view;
                    //RadGrid1.DataBind();
                }
                else
                {
                    this.RadGrid1.DataSource = ds;
                    //RadGrid1.DataBind();
                }



                //RadGrid1.DataSource = ds;
                //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
                //foreach (DataRow dr in drResults)
                //{
                //    Session["PayStartDay"] = dr["PayStartDay"].ToString();
                //    Session["PayEndDay"] = dr["PayEndDay"].ToString();
                //    Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                //    Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                //    Session["CurrentMonth"] = dr["Month"].ToString();
                //    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                //    Session["PaySubEndDate"]= dr["PaySubEndDate"].ToString();
                //    strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
                //    strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
                //    strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
                //    strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
                //}
                //if (Session["PaySubStartDate"] != null)
                //{
                //}
                //RadGrid1.DataBind();
                Session["ROWID"] = cmbMonth.SelectedValue.ToString();
                Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();

                 ////int NoOfDaysInMonth =DateTime.DaysInMonth( Convert.ToInt32(cmbYear.SelectedValue),Convert.ToInt32(cmbMonth.SelectedValue));


            }
        }

        void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            bingrid1();
        }


        void RadGrid1_PageSizeChanged(object source, GridPageSizeChangedEventArgs e)
        {


            bingrid1();
            //throw new Exception("The method or operation is not implemented.");
        }


       


        void bingrid1()
        {
            if (strEmpvisible != "")
            {
                Session["EmpPassID"] = strEmpvisible;
            }
            else
            {
                Session["EmpPassID"] = "";
            }

            tbRecord.Visible = true;//toolbar

            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            rdFrom.Enabled = false;
            rdEnd.Enabled = false;
            deptID.Enabled = false;
            //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            int i = 0;

            DataSet ds = new DataSet();

            string ssql = "sp_emp_overtime";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[5];
            parms[i++] = new SqlParameter("@month", cmbMonth.SelectedValue);
            parms[i++] = new SqlParameter("@year", cmbYear.SelectedValue);
            parms[i++] = new SqlParameter("@company_id", comp_id);
            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));
            parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(deptID.SelectedValue));
            ds = DataAccess.ExecuteSPDataSet(ssql, parms);


            if (strEmpvisible != "")
            {
                DataView view = new DataView();

                view = ds.Tables[0].DefaultView;
                //ds.Tables[0].DefaultView.RowFilter = "emp_id IN(" + strEmpvisible + ")";


                //view.Table = DataSet1.Tables["Suppliers"];
                //view.AllowDelete = true;
                //view.AllowEdit = true;
                // view.AllowNew = true;
                view.RowFilter = "empid IN(" + strEmpvisible + ")";
                // Simple-bind to a TextBox control
                Session["EmpPassID"] = strEmpvisible;
                this.RadGrid1.DataSource = view;
                RadGrid1.DataBind();
            }
            else
            {
                this.RadGrid1.DataSource = ds;
                RadGrid1.DataBind();
            }



            //RadGrid1.DataSource = ds;
            //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            //foreach (DataRow dr in drResults)
            //{
            //    Session["PayStartDay"] = dr["PayStartDay"].ToString();
            //    Session["PayEndDay"] = dr["PayEndDay"].ToString();
            //    Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
            //    Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
            //    Session["CurrentMonth"] = dr["Month"].ToString();
            //    Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
            //    Session["PaySubEndDate"]= dr["PaySubEndDate"].ToString();
            //    strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
            //    strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
            //    strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
            //    strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
            //}
            //if (Session["PaySubStartDate"] != null)
            //{
            RadGrid1.DataBind();
            //}
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();

        }

        protected void bindgrid(object sender, EventArgs e)
        {
            bingrid1();
        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private void bindMonth()
        {
            MonthFill();
        }
        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
            strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);
            strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
            strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);
        
            if ((Utility.AllowedAction1(Session["Username"].ToString(), "Manage Overtime Payroll")) == false)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.None;

            }
            else
            {
                RadGrid1.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.Bottom;
            }

            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if (Session["PaySubStartDate"] != null)
                {
                    GridItem dataItem = (GridItem)e.Item;
                    GridDataItem dtItem = e.Item as GridDataItem;
                    string empid = dataItem.Cells[4].Text.ToString();
                    TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                    TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                    TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                    TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                    TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                    TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                    TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                    TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                    TextBox txtot_entitlement = (TextBox)dataItem.FindControl("txtot_entitlement");
                    CheckBox checkBoxVal = (CheckBox)dataItem.FindControl("GridClientSelectColumn");
                    string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                    ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                    string status;
                    DataSet ds = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        status = "";
                    }
                    else
                    {
                        status = Convert.ToString(ds.Tables[0].Rows[0]["status"]).ToString();
                    }

                    if (dtItem["pay_frequency"].Text.Trim() != "D")
                    {
                        txtbox2.Enabled = false;
                        txtbox2.BackColor = Color.LightYellow;

                    }
                    else
                    {
                        txtbox2.Enabled = true;
                        txtbox2.BackColor = Color.White;
                    }
                   // if (dtItem["pay_frequency"].Text.Trim() == "M" || dtItem["pay_frequency"].Text.Trim() == "H")
                   if (dtItem["pay_frequency"].Text.Trim() == "H")
                    {
                        txtbox3.Enabled = true;
                    }
                    else
                    {
                        txtbox3.Enabled = false;
                        txtbox3.BackColor = Color.LightYellow;
                        //txtbox2.Text = "";
                    }
                    Label lblCeilingOt1 = (Label)dtItem["overtime1"].FindControl("lblCeilingOt1");
                    Label lblCeilingOt2 = (Label)dtItem["overtime2"].FindControl("lblCeilingOt2");
                    lblCeilingOt1.Text = "{" + dataItem.Cells[26].Text.ToString().Replace("&nbsp;", "") + "}" ;
                    lblCeilingOt2.Text = "{" + dataItem.Cells[27].Text.ToString().Replace("&nbsp;", "") + "}"; 

                    if (status == "G" || status == "P" || status == "A")
                    {
                        txtbox.Enabled = false;
                        txtbox1.Enabled = false;
                        txtbox2.Enabled = false;
                        txtbox3.Enabled = false;
                        txtv1.Enabled = false;
                        txtv2.Enabled = false;
                        txtv3.Enabled = false;
                        txtv4.Enabled = false;
                        dataItem.BackColor = Color.Azure;
                        ((e.Item as GridDataItem)["GridClientSelectColumn"].Controls[0] as CheckBox).Enabled = false; 
                    }
                    else
                    {
                        if (txtot_entitlement.Text.ToString().Trim() == "N")
                        {
                            txtbox.Enabled = false;
                            txtbox1.Enabled = false;
                            txtbox.BackColor = Color.LightYellow;
                            txtbox1.BackColor = Color.LightYellow;

                        }
                        //if (Session["V1Formula"].ToString() != "0")

                        
                        if (Convert.ToString(Session["V1Formula"]) != "0")
                        {

                            string strRate = dtItem["V1Rate"].Text.ToString().Replace("&nbsp;", "");
                            //km
                           // string strRate = dataItem.Cells[19].Text.ToString().Replace("&nbsp;", "");                            
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    //(Lab)dataItem.FindControl("txtovertime");
                                    Label lblv1 = (Label)dtItem["v1"].FindControl("lblv1");
                                    if (intrae <= 0)
                                    {
                                        (dtItem["V1"].Controls[1] as TextBox).Enabled = false;
                                        (dtItem["V1"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                        //((TextBox)dataItem.Cells[13].Controls[1]).Enabled = false;
                                        //((TextBox)dataItem.Cells[13].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv1.Text.Length > 0)
                                        {
                                            lblv1.Text = lblv1.Text + "------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            lblv1.Text ="{" + intrae + " }";
                                        }
                                        lblv1.ToolTip = lblv1.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                (dtItem["V1"].Controls[1] as TextBox).Enabled = false;
                                (dtItem["V1"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                //((TextBox)dataItem.Cells[13].Controls[1]).Enabled = false;
                                //((TextBox)dataItem.Cells[13].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }
                        // if (Session["V2Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V2Formula"]) != "0")
                        {
                            string strRate = dtItem["V2Rate"].Text.ToString().Replace("&nbsp;", "");
                            //string strRate = dataItem.Cells[20].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv2 = (Label)dtItem["v2"].FindControl("lblv2");
                                    if (intrae <= 0)
                                    {

                                        (dtItem["V2"].Controls[1] as TextBox).Enabled = false;
                                        (dtItem["V2"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                        //((TextBox)dataItem.Cells[14].Controls[1]).Enabled = false;
                                        //((TextBox)dataItem.Cells[14].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv2.Text.Length > 0)
                                        {
                                            lblv2.Text = lblv2.Text + "------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            lblv2.Text ="{" + intrae + " }";
                                        }
                                        lblv2.ToolTip = lblv2.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                (dtItem["V2"].Controls[1] as TextBox).Enabled = false;
                                (dtItem["V2"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                //((TextBox)dataItem.Cells[14].Controls[1]).Enabled = false;
                                //((TextBox)dataItem.Cells[14].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }

                        // if (Session["V3Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V3Formula"]) != "0")
                        {
                            string strRate = dtItem["V3Rate"].Text.ToString().Replace("&nbsp;", "");

                           // string strRate = dataItem.Cells[21].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv3 = (Label)dtItem["v3"].FindControl("lblv3");
                                    if (intrae <= 0)
                                    {
                                        (dtItem["V3"].Controls[1] as TextBox).Enabled = false;
                                        (dtItem["V3"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                        //((TextBox)dataItem.Cells[15].Controls[1]).Enabled = false;
                                        //((TextBox)dataItem.Cells[15].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv3.Text.Length > 0)
                                        {
                                            lblv3.Text = lblv3.Text + "--------" + "{" + intrae + " }";
                                        }
                                        else
                                        {
                                            lblv3.Text = "{" + intrae + " }";
                                        }
                                        lblv3.ToolTip = lblv3.Text;
                                    }
                                }
                                catch (Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {
                                }
                            }
                            else
                            {
                                (dtItem["V3"].Controls[1] as TextBox).Enabled = false;
                                (dtItem["V3"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                //((TextBox)dataItem.Cells[15].Controls[1]).Enabled = false;
                                //((TextBox)dataItem.Cells[15].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }

                        //if (Session["V4Formula"].ToString() != "0")
                        if (Convert.ToString(Session["V4Formula"]) != "0")
                        {
                            string strRate = dtItem["V4Rate"].Text.ToString().Replace("&nbsp;", "");
                          //  string strRate = dataItem.Cells[22].Text.ToString().Replace("&nbsp;", "");
                            if (strRate.Length > 0)
                            {
                                try
                                {
                                    double intrae = Utility.ToDouble(strRate);
                                    Label lblv4 = (Label)dtItem["v4"].FindControl("lblv4");
                                    if (intrae <= 0)
                                    {
                                        (dtItem["V4"].Controls[1] as TextBox).Enabled = false;
                                        (dtItem["V4"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                        //((TextBox)dataItem.Cells[16].Controls[1]).Enabled = false;
                                        //((TextBox)dataItem.Cells[16].Controls[1]).BackColor = Color.LightYellow;
                                    }
                                    else
                                    {
                                        if (lblv4.Text.Length > 0)
                                        {
                                            lblv4.Text = lblv4.Text + "------" + "{" + intrae + " }";
                                        }
                                        else 
                                        {
                                            lblv4.Text = "{" + intrae + " }";
                                        }
                                            lblv4.ToolTip = lblv4.Text;
                                    }
                                }
                                catch(Exception ee)
                                {
                                    throw ee;
                                }
                                finally
                                {

                                }
                            }
                            else
                            {

                                (dtItem["V4"].Controls[1] as TextBox).Enabled = false;
                                (dtItem["V4"].Controls[1] as TextBox).BackColor = Color.Yellow;
                                //((TextBox)dataItem.Cells[16].Controls[1]).Enabled = false;
                                    
                                //((TextBox)dataItem.Cells[16].Controls[1]).BackColor = Color.LightYellow;
                            }
                        }
                    }
             }

         }
        }

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            
            //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            //strstdatemdy           = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
            //strendatemdy           = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);
            //strstdatedmy           = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
            //strendatedmy           = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);

            if(string.IsNullOrEmpty(rdFrom.SelectedDate.ToString()) && string.IsNullOrEmpty(rdEnd.SelectedDate.ToString()))
            {
            ShowMessageBox("Please Select Overtime From and To Date");
                return;
            }


            int ka = 0;
            string ssqlA = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parmsA = new SqlParameter[3];
            parmsA[ka++] = new SqlParameter("@ROWID", cmbMonth.SelectedValue.ToString());
            parmsA[ka++] = new SqlParameter("@YEARS", cmbYear.SelectedValue.ToString());
            parmsA[ka++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssqlA, parmsA);


            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue.ToString());
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);


            }







            if (e.Item is GridCommandItem)
            {
                //GridCommandItem commandItem = (GridCommandItem)e.Item;
                //((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                //((Button)commandItem.FindControl("btnCalcOverVar")).Enabled = false;
                //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = false;

                if (e.CommandName == "ApplyCeiling")
                {
                   //Hours/Days 
                    string strHourCeiling = "Select *  from CeilingMaster Where CeilingType=1 And CompanyID=" + comp_id;
                    DataSet dsHourCeiling = DataAccess.FetchRS(CommandType.Text, strHourCeiling, null);

                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));

                                TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");

                                
                                   

                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {
                                    //Get Employee Ceiling for Hours 
                                    //string strEmpCeilingDetails = "Select Top(Select count(*) from CeilingEmployee  Where CompanyId=" + comp_id + ") *  from EmployeeCeilingDetails Where EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year+ " ',103)";
                                    string strEmpCeilingDetails = "Select Top(1) *  from EmployeeCeilingDetails Where Emp_Code=" + empcode + " And EffDate <= Convert(Datetime,'" + rdEnd.SelectedDate.Value.Day + "/" + rdEnd.SelectedDate.Value.Month + "/" + rdEnd.SelectedDate.Value.Year + " ',103)  Order By EffDate     Desc";
                                    DataSet dsEmpCeilingDetails  = DataAccess.FetchRS(CommandType.Text, strEmpCeilingDetails, null);

                                    double i = Utility.ToDouble(txtbox.Text); //Ot1
                                    double j = Utility.ToDouble(txtbox1.Text);//Ot2
                                    double k = Utility.ToDouble(txtbox2.Text);//Days
                                    double l = Utility.ToDouble(txtbox3.Text);// NH
                                    double dblv1 = Utility.ToDouble(txtv1.Text);//V1
                                    double dblv2 = Utility.ToDouble(txtv2.Text);//V2
                                    double dblv3 = Utility.ToDouble(txtv3.Text);//V3
                                    double dblv4 = Utility.ToDouble(txtv4.Text);//V4

                                    Label lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                    Label lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                    lblCeilingOt1.Text = "{" + dataItem["ot1rate"].Text + "}";
                                    lblCeilingOt2.Text = "{" +  dataItem["ot2rate"].Text + "}";
                                    
                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                                DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                                if (drNHHr.Length > 0)
                                                {
                                                    if (dsHourCeiling.Tables.Count > 0)
                                                    {
                                                        if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                                        {
                                                            DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("Parameter='OT1'");
                                                            if (drOT1.Length > 0)
                                                            {
                                                                if (i > Convert.ToDouble(drNHHr[0][4].ToString()))
                                                                {
                                                                     lblCeilingOt1 = (Label)dataItem["overtime1"].FindControl("lblCeilingOt1");
                                                                    //lblCeilingOt1
                                                                   lblCeilingOt1.Text = "{" + i.ToString() + "}";
                                                                   lblCeilingOt1.ToolTip = lblCeilingOt1.Text;
                                                                    i = Convert.ToDouble(drNHHr[0][4].ToString());
                                                                    txtbox.Text = i.ToString();
                                                                    txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("Parameter='OT2'");
                                                            if (drOT2.Length > 0)
                                                            {
                                                                if (j > Convert.ToDouble(drNHHr[0][5].ToString()))
                                                                {
                                                                     lblCeilingOt2 = (Label)dataItem["overtime2"].FindControl("lblCeilingOt2");
                                                                    //lblCeilingOt1
                                                                    lblCeilingOt2.Text = "{" + j.ToString() + "}";
                                                                    lblCeilingOt2.ToolTip = "{" + j.ToString() + "}";
                                                                    j = Convert.ToDouble(drNHHr[0][5].ToString());
                                                                    txtbox1.Text = j.ToString();
                                                                    txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drNH = dsHourCeiling.Tables[0].Select("Parameter='NH'");
                                                            if (drNH.Length > 0)
                                                            {
                                                                if (l > Convert.ToDouble(drNHHr[0][3].ToString()))
                                                                {
                                                                    Label lblCeilNH = (Label)dataItem["NH_Work"].FindControl("lblCeilNH");
                                                                    //lblCeilingOt1
                                                                    lblCeilNH.Text = "{" + l.ToString() + "}";
                                                                    lblCeilNH.ToolTip = "{" + l.ToString() + "}";
                                                                    l = Convert.ToDouble(drNHHr[0][3].ToString());
                                                                    txtbox3.Text = l.ToString();
                                                                    txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drDays = dsHourCeiling.Tables[0].Select("Parameter='Days'");
                                                            if (drDays.Length > 0)
                                                            {
                                                                if (k > Convert.ToDouble(drNHHr[0][6].ToString()))
                                                                {
                                                                    Label lblDaysWork = (Label)dataItem["days_work"].FindControl("lblDaysWork");
                                                                    //lblCeilingOt1
                                                                    lblDaysWork.Text = "{" + k.ToString() + "}";
                                                                    lblDaysWork.ToolTip = "{" + k.ToString() + "}";
                                                                    k = Convert.ToDouble(drNHHr[0][6].ToString());
                                                                    txtbox2.Text = k.ToString();
                                                                    txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drV1 = dsHourCeiling.Tables[0].Select("Parameter='V1'");
                                                            if (drV1.Length > 0)
                                                            {
                                                                if (dblv1 > Convert.ToDouble(drNHHr[0][7].ToString()))
                                                                {
                                                                    Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                    //lblCeilingOt1
                                                                   lblV1Ceil.Text = "{" + dblv1.ToString() + "}";
                                                                   lblV1Ceil.ToolTip = "{" + dblv1.ToString() + "}";
                                                                    dblv1 = Convert.ToDouble(drNHHr[0][7].ToString());
                                                                    txtv1.Text = dblv1.ToString();
                                                                    txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drV2 = dsHourCeiling.Tables[0].Select("Parameter='V2'");
                                                            if (drV2.Length > 0)
                                                            {
                                                                if (dblv2 > Convert.ToDouble(drNHHr[0][8].ToString()))
                                                                {
                                                                    Label lblV2Ceil = (Label)dataItem["v2"].FindControl("lblv2");
                                                                    //lblCeilingOt1
                                                                    lblV2Ceil.Text = "{" + dblv2.ToString() + "}";
                                                                    lblV2Ceil.ToolTip = "{" + dblv2.ToString() + "}";
                                                                    dblv2 = Convert.ToDouble(drNHHr[0][8].ToString());
                                                                    txtv2.Text = dblv2.ToString();
                                                                    txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                            DataRow[] drV3 = dsHourCeiling.Tables[0].Select("Parameter='V3'");
                                                            if (drV3.Length > 0)
                                                            {
                                                                if (dblv3 > Convert.ToDouble(drNHHr[0][9].ToString()))
                                                                {
                                                                    Label lblV3Ceil = (Label)dataItem["v3"].FindControl("lblv3");
                                                                    //lblCeilingOt1
                                                                    lblV3Ceil.Text = "{" + dblv3.ToString() + "}";
                                                                    lblV3Ceil.ToolTip = "{" + dblv3.ToString() + "}";
                                                                    dblv3 = Convert.ToDouble(drNHHr[0][9].ToString());
                                                                    txtv3.Text = dblv3.ToString();
                                                                    txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                }
                                                            }

                                                                DataRow[] drV4 = dsHourCeiling.Tables[0].Select("Parameter='V4'");
                                                                if (drV4.Length > 0)
                                                                {
                                                                    if (dblv4 > Convert.ToDouble(drNHHr[0][10].ToString()))
                                                                    {
                                                                        Label lblV4Ceil = (Label)dataItem["v4"].FindControl("lblv4");
                                                                        //lblCeilingOt1
                                                                        lblV4Ceil.Text = "{" + dblv4.ToString() + "}";
                                                                        lblV4Ceil.ToolTip = "{" + dblv4.ToString() + "}";
                                                                        dblv4 = Convert.ToDouble(drNHHr[0][10].ToString());
                                                                        txtv4.Text = dblv4.ToString();
                                                                        txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                            }
                                        }

                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drOT1 = dsHourCeiling.Tables[0].Select("Parameter=OT1");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drOT2 = dsHourCeiling.Tables[0].Select("Parameter=OT2");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drDays = dsHourCeiling.Tables[0].Select("Parameter=Days");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drV1 = dsHourCeiling.Tables[0].Select("Parameter=V1");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drV2 = dsHourCeiling.Tables[0].Select("Parameter=V2");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drV3 = dsHourCeiling.Tables[0].Select("Parameter=V3");
                                        //}
                                        //if (dsHourCeiling.Tables[0].Rows.Count > 0)
                                        //{
                                        //    DataRow[] drV4 = dsHourCeiling.Tables[0].Select("Parameter=V4");
                                        //}
                                        //Check Value For Rates

                                    status = "U";
                                    double v1, v2, v3, v4;

                                    ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate from employee where emp_code='" + empid + "'";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                    if (ds.Tables[0].Rows.Count == 0)
                                    {
                                        v1 = 0;
                                        v2 = 0;
                                        v3 = 0;
                                        v4 = 0;
                                    }
                                    else
                                    {
                                        v1 = Utility.ToDouble(ds.Tables[0].Rows[0]["v1rate"].ToString());
                                        v2 = Utility.ToDouble(ds.Tables[0].Rows[0]["v2rate"].ToString());
                                        v3 = Utility.ToDouble(ds.Tables[0].Rows[0]["v3rate"].ToString());
                                        v4 = Utility.ToDouble(ds.Tables[0].Rows[0]["v4rate"].ToString());
                                    }

                                    //Check for NH , OT1, OT2 , Days
                                    DateTime enddate = new DateTime();
                                    enddate = Convert.ToDateTime(strendatedmy);
                                    DataSet dsProgression = new DataSet();
                                    string strEndDate = enddate.Year.ToString() + "-" + enddate.Month.ToString() + "-" + enddate.Day.ToString() + " 00:00:00.000";
                                    string ssql = "Sp_GetProgressionData";// 0,2009,2
                                    SqlParameter[] parms = new SqlParameter[2];
                                    parms[0] = new SqlParameter("@emp_code", empid);
                                    parms[1] = new SqlParameter("@FromDate", strEndDate);
                                    dsProgression = DataAccess.ExecuteSPDataSet(ssql, parms);

                                    double nhRate = 0.00;
                                    double ot1Rate = 0.00;
                                    double ot2Rate = 0.00;
                                    double daysRate = 0.00;
                                    double hourlyRate = 0.00;

                                    foreach (DataRow drP in dsProgression.Tables[0].Rows)
                                    {
                                        nhRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = Convert.ToDouble(drP["OT1Rate"].ToString());
                                        ot2Rate = Convert.ToDouble(drP["OT2Rate"].ToString());
                                        daysRate = Convert.ToDouble(drP["Daily_Rate"].ToString());
                                        hourlyRate = Convert.ToDouble(drP["Hourly_Rate"].ToString());
                                        ot1Rate = ot1Rate * hourlyRate;
                                        ot2Rate = ot2Rate * hourlyRate;
                                    }


                                   /* 
                                   //Hours/Days /NH/OT1/OT2/Days
                                   //Amount Parameter No need to Take in consideration
                                   string strRateCeiling = "Select *  from CeilingMaster Where CeilingType=2 And CompanyID=" + comp_id;
                                   DataSet dsRateCeiling = DataAccess.FetchRS(CommandType.Text, strRateCeiling, null);
                                    //Ceiling OT1 
                                    if (dsEmpCeilingDetails.Tables.Count > 0)
                                    {
                                        if (dsEmpCeilingDetails.Tables[0].Rows.Count > 0)
                                        {
                                            DataRow[] drNHHr = dsEmpCeilingDetails.Tables[0].Select("Emp_code=" + empcode);
                                            if (drNHHr.Length > 0)
                                            {
                                                if (dsRateCeiling.Tables.Count > 0)
                                                {
                                                    if (dsRateCeiling.Tables[0].Rows.Count > 0)
                                                    {
                                                        DataRow[] drV1 = dsRateCeiling.Tables[0].Select("Parameter='V1'");
                                                        if (drV1.Length > 0)
                                                        {
                                                            double V1Orignal = dblv1 * v1; //30 * 20 =600 
                                                            double V1New = Convert.ToDouble(drNHHr[0][7].ToString()); //300 
                                                            double dbv1or = dblv1;
                                                            if (V1Orignal > V1New)
                                                            {
                                                                //Val = 30/(600/300);        
                                                                dblv1 =Convert.ToDouble(dblv1/Convert.ToDouble(V1Orignal / V1New));
                                                                txtv1.Text = dblv1.ToString("#0.00");
                                                                txtv1.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                        DataRow[] drV2 = dsRateCeiling.Tables[0].Select("Parameter='V2'");

                                                        if (drV2.Length > 0)
                                                        {
                                                            double V2Orignal = dblv2 * v2;
                                                            double V2New = Convert.ToDouble(drNHHr[0][8].ToString());
                                                            double dbv2or = dblv2;
                                                            if (V2Orignal > V2New)
                                                            {
                                                                dblv2= Convert.ToDouble(dblv2 / Convert.ToDouble(V2Orignal / V2New));
                                                                txtv2.Text = dblv2.ToString("#0.00");
                                                                txtv2.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }



                                                        DataRow[] drV3 = dsRateCeiling.Tables[0].Select("Parameter='V3'");
                                                        if (drV3.Length > 0)
                                                        {
                                                            double V3Orignal = dblv3 * v3;
                                                            double V3New = Convert.ToDouble(drNHHr[0][9].ToString());
                                                            double dbv3or = dblv3;
                                                            if (V3Orignal > V3New)
                                                            {
                                                                dblv3 = Convert.ToDouble(dblv3 / Convert.ToDouble(V3Orignal / V3New));
                                                                txtv3.Text = dblv3.ToString("#0.00") ;
                                                                txtv3.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                        DataRow[] drV4 = dsRateCeiling.Tables[0].Select("Parameter='V4'");
                                                        if (drV4.Length > 0)
                                                        {
                                                            double V4Orignal = dblv4 * v4;
                                                            double V4New = Convert.ToDouble(drNHHr[0][10].ToString());
                                                            double dbv4or = dblv4;
                                                            if (V4Orignal > V4New)
                                                            {
                                                                dblv4 = Convert.ToDouble(dblv4 / Convert.ToDouble(V4Orignal / V4New));
                                                                txtv4.Text = dblv4.ToString("#0.00");
                                                                txtv4.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }


                                                        DataRow[] drNH = dsRateCeiling.Tables[0].Select("Parameter='NH'");
                                                        if (drNH.Length > 0)
                                                        {
                                                            double nhOrignal = l * nhRate;
                                                            double nhNew = Convert.ToDouble(drNHHr[0]["NH"].ToString());
                                                            double nhOr = l;
                                                            if (nhOrignal > nhNew)
                                                            {
                                                                l = Convert.ToDouble(l / Convert.ToDouble(nhOrignal / nhNew));
                                                                txtbox3.Text = l.ToString("#0.00");
                                                                txtbox3.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                        

                                                        DataRow[] drOt1 = dsRateCeiling.Tables[0].Select("Parameter='OT1'");
                                                        if (drOt1.Length > 0)
                                                        {
                                                            double ot1Orignal = i * ot1Rate;
                                                            double ot1New =Convert.ToDouble(drNHHr[0]["OT1"].ToString());
                                                            double ot1Or = i;
                                                            if (ot1Orignal > ot1New)
                                                            {
                                                                i = Convert.ToDouble(i / Convert.ToDouble(ot1Orignal / ot1New));
                                                                txtbox.Text = i.ToString("#0.00");
                                                                txtbox.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                        DataRow[] drOt2 = dsRateCeiling.Tables[0].Select("Parameter='OT2'");
                                                        if (drOt2.Length > 0)
                                                        {
                                                            double ot2Orignal = j * ot2Rate;
                                                            double ot2New =Convert.ToDouble(drNHHr[0]["OT2"].ToString());
                                                            double ot2Or = j;
                                                            if (ot2Orignal > ot2New)
                                                            {
                                                                j = Convert.ToDouble(j / Convert.ToDouble(ot2Orignal / ot2New));
                                                                txtbox1.Text = j.ToString("#0.00");
                                                                txtbox1.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                        DataRow[] drDays = dsRateCeiling.Tables[0].Select("Parameter='Days'");
                                                        if (drDays.Length > 0)
                                                        {
                                                            double daysOrignal = k * daysRate;
                                                            double daysNew =Convert.ToDouble(drNHHr[0]["Days"].ToString());
                                                            double daysOr = k;
                                                            if (daysOrignal > daysNew)
                                                            {
                                                                k = Convert.ToDouble(k / Convert.ToDouble(daysOrignal / daysNew));
                                                                txtbox2.Text = k.ToString("#0.00");
                                                                txtbox2.BackColor = Color.LightGoldenrodYellow;
                                                                //Label lblV1Ceil = (Label)dataItem["v1"].FindControl("lblv1");
                                                                //lblV1Ceil.Text = dbv1or.ToString() + " /{" + dbv1or.ToString() + "*" + v1.ToString() + "}/{" + dbv1or.ToString() + "*" + Convert.ToDouble(drNHHr[0][7].ToString()) + "}";
                                                                //lblV1Ceil.ToolTip = lblV1Ceil.Text;
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }*/

                                    
                                    //string date = cmbMonth.SelectedValue + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                    string date = strstdatemdy;
                                    //string v1id = Session["V1Id"].ToString();
                                    //string v2id = Session["V2Id"].ToString();
                                    //string v3id = Session["V3Id"].ToString();
                                    //string v4id = Session["V4Id"].ToString();

                                    string v1id = Convert.ToString(Session["V1Id"]);
                                    string v2id = Convert.ToString(Session["V2Id"]);
                                    string v3id = Convert.ToString(Session["V3Id"]);
                                    string v4id = Convert.ToString(Session["V4Id"]);

                                    //Getting multiplication for rate and days
                                    double ratev1 = dblv1 * v1;
                                    double ratev2 = dblv2 * v2;
                                    double ratev3 = dblv3 * v3;
                                    double ratev4 = dblv4 * v4;

                                    string ssqlv1;
                                    string chkv1;
                                    //variable1 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    DataSet dschk1 = new DataSet();
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev1 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v1id + "','" + date + "','" + ratev1 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev1 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev1 + "' where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable1 condition -END

                                    //variable2 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev2 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v2id + "','" + date + "','" + ratev2 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev2 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev2 + "' where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable2 condition -END

                                    //variable3 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev3 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v3id + "','" + date + "','" + ratev3 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev3 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev3 + "' where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable3 condition -END

                                    //variable4 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev4 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v4id + "','" + date + "','" + ratev4 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev4 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set trx_amount='" + ratev4 + "' where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable4 condition -END

                                    sSQL = "";
                                    if ((empcode == "") && ((i >= 0) || (j >= 0) || (k >= 0) || (l >= 0)))
                                    {
                                        string newdate = strstdatemdy;
                                        int icnt = 0;
                                        //DataSet monthDs = new DataSet();
                                        //string ssql = "sp_GetPayrollMonth";// 0,2009,2
                                        //SqlParameter[] parms = new SqlParameter[3];
                                        //parms[icnt++] = new SqlParameter("@ROWID", "0");
                                        //parms[icnt++] = new SqlParameter("@YEARS", 0);
                                        //parms[icnt++] = new SqlParameter("@PAYTYPE", 0);
                                        //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                                        //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);

                                        //if (cmbMonth.SelectedItem.Text.ToString().ToLower().IndexOf("first") > 0)
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //else
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "16" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        {
                                            sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,NH_Work,payrollstdate) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + l + ",'" + newdate + "')";
                                        }
                                    }
                                    else if ((empcode != ""))  //&& ( (i != 0) || (j !=0) || (k != 0)))
                                    {
                                        sSQL = "Update emp_overtime set NH_Work=" + l + ", overtime1=" + i + ",overtime2=" + j + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(dblv1) + ",v2=" + Utility.ToDouble(dblv2) + ",v3=" + Utility.ToDouble(dblv3) + ",v4=" + Utility.ToDouble(dblv4) + " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                        //lblerror.Text = "Updated Sucessfully";
                                        _actionMessage = "Success|Updated Successfully";
                                        ViewState["actionMessage"] = _actionMessage;
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();     //"Please click the go button and then insert the record for the corresponding month";
                                    }
                                }
                            }
                        }
                    }

                }
                if (e.CommandName == "UpdateAll")
                {
                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));

                                TextBox txtbox = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtbox1 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtbox2 = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtbox3 = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                                TextBox txtlateness = (TextBox)dataItem.FindControl("txtlateness");


                                //DateTime paystartdate;

                                //string sql_emp_terminationdate = "select top (1) [joining_date],[termination_date] from employee where emp_code=" + empcode;



                                //SqlDataReader dr;
                                //dr = DataAccess.ExecuteReader(CommandType.Text, sql_emp_terminationdate, null);

                                //while (dr.Read())
                                //{
                                //    if (dr.GetValue(0) != null)
                                //    {
                                //        paystartdate = Convert.ToDateTime(dr["joining_date"].ToString());

                                //        if (Convert.ToDateTime(strstdatedmy) <= paystartdate)
                                //        {
                                //            strstdatedmy = paystartdate.ToString();
                                //        }
                                //    }

                                //}




                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and (Convert(DateTime,h.start_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,h.end_period,103) <= Convert(DateTime,'" + strendatedmy + "',103)) and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);
                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {


                                    double i = Utility.ToDouble(txtbox.Text);
                                    double j = Utility.ToDouble(txtbox1.Text);
                                    double k = Utility.ToDouble(txtbox2.Text);
                                    double l = Utility.ToDouble(txtbox3.Text);
                                    double dblv1 = Utility.ToDouble(txtv1.Text);
                                    double dblv2 = Utility.ToDouble(txtv2.Text);
                                    double dblv3 = Utility.ToDouble(txtv3.Text);
                                    double dblv4 = Utility.ToDouble(txtv4.Text);
                                    decimal dbllateness = 0.0m;
                                    decimal lateness_minitute = 0.0m;
                                    if(!string.IsNullOrEmpty(txtlateness.Text))
                                    {
                                     dbllateness = Convert.ToDecimal(txtlateness.Text);
                                    }

                                    decimal hours = 0.0m;
                                    hours=Math.Floor(dbllateness); //take integral part
                                    decimal minutes=0.0m;
                                    minutes= (dbllateness - hours); //multiply fractional part with 60
                                 
                                    lateness_minitute = (hours * 60.0m) +(minutes*100.0m);



                                    status = "U";
                                    double v1, v2, v3, v4,HourlyRate,MinituteRate=0;




                                    ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate,isnull(hourly_rate,0)as Hourly_Rate,DeductLateness,IsLatenessManval,LatenessRate  from employee where emp_code='" + empid + "'";
                                    DataSet ds = new DataSet();
                                    ds = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                    bool deductlateness = false;
                                    bool iscustomrate = false;
                                    double customlaterate = 0.00;
                                    v1 = 0;
                                    v2 = 0;
                                    v3 = 0;
                                    v4 = 0;
                                    HourlyRate = 0;
                                    MinituteRate = 0;
                                    if (ds.Tables[0].Rows.Count != 0)
                                    {

                                        if (Utility.ToString(ds.Tables[0].Rows[0]["DeductLateness"]) == "True")
                                        {
                                            deductlateness = true;
                                        }

                                        if (Utility.ToString(ds.Tables[0].Rows[0]["IsLatenessManval"]) == "True")
                                        {
                                            iscustomrate = true;
                                        }

                                        customlaterate = Utility.ToDouble(ds.Tables[0].Rows[0]["LatenessRate"].ToString());


                                      

                                        //HourlyRate = Utility.ToDouble(ds.Tables[0].Rows[0]["Hourly_Rate"].ToString());


                                        v1 = Utility.ToDouble(ds.Tables[0].Rows[0]["v1rate"].ToString());
                                        v2 = Utility.ToDouble(ds.Tables[0].Rows[0]["v2rate"].ToString());
                                        v3 = Utility.ToDouble(ds.Tables[0].Rows[0]["v3rate"].ToString());
                                        v4 = Utility.ToDouble(ds.Tables[0].Rows[0]["v4rate"].ToString());

                                        HourlyRate = Utility.ToDouble(ds.Tables[0].Rows[0]["Hourly_Rate"].ToString());

                                    }

                                    if (HourlyRate > 0)
                                    {
                                        MinituteRate = HourlyRate / 60.0;
                                    }

                                    CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(1);


                                    //string date = cmbMonth.SelectedValue + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                    string date = strstdatemdy;
                                    //string v1id = Session["V1Id"].ToString();
                                    //string v2id = Session["V2Id"].ToString();
                                    //string v3id = Session["V3Id"].ToString();
                                    //string v4id = Session["V4Id"].ToString();

                                    int _year = Convert.ToDateTime(date).Year;

                                    string v1id = Convert.ToString(Session["V1Id"]);
                                    string v2id = Convert.ToString(Session["V2Id"]);
                                    string v3id = Convert.ToString(Session["V3Id"]);
                                    string v4id = Convert.ToString(Session["V4Id"]);


                                    //Getting multiplication for rate and days
                                    double ratev1 = dblv1 * v1;
                                    double ratev2 = dblv2 * v2;
                                    double ratev3 = dblv3 * v3;
                                    double ratev4 = dblv4 * v4;
                                    decimal rateLatenes = 0.00m;
                                    if (deductlateness)
                                    {
                                        if (iscustomrate)
                                        {
                                            if (customlaterate > 0)
                                            {
                                                MinituteRate = customlaterate / 60.0;
                                                rateLatenes = lateness_minitute * Convert.ToDecimal(MinituteRate);
                                            }

                                          
                                        }
                                        else
                                        {
                                        rateLatenes = lateness_minitute * Convert.ToDecimal(MinituteRate);
                                        }

                                    }

                                    string ssqlv1;
                                    string chkv1;
                                    //variable1 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    DataSet dschk1 = new DataSet();
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev1 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,amount,trx_amount,emp_code,status,additionsforyear,claimstatus) values('" + v1id + "','" + date + "','" + ratev1 + "','" +  ratev1 + "','" + empid + "','" + status + "','" + _year + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev1 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set amount='" + ratev1 + "', trx_amount='" + ratev1 + "' where emp_code='" + empid + "' and trx_type='" + v1id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable1 condition -END

                                    //variable2 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev2 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,amount,emp_code,status,additionsforyear,claimstatus) values('" + v2id + "','" + date + "','" + ratev2 + "','" + ratev2 + "','" + empid + "','" + status + "','" + _year + "'," + "'Approved')";
                                        //    ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v2id + "','" + date + "','" + ratev2 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev2 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set amount='" + ratev2 + "', trx_amount='" + ratev2 + "' where emp_code='" + empid + "' and trx_type='" + v2id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable2 condition -END

                                    //variable3 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev3 > 0)
                                        {
                                            ssqlv1 = ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,amount,emp_code,status,additionsforyear,claimstatus) values('" + v3id + "','" + date + "','" + ratev3 + "','" + ratev3 + "','" + empid + "','" + status + "','" + _year + "'," + "'Approved')";
                                          //  ssqlv1 = "insert into emp_additions(trx_type,trx_period,trx_amount,emp_code,status,claimstatus) values('" + v3id + "','" + date + "','" + ratev3 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev3 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set amount='" + ratev3 + "', trx_amount='" + ratev3 + "' where emp_code='" + empid + "' and trx_type='" + v3id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable3 condition -END

                                    //variable4 condition -START
                                    chkv1 = "select trx_type,trx_period,emp_code from emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                    dschk1 = DataAccess.FetchRS(CommandType.Text, chkv1, null);
                                    if (dschk1.Tables[0].Rows.Count == 0)
                                    {
                                        if (ratev4 > 0)
                                        {
                                            ssqlv1 = "insert into emp_additions(trx_type,trx_period,amount,trx_amount,emp_code,status,claimstatus) values('" + v4id + "','" + date + "','" + ratev4 + "','" + ratev4 + "','" + empid + "','" + status + "'," + "'Approved')";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                    }
                                    else
                                    {
                                        if (ratev4 <= 0)
                                        {
                                            ssqlv1 = "Delete From emp_additions where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retv1 = DataAccess.ExecuteStoreProc(ssqlv1);
                                        }
                                        ssqlv1 = "update emp_additions set amount='" + ratev4 + "',trx_amount='" + ratev4 + "' where emp_code='" + empid + "' and trx_type='" + v4id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        int retv2 = DataAccess.ExecuteStoreProc(ssqlv1);
                                    }
                                    //variable4 condition -END

                                    //lateness
                                  int lateness_id = 0;

                                  string  lateness_sql = "select id from deductions_types where [desc]='Lateness'";
                                    DataSet ds1 = new DataSet();
                                    ds1 = DataAccess.FetchRS(CommandType.Text, lateness_sql, null);
                                    if (ds1.Tables[0].Rows.Count != 0)
                                    {
                                        lateness_id = Utility.ToInteger(ds1.Tables[0].Rows[0]["id"].ToString());

                                        string ssqllateness;
                                        string chklateness;
                                        //variable1 condition -START
                                        chklateness = "select trx_type,trx_period,emp_code from emp_deductions where emp_code='" + empid + "' and trx_type='" + lateness_id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                        DataSet dschlateness = new DataSet();
                                        dschlateness = DataAccess.FetchRS(CommandType.Text, chklateness, null);
                                        if (dschlateness.Tables[0].Rows.Count == 0)
                                        {
                                            if (rateLatenes > 0)
                                            {
                                                ssqllateness = "insert into emp_deductions(trx_type,trx_period,trx_amount,amount,emp_code,status) values('" + lateness_id + "','" + date + "','" + rateLatenes + "','" + rateLatenes + "','" + empid + "','" + status + "')";
                                                int retlateness = DataAccess.ExecuteStoreProc(ssqllateness);
                                            }
                                        }
                                        else
                                        {
                                            if (rateLatenes <= 0)
                                            {
                                                ssqllateness = "Delete From emp_deductions where emp_code='" + empid + "' and trx_type='" + lateness_id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                                int retlateness1 = DataAccess.ExecuteStoreProc(ssqllateness);
                                            }
                                            ssqllateness = "update emp_deductions set amount='" + rateLatenes + "', trx_amount='" + rateLatenes + "' where emp_code='" + empid + "' and trx_type='" + lateness_id + "' and (Convert(DateTime,trx_period,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,trx_period,103) <= Convert(DateTime,'" + strendatedmy + "',103))";
                                            int retlateness2 = DataAccess.ExecuteStoreProc(ssqllateness);
                                        }
                                    }

                                    sSQL = "";
                                    if ((empcode == "") && ((i >= 0) || (j >= 0) || (k >= 0) || (l >= 0)))
                                    {
                                        string newdate = strstdatemdy;
                                        int icnt = 0;
                                        //DataSet monthDs = new DataSet();
                                        //string ssql = "sp_GetPayrollMonth";// 0,2009,2
                                        //SqlParameter[] parms = new SqlParameter[3];
                                        //parms[icnt++] = new SqlParameter("@ROWID", "0");
                                        //parms[icnt++] = new SqlParameter("@YEARS", 0);
                                        //parms[icnt++] = new SqlParameter("@PAYTYPE", 0);
                                        //monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
                                        //DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);

                                        //if (cmbMonth.SelectedItem.Text.ToString().ToLower().IndexOf("first") > 0)
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "01" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //else
                                        //{
                                        //    newdate = drResults[0]["Month"].ToString() + "/" + "16" + "/" + cmbYear.SelectedValue + "";
                                        //}
                                        //if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        //{
                                        //    sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,Lateness,NH_Work,payrollstdate) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + Utility.ToDouble(dbllateness) + "," + l + ",'" + newdate + "')";
                                        //}
                                        
                                        //Senthil Added-For Overtime period

                                        string rdfrom = rdFrom.SelectedDate.ToString();
                                        if (Utility.ToDouble(dblv1) > -1000 || Utility.ToDouble(dblv2) > -1000 || Utility.ToDouble(dblv3) > -1000 || Utility.ToDouble(dblv4) > -1000 || Utility.ToDouble(i) > -1000 || Utility.ToDouble(j) > -1000 || Utility.ToDouble(k) > -1000 || Utility.ToDouble(l) > -1000)
                                        {
                                            sSQL = "Insert into emp_overtime (emp_code,overtime1,overtime2,trx_date,trx_month,trx_year,days_work,v1,v2,v3,v4,Lateness,NH_Work,payrollstdate,OvertimeStart,OvertimeEnd) values(" + empid + "," + i + "," + j + ", getdate(), " + cmbMonth.SelectedValue + "," + cmbYear.SelectedValue + "," + Utility.ToDouble(k) + "," + Utility.ToDouble(dblv1) + "," + Utility.ToDouble(dblv2) + "," + Utility.ToDouble(dblv3) + "," + Utility.ToDouble(dblv4) + "," + Utility.ToDouble(dbllateness) + "," + l + ",'" + newdate + "', convert(datetime,'" + rdFrom.SelectedDate.ToString() + "',103),convert(datetime,'" + rdEnd.SelectedDate.ToString() + "',103))";
                                        }

                                    }
                                    else if ((empcode != ""))  //&& ( (i != 0) || (j !=0) || (k != 0)))
                                    {
                                        sSQL = "Update emp_overtime set NH_Work=" + l + ", overtime1=" + i + ",overtime2=" + j + ",days_work=" + Utility.ToDouble(k) + ",v1=" + Utility.ToDouble(dblv1) 
                                            + ",v2=" + Utility.ToDouble(dblv2) + ",v3=" + Utility.ToDouble(dblv3) 
                                            + ",v4=" + Utility.ToDouble(dblv4) + ",Lateness=" + Utility.ToDouble(dbllateness)
                                            + ",OvertimeStart=convert(datetime,'" + rdFrom.SelectedDate.ToString()+"',103)" +
                                             ",OvertimeEnd=convert(datetime,'" + rdEnd.SelectedDate.ToString()+"',103)" +
                                            
                                            " where emp_code=" + empcode + " and (Convert(DateTime,PayRollStDate,103) >= Convert(DateTime,'" + strstdatedmy + "',103) And Convert(DateTime,PayRollStDate,103) <= Convert(DateTime,'" + strendatedmy + "',103)) And trx_year=" + cmbYear.SelectedValue; //+ " and id=" + id;
                                    }
                                    try
                                    {
                                        if (sSQL != "")
                                            DataAccess.ExecuteStoreProc(sSQL);
                                        //lblerror.Text = "Updated Sucessfully";
                                        _actionMessage = "Success|Updated Successfully";
                                        ViewState["actionMessage"] = _actionMessage;
                                    }
                                    catch (Exception msg)
                                    {
                                        lblerror.Text = msg.Message.ToString();     //"Please click the go button and then insert the record for the corresponding month";
                                    }
                                }
                            }
                        }
                    }
                    // RadGrid1.DataBind();
                }
                if (e.CommandName == "CalcOverVar")
                {
                                      
                    string time_card_no = "";
                    string srSql;
                    string empcode;
                    string empid;
                    int id;
                    string strInTime = "";
                    string strOutTime = "";
                    string strOT = "";
                    double intot1;
                    double intotcalc;
                    int inthour;
                    int intmin;
                    double inttotmin;
                    double intworkinghours;
                    double intworkingmints;
                    double intworkingtotmin;
                    double intbalancemin = 0;
                    double intdaysworkedhrmin = 0;
                    double intdaysworked = 0;
                    double intdaysworkedhrsmon = 0;
                    double intdaysworkedminmon = 0;


                    //srSql = "Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105)) date,convert(varchar, timeentry, 8) timeentry From actatek_logs  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%IN%' and A.softdelete=0 and Month(CONVERT(datetime,timeentry, 105) )=" + cmbMonth.SelectedValue + " and year(CONVERT(datetime,timeentry, 105) )=" + cmbYear.SelectedValue + " Order By timeentry";
                    //srSql = srSql + ";Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105) ) date,convert(varchar, timeentry, 8) timeentry From actatek_logs  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%OUT%' and A.softdelete=0 and Month(CONVERT(datetime,timeentry, 105) )=" + cmbMonth.SelectedValue + " and year(CONVERT(datetime,timeentry, 105) )=" + cmbYear.SelectedValue + " Order By timeentry Desc";
                    //srSql = srSql + ";Select * From DaysInMonth where month=" + cmbMonth.SelectedValue + " and year=" + cmbYear.SelectedValue;
                    //srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From ACTATEK_LOGS A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where year(CONVERT(datetime,Res.[month], 103))= " + cmbYear.SelectedValue + " and Month(CONVERT(datetime,Res.[month], 103))= " + cmbMonth.SelectedValue + "  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where Month(TimeEntryStart)=" + cmbMonth.SelectedValue + " and Year(TimeEntryStart)=" + cmbYear.SelectedValue + ") D Group By Time_Card_No";
                   
                    //kumar
                    //srSql = "Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105)) date,convert(varchar, timeentry, 8) timeentry From actatek_logs_proxy  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%IN%' and A.softdelete=0 and (CONVERT(datetime,timeentry, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,timeentry, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105))  Order By timeentry";
                    //srSql = srSql + ";Select userid,E.Emp_code,day(CONVERT(datetime,timeentry, 105) ) date,convert(varchar, timeentry, 8) timeentry From actatek_logs_proxy  A Inner Join Employee E On A.UserID= E.Time_Card_No where upper(A.EventID) Like '%OUT%' and A.softdelete=0 and (CONVERT(datetime,timeentry, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,timeentry, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105)) Order By timeentry Desc";
                    //srSql = srSql + ";Select DATEDIFF(day,Convert(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103),Convert(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103)) calendar_days";
                    //srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where (CONVERT(datetime,InTime, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103) And CONVERT(datetime,OutTime, 103)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103))  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    ////srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryEnd,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";

                    //srSql = "Select a.Time_Card_No as userid,E.Emp_code,day(CONVERT(datetime,TimeEntryStart, 105)) date,convert(varchar, TimeEntryStart, 20) TimeEntryStart,convert(varchar, TimeEntryEnd, 20) TimeEntryEnd From ApprovedTimeSheet   A Inner Join Employee E On A.Time_Card_No= E.Time_Card_No where  (CONVERT(datetime,TimeEntryStart, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,TimeEntryStart, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy 23:59", format) + "', 105))  Order By TimeEntryStart";
                    //srSql = srSql + ";Select a.Time_Card_No as userid,E.Emp_code,day(CONVERT(datetime,TimeEntryEnd, 105)) date,convert(varchar, TimeEntryEnd, 20) timeentry From ApprovedTimeSheet   A Inner Join Employee E On A.Time_Card_No= E.Time_Card_No where  (CONVERT(datetime,TimeEntryEnd, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,TimeEntryEnd, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy 23:59", format) + "', 105))  Order By TimeEntryStart";
                    //srSql = srSql + ";Select DATEDIFF(day,Convert(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103),Convert(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103)) calendar_days";
                    //srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where (CONVERT(datetime,InTime, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103) And CONVERT(datetime,OutTime, 103)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103))  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    ////srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryEnd,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";

                    srSql = "Select a.Time_Card_No as userid,E.Emp_code,day(CONVERT(datetime,TimeEntryStart, 105)) date,convert(varchar, TimeEntryStart, 20) TimeEntryStart,convert(varchar, TimeEntryEnd, 20) TimeEntryEnd From ApprovedTimeSheet   A Inner Join Employee E On A.Time_Card_No= E.Time_Card_No inner join TimeSheetMangment t on t.RefId= A.refid  where t.[Status]='Approved' and  (CONVERT(datetime,TimeEntryStart, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,TimeEntryStart, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy 23:59", format) + "', 105))  Order By TimeEntryStart";
                    srSql = srSql + ";Select a.Time_Card_No as userid,E.Emp_code,day(CONVERT(datetime,TimeEntryEnd, 105)) date,convert(varchar, TimeEntryEnd, 20) timeentry From ApprovedTimeSheet   A Inner Join Employee E On A.Time_Card_No= E.Time_Card_No where  (CONVERT(datetime,TimeEntryEnd, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,TimeEntryEnd, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy 23:59", format) + "', 105))  Order By TimeEntryStart";
                    srSql = srSql + ";Select DATEDIFF(day,Convert(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103),Convert(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103)) calendar_days";
                    srSql = srSql + ";Select Res.Emp_Code,day(CONVERT(datetime,Res.[month], 103)) [date],ph.ID,datename(dw,CONVERT(datetime,Res.[month], 103)) Day_Name From (Select Em.Emp_Code,Em.Emp_Name,TS.* From (Select 'Time_Card_No' = CASE  WHEN InUserID IS NULL THEN OutUserId Else InUserID END, 'TranID' = CASE  WHEN InTranID IS NULL THEN OutTranID Else InTranID END, 'Month' = CASE  WHEN InMonth IS NULL THEN OutMonth  Else InMonth END, InTime,OutTime,CONVERT(CHAR(5), CONVERT(datetime,InTime, 105), 108) InShortTime,CONVERT(CHAR(5), CONVERT(datetime,OutTime, 105), 108) OutShortTime From(Select TSIN.UserId InUserID,TSIN.TranID InTranID,TSIN.[Month] InMonth,TSIN.TimeEntry as [InTime],TSOUT.UserId OutUserID,TSOUT.TranID OutTranID,TSOUT.[Month] OutMonth,TSOut.TimeEntry as [OutTime] From  (Select  [Month],TranID,UserID,EventID,MIN(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where softdelete=0 and upper(EventID) Like '%IN%' Group By TranID,USerID,EventID,[Month]) TSIN FULL OUTER JOIN( Select [Month],TranID,UserID,EventID,MAX(CONVERT(datetime,TimeEntry, 103)) TimeEntry From (SELECT *,CONVERT(VARCHAR, CONVERT(datetime,A.TimeEntry, 105), 103) [Month] From actatek_logs_proxy A ) As Ts Where upper(EventID) Like '%OUT%' and softdelete=0 Group By TranID,USerID,EventID,[Month] ) TSOUT On  TSIN.TranID=TSOUT.TranID And TSIN.UserID=TSOUT.UserID And TSIN.[Month]=TSOUT.[Month]) TSOutPut ) TS Inner Join Employee Em On TS.Time_Card_No = Em.Time_Card_No)  [Res]  Left Outer Join public_holidays [ph] On CONVERT(datetime,Res.[month], 103) = CONVERT(datetime,ph.holiday_date, 103) Where (CONVERT(datetime,InTime, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103) And CONVERT(datetime,OutTime, 103)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 103))  and datename(dw,CONVERT(datetime,Res.[month], 103)) = 'Sunday' or ph.id is not null Order By Res.[month]";
                    //srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	Time_Card_No,dbo.ConvertToMinutes(NH) NH,dbo.ConvertToMinutes(OT1) OT1,dbo.ConvertToMinutes(OT2) OT2,dbo.ConvertToMinutes(TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryEnd,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0) D Group By Time_Card_No";
                    srSql = srSql + ";Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk From (Select	A.Time_Card_No,dbo.ConvertToMinutes(A.NH) NH,dbo.ConvertToMinutes(A.OT1) OT1,dbo.ConvertToMinutes(A.OT2) OT2,dbo.ConvertToMinutes(A.TotalHrsWrk) TotalHrsWrk From ApprovedTimeSheet  A inner join  TimeSheetMangment  T on A.RefID=T.RefId Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0 and t.[Status]='Approved') D Group By Time_Card_No";

                    int noofdaywork = 0;

                    int intbldcount = 5;

                    DataSet ds = new DataSet();
                    DataSet dsTS = new DataSet();
                    ds = DataAccess.FetchRS(CommandType.Text, srSql, null);
                    int intCountV1 = 0;
                    int intCountV2 = 0;
                    int intCountV3 = 0;
                    int intCountV4 = 0;

                    int intCountV11 = 0;
                    int intCountV22 = 0;
                    int intCountV33 = 0;
                    int intCountV44 = 0;

                    double v1, v2, v3, v4, wdayperweek;
                    int intdaysinmonth = 0;
                    int NoOfDaysInMonth = 0;

                    if (ds.Tables.Count == intbldcount)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            intdaysinmonth = Convert.ToInt32(ds.Tables[2].Rows[0]["calendar_days"]);
                        }

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            noofdaywork = ds.Tables[0].Rows.Count;
                        }
                    }
                        int _month= GetMonth(Convert.ToInt32(cmbMonth.SelectedValue.ToString()));
                    NoOfDaysInMonth= DateTime.DaysInMonth(Convert.ToInt32(cmbYear.SelectedValue.ToString()),_month);
                
                    //SqlParameter[] parms1 = new SqlParameter[7];
                    //parms1[0] = new SqlParameter("@start_date", rdFrom.SelectedDate.ToString("dd/MM/yyyy"));
                    //parms1[1] = new SqlParameter("@end_date", rdEnd.SelectedDate.ToString("dd/MM/yyyy"));
                    //parms1[2] = new SqlParameter("@compid", comp_id);
                    //parms1[3] = new SqlParameter("@isEmpty", "No");
                    //parms1[4] = new SqlParameter("@empid", "-1");
                    //parms1[5] = new SqlParameter("@subprojid", "-1");
                    //parms1[6] = new SqlParameter("@sessid", 0);
                    //dsTS = DataAccess.ExecuteSPDataSet("sp_ProcessTimesheet", parms1);



                    foreach (GridItem item in RadGrid1.MasterTableView.Items)
                    {
                        if (item is GridItem)
                        {
                            GridDataItem dataItem = (GridDataItem)item;
                            CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                            if (chkBox.Checked == true)
                            {
                                intCountV1 = 0;
                                intCountV2 = 0;
                                intCountV3 = 0;
                                intCountV4 = 0;
                                srSql = "";
                                empcode = "";
                                empid = "";
                                empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                                time_card_no = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("time_card_no"));
                                TextBox txtot1 = (TextBox)dataItem.FindControl("txtovertime");
                                TextBox txtot2 = (TextBox)dataItem.FindControl("txtovertime2");
                                TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                                TextBox txtdayswork = (TextBox)dataItem.FindControl("txtDaysWork");
                                TextBox txtNHWork = (TextBox)dataItem.FindControl("txtNHWork");
                                TextBox txtlateness = (TextBox)dataItem.FindControl("txtlateness");

                                strInTime = "";
                                strOutTime = "";
                                string ssql9 = "select d.emp_id,isnull(d.status,'') as status from prepare_payroll_detail d,prepare_payroll_hdr h";
                                ssql9 = ssql9 + " where d.trx_id=h.trx_id and d.emp_id='" + empid + "' and month(h.start_period)='" + cmbMonth.SelectedValue + "' and year(h.start_period)='" + cmbYear.SelectedValue + "' and d.status not in('R')";
                                ssql9 = ssql9 + ";Select Count(distinct TimeEntryStart) as totaldays From ApprovedTimeSheet   A Inner Join Employee E On A.Time_Card_No= E.Time_Card_No where  (CONVERT(datetime,TimeEntryStart, 105)>=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "', 105) And CONVERT(datetime,TimeEntryStart, 105)<=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy 23:59", format) + "', 105))and A.Time_Card_No='" + time_card_no + "'";
                                
                                string status;
                                DataSet dsStatus = new DataSet();
                                dsStatus = DataAccess.FetchRS(CommandType.Text, ssql9, null);

                                if (dsStatus.Tables[1].Rows.Count > 0)
                                {
                                    intCountV11 = Utility.ToInteger(dsStatus.Tables[1].Rows[0]["totaldays"].ToString());
                                    intCountV22 = Utility.ToInteger(dsStatus.Tables[1].Rows[0]["totaldays"].ToString());
                                    intCountV33 = Utility.ToInteger(dsStatus.Tables[1].Rows[0]["totaldays"].ToString());
                                    intCountV44 = Utility.ToInteger(dsStatus.Tables[1].Rows[0]["totaldays"].ToString());
                                   
                                }

                                if (dsStatus.Tables[0].Rows.Count == 0)
                                {
                                    status = "";
                                }
                                else
                                {
                                    status = Convert.ToString(dsStatus.Tables[0].Rows[0]["status"]).ToString();
                                }

                                if (status == "G" || status == "P" || status == "A")
                                {
                                }
                                else
                                {
                                    if (ds.Tables.Count == intbldcount)
                                    {
                                        DataRow[] dr;
                                        DataRow[] drpubholday;

                                        ssqle = "select isnull(v1rate,0) as v1rate, isnull(v2rate,0) as v2rate,isnull(v3rate,0) as v3rate,isnull(v4rate,0) as v4rate, ot_entitlement,pay_frequency, payrate,wdays_per_week from employee where emp_code='" + empid + "'";
                                        //here check if employee is Hourly then one need to just total the hours for each day....
                                        DataSet dsRate = new DataSet();
                                        dsRate = DataAccess.FetchRS(CommandType.Text, ssqle, null);
                                        if (dsRate.Tables[0].Rows.Count == 0)
                                        {
                                            v1 = 0;
                                            v2 = 0;
                                            v3 = 0;
                                            v4 = 0;
                                            wdayperweek = 0;
                                        }
                                        else
                                        {
                                            v1 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v1rate"].ToString());
                                            v2 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v2rate"].ToString());
                                            v3 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v3rate"].ToString());
                                            v4 = Utility.ToDouble(dsRate.Tables[0].Rows[0]["v4rate"].ToString());
                                            wdayperweek = Utility.ToDouble(dsRate.Tables[0].Rows[0]["wdays_per_week"].ToString());
                                            strOT = dsRate.Tables[0].Rows[0]["ot_entitlement"].ToString().Trim().ToUpper();
                                            //dataItem["v1"].Text = v1.ToString();
                                        }

                                        if ((strOT == "Y") || (txtdayswork.Enabled == true))
                                        {
                                            intdaysworkedhrmin = 0;
                                            intdaysworkedhrsmon = 0;
                                            intdaysworkedminmon = 0;
                                            txtot1.Text = "0";
                                            txtot2.Text = "0";
                                            intdaysworked = 0;
                                            intdaysworkedhrmin = Utility.ToDouble(Session["WorkingHours"]) + Utility.ToDouble(Session["WorkingMinutes"]);

                                            if (time_card_no.ToString().Trim().Length > 0)
                                            {
                                                dr = ds.Tables[4].Select("Time_Card_No ='" + time_card_no + "'");

                                                if (dr.Length > 0)
                                                {
                                                    if (dr[0]["NH"].ToString() == "0.00" || dr[0]["NH"].ToString() == "")
                                                    {
                                                        txtNHWork.Text = "";
                                                    }
                                                    else
                                                    {
                                                        if (txtNHWork.Enabled == true)
                                                        {
                                                            txtNHWork.Text = dr[0]["NH"].ToString();
                                                        }
                                                        else
                                                        {
                                                            txtNHWork.Text = "";
                                                        }
                                                        if (txtdayswork.Enabled)
                                                        {
                                                            if (dr[0]["NH"].ToString() != "0" && dr[0]["NH"].ToString() != "")
                                                            {
                                                                double hours = Convert.ToDouble(dr[0]["NH"].ToString()) / 8;
                                                                txtdayswork.Text = hours.ToString();
                                                            }
                                                        }
                                                    }
                                                    if (dr[0]["OT1"].ToString() == "0.00" || dr[0]["OT1"].ToString() == "")
                                                    {
                                                        txtot1.Text = "";
                                                    }
                                                    else
                                                    {
                                                        txtot1.Text = dr[0]["OT1"].ToString();
                                                    }
                                                    if (dr[0]["OT2"].ToString() == "0.00" || dr[0]["OT2"].ToString() == "")
                                                    {
                                                        txtot2.Text = "";
                                                    }
                                                    else
                                                    {
                                                        txtot2.Text = dr[0]["OT2"].ToString();
                                                    }
                                                }
                                            }


                                            dr = ds.Tables[0].Select("Emp_Code ='" + empid+"'");
                                            //km 
                                            for (int i = 0; i <= dr.Length-1; i++)
                                            {
                                                strInTime = "";
                                                strOutTime = "";
                                                //String DAYSTRING = ds.Tables[0].Rows[i][3].ToString();
                                                //int _DAY =Convert.ToDateTime(ds.Tables[0].Rows[i][3].ToString()).Day;
                                             
                                                   
                                               

                                                drpubholday = ds.Tables[3].Select("Emp_Code ='" + empid + "' And Date=" + i);
                                             
                                                if (dr.Length > 0)
                                                {
                                                    strInTime = dr[i]["TimeEntryStart"].ToString();
                                                    strOutTime = dr[i]["TimeEntryEnd"].ToString();
                                                }

                                                //dr = ds.Tables[1].Select("Emp_Code ='" + empid + "' And Date=" + i);

                                                //if (dr.Length > 0)
                                                //{
                                                //    strOutTime = dr[0]["TimeEntryEnd"].ToString();
                                                //}

                                                

                                                if (strInTime != "" && strOutTime != "")
                                                {
                                                    #region "DELETED"
                                                    //if (Session["TimeSheetApproved"] != null)
                                                    //{
                                                    //    if (Session["TimeSheetApproved"].ToString() == "1") //Timesheet Approved
                                                    //    {
                                                    //        try
                                                    //        {
                                                    //            DateTime dtIn = DateTime.Parse(strInTime, format);
                                                    //            DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                    //            SMEDateTime timein = new SMEDateTime(dtIn.Hour, dtIn.Minute, dtIn.Second);
                                                    //            SMEDateTime timeout = new SMEDateTime(dtOut.Hour, dtOut.Minute, dtOut.Second);
                                                    //            SMEDateTime smetime = new SMEDateTime();

                                                    //            inthour = SMEDateTime.TimeDiff(timeout, timein).Hour;
                                                    //            intmin = SMEDateTime.TimeDiff(timeout, timein).Minute;
                                                    //            inttotmin = (inthour * 60) + intmin;

                                                    //            intworkinghours = Utility.ToDouble(Session["WorkingHours"]) * 60;
                                                    //            intworkingmints = Utility.ToDouble(Session["WorkingMinutes"]);
                                                    //            intworkingtotmin = intworkinghours + intworkingmints;
                                                    //            intbalancemin = 0;

                                                    //            if (inttotmin > intworkingtotmin) //worked hrs is more than the expected hours i.e ot
                                                    //            {
                                                    //                if (wdayperweek == 5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat" || dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For saturday and sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 5.5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - (intworkingtotmin / 2); // For saturday and half day as OT
                                                    //                    }
                                                    //                    else if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 6)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }
                                                    //                if (wdayperweek == 7)
                                                    //                {
                                                    //                    intbalancemin = inttotmin - intworkingtotmin; //overtime hour to be charged for regular days
                                                    //                }

                                                    //                if (strOT == "Y")
                                                    //                {
                                                    //                    if (intbalancemin > 0)
                                                    //                    {

                                                    //                        if (drpubholday.Length > 0)
                                                    //                        {
                                                    //                            if (drpubholday[0]["id"] != DBNull.Value)
                                                    //                            {
                                                    //                                // This is a Public Holdiday hence whole day worked should be counted
                                                    //                                intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                                intotcalc = Utility.ToDouble(inttotmin / 60) + intot1;
                                                    //                                txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                            }
                                                    //                            else
                                                    //                            {
                                                    //                                //This is a Sunday type of holiday
                                                    //                                intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                                intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                                txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                            }
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            intot1 = Utility.ToDouble(txtot1.Text);
                                                    //                            intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                            txtot1.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                    }
                                                    //                    //if OT entitled yes
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + (inttotmin - intbalancemin);
                                                    //                }
                                                    //                else
                                                    //                {
                                                    //                    //if OT Entit is no then all the hours will b treated in days hour worked
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + inttotmin;
                                                    //                }
                                                    //            }
                                                    //            else
                                                    //            {
                                                    //                //total time for the day is worked is less than the actual working time.
                                                    //                string eid = empid;
                                                    //                intbalancemin = Math.Abs(intbalancemin);

                                                    //                if (wdayperweek == 5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat" || dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        intbalancemin = inttotmin; // For saturday and sunday worked whole day as OT
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days is 0 since it is less 
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 5.5)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sat")
                                                    //                    {
                                                    //                        if (intbalancemin > (intworkingtotmin / 2)) // For saturday and half day as OT
                                                    //                        {
                                                    //                            intbalancemin = (intbalancemin - (intworkingtotmin / 2));
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            intbalancemin = 0;
                                                    //                        }

                                                    //                    }
                                                    //                    else if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        // For sunday worked hour which is already calcuated will be done.
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days is 0 since it is less 
                                                    //                    }
                                                    //                }

                                                    //                if (wdayperweek == 6)
                                                    //                {
                                                    //                    if (dtIn.ToString("ddd") == "Sun")
                                                    //                    {
                                                    //                        // For sunday worked hour which is already calcuated will be done.
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intbalancemin = 0; //overtime hour to be charged for regular days
                                                    //                    }
                                                    //                }
                                                    //                if (wdayperweek == 7)
                                                    //                {
                                                    //                    intbalancemin = 0; //overtime hour to be charged for regular days
                                                    //                }
                                                    //                if (strOT == "Y")
                                                    //                {
                                                    //                    if (drpubholday.Length > 0)
                                                    //                    {
                                                    //                        if (drpubholday[0]["id"] != DBNull.Value)
                                                    //                        {
                                                    //                            // This is a Public Holdiday hence whole day worked should be counted
                                                    //                            intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                            intotcalc = Utility.ToDouble(inttotmin / 60) + intot1;
                                                    //                            txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                        else
                                                    //                        {
                                                    //                            //This is a Sunday type of holiday
                                                    //                            intot1 = Utility.ToDouble(txtot2.Text);
                                                    //                            intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                            txtot2.Text = intotcalc.ToString("#0.00");
                                                    //                        }
                                                    //                    }
                                                    //                    else
                                                    //                    {
                                                    //                        intot1 = Utility.ToDouble(txtot1.Text);
                                                    //                        intotcalc = Utility.ToDouble(intbalancemin / 60) + intot1;
                                                    //                        txtot1.Text = intotcalc.ToString("#0.00");
                                                    //                    }
                                                    //                    //if OT entitled yes
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + (inttotmin - intbalancemin);
                                                    //                }
                                                    //                else
                                                    //                {
                                                    //                    //if OT Entit is no then all the hours will b treated in days hour worked
                                                    //                    intdaysworkedminmon = intdaysworkedminmon + inttotmin;
                                                    //                }
                                                    //            }


                                                    //        }
                                                    //        catch (Exception ex)
                                                    //        {
                                                    //            throw ex;
                                                    //        }
                                                    //    }
                                                    //}
                                                    #endregion
                                                    DateTime intimeday = DateTime.Parse(strInTime, format);
                                                    DateTime outtimeday = DateTime.Parse(strOutTime, format);
                                                    //If Variable Pay is Applicable...
                                                    if (txtv1.Enabled == true)
                                                    {
                                                        if (Session["V1Formula"] != null)
                                                        {
                                                            if (Session["V1Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV1 = intCountV1 + 1;
                                                               // if (txtNHWork.Enabled == true)
                                                               // {
                                                               //     if (Session["V1FormulaCalc"].ToString() == "")
                                                               //     {
                                                               //         if(!string.IsNullOrEmpty(txtNHWork.Text))
                                                               //         {
                                                               //         intCountV1 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / 8);
                                                               //         }
                                                               //     }
                                                               //     else
                                                               //     {
                                                               //         if (Session["V1FormulaCalc"].ToString() != "0")
                                                               //         {
                                                               //             intCountV1 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / Convert.ToInt32(Session["V1FormulaCalc"].ToString()));
                                                               //         }

                                                               //     }
                                                               //}

                                                           
                                                             
                                                                if(intimeday != DateTime.MinValue) 
                                                                                                                             
                                                                {

                                                                    intCountV1 = intCountV11;
                                                                }
                                                              
                                                            }
                                                            if (Session["V1Formula"].ToString() == "2") //TIME
                                                            {
                                                                //DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["V1FormulaCalc"].ToString());
                                                                //DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                //if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                //{
                                                                //    intCountV1 = intCountV1 + 1;
                                                                //}

                                                                //km
                                                                DateTime intime = DateTime.Parse(strInTime, format);
                                                                DateTime outtime = DateTime.Parse(strOutTime, format);
                                                                DateTime intime_formula = DateTime.Parse(Session["V1InTime"].ToString(), format);
                                                                DateTime outtime_formula = DateTime.Parse(Session["V1OutTime"].ToString(), format);


                                                                if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                {
                                                                    int RERIN = TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay);
                                                                    int REROUT = TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay);
                                                                    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) <= 0 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) > 0)
                                                                    {
                                                                        intCountV1 = intCountV1 + 1;
                                                                    }
                                                                }                           //else if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == 1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == 1)
                                                                //{
                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == 1)
                                                                //    {
                                                                //        intCountV1 = intCountV1 + 1;
                                                                //    }
                                                                //}

                                                            }
                                                        }
                                                    }

                                                    //If Variable Pay is Applicable...
                                                    if (txtv2.Enabled == true)
                                                    {
                                                        if (Session["V2Formula"] != null)
                                                        {
                                                            if (Session["V2Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV2 = intCountV2 + 1;
                                                                //if (txtNHWork.Enabled == true)
                                                                //{
                                                                //    if (Session["V2FormulaCalc"].ToString() == "")
                                                                //    {
                                                                //        if (!string.IsNullOrEmpty(txtNHWork.Text))
                                                                //        {
                                                                //            intCountV2 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / 8);

                                                                //        }
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (Session["V2FormulaCalc"].ToString() != "0")
                                                                //        {
                                                                //            intCountV2 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / Convert.ToInt32(Session["V2FormulaCalc"].ToString()));
                                                                //        }
                                                                //    }
                                                                //}

                                                                

                                                                if (intimeday != DateTime.MinValue && outtimeday != DateTime.MinValue)
                                                                {

                                                                    intCountV2 = intCountV22;
                                                                }

                                                            }
                                                            if (Session["V2Formula"].ToString() == "2") //TIME
                                                            {
                                                                ////DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["V2FormulaCalc"].ToString());
                                                                ////DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                ////if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                ////{
                                                                ////    intCountV2 = intCountV2 + 1;
                                                                ////}

                                                //km
                                                                DateTime intime = DateTime.Parse(strInTime, format);
                                                                DateTime outtime = DateTime.Parse(strOutTime, format);
                                                                DateTime intime_formula = DateTime.Parse(Session["V2InTime"].ToString(), format);
                                                                DateTime outtime_formula = DateTime.Parse(Session["V2OutTime"].ToString(), format);

                                                                //kl
                                                                if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 &&(TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                {
                                                                    int RERIN = TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay);
                                                                    int REROUT = TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay);
                                                                    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) <= 0 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) > 0)
                                                                    {
                                                                        intCountV2 = intCountV2 + 1;
                                                                    }
                                                                }
                                                                //else if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == 1 &&(TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == 1)
                                                                
                                                                //{
                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == 1)
                                                                //    {
                                                                //        intCountV2 = intCountV2 + 1;
                                                                //    }
                                                                //}

                                                            }
                                                        }
                                                    }



                                                    //If Variable Pay is Applicable...
                                                    if (txtv3.Enabled == true)
                                                    {
                                                        if (Session["V3Formula"] != null)
                                                        {
                                                            if (Session["V3Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV3 = intCountV3 + 1;
                                                                //if (txtNHWork.Enabled == true)
                                                                //{
                                                                //    if (Session["V3FormulaCalc"].ToString() == "")
                                                                //    {
                                                                //        if (!string.IsNullOrEmpty(txtNHWork.Text))
                                                                //        {
                                                                //            intCountV3 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / 8);

                                                                //        }
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (Session["V3FormulaCalc"].ToString() != "0")
                                                                //        {
                                                                //            intCountV3 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / Convert.ToInt32(Session["V3FormulaCalc"].ToString()));
                                                                //        }
                                                                //    }
                                                                //}

                                                                if (intimeday != DateTime.MinValue && outtimeday != DateTime.MinValue)
                                                                {

                                                                    intCountV3 = intCountV33;
                                                                }





                                                            }
                                                            if (Session["V3Formula"].ToString() == "2") //TIME
                                                            {
                                                                ////DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["V3FormulaCalc"].ToString());
                                                                ////DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                ////if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                ////{
                                                                ////    intCountV3 = intCountV3 + 1;
                                                                ////}

                                                                //km
                                                                DateTime intime = DateTime.Parse(strInTime, format);
                                                                DateTime outtime = DateTime.Parse(strOutTime, format);
                                                                DateTime intime_formula = DateTime.Parse(Session["V3InTime"].ToString(), format);
                                                                DateTime outtime_formula = DateTime.Parse(Session["V3OutTime"].ToString(), format);

                                                                if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                {
                                                                    int RERIN = TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay);
                                                                    int REROUT = TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay);
                                                                    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) <= 0 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) > 0)
                                                                    {
                                                                        intCountV3 = intCountV3 + 1;
                                                                    }
                                                                }





                                                                //if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                //{

                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == 1)
                                                                //    {
                                                                //        intCountV3 = intCountV3 + 1;
                                                                //    }
                                                                //}
                                                                //else if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == 1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == 1)
                                                                //{
                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == 1)
                                                                //    {
                                                                //        intCountV3 = intCountV3 + 1;
                                                                //    }
                                                                //}

                                                            }
                                                        }
                                                    }


                                                    //If Variable Pay is Applicable...
                                                    if (txtv4.Enabled == true)
                                                    {
                                                        if (Session["V4Formula"] != null)
                                                        {
                                                            if (Session["V4Formula"].ToString() == "1") //DAY
                                                            {
                                                                //intCountV4 = intCountV4 + 1;
                                                                //if (txtNHWork.Enabled == true)
                                                                //{
                                                                //    if (Session["V4FormulaCalc"].ToString() == "")
                                                                //    {
                                                                //        if (!string.IsNullOrEmpty(txtNHWork.Text))
                                                                //        {
                                                                //            intCountV4 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / 8);
                                                                //        }
                                                                //    }
                                                                //    else
                                                                //    {
                                                                //        if (Session["V4FormulaCalc"].ToString() != "0")
                                                                //        {
                                                                //            intCountV4 = Convert.ToInt32(Convert.ToDouble(txtNHWork.Text) / Convert.ToInt32(Session["V4FormulaCalc"].ToString()));
                                                                //        }
                                                                //    }
                                                                //}

                                                                if (intimeday != DateTime.MinValue && outtimeday != DateTime.MinValue)
                                                                {

                                                                    intCountV4 = intCountV44;
                                                                }



                                                            }
                                                            if (Session["V4Formula"].ToString() == "2") //TIME
                                                            {
                                                                //DateTime dtFormulaCalc = DateTime.Parse("2001-01-01 " + Session["V4FormulaCalc"].ToString());
                                                                //DateTime dtOut = DateTime.Parse(strOutTime, format);
                                                                //if (dtOut.TimeOfDay.Ticks > dtFormulaCalc.TimeOfDay.Ticks)
                                                                //{
                                                                //    intCountV4 = intCountV4 + 1;
                                                                //}


                                                                //km
                                                                DateTime intime = DateTime.Parse(strInTime, format);
                                                                DateTime outtime = DateTime.Parse(strOutTime, format);
                                                                DateTime intime_formula = DateTime.Parse(Session["V4InTime"].ToString(), format);
                                                                DateTime outtime_formula = DateTime.Parse(Session["V4OutTime"].ToString(), format);

                                                                if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                {
                                                                    int RERIN = TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay);
                                                                    int REROUT = TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay);
                                                                    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) <= 0 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) > 0)
                                                                    {
                                                                        intCountV4 = intCountV4 + 1;
                                                                    }
                                                                }




                                                                //if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == -1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == -1)
                                                                //{

                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == -1)
                                                                //    {
                                                                //        intCountV4 = intCountV4 + 1;
                                                                //    }
                                                                //}
                                                                //else if ((TimeSpan.Compare(intime.TimeOfDay, outtime.TimeOfDay)) == 1 && (TimeSpan.Compare(intime_formula.TimeOfDay, outtime_formula.TimeOfDay)) == 1)
                                                                //{
                                                                //    if (TimeSpan.Compare(intime.TimeOfDay, intime_formula.TimeOfDay) == -1 && TimeSpan.Compare(outtime.TimeOfDay, outtime_formula.TimeOfDay) == 1)
                                                                //    {
                                                                //        intCountV4 = intCountV4 + 1;
                                                                //    }
                                                                //}

                                                            }
                                                        }
                                                    }
                                                  
                                                    //Calculates only when intime and outime is presetn
                                                    intdaysworkedhrsmon = intdaysworkedminmon / 60;
                                                    intdaysworked = (intdaysworkedhrsmon / intdaysworkedhrmin);

                                                }
                                            }


                                            #region Deleted

                                            //if (intdaysworked > 0)
                                            //{
                                            //    txtdayswork.Text = intdaysworked.ToString("#0.00");
                                            //}
                                            #endregion
                                        }

                                        //If Variable Pay is Applicable...
                                        if (txtv1.Enabled == true)
                                        {
                                            txtv1.Text = Convert.ToString(intCountV1);
                                            //Check for Formula As Day or not 
                                        }
                                        if (txtv2.Enabled == true)
                                        {
                                            txtv2.Text = Convert.ToString(intCountV2);
                                        }
                                        if (txtv3.Enabled == true)
                                        {
                                            txtv3.Text = Convert.ToString(intCountV3);
                                        }
                                        if (txtv4.Enabled == true)
                                        {
                                            txtv4.Text = Convert.ToString(intCountV4);
                                        }
                                        //Check for Approved TimeSheet Data If data is there then 
                                        //Get Data From Approved TimeSheet and then show the data in the ApprovedTimeSheet Data 
                                        string srSqlTS = "Select Time_Card_No, dbo.ConvertToHours(Sum(NH)) NH, dbo.ConvertToHours(Sum(OT1)) OT1, dbo.ConvertToHours(Sum(OT2)) OT2, dbo.ConvertToHours(Sum(TotalHrsWrk)) TotalHrsWrk,sum(v1) v1,sum(v2) v2,sum(v3) v3,sum(v4) v4,dbo.ConvertToHours(sum(Lateness))Lateness  From (Select	A.Time_Card_No,dbo.ConvertToMinutes(A.NH) NH,dbo.ConvertToMinutes(A.OT1) OT1,dbo.ConvertToMinutes(A.OT2) OT2,dbo.ConvertToMinutes(A.TotalHrsWrk) TotalHrsWrk,A.v1,A.v2,A.v3,A.v4,dbo.ConvertToMinutes(A.Lateness)Lateness From ApprovedTimeSheet A inner join  TimeSheetMangment  T on A.RefID=T.RefId  Where (CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) >=CONVERT(datetime,'" + rdFrom.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103) And CONVERT(datetime,CONVERT(varchar,TimeEntryStart,103),103) <=CONVERT(datetime,'" + rdEnd.SelectedDate.Value.ToString("dd/MM/yyyy", format) + "',103))  And SoftDelete=0 and t.[Status]='Approved') D Group By Time_Card_No";
                                        DataSet dsTs_New = new DataSet();

                                        dsTs_New = DataAccess.FetchRS(CommandType.Text, srSqlTS, null);
                                        if (dsTs_New != null)
                                        {
                                            if (dsTs_New.Tables.Count > 0)
                                            {
                                                DataRow[] drTS;
                                                drTS = dsTs_New.Tables[0].Select("Time_Card_No ='" + time_card_no + "'");

                                                if (drTS.Length > 0)
                                                {

                                                    if (drTS[0]["v1"].ToString() != "0" || drTS[0]["v1"].ToString() != "")
                                                    {
                                                        if (txtv1.Enabled == true && txtv1.Text == "" || txtv1.Enabled == true && txtv1.Text == "0")
                                                        {
                                                             txtv1.Text = Convert.ToString(drTS[0]["v1"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }
                                                    if (drTS[0]["v2"].ToString() != "0" || drTS[0]["v2"].ToString() != "")
                                                    {
                                                        if (txtv2.Enabled == true && txtv2.Text == "" || txtv2.Enabled == true && txtv2.Text == "0")
                                                        {
                                                            txtv2.Text = Convert.ToString(drTS[0]["v2"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }

                                                    if (drTS[0]["v3"].ToString() != "0" || drTS[0]["v3"].ToString() != "")
                                                    {
                                                        if (txtv3.Enabled == true && txtv3.Text == "" || txtv3.Enabled == true && txtv3.Text == "0")
                                                        {
                                                            txtv3.Text = Convert.ToString(drTS[0]["v3"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }

                                                    if (drTS[0]["v4"].ToString() != "0" || drTS[0]["v4"].ToString() != "")
                                                    {
                                                        if (txtv4.Enabled == true && txtv4.Text == "" || txtv4.Enabled == true && txtv4.Text == "0")
                                                        {
                                                            txtv4.Text = Convert.ToString(drTS[0]["v4"].ToString());
                                                            //Check for Formula As Day or not 
                                                        }
                                                    }

                                                    if (drTS[0]["Lateness"].ToString() != "0" || drTS[0]["Lateness"].ToString() != "")
                                                    {

                                                        txtlateness.Text = Convert.ToString(drTS[0]["Lateness"].ToString());
                                                          
                                                       
                                                    }

                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                //((Button)commandItem.FindControl("btnsubmit")).Enabled = true;
                //((Button)commandItem.FindControl("btnCalcOverVar")).Enabled = true;
                //((Button)commandItem.FindControl("btnApplyCeiling")).Enabled = true;
            }


        }


       

        void MonthFill()
        {
            int i = 0;
            string ssql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[3];
            parms[i++] = new SqlParameter("@ROWID", "0");
            parms[i++] = new SqlParameter("@YEARS", 0);
            parms[i++] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            monthDs = DataAccess.ExecuteSPDataSet(ssql, parms);
            dtFilterFound = new DataTable();
            dtFilterFound = monthDs.Tables[0].Clone();

            if (Session["ROWID"] == null)
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            else
            {
                if (Session["ROWYEAR"] != null)
                {
                    foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                    {
                        dtFilterFound.ImportRow(dr);
                    }
                }
                else
                {
                    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
                    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
                    {
                        dtFilterFound.ImportRow(dr);
                    }

                }
            }

            //if (PayrollType == 0)
            //{
            //    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'");
            //    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "'"))
            //    {
            //        dtFilterFound.ImportRow(dr);
            //    }
            //}
            //else
            //{
            //    foundRows = monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "' And PAYTYPE='" + PayrollType.ToString() + "'");
            //    foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + cmbYear.SelectedValue + "' And PAYTYPE='" + PayrollType.ToString() + "'"))
            //    {
            //        dtFilterFound.ImportRow(dr);
            //    }
            //}

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
            cmbMonth.SelectedIndex = DateTime.Now.Month-1;
            SetControlDate(cmbMonth.SelectedValue);
        }




        void SetControlDate(string mon)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + mon);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
                strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MMM/yyyy", format);
                strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MMM/yyyy", format);

                rdFrom.DbSelectedDate = Utility.ToString(dr["PaySubStartDate"].ToString());
                rdEnd.DbSelectedDate = Utility.ToString(dr["PaySubEndDate"].ToString());
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            //if (Session["ROWID"] == null)
            //{
            //}
            //else
            //{
            //    if (intcnt == 1)
            //    {
            //        cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
            //        cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
            //    }
            //    else
            //    {
            //        if (IsPostBack == true)
            //        {
            //            MonthFill();
            //        }
            //        cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
            //        cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
            //        SetControlDate(cmbMonth.SelectedValue);
            //    }
            //}

        }

        #region Importing
        protected void chkId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                FileUpload.Visible = true;
                ImageButton1.Visible = true;
            }
            else
            {
                FileUpload.Visible = false;
                ImageButton1.Visible = false;
            }
        }

        bool output;
        protected void bindgrid1(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                output = ExcelImport();
            }
        }

        bool res;



        protected bool ExcelImport()
        {
            int TotalNoRecordes = 0;
            string strMsg = "";
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType = FileUpload.PostedFile.ContentType;
                int IntFileSize = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                {
                    strMsg = "Please Select File to be uploaded";
                    ShowMessageBox("Please Select File to be uploaded");
                    res = false;
                }

                else
                {
                    res = true;
                    int RandomNumber = 0;
                    RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

                    string strTranID = Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];
                    StrFileName = FileExt[0] + strTranID;
                    string stfilepath = Server.MapPath(@"..\\Documents\\UploadAddDed\" + StrFileName + strExtent);
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);

                        string filename = StrFileName + strExtent;

                      //  ImportExcelTosqlServer(filename);

                       
                        DataTable dt = GetDataFromExcel(filename);

                        int selectitem = 0;


                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {



                            if (item is GridItem)
                            {
                                GridDataItem dataItem = (GridDataItem)item;

                                CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                if (chkBox.Checked == true)
                                {
                                    selectitem = selectitem + 1;
                                }
                            }
                        }


                      if (selectitem == 0)
                      {
                          ShowMessageBox("Please Select Employee to Import");
                          return false;
                      }

                         
                        foreach (GridItem item in RadGrid1.MasterTableView.Items)
                        {

                           

                                 if (item is GridItem)
                                 {
                                     GridDataItem dataItem = (GridDataItem)item;
                                     
                                     CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                                     if (chkBox.Checked == true)
                                     {

                                     string empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                                     string empid = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("empid"));
                                     int id = Utility.ToInteger(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("id"));
                                     string time_card_no = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("time_card_no")).Trim().Replace(" ",string.Empty );
                                     TextBox txtot1 = (TextBox)dataItem.FindControl("txtovertime");
                                     TextBox txtot2 = (TextBox)dataItem.FindControl("txtovertime2");
                                     TextBox txtv1 = (TextBox)dataItem.FindControl("txtv1");
                                     TextBox txtv2 = (TextBox)dataItem.FindControl("txtv2");
                                     TextBox txtv3 = (TextBox)dataItem.FindControl("txtv3");
                                     TextBox txtv4 = (TextBox)dataItem.FindControl("txtv4");
                                     TextBox txtdayswork = (TextBox)dataItem.FindControl("txtDaysWork");
                                     TextBox txtNHWork = (TextBox)dataItem.FindControl("txtNHWork");
                                     TextBox txtlateness = (TextBox)dataItem.FindControl("txtlateness");
                                

                                     string expression = String.Format("TRIM(TIMECARD_ID) = '{0}'", time_card_no);

                                  

                                
                                     DataRow[] dr = dt.Select(expression);

                                     
                                     if ((dr.Length > 0))
                                     {

                                         string ROW = dr[0]["TIMECARD_ID"].ToString();
                                         //nh

                                         if (txtNHWork.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["NH"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time  NH" + dr[0]["NH"].ToString() + "<br/> ";
                                                 ;
                                             }
                                             else
                                             {
                                                 if (!string.IsNullOrEmpty(dr[0]["NH"].ToString()))
                                                 {

                                                     txtNHWork.Text = dr[0]["NH"].ToString().Replace(":", ".");
                                                 }
                                             }
                                         }
                                         

                                   
                                         //ot1

                                         if (txtot1.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["OT1"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time OT1 " + dr[0]["OT1"].ToString() + "<br/> ";
                                               
                                             }
                                             else
                                             {
                                                 if (!string.IsNullOrEmpty(dr[0]["OT1"].ToString()))
                                                 {

                                                     txtot1.Text = dr[0]["OT1"].ToString().Replace(":", ".");
                                                 }
                                             }
                                         }
                                         //ot2
                                         if (txtot2.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["OT2"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time OT2 " + dr[0]["OT2"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["OT2"].ToString()))
                                                 {
                                                 txtot2.Text = dr[0]["OT2"].ToString().Replace(":", ".");
                                                  }
                                             }
                                         }


                                         //v1

                                         if (txtv1.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["V1"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time V1 " + dr[0]["V1"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["V1"].ToString()))
                                                 {
                                                 txtv1.Text = dr[0]["V1"].ToString().Replace(":", "."); ;
                                                  }
                                             }
                                         }


                                         //v2
                                         if (txtv2.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["V2"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time  v2 " + dr[0]["V2"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["V2"].ToString()))
                                                 {
                                                 txtv2.Text = dr[0]["V2"].ToString().Replace(":", ".");
                                                  }
                                             }
                                         }

                                         //v3
                                         if (txtv3.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["V3"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time v3  " + dr[0]["V3"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["V3"].ToString()))
                                                 {
                                                 txtv3.Text = dr[0]["V3"].ToString().Replace(":", ".");
                                                  }
                                             }
                                         }


                                         if (txtv4.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["V4"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time V4" + dr[0]["V4"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["V4"].ToString()))
                                                 {

                                                 txtv4.Text = dr[0]["V4"].ToString().Replace(":", ".");
                                                  }
                                             }
                                         }


                                         if (txtdayswork.Enabled)
                                         {
                                              if (!string.IsNullOrEmpty(dr[0]["DAYSWORK"].ToString()))
                                                 {
                                             txtdayswork.Text = dr[0]["DAYSWORK"].ToString();
                                              }
                                         }



                                         if (txtlateness.Enabled)
                                         {
                                             if (!IsValidTime(dr[0]["LATENESS"].ToString()))
                                             {
                                                 strMsg += ROW + " has invalid Time LATNESS " + dr[0]["LATENESS"].ToString() + "<br/> ";


                                             }
                                             else
                                             {
                                                  if (!string.IsNullOrEmpty(dr[0]["LATENESS"].ToString()))
                                                 {
                                                 
                                                 txtlateness.Text = dr[0]["LATENESS"].ToString().Replace(":", ".");
                                                  }
                                             }
                                         }

                                     
                                  
                                   
                                   
                                    
                                    
                                     
                                    

                                   
                                         TotalNoRecordes = TotalNoRecordes + 1;

                                       
                                     }



                                 }
                             }
                        }


                      //  strMsg += TotalNoRecordes.ToString() + " Recordes Imported out of " + dt.Rows.Count + "Records <br/> Click Submit to Save Records";

                        strMsg += "<br/> Click Submit to Save Records";


                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                    }
                }

            }
            ShowMessageBox(strMsg);

            //lblerror.Text = strMsg;

            return res;
        }

        protected void deptID_databound(object sender, EventArgs e)
        {
            //deptID.Items.Insert(0, new ListItem("ALL", "-1"));
            deptID.SelectedValue = "-1";
        }


        string col, Empcode, ICNUMBER, Empcode1;
        decimal A1, Addition;
        int Month, year, categoryid;
        string EmpList;
        public void ImportExcelTosqlServer(string filename)
        {
            
            #region Old Code
            DataSet ds = new DataSet();
            tbRecord.Visible = true;//toolbar

            intcnt = 1;
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            rdFrom.Enabled = false;
            rdEnd.Enabled = false;
            deptID.Enabled = false;
            //IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            int k = 0;

            // DataSet ds = new DataSet();

            string ssql = "sp_emp_overtime";// 0,2009,2
            SqlParameter[] parms = new SqlParameter[5];
            parms[k++] = new SqlParameter("@month", cmbMonth.SelectedValue);
            parms[k++] = new SqlParameter("@year", cmbYear.SelectedValue);
            parms[k++] = new SqlParameter("@company_id", comp_id);
            parms[k++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));
            parms[k++] = new SqlParameter("@DeptId", Utility.ToInteger(deptID.SelectedValue));
            ds = DataAccess.ExecuteSPDataSet(ssql, parms);

            #endregion

            DataTable dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append("");
            try
            {

                        foreach (DataRow dr in dt.Rows)
                        {

                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                if (i >= 1)//skip the first 2 column
                                {


                            col = dt.Columns[i].ToString();

                            //check whether IC number is present
                            //if yes--> get empno from that
                            //else --> check the empno directly
                            ICNUMBER = dr["TIME_CARD_NO"].ToString();
                            if (ICNUMBER != "")
                            {

                                Empcode = null;
                                string sql = " select emp_code from employee where TIME_CARD_NO='" + ICNUMBER + "'";
                                SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr_empcode.Read())
                                {
                                    Empcode = dr_empcode["emp_code"].ToString();
                                }
                            }

                            //

                            if (Empcode != null)
                            {
                            

                                #region TODO 
                                DataRow[] dr1 = ds.Tables[0].Select("Empid=" + Empcode);
                                if (dr1[0].Table.Rows.Count > 0)//check if row exist
                                {
                                    //NH
                                    if (dr["NH"].ToString() != "")
                                    {
                                        dr1[0]["days_work"] = Convert.ToInt32(dr["NH"].ToString());
                                        //dr1[0]["days_work"] = 3;
                                    }
                                    //TO Do

                                    //Add other columns and update

                                    ds.Tables[0].AcceptChanges();
                                }
                                #endregion

                            }


                        }

                    }

                        if (Empcode != "")//if mapped then only insert
                        {
                            EmpList = EmpList + Empcode + ",";

                           
                        }
                   }


                DataTable table = new DataTable();
                DataView DataView1 = new DataView();
                DataView1 = ds.Tables[0].DefaultView;
                DataView1.RowFilter = "Empid in  (" + EmpList.Substring(0, EmpList.Length - 1)  + ")";
           

                //this.RadGrid1.DataSource = ds;
                this.RadGrid1.DataSource = DataView1;
                RadGrid1.DataBind();


           
            }
            catch (Exception e)
            {
                //DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                ShowMessageBox("Error for the Employee:" + Empcode + " Msg-" + e.Message.ToString());
                //lblerror.Text = "Error in Data" + e.Message.ToString();
            }
        }


        int Empid;
        string FULLNAME;
        object retval;
        private object GetName(object TimeCard, string val)
        {
            string sqlqry = "select Emp_Code,(E.Emp_Name + ' ' + E.Emp_LName) FULLNAME from Employee E where TIME_CARD_NO='" + TimeCard + "' AND company_ID='" + comp_id + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlqry, null);
            while (dr.Read())
            {
                Empid = Utility.ToInteger(dr.GetValue(0));
                FULLNAME = Utility.ToString(dr.GetValue(1));
            }
            if (dr.HasRows)
            {
                if (val == "FULLNAME")
                {
                    retval = FULLNAME;
                }
                else if (val == "Emp_Code")
                {
                    retval = Empid;
                }
            }
            else
            {
                //throw new Exception("The method or operation is not implemented.");
                return 0;
            }
            return retval;
        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public System.Data.DataTable GetDataFromExcel(string filename)
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("TIMECARD_ID", typeof(string)));
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/UploadAddDed/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                //string SheetName = "Sheet1";//here enter sheet name       
                string SheetName = "Table1";//here enter sheet name    
                oledbconn.Open();
                OleDbCommand cmdSelect = new OleDbCommand(@"SELECT * FROM [" + SheetName + "$]", oledbconn);
                OleDbDataAdapter oledbda = new OleDbDataAdapter();
                oledbda.SelectCommand = cmdSelect;
                oledbda.Fill(dt);
                oledbconn.Close();
                oledbda = null;

              
            }
            

            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return dt;
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

        #region Generate Template
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }

        protected void btnTemplate_Click(object sender, EventArgs e)
        {
            DataSet dss = new DataSet();
            string sql = "select Apcategory  from dbo.APCategory where companyid='" + comp_id + "'";
            dss = getDataSet(sql);

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("FULLNAME"));
            dt.Columns.Add(new DataColumn("TIME_CARD_NO"));
            dt.Columns.Add(new DataColumn("MONTH"));
            dt.Columns.Add(new DataColumn("Year"));


            //change each row to column
            foreach (DataRow row in dss.Tables[0].Rows)
            {
                foreach (DataColumn column in dss.Tables[0].Columns)
                {
                    if (row[column] != null) // this will checks the null values also if you want to check
                    {
                        string columnname = Convert.ToString(row[column]);
                        dt.Columns.Add(new DataColumn(columnname));
                    }
                }
            }

            DataSet dd = new DataSet();
            dd.Tables.Add(dt);

            //string fastExportFilePath = "C:\\CosingByProjecttemplate.xls";
            string fastExportFilePath = Server.MapPath(@"..\\Documents\\UploadAddDed\CosingByProjecttemplate.xls");
            FastExportingMethod.ExportToExcel(dd, fastExportFilePath);

        }

        #endregion

        protected void btnClear_Click(object sender, EventArgs e)
        {
            string sql_delete = "delete from dbo.AdditionPay where MONTH(startdate)='" + cmbMonth.SelectedValue + "'  and YEAR(startdate)='" + cmbYear.SelectedValue + "'  and company_id='" + comp_id + "'";
            DataAccess.FetchRS(CommandType.Text, sql_delete, null);
           // bindgriddata();
        }


        #endregion


        //Toolbar
        #region Toolbar and Exporting

        //column to hide wile export
        protected void HideGridColumnseExport()
        {
            RadGrid1.MasterTableView.GetColumn("TemplateColumn").Visible = false;
            RadGrid1.MasterTableView.GetColumn("GridClientSelectColumn").Visible = false;// UniqueName="DeleteColumn"
        }

        protected void tbRecord_ButtonClick(object sender, Telerik.Web.UI.RadToolBarEventArgs e)
        {
            if (e.Item.Text == "Excel" || e.Item.Text == "Word")
            {
                HideGridColumnseExport();
            }

            GridSettingsPersister obj2 = new GridSettingsPersister();
            obj2.ToolbarButtonClick(e, RadGrid1, Utility.ToString(Session["Username"]));

        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("102", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);

        }


        #endregion
        //Toolbar End


    }
}
