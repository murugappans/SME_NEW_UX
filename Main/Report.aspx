<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="SMEPayroll.Main.Report" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Reference Control="~/Main/APPTP.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scheduler</title>
</head>
<body>
    <form id="form1" runat="server">
             <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script language="javascript" type="text/javascript">
                  function SplitterLoaded(splitter, arg)   
                    {   
                        var pane = splitter.getPaneById('<%= CalendarPane.ClientID %>');
                        var height = pane.getContentElement().scrollHeight;
                        splitter.set_height(height);
                        pane.set_height(height);    
                    }   
                                
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
                                alert('hi there1');
                                
                                var prm = Sys.WebForms.PageRequestManager.getInstance();                                
                                if (args.get_postBackElement().id.indexOf('RadScheduler1') != -1) 
                                { 
                                    hideActiveToolTip(); 
                                    alert('hi there1');
                                } 
                                else
                                {
                                    alert('hi there');
                                    //var res = SMEPayroll.Main.Report.OnAjaxUpdate(sender, args);
                                }                               
                            } 
                            
                            function clientBeforeShow (sender, eventArgs)
                            {
                                 w = $telerik.$(window).width() / 2;
                                 h = $telerik.$(window).height() / 2;
                                
                                if ((sender._mouseX > w) && (sender._mouseY > h)) 
                                {
                                    sender.set_position(Telerik.Web.UI.ToolTipPosition.TopLeft);
                                    return;
                                }
                                if ((sender._mouseX < w) && (sender._mouseY > h)) 
                                {
                                    sender.set_position(Telerik.Web.UI.ToolTipPosition.TopRight);
                                    return;
                                }
                                if ((sender._mouseX > w) && (sender._mouseY < h))
                                {
                                    sender.set_position(Telerik.Web.UI.ToolTipPosition.BottomLeft);
                                    return;                                                    
                                }
                              sender.set_position(Telerik.Web.UI.ToolTipPosition.BottomRight);
                            }
                        //]]>
            </script>
        </telerik:RadCodeBlock>   
    <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager> 
    



              
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="500"  runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
            <asp:Image ID="Image1" Style="margin-top: 200px" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    
   
     
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadCalendar1">
                <UpdatedControls>
                     <telerik:AjaxUpdatedControl  ControlID="RadCalendar1"></telerik:AjaxUpdatedControl>
                     <telerik:AjaxUpdatedControl ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />   
                     <telerik:AjaxUpdatedControl ControlID="id1" LoadingPanelID="RadAjaxLoadingPanel1" /> 
                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radList">
                 <UpdatedControls>                     
                     <telerik:AjaxUpdatedControl  ControlID="radlistDept"></telerik:AjaxUpdatedControl>                     
                     <telerik:AjaxUpdatedControl  ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />  
                     <telerik:AjaxUpdatedControl ControlID="id1" LoadingPanelID="RadAjaxLoadingPanel1" />                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="radlistDept">
                 <UpdatedControls>                     
                     <telerik:AjaxUpdatedControl  ControlID="radlistDept"></telerik:AjaxUpdatedControl>                     
                     <telerik:AjaxUpdatedControl  ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />  
                     <telerik:AjaxUpdatedControl ControlID="id1" LoadingPanelID="RadAjaxLoadingPanel1" />                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="radioCulture">
                 <UpdatedControls>                     
                     <telerik:AjaxUpdatedControl  ControlID="radioCulture"></telerik:AjaxUpdatedControl>                     
                     <telerik:AjaxUpdatedControl  ControlID="RadScheduler1" LoadingPanelID="RadAjaxLoadingPanel1" />  
                     <telerik:AjaxUpdatedControl ControlID="id1" LoadingPanelID="RadAjaxLoadingPanel1" />                         
                </UpdatedControls>
            </telerik:AjaxSetting>
            
          <%--<telerik:AjaxSetting AjaxControlID="RadScheduler1">
                 <UpdatedControls>                     
                     <telerik:AjaxUpdatedControl  ControlID="RadScheduler1"></telerik:AjaxUpdatedControl>                     
                     <telerik:AjaxUpdatedControl  ControlID="RadToolTipManager2" LoadingPanelID="RadAjaxLoadingPanel1" />  
                     <telerik:AjaxUpdatedControl ControlID="id1" LoadingPanelID="RadAjaxLoadingPanel1" />                         
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    
    <asp:SqlDataSource ID="SchedulerDataSource" runat="server">
    </asp:SqlDataSource>         
    <asp:SqlDataSource ID="SqlDataSource2" runat="server">     
    </asp:SqlDataSource> 
    <asp:SqlDataSource ID="SqlDataSource1" runat="server">      
    </asp:SqlDataSource> 
    
    <telerik:RadSplitter ID="MainSplitter" runat="server"   Height="1000px" Width="100%"  OnClientLoaded="SplitterLoaded" >
                    <telerik:RadPane ID="CalendarPane" runat="server" Scrolling="None" Height="100%" Width="20%" >                        
                                <div style="padding:8px" >
                                     <telerik:RadCalendar runat="server"  Height="80px" ID="RadCalendar1" Width="100%" Skin="Office2007"  AutoPostBack="true" EnableMultiSelect="false" DayNameFormat="Shortest"  
                                         ShowRowHeaders="false" >                                     
                                    </telerik:RadCalendar>                              
                                </div>
                                <div style="padding:8px">
                                    <telerik:RadPanelBar    runat="server" ID="RadPanelBar1" Width="100%" Skin="Office2007"  >
                                        
                                        <Items>
                                            <telerik:RadPanelItem  Value="ctrlPanel1" id="ctrlPanel1" Expanded="False"  Width="100%"  >                                                
                                                <Items>
                                               <telerik:RadPanelItem Value="rpitem1" id="rpitem1">
                                               <ItemTemplate>
                                                    
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chk" runat="server" Text ="Select All" OnCheckedChanged="chk_CheckChanged" AutoPostBack="true" Checked ="true"  />
                                                   
                                                 </ItemTemplate>
                                                
                                                    
                                                
                                                </telerik:RadPanelItem>
                                                  
                                                    <telerik:RadPanelItem Value="ctrlPanel" Width="100%">
                                                    
                                                    
                                                         <ItemTemplate>
                                                                <telerik:RadListBox ID="radList"  Height="80px" runat="server" SelectionMode="Multiple"   AutoPostBack="True" Skin="Web20" Width="100%" >                                        
                                                                </telerik:RadListBox>                                                        
                                                         </ItemTemplate>
                                                    </telerik:RadPanelItem>                                              
                                                </Items>                                                    
                                            </telerik:RadPanelItem>                                        
                                        </Items>
                                    </telerik:RadPanelBar>                                    
                                </div>
                                <div style="padding:8px">
                                        <telerik:RadPanelBar    runat="server" ID="RadPanelBar2" Width="100%" Skin="Office2007" >
                                                <Items>
                                                    <telerik:RadPanelItem  Value="ctrlPanel5" Expanded="False"  Width="100%"  >                                                
                                                        <Items>
                                                        <telerik:RadPanelItem Value="rpitem2" id="rpitem2">
                                               <ItemTemplate>
                                                    
                                                    &nbsp;&nbsp;<asp:CheckBox ID="chk2" runat="server" Text ="Select All" OnCheckedChanged="chk2_CheckChanged" AutoPostBack="true" Checked ="true"  />
                                                   
                                                 </ItemTemplate>
                                                
                                                    
                                                
                                                </telerik:RadPanelItem>
                                                            <telerik:RadPanelItem Value="ctrlPanel6" Width="100%">
                                                                 <ItemTemplate>
                                                                           <telerik:RadListBox ID="radlistDept"   Height="80px" runat="server" SelectionMode="Multiple" AutoPostBack="true" Skin="Web20" Width="100%" >                                                                        
                                                                            </telerik:RadListBox>
                                                                 </ItemTemplate>
                                                            </telerik:RadPanelItem>                                              
                                                        </Items>                                                    
                                                    </telerik:RadPanelItem>                                        
                                                </Items>
                                        </telerik:RadPanelBar>
                                </div>
                                <div style="padding:8px">
                                   <telerik:RadPanelBar    runat="server" ID="RadPanelBar3" Width="100%" Skin="Office2007" >
                                                <Items>
                                                    <telerik:RadPanelItem  Value="ctrlPanel7" Expanded="False"  Width="100%"  >                                                
                                                        <Items>
                                                            <telerik:RadPanelItem Value="ctrlPanel8" Width="100%">
                                                                 <ItemTemplate>
                                                                        <telerik:RadListBox ID="radioCulture"   Height="50px"  runat="server" AutoPostBack="True"  CheckBoxes="true" SelectionMode="Single" Skin="Web20" Width="100%" >
                                                                            <Items>
                                                                                <telerik:RadListBoxItem  runat="server" Text="English(En)" Value="en-US" Checkable="true"    Checked="true"/>
                                                                                <telerik:RadListBoxItem runat="server" Text="Malay – Malaysia" Value="ms-MY" Checkable="true"       />                                                                                                                                
                                                                                <%--<telerik:RadListBoxItem  runat="server" Text="Chinese – Singapore"  Value="zh-SG" Checkable="true"  />
                                                                                <telerik:RadListBoxItem  runat="server" Text="Chinese – Taiwan"   Value="zh-TW" Checkable="true"      />--%>
                                                                                <telerik:RadListBoxItem  runat="server" Text="Thai – Thailand"  Value="th-TH" Checkable="true"      />                                            
                                                                                <telerik:RadListBoxItem runat="server" Text="Spanish – Spain"  Value="es-ES" Checkable="true"       />                                            
                                                                            </Items>                                        
                                                                        </telerik:RadListBox>
                                                          </ItemTemplate>
                                                            </telerik:RadPanelItem>                                              
                                                        </Items>                                                    
                                                    </telerik:RadPanelItem>                                        
                                                </Items>
                                      </telerik:RadPanelBar>
                                </div>
                    </telerik:RadPane>
                     <telerik:RadPane ID="RadPane1" runat="server" Scrolling="Y"  Width="80%" Height="100%" >
                                <asp:Label ID="id1"  runat="server" Visible="false"  ></asp:Label>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                                <telerik:RadScheduler ID="RadScheduler1" Height="800px"  runat="server"   
                                                          TimelineView-UserSelectable="false" Skin="Office2007" ReadOnly="true">
                                                          <AdvancedForm Modal="false"  />                                     
                                                            <TimelineView UserSelectable="false" />                                                                             
                                                </telerik:RadScheduler>  
                                                <asp:Label ID="Label1"  runat="server" Visible="false" ></asp:Label>                                                   
                                                
                                              <telerik:RadToolTipManager runat="server" ID="RadToolTipManager2" ToolTipZoneID="RadScheduler1"
                                                        Animation="FlyIn" Position="BottomRight"
                                                        HideEvent="LeaveToolTip" Text="Loading..." Width="300" Height="150" AutoTooltipify="true"
                                                        RelativeTo="Element" Skin="Office2007"           
                                                        OnAjaxUpdate="OnAjaxUpdate" >                        
                                               </telerik:RadToolTipManager>
                                                
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     </telerik:RadPane>                                           
    </telerik:RadSplitter>
    </form>
</body>
</html>
