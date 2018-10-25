<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveAddDed.aspx.cs" Inherits="SMEPayroll.Leaves.LeaveAddDed" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc3:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->








        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <!-- BEGIN PAGE HEADER-->

                <div class="theme-panel hidden-xs hidden-sm">
                    <div class="toggler"></div>
                    <div class="toggler-close"></div>
                    <div class="theme-options">
                        <div class="theme-option theme-colors clearfix">
                            <span>THEME COLOR </span>
                            <ul>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                            </ul>
                        </div>
                    </div>
                </div>


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Manage Leave</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Leave</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Leave</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">

                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-10">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <%--          <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields"
                                    AutoPostBack="True" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0">-select-</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList>--%>

                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm input-xsmall" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            AutoPostBack="True" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged" runat="server">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>

                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>Leave Type</label>
                                        <asp:DropDownList OnDataBound="cmbLeaveType_databound" ID="cmbLeaveType" DataTextField="Type"
                                            OnSelectedIndexChanged="cmbLeaveType_selectedIndexChanged" CssClass="textfields form-control input-sm"
                                            DataValueField="ID" DataSourceID="SqlDataSource3" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Type</label>
                                        <asp:DropDownList ID="cmbAddDeduct" runat="server" CssClass="textfields form-control input-sm">
                                            <asp:ListItem Selected="true" Value="0">-select-</asp:ListItem>
                                            <asp:ListItem Value="1">Addition</asp:ListItem>
                                            <asp:ListItem Value="2">Deduction</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Leave</label>
                                        <asp:TextBox ID="txtleaveaddded" runat="server"  data-type="currency" MaxLength="4" CssClass="textfields form-control input-sm input-xsmall number-dot"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator ID="vldtxtv3" ControlToValidate="txtleaveaddded"
                                            Display="Dynamic" ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,5}(\.\d{1,3})?$"
                                            runat="server"> 
                                        </asp:RegularExpressionValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="getData" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-2 text-right">
                                    <asp:Button CssClass="textfields btn btn-sm red" Text="Report" runat="server" OnClick="GetReport" /><asp:Button />
                                </div>
                            </div>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">

                                    function RowSelecting(sender, eventArgs) {
                                        alert("Selecting row: " + eventArgs.get_itemIndexHierarchical());
                                    }

                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>
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
                                        if (args.get_item().get_text() == 'Print') {

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
                                    <%--window.onload = Resize;
                                    function Resize() {
                                        if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                        }
                                        else {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                        }
                                    }--%>
                                </script>

                            </radG:RadCodeBlock>

                            <input type="hidden" id="msg" runat="server" />
                            <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server" Visible = "false"></asp:Label>
                                                      
                            <radG:RadGrid ID="RadGrid1"  AllowMultiRowEdit="True" OnGridExporting="RadGrid1_GridExporting"
                                OnItemCommand="RadGrid1_ItemCommand" Skin="Outlook" Width="100%" runat="server"
                                GridLines="Both" AllowMultiRowSelection="true" AllowFilteringByColumn="false" EnableHeaderContextMenu="true">

                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="emp_code,leave_remaining,Remarks,row_id"
                                    EditMode="InPlace" AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    AllowAutomaticDeletes="true" EnableHeaderContextMenu="true">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" ForeColor="Navy" />
                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                        ShowExportToCsvButton="true" />
                                    <ItemStyle HorizontalAlign="left" BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />

                                    <CommandItemTemplate>
                                    </CommandItemTemplate>
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" />
                                            <HeaderStyle Width="35px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridClientSelectColumn ItemStyle-HorizontalAlign="Left" UniqueName="GridClientSelectColumn">
                                       <ItemStyle Width="30px" />
                                            <HeaderStyle Width="30px" />
                                             </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="row_id" DataType="System.Int32" HeaderText="ID"
                                            SortExpression="row_id" ItemStyle-HorizontalAlign="Left" Display="false" UniqueName="row_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="false" DataType="System.Int32"
                                            HeaderText="emp_code" ItemStyle-HorizontalAlign="Left" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="contains">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="department" HeaderText="Department" SortExpression="department"
                                            UniqueName="department" ItemStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="contains">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leave_remaining" HeaderText="Current Year Leave" UniqueName="leave_remaining"
                                            AutoPostBackOnFilter="true" ItemStyle-HorizontalAlign="Left" CurrentFilterFunction="contains">
                                            <ItemStyle Width="132px" />
                                            <HeaderStyle Width="132px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="calcleaverem" HeaderText="Calculate" UniqueName="calcleaverem"
                                            AutoPostBackOnFilter="true" ItemStyle-HorizontalAlign="Left" CurrentFilterFunction="contains">
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Remarks" UniqueName="Remarks"
                                            HeaderText="Employee Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control input-sm max-character custom-maxlength" runat="server" data-maxlength ="250" Text='<%# DataBinder.Eval(Container,"DataItem.remarks")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle  />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="TimeCardId" HeaderText="Time Card ID" ShowFilterIcon="False" CurrentFilterFunction="StartsWith" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            ReadOnly="True" SortExpression="TimeCardId" UniqueName="TimeCardId">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Department" UniqueName="Department" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>


                                    </Columns>
                                    <CommandItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Button runat="server" ID="UpdateAll" class="textfields btn btn-sm red" Style="height: 22px"
                                                Text="Copy Remarks For Selected" CommandName="UpdateAll" />
                                        </div>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>
                            <div style="text-align: center">
                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" Style="width: 80px; height: 22px"
                                    class="textfields btn btn-sm red" CommandName="UpdateAll" OnClick="btnsubmit_Click" />
                                <asp:Button ID="btnCalc" runat="server" Text="Calculate before Update" Style="height: 22px"
                                    class="textfields btn btn-sm default" CommandName="CalculateAll" OnClick="btnCalc_Click" />
                            </div>

                            <radG1:RadToolBar ID="RecordToolBar" CssClass="heading-box heading-box-showhide" Visible="false" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                BorderWidth="0px" OnButtonClick="RecordToolBar_ButtonClick">
                                <Items>
                                    <radG1:RadToolBarButton runat="server" CommandName="Print"
                                        Text="Print" ToolTip="Print">
                                    </radG1:RadToolBarButton>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton runat="server" Text="">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" CommandName="Excel"
                                        Text="Excel" ToolTip="Excel">
                                    </radG1:RadToolBarButton>
                                    <radG1:RadToolBarButton runat="server" CommandName="Word"
                                        Text="Word" ToolTip="Word">
                                    </radG1:RadToolBarButton>
                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" CommandName="Refresh"
                                        Text="UnGroup" ToolTip="UnGroup">
                                    </radG1:RadToolBarButton>
                                    <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                </radG:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                            </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="height: 30px">
                                                    <tr>
                                                        <td valign="middle">
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td valign="middle">
                                                            <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG1:RadToolBarButton>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server"
                                        Text="Reset to Default" ToolTip="Reset to Default">
                                    </radG1:RadToolBarButton>
                                    <radG1:RadToolBarButton runat="server"
                                        Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                    </radG1:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                </Items>
                            </radG1:RadToolBar>




                            <radG:RadGrid ID="RadGridReport" AllowMultiRowEdit="True" Visible="true"
                                Skin="Outlook" Width="100%" runat="server"
                                GridLines="Both" AllowMultiRowSelection="false" AllowFilteringByColumn="false" OnItemCommand="Reprort_ItemCommand"
                                EnableHeaderContextMenu="true">

                                <MasterTableView DataKeyNames="Date"
                                    EditMode="InPlace" AutoGenerateColumns="true"
                                    EnableHeaderContextMenu="true">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" ForeColor="Navy" />

                                    <ItemStyle HorizontalAlign="left" BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" HorizontalAlign="Left" Height="20px" />


                                </MasterTableView>
                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="false">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <ClientEvents />
                                </ClientSettings>
                            </radG:RadGrid>



                            <radG1:RadToolBar Visible="false" ID="DetailRadToolBar" CssClass="heading-box heading-box-showhide" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                OnButtonClick="DetailRadToolBar_ButtonClick" BorderWidth="0px">
                                <Items>
                                    <radG1:RadToolBarButton runat="server" CommandName="Print"
                                        Text="Print" ToolTip="Print">
                                    </radG1:RadToolBarButton>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton runat="server" Text="">
                                    <ItemTemplate>
                                        <div>
                                            <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                    <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                </tr>
                                            </table>
                                        </div>
                                    </ItemTemplate>
                                </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" CommandName="Excel"
                                        Text="Excel" ToolTip="Excel">
                                    </radG1:RadToolBarButton>
                                    <radG1:RadToolBarButton runat="server" CommandName="Word"
                                        Text="Word" ToolTip="Word">
                                    </radG1:RadToolBarButton>
                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" CommandName="Refresh"
                                        Text="UnGroup" ToolTip="UnGroup">
                                    </radG1:RadToolBarButton>
                                    <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                </radG:RadToolBarButton>--%>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server" Text="Count">
                                        <ItemTemplate>
                                            <div>
                                                <table cellpadding="0" cellspacing="0" border="0" style="height: 30px">
                                                    <tr>
                                                        <td valign="middle">
                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                        <td valign="middle">
                                                            <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </radG1:RadToolBarButton>
                                    <%--<radG1:RadToolBarButton IsSeparator="true">
                                        </radG1:RadToolBarButton>--%>
                                    <radG1:RadToolBarButton runat="server"
                                        Text="Reset to Default" ToolTip="Reset to Default">
                                    </radG1:RadToolBarButton>
                                    <radG1:RadToolBarButton runat="server"
                                        Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                    </radG1:RadToolBarButton>
                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                </Items>
                            </radG1:RadToolBar>
                            <radG:RadGrid ID="RadGridDetails" AllowMultiRowEdit="True" Visible="true"
                                Skin="Outlook" Width="100%" runat="server"
                                GridLines="Both" AllowMultiRowSelection="false" AllowFilteringByColumn="false"
                                EnableHeaderContextMenu="true" OnGridExporting="RadGridDetail_GridExporting">

                                <MasterTableView DataKeyNames="Date"
                                    EditMode="InPlace" AutoGenerateColumns="true"
                                    EnableHeaderContextMenu="true">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="left" ForeColor="Navy" />

                                    <ItemStyle HorizontalAlign="left" BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" HorizontalAlign="Left" Height="20px" />


                                </MasterTableView>
                                <ClientSettings EnablePostBackOnRowClick="true" EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="false">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>

                                    <ClientEvents />
                                </ClientSettings>
                            </radG:RadGrid>



                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select Id,Type from leave_types lt Where (lt.code!='0005' Or lt.code is null)"></asp:SqlDataSource>


                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->

        
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2014 &copy; Metronic by keenthemes.
            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>

<uc_js:bundle_js ID="bundle_js" runat="server" />

    <script type="text/javascript">
        $("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        $("table.rgMasterTable input[type='text']").addClass("form-control");
        $("input[type='submit']").removeAttr("style");
        $("input[type='button']").removeAttr("style");
          window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
          }
        $(document).ready(function () {
            $("div#rdTrdate_wrapper").removeAttr("style");
            $('#imgbtnfetch').click(function () {
                return validateform();
            });
            $('#btnsubmit').click(function () {
                return validateformsubmit();
            });
            $('#btnCalc').click(function () {
                return validateformsubmit();
            });
            $(document).on("click", "#RadGrid1_ctl00_ctl03_ctl01_UpdateAll", function () {
                return checkboxchecked();
                $("input[type=checkbox]").each(function () {

                    if ($(this).is(":checked") && $(this).next("#txtRemarks")==="") {
                        WarningNotification("Please");
                        return false;
                    }
                });
            });
        })
        function validateform() {
            var _message = "";
            if ($.trim($("#cmbLeaveType").val()) === "-1")
                _message = "Please Select Leave Type";
            else if ($.trim($("#cmbAddDeduct").val()) === "0")
                _message = "Please select Addition/Deduction Type";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validateformsubmit() {
            var _message = "";
            if ($.trim($("#cmbLeaveType").val()) === "-1")
                _message = "Please Select Leave Type";
            else if ($.trim($("#cmbAddDeduct").val()) === "0")
                _message = "Please select Addition/Deduction Type";
            else if ($.trim($("#txtleaveaddded").val()) === "")
                _message = "Please enter Leave Figure";
            else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function checkboxchecked() {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
