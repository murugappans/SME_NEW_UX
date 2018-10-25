<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CertificateInformaton.aspx.cs"
    Inherits="SMEPayroll.Company.CertificateInformaton" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Certificate InFormation</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="left" >                                
                                <asp:Label ID="lbltotalemp" runat="server" CssClass="bodytxt"></asp:Label>
                            </td>
                            <td align="left">                            
                                <asp:Label ID="lbldbemp" runat="server" CssClass="bodytxt"></asp:Label>
                            </td>
                            <td align="left">                                
                                <asp:Label ID="lbldiff" runat="server" CssClass="bodytxt"></asp:Label>
                            </td>
                            <td align="right" >
                                <asp:Button ID="bnrefresh" Text="Refresh" runat="server" style="width: 80px; height: 15px;visibility:hidden" />                                
                             </td>    
                        </tr>
                    </table>
                </td>
            </tr>
        </table> 
        <div>
            <input type="hidden" id="flag" runat="server" />
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%" border="0">                
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGridCertification" AllowPaging="true" PageSize="15" runat="server" ExportSettings-OpenInNewWindow="true"
                        GridLines="None" Skin="Outlook" Width="100%" AllowSorting="true" ExportSettings-ExportOnlyData="true" >
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                
                            </ClientSettings>
                        </radG:RadGrid></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
