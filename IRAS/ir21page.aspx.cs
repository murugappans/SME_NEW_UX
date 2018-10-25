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

namespace IRAS
{
    public partial class ir21page : System.Web.UI.Page
    {
        string varEmpCode = "";
        string yearCode = "";
        string Emp_name = "";
        string Companyname = "";
        string NricNo = "";
        string compid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            varEmpCode = Request.QueryString["empcode"].ToString();
            yearCode = Request.QueryString["year"].ToString();

            btnsave.ServerClick += new EventHandler(btnsave_ServerClick);
           // calbut.Click  += new EventHandler(calbut_Click);


            if (!this.IsPostBack)
            {
                init_textbox();
                fill_ir21_appendix1();
                fill_ir21_appendix2();
                fill_ir21_appendix3();
                getForm21_details();
            }
        }
        protected void calbut_Click(object sender, EventArgs e)
        {
            
            if (tbsEmp.SelectedTab.Text == "FORM IR21")
            {
                calculate_ir21_form();

            }
            else if (tbsEmp.SelectedTab.Text == "APPENDIX 1")
            {
                calculate_ir21_app1();
            }
            else if (tbsEmp.SelectedTab.Text == "APPENDIX 2")
            {
                calculate_ir21_app2();
            }
            else if (tbsEmp.SelectedTab.Text == "APPENDIX 3")
            {
               // ir21_app3save();
            }





        }
        public void getForm21_details()
        {

            Control[] allControls = helper.FlattenHierachy(FORM_IR21_Panel);
            TextBox t;
            foreach (Control control in allControls)
            {

                if (control.GetType() == typeof(TextBox) && (control.ID != "txtEmpname" && control.ID != "txtfin"))
                {
                    t = new TextBox();
                    t = (TextBox)control;
                   // t.Text = "0";

                }
            }
            
            //---------------
            DataSet ds_nationality = new DataSet();
            ds_nationality = DataAccess.FetchRS( CommandType.Text ,"select Id Nationality_ID ,Nationality from Nationality order by 2",null);
            txt_spnationality.DataSource = ds_nationality.Tables[0];
            txt_spnationality.DataTextField = ds_nationality.Tables[0].Columns["Nationality"].ColumnName.ToString();
            txt_spnationality.DataValueField = ds_nationality.Tables[0].Columns["Nationality_ID"].ColumnName.ToString();
            txt_spnationality.DataBind();
            txt_spnationality.Items.Insert(0, "-select-");
            //--------
            
           string  sql = "select Company_name,company.address cadd1,company.address2 unit,company.postal_code,emp_name+' '+emp_lname ename ,ic_pp_number,[date_of_birth],sex,[nationality_id],marital_status,employee.email,[joining_date],[termination_date],[desig_id],[giro_bank],cpf_ref_no,auth_person,designation,wp_arrival_date from company,employee where company.Company_Id=employee.Company_Id and employee.emp_code=" + varEmpCode;
           SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            while (dr.Read())
            {
                txt_cname.Text = dr["Company_name"].ToString();
                txt_stname.Text = dr["cadd1"].ToString();
                txt_unit.Text = dr["unit"].ToString();
                txt_pcode.Text = dr["postal_code"].ToString();
                txt_ename.Text = dr["ename"].ToString();
                txt_fin.Text = dr["ic_pp_number"].ToString();
                txt_comtaxref.Text = dr["cpf_ref_no"].ToString();
                txt_ename_income.Text = dr["ename"].ToString();
                txt_fin_income.Text = dr["ic_pp_number"].ToString();

                tEmpname.Text = dr["ename"].ToString();
                tfin.Text  = dr["ic_pp_number"].ToString();

                txtEmpname.Text = dr["ename"].ToString();
                txtfin.Text = dr["ic_pp_number"].ToString();

                ename2.Text = dr["ename"].ToString();
                fin2.Text = dr["ic_pp_number"].ToString();

                txtauthname.Text = dr["auth_person"].ToString();
                TextBox4 .Text= dr["designation"].ToString();
                f21_aname.Text = dr["auth_person"].ToString();
                f21_design.Text = dr["designation"].ToString();
                TextBox7.Text = dr["auth_person"].ToString();
                TextBox8.Text = dr["designation"].ToString();
                txtdesign.Text   = dr["designation"].ToString();
                txtauthoris.Text = dr["auth_person"].ToString();
                if (dr["wp_arrival_date"].ToString().Length >= 10)
                {
                    txt_datearrival.SelectedDate =Convert.ToDateTime ( dr["wp_arrival_date"].ToString());
                }

                DateTime d1 = Convert.ToDateTime(dr["date_of_birth"].ToString());
                txt_dob.SelectedDate = Convert.ToDateTime(dr["date_of_birth"].ToString());
                //txt_dob.Text = dr["date_of_birth"].ToString(); 
               int iryear=Convert.ToInt16( HttpContext.Current.Session["IR8AYEAR"].ToString());
               if (Convert.ToDateTime(dr["joining_date"]).Year == DateTime.Now.Year)
               {
                   txt_fdate_yoc1.SelectedDate = Convert.ToDateTime(dr["joining_date"]);
                   txt_tdate_yoc1.SelectedDate = Convert.ToDateTime(dr["termination_date"]);
                   
               }
               else
               {
                   txt_fdate_yoc1.SelectedDate = new DateTime(iryear + 1, 1, 1);
                   txt_tdate_yoc1.SelectedDate = Convert.ToDateTime(dr["termination_date"]);
                   if (Convert.ToDateTime(dr["joining_date"]).Year < (DateTime.Now.Year - 1))
                   {
                       txt_fdate_yoc2.SelectedDate = new DateTime(iryear, 1, 1);
                       txt_tdate_yoc2.SelectedDate = new DateTime(iryear, 12, 31);
                   }
                   else if (Convert.ToDateTime(dr["joining_date"]).Year == (DateTime.Now.Year - 1))
                   {
                       txt_fdate_yoc2.SelectedDate = Convert.ToDateTime(dr["joining_date"]);
                       txt_tdate_yoc2.SelectedDate = new DateTime(iryear, 12, 31);
                   }
                   
               }
             

              
                dp_empgender.SelectedValue = dr["sex"].ToString();

                 string sql1 = "select nationality from Nationality where id=" + dr["nationality_id"].ToString();
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr1.Read())
                {
                    txt_nationality.Text = dr1["Nationality"].ToString();
                }
                
                if (dr["marital_status"].ToString() == "S") {
                    txt_martial.Text = "SINGLE";
                }
                else if (dr["marital_status"].ToString() == "M")
                {
                    txt_martial.Text = "MARRIED";
                }

                
                txt_email.Text = dr["email"].ToString();
                txt_datecommence.SelectedDate = Convert.ToDateTime(dr["joining_date"].ToString());
                if (dr["termination_date"].ToString().Length != 0)
                {
                    txt_terminate.SelectedDate = Convert.ToDateTime(dr["termination_date"].ToString());
                }
                 sql1 = "select designation from designation where id=" + dr["desig_id"].ToString();
                dr1 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr1.Read()) {
                    txt_designation.Text = dr1[0].ToString();
                }
                //---------------------- children details
                sql1 = "select * from Family where emp_id=" + varEmpCode +" and Relation=2";
                SqlDataReader dr2 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                int i = 0;
                string s = "";
                while (dr2.Read())
                {
                    i++;
                    if (i == 1)
                    {
                        txt_chilename1.Text = dr2["Name"].ToString();
                        dp_gender1.SelectedValue = dr2["Sex"].ToString();
                        if (dr2["dateofbirth"].ToString() != "NULL" && dr2["dateofbirth"].ToString().Length > 0)
                        {
                            txt_cdob1.SelectedDate = Convert.ToDateTime(dr2["dateofbirth"].ToString());
                        }

                    }
                    if (i == 2)
                    {
                        txt_chilename2.Text = dr2["Name"].ToString();
                        dp_gender2.SelectedValue = dr2["Sex"].ToString();
                        if (dr2["dateofbirth"].ToString() != "NULL" && dr2["dateofbirth"].ToString().Length > 0)
                        {
                            txt_cdob2.SelectedDate = Convert.ToDateTime(dr2["dateofbirth"].ToString());
                        }

                    }
                    if (i == 3)
                    {
                        txt_chilename3.Text = dr2["Name"].ToString();
                        dp_gender3.SelectedValue = dr2["Sex"].ToString();
                        if (dr2["dateofbirth"].ToString() != "NULL" && dr2["dateofbirth"].ToString().Length > 0)
                        {
                            txt_cdob3.SelectedDate = Convert.ToDateTime(dr2["dateofbirth"].ToString());
                        }

                    }
                    if (i == 4)
                    {
                        txt_chilename4.Text = dr2["Name"].ToString();
                        dp_gender4.SelectedValue = dr2["Sex"].ToString();
                        if (dr2["dateofbirth"].ToString() != "NULL" && dr2["dateofbirth"].ToString().Length > 0)
                        {
                            txt_cdob4.SelectedDate = Convert.ToDateTime(dr2["dateofbirth"].ToString());
                        }

                    }
                }

                //--------------------spouse details
                sql1 = "select * from Family where emp_id=" + varEmpCode + " and Relation=1";
                SqlDataReader dr3 = DataAccess.ExecuteReader(CommandType.Text, sql1, null);
                if (dr3.Read())
                {
                    txt_spname.Text = dr3["Name"].ToString();
                    if (dr3["dateofbirth"].ToString().Length >= 10)
                    {
                        txt_spdob.SelectedDate = Convert.ToDateTime (dr3["dateofbirth"].ToString());

                    }
                    txt_spid.Text = dr3["UIDN"].ToString();
                    if (dr3["Marriage_Date"].ToString().Length >= 10)
                    {
                        txt_spmarrydate.SelectedDate = Convert.ToDateTime (dr3["Marriage_Date"].ToString());
                    }

                }
                //-------------
                call_pro();

            }


            //-------------
           
            sql = "select * from FIR21  where emp_code='" + varEmpCode + "'";

            dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            
            if (dr.Read())
            {
                //Call_ir21_table();
                string s = dr["type_of_form"].ToString();
                if (s == "O")
                {
                    chk_original.Checked = true;

                }
                else if (s == "A")
                {
                    chk_additional.Checked = true;
                }
                else if (s == "M")
                {
                    chk_amended.Checked = true;
                }
                if (dr["addtional_date"].ToString().Length >= 10)
                {

                    txt_additional_date.SelectedDate = Convert.ToDateTime(dr["addtional_date"].ToString());
                }
                if (dr["amended_date"].ToString().Length>=10) {
                    txt_amended_date.SelectedDate = Convert.ToDateTime(dr["amended_date"].ToString());
                }
                txt_others.Text = dr["otherreason_details"].ToString();

                txt_comtaxref.Text = dr["com_taxref"].ToString();
                txt_cname.Text = dr["com_name"].ToString();
                txt_hno.Text = dr["com_hseno"].ToString();
                txt_unit.Text = dr["com_unit"].ToString();
                txt_stname.Text = dr["com_street"].ToString();
                txt_pcode.Text = dr["pcode"].ToString();
                txt_ename.Text = dr["emp_name"].ToString();
                txt_nric.Text = dr["emp_nric"].ToString();
                txt_fin.Text = dr["emp_fin"].ToString();
                txt_malic.Text = dr["emp_malic"].ToString();
                txt_designation.Text = dr["designation"].ToString();
                if (dr["emp_dob"].ToString().Length >=10)
                {
                    txt_dob.SelectedDate = Convert.ToDateTime(dr["emp_dob"].ToString());
                }
                dp_empgender.SelectedValue = dr["emp_gender"].ToString();

                txt_nationality.Text = dr["emp_natioality"].ToString();
                txt_contact.Text = dr["emp_contact"].ToString();
                txt_email.Text = dr["emp_email"].ToString();
                if (dr["emp_arrival"].ToString().Length != 0)
                {
                    txt_datearrival.SelectedDate = Convert.ToDateTime(dr["emp_arrival"].ToString());
                }
                if (dr["emp_commence"].ToString().Length != 0)
                {
                    txt_datecommence.SelectedDate = Convert.ToDateTime(dr["emp_commence"].ToString());
                }
                if (dr["emp_cessation"].ToString().Length != 0)
                {
                    txt_dateposting.SelectedDate = Convert.ToDateTime(dr["emp_cessation"].ToString());
                }
                if (dr["emp_depature"].ToString().Length != 0)
                {
                    txt_depature.SelectedDate = Convert.ToDateTime(dr["emp_depature"].ToString());
                }
                if (dr["emp_terminate"].ToString().Length != 0)
                {
                    txt_terminate.SelectedDate = Convert.ToDateTime(dr["emp_terminate"].ToString());
                }

                if (dr["reason_lessthanmonth"].ToString() == "L")
                {
                    chk_left.Checked = true;
                }
                else if (dr["reason_lessthanmonth"].ToString() == "S")
                {
                    chk_shot.Checked = true;
                }
                else if (dr["reason_lessthanmonth"].ToString() == "W")
                {
                    chk_whilst.Checked = true;
                }
                else if (dr["reason_lessthanmonth"].ToString() == "O")
                {
                    chk_others.Checked = true;
                }
              txt_others .Text  = dr["otherreason_details"].ToString();

                txt_pendingtax.Text = dr["pending_amt"].ToString();
                if (dr["withhold_money"].ToString() == "Y")
                {
                    chk_withoutyes.Checked = true;

                }
                else
                {
                    chk_withoutno.Checked = true;
                }

                if (dr["reason_withhold"].ToString() == "R")
                {
                    chk_afterpayday.Checked = true;
                }
                else if (dr["reason_withhold"].ToString() == "N")
                {
                     
                    chk_notreturn .Checked =true ;
                }
                else if (dr["reason_withhold"].ToString() == "P")
                {

                    chk_paid.Checked = true;
                }
                else if (dr["reason_withhold"].ToString() == "W")
                {

                    chk_owes.Checked = true;
                }
                else if (dr["reason_withhold"].ToString() == "O")
                {

                    chk_others.Checked = true;
                }
                txt_otherdetails.Text = dr["reason_with_hold_other_details"].ToString();
                //txt_reasondetils
                if (dr["lastsalary_date"].ToString().Length >= 10)
                {
                    txt_lastpaid.SelectedDate = Convert.ToDateTime(dr["lastsalary_date"].ToString());
                }
                txt_lastamt.Text = dr["lastsalary_amt"].ToString();
                txt_periodsalary.Text = dr["period_lastsalary"].ToString();
                txt_bkname.Text = dr["emp_bank"].ToString();
                txt_employername.Text = dr["name_newemployer"].ToString();
                txt_spname.Text = dr["spouse_name"].ToString();
                
                if (dr["spouse_dob"].ToString().Length >= 10)
                {
                    txt_spdob.SelectedDate = Convert.ToDateTime(dr["spouse_dob"].ToString());
                }
                txt_spid.Text = dr["spouse_id"].ToString();
                if (dr["marriage_date"].ToString().Length >= 10)
                {
                    txt_spmarrydate.SelectedDate = Convert.ToDateTime(dr["marriage_date"].ToString());
                }
                txt_spnationality.SelectedItem.Text =dr["spouse_nationality"].ToString();
               
                if (dr["spouse_income4000"].ToString() == "Y")
                {
                    chk_incomeyes.Checked = true;
                }
                else
                {
                    chk_incomeyes.Checked = false;
                }
                txt_spemployername.Text = dr["spouse_employer_det"].ToString();
                if (dr["child1_name"].ToString().Length > 1)
                {
                    txt_chilename1.Text = dr["child1_name"].ToString();
                    if (dr["child1_dob"].ToString().Length >= 10)
                    {
                        txt_cdob1.SelectedDate = Convert.ToDateTime(dr["child1_dob"].ToString());
                    }
                    dp_gender1.SelectedValue = dr["child1_gender"].ToString();
                    txt_cschool1.Text = dr["child1_school"].ToString();
                }
                else {
                    txt_chilename1.Text = "";
                    txt_cschool1.Text = "";
                }
                if (dr["child2_name"].ToString().Length > 1)
                {
                    txt_chilename2.Text = dr["child2_name"].ToString();
                    if (dr["child2_dob"].ToString().Length >= 10)
                    {
                        txt_cdob2.SelectedDate = Convert.ToDateTime(dr["child2_dob"].ToString());
                    }
                    dp_gender2.SelectedValue = dr["child2_gender"].ToString();
                    txt_cschool2.Text = dr["child2_school"].ToString();
                }
                else
                {
                    txt_chilename2.Text = "";
                    txt_cschool2.Text = "";
                }
                if (dr["child3_name"].ToString().Length > 1)
                {
                    txt_chilename3.Text = dr["child3_name"].ToString();
                    if (dr["child3_dob"].ToString().Length >= 10)
                    {
                        txt_cdob3.SelectedDate = Convert.ToDateTime(dr["child3_dob"].ToString());
                    }
                    dp_gender3.SelectedValue = dr["child3_gender"].ToString();
                    txt_cschool3.Text = dr["child3_school"].ToString();
                }
                else
                {
                    txt_chilename3.Text = "";
                    txt_cschool3.Text = "";
                }
                if (dr["child4_name"].ToString().Length > 1)
                {
                    txt_chilename4.Text = dr["child4_name"].ToString();
                    if (dr["child4_dob"].ToString().Length >= 10)
                    {
                        txt_cdob4.SelectedDate = Convert.ToDateTime(dr["child4_dob"].ToString());
                    }
                    dp_gender4.SelectedValue = dr["child4_gender"].ToString();
                    txt_cschool4.Text = dr["child4_school"].ToString();
                }
                else
                {
                    txt_chilename4.Text = "";
                    txt_cschool4.Text = "";
                }
                if (dr["prior_year_of_cesssation_from"].ToString().Length >= 10)
                {

                    txt_fdate_yoc1.SelectedDate = Convert.ToDateTime(dr["prior_year_of_cesssation_from"].ToString());
                }
                if (dr["year_of_cesssation_from"].ToString().Length >= 10)
                {
                    txt_fdate_yoc2.SelectedDate = Convert.ToDateTime(dr["year_of_cesssation_from"].ToString());
                }
                if (dr["prior_year_of_cesssation_to"].ToString().Length >= 10)
                {
                    txt_tdate_yoc1.SelectedDate = Convert.ToDateTime(dr["prior_year_of_cesssation_to"].ToString());
                }
                if (dr["year_of_cesssation_to"].ToString().Length >= 10)
                {
                    txt_tdate_yoc2.SelectedDate = Convert.ToDateTime(dr["year_of_cesssation_to"].ToString());
                }

                txt_gsal1.Text =Convert.ToInt32(dr["gsalary1"]).ToString();
                txt_gsal2.Text = Convert.ToInt32(dr["gsalary2"]).ToString();

                txt_bonus1.Text = Convert.ToInt32(dr["bonus1"]).ToString();
                txt_bonus2.Text = Convert.ToInt32(dr["bonus1"]).ToString();

                txt_nbonus1.Text =Convert.ToInt32( dr["nbonus1"]).ToString ();
                txt_nbonus2.Text =Convert.ToInt32( dr["nbonus2"]).ToString();

                if (dr["state_date1"].ToString().Length >= 10)
                {
                    txt_state_date1.SelectedDate = Convert.ToDateTime(dr["state_date1"]);
                }
                if (dr["state_date2"].ToString().Length >= 10)
                {
                    txt_state_date2.SelectedDate = Convert.ToDateTime(dr["state_date2"]);
                }

                txt_direct_fees1.Text =Convert.ToInt32( dr["director_fee1"]).ToString();
                txt_direct_fees2.Text =Convert.ToInt32( dr["director_fee2"]).ToString();

                if (dr["app_date1"].ToString().Length >= 10)
                {
                    txt_app_date1.SelectedDate =Convert.ToDateTime(dr["app_date1"]);
                }
                if (dr["app_date2"].ToString().Length >= 10)
                {
                    txt_app_date2.SelectedDate  =Convert.ToDateTime(dr["app_date2"]);
                }

                txt_ogcomm1.Text = Convert.ToInt32(dr["other_gcomm1"]).ToString();
                txt_gcomm2.Text = Convert.ToInt32(dr["other_gcomm2"]).ToString();

                txt_oallowance1.Text =Convert.ToInt32( dr["other_allowance1"]).ToString();
                txt_oallowance2.Text =Convert.ToInt32( dr["other_allowance2"]).ToString();

                //txt_gcomm

                txt_gratuity1.Text = Convert.ToInt32(dr["gratuity1"]).ToString();
                txt_gratuity2.Text = Convert.ToInt32(dr["gratuity2"]).ToString();

                txt_noticepay1.Text = Convert.ToInt32(dr["notice_pay1"]).ToString();
                txt_noticepay2.Text = Convert.ToInt32(dr["notice_pay2"]).ToString();

                txt_compensation1.Text = Convert.ToInt32(dr["compensation1"]).ToString();
                txt_compensation2.Text = Convert.ToInt32(dr["compensation2"]).ToString();
                txt_compreasion.Text = dr["compensation_reason"].ToString();

                txt_retamt1.Text = Convert.ToInt32(dr["ret_fund1"]).ToString();
                txt_retamt2.Text = Convert.ToInt32(dr["ret_fund2"]).ToString();
                txt_retname.Text = dr["ret_fundname"].ToString();

                txt_conamt1.Text = Convert.ToInt32(dr["cont_fund1"]).ToString();
                txt_conamt2.Text = Convert.ToInt32(dr["cont_fund2"]).ToString();
                txt_conname.Text = dr["cont_fundname"].ToString();

                txt_excess1.Text = Convert.ToInt32(dr["excess1"]).ToString();
                txt_excess2.Text = Convert.ToInt32(dr["excess2"]).ToString();

                txt_totalgross1.Text = Convert.ToInt32(dr["tot_gross1"]).ToString();
                txt_totalgross2.Text = Convert.ToInt32(dr["tot_gross2"]).ToString();

                if (dr["esop_before"].ToString() == "Y")
                {
                    chk_esopbefore.Checked = true;
                }
                if (dr["esop_after"].ToString() == "Y")
                {
                    chk_esopafter.Checked = true;
                }

                if (dr["a1completed"].ToString() == "Y")
                {
                    chk_a1completed.Checked = true;

                }
                txt_benefit1.Text = Convert.ToInt32(dr["benefit1"]).ToString();
                txt_benefit2.Text = Convert.ToInt32(dr["benefit2"]).ToString();

                txt_benefit_subtotal1.Text = Convert.ToInt32(dr["benefit_subtot1"]).ToString();
                txt_benefit_subtotal2.Text = Convert.ToInt32(dr["benefit_subtot2"]).ToString();

                txt_totalitem1.Text = Convert.ToInt32(dr["tot_item1"]).ToString();
                txt_totalitem2.Text = Convert.ToInt32(dr["tot_item2"]).ToString();

                txt_ded_fundname.Text = dr["ded_fundname"].ToString();
                txt_ded_fundamt1.Text = Convert.ToInt32(dr["ded_fundamt1"]).ToString();
                txt_ded_fundamt2.Text = Convert.ToInt32(dr["ded_fundamt2"]).ToString();

                txt_donationamt1.Text = Convert.ToInt32(dr["ded_donationamt1"]).ToString();
                txt_donationamt2.Text = Convert.ToInt32(dr["ded_donationamt2"]).ToString();

                txt_contamt1.Text = Convert.ToInt32(dr["ded_contamt1"]).ToString();
                txt_contamt2.Text = Convert.ToInt32(dr["ded_contamt2"]).ToString();

                f21_aname.Text = dr["auth_name"].ToString();
                f21_design.Text = dr["design"].ToString();
                if (dr["date"].ToString().Length >= 10)
                {
                    f21_date.SelectedDate = Convert.ToDateTime(dr["date"].ToString());
                }
                f21_contper.Text = dr["cont_person"].ToString();
                f21_contno.Text = dr["cont_no"].ToString();
                f21_fax.Text = dr["fax"].ToString();
                f21_email.Text = dr["email"].ToString();

                calculate_ir21_form();

            }
            else
            {

                
            }

            
        }

        void fill_ir21_appendix1()
        {
            Control[] allControls = helper.FlattenHierachy(IR21_APP1_Panel);
            TextBox t;
            foreach (Control control in allControls)
            {

                if (control.GetType() == typeof(TextBox) && (control.ID != "txtEmpname" && control.ID != "txtfin"))
                {
                    t = new TextBox();
                    t = (TextBox)control;
                    t.Text = "0";

                }
            }
            txtdate.SelectedDate = DateTime.Now.Date;
            string sql = "select * from IR21_Appendix1 where empcode='" + varEmpCode + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {

                txtadder1.Text = dr["elyadder1"].ToString();
                txtadder2.Text = dr["elyadder2"].ToString();
                //   txtfrom1.Text = dr["periodoccfrom1"].ToString();
                //  txtfrom2.Text = dr["periodoccfrom2"].ToString();
                txtfin.Text = dr["fin"].ToString();
                txtEmpname.Text = dr["empname"].ToString();
                if (dr["periodoccfrom1"].ToString().Length >= 10)
                {
                    txtprefrom1.SelectedDate = Convert.ToDateTime(dr["periodoccfrom1"]);
                }
                if (dr["periodoccfrom2"].ToString().Length >= 10)
                {
                    txtprefrom2.SelectedDate = Convert.ToDateTime(dr["periodoccfrom2"]);
                }
                if (dr["periodoccto1"].ToString().Length >= 10)
                {
                    txtpreto1 .SelectedDate = Convert.ToDateTime( dr["periodoccto1"].ToString());
                   
                }
                if (dr["periodoccto2"].ToString().Length >= 10)
                {
                    txtpreto2.SelectedDate = Convert.ToDateTime(dr["periodoccto2"].ToString());
                }
                txtnumday1.Text = dr["numberday1"].ToString();
                txtnumday2.Text = dr["numberday2"].ToString();
                txtnumemp1.Text = dr["numbeeremp1"].ToString();
                txtnumemp2.Text = dr["numbeeremp2"].ToString();
                txtrent1.Text = dr["rent1"].ToString();
                txtrent2.Text = dr["rent2"].ToString();
                txtres1.Text = dr["residence1"].ToString();
                txtres2.Text = dr["residence2"].ToString();
                txtannu1.Text = dr["atotal1"].ToString();
                txtannu2.Text = dr["atotal2"].ToString();

                txtacctA1.Text = dr["atotal1"].ToString();
                txtacctA2.Text = dr["atotal2"].ToString();

                txtFurnUnit.Text = dr["furnitureunit"].ToString();
                txtFurn1.Text = dr["furniturevalu1"].ToString();
                txtFurn2.Text = dr["furniturevalu2"].ToString();
                txtRefUnit1.Text = dr["refrunit"].ToString();
                txtRef1.Text = dr["refrvalu1"].ToString();
                txtRef2.Text = dr["refrvalu2"].ToString();
                txtwasunit1.Text = dr["washinunit1"].ToString();
                txtwasunit2.Text = dr["washinunit2"].ToString();
                txtwasunit3.Text = dr["washinunit3"].ToString();
                txtwas1.Text = dr["washinvalu1"].ToString();
                txtwas2.Text = dr["washinvalu2"].ToString();
                txtAirUnit.Text = dr["airunit"].ToString();
                txtAir1.Text = dr["airvalu1"].ToString();
                txtAir2.Text = dr["airvalu2"].ToString();
                txtDiningunit1.Text = dr["diningunit1"].ToString();
                txtDiningunit2.Text = dr["diningunit2"].ToString();
                txtDining1.Text = dr["dining1"].ToString();
                txtDining2.Text = dr["dining2"].ToString();
                

                txtAddromUnit1.Text = dr["addroomunit"].ToString();
                txtAddrom1.Text = dr["airvalu1"].ToString();
                txtAddrom1.Text = dr["airvalu1"].ToString();

                txtTVunit1.Text = dr["tvunit"].ToString();
                txtRadiounit1.Text = dr["radiounit"].ToString();
                txtAmplunit1.Text = dr["ampunit"].ToString();
                txtHifiunit1.Text = dr["hifiunit"].ToString();
                txtGuitunit1.Text = dr["guitarunit"].ToString();
                txtTV1.Text = dr["tvvalu1"].ToString();
                txtTV2.Text = dr["tvvalu2"].ToString();

                txtComUnit1.Text = dr["computerunit1"].ToString();
                txtComUnit2.Text = dr["computerunit2"].ToString();
                txtCom1.Text = dr["computervalu1"].ToString();
                txtCom2.Text = dr["computervalu2"].ToString();

                txtswimunit1.Text = dr["swimmingunit"].ToString();
                txtswim1.Text = dr["swimmingvalu1"].ToString();
                txtswim2.Text = dr["swimmingvalu2"].ToString();
                //  txtoth1.Text=dr["othersvalu1"].ToString();
                //  txtoth2.Text=dr["othersvalu2"].ToString();

                txttaxtot1.Text = dr["btotal1"].ToString();
                txttaxtot2.Text = dr["btotal2"].ToString();

                txtpub1.Text = dr["pubvalu1"].ToString();
                txtpub2.Text = dr["pubvalu2"].ToString();

                TextBox1.Text = dr["drivvalu1"].ToString();
                txtdriv2.Text = dr["drivvalu2"].ToString();

                txtgardener1.Text = dr["gardernvalu1"].ToString();
                txtgardener2.Text = dr["gardernvalu2"].ToString();

                txttaxpubtot1.Text = dr["b1total1"].ToString();
                txttaxpubtot2.Text = dr["b1total2"].ToString();

                txtselfA.Text = dr["selfaunit"].ToString();
                txtselfC.Text = dr["selfcunit"].ToString();
                txtselfC1.Text = dr["selfvalu1"].ToString();
                txtselfC2.Text = dr["selfvalu2"].ToString();

                txtwifeA.Text = dr["wifeaunit"].ToString();
                txtwifeC.Text = dr["wifecunit"].ToString();
                txtwifeC1.Text = dr["wifevalu1"].ToString();
                txtwifeC2.Text = dr["wifevalu2"].ToString();

                txtChil8A.Text = dr["child8aunit"].ToString();
                txtChil8C.Text = dr["child8aunit"].ToString();

                txtChil8C1.Text = dr["child8valu1"].ToString();
                txtChil8C2.Text = dr["child8valu2"].ToString();

                txtchil3A.Text = dr["child3aunit"].ToString();
                txtchil3C.Text = dr["child3cunit"].ToString();
                txtchil3C1.Text = dr["child3valu1"].ToString();
                txtchil3C2.Text = dr["child3valu2"].ToString();
                txtchilA.Text = dr["childaunit"].ToString();
                txtchilC.Text = dr["childcunit"].ToString();

                txtchilC1.Text = dr["childvalu1"].ToString();
                txtchilC2.Text = dr["childvalu2"].ToString();

                txtHottot1.Text = dr["ctotal1"].ToString();
                txtHottot2.Text = dr["ctotal2"].ToString();

                //----------------------------------------------

                txtAddres1.Text = dr["dresidanceadd1"].ToString();
                txtAddres1.Text = dr["dresidanceadd1"].ToString();

                if (dr["dperiodoccfrom1_1"].ToString().Length >= 10)
                {
                    txtdfrom1.SelectedDate = Convert.ToDateTime(dr["dperiodoccfrom1_1"]);
                }
                if (dr["dperiodoccfrom1_2"].ToString().Length >= 10)
                {
                    txtdfrom2.SelectedDate =Convert.ToDateTime( dr["dperiodoccfrom1_2"]);
                }
                if (dr["dperiodoccto1_1"].ToString().Length >= 10)
                {
                    txtdto1.SelectedDate  =Convert.ToDateTime( dr["dperiodoccto1_1"]);
                }
                if (dr["dperiodoccto1_2"].ToString().Length >= 10)
                {
                    txtdto2.SelectedDate = Convert.ToDateTime(dr["dperiodoccto1_2"]);
                }
                //txtocc1.Text = dr["dperiodoccto1_1"].ToString();
                //txtocc2.Text = dr["dperiodoccto1_2"].ToString();

                //txtnumday1.Text = dr["dnumberdayocc1"].ToString();
                //txtnumday2.Text = dr["dnumberdayocc2"].ToString();

                txtannu1.Text = dr["dannualval1"].ToString();
                txtannu2.Text = dr["dannualval2"].ToString();
                txtfur1.Text = dr["dfurniture1"].ToString();
                txtfur2.Text = dr["dfurniture2"].ToString();

                txtact1.Text = dr["dactualrent1"].ToString();
                txtact2.Text = dr["dactualrent2"].ToString();

                txtdrent1.Text = dr["drentpaid1"].ToString();
                txtdrent2.Text = dr["drentpaid2"].ToString();
                //txtadd2.Text = dr["drentpaid2"].ToString();
                if (dr["dperiodoccfrom2_1"].ToString().Length >= 10)
                {
                    txtperfrom1.SelectedDate =Convert .ToDateTime ( dr["dperiodoccfrom2_1"]);
                }
                if (dr["dperiodoccfrom2_2"].ToString().Length >= 10)
                {
                    txtperfrom2.SelectedDate  =Convert .ToDateTime (  dr["dperiodoccfrom2_2"]);
                }
                if (dr["dperiodoccto2_1"].ToString().Length >= 10)
                {
                    txtperto1.SelectedDate  = Convert .ToDateTime(dr["dperiodoccto2_1"]);
                }
                if (dr["dperiodoccto2_2"].ToString().Length >= 10)
                {
                    txtperto2.SelectedDate  =Convert .ToDateTime(dr["dperiodoccto2_2"]);
                }

                txtoccA1.Text = dr["dnumberdayocc1"].ToString();
                txtoccA2.Text = dr["dnumberdayocc2"].ToString();

                txtAnnA1.Text = dr["dannualvalu1"].ToString();
                txtAnnA2.Text = dr["dannualvalu2"].ToString();

                txtfurA1.Text = dr["dfurniture1_1"].ToString();
                TextBox2.Text = dr["dfurniture1_2"].ToString();

                txtacctA1.Text = dr["dactualrent1_1"].ToString();
                txtacctA2.Text = dr["dactualrent1_2"].ToString();
                txtrentA1.Text = dr["drentpaid1_1"].ToString();
                txtrentA2.Text = dr["drentpaid1_1"].ToString();

              
                txttaxtotA1.Text = dr["dtotald15_1"].ToString();
                txttaxtotA2.Text = dr["dtotald15_2"].ToString();

                txtutilit1.Text = dr["dutillites1"].ToString();
                txtutilit2.Text = dr["dutillites2"].ToString();

               
                txtddriver1.Text = dr["ddrive1"].ToString();
                txtddriver2.Text = dr["ddrive1"].ToString();

                txtServ1.Text = dr["dservant1"].ToString();
                txtServ2.Text = dr["dservant2"].ToString();

                txtDtot1.Text = dr["dtotal1"].ToString();
                txtDtot2.Text = dr["dtotal2"].ToString();


                txthote1.Text = dr["ehotle1"].ToString();
                txthote2.Text = dr["ehotel2"].ToString();

                txtEtax1.Text = dr["etotal1"].ToString();
                txtEtax2.Text = dr["etotal2"].ToString();

                //--------------------------------------------
                txtcost1.Text = dr["fempcost1"].ToString();
                txtcost2.Text = dr["fempcost2"].ToString();

                txtpayment1.Text = dr["finterestpayment1"].ToString();
                txtpayment2.Text = dr["finterestpayment2"].ToString();

                txtinsur1.Text = dr["flifinsurance1"].ToString();
                txtinsur2.Text = dr["flifinsurance2"].ToString();

                txtsub1.Text = dr["ffreesubsidised1"].ToString();
                txtsub2.Text = dr["ffreesubsidised2"].ToString();

                txtEducat1.Text = dr["feducational1"].ToString();
                // txtEducat2.Text = dr["feducational2"].ToString();

                txtmonetary1.Text = dr["fmonetary1"].ToString();
                txtmonetary2.Text = dr["fmonetary2"].ToString();

                txtenterance1.Text = dr["fmonetary1"].ToString();
                txtenterance2.Text = dr["fmonetary2"].ToString();

                txtgains1.Text = dr["fgains1"].ToString();
                txtgains2.Text = dr["fgains2"].ToString();

                txtfullcost1.Text = dr["ffullcost1"].ToString();
                txtfullcost2.Text = dr["ffullcost2"].ToString();

                txtcarbenefit1.Text = dr["fcarbenefit1"].ToString();
                txtcarbenefit2.Text = dr["fcarbenefit2"].ToString();

                others_total1.Text = dr["fotherbenefit1"].ToString();
                others_total2.Text = dr["fotherbenefit2"].ToString();



                txtothbenef1.Text = dr["fotherbenefit1"].ToString();
                txtothbenef2.Text = dr["fotherbenefit2"].ToString();

                // txtFtot1.Text = dr["ftotal1"].ToString();
                txtFtot2.Text = dr["ftotal2"].ToString();

                txtIRTot1.Text = dr["total1"].ToString();
                txtIRTot2.Text = dr["total2"].ToString();


                txtauthname.Text = dr["nameauthorised"].ToString();
                TextBox4.Text = dr["designation"].ToString();
                if(dr["date"].ToString().Length >=12){
                txtdate.SelectedDate  =Convert.ToDateTime( dr["date"].ToString());
                }
                txtconname.Text = dr["contactperson"].ToString();
                txtconno.Text = dr["contactno"].ToString();
                TextBox5.Text = dr["faxno"].ToString();
                TextBox6.Text = dr["emailaddress"].ToString();



                calculate_ir21_app1();
            }

        }
        void fill_ir21_appendix2()
        {
            Control[] allControls = helper.FlattenHierachy(IR21_APP2_Panel21);

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
                if (control.GetType() == typeof(Telerik.WebControls.RadNumericTextBox))
                {
                    Telerik.WebControls.RadNumericTextBox tbox = control as Telerik.WebControls.RadNumericTextBox;

                    if (tbox.Text.Length == 0)
                    {
                        tbox.Text = "0";
                    }


                }
            }

                string sql = "select * from ir21_app2_details2 where empcode='" + varEmpCode + "'";
                SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
                while (dr.Read())
                {

                    ename2.Text = dr["empname"].ToString();
                    fin2.Text = dr["fin"].ToString();
                    txtEmpname.Text = dr["empname"].ToString();
                    txtfin.Text = dr["fin"].ToString();
                    if (dr["Section"].ToString() == "a1")
                    {
                        sa_a121.Text = dr["regno"].ToString();
                        sa_b121.Text = dr["companyname"].ToString();
                        sa_ca121.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe1.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker1.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker2.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox1.Text = dr["Price"].ToString();
                        RadNumericTextBox2.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox3.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "a2")
                    {
                        sa_a221.Text = dr["regno"].ToString();
                        sa_b221.Text = dr["companyname"].ToString();
                        sa_ca221.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe2.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker3.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker4.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox4.Text = dr["Price"].ToString();
                        RadNumericTextBox5.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox6.Text = dr["NoOfShares"].ToString();

                    }
                    else if (dr["Section"].ToString() == "a3")
                    {
                        sa_a321.Text = dr["regno"].ToString();
                        sa_b321.Text = dr["companyname"].ToString();
                        sa_ca321.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe3.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker5.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker6.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox7.Text = dr["Price"].ToString();
                        RadNumericTextBox8.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox9.Text = dr["NoOfShares"].ToString();

                    }
                    //------------------------------b
                    if (dr["Section"].ToString() == "b1")
                    {
                        sb_a121.Text = dr["regno"].ToString();
                        sb_b121.Text = dr["companyname"].ToString();
                        sb_ca121.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe4.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker7.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker8.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox10.Text = dr["Price"].ToString();
                        RadNumericTextBox11.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        RadNumericTextBox12.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox13.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "b2")
                    {
                        sb_a221.Text = dr["regno"].ToString();
                        sb_b221.Text = dr["companyname"].ToString();
                        sb_ca221.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe5.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker9.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker10.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox14.Text = dr["Price"].ToString();
                        RadNumericTextBox15.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        RadNumericTextBox16.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox17.Text = dr["NoOfShares"].ToString();

                    }
                    else if (dr["Section"].ToString() == "b3")
                    {
                        sb_a321.Text = dr["regno"].ToString();
                        sb_b321.Text = dr["companyname"].ToString();
                        sb_ca321.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe6.SelectedItem.Text = dr["exerciseType"].ToString();
                        RadDatePicker11.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        RadDatePicker12.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        RadNumericTextBox18.Text = dr["Price"].ToString();
                        RadNumericTextBox19.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        RadNumericTextBox20.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        RadNumericTextBox21.Text = dr["NoOfShares"].ToString();

                    }
                    //-------------------c
                    if (dr["Section"].ToString() == "c1")
                    {
                        sc_a121.Text = dr["regno"].ToString();
                        sc_b121.Text = dr["companyname"].ToString();
                        sc_ca121.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe7.SelectedItem.Text = dr["exerciseType"].ToString();
                        sc_dp11.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sc_dp12.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sc_e121.Text = dr["Price"].ToString();
                        sc_f121.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sc_g121.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sc_h121.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "c2")
                    {
                        sc_a221.Text = dr["regno"].ToString();
                        sc_b221.Text = dr["companyname"].ToString();
                        sc_ca221.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe8.SelectedItem.Text = dr["exerciseType"].ToString();
                        sc_dp21.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sc_dp22.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sc_e221.Text = dr["Price"].ToString();
                        sc_f221.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sc_g221.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sc_h221.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "c3")
                    {
                        sc_a321.Text = dr["regno"].ToString();
                        sc_b321.Text = dr["companyname"].ToString();
                        sc_ca321.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe9.SelectedItem.Text = dr["exerciseType"].ToString();
                        sc_dp31.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sc_dp32.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sc_e321.Text = dr["Price"].ToString();
                        sc_f321.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sc_g321.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sc_h321.Text = dr["NoOfShares"].ToString();

                    }
                    //--------------------d
                    if (dr["Section"].ToString() == "d1")
                    {
                        sd_a121.Text = dr["regno"].ToString();
                        sd_b121.Text = dr["companyname"].ToString();
                        sd_ca121.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe10.SelectedItem.Text = dr["exerciseType"].ToString();
                        sd_dp11.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sd_dp12.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sd_e121.Text = dr["Price"].ToString();
                        sd_f121.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sd_g121.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sd_h121.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "d2")
                    {
                        sd_a221.Text = dr["regno"].ToString();
                        sd_b221.Text = dr["companyname"].ToString();
                        sd_ca221.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe11.SelectedItem.Text = dr["exerciseType"].ToString();
                        sd_dp21.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sd_dp22.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sd_e221.Text = dr["Price"].ToString();
                        sd_f221.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sd_g221.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sd_h221.Text = dr["NoOfShares"].ToString();
                    }
                    else if (dr["Section"].ToString() == "d3")
                    {
                        sd_a321.Text = dr["regno"].ToString();
                        sd_b321.Text = dr["companyname"].ToString();
                        sd_ca321.SelectedItem.Text = dr["PlanType"].ToString();
                        ddtoe12.SelectedItem.Text = dr["exerciseType"].ToString();
                        sd_dp31.SelectedDate = Convert.ToDateTime(dr["DateOfGrant"].ToString());
                        sd_dp32.SelectedDate = Convert.ToDateTime(dr["DateOfExercise"].ToString());
                        sd_e321.Text = dr["Price"].ToString();
                        sd_f321.Text = dr["OpenMarketValueAtDateOfGrant"].ToString();
                        sd_g321.Text = dr["OpenMarketValueAtDateOfReflected"].ToString();
                        sd_h321.Text = dr["NoOfShares"].ToString();

                    }
                   // calculate_ir21_app2();
                }
            




        }


        void fill_ir21_appendix3()
        {
            Control[] allControls = helper.FlattenHierachy(IR21_APP3_panel);

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
            date1.SelectedDate = DateTime.Now.Date;

            string sql = "select * from fir21_app3 where empcode='" + varEmpCode + "'";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql, null);
            if (dr.Read())
            {
                string str1 = "select ic_pp_number,emp_name +' '+emp_lname ename from employee where emp_code='" + varEmpCode + "'";
                SqlDataReader dr1 = DataAccess.ExecuteReader(CommandType.Text, str1, null);
                if (dr1.Read())
                {
                    tfin.Text = dr1[0].ToString();
                    tEmpname.Text = dr1[1].ToString();

                }
                txtrocA1.Text = dr["rocA1"].ToString();
                txtcmpnameA1.Text = dr["cmpnameA1"].ToString();
                drpA1.SelectedItem.Text = dr["drpA1"].ToString();
                doga1.SelectedDate = Convert.ToDateTime(dr["dogA1"].ToString());
                txtopenmarkA1.Text = dr["openmarkA1"].ToString();
                txtMValueA1.Text = dr["marketvalueA1"].ToString();
                txtexpriceA1.Text = dr["expriceA1"].ToString();
                txtNounexA1.Text = dr["NounexA1"].ToString();
                doea1.SelectedDate = Convert.ToDateTime(dr["doexpA1"].ToString());

                txtrocA2.Text = dr["rocA2"].ToString();
                txtcmpnameA2.Text = dr["cmpnameA2"].ToString();
                drpA2.SelectedItem.Text = dr["drpA2"].ToString();
                doga2.SelectedDate = Convert.ToDateTime(dr["dogA2"].ToString());
                txtopenmarkA2.Text = dr["openmarkA2"].ToString();
                txtMValueA2.Text = dr["marketvalueA2"].ToString();
                txtexpriceA2.Text = dr["expriceA2"].ToString();
                txtNounexA2.Text = dr["NounexA2"].ToString();
                doea2.SelectedDate = Convert.ToDateTime(dr["doexpA2"].ToString());

                txtrocA3.Text = dr["rocA3"].ToString();
                txtcmpnameA3.Text = dr["cmpnameA3"].ToString();
                drpA3.SelectedItem.Text = dr["drpA3"].ToString();
                doga3.SelectedDate = Convert.ToDateTime(dr["dogA3"].ToString());
                txtopenmarkA3.Text = dr["openmarkA3"].ToString();
                txtMValueA3.Text = dr["marketvalueA3"].ToString();
                txtexpriceA3.Text = dr["expriceA3"].ToString();
                txtNounexA3.Text = dr["NounexA3"].ToString();
                doea3.SelectedDate = Convert.ToDateTime(dr["doexpA3"].ToString());


                txtrocA4.Text = dr["rocA4"].ToString();
                txtcmpnameA4.Text = dr["cmpnameA4"].ToString();
                drpA4.SelectedItem.Text = dr["drpA4"].ToString();
                doga4.SelectedDate = Convert.ToDateTime(dr["dogA4"].ToString());
                txtopenmarkA4.Text = dr["openmarkA4"].ToString();
                txtMValueA4.Text = dr["marketvalueA4"].ToString();
                txtexpriceA4.Text = dr["expriceA4"].ToString();
                txtNounexA4.Text = dr["NounexA4"].ToString();
                doea4.SelectedDate = Convert.ToDateTime(dr["doexpA4"].ToString());

                txtrocB1.Text = dr["rocB1"].ToString();
                txtcmpnameB1.Text = dr["cmpnameB1"].ToString();
                drpB1.SelectedItem.Text = dr["drpB1"].ToString();
                dogb1.SelectedDate = Convert.ToDateTime(dr["dogB1"].ToString());
                txtopenmarkB1.Text = dr["openmarkB1"].ToString();
                txtMValueB1.Text = dr["marketvalueB1"].ToString();
                txtexpriceB1.Text = dr["expriceB1"].ToString();
                txtNounexB1.Text = dr["NounexB1"].ToString();
                doeb1.SelectedDate = Convert.ToDateTime(dr["doexpB1"].ToString());

                txtrocB2.Text = dr["rocB2"].ToString();
                txtcmpnameB2.Text = dr["cmpnameB2"].ToString();
                drpB2.SelectedItem.Text = dr["drpB2"].ToString();
                dogb2.SelectedDate = Convert.ToDateTime(dr["dogB2"].ToString());
                txtopenmarkB2.Text = dr["openmarkB2"].ToString();
                txtMValueB2.Text = dr["marketvalueB2"].ToString();
                txtexpriceB2.Text = dr["expriceB2"].ToString();
                txtNounexB2.Text = dr["NounexB2"].ToString();
                doeb2.SelectedDate = Convert.ToDateTime(dr["doexpB2"].ToString());

                txtrocB3.Text = dr["rocB3"].ToString();
                txtcmpnameB3.Text = dr["cmpnameB3"].ToString();
                drpB3.SelectedItem.Text = dr["drpB3"].ToString();
                dogb3.SelectedDate = Convert.ToDateTime(dr["dogB3"].ToString());
                txtopenmarkB3.Text = dr["openmarkB3"].ToString();
                txtMValueB3.Text = dr["marketvalueB3"].ToString();
                txtexpriceB3.Text = dr["expriceB3"].ToString();
                txtNounexB3.Text = dr["NounexB3"].ToString();
                doeb3.SelectedDate = Convert.ToDateTime(dr["doexpB3"].ToString());

                txtrocB4.Text = dr["rocB4"].ToString();
                txtcmpnameB4.Text = dr["cmpnameB4"].ToString();
                drpB4.SelectedItem.Text = dr["drpB4"].ToString();
                dogb4.SelectedDate = Convert.ToDateTime(dr["dogB4"].ToString());
                txtopenmarkB4.Text = dr["openmarkB4"].ToString();
                txtMValueB4.Text = dr["marketvalueB4"].ToString();
                txtexpriceB4.Text = dr["expriceB4"].ToString();
                txtNounexB4.Text = dr["NounexB4"].ToString();
                doeb4.SelectedDate = Convert.ToDateTime(dr["doexpB4"].ToString());

                txtrocc1.Text = dr["rocC1"].ToString();
                txtcmpnameC1.Text = dr["cmpnameC1"].ToString();
                drpC1.SelectedItem.Text = dr["drpC1"].ToString();
                dogc1.SelectedDate = Convert.ToDateTime(dr["dogC1"].ToString());
                txtopenmarkC1.Text = dr["openmarkC1"].ToString();
                txtMValueC1.Text = dr["marketvalueC1"].ToString();
                txtexpriceC1.Text = dr["expriceC1"].ToString();
                txtNounexC1.Text = dr["NounexC1"].ToString();
                doec1.SelectedDate = Convert.ToDateTime(dr["doexpC1"].ToString());

                txtrocc2.Text = dr["rocC2"].ToString();
                txtcmpnameC2.Text = dr["cmpnameC2"].ToString();
                drpC2.SelectedItem.Text = dr["drpC2"].ToString();
                dogc2.SelectedDate = Convert.ToDateTime(dr["dogC2"].ToString());
                txtopenmarkC2.Text = dr["openmarkC2"].ToString();
                txtMValueC2.Text = dr["marketvalueC2"].ToString();
                txtexpriceC2.Text = dr["expriceC2"].ToString();
                txtNounexC2.Text = dr["NounexC2"].ToString();
                doec2.SelectedDate = Convert.ToDateTime(dr["doexpC2"].ToString());

                txtrocc3.Text = dr["rocC3"].ToString();
                txtcmpnameC3.Text = dr["cmpnameC3"].ToString();
                drpC3.SelectedItem.Text = dr["drpC3"].ToString();
                dogc3.SelectedDate = Convert.ToDateTime(dr["dogC3"].ToString());
                txtopenmarkC3.Text = dr["openmarkC3"].ToString();
                txtMValueC3.Text = dr["marketvalueC3"].ToString();
                txtexpriceC3.Text = dr["expriceC3"].ToString();
                txtNounexC3.Text = dr["NounexC3"].ToString();
                doec3.SelectedDate = Convert.ToDateTime(dr["doexpC3"].ToString());

                txtrocD1.Text = dr["rocD1"].ToString();
                txtcmpnameD1.Text = dr["cmpnameD1"].ToString();
                drpD1.SelectedItem.Text = dr["drpD1"].ToString();
                dogd1.SelectedDate = Convert.ToDateTime(dr["dogD1"].ToString());
                txtopenmarkD1.Text = dr["openmarkD1"].ToString();
                txtMValueD1.Text = dr["marketvalueD1"].ToString();
                txtexpriceD1.Text = dr["expriceD1"].ToString();
                txtNounexD1.Text = dr["NounexD1"].ToString();
                doed1.SelectedDate = Convert.ToDateTime(dr["doexpD1"].ToString());

                txtrocD2.Text = dr["rocD2"].ToString();
                txtcmpnameD2.Text = dr["cmpnameD2"].ToString();
                drpD2.SelectedItem.Text = dr["drpD2"].ToString();
                dogd2.SelectedDate = Convert.ToDateTime(dr["dogD2"].ToString());
                txtopenmarkD2.Text = dr["openmarkD2"].ToString();
                txtMValueD2.Text = dr["marketvalueD2"].ToString();
                txtexpriceD2.Text = dr["expriceD2"].ToString();
                txtNounexD2.Text = dr["NounexD2"].ToString();
                doed2.SelectedDate = Convert.ToDateTime(dr["doexpD2"].ToString());

                txtrocD3.Text = dr["rocD3"].ToString();
                txtcmpnameD3.Text = dr["cmpnameD3"].ToString();
                drpD3.SelectedItem.Text = dr["drpD3"].ToString();
                dogd3.SelectedDate = Convert.ToDateTime(dr["dogD3"].ToString());
                txtopenmarkD3.Text = dr["openmarkD3"].ToString();
                txtMValueD3.Text = dr["marketvalueD3"].ToString();
                txtexpriceD3.Text = dr["expriceD3"].ToString();
                txtNounexD3.Text = dr["NounexD3"].ToString();
                doed3.SelectedDate = Convert.ToDateTime(dr["doexpD3"].ToString());

                txtrocD4.Text = dr["rocD4"].ToString();
                txtcmpnameD4.Text = dr["cmpnameD4"].ToString();
                drpD4.SelectedItem.Text = dr["drpD4"].ToString();
                dogd4.SelectedDate = Convert.ToDateTime(dr["dogD4"].ToString());
                txtopenmarkD4.Text = dr["openmarkD4"].ToString();
                txtMValueD4.Text = dr["marketvalueD4"].ToString();
                txtexpriceD4.Text = dr["expriceD4"].ToString();
                txtNounexD4.Text = dr["NounexD4"].ToString();
                doed4.SelectedDate = Convert.ToDateTime(dr["doexpD4"].ToString());

                txtrocD5.Text = dr["rocD5"].ToString();
                txtcmpnameD5.Text = dr["cmpnameD5"].ToString();
                drpD5.SelectedItem.Text = dr["drpD5"].ToString();
                dogd5.SelectedDate = Convert.ToDateTime(dr["dogD5"].ToString());
                txtopenmarkD5.Text = dr["openmarkD5"].ToString();
                txtMValueD5.Text = dr["marketvalueD5"].ToString();
                txtexpriceD5.Text = dr["expriceD5"].ToString();
                txtNounexD5.Text = dr["NounexD5"].ToString();
                doed5.SelectedDate = Convert.ToDateTime(dr["doexpD5"].ToString());

                txtremark.Text = dr["remarks"].ToString();

                txtauthoris.Text = dr["authoris"].ToString();
                txtdesign.Text = dr["design"].ToString();
                txtnamecont.Text = dr["namecont"].ToString();
                txtcontno.Text = dr["contno"].ToString();
                txtfaxno.Text = dr["faxno"].ToString();
                txtemail .Text = dr["email"].ToString();

                TextBox7.Text = dr["authoris"].ToString();
                TextBox8.Text = dr["design"].ToString();
                TextBox9.Text = dr["namecont"].ToString();
                TextBox10.Text = dr["contno"].ToString();
                TextBox11.Text = dr["faxno"].ToString();
                TextBox12.Text = dr["email"].ToString();
                if(dr["email"].ToString().Length >=10)
                {
                    date1.SelectedDate = Convert.ToDateTime(dr["date"]);
                }
                

            }
            calculate_ir21_app2();

        }

        void btnsave_ServerClick(object sender, EventArgs e)
        {

           
                if (tbsEmp.SelectedTab.Text == "FORM IR21")
                {
                    form2save();
                }
                else if (tbsEmp.SelectedTab.Text == "APPENDIX 1")
                {
                    ir21_app1save();
                }
                else if (tbsEmp.SelectedTab.Text == "APPENDIX 2")
                {
                    //update_ir21_app2_form();
                   ir21_app2save();
                }
                else if (tbsEmp.SelectedTab.Text == "APPENDIX 3")
                {
                   ir21_app3save();
                }

        }
        public void form2save()
        {
            int i = 0;
            string sql = "";
            string type_of_form = "";



            if (chk_original.Checked)
            {
                type_of_form = "O";
            }
            else if (chk_additional.Checked)
            {
                type_of_form = "A";
            }
            else if (chk_amended.Checked)
            {
                type_of_form = "M";
            }
            sql = "insert into FIR21([emp_code],[type_of_form],[addtional_date],[amended_date],[com_taxref],[com_name],[com_hseno],[com_unit],[com_street]";
            sql = sql + ",[pcode],[emp_name],[emp_nric],[emp_fin],[emp_malic] ,[emp_dob],[emp_gender],[emp_natioality],[emp_contact],[emp_email],[emp_arrival]";
            sql = sql + ",[emp_commence],[emp_cessation],[emp_depature],[emp_terminate],[reason_lessthanmonth],[otherreason_details],[pending_amt],[withhold_money]";
            sql = sql + ",[reason_withhold] ,[reason_with_hold_other_details],[lastsalary_date] ,[lastsalary_amt] ,[period_lastsalary],[emp_bank],[name_newemployer]";
            sql = sql + ",[spouse_name],[spouse_dob],[spouse_id],[marriage_date],[spouse_nationality],[spouse_income4000],[spouse_employer_det],[child1_name],[child1_dob]";
            sql = sql + ",[child1_gender],[child1_school],[child2_name],[child2_dob],[child2_gender],[child2_school],[child3_name],[child3_dob] ,[child3_gender],[child3_school]";
            sql = sql + ",[child4_name],[child4_dob],[child4_gender],[child4_school],[prior_year_of_cesssation_from],[year_of_cesssation_from],[prior_year_of_cesssation_to]";
            sql = sql + ",[year_of_cesssation_to],[gsalary1] ,[gsalary2],[bonus1],[bonus2],[nbonus1],[nbonus2],[state_date1],[state_date2],[director_fee1],[director_fee2]";
            sql = sql + ",[app_date1],[app_date2],[other_gcomm1],[other_gcomm2],[other_allowance1],[other_allowance2],[gratuity1],[gratuity2] ,[notice_pay1],[notice_pay2],[compensation1]";
            sql = sql + ",[compensation2],[compensation_reason],[ret_fund1],[ret_fund2],[ret_fundname],[cont_fund1],[cont_fund2],[cont_fundname],[excess1],[excess2]";
            sql = sql + ",[tot_gross1],[tot_gross2],[esop_before],[esop_after],[a1completed],[benefit1]";
            sql = sql + ",[benefit2],[benefit_subtot1],[benefit_subtot2],[tot_item1],[tot_item2],[ded_fundname],[ded_fundamt1] ,[ded_fundamt2],[ded_donationamt1]";
            sql = sql + ",[ded_donationamt2],[ded_contamt1],[ded_contamt2],[auth_name],[design],[date],[cont_person],[cont_no],[fax],[email],[designation])";
            sql = sql + "values('" + varEmpCode + "','" + type_of_form + "','" + txt_additional_date.SelectedDate.ToString() + "','" + txt_amended_date.SelectedDate.ToString() + "','" + txt_comtaxref.Text + "'";
            sql = sql + ",'" + txt_cname.Text + "','" + txt_hno.Text + "','" + txt_unit.Text + "','" + txt_stname.Text + "','" + txt_pcode.Text + "','" + txt_ename.Text + "','" + txt_nric.Text + "','" + txt_fin.Text + "','" + txt_malic.Text + "','";
            sql = sql + txt_dob.SelectedDate.Value.ToString() + "','" + dp_empgender.SelectedValue.ToString() + "','" + txt_nationality.Text + "','" + txt_contact.Text + "','" + txt_email.Text + "','" + txt_datearrival.SelectedDate.ToString() + "','" + txt_datecommence.SelectedDate.ToString() + "','" + txt_dateposting.SelectedDate.ToString() + "','" + txt_depature.SelectedDate.ToString() + "','" + txt_terminate.SelectedDate.ToString() + "','";

            string v = "";
            if (chk_left.Checked)
            {
                v = "L";
            }
            else if (chk_shot.Checked)
            {
                v = "S";
            }
            else if (chk_whilst.Checked)
            {
                v = "W";
            }
            else if (chk_others.Checked)
            {
                v = "O";
            }
            sql = sql + v + "','" + txt_others.Text + "','" + txt_pendingtax.Text.Trim() + "','";

            if (chk_withoutyes.Checked)
            {
                v = "Y";
            }
            else
            {
                v = "N";
            }
            sql = sql + v + "','";

            if (chk_afterpayday.Checked)
            {
                v = "R";
            }
            else if (chk_paid.Checked)
            {
                v = "P";
            }
            else if (chk_notreturn.Checked)
            {
                v = "N";
            }
            else if (chk_owes.Checked)
            {
                v = "W";
            }
            else if (chk_otherss.Checked)
            {
                v = "O";
            }
            sql = sql + v + "','" + txt_otherdetails.Text + "','" + txt_lastpaid.SelectedDate.ToString() + "','" + txt_lastamt.Text + "','" + txt_periodsalary.Text + "','";
            sql = sql + txt_bkname.Text + "','" + txt_employername.Text + "','" + txt_spname.Text + "','" + txt_spdob.SelectedDate.ToString() + "','" + txt_spid.Text + "','";
            sql = sql + txt_spmarrydate.SelectedDate.ToString() + "','" + txt_nationality.Text + "','";
            if (chk_incomeyes.Checked)
            {
                v = "Y";
            }
            else
            {
                v = "N";
            }

            sql = sql + v + "','" + txt_spemployername.Text + "','" + txt_chilename1.Text + "','" + txt_cdob1.SelectedDate.ToString() + "','" + dp_gender1.SelectedValue.ToString() + "','" + txt_cschool1.Text + "','";

            sql = sql + txt_chilename2.Text + "','" + txt_cdob2.SelectedDate.ToString() + "','" + dp_gender2.SelectedValue.ToString() + "','" + txt_cschool2.Text + "','";
            sql = sql + txt_chilename3.Text + "','" + txt_cdob3.SelectedDate.ToString() + "','" + dp_gender3.SelectedValue.ToString() + "','" + txt_cschool3.Text + "','";
            sql = sql + txt_chilename4.Text + "','" + txt_cdob4.SelectedDate.ToString() + "','" + dp_gender4.SelectedValue.ToString() + "','" + txt_cschool4.Text + "','";

            sql = sql + txt_fdate_yoc1.SelectedDate.ToString() + "','" + txt_fdate_yoc2.SelectedDate.ToString() + "','" + txt_tdate_yoc1.SelectedDate.ToString() + "','" + txt_tdate_yoc1.SelectedDate.ToString() + "','";
            sql = sql + txt_gsal1.Text + "','" + txt_gsal2.Text + "','" + txt_bonus1.Text + "','" + txt_bonus2.Text + "','" + txt_nbonus1.Text + "','" + txt_nbonus2.Text + "','";
          //kumar change on 22_6_2017
         //   sql = sql + txt_state_date1.SelectedDate.ToString() + "','" + txt_state_date2.SelectedDate.ToString() + "','" + txt_direct_fees1.Text + "','" + txt_direct_fees2.Text + "','" + txt_app_date1.SelectedDate.ToString() + "','" + txt_app_date2.SelectedDate.ToString() + "','" + txt_ogcomm1.Text + "','" + txt_ogcomm1.Text + "','";
            sql = sql + txt_state_date1.SelectedDate.ToString() + "','" + txt_state_date2.SelectedDate.ToString() + "','" + txt_direct_fees1.Text + "','" + txt_direct_fees2.Text + "','" + txt_app_date1.SelectedDate.ToString() + "','" + txt_app_date2.SelectedDate.ToString() + "','" + txt_ogcomm1.Text + "','" + txt_gcomm2.Text + "','";

            sql = sql + txt_oallowance1.Text + "','" + txt_oallowance2.Text + "','" + txt_gratuity1.Text + "','" + txt_gratuity2.Text + "','" + txt_noticepay1.Text + "','" + txt_noticepay2.Text + "','" + txt_compensation1.Text + "','" + txt_compensation2.Text + "','" + txt_compreasion.Text + "','";

            sql = sql + txt_retamt1.Text + "','" + txt_retamt2.Text + "','" + txt_retname.Text + "','";
            sql = sql + txt_conamt1.Text + "','" + txt_conamt2.Text + "','" + txt_conname.Text + "','";
            sql = sql + txt_excess1.Text + "','" + txt_excess2.Text + "','" + txt_totalgross1.Text + "','" + txt_totalgross2.Text + "','";

            if (chk_esopbefore.Checked)
            {
                sql = sql + "Y" + "','" + "N" + "','";
            }

            else
            {
                sql = sql + "N" + "','" + "Y" + "','";
            }

            if (chk_a1completed.Checked)
            {
                v = "Y";
            }
            else
            {
                v = "N";
            }
            sql = sql + v + "','";
            sql = sql + txt_benefit1.Text + "','" + txt_benefit2.Text + "','" + txt_benefit_subtotal1.Text + "','" + txt_benefit_subtotal2.Text + "','";
            sql = sql + txt_totalitem1.Text + "','" + txt_totalitem2.Text + "','" + txt_ded_fundname.Text + "','" + txt_ded_fundamt1.Text + "','";
            sql = sql + txt_ded_fundamt2.Text + "','" + txt_donationamt1.Text + "','" + txt_donationamt2.Text + "','" + txt_conamt1.Text + "','";
            sql = sql + txt_conamt2.Text + "','" + f21_aname.Text + "','" + f21_design.Text + "','" + f21_date.SelectedDate.ToString() + "','" + f21_contper.Text + "','" + f21_contno.Text + "','" + f21_fax.Text + "','" + f21_email.Text +"','"+ txt_designation.Text + "')";

            string sql1 = "select * from FIR21 WHERE emp_code='" + varEmpCode + "'";

            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sql1, null);

            if (dr.Read())
            {
                DataAccess.ExecuteNonQuery("delete from FIR21 WHERE emp_code='" + varEmpCode + "'", null);
            }

            DataAccess.ExecuteNonQuery(sql, null);
            
            lblerror.Text = "Save successfully.";



        }

        public void ir21_app3save()
        {
            string str = "INSERT INTO [dbo].[fir21_app3]([empcode],[fin],[empname],[roca1],[roca2],[roca3],[roca4],[rocb1],[rocb2] ,[rocb3]";
            str = str + ",[rocb4],[rocc1],[rocc2],[rocc3],[rocd1],[rocd2],[rocd3],[rocd4],[rocd5],[cmpnameA1],[cmpnameA2],[cmpnameA3],[cmpnameA4]";
            str = str + ",[cmpnameB1],[cmpnameB2],[cmpnameB3],[cmpnameB4],[cmpnameC1],[cmpnameC2],[cmpnameC3],[cmpnameD1],[cmpnameD2]";
            str = str + ",[cmpnameD3],[cmpnameD4],[cmpnameD5],[drpA1],[drpA2],[drpA3],[drpA4],[drpB1],[drpB2],[drpB3],[drpB4],[drpC1]";
            str = str + ",[drpC2],[drpC3],[drpD1],[drpD2],[drpD3],[drpD4],[drpD5],[dogA1],[dogA2],[dogA3],[dogA4],[dogB1],[dogB2]";
            str = str + ",[dogB3],[dogB4],[dogC1],[dogC2],[dogC3],[dogD1],[dogD2],[dogD3],[dogD4],[dogD5],[openmarkA1],[openmarkA2]";
            str = str + ",[openmarkA3],[openmarkA4],[openmarkB1],[openmarkB2],[openmarkB3],[openmarkB4],[openmarkC1],[openmarkC2]";
            str = str + ",[openmarkC3],[openmarkD1],[openmarkD2],[openmarkD3],[openmarkD4],[openmarkD5],[marketvalueA1],[marketvalueA2]";
            str = str + ",[marketvalueA3],[marketvalueA4],[marketvalueB1],[marketvalueB2],[marketvalueB3],[marketvalueB4],[marketvalueC1]";
            str = str + ",[marketvalueC2],[marketvalueC3],[marketvalueD1],[marketvalueD2],[marketvalueD3],[marketvalueD4],[marketvalueD5]";
            str = str + ",[expriceA1],[expriceA2],[expriceA3],[expriceA4],[expriceB1],[expriceB2],[expriceB3],[expriceB4],[expriceC1]";
            str = str + ",[expriceC2],[expriceC3],[expriceD1],[expriceD2],[expriceD3],[expriceD4],[expriceD5],[NounexA1],[NounexA2]";
            str = str + ",[NounexA3],[NounexA4],[NounexB1],[NounexB2],[NounexB3],[NounexB4],[NounexC1],[NounexC2],[NounexC3],[NounexD1]";
            str = str + ",[NounexD2],[NounexD3],[NounexD4],[NounexD5],[doexpA1],[doexpA2],[doexpA3],[doexpA4],[doexpB1],[doexpB2]";
            str = str + ",[doexpB3],[doexpB4],[doexpC1],[doexpC2],[doexpC3],[doexpD1],[doexpD2],[doexpD3],[doexpD4],[doexpD5],[remarks]";
            str = str + ",[authoris],[design],[date],[namecont],[contno],[faxno],[email])VALUES('" + varEmpCode + "','" + tfin.Text + "','" + tEmpname.Text + "','";
            str = str + txtrocA1.Text + "','" + txtrocA2.Text + "','" + txtrocA3.Text + "','" + txtrocA4.Text + "','";
            str = str + txtrocB1.Text + "','" + txtrocB2.Text + "','" + txtrocB3.Text + "','" + txtrocB4.Text + "','";
            str = str + txtrocc1.Text + "','" + txtrocc2.Text + "','" + txtrocc3.Text + "','";
            str = str + txtrocD1.Text + "','" + txtrocD2.Text + "','" + txtrocD3.Text + "','" + txtrocD4.Text + "','" + txtrocD5.Text + "','";
            str = str + txtcmpnameA1.Text + "','" + txtcmpnameA2.Text + "','" + txtcmpnameA3.Text + "','" + txtcmpnameA4.Text + "','";
            str = str + txtcmpnameB1.Text + "','" + txtcmpnameB2.Text + "','" + txtcmpnameB3.Text + "','" + txtcmpnameB4.Text + "','";
            str = str + txtcmpnameC1.Text + "','" + txtcmpnameC2.Text + "','" + txtcmpnameC3.Text + "','";
            str = str + txtcmpnameD1.Text + "','" + txtcmpnameD2.Text + "','" + txtcmpnameD3.Text + "','" + txtcmpnameD4.Text + "','" + txtcmpnameD5.Text + "','";

            str = str + drpA1.SelectedValue.ToString() + "','" + drpA2.SelectedValue.ToString() + "','" + drpA3.SelectedValue.ToString() + "','" + drpA4.SelectedValue.ToString() + "','";
            str = str + drpB1.SelectedValue.ToString() + "','" + drpB2.SelectedValue.ToString() + "','" + drpB3.SelectedValue.ToString() + "','" + drpB4.SelectedValue.ToString() + "','";
            str = str + drpC1.SelectedValue.ToString() + "','" + drpC2.SelectedValue.ToString() + "','" + drpC3.SelectedValue.ToString() + "','";
            str = str + drpD1.SelectedValue.ToString() + "','" + drpD2.SelectedValue.ToString() + "','" + drpD3.SelectedValue.ToString() + "','" + drpD4.SelectedValue.ToString() + "','" + drpD5.SelectedValue.ToString() + "','";

            str = str + doga1.SelectedDate.ToString() + "','" + doga2.SelectedDate.ToString() + "','" + doga3.SelectedDate.ToString() + "','" + doga4.SelectedDate.ToString() + "','";
            str = str + dogb1.SelectedDate.ToString() + "','" + dogb2.SelectedDate.ToString() + "','" + dogb3.SelectedDate.ToString() + "','" + dogb4.SelectedDate.ToString() + "','";
            str = str + dogc1.SelectedDate.ToString() + "','" + dogc2.SelectedDate.ToString() + "','" + dogc3.SelectedDate.ToString() + "','";
            str = str + dogd1.SelectedDate.ToString() + "','" + dogd2.SelectedDate.ToString() + "','" + dogd3.SelectedDate.ToString() + "','" + dogd4.SelectedDate.ToString() + "','" + dogd5.SelectedDate.ToString() + "','";

            str = str + txtopenmarkA1.Text + "','" + txtopenmarkA2.Text + "','" + txtopenmarkA3.Text + "','" + txtopenmarkA4.Text + "','";
            str = str + txtopenmarkB1.Text + "','" + txtopenmarkB2.Text + "','" + txtopenmarkB3.Text + "','" + txtopenmarkB4.Text + "','";
            str = str + txtopenmarkC1.Text + "','" + txtopenmarkC2.Text + "','" + txtopenmarkC3.Text + "','";
            str = str + txtopenmarkD1.Text + "','" + txtopenmarkD2.Text + "','" + txtopenmarkD3.Text + "','" + txtopenmarkD4.Text + "','" + txtopenmarkD5.Text + "','";

            str = str + txtMValueA1.Text + "','" + txtMValueA2.Text + "','" + txtMValueA3.Text + "','" + txtMValueA4.Text + "','";
            str = str + txtMValueB1.Text + "','" + txtMValueB2.Text + "','" + txtMValueB3.Text + "','" + txtMValueB4.Text + "','";
            str = str + txtMValueC1.Text + "','" + txtMValueC2.Text + "','" + txtMValueC3.Text + "','";
            str = str + txtMValueD1.Text + "','" + txtMValueD2.Text + "','" + txtMValueD3.Text + "','" + txtMValueD4.Text + "','" + txtMValueD5.Text + "','";


            str = str + txtexpriceA1.Text + "','" + txtexpriceA2.Text + "','" + txtexpriceA3.Text + "','" + txtexpriceA4.Text + "','";
            str = str + txtexpriceB1.Text + "','" + txtexpriceB2.Text + "','" + txtexpriceB3.Text + "','" + txtexpriceB4.Text + "','";
            str = str + txtexpriceC1.Text + "','" + txtexpriceC2.Text + "','" + txtexpriceB3.Text + "','";
            str = str + txtexpriceD1.Text + "','" + txtexpriceD2.Text + "','" + txtexpriceD3.Text + "','" + txtexpriceD4.Text + "','" + txtexpriceD5.Text + "','";

            str = str + txtNounexA1.Text + "','" + txtNounexA2.Text + "','" + txtNounexA3.Text + "','" + txtNounexA4.Text + "','";
            str = str + txtNounexB1.Text + "','" + txtNounexB2.Text + "','" + txtNounexB3.Text + "','" + txtNounexB4.Text + "','";
            str = str + txtNounexC1.Text + "','" + txtNounexC2.Text + "','" + txtNounexC3.Text + "','";
            str = str + txtNounexD1.Text + "','" + txtNounexD2.Text + "','" + txtNounexD3.Text + "','" + txtNounexD4.Text + "','" + txtNounexD5.Text + "','";

            str = str + doea1.SelectedDate.ToString() + "','" + doea2.SelectedDate.ToString() + "','" + doea3.SelectedDate.ToString() + "','" + doea4.SelectedDate.ToString() + "','";
            str = str + doeb1.SelectedDate.ToString() + "','" + doeb2.SelectedDate.ToString() + "','" + doeb3.SelectedDate.ToString() + "','" + doeb4.SelectedDate.ToString() + "','";
            str = str + doec1.SelectedDate.ToString() + "','" + doec2.SelectedDate.ToString() + "','" + doec3.SelectedDate.ToString() + "','";
            str = str + doec1.SelectedDate.ToString() + "','" + doec2.SelectedDate.ToString() + "','" + doec3.SelectedDate.ToString() + "','" + doed4.SelectedDate.ToString() + "','" + doed5.SelectedDate.ToString() + "','";

            str = str + txtremark.Text + "','" + txtauthoris.Text + "','" + txtdesign.Text + "','" + date1.SelectedDate.ToString()+"','"+ txtnamecont.Text + "','" + txtcontno.Text + "','" + txtfaxno.Text + "','" + txtemail.Text + "')";
            int countrec = DataAccess.ExecuteNonQuery("delete from fir21_app3 where empcode='" + varEmpCode + "'", null);

            countrec = DataAccess.ExecuteNonQuery(str, null);


            lblerror.Text = "Save successfully.";




        }
        public void ir21_app2save()
        {
            DataAccess.ExecuteNonQuery("delete from ir21_app2_details2 where empcode='" + varEmpCode + "'", null);


            // RadDatePicker dtpRadDatePicker = (RadDatePicker)RadPanelBar1.FindItemByValue("IR21_APP2_Panel21").FindControl("RadDatePicker1");
            //  RadDatePicker dp = (RadDatePicker)IR21_APP2_Panel21.FindControl("RadDatePicker1");
            // string s = dp.SelectedDate.Value.ToString("yyyyMMdd");
            string s = RadDatePicker1.SelectedDate.Value.ToString("yyyyMMdd");
            string str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','a1','";
            str = str + sa_a121.Text + "','" + sa_b121.Text + "','" + sa_ca121.Text + "','" + ddtoe1.Text + "','" + RadDatePicker1.SelectedDate.ToString() + "','";
            str = str + RadDatePicker1.SelectedDate.ToString() + "'," + RadNumericTextBox1.Text + ",0," + RadNumericTextBox2.Text + "," + RadNumericTextBox3.Text + ",0,0,0,"+sa_l121.Text+","+sa_m121.Text+")";
             
            DataAccess.ExecuteNonQuery(str, null);
            //----------a
            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','a2','";
            str = str + sa_a221.Text + "','" + sa_b221.Text + "','" + sa_ca221.Text + "','" + ddtoe2.Text + "','" + RadDatePicker3.SelectedDate.ToString() + "','";
            str = str + RadDatePicker4.SelectedDate.ToString() + "'," + RadNumericTextBox4.Text + ",0," + RadNumericTextBox5.Text + "," + RadNumericTextBox6.Text + ",0,0,0,"+sa_l221.Text+","+sb_m221.Text+")";
            DataAccess.ExecuteNonQuery(str, null); 

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','a3','";
            str = str + sa_a321.Text + "','" + sa_b321.Text + "','" + sa_ca321.Text + "','" + ddtoe3.Text + "','" + RadDatePicker5.SelectedDate.ToString() + "','";
            str = str + RadDatePicker6.SelectedDate.ToString() + "'," + RadNumericTextBox7.Text + ",0," + RadNumericTextBox8.Text + "," + RadNumericTextBox9.Text + ",0,0,0,"+sa_l321.Text+","+sa_m321.Text+")";
            DataAccess.ExecuteNonQuery(str, null);

            //----b
            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','b1','";
            str = str + sb_a121.Text + "','" + sb_b121.Text + "','" + sb_ca121.Text + "','" + ddtoe4.Text + "','" + RadDatePicker7.SelectedDate.ToString() + "','";
            str = str + RadDatePicker8.SelectedDate.ToString() + "'," + RadNumericTextBox10.Text + "," + RadNumericTextBox11.Text + "," + RadNumericTextBox12.Text + "," + RadNumericTextBox13.Text + "," + sb_i121.Text + ",0,0,"+ sb_l121.Text +","+ sb_m121.Text+")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','b2','";
            str = str + sb_a221.Text + "','" + sb_b221.Text + "','" + sb_ca221.Text + "','" + ddtoe5.Text + "','" + RadDatePicker9.SelectedDate.ToString() + "','";
            str = str + RadDatePicker10.SelectedDate.ToString() + "'," + RadNumericTextBox14.Text + "," + RadNumericTextBox15.Text + "," + RadNumericTextBox16.Text + "," + RadNumericTextBox17.Text + "," + sb_i221.Text + ",0,0," + sb_l221.Text + "," + sb_m221.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','b3','";
            str = str + sb_a321.Text + "','" + sb_b321.Text + "','" + sb_ca321.Text + "','" + ddtoe6.Text + "','" + RadDatePicker11.SelectedDate.ToString() + "','";
            str = str + RadDatePicker12.SelectedDate.ToString() + "'," + RadNumericTextBox18.Text + "," + RadNumericTextBox19.Text + "," + RadNumericTextBox20.Text + "," + RadNumericTextBox21.Text + "," + sb_i321.Text + ",0,0," + sb_l321.Text + "," + sb_m321.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);
            //------------c
            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','c1','";
            str = str + sc_a121.Text + "','" + sc_b121.Text + "','" + sc_ca121.Text + "','" + ddtoe7.Text + "','" + sc_dp11.SelectedDate.ToString() + "','";
            str = str + sc_dp12.SelectedDate.ToString() + "'," + sc_e121.Text + "," + sc_f121.Text + "," + sc_g121.Text + "," + sc_h121.Text + ",0," + sc_j121.Text + ",0," + sc_l121.Text + "," + sc_m121.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','c2','";
            str = str + sc_a221.Text + "','" + sc_b221.Text + "','" + sc_ca221.Text + "','" + ddtoe8.Text + "','" + sc_dp21.SelectedDate.ToString() + "','";
            str = str + sc_dp22.SelectedDate.ToString() + "'," + sc_e221.Text + "," + sc_f221.Text + "," + sc_g221.Text + "," + sc_h221.Text + ",0," + sc_j221.Text + ",0," + sc_l221.Text + "," + sc_m221.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','c3','";
            str = str + sc_a321.Text + "','" + sc_b321.Text + "','" + sc_ca321.Text + "','" + ddtoe9.Text + "','" + sc_dp31.SelectedDate.ToString() + "','";
            str = str + sc_dp32.SelectedDate.ToString() + "'," + sc_e321.Text + "," + sc_f321.Text + "," + sc_g321.Text + "," + sc_h321.Text + ",0," + sc_j321.Text + ",0," + sc_l321.Text + "," + sc_m321.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            //-----------d
            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','d1','";
            str = str + sd_a121.Text + "','" + sd_b121.Text + "','" + sd_ca121.Text + "','" + ddtoe10.Text + "','" + sd_dp11.SelectedDate.ToString() + "','";
            str = str + sd_dp12.SelectedDate.ToString() + "'," + sd_e121.Text + "," + sd_f121.Text + "," + sd_g121.Text + "," + sd_h121.Text + ",0,0," + sd_k121.Text + "," + sd_l121.Text + "," + sd_m121.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','d2','";
            str = str + sd_a221.Text + "','" + sd_b221.Text + "','" + sd_ca221.Text + "','" + ddtoe11.Text + "','" + sd_dp21.SelectedDate.ToString() + "','";
            str = str + sd_dp22.SelectedDate.ToString() + "'," + sd_e221.Text + "," + sd_f221.Text + "," + sd_g221.Text + "," + sd_h221.Text + ",0,0," + sd_k221.Text + "," + sd_l221.Text + "," + sd_m221.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            str = "INSERT INTO [dbo].[ir21_app2_details2]([empcode],[fin],[empname],[Section],[regno],[companyname],[PlanType],[exerciseType]";
            str = str + ",[DateOfGrant],[DateOfExercise],[Price],[OpenMarketValueAtDateOfGrant],[OpenMarketValueAtDateOfReflected],[NoOfShares]";
            str = str + ",[Total_i],[Total_j],[Total_k],[Total_l],[Total_m]) VALUES('" + varEmpCode + "','" + fin2.Text + "','" + ename2.Text + "','d3','";
            str = str + sd_a321.Text + "','" + sd_b321.Text + "','" + sd_ca321.Text + "','" + ddtoe12.Text + "','" + sd_dp31.SelectedDate.ToString() + "','";
            str = str + sd_dp32.SelectedDate.ToString() + "'," + sd_e321.Text + "," + sd_f321.Text + "," + sd_g321.Text + "," + sd_h321.Text + ",0,0," + sd_k321.Text + "," + sd_l321.Text + "," + sd_m321.Text + ")";
            DataAccess.ExecuteNonQuery(str, null);

            lblerror.Text = "Save successfully.";

        }

        public void ir21_app1save()
        {
            string s = txtadder1.Text;
            s = txtadder2.Text;
            s = txtAddres1.Text;


            string str = "INSERT INTO [dbo].[IR21_Appendix1]([empcode],[fin],[empname],[elyadder1],[elyadder2],[periodoccfrom1],[periodoccfrom2],[periodoccto1],[periodoccto2],[numberday1],[numberday2],[numbeeremp1],[numbeeremp2]";
            str = str + ",[rent1],[rent2],[residence1],[residence2],[atotal1],[atotal2],[furnitureunit],[furniturevalu1],[furniturevalu2],[refrunit],[refrvalu1],[refrvalu2],[washinunit1]";
            str = str + ",[washinunit2],[washinunit3],[washinvalu1],[washinvalu2],[airunit],[airvalu1],[airvalu2],[diningunit1],[diningunit2],[dining1],[dining2],[addroomunit],[addroomvalu1],[addroomvalu2],[tvunit],[radiounit]";
            str = str + ",[ampunit],[hifiunit],[guitarunit],[tvvalu1],[tvvalu2],[computerunit1],[computerunit2],[computervalu1],[computervalu2],[swimmingunit],[swimmingvalu1]";
            str = str + ",[swimmingvalu2],[othersvalu1],[othersvalu2],[btotal1],[btotal2],[pubvalu1],[pubvalu2],[drivvalu1],[drivvalu2],[gardernvalu1],[gardernvalu2],[b1total1]";
            str = str + ",[b1total2],[selfaunit],[selfcunit],[selfvalu1],[selfvalu2],[wifeaunit],[wifecunit],[wifevalu1],[wifevalu2],[child8aunit],[child8cunit],[child8valu1]";
            str = str + ",[child8valu2],[child3aunit],[child3cunit],[child3valu1],[child3valu2],[childaunit],[childcunit],[childvalu1],[childvalu2],[ctotal1],[ctotal2],[dresidanceadd1]";
            str = str + ",[dresidanceadd2],[dperiodoccfrom1_1],[dperiodoccfrom1_2],[dperiodoccto1_1],[dperiodoccto1_2],[dnumberocc1],[dnumberocc2],[dannualval1],[dannualval2]";
            str = str + ",[dfurniture1],[dfurniture2],[dactualrent1],[dactualrent2],[drentpaid1],[drentpaid2],[dtotald7_1],[dtotald7_2],[dresidance2add1],[dresidance2add2]";
            str = str + ",[dperiodoccfrom2_1],[dperiodoccfrom2_2],[dperiodoccto2_1],[dperiodoccto2_2],[dnumberdayocc1],[dnumberdayocc2],[dannualvalu1],[dannualvalu2]";
            str = str + ",[dfurniture1_1],[dfurniture1_2],[dactualrent1_1],[dactualrent1_2],[drentpaid1_1],[drentpaid1_2],[dtotald15_1],[dtotald15_2],[dutillites1],[dutillites2]";
            str = str + ",[ddrive1],[ddrive2],[dservant1],[dservant2],[dtotal1],[dtotal2],[ehotle1],[ehotel2],[etotal1],[etotal2],[fempcost1],[fempcost2],[finterestpayment1]";
            str = str + ",[finterestpayment2],[flifinsurance1],[flifinsurance2],[ffreesubsidised1],[ffreesubsidised2],[feducational1],[feducational2],[fmonetary1],[fmonetary2]";
            str = str + ",[fentrance1],[fentrance2],[fgains1],[fgains2],[ffullcost1],[ffullcost2],[fcarbenefit1],[fcarbenefit2],[fotherbenefit1],[fotherbenefit2],[ftotal1]";
            str = str + ",[ftotal2],[total1],[total2],[nameauthorised],[designation],[date],[contactperson],[contactno],[faxno],[emailaddress])";
            str = str + "VALUES('" + varEmpCode + "','" + txtfin.Text + "','" + txtEmpname.Text + "','" + txtadder1.Text + "','" + txtadder2.Text + "','" + txtprefrom1.SelectedDate.ToString() + "','" + txtprefrom2.SelectedDate.ToString() + "','" + txtpreto1 .SelectedDate.ToString() + "','" +txtpreto2.SelectedDate.ToString() + "','";
            str = str + txtnumday1.Text + "','" + txtnumday2.Text + "','" + txtnumemp1.Text + "','" + txtnumemp2.Text + "','" + txtrent1.Text + "','" + txtrent2.Text + "','" + txtres1.Text + "','" + txtres2.Text + "',";
            str = str + Convert.ToDecimal(txtAnn1.Text) + "," + Convert.ToDecimal(txtAnn2.Text) + ",'" + txtFurnUnit.Text + "'," + Convert.ToDecimal(txtfur1.Text) + "," + Convert.ToDecimal(txtfur2.Text) + ",'";
            str = str + txtRefUnit1.Text + "'," + Convert.ToDecimal(txtRef1.Text) + "," + Convert.ToDecimal(txtRef2.Text) + ",'" + txtwasunit1.Text + "','" + txtwasunit2.Text + "','" + txtwasunit3.Text + "',";
            str = str + Convert.ToDecimal(txtwas1.Text) + "," + Convert.ToDecimal(txtwas2.Text) + ",'" + txtAirUnit.Text + "'," + Convert.ToDecimal(txtAir1.Text) + "," + Convert.ToDecimal(txtAir2.Text) + ",'" + txtDiningunit1.Text + "','" + txtDiningunit2.Text + "','" + txtDining1.Text + "','" + txtDining2.Text + "','" + txtAddromUnit1.Text + "'," + Convert.ToDecimal(txtAddrom1.Text) + "," + Convert.ToDecimal(txtAddrom2.Text) + ",'" + txtTVunit1.Text + "','";
            str = str + txtRadiounit1.Text + "','" + txtAmplunit1.Text + "','" + txtHifiunit1.Text + "','" + txtGuitunit1.Text + "'," + Convert.ToDecimal(txtTV1.Text) + "," + Convert.ToDecimal(txtTV2.Text) + ",'" + txtComUnit1.Text + "','";
            str = str + txtComUnit2.Text + "'," + Convert.ToDecimal(txtCom1.Text) + "," + Convert.ToDecimal(txtCom2.Text) + ",'" + txtswimunit1.Text + "'," + Convert.ToDecimal(txtswim1.Text) + "," + Convert.ToDecimal(txtswim2.Text) + "," + Convert.ToDecimal(txtothbenef1.Text) + "," + Convert.ToDecimal(txtothbenef2.Text) + ",'";
            str = str + txttaxtot1.Text + "','" + txttaxtot2.Text + "'," + Convert.ToDecimal(txtpub1.Text) + "," + Convert.ToDecimal(txtpub2.Text) + "," + Convert.ToDecimal(TextBox1.Text) + "," + Convert.ToDecimal(txtdriv2.Text) + "," + Convert.ToDecimal(txtgardener1.Text) + "," + Convert.ToDecimal(txtgardener2.Text) + ",";
            str = str + Convert.ToDecimal(txttaxpubtot1.Text) + "," + Convert.ToDecimal(txttaxpubtot2.Text) + ",'";



            str = str + txtselfA.Text + "','" + txtselfC.Text + "'," + Convert.ToDecimal(txtselfC1.Text) + "," + Convert.ToDecimal(txtselfC2.Text) + ",'" + txtwifeA.Text + "','" + txtwifeC.Text + "'," + Convert.ToDecimal(txtwifeC1.Text) + "," + Convert.ToDecimal(txtwifeC2.Text) + ",'";

            str = str + txtChil8A.Text + "','" + txtChil8C.Text + "'," + Convert.ToDecimal(txtChil8C1.Text) + "," + Convert.ToDecimal(txtChil8C2.Text) + ",'" + txtchil3A.Text + "','" + txtchil3C.Text + "'," + Convert.ToDecimal(txtchil3C1.Text) + "," + Convert.ToDecimal(txtchil3C2.Text) + ",'";

            str = str + txtchilA.Text + "','" + txtchilC.Text + "'," + Convert.ToDecimal(txtchilC1.Text) + "," + Convert.ToDecimal(txtchilC2.Text) + "," + Convert.ToDecimal(txtbasic1.Text) + "," + Convert.ToDecimal(txtbasic2.Text) + ",'" + txtAddres1.Text + "','','";

            str = str + txtdfrom1.SelectedDate.ToString () + "','" + txtdfrom2.SelectedDate.ToString () + "','" + txtdto1.SelectedDate.ToString ()+ "','" + txtdto2.SelectedDate.ToString() + "','" + txtocc1.Text + "','" + txtocc2.Text + "',";


            str = str + Convert.ToDecimal(txtannu1.Text) + "," + Convert.ToDecimal(txtannu2.Text) + ",'" + txtfur1.Text + "','" + txtfur2.Text + "','" + txtact1.Text + "','" + txtact2.Text + "'," + Convert.ToDecimal(txtdrent1.Text) + "," + Convert.ToDecimal(txtdrent2.Text) + ",";

            str = str + Convert.ToDecimal(txtdtotal1.Text) + "," + Convert.ToDecimal(txtdtotal2.Text) + ",'" + txtadd2.Text +"','"+ txtadd2.Text + "','" + txtperfrom1.SelectedDate.ToString() + "','" + txtperfrom2.SelectedDate.ToString() + "','" + txtperto1.SelectedDate.ToString() + "','" + txtperto2.SelectedDate.ToString() + "','" + txtoccA1.Text + "','" + txtoccA2.Text + "',";

            str = str + Convert.ToDecimal(txtAnnA1.Text) + "," + Convert.ToDecimal(txtAnnA2.Text) + ",'" + txtfurA1.Text + "','" + TextBox2.Text + "','" + txtacctA1.Text + "','" + txtacctA2.Text + "','" + txtrentA1.Text + "','" + txtrentA2.Text + "'," + Convert.ToDecimal(txttaxtotA1.Text) + "," + Convert.ToDecimal(txttaxtotA2.Text) + ",'" + txtutilit1.Text + "','" + txtutilit2.Text + "','" + txtddriver1.Text + "','";
            str = str + txtddriver2.Text + "','" + txtServ1.Text + "','" + txtServ2.Text + "'," + Convert.ToDecimal(txtDtot1.Text) + "," + Convert.ToDecimal(txtDtot2.Text) + ",'" + txthote1.Text + "','" + txthote2.Text + "'," + Convert.ToDecimal(txtEtax1.Text) + "," + Convert.ToDecimal(txtEtax2.Text) + ",'" + txtcost1.Text + "','";
            str = str + txtcost2.Text + "'," + Convert.ToDecimal(txtpayment1.Text) + "," + Convert.ToDecimal(txtpayment2.Text) + ",'" + txtinsur1.Text + "','" + txtinsur2.Text + "','" + txtsub1.Text + "','" + txtsub2.Text + "'," + Convert.ToDecimal(txtEducat1.Text) + ",0,'";



            str = str + txtmonetary1.Text + "','" + txtmonetary2.Text + "','" + txtenterance1.Text + "','" + txtenterance2.Text + "','" + txtgains1.Text + "','" + txtgains2.Text + "'," + Convert.ToDecimal(txtfullcost1.Text) + "," + Convert.ToDecimal(txtfullcost2.Text) + ",'";

            str = str + txtcarbenefit1.Text + "','" + txtcarbenefit2.Text + "','"+ others_total1.Text +"','"+ others_total2.Text +"',"+ Convert.ToDecimal(txtIRTot1.Text) + "," + Convert.ToDecimal(txtFtot2.Text) + "," + Convert.ToDecimal(TextBox3.Text) + "," + Convert.ToDecimal(txtIRTot2.Text) + ",'";

            str = str + txtauthname.Text + "','" + TextBox4.Text + "','" + txtdate.SelectedDate.ToString() + "','" + txtconname.Text + "','" + txtconno.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "')";

            int countrec = DataAccess.ExecuteNonQuery("delete from IR21_Appendix1 where empcode='" + varEmpCode + "'", null);

            countrec = DataAccess.ExecuteNonQuery(str, null);


            lblerror.Text = "Save successfully.";


        }

       
        void calculate_ir21_app2()
        {
            decimal d;
            d = (Convert.ToDecimal(RadNumericTextBox2.Text) - Convert.ToDecimal(RadNumericTextBox1.Text)) * Convert.ToDecimal(RadNumericTextBox3.Text.ToString());
            sa_l121.Text = d.ToString();
            sa_m121.Text = sa_l121.Text;
            sa_l221.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox5.Text) - Convert.ToDecimal(RadNumericTextBox4.Text)) * Convert.ToDecimal(RadNumericTextBox6.Text));
            sa_m221.Text = sa_l221.Text;
            sa_l321.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox8.Text) - Convert.ToDecimal(RadNumericTextBox7.Text)) * Convert.ToDecimal(RadNumericTextBox9.Text));
            sa_m321.Text = sa_l321.Text;

            sa_tl21.Text = Convert.ToString(Convert .ToDecimal( sa_l121.Text) + Convert .ToDecimal(sa_l221.Text) + Convert .ToDecimal(sa_l321.Text));
            sa_tm21.Text = Convert.ToString(Convert.ToDecimal(sa_m121.Text) + Convert.ToDecimal(sa_m221.Text) + Convert.ToDecimal(sa_m321.Text));

            sb_i121.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox12.Text) - Convert.ToDecimal(RadNumericTextBox11.Text)) * Convert.ToDecimal(RadNumericTextBox13.Text));
            sb_l121.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox11.Text) - Convert.ToDecimal(RadNumericTextBox10.Text)) * Convert.ToDecimal(RadNumericTextBox13.Text));
            sb_m121.Text = Convert.ToString(Convert.ToDecimal(sb_i121.Text) + Convert.ToDecimal(sb_l121.Text));

            sb_i221.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox16.Text) - Convert.ToDecimal(RadNumericTextBox15.Text)) * Convert.ToDecimal(RadNumericTextBox17.Text));
            sb_l221.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox15.Text) - Convert.ToDecimal(RadNumericTextBox14.Text)) * Convert.ToDecimal(RadNumericTextBox17.Text));
            sb_m221.Text = Convert.ToString(Convert.ToDecimal(sb_i221.Text) + Convert.ToDecimal(sb_l221.Text));

            sb_i321.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox20.Text) - Convert.ToDecimal(RadNumericTextBox18.Text)) * Convert.ToDecimal(RadNumericTextBox21.Text));
            sb_l321.Text = Convert.ToString((Convert.ToDecimal(RadNumericTextBox19.Text) - Convert.ToDecimal(RadNumericTextBox18.Text)) * Convert.ToDecimal(RadNumericTextBox21.Text));
            sb_m321.Text = Convert.ToString(Convert.ToDecimal(sb_i321.Text) + Convert.ToDecimal(sb_l321.Text));

            sb_ti21.Text = Convert.ToString(Convert.ToDecimal(sb_i121.Text) + Convert.ToDecimal(sb_i221.Text) + Convert.ToDecimal(sb_i321.Text));
            sb_tl21.Text = Convert.ToString(Convert.ToDecimal(sb_l121.Text) + Convert.ToDecimal(sb_l221.Text) + Convert.ToDecimal(sb_l321.Text));
            sb_tm21.Text = Convert.ToString(Convert.ToDecimal(sb_m121.Text) + Convert.ToDecimal(sb_m221.Text) + Convert.ToDecimal(sb_m321.Text));

            
            sc_j121.Text = Convert.ToString((Convert.ToDecimal(sc_g121.Text) - Convert.ToDecimal(sc_f121.Text)) * Convert.ToDecimal(sc_h121.Text));
            sc_l121.Text = Convert.ToString((Convert.ToDecimal(sc_f121.Text) - Convert.ToDecimal(sc_e121.Text)) * Convert.ToDecimal(sc_h121.Text));
            sc_m121.Text = Convert.ToString(Convert.ToDecimal(sc_j121.Text) + Convert.ToDecimal(sc_l121.Text));

            sc_j221.Text = Convert.ToString((Convert.ToDecimal(sc_g221.Text) - Convert.ToDecimal(sc_f221.Text)) * Convert.ToDecimal(sc_h221.Text));
            sc_l221.Text = Convert.ToString((Convert.ToDecimal(sc_f221.Text) - Convert.ToDecimal(sc_e221.Text)) * Convert.ToDecimal(sc_h221.Text));
            sc_m221.Text = Convert.ToString(Convert.ToDecimal(sc_j221.Text) + Convert.ToDecimal(sc_l221.Text));

            sc_j321.Text = Convert.ToString((Convert.ToDecimal(sc_g321.Text) - Convert.ToDecimal(sc_f321.Text)) * Convert.ToDecimal(sc_h321.Text));
            sc_l321.Text = Convert.ToString((Convert.ToDecimal(sc_f321.Text) - Convert.ToDecimal(sc_e321.Text)) * Convert.ToDecimal(sc_h321.Text));
            sc_m321.Text = Convert.ToString(Convert.ToDecimal(sc_j321.Text) + Convert.ToDecimal(sc_l321.Text));

            sc_tj21.Text = Convert.ToString(Convert.ToDecimal(sc_j121.Text) + Convert.ToDecimal(sc_j221.Text) + Convert.ToDecimal(sc_j321.Text));
            sc_tl21.Text = Convert.ToString(Convert.ToDecimal(sc_l121.Text) + Convert.ToDecimal(sc_l221.Text) + Convert.ToDecimal(sc_l321.Text));
            sc_tm21.Text = Convert.ToString(Convert.ToDecimal(sc_m121.Text) + Convert.ToDecimal(sc_m221.Text) + Convert.ToDecimal(sc_m321.Text));


            sd_k121.Text = Convert.ToString((Convert.ToDecimal(sd_g121.Text) - Convert.ToDecimal(sd_f121.Text)) * Convert.ToDecimal(sd_h121.Text));
            sd_l121.Text = Convert.ToString((Convert.ToDecimal(sd_f121.Text) - Convert.ToDecimal(sd_e121.Text)) * Convert.ToDecimal(sd_h121.Text));
            sd_m121.Text = Convert.ToString(Convert.ToDecimal(sd_k121.Text) + Convert.ToDecimal(sd_l121.Text));

            sd_k221.Text = Convert.ToString((Convert.ToDecimal(sd_g221.Text) - Convert.ToDecimal(sd_f221.Text)) * Convert.ToDecimal(sd_h221.Text));
            sd_l221.Text = Convert.ToString((Convert.ToDecimal(sd_f221.Text) - Convert.ToDecimal(sd_e221.Text)) * Convert.ToDecimal(sd_h221.Text));
            sd_m221.Text = Convert.ToString(Convert.ToDecimal(sd_k221.Text) + Convert.ToDecimal(sd_l221.Text));

            sd_k321.Text = Convert.ToString((Convert.ToDecimal(sd_g321.Text) - Convert.ToDecimal(sd_f321.Text)) * Convert.ToDecimal(sd_h321.Text));
            sd_l321.Text = Convert.ToString((Convert.ToDecimal(sd_f321.Text) - Convert.ToDecimal(sd_e321.Text)) * Convert.ToDecimal(sd_h321.Text));
            sd_m321.Text = Convert.ToString(Convert.ToDecimal(sd_k321.Text) + Convert.ToDecimal(sd_l321.Text));

            sd_tk21.Text = Convert.ToString(Convert.ToDecimal(sd_k121.Text) + Convert.ToDecimal(sd_k221.Text) + Convert.ToDecimal(sd_k321.Text));
            sd_tl21.Text = Convert.ToString(Convert.ToDecimal(sd_l121.Text) + Convert.ToDecimal(sd_l221.Text) + Convert.ToDecimal(sd_l321.Text));
            sd_tm21.Text = Convert.ToString(Convert.ToDecimal(sd_m121.Text) + Convert.ToDecimal(sd_m221.Text) + Convert.ToDecimal(sd_m321.Text));

            Total21.Text = Convert.ToString(Convert.ToDecimal(sa_tm21.Text) + Convert.ToDecimal(sb_tm21.Text) + Convert.ToDecimal(sc_tm21.Text) + Convert.ToDecimal(sd_tm21.Text));


        }
        void calculate_ir21_app1()
        {
            

            DateTime fdate =Convert.ToDateTime(txtprefrom1.SelectedDate);
            DateTime tdate = Convert.ToDateTime(txtpreto1.SelectedDate);
            TimeSpan t=(tdate - fdate);
            txtnumday1.Text = Convert.ToString(t.Days);

            DateTime fdate2 = Convert.ToDateTime(txtprefrom2.SelectedDate);
            DateTime tdate2 = Convert.ToDateTime(txtpreto2.SelectedDate);
            TimeSpan t2 = (tdate2 - fdate2);
            txtnumday2.Text = Convert.ToString(t2.Days);


            fdate = Convert.ToDateTime(txtdfrom1.SelectedDate);
            tdate = Convert.ToDateTime(txtdto1.SelectedDate);
            t = (tdate - fdate);
            txtocc1.Text = Convert.ToString(t.Days);

             fdate2 = Convert.ToDateTime(txtdfrom2.SelectedDate);
             tdate2 = Convert.ToDateTime(txtdto2.SelectedDate);
             t2 = (tdate2 - fdate2);
            txtocc2.Text = Convert.ToString(t2.Days);

            // fdate = Convert.ToDateTime(txtperfrom1.SelectedDate);
            // tdate = Convert.ToDateTime(txtperto1.SelectedDate);
            // t = (tdate - fdate);
            //txtnumday1.Text = Convert.ToString(t.Days);

            //fdate2 = Convert.ToDateTime(txtperfrom2.SelectedDate);
            //tdate2 = Convert.ToDateTime(txtperto2.SelectedDate);
            //t2 = (tdate2 - fdate2);
            //txtnumday2.Text = Convert.ToString(t2.Days);

            decimal a, b;
            if (txtnumday1.Text != "" && txtnumday1.Text != "0")
            {
                a = Convert.ToDecimal(txtFurnUnit.Text) * 120;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                //txtFurn1.Text = Convert.ToString(Math.Round(a * b, 2));
                decimal d = (Math.Round(a * b, 2));
                txtFurn1.Text = d.ToString("####.00");

                a = Convert.ToDecimal(txtRefUnit1.Text) * 120 + Convert.ToDecimal(txtRefUnit2.Text) * 240;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtRef1.Text = Convert.ToString(Math.Round(a * b, 2));

              

                a = (Convert.ToDecimal(txtwasunit1.Text) + Convert.ToDecimal(txtwasunit2.Text) + Convert.ToDecimal(txtwasunit3.Text)) * 180;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtwas1.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = Convert.ToDecimal(txtAirUnit.Text) * 120;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtAir1.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = Convert.ToDecimal(txtDiningunit1.Text) * 180 + Convert.ToDecimal(txtDiningunit1.Text) * 180;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtDining1.Text = Convert.ToString(Math.Round(a * b, 2));

               

                a = Convert.ToDecimal(txtAddromUnit1.Text) * 120;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtAddrom1.Text = Convert.ToString(Math.Round(a * b, 2));

                
                a = (Convert.ToDecimal(txtTVunit1.Text) + Convert.ToDecimal(txtRadiounit1.Text) + Convert.ToDecimal(txtAmplunit1.Text) + Convert.ToDecimal(txtHifiunit1.Text) + Convert.ToDecimal(txtGuitunit1.Text)) * 360;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtTV1.Text = Convert.ToString(Math.Round(a * b, 2));

                

                
                 a = (Convert.ToDecimal(txtComUnit1.Text) * 180) + (Convert.ToDecimal(txtComUnit2.Text) * 180); 
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtCom1.Text = Convert.ToString(Math.Round(a * b, 2));
                
               

                a = Convert.ToDecimal(txtswimunit1.Text) * 120;
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtswim1.Text = Convert.ToString(Math.Round(a * b, 2));

                a = Convert.ToDecimal(txtswimunit1.Text) * 120;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtswim2.Text = Convert.ToString(Math.Round(a * b, 2));
                txttaxtot1.Text =Convert.ToString ( Convert.ToDecimal(txtFurn1.Text) + Convert.ToDecimal(txtRef1.Text) + Convert.ToDecimal(txtwas1.Text) + Convert.ToDecimal(txtAir1.Text) + Convert.ToDecimal(txtDining1.Text) + Convert.ToDecimal(txtAddrom1.Text) + Convert.ToDecimal(txtTV1.Text) + Convert.ToDecimal(txtCom1.Text) + Convert.ToDecimal(txtswim1.Text));
                
              }

            if (txtnumday2.Text != "" && txtnumday2.Text != "0")
            {
                 a = Convert.ToDecimal(txtFurnUnit.Text) * 120;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtFurn2.Text = Convert.ToString(Math.Round(a * b, 2));

              

                a = Convert.ToDecimal(txtRefUnit1.Text) * 120 + Convert.ToDecimal(txtRefUnit2.Text) * 240;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtRef2.Text = Convert.ToString(Math.Round(a * b, 2));

               
                a = (Convert.ToDecimal(txtwasunit1.Text) + Convert.ToDecimal(txtwasunit2.Text) + Convert.ToDecimal(txtwasunit3.Text)) * 180;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtwas2.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = Convert.ToDecimal(txtAirUnit.Text) * 120;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtAir2.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = Convert.ToDecimal(txtDiningunit1.Text) * 180 + Convert.ToDecimal(txtDiningunit1.Text) * 180;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtDining2.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = Convert.ToDecimal(txtAddromUnit1.Text) * 120;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtAddrom2.Text = Convert.ToString(Math.Round(a * b, 2));

               

                a = (Convert.ToDecimal(txtTVunit1.Text) + Convert.ToDecimal(txtRadiounit1.Text) + Convert.ToDecimal(txtAmplunit1.Text) + Convert.ToDecimal(txtHifiunit1.Text) + Convert.ToDecimal(txtGuitunit1.Text)) * 360;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtTV2.Text = Convert.ToString(Math.Round(a * b, 2));

                

                a = (Convert.ToDecimal(txtComUnit1.Text) * 180) + (Convert.ToDecimal(txtComUnit2.Text) * 180);
                b = Convert.ToDecimal(txtnumday1.Text) / 365;
                txtCom1.Text = Convert.ToString(Math.Round(a * b, 2));



                

                a = Convert.ToDecimal(txtswimunit1.Text) * 120;
                b = Convert.ToDecimal(txtnumday2.Text) / 365;
                txtswim2.Text = Convert.ToString(Math.Round(a * b, 2));
                
                txttaxtot2.Text = Convert.ToString(Convert.ToDecimal(txtFurn2.Text) + Convert.ToDecimal(txtRef2.Text) + Convert.ToDecimal(txtwas2.Text) + Convert.ToDecimal(txtAir2.Text) + Convert.ToDecimal(txtDining2.Text) + Convert.ToDecimal(txtAddrom2.Text) + Convert.ToDecimal(txtTV2.Text) + Convert.ToDecimal(txtCom2.Text) + Convert.ToDecimal(txtswim2.Text));
            }

            txta6b9_tot1.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtres1.Text) + Convert.ToDecimal(txttaxtot1.Text), 2));
            txta6b9_tot2.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtres2.Text) + Convert.ToDecimal(txttaxtot2.Text), 2));


            txttaxpubtot1.Text = Convert.ToString(Convert.ToDecimal(txtpub1.Text) + Convert.ToDecimal(TextBox1.Text) + Convert.ToDecimal(txtgardener1.Text));
            txttaxpubtot2.Text = Convert.ToString(Convert.ToDecimal(txtpub2.Text) + Convert.ToDecimal(txtdriv2.Text) + Convert.ToDecimal(txtgardener2.Text));

            a=Convert.ToDecimal(txtselfA.Text ) * 3000;
            b=Convert.ToDecimal(txtselfC.Text ) / 365;
            txtselfC1.Text = Convert.ToString(Math.Round(a*b,2));
            txtselfC2.Text = Convert.ToString(Math.Round(a*b,2));

            a=Convert.ToDecimal(txtwifeA.Text) * 3000;
            b=Convert.ToDecimal(txtwifeC.Text) / 365;
            txtwifeC1.Text = Convert.ToString(Math.Round(a*b,2));
            txtwifeC2.Text = Convert.ToString(Math.Round(a*b,2));

            a=Convert.ToDecimal(txtChil8A.Text) * 3000;
            b=Convert.ToDecimal(txtChil8C.Text) / 365;
            txtChil8C1.Text = Convert.ToString(Math.Round(a*b,2));
            txtChil8C2.Text =  Convert.ToString(Math.Round(a*b,2));

            a=Convert.ToDecimal(txtchil3A.Text) * 3000;
            b=Convert.ToDecimal(txtchil3C.Text) / 365;
            txtchil3C1.Text = Convert.ToString(Math.Round(a*b,2));
            txtchil3C2.Text = Convert.ToString(Math.Round(a*b,2));

            a=Convert.ToDecimal(txtchilA.Text) * 3000;
            b=Convert.ToDecimal(txtchilC.Text) / 365;
            txtchilC1.Text = Convert.ToString(Math .Round(a*b,2));
            txtchilC2.Text = Convert.ToString(Math .Round(a*b,2));


            txtHottot1.Text = Convert.ToString(Convert.ToDecimal(txtselfC1.Text) + Convert.ToDecimal(txtwifeC1.Text) + Convert.ToDecimal(txtChil8C1.Text) + Convert.ToDecimal(txtchil3C1.Text) + Convert.ToDecimal(txtchilC1.Text));
            txtHottot2.Text =Convert.ToString(Convert.ToDecimal(txtselfC2.Text) + Convert.ToDecimal(txtwifeC2.Text) + Convert.ToDecimal(txtChil8C2.Text) + Convert.ToDecimal(txtchil3C2.Text) + Convert.ToDecimal(txtchilC2.Text));



            txtdtotal1.Text = Convert.ToString(Convert.ToDecimal(txtact1.Text) - Convert.ToDecimal(txtdrent1.Text));
            txtdtotal2.Text = Convert.ToString(Convert.ToDecimal(txtact2.Text) - Convert.ToDecimal(txtdrent2.Text));

            txtDtot1.Text = Convert.ToString(Convert.ToDecimal(txtutilit1.Text) + Convert.ToDecimal(txtddriver1.Text) + Convert.ToDecimal(txtServ1.Text));
            txtDtot2.Text = Convert.ToString(Convert.ToDecimal(txtutilit2.Text) + Convert.ToDecimal(txtddriver2.Text) + Convert.ToDecimal(txtServ2.Text));

            //---------------
            txtDtot1.Text = Convert.ToString(Convert.ToDecimal(txtact1.Text) - Convert.ToDecimal( txtrent1.Text));
            txtDtot2.Text = Convert.ToString(Convert.ToDecimal(txtact2.Text) - Convert.ToDecimal(txtact2.Text));

            txttaxtotA1.Text = Convert.ToString(Convert.ToDecimal(txtacctA1.Text) - Convert.ToDecimal(txtrentA1 .Text ));
            txttaxtotA2.Text = Convert.ToString(Convert.ToDecimal(txtacctA2.Text) - Convert.ToDecimal(txtrentA2.Text));

            txttottax1.Text  = Convert.ToString(Convert.ToDecimal(txtDtot1 .Text ) + Convert.ToDecimal(txttaxtotA1.Text ));
            txttottax2.Text = Convert.ToString(Convert.ToDecimal(txtDtot2.Text) + Convert.ToDecimal(txttaxtotA2.Text));

            

            txtDtot1 .Text = Convert.ToString(Convert.ToDecimal(txtutilit1.Text ) + Convert.ToDecimal(txtdriv2.Text ));
            txtDtot2.Text = Convert.ToString(Convert.ToDecimal(txtutilit2.Text) + Convert.ToDecimal(txtdriv2.Text));

            txtEtax1.Text = txthote1.Text;
            txtEtax2.Text = txthote2.Text;
            string s1, s2;
            s1 = Convert.ToString( Convert.ToDecimal(txtcost1.Text)+Convert.ToDecimal(txtpayment1.Text)+Convert.ToDecimal(txtinsur1.Text )+Convert.ToDecimal(txtsub1.Text ));
            s2 = Convert.ToString(Convert.ToDecimal(txtEducat1.Text) + Convert.ToDecimal(txtmonetary1.Text) + Convert.ToDecimal(txtenterance1.Text) + Convert.ToDecimal(txtgains1.Text) + Convert.ToDecimal(txtfullcost1.Text) + Convert.ToDecimal(txtcarbenefit1.Text));
            txtIRTot1.Text = Convert.ToString(Convert.ToDecimal(s1) + Convert.ToDecimal(s2) + Convert.ToDecimal(txtothbenef1.Text));

            s1 = Convert.ToString(Convert.ToDecimal(txtcost2.Text) + Convert.ToDecimal(txtpayment2.Text) + Convert.ToDecimal(txtinsur2.Text) + Convert.ToDecimal(txtsub2.Text));
            s2 = Convert.ToString(Convert.ToDecimal(txtEducat1.Text ) + Convert.ToDecimal(txtmonetary2.Text) + Convert.ToDecimal(txtenterance2.Text) + Convert.ToDecimal(txtgains2.Text) + Convert.ToDecimal(txtfullcost1.Text) + Convert.ToDecimal(txtothbenef2.Text));
            txtFtot2.Text = Convert.ToString(Convert.ToDecimal(s1) + Convert.ToDecimal(s2)  + Convert.ToDecimal(txtothbenef2.Text));

            TextBox3.Text = Convert.ToString(Convert.ToDecimal(txtDtot1.Text) + Convert.ToDecimal(txttaxtot1.Text) + Convert.ToDecimal(txttottax1.Text) + Convert.ToDecimal(txtDtot1.Text) + Convert.ToDecimal(txtEtax1.Text) + Convert.ToDecimal(txtIRTot1.Text));
            txtIRTot2.Text  = Convert.ToString(Convert.ToDecimal ( txtDtot2.Text ) + Convert.ToDecimal(txttaxtot2.Text) + Convert.ToDecimal(txttottax2.Text) + Convert.ToDecimal(txtDtot2.Text) + Convert.ToDecimal(txtEtax2.Text) + Convert.ToDecimal(txtIRTot2.Text));




        }
        void calculate_ir21_form()
        {
            decimal tot1, tot2, tot3, tot4;
            tot1 = Convert.ToDecimal(txt_ogcomm1.Text) + Convert.ToDecimal(txt_oallowance1.Text) + Convert.ToDecimal(txt_gratuity1.Text) + Convert.ToDecimal(txt_noticepay1.Text);
            tot1 = tot1 + Convert.ToDecimal(txt_compensation1.Text) + Convert.ToDecimal(txt_retamt1.Text) + Convert.ToDecimal(txt_conamt1.Text) + Convert.ToDecimal(txt_excess1.Text);
            tot1 = tot1 + Convert.ToDecimal(txt_totalgross1.Text) + Convert.ToDecimal(txt_benefit1.Text);
            txt_benefit_subtotal1.Text = tot1.ToString();

            tot2 = Convert.ToDecimal( txt_gcomm2.Text) + Convert.ToDecimal(txt_oallowance2.Text) + Convert.ToDecimal(txt_gratuity2.Text) + Convert.ToDecimal(txt_noticepay2.Text);
            tot2 = tot2 + Convert.ToDecimal(txt_compensation2.Text) + Convert.ToDecimal(txt_retamt2.Text) + Convert.ToDecimal(txt_conamt2.Text) + Convert.ToDecimal(txt_excess2.Text);
            tot2 = tot2 + Convert.ToDecimal(txt_totalgross2.Text) + Convert.ToDecimal(txt_benefit2.Text);
            txt_benefit_subtotal2.Text = tot2.ToString();

            tot3 = Convert.ToDecimal(txt_gsal1.Text) + Convert.ToDecimal(txt_bonus1.Text) + Convert.ToDecimal(txt_nbonus1.Text) + Convert.ToDecimal(txt_direct_fees1.Text) + tot1;
            txt_totalitem1.Text = tot3.ToString();

            tot4 = Convert.ToDecimal(txt_gsal2.Text) + Convert.ToDecimal(txt_bonus2.Text) + Convert.ToDecimal(txt_nbonus2.Text) + Convert.ToDecimal(txt_direct_fees2.Text) + tot2;
            txt_totalitem2.Text = tot4.ToString();
        }

        void call_pro() {

            SqlDataReader sqlDr = null;
            string sql = "select e.emp_name, e.ic_pp_number,c.Company_name,c.Company_Id from employee e inner join Company c on e.Company_Id=c.Company_Id where  emp_code=" + varEmpCode;

            sqlDr = DataAccess.ExecuteReader(CommandType.Text, sql, null);



            while (sqlDr.Read())
            {
                Emp_name = Convert.ToString(sqlDr["emp_name"].ToString());
                Companyname = Convert.ToString(sqlDr["Company_name"].ToString());
                NricNo = Convert.ToString(sqlDr["ic_pp_number"].ToString());
                compid = sqlDr["Company_Id"].ToString();
            }


            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            
               string  sSQL = "sp_EMP_IR8A_MonthReports";
                SqlParameter[] parms = new SqlParameter[3];
                int y = Convert.ToInt32(yearCode) + 1;
                parms[0] = new SqlParameter("@year", y);
                parms[1] = new SqlParameter("@companyid", Utility.ToInteger( compid));
                parms[2] = new SqlParameter("@EmpCode", Utility.ToInteger(varEmpCode));
                ds = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                  
                  
                    txt_gsal1.Text = dt.Rows[dt.Rows.Count - 1]["GrossPay"].ToString();
                    if (txt_gsal1.Text == "0.00")
                    {
                        txt_gsal1.Text="0";
                    }
                     txt_bonus1.Text = dt.Rows[dt.Rows.Count - 1]["Bonus"].ToString();
                    if (txt_bonus1.Text == "0.00")
                        {
                            txt_bonus1.Text = "0";
                        }
                    txt_direct_fees1.Text = dt.Rows[dt.Rows.Count - 1]["DirectorFee"].ToString ();
                    if (txt_direct_fees1.Text == "0.00")
                    {
                        txt_direct_fees1.Text = "0";
                    }
                    txt_ogcomm1.Text = dt.Rows[dt.Rows.Count - 1]["Commission"].ToString ();
                    if (txt_ogcomm1.Text == "0.00")
                    {
                        txt_ogcomm1.Text = "0";
                    }
                    txt_donationamt1.Text =dt.Rows[dt.Rows.Count - 1]["MBMF"].ToString();
                    if (txt_donationamt1.Text == "0.00")
                    {
                        txt_donationamt1.Text = "0";
                    }

                    txt_oallowance1.Text = Convert.ToString(Convert.ToInt32((Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["TransptAllw"]) + Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["EntAllow"]) + Convert.ToDecimal(dt.Rows[dt.Rows.Count - 1]["OtherAllow"]))));

                    if (txt_oallowance1.Text == "0.00")
                    {
                        txt_oallowance1.Text = "0";
                    }
                    txt_ded_fundamt1.Text = dt.Rows[dt.Rows.Count - 1]["EmployeeCPF"].ToString();
                    if (txt_ded_fundamt1.Text == "0.00")
                    {
                        txt_ded_fundamt1.Text = "0";

                    }
                  

                }
                //-------------------------
                sSQL = "sp_EMP_IR8A_MonthReports";
                SqlParameter[] parms1 = new SqlParameter[3];

                parms1[0] = new SqlParameter("@year", Utility.ToInteger(yearCode));
                parms1[1] = new SqlParameter("@companyid", Utility.ToInteger(compid));
                parms1[2] = new SqlParameter("@EmpCode", Utility.ToInteger(varEmpCode));
                ds1 = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms1);
                DataTable dt1 = new DataTable();
                dt1 = ds1.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txt_gsal2.Text = dt1.Rows[dt1.Rows.Count - 1]["GrossPay"].ToString();
                    if (txt_gsal2.Text == "0.00")
                    {
                        txt_bonus2.Text = "0";
                    }
                    
                    txt_direct_fees2.Text = dt1.Rows[dt1.Rows.Count-1]["DirectorFee"].ToString();
                    if (txt_direct_fees2.Text == "0.00")
                    {
                        txt_direct_fees2.Text = "0";
                    }
                    txt_gcomm2.Text = dt1.Rows[dt1.Rows.Count-1]["Commission"].ToString();
                    if (txt_gcomm2.Text == "0.00")
                    {
                        txt_gcomm2.Text = "0";
                      
                    }
                    txt_donationamt2.Text = dt.Rows[dt.Rows.Count - 1]["MBMF"].ToString();
                    if (txt_donationamt2.Text == "0.00")
                    {
                        txt_donationamt2.Text = "0";
                    }

                    txt_oallowance2.Text = Convert.ToString(Convert.ToInt32((Convert.ToDecimal(dt1.Rows[dt1.Rows.Count - 1]["TransptAllw"]) + Convert.ToDecimal(dt1.Rows[dt1.Rows.Count - 1]["EntAllow"]) + Convert.ToDecimal(dt1.Rows[dt1.Rows.Count - 1]["OtherAllow"]))));

                    if (txt_oallowance2.Text == "0.00")
                    {
                        txt_oallowance1.Text = "0";
                    }
                   txt_ded_fundamt2.Text = dt1.Rows[dt1.Rows.Count - 1]["EmployeeCPF"].ToString();
                   if (txt_ded_fundamt2.Text == "0.00")
                    {
                        txt_ded_fundamt2.Text = "0";
                       
                    }

                }

            
        }
       void  init_textbox(){

                txt_gsal1.Style["text-align"] = "right";
                txt_gsal2.Style["text-align"] = "right";
                txt_bonus1.Style["text-align"] = "right";
                txt_bonus2.Style["text-align"] = "right";
                txt_nbonus1.Style["text-align"] = "right";
                txt_nbonus2.Style["text-align"] = "right";
                txt_direct_fees1.Style["text-align"] = "right";
                txt_direct_fees2.Style["text-align"] = "right";
                txt_gcomm2.Style["text-align"] = "right";
                txt_ogcomm1.Style["text-align"] = "right";
                txt_oallowance1.Style["text-align"] = "right";
                txt_oallowance2.Style["text-align"] = "right";
                txt_gratuity1.Style["text-align"] = "right";
                txt_gratuity2.Style["text-align"] = "right";
                txt_noticepay1.Style["text-align"] = "right";
                txt_noticepay2.Style["text-align"] = "right";
                txt_compensation1.Style["text-align"] = "right";
                txt_compensation2.Style["text-align"] = "right";
                txt_benefit1.Style["text-align"] = "right";
                txt_benefit2.Style["text-align"] = "right";
                txt_conamt1.Style["text-align"] = "right";
                txt_conamt2.Style["text-align"] = "right";
                txt_excess1.Style["text-align"] = "right";
                txt_excess2.Style["text-align"] = "right";
                txt_totalgross1.Style["text-align"] = "right";
                txt_totalgross2.Style["text-align"] = "right";
                txt_benefit_subtotal1.Style["text-align"] = "right";
                txt_benefit_subtotal2.Style["text-align"] = "right";
                txt_totalgross1.Style["text-align"] = "right";
                txt_totalgross2.Style["text-align"] = "right";
                txt_totalitem1.Style["text-align"] = "right";
                txt_totalitem2.Style["text-align"] = "right";
                txt_donationamt1.Style["text-align"] = "right";
                txt_donationamt2.Style["text-align"] = "right";
                txt_ded_fundamt1.Style["text-align"] = "right";
                txt_ded_fundamt2.Style["text-align"] = "right";
                txt_contamt1.Style["text-align"] = "right";
                txt_contamt2.Style["text-align"] = "right";
                txt_retamt1.Style["text-align"] = "right";
                txt_retamt2.Style["text-align"] = "right";
                txttaxpubtot1.Style["text-align"] = "right";
                txttaxpubtot2.Style["text-align"] = "right";
                txtdriv2.Style["text-align"] = "right";
                txtgardener1.Style["text-align"] = "right";
                txtgardener2.Style["text-align"] = "right";
                txtocc1.Style["text-align"] = "right";
                txtocc2.Style["text-align"] = "right";
                txtFurn1.Style["text-align"] = "right";
                txtFurn2.Style["text-align"] = "right";
                txtdrent1.Style["text-align"] = "right";
                txtdrent2.Style["text-align"] = "right";
                txtdrent1.Style["text-align"] = "right";
                txtdrent2.Style["text-align"] = "right";
                txtDtot1.Style["text-align"] = "right";
                txtDtot2.Style["text-align"] = "right";
                txtdtotal1.Style["text-align"] = "right";
                txtdtotal2.Style["text-align"] = "right";
                txtres1.Style["text-align"] = "right";
                txtres2.Style["text-align"] = "right";
                txtoccA1.Style["text-align"] = "right";
                txtoccA2.Style["text-align"] = "right";
                txtAnnA1.Style["text-align"] = "right";
                txtAnnA2.Style["text-align"] = "right";
                txtfurA1.Style["text-align"] = "right";
                TextBox2.Style["text-align"] = "right";
                
                txtacctA1.Style["text-align"] = "right";
                txtacctA2.Style["text-align"] = "right";
                txtrentA1.Style["text-align"] = "right";
                txtrentA2.Style["text-align"] = "right";
                txttaxtotA1.Style["text-align"] = "right";
                txttaxtotA2.Style["text-align"] = "right";
                txttottax1.Style["text-align"] = "right";
                txttottax2.Style["text-align"] = "right";
                txtutilit1.Style["text-align"] = "right";
                txtutilit2.Style["text-align"] = "right";
                txtddriver1.Style["text-align"] = "right";
                txtddriver2.Style["text-align"] = "right";
                txtServ1.Style["text-align"] = "right";
                txtServ2.Style["text-align"] = "right";
                txtDtot1.Style["text-align"] = "right";
                txtDtot2.Style["text-align"] = "right";
                txthote1.Style["text-align"] = "right";
                txthote2.Style["text-align"] = "right";
                txtEtax1.Style["text-align"] = "right";
                txtEtax2.Style["text-align"] = "right";
                txtcost1.Style["text-align"] = "right";
                txtcost2.Style["text-align"] = "right";
                txtinsur1.Style["text-align"] = "right";
                txtinsur2.Style["text-align"] = "right";
                txtsub1.Style["text-align"] = "right";
                txtsub2.Style["text-align"] = "right";
                txtEducat1.Style["text-align"] = "right";
                txtmonetary1.Style["text-align"] = "right";
                txtmonetary2.Style["text-align"] = "right";
                txtenterance1.Style["text-align"] = "right";
                txtenterance2.Style["text-align"] = "right";
                txtgains1.Style["text-align"] = "right";
                txtgains2.Style["text-align"] = "right";
                txtfullcost1.Style["text-align"] = "right";
                txtfullcost2.Style["text-align"] = "right";
                txtcarbenefit1.Style["text-align"] = "right";
                txtcarbenefit2.Style["text-align"] = "right";
                txtothbenef1.Style["text-align"] = "right";
                txtothbenef2.Style["text-align"] = "right";
                txtpayment1.Style["text-align"] = "right";
                txtpayment2.Style["text-align"] = "right";
                txtIRTot1.Style["text-align"] = "right";
                txtIRTot2.Style["text-align"] = "right";
                txtFtot2.Style["text-align"] = "right";
                TextBox3.Style["text-align"] = "right";
                txtnumday1.Style["text-align"] = "right";
                txtnumday2.Style["text-align"] = "right";
                txtAnn1.Style["text-align"] = "right";
                txtAnn2.Style["text-align"] = "right";
                txta6b9_tot1.Style["text-align"] = "right";
                txta6b9_tot2.Style["text-align"] = "right";
                others_total1.Style["text-align"] = "right";
                others_total2.Style["text-align"] = "right";
                txtpub1.Style["text-align"] = "right";
                txtpub2.Style["text-align"] = "right";
                TextBox1.Style["text-align"] = "right";
                txtdriv2.Style["text-align"] = "right";
                txtact1.Style["text-align"] = "right";
                txtact2.Style["text-align"] = "right";
               txttaxtot1.Style["text-align"] = "right";
               txttaxtot2.Style["text-align"] = "right";



           
           //----------
                txtselfC1.Style["text-align"] = "right";
                txtselfC2.Style["text-align"] = "right";
                txtwifeC1.Style["text-align"] = "right";
                txtwifeC2.Style["text-align"] = "right";
                txtchil3C1.Style["text-align"] = "right";
                txtchil3C2.Style["text-align"] = "right";
                txtChil8C1.Style["text-align"] = "right";
                txtChil8C2.Style["text-align"] = "right";
                txtchilC1.Style["text-align"] = "right";
                txtchilC2.Style["text-align"] = "right";
                txtbasic1.Style["text-align"] = "right";
                txtbasic2.Style["text-align"] = "right";
                txtHottot1.Style["text-align"] = "right";
                txtHottot2.Style["text-align"] = "right";
                txtnumday1.Style["text-align"] = "right";
                txtnumday2.Style["text-align"] = "right";
                txtannu1.Style["text-align"] = "right";
                txtannu2.Style["text-align"] = "right";
                txtfur1.Style["text-align"] = "right";
                txtfur2.Style["text-align"] = "right";
                txtacctA1.Style["text-align"] = "right";
                txtacctA2.Style["text-align"] = "right";
                txtrent1.Style["text-align"] = "right";
                txtrent2.Style["text-align"] = "right";
                txttaxpubtot1.Style["text-align"] = "right";
                txttaxpubtot2.Style["text-align"] = "right";


                txtfur1.Style["text-align"] = "right";
                txtfur2.Style["text-align"] = "right";
                txtRef1.Style["text-align"] = "right";
                txtRef2.Style["text-align"] = "right";
                txtwas1.Style["text-align"] = "right";
                txtwas2.Style["text-align"] = "right";
                txtAir1.Style["text-align"] = "right";
                txtAir2.Style["text-align"] = "right";
                txtDining1.Style["text-align"] = "right";
                txtDining2.Style["text-align"] = "right";
                txtAddrom1.Style["text-align"] = "right";
                txtAddrom2.Style["text-align"] = "right";
                txtTV1.Style["text-align"] = "right";
                txtTV2.Style["text-align"] = "right";
                txtCom1.Style["text-align"] = "right";
                txtCom2.Style["text-align"] = "right";
                txtswim1.Style["text-align"] = "right";
                txtswim2.Style["text-align"] = "right";
                txttaxpubtot1.Style["text-align"] = "right";
                txttaxpubtot2.Style["text-align"] = "right";
           //--

                txtauthoris.Style["text-align"] = "center";
                txtdesign.Style["text-align"] = "center";
                txtnumemp1.Style["text-align"] = "right";
                txtnumemp2.Style["text-align"] = "right";

                txt_chilename1.Text = "";
                txt_chilename2.Text = "";
                txt_chilename3.Text = "";
                txt_chilename4.Text = "";
                txt_cschool1.Text = "";
                txt_cschool2.Text = "";
                txt_cschool3.Text = "";
                txt_cschool4.Text = "";
                txt_ded_fundname.Text = "";
                txt_designation.Text = "";
                txt_conname.Text = "";
                txt_contact.Text = "";
                txt_bkname.Text = "";
                txt_email.Text = "";
                txt_others.Text = "";
                txt_spname.Text = "";
                txt_spnationality.SelectedIndex = -1;
                txtadder1.Text = "";
                txtadder2.Text ="";
                txt_compreasion.Text = "";
                f21_aname.Text = "";
                f21_contno.Text = "";
                f21_contper.Text = "";
                f21_design.Text = "";
                f21_email.Text = "";
                f21_fax.Text = "";
                txt_nric.Text = "";
                txt_malic.Text = "";
                txt_bkname.Text = "";
                txt_employername.Text = "";
                txt_retname.Text = "";
                txt_periodsalary.Text = "";
                txt_borneamt.Text = "";
                txt_spemployername.Text = "";
           
           //--------------

                txt_bonus2.ReadOnly = true;
                txt_nbonus2.ReadOnly = true;
                txt_gcomm2.ReadOnly = true;
                txt_oallowance2.ReadOnly = true;
                txt_gratuity2.ReadOnly = true;
                txt_noticepay2.ReadOnly = true;
                txt_compensation2.ReadOnly = true;
                txt_retamt2.ReadOnly = true;
                txt_conamt2.ReadOnly = true;
                txt_excess2.ReadOnly = true;
                txt_ded_fundamt2.ReadOnly = true;
                txt_donationamt2.ReadOnly = true;
                txt_contamt2.ReadOnly = true;
                txt_direct_fees2.ReadOnly = true;

        }
        protected void chkpyc_CheckedChanged(Object sender, EventArgs args)
        {
            if (chkpyc.Checked)
            {

                txt_bonus2.ReadOnly = false;
                txt_nbonus2.ReadOnly = false;
                txt_gcomm2.ReadOnly = false;
                txt_oallowance2.ReadOnly = false;
                txt_gratuity2.ReadOnly = false;
                txt_noticepay2.ReadOnly = false;
                txt_compensation2.ReadOnly = false;
                txt_retamt2.ReadOnly = false;
                txt_conamt2.ReadOnly = false;
                txt_excess2.ReadOnly = false;
                txt_ded_fundamt2.ReadOnly = false;
                txt_donationamt2.ReadOnly = false;
                txt_contamt2.ReadOnly = false;
                txt_direct_fees2.ReadOnly = false;

            }
            else {

                txt_bonus2.ReadOnly = true;
                txt_nbonus2.ReadOnly = true;
                txt_gcomm2.ReadOnly = true;
                txt_oallowance2.ReadOnly = true;
                txt_gratuity2.ReadOnly = true;
                txt_noticepay2.ReadOnly = true;
                txt_compensation2.ReadOnly = true;
                txt_retamt2.ReadOnly = true;
                txt_conamt2.ReadOnly = true;
                txt_excess2.ReadOnly = true;
                txt_ded_fundamt2.ReadOnly = true;
                txt_donationamt2.ReadOnly = true;
                txt_contamt2.ReadOnly = true;
                txt_direct_fees2.ReadOnly = true;
            
            }
        
        }
        protected void chk1_CheckedChanged(Object sender, EventArgs args)
        {
            RadTab   Tab = tbsEmp.FindTabByText("APPENDIX 1");
            if (Tab.Enabled == true)
            {
                Tab.Enabled = false;
            }
            else {
                Tab.Enabled = true; 
            }
            
            
            
        }
        protected void chk2_CheckedChanged(Object sender, EventArgs args)
        {
            RadTab Tab = tbsEmp.FindTabByText("APPENDIX 2");
            if (Tab.Enabled == true)
            {
                Tab.Enabled = false;
            }
            else
            {
                Tab.Enabled = true;
            }
        }
        protected void chk3_CheckedChanged(Object sender, EventArgs args)
        {
            RadTab Tab = tbsEmp.FindTabByText("APPENDIX 3");
            if (Tab.Enabled == true)
            {
                Tab.Enabled = false;
            }
            else
            {
                Tab.Enabled = true;
            }
        }


        protected void chk_a1completed_CheckedChanged(Object sender, EventArgs args)
        {
           
            if (chk_a1completed.Checked)
            {
                txt_benefit1.Text = TextBox3.Text;
                txt_benefit2.Text = txtIRTot2.Text;

            }
            else {
                txt_benefit1.Text = "0";
                txt_benefit2.Text = "0";
            }
        }

    }
}
