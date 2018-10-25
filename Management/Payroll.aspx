<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Payroll.aspx.cs" Inherits="SMEPayroll.Management.Payroll" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <form id="form1" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
           <uc1:TopRightControl ID="TopRightControl1" runat="server" />
         <table cellpadding="0"  cellspacing="0"  width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%"  border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Payroll Management</b></font>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Additions"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-additons.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollAdditions.aspx" class="nav"><b>Payroll Addition </b></a>
                            <br />
                            <tt class="bodytxt">Manage your payroll addition's information. <br />
                                Add/Edit payroll addition.</tt></td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Deductions"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-deductions .png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollDeductions.aspx" class="nav"><b>Payroll Deduction </b></a>
                            <br />
                            <tt class="bodytxt">Manage your payroll deduction's information.
                                <br />
                                 Add/Edit payroll deduction.</tt></td>
                        <%}%>
                    </tr>
                    <ul>
                    </ul>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View overtimepay") && Utility.IsPayrollCeiling(Session["Compid"].ToString()) == false)
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-payrollvaribales.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../employee/emp_overtime.aspx" class="nav"><b>Overtime Pay </b></a>
                            <br />
                            <tt class="bodytxt"> Manage overtime pay rate for employee.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View overtimepay") && Utility.IsPayrollCeiling(Session["Compid"].ToString()) == true)
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-payrollvaribales.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../employee/emp_overtime_ceiling.aspx" class="nav"><b>Overtime Pay </b></a>
                            <br />
                            <tt class="bodytxt"> Manage overtime pay rate for employee.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Generate or Submit Payroll"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-submitpayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/SubmitPayroll.aspx" class="nav"><b>Submit Payroll</b></a>
                            <br />
                            <tt class="bodytxt">Submit payroll for approval to a designated personel
                                <br />
                                as defined in the company management screen.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <% //if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve or Reject Payroll") || (supervisor)) && (string)Session["processPayroll"].ToString() == "1")
                             if ((Utility.AllowedAction1(Session["Username"].ToString(), "Approve or Reject Payroll") ) && (string)Session["processPayroll"].ToString() == "1")
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-approvepayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/ApprovePayroll.aspx" class="nav"><b>Approve Payroll&nbsp;<asp:Label  ForeColor="red"
                                ID="lblApprove" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">Manage your employee's payroll information.Approve
                                <br />
                                or reject submitted payroll.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if ((Utility.AllowedAction1(Session["Username"].ToString(), "View Generate Payroll") && (string)Session["processPayroll"].ToString() == "1"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-generatepayroll .png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/GenPayroll.aspx" class="nav"><b>Generate Payroll&nbsp;<asp:Label ForeColor="red"
                                ID="lblGenerate" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">View approved payroll and generated Payroll for the month.</tt></td>
                        <%}%>
                    </tr>
                </table>
                <br />
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "print payroll"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-payroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PrintPayroll.aspx" class="nav"><b>Print Payroll&nbsp;<asp:Label  ForeColor="Blue"
                                ID="lblPrint" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">Print or email the generated payroll for the current
                                <br />
                                month and view previous months payroll information.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Unlock Payroll"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-unlockpayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/UnlockPayroll.aspx" class="nav"><b>Unlock Payroll&nbsp;<asp:Label  ForeColor="Blue"
                                ID="lblUnlock" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">Unlock the approved and generated payroll for the month.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multi Additions"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-additons.png" />
                        </td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/SetupAdditionTypes.aspx?type=true" class="nav"><b>Multi Additions</b></a>
                            <br />
                            <tt class="bodytxt">Multi Additions.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Multi Deductions"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-deductions .png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/SetupDeductionTypes.aspx?type=true" class="nav"><b>Multi Deductions</b></a>
                            <br />
                            <tt class="bodytxt">Multi Deductions </tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "print my payroll"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-payroll.png" />
                        </td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PrintMyPayroll.aspx?PageType=1" class="nav"><b>Print My Payslip</b></a>
                            <br />
                            <tt class="bodytxt">Right to Print my Payslip.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Deleted"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-unlockpayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/GenerateLedger.aspx" class="nav"><b>Generate Ledger</b></a>
                            <br />
                            <tt class="bodytxt"></tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Payroll Addition Auto"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-additons.png" />
                        </td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollAdditionsAuto.aspx" class="nav"><b>Payroll Additions Auto&nbsp;<asp:Label
                                ID="Label3" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">Bulk Addition</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Payroll Addition Auto"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-unlockpayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollAdditionAll.aspx" class="nav"><b>Payroll All Additions</b></a>
                            <br />
                            <tt class="bodytxt">Add All Additions </tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
              <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Payroll Addition Auto"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/Payroll/B-deductions .png" />
                        </td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollDeductionsAll.aspx" class="nav"><b>Payroll Deductions&nbsp;<asp:Label
                                ID="Label1" runat="server"></asp:Label></b></a>
                            <br />
                            <tt class="bodytxt">Bulk Deductions</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Payroll Addition Auto1"))
                          {%>
                        <td style="width: 9%" align="left">
                            <img alt="" src="../frames/images/Payroll/B-unlockpayroll.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Payroll/PayrollAdditionAll.aspx" class="nav"><b>Payroll All Additions</b></a>
                            <br />
                            <tt class="bodytxt">Add All Additions </tt></td>
                        <%}%>
                    </tr>
                  
                </table>
            </center>
        </div>
    </form>
</body>
</html>
