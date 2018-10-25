<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KetForm.aspx.cs" Inherits="SMEPayroll.Employee.KetForm" %>


<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>





<%@ Import Namespace="SMEPayroll" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />

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
                        <li>Key Employment Terms</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>KET</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Key Employment Terms</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="ScriptManager" runat="server">
                            </radG:RadScriptManager>

                            

                                            <div class="search-box padding-tb-10 clearfix">
                                                <div class="col-md-12 form-inline text-right">
                                                    <div class="form-group">
                                                        <!-- controll -->
                                                        <asp:DropDownList ID="drpCompany" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"
                                                            AutoPostBack="true" runat="server" DataSourceID="SqlDataSource1" DataTextField="Company_Name"
                                                            DataValueField="Company_ID" Width="250px" CssClass="trstandtop">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="drpShowAll" AutoPostBack="true" OnSelectedIndexChanged="drpShowAll_SelectedIndexChanged"
                                                            runat="server" CssClass="trstandtop form-control input-sm">
                                                        </asp:DropDownList>
                                                        <asp:Button ID="btnshowall" Visible="false" CssClass="textfields" Text="Show All Employees"
                                                            runat="server" OnClick="btnshowall_Click" />
                                                        <asp:Button Visible="false" ID="btnallemp" CssClass="textfields"
                                                            Text="ExportAllEmpToExcel" runat="server" />
                                                        <asp:Button ID="Button4" CssClass="textfields" Visible="false" Width="150px" Text="Export to Excel"
                                                            OnClick="Button1_Click" runat="server"></asp:Button>
                                                        <asp:Button ID="Button5" CssClass="textfields" Width="150px" Text="Export to Word"
                                                            Visible="false" OnClick="Button2_Click" runat="server"></asp:Button><asp:CheckBox
                                                                ID="CheckBox1" Visible="false" CssClass="bodytxt" Text="Exports All" runat="server"></asp:CheckBox>
                                                        <asp:Button ID="btnsubapprove" Visible="false" OnClick="btnsubapprove_click" runat="server"
                                                            Text="Approve Employee" class="textfields" Style="width: 180px; height: 22px" />
                                                        <!-- controls end -->
                                                    </div>
                                                </div>
                                            </div>

                                            <!-- content start -->
                                            <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                                <AjaxSettings>
                                                    <radG:AjaxSetting AjaxControlID="RadGrid2">
                                                        <UpdatedControls>
                                                            <radG:AjaxUpdatedControl ControlID="RadGrid2" />
                                                        </UpdatedControls>
                                                    </radG:AjaxSetting>
                                                </AjaxSettings>
                                            </radG:RadAjaxManager>
                                            <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

                                                <script language="javascript" type="text/javascript">

                                                    function ShowMsg() {
                                                        var sMsg = '<%=sMsg%>';
                                                        if (sMsg != '')
                                                            alert(sMsg);
                                                    }
                                                </script>

                                            </radG:RadCodeBlock>
<label id="lblMessage" style="color: Red" class="bodytxt" runat="server" visible="true">
                                                            </label>
                                            
                                                            <!-- ToolBar -->
                                                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                                                <script type="text/javascript">
                                                                    function openNewWin(url) {
                                                                        var width = screen.availWidth - 100;
                                                                        var height = screen.availHeight - 100;
                                                                        var left = parseInt((screen.availWidth / 2) - (width / 2));
                                                                        var top = parseInt((screen.availHeight / 2) - (height / 2));
                                                                        var windowFeatures = "width=" + width + ",height=" + height + ",status,resizable,left=" + left + ",top=" + top + "screenX=" + left + ",screenY=" + top;
                                                                        var myWindow = window.open(url, "mynewwin", windowFeatures);

                                                                        // var x = window.open(url, 'mynewwin', 'width=800,height=600,toolbar=0');
                                                                        myWindow.focus();
                                                                    }


                                                                    function getOuterHTML(obj) {
                                                                        if (typeof (obj.outerHTML) == "undefined") {
                                                                            var divWrapper = document.createElement("div");
                                                                            var copyOb = obj.cloneNode(true);
                                                                            divWrapper.appendChild(copyOb);
                                                                            return divWrapper.innerHTML
                                                                        }
                                                                        else
                                                                            return obj.outerHTML;
                                                                    }

                                                                    function PrintRadGrid(sender, args) {
                                                                        if (args.get_item().get_text() == 'Print') {

                                                                            var previewWnd = window.open('about:blank', '', '', false);
                                                                            var str=".RadGrid .rgMasterTable,.RadGrid .rgDetailTable{ border-collapse:separate;}.RadGrid .rgRow,.RadGrid .rgAltRow,.RadGrid .rgHeader,.RadGrid .rgResizeCol,.RadGrid .rgPager,.RadGrid .rgGroupPanel,.RadGrid .rgGroupHeader{	cursor:default;}";
                                                                            str=str+".RadGrid input[type='image']{	cursor:pointer;}.RadGrid .rgRow td,.RadGrid .rgAltRow td,.RadGrid .rgEditRow td,.RadGrid .rgFooter td,.RadGrid .rgFilterRow td,.RadGrid .rgHeader,.RadGrid .rgResizeCol,.RadGrid .rgGroupHeader td{	padding-left:7px;	padding-right:7px;}";
                                                                            str=str+".RadGrid .rgClipCells .rgHeader,.RadGrid .rgClipCells .rgFilterRow>td,.RadGrid .rgClipCells .rgRow>td,.RadGrid .rgClipCells .rgAltRow>td,.RadGrid .rgClipCells .rgEditRow>td,.RadGrid .rgClipCells .rgFooter>td{	overflow:hidden;}";
                                                                            str = str + ".RadGrid .rgAdd,.RadGrid .rgRefresh,.RadGrid .rgEdit,.RadGrid .rgDel,.RadGrid .rgDrag,.RadGrid .rgFilter,.RadGrid .rgPagePrev,.RadGrid .rgPageNext,.RadGrid .rgPageFirst,.RadGrid .rgPageLast,.RadGrid .rgExpand,.RadGrid .rgCollapse,.RadGrid .rgSortAsc,.RadGrid .rgSortDesc,.RadGrid .rgUpdate,.RadGrid .rgCancel,.RadGrid .rgUngroup,.RadGrid .rgExpXLS,.RadGrid .rgExpDOC,.RadGrid .rgExpPDF,.RadGrid .rgExpCSV{	width:16px;	height:16px;	border:0;	margin:0;	padding:0;	background-color:transparent;	background-repeat:no-repeat;	vertical-align:middle;	font-size:1px;	cursor:pointer;}";
                                                                            str=str+".RadGrid .rgGroupItem input,.RadGrid .rgCommandRow img,.RadGrid .rgHeader input,.RadGrid .rgFilterRow img,.RadGrid .rgFilterRow input,.RadGrid .rgPager img{	vertical-align:middle;}";
                                                                            str=str+".rgNoScrollImage div.rgHeaderDiv{	background-image:none;}";
                                                                            str=str+".RadGrid .rgHeader,.RadGrid th.rgResizeCol{	padding-top:5px;	padding-bottom:4px;	text-align:left;	font-weight:normal;}";
                                                                            str=str+".RadGrid .rgHeader a{    text-decoration:none;}.RadGrid .rgCheck input{	height:15px;	margin-top:0;	margin-bottom:0;	padding-top:0;	padding-bottom:0;	cursor:default;}";
                                                                            str=str+".rfdCheckbox .RadGrid .rgCheck input /*Safari,Chrome fix*/{	height:20px;}/*rows*/";
                                                                            str=str+".RadGrid .rgRow td,.RadGrid .rgAltRow td,.RadGrid .rgEditRow td,.RadGrid .rgFooter td{	padding-top:4px;	padding-bottom:3px;}";
                                                                            str=str+".RadGrid table.rgMasterTable tr .rgDragCol{	padding-left:0;	padding-right:0;	text-align:center;}";
                                                                            str=str+".RadGrid .rgDrag{	width:15px;	height:15px;	cursor:url('WebResource.axd?d=zygyn5bPuSx4K1F5VvdlQIIhR_g7DeTW4tm5LTl9ZmWnaHZtSqk-WIiHC3KrlkjAvr11iumg0ZzEDPoGAdYLHyvYsOAcDIDvhIw9c70pkzhM6ctP6cZFiSGyRLQ4gfetmKIX14By0UeH3aqg5_GCqw2&t=635737863581364401'), move;}";
                                                                            str=str+".RadGrid .rgPager .rgStatus{	width:35px;	padding:3px 0 2px;}";
                                                                            str=str+".RadGrid .rgStatus div{	width:24px;	height:24px;	overflow:hidden;	border:0;	margin:0 auto;	padding:0;	background-color:transparent;	background-position:center center;	background-repeat:no-repeat;	text-indent:-2222px;}";
                                                                            str=str+".RadGrid .rgPager td{	padding:0;}";
                                                                            str=str+".RadGrid td.rgPagerCell{	border:0;	padding:5px 0 4px;}";
                                                                            str=str+".RadGrid .rgWrap{	float:left;	padding:0 10px;	line-height:22px;	white-space:nowrap;}";
                                                                            str=str+".RadGrid .rgArrPart1{	padding-right:0;}";
                                                                            str=str+".RadGrid .rgArrPart2{	padding-left:0;}";
                                                                            str=str+".RadGrid .rgInfoPart{	float:right;}.RadGrid .rgInfoPart strong{	font-weight:normal;}";
                                                                            str=str+".RadGrid .rgArrPart1 img,.RadGrid .rgArrPart2 img{	border:0;	margin:-3px 8px 0;}";
                                                                            str=str+".RadGrid .rgPageFirst,.RadGrid .rgPagePrev,.RadGrid .rgPageNext,.RadGrid .rgPageLast{	width:22px;	height:22px;	vertical-align:top;}";
                                                                            str=str+".RadGrid .NextPrev .rgPageFirst,.RadGrid .NextPrev .rgPagePrev,.RadGrid .NextPrev .rgPageNext,.RadGrid .NextPrev .rgPageLast{	vertical-align:middle;}";
                                                                            str=str+".RadGrid .rgPageFirst,.RadGrid .rgPagePrev{	margin-right:1px;}";
                                                                            str=str+".RadGrid .rgPageNext,.RadGrid .rgPageLast{	margin-left:1px;}";
                                                                            str=str+".RadGrid .rgPager .rgPagerButton{	height:22px;	border-style:solid;	border-width:1px;	margin:0 14px 0 0;	padding:0 4px 2px;	font-size:12px;	line-height:12px;	vertical-align:middle;	cursor:pointer;}";
                                                                            str = str + ".RadGrid .rgNumPart{	padding:0;}  ";
                                                                            str = str + ".RadGrid .NumericPages .rgNumPart{	padding:0 10px;}.RadGrid .rgNumPart a{	float:left;	line-height:22px;	margin:0;	padding:0 5px 0 0;	text-decoration:none;}";
                                                                            str = str + ".RadGrid .rgPager .RadSlider{	float:left;	margin:0 10px 0 0;}.RadGrid .rgPagerLabel,.RadGrid .rgPager .RadComboBox,.RadGrid .rgPager .RadInput{	margin:0 4px 0 0;	vertical-align:middle;}";
                                                                            str = str + "*+html .RadGrid .rgPager .RadComboBox{margin-top:-1px;}* html .RadGrid .rgPager .RadComboBox{margin-top:-1px;padding:1px 0;}.RadGrid .rgPagerTextBox{	text-align:center;}";
                                                                            str = str + ".GridReorderTop,.GridReorderBottom{	width:9px;	height:9px;	margin:0 0 0 -5px;	padding:0;}";
                                                                            str = str + ".RadGrid .rgFilterRow td{    padding-top:4px;    padding-bottom:7px;}.RadGrid .rgFilter{	width:22px;	height:22px;	margin:0 0 0 2px;}.RadGrid .rgFilterBox{	border-width:1px;	border-style:solid;	margin:0;	padding:2px 1px 3px;	font-size:12px;	vertical-align:middle;}";
                                                                            str = str + ".RadGrid .rgFilterRow .RadRating{	display:inline-block;	vertical-align:middle;}*+html .RadGrid .rgFilterRow .RadRating{display:inline;zoom:1;}* html .RadGrid .rgFilterRow .RadRating{display:inline;zoom:1;}";
                                                                            str = str + ".RadMenu .rmGroup .rgHCMItem .rmText{	width:161px;	padding:6px 5px 5px 30px;}.rgHCMItem .rgHCMClear,.rgHCMItem .rgHCMShow,.rgHCMItem .rgHCMAnd,.rgHCMItem .rgHCMFilter{	display:block;}";
                                                                            str = str + ".rgHCMItem .rgHCMShow,.rgHCMItem .rgHCMAnd{	padding-top:5px;	line-height:12px;}";
                                                                            str = str + ".rgHCMItem .rgHCMClear,.rgHCMItem .rgHCMShow,.rgHCMItem .RadComboBox,.rgHCMItem .rgHCMAnd{	margin:0 0 5px;}";
                                                                            str = str + ".rgHCMItem .rgHCMAnd{	margin-top:5px;}.rgHCMItem .rgHCMFilter{	margin-top:11px;}";
                                                                            str = str + ".rgHCMItem .rgHCMClear,.rgHCMItem .rgHCMFilter{	width:160px;	border-style:solid;	border-width:1px;	-moz-border-radius:3px;	-webkit-border-radius:3px;	border-radius:3px;	padding:1px 0;	font-size:12px;	cursor:pointer;}";
                                                                            str = str + ".RadGrid .rgGroupPanel{	height:24px;}.RadGrid .rgGroupItem{	padding:0 2px 1px 3px;	line-height:20px;	font-weight:normal;	vertical-align:middle;}";
                                                                            str = str + ".RadGrid .rgGroupHeader td{	padding-top:0;	padding-bottom:0;}.RadGrid .rgGroupHeader td p{    display:inline;    margin:0;    padding:0 10px;}";
                                                                            str = str + ".RadGrid .rgGroupHeader td div div{	top:-0.8em;	padding:0 10px;}/*IE quirks mode*/* html .RadGrid .rgGroupHeader td div div{	top /**/:0;}.RadGrid .rgGroupHeader td div div div{	top:0;	padding:0;	border:0;}";
                                                                            str = str + " .RadGrid .rgUpdate,.RadGrid .rgCancel{	width:18px;	height:18px;}";
                                                                            str = str + ".RadGrid .rgDetailTable{	border-style:solid;	border-width:1px;	border-right-width:0;}";
                                                                            str = str + ".RadGrid .rgAdd,.RadGrid .rgRefresh{	width:18px;	height:18px;	vertical-align:bottom;}";
                                                                            str = str + ".RadGrid .rgEdit,.RadGrid .rgDel{	width:15px;	height:15px;}";
                                                                            str = str + ".RadGridRTL .rgHeader,.RadGridRTL .rgResizeCol{	text-align:right;}.RadGridRTL .rgPager .rgStatus{	border-right:0;	border-left-width:1px;}.RadGridRTL .rgWrap{	float:right;}.RadGridRTL .rgInfoPart{	float:left;}";
                                                                            str = str + ".RadGridRTL .rgNumPart{	width:220px;}.RadGridRTL .rgNumPart a{	float:right;}.RadGridRTL .rgDetailTable{	border-right-width:1px;	border-left-width:0;}";
                                                                            str = str + ".RadGrid_Outlook{    border:1px solid #002d96;    background:#fff;    color:#000;}";
                                                                            str = str + ".RadGrid_Outlook,.RadGrid_Outlook .rgMasterTable,.RadGrid_Outlook .rgDetailTable,.RadGrid_Outlook .rgGroupPanel table,.RadGrid_Outlook .rgCommandRow table,.RadGrid_Outlook .rgEditForm table,.RadGrid_Outlook .rgPager table,.GridToolTip_Outlook{    font:12px/16px 'segoe ui',arial,sans-serif;}";
                                                                            str = str + ".RadGrid_Outlook .rgHeader:first-child,.RadGrid_Outlook th.rgResizeCol:first-child,.RadGrid_Outlook .rgFilterRow>td:first-child,.RadGrid_Outlook .rgRow>td:first-child,.RadGrid_Outlook .rgAltRow>td:first-child{	border-left:0;	padding-left:8px;}";
                                                                            str = str + ".RadGrid_Outlook .rgAdd,.RadGrid_Outlook .rgRefresh,.RadGrid_Outlook .rgEdit,.RadGrid_Outlook .rgDel,.RadGrid_Outlook .rgFilter,.RadGrid_Outlook .rgPagePrev,.RadGrid_Outlook .rgPageNext,.RadGrid_Outlook .rgPageFirst,.RadGrid_Outlook .rgPageLast,.RadGrid_Outlook .rgExpand,.RadGrid_Outlook .rgCollapse,.RadGrid_Outlook .rgSortAsc,.RadGrid_Outlook .rgSortDesc,.RadGrid_Outlook .rgUpdate,";
                                                                            str = str + ".RadGrid_Outlook .rgCancel,.RadGrid_Outlook .rgUngroup,.RadGrid_Outlook .rgExpXLS,.RadGrid_Outlook .rgExpDOC,.RadGrid_Outlook .rgExpPDF,.RadGrid_Outlook .rgExpCSV{background-image:url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgHeaderDiv{background:#d6e5f3 0 -7750px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".rgTwoLines .rgHeaderDiv{	background-position:0 -7250px;}.RadGrid_Outlook .rgHeader,.RadGrid_Outlook th.rgResizeCol{	border:0;	border-left:1px solid #89aee5;	border-bottom:1px solid #5d8cc9;background:0 -2300px repeat-x #7da5e0 url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook th.rgSorted{	background-color:#ef9718;	background-position:0 -2600px;}.RadGrid_Outlook .rgHeader,.RadGrid_Outlook .rgHeader a{    color:#000;}";
                                                                            str = str + ".RadGrid_Outlook .rgRow td,.RadGrid_Outlook .rgAltRow td,.RadGrid_Outlook .rgEditRow td,.RadGrid_Outlook .rgFooter td{	border-style:solid;	border-width:0 0 1px 1px;}";
                                                                            str = str + ".RadGrid_Outlook .rgRow td,.RadGrid_Outlook .rgAltRow td{	border-color:#d0d7e5;}.RadGrid_Outlook .rgRow .rgSorted,.RadGrid_Outlook .rgAltRow .rgSorted{	background-color:#f2f2f2;}";
                                                                            str = str + ".RadGrid_Outlook .rgRow a,.RadGrid_Outlook .rgAltRow a,.RadGrid_Outlook .rgEditRow a,.RadGrid_Outlook .rgFooter a,.RadGrid_Outlook .rgEditForm a{	color:#000;}";
                                                                            str = str + ".RadGrid_Outlook .rgSelectedRow{background:#97b8e8 0 -3900px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + "*+html .RadGrid_Outlook .rgSelectedRow .rgSorted{background-color:#97b8e8}* html .RadGrid_Outlook .rgSelectedRow .rgSorted{background-color:#97b8e8}.RadGrid_Outlook .rgActiveRow,.RadGrid_Outlook .rgHoveredRow{ background:#ffd192 0 -2900px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgEditRow{	background:#c8dbf6;}*+html .RadGrid_Outlook .rgEditRow .rgSorted{background-color:#c8dbf6}* html .RadGrid_Outlook .rgEditRow .rgSorted{background-color:#c8dbf6}";
                                                                            str = str + ".RadGrid_Outlook .rgSelectedRow td{	border-bottom-color:#002d96;}.RadGrid_Outlook .rgActiveRow td,.RadGrid_Outlook .rgHoveredRow td{	border-color:#d0d7e5;}";
                                                                            str = str + ".RadGrid_Outlook .rgDrag{background-image:url('WebResource.axd?d=7LS9SBnMd9iCx3mSunjQUkhrxg7QU1n8FQVRLW0fp9y4-8idopMyIyKl2UYJECghs-wv6CKUD0pj8eHMCMiZw72PJjruV96mO10EV5rt-Qvu9nI4Wr3kvsVySAs-CtKe8R8pXllem_IQYGDpEm3q6g2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgFooterDiv,.RadGrid_Outlook .rgFooter{background:#d5e4f3 0 -6500px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgFooter td{	border-width:1px 0;	border-color:#567db0 #fff #fff;	padding-left:8px;}";
                                                                            str = str + ".RadGrid_Outlook .rgPager .rgStatus{	border:0;	border-top:1px solid;	border-right:1px solid;	border-color:#5d8cc9 #9cb9dc;}";
                                                                            str = str + ".RadGrid_Outlook .rgStatus div{ackground-image:url('WebResource.axd?d=Its3Yr6rz5_XeFRfGbQ0lDEYXgOxOScjZJRGBz2Ph0I4XQ2B5ocsE9auvtFzr1FEVyi0ed2p9tKPtMbQTcmOuuCUl2-U0EaNrPtD7HNgECTVeqkuzVRbdwA3fGaDf_eIl5j8BfUnIsc306N7vd900w-WkdrS3Gbt94lRCWpqJuU1&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgPager{	background:#d6e5f3;}.RadGrid_Outlook td.rgPagerCell{	border:0;	border-top:1px solid #5d8cc9;	border-left:1px solid #fff;}";
                                                                            str = str + ".RadGrid_Outlook .rgInfoPart{	color:#333;}.RadGrid_Outlook .rgInfoPart strong{	color:#000;}.RadGrid_Outlook .rgPageFirst{	margin:0 3px 0 0;	background-position:0 -550px;}";
                                                                            str = str + ".RadGrid_Outlook .rgHeader .rgSortAsc{	background-position:3px -146px;	height:10px;}";
                                                                            str = str + ".RadGrid_Outlook .rgHeader .rgSortDesc{	background-position:3px -96px;	height:10px;}";
                                                                            str = str + ".GridReorderTop_Outlook,.GridReorderBottom_Outlook{	height:12px;background:0 0 no-repeat url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".GridReorderBottom_Outlook{	background-position:0 -50px;}.RadGrid_Outlook .rgFilterRow{	background:#d6e5f3;}.RadGrid_Outlook .rgFilterRow td{    border:solid #89aee5;    border-width:0 0 1px 1px;}";
                                                                            str = str + ".RadGrid_Outlook .rgFilterBox{	border-color:#c3d9f9;	font:12px 'segoe ui',arial,sans-serif;	color:#000;}.RadMenu_Outlook .rgHCMClear,.RadMenu_Outlook .rgHCMFilter{	border-color:#003c74;background:#ebe8e2 center -23px repeat-x url('WebResource.axd?d=St7S2l7cwtEY3vZx-tmSepLGh-jewCe0pvZ2wcZu0c_JCt_uW5mG0XGf_Ht3tH4fCVfAI-eQ2CecEZZe1wwa7RJcGs2yFn5IhTesH2GxSh6n02OWhiDuQLJh2wFqih047MbknHiOf7D-PGxZwPtf4qVhfJxv1G0-9wvmKUBBei81&t=635737863581364401');	color:#000;	font-family:'segoe ui',arial,sans-serif;}";
                                                                            str = str + ".RadGrid_Outlook .rgGroupPanel{	border:0;	border-bottom:1px solid #002d96;background:#d5e4f3 -1900px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgGroupPanel td{	border:0;	padding:3px 4px;	vertical-align:middle;}.RadGrid_Outlook .rgGroupPanel td td{	padding:0;}.RadGrid_Outlook .rgGroupPanel .rgSortAsc{	height:12px;	background-position:4px -146px;}.RadGrid_Outlook .rgGroupPanel .rgSortDesc{	height:12px;	background-position:4px -96px;}";
                                                                            str = str + ".RadGrid_Outlook .rgUngroup{	background-position:0 -7200px;}.RadGrid_Outlook .rgGroupItem{	border:1px solid #4b78ca;background:#eae7e2 0 -7000px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');	color:#000;}";
                                                                            str = str + ".RadGrid_Outlook .rgGroupHeader{    background:#d7e6f7;    font-size:1.1em;    line-height:21px;	color:#0045d6;}.RadGrid_Outlook .rgGroupHeader td{	border-top:1px solid #fcfdfe;	border-bottom:1px solid #8fb2e6;    padding-left:8px;}";
                                                                            str = str + ".RadGrid_Outlook td.rgGroupCol,.RadGrid_Outlook td.rgExpandCol{	background:#d7e6f7 none;	border-color:#d7e6f7;}.RadGrid_Outlook .rgGroupHeader .rgExpand{	background-position:5px -495px;}.RadGrid_Outlook .rgGroupHeader .rgCollapse{	background-position:3px -93px;}";
                                                                            str = str + ".RadGrid_Outlook .rgEditForm{	border-bottom:1px solid #d0d7e5;}.RadGrid_Outlook .rgUpdate{	background-position:0 -1800px;}.RadGrid_Outlook .rgCancel{	background-position:0 -1850px;}.RadGrid_Outlook .rgDetailTable{	border-color:#002d96;}";
                                                                            str = str + " .RadGrid_Outlook .rgExpand{	background-position:5px -496px;}.RadGrid_Outlook .rgCollapse{	background-position:3px -94px;}.RadGrid_Outlook .rgCommandRow{background:#043199 0 -2100px repeat-x url('WebResource.axd?d=IQKfA46SV5fvqmi5MVtgOsvXXMETqAMFIhPtQcbayEMtfVdegQsSo1Jq8clCAC0WDt0Tt6oBSn8O_qTiJnAZyvIBA9WeMFdwRGI1UtROLrkWXfw3Si9aisC3Bwpi1_e6lEdHh_h3ujLVZYmB4sMPyg2&t=635737863581364401');	color:#fff;}";
                                                                            str = str + ".RadGrid_Outlook .rgCommandCell{	border:0;	padding:0;}.RadGrid_Outlook thead .rgCommandCell{	border-bottom:1px solid #002d96;}.RadGrid_Outlook tfoot .rgCommandCell,.RadGrid_Outlook .rgMasterTable>tbody>tr.rgCommandRow .rgCommandCell{	border-top:1px solid #002d96;}";
                                                                            str = str + ".RadGrid_Outlook .rgCommandTable{	border:0;	border-bottom:1px solid #6797d1;}.RadGrid_Outlook .rgCommandTable td{	border:0;	padding:2px 7px;}.RadGrid_Outlook .rgCommandTable td td{padding:0;}";
                                                                            str = str + " .RadGrid_Outlook .rgCommandRow a{	color:#fff;	text-decoration:none;}.RadGrid_Outlook .rgAdd{	margin-right:3px;	background-position:0 -1650px;}.RadGrid_Outlook .rgRefresh{	margin-right:3px;	background-position:0 -1600px;}";
                                                                            str = str + ".RadGrid_Outlook .rgEdit{	background-position:0 -1700px;}.RadGrid_Outlook .rgDel{	background-position:0 -1750px;}.RadGrid_Outlook .rgExpXLS,.RadGrid_Outlook .rgExpDOC,.RadGrid_Outlook .rgExpPDF,.RadGrid_Outlook .rgExpCSV{	background-image:url('WebResource.axd?d=xbKdJhqo6ynr8KJiqQaGcfzbM2mu1K6lEl_dMSgFtZbpluoe-hb4Zk2hAwzXUjfstm-cNziJUUF4PtQfIM9LmTk-XVZDGo13y_sTnlr4g7UBtWK7PT8QnZmRI3GheQLme9ewCaesId-v-p3nJr_rsw2&t=635737863581364401');}";
                                                                            str = str + ".RadGrid_Outlook .rgExpXLS{	background-position:0 0;}.RadGrid_Outlook .rgExpDOC{	background-position:0 -50px;}.RadGrid_Outlook .rgExpPDF{	background-position:0 -100px;}.RadGrid_Outlook .rgExpCSV{	background-position:0 -150px;}";
                                                                            str = str + ".GridRowSelector_Outlook{background:#002d96;}.GridItemDropIndicator_Outlook{    border-top:1px dashed #002d96;}.GridToolTip_Outlook{	border:1px solid #6187b8;	padding:3px;	background:#ccddf5;	color:#000;}";
                                                                            str = str + " .RadGridRTL_Outlook .rgHeader:first-child,.RadGridRTL_Outlook th.rgResizeCol:first-child,.RadGridRTL_Outlook .rgFilterRow>td:first-child,.RadGridRTL_Outlook .rgRow>td:first-child,.RadGridRTL_Outlook .rgAltRow>td:first-child{	border-left-width:1px;	padding-left:7px;}";
                                                                          
                                                                            var styleStr = "<html><head><style type='text/css'>" + str + "</style></head>";
                                                                            
                                                                           var htmlcontent = styleStr + "<body>" + $find('<%= RadGrid1.ClientID %>').get_element().outerHTML + "</body></html>";
                                                                         

                                                                          

                                                                            previewWnd.document.open();
                                                                            previewWnd.document.write(htmlcontent);
                                                                            previewWnd.document.close();
                                                                            previewWnd.print();
                                                                            previewWnd.close();
                                                                        }
                                                                    }


                                                                </script>

                                                                <script type="text/javascript">


                                                                    //Approach 2
                                                                    //function to set the height of the scroll
                                                                    //<Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"  />
                                                                    function GridCreated() {
                                                                        //                                                        var scrollArea = document.getElementById("<%= RadGrid1.ClientID %>" + "_GridData");
                                                                        //                                                        var table = document.getElementById("gridPane2");
                                                                        //                                                        var height1=table.clientHeight-100;
                                                                        //                                                        scrollArea.style.height =height1+ "px";
                                                                    }


                                                                    window.onload = Resize;
                                                                    <%--function Resize() {
                                                                        //alert(screen.height);
                                                                        if (screen.height > 768) {
                                                                            //alert("1");
                                                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                                                        }
                                                                        else {
                                                                            //alert("2");
                                                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                                                        }
                                                                    }--%>



                                                                    /* function pageLoad() 
                                                                     {  
                                                                        var grid = $find("RadGrid1");  
                                                                        var columns = grid.get_masterTableView().get_columns();  
                                                                        for (var i = 0; i < columns.length; i++) 
                                                                        {  
                                                                         columns[i].resizeToFit();  
                                                                        }  
                                                                      }
                                                                     */

                                                                </script>



                                                            </radG:RadCodeBlock>

                                                            <!-- ToolBar End -->
                                                        
                                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select Company_ID,Company_Name from company"></asp:SqlDataSource>

                                           

                                   
                                     <div class="clearfix heading-box">
                                        <div class="col-md-12">
                                            <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                                <Items>
                                                    <radG:RadToolBarButton runat="server" CommandName="Print" 
                                                        Text="Print" ToolTip="Print">
                                                    </radG:RadToolBarButton>

                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" Text="">
                                                        <ItemTemplate>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" border="0">
                                                                    <tr>
                                                                        <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </radG:RadToolBarButton>--%>

                                                    <radG:RadToolBarButton runat="server" CommandName="Excel" 
                                                        Text="Excel" ToolTip="Excel">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Word" 
                                                        Text="Word" ToolTip="Word">
                                                    </radG:RadToolBarButton>
                                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh" 
                                                        Text="UnGroup" ToolTip="UnGroup">
                                                    </radG:RadToolBarButton>
                                                    <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                                </radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" Text="Count">
                                                        <ItemTemplate>
                                                            <div>
                                                                <table cellpadding="0" cellspacing="0" border="0" style="height: 30px">
                                                                    <tr>
                                                                        <td valign="middle">
                                                                            <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                                                        <td valign="middle">
                                                                            <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </ItemTemplate>
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                                    </radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton runat="server" 
                                                        Text="Reset to Default" ToolTip="Reset to Default">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" 
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Graph"  Text="Graph" ToolTip="Graph" Enabled="True"></radG:RadToolBarButton>--%>
                                                </Items>
                                            </radG:RadToolBar>
                                        </div>
                                    </div>

                                    <radG:RadGrid AllowMultiRowSelection="true" ID="RadGrid1" runat="server"
                                        AutoGenerateColumns="False" Skin="Outlook" Width="100%" Height="100%" AllowPaging="true"
                                        PageSize="50" AllowFilteringByColumn="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound1"
                                        OnItemCommand="RadGrid1_ItemDataBound1" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        OnPageIndexChanged="RadGrid1_PageIndexChanged" EnableHeaderContextMenu="true"
                                        AllowCustomPaging="false" OnPreRender="RadGrid1_PreRender"
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="false"
                                        OnGridExporting="RadGrid1_GridExporting" PagerStyle-AlwaysVisible="True" GridLines="Both" AllowSorting="true"
                                        Font-Size="11"
                                        Font-Names="Tahoma">
                                        <ExportSettings HideStructureColumns="true" />
                                        <%--      <ExportSettings HideStructureColumns="true"  IgnorePaging="true" OpenInNewWindow="true">
                                                <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="SushiBar menu" 
                                                    PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm"  />
                                            </ExportSettings>--%>
                                        <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="emp_code" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                                ShowExportToCsvButton="true" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" />
                                            <ItemStyle Height="25px" VerticalAlign="middle" />
                                            <CommandItemTemplate>
                                                <div>
                                                    <table style="height: 25px; vertical-align: middle; float: right">
                                                        <tr>
                                                            <td>
                                                                <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" Visible="false" />&nbsp;&nbsp;
                                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/employee/AddEditEmployee.aspx"
                                                                    Font-Underline="false" Visible="false">Add New Employee</asp:HyperLink>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="PrintKet" CssClass="btn red" runat="server" Text="Print Selected Employees" OnClick="PrintKet_Click"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <radG:GridBoundColumn ShowFilterIcon="False" CurrentFilterFunction="StartsWith" UniqueName="EmpCode" HeaderImageUrl="../frames/images/EMPLOYEE/Grid- employee.png"
                                                    HeaderText="Emp Code" DataField="emp_code"
                                                    Display="false">
                                                </radG:GridBoundColumn>
                                                <%--    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="1">
                                                    <ItemTemplate>
                                                     <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" /> 
                                                        <asp:CheckBox ID="chkemp" runat="server" />
                                                    </ItemTemplate>
                                                   <HeaderStyle Width="30px" />
                                                </radG:GridTemplateColumn> --%>
                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn2" HeaderStyle-Width="30px">
                                                </radG:GridClientSelectColumn>
                                                <radG:GridTemplateColumn ShowFilterIcon="False" AllowFiltering="False" UniqueName="TemplateColumnEC"
                                                    Display="false" HeaderText="Code" SortExpression="emp_code">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" Text='<%# "DPT"+ DataBinder.Eval(Container.DataItem,"emp_code").ToString()%>'
                                                            NavigateUrl='<%# "../Reports/Ket_Form.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                                            ID="empcode" />
                                                    </ItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckAll" runat="server"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridTemplateColumn>
                                                <%--                                <radG:GridTemplateColumn UniqueName="TCEmpName" HeaderText="Employee Name" SortExpression="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                        <ItemTemplate>
                                                            <asp:HyperLink runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"emp_name").ToString()%>'
                                                                NavigateUrl='<%# "AddEditEmployee.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                                                ID="empname" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>--%>


                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_name" HeaderText="Employee Name" FilterControlAltText="alphabetsonly"
                                                    DataField="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true" >
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <%--<HeaderStyle Width="300px" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_type" HeaderText="Pass Type"  FilterControlAltText="alphabetsonly"
                                                    DataField="emp_type" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                    <%--<HeaderStyle Width="90px" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ic_pp_number" HeaderText="IC/FIN Number"  FilterControlAltText="cleanstring"
                                                    DataField="ic_pp_number" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="empgroupname" HeaderText="Type" FilterControlAltText="alphabetsonly"
                                                    DataField="empgroupname" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID" HeaderText="Time Card ID"  FilterControlAltText="cleanstring"
                                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Designation" FilterControlAltText="alphabetsonly"
                                                    HeaderText="Designation" DataField="Designation" CurrentFilterFunction="contains"
                                                    AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="true" ShowFilterIcon="False" UniqueName="Department" FilterControlAltText="alphabetsonly"
                                                    HeaderText="Department" DataField="Department" CurrentFilterFunction="contains"
                                                    AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="hand_phone"  FilterControlAltText="numericonly"
                                                    HeaderText="Mobile" DataField="hand_phone" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="email" HeaderText="Email" 
                                                    DataField="email" CurrentFilterFunction="contains" AutoPostBackOnFilter="true" FilterControlAltText="cleanstring">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink">
                                                    <ItemTemplate>
                                                        <tt class="bodytxt">
                                                            <asp:ImageButton ID="btnedit" ToolTip="Edit" CssClass="fa edit-icon" AlternateText=" " 
                                                                runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridClientSelectColumn Visible="false" UniqueName="GridClientSelectColumn">
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="Nationality" HeaderText="Nationality" FilterControlAltText="alphabetsonly"
                                                    DataField="Nationality" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ShowFilterIcon="False" UniqueName="Trade" HeaderText="Trade" FilterControlAltText="alphabetsonly"
                                                    DataField="Trade" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                                    <HeaderStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                            <ClientEvents OnGridCreated="GridCreated" />
                                        </ClientSettings>
                                    </radG:RadGrid>

                                
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
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
        $("#RadGrid1_ctl00_ctl03_ctl01_PrintKet").click(function () {
            var _message = "";
          
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        });

        var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
        $.each(_inputs, function (index, val) {
            $(this).addClass($(this).attr('alt'));

        });
    </script>

</body>
</html>
