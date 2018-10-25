<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LicenseUpdate.aspx.cs" Inherits="SMEPayroll.Management.LicenseUpdate" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
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
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>License Update</b></font>
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
               
            </tr>
        </table>

        <div>
            <table cellpadding="1" cellspacing="20" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td>
                       
                          <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                             Text="Update License" />&nbsp;&nbsp;
                          <asp:Label ID="lblMsg" runat="server"></asp:Label>
                     </td>
                </tr>
            </table>
       
        </div>
    </form>
</body>
</html>
