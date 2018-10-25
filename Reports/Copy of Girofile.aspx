<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Copy of Girofile.aspx.cs" Inherits="SMEPayroll.Reports.CopyGirofile" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script type="text/javascript">
        function SubmitForm() {
            if (ValidateDate()) {
                document.form1.submit();
            }
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
                        <li>Create Payment File</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Cheque Cash Export</span>
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
                            <asp:ScriptManager runat="server" ID="scriptManager">
                            </asp:ScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix no-margin">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                       <%-- <asp:DropDownList ID="cmbYear" runat="server" CssClass="textfields form-control input-sm" AutoPostBack="true"
                                            OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                            <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                            <asp:ListItem Value="2007">2007</asp:ListItem>
                                            <asp:ListItem Value="2008">2008</asp:ListItem>
                                            <asp:ListItem Value="2009">2009</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                            <asp:ListItem Value="2014">2014</asp:ListItem>
                                            <asp:ListItem Value="2015">2015</asp:ListItem>
                                            <asp:ListItem Value="2016">2016</asp:ListItem>
                                            <asp:ListItem Value="2017">2017</asp:ListItem>
                                            <asp:ListItem Value="2018">2018</asp:ListItem>
                                        </asp:DropDownList>--%>
                                        <asp:DropDownList ID="cmbYear" CssClass="trstandtop form-control input-sm input-small" runat="server" AutoPostBack="true" >
                                        </asp:DropDownList>
                                                        
                                                       
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
                                            <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Cheque/Cash</label>
                                        <asp:DropDownList ID="drpbank" AutoPostBack="false" runat="server" CssClass="textfields form-control input-sm"
                                            OnSelectedIndexChanged="drpbank_SelectedIndexChanged">
                                            <asp:ListItem Value="-5" Text="-Select-"></asp:ListItem>
                                            <asp:ListItem Value="-2" Text="Cheque"></asp:ListItem>
                                            <asp:ListItem Value="-1" Text="Cash"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Date</label>
                                        <radCln:RadDatePicker ID="radDtInput" runat="server" CssClass="trstandtop" Calendar-ShowRowHeaders="false"
                                            DateInput-Enabled="true">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid"  runat="server">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group hidden">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList ID="drpaccno" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group hidden">
                                        <label>&nbsp;</label>
                                        <asp:DropDownList ID="drpValueDate" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>



                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <div class="padding-tb-10 text-right">                              
                                <asp:LinkButton id="btnExport" class="btn btn-export"  runat="server">
                                        <i class="fa fa-file-excel-o font-red"></i>
                                    </asp:LinkButton>
                            </div>

                            <radG:RadGrid AllowPaging="false" Width="99%" ID="RadGrid1" CssClass="radGrid-single"
                                runat="server" GridLines="None" Skin="Outlook" AllowMultiRowSelection="True"
                                DataSourceID="SqlDataSource1">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="emp_id" DataSourceID="SqlDataSource1"
                                    PageSize="100" AllowPaging="false">

                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                    <Columns>
                                        <radG:GridBoundColumn DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id"
                                            UniqueName="emp_id" Visible="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Date" HeaderText="Date" SortExpression="Date"
                                            UniqueName="Date" Visible="True">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="To" SortExpression="emp_name"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DeptName" HeaderText="Dept Name" SortExpression="DeptName"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" Visible="False" UniqueName="DeptName">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="giro_bank" HeaderText="Bank Code" SortExpression="giro_bank" Visible="False"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="giro_bank">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="bank_name" HeaderText="Bank Name" SortExpression="bank_name" Visible="False"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="bank_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="giro_acct_number" HeaderText="Bank AccNo" CurrentFilterFunction="StartsWith" Visible="False"
                                            AutoPostBackOnFilter="True" SortExpression="giro_acct_number" UniqueName="giro_acct_number">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="payrate" HeaderText="Basic Pay/Pay Rate" CurrentFilterFunction="StartsWith" Visible="False"
                                            AutoPostBackOnFilter="True" SortExpression="payrate" UniqueName="payrate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="total_additions" HeaderText="Additions" CurrentFilterFunction="StartsWith" Visible="False"
                                            AutoPostBackOnFilter="True" SortExpression="total_additions" UniqueName="total_additions">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="total_deductions" HeaderText="Deductions" CurrentFilterFunction="StartsWith" Visible="False"
                                            AutoPostBackOnFilter="True" SortExpression="total_deductions" UniqueName="total_deductions">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="GrossPay" HeaderText="Amount" CurrentFilterFunction="StartsWith" Visible="True"
                                            AutoPostBackOnFilter="True" SortExpression="GrossPay" UniqueName="GrossPay">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Percentage" HeaderText="%Age" CurrentFilterFunction="StartsWith" Visible="False"
                                            AutoPostBackOnFilter="True" SortExpression="Percentage" UniqueName="Percentage">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="netpay" HeaderText="Amount" CurrentFilterFunction="StartsWith"
                                            AutoPostBackOnFilter="True" SortExpression="netpay" UniqueName="netpay" Visible="False">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Description" HeaderText="Description"
                                            AutoPostBackOnFilter="True" SortExpression="Description" UniqueName="Description">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Sp_get_Cheque_CashInfo"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="drpbank" Name="bank" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="radDtInput" Name="Date" PropertyName="SelectedDate"
                                        Type="DateTime" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <div class="text-center">
                                <asp:HiddenField ID="txthiddenbankvalue" runat="server" />
                                <asp:CheckBox Visible="false" ID="chkHash" CssClass="bodytxt" runat="server" Text="Hash Validation"></asp:CheckBox>
                                <asp:Button Visible="false" ID="btnsubmit" CssClass="btn red" CausesValidation="true" runat="server" Text="Generate Giro File" OnClick="btngenerate_Click" />
                            </div>






                            <center>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpbank"
                                    Display="None" ErrorMessage="Cheque/Cash Required!" InitialValue="-5"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radDtInput"
                                    Display="None" ErrorMessage="Date Required!"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" />--%>
                                <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                    <AjaxSettings>
                                        <radA:AjaxSetting AjaxControlID="drpbank">
                                            <UpdatedControls>
                                                <radA:AjaxUpdatedControl ControlID="drpaccno" />
                                            </UpdatedControls>
                                        </radA:AjaxSetting>
                                    </AjaxSettings>
                                </radA:RadAjaxManager>
                            </center>
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
    <script type="text/javascript" language="javascript">

        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
      }



      $(imgbtnfetch).click(function () {
          var alertmsg = "";
          if ($(drpbank).val() == "-5") {
              alertmsg = "Please select Payment Type <br/>";
          }
          if ($(radDtInput_dateInput_text).val() == "") {
              alertmsg += "Please select Date <br/>";
          }
          if ($(cmbYear).val() == "-1") {
              alertmsg += " Please select Year";

          }
          if (alertmsg != "") {
              WarningNotification(alertmsg);
              return false;
          }
      });



      $(btnExport).click(function () {
          var alertmsg = "";
          var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1) {
                alertmsg = "Please First Fetch some Records!! No Record to Export to Excel File";

            }
        //else {
        //    if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
        //        alertmsg = "Atleast one record must be selected from grid.";

        //    }
        //}

            if (alertmsg != "") {
                WarningNotification(alertmsg);
                return false;
            }

    });
    </script>
</body>
</html>
