<%@ Control Language="C#" AutoEventWireup="true" Codebehind="TopLeftMenu.ascx.cs"
    Inherits="IRAS.TopLeftMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div id="div1" style="height: 10" align="left" runat="server">
    <table width="100%" height="10" style="height: '10px'; background-image: url(../frames/images/TOOLBAR/backsTop.jpg)">
        <tr>
            <td style="display:none">
                <a href="#" visible="false">
                    <img visible="false" id="imgHome" style="vertical-align: top" alt="" src="../frames/images/toolbar/S - Home.png"
                        border="0" onclick="javascript:homeclick();" runat="server" /></a></td>
            <td style="display:none">
            </td>
            <td style="display:none">
                <a href="#">
                    <img visible="false" id="imgLogin" style="vertical-align: middle" alt="" src="../frames/images/toolbar/S - Login info.png"
                        border="0" runat="server" /></a></td>
            <td style="display:none">
            </td>
            <td>
                <a href="#"  onclick="javascript:logoutclick();">
                    <img id="imgLogOut" style="vertical-align: top" alt="" src="../frames/images/toolbar/S - Logout.png"
                        border="0" onclick="javascript:logoutclick();" runat="server" />&nbsp;Logout</a></td>
            <td>
            </td>
        </tr>
    </table>
</div>
<telerik:RadToolTipManager ID="RadToolTipManager11" runat="server" OnAjaxUpdate="RadToolTipManager11_AjaxUpdate">
    <TargetControls>
        <telerik:ToolTipTargetControl TargetControlID="imgHome" />
    </TargetControls>
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ID="RadToolTipManager1" runat="server" OnAjaxUpdate="RadToolTipManager1imgLogin_AjaxUpdate">
    <TargetControls>
        <telerik:ToolTipTargetControl TargetControlID="imgLogin" />
    </TargetControls>
</telerik:RadToolTipManager>
<telerik:RadToolTipManager ID="RadToolTipManager2" runat="server" OnAjaxUpdate="RadToolTipManager1imgLogOut_AjaxUpdate">
    <TargetControls>
        <telerik:ToolTipTargetControl TargetControlID="imgLogOut" />
    </TargetControls>
</telerik:RadToolTipManager>
