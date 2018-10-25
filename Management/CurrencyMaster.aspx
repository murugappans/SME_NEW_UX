<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrencyMaster.aspx.cs" Inherits="SMEPayroll.Payroll.CurrencyMaster" %>

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
                        <li>Currency Master</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="MultiCurrency.aspx"><span>MultiCurrency</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Currency Master</span>
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
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>
                                <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
                                </script>
                                <script type="text/javascript">
                                    var type = "";
                                    var changedFlage = "false";

                                    //Leave Type
                                    function Validations(sender, args) {

                                        if (typeof (args) !== "undefined") {
                                            var commandName = args.get_commandName();
                                            var commandArgument = args.get_commandArgument();
                                            switch (commandName) {
                                                case "startRunningCommand":
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                                case "alertCommand":
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                                default:
                                                    $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                                    break;
                                            }
                                        }


                                        var result = args.get_commandName();
                                        if (type == "" && changedFlage == "false") {
                                            var itemIndex = args.get_commandArgument();
                                            var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                                            if (row != null) {
                                                cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                                                type = cellvalue;
                                            }
                                        }
                                        if (result == 'Update' || result == 'PerformInsert') {
                                            var sMsg = "";
                                            var message = "";
                                            message = MandatoryData(trim(type), "Leave Type");
                                            if (message != "")
                                                sMsg += message + "\n";

                                            if (sMsg != "") {
                                                args.set_cancel(true);
                                                alert(sMsg);
                                            }
                                        }
                                    }

                                    //Onlost Focus Of the Manual Leave Type
                                    function OnFocusLost_type(val) {
                                        var Object = document.getElementById(val);
                                        type = GetDataFromHtml(Object);
                                        changedFlage = "true";
                                    }
                                </script>


                            </radG:RadCodeBlock>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>


                            <radG:RadGrid ID="RadGrid1" AllowFilteringByColumn="true" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnItemDataBound="RadGrid1_ItemDataBound" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                AllowAutomaticDeletes="True" runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                Skin="Outlook" Width="99%">
                                <MasterTableView CommandItemDisplay="Bottom" DataSourceID="SqlDataSource1" AutoGenerateColumns="False">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image11" ImageUrl="../frames/images/LEAVES/Grid-leavetypes.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="35px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle Width="35px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" Visible="false" DataField="id"
                                            DataType="System.Int32" UniqueName="id" SortExpression="id" HeaderText="Id">
                                            <%--<ItemStyle Width="10px"></ItemStyle>--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" Visible="false" DataField="Selected"
                                            DataType="System.Int32" UniqueName="Selected" SortExpression="Selected" HeaderText="Selected">
                                            <%--<ItemStyle Width="10px"></ItemStyle>--%>
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="Currency" AllowFiltering="false" UniqueName="Currency"
                                            HeaderText="Currency">
                                            <%--<ItemStyle HorizontalAlign="left" Width="80%"></ItemStyle>--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <%--<ItemStyle Width="30px"></ItemStyle>--%>
                                        </radG:GridEditCommandColumn>
                                        <radG:GridClientDeleteColumn ButtonType="ImageButton" UniqueName="DeleteColumn">
                                        </radG:GridClientDeleteColumn>

                                        <radG:GridTemplateColumn HeaderText="Currency Selected"  UniqueName="Selected" AllowFiltering="false" ItemStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCurrencyId" ToolTip='<%# Bind( "id" ) %>' runat="server" AutoPostBack="true" OnCheckedChanged="CheckChanged" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="150px" HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle Width="150px" HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                    <CommandItemSettings AddNewRecordText="Add New Leave Type" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <ClientEvents OnCommand="Validations" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                SelectCommand="SELECT [id],[Currency] + '--->' + [Symbol] Currency, [Symbol],[Selected] FROM [Currency] order by Currency Asc"></asp:SqlDataSource>
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
        //$("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");

    </script>
</body>
</html>
