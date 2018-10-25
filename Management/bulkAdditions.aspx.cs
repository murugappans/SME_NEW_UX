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

namespace SMEPayroll.Management
{
    public partial class bulkAdditions : System.Web.UI.Page
    {
        protected string strMessage = "";
        protected string strError = "";
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
            if (FileUpload.PostedFile != null)
            {
                //lblErr.Text = "";
                string fileName = FileUpload.PostedFile.FileName;
                if (fileName.Length > 0)
                {
                    string[] fileExt = fileName.Split('.');
                    string errMsg = "";
                    if (fileExt[1].ToString() == "XLS" || fileExt[1].ToString() == "xls")
                    {
                        try
                        {
                            DataSet ExcelData = Utility.OpenExcelFile(false, fileName);
                            int recordsUpdated = 0;
                            int j = 0;
                            for (int i = 0; i < ExcelData.Tables[0].Rows.Count; i++)
                            {
                                try
                                {
                                    double trx_amount = Utility.ToDouble(ExcelData.Tables[0].Rows[i][1].ToString());
                                    string timecardid = Utility.ToString(ExcelData.Tables[0].Rows[i][0].ToString());

                                    if (trx_amount.ToString().Trim().Length > 0)
                                    {
                                        int emp_code = 0;
                                        string sSQL = "select [emp_code] From EMployee where (time_card_no='" + Utility.ToString(timecardid) + "' Or Emp_Code='" + Utility.ToString(timecardid) + "' OR ic_pp_number='" + Utility.ToString(timecardid) + "')";
                                        SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                                        if (dr.Read())
                                        {
                                            emp_code = Utility.ToInteger(dr[0].ToString());
                                        }

                                        string sqlStr = "";
                                        if (txtiras_approval_date.Text.ToString().Length > 0)
                                        {
                                            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                                            DateTime dt = new DateTime();
                                            dt = Convert.ToDateTime(txtiras_approval_date.Text, format);
                                            sqlStr = "INSERT INTO [dbo].[emp_additions]( [trx_type],[trx_amount],[trx_period],[created_on],[emp_code],[status],[ClaimStatus],[AdditionsForYear],[iras_approval],[service_length],[basis_arriving_payment],[iras_approval_date]) VALUES ( " + drpaddtype.SelectedValue + "," + trx_amount + " ,'" + RadDatePicker1.SelectedDate.Value.ToString("MM/dd/yyyy") + "',getdate(), " + emp_code + ",'U','Approved','" + cmbYear.SelectedValue + "','" + drpiras_approval.SelectedValue.ToString() + "'," + Utility.ToInteger(txtservice_length.Text.ToString()) + ",'" + txtbasis_arriving_payment.Text.ToString().Trim() + "','" + dt.ToShortDateString() + "')";
                                        }
                                        else
                                        {
                                            sqlStr = "INSERT INTO [dbo].[emp_additions]( [trx_type],[trx_amount],[trx_period],[created_on],[emp_code],[status],[ClaimStatus],[AdditionsForYear],[iras_approval],[service_length],[basis_arriving_payment]) VALUES ( " + drpaddtype.SelectedValue + "," + trx_amount + " ,'" + RadDatePicker1.SelectedDate.Value.ToString("MM/dd/yyyy") + "',getdate(), " + emp_code + ",'U','Approved','" + cmbYear.SelectedValue + "','" + drpiras_approval.SelectedValue.ToString() + "'," + Utility.ToInteger(txtservice_length.Text.ToString()) + ",'" + txtbasis_arriving_payment.Text.ToString().Trim() + "')";
                                        }
                                        int status = DataAccess.ExecuteNonQuery(sqlStr, null);
                                        if (status > 0)
                                            recordsUpdated += 1;
                                    }
                                    else
                                    {
                                        strError = strError + " <br/> " + "ID : " + ExcelData.Tables[0].Rows[i][0].ToString() + " at Row " + Convert.ToString(i + 2) + " with Amount " + ExcelData.Tables[0].Rows[i][1].ToString();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    strError = strError + " <br/> " + "ID : " + ExcelData.Tables[0].Rows[i][0].ToString() + " at Row " + Convert.ToString(i + 2) + " with Amount " + ExcelData.Tables[0].Rows[i][1].ToString();
                                }
                            }
                            if (recordsUpdated > 0)
                            {
                                lblErr.Text = recordsUpdated + " Records Updated " + " <br/> ";
                                if (strError.Length > 0)
                                {
                                    strError = " Following Record Cannot Be Updated : " + strError;
                                    lblErr.Text = lblErr.Text + " <br/> " + strError;
                                    strError = "";
                                }
                            }
                            else
                            {
                                lblErr.Text = " No Records Updated " + " <br/> ";
                            }
                        }
                        catch (OleDbException Oleex)
                        {

                            lblErr.Text = "Invalid File Selection,Employee Additions ";
                            lblErr.Text = lblErr.Text + Oleex.Message.ToString();
                        }
                        catch (Exception ex)
                        {
                            lblErr.Text = ex.Message.ToString();
                        }
                    }
                    else
                    {
                        lblErr.Text = "Invalid File Selection,Please select an excel file to upload";
                    }
                }
                else
                {
                    lblErr.Text = "Please select an excel file to upload";
                }
            }
            else
            {
                lblErr.Text = "Please select an excel file to upload";
            }

            strMessage = lblErr.Text.ToString().Trim();
        }
        //protected DataSet ReadExcelFile(string fileName)
        //{
        //    OleDbDataAdapter connection = new OleDbDataAdapter("SELECT * FROM [EmployeeAdditions$]", @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + FileUpload.PostedFile.FileName + ";Extended Properties=Excel 8.0;");
        //    DataSet ExcelData = new DataSet();
        //    connection.Fill(ExcelData, 0);
        //    return ExcelData;
        //}

        void Page_PreRender(Object sender, EventArgs e)
        {
            if (strMessage.Length > 0)
            {
                //string str = strMessage.Replace("<br/>", "\r\n");
                //Response.Write("<SCRIPT>alert('" + @str  + "');</SCRIPT>");
                strMessage = "";
            }
        }
    }
}
