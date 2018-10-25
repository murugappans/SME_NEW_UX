<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="SMEPayroll.Invoice.Invoice" %>
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Invoicing Management</b></font>
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
        <br />
        <center>
                <table border="0" cellpadding="0" cellspacing="5" style="border-collapse: collapse"
                    width="90%">
                    <tr>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Clients"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../Frames/Images/INVOICE/B-client.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/ClientManagement.aspx" class="nav"><b>Manage Clients</b></a><br />
                            <tt class="bodytxt">Manage Clients.</tt><br />
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Quotation"))
                          {%>
                        <td width="9%" align="left" >
                            <img alt="" src="../Frames/Images/INVOICE/B-Quotation.png"  /></td>
                        <td style="width: 41%; text-align: left;">
                            <a href="../Invoice/Quotation.aspx" class="nav"><b>Manage Quotation</b></a>
                            <br />
                            <tt class="bodytxt">Add/Edit/Delete Quotation information.</tt></td>
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
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Order"))
                          {%>
                         <td width="9%" align="left">
                           <img alt="" src="../Frames/Images/INVOICE/B-Order.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/Order.aspx" class="nav"><b>Manage Order</b></a>
                            <br />
                            <tt class="bodytxt">Manage Order.</tt></td>
                        </td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Invoice"))
                          {%>
                        <td align="left" style="width: 9%">
                            <img alt="" src="../Frames/Images/INVOICE/B-Invoice.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/Invoice_Main.aspx" class="nav"><b>Manage Invoice</b></a><br />
                            <tt class="bodytxt">Manage Invoice.</tt><br />
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
                        
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Manage Preference"))
                          {%>
                        <td width="9%" align="left">
                            <img alt="" src="../Frames/Images/INVOICE/B-Preference.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/PreferenceIcon.aspx"class="nav"><b>Manage Preference</b></a>
                            <br />
                            <tt class="bodytxt">Manage Preference.</tt></td>
                        <%}%>
                        <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Payment Management"))
                          {%>
                        <td width="9%" align="left">
                           <img alt="" src="../Frames/Images/INVOICE/client.png" /></td>
                        <td style="width: 41%; text-align: left">
                            <a href="../Invoice/ProjectAssignment.aspx" class="nav"><b>Project Assignment</b></a>
                            <br />
                            <tt class="bodytxt">Project Assignment.</tt></td>
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
        
        </div>
    </form>
</body>
</html>

