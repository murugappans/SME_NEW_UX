using System;
using System.Collections.Generic;

using System.Web;
using System.Xml;
using System.Xml.Serialization;
namespace IRAS.Appendix_A
{
    public partial class A8AST
    {





        private decimal ValueFurnitureFittingField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal ValueFurnitureFitting
        {
            get
            {
                return this.ValueFurnitureFittingField;
            }
            set
            {
                this.ValueFurnitureFittingField = value;
            }
        }

        private string ValueFurnitureFittingIndField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual string ValueFurnitureFittingInd
        {
            get
            {
                return this.ValueFurnitureFittingIndField;
            }
            set
            {
                this.ValueFurnitureFittingIndField = value;
            }
        }






        private decimal TotalRentPaidByEmployeePlaceOfResidenceField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TotalRentPaidByEmployeePlaceOfResidence
        {
            get
            {
                return this.TotalRentPaidByEmployeePlaceOfResidenceField;
            }
            set
            {
                this.TotalRentPaidByEmployeePlaceOfResidenceField = value;
            }
        }

        private decimal TotalTaxableValuePlaceOfResidenceField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TotalTaxableValuePlaceOfResidence
        {
            get
            {
                return (this.TaxableValuePlaceOfResidence - this.TotalRentPaidByEmployeePlaceOfResidence);
            }
            set
            {
                this.TotalTaxableValuePlaceOfResidenceField = value;
            }
        }



        private decimal AVOfPremisesField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal AVOfPremises
        {
            get
            {
                return this.AVOfPremisesField;
            }
            set
            {
                this.AVOfPremisesField = value;
            }
        }



        private decimal TaxableValuePlaceOfResidenceField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TaxableValuePlaceOfResidence
        {
            get
            {
                return this.TaxableValuePlaceOfResidenceField;
            }
            set
            {
                this.TaxableValuePlaceOfResidenceField = value;
            }
        }

        private decimal RentPaidToLandlordField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal RentPaidToLandlord
        {
            get
            {
                return this.RentPaidToLandlordField;
            }
            set
            {
                this.RentPaidToLandlordField = value;
            }
        }

        private decimal TaxableValueUtilitiesHouseKeepingField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TaxableValueUtilitiesHouseKeeping
        {
            get
            {
                return this.UtilitiesTelPagerSuitCaseAccessories + this.Driver + this.ServantGardener;
            }
            set
            {
                this.TaxableValueUtilitiesHouseKeepingField = value;
            }
        }

       


        private decimal ActualHotelAccommodationField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal ActualHotelAccommodation 
        {
            get
            {
                return this.ActualHotelAccommodationField;
            }
            set
            {
                this.ActualHotelAccommodationField = value;
            }
        }

        //private decimal RentPaidToLandlordField;

        //[XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal RentPaidToLandlord 
        //{
        //    get
        //    {
        //        return this.RentPaidToLandlordField;
        //    }
        //    set
        //    {
        //        this.RentPaidToLandlordField = value;
        //    }
        //}


        private decimal AmountPaidByEmployeeField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal AmountPaidByEmployee
        {
            get
            {
                return this.AmountPaidByEmployeeField;
            }
            set
            {
                this.AmountPaidByEmployeeField = value;
            }
        }

         private decimal TaxableValueHotelAccommodationField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TaxableValueHotelAccommodation
        {
            get
            {
                return this.ActualHotelAccommodation - AmountPaidByEmployee;
            }
            set
            {
                this.TaxableValueHotelAccommodationField = value;
            }
        }

       


        private bool _c2bc1;
        private bool _c2bc2;
        private bool _c2cc1;
        private bool _c2cc2;
        private bool _c2cc3;

        private bool _c2dc1;
        private bool _c2dc2;
        private bool _c2dc3;
        private bool _c2dc4;
        private bool _c2dc5;

        private bool _c2ec1;
        private bool _c2ec2;
        private bool _c2ec3;
        private bool _c2ec4;
        private bool _c2ec5;

        private bool _c2hc1;
        private bool _c2hc2;
        private bool _c2hc3;
        private bool _c2hc4;
        private bool _c2hc5;
        private bool _c2hc6;
        private bool _c2hc7;

        private bool _c3ac1;
        private bool _c3ac2;
        private bool _c3ac3;
        private bool _c2fc1;
        private bool _c2fc2;
        private bool _IS_AMENDMENT;


        public virtual bool IS_AMENDMENT
        {
            get { return this._IS_AMENDMENT; }
            set { this._IS_AMENDMENT = value; }
        }


        public virtual bool C2fc1
        {
            get { return this._c2fc1; }
            set { this._c2fc1 = value; }
        }

        public virtual bool C2fc2
        {
            get { return this._c2fc2; }
            set { this._c2fc2 = value; }
        }

        public virtual bool C2bc1
        {
            get { return this._c2bc1; }
            set { this._c2bc1 = value; }
        }
        public virtual bool C2bc2
        {
            get { return this._c2bc2; }
            set { this._c2bc2 = value; }
        }

        public virtual bool C2cc1
        {
            get { return this._c2cc1; }
            set { this._c2cc1 = value; }
        }
        public virtual bool C2cc2
        {
            get { return this._c2cc2; }
            set { this._c2cc2 = value; }
        }
        public virtual bool C2cc3
        {
            get { return this._c2cc3; }
            set { this._c2cc3 = value; }
        }



        public virtual bool C2dc1
        {
            get { return this._c2dc1; }
            set { this._c2dc1 = value; }
        }
        public virtual bool C2dc2
        {
            get { return this._c2dc2; }
            set { this._c2dc2 = value; }
        }
        public virtual bool C2dc3
        {
            get { return this._c2dc3; }
            set { this._c2dc3 = value; }
        }
        public virtual bool C2dc4
        {
            get { return this._c2dc4; }
            set { this._c2dc4 = value; }
        }
        public virtual bool C2dc5
        {
            get { return this._c2dc5; }
            set { this._c2dc5 = value; }
        }



        public virtual bool C2ec1
        {
            get { return this._c2ec1; }
            set { this._c2ec1 = value; }
        }
        public virtual bool C2ec2
        {
            get { return this._c2ec2; }
            set { this._c2ec2 = value; }
        }
        public virtual bool C2ec3
        {
            get { return this._c2ec3; }
            set { this._c2ec3 = value; }
        }
        public virtual bool C2ec4
        {
            get { return this._c2ec4; }
            set { this._c2ec4 = value; }
        }
        public virtual bool C2ec5
        {
            get { return this._c2ec5; }
            set { this._c2ec5 = value; }
        }


        public virtual bool C2hc1
        {
            get { return this._c2hc1; }
            set { this._c2hc1 = value; }
        }
        public virtual bool C2hc2
        {
            get { return this._c2hc2; }
            set { this._c2hc2 = value; }
        }
        public virtual bool C2hc3
        {
            get { return this._c2hc3; }
            set { this._c2hc3 = value; }
        }
        public virtual bool C2hc4
        {
            get { return this._c2hc4; }
            set { this._c2hc4 = value; }
        }
        public virtual bool C2hc5
        {
            get { return this._c2hc5; }
            set { this._c2hc5 = value; }
        }
        public virtual bool C2hc6
        {
            get { return this._c2hc6; }
            set { this._c2hc6 = value; }
        }
        public virtual bool C2hc7
        {
            get { return this._c2hc7; }
            set { this._c2hc7 = value; }
        }

        public virtual bool C3ac1
        {
            get { return this._c3ac1; }
            set { this._c3ac1 = value; }
        }
        public virtual bool C3ac2
        {
            get { return this._c3ac2; }
            set { this._c3ac2 = value; }
        }
        public virtual bool C3ac3
        {
            get { return this._c3ac3; }
            set { this._c3ac3 = value; }
        }





















        private int _Id;
        private string recordTypeField;
        private int _emp_id;

        private int selfWifeChildAbove20NoOfPersons_20aboveField;

        private int selfWifeChildAbove20NoOfPersons_SpouseField;

        private int selfWifeChildAbove20NoOfDays_20aboveField;

        private int selfWifeChildAbove20NoOfDays_SpouseField;













        private int _AppendixA_year;

        private string iDTypeField;
        

        private string iDNoField;

        private string nameLine1Field;

        private string nameLine2Field;

        private decimal residencePlaceValueField;

        private string residenceAddressLine1Field;

        private string residenceAddressLine2Field;

        private string residenceAddressLine3Field;

        private string occupationFromDateField;

        private string occupationToDateField;

        private int  noOfDaysField;
        private int noofsharing;
        private decimal aVOrRentByEmployerField;

        private decimal rentByEmployeeField;

        private decimal furnitureValueField;

        private decimal hardOrsoftFurnitureItemsValueField;

        private decimal refrigeratorValueField;

        private int noOfRefrigeratorsField;

        private decimal videoRecorderValueField;

        private int noOfVideoRecordersField;

        private decimal washingMachineDryerDishWasherValueField;

        private int noOfWashingMachinesField;

        private int noOfDryersField;

        private int noOfDishWashersField;

        private decimal airConditionerValueField;

        private int noOfAirConditionersField;

        private int noOfCentralACDiningField;

        private int noOfAirpurifieFiled;//

        private int noOfCentralACSittingField;

        private int noOfCentralACAdditionalField;

        private decimal tVRadioAmpHiFiStereoElectriGuitarValueField;

        private int noOfTVsField;

        private int noOfRadiosField;

        private int noOfAmplifiersField;

        private int noOfHiFiStereosField;

        private int noOfElectriGuitarField;

        private decimal computerAndOrganValueField;

        private int noOfComputersField;

        private int noOfOrgansField;

        private decimal swimmingPoolValueField;

        private int noOfSwimmingPoolsField;

        private decimal UtilitiesTelPagerSuitCaseAccessoriesField;

        private decimal telephoneField;

        private decimal pagerField;

        private decimal suitcaseField;

        private decimal golfBagAndAccessoriesField;

        private decimal cameraField;

        private decimal ServantGardenerField;

        private decimal driverField;

        private decimal gardenerOrCompoundUpkeepField;

        private decimal otherBenefitsInKindValueField;

        private decimal hotelAccommodationValueField;

        private int selfWifeChildAbove20NoOfPersonsField;

        private int selfWifeChildAbove20NoOfDaysField;

        private decimal selfWifeChildAbove20ValueField;

        private int childBetween8And20NoOfPersonsField;

        private decimal childBetween8And20NoOfDaysField;

        private decimal childBetween8And20ValueField;

        private decimal childBetween3And7NoOfPersonsField;

        private decimal childBetween3And7NoOfDaysField;

        private decimal childBetween3And7ValueField;

        private decimal childBelow3NoOfPersonsField;

        private decimal childBelow3NoOfDaysField;

        private decimal childBelow3ValueField;

        private decimal percent2OfBasicField;

        private decimal costOfLeavePassageAndIncidentalBenefitsField;

        private int noOfLeavePassageSelfField;

        private int NoOfLeavePassageSpouseField;

        private decimal noOfLeavePassageChildrenField;

        private bool oHQStatusField;

        private decimal interestPaidByEmployerField;

        private decimal lifeInsurancePremiumsPaidByEmployerField;

        private decimal freeOrSubsidisedHolidayField;

        private decimal educationalExpensesField;

        private decimal nonMonetaryAwardsForLongServiceField;

        private decimal entranceOrTransferFeesToSocialClubsField;

        private decimal gainsFromAssetsField;

        private decimal fullCostOfMotorVehicleField;

        private decimal carBenefitField;

        private decimal othersBenefitsField;

        private decimal totalBenefitsInKindField;

        private int noOfEmployeesSharingQRSField;

        private int noOfSurvellanceField;

        private string fillerField;

        private string remarksField;

        private string DateOfIncorporationField;
         public virtual string DateOfIncorporation
        {
            get
            {
                return this.DateOfIncorporationField;
            }
            set
            {
                this.DateOfIncorporationField = value;
            }
        }

        private int noOfHardSoftFunnitureFiled;




        private decimal _HardOrsoftFurnitureItemsRatePM;
        private decimal _RefrigeratorRatePM;
        private decimal _VideoRecorderRatePM;
        private decimal _WashingMachineDryerDishWasherRatePM;
        private decimal _AirConditionersUnitRatePM;
        private decimal _AirpurifierRatePM;
        private decimal _TVRadioAmpHiFiStereoElectriGuitarRatePM;
        private decimal _ComputerAndOrganRatePM;
        private decimal _SwimmingPoolRatePM;
        private decimal _SelfWifeChildAbove20RatePM;
        private decimal _ChildBetween8And20RatePM;
        private decimal _ChildBetween3And7RatePM;
        private decimal _ChildBelow3RatePM;
       
        public A8AST()
        {
              
        }

     
        [XmlIgnore]
        public virtual int SelfWifeChildAbove20NoOfPersons_20above
        {
            get { return this.selfWifeChildAbove20NoOfPersons_20aboveField; }
            set { this.selfWifeChildAbove20NoOfPersons_20aboveField = value; }
        }

        [XmlIgnore]
        public virtual int SelfWifeChildAbove20NoOfPersons_Spouse
        {
            get { return this.selfWifeChildAbove20NoOfPersons_SpouseField; }
            set { this.selfWifeChildAbove20NoOfPersons_SpouseField = value; }
        }

        [XmlIgnore]
        public virtual int SelfWifeChildAbove20NoOfDays_20above
        {
            get { return this.selfWifeChildAbove20NoOfDays_20aboveField; }
            set { this.selfWifeChildAbove20NoOfDays_20aboveField = value; }
        }

        [XmlIgnore]
        public virtual int SelfWifeChildAbove20NoOfDays_Spouse
        {
            get { return this.selfWifeChildAbove20NoOfDays_SpouseField; }
            set { this.selfWifeChildAbove20NoOfDays_SpouseField = value; }
        }







        [XmlIgnore]
        public virtual int NoOfHardSoftFunniture
        {
            get { return this.noOfHardSoftFunnitureFiled; }
            set { this.noOfHardSoftFunnitureFiled = value; }
        }
        [XmlIgnore]
        public virtual int NoOfSurvellance
        {
            get { return this.noOfSurvellanceField; }
            set { this.noOfSurvellanceField = value; }
        }


        [XmlIgnore]
        public virtual int NoOfAirpurifier
        {
            get { return this.noOfAirpurifieFiled; }
            set { this.noOfAirpurifieFiled = value; }
        }



        [XmlIgnore]
        public virtual decimal SelfWifeChildAbove20RatePM
        {

            get { return this._SelfWifeChildAbove20RatePM; }
            set { this._SelfWifeChildAbove20RatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal ChildBetween8And20RatePM
        {

            get { return this._ChildBetween8And20RatePM; }
            set { this._ChildBetween8And20RatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal ChildBetween3And7RatePM
        {

            get { return this._ChildBetween3And7RatePM; }
            set { this._ChildBetween3And7RatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal ChildBelow3RatePM
        {

            get { return this._ChildBelow3RatePM; }
            set { this._ChildBelow3RatePM = value; }
        }
        [XmlIgnore]
        public virtual int NoOfEmployeeSharing
        {
            get { return this.noofsharing; }
            set { this.noofsharing = value; }
        }







        //private decimal Calculatefurniturevalue(int noofunit, decimal rate_month, int noof_sharing, int noofdays)
        //{
        //    return Math.Round(((noofunit * rate_month * 12 * noofdays) / 365), 2); ;
        //}

        [XmlIgnore]
        public virtual  int emp_id
        {
            get{ return this._emp_id;}
            set { this._emp_id=value; }
        }
        [XmlIgnore]
        public virtual decimal HardOrsoftFurnitureItemsRatePM 
        {

            get { return this._HardOrsoftFurnitureItemsRatePM; } 
            set { this._HardOrsoftFurnitureItemsRatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal VideoRecorderRatePM 
        {
            get { return this._VideoRecorderRatePM; } 
            set { 
                this._VideoRecorderRatePM = value; } 
        }
        [XmlIgnore]
        public virtual decimal WashingMachineDryerDishWasherRatePM 
        {
            get { return this._WashingMachineDryerDishWasherRatePM; }
            set { this._WashingMachineDryerDishWasherRatePM = value; } 
        }
        [XmlIgnore]
        public virtual decimal AirConditionersUnitRatePM 
        { 
            get { return this._AirConditionersUnitRatePM; } 
            set { this._AirConditionersUnitRatePM = value; } 
        }
        [XmlIgnore]
        public virtual decimal AirpurifierRatePM 
        { get { return this._AirpurifierRatePM; } 
            set { this._AirpurifierRatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal ComputerAndOrganRatePM 
        { get { return this._ComputerAndOrganRatePM; } 
            set { this._ComputerAndOrganRatePM = value; }
        }
        [XmlIgnore]
        public virtual decimal SwimmingPoolRatePM 
        { get { return this._SwimmingPoolRatePM; } 
            set { this._SwimmingPoolRatePM = value; } 
        }
        [XmlIgnore]
        public virtual decimal TVRadioAmpHiFiStereoElectriGuitarRatePM 
        { get { return this._TVRadioAmpHiFiStereoElectriGuitarRatePM; }
            set { this._TVRadioAmpHiFiStereoElectriGuitarRatePM = value; } 
        }
        [XmlIgnore]
        public virtual decimal RefrigeratorRatePM 
        { 
            get { return this._RefrigeratorRatePM; } 
            set { this._RefrigeratorRatePM = value; } 
        }
        public virtual int AppendixA_year
        {
            get { return this._AppendixA_year; }
            set { this._AppendixA_year = value; }
        }
        [XmlIgnore]
         public virtual  int Id
        {
            get{ return this._Id;} 
            set { this._Id =value; }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual string RecordType
        {
            get
            {
                return this.recordTypeField;
            }
            set
            {
                this.recordTypeField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string IDType
        {
            get
            {
                return this.iDTypeField;
            }
            set
            {
                this.iDTypeField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string IDNo
        {
            get
            {
                return this.iDNoField;
            }
            set
            {
                this.iDNoField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string NameLine1
        {
            get
            {
                return this.nameLine1Field;
            }
            set
            {
                this.nameLine1Field = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string NameLine2
        {
            get
            {
                return this.nameLine2Field;
            }
            set
            {
                this.nameLine2Field = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
       public virtual decimal ResidencePlaceValue
        {
            get
            {
                return this.residencePlaceValueField;
            }
            set
            {
                this.residencePlaceValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string ResidenceAddressLine1
        {
            get
            {
                return this.residenceAddressLine1Field;
            }
            set
            {
                this.residenceAddressLine1Field = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string ResidenceAddressLine2
        {
            get
            {
                return this.residenceAddressLine2Field;
            }
            set
            {
                this.residenceAddressLine2Field = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string ResidenceAddressLine3
        {
            get
            {
                return this.residenceAddressLine3Field;
            }
            set
            {
                this.residenceAddressLine3Field = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string OccupationFromDate
        {
            get
            {
                return this.occupationFromDateField;
            }
            set
            {
                this.occupationFromDateField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string OccupationToDate
        {
            get
            {
                return this.occupationToDateField;
            }
            set
            {
                this.occupationToDateField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int NoOfDays
        {
            get
            {
               

                 TimeSpan difference =  Convert.ToDateTime(this.OccupationToDate)-Convert.ToDateTime(this.OccupationFromDate); 

                 return difference.Days+1;
            }
            set
            {
                this.noOfDaysField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal AVOrRentByEmployer
        {
            get
            {
                return this.aVOrRentByEmployerField;
            }
            set
            {
                this.aVOrRentByEmployerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal RentByEmployee
        {
            get
            {
                return this.rentByEmployeeField;
            }
            set
            {
                this.rentByEmployeeField = value;
            }
        }
        //[XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        //public virtual decimal FurnitureValue
        //{
        //    get
        //    {
        //        //decimal total = Math.Round((HardOrsoftFurnitureItemsValue + RefrigeratorValue + this.VideoRecorderValue+ WashingMachineDryerDishWasherValue
        //        //+ AirConditionerValue + this.TVRadioAmpHiFiStereoElectriGuitarValue + this.ComputerAndOrganValue + this.SwimmingPoolValue +
        //        //UtilitiesTelPagerSuitCaseAccessories + Telephone + Pager + Suitcase + GolfBagAndAccessories + Camera + Servant + Driver + GardenerOrCompoundUpkeep + OtherBenefitsInKindValue), 2);

        //        //return total;
        //        return FurnitureValue;
        //    }
        //    set
        //    {
        //        this.furnitureValueField = value;
        //    }
        //}
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
       
        public virtual decimal HardOrsoftFurnitureItemsValue
        {
            get
            {


               // return this.Calculatefurniturevalue(this.NoOfHardSoftFunniture, this.HardOrsoftFurnitureItemsRatePM, this.noofsharing, this.NoOfDays);
                return hardOrsoftFurnitureItemsValueField;
            
            }
            set
            {
                this.hardOrsoftFurnitureItemsValueField = value;
            }
        }
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal RefrigeratorValue
        //{
        //    get
        //    {
        //  return this.Calculatefurniturevalue(this.NoOfRefrigerators , this.RefrigeratorRatePM, this.noofsharing, this.NoOfDays);
        //    }
        //    set
        //    {
        //        this.refrigeratorValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfRefrigerators
        //{
        //    get
        //    {
        //        return this.noOfRefrigeratorsField;
        //    }
        //    set
        //    {
        //        this.noOfRefrigeratorsField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal VideoRecorderValue
        //{
        //    get
        //    {
        //        return this.Calculatefurniturevalue(this.NoOfVideoRecorders, this.VideoRecorderRatePM, this.noofsharing, this.NoOfDays);
        //    }
        //    set
        //    {
        //        this.videoRecorderValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfVideoRecorders
        //{
        //    get
        //    {
        //        return this.noOfVideoRecordersField;
        //    }
        //    set
        //    {
        //        this.noOfVideoRecordersField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal WashingMachineDryerDishWasherValue
        //{
        //    get
        //        //ndo washvalue per month
        //    {   decimal wash_value= this.Calculatefurniturevalue(this.NoOfWashingMachines+this.NoOfDryers+this.NoOfDishWashers, this.WashingMachineDryerDishWasherRatePM, this.noofsharing, this.NoOfDays);
               
            
                
                
        //        return wash_value;
        //    }
        //    set
        //    {
        //        this.washingMachineDryerDishWasherValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfWashingMachines
        //{
        //    get
        //    {
        //        return this.noOfWashingMachinesField;
        //    }
        //    set
        //    {
        //        this.noOfWashingMachinesField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfDryers
        //{
        //    get
        //    {
        //        return this.noOfDryersField;
        //    }
        //    set
        //    {
        //        this.noOfDryersField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfDishWashers
        //{
        //    get
        //    {
        //        return this.noOfDishWashersField;
        //    }
        //    set
        //    {
        //        this.noOfDishWashersField = value;
        //    }
        //}
        ////ndo :change dyanmic
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal AirConditionerValue
        //{
        //    get
        //    {   decimal  total_value =  this.Calculatefurniturevalue(this.NoOfAirConditioners, this.AirConditionersUnitRatePM, this.noofsharing, this.NoOfDays);
        //                 total_value += this.Calculatefurniturevalue(this.NoOfCentralACDining, 15.00m, this.noofsharing, this.NoOfDays);
        //                 total_value += this.Calculatefurniturevalue(this.NoOfCentralACSitting,15.00m, this.noofsharing, this.NoOfDays);
        //                 total_value += this.Calculatefurniturevalue(this.NoOfCentralACAdditional, 10.00m, this.noofsharing, this.NoOfDays);
        //                total_value += this.Calculatefurniturevalue(this.NoOfAirpurifier, this.AirpurifierRatePM, this.noofsharing, this.NoOfDays);


        //                 return total_value;
        //    }
        //    set
        //    {
        //        this.airConditionerValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfAirConditioners
        //{
        //    get
        //    {
        //        return this.noOfAirConditionersField;
        //    }
        //    set
        //    {
        //        this.noOfAirConditionersField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfCentralACDining
        //{
        //    get
        //    {
        //        return this.noOfCentralACDiningField;
        //    }
        //    set
        //    {
        //        this.noOfCentralACDiningField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfCentralACSitting
        //{
        //    get
        //    {
        //        return this.noOfCentralACSittingField;
        //    }
        //    set
        //    {
        //        this.noOfCentralACSittingField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfCentralACAdditional
        //{
        //    get
        //    {
        //        return this.noOfCentralACAdditionalField;
        //    }
        //    set
        //    {
        //        this.noOfCentralACAdditionalField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal TVRadioAmpHiFiStereoElectriGuitarValue
        //{
        //    get
        //    {


        //        return  this.Calculatefurniturevalue(this.NoOfTVs+this.NoOfRadios+this.NoOfHiFiStereos+this.NoOfElectriGuitar+this.NoOfSurvellance, this.TVRadioAmpHiFiStereoElectriGuitarRatePM, this.noofsharing, this.NoOfDays);
        //    }
        //    set
        //    {
        //        this.tVRadioAmpHiFiStereoElectriGuitarValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfTVs
        //{
        //    get
        //    {
        //        return this.noOfTVsField;
        //    }
        //    set
        //    {
        //        this.noOfTVsField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfRadios
        //{
        //    get
        //    {
        //        return this.noOfRadiosField;
        //    }
        //    set
        //    {
        //        this.noOfRadiosField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfAmplifiers
        //{
        //    get
        //    {
        //        return this.noOfAmplifiersField;
        //    }
        //    set
        //    {
        //        this.noOfAmplifiersField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfHiFiStereos
        //{
        //    get
        //    {
        //        return this.noOfHiFiStereosField;
        //    }
        //    set
        //    {
        //        this.noOfHiFiStereosField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfElectriGuitar
        //{
        //    get
        //    {
        //        return this.noOfElectriGuitarField;
        //    }
        //    set
        //    {
        //        this.noOfElectriGuitarField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal ComputerAndOrganValue
        //{
        //    get
        //    {
        //        return this.Calculatefurniturevalue(this.NoOfComputers+this.NoOfOrgans, this.ComputerAndOrganRatePM, this.noofsharing, this.NoOfDays);
        //    }
        //    set
        //    {
        //        this.computerAndOrganValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfComputers
        //{
        //    get
        //    {
        //        return this.noOfComputersField;
        //    }
        //    set
        //    {
        //        this.noOfComputersField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]

        //public virtual int NoOfOrgans
        //{
        //    get
        //    {
        //        return this.noOfOrgansField;
        //    }
        //    set
        //    {
        //        this.noOfOrgansField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal SwimmingPoolValue
        //{
        //    get
        //    {
        //        return  this.Calculatefurniturevalue(this.NoOfSwimmingPools, this.SwimmingPoolRatePM, this.noofsharing, this.NoOfDays);
        //    }
        //    set
        //    {
        //        this.swimmingPoolValueField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual int NoOfSwimmingPools
        //{
        //    get
        //    {
        //        return this.noOfSwimmingPoolsField;
        //    }
        //    set
        //    {
        //        this.noOfSwimmingPoolsField = value;
        //    }
        //}
        // [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal UtilitiesTelPagerSuitCaseAccessories
        {
            get
            {
                return this.UtilitiesTelPagerSuitCaseAccessoriesField;
            }
            set
            {
                this.UtilitiesTelPagerSuitCaseAccessoriesField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Pager
        {
            get
            {
                return this.pagerField;
            }
            set
            {
                this.pagerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Suitcase
        {
            get
            {
                return this.suitcaseField;
            }
            set
            {
                this.suitcaseField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal GolfBagAndAccessories
        {
            get
            {
                return this.golfBagAndAccessoriesField;
            }
            set
            {
                this.golfBagAndAccessoriesField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Camera
        {
            get
            {
                return this.cameraField;
            }
            set
            {
                this.cameraField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ServantGardener
        {
            get
            {
                return this.ServantGardenerField;
            }
            set
            {
                this.ServantGardenerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Driver
        {
            get
            {
                return this.driverField;
            }
            set
            {
                this.driverField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal GardenerOrCompoundUpkeep
        {
            get
            {
                return this.gardenerOrCompoundUpkeepField;
            }
            set
            {
                this.gardenerOrCompoundUpkeepField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal OtherBenefitsInKindValue
        {
            get
            {
                return this.otherBenefitsInKindValueField;
            }
            set
            {
                this.otherBenefitsInKindValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal HotelAccommodationValue
        {
            get
            {
                return Math.Round((this.SelfWifeChildAbove20Value + ChildBelow3Value + this.ChildBetween3And7Value + this.ChildBetween8And20Value+this.Percent2OfBasic), 2);
                                    
            }
            set
            {
                this.hotelAccommodationValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int SelfWifeChildAbove20NoOfPersons
        {
            get
            {
                return this.selfWifeChildAbove20NoOfPersonsField;
            }
            set
            {
                this.selfWifeChildAbove20NoOfPersonsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int SelfWifeChildAbove20NoOfDays
        {
            get
            {
                return this.selfWifeChildAbove20NoOfDaysField;
            }
            set
            {
                this.selfWifeChildAbove20NoOfDaysField = value;
            }
        }


        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal SelfWifeChildAbove20Value
        {
            get
            {
                int no_of_person=this.SelfWifeChildAbove20NoOfPersons+this.SelfWifeChildAbove20NoOfPersons_20above+this.SelfWifeChildAbove20NoOfPersons_Spouse;
                int no_of_days= this.SelfWifeChildAbove20NoOfDays+this.SelfWifeChildAbove20NoOfDays_20above+this.SelfWifeChildAbove20NoOfDays_Spouse;


                return  Math.Round((( no_of_person* SelfWifeChildAbove20RatePM *no_of_days *12)/365),2);
            }
            set
            {
                this.selfWifeChildAbove20ValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int ChildBetween8And20NoOfPersons
        {
            get
            {
                return this.childBetween8And20NoOfPersonsField;
            }
            set
            {
                this.childBetween8And20NoOfPersonsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBetween8And20NoOfDays
        {
            get
            {
                return this.childBetween8And20NoOfDaysField;
            }
            set
            {
                this.childBetween8And20NoOfDaysField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBetween8And20Value
        {
            get
            {
                return Math.Round(((this.ChildBetween8And20NoOfDays * this.ChildBetween8And20RatePM * ChildBetween8And20NoOfDays * 12) / 365), 2);
            }
            set
            {
                this.childBetween8And20ValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBetween3And7NoOfPersons
        {
            get
            {
                return this.childBetween3And7NoOfPersonsField;
            }
            set
            {
                this.childBetween3And7NoOfPersonsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBetween3And7NoOfDays
        {
            get
            {
                return this.childBetween3And7NoOfDaysField;
            }
            set
            {
                this.childBetween3And7NoOfDaysField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBetween3And7Value
        {
            get
            {
                return Math.Round(((this.ChildBetween3And7NoOfPersons * this.ChildBetween3And7RatePM * this.ChildBetween3And7NoOfDays* 12) / 365), 2);
            }
            set
            {
                this.childBetween3And7ValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBelow3NoOfPersons
        {
            get
            {
                return this.childBelow3NoOfPersonsField;
            }
            set
            {
                this.childBelow3NoOfPersonsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBelow3NoOfDays
        {
            get
            {
                return this.childBelow3NoOfDaysField;
            }
            set
            {
                this.childBelow3NoOfDaysField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal ChildBelow3Value
        {
            get
            {
                return Math.Round(((this.ChildBelow3NoOfDays * this.ChildBelow3NoOfPersons * this.ChildBelow3RatePM * 12) / 365), 2);
            }
            set
            {
                this.childBelow3ValueField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal Percent2OfBasic
        {
            get
            {
                return this.percent2OfBasicField;
            }
            set
            {
                this.percent2OfBasicField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal CostOfLeavePassageAndIncidentalBenefits
        {
            get
            {
                return this.costOfLeavePassageAndIncidentalBenefitsField;
            }
            set
            {
                this.costOfLeavePassageAndIncidentalBenefitsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int NoOfLeavePassageSelf
        {
            get
            {
                return this.noOfLeavePassageSelfField;
            }
            set
            {
                this.noOfLeavePassageSelfField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int NoOfLeavePassageSpouse
        {
            get
            {
                return this.NoOfLeavePassageSpouseField;
            }
            set
            {
                this.NoOfLeavePassageSpouseField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal NoOfLeavePassageChildren
        {
            get
            {
                return this.noOfLeavePassageChildrenField;
            }
            set
            {
                this.noOfLeavePassageChildrenField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual bool OHQStatus
        {
            get
            {
                return this.oHQStatusField;
            }
            set
            {
                this.oHQStatusField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal InterestPaidByEmployer
        {
            get
            {
                return this.interestPaidByEmployerField;
            }
            set
            {
                this.interestPaidByEmployerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal LifeInsurancePremiumsPaidByEmployer
        {
            get
            {
                return this.lifeInsurancePremiumsPaidByEmployerField;
            }
            set
            {
                this.lifeInsurancePremiumsPaidByEmployerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal FreeOrSubsidisedHoliday
        {
            get
            {
                return this.freeOrSubsidisedHolidayField;
            }
            set
            {
                this.freeOrSubsidisedHolidayField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal EducationalExpenses
        {
            get
            {
                return this.educationalExpensesField;
            }
            set
            {
                this.educationalExpensesField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal NonMonetaryAwardsForLongService
        {
            get
            {
                return this.nonMonetaryAwardsForLongServiceField;
            }
            set
            {
                this.nonMonetaryAwardsForLongServiceField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal EntranceOrTransferFeesToSocialClubs
        {
            get
            {
                return this.entranceOrTransferFeesToSocialClubsField;
            }
            set
            {
                this.entranceOrTransferFeesToSocialClubsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal GainsFromAssets
        {
            get
            {
                return this.gainsFromAssetsField;
            }
            set
            {
                this.gainsFromAssetsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal FullCostOfMotorVehicle
        {
            get
            {
                return this.fullCostOfMotorVehicleField;
            }
            set
            {
                this.fullCostOfMotorVehicleField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal CarBenefit
        {
            get
            {
                return this.carBenefitField;
            }
            set
            {
                this.carBenefitField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal OthersBenefits
        {
            get
            {
                return this.othersBenefitsField;
            }
            set
            {
                this.othersBenefitsField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual decimal TotalBenefitsInKind
        {
            get
            {
                return Math.Round((this.TotalTaxableValuePlaceOfResidence+this.TaxableValueUtilitiesHouseKeeping+this.TaxableValueHotelAccommodation+this.CostOfLeavePassageAndIncidentalBenefits
                 +this.InterestPaidByEmployer+this.LifeInsurancePremiumsPaidByEmployer+this.FreeOrSubsidisedHoliday+this.EducationalExpenses
                +this.NonMonetaryAwardsForLongService+this.EntranceOrTransferFeesToSocialClubs+this.GainsFromAssets+this.FullCostOfMotorVehicle
                +this.CarBenefit+this.OthersBenefits),2);

            }
            set
            {
                this.totalBenefitsInKindField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual int NoOfEmployeesSharingQRS
        {
            get
            {
                return this.noOfEmployeesSharingQRSField;
            }
            set
            {
                this.noOfEmployeesSharingQRSField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
        public virtual string Filler
        {
            get
            {
                return this.fillerField;
            }
            set
            {
                this.fillerField = value;
            }
        }
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A2015")]
      public virtual string Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }
    }
   

    public  enum IDType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3")]
        Item3,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        Item4,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("5")]
        Item5,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("6")]
        Item6,
    }

    public  enum BooleanOptionalType
    {

        /// <remarks/>
        Y,

        /// <remarks/>
        N,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("")]
        Item,
    }
}