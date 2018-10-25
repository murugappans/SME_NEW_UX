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

namespace SMEPayroll.Management
{
    public partial class SalaryChange : System.Web.UI.Page
    {
        public int compid;

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            ViewState["actionMessage"] = "";
        }

        protected void RadGrid1_PageIndexChanged(object source, Telerik.Web.UI.GridPageChangedEventArgs e)
        {     
            RadGrid1.CurrentPageIndex = e.NewPageIndex;
            FillGrid();
        }
        decimal NewAmount;
        protected void imgbtnView_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void FillGrid()
        {
            DataSet ds = new DataSet();
            string sSQL = "Sp_UpdateSalary";

            if (drpList.SelectedValue == "Manual")
            {
                NewAmount = 0;
            }
            else
            {
                NewAmount = Convert.ToDecimal(txtAmount.Text);
            }

            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@company_id", compid);
            parms[1] = new SqlParameter("@Formula", drpList.SelectedValue);
            parms[2] = new SqlParameter("@Newpay", NewAmount);
            ds = DataAccess.ExecuteSPDataSet(sSQL, parms);


            RadGrid1.DataSource = ds;
            RadGrid1.DataBind();
        }
        protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (drpList.SelectedValue != "Manual")
            {
                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;
                    TextBox txtbox = (TextBox)item.FindControl("txtNewBasicpay");//accessing textbox
                    txtbox.ReadOnly = true;
                    
                }

            }
        }

        string Hourly_rate_mode, Daily_Rate_Mode;
        double Hourly_Rate, Daily_Rate, OT1Rate, OT2Rate, WDays_Per_Week;
        string empcode, OT_Entitlement, CPF_Entitlement, Pay_Frequency, Fromdate, Fromdate1;
        int DepartmentID, DesignationID;
        protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.Item is GridCommandItem)
            {
                GridCommandItem commandItem = (GridCommandItem)e.Item;
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
                               empcode = Utility.ToString(RadGrid1.MasterTableView.Items[dataItem.ItemIndex].GetDataKeyValue("emp_code"));
                               TextBox monthlyPaytxt = (TextBox)dataItem.FindControl("txtNewBasicpay");

                               string sqlup = @"select Hourly_rate_mode,Hourly_Rate,Daily_Rate_Mode,Daily_Rate,OT1Rate,OT2Rate,DepartmentID,DesignationID,OT_Entitlement,CPF_Entitlement,Pay_Frequency,  
                                              WDays_Per_Week from Employeepayhistory where ID=(select Top 1 ID from Employeepayhistory where Emp_id='" + empcode + "' order by ID desc)";
                               SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlup, null);
                                if (dr.Read())
                                {
                                    Hourly_rate_mode = dr[0].ToString();
                                    Hourly_Rate = Convert.ToDouble(dr[1].ToString());
                                    Daily_Rate_Mode = dr[2].ToString();
                                    Daily_Rate = Convert.ToDouble(dr[3].ToString());
                                    OT1Rate = Convert.ToDouble(dr[4].ToString());
                                    OT2Rate = Convert.ToDouble(dr[5].ToString());
                                    DepartmentID = Convert.ToInt32(dr[6].ToString());
                                    DesignationID = Convert.ToInt32(dr[7].ToString());
                                    OT_Entitlement = Convert.ToString(dr[8].ToString());
                                    CPF_Entitlement = dr[9].ToString();
                                    Pay_Frequency=dr[10].ToString();
                                    WDays_Per_Week = Convert.ToDouble(dr[11].ToString());
                                }

                                #region Hourly_rate_mode
                                //check Hourly_rate_mode is auto or manual
                                if (Hourly_rate_mode == "A")//auto
                                {

                                    if (monthlyPaytxt.Text == null)
                                    {
                                        monthlyPaytxt.Text = "0";
                                    }
                                    else
                                    {
                                        double NewHourly_Rate = (12 * Convert.ToDouble(monthlyPaytxt.Text)) / (52 * 44);
                                        NewHourly_Rate = Math.Round(NewHourly_Rate * Math.Pow(10, 2)) / Math.Pow(10, 2);
                                        Hourly_Rate = NewHourly_Rate;
                                    }
                                   
                                }
                                else if (Hourly_rate_mode == "M")//Manual
                                {
                                        
                                }

                                //updating OT1 and OT2
                                    //OT1 = Hourly_Rate * OT1Rate;
                                    //OT2 = Hourly_Rate * OT2Rate;
                                //

                                #endregion


                                #region Daily Mode
                                if (Daily_Rate_Mode == "A")//auto
                                {
                                    Daily_Rate = calculate_DailyRate(Convert.ToDouble(monthlyPaytxt.Text), WDays_Per_Week);
                                }
                                else if (Daily_Rate_Mode == "M")//Manual
                                {

                                }
                                #endregion


                                #region Insert New Row in Employeepayhistory(Progression Info) 
                               
                                    DateTime dt = new DateTime();
                                    dt = rdFrom.SelectedDate.Value;

                                    int m = dt.Date.Month;
                                    int d = dt.Date.Day;
                                    int y = dt.Date.Year;
                                    Fromdate = y + "/" + m + "/" + d;


                                    DateTime dt1 = new DateTime();
                                    TimeSpan span1 = TimeSpan.FromDays(1);
                                    dt1 = rdFrom.SelectedDate.Value.Subtract(span1);

                                    int m1 = dt1.Date.Month;
                                    int d1 = dt1.Date.Day;
                                    int y1 = dt1.Date.Year;

                                    Fromdate1 = y1 + "/" + m1 + "/" + d1;

                                    string ssqlb = "Insert Into EmployeePayHistory " +
                                                   "(Emp_ID, FromDate, ToDate, ConfirmationDate, DepartmentID, DesignationID, OT_Entitlement, CPF_Entitlement, OT1Rate, OT2Rate, Pay_Frequency, WDays_Per_Week, PayRate, Hourly_Rate_Mode, Hourly_Rate, Daily_Rate_Mode, Daily_Rate)" +
                                                   "values('" + empcode + "','" + Fromdate + "','" + Fromdate1 + "',NULL,'" + DepartmentID + "','" + DesignationID + "','" + OT_Entitlement + "','" + CPF_Entitlement + "','" + OT1Rate + "','" + OT2Rate + "','" + Pay_Frequency + "','" + WDays_Per_Week + "',Encryptbyasymkey(Asymkey_id('AsymKey'), '" + monthlyPaytxt.Text + "'),'" + Hourly_rate_mode + "','" + Hourly_Rate + "','" + Daily_Rate_Mode + "','" + Daily_Rate + "')";

                                    DataAccess.FetchRS(CommandType.Text, ssqlb, null);
                                ViewState["actionMessage"]="Success|Salary Updated Sucessfully";
                                   
                                #endregion
                            }
                        }

                    }

                }
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

        public double calculate_DailyRate(double payrate, double workingdays)
        {
            double valResult=0;
            string SQL = "select dbo.fn_GetDailyRate(" + payrate + "," + workingdays + ")";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (dr.Read())
            {
                valResult = Utility.ToDouble(dr.GetValue(0));
            }
            return Utility.ToDouble(valResult);
        }


        protected void drpList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpList.SelectedValue == "Percentage")
            {
                txtAmount.Visible = true;
                lblAmount.Visible = true;
            }
            else if (drpList.SelectedValue == "Fixed")
            {
                txtAmount.Visible = true;
                lblAmount.Visible = true;
            }
            else if (drpList.SelectedValue == "Manual")
            {
                txtAmount.Visible = false;
                lblAmount.Visible = false;
            }

        }

    }
}
