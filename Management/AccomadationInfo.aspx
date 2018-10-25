<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccomadationInfo.aspx.cs"
    Inherits="SMEPayroll.Management.AccomadationInfo" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />
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

        function ShowInsert(row) {
            window.radopen(row, "DetailGrid");
            return false;
        }

        function ShowInsertForm(row) {
            return false;
        }
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
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
                        <li>Accommodation Info</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAccomadation.aspx">Manage Accommodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Accommodation</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Accommodation Info</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12">
                                    <asp:Button ID="Button4" CssClass="textfields btn btn-sm default" OnClick="Button4_Click" 
                                        Text="Export to Excel" runat="server"></asp:Button>
                                    <asp:Button ID="Button5" CssClass="textfields btn btn-sm default" OnClick="Button5_Click" 
                                        Text="Export to Word" runat="server"></asp:Button>
                                    <asp:Button ID="Button6" CssClass="textfields btn btn-sm default" OnClick="btnExportPdf_click" 
                                        Text="Export to PDF" runat="server"></asp:Button>
                                </div>
                                
                            </div>

                            
                                <radG:RadGrid ID="RadGrid1"  runat="server" AllowFilteringByColumn="True" AllowPaging="true"
                                    PageSize="20" GridLines="None" Skin="Outlook" Width="98%" OnInsertCommand="RadGrid1_InsertCommand"
                                    OnUpdateCommand="RadGrid1_UpdateCommand" OnItemCreated="RadGrid1_ItemCreated"
                                    OnItemCommand="RadGrid1_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    OnDeleteCommand="RadGrid1_DeleteCommand">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="AccomadationCode,UsedCapacity"
                                        CommandItemDisplay="Bottom" CommandItemSettings-AddNewRecordText="Add New Accomadation">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>

                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                <ItemTemplate>
                                                    <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>

                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="AccomadationCode"
                                                DataType="System.String" UniqueName="AccomadationCode" Visible="true" SortExpression="AccomadationCode"
                                                HeaderText="AccommodationCode">
                                                <%--<HeaderStyle Width="10%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="AccomadationName" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                                DataType="System.String" UniqueName="AccomadationName" Visible="true" SortExpression="AccomadationName"
                                                HeaderText="Accommodation Name">
                                                <%--<HeaderStyle Width="20%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="Capacity" DataType="System.String" FilterControlAltText="numericonly" ShowFilterIcon="false"
                                                UniqueName="Capacity" Visible="true" SortExpression="Capacity" HeaderText="Capacity">
                                                <%--<HeaderStyle Width="10%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="UsedCapacity" DataType="System.String" FilterControlAltText="numericonly" ShowFilterIcon="false"
                                                UniqueName="UsedCapacity" Visible="true" SortExpression="UsedCapacity" HeaderText="Used Capacity">
                                                <%--<HeaderStyle Width="10%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="AccAuthPerson1" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                DataType="System.String" UniqueName="AccAuthPerson1" Visible="true" SortExpression="AccAuthPerson1"
                                                HeaderText="Authorised Person">
                                                <%--<HeaderStyle Width="10%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="AccAuthPersonPhone" FilterControlAltText="numericonly" ShowFilterIcon="false"
                                                DataType="System.String" UniqueName="AccAuthPersonPhone" Visible="true" SortExpression="AccAuthPersonPhone"
                                                HeaderText="Auth Person Phone">
                                                <%--<HeaderStyle Width="10%" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="Cooking" DataType="System.String" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                UniqueName="Cooking" Visible="true" SortExpression="Cooking" HeaderText="Cooking">
                                                <ItemStyle Width="100px"  />
                                                <HeaderStyle Width="100px"  />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" ItemStyle-Width="10%" DataField="Laundry" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                DataType="System.String" UniqueName="Laundry" Visible="true" SortExpression="Laundry"
                                                HeaderText="Laundry">
                                                <ItemStyle Width="100px"  />
                                                <HeaderStyle Width="100px"  />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="True" ReadOnly="False" DataField="AirCon" DataType="System.String" FilterControlAltText="alphabetsonly" ShowFilterIcon="false"
                                                UniqueName="AirCon" Visible="true" SortExpression="AirCon" HeaderText="Air Con">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px"  />
                                            </radG:GridBoundColumn>
                                            <radG:GridTemplateColumn AllowFiltering="False" HeaderText="" UniqueName="Image">
                                                <ItemTemplate>
                                                    <input type="button" id="Image3" value="Details" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn AllowFiltering="false" UniqueName="editHyperlink">
                                                <ItemTemplate>
                                                    <tt class="bodytxt">
                                                        <asp:ImageButton ID="btnDetails" AlternateText="Edit" ImageUrl="../frames/images/toolbar/edit.gif"
                                                            runat="Server" />
                                                    </tt>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridButtonColumn  ButtonType="ImageButton"
                                                ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                UniqueName="DeleteColumn">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridButtonColumn>
                                        </Columns>
                                        <ExpandCollapseColumn Visible="False">
                                            <HeaderStyle Width="19px" />
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                        <CommandItemTemplate>
                                            <div>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />--%>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Management/AccomadationManagement.aspx?tType=0"
                                                    Font-Bold="True">Add New Accommodation</asp:HyperLink>
                                            </div>
                                        </CommandItemTemplate>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                       <%-- <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                       <%-- <ClientEvents OnRowDblClick="RowDblClick" />--%>
                                    </ClientSettings>
                                </radG:RadGrid>
                            
                            <input type="hidden" runat="server" id="txtRadId" />
                            <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
                                <Windows>
                                    <telerik:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="150px"
                                        Height="340px" Width="660px" Left="20px" ReloadOnShow="false" Modal="true" />
                                </Windows>
                            </telerik:RadWindowManager>
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
        $("input[type='button']").removeAttr("style");
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Accommodation Info?", _id, "Confirm Delete", "Delete");
        });
         window.onload = function () {
             CallNotification('<%=ViewState["actionMessage"].ToString() %>');
             var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
             $.each(_inputs, function (index, val) {
                 $(this).addClass($(this).attr('alt'));

             })
         }

    </script>

</body>
</html>
