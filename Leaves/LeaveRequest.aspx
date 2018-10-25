<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveRequest.aspx.cs" Inherits="SMEPayroll.Leaves.LeaveRequest"
    EnableViewState="true" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="GridToolBar" Src="~/Frames/GridToolBarSmall.ascx" %>
<%@ Register TagPrefix="uc3" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />
    <link rel="stylesheet" href="../Style/metronic/bs-switches.css" type="text/css" />
    <%--      <style type="text/css">
        .modal-body .form .form-body, .modal-body .portlet-form .form-body {
            padding: 0px 20px !important;
        }

        .modal-body .form-group:last-child {
            /*margin-bottom: 0!important;*/
        }

        .modal-footer .mt-checkbox.mt-checkbox-outline > span {
            border-color: #999 !important;
        }

        .ui-dialog {
            top: 10% !important;
            left: 40% !important;
            width: 346px !important;
        }

        .ui-widget-header, .ui-state-default, .ui-widget-content .ui-state-default,
        .ui-widget-header .ui-state-default {
            color: #FFF !important;
            border: none !important;
        }

        .ui-widget-content {
            border-color: #ddd !important;
        }



        .ui-state-default .ui-icon {
            background: none !important;
            text-indent: unset !important;
        }

            .ui-state-default .ui-icon:before {
                background: none !important;
                font: normal normal normal 14px/1 FontAwesome;
                content: "\f00d";
                color: #fff;
            }

        .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset {
            float: none !important;
            text-align: center !important;
        }

        .ui-dialog .ui-dialog-buttonpane {
            padding: .3em 1em .5em 1em !important;
        }
        .ui-icon-closethick{
            display:none;
        }
    </style>--%>
    
        </style>
    <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

        <script language="javascript" type="text/javascript">
            function checkChanged() {
              
                 var bEnableHalfDay = document.form1.chkHalfDayLeave.checked;
                 
                 var daysgrid = document.getElementById('<%= RadGrid_days.ClientID %>');
               
                if (bEnableHalfDay == true)
                {
                  //  alert(bEnableHalfDay);
                    //daysbtn.style.display = "block";
                    
                    var rval=ValidateForm_showdays();
                    //alert(rval);
                    daysgrid.style.display = "block";
                    // SMEPayroll.Leaves.LeaveRequest.BindLeavegrid();
                 }
                else {

                    daysgrid.style.display = "none";
                }
            }

            function ValidateForm_showdays() {
                

                var applyleaveyear = document.getElementById('cmbLeaveYear').value;
                var applyleaveon = document.getElementById('rdGetLeaveOnDated').value;
                var strmsg = '';
                if (document.getElementById('drpname').value == '0')
                    strmsg = strmsg + ' Please Select Employee. <br>';
                if (document.getElementById('drpleave').value == '0')
                    strmsg = strmsg + ' Please Select Leave Type. <br>';

                if (!document.getElementById('RadDatePicker1').value)
                    strmsg = strmsg + ' Please Enter From Date.  <br>';

                if (!document.getElementById('RadDatePicker2').value)
                    strmsg = strmsg + ' Please Enter To Date.  <br>';
                if (document.getElementById('RadDatePicker1').value > document.getElementById('RadDatePicker2').value)
                    strmsg = strmsg + ' From Date cannot be greater than To Date. <br>';

                if (strmsg.length > 0) {
                    WarningNotification(strmsg);
                    return false;
                }
              
                if (!document.getElementById('RadDatePicker1').value || !document.getElementById('RadDatePicker2').value || document.getElementById('drpleave').value == '-select-')
                    return false;
                else {
                 
                    var stDate = document.getElementById('RadDatePicker1').value;
                    var enDate = document.getElementById('RadDatePicker2').value;

                    if (stDate > enDate) {
                        WarningNotification('From Date cannot be greater than To Date.');
                        return false;
                    }
                    else {
                      
                        if (stDate.substring(0, 4) != enDate.substring(0, 4)) {
                            WarningNotification('Leave applied should fall with in same year.');
                            return false;
                        }
                       
                        var username = document.getElementById("drpname").value;
                        var leaveType = document.getElementById('drpleave').value;
                        var bEnableHalfDay = document.form1.chkHalfDayLeave.checked;
                        //var timesession = document.getElementById('ddltime').value;
                        //alert('enter');
                        //if (bEnableHalfDay == true) {
                        //    if (stDate != enDate) {
                        //        WarningNotification('Half Day can be only selected for a single/one day.');
                        //        return false;
                        //    }
                       // }

                    }

                }
          
                return true;
            }

            function ValidateForm() {
                var applyleaveon = document.getElementById('rdGetLeaveOnDated').value;
                var applyleaveyear = document.getElementById('cmbLeaveYear').value;
                var strmsg = '';
                if (document.getElementById('drpname').value == '0')
                    strmsg = strmsg + ' Please Select Employee. <br>';
                if (document.getElementById('drpleave').value == '0')
                    strmsg = strmsg + ' Please Select Leave Type. <br>';

                if (!document.getElementById('RadDatePicker1').value)
                    strmsg = strmsg + ' Please Enter From Date. <br>';

                if (!document.getElementById('RadDatePicker2').value)
                    strmsg = strmsg + ' Please Enter To Date. <br>';
                if (document.getElementById('RadDatePicker1').value > document.getElementById('RadDatePicker2').value)
                    strmsg = strmsg + ' From Date cannot be greater than To Date. <br>';



                if (strmsg.length > 0) {
                    WarningNotification(strmsg);
                    return false;
                }
               
                if (!document.getElementById('RadDatePicker1').value || !document.getElementById('RadDatePicker2').value || document.getElementById('drpleave').value == '-select-')
                    return false;
                else {
                   
                    var stDate = document.getElementById('RadDatePicker1').value;
                    var enDate = document.getElementById('RadDatePicker2').value;

                    if (stDate > enDate) {
                        WarningNotification('From Date cannot be greater than To Date.');
                        return false;
                    }
                    else {
                    
                        if (stDate.substring(0, 4) != enDate.substring(0, 4)) {
                            WarningNotification('Leave applied should fall with in same year.');
                            return false;
                        }

                        var username = document.getElementById("drpname").value;
                        var leaveType = document.getElementById('drpleave').value;
                       // var bEnableHalfDay = document.form1.chkHalfDayLeave.checked;
                       // var timesession = document.getElementById('ddltime').value;
                       
                        
                        //if (bEnableHalfDay == true) {

                        //    if (stDate != enDate) {
                        //        WarningNotification('Half Day can be only selected for a single/one day.');
                        //        return false;
                        //    }
                           
                        //}
                       
                        var timesession=null;
                        var bEnableHalfDay = null;
                        
                        //alert($('chkHalfDayLeave').css('display') == 'none');
                       // alert($('#chkHalfDayLeave').is(':visible'));
                       // alert($('#ddltime').is(':visible'));
                      //  alert(document.getElementById('chkHalfDayLeave').style.display);
                        if ($('#chkHalfDayLeave').is(':visible'))
                        {
                           
                             bEnableHalfDay = document.form1.chkHalfDayLeave.checked;
                           // bEnableHalfDay = $('chkHalfDayLeave').is(':visible');
                             timesession = 'NA';
                            
                         }
                        else
                        {
                           
                            timesession = document.getElementById('ddltime').value;
                            bEnableHalfDay = false;
                           // alert("else");
                        }
                        
                        //new
                        //SMEPayroll.Leaves.LeaveRequest.InsertInLeave_days();
                        
                        var res = SMEPayroll.Leaves.LeaveRequest.getLeavesValidity(stDate, enDate, leaveType, bEnableHalfDay, applyleaveyear, applyleaveon, timesession, username);
                        var resVal = res.value;
                        res.value = null;
                        var resValAr = resVal.split(',');
                        var sMsg = '';
                        var ddlname = document.getElementById("drpname");
                        var ddlnametxt = ddlname.options[ddlname.selectedIndex].text;

                        var ddlleave = document.getElementById("drpleave");
                        var ddlleavetxt = ddlleave.options[ddlleave.selectedIndex].text;

                        if (resValAr[0] != 'yes') {
                            var lblleave = document.getElementById('lblLeaveText');

                            if (resValAr[1] == '')
                            { WarningNotification('There is/are no leave(s) for selected date(s).'); return false; }

                            if (resValAr[1] == '-100')
                            { WarningNotification('Employee cannot apply leave before Joining date.'); return false; }

                            if (resValAr[1] == '-101')
                            { WarningNotification('Try other dates, leave already applied for this period.'); return false; }

                            if (resValAr[1] == '-102')
                            { WarningNotification('Cannot apply unpaid leave for the Payroll processed, action is not allowed.'); return false; }

                            if (resValAr[1] == '-103')
                            { WarningNotification('Apply Leave Between From ' + lblleave.innerHTML); return false; }

                            if (resValAr[1] == '-104')
                            { WarningNotification('Cannot apply Leave Prior to 24 months.'); return false; }

                            if (resValAr[1] == '-105') {
                                var sql = 'Leave has not been trasnfered in next year.<br/> Or<br/>';
                                sql = sql + 'Leave cannot be applied on these future date.';
                                WarningNotification(sql);
                                return false;
                            }
                            if (resValAr[1] == '-106') {
                                var sql = 'Employee has been already assigned with Project between this date range \n';
                                sql = sql + 'Leave cannot be applied on these future date.';
                                WarningNotification(sql);
                                return false;
                            }
                            if (parseFloat(resValAr[2]) > 0) {
                                sMsg = '\nThere is ' + resValAr[2] + ' Public Holidays in the selected date range.';
                            }
                            //Added by Sandi on 31/3/2014
                            var chk = SMEPayroll.Leaves.LeaveRequest.LeaveAheadCheck(stDate);
                            var chkvalue = chk.value;

                            if (chkvalue == "fail") {
                                var ldays = SMEPayroll.Leaves.LeaveRequest.GetLeaveDay();
                                var leavedays = ldays.value;

                                var sql = "Company Policy does not allow applying leave (" + leavedays + ") days ahead. Please re-apply with different dates.";
                                WarningNotification(sql);
                                return false;
                            }
                                //End Added
                            else {
                                var dblpaidleave = 0;

                                dblpaidleave = parseFloat(resValAr[1]) - parseFloat(resValAr[0]);


                                if (dblpaidleave > 0) {
                                    sMsg = "You are applying for " + ddlnametxt + "'s " + ddlleavetxt + " of " + resValAr[1] + " Days. \nThere will be " + dblpaidleave + " PAID Leave & " + resValAr[0] + " UN-PAID leaves." + sMsg + "\n";
                                    sMsg = sMsg + "Balance Leave Available will be: " + resValAr[3] + " Days. \nDo you want to proceed?";
                                }
                                else {
                                    sMsg = "You are applying for " + ddlnametxt + "'s " + ddlleavetxt + " of " + resValAr[1] + " Days. \nThere will be " + resValAr[0] + " UN-PAID leave." + sMsg + "<br/>";
                                    sMsg = sMsg + "Balance Leave Available will be: " + resValAr[3] + " Days. <br/>Do you want to proceed?";
                                }
                                GetConfirmation(sMsg, 'imgbtnsave');


                                //var bAnswer = confirm(sMsg);
                                //if (bAnswer)
                                //    return true;
                                //else
                                //    return false;
                            }
                        }
                        else {
                            if (parseInt(resValAr[2]) > 0) {
                                sMsg = '\nThere is ' + resValAr[2] + ' Public Holidays in the selected date range.';
                            }

                            if (resValAr[1] == '') {
                                WarningNotification('There are no leave in the selected date range..\n' + resValAr[1]);

                                return false;
                            }
                            else {
                                //Added by Sandi on 31/3/2014
                                var chk = SMEPayroll.Leaves.LeaveRequest.LeaveAheadCheck(stDate);
                                var chkvalue = chk.value;

                                if (chkvalue == "fail") {
                                    var ldays = SMEPayroll.Leaves.LeaveRequest.GetLeaveDay(stDate);
                                    var leavedays = ldays.value;

                                    var sql = "Company Policy does not allow applying leave (" + leavedays + ") days ahead. Please re-apply with different dates.";
                                    WarningNotification(sql);
                                    return false;
                                }
                                    //End Added
                                else {
                                    sMsg = "You are applying for " + ddlnametxt + "'s " + ddlleavetxt + " of " + resValAr[1] + " PAID Leave." + sMsg + "<br/>";
                                    sMsg = sMsg + "Balance Leave Available will be: " + resValAr[3] + " Days. <br/>Do you want to proceed?";
                                    GetConfirmation(sMsg, 'imgbtnsave');

                                    //var bAnswer = confirm(sMsg);
                                    //if (bAnswer)
                                    //    return true;
                                    //else
                                    //    return false;
                                }
                            }
                        }
                    }
                }
            }
    </script>
        
    </radG:RadCodeBlock>
    <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

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
                        <li>Leave Application Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Apply Leave</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Leave Application Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">


                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>



                            <radG:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="all"
                                Skin="Outlook"></radG:RadFormDecorator>
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    //                    function RowDblClick(sender, eventArgs)
                                    //                    {
                                    //                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    //                    }
                                </script>

                            </radG:RadCodeBlock>


                            <div class="row form-group-label-block">
                                <div class="col-md-8 col-big-6">
                                    <div class="row">
                                        <div class="col-sm-3 col-big-2">
                                            <div class="form-group">
                                                <label>Apply Leave</label>
                                                <asp:DropDownList ID="cmbLeaveYear" CssClass="trstandtop form-control input-sm input-small" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbLeaveYear_SelectedIndexChanged">
                                                    <%--  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>--%>
                                                    <%--<asp:ListItem Value="2007">2007</asp:ListItem>
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
                                                        <asp:ListItem Value="2018">2018</asp:ListItem>--%>
                                                    <%-- Added By Jammu Office--%>

                                                    <%-- Added By Jammu Office ends--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label>Employee Name</label>
                                                <asp:DropDownList ID="drpname" CssClass="trstandtop form-control input-sm input-medium" AutoPostBack="true" runat="server"
                                                    OnSelectedIndexChanged="drpname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Apply Leave Type</label>
                                                <asp:DropDownList CssClass="trstandtop form-control input-sm input-medium" ID="drpleave" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Leave From</label>
                                                <radCln:RadDatePicker ID="RadDatePicker1" runat="server" CssClass="trstandtop input-small" Calendar-ShowRowHeaders="false"
                                                    DateInput-Enabled="true" OnSelectedDateChanged="DateValueChanged" AutoPostBack="true">
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
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label>Leave To</label>
                                                <radCln:RadDatePicker CssClass="trstandtop input-small" Calendar-ShowRowHeaders="false" ID="RadDatePicker2"
                                                    runat="server" DateInput-Enabled="true" OnSelectedDateChanged="DateValueChanged" AutoPostBack="true">
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
                                        <div class="col-sm-4">
                                            <div class="form-group inline-switch">
                                                <label class="block">Apply Half Day Leave</label>

                                                <asp:DropDownList ID="ddltime" CssClass="trstandtop form-control input-sm input-xsmall inline-block" runat="server" >
                                                     <asp:ListItem Value="NA" Selected="True">-select-</asp:ListItem>
                                                    <asp:ListItem Value="AM">AM</asp:ListItem>
                                                    <asp:ListItem Value="PM">PM</asp:ListItem>
                                                </asp:DropDownList>
                                               <%-- <div class="btn-group" style="margin :0px;padding :0px">
                                                <button id="FD" type="button" class="btn btn-info btn-sm" style="margin :0px;" onclick="btncall('FD');" >FD</button>
                                                <button id="AM" type="button" class="btn btn-default btn-sm" style="margin :0px;" onclick="btncall('AM');">AM</button>
                                                <button id="PM" type="button" class="btn btn-default btn-sm" style="margin :0px;" onclick="btncall('PM');">PM</button>
                                                </div>--%>
                                                <span class="switch">
                                                    <input id="chkHalfDayLeave" runat="server" class="trstandbottom margin-left-5 switch-toggle switch-toggle-round switch-toggle-round" type="checkbox" visible ="false" onclick="checkChanged()" >
                                                    <label for="chkHalfDayLeave"></label>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <label>Apply Leave On</label>
                                                <radCln:RadDatePicker CssClass="trstandtop input-small" AutoPostBack="true" Calendar-ShowRowHeaders="false"
                                                    ID="rdGetLeaveOnDated" runat="server" DateInput-Enabled="true"
                                                    OnSelectedDateChanged="rdGetLeaveOnDated_SelectedDateChanged">
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
                                        <div class="col-sm-5">
                                            <div class="form-group">
                                                <label>Upload Document</label>
                                                <radG:RadUpload ID="RadUpload1" CssClass="apply-leave" InitialFileInputsCount="1" runat="server" ControlObjectsVisibility="ClearButtons"
                                                    MaxFileInputsCount="1" OverwriteExistingFiles="True" Width="350px" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Remarks</label>
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control max-character custom-maxlength" Font-Names="Tahoma" Font-Size="11px" TextMode="MultiLine" data-maxlength="250"
                                                    Wrap="true" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-big-6">

                                    <div class="panel-group accordion accordion-note no-margin" id="accordion-moreInfo">
                                        <div class="panel panel-default shadow-none">
                                            
                                            <div id="accordion-moreInfo-1" class="panel-collapse collapse">
                                                <div class="panel-body border-top-none no-padding">
                                                    <div class="portlet light">
                                                        <div class="portlet-body">
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Approver</label>
                                                                        <asp:Label ID="lblsupervisor" CssClass="trstandtop" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Joining Date</label>
                                                                        <asp:Label ID="lblJoinDate" CssClass="trstandtop" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Confirm Date</label>
                                                                        <asp:Label ID="lblConfirm" CssClass="trstandtop" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Employee Group</label>
                                                                        <asp:Label ID="lblempgroup" CssClass="trstandtop" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Leave Model</label>
                                                                        <asp:Label ID="lblLeaveModel" runat="server" Text=""></asp:Label><br />
                                                                        <asp:Label ID="lblLeaveText" runat="server" Text=""></asp:Label>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-4">
                                                                    <div class="form-group">
                                                                        <label>Work Days in Week</label>
                                                                        <asp:Label ID="lblWorkDays" CssClass="trstandtop" runat="server"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row margin-top-10 margin-bottom-20">
                                <div class="col-sm-12 text-center">
                                    <asp:Button ID="btnShowdays" runat="server" Text="Select Leave days" CssClass="textfields btn red"
                                        OnClick="btnShowdays_Click" OnClientClick="return ValidateForm_showdays();"  style="display:none;"  />
                                    <asp:Button ID="btnConfirm" runat="server" Text="Confirm leave" CssClass="textfields btn default"
                                        OnClick="lknbtn_Click" Enabled="false" Visible="false"  />

                                    <asp:Button ID="imgbtnsave" runat="server" Text="Submit Leave Request" OnClick="imgbtnsave_Click"
                                        CssClass="textfields btn default hidden" UseSubmitBehavior="false" />

                                    <input type="button" id="fakebutton" value="Submit Leave Request" class="textfields btn default" onclick="ValidateForm()" />

                                    <%--    <asp:Button ID="imgbtnsave" runat="server" Text="Submit Leave Request " OnClick="imgbtnsave_Click"
        Height="22px" Width="150px" CssClass="textfields" OnClientClick=" if ( ValidateForm() ) { this.value='Submitting..'; this.disabled=true; } else { return false;}" UseSubmitBehavior="false" />--%>
                                
                                
                                <%--<div class="panel-heading bg-color-none inline-block">
                                                <h4 class="panel-title">
                                                    <a title="View Additional Information" class="accordion-toggle  collapsed text-right" data-toggle="collapse" data-parent="#accordion3" href="#accordion-moreInfo-1"><i class="icon-info"></i></a>
                                                </h4>
                                            </div>--%>

                                    <input type="button"  value="Additional Information" class="btn default accordion-toggle collapsed" data-toggle="collapse" data-parent="#accordion3" href="#accordion-moreInfo-1" />
                                
                                </div>
                            </div>




                            <div id="portlet_days" class="portlet light portlet-leave" runat="server">
                                <div class="portlet-body">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <radG:RadGrid ID="RadGrid_days" runat="server" GridLines="Both"
                                                Skin="Outlook" AutoGenerateColumns="false" ClientSettings-AllowDragToGroup="true" ShowFooter="false" Width="100%" Style="display:none;">
                                                <MasterTableView AllowAutomaticUpdates="True" AllowSorting="FALSE" AllowFilteringByColumn="false"
                                                    PagerStyle-AlwaysVisible="true" ShowGroupFooter="false" ShowFooter="false" TableLayout="auto" DataKeyNames="startdate,Enddate">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" />
                                                    <Columns>

                                                        <radG:GridBoundColumn DataField="dateRange" HeaderText="Date" SortExpression="dateRange"
                                                            UniqueName="dateRange" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" DataFormatString="{0:d}">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center"
                                                            UniqueName="Fullday" AllowFiltering="false" HeaderText="Fullday">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkFullday" runat="server" AutoPostBack="true" Checked='<%# DataBinder.Eval(Container, "DataItem.Fullday").ToString()!="0"?true:false %>'
                                                                    Visible='<%# DataBinder.Eval(Container, "DataItem.Fullday").ToString()!="0"?true:false %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridTemplateColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center"
                                                            UniqueName="AM" AllowFiltering="false" HeaderText="AM">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkAM" runat="server" AutoPostBack="true"
                                                                    Visible='<%# DataBinder.Eval(Container, "DataItem.AM").ToString()!="0"?true:false %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridTemplateColumn>
                                                        <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center"
                                                            UniqueName="PM" AllowFiltering="false" HeaderText="PM">
                                                            <ItemStyle HorizontalAlign="center" />
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkPM" runat="server" AutoPostBack="true" Visible='<%# DataBinder.Eval(Container, "DataItem.PM").ToString()!="0"?true:false %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridTemplateColumn>


                                                    </Columns>

                                                </MasterTableView>
                                                <ClientSettings Resizing-ClipCellContentOnResize="true">
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                <ExportSettings>
                                                    <Pdf PageHeight="210mm" />
                                                </ExportSettings>
                                                <GroupingSettings ShowUnGroupButton="false" />
                                            </radG:RadGrid>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="RadGrid_days" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                </div>




                <%--       <radG:GridBoundColumn DataField="LeavesAllowed" DataType="System.Double" HeaderText="*CYL"
                                    SortExpression="LeavesAllowed" UniqueName="LeavesAllowed">
                                    <HeaderStyle HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" Width="75px" />
                                </radG:GridBoundColumn>--%>


                <%--Commented By Jaspreet--%>

                <%--<table cellspacing="0" cellpadding="1" border="0" width="99%" class="tbl2">
                                <tr>
                                    <td colspan="2" align="center">--%>


                <div class="portlet light portlet-leave">
                    <div class="portlet-body no-padding">


                        <div class="panel-group accordion accordion-note no-margin" id="accordion3">
                            <div class="panel panel-default shadow-none">
                                <div class="panel-heading bg-color-none">
                                    <h4 class="panel-title">
                                        <a class="accordion-toggle  collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                    </h4>
                                </div>
                                <div id="collapse_3_1" class="panel-collapse collapse">
                                    <div class="panel-body border-top-none no-padding">
                                        <div class="note-custom note">
                                            <ul class="list-inline no-margin">
                                                <li>*CYL=Current Year Leave</li>
                                                <li>*LYCF=Last Year Carry Forward</li>
                                                <li>*CYLE=Current Year Leave Earned</li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <uc2:GridToolBar ID="GridToolBarSmall" runat="server" Width="100%" />

                        <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" GridLines="Both"
                            Skin="Outlook" Width="100%" OnGridExporting="RadGrid1_GridExporting">
                            <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource2" TableLayout="Auto">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <Columns>

                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                        <ItemTemplate>
                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                        </ItemTemplate>
                                        <HeaderStyle Width="30px" />
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </radG:GridTemplateColumn>

                                    <radG:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" UniqueName="Type">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <%--<ItemStyle HorizontalAlign="left" Width="160px" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeavesAllowed" DataType="System.Double" HeaderText="*CYL"
                                        SortExpression="LeavesAllowed" UniqueName="LeavesAllowed">
                                        <HeaderStyle HorizontalAlign="center" Width="75px" />
                                        <ItemStyle HorizontalAlign="right" Width="75px" />
                                    </radG:GridBoundColumn>
                                    <%--  <radG:GridBoundColumn DataField="companyleaveallowed" DataType="System.Double" HeaderText="*CAL"
                                    SortExpression="companyleaveallowed" UniqueName="companyleaveallowed">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle HorizontalAlign="right" Width="75px" />
                                </radG:GridBoundColumn>--%>
                                    <radG:GridBoundColumn DataField="LY_Leaves_Bal" DataType="System.Double" HeaderText="*LYCF"
                                        SortExpression="LY_Leaves_Bal" UniqueName="LY_Leaves_Bal">
                                        <HeaderStyle HorizontalAlign="center" Width="75px" />
                                        <ItemStyle HorizontalAlign="right" Width="75px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeavesEarned" DataType="System.Double" HeaderText="*CYLE"
                                        SortExpression="LeavesEarned" UniqueName="LeavesEarned">
                                        <HeaderStyle HorizontalAlign="center" Width="75px" />
                                        <ItemStyle HorizontalAlign="right" Width="75px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="PaidLeaves" DataType="System.Double" HeaderText="Paid Leave"
                                        SortExpression="PaidLeaves" UniqueName="PaidLeaves">
                                        <HeaderStyle HorizontalAlign="center" Width="100px" />
                                        <ItemStyle HorizontalAlign="right" Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="UnpaidLeaves" DataType="System.Double" HeaderText="Unpaid Leave"
                                        SortExpression="UnpaidLeaves" UniqueName="UnpaidLeaves">
                                        <HeaderStyle HorizontalAlign="center" Width="100px" />
                                        <ItemStyle HorizontalAlign="right" Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="PendingLeaves" DataType="System.Double" HeaderText="Pending Leave"
                                        SortExpression="PendingLeaves" UniqueName="PendingLeaves">
                                        <HeaderStyle HorizontalAlign="center" Width="120px" />
                                        <ItemStyle HorizontalAlign="right" Width="120px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="TotalLeavesTaken" DataType="System.Double" HeaderText="Leave Taken"
                                        SortExpression="TotalLeavesTaken" UniqueName="TotalLeavesTaken">
                                        <HeaderStyle HorizontalAlign="center" Width="100px" />
                                        <ItemStyle HorizontalAlign="right" Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="ActualLeavesAvailable" DataType="System.Double"
                                        HeaderText="Balance Leave" SortExpression="ActualLeavesAvailable" UniqueName="ActualLeavesAvailable">
                                        <HeaderStyle Font-Bold="true" HorizontalAlign="center" Width="120px" />
                                        <ItemStyle Font-Bold="true" HorizontalAlign="right" Width="120px" />
                                    </radG:GridBoundColumn>

                                    <%--   <radG:GridBoundColumn DataField="leavesearned" DataType="System.Double"
                                    HeaderText="Balance Leave" SortExpression="leavesearned" UniqueName="leavesearned">
                                    <HeaderStyle Font-Bold="true" HorizontalAlign="center" />
                                    <ItemStyle Font-Bold="true" HorizontalAlign="right"  Width="90px"/>
                                </radG:GridBoundColumn>--%>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                        </radG:RadGrid>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_GetEmployeeLeavePolicy"
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="drpname" Name="empid" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="cmbLeaveYear" Name="year" PropertyName="SelectedValue"
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="rdGetLeaveOnDated" Name="applydateon" PropertyName="SelectedDate"
                                    Type="datetime" />
                                <asp:Parameter Name="filter" Type="int16" DefaultValue="-1" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <div class="text-center margin-top-10">
                            <asp:Label ID="lblMsg1" runat="server"></asp:Label>
                            <asp:Label ID="lblmsg" CssClass="bodytxt" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

                <%--<tr height="30px">
                                <td colspan="2"></td>
                            </tr>
                            <tr class="bodytxt" height="30px">
                                <td colspan="2">
                                    <tt class="bodytxt">
                                        <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                                        </b>
                                    </tt>
                                </td>
                            </tr>
                            </table>--%>


                <div class="portlet light portlet-leave">
                    <div class="portlet-body">
                        <div class="clearfix">
                            <div class="col-md-12 text-center">
                                <h4>Ad-hoc Leave Information</h4>
                            </div>
                        </div>
                        <div class="clearfix heading-box">
                            <div class="col-md-12">


                                <radG1:RadToolBar Visible="TRUE" ID="DetailRadToolBar" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                    OnButtonClick="DetailRadToolBar_ButtonClick" BorderWidth="0px">
                                    <Items>
                                        <radG1:RadToolBarButton runat="server" CommandName="Print"
                                            Text="Print" ToolTip="Print" CssClass="print-btn">
                                        </radG1:RadToolBarButton>
                                        <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                        <%--<radG1:RadToolBarButton runat="server" Text="">
                                                <ItemTemplate>
                                                    <div>
                                                        <table cellpadding="0" cellspacing="0" border="0">
                                                            <tr>
                                                                <td class="bodytxt" valign="middle" style="height: 30px">&nbsp;Export To:</td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </radG1:RadToolBarButton>--%>
                                        <radG1:RadToolBarButton runat="server" CommandName="Excel"
                                            Text="Excel" ToolTip="Excel" CssClass="excel-btn">
                                        </radG1:RadToolBarButton>
                                        <radG1:RadToolBarButton runat="server" CommandName="Word"
                                            Text="Word" ToolTip="Word" CssClass="word-btn">
                                        </radG1:RadToolBarButton>
                                        <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                                        <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                        <%-- <radG1:RadToolBarButton runat="server" CommandName="Refresh"
                                                        Text="UnGroup" ToolTip="UnGroup" CssClass="ungroup-btn">
                                                    </radG1:RadToolBarButton>--%>
                                        <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                                    Text="Clear Sorting" ToolTip="Clear Sorting">
                                </radG:RadToolBarButton>--%>
                                        <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                        <radG1:RadToolBarButton runat="server" Text="Count">
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
                                        </radG1:RadToolBarButton>
                                        <%--<radG1:RadToolBarButton IsSeparator="true">
                                                    </radG1:RadToolBarButton>--%>
                                        <%--<radG1:RadToolBarButton runat="server"
                                                        Text="Reset to Default" ToolTip="Reset to Default" CssClass="reset-default-btn">
                                                    </radG1:RadToolBarButton>
                                                    <radG1:RadToolBarButton runat="server"
                                                        Text="Save Grid Changes" ToolTip="Save Grid Changes" CssClass="save-changes-btn">
                                                    </radG1:RadToolBarButton>--%>
                                        <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                                    </Items>
                                </radG1:RadToolBar>


                            </div>
                        </div>
                        <radG:RadGrid ID="RadGridReport" AllowMultiRowEdit="True" Visible="true"
                            Skin="Outlook" Width="100%" runat="server" AutoGenerateColumns="false"
                            GridLines="Both" AllowMultiRowSelection="false" AllowFilteringByColumn="false" EnableHeaderContextMenu="true">

                            <MasterTableView DataKeyNames="Date"
                                EditMode="InPlace" AutoGenerateColumns="false"
                                EnableHeaderContextMenu="true">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="left" ForeColor="Navy" />

                                <ItemStyle HorizontalAlign="left" BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" HorizontalAlign="Left" Height="20px" />
                                <Columns>
                                    <radG:GridBoundColumn DataField="Date" HeaderText="Date" UniqueName="Date">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <%--<ItemStyle HorizontalAlign="left" Width="160px" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Addition_Deduction" HeaderText="Addition/Deduction" UniqueName="Addition_Deduction">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <%--<ItemStyle HorizontalAlign="left" Width="160px" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="No Of Days" DataType="System.Double" HeaderText="*No Of Days"
                                        UniqueName="No Of Days">
                                        <HeaderStyle HorizontalAlign="center" Width="150px" />
                                        <ItemStyle HorizontalAlign="right" Width="150px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeaveType" HeaderText="Leave Type" UniqueName="LeaveType">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <%--<ItemStyle HorizontalAlign="left" Width="160px" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Leave After" DataType="System.Double" HeaderText="Leave After"
                                        UniqueName="Leave After">
                                        <HeaderStyle HorizontalAlign="center" Width="150px" />
                                        <ItemStyle HorizontalAlign="right" Width="150px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Remark" HeaderText="Remark" UniqueName="Remark">
                                        <HeaderStyle HorizontalAlign="left" />
                                        <%--<ItemStyle HorizontalAlign="left" Width="160px" />--%>
                                    </radG:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="false">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                <Selecting AllowRowSelect="true" />
                            </ClientSettings>
                        </radG:RadGrid>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </div>






                <%--  by murugan--%>

                 <%--<radA:RadAjaxManager ID="RadAjaxManager1" runat="server"> 
                                <AjaxSettings>
                                    <radA:AjaxSetting AjaxControlID="btnShowdays">
                                        <UpdatedControls>
                                            <radA:AjaxUpdatedControl ControlID="RadGrid_days" />
                                            <radA:AjaxUpdatedControl ControlID="imgbtnsave" />
                                            <radA:AjaxUpdatedControl ControlID="btnConfirm" />
                                        </UpdatedControls>
                                    </radA:AjaxSetting>
                                </AjaxSettings>
                            </radA:RadAjaxManager>--%>



                <%--      <radA:RadAjaxManager ID="RadAjaxManager1" runat="server"  >
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="drpname">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="drpleave" />
                        <radA:AjaxUpdatedControl ControlID="lblsupervisor" />
                        <radA:AjaxUpdatedControl ControlID="lblempgroup" />
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                        <radA:AjaxUpdatedControl ControlID="lblmsg" />
                        <radA:AjaxUpdatedControl ControlID="imgbtnsave" />
                        <radA:AjaxUpdatedControl ControlID="lblJoinDate" />
                        <radA:AjaxUpdatedControl ControlID="lblConfirm" />
                        <radA:AjaxUpdatedControl ControlID="lblWorkDays" />
                        <radA:AjaxUpdatedControl ControlID="lblLeaveModel" />
                        <radA:AjaxUpdatedControl ControlID="lblLeaveText" />
                        <radA:AjaxUpdatedControl ControlID="lblMsg1" />                        
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>--%>

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
    <script type="text/javascript" src="../scripts/metronic/bs-switches.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            //  $(rdGetLeaveOnDated_popupButton).addClass("hidden");
        });
        $(".riTextBox").addClass("form-control");
        $(".RadPicker.RadPicker_Default.trstandtop").removeAttr("style");

        //var _infoicon = '<span class="btn btn-circle btn-icon-only show-info-message btn-info-leave" onclick="showInfoMessage();"><i class="fa fa-info"></i></span>';
        //$('.rgMasterTable thead th:contains(*CYL),.rgMasterTable thead th:contains(*LY)').append(_infoicon);

        //function showInfoMessage() {
        //    InfoNotification("*CYL=Current Year Leave *LYCF=Last Year Carry Forward *CYLE=Current Year Leave Earned");
        //}
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }

        function btncall( a)
        {
            if (a == 'AM') {
                $("#AM").removeClass("btn-default");
                $("#AM").addClass("btn-info");

                $("#FD").removeClass("btn-info");
                $("#FD").addClass("btn-default");

                $("#PM").removeClass("btn-info");
                $("#PM").addClass("btn-default");
            }
            else if (a == 'PM') {
                $("#PM").removeClass("btn-default");
                $("#PM").addClass("btn-info");

                $("#FD").removeClass("btn-info");
                $("#FD").addClass("btn-default");

                $("#AM").removeClass("btn-info");
                $("#AM").addClass("btn-default");
            }
            else if (a == 'FD') {
                $("#FD").removeClass("btn-default");
                $("#FD").addClass("btn-info");

                $("#AM").removeClass("btn-info");
                $("#AM").addClass("btn-default");

                $("#PM").removeClass("btn-info");
                $("#PM").addClass("btn-default");
            }
        }
    </script>
</body>
</html>
