
using System;
using System.ComponentModel.DataAnnotations;

namespace SMEPayroll.Appraisal.Model
{
    public class EmployeeObjective
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string RatingType { get; set; }
    }

    public class EnableRplyToEmployee
    {
        public int appId { get; set; }
        public int EmpId { get; set; }
        
    }
    public class AppraisalHistory
    {
        public int ObjectiveId { get; set; }
        public string Comment { get; set; }
        public string Remark { get; set; }
    }
    public class NewTemplate
    {
        public string TemplateName { get; set; }
        public int[] ObjectiveId { get; set; }
    }
    public enum AppraisalStatus
    { 
        Open,
        InProgress,
         Pending,
        Complete,
        InComplete,
        Rejected
    }

    public class Employee
    {
        public string TypeName { get; set; }
        public string EmpName { get; set; }
        public int EmpId { get; set; }
    }
    public class EmployeeAppraisal
    {
        public int? Id { get; set; }
        public int[] EmpId { get; set; }
        public int? AppraisalTemplateID { get; set; }
        public int? ApprisalApprover { get; set; }
        public int DaysToApproveLevel { get; set; }
        public int EnbleToEmployee { get; set; }
       
        public string AppraisalName { get; set; }
       
        public DateTime ValidFrom { get; set; }
        public DateTime ValidEnd { get; set; }


        public static bool AppraisalTitleExists(string appraisalTitle)
        {
            string ssl = "SELECT  Id FROM Appraisal where AppraisalName = '" + appraisalTitle + "'";
            int rply = DataAccess.ExecuteScalar(ssl);
            if (rply > 0)
                return true;
            else
                return false;
        }

        public static bool Exists(int empId, string validdate)
        {
            string ssl = "SELECT  Id FROM Appraisal where EmpId = "+ empId + " and ValidFrom <='" + validdate + "' and ValidEnd >= '" + validdate + "'";
            int rply = DataAccess.ExecuteScalar(ssl);
            if (rply > 0)
                return true;
            else
                return false;
        }
    }

}
