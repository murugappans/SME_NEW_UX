<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignAccomadation.aspx.cs"
    Inherits="SMEPayroll.Management.AssignAccomadation" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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

    <script language="javascript" type="text/javascript">
        function ValidateForm() {
            var sMSG = "";
            var ctrlDt;
            var ctrlDt2;
            ctrlDt = document.getElementById('drpDormetry');
            if (ctrlDt.value == '- Select -')
                sMSG += "Select Accommodation <br/>";
            if (document.form1.drpCheckIN.value == '-1')
                sMSG += "Select Either Check In / Check Out <br/>";
            ctrlDt = document.getElementById('RadDatePicker2');
            if (ctrlDt.value == '') {
                sMSG += "Accomadation Date should not be blank  <br/>";
            }
            if (sMSG == "") {
                var empCode = document.form1.drpname.value;
                ctrlDt = document.getElementById('drpCheckIN');
                var res1 = SMEPayroll.Management.AssignAccomadation.validateCheckInCheckOut(empCode, ctrlDt.value);
                if (res1.value == 'False') {
                    sMSG += "Select Check In";
                    WarningNotification(sMSG);
                    return false;
                }
                ctrlDt = document.getElementById('RadDatePicker2');
                ctrlDt2 = document.getElementById('drpCheckIN');
                var res = SMEPayroll.Management.AssignAccomadation.verifyCheckInDate(ctrlDt.value, empCode, ctrlDt2.value);
                if (res.value == 'False') {
                    sMSG += "Invalid Checkin / CheckOut Date, Select Future Date  <br/>";
                    WarningNotification(sMSG);
                    return false;
                }
                else {
                    if (document.form1.drpCheckIN.value == '2') {
                        var ctrl = document.getElementById('drpDormetry');

                        var res = SMEPayroll.Management.AssignAccomadation.validateDormetry(empCode, ctrl.value, '2');
                        if (res.value == 'False') {
                            sMSG += "Select Check In";
                            WarningNotification(sMSG);
                            return false;
                        }

                    }
                    if (document.form1.drpCheckIN.value == '1') {
                        var ctrl = document.getElementById('drpDormetry');

                        var res = SMEPayroll.Management.AssignAccomadation.validateDormetry(empCode, ctrl.value, '1');

                        if (res.value == 'False') {
                            sMSG += "Invalid Check In";
                            WarningNotification(sMSG);
                            return false;
                        }

                    }

                    return true;
                }
            }
            else {
                sMSG = "Following fields are missing. <br/>" + sMSG;
                WarningNotification(sMSG);
                return false;
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
                        <li>Accommodation Assignment Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAccomadation.aspx">Manage Accomodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/EmployeeAccomadationInfo.aspx">Assign Accommodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Accommodation Assignment Form</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Accommodation Assignment Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<radG:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="all"
                                Skin="Outlook"></radG:RadFormDecorator>--%>



                            <div class="search-box padding-tb-10 clearfix margin-bottom-0">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <asp:DropDownList ID="drpname" CssClass="trstandtop form-control input-sm" AutoPostBack="true" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Select Accommodation</label>
                                        <asp:DropDownList ID="drpDormetry" CssClass="trstandtop form-control input-sm input-medium" OnDataBound="drpDormetry_DataBound"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Check In / Check Out</label>
                                        <asp:DropDownList ID="drpCheckIN" CssClass="trstandtop form-control input-sm input-small" runat="server">
                                            <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Check In" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Check Out" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Accommodation Date</label>
                                        <radCln:RadDatePicker ID="RadDatePicker2" runat="server" CssClass="trstandtop" Calendar-ShowRowHeaders="false"
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
                                </div>
                            </div>

                            <div class="text-center padding-tb-10">
                                <asp:Button ID="imgbtnsave" runat="server" Text="Assign Accommodation" OnClick="imgbtnsave_Click"
                                    CssClass="textfields  btn red" OnClientClick="return ValidateForm();" />
                            </div>

                            <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnPreRender="RadGrid1_PreRender" OnItemCreated="RadGrid1_ItemCreated" GridLines="None" Skin="Outlook" Width="93%">

                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource2">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="EmpCode" DataType="System.String" HeaderText="EmpCode"
                                            SortExpression="EmpCode" Display="false" UniqueName="EmpCode">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AccomadationCode" Display="false" Visible="false"
                                            DataType="System.String" HeaderText="AccomadationCode" SortExpression="AccomadationCode"
                                            UniqueName="AccomadationCode">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AccomadationName" DataType="System.String" HeaderText="Accommodation Name"
                                            SortExpression="AccomadationName" UniqueName="AccomadationName">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="EffectiveCheckInDate" DataType="System.String" HeaderText="Effective Check In Date"
                                            SortExpression="EffectiveCheckInDate" UniqueName="EffectiveCheckInDate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="EffectiveCheckOutDate" DataType="System.String"
                                            HeaderText="Effective Check Out Date" SortExpression="EffectiveCheckOutDate" UniqueName="EffectiveCheckOutDate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CheckInStatus" DataType="System.String" HeaderText="Check In Status"
                                            SortExpression="CheckInStatus" UniqueName="CheckInStatus">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="AssignedBy" DataType="System.String" HeaderText="Assigned By"
                                            SortExpression="AssignedBy" UniqueName="AssignedBy">
                                            <ItemStyle Font-Bold="true" />
                                            <ItemStyle ForeColor="BurlyWood" />
                                            <HeaderStyle Font-Bold="true" />
                                        </radG:GridBoundColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_GetEmpCheckInCheckOutDetails"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpname" Name="empCode" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:Parameter DefaultValue="-1" Name="NoRecords" Type="int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                <AjaxSettings>
                                    <radA:AjaxSetting AjaxControlID="drpname">
                                        <UpdatedControls>
                                            <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                                        </UpdatedControls>
                                    </radA:AjaxSetting>
                                </AjaxSettings>
                            </radA:RadAjaxManager>
                            <center>
                                <asp:Label ID="lblmsg" CssClass="bodytxt" ForeColor="red" runat="server"></asp:Label>
                            </center>
                            <!-- Gap to fill the bottom -->
                            <!-- footer -->
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
            GetConfirmation("Are you sure you want to delete this Accomodation Assignment?", _id, "Confirm Delete", "Delete");
        });
    </script>

</body>
</html>
