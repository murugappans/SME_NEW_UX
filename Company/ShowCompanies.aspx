<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowCompanies.aspx.cs"
    Inherits="SMEPayroll.Company.ShowCompanies" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

                <div class="theme-panel hidden-xs hidden-sm ">
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
                        <li>View Companies</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Company</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">View Companies</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-4">
                                    <label>Total Licenced</label>
                                    <asp:Label ID="lbltotalemp" runat="server" CssClass="bodytxt"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <label>Licence Used</label>
                                    <asp:Label ID="lbldbemp" runat="server" CssClass="bodytxt"></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <label>Licence Remaining</label>
                                    <asp:Label ID="lbldiff" runat="server" CssClass="bodytxt"></asp:Label>
                                </div>
                                <%--<div class="col-md-6 text-right">
                                    <asp:Button ID="bnrefresh" Text="Refresh" class="textfields btn btn-sm red" runat="server"  OnClick="bnrefresh_Click" Visible="false" />
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" style="display: none;" />
                                </div>--%>
                            </div>






                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <div>
                                <input type="hidden" id="flag" runat="server" />
                                <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" AllowPaging="true" PageSize="15" runat="server" OnPreRender="RadGrid1_PreRender"
                                                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemDataBound"
                                                OnItemDataBound="RadGrid1_ItemDataBound1" GridLines="None" Skin="Outlook" Width="100%" AllowSorting="true">
                                                <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="Bottom" DataKeyNames="Company_Id">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <CommandItemTemplate>
                                                        <div style="float: LEFT">
                                                            <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />
                                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Company/AddCompanyNew.aspx"
                                                                Font-Bold="True" Font-Underline="true">Add New Company</asp:HyperLink>
                                                        </div>
                                                    </CommandItemTemplate>
                                                    <Columns>
                                                        <radG:GridBoundColumn Display="false" DataField="Company_Id" HeaderText="Company_Id"
                                                            ReadOnly="True" SortExpression="Company_Id" Visible="False" UniqueName="Company_Id">
                                                        </radG:GridBoundColumn>

                                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                            <ItemTemplate>
                                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-company.png" runat="Server" />--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="2%" />
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn DataField="Company_code" HeaderText="Company Code" SortExpression="Company_code"
                                                            UniqueName="Company_code">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Company_name" HeaderText="Company Name" SortExpression="Company_name"
                                                            UniqueName="Company_name">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="phone" HeaderText="Company Phone" SortExpression="phone"
                                                            UniqueName="phone">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="email" HeaderText="Company Email" SortExpression="email"
                                                            UniqueName="email">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Country" HeaderText="Country" SortExpression="Country"
                                                            UniqueName="Country">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn AllowFiltering="false" UniqueName="editHyperlink">
                                                            <ItemTemplate>
                                                                <tt class="bodytxt">
                                                                    <asp:ImageButton ID="btnedit" ToolTip="Edit" CssClass="fa edit-icon" AlternateText=" " 
                                                                        runat="Server" />
                                                                </tt>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            <ItemStyle  HorizontalAlign="Center" />
                                                        </radG:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                                </ClientSettings>
                                            </radG:RadGrid>
                            </div>

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
        window.onload = function () {
            CallNotification('<%=Session["actionMessage"].ToString() %>');
            '<%Session["actionMessage"] = ""; %>';
        }

    </script>

</body>
</html>
