<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Employee.aspx.cs" Inherits="SMEPayroll.Management.Employee" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="SMEPayroll" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    <script language="JavaScript1.2">
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
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
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee Management</b></font>
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
        <div>
            <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employees"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/EMPLOYEE/B-Employee.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../employee/Employee.aspx" class="nav"><b>Employees </b></a>
                            <br />
                            <tt class="bodytxt">Manage your Employee's information. Add new Employee's
                                <br />
                                or update the existing employee's information.</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Groups"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/EMPLOYEE/employeegrp.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../employee/EmployeeGroups.aspx" class="nav"><b>Employee Leave Groups</b></a>
                            <br />
                            <tt class="bodytxt">Create Employee Groups for better management of
                                <br />
                                Employee Rights and categorising them into right groups.</tt></td>
                        <%}%>
                    </tr>
                </table>
            </center>
            
            <!-- -->
                     <br />
            <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employees"))
                          {%>
                        <td align="left" style="width: 9%;visibility:hidden">
                            <img alt="" src="../frames/images/EMPLOYEE/B-Employee.png" /></td>
                        <td style="width: 41%; text-align: left;visibility:hidden">
                       <%-- href="../employee/Employee.aspx"--%>
                            <a id="DocId" runat="server"  class="nav"><b>Document Management </b></a>
                            <br />
                            <tt class="bodytxt">Manage Documents</tt><br />
                        </td>
                        <%}
                          else
                          {%>
                        <td colspan="2">
                        </td>
                        <%}%>
                        <td colspan="2">
                        </td>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Employee Groups"))
                          {%>
                        <td width="9%" align="left" style="visibility:hidden">
                            <img alt="" src="../frames/images/EMPLOYEE/employeegrp.png" /></td>
                        <td style="width: 41%; text-align: left"  style="visibility:hidden">
                            
                            <br />
                          </td>
                        <%}%>
                    </tr>
                </table>
            </center>
            <!-- -->
        </div>
    </form>
</body>
</html>
