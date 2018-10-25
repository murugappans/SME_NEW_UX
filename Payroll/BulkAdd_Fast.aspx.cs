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
using SMEPayroll.TextTemplate;
using System.IO;
using System.Text;

using System.Data.OleDb;
using System.Collections.Generic;

namespace SMEPayroll.Payroll
{
    public partial class BulkAdd_Fast : System.Web.UI.Page
    {
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
        DataSet dsFill;
        string strWF = "";
        string strEmpvisible = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            comp_id = Utility.ToInteger(Session["Compid"]);
           
            if (!IsPostBack)
            {
                #region Yeardropdown
                cmbYear.DataBind();
                #endregion
                cmbYear.SelectedValue = Utility.ToString(System.DateTime.Today.Year);
                MonthFill();

            }
        }

        #region Month and yeardropdown -old code
          protected void cmbYear_selectedIndexChanged(object sender, EventArgs e)
        {
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();
            MonthFill();
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
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
            if (Session["ROWID"] != null)
            {
                SetControlDate(Session["ROWID"].ToString());
            }
            else
            {
                SetControlDate(cmbMonth.SelectedValue);
            }
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
                strstdatemdy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format);
                strendatemdy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("MM/dd/yyyy", format);
                strstdatedmy = Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("dd/MM/yyyy", format);
                strendatedmy = Convert.ToDateTime(Session["PaySubEndDate"].ToString()).ToString("dd/MM/yyyy", format);
            }
        }

        #endregion

        bool output;
        protected void bindgrid(object sender, ImageClickEventArgs e)
        {
            Session["ROWID"] = cmbMonth.SelectedValue.ToString();
            Session["ROWYEAR"] = cmbYear.SelectedValue.ToString();

            output = ExcelImport();
            if (output)
            {
                //BuildTable();
            }
        }



        #region Excel Import
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
                        ImportExcelTosqlServer(filename);



                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                    }
                }

            }
            lblerror.Text = strMsg;

            return res;
        }
        string col, Empcode, ICNUMBER, Empcode1;
        decimal A1, Addition;
        List<Logg> list = new List<Logg>();
        Logg obj;
        public void ImportExcelTosqlServer(string filename)
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            DataTable dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append("");

            StringBuilder SqlQuery_alreadyExist = new StringBuilder();
            SqlQuery_alreadyExist.Append("");
            try
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 1)//skip the first 2 column
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            col = dt.Columns[i].ToString();

                            //check whether IC number is present
                            //if yes--> get empno from that
                            //else --> check the empno directly
                            ICNUMBER = dr["IC NUMBER"].ToString();
                            if (ICNUMBER == "")
                            {
                                Empcode = dr["EMP ID"].ToString();
                            }
                            else
                            {
                                Empcode = null;
                                string sql = " select emp_code from employee where ic_pp_number='" + ICNUMBER + "'";
                                SqlDataReader dr_empcode = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                                if (dr_empcode.Read())
                                {
                                    Empcode = dr_empcode["emp_code"].ToString();
                                }
                            }
                            if (Empcode == null)
                            {
                                Empcode = dr["EMP ID"].ToString();
                            }
                            //


                            if (dr[dt.Columns[i].ToString()] == System.DBNull.Value)
                            {
                                A1 = 0;
                            }
                            else
                            {
                                A1 = Convert.ToDecimal(dr[dt.Columns[i].ToString()]);//A1 = dr["A1"].ToString();
                            }
                            #region converting A1 to mapped column id
                            string sql_emp = "SELECT Additions_Id FROM [MapAdditions] where Company_id='" + comp_id + "' AND  MapVariable='" + dt.Columns[i].ToString() + "'";
                            SqlDataReader dr_addit = DataAccess.ExecuteReader(CommandType.Text, sql_emp, null);
                            if (dr_addit.Read())
                            {
                                if (dr_addit["Additions_Id"] == System.DBNull.Value)
                                {
                                    Addition = 0;
                                }
                                else
                                {

                                    Addition = Convert.ToInt32(dr_addit["Additions_Id"]);
                                }

                            }
                            #endregion

                            #region Convert the (Empcode)timecardId to Emp_code
                            string sql_timecard2empcode = " select emp_code from employee where time_card_no='" + Empcode + "'";
                            SqlDataReader dr_empcode_timecard2empcode = DataAccess.ExecuteReader(CommandType.Text, sql_timecard2empcode, null);
                            if (dr_empcode_timecard2empcode.HasRows)
                            {
                                if (dr_empcode_timecard2empcode.Read())
                                {
                                    Empcode1 = dr_empcode_timecard2empcode["emp_code"].ToString();
                                }
                            #endregion

                                //if (dr_addit.HasRows && Empcode!="")//if mapped then only insert
                                if (dr_addit.HasRows && Empcode1 != "" && Addition > 0 && A1 > 0)//if mapped then only insert
                                {
                                    string sql_checkAlreadyExist = "select * from emp_additions where Emp_Code='" + Empcode1 + "' and BulkAddInMonth='" + cmbMonth.SelectedValue + "' and status='U'";
                                    SqlDataReader checkAlreadyExist = DataAccess.ExecuteReader(CommandType.Text, sql_checkAlreadyExist, null);
                                    if (checkAlreadyExist.HasRows)
                                     {
                                         //Response.Write("Data Exist already and changed:  " + Empcode + " additions:" + dt.Columns[i].ToString()+"</br>");
                                        // list.Add(Empcode, dt.Columns[i].ToString());
                                         obj = new Logg(Empcode, dt.Columns[i].ToString());
                                         list.Add(obj);
                                         SqlQuery_alreadyExist.Append("Delete From Emp_Additions Where  Emp_Code='" + Empcode1 + "' and BulkAddInMonth= '" + cmbMonth.SelectedValue + "';");
                                     }
                                     
                                     #region if payroll processed dont disturb, else Insert
                                         string sql_checkAlreadyExist1 = "select * from emp_additions where Emp_Code='" + Empcode1 + "' and BulkAddInMonth='" + cmbMonth.SelectedValue + "' and status='L'";
                                         SqlDataReader checkAlreadyExist1 = DataAccess.ExecuteReader(CommandType.Text, sql_checkAlreadyExist1, null);
                                         if (checkAlreadyExist1.HasRows == false)
                                         {
                                             string sqlInsert = "Insert Into Emp_Additions (trx_type,trx_amount, trx_period, emp_code, status, claimstatus, basis_arriving_payment, service_length, iras_approval, additionsforyear, BulkAddInMonth,recpath) values ('" + Addition + "'," + A1.ToString() + ",'" + Convert.ToDateTime(Session["PaySubStartDate"].ToString()).ToString("MM/dd/yyyy", format) + "','" + Empcode1 + "','U','Approved','',0,'No','" + Session["ROWYEAR"].ToString() + "','" + Session["ROWID"].ToString() + "','import')";
                                             SqlQuery.Append(sqlInsert);
                                         }
                                     #endregion
                                 
                                   
                                    
                                  
                                }
                            }

                        }
                    }
                }


                RadGrid1.DataSource = list;
                RadGrid1.DataBind();

                if (SqlQuery_alreadyExist.Length > 0)
                {
                    DataAccess.FetchRS(CommandType.Text, SqlQuery_alreadyExist.ToString(), null);
                }
                if (SqlQuery.Length > 0)
                {
                    DataAccess.FetchRS(CommandType.Text, SqlQuery.ToString(), null);
                }

                ShowMessageBox("Inserted Sucessfully!!");
            }
            catch (Exception e)
            {
                //DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                ShowMessageBox("Error for the Employee:" + Empcode + " Msg-" + e.Message.ToString());
                //lblerror.Text = "Error in Data" + e.Message.ToString();
            }

        }
        //http://www.dotnetspider.com/forum/286377-Reading-excel-file-row-by-row-storing-into-database.aspx
        public DataTable GetDataFromExcel(string filename)
        {
            DataTable dt = new DataTable();
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/UploadAddDed/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
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
                Response.Write(ex.Message);
            }
            return dt;
        }



        #endregion


        #region Utili
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
        #endregion
    }
}



public class Logg
{
    private string _empcode;

	public string Empcode
	{
		get { return _empcode;}
		set { _empcode = value;}
	}
	
    private string _addition;

	public string Addition
	{
		get { return _addition;}
		set { _addition = value;}
	}
     
    public Logg (string empcode,string addition)
	{
        _empcode=empcode;
        _addition=addition;
	}
}
