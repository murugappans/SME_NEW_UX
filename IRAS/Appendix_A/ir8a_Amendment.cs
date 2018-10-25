using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace IRAS.Appendix_A
{
    public class ir8a_Amendment
    {
        private int _ID;
      
	    private int _IRYear; 
	    private int _Emp_ID;
	    private decimal _GrossPay ;
	    private decimal _Bonus ;
	    private decimal _DirectorFee ;
	    private decimal _Commission; 
	    private decimal _Pension ;
	    private decimal _TransAllow ;
	    private decimal _EntAllow ;
	    private decimal _OtherAllow ;
	    private decimal _EmployeeCPF ;
	    private decimal _Funds;
	    private decimal _MBMF ;
	    private string _ISAmendment;

        public ir8a_Amendment()
        {
        }


        public virtual int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }

        public virtual int IRYear
        {
            get { return this._IRYear; }
            set { this._IRYear = value; }
        }

        public virtual int Emp_ID
        {
            get { return this._Emp_ID; }
            set { this._Emp_ID = value; }
        }

        public virtual decimal GrossPay
        {
            get { return this._GrossPay; }
            set { this._GrossPay = value; }
        }

        public virtual decimal Bonus
        {
            get { return this._Bonus; }
            set { this._Bonus = value; }
        }

        public virtual decimal DirectorFee
        {
            get { return this._DirectorFee; }
            set { this._DirectorFee = value; }
        }
        public virtual decimal Commission
        {
            get { return this._Commission; }
            set { this._Commission = value; }
        }

        public virtual decimal Pension 
        {
            get { return this._Pension; }
            set { this._Pension = value; }
        }

        public virtual decimal TransAllow
        {
            get { return this._TransAllow; }
            set { this._TransAllow = value; }
        }

        public virtual decimal EntAllow
        {
            get { return this._EntAllow; }
            set { this._EntAllow = value; }
        }
        public virtual decimal OtherAllow
        {
            get { return this._OtherAllow; }
            set { this._OtherAllow = value; }
        }
        public virtual decimal EmployeeCPF
        {
            get { return this._EmployeeCPF; }
            set { this._EmployeeCPF = value; }
        }
        public virtual decimal Funds
        {
            get { return this._Funds; }
            set { this._Funds = value; }
        }
        public virtual decimal MBMF
        {
            get { return this._MBMF; }
            set { this._MBMF = value; }
        }
        public virtual string ISAmendment
        {
            get { return this._ISAmendment; }
            set { this._ISAmendment = value; }
        }
    }
}
