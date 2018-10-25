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
using System.IO;
using System.Data.OleDb;
using Telerik.Web.UI;

namespace SMEPayroll.TimeSheet
{
    public partial class PayrollAdditionsAuto : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {
            string sSQL;
            compid = Utility.ToInteger(Session["Compid"]);

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            if (!IsPostBack)
            {
                drpaddtype.Attributes.Add("OnChange", "EnablePayableOtions();");
                drpiras_approval.Attributes.Add("OnChange", "EnableApproval();");
                CmdUpload.Attributes.Add("onclick", "javascript:return ValidateForm();");
                tr1.Attributes.Add("style", "display:none");
                tr2.Attributes.Add("style", "display:none");
                tr3.Attributes.Add("style", "display:none");
                tr4.Attributes.Add("style", "display:none");


                cmbYear.SelectedValue = System.DateTime.Now.Year.ToString();

                DataSet ds_additiontype = new DataSet();
                if (compid == 1)
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=-1 or company_id=" + compid + " And (OptionSelection='General' Or OptionSelection='Claim')";
                else
                    sSQL = "SELECT [id], [desc], tax_payable_options, company_id FROM [additions_types] where company_id=" + compid + " And (OptionSelection='General' Or OptionSelection='Claim')";
                ds_additiontype = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                drpaddtype.DataSource = ds_additiontype.Tables[0];
                drpaddtype.DataTextField = ds_additiontype.Tables[0].Columns["desc"].ColumnName.ToString();
                drpaddtype.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
                drpaddtype.DataBind();

                drplumsum.DataSource = ds_additiontype.Tables[0];
                drplumsum.DataTextField = ds_additiontype.Tables[0].Columns["tax_payable_options"].ColumnName.ToString();
                drplumsum.DataValueField = ds_additiontype.Tables[0].Columns["id"].ColumnName.ToString();
                drplumsum.DataBind();

            }
        }



        protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
        }

        private DataSet dstab
        {
            get
            {
                DataSet ds = new DataSet();
                int intMonth = Utility.ToInteger(RadDatePicker1.SelectedDate.Value.Month);
                int intYear = Utility.ToInteger(cmbYear.SelectedValue.ToString());
                string sSQL = "select TranID,FileName,status,OriFileName from TS_FileUploaded where Company_Id=" + compid + " And Month = " + intMonth + " And Year=" + intYear + " And TypeNum=1 ORDER BY CreatedDate ";
                ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                return ds;
            }
        }

        private void DocUploaded()
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocUploaded();
        }

        protected void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DocUploaded();
        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                GridItem dataItem = (GridItem)e.Item;
                string status = dataItem.Cells[2].Text.ToString();
                LinkButton lnk = ((LinkButton)dataItem.Cells[6].Controls[0]);
                if (status == "0")
                {
                    lnk.Text = "Uploaded";
                }
                else if (status == "1")
                {
                    lnk.Text = "Timesheet";
                }
                else if (status == "100")
                {
                    lnk.Text = "Error";
                }
            }
        }

        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            GridItem dataItem = (GridItem)e.Item;
            if (e.CommandName == "Delete")
            {
                string strtranid = dataItem.Cells[3].Text.ToString();
                string sSQL = "DELETE FROM [TS_FileUploaded] WHERE [TranID] ='" + strtranid;
                int retVal = DataAccess.ExecuteStoreProc(sSQL);
                if (retVal == 1)
                {
                    DocUploaded();
                }

            }
            if (e.CommandName == "Process")
            {
                string strtranid = dataItem.Cells[3].Text.ToString();
                Session["ProcessTranId"]= strtranid;
                LinkButton lnk = ((LinkButton)dataItem.Cells[6].Controls[0]);

                if (lnk.Text.ToString() == "Uploaded")
                {
                    string strPath = Server.MapPath(@"..\\Documents\\UploadAddDed\" + dataItem.Cells[4].Text.ToString());
                    //Response.Redirect("MAPTimesheet.aspx");
                }
                if (lnk.Text.ToString() == "Timesheet")
                {
                    //Response.Redirect("TimesheetEditUpdate.aspx");
                }
            }
        }

        protected void drpaddtype_databound(object sender, EventArgs e)
        {
            drpaddtype.Items.Insert(0, new ListItem("-select-", "-select-"));
        }

        protected void drpaddtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sSQL = "select [id],cpf from additions_types where id=" + Utility.ToInteger(drpaddtype.SelectedItem.Value);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            if (dr.Read())
            {
                lblcpf.Text = dr[1].ToString();
            }
        }
        protected void CmdUpload_Click(object sender, EventArgs e)
        {
            string strMsg = "";
            if (FileUpload.PostedFile != null) //Checking for valid file
            {
                // Since the PostedFile.FileNameFileName gives the entire path we use Substring function to rip of the filename alone.
                string StrFileName    = FileUpload.PostedFile.FileName.Substring(FileUpload.PostedFile.FileName.LastIndexOf("\\") + 1);
                string strorifilename = StrFileName;
                string StrFileType    = FileUpload.PostedFile.ContentType;
                int    IntFileSize    = FileUpload.PostedFile.ContentLength;
                //Checking for the length of the file. If length is 0 then file is not uploaded.
                if (IntFileSize <= 0)
                    strMsg = "Please Select File to be uploaded";
                else
                {


                    int RandomNumber = 0;
                    RandomNumber = Utility.GetRandomNumberInRange(10000, 1000000);

                    string strMonth = RadDatePicker1.SelectedDate.Value.Month.ToString();
                    string strYear = cmbYear.SelectedItem.Value.ToString();
                    string strTranID = strMonth + strYear + Convert.ToString(RandomNumber);
                    string[] FileExt = StrFileName.Split('.');
                    string strExtent = "." + FileExt[FileExt.Length - 1];

                    StrFileName = strMonth + "_" + strYear + "_" + strTranID;

                    int i = 0;
                    SqlParameter[] parms = new SqlParameter[7];
                    parms[i++] = new SqlParameter("@Company_ID", compid);
                    parms[i++] = new SqlParameter("@Month", Utility.ToInteger(strMonth));
                    parms[i++] = new SqlParameter("@Year", Utility.ToInteger(strYear));
                    parms[i++] = new SqlParameter("@TranID", Utility.ToString(strTranID));
                    parms[i++] = new SqlParameter("@FileName", Utility.ToString(StrFileName + strExtent));
                    parms[i++] = new SqlParameter("@OriFileName", Utility.ToString(strorifilename));
                    parms[i++] = new SqlParameter("@TypeNum", 1);


                    string sSQL = "sp_Add_TS_FileUpload";
                    try
                    {
                        FileUpload.PostedFile.SaveAs(Server.MapPath(@"..\\Documents\\UploadAddDed\" + StrFileName + strExtent));
                        int retVal = DataAccess.ExecuteStoreProc(sSQL, parms);
                        if (retVal == 1)
                        {
                            strMsg = "File Uploaded Succesfully, Doc No :" + strTranID;

                            string file = Server.MapPath(@"..\\Documents\\UploadAddDed\" + StrFileName + strExtent);
                            string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";Extended Properties=Excel 8.0;";
                            string query = "Select * from [" + StrFileName  + "]";
                            DataSet dataSet = new DataSet();
                            using (OleDbConnection Connection = new OleDbConnection(constr))
                            {
                                using (OleDbDataAdapter DataAdapter = new OleDbDataAdapter(query, Connection))
                                {
                                    DataAdapter.Fill(dataSet, "DataSetName");
                                    DataAdapter.AcceptChangesDuringFill = false;
                                }
                            }
                           
                            DocUploaded();
                        }
                    }
                    catch (Exception ex)
                    {
                        strMsg = ex.Message;
                    }

                }
            }
        }

    }
}
