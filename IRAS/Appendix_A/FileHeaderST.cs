using System;
using System.Collections.Generic;

using System.Web;
using System.Xml.Serialization;

namespace IRAS.Appendix_A
{
    public class FileHeaderST
    {
        private string recordTypeField;

        private string sourceField;

        private int basisYearField;

        private string paymentTypeField;

        private string  organizationIDField;

        private string organizationIDNoField;

        private string authorisedPersonNameField;

        private string authorisedPersonDesignationField;

        private string employerNameField;

        private string telephoneField;

        private string authorisedPersonEmailField;

        private string batchIndicatorField;

        private string batchDateField;

        private string incorporationDateField;

        private string divisionOrBranchNameField;

        private string addressofemployerfield;

        public FileHeaderST()
        {
            //this.recordTypeField = RecordIndHeaderType.Item0;
            //this.sourceField = SourceType.Item1;
            //this.paymentTypeField = "payment";
            //this.basisYearField = 2014;
            //this.organizationIDField = OrgIDType.A;
            //this.authorisedPersonNameField = "kumar";
            //this.batchIndicatorField = BatchType.A;
            //this.batchDateField = "2013";
            //this.authorisedPersonDesignationField = "test";
            //this.telephoneField = "test";
            //this.authorisedPersonEmailField = "test";
            //this.divisionOrBranchNameField = "test";
        }

        [XmlIgnore]
        public string AddressOf_Employer
        {
            get
            {
                return this.addressofemployerfield;
            }
            set
            {
                this.addressofemployerfield= value;
            }
        }

        public string RecordType
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

        public string Source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }

        public int BasisYear
        {
            get
            {
                return this.basisYearField;
            }
            set
            {
                this.basisYearField = value;
            }
        }

        public string PaymentType
        {
            get
            {
                return this.paymentTypeField;
            }
            set
            {
                this.paymentTypeField = value;
            }
        }

        public string OrganizationID
        {
            get
            {
                return this.organizationIDField;
            }
            set
            {
                this.organizationIDField = value;
            }
        }

        public string OrganizationIDNo
        {
            get
            {
                return this.organizationIDNoField;
            }
            set
            {
                this.organizationIDNoField = value;
            }
        }

        public string AuthorisedPersonName
        {
            get
            {
                return this.authorisedPersonNameField;
            }
            set
            {
                this.authorisedPersonNameField = value;
            }
        }

        public string AuthorisedPersonDesignation
        {
            get
            {
                return this.authorisedPersonDesignationField;
            }
            set
            {
                this.authorisedPersonDesignationField = value;
            }
        }

        public string EmployerName
        {
            get
            {
                return this.employerNameField;
            }
            set
            {
                this.employerNameField = value;
            }
        }

        public string Telephone
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

        public string AuthorisedPersonEmail
        {
            get
            {
                return this.authorisedPersonEmailField;
            }
            set
            {
                this.authorisedPersonEmailField = value;
            }
        }

        public string BatchIndicator
        {
            get
            {
                return this.batchIndicatorField;
            }
            set
            {
                this.batchIndicatorField = value;
            }
        }

        public string BatchDate
        {
            get
            {
                return this.batchDateField;
            }
            set
            {
                this.batchDateField = value;
            }
        }

        public string IncorporationDate
        {
            get
            {
                return this.incorporationDateField;
            }
            set
            {
                this.incorporationDateField = value;
            }
        }

        public string DivisionOrBranchName
        {
            get
            {
                return this.divisionOrBranchNameField;
            }
            set
            {
                this.divisionOrBranchNameField = value;
            }
        }
    
    }

    //public enum SourceType
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("1")]
    //    Item1,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("4")]
    //    Item4,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("5")]
    //    Item5,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("6")]
    //    Item6,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("9")]
    //    Item9,
    //}

    //public enum OrgIDType
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("1")]
    //    Item1,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("2")]
    //    Item2,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("7")]
    //    Item7,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("8")]
    //    Item8,

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("9")]
    //    Item9,

    //    /// <remarks/>
    //    A,

    //    /// <remarks/>
    //    I,

    //    /// <remarks/>
    //    U,
    //}

    //public enum BatchType
    //{

    //    /// <remarks/>
    //    O,

    //    /// <remarks/>
    //    A,
    //}
    //public enum RecordIndDetailType
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("1")]
    //    Item1,
    //}
    //public enum RecordIndHeaderType
    //{

    //    /// <remarks/>
    //    [System.Xml.Serialization.XmlEnumAttribute("0")]
    //    Item0,
    //}

}