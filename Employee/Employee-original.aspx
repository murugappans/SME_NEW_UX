<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Employee.aspx.cs" Inherits="SMEPayroll.employee.Employee" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../STYLE/PMSStyle.css" type="text/css" />
    <link rel="stylesheet" href="../style/MenuStyle.css" type="text/css" />

    <script language="JavaScript1.2"> 
        <!-- 
//            if (document.all)
//            window.parent.defaultconf=window.parent.document.body.cols
//            function expando()
//            {
//                window.parent.expandf()
//            }
//            document.ondblclick=expando 
        -->
    </script>
    <style type="text/css">   
    html, body, form   
    {   
       height: 100%;   
       margin: 0px;   
       padding: 0px;  
       overflow: hidden;
    }   



    </style>
</head>
<body onload="ShowMsg();" style="margin-left: auto; margin: 0px; height: 100%; overflow: hidden;"  >
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server"  />
        <telerik:RadSplitter ID="RadSplitter1" Width="100%" Height="100%" runat="server" 
            Orientation="Horizontal" BorderSize="0" BorderStyle="None" PanesBorderSize="0" BackColor="AliceBlue" BorderWidth="0px"  >
            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="none" Height="32px" Width="100%"   MaxHeight="100">
                <telerik:RadSplitter ID="Radsplitter11" runat="server">
                    <telerik:RadPane ID="Radpane111" runat="server" Scrolling="none">
                        <!-- top -->
                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellpadding="0" cellspacing="0" width="100%" border="0" background="../frames/images/toolbar/backs.jpg" height="31.5px">
                                        <tr>
                                            <td>
                                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employment Management Form</b></font>
                                             </td>
                                             <td align="right">
                                              <!-- controll -->
                                              <asp:DropDownList ID="drpCompany" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"
                                                AutoPostBack="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Company_Name"
                                                DataValueField="Company_ID" Width="250px" CssClass="trstandtop">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="drpShowAll" AutoPostBack="true" OnSelectedIndexChanged="drpShowAll_SelectedIndexChanged"
                                                runat="server" CssClass="trstandtop">
                                            </asp:DropDownList>
                                            <asp:Button ID="btnshowall" Visible="false" CssClass="textfields" Text="Show All Employees"
                                                runat="server" OnClick="btnshowall_Click" />
                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Visible="false" ID="btnallemp" CssClass="textfields"
                                                Text="ExportAllEmpToExcel" runat="server" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="Button4" CssClass="textfields" Visible="false" Width="150px" Text="Export to Excel"
                                                OnClick="Button1_Click" runat="server"></asp:Button>
                                            <asp:Button ID="Button5" CssClass="textfields" Width="150px" Text="Export to Word"
                                                Visible="false" OnClick="Button2_Click" runat="server"></asp:Button><asp:CheckBox
                                                    ID="CheckBox1" Visible="false" CssClass="bodytxt" Text="Exports All" runat="server">
                                                </asp:CheckBox>
                                            <asp:Button ID="btnsubapprove" Visible="false" OnClick="btnsubapprove_click" runat="server"
                                                Text="Approve Employee" class="textfields" Style="width: 180px; height: 22px" />
                                              <!-- controls end -->
                                                
                                            </td>
                                        </tr>
                                       <%--   <tr bgcolor="#E5E5E5" visible="false">
                                            <td align="left" style="height: 0px">
                                              <asp:DropDownList ID="drpCompany" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"
                                                    AutoPostBack="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Company_Name"
                                                    DataValueField="Company_ID" Width="250px">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="drpShowAll" AutoPostBack="true" OnSelectedIndexChanged="drpShowAll_SelectedIndexChanged"
                                                    runat="server">
                                                </asp:DropDownList>
                                                <asp:Button ID="btnshowall" Visible="false" CssClass="textfields" Text="Show All Employees"
                                                    runat="server" OnClick="btnshowall_Click" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button Visible="false" ID="btnallemp" CssClass="textfields"
                                                    Text="ExportAllEmpToExcel" runat="server" />
                                                &nbsp;&nbsp;
                                                <asp:Button ID="Button4" CssClass="textfields" Visible="false" Width="150px" Text="Export to Excel"
                                                    OnClick="Button1_Click" runat="server"></asp:Button>
                                                <asp:Button ID="Button5" CssClass="textfields" Width="150px" Text="Export to Word"
                                                    Visible="false" OnClick="Button2_Click" runat="server"></asp:Button><asp:CheckBox
                                                        ID="CheckBox1" Visible="false" CssClass="bodytxt" Text="Exports All" runat="server">
                                                    </asp:CheckBox>
                                                <asp:Button ID="btnsubapprove" Visible="false" OnClick="btnsubapprove_click" runat="server"
                                                    Text="Approve Employee" class="textfields" Style="width: 180px; height: 22px" />
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <!-- content start -->
                        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
                            <AjaxSettings>
                                <radG:AjaxSetting AjaxControlID="RadGrid2">
                                    <UpdatedControls>
                                        <radG:AjaxUpdatedControl ControlID="RadGrid2" />
                                    </UpdatedControls>
                                </radG:AjaxSetting>
                            </AjaxSettings>
                        </radG:RadAjaxManager>
                        <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

                            <script language="javascript" type="text/javascript">
            
                            function ShowMsg()
                            {
                                var sMsg = '<%=sMsg%>';
                                if (sMsg != '')
                                    alert(sMsg);
                            }
                            </script>

                        </radG:RadCodeBlock>
                            <div>
                                <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0" style="table-layout: fixed;" id="gridTable">
                                <tr>
                                    <td valign="top">
                                        <label id="lblMessage" style="color: Red" class="bodytxt" runat="server" visible="true">
                                        </label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%; vertical-align: top;">
                                        <!-- ToolBar -->
                                        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                            <script type="text/javascript">
                                               function openNewWin(url) {
	                                                var width = screen.availWidth - 100;
	                                                var height = screen.availHeight - 100;
	                                                var left = parseInt((screen.availWidth / 2) - (width / 2));
	                                                var top = parseInt((screen.availHeight / 2) - (height / 2));
	                                                var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
	                                                var myWindow = window.open(url, "mynewwin", windowFeatures);

	                                                // var x = window.open(url, 'mynewwin', 'width=800,height=600,toolbar=0');
	                                                myWindow.focus();
	                                            } 
                                            
                                            
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

                                            <script type="text/javascript">
                           
                                                       
                                               //Approach 2
                                               //function to set the height of the scroll
                                                //<Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"  />
                                                function GridCreated()
                                                   {
//                                                        var scrollArea = document.getElementById("<%= RadGrid1.ClientID %>" + "_GridData");
//                                                        var table = document.getElementById("gridPane2");
//                                                        var height1=table.clientHeight-100;
//                                                        scrollArea.style.height =height1+ "px";
                                                   }
                                                 
                                                 
                                                 window.onload = Resize;
                                                 function Resize()
                                                  {
                                                    //alert(screen.height);
                                                    if( screen.height > 768)
                                                    {
                                                     //alert("1");
                                                        document.getElementById('<%= RadGrid1.ClientID %>').style.height="90.7%";
                                                    }
                                                    else
                                                    {
                                                     //alert("2");
                                                        document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                                    }
                                                  }
                                                 
                                                 
                                                 
                                                /* function pageLoad() 
                                                 {  
                                                    var grid = $find("RadGrid1");  
                                                    var columns = grid.get_masterTableView().get_columns();  
                                                    for (var i = 0; i < columns.length; i++) 
                                                    {  
                                                     columns[i].resizeToFit();  
                                                    }  
                                                  }
                                                 */

                                            </script>
                                            
                                            

                                        </radG:RadCodeBlock>
                                        
                                        <!-- ToolBar End -->
                                    </td>
                                </tr>
                            </table>
                            </div>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select Company_ID,Company_Name from company">
                            </asp:SqlDataSource>
                       
                        <!-- top end -->
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
            <telerik:RadPane ID="gridPane2" runat="server" Width="100%" Height="100%"   Scrolling="None" BorderWidth="0px">
                <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"  
                                            OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px" >
                                            <Items>
                                                <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                                                    Text="Print" ToolTip="Print">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton IsSeparator="true">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="">
                                                    <ItemTemplate>
                                                        <div>
                                                            <table cellpadding="0" cellspacing="0" border="0" >
                                                                <tr>
                                                                    <td class="bodytxt" valign="middle" style="height:30px">
                                                                        &nbsp;Export To:</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                                                    Text="Excel" ToolTip="Excel">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png"
                                                    Text="Word" ToolTip="Word">
                                                </radG:RadToolBarButton>
                                                <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton IsSeparator="true">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                                    Text="UnGroup" ToolTip="UnGroup">
                                                </radG:RadToolBarButton>
                                        <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                                </radG:RadToolBarButton>--%>
                                                <radG:RadToolBarButton IsSeparator="true">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <div>
                                                            <table cellpadding="0" cellspacing="0" border="0" style="height:30px">
                                                                <tr>
                                                                    <td valign="middle">
                                                                        <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                                    <td valign="middle">
                                                                        <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton IsSeparator="true">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                                    Text="Reset to Default" ToolTip="Reset to Default">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" ImageUrl="../Frames/Images/GRIDTOOLBAR/save-s.png"
                                                    Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                                </radG:RadToolBarButton>
                                                <radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="True"></radG:RadToolBarButton>
                                            </Items>
                                        </radG:RadToolBar>
               
                                        <radG:RadGrid AllowMultiRowSelection="true" ID="RadGrid1" runat="server"   EnableLinqExpressions="False"
                                        AutoGenerateColumns="False" Skin="Outlook" Width="99%"  Height="90%"  AllowPaging="true"
                                        PageSize="50" AllowFilteringByColumn="true" 
                                        OnItemDataBound="RadGrid1_ItemDataBound1"
                                        OnItemCommand="RadGrid1_ItemDataBound1" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        OnPageIndexChanged="RadGrid1_PageIndexChanged" EnableHeaderContextMenu="true"
                                        AllowCustomPaging="false" OnPreRender="RadGrid1_PreRender" 
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="false"
                                        OnGridExporting="RadGrid1_GridExporting"  PagerStyle-AlwaysVisible="True" GridLines="Both" AllowSorting="true" 
                                        Font-Size="11"
                                        Font-Names="Tahoma"   >
                                        <ExportSettings HideStructureColumns="true" />
                                        <%--      <ExportSettings HideStructureColumns="true"  IgnorePaging="true" OpenInNewWindow="true">
                                                <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="SushiBar menu" 
                                                    PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm"  />
                                            </ExportSettings>--%>
                                        <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="emp_code" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                ShowExportToCsvButton="true" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" />
                                            <ItemStyle Height="25px" VerticalAlign="middle" />
                                            <CommandItemTemplate>
                                                <div >
                                                    <table style="height:25px; vertical-align:middle;">
                                                        <tr>
                                                            <td>
                                                                 <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />&nbsp;&nbsp;
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/employee/AddEditEmployee.aspx" 
                                                                    Font-Underline="false">Add New Employee</asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                   
                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <radG:GridBoundColumn ShowFilterIcon="False" CurrentFilterFunction="StartsWith" UniqueName="EmpCode" HeaderImageUrl="../frames/images/EMPLOYEE/Grid- employee.png"
                                                    HeaderText="Emp Code"  DataField="emp_code"
                                                    Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="1">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                                    </ItemTemplate>
                                                   <HeaderStyle Width="30px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridTemplateColumn ShowFilterIcon="False" AllowFiltering="False" UniqueName="TemplateColumnEC"
                                                    Display="false" HeaderText="Code" SortExpression="emp_code">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" Text='<%# "DPT"+ DataBinder.Eval(Container.DataItem,"emp_code").ToString()%>'
                                                            NavigateUrl='<%# "AddEditEmployee.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                                            ID="empcode" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridTemplateColumn>
                                                <%--                                <radG:GridTemplateColumn UniqueName="TCEmpName" HeaderText="Employee Name" SortExpression="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                        <ItemTemplate>
                                                            <asp:HyperLink runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"emp_name").ToString()%>'
                                                                NavigateUrl='<%# "AddEditEmployee.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                                                ID="empname" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>--%>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_name" HeaderText="Employee Name"
                                                    DataField="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <%--<HeaderStyle Width="300px" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_type" HeaderText="Pass Type"
                                                    DataField="emp_type" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <%--<HeaderStyle Width="90px" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ic_pp_number" HeaderText="IC/FIN Number"
                                                    DataField="ic_pp_number" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="empgroupname" HeaderText="Type"
                                                    DataField="empgroupname" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID" HeaderText="Time Card ID"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Designation"
                                                    HeaderText="Designation" DataField="Designation" CurrentFilterFunction="contains"
                                                    AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Department"
                                                    HeaderText="Department" DataField="Department" CurrentFilterFunction="contains"
                                                    AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="hand_phone"
                                                    HeaderText="Mobile" DataField="hand_phone" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="email" HeaderText="Email"
                                                    DataField="email" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink">
                                                    <ItemTemplate>
                                                        <tt class="bodytxt">
                                                            <asp:ImageButton ID="btnedit" AlternateText="Edit" ImageUrl="../frames/images/toolbar/edit.gif"
                                                                runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridClientSelectColumn Visible="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="Nationality" HeaderText="Nationality"
                                                    DataField="Nationality" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="Trade" HeaderText="Trade"
                                                    DataField="Trade" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"   ></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                            <ClientEvents OnGridCreated="GridCreated" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                
            </telerik:RadPane>
        </telerik:RadSplitter>
    </form>
</body>
</html>