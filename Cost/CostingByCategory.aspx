<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CostingByCategory.aspx.cs" Inherits="SMEPayroll.Cost.CostingByCategory" %>

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





    <script type="text/javascript">
        //function Validate() {
        //    if (document.getElementById("dtp1").value == "") {
        //        alert("Please Select Date");
        //        return false;
        //    } else
        //        return true;
        //}

        function isNumericKeyStrokeDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                return false;

            return true;
        }
        function isNumericKeyStroke(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
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
                        <li>Define Costing Percentage</li>
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
                            <a href="CostingByCategoryIndex.aspx"><span>Costing By Category</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Define Costing Percentage</span>
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

                            </radG:RadCodeBlock>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>
                            <!------------------------------ start ---------------------------------->

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>As On Date</label>
                                        <telerik:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp1" runat="server">
                                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="rfvdtp1" ValidationGroup="ValidationSummary1" runat="server"
                                            ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                    </div>
                                    <%--<div class="form-group">
                                        <label>View History</label>
                                    </div>--%>
                                    <div class="form-group">
                                        <label>View History</label>
                                        <asp:DropDownList CssClass="form-control input-sm" ID="drphistorydate" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid1" runat="server">GO</asp:LinkButton>
                                    </div>
                                    <%--<div class="form-group">
                                        <label>Import</label>
                                    </div>--%>
                                    <div class="form-group">
                                        <label>Import</label>
                                        <asp:CheckBox ID="chkId" Text="Import From Excel" runat="server"
                                            OnCheckedChanged="chkId_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input id="FileUpload" runat="server" class="textfields btn" name="FileUpload"
                                            type="file" visible="false" />
                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="Server" ControlToValidate="FileUpload"
                                            ErrorMessage="Please Select xls Files" ValidationExpression=".+\.(([xX][lL][sS]))">*</asp:RegularExpressionValidator>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="ImageButton1"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server" Visible="false">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnTemplate" CssClass="btn default" runat="server" Text="Generate Template(.XLS)" OnClick="btnTemplate_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="margin-bottom-10">
                                <asp:Label ID="errormsg" runat="server" ForeColor="red" Visible="false"></asp:Label>
                                <asp:Label ID="lblerror" runat="server" Text="" Visible="false"></asp:Label>
                            </div>


                            <%-- OnNeedDataSource="RadGrid1_NeedDataSource"--%>
                            <%-- OnPageIndexChanged="RadGrid1_PageIndexChanged" --%>
                            <telerik:RadGrid ID="RadGrid1" runat="server" AutoGenerateColumns="false" ShowStatusBar="false"
                                Skin="Outlook" AllowMultiRowSelection="true" ItemStyle-Wrap="false"
                                AlternatingItemStyle-Wrap="false" PagerStyle-AlwaysVisible="True" GridLines="Both"
                                Font-Size="11" Font-Names="Tahoma" HeaderStyle-Wrap="false" AllowPaging="true"
                                PageSize="10000" PagerStyle-Visible="false" Width="100%" AllowSorting="true"
                                OnItemCreated="RadGrid1_ItemCreated" ShowFooter="true">
                                <MasterTableView DataKeyNames="Emp_Code, Time_Card_No" ShowFooter="true" TableLayout="Auto">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="25px" VerticalAlign="middle" />
                                    <AlternatingItemStyle Height="25px" VerticalAlign="middle" />
                                    <HeaderStyle Wrap="false" Height="25px" />
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="Emp_Code" UniqueName="Emp_Code" HeaderText="Emp_Code"
                                            Display="false">
                                            <%--<HeaderStyle Width="100px" />--%>
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FullName" UniqueName="FullName" HeaderText="FullName"
                                            ItemStyle-Wrap="false">
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Time_Card_No" UniqueName="Time_Card_No" HeaderText="Time_Card_No">
                                            <HeaderStyle Width="105px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column3">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column4">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column5">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column6">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column7">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column8">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column9">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column10">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column11">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column12">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column13">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column14">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column15">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column16">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column17">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column18">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column19">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column20">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column21">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column22">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column23">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <%--------------------------------------- new --column --%>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column24">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column25">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column26">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column27">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column28">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column29">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column30">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column31">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column32">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column33">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column34">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column35">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column36">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column37">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column38">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column39">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn Display="false" UniqueName="Column40">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderStyle Width="200px" />
                                        </telerik:GridTemplateColumn>
                                        <%------------------------------------- End new column --%>
                                        <telerik:GridTemplateColumn UniqueName="Total" HeaderText="Total %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <HeaderStyle Width="30px" />
                                        </telerik:GridClientSelectColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </telerik:RadGrid>
                            <!-------------------- end -------------------------------------->

                            <div class="text-center margin-top-20">
                                <asp:Button ID="btnValidate" CssClass="btn Default" runat="server" Text="Validate Total" OnClick="btnValidate_Click" />
                                <asp:Button ID="btnSubmit" CssClass="btn red" runat="server" Text="Submit " OnClick="btnSubmit_Click" Enabled="false" OnClientClick="return Validate();" />
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
        $(document).ready(function () {

            //$("table.rgMasterTable td input[type='text']").addClass("form-control input-sm");

            //$("table.rgMasterTable td input[type='text']").removeAttr("style");

            $('#btnValidate').click(function () {
                var _message = "";
                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _message += "At least one record must be selected from the grid <br>";
                if (_message != "") {
                    WarningNotification(_message);
                    return false;
                }
            });
            $('#btnSubmit').click(function () {
                var _message = "";
                if ($("#dtp1").val() == "") {
                    _message = "Please select As On Date <br>";
                }
                else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    _message = "At least one record must be selected from the grid <br>";
                }
                if (_message != "") {
                    WarningNotification(_message);
                    return false;
                }
            });
            $('#imgbtnfetch').click(function () {
                var _message = "";
                if ($.trim($("#drphistorydate option:selected").text()) === "-select date-")
                    _message += "Please Select View History";
                if (_message != "") {
                    WarningNotification(_message);
                    return false;
                }
            });
            //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red"); 
            window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }
        });
    </script>
</body>
</html>
