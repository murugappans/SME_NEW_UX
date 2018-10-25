using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SMEPayroll.UserRights
{
    public static class EmployeeRights
    {
        public static DataSet empRights;
        public static DataSet sEmpRights
        {
            get 
            {
                return empRights;
            }
            set 
            {
                empRights = value;
            }
        }
    }
   
}
