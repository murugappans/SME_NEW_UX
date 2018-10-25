<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DailyAttendance.aspx.cs"
    Inherits="SMEPayroll.Management.DailyAttendance" %>
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



    <script type="text/javascript" language="javascript">

        function validateform() {

            if (!document.form1.RadDatePicker1.value) {
                alert("Please Select Date");
                return false;
            }
            else
                return true;
        }

        function storeoldval(val) {
            document.getElementById('txthid').value = val;
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
    <uc1:toprightcontrol id="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:topleftcontrol id="TopLeftControl" runat="server" />
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
                        <li>
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Daily Attendance</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server" />

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-10">
                                    <div class="form-group">
                                        <label>Effective Date</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server"
                                            Width="169px">
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnReport" runat="server" class="textfields btn default btn-sm" 
                                            Text="Report" OnClick="btnReport_Click" />
                                    </div>

                                </div>
                                <div class="col-md-2 text-right">
                                    <label>&nbsp;</label>
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn red btn-sm" type="button">
                                </div>
                            </div>

                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                                OnItemCreated="RadGrid1_ItemCreated" OnItemCommand="RadGrid1_ItemCommand" Skin="Outlook"
                                Width="99%" runat="server" GridLines="None" AllowPaging="true" AllowMultiRowSelection="true"
                                PageSize="50">
                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="ID" EditMode="InPlace"
                                    AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    AllowAutomaticDeletes="true" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <CommandItemTemplate>
                                        <%--to get the button in the grid header--%>
                                        <div class="textfields" style="text-align: center">
                                            <asp:Button ID="btnsubmit" runat="server" class="textfields btn red" 
                                                Text="Submit" CommandName="UpdateAll" />
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="2px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="ID" Display="false" DataType="System.Int32" HeaderText="ID"
                                            SortExpression="ID" UniqueName="ID">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Sub_Project_Name" HeaderText="Sub Project" SortExpression="Sub_Project_Name"
                                            UniqueName="Sub_Project_Name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <ItemStyle  />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Total" UniqueName="Shift"
                                            HeaderText="Shift" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="drpShift" runat="server" CssClass="form-control input-sm">
                                                    <asp:ListItem Text="Day" Value="D"></asp:ListItem>
                                                    <asp:ListItem Text="Night" Value="N"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Total" UniqueName="Total"
                                            HeaderText="Total" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotal" onkeypress="return isNumericKeyStroke(event);" 
                                                    CssClass="form-control input-sm text-right" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Total")%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="vld1" ControlToValidate="txtTotal" Display="Dynamic"
                                                    ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                                    runat="server"> 
                                                </asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Remark"
                                            UniqueName="Remark" HeaderText="Remark" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark" CssClass="form-control input-sm text-right" runat="server"
                                                    Text='<%# DataBinder.Eval(Container,"DataItem.Remark")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="PIC" UniqueName="PIC"
                                            HeaderText="PIC" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPIC" CssClass="form-control input-sm text-right" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.PIC")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                        </radG:GridClientSelectColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>

                            <asp:Label ID="lblMessage" class="bodytxt" runat="server" Text=""></asp:Label>
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
