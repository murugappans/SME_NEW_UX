<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageIr8a.aspx.cs" Inherits="SMEPayroll.Management.ManageIr8a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0"  style="visibility:hidden">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Reports Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div >
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;visibility:hidden"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IRAS information"))
                          {%>
                        <td align="left" style="width: 9%">
                           <img alt="" src="../frames/images/EMPLOYEE/employeegrp.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Management/ManageIr8aInfo.aspx" class="nav"><b>IRAS Setup</b></a>
                            <br />
                            <tt class="bodytxt">View & Print IR8A information (To be submitted to IRAS end of the
                                year).</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IRAS information"))
                          {%>
                           <td width="9%" align="left">
                            <img alt="" src="../frames/images/toolbar/formsIR8A.jpg" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../IR8A/IR8ADetailsNew.aspx" class="nav"><b>IR8A</b></a>
                            <br />
                            <tt class="bodytxt">View & Print IR8A information 
                                <br />
                                (To be submitted to IRAS end of the
                                year).</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
           <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse;visibility:hidden"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IRAS information"))
                          {%>
                        <td align="left" style="width: 9%">
                           <img alt="" src="../frames/images/EMPLOYEE/employeegrp.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../IR8A/Ir8aAppendixADetails.aspx" class="nav"><b>IR8A Appendix A</b></a>
                            <br />
                            <tt class="bodytxt">View & Print IR8A information (To be submitted to IRAS end of the
                                year).</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%} %>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View IRAS information"))
                          {%>
                           <td width="9%" align="left">
                            <img alt="" src="../frames/images/toolbar/formsIR8A.jpg" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="#" class="nav"><b>IR8A Appendix A</b></a>
                            <br />
                            <tt class="bodytxt">View & Print IR8A information 
                                <br />
                                (To be submitted to IRAS end of the
                                year).</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
