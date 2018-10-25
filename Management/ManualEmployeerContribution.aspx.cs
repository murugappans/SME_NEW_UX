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



using System.Data.OleDb;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMEPayroll.Management
{

    public partial class ManualEmployeerContribution : System.Web.UI.Page
    {
        int compid, comp_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            compid = Utility.ToInteger(Session["Compid"].ToString());
            comp_id = Utility.ToInteger(Session["Compid"].ToString());
            if (!IsPostBack)
            {
                cmbYear.SelectedValue = DateTime.Today.Year.ToString();
                cmbMonth.SelectedValue = DateTime.Today.Month.ToString();
            }
            RadGrid1.NeedDataSource+=new GridNeedDataSourceEventHandler(RadGrid1_NeedDataSource);
            xmldtYear1.ConnectionString = Session["ConString"].ToString();
        }

       

        
        private static DataSet GetDataSet(string query)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, query, null);
            return ds;
        }
        private DataSet AdditionDetails
        {
            get
            {
                //DataSet ds = new DataSet();
                //string sSQL = "";
                //int compID = Utility.ToInteger(Session["Compid"].ToString());
                ////sSQL = "select Aid,MapVariable,Additions_id,(select [desc] from Additions_Types where id=Additions_id)as [AdditionType] from  MapAdditions where Company_id='" + compID + "'";
                //sSQL = "SELECT  datename(month,A.startdate)+' '+ CONVERT(varchar(300), year(A.startdate)) as period,(select Apcategory  from dbo.APCategory where APCatId=A.[APType]) Category,[APType] as catid ,(select  (isnull(emp_name,'') + ' ' + isnull(emp_lname,'')) from Employee where emp_code=A.Emp_code )  EMP_NAME,[Eid],[TimeCardId],[Emp_code],[Amount],[APType],[StartDate],[EndDate] FROM [AdditionPay] A where [Company_id]='" + compID + "' and year(StartDate)='" + cmbYear.SelectedValue + "' and month(StartDate)='" + cmbMonth.SelectedValue + "' ";
                //ds = GetDataSet(sSQL);
                //return ds;


                int compid = Utility.ToInteger(Session["Compid"].ToString());
                SqlParameter[] parms1 = new SqlParameter[3];
                parms1[0] = new SqlParameter("@Company ", compid);
                parms1[1] = new SqlParameter("@year ", cmbYear.SelectedValue);
                parms1[2] = new SqlParameter("@month ", cmbMonth.SelectedValue);
                DataSet ds_new = new DataSet();
                ds_new = DataAccess.ExecuteSPDataSet("SP_manualEmployerContribution", parms1);
                return ds_new;

            }
        }
        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            if (this.AdditionDetails != null)
            {
                this.RadGrid1.DataSource = this.AdditionDetails;
            }
        }
        


        


        protected void bindgrid(object sender,  EventArgs  e)
        {
           
            bindgriddata();

        }

        private void bindgriddata()
        {
            if (this.AdditionDetails != null)
            {

                this.RadGrid1.DataSource = this.AdditionDetails;
                this.RadGrid1.DataBind();
            }
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

            string strMsg = "";
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                string filename = FileUpload.PostedFile.FileName.ToString();
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
                  
                    string stfilepath = Server.MapPath(@"~/Documents/UploadAddDed/Book1.xls");
                    try
                    {
                        FileUpload.PostedFile.SaveAs(stfilepath);

                      


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
        decimal A1,Addition;
        int Month, year,categoryid;
        public void ImportExcelTosqlServer(string filename)
        {
          DataTable dt = GetDataFromExcel(filename);
            StringBuilder SqlQuery = new StringBuilder();
            SqlQuery.Append("");
            try
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i > 3)//skip the first 2 column
                    {
                        foreach (DataRow dr in dt.Rows)
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
                                #region Get the start date and end date
                                Month = Convert.ToInt32(dr["MONTH"].ToString());
                                year = Convert.ToInt32(dr["YEAR"].ToString());

                                //get first day in the month
                                DateTime startOfMonth = new DateTime(year, Month, 1);
                                //get last day in the month
                                int DaysInMonth = DateTime.DaysInMonth(year, Month);
                                DateTime lastDay = new DateTime(year, Month, DaysInMonth);
                                #endregion

                                #region GET CATEGORYID
		                            string sql_cat = "select APCatId from   dbo.APCategory WHERE companyid='"+comp_id+"' and apcategory='"+col+"'";
                                    SqlDataReader dr_cat = DataAccess.ExecuteReader(CommandType.Text, sql_cat, null);
                                    if (dr_cat.Read())
                                    {
                                        categoryid = Convert.ToInt32(dr_cat["APCatId"].ToString());
                                    }
	                            #endregion


                              
                                    if (Empcode != "")//if mapped then only insert
                                    {

                                        SqlQuery.Append("INSERT INTO [AdditionPay]([Emp_code],[TimeCardId],[StartDate],[EndDate],[Company_id],[APType],[Amount]) VALUES(" + Empcode + ",'','" + startOfMonth.Date.Month + "/" + startOfMonth.Date.Day + "/" + startOfMonth.Date.Year + "','" + lastDay.Date.Month + "/" + lastDay.Date.Day + "/" + lastDay.Date.Year + "'," + comp_id + " ,'" + categoryid + "','" + dr[col].ToString() + "' )");
                                    }
                                
                            }

                        }
                    }
                }

            

           
                //DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                DataAccess.FetchRS(CommandType.Text, SqlQuery.ToString(), null);
                bindgriddata();
               // lblerror.Text = "Updated Sucessfully";

                ShowMessageBox("Updated Sucessfully");
            }
            catch (Exception e)
            {
                //DataAccess.FetchRS(CommandType.Text, "delete from Temp_Emp_Additions", null);
                ShowMessageBox("Error for the Employee:"+Empcode+" Msg-"+ e.Message.ToString());
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
            try
            {
                //OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Book1.xls").ToString() + ";Extended Properties=Excel 8.0;");
               OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+filename+ ";Extended Properties=Excel 8.0;");
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
            string sql_delete = "delete from dbo.AdditionPay where MONTH(startdate)='" + cmbMonth.SelectedValue + "'  and YEAR(startdate)='" + cmbYear.SelectedValue + "'  and company_id='"+comp_id+"'";
            DataAccess.FetchRS(CommandType.Text, sql_delete, null);
            bindgriddata();
        }


        #endregion


        
    }

    static class FastExportingMethod
    {

        public static void ExportToExcel(DataSet dataSet, string outputPath)
        {
            // Create the Excel Application object
            Excel.ApplicationClass excelApp = new Excel.ApplicationClass();

            // Create a new Excel Workbook
            Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);

            int sheetIndex = 0;

            // Copy each DataTable
            foreach (System.Data.DataTable dt in dataSet.Tables)
            {

                // Copy the DataTable to an object array
                object[,] rawData = new object[dt.Rows.Count + 1, dt.Columns.Count];

                // Copy the column names to the first row of the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    rawData[0, col] = dt.Columns[col].ColumnName;
                }

                // Copy the values to the object array
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        rawData[row + 1, col] = dt.Rows[row].ItemArray[col];
                    }
                }

                // Calculate the final column letter
                string finalColLetter = string.Empty;
                string colCharset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int colCharsetLen = colCharset.Length;

                if (dt.Columns.Count > colCharsetLen)
                {
                    finalColLetter = colCharset.Substring(
                        (dt.Columns.Count - 1) / colCharsetLen - 1, 1);
                }

                finalColLetter += colCharset.Substring(
                        (dt.Columns.Count - 1) % colCharsetLen, 1);

                // Create a new Sheet
                Excel.Worksheet excelSheet = (Excel.Worksheet)excelWorkbook.Sheets.Add(
                    excelWorkbook.Sheets.get_Item(++sheetIndex),
                    Type.Missing, 1, Excel.XlSheetType.xlWorksheet);

                excelSheet.Name = dt.TableName;
                //excelSheet.Name = "Sheet1";

                // Fast data export to Excel
                string excelRange = string.Format("A1:{0}{1}",
                    finalColLetter, dt.Rows.Count + 1);

                excelSheet.get_Range(excelRange, Type.Missing).Value2 = rawData;

                // Mark the first row as BOLD
                ((Excel.Range)excelSheet.Rows[1, Type.Missing]).Font.Bold = true;
            }

            //r
            // Save and Close the Workbook
            #region MyRegion
            //excelWorkbook.SaveAs(outputPath, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
            //Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //excelWorkbook.Close(true, Type.Missing, Type.Missing);
            //excelWorkbook = null;
            #endregion


            // Release the Application object
            excelApp.Quit();
            excelApp = null;

            // Collect the unreferenced objects
            GC.Collect();
            GC.WaitForPendingFinalizers();



            //ram
            //for opening a file

            //excelApp.Workbooks.Open(@"C:\YourPath\YourExcelFile.xls",
            // string openPath = outputPath + "CosingByProjecttemplate.xls";
            // excelApp.Workbooks.Open(openPath,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        }

    }
}
