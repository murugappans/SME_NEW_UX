<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DropDowns.aspx.cs" Inherits="SMEPayroll.Management.DropDowns" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../STYLE/PMSStyle.css" type="text/css"></link>
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
<body>
    <form id="frmClients" runat="server">
    <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <br />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Dropdowns</b></font>
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td bgcolor="<% =sHeadingColor %>" colspan="4">
                                <font class="colheading">
                                    <center>
                                        <b>ADMIN MODULE</b></center>
                                </font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td valign="middle" align="right">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/bgifs/documents.jpg" /></td>
            </tr>
        </table>
        <br />
        <center>
            <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                width="90%">
                <tr>
                    <td width="95%" align="center">
                        <img alt="" src="../frames/images/bgifs/documents.jpg" /><font face="tahoma" size="2"><b>DropDown
                            Management</b></font>
                        <hr color="lightgrey" width="400">
                    </td>
                    <td width="5%">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                width="90%">
                <tr>
                    <td width="30%">
                    </td>
                    <td width="40%">
                        <tt class="bodytxt">Manage all your DropDowns information. Add new DropDowns or update
                            the existing information</tt></td>
                    <td width="30%">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                width="90%">
                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Lookups"))
    {%>
                <tr>
                    <td width="35%">
                    </td>
                    <td width="60%" align="left">
                        <a href="../Management/ShowDropdowns.aspx" class="nav2">Manage Dropdowns</a></td>
                    <td width="5%">
                    </td>
                </tr>
                <%}%>
            </table>
        </center>
    </form>
</body>
</html>
