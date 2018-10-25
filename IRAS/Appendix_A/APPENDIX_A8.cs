using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace IRAS
{


    public class APPENDIX_A8
    {

        public APPENDIX_A8()
        {
        }



        private int _Id;
        private int _emp_id;
        private int _AppendixA_year;
        private string _RecordType;
        private string _IDType;
        private string _IDNo;
        private string _NameLine1;
        private string _NameLine2;
        private decimal _ResidencePlaceValue;
        private string _ResidenceAddressLine1;
        private string _ResidenceAddressLine2;
        private string _ResidenceAddressLine3;
        private string _OccupationFromDate;
        private string _OccupationToDate;
        private int _NoOfDays;
        private decimal _AVOrRentByEmployer;
        private int _NoOfEmployeeSharing;
        private decimal _RentByEmployee;
        private decimal _FurnitureValue;
        private decimal _HardOrsoftFurnitureItemsRatePM;
        private decimal _HardOrsoftFurnitureItemsValue;
        private decimal _RefrigeratorValue;
        private decimal _RefrigeratorRatePM;
        private int _NoOfRefrigerators;
        private decimal _VideoRecorderValue;
        private decimal _VideoRecorderRatePM;
        private int _NoOfVideoRecorders;
        private decimal _WashingMachineDryerDishWasherValue;
        private decimal _WashingMachineDryerDishWasherRatePM;
        private int _NoOfWashingMachines;
        private int _NoOfDryers;
        private int _NoOfDishWashers;
        private decimal _AirConditionerValue;
        private int _NoOfAirConditioners;
        private decimal _AirConditionersUnitRatePM;
        private int _NoOfCentralACDining;
        private decimal _CentralACDining;
        private int _NoOfCentralACSitting;
        private decimal _CentralACSitting;
        private int _NoOfCentralACAdditional;
        private decimal _CentralACAdditional;
        private int _NoOfAirpurifier;
        private decimal _AirpurifierRatePM;
        private decimal _TVRadioAmpHiFiStereoElectriGuitarValue;
        private decimal _TVRadioAmpHiFiStereoElectriGuitarRatePM;
        private int _NoOfTVs;
        private int _NoOfRadios;
        private int _NoOfAmplifiers;
        private int _NoOfHiFiStereos;
        private int _NoOfElectriGuitar;
        private decimal _ComputerAndOrganValue;
        private decimal _ComputerAndOrganRatePM;
        private int _NoOfComputers;
        private int _NoOfOrgans;
        private decimal _SwimmingPoolValue;
        private decimal _SwimmingPoolRatePM;
        private int _NoOfSwimmingPools;
        private decimal _PublicUtilities;
        private decimal _Telephone;
        private decimal _Pager;
        private decimal _Suitcase;
        private decimal _GolfBagAndAccessories;
        private decimal _Camera;
        private decimal _Servant;
        private decimal _Driver;
        private decimal _GardenerOrCompoundUpkeep;
        private decimal _OtherBenefitsInKindValue;
        private decimal _HotelAccommodationValue;
        private int _SelfWifeChildAbove20NoOfPersons;
        private int _SelfWifeChildAbove20NoOfDays;
        private decimal _SelfWifeChildAbove20Value;
        private decimal _SelfWifeChildAbove20RatePM;
        private int _ChildBetween8And20NoOfPersons;
        private int _ChildBetween8And20NoOfDays;
        private decimal _ChildBetween8And20Value;
        private decimal _ChildBetween8And20RatePM;
        private int _ChildBetween3And7NoOfPersons;
        private decimal _ChildBetween3And7NoOfDays;
        private decimal _ChildBetween3And7Value;
        private decimal _ChildBetween3And7RatePM;
        private int _ChildBelow3NoOfPersons;
        private int _ChildBelow3NoOfDays;
        private decimal _ChildBelow3Value;
        private decimal _ChildBelow3RatePM;
        private decimal _Percent2OfBasic;
        private decimal _CostOfLeavePassageAndIncidentalBenefits;
        private int _NoOfLeavePassageSelf;
        private int _NoOfLeavePassageWife;
        private decimal _NoOfLeavePassageChildren;
        private bool? _OHQStatus;
        private decimal _InterestPaidByEmployer;
        private decimal _LifeInsurancePremiumsPaidByEmployer;
        private decimal _FreeOrSubsidisedHoliday;
        private decimal _EducationalExpenses;
        private decimal _NonMonetaryAwardsForLongService;
        private decimal _EntranceOrTransferFeesToSocialClubs;
        private decimal _GainsFromAssets;
        private decimal _FullCostOfMotorVehicle;
        private decimal _CarBenefit;
        private decimal _OthersBenefits;
        private decimal _TotalBenefitsInKind;
        private decimal _NoOfEmployeesSharingQRS;
        private string _Filler;
        private string _Remarks;




        public virtual int Id { get { return this._Id; } set { this._Id = value; } }
        public virtual int emp_id { get { return this._emp_id; } set { this._emp_id = value; } }
        public virtual int AppendixA_year { get { return this._AppendixA_year; } set { this._AppendixA_year = value; } }
        public virtual string RecordType { get { return this._RecordType; } set { this._RecordType = value; } }
        public virtual string IDType { get { return this._IDType; } set { this._IDType = value; } }
        public virtual string IDNo { get { return this._IDNo; } set { this._IDNo = value; } }
        public virtual string NameLine1 { get { return this._NameLine1; } set { this._NameLine1 = value; } }
        public virtual string NameLine2 { get { return this._NameLine2; } set { this._NameLine2 = value; } }
        public virtual decimal ResidencePlaceValue { get { return this._ResidencePlaceValue; } set { this._ResidencePlaceValue = value; } }
        public virtual string ResidenceAddressLine1 { get { return this._ResidenceAddressLine1; } set { this._ResidenceAddressLine1 = value; } }
        public virtual string ResidenceAddressLine2 { get { return this._ResidenceAddressLine2; } set { this._ResidenceAddressLine2 = value; } }
        public virtual string ResidenceAddressLine3 { get { return this._ResidenceAddressLine3; } set { this._ResidenceAddressLine3 = value; } }
        public virtual string OccupationFromDate { get { return this._OccupationFromDate; } set { this._OccupationFromDate = value; } }
        public virtual string OccupationToDate { get { return this._OccupationToDate; } set { this._OccupationToDate = value; } }
        public virtual int NoOfDays { get { return this._NoOfDays; } set { this._NoOfDays = value; } }
        public virtual decimal AVOrRentByEmployer { get { return this._AVOrRentByEmployer; } set { this._AVOrRentByEmployer = value; } }
        public virtual int NoOfEmployeeSharing { get { return this._NoOfEmployeeSharing; } set { this._NoOfEmployeeSharing = value; } }
        public virtual decimal RentByEmployee { get { return this._RentByEmployee; } set { this._RentByEmployee = value; } }
        public virtual decimal FurnitureValue { get { return this._FurnitureValue; } set { this._FurnitureValue = value; } }
        public virtual decimal HardOrsoftFurnitureItemsRatePM { get { return this._HardOrsoftFurnitureItemsRatePM; } set { this._HardOrsoftFurnitureItemsRatePM = value; } }
        public virtual decimal HardOrsoftFurnitureItemsValue { get { return this._HardOrsoftFurnitureItemsValue; } set { this._HardOrsoftFurnitureItemsValue = value; } }
        public virtual decimal RefrigeratorValue { get { return this._RefrigeratorValue; } set { this._RefrigeratorValue = value; } }
        public virtual decimal RefrigeratorRatePM { get { return this._RefrigeratorRatePM; } set { this._RefrigeratorRatePM = value; } }
        public virtual int NoOfRefrigerators { get { return this._NoOfRefrigerators; } set { this._NoOfRefrigerators = value; } }
        public virtual decimal VideoRecorderValue { get { return this._VideoRecorderValue; } set { this._VideoRecorderValue = value; } }
        public virtual decimal VideoRecorderRatePM { get { return this._VideoRecorderRatePM; } set { this._VideoRecorderRatePM = value; } }
        public virtual int NoOfVideoRecorders { get { return this._NoOfVideoRecorders; } set { this._NoOfVideoRecorders = value; } }
        public virtual decimal WashingMachineDryerDishWasherValue { get { return this._WashingMachineDryerDishWasherValue; } set { this._WashingMachineDryerDishWasherValue = value; } }
        public virtual decimal WashingMachineDryerDishWasherRatePM { get { return this._WashingMachineDryerDishWasherRatePM; } set { this._WashingMachineDryerDishWasherRatePM = value; } }
        public virtual int NoOfWashingMachines { get { return this._NoOfWashingMachines; } set { this._NoOfWashingMachines = value; } }
        public virtual int NoOfDryers { get { return this._NoOfDryers; } set { this._NoOfDryers = value; } }
        public virtual int NoOfDishWashers { get { return this._NoOfDishWashers; } set { this._NoOfDishWashers = value; } }
        public virtual decimal AirConditionerValue { get { return this._AirConditionerValue; } set { this._AirConditionerValue = value; } }
        public virtual int NoOfAirConditioners { get { return this._NoOfAirConditioners; } set { this._NoOfAirConditioners = value; } }
        public virtual decimal AirConditionersUnitRatePM { get { return this._AirConditionersUnitRatePM; } set { this._AirConditionersUnitRatePM = value; } }
        public virtual int NoOfCentralACDining { get { return this._NoOfCentralACDining; } set { this._NoOfCentralACDining = value; } }
        public virtual decimal CentralACDining { get { return this._CentralACDining; } set { this._CentralACDining = value; } }
        public virtual int NoOfCentralACSitting { get { return this._NoOfCentralACSitting; } set { this._NoOfCentralACSitting = value; } }
        public virtual decimal CentralACSitting { get { return this._CentralACSitting; } set { this._CentralACSitting = value; } }
        public virtual int NoOfCentralACAdditional { get { return this._NoOfCentralACAdditional; } set { this._NoOfCentralACAdditional = value; } }
        public virtual decimal CentralACAdditional { get { return this._CentralACAdditional; } set { this._CentralACAdditional = value; } }
        public virtual int NoOfAirpurifier { get { return this._NoOfAirpurifier; } set { this._NoOfAirpurifier = value; } }
        public virtual decimal AirpurifierRatePM { get { return this._AirpurifierRatePM; } set { this._AirpurifierRatePM = value; } }
        public virtual decimal TVRadioAmpHiFiStereoElectriGuitarValue { get { return this._TVRadioAmpHiFiStereoElectriGuitarValue; } set { this._TVRadioAmpHiFiStereoElectriGuitarValue = value; } }
        public virtual decimal TVRadioAmpHiFiStereoElectriGuitarRatePM { get { return this._TVRadioAmpHiFiStereoElectriGuitarRatePM; } set { this._TVRadioAmpHiFiStereoElectriGuitarRatePM = value; } }
        public virtual int NoOfTVs { get { return this._NoOfTVs; } set { this._NoOfTVs = value; } }
        public virtual int NoOfRadios { get { return this._NoOfRadios; } set { this._NoOfRadios = value; } }
        public virtual int NoOfAmplifiers { get { return this._NoOfAmplifiers; } set { this._NoOfAmplifiers = value; } }
        public virtual int NoOfHiFiStereos { get { return this._NoOfHiFiStereos; } set { this._NoOfHiFiStereos = value; } }
        public virtual int NoOfElectriGuitar { get { return this._NoOfElectriGuitar; } set { this._NoOfElectriGuitar = value; } }
        public virtual decimal ComputerAndOrganValue { get { return this._ComputerAndOrganValue; } set { this._ComputerAndOrganValue = value; } }
        public virtual decimal ComputerAndOrganRatePM { get { return this._ComputerAndOrganRatePM; } set { this._ComputerAndOrganRatePM = value; } }
        public virtual int NoOfComputers { get { return this._NoOfComputers; } set { this._NoOfComputers = value; } }
        public virtual int NoOfOrgans { get { return this._NoOfOrgans; } set { this._NoOfOrgans = value; } }
        public virtual decimal SwimmingPoolValue { get { return this._SwimmingPoolValue; } set { this._SwimmingPoolValue = value; } }
        public virtual decimal SwimmingPoolRatePM { get { return this._SwimmingPoolRatePM; } set { this._SwimmingPoolRatePM = value; } }
        public virtual int NoOfSwimmingPools { get { return this._NoOfSwimmingPools; } set { this._NoOfSwimmingPools = value; } }
        public virtual decimal PublicUtilities { get { return this._PublicUtilities; } set { this._PublicUtilities = value; } }
        public virtual decimal Telephone { get { return this._Telephone; } set { this._Telephone = value; } }
        public virtual decimal Pager { get { return this._Pager; } set { this._Pager = value; } }
        public virtual decimal Suitcase { get { return this._Suitcase; } set { this._Suitcase = value; } }
        public virtual decimal GolfBagAndAccessories { get { return this._GolfBagAndAccessories; } set { this._GolfBagAndAccessories = value; } }
        public virtual decimal Camera { get { return this._Camera; } set { this._Camera = value; } }
        public virtual decimal Servant { get { return this._Servant; } set { this._Servant = value; } }
    }
}

