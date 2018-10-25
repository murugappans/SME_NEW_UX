<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DashBoard.aspx.cs" Inherits="SMEPayroll.Main.DashBoard" MasterPageFile="~/Master.Master" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Import Namespace="SMEPayroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />
    <style type="text/css">
    div.RadToolBar .rtbOuter {
        display: block !important;
    }
     
    .RadToolBar .rtbInner {
        padding: 0 !important;
    }
     
    .RadToolBar .rtbItem {
        height: 25px !important;
    }
     
    .RadToolBar .rtbIcon {
        padding: 0 !important;
    }
    .smallToolBar
    {
     vertical-align:middle;
    }
</style>

    <script type="text/javascript"> 
        function OpenModalWindow()  
    {  
        window.radopen(null,"MYMODALWINDOW");  
    }  
      
function CloseModalWindow()
{  
        var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");          
        win.Close();  
} 
 
  function ShowInsertForm(a)
  {  
     var NoOfRecords=0;
     var radio = document.getElementsByName('radNoOfdays');
            for (var j = 0; j < radio.length; j++)
            {
                if (radio[j].checked)
                    NoOfRecords=radio[j].value;
            }
         
        a=a+'&nof='+NoOfRecords;
     window.radopen("Grid.aspx"+"?id="+a, "EditGrid",800,400,500,500);
     
     
     return false;
  }

    </script>
</asp:Content>
<%--    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>
   <script type="text/ecmascript">
     function loadmenu()
        {            
            window.parent.frames[0].location = "../frames/right.aspx";
          return false;
        }
    
    </script>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" border="0" background="../frames/images/toolbar/backs.jpg">
                        <tr>
                            <td style="height: 25px;" width="20%">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Alerts & Reminders</b></font>
                            </td>
                            <td style="height: 30px; font-family: Tahoma; font-size: 12px; color: White;" align="left">
                                <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                    <tr style="visibility:hidden;">
                                        <td width="40%" align="right">
                                            <b>Reminders For Number Of Days :</b></td>
                                        <td style="height: 30px; font-family: Tahoma; font-size: 12px; color: White; width: 200px;"
                                            align="left">
                                            <asp:RadioButtonList Font-Bold="true" RepeatDirection="Horizontal" AutoPostBack="true"
                                                ID="radNoOfdays" runat="server">
                                                <asp:ListItem Selected="true" Text="30" Value="30"></asp:ListItem>
                                                <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                                <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                                <asp:ListItem Text="120" Value="120"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--<tr bgcolor="#E5E5E5">
                            <td style="height: 30px; font-family: Tahoma; font-size: 12px;" width="40%" align="right">
                                <b>Reminders For Number Of Days :</b>
                            </td>
                            <td style="height: 30px; font-family: Tahoma; font-size: 12px;" width="60%" align="left">
                                <asp:RadioButtonList Font-Bold="true" RepeatDirection="Horizontal" AutoPostBack="true"
                                    ID="radNoOfdays" runat="server">
                                    <asp:ListItem Selected="true" Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="60" Value="60"></asp:ListItem>
                                    <asp:ListItem Text="90" Value="90"></asp:ListItem>
                                    <asp:ListItem Text="120" Value="120"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
        </table>
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <br />

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <!-- grid -->
        <table cellpadding="0" cellspacing="0" border="0" width="90%" align="center" >
            <tr>
                <td width="45%" valign="top" align="left">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                                 {%>
                                <img alt="Employee" src="../frames/images/home/B-reminders.png" />&nbsp;<font face="verdana"
                                    size="2">Employee On Leave<%--<a href="..\leaves\leaveCalendar.aspx">Leave Calendar</a>--%></font><hr />
                          
                                <%}%>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left" valign="top">
                                      <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                          {%>
                            <!-- -->
                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

                                <script type="text/javascript">
                                function getOuterHTML(obj) {
                                    if (typeof (obj.outerHTML) == "undefined") {
                                        var divWrapper = document.createElement("div");
                                        var copyOb = obj.cloneNode(true);
                                        divWrapper.appendChild(copyOb);
                                        return divWrapper.innerHTML
                                    }
                                    else
                                        return obj.outerHTML;
                                }

                                function PrintRadGrid6(sender, args) {
                                    
                                   if (args.get_item().get_text() == 'Print')
                                     {

                                        var previewWnd = window.open('about:blank', '', '', false);
                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid6.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid6.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid6.ClientID %>').get_element()) + "</body></html>";
                                        previewWnd.document.open();
                                        previewWnd.document.write(htmlcontent);
                                        previewWnd.document.close();
                                        previewWnd.print();
                                        previewWnd.close();
                                    }
                                }
                                
                            
                             
                            
                           
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="Radtoolbar1" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar1_ButtonClick"
                                OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid6" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid6_GridExporting" AutoGenerateColumns="true"
                                ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed" PageSize="5">
                                    <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="10"
                                            HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                            </td>
                        </tr>
                        <tr>
                          <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View  Employee Leaves"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(70);">...More</a>
                            <%}%>
                        </td>
                        </tr>
                          <!--Employee Birthday -->
                        <tr>
                            <td width="100%" align="left">
                              <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                              {%>
                                <img alt="" src="../frames/images/home/B-reminders.png">
                                <font face="verdana" size="2">Employee Birthday</font>
                                <hr color="lightgrey" width="300" align="left">
                                <%}%>
                           </td>
                        </tr>
                        <tr>
                             <td width="100%" align="left" valign="top">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                          {%>
                            <!-- -->
                            <radG:RadCodeBlock ID="RadCodeBlock6" runat="server">

                                <script type="text/javascript">
                                                function getOuterHTML(obj) 
                                                {
                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                        var divWrapper = document.createElement("div");
                                                        var copyOb = obj.cloneNode(true);
                                                        divWrapper.appendChild(copyOb);
                                                        return divWrapper.innerHTML
                                                    }
                                                    else
                                                        return obj.outerHTML;
                                                }

                                                function PrintRadGrid6(sender, args) 
                                                {
                                                    
                                                   if (args.get_item().get_text() == 'Print')
                                                     {

                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid7.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid7.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid6.ClientID %>').get_element()) + "</body></html>";
                                                        previewWnd.document.open();
                                                        previewWnd.document.write(htmlcontent);
                                                        previewWnd.document.close();
                                                        previewWnd.print();
                                                        previewWnd.close();
                                                    }
                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="RadtoolbarBD" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar7_ButtonClick"
                                OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="lblBd" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid7" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid7_GridExporting" PagerStyle-Visible="true"
                                OnItemDataBound="RadGrid3_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                   
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="16"
                                            HeaderStyle-Width="16px">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Birthday Dates"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(80);">...More in</a>
                            <%}%>
                        </td> 
                        </tr>
                        <!--Passes Expiring -->
                        <tr>
                           <td align="left" width="100%">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                              {%>
                                <img alt="Employee" src="../frames/images/home/B-reminders.png" />
                                <font face="verdana" size="2">Passes Expiring</font>
                                <hr align="left" width="300" color="lightgrey" />
                                <%}%>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left" valign="top">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                              {%>
                                <!-- -->
                                <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

                                    <script type="text/javascript">
                                function getOuterHTML(obj) {
                                    if (typeof (obj.outerHTML) == "undefined") {
                                        var divWrapper = document.createElement("div");
                                        var copyOb = obj.cloneNode(true);
                                        divWrapper.appendChild(copyOb);
                                        return divWrapper.innerHTML
                                    }
                                    else
                                        return obj.outerHTML;
                                }

                                function PrintRadGrid1(sender, args) {

                                   if (args.get_item().get_text() == 'Print')
                                     {

                                        var previewWnd = window.open('about:blank', '', '', false);
                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid1.ClientID %>').get_element()) + "</body></html>";
                                        previewWnd.document.open();
                                        previewWnd.document.write(htmlcontent);
                                        previewWnd.document.close();
                                        previewWnd.print();
                                        previewWnd.close();
                                    }
                                }
                                
                                                                                      
                                    </script>

                                </radG:RadCodeBlock>
                                <radG:RadToolBar Visible="true" ID="Radtoolbar11" CssClass="smallToolBar" Height="100%"
                                    runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid1">
                                    <Items>
                                        <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                        <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                            Height="24px" Text="Print" ToolTip="Print">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                            Height="24px" Text="Excel" ToolTip="Excel">
                                        </radG:RadToolBarButton>
                                        <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" Text="Count1">
                                            <ItemTemplate>
                                                <div>
                                                    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                        height: 30px;">
                                                        <tr>
                                                            <td>
                                                                <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                            Height="24px" Text="Calender" ToolTip="Graph">
                                        </radG:RadToolBarButton>
                                    </Items>
                                </radG:RadToolBar>
                                <!-- tool bar end -->
                                <!-- -->
                                <radG:RadGrid ID="RadGrid1" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True" PageSize="2"
                                    OnGridExporting="RadGrid1_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <MasterTableView TableLayout="Fixed" >
                                        <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <ExpandCollapseColumn Visible="False">
                                            <HeaderStyle Width="19px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <Columns>
                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="12">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="25px" />
                                            </radG:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                   
                                    
                               
                                </radG:RadGrid>
                                <%}%>
                            </td>
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View WP Expiry"))
                              {%>
                                <a href="#" onclick="return ShowInsertForm(10);">...More</a>
                                <%}%>
                            </td> 
                        </tr>
                        <!-- CSOC Expiring -->
                        <tr>
                           <td width="100%" align="left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                          {%>
                            <img alt="" src="../frames/images/home/B-reminders.png">
                            <font face="verdana" size="2">CSOC Expiring</font>
                            <hr color="lightgrey" width="300" align="left">
                            <%}%>
                           </td> 
                        </tr>
                        <tr>
                             <td width="100%" align="left" valign="top">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                          {%>
                            <!-- -->
                            <radG:RadCodeBlock ID="RadCodeBlock88" runat="server">

                                <script type="text/javascript">
                                            function getOuterHTML(obj) 
                                            {
                                                if (typeof (obj.outerHTML) == "undefined") {
                                                    var divWrapper = document.createElement("div");
                                                    var copyOb = obj.cloneNode(true);
                                                    divWrapper.appendChild(copyOb);
                                                    return divWrapper.innerHTML
                                                }
                                                else
                                                    return obj.outerHTML;
                                            }

                                            function PrintRadGrid6(sender, args) 
                                            {
                                                
                                               if (args.get_item().get_text() == 'Print')
                                                 {

                                                    var previewWnd = window.open('about:blank', '', '', false);
                                                    var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                    var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                    var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                    styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                    var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid4.ClientID %>').get_element()) + "</body></html>";
                                                    previewWnd.document.open();
                                                    previewWnd.document.write(htmlcontent);
                                                    previewWnd.document.close();
                                                    previewWnd.print();
                                                    previewWnd.close();
                                                }
                                            }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="RadtoolbarCs" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid4" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid4_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="14">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View CSOC Expiry"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(50);">...More</a>
                            <%}%>
                        </td>
                        </tr>
                      
                        <!--Other Certificate Expiry -->
                        <tr>
                           <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                              {%>
                                <img alt="" src="../frames/images/home/B-reminders.png">
                                <font face="verdana" size="2">Other Certificates Expiring</font>
                                <hr color="lightgrey" width="300" align="left">
                                <%}%>
                          </td>
                        </tr>
                        <tr>
                          <td width="100%" align="left" valign="top">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                          {%>
                            <!--Tool bar -->
                            <radG:RadCodeBlock ID="RadCodeBlock63343" runat="server">

                                <script type="text/javascript">
                                                                function getOuterHTML(obj) 
                                                                {
                                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                                        var divWrapper = document.createElement("div");
                                                                        var copyOb = obj.cloneNode(true);
                                                                        divWrapper.appendChild(copyOb);
                                                                        return divWrapper.innerHTML
                                                                    }
                                                                    else
                                                                        return obj.outerHTML;
                                                                }

                                                                function PrintRadGrid6(sender, args) 
                                                                {
                                                                    
                                                                   if (args.get_item().get_text() == 'Print')
                                                                     {

                                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid9.ClientID %>').get_element()) + "</body></html>";
                                                                        previewWnd.document.open();
                                                                        previewWnd.document.write(htmlcontent);
                                                                        previewWnd.document.close();
                                                                        previewWnd.print();
                                                                        previewWnd.close();
                                                                    }
                                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="Radtoolbarothcp" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid9" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid9_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>  
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(100);">...More</a>
                            <%}%>
                        </td>
                        </tr>
                        <!--Backup Information -->
                       <tr>
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                          {%>
                        <td width="5%" align="left">
                            <div>
                                <img alt="" src="../frames/images/home/B-reminders.png">
                                <font face="verdana" size="2">Backup Information</font>
                                <hr color="lightgrey" width="300" align="left">
                            </div>
                            <div style="font-family: Verdana; font-size: 8pt">
                                <asp:Label ID="lblbkp" runat="server">                             
                                </asp:Label>
                            </div>
                        </td>
                           <%}%>
                       </tr>
                       <tr>
                             <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                          {%>
                        <td width="5%" align="left" valign="top">
                            <!--Tool bar -->
                            <radG:RadCodeBlock ID="RadCodeBlock8" runat="server">

                                <script type="text/javascript">
                                                                function getOuterHTML(obj) 
                                                                {
                                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                                        var divWrapper = document.createElement("div");
                                                                        var copyOb = obj.cloneNode(true);
                                                                        divWrapper.appendChild(copyOb);
                                                                        return divWrapper.innerHTML
                                                                    }
                                                                    else
                                                                        return obj.outerHTML;
                                                                }

                                                                function PrintRadGrid12(sender, args) 
                                                                {
                                                                    
                                                                   if (args.get_item().get_text() == 'Print')
                                                                     {

                                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid12.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid12.Skin)) %>';
                                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid12.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid12.ClientID %>').get_element()) + "</body></html>";
                                                                        previewWnd.document.open();
                                                                        previewWnd.document.write(htmlcontent);
                                                                        previewWnd.document.close();
                                                                        previewWnd.print();
                                                                        previewWnd.close();
                                                                    }
                                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="RadtoolbarBackUp" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid12">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Lblbackup" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <radG:RadGrid ID="RadGrid12" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid12_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                AllowSorting="true" OnItemDataBound="RadGrid12_ItemDataBound" ItemStyle-Wrap="false"
                                AlternatingItemStyle-Wrap="false" Height="125px">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="20">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                        </td>
                        <%}%>  
                       </tr>
                       <tr>
                         <%if (Utility.AllowedAction1(Session["Username"].ToString(), "BACKUP LOGS"))
                          {%>
                        <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <a href="#" onclick="return ShowInsertForm(150);">...More</a>
                        </td>
                        <td>
                        </td>
                        <%}%>
                       </tr>
                    </table>
                </td>
                <td width="5%" valign="top">
                </td>
                <!-- second td -->
                <td width="45%" valign="top" align="left">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                      <%--  Pending leave request--%>
                        
                        <tr>
                           <td width="100%" align="left">
                             <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                              {%>
                                <img alt="Users" src="../frames/images/home/B-reminders.png" />&nbsp;<font face="verdana"
                                    size="2">Pending Leave Request</font><hr color="lightgrey" width="300" align="left">
                              <%}%>
                           </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left" valign="top">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                          {%>
                            <!-- -->
                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
                                function getOuterHTML(obj) {
                                    if (typeof (obj.outerHTML) == "undefined") {
                                        var divWrapper = document.createElement("div");
                                        var copyOb = obj.cloneNode(true);
                                        divWrapper.appendChild(copyOb);
                                        return divWrapper.innerHTML
                                    }
                                    else
                                        return obj.outerHTML;
                                }

                                function PrintRadGrid(sender, args) {

                                   if (args.get_item().get_text() == 'Print')
                                     {

                                        var previewWnd = window.open('about:blank', '', '', false);
                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid5.ClientID %>').get_element()) + "</body></html>";
                                        previewWnd.document.open();
                                        previewWnd.document.write(htmlcontent);
                                        previewWnd.document.close();
                                        previewWnd.print();
                                        previewWnd.close();
                                    }
                                }
                                
                                                                                      
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="tbRecord5" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid5" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid5_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound">
                                <MasterTableView TableLayout="fixed" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                    <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <ItemStyle Height="20px" />
                                    <AlternatingItemStyle Height="20px" />
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="11"
                                            HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <%--   <ClientSettings>
                                    <Resizing AllowColumnResize="true" />
                                </ClientSettings>--%>
                            </radG:RadGrid>
                            <%}%>
                        </td>
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Pending Leaves"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(20);">...More</a>
                            <%}%>
                        </td>
                        </tr>
                         <!-- Probation Period Expiring -->
                        <tr>
                           <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                              {%>
                                <img alt="Users" src="../frames/images/home/B-reminders.png" />
                                <font face="verdana" size="2">Probation Period Expiring</font>
                                <hr align="left" width="300" color="lightgrey" />
                                <%}%>
                          </td>
                        </tr>
                        <tr>
                            <td width="100%" align="left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                          {%>
                            <!-- -->
                            <radG:RadCodeBlock ID="RadCodeBlock699" runat="server">

                                <script type="text/javascript">
                                                                function getOuterHTML(obj) 
                                                                {
                                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                                        var divWrapper = document.createElement("div");
                                                                        var copyOb = obj.cloneNode(true);
                                                                        divWrapper.appendChild(copyOb);
                                                                        return divWrapper.innerHTML
                                                                    }
                                                                    else
                                                                        return obj.outerHTML;
                                                                }

                                                                function PrintRadGrid6(sender, args) 
                                                                {
                                                                    
                                                                   if (args.get_item().get_text() == 'Print')
                                                                     {

                                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid8.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid8.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid8.ClientID %>').get_element()) + "</body></html>";
                                                                        previewWnd.document.open();
                                                                        previewWnd.document.write(htmlcontent);
                                                                        previewWnd.document.close();
                                                                        previewWnd.print();
                                                                        previewWnd.close();
                                                                    }
                                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="Radtoolbarprb" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid8" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid8_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="17">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Probation Expiry"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(90);">...More</a>
                            <%}%>
                        </td>  
                        </tr>
                  <%--  Licence Expire--%>
                   <tr>
                           <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                              {%>
                                <img alt="" src="../frames/images/home/B-reminders.png">
                                <font face="verdana" size="2">License Expiring</font>
                                <hr color="lightgrey" width="300" align="left">
                                <%}%>
                          </td>
                        </tr>
                        <tr>
                          <td width="100%" align="left" valign="top">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                          {%>
                            <!--Tool bar -->
                            <radG:RadCodeBlock ID="RadCodeBlock9" runat="server">

                                <script type="text/javascript">
                                                                function getOuterHTML(obj) 
                                                                {
                                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                                        var divWrapper = document.createElement("div");
                                                                        var copyOb = obj.cloneNode(true);
                                                                        divWrapper.appendChild(copyOb);
                                                                        return divWrapper.innerHTML
                                                                    }
                                                                    else
                                                                        return obj.outerHTML;
                                                                }

                                                                function PrintRadGrid6(sender, args) 
                                                                {
                                                                    
                                                                   if (args.get_item().get_text() == 'Print')
                                                                     {

                                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid9.ClientID %>').get_element()) + "</body></html>";
                                                                        previewWnd.document.open();
                                                                        previewWnd.document.write(htmlcontent);
                                                                        previewWnd.document.close();
                                                                        previewWnd.print();
                                                                        previewWnd.close();
                                                                    }
                                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="Radtoolbar3" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar3_ButtonClick">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label11" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;
                                                            </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid11" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid11_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="18">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>  
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Other Certificate Expiry"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(160);">...More</a><%--Senthil ---%>
                            <%}%>
                        </td>
                        </tr>
                    
                    
                    
                    
                    
                
                        <!-- Passport Expiring-->
                        <tr>
                            <td align="left" width="100%">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                              {%>
                                <img alt="Users" src="../frames/images/home/B-reminders.png" />
                                <font face="verdana" size="2">Passport Expiring </font>
                                <hr align="left" width="300" color="lightgrey" />
                                <%}%>
                            </td> 
                        </tr>
                        <tr>
                             <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                              {%>
                                <!-- -->
                                <radG:RadCodeBlock ID="RadCodeBlock5" runat="server">

                                    <script type="text/javascript">
                                function getOuterHTML(obj) {
                                    if (typeof (obj.outerHTML) == "undefined") {
                                        var divWrapper = document.createElement("div");
                                        var copyOb = obj.cloneNode(true);
                                        divWrapper.appendChild(copyOb);
                                        return divWrapper.innerHTML
                                    }
                                    else
                                        return obj.outerHTML;
                                }

                                function PrintRadGrid2(sender, args) {

                                   if (args.get_item().get_text() == 'Print')
                                     {

                                        var previewWnd = window.open('about:blank', '', '', false);
                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid2.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid2.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid2.ClientID %>').get_element()) + "</body></html>";
                                        previewWnd.document.open();
                                        previewWnd.document.write(htmlcontent);
                                        previewWnd.document.close();
                                        previewWnd.print();
                                        previewWnd.close();
                                    }
                                }
                                
                                                                                      
                                    </script>

                                </radG:RadCodeBlock>
                                <radG:RadToolBar Visible="true" ID="Radtoolbar2" CssClass="smallToolBar" runat="server"
                                    Width="100%" Skin="Office2007" UseFadeEffect="True" OnButtonClick="Radtoolbar2_ButtonClick"
                                    OnClientButtonClicking="PrintRadGrid2">
                                    <Items>
                                        <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                        <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                            Height="24px" Text="Print" ToolTip="Print">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                            Height="24px" Text="Excel" ToolTip="Excel">
                                        </radG:RadToolBarButton>
                                        <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" Text="Count">
                                            <ItemTemplate>
                                                <div>
                                                    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                        height: 30px;">
                                                        <tr>
                                                            <td>
                                                                <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton IsSeparator="true">
                                        </radG:RadToolBarButton>
                                        <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                            Height="24px" Text="Calender" ToolTip="Calender">
                                        </radG:RadToolBarButton>
                                    </Items>
                                </radG:RadToolBar>
                                <!-- tool bar end -->
                                <!-- -->
                                <radG:RadGrid ID="RadGrid2" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                    OnGridExporting="RadGrid2_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                    ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                    <MasterTableView TableLayout="Fixed">
                                        <PagerStyle Mode="Advanced" AlwaysVisible="true" Visible="false" />
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <ExpandCollapseColumn Visible="False">
                                            <HeaderStyle Width="19px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                        <Columns>
                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="13">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                                </ItemTemplate>
                                                <HeaderStyle Width="25px" />
                                            </radG:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </radG:RadGrid>
                                <%}%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="font-family: Verdana; font-size: 8pt">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Passport Expiry"))
                              {%>
                                <a href="#" onclick="return ShowInsertForm(30);">...More</a>
                                <%}%>
                            </td>
                        </tr>
                        <!-- Insurance Expiry -->
                        <tr>
                           <td width="100%" align="left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                          {%>
                            <img alt="Users" src="../frames/images/home/B-reminders.png" />
                            <font face="verdana" size="2">
                                <nowrap>
                                Insurance Expiring</font>
                            <hr align="left" width="300" color="lightgrey" />
                            <%}%>
                          </td> 
                        </tr>
                        <tr>
                           <td width="100%" align="left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                          {%>
                            <radG:RadCodeBlock ID="RadCodeBlock99" runat="server">

                                <script type="text/javascript">
                                            function getOuterHTML(obj) 
                                            {
                                                if (typeof (obj.outerHTML) == "undefined") {
                                                    var divWrapper = document.createElement("div");
                                                    var copyOb = obj.cloneNode(true);
                                                    divWrapper.appendChild(copyOb);
                                                    return divWrapper.innerHTML
                                                }
                                                else
                                                    return obj.outerHTML;
                                            }

                                            function PrintRadGrid6(sender, args) 
                                            {
                                                
                                               if (args.get_item().get_text() == 'Print')
                                                 {

                                                    var previewWnd = window.open('about:blank', '', '', false);
                                                    var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid3.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                    var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid3.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                    var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                    styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                    var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid3.ClientID %>').get_element()) + "</body></html>";
                                                    previewWnd.document.open();
                                                    previewWnd.document.write(htmlcontent);
                                                    previewWnd.document.close();
                                                    previewWnd.print();
                                                    previewWnd.close();
                                                }
                                            }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="RadtoolbarIN" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Countd">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png"
                                        Height="24px" Text="Calender" ToolTip="Calender">
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <!-- -->
                            <radG:RadGrid ID="RadGrid3" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid3_GridExporting" OnItemDataBound="RadGrid2_ItemDataBound"
                                ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="Fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="15">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td> 
                        </tr>
                        <tr>
                           <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "View Insurance Expiry"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(60);">...More</a>
                            <%}%>
                        </td>    
                        </tr>
                       
                        <!-- Year Of Service -->
                        <tr>
                            <td width="100%" align="left">
                                <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                              {%>
                                <img alt="Users" src="../frames/images/home/B-reminders.png" />
                                <font face="verdana" size="2">Number Of Service Years Completed </font>
                                <hr align="left" width="300" color="lightgrey" />
                                <%}%>
                           </td> 
                        </tr>
                        <tr>
                            <td width="100%" align="left">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                          {%>
                            <!--Tool bar -->
                            <radG:RadCodeBlock ID="RadCodeBlock7" runat="server">

                                <script type="text/javascript">
                                                                function getOuterHTML(obj) 
                                                                {
                                                                    if (typeof (obj.outerHTML) == "undefined") {
                                                                        var divWrapper = document.createElement("div");
                                                                        var copyOb = obj.cloneNode(true);
                                                                        divWrapper.appendChild(copyOb);
                                                                        return divWrapper.innerHTML
                                                                    }
                                                                    else
                                                                        return obj.outerHTML;
                                                                }

                                                                function PrintRadGrid6(sender, args) 
                                                                {
                                                                    
                                                                   if (args.get_item().get_text() == 'Print')
                                                                     {

                                                                        var previewWnd = window.open('about:blank', '', '', false);
                                                                        var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid10.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid10.Skin)) %>';
                                                                        var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid9.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                                        var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                                        styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                                        var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid10.ClientID %>').get_element()) + "</body></html>";
                                                                        previewWnd.document.open();
                                                                        previewWnd.document.write(htmlcontent);
                                                                        previewWnd.document.close();
                                                                        previewWnd.print();
                                                                        previewWnd.close();
                                                                    }
                                                                }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadToolBar ID="RadtoolbarYOS" CssClass="smallToolBar" runat="server" Width="100%"
                                Skin="Office2007" UseFadeEffect="True" OnClientButtonClicking="PrintRadGrid6">
                                <Items>
                                    <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                        Height="24px" Text="Print" ToolTip="Print">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                        Height="24px" Text="Excel" ToolTip="Excel">
                                    </radG:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png" Text="Word"     ToolTip="Word"></radG:RadToolBarButton>--%>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <radG:RadToolBarButton IsSeparator="true">
                                    </radG:RadToolBarButton>
                                    <radG:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: middle;
                                                    height: 30px;">
                                                    <tr>
                                                        <td>
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td>
                                                            <asp:Label ID="LblYos" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG:RadToolBarButton>
                                </Items>
                            </radG:RadToolBar>
                            <!-- tool bar end -->
                            <radG:RadGrid ID="RadGrid10" runat="server" GridLines="Both" Skin="Outlook" AllowPaging="True"
                                OnGridExporting="RadGrid10_GridExporting" PageSize="5" PagerStyle-Visible="false"
                                OnItemDataBound="RadGrid2_ItemDataBound" ItemStyle-Wrap="false" AlternatingItemStyle-Wrap="false">
                                <MasterTableView TableLayout="fixed">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="19">
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </radG:RadGrid>
                            <%}%>
                        </td>  
                        </tr>
                        <tr>
                            <td align="right" style="font-family: Verdana; font-size: 8pt">
                            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Year Of Service"))
                          {%>
                            <a href="#" onclick="return ShowInsertForm(110);">...More</a>
                            <%}%>
                        </td> 
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!-- grid End -->
         <input type="hidden" id="hdNoOfRecords" value="0" />
        <radW:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <radW:RadWindow ID="EditGrid" runat="server" Title="User List Dialog" Top="50px"
                    Height="400px" Width="500px" Left="60px" ReloadOnShow="true" Modal="true" />
            </Windows>
        </radW:RadWindowManager>
  
</asp:Content>