<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreferenceIcon.aspx.cs" Inherits="SMEPayroll.Invoice.PreferenceIcon" %>

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
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Project Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/PROJECTS/B-location.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/variables.aspx" class="nav"><b>Manage Variables  </b></a>
                            <br />
                            <tt class="bodytxt">Manage Variables.</tt></td>
                        <%}
                          if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/PaymentTerms.aspx" class="nav"><b>Manage Payment Terms</b></a><br />
                            <tt class="bodytxt">Manage Payment Terms.</tt><br />
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
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/PROJECTS/B-Subproject.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/ProjectAssignment.aspx" class="nav"><b> Project Assignment </b></a>
                            <br />
                            <tt class="bodytxt"> Project Assignment.</tt></td>
                        <%}
                        if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../frames/images/PROJECTS/B-Project.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/StartNumber.aspx" class="nav"><b>Manage Invoice Start Number</b></a><br />
                            <tt class="bodytxt">Manage Quotation,Order Start Number.</tt><br />
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
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../frames/images/PROJECTS/B-Subproject.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/ManageReportText.aspx" class="nav"><b>Manage Report Text</b></a>
                            <br />
                            <tt class="bodytxt">Manage Report Text.</tt></td>
                        <%}
                          if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td align="left" style="width: 9%; visibility:hidden">
                            <img alt="" src="../frames/images/PROJECTS/B-Employee Assignment.png" /></td>
                        <td style="width: 41%; text-align: left;visibility:hidden">
                            <a href="../Management/ProjectAssigned.aspx" class="nav"><b>Manage Report Text
                            </b></a>
                            <br />
                            <tt class="bodytxt">Manage Report Text.</tt><br />
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
            
        </div>
    </form>
</body>
</html>
