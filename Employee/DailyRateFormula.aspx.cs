using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace SMEPayroll.employee
{
    public partial class DailyRateFormula : System.Web.UI.Page
    {
        #region STYLE
        protected string sHeadingColor = Constants.HEADING_COLOR;
        protected string sBorderColor = Constants.TABLE_BORDER_COLOR;
        protected string sEvenRowColor = Constants.EVEN_ROW_COLOR;
        protected string sOddRowColor = Constants.ODD_ROW_COLOR;
        protected string sBaseColor = Constants.BASE_COLOR;
        #endregion
        protected ArrayList alDays;
        protected void Page_Load(object sender, EventArgs e)
        {
            FetchData();
        }
        private void FetchData()
        {
            alDays = new ArrayList();
            string sSQL = "sp_GetDaysInMonth";
            SqlDataReader dr = DataAccess.ExecuteReader(CommandType.StoredProcedure, sSQL, null);
            while (dr.Read())
            {
                SMEPayroll.employee.DaysInMonth oDays = new DaysInMonth();
                oDays.year = Utility.ToString(dr.GetValue(0));
                oDays.month = Utility.ToString(dr.GetValue(1));
                oDays.calendar_days = Utility.ToString(dr.GetValue(2));
                oDays.sundays = Utility.ToString(dr.GetValue(3));
                oDays.saturdays = Utility.ToString(dr.GetValue(4));
                oDays.days_week5 = Utility.ToString(dr.GetValue(5));
                oDays.days_week512 = Utility.ToString(dr.GetValue(6));
                oDays.days_week6 = Utility.ToString(dr.GetValue(7));
                oDays.days_week7 = Utility.ToString(dr.GetValue(8));
                alDays.Add(oDays);
            }            
        }
    }
    public class DaysInMonth
    {
        public string year;
        public string month;
        public string calendar_days;
        public string sundays;
        public string saturdays;
        public string days_week5;
        public string days_week512;
        public string days_week6;
        public string days_week7;
    }
}
