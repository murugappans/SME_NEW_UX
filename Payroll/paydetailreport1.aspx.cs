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

namespace SMEPayroll.Payroll
{
    public partial class paydetailreport1 : System.Web.UI.Page
    {
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        public double tot_ot1;
        public double tot_ot2;
        public double tot_basicpay;
        public double tot_total_deductions;
        public double tot_total_additions;
        public double tot_netpay;
        public double tot_employercpf;
        public double tot_employeecpf;
        public double tot_fundamt;
        public double tot_grosspay;
        public double tot_cpfGrossPay; 

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
            string sSQL = "";
            sSQL = "sp_paydetailreport2";
            SqlParameter[] parms = new SqlParameter[3];
            parms[0] = new SqlParameter("@compid", compid);
            parms[1] = new SqlParameter("@month", month);
            parms[2] = new SqlParameter("@year", year);
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, parms);
            payrolllist = new ArrayList();

            char strEmp = ',';
            string[] arrayEmp = null;
            if (Session["EmpPassID"] != null)
            {
                if (Session["EmpPassID"].ToString() != "")
                {
                    string strtemp = (string)Session["EmpPassID"].ToString();
                    arrayEmp = strtemp.Split(strEmp);
                }
            }
            //if (ds.Tables.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        for (int i = 0; i <= arrayEmp.Length - 1; i++)
            //        {
            //            if (dr["emp_id"].ToString() != arrayEmp[i].ToString())
            //            {
            //                ds.Tables[0].Rows.Remove(dr);
            //            }
            //        }
            //    }
            //}


            if (arrayEmp!=null && arrayEmp.Length > 0)
            {

                while (dr.Read())
                {
                    for (int i = 0; i <= arrayEmp.Length - 1; i++)
                    {
                        if (dr["emp_id"].ToString() == arrayEmp[i].ToString())
                        {
                            paylist opaylist = new paylist();
                            opaylist.empname = Utility.ToString(dr.GetValue(1));
                            opaylist.basicpay = Utility.ToString(dr.GetValue(2));
                            opaylist.total_additions = Utility.ToString(dr.GetValue(3));
                            opaylist.total_deductions = Utility.ToString(dr.GetValue(4));
                            opaylist.ot1 = Utility.ToString(dr.GetValue(5));
                            opaylist.ot2 = Utility.ToString(dr.GetValue(6));


                            opaylist.netpay = Utility.ToString(dr.GetValue(7));
                            opaylist.employeecpf = Utility.ToString(dr.GetValue(9));
                            opaylist.employercpf = Utility.ToString(dr.GetValue(10));

                            opaylist.fundtype = Utility.ToString(dr.GetValue(12));
                            opaylist.fundamt = Utility.ToString(dr.GetValue(13));
                            opaylist.cpfGross = Utility.ToString(dr.GetValue(16));
                            //opaylist.grosspay = Utility.ToString(dr.GetValue(15));

                            payrolllist.Add(opaylist);
                            tot_ot1 = tot_ot1 + Utility.ToDouble(dr.GetValue(5));
                            tot_ot2 = tot_ot2 + Utility.ToDouble(dr.GetValue(6));
                            tot_basicpay = tot_basicpay + Utility.ToDouble(dr.GetValue(2));
                            tot_total_deductions = tot_total_deductions + Utility.ToDouble(dr.GetValue(4));
                            tot_total_additions = tot_total_additions + Utility.ToDouble(dr.GetValue(3));
                            tot_netpay = tot_netpay + Utility.ToDouble(dr.GetValue(7));
                            tot_employercpf = tot_employercpf + Utility.ToDouble(dr.GetValue(10));
                            tot_employeecpf = tot_employeecpf + Utility.ToDouble(dr.GetValue(9));
                            tot_fundamt = tot_fundamt + Utility.ToDouble(dr.GetValue(13));
                            //tot_grosspay = tot_grosspay + Utility.ToDouble(dr.GetValue(15));
                            tot_cpfGrossPay = tot_cpfGrossPay + Utility.ToDouble(dr.GetValue(16));

                        }
                    }

                }

            }
            else
            {

                while (dr.Read())
                {
                   
                    paylist opaylist = new paylist();
                    opaylist.empname = Utility.ToString(dr.GetValue(1));
                    opaylist.basicpay = Utility.ToString(dr.GetValue(2));
                    opaylist.total_additions = Utility.ToString(dr.GetValue(3));
                    opaylist.total_deductions = Utility.ToString(dr.GetValue(4));
                    opaylist.ot1 = Utility.ToString(dr.GetValue(5));
                    opaylist.ot2 = Utility.ToString(dr.GetValue(6));


                    opaylist.netpay = Utility.ToString(dr.GetValue(7));
                    opaylist.employeecpf = Utility.ToString(dr.GetValue(9));
                    opaylist.employercpf = Utility.ToString(dr.GetValue(10));

                    opaylist.fundtype = Utility.ToString(dr.GetValue(12));
                    opaylist.fundamt = Utility.ToString(dr.GetValue(13));
                    opaylist.cpfGross = Utility.ToString(dr.GetValue(16));
                    opaylist.grosspay = Utility.ToString(dr.GetValue(15));

                    payrolllist.Add(opaylist);
                    tot_ot1 = tot_ot1 + Utility.ToDouble(dr.GetValue(5));
                    tot_ot2 = tot_ot2 + Utility.ToDouble(dr.GetValue(6));
                    tot_basicpay = tot_basicpay + Utility.ToDouble(dr.GetValue(2));
                    tot_total_deductions = tot_total_deductions + Utility.ToDouble(dr.GetValue(4));
                    tot_total_additions = tot_total_additions + Utility.ToDouble(dr.GetValue(3));
                    tot_netpay = tot_netpay + Utility.ToDouble(dr.GetValue(7));
                    tot_employercpf = tot_employercpf + Utility.ToDouble(dr.GetValue(10));
                    tot_employeecpf = tot_employeecpf + Utility.ToDouble(dr.GetValue(9));
                    tot_fundamt = tot_fundamt + Utility.ToDouble(dr.GetValue(13));
                    //tot_grosspay = tot_grosspay + Utility.ToDouble(dr.GetValue(15));
                    tot_cpfGrossPay = tot_cpfGrossPay + Utility.ToDouble(dr.GetValue(16));
                }
            
            }

           
        }
        public class paylist
        {
            public string empname;
            public string ot1;
            public string ot2;
            public string basicpay;
            public string total_deductions;
            public string total_additions;
            public string netpay;
            public string employercpf;
            public string employeecpf;
            public string fundtype;
            public string fundamt;
            public string grosspay;
            public string cpfGross;

        }
    }
}