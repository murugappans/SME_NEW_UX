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
using Telerik.Web.UI;

namespace SMEPayroll.Management
{
    public partial class Ir8aSetup : System.Web.UI.Page
    {
        # region Style
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;

        string varEmpCode = "";
        # endregion Style
        string NRIC = "";
        string yearCode = null;
        public string faddress1 = null;
        public string faddress2 = null;
        public string fPostalCode = null;
        public string block_no = null;
        public string Level_no = null;
        public string Unit_no = null;
        public string postal_code = null;
        public string strname = null;

        int compid;
        protected void Page_Load(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            ds = null;
            string sSQL = "";
            string NRIC = "";
            compid = Utility.ToInteger(Session["Compid"]);
            varEmpCode = Request.QueryString["empcode"].ToString();
            yearCode = Request.QueryString["year"].ToString();
            SqlDataReader sqlDr = null;
            AjaxPro.Utility.RegisterTypeForAjax(typeof(Ir8aSetup));
            sSQL = "Select block_no,Level_no,Unit_no,postal_code,STREET_NAME, foreignAddress1,foreignAddress2,foreignPostalCode From Employee where emp_code=" + varEmpCode + " and company_id=" + compid;
            sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
            while (sqlDr.Read())
            {
                block_no = Convert.ToString(sqlDr["block_no"].ToString());
                Level_no = Convert.ToString(sqlDr["Level_no"].ToString());
                Unit_no = Convert.ToString(sqlDr["Unit_no"].ToString());
                postal_code = Convert.ToString(sqlDr["postal_code"].ToString());
                strname = Convert.ToString(sqlDr["STREET_NAME"].ToString());
                faddress1 = Convert.ToString(sqlDr["foreignAddress1"].ToString());
                faddress2 = Convert.ToString(sqlDr["foreignAddress2"].ToString());
                fPostalCode = Convert.ToString(sqlDr["foreignPostalCode"].ToString());
            }
          if (oHidden.Value == "Save")
            {
                empSavebasicIr8a();
                EmpSave();
                empSaveIr8aAppendixB();
                //string sqlQuery = "SELECT RecordType,IDType,IDNo ,NameLine1,NameLine2,ResidencePlaceValue,ResidenceAddressLine1,ResidenceAddressLine2,ResidenceAddressLine3,OccupationFromDate,OccupationToDate,NoOfDays,AVOrRentByEmployer,RentByEmployee,FurnitureValue,HardOrsoftFurnitureItemsValue,RefrigeratorValue,NoOfRefrigerators,VideoRecorderValue,NoOfVideoRecorders,WashingMachineDryerDishWasherValue,NoOfWashingMachines,NoOfDryers,NoOfDishWashers,AirConditionerValue,NoOfAirConditioners,NoOfCentralACDining,NoOfCentralACSitting,NoOfCentralACAdditional,TVRadioAmpHiFiStereoElectriGuitarValue,NoOfTVs,NoOfRadios,NoOfAmplifiers,NoOfHiFiStereos,NoOfElectriGuitar,ComputerAndOrganValue,NoOfComputers,NoOfOrgans,SwimmingPoolValue,NoOfSwimmingPools,PublicUtilities,Telephone,Pager,Suitcase,GolfBagAndAccessories,Camera,Servant,Driver,GardenerOrCompoundUpkeep,OtherBenefitsInKindValue,HotelAccommodationValue,SelfWifeChildAbove20NoOfPersons,SelfWifeChildAbove20NoOfDays,SelfWifeChildAbove20Value,ChildBetween8And20NoOfPersons,ChildBetween8And20NoOfDays,ChildBetween8And20Value,ChildBetween3And7NoOfPersons,ChildBetween3And7NoOfDays,ChildBetween3And7Value,ChildBelow3NoOfPersons,ChildBelow3NoOfDays,ChildBelow3Value,Percent2OfBasic,CostOfLeavePassageAndIncidentalBenefits,NoOfLeavePassageSelf,NoOfLeavePassageWife,NoOfLeavePassageChildren,OHQStatus,InterestPaidByEmployer,LifeInsurancePremiumsPaidByEmployer,FreeOrSubsidisedHoliday,EducationalExpenses,NonMonetaryAwardsForLongService,EntranceOrTransferFeesToSocialClubs,GainsFromAssets,FullCostOfMotorVehicle,CarBenefit,OthersBenefits,TotalBenefitsInKind,NoOfEmployeesSharingQRS,Filler,Remarks from IR8AAPENDIX_EMPLOYEE";
                //ds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
                //overWriteIR8AXml();
                //appendIR8AAppendixTemplateXml(ds);
            }
            if (!Page.IsPostBack)
            {
                string sDate = null;
                DataSet ds_ir8a = new DataSet();
                string sqlQuery = "select *,CONVERT(VARCHAR(10), CONVERT(datetime,dateofcessation, 105), 103)  [dateofcessationconv],CONVERT(VARCHAR(10), CONVERT(datetime,dateofcommencement, 105), 103)  [dateofcommencementconv] from employee_ir8a where emp_id =" + varEmpCode + " and ir8a_year='" + yearCode + "'";
                ds_ir8a = getDataSet(sqlQuery);
                if (ds_ir8a.Tables[0].Rows.Count > 0)
                {
                    object obj1_tax_borne_employer = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer"];
                    object obj2_tax_borne_employer_options = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_options"];
                    object obj3_tax_borne_employer_amount = ds_ir8a.Tables[0].Rows[0]["tax_borne_employer_amount"];
                    object obj4_pension_out_singapore = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore"];
                    object obj5_pension_out_singapore_amount = ds_ir8a.Tables[0].Rows[0]["pension_out_singapore_amount"];
                    object obj6_excess_voluntary_cpf_employer = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer"];
                    object obj7_excess_voluntary_cpf_employer_amount = ds_ir8a.Tables[0].Rows[0]["excess_voluntary_cpf_employer_amount"];
                    object obj8_stock_options = ds_ir8a.Tables[0].Rows[0]["stock_options"];
                    object obj9_stock_options_amount = ds_ir8a.Tables[0].Rows[0]["stock_options_amount"];
                    object obj10_benefits_in_kind = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind"];
                    object obj11_benefits_in_kind_amount = ds_ir8a.Tables[0].Rows[0]["benefits_in_kind_amount"];
                    object obj12_retirement_benefits = ds_ir8a.Tables[0].Rows[0]["retirement_benefits"];
                    object obj13_retirement_benefits_fundName = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_fundName"];
                    object obj14_retirement_benefits_amount = ds_ir8a.Tables[0].Rows[0]["retirement_benefits_amount"];
                    object obj15_s45_tax_on_directorFee = ds_ir8a.Tables[0].Rows[0]["s45_tax_on_directorFee"];
                    object obj16_cessation_provision = ds_ir8a.Tables[0].Rows[0]["cessation_provision"];
                    object obj17_addr_type = ds_ir8a.Tables[0].Rows[0]["addr_type"];
                    object obj18_dateofcessationconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcessationconv"]);
                    object obj19_dateofcommencementconv = Convert.ToString(ds_ir8a.Tables[0].Rows[0]["dateofcommencementconv"]);

                    cmbIR8A_year.Items.FindByText(yearCode).Selected = true;

                    if (obj1_tax_borne_employer != DBNull.Value)
                    {
                        cmbtaxbornbyemployer.Items.FindByValue(obj1_tax_borne_employer.ToString()).Selected = true;
                    }
                    if (obj2_tax_borne_employer_options != DBNull.Value)
                    {
                        cmbtaxbornbyemployerFPHN.Items.FindByValue(obj2_tax_borne_employer_options.ToString()).Selected = true;
                    }
                    if (obj3_tax_borne_employer_amount != DBNull.Value)
                    {
                        txttaxbornbyempamt.Value = obj3_tax_borne_employer_amount.ToString();
                    }
                    if (obj4_pension_out_singapore != DBNull.Value)
                    {
                        cmbpensionoutsing.Items.FindByText(obj4_pension_out_singapore.ToString()).Selected = true;
                    }
                    if (obj5_pension_out_singapore_amount != DBNull.Value)
                    {
                        txtpensionoutsing.Value = obj5_pension_out_singapore_amount.ToString();
                    }
                    if (obj6_excess_voluntary_cpf_employer != DBNull.Value)
                    {
                        cmbexcessvolcpfemp.Items.FindByText(obj6_excess_voluntary_cpf_employer.ToString()).Selected = true;
                    }
                    if (obj7_excess_voluntary_cpf_employer_amount != DBNull.Value)
                    {
                        txtexcessvolcpfemp.Value = obj7_excess_voluntary_cpf_employer_amount.ToString();
                    }
                    if (obj8_stock_options != DBNull.Value)
                    {
                        cmbstockoption.Items.FindByText(obj8_stock_options.ToString()).Selected = true;
                    }
                    if (obj9_stock_options_amount != DBNull.Value)
                    {
                        txtstockoption.Value = obj9_stock_options_amount.ToString();
                    }
                    if (obj10_benefits_in_kind != DBNull.Value)
                    {
                        cmbbenefitskind.Items.FindByText(obj10_benefits_in_kind.ToString()).Selected = true;
                    }
                    if (obj11_benefits_in_kind_amount != DBNull.Value)
                    {
                        txtbenefitskind.Value = obj11_benefits_in_kind_amount.ToString();
                    }
                    if (obj12_retirement_benefits != DBNull.Value)
                    {
                        cmbretireben.Items.FindByText(obj12_retirement_benefits.ToString()).Selected = true;
                    }
                    if (obj13_retirement_benefits_fundName != DBNull.Value)
                    {
                        txtretirebenfundname.Value = obj13_retirement_benefits_fundName.ToString();
                    }
                    if (obj14_retirement_benefits_amount != DBNull.Value)
                    {
                        txtbretireben.Value = obj14_retirement_benefits_amount.ToString();
                    }
                    if (obj15_s45_tax_on_directorFee != DBNull.Value)
                    {
                        staxondirector.Items.FindByText(obj15_s45_tax_on_directorFee.ToString()).Selected = true;
                    }
                    if (obj16_cessation_provision != DBNull.Value)
                    {
                        cmbcessprov.Items.FindByText(obj16_cessation_provision.ToString()).Selected = true;
                    }
                    if (obj17_addr_type != DBNull.Value)
                    {
                        if (cmbaddress.Items.Count > 0)
                        {
                            cmbaddress.Items.FindByValue(obj17_addr_type.ToString()).Selected = true;
                        }
                    }

                    IFormatProvider culture = new System.Globalization.CultureInfo("en-GB", true);

                    DateTime mDate;
                    if (obj18_dateofcessationconv != DBNull.Value)
                    {
                        sDate = obj18_dateofcessationconv.ToString();

                        if (sDate.Length > 0)
                            dtcessdate.SelectedDate = System.DateTime.Parse(sDate);
                    }
                    if (obj19_dateofcommencementconv != DBNull.Value)
                    {
                        sDate = obj19_dateofcommencementconv.ToString();
                        if (sDate.Length > 0)
                            dtcommdate.SelectedDate = System.DateTime.Parse(sDate); ;
                    }
                    tbsEmp.Tabs[1].Enabled = true;
                    if (txtstockoption.Value != "")
                    {
                        tbsEmp.Tabs[2].Enabled = true;
                    }
                    FillRateTextBox();
                    FillappendixAtextBox();
                }

            }
        }

        private void FillRateTextBox()
        {
            string sqlQuery = " SELECT * FROM IR8ALIST";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlQuery, null);
            if (dr.Read())
            {
                txtCostHard.Value = dr["FurnitureHard"].ToString();
                txtCostSoft.Value = dr["FurnitureSoft"].ToString();
                txtCostRef.Value = dr["Refigerator"].ToString();
                txtCostVCD.Value = dr["VideoPlayer"].ToString();
                txtCostWashingMachine.Value = dr["WashingMachine"].ToString();
                txtCostDryer.Value = dr["Dryer"].ToString();
                txtCostDish.Value = dr["DishWasher"].ToString();
                txtCostAc.Value = dr["AirConditionerUnit"].ToString();

                txtCostAcdining.Value = dr["AirConditionerDining"].ToString();
                txtCostACsitting.Value = dr["AirConditionerSitting"].ToString();
                txtCostAcAdditional.Value = dr["AirConditionerAdditional"].ToString();

                txtCostTV.Value = dr["HomeTheater"].ToString();
                txtCostRadio.Value = dr["Radio"].ToString();
                txtCostHifi.Value = dr["HIFiStereo"].ToString();
                // Dont Delete
                txtCostGuitar.Value = dr["ElectricGuitar"].ToString();

                txtCostComputer.Value = dr["Computer"].ToString();
                txtCostOrgan.Value = dr["Organ"].ToString();
                txtCostPool.Value = dr["SwimmingPool"].ToString();
                txtAccomodationSelfRate.Value = dr["SelfAccomodation"].ToString();
                //            Children2yrAccomodation 
                //Children7yrAccomodation 
                //Children20yrAccomodation 
                txtChildren2yrAccomodationRate.Value = dr["Children2yrAccomodation"].ToString();
                txtChildren7yrAccomodationRate.Value = dr["Children7yrAccomodation"].ToString();
                txtChildren20yrAccomodationRate.Value = dr["Children20yrAccomodation"].ToString();
                //txtCostUtilities.Value = dr["HardFurniture"].ToString();
                //txtCostTelephone.Value = dr["HardFurniture"].ToString();
                //txtCostPager.Value = dr["HardFurniture"].ToString();
                //txtCostSuitcase.Value = dr["HardFurniture"].ToString();
                //txtCostAccessories.Value = dr["HardFurniture"].ToString();
                //txtCostCamera.Value = dr["HardFurniture"].ToString();
                //txtCostServant.Value = dr["HardFurniture"].ToString();
                //txtCostDriver.Value = dr["HardFurniture"].ToString();
                //txtCostGardener.Value = dr["HardFurniture"].ToString();
                //Text107.Value = "1";

            }
        }
        private void empSavebasicIr8a()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            object obj1_tax_borne_employer = cmbtaxbornbyemployer.Value;
            object obj2_tax_borne_employer_options = cmbtaxbornbyemployerFPHN.Value;
            object obj3_tax_borne_employer_amount = txttaxbornbyempamt.Value;
            object obj4_pension_out_singapore = cmbpensionoutsing.Value;
            object obj5_pension_out_singapore_amount = txtpensionoutsing.Value;
            object obj6_excess_voluntary_cpf_employer = cmbexcessvolcpfemp.Value;
            object obj7_excess_voluntary_cpf_employer_amount = txtexcessvolcpfemp.Value;
            object obj8_stock_options = cmbstockoption.Value;
            object obj9_stock_options_amount = txtstockoption.Value;
            object obj10_benefits_in_kind = cmbbenefitskind.Value;
            object obj11_benefits_in_kind_amount = txtbenefitskind.Value;
            object obj12_retirement_benefits = cmbretireben.Value;
            object obj13_retirement_benefits_fundName = txtretirebenfundname.Value;
            object obj14_retirement_benefits_amount = txtbretireben.Value;
            object obj15_s45_tax_on_directorFee = staxondirector.Value;
            object obj16_cessation_provision = cmbcessprov.Value;
            object obj17_addr_type = cmbaddress.SelectedItem.Value;
            object obj18_dateofcessationconv = null;
            object obj19_dateofcommencementconv = null;
            string sDate = "";
            if (dtcessdate.SelectedDate == null)
            {
                obj18_dateofcessationconv = null;
            }
            else
            {
                sDate = dtcessdate.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj18_dateofcessationconv = sDate;

            }
            if (dtcommdate.SelectedDate == null)
            {

                obj19_dateofcommencementconv = null;
            }
            else
            {
                sDate = dtcommdate.SelectedDate.ToString();
                sDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
                obj19_dateofcommencementconv = sDate;

            }





            int recFound = 0;
            string sql = "select * from employee_ir8a where emp_id=" + varEmpCode + " and ir8a_year = " + yearCode;
            DataSet sqlds = DataAccess.FetchRS(CommandType.Text, sql, null);
            if (sqlds.Tables[0].Rows.Count == 0)
            {
                sql = "insert into employee_ir8a (emp_id,ir8a_year,tax_borne_employer ,tax_borne_employer_options ,tax_borne_employer_amount,pension_out_singapore,pension_out_singapore_amount,excess_voluntary_cpf_employer,excess_voluntary_cpf_employer_amount,stock_options,stock_options_amount,benefits_in_kind ,benefits_in_kind_amount,retirement_benefits,retirement_benefits_fundname,retirement_benefits_amount ,s45_tax_on_directorFee,cessation_Provision,dateofcessation,dateofcommencement) VALUES( '" + varEmpCode + "','" + yearCode + "','" + obj1_tax_borne_employer + "','" + obj2_tax_borne_employer_options + "','" + obj3_tax_borne_employer_amount + "','" + obj4_pension_out_singapore + "','" + obj5_pension_out_singapore_amount + "','" + obj6_excess_voluntary_cpf_employer + "','" + obj7_excess_voluntary_cpf_employer_amount + "','" + obj8_stock_options + "','" + obj9_stock_options_amount + "','" + obj10_benefits_in_kind + "','" + obj11_benefits_in_kind_amount + "','" + obj12_retirement_benefits + "','" + obj13_retirement_benefits_fundName + "','" + obj14_retirement_benefits_amount + "','" + obj15_s45_tax_on_directorFee + "','" + obj16_cessation_provision + "','" + obj18_dateofcessationconv + "','" + obj19_dateofcommencementconv + "')";
                recFound = DataAccess.ExecuteNonQuery(sql, null);
            }
            else
            {
                //sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount + "' ,pension_out_singapore = '" + obj4_pension_out_singapore + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount + "',stock_options = '" + obj8_stock_options + "' ,stock_options_amount = '" + obj9_stock_options_amount + "' ,benefits_in_kind = '" + obj10_benefits_in_kind + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount + "' ,retirement_benefits_fundname = '" + obj12_retirement_benefits + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee + "' ,cessation_Provision = '" + obj16_cessation_provision + "' ,dateofcessation = '" + obj17_addr_type + "' , dateofcommencement = '" + obj18_dateofcessationconv + "'  where emp_id= " + varEmpCode;
                sql = "update employee_ir8a set  tax_borne_employer = '" + obj1_tax_borne_employer + "' ,tax_borne_employer_options = '" + obj2_tax_borne_employer_options + "' ,tax_borne_employer_amount = '" + obj3_tax_borne_employer_amount + "' ,pension_out_singapore = '" + obj4_pension_out_singapore + "' ,pension_out_singapore_amount = '" + obj5_pension_out_singapore_amount + "' ,excess_voluntary_cpf_employer = '" + obj6_excess_voluntary_cpf_employer + "' ,excess_voluntary_cpf_employer_amount = '" + obj7_excess_voluntary_cpf_employer_amount + "',stock_options = '" + obj8_stock_options + "' ,stock_options_amount = '" + obj9_stock_options_amount + "' ,benefits_in_kind = '" + obj10_benefits_in_kind + "' ,benefits_in_kind_amount = '" + obj11_benefits_in_kind_amount + "' ,retirement_benefits_fundname = '" + obj13_retirement_benefits_fundName + "' ,retirement_benefits_amount = '" + obj14_retirement_benefits_amount + "' ,s45_tax_on_directorFee = '" + obj15_s45_tax_on_directorFee + "' ,cessation_Provision = '" + obj16_cessation_provision + "' ,addr_type='" + obj17_addr_type + "' ,dateofcessation = '" + obj18_dateofcessationconv + "' , dateofcommencement = '" + obj19_dateofcommencementconv + "',retirement_benefits='" + obj12_retirement_benefits + "' where emp_id= " + varEmpCode + " and ir8a_year= " + Utility.ToInteger(Utility.ToInteger(yearCode));
                recFound = DataAccess.ExecuteNonQuery(sql, null);
            }




        }
        private void EmpSave()
        {
            IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
            decimal totalFurnitureValue = 0;
            string sDate = null;
            string FurnitureValue, noOfHard,HfurnitureValue ,sfurnitureValue,NoOfSoft,HardOrsoftFurnitureItemsValue, RefrigeratorValue, NoOfRefrigerators, VideoRecorderValue, NoOfVideoRecorders, WashingMachineDryerDishWasherValue, NoOfWashingMachines, NoOfDryers, NoOfDishWashers, AirConditionerValue, NoOfAirConditioners, NoOfCentralACDining, NoOfCentralACSitting, NoOfCentralACAdditional, TVRadioAmpHiFiStereoElectriGuitarValue, NoOfTVs, NoOfRadios, NoOfAmplifiers, NoOfHiFiStereos, NoOfElectriGuitar, ComputerAndOrganValue, NoOfComputers, NoOfOrgans, SwimmingPoolValue, NoOfSwimmingPools, PublicUtilities, Telephone, Pager, Suitcase, GolfBagAndAccessories, Camera, Servant, Driver, GardenerOrCompoundUpkeep, OtherBenefitsInKindValue, HotelAccommodationValue, SelfWifeChildAbove20NoOfPersons, SelfWifeChildAbove20NoOfDays, SelfWifeChildAbove20Value, ChildBetween8And20NoOfPersons, ChildBetween8And20NoOfDays, ChildBetween8And20Value, ChildBetween3And7NoOfPersons, ChildBetween3And7NoOfDays, ChildBetween3And7Value, ChildBelow3NoOfPersons, ChildBelow3NoOfDays, ChildBelow3Value, Percent2OfBasic, CostOfLeavePassageAndIncidentalBenefits, NoOfLeavePassageSelf, NoOfLeavePassageWife, NoOfLeavePassageChildren, OHQStatus, InterestPaidByEmployer, LifeInsurancePremiumsPaidByEmployer, FreeOrSubsidisedHoliday, EducationalExpenses, NonMonetaryAwardsForLongService, EntranceOrTransferFeesToSocialClubs, GainsFromAssets, FullCostOfMotorVehicle, CarBenefit, OthersBenefits, TotalBenefitsInKind, NoOfEmployeesSharingQRS, Filler, Remarks;
            FurnitureValue = "";

            string ResidencePlaceValue, ResidenceAddressLine1, ResidenceAddressLine2, ResidenceAddressLine3, OccupationFromDate, OccupationToDate, NoOfDays, AVOrRentByEmployer, RentByEmployee;

            ResidencePlaceValue = "0";
            ResidenceAddressLine1 = txtAddress1.Value.ToString();
            ResidenceAddressLine2 = txtAddress2.Value.ToString();
            ResidenceAddressLine3 = txtAddress3.Value.ToString();
            OccupationFromDate = txtFrom.SelectedDate.ToString();
            sDate = OccupationFromDate;
            OccupationFromDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);
            OccupationToDate = txtTo.SelectedDate.ToString();
            sDate = OccupationToDate;
            OccupationToDate = Convert.ToDateTime(sDate.ToString()).ToString("MM/dd/yyyy", format);

            NoOfDays = txtNoOfDays.Value.ToString();
            AVOrRentByEmployer = txtEmployerRent.Value.ToString();
            RentByEmployee = txtEmployeeRent.Value.ToString();

            RefrigeratorValue = txtTotalRef.Value;
            NoOfRefrigerators = txtRef.Value;
            VideoRecorderValue = txtTotalVcd.Value;
            NoOfVideoRecorders = txtVcd.Value;
            
            noOfHard = Utility.ToString(txtFHard.Value);
            HfurnitureValue = Utility.ToString(txtTotalHard.Value);

            NoOfSoft = Utility.ToString(txtFSoft.Value);
            sfurnitureValue = Utility.ToString(txtTotalSoft.Value);

            HardOrsoftFurnitureItemsValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(RefrigeratorValue)) + Convert.ToDecimal(Utility.ToDouble(VideoRecorderValue)));
            totalFurnitureValue = Convert.ToDecimal(HardOrsoftFurnitureItemsValue);

            NoOfWashingMachines = txtWashingMachine.Value;

            NoOfDryers = txtDryer.Value;

            NoOfDishWashers = txtDish.Value;
            WashingMachineDryerDishWasherValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalWashingMachine.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalDish.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(WashingMachineDryerDishWasherValue);
            NoOfAirConditioners = txtAc.Value;

            NoOfCentralACDining = txtAcdining.Value;

            NoOfCentralACSitting = txtACsitting.Value;

            NoOfCentralACAdditional = txtAcAdditional.Value;

            AirConditionerValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalAc.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalAcdining.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalACsitting.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalAcAdditional.Value)));
            totalFurnitureValue = totalFurnitureValue + Convert.ToDecimal(AirConditionerValue);
            NoOfTVs = txtTV.Value;
            NoOfRadios = txtRadio.Value;
            NoOfAmplifiers = "";
            NoOfHiFiStereos = txtHifi.Value;
            NoOfElectriGuitar = txtGuitar.Value;

            TVRadioAmpHiFiStereoElectriGuitarValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalTV.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalRadio.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalHifi.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalGuitar.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(TVRadioAmpHiFiStereoElectriGuitarValue);

            NoOfComputers = txtComputer.Value;
            NoOfOrgans = txtOrgan.Value;
            ComputerAndOrganValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalComputer.Value)) + Convert.ToDecimal(Utility.ToDouble(txtTotalOrgan.Value)));

            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(ComputerAndOrganValue);
            SwimmingPoolValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalPool.Value)));
            NoOfSwimmingPools = txtsPool.Value;
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(SwimmingPoolValue);
            PublicUtilities = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalUtilities.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(PublicUtilities);
            Telephone = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalTelephone.Value)));

            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Telephone);
            Pager = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalPager.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Pager);
            Suitcase = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalSuitcase.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Suitcase);
            GolfBagAndAccessories = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalAccessories.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(GolfBagAndAccessories);
            Camera = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalCamera.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Camera);
            Servant = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalServant.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Servant);
            Driver = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalDriver.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(Driver);
            GardenerOrCompoundUpkeep = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalGardener.Value)));

            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(GardenerOrCompoundUpkeep);
            OtherBenefitsInKindValue = Utility.ToString(Convert.ToDecimal(Utility.ToDouble(txtTotalOthers.Value)));
            totalFurnitureValue = Convert.ToDecimal(totalFurnitureValue) + Convert.ToDecimal(OtherBenefitsInKindValue);


            SelfWifeChildAbove20NoOfPersons = txtAccomodationSelf.Value;
            SelfWifeChildAbove20NoOfDays = txtAccomodationSelfPeriod.Value;
            SelfWifeChildAbove20Value = Utility.ToString(Utility.ToDouble(txtAccomodationSelfValue.Value));

            ChildBetween8And20NoOfPersons = txtChildren20yrAccomodation.Value;
            ChildBetween8And20NoOfDays = txtChildren20yrAccomodationPeriod.Value;
            ChildBetween8And20Value = Utility.ToString(Utility.ToDouble(txtChildren20yrAccomodationValue.Value));

            ChildBetween3And7NoOfPersons = txtChildren7yrAccomodation.Value;
            ChildBetween3And7NoOfDays = txtChildren7yrAccomodationPeriod.Value;
            ChildBetween3And7Value = Utility.ToString(Utility.ToDouble(txtChildren7yrAccomodationValue.Value));

            ChildBelow3NoOfPersons = txtChildren2yrAccomodation.Value;
            ChildBelow3NoOfDays = txtChildren2yrAccomodationPeriod.Value;
            ChildBelow3Value = Utility.ToString(Utility.ToDouble(txtChildren2yrAccomodationValue.Value));



            Percent2OfBasic = Utility.ToString(Utility.ToDouble(Utility.ToString(txtBasicSalPersantage.Value)));

            HotelAccommodationValue = Utility.ToString(Utility.ToDouble(SelfWifeChildAbove20Value) + Utility.ToDouble(ChildBetween8And20Value) + Utility.ToDouble(ChildBetween3And7Value) + Utility.ToDouble(ChildBelow3Value) + Utility.ToDouble(Percent2OfBasic));

            NoOfLeavePassageSelf = Utility.ToString(Utility.ToInteger(txtpassagesSelf.Value));
            NoOfLeavePassageWife = Utility.ToString(Utility.ToInteger(txtpassagesSpouse.Value));
            NoOfLeavePassageChildren = Utility.ToString(Utility.ToInteger(txtpassagesChildren.Value));
            CostOfLeavePassageAndIncidentalBenefits = Utility.ToString(Utility.ToInteger(NoOfLeavePassageSelf) + Utility.ToInteger(NoOfLeavePassageWife) + Utility.ToInteger(NoOfLeavePassageChildren));
            if (ohqyes.Checked)
                OHQStatus = "Yes";
            else if (ohqNo.Checked)
                OHQStatus = "No";
            else
                OHQStatus = "NULL";
            InterestPaidByEmployer = txtInterestPaidByEmployer.Value;
            LifeInsurancePremiumsPaidByEmployer = txtInsurancePaidbyEmployer.Value;
            FreeOrSubsidisedHoliday = txtsubsidisedHolidays.Value;
            EducationalExpenses = txtEducationalExpenses.Value;
            NonMonetaryAwardsForLongService = txtNonMonetary.Value;
            EntranceOrTransferFeesToSocialClubs = txtEntrance.Value;
            GainsFromAssets = txtGains.Value;
            FullCostOfMotorVehicle = txtMotorVehicle.Value;
            CarBenefit = txtCar.Value;
            OthersBenefits = txtOtherNonMonetary.Value;
            TotalBenefitsInKind = txtTotalBenefits.Value;
            NoOfEmployeesSharingQRS = "";
            Filler = "";
            Remarks = "";

            int j = 0;
            SqlParameter[] parms = new SqlParameter[85];
            parms[j++] = new SqlParameter("@emp_id", Utility.ToString(varEmpCode));
            parms[j++] = new SqlParameter("@ResidencePlaceValue", Utility.ToString(ResidencePlaceValue));
            parms[j++] = new SqlParameter("@ResidenceAddressLine1", Utility.ToString(ResidenceAddressLine1));
            parms[j++] = new SqlParameter("@ResidenceAddressLine2", Utility.ToString(ResidenceAddressLine2));
            parms[j++] = new SqlParameter("@ResidenceAddressLine3", Utility.ToString(ResidenceAddressLine3));
            parms[j++] = new SqlParameter("@OccupationFromDate", Utility.ToString(OccupationFromDate));
            parms[j++] = new SqlParameter("@OccupationToDate", Utility.ToString(OccupationToDate));
            parms[j++] = new SqlParameter("@NoOfDays", Utility.ToString(NoOfDays));
            parms[j++] = new SqlParameter("@AVOrRentByEmployer", Utility.ToString(AVOrRentByEmployer));
            parms[j++] = new SqlParameter("@RentByEmployee", Utility.ToString(RentByEmployee));
           
            parms[j++] = new SqlParameter("@noOfHard", Utility.ToString(noOfHard));
            parms[j++] = new SqlParameter("@HfurnitureValue", Utility.ToString(HfurnitureValue));
            parms[j++] = new SqlParameter("@NoOfSoft", Utility.ToString(NoOfSoft));
            parms[j++] = new SqlParameter("@sfurnitureValue", Utility.ToString(sfurnitureValue));
            
            parms[j++] = new SqlParameter("@FurnitureValue", Utility.ToString(totalFurnitureValue));
            parms[j++] = new SqlParameter("@HardOrsoftFurnitureItemsValue", Utility.ToString(HardOrsoftFurnitureItemsValue));
            parms[j++] = new SqlParameter("@RefrigeratorValue", Utility.ToString(RefrigeratorValue));
            parms[j++] = new SqlParameter("@NoOfRefrigerators", Utility.ToString(NoOfRefrigerators));
            parms[j++] = new SqlParameter("@VideoRecorderValue", Utility.ToString(VideoRecorderValue));
            parms[j++] = new SqlParameter("@NoOfVideoRecorders", Utility.ToString(NoOfVideoRecorders));
            parms[j++] = new SqlParameter("@WashingMachineDryerDishWasherValue", Utility.ToString(WashingMachineDryerDishWasherValue));
            parms[j++] = new SqlParameter("@NoOfWashingMachines", Utility.ToString(NoOfWashingMachines));
            parms[j++] = new SqlParameter("@NoOfDryers", Utility.ToString(NoOfDryers));
            parms[j++] = new SqlParameter("@NoOfDishWashers", Utility.ToString(NoOfDishWashers));
            parms[j++] = new SqlParameter("@AirConditionerValue", Utility.ToString(AirConditionerValue));
            parms[j++] = new SqlParameter("@NoOfAirConditioners", Utility.ToString(NoOfAirConditioners));
            parms[j++] = new SqlParameter("@NoOfCentralACDining", Utility.ToString(NoOfCentralACDining));
            parms[j++] = new SqlParameter("@NoOfCentralACSitting", Utility.ToString(NoOfCentralACSitting));
            parms[j++] = new SqlParameter("@NoOfCentralACAdditional", Utility.ToString(NoOfCentralACAdditional));
            parms[j++] = new SqlParameter("@TVRadioAmpHiFiStereoElectriGuitarValue", Utility.ToString(TVRadioAmpHiFiStereoElectriGuitarValue));
            parms[j++] = new SqlParameter("@NoOfTVs", Utility.ToString(NoOfTVs));
            parms[j++] = new SqlParameter("@NoOfRadios", Utility.ToString(NoOfRadios));
            parms[j++] = new SqlParameter("@NoOfAmplifiers", Utility.ToString(NoOfAmplifiers));
            parms[j++] = new SqlParameter("@NoOfHiFiStereos", Utility.ToString(NoOfHiFiStereos));
            parms[j++] = new SqlParameter("@NoOfElectriGuitar", Utility.ToString(NoOfElectriGuitar));
            parms[j++] = new SqlParameter("@ComputerAndOrganValue", Utility.ToString(ComputerAndOrganValue));
            parms[j++] = new SqlParameter("@NoOfComputers", Utility.ToString(NoOfComputers));
            parms[j++] = new SqlParameter("@NoOfOrgans", Utility.ToString(NoOfOrgans));
            parms[j++] = new SqlParameter("@SwimmingPoolValue", Utility.ToString(SwimmingPoolValue));
            parms[j++] = new SqlParameter("@NoOfSwimmingPools", Utility.ToString(NoOfSwimmingPools));
            parms[j++] = new SqlParameter("@PublicUtilities", Utility.ToString(PublicUtilities));
            parms[j++] = new SqlParameter("@Telephone", Utility.ToString(Telephone));
            parms[j++] = new SqlParameter("@Pager", Utility.ToString(Pager));
            parms[j++] = new SqlParameter("@Suitcase", Utility.ToString(Suitcase));
            parms[j++] = new SqlParameter("@GolfBagAndAccessories", Utility.ToString(GolfBagAndAccessories));
            parms[j++] = new SqlParameter("@Camera", Utility.ToString(Camera));
            parms[j++] = new SqlParameter("@Servant", Utility.ToString(Servant));
            parms[j++] = new SqlParameter("@Driver", Utility.ToString(Driver));
            parms[j++] = new SqlParameter("@GardenerOrCompoundUpkeep", Utility.ToString(GardenerOrCompoundUpkeep));
            parms[j++] = new SqlParameter("@OtherBenefitsInKindValue", Utility.ToString(OtherBenefitsInKindValue));
            parms[j++] = new SqlParameter("@HotelAccommodationValue", Utility.ToString(HotelAccommodationValue));
            parms[j++] = new SqlParameter("@SelfWifeChildAbove20NoOfPersons", Utility.ToString(SelfWifeChildAbove20NoOfPersons));
            parms[j++] = new SqlParameter("@SelfWifeChildAbove20NoOfDays", Utility.ToString(SelfWifeChildAbove20NoOfDays));
            parms[j++] = new SqlParameter("@SelfWifeChildAbove20Value", Utility.ToString(SelfWifeChildAbove20Value));
            parms[j++] = new SqlParameter("@ChildBetween8And20NoOfPersons", Utility.ToString(ChildBetween8And20NoOfPersons));
            parms[j++] = new SqlParameter("@ChildBetween8And20NoOfDays", Utility.ToString(ChildBetween8And20NoOfDays));
            parms[j++] = new SqlParameter("@ChildBetween8And20Value", Utility.ToString(ChildBetween8And20Value));
            parms[j++] = new SqlParameter("@ChildBetween3And7NoOfPersons", Utility.ToString(ChildBetween3And7NoOfPersons));
            parms[j++] = new SqlParameter("@ChildBetween3And7NoOfDays", Utility.ToString(ChildBetween3And7NoOfDays));
            parms[j++] = new SqlParameter("@ChildBetween3And7Value", Utility.ToString(ChildBetween3And7Value));
            parms[j++] = new SqlParameter("@ChildBelow3NoOfPersons", Utility.ToString(ChildBelow3NoOfPersons));
            parms[j++] = new SqlParameter("@ChildBelow3NoOfDays", Utility.ToString(ChildBelow3NoOfDays));
            parms[j++] = new SqlParameter("@ChildBelow3Value", Utility.ToString(ChildBelow3Value));
            parms[j++] = new SqlParameter("@Percent2OfBasic", Utility.ToString(Percent2OfBasic));
            parms[j++] = new SqlParameter("@CostOfLeavePassageAndIncidentalBenefits", Utility.ToString(CostOfLeavePassageAndIncidentalBenefits));
            parms[j++] = new SqlParameter("@NoOfLeavePassageSelf", Utility.ToString(NoOfLeavePassageSelf));
            parms[j++] = new SqlParameter("@NoOfLeavePassageWife", Utility.ToString(NoOfLeavePassageWife));
            parms[j++] = new SqlParameter("@NoOfLeavePassageChildren", Utility.ToString(NoOfLeavePassageChildren));
            parms[j++] = new SqlParameter("@OHQStatus", Utility.ToString(OHQStatus));
            parms[j++] = new SqlParameter("@InterestPaidByEmployer", Utility.ToString(InterestPaidByEmployer));
            parms[j++] = new SqlParameter("@LifeInsurancePremiumsPaidByEmployer", Utility.ToString(LifeInsurancePremiumsPaidByEmployer));
            parms[j++] = new SqlParameter("@FreeOrSubsidisedHoliday", Utility.ToString(FreeOrSubsidisedHoliday));
            parms[j++] = new SqlParameter("@EducationalExpenses", Utility.ToString(EducationalExpenses));
            parms[j++] = new SqlParameter("@NonMonetaryAwardsForLongService", Utility.ToString(NonMonetaryAwardsForLongService));
            parms[j++] = new SqlParameter("@EntranceOrTransferFeesToSocialClubs", Utility.ToString(EntranceOrTransferFeesToSocialClubs));
            parms[j++] = new SqlParameter("@GainsFromAssets", Utility.ToString(GainsFromAssets));
            parms[j++] = new SqlParameter("@FullCostOfMotorVehicle", Utility.ToString(FullCostOfMotorVehicle));
            parms[j++] = new SqlParameter("@CarBenefit", Utility.ToString(CarBenefit));
            parms[j++] = new SqlParameter("@OthersBenefits", Utility.ToString(OthersBenefits));
            parms[j++] = new SqlParameter("@TotalBenefitsInKind", Utility.ToString(TotalBenefitsInKind));
            parms[j++] = new SqlParameter("@year", Utility.ToString(cmbIR8AaPEPNDIXa_year.Value.ToString()));
            parms[j++] = new SqlParameter("@compid", Utility.ToString(compid));
            parms[j++] = new SqlParameter("@NoOfEmployeesSharingQRS", 1);
            parms[j++] = new SqlParameter("@SectionAValue", Utility.ToString(txtSecATotal.Value));
            parms[j++] = new SqlParameter("@SectionBValue", Utility.ToString(txtSecBTotal.Value));
            //,
            //compid
            string sqlQuery = "";//varEmpCode
            sqlQuery = "select count(*) from IR8AAPENDIX_EMPLOYEE where emp_id= '" + varEmpCode + "' and year='" + cmbIR8AaPEPNDIXa_year.Value.ToString() + "' and Comp_id='" + compid + "'";
            DataSet sds;
            sds = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            if (sds.Tables[0].Rows.Count > 0)
            {
                sqlQuery = "delete from IR8AAPENDIX_EMPLOYEE where emp_id= '" + varEmpCode + "' and year='" + cmbIR8AaPEPNDIXa_year.Value.ToString() + "' and Comp_id='" + compid + "'";
                int reccount = DataAccess.ExecuteNonQuery(sqlQuery, null);
            }
            sqlQuery = " insert into ir8aApendix_employee(emp_id,ResidencePlaceValue,ResidenceAddressLine1,ResidenceAddressLine2,ResidenceAddressLine3,OccupationFromDate,OccupationToDate ,NoOfDays,AVOrRentByEmployer,RentByEmployee,FurnitureValue,HardOrsoftFurnitureItemsValue,RefrigeratorValue,NoOfRefrigerators,VideoRecorderValue,NoOfVideoRecorders,WashingMachineDryerDishWasherValue,NoOfWashingMachines,NoOfDryers,NoOfDishWashers,AirConditionerValue,NoOfAirConditioners,NoOfCentralACDining,NoOfCentralACSitting,NoOfCentralACAdditional,TVRadioAmpHiFiStereoElectriGuitarValue,NoOfTVs,NoOfRadios,NoOfAmplifiers,NoOfHiFiStereos,NoOfElectriGuitar,ComputerAndOrganValue,NoOfComputers,NoOfOrgans,SwimmingPoolValue,NoOfSwimmingPools,PublicUtilities,Telephone,Pager,Suitcase,GolfBagAndAccessories,Camera,Servant,Driver,GardenerOrCompoundUpkeep,OtherBenefitsInKindValue,HotelAccommodationValue,SelfWifeChildAbove20NoOfPersons,SelfWifeChildAbove20NoOfDays,SelfWifeChildAbove20Value,ChildBetween8And20NoOfPersons,ChildBetween8And20NoOfDays,ChildBetween8And20Value,ChildBetween3And7NoOfPersons,ChildBetween3And7NoOfDays,ChildBetween3And7Value,ChildBelow3NoOfPersons,ChildBelow3NoOfDays,ChildBelow3Value,Percent2OfBasic,CostOfLeavePassageAndIncidentalBenefits,NoOfLeavePassageSelf,NoOfLeavePassageWife,NoOfLeavePassageChildren,OHQStatus,InterestPaidByEmployer,LifeInsurancePremiumsPaidByEmployer,FreeOrSubsidisedHoliday,EducationalExpenses,NonMonetaryAwardsForLongService,EntranceOrTransferFeesToSocialClubs,GainsFromAssets,FullCostOfMotorVehicle,CarBenefit,OthersBenefits,TotalBenefitsInKind,[YEAR],cOMP_ID,NoOfEmployeesSharingQRS,NoOfHardFurniture,HardFurnitureValue,NoOfSoftFurniture,SoftFurnitureValue,SectionAValue,SectionBValue)values(@emp_id,@ResidencePlaceValue,@ResidenceAddressLine1,@ResidenceAddressLine2,@ResidenceAddressLine3,@OccupationFromDate,@OccupationToDate,@NoOfDays,@AVOrRentByEmployer,@RentByEmployee,@FurnitureValue,@HardOrsoftFurnitureItemsValue,@RefrigeratorValue,@NoOfRefrigerators,@VideoRecorderValue,@NoOfVideoRecorders,@WashingMachineDryerDishWasherValue,@NoOfWashingMachines,@NoOfDryers,@NoOfDishWashers,@AirConditionerValue,@NoOfAirConditioners,@NoOfCentralACDining,@NoOfCentralACSitting,@NoOfCentralACAdditional,@TVRadioAmpHiFiStereoElectriGuitarValue,@NoOfTVs,@NoOfRadios,@NoOfAmplifiers,@NoOfHiFiStereos,@NoOfElectriGuitar,@ComputerAndOrganValue,@NoOfComputers,@NoOfOrgans,@SwimmingPoolValue,@NoOfSwimmingPools,@PublicUtilities,@Telephone,@Pager,@Suitcase,@GolfBagAndAccessories,@Camera,@Servant,@Driver,@GardenerOrCompoundUpkeep,@OtherBenefitsInKindValue,@HotelAccommodationValue,@SelfWifeChildAbove20NoOfPersons,@SelfWifeChildAbove20NoOfDays,@SelfWifeChildAbove20Value,@ChildBetween8And20NoOfPersons,@ChildBetween8And20NoOfDays,@ChildBetween8And20Value,@ChildBetween3And7NoOfPersons,@ChildBetween3And7NoOfDays,@ChildBetween3And7Value,@ChildBelow3NoOfPersons,@ChildBelow3NoOfDays,@ChildBelow3Value,@Percent2OfBasic,@CostOfLeavePassageAndIncidentalBenefits,@NoOfLeavePassageSelf,@NoOfLeavePassageWife,@NoOfLeavePassageChildren,@OHQStatus,@InterestPaidByEmployer,@LifeInsurancePremiumsPaidByEmployer,@FreeOrSubsidisedHoliday,@EducationalExpenses,@NonMonetaryAwardsForLongService,@EntranceOrTransferFeesToSocialClubs,@GainsFromAssets,@FullCostOfMotorVehicle,@CarBenefit,@OthersBenefits,@TotalBenefitsInKind,@year,@compid,@NoOfEmployeesSharingQRS,@noOfHard,@HfurnitureValue,@NoOfSoft,@sfurnitureValue,@SectionAValue,@SectionBValue)";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.Text, sqlQuery, parms);
        }
        private void overWriteIR8AXml()
        {

            try
            {
                string sSource = Server.MapPath("~/XML/IR8aApendixATemplate.xml");
                string sDestn = Server.MapPath("~/XML/IR8AAppendixA.xml");
                if (File.Exists(sSource) == true)
                {
                    if (File.Exists(sDestn) == true)
                    {
                        File.Copy(sSource, sDestn, true);
                    }
                    else
                    {
                        File.Copy(sSource, sDestn);
                    }

                }

            }
            catch (FileNotFoundException exFile)
            {
                Response.Write(exFile.Message.ToString());
            }

        }
        private void appendIR8AAppendixTemplateXml(DataSet ir8aEmpDetails)
        {

            XmlDocument document = new XmlDocument();
            document.Load(Server.MapPath("~/XML/IR8AAppendixA.xml"));


            for (int empRecord = 0; empRecord < ir8aEmpDetails.Tables[0].Rows.Count; empRecord++)
            {

                XmlNode section1 = document.CreateElement("A8ARecord", "http://www.iras.gov.sg/A8ADef");
                XmlNode section2 = document.CreateElement("ESubmissionSDSC", "http://tempuri.org/ESubmissionSDSC.xsd");
                XmlNode section3 = document.CreateElement("A8AST", "http://tempuri.org/ESubmissionSDSC.xsd");
                for (int empColumn = 0; empColumn < ir8aEmpDetails.Tables[0].Columns.Count; empColumn++)
                {
                    string columnName = Utility.ToString(ir8aEmpDetails.Tables[0].Columns[empColumn].ToString());
                    XmlNode key = document.CreateElement(columnName, "http://www.iras.gov.sg/A8A");
                    key.InnerText = ir8aEmpDetails.Tables[0].Rows[empRecord][columnName].ToString();
                    section3.AppendChild(key);
                }

                section2.AppendChild(section3);
                section1.AppendChild(section2);
                document.DocumentElement.ChildNodes[1].AppendChild(section1);
            }
            document.Save(Server.MapPath("~/XML/IR8AAppendixA.xml"));

            string FilePath = Server.MapPath("~/XML/IR8AAppendixA.xml");
            document.Save(FilePath);
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<Script>alert('The A8A Xml File Created.')</Script>");
            RegisterClientScriptBlock("k1", "<Script>alert('The IR8AAppendixA Xml File Created.')</Script>");
            string filename = Path.GetFileName(FilePath);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            Response.TransmitFile(FilePath);
            Response.End();
            document = null;
        }
        private void appendIR8AHeaderXml(DataSet ir8aEmpDetails)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(Server.MapPath("~/XML/IR8A.XML"));
            System.Xml.XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xdoc.NameTable);
            xmlnsManager.AddNamespace("sm", "http://tempuri.org/ESubmissionSDSC.xsd");
            xmlnsManager.AddNamespace("sm2", "http://www.iras.gov.sg/IR8ADef");
            XmlNode header;
            header = xdoc.SelectSingleNode("sm2:IR8A/sm2:IR8AHeader/sm:ESubmissionSDSC/sm:FileHeaderST", xmlnsManager);
            string headerText = header.InnerText;
            header["RecordType"].InnerText = "0";
            header["Source"].InnerText = "6";
            header["BasisYear"].InnerText = System.DateTime.Now.Year.ToString();
            header["OrganizationID"].InnerText = "8";
            header["OrganizationIDNo"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["OrganizationIDNo"].ToString());
            header["AuthorisedPersonName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonName"].ToString());
            header["AuthorisedPersonDesignation"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonDesignation"].ToString());
            header["EmployerName"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["EmployerName"].ToString());
            header["Telephone"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["Telephone"].ToString());
            header["AuthorisedPersonEmail"].InnerText = Utility.ToString(ir8aEmpDetails.Tables[0].Rows[0]["AuthorisedPersonEmail"].ToString());
            header["BatchIndicator"].InnerText = "O";
            string today_Date = Convert.ToString(System.DateTime.Now.ToShortDateString());
            string[] curDate = today_Date.Split('/');
            today_Date = curDate[2] + curDate[0] + curDate[1];

            header["BatchDate"].InnerText = today_Date;
            header["DivisionOrBranchName"].InnerText = "";
            xdoc.Save(Server.MapPath("~/XML/IR8A.XML"));
            xdoc = null;
        }
        private void empSaveIr8aAppendixB()
        {
            string sqlQuery = "insert into dbo.IR8AppendixB (emp_code,nric,CompanyCode,TypeOfPlan,Dateofgrant,DateOfExcercise,ExcercisePrice,OpenMarketShareValueAtDateGrant,OpenMarketShareValue,NoOfSharesAcquired,GrossAmountQualifyingForIncomeTax,GrossAmountNotQualifyingForTaxExemption,GrossAmountOfGainsFromPlans,Year,sectionType)Values";
            string emp_code = Utility.ToString(Utility.ToInteger(varEmpCode));

            string CompanyCode = Utility.ToString(Utility.ToInteger(Session["Compid"]));
            string TypeOfPlan = cmbPlan.SelectedIndex.ToString();
            string Dateofgrant = rdGrant.SelectedDate.ToString();
            string DateOfExcercise = rdExcercise.SelectedDate.ToString();
            string ExcercisePrice = txtExPrice.Text.ToString();
            string OpenMarketShareValueAtDateGrant = txtOpenPrice.Text.ToString();
            string OpenMarketShareValue = txtRefPrice.Text.ToString();
            string NoOfSharesAcquired = txtNoShares.Text.ToString();
            string GrossAmountQualifyingForIncomeTax = txtGrossAmount.Text.ToString();
            string GrossAmountNotQualifyingForTaxExemption = txtNoTaxAmt.Text.ToString();
            string GrossAmountOfGainsFromPlans = txtGainAmt.Text.ToString();
            string strYear = rdYear.Value;

            sqlQuery = sqlQuery + "( '" + emp_code + "',' " + NRIC + "','" + CompanyCode + "','" + TypeOfPlan + "',' " + Dateofgrant + "','" + DateOfExcercise + "','" + ExcercisePrice + "','" + OpenMarketShareValueAtDateGrant + "','" + OpenMarketShareValue + "','" + NoOfSharesAcquired + "','" + GrossAmountQualifyingForIncomeTax + "','" + GrossAmountNotQualifyingForTaxExemption + "','" + GrossAmountOfGainsFromPlans + "','" + strYear + "','" + empSection.SelectedValue + "')";
            int result = DataAccess.ExecuteNonQuery(sqlQuery, null);
            if (result > 0)
            {
                lblerr.Text = "Records Inserted Successfully";
                //  clearTexts();
            }
        }
        protected void exemType_slectIndexChanged(object sender, EventArgs e)
        {
            if (Utility.ToString(exemType.SelectedValue.ToString()) != "0")
                lblSchemeType.Text = exemType.SelectedItem.ToString();
        }
        protected void empSection_slectIndexChanged(object sender, EventArgs e)
        {
            if (Utility.ToString(empSection.SelectedValue.ToString()) == "1")
            {
                lblTaxExemptionFormula.Text = "(l)=(g-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)";
                txtGrossAmount.Text = "0";
                txtNoTaxAmt.Text = (((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text))).ToString();
                txtGainAmt.Text = txtGrossAmount.Text = "0";
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "2")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(i)+(l)";
                txtGrossAmount.Text = (((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text))).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "3")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(j)+(l)";
                txtGrossAmount.Text = ((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
            else if (Utility.ToString(empSection.SelectedValue.ToString()) == "4")
            {
                lblTaxExemptionFormula.Text = "(l)=(f-e)*h";
                lblTaxGainFormula.Text = "(m)=(k)+(l)";
                txtGrossAmount.Text = ((Utility.ToInteger(Utility.ToInteger(txtRefPrice.Text)) - (Utility.ToInteger(txtOpenPrice.Text))) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtNoTaxAmt.Text = ((Utility.ToInteger(txtOpenPrice.Text) - Utility.ToInteger(txtExPrice.Text)) * Utility.ToInteger(txtNoShares.Text)).ToString();
                txtGainAmt.Text = (Utility.ToInteger(txtNoTaxAmt.Text) + Utility.ToInteger(txtGrossAmount.Text)).ToString();
            }
        }
        private void empSaveAppendixB()
        {
            string sqlQuery = "insert into dbo.IR8AppendixB (emp_code,nric,CompanyCode,TypeOfPlan,Dateofgrant,DateOfExcercise,ExcercisePrice,OpenMarketShareValueAtDateGrant,OpenMarketShareValue,NoOfSharesAcquired,GrossAmountQualifyingForIncomeTax,GrossAmountNotQualifyingForTaxExemption,GrossAmountOfGainsFromPlans,Year,sectionType)Values";
            string emp_code = Utility.ToString(Utility.ToInteger(varEmpCode));

            string CompanyCode = Utility.ToString(Utility.ToInteger(Session["Compid"]));
            string TypeOfPlan = cmbPlan.SelectedIndex.ToString();
            string Dateofgrant = rdGrant.SelectedDate.ToString();
            string DateOfExcercise = rdExcercise.SelectedDate.ToString();
            string ExcercisePrice = txtExPrice.Text.ToString();
            string OpenMarketShareValueAtDateGrant = txtOpenPrice.Text.ToString();
            string OpenMarketShareValue = txtRefPrice.Text.ToString();
            string NoOfSharesAcquired = txtNoShares.Text.ToString();
            string GrossAmountQualifyingForIncomeTax = txtGrossAmount.Text.ToString();
            string GrossAmountNotQualifyingForTaxExemption = txtNoTaxAmt.Text.ToString();
            string GrossAmountOfGainsFromPlans = txtGainAmt.Text.ToString();
            string strYear = rdYear.Value;

            sqlQuery = sqlQuery + "( '" + emp_code + "',' " + NRIC + "','" + CompanyCode + "','" + TypeOfPlan + "',' " + Dateofgrant + "','" + DateOfExcercise + "','" + ExcercisePrice + "','" + OpenMarketShareValueAtDateGrant + "','" + OpenMarketShareValue + "','" + NoOfSharesAcquired + "','" + GrossAmountQualifyingForIncomeTax + "','" + GrossAmountNotQualifyingForTaxExemption + "','" + GrossAmountOfGainsFromPlans + "','" + strYear + "','" + empSection.SelectedValue + "')";
            int result = DataAccess.ExecuteNonQuery(sqlQuery, null);
            if (result > 0)
            {
                //lblerr.Text = "Records Inserted Successfully";
                clearTexts();
            }

        }
        protected static DataSet getDataSet(string sSQL)
        {
            DataSet ds = new DataSet();
            ds = DataAccess.FetchRS(CommandType.Text, sSQL, null);
            return ds;
        }
        public void getEmpDetails()
        {

            DataSet sqldS;
            string sqlQuery = "SELECT Company_Name ,Company_Roc,IC_PP_NUMBER,emp_code FROM EMPLOYEE E INNER JOIN COMPANY C ON E.COMPANY_ID=C.COMPANY_ID WHERE E.EMP_CODE = " + Utility.ToInteger(varEmpCode);
            sqldS = DataAccess.FetchRS(CommandType.Text, sqlQuery, null);
            txtCompany.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Name"].ToString());
            txtComRoc.Text = Utility.ToString(sqldS.Tables[0].Rows[0]["Company_Roc"].ToString());
            NRIC = Utility.ToString(sqldS.Tables[0].Rows[0]["IC_PP_NUMBER"].ToString());
            varEmpCode = Utility.ToString(sqldS.Tables[0].Rows[0]["emp_code"].ToString());
        }
        private void clearTexts()
        {

            txtExPrice.Text = "";
            txtOpenPrice.Text = "";
            txtRefPrice.Text = "";
            txtNoShares.Text = "";
            txtGrossAmount.Text = "";
            txtNoTaxAmt.Text = "";
            txtGainAmt.Text = "";

            txtCompany.Text = "";
            txtComRoc.Text = "";
            cmbPlan.SelectedIndex = 0;
            exemType.SelectedIndex = 0;
            empSection.SelectedIndex = 0;
        }
        public void FillappendixAtextBox()
        {
            string sSql = "Sp_getEmployeeIR8aDetails";
            SqlParameter[] sparam = new SqlParameter[2];
            sparam[0] = new SqlParameter("@empCode", varEmpCode);
            sparam[1] = new SqlParameter("@compId", compid);
            string yearCode = "";
            DataSet sqlDs = DataAccess.FetchRS(CommandType.StoredProcedure, sSql, sparam);
            if (sqlDs.Tables[0].Rows.Count > 0)
            {
                txtAddress1.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["ResidenceAddressLine1"].ToString());
                txtAddress2.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["ResidenceAddressLine2"].ToString());
                txtAddress3.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["ResidenceAddressLine3"].ToString());
                yearCode = Convert.ToString(sqlDs.Tables[0].Rows[0]["Year"].ToString());
                cmbIR8AaPEPNDIXa_year.Items.FindByText(yearCode.Trim()).Selected = true;

                string sDate = sqlDs.Tables[0].Rows[0]["OccupationFromDate"].ToString();
                IFormatProvider format = new System.Globalization.CultureInfo("en-GB", true);
                sDate = Convert.ToDateTime(sDate).ToString("dd/MM/yyyy", format);
                txtFrom.SelectedDate = System.DateTime.Parse(sDate);

                sDate = sqlDs.Tables[0].Rows[0]["OccupationToDate"].ToString();

                sDate = Convert.ToDateTime(sDate).ToString("dd/MM/yyyy", format);
                txtNoOfDays.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["NoOfDays"].ToString());
                txtTo.SelectedDate = System.DateTime.Parse(sDate);
                txtEmployerRent.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["AVOrRentByEmployer"].ToString());
                txtEmployeeRent.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["RentByEmployee"].ToString());
               
                txtFHard.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["NoOfHardFurniture"].ToString());
                txtTotalHard.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["HardFurnitureValue"].ToString());
                txtFSoft.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["NoOfSoftFurniture"].ToString());
                txtTotalSoft.Value = Convert.ToString(sqlDs.Tables[0].Rows[0]["SoftFurnitureValue"].ToString());
                //NoOfDays
                txtRef.Value = sqlDs.Tables[0].Rows[0]["NoOfRefrigerators"].ToString();
                txtTotalRef.Value = sqlDs.Tables[0].Rows[0]["RefrigeratorValue"].ToString();
                txtVcd.Value = sqlDs.Tables[0].Rows[0]["NoOfVideoRecorders"].ToString();
                txtTotalVcd.Value = sqlDs.Tables[0].Rows[0]["VideoRecorderValue"].ToString();
                txtWashingMachine.Value = sqlDs.Tables[0].Rows[0]["NoOfWashingMachines"].ToString();
                txtTotalWashingMachine.Value = sqlDs.Tables[0].Rows[0]["WashingMachineDryerDishWasherValue"].ToString();
                txtDryer.Value = sqlDs.Tables[0].Rows[0]["NoOfDryers"].ToString();
                txtTotalDryer.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfDryers"].ToString()) * Utility.ToDouble(txtCostDryer.Value));
                txtDish.Value = sqlDs.Tables[0].Rows[0]["NoOfDishWashers"].ToString();
                txtTotalDish.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfDishWashers"].ToString()) * Utility.ToDouble(txtCostDish.Value));
                txtAc.Value = sqlDs.Tables[0].Rows[0]["NoOfAirConditioners"].ToString();
                txtTotalAc.Value = sqlDs.Tables[0].Rows[0]["AirConditionerValue"].ToString();

                txtAcdining.Value = sqlDs.Tables[0].Rows[0]["NoOfCentralACDining"].ToString();
                txtTotalAcdining.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfCentralACDining"].ToString()) * Utility.ToDouble(txtCostAcdining.Value));
                txtACsitting.Value = sqlDs.Tables[0].Rows[0]["NoOfCentralACSitting"].ToString();
                txtTotalACsitting.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfCentralACSitting"].ToString()) * Utility.ToDouble(txtCostACsitting.Value));
                txtAcAdditional.Value = sqlDs.Tables[0].Rows[0]["NoOfCentralACAdditional"].ToString();
                txtTotalAcAdditional.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfCentralACAdditional"].ToString()) * Utility.ToDouble(txtCostAcAdditional.Value));


                txtTV.Value = sqlDs.Tables[0].Rows[0]["NoOfTVs"].ToString();
                txtTotalTV.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfTVs"].ToString()) * Utility.ToDouble(txtCostTV.Value));
                txtRadio.Value = sqlDs.Tables[0].Rows[0]["NoOfRadios"].ToString();
                txtTotalRadio.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfRadios"].ToString()) * Utility.ToDouble(txtCostRadio.Value));
                txtHifi.Value = sqlDs.Tables[0].Rows[0]["NoOfHiFiStereos"].ToString();
                txtTotalHifi.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfHiFiStereos"].ToString()) * Utility.ToDouble(txtCostHifi.Value));
                txtGuitar.Value = sqlDs.Tables[0].Rows[0]["NoOfElectriGuitar"].ToString();
                txtTotalGuitar.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfElectriGuitar"].ToString()) * Utility.ToDouble(txtCostGuitar.Value));

                txtComputer.Value = sqlDs.Tables[0].Rows[0]["NoOfComputers"].ToString();
                txtTotalComputer.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfComputers"].ToString()) * Utility.ToDouble(txtCostComputer.Value));
                txtOrgan.Value = sqlDs.Tables[0].Rows[0]["NoOfOrgans"].ToString();
                txtTotalOrgan.Value = Convert.ToString(Utility.ToInteger(sqlDs.Tables[0].Rows[0]["NoOfComputers"].ToString()) * Utility.ToDouble(txtCostOrgan.Value));
                txtsPool.Value = sqlDs.Tables[0].Rows[0]["NoOfSwimmingPools"].ToString();
                txtTotalPool.Value = sqlDs.Tables[0].Rows[0]["SwimmingPoolValue"].ToString();
                txtTotalUtilities.Value = sqlDs.Tables[0].Rows[0]["PublicUtilities"].ToString();
                txtTotalTelephone.Value = sqlDs.Tables[0].Rows[0]["Telephone"].ToString();
                txtTotalPager.Value = sqlDs.Tables[0].Rows[0]["Pager"].ToString();
                txtTotalSuitcase.Value = sqlDs.Tables[0].Rows[0]["Suitcase"].ToString();
                txtTotalAccessories.Value = sqlDs.Tables[0].Rows[0]["GolfBagAndAccessories"].ToString();
                txtTotalCamera.Value = sqlDs.Tables[0].Rows[0]["Camera"].ToString();
                txtTotalServant.Value = sqlDs.Tables[0].Rows[0]["Servant"].ToString();
                txtTotalDriver.Value = sqlDs.Tables[0].Rows[0]["Driver"].ToString();
                txtTotalGardener.Value = sqlDs.Tables[0].Rows[0]["GardenerOrCompoundUpkeep"].ToString();
                txtTotalOthers.Value = sqlDs.Tables[0].Rows[0]["OtherBenefitsInKindValue"].ToString();

                // Accomadation 
                txtAccomodationSelf.Value = sqlDs.Tables[0].Rows[0]["SelfWifeChildAbove20NoOfPersons"].ToString();
                txtAccomodationSelfPeriod.Value = sqlDs.Tables[0].Rows[0]["SelfWifeChildAbove20NoOfDays"].ToString();
                txtAccomodationSelfValue.Value = sqlDs.Tables[0].Rows[0]["SelfWifeChildAbove20Value"].ToString();

                txtChildren2yrAccomodation.Value = sqlDs.Tables[0].Rows[0]["ChildBelow3NoOfPersons"].ToString();
                txtChildren2yrAccomodationPeriod.Value = sqlDs.Tables[0].Rows[0]["ChildBelow3NoOfDays"].ToString();
                txtChildren2yrAccomodationValue.Value = sqlDs.Tables[0].Rows[0]["ChildBelow3Value"].ToString();

                txtChildren7yrAccomodation.Value = sqlDs.Tables[0].Rows[0]["ChildBetween3And7NoOfPersons"].ToString();
                txtChildren7yrAccomodationPeriod.Value = sqlDs.Tables[0].Rows[0]["ChildBetween3And7NoOfDays"].ToString();
                txtChildren7yrAccomodationValue.Value = sqlDs.Tables[0].Rows[0]["ChildBetween3And7Value"].ToString();

                txtChildren20yrAccomodation.Value = sqlDs.Tables[0].Rows[0]["ChildBetween8And20NoOfPersons"].ToString();
                txtChildren20yrAccomodationPeriod.Value = sqlDs.Tables[0].Rows[0]["ChildBetween8And20NoOfDays"].ToString();
                txtChildren20yrAccomodationValue.Value = sqlDs.Tables[0].Rows[0]["ChildBetween8And20Value"].ToString();

                txtBasicSalPersantage.Value = sqlDs.Tables[0].Rows[0]["Percent2OfBasic"].ToString(); ;

                // Others

                txtpassagesSelf.Value = sqlDs.Tables[0].Rows[0]["NoOfLeavePassageSelf"].ToString();
                txtpassagesSpouse.Value = sqlDs.Tables[0].Rows[0]["NoOfLeavePassageWife"].ToString();
                txtpassagesChildren.Value = sqlDs.Tables[0].Rows[0]["NoOfLeavePassageChildren"].ToString();
                txtpassagesTotal.Value = sqlDs.Tables[0].Rows[0]["CostOfLeavePassageAndIncidentalBenefits"].ToString();
                txtOtherNonMonetary.Value = sqlDs.Tables[0].Rows[0]["NonMonetaryAwardsForLongService"].ToString();
                txtInterestPaidByEmployer.Value = sqlDs.Tables[0].Rows[0]["InterestPaidByEmployer"].ToString();
                txtInsurancePaidbyEmployer.Value = sqlDs.Tables[0].Rows[0]["LifeInsurancePremiumsPaidByEmployer"].ToString();
                txtsubsidisedHolidays.Value = sqlDs.Tables[0].Rows[0]["FreeOrSubsidisedHoliday"].ToString();
                txtEducationalExpenses.Value = sqlDs.Tables[0].Rows[0]["EducationalExpenses"].ToString();
                txtNonMonetary.Value = sqlDs.Tables[0].Rows[0]["NonMonetaryAwardsForLongService"].ToString();
                txtEntrance.Value = sqlDs.Tables[0].Rows[0]["EntranceOrTransferFeesToSocialClubs"].ToString();
                txtGains.Value = sqlDs.Tables[0].Rows[0]["GainsFromAssets"].ToString();
                txtMotorVehicle.Value = sqlDs.Tables[0].Rows[0]["FullCostOfMotorVehicle"].ToString();
                txtCar.Value = sqlDs.Tables[0].Rows[0]["CarBenefit"].ToString();
                txtNonMonetary.Value = sqlDs.Tables[0].Rows[0]["NonMonetaryAwardsForLongService"].ToString();
                txtTotalBenefits.Value = sqlDs.Tables[0].Rows[0]["TotalBenefitsInKind"].ToString();
                //ohqyes
                string ohq = sqlDs.Tables[0].Rows[0]["OHQStatus"].ToString();
                if (ohq == "y")
                {
                    ohqyes.Checked = true;
                }
                else
                {
                    ohqNo.Checked = true;
                }
            }

        }


        protected void btnSecA_ServerClick(object sender, EventArgs e)
        {
            int totalSecA = 0;
                totalSecA = convertFunction(txtTotalHard.Value);
                totalSecA = totalSecA + convertFunction(txtTotalSoft.Value);
                totalSecA = totalSecA + convertFunction(txtTotalRef.Value);
                totalSecA = totalSecA + convertFunction(txtTotalVcd.Value);
                totalSecA = totalSecA + convertFunction(txtTotalWashingMachine.Value);
                totalSecA = totalSecA + convertFunction(txtTotalDryer.Value);
                totalSecA = totalSecA + convertFunction(txtTotalDish.Value);
                totalSecA = totalSecA + convertFunction(txtTotalAc.Value);
                totalSecA = totalSecA + convertFunction(txtTotalAcdining.Value);
                totalSecA = totalSecA + convertFunction(txtTotalACsitting.Value);
                totalSecA = totalSecA + convertFunction(txtTotalAcAdditional.Value);

                totalSecA = totalSecA + convertFunction(txtTotalTV.Value);
                totalSecA = totalSecA + convertFunction(txtTotalRadio.Value);
                totalSecA = totalSecA + convertFunction(txtTotalHifi.Value);
                totalSecA = totalSecA + convertFunction(txtTotalGuitar.Value);
                totalSecA = totalSecA + convertFunction(txtTotalComputer.Value);
                totalSecA = totalSecA + convertFunction(txtTotalOrgan.Value);
                totalSecA = totalSecA + convertFunction(txtTotalPool.Value);
                totalSecA = totalSecA + convertFunction(txtTotalUtilities.Value);
                totalSecA = totalSecA + convertFunction(txtTotalTelephone.Value);
                totalSecA = totalSecA + convertFunction(txtTotalPager.Value);
                totalSecA = totalSecA + convertFunction(txtTotalSuitcase.Value);
                totalSecA = totalSecA + convertFunction(txtTotalAccessories.Value);
                totalSecA = totalSecA + convertFunction(txtTotalCamera.Value);
                totalSecA = totalSecA + convertFunction(txtTotalServant.Value);
                totalSecA = totalSecA + convertFunction(txtTotalDriver.Value);
                totalSecA = totalSecA + convertFunction(txtTotalGardener.Value);
                totalSecA = totalSecA + convertFunction(txtTotalOthers.Value);
                txtSecATotal.Value = totalSecA.ToString();
     

        }
        protected void btnSecB_ServerClick(object sender, EventArgs e)
        {
            decimal totalSecB = 0;
            totalSecB = Convert.ToDecimal(txtAccomodationSelfValue.Value);
            totalSecB = totalSecB + Convert.ToDecimal(txtChildren2yrAccomodationValue.Value);
            totalSecB = totalSecB + Convert.ToDecimal(txtChildren7yrAccomodationValue.Value);
            totalSecB = totalSecB + Convert.ToDecimal(txtChildren20yrAccomodationValue.Value);
            totalSecB = totalSecB + Convert.ToDecimal(txtBasicSalPersantage.Value);
            txtSecBTotal.Value = totalSecB.ToString();
           


        }
        protected void btnSecC_ServerClick(object sender, EventArgs e)
        {
            decimal totalSecC = 0;
            totalSecC = totalSecC + Convert.ToDecimal(txtInterestPaidByEmployer.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtInsurancePaidbyEmployer.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtsubsidisedHolidays.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtEducationalExpenses.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtNonMonetary.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtEntrance.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtGains.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtMotorVehicle.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtCar.Value);
            totalSecC = totalSecC + Convert.ToDecimal(txtOtherNonMonetary.Value);
            txtTotalBenefits.Value = Convert.ToDecimal(txtSecATotal.Value) + Convert.ToDecimal(txtSecBTotal.Value) + totalSecC.ToString();
        }

        public int convertFunction(object strinput)
        {
            return (strinput == null || Convert.ToString(strinput).Length == 0 ? 0 : Convert.ToInt32(strinput));

        }
    }
}
