<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollCeilingMaster.aspx.cs" Inherits="SMEPayroll.Payroll.PayrollCeilingMaster" %>

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
                        <li>Payroll Ceiling</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="EmpCeilingMaster.aspx"><span>Payroll Ceiling</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Payroll Ceiling</span>
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
                                                sMsg += message + "<br/>";

                                            if (sMsg != "") {
                                                args.set_cancel(true);
                                                WarningNotifications(sMsg);
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
                                AllowAutomaticDeletes="True" runat="server" GridLines="None"
                                Skin="Outlook" >
                                <MasterTableView CommandItemDisplay="Bottom" AutoGenerateColumns="False">
                                    <CommandItemTemplate>
                                        <div class="text-center">
                                            <asp:Button ID="btnSubmit" runat="server" class="textfields btn red"
                                            Text="Submit" CommandName="SubmitData" />
                                        </div>
                                    </CommandItemTemplate>
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>
                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" Visible="false" DataField="id" Display="false"
                                            DataType="System.Int32" UniqueName="id" SortExpression="id" HeaderText="Id">
                                            <%--<ItemStyle Width="0px"></ItemStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" DataField="Parameter" Visible="false"
                                            UniqueName="Parameter" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Parameter">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </radG:GridBoundColumn>


                                        <radG:GridTemplateColumn DataField="Parameter" UniqueName="Parameter" HeaderText="Parameter" 
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox
                                                        Text='<%# DataBinder.Eval(Container,"DataItem.Parameter")%>'
                                                        ID="txtParameter" CssClass="form-control input-sm custom-maxlength" MaxLength="50"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn HeaderText="Hours/Amount" UniqueName="HRAMT" AllowFiltering="false" >
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="radPara" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Hours" Value="Hours"></asp:ListItem>
                                                    <asp:ListItem Text="Amount" Value="Amount"></asp:ListItem>
                                                    <asp:ListItem Text="None" Selected="True" Value="None"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                            <ItemStyle Width="300px"></ItemStyle>
                                            <HeaderStyle Width="300px"></HeaderStyle>
                                        </radG:GridTemplateColumn>


                                        <radG:GridTemplateColumn Visible="false" HeaderText="Hours"  UniqueName="CeilingTypeHR" AllowFiltering="false" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCeilingHr" ToolTip='<%# Bind( "idNew" ) %>' runat="server" />
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn Visible="false" HeaderText="Amount"  UniqueName="CeilingTypeRate" AllowFiltering="false" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCeilingRate" ToolTip='<%# Bind( "idNew" ) %>' runat="server" />
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>


                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" Visible="false" DataField="CeilingType"
                                            UniqueName="CeilingType" HeaderStyle-HorizontalAlign="Center" HeaderText="Parameter">
<%--                                            <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" DataField="AddType"
                                            UniqueName="AddType" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center" HeaderText="AddType">
                                            <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                            <HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
                                        </radG:GridBoundColumn>

                                    </Columns>
                                    <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />

                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="300px" SaveScrollPosition="True" />
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
         window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }
    </script>
</body>
</html>
