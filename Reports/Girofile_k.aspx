<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Girofile_k.aspx.cs" Inherits="SMEPayroll.Reports.Girofile_k" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script type="text/javascript"
        language="javascript"
        src="../scripts/jquery-1.6.2.min.js"></script>
    <script type="text/javascript"
        language="javascript"
        src="../scripts/jquery.json-2.2.min.js"></script>

    <script type="text/javascript">
        function SubmitForm() {
            if (ValidateDate()) {
                document.form1.submit();
            }
        }

        function keyPress(sender, args) {
            var text = sender.get_value() + args.get_keyCharacter();
            if (!text.match('^[0-9]+$'))
                args.set_cancel(true);
        }


    </script>

    <%--<script type="text/javascript">
$('#<%= drpbank.ClientID %>').change(function () {
if (this.value == 'UOB') {
$('#<%= batch_no.ClientID %>').css('display', 'block');
}
else {
$('#<%= batch_no.ClientID %>').css('display', 'none');
}
});
</script>--%>





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
                        <li>Create Bank File</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Bank Giro</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Create Bank File</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>


                            <script type="text/javascript">
                                //    function GetSetValue() {
                                //     
                                //        $telerik.$('#batch_no').val('My text')
                                //    }
                            </script>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                            <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm" OnSelectedIndexChanged="cmbMonth_SelectedIndexChanged" AutoPostBack="true">
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
                                        <label>Bank</label>
                                        <asp:DropDownList ID="drpbank" AutoPostBack="true" runat="server" CssClass="textfields form-control input-sm"
                                            OnSelectedIndexChanged="drpbank_SelectedIndexChanged" onchange="hidecheckbox(this.value)">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Bank AccNo</label>
                                        <asp:DropDownList ID="drpaccno" runat="server" AutoPostBack="false" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Value Date</label>
                                        <asp:DropDownList ID="drpValueDate" runat="server" CssClass="textfields form-control input-sm" Visible="false">
                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                            <asp:ListItem Text="31" Value="31"></asp:ListItem>
                                        </asp:DropDownList>
                                        <radG:RadDatePicker  Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                            ID="rddate" runat="server">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>Batch No</label>
                                        <telerik:RadTextBox ID="batch_no" runat="server" CssClass="form-control input-sm">
                                            <ClientEvents OnKeyPress="keyPress" />
                                        </telerik:RadTextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
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

                            <radG:RadGrid AllowPaging="false" Width="100%" AllowFilteringByColumn="true" ID="RadGrid1" CssClass="radGrid-single"
                                runat="server" GridLines="None" Skin="Outlook" AllowMultiRowSelection="True"
                                DataSourceID="SqlDataSource1" EnableHeaderContextMenu="true" AllowSorting="true">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="emp_id" DataSourceID="SqlDataSource1"
                                    PageSize="1000" AllowPaging="false" TableLayout="Fixed">
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
                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_id" HeaderText="emp_id" SortExpression="emp_id"
                                            UniqueName="emp_id" Visible="false">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="emp_name" AllowFiltering="true" FilterControlAltText="alphabetsonly">
                                            <HeaderStyle Width="250px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DeptName" HeaderText="Dept Name" SortExpression="DeptName" FilterControlAltText="alphabetsonly"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="DeptName">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="giro_bank" HeaderText="Bank Code" SortExpression="giro_bank"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="giro_bank" FilterControlAltText="numericonly">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="bank_name" HeaderText="Bank Name" SortExpression="bank_name" FilterControlAltText="alphabetsonly"
                                            CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" UniqueName="bank_name">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="giro_acct_number" HeaderText="Bank AccNo" CurrentFilterFunction="StartsWith" FilterControlAltText="cleanstring"
                                            AutoPostBackOnFilter="True" SortExpression="giro_acct_number" UniqueName="giro_acct_number">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="payrate" HeaderText="Basic Pay/Pay Rate" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="payrate" UniqueName="payrate">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="total_additions" HeaderText="Additions" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="total_additions" UniqueName="total_additions">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="NH_e" HeaderText="NH" CurrentFilterFunction="StartsWith"
                                            AutoPostBackOnFilter="True" SortExpression="NH" UniqueName="NH">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT1_e" HeaderText="OT1" CurrentFilterFunction="StartsWith"
                                            AutoPostBackOnFilter="True" SortExpression="OT1" UniqueName="OT1">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OT2_e" HeaderText="OT2" CurrentFilterFunction="StartsWith"
                                            AutoPostBackOnFilter="True" SortExpression="OT2" UniqueName="OT2">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>



                                        <radG:GridBoundColumn DataField="total_deductions" HeaderText="Deductions" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="total_deductions" UniqueName="total_deductions">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="GrossPay" HeaderText="NetPay" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="GrossPay" UniqueName="GrossPay">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Percentage" HeaderText="%Age" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="Percentage" UniqueName="Percentage">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="netpay" HeaderText="NetPay" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="netpay" UniqueName="netpay">
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" CurrentFilterFunction="StartsWith" FilterControlAltText="alphabetsonly"
                                            AutoPostBackOnFilter="True" SortExpression="Trade" UniqueName="Trade">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="BUnit" HeaderText="Business Unit" CurrentFilterFunction="StartsWith"
                                            AutoPostBackOnFilter="True" SortExpression="BUnit" UniqueName="BUnit">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="region" HeaderText="Region" CurrentFilterFunction="StartsWith" FilterControlAltText="alphabetsonly"
                                            AutoPostBackOnFilter="True" SortExpression="region" UniqueName="region">
                                            <HeaderStyle Width="150px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="cost" HeaderText="Cost" CurrentFilterFunction="StartsWith" FilterControlAltText="numericonly"
                                            AutoPostBackOnFilter="True" SortExpression="cost" UniqueName="cost">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                </ClientSettings>
                            </radG:RadGrid>


                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_get_giro_emp"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="drpbank" Name="bank" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="drpaccno" Name="bankaccno" PropertyName="SelectedValue"
                                        Type="string" />
                                    <asp:ControlParameter ControlID="drpValueDate" Name="valuedate" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <div class="padding-tb-20 text-center">
                                <asp:HiddenField ID="txthiddenbankvalue" runat="server" />
                                <asp:CheckBox ID="chkHash" CssClass="bodytxt" runat="server" Visible="false"></asp:CheckBox>

                                <asp:Label ID="Label1" runat="server" CssClass="bodytxt" Text="New Format" Visible="true"></asp:Label>
                                <asp:CheckBox ID="newformate" CssClass="bodytxt" runat="server" Visible="true" OnCheckedChanged="newformate_CheckedChanged" AutoPostBack="true"></asp:CheckBox>

                                <asp:Label ID="Label_G3" runat="server" CssClass="bodytxt" Text="G3 Format"></asp:Label>
                                <asp:CheckBox ID="g3format" CssClass="bodytxt" runat="server" OnCheckedChanged="g3format_CheckedChanged" AutoPostBack="true"></asp:CheckBox>


                                <asp:Label ID="hashlabel" runat="server" CssClass="bodytxt" Text="Hash Validation" Visible="false"></asp:Label>
                                <asp:Button ID="btnsubmit" CausesValidation="true" runat="server" Text="Generate Giro File" OnClick="btngenerate_Click" CssClass="btn red" />
                            </div>

                            <center>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpbank"
                                    Display="None" ErrorMessage="Bank Name Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpaccno"
                                    Display="None" ErrorMessage="Bank AccNo Required!" InitialValue="-select-"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cmbMonth"
                                    Display="None" ErrorMessage="Month Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="cmbYear"
                                    Display="None" ErrorMessage="Year Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="True" ShowSummary="False" />--%>
                                <%--  <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <radA:AjaxSetting AjaxControlID="drpbank">
                        <UpdatedControls>
                            <radA:AjaxUpdatedControl ControlID="drpaccno" />
                        </UpdatedControls>
                    </radA:AjaxSetting>
                </AjaxSettings>
            </radA:RadAjaxManager>--%>
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

    <script type="text/javascript">
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

              var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
              $.each(_inputs, function (index, val) {
                  $(this).addClass($(this).attr('alt'));

              })
              // $('#RadGrid1_ctl00 thead tr td').find('input[type=text]').addClass($('#RadGrid1_ctl00 thead tr td').find('input[type=text]').attr('alt'));
          }

          $("input[type='button']").removeAttr("style");
          $(btnsubmit).click(function () {
              var alertmsg = "";
              var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1) {
                alertmsg = "Please First Fetch some Records!! No Record to Generate GIRO File";

            }
            else {
                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    alertmsg = "Atleast one record must be selected from grid.";

                }
            }

            if (alertmsg != "") {
                WarningNotification(alertmsg);
                return false;
            }

        });
        $(imgbtnfetch).click(function () {
            var alertmsg = "";
            if ($(drpbank).val() == "-1") {
                alertmsg = "Please select Bank <br/>";
            }
            if ($(drpaccno).val() == "-select-") {
                alertmsg += "Please select Bank account Number <br/>";
            }
            if ($(cmbYear).val() == "-1") {
                alertmsg += " Please select Year";

            }
            if (alertmsg != "") {
                WarningNotification(alertmsg);
                return false;
            }
        });
    </script>

    <script type="text/javascript">
        function hidecheckbox(value) {
            //alert(value);
            GetSetValue(value);

            //     if(value="9")
            //     {
            //     alert(value);
            //     var ck='<%=g3format.ClientID %>';
            //     alert(ck);
            //     var cntrl= document.getElementById(ck);
            //     cntrl.style.visibility = 'visible';
            //     alert(value);
            //     }
            //     else
            //     {
            //     cntrl.style.visibility = 'hidden';
            //     }

            //     $.ajax (
            //{
            //type: "POST",
            //url: "Girofile_k.aspx/ishash",
            //data: "{value:"+value+"}",
            //contentType: "application/json",
            //charset:"utf-8",
            //dataType:"json",
            //async: true,
            //cache: false,
            //success: function (msg) 
            //{
            //var cntrl= document.getElementById('<%= chkHash.ClientID %>');
            //var lb_cntrl= document.getElementById('<%= hashlabel.ClientID %>');

            //        if(msg==true)
            //        {

            //  // cntrl.style.display = 'none';
            //  cntrl.style.visibility = 'visible';
            //   lb_cntrl.style.visibility = 'visible';
            //  
            //   }
            //   else
            //   {
            //   // cntrl.style.display = '';
            //   cntrl.style.visibility = 'hidden';
            //    lb_cntrl.style.visibility = 'hidden';
            // 
            //   }
            //},
            //error: function (x, e)
            //{
            //alert("The call to the server side failed. " + x.responseText);
            //}
            //}
            //);
            //return false;

        };


        function GetSetValue(value) {
            var radTextBox1 = $find("<%= batch_no.ClientID %>");

            radTextBox1.set_value('');

            if (value == "4") {
                radTextBox1.set_value('00001');
            }
            else {
                radTextBox1.set_value('01');
            }

        }

    </script>


</body>
</html>
