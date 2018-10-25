<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicHolidays.aspx.cs"
    Inherits="SMEPayroll.Leaves.PublicHolidays" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">

<%--<div style="page-break-before: always">this div starts a new page</div>--%>



    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc3:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper multi-table-design">
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
                        <li>View Public Holidays List</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Holidays</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">VIEW PUBLIC HOLIDAYS LIST</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
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
                                    <%--window.onload = Resize;
                                    function Resize() {
                                        if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                        }
                                        else {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                        }
                                    }--%>
                                </script>

                            </radG:RadCodeBlock>

                            <radG1:RadScriptManager ID="ScriptManager" runat="server">
                            </radG1:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>

                            </div>


                            <uc2:GridToolBar ID="GridToolBar" runat="server" Width="100%"  Visible="false" />


                            <div>

                                <radG:RadGrid OnUpdateCommand="RadGrid1_UpdateCommand" OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand ="RadGrid1_ItemCommand" 
                                    OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GridLines="Both" Skin="Outlook"
                                    Width="100%" OnGridExporting="RadGrid1_GridExporting">
                                    <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="ID"
                                        CommandItemDisplay="Bottom">
                                        <Columns>
                                            <radG:GridBoundColumn ReadOnly="True" DataField="holiday_date" DataType="System.string"
                                                UniqueName="holiday_date" SortExpression="holiday_date" HeaderText="Date">
                                                <HeaderStyle Width="150px" />
                                                <ItemStyle Width="150px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn ReadOnly="True" Visible="False" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="holiday_name" UniqueName="holiday_name" SortExpression="holiday_name"
                                                HeaderText="Name">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="False" DataField="companyid" UniqueName="companyid"
                                                SortExpression="companyid" HeaderText="company">
                                            </radG:GridBoundColumn>
                                            <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="Editcolumn">
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            </radG:GridEditCommandColumn>
                                            <radG:GridButtonColumn ButtonType="ImageButton" 
                                                CommandName="Delete" Text="Delete" UniqueName="Deletecolumn" ConfirmDialogType="RadWindow" ConfirmTitle="Want to delete?">
                                                
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                <ItemStyle Width="35px" CssClass="clsCnfrmButton" HorizontalAlign="Center"/>
                                            </radG:GridButtonColumn>
                                            <%--<telerik:GridTemplateColumn UniqueName="rbDelete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="rbDelete" runat="server" ImageAlign="Middle" CssClass="fa delete-icon edit-icon-chrome font-red clsCnfrmButton" ImageUrl="_"  Value="">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                        </Columns>
                                        <EditFormSettings UserControlName="publicholidays.ascx" EditFormType="WebUserControl">
                                            <EditColumn UniqueName="Editcolumn">
                                            </EditColumn>
                                        </EditFormSettings>
                                        <ExpandCollapseColumn Visible="False">
                                            <%--<HeaderStyle Width="19px"></HeaderStyle>--%>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <%--<HeaderStyle Width="20px"></HeaderStyle>--%>
                                        </RowIndicatorColumn>
                                        <CommandItemSettings AddNewRecordText="Add New Holiday" />
                                    </MasterTableView>
                                </radG:RadGrid>
                                <%--SelectCommand="SELECT [holiday_date] holiday1,convert(varchar(15),[holiday_date],103)'holiday_date', [holiday_name], [ID],companyid FROM [public_holidays] where datepart(yyyy,holiday_date)=datepart(yyyy,getdate()) order by 1"--%>
                                <div id="divMessage" class="bodytxt" style="visibility: visible" runat="server">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label>
                                </div>

                                <div class="row padding-tb-10">
                                    <div class="col-md-12">
                                        <div id="divHeader" class="inline-block" style="cursor: pointer; visibility: visible" onclick="toggleDiv('divContent', 'img1')" runat="server" visible ="false">
                                            <img id="img1" alt="Import Holiday List" src="../Frames/Images/Other/expand.gif" />
                                        </div>
                                        <div class="inline-block" id="divimpHl" runat="server" visible ="false">
                                            Import Holiday List
                                       
                                        </div>
                                    </div>
                                </div>

                                <div id="divContent" style="overflow: hidden; display: none;" runat="server">
                                    <hr />
                                    <div class="row">
                                        <div class="form-inline col-sm-12">
                                            <div class="form-group">
                                                <label>Select File</label>                                           
                                                <input id="FileUpload" runat="server" name="FileUpload" type="file" class="textfields" size="58" />
                                                <%--<asp:RequiredFieldValidator
                                           ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload" Display="Static"
                                           ErrorMessage="Please Select File">*</asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator
                                                    ID="revFileUpload" runat="Server" ControlToValidate="FileUpload"
                                                    ErrorMessage="Please Select CSV Files"
                                                    ValidationExpression=".+\.(([cC][sS][vV]))">Please Select CSV Files</asp:RegularExpressionValidator>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="CmdUpload" runat="server" Text="Upload" CssClass="textfields btn red" OnClick="CmdUpload_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <label id="lblHlist" runat="server"></label>
                                            <asp:HyperLink ID="FileDownload" runat="server" Text="Download Holiday File" NavigateUrl="http://smepayroll.com/downloads.html" Target="_search">
                                            </asp:HyperLink>
                                        </div>
                                    </div>

                                </div>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="DELETE FROM [public_holidays] WHERE [ID] = @ID"
                                    InsertCommand="INSERT INTO [public_holidays] ([holiday_date], [holiday_name]) VALUES (@holiday_date, @holiday_name)"
                                    SelectCommand="SELECT [holiday_date] holiday1,convert(varchar(15),[holiday_date],103)'holiday_date', [holiday_name], [ID],companyid FROM [public_holidays] where year(holiday_date)=@year And (CompanyID=@company_id Or CompanyID=-1) order by 1" 
                                    UpdateCommand="UPDATE [public_holidays] SET [holiday_date] = @holiday_date, [holiday_name] = @holiday_name WHERE [ID] = @ID">
                                    <DeleteParameters>
                                        <asp:Parameter Name="ID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="holiday_date" Type="DateTime" />
                                        <asp:Parameter Name="holiday_name" Type="String" />
                                        <asp:Parameter Name="ID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="holiday_date" Type="DateTime" />
                                        <asp:Parameter Name="holiday_name" Type="String" />
                                    </InsertParameters>
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
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

    <%-- ////Added By Jammu Office//////////--%>
    <script src="../Frames/Script/collapseableDIV.js"></script>
    <%-- ////Added By Jammu Office ends//////////--%>


    <script type="text/javascript">
        $("input[type='button']").addClass("btn btn-sm red");
        $("input[type='button']").removeAttr("style");
        //$("input[type='image']").addClass("clsCnfrmButton");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }
        $(".clsCnfrmButton").click(function () {

            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this holiday?", _id, "Confirm Delete", "Delete");
        });




    </script>
</body>
</html>
