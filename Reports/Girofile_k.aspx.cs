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
using System.Net.Mail;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Services;
using Telerik.Web.UI.Calendar;

namespace SMEPayroll.Reports
{
    public partial class Girofile_k : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
      
        string _actionMessage = "";
        string sSQL = "";
        int intcnt;
        int compid = 0;
        DataSet monthDs;
        DataRow[] foundRows;
        DataTable dtFilterFound;
        DateTime vd;
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString() != "-1"?cmbYear.SelectedValue.ToString():DateTime.Now.Year.ToString();
            bindMonth();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
        }
        private object _dataItem = null;
        int c1;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //----murugan
            //if (ViewState["rddate"] != null)
            //{
            //    rddate.SelectedDate = Convert.ToDateTime(ViewState["rddate"]);
            //    //rddate.FocusedDate = Convert.ToDateTime(ViewState["rddate"]);
            //   // rddate.DbSelectedDate = Convert.ToDateTime(ViewState["rddate"]);
                
            //}
            //else
            //{
            //    rddate.SelectedDate = DateTime.Today;
            //}
            ViewState["actionMessage"] = "";

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            compid = Utility.ToInteger( Session["Compid"]);
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
            /* To disable Grid filtering options  */
            GridFilterMenu menu = RadGrid1.FilterMenu;
            int i = 0;
       
            while (i < menu.Items.Count)
            {
                menu.Items.RemoveAt(i);
            }
         
            if (!IsPostBack)
            {
                LoadGridSettingsPersister();  //--murugan

                #region Yeardropdown
                cmbYear.DataBind();
                #endregion 
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                bindMonth();

                DataSet ds_bank = new DataSet();
                sSQL = "SELECT [id], [desc],code FROM bank where Code Is Not Null and id in(Select distinct bank_id From girobanks where company_id="+ compid +" )";
                ds_bank = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpbank.DataSource = ds_bank.Tables[0];
                drpbank.DataTextField = ds_bank.Tables[0].Columns["desc"].ColumnName.ToString();
                drpbank.DataValueField = ds_bank.Tables[0].Columns["id"].ColumnName.ToString();
                
                drpbank.DataBind();
                drpbank.Items.Insert(0, new ListItem("-select-", "-1"));
                drpbank.SelectedIndex = -1;
                //drpbank.Items.Insert(ds_bank.Tables[0].Rows.Count+1, new ListItem("ChkPro", "-5"));
                drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));

                //lblHash.Visible = false;
                ////chkHash.Visible = false;
                Label_G3.Visible = false;
                g3format.Visible = false;

                rddate.SelectedDate = DateTime.Today;
            }
        }

        private void bindMonth()
        {
            MonthFill();
        }
        private bool ISnewformate = false;
        private bool ISg3formate = false;
        protected void btngenerate_Click(object sender, EventArgs e)
        {
           ISnewformate = this.newformate.Checked;
           ISg3formate = this.g3format.Checked;

            string batch_no = string.IsNullOrEmpty(this.batch_no.Text)? "01" : this.batch_no.Text.Trim();

            
            Session["ValueDate"] = Convert.ToDateTime(rddate.SelectedDate.Value.ToString());

            string emp_list = "";
            string sFileName = "";

            foreach (GridItem item in RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        int empid = Utility.ToInteger(this.RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_id"));
                        emp_list = emp_list + empid + ",";
                    }
                }
            }

            //if (emp_list == "")
            //{
            //    Response.Write("<SCRIPT language='Javascript'>alert('Please select at least one employee.');</SCRIPT>");
            //    return;
            //}

            //string[] selectedvalue = drpbank.SelectedValue.Split('_');


            //string select = selectedvalue[0];

            sFileName = "";
            SMEGiro sme = new SMEGiro();
            sme.ISnewformate = ISnewformate;
            sme.ISg3format = ISg3formate;

            if (drpbank.SelectedValue == Utility.ToString(Session["OCBC"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }


                //sme.BatchNo = batch_no.Trim().Length < 5 ? "00001" : batch_no.Trim();
                sme.BatchNo = batch_no;
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\OCBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_OCBC();
                sFileName = "../Documents/Girofiles/OCBC/" + sme.LogFileName;
            }
            // murugan
            //if (drpbank.SelectedValue == Utility.ToString(Session["CIMB"]))
            //{
            //    string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

            //    string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
            //    if (File.Exists(strPath))
            //    {
            //        File.Delete(strPath);
            //    }



            //    sme.CompanyID = (short)compid;
            //    sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
            //    sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
            //    sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
            //    sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
            //    sme.SenderAccountNo = drpaccno.Text;
            //    sme.sEmployeeList = emp_list;
            //    sme.LogFilePath = @"..\\Documents\\GiroFiles\\CIMB\\";
            //    sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
            //    sme.GenerateGiroFile_CIMB();
            //    sFileName = "../Documents/Girofiles/CIMB/" + sme.LogFileName;
            //}
            //-------------------
            if (drpbank.SelectedValue == "34")//
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }



                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\SEB\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_SEB();
                sFileName = "../Documents/Girofiles/SEB/" + sme.LogFileName;
            }



            if (drpbank.SelectedValue == "45")
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }



                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\ANZ\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_ANZ();
                sFileName = "../Documents/Girofiles/ANZ/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["UOB"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.isHash = ishash(drpbank.SelectedValue.ToString());
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\UOB\\";
                //KM
                if (!ISnewformate)
                {
                    sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + batch_no.Trim().Substring(0, 2) + ".txt";
                }
                else
                {
                    sme.LogFileName = "UGBI" + DateTime.Today.ToString("ddMM") + batch_no.Trim().Substring(0, 2) + ".txt"; 

                }
                
                sme.GenerateGiroFile_UOB();
                sFileName = "../Documents/Girofiles/UOB/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["HABIB"]))
            {
                //SMEGiro sme = new SMEGiro();
                sme.isHash = chkHash.Checked;
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\HABIB\\";
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + "01.csv";
                sme.GenerateGiroFile_HABIB();
                sFileName = "../Documents/Girofiles/HABIB/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["FAREAST"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\FAREAST\\";
                //KM
                sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") +batch_no.Trim().Substring(0,2); 
                sme.GenerateGiroFile_UOB();
                sFileName = "../Documents/Girofiles/FAREAST/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["NOR"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\NORDEA\\";

                if (!ISnewformate)
                {
                    sme.LogFileName = "UITI" + DateTime.Today.ToString("ddMM") + batch_no.Trim().Substring(0, 2) + ".txt"; 
                }
                else
                {
                    sme.LogFileName = "UGBI" + DateTime.Today.ToString("ddMM") + batch_no.Trim().Substring(0, 2) + ".txt"; 
                }
                
                sme.GenerateGiroFile_NOR();
                sFileName = "../Documents/Girofiles/NORDEA/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["DBS"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }
                //sme.BatchNo = batch_no.Trim().Length< 5? "00001":batch_no.Trim();
                sme.BatchNo = batch_no;
                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\DBS\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_DBS();
                //sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                //sme.GenerateGiroFile_JPMorgan();
                sFileName = "../Documents/Girofiles/DBS/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["DB"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\Deutsche\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_Deutsche();
                sFileName = "../Documents/Girofiles/Deutsche/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["SC"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\SC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".csv";
                sme.GenerateGiroFile_SC();
                sFileName = "../Documents/Girofiles/SC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["MIZ"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\Mizuho\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".csv";
                sme.GenerateGiroFile_Mizuho();
                sFileName = "../Documents/Girofiles/Mizuho/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["SMBC"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\SMBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".csv";
                sme.GenerateGiroFile_SMBC();
                sFileName = "../Documents/Girofiles/SMBC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["CITI"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\CITI\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_CITI();
                sFileName = "../Documents/Girofiles/CITI/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["ABN"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\ABN\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_ABN();
                sFileName = "../Documents/Girofiles/ABN/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["HSBC"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\HSBC\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_HSBC();
                sFileName = "../Documents/Girofiles/HSBC/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == "54")
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\HSBC_SG\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_HSBC_SG();
                sFileName = "../Documents/Girofiles/HSBC_SG/" + sme.LogFileName;
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["MAY"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\MAY\\";
               // sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.LogFileName = "MAY" + DateTime.Today.ToString("MMdd") + DateTime.Now.ToString("HHmm") + ".txt";
                sme.GenerateGiroFile_MAY();
                sFileName = "../Documents/Girofiles/MAY/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["BTMU"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BTMU\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BTMU();
                sFileName = "../Documents/Girofiles/BTMU/" + sme.LogFileName;
            }
            //new 
            else if (drpbank.SelectedValue == Utility.ToString(Session["DBS_DISK"]))
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\DBS_Disk\\";
                //sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.LogFileName = "DPAY2.txt";
                string res= sme.GenerateGiroFile_DBS_Diskete();
               
                sFileName = "../Documents/Girofiles/DBS_Disk/" + sme.LogFileName;
                if (res == "File Succesfully created!")
                {
                    //showing information file
                    Response.Write("<SCRIPT language='Javascript'>window.open('../Documents/Girofiles/DBS_Disk/Information.txt');</SCRIPT>");
                }
                else//showing error message
                {
                   // Response.Write("<SCRIPT language='Javascript'>alert('" + res + "');</SCRIPT>");
                   // ShowMessageBox(res);
                    _actionMessage = "Warning|"+res;
                    ViewState["actionMessage"] = _actionMessage;
                    return;
                }
            }
            else if (drpbank.SelectedValue == Utility.ToString(Session["BNP_PARIBAS"]))//BNP PARIBAS
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BNP\\";
                sme.BatchNo = string.IsNullOrEmpty(this.batch_no.Text)? "01" : this.batch_no.Text.Trim();
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BNP();
                sFileName = "../Documents/Girofiles/BNP/" + sme.LogFileName;
            }

            else if (drpbank.SelectedValue == Utility.ToString(Session["BANK_OF_AMERICA"]))//BANK OF AMERICA
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\BNP\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.GenerateGiroFile_BOA();
                sFileName = "../Documents/Girofiles/BNP/" + sme.LogFileName;
            }


            else if (drpbank.SelectedValue == Utility.ToString(Session["CIMB"]))//BANK OF CIMB
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\CIMB\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".csv";
                sme.GenerateGiroFile_CIMB();
                sFileName = "../Documents/Girofiles/CIMB/" + sme.LogFileName;
            }



            else if (drpbank.SelectedValue == Utility.ToString(Session["FAR_EASTERN_BANK"]))//FAR EASTERN BANK
            {
                string patch_giro = @"..\\Documents\\GiroFiles\\" + sFileName;

                string strPath = System.Web.HttpContext.Current.Server.MapPath(patch_giro);
                if (File.Exists(strPath))
                {
                    File.Delete(strPath);
                }

                sme.CompanyID = (short)compid;
                sme.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                sme.SenderAccountNo = drpaccno.Text;
                sme.sEmployeeList = emp_list;
                sme.LogFilePath = @"..\\Documents\\GiroFiles\\JPMorgan\\";
                sme.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".txt";
                sme.dtJPMorgan = Convert.ToDateTime(rddate.SelectedDate);
                sme.GenerateGiroFile_JPMorgan();
                sFileName = "../Documents/Girofiles/JPMorgan/" + sme.LogFileName;
            }
           

            
            #region calling the log file
            //try
            //{
                //delete the old file
                //string patch_log = @"..\\Documents\\GiroFiles\\Log.txt";

                //string str_Path = System.Web.HttpContext.Current.Server.MapPath(patch_log);
                //if (File.Exists(str_Path))
                //{
                //    File.Delete(str_Path);
                //}


                SMEGiro sme1 = new SMEGiro();
                sme1.CompanyID = (short)compid;
                sme1.SalMonth = (short)Utility.ToInteger(cmbMonth.SelectedValue);
                sme1.SalYear = (short)Utility.ToInteger(cmbYear.SelectedValue);
                sme1.SenderBank = (short)Utility.ToInteger(drpbank.SelectedValue);
                sme1.ValueDate = (short)Utility.ToInteger(drpValueDate.SelectedValue);
                sme1.SenderAccountNo = drpaccno.Text;
                sme1.sEmployeeList = emp_list;
                sme1.LogFilePath = @"..\\Documents\\GiroFiles\\";
                sme1.LogFileName = sme.CompanyID + DateTime.Today.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + "Log.txt";
                sme1.GenerateLog();
              
            //}
            //catch (Exception err)
            //{

                
            //}
            #endregion
            


            if (sFileName == "")
            {
                //Response.Write("<SCRIPT language='Javascript'>alert('The feature is not supported for the selected bank.');</SCRIPT>");
                _actionMessage = "Warning|The feature is not supported for the selected bank.";
                ViewState["actionMessage"] = _actionMessage;
                return;
            }
            else
            {                
                Response.Write("<SCRIPT language='Javascript'>window.open('" + sFileName + "');</SCRIPT>");
            }
            Response.Write("<SCRIPT language='Javascript'>window.open('"+ sme1.LogFilePath+sme1.LogFileName+"');</SCRIPT>");
        }
        protected void bindgrid(object sender, EventArgs e)
        {
            string cmbMonthstr = cmbMonth.SelectedValue.ToString();
            string strcmbYear = cmbYear.SelectedValue.ToString();
            string strdrpbank = drpbank.SelectedValue.ToString();
            string strdrpaccno = drpaccno.SelectedValue.ToString();
            string strdrpValueDate = drpValueDate.SelectedValue.ToString();

            RadGrid1.DataBind();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();

            if (drpbank.SelectedValue == "9")
            {
                Label_G3.Visible = true;
                g3format.Visible = true;

            }
             else
            {
                Label_G3.Visible = false;
                g3format.Visible = false;
            }
            if (drpbank.SelectedValue == "4")
            {
                Label1.Text = "DBS IDEAL Format";
                           }
            else
            {
                Label1.Text = "New Format";
            }
            cmbYear.Enabled = false;
            cmbMonth.Enabled = false;
            drpbank.Enabled = false;
            drpaccno.Enabled = false;
        }
       
        public object DataItem
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


        protected void drpbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpbank.SelectedValue == "9")
            //{
            //    Label_G3.Visible = true;
            //    g3format.Visible = true;

            //}
            //else
            //{
            //    Label_G3.Visible = false;
            //    g3format.Visible = false;
            //}
            if (drpbank.SelectedValue != "-1")
            {

                DataSet ds_bankacc = new DataSet();
                sSQL = "SELECT bank_accountno,value_date FROM girobanks where company_id=" + compid + " and bank_id=" + drpbank.SelectedValue;
                ds_bankacc = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpaccno.DataSource = ds_bankacc.Tables[0];
                drpaccno.DataTextField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();
                drpaccno.DataValueField = ds_bankacc.Tables[0].Columns["bank_accountno"].ColumnName.ToString();
                drpaccno.DataBind();
                string d1 = ds_bankacc.Tables[0].Rows[0][1].ToString();
                drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));
                DateTime d;
                if (Convert.ToInt32(d1) < 8)
                {
                    if (DateTime.Today.Month == 12)
                    {
                        d = new DateTime(DateTime.Today.Year + 1, 1, Convert.ToInt32(d1));
                    }
                    else
                    {
                        d = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, Convert.ToInt32(d1));
                    }
                }
                else
                {
                    d = new DateTime(DateTime.Today.Year, DateTime.Today.Month, Convert.ToInt32(d1));
                }

                ViewState["rddate"] = d.ToString("dd/MM/yyyy");
            }
            else
            {
                drpaccno.Items.Clear();
                drpaccno.Items.Insert(0, new ListItem("-select-", "-select-"));
            }
        }

        //[WebMethod]
        public static bool ishash(string value)
        {
            bool result = false;
            if (value != "-1")
            {

                DataSet ds_bank = new DataSet();
                string sSQL = "SELECT [id],ishash FROM bank where id=" + value;
                ds_bank = DataAccess.FetchRS(CommandType.Text, sSQL, null);

                if (ds_bank != null)
                {

                    result = ds_bank.Tables[0].Rows[0]["ishash"] == DBNull.Value ? false : (bool)ds_bank.Tables[0].Rows[0]["ishash"];
                }
            }
            return result;
        }



        void CallBeforeMonthFill()
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
        }

        void MonthFill()
        {
            CallBeforeMonthFill();
            string selectedYearvalue = cmbYear.SelectedValue != "-1" ? cmbYear.SelectedValue : DateTime.Now.Year.ToString();
            if (Session["ROWID"] == null)
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + selectedYearvalue + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + selectedYearvalue + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }
            else
            {
                foundRows = monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'");
                foreach (DataRow dr in monthDs.Tables[0].Select("YEAR = '" + Session["ROWYEAR"].ToString() + "'"))
                {
                    dtFilterFound.ImportRow(dr);
                }
            }

            cmbMonth.DataSource = dtFilterFound;
            cmbMonth.DataTextField = "MonthName";
            cmbMonth.DataValueField = "RowID";
            cmbMonth.DataBind();
           // cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            SetControlDate();
        }


        protected override void OnPreRender(EventArgs e)
        {
           
            base.OnPreRender(e);
            if (Session["ROWID"] == null)
            {
            }
            else
            {
                if (intcnt == 1)
                {
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                    CallBeforeMonthFill();
                }
                else
                {
                    if (IsPostBack == true)
                    {
                        MonthFill();
                    }
                    cmbMonth.SelectedValue = Utility.ToString(Session["ROWID"]);
                    cmbYear.SelectedValue = Utility.ToString(Session["ROWYEAR"]);
                }
                SetControlDate();
            }
         
        }

        void SetControlDate()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + cmbMonth.SelectedValue);
            foreach (DataRow dr in drResults)
            {
                Session["PayStartDay"] = dr["PayStartDay"].ToString();
                Session["PayEndDay"] = dr["PayEndDay"].ToString();
                Session["PaySubStartDay"] = dr["PaySubStartDay"].ToString();
                Session["PaySubEndDay"] = dr["PaySubEndDay"].ToString();
                Session["PaySubStartDate"] = dr["PaySubStartDate"].ToString();
                Session["PaySubEndDate"] = dr["PaySubEndDate"].ToString();
            }
        }
        protected void g3format_CheckedChanged(object sender, EventArgs e)
        {
            if (g3format.Checked == true)
            {
                                newformate.Checked = false;
            }
        }
        protected void newformate_CheckedChanged(object sender, EventArgs e)
        {
            if (newformate.Checked ==true )
            {
                g3format.Checked = false;
                
            }
        }

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString() != "-1" ? cmbYear.SelectedValue.ToString() : DateTime.Now.Year.ToString();
        }

        //protected void drpaccno_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ViewState["rddate"] != null)
        //    {
        //        rddate.SelectedDate = Convert.ToDateTime(ViewState["rddate"]);

        //    }


        //}
    }
}
