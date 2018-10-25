<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryChange.aspx.cs" Inherits="SMEPayroll.Management.SalaryChange" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
        function expando()
        {
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
                        <li>Salary Update</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/AssignmentManagement.aspx">Manage Assignments</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Salary Increment</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Salary Update</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script language="javascript" type="text/javascript">
                                    function Validation() {
                                        alertmsg = "";
                                        if (document.getElementById('<%=rdFrom.ClientID %>').value == "") {
                                            alertmsg += "Please Enter Effective Date <br/>";

                                        }
                                        if (document.getElementById('<%=drpList.ClientID %>').value == "-1") {
                                            alertmsg += "Please Select Formula <br/>";

                                        }
                                        if (document.getElementById('<%=drpList.ClientID %>').value == "Percentage" && document.getElementById('<%=txtAmount.ClientID %>').value == "") {
                                            alertmsg += "Please Enter the  Percentage <br/>";

                                        }
                                        if (document.getElementById('<%=drpList.ClientID %>').value == "Fixed" && document.getElementById('<%=txtAmount.ClientID %>').value == "") {
                                            alertmsg += "Please Enter the  Fixed Amount <br/>";

                                        }
                                        if (alertmsg != "") {
                                            WarningNotification(alertmsg);
                                            return false;
                                        }

                                    }
                                </script>
                            </radG:RadCodeBlock>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>


                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label>Effective Date</label>
                                        <radCln:RadDatePicker ID="rdFrom" runat="server" CssClass="trstandtop input-small" Calendar-ShowRowHeaders="false"
                                            DateInput-Enabled="true" AutoPostBack="true">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                        <%--<telerik:RadDatePicker ID="rdFrom" runat="server">
                                        </telerik:RadDatePicker>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Formula</label>
                                        <asp:DropDownList ID="drpList" runat="server" CssClass="textfields form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="drpList_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select--" Value="-1"> </asp:ListItem>
                                            <asp:ListItem Text="% of Basic Salary" Value="Percentage"> </asp:ListItem>
                                            <asp:ListItem Text="Fixed Amount" Value="Fixed"> </asp:ListItem>
                                            <asp:ListItem Text="Manual" Value="Manual"> </asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label id="lblAmount" runat="server" visible="false">Value</label>
                                        <asp:TextBox ID="txtAmount" runat="server" Visible="false" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnView" CssClass="btn red btn-circle btn-sm" OnClick="imgbtnView_Click" OnClientClick="return Validation();" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                            <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                                Skin="Outlook" Width="99%" runat="server" GridLines="None" AllowPaging="true"
                                AllowMultiRowSelection="true" PageSize="50" OnItemDataBound="RadGrid1_ItemDataBound"
                                OnPageIndexChanged="RadGrid1_PageIndexChanged"
                                OnItemCommand="RadGrid1_ItemCommand">
                                <MasterTableView CommandItemDisplay="bottom" EditMode="InPlace" AutoGenerateColumns="False"
                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
                                    TableLayout="Auto" DataKeyNames="emp_code">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <CommandItemTemplate>
                                        <%--to get the button in the grid header--%>
                                        <div class="textfields" style="text-align: center">
                                            <asp:Button ID="btnUpdate" runat="server" class="textfields btn red"
                                                Text="Update Selected" CommandName="UpdateAll" />
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn" HeaderStyle-Width="10%">
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_code" Display="false" DataType="System.Int32"
                                            HeaderText="emp_code" SortExpression="emp_code" UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="TimeCardId" DataType="System.String"
                                            UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" ShowFilterIcon="false" FilterControlAltText="cleanstring" HeaderText="Time Card NO">
                                            <ItemStyle Width="150px" />
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" ShowFilterIcon="false" FilterControlAltText="alphabetsonly" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <%--<ItemStyle Width="50%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Dept" ShowFilterIcon="false" FilterControlAltText="alphabetsonly" HeaderText="Department" SortExpression="Dept"
                                            UniqueName="Dept" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <%--<ItemStyle Width="20%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="Trade"
                                            UniqueName="Trade" Visible="true" SortExpression="Trade" ShowFilterIcon="false" FilterControlAltText="alphabetsonly" HeaderText="Trade">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="basicPay" ShowFilterIcon="false" FilterControlAltText="number-dot" HeaderText="Basic Pay" SortExpression="basicPay"
                                            UniqueName="basicPay">
                                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="120px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="NewbasicPay"
                                            UniqueName="NewbasicPay" HeaderText="New Basic pay" AllowFiltering="false">
                                            <ItemStyle HorizontalAlign="center" CssClass="number-dot" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewBasicpay" onkeyup="javascript:return validatenumbers(this);"
                                                    onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" CssClass="form-control input-sm"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NewBasicpay")%>'></asp:TextBox>
                                                <%--     <asp:RegularExpressionValidator ID="vldNH" ControlToValidate="txtNewBasicpay" Display="Dynamic"
                                ErrorMessage="*"   ValidationExpression="^\d{0,6}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="120px" HorizontalAlign="Right" />
                                            <HeaderStyle Width="120px" />
                                        </radG:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                    ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid><%--ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,3}(\.\d{1,3})?$"--%>
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
        $("#RadGrid1_ctl00 input[type='text']").addClass("number-dot");
        $("#RadGrid1_ctl00 input[type='text']").attr("maxlength", "12");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
             var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
             $.each(_inputs, function (index, val) {
                 $(this).addClass($(this).attr('alt'));

             })
         }
        $("#RadGrid1_ctl00_ctl03_ctl01_btnUpdate").click(function () {
            var _msg = "";
            var grid = $find("<%= RadGrid1.ClientID %>");
             var rowCount = grid.get_masterTableView().get_dataItems().length;
             if (rowCount < 1)
                 _msg += "No  records  found to update.  <br/>";
             else
                 if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                     _msg += "Atleast one record must be selected to update. <br/>";

             if (_msg != "") {
                 WarningNotification(_msg);
                 return false;
             }


         });

    </script>

</body>
</html>
