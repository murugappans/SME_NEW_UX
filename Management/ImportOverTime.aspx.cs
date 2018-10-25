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
using System.Data.OleDb;

namespace SMEPayroll.Management
{
    public partial class ImportOverTime : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        int varCompid;
        protected string sMsg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            /* To disable Grid filtering options  */

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            varCompid = Utility.ToInteger(Session["Compid"]);
            sMsg = Utility.ToString(Request.QueryString["msg"]);

            if (!IsPostBack)
            {
                Session["s"] = 0;
                lblErr.Text = "";

            }

        }
        protected DataSet ReadExcelFile(string fileName)
        {
            try
            {
                OleDbDataAdapter connection = new OleDbDataAdapter("SELECT * FROM [Overtime$]", @"Provider=Microsoft.Jet.OleDb.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;");
                DataSet ExcelData = new DataSet();
                connection.Fill(ExcelData, "ExcelData");

                return ExcelData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnExport_click(object sender, EventArgs e)
        {

            if (fileUpload1.HasFile)
            {
                lblErr.Text = "";
                string fileName = fileUpload1.PostedFile.FileName;
                string[] fileExt = fileName.Split('.');
                if (fileExt[1].ToString() == "XLS" || fileExt[1].ToString() == "xls")
                {
                    try
                    {
                        DataSet ExcelData = ReadExcelFile(fileName);
                        int recordsUpdated = 0;
                        int errRecs = 0;
                        for (int i = 0; i < ExcelData.Tables["ExcelData"].Rows.Count; i++)
                        {
                            try
                            {
                                string errMsg = "";
                                
                                int emp_code, overtime1, trx_date, status, overtime2, trx_month, trx_year, days_work, v1, v2, v3, v4, NH_Work;
                                emp_code = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][0].ToString());
                                overtime1 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][1].ToString());
                                // trx_date = ExcelData.Tables["ExcelData"].Rows[i][0].ToString();
                                // status = ExcelData.Tables["ExcelData"].Rows[i][0].ToString();
                                overtime2 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][2].ToString());
                                //  trx_month = ExcelData.Tables["ExcelData"].Rows[i][0].ToString();
                                // trx_year = ExcelData.Tables["ExcelData"].Rows[i][0].ToString();
                                days_work = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][3].ToString());
                                v1 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][4].ToString());
                                v2 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][5].ToString());
                                v3 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][6].ToString());
                                v4 = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][7].ToString());
                                NH_Work = Utility.ToInteger(ExcelData.Tables["ExcelData"].Rows[i][8].ToString());
                                int j = 0;
                                SqlParameter[] parms = new SqlParameter[11];
                                parms[j++] = new SqlParameter("@emp_code", Utility.ToInteger(emp_code));
                                parms[j++] = new SqlParameter("@overtime1", Utility.ToInteger(overtime1));
                                parms[j++] = new SqlParameter("@overtime2", Utility.ToInteger(overtime2));
                                parms[j++] = new SqlParameter("@trx_month", Utility.ToInteger(System.DateTime.Today.Month.ToString()));
                                parms[j++] = new SqlParameter("@trx_year", Utility.ToInteger(System.DateTime.Today.Year.ToString()));
                                parms[j++] = new SqlParameter("@days_work", Utility.ToInteger(days_work));
                                parms[j++] = new SqlParameter("@v1", Utility.ToInteger(v1));
                                parms[j++] = new SqlParameter("@v2", Utility.ToInteger(v2));
                                parms[j++] = new SqlParameter("@v3", Utility.ToInteger(v3));
                                parms[j++] = new SqlParameter("@v4", Utility.ToInteger(v4));
                                parms[j++] = new SqlParameter("@NH_Work", Utility.ToString(NH_Work));
                                string sqlStr = "update [emp_overtime] set [overtime1]=@overtime1,[overtime2]=@overtime2,[trx_month]=@trx_month,[trx_year]=@trx_year,[days_work]=@days_work,[v1]=@v1,[v2]=@v2,[v3]=@v3,[v4]=@v4,[NH_Work]=@NH_Work where [emp_code]=@emp_code";
                                int result = 0;
                                if (emp_code != 0)
                                {
                                    result = DataAccess.ExecuteNonQuery(sqlStr, parms);
                                }else
                                {
                                    errRecs = i + 2;
                                    errMsg = errMsg + "," + errRecs;
                                }
                                if (result > 0)
                                {
                                    recordsUpdated += 1;
                                    lblErr.Text = recordsUpdated + " Records Updated ";
                                    if (errRecs > 0)
                                    {
                                        errMsg = errMsg.Remove(0, 1);
                                        lblErr.Text = lblErr.Text + " And Invalid Record at Line No :" + errMsg;
                                    }
                                }
                                else 
                                {
                                    if (errRecs > 0)
                                    {
                                        errMsg = errMsg.Remove(0, 1);
                                        lblErr.Text =  " Invalid Record at Line No :" + errMsg;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblErr.Text = ex.Message.ToString();
                            }



                        }
                    }
                    catch (OleDbException Oleex)
                    {
                        lblErr.Text = "Invalid File Selection,Sheet Name Should be Overtime";
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


    }
}
