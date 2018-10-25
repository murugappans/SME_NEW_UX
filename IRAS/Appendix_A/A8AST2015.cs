using System;
using System.Collections.Generic;

using System.Web;
using System.Xml;
using System.Xml.Serialization;
namespace IRAS.Appendix_A
{
    public partial class A8AST2015
    {

        
        private bool _IS_AMENDMENT;


        public virtual bool IS_AMENDMENT
        {
            get { return this._IS_AMENDMENT; }
            set { this._IS_AMENDMENT = value; }
        }


      

      


      


    




















        private int _Id;
        private string recordTypeField;
        private int _emp_id;











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

        private decimal TotalTaxableValuePlaceOfResidenceField;

        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TotalTaxableValuePlaceOfResidence
        {
            get
            {
                return this.TotalTaxableValuePlaceOfResidenceField;
            }
            set
            {
                this.TotalTaxableValuePlaceOfResidenceField = value;
            }
        }





        //private decimal TaxableValuePlaceOfResidenceField;

        //[XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        //public virtual decimal TaxableValuePlaceOfResidence
        //{
        //    get
        //    {
        //        return this.TaxableValuePlaceOfResidenceField;
        //    }
        //    set
        //    {
        //        this.TaxableValuePlaceOfResidenceField = value;
        //    }
        //}

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

     

       

       
        private decimal publicUtilitiesField;

       

        private decimal servantField;

        private decimal driverField;

        private decimal gardenerOrCompoundUpkeepField;

      
        private decimal hotelAccommodationValueField;

       
       

        private decimal costOfLeavePassageAndIncidentalBenefitsField;

        private int noOfLeavePassageSelfField;

        private int noOfLeavePassageWifeField;

        private int noOfLeavePassageChildrenField;

        private bool oHQStatusField;

        private decimal interestPaidByEmployerField;

        private decimal lifeInsurancePremiumsPaidByEmployerField;

        private decimal freeOrSubsidisedHolidayField;

        private decimal educationalExpensesField;

        private int nonMonetaryAwardsForLongServiceField;

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



        private int noOfHardSoftFunnitureFiled;




      
       
        public A8AST2015()
        {
            
            
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal FurnitureValue
        {
            get
            {
                return furnitureValueField;
            }
            set
            {
                this.furnitureValueField = value;
            }
        }
        
       
       
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal PublicUtilities
        {
            get
            {
                return this.publicUtilitiesField;
            }
            set
            {
                this.publicUtilitiesField = value;
            }
        }
      
        public virtual decimal Servant
        {
            get
            {
                return this.servantField;
            }
            set
            {
                this.servantField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         
        public virtual decimal HotelAccommodationValue
        {
            get
            {
                return hotelAccommodationValueField;    
            }
            set
            {
                this.hotelAccommodationValueField = value;
            }
        }
        
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual int NoOfLeavePassageWife
        {
            get
            {
                return this.noOfLeavePassageWifeField;
            }
            set
            {
                this.noOfLeavePassageWifeField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual int NoOfLeavePassageChildren
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual int NonMonetaryAwardsForLongService
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
        public virtual decimal TotalBenefitsInKind
        {
            get
            {
                return Math.Round((this.ResidencePlaceValue+this.FurnitureValue+this.HotelAccommodationValue+this.CostOfLeavePassageAndIncidentalBenefits
                 +this.InterestPaidByEmployer+this.LifeInsurancePremiumsPaidByEmployer+this.FreeOrSubsidisedHoliday+this.EducationalExpenses
                +this.NonMonetaryAwardsForLongService+this.EntranceOrTransferFeesToSocialClubs+this.GainsFromAssets+this.FullCostOfMotorVehicle
                +this.CarBenefit+this.OthersBenefits),2);

            }
            set
            {
                this.totalBenefitsInKindField = value;
            }
        }
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
         [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
        [XmlElement(Namespace = "http://www.iras.gov.sg/A8A")]
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
   

    //public  enum IDType
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("1")]
    //    Item1,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("2")]
    //    Item2,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("3")]
    //    Item3,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("4")]
    //    Item4,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("5")]
    //    Item5,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("6")]
    //    Item6,
    //}

    //public  enum BooleanOptionalType
    //{

    //    /// <remarks/>
    //    Y,

    //    /// <remarks/>
    //    N,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("")]
    //    Item,
    //}
}