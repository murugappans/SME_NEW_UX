<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Reportdetail.aspx.cs" Inherits="SMEPayroll.Management.Reportdetail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title>SMEPayroll</title>
    
        <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>
</head>
<body style="margin-left: auto">
    <form id="form2" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0"  cellspacing="0" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Report Details</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <td width="45%" align="left">
                            <img alt="" src="../frames/images/reports/B-Reports 4.png" /><font face="tahoma"
                                size="2"><b>Pay Related Reports</b></font></a>
                            <hr color="lightgrey" width="300" align="left">
                        </td>
                        <td width="5%">
                        </td>
                        <td width="45%" align="left">
                            <img alt="" src="../frames/images/reports/B-Reports 2.png" /><font face="tahoma"
                                size="2"><b>Employee Related Reports </b></font></a>
                            <hr color="lightgrey" width="300" align="left">
                        </td>
                        <td width="5%">
                        </td>
                    </tr>
                    <ul>
                        <tr>
                            <td width="45%" align="left" valign="top">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Payroll Details Report"))
        {%>
                                <tt><a href="../Reports/crys_payrolldetail_report.aspx" class="bodytxt" style="text-decoration: none;
                                    color: Black" /><tt class="bodytxt">Payroll Details Report</tt><%}%></br>
                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Payroll Summary Report"))
      {%>
                                    <tt><a href="../Reports/crys_empsalsum_report.aspx" class="bodytxt" style="text-decoration: none;
                                        color: Black" /><tt class="bodytxt">Payroll Summary Report</tt><%}%></br>
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Payroll Summary(detail) Report"))
       {%>
                                        <tt><a href="../Reports/crys_empsalhis_report.aspx" class="bodytxt" style="text-decoration: none;
                                            color: Black" /><tt class="bodytxt">Payroll Summary (detail) Report</tt><%}%></br>
                                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CPF Report"))
       {%>
                                            <tt><a href="../Reports/crys_cpf_monthlyreport.aspx" class="bodytxt" style="text-decoration: none;
                                                color: Black" /><tt class="bodytxt">Employers CPF Contribution Report</tt><%}%></br>
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Funds Summary Report"))
       {%>
                                                <tt><a href="../Reports/crys_fundsummary_report.aspx" class="bodytxt" style="text-decoration: none;
                                                    color: Black" /><tt class="bodytxt">Funds Summary Report</tt><%}%></br>
                                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Funds Detail Report"))
      {%>
                                                    <tt><a href="../Reports/crys_fund_report.aspx" class="bodytxt" style="text-decoration: none;
                                                        color: Black" /><tt class="bodytxt">Funds Detail Report</tt><%}%></br>
                                                        <!-- Added BY Raja(16/09/2008)-->
                                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View SDL Detail Report"))
      {%>
                                                        <tt><a href="../Reports/crys_sdl_report.aspx" class="bodytxt" style="text-decoration: none;
                                                            color: Black" /><tt class="bodytxt">SDL Detail Report</tt><%}%></br>
                                                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View SDL Summary Report"))
      {%>
                                                            <tt><a href="../Reports/crys_sdl_report_summary.aspx" class="bodytxt" style="text-decoration: none;
                                                                color: Black" /><tt class="bodytxt">SDL Summary Report</tt><%}%></br>
                            </td>
                            <td width="5%">
                            </td>
                            <td width="45%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Details"))
        {%>
                                <tt><a href="../Reports/crys_employee_report.aspx" class="bodytxt" style="text-decoration: none;
                                    color: Black" /><tt class="bodytxt">Employee Details Report</tt><%}%>
                                    <br />
                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Personal Details"))
      {%>
                                    <tt><a href="../Reports/crys_emppersonal_report.aspx" class="bodytxt" style="text-decoration: none;
                                        color: Black" /><tt class="bodytxt">Employee Personal Details Report</tt>
                                        <%}%>
                                        <br />
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Address Details"))
        {%>
                                        <tt><a href="../Reports/crys_empaddress_report.aspx" class="bodytxt" style="text-decoration: none;
                                            color: Black" /><tt class="bodytxt">Employee Address Details Report</tt><%}%>
                                            <br />
                                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Job Details"))
     {%>
                                            <tt><a href="../Reports/crys_empjobdetails_report.aspx" class="bodytxt" style="text-decoration: none;
                                                color: Black" /><tt class="bodytxt">Employee Job Details Report</tt><%}%>
                                                <br />
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Certs Details"))
      {%>
                                                <tt><a href="../Reports/crys_empcertdetails_report.aspx" class="bodytxt" style="text-decoration: none;
                                                    color: Black" /><tt class="bodytxt">Employee Certs/Docs Details Report</tt><%}%>
                                                    <br />
                                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Salary Details"))
      {%>
                                                    <tt><a href="../Reports/crys_empsaldetails_report.aspx" class="bodytxt" style="text-decoration: none;
                                                        color: Black" /><tt class="bodytxt">Employee Salary Details Report</tt><%}%>
                                                        <br />
                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <td width="45%" align="left">
                            <img alt="" src="../frames/images/reports/B-Reports 3.png" /><font face="tahoma"
                                size="2"><b>Reminder Related Reports</b></font></a>
                            <hr color="lightgrey" width="300" align="left">
                        </td>
                        <td width="5%">
                        </td>
                        <td width="45%" align="left">
<%--                            <img alt="" src="../frames/images/reports/B-Reports.png" /><font face="tahoma" size="2"><b>&nbsp;Employee
                                Related Reports</b></font></a>
                            <hr color="lightgrey" width="300" align="left">
--%>                        </td>
                        <td width="5%">
                        </td>
                    </tr>
                    <ul>
                        <tr>
                            <td width="45%" align="left" valign="top">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pass Expiry Report"))
       {%>
                                <tt><a href="../Reports/crys_wpexpiry_report.aspx" class="bodytxt" style="text-decoration: none;
                                    color: Black" /><tt class="bodytxt">Pass Expiry Report</tt><%}%>
                                    <br />
                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry Report"))
        {%>
                                    <tt><a href="../Reports/crys_passport_report.aspx" class="bodytxt" style="text-decoration: none;
                                        color: Black" /><tt class="bodytxt">Passport Expiry Report</tt><%}%>
                                        <br />
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry Report"))
        {%>
                                        <tt><a href="../Reports/crys_insurance_report.aspx" class="bodytxt" style="text-decoration: none;
                                            color: Black" /><tt class="bodytxt">Insurance Expiry Report</tt><%}%><br />
                                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry Report"))
        {%>
                                            <tt><a href="../Reports/crys_csoc_report.aspx" class="bodytxt" style="text-decoration: none;
                                                color: Black" /><tt class="bodytxt">CSOC Expiry Report</tt><%}%><br />
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Levy Report"))
  {%>
                                                <tt><a href="../Reports/crys_clavon_levyreport.aspx" class="bodytxt" style="text-decoration: none;
                                                    color: Black" /><tt class="bodytxt">Levy Report</tt><%}%><br />
                            </td>
                            <td width="5%">
                            </td>
                            <td width="45%" align="left" valign="top">
<%--                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Individual  Monthly Leave Report"))
        {%>
                                <tt><a href="../Reports/crys_leaves_monthly_report.aspx" class="bodytxt" style="text-decoration: none;
                                    color: Black" /><tt class="bodytxt">Leave Report ( Individually) Monthly Report</tt><%}%>
                                    </br>
                                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Individual  Monthly Details  Leave Report"))
        {%>
                                    <tt><a href="../Reports/crys_monthlyleavedetails_report.aspx" class="bodytxt" style="text-decoration: none;
                                        color: Black" /><tt class="bodytxt">Leave Report ( Individually) Monthly Details Report</tt><%}%>
                                        </br>
                                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Individual  Yearly  Leave Report"))
       {%>
                                        <tt><a href="../Reports/crys_leaves_yearly_report.aspx" class="bodytxt" style="text-decoration: none;
                                            color: Black" /><tt class="bodytxt">Leave Report ( Individually) Yearly Report</tt><%}%>
                                            </br>
                                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Individual  Yearly Detail  Leave Report"))
         {%>
                                            <tt><a href="../Reports/crys_yearlyleavedetails_report.aspx" class="bodytxt" style="text-decoration: none;
                                                color: Black" /><tt class="bodytxt">Leave Report ( Individually) Yearly Details Report</tt><%}%>
                                                </br></br>
                                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Summary Yearly  Leave Report"))
           {%>
                                                <tt><a href="../Reports/crys_summaryleave_report.aspx" class="bodytxt" style="text-decoration: none;
                                                    color: Black" /><tt class="bodytxt">Yearly Leave Report (Summary) - Standard </tt>
                                                    <%}%></br>
--%>                            </td>
                            <td width="5%">
                            </td>
                        </tr>
                </table>
            </center>
            
            
<%--            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View TimeCard Details") && (Session["TimeSheetApproved"].ToString() == "1"))
       {%>
                    <tr>
                        <td width="45%" align="left">
                            <img alt="" src="../frames/images/reports/B-Reports.png" /><font face="tahoma" size="2"><b>TimeCard
                                Reports</b></font></a>
                            <hr color="lightgrey" width="300" align="left">
                        </td>
                        <td width="5%">
                        </td>
                    </tr>
                    <ul>
                        <tr>
                            <td width="45%" align="left" valign="top">
                                <tt><a href="../Reports/TimeSheetReports.aspx" class="bodytxt" style="text-decoration: none;
                                    color: Black" /><tt class="bodytxt">Time Card Report</tt>
                                    <br />
                            </td>
                            <td width="5%">
                            </td>
                            <td colspan="2"> 
                            </td>
                        </tr>
                        <%}%>
                </table>
            </center>
--%>        </div>
    </form>
</body>
</html>
