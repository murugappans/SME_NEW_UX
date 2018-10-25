using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using IRAS.Appendix_B;
using IRAS.Appendix_A;
using System.IO;
using System.Text;
using System.Xml;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Reflection;


using System.Globalization;
namespace IRAS
{
    public partial class Ir8aSetup : System.Web.UI.Page
    {

      //  void update_appendixA_form()
      //  {
      //      A8AST _a8ast;

          
      //      using (ISession session = NHibernateHelper.GetCurrentSession())
      //      {


      //          IQuery query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + varEmpCode + "and AppendixA_year='" + yearCode+"'and IS_AMENDMENT=0").AddEntity(typeof(A8AST));
      //          _a8ast = query.List<A8AST>()[0];
                

      //          if (_a8ast != null)
      //          {
      //              _a8ast.NoOfDays = Convert.ToInt32(this.noofdaystextbox.Value);
                    
      //            //  _a8ast.RentByEmployee = Convert.ToDecimal(this._RentByEmployee.Value);
      //              _a8ast.ResidencePlaceValue = Convert.ToDecimal(this.resistenceVlaue.Value);
      //              _a8ast.ResidenceAddressLine1 = this.address_label1.Text;
      //              _a8ast.ResidenceAddressLine2 = this.address_label2.Text;
      //              _a8ast.ResidenceAddressLine3 = this.address_label3.Text;
      //              _a8ast.NoOfEmployeesSharingQRS = Convert.ToInt32(this.employee_sharing.Value);
      //              //_a8ast.NoOfHardSoftFunniture = 0.0m;//km
      //              //_a8ast.NoOfRefrigerators = 0.0m;//km
      //              //_a8ast.NoOfTVs = 0.0m;//km
      //              //_a8ast.NoOfRadios = 0.0m;//km
      //              //_a8ast.NoOfVideoRecorders = 0.0m;//km
      //              //_a8ast.NoOfWashingMachines = 0.0m;//km
      //              //_a8ast.NoOfDryers = 0.0m;//km
      //              //_a8ast.NoOfDishWashers = 0.0m;//km
      //              //_a8ast.NoOfAirConditioners = 0.0m;//km
      //              //_a8ast.NoOfCentralACDining = 0.0m;//km
      //              //_a8ast.NoOfCentralACSitting = 0.0m;//km
      //              //_a8ast.NoOfCentralACAdditional = 0.0m;//km
      //              //_a8ast.NoOfAirpurifier = 0.0m;//km
      //              //_a8ast.NoOfHiFiStereos = 0.0m;//km
      //              //_a8ast.NoOfElectriGuitar = 0.0m;//km
      //              //_a8ast.NoOfSurvellance = 0.0m;//km
      //              //_a8ast.NoOfComputers = 0.0m;//km
      //              //_a8ast.NoOfOrgans = 0.0m;//km
      //              //_a8ast.NoOfSwimmingPools = 0.0m;//km

      //              //_a8ast.AVOfPremises = Convert.ToDecimal(this.ta_2.Value);
      //              //_a8ast.ValueFurnitureFitting = Convert.ToDecimal(this.tb_2.Value);
      //              //_a8ast.RentPaidToLandlord = Convert.ToDecimal(this.tc_2.Value);
      //              //_a8ast.TaxableValuePlaceOfResidence = Convert.ToDecimal(this.td_2.Value);
      //              //_a8ast.TotalRentPaidByEmployeePlaceOfResidence = Convert.ToDecimal(this.te_2.Value);
      //              //_a8ast.TotalTaxableValuePlaceOfResidence = Convert.ToDecimal(this.tf_2.Value);

      //              _a8ast.PublicUtilities = Convert.ToDecimal(this.tg_2.Value);

      //              _a8ast.Servant = Convert.ToDecimal(this.ti_2.Value);

      //              _a8ast.Driver = Convert.ToDecimal(this.th_2.Value);

               

      //              //_a8ast.OtherBenefitsInKindValue = Convert.ToDecimal(this.tk_21.Value);

      //              //_a8ast.SelfWifeChildAbove20NoOfPersons = Convert.ToInt32(this.no_of_self.Value);
      //              //_a8ast.SelfWifeChildAbove20NoOfDays = Convert.ToInt32(this.days_self.Value);

      //              //ndo self with 
      //              //_a8ast.ChildBelow3NoOfPersons = Convert.ToDecimal(this.no_of_chilbelow3.Value);
      //              //_a8ast.ChildBelow3NoOfDays = Convert.ToDecimal(this.days_childbelow3.Value);

      //              //_a8ast.ChildBetween3And7NoOfPersons = Convert.ToDecimal(this.no_of_childabove7.Value);
      //              //_a8ast.ChildBetween3And7NoOfDays = Convert.ToDecimal(this.days_childabove7.Value);
                   
      //              //_a8ast.ChildBetween8And20NoOfPersons = Convert.ToInt32(this.no_of_child8.Value);
      //              //_a8ast.ChildBetween8And20NoOfDays = Convert.ToDecimal(this.days_childabove8.Value);

      //              _a8ast.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(this.Costof_leavepassages.Value);
      //              _a8ast.NoOfLeavePassageSelf = Convert.ToInt32(this.no_of_selfpassages.Value);
      //              _a8ast.NoOfLeavePassageWife = Convert.ToInt32(this.no_of_passspouse.Value);
      //              _a8ast.NoOfLeavePassageChildren = Convert.ToInt32(this.no_of_passeschildrn.Value);
                  
      //                  _a8ast.OHQStatus = this.ohqstatus.Checked;
                   
      //              _a8ast.InterestPaidByEmployer = Convert.ToDecimal(this.interestpayment.Value);
      //              _a8ast.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(this.lifeinsurance.Value);
      //              _a8ast.FreeOrSubsidisedHoliday = Convert.ToDecimal(this.subsidial_holydays.Value);
      //              _a8ast.EducationalExpenses = Convert.ToDecimal(this.educational.Value);
      //              _a8ast.NonMonetaryAwardsForLongService = Convert.ToInt32(this.longserviceavard.Value);
      //              _a8ast.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(this.socialclubsfee.Value);
      //              _a8ast.GainsFromAssets = Convert.ToDecimal(this.gainfromassets.Value);
      //              _a8ast.FullCostOfMotorVehicle = Convert.ToDecimal(this.fullcostofmotor.Value);
      //              _a8ast.CarBenefit = Convert.ToDecimal(this.carbenefits.Value);
      //              _a8ast.OthersBenefits = Convert.ToDecimal(this.non_manetarybenifits.Value);


      //              _a8ast.OccupationFromDate = this.OccupationFromDate.SelectedDate.Value.ToString("ddMMMyyyy");
      //              _a8ast.OccupationToDate = this.OccupationToDate.SelectedDate.Value.ToString("ddMMMyyyy");

      //              //_a8ast.SelfWifeChildAbove20NoOfPersons_Spouse = Convert.ToInt32(this.no_of_spouse.Text);
      //              //_a8ast.SelfWifeChildAbove20NoOfPersons_20above=  Convert.ToInt32(this.no_of_childrenabove20.Text) ;
      //              //_a8ast.SelfWifeChildAbove20NoOfDays_Spouse= Convert.ToInt32(this.days_spouse.Text);
      //              //_a8ast.SelfWifeChildAbove20NoOfDays_20above=Convert.ToInt32(this.days_childrenabove20.Text);



      ////              _a8ast.C2bc1 = this.refcheck.Checked;
      ////              _a8ast.C2bc2 = this.dvdcheck.Checked;
      
      ////_a8ast.C2cc1=this.washcheck.Checked;
      ////_a8ast.C2cc2=this.drycheck.Checked;
      ////_a8ast.C2cc3=this.dishcheck.Checked;
      
      ////_a8ast.C2dc1=this.unitcheck.Checked;
      ////_a8ast.C2dc2=this.dinicheck.Checked;
      ////_a8ast.C2dc3=this.sittingcheck.Checked;
      ////_a8ast.C2dc4=this.additioncheck.Checked;
      ////_a8ast.C2dc5=this.airpuifiercheck.Checked;
      
      ////_a8ast.C2ec1=this.tvcheck.Checked;
      ////_a8ast.C2ec2=this.radiocheck.Checked;
      ////_a8ast.C2ec3=this.hificheck.Checked;
      ////_a8ast.C2ec4=this.guitarcheck.Checked;
      ////_a8ast.C2ec5=this.surveillance.Checked;
      
      ////_a8ast.C2fc1=this.compcheck.Checked;
      ////_a8ast.C2fc2=this.organcheck.Checked;

      ////_a8ast.C2hc1=this.popcheck.Checked;
      ////_a8ast.C2hc2=this.telecheck.Checked;
      ////_a8ast.C2hc3=this.pager.Checked;
      ////_a8ast.C2hc4=this.suitcasecheck.Checked;
      ////_a8ast.C2hc5=this.golfbagcheck.Checked;
      ////_a8ast.C2hc6=this.suitcasecheck.Checked;
      ////_a8ast.C2hc7=this.golfbagcheck.Checked;
      
      ////_a8ast.C3ac1=this.selfcheck.Checked;
      ////_a8ast.C3ac2=this.spousecheck.Checked;
      ////_a8ast.C3ac3=this.childrencheck.Checked;

















      //          }


      //          using (ITransaction transaction = session.BeginTransaction())
      //          {
      //              session.Save(_a8ast);
      //              transaction.Commit();
      //          }


      //      }
         
      //  }
     
        //void fill_appendixA_form(employe _em)
        //{






        //    if (!IsPostBack)
        //    {

        //        A8AST a8ast;

        //        using (ISession session = NHibernateHelper.GetCurrentSession())
        //        {
        //            //a8ast= session.Get<A8AST>(Utility.ToInteger(varEmpCode));





        //            IQuery query = session.CreateSQLQuery(@" SELECT * FROM [A8AST]where emp_id=" + varEmpCode + " and AppendixA_year='" + yearCode+"' and IS_AMENDMENT=0").AddEntity(typeof(A8AST));
                   
                    
        //            if (query != null)
        //            {


        //                if (query.List<A8AST>().Count > 0)
        //                {

        //                    a8ast = query.List<A8AST>()[0];
        //                    this.noofdaystextbox.Text = a8ast.NoOfDays.ToString();
        //                    a8ast.ResidenceAddressLine1 = _em.block_no + " " + _em.level_no + _em.unit_number;
        //                    a8ast.ResidenceAddressLine2 = _em.street_name;
        //                    a8ast.ResidenceAddressLine3 = "Singapore-" + _em.postal_code;
        //                    //this._AVOrRentByEmployerx1.Text = a8ast.AVOrRentByEmployer.ToString();
        //                   // this._RentByEmployee.Text = a8ast.RentByEmployee.ToString();
        //                    this.address_label1.Text = a8ast.ResidenceAddressLine1;
        //                    this.address_label2.Text = a8ast.ResidenceAddressLine2;
        //                    this.address_label3.Text = a8ast.ResidenceAddressLine3;

        //                    this.employee_sharing.Text = a8ast.NoOfEmployeesSharingQRS.ToString();

                           
        //                    this.OccupationFromDate.SelectedDate = Utility.toDateTime(a8ast.OccupationFromDate);
        //                    this.OccupationToDate.SelectedDate = Utility.toDateTime(a8ast.OccupationToDate);
        //                    this.noofdaystextbox.Text = Convert.ToString(a8ast.NoOfDays);
        //                    //this.no_furniture.Text = Convert.ToString(a8ast.NoOfHardSoftFunniture);//
        //                    //this.no_refrigerator.Text = a8ast.NoOfRefrigerators.ToString();//
        //                    //this.no_of_surveillance.Text = a8ast.NoOfSurvellance.ToString();//
        //                    //this.no_dvd.Text = a8ast.NoOfVideoRecorders.ToString();//
        //                    //this._NoOfWashingMachines.Text = a8ast.NoOfWashingMachines.ToString();//
        //                    //this.no_of_dryer.Text = a8ast.NoOfDryers.ToString();//
        //                    //this.no_of_diswash1.Text = a8ast.NoOfDishWashers.ToString();//
        //                    //this.no_of_unitcentral.Text = a8ast.NoOfAirConditioners.ToString();//
        //                    //this.no_of_dining.Text = a8ast.NoOfCentralACDining.ToString();//
        //                    //this.no_of_sitting.Text = a8ast.NoOfCentralACSitting.ToString();//
        //                    //this._no_of_additional.Text = a8ast.NoOfCentralACAdditional.ToString();//
        //                    //this.no_of_airpurifier.Text = a8ast.NoOfAirpurifier.ToString();//
        //                    //this.no_of_tvplasma1.Text = a8ast.NoOfTVs.ToString();//
        //                    //this.no_of_radio1.Text = a8ast.NoOfRadios.ToString();//
        //                    //this.no_of_hifi.Text = a8ast.NoOfHiFiStereos.ToString();//
        //                    //this.no_of_guitar.Text = a8ast.NoOfElectriGuitar.ToString();//
        //                    //this.no_of_computer.Text = a8ast.NoOfComputers.ToString();//
        //                    //this.no_of_organ.Text = a8ast.NoOfOrgans.ToString();//
        //                    //this.no_of_swimmingpool.Text = a8ast.NoOfSwimmingPools.ToString();//
        //                    //this.publicudilities_value.Text = a8ast.PublicUtilities.ToString();//
        //                    //this.telephone_value.Text = a8ast.Telephone.ToString();//
        //                    //this.pager_value.Text = a8ast.Pager.ToString();//
        //                    //this.suitcase_value.Text = a8ast.Suitcase.ToString();//
        //                    //this.golfbag_value.Text = a8ast.GolfBagAndAccessories.ToString();
        //                    //this.camera_value.Text = a8ast.Camera.ToString();//
        //                    //this.gardener_value.Text = a8ast.GardenerOrCompoundUpkeep.ToString();//
        //                    //this.driver_value.Text = a8ast.Driver.ToString();
        //                    //this.sarvent_value.Text = a8ast.Servant.ToString();
        //                    //this.PERSENTBASICPAY.Text = a8ast.Percent2OfBasic.ToString();
        //                    //this.tk_21.Text = a8ast.OtherBenefitsInKindValue.ToString();
        //                    //this.no_of_self.Text = a8ast.SelfWifeChildAbove20NoOfPersons.ToString();
        //                    //this.days_self.Text = a8ast.SelfWifeChildAbove20NoOfDays.ToString();
        //                    //this.no_of_chilbelow3.Text = a8ast.ChildBelow3NoOfPersons.ToString();
        //                    //this.no_of_childabove7.Text = a8ast.ChildBetween3And7NoOfPersons.ToString();
        //                    //this.no_of_child8.Text = a8ast.ChildBetween8And20NoOfPersons.ToString();
        //                    //this.days_childbelow3.Text = a8ast.ChildBelow3NoOfDays.ToString();
        //                    //this.days_childabove7.Text = a8ast.ChildBetween3And7NoOfDays.ToString();
        //                    //this.days_childabove8.Text = a8ast.ChildBetween8And20NoOfDays.ToString();
        //                    this.no_of_selfpassages.Text = a8ast.NoOfLeavePassageSelf.ToString();
        //                    this.no_of_passspouse.Text = a8ast.NoOfLeavePassageWife.ToString();
        //                    this.no_of_passeschildrn.Text = a8ast.NoOfLeavePassageChildren.ToString();
        //                    this.Costof_leavepassages.Text = a8ast.CostOfLeavePassageAndIncidentalBenefits.ToString();
        //                    this.ohqstatus.Checked = (bool)a8ast.OHQStatus;
        //                    this.interestpayment.Text = a8ast.InterestPaidByEmployer.ToString();
        //                    this.lifeinsurance.Text = a8ast.LifeInsurancePremiumsPaidByEmployer.ToString();
        //                    this.subsidial_holydays.Text = a8ast.FreeOrSubsidisedHoliday.ToString();
        //                    this.educational.Text = a8ast.EducationalExpenses.ToString();
        //                    this.longserviceavard.Text = a8ast.NonMonetaryAwardsForLongService.ToString();
        //                    this.socialclubsfee.Text = a8ast.EntranceOrTransferFeesToSocialClubs.ToString();
        //                    this.gainfromassets.Text = a8ast.GainsFromAssets.ToString();
        //                    this.fullcostofmotor.Text = a8ast.FullCostOfMotorVehicle.ToString();
        //                    this.carbenefits.Text = a8ast.CarBenefit.ToString();
        //                    this.non_manetarybenifits.Text = a8ast.OthersBenefits.ToString();
        //                    this._total_2a_2k.Text = a8ast.FurnitureValue.ToString();

        //                    //this.ta_2.Text = a8ast.AVOfPremises.ToString();
        //                    //this.tb_2.Text = a8ast.ValueFurnitureFitting.ToString();
        //                    //this.tc_2.Text = a8ast.RentPaidToLandlord.ToString();
        //                    //this.td_2.Text = a8ast.TaxableValuePlaceOfResidence.ToString();
        //                    //this.te_2.Text = a8ast.TotalRentPaidByEmployeePlaceOfResidence.ToString();
        //                    //this.tf_2.Text = a8ast.TotalTaxableValuePlaceOfResidence.ToString();


        //                    this.tg_2.Text = a8ast.PublicUtilities.ToString();
        //                    this.th_2.Text = a8ast.Driver.ToString();
        //                    this.ti_2.Text = a8ast.Servant.ToString();
                           
        //                    //this.tk_2.Text = a8ast.OtherBenefitsInKindValue.ToString();



        //                    // 3.value of hotel Accommodation
        //                    this.ta_3.Text = a8ast.SelfWifeChildAbove20Value.ToString();
        //                    this.tb_3.Text = a8ast.ChildBelow3Value.ToString();
        //                    //this.tc_3.Text = a8ast.ChildBetween3And7Value.ToString();
        //                    //this.td_3.Text = a8ast.ChildBetween8And20Value.ToString();
        //                    //this.totalhotelacoomadation.Text = a8ast.HotelAccommodationValue.ToString();
        //                    te_3.Text = a8ast.Percent2OfBasic.ToString();
        //                   // this.GarndTotal.Text = a8ast.TotalBenefitsInKind.ToString();


        //                    //this.no_of_spouse.Text = a8ast.SelfWifeChildAbove20NoOfPersons_20above.ToString();
        //                    //this.no_of_childrenabove20.Text = a8ast.SelfWifeChildAbove20NoOfPersons_20above.ToString();
        //                    //this.days_spouse.Text = a8ast.SelfWifeChildAbove20NoOfDays_Spouse.ToString();
        //                    //this.days_childrenabove20.Text = a8ast.SelfWifeChildAbove20NoOfDays_20above.ToString();






        //                    //this.dvdcheck.Checked = (bool)a8ast.C2bc2;
        //                    //this.refcheck.Checked = (bool)a8ast.C2bc1;


        //                    //this.washcheck.Checked = (bool)a8ast.C2cc1;
        //                    //this.drycheck.Checked = (bool)a8ast.C2cc2;
        //                    //this.dishcheck.Checked = (bool)a8ast.C2cc3;

        //                    //this.unitcheck.Checked = (bool)a8ast.C2dc1;
        //                    //this.dinicheck.Checked = (bool)a8ast.C2dc2;
        //                    //this.sittingcheck.Checked = (bool)a8ast.C2dc3;
        //                    //this.additioncheck.Checked = (bool)a8ast.C2dc4;
        //                    //this.airpuifiercheck.Checked = (bool)a8ast.C2dc5;

        //                    //this.tvcheck.Checked = (bool)a8ast.C2ec1;
        //                    //this.radiocheck.Checked = (bool)a8ast.C2ec2;
        //                    //this.hificheck.Checked = (bool)a8ast.C2ec3;
        //                    //this.guitarcheck.Checked = (bool)a8ast.C2ec4;
        //                    //this.surveillance.Checked = (bool)a8ast.C2ec5;

        //                    //this.compcheck.Checked = (bool)a8ast.C2fc1;
        //                    //this.organcheck.Checked = (bool)a8ast.C2fc2;

        //                    //this.popcheck.Checked = (bool)a8ast.C2hc1;
        //                    //this.telecheck.Checked = (bool)a8ast.C2hc2;
        //                    //this.pager.Checked = (bool)a8ast.C2hc3;
        //                    //this.suitcasecheck.Checked = (bool)a8ast.C2hc4;
        //                    //this.golfbagcheck.Checked = (bool)a8ast.C2hc5;
        //                    //this.suitcasecheck.Checked = (bool)a8ast.C2hc6;
        //                    //this.golfbagcheck.Checked = (bool)a8ast.C2hc7;

        //                    //this.selfcheck.Checked = (bool)a8ast.C3ac1;
        //                    //this.spousecheck.Checked = (bool)a8ast.C3ac2;
        //                    //this.childrencheck.Checked = (bool)a8ast.C3ac3;

        //                }







        //            }
        //        }
        //    }
        //}

        void fill_appendixA_form_sql(employe _em)
        {
            A8AST a8ast= new A8AST();
            SqlDataReader sqlDr = null;

            string SQL = @"SELECT  [Id]
      ,[emp_id]
      ,[AppendixA_year]
      ,[RecordType]
      ,[IDType]
      ,[IDNo]
      ,[NameLine1]
      ,[NameLine2]
      ,[ResidencePlaceValue]
      ,[ResidenceAddressLine1]
      ,[ResidenceAddressLine2]
      ,[ResidenceAddressLine3]
      ,[OccupationFromDate]
      ,[OccupationToDate]
      ,[NoOfDays]
      ,[AVOrRentByEmployer]
      ,[NoOfEmployeeSharing]
      ,[PublicUtilities]
      ,[Servant]
      ,[Driver]
      
      ,[HotelAccommodationValue]
      
      ,[CostOfLeavePassageAndIncidentalBenefits]
      ,[NoOfLeavePassageSelf]
      ,[NoOfLeavePassageWife]
      ,[NoOfLeavePassageChildren]
      ,ISNULL([OHQStatus],0)OHQStatus
      ,[InterestPaidByEmployer]
      ,[LifeInsurancePremiumsPaidByEmployer]
      ,[FreeOrSubsidisedHoliday]
      ,[EducationalExpenses]
      ,[NonMonetaryAwardsForLongService]
      ,[EntranceOrTransferFeesToSocialClubs]
      ,[GainsFromAssets]
      ,[FullCostOfMotorVehicle]
      ,[CarBenefit]
      ,[OthersBenefits]
      ,[TotalBenefitsInKind]
      ,[NoOfEmployeesSharingQRS]
      ,[Filler]
      ,[Remarks]
          
      ,[IS_AMENDMENT]
      ,[AVOfPremises]
      ,[ValueFurnitureFittingInd]
      ,[ValueFurnitureFitting]
      ,[RentPaidToLandlord]
      ,[TaxableValuePlaceOfResidence]
      ,[TotalRentPaidByEmployeePlaceOfResidence]
      ,[TotalTaxableValuePlaceOfResidence]
      ,[ActualHotelAccommodation]
      ,[AmountPaidByEmployee]
      ,[TaxableValueHotelAccommodation]
       ,TotalBenefitsInKind
        ,TaxableValueUtilitiesHouseKeeping
  FROM [A8AST]where emp_id=" + varEmpCode + " and AppendixA_year='" + yearCode+"' and IS_AMENDMENT=0";

            sqlDr = DataAccess.ExecuteReader(CommandType.Text, SQL, null);
            while (sqlDr.Read())
            {




                this.noofdaystextbox.Text = a8ast.NoOfDays.ToString();

                //this.address_label1.Text = _em.block_no + " " + _em.level_no + _em.unit_number;
                //this.address_label2.Text = _em.street_name;
                //this.address_label3.Text = "Singapore-" + _em.postal_code;

                this.address_label1.Text = Convert.ToString(sqlDr["ResidenceAddressLine1"].ToString());
                this.address_label2.Text = Convert.ToString(sqlDr["ResidenceAddressLine2"].ToString());
                this.address_label3.Text = Convert.ToString(sqlDr["ResidenceAddressLine3"].ToString());

               
                this.employee_sharing.Text = Convert.ToString(sqlDr["NoOfEmployeeSharing"].ToString());


                this.OccupationFromDate.SelectedDate = Utility.toDateTime(sqlDr["OccupationFromDate"].ToString());
                this.OccupationToDate.SelectedDate = Utility.toDateTime(sqlDr["OccupationToDate"].ToString());
                this.noofdaystextbox.Text = Convert.ToString(sqlDr["NoOfDays"].ToString());
                this.no_of_selfpassages.Text = Convert.ToString(sqlDr["NoOfLeavePassageSelf"].ToString());
                this.no_of_passspouse.Text = Convert.ToString(sqlDr["NoOfLeavePassageWife"].ToString());
                this.no_of_passeschildrn.Text = Convert.ToString(sqlDr["NoOfLeavePassageChildren"].ToString());
                this.Costof_leavepassages.Text = Convert.ToString(sqlDr["CostOfLeavePassageAndIncidentalBenefits"].ToString());
              //  this.ohqstatus.Checked =   Convert.ToBoolean(sqlDr["OHQStatus"].ToString());
                this.interestpayment.Text =Convert.ToString(sqlDr["InterestPaidByEmployer"].ToString());
                this.lifeinsurance.Text = Convert.ToString(sqlDr["LifeInsurancePremiumsPaidByEmployer"].ToString());
                this.subsidial_holydays.Text = Convert.ToString(sqlDr["FreeOrSubsidisedHoliday"].ToString());
                this.educational.Text = Convert.ToString(sqlDr["EducationalExpenses"].ToString());
                this.longserviceavard.Text = Convert.ToString(sqlDr["NonMonetaryAwardsForLongService"].ToString());
                this.socialclubsfee.Text = Convert.ToString(sqlDr["EntranceOrTransferFeesToSocialClubs"].ToString());
                this.gainfromassets.Text = Convert.ToString(sqlDr["GainsFromAssets"].ToString());
                this.fullcostofmotor.Text = Convert.ToString(sqlDr["FullCostOfMotorVehicle"].ToString());
                this.carbenefits.Text = Convert.ToString(sqlDr["CarBenefit"].ToString());
                this.non_manetarybenifits.Text = Convert.ToString(sqlDr["OthersBenefits"].ToString());
              //  this._total_2a_2k.Text = Convert.ToString(sqlDr["FurnitureValue"].ToString());
                this.tg_2.Text = Convert.ToString(sqlDr["PublicUtilities"].ToString());
                this.th_2.Text = Convert.ToString(sqlDr["Driver"].ToString());
                this.ti_2.Text = Convert.ToString(sqlDr["Servant"].ToString());
                this.tj_2.Text = Convert.ToString(sqlDr["TaxableValueUtilitiesHouseKeeping"].ToString());

                this.ta_2.Text =  Convert.ToString(sqlDr["AVOfPremises"].ToString());
               // this.tb_2.Text = Convert.ToString(sqlDr["ValueFurnitureFittingInd"].ToString());
                this.tb_2.Text = Convert.ToString(sqlDr["ValueFurnitureFitting"].ToString());
                this.tc_2.Text =  Convert.ToString(sqlDr["RentPaidToLandlord"].ToString());
                this.td_2.Text =  Convert.ToString(sqlDr["TaxableValuePlaceOfResidence"].ToString());
                this.te_2.Text =  Convert.ToString(sqlDr["TotalRentPaidByEmployeePlaceOfResidence"].ToString());
                this.tf_2.Text =  Convert.ToString(sqlDr["TotalTaxableValuePlaceOfResidence"].ToString());
                this.totalvalueofbenifits.Text = Convert.ToString(sqlDr["TotalBenefitsInKind"].ToString());

                this.ta_3.Text = Convert.ToString(sqlDr["ActualHotelAccommodation"].ToString());
                this.tb_3.Text = Convert.ToString(sqlDr["AmountPaidByEmployee"].ToString());
                this.tc_3.Text = Convert.ToString(sqlDr["TaxableValueHotelAccommodation"].ToString());
                //this.GarndTotal.Text = Convert.ToString(sqlDr["TotalBenefitsInKind"].ToString());


            }


        }

        void update_appendixA_sql()
        {

            A8AST a8sat = new A8AST();

            if (Convert.ToDecimal(this.longserviceavard.Value) != 0.00m && Convert.ToDecimal(this.longserviceavard.Value) <= 200.00m)
            {

                ShowMessageBox("Long Service must be 200 and Above");
                return;
            }


            string sql = "UPDATE [dbo].[A8AST] SET [OccupationFromDate] ='" + this.OccupationFromDate.SelectedDate.Value.ToString("dd MMM yyyy") + 
      "',[OccupationToDate] = '"+this.OccupationToDate.SelectedDate.Value.ToString("dd MMM yyyy") +
      "',[NoOfDays] ='"+ this.noofdaystextbox.Text+
     // ",[AVOrRentByEmployer] ='"+this.employee_sharing.Text+
      "',[NoOfEmployeeSharing] ='"+ this.employee_sharing.Text+
      
   

      "',[PublicUtilities] ='"+ this.tg_2.Text+
     
      "',[Servant] ="+this.ti_2.Text+
      ",[Driver] ='"+ this.th_2.Text+
     
    //  ",[HotelAccommodationValue] ='"+ <HotelAccommodationValue, decimal(18,2),>

      "',[ResidenceAddressLine1] ='" + this.address_label1.Text +
      "',[ResidenceAddressLine2] ='" + this.address_label2.Text +
      "',[ResidenceAddressLine3] ='" + this.address_label3.Text +
     
      "',[CostOfLeavePassageAndIncidentalBenefits] ='"+ this.Costof_leavepassages.Text+
      "',[NoOfLeavePassageSelf] ='"+ this.no_of_selfpassages.Text+
      "',[NoOfLeavePassageWife] ='"+this.no_of_passspouse.Text+
      "',[NoOfLeavePassageChildren] ='"+    this.no_of_passeschildrn.Text+
    //  "',[OHQStatus] ='"+this.ohqstatus.Checked.ToString()+
      "',[InterestPaidByEmployer] ='"+this.interestpayment.Text+
      "',[LifeInsurancePremiumsPaidByEmployer] ='"+ this.lifeinsurance.Text+
      "',[FreeOrSubsidisedHoliday] ='"+  this.subsidial_holydays.Text +
      "',[EducationalExpenses] ='"+    this.educational.Text+
      "',[NonMonetaryAwardsForLongService] ='"+  this.longserviceavard.Text+
      "',[EntranceOrTransferFeesToSocialClubs] ='"+  this.socialclubsfee.Text+
      "',[GainsFromAssets] ='"+  this.gainfromassets.Text+
      "',[FullCostOfMotorVehicle] ='"+   this.fullcostofmotor.Text+
      "',[CarBenefit] ='"+this.carbenefits.Text+
      "',[OthersBenefits] ='"+  this.non_manetarybenifits.Text+
 
      "',[AVOfPremises] ='"+  this.ta_2.Text+
     // "',[ValueFurnitureFittingInd] ='"+  this.tb_2.Text+
      "',[ValueFurnitureFitting] ='"+  this.tb_2.Text+
      "',[RentPaidToLandlord] ='"+  this.tc_2.Text+
      "',[TaxableValuePlaceOfResidence] ='"+  this.td_2.Text+
      "',[TotalRentPaidByEmployeePlaceOfResidence] ='"+ this.te_2.Text+
      "',[TotalTaxableValuePlaceOfResidence] ='"+  this.tf_2.Text+
       "',[TotalBenefitsInKind] ='" + this.totalvalueofbenifits.Text +
        "',[ActualHotelAccommodation] ='" + this.ta_3.Text +
        "',[AmountPaidByEmployee] ='" + this.tb_3.Text +
        "',[TaxableValueHotelAccommodation] ='" + this.tc_3.Text +
         "',[TaxableValueUtilitiesHouseKeeping] ='" + this.tj_2.Text +
         "' where emp_id=" + varEmpCode + " and AppendixA_year='" + yearCode + "' and IS_AMENDMENT=0";


            int Update_result = DataAccess.ExecuteStoreProc(sql);
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
where k.emp_id='" + varEmpCode + "' and AppendixB_year='" + yearCode + "' and IS_AMMENDMENT=0";
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




                Control[] allControls = helper.FlattenHierachy(RadAjaxPanel4);

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





                //sSQL = "Select block_no,Level_no,Unit_no,postal_code,STREET_NAME, foreignAddress1,foreignAddress2,foreignPostalCode From Employee where emp_code=" + varEmpCode + " and company_id=" + compid;
                //sqlDr = DataAccess.ExecuteReader(CommandType.Text, sSQL, null);
                //while (sqlDr.Read())
                //{
                //    block_no = Convert.ToString(sqlDr["block_no"].ToString());
                //    Level_no = Convert.ToString(sqlDr["Level_no"].ToString());
                //    Unit_no = Convert.ToString(sqlDr["Unit_no"].ToString());
                //    postal_code = Convert.ToString(sqlDr["postal_code"].ToString());
                //    strname = Convert.ToString(sqlDr["STREET_NAME"].ToString());
                //    faddress1 = Convert.ToString(sqlDr["foreignAddress1"].ToString());
                //    faddress2 = Convert.ToString(sqlDr["foreignAddress2"].ToString());
                //    fPostalCode = Convert.ToString(sqlDr["foreignPostalCode"].ToString());
                //}



            }
        }


        void update_appendixB_form()
        {

            SqlDataReader sqlDr = null;

            int FK_DETAILS = 0;

            string sql = @"SELECT  [ID]
     
  FROM [A8B2009ST]
  where emp_id='" + varEmpCode + "' and AppendixB_year='" + yearCode + "'and IS_AMMENDMENT=0";


            sqlDr = DataAccess.ExecuteReader(CommandType.Text, sql, null);

            while (sqlDr.Read())
            {
                FK_DETAILS = Convert.ToInt32(sqlDr["ID"]);
            }



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


            _sectionA1.G_Total_L = _sectionA1.Total_L+ _sectionA2.Total_L+ _sectionA3.Total_L;
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
     



            _sectionB1.G_Total_I = _sectionB1.I_J_K + _sectionB2.I_J_K+ _sectionB3.I_J_K;
         
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

            _sectionC1.G_Total_J = _sectionC1.I_J_K + _sectionC2.I_J_K+ _sectionC3.I_J_K;
         

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


        protected static void ShowMessageBox(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                if (message.EndsWith("."))
                    message = message.Substring(0, message.Length - 1);
            }
            StringBuilder sbScript = new StringBuilder();


            //Java Script header            
            sbScript.Append("<script type='text/javascript'>" + Environment.NewLine);
            sbScript.Append("// Show messagebox" + Environment.NewLine);
            message = message.Replace("<br/>", "\\n").Replace("\"", "'");
            sbScript.Append(@"alert( """ + message + @""" );");
            sbScript.Append(@"</script>");

            HttpContext.Current.Response.Write(sbScript);
        }

        
        protected void ButtonCALCULATE_Click(object sender, EventArgs e)
        {


            A8AST _a8ast = new A8AST();
            _a8ast.NoOfDays = Convert.ToInt32(this.noofdaystextbox.Value);
            _a8ast.AVOfPremises = Convert.ToDecimal(this.ta_2.Value);
            _a8ast.ValueFurnitureFitting = Convert.ToDecimal(this.tb_2.Value);
            _a8ast.RentPaidToLandlord = Convert.ToDecimal(this.tc_2.Value);
            _a8ast.ResidenceAddressLine1 = this.address_label1.Text;
            _a8ast.ResidenceAddressLine2 = this.address_label2.Text;
            _a8ast.ResidenceAddressLine3 = this.address_label3.Text;
            _a8ast.NoOfEmployeesSharingQRS = Convert.ToInt32(this.employee_sharing.Value);

            _a8ast.TotalRentPaidByEmployeePlaceOfResidence = Convert.ToDecimal(te_2.Value);


            _a8ast.UtilitiesTelPagerSuitCaseAccessories = Convert.ToDecimal(tg_2.Value);


           
           
              _a8ast.Driver = Convert.ToDecimal(this.th_2.Value);



            _a8ast.ServantGardener = Convert.ToDecimal(this.ti_2.Value);



            _a8ast.ActualHotelAccommodation = Convert.ToDecimal(this.ta_3.Value);

            _a8ast.AmountPaidByEmployee = Convert.ToDecimal(tb_3.Value);

           
            _a8ast.CostOfLeavePassageAndIncidentalBenefits = Convert.ToDecimal(this.Costof_leavepassages.Value);

            _a8ast.NoOfLeavePassageSelf = Convert.ToInt32(this.no_of_selfpassages.Value);
            _a8ast.NoOfLeavePassageSpouse = Convert.ToInt32(this.no_of_passspouse.Value);
            _a8ast.NoOfLeavePassageChildren = Convert.ToInt32(this.no_of_passeschildrn.Value);

          //  _a8ast.OHQStatus = this.ohqstatus.Checked;


            _a8ast.InterestPaidByEmployer = Convert.ToDecimal(this.interestpayment.Value);
            _a8ast.LifeInsurancePremiumsPaidByEmployer = Convert.ToDecimal(this.lifeinsurance.Value);
            _a8ast.FreeOrSubsidisedHoliday = Convert.ToDecimal(this.subsidial_holydays.Value);
            _a8ast.EducationalExpenses = Convert.ToDecimal(this.educational.Value);
            _a8ast.NonMonetaryAwardsForLongService = Convert.ToInt32(this.longserviceavard.Value);

            if (Convert.ToDecimal(this.longserviceavard.Value) != 0.00m && Convert.ToDecimal(this.longserviceavard.Value) <= 200.00m)
            {

                ShowMessageBox("Long Service must be above 200");
                return;
            }

            
            _a8ast.EntranceOrTransferFeesToSocialClubs = Convert.ToDecimal(this.socialclubsfee.Value);
            _a8ast.GainsFromAssets = Convert.ToDecimal(this.gainfromassets.Value);
            _a8ast.FullCostOfMotorVehicle = Convert.ToDecimal(this.fullcostofmotor.Value);
            _a8ast.CarBenefit = Convert.ToDecimal(this.carbenefits.Value);
            _a8ast.OthersBenefits = Convert.ToDecimal(this.non_manetarybenifits.Value);


            _a8ast.OccupationFromDate = this.OccupationFromDate.SelectedDate.Value.ToString("ddMMMyyyy");
            _a8ast.OccupationToDate = this.OccupationToDate.SelectedDate.Value.ToString("ddMMMyyyy");

        
            //calculation field

            if (ta_2.Value <= 0.00 && tb_2.Value <= 0.00)
            {
                _a8ast.TaxableValuePlaceOfResidence = Convert.ToDecimal(tc_2.Value);

            }
            else
            {
                _a8ast.TaxableValuePlaceOfResidence = Convert.ToDecimal(ta_2.Value+tb_2.Value);
            }

            this.td_2.Text = _a8ast.TaxableValuePlaceOfResidence.ToString();

            this.tf_2.Text = Convert.ToString(_a8ast.TaxableValuePlaceOfResidence - _a8ast.TotalRentPaidByEmployeePlaceOfResidence);

           
            this.noofdaystextbox.Text = Convert.ToString(_a8ast.NoOfDays);



            this.tj_2.Text = Convert.ToString(_a8ast.UtilitiesTelPagerSuitCaseAccessories + _a8ast.Driver + _a8ast.ServantGardener);

            this.tc_3.Text = Convert.ToString(_a8ast.ActualHotelAccommodation - _a8ast.AmountPaidByEmployee);


            this.totalvalueofbenifits.Text = _a8ast.TotalBenefitsInKind.ToString();








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
