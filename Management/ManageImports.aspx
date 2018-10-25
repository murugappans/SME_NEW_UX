<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageImports.aspx.cs" Inherits="SMEPayroll.Management.ManageImports" %>
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
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Imports</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
        <div>
              <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                          <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Employee Detail Import"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/IMPORTS/B-importadditions.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ImportEmployee.aspx" class="nav"><b>Bulk Employee Import
                            </b></a>
                            <br />
                            <tt class="bodytxt"></tt><br />
                        </td>
                        <%}
                          if (Utility.AllowedAction1(Session["Username"].ToString(), "Employee Detail Import"))
                          {%>
                        <td width="9%" align="left" style="visibility:hidden">
                            <img alt="" src="../frames/images/IMPORTS/B-importdeductions.png" /></td>
                        <td style="width: 41%; text-align: left;visibility:hidden">
                        <%--<a href="../Management/bulkDeductions.aspx" class="nav">--%>
                            <a href="../Management/MappingExcel_MultiAddition.aspx" class="nav"><b>Bulk Additions
                            </b></a>
                            <br />
                          <tt class="bodytxt"> </tt><br />
                         <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                       
                    </tr>
                </table>
            </center>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr >
                          <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Payroll Additions Import"))
                          {%>
                        <td align="left" style="width: 9%;visibility:hidden">
                            <img alt="" src="../frames/images/IMPORTS/B-importadditions.png" /></td>
                        <td style="width: 41%; text-align: left;visibility:hidden">
                            <a href="../Management/MappingExcel_MultiDeduction.aspx" class="nav"><b>Bulk Deductions
                            </b></a>
                            <br />
                            <tt class="bodytxt"></tt><br />
                        </td>
                        <%}
                        if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Payroll Deductions Import"))
                          {%>
                        <td width="9%" align="left" style="border-collapse: collapse;visibility:hidden">
                            <img alt="" src="../frames/images/IMPORTS/B-importdeductions.png" /></td>
                        <td style="width: 41%; text-align: left;border-collapse: collapse;visibility:hidden">
                            <a href="../Management/bulkDeductions.aspx" class="nav"><b>Bulk Deductions
                            </b></a>
                            <br />
                          <tt class="bodytxt"> </tt><br />
                          </td>
                         <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                       
                    </tr>
                </table>
            </center>
            <br />
          
            <br />
          <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                          <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Employee Detail Import1"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/IMPORTS/B-importemployee.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ImportEmployeeInfo.aspx" class="nav"><b>Import Employee Info
                            </b></a>
                            <br />
                            <tt class="bodytxt">Import and Process Employee Information </tt></td>
                        <%}
                           if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Payroll Deductions Import1"))
                          {%>
                        <td align="left" style="width: 9%" style="border-collapse: collapse;visibility:hidden">
                            <img alt="" src="../frames/images/IMPORTS/B-importdeductions.png" /></td>
                        <td style="width: 41%; text-align: left" style="border-collapse: collapse;visibility:hidden">
                            <a href="../Management/ImportOvertime.aspx" class="nav"><b>Import Overtime
                            </b></a>
                            <br />
                            <tt class="bodytxt"></tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                    </tr>
                </table>
            </center>
            <br />
               <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;visibility:hidden"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Bulk Claims Import"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/IMPORTS/B-importclaims.png" /></td>
                        <td style="width: 41%; text-align: left">
                           <a href="../Management/ClaimsAdditions.aspx" class="nav"><b>Claims Additions </b>
                            </a>
                            <br />
                            <tt class="bodytxt">Add Claims.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Rejected Claims1"))
                          {%>
                        <td width="9%" align="left">
                           </td>
                        <td style="width: 41%; text-align: left">
                            
                            <br />
                            <tt class="bodytxt"></tt></td>
                         <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
