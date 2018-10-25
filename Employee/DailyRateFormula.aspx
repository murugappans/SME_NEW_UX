<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DailyRateFormula.aspx.cs"
    Inherits="SMEPayroll.employee.DailyRateFormula" %>
    <%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily Rate</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Daily Rate Formula</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right"style="height: 25px">&nbsp;
                                <%--<input id="Button2" type="button" onclick="history.go(-1)" value="Backs" class="textfields"
                                    style="width: 80px; height: 22px" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
        <br />
        <table cellpadding="7" cellspacing="2" width="88%" bgcolor="<% =sBorderColor %>"
            border="3">
            <tr>
                <td align="center">
                    <tt class="bodytxt">Monthly Basic Rate of Pay</tt>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <tt class="bodytxt">Working Days in a Month</tt>
                </td>
            </tr>
        </table>
        <br />
        <table cellpadding="1" cellspacing="0" width="80%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="1" width="100%" bordercolor="#000000">
                        <tr>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>YEAR</b></tt></td>
                            <td height="20" align="center" width="16%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>MONTH</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>CALENDAR DAYS</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>SUNDAYS</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>SATURDAYS</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>5 DAY WEEK</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>5.5 DAY WEEK</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>6 DAY WEEK</b></tt></td>
                            <td height="20" align="center" width="12%">
                                <tt class="bodytxt">&nbsp;&nbsp;<b>7 DAY WEEK</b></tt></td>
                        </tr>
                        <% if (alDays != null)
                           {
                               string sRowColor = "";
                               for (int i = 0; i < alDays.Count; i++)
                               {
                                   SMEPayroll.employee.DaysInMonth oDays = (SMEPayroll.employee.DaysInMonth)alDays[i];
                                   if (i % 2 == 0)
                                       sRowColor = sOddRowColor;
                                   else
                                       sRowColor = sEvenRowColor;
                        %>
                        <tr>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.year%>
                                </tt>
                            </td>
                            <td align="center">
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.month%>
                                </tt>
                            </td>
                            <td align="center">
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.calendar_days%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.sundays%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.saturdays%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.days_week5%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.days_week512%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.days_week6%>
                                </tt>
                            </td>
                            <td>
                                &nbsp; <tt class="bodytxt">
                                    <%=oDays.days_week7%>
                                </tt>
                            </td>
                        </tr>
                        <%
                            }
                        }
                        %>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
