<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowDropdowns.aspx.cs"
    Inherits="SMEPayroll.Management.ShowDropdowns" %>

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
                        <li>Show Dropdowns</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Settings</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Show Dropdowns</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row no-bg">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>


                            <%--<div class="search-box clearfix">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" style="display: none;" />
                                </div>
                            </div>--%>


                           


                  <%--  <radG:RadGrid ID="RadGrid1" CssClass="noHover-Effect" runat="server" DataSourceID="SqlDataSource1" AllowFilteringByColumn='false'
                        GridLines="none" AllowPaging="true" AllowSorting="false" PageSize="100" OnPreRender="RadGrid1_PreRender" Width="100%">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView ShowHeader="false" TableLayout="Auto">
                            <ItemTemplate>

                                <%# (((GridItem)Container).ItemIndex != 0)? "</td></tr></table>" : "" %>

                                <div class="mt-element-list mt-element-list-style-2 col-md-3 ">
                                            <div class="mt-list-head list-todo default margin-tb-10">
                                                <div class="list-head-title-container">
                                                    <h5 class="list-title"><%# DataBinder.Eval(Container.DataItem, "DropDown") %></h5>
                                                </div>
                                                <a href='<%# DataBinder.Eval(Container.DataItem, "Navigate") %>'>
                                                    <div class="list-count pull-right red">
                                                        <i class="fa fa-plus"></i>
                                                    </div>
                                                </a>
                                            </div>
                                        </div>




                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                        </MasterTableView>
                        <GroupingSettings CaseSensitive="false" />
                    </radG:RadGrid>--%>




                               <radG:RadGrid ID="RadGrid1" runat="server"  AllowFilteringByColumn='false' CssClass="noHover-Effect no-bg dropdowns"
                    GridLines="none" AllowPaging="true" AllowSorting="false" PageSize="100" OnPreRender="RadGrid1_PreRender" Width="80%">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <MasterTableView ShowHeader="false" TableLayout="Auto" CssClass="border-right-0">
                        <ItemTemplate>
                           <%# (((GridItem)Container).ItemIndex != 0)? "</td></tr></table>" : "" %>

                                <div class="mt-element-list mt-element-list-style-2 col-md-3 ">
                                            <div class="mt-list-head list-todo default margin-tb-10">
                                                <div class="list-head-title-container">
                                                    <h5 class="list-title" style ="font-weight:600"><%# DataBinder.Eval(Container.DataItem, "DropDown") %></h5>
                                                     <span>This management setttings</span>
                                                  <br /><span style ="color:black"><a href="#" style ="color:green ">Edit</a> | <a href="#" style ="color:red ">Delete</a></span>
                                                </div>
                                                 <a href='<%# DataBinder.Eval(Container.DataItem, "Navigate") %>'>
                                                    <div class="list-count pull-right red">
                                                        <i class="fa fa-arrow-right"></i>
                                                    </div>
                                                </a>
                                             
                                            </div>
                                   
                                        </div>


                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                    </MasterTableView>
                    <GroupingSettings CaseSensitive="false" />
                </radG:RadGrid>







                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT [ID], [dropdown],[Navigate] FROM [dropdowns] order by DropDown"></asp:SqlDataSource>



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
    </script>

</body>
</html>
