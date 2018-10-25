<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollDeductions.aspx.cs"
    Inherits="SMEPayroll.Payroll.PayrollDeductions" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
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
                        <li>Payroll Deductions</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Deduction</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payroll Deductions</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

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
                                        var myHeight = document.body.clientHeight;
                                        myHeight = myHeight - 100;
                                        document.getElementById('<%= RadGrid1.ClientID %>').style.height = myHeight;
                                        if (screen.height > 768) {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "90.7%";
                                        }
                                        else {
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "85.5%";
                                        }
                                    }--%>

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->
                            <radA:RadScriptManager ID="ScriptManager" runat="server">
                            </radA:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            


                            <div class="search-box padding-tb-10 clearfix">
                                                <div class="form-inline col-sm-12">
                                                    <div class="form-group">
                                                        <label>Search</label>
                                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                            runat="server" AppendDataBoundItems="true" AutoPostBack="True" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Employee</label>
                                                        <radG:RadComboBox ID="RadComboBoxPrjEmp" runat="server" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true" EmptyMessage="Select Employee" OnSelectedIndexChanged="RadComboBoxPrjEmp_SelectedIndexChanged"
                                                            DropDownWidth="375px"  Height="200px">
                                                            <HeaderTemplate>
                                                                <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <%--                                                <td style="width: 20px">
                                                            Select
                                                        </td>--%>
                                                                        <td style="width: 120px; font-family: Tahoma; font-size: 12px;">Emp Name</td>
                                                                        <td style="width: 80px; font-family: Tahoma; font-size: 12px;">Card No</td>
                                                                        <td style="width: 80px; font-family: Tahoma; font-size: 12px;">IC NO</td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <%--                                                <td style="width: 30px;">
                                                            <asp:CheckBox runat="server" ID="CheckBox" onclick="stopPropagation(event);" value='<%# DataBinder.Eval(Container.DataItem, "Value") %>'
                                                                Checked='<%# ((Container.DataItem != null) ? (( bool.Parse(DataBinder.Eval(Container.DataItem, "Selected").ToString())) ? true : false) : false) %>' />
                                                        </td>--%>
                                                                        <td style="width: 120px; font-family: Tahoma; font-size: 12px;">
                                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                                        </td>
                                                                        <td style="width: 80px; font-family: Tahoma; font-size: 12px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                                        </td>
                                                                        <td style="width: 80px; font-family: Tahoma; font-size: 12px;">
                                                                            <%# DataBinder.Eval(Container, "Attributes['ic_pp_number']")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </radG:RadComboBox>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Month</label>
                                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textfields form-control input-sm">
                                                            <asp:ListItem Value="13">All</asp:ListItem>
                                                            <asp:ListItem Value="01">January</asp:ListItem>
                                                            <asp:ListItem Value="02">February</asp:ListItem>
                                                            <asp:ListItem Value="03">March</asp:ListItem>
                                                            <asp:ListItem Value="04">April</asp:ListItem>
                                                            <asp:ListItem Value="05">May</asp:ListItem>
                                                            <asp:ListItem Value="06">June</asp:ListItem>
                                                            <asp:ListItem Value="07">July</asp:ListItem>
                                                            <asp:ListItem Value="08">August</asp:ListItem>
                                                            <asp:ListItem Value="09">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                            <div class="heading-box clearfix">
                                        <div class="col-md-12">

                                            <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px">
                                                <Items>
                                                    <radG:RadToolBarButton runat="server" CommandName="Print"
                                                        Text="Print" ToolTip="Print" CssClass="print-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton runat="server" Text="">
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
                                                        Text="Excel" ToolTip="Excel" CssClass="excel-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Word"
                                                        Text="Word" ToolTip="Word" CssClass="word-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                                    <%--<radG:RadToolBarButton IsSeparator="true">
                                            </radG:RadToolBarButton>--%>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh"
                                                        Text="UnGroup" ToolTip="UnGroup" CssClass="unGroup-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server" CommandName="Refresh"
                                                        Text="Clear Sorting" ToolTip="Clear Sorting" CssClass="clear-sorting-btn">
                                                    </radG:RadToolBarButton>
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
                                                    <radG:RadToolBarButton runat="server"
                                                        Text="Reset to Default" ToolTip="Reset to Default" CssClass="reset-default-btn">
                                                    </radG:RadToolBarButton>
                                                    <radG:RadToolBarButton runat="server"
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes" CssClass="save-changes-btn">
                                                    </radG:RadToolBarButton>
                                                    <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                                </Items>
                                            </radG:RadToolBar>

                                        </div>
                                    </div>
                                    <radG:RadGrid ID="RadGrid1" runat="server" OnUpdateCommand="RadGrid1_UpdateCommand"
                                        OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
                                        Skin="Outlook" Width="100%" OnItemDataBound="RadGrid1_ItemDataBound" OnNeedDataSource="RadGrid1_NeedDataSource"
                                        PageSize="50"
                                        AllowPaging="true"
                                        ItemStyle-Wrap="false"
                                        AlternatingItemStyle-Wrap="false"
                                        PagerStyle-AlwaysVisible="True"
                                        GridLines="Both"
                                        OnItemCreated="RadGrid1_ItemCreated"
                                        OnGridExporting="RadGrid1_GridExporting"
                                        Font-Size="11"
                                        Font-Names="Tahoma"
                                        EnableHeaderContextMenu="true"
                                        AllowSorting="true" AllowFilteringByColumn="true">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id" CommandItemDisplay="Bottom" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <%-- <HeaderStyle ForeColor="Navy" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" />
                                            <ItemStyle Height="25px" VerticalAlign="middle" />--%>

                                            <Columns>

                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridBoundColumn DataField="emp_code" Visible="false" HeaderText="Employee Code"
                                                    SortExpression="emp_code" UniqueName="emp_code">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name"
                                                    UniqueName="emp_name" Visible="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                                    ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Type" HeaderText="Description" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="Type" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id"
                                                    UniqueName="id" Visible="False">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                                    SortExpression="trx_amount" UniqueName="trx_amount" AllowFiltering="false">
                                                    <HeaderStyle Width="100px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_period" DataType="System.DateTime" HeaderText="Period" DataFormatString="{0:d/M/yyyy}"
                                                    SortExpression="trx_period" UniqueName="trx_period" AllowFiltering="false">
                                                    <HeaderStyle Width="100px" />
                                                </radG:GridBoundColumn>
                                                <%-- murugan--%>
                                                <radG:GridBoundColumn DataField="status" AllowFiltering="false" Visible="true" SortExpression="status"
                                                    HeaderText="Status" UniqueName="status">
                                                </radG:GridBoundColumn>

                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ButtonType="ImageButton"
                                                    ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" CssClass="btncnfrmbtn" />
                                                </radG:GridButtonColumn>
                                                <radG:GridBoundColumn DataField="CurrencyID" AllowFiltering="false"
                                                    UniqueName="CurrencyID" HeaderText="CurrencyID" Display="False">
                                                    <HeaderStyle Width="100px" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="ConversionOpt" AllowFiltering="false"
                                                    UniqueName="ConversionOpt" HeaderText="ConversionOpt" Visible="false">
                                                </radG:GridBoundColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="deduction.ascx" EditFormType="WebUserControl">
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add New Payroll Deduction" />
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
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
        $(document).on('click', '#imgbtnfetch', function () {
            //$(imgbtnfetch).click(function () {

            if ($(RadComboBoxPrjEmp_Input).val() === "Select Employee") {
                WarningNotification("Employee not selected.");
                $(RadComboBoxPrjEmp_Input).focus();
                event.preventDefault();
                return false;
            }
        });

        $(".btncnfrmbtn").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Deduction?", _id, "Confirm Delete", "Delete");
        });


        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }

    </script>
</body>
</html>
