<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetCalculator.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimeSheetCalculator" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>

<%-- Important:- Comment 'AjaxControlToolkit' to datepicker work --%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Import Namespace="SMEPayroll" %>

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
                        <li>Manual Timesheet</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Timesheet Monthly</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manual Timesheet</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" AnimationDuration="1500" runat="server"
                                Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif"
                                    AlternateText="Loading"></asp:Image>
                            </telerik:RadAjaxLoadingPanel>
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                <AjaxSettings>
                                    <%-- <telerik:AjaxSetting AjaxControlID="RadGrid2">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            
                        </UpdatedControls>
            </telerik:AjaxSetting>      --%>
                                    <telerik:AjaxSetting AjaxControlID="btnUpdate">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="btncalculate">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <%--  <telerik:AjaxSetting AjaxControlID="btnSubApprove">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                     
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
                                    <telerik:AjaxSetting AjaxControlID="btnApprove">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>

                                    <telerik:AjaxSetting AjaxControlID="btnDelete">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>

                                    <telerik:AjaxSetting AjaxControlID="btnUnlock">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="Button1">
                                        <UpdatedControls>

                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="btncalculate">
                                        <UpdatedControls>

                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <%-- <telerik:AjaxSetting AjaxControlID="btnCopy">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
                                    --%>
                                    <telerik:AjaxSetting AjaxControlID="imgbtnfetchEmpPrj">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>
                                            <telerik:AjaxUpdatedControl ControlID="Button1" LoadingPanelID="RadAjaxLoadingPanel1"></telerik:AjaxUpdatedControl>


                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
                            </telerik:RadCodeBlock>

                            <script type="text/javascript">
                                debugger;

                                function addnewrowdata() {

                                }



                                function GridCreated() {
                                    Calculate();
                                }


                                function Copy() {
                                    var grid = $find("<%=RadGrid2.ClientID %>");
                                    var masterTableView = grid.get_masterTableView();
                                    var selectedRows = masterTableView.get_selectedItems();
                                    var OutTime = $find("<%=DeftxtOutTime.ClientID %>");
           var InTime = $find("<%=DeftxtInTime.ClientID %>");

                                    //alert(document.getElementById("DeftxtOutTime").value);

                                    for (var j = 0; j < selectedRows.length; j++) {
                                        //alert(1);
                                        var rowInner = selectedRows[j];
                                        var inTimeCell = masterTableView.getCellByColumnUniqueName(rowInner, "InShortTime");
                                        var outTimeCell = masterTableView.getCellByColumnUniqueName(rowInner, "OutShortTime");

                                        //alert(inTimeCell.getElementsByTagName("input")[0].visible);

                                        if (inTimeCell.getElementsByTagName("input")[0].disabled) {
                                            //rowInner.background-color="#FFFFFF !important";
                                        }
                                        else {

                                            inTimeCell.getElementsByTagName("input")[0].value = document.getElementById("DeftxtInTime").value;
                                            outTimeCell.getElementsByTagName("input")[0].value = document.getElementById("DeftxtOutTime").value;
                                        }
                                    }

                                    //Check The RIghts And Disable Enable Buttons

                                    return false;
                                }

                                function Calculate() {
                                    debugger;
                                    var grid = $find("<%=RadGrid2.ClientID %>");
           var masterTableView = grid.get_masterTableView();
           var selectedRows = masterTableView.get_selectedItems();

           var NHTOTAL = 0;
           var OT1TOTAL = 0;
           var OT2TOTAL = 0;
           var TTOTAL = 0;
           var LATOTAL = 0;
           var LATOTALMin = 0;

           var flag1 = 'true';
           var flag2 = 'true';
           var falxintime = "";
           var nhbalance = 540;
           var flexbrkinculde = 0;

           for (var i = 0; i < selectedRows.length; i++) {
               LATOTALMin = 0;

               var row = selectedRows[i];
               var cell = masterTableView.getCellByColumnUniqueName(row, "Err");
               var timeinCell = masterTableView.getCellByColumnUniqueName(row, "InShortTime");
               var timeoutCell = masterTableView.getCellByColumnUniqueName(row, "OutShortTime");
               var Tsdatecell = masterTableView.getCellByColumnUniqueName(row, "Tsdate");
               var inTimeCell = masterTableView.getCellByColumnUniqueName(row, "InTime");
               var BRKNEXTDAYCell = masterTableView.getCellByColumnUniqueName(row, "BRKNEXTDAY");
               //  var FLEXBRKTIMEINCLUDECell   = masterTableView.getCellByColumnUniqueName(row, "FLEXBRKTIMEINCLUDE");
               var BreakTimeAftercell = masterTableView.getCellByColumnUniqueName(row, "BreakTimeAfter");
               var BreakTimeAftOtFlxcell = masterTableView.getCellByColumnUniqueName(row, "BreakTimeAftOtFlx");
               var outTimeCell = masterTableView.getCellByColumnUniqueName(row, "OutTime");

               var imgErrCell = masterTableView.getCellByColumnUniqueName(row, "ErrImg");

               var sdateCell = masterTableView.getCellByColumnUniqueName(row, "SDate");
               var eDateCell = masterTableView.getCellByColumnUniqueName(row, "EDate");

               var earyInTimeCell = masterTableView.getCellByColumnUniqueName(row, "EarlyInBy");
               var lateIntimeCell = masterTableView.getCellByColumnUniqueName(row, "LateInBy");
               var earlyOutTimeCell = masterTableView.getCellByColumnUniqueName(row, "EarlyOutBy");

               var errorNew = masterTableView.getCellByColumnUniqueName(row, "ErrNew");

               rosterType = masterTableView.getCellByColumnUniqueName(row, "RosterType");



               var cell_SD = masterTableView.getCellByColumnUniqueName(row, "SD");
               var cell_ED = masterTableView.getCellByColumnUniqueName(row, "ED");

               var cell_NewR = masterTableView.getCellByColumnUniqueName(row, "NewR");

               var breakTimeNH = masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH1");
               var breakTimeOt = masterTableView.getCellByColumnUniqueName(row, "BreakTimeOt");

               var btNh = masterTableView.getCellByColumnUniqueName(row, "BreakTimeNH");
               var btot = masterTableView.getCellByColumnUniqueName(row, "BKOT1");

               var earlyinTime = earyInTimeCell.innerHTML;
               var BTOTHR_I = masterTableView.getCellByColumnUniqueName(row, "BTOTHR");
               var BTNHHT_I = masterTableView.getCellByColumnUniqueName(row, "BTNHHT");

               var NH = masterTableView.getCellByColumnUniqueName(row, "NH");

               var OT1 = masterTableView.getCellByColumnUniqueName(row, "OT1");
               var OT2 = masterTableView.getCellByColumnUniqueName(row, "OT2");
               var Lateness = masterTableView.getCellByColumnUniqueName(row, "LateHR");
               var TWH = masterTableView.getCellByColumnUniqueName(row, "TWH");

               var NHA = masterTableView.getCellByColumnUniqueName(row, "NHA");
               var OT1A = masterTableView.getCellByColumnUniqueName(row, "OT1A");
               var OT2A = masterTableView.getCellByColumnUniqueName(row, "OT2A");
               var TotalA = masterTableView.getCellByColumnUniqueName(row, "TotalA");

               var NHAT = masterTableView.getCellByColumnUniqueName(row, "NHAT");
               var OT1AT = masterTableView.getCellByColumnUniqueName(row, "OT1AT");
               var OT2AT = masterTableView.getCellByColumnUniqueName(row, "OT2AT");
               var TotalAT = masterTableView.getCellByColumnUniqueName(row, "TotalAT");


               var NHWHMcell = masterTableView.getCellByColumnUniqueName(row, "NHWHM1");



               var cell = masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn");

               var check = cell.children[0];
               if (cell.children[0].children[0] == null) {
                   check = '';
               }
               else {

                   check = cell.children[0].children[0].disabled;

               }



               ////////////  get Data

               var timein = timeinCell.getElementsByTagName("input")[0].value;

               var timeout = timeoutCell.getElementsByTagName("input")[0].value;

               var NHWHM = NHWHMcell.getElementsByTagName("input")[0].value;

               var BRKNEXTDAYvalue = BRKNEXTDAYCell.getElementsByTagName("input")[0].value;

               //       var FLEXBRKTIMEINCLUDEvalue = FLEXBRKTIMEINCLUDECell.getElementsByTagName("input")[0].value; 


               var startdate = cell_SD.getElementsByTagName("SELECT")[0].value;
               var enddate = cell_ED.getElementsByTagName("SELECT")[0].value;

               var earlyin = earyInTimeCell.innerHTML;
               if (earlyin == "&nbsp;") {
                   earlyin = 0;
               }



               var latein = lateIntimeCell.innerHTML;
               if (latein == "&nbsp;") {
                   latein = 0;
               }


               var earlyout = earlyOutTimeCell.innerHTML;
               if (earlyout == "&nbsp;") {
                   earlyout = 0;
               }


               var lateout = "23:59";

               var rostertypevalue = rosterType.innerHTML;

               var New_R = cell_NewR.innerHTML;

               //var brknhmin=btNh.innerText;//chnage on 24_05_2017
               var brknhmin = breakTimeNH.getElementsByTagName("input")[0].value;

               var brkotmin = btot.innerText;

               var brknhstart = BTNHHT_I.innerHTML;

               if (brknhstart == "&nbsp;") {
                   brknhstart = 0;
               }
               var brkotstart = BTOTHR_I.innerHTML;
               if (brkotstart == "&nbsp;") {
                   brkotstart = 0;
               }



               var brktimeafter = BreakTimeAftercell.innerHTML;
               var brktimeafterflx = BreakTimeAftOtFlxcell.innerHTML;


               var intime = inTimeCell.getElementsByTagName("input")[0].value;






               if (rostertypevalue == "FLEXIBLE" && New_R != 'Y') {
                   intime = timein;
                   falxintime = timein;
                   //change on 24-05-2017
                   // nhbalance=(NHWHM-brknhmin);
                   nhbalance = (NHWHM);

               }





               if (rostertypevalue == "FLEXIBLE" && New_R == 'Y') {


                   intime = falxintime;
               }

               var outtime = outTimeCell.innerText;
               var Tstatevalue = Tsdatecell.innerText;




               if (BRKNEXTDAYvalue == "") {
                   BRKNEXTDAYvalue = 0;
               }







               if (brktimeafter == "&nbsp;") {
                   brktimeafter = 0;
               }

               if (brktimeafterflx == "&nbsp;") {
                   brktimeafterflx = 0;
               }





               $.ajax({
                   type: "POST",
                   url: "TimeSheetCalculator.aspx/CalculateWorkHour",
                   data: '{ "NHWHM":"' + NHWHM + '","nhbalance":"' + nhbalance + '","orginaldate":"' + Tstatevalue + '","BRKNEXTDAY":"' + BRKNEXTDAYvalue + '","NewR":"' + New_R + '","timein":"' + timein + '","timeout":"' + timeout + '","startdate":"' + startdate + '","enddate":"' + enddate + '","earlyin":"' + earlyin + '","earlyout":"' + earlyout + '","latein":"' + latein + '","lateout":"' + lateout + '","rostertype":"' + rostertypevalue + '","intime":"' + intime + '","outtime":"' + outtime + '","brknhstart":"' + brknhstart + '","brknhmin":' + brknhmin + ',"brkotstart":"' + brkotstart + '","brkotmin":' + brkotmin + ',"brktimeafter":' + brktimeafter + ',"brktimeafterflx":' + brktimeafterflx + '}',




                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   async: false,
                   cache: false,
                   success: function (response) {

                       if (response == null) {
                           NH.innerText = "-";
                           Lateness.innerText = "-";
                           OT1.innerText = "-";
                           LATOTALMin = "0";
                       }
                       else {


                           NH.innerText = response.NH;
                           nhbalance = (nhbalance - response.NH);

                           OT1.innerText = response.OT;

                           Lateness.getElementsByTagName("input")[0].value = response.LateHR

                           LATOTALMin = response.LateHRMin;
                           //  NHWHREMcell.getElementsByTagName("input")[0].value=response.NHREM; 
                           // NHWHREMcell.innerText=response.NHREM; 

                           // inTimeCell.getElementsByTagName("input")[0].value=responce.FLXROSTERINTIME;
                       }


                   },
                   failure: function (response) {
                       alert(response);
                   }
               });










               var converHrd = masterTableView.getCellByColumnUniqueName(row, "TrHrs");
               /***********Convert Hrs As per settings ******************/


               //                                if(rostertypevalue == "FLEXIBLE" &&  New_R == 'Y')
               //                                    {
               //                                        if( (parseInt(nhbalance) >0) && (parseInt(OT1.innerText) >0))
               //                                        {
               //                                           if((parseInt(OT1.innerText)-parseInt(nhbalance))>0)
               //                                           {
               //                                             OT1.innerText=  parseInt(OT1.innerText)-parseInt(nhbalance);
               //                                           }
               //                                            else
               //                                            {
               //                                             OT1.innerText=0;
               //                                           }
               //                                           
               //                                             NH.innerText = parseInt(NH.innerText) + parseInt(nhbalance);
               //                                        }
               //                                        
               //                                
               //                                    }



               if (converHrd.innerText == 'NH To OT1') {
                   //OT1TOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);                                                                        
                   OT1.innerText = parseInt(NH.innerText) + parseInt(OT1.innerText);

                   NH.innerText = 0;
                   //NHTOTAL=0;
                   TWH.innerText = OT2.innerText;

               }

               if (converHrd.innerText == 'NH To OT2') {
                   //OT2TOTAL = parseInt(NHTOTAL);
                   OT2.innerText = parseInt(NH.innerText);
                   NH.innerText = 0;
                   //NHTOTAL=0; 
                   TWH.innerText = parseInt(OT2.innerText) + parseInt(OT1.innerText);

               }

               if (converHrd.innerText == 'OT1 To NH') {
                   // NHTOTAL     = parseInt(NHTOTAL) + parseInt(OT1TOTAL);   
                   NH.innerText = parseInt(NH.innerText) + parseInt(OT1.innerText);
                   OT1.innerText = 0;
                   //OT1TOTAL=0;
                   TWH.innerText = NH.innerText;
               }

               if (converHrd.innerText == 'OT1 To OT2') {
                   //OT2TOTAL        = parseInt(OT1TOTAL);   
                   OT2.innerText = parseInt(OT1.innerText);
                   OT1.innerText = 0;
                   //OT1TOTAL=0;   
                   TWH.innerText = OT2.innerText;
               }

               if (converHrd.innerText == 'NH+OT1 To NH') {
                   //NHTOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);      
                   NH.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);
                   OT1.innerText = 0;
                   //OT1TOTAL=0;
                   TWH.innerText = NH.innerText;

               }

               if (converHrd.innerText == 'NH+OT1 To OT1') {
                   //                                      OT1TOTAL      = parseInt(NHTOTAL) + parseInt(OT1TOTAL); 
                   //                                      OT1.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);     
                   //                                     
                   //                                      alert("Hello");
                   //                                      OT1.innerText = parseInt(OT1_I.innerText) + parseInt(NH_I.innerText); 
                   //                                     
                   //                                      alert("OT1 ----- "  + OT1.innerText + " NH I " + NH_I.innerText);
                   //                                      NH.innerText=0;
                   //                                      //OT1.innerText=12;
                   //                                      NHTOTAL=0;                                                                      
                   //                                      TWH.innerText =OT1.innerText;


                   //alert("OT 1 " + OT1.innerText + " NH " + NH.innerText);
                   //OT2TOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);      
                   OT1.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);
                   NH.innerText = 0;
                   OT2.innerText = 0;
                   //NHTOTAL=0;
                   //OT1TOTAL=0;
                   TWH.innerText = OT1.innerText;
               }

               if (converHrd.innerText == 'NH+OT1 To OT2') {
                   //OT2TOTAL = parseInt(NHTOTAL) + parseInt(OT1TOTAL);      
                   OT2.innerText = parseInt(OT1.innerText) + parseInt(NH.innerText);
                   NH.innerText = 0;
                   OT1.innerText = 0;
                   //NHTOTAL=0;
                   //OT1TOTAL=0;
                   TWH.innerText = OT2.innerText;

               }

               var NHH = (NH.innerText / 60);
               var NHM = (NH.innerText % 60);

               var OT1H = (OT1.innerText / 60);
               var OT1M = (OT1.innerText % 60);

               var OT2H = (OT2.innerText / 60);
               var OT2M = (OT2.innerText % 60);

               var TotalAH = (TWH.innerText / 60);
               var TotalAM = (TWH.innerText % 60);



               //                              NHAT.getElementsByTagName("input")[0].value      =   NHH     +   '.' + NHM;
               //                              OT1AT.getElementsByTagName("input")[0].value     =   OT1H    +   '.' + OT1M;
               //                              OT2AT.getElementsByTagName("input")[0].value     =   OT2H    +   '.' + OT2H;
               //                              TotalAT.getElementsByTagName("input")[0].value   =   TotalAH +   '.' + TotalAM;

               var objhr = new Object();
               var rnd = masterTableView.getCellByColumnUniqueName(row, "Rounding").innerText;

               //alert(NHH);
               //alert(NHM);

               objhr = rounding(NHH, NHM, rnd);
               NHH = objhr.hr;
               NHM = objhr.minutes;
               if (parseInt(NHM) < parseInt(10)) {
                   NHM = "0" + NHM;
               }



               NHA.innerText = parseInt(NHH) + ' : ' + NHM;        //Math.floor(NH.innerText/60);



               NHTOTAL = parseInt(NHTOTAL) + parseInt((parseInt(NHH) * 60) + parseInt(NHM));

               if (NHA.innerText == '0 : 00') {
                   NHA.innerText = '-';
               }


               objhr = rounding(OT1H, OT1M, rnd);
               OT1H = objhr.hr;
               OT1M = objhr.minutes;
               if (parseInt(OT1M) < parseInt(10)) {
                   OT1M = "0" + OT1M;
               }
               OT1A.innerText = parseInt(OT1H) + ' : ' + OT1M;       //Math.floor(NH.innerText/60);
               OT1TOTAL = parseInt(OT1TOTAL) + parseInt((parseInt(OT1H) * 60) + parseInt(OT1M));

               if (OT1A.innerText == '0 : 00') {
                   OT1A.innerText = '-';
               }

               //                                  if(LateHR.innerText=='0 : 00')
               //                                 {
               //                                    LateHR.innerText='-';
               //                                 }


               objhr = rounding(OT2H, OT2M, rnd);
               OT2H = objhr.hr;
               OT2M = objhr.minutes;

               if (parseInt(OT2M) < parseInt(10)) {
                   OT2M = "0" + OT2M;
               }
               OT2A.innerText = parseInt(OT2H) + ' : ' + OT2M;       //Math.floor(NH.innerText/60);

               OT2TOTAL = parseInt(OT2TOTAL) + parseInt((parseInt(OT2H) * 60) + parseInt(OT2M));

               //alert(OT2A.innerText); 
               if (OT2A.innerText == '0 : 00') {
                   OT2A.innerText = '-';
               }

               objhr = rounding(TotalAH, TotalAM, rnd);
               TotalAH = objhr.hr;
               TotalAM = objhr.minutes;
               if (parseInt(TotalAM) < parseInt(10)) {
                   TotalAM = "0" + TotalAM;
               }
               TotalA.innerText = parseInt(TotalAH) + ' : ' + TotalAM;    //Math.floor(NH.innerText/60);

               if (TotalA.innerText == '0 : 00') {
                   TotalA.innerText = '-';
               }




               //  OT1TOTAL               =   parseInt(OT1TOTAL)      +   parseInt(OT1.innerText);
               //   OT2TOTAL               =   parseInt(OT2TOTAL)      +   parseInt(OT2.innerText);
               TTOTAL = parseInt(TTOTAL) + parseInt(TWH.innerText);
               LATOTAL = parseInt(LATOTAL) + parseInt(LATOTALMin);


               NHAT.getElementsByTagName("input")[0].value = NHA.innerText;
               OT1AT.getElementsByTagName("input")[0].value = OT1A.innerText;
               OT2AT.getElementsByTagName("input")[0].value = OT2A.innerText;
               TotalAT.getElementsByTagName("input")[0].value = TotalA.innerText;

           }

           var masterTable1 = $find("<%= RadGrid2.ClientID%>").get_masterTableView();
    var footer = masterTable1.get_element().getElementsByTagName("TFOOT")[0];

    var NHTOTAL1H = (NHTOTAL / 60);
    var NHTOTAL1M = (NHTOTAL % 60);

    var OT1TOTAL1H = (OT1TOTAL / 60);
    var OT1TOTAL1M = (OT1TOTAL % 60);

    var OT2TOTAL1H = (OT2TOTAL / 60);
    var OT2TOTAL1M = (OT2TOTAL % 60);

    var TTOTALH1 = (TTOTAL / 60);
    var TTOTALM1 = (TTOTAL % 60);


    var LTOTALH1 = (LATOTAL / 60);
    var LTOTALM1 = (LATOTAL % 60);
           //alert( parseInt(TTOTALH1)+ ' : ' + TTOTALM1);

    objhr = rounding(NHTOTAL1H, NHTOTAL1M, 0);
    NHTOTAL1H = objhr.hr;
    NHTOTAL1M = objhr.minutes;

    footer.rows[0].cells[32].innerHTML = parseInt(NHTOTAL1H) + ' : ' + NHTOTAL1M;

    objhr = rounding(OT1TOTAL1H, OT1TOTAL1M, 0);
    OT1TOTAL1H = objhr.hr;
    OT1TOTAL1M = objhr.minutes;


    footer.rows[0].cells[33].innerHTML = parseInt(OT1TOTAL1H) + ' : ' + OT1TOTAL1M;
           //                              
           //                              
           //                              //alert(OT2TOTAL1H);
           //                              //alert(OT2TOTAL1M);
           //                              
    objhr = rounding(OT2TOTAL1H, OT2TOTAL1M, 0);


    OT2TOTAL1H = objhr.hr;
    OT2TOTAL1M = objhr.minutes;


    footer.rows[0].cells[34].innerHTML = parseInt(OT2TOTAL1H) + ' : ' + OT2TOTAL1M;

           //                                     objhr =rounding(LTOTALH1,LTOTALM1,"0.00");
           //                              LTOTALH1 = objhr.hr;
           //                              LTOTALM1 = objhr.minutes; 

    if (LTOTALM1.toString().length == 1) {
        LTOTALM1 = "0" + LTOTALM1;
    }

    footer.rows[0].cells[36].innerHTML = parseInt(LTOTALH1) + ' : ' + LTOTALM1;



    var objbtnUpdate = document.getElementById('btnUpdate');
    var objbtnApprove = document.getElementById('btnApprove');
    var objbtnDelete = document.getElementById('btnDelete');
    var objbtnReject = document.getElementById('btnUnlock');
           //var objbtnSubApprove= document.getElementById('btnSubApprove');

    if (check) {

    }
    else {
        flag1 = 'false';
    }

    if (check) {
        if (flag2 == 'true') {

            flag2 = 'false';
        }
    }

    if (flag1 == 'false') {
        objbtnUpdate.disabled = true;
        objbtnUpdate.disabled = false;
        //   objbtnApprove.disabled  =  true;  
        // objbtnSubApprove.disabled  =  true;   
        //objbtnDelete.disabled  =  true;   
        //objbtnReject.disabled  =  true;                                 
    }
    else {
        objbtnUpdate.disabled = false;

        objbtnApprove.disabled = false;
        // objbtnSubApprove.disabled  =  false;                                     
    }

    if (flag2 == 'false') {
        //objbtnDelete.disabled =  false;
        objbtnReject.disabled = false;
    }
    else {
        //objbtnDelete.disabled =true;                                   
        objbtnReject.disabled = true;
    }

           //Check For Rights 
    var lblRIghts = document.getElementById('lblName');
           //                                if(lblRIghts.innerHTML=="VT")
           //                                {
           //                                     objbtnUpdate.disabled   =  true;                                    
           //                                     objbtnApprove.disabled  =  true; 
           //                                     objbtnReject.disabled =true; 
           //                                     objbtnDelete.disabled =true;      
           //                                }

           //                                 if(lblRIghts.innerHTML=="VTST")
           //                                {
           //                                     if(flag1!='false')
           //                                     {
           //                                        objbtnUpdate.disabled   =  false;
           //                                     }
           //                                     objbtnApprove.disabled  =  true; 
           //                                     objbtnReject.disabled =true; 
           //                                     objbtnDelete.disabled =true;      
           //                                }

           //                                 if(lblRIghts.innerHTML=="VTSTAT")
           //                                {
           //                                    if(flag1!='false')
           //                                    {
           //                                        objbtnUpdate.disabled   =  false;
           //                                     }
           //                                     objbtnApprove.disabled  =  false; 
           //                                     objbtnReject.disabled =false; 
           //                                     objbtnDelete.disabled =false;      
           //                                }

           //                                 if(lblRIghts.innerHTML=="SADMIN")
           //                                 {
           //                                    if(flag1!='false')
           //                                    {
           //                                        objbtnUpdate.disabled   =  false;
           //                                     }
           //                                     objbtnApprove.disabled  =  false; 
           //                                     objbtnReject.disabled =false; 
           //                                     objbtnDelete.disabled =false;   
           //                                 }
           //                    


           ///////////////////

    function rounding(hour, min, round) {
        var oDiff = new Object();
        var factor = 0;

        if (min == 0 || round == 0) {
            oDiff.hr = hour;
            oDiff.minutes = min;

        }
        else if (round == 30) {
            if (min == 30) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min < 30) {
                oDiff.hr = hour;
                oDiff.minutes = 0;
            }

            if (min > 30) {
                oDiff.hr = hour;
                oDiff.minutes = 30;
            }

        }
        else if (round == 15) {

            //alert(min);
            //alert(hour);

            if (min == 15) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 30) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 45) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min > 0 && min < 15) {
                oDiff.hr = hour;
                oDiff.minutes = 0;
            }

            if (min > 15 && min < 30) {
                oDiff.hr = hour;
                oDiff.minutes = 15;
            }

            if (min > 30 && min < 45) {
                oDiff.hr = hour;
                oDiff.minutes = 30;
            }

            if (min > 45 && min < 60) {
                oDiff.hr = hour;
                oDiff.minutes = 45;
            }

        }
        else if (round == 10) {
            if (min == 10) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 20) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 30) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 40) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 50) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min > 0 && min < 10) {
                oDiff.hr = hour;
                oDiff.minutes = 0;
            }

            if (min > 10 && min < 20) {
                oDiff.hr = hour;
                oDiff.minutes = 10;
            }

            if (min > 20 && min < 30) {
                oDiff.hr = hour;
                oDiff.minutes = 20;
            }

            if (min > 30 && min < 40) {
                oDiff.hr = hour;
                oDiff.minutes = 30;
            }

            if (min > 40 && min < 50) {
                oDiff.hr = hour;
                oDiff.minutes = 40;
            }

            if (min > 50 && min < 59) {
                oDiff.hr = hour;
                oDiff.minutes = 50;
            }

        }
        else if (round == 5) {
            if (min == 5) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 10) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 15) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 20) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 25) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }
            if (min == 30) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min == 35) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min == 40) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min == 45) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min == 50) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min == 55) {
                oDiff.hr = hour;
                oDiff.minutes = min;
            }

            if (min > 0 && min < 5) {
                oDiff.hr = hour;
                oDiff.minutes = 0;
            }

            if (min > 5 && min < 10) {
                oDiff.hr = hour;
                oDiff.minutes = 05;
            }

            if (min > 10 && min < 15) {
                oDiff.hr = hour;
                oDiff.minutes = 10;
            }

            if (min > 15 && min < 20) {
                oDiff.hr = hour;
                oDiff.minutes = 15;
            }

            if (min > 20 && min < 25) {
                oDiff.hr = hour;
                oDiff.minutes = 20;
            }

            if (min > 25 && min < 30) {
                oDiff.hr = hour;
                oDiff.minutes = 25;
            }

            if (min > 30 && min < 35) {
                oDiff.hr = hour;
                oDiff.minutes = 30;
            }

            if (min > 35 && min < 40) {
                oDiff.hr = hour;
                oDiff.minutes = 35;
            }

            if (min > 40 && min < 45) {
                oDiff.hr = hour;
                oDiff.minutes = 40;
            }

            if (min > 45 && min < 50) {
                oDiff.hr = hour;
                oDiff.minutes = 45;
            }

            if (min > 50 && min < 55) {
                oDiff.hr = hour;
                oDiff.minutes = 50;
            }

            if (min > 55 && min < 59) {
                oDiff.hr = hour;
                oDiff.minutes = 55;
            }

        }
        else {
            oDiff.hr = hour;
            oDiff.minutes = min;
        }


        return oDiff;
    }


    function timeHourDifference(endDate, startDate) {
        var difference = parseInt(((endDate.getTime() / 1000.0) - (startDate.getTime() / 1000.0)) / 60);
        return difference;
    }

    function timeHourDifferenceCD(earlierDate, laterDate) {
        var nTotalDiff = laterDate.getTime() - earlierDate.getTime();
        //alert(nTotalDiff);
        var oDiff = new Object();

        oDiff.days = Math.floor(nTotalDiff / 1000 / 60 / 60 / 24);
        nTotalDiff -= oDiff.days * 1000 * 60 * 60 * 24;

        //alert(nTotalDiff);
        oDiff.hours = Math.floor(nTotalDiff / 1000.0 / 60.0 / 60.0);
        nTotalDiff -= oDiff.hours * 1000 * 60 * 60;

        oDiff.minutes = Math.floor(nTotalDiff / 1000 / 60);
        nTotalDiff -= oDiff.minutes * 1000 * 60;

        oDiff.seconds = Math.floor(nTotalDiff / 1000);

        //alert(oDiff.hours+  '  .  ' +  oDiff.minutes);                           
        //alert(parseInt((oDiff.hours*60)+ oDiff.minutes ));                           
        return parseInt((oDiff.hours * 60) + oDiff.minutes);
    }


    function ShowDate(ED) {
        var now = new Date();
        now = ED;
        var then = now.getFullYear() + '-' + (now.getMonth() + 1) + '-' + now.getDay();
        then += ' ' + now.getHours() + ':' + now.getMinutes();
        return then;
        //alert(now+'\n'+then); 
    }


    function rectime(sec) {
        var hr = Math.floor(sec / 3600);
        var min = Math.floor((sec - (hr * 3600)) / 60);
        sec -= ((hr * 3600) + (min * 60));
        sec += ''; min += '';
        while (min.length < 2) { min = '0' + min; }
        while (sec.length < 2) { sec = '0' + sec; }
        hr = (hr) ? '.' + hr : '0.';
        return hr + min;
    }

    function timeFromSecs(seconds) {
        var hr = Math.floor(((seconds / 86400) % 1) * 24);
        var min = Math.floor(((seconds / 3600) % 1) * 60);
        while (hr.length < 1) { hr = '0' + hr; }
        while (min.length < 1) { min = '0' + min; }
        if (min.length = 0) {
            min = '00';
        }
        return hr + '.' + min;
        //                    return(                    
        //                    Math.floor(((seconds/86400)%1)*24) +
        //                    Math.floor(((seconds/3600)%1)*60)+'.');
    }








}







                            </script>

                            <script type="text/javascript">

                                function openWindow(value) {

                                    var url = "https://maps.google.com/maps?q=" + value;
                                    window.open(url);
                                }



                                function stopPropagation(e) {
                                    e.cancelBubble = true;

                                    if (e.stopPropagation) {
                                        e.stopPropagation();
                                    }
                                }
                            </script>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>


                            <div class="search-box padding-tb-10">

                                <div class="form-inline col-md-12">

                                    <div class="form-group">
                                        <label>Employee</label>
                                        <radG:RadComboBox ID="RadComboBoxEmpPrj" runat="server" BorderWidth="0px" AutoPostBack="true"
                                            EmptyMessage="Select a Employee" HighlightTemplatedItems="true" EnableLoadOnDemand="true"
                                            OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested" DropDownWidth="275px" Height="200px"
                                            OnSelectedIndexChanged="RadComboBoxEmpPrj_SelectedIndexChanged">
                                            <HeaderTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0" style="width: 260px">
                                                    <tr>
                                                        <td style="width: 80px;">TimeCardNo</td>
                                                        <td style="width: 180px; white-space: nowrap">Emp Name</td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 80px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                        </td>
                                                        <td style="width: 180px;">
                                                            <%# DataBinder.Eval(Container, "Text")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </radG:RadComboBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Start Date</label>
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                            runat="server">
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
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"
                                            runat="server" OnSelectedDateChanged="rdEmpPrjEnd_SelectedDateChanged" AutoPostBack="true">
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
                                        <label>NightShift</label>
                                        <asp:CheckBoxList Visible="True" ID="chkrecords" runat="server" CssClass="bodytxt"
                                            ValidationGroup="val1" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"
                                            CausesValidation="true">
                                            <asp:ListItem Selected="False"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <telerik:RadComboBox ID="cmbLeaveLock" runat="server"  EmptyMessage="-select-"
                                            MarkFirstMatch="true" Font-Names="Tahoma" EnableLoadOnDemand="true">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Complete Lock" Value="1" Selected="true" />
                                                <telerik:RadComboBoxItem Text="Lock only Unpaid Leave" Value="2" />
                                                <telerik:RadComboBoxItem Text="Don't Lock" Value="3" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetchEmpPrj"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group">
                                        <label>In Time</label>
                                        <asp:TextBox Visible="True" Text='' ID="DeftxtInTime" runat="server"
                                            ValidationGroup="vldSum" CssClass="form-control input-sm input-xsmall" />
                                    </div>
                                    <div class="form-group">
                                        <label>Out Time</label>
                                        <asp:TextBox Visible="True" Text='' ID="DeftxtOutTime" runat="server"
                                            ValidationGroup="vldSum" CssClass="form-control input-sm input-xsmall" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCopy"  CssClass="btn btn-sm red" Visible="true" runat="server" Text="Key In/Out Time" OnClientClick="return Copy()" />
                                    </div>
                                    <div class="form-group">
                                        <table  id="table1" runat="server" width="100%"><tr><td>
                                            <asp:Label ID="lblerr" runat="server" Text=""></asp:Label>
                                            <asp:CheckBox ID="autofill" runat="server" /> <label>Auto Change Project</label> 
                                        <asp:Button ID="Button1" CssClass="btn btn-sm red" runat="server" Visible="true" Text="Upload File" OnClick="btnSub_Click" OnClientClick="document.forms[0].target = '_blank';" Enabled="false" />
                                        </td></tr></table>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btncalculate" CssClass="btn btn-sm default" runat="server" Visible="true" OnClientClick="Calculate()"
                                            Text="Calculate" />
                                        <asp:Button ID="btnUpdate" CssClass="btn btn-sm default" runat="server" Text="Save" OnClick="btnUpdate_Click" />

                                        <%if (SMEPayroll.Utility.AllowedAction1(Session["Username"].ToString(), "Submit Timesheet"))
                                            {%>
                                        <asp:Button ID="btnApprove" CssClass="btn btn-sm default" runat="server" OnClick="btnApprove_Click" Text="Submit" />
                                        <%}
                                            else
                                            { %>
                                        <asp:Button ID="Button2" CssClass="btn btn-sm default" runat="server" OnClick="btnApprove_Click" Text="Submit" Enabled="false" />

                                        <%}%>

                                        <asp:Button ID="btnSubApprove" CssClass="btn btn-sm default" runat="server" Visible="false" Enabled="false" Text="Submit/Approve" />

                                        <asp:Button ID="btnUnlock" CssClass="btn btn-sm default" runat="server" Visible="false" Text="Unlock" />

                                        <asp:Button ID="btnDelete" CssClass="btn btn-sm default" runat="server" Visible="true" OnClick="btnDelete_Click"
                                            Text="Delete" />

                                    </div>
                                </div>

                                

                                <div class="clearfix">

                                    <div class="col-md-6">
                                        <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderIn" runat="server" TargetControlID="DeftxtInTime"
                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                            MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                        <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorIn" runat="server" ControlExtender="DefMaskedEditExtenderIn"
                                            ControlToValidate="DeftxtInTime" IsValidEmpty="False" InvalidValueMessage="*"
                                            ValidationGroup="vldSum" Display="Dynamic" Width="10" />
                                    </div>
                                    <div class="col-md-6">
                                        <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderOut" runat="server" TargetControlID="DeftxtOutTime"
                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                            MaskType="Time" AcceptAMPM="false" CultureName="en-US" />

                                        <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorOut" runat="server" ControlExtender="DefMaskedEditExtenderOut"
                                            ControlToValidate="DeftxtOutTime" IsValidEmpty="False" InvalidValueMessage="*"
                                            ValidationGroup="vldSum" Display="Dynamic" />
                                    </div>


                                </div>
                            </div>

                             <table id="tbl1" runat="server" border="0" cellpadding="1" cellspacing="0" style="width:100%">
                                <tr>
                                    <td align="center">
                                        <tt class="bodytxt">
                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Width="80%"></asp:Label></tt>
                                    </td>
                                </tr>
                            </table>


                            <%--<table id="tbl1" runat="server" border="0"  style="width:100%">
                                <tr>
                                    <td align="center">
                                        <tt class="bodytxt">
                                            <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Width="80%"></asp:Label></tt>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">--%>
                                        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                            <script type="text/javascript">
                                                function RowDblClick(sender, eventArgs) {
                                                    //alert(1);
                                                    var e = eventArgs.get_domEvent();
                                                    var rowno = eventArgs.get_itemIndexHierarchical();

                                                    var grid = $find("<%=RadGrid2.ClientID %>");
                                                    var masterTableView = grid.get_masterTableView();

                                                    if (masterTableView != null) {
                                                        var gridItems = masterTableView.get_dataItems();//get_selectedItems(); 
                                                        var i;
                                                        for (i = 0; i < gridItems.length; ++i) {

                                                            if (i == rowno) {


                                                                var row = gridItems[i];

                                                                var inTimeCell = masterTableView.getCellByColumnUniqueName(row, "InShortTime");
                                                                var outTimeCell = masterTableView.getCellByColumnUniqueName(row, "OutShortTime");
                                                                if (inTimeCell.getElementsByTagName("input")[0].disabled) {
                                                                    //rowInner.background-color="#FFFFFF !important";
                                                                }
                                                                else {
                                                                    //alert(1);
                                                                    //inTimeCell.getElementsByTagName("input")[0].value='__:__';
                                                                    //outTimeCell.getElementsByTagName("input")[0].value='__:__';
                                                                }
                                                            }
                                                        }
                                                    }
                                                    Test(1);
                                                }
                                            </script>

                                        </radG:RadCodeBlock>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                            <ContentTemplate>

                                                <radG:RadGrid ID="RadGrid2" CssClass="radGrid-single" runat="server" OnItemDataBound="RadGrid2_ItemDataBound"
                                                    Width="100%" AllowFilteringByColumn="false" AllowSorting="true" Skin="Outlook"
                                                    EnableAjaxSkinRendering="true" MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                                    MasterTableView-AllowAutomaticInserts="False" MasterTableView-AllowMultiColumnSorting="False"
                                                    GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="false"
                                                    ClientSettings-AllowColumnsReorder="false" ClientSettings-ReorderColumnsOnClient="false"
                                                    ClientSettings-AllowDragToGroup="False" ShowFooter="true" ShowStatusBar="true"
                                                    AllowMultiRowSelection="true" PageSize="50" AllowPaging="true">
                                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                                    <SelectedItemStyle CssClass="SelectedRow" />
                                                    <MasterTableView>
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn DataField="RosterType" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="RosterType" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="RosterType" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="RosterType" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <%-- <radG:GridBoundColumn DataField="InTime"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="InTime"
                                    Display="false">
                                     <ItemStyle HorizontalAlign="Center"  Width="0%" />
                                </radG:GridBoundColumn>--%>
                                                            <radG:GridBoundColumn DataField="EarlyInBy" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="EarlyInBy" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="LateInBy" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="LateInBy" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="EarlyInBy" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="EarlyInBy" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="EarlyOutBy" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="EarlyOutBy" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="LateOutBy" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="LateOutBy" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <%--17--%>
                                                            <radG:GridClientSelectColumn HeaderStyle-HorizontalAlign="Center" UniqueName="GridClientSelectColumn">
                                                                <ItemStyle HorizontalAlign="center" Width="2%" />
                                                            </radG:GridClientSelectColumn>
                                                            <%--2--%>
                                                            <radG:GridTemplateColumn HeaderText="Add" HeaderButtonType="PushButton" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Add">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnAdd" runat="server" CommandName="AddNew" ImageUrl="~/Frames/Images/STATUSBAR/Add-ss.png"
                                                                        ToolTip="Add New Record" AlternateText="Add" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridTemplateColumn>
                                                            <%--3--%>
                                                            <radG:GridBoundColumn DataField="SrNo" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="SrNo" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <%--4--%>
                                                            <radG:GridBoundColumn DataField="PK" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="PK" HeaderText="PK" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <%--5--%>
                                                            <radG:GridBoundColumn DataField="Emp_code" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Emp_code" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <%-- <radG:GridTemplateColumn HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" UniqueName="Employee">   
                                  <ItemTemplate >  
                                      <asp:DropDownList  ID="drpEmp" Width="100%"  runat="server" CssClass="bodytxt" >
                                      </asp:DropDownList>                                       
                                  </ItemTemplate>  
                                </radG:GridTemplateColumn>--%>
                                                            <%--6--%>
                                                            <radG:GridTemplateColumn HeaderText="Date|Employee Name" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Employee" ItemStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEmp"  runat="server" CssClass="bodytxt"></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </radG:GridTemplateColumn>
                                                            <%--7--%>
                                                            <radG:GridTemplateColumn HeaderText="Project Name" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Project">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpProject"  runat="server" CssClass="form-control input-sm  input-xsmall"
                                                                         AutoPostBack="true" OnSelectedIndexChanged="drpSubProjectEmp_SelectedIndexChanged">
                                                                    </asp:DropDownList>

                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </radG:GridTemplateColumn>
                                                            <%--8--%>
                                                            <radG:GridTemplateColumn HeaderText="Start Date" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="SD">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpSD" runat="server" CssClass="form-control input-sm  input-xsmall">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </radG:GridTemplateColumn>
                                                            <%--09--%>
                                                            <radG:GridTemplateColumn HeaderText="End Date" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="ED">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="drpED" runat="server" CssClass="form-control input-sm  input-xsmall" >
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </radG:GridTemplateColumn>
                                                            <%--10--%>
                                                            <radG:GridBoundColumn DataField="Time_card_no" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Time_card_no" Display="true" HeaderText="Time Card No">
                                                            </radG:GridBoundColumn>
                                                            <%--11--%>
                                                            <radG:GridBoundColumn DataField="Sub_project_id" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Sub_project_id" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <%--12--%>
                                                            <radG:GridBoundColumn DataField="Tsdate" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="Tsdate" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <%--13--%>
                                                            <radG:GridBoundColumn DataField="Err" Visible="false" HeaderStyle-ForeColor="black"
                                                                HeaderText="Err" HeaderStyle-HorizontalAlign="Center" UniqueName="Err" Display="True">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn HeaderText="" UniqueName="ErrImg" Display="false" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image Style="cursor: hand" ID="Image2" runat="server" BorderStyle="Inset" ImageUrl="~/Frames/Images/STATUSBAR/alert4.png" />
                                                                    <telerik:RadToolTip EnableAjaxSkinRendering="true" ShowCallout="true" Visible="false"
                                                                        Overlay="true" ID="RadToolTip1" Animation="Slide" runat="server" TargetControlID="Image1"
                                                                        Width="500px" Height="200px" MouseTrailing="true" RelativeTo="Element" Position="MiddleRight"
                                                                        EnableShadow="true" ShowDelay="2">
                                                                        Valid Data1
                                                                    </telerik:RadToolTip>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center"  />
                                                            </radG:GridTemplateColumn>
                                                            <%--14--%>
                                                            <radG:GridBoundColumn DataField="Roster_Day" UniqueName="Roster_Day" HeaderText="Day"
                                                                Display="True" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="InTime" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="rsin" HeaderText="ROS_IN" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="OutTime" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="OutTime" HeaderText="ROS_OUT" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ackin" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="ackin" HeaderText="ACT_IN" Display="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="0%" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ackout" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="ackout" HeaderText="ACT_OUT" Display="false">
                                                                <ItemStyle HorizontalAlign="Center"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn DataField="InTime" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="InTime" HeaderText="Roster InTime"
                                                                Display="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InTime")%>' ID="InTimetxt"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <%--15--%>
                                                            <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>' ID="txtInTime"
                                                                            runat="server"
                                                                            CssClass="form-control input-sm input-xsmall" ValidationGroup="vldSum" />
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderIn" runat="server" TargetControlID="txtInTime"
                                                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorIn" runat="server" ControlExtender="MaskedEditExtenderIn"
                                                                            ControlToValidate="txtInTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                                            Display="Dynamic" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn HeaderText="LocationIn" Display="false" UniqueName="lin">
                                                                <ItemTemplate>
                                                                    <%--  <asp:Image style="cursor:hand" ID="locatorurl" runat="server"  onclientclick='openWindow(<%# location_evalue(Eval("location").ToString())%>);'  BorderStyle="Inset" ImageUrl="~/Documents/TimeSheet/MapsMarker.png"/>
                                                                    --%>
                                                                    <img alt="" style="cursor: hand" src="<%# location_image(Eval("location").ToString())%>"
                                                                        onclick='openWindow(<%# location_evalue(Eval("location").ToString())%>);' />
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn HeaderText="In Photo" Display="false" UniqueName="inphoto">
                                                                <ItemTemplate>
                                                                    <asp:Image Style="cursor: hand" ID="inimageid" runat="server" BorderStyle="Inset"
                                                                        ImageUrl='<%# photoImage(DataBinder.Eval(Container,"DataItem.inimage").ToString())%>' />
                                                                    <telerik:RadToolTip ID="RadToolTip2" runat="server" TargetControlID="inimageid" RelativeTo="Element"
                                                                        Position="BottomCenter">
                                                                        <img alt="" src='<%#DataBinder.Eval(Container,"DataItem.inimage")%>' height="150px"
                                                                            width="150px" />
                                                                    </telerik:RadToolTip>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <%--16 --%>
                                                            <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                                                AllowFiltering="false" Groupable="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>' ID="txtOutTime"
                                                                            runat="server"
                                                                            CssClass="form-control input-sm input-xsmall" ValidationGroup="vldSum" />
                                                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderOut" runat="server" TargetControlID="txtOutTime"
                                                                            Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                            MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                                                        <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorOut" runat="server" ControlExtender="MaskedEditExtenderOut"
                                                                            ControlToValidate="txtOutTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                                            Display="Dynamic" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn HeaderText="LocationOut" Display="false" UniqueName="lout">
                                                                <ItemTemplate>
                                                                    <%--  <asp:Image style="cursor:hand" ID="locatorurl" runat="server"  onclientclick='openWindow(<%# location_evalue(Eval("location").ToString())%>);'  BorderStyle="Inset" ImageUrl="~/Documents/TimeSheet/MapsMarker.png"/>
                                                                    --%>
                                                                    <img alt="" style="cursor: hand" src="<%# location_image(Eval("locationout").ToString())%>"
                                                                        onclick='openWindow(<%# location_evalue(Eval("locationout").ToString())%>);' />
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <%--   outphoto--%>
                                                            <radG:GridTemplateColumn HeaderText="Out Photo" Display="false" UniqueName="outphoto">
                                                                <ItemTemplate>
                                                                    <asp:Image Style="cursor: hand" ID="outimageid" runat="server" BorderStyle="Inset"
                                                                        ImageUrl='<%# photoImage(DataBinder.Eval(Container,"DataItem.outimage").ToString())%>' />
                                                                    <telerik:RadToolTip ID="RadToolTip3" runat="server" TargetControlID="outimageid"
                                                                        RelativeTo="Element" Position="BottomCenter">
                                                                        <img alt="" src='<%#DataBinder.Eval(Container,"DataItem.outimage")%>' height="150px"
                                                                            width="150px" />
                                                                    </telerik:RadToolTip>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridBoundColumn DataField="NHA" DataType="System.Decimal" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="NHA" HeaderText="NH" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                                                ItemStyle-Wrap="false" FooterStyle-Wrap="false" Display="True">
                                                                <ItemStyle  HorizontalAlign="center" Font-Size="Small"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="OT1A" DataType="System.Decimal" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="OT1A" HeaderText="OT1" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                                                Display="True">
                                                                <ItemStyle  HorizontalAlign="center" Font-Size="Small"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="OT2A" DataType="System.Decimal" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="OT2A" HeaderText="OT2" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                                                Display="True">
                                                                <ItemStyle  HorizontalAlign="center" Font-Size="Small"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="TotalA" DataType="System.Decimal" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="TotalA" HeaderText="Total" FooterStyle-HorizontalAlign="Center" FooterStyle-Font-Bold="true"
                                                                Display="false">
                                                                <ItemStyle  HorizontalAlign="center" Font-Size="Small"  />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn DataField="LateHR" DataType="System.string" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="LateHR" HeaderText="Late" Display="True">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.LateHR")%>' ID="LateHRtxt"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" Enabled="false" />
                                                                        <%--<asp:Label Text='<%# (DataBinder.Eval(Container,"DataItem.LateHR")=="") ? "0" : DataBinder.Eval(Container,"DataItem.LateHR") %>' ID="LateHRtxt"
                                                        runat="server" Width="38px" /></asp:Label>--%>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn DataField="NHAT" DataType="System.string" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="NHAT" HeaderText="NHAT" Display="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NHAT")%>' ID="NHAT" runat="server"
                                                                            CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn DataField="OT1AT" DataType="System.string" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="OT1AT" HeaderText="OT1T" Display="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OT1AT")%>' ID="OT1AT"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn DataField="OT2AT" DataType="System.string" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="OT2AT" HeaderText="OT2T" Display="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OT2AT")%>' ID="OT2AT"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn DataField="TotalAT" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="TotalAT" HeaderText="TotalT"
                                                                Display="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.TotalAT")%>' ID="TotalAT"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridTemplateColumn>
                                                            <%--18--%>
                                                            <radG:GridBoundColumn DataField="SDate" DataType="System.DateTime" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="SDate" HeaderText="SDate" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <%--19--%>
                                                            <radG:GridBoundColumn DataField="EDate" DataType="System.DateTime" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="EDate" HeaderText="EDate" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <%--20--%>
                                                            <radG:GridBoundColumn DataField="Roster_id" DataType="System.Int64" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="Roster_id" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="EarlyInBy" DataType="System.Int64" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyInBy" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="LateInBy" DataType="System.Int64" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="LateInBy" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="EarlyOutBy" DataType="System.Int64" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="EarlyOutBy" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ErrNew" DataType="System.Int64" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="ErrNew" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="NewR" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center"
                                                                UniqueName="NewR" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <%--NHWHMINUTES DEDUCTED BREAK TIME --%>
                                                            <radG:GridBoundColumn DataField="NHWHM" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="NHWHM" HeaderText="NHWHM" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <%--BREAK TIME --%>
                                                            <radG:GridBoundColumn DataField="BKOT1" HeaderText="BKOT1" HeaderStyle-ForeColor="black"
                                                                DataType="System.Int32" HeaderStyle-HorizontalAlign="Center" UniqueName="BKOT1"
                                                                Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="NH" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="NH" HeaderText="NH" Display="false">
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridBoundColumn>
                                                            <%--BREAK TIME --%>
                                                            <radG:GridBoundColumn DataField="OT1" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="OT1" HeaderText="OT1" Display="false">
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="OT2" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="OT2" HeaderText="OT2" Display="false">
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="TWH" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="TWH" HeaderText="Total" Display="false">
                                                                <ItemStyle  HorizontalAlign="center" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="WHACT" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="WHACT" HeaderText="WHACT" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="BTOTHR" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="BTOTHR" HeaderText="BTOTHR"
                                                                Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="BTNHHT" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="BTNHHT" HeaderText="BTNHHT"
                                                                Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="BreakTimeNH" HeaderStyle-ForeColor="black" DataType="System.Int32"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="BreakTimeNH" HeaderText="BreakTimeLunch"
                                                                Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn Display="true" UniqueName="BreakTimeNH1" HeaderText="BTNH"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.BreakTimeNH")%>' ID="txtBTNH"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <%-- <radG:GridBoundColumn DataField="NHWHREM"   HeaderStyle-ForeColor="black" DataType="System.Int32"  HeaderStyle-HorizontalAlign="Center" UniqueName="NHWHREM" HeaderText="NHWHREM"
                                    Display="True">
                                </radG:GridBoundColumn>
                                                            --%>
                                                            <radG:GridTemplateColumn Display="FALSE" DataField="NHWHREM" UniqueName="NHWHREM"
                                                                HeaderText="NHWHREM" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NHWHREM")%>' ID="txtNHWHREM"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridBoundColumn DataField="EmailSup" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="EmailSup" HeaderText="EmailSup"
                                                                Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="ID" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="ID" HeaderText="ID" Display="False">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="TrHrs" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="TrHrs" HeaderText="TrHrs" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="TrHrs" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="TrHrs" HeaderText="TrHrs" Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="Rounding" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="Rounding" HeaderText="Rounding"
                                                                Display="false">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="BreakTimeAfter" HeaderStyle-ForeColor="black" DataType="System.string"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="BreakTimeAfter" HeaderText="BreakTimeAfter"
                                                                Display="true">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn Display="true" UniqueName="BreakTimeOt" HeaderText="BKOT"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.BKOT1")%>' ID="txtBTOT"
                                                                            AutoPostBack="true" OnTextChanged="ChangeBT" runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn Display="false" UniqueName="LunchBreak" HeaderText="BHNH(Y/N)"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:CheckBox ID="chkLunchBreak" AutoPostBack="true" OnCheckedChanged="ChangeBT"
                                                                            Checked='<%#Convert.ToBoolean(Eval("BNH"))%>' runat="server" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn Display="false" UniqueName="OvertimeBreak" HeaderText="BHOT(Y/N)"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div>
                                                                        <asp:CheckBox ID="chkOTBreak" runat="server" AutoPostBack="true" Checked='<%#Convert.ToBoolean(Eval("BOT"))%>'
                                                                            OnCheckedChanged="ChangeBT" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn Display="true" DataField="Remarks" UniqueName="Remarks"
                                                                HeaderText="Remarks" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                                                Groupable="false" HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div align="justify" class="bodytxt">
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.Remarks")%>' ID="txtReamrks"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" TextMode="MultiLine" Height="30" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn Display="false" DataField="NHWHM" UniqueName="NHWHM1" HeaderText="NHWHM1"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div align="justify" class="bodytxt">
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.NHWHM")%>' ID="txtNHWHM"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridBoundColumn DataField="BreakTimeAftOtFlx" HeaderStyle-ForeColor="black"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="BreakTimeAftOtFlx" HeaderText="BreakTimeAftOtFlx"
                                                                Display="true">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn Display="false" DataField="BRKNEXTDAY" UniqueName="BRKNEXTDAY"
                                                                HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                                                HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                                                <ItemTemplate>
                                                                    <div align="justify" class="bodytxt">
                                                                        <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.BRKNEXTDAY")%>' ID="txtBRKNEXTDAY"
                                                                            runat="server" CssClass="form-control input-sm input-xsmall" />
                                                                    </div>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>



                                                            <%--<radG:GridTemplateColumn Display="true" DataField="FLEXBRKTIMEINCLUDE" UniqueName="FLEXBRKTIMEINCLUDE"
                                            HeaderStyle-HorizontalAlign="Center" AllowFiltering="false" Groupable="false"
                                            HeaderStyle-ForeColor="blue" HeaderStyle-Font-Bold="false">
                                            <ItemTemplate>
                                                <div align="justify" class="bodytxt">
                                                    <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.FLEXBRKTIMEINCLUDE")%>' ID="txtFLEXBRKTIMEINCLUDE"
                                                        runat="server" Width="20px" Height="10px" />
                                                </div>
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>--%>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="true" />
                                                        <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                                            AllowColumnResize="false"></Resizing>
                                                        <ClientEvents OnGridCreated="GridCreated"></ClientEvents>
                                                        <Animation AllowColumnReorderAnimation="true" ColumnReorderAnimationDuration="3" />
                                                        <Selecting AllowRowSelect="true" />
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    <%--</td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td width="0%" height="0%">
                                        
                                    </td>
                                </tr>
                            </table>--%>

                            <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="True" />
                            <asp:Label ID="lblName" runat="server"></asp:Label>

                            <center>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource6" runat="server"></asp:SqlDataSource>
                            </center>
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
    </script>

</body>
</html>
