<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTeam.aspx.cs" Inherits="SMEPayroll.Cost.ManageTeam" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
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
                        <li>Manage Team</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Cost.aspx"><span>Costing Managements</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="CostingByTeamIndex.aspx"><span>Costing By Team</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Team</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>
                            
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" AllowSorting="true" GridLines="None" OnPreRender="RadGrid1_PreRender"
                                                OnNeedDataSource="RadGrid1_NeedDataSource"
                                                OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand"
                                                OnDeleteCommand="RadGrid1_DeleteCommand" AutoGenerateColumns="False" Skin="Outlook"
                                                OnItemCommand="Radgrid1_Itemcommand" AllowFilteringByColumn="True" Width="93%">
                                                <MasterTableView DataKeyNames="Tmid,LeadId,TeamName" CommandItemDisplay="Bottom">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <Columns>

                                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                            <ItemTemplate>
                                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            <ItemStyle  HorizontalAlign="Center" />
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn UniqueName="LeadId" Visible="False" HeaderText="LeadId" DataField="LeadId">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn UniqueName="Team Name" HeaderText="Team Name" DataField="TeamName" FilterControlAltText="cleanstring" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                            
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn UniqueName="Team Lead" HeaderText="Team Lead" DataField="emp_Lead" FilterControlAltText="cleanstring" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                            
                                                        </radG:GridBoundColumn>

                                                        <radG:GridTemplateColumn AllowFiltering="False" DataField="Rights" UniqueName="Rights">
                                                            <ItemTemplate>
                                                                <tt class="bodytxt">
                                                                    <asp:Button runat="server"  Style="text-decoration: underline; text-align: left"
                                                                        BackColor="transparent"  BorderStyle="None" BorderWidth="0" UseSubmitBehavior="true"
                                                                        Text="Members" ID="btnrights" />
                                                                </tt>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                                            <ItemStyle  HorizontalAlign="Center" />
                                                        </radG:GridTemplateColumn>
                                                        <radG:GridButtonColumn ButtonType="ImageButton" ImageUrl="../frames/images/toolbar/edit.gif"
                                                            CommandName="Edit" Text="Edit" UniqueName="EditColumn">
                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                            <ItemStyle  HorizontalAlign="Center" />
                                                        </radG:GridButtonColumn>
                                                        <radG:GridButtonColumn  ButtonType="ImageButton"
                                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                            UniqueName="DeleteColumn">
                                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                                            <ItemStyle  HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                                        </radG:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings UserControlName="EditTeam.ascx" EditFormType="WebUserControl">
                                                    </EditFormSettings>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px" />
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px" />
                                                    </RowIndicatorColumn>
                                                    <CommandItemSettings AddNewRecordText="Add Team Name" />
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
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
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Team?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
    </script>
</body>
</html>
