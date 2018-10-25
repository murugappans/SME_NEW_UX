using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DataStreams.Csv;
using Telerik.Web.UI;
using Microsoft.VisualBasic;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Collections.Generic;


namespace SMEPayroll.TimeSheet
{
    public partial class MAPTimesheet : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        string strFile;
        string strtranid;
        string fileType;
        ArrayList headers;



        protected void Page_Load(object sender, EventArgs e)
        {

            compid = Utility.ToInteger(Session["Compid"]);
            strtranid = Session["ProcessTranId"].ToString();
            strFile = Session["TS_FileName"].ToString();
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            ButtonDelete.Enabled = false;

            CmdImport.BackColor = Color.White;

            //CmdImport.Enabled = true;
            //btnSave.Enabled = false;
            //btnCancel.Enabled = false;

            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            btnExit.Click += new EventHandler(btnExit_Click);

            drpUserid.SelectedIndexChanged += new EventHandler(drpUserid_SelectedIndexChanged);
            drptimesheetdateSingle.SelectedIndexChanged += new EventHandler(drptimesheetdateSingle_SelectedIndexChanged);
            drpProjectSingle.SelectedIndexChanged += new EventHandler(drpProjectSingle_SelectedIndexChanged);


            drptimesheettimeInSingle.SelectedIndexChanged += new EventHandler(drptimesheettimeInSingle_SelectedIndexChanged);
            drptimesheettimeOutSingle.SelectedIndexChanged += new EventHandler(drptimesheettimeOutSingle_SelectedIndexChanged);
            drptimesheetdateoutSingle.SelectedIndexChanged += new EventHandler(drptimesheetdateoutSingle_SelectedIndexChanged);
            drpTimeSheetDate.SelectedIndexChanged += new EventHandler(drpTimeSheetDate_SelectedIndexChanged);


            DataSet ds = new DataSet();
            string sql = "SELECT FileType FROM TS_FileUploaded WHERE TranID='" + Session["TRANSID"].ToString() + "'";
            ds = DataAccess.FetchRS(CommandType.Text, sql, null);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    fileType = ds.Tables[0].Rows[0][0].ToString();
                }
            }


            if (!Page.IsPostBack)
            {
                FileStream fileStream = new FileStream(strFile, FileMode.Open);
                CSVReader reader = new CSVReader(fileStream);
                if (headers != null)
                {
                    headers.Clear();
                }
                headers = reader.GetCSVLine_new();

                add_item(headers);
                fileStream.Close();
                fileStream = null;

            }


            if (IsPostBack)
            {
                // string txt = drpUserid.SelectedItem.Text.ToString();







                //string txt1 = drpProjectSingle.SelectedItem.Text.ToString();
                //drpProjectSingle.Items.Remove(drpProjectSingle.SelectedItem);




                //   string txt2 = drptimesheetdateSingle.SelectedItem.Text.ToString();
                //drpProjectSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
                ////drptimesheetdateSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
                //drptimesheettimeInSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
                //drptimesheettimeOutSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
                //drptimesheetdateoutSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
                //drpTimeSheetDate.Items.Remove(drptimesheetdateSingle.SelectedItem);


                //string txt3 = drpProjectSingle.SelectedItem.Text.ToString();
                // drpProjectSingle.Items.Remove(drpProjectSingle.SelectedItem);
                //drptimesheetdateSingle.Items.Remove(drpProjectSingle.SelectedItem);
                //drptimesheettimeInSingle.Items.Remove(drpProjectSingle.SelectedItem);
                //drptimesheettimeOutSingle.Items.Remove(drpProjectSingle.SelectedItem);
                //drptimesheetdateoutSingle.Items.Remove(drpProjectSingle.SelectedItem);
                //drpTimeSheetDate.Items.Remove(drpProjectSingle.SelectedItem);





                //  string txt6 = drptimesheetdateoutSingle.SelectedItem.Text.ToString();



            }


        }
        private void add_item(ArrayList header)
        {
            if (Session["ProcessTranId"] != null)
            {
                lblDocNo.Text = "Doc No: " + Session["ProcessTranId"].ToString();





                //int i = this.userid.Items.Count;
                //for (int j = 0; j < i; j++)
                //{
                //    if (userid.Items[j].Selected == true)
                //    {
                //        int numIdx = Array.IndexOf(headers, userid.Items[i]);
                //        List<string> tmp = new List<string>(headers);
                //        tmp.RemoveAt(numIdx);
                //        headers = tmp.ToArray();

                //        //  headers.Remove(userid.Items[i].toString());
                //        i--;
                //    }
                //}


                //drpProjectSingle.Items.Clear();
                //drptimesheetdateSingle.Items.Clear();
                //drptimesheettimeInSingle.Items.Clear();
                //drptimesheettimeOutSingle.Items.Clear();
                //drptimesheetdateoutSingle.Items.Clear();
                //drpTimeSheetDate.Items.Clear();






                DataTable dt = new DataTable();
                //add headers
                int iCount = 0;

                if (fileType.ToUpper() == "M")
                {

                    userid.Items.Insert(iCount, new ListItem("Select", "Select"));
                    timeentry.Items.Insert(iCount, new ListItem("Select", "Select"));
                    eventid.Items.Insert(iCount, new ListItem("Select", "Select"));
                    terminalsn.Items.Insert(iCount, new ListItem("Select", "Select"));

                    rowSingle.Visible = false;
                    rowMulti.Visible = true;

                    row1.Visible = true;
                    row2.Visible = true;
                    row3.Visible = true;
                    row4.Visible = true;

                    row5.Visible = false;
                    row6.Visible = false;
                    row7.Visible = false;
                    row8.Visible = false;
                    row9.Visible = false;
                    row10.Visible = false;
                    row11.Visible = false;

                    foreach (string strHeader in headers)
                    {
                        iCount = iCount + 1;
                        userid.Items.Insert(iCount, strHeader);
                        timeentry.Items.Insert(iCount, strHeader);
                        eventid.Items.Insert(iCount, strHeader);
                        terminalsn.Items.Insert(iCount, strHeader);
                    }
                    //fileStream.Close();
                    //fileStream = null;

                }
                else if (fileType.ToUpper() == "S")
                {

                    drpUserid.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drpProjectSingle.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drptimesheetdateSingle.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drptimesheettimeInSingle.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drptimesheettimeOutSingle.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drptimesheetdateoutSingle.Items.Insert(iCount, new ListItem("Select", "Select"));
                    drpTimeSheetDate.Items.Insert(iCount, new ListItem("Select", "Select"));

                    rowSingle.Visible = true;
                    rowMulti.Visible = false;

                    row1.Visible = false;
                    row2.Visible = false;
                    row3.Visible = false;
                    row4.Visible = false;

                    row5.Visible = true;
                    row6.Visible = true;
                    row7.Visible = true;
                    row8.Visible = true;
                    row9.Visible = true;
                    row10.Visible = true;
                    row11.Visible = true;
                    foreach (string strHeader in headers)
                    {
                        iCount = iCount + 1;
                        drpUserid.Items.Insert(iCount, strHeader);
                        drpProjectSingle.Items.Insert(iCount, strHeader);
                        drptimesheetdateSingle.Items.Insert(iCount, strHeader);
                        drptimesheettimeInSingle.Items.Insert(iCount, strHeader);
                        drptimesheettimeOutSingle.Items.Insert(iCount, strHeader);
                        drptimesheetdateoutSingle.Items.Insert(iCount, strHeader);
                        drpTimeSheetDate.Items.Insert(iCount, strHeader);

                    }
                    // fileStream.Close();
                    // fileStream = null;
                }
            }
        }

        protected void drptimesheetdateSingle_SelectedIndexChanged(object sender, EventArgs e)
        {

            drptimesheettimeInSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
            drptimesheettimeOutSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
            drptimesheetdateoutSingle.Items.Remove(drptimesheetdateSingle.SelectedItem);
            drpTimeSheetDate.Items.Remove(drptimesheetdateSingle.SelectedItem);


        }


        protected void drptimesheettimeInSingle_SelectedIndexChanged(object sender, EventArgs e)
        {




            drptimesheettimeOutSingle.Items.Remove(drptimesheettimeInSingle.SelectedItem);
            drptimesheetdateoutSingle.Items.Remove(drptimesheettimeInSingle.SelectedItem);
            drpTimeSheetDate.Items.Remove(drptimesheettimeInSingle.SelectedItem);

        }

        protected void drptimesheettimeOutSingle_SelectedIndexChanged(object sender, EventArgs e)
        {

            // string txt5 = drptimesheettimeOutSingle.SelectedItem.Text.ToString();
            // drpProjectSingle.Items.Remove(drptimesheettimeOutSingle.SelectedItem);
            //drptimesheetdateSingle.Items.Remove(drptimesheettimeOutSingle.SelectedItem);
            //  drptimesheettimeInSingle.Items.Remove(drptimesheettimeOutSingle.SelectedItem);
            //  drptimesheettimeOutSingle.Items.Remove(drptimesheettimeOutSingle.SelectedItem);
            //drptimesheetdateoutSingle.Items.Remove(drptimesheettimeOutSingle.SelectedItem);
            //drpTimeSheetDate.Items.Remove(drptimesheettimeOutSingle.SelectedItem);


        }

        protected void drptimesheetdateoutSingle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //drpProjectSingle.Items.Remove(drptimesheetdateoutSingle.SelectedItem);
            //drptimesheetdateSingle.Items.Remove(drptimesheetdateoutSingle.SelectedItem);
            //drptimesheettimeInSingle.Items.Remove(drptimesheetdateoutSingle.SelectedItem);
            drptimesheettimeOutSingle.Items.Remove(drptimesheetdateoutSingle.SelectedItem);

            // drpTimeSheetDate.Items.Remove(drptimesheetdateoutSingle.SelectedItem);


        }

        protected void drpTimeSheetDate_SelectedIndexChanged(object sender, EventArgs e)
        {


        }




        protected void drpProjectSingle_SelectedIndexChanged(object sender, EventArgs e)
        {


            //drptimesheetdateSingle.Items.Remove(drpProjectSingle.SelectedItem);
            drptimesheettimeInSingle.Items.Remove(drpProjectSingle.SelectedItem);
            drptimesheettimeOutSingle.Items.Remove(drpProjectSingle.SelectedItem);
            drptimesheetdateoutSingle.Items.Remove(drpProjectSingle.SelectedItem);
            drptimesheetdateSingle.Items.Remove(drpProjectSingle.SelectedItem);

            // drpTimeSheetDate.Items.Remove(drpProjectSingle.SelectedItem);



        }






        protected void drpUserid_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpProjectSingle.Items.Remove(drpUserid.SelectedItem);
            drptimesheetdateSingle.Items.Remove(drpUserid.SelectedItem);
            drptimesheettimeInSingle.Items.Remove(drpUserid.SelectedItem);
            drptimesheettimeOutSingle.Items.Remove(drpUserid.SelectedItem);
            drptimesheetdateoutSingle.Items.Remove(drpUserid.SelectedItem);
            // drpTimeSheetDate.Items.Remove(drpUserid.SelectedItem);
        }



        void btnExit_Click(object sender, EventArgs e)
        {
            string sSQL = "";
            if (fileType.ToUpper() == "M")
            {
                sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_BC] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "'; ";
            }
            else if (fileType.ToUpper() == "S")
            {
                sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_TEMP] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "';";
            }
            int retVal = DataAccess.ExecuteStoreProc(sSQL);
            if (retVal >= 1)
            {

                string sSQL1 = "Update [TS_FileUploaded] set Status=0 WHERE [TranID] ='" + strtranid + "'";
                int retVal1 = DataAccess.ExecuteStoreProc(sSQL1);

                lblMsg.InnerHtml = "Doc No: " + strtranid + " deleted successfully";
                Session["TRANSID"] = null;
                Response.Redirect("TimeSheetDocument.aspx");
            }
            Response.Redirect("TimeSheetDocument.aspx");
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            string sSQL = "";
            if (fileType.ToUpper() == "M")
            {
                sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_BC] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "';";
            }
            else if (fileType.ToUpper() == "S")
            {
                sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_TEMP] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "';";
            }
            int retVal = DataAccess.ExecuteStoreProc(sSQL);
            if (retVal >= 1)
            {

                string sSQL1 = "Update [TS_FileUploaded] set Status=0 WHERE [TranID] ='" + strtranid + "'";
                int retVal1 = DataAccess.ExecuteStoreProc(sSQL1);

                lblMsg.InnerHtml = "Doc No: " + strtranid + " deleted successfully";
                Session["TRANSID"] = null;
                Response.Redirect("TimeSheetDocument.aspx");
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            if (fileType.ToUpper() == "M")
            {
                saveData("M");
            }
            else if (fileType.ToUpper() == "S")
            {
                saveData("S");

            }
            lblMsg.InnerHtml = "";
            //CmdImport.Enabled = false;
            //btnCancel.Enabled = false;
            //btnSave.Enabled = false;
        }

        protected void CmdImport_Click(object sender, EventArgs e)
        {
            if (fileType.ToUpper() == "M")
            {
                string strDrpCol1 = userid.SelectedItem.Text;
                string strDrpCol2 = timeentry.SelectedItem.Text;
                string strDrpCol3 = eventid.SelectedItem.Text;
                string strDrpCol4 = terminalsn.SelectedItem.Text;
                lblMsg.InnerHtml = "";

                if ((strDrpCol1 == strDrpCol2) || (strDrpCol1 == strDrpCol3) || (strDrpCol1 == strDrpCol4))
                {
                    lblMsg.InnerHtml = strDrpCol1 + " is selected more than once";
                }
                if ((strDrpCol2 == strDrpCol3) || (strDrpCol2 == strDrpCol4))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol2) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol2 + " is selected more than once";
                    }
                }
                if ((strDrpCol3 == strDrpCol2) || (strDrpCol3 == strDrpCol4))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol3) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol3 + " is selected more than once";
                    }
                }
            }
            else if (fileType.ToUpper() == "S")
            {

                lblMsg.InnerHtml = "";

                string strDrpCol1 = drpUserid.SelectedItem.Text;
                string strDrpCol2 = drpProjectSingle.SelectedItem.Text;
                string strDrpCol3 = drptimesheetdateSingle.SelectedItem.Text;
                string strDrpCol4 = drptimesheettimeInSingle.SelectedItem.Text;
                string strDrpCol5 = drptimesheettimeOutSingle.SelectedItem.Text;
                string strDrpCol6 = drptimesheetdateoutSingle.SelectedItem.Text;
                //string strDrpCol7 = drpTimeSheetDate.SelectedItem.Text;


                if ((strDrpCol1 == strDrpCol2) || (strDrpCol1 == strDrpCol3) || (strDrpCol1 == strDrpCol4) || (strDrpCol1 == strDrpCol5) || (strDrpCol1 == strDrpCol6))
                {
                    lblMsg.InnerHtml = strDrpCol1 + " is selected more than once";
                }
                if ((strDrpCol2 == strDrpCol3) || (strDrpCol2 == strDrpCol4) || (strDrpCol2 == strDrpCol5) || (strDrpCol2 == strDrpCol6))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol2) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol2 + " is selected more than once";
                    }
                }
                if ((strDrpCol3 == strDrpCol4) || (strDrpCol3 == strDrpCol5) || (strDrpCol3 == strDrpCol6))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol3) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol3 + " is selected more than once";
                    }
                }
                if ((strDrpCol4 == strDrpCol5) || (strDrpCol4 == strDrpCol6))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol4) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol4 + " is selected more than once";
                    }
                }
                if ((strDrpCol5 == strDrpCol6))
                {
                    if (lblMsg.InnerHtml.ToString().IndexOf(strDrpCol5) < 0)
                    {
                        lblMsg.InnerHtml = lblMsg.InnerHtml + "<br/>" + strDrpCol5 + " is selected more than once";
                    }
                }
            }

            if (lblMsg.InnerHtml.ToString().Length == 0)
            {
                string strMessage = "";
                string notvalidtime = "";

                FileStream fileStream = new FileStream(strFile, FileMode.Open);
                CSVReader reader = new CSVReader(fileStream);
                //get the header
                string[] headers = reader.GetCSVLine();
                DataTable dt = new DataTable();

                //add headers
                foreach (string strHeader in headers)
                {
                    dt.Columns.Add(strHeader.ToString().Trim());
                }

                string[] data;
                int introw = 0;

                dt.Columns.Add("Company_Id");
                dt.Columns.Add("TranId");

                if (fileType.ToUpper() == "M")
                {

                    while ((data = reader.GetCSVLine()) != null)
                    {
                        dt.Rows.Add(data);
                        dt.Rows[introw]["Company_Id"] = Convert.ToString(compid);
                        dt.Rows[introw]["TranId"] = strtranid.ToString().Trim();
                        introw = introw + 1;
                    }
                }
                else if (fileType.ToUpper() == "S")
                {
                    bool result1 = false;
                    bool result2 = false;
                    DateTime intime, outtime;
                    while ((data = reader.GetCSVLine()) != null)
                    {
                        if (data[3].Length == 3)
                        {
                            data[3] = "0" + data[3];
                        }

                        result1 = DateTime.TryParseExact(data[2] + data[3], "dd/MMM/yyyyHHmm", new CultureInfo("en-US"),
                                  DateTimeStyles.None, out intime);


                        if (data[5].Length == 3)
                        {

                            data[5] = "0" + data[5];
                        }

                        string d4 = data[4];
                        string d5 = data[5];


                        result2 = DateTime.TryParseExact(data[4] + data[5], "dd/MMM/yyyyHHmm", new CultureInfo("en-US"),
                                  DateTimeStyles.None, out outtime);

                        if (result1 && result2)
                        {
                            data[2] = intime.ToString("dd/MM/yyyy");
                            data[4] = outtime.ToString("dd/MM/yyyy");
                            dt.Rows.Add(data);
                            dt.Rows[introw]["Company_Id"] = Convert.ToString(compid);
                            dt.Rows[introw]["TranId"] = strtranid.ToString().Trim();
                            introw = introw + 1;
                        }
                        else
                        {
                            notvalidtime = notvalidtime + "<br/>" + data[0].ToString() + "," + data[1].ToString() + "," + data[2].ToString() + "," + " row  have not Valid Time";

                        }

                    }

                }
                fileStream.Dispose();

                errorLabel.InnerHtml = notvalidtime;
                try
                {

                    if (fileType.ToUpper() == "M")
                    {

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.CONNECTION_STRING))
                        {
                            bulkCopy.DestinationTableName = "[dbo].[ACTATEK_LOGS_BC]";
                            bulkCopy.ColumnMappings.Add(userid.SelectedItem.Text.ToString().Trim(), 1);
                            bulkCopy.ColumnMappings.Add(timeentry.SelectedItem.Text.ToString(), 2);
                            bulkCopy.ColumnMappings.Add(eventid.SelectedItem.Text.ToString().Trim(), 3);
                            bulkCopy.ColumnMappings.Add(terminalsn.SelectedItem.Text.ToString().Trim(), 4);
                            bulkCopy.ColumnMappings.Add("Company_Id", 6);
                            bulkCopy.ColumnMappings.Add("TranId", 7);

                            //bulkCopy.WriteToServer(dt);
                            //update and Redirect to prev page...

                            //if (retVal == 1)
                            //{
                            //    CmdImport.Enabled = false;
                            //    btnCancel.Enabled = true;
                            //    btnSave.Enabled = true;
                            //}
                        }
                        string mysql = "";
                        string mytime = "";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mytime = dt.Rows[i][1].ToString();
                            //convert(varchar,timeentry,103)+' '+CONVERT(VARCHAR,[timeentry],108)
                            mytime = "convert(varchar," + mytime + ",103)+' '+CONVERT(VARCHAR," + mytime + ",108)";
                            mysql = "Insert into ACTATEK_LOGS_BC (userid, timeentry, eventID, terminalSN, company_id, TranID) values " +
                                "( '" + dt.Rows[i][0].ToString() + "', '" + dt.Rows[i][1].ToString() + "',  '" + dt.Rows[i][2].ToString() + "', '" + dt.Rows[i][3].ToString() + "', " + int.Parse(dt.Rows[i][4].ToString()) + ", '" + dt.Rows[i][5].ToString() + "'   )";
                            DataAccess.FetchRS(CommandType.Text, mysql, null);
                        }

                        lblMsg.InnerHtml = lblDocNo.Text + " Imported Sucessfully";

                        //Show Number Of Rows In Grid Which are not valid
                        string sSQL1 = "";
                        sSQL1 = "SELECT * FROM ACTATEK_LOGS_BC  WHERE   LEN(timeentry)<=0  OR LEN(tranid)<=0   OR convert(varchar, timeentry, 100) IS  NULL";
                        DataSet ds1 = new DataSet();
                        ds1 = DataAccess.FetchRS(CommandType.Text, sSQL1, null);


                        //Check if Records are approved or not or records are there in Database ...
                        string sqlDataExsist = "Select A.*  from ACTATEK_LOGS_BC  A ,ACTATEK_LOGS_PROXY B Where A.userID =B.userID and CONVERT(VARCHAR(10),A.timeentry,111) = CONVERT(VARCHAR(10),B.timeentry,111) ";
                        DataSet dsExist = new DataSet();
                        dsExist = DataAccess.FetchRS(CommandType.Text, sqlDataExsist, null);

                        ds1.Merge(dsExist, true, MissingSchemaAction.AddWithKey);

                        RadGrid1.DataSource = ds1;
                        RadGrid1.DataBind();

                        ////Check if Records are approved or not or records are there in Database ...
                        //string sqlDataApp = "Select count(*)  from ACTATEK_LOGS_BC  A ,ACTATEK_LOGS_PROXY B Where A.userID =B.userID and CONVERT(VARCHAR(10),A.timeentry,111) = CONVERT(VARCHAR(10),B.timeentry,111) ";
                        //DataSet dsApproved = new DataSet();
                        //dsApproved = DataAccess.FetchRS(CommandType.Text, sqlDataApp, null);


                        if (ds1.Tables[0].Rows.Count > 0 || dsExist.Tables[0].Rows.Count > 0)
                        {
                            ButtonDelete.Enabled = true;
                            RadGrid1.Visible = true;
                            string sql1 = "SELECT count(*) FROM ACTATEK_LOGS_BC  WHERE  Len(eventID)>0 AND LEN(timeentry)>0  AND LEN(tranid)>0  AND convert(varchar, timeentry, 100) IS NOT NULL";
                            int cnt = DataAccess.ExecuteScalar(sql1);


                            int invalidROws = ds1.Tables[0].Rows.Count;
                            //int InvalidRows = cnt - ds1.Tables[0].Rows.Count;
                            if (dsExist.Tables[0].Rows.Count == 0)
                            {
                                strMessage = "Number of Valid Rows " + cnt.ToString();
                            }
                            strMessage = strMessage + " <br/> Number of Invalid Rows " + invalidROws.ToString() + "  <br/> Invalid Rows Displayed Below.";
                            strMessage = strMessage + "<br/> **************** Action ****************<br/> ";
                            strMessage = strMessage + "To Import : Click On continue Import Leaving Invalid Rows <br/>";
                            strMessage = strMessage + "This Will not import invalid rows displayed below <br/> To Cancel :";
                            strMessage = strMessage + "Click on Cancel Import this will delete the import data,User can fix the rows and re import data";

                            //lblMsg.InnerHtml = "Number of Valid Rows " + cnt.ToString() + "  <br/> Below are invalid rows. <br/> Please Click on Save Button to insert Valid data.<br/> Press Cancel to Upload File Again";
                            lblMsg.InnerHtml = strMessage;

                            CmdImport.Enabled = false;
                            btnCancel.Enabled = true;
                            btnSave.Enabled = true;
                            if (dsExist.Tables[0].Rows.Count > 0)
                            {
                                btnSave.Enabled = false;
                                lblMsg.InnerHtml = "<br/> You Can Not Import Data Again.<br/> Please Delete Existing Data From Time Sheet And Then Import Again.";
                            }
                        }
                        else
                        {
                            saveData("M");
                            //lblMsg.InnerHtml = "Data Uploaded Successfully";
                        }
                    }
                    else if (fileType.ToUpper() == "S")
                    {
                        try
                        {
                            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Constants.CONNECTION_STRING))
                            {
                                bulkCopy.DestinationTableName = "ACTATEK_LOGS_TEMP";
                                bulkCopy.ColumnMappings.Add(drpUserid.SelectedItem.Text.ToString(), 1);
                                bulkCopy.ColumnMappings.Add(drpProjectSingle.SelectedItem.Text.ToString().Trim(), 4);
                                bulkCopy.ColumnMappings.Add(drptimesheetdateSingle.SelectedItem.Text.ToString().Trim(), 16);
                                bulkCopy.ColumnMappings.Add(drptimesheettimeInSingle.SelectedItem.Text.ToString().Trim(), 14);
                                bulkCopy.ColumnMappings.Add(drptimesheetdateoutSingle.SelectedItem.Text.ToString().Trim(), 17);
                                bulkCopy.ColumnMappings.Add(drptimesheettimeOutSingle.SelectedItem.Text.ToString().Trim(), 19);
                                bulkCopy.ColumnMappings.Add("Company_Id", 6);
                                bulkCopy.ColumnMappings.Add("TranId", 7);
                                //bulkCopy.ColumnMappings.Add(drpTimeSheetDate.SelectedItem.Text.ToString().Trim(), 8);
                                bulkCopy.WriteToServer(dt);
                                //update and Redirect to prev page...
                                lblMsg.InnerHtml = lblDocNo.Text + " Imported Sucessfully";
                                //if (retVal == 1)
                                //{
                                //    CmdImport.Enabled = false;
                                //    btnCancel.Enabled = true;
                                //    btnSave.Enabled = true;
                                //}
                            }
                            //Save Data in Data base for 
                            //Upload File And sp_BulkInsert_TimeSheet and show Summary : Number of Rows Valid
                            // and number of rows which are not valid and ask user if they want to upload remaining
                            // rows or not.....


                            int i = 0;
                            SqlParameter[] parms = new SqlParameter[1];
                            parms[i++] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));

                            string sSQL = "sp_BulkInsert_TimeSheet";
                            try
                            {
                                DataSet ds = new DataSet();
                                ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
                                RadGrid2.DataSource = ds;
                                RadGrid2.DataBind();
                                RadGrid2.Rebind();

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    RadGrid2.Visible = true;
                                    //Show Data in 
                                    string sql = "SELECT Count(*) FROM ACTATEK_LOGS_TEMP WHERE LEN(TimeIn)=8 and LEN([TimeOut])=8";

                                    int cnt = DataAccess.ExecuteScalar(sql);


                                    //int InvalidRows = cnt - ds1.Tables[0].Rows.Count;
                                    strMessage = "Number of Valid Rows " + cnt.ToString();
                                    strMessage = strMessage + " <br/> Number of Invalid Rows " + ds.Tables[0].Rows.Count.ToString() + "  <br/> Invalid Rows Displayed Below.";
                                    strMessage = strMessage + "<br/> ******************* Action *************<br/> ";
                                    strMessage = strMessage + "To Import : Click On continue Import Leaving Invalid Rows <br/>";
                                    strMessage = strMessage + "This Will not import invalid rows displayed below <br/> To Cancel :";
                                    strMessage = strMessage + "Click on Cancel Import this will delete the import data,User can fix the rows and re import data";

                                    //lblMsg.InnerHtml = "Number of Valid Rows " + cnt.ToString() + "  <br/> Below are invalid rows. <br/> Please Click on Save Button to insert Valid data.<br/> Press Cancel to Upload File Again";
                                    lblMsg.InnerHtml = strMessage;
                                    //CmdImport.Enabled = false;
                                    //btnCancel.Enabled = true;
                                    //btnSave.Enabled = true;

                                }
                                else
                                {
                                    //All Rows Are Valid Now Check which rows are already in the database 
                                    //and need to show as an error

                                    //Check if Records are approved or not or records are there in Database ...
                                    //string sqlDataExsist = "Select A.*  from ACTATEK_LOGS_TEMP  A ,ACTATEK_LOGS_PROXY B Where A.userID =B.userID and CONVERT(VARCHAR(10),A.timeentry,111) = CONVERT(VARCHAR(10),B.timeentry,111) ";

                                    string sqlDataExsist = "Select A.userID WorkerID ,A.terminalSN ProjectCode,A.DateIn ,A.TimeIn,A.TimeOut ,A.DateIn timesheetdate from ACTATEK_LOGS_TEMP  A INNER JOIN ACTATEK_LOGS_PROXY B ON  A.userID =B.userID ";
                                    sqlDataExsist = sqlDataExsist + " and ( Convert(Datetime,CONVERT(VARCHAR(10),A.DateIn,111),103) = Convert(Datetime,CONVERT(VARCHAR(10),B.timeentry,111),103))  ";

                                    DataSet dsExist = new DataSet();
                                    dsExist = DataAccess.FetchRS(CommandType.Text, sqlDataExsist, null);

                                    //ds1.Merge(dsExist, true, MissingSchemaAction.AddWithKey);
                                    RadGrid2.DataSource = dsExist;
                                    RadGrid2.DataBind();

                                    if (dsExist.Tables[0].Rows.Count > 0)
                                    {
                                        CmdImport.BackColor = Color.Orange;
                                        ButtonDelete.Enabled = true;
                                        RadGrid2.Visible = true;
                                        btnSave.Enabled = false;
                                        CmdImport.Enabled = false;
                                        lblMsg.InnerHtml = "<br/> You Can Not Import Data Again.<br/> Please Delete Existing Data From Time Sheet And Then Import Again.";
                                    }
                                    else
                                    {
                                        ButtonDelete.Enabled = false;
                                        saveData("S");
                                        lblMsg.InnerHtml = "Data Uploaded Successfully";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMsg.InnerHtml = "Error In Uploading File Please Try again";
                                btnSave.Enabled = false;
                                CmdImport.Enabled = false;
                                ButtonDelete.Enabled = false;
                                sSQL = "";
                                if (fileType.ToUpper() == "M")
                                {
                                    sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_BC] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "'; ";
                                }
                                else if (fileType.ToUpper() == "S")
                                {
                                    sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS] where Company_Id='" + compid + "' And tranid='" + Session["TRANSID"].ToString() + "';DELETE FROM [ACTATEK_LOGS_TEMP] WHERE [tranID] ='" + Session["TRANSID"].ToString() + "';";
                                }
                                int retVal = DataAccess.ExecuteStoreProc(sSQL);
                                if (retVal >= 1)
                                {

                                    string sSQL1 = "Update [TS_FileUploaded] set Status=0 WHERE [TranID] ='" + strtranid + "'";
                                    int retVal1 = DataAccess.ExecuteStoreProc(sSQL1);

                                    //lblMsg.InnerHtml = "Doc No: " + strtranid + " deleted successfully";
                                    lblMsg.InnerHtml = "Error In Uploading File Please Try again";
                                    Session["TRANSID"] = null;
                                    //Response.Redirect("TimeSheetDocument.aspx");
                                }
                                //Response.Redirect("TimeSheetDocument.aspx");
                                //Delete data from ACTATEK_LOGS_TEMP OR ACTATEK_LOGS_BC ...
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMsg.InnerHtml = ex.Message;

                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.InnerHtml = ex.Message;
                    string sSQL = "Update [TS_FileUploaded] set Status=100, ErrorMsg ='" + ex.Message.Replace("'", "") + " " + ex.Source + "' WHERE [TranID] ='" + strtranid + "'";
                    int retVal = DataAccess.ExecuteStoreProc(sSQL);
                    if (retVal == 1)
                    {
                        Session["ProcessTranId"] = null;
                        Response.Redirect("TimeSheetDocument.aspx");
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void saveData(string FileType)
        {

            if (FileType == "M")
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[1];
                //parms[i++] = new SqlParameter("@Company_ID", compid);
                parms[i++] = new SqlParameter("@transid", Session["TRANSID"].ToString());

                string sSQL = "sp_Save_Data_ACTATEK_LOGS";
                try
                {
                    int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);

                    string sql1 = "SELECT Count(*) FROM ACTATEK_LOGS WHERE tranid = '" + Session["TRANSID"].ToString() + "'";
                    int cnt = DataAccess.ExecuteScalar(sql1);

                    //btnCancel.Enabled = false;
                    //btnSave.Enabled = false;
                    //CmdImport.Enabled = false;
                    rowMulti.Visible = false;
                    rowSingle.Visible = false;

                    lblMsg1.InnerHtml = " </br> " + cnt.ToString() + "Rows Imported Successfully";
                    RadGrid1.Visible = false;
                    RadGrid2.Visible = false;
                    string sSQL1 = "Update [TS_FileUploaded] set Status=1 WHERE [TranID] ='" + strtranid + "'";
                    int retVal1 = DataAccess.ExecuteStoreProc(sSQL1);
                }
                catch (Exception ex)
                {

                    lblMsg.InnerHtml = ex.Message;
                }
            }
            else if (FileType == "S")
            {
                int i = 0;
                SqlParameter[] parms = new SqlParameter[1];
                parms[i++] = new SqlParameter("@flag", "1");
                //string sql1 = "SELECT Count(*) FROM ACTATEK_LOGS_TEMP WHERE LEN(TimeIn)=8 AND LEN([TimeOut])=8";
                //int cnt = DataAccess.ExecuteScalar(sql1);

                string sql = "sp_BulkInsert_ValidTimeSheet_Data";
                try
                {
                    //int cnt = DataAccess.ExecuteScalar(sql);
                    DataSet ds = new DataSet();
                    ds = DataAccess.ExecuteSPDataSet(sql, parms);
                    //lblMsg.InnerHtml = "Data Uploaded Successfully </br> ( " + cnt.ToString() + ") Rows";
                    //string sql1 = "SELECT Count(*) FROM ACTATEK_LOGS_TEMP WHERE tranid = '" + Session["TRANSID"].ToString() + "'";
                    string strcnt = "";
                    if (ds != null)
                    {
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                strcnt = ds.Tables[0].Rows[0][0].ToString();
                            }
                        }
                    }
                    // int cnt = DataAccess.ExecuteScalar(sql1);
                    lblMsg1.InnerHtml = " </br> " + strcnt + "... Rows Imported Successfully";

                    rowMulti.Visible = false;
                    rowSingle.Visible = false;
                    RadGrid1.Visible = false;
                    RadGrid2.Visible = false;
                    btnCancel.Enabled = false;
                    btnSave.Enabled = false;
                    CmdImport.Enabled = false;

                    string sSQL1 = "Update [TS_FileUploaded] set Status=1 WHERE [TranID] ='" + strtranid + "'";
                    int retVal1 = DataAccess.ExecuteStoreProc(sSQL1);
                }
                catch (Exception ex)
                {
                    lblMsg.InnerHtml = ex.Message;
                }
            }

            lblMsg.InnerHtml = "";
        }

        protected void Button1_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("TimeSheetDocument.aspx");
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {


            string sql = @"delete B from ACTATEK_LOGS_TEMP  A INNER JOIN ACTATEK_LOGS_PROXY B ON  A.userID =B.userID  
            and ( Convert(Datetime,CONVERT(VARCHAR(10),A.DateIn,111),103) = Convert(Datetime,CONVERT(VARCHAR(10),B.timeentry,111),103))";

            int retVal1 = DataAccess.ExecuteStoreProc(sql);

            int retVal2 = DataAccess.ExecuteStoreProc("delete  from ACTATEK_LOGS_TEMP");


            lblMsg.InnerHtml = string.Format(" {0} Record Deleted Sueccessfuly", retVal1.ToString());

            int i = 0;
            SqlParameter[] parms = new SqlParameter[1];
            parms[i++] = new SqlParameter("@compid", Utility.ToInteger(Session["Compid"]));

            string sSQL = "sp_BulkInsert_TimeSheet";

            DataSet ds = new DataSet();
            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);
            RadGrid2.DataSource = ds;
            RadGrid2.DataBind();
            RadGrid2.Rebind();
            this.CmdImport.Enabled = true;
            this.CmdImport.BackColor = Color.Brown;
        }













    }
}
