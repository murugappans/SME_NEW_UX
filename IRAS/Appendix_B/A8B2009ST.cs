using System;
using System.Collections.Generic;
using System.Web;
using Iesi.Collections.Generic;
using IRAS.Appendix_A;
namespace IRAS.Appendix_B
{
    public class A8B2009ST
    {
        private int _ID;
        private string recordTypeField;

        private string iDTypeField;

        private string iDNoField;

        private string nameLine1Field;

        private string nameLine2Field;

        private string nationalityField;

        private string sexField;

        private string dateOfBirthField;

        private string _dateOfIncorporation;

      

        private List<A8BRECORDDETAILS> _A8BRECORDDETAILS;

        private FileHeaderST _Fileheader;

  



        public virtual string DateOfIncorporation
        {
            get
            {
                return this._dateOfIncorporation;
            }
            set
            {
                this._dateOfIncorporation = value;
            }
        }







        public virtual int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }
        public virtual List<A8BRECORDDETAILS> A8BRECORDDETAILS
        {
            get
            {
                return this._A8BRECORDDETAILS;

            }
            set
            {
                this._A8BRECORDDETAILS = value;
            }

        }

        public virtual FileHeaderST Fileheader
        {
            get
            {
                return this._Fileheader;

            }
            set
            {
                this._Fileheader = value;
            }

        }

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

        public virtual string Nationality
        {
            get
            {
                return this.nationalityField;
            }
            set
            {
                this.nationalityField = value;
            }
        }

        public virtual string Sex
        {
            get
            {
                return this.sexField;
            }
            set
            {
                this.sexField = value;
            }
        }

        public virtual string DateOfBirth
        {
            get
            {
                return this.dateOfBirthField;
            }
            set
            {
                this.dateOfBirthField = value;
            }
        }

    }
}