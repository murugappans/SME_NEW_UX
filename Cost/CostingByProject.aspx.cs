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
using System.Data.OleDb;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using Excel= Microsoft.Office.Interop.Excel;



namespace SMEPayroll.Cost
{
    public partial class CostingByProject : System.Web.UI.Page
    {
        string _actionMessage = "";
        int comp_id;
        public  DataSet dsFill=new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            ViewState["actionMessage"] = "";

            comp_id = Utility.ToInteger(Session["Compid"]);

            if (!IsPostBack)
            {
                LoadHistoryDropdown();
                btnSubmit.Enabled = false;
                BindGrid();
             //   errormsg.Text = "";

            }

        
        }

        protected void bindgrid1(object sender, EventArgs e)
        {
            BindGrid();
        }

      

        private void LoadHistoryDropdown()
        {
            DataSet ds_leave = new DataSet();
            string sql = "select Distinct AsDate,CONVERT(VARCHAR(10), AsDate, 103) as FormateDate  from dbo.Cost_ByProject where Emp_code in (select Emp_code from employee where Company_Id='" + comp_id + "') order by AsDate Desc";
            ds_leave = getDataSet(sql);
            drphistorydate.Items.Clear();
            drphistorydate.DataSource = ds_leave.Tables[0];
            drphistorydate.DataTextField = ds_leave.Tables[0].Columns["FormateDate"].ColumnName.ToString();
            drphistorydate.DataValueField = ds_leave.Tables[0].Columns["AsDate"].ColumnName.ToString();
            drphistorydate.DataBind();
            drphistorydate.Items.Insert(0, new ListItem("-select date-", ""));
            drphistorydate.Items.FindByText("-select date-").Selected = true;
        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        public void BindGrid()
        {
            //dsFill = new DataSet();
            string sSQL = "sp_CostingByProject";
            SqlParameter[] parms = new SqlParameter[2];
            parms[0] = new SqlParameter("@compid", Utility.ToInteger(comp_id));

            parms[1] = new SqlParameter("@Date", drphistorydate.SelectedValue);
            dsFill = DataAccess.ExecuteSPDataSet(sSQL, parms);


            if (dsFill.Tables.Count > 0)
            {
                Session["dsFill"] = dsFill;

                #region Bind in Textbox column
                string strColName = "";
                int i = 3;

                foreach (DataColumn dc in dsFill.Tables[0].Columns)
                {

                    if (i <= 40)
                    {

                        string templateColumnName = dc.ColumnName.ToString().ToUpper();

                        if (templateColumnName != "EMP_CODE" && templateColumnName != "FULLNAME" && templateColumnName != "TIME_CARD_NO")
                        {
                            strColName = templateColumnName.ToString();
                        }

                        if (strColName.ToString().Length > 0)
                        {
                            string strc = "Column" + i.ToString();
                            if (strColName.Length > 20)
                            {
                                // this.RadGrid1.Columns[i].HeaderText = strColName.ToString().Substring(0, 10);
                                this.RadGrid1.Columns[i].HeaderText = strColName.ToString();
                                this.RadGrid1.Columns[i].Display = true;
                            }
                            else
                            {
                                this.RadGrid1.Columns[i].HeaderText = strColName.ToString();
                                this.RadGrid1.Columns[i].Display = true;
                            }
                            i++;
                        }
                    }
                }
                #endregion



                this.RadGrid1.DataSource = dsFill;
                RadGrid1.DataBind();
            }
        }
     

        protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
        {
            dsFill = (DataSet)Session["dsFill"];

             #region Dynamic textbox
            string strColName = "";
            if (e.Item is GridDataItem)
            {
                int i = 3;
                foreach (DataColumn dc in dsFill.Tables[0].Columns)
                {
                    if (i <= 40)
                    {
                        string templateColumnName = dc.ColumnName.ToString().ToUpper();

                        if (templateColumnName != "EMP_CODE" && templateColumnName != "FULLNAME" && templateColumnName != "TIME_CARD_NO")
                        {
                            strColName = templateColumnName.ToString();
                        }
                        if (strColName.ToString().Length > 0)
                        {
                            TextBox textBox = new TextBox();
                            textBox.ID = "txt" + templateColumnName;
                            textBox.DataBinding += new EventHandler(textBox_DataBinding);
                            textBox.Width = Unit.Pixel(35);
                            textBox.Height = Unit.Pixel(55);

                            if (dsFill.Tables[0].Rows[e.Item.ItemIndex][dc].ToString() != "0.00")
                            {
                                textBox.Text = dsFill.Tables[0].Rows[e.Item.ItemIndex][dc].ToString();//dsFill.Tables[0].Rows[e.Item.ItemIndex][dc].ToString();
                            }

                            textBox.Attributes.Add("onkeypress", "return isNumericKeyStrokeDecimal(event);");
                           
                            GridDataItem item = e.Item as GridDataItem;

                            string strc = "Column" + i.ToString();
                            item[strc].Controls.Add(textBox);
                            if (strColName.Length > 10)
                            {
                                this.RadGrid1.Columns[i].HeaderText = strColName.ToString().Substring(0, 10);
                            }
                            else
                            {
                                this.RadGrid1.Columns[i].HeaderText = strColName.ToString();
                            }
                            i++;
                        }
                    }
                }
            }
            #endregion


        }



        public void textBox_DataBinding(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            GridDataItem container = (GridDataItem)t.NamingContainer;
            t.Height = Unit.Pixel(17);
            t.Width = Unit.Pixel(70);
            t.CssClass = "txtheight";
        }

        
      


        int subprojectid;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string strUpdateDelSQL = "";
            StringBuilder strInsertAddition = new StringBuilder();
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);

            foreach (GridItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];
                    if (chkBox.Checked == true)
                    {
                        string strEmpCode = this.RadGrid1.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
                        double Percentage = 0;
                        for (int i = 3; i <= 20; i++)
                        {
                            string strcol = "Column" + i.ToString();
                            if (this.RadGrid1.Items[dataItem.ItemIndex][strcol].Controls.Count > 0)
                            {

                                Percentage = Utility.ToDouble(((TextBox)this.RadGrid1.Items[dataItem.ItemIndex][strcol].Controls[0]).Text.ToString());

                                if (Percentage > 0)
                                {
                                    string Subproject = ((TextBox)this.RadGrid1.Items[dataItem.ItemIndex][strcol].Controls[0]).ID.ToString().Replace("txt", "");

                                    #region Get Subproject id
                                    
                                    string sqlqry = "select id from SubProject where Sub_Project_Name='" + Subproject + "'";
                                    SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlqry, null);                    
                                        while (dr.Read())
                                        {
                                            subprojectid = Utility.ToInteger(dr.GetValue(0));
                                        }
                                    #endregion
                                        if (subprojectid > 0)
                                        {
                                            //check whether the record exist already
                                            string sDate = dtp1.SelectedDate.Value.Month + "/" + dtp1.SelectedDate.Value.Day + "/" + dtp1.SelectedDate.Value.Year;
                                            string sqlupdate = "SELECT *  FROM [Cost_ByProject] where [SubProjectId]='" + subprojectid + "' AND [Emp_code]=" + strEmpCode + " AND [AsDate]='" + sDate + "'";
                                            SqlDataReader dr_ins = DataAccess.ExecuteReader(CommandType.Text, sqlupdate, null);
                                            if (dr_ins.HasRows)
                                             {
                                                 strInsertAddition.Append("UPDATE  [Cost_ByProject]  SET  [Percentage] =" + Percentage + "  where [SubProjectId]='" + subprojectid + "' AND [Emp_code]=" + strEmpCode + " AND [AsDate]='" + sDate + "'");
                                             }
                                             else
                                             {
                                                 strInsertAddition.Append("INSERT INTO [Cost_ByProject]([SubProjectId],[Emp_code],[Percentage],[AsDate])  VALUES ('" + subprojectid + "'," + strEmpCode + "," + Percentage + ",'" + sDate + "')");
                                             }
                                        }
                                }
                            }
                        }
                    }
                }
            }

         

      
            if (strInsertAddition.ToString().Length > 0)
            {
                strUpdateDelSQL = "" + strInsertAddition + "";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(strUpdateDelSQL);
                    LoadHistoryDropdown();
                }
                catch (Exception Ex)
                {
                    // errormsg.Text = "Error :" + Ex.Message.ToString();
                    _actionMessage = "Warning|Error :" + Ex.Message.ToString();
                    ViewState["actionMessage"] = _actionMessage;
                }
                // ShowMessageBox("Additions Updated Successfully");
                // RadGrid1.Controls.Add(new LiteralControl("<font color = 'Red'>Updated Successfully</font> "));
                // errormsg.Text = "Updated Successfully";
                _actionMessage = "Success|Updated Successfully";
                ViewState["actionMessage"] = _actionMessage;
            }




        }

        double Percentage1 = 0,PercentageTotal = 0;
        string strcol;
        int k = 0;
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = false;
            foreach (GridDataItem item in this.RadGrid1.MasterTableView.Items)
            {
                if (item is GridDataItem)
                {
                    GridDataItem dataItem = (GridDataItem)item;
                    CheckBox chkBox = (CheckBox)dataItem["GridClientSelectColumn"].Controls[0];

                   
                    Percentage1 = 0;//each row start with 0
                    PercentageTotal = 0;//each row start with 0
                    if (chkBox.Checked == true)
                    {
                        string strEmpCode = this.RadGrid1.Items[dataItem.ItemIndex]["Emp_Code"].Text.ToString();
                       
                        for (int i = 3; i <= 40; i++)
                        {
                            strcol = "Column" + i.ToString();
                            if (this.RadGrid1.Items[dataItem.ItemIndex][strcol].Controls.Count > 0)
                            {

                                Percentage1 = Utility.ToDouble(((TextBox)this.RadGrid1.Items[dataItem.ItemIndex][strcol].Controls[0]).Text.ToString());

                                if (Percentage1 >= 0)
                                {
                                    PercentageTotal = PercentageTotal+Percentage1;

                                }
                            }
                           
                        }

                        //display in  total column
                        Label lbl = (Label)dataItem.FindControl("lblTotal");
                        lbl.Text = Convert.ToString(PercentageTotal);


                        //change the color
                        if (PercentageTotal != 100)
                        {
                            k = k + 1;// check is there any value not equal to 100
                            lbl.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                           lbl.ForeColor = System.Drawing.Color.Black;
                        }



                    }


                }
            }


            if (k > 0)//if any of the selected row is invalid
            {
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
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
        protected void bindgrid(object sender, EventArgs e)
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
                string StrFileName = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType = FileUpload.PostedFile.ContentType;
                int IntFileSize = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                {
                    strMsg = "Please Select File to be uploaded";
                   // ShowMessageBox("Please Select File to be uploaded");
                    _actionMessage = "Warning|Please Select File to be uploaded";
                    ViewState["actionMessage"] = _actionMessage;
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
            //lblerror.Text = strMsg;
            _actionMessage = "Warning|"+strMsg;
            ViewState["actionMessage"] = _actionMessage;

            return res;
        }

        public void ImportExcelTosqlServer(string filename)
        {
            DataTable dt = GetDataFromExcel(filename);

            
            //adding Emp_code in first column
            DataColumn Col = dt.Columns.Add("Emp_Code", System.Type.GetType("System.Int32"));
            Col.SetOrdinal(0);// to put the column in position 0;


            //Fetch name and emp-code from timecard
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToString(GetName(row["TIME_CARD_NO"], "FULLNAME")) != "0" && Convert.ToInt32(GetName(row["TIME_CARD_NO"], "Emp_Code")) != 0)
                {
                    row["FULLNAME"] = GetName(row["TIME_CARD_NO"], "FULLNAME");
                    row["Emp_Code"] = GetName(row["TIME_CARD_NO"], "Emp_Code");
                }
            
            }


            //delete the row if the employee does not exist
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (DBNull.Value.Equals(dr["Emp_Code"]))
                    dr.Delete();
            }
            dt.AcceptChanges();


            DataSet dsFill = new DataSet();
            dsFill.Tables.Add(dt);

            Session["dsFill"] = dsFill;


            this.RadGrid1.DataSource = dsFill;
            RadGrid1.DataBind();
        }


        int Empid;
        string FULLNAME;
        object retval;
        private object GetName(object TimeCard,string val)
        {
            string sqlqry = "select Emp_Code,(E.Emp_Name + ' ' + E.Emp_LName) FULLNAME from Employee E where TIME_CARD_NO='" + TimeCard + "' AND company_ID='"+comp_id+"'";
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
                    retval= FULLNAME;
                }
                else if (val == "Emp_Code")
                {
                    retval= Empid;
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
                OleDbConnection oledbconn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Documents/UploadAddDed/" + filename + "").ToString() + ";Extended Properties=Excel 8.0;");
                //string SheetName = "Sheet1";//here enter sheet name       
                string SheetName = "CostingByProject";//here enter sheet name    
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
               // Response.Write(ex.Message);
                _actionMessage = "Warning|"+ex.Message;
                ViewState["actionMessage"] = _actionMessage;
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

        public static void WriteAttachment(string FileName, string FileType, string content)
        {
            HttpResponse Response = System.Web.HttpContext.Current.Response;
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.ContentType = FileType;
            Response.Write(content);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Response.End();

        }

         protected void btnTemplate_Click(object sender, EventArgs e)
         {
             DataSet dss=new DataSet();
             string sql=" select Sub_Project_Name from subproject SP inner join Project P on  SP.Parent_Project_ID=P.ID  where Company_ID='"+ comp_id +"' order by Sub_Project_Name";
             dss=getDataSet(sql);

             DataTable dt = new DataTable();
             dt.Columns.Add(new DataColumn("FULLNAME"));
             dt.Columns.Add(new DataColumn("TIME_CARD_NO"));


             //change each row to column
             foreach (DataRow row in dss.Tables[0].Rows)
             {
                 foreach (DataColumn column in dss.Tables[0].Columns)
                 {
                     if (row[column] != null) // this will checks the null values also if you want to check
                     {
                            string columnname =Convert.ToString(row[column]);
                            dt.Columns.Add(new DataColumn(columnname));
                     }
                 }
             }



             //DataSet dd = new DataSet();
             //dd.Tables.Add(dt);

             //string fastExportFilePath = "C:\\CosingByProjecttemplate.xls";
          //   string fastExportFilePath = Server.MapPath(@"..\\Documents\\UploadAddDed\CosingByProjecttemplate.xls");
          //   FastExportingMethod.ExportToExcel(dd, fastExportFilePath);

             



             try
             {
               
                 if (dt == null)
                 {
                     throw new Exception("No Records to Export");
                 }
                 // string Path = "D:\\ImportExcelFromDatabase\\myexcelfile_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + ".xls";
                 string Path = Server.MapPath(@"..\\Documents\\UploadAddDed\CostingByProject.xls");
                 FileInfo FI = new FileInfo(Path);
                 StringWriter stringWriter = new StringWriter();
                 HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWriter);
                 DataGrid DataGrd = new DataGrid();
                 DataGrd.DataSource = dt;
                 DataGrd.DataBind();

                 DataGrd.RenderControl(htmlWrite);
                 string directory = Path.Substring(0, Path.LastIndexOf("\\"));// GetDirectory(Path);
                 if (!Directory.Exists(directory))
                 {
                     Directory.CreateDirectory(directory);
                 }

                 System.IO.StreamWriter vw = new System.IO.StreamWriter(Path, true);
                 stringWriter.ToString().Normalize();
                 vw.Write(stringWriter.ToString());
                 vw.Flush();
                 vw.Close();
                 WriteAttachment(FI.Name, "application/vnd.ms-excel", stringWriter.ToString());
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }


         }




        #endregion




        #endregion

    }
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
        excelWorkbook.SaveAs(outputPath, Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
        Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive,
        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        excelWorkbook.Close(true, Type.Missing, Type.Missing);
        excelWorkbook = null;
        #endregion


        // Release the Application object
        excelApp.Quit();
        excelApp = null;

        // Collect the unreferenced objects
       // GC.Collect();
       // GC.WaitForPendingFinalizers();


      ////  HttpContext.Current.Response.Clear();
      //  HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", outputPath));
      //  HttpContext.Current.Response.ContentType = "application/ms-excel";
      //  HttpContext.Current.Response.Write(outputPath);
 



        //ram
        //for opening a file

        //excelApp.Workbooks.Open(@"C:\YourPath\YourExcelFile.xls",
        //string openPath = outputPath + "CosingByProjecttemplate.xls";
       // excelApp.Workbooks.Open(outputPath,
       //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
       //Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

    }

}