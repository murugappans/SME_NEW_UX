<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Company.aspx.cs" Inherits="SMEPayroll.Management.Company" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    
        <script type="text/javascript" language="JavaScript1.2"> 
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
    <form id="form1" runat="server">
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Company Information</b></font>
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
                                <font class="colheading"><b>ADMIN MODULE</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td valign="middle" align="right">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/bgifs/company.jpg" /></td>
            </tr>
        </table>
        <br />
        <center>
            <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                width="90%">
                <tr>
                    <td width="95%" align="center">
                        <img alt="" src="../frames/images/bgifs/comapny.jpg" /><font face="tahoma" size="2"><b>Company
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
                        <tt class="bodytxt">Manage all your Company & Company Groups information. Add new Company
                            and Company Groups or update the existing information</tt></td>
                    <td width="30%">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                width="90%">
                <ul>
                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Add Company"))
    {%>
                    <tr>
                        <td width="45%">
                        </td>
                        <td width="50%" align="left">
                            <li><a href="../Company/AddCompanyNew.aspx" class="nav2">Add Company</a></li></td>
                        <td width="5%">
                        </td>
                    </tr>
                    <%}%>
                    <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Company"))
    {%>
                    <tr>
                        <td width="45%">
                        </td>
                        <td width="50%" align="left">
                            <li><a href="../Company/ShowCompanies.aspx" class="nav2">View Company</a></li></td>
                        <td width="5%">
                        </td>
                    </tr>
                    <%}%>
            </table>
        </center>
        </div>
    </form>
</body>
</html>
