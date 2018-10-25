<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IR8AInfo.aspx.cs" Inherits="SMEPayroll.Management.IR8AInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="SMEPayroll" %>
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR8A Information</b></font>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IR8A information"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/EMPLOYEE/B-Employee.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../IR8A/IR8Adetails.aspx" class="nav"><b>View IR8A </b></a>
                            <br />
                            <tt class="bodytxt">Text Here Line1
                                <br />
                                or Text Here Line 2 .</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IR8A information"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/EMPLOYEE/employeegrp.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/Ir8aSetup.aspx" class="nav"><b>IR8A Setup </b></a>
                            <br />
                            <tt class="bodytxt">Text Here Line1
                                <br />
                                Text Here Line2.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
