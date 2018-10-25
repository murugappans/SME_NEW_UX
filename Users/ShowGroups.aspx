<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowGroups.aspx.cs" Inherits="SMEPayroll.Users.ShowGroups" %>

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
                        <li>List of Groups</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Security</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">List of Groups</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>



                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>


                            <radG:RadGrid ID="RadGrid1" runat="server" AllowSorting="true" GridLines="None" OnPreRender="RadGrid1_PreRender"
                                OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound"
                                OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand"
                                OnDeleteCommand="RadGrid1_DeleteCommand" AutoGenerateColumns="False" Skin="Outlook"
                                OnItemCommand="Radgrid1_Itemcommand" AllowFilteringByColumn="True" Width="93%">
                                <MasterTableView DataKeyNames="GroupId,GroupName" CommandItemDisplay="Bottom">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <%--<radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" />
                                            <HeaderStyle Width="35px" />
                                        </radG:GridTemplateColumn>--%>

                                        <radG:GridBoundColumn UniqueName="Groupid" Visible="False" HeaderText="GroupID" DataField="GroupID">
                                            <%--<ItemStyle Width="100px" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="GroupName" HeaderText="GroupName" DataField="GroupName" FilterControlAltText="cleanstring" ShowFilterIcon="false" AutoPostBackOnFilter ="true">
                                            <%--<ItemStyle Height="1px" Width="90%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn AllowFiltering="False" DataField="Rights" UniqueName="Rights">
                                            <ItemTemplate>
                                                <tt class="bodytxt">
                                                    <asp:Button runat="server" CssClass="btn red btn-xs"
                                                        BackColor="transparent" BorderStyle="None" BorderWidth="0" UseSubmitBehavior="true"
                                                        Text="Rights" ID="btnrights" />
                                                </tt>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="80px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton" ButtonCssClass="btn" ImageUrl="../frames/images/toolbar/edit.gif"
                                            CommandName="Edit" Text="Edit" UniqueName="EditColumn">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton" ButtonCssClass="btn"
                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="60px" HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings UserControlName="Grouptemplate.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <CommandItemSettings AddNewRecordText="Add New User Group" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                </ClientSettings>
                            </radG:RadGrid>

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
            GetConfirmation("Are you sure you want to delete this GroupName?", _id, "Confirm Delete", "Delete");
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
