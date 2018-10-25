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

namespace SMEPayroll.Payroll
{
    public partial class payrolldetailreport : System.Web.UI.Page
    {
        
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string monthstr = "";

        
        int compid;
        string month = "", year = "";
        protected ArrayList payrolllist;
        protected void Page_Load(object sender, EventArgs e)
        {
            compid = Utility.ToInteger(Session["Compid"]);
            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");
            month = Request.QueryString["month"].ToString();
            year = Request.QueryString["year"].ToString();         

            FetchData();

        }

        private void FetchData()
        {
            string CompanyID = Utility.ToString(Session["Compid"]);
            int Month = Convert.ToInt32(Request.QueryString["Month"]);
            int Year = Convert.ToInt32(Request.QueryString["Year"]);
            string monthstr = "";

            DataSet monthDs = new DataSet();
            string strsql = "sp_GetPayrollMonth";// 0,2009,2
            SqlParameter[] params1 = new SqlParameter[3];
            params1[0] = new SqlParameter("@ROWID", "0");
            params1[1] = new SqlParameter("@YEARS", 0);
            //params1[2] = new SqlParameter("@PAYTYPE", Session["PAYTYPE"].ToString());
            params1[2] = new SqlParameter("@PAYTYPE", 0);
            monthDs = DataAccess.ExecuteSPDataSet(strsql, params1);
            DataRow[] drResults = monthDs.Tables[0].Select("RowID = " + Request.QueryString["Month"]);
            foreach (DataRow drnew in drResults)
            {
                monthstr = drnew["Month"].ToString();
            }


            string EmpPassID = "";

            if (Session["EmpPassID"] != null)
            {
                EmpPassID = Convert.ToString(Session["EmpPassID"]);
            }
            else
            {
                EmpPassID = Request.QueryString["EmpPassID"].ToString();
            }


            int i = 0;
            string sSQL = "sp_GeneratePayRollAdv";
            string sSQLextData = "";
            SqlParameter[] parms = new SqlParameter[11];
            parms[i++] = new SqlParameter("@company_id", Utility.ToInteger(CompanyID));
            parms[i++] = new SqlParameter("@month", Utility.ToInteger(Month));
            parms[i++] = new SqlParameter("@year", Utility.ToInteger(Year));
            parms[i++] = new SqlParameter("@stdatemonth", Request.QueryString["stdatemonth"]);
            parms[i++] = new SqlParameter("@endatemonth", Request.QueryString["endatemonth"]);
            parms[i++] = new SqlParameter("@stdatesubmonth", Request.QueryString["stdatesubmonth"]);
            parms[i++] = new SqlParameter("@endatesubmonth", Request.QueryString["endatesubmonth"]);
            parms[i++] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"].ToString()));
            parms[i++] = new SqlParameter("@EmpPassID",EmpPassID);
            parms[i++] = new SqlParameter("@DeptId", Utility.ToInteger(Request.QueryString["dept_id"]));
            parms[i++] = new SqlParameter("@monthidintbl", Request.QueryString["monthidintbl"]);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            while (dr.Read())
            {
                int row = 0;
                int iSum=0;
                //generate rows and cells
                int numrows = 3;
                int numcells = 6;
                double dblage = 0;
                string strempcpfrow = "";
                string strempycpfrow = "";
                string pryears = "";
                string empcpftype = "";
                double dblCPFGross = 0;
                string calcCPF = "";
                string stragegroup = "";
                string strdailybasicrate = "";

                for (int iRows = 0; iRows < numrows; iRows++)
                {
                    dblage = Utility.ToDouble(dr["Age"].ToString());

                    HtmlTableRow rHead = new HtmlTableRow();

                    // set bgcolor on alternating rows
                    if (row % 3 == 0)
                        rHead.BgColor = "Gainsboro";


                    for (int iCols = 0; iCols < numcells; iCols++)
                    {
                        iSum=iCols+iRows;
                        HtmlTableCell c1 = new HtmlTableCell();
                        c1.Attributes.Add("class", "colnamebold");
                        c1.Style.Add("font-family", "Verdana");
                        c1.Style.Add("font-size", "9px");

                        HtmlTableCell c2 = new HtmlTableCell();
                        c2.Attributes.Add("class", "bodytxt");
                        c2.Style.Add("font-family", "Verdana");
                        c2.Style.Add("font-size", "9px");

                        if (row % 3 == 0)
                        {
                            if (iSum == 0)
                            {
                                double years = Math.Floor(dblage);
                                double days = dblage - years;
                                days = Math.Floor(days * 365);
                                c1.Controls.Add(new LiteralControl("Name:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["FullName"].ToString()) + " is " + years + " years" + " & " + days + " days."));
                            }
                            else if (iSum == 1)
                            {
                                c1.Controls.Add(new LiteralControl("Dept:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["DeptName"].ToString())));
                            }
                            else if (iSum == 2)
                            {
                                c1.Controls.Add(new LiteralControl("Group:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["GroupName"].ToString())));
                            }
                            else if (iSum == 3)
                            {
                                c1.Controls.Add(new LiteralControl("Joining:"));

                                if (dr["Join_Date"] != DBNull.Value)
                                {
                                    c2.Controls.Add(new LiteralControl(DateTime.Parse(dr["Join_Date"].ToString()).ToShortDateString()));
                                }
                                else
                                {
                                    c2.Controls.Add(new LiteralControl(""));
                                }
                            }
                            else if (iSum == 4)
                            {
                                c1.Controls.Add(new LiteralControl("PR Date:"));

                                if (dr["PR_Date"] != DBNull.Value)
                                {
                                    c2.Controls.Add(new LiteralControl(DateTime.Parse(dr["PR_Date"].ToString()).ToShortDateString()));
                                }
                                else
                                {
                                    c2.Controls.Add(new LiteralControl(""));
                                }
                            }
                            else if (iSum == 5)
                            {
                                c1.Controls.Add(new LiteralControl("Termination:"));

                                if (dr["Term_Date"] != DBNull.Value)
                                {
                                    c2.Controls.Add(new LiteralControl(DateTime.Parse(dr["Term_Date"].ToString()).ToShortDateString()));
                                }
                                else
                                {
                                    c2.Controls.Add(new LiteralControl(""));
                                }
                            }
                        }
                        else if (row % 3 == 1)
                        {
                            if (iSum == 1)
                            {
                                c1.Controls.Add(new LiteralControl("Date:"));

                                c2.Controls.Add(new LiteralControl("Days in Month (" + Utility.ToDouble(dr["WrkgDaysInRoll"].ToString()) + ")| Act worked Days(" + Utility.ToDouble(dr["ActWrkgDaysSpan"].ToString()) + ")| Unpaid Leaves(" + Utility.ToDouble(dr["UnpaidLeaves"].ToString()) + ")"));
                            }
                            else if (iSum == 2)
                            {
                                c1.Controls.Add(new LiteralControl("Day Rate:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["BasicDayRate"].ToString())));
                            }
                            else if (iSum == 3)
                            {
                                c1.Controls.Add(new LiteralControl("Hr Rate:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["Hourly_Rate"].ToString())));
                            }
                            else if (iSum == 4)
                            {
                                c1.Controls.Add(new LiteralControl("OT Entitled:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["OT"].ToString())));
                            }
                            else if (iSum == 5)
                            {
                                c1.Controls.Add(new LiteralControl("OT1 Rate:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["OT1Rate"].ToString())));
                            }
                            else if (iSum == 6)
                            {
                                c1.Controls.Add(new LiteralControl("OT2 Rate:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(dr["OT2Rate"].ToString())));
                            }
                        }
                        else if (row % 3 == 2)
                        {
                            if (iSum == 2)
                            {
                                pryears = Utility.ToString(dr["PRAge"].ToString());
                                empcpftype = Utility.ToString(dr["EmpCPFType"].ToString());
                                dblCPFGross = Utility.ToDouble(dr["CPFGross"].ToString());

                                if (dblage <= 35)
                                {
                                    stragegroup = "Below 35";
                                }
                                else if (dblage > 35 && dblage <= 50)
                                {
                                    stragegroup = "35 - 50";
                                }
                                else if (dblage > 50 && dblage <= 55)
                                {
                                    stragegroup = "50 - 55";
                                }
                                else if (dblage > 55 && dblage <= 60)
                                {
                                    stragegroup = "55 - 60";
                                }
                                else if (dblage > 60 && dblage <= 65)
                                {
                                    stragegroup = "60 - 65";
                                }
                                else if (dblage > 65)
                                {
                                    stragegroup = "Above 65";
                                }

                                if (dr["EmployeeCPFRow"] == DBNull.Value || dr["EmployeeCPFRow"].ToString() == "0")
                                {
                                    strempcpfrow = Utility.ToString(dr["CPFEmployeePerc"].ToString());
                                }
                                else
                                {
                                    strempcpfrow = Utility.ToString(dr["EmployeeCPFRow"].ToString());
                                }
                                if (dr["EmployerCPFRow"] == DBNull.Value || dr["EmployerCPFRow"].ToString() == "0")
                                {
                                    strempycpfrow = Utility.ToString(dr["CPFEmployerPerc"].ToString());
                                }
                                else
                                {
                                    strempycpfrow = Utility.ToString(dr["EmployerCPFRow"].ToString());
                                }


                                if (Utility.ToString(dr["CalculateCPF"].ToString()) == "Y")
                                {
                                    if (dblCPFGross <= 1500)
                                    {
                                        //NONE
                                    }
                                    else
                                    {
                                        sSQLextData = "SELECT age_from, age_to FROM cpf_calcs WHERE (pr_year = " + pryears + ") AND (emp_group = " + empcpftype + " ) AND (" + dblage + " >= age_from) AND (" + dblage + " <= age_to)";
                                        SqlDataReader drExtData = DataAccess.ExecuteReader(CommandType.Text, sSQLextData, null);
                                        while (drExtData.Read())
                                        {
                                            stragegroup = Utility.ToString(drExtData["age_from"].ToString()) + " - " + Utility.ToString(drExtData["age_to"].ToString());
                                        }
                                        drExtData.Close();
                                        strempcpfrow = Utility.ToString(dr["CPFEmployeePerc"].ToString());
                                        strempycpfrow = Utility.ToString(dr["CPFEmployerPerc"].ToString());
                                    }

                                }


                                c1.Controls.Add(new LiteralControl("CPF:"));

                                c2.Controls.Add(new LiteralControl("Applicable (" + Utility.ToString(dr["CPF"].ToString()) + ") | Group (" + stragegroup + ") | Emp%(" + strempcpfrow + ")" + "| Empyr%(" + strempycpfrow + ")"));
                            }
                            else if (iSum == 3)
                            {
                                string strpaymode = Utility.ToString(dr["Pay_Mode"].ToString());

                                if (strpaymode != "Cheque" && strpaymode != "Cash")
                                {
                                    strpaymode = "Bank";
                                }

                                c1.Controls.Add(new LiteralControl("Pay Mode:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(strpaymode)));
                            }
                            else if (iSum == 4)
                            {
                                c1.Controls.Add(new LiteralControl("Gross Pay:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(Utility.ToDouble(dr["GrossWithAddition"].ToString()).ToString("#0.00"))));
                            }
                            else if (iSum == 5)
                            {
                                c1.Controls.Add(new LiteralControl("Empyr CPF:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(Utility.ToDouble(dr["EmployerCPFAmt"].ToString()).ToString("#0.00"))));
                            }
                            else if (iSum == 6)
                            {
                                double sdlamount = Utility.ToDouble(dr["SDLFundGrossAmount"]);
                                string sdl = "";
                                Object objsdl = null;
                                if (dr["SDF_Required"].ToString().Trim() == "2")
                                {
                                    if (sdlamount > 0)
                                    {
                                        objsdl = sdlamount;
                                    }
                                    else
                                    {
                                        objsdl = Utility.ToDouble(dr["FundGrossAmount"]);
                                    }
                                }

                                if (objsdl != DBNull.Value)
                                {
                                    int intmonth = Utility.ToInteger(Request.QueryString["Month"]) - 1;
                                    double fundgrossamount = Utility.ToDouble(objsdl);
                                    string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + ")";

                                    ////////////////////////////////////////Get Payroll Month///////////////////////////////////////
                                    //string sqlmonth = "SELECT Month FROM payrollmonthlydetail WHERE ROWID=" + intmonth;
                                    //SqlDataReader dr1;
                                    //dr1 = DataAccess.ExecuteReader(CommandType.Text, sqlmonth, null);
                                    //int intactualmonth = 1;
                                    //while (dr1.Read())
                                    //{
                                    //    intactualmonth = Convert.ToInt32(dr1[0].ToString());
                                    //}
                                    //string sSql = "Select dbo.fn_getSDLAmount(" + fundgrossamount.ToString() + "," + intactualmonth + "," + Year + ")";
                                    ///////////////////////////////////////////////////////////////////////////////////////////////////////


                                    string strSqlDed = "; Select isnull(sum(SDL),0) SDLAmt From prepare_payroll_detail pd Inner Join prepare_payroll_hdr ph on pd.trx_id = ph.trx_id Where ([Status]='A' Or [Status]='G' Or [Status]='P') And Emp_ID=" + dr["Emp_Code"] + " And ph.end_period=(SELECT PaySubEndDate FROM   PayrollMonthlyDetail  WHERE  ROWID = (" + intmonth.ToString() + "))";
                                    DataSet dsSDL = new DataSet();
                                    dsSDL = DataAccess.FetchRS(CommandType.Text, (sSql + strSqlDed), null);
                                    if (dsSDL != null)
                                    {
                                        if (dsSDL.Tables[0].Rows.Count > 0)
                                        {
                                            if (Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) <= 0)
                                            {
                                                //NONE
                                            }
                                            else
                                            {
                                               if (Utility.ToInteger(Session["PaySubStartDay"].ToString()) > 1)
                                                {
                                                    sdl = Utility.ToString(Utility.ToDouble(dsSDL.Tables[0].Rows[0][0].ToString()) - Utility.ToDouble(dsSDL.Tables[1].Rows[0][0].ToString()));
                                                }
                                                else
                                                {
                                                    sdl = dsSDL.Tables[0].Rows[0][0].ToString();
                                                }
                                            }
                                        }
                                    }
                                }
                                c1.Controls.Add(new LiteralControl("SDL:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToString(sdl)));
                            }
                            else if (iSum == 7)
                            {
                                c1.Controls.Add(new LiteralControl("Net Pay:"));

                                c2.Controls.Add(new LiteralControl(Utility.ToDouble(dr["NetPay"].ToString()).ToString("#0.00")));
                            }
                           
                        }

                        rHead.Cells.Add(c1);
                        rHead.Cells.Add(c2);

                        c1 = null;
                        c2 = null;
                    }

                    Table1.Rows.Add(rHead);

                    row++;
                }

                if (dr["PayType"].ToString().Trim() == "H" && dr["Daily_rate_mode"].ToString().Trim() == "A")
                {
                    strdailybasicrate = dr["BasicDayRate"].ToString();
                }
                else if ((dr["PayType"].ToString().Trim() == "H" && dr["Daily_rate_mode"].ToString().Trim() == "M") || dr["PayType"].ToString().Trim() == "D" && dr["Daily_rate_mode"].ToString().Trim() == "M")
                {
                    strdailybasicrate = dr["Daily_Rate"].ToString();
                }

                string sSQL1 = "sp_GetEmployeePayDetails";
                SqlParameter[] parms1 = new SqlParameter[23];
                int j = 0;
                parms1[j++] = new SqlParameter("@emp_code", dr["Emp_Code"].ToString());
                parms1[j++] = new SqlParameter("@Year", Utility.ToString(Request.QueryString["Year"]));
                parms1[j++] = new SqlParameter("@Month", Utility.ToString(monthstr));
                parms1[j++] = new SqlParameter("@stdatesubmonth", Session["PaySubStartDay"].ToString());
                parms1[j++] = new SqlParameter("@endatesubmonth", Session["PaySubEndDay"].ToString());
                if (Utility.ToString(dr["Days_Work"]) == "")
                {
                    parms1[j++] = new SqlParameter("@Day_Work", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@Day_Work", Utility.ToString(dr["Days_Work"]));
                }
                if (Utility.ToString(dr["OT1"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1", Utility.ToString(dr["OT1"]));
                }
                if (Utility.ToString(dr["OT2"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2", Utility.ToString(dr["OT2"]));
                }

                if (Utility.ToString(strdailybasicrate) == "")
                {
                    parms1[j++] = new SqlParameter("@BasicDayRate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@BasicDayRate", Utility.ToString(strdailybasicrate));
                }
                if (Utility.ToString(dr["OT1Hrs"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1Hrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1Hrs", Utility.ToString(dr["OT1Hrs"]));
                }
                if (Utility.ToString(dr["OT2Hrs"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2Hrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2Hrs", Utility.ToString(dr["OT2Hrs"]));
                }
                if (Utility.ToString(dr["OT1Rate"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT1Rate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT1Rate", Utility.ToString(dr["OT1Rate"]));
                }

                if (Utility.ToString(dr["OT2Rate"]) == "")
                {
                    parms1[j++] = new SqlParameter("@OT2Rate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@OT2Rate", Utility.ToString(dr["OT2Rate"]));
                }

                if (Utility.ToString(dr["EmployeeCPFAmt"]) == "")
                {
                    parms1[j++] = new SqlParameter("@empcpfamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@empcpfamount", Utility.ToString(dr["EmployeeCPFAmt"]));
                }

                if (Utility.ToString(dr["CPFOrdinaryCeil"]) == "")
                {
                    parms1[j++] = new SqlParameter("@ordwages", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@ordwages", Utility.ToString(dr["CPFOrdinaryCeil"]));
                }

                if (Utility.ToString(dr["CPFAdditionNet"]) == "")
                {
                    parms1[j++] = new SqlParameter("@addwages", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@addwages", Utility.ToString(dr["CPFAdditionNet"]));
                }
                if (Utility.ToString(dr["CPFEmployeePerc"]) == "")
                {
                    parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString(strempcpfrow));
                }
                else
                {
                    parms1[j++] = new SqlParameter("@cpfrate", Utility.ToString(strempcpfrow));
                }
                if (Utility.ToString(dr["FundType"]) == "")
                {
                    parms1[j++] = new SqlParameter("@fundname", '-');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundname", Utility.ToString(dr["FundType"]));
                }
                if (Utility.ToDouble(dr["FundAmount"]) <= 0)
                {
                    parms1[j++] = new SqlParameter("@fundamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundamount", Utility.ToString(dr["FundAmount"]));
                }
                if (Utility.ToDouble(dr["FundGrossAmount"]) <= 0)
                {
                    parms1[j++] = new SqlParameter("@fundgrossamount", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@fundgrossamount", Utility.ToString(dr["FundGrossAmount"]));
                }
                if (Utility.ToDouble(dr["NHHrs"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@nhhrs", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@nhhrs", Utility.ToString(dr["NHHrs"]));
                }
                if (Utility.ToDouble(dr["Hourly_Rate"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@hourlyrate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@hourlyrate", Utility.ToString(dr["Hourly_Rate"]));
                }
                if (Utility.ToDouble(dr["DaysWorkedRate"]) == 0)
                {
                    parms1[j++] = new SqlParameter("@daysworkedrate", '0');
                }
                else
                {
                    parms1[j++] = new SqlParameter("@daysworkedrate", Utility.ToString(dr["DaysWorkedRate"]));
                }


                
                DataSet ds = new DataSet();
                ds = DataAccess.ExecuteSPDataSet(sSQL1, parms1);
                if (ds != null)
                {
                    HtmlTableRow rFoot = new HtmlTableRow();

                    HtmlTableCell c1 = new HtmlTableCell();
                    c1.Attributes.Add("class", "colnamebold");
                    c1.Style.Add("font-family", "Verdana");
                    c1.Style.Add("font-size", "9px");
                    c1.ColSpan = 4;
                    c1.VAlign = "Bottom";

                    HtmlTableCell c2 = new HtmlTableCell();
                    c2.Attributes.Add("class", "colnamebold");
                    c2.Style.Add("font-family", "Verdana");
                    c2.Style.Add("font-size", "9px");
                    c2.ColSpan = 8;
                    c2.VAlign = "Bottom";
                    c1.Controls.Add(new LiteralControl("ADDITIONS"));
                    c2.Controls.Add(new LiteralControl("DEDUCTIONS"));

                    rFoot.Cells.Add(c1);
                    rFoot.Cells.Add(c2);

                    c1 = null;
                    c2 = null;
                    rFoot.VAlign = "Bottom";

                    Table1.Rows.Add(rFoot);




                    HtmlTableRow rFootnew = new HtmlTableRow();
                    HtmlTableCell c111 = new HtmlTableCell();
                    c111.Attributes.Add("class", "colnamebold");
                    c111.Style.Add("font-family", "Verdana");
                    c111.Style.Add("font-size", "9px");
                    c111.ColSpan = 4;
                    c111.VAlign = "Top";


                    HtmlTableCell c211 = new HtmlTableCell();
                    c211.Attributes.Add("class", "colnamebold");
                    c211.Style.Add("font-family", "Verdana");
                    c211.Style.Add("font-size", "9px");
                    c211.ColSpan = 8;
                    c211.VAlign = "Top";

                    




                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        HtmlTable ht1 = new HtmlTable();
                        

                        foreach (DataRow drnew in ds.Tables[0].Rows)
                        {
                            HtmlTableRow htrow = new HtmlTableRow();
                            HtmlTableCell c11 = new HtmlTableCell();
                            c11.Attributes.Add("class", "colnamebold");
                            c11.Style.Add("font-family", "Verdana");
                            c11.Style.Add("font-size", "9px");
                            //c11.ColSpan = 4;
                            c11.VAlign = "Top";
                            c11.Controls.Add(new LiteralControl(drnew[0].ToString()));

                            HtmlTableCell c21 = new HtmlTableCell();
                            c21.Attributes.Add("class", "colnamebold");
                            c21.Style.Add("font-family", "Verdana");
                            c21.Style.Add("font-size", "9px");
                            //c21.ColSpan = 4;
                            c21.VAlign = "Top";
                            c21.Controls.Add(new LiteralControl(drnew[1].ToString()));
                            htrow.Cells.Add(c11);
                            htrow.Cells.Add(c21);
                            ht1.Rows.Add(htrow);
                        }

                        c111.Controls.Add(ht1);
                        rFootnew.Cells.Add(c111);
                        
                        
                        //RadGrid rd = new RadGrid();
                        //rd.Font.Size = FontUnit.Smaller;
                        //rd.Width = Unit.Percentage(100);
                        //rd.DataSource = ds.Tables[0];
                        //rd.DataBind();
                        //c1.Controls.Add(rd);
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        HtmlTable ht1 = new HtmlTable();


                        foreach (DataRow drnew in ds.Tables[1].Rows)
                        {
                            HtmlTableRow htrow = new HtmlTableRow();
                            HtmlTableCell c11 = new HtmlTableCell();
                            c11.Attributes.Add("class", "colnamebold");
                            c11.Style.Add("font-family", "Verdana");
                            c11.Style.Add("font-size", "9px");
                            //c11.ColSpan = 4;
                            c11.VAlign = "Top";
                            c11.Controls.Add(new LiteralControl(drnew[0].ToString()));

                            HtmlTableCell c21 = new HtmlTableCell();
                            c21.Attributes.Add("class", "colnamebold");
                            c21.Style.Add("font-family", "Verdana");
                            c21.Style.Add("font-size", "9px");
                            //c21.ColSpan = 8;
                            c21.VAlign = "Top";
                            c21.Controls.Add(new LiteralControl(drnew[1].ToString()));
                            htrow.Cells.Add(c11);
                            htrow.Cells.Add(c21);
                            ht1.Rows.Add(htrow);
                        }

                        c211.Controls.Add(ht1);

                        
                        rFootnew.Cells.Add(c211);

                        //RadGrid rd = new RadGrid();
                        //rd.Font.Size = FontUnit.Smaller;
                        //rd.Width = Unit.Percentage(100);
                        //rd.DataSource = ds.Tables[1];
                        //rd.DataBind();
                        //c2.Controls.Add(rd);

                    }

                    Table1.Rows.Add(rFootnew);
                }
            }
        }       
    }
}


