using SMEPayroll.EmployeeRoster.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SMEPayroll.Main.Model.DataSQL
{
    public class EmployeeDataSQL
    {
        static int compID = Utility.ToInteger(HttpContext.Current.Session["Compid"]);
        public static string GetAllEventList()
        {
            var _sql = "";
            _sql = "SELECT emp_code as mainID, emp_name as title, date_of_birth as start,other='', _type = 'Birthday' FROM employee WHERE date_of_birth IS NOT NULL AND Company_Id = " + compID + " "
                    + "Union All "
                    + "Select  ID AS mainID, holiday_name AS title ,holiday_date as start,other='', _type = 'Publicholiday' from public_holidays where companyid = " + compID + " "
                    + "Union All "
                    + "SELECT emp_code as mainID, emp_name as title,joining_date as start,  probation_period as other, _type = 'ProbationPeriod' FROM employee WHERE probation_period > 0 AND Company_Id = " + compID + " "
                    + "Union All "
                    + "SELECT e.emp_code as mainID,e.emp_name as title,ec.ExpiryDate AS start,other='', _type = 'PassportExpiry' FROM employee e "
                   + "INNER JOIN EmployeeCertificate ec ON e.emp_code = ec.emp_Id "
                   + "INNER JOIN CertificateCategory cc ON cc.ID = ec.CertificateCategoryID "
                   + "WHERE ec.ExpiryDate IS NOT NULL AND e.Company_Id = " + compID + " and cc.Category_Name = 'Passport' ";


                    //+ "SELECT emp_code, emp_name, date_of_birth, passport_expiry, probation_period, joining_date, termination_date, ID = '', holiday_date = '', holiday_name = '', companyid = '', _type = 'PassportExpiry' FROM employee WHERE passport_expiry IS NOT NULL AND Company_Id = " + compID + " "
                    //+ "order by _type;";
            return _sql;
        }

        public static string GetBirthdayList()
        {
            var _sql = "";  
                _sql = "SELECT emp_code,emp_name, date_of_birth,passport_expiry,probation_period,joining_date,termination_date FROM employee WHERE date_of_birth IS NOT NULL AND Company_Id = " + compID + " ORDER BY date_of_birth DESC;"; 
            return _sql;
        }


        public static string GetPHList()
        {
            var _sql = "Select * from public_holidays WHERE companyid = " + compID + " ORDER BY holiday_date DESC";
            return _sql;
        }

        public static string GetProbationPeriodExpiryList()
        {
            var _sql = "SELECT emp_code,emp_name, date_of_birth,passport_expiry,probation_period,joining_date,termination_date FROM employee WHERE probation_period >0 AND Company_Id = " + compID + " ORDER BY joining_date DESC";
            return _sql;
        }

        public static string GetPassportExpiryList()
        {
            var _sql = "";
            //_sql = "SELECT emp_code,emp_name, date_of_birth,passport_expiry,probation_period,joining_date,termination_date FROM employee WHERE passport_expiry IS NOT NULL AND Company_Id = " + compID + " ORDER BY passport_expiry DESC";

            _sql = "SELECT e.emp_code,e.emp_name,ec.ExpiryDate FROM employee e "
                    + "INNER JOIN EmployeeCertificate ec ON e.emp_code = ec.emp_Id "
                    + "INNER JOIN CertificateCategory cc ON cc.ID = ec.CertificateCategoryID "
                    + "WHERE ec.ExpiryDate IS NOT NULL AND e.Company_Id = " + compID + " and cc.Category_Name = 'Passport'  ORDER BY ExpiryDate DESC;";

            return _sql;
        }


        public static string GetAllDocsExpiryList()
        {
            var _sql = "";
            _sql = "SELECT e.emp_code,e.emp_name,ec.ExpiryDate FROM employee e "
                    + "INNER JOIN EmployeeCertificate ec ON e.emp_code = ec.emp_Id "
                    + "INNER JOIN CertificateCategory cc ON cc.ID = ec.CertificateCategoryID "
                    + "WHERE ec.ExpiryDate IS NOT NULL AND e.Company_Id = " + compID + " and cc.Category_Name IS NOT NULL  ORDER BY ExpiryDate DESC;";
            return _sql;
        }

    }
}