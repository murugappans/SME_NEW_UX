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
using Telerik.Web.UI;
using System.Data;
namespace SMEPayroll.Leaves
{
    public partial class LeaveEmp : System.Web.UI.UserControl
    {
        private Appointment apt;
       
        public string Name = null;
        public string startDate = null;
        public string endDate = null;
        public string leaveType = null;
        public string noa = null;
        public string approver = null;
        public string dept = null;
        public string contact = null;

        public Appointment TargetAppointment
        {
            get
            {
                return apt;
            }

            set
            {
                apt = value;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            string SQL = null;
            startDate = Convert.ToDateTime(apt.Owner.UtcToDisplay(apt.Start).ToString()).ToString("yyyy-MM-dd");

            endDate = Convert.ToDateTime(apt.Owner.UtcToDisplay(apt.End).ToString()).ToString("yyyy-MM-dd");
            Name = apt.Subject;
            DataSet leaveDs=new DataSet();
            SQL = "SELECT DISTINCT emp_code,EMP_NAME,APPROVER,START_DATE,END_DATE,CASE  DATEDIFF ( dd , START_DATE , END_DATE )WHEN '0' THEN CAST('0.5' AS VARCHAR)ELSE CAST(DATEDIFF( dd , START_DATE , END_DATE )AS NVARCHAR)END As NO_OF_DAYS ,HAND_PHONE ,LEAVE_TYPE,TIMESESSION,TYPE,DEPTNAME,DEPT_ID FROM EMP_LEAVES INNER JOIN dbo.EMPLOYEE ON EMP_ID = EMP_CODE INNER JOIN LEAVE_TYPES ON lEAVE_TYPE = ID INNER JOIN department ON DEPT_ID = department.ID WHERE STATUS='APPROVED' and EMP_NAME = '" + Name + "' and CONVERT(VARCHAR(10),START_DATE, 121) like '" + startDate + "' AND CONVERT(VARCHAR(10),END_DATE, 121) like '" + endDate + "'";
            leaveDs = DataAccess.FetchRS(CommandType.Text, SQL, null);

            if (leaveDs.Tables[0].Rows.Count > 0)
            {
                leaveType = leaveDs.Tables[0].Rows[0]["Type"].ToString();
                noa = leaveDs.Tables[0].Rows[0]["No_Of_Days"].ToString();
                approver = leaveDs.Tables[0].Rows[0]["Approver"].ToString();
                dept = leaveDs.Tables[0].Rows[0]["DEPTNAME"].ToString();
                contact = leaveDs.Tables[0].Rows[0]["Hand_Phone"].ToString();
            }
        }
       
    }
}


