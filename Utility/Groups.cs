using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll
{

    #region Nested type: Groups

    public class Groups
    {
        private string _TimeCardNo;
        private int _EmpCode;
        private string _EmpName;
        private int _Id;
       

        public Groups(int Id, string TimeCardNo, string EmpName, int EmpCode)
        {
            _TimeCardNo = TimeCardNo;
            _EmpName = EmpName;
            _Id = Id;
            _EmpCode = EmpCode;
        }

        public int ID
        {
            get { return _Id; }
        }

        public string Time_Card_NO
        {
            get { return _TimeCardNo; }
        }

        public string Emp_Name
        {
            get { return _EmpName; }
        }

        public int Emp_Code
        {
            get { return _EmpCode; }
        }
    }

    #endregion
}
