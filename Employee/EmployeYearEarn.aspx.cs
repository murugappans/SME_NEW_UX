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
using System.Data.OleDb;
using System.Text;
namespace SMEPayroll.employee
{
    public partial class EmployeYearEarn : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        protected string strstdatemdy ="";
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
        public string connection;

        string grossvalue;     
        

        protected void Radgrid1_databound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
           
        }         
        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            GridCommandItem item = e.Item as GridCommandItem;
            if (item != null)
            {
                Button btn = item.FindControl("btnsubmit") as Button;
                btn.Attributes.Add("onclick", "javascript:return validateform();");
                lblMessage.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            /* To disable Grid filtering options  */
            ViewState["actionMessage"] = "";
            SqlDataSource1.ConnectionString = Session["ConString"].ToString();

            lblerror.Text = "";
            comp_id = Utility.ToInteger(Session["Compid"]);
            
            if (!IsPostBack)
            {
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);

                //Method to Load grid Seting Persister
                LoadGridSettingsPersister();
                RadGrid1.ExportSettings.FileName = "Year_of_Assessment_" + cmbYear.SelectedValue.ToString();
            }

            //Response.Write("<script l");

            connection = Session["ConString"].ToString();

            RadGrid1.NeedDataSource += new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
        }

       public void RadGrid1_NeedDataSource(object source, GridNeedDataSourceEventArgs e)
        {
            bindTable();
        }

        bool output;
        DataSet dsFill = new DataSet();
        protected void bindgrid(object sender, EventArgs e)
        {
            //Insert default value for each employee to reflect here
            #region Insert default value for each employee to reflect here
            string ssqlb = "sp_InsertBlankRecord_EmployeeEarning";
            SqlParameter[] parms5 = new SqlParameter[2];
            parms5[0] = new SqlParameter("@year", Convert.ToInt32(cmbYear.SelectedValue.ToString()));
            parms5[1] = new SqlParameter("@companyId", Convert.ToInt32(comp_id));
            DataAccess.FetchRS(CommandType.StoredProcedure, ssqlb, parms5);
            #endregion
            //


              if (chkId.Checked)
            {
                output = ExcelImport();
            }


            if (output || chkId.Checked == false)
            {

                #region Bind Grid
                bindTable();
                #endregion


                intcnt = 1;
                //cmbYear.Enabled = false;
                RadGrid1.DataBind();
            }
        }

        private void bindTable()
        {
            //if (chkId.Checked)
            //{
            //    sSQL = "sp_emp_yearearn_Temp";
            //}
            //else
            //{
                sSQL = "sp_emp_yearearn";
           // }
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));
            parms[2] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"])); //Senthil for Group Management
            dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid1.DataSource = dsFill;
           
        }

        private void bindTable1()
        {
            sSQL = "sp_emp_yearearn";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@year", Utility.ToInteger(cmbYear.SelectedValue));
            parms[1] = new SqlParameter("@company_id", Utility.ToInteger(comp_id));
            parms[2] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"])); //Senthil for Group Management
            dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid1.DataSource = dsFill;

           chkId.Checked = false;

        }
        protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
        }
        

        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;                
                ((Button)commandItem.FindControl("btnsubmit")).Enabled = false;
                string stryear = cmbYear.SelectedItem.Value;
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
                                    string id = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("ID"));
                                    string Emp_ID = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("Emp_ID"));

                                    TextBox txtGrossPay = (TextBox)dataItem.FindControl("txtGrossPay");
                                    TextBox txtBonus = (TextBox)dataItem.FindControl("txtBonus");
                                    TextBox txtDirectorFee = (TextBox)dataItem.FindControl("txtDirectorFee");
                                    TextBox txtCommission = (TextBox)dataItem.FindControl("txtCommission");
                                    TextBox txtPension = (TextBox)dataItem.FindControl("txtPension");
                                    TextBox txtTransAllow = (TextBox)dataItem.FindControl("txtTransAllow");
                                    TextBox txtEntAllow = (TextBox)dataItem.FindControl("txtEntAllow");
                                    TextBox txtOtherAllow = (TextBox)dataItem.FindControl("txtOtherAllow");
                                    TextBox txtEmployeeCPF = (TextBox)dataItem.FindControl("txtEmployeeCPF");
                                    TextBox txtFunds = (TextBox)dataItem.FindControl("txtFunds");
                                    TextBox txtMBMF = (TextBox)dataItem.FindControl("txtMBMF");


                                    double dbltxtGrossPay = Utility.ToDouble(txtGrossPay.Text);
                                    double dbltxtBonus = Utility.ToDouble(txtBonus.Text);
                                    double dbltxtDirectorFee = Utility.ToDouble(txtDirectorFee.Text);
                                    double dbltxtCommission = Utility.ToDouble(txtCommission.Text);
                                    double dbltxtPension = Utility.ToDouble(txtPension.Text);
                                    double dbltxtTransAllow = Utility.ToDouble(txtTransAllow.Text);
                                    double dbltxtEntAllow = Utility.ToDouble(txtEntAllow.Text);
                                    double dbltxtOtherAllow = Utility.ToDouble(txtOtherAllow.Text);
                                    double dbltxtEmployeeCPF = Utility.ToDouble(txtEmployeeCPF.Text);
                                    double dbltxtFunds = Utility.ToDouble(txtFunds.Text);
                                    double dbltxtMBMF = Utility.ToDouble(txtMBMF.Text);

                                    sSQL = "";

                                    if (chkId.Checked == false)//if importing
                                    {
                                        if ((id == "") && ((dbltxtGrossPay >= 0) || (dbltxtBonus >= 0) || (dbltxtDirectorFee >= 0) || (dbltxtCommission >= 0) || (dbltxtPension >= 0) || (dbltxtTransAllow >= 0) || (dbltxtEntAllow >= 0) || (dbltxtOtherAllow >= 0) || (dbltxtEmployeeCPF >= 0) || (dbltxtFunds >= 0) || (dbltxtMBMF >= 0)))
                                        {
                                            sSQL = "Insert into EmployeeEarning (Emp_ID,IRYear,GrossPay,Bonus,DirectorFee,Commission,Pension,TransAllow,EntAllow,OtherAllow,EmployeeCPF,Funds,MBMF) values(" + Emp_ID + ",'" + stryear + "'," + dbltxtGrossPay + "," + dbltxtBonus + "," + dbltxtDirectorFee + "," + dbltxtCommission + "," + dbltxtPension + "," + dbltxtTransAllow + "," + dbltxtEntAllow + "," + dbltxtOtherAllow + "," + dbltxtEmployeeCPF + "," + dbltxtFunds + "," + dbltxtMBMF + ")";
                                        }
                                        else if ((id != ""))
                                        {
                                            sSQL = "Update EmployeeEarning Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + " Where ID =" + id;
                                        }
                                    }
                                    else
                                    {
                                        sSQL = "Update EmployeeEarning Set GrossPay=" + dbltxtGrossPay + ",Bonus=" + dbltxtBonus + ",DirectorFee=" + dbltxtDirectorFee + ",Commission=" + dbltxtCommission + ",Pension=" + dbltxtPension + ",TransAllow=" + dbltxtTransAllow + ",EntAllow=" + dbltxtEntAllow + ",OtherAllow=" + dbltxtOtherAllow + ",EmployeeCPF=" + dbltxtEmployeeCPF + ",Funds=" + dbltxtFunds + ",MBMF=" + dbltxtMBMF + " Where Emp_id =" + Emp_ID + " AND IRYear=" + Utility.ToInteger(cmbYear.SelectedValue);
                                    }
                                    try
                                    {
                                    if (sSQL != "")
                                    {
                                        DataAccess.ExecuteStoreProc(sSQL);
                                        ViewState["actionMessage"] = "Success|Year of assessment submitted for selected employee.";
                                    }
                                            
                                    }
                                    catch (Exception msg)
                                    {
                                    //lblerror.Text = msg.Message.ToString();
                                    ViewState["actionMessage"] = "Warning|Some error occured.Please try again later.";
                                }
                                }
                            }
                        }
                        bindTable1();
                        RadGrid1.DataBind();
                    }
                    ((Button)commandItem.FindControl("btnsubmit")).Enabled = true;               
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }

        #region Import from Excel
        protected void chkId_CheckedChanged(object sender, EventArgs e)
        {
            if (chkId.Checked)
            {
                FileUpload.Visible = true;
       
            }
            else
            {
                FileUpload.Visible = false;
            
            }
        }
     
        bool res;
        protected bool ExcelImport()
        {

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
                   // ShowMessageBox("Please Select File to be uploaded");
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
                    string stfilepath = Server.MapPath(@"..\\Documents\\IR8A\" + StrFileName + strExtent);
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);

                        string filename = StrFileName + strExtent;
                        ImportExcelTosqlServer(filename);
                       // ViewState["actionMessage"] = "Success|"; // if needed to be used later

                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                    }
                }

            }
            //lblerror.Text = strMsg;

            return res;
        }
        string col, Empcode = "", ICNUMBER, Empcode1, sQLFinal;
        int IRYear,j=0;
        decimal GrossPay, Bonus, DirectorFee, Commission, Pension, TransAllow, EntAllow, OtherAllow, EmployeeCPF, Funds, MBMF;
        DataTable dt;
        public void ImportExcelTosqlServer(string filename)
        {
            String emplist = String.Empty;
             dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            //SqlQuery.Append("INSERT INTO [dbo].[EmployeeEarning] ([IRYear],[Emp_ID],[GrossPay],[Bonus] ,[DirectorFee],[Commission],[Pension],[TransAllow],[EntAllow],[OtherAllow],[EmployeeCPF],[Funds] ,[MBMF]) VALUES ");
            try
            {        
                foreach (DataRow dr in dt.Rows)
                {
                    ICNUMBER = dr["IC"].ToString();
                    string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                    SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                    if (dr_empcode.Read())
                    {
                        Empcode = dr_empcode["emp_code"].ToString();
                    }
                    else
                    {
                        Empcode = "";
                    }
                    if (Empcode != "")
                    {

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (i > 0)//skip the first 1 column
                        {
                            col = dt.Columns[i].ToString();

                            IRYear = Convert.ToInt32(cmbYear.SelectedValue.ToString());

                            GrossPay = Convert.ToDecimal(Check(dr["GrossPay"].ToString()));
                            Bonus = Convert.ToDecimal(Check(dr["Bonus"].ToString()));
                            DirectorFee = Convert.ToDecimal(Check(dr["DirectorFee"].ToString()));
                            Commission = Convert.ToDecimal(Check(dr["Commission"].ToString()));
                            Pension = Convert.ToDecimal(Check(dr["Pension"].ToString()));
                            TransAllow = Convert.ToDecimal(Check(dr["TransAllow"].ToString()));
                            EntAllow = Convert.ToDecimal(Check(dr["EntAllow"].ToString()));
                            OtherAllow = Convert.ToDecimal(Check(dr["OtherAllow"].ToString()));
                            EmployeeCPF = Convert.ToDecimal(Check(dr["EmployeeCPF"].ToString()));
                            Funds = Convert.ToDecimal(Check(dr["Funds"].ToString()));
                            MBMF = Convert.ToDecimal(Check(dr["MBMF"].ToString()));

                             
                         
                              
                         }
                         DataAccess.FetchRS(CommandType.Text, @"UPDATE [dbo].[EmployeeEarning]SET[GrossPay] = " + GrossPay + ",[Bonus] = " + Bonus + ",[DirectorFee] = " + DirectorFee + ",[Commission] = " + Commission + ",[Pension] = " + Pension + ",[TransAllow] = " + TransAllow + ",[EntAllow] = " + EntAllow + ",[OtherAllow] = " + OtherAllow + ",[EmployeeCPF] = " + EmployeeCPF + ",[Funds] = " + Funds + ",[MBMF] = " + MBMF + " WHERE IRYear=" + IRYear + "and  Emp_ID =" + Empcode + "", null);                  
                            
                        }
                    }

                 
                    
                }                   



               
            }
            catch (Exception e)
            {
                //ShowMessageBox(e.Message.ToString());
                ViewState["actionMessage"] = "Warning|Some error occured.Please try again later.";

                return;
            }

        }
        //if there is no value then return 0
        private string Check(string p)
        {
            if (p == "")
                return "0";
            else
                return p;
        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/IR8A/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                string SheetName = "Sheet1";//here enter sheet name        
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
                //Response.Write(ex.Message);
                ViewState["actionMessage"] = "Warning|Some error occured.Please try again later.";
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

        public Color ColorChange(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID,Datafield);//get gross value for each cell

            if (Convert.ToInt32(grossvalue) > 0)
                // return Color.Yellow;
                return Color.LightYellow;
            else
                return Color.White;
        }

        public string ToolTipValue(string Emp_ID, string val, string Datafield)
        {
            string grossvalue = GetGrossvalue(Emp_ID, Datafield);//get gross value for each cell
            return grossvalue;
        }

        private string GetGrossvalue(string Emp_ID,  string Datafield)
        {
            if (chkId.Checked)
            {
                string sql_check = "select Top 1 " + Datafield + "  from [EmployeeEarning] where Emp_ID=" + Emp_ID + " and IRYear=" + cmbYear.SelectedValue + "";
                SqlDataReader dr_grossvalue = DataAccess.ExecuteReader(CommandType.Text, sql_check, null);
                if (dr_grossvalue.HasRows)
                {
                    if (dr_grossvalue.Read())
                    {
                        grossvalue = dr_grossvalue["" + Datafield + ""].ToString();
                    }
                }
            }
            return grossvalue;
        }

        #endregion

        protected void LoadGridSettingsPersister()//call directly from page load
        {
            GridSettingsPersister obj = new GridSettingsPersister();
            obj.GrabGridSettingsPersister(Utility.ToString(Session["Username"]), RadGrid1);
        }

        protected void RadGrid1_GridExporting(object source, GridExportingArgs e)
        {
            GridSettingsPersister obj1 = new GridSettingsPersister();
            obj1.ExportGridHeader("107", Session["CompanyName"].ToString(), Session["Emp_Name"].ToString(), e);
        }
    }
}
