<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdditionType.aspx.cs" Inherits="SMEPayroll.Management.AdditionType" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
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

    <script language="javascript">
        function EnableApproval() {
            var txthid = document.getElementById('txtRadId').value + "_";
            document.getElementById(txthid + 'txtiras_approval_date').value = '';

            if (document.getElementById(txthid + 'drpiras_approval').value == "Yes" || document.getElementById(txthid + 'drpiras_approval').value == "YES") {
                document.getElementById(txthid + 'tr5').style.display = "block";
            }
            else {
                document.getElementById(txthid + 'tr5').style.display = "none";
            }

        }
        function EnablePayableOtions() {
            var txthid = document.getElementById('txtRadId').value + "_";
            var oOption = document.getElementById('drplumsumswitch');
            var strOptions = oOption.options[oOption.selectedIndex].text;
            var strCompare = "," + document.getElementById(txthid + 'drptax_payable_options').value + ",";

            var rfv1 = document.getElementById(txthid + 'rfvtxtbasis_arriving_payment');
            var rfv2 = document.getElementById(txthid + 'rfvtxtservice_length');
            var rfv3 = document.getElementById(txthid + 'rfvtxtiras_approval_date');

            if (strOptions.indexOf(strCompare) >= 0) {
                document.getElementById(txthid + 'tr2').style.display = "block";
                document.getElementById(txthid + 'tr3').style.display = "block";
                document.getElementById(txthid + 'tr4').style.display = "block";
                document.getElementById(txthid + 'tr5').style.display = "none";
                var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                oSwitch.selectedIndex = 0;
            }
            else {
                document.getElementById(txthid + 'tr2').style.display = "none";
                document.getElementById(txthid + 'tr3').style.display = "none";
                document.getElementById(txthid + 'tr4').style.display = "none";
                document.getElementById(txthid + 'tr5').style.display = "none";
                var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                oSwitch.selectedIndex = 0;
            }
        }

        function EnablePayable() {
            var txthid = document.getElementById('txtRadId').value + "_";
            var rfv1 = document.getElementById(txthid + 'rfvfcal');
            var oSwitch = document.getElementById(txthid + 'drptax_payable_options');
            oSwitch.selectedIndex = 0;
            if (document.getElementById(txthid + 'drptax_payable').value == "Yes" || document.getElementById(txthid + 'drptax_payable').value == "YES") {
                document.getElementById(txthid + 'tr1').style.display = "block";
                if (rfv1 != null) {
                    provinceValidator = document.getElementById(txthid + 'rfvfcal');
                    ValidatorEnable(provinceValidator, true);
                }
            }
            else {
                document.getElementById(txthid + 'tr1').style.display = "none";
                if (rfv1 != null) {
                    provinceValidator = document.getElementById(txthid + 'rfvfcal');
                    ValidatorEnable(provinceValidator, true);
                }
            }
        }
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
                        <li>Addition Types</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ShowDropdowns.aspx">Manage Settings</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Additions</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Addition Types</h3>--%>
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
                            <div>
                                <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                    border="0">
                                    <tr style="display: none;">
                                        <td>
                                            <asp:DropDownList ID="drplumsumswitch" DataTextField="text" DataValueField="id" DataSourceID="XmldtTaxPayableTypeLumSumSwtich"
                                                class="textfields" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                                <radG:RadGrid ID="RadGrid1" AllowPaging="true" AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand" OnItemCommand ="RadGrid1_ItemCommand" 
                                    OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand" PagerStyle-AlwaysVisible ="true"
                                    PageSize="20" OnItemDataBound="RadGrid1_ItemDataBound" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="None" Skin="Outlook" Width="93%" PagerStyle-Mode="NextPrevAndNumeric">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,cpf,company_id" CommandItemDisplay="Bottom">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>

                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                <ItemTemplate>
                                                    <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>

                                            <radG:GridBoundColumn DataField="optionselection" AllowFiltering="false" HeaderText="Addition Type"
                                                SortExpression="OptionSelection" UniqueName="OptionSelection">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="desc" CurrentFilterFunction="StartsWith" AutoPostBackOnFilter="True" ShowFilterIcon="false"
                                                HeaderText="Addition Type Description" SortExpression="desc" UniqueName="desc" FilterControlAltText="cleanstring">
                                                <%--<ItemStyle Width="500px" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="TypeOfWageDesc" AllowFiltering="false" HeaderText="Type Of Wage"
                                                UniqueName="TypeOfWageDesc">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="cpf" HeaderText="CPF" AllowFiltering="false" SortExpression="cpf"
                                                UniqueName="cpf">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Visible="false" DataField="company_id" HeaderText="company_id"
                                                SortExpression="company_id" UniqueName="company_id">
                                                <%--<ItemStyle Width="150px" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Visible="false" DataField="optionselection" HeaderText="OptionSelection"
                                                SortExpression="OptionSelection" UniqueName="OptionSelection">
                                                <%--<ItemStyle Width="150px" />--%>
                                            </radG:GridBoundColumn>


                                            <radG:GridBoundColumn DataField="ClaimCash" AllowFiltering="false" HeaderText="Petty Cash"
                                                UniqueName="ClaimCash">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn DataField="Prorated" AllowFiltering="false" HeaderText="Prorated"
                                                UniqueName="Prorated">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="IsSDL" AllowFiltering="false" HeaderText="Compute SDL"
                                                UniqueName="ISSDL">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="IsFund" AllowFiltering="false" HeaderText="Compute Fund"
                                                UniqueName="IsFund">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn DataField="isGrosspay" AllowFiltering="false" HeaderText="Compute Encash"
                                                UniqueName="isGrosspay">
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="tax_payable" AllowFiltering="false" HeaderText="Taxable"
                                                UniqueName="Tax">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Active" AllowFiltering="false" HeaderText="Active"
                                                UniqueName="Active">
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridEditCommandColumn>
                                            <radG:GridButtonColumn ButtonType="ImageButton"
                                                ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                UniqueName="DeleteColumn">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridButtonColumn>

                                            <radG:GridBoundColumn DataField="ClaimCash" Visible="false" UniqueName="ClaimCash">
                                                <%--<ItemStyle Width="150px" />--%>
                                            </radG:GridBoundColumn>
                                        </Columns>
                                        <ExpandCollapseColumn Visible="False">
                                            <%--<HeaderStyle Width="19px" />--%>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <%--<HeaderStyle Width="20px" />--%>
                                        </RowIndicatorColumn>
                                        <EditFormSettings UserControlName="addition_edit.ascx" EditFormType="WebUserControl">
                                        </EditFormSettings>
                                        <CommandItemSettings AddNewRecordText="Add New Addition Type" />
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                </radG:RadGrid>

                            </div>
                            <input type="hidden" runat="server" id="txtRadId" />
                            <input type="hidden" runat="server" id="txtLumSum" />
                            <asp:XmlDataSource ID="XmldtTaxPayableTypeLumSumSwtich" runat="server" DataFile="~/XML/xmldata.xml"
                                XPath="SMEPayroll/Tax/TaxPayableTypeLumSumSwtich"></asp:XmlDataSource>
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
            GetConfirmation("Are you sure you want to delete this Addition Type?", _id, "Confirm Delete", "Delete");
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
