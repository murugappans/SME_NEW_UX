<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollAdditions.aspx.cs"
    Inherits="SMEPayroll.Payroll.PayrollAddittions" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js"> </script>

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
                        <li>Payroll Additions</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Addition</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payroll Additions</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG1:RadScriptManager ID="ScriptManager" runat="server" ScriptMode="Release">
                            </radG1:RadScriptManager>
                            <!-- ToolBar -->
                            <radG1:RadCodeBlock ID="newcodeid" runat="server">
                                <script language="javascript" type="text/javascript">
                                    String.prototype.trim = function () {
                                        a = this.replace(/^\s+/, '');
                                        return a.replace(/\s+$/, '');
                                    };

                                    function checkDate(fld, msg) {
                                        var newmsg = "";
                                        var mo, day, yr;
                                        var entry = fld.value;
                                        var re = /\b\d{1,2}[\/-]\d{1,2}[\/-]\d{4}\b/;
                                        if (re.test(entry)) {
                                            var delimChar = (entry.indexOf("/") != -1) ? "/" : "-";
                                            var delim1 = entry.indexOf(delimChar);
                                            var delim2 = entry.lastIndexOf(delimChar);
                                            day = parseInt(entry.substring(0, delim1), 10);
                                            mo = parseInt(entry.substring(delim1 + 1, delim2), 10);
                                            yr = parseInt(entry.substring(delim2 + 1), 10);
                                            var testDate = new Date(yr, mo - 1, day);
                                            if (testDate.getDate() == day) {
                                                if (testDate.getMonth() + 1 == mo) {
                                                    if (testDate.getFullYear() == yr) {
                                                        return true;
                                                    } else {
                                                        newmsg = msg + " has a problem with the year entry.";
                                                    }
                                                } else {
                                                    newmsg = msg + " has a problem with the month entry.";
                                                }
                                            } else {
                                                newmsg = msg + " has a problem with the date entry.";
                                            }
                                        } else {
                                            newmsg = msg + " has Incorrect date format. Enter as dd/mm/yyyy.";
                                        }
                                        return newmsg;
                                    }

                                    function ValidateForm() {
                                        var strmsg = '';
                                        var txthid = document.getElementById('txtRadId').value + "_";

                                        var obj = document.getElementById(txthid + 'tr1');
                                        if (obj.style.display == "block") {
                                            var ctrl1 = document.getElementById(txthid + 'txtbasis_arriving_payment');
                                            var ctrl2 = document.getElementById(txthid + 'txtservice_length');
                                            var ctrl3 = document.getElementById(txthid + 'drpiras_approval');
                                            var ctrl4 = document.getElementById(txthid + 'txtiras_approval_date');

                                            if (ctrl1.value.trim().length <= 0) {
                                                strmsg = strmsg + "Please enter Basis Arriving Payment." + "\n";
                                            }

                                            if (ctrl2.value.trim().length <= 0) {
                                                strmsg = strmsg + "Please enter Service Length." + "\n";
                                            }
                                            else {
                                                if (isNaN(ctrl2.value) == true) {
                                                    strmsg = strmsg + "Please enter numeric value in Service Length." + "\n";
                                                }
                                            }

                                            if (ctrl3.value == "Yes") {
                                                if (ctrl4.value.trim().length <= 0) {
                                                    strmsg = strmsg + "Please enter IRAS Approval Date." + "\n";
                                                }
                                                else {
                                                    var strdate = checkDate(ctrl4, 'Approval Date');
                                                    if (strdate.length >= 0) {
                                                        strmsg = strmsg + strdate + "\n";
                                                    }
                                                }
                                            }
                                        }

                                        var ctrl5 = document.getElementById(txthid + 'drpAdditionYear');
                                        var ctrl6 = document.getElementById(txthid + 'txtamt');
                                        var ctrl7 = document.getElementById(txthid + 'drpaddtype');
                                        var ctrl8 = document.getElementById(txthid + 'drpemployee');
                                        //alert(ctrl5);txtamt drpemployee

                                        if (ctrl8.value == '-select-') {
                                            strmsg = strmsg + "Payroll Additions - Please select an Employee." + "\n";
                                        }
                                        if (ctrl6.value.trim().length <= 0) {
                                            strmsg = strmsg + "Payroll Additions - Please enter an Amount" + "\n";
                                        }
                                        if (ctrl5.value == '') {
                                            strmsg = strmsg + "Payroll Additions - Please select year for the Addition." + "\n";
                                        }
                                        if (ctrl7.value == '-select-') {
                                            strmsg = strmsg + "Payroll Additions - Please select an Addition type." + "\n";
                                        }

                                        var numericheck = CheckNumeric(ctrl6.value, "\nPayroll Additions -Amount");
                                        if (numericheck != "")
                                            strmsg += numericheck;

                                        if (strmsg.length > 0) {
                                            alert(strmsg);
                                            strmsg = "";
                                            return false;
                                        }
                                    }

                                    function selectByValue(select, value) {
                                        for (var i = 0; i < select.options.length; i++) {
                                            if (value == select.options[i].value) {
                                                select.selectedIndex = i;
                                                break; // Break out to stop at the first value
                                            }
                                        }
                                    }

                                    function EnableApproval() {

                                        var txthid = document.getElementById('txtRadId').value + "_";
                                        document.getElementById(txthid + 'txtiras_approval_date').value = '';

                                        if (document.getElementById(txthid + 'drpiras_approval').value == "Yes") {
                                            document.getElementById(txthid + 'tr4').style.display = "block";
                                        }
                                        else {
                                            document.getElementById(txthid + 'tr4').style.display = "none";
                                        }
                                    }
                                    function EnablePayableOtions() {
                                        var txthid = document.getElementById('txtRadId').value + "_";
                                        var oOption = document.getElementById('drplumsumswitch');
                                        var strOptions = oOption.options[oOption.selectedIndex].text;
                                        var drplumsum = document.getElementById(txthid + 'drplumsum');

                                        selectByValue(drplumsum, document.getElementById(txthid + 'drpaddtype').value);

                                        var intdex = drplumsum.selectedIndex;
                                        var txt = drplumsum.options[intdex].text;
                                        document.getElementById('txtLumSum').value = txt;
                                        var strCompare = "," + document.getElementById('txtLumSum').value + ",";

                                        if (strOptions.indexOf(strCompare) >= 0) {
                                            document.getElementById(txthid + 'tr1').style.display = "block";
                                            document.getElementById(txthid + 'tr2').style.display = "block";
                                            document.getElementById(txthid + 'tr3').style.display = "block";
                                            document.getElementById(txthid + 'tr4').style.display = "none";
                                            var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                                            oSwitch.selectedIndex = 0;
                                        }
                                        else {
                                            document.getElementById(txthid + 'txtbasis_arriving_payment').value = "";
                                            document.getElementById(txthid + 'txtservice_length').value = "";
                                            document.getElementById(txthid + 'tr1').style.display = "none";
                                            document.getElementById(txthid + 'tr2').style.display = "none";
                                            document.getElementById(txthid + 'tr3').style.display = "none";
                                            document.getElementById(txthid + 'tr4').style.display = "none";
                                            var oSwitch = document.getElementById(txthid + 'drpiras_approval');
                                            oSwitch.selectedIndex = 0;
                                        }
                                    }
                                </script>

                                <%--   <script language="JavaScript1.2"> 
                    <!-- 

                    if (document.all)
                    window.parent.defaultconf=window.parent.document.body.cols
                    function expando(){
                    window.parent.expandf()

                    }
                    document.ondblclick=expando 

                    -->
                </script>--%>
                            </radG1:RadCodeBlock>
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
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                           


                             <div class="search-box padding-tb-10 clearfix">
                                                <div class="form-inline col-sm-12">
                                                    <div class="form-group">
                                                        <label>Search</label>
                                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                            runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>

                                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                    </div>
                                                    <div class="form-group">
                                                        <label>Employee</label>
                                                        <radG:RadComboBox ID="RadComboBoxPrjEmp" runat="server" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested"
                                                            EnableLoadOnDemand="true" HighlightTemplatedItems="true"
                                                            EmptyMessage="Select Employee" OnSelectedIndexChanged="RadComboBoxPrjEmp_SelectedIndexChanged"
                                                            DropDownWidth="375px"  Height="200px">
                                                            <HeaderTemplate>
                                                                <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <%--<td style="width: 20px">
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
                                            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                                border="0">
                                                <asp:XmlDataSource ID="XmldtTaxPayableTypeLumSumSwtich" runat="server" DataFile="~/XML/xmldata.xml"
                                                    XPath="SMEPayroll/Tax/TaxPayableTypeLumSumSwtich"></asp:XmlDataSource>
                                                <tr style="display: none;">
                                                    <td>
                                                        <asp:DropDownList ID="drplumsumswitch" DataTextField="text" DataValueField="id" DataSourceID="XmldtTaxPayableTypeLumSumSwtich"
                                                            class="textfields" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>
                                    <div class="heading-box clearfix">
                                        <div class="col-md-12">
                                            <!-- grid -->
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
                                                        Text="UnGroup" ToolTip="UnGroup" CssClass="ungroup-btn">
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
                                    
                                    <radG:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                        OnUpdateCommand="RadGrid1_UpdateCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        OnInsertCommand="RadGrid1_InsertCommand" Skin="Outlook" Width="100%"
                                        OnNeedDataSource="RadGrid1_NeedDataSource"
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
                                        AllowSorting="true">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,id,desc" CommandItemDisplay="Bottom" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <%--<HeaderStyle ForeColor="Navy" Height="25px" />--%>
                                            <%--<AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" Wrap="false" />--%>
                                            <%--<ItemStyle Height="25px" VerticalAlign="middle" Wrap="false" />--%>
                                            <Columns>

                                                <radG:GridBoundColumn DataField="emp_code" Visible="false" HeaderText="Code" SortExpression="emp_code"
                                                    UniqueName="emp_code">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" DataField="emp_name" AllowFiltering="false"
                                                    HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                                    <%--<HeaderStyle Width="200px"  />
                                                    <ItemStyle Width="200px"  />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                                    ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="desc" HeaderText="Description" AutoPostBackOnFilter="true"
                                                    CurrentFilterFunction="contains" ReadOnly="True" SortExpression="desc" UniqueName="Type" ShowFilterIcon="false" FilterControlAltText="cleanstring">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="Type"
                                                    Visible="False" UniqueName="Type1">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_amount" AllowFiltering="false" DataType="System.Decimal"
                                                    HeaderText="Amount" SortExpression="trx_amount" UniqueName="trx_amount">
                                                    <%--<HeaderStyle Width="70px" HorizontalAlign="Right" />
                                                    <ItemStyle Width="70px" HorizontalAlign="Right" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="trx_period" AllowFiltering="false" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                                                    HeaderText="Period" SortExpression="trx_period" UniqueName="trx_period">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="cpf" AllowFiltering="false" DataType="System.string"
                                                    HeaderText="Cpf" SortExpression="cpf" UniqueName="cpf">
                                                    <%--<HeaderStyle Width="70px" HorizontalAlign="Right" />
                                                    <ItemStyle Width="70px" HorizontalAlign="Right" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="modified_on" AllowFiltering="false" DataType="System.DateTime" DataFormatString="{0:d/M/yyyy}"
                                                    HeaderText="Approval Date" UniqueName="modified_on">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="additionsforyear" AllowFiltering="false" DataType="System.string"
                                                    HeaderText="Additions For Year" SortExpression="additionsforyear"
                                                    UniqueName="additionsforyear">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="optionselection" AllowFiltering="false" Visible="true"
                                                    HeaderText="Type" UniqueName="optionselection">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="RecStatus" AllowFiltering="false" Visible="true"
                                                    HeaderText="Status" UniqueName="RecStatus">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="basis_arriving_payment" AllowFiltering="false" DataType="System.string"
                                                    UniqueName="basis_arriving_payment" HeaderText="Basis" Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="service_length" AllowFiltering="false" DataType="System.string"
                                                    UniqueName="service_length" HeaderText="Service" Display="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="iras_approval" AllowFiltering="false" DataType="System.string"
                                                    UniqueName="iras_approval" HeaderText="IRAS Appr" Display="False">
                                                </radG:GridBoundColumn>
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ButtonType="ImageButton"
                                                    ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="addition.ascx" EditFormType="WebUserControl">
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add New Payroll Addition" />
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                        </ClientSettings>

                                    </radG:RadGrid>





                            <%--        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppay_add"
                SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="RadComboBoxPrjEmp" Name="empcode" PropertyName="SelectedValue"
                        Type="Int32" />
                    <asp:ControlParameter ControlID="DropDownList1" Name="empmonth" PropertyName="SelectedValue"
                        Type="Int32" />
                    <asp:ControlParameter ControlID="cmbYear" Name="empyear" PropertyName="SelectedValue"
                        Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>--%>
                            <input type="hidden" runat="server" id="txtRadId" />
                            <input type="hidden" runat="server" id="txtLumSum" />

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
        $(imgbtnfetch).click(function () {

            if ($(RadComboBoxPrjEmp_Input).val() == "Select Employee") {
                WarningNotification("Select Employee!!");
                $(RadComboBoxPrjEmp_Input).focus();
                event.preventDefault();
                return false;
            }
        });

        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Addition?", _id, "Confirm Delete", "Delete");
        });


        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RAD_SPLITTER_RadSplitter1 tbody tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }

    </script>
</body>
</html>
