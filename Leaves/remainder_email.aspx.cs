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
using Telerik.Web.UI;
using System.Net.Mail;
using System.IO;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
namespace SMEPayroll.Leaves
{
    public partial class remainder_email : System.Web.UI.Page
    {

        SqlConnection  con;
        SqlCommand   cmd;
        SqlCommand cmd1;
        SqlDataAdapter   da;
        SqlDataReader   dr;
        string toAddress;
        string sql;
        protected int compID;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        System.Data.DataTable dt,dt1;
        protected void Page_Load(object sender, EventArgs e)
        {
            con = new SqlConnection();
            con.ConnectionString = Session["ConString"].ToString();
            con.Open();
            compID = Utility.ToInteger(Session["Compid"]);
        }
        protected void  makeBody(string sql)
        {
            dt = new System.Data.DataTable();
           
            cmd1 = new SqlCommand (sql, con);
            
           SqlDataAdapter  da1 = new SqlDataAdapter  (cmd1);
            da1.Fill(dt);
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    xlWorkSheet.Cells[1, i + 1] = dt.Columns[i].Caption;
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        xlWorkSheet.Cells[i + 2, j + 1] = dt.Rows[i][j];
                    }

                }

            }
            dt.Clear();
            da.Dispose();
        //-----------------
            //StringBuilder mailBody = new StringBuilder();
            //System.Data.DataTable  dt=new System.Data.DataTable ();

            //cmd = new SqlCommand(sql, con);
            //da = new SqlDataAdapter(cmd);
            //da.Fill(dt);
            //if(dt.Rows.Count>0)
            //{
            //    mailBody.AppendFormat("<table><tr>");
            //for(int i=0;i<dt.Columns.Count-1 ;i++)
            //{
            //    mailBody.AppendFormat("<th>"+dt.Columns[i].Caption+"</th>");
            // }
            //mailBody.AppendFormat("</tr>");
            //for(int i=0;i<dt.Rows.Count-1  ;i++)
            //{
            //     mailBody.AppendFormat("<tr>");
            // for(int j=0;j<dt.Columns.Count-1 ;j++)
            //{
            //    mailBody.AppendFormat("<td>"+dt.Rows[i][j]+"</td>");
            // }
            //    mailBody.AppendFormat("</tr>");
            //}
            //mailBody.AppendFormat("<table>");

            //}
            //return mailBody;       
     }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            
            int c = 0;
            cmd = new SqlCommand ("select * from EmailRemainder", con);
            dt1 = new DataTable();
           // dr = cmd.ExecuteReader();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt1);

           // while (dr.Read())
            for(int k=0;k<dt1.Rows.Count ;k++)
            {
                //toAddress = dr["Emails"].ToString();
                toAddress =dt1.Rows[k][1].ToString ();
                if (Convert.ToBoolean(dt1.Rows[k][2]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "EmployeeOnLeave";
                    sql = "Select  TimeCardNo,Name,[Start Date], [End Date], [Leave Type] From (select distinct c.time_card_no as TimeCardNo,isnull(c.emp_name,'')+' '+isnull(c.emp_lname,'') Name, convert(varchar(15),a.start_date,103)'Start Date',convert(varchar(15),a.end_date,103) 'End Date',d.[type] 'Leave Type',a.start_date from emp_leaves a,emp_leaves_detail b,employee c ,leave_types d where b.trx_id=a.trx_id and a.emp_id=c.emp_code and a.leave_type=d.id and a.status='Approved' and c.emp_code IN(SELECT EmployeeID FROM EmployeeAssignedToGroup WHERE GroupID=3 AND ValidFrom<=GETDATE()) and (b.leave_date >= getdate()-1  and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=1 and Company_Id='" + compID + "' )) and c.termination_date is null and c.company_id=3 )  D  Order By D.start_date,[Name]";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][3]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "PendingLeaveRequest";
                    sql = "select  b.time_card_no as TimeCardNo, isnull(b.emp_name,'')+' '+isnull(b.emp_lname,'') Name, c.Type Type, Convert(varchar(15),Start_Date,103) StartDate,  Convert(varchar(15),End_Date,103) EndDate,datediff(dd,getdate(),a.start_date) [Days] from emp_leaves a,  employee b, leave_types c, emp_group d where a.emp_id = b.emp_code  and a.leave_Type = c.id and b.Emp_Group_Id=d.id and status <>'Rejected' and a.status = 'Open'  AND  status <>'Approved' and b.termination_date is null and datediff(dd,getdate(),a.start_date) <=(select [Days] from [Remainder_Day] where Sno=2 and Company_Id='" + compID + "'  ) and  b.emp_code  in ( select emp_code from employee where termination_date is null and company_id = '" + compID + "'  )  order by 5 Asc";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][4]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "PassesExpiring";
                    sql = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'FIN/WP No', Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 3 And termination_date is null and EY.company_id=" + compID + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=3 and Company_Id='" + compID + "' ) Order by [days] Asc";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][5]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "PassportExpiring";
                    sql = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'PP No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 4 And termination_date is null and EY.company_id=" + compID + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=4 and Company_Id='" + compID + "' )  Order by  [Days] Asc";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][6]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "CSOCExpiring";
                    sql = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'CSOC No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =5 And termination_date is null and EY.company_id=" + compID + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=5  and Company_Id='" + compID + "')   Order by  [Days] Asc";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][7]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "InsuranceExpiring";
                    sql = "Select  EY.time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Insurance No', Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where CC.ColID =6 And termination_date is null and EY.company_id=" + compID + " and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=6 and Company_Id='" + compID + "') Order by  ExpiryDate Asc";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][8]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "EmployeeBirthday";
                    sql = "";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][9]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "ProbationPeriodExpiring";
                    sql = "select time_card_no as TimeCardNo, isnull(emp_name,'')+' '+isnull(emp_lname,'')  as Name,convert(varchar(20),joining_date,103) 'Date Of Join',convert(varchar(20),dateadd(month,probation_period,joining_date),103)  as 'Prob Exp Date',datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date)) [Days] from employee where company_id=3 AND termination_date is null   AND datediff(dd,GETDATE(),dateadd(month,probation_period,joining_date))<=(select [Days] from [Remainder_Day] where Sno=8 and Company_Id='" + compID + "' )  and probation_period<>-1 AND confirmation_date is null order by [Days] ASC";
                    makeBody(sql);
                }
                if (Convert.ToBoolean(dt1.Rows[k][10]))
                {
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(++c);
                    xlWorkSheet.Name = "OtherCertificatesExpiring";
                    sql = "Select EY.time_card_no as TimeCardNo,isnull(emp_name,'')+' '+isnull(emp_lname,'') Name,CertificateNumber 'Certificate No',Emp_type [Type], Convert(varchar(15),ExpiryDate,103) 'Exp Date',datediff(dd, getdate(), ExpiryDate) [Days] from EmployeeCertificate EC  Inner Join Employee EY On EC.Emp_ID=EY.Emp_Code Inner Join CertificateCategory CC ON  EC.CertificateCategoryID = CC.ID where COLID = 9 And termination_date is null and EY.company_id='" + compID + "'  and datediff(dd, getdate(), ExpiryDate)<=(select [Days] from [Remainder_Day] where Sno=9 and Company_Id='" + compID + "'  ) order by [Days] ASC";
                    makeBody(sql);
                }
               
            }
            xlWorkBook.SaveAs("d:\\remainder_excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            // Excel.XlFileFormat.xlWorkbookNormal
            xlWorkBook.Close(true, misValue, misValue);

            xlApp.Quit();

        } 
            
        }
        
   
}
