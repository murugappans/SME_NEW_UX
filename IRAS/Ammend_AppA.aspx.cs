
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
using NHibernate;
using NHibernate.Cfg;
using System.Collections.Generic;
using IRAS.Appendix_A;

namespace IRAS
{
    public partial class Ammend_AppA : System.Web.UI.Page
    {

        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        string varEmpCode = "";
        int yearCode;
        
        SqlDataSource sql = new SqlDataSource();
        A8AST orginal_appx_data1;
        A8AST a8ast;
        protected void Page_Load(object sender, EventArgs e)
        {


            sql.ConnectionString = Session["ConString"].ToString();
            varEmpCode = Request.QueryString["empcode"].ToString();
            yearCode =  Utility.ToInteger(Request.QueryString["year"].ToString())-1;




            btnsave.ServerClick += new EventHandler(btnsave_ServerClick);
            ButtonCALCULATE.ServerClick += new EventHandler(ButtonCALCULATE_Click);
        
           

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {
                IQuery query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + Utility.ToInteger(varEmpCode) + "AND AppendixA_year=" + yearCode.ToString()+" and IS_AMENDMENT=0").AddEntity(typeof(A8AST));
                
                if (query.List().Count != 0)
                {
                    orginal_appx_data1 = query.List<A8AST>()[0];
                    this.name_label.Text = orginal_appx_data1.NameLine1 + " " + orginal_appx_data1.NameLine2;
                    this.nric_label.Text = orginal_appx_data1.IDNo;
                    this.address_label1.Text = orginal_appx_data1.ResidenceAddressLine1;
                    this.address_label2.Text = orginal_appx_data1.ResidenceAddressLine2;
                    this.address_label3.Text = orginal_appx_data1.ResidenceAddressLine3;
                   
                 }
                                                               
            }

            if (!IsPostBack)
            {
                using (ISession session = NHibernateHelper.GetCurrentSession())
                {
                    IQuery query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + Utility.ToInteger(varEmpCode) + "AND AppendixA_year=" + yearCode.ToString() + " and IS_AMENDMENT=1").AddEntity(typeof(A8AST));

                    if (query.List().Count != 0)
                    {
                        a8ast = query.List<A8AST>()[0];
                        this.OccupationFromDate.SelectedDate = Utility.toDateTime(a8ast.OccupationFromDate);
                        this.OccupationToDate.SelectedDate = Utility.toDateTime(a8ast.OccupationToDate);
                        this.noofdaystextbox.Text = a8ast.NoOfDays.ToString();
                        this._RentByEmployee.Text = a8ast.RentByEmployee.ToString();
                        this.address_label1.Text = a8ast.ResidenceAddressLine1;
                        this.address_label2.Text = a8ast.ResidenceAddressLine2;
                        this.address_label3.Text = a8ast.ResidenceAddressLine3;
                        this.employee_sharing.Text = a8ast.NoOfEmployeesSharingQRS.ToString();
                        this.resistenceVlaue.Text = a8ast.ResidencePlaceValue.ToString();
                        this.OccupationFromDate.SelectedDate = Utility.toDateTime(a8ast.OccupationFromDate);
                        this.OccupationToDate.SelectedDate = Utility.toDateTime(a8ast.OccupationToDate);
                        this.noofdaystextbox.Text = Convert.ToString(a8ast.NoOfDays);
                        this.no_furniture.Text = Convert.ToString(a8ast.NoOfHardSoftFunniture);
                        this.no_refrigerator.Text = a8ast.NoOfRefrigerators.ToString();
                        this.no_of_surveillance.Text = a8ast.NoOfSurvellance.ToString();
                        this.no_dvd.Text = a8ast.NoOfVideoRecorders.ToString();
                        this._NoOfWashingMachines.Text = a8ast.NoOfWashingMachines.ToString();
                        this.no_of_dryer.Text = a8ast.NoOfDryers.ToString();
                        this.no_of_diswash1.Text = a8ast.NoOfDishWashers.ToString();
                        this.no_of_unitcentral.Text = a8ast.NoOfAirConditioners.ToString();
                        this.no_of_dining.Text = a8ast.NoOfCentralACDining.ToString();
                        this.no_of_sitting.Text = a8ast.NoOfCentralACSitting.ToString();
                        this._no_of_additional.Text = a8ast.NoOfCentralACAdditional.ToString();
                        this.no_of_airpurifier.Text = a8ast.NoOfAirpurifier.ToString();
                        this.no_of_tvplasma1.Text = a8ast.NoOfTVs.ToString();
                        this.no_of_radio1.Text = a8ast.NoOfRadios.ToString();
                        this.no_of_hifi.Text = a8ast.NoOfHiFiStereos.ToString();
                        this.no_of_guitar.Text = a8ast.NoOfElectriGuitar.ToString();
                        this.no_of_computer.Text = a8ast.NoOfComputers.ToString();
                        this.no_of_organ.Text = a8ast.NoOfOrgans.ToString();
                        this.no_of_swimmingpool.Text = a8ast.NoOfSwimmingPools.ToString();
                        this.publicudilities_value.Text = a8ast.PublicUtilities.ToString();
                        this.telephone_value.Text = a8ast.Telephone.ToString();
                        this.pager_value.Text = a8ast.Pager.ToString();
                        this.suitcase_value.Text = a8ast.Suitcase.ToString();
                        this.golfbag_value.Text = a8ast.GolfBagAndAccessories.ToString();
                        this.camera_value.Text = a8ast.Camera.ToString();
                        this.gardener_value.Text = a8ast.GardenerOrCompoundUpkeep.ToString();
                        this.driver_value.Text = a8ast.Driver.ToString();
                        this.sarvent_value.Text = a8ast.Servant.ToString();
                        this.PERSENTBASICPAY.Text = a8ast.Percent2OfBasic.ToString();
                        this.tk_21.Text = a8ast.OtherBenefitsInKindValue.ToString();
                        this.no_of_self.Text = a8ast.SelfWifeChildAbove20NoOfPersons.ToString();
                        this.days_self.Text = a8ast.SelfWifeChildAbove20NoOfDays.ToString();
                        this.no_of_chilbelow3.Text = a8ast.ChildBelow3NoOfPersons.ToString();
                        this.no_of_childabove7.Text = a8ast.ChildBetween3And7NoOfPersons.ToString();
                        this.no_of_child8.Text = a8ast.ChildBetween8And20NoOfPersons.ToString();
                        this.days_childbelow3.Text = a8ast.ChildBelow3NoOfDays.ToString();
                        this.days_childabove7.Text = a8ast.ChildBetween3And7NoOfDays.ToString();
                        this.days_childabove8.Text = a8ast.ChildBetween8And20NoOfDays.ToString();
                        this.no_of_selfpassages.Text = a8ast.NoOfLeavePassageSelf.ToString();
                        this.no_of_passspouse.Text = a8ast.NoOfLeavePassageWife.ToString();
                        this.no_of_passeschildrn.Text = a8ast.NoOfLeavePassageChildren.ToString();
                        this.Costof_leavepassages.Text = a8ast.CostOfLeavePassageAndIncidentalBenefits.ToString();
                        this.ohqstatus.Checked = (bool)a8ast.OHQStatus;
                        this.interestpayment.Text = a8ast.InterestPaidByEmployer.ToString();
                        this.lifeinsurance.Text = a8ast.LifeInsurancePremiumsPaidByEmployer.ToString();
                        this.subsidial_holydays.Text = a8ast.FreeOrSubsidisedHoliday.ToString();
                        this.educational.Text = a8ast.EducationalExpenses.ToString();
                        this.longserviceavard.Text = a8ast.NonMonetaryAwardsForLongService.ToString();
                        this.socialclubsfee.Text = a8ast.EntranceOrTransferFeesToSocialClubs.ToString();
                        this.gainfromassets.Text = a8ast.GainsFromAssets.ToString();
                        this.fullcostofmotor.Text = a8ast.FullCostOfMotorVehicle.ToString();
                        this.carbenefits.Text = a8ast.CarBenefit.ToString();
                        this.non_manetarybenifits.Text = a8ast.OthersBenefits.ToString();
                        this._total_2a_2k.Text = a8ast.FurnitureValue.ToString();
                        this.ta_2.Text = a8ast.HardOrsoftFurnitureItemsValue.ToString();
                        this.tb_2.Text = Convert.ToString((a8ast.RefrigeratorValue + a8ast.VideoRecorderValue));
                        this.tc_2.Text = a8ast.WashingMachineDryerDishWasherValue.ToString();
                        this.td_2.Text = a8ast.AirConditionerValue.ToString();
                        this.te_2.Text = a8ast.TVRadioAmpHiFiStereoElectriGuitarValue.ToString();
                        this.tf_2.Text = a8ast.ComputerAndOrganValue.ToString();
                        this.tg_2.Text = a8ast.SwimmingPoolValue.ToString();
                        this.th_2.Text = Convert.ToString(a8ast.PublicUtilities + a8ast.Telephone + a8ast.Pager + a8ast.Suitcase + a8ast.GolfBagAndAccessories +
                                         a8ast.Camera + a8ast.Servant);

                        this.ti_2.Text = a8ast.Driver.ToString();
                        this.tj_2.Text = a8ast.GardenerOrCompoundUpkeep.ToString();
                        this.tk_2.Text = a8ast.OtherBenefitsInKindValue.ToString();



                        // 3.value of hotel Accommodation
                        this.ta_3.Text = a8ast.SelfWifeChildAbove20Value.ToString();
                        this.tb_3.Text = a8ast.ChildBelow3Value.ToString();
                        this.tc_3.Text = a8ast.ChildBetween3And7Value.ToString();
                        this.td_3.Text = a8ast.ChildBetween8And20Value.ToString();
                        this.totalhotelacoomadation.Text = a8ast.HotelAccommodationValue.ToString();
                        te_3.Text = a8ast.Percent2OfBasic.ToString();
                        this.GarndTotal.Text = a8ast.TotalBenefitsInKind.ToString();


                        this.no_of_spouse.Text = a8ast.SelfWifeChildAbove20NoOfPersons_20above.ToString();
                        this.no_of_childrenabove20.Text = a8ast.SelfWifeChildAbove20NoOfPersons_20above.ToString();
                        this.days_spouse.Text = a8ast.SelfWifeChildAbove20NoOfDays_Spouse.ToString();
                        this.days_childrenabove20.Text = a8ast.SelfWifeChildAbove20NoOfDays_20above.ToString();






                        this.dvdcheck.Checked = (bool)a8ast.C2bc2;
                        this.refcheck.Checked = (bool)a8ast.C2bc1;


                        this.washcheck.Checked = (bool)a8ast.C2cc1;
                        this.drycheck.Checked = (bool)a8ast.C2cc2;
                        this.dishcheck.Checked = (bool)a8ast.C2cc3;

                        this.unitcheck.Checked = (bool)a8ast.C2dc1;
                        this.dinicheck.Checked = (bool)a8ast.C2dc2;
                        this.sittingcheck.Checked = (bool)a8ast.C2dc3;
                        this.additioncheck.Checked = (bool)a8ast.C2dc4;
                        this.airpuifiercheck.Checked = (bool)a8ast.C2dc5;

                        this.tvcheck.Checked = (bool)a8ast.C2ec1;
                        this.radiocheck.Checked = (bool)a8ast.C2ec2;
                        this.hificheck.Checked = (bool)a8ast.C2ec3;
                        this.guitarcheck.Checked = (bool)a8ast.C2ec4;
                        this.surveillance.Checked = (bool)a8ast.C2ec5;

                        this.compcheck.Checked = (bool)a8ast.C2fc1;
                        this.organcheck.Checked = (bool)a8ast.C2fc2;

                        this.popcheck.Checked = (bool)a8ast.C2hc1;
                        this.telecheck.Checked = (bool)a8ast.C2hc2;
                        this.pager.Checked = (bool)a8ast.C2hc3;
                        this.suitcasecheck.Checked = (bool)a8ast.C2hc4;
                        this.golfbagcheck.Checked = (bool)a8ast.C2hc5;
                        this.suitcasecheck.Checked = (bool)a8ast.C2hc6;
                        this.golfbagcheck.Checked = (bool)a8ast.C2hc7;

                        this.selfcheck.Checked = (bool)a8ast.C3ac1;
                        this.spousecheck.Checked = (bool)a8ast.C3ac2;
                        this.childrencheck.Checked = (bool)a8ast.C3ac3;

                    }

                }
            }



       }
       






        protected void ButtonCALCULATE_Click(object sender, EventArgs e)
        {


            A8AST _a8ast = new A8AST();
             
            _a8ast.AVOrRentByEmployer = Convert.ToDecimal(this._AVOrRentByEmployerx1.Value);
            _a8ast.RentByEmployee = Convert.ToDecimal(this._RentByEmployee.Value);
            _a8ast.ResidencePlaceValue = Convert.ToDecimal(this.resistenceVlaue.Value);
            _a8ast.ResidenceAddressLine1 = this.address_label1.Text;
            _a8ast.ResidenceAddressLine2 = this.address_label2.Text;
            _a8ast.ResidenceAddressLine3 = this.address_label3.Text;
            _a8ast.NoOfEmployeesSharingQRS = Convert.ToInt32(this.employee_sharing.Value);
            _a8ast.NoOfHardSoftFunniture = Convert.ToInt32(this.no_furniture.Value);
            _a8ast.NoOfRefrigerators = Convert.ToInt32(this.no_refrigerator.Value);
            _a8ast.NoOfTVs = Convert.ToInt32(this.no_of_tvplasma1.Value);
            _a8ast.NoOfRadios = Convert.ToInt32(this.no_of_radio1.Value);
            _a8ast.NoOfVideoRecorders = Convert.ToInt32(this.no_dvd.Value);
            _a8ast.NoOfWashingMachines = Convert.ToInt32(this._NoOfWashingMachines.Value);
            _a8ast.NoOfDryers = Convert.ToInt32(this.no_of_dryer.Value);
            _a8ast.NoOfDishWashers = Convert.ToInt32(this.no_of_diswash1.Value);
            _a8ast.NoOfAirConditioners = Convert.ToInt32(this.no_of_unitcentral.Value);
            _a8ast.NoOfCentralACDining = Convert.ToInt32(this.no_of_dining.Value);
            _a8ast.NoOfCentralACSitting = Convert.ToInt32(this.no_of_sitting.Value);
            _a8ast.NoOfCentralACAdditional = Convert.ToInt32(this._no_of_additional.Value);
            _a8ast.NoOfAirpurifier = Convert.ToInt32(this.no_of_airpurifier.Value);
            _a8ast.NoOfHiFiStereos = Convert.ToInt32(this.no_of_hifi.Value);
            _a8ast.NoOfElectriGuitar = Convert.ToInt32(this.no_of_guitar.Value);
            _a8ast.NoOfSurvellance = Convert.ToInt32(this.no_of_surveillance.Value);
            _a8ast.NoOfComputers = Convert.ToInt32(this.no_of_computer.Value);
            _a8ast.NoOfOrgans = Convert.ToInt32(this.no_of_organ.Value);
            _a8ast.NoOfSwimmingPools = Convert.ToInt32(this.no_of_swimmingpool.Value);
            _a8ast.PublicUtilities = Convert.ToDecimal(this.publicudilities_value.Value);
            _a8ast.Telephone = Convert.ToDecimal(this.telephone_value.Value);
            _a8ast.Pager = Convert.ToDecimal(this.pager_value.Value);
            _a8ast.Suitcase = Convert.ToDecimal(this.suitcase_value.Value);
            _a8ast.GolfBagAndAccessories = Convert.ToDecimal(this.golfbag_value.Value);
            _a8ast.Camera = Convert.ToDecimal(this.camera_value.Value);
            _a8ast.Servant = Convert.ToDecimal(this.sarvent_value.Value);
            _a8ast.GardenerOrCompoundUpkeep = Convert.ToDecimal(this.gardener_value.Value);
            _a8ast.Driver = Convert.ToDecimal(this.driver_value.Value);
            _a8ast.OtherBenefitsInKindValue = Convert.ToDecimal(this.tk_21.Value);

            _a8ast.SelfWifeChildAbove20NoOfPersons = Convert.ToInt32(this.no_of_self.Value);
            _a8ast.SelfWifeChildAbove20NoOfDays = Convert.ToInt32(this.days_self.Value);

            //ndo self with 
            _a8ast.ChildBelow3NoOfPersons = Convert.ToDecimal(this.no_of_chilbelow3.Value);
            _a8ast.ChildBelow3NoOfDays = Convert.ToDecimal(this.days_childbelow3.Value);

            _a8ast.ChildBetween3And7NoOfPersons = Convert.ToDecimal(this.no_of_childabove7.Value);
            _a8ast.ChildBetween3And7NoOfDays = Convert.ToDecimal(this.days_childabove7.Value);

            _a8ast.ChildBetween8And20NoOfPersons = Convert.ToInt32(this.no_of_child8.Value);
            _a8ast.ChildBetween8And20NoOfDays = Convert.ToDecimal(this.days_childabove8.Value);

            _a8ast.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(this.Costof_leavepassages.Value);
            _a8ast.NoOfLeavePassageSelf = Convert.ToInt32(this.no_of_selfpassages.Value);
            _a8ast.NoOfLeavePassageWife = Convert.ToInt32(this.no_of_passspouse.Value);
            _a8ast.NoOfLeavePassageChildren = Convert.ToInt32(this.no_of_passeschildrn.Value);

            _a8ast.OHQStatus = this.ohqstatus.Checked;

            _a8ast.InterestPaidByEmployer = Convert.ToDecimal(this.interestpayment.Value);
            _a8ast.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(this.lifeinsurance.Value);
            _a8ast.FreeOrSubsidisedHoliday = Convert.ToDecimal(this.subsidial_holydays.Value);
            _a8ast.EducationalExpenses = Convert.ToDecimal(this.educational.Value);
            _a8ast.NonMonetaryAwardsForLongService = Convert.ToInt32(this.longserviceavard.Value);
            _a8ast.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(this.socialclubsfee.Value);
            _a8ast.GainsFromAssets = Convert.ToDecimal(this.gainfromassets.Value);
            _a8ast.FullCostOfMotorVehicle = Convert.ToDecimal(this.fullcostofmotor.Value);
            _a8ast.CarBenefit = Convert.ToDecimal(this.carbenefits.Value);
            _a8ast.OthersBenefits = Convert.ToDecimal(this.non_manetarybenifits.Value);


            _a8ast.OccupationFromDate = this.OccupationFromDate.SelectedDate.Value.ToString("dd MMM yyyy");
            _a8ast.OccupationToDate = this.OccupationToDate.SelectedDate.Value.ToString("dd MMM yyyy");

            _a8ast.SelfWifeChildAbove20NoOfPersons_Spouse = Convert.ToInt32(this.no_of_spouse.Text);
            _a8ast.SelfWifeChildAbove20NoOfPersons_20above = Convert.ToInt32(this.no_of_childrenabove20.Text);
            _a8ast.SelfWifeChildAbove20NoOfDays_Spouse = Convert.ToInt32(this.days_spouse.Text);
            _a8ast.SelfWifeChildAbove20NoOfDays_20above = Convert.ToInt32(this.days_childrenabove20.Text);








            this.te_3.Text = _a8ast.Percent2OfBasic.ToString();

            this.resistenceVlaue.Text = _a8ast.ResidencePlaceValue.ToString();
            this.tc_2.Text = _a8ast.VideoRecorderValue.ToString();
            this.noofdaystextbox.Text = Convert.ToString(_a8ast.NoOfDays);

            this._total_2a_2k.Text = _a8ast.FurnitureValue.ToString();
            this.ta_2.Text = _a8ast.HardOrsoftFurnitureItemsValue.ToString();
            this.tb_2.Text = Convert.ToString((_a8ast.RefrigeratorValue + _a8ast.VideoRecorderValue));
            this.tc_2.Text = _a8ast.WashingMachineDryerDishWasherValue.ToString();
            this.td_2.Text = _a8ast.AirConditionerValue.ToString();
            this.te_2.Text = _a8ast.TVRadioAmpHiFiStereoElectriGuitarValue.ToString();
            this.tf_2.Text = _a8ast.ComputerAndOrganValue.ToString();
            this.tg_2.Text = _a8ast.SwimmingPoolValue.ToString();
            this.th_2.Text = Convert.ToString(_a8ast.PublicUtilities + _a8ast.Telephone + _a8ast.Pager + _a8ast.Suitcase + _a8ast.GolfBagAndAccessories +
                             _a8ast.Camera + _a8ast.Servant);

            this.ti_2.Text = _a8ast.Driver.ToString();
            this.tj_2.Text = _a8ast.GardenerOrCompoundUpkeep.ToString();
            this.tk_2.Text = _a8ast.OtherBenefitsInKindValue.ToString();



            // 3.value of hotel Accommodation
            this.ta_3.Text = _a8ast.SelfWifeChildAbove20Value.ToString();
            this.tb_3.Text = _a8ast.ChildBelow3Value.ToString();
            this.tc_3.Text = _a8ast.ChildBetween3And7Value.ToString();
            this.td_3.Text = _a8ast.ChildBetween8And20Value.ToString();
            this.totalhotelacoomadation.Text = _a8ast.HotelAccommodationValue.ToString();
            te_3.Text = _a8ast.Percent2OfBasic.ToString();
            this.GarndTotal.Text = _a8ast.TotalBenefitsInKind.ToString();
          

        }

        void update_Amendment_appendixA_form()
        {
          
            int recodtype=1;

            A8AST _a8ast_Amendment = new A8AST();


            _a8ast_Amendment.emp_id = Utility.ToInteger(varEmpCode);
            _a8ast_Amendment.NameLine1 = orginal_appx_data1.NameLine1;
            _a8ast_Amendment.NameLine2 = orginal_appx_data1.NameLine2;
            _a8ast_Amendment.IDNo = orginal_appx_data1.IDNo;
            _a8ast_Amendment.IDType = orginal_appx_data1.IDType;
            _a8ast_Amendment.RecordType = recodtype.ToString();
            _a8ast_Amendment.AppendixA_year = yearCode;
            _a8ast_Amendment.AVOrRentByEmployer = Convert.ToDecimal(this._AVOrRentByEmployerx1.Value);
            _a8ast_Amendment.RentByEmployee = Convert.ToDecimal(this._RentByEmployee.Value);
            _a8ast_Amendment.ResidencePlaceValue = Convert.ToDecimal(this.resistenceVlaue.Value);
            _a8ast_Amendment.ResidenceAddressLine1 = this.address_label1.Text;
            _a8ast_Amendment.ResidenceAddressLine2 = this.address_label2.Text;
            _a8ast_Amendment.ResidenceAddressLine3 = this.address_label3.Text;
            _a8ast_Amendment.NoOfEmployeesSharingQRS = Convert.ToInt32(this.employee_sharing.Value);
            _a8ast_Amendment.NoOfHardSoftFunniture = Convert.ToInt32(this.no_furniture.Value);
            _a8ast_Amendment.NoOfRefrigerators = Convert.ToInt32(this.no_refrigerator.Value);
            _a8ast_Amendment.NoOfTVs = Convert.ToInt32(this.no_of_tvplasma1.Value);
            _a8ast_Amendment.NoOfRadios = Convert.ToInt32(this.no_of_radio1.Value);
            _a8ast_Amendment.NoOfVideoRecorders = Convert.ToInt32(this.no_dvd.Value);
            _a8ast_Amendment.NoOfWashingMachines = Convert.ToInt32(this._NoOfWashingMachines.Value);
            _a8ast_Amendment.NoOfDryers = Convert.ToInt32(this.no_of_dryer.Value);
            _a8ast_Amendment.NoOfDishWashers = Convert.ToInt32(this.no_of_diswash1.Value);
            _a8ast_Amendment.NoOfAirConditioners = Convert.ToInt32(this.no_of_unitcentral.Value);
            _a8ast_Amendment.NoOfCentralACDining = Convert.ToInt32(this.no_of_dining.Value);
            _a8ast_Amendment.NoOfCentralACSitting = Convert.ToInt32(this.no_of_sitting.Value);
            _a8ast_Amendment.NoOfCentralACAdditional = Convert.ToInt32(this._no_of_additional.Value);
            _a8ast_Amendment.NoOfAirpurifier = Convert.ToInt32(this.no_of_airpurifier.Value);
            _a8ast_Amendment.NoOfHiFiStereos = Convert.ToInt32(this.no_of_hifi.Value);
            _a8ast_Amendment.NoOfElectriGuitar = Convert.ToInt32(this.no_of_guitar.Value);
            _a8ast_Amendment.NoOfSurvellance = Convert.ToInt32(this.no_of_surveillance.Value);
            _a8ast_Amendment.NoOfComputers = Convert.ToInt32(this.no_of_computer.Value);
            _a8ast_Amendment.NoOfOrgans = Convert.ToInt32(this.no_of_organ.Value);
            _a8ast_Amendment.NoOfSwimmingPools = Convert.ToInt32(this.no_of_swimmingpool.Value);
            _a8ast_Amendment.PublicUtilities = Convert.ToDecimal(this.publicudilities_value.Value);
            _a8ast_Amendment.Telephone = Convert.ToDecimal(this.telephone_value.Value);
            _a8ast_Amendment.Pager = Convert.ToDecimal(this.pager_value.Value);
            _a8ast_Amendment.Suitcase = Convert.ToDecimal(this.suitcase_value.Value);
            _a8ast_Amendment.GolfBagAndAccessories = Convert.ToDecimal(this.golfbag_value.Value);
            _a8ast_Amendment.Camera = Convert.ToDecimal(this.camera_value.Value);
            _a8ast_Amendment.Servant = Convert.ToDecimal(this.sarvent_value.Value);
            _a8ast_Amendment.GardenerOrCompoundUpkeep = Convert.ToDecimal(this.gardener_value.Value);
            _a8ast_Amendment.Driver = Convert.ToDecimal(this.driver_value.Value);

            _a8ast_Amendment.Percent2OfBasic = Convert.ToDecimal(this.PERSENTBASICPAY.Value);

            _a8ast_Amendment.OtherBenefitsInKindValue = Convert.ToDecimal(this.tk_21.Value);

            _a8ast_Amendment.SelfWifeChildAbove20NoOfPersons = Convert.ToInt32(this.no_of_self.Value);
            _a8ast_Amendment.SelfWifeChildAbove20NoOfDays = Convert.ToInt32(this.days_self.Value);

            //ndo self with 
            _a8ast_Amendment.ChildBelow3NoOfPersons = Convert.ToDecimal(this.no_of_chilbelow3.Value);
            _a8ast_Amendment.ChildBelow3NoOfDays = Convert.ToDecimal(this.days_childbelow3.Value);

            _a8ast_Amendment.ChildBetween3And7NoOfPersons = Convert.ToDecimal(this.no_of_childabove7.Value);
            _a8ast_Amendment.ChildBetween3And7NoOfDays = Convert.ToDecimal(this.days_childabove7.Value);

            _a8ast_Amendment.ChildBetween8And20NoOfPersons = Convert.ToInt32(this.no_of_child8.Value);
            _a8ast_Amendment.ChildBetween8And20NoOfDays = Convert.ToDecimal(this.days_childabove8.Value);

            _a8ast_Amendment.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(this.Costof_leavepassages.Value);
            _a8ast_Amendment.NoOfLeavePassageSelf = Convert.ToInt32(this.no_of_selfpassages.Value);
            _a8ast_Amendment.NoOfLeavePassageWife = Convert.ToInt32(this.no_of_passspouse.Value);
            _a8ast_Amendment.NoOfLeavePassageChildren = Convert.ToInt32(this.no_of_passeschildrn.Value);

            _a8ast_Amendment.OHQStatus = this.ohqstatus.Checked;

            _a8ast_Amendment.InterestPaidByEmployer = Convert.ToDecimal(this.interestpayment.Value);
            _a8ast_Amendment.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(this.lifeinsurance.Value);
            _a8ast_Amendment.FreeOrSubsidisedHoliday = Convert.ToDecimal(this.subsidial_holydays.Value);
            _a8ast_Amendment.EducationalExpenses = Convert.ToDecimal(this.educational.Value);
            _a8ast_Amendment.NonMonetaryAwardsForLongService = Convert.ToInt32(this.longserviceavard.Value);
            _a8ast_Amendment.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(this.socialclubsfee.Value);
            _a8ast_Amendment.GainsFromAssets = Convert.ToDecimal(this.gainfromassets.Value);
            _a8ast_Amendment.FullCostOfMotorVehicle = Convert.ToDecimal(this.fullcostofmotor.Value);
            _a8ast_Amendment.CarBenefit = Convert.ToDecimal(this.carbenefits.Value);
            _a8ast_Amendment.OthersBenefits = Convert.ToDecimal(this.non_manetarybenifits.Value);


            _a8ast_Amendment.OccupationFromDate = this.OccupationFromDate.SelectedDate.Value.ToString("ddMMMyyyy");
            _a8ast_Amendment.OccupationToDate = this.OccupationToDate.SelectedDate.Value.ToString("ddMMMyyyy");

            _a8ast_Amendment.SelfWifeChildAbove20NoOfPersons_Spouse = Convert.ToInt32(this.no_of_spouse.Text);
            _a8ast_Amendment.SelfWifeChildAbove20NoOfPersons_20above = Convert.ToInt32(this.no_of_childrenabove20.Text);
            _a8ast_Amendment.SelfWifeChildAbove20NoOfDays_Spouse = Convert.ToInt32(this.days_spouse.Text);
            _a8ast_Amendment.SelfWifeChildAbove20NoOfDays_20above = Convert.ToInt32(this.days_childrenabove20.Text);



            _a8ast_Amendment.C2bc1 = this.refcheck.Checked;
            _a8ast_Amendment.C2bc2 = this.dvdcheck.Checked;

            _a8ast_Amendment.C2cc1 = this.washcheck.Checked;
            _a8ast_Amendment.C2cc2 = this.drycheck.Checked;
            _a8ast_Amendment.C2cc3 = this.dishcheck.Checked;

            _a8ast_Amendment.C2dc1 = this.unitcheck.Checked;
            _a8ast_Amendment.C2dc2 = this.dinicheck.Checked;
            _a8ast_Amendment.C2dc3 = this.sittingcheck.Checked;
            _a8ast_Amendment.C2dc4 = this.additioncheck.Checked;
            _a8ast_Amendment.C2dc5 = this.airpuifiercheck.Checked;

            _a8ast_Amendment.C2ec1 = this.tvcheck.Checked;
            _a8ast_Amendment.C2ec2 = this.radiocheck.Checked;
            _a8ast_Amendment.C2ec3 = this.hificheck.Checked;
            _a8ast_Amendment.C2ec4 = this.guitarcheck.Checked;
            _a8ast_Amendment.C2ec5 = this.surveillance.Checked;

            _a8ast_Amendment.C2fc1 = this.compcheck.Checked;
            _a8ast_Amendment.C2fc2 = this.organcheck.Checked;

            _a8ast_Amendment.C2hc1 = this.popcheck.Checked;
            _a8ast_Amendment.C2hc2 = this.telecheck.Checked;
            _a8ast_Amendment.C2hc3 = this.pager.Checked;
            _a8ast_Amendment.C2hc4 = this.suitcasecheck.Checked;
            _a8ast_Amendment.C2hc5 = this.golfbagcheck.Checked;
            _a8ast_Amendment.C2hc6 = this.suitcasecheck.Checked;
            _a8ast_Amendment.C2hc7 = this.golfbagcheck.Checked;

            _a8ast_Amendment.C3ac1 = this.selfcheck.Checked;
            _a8ast_Amendment.C3ac2 = this.spousecheck.Checked;
            _a8ast_Amendment.C3ac3 = this.childrencheck.Checked;
            _a8ast_Amendment.IS_AMENDMENT = true;


            A8AST _a8ast = new A8AST();
            IQuery query;

            using (ISession session = NHibernateHelper.GetCurrentSession())
            {


                query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + varEmpCode + "and AppendixA_year='" + _a8ast_Amendment.AppendixA_year.ToString() + "'" + "and IS_AMENDMENT=1").AddEntity(typeof(A8AST));
           
                
                if (query.List<A8AST>().Count > 0)
                    {
                        _a8ast = query.List<A8AST>()[0];
                }
           }

            
            
            using (ISession session = NHibernateHelper.GetCurrentSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {


                    if(_a8ast.Id >0)

                    {
                        session.Update(_a8ast_Amendment, _a8ast.Id);
                        transaction.Commit();


                    }
                    else
                    {


                        session.Save(_a8ast_Amendment);
                        transaction.Commit();


                    }
                }

            }


         string   sSQL = "Update ir8a_Amendment Set Benefits_in_kind="+ _a8ast_Amendment.TotalBenefitsInKind.ToString()+" Where Emp_ID =" + varEmpCode;


            try
            {
                if (sSQL != "")
                    DataAccess.ExecuteStoreProc(sSQL);
            }
            catch (Exception msg)
            {
                lblerror.Text = msg.Message.ToString();
            }



        }


        void Page_Render(Object sender, EventArgs e)
        {

            Page.ClientScript.RegisterForEventValidation(this.UniqueID);
        }


        void btnsave_ServerClick(object sender, EventArgs e)
        {



            update_Amendment_appendixA_form();


        }

      


















    }


}


