<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitCPF.aspx.cs" Inherits="SMEPayroll.CPF.SubmitCPF" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <telerik:RadCodeBlock runat="server">
        <script src="../Frames/Script/jquery-1.3.2.min.js" type="text/javascript"></script>



        <script language="javascript" type="text/javascript">
            function getOuterHTML(obj) {
                if (typeof (obj.outerHTML) == "undefined") {
                    var divWrapper = document.createElement("div");
                    var copyOb = obj.cloneNode(true);
                    divWrapper.appendChild(copyOb);
                    return divWrapper.innerHTML;
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


        <script language="javascript" type="text/javascript">

            function printdetails() {
                var divToPrint = document.getElementById("details_table");
                newWin = window.open("");
                newWin.document.write(divToPrint.outerHTML);
                newWin.document.close();
                newWin.print();
                newWin.close();
                return false;
            }

            function isNumericKeyStrokeDecimal(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                    return false;

                return true;
            }
            function isNumericKeyStroke(evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            }
            function GrandTotal() {
                document.form1.txtGrandTotal.value = parseFloat(TotalCPF);
                var TotalCPF = document.form1.txtTotalCPF.value;
                var CPFLatePayment = document.form1.txtCPFLatePayment.value;
                var FWL = document.form1.txtFWL.value;
                var FWLLatePayment = document.form1.txtFWLLatePayment.value;
                var SDL = document.form1.txtSDL.value;
                var DonationComChest = document.form1.txtDonationComChest.value;
                var MBMF = document.form1.txtMBMF.value;
                var SINDA = document.form1.txtSINDA.value;
                var CDAC = document.form1.txtCDAC.value;
                var ECF = document.form1.txtECF.value;

                TotalCPF = parseFloat(TotalCPF);
                CPFLatePayment = parseFloat(CPFLatePayment);
                FWL = parseFloat(FWL);
                FWLLatePayment = parseFloat(FWLLatePayment);
                SDL = parseFloat(SDL);
                DonationComChest = parseFloat(DonationComChest);
                MBMF = parseFloat(MBMF);
                SINDA = parseFloat(SINDA);
                CDAC = parseFloat(CDAC);
                ECF = parseFloat(ECF);

                if (isNaN(TotalCPF))
                    TotalCPF = 0;
                if (isNaN(CPFLatePayment))
                    CPFLatePayment = 0;
                if (isNaN(FWL))
                    FWL = 0;
                if (isNaN(FWLLatePayment))
                    FWLLatePayment = 0;
                if (isNaN(SDL))
                    SDL = 0;
                if (isNaN(DonationComChest))
                    DonationComChest = 0;
                if (isNaN(MBMF))
                    MBMF = 0;
                if (isNaN(SINDA))
                    SINDA = 0;
                if (isNaN(CDAC))
                    CDAC = 0;
                if (isNaN(ECF))
                    ECF = 0;
                document.form1.txtGrandTotal.value = TotalCPF + CPFLatePayment + FWL + FWLLatePayment + SDL + DonationComChest + MBMF + SINDA + CDAC + ECF;
            }
            function SubmitForm() {
                if (ValidateDate()) {
                    document.form1.check.value = 'true';
                    document.form1.submit();
                }
            }
            function ViewDetails() {
                document.form1.check.value = 'viewdetails';
                document.form1.submit();
            }
            function ValidateDoner() {
                var DonationComChest = document.form1.txtDonationComChest.value;
                var DonationComChestDC = document.form1.txtDCDonationComChest.value;
                var MBMF = document.form1.txtMBMF.value;
                var MBMFDC = document.form1.txtDCMBMF.value;
                var SINDA = document.form1.txtSINDA.value;
                var SINDADC = document.form1.txtDCSINDA.value;
                var CDAC = document.form1.txtCDAC.value;
                var CDACDC = document.form1.txtDCCDAC.value;
                var ECF = document.form1.txtECF.value;
                var ECFDC = document.form1.txtDCECF.value;
                DonationComChest = parseFloat(DonationComChest);
                MBMF = parseFloat(MBMF);
                SINDA = parseFloat(SINDA);
                CDAC = parseFloat(CDAC);
                ECF = parseFloat(ECF);
                DonationComChestDC = parseFloat(DonationComChestDC);
                MBMFDC = parseFloat(MBMFDC);
                SINDADC = parseFloat(SINDADC);
                CDACDC = parseFloat(CDACDC);
                ECFDC = parseFloat(ECFDC);
                WarningNotification(MBMF);
                WarningNotification(MBMFDC);
                var sMSG = ""
                if (DonationComChest > 0 && DonationComChestDC < 0)
                    sMSG += "Donation to community chest <br/>";
                if (MBMF > 0 && MBMFDC < 0)
                    WarningNotification(MBMF);
                // sMSG +="MBMF <br/>";
                if (SINDA > 0 && SINDADC < 0)
                    sMSG += "SINDA<br/>";
                if (CDAC > 0 && CDACDC < 0)
                    sMSG += "CDAC <br/>";
                if (ECF > 0 && ECFDC < 0)
                    sMSG += "ECF <br/>";
                if (sMSG == "")
                    return true;
                else
                { sMSG = "Following Donner Count can not be Zero.<br/>" + sMSG; WarningNotification(sMSG); return false; }

            }
            function Print() {
                window.print();
            }
            function ValidateDate() {
                var DonationComChest = document.form1.txtDonationComChest.value;
                var DonationComChestDC = document.form1.txtDCDonationComChest.value;
                var MBMF = document.form1.txtMBMF.value;
                var MBMFDC = document.form1.txtDCMBMF.value;
                var SINDA = document.form1.txtSINDA.value;
                var SINDADC = document.form1.txtDCSINDA.value;
                var CDAC = document.form1.txtCDAC.value;
                var CDACDC = document.form1.txtDCCDAC.value;
                var ECF = document.form1.txtECF.value;
                var ECFDC = document.form1.txtDCECF.value;
                DonationComChest = parseFloat(DonationComChest);
                MBMF = parseFloat(MBMF);
                SINDA = parseFloat(SINDA);
                CDAC = parseFloat(CDAC);
                ECF = parseFloat(ECF);
                DonationComChestDC = parseFloat(DonationComChestDC);
                MBMFDC = parseFloat(MBMFDC);
                SINDADC = parseFloat(SINDADC);
                CDACDC = parseFloat(CDACDC);
                ECFDC = parseFloat(ECFDC);
                /*  Hidden values */
                var DonationComChest1 = document.form1.txtHDonationComChest.value;
                var DonationComChestDC1 = document.form1.txtHDCDonantionComChest.value;
                var MBMF1 = document.form1.txtHMBMF.value;
                var MBMFDC1 = document.form1.txtHDCMBMF.value;
                var SINDA1 = document.form1.txtHSINDA.value;
                var SINDADC1 = document.form1.txtHDCSINDA.value;
                var CDAC1 = document.form1.txtHCDAC.value;
                var CDACDC1 = document.form1.txtHDCCDAC.value;
                var ECF1 = document.form1.txtHECF.value;
                var ECFDC1 = document.form1.txtHDCECF.value;
                DonationComChest1 = parseFloat(DonationComChest1);
                MBMF1 = parseFloat(MBMF1);
                SINDA1 = parseFloat(SINDA1);
                CDAC1 = parseFloat(CDAC1);
                ECF1 = parseFloat(ECF1);
                DonationComChestDC1 = parseFloat(DonationComChestDC1);
                MBMFDC1 = parseFloat(MBMFDC1);
                SINDADC1 = parseFloat(SINDADC1);
                CDACDC1 = parseFloat(CDACDC1);
                ECFDC1 = parseFloat(ECFDC1);

                var sMSG = "";
                if (DonationComChest1 > DonationComChest)
                    sMSG = "DonationComChest <br/>";
                if (DonationComChestDC1 > DonationComChestDC)
                    sMSG += "DonationComChest Donner Count <br/>";
                if (MBMF1 > MBMF)
                    sMSG += "MBMF <br/>";
                if (MBMFDC1 > MBMFDC)
                    sMSG += "MBMF Donner Count <br/>";
                if (SINDA1 > SINDA)
                    sMSG += "SINDA <br/>";
                if (SINDADC1 > SINDADC)
                    sMSG += "SINDA Donner Count<br/>";
                if (CDAC1 > CDAC)
                    sMSG += "CDAC <br/>";
                if (CDACDC1 > CDACDC)
                    sMSG += "CDAC Donner Count <br/>";
                if (ECF1 > ECF)
                    sMSG += "ECF <br/>";
                if (ECFDC1 > ECFDC)
                    sMSG += "ECF Donner Count<br/>";

                if (sMSG == "")
                    return true;
                else {
                    sMSG = "Following Contribution can not be less than their calculated values <br/>" + sMSG;
                    WarningNotification(sMSG);
                    return false;
                }

            }
        </script>

        <%--    

<script language="JavaScript1.2" type="text/javascript"> 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 


</script>--%>
    </telerik:RadCodeBlock>

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
                        <li>Generate CPF File</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="reportsDashboard">Reports</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>CPF File</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Generate CPF File</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server" AsyncPostBackTimeout="3600" EnableScriptCombine="False" />
                            <%-- <ajaxToolkit:ToolkitScriptManager runat="server" ID="ToolkitScriptManager1" CombineScripts="false"   />--%>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <div id="details_table">

                                <div class="search-box padding-tb-10 clearfix">
                                    <div class="form-inline col-md-12">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:CheckBox ID="chkCPFOld" runat="server" Text="New Version" Checked="true" />
                                        </div>
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:CheckBox ID="chkFWL" runat="server" Text="Foreign Worker Levy(FWL): " />
                                        </div>
                                        <div class="form-group">
                                            <label>CPF Number</label>
                                            <select id="cmbEmployerCPFAcctNumber" runat="server" class="textfields form-control input-sm">
                                            </select>
                                        </div>
                                        <div class="form-group">
                                            <label>Month</label>
                                            <asp:DropDownList ID="cmbMonth" runat="server" AutoPostBack="false"
                                                CssClass="textfields form-control input-sm">
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
                                            <label>Year</label>
                                            <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                runat="server" AutoPostBack="false" AppendDataBoundItems="true">
                                                <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>Advice No</label>
                                            <asp:DropDownList ID="ADVICE_NO" runat="server" AutoPostBack="false" CssClass="textfields form-control input-sm">
                                                <asp:ListItem Selected="true" Value="01">01</asp:ListItem>

                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" runat="server">GO</asp:LinkButton>
                                        </div>
                                    </div>

                                    <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                    <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                </div>

                                <input type="hidden" id="txtHDonationComChest" name="txtHDonationComChest" runat="server" />
                                <input type="hidden" id="txtHDCDonantionComChest" name="txtHDCDonantionComChest" runat="server" />
                                <input type="hidden" id="txtHMBMF" name="txtHMBMF" runat="server" />
                                <input type="hidden" id="txtHDCMBMF" name="txtHDCMBMF" runat="server" />
                                <input type="hidden" id="txtHSINDA" name="txtHSINDA" runat="server" />
                                <input type="hidden" id="txtHDCSINDA" name="txtHDCSINDA" runat="server" />
                                <input type="hidden" id="txtHCDAC" name="txtHCDAC" runat="server" />
                                <input type="hidden" id="txtHDCCDAC" name="txtHDCCDAC" runat="server" />
                                <input type="hidden" id="txtHECF" name="txtHECF" runat="server" />
                                <input type="hidden" id="txtHDCECF" name="txtHDCECF" runat="server" />
                                <input type="hidden" id="check" name="check" runat="server" />

                                <div class="clearfix margin-bottom-20 bg-default">
                                    <div class="col-md-6">
                                        <h4>Contribution Summary</h4>
                                    </div>
                                    <div class="col-md-6 text-right">
                                        <asp:Button Visible="false" ID="btndetail" Height="25px" runat="server" Text="View Details"
                                            OnClientClick="ViewDetails();" />

                                        <asp:Button ID="print_details" Text="Print" OnClientClick="printdetails()" runat="server" CssClass="btn default btn-sm" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">



                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Total CPF Contributions</label>
                                            <div class="col-sm-3">
                                                <input id="txtTotalCPF" readonly type="text" class="textfields form-control input-sm" runat="server" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">CPF Late Payment Interest</label>
                                            <div class="col-sm-3">
                                                <input id="txtCPFLatePayment" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" onkeyup="GrandTotal();"
                                                    maxlength="12" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Foreign Worker Levy(FWL)</label>
                                            <div class="col-sm-3">
                                                <input id="txtFWL" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" onkeyup="GrandTotal();"
                                                    maxlength="12" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">FWL Late Payment Interest</label>
                                            <div class="col-sm-3">
                                                <input id="txtFWLLatePayment" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Skills Development Levy(SDL)</label>
                                            <div class="col-sm-3">
                                                <input id="txtSDL" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Donation to Community Chest</label>
                                            <div class="col-sm-3">
                                                <input id="txtDonationComChest" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                            <label class="col-sm-2 control-label">Donor Count</label>
                                            <div class="col-sm-3">
                                                <input id="txtDCDonationComChest" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStroke(event);" maxlength="7" />
                                            </div>
                                        </div>

                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Total MBMF Contributions</label>
                                            <div class="col-sm-3">
                                                <input id="txtMBMF" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                            <label class="col-sm-2 control-label">Donor Count</label>
                                            <div class="col-sm-3">
                                                <input id="txtDCMBMF" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStroke(event);" maxlength="7" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Total SINDA Contributions</label>
                                            <div class="col-sm-3">
                                                <input id="txtSINDA" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                            <label class="col-sm-2 control-label">Donor Count</label>
                                            <div class="col-sm-3">
                                                <input id="txtDCSINDA" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStroke(event);" maxlength="7" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Total CDAC Contributions</label>
                                            <div class="col-sm-3">
                                                <input id="txtCDAC" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                            <label class="col-sm-2 control-label">Donor Count</label>
                                            <div class="col-sm-3">
                                                <input id="txtDCCDAC" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStroke(event);" maxlength="7" />
                                            </div>
                                        </div>
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Total ECF Contributions</label>
                                            <div class="col-sm-3">
                                                <input id="txtECF" type="text" class="textfields form-control input-sm custom-maxlength number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStrokeDecimal(event);" maxlength="12" onkeyup="GrandTotal();" />
                                            </div>
                                            <label class="col-sm-2 control-label">Donor Count</label>
                                            <div class="col-sm-3">
                                                <input id="txtDCECF" type="text" class="textfields form-control input-sm custom-maxlength number-dot number-dot" runat="server"
                                                    onkeypress="return isNumericKeyStroke(event);" maxlength="7" />
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="form-group clearfix">
                                            <label class="col-sm-3 control-label">Grand Total</label>
                                            <div class="col-sm-3">
                                                <input type="text"
                                                    id="txtGrandTotal" name="txtGrandTotal" class="textfields form-control input-sm" runat="server"
                                                    readonly />
                                                <asp:Button ID="btnPrint" Visible="false" runat="server" Text="Print" OnClientClick="Print();" />
                                                <asp:Label ID="WarningLbkl" runat="server"></asp:Label>
                                            </div>
                                        </div>


                                    </div>



                                </div>


                            </div>




                            <br />
                            <div id="grid" runat="server">

                                <div class="clearfix heading-box">
                                    <div class="col-md-12">
                                        <radA:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                            OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px" >
                                            <Items>
                                                <radA:RadToolBarButton runat="server" CommandName="Print"
                                                    Text="Print" ToolTip="Print" CssClass="print-btn">
                                                </radA:RadToolBarButton>
                                                <%--<radA:RadToolBarButton IsSeparator="true">
                                            </radA:RadToolBarButton>--%>
                                                <%--<radA:RadToolBarButton runat="server" Text="">
                                            <ItemTemplate>
                                                <div>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </radA:RadToolBarButton>--%>
                                                <radA:RadToolBarButton runat="server" CommandName="Excel"
                                                    Text="Excel" ToolTip="Excel" CssClass="excel-btn" id="excelbtn" Enabled ="false" >
                                                </radA:RadToolBarButton>
                                                <radA:RadToolBarButton runat="server" CommandName="Word"
                                                    Text="Word" ToolTip="Word" CssClass="word-btn" id="wordbtn"  Enabled ="false">
                                                </radA:RadToolBarButton>
                                                <radA:RadToolBarButton runat="server" CommandName="PDF" Text="PDF" ToolTip="PDF" CssClass="pdf-btn" id="pdfbtn"  Enabled ="false">
                                                </radA:RadToolBarButton>
                                                <%--<radA:RadToolBarButton IsSeparator="true">
                                            </radA:RadToolBarButton>--%>
                                                <radA:RadToolBarButton runat="server" CommandName="Refresh"
                                                    Text="UnGroup" ToolTip="UnGroup" CssClass="ungroup-btn" Visible ="false">
                                                </radA:RadToolBarButton>
                                                <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                </radG:RadToolBarButton>--%>
                                                <%--<radA:RadToolBarButton IsSeparator="true">
                                            </radA:RadToolBarButton>--%>
                                                <radA:RadToolBarButton runat="server" Text="Count">
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
                                                </radA:RadToolBarButton>
                                                <%--<radA:RadToolBarButton IsSeparator="true">
                                            </radA:RadToolBarButton>--%>
                                                <telerik:RadToolBarButton runat="server"
                                                    Text="Reset to Default" ToolTip="Reset to Default" CssClass="reset-default-btn" Visible ="false">
                                                </telerik:RadToolBarButton>
                                                <telerik:RadToolBarButton runat="server"
                                                    Text="Save Grid Changes" ToolTip="Save Grid Changes" CssClass="save-changes-btn" Visible ="false">
                                                </telerik:RadToolBarButton>
                                                <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                            </Items>
                                        </radA:RadToolBar>
                                    </div>
                                </div>

                                <telerik:RadGrid Visible="false" ID="RadGrid1" CssClass="radGrid-single" runat="server"
                                    Skin="Default" Width="94%" AllowPaging="false" OnGridExporting="RadGrid10_GridExporting">
                                    <MasterTableView AutoGenerateColumns="False" GridLines="Both">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" UniqueName="emp_name"
                                                SortExpression="emp_name">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fund_type" HeaderText="Fund Type" UniqueName="fund_type"
                                                SortExpression="fund_type">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="fund_amount" HeaderText="Fund Amount" DataFormatString="{0:N2}"
                                                DataType="System.Decimal" UniqueName="fund_amount" SortExpression="fund_amount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="empcpf" HeaderText="Employee CPF" DataFormatString="{0:N2}"
                                                DataType="System.Decimal" UniqueName="empcpf" SortExpression="empcpf">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="employercpf" HeaderText="Employer CPF" DataFormatString="{0:N2}"
                                                DataType="System.Decimal" UniqueName="employercpf" SortExpression="employercpf">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="cpfamount" HeaderText="CPFAmount" DataFormatString="{0:N2}"
                                                DataType="System.Decimal" UniqueName="cpfamount" SortExpression="cpfamount">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Gross" HeaderText="CpfGross" DataFormatString="{0:N2}"
                                                DataType="System.Decimal" UniqueName="netpay" SortExpression="netpay">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="sdl" HeaderText="SDL" DataType="System.Decimal"
                                                DataFormatString="{0:N2}" UniqueName="sdl" SortExpression="sdl">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <ExpandCollapseColumn Visible="False">
                                            <HeaderStyle Width="19px"></HeaderStyle>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px"></HeaderStyle>
                                        </RowIndicatorColumn>
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                    SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:SessionParameter Name="companyid" SessionField="Compid" Type="Int32" />
                                        <asp:ControlParameter ControlID="cmbEmployerCPFAcctNumber" Name="csnno" PropertyName="Value"
                                            Type="string" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>

                            <div class="row padding-tb-20">
                                <div class="col-md-12 text-center">
                                    <input type="button" id="btnSave" name="btnSave" runat="server" value="Generate CPF"
                                        class="textfields btn red" onclick="SubmitForm();" />
                                    <%--<input type="button" id="btnPaybillBack" name="btnPaybillBack" runat="server" value="Back"
                                        class="textfields btn default" onclick="history.back();" />--%>

                                        
                                                <h4 class="panel-title inline">
                                                    <a class="accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                                </h4>
                                            


                                    <div class="panel-group accordion accordion-note no-margin" id="accordion3">
                                        <div class="panel panel-default shadow-none">
                                            
                                            <div id="collapse_3_1" class="panel-collapse collapse">
                                                <div class="panel-body border-top-none no-padding text-left">
                                                    <div class="note-custom note">
                                                        <p class="color-red">*Press and hold down the CTRL key while you Click on "Generate CPF" Button If File is not Generated</p>
                                                        <%-- muru --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>



                            <%--<radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="cmbMonth">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                        <radA:AjaxUpdatedControl ControlID="txtHDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCDonantionComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtHMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtHSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtHCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtHECF" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCECF" />
                        <radA:AjaxUpdatedControl ControlID="check" />
                        <radA:AjaxUpdatedControl ControlID="txtTotalCPF" />
                        <radA:AjaxUpdatedControl ControlID="txtCPFLatePayment" />
                        <radA:AjaxUpdatedControl ControlID="txtFWL" />
                        <radA:AjaxUpdatedControl ControlID="txtFWLLatePayment" />
                        <radA:AjaxUpdatedControl ControlID="txtSDL" />
                        <radA:AjaxUpdatedControl ControlID="txtDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtDCDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtDCMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtDCSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtDCCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtECF" />
                        <radA:AjaxUpdatedControl ControlID="txtDCECF" />
                        <radA:AjaxUpdatedControl ControlID="txtGrandTotal" />
                    </UpdatedControls>
                </radA:AjaxSetting>
                <radA:AjaxSetting AjaxControlID="cmbYear">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                        <radA:AjaxUpdatedControl ControlID="txtHDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCDonantionComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtHMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtHSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtHCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtHECF" />
                        <radA:AjaxUpdatedControl ControlID="txtHDCECF" />
                        <radA:AjaxUpdatedControl ControlID="txtTotalCPF" />
                        <radA:AjaxUpdatedControl ControlID="txtCPFLatePayment" />
                        <radA:AjaxUpdatedControl ControlID="txtFWL" />
                        <radA:AjaxUpdatedControl ControlID="txtFWLLatePayment" />
                        <radA:AjaxUpdatedControl ControlID="txtSDL" />
                        <radA:AjaxUpdatedControl ControlID="txtDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtDCDonationComChest" />
                        <radA:AjaxUpdatedControl ControlID="txtMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtDCMBMF" />
                        <radA:AjaxUpdatedControl ControlID="txtSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtDCSINDA" />
                        <radA:AjaxUpdatedControl ControlID="txtCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtDCCDAC" />
                        <radA:AjaxUpdatedControl ControlID="txtECF" />
                        <radA:AjaxUpdatedControl ControlID="txtDCECF" />
                        <radA:AjaxUpdatedControl ControlID="txtGrandTotal" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>--%>
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

        $(imgbtnfetch).click(function () {
            var alertmsg = "";
            if( $(cmbMonth).val() == "-1")
            {
                alertmsg = "Please select Month <br/>";
            }
            if ($(cmbYear).val() == "-1") {
                alertmsg += " Please select Year";

            }
            if(alertmsg != "")
            {
                WarningNotification(alertmsg);
                return false;
            }
        });
    </script>

</body>
</html>
