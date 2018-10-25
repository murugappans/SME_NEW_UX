<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RosterSettings.aspx.cs"
    Inherits="SMEPayroll.Management.RosterSettings" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

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
                        <li>Roster Settings</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Timesheet/Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Roster Settings</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Roster Settings</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12 roster-setting">

                        <form id="form1" runat="server">
                            <radG:RadCodeBlock>
                                <script type="text/javascript" language="javascript">
                                    function chktrig(e) {
                                    }
                                    function ValueChanged(sender, args) {
                                        //var panelBar = document.getElementById("RadPanelBar1").FindItemByValue("ctrlPanel");                        
                                        //var rtpEarlyInTime1 = document.getElementById("<%= RadPanelBar1.FindItemByValue("ctrlPanel").FindControl("rtpEarlyInTime").ClientID %>"); 
                                        //rtpEarlyInTime1.value  = args.get_newValue();
                                        //alert(args.get_newValue());
                                        //alert(rtpEarlyInTime1.value);
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>



                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Roster Name</label>
                                        <asp:DropDownList ID="ddlRoster" runat="server" CssClass="form-control input-sm input-large" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlRoster_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>




                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">

                                    function RowDblClick(sender, eventArgs) {

                                        //alert(document.getElementById("RadPanelBar1_i0_i0_rtpInTime_dateInput"));

                                        //                    var masterTable = sender.get_masterTableView();
                                        //                    alert(eventArgs.getDataKeyValue("InTime"));
                                        //                    
                                        //                    var date1 = new Date();
                                        //                    date1.setHours(5, 0, 0, 0);
                                        //                    timePicker.set_minDate(date1);

                                        //sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }





                                </script>

                            </radG:RadCodeBlock>


                            <div>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" AnimationDuration="1500" runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
                                </telerik:RadAjaxLoadingPanel>

                                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="btnCopy">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadCalendar1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>

                                        <telerik:AjaxSetting AjaxControlID="RadDatePicker1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadCalendar1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                                <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>



                                    </AjaxSettings>


                                </telerik:RadAjaxManager>

                                <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                    border="0">
                                    <tr>
                                        <td colspan="2">

                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" class="bodytxt">--%>


                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select Emp_Code,(Emp_Name + ' ' + Emp_Lname) Emp_Name From Employee Where Emp_Name is not null And StatusID=1 And Company_ID=@company_id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                <radG:RadPanelBar runat="server" ID="RadPanelBar1" Width="100%">
                                    <Items>
                                        <radG:RadPanelItem Expanded="False" Text="Add Entry In Rosters" Width="100%">
                                            <Items>
                                                <radG:RadPanelItem Value="ctrlPanel">
                                                    <ItemTemplate>

                                                        <div class="clearfix padding-tb-20">
                                                            <div class="col-md-4">
                                                                <div class="table-scrollable">
                                                                    <radG:RadCalendar ID="RadCalendar1" TabIndex="10000" runat="server" Skin="Outlook"
                                                                        EnableMultiSelect="true" ShowOtherMonthsDays="false" FirstDayOfWeek="Monday"
                                                                        ShowRowHeaders="false">
                                                                    </radG:RadCalendar>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4 text-center">
                                                                <asp:Button ID="btnCopy" runat="server" CssClass="btn btn-sm default" Text="CopyRoster" Enabled="false" OnClick="btnCopy_Click" />
                                                                <radCln:RadDatePicker CssClass="input-inline width-80-pcent" AutoPostBack="true" Calendar-ShowRowHeaders="false"
                                                                    ID="RadDatePicker1" runat="server" DateInput-Enabled="true">
                                                                    <Calendar runat="server">
                                                                        <SpecialDays>
                                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                            </telerik:RadCalendarDay>
                                                                        </SpecialDays>
                                                                    </Calendar>
                                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                </radCln:RadDatePicker>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="portlet box red">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">Roster Control</div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-horizontal" role="form">
                                                                            <div class="form-body">

                                                                                <div class="form-group">
                                                                                    <div class="col-md-8">
                                                                                        <label>Flexible</label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <asp:DropDownList ID="drpFlex" runat="server" AutoPostBack="true" class="textfields form-control input-sm input-xsmall" OnSelectedIndexChanged="drpFlex_SelectedIndexChanged">
                                                                                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                                            <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-8">
                                                                                        <label>In Minutes</label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <radG:RadNumericTextBox CssClass="textfields form-control input-sm input-xsmall" NumberFormat-GroupSeparator="" ID="rdFlexHours"
                                                                                            runat="server" MinValue="0" MaxValue="1300" NumberFormat-AllowRounding="true"
                                                                                            NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                        </radG:RadNumericTextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-8">
                                                                                        <label>BreakTime After(Min)(NH)(Flexi)</label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <radG:RadNumericTextBox CssClass="textfields form-control input-sm input-xsmall" Value="240" NumberFormat-GroupSeparator="" ID="radBreakTimeAfterFlexi"
                                                                                            runat="server" MinValue="1" MaxValue="1440" NumberFormat-AllowRounding="true"
                                                                                            NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                        </radG:RadNumericTextBox>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-8">
                                                                                        <label>BreakTime After(Min)(OT)(Flexi)</label>
                                                                                    </div>
                                                                                    <div class="col-md-4">
                                                                                        <radG:RadNumericTextBox CssClass="textfields form-control input-sm input-xsmall" Value="240" NumberFormat-GroupSeparator="" ID="radBreakTimeAfterFlexiOT"
                                                                                            runat="server" MinValue="1" MaxValue="1440" NumberFormat-AllowRounding="true"
                                                                                            NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                        </radG:RadNumericTextBox>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="clearfix padding-top-20">

                                                            <div class="col-md-4">
                                                                <div class="portlet box red">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">Other Settings</div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-horizontal" role="form">
                                                                            <div class="form-body">

                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>Transfer Hrs</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:DropDownList ID="drpTransfer" runat="server" DataSourceID="SqlDataSource3" DataTextField="TranferName"
                                                                                            DataValueField="ID" class="textfields form-control input-sm input-small">
                                                                                        </asp:DropDownList>
                                                                                        <div style="display: none">
                                                                                            <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBoxClockInBef"
                                                                                                runat="server" MinValue="1" MaxValue="1439" NumberFormat-AllowRounding="true"
                                                                                                NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                            </radG:RadNumericTextBox>
                                                                                        </div>
                                                                                        <div style="display: none">
                                                                                            <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBoxClockOutBef"
                                                                                                runat="server" MinValue="1" MaxValue="1439" NumberFormat-AllowRounding="true"
                                                                                                NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                            </radG:RadNumericTextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>Public Holiday</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:DropDownList ID="drpPH" runat="server" DataSourceID="SqlDataSource3" DataTextField="TranferName"
                                                                                            DataValueField="ID" class="textfields form-control input-sm input-small">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>Sunday</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:DropDownList ID="drpSunday" runat="server" DataSourceID="SqlDataSource3" DataTextField="TranferName"
                                                                                            DataValueField="ID" class="textfields form-control input-sm input-small">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>No Roster Assign</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:DropDownList ID="drpNoRoster" runat="server" Enabled="false" DataSourceID="SqlDataSource3" DataTextField="TranferName"
                                                                                            DataValueField="ID" class="textfields form-control input-sm input-small">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>Rounding</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:DropDownList ID="drpROund" runat="server" class="textfields form-control input-sm input-small">
                                                                                            <asp:ListItem Text="0" Value="0" />
                                                                                            <asp:ListItem Text="5" Value="5" />
                                                                                            <asp:ListItem Text="10" Value="10" />
                                                                                            <asp:ListItem Text="15" Value="15" />
                                                                                            <asp:ListItem Text="30" Value="30" />
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-5">
                                                                                        <label>No Roster Assign</label>
                                                                                    </div>
                                                                                    <div class="col-md-7">
                                                                                        <asp:CheckBoxList Visible="True" ID="chkFiFo" runat="server" CssClass="bodytxt" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                                            <asp:ListItem Value="FILO" Selected="False"></asp:ListItem>
                                                                                        </asp:CheckBoxList>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="portlet box red">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">Time Settings</div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-horizontal" role="form">
                                                                            <div class="form-body">

                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>In Time</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">
                                                                                            <script type="text/javascript">
                                                                                                Telerik.Web.UI.RadTimeView.prototype.initialize = function () {
                                                                                                    Telerik.Web.UI.RadTimeView.callBaseMethod(this, "initialize");
                                                                                                    this.DivElement = $get(this.get_id());
                                                                                                    var searchedText = '"rcFooter"';
                                                                                                    if ($telerik.isIE8 || $telerik.isIE7) {
                                                                                                        searchedText = searchedText.replace(/"/gi, "");
                                                                                                    }
                                                                                                    if (this.get_showFooter() && this.DivElement.innerHTML.indexOf(searchedText) == -1) {
                                                                                                        this.set_showFooter(false);
                                                                                                    } this._timeMatrix = this._setTimeMatrix();
                                                                                                    this._tempStyle = null;
                                                                                                    this._attachEventHandlers();
                                                                                                }
                                                                                            </script>
                                                                                        </telerik:RadCodeBlock>
                                                                                        <radG:RadTimePicker ID="rtpInTime" runat="server" Skin="Outlook" TabIndex="0" CssClass="input-small">
                                                                                            <DateInput runat="server">
                                                                                                <ClientEvents OnValueChanged="ValueChanged" />
                                                                                            </DateInput>
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>Out Time</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpOutTime" runat="server" Skin="Outlook" TabIndex="0" CssClass="input-small">
                                                                                            <%--<TimeView ID="TimeView1" Skin="Default"
                                                                                                                    ShowHeader="true"
                                                                                                                    HeaderText="Out Time Picker"
                                                                                                                    StartTime="00:00:00"
                                                                                                                    Interval="00:30:00"
                                                                                                                    EndTime="1.00:00:00"
                                                                                                                    Columns="4"
                                                                                                                    RenderDirection="vertical"
                                                                                                                    runat="server">
                                                                                                                </TimeView>--%>
                                                                                        </radG:RadTimePicker>
                                                                                        <%--<telerik:RadTimePicker ID="RadTimePicker1" runat="server" Skin="Outlook">
                                                                                                                <TimeView Skin="Default"
                                                                                                                    ShowHeader="true"
                                                                                                                    HeaderText="Out Time Picker"
                                                                                                                    StartTime="00:00:00"
                                                                                                                    Interval="00:30:00"
                                                                                                                    EndTime="1.00:00:00"
                                                                                                                    Columns="4"
                                                                                                                     RenderDirection="vertical"
                                                                                                                    runat="server">
                                                                                                                </TimeView>
                                                                                                            </telerik:RadTimePicker>--%>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>
                                                                                            <asp:Label ID="lblEarlyin" Text="Early In By" runat="server"></asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpEarlyInTime" runat="server" Skin="Outlook"
                                                                                            TabIndex="0" CssClass="input-small">
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>
                                                                                            <asp:Label ID="lblEarlyOut" Text="Early Out By" runat="server"></asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpEarlyOutTime" runat="server" Skin="Outlook"
                                                                                            TabIndex="0" CssClass="input-small">
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>
                                                                                            <asp:Label ID="lblLateIn" Text="Late In By" runat="server"></asp:Label></label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpLateInTime" runat="server" Skin="Outlook" CssClass="input-small">
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="portlet box red">
                                                                    <div class="portlet-title">
                                                                        <div class="caption">Break Time Settings</div>
                                                                    </div>
                                                                    <div class="portlet-body form">
                                                                        <div class="form-horizontal" role="form">
                                                                            <div class="form-body">

                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>NH After</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpBreakTimeNH" runat="server" Skin="Outlook"
                                                                                            TabIndex="0" CssClass="input-small">
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>OT After</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <radG:RadTimePicker ID="rtpBreakTimeOT" runat="server" Skin="Outlook"
                                                                                            TabIndex="0" CssClass="input-small">
                                                                                        </radG:RadTimePicker>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>Break Time NH</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <table class="rcTable" cellpadding="0">
                                                                                            <tr>
                                                                                                <td class="rcInputCell width-100px">
                                                                                                    <radG:RadNumericTextBox NumberFormat-GroupSeparator="" ID="txtBreakTime" CssClass="form-control input-sm input-xsmall"
                                                                                                        runat="server" MinValue="0" MaxValue="600" NumberFormat-AllowRounding="true"
                                                                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                                    </radG:RadNumericTextBox>
                                                                                                </td>
                                                                                                <td>Min
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>Break Time OT</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <table class="rcTable" cellpadding="0">
                                                                                            <tr>
                                                                                                <td class="rcInputCell width-100px">
                                                                                                    <radG:RadNumericTextBox NumberFormat-GroupSeparator="" ID="txtBreakTimeOT" CssClass="form-control input-sm input-xsmall"
                                                                                                        runat="server" MinValue="0" MaxValue="600" NumberFormat-AllowRounding="true"
                                                                                                        NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                                    </radG:RadNumericTextBox>
                                                                                                </td>
                                                                                                <td>Min
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="form-group">
                                                                                    <div class="col-md-4">
                                                                                        <label>Break Time Fall</label>
                                                                                    </div>
                                                                                    <div class="col-md-8">
                                                                                        <asp:CheckBoxList Visible="True" ID="brktimefallnextday" runat="server" CssClass="bodytxt" RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                                            <asp:ListItem Value="&nbsp;NEXTDAY" Selected="False"></asp:ListItem>
                                                                                        </asp:CheckBoxList>
                                                                                    </div>
                                                                                </div>


                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="clearfix">
                                                            <div class="col-md-12">
                                                                <table cellpadding="3" cellspacing="0" border="1" width="90%" style="height: 50%; display: none">
                                                                    <tr>
                                                                        <td>
                                                                            <div style="display: none">
                                                                                <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBoxClockInAft"
                                                                                    runat="server" MinValue="1" MaxValue="1439" NumberFormat-AllowRounding="true"
                                                                                    NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                </radG:RadNumericTextBox>
                                                                            </div>
                                                                            <div style="display: none">
                                                                                <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBoxClockOutAft"
                                                                                    runat="server" MinValue="1" MaxValue="1439" NumberFormat-AllowRounding="true"
                                                                                    NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                                </radG:RadNumericTextBox>
                                                                            </div>
                                                                            <div style="display: none">
                                                                                <radG:RadTimePicker Width="80px" ID="rtpLateOutTime" runat="server" Skin="Outlook">
                                                                                </radG:RadTimePicker>
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>

                                                        <div class="clearfix padding-bottom-20">
                                                            <div class="col-md-12 text-center">
                                                                <asp:Button ID="btnInsert" CssClass="btn red" runat="server" OnClick="btnInsert_Click" Text="Add" />
                                                                <asp:Button ID="btnCancel" CssClass="btn default" runat="server" OnClick="btnClear_Click" Text="Clear" /><br />
                                                                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label></td>
                                                            </div>
                                                        </div>



                                                    </ItemTemplate>
                                                </radG:RadPanelItem>
                                            </Items>
                                        </radG:RadPanelItem>
                                        <radG:RadPanelItem Expanded="False" Text="Roster Information" Width="100%">
                                            <Items>
                                                <radG:RadPanelItem Value="ctrlPanel2">
                                                    <ItemTemplate>
                                                        <div class="search-box padding-tb-10 clearfix">
                                                            <div class="form-inline col-sm-10">
                                                                <div class="form-group">
                                                                    <label>Start Date</label>
                                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdStartDate" runat="server">
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
                                                                    <label>End Date</label>
                                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEndDate" runat="server">
                                                                        <Calendar runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                                        <%--  <ClientEvents OnDateSelected="AddDateSelected" /> --%>
                                                                    </radG:RadDatePicker>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>&nbsp;</label>
                                                                    <asp:LinkButton ID="ImageButton1" CssClass="btn red btn-circle btn-sm" OnClick="bindgridDelete" runat="server">GO</asp:LinkButton>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-2 text-right">
                                                                <asp:Button ID="btnDelete" CssClass="btn btn-sm red" OnClientClick="return confirm('Are you sure you want to delete Selected Records.?');"
                                                                    runat="server" OnClick="btnDelete_Click" Text="Delete" />
                                                            </div>
                                                        </div>

                                                        <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" Width="98%" AllowFilteringByColumn="false"
                                                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                                            ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true" OnGroupsChanging="RadGrid1_GroupsChanging"
                                                            OnSortCommand="RadGrid1_SortCommand1" AllowMultiRowSelection="true" PageSize="50"
                                                            AllowPaging="true" OnPageIndexChanged="RadGrid1_PageIndexChanged" OnPageSizeChanged="RadGrid1_PageSizeChanged">
                                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                                            <SelectedItemStyle CssClass="SelectedRow" />
                                                            <MasterTableView CommandItemDisplay="none" DataKeyNames="ID,InTime" ClientDataKeyNames="ID,InTime,OutTime,EarlyInBy,LateInBy,EarlyOutBy,LateOutBy,ClockInBefore,ClockInAfter,ClockOutBefore,ClockOutAfter,BreakTimeNH,BreakTimeOT, BreakTimeNHhr, BreakTimeOThr, FlexibleWorkinghr,BreakTimeAftOtFlx">
                                                                <FilterItemStyle HorizontalAlign="left" />
                                                                <HeaderStyle ForeColor="Navy" />
                                                                <ItemStyle BackColor="White" Height="20px" />
                                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                                <Columns>
                                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                    </radG:GridClientSelectColumn>
                                                                    <radG:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="black" HeaderText="ID"
                                                                        UniqueName="ID" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Roster_ID" HeaderStyle-ForeColor="black" HeaderText="Roster_ID"
                                                                        UniqueName="Roster_ID" Display="false">
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Roster_Day" HeaderStyle-ForeColor="black" HeaderText="Day"
                                                                        UniqueName="Roster_Day">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="Roster_Date" HeaderStyle-ForeColor="black" HeaderText="Date"
                                                                        UniqueName="Roster_Date">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="InTime" HeaderStyle-ForeColor="black" HeaderText="In Time"
                                                                        UniqueName="InTime">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="OutTime" HeaderStyle-ForeColor="black" HeaderText="Out Time"
                                                                        UniqueName="OutTime">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="EarlyInBy" HeaderStyle-ForeColor="black" HeaderText="Early In Time"
                                                                        UniqueName="EarlyInBy">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="EarlyOutBy" HeaderStyle-ForeColor="black" HeaderText="Early Out Time"
                                                                        UniqueName="EarlyOutBy">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="LateInBy" HeaderStyle-ForeColor="black" HeaderText="Late In Time"
                                                                        UniqueName="LateInBy">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="LateOutBy" HeaderStyle-ForeColor="black" HeaderText="Late Out Time"
                                                                        Display="false" UniqueName="LateOutBy">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="BreakTimeNHHr" HeaderStyle-ForeColor="black" HeaderText="Brk Time NH Hr"
                                                                        UniqueName="BreakTimeNHHr">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="BreakTimeOTHr" HeaderStyle-ForeColor="black" HeaderText="Brk Time OT Hr"
                                                                        UniqueName="BreakTimeOTHr">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn Display="false" DataField="ClockInBefore" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Clock In Before" UniqueName="ClockInBefore">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn Display="false" DataField="ClockInAfter" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Clock In After" UniqueName="ClockInAfter">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn Display="false" DataField="ClockOutBefore" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Clock Out Before" UniqueName="ClockOutBefore">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn Display="false" DataField="ClockOutAfter" HeaderStyle-ForeColor="black"
                                                                        HeaderText="Clock Out After" UniqueName="ClockOutAfter">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="BreakTimeNH" HeaderStyle-ForeColor="black" HeaderText="Break Time In NH"
                                                                        UniqueName="BreakTimeNH">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="BreakTimeOT" HeaderStyle-ForeColor="black" HeaderText="Break Time In OT"
                                                                        UniqueName="BreakTimeOT">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="FlexibleWorkinghr" HeaderStyle-ForeColor="black" HeaderText="Flex Work Hr"
                                                                        UniqueName="FlexibleWorkinghr">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>
                                                                    <radG:GridBoundColumn DataField="TransferIn" HeaderStyle-ForeColor="black" HeaderText="Transfer In"
                                                                        UniqueName="TransferIn">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>

                                                                    <radG:GridBoundColumn DataField="FIFO" HeaderStyle-ForeColor="black" HeaderText="FirstIn Last out"
                                                                        UniqueName="FIFO">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>

                                                                    <radG:GridBoundColumn DataField="Rounding" HeaderStyle-ForeColor="black" HeaderText="Rounding"
                                                                        UniqueName="Rounding">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>

                                                                    <radG:GridBoundColumn DataField="BreakTimeAfter" HeaderStyle-ForeColor="black" HeaderText="Break TimeAfter(Flexi)(NH)"
                                                                        UniqueName="BreakTimeAfter">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>

                                                                    <radG:GridBoundColumn DataField="BreakTimeAftOtFlx" HeaderStyle-ForeColor="black" HeaderText="Break TimeAfter(Flexi)(OT)"
                                                                        UniqueName="BreakTimeAftOtFlx" EmptyDataText="0">
                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </radG:GridBoundColumn>

                                                                </Columns>
                                                                <EditFormSettings ColumnNumber="2">
                                                                    <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                                        BackColor="White" Width="100%" />
                                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                                        Height="110px" BackColor="White" />
                                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="1" Wrap="False"></FormTableAlternatingItemStyle>
                                                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Project" UpdateText="Update"
                                                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                                                    </EditColumn>
                                                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                                </EditFormSettings>
                                                            </MasterTableView>
                                                            <ClientSettings>
                                                                <Selecting AllowRowSelect="true" />
                                                                <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                                    AllowColumnResize="false"></Resizing>
                                                                <ClientEvents OnRowDblClick="RowDblClick" />
                                                            </ClientSettings>
                                                        </radG:RadGrid>




                                                    </ItemTemplate>
                                                </radG:RadPanelItem>
                                            </Items>
                                        </radG:RadPanelItem>
                                    </Items>
                                </radG:RadPanelBar>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select * From HourTransfer"></asp:SqlDataSource>
                                <asp:XmlDataSource ID="xmldtTimesheet" runat="server" DataFile="~/XML/xmldata.xml"
                                    XPath="SMEPayroll/Timesheet/HourCalc"></asp:XmlDataSource>



                                <%--</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>--%>
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
        $("input[type='button']").removeAttr("style");
        //$("#RadPanelBar1_i0_i0_RadCalendar1_wrapper").attr("align", "center");
        $(".RadPicker.RadPicker_Default, .RadPicker.RadPicker_Outlook, .rcTable, .riTextBox").removeAttr("style");


    </script>

</body>
</html>
