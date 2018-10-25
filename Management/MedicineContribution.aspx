<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicineContribution.aspx.cs" Inherits="SMEPayroll.Management.MedicineContribution" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
                        <li>Additional Medisave Contribution Scheme</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAMC.aspx">AMCS</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Setup AMCS</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Additional Medisave Contribution Scheme</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                           <%-- <div class="search-box clearfix">
                                <div class="col-md-12 text-right">

                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                            <radG:RadGrid ID="RadGrid4"  runat="server" GridLines="None" Skin="Outlook" Width="95%" OnItemCommand ="RadGrid4_ItemCommand" 
                                OnInsertCommand="RadGrid4_InsertCommand" OnNeedDataSource="RadGrid4_NeedDataSource"
                                OnUpdateCommand="RadGrid4_UpdateCommand" OnDeleteCommand="RadGrid4_DeleteCommand">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,CSN" CommandItemDisplay="Bottom">
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridBoundColumn Visible="false" DataField="id" UniqueName="id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CSN" HeaderText="CSN" SortExpression="CSN">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="FormulaId" HeaderText="Formula Id" SortExpression="FormulaId">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="FormulaType" HeaderText="Formula Type" SortExpression="FormulaType">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Formula" HeaderText="Formula / Amount" SortExpression="Formula">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AMCSLimit" HeaderText="AMCS Max Limit" SortExpression="AMCSLimit">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AMCSMinLimit" HeaderText="AMCS Min Limit" SortExpression="AMCSLimit">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AMCSRound" HeaderText="AMCS Rounding Option" SortExpression="AMCSRound">
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn Visible="true" ButtonType="ImageButton">
                                             <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                             <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <CommandItemSettings AddNewRecordText="Add New CSN details" />
                                    <EditFormSettings UserControlName="MedicalControl.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
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
           window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
    </script>
</body>
</html>
