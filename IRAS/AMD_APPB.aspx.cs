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
using System.IO;
using System.Text;
using System.Xml;
using System.Reflection;

using IRAS.Appendix_A;
using IRAS.Appendix_B;

using System.Collections.Generic;
using System.Globalization;

namespace IRAS
{
    public partial class AMD_APPB : System.Web.UI.Page
    {
        # region Style
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string varEmpCode = "";
        # endregion Style

        //string varEmpCode = "";
        int yearCode;

        //SqlDataSource sql = new SqlDataSource();
        //public List<A8BRECORDDETAILS> _a8details = new List<A8BRECORDDETAILS>();
         SqlDataSource sql = new SqlDataSource();
            public List<A8BRECORDDETAILS> _a8details = new List<A8BRECORDDETAILS>();
        void Page_Render(Object sender, EventArgs e)
        {

            Page.ClientScript.RegisterForEventValidation(this.UniqueID);
        }

     
        protected void Page_Load(object sender, EventArgs e)
        {
            sql.ConnectionString = Session["ConString"].ToString();
            varEmpCode = Request.QueryString["empcode"].ToString();
            yearCode = Utility.ToInteger(Request.QueryString["year"].ToString());

            btnsave.ServerClick += new EventHandler(btnsave_ServerClick);
            ButtonCALCULATE.ServerClick += new EventHandler(ButtonCALCULATE_Click);
            if (!IsPostBack)
            {
                fill_appendixB_form();
            }
        }

      
        void btnsave_ServerClick(object sender, EventArgs e)
        {



            update_appendixB_form();


        }


        void fill_appendixB_form()
        {
            //ndo appendix year

            if (!IsPostBack)
            {

                A8BRECORDDETAILS single_details;
                SqlDataReader sqlDr = null;


                string sSQL = @"SELECT 


k.ID as A8a2009ST_ID ,
k.[DateOfIncorporation],
l.[ID]
      ,l.[CompanyIDType]
      ,l.[CompanyIDNo]
      ,l.[CompanyName]
      ,l.[PlanType]
      ,l.[DateOfGrant]
      ,l.[DateOfExercise]
      ,l.[Price]
      ,l.[OpenMarketValueAtDateOfGrant]
      ,l.[OpenMarketValueAtDateOfExercise]
      ,l.[NoOfShares]
      ,l.[NonExemptGrossAmount]
      ,l.[GrossAmountGains]
      ,l.[FK_A8A2009ST]
      ,l.[Total_i]
      ,l.[Total_j]
      ,l.[Total_k]
      ,l.[Total_L]
      ,l.[Total_M]
      ,l.[RecordNo]
       ,l.[Section]
       ,l.[G_Total]
        ,[G_Total_I]
      ,[G_Total_J]
      ,[G_Total_K]
      ,[G_Total_L]
      ,[G_Total_M]
FROM  [A8B2009ST] as k join [A8BRECORDDETAILS] as l on k.ID =l.FK_A8A2009ST
where k.emp_id='" + varEmpCode + "' and AppendixB_year='" + yearCode + "' and IS_AMMENDMENT=1";
                sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);

                Session["fk"] = "";
                string _DateofIncorporation = "";
                while (sqlDr.Read())
                {

                    single_details = new A8BRECORDDETAILS();
                    single_details.CompanyIDNo = Convert.ToString(sqlDr["CompanyIDNo"].ToString());
                    single_details.CompanyName = Convert.ToString(sqlDr["CompanyName"].ToString());
                    single_details.PlanType = Convert.ToString(sqlDr["PlanType"].ToString());
                    single_details.DateOfGrant = Convert.ToString(sqlDr["DateOfGrant"].ToString());
                    single_details.DateOfExercise = Convert.ToString(sqlDr["DateOfExercise"].ToString());
                    single_details.Price = Convert.ToDecimal(sqlDr["Price"]);
                    single_details.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfGrant"]);
                    single_details.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sqlDr["OpenMarketValueAtDateOfExercise"]);
                    single_details.NoOfShares = Convert.ToInt32(sqlDr["NoOfShares"]);
                    single_details.NonExemptGrossAmount = Convert.ToDecimal(sqlDr["NonExemptGrossAmount"]);
                    single_details.GrossAmountGains = Convert.ToDecimal(sqlDr["GrossAmountGains"]);
                    single_details.RecordNo = Convert.ToString(sqlDr["RecordNo"].ToString());
                    single_details.Section = Convert.ToString(sqlDr["Section"].ToString());
                    single_details.FK_ID = Convert.ToInt32(sqlDr["ID"]);
                    _a8details.Add(single_details);
                    _DateofIncorporation = Convert.ToString(sqlDr["DateOfIncorporation"].ToString());
                }

                DateTime DateOfIncorpration;




                bool isDateTime = DateTime.TryParseExact(_DateofIncorporation, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture,
                              DateTimeStyles.None,
                              out DateOfIncorpration);

                if (isDateTime)
                {
                    this.DateOfIncorporation.SelectedDate = Convert.ToDateTime(_DateofIncorporation);
                }




                Control[] allControls = helper.FlattenHierachy(this.Form);

                foreach (Control control in allControls)
                {

                    if (control.GetType() == typeof(Telerik.WebControls.RadDatePicker))
                    {
                        Telerik.WebControls.RadDatePicker datepicker = control as Telerik.WebControls.RadDatePicker;
                        if (!datepicker.SelectedDate.HasValue)
                        {
                            datepicker.SelectedDate = DateTime.Now;
                        }
                    }
                }



                foreach (A8BRECORDDETAILS st in _a8details)
                {

                    if (st.Section == "A")
                    {
                        if (st.RecordNo == "sa1")
                        {
                            this.sa_a1.Text = st.CompanyIDNo;
                            this.sa_b1.Text = st.CompanyName;
                            this.sa_ca1.Text = st.PlanType;
                            this.sa_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sa_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sa_e1.Value = Convert.ToDouble(st.Price);
                            this.sa_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sa_h1.Value = Convert.ToInt32(st.NoOfShares);
                            this.sa_l1.Text = Convert.ToString(st.Total_L);
                            this.sa_m1.Text = Convert.ToString(st.Total_M);


                        }
                        else if (st.RecordNo == "sa2")
                        {
                            this.sa_a2.Text = st.CompanyIDNo;
                            this.sa_b2.Text = st.CompanyName;
                            this.sa_ca2.Text = st.PlanType;
                            this.sa_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sa_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sa_e2.Value = Convert.ToDouble(st.Price);
                            this.sa_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sa_h2.Value = Convert.ToInt32(st.NoOfShares);
                            this.sa_l2.Text = Convert.ToString(st.Total_L);
                            this.sa_m2.Text = Convert.ToString(st.Total_M);

                        }
                        else if (st.RecordNo == "sa3")
                        {
                            this.sa_a3.Text = st.CompanyIDNo;
                            this.sa_b3.Text = st.CompanyName;
                            this.sa_ca3.Text = st.PlanType;
                            this.sa_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sa_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sa_e3.Value = Convert.ToDouble(st.Price);
                            this.sa_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sa_h3.Value = Convert.ToInt32(st.NoOfShares);
                            this.sa_l3.Text = Convert.ToString(st.Total_L);
                            this.sa_m3.Text = Convert.ToString(st.Total_M);

                        }
                        decimal l = (Convert.ToDecimal(sa_l1.Text) + Convert.ToDecimal(sa_l2.Text) + Convert.ToDecimal(sa_l3.Text));
                        sa_tl.Text = Convert.ToString(l);
                        decimal m = (Convert.ToDecimal(sa_m1.Text) + Convert.ToDecimal(sa_m2.Text) + Convert.ToDecimal(sa_m3.Text));
                        sa_tm.Text = Convert.ToString(m);

                    }
                    else if (st.Section == "B")
                    {
                        if (st.RecordNo == "sb1")
                        {
                            this.sb_a1.Text = st.CompanyIDNo;
                            this.sb_b1.Text = st.CompanyName;
                            this.sb_ca1.Text = st.PlanType;
                            this.sb_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sb_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sb_e1.Value = Convert.ToDouble(st.Price);
                            this.sb_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sb_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sb_h1.Value = Convert.ToInt32(st.NoOfShares);
                            this.sb_l1.Text = Convert.ToString(st.Total_L);
                            this.sb_m1.Text = Convert.ToString(st.Total_M);
                            this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sb_i1.Text = Convert.ToString(st.I_J_K);
                        }
                        else if (st.RecordNo == "sb2")
                        {
                            this.sb_a2.Text = st.CompanyIDNo;
                            this.sb_b2.Text = st.CompanyName;
                            this.sb_ca2.Text = st.PlanType;
                            this.sb_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sb_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sb_e2.Value = Convert.ToDouble(st.Price);
                            this.sb_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sb_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sb_h2.Value = Convert.ToInt32(st.NoOfShares);
                            this.sb_l2.Text = Convert.ToString(st.Total_L);
                            this.sb_m2.Text = Convert.ToString(st.Total_M);
                            this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sb_i2.Text = Convert.ToString(st.I_J_K);
                        }
                        else if (st.RecordNo == "sb3")
                        {
                            this.sb_a3.Text = st.CompanyIDNo;
                            this.sb_b3.Text = st.CompanyName;
                            this.sb_ca3.Text = st.PlanType;
                            this.sb_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sb_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sb_e3.Value = Convert.ToDouble(st.Price);
                            this.sb_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sb_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sb_h3.Value = Convert.ToInt32(st.NoOfShares);
                            this.sb_l3.Text = Convert.ToString(st.Total_L);
                            this.sb_m3.Text = Convert.ToString(st.Total_M);
                            this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sb_i3.Text = Convert.ToString(st.I_J_K);
                        }
                        decimal x = (Convert.ToDecimal(sb_i1.Text) + Convert.ToDecimal(sb_i2.Text) + Convert.ToDecimal(sb_i3.Text));
                        sb_ti.Text = Convert.ToString(x);

                        decimal l = (Convert.ToDecimal(sb_l1.Text) + Convert.ToDecimal(sb_l2.Text) + Convert.ToDecimal(sb_l3.Text));
                        sb_tl.Text = Convert.ToString(l);
                        decimal m = (Convert.ToDecimal(sb_m1.Text) + Convert.ToDecimal(sb_m2.Text) + Convert.ToDecimal(sb_m3.Text));
                        sb_tm.Text = Convert.ToString(m);




                    }
                    else if (st.Section == "C")
                    {
                        if (st.RecordNo == "sc1")
                        {
                            this.sc_a1.Text = st.CompanyIDNo;
                            this.sc_b1.Text = st.CompanyName;
                            this.sc_ca1.Text = st.PlanType;
                            this.sc_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sc_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sc_e1.Value = Convert.ToDouble(st.Price);
                            this.sc_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sc_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sc_h1.Value = Convert.ToInt32(st.NoOfShares);
                            this.sc_l1.Text = Convert.ToString(st.Total_L);
                            this.sc_m1.Text = Convert.ToString(st.Total_M);
                            this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sc_j1.Text = Convert.ToString(st.I_J_K);
                        }
                        else if (st.RecordNo == "sc2")
                        {
                            this.sc_a2.Text = st.CompanyIDNo;
                            this.sc_b2.Text = st.CompanyName;
                            this.sc_ca2.Text = st.PlanType;
                            this.sc_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sc_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sc_e2.Value = Convert.ToDouble(st.Price);
                            this.sc_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sc_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sc_h2.Value = Convert.ToInt32(st.NoOfShares);
                            this.sc_l2.Text = Convert.ToString(st.Total_L);
                            this.sc_m2.Text = Convert.ToString(st.Total_M);
                            this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sc_j2.Text = Convert.ToString(st.I_J_K);

                        }
                        else if (st.RecordNo == "sc3")
                        {
                            this.sc_a3.Text = st.CompanyIDNo;
                            this.sc_b3.Text = st.CompanyName;
                            this.sc_ca3.Text = st.PlanType;
                            this.sc_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sc_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sc_e3.Value = Convert.ToDouble(st.Price);
                            this.sc_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sc_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sc_h3.Value = Convert.ToInt32(st.NoOfShares);
                            this.sc_l3.Text = Convert.ToString(st.Total_L);
                            this.sc_m3.Text = Convert.ToString(st.Total_M);
                            this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sc_j3.Text = Convert.ToString(st.I_J_K);
                        }
                        decimal x = (Convert.ToDecimal(sc_j1.Text) + Convert.ToDecimal(sc_j2.Text) + Convert.ToDecimal(sc_j3.Text));
                        sc_tj.Text = Convert.ToString(x);

                        decimal l = (Convert.ToDecimal(sc_l1.Text) + Convert.ToDecimal(sc_l2.Text) + Convert.ToDecimal(sc_l3.Text));
                        sc_tl.Text = Convert.ToString(l);
                        decimal m = (Convert.ToDecimal(sc_m1.Text) + Convert.ToDecimal(sc_m2.Text) + Convert.ToDecimal(sc_m3.Text));
                        sc_tm.Text = Convert.ToString(m);

                    }
                    else if (st.Section == "D")
                    {
                        if (st.RecordNo == "sd1")
                        {
                            this.sd_a1.Text = st.CompanyIDNo;
                            this.sd_b1.Text = st.CompanyName;
                            this.sd_ca1.Text = st.PlanType;
                            this.sd_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sd_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sd_e1.Value = Convert.ToDouble(st.Price);
                            this.sd_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sd_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sd_h1.Value = Convert.ToInt32(st.NoOfShares);
                            this.sd_l1.Text = Convert.ToString(st.Total_L);
                            this.sd_m1.Text = Convert.ToString(st.Total_M);
                            this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sd_k1.Text = Convert.ToString(st.I_J_K);

                        }
                        else if (st.RecordNo == "sd2")
                        {
                            this.sd_a2.Text = st.CompanyIDNo;
                            this.sd_b2.Text = st.CompanyName;
                            this.sd_ca2.Text = st.PlanType;
                            this.sd_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sd_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sd_e2.Value = Convert.ToDouble(st.Price);
                            this.sd_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sd_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sd_h2.Value = Convert.ToInt32(st.NoOfShares);
                            this.sd_l2.Text = Convert.ToString(st.Total_L);
                            this.sd_m2.Text = Convert.ToString(st.Total_M);
                            this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sd_k2.Text = Convert.ToString(st.I_J_K);

                        }
                        else if (st.RecordNo == "sd3")
                        {
                            this.sd_a3.Text = st.CompanyIDNo;
                            this.sd_b3.Text = st.CompanyName;
                            this.sd_ca3.Text = st.PlanType;
                            this.sd_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                            this.sd_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                            this.sd_e3.Value = Convert.ToDouble(st.Price);
                            this.sd_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                            this.sd_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                            this.sd_h3.Value = Convert.ToInt32(st.NoOfShares);
                            this.sd_l3.Text = Convert.ToString(st.Total_L);
                            this.sd_m3.Text = Convert.ToString(st.Total_M);
                            this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                            this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                            this.sd_k3.Text = Convert.ToString(st.I_J_K);
                        }

                        decimal x = (Convert.ToDecimal(sd_k1.Text) + Convert.ToDecimal(sd_k2.Text) + Convert.ToDecimal(sd_k3.Text));
                        sd_tk.Text = Convert.ToString(x);

                        decimal l = (Convert.ToDecimal(sd_l1.Text) + Convert.ToDecimal(sd_l2.Text) + Convert.ToDecimal(sd_l3.Text));
                        sd_tl.Text = Convert.ToString(l);
                        decimal m = (Convert.ToDecimal(sd_m1.Text) + Convert.ToDecimal(sd_m2.Text) + Convert.ToDecimal(sd_m3.Text));
                        sd_tm.Text = Convert.ToString(m);

                    }
                }
                decimal total_m = (Convert.ToDecimal(sa_tm.Text) + Convert.ToDecimal(sb_tm.Text) + Convert.ToDecimal(sc_tm.Text) + Convert.ToDecimal(sd_tm.Text));
                this.Total.Text = Convert.ToString(total_m);









            }
        }

        private int check_for_AmmendmentB()
        {
            SqlDataReader sqlDr = null;

            int FK_DETAILS = 0;

            string sql = @"SELECT  [ID]
     
  FROM [A8B2009ST]
  where emp_id='" + varEmpCode + "' and AppendixB_year='" + yearCode + "'and IS_AMMENDMENT=1";




            sqlDr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            while (sqlDr.Read())
            {
                FK_DETAILS = Convert.ToInt32(sqlDr["ID"]);
            }

            return FK_DETAILS;
        }



        void update_appendixB_form()
        {
            int FK_DETAILS = 0;

            if (check_for_AmmendmentB() <= 0)
            {
                string sql = "insert into A8B2009ST(emp_id,AppendixB_year,RecordType,IDType,IDNo,NameLine1,NameLine2,Nationality,Sex,DateOfBirth,DateOfIncorporation,IS_AMMENDMENT)" +
"select emp_id,AppendixB_year,RecordType,IDType,IDNo,NameLine1,NameLine2,Nationality,Sex,DateOfBirth,DateOfIncorporation,1" +
"from A8B2009ST   where emp_id='" + varEmpCode + "' and AppendixB_year='" + yearCode + "'and IS_AMMENDMENT=0";

                int result = DataAccess.ExecuteStoreProc(sql);


            }

            FK_DETAILS = check_for_AmmendmentB();

            A8BRECORDDETAILS _sectionA1 = new A8BRECORDDETAILS();

            _sectionA1.CompanyIDNo = sa_a1.Text;
            _sectionA1.CompanyName = sa_b1.Text;
            _sectionA1.PlanType = sa_ca1.SelectedValue.ToString();
            _sectionA1.DateOfGrant = sa_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA1.DateOfExercise = sa_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA1.Price = Convert.ToDecimal(sa_e1.Value);
            _sectionA1.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g1.Value);
            _sectionA1.NoOfShares = Convert.ToInt32(sa_h1.Value);
            _sectionA1.Section = "A";
            _sectionA1.RecordNo = "sa1";
            _sectionA1.FK_ID = FK_DETAILS;
            _sectionA1.NonExemptGrossAmount = _sectionA1.Total_L;
            _sectionA1.GrossAmountGains = _sectionA1.Total_M;











            A8BRECORDDETAILS _sectionA2 = new A8BRECORDDETAILS();

            _sectionA2.CompanyIDNo = sa_a2.Text;
            _sectionA2.CompanyName = sa_b2.Text;
            _sectionA2.PlanType = sa_ca2.SelectedValue.ToString();
            _sectionA2.DateOfGrant = sa_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA2.DateOfExercise = sa_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA2.Price = Convert.ToDecimal(sa_e2.Value);
            _sectionA2.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g2.Value);
            _sectionA2.NoOfShares = Convert.ToInt32(sa_h2.Value);
            _sectionA2.Section = "A";
            _sectionA2.RecordNo = "sa2";
            _sectionA2.FK_ID = FK_DETAILS;
            _sectionA2.NonExemptGrossAmount = _sectionA2.Total_L;
            _sectionA2.GrossAmountGains = _sectionA2.Total_M;



            A8BRECORDDETAILS _sectionA3 = new A8BRECORDDETAILS();

            _sectionA3.CompanyIDNo = sa_a3.Text;
            _sectionA3.CompanyName = sa_b3.Text;
            _sectionA3.PlanType = sa_ca3.SelectedValue.ToString();
            _sectionA3.DateOfGrant = sa_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA3.DateOfExercise = sa_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA3.Price = Convert.ToDecimal(sa_e3.Value);
            _sectionA3.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g3.Value);
            _sectionA3.NoOfShares = Convert.ToInt32(sa_h3.Value);
            _sectionA3.Section = "A";
            _sectionA3.RecordNo = "sa3";
            _sectionA3.FK_ID = FK_DETAILS;

            _sectionA3.NonExemptGrossAmount = _sectionA3.Total_L;
            _sectionA3.GrossAmountGains = _sectionA3.Total_M;


            _sectionA1.G_Total_L = _sectionA1.Total_L + _sectionA2.Total_L + _sectionA3.Total_L;
            _sectionA1.G_Total_M = _sectionA1.Total_M + _sectionA2.Total_M + _sectionA3.Total_M;


            _sectionA2.G_Total_L = _sectionA1.Total_L + _sectionA2.Total_L + _sectionA3.Total_L;
            _sectionA2.G_Total_M = _sectionA1.Total_M + _sectionA2.Total_M + _sectionA3.Total_M;

            _sectionA3.G_Total_L = _sectionA1.Total_L + _sectionA2.Total_L + _sectionA3.Total_L;
            _sectionA3.G_Total_M = _sectionA1.Total_M + _sectionA2.Total_M + _sectionA3.Total_M;





            A8BRECORDDETAILS _sectionB1 = new A8BRECORDDETAILS();

            _sectionB1.CompanyIDNo = sb_a1.Text;
            _sectionB1.CompanyName = sb_b1.Text;
            _sectionB1.PlanType = sb_ca1.SelectedValue.ToString();
            _sectionB1.DateOfGrant = sb_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB1.DateOfExercise = sb_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB1.Price = Convert.ToDecimal(sb_e1.Value);
            _sectionB1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f1.Value);
            _sectionB1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g1.Value);
            _sectionB1.NoOfShares = Convert.ToInt32(sb_h1.Value);
            _sectionB1.Section = "B";
            _sectionB1.RecordNo = "sb1";
            _sectionB1.FK_ID = FK_DETAILS;
            _sectionB1.NonExemptGrossAmount = _sectionB1.Total_L;
            _sectionB1.GrossAmountGains = _sectionB1.Total_M;


            A8BRECORDDETAILS _sectionB2 = new A8BRECORDDETAILS();

            _sectionB2.CompanyIDNo = sb_a2.Text;
            _sectionB2.CompanyName = sb_b2.Text;
            _sectionB2.PlanType = sb_ca2.SelectedValue.ToString();
            _sectionB2.DateOfGrant = sb_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB2.DateOfExercise = sb_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB2.Price = Convert.ToDecimal(sb_e2.Value);
            _sectionB2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f2.Value);
            _sectionB2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g2.Value);
            _sectionB2.NoOfShares = Convert.ToInt32(sb_h2.Value);
            _sectionB2.Section = "B";
            _sectionB2.RecordNo = "sb2";
            _sectionB2.FK_ID = FK_DETAILS;

            _sectionB2.NonExemptGrossAmount = _sectionB2.Total_L;
            _sectionB2.GrossAmountGains = _sectionB2.Total_M;



            A8BRECORDDETAILS _sectionB3 = new A8BRECORDDETAILS();
            _sectionB3.CompanyIDNo = sb_a3.Text;
            _sectionB3.CompanyName = sb_b3.Text;
            _sectionB3.PlanType = sb_ca3.SelectedValue.ToString();
            _sectionB3.DateOfGrant = sb_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB3.DateOfExercise = sb_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB3.Price = Convert.ToDecimal(sb_e3.Value);
            _sectionB3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f3.Value);
            _sectionB3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g3.Value);
            _sectionB3.NoOfShares = Convert.ToInt32(sb_h3.Value);
            _sectionB3.Section = "B";
            _sectionB3.RecordNo = "sb3";
            _sectionB3.FK_ID = FK_DETAILS;
            _sectionB3.NonExemptGrossAmount = _sectionB3.Total_L;
            _sectionB3.GrossAmountGains = _sectionB3.Total_M;




            _sectionB1.G_Total_I = _sectionB1.I_J_K + _sectionB2.I_J_K + _sectionB3.I_J_K;

            _sectionB1.G_Total_L = _sectionB1.Total_L + _sectionB2.Total_L + _sectionB3.Total_L;
            _sectionB1.G_Total_M = _sectionB1.Total_M + _sectionB2.Total_M + _sectionB3.Total_M;

            _sectionB2.G_Total_I = _sectionB1.I_J_K + _sectionB2.I_J_K + _sectionB3.I_J_K;

            _sectionB2.G_Total_L = _sectionB1.Total_L + _sectionB2.Total_L + _sectionB3.Total_L;
            _sectionB2.G_Total_M = _sectionB1.Total_M + _sectionB2.Total_M + _sectionB3.Total_M;


            _sectionB3.G_Total_I = _sectionB1.I_J_K + _sectionB2.I_J_K + _sectionB3.I_J_K;

            _sectionB3.G_Total_L = _sectionB1.Total_L + _sectionB2.Total_L + _sectionB3.Total_L;
            _sectionB3.G_Total_M = _sectionB1.Total_M + _sectionB2.Total_M + _sectionB3.Total_M;






            A8BRECORDDETAILS _sectionC1 = new A8BRECORDDETAILS();

            _sectionC1.CompanyIDNo = sc_a1.Text;
            _sectionC1.CompanyName = sc_b1.Text;
            _sectionC1.PlanType = sc_ca1.SelectedValue.ToString();
            _sectionC1.DateOfGrant = sc_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC1.DateOfExercise = sc_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC1.Price = Convert.ToDecimal(sc_e1.Value);
            _sectionC1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f1.Value);
            _sectionC1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g1.Value);
            _sectionC1.NoOfShares = Convert.ToInt32(sc_h1.Value);
            _sectionC1.Section = "C";
            _sectionC1.RecordNo = "sc1";
            _sectionC1.FK_ID = FK_DETAILS;

            _sectionC1.NonExemptGrossAmount = _sectionC1.Total_L;
            _sectionC1.GrossAmountGains = _sectionC1.Total_M;


            A8BRECORDDETAILS _sectionC2 = new A8BRECORDDETAILS();

            _sectionC2.CompanyIDNo = sc_a2.Text;
            _sectionC2.CompanyName = sc_b2.Text;
            _sectionC2.PlanType = sc_ca2.SelectedValue.ToString();
            _sectionC2.DateOfGrant = sc_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC2.DateOfExercise = sc_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC2.Price = Convert.ToDecimal(sc_e2.Value);
            _sectionC2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f2.Value);
            _sectionC2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g2.Value);
            _sectionC2.NoOfShares = Convert.ToInt32(sc_h2.Value);
            _sectionC2.Section = "C";
            _sectionC2.RecordNo = "sc2";
            _sectionC2.FK_ID = FK_DETAILS;

            _sectionC2.NonExemptGrossAmount = _sectionC2.Total_L;
            _sectionC2.GrossAmountGains = _sectionC2.Total_M;

            A8BRECORDDETAILS _sectionC3 = new A8BRECORDDETAILS();

            _sectionC3.CompanyIDNo = sc_a3.Text;
            _sectionC3.CompanyName = sc_b3.Text;
            _sectionC3.PlanType = sc_ca3.SelectedValue.ToString();
            _sectionC3.DateOfGrant = sc_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC3.DateOfExercise = sc_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC3.Price = Convert.ToDecimal(sc_e3.Value);
            _sectionC3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f3.Value);
            _sectionC3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g3.Value);
            _sectionC3.NoOfShares = Convert.ToInt32(sc_h3.Value);
            _sectionC3.Section = "C";
            _sectionC3.RecordNo = "sc3";
            _sectionC3.FK_ID = FK_DETAILS;


            _sectionC3.NonExemptGrossAmount = _sectionC3.Total_L;
            _sectionC3.GrossAmountGains = _sectionC3.Total_M;

            _sectionC1.G_Total_J = _sectionC1.I_J_K + _sectionC2.I_J_K + _sectionC3.I_J_K;


            _sectionC1.G_Total_L = _sectionC1.Total_L + _sectionC2.Total_L + _sectionC3.Total_L;
            _sectionC1.G_Total_M = _sectionC1.Total_M + _sectionC2.Total_M + _sectionC3.Total_M;

            _sectionC2.G_Total_J = _sectionC1.I_J_K + _sectionC2.I_J_K + _sectionC3.I_J_K;


            _sectionC2.G_Total_L = _sectionC1.Total_L + _sectionC2.Total_L + _sectionC3.Total_L;
            _sectionC2.G_Total_M = _sectionC1.Total_M + _sectionC2.Total_M + _sectionC3.Total_M;

            _sectionC3.G_Total_J = _sectionC1.I_J_K + _sectionC2.I_J_K + _sectionC3.I_J_K;


            _sectionC3.G_Total_L = _sectionC1.Total_L + _sectionC2.Total_L + _sectionC3.Total_L;
            _sectionC3.G_Total_M = _sectionC1.Total_M + _sectionC2.Total_M + _sectionC3.Total_M;



            A8BRECORDDETAILS _sectionD1 = new A8BRECORDDETAILS();

            _sectionD1.CompanyIDNo = sd_a1.Text;
            _sectionD1.CompanyName = sd_b1.Text;
            _sectionD1.PlanType = sd_ca1.SelectedValue.ToString();
            _sectionD1.DateOfGrant = sd_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD1.DateOfExercise = sd_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD1.Price = Convert.ToDecimal(sd_e1.Value);
            _sectionD1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f1.Value);
            _sectionD1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g1.Value);
            _sectionD1.NoOfShares = Convert.ToInt32(sd_h1.Value);
            _sectionD1.Section = "D";
            _sectionD1.RecordNo = "sd1";
            _sectionD1.FK_ID = FK_DETAILS;


            _sectionD1.NonExemptGrossAmount = _sectionD1.Total_L;
            _sectionD1.GrossAmountGains = _sectionD1.Total_M;





            A8BRECORDDETAILS _sectionD2 = new A8BRECORDDETAILS();

            _sectionD2.CompanyIDNo = sd_a2.Text;
            _sectionD2.CompanyName = sd_b2.Text;
            _sectionD2.PlanType = sd_ca2.SelectedValue.ToString();
            _sectionD2.DateOfGrant = sd_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD2.DateOfExercise = sd_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD2.Price = Convert.ToDecimal(sd_e2.Value);
            _sectionD2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f2.Value);
            _sectionD2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g2.Value);
            _sectionD2.NoOfShares = Convert.ToInt32(sd_h2.Value);
            _sectionD2.Section = "D";
            _sectionD2.RecordNo = "sd2";
            _sectionD2.FK_ID = FK_DETAILS;

            _sectionD2.NonExemptGrossAmount = _sectionD2.Total_L;
            _sectionD2.GrossAmountGains = _sectionD2.Total_M;


            A8BRECORDDETAILS _sectionD3 = new A8BRECORDDETAILS();

            _sectionD3.CompanyIDNo = sd_a3.Text;
            _sectionD3.CompanyName = sd_b3.Text;
            _sectionD3.PlanType = sd_ca3.SelectedValue.ToString();
            _sectionD3.DateOfGrant = sd_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD3.DateOfExercise = sd_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD3.Price = Convert.ToDecimal(sd_e3.Value);
            _sectionD3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f3.Value);
            _sectionD3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g3.Value);
            _sectionD3.NoOfShares = Convert.ToInt32(sd_h3.Value);
            _sectionD3.Section = "D";
            _sectionD3.RecordNo = "sd3";
            _sectionD3.FK_ID = FK_DETAILS;

            _sectionD3.NonExemptGrossAmount = _sectionD3.Total_L;
            _sectionD3.GrossAmountGains = _sectionD3.Total_M;



            _sectionD1.G_Total_K = _sectionD1.I_J_K + _sectionD2.I_J_K + _sectionD3.I_J_K;


            _sectionD1.G_Total_L = _sectionD1.Total_L + _sectionD2.Total_L + _sectionD3.Total_L;
            _sectionD1.G_Total_M = _sectionD1.Total_M + _sectionD2.Total_M + _sectionD3.Total_M;


            _sectionD2.G_Total_K = _sectionD1.I_J_K + _sectionD2.I_J_K + _sectionD3.I_J_K;


            _sectionD2.G_Total_L = _sectionD1.Total_L + _sectionD2.Total_L + _sectionD3.Total_L;
            _sectionD2.G_Total_M = _sectionD1.Total_M + _sectionD2.Total_M + _sectionD3.Total_M;

            _sectionD3.G_Total_K = _sectionD1.I_J_K + _sectionD2.I_J_K + _sectionD3.I_J_K;


            _sectionD3.G_Total_L = _sectionD1.Total_L + _sectionD2.Total_L + _sectionD3.Total_L;
            _sectionD3.G_Total_M = _sectionD1.Total_M + _sectionD2.Total_M + _sectionD3.Total_M;


            IList<A8BRECORDDETAILS> detailsupdateList = new List<A8BRECORDDETAILS>();

            detailsupdateList.Add(_sectionA1);
            detailsupdateList.Add(_sectionA2);
            detailsupdateList.Add(_sectionA3);

            detailsupdateList.Add(_sectionB1);
            detailsupdateList.Add(_sectionB2);
            detailsupdateList.Add(_sectionB3);

            detailsupdateList.Add(_sectionC1);
            detailsupdateList.Add(_sectionC2);
            detailsupdateList.Add(_sectionC3);

            detailsupdateList.Add(_sectionD1);
            detailsupdateList.Add(_sectionD2);
            detailsupdateList.Add(_sectionD3);


            decimal total_m = (Convert.ToDecimal(sa_tm.Text) + Convert.ToDecimal(sb_tm.Text) + Convert.ToDecimal(sc_tm.Text) + Convert.ToDecimal(sd_tm.Text));


            foreach (A8BRECORDDETAILS update in detailsupdateList)
            {

                if (this.DateOfIncorporation.SelectedDate.HasValue)
                {
                    if (update.Section == "D")
                    {

                        string sql_appendixB = "update A8B2009ST set DateOfIncorporation='" + this.DateOfIncorporation.SelectedDate.Value.ToString() + "'where ID=" + update.FK_ID;

                        int Update_result = DataAccess.ExecuteStoreProc(sql_appendixB);
                    }
                }


                try
                {
                    string appendixB_update = @"UPDATE A8BRECORDDETAILS SET" +
              "[CompanyIDType]='" + update.CompanyIDType + "',[CompanyIDNo]='" +
              update.CompanyIDNo + "',[CompanyName]='" + update.CompanyName + "',[PlanType]='"
              + update.PlanType + "'     ,[DateOfGrant]='" + update.DateOfGrant + "'      ,[DateOfExercise]='" +
              update.DateOfExercise + "'     ,[Price]='" + update.Price + " '     ,[OpenMarketValueAtDateOfGrant]='"
              + update.OpenMarketValueAtDateOfGrant + "'      ,[OpenMarketValueAtDateOfExercise]='" +
              update.OpenMarketValueAtDateOfExercise + "'    ,[NoOfShares]=" + update.NoOfShares + "     ,[NonExemptGrossAmount]='"
              + update.NonExemptGrossAmount + " '   ,[GrossAmountGains]='" + update.GrossAmountGains + "'    ,[FK_A8A2009ST]='" +
              update.FK_ID + " '     ,[Section]= '" + update.Section + " '    ,[Total_i]='" +
              update.I_J_K + "  '    ,[Total_j]='" + update.I_J_K + " '     ,[Total_k]='" +
              update.I_J_K + " '    ,[Total_l]=" + update.Total_L
              + ",[Total_m]='" + update.Total_M
              + "' ,[RecordNo]='" + update.RecordNo
              + "' ,[G_Total]='" + total_m
              + "' ,[G_Total_I]='" + update.G_Total_I
              + "' ,[G_Total_J]='" + update.G_Total_J
              + "' ,[G_Total_K]='" + update.G_Total_K
              + "' ,[G_Total_L]='" + update.G_Total_L
              + "' ,[G_Total_M]='" + update.G_Total_M
              + "'where [FK_A8A2009ST]='" + update.FK_ID
              + "' and [RecordNo]='" + update.RecordNo
              + "'IF @@ROWCOUNT=0 " + @"INSERT INTO  A8BRECORDDETAILS VALUES ('" +
                update.CompanyIDType + "','" +
                update.CompanyIDNo + "','" + update.CompanyName + "','" +
    update.PlanType + "','" +
     update.DateOfGrant + "','"
   + update.DateOfExercise + "','"
   + update.Price + "','"
     + update.OpenMarketValueAtDateOfGrant + "','"
   + update.OpenMarketValueAtDateOfExercise + "','"
  + update.NoOfShares + "','"
     + update.NonExemptGrossAmount + "','"
    + update.GrossAmountGains + "','"
    + update.FK_ID + "','"
    + update.Section + "','"
     + update.Total_I + "','"
     + update.Total_J + "','"
     + update.Total_K + "','"
     + update.Total_L + "','"
   + update.Total_M + "','"
     + update.RecordNo + "','"
       + update.G_Total + "','"
       + update.G_Total_I + "','"

       + update.G_Total_J + "','"
       + update.G_Total_K + "','"
       + update.G_Total_L + "','"
                      + update.G_Total_M + "' )";



                    int result = DataAccess.ExecuteStoreProc(appendixB_update);

                }
                catch (Exception ex)
                {

                    throw ex;
                }





            }
        }
        protected void ButtonCALCULATE_Click(object sender, EventArgs e)
        {
            calculateAppendixB();
        }
        private void calculateAppendixB()
        {
            IList<A8BRECORDDETAILS> detailsupdateList = new List<A8BRECORDDETAILS>();

            A8BRECORDDETAILS _sectionA1 = new A8BRECORDDETAILS();

            _sectionA1.CompanyIDNo = sa_a1.Text;
            _sectionA1.CompanyName = sa_b1.Text;
            _sectionA1.PlanType = sa_ca1.SelectedValue.ToString();
            _sectionA1.DateOfGrant = sa_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA1.DateOfExercise = sa_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA1.Price = Convert.ToDecimal(sa_e1.Value);
            _sectionA1.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g1.Value);
            _sectionA1.NoOfShares = Convert.ToInt32(sa_h1.Value);
            _sectionA1.Section = "A";
            _sectionA1.RecordNo = "sa1";



            A8BRECORDDETAILS _sectionA2 = new A8BRECORDDETAILS();

            _sectionA2.CompanyIDNo = sa_a2.Text;
            _sectionA2.CompanyName = sa_b2.Text;
            _sectionA2.PlanType = sa_ca2.SelectedValue.ToString();
            _sectionA2.DateOfGrant = sa_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA2.DateOfExercise = sa_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA2.Price = Convert.ToDecimal(sa_e2.Value);
            _sectionA2.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g2.Value);
            _sectionA2.NoOfShares = Convert.ToInt32(sa_h2.Value);
            _sectionA2.Section = "A";
            _sectionA2.RecordNo = "sa2";



            A8BRECORDDETAILS _sectionA3 = new A8BRECORDDETAILS();

            _sectionA3.CompanyIDNo = sa_a3.Text;
            _sectionA3.CompanyName = sa_b3.Text;
            _sectionA3.PlanType = sa_ca3.SelectedValue.ToString();
            _sectionA3.DateOfGrant = sa_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA3.DateOfExercise = sa_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionA3.Price = Convert.ToDecimal(sa_e3.Value);
            _sectionA3.OpenMarketValueAtDateOfGrant = 0.00m;
            _sectionA3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sa_g3.Value);
            _sectionA3.NoOfShares = Convert.ToInt32(sa_h3.Value);
            _sectionA3.Section = "A";
            _sectionA3.RecordNo = "sa3";





            _sectionA1.G_Total_L = _sectionA1.Total_L + _sectionA2.Total_L + _sectionA3.Total_L;
            _sectionA1.G_Total_M = _sectionA1.Total_M + _sectionA2.Total_M + _sectionA3.Total_M;






            A8BRECORDDETAILS _sectionB1 = new A8BRECORDDETAILS();

            _sectionB1.CompanyIDNo = sb_a1.Text;
            _sectionB1.CompanyName = sb_b1.Text;
            _sectionB1.PlanType = sb_ca1.SelectedValue.ToString();
            _sectionB1.DateOfGrant = sb_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB1.DateOfExercise = sb_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB1.Price = Convert.ToDecimal(sb_e1.Value);
            _sectionB1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f1.Value);
            _sectionB1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g1.Value);
            _sectionB1.NoOfShares = Convert.ToInt32(sb_h1.Value);
            _sectionB1.Section = "B";
            _sectionB1.RecordNo = "sb1";




            A8BRECORDDETAILS _sectionB2 = new A8BRECORDDETAILS();

            _sectionB2.CompanyIDNo = sb_a2.Text;
            _sectionB2.CompanyName = sb_b2.Text;
            _sectionB2.PlanType = sb_ca2.SelectedValue.ToString();
            _sectionB2.DateOfGrant = sb_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB2.DateOfExercise = sb_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB2.Price = Convert.ToDecimal(sb_e2.Value);
            _sectionB2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f2.Value);
            _sectionB2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g2.Value);
            _sectionB2.NoOfShares = Convert.ToInt32(sb_h2.Value);
            _sectionB2.Section = "B";
            _sectionB2.RecordNo = "sb2";




            A8BRECORDDETAILS _sectionB3 = new A8BRECORDDETAILS();
            _sectionB3.CompanyIDNo = sb_a3.Text;
            _sectionB3.CompanyName = sb_b3.Text;
            _sectionB3.PlanType = sb_ca3.SelectedValue.ToString();
            _sectionB3.DateOfGrant = sb_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB3.DateOfExercise = sb_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionB3.Price = Convert.ToDecimal(sb_e3.Value);
            _sectionB3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sb_f3.Value);
            _sectionB3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sb_g3.Value);
            _sectionB3.NoOfShares = Convert.ToInt32(sb_h3.Value);
            _sectionB3.Section = "B";
            _sectionB3.RecordNo = "sb3";



            _sectionB1.G_Total_I = _sectionB1.I_J_K + _sectionB2.I_J_K + _sectionB3.I_J_K;

            _sectionB1.G_Total_L = _sectionB1.Total_L + _sectionB2.Total_L + _sectionB3.Total_L;
            _sectionB1.G_Total_M = _sectionB1.Total_M + _sectionB2.Total_M + _sectionB3.Total_M;






            A8BRECORDDETAILS _sectionC1 = new A8BRECORDDETAILS();

            _sectionC1.CompanyIDNo = sc_a1.Text;
            _sectionC1.CompanyName = sc_b1.Text;
            _sectionC1.PlanType = sc_ca1.SelectedValue.ToString();
            _sectionC1.DateOfGrant = sc_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC1.DateOfExercise = sc_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC1.Price = Convert.ToDecimal(sc_e1.Value);
            _sectionC1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f1.Value);
            _sectionC1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g1.Value);
            _sectionC1.NoOfShares = Convert.ToInt32(sc_h1.Value);
            _sectionC1.Section = "C";
            _sectionC1.RecordNo = "sc1";



            A8BRECORDDETAILS _sectionC2 = new A8BRECORDDETAILS();

            _sectionC2.CompanyIDNo = sc_a2.Text;
            _sectionC2.CompanyName = sc_b2.Text;
            _sectionC2.PlanType = sc_ca2.SelectedValue.ToString();
            _sectionC2.DateOfGrant = sc_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC2.DateOfExercise = sc_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC2.Price = Convert.ToDecimal(sc_e2.Value);
            _sectionC2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f2.Value);
            _sectionC2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g2.Value);
            _sectionC2.NoOfShares = Convert.ToInt32(sc_h2.Value);
            _sectionC2.Section = "C";
            _sectionC2.RecordNo = "sc2";




            A8BRECORDDETAILS _sectionC3 = new A8BRECORDDETAILS();

            _sectionC3.CompanyIDNo = sc_a3.Text;
            _sectionC3.CompanyName = sc_b3.Text;
            _sectionC3.PlanType = sc_ca3.SelectedValue.ToString();
            _sectionC3.DateOfGrant = sc_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC3.DateOfExercise = sc_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionC3.Price = Convert.ToDecimal(sc_e3.Value);
            _sectionC3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sc_f3.Value);
            _sectionC3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sc_g3.Value);
            _sectionC3.NoOfShares = Convert.ToInt32(sc_h3.Value);
            _sectionC3.Section = "C";
            _sectionC3.RecordNo = "sc3";


            _sectionC1.G_Total_J = _sectionC1.I_J_K + _sectionC2.I_J_K + _sectionC3.I_J_K;


            _sectionC1.G_Total_L = _sectionC1.Total_L + _sectionC2.Total_L + _sectionC3.Total_L;
            _sectionC1.G_Total_M = _sectionC1.Total_M + _sectionC2.Total_M + _sectionC3.Total_M;



            A8BRECORDDETAILS _sectionD1 = new A8BRECORDDETAILS();

            _sectionD1.CompanyIDNo = sd_a1.Text;
            _sectionD1.CompanyName = sd_b1.Text;
            _sectionD1.PlanType = sd_ca1.SelectedValue.ToString();
            _sectionD1.DateOfGrant = sd_cb1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD1.DateOfExercise = sd_d1.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD1.Price = Convert.ToDecimal(sd_e1.Value);
            _sectionD1.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f1.Value);
            _sectionD1.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g1.Value);
            _sectionD1.NoOfShares = Convert.ToInt32(sd_h1.Value);
            _sectionD1.Section = "D";
            _sectionD1.RecordNo = "sd1";








            A8BRECORDDETAILS _sectionD2 = new A8BRECORDDETAILS();

            _sectionD2.CompanyIDNo = sd_a2.Text;
            _sectionD2.CompanyName = sd_b2.Text;
            _sectionD2.PlanType = sd_ca2.SelectedValue.ToString();
            _sectionD2.DateOfGrant = sd_cb2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD2.DateOfExercise = sd_d2.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD2.Price = Convert.ToDecimal(sd_e2.Value);
            _sectionD2.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f2.Value);
            _sectionD2.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g2.Value);
            _sectionD2.NoOfShares = Convert.ToInt32(sd_h2.Value);
            _sectionD2.Section = "D";
            _sectionD2.RecordNo = "sd2";



            A8BRECORDDETAILS _sectionD3 = new A8BRECORDDETAILS();

            _sectionD3.CompanyIDNo = sd_a3.Text;
            _sectionD3.CompanyName = sd_b3.Text;
            _sectionD3.PlanType = sd_ca3.SelectedValue.ToString();
            _sectionD3.DateOfGrant = sd_cb3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD3.DateOfExercise = sd_d3.SelectedDate.Value.ToString("yyyyMMdd");
            _sectionD3.Price = Convert.ToDecimal(sd_e3.Value);
            _sectionD3.OpenMarketValueAtDateOfGrant = Convert.ToDecimal(sd_f3.Value);
            _sectionD3.OpenMarketValueAtDateOfExercise = Convert.ToDecimal(sd_g3.Value);
            _sectionD3.NoOfShares = Convert.ToInt32(sd_h3.Value);
            _sectionD3.Section = "D";
            _sectionD3.RecordNo = "sd3";



            _sectionD1.G_Total_K = _sectionD1.I_J_K + _sectionD2.I_J_K + _sectionD3.I_J_K;


            _sectionD1.G_Total_L = _sectionD1.Total_L + _sectionD2.Total_L + _sectionD3.Total_L;
            _sectionD1.G_Total_M = _sectionD1.Total_M + _sectionD2.Total_M + _sectionD3.Total_M;






            detailsupdateList.Add(_sectionA1);
            detailsupdateList.Add(_sectionA2);
            detailsupdateList.Add(_sectionA3);

            detailsupdateList.Add(_sectionB1);
            detailsupdateList.Add(_sectionB2);
            detailsupdateList.Add(_sectionB3);

            detailsupdateList.Add(_sectionC1);
            detailsupdateList.Add(_sectionC2);
            detailsupdateList.Add(_sectionC3);

            detailsupdateList.Add(_sectionD1);
            detailsupdateList.Add(_sectionD2);
            detailsupdateList.Add(_sectionD3);

            decimal total_m = (Convert.ToDecimal(sa_tm.Text) + Convert.ToDecimal(sb_tm.Text) + Convert.ToDecimal(sc_tm.Text) + Convert.ToDecimal(sd_tm.Text));



            foreach (A8BRECORDDETAILS st in detailsupdateList)
            {

                if (st.Section == "A")
                {
                    if (st.RecordNo == "sa1")
                    {
                        this.sa_a1.Text = st.CompanyIDNo;
                        this.sa_b1.Text = st.CompanyName;
                        this.sa_ca1.Text = st.PlanType;
                        this.sa_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sa_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sa_e1.Value = Convert.ToDouble(st.Price);
                        this.sa_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sa_h1.Value = Convert.ToInt32(st.NoOfShares);
                        this.sa_l1.Text = Convert.ToString(st.Total_L);
                        this.sa_m1.Text = Convert.ToString(st.Total_M);


                    }
                    else if (st.RecordNo == "sa2")
                    {
                        this.sa_a2.Text = st.CompanyIDNo;
                        this.sa_b2.Text = st.CompanyName;
                        this.sa_ca2.Text = st.PlanType;
                        this.sa_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sa_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sa_e2.Value = Convert.ToDouble(st.Price);
                        this.sa_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sa_h2.Value = Convert.ToInt32(st.NoOfShares);
                        this.sa_l2.Text = Convert.ToString(st.Total_L);
                        this.sa_m2.Text = Convert.ToString(st.Total_M);

                    }
                    else if (st.RecordNo == "sa3")
                    {
                        this.sa_a3.Text = st.CompanyIDNo;
                        this.sa_b3.Text = st.CompanyName;
                        this.sa_ca3.Text = st.PlanType;
                        this.sa_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sa_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sa_e3.Value = Convert.ToDouble(st.Price);
                        this.sa_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sa_h3.Value = Convert.ToInt32(st.NoOfShares);
                        this.sa_l3.Text = Convert.ToString(st.Total_L);
                        this.sa_m3.Text = Convert.ToString(st.Total_M);

                    }
                    decimal l = (Convert.ToDecimal(sa_l1.Text) + Convert.ToDecimal(sa_l2.Text) + Convert.ToDecimal(sa_l3.Text));
                    sa_tl.Text = Convert.ToString(l);
                    decimal m = (Convert.ToDecimal(sa_m1.Text) + Convert.ToDecimal(sa_m2.Text) + Convert.ToDecimal(sa_m3.Text));
                    sa_tm.Text = Convert.ToString(m);

                }
                else if (st.Section == "B")
                {
                    if (st.RecordNo == "sb1")
                    {
                        this.sb_a1.Text = st.CompanyIDNo;
                        this.sb_b1.Text = st.CompanyName;
                        this.sb_ca1.Text = st.PlanType;
                        this.sb_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sb_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sb_e1.Value = Convert.ToDouble(st.Price);
                        this.sb_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sb_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sb_h1.Value = Convert.ToInt32(st.NoOfShares);
                        this.sb_l1.Text = Convert.ToString(st.Total_L);
                        this.sb_m1.Text = Convert.ToString(st.Total_M);
                        this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sb_i1.Text = Convert.ToString(st.I_J_K);
                    }
                    else if (st.RecordNo == "sb2")
                    {
                        this.sb_a2.Text = st.CompanyIDNo;
                        this.sb_b2.Text = st.CompanyName;
                        this.sb_ca2.Text = st.PlanType;
                        this.sb_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sb_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sb_e2.Value = Convert.ToDouble(st.Price);
                        this.sb_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sb_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sb_h2.Value = Convert.ToInt32(st.NoOfShares);
                        this.sb_l2.Text = Convert.ToString(st.Total_L);
                        this.sb_m2.Text = Convert.ToString(st.Total_M);
                        this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sb_i2.Text = Convert.ToString(st.I_J_K);
                    }
                    else if (st.RecordNo == "sb3")
                    {
                        this.sb_a3.Text = st.CompanyIDNo;
                        this.sb_b3.Text = st.CompanyName;
                        this.sb_ca3.Text = st.PlanType;
                        this.sb_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sb_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sb_e3.Value = Convert.ToDouble(st.Price);
                        this.sb_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sb_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sb_h3.Value = Convert.ToInt32(st.NoOfShares);
                        this.sb_l3.Text = Convert.ToString(st.Total_L);
                        this.sb_m3.Text = Convert.ToString(st.Total_M);
                        this.sb_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sb_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sb_i3.Text = Convert.ToString(st.I_J_K);
                    }
                    decimal x = (Convert.ToDecimal(sb_i1.Text) + Convert.ToDecimal(sb_i2.Text) + Convert.ToDecimal(sb_i3.Text));
                    sb_ti.Text = Convert.ToString(x);

                    decimal l = (Convert.ToDecimal(sb_l1.Text) + Convert.ToDecimal(sb_l2.Text) + Convert.ToDecimal(sb_l3.Text));
                    sb_tl.Text = Convert.ToString(l);
                    decimal m = (Convert.ToDecimal(sb_m1.Text) + Convert.ToDecimal(sb_m2.Text) + Convert.ToDecimal(sb_m3.Text));
                    sb_tm.Text = Convert.ToString(m);




                }
                else if (st.Section == "C")
                {
                    if (st.RecordNo == "sc1")
                    {
                        this.sc_a1.Text = st.CompanyIDNo;
                        this.sc_b1.Text = st.CompanyName;
                        this.sc_ca1.Text = st.PlanType;
                        this.sc_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sc_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sc_e1.Value = Convert.ToDouble(st.Price);
                        this.sc_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sc_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sc_h1.Value = Convert.ToInt32(st.NoOfShares);
                        this.sc_l1.Text = Convert.ToString(st.Total_L);
                        this.sc_m1.Text = Convert.ToString(st.Total_M);
                        this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sc_j1.Text = Convert.ToString(st.I_J_K);
                    }
                    else if (st.RecordNo == "sc2")
                    {
                        this.sc_a2.Text = st.CompanyIDNo;
                        this.sc_b2.Text = st.CompanyName;
                        this.sc_ca2.Text = st.PlanType;
                        this.sc_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sc_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sc_e2.Value = Convert.ToDouble(st.Price);
                        this.sc_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sc_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sc_h2.Value = Convert.ToInt32(st.NoOfShares);
                        this.sc_l2.Text = Convert.ToString(st.Total_L);
                        this.sc_m2.Text = Convert.ToString(st.Total_M);
                        this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sc_j2.Text = Convert.ToString(st.I_J_K);

                    }
                    else if (st.RecordNo == "sc3")
                    {
                        this.sc_a3.Text = st.CompanyIDNo;
                        this.sc_b3.Text = st.CompanyName;
                        this.sc_ca3.Text = st.PlanType;
                        this.sc_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sc_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sc_e3.Value = Convert.ToDouble(st.Price);
                        this.sc_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sc_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sc_h3.Value = Convert.ToInt32(st.NoOfShares);
                        this.sc_l3.Text = Convert.ToString(st.Total_L);
                        this.sc_m3.Text = Convert.ToString(st.Total_M);
                        this.sc_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sc_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sc_j3.Text = Convert.ToString(st.I_J_K);
                    }
                    decimal x = (Convert.ToDecimal(sc_j1.Text) + Convert.ToDecimal(sc_j2.Text) + Convert.ToDecimal(sc_j3.Text));
                    sc_tj.Text = Convert.ToString(x);

                    decimal l = (Convert.ToDecimal(sc_l1.Text) + Convert.ToDecimal(sc_l2.Text) + Convert.ToDecimal(sc_l3.Text));
                    sc_tl.Text = Convert.ToString(l);
                    decimal m = (Convert.ToDecimal(sc_m1.Text) + Convert.ToDecimal(sc_m2.Text) + Convert.ToDecimal(sc_m3.Text));
                    sc_tm.Text = Convert.ToString(m);

                }
                else if (st.Section == "D")
                {
                    if (st.RecordNo == "sd1")
                    {
                        this.sd_a1.Text = st.CompanyIDNo;
                        this.sd_b1.Text = st.CompanyName;
                        this.sd_ca1.Text = st.PlanType;
                        this.sd_cb1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sd_d1.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sd_e1.Value = Convert.ToDouble(st.Price);
                        this.sd_f1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sd_g1.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sd_h1.Value = Convert.ToInt32(st.NoOfShares);
                        this.sd_l1.Text = Convert.ToString(st.Total_L);
                        this.sd_m1.Text = Convert.ToString(st.Total_M);
                        this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sd_k1.Text = Convert.ToString(st.I_J_K);

                    }
                    else if (st.RecordNo == "sd2")
                    {
                        this.sd_a2.Text = st.CompanyIDNo;
                        this.sd_b2.Text = st.CompanyName;
                        this.sd_ca2.Text = st.PlanType;
                        this.sd_cb2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sd_d2.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sd_e2.Value = Convert.ToDouble(st.Price);
                        this.sd_f2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sd_g2.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sd_h2.Value = Convert.ToInt32(st.NoOfShares);
                        this.sd_l2.Text = Convert.ToString(st.Total_L);
                        this.sd_m2.Text = Convert.ToString(st.Total_M);
                        this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sd_k2.Text = Convert.ToString(st.I_J_K);

                    }
                    else if (st.RecordNo == "sd3")
                    {
                        this.sd_a3.Text = st.CompanyIDNo;
                        this.sd_b3.Text = st.CompanyName;
                        this.sd_ca3.Text = st.PlanType;
                        this.sd_cb3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfGrant.Substring(0, 4)), Convert.ToInt32(st.DateOfGrant.Substring(4, 2)), Convert.ToInt32(st.DateOfGrant.Substring(6, 2)));
                        this.sd_d3.SelectedDate = new DateTime(Convert.ToInt32(st.DateOfExercise.Substring(0, 4)), Convert.ToInt32(st.DateOfExercise.Substring(4, 2)), Convert.ToInt32(st.DateOfExercise.Substring(6, 2)));
                        this.sd_e3.Value = Convert.ToDouble(st.Price);
                        this.sd_f3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfGrant);
                        this.sd_g3.Value = Convert.ToDouble(st.OpenMarketValueAtDateOfExercise);
                        this.sd_h3.Value = Convert.ToInt32(st.NoOfShares);
                        this.sd_l3.Text = Convert.ToString(st.Total_L);
                        this.sd_m3.Text = Convert.ToString(st.Total_M);
                        this.sd_tl.Text = Convert.ToString(st.G_Total_L);
                        this.sd_tm.Text = Convert.ToString(st.G_Total_M);
                        this.sd_k3.Text = Convert.ToString(st.I_J_K);
                    }

                    decimal x = (Convert.ToDecimal(sd_k1.Text) + Convert.ToDecimal(sd_k2.Text) + Convert.ToDecimal(sd_k3.Text));
                    sd_tk.Text = Convert.ToString(x);

                    decimal l = (Convert.ToDecimal(sd_l1.Text) + Convert.ToDecimal(sd_l2.Text) + Convert.ToDecimal(sd_l3.Text));
                    sd_tl.Text = Convert.ToString(l);
                    decimal m = (Convert.ToDecimal(sd_m1.Text) + Convert.ToDecimal(sd_m2.Text) + Convert.ToDecimal(sd_m3.Text));
                    sd_tm.Text = Convert.ToString(m);

                }
            }
            decimal total = (Convert.ToDecimal(sa_tm.Text) + Convert.ToDecimal(sb_tm.Text) + Convert.ToDecimal(sc_tm.Text) + Convert.ToDecimal(sd_tm.Text));
            this.Total.Text = Convert.ToString(total);


        }


    }
}