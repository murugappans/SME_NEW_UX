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
namespace SMEPayroll.Main
{
    public partial class Grid : System.Web.UI.Page
    {
        string compid = "", empcode = "";
        SqlParameter[] parms;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Utility.ToString(Session["Username"]) == "")
                Response.Redirect("../SessionExpire.aspx");

            compid = Session["Compid"].ToString();
            empcode = Session["EmpCode"].ToString();
            string sgroupname = Utility.ToString(Session["GroupName"]);
            string s = Request.QueryString["id"].ToString();
            string noOf = Request.QueryString["nof"].ToString();
            string sSQL = "";
            //Senthil Start
            //pas expiry
            //if (s == "10")
            //{
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =6 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " Order by 5";
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =6 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "' ) order by [Days] desc";
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =7 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "' ) order by [Days] desc";
            //    sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'FIN/WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id='" + compid + "'  and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "'  ) order by [Days] ASC";

            //}
            if (s == "10")
            {
                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =6 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " Order by 5";
                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =6 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "' ) order by [Days] desc";
                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =4 Or CertificateCategoryID =5 Or CertificateCategoryID =7 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "' ) order by [Days] desc";
                
                    sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'FIN/WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id='" + compid + "'  and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compid + "'  ) order by [Days] ASC";
                
            }
            //Pending leave Request
            //if (s == "20")
            //{
            //    //sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) FromDate, ";
            //    //sSQL = sSQL + " Convert(varchar(15),End_Date,103) ToDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
            //    //sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
            //    //sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
            //    //sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=" + noOf + " and  b.emp_code  in ( select emp_code from employee where termination_date is null"; 
            //    ////AND emp_supervisor = " + empcode + " ";
            //    //sSQL = sSQL + ")  order by 5 desc";


            //    sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate, ";
            //    sSQL = sSQL + " Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
            //    sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
            //    sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
            //    sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compid + "') and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compid + "'";
            //    //AND emp_supervisor = " + empcode + " ";
            //    //sSQL = sSQL + ")  order by 5 desc";
            //    sSQL = sSQL + ")  order by [Days] ASC";

            //}
            if (s == "20")
            {
                //sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) FromDate, ";
                //sSQL = sSQL + " Convert(varchar(15),End_Date,103) ToDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
                //sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                //sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
                //sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=" + noOf + " and  b.emp_code  in ( select emp_code from employee where termination_date is null"; 
                ////AND emp_supervisor = " + empcode + " ";
                //sSQL = sSQL + ")  order by 5 desc";

                if (sgroupname == "Super Admin")
                {
                    //sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate, ";
                    //sSQL = sSQL + " Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
                    //sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                    //sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
                    //sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compid + "') and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compid + "'";
                    ////AND emp_supervisor = " + empcode + " ";
                    ////sSQL = sSQL + ")  order by 5 desc";
                    //sSQL = sSQL + ")  order by [Days] ASC";
                    //---------------------
                    sSQL = "select  b.time_card_no as TimeCardNo, isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate, ";
                    sSQL = sSQL + " Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
                    // sSQL = sSQL + " employee b, leave_types c, emp_group d where  a.emp_id = b.emp_code "; //--murugan
                    sSQL = sSQL + " employee b, leave_types c, emp_group d where datediff(dd,getdate(),a.start_date)>0 and  a.emp_id = b.emp_code ";
                    sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and ";
                    //a.status = 'Open' ";
                    //sSQL = sSQL + "status <>'Rejected' and a.status = 'Open'  AND  status <>'Approved'";  //--murugan
                    sSQL = sSQL + "status <>'Rejected' AND  status <>'Approved'";
                    sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compid + "'  ) and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compid + "'  ";
                    //AND emp_supervisor = " + empcode + "
                    sSQL = sSQL + ")  order by [Days] ASC";
                }
                else
                {
                    if (Utility.GetGroupStatus(Convert.ToInt32(compid)) == 1)
                    {
                        sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate, ";
                        sSQL = sSQL + " Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
                        sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
                        sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compid + "') and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compid + "' and b.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=" + Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE()) OR b.emp_code IN(" + Utility.ToInteger(Session["EmpCode"]) + ") ";
                        //AND emp_supervisor = " + empcode + " ";
                        //sSQL = sSQL + ")  order by 5 desc";
                        sSQL = sSQL + ")  order by [Days] ASC";
                    }
                    else
                    {
                        sSQL = "select b.time_card_no as TimeCardNo,isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate, ";
                        sSQL = sSQL + " Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a, ";
                        sSQL = sSQL + " employee b, leave_types c, emp_group d where a.emp_id = b.emp_code ";
                        sSQL = sSQL + " and a.leave_Type = c.id and b.Emp_Group_Id=d.id and a.status = 'Open' ";
                        sSQL = sSQL + " and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compid + "') and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compid + "'";
                        sSQL = sSQL + "AND emp_supervisor = " + empcode + "";
                        //sSQL = sSQL + ")  order by 5 desc";
                        sSQL = sSQL + ")  order by [Days] ASC";
                    }
                }

            }
            //passport Expiry
            //if (s == "30")
            //{
            //    // sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'PP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =3 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";
            //    sSQL = "Select Top 5 EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'PP No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 4 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + compid + "' )  Order by  [Days] Asc";
            //}
            //if (s == "40")
            //{
            //    sSQL = "select  time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name, Insurance_number 'Insurance NO', Convert(varchar(15),Insurance_expiry,103) 'Exp Date' from employee where termination_date is null and company_id= " + compid;
            //    sSQL = sSQL + " and datediff(dd, getdate(), Insurance_expiry)<=" + noOf + " order by 3";
            //}
            if (s == "30")
            {     
                    // sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'PP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =3 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";
                    sSQL = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'PP No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 4 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + compid + "' )  Order by  [Days] Asc";
               
            }
            if (s == "40")
            {     
                    sSQL = "select  time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name, Insurance_number 'Insurance NO', Convert(varchar(15),Insurance_expiry,103) 'Exp Date' from employee where termination_date is null and company_id= " + compid;
                    sSQL = sSQL + " and datediff(dd, getdate(), Insurance_expiry)<=" + noOf + " order by 3";
               
            }
            //CSOC Expiring 
            //if (s == "50")
            //{
            //    //sSQL = "select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name, CSOC_number 'CSOC NO', Convert(varchar(15),CSOC_expiry,103) 'Exp Date' from employee where termination_date is null and company_id= " + compid;
            //    //sSQL = sSQL + " and datediff(dd, getdate(), CSOC_expiry)<=" + noOf +" order by 3";
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =2 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";
            //    sSQL = "Select EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'CSOC No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =5 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5  and Company_Id='" + compid + "')   Order by  [Days] ASC ";
            //}
            if (s == "50")
            {
               
                    //sSQL = "select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name, CSOC_number 'CSOC NO', Convert(varchar(15),CSOC_expiry,103) 'Exp Date' from employee where termination_date is null and company_id= " + compid;
                    //sSQL = sSQL + " and datediff(dd, getdate(), CSOC_expiry)<=" + noOf +" order by 3";
                    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =2 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";
                    sSQL = "Select EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'CSOC No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =5 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5  and Company_Id='" + compid + "')   Order by  [Days] ASC ";      
            }
            //Insurance Expiring 
            //if (s == "60")
            //{
            //    //sSQL = "Select Name,[Start Date], [End Date], [Leave Type] From (select distinct isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
            //    //sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date,datediff(dd,getdate(),a.start_date) [Days] from ";
            //    //sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
            //    //sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
            //    //sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
            //    //sSQL += " and c.termination_date is null and c.company_id=" + compid + " )  D  Order By D.start_date,[Name]";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =1 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";

            //    sSQL = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Insurance No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =6 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + compid + "') Order by  [Days] ASC ";

            //}
            if (s == "60")
            {
                //sSQL = "Select Name,[Start Date], [End Date], [Leave Type] From (select distinct isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
                //sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date,datediff(dd,getdate(),a.start_date) [Days] from ";
                //sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
                //sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
                //sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
                //sSQL += " and c.termination_date is null and c.company_id=" + compid + " )  D  Order By D.start_date,[Name]";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'WP NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID =1 And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + "  Order by 5";
               
               sSQL = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Insurance No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =6 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + compid + "') Order by  [Days] ASC ";
                
            }
            //Employee On Leave
            //if (s == "70")
            //{
            //    // sSQL = "Select  TimeCardNo,Name,[Start Date], [End Date], [Leave Type] From (select distinct c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
            //    // sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date from ";
            //    // sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
            //    // sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
            //    //// sSQL += " and (b.leave_date >= getdate()  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
            //    // sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
            //    // sSQL += " and c.termination_date is null and c.company_id=" + compid + " ";
            //    // sSQL += ")  D  Order By D.start_date,[Name]";



            //    sSQL = "Select  TimeCardNo,Name,[Start Date], [End Date], [Leave Type] From (select distinct c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
            //    sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date from ";
            //    sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
            //    sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
            //    // sSQL += " and (b.leave_date >= getdate()  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
            //    sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + compid + "' ))";
            //    sSQL += " and c.termination_date is null and c.company_id=" + compid + " ";
            //    sSQL += ")  D  Order By D.start_date,[Name]";

            //}
            if (s == "70")
            {
                // sSQL = "Select  TimeCardNo,Name,[Start Date], [End Date], [Leave Type] From (select distinct c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
                // sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date from ";
                // sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
                // sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
                //// sSQL += " and (b.leave_date >= getdate()  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
                // sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
                // sSQL += " and c.termination_date is null and c.company_id=" + compid + " ";
                // sSQL += ")  D  Order By D.start_date,[Name]";

                    //sSQL = "Select  TimeCardNo,Name,[Start Date], [End Date], [Leave Type] From (select distinct c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
                    //sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date from ";
                    //sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
                    //sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
                    //sSQL += " and c.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID="+ Utility.ToInteger(Session["EmpCode"]) + " AND ValidFrom<=GETDATE())";
                    //// sSQL += " and (b.leave_date >= getdate()  and datediff(dd,getdate(),a.start_date) <=" + noOf + ")";
                    //sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + compid + "' ))";
                    //sSQL += " and c.termination_date is null and c.company_id=" + compid + " ";
                    //sSQL += ")  D  Order By D.start_date,[Name]";


                sSQL = "Select  TimeCardNo,[Name],[Start Date], [End Date], [Leave Type] From (select distinct  c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',";
                sSQL += "convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date,datediff(dd,getdate(),a.start_date) [Days] from ";
                sSQL += "emp_leaves a,emp_leaves_detail b,employee c ,leave_types d ";
                sSQL += "where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved'";
                sSQL += " and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + compid + "' ))";
                sSQL += " and c.termination_date is null and c.company_id=" + compid + " and Company_Id='" + compid + "' )  D  Order By D.start_date,[Name] Asc";

               
            }
            //Next 30 days employees birthday list(Added By Raja)
            //if (s == "80")
            //{
            //    sSQL = "sp_calbday_details";
            //    SqlParameter[] parms = new SqlParameter[2];
            //    int i = 0;
            //    parms[i] = new SqlParameter("@company_id", Utility.ToInteger(compid));
            //    parms[1] = new SqlParameter("@no_ofDays", Utility.ToInteger(noOf));
            //    RadGrid1.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            //}
            if (s == "80")
            {
                sSQL = "sp_calbday_details";
                SqlParameter[] parms = new SqlParameter[3];
                int i = 0;
                parms[i] = new SqlParameter("@company_id", Utility.ToInteger(compid));
                parms[1] = new SqlParameter("@no_ofDays", Utility.ToInteger(noOf));
                parms[2] = new SqlParameter("@UserID", Utility.ToInteger(Session["EmpCode"]));

                RadGrid1.DataSource = DataAccess.FetchRS(CommandType.StoredProcedure, sSQL, parms);
            }
            ////Probation Expiry in next 1 month
            //if (s == "90")
            //{
            //    //sSQL = "select  emp_name as Name,convert(varchar(20),joining_date,103) 'Date Of Joining',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Probation Expiry Date' from employee where company_id=" + compid;
            //    //sSQL = sSQL + " and datediff(dd, getdate(), dateadd(month,probation_period,joining_date))<=" + noOf +" and probation_period<>-1 AND YEAR(JOINING_DATE)=YEAR(GETDATE()) order by 3";

            //    //sSQL = "select time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Name,convert(varchar(20),joining_date,103) 'Date Of Joining',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Prob Exp Date',datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + compid;
            //    //sSQL = sSQL + " AND termination_date is null AND confirmation_date is null   AND datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))<=" + noOf + " AND  datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1";

            //    sSQL = "select time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Name,convert(varchar(20),joining_date,103) 'Date Of Join',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Prob Exp Date',datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + compid;
            //    sSQL = sSQL + " AND termination_date is null   AND datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + compid + "' )  and probation_period<>-1 AND confirmation_date is null order by [Days] ASC";
            //}
            if (s == "90")
            {
                //sSQL = "select  emp_name as Name,convert(varchar(20),joining_date,103) 'Date Of Joining',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Probation Expiry Date' from employee where company_id=" + compid;
                //sSQL = sSQL + " and datediff(dd, getdate(), dateadd(month,probation_period,joining_date))<=" + noOf +" and probation_period<>-1 AND YEAR(JOINING_DATE)=YEAR(GETDATE()) order by 3";

                //sSQL = "select time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Name,convert(varchar(20),joining_date,103) 'Date Of Joining',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Prob Exp Date',datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + compid;
                //sSQL = sSQL + " AND termination_date is null AND confirmation_date is null   AND datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))<=" + noOf + " AND  datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))>0 and probation_period<>-1";
               
                    sSQL = "select time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Name,convert(varchar(20),joining_date,103) 'Date Of Join',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Prob Exp Date',datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=" + compid;
                    sSQL = sSQL + " AND termination_date is null   AND datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + compid + "' )  and probation_period<>-1 AND confirmation_date is null order by [Days] ASC";
               
            }
            //other certificate Expirt
            //if (s == "100")
            //{
            //    //sSQL = "Select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date' from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID NOT IN (1,2,3,4,5) And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf +" Order by 4";
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 Order by 4";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "')  order by [Days] desc";
            //    sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 9 And termination_date is null and EY.company_id='" + compid + "'  and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "'  ) order by [Days] ASC";
            //}
            if (s == "100")
            {
                //sSQL = "Select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date' from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID NOT IN (1,2,3,4,5) And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf +" Order by 4";
                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 Order by 4";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "')  order by [Days] desc";
                
                    sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 9 And termination_date is null and EY.company_id='" + compid + "'  and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "'  ) order by [Days] ASC";
                
            }
            //if (s == "110")
            //{
            //    //sSQL = "select  isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0 Order By JOINING_DATE ";                
            //    //sSQL = "select Top 5 time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0  AND ((Select Month(JOINING_DATE))-(Select Month(GETDATE()))) =0  AND  datediff(YY,JOINING_DATE,GETDATE())>0 Order By JOINING_DATE ";
            //    sSQL = "select  time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0  AND ((Select Month(JOINING_DATE))-(Select Month(GETDATE()))) =0  AND  datediff(YY,JOINING_DATE,GETDATE())>0 Order By JOINING_DATE ";

            //}
            if (s == "110")
            {
                //sSQL = "select  isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0 Order By JOINING_DATE ";                
                //sSQL = "select Top 5 time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0  AND ((Select Month(JOINING_DATE))-(Select Month(GETDATE()))) =0  AND  datediff(YY,JOINING_DATE,GETDATE())>0 Order By JOINING_DATE ";
              
                    sSQL = "select  time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') as Name,convert(varchar(20),joining_date,103) 'Date Of Joining', datediff(YY,JOINING_DATE,GETDATE()) AS 'No Of Service' from employee where company_id=" + compid + " AND termination_date is null   AND datediff(YY,JOINING_DATE,GETDATE()) >= 0  AND ((Select Month(JOINING_DATE))-(Select Month(GETDATE()))) =0  AND  datediff(YY,JOINING_DATE,GETDATE())>0 Order By JOINING_DATE ";
                
            }
            //if (s == "160")
            //{
            //    //sSQL = "Select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date' from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID NOT IN (1,2,3,4,5) And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf +" Order by 4";
            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 Order by 4";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

            //    //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "')  order by [Days] desc";
            //    sSQL = "Select EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'License No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + compid + "' ) Order by [days] ASC";
            //}

            if (s == "160")
            {
                //sSQL = "Select  isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date' from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID NOT IN (1,2,3,4,5) And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf +" Order by 4";
                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=" + noOf + " and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 Order by 4";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID > 6  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "') and datediff(yy,getdate(),ExpiryDate)=0  and datediff(mm,getdate(),ExpiryDate)=0 order by [Days] desc";

                //sSQL = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate NO',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code where CertificateCategoryID <> 7  And termination_date is null and company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compid + "')  order by [Days] desc";
              
                sSQL = "Select EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'License No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 10 And termination_date is null and EY.company_id= " + compid + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=10 and Company_Id='" + compid + "' ) Order by [days] ASC";
                
            }
            if (s == "150")
            {
                sSQL = "";
                //sSQL = "Select TOP 100 *  FROM VIEW_GET_DATABASE_LOG_DETAILS Order by LogTime DESC";
                sSQL = " SELECT TOP 20 * FROM VIEW_GET_DATABASE_LOG_DETAILS  WHERE LogMessage Like '%database%' Order by LogTime ASC";
            }
            //Senthil End
            if (s == "80")
            {
                RadGrid1.DataBind();
            }
            else
            {
                RadGrid1.DataSource = DataAccess.FetchRS(CommandType.Text, sSQL, null);
                RadGrid1.DataBind();
            }


        }

       
        protected void btnExportExcel_click(object sender, EventArgs e)
        {
            //ExportToExcel(sqlRptDs, 0, Response, "EmployeeReports");
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = true;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
            RadGrid1.MasterTableView.ExportToExcel();
        }

    }
}
