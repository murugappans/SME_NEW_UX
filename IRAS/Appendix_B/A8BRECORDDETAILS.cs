using System;
using System.Collections.Generic;
using System.Web;

namespace IRAS.Appendix_B
{
    public class A8BRECORDDETAILS
    {

            private int _ID;
        private string _RecordNo;
            private string companyIDTypeField;

            private string companyIDNoField;

            private string companyNameField;

            private string planTypeField;

            private string dateOfGrantField;

            private string dateOfExerciseField;

            private decimal priceField;

            private decimal openMarketValueAtDateOfGrantField;

            private decimal openMarketValueAtDateOfExerciseField;

            private decimal noOfSharesField;

            private decimal nonExemptGrossAmountField;

            private decimal grossAmountGainsField;
            private string  SectionField;
          private decimal Total_iField;
          private decimal Total_jField;
          private decimal Total_kField;
          private decimal Total_lField;
         private decimal Total_mField;
        private decimal g_Total_iField;
        private decimal g_Total_jField;
        private decimal g_Total_kField;
        private decimal g_Total_lField;
        private decimal g_Total_mField;
        private decimal i_j_k;
        
        
        
        private decimal g_TotalField;
        private int _FK_ID;

        private bool _is_Amentment;
        public virtual bool Is_Amentment
        {
            get
            {
                return this._is_Amentment;
            }
            set
            {
                this._is_Amentment = value;
            }
        }

        //private  A8B2009ST _A8B2009ST;
        




            public 
                A8BRECORDDETAILS()
            {
                
            }

        public virtual decimal I_J_K
        {
             get
            {
                    decimal _m = 0.00m;
               
                    _m = (this.OpenMarketValueAtDateOfExercise - OpenMarketValueAtDateOfGrant) * NoOfShares;
                
                 return  this.i_j_k = _m;
              
             }
             set
            {
                this.i_j_k=value;
            }
        }



        public virtual decimal G_Total_I
        {
            get
            {
                return this.g_Total_iField;
            }
            set
            {
                this.g_Total_iField=value;
            }
        }
        public virtual decimal G_Total_J
        {
            get
            {
                return this.g_Total_jField;
            }
            set
            {
                this.g_Total_jField=value;
            }
        }

        public virtual decimal G_Total_K
        {
            get
            {
                return this.g_Total_kField;
            }
            set
            {
                this.g_Total_kField=value;
            }
        }

        public virtual decimal G_Total_L
        {
            get
            {
                return this.g_Total_lField;
            }
            set
            {
                this.g_Total_lField=value;
            }
        }
        public virtual decimal G_Total_M
        {
            get
            {
                return this.g_Total_mField;
            }
            set
            {
                this.g_Total_mField=value;
            }
        }
        public virtual string RecordNo
        {
            get
            {
                return this._RecordNo;
            }
            set
            {
                this._RecordNo = value;
            }
        }

        public virtual decimal G_Total
        {
            get
            {
                return this.g_TotalField;
            }
            set
            {
                this.g_TotalField = value;
            }
        }
        public virtual int FK_ID
        {
            get { return this._FK_ID; }
            set { this._FK_ID = value; }
        }

        //public virtual A8B2009ST A8B2009ST
        //{
        //    get { return this._A8B2009ST; }
        //    set { this._A8B2009ST=value; }
        //}

        public virtual string Section
        {
            get
            {
                return this.SectionField;
            }
            set
            {
                this.SectionField = value;
            }
        }
        public virtual decimal Total_M
        {
            
            get
            {
                decimal _m = 0.00m;
                if (Section == "A")
                {
                    _m = Total_L;
                    this.Total_mField = _m;
                }
                else if(Section == "B")
                {
                    _m = Total_L+I_J_K;
                    this.Total_mField = _m;
                }
                else if(Section == "C")
                {
                    _m = Total_L+I_J_K;
                    this.Total_mField = _m;

                }
                else if (Section == "D")
                {
                    _m = Total_L+I_J_K;
                    this.Total_mField = _m;
                }
                return this.Total_mField;
            }
            set
            {
                this.Total_mField = value;
            }
        }
        public virtual decimal Total_I
        {
            get
            {
               
                return this.Total_iField;
            }
            set
            {
                this.Total_iField = value;
            }
        }
        public virtual decimal Total_J
        {
            get
            {
              
             
                return this.Total_jField;
            }
            set
            {
                this.Total_jField = value;
            }
        }
        public virtual decimal Total_K
        {
            get
            {
               
              
                return this.Total_kField;
            }
            set
            {
                this.Total_kField = value;
            }
        }
        public virtual decimal Total_L
        {
            get
            {
                decimal _m = 0.00m;
                if (Section == "A")
                {
                    _m = (this.OpenMarketValueAtDateOfExercise-Price)*NoOfShares;
                    this.Total_lField= _m;
                }
                else 
                {
                    _m = (this.OpenMarketValueAtDateOfGrant - Price) * NoOfShares;
                    this.Total_lField = _m;
                }
               




                return this.Total_lField;
            }
            set
            {
                this.Total_lField = value;
            }
        }




        public virtual int ID
            {
                get{ return this._ID;}

                set{this._ID = value;}
            }

        public virtual string CompanyIDType
            {
                get
                {
                    return this.companyIDTypeField;
                }
                set
                {
                    this.companyIDTypeField = value;
                }
            }

        public virtual string CompanyIDNo
            {
                get
                {
                    return this.companyIDNoField;
                }
                set
                {
                    this.companyIDNoField = value;
                }
            }

        public virtual string CompanyName
            {
                get
                {
                    return this.companyNameField;
                }
                set
                {
                    this.companyNameField = value;
                }
            }

        public virtual string PlanType
            {
                get
                {
                    return this.planTypeField;
                }
                set
                {
                    this.planTypeField = value;
                }
            }

        public virtual  string DateOfGrant
            {
                get
                {
                    return this.dateOfGrantField;
                }
                set
                {
                    this.dateOfGrantField = value;
                }
            }

        public virtual string DateOfExercise
            {
                get
                {
                    return this.dateOfExerciseField;
                }
                set
                {
                    this.dateOfExerciseField = value;
                }
            }

        public virtual decimal Price
            {
                get
                {
                    return this.priceField;
                }
                set
                {
                    this.priceField = value;
                }
            }

        public virtual decimal OpenMarketValueAtDateOfGrant
            {
                get
                {
                    return this.openMarketValueAtDateOfGrantField;
                }
                set
                {
                    this.openMarketValueAtDateOfGrantField = value;
                }
            }

        public virtual decimal OpenMarketValueAtDateOfExercise
            {
                get
                {
                    return this.openMarketValueAtDateOfExerciseField;
                }
                set
                {
                    this.openMarketValueAtDateOfExerciseField = value;
                }
            }

        public virtual decimal NoOfShares
            {
                get
                {
                    return this.noOfSharesField;
                }
                set
                {
                    this.noOfSharesField = value;
                }
            }

        public virtual decimal NonExemptGrossAmount
            {
                get
                {
                    return this.nonExemptGrossAmountField;
                }
                set
                {
                    this.nonExemptGrossAmountField = value;
                }
            }

        public virtual decimal GrossAmountGains
            {
                get
                {
                    return this.grossAmountGainsField;
                }
                set
                {
                    this.grossAmountGainsField = value;
                }
            }
        }
    }
