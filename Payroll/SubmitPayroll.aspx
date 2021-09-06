<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitPayroll.aspx.cs" Inherits="SMEPayroll.Payroll.SubmitPayroll" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_pagefooter" TagName="pagefooter" Src="~/Frames/pagefooter.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Import Namespace="SMEPayroll" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />
    <link rel="stylesheet" href="../Style/metronic/bs-switches.css" type="text/css" />

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




                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Submit Payroll</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Payroll-Dashboard.aspx">Payroll</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Submit Payroll</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Submit Payroll</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">

                            <radG:RadScriptManager ID="RadScriptManager1" runat="server" ScriptMode="release" AsyncPostBackTimeout="1000000">
                                <Scripts>
                                    <asp:ScriptReference Path="Init.js" />
                                </Scripts>


                            </radG:RadScriptManager>

                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

                                <script type="text/javascript">


                                    function GETMONTH(MONTH_NO) {
                                        var month = new Array();
                                        month[0] = "JAN";
                                        month[1] = "FEB";
                                        month[2] = "MAR";
                                        month[3] = "APR";
                                        month[4] = "MAY";
                                        month[5] = "JUN";
                                        month[6] = "JUL";
                                        month[7] = "AUG";
                                        month[8] = "SEP";
                                        month[9] = "OCT";
                                        month[10] = "NOV";
                                        month[11] = "DEC";

                                        var d = new Date();
                                        return month[MONTH_NO];
       
                                    }


                                    function Updatepass() {


                                        var server = '<%=ConfigurationSettings.AppSettings["DB_SERVER"].ToString() %>';
                                        var user_id = '<%=ConfigurationSettings.AppSettings["DB_UID"].ToString() %>';
                                        var database = '<%=ConfigurationSettings.AppSettings["DB_NAME"].ToString() %>';
                                        var password = '<%=ConfigurationSettings.AppSettings["DB_PWD"].ToString() %>';




                                        var year = document.getElementById('<%= cmbYear %>').value;
                                        var month = document.getElementById('<%= cmbMonth %>').value;
         
                                        alert(year);
    
                                        if (year.length != 0 && month.length != 0) {
        
                                            var connection = new ActiveXObject("ADODB.Connection");
                                            var connectionstring = "driver={sql server};server=" + server + ";database=" + database + ";uid=" + user_id + ";password=" + password + "";
                                            connection.Open(connectionstring);
                                            var rs = new ActiveXObject("ADODB.Recordset");
                                            rs.Open("update dbo.fund_year set year_month = '1/MAY/2015'", connection);
                                            alert("Update Password Successfuly");
                                            connection.close();
          
                                        }
                                        else {
                                            alert("fund year Error");
                                        }
 
                                    }

 

                                </script>






                                <%--    
          <script type = "text/javascript">
function ShowCurrentTime() {

    $.ajax({
        type: "POST",
        url: "SubmitPayroll.aspx/GetCurrentTime",
        data: '{name :"kumar"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function(response) {
            alert(response.d);
        }
    });
}
function OnSuccess(response) {
    alert(response);
}
</script>
          
        
        
<script type = "text/javascript">
function ShowDetailReport() {

                                                var month = document.getElementById('cmbMonth').value;
                                                var year = document.getElementById('cmbYear').value;
                                                var deptID = document.getElementById('deptID').value;

    $.ajax({
        type: "POST",
        url: "SubmitPayroll.aspx/btndetail_Click",
        data: { 'monthid':4, 'yearid':2014,'DEPTID':5 },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function(response) {
            alert(response.d);
        }
    });
}
function OnSuccess(response) {
    alert(response);
}
</script>
                                --%>

                                <script type="text/javascript"> 
              
              
           
              
              
                                    function DisableButton() {
                                        document.getElementById("<%=btnsubapprove.ClientID %>").disabled = true;
                                    }
                                    // window.onbeforeunload = DisableButton;
                                    
                                    function OpenModalWindow()  
                                    {  
                                        window.radopen(null,"MYMODALWINDOW");  
                                    }  
                                      
                                    function CloseModalWindow()  
                                    {  
                                        var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");          
                                        win.Close();  
                                    }  
                                    function showreport(e)
                                    {
                                        var month           = document.getElementById('cmbMonth').value;
                                        var year            = document.getElementById('cmbYear').value;
                                        var deptID = document.getElementById('deptID').value;
                                        var res             = SMEPayroll.Payroll.SubmitPayroll.btndetail_Click(month, year,deptID);
                                        window.open(res.value, '_blank', '');
                                        return false;
                                    }
                                    
                                    function showpayroll(e)
                                    {
                                        var month           = document.getElementById('cmbMonth').value;
                                        var year            = document.getElementById('cmbYear').value;
                                        var res             = SMEPayroll.Payroll.SubmitPayroll.btnPayroll_Click(month, year);
                                        window.open(res.value, '_blank', '');
                                        return false;
                                    }
                                    //details report
                                    function showPayrollDetails(e) {
                                        var month = document.getElementById('cmbMonth').value;
                                        var year = document.getElementById('cmbYear').value;
                                        var deptID = document.getElementById('deptID').value;
                                           
                                        var res = SMEPayroll.Payroll.SubmitPayroll.btndetail_Click(month, year,deptID);
                                        window.open(res.value, '_blank', '');
                                        return false;
                                    }


                                    
                                    function ShowInsert(row)
                                    {    
                                        
                                        
                                        //alert(row);
                                        //window.radopen(row, "DetailGrid");
                                        //return false;



                                        $(".loader").css("display", "block");                                      
                                        var url = row;
                                        $('.modal-content').load(url, function (result) {
                                            $('#myModal').modal({ show: true });
                                            $(".loader").css("display", "none");
                                            var documentHeight = $(window).height() - 120;
                                            $(".modal-body").css({ "maxHeight": documentHeight, "overflow": "auto" });
                                        });




                                    }

                                    function ShowInsertForm(row)
                                    {          
                                        //        var month = document.getElementById('cmbMonth').value;                            //        var year = document.getElementById('cmbYear').value;
                                        //        var rowVal =RadGrid1.MasterTableView.GetCellByColumnUniqueName(RadGrid1.MasterTableView.Rows[row], "Emp_Code").innerHTML; 
                                        //        window.radopen("EmployeePayReport.aspx"+"?qsEmpID="+rowVal+"&qsMonth="+month+"&qsYear="+year, "DetailGrid");
                                        return false;
                                    }

                                </script>
                                <script type="text/JavaScript" language="JavaScript">
                                    //http://msdn.microsoft.com/en-us/library/bb386518.aspx
                                    //        function pageLoad() {
                                    //            var manager = Sys.WebForms.PageRequestManager.getInstance();
                                    //           // manager.add_beginRequest(OnBeginRequest);
                                    //            manager.add_endRequest(endRequest);
                                    //        }
                                    //        function OnBeginRequest(sender, args) {
                                    //            var postBackElement = args.get_postBackElement();
                                    //            if (postBackElement.id == 'imgbtnfetch') {
                                    //                 document.getElementById("imgbtnfetch").disabled = true;
                                    //                document.getElementById("lblLoading").innerHTML = "Processing Payroll...";
                                    //            }
                                    //            
                                    //        }

                                    //        function endRequest(sender, args) {
                                    //            alert("sfsdfsfs");
                                    //            Resize();
                                    //alert("unloading");
                                    //            document.getElementById("imgbtnfetch").disabled = false;
                                    //            document.getElementById("lblLoading").innerHTML = "";
                                    //      }
                                    //            //error handling
                                    //             if( args.get_error() )
                                    //             {   
                                    //                //document.getElementById("lblLoading").innerHTML =  args.get_error().description;
                                    //                // args.set_errorHandled( true );
                                    //                document.getElementById("lblLoading").innerHTML ="refresh and try again..";             
                                    //                     
                                    //             }
          
                                </script>
                                <!-- to fix sys is undefines -->
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
                                </script>
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
                                    window.onload = Resize;
                                    function Resize()
                                    {
              
                                        //125=(dedecting other height like menu and footer)
                                        <%--var myHeight = document.body.clientHeight; 
                                        myHeight =myHeight - 130;
                                        document.getElementById('<%= RadGrid1.ClientID %>').style.height=myHeight;--%>
              
                                        //                if( screen.height > 768)
                                        //                {
                                        //                   //"90.7%";
                                        //                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="86%";
                                        //                 }
                                        //                else
                                        //                {
                                        //                   //alert("2");
                                        //                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                        //                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="79%";
                                        //                }
                                    }
            
                                </script>




                                <script type="text/javascript">
     

 

                                    function GETMONTH(MONTH_NO) {
                                        var month = new Array();
                                        month[0] = "JAN";
                                        month[1] = "FEB";
                                        month[2] = "MAR";
                                        month[3] = "APR";
                                        month[4] = "MAY";
                                        month[5] = "JUN";
                                        month[6] = "JUL";
                                        month[7] = "AUG";
                                        month[8] = "SEP";
                                        month[9] = "OCT";
                                        month[10] = "NOV";
                                        month[11] = "DEC";

                                        var d = new Date();
                                        return  month[MONTH_NO];
       
                                    }


                                    function Updatepass() {


                                        // var server = '<%=ConfigurationSettings.AppSettings["DB_SERVER"].ToString() %>';


                                        //         var user_id = '<%=ConfigurationSettings.AppSettings["DB_UID"].ToString() %>';
                                        //         var database = '<%=ConfigurationSettings.AppSettings["DB_NAME"].ToString() %>';
                                        //         var password = '<%=ConfigurationSettings.AppSettings["DB_PWD"].ToString() %>';

                                        var CONSTRING= '<%=SMEPayroll.Constants.CONNECTION_STRING.Replace(@"\",@"\\") %>';

                                        var year = document.getElementById('<%= cmbYear.ClientID  %>').value;
                                        var month_id = document.getElementById('<%= cmbMonth.ClientID  %>');
         
                                        var month=month_id.options[month_id.selectedIndex].text;
       
                                        if (year.length != 0 && month.length != 0) {
        
                                            var connection = new ActiveXObject("ADODB.Connection");
                                            //                var connectionstring = "driver={sql server};server="+server+";database=" + database + ";uid=" + user_id + ";password=" + password + "";
                                            //                alert(connectionstring);
                
                                            connection.Open(CONSTRING);
                                            var rs = new ActiveXObject("ADODB.Recordset");
                                            rs.Open("update dbo.fund_year set year_month = '1/feb/2015' ", CONSTRING);
                                            alert("Update Password Successfuly");
                                            connection.close();
          
                                        }
                                        else {
                                            alert("fund year Error");
                                        }
 
                                    }



                                </script>






                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->


                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <!------------------------------ start ---------------------------------->





                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList ID="cmbMonth" runat="server" CssClass="textfields form-control input-sm">
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
                                        <label>Dept.</label>
                                        <asp:DropDownList OnDataBound="deptID_databound" CssClass="textfields form-control input-sm"
                                            ID="deptID" DataTextField="DeptName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                            runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>

                                    <% if (Session["Country"].ToString() != "383")
                                        { %>
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="paylbl" runat="server" Text="Label">Payment Date :</asp:Label></label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="rdPayDatePicker" runat="server">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                            <ClientEvents />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input type="button" runat="server" id="btndetail" class="textfields btn btn-sm default" onserverclick="btnSummary_Click"
                                            value="Summary Rpt" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input type="button" id="btnPayrollDetail" class="textfields btn btn-sm default"
                                            value="Detail Rpt" onserverclick="btndetail_Click"
                                            runat="server" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnPayroll" CssClass="textfields btn btn-sm default" Text="View All Payslips"
                                            OnClick="btnPayroll_Click" runat="server"></asp:Button>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnReportAll" CssClass="textfields btn btn-sm default" Text="Reports"
                                            OnClick="btnReportAll_Click" runat="server"></asp:Button>
                                    </div>

                                    <% } %>
                                    <div class="form-group">
                                        <asp:Label ID="lblLoading1" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs)
                                    {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                    
                                </script>

                            </radG:RadCodeBlock>
                            <asp:PlaceHolder ID="placeholder1" runat="server"></asp:PlaceHolder>
                            <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" Visible="true" />

                            <radG:RadGrid ID="RadGrid1" AllowPaging="true" PageSize="100" runat="server"
                                AllowMultiRowSelection="true" Skin="Outlook" Width="100%" AutoGenerateColumns="False"
                                AllowFilteringByColumn="True" OnItemDataBound="RadGrid1_ItemDataBound"
                                EnableHeaderContextMenu="true"
                                ItemStyle-Wrap="false"
                                AlternatingItemStyle-Wrap="false"
                                PagerStyle-AlwaysVisible="True"
                                GridLines="Both"
                                AllowSorting="true"
                                OnItemCreated="RadGrid1_ItemCreated"
                                OnGridExporting="RadGrid1_GridExporting"
                                Font-Size="11"
                                Font-Names="Tahoma"
                                HeaderStyle-Wrap="false">
                                <MasterTableView DataKeyNames="FullName,Emp_Code,Basic,Netpay,TotalAdditions,TotalDeductions,TotalAdditionsWONH,paidfullday,paidhalfday,unpaidfullday,unpaidhalfday,
                                            Hourly_Rate,OT1Rate,OT2Rate,NHHrs,OT1Hrs, OT2Hrs,NH,OT1,OT2,Days_Work,DeptName,
                                            OT ,CPFOrdinaryCeil,CPFAdditionNet ,cpfgross ,EmployeeCPFAmt ,EmployerCPFAmt ,CPFAmount,
                                            CPF ,EmpCPFtype ,PRAge ,CPFCeiling ,FundType , FundAmount,UnPaidLeaves,TotalUnPaid,ActualBasic,Pay_Mode,EmployeeGiro,EmployerGiro,GiroBank,FundGrossAmount,GrossWithAddition, CPFCeiling, SDLFundGrossAmount, CMOW,LYOW,CYOW,CPFAWCIL,EST_AWCIL,ACTCIL,AWCM,AWB4CM,AWCM_AWB4CM,AWSUBJCPF,time_card_no,SDF_REQUIRED, PayProcessFH,Daily_Rate,DaysWorkedRate,CPFGross1,WrkgDaysInRoll,ActWrkgDaysSpan,mfc,mvc,cpfordinary,awcm_awb4cm,awcm"
                                    DataSourceID="SqlDataSource1" TableLayout="Auto" PagerStyle-Mode="Advanced">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" Wrap="false" Height="25px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" />
                                    <ItemStyle Height="25px" VerticalAlign="middle" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="Emp_Code" Display="false" HeaderText="Employee Code"
                                            SortExpression="Emp_Code" ReadOnly="True" UniqueName="Emp_Code">
                                            <HeaderStyle Width="114px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="FullName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                            HeaderText="Employee Name" SortExpression="FullName" ReadOnly="True" UniqueName="FullName" ShowFilterIcon="False" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" FilterControlAltText="alphabetsonly">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="OT2Rate" HeaderText="OT2Rate" SortExpression="OT2Rate"
                                            UniqueName="OT2Rate">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="DeptName" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                            HeaderText="Department" SortExpression="DeptName" UniqueName="DeptName" ShowFilterIcon="False" ItemStyle-Wrap="false" FilterControlAltText="cleanstring">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="Hourly_Rate" HeaderText="NHRate"
                                            SortExpression="Hourly_Rate" UniqueName="Hourly_Rate">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="OT1Rate" HeaderText="OT1Rate" SortExpression="OT1Rate"
                                            UniqueName="OT1Rate">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="NHHrs" HeaderText="NHHrs" SortExpression="NHHrs"
                                            UniqueName="NHHrs">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="OT1Hrs" HeaderText="OT1Hrs" SortExpression="OT1Hrs"
                                            UniqueName="OT1Hrs">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="OT2Hrs" HeaderText="OT2Hrs" SortExpression="OT2Hrs"
                                            UniqueName="OT2Hrs">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="Days_Work" HeaderText="Days_Work"
                                            SortExpression="Days_Work" UniqueName="Days_Work">
                                            <HeaderStyle Width="87px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="OT" HeaderText="OT" SortExpression="OT"
                                            UniqueName="OT" >
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CPF" HeaderText="CPF" SortExpression="CPF"
                                            UniqueName="CPF">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CPFOrdinaryCeil" HeaderText="CPFOrdinaryCeil"
                                            SortExpression="CPFOrdinaryCeil" UniqueName="CPFOrdinaryCeil">
                                            <HeaderStyle Width="118px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CPFAdditionNet" HeaderText="CPFAdditionNet"
                                            SortExpression="CPFAdditionNet" UniqueName="CPFAdditionNet">
                                            <HeaderStyle Width="118px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="EmployeeCPFAmt" HeaderText="EmployeeCPFAmt"
                                            SortExpression="EmployeeCPFAmt" UniqueName="EmployeeCPFAmt">
                                            <HeaderStyle Width="130px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="EmployerCPFAmt" HeaderText="EmployerCPFAmt"
                                            SortExpression="EmployerCPFAmt" UniqueName="EmployerCPFAmt">
                                            <HeaderStyle Width="130px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CPFAmount" HeaderText="CPFAmount"
                                            SortExpression="CPFAmount" UniqueName="CPFAmount">
                                            <HeaderStyle Width="90px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="PRAge" HeaderText="PRAge" SortExpression="PRAge"
                                            UniqueName="PRAge">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="FundType" HeaderText="FundType"
                                            SortExpression="FundType" UniqueName="FundType">
                                            <HeaderStyle Width="80px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="FundAmount" HeaderText="FundAmount"
                                            SortExpression="FundAmount" UniqueName="FundAmount">
                                            <HeaderStyle Width="100px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="Basic" HeaderText="Basic Pay"
                                            SortExpression="Basic" UniqueName="Basic" ItemStyle-Wrap="false">
                                            <HeaderStyle Width="76px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="TotalAdditionsWONH" HeaderText="Additions"
                                            SortExpression="TotalAdditionsWONH" UniqueName="TotalAdditionsWONH">
                                            <HeaderStyle Width="75px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" AllowFiltering="False" DataField="TotalAdditions"
                                            HeaderText="Additions" SortExpression="TotalAdditions" UniqueName="TotalAdditions">
                                            <HeaderStyle Width="80px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="NH" HeaderText="NH" SortExpression="NH"
                                            UniqueName="NH">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="OT1" HeaderText="OT1" SortExpression="OT1"
                                            UniqueName="OT1" DataFormatString="{0:#0.00}">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="OT2" HeaderText="OT2" SortExpression="OT2"
                                            UniqueName="OT2" DataFormatString="{0:#0.00}">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="Totaldeductions" HeaderText="Deductions"
                                            SortExpression="Totaldeductions" UniqueName="Totaldeductions">
                                            <HeaderStyle Width="87px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn AllowFiltering="False" DataField="Netpay" HeaderText="Netpay"
                                            SortExpression="Netpay" UniqueName="Netpay">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="UnPaidLeaves" HeaderText="UnPaidLeaves"
                                            SortExpression="UnPaidLeaves" UniqueName="UnPaidLeaves">
                                            <HeaderStyle Width="105px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="TotalUnPaid" HeaderText="TotalUnPaid"
                                            SortExpression="TotalUnPaid" UniqueName="TotalUnPaid">
                                            <HeaderStyle Width="92px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="FundGrossAmount" HeaderText="FundGrossAmount"
                                            SortExpression="FundGrossAmount" UniqueName="FundGrossAmount">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="GrossWithAddition" HeaderText="GrossWithAddition"
                                            SortExpression="GrossWithAddition" UniqueName="GrossWithAddition">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="SDLFundGrossAmount" HeaderText="SDLFundGrossAmount"
                                            SortExpression="SDLFundGrossAmount" UniqueName="SDLFundGrossAmount">
                                            <HeaderStyle Width="160px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="MediumURL" HeaderText="MediumURL"
                                            SortExpression="MediumURL" UniqueName="MediumURL">
                                            <HeaderStyle Width="250px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CMOW" HeaderText="" SortExpression="CMOW"
                                            UniqueName="CMOW">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="LYOW" HeaderText="" SortExpression="LYOW"
                                            UniqueName="LYOW">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CYOW" HeaderText="" SortExpression="CYOW"
                                            UniqueName="CYOW">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CPFAWCIL" HeaderText="" SortExpression="CPFAWCIL"
                                            UniqueName="CPFAWCIL">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="EST_AWCIL" HeaderText="" SortExpression="EST_AWCIL"
                                            UniqueName="EST_AWCIL">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="ACTCIL" HeaderText="" SortExpression="ACTCIL"
                                            UniqueName="ACTCIL">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="AWCM" HeaderText="" SortExpression="AWCM"
                                            UniqueName="AWCM">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="AWB4CM" HeaderText="" SortExpression="AWB4CM"
                                            UniqueName="AWB4CM">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="AWCM_AWB4CM" HeaderText="" SortExpression="AWCM_AWB4CM"
                                            UniqueName="AWCM_AWB4CM">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="AWSUBJCPF" HeaderText="" SortExpression="AWSUBJCPF"
                                            UniqueName="AWSUBJCPF">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="SDF_REQUIRED" HeaderText="" SortExpression="SDF_REQUIRED"
                                            UniqueName="SDF_REQUIRED">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID" HeaderText="Time Card ID"
                                            CurrentFilterFunction="contains" AutoPostBackOnFilter="true" DataField="time_card_no" FilterControlAltText="cleanstring">
                                            <HeaderStyle Width="98px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" UniqueName="PayProcessFH" DataField="PayProcessFH">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="cpfgross" HeaderText="cpfgross" DataField="cpfgross" Display="False" AllowFiltering="false">
                                            <HeaderStyle Width="70px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="CPFGross1" HeaderText="CPFGross1" DataField="CPFGross1" Display="False" AllowFiltering="false">
                                            <HeaderStyle Width="80px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn AllowFiltering="False" HeaderText="Payment Date" UniqueName="Payment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaymentDate" CssClass="form-control input-sm _txtbox " runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="105px" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn AllowFiltering="False" HeaderText="" UniqueName="Image">
                                            <ItemTemplate>
                                                <asp:HyperLink Text="Detail" ID="Image3" runat="server"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>


                                        <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                            <HeaderStyle Width="120px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                            <HeaderStyle Width="120px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                            <HeaderStyle Width="80px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                            <HeaderStyle Width="200px" />
                                        </radG:GridBoundColumn>
                                        <%--<radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false" >
                                            <HeaderStyle Width="110px" />
                                        </radG:GridBoundColumn>--%>
                                        <radG:GridBoundColumn Display="false" UniqueName="WrkgDaysInRoll" HeaderText="WrkgDaysInRoll" DataField="WrkgDaysInRoll" AllowFiltering="false">
                                            <HeaderStyle Width="120px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" UniqueName="ActWrkgDaysSpan" HeaderText="ActWrkgDaysSpan" DataField="ActWrkgDaysSpan" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" UniqueName="mfc" HeaderText="mfc" DataField="mfc" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" UniqueName="mvc" HeaderText="mvc" DataField="mvc" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                         <radG:GridBoundColumn Display="false" UniqueName="cpfordinary" HeaderText="cpfordinary" DataField="cpfordinary" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" UniqueName="awcm_awb4cm" HeaderText="awcm_awb4cm" DataField="awcm_awb4cm" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>
                                        <%--<radG:GridBoundColumn Display="false" UniqueName="wlevy" HeaderText="wlevy" DataField="wlevy" AllowFiltering="false">
                                            <HeaderStyle Width="135px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </radG:GridBoundColumn>--%>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>

                            </radG:RadGrid>
                            <%-- SelectCommand="Sp_DeductionPerOfGross"--%><%--SelectCommand="sp_GeneratePayRollAdv"--%>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_GeneratePayRollAdv"
                                InsertCommand="sp_payroll_add" SelectCommandType="StoredProcedure" InsertCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbMonth" Name="month" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="stdatemonth" SessionField="PayStartDay" Type="Int32" />
                                    <asp:SessionParameter Name="endatemonth" SessionField="PayEndDay" Type="Int32" />
                                    <asp:SessionParameter Name="stdatesubmonth" SessionField="PaySubStartDay" Type="Int32" />
                                    <asp:SessionParameter Name="endatesubmonth" SessionField="PaySubEndDay" Type="Int32" />
                                    <%--  <asp:SessionParameter Name="IsDateCalculation" SessionField="IsDateCalculation" Type="string" />--%>
                                    <asp:ControlParameter ControlID="cmbMonth" Name="monthidintbl" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:ControlParameter ControlID="deptID" Name="DeptId" PropertyName="SelectedValue" Type="string" />
                                </SelectParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Emp_Code" Type="String" />
                                    <asp:Parameter Name="basic_pay" Type="Decimal" />
                                    <asp:Parameter Name="overtime" Type="Decimal" />
                                    <asp:Parameter Name="overtime2" Type="Decimal" />
                                    <asp:Parameter Name="total_additions" Type="Decimal" />
                                    <asp:Parameter Name="total_deductions" Type="Decimal" />
                                    <asp:Parameter Name="status" Type="String" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by DeptName">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>





                            <div class="clearfix padding-tb-10">


                                <div class="col-md-5">


                                    <div class="panel-group accordion accordion-note no-margin" id="accordion3">
                                        <div class="panel panel-default shadow-none">
                                            <div class="panel-heading bg-color-none">
                                                <h4 class="panel-title">
                                                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                                </h4>
                                            </div>
                                            <div id="collapse_3_1" class="panel-collapse collapse in">
                                                <div class="panel-body border-top-none no-padding">
                                                    <div class="note-custom note">
                                                        Make sure that you have settled all the leaves, additions, deductions for above listed employees.Once the payroll is approved, ALL Transactions will be locked for security reasons (For Example: Leaves,Additions,Deductions)
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>





                                </div>
                                <div class="col-md-2 text-right">

                                    <%--<asp:CheckBox ID="chkDateSelection" runat="server" ForeColor="white" CssClass="bodytxt" OnCheckedChanged="chkPayment_CheckedChanged" AutoPostBack="true" />--%>

                                    <div class="inline-switch-S2">                                        
                                        <span class="switch">
                                            <input id="chkDateSelection" name="chkDateSelection" class="trstandbottom margin-left-5 switch-toggle switch-toggle-round switch-toggle-round" runat="server" onclick="javascript: form1.submit();" onserverchange="chkPayment_CheckedChanged" enableviewstate="true" type="checkbox" />
                                            <label for="chkDateSelection"></label>
                                        </span>
                                        <asp:Label ID="lblpaymentdate" runat="server" Text="Payment Date"></asp:Label>
                                    </div>


                                    <%--<label class="mt-checkbox mt-checkbox-outline">
                                                    <input type="checkbox" ID="chkDateSelection" runat="server" ForeColor="white" CssClass="bodytxt" OnCheckedChanged="chkPayment_CheckedChanged" AutoPostBack="true" Text="Payment Date"  />
                                                        <span></span>
                                                    </label>--%>
                                </div>
                                <div class="col-md-5 text-right">
              
                                    <asp:Button ID="btnsubapprove" OnClick="btnsubapprove_click" OnClientClick="DisableButton();"   UseSubmitBehavior="false"  
                                        runat="server" Text="Submit for approval" class="textfields btn red" />
                                </div>

                            </div>


                            <!-------------------- end -------------------------------------->


                            <asp:Label ID="dataexportmessage" runat="server" Visible="false" ForeColor="red"
                                CssClass="bodytxt">No Records to export!</asp:Label>




                            <radW:RadWindowManager ID="RadWindowManager1" runat="server">
                                <Windows>
                                    <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="10px"
                                        Height="740px" Width="960px" Left="20px" Modal="true" />
                                </Windows>
                            </radW:RadWindowManager>
                            <!-- IF GENERAL SOLUTION :- USE sp_GeneratePayRoll -->
                            <!-- IF BIOMETREICS :- USE sp_GeneratePayRoll_TimeSheet -->
                            <!-- IF CLVAVON :- USE [sp_GeneratePayRoll_Clavon] -->
                            <!-- Gap to fill the bottom -->
                            <!-- footer -->




                        </form>


                        <script type="text/javascript">


                            function GETMONTH(MONTH_NO) {
                                var month = new Array();
                                month[0] = "JAN";
                                month[1] = "FEB";
                                month[2] = "MAR";
                                month[3] = "APR";
                                month[4] = "MAY";
                                month[5] = "JUN";
                                month[6] = "JUL";
                                month[7] = "AUG";
                                month[8] = "SEP";
                                month[9] = "OCT";
                                month[10] = "NOV";
                                month[11] = "DEC";

                                var d = new Date();
                                return  month[MONTH_NO];
       
                            }


                            function Updatepass() {


                                var server = '<%=ConfigurationSettings.AppSettings["DB_SERVER"].ToString() %>';
                                var user_id = '<%=ConfigurationSettings.AppSettings["DB_UID"].ToString() %>';
                                var database = '<%=ConfigurationSettings.AppSettings["DB_NAME"].ToString() %>';
                                var password = '<%=ConfigurationSettings.AppSettings["DB_PWD"].ToString() %>';




                                var year = document.getElementById('<%= cmbYear %>').value;
                                var month = document.getElementById('<%= cmbMonth %>').value;
         
                                alert(year);
    
                                if (year.length != 0 && month.length != 0) {
        
                                    var connection = new ActiveXObject("ADODB.Connection");
                                    var connectionstring = "driver={sql server};server=" + server + ";database=" + database + ";uid=" + user_id + ";password=" + password + "";
                                    connection.Open(connectionstring);
                                    var rs = new ActiveXObject("ADODB.Recordset");
                                    rs.Open("update dbo.fund_year set year_month = '1/MAY/2015'", connection);
                                    alert("Update Password Successfuly");
                                    connection.close();
          
                                }
                                else {
                                    alert("fund year Error");
                                }
 
                            }



                        </script>







                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->

        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->


        <div id="myModal" class="modal fade">
            <div class="modal-dialog" style="width: 80%">
                <div class="modal-content">
                </div>
            </div>
        </div>


    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <uc_pagefooter:pagefooter ID="pagefooter" runat="server" />

    <uc_js:bundle_js ID="bundle_js" runat="server" />
    <script type="text/javascript" src="../scripts/metronic/bs-switches.js"></script>
    <script type="text/javascript" src="../Audit/scripts/SaveAuditLog.js"></script>
    <script type="text/javascript"  src="../Audit/scripts/SubmitPayroll.js"></script>
    <!-- Audit -->
   <script type="text/javascript">
       //Submit Payroll method
        $(document).ready(function () {
             var actionModuleRef = "<%= ViewState["AuditActionModuleRef"] %>";
             if (actionModuleRef != null && actionModuleRef != "") {
                 var contents = actionModuleRef.split('||');
                 if ((contents[1].toString().localeCompare("Payroll_module")) == 0) {
                     if (contents[0] == 9 ) {
                         var info = '';
                         info = "Assignment";
                         var feature = "Submit Payroll";
                         Assignment_unAssignment(contents, "Payroll/SubmitPayroll.aspx", info, feature);
                     }else if (contents[0] == 4) {
                         exportNPrint(contents, "Payroll/SubmitPayroll.aspx", tableHeading, "Submit Payroll");
                     }else if (contents[0] == 8) {
                         var feature = "Submit Payroll";
                         reqOnlyForm(contents, "Payroll/SubmitPayroll.aspx", feature);
                     }else if (contents[0] == 12) {
                         var feature = "Submit Payroll";
                         var urlStr =  "Payroll/SubmitPayroll.aspx";
                         var tableHeading ="Summary Report";
                         var info =" Has Viewed Report on Summary Report"
                         Reports(contents, urlStr, tableHeading, feature,info)
                     }
                 }
             }
             else {
                 clearStorage();
                 BeforeSubmitProcess();
            //     localStorage.setItem('previousData', JSON.stringify(currentandPreviousValueGenerator()));// Getting Previous Data
             }
        });
      
   </script>
    
      <!-- Audit -->
    <%--//By jammu Offfice--%>
    <script type="text/javascript">
        $(document).ready(function () {     

            $("#myModal").on('hide.bs.modal', function () {
                $('.modal-content').empty();
            });  

            $(document).on("click", "#btnsubapprove", function () {
                if( !validateformsubmit())
                {
                    return false;
                }
                var _message = "";
                $("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]').each(function () {
                    if ($(this).is(":checked") && $(this).closest('tr').find('._txtbox').val() =="") 
                    {
                        _message = "Payment Date cannot be empty for selected employees";
                    }
                    if (_message != "") {
                        event.preventDefault();
                        WarningNotification(_message);
                        return false;
                    }
                });
            });

            window.onload = function () {
                CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RAD_SPLITTER_RadSplitter1 tbody tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        });
        function validateformsubmit() {
            var _message = "";
            if ($("#chkDateSelection").prop('checked')==false)
                _message = "Select Payment Date.";
            else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
       
    </script>
</body>
</html>
