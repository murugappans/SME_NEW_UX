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
    public class employe
    {
        private int _id;
        private string _Name;
        private string _Nric;
        private string _postal_code;
        private string _block_no;
        private string _level_no;
        private string _street_name;
        private string _unit_number;
        private string _Last_Name;

        public virtual string Last_Name
        {
            get
            {
                return this._Last_Name;
            }
            set
            {
                this._Last_Name = value;
            }
        }
        public virtual string street_name
        {
            get
            {
                return this._street_name;
            }
            set
            {
                this._street_name = value;
            }
        }


        public virtual string unit_number
        {
            get
            {
                string unit = "";
                if (!string.IsNullOrEmpty(_unit_number))
                {
                   unit= " " + _unit_number;
                }

                return unit;
            }
            set
            {
                this._unit_number = value;
            }
        }
        public virtual string level_no
        {
            get
            {
                string level = "";
                if (!string.IsNullOrEmpty(_level_no))
                {
                    level= "#" + _level_no;
                }


                return level;
            }
            set
            {
                this._level_no = value;
            }
        }


        public virtual string block_no
        {
            get
            {
                string blk = "";
                if (!string.IsNullOrEmpty(_block_no))
                {
                    blk = "BLK " + _block_no;
                }

                return blk;
            }
            set
            {
                this._block_no = value;
            }
        }

        public virtual string postal_code
        {
            get
            {
                return this._postal_code;
            }
            set
            {
                this._postal_code = value;
            }
        }




        public virtual int id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }
        public virtual string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        public virtual string Nric
        {
            get
            {
                return this._Nric;
            }
            set
            {
                this._Nric= value;
            }
        }
    }
}
