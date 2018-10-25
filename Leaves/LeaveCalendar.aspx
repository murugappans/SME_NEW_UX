<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LeaveCalendar.aspx.cs"
    Inherits="SMEPayroll.Leaves.LeaveCalendar" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/tr/xhtml11/dtd/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    
    
</head>
<body class="BODY1">
    <form id="Form1" method="post" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager runat="Server" ID="RadScriptManager1" />

        <script type="text/javascript">

            /* Firefox resize scrollable content */
     function hideScrollableArea(sender, eventArgs) {
     if ($telerik.isFirefox)
     $telerik.$('.rsContentScrollArea').css('overflow', 'hidden');
     }
     function showScrollableArea(sender, eventArgs) {
     if ($telerik.isFirefox)
         $telerik.$('.rsContentScrollArea').css('overflow', 'auto');
     }
     
        </script>
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
        <script type="text/javascript">
        //<![CDATA[
            function hideActiveToolTip()
            {            
                var tooltip = Telerik.Web.UI.RadToolTip.getCurrent();
                if (tooltip)
                {
                    tooltip.hide(); 
                }
            }
            
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequestHandler);
            function beginRequestHandler(sender, args)
            {
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (args.get_postBackElement().id.indexOf('RadScheduler1') != -1) 
                { 
                    hideActiveToolTip(); 
                } 
            } 
        //]]>
        </script>

        <telerik:RadAjaxManager runat="Server" ID="RadAjaxManager1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="PanelBar">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadScheduler1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadCalendar1" />
                        <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Outlook" />
        <div id="LeavePanel" class="Leave-panel" >
            <div class="title">
                Leave Calendar</div>
            <telerik:RadSplitter runat="server" ID="RadSplitter1" PanesBorderSize="0" Width="1013px"
                Height="536px" Skin="Outlook">
                <telerik:RadPane runat="Server" ID="leftPane" Width="240px" MinWidth="240" MaxWidth="300"
                    Scrolling="None" OnClientBeforeResize="hideScrollableArea" OnClientResized="showScrollableArea"
                    OnClientBeforeExpand="hideScrollableArea" OnClientExpanded="showScrollableArea"
                    OnClientBeforeCollapse="hideScrollableArea" OnClientCollapsed="showScrollableArea">
                    <telerik:RadPanelBar runat="server" Skin="Outlook" ID="PanelBar" Width="100%">
                        <Items>
                            <telerik:RadPanelItem runat="server" Text="Department" PreventCollapse="false" Expanded="false">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="ChkDepartments">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                            <telerik:RadPanelItem runat="server" Text="Leave Types" PreventCollapse="true" Expanded="true">
                                <Items>
                                    <telerik:RadPanelItem runat="server" Value="ChkLeaves">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </telerik:RadPanelItem>
                                </Items>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelBar>
                </telerik:RadPane>
                <telerik:RadSplitBar runat="server" ID="RadSplitBar2" CollapseMode="Forward" />
                <telerik:RadPane runat="Server" ID="rightPane" Scrolling="None" Width="490px" EnableEmbeddedBaseStylesheet="False"
                    EnableEmbeddedSkins="False" Index="2" Skin="">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                        <ContentTemplate>
                            <telerik:RadScheduler runat="server" ID="RadScheduler1" Skin="Outlook" EnableEmbeddedSkins="True"
                                ShowFooter="false" SelectedDate="2009-02-02" DayStartTime="09:00:00" DayEndTime="19:00:00"
                                TimeZoneOffset="03:00:00" OverflowBehavior="Scroll" SelectedView="MonthView"
                                Height="551px" TimelineView-UserSelectable="false" onappointmentcreated="RadScheduler1_AppointmentCreated"
                                OnAppointmentDataBound="RadScheduler1_AppointmentDataBound" ShowAllDayRow="False"
                                AllowDelete="False" AllowEdit="False" AllowInsert="False" TimeLabelRowSpan="10"
                                ReadOnly="True" HoursPanelTimeFormat="tt">
                                <ResourceStyles>
                                    <telerik:ResourceStyleMapping Type="Department" Text="Shipyard" ApplyCssClass="rsCategoryGreen"
                                        Key="" />
                                    <telerik:ResourceStyleMapping Type="Department" Text="Director" ApplyCssClass="rsCategoryRed"
                                        Key="" />
                                    <telerik:ResourceStyleMapping Type="Leave" Text="Hospitalisation Leave" ApplyCssClass="rsCategoryOrange"
                                        Key="" />
                                </ResourceStyles>
                                <TimelineView UserSelectable="False" NumberOfSlots="2" />
                            </telerik:RadScheduler>
                            <telerik:RadToolTipManager runat="server" ID="RadToolTipManager2" ToolTipZoneID="RadScheduler1"
                                Skin="WebBlue" Animation="None" Position="BottomRight" HideEvent="LeaveToolTip"
                                Text="Loading..." Width="300" Height="150" AutoTooltipify="true" RelativeTo="Element"
                                OnAjaxUpdate="RadToolTipManager2_AjaxUpdate" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </telerik:RadPane>
            </telerik:RadSplitter>
        </div>
    </form>
</body>
</html>
