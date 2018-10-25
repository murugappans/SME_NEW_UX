<%@ Page Language="C#" AutoEventWireup="false" CodeBehind="AddEditEmployee.aspx.cs"
    Inherits="SMEPayroll.employee.AddEditEmployee" %>

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


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />
    <link href="../Style/metronic/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/profile.min.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js"></script>
    <script src="../scripts/jquery-1.6.2.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        // fix for deprecated method in Chrome 37
        if (!window.showModalDialog) {
            window.showModalDialog = function (arg1, arg2, arg3) {

                var w;
                var h;
                var resizable = "no";
                var scroll = "no";
                var status = "no";

                // get the modal specs
                var mdattrs = arg3.split(";");
                for (i = 0; i < mdattrs.length; i++) {
                    var mdattr = mdattrs[i].split(":");

                    var n = mdattr[0];
                    var v = mdattr[1];
                    if (n) { n = n.trim().toLowerCase(); }
                    if (v) { v = v.trim().toLowerCase(); }

                    if (n == "dialogheight") {
                        h = v.replace("px", "");
                    } else if (n == "dialogwidth") {
                        w = v.replace("px", "");
                    } else if (n == "resizable") {
                        resizable = v;
                    } else if (n == "scroll") {
                        scroll = v;
                    } else if (n == "status") {
                        status = v;
                    }
                }

                var left = window.screenX + (window.outerWidth / 2) - (w / 2);
                var top = window.screenY + (window.outerHeight / 2) - (h / 2);
                var targetWin = window.open(arg1, arg1, 'toolbar=no, location=no, directories=no, status=' + status + ', menubar=no, scrollbars=' + scroll + ', resizable=' + resizable + ', copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
                targetWin.focus();
            };
        }
    </script>

    <script type="text/javascript">

        //$(document.getElementById('txtPayRate')).on("keypress keyup blur", function (event) {
        //    //this.value = this.value.replace(/[^0-9\.]/g,'');
        //    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        //    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        //        event.preventDefault();
        //    }
        //});
        function __doPost(hld, h) {

            var theForm1 = document.forms['employeeform'];
            if (!theForm1) {
                theForm1 = document.form1;
            }
            if (!theForm1.onsubmit || (theForm.onsubmit() != false)) {
                theForm1.__EVENTARGUMENT1.value = hld;
                theForm1.__EVENTARGUMENT2.value = h;
                theForm1.submit();

            }
        }


        //single
        function clickfunction(k) {
            if (!window.clicktimer) window.clicktimer = setTimeout(function () {
                __doPost(k.innerHTML, "F");
                window.clicktimer = undefined;
            },
               500);



        }
        //doube
        function dbclickfunction(k) {

            clearTimeout(clicktimer);

            __doPost(k.innerHTML, "H");

            window.clicktimer = undefined;
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
                        <li>Employee Add/Edit Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Employee_Dashboard.aspx">Employee</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="employee.aspx">Employee</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Add/Edit Form</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employee Edit Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-sm-12">

                        <form id="employeeform" runat="server" method="post">

                            <input type="hidden" name="__EVENTARGUMENT1" id="__EVENTARGUMENT1" value="0" />
                            <input type="hidden" name="__EVENTARGUMENT2" id="__EVENTARGUMENT2" value="0" />
                            <radTS:RadScriptManager ID="ScriptManager" runat="server">
                            </radTS:RadScriptManager>
                            <!-- pop up  -->

                            <telerik:RadWindowManager ID="radWindiowMng1" runat="server">
                            </telerik:RadWindowManager>


                            <telerik:RadWindow ID="radWindow1" Skin="Outlook" runat="server" Animation="Fade"
                                AnimationDuration="2900" IconUrl="../frames/Images/Other/CHM.ico" VisibleStatusbar="true"
                                VisibleTitlebar="true" ReloadOnShow="true" AutoSize="false" Top="25" Width="500"
                                Height="400" Left="495" ShowContentDuringLoad="true">
                            </telerik:RadWindow>


                            <%--<div>
                                <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                    border="0" style="table-layout: fixed;" id="gridTable">
                                    <tr>
                                        <td valign="top">
                                            
                                        </td>
                                    </tr>                                   
                                </table>
                            </div>--%>

                            <label id="lblMessage" style="color: Red" class="bodytxt" runat="server" visible="true">
                            </label>

                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function customvalidate(sender, eventArgs) {

                                        var selectedValueID = sender.id.replace("rfvDateOfBirth", "RCB_GridDropDownColumnRelation_ClientState");
                                        var dateId = sender.id.replace("rfvDateOfBirth", "rdDateOfBirth_dateInput_text");
                                        var selectedValue = JSON.parse($("#" + selectedValueID).val());

                                        var datevalue = $("#" + dateId).val();


                                        if (selectedValue['value'] != 2) {
                                            eventArgs.IsValid = true;
                                        }
                                        else if (datevalue) {
                                            eventArgs.IsValid = true;
                                        }
                                        else {
                                            eventArgs.IsValid = false;
                                        }
                                    }
                                </script>
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
                                            var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid4.Skin)) %>';
                                            var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid4.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                            var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                            styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                            var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid4.ClientID %>').get_element()) + "</body></html>";
                                            previewWnd.document.open();
                                            previewWnd.document.write(htmlcontent);
                                            previewWnd.document.close();
                                            previewWnd.print();
                                            previewWnd.close();
                                        }
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->


                            <%-- <div>
                                <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                    border="0" style="table-layout: fixed;" id="Table1">
                                    <tr>
                                        <td valign="top">
                                            
                                        </td>
                                    </tr>                                   
                                </table>
                            </div>--%>


                            <label id="Label2" style="color: Red" class="bodytxt" runat="server" visible="true">
                            </label>

                            <!-- ToolBar -->
                            <radG:RadCodeBlock ID="RadCodeBlock5" runat="server">

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

                                    function PrintRadGrid1(sender, args) {
                                        if (args.get_item().get_text() == 'Print') {

                                            var previewWnd = window.open('about:blank', '', '', false);
                                            var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid5.Skin)) %>';
                                            var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid5.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                            var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                            styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                            var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid5.ClientID %>').get_element()) + "</body></html>";
                                            previewWnd.document.open();
                                            previewWnd.document.write(htmlcontent);
                                            previewWnd.document.close();
                                            previewWnd.print();
                                            previewWnd.close();
                                        }
                                    }

                                </script>

                            </radG:RadCodeBlock>
                            <!-- ToolBar End -->

                            <script type="text/javascript">
                                function OpenWindowTerminate(empcode, compId) {


                                    //                var oWnd = $find("<= radWindow1.ClientID >");            
                                    //                alert(oWnd);
                                    //                //GetNewUrl();
                                    //                var url = "/frames/help.aspx?Pagename=" + 'about';
                                    //                oWnd.setUrl(url);
                                    //                oWnd.show();

                                    // var url = "Terminate.aspx?emp_code=" + empcode + "&msg=" + Msg + "";
                                    var url = "Terminate.aspx?emp_code=" + empcode;
                                    window.open(url, "myWindow", "status = 1, height = 700, width = 700, resizable = 1,scrollbars=1");
                                    return false;

                                }
                            </script>

                            <!-- -->
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<table cellpadding="0" cellspacing="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="5" cellspacing="0" width="100%" border="0">--%>

                            <%--<tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee Edit Form</b></font>
                            </td>
                        </tr>--%>

                            <%--<tr class="search-box">
                                                <td align="left" style="height: 25px">&nbsp;&nbsp;
                                &nbsp;&nbsp;
                                
                                                </td>
                                                <td align="right">
                                                    
                                                </td>
                                                <td style="width: 5%">
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>

                            <div class="search-box clearfix margin-bottom-20 padding-tb-10">
                                <div class="col-sm-6 emp-details">
                                    <asp:Label ID="lblEmp" runat="server" Text="Employee Name:"
                                        class="bodytxt"></asp:Label>
                                    <asp:Label ID="lblEmpName" runat="server" Text="" class="bodytxt"></asp:Label>
                                    <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt"></asp:Label>
                                </div>
                                <div class="col-sm-6 text-right">
                                    <asp:Button ID="btnBulkInsert" runat="server" OnClick="btnBulkInsert_Click" Text="Bulk Insert" />
                                    <input id="btnsave" type="button" onclick="SubmitForm();" disabled="disabled" value="Save"
                                        class="textfields btn btn-sm red" style="visibility: hidden" />
                                </div>
                            </div>



                            <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">
                                <script type="text/javascript">
                                    function ValidateNRIC() {
                                        var result = false;
                                        // alert('passtype');
                                        //  alert(document.getElementById("cmbQuickPassType").value);
                                        var passtype = document.getElementById("cmbQuickPassType").value;

                                        if (passtype == 'OT') {
                                            // alert('passtype true');
                                            result = true;
                                        }
                                        else {
                                            var nricType = $("#cmbQuickRefType option:selected").val();
                                            var nricVal = $("#txtNRICNumber").val();
                                            // alert(nricType);
                                            // alert(nricVal);
                                            $.ajax({
                                                type: "POST",
                                                url: "AddEditEmployee.aspx/GetNRI_Check",
                                                data: '{"nricType":"' + nricType + '","nricVal":"' + nricVal + '"}',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                async: false,
                                                cache: false,
                                                success: function (response) {

                                                    if (response.d == 'yes') {

                                                        result = true;

                                                    }
                                                    if (response.d == 'no') {

                                                        result = false;
                                                    }

                                                },
                                                failure: function (response) {
                                                    //  alert(response.d);
                                                }
                                            });
                                        }
                                        //alert('exception');
                                        // alert(result);
                                        return result;
                                    }
                                </script>
                                <script type="text/javascript">
                                    function ValidateNRICValue() {
                                        var result = false;
                                        if (document.getElementById("cmbEmpType").value == 'OT') {

                                            result = true;
                                        }
                                        else {
                                            var nricType = $("#cmbEmpRefType option:selected").val();
                                            var nricVal = $("#txtnricno").val();
                                            $.ajax({
                                                type: "POST",
                                                url: "AddEditEmployee.aspx/GetNRI_Check",
                                                data: '{"nricType":"' + nricType + '","nricVal":"' + nricVal + '"}',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                async: false,
                                                cache: false,
                                                success: function (response) {


                                                    if (response.d == 'yes') {
                                                        result = true;

                                                    }
                                                    if (response.d == 'no') {
                                                        result = false;
                                                    }

                                                },
                                                failure: function (response) {
                                                    //   alert(response.d);
                                                }
                                            });
                                        }

                                        return result;
                                    }
                                </script>

                                <script type="text/javascript">
                                    function ValidateQuickNRICValue() {
                                        var result = false;
                                        if (document.getElementById("cmbQuickPassType").value == 'OT') {

                                            result = true;
                                        }
                                        else {
                                            var nricType = $("#cmbQuickRefType option:selected").val();
                                            var nricVal = $("#txtNRICNumber").val();
                                            $.ajax({
                                                type: "POST",
                                                url: "AddEditEmployee.aspx/GetNRI_Check",
                                                data: '{"nricType":"' + nricType + '","nricVal":"' + nricVal + '"}',
                                                contentType: "application/json; charset=utf-8",
                                                dataType: "json",
                                                async: false,
                                                cache: false,
                                                success: function (response) {

                                                    if (response == 'yes') {
                                                        result = true;

                                                    }
                                                    if (response == 'no') {
                                                        result = false;
                                                    }

                                                },
                                                failure: function (response) {
                                                    //  alert(response.d);
                                                }
                                            });
                                        }

                                        return result;
                                    }
                                </script>

                                <script type="text/javascript">

                                    function onTabSelected(sender, args) {
                                        var ctrl = document.getElementById('txtEmpCodeHdn');
                                        if (ctrl.value != "0") {
                                            var str = args.get_tab().get_text();
                                            var btn = document.getElementById('btnsave')
                                            if (str == 'Leave Info' || str == 'Certificate Info' || str == 'Safety Info' || str == 'Training Info' || str == 'Item Issued Info' || str == 'File Upload Info' || str == 'Progression Info' || str == 'Family Info') {
                                                document.getElementById('btnsave').style.visibility = "visible";
                                                btn.disabled = true;
                                            }
                                            else {
                                                document.getElementById('btnsave').style.visibility = "visible";
                                                btn.disabled = false;
                                            }
                                        } else {

                                            var str = args.get_tab().get_text();
                                            var btn1 = document.getElementById('btnsave')
                                            if (str == 'Quick Add') {

                                                document.getElementById('btnsave').style.visibility = "hidden";
                                            }
                                            else {

                                                document.getElementById('btnsave').style.visibility = "visible";
                                                btn1.disabled = false;
                                            }
                                        }
                                    }

                                    function computeTotal(txtBoxUnit, txtBoxCost, txtBoxTotal) {
                                        if (event.keyCode == 9) {
                                            var tbUnit = txtBoxUnit.value;
                                            var tbCost = txtBoxCost.value;
                                            var tbTotal = tbUnit * tbCost;
                                            txtBoxTotal.value = tbTotal;
                                        }
                                    }
                                    function datepicker() {

                                        //                var datepicker = (<%= rdPRDate.ClientID %>); 
                                        //                datepicker.Clear();   
                                        //                datepicker.DateInput.Disable();                          
                                        //                datepicker.PopupButton.onclick = null;

                                        var datepicker = $find('<%= rdPRDate.ClientID %>');
                                        datepicker.clear();
                                        datepicker.set_enabled(false);
                                    }
                                    function LoadSaveControlStatus() {
                                        var ctrl = document.getElementById('txtEmpCodeHdn');
                                        if (ctrl.value != "0") {

                                            var btn = document.getElementById('btnsave')
                                            document.getElementById('btnsave').style.visibility = "visible";
                                            btn.disabled = false;


                                        } else {

                                            var btn1 = document.getElementById('btnsave')

                                            btn1.disabled = true;
                                        }
                                    }

                                    function cleardata() {

                                        document.getElementById('txtFirstName').value = "";
                                        document.getElementById('txtQuickLastName').value = "";
                                        document.getElementById('cmbQuickSex').value = "";
                                        document.getElementById('rdBirthDate').value = "";
                                        document.getElementById('rdJointingDate').value = "";
                                        document.getElementById('cmbQuickRace').value = "";
                                        document.getElementById('rdQuickPRDate').value = "";
                                        document.getElementById('txtNRICNumber').value = "";
                                        document.getElementById('cmbQuickPassType').value = "";
                                        document.getElementById('cmbQuickNationality').value = "";


                                        //                
                                    }
                                    function datepickerenable() {
                                        var datepicker = $find('<%= rdPRDate.ClientID %>');
                                        datepicker.set_enabled(true);

                                        //                var datepicker = (<%= rdPRDate.ClientID %>); 
                                        //                datepicker.DateInput.Enable();
                                        //                datepicker.PopupButton.onclick =function()
                                        //                {
                                        //                    datepicker.TogglePopup();
                                        //                    return false;
                                        //                };  

                                    }

                                    function OnDateSelected(sender, e) {
                                        //cpf_change();
                                        PREmployyeCPFChnage();
                                    }

                                    function OnDateSelected2(sender, e) {
                                        var datepicker1 = $find('<%= rdJoiningDate.ClientID %>');
                                        datepicker1.set_enabled(true);
                                        var date = datepicker1.get_selectedDate();
                                        if (date != null) {
                                            var ctrl = document.getElementById('txtLeaveModel');
                                            var ctrltxtJoiningDt = document.getElementById('txtJoiningDt');
                                            if (ctrl.value == "3" || ctrl.value == "4" || ctrl.value == "6" || ctrl.value == "8") {
                                                var bAnswer = confirm(' Are you sure, you want to enter/change Joining Date: ' + date.toDateString() + '\n Once you enter Joining Date, then the existing YOS Leave Records will get Reset.');
                                                if (!bAnswer) {

                                                    var dt2 = parseInt(ctrltxtJoiningDt.value.substring(0, 2), 10);
                                                    var mo2 = parseInt(ctrltxtJoiningDt.value.substring(3, 5), 10);
                                                    var yr2 = parseInt(ctrltxtJoiningDt.value.substring(6, 10), 10);
                                                    var date2 = new Date(yr2, mo2, dt2);

                                                    datepicker1.set_selectedDate(date2);
                                                    //alert(ctrltxtJoiningDt.value);
                                                    //datepicker1.clear();
                                                }
                                                else {
                                                    ctrltxtJoiningDt.value = date;
                                                }
                                            }
                                        }
                                    }

                                    function OnDateSelected1(sender, e) {

                                        var strirmsg = "";
                                        strirmsg = CompareDate(document.getElementById("rdJoiningDate").value, document.getElementById("rdTerminationDate").value,
                                            "Job Info-Termination Date Cannot be less than Joining Date <br>", "");
                                        if (strirmsg != "") {
                                            WarningNotification(strirmsg);
                                            //var datepicker1 = <%= rdTerminationDate.ClientID %>;
                                            var datepicker2 = $find('<%= rdTerminationDate.ClientID %>');
                                            datepicker2.clear();
                                        } else {
                                            //var datepicker1 = <%= rdTerminationDate.ClientID %>;
                                            var datepicker1 = $find('<%= rdTerminationDate.ClientID %>');
                                            datepicker1.set_enabled(true);
                                            var date = datepicker1.get_selectedDate();
                                            if (date != null) {
                                                var bAnswer = confirm(' Are you sure, you want to enter Termination Date: ' + date.toDateString() + '\n Once you enter Termination Date, this employee will no longer be avail for any PAYROLL task e.g. No leave, No payroll, No Addition etc.');
                                                if (!bAnswer) {
                                                    datepicker1.clear();
                                                }
                                            }
                                        }
                                    }

                                    function ValidatePayRecord() {
                                        if (document.getElementById('drpPayFrequency').value == 'H') {
                                            document.getElementById('custxtPayRate').value = '0';

                                        }
                                        else if (document.getElementById('drpPayFrequency').value == 'D') {
                                            document.getElementById('custxtPayRate').value = '0';
                                        }
                                        else {
                                            document.getElementById('custxtPayRate').value = '';
                                        }
                                    }

                                </script>

                            </telerik:RadCodeBlock>
                            <input type="hidden" id="txthid" runat="server" value="0" />
                            <input type="hidden" id="oHidden" name="oHidden" runat="server" />
                            <input type="hidden" id="Hidden1" name="Hidden1" runat="server" />
                            <input type="hidden" id="txtHiddenPayRate" name="txtHiddenPayRate" runat="server" />
                            <input type="hidden" id="hdsinda" name="hdsinda" runat="server" />
                            <input type="hidden" id="hdcdac" name="hdcdac" runat="server" />
                            <input type="hidden" id="hdecf" name="hdecf" runat="server" />
                            <input type="hidden" id="hdmbmf" name="hdmbmf" runat="server" />
                            <input type="hidden" id="hdcchest" name="hdcchest" runat="server" />
                            <input type="hidden" id="txtFundType" name="txtFundType" runat="server" />
                            <input type="hidden" id="txtFundAmount" name="txtFundAmount" runat="server" />
                            <input type="hidden" id="txtEmployerCPF_H" name="txtEmployerCPF_H" runat="server" />
                            <input type="hidden" id="txtEmployeeCPF_H" name="txtEmployeeCPF_H" runat="server" />
                            <input type="hidden" id="txt_Nric_H" name="txt_Nric_H" runat="server" />
                            <input type="hidden" id="txtPhotoExt_H" name="txtPhotoExt_H" runat="server" />
                            <input type="hidden" id="txthdnleaves" name="txthdnleaves" runat="server" />
                            <input type="hidden" id="txtEmpCodeHdn" name="txtEmpCodeHdn" runat="server" />
                            <input type="hidden" id="txtLeaveModel" name="txtLeaveModel" runat="server" />
                            <input type="hidden" id="txtJoiningDt" name="txtJoiningDt" runat="server" />
                            <input type="hidden" id="txtPerceSB" name="txtPerceSB" runat="server" />
                            <asp:Label ID="Label1" Visible="False" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="9pt" ForeColor="Red" Text="Label" Width="31px"></asp:Label>
                            <table style="display: none" width="98%" border="0" cellpadding="2" cellspacing="2">
                                <tr>
                                    <td>
                                        <input type="text" disabled="disabled" class="textfields" id="txtICPPNumber" runat="server"
                                            style="width: 160px" />
                                    </td>
                                    <td></td>
                                    <td style="height: 29px">
                                        <input type="text" class="textfields" id="txtinsurance" runat="server" style="width: 88px" />
                                    </td>
                                    <td style="height: 29px">
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdinsurance"
                                            runat="server">
                                            <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hleaves" runat="server" />
                                    </td>
                                    <td>
                                        <select id="cmbEmpCategory" runat="server" class="textfields" style="width: 120px">
                                            <option value="1" selected="selected">General </option>
                                            <option value="2">OT PAY - 2 HOURS </option>
                                            <option value="3">OT PAY - 4 HOURS </option>
                                        </select>
                                    </td>
                                    <td style="height: 30px">
                                        <input type="text" class="textfields" id="txtcsoc" runat="server" style="width: 88px" />
                                    </td>
                                    <td style="height: 30px">
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdcsoc"
                                            runat="server">
                                            <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </td>
                                    <td>
                                        <input type="text" class="textfields" id="txtpassport" runat="server" style="width: 88px" />
                                    </td>
                                    <td width="35%">
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdpassport"
                                            runat="server">
                                            <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </td>
                                    <td width="35%">
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdppissuedate"
                                            runat="server">
                                            <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </td>
                                    <td>
                                        <input type="text" readonly="readonly" id="txtCCHEST" runat="server" style="width: 90px; font-size: 9pt; height: 12px;" /></td>
                                </tr>
                            </table>
                            <div class="exampleWrapper clearfix exampleWrapper-employee">
                                <telerik:RadTabStrip OnClientTabSelected="onTabSelected" ID="tbsEmp" runat="server"
                                    SelectedIndex="0" MultiPageID="tbsEmp12" Skin="Outlook" Orientation="VerticalLeft"
                                    CssClass="col-sm-2">
                                    <Tabs>
                                        <telerik:RadTab TabIndex="0" runat="server" AccessKey="P" Text="&lt;u&gt;Q&lt;/u&gt;uick Add"
                                            PageViewID="tabQucikInfo" Selected="true">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="1" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ersonal Info"
                                            PageViewID="tabPagePersonal" Selected="true">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="2" runat="server" AccessKey="O" Text="Contact inf&lt;u&gt;o&lt;/u&gt;"
                                            PageViewID="tabPageContact">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="3" runat="server" AccessKey="J" Text="&lt;u&gt;J&lt;/u&gt;ob Info"
                                            PageViewID="tabPageJob">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="4" runat="server" AccessKey="S" Text="&lt;u&gt;S&lt;/u&gt;alary Info"
                                            PageViewID="tabPageSalary">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="5" runat="server" AccessKey="B" Text="&lt;u&gt;B&lt;/u&gt;ank Info"
                                            PageViewID="tabPageBank">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="6" runat="server" AccessKey="W" Text="Foreign &lt;u&gt;W&lt;/u&gt;orker"
                                            PageViewID="tabPageForeignWorker">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="7" runat="server" AccessKey="R" Text="&lt;u&gt;R&lt;/u&gt;emarks"
                                            PageViewID="tbsNotes">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="8" runat="server" AccessKey="L" Text="&lt;u&gt;L&lt;/u&gt;eave Info"
                                            PageViewID="tabPageLeaveHistory">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="9" runat="server" AccessKey="C" Text="&lt;u&gt;C&lt;/u&gt;ertificate Info"
                                            PageViewID="tabPageCertificate">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="10" runat="server" AccessKey="f" Text="Sa&lt;u&gt;f&lt;/u&gt;ety Info"
                                            PageViewID="tabPageSaftety">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="11" runat="server" AccessKey="T" Text="&lt;u&gt;T&lt;/u&gt;raining Info"
                                            PageViewID="tabPageTraining">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="12" runat="server" AccessKey="U" Text="Item Iss&lt;u&gt;u&lt;/u&gt;ed Info"
                                            PageViewID="tbsItemIss">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="13" runat="server" AccessKey="F" Text="File Upload Info"
                                            PageViewID="tbsFileUpload">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="14" runat="server" AccessKey="H" Text="Progression Info"
                                            PageViewID="tbsEmpPayHistory">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="15" runat="server" AccessKey="M" Text="Fa&lt;u&gt;m&lt;/u&gt;ily Info"
                                            PageViewID="tabPageFamily">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="16" Visible="false" runat="server" AccessKey="I" Text="&lt;u&gt;I&lt;/u&gt;R8A Info"
                                            PageViewID="tbsIR8A">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="17" Visible="false" runat="server" AccessKey="A" Text="&lt;u&gt;I&lt;/u&gt;R8A Appendex A"
                                            PageViewID="tbsIR8AApendix">
                                        </telerik:RadTab>
                                        <telerik:RadTab TabIndex="18" Visible="true" runat="server" AccessKey="E" Text="Employee Calendar"
                                            PageViewID="tbsempcalendar">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <!--
            no spaces between the tabstrip and multipage, in order to remove unnecessary whitespace
            -->
                                <telerik:RadMultiPage runat="server" ID="tbsEmp12" SelectedIndex="0" CssClass="multiPage col-sm-10">
                                    <telerik:RadPageView ID="tabQucikInfo" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">Quick Add Information</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>First Name</label>
                                                    <input id="txtFirstName" type="text" class="textfields form-control input-sm alphabetsonly" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Last Name</label>
                                                    <input id="txtQuickLastName" type="text" class="textfields form-control input-sm alphabetsonly" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Gender</label>
                                                    <select id="cmbQuickSex" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected" value="s">-select-</option>
                                                        <option value="M">Male</option>
                                                        <option value="F">Female</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt><a href="../Management/ManageNationality.aspx">Nationality</a></label>
                                                    <select id="cmbQuickNationality" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                        </div>
                                        <!-- /.row -->

                                        <div class="row">
                                            <% if (Session["Country"].ToString() != "383")
                                                { %>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Employer CPF Number</label>
                                                    <input id="txtCPFNumber" type="text" class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>
                                            <%} %>

                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Employee Pass Type</label>
                                                    <select id="cmbQuickPassType" runat="server" class="textfields form-control input-sm" onchange="emp_qucick_type_change(this)">
                                                        <option value="NA" selected="selected">-select-</option>
                                                        <option value="SC">Singapore Citizen</option>
                                                        <option value="SPR">Singapore PR</option>
                                                        <option value="DP">Dependant Pass</option>
                                                        <option value="EP">Employment Pass</option>
                                                        <option value="WP">Work Permit</option>
                                                        <option value="SP">S Pass</option>
                                                        <option value="OT">Others</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">


                                                    <% if (Session["Country"].ToString() != "383")
                                                        { %>
                                                    <label class="block"><tt class="required">*</tt>NRIC/FIN</label>
                                                    <select id="cmbQuickRefType" name="cmbQuickRefType" runat="server" class="textfields form-control width-85px input-inline">
                                                        <option value="1" selected="selected">NRIC</option>
                                                        <option value="2">FIN</option>
                                                    </select>
                                                    <%}
                                                        else
                                                        {%>
                                                    <tt class="required">*</tt><label><tt class="required">*</tt>Employe ID</label>
                                                    <%} %>



                                                    <%-- <asp:TextBox class="textfields form-control input-small input-inline custom-maxlength" MaxLength="50" AutoPostBack="false" onkeyup="javascript:CheckCharacters(this);"
                                                        runat="server" ID="txtNRICNumber"></asp:TextBox>--%>

                                                    <asp:TextBox class="textfields form-control width-120px input-inline custom-maxlength" MaxLength="50" AutoPostBack="false"
                                                        runat="server" ID="txtNRICNumber"></asp:TextBox>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <%-- <div class="col-sm-3" style="visibility: hidden">--%>
                                            <div class="form-group" id="tdPRDate" style="visibility: hidden">
                                                <%--MURU--%>
                                                <label class="block"><tt class="required">*</tt>PR Date</label>
                                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                    ID="rdQuickPRDate" runat="server" ViewStateMode="Enabled">
                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    <ClientEvents OnDateSelected="OnDateSelected" />
                                                </radCln:RadDatePicker>
                                            </div>
                                            <%--   </div>--%>

                                            <!-- /.col-sm-6 -->
                                        </div>
                                        <!-- /.row -->

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt><a href="../Management/ManageRace.aspx">Race</a></label>
                                                    <select id="cmbQuickRace" runat="server" onchange="race_change(this)" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Joining Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdJointingDate" runat="server">
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="OnDateSelected2" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>

                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Birth Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdBirthDate" runat="server">
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <!-- /.col-sm-6 -->
                                            <!-- /.col-sm-6 -->
                                            <% if ((HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMS") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSI") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSAMC") || (HttpContext.Current.Session["ANBPRODUCT"].ToString() == "WMSMC"))
                                                {
                                            %>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Time/Card/Swipe/Punch ID</label>
                                                    <input id="txtTimeCardNumber" type="text" class="textfields form-control input-sm custom-maxlength" runat="server" maxlength="50" />
                                                </div>
                                            </div>
                                            <%} %>

                                            <!-- /.col-sm-6 -->
                                        </div>
                                        <!-- /.row -->
                                        <div class="row">
                                            <div class="row col-sm-12 text-center padding-tb-15">

                                                <input id="btnQuickSave" type="button" onclick="SubmitQuickForm();" value="Save Changes"
                                                    class="textfields btn red" />
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPagePersonal" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Personal Information</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Employee Number</label>
                                                    <input id="txtEmpCode" type="text" class="textfields form-control input-sm" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>First Name</label>
                                                    <input id="txtEmpName" type="text" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Last Name</label>
                                                    <input id="txtlastname" type="text" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Gender</label>
                                                    <select id="cmbSex" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected" value="s">-select-</option>
                                                        <option value="M">Male</option>
                                                        <option value="F">Female</option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Marital Status</label>
                                                    <select id="cmbMaritalStatus" runat="server" class="textfields form-control input-sm">
                                                        <option value="S" selected="selected">Single</option>
                                                        <option value="M">Married </option>
                                                        <option value="D">Divorced </option>
                                                        <option value="W">Widower </option>
                                                        <option value="WE">Widowee </option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageReligion.aspx">Religion</a></label>
                                                    <select id="cmbReligion" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Blood Group</label>
                                                    <asp:TextBox ID="txtBloodGroup" class="textfields form-control input-sm custom-maxlength" runat="server" MaxLength="3">
                                                    </asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Time/Card/Swipe/Punch ID</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txttimecardno" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alias/Login ID</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength cleanstring" id="txtEmpAlias" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Place of Birth</label>
                                                    <select id="cmbbirthplace" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt><a href="../Management/ManageNationality.aspx">Nationality</a></label>
                                                    <select id="cmbNationality" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageCountry.aspx">Country of Residence</a></label>
                                                    <select id="cmbCountry" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <% if (Session["mobilescancode"].ToString() == "True")
                                                    { %>
                                                <label>Scan Code</label>
                                                <input type="text" class="textfields form-control input-sm" id="ScanCode" runat="server" />
                                                <%} %>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Rights Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Login Rights</label>
                                                    <select id="cmbUserGroup" disabled="disabled" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="EmployeeGroups.aspx">Employee Leave Group</a></label>
                                                    <select id="cmbEmployeeGroup" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-9">

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="block">(C) Contact Information</h4>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Phone</label>
                                                            <input type="text" class="textfields form-control input-sm numericonly custom-maxlength" id="txtPhone" runat="server" maxlength="10" />
                                                            <input style="display: none" type="text" class="textfields form-control input-sm" id="txtAddress" runat="server" />
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Mobile</label>
                                                            <input type="text" id="txtHandPhone" class="textfields form-control input-sm  numericonly custom-maxlength" runat="server" maxlength="10" />
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>Email</label>
                                                            <input type="text" class="textfields form-control input-sm  custom-maxlength chkemail" id="txtEmail" runat="server" maxlength="50" />
                                                            <input style="display: none" type="text" class="textfields form-control input-sm custom-maxlength" id="txtlocal2" runat="server" />
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <h4 class="block">(D) Other Information</h4>
                                                    </div>
                                                    <% if (Session["Country"].ToString() != "383")
                                                        { %>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label><tt class="required">*</tt>Employer CPF Number</label>
                                                            <select id="cmbEmployerCPFAcctNumber" runat="server" class="textfields form-control input-sm">
                                                            </select>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <label>CPF Contribution Sector</label>
                                                            <select id="cmbempcpfgroup" runat="server" class="textfields form-control input-sm">
                                                                <option value="1" selected="selected">Private Sector Employees</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <%} %>
                                                </div>

                                            </div>
                                            <div class="col-sm-3 text-center">
                                                <div class="form-group">
                                                    <div class="fileinput fileinput-new" data-provides="fileinput">
                                                        <div class="fileinput-new thumbnail" style="width: 200px; height: 150px; border: none;">
                                                            <asp:Image ID="UploadedImage" runat="server" Style="width: 200px; height: 150px;" />
                                                        </div>
                                                        <div class="fileinput-preview fileinput-exists thumbnail" style="width: 200px; height: 150px;"></div>
                                                        <div>
                                                            <span class="btn default btn-file">
                                                                <span class="fileinput-new">Select image </span>
                                                                <span class="fileinput-exists">Change </span>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            </span>
                                                            <a href="javascript:;" class="btn default fileinput-exists" data-dismiss="fileinput">Remove </a>
                                                        </div>
                                                    </div>
                                                    <asp:Button ID="btnSaveProfilePicture" Text="Save Image" OnClientClick="return validateEmployeeImage();" OnClick="btnSaveProfilePicture_click" runat="server" CssClass="btn red" />

                                                </div>
                                                <div>
                                                    <%--<asp:Button ID="btnCancelProfilePicture" Text="Cancel" OnClick="btncancel_click" runat="server" CssClass="btn default" />--%>
                                                </div>
                                                <img alt="" src="" id="Image1" runat="server" style="width: 157px; height: 159px" visible="false" />
                                                <br />
                                                <asp:ImageButton ID="buttonDelete" AlternateText="Delete" runat="server" ImageUrl="~/frames/images/toolbar/treeMinus.gif"
                                                    OnClick="buttonDelete_Click" Height="16px" Width="15px" Visible="false" />
                                                <asp:ImageButton ID="ButtonAdd" AlternateText="Add" runat="server" Height="16px"
                                                    ImageUrl="~/frames/images/toolbar/treePlus.gif" OnClick="ButtonAdd_Click" Width="15px" Visible="false" />

                                                <div class="form-group">
                                                    <radU:RadUpload ID="RadUpload1" BackColor="transparent" BorderColor="transparent"
                                                        OnClientFileSelected="myOnCientFileSelected" OnClientClearing="myOnClientCleared"
                                                        InitialFileInputsCount="1" runat="server" EnableFileInputSkinning="false" Localization-Select=""
                                                        ControlObjectsVisibility="ClearButtons" MaxFileInputsCount="1" OverwriteExistingFiles="True" />
                                                </div>

                                            </div>

                                        </div>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageContact" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Local Address Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Block</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtblock" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Street/Building</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtstreet" runat="server" maxlength="100" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Level</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtlevel" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Unit</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtunit" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Postal Code</label>
                                                    <input type="text" class="textfields form-control input-sm numericonly _txtpostalcode" id="txtPostalCode" runat="server" maxlength="6" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Foreign Address Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address-1</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtforeign1" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address-2</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtforeign2" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address-3</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtforeignpostalcode" runat="server" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Emergency Contact Address Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Contact Person</label>
                                                    <asp:TextBox ID="txtEmeConPer" CssClass="textfields form-control input-sm alphabetsonly" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Relationship</label>
                                                    <asp:TextBox ID="txtEmeConPerRel" CssClass="textfields form-control input-sm alphabetsonly" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Phone-1</label>
                                                    <asp:TextBox ID="txtEmeConPerPh1" CssClass="textfields form-control input-sm numericonly" runat="server" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Phone-2</label>
                                                    <asp:TextBox ID="txtEmeConPerPh2" CssClass="textfields form-control input-sm numericonly" runat="server" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address</label>
                                                    <asp:TextBox ID="txtEmeConPerAdd" CssClass="textfields form-control input-sm custom-maxlength" runat="server" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                                    <textarea id="txtEmeConPerRem" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="250"
                                                        rows="1" cols="1"></textarea>
                                                </div>
                                            </div>

                                        </div>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageSalary" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Salary Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Employee Pass Type</label>
                                                    <select id="cmbEmpType" runat="server" class="textfields form-control input-sm" onchange="emp_type_change(this)">
                                                        <option value="NA" selected="selected">-select-</option>
                                                        <option value="SC">Singapore Citizen</option>
                                                        <option value="SPR">Singapore PR</option>
                                                        <option value="DP">Dependant Pass</option>
                                                        <option value="EP">Employment Pass</option>
                                                        <option value="WP">Work Permit</option>
                                                        <option value="SP">S Pass</option>
                                                        <option value="OT">Others</option>
                                                    </select>
                                                    <label runat="server" style="display: none;" id="other_type">
                                                        CPF/Funds/SDL not calculated</label>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <% if (Session["Country"].ToString() != "383")
                                                        { %>
                                                    <label class="block"><tt class="required">*</tt>NRIC/FIN</label>
                                                    <select id="cmbEmpRefType" name="cmbEmpRefType" runat="server" class="textfields form-control input-sm width-85px  input-inline">
                                                        <option value="1" selected="selected">NRIC</option>
                                                        <option value="2">FIN</option>
                                                    </select>
                                                    <%}
                                                        else
                                                        {%>
                                                    <label><tt class="required">*</tt>Employe ID</label>
                                                    <%} %>

                                                    <asp:TextBox class="textfields form-control  input-inline width-120px custom-maxlength" MaxLength="50" AutoPostBack="false" onkeyup="javascript:CheckCharacters(this);"
                                                        runat="server" ID="txtnricno"></asp:TextBox>


                                                </div>
                                            </div>



                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <% if (Session["Country"].ToString() != "383")
                                                        { %>
                                                    <label>PR Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdPRDate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="OnDateSelected" />
                                                    </radCln:RadDatePicker>
                                                    <%} %>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div id="tdEmployeeCPFText" style="visibility: hidden">
                                                        <b>Employee CPF:</b>
                                                    </div>
                                                    <div id="tdEmployeeCPFValue" style="visibility: hidden">
                                                        <input type="radio" id="rdoEmployeeNo" name="rdoEmployee" checked runat="server" />Graduated
                                                                   
                                                        <input type="radio" id="rdoEmployeeYes" name="rdoEmployee" runat="server" />Full
                                                   
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Email Pay Slip</label>
                                                    <select id="cmbEmailPaySlip" class="textfields form-control input-sm" runat="server">
                                                        <option value="Y">Yes </option>
                                                        <option value="N" selected="selected">No</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll Type</label>
                                                    <telerik:RadComboBox ID="ddlPayrollType" runat="server" EmptyMessage="-select-"
                                                        MarkFirstMatch="true" Font-Names="Tahoma" EnableLoadOnDemand="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="Monthly" Value="1" />
                                                            <telerik:RadComboBoxItem Text="Bi-Monthly" Value="2" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Half Salary</label>
                                                    <telerik:RadComboBox ID="ddlHalfSal" runat="server" EmptyMessage="-select-"
                                                        MarkFirstMatch="true" Font-Names="Tahoma" OnSelectedIndexChanged="ddlHalfSal_SelectedIndexChanged" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="None" Value="0" />
                                                            <telerik:RadComboBoxItem Text="Half" Value="1" />
                                                            <telerik:RadComboBoxItem Text="ExcludeBasicFirstHalf" Value="2" />
                                                            <telerik:RadComboBoxItem Text="IncludeFullBasicFirstHalf" Value="3" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="display: none">
                                                <div class="form-group">
                                                    <div id="tdEmployerCPFText" style="visibility: hidden">
                                                        <label>Employer CPF</label>
                                                    </div>
                                                    <div id="tdEmployerCPFValue" style="visibility: hidden">
                                                        <input type="radio" id="rdoEmployerNo" name="rdoEmployer" checked runat="server" />Graduated
                                                                   
                                                        <input type="radio" id="rdoEmployerYes" name="rdoEmployer" runat="server" />Full
                                                   
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Rate Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Basic Pay</label>
                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <asp:TextBox ID="txtPayRate" CssClass="textfields form-control input-sm" runat="server" onkeyup="javascript:CalculateHourlyRate(this);"
                                                        onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                    <% }
                                                        else
                                                        { %>
                                                    <asp:TextBox ID="txtPayRate1" CssClass="textfields form-control input-sm" runat="server" Text="******" Enabled="false"></asp:TextBox>
                                                    SGD
                                                                   
                                                    <% } %>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>No of Work Days in Week</label>
                                                    <select id="cmbworkingdays" runat="server" class="textfields form-control input-sm" onchange="CHK_WorkingDays(this);">
                                                        <option value="">-select-</option>
                                                        <option value="5">5</option>
                                                        <option value="5.5">5.5</option>
                                                        <option value="6">6</option>
                                                        <option value="7">7</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <label>Pay Frequency</label>
                                                <asp:RadioButtonList ID="rbpayfrequency" class="bodytxt" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Value="M">&nbsp;Monthly</asp:ListItem>
                                                    <asp:ListItem Value="H">&nbsp;Hourly</asp:ListItem>
                                                    <asp:ListItem Value="D">&nbsp;Daily</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group form-md-radios">
                                                    <input type="radio" id="rdoMOMDailyRate" name="DailyRate" value="A" checked runat="server"
                                                        onclick="CHK_DailyRate(this);" />
                                                    <label>Daily Rate(MOM)</label>

                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <input type="text" id="txtDailyRate" name="txtDailyRate"
                                                        class="textfields form-control input-sm" runat="server" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="text" id="txtDailyRate1" name="txtDailyRate1" disabled="disabled" value="******"
                                                        class="textfields form-control input-sm" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group form-md-radios">
                                                    <input type="radio" id="rdoMannualDailyRate" name="DailyRate" value="M" runat="server"
                                                        onclick="CHK_DailyRate(this);" />
                                                    <label>Daily Rate(M)</label>

                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <input type="text" id="txtMannualDailyRate" name="txtMannualDailyRate"
                                                        class="textfields form-control input-sm" runat="server" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="text" id="txtMannualDailyRate1" name="txtMannualDailyRate1" disabled="disabled"
                                                        value="******" class="textfields form-control input-sm" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group form-md-radios">
                                                    <input type="radio" id="rdoMOMHourlyRate" name="HourlyRate" value="A" checked runat="server"
                                                        onclick="CHK_HourlyRate(this);" />
                                                    <label><a href="#" class="nav1" onclick="ShowHourlyRate();">Hr Rate(MOM)</a></label>

                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                                    &nbsp;&nbsp;<input type="text" id="txtHourlyRate" onkeyup="javascript:CalculateOTRate(this);"
                                                                        name="txtHourlyRate" class="textfields form-control input-sm" runat="server" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="text" id="txtHourlyRate1" name="txtHourlyRate1" disabled="disabled"
                                                        value="******" class="textfields form-control input-sm" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group form-md-radios">
                                                    <input type="radio" id="rdoManualHourlyRate" name="HourlyRate" value="M" runat="server"
                                                        onclick="CHK_HourlyRate(this);" />
                                                    <label>Hr Rate(M)</label>

                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <input type="text" id="txtMannualHourlyRate" onkeyup="javascript:CalculateOTRate(this);"
                                                        name="txtMannualHourlyRate" class="textfields form-control input-sm" runat="server" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="text" id="txtMannualHourlyRate1" name="txtMannualHourlyRate1" disabled="disabled"
                                                        value="******" class="textfields form-control input-sm" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Overtime Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Overtime Entitled</label>
                                                    <select id="cmbOTEntitled" runat="server" class="textfields form-control input-sm">
                                                        <option value="N">No</option>
                                                        <option value="Y">Yes</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Overtime Rate-1</label>
                                                    <asp:TextBox Style="text-align: right" MaxLength="5" ID="txtOT1Rate" CssClass="textfields form-control input-sm" Text="1.5"
                                                        runat="server" onkeyup="javascript:CalculateOTRate(this);" onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                    &nbsp;<tt class="bodytxt"><asp:Label ID="lbltxtOt1Rate" runat="server"></asp:Label></tt>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Overtime Rate-2</label>
                                                    <asp:TextBox Style="text-align: right" MaxLength="5" ID="txtOT2Rate" Text="2" CssClass="textfields form-control input-sm"
                                                        runat="server" onkeyup="javascript:CalculateOTRate(this);" onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                    &nbsp;<tt class="bodytxt"><asp:Label ID="lbltxtOt2Rate" runat="server"></asp:Label></tt>
                                                </div>
                                            </div>

                                        </div>



                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(D) Variable Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div style="background-color: Transparent; border: 0px;" id="trv1" runat="server">
                                                        <label>
                                                            <asp:Label ID="lblv1rate" runat="server"></asp:Label></label>
                                                    </div>

                                                    <div style="background-color: Transparent; border: 0px;" id="trv4" runat="server">
                                                        <asp:TextBox Style="text-align: right" ID="txtv1rate" CssClass="textfields form-control input-sm number-dot" data-type="currency" MaxLength="12" runat="server"></asp:TextBox><label>[Variable-1]</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div style="background-color: Transparent; border: 0px;" id="trv2" runat="server">
                                                        <label>
                                                            <asp:Label ID="lblv2rate" runat="server"></asp:Label></label>
                                                    </div>

                                                    <div style="background-color: Transparent; border: 0px;" id="trv5" runat="server">
                                                        <asp:TextBox Style="text-align: right" ID="txtv2rate" CssClass="textfields form-control input-sm number-dot" data-type="currency" MaxLength="12" runat="server"></asp:TextBox><label>[Variable-2]</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div style="background-color: Transparent; border: 0px;" id="trv3" runat="server">
                                                        <label>
                                                            <asp:Label ID="lblv3rate" runat="server"></asp:Label></label>
                                                    </div>

                                                    <div style="background-color: Transparent; border: 0px;" id="trv6" runat="server">
                                                        <asp:TextBox Style="text-align: right" ID="txtv3rate" CssClass="textfields form-control input-sm number-dot" data-type="currency" MaxLength="12" runat="server"></asp:TextBox><label>[Variable-3]</label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div style="background-color: Transparent; border: 0px;" id="trv7" runat="server">
                                                        <label>
                                                            <asp:Label ID="lblv4rate" runat="server"></asp:Label></label>
                                                    </div>

                                                    <div style="background-color: Transparent; border: 0px;" id="trv8" runat="server">
                                                        <asp:TextBox Style="text-align: right" ID="txtv4rate" CssClass="textfields form-control input-sm number-dot" data-type="currency" MaxLength="12" runat="server"></asp:TextBox><label>[Variable-4]</label>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(E) CPF Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CPF Entitled</label>

                                                    <select id="cmbCPFEntitlement" runat="server" onchange="return cpf_change();" class="textfields form-control input-sm">
                                                        <option value="Y">Yes</option>
                                                        <option value="N" selected="selected">No</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Employee CPF A/c No</label>

                                                    <input type="text" class="textfields form-control input-sm" disabled="disabled" id="txtEmployeeCPFAcctNumber"
                                                        runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Income Tax ID</label>

                                                    <input type="text" disabled="disabled" class="textfields form-control input-sm" id="txtIncomeTaxID" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label>&nbsp;</label>
                                                <div>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        Compute CPF First Half
                                                    <input type="checkbox" onclick="CHKOptOutCPF();" id="chkcomputecpffh" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>



                                                <div style="display: none">
                                                    <input type="text" class="textfields form-control input-sm" id="txtEmployerCPF" disabled="disabled" runat="server" />
                                                    <input type="text" class="textfields form-control input-sm" id="txtEmployeeCPF" disabled="disabled" runat="server" />
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(F) Funds Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt><a href="../Management/ManageRace.aspx">Race</a></label>

                                                    <select id="cmbRace" runat="server" onchange="race_change(this)" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <label>&nbsp;</label>
                                                <div>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        Fund OPT Out
                                                    <input type="checkbox" onclick="CHKOptOut();" id="chkoptfund" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>

                                            </div>
                                            <div class="col-sm-3">
                                                <label>&nbsp;</label>
                                                <div>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        Compute Fund First Half
                                                    <input type="checkbox" id="chkFUNDRequired" onclick="CHKOptOutFund();" runat="server" />
                                                        <span></span>
                                                    </label>
                                                </div>

                                            </div>
                                            <div class="col-sm-3">
                                                <label>&nbsp;</label>
                                                <div>
                                                    <label class="mt-checkbox mt-checkbox-outline">
                                                        SDL Opt Out
                                                    <input type="checkbox" id="chkSDFRequired" runat="server">
                                                        <span></span>
                                                    </label>
                                                </div>


                                            </div>
                                        </div>


                                        <div class="row" style="visibility: hidden">
                                            <div class="col-sm-3">
                                                <label>SINDA</label>
                                                <input type="text" readonly="readonly" id="txtSINDA" runat="server" class="textfields form-control input-sm" />
                                            </div>
                                            <div class="col-sm-3">
                                                <label>ECF</label>
                                                <input type="text" readonly="readonly" id="txtECF" runat="server" class="textfields form-control input-sm" />
                                            </div>
                                            <div class="col-sm-3">
                                                <label>CDAC</label>
                                                <input type="text" id="txtCDAC" readonly="readonly" runat="server" class="textfields form-control input-sm" />
                                            </div>
                                            <div class="col-sm-3">
                                                <label>MBMF</label>
                                                <input type="text" readonly="readonly" id="txtMBMF" runat="server" class="textfields form-control input-sm" />
                                            </div>
                                        </div>

                                        <%} %>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageBank" runat="server" Width="100%">
                                        <div class="row">

                                            <div class="form-group form-md-line-input">
                                                <label class="col-sm-3 control-label width-auto" for="form_control_1">Payment Type Based</label>
                                                <div class="col-sm-9">
                                                    <asp:RadioButtonList ID="radListPayType" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="0" Selected="True">Percentage</asp:ListItem>
                                                        <asp:ListItem Value="1" Enabled="false">Currency</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Primary Bank Information:
                                                   
                                                    <asp:Label ID="lblPerc" runat="server" Text=""></asp:Label>%</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Pay Mode</label>
                                                    <select id="cmbPayMode" onchange="giro_change(this)" class="textfields form-control input-sm" runat="server">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>GIRO Account No</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength numericonly" maxlength="50" id="txtGIROAccountNo" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="row">
                                                    <div class="col-sm-8">
                                                        <label><tt class="required">*</tt><a href="../Management/Banks.aspx">GIRO Bank Code</a></label>
                                                        <div class="input-group">
                                                            <%--<span class="input-group-addon"></span>--%>
                                                            <select id="cmbbranchcode" onchange="setbranchcode(this);" class="textfields form-control input-sm" runat="server">
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>&nbsp;</label>
                                                        <div class="input-group">
                                                            <input type="text" class="textfields form-control input-sm" id="txtgirobankname" runat="server"
                                                                readonly="readonly" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>GIRO Branch Code</label>
                                                    <input type="text" id="txtgirobranch" runat="server" class="textfields form-control input-sm custom-maxlength cleanstring" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>GIRO Account Name</label>
                                                    <input type="text" id="txtgiroaccountname" runat="server" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength="50" />
                                                    <asp:DropDownList Visible="false" ID="drpCurrBank1" class="bodytxt" Enabled="false"
                                                        runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payment</label>
                                                    <asp:DropDownList ID="drpPaymentMode" class="textfields form-control input-sm" runat="Server">
                                                        <asp:ListItem Value="1">All</asp:ListItem>
                                                        <asp:ListItem Value="2">Basic</asp:ListItem>
                                                        <asp:ListItem Value="3">Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Secondary Bank Information:&nbsp;<asp:Label ID="lblPercSB2" runat="server" Text=""></asp:Label>%</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Pay From</label>
                                                    <select id="cmbSBPayMode" onchange="giro_changeSB(this)" class="textfields form-control input-sm" runat="server">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>GIRO Account No</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength numericonly" maxlength="50" id="txtSBGIROAccountNo" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/Banks.aspx">GIRO Bank Code</a></label>
                                                    <div class="input-group">
                                                        <%--<span class="input-group-addon"></span>--%>
                                                        <select id="cmbSBbranchcode" onchange="setbranchcodeSB(this);" class="textfields form-control input-sm"
                                                            runat="server">
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>GIRO Branch Code</label>
                                                    <input type="text" id="txtSBgirobranch" runat="server" class="textfields form-control input-sm custom-maxlength cleanstring" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>GIRO Account Name</label>

                                                    <input type="text" id="txtSBgiroaccountname" runat="server" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength="50" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Percentage</label>
                                                    <input type="text" id="txtSBperct" onkeyup="javascript:return changeperc(this);"
                                                        onkeydown="javascript:storeoldval(this.value);" onkeypress="return isNumericKeyStrokeDecimalPercent(event)"
                                                        maxlength="4" runat="server" class="textfields form-control input-sm" />
                                                    <asp:DropDownList Visible="false" class="bodytxt textfields form-control input-sm" ID="drpCurrBank2" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payment</label>
                                                    <asp:DropDownList ID="drpPaymentMode1" class="textfields form-control input-sm" runat="Server">
                                                        <asp:ListItem Value="1">All</asp:ListItem>
                                                        <asp:ListItem Value="2">Basic</asp:ListItem>
                                                        <asp:ListItem Value="3">Other</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Remarks</label>
                                                    <div class="row">
                                                        <div class="col-sm-8">
                                                            <input type="text" id="txtSBRemarks" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="250" />
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:Button ID="btnSB" runat="server" Text="Add" OnClientClick="return submitforSB();"
                                                                OnClick="btnSB_Click" CssClass="btn default" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <radG:RadGrid ID="RadGrid10" runat="server" Width="100%" AllowFilteringByColumn="true"
                                                    OnDeleteCommand="RadGrid10_DeleteCommand" AllowSorting="true" Skin="Outlook"
                                                    MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                                                    MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                    MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                    ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                    ClientSettings-ReorderColumnsOnClient="true">
                                                    <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="None" DataKeyNames="id">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>

                                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="Bank Info" Display="false">
                                                                <ItemTemplate>
                                                                    <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10px" />
                                                            </radG:GridTemplateColumn>

                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                                UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="FromBank" DataType="System.Int32"
                                                                UniqueName="FromBank" Visible="true" SortExpression="FromBank" HeaderText="FromBank">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="FromBankAcNo" DataType="System.Int32"
                                                                UniqueName="FromBankAcNo" Visible="true" SortExpression="FromBankAcNo" HeaderText="FromBankAcNo">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="ToBank" DataType="System.Int32"
                                                                UniqueName="ToBank" Visible="true" SortExpression="ToBank" HeaderText="ToBank">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Giro_Acct_Number" DataType="System.Int32"
                                                                UniqueName="Giro_Acct_Number" Visible="true" SortExpression="Giro_Acct_Number"
                                                                HeaderText="Giro_Acct_Number">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Giro_Branch" DataType="System.Int32"
                                                                UniqueName="Giro_Branch" Visible="true" SortExpression="Giro_Branch" HeaderText="Giro_Branch">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Giro_Acc_Name" DataType="System.Int32"
                                                                UniqueName="Giro_Acc_Name" Visible="true" SortExpression="Giro_Acc_Name" HeaderText="Giro_Acc_Name">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Percentage" DataType="System.Int32"
                                                                UniqueName="Percentage" Visible="true" SortExpression="Percentage" HeaderText="%Age">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Currency" UniqueName="CurrencyBank"
                                                                Visible="true" SortExpression="CurrencyBank" HeaderText="Currency">
                                                            </radG:GridBoundColumn>
                                                            <%-- <radG:GridBoundColumn ReadOnly="True" DataField="PaymentPart" UniqueName="PaymentPart"
                                                                Visible="true" SortExpression="PaymentPart" HeaderText="PaymentMode">
                                                            </radG:GridBoundColumn>--%>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Remarks" DataType="System.Int32"
                                                                UniqueName="Remarks" Visible="true" SortExpression="Remarks" HeaderText="Remarks">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                                UniqueName="DeleteColumn">
                                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                            </radG:GridButtonColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                            AllowColumnResize="False"></Resizing>
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageJob" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Job Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Joining Date</label>
                                                    <radCln:RadDatePicker Width="160px" Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdJoiningDate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="OnDateSelected2" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Probation Period [ Month(s) ]</label>
                                                    <asp:DropDownList ID="cmbprobation" class="textfields form-control input-sm" runat="Server">
                                                        <asp:ListItem Value="-1">-select-</asp:ListItem>
                                                        <asp:ListItem Value="1"> 1 </asp:ListItem>
                                                        <asp:ListItem Value="2"> 2 </asp:ListItem>
                                                        <asp:ListItem Value="3"> 3 </asp:ListItem>
                                                        <asp:ListItem Value="4"> 4 </asp:ListItem>
                                                        <asp:ListItem Value="5"> 5 </asp:ListItem>
                                                        <asp:ListItem Value="6"> 6 </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required"></tt>Confirmation Date</label>


                                                    <radCln:RadDatePicker Width="160px" Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdConfirmationDate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Termination/Resignation Date</label>
                                                    <radCln:RadDatePicker Width="160px" Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdTerminationDate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                        <ClientEvents OnDateSelected="OnDateSelected1" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Termination/Resignation Reason</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" maxlength="50" id="txtterminreason" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Birth Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdDOB" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>

                                                <span style="display: none">
                                                    <input disabled="disabled" type="text" class="textfields" id="txtRemainingLeaves"
                                                        runat="server" style="width: 88px" /></span>

                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Other Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageDepartment.aspx">Department</a></label>
                                                    <select id="cmbDepartment" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageTrade.aspx">Trade</a></label>
                                                    <select id="cmbTrade" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageDesignation.aspx">Designation</a></label>
                                                    <select id="cmbDesignation" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                    <asp:TextBox Visible="false" ID="txtRosterAssigned" CssClass="textfields form-control input-sm" Enabled="false" runat="server"
                                                        ReadOnly="true" Text=""></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Highest Education</label>
                                                    <select id="cmbeducation" runat="server" class="textfields form-control input-sm">
                                                        <option value=" ">-select-</option>
                                                        <option value="O Level">O Level</option>
                                                        <option value="A Level">A Level</option>
                                                        <option value="Diploma">Diploma</option>
                                                        <option value="Graduate">Graduate</option>
                                                        <option value="Masters">Masters</option>
                                                        <option value="Phd">Phd</option>
                                                        <option value="Others">Others</option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Alert Supervisor</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave Supervisor</label>
                                                    <select id="cmbsupervisor" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claim Supervisor</label>
                                                    <select id="cmbclaimsupervisor" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Supervisor</label>
                                                    <select id="cmbtimesheetsupervisor" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(D) Workflow</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave</label>
                                                    <select id="cmbLeaveApproval" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Cliam</label>
                                                    <select id="cmbCliamApproval" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>TimeSheet</label>
                                                    <select id="cmbTimesheetApproval" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll</label>
                                                    <select id="cmbpayrollsupervisor" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Appraisal</label>
                                                    <select id="cmbAppraisalApproval" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(E) Costing</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Business Unit</label>
                                                    <select id="cmbbusinessunit" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Region</label>
                                                    <select id="cmbRegion" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Cost Category</label>
                                                    <select id="cmbCategory" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                        </div>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageForeignWorker" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Work Permit Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Work Permit Number</label>
                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtWPNumber" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Application Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdwpappdate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Issue Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        ID="rdwpissuedate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        AllowEmpty="true" ID="rdWPExpDate" Visible="false" runat="server">
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Date of Arrival</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900"
                                                        AllowEmpty="true" ID="rdWPArrDate" runat="server">
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radCln:RadDatePicker>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>MYE Certificate</label>

                                                    <select id="cmbMYE" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Monthly Levy</label>

                                                    <input type="text" id="txtMonthlyLevy" runat="server" class="textfields form-control input-sm text-right number-dot MonthlyLevy" data-type="currency" maxlength="12" />
                                                </div>
                                            </div>


                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Worker Levy</label>

                                                    <select id="cmblevy" runat="server" class="textfields form-control input-sm">
                                                        <option value="high">High</option>
                                                        <option value="low">Low</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>FWL Code</label>

                                                    <input type="text" id="txtFWLCode" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="50" />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Other Information</h4>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Shipyard Quota</label>

                                                    <input type="text" class="textfields form-control input-sm custom-maxlength" id="txtShipyardQuota" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Batch No</label>

                                                    <input type="text" class="textfields form-control input-sm numericonly" id="txtBatchNo" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageAgent.aspx">Agent Name</a></label>

                                                    <select id="cmbAgent" runat="server" class="textfields form-control input-sm">
                                                        <option selected="selected"></option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Photo No</label>

                                                    <input type="text" class="textfields form-control input-sm numericonly" id="txtPhotoNo" maxlength="50" runat="server" />
                                                </div>
                                            </div>

                                        </div>

                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageLeaveHistory" runat="server">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="drpYear" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                    AutoPostBack="true" OnSelectedIndexChanged="drpYear_SelectedIndexChanged" runat="server"
                                                    AppendDataBoundItems="true" CssClass="form-control input-sm input-small input-inline">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                                <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                                <asp:Button ID="btnLeaveUpdate" CssClass="btn btn-sm default" runat="server" Text="Update" OnClick="btnLeaveUpdate_Click" />
                                            </div>
                                            <div class="col-sm-2">
                                                Joining Date
                                               
                                                <asp:Label ID="lblJoining" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                Confirm Date
                                               
                                                <asp:Label ID="lblConfirm" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                Leave Model
                                               
                                                <asp:Label ID="lblLeaveModel" runat="server" Text=""></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                                Work Days in Week
                                               
                                                <asp:Label ID="lblWorkdays" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>

                                        <hr />

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
                                                                <li>*CAL=Company Allowed Leave</li>
                                                                <li>*LYL=Last Year</li>
                                                                <li>*CYL=Current Year Leave</li>
                                                                <li>*CMLE=Current Month Leave Earned</li>
                                                            </ul>
                                                            <span id="fullDay_span" visible="false" runat="server" class="bodytxt">
                                                                <input type="checkbox" id="FullDayLeaveDeductionID" runat="server" />
                                                                Full Day Leave Deduction </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <radG:RadGrid ID="RadGrid7" runat="server" Width="100%" AllowFilteringByColumn="true"
                                                    AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                                    MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                                    MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                                    GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                                    ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                                    OnItemCommand="RadGrid7_ItemCommand" OnItemDataBound="RadGrid7_ItemDataBound"
                                                    OnItemCreated="RadGrid7_ItemCreated">
                                                    <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="None" DataKeyNames="id, LeavesAllowed, LY_Leaves_Bal">
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

                                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                                UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Leave Type">
                                                                <ItemStyle Width="150px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="*CAL" UniqueName="CLColumn">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCLA" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyLeaveAllowed")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="*LYL" UniqueName="LYColumn">
                                                                <ItemTemplate>
                                                                    <asp:TextBox Style="text-align: right" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                                        ID="txtLY" runat="server" Visible='<%# Eval("ShowLeaveAllowed").ToString() == "0" && Eval("ID").ToString() == "8" ? true : false %>'
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "LY_Leaves_Bal")%>' CssClass="form-control input-sm text-right"></asp:TextBox>
                                                                    <asp:Label Visible='<%# Eval("ShowLeaveAllowed").ToString() == "0" && Eval("ID").ToString() == "8" ? false : true %>'
                                                                        ID="lblLY" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LY_Leaves_Bal")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="*CYL" UniqueName="LAColumn">
                                                                <ItemTemplate>
                                                                    <asp:TextBox Style="text-align: right" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                                        ID="txtLA" runat="server" Visible='<%# Eval("ShowLeaveAllowed").ToString() == "1" ? false : true %>'
                                                                        Text='<%# DataBinder.Eval(Container.DataItem, "LeavesAllowed")%>' CssClass="form-control input-sm text-right"></asp:TextBox>
                                                                    <asp:Label Visible='<%# Eval("ShowLeaveAllowed").ToString() == "1" ? true : false %>'
                                                                        Style="text-align: right" ID="lblChildCare" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LeavesAllowed")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Paid Leave">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PaidLeaves")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Unpaid Leave">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnpaidLeaves")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Pending Leave">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPending" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PendingLeaves")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Leave Taken">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotLeave" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalLeavesTaken")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Balance Leave">
                                                                <%-- <ItemTemplate>
                                                                                        <asp:Label ID="lblLA" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualLeavesAvailable")%>'></asp:Label>
                                                                                    </ItemTemplate>--%>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLA" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "actualleavesavailable")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="true" HorizontalAlign="right" />
                                                                <ItemStyle Font-Bold="true" HorizontalAlign="right" Width="55px" />
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="" Display="True">
                                                                <ItemTemplate>
                                                                    <asp:Button CommandName="History" ID="btnHistory" runat="server" Text="Detail" Visible='<%# Eval("ID").ToString() != "8" ? false : true %>' />
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="center" />
                                                                <ItemStyle HorizontalAlign="center" Width="25px" />
                                                            </radG:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                            AllowColumnResize="False"></Resizing>
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <radG:RadGrid ID="RadGrid8" runat="server" Width="95%" AllowFilteringByColumn="true"
                                                    AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                                    MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                                    MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                                    GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                                    ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                                    OnItemDataBound="RadGrid8_ItemDataBound">
                                                    <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="None" DataKeyNames="MonthID">
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

                                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="MonthID" DataType="System.Int32"
                                                                UniqueName="MonthID" Visible="true" SortExpression="MonthID" HeaderText="Month">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="MthName" DataType="System.String" UniqueName="MthName"
                                                                SortExpression="MthName" HeaderText="Month">
                                                                <HeaderStyle HorizontalAlign="left" />
                                                                <ItemStyle HorizontalAlign="left" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="CompanyLeaveAllowed" DataType="System.Double" UniqueName="CompanyLeaveAllowed"
                                                                SortExpression="CompanyLeaveAllowed" HeaderText="*CAL">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="75px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="LYL" DataType="System.Double" UniqueName="LYL" SortExpression="LYL"
                                                                HeaderText="*LYL">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="75px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="CMLE" DataType="System.Double" UniqueName="CMLE"
                                                                SortExpression="CMLE" HeaderText="*CMLE">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="75px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="CurrentLeaveEarned" DataType="System.Double" UniqueName="CurrentLeaveEarned"
                                                                SortExpression="CurrentLeaveEarned" HeaderText="*CYLE">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="75px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="PaidLeaves" DataType="System.Double" UniqueName="PaidLeaves"
                                                                SortExpression="PaidLeaves" HeaderText="Paid Leave">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="75px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="UnPaidLeaves" DataType="System.Double" UniqueName="UnPaidLeaves"
                                                                SortExpression="UnPaidLeaves" HeaderText="Unpaid Leave">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="80px" />
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn DataField="LeaveAvailable" DataType="System.Double" UniqueName="LeaveAvailable"
                                                                SortExpression="LeaveAvailable" HeaderText="Balance Leave">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" Width="95px" />
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                            AllowColumnResize="False"></Resizing>
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <radG:RadGrid ID="RadGrid9" runat="server" Width="75%" AllowFilteringByColumn="true"
                                                    Visible="false" DataSourceID="SqlDataSource13" AllowSorting="true" Skin="Outlook"
                                                    MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                                                    MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                                    MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                                    ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                                    ClientSettings-ReorderColumnsOnClient="true">
                                                    <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="None" DataKeyNames="id"
                                                        DataSourceID="SqlDataSource13">
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

                                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                                UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="Actual_YOS" DataType="System.Int32"
                                                                UniqueName="Actual_YOS" Visible="true" SortExpression="Actual_YOS" HeaderText="Year In Service">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="YOSCAL" DataType="System.Int32"
                                                                UniqueName="YOSCAL" Visible="true" SortExpression="YOSCAL" HeaderText="*CAL">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="StartDate" DataType="System.dateTime"
                                                                UniqueName="StartDate" Visible="true" SortExpression="StartDate" HeaderText="Start Date">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="TodayDate" DataType="System.dateTime"
                                                                UniqueName="TodayDate" Visible="true" SortExpression="TodayDate" HeaderText="Today Date">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn ReadOnly="True" DataField="EndDate" DataType="System.dateTime"
                                                                UniqueName="EndDate" Visible="true" SortExpression="EndDate" HeaderText="End Date">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="LY Leave" UniqueName="LYColumn">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox Style="text-align: right" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                                        ID="txtLY" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LY_Leaves_Bal")%>'
                                                                        Width="50px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                            <radG:GridTemplateColumn AllowFiltering="false" HeaderText="Current Leave" UniqueName="LAColumn">
                                                                <HeaderStyle HorizontalAlign="right" />
                                                                <ItemStyle HorizontalAlign="right" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox Style="text-align: right" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                                        ID="txtYOS" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LeavesAllowed")%>'
                                                                        Width="50px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </radG:GridTemplateColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                            AllowColumnResize="False"></Resizing>
                                                    </ClientSettings>
                                                </radG:RadGrid>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:Button ID="btnYOSUpdate" runat="server" Text="Update Year in Service Annual Leave" OnClick="btnYOSUpdate_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageFamily" runat="server" Width="100%">

                                        <%--<table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">
                                                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top">--%>
                                        <div class="clearfix heading-box heading-box-showhide">
                                            <div class="col-md-12">
                                                <radG:RadToolBar ID="Radtoolbar1" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"
                                                    CssClass="smallToolBar" OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid"
                                                    BorderWidth="0px">
                                                    <Items>
                                                        <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                        <radG:RadToolBarButton runat="server" CommandName="Print"
                                                            Height="25px" Text="Print" ToolTip="Print">
                                                        </radG:RadToolBarButton>
                                                        <%--<radG:RadToolBarButton IsSeparator="true">
                                                                                    </radG:RadToolBarButton>--%>
                                                        <%--<radG:RadToolBarButton runat="server" Text="">
                                                    <ItemTemplate>
                                                        <div>
                                                            <table cellpadding="0" cellspacing="0" border="0" >
                                                                <tr>
                                                                    <td class="bodytxt" valign="middle" style="height:30px">
                                                                        &nbsp;Export To:</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>--%>
                                                        <radG:RadToolBarButton runat="server" CommandName="Excel"
                                                            Height="25px" Text="Excel" ToolTip="Excel">
                                                        </radG:RadToolBarButton>
                                                        <radG:RadToolBarButton runat="server" CommandName="Word"
                                                            Height="25px" Text="Word" ToolTip="Word">
                                                        </radG:RadToolBarButton>
                                                    </Items>
                                                </radG:RadToolBar>
                                            </div>
                                        </div>

                                        <radG:RadGrid ID="RadGrid4" runat="server" AllowFilteringByColumn="true" AllowSorting="true"
                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-EnableRowHoverStyle="true"
                                            ClientSettings-ReorderColumnsOnClient="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowAutomaticUpdates="true"
                                            MasterTableView-AllowMultiColumnSorting="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-CommandItemDisplay="bottom" OnInsertCommand="RadGrid4_ItemInserted"
                                            OnGridExporting="RadGrid4_GridExporting" OnUpdateCommand="RadGrid4_ItemUpdated"
                                            Skin="Outlook" Width="100%" OnDeleteCommand="RadGrid4_DeleteCommand">
                                            <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="Bottom" DataKeyNames="id"
                                                GridLines="Both">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="Family Info" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                                        HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtName" MaxLength="50" CssClass="custom-maxlength alphabetsonly form-control clsfamilyname" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name")%>'></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                                                Display="None" ErrorMessage="Please Enter Name." SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" Width="20%" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="Relation" DataSourceID="xmldtRelation"
                                                        HeaderText="Relation" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnRelation">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="2" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                                        HeaderText="Date of Birth">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDateOfBirth" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfBirth")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <radG:RadDatePicker Calendar-ShowRowHeaders="false" ValidationGroup="ValidationSummary1"
                                                                MinDate="01-01-1900" ID="rdDateOfBirth" DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "DateOfBirthCopy")%>'
                                                                runat="server">
                                                                <Calendar runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="clsdatechk clsdate" />
                                                            </radG:RadDatePicker>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvDateOfBirth" runat="server" ControlToValidate="rdDateOfBirth"
                                                                Display="None" ErrorMessage="Please Enter Date of Birth." SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                            <asp:CustomValidator ValidateEmptyText="true" ControlToValidate="rdDateOfBirth" ID="rfvDateOfBirth" runat="server" ClientValidationFunction="customvalidate"
                                                                Display="None" ErrorMessage="Please Enter Date of Birth." SetFocusOnError="True">

                                                            </asp:CustomValidator>
                                                        </EditItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                                        HeaderText="Date of Marriage">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMarriage_Date" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Marriage_Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdMarriage_Date"
                                                                DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "Marriage_DateCopy")%>'
                                                                runat="server">
                                                                <Calendar runat="server">
                                                                    <SpecialDays>
                                                                        <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                        </telerik:RadCalendarDay>
                                                                    </SpecialDays>
                                                                </Calendar>
                                                                <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                            </radG:RadDatePicker>
                                                        </EditItemTemplate>
                                                        <ItemStyle Width="15%" HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="Sex" DataSourceID="xmldtSex"
                                                        HeaderText="Sex" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnSex">
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="2" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                                        HeaderText="Phone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phone")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtPhone" MaxLength="10" CssClass="form-control input-sm numericonly custom-maxlength txtphone" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Phone")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true"
                                                        HeaderText="ID NO">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUIDN" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UIDN")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUIDN" MaxLength="50" CssClass="form-control input-sm custom-maxlength" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UIDN")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="Status" DataSourceID="xmldtNat"
                                                        HeaderText="Status" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnStatus">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="2" Display="false">
                                                        <ItemTemplate>
                                                            &nbsp;
                                                       
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lbl1" runat="server" Height="25px"></asp:Label>
                                                        </EditItemTemplate>
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    </radG:GridEditCommandColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings ColumnNumber="3">
                                                    <FormTableItemStyle HorizontalAlign="left"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                        Height="110px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" HorizontalAlign="left"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="PushButton" InsertText="Add" UpdateText="Update" UniqueName="EditCommandColumn1"
                                                        CancelText="Cancel">
                                                    </EditColumn>

                                                    <FormTableButtonRowStyle HorizontalAlign="Center" CssClass="EditFormButtonRow "></FormTableButtonRowStyle>
                                                </EditFormSettings>
                                                <CommandItemSettings AddNewRecordText="Add New Family Information" />
                                            </MasterTableView>
                                            <ValidationSettings CommandsToValidate="PerformInsert,Update"></ValidationSettings>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                    AllowColumnResize="False"></Resizing>
                                            </ClientSettings>
                                        </radG:RadGrid>

                                        <%--</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageCertificate" runat="server">

                                        <%--<table class="tbl" border="0" width="100%">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">--%>
                                        <div class="clearfix heading-box heading-box-showhide">
                                            <div class="col-md-12">
                                                <radG:RadToolBar ID="Radtoolbar2" runat="server" Width="98%" Skin="Office2007" UseFadeEffect="True"
                                                    CssClass="smallToolBar" OnButtonClick="tbCertificateRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid1"
                                                    BorderWidth="0px">
                                                    <Items>
                                                        <%--   <radG:RadToolBarButton runat="server" CommandName="Add" ImageUrl="../Frames/Images/New.gif" Text="Add" ToolTip="Add"></radG:RadToolBarButton>
                                                        <radG:RadToolBarButton IsSeparator="true"></radG:RadToolBarButton>--%>
                                                        <radG:RadToolBarButton runat="server" CommandName="Print"
                                                            Height="25px" Text="Print" ToolTip="Print">
                                                        </radG:RadToolBarButton>
                                                        <%--<radG:RadToolBarButton IsSeparator="true">
                                                                        </radG:RadToolBarButton>--%>
                                                        <%--  <radG:RadToolBarButton runat="server" Text="">
                                                    <ItemTemplate>
                                                        <div>
                                                            <table cellpadding="0" cellspacing="0" border="0" >
                                                                <tr>
                                                                    <td class="bodytxt" valign="middle" style="height:30px">
                                                                        &nbsp;Export To:</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </radG:RadToolBarButton>--%>
                                                        <radG:RadToolBarButton runat="server" CommandName="Excel"
                                                            Height="25px" Text="Excel" ToolTip="Excel">
                                                        </radG:RadToolBarButton>
                                                        <radG:RadToolBarButton runat="server" CommandName="Word"
                                                            Height="25px" Text="Word" ToolTip="Word">
                                                        </radG:RadToolBarButton>
                                                    </Items>
                                                </radG:RadToolBar>
                                            </div>
                                        </div>

                                        <%-- <radG:RadGrid ID="RadGrid5" runat="server" Width="98%" OnDeleteCommand="RadGrid5_DeleteCommand"
                                            OnItemDataBound="RadGrid5_ItemDataBound" 
                                            AllowFilteringByColumn="true" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                            OnGridExporting="RadGrid5_GridExporting" ClientSettings-AllowColumnsReorder="true"
                                            ClientSettings-ReorderColumnsOnClient="true">--%>
                                        <radG:RadGrid ID="RadGrid5" runat="server" AllowFilteringByColumn="true" OnItemDataBound="RadGrid5_ItemDataBound"
                                            OnUpdateCommand="RadGrid5_UpdateCommand" OnDeleteCommand="RadGrid5_DeleteCommand" OnInsertCommand="RadGrid5_InsertCommand"
                                            GridLines="None"
                                            Skin="Outlook" Width="98%" EnableHeaderContextMenu="true" AllowMultiRowSelection="true">
                                            <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="Bottom" DataKeyNames="id"
                                                GridLines="Both" AutoGenerateColumns="false">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="Certificate Info" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="CertificateCategoryID"
                                                        DataSourceID="SqlDataSource5" HeaderText="Category Name" ListTextField="Category_Name"
                                                        ListValueField="ID" UniqueName="drpCategoryName">
                                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="CertificateNumber" UniqueName="CertificateNumber"
                                                        SortExpression="CertificateNumber" HeaderText="Certificate Number">
                                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDateTimeColumn EditFormColumnIndex="0" DataField="TestDate" HeaderText="Test Date"
                                                        SortExpression="TestDate" UniqueName="TestDate" DataFormatString="{0:d}" PickerType="DatePicker"
                                                        DataType="System.DateTime">
                                                    </radG:GridDateTimeColumn>
                                                    <radG:GridDateTimeColumn EditFormColumnIndex="1" DataField="IssueDate" HeaderText="Issue Date"
                                                        SortExpression="IssueDate" UniqueName="IssueDate" DataFormatString="{0:d}" PickerType="DatePicker"
                                                        DataType="System.DateTime">
                                                    </radG:GridDateTimeColumn>
                                                    <radG:GridDateTimeColumn EditFormColumnIndex="0" DataField="ExpiryDate" HeaderText="Expiry Date"
                                                        SortExpression="ExpiryDate" UniqueName="ExpiryDate" DataFormatString="{0:d}"
                                                        PickerType="DatePicker" DataType="System.DateTime">
                                                    </radG:GridDateTimeColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="IssueLocation" UniqueName="IssueLocation"
                                                        SortExpression="IssueLocation" HeaderText="Issue Location">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="IssuedBy" UniqueName="IssuedBy"
                                                        SortExpression="IssuedBy" HeaderText="Issued By">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Remarks" UniqueName="Remarks"
                                                        SortExpression="Remarks" HeaderText="Remarks">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridTemplateColumn UniqueName="filename" HeaderText="File Name" DataField="filename">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="h22" Target="_blank" runat="server" Text='<%# Bind("filename") %>'>      
                                                            </asp:HyperLink>
                                                        </ItemTemplate>


                                                    </radG:GridTemplateColumn>

                                                    <%--                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="Active"
                                                            DataSourceID="SqlDataSource_ActiveInactive" HeaderText="Active" ListTextField="Text"
                                                            ListValueField="value" UniqueName="drpActive">
                                                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                        </radG:GridDropDownColumn>--%>
                                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                        <ItemStyle Width="50px" />
                                                    </radG:GridEditCommandColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>
                                                <ExpandCollapseColumn Visible="False">
                                                    <HeaderStyle Width="19px" />
                                                </ExpandCollapseColumn>
                                                <RowIndicatorColumn Visible="False">
                                                    <HeaderStyle Width="20px" />
                                                </RowIndicatorColumn>
                                                <EditFormSettings UserControlName="certificate_edit.ascx" EditFormType="WebUserControl">
                                                </EditFormSettings>
                                                <CommandItemSettings AddNewRecordText="Add New" />
                                                <CommandItemSettings AddNewRecordText="Add New Certificate" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                    AllowColumnResize="False"></Resizing>
                                            </ClientSettings>
                                        </radG:RadGrid>


                                        <%--</td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>

                                        <asp:SqlDataSource runat="server" ID="SqlDataSource_ActiveInactive" SelectCommand="select '1' as value, 'Active' as Text union select '0' as value, 'InActive' as Text "></asp:SqlDataSource>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageSaftety" runat="server">

                                        <%--<table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">--%>

                                        <radG:RadGrid ID="RadGrid2" Width="90%" runat="server" OnDeleteCommand="RadGrid2_DeleteCommand"
                                            OnItemDataBound="RadGrid2_ItemDataBound" OnItemCommand="RadGrid2_ItemCommand"
                                            AllowFilteringByColumn="true" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true">
                                            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="id" AutoGenerateColumns="False">
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="Safety Pass" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn DataField="id" DataType="System.Int32" Visible="false" HeaderText="Id"
                                                        ReadOnly="True" SortExpression="id" UniqueName="id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="safetypass_id" DataSourceID="SqlDataSource11" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        HeaderText="Safety Type" ListTextField="Safety_Type" ListValueField="ID" UniqueName="drpSPass" AutoPostBackOnFilter="true">
                                                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="safetypass_sno" UniqueName="safetypass_sno" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        SortExpression="safetypass_sno" HeaderText="Safety Pass No" AutoPostBackOnFilter="true">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDateTimeColumn EditFormColumnIndex="2" AllowFiltering="false" DataField="safetypass_expiry"
                                                        HeaderText="Expiry Date" SortExpression="safetypass_expiry" UniqueName="safetypass_expiry"
                                                        DataFormatString="{0:d}" PickerType="DatePicker" DataType="System.DateTime">
                                                        <ItemStyle Width="15%" HorizontalAlign="Left" />
                                                    </radG:GridDateTimeColumn>
                                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                        <ItemStyle Width="20px" />
                                                    </radG:GridEditCommandColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings ColumnNumber="3">
                                                    <FormTableItemStyle HorizontalAlign="left"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                        Height="110px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" HorizontalAlign="left"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="PushButton" InsertText="Add" UpdateText="Update" UniqueName="EditCommandColumn1"
                                                        CancelText="Cancel">
                                                    </EditColumn>
                                                    <FormTableButtonRowStyle HorizontalAlign="Center" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                </EditFormSettings>
                                                <CommandItemSettings AddNewRecordText="Add New Safety Pass" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                    AllowColumnResize="False"></Resizing>
                                            </ClientSettings>
                                        </radG:RadGrid>

                                        <%--</td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tabPageTraining" runat="server">

                                        <%--<table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">--%>

                                        <radG:RadGrid ID="RadGrid1" Width="100%" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                            OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand"
                                            AllowFilteringByColumn="true" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true">
                                            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="id" AutoGenerateColumns="False">
                                                <Columns>
                                                    <radG:GridBoundColumn DataField="id" DataType="System.Int32" Visible="false" HeaderText="Id"
                                                        ReadOnly="True" SortExpression="id" UniqueName="id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="courseid" DataSourceID="SqlDataSource9" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        HeaderText="Course Name" ListTextField="CourseName" ListValueField="ID" UniqueName="drpCourseName">
                                                        <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="result" UniqueName="result" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        SortExpression="result" HeaderText="Result">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="2" DataField="venue" UniqueName="venue" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        SortExpression="Venue" HeaderText="Venue">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDateTimeColumn AllowFiltering="false" EditFormColumnIndex="0" DataField="course_date"
                                                        HeaderText="Course Date" SortExpression="Course_Date" UniqueName="course_date"
                                                        DataFormatString="{0:d}" PickerType="DatePicker" DataType="System.DateTime">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridDateTimeColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="no_of_attempts" UniqueName="no_of_attempts" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        SortExpression="No_Of_Attempts" HeaderText="No Of Attempts">
                                                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                        <ItemStyle Width="20px" />
                                                    </radG:GridEditCommandColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle Width="20px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings ColumnNumber="3">
                                                    <FormTableItemStyle HorizontalAlign="left"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                        Height="110px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" HorizontalAlign="left"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="PushButton" InsertText="Add" UpdateText="Update" UniqueName="EditCommandColumn1"
                                                        CancelText="Cancel">
                                                    </EditColumn>
                                                    <FormTableButtonRowStyle HorizontalAlign="Center" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                </EditFormSettings>
                                                <CommandItemSettings AddNewRecordText="Add New Training" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                    AllowColumnResize="False"></Resizing>
                                            </ClientSettings>
                                        </radG:RadGrid>

                                        <%--</td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageView1" runat="server">
                                        <table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%"></td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>

                                    <telerik:RadPageView ID="tbsEmpPayHistory" runat="server">

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Pay History Information</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Effective Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdFrom"
                                                        runat="server">
                                                        <DateInput CssClass="clsdate" Skin="" DateFormat="dd/MM/yyyy" />
                                                        <Calendar runat="server">
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </radCln:RadDatePicker>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Pay Frequency</label>
                                                    <select id="drpPayFrequency" runat="server" class="textfields form-control input-sm"
                                                        onchange="ValidatePayRecord()">
                                                        <option value="M">Monthly</option>
                                                        <option value="H">Hourly</option>
                                                        <option value="D">Daily</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Trade</label>
                                                    <select id="drpTrade" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>No of Work Days:</label>
                                                    <select id="drpworkingdays" runat="server" class="textfields form-control input-sm" onchange="CHK_WorkingDaysCust(this);">
                                                        <option value="5">5</option>
                                                        <option value="5.5">5.5</option>
                                                        <option value="6">6</option>
                                                        <option value="7">7</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageDepartment.aspx">Department</a></label>
                                                    <select id="drpDepartment" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>BasicPay/PayRate</label>
                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <asp:TextBox ID="custxtPayRate" runat="server" CssClass="textfields form-control input-sm text-right number-dot cleanstring" MaxLength="12"
                                                        onkeyup="javascript:CalculateHourlyRateCust(this);" onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                    <% }
                                                        else
                                                        { %>
                                                    <asp:TextBox ID="custxtPayRate1" runat="server" CssClass="textfields form-control input-sm text-right number-dot cleanstring" MaxLength="12"
                                                        Text="******" Enabled="false"></asp:TextBox>
                                                    <% } %>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Hourly Mode</label>
                                                    <select id="drpHourlyMode" runat="server" class="textfields form-control input-sm"
                                                        onchange="javascript:CHK_HourlyRateCust(this);">
                                                        <option value="A">Auto</option>
                                                        <option value="M">Manual</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Hourly Rate</label>
                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <input type="text" id="custxtHourlyRate" onkeyup="javascript:CalculateOTRate(this);"
                                                        name="custxtHourlyRate" class="textfields form-control input-sm text-right number-dot cleanstring" maxlength="12" runat="server"
                                                        onkeypress="return isNumericKeyStrokeDecimal(event)" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="Text" id="custxtHourlyRate1" name="custxtHourlyRat1" value="******"
                                                        disabled="disabled" class="textfields form-control input-sm text-right number-dot cleanstring" maxlength="12" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Daily Mode</label>
                                                    <select id="drpDailyMode" runat="server" class="textfields form-control input-sm"
                                                        onchange="javascript:CHK_DailyRateCust(this);">
                                                        <option value="A">Auto</option>
                                                        <option value="M">Manual</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><tt class="required">*</tt>Daily Rate</label>
                                                    <% if (bViewSalAllowed)
                                                        {%>
                                                    <input type="text" id="custxtDailyRate" name="custxtDailyRate"
                                                        class="textfields form-control input-sm text-right number-dot cleanstring" maxlength="12" runat="server" onkeypress="return isNumericKeyStrokeDecimal(event)" />
                                                    <% }
                                                        else
                                                        { %>
                                                    <input type="Text" id="custxtDailyRate1" name="custxtDailyRate1" value="******"
                                                        disabled="disabled" class="textfields form-control input-sm text-right number-dot cleanstring" maxlength="12" runat="server" />
                                                    <% } %>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><a href="../Management/ManageDesignation.aspx">Designation:</a></label>
                                                    <select id="drpDesignation" runat="server" class="textfields form-control input-sm">
                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>OT Entitled</label>
                                                    <select id="drpOTEntitled" runat="server" class="textfields form-control input-sm">
                                                        <option value="Y">Yes</option>
                                                        <option value="N">No</option>
                                                    </select>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <% if (Session["Country"].ToString() != "383")
                                                { %>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CPF Entitled</label>
                                                    <select id="drpCPFEntitlement" onchange="return cpf_change();" runat="server" class="textfields form-control input-sm">
                                                        <option value="Y">Yes</option>
                                                        <option value="N">No</option>
                                                    </select>
                                                </div>
                                            </div>
                                            <%} %>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <label>OT-1 Rate</label>
                                                            <asp:TextBox ID="custxtOT1Rate" Text="1.5" MaxLength="5" CssClass="textfields form-control input-sm cleanstring"
                                                                runat="server" onkeyup="javascript:CalculateOTRateCust(this);" onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label>OT-1</label>
                                                            <% if (bViewSalAllowed)
                                                                {%>
                                                            <asp:TextBox ID="txtManualOT1" runat="server" CssClass="textfields form-control input-sm cleanstring" MaxLength="5" onkeyup="javascript:CalculateOTRateManual(this);"
                                                                onkeypress="return isNumericKeyStrokeDecimal(event)"></asp:TextBox>
                                                            <% }
                                                                else
                                                                { %>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="textfields form-control input-sm cleanstring" MaxLength="5" Text="***"></asp:TextBox>
                                                            <% } %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <label>OT-2 Rate</label>
                                                            <asp:TextBox MaxLength="5" ID="custxtOT2Rate" Text="2" CssClass="textfields form-control input-sm cleanstring"
                                                                runat="server" onkeyup="javascript:CalculateOTRateCust(this);" onkeypress="return isNumericKeyStrokeDecimal(event)"> </asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <label>OT-2</label>
                                                            <% if (bViewSalAllowed)
                                                                {%>
                                                            <asp:TextBox ID="txtManualOT2" runat="server" CssClass="textfields form-control input-sm cleanstring" MaxLength="5" onkeyup="javascript:CalculateOTRateManual(this);"
                                                                onkeypress="return isNumericKeyStrokeDecimal(event)"></asp:TextBox>
                                                            <% }
                                                                else
                                                                { %>
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="textfields form-control input-sm cleanstring" MaxLength="5" Text="***"></asp:TextBox>
                                                            <% } %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Currency</label>
                                                    <asp:DropDownList ID="drpCurrency" class="textfields form-control input-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Progression Reason</label>
                                                    <asp:DropDownList ID="drpProgReason" class="textfields form-control input-sm" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-2">
                                                            <label>&nbsp;</label>
                                                            <div>
                                                                <label class="mt-checkbox mt-checkbox-outline">
                                                                    <%--<asp:CheckBox runat="server" ID="latenesschkbox"  />--%>
                                                                    <input type="checkbox" id="latenesschkbox" runat="server" />
                                                                    <span></span>
                                                                </label>
                                                            </div>

                                                        </div>
                                                        <div class="col-sm-10">
                                                            <label>Lateness Rate/HR</label>
                                                            <asp:TextBox class="textfields form-control input-sm text-right number-dot cleanstring" data-type="currency" MaxLength="12" runat="server" ID="LatenessTextBox"></asp:TextBox>
                                                        </div>
                                                    </div>




                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label runat="server" ID="CustomHour" Text="Custom Hour"></asp:Label></label>
                                                    <asp:TextBox class="textfields form-control input-sm" runat="server" ID="hourtextbox"
                                                        Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>
                                                        <asp:Label runat="server" ID="customRate" Text="Custom Hour Rate"></asp:Label></label>
                                                    <asp:TextBox runat="server" ID="hourrate" class="textfields form-control input-sm"
                                                        Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:TextBox Style="width: 90px" class="textfields form-control input-sm" runat="server" ID="TextBox3"
                                                    Visible="false"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12 text-center">
                                                <asp:Button ID="btnAddHistory" runat="server" CssClass="btn red" Text="Update Pay Record" OnClick="btnAddHistory_Click" />
                                            </div>
                                        </div>



                                        <radG:RadGrid ID="RadGrid6" runat="server" Width="100%" AllowFilteringByColumn="true"
                                            AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                            OnDeleteCommand="RadGrid6_DeleteCommand" OnItemDataBound="RadGrid6_ItemDataBound">
                                            <MasterTableView AllowFilteringByColumn="false" CommandItemDisplay="None" DataKeyNames="id">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="record" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="FromDateCopy" DataType="System.DateTime" UniqueName="FromDateCopy"
                                                        Visible="true" SortExpression="FromDateCopy" HeaderText="From Date">
                                                    </radG:GridBoundColumn>
                                                    <%--                                                        <radG:s DataField="ToDateCopy" DataType="System.DateTime" UniqueName="ToDateCopy"
                                                            Visible="true" SortExpression="ToDateCopy" HeaderText="To Date">
                                                        </radG:GridBoundColumn>
                                                    --%>
                                                    <%--                                                        <radG:GridBoundColumn DataField="ConfirmationDateCopy" DataType="System.DateTime"
                                                            UniqueName="ConfirmationDateCopy" Visible="true" SortExpression="ConfirmationDateCopy"
                                                            HeaderText="Confirm Date">
                                                        </radG:GridBoundColumn>
                                                    --%>
                                                    <radG:GridBoundColumn DataField="PayRate" DataType="System.String" UniqueName="PayRate"
                                                        Visible="true" SortExpression="PayRate" HeaderText="Basic">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Designation" DataType="System.String" UniqueName="Designation"
                                                        Visible="true" SortExpression="Designation" HeaderText="Designation">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="DeptName" DataType="System.String" UniqueName="DeptName"
                                                        Visible="true" SortExpression="DeptName" HeaderText="Department">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="OT_Entitlement" DataType="System.String" UniqueName="OT_Entitlement"
                                                        Visible="true" SortExpression="OT_Entitlement" HeaderText="OT">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="CPF_Entitlement" DataType="System.String" UniqueName="CPF_Entitlement"
                                                        Visible="true" SortExpression="CPF_Entitlement" HeaderText="CPF">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Ot1Rate" DataType="System.Double" UniqueName="Ot1Rate"
                                                        Visible="true" SortExpression="Ot1Rate" HeaderText="Ot-1 Rate">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Ot2Rate" DataType="System.Double" UniqueName="Ot2Rate"
                                                        Visible="true" SortExpression="Ot2Rate" HeaderText="Ot-2 Rate">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="WDays_Per_Week" DataType="System.Double" UniqueName="WDays_Per_Week"
                                                        Visible="true" SortExpression="WDays_Per_Week" HeaderText="Days Per Week">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Hourly_Rate" DataType="System.Double" UniqueName="Hourly_Rate"
                                                        Visible="true" SortExpression="Hourly_Rate" HeaderText="Hourly Rate">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Daily_Rate" DataType="System.Double" UniqueName="Daily_Rate"
                                                        Visible="true" SortExpression="Daily_Rate" HeaderText="Daily Rate">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Daily_Rate_Mode" DataType="System.String" UniqueName="Daily_Rate_Mode"
                                                        Visible="true" SortExpression="Daily_Rate_Mode" HeaderText="Daily Rate Mode">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                    <radG:GridBoundColumn DataField="Currency" DataType="System.String" UniqueName="Currency"
                                                        Visible="true" SortExpression="Currency" HeaderText="Currency">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="HistoryDetails" DataType="System.String" UniqueName="HistoryDetails"
                                                        Visible="true" SortExpression="HistoryDetails" HeaderText="HistoryDetails">
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                                <EditFormSettings ColumnNumber="2">
                                                    <FormTableItemStyle HorizontalAlign="left"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                        Height="110px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" HorizontalAlign="left"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="PushButton" InsertText="Add" UpdateText="Update" UniqueName="EditCommandColumn1"
                                                        CancelText="Cancel">
                                                    </EditColumn>
                                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                </EditFormSettings>
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="False"
                                                    AllowColumnResize="False"></Resizing>
                                            </ClientSettings>
                                        </radG:RadGrid>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tbsItemIss" runat="server">

                                        <%--<table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">
                                                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top">--%>

                                        <radG:RadGrid ID="radItemIssued" runat="server" OnDeleteCommand="radItemIssued_DeleteCommand"
                                            OnItemDataBound="radItemIssued_ItemDataBound" DataSourceID="SqlDataSource8" Width="95%"
                                            OnItemInserted="radItemIssued_ItemInserted" OnItemUpdated="radItemIssued_ItemUpdated"
                                            AllowFilteringByColumn="true" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                            ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true">
                                            <MasterTableView DataSourceID="SqlDataSource8" DataKeyNames="id">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="Item Issued" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="ItemID" DataSourceID="SqlDataSource7" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        HeaderText="Item Name" ListTextField="ItemName" ListValueField="ID" UniqueName="GridDropDownColumn">
                                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="SerialNumber" UniqueName="SerialNumber" ShowFilterIcon="false" FilterControlAltText="numericonly"
                                                        SortExpression="SerialNumber" HeaderText="Serial Number">
                                                        <ItemStyle Width="20%" CssClass="cleanstring" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Quantity" UniqueName="Quantity" ShowFilterIcon="false" FilterControlAltText="numericonly"
                                                        SortExpression="Quantity" HeaderText="Quantity">
                                                        <ItemStyle Width="10%" CssClass="numericonly" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="ItemReturn" DataSourceID="xmldtYesNo" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        HeaderText="Item Return" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumnYesNo">
                                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                                    </radG:GridDropDownColumn>
                                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Remarks" UniqueName="Remarks" ShowFilterIcon="false"
                                                        SortExpression="Remarks" HeaderText="Remarks" FooterStyle-CssClass="custom-maxlength">
                                                        <%--<EditItemTemplate>
                                                            <asp:TextBox ID="txtremark" MaxLength="50" CssClass=" custom-maxlength" runat="server" ></asp:TextBox>
                                                        </EditItemTemplate>--%>
                                                        <ItemStyle Width="30%" CssClass="custom-maxlength" HorizontalAlign="Left" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridTemplateColumn EditFormColumnIndex="2" Display="false" AllowFiltering="False"
                                                        UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            &nbsp;
                                                       
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblSt" runat="server" Text="&nbsp;">&nbsp;</asp:Label>
                                                        </EditItemTemplate>
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                        <ItemStyle Width="50px" CssClass="Updateitemissued" />
                                                    </radG:GridEditCommandColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        UniqueName="DeleteColumn">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>

                                                <EditFormSettings ColumnNumber="3">
                                                    <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                        Height="70px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                                    <EditColumn ButtonType="PushButton" InsertText="Add" UpdateText="Update" UniqueName="EditCommandColumn1"
                                                        CancelText="Cancel">
                                                    </EditColumn>
                                                    <FormTableButtonRowStyle HorizontalAlign="Center" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                </EditFormSettings>

                                                <CommandItemSettings AddNewRecordText="Add New Item Issued" />
                                            </MasterTableView>
                                            <ClientSettings>
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                <ClientEvents OnRowDblClick="RowDblClick" />
                                            </ClientSettings>
                                        </radG:RadGrid>

                                        <%--</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tbsFileUpload" runat="server">

                                        <%-- <table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">
                                                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td style="width: 20%"></td>
                                                                                    <td style="width: 30%"></td>
                                                                                    <td style="width: 40%"></td>
                                                                                    <td style="width: 10%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tdstand" colspan="4">(A) File Information
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandbottom">
                                                                                    <td>Category Name:
                                                                                    </td>
                                                                                    <td>&nbsp;Document Name:
                                                                                    </td>
                                                                                    <td>Select File:
                                                                                    </td>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSource1" DataTextField="Category_Name"
                                                                                            DataValueField="ID" Width="100%">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                    <td>&nbsp;<asp:TextBox ID="txtDocumentName" runat="server" Width="90%"></asp:TextBox>
                                                                                    </td>
                                                                                    <td>
                                                                                        <radU:RadUpload BackColor="transparent" BorderColor="transparent" EnableFileInputSkinning="false"
                                                                                            ID="file1" runat="server" InitialFileInputsCount="1" MaxFileInputsCount="1" OnClientClearing="myOnClientCleared"
                                                                                            ControlObjectsVisibility="none" Width="250px">
                                                                                        </radU:RadUpload>
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="button2" runat="server" CssClass="RadUploadSubmit" Text="Submit"
                                                                                            OnClick="buttonSubmit_Click" Style="margin-top: 6px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>--%>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) File Information</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <label>Category Name</label>
                                                <asp:DropDownList ID="ddlCategory" runat="server" DataSourceID="SqlDataSource1" DataTextField="Category_Name"
                                                    DataValueField="ID" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                <label>Document Name</label>
                                                <asp:TextBox ID="txtDocumentName" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <label>Select File</label>
                                                <radU:RadUpload BackColor="transparent" BorderColor="transparent" EnableFileInputSkinning="false"
                                                    ID="file1" runat="server" InitialFileInputsCount="1" MaxFileInputsCount="1" OnClientClearing="myOnClientCleared"
                                                    ControlObjectsVisibility="none" Width="250px">
                                                </radU:RadUpload>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="button2" runat="server" CssClass="RadUploadSubmit btn red" Text="Submit" OnClick="buttonSubmit_Click" />
                                            </div>
                                        </div>
                                        <hr />
                                        <radG:RadGrid ID="RadGrid3" runat="server" OnItemDataBound="RadGrid3_ItemDataBound"
                                            OnDeleteCommand="RadGrid3_DeleteCommand" DataSourceID="SqlDataSource2" Width="98%"
                                            AllowFilteringByColumn="true" AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="None"
                                            MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                            GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-AllowColumnsReorder="true"
                                            ClientSettings-ReorderColumnsOnClient="true">
                                            <MasterTableView DataSourceID="SqlDataSource2" DataKeyNames="id, FileName, Emp_Code">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>

                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderAbbr="File Info" Display="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>

                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                        UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                        UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="Category_Name" UniqueName="Category_Name" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        Visible="true" SortExpression="Category_Name" HeaderText="Category Name" AutoPostBackOnFilter="true">
                                                        <ItemStyle Width="30%" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="Document_Name" UniqueName="Document_Name" ShowFilterIcon="false" FilterControlAltText="cleanstring"
                                                        Visible="true" SortExpression="Document_Name" HeaderText="Document Name" AutoPostBackOnFilter="true">
                                                        <ItemStyle Width="30%" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="FileName" UniqueName="FileName"
                                                        Visible="true" SortExpression="FileName" HeaderText="Filename">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridTemplateColumn UniqueName="lnk" HeaderText="File Name" ShowFilterIcon="false" FilterControlAltText="cleanstring" AllowFiltering="false">
                                                        <ItemTemplate>
                                                            <asp:HyperLink Target="_blank" runat="server" ID="hlnFile" Text='<%# Bind("FileName") %>'>      
                                                            </asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80%" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                        HeaderText="Delete" UniqueName="DeleteColumn">
                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                    </radG:GridButtonColumn>
                                                </Columns>
                                                <EditFormSettings ColumnNumber="2">
                                                    <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                                        BackColor="White" Width="100%" />
                                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                                        Height="110px" BackColor="White" />
                                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                </EditFormSettings>
                                            </MasterTableView>
                                        </radG:RadGrid>


                                        <%--</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>--%>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" Visible='false' ID="tbsIR8A">
                                        <table class="tbl" border="0">
                                            <tr valign="top">
                                                <td>
                                                    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                        <tr>
                                                            <td style="width: 1%">&nbsp;
                                                            </td>
                                                            <td style="width: 98%">
                                                                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td style="width: 20%"></td>
                                                                                    <td style="width: 20%"></td>
                                                                                    <td style="width: 20%"></td>
                                                                                    <td style="width: 20%"></td>
                                                                                    <td style="width: 20%"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="tdstand" colspan="5">(A) IR8A Information
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandbottom">
                                                                                    <td>Year:
                                                                                    </td>
                                                                                    <td>Tax Borne Employer:
                                                                                    </td>
                                                                                    <td>Tax Borne Employer Options:
                                                                                    </td>
                                                                                    <td>Tax Borne Employer Amount:</td>
                                                                                    <td>Pension/PF outside Singapore:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandtop">
                                                                                    <td>
                                                                                        <select id="cmbIR8A_year" runat="server" class="textfields" style="width: 116px">
                                                                                            <option value="2007">2007</option>
                                                                                            <option value="2008">2008</option>
                                                                                            <option value="2008">2009</option>
                                                                                            <option value="2008">2010</option>
                                                                                            <%--<option value="2008">2011</option>
                                                                        <option value="2008">2012</option>
                                                                        <option value="2008">2013</option>
                                                                        <option value="2008">2014</option>
                                                                        <option value="2008">2015</option>
                                                                                            --%>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbtaxbornbyemployer" runat="server" name="cmbtaxbornbyemployer" class="textfields"
                                                                                            style="width: 116px" onchange="javascript:taxbornbyemployer();">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbtaxbornbyemployerFPHN" runat="server" class="textfields" style="width: 116px"
                                                                                            onchange="javascript:taxbornbyemployerFPHN();">
                                                                                            <option value="">Select</option>
                                                                                            <option value="F">F - Tax fully borne by employer on employment income only</option>
                                                                                            <option value="P">P - Tax partially borne by employer on certain employment income items</option>
                                                                                            <option value="H">H - A fixed amount of income tax liability borne by Employee</option>
                                                                                            <option value="N">N - Not applicable (default)</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input disabled="disabled" type="text" class="textfields" id="txttaxbornbyempamt"
                                                                                            runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbpensionoutsing" runat="server" name="cmbpensionoutsing" class="textfields"
                                                                                            style="width: 116px" onchange="javascript:EnableDisableandValue('cmbpensionoutsing','txtpensionoutsing');">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandbottom">
                                                                                    <td>Pension/PF outside Singapore Amount:
                                                                                    </td>
                                                                                    <td>Excess voluntary CPF employer:
                                                                                    </td>
                                                                                    <td>Excess voluntary cpf employer Amount:
                                                                                    </td>
                                                                                    <td>Stock options:</td>
                                                                                    <td>Stock options Amount:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandtop">
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="txtpensionoutsing" runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbexcessvolcpfemp" runat="server" name="cmbexcessvolcpfemp" class="textfields"
                                                                                            style="width: 116px" onchange="javascript:EnableDisableandValue('cmbexcessvolcpfemp','txtexcessvolcpfemp');">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="txtexcessvolcpfemp" runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbstockoption" runat="server" name="cmbstockoption" class="textfields"
                                                                                            style="width: 116px" onchange="javascript:EnableDisableandValue('cmbstockoption','txtstockoption');">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="txtstockoption" runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandbottom">
                                                                                    <td>Benefits in kind:
                                                                                    </td>
                                                                                    <td>Benefits in kind Amount:
                                                                                    </td>

                                                                                    <td>Retirement Benefits:
                                                                                    </td>
                                                                                    <td>Retirement Benefits fundname:</td>
                                                                                    <td>Retirement Benefits Amount:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandtop">
                                                                                    <td>
                                                                                        <select id="cmbbenefitskind" runat="server" name="cmbbenefitskind" class="textfields"
                                                                                            style="width: 116px" onchange="javascript:EnableDisableandValue('cmbbenefitskind','txtbenefitskind');">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="txtbenefitskind" runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbretireben" runat="server" name="cmbretireben" class="textfields" onchange="javascript:EnableDisableandValue('cmbretireben','txtretirebenfundname,txtbretireben');"
                                                                                            style="width: 116px">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input maxlength="200" type="text" class="textfields" id="txtretirebenfundname" runat="server"
                                                                                            style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="txtbretireben" runat="server" style="width: 110px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandbottom">
                                                                                    <td>S-45 Tax on Director Fee:
                                                                                    </td>
                                                                                    <td>Cessation Provision:
                                                                                    </td>
                                                                                    <td>Date of Cessation:
                                                                                    </td>
                                                                                    <td>Date of Commencement:</td>
                                                                                    <td>Address Type:
                                                                                    </td>
                                                                                </tr>
                                                                                <tr class="trstandtop">
                                                                                    <td>
                                                                                        <select id="staxondirector" runat="server" name="staxondirector" class="textfields"
                                                                                            style="width: 116px">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <select id="cmbcessprov" runat="server" name="cmbcessprov" class="textfields" onchange="javascript:EnableDisableandValue('cmbcessprov','dtcessdate,dtcommdate');"
                                                                                            style="width: 116px">
                                                                                            <option>No</option>
                                                                                            <option>Yes</option>
                                                                                        </select>
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="dtcessdate" maxlength="10" runat="server"
                                                                                            style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <input type="text" class="textfields" id="dtcommdate" maxlength="10" runat="server"
                                                                                            style="width: 110px" />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList ID="cmbaddress" class="textfields" runat="server" Width="116px">
                                                                                            <asp:ListItem Value="N" Text="No Address"></asp:ListItem>
                                                                                            <asp:ListItem Value="L" Text="Local Residential address"></asp:ListItem>
                                                                                            <asp:ListItem Value="F" Text="Foreign Address"></asp:ListItem>
                                                                                            <asp:ListItem Value="C" Text="C/O Address"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 1%">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView Visible="false" runat="server" ID="tbsIR8AApendix">
                                        <tr>
                                            <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server">
                                                <table border="0">
                                                    <tr valign="top">
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 1%">&nbsp;
                                                                    </td>
                                                                    <td style="width: 98%">
                                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 20%"></td>
                                                                                            <td style="width: 20%"></td>
                                                                                            <td style="width: 20%"></td>
                                                                                            <td style="width: 20%"></td>
                                                                                            <td style="width: 20%"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="tdstand" colspan="5">(A) IR8A Appendix Information
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td>1. <b>Value of the place of residence (See paragraph 14 of the Explanatory Notes ) :</b>
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td>Year:
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>
                                                                                                <select id="Select1" runat="server" class="textfields" style="width: 116px">
                                                                                                    <option value="2007">2007</option>
                                                                                                    <option value="2008">2008</option>
                                                                                                    <option value="2008">2009</option>
                                                                                                    <option value="2008">2010</option>
                                                                                                </select>
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td>Address
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td colspan="2" align="right">Period Of Occupation
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text2" runat="server" style="width: 210px" />
                                                                                            </td>
                                                                                            <td colspan="2"></td>
                                                                                            <td align="right">From:
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text1" runat="server" style="width: 110px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text3" runat="server" style="width: 210px" />
                                                                                            </td>
                                                                                            <td colspan="2"></td>
                                                                                            <td align="right">To:
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text104" runat="server" style="width: 110px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text105" runat="server" style="width: 210px" />
                                                                                            </td>
                                                                                            <td colspan="2"></td>
                                                                                            <td align="right">No. Of Days
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text106" runat="server" style="width: 110px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td align="left">2.<b>Value of Furniture/Driver/Gardener (Total of 2a to 2k):</b>
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td align="right"></td>
                                                                                            <td align="right"></td>
                                                                                            <td align="right"></td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td align="right">Furniture :
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td align="right">Number Of Units :
                                                                                            </td>
                                                                                            <td align="right">Rate Per Unit p.m($):
                                                                                            </td>
                                                                                            <td align="right">Value :
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Furniture Hard
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtFHard'),document.getElementById('txtCostHard'),document.getElementById('txtTotalHard'));"
                                                                                                    id="txtFHard" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostHard" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalHard" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Furniture Soft
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtFSoft'),document.getElementById('txtCostSoft'),document.getElementById('txtTotalSoft'));"
                                                                                                    id="txtFSoft" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostSoft" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalSoft" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Refrigirator
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtRef'),document.getElementById('txtCostRef'),document.getElementById('txtTotalRef'));"
                                                                                                    id="txtRef" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostRef" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalRef" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td colspan="2">Video Recorder/DVD/VCD Player
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtVcd'),document.getElementById('txtCostVCD'),document.getElementById('txtTotalVcd'));"
                                                                                                    id="txtVcd" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostVCD" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalVcd" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Washing Machine
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtWashingMachine'),document.getElementById('txtCostWashingMachine'),document.getElementById('txtTotalWashingMachine'));"
                                                                                                    id="txtWashingMachine" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostWashingMachine"
                                                                                                    runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalWashingMachine"
                                                                                                    runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Dryer
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtDryer'),document.getElementById('txtCostDryer'),document.getElementById('txtTotalDryer'));"
                                                                                                    id="txtDryer" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostDryer" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalDryer" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Dish Washer
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtDish'),document.getElementById('txtCostDish'),document.getElementById('txtTotalDish'));"
                                                                                                    id="txtDish" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostDish" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalDish" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Conditioner Unit
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAc'),document.getElementById('txtCostAc'),document.getElementById('txtTotalAc'));"
                                                                                                    id="txtAc" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostAc" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalAc" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Conditioner Central
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAcCentral'),document.getElementById('txtCostCentral'),document.getElementById('txtTotalCentral'));"
                                                                                                    id="txtAcCentral" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostCentral" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalCentral" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Conditioner Dining
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAcdining'),document.getElementById('txtCostAcdining'),document.getElementById('txtTotalAcdining'));"
                                                                                                    id="txtAcdining" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostAcdining" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalAcdining" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Conditioner Sitting
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtACsitting'),document.getElementById('txtCostACsitting'),document.getElementById('txtTotalACsitting'));"
                                                                                                    id="txtACsitting" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostACsitting" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalACsitting" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Conditioner Additional
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAcAdditional'),document.getElementById('txtCostAcAdditional'),document.getElementById('txtTotalAcAdditional'));"
                                                                                                    id="txtAcAdditional" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostAcAdditional" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalAcAdditional" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Air Purifier
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAirpurifier'),document.getElementById('txtCostAirpurifier'),document.getElementById('txtTotalAirpurifier'));"
                                                                                                    id="txtAirpurifier" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostAirpurifier" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalAirpurifier" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td colspan="2" style="vertical-align: middle; width: 80px" valign="middle">TV /Home Entertainment Theater/Plasma TV/High definition TV
                                                                                            </td>
                                                                                            <td valign="middle">
                                                                                                <input class="textfields" id="txtTV" onkeydown="javascript:computeTotal(document.getElementById('txtTV'),document.getElementById('txtCostTV'),document.getElementById('txtTotalTV'));"
                                                                                                    style="vertical-align: middle; width: 80px" name="Text43" value="" />
                                                                                            </td>
                                                                                            <td valign="middle">
                                                                                                <input class="textfields" id="txtCostTV" disabled="disabled" style="vertical-align: middle; width: 80px"
                                                                                                    name="Text44" value="" />
                                                                                            </td>
                                                                                            <td valign="middle">
                                                                                                <input class="textfields" id="txtTotalTV" disabled="disabled" style="vertical-align: middle; width: 80px"
                                                                                                    name="Text45" value="" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Radio
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtRadio'),document.getElementById('txtCostRadio'),document.getElementById('txtTotalRadio'));"
                                                                                                    id="txtRadio" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostRadio" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalRadio" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Hi-Fi Stereo
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtHifi'),document.getElementById('txtCostHifi'),document.getElementById('txtTotalHifi'));"
                                                                                                    id="txtHifi" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostHifi" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalHifi" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Electric Guitar
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtGuitar'),document.getElementById('txtCostGuitar'),document.getElementById('txtTotalGuitar'));"
                                                                                                    id="txtGuitar" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostGuitar" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalGuitar" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Surveillance System
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtSurveillance'),document.getElementById('txtCostSurveillance'),document.getElementById('txtTotalSurveillance'));"
                                                                                                    id="txtSurveillance" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostSurveillance" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalSurveillance" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Computer
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtComputer'),document.getElementById('txtCostComputer'),document.getElementById('txtTotalComputer'));"
                                                                                                    id="txtComputer" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostComputer" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalComputer" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Organ
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtOrgan'),document.getElementById('txtCostOrgan'),document.getElementById('txtTotalOrgan'));"
                                                                                                    id="txtOrgan" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostOrgan" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalOrgan" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Swimming Pool
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtsPool'),document.getElementById('txtCostPool'),document.getElementById('txtTotalPool'));"
                                                                                                    id="txtsPool" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostPool" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalPool" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Public Utilities
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtUtilities'),document.getElementById('txtCostUtilities'),document.getElementById('txtTotalUtilities'));"
                                                                                                    id="txtUtilities" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostUtilities" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalUtilities" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Telephone
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtTelephone'),document.getElementById('txtCostTelephone'),document.getElementById('txtTotalTelephone'));"
                                                                                                    id="txtTelephone" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostTelephone" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalTelephone" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Pager
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtPager'),document.getElementById('txtCostPager'),document.getElementById('txtTotalPager'));"
                                                                                                    id="txtPager" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostPager" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalPager" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Suitcase
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtSuitcase'),document.getElementById('txtCostSuitcase'),document.getElementById('txtTotalSuitcase'));"
                                                                                                    id="txtSuitcase" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostSuitcase" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalSuitcase" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Golf Bag & Accessories
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtAccessories'),document.getElementById('txtCostAccessories'),document.getElementById('txtTotalAccessories'));"
                                                                                                    id="txtAccessories" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostAccessories" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalAccessories" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Camera
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtCamera'),document.getElementById('txtCostCamera'),document.getElementById('txtTotalCamera'));"
                                                                                                    id="txtCamera" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostCamera" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalCamera" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Servant
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtServant'),document.getElementById('txtCostServant'),document.getElementById('txtTotalServant'));"
                                                                                                    id="txtServant" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostServant" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalServant" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Driver
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtDriver'),document.getElementById('txtCostDriver'),document.getElementById('txtTotalDriver'));"
                                                                                                    id="txtDriver" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostDriver" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalDriver" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>Gardener or Upkeep of Compound
                                                                                            </td>
                                                                                            <td></td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" onkeydown="javascript:computeTotal(document.getElementById('txtGardener'),document.getElementById('txtCostGardener'),document.getElementById('txtTotalGardener'));"
                                                                                                    id="txtGardener" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtCostGardener" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="txtTotalGardener" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 1%">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </tr>
                                        <tr>
                                            <telerik:RadAjaxPanel CssClass="tbl" ID="RadAjaxPanel3" runat="server">
                                                <table class="tbl" border="0">
                                                    <tr valign="top">
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 1%">&nbsp;
                                                                    </td>
                                                                    <td style="width: 98%">
                                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tr class="trstandtop">
                                                                                            <td style="width: 60%"></td>
                                                                                            <td style="width: 10%"></td>
                                                                                            <td style="width: 10%"></td>
                                                                                            <td style="width: 10%"></td>
                                                                                            <td style="width: 10%"></td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td></td>
                                                                                            <td>A) No.of Persons
                                                                                            </td>
                                                                                            <td>B) Rate per Person p.m($)
                                                                                            </td>
                                                                                            <td>C) Period provided(No.of days)
                                                                                            </td>
                                                                                            <td>D) Value($) A*B*12*C/365
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>a. Self /Wife /Child > 20 years old
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text107" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text108" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text113" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text114" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>b.Children < 3 years old
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text115" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text116" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text117" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text118" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>c.Children : 3-7 years old
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text119" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text120" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text121" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text122" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>d.Children : 8-20 years old
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text123" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text124" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text125" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" disabled="true" id="Text126" runat="server"
                                                                                                    style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td colspan="3">e.Plus 2% of basic salary for period provided
                                                                                            </td>
                                                                                            <td>
                                                                                                <td>
                                                                                                    <input type="text" class="textfields" disabled="true" id="Text130" runat="server"
                                                                                                        style="width: 80px" />
                                                                                                </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </tr>
                                        <tr>
                                            <telerik:RadAjaxPanel CssClass="tbl" ID="RadAjaxPanel2" runat="server">
                                                <table class="tbl" border="0">
                                                    <tr valign="top">
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                                                <tr>
                                                                    <td style="width: 1%">&nbsp;
                                                                    </td>
                                                                    <td style="width: 98%">
                                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table border="0" cellspacing="0" cellpadding="0" width="100%">
                                                                                        <tr>
                                                                                            <td style="width: 90%"></td>
                                                                                            <td style="width: 10%"></td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td class="tdstand" colspan="2">(4) Others
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>a) Cost of home leave passages and incidental benefits.<br />
                                                                                                (See Paragraph 16 of the Explanatory Notes)
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandbottom">
                                                                                            <td>No. of passengers for self:<input type="text" class="textfields" id="Text109" runat="server"
                                                                                                style="width: 80px" />
                                                                                                Spouse:<input type="text" class="textfields" id="Text110" runat="server" style="width: 80px" />
                                                                                                Childern:<input type="text" class="textfields" id="Text111" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text112" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>b) Interest payment made by the employer to a third party on behalf of an employee
                                                                            and / or interest benefits asising from loans provided by employer interest free
                                                                            or at a rate below market rate to the employee who has substantial shareholding
                                                                            or control or influence over the company:
                                                                                            </td>
                                                                                            <td></td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>c) Life insurance paid by the employer :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text94" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>d) Free or subsidised holidays including air passage,etc.:
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text95" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>e) Educational expenses including tutor provided :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text96" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>f) Non-monetary awards for long service (excluding awards with little commercial
                                                                            value) :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text97" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>g) Entrance / Transfer fees and annual subscription to social or recreational clubs
                                                                            :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text98" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>h) Gains from assets.eg.vehicles,property,etc.sold to employees at a price lower
                                                                            than open market value :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text99" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>i) Full cost of motor vehicles given to employee :
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text100" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>j) Car benefits <b>(See Paragraph 17 of the Explanatory Notes)</b>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text101" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>k) Other non-monetary benefits which do not fall within the above items
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text102" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                        </tr>
                                                                                        <tr class="trstandtop">
                                                                                            <td>
                                                                                                <b>TOTAL VALUE OF BENEFITS-IN-KIND(ITEMS 1 TO 4) TO BE REFLECTED IN ITEM d9 OF FORM
                                                                                1R8A</b>
                                                                                            </td>
                                                                                            <td>
                                                                                                <input type="text" class="textfields" id="Text103" runat="server" style="width: 80px" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </telerik:RadAjaxPanel>
                                        </tr>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView runat="server" ID="tbsNotes">
                                        <div class="col-sm-12">
                                            <div class="form-group">
                                                <label>Remarks</label>
                                                <textarea id="txtRemarks" runat="server" class="textfields form-control  custom-maxlength" maxlength="250"
                                                    rows="10" cols="1"></textarea>
                                            </div>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="tbsempcalendar" runat="server" CssClass="multiPage">
                                        <table border="0" cellpadding="0" cellspacing="0" style="table-layout: auto; width: 100%;">
                                            <tr>
                                                <td style="width: 1%">&nbsp;
                                                </td>
                                                <tr>
                                                    <td class="tdstand" colspan="4">(A) Calendar Information
                                                    </td>
                                                </tr>
                                                <td>


                                                    <asp:Button ID="btnsavecal" runat="server" Text="Save" OnClick="btnsavecal_Click"
                                                        Style="margin-top: 6px" Width="80px" />
                                                    <asp:Button ID="btnClearOff" runat="server" Text="Clear" OnClick="btnClearOff_Click"
                                                        Style="margin-top: 6px" Width="80px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="width: 80%; height: 300px;" rowspan="2">
                                                    <asp:Calendar ID="click_calender" runat="server" BackColor="White" BorderColor="Black"
                                                        DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black"
                                                        Height="220px" NextPrevFormat="FullMonth" OnDayRender="clickcalender_DayRender"
                                                        TitleFormat="Month" Width="400px" OnSelectionChanged="clickcalender_SelectionChanged"
                                                        OnVisibleMonthChanged="clickcalender_VisibleMonthChanged">
                                                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt"
                                                            ForeColor="#333333" Width="1%" />
                                                        <OtherMonthDayStyle ForeColor="#999999" />
                                                        <DayStyle Width="14%" />
                                                        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                                                        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333"
                                                            Height="10pt" />
                                                        <TitleStyle BackColor="navy" Font-Bold="True" Font-Size="13pt" ForeColor="White"
                                                            Height="14pt" />
                                                    </asp:Calendar>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="width: 80%; height: 300px;" rowspan="2">
                                                    <br />
                                                    <telerik:RadCalendar ID="calEmpOff" runat="server" AutoPostBack="true" Width="100%"
                                                        Height="100%" ShowOtherMonthsDays="false" EnableMultiSelect="true" Skin="Outlook"
                                                        Visible="false">
                                                    </telerik:RadCalendar>
                                                </td>
                                            </tr>
                                        </table>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                            </div>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="False" />
                            <asp:XmlDataSource ID="xmldtNat" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/NationalityStatus/Nationality"></asp:XmlDataSource>
                            <asp:XmlDataSource ID="xmldtSex" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Sex/Gender"></asp:XmlDataSource>
                            <asp:XmlDataSource ID="xmldtYesNo" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Options/Option"></asp:XmlDataSource>
                            <asp:XmlDataSource ID="xmldtRelation" runat="server" DataFile="~/XML/xmldata.xml"
                                XPath="SMEPayroll/Relations/Relation"></asp:XmlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select ID,Category_Name From DocumentCategory Where Company_Id= @company_id or Company_id=-1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server">
                                <SelectParameters>
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select ID, Name, Convert(varchar,DateOfBirth,103) DateOfBirth, DateOfBirth DateOfBirthCopy, Sex, Relation, Convert(varchar,Marriage_Date,103) Marriage_Date, Marriage_Date Marriage_DateCopy, Phone, Status, UIDN From Family  Where Emp_ID = @Emp_ID">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select EC.ID, CertificateCategoryID, Category_Name, CertificateNumber, Convert(varchar,TestDate,103) TestDate, TestDate TestDateCopy,  Convert(varchar,IssueDate,103) IssueDate, IssueDate IssueDateCopy, Convert(varchar,ExpiryDate,103) ExpiryDate, ExpiryDate ExpiryDateCopy, IssueLocation, IssuedBy, Remarks,filename From EmployeeCertificate EC Inner Join CertificateCategory CC On EC.CertificateCategoryID = CC.ID Where EC.Emp_ID = @Emp_ID"
                                InsertCommand="INSERT INTO [EmployeeCertificate] (Emp_ID, CertificateCategoryID, CertificateNumber, TestDate, IssueDate, ExpiryDate, IssueLocation, IssuedBy, Remarks,filename) VALUES (@Emp_ID, @CertificateCategoryID, @CertificateNumber, @TestDate, @IssueDate, @ExpiryDate, @IssueLocation, @IssuedBy, @Remarks,@filename)"
                                UpdateCommand="UPDATE [EmployeeCertificate] SET CertificateCategoryID=@CertificateCategoryID, CertificateNumber=@CertificateNumber, TestDate=@TestDate, IssueDate=@IssueDate, ExpiryDate=@ExpiryDate, IssueLocation=@IssueLocation, IssuedBy=@IssuedBy, Remarks=@Remarks ,filename=@filename WHERE [id] = @id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="CertificateCategoryID" Type="String" />
                                    <asp:Parameter Name="CertificateNumber" Type="String" />
                                    <asp:Parameter Name="TestDate" Type="datetime" />
                                    <asp:Parameter Name="IssueDate" Type="datetime" />
                                    <asp:Parameter Name="ExpiryDate" Type="datetime" />
                                    <asp:Parameter Name="IssueLocation" Type="String" />
                                    <asp:Parameter Name="IssuedBy" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                    <asp:Parameter Name="filename" Type="String" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" ConvertEmptyStringToNull="true"
                                        PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="CertificateCategoryID" Type="String" />
                                    <asp:Parameter Name="CertificateNumber" Type="String" />
                                    <asp:Parameter Name="TestDate" Type="datetime" />
                                    <asp:Parameter Name="IssueDate" Type="datetime" />
                                    <asp:Parameter Name="ExpiryDate" Type="datetime" />
                                    <asp:Parameter Name="IssueLocation" Type="String" />
                                    <asp:Parameter Name="IssuedBy" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                    <asp:Parameter Name="filename" Type="String" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="Select ID,Category_Name From CertificateCategory Where Company_Id= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" SelectCommand="Select EH.ID, EH.Emp_ID, SUBSTRING(CONVERT(VARCHAR(11), EH.FromDate, 113), 4, 8) FromDate, EH.ToDate, EH.ConfirmationDate, EH.DepartmentID, EH.DesignationID, OT_Entitlement=Case When EH.OT_Entitlement='Y' Then 'Yes' Else 'No' End,CPF_Entitlement=Case When EH.CPF_Entitlement='Y' Then 'Yes' Else 'No' End,EH.OT1Rate, EH.OT2Rate, Pay_Frequency=Case When EH.Pay_Frequency='M' Then 'Monthly' Else 'Hourly' End,  EH.WDays_Per_Week, convert(varchar(10),DecryptByAsymKey(AsymKey_ID('AsymKey'), EH.payrate)) as payrate,  Hourly_Rate_Mode=Case When EH.Hourly_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Hourly_Rate,  Daily_Rate_Mode=Case When EH.Daily_Rate_Mode='A' Then 'Auto' Else 'Manual' End,  EH.Daily_Rate ,SUBSTRING(CONVERT(VARCHAR(11), FromDate, 113), 4, 8) FromDateCopy,  Convert(varchar,ToDate,103) ToDateCopy,  Convert(varchar,ConfirmationDate,103) ConfirmationDateCopy,  DE.Designation, DP.DeptName From EmployeePayHistory EH Left Outer Join Designation DE On EH.DesignationID = DE.ID Left Outer Join Department  DP On EH.DepartmentID=DP.ID Where EH.Emp_ID= @Emp_ID And EH.SoftDelete=0 Order By EH.CreatedDate Desc">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <%-- SelectCommand="Select ID,(ItemID+'-'+ItemName) ItemName From Item Where ItemType = 1 And Company_ID=@company_id"--%>
                            <asp:SqlDataSource ID="SqlDataSource7" runat="server" SelectCommand="Select ID,(ItemID+'-'+ItemName) ItemName From Item Where ItemType = 2 And Company_ID=@company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <%-- SelectCommand="Select EI.ID, EI.ItemID, ISC.ItemName, EI.SerialNumber, EI.Quantity, EI.Remarks,EI.ItemReturn From EmployeeItemIssued EI Inner Join Item ISC On EI.ItemID = ISC.ID Where EI.Emp_ID=@Emp_ID"--%>
                            <asp:SqlDataSource ID="SqlDataSource8" runat="server" InsertCommand="INSERT INTO [EmployeeItemIssued] (Emp_ID, ItemID, SerialNumber, Quantity, Remarks, ItemReturn) VALUES (@Emp_ID, @ItemID, @SerialNumber, @Quantity, @Remarks, @ItemReturn)"
                                SelectCommand="Select EI.ID, EI.ItemID, ISC.ItemName, EI.SerialNumber, EI.Quantity, EI.Remarks,EI.ItemReturn From EmployeeItemIssued EI Inner Join Item ISC On EI.ItemID = ISC.ID Where EI.Emp_ID=@Emp_ID  union   select '' as ID,A.ItemID,(select ItemName from Item where ID=A.ItemID)as ItemName,'' as SerialNumber,sum(CONVERT(INT, B.Quantity)),''as  Remarks,'1'as ItemReturn from  StockOutDetails A Inner JOIN IssueDetails B on A.TransSubId=B.TransSubId where A.EmpId=@Emp_ID and B.IssueType='1' group by ItemID"
                                UpdateCommand="UPDATE [EmployeeItemIssued] SET [Emp_ID] = @Emp_ID, ItemID = @ItemID, SerialNumber=@SerialNumber, Quantity=@Quantity, Remarks=@Remarks, ItemReturn=@ItemReturn  WHERE [id] = @id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                    <asp:Parameter Name="ItemReturn" Type="Int32" />
                                    <asp:Parameter Name="ItemID" Type="Int32" />
                                    <asp:Parameter Name="Quantity" Type="Int32" />
                                    <asp:Parameter Name="SerialNumber" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <%--muru--%>
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="Emp_ID" PropertyName="Value"
                                        Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:Parameter Name="ItemReturn" Type="Int32" />
                                    <asp:Parameter Name="ItemID" Type="Int32" />
                                    <asp:Parameter Name="Quantity" Type="Int32" />
                                    <asp:Parameter Name="SerialNumber" Type="String" />
                                    <asp:Parameter Name="Remarks" Type="String" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource9" runat="server" SelectCommand="Select ID,CourseName From Course Where CompanyId= @company_id Or CompanyID=-1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource10" runat="server" SelectCommand="Select T.ID,T.CourseID, result, Venue, Convert(varchar,Course_Date,103) Course_Date,No_Of_Attempts From Training_details T Inner Join Course C On T.CourseID = C.ID Where T.EmpID = @empid"
                                InsertCommand="INSERT INTO [Training_details] (empid, courseid, result, venue, course_date, no_of_attempts) VALUES (@empid, @courseid, @result, @venue, @course_date, @no_of_attempts)"
                                UpdateCommand="UPDATE [Training_details] SET courseid=@courseid, result=@result, venue=@venue, course_date=@course_date, no_of_attempts=@no_of_attempts WHERE [id] = @id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="empid" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="result" Type="String" />
                                    <asp:Parameter Name="venue" Type="String" />
                                    <asp:Parameter Name="course_date" Type="datetime" />
                                    <asp:Parameter Name="courseid" Type="Int32" />
                                    <asp:Parameter Name="no_of_attempts" Type="Int32" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="empid" ConvertEmptyStringToNull="true"
                                        PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="courseid" Type="Int32" />
                                    <asp:Parameter Name="result" Type="String" />
                                    <asp:Parameter Name="venue" Type="String" />
                                    <asp:Parameter Name="course_date" Type="datetime" />
                                    <asp:Parameter Name="no_of_attempts" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource11" runat="server" SelectCommand="Select ID,Safety_Type From Safety_pass Where CompanyId= @company_id Or CompanyID=-1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource12" runat="server" SelectCommand="Select SD.ID,SD.Safetypass_id,safetypass_sno,safetypass_expiry From Safetypass_details SD Inner Join Safety_pass SP On SD.Safetypass_id = SP.ID Where SD.EmpID = @empid"
                                InsertCommand="INSERT INTO [Safetypass_details] (empid, safetypass_id, safetypass_sno, safetypass_expiry) VALUES (@empid, @safetypass_id, @safetypass_sno, @safetypass_expiry)"
                                UpdateCommand="UPDATE [Safetypass_details] SET safetypass_id=@safetypass_id, safetypass_sno=@safetypass_sno, safetypass_expiry=@safetypass_expiry WHERE [id] = @id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="empid" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="safetypass_sno" Type="String" />
                                    <asp:Parameter Name="safetypass_expiry" Type="datetime" />
                                    <asp:Parameter Name="Safetypass_id" Type="Int32" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="empid" ConvertEmptyStringToNull="true"
                                        PropertyName="Value" Type="Int32" />
                                    <asp:Parameter Name="safetypass_sno" Type="String" />
                                    <asp:Parameter Name="safetypass_expiry" Type="datetime" />
                                    <asp:Parameter Name="safetypass_id" Type="Int32" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <%--SelectCommand="Select ID, YOS, P.leaves_allowed YOSCAL, Actual_YOS, LY_Leaves_Bal, StartDate, EndDate, TodayDate, LeavesAllowed From (	Select ID, YOS=Case When Actual_YOS > 10 THEN 10 WHEN Actual_YOS =0 THEN 1 Else Actual_YOS End,Actual_YOS,LY_Leaves_Bal, 	Convert(varchar,StartDate,103) StartDate, 	Convert(varchar,EndDate,103) EndDate, 	LeavesAllowed, 	TodayDate=Case When Getdate() Between Startdate And Enddate then Convert(varchar,Getdate(),103) else null End 	, E.Emp_Code, E.Emp_Group_ID	From YOSLeavesAllowed  	Y Inner Join  Employee E On Y.Emp_ID = E.Emp_Code ) E Inner Join  Prorated_Leaves P On ( 	E.Emp_Group_ID = P.Group_ID And P.year_of_service = E.Actual_Yos ) Where Emp_Code= @empid"--%>
                            <asp:SqlDataSource ID="SqlDataSource13" runat="server" SelectCommand="Select top 1 ID, YOS, P.leaves_allowed YOSCAL, Actual_YOS, LY_Leaves_Bal, StartDate, EndDate, TodayDate, LeavesAllowed From (	Select ID, YOS=Case When Actual_YOS > 10 THEN 10 WHEN Actual_YOS =0 THEN 1 Else Actual_YOS End,Actual_YOS,LY_Leaves_Bal, 	Convert(varchar,StartDate,103) StartDate, 	Convert(varchar,EndDate,103) EndDate, 	LeavesAllowed, 	TodayDate=Case When Getdate() Between Startdate And Enddate then Convert(varchar,Getdate(),103) else null End 	, E.Emp_Code, E.Emp_Group_ID	From YOSLeavesAllowed  	Y Inner Join  Employee E On Y.Emp_ID = E.Emp_Code ) E Inner Join  Prorated_Leaves P On ( 	E.Emp_Group_ID = P.Group_ID And P.year_of_service = Case When E.Actual_Yos > 10 THEN 10 WHEN E.Actual_Yos =0 THEN 1 Else E.Actual_Yos End) Where Emp_Code= @empid">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="empid" PropertyName="Value"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <%--<asp:SqlDataSource ID="SqlDataSource13" runat="server" 
        SelectCommand="Sp_YOS_Select"  SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtEmpCodeHdn" Name="emp_code" PropertyName="Value"
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>--%>
                            <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">

                                    $(document).ready(function () {
                                        PREmployyeCPFChnage();
                                    });

                                    window.onload = function () {
                                        //               var tabstrip = <%= tbsEmp.ClientID %>;      
                                        //               for (var i = 0; i < tabstrip.Tabs.Count-1; i++)
                                        //               {      
                                        //                    tabstrip.Tabs[i].Enable();      
                                        //               }          
                                        var ctrl2 = document.getElementById('txttaxbornbyempamt');
                                        if (ctrl2 != null) {
                                            taxbornbyemployer();
                                            taxbornbyemployerFPHN();
                                            EnableDisableandValue('cmbpensionoutsing', 'txtpensionoutsing');
                                            EnableDisableandValue('cmbexcessvolcpfemp', 'txtexcessvolcpfemp');
                                            EnableDisableandValue('cmbstockoption', 'txtstockoption');
                                            EnableDisableandValue('cmbbenefitskind', 'txtbenefitskind');
                                            EnableDisableandValue('cmbretireben', 'txtretirebenfundname,txtbretireben');
                                            EnableDisableandValue('cmbcessprov', 'dtcessdate,dtcommdate');
                                        }
                                        ShowNoticePeriod();
                                    }
                                </script>

                            </telerik:RadCodeBlock>
                            <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

                                <script type="text/javascript">


                                    function RenderADay(sender, eventArgs) {
                                        var cell = eventArgs.get_cell();
                                        var day = eventArgs.get_renderDay();
                                        if (day) {
                                            var view = day.RadCalendarView;
                                            if (eventArgs.get_date()[1] != view._MonthStartDate[1]) {
                                                //cell.innerText = "(" + cell.innerText + ")";
                                            }


                                            if (day.get_isWeekend()) {
                                                cell.style.backgroundColor = "#efefef";
                                                cell.disabled = true;
                                            }
                                        }
                                        else
                                            cell.innerText = "";
                                    }
                                </script>

                                <script type="text/javascript" language="javascript">
                                    var test;
                                    var giro;

                                    function msg() {
                                        var sMSG = "Personal Info - Time/Card/Swipe/Punch ID";
                                        sMSG = "Following fields are missing. <br/>" + sMSG;
                                        WarningNotification(sMSG);
                                        return false;
                                    }



                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }

                                    function storeoldval(val) {
                                        document.getElementById('txthid').value = val;
                                    }


                                    function nric_change() {
                                        var ctrltxtnricno = document.getElementById('txtnricno');
                                        var ctrltxtEmployeeCPFAcctNumber = document.getElementById('txtEmployeeCPFAcctNumber');
                                        var ctrltxtICPPNumber = document.getElementById('txtICPPNumber');
                                        var ctrltxtIncomeTaxID = document.getElementById('txtIncomeTaxID');

                                        if (ctrltxtnricno.value.trim().length > 0) {
                                            ctrltxtEmployeeCPFAcctNumber.value = ctrltxtnricno.value;
                                            ctrltxtICPPNumber.value = ctrltxtnricno.value;
                                            ctrltxtIncomeTaxID.value = ctrltxtnricno.value;
                                        }
                                    }

                                    function isNumericKeyStrokeDecimalPercent(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode
                                        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46)) {
                                            return false;
                                        }
                                        return true;
                                    }


                                    function isNumericKeyStrokeDecimal(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode
                                        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                                            return false;

                                        return true;
                                    }
                                    function ShowHourlyRate() {
                                        window.showModalDialog('HourlyRateFormula.aspx', null, 'dialogWidth:500px;dialogHeight:250px;');
                                    }
                                    function ShowDailyRate() {
                                        window.showModalDialog('DailyRateFormula.aspx', null, 'dialogWidth:600px;dialogHeight:520px;');
                                    }
                                    //        function fnEmptype(e) 	
                                    //        {
                                    //            if(e.value == "SC" || e.value == "SPR" || e.value == "SDPR")
                                    //                {
                                    //                    alert("test");
                                    //                    document.getElementById('cmbEmpRefType').value = "1";
                                    //                }
                                    //        }

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

                                    function EnableDisableandValue(controlid, obj) {
                                        var objval = obj.split(',');
                                        var control = document.getElementById(controlid);
                                        for (var num = 0; num < objval.length; num++) {
                                            var ctrl = document.getElementById(objval[num]);
                                            if (control.value == "Yes") {
                                                ctrl.disabled = false;
                                            }
                                            else {
                                                ctrl.disabled = true;

                                            }
                                        }
                                    }

                                    function changeperc(obj) {
                                        if (isNaN(obj.value) == true) {
                                            WarningNotification("Please enter numeric value in Giro Percentage");
                                            return false;
                                        }
                                        else {
                                            var fltpercent = obj.value;
                                            if (fltpercent >= 100) {
                                                WarningNotification("Please enter value less than 100 in Giro Percentage.");
                                                obj.value = document.getElementById('txthid').value;
                                                return false;
                                            }
                                            else {
                                                var vallbl = 0;
                                                var ctrl1 = document.getElementById('txtPerceSB');
                                                vallbl = 100 - (parseFloat(ctrl1.value) + parseFloat(document.getElementById("txtSBperct").value));
                                                if (isNaN(vallbl) == true) {
                                                    if (vallbl.length <= 0) {
                                                        document.getElementById('lblPerc').innerText = "0";
                                                        document.getElementById('lblPercSB2').innerText = "0";
                                                    }
                                                    else {
                                                        vallbl = 100 - (parseFloat(ctrl1.value));
                                                        document.getElementById('lblPerc').innerText = vallbl;
                                                        document.getElementById('lblPercSB2').innerText = 100 - parseFloat(vallbl);
                                                    }
                                                }
                                                else {
                                                    document.getElementById('lblPerc').innerText = vallbl;
                                                    document.getElementById('lblPercSB2').innerText = 100 - parseFloat(vallbl);
                                                }
                                                return true;
                                            }
                                        }
                                    }

                                    function taxbornbyemployerFPHN() {
                                        var ctrl1 = document.getElementById('cmbtaxbornbyemployerFPHN');
                                        var ctrl2 = document.getElementById('txttaxbornbyempamt');

                                        if (ctrl1.value == "" || ctrl1.value == "F" || ctrl1.value == "N") {
                                            ctrl2.value = "";
                                            ctrl2.disabled = true;
                                        }
                                        if (ctrl1.value == "P" || ctrl1.value == "H") {
                                            ctrl2.disabled = false;
                                        }
                                    }
                                    //        function enaretireben()
                                    //        {
                                    //            var ctrl12 = document.getElementById('txtretirebenfundname');
                                    //            var ctrl13 = document.getElementById('txtbretireben');
                                    //            ctrl12.value ="";
                                    //            ctrl13.value ="";
                                    //            ctrl12.disabled = !ctrl12.disabled;
                                    //            ctrl13.disabled = !ctrl13.disabled;
                                    //        }

                                    function submitforSB() {
                                        var sMSG = "";
                                        if ((!document.getElementById("cmbPayMode").value) || (document.getElementById("cmbPayMode").value == '-select-'))
                                            sMSG += "Primary Bank Info-Secondary Info cannot be enterd unless Primary Info present <br>";

                                        if (document.getElementById("txtSBperct").value > 100)
                                            sMSG += "Primary Bank Info-Percentage cannot be more than 100 <br>";


                                        if ((!document.getElementById("cmbSBPayMode").value) || (document.getElementById("cmbSBPayMode").value == '-select-'))
                                            sMSG += "Secondary Bank Info-Pay Mode <br>";

                                        if ((!document.getElementById("txtSBGIROAccountNo").value))
                                            sMSG += "Secondary Bank Info-Giro Account No <br>";

                                        if ((!document.getElementById("cmbSBbranchcode").value) || (document.getElementById("cmbSBbranchcode").value == '-select-'))
                                            sMSG += "Secondary Bank Info-Giro Bank <br/>";

                                        if (((!document.getElementById("txtSBgirobranch").value)))
                                            sMSG += "Secondary Bank Info-Giro Branch <br>";

                                        if ((!document.getElementById("txtSBgiroaccountname").value))
                                            sMSG += "Secondary Bank Info-Giro Account Name <br>";
                                        //  if (document.getElementById("radListPayType").value == '0') {
                                        if ($(document.getElementById("radListPayType")).find("input[type=radio]:checked").val() == '0') {
                                            if ((!document.getElementById("txtSBperct").value)) {
                                                sMSG += "Secondary Bank Info-Giro Percentage <br>";
                                            }
                                            else {
                                                if (isNaN(document.getElementById("txtSBperct").value) == true) {
                                                    sMSG = sMSG + "Please enter numeric value in Giro Percentage" + " <br>";
                                                }
                                                else {
                                                    var fltpercent = document.getElementById("txtSBperct").value;
                                                    if (fltpercent >= 100) {
                                                        sMSG = sMSG + "Please enter value less than 100 in Giro Percentage" + " <br>";
                                                    }
                                                }
                                            }
                                        }


                                        if (sMSG == "")
                                            return true;
                                        else {
                                            sMSG = "Following fields are missing. <br>" + sMSG;
                                            WarningNotification(sMSG);
                                            return false;
                                        }
                                    }

                                    Date.isLeapYear = function (year) {
                                        return (((year % 4 === 0) && (year % 100 !== 0)) || (year % 400 === 0));
                                    };

                                    Date.getDaysInMonth = function (year, month) {
                                        return [31, (Date.isLeapYear(year) ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
                                    };

                                    Date.prototype.isLeapYear = function () {
                                        return Date.isLeapYear(this.getFullYear());
                                    };

                                    Date.prototype.getDaysInMonth = function () {
                                        return Date.getDaysInMonth(this.getFullYear(), this.getMonth());
                                    };


                                    Date.prototype.addMonths = function (value) {
                                        var n = this.getDate();
                                        this.setDate(1);
                                        this.setMonth(this.getMonth() + value);
                                        this.setDate(Math.min(n, this.getDaysInMonth()));
                                        return this;
                                    };

                                    Date.prototype.getRealYear = function () {
                                        if (this.getFullYear)
                                            return this.getFullYear();
                                        else
                                            return this.getYear() + 1900;
                                    };

                                    //Show Notice period
                                    function ShowNoticePeriod() {
                                        var ctrl4 = document.getElementById('cmbprobation');
                                        var ctrlable = document.getElementById('lblProbationperiod');
                                        if (document.getElementById("rdJoiningDate").value != "" && ctrl4.value != '-1') {
                                            // var myDate = new Date(document.employeeform.rdJoiningDate.value);
                                            var myString = document.getElementById("rdJoiningDate").value;
                                            var mySplitResult = myString.split("-");

                                            var currDate = new Date();
                                            currDate.setFullYear(mySplitResult[0], mySplitResult[1], mySplitResult[2]);

                                            var currDay = currDate.getDate();
                                            var currMonth = currDate.getMonth();
                                            var currYear = currDate.getRealYear();

                                            var ModMonth = currMonth + 1 + (ctrl4.value - 1);

                                            if (ModMonth > 12) {
                                                ModMonth = ModMonth - 12;
                                                currYear = currYear + 1;
                                            }


                                            var ModDateStr = currDay + "/" + ModMonth + "/" + currYear;
                                            ctrlable.innerHTML = "Probation period End Date: " + ModDateStr
                                        }
                                        else {
                                            ctrlable.innerHTML = "";
                                        }
                                    }

                                    function validcontrols() {

                                        var strmsg = "";



                                        var ctrl2 = document.getElementById('txttaxbornbyempamt');
                                        var ctrl = document.getElementById('tbsIR8A');
                                        if (ctrl2 != null) {
                                            if (ctrl.disabled == false) {
                                                var ctrl4 = document.getElementById('txtpensionoutsing');
                                                var ctrl6 = document.getElementById('txtexcessvolcpfemp');
                                                var ctrl8 = document.getElementById('txtstockoption');
                                                var ctrl10 = document.getElementById('txtbenefitskind');
                                                var ctrl13 = document.getElementById('txtbretireben');
                                                var ctrl16 = document.getElementById('cmbcessprov');
                                                var ctrl17 = document.getElementById('dtcessdate');
                                                var ctrl18 = document.getElementById('dtcommdate');
                                                var ctrl19 = document.getElementById('cmbaddress');
                                                var ctrllocaladdr1 = document.getElementById('txtblock').value;
                                                var ctrllocaladdr2 = document.getElementById('txtunit').value;
                                                var ctrllocaladdr3 = document.getElementById('txtstreet').value;
                                                var ctrllocaladdr4 = document.getElementById('txtlevel').value;

                                                if (document.getElementById('txtv1rate') != null)
                                                    var ctrlvrate1 = document.getElementById('txtv1rate').value;
                                                if (document.getElementById('txtv2rate') != null)
                                                    var ctrlvrate2 = document.getElementById('txtv2rate').value;
                                                if (document.getElementById('txtv3rate') != null)
                                                    var ctrlvrate3 = document.getElementById('txtv3rate').value;
                                                if (document.getElementById('txtv4rate') != null)
                                                    var ctrlvrate4 = document.getElementById('txtv4rate').value;




                                                if (ctrl2.disabled == false) {
                                                    if (ctrl2.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Tax Born by Employer Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl2.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Tax Born by Employer Amount" + " <br>";
                                                        }
                                                    }
                                                }
                                                if (ctrl4.disabled == false) {
                                                    if (ctrl4.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Pension out of singapore Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl4.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Pension out of singapore Amount" + " <br>";
                                                        }
                                                    }
                                                }
                                                if (ctrl6.disabled == false) {
                                                    if (ctrl6.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Excess Voluntary CPF Employer Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl6.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Excess Voluntary CPF Employer Amount" + " <br>";
                                                        }
                                                    }
                                                }
                                                if (ctrl8.disabled == false) {
                                                    if (ctrl8.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Stock Options Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl8.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Stock Options Amount" + " <br>";
                                                        }
                                                    }
                                                }
                                                if (ctrl10.disabled == false) {
                                                    if (ctrl10.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Benefits in kind Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl10.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Benefits in kind Amount" + " <br>";
                                                        }
                                                    }
                                                }
                                                if (ctrl13.disabled == false) {
                                                    if (ctrl13.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Retirement benefits Amount" + " <br>";
                                                    }
                                                    else {
                                                        if (isNaN(ctrl13.value) == true) {
                                                            strmsg = strmsg + "Please enter numeric value in Retirement benefits Amount" + " <br>";
                                                        }
                                                    }
                                                }

                                                if (ctrl17.disabled == false) {
                                                    if (ctrl17.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Date of Cessation" + " <br>";
                                                    }
                                                    else {
                                                        var strdate = checkDate(ctrl17, 'Date of Cessation');
                                                        if (strdate.length >= 0) {
                                                            strmsg = strmsg + strdate + " <br>";
                                                        }
                                                    }
                                                }

                                                if (ctrl18.disabled == false) {
                                                    if (ctrl18.value.trim().length <= 0) {
                                                        strmsg = strmsg + "Please enter Date of Commencement" + " <br>";
                                                    }
                                                    else {
                                                        var strdate = checkDate(ctrl18, 'Date of Commencement');
                                                        if (strdate.length >= 0) {
                                                            strmsg = strmsg + strdate + " <br>";
                                                        }
                                                        else {
                                                            var dtcomp1 = ctrl18.value;
                                                            var dtcomp2 = "31/12/1968";
                                                            var dt1 = parseInt(dtcomp1.substring(0, 2), 10);
                                                            var mo1 = parseInt(dtcomp1.substring(3, 5), 10);
                                                            var yr1 = parseInt(dtcomp1.substring(6, 10), 10);

                                                            var dt2 = parseInt(dtcomp2.substring(0, 2), 10);
                                                            var mo2 = parseInt(dtcomp2.substring(3, 5), 10);
                                                            var yr2 = parseInt(dtcomp2.substring(6, 10), 10);

                                                            var date1 = new Date(yr1, mo1, dt1);
                                                            var date2 = new Date(yr2, mo2, dt2);

                                                            if (yr1 > yr2) {
                                                                strmsg = strmsg + "Please enter Commencement Date less 01/01/1969" + " <br>";
                                                            }
                                                        }
                                                    }
                                                }

                                                if (ctrl19.disabled == false) {

                                                    if (ctrl19.value == "L") {
                                                        if (ctrllocaladdr1.trim().length <= 0) {
                                                            strmsg = strmsg + "Please enter Block in Address Info Tab" + " <br>";
                                                        }
                                                        if (ctrllocaladdr2.trim().length <= 0) {
                                                            strmsg = strmsg + "Please enter Unit in Address Info Tab" + " <br>";
                                                        }
                                                        if (ctrllocaladdr3.trim().length <= 0) {
                                                            strmsg = strmsg + "Please enter Street/Building in Address Info Tab" + " <br>";
                                                        }
                                                        if (ctrllocaladdr4.trim().length <= 0) {
                                                            strmsg = strmsg + "Please enter Level in Address Info Tab" + " <br>";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        return strmsg;
                                    }

                                    function taxbornbyemployer() {
                                        var ctrl = document.getElementById('cmbtaxbornbyemployer');
                                        var ctrl1 = document.getElementById('cmbtaxbornbyemployerFPHN');
                                        var ctrl2 = document.getElementById('txttaxbornbyempamt');

                                        if (ctrl.value == "No") {
                                            ctrl1.selectedIndex = 0;
                                            ctrl1.disabled = true;
                                            ctrl2.disabled = true;
                                            ctrl2.value = "";
                                        }
                                        else {
                                            ctrl1.disabled = false;
                                            ctrl2.disabled = false;
                                        }
                                    }

                                    function SubmitForm() {
                                        //debugger

                                        var bValid = ValidateForm();

                                        if (bValid) {

                                            var result = ValidateNRICValue();
                                            //alert(result);
                                            if (result) {

                                                //  FundType_Val();
                                                document.getElementById("oHidden").value = "Save";
                                                document.getElementById("txtEmployerCPF_H").value = document.getElementById("txtEmployerCPF").value;
                                                document.getElementById("txtEmployeeCPF_H").value = document.getElementById("txtEmployeeCPF").value;
                                                document.employeeform.submit();
                                                //document.forms("employeeform").submit();		                
                                            }
                                            else {
                                                WarningNotification('Please Enter a valid NRIC/FIN number.');
                                            }

                                        }
                                    }
                                    function SubmitForm_dubai() {
                                        document.employeeform.submit();
                                    }
                                    //function NRIC_Check() {
                                    //    var nricType = document.getElementById("cmbEmpRefType").value;
                                    //    if (document.getElementById("cmbEmpType").value == 'OT') {
                                    //        return true;
                                    //    }
                                    //    else {
                                    //        var nricVal = document.getElementById("txtnricno").value;
                                    //        var res = SMEPayroll.employee.AddEditEmployee.NRIC_Check(nricType, nricVal);
                                    //        var result = res.value;
                                    //        res.value = null;
                                    //        alert(result);
                                    //        if (result == "yes")
                                    //            return true;
                                    //        if (result == "no")
                                    //            return false;
                                    //    }

                                    //}

                                    function FundType_Val() {
                                        var race = document.getElementById('cmbRace').options[document.getElementById('cmbRace').selectedIndex].text;
                                        race = race.toUpperCase();
                                        var e = document.getElementById('cmbEmpType').value;
                                        if (race == 'CHINESE') {
                                            document.getElementById("txtFundType").value = 'CDAC';
                                            document.getElementById("txtFundAmount").value = document.getElementById("txtCDAC").value;
                                        }
                                        if (race == 'INDIAN') {
                                            document.getElementById("txtFundType").value = 'SINDA';
                                            document.getElementById("txtFundAmount").value = document.getElementById("txtSINDA").value;
                                        }

                                        if (race == 'MALAY' || race == 'MALAYSIAN') {
                                            document.getElementById("txtFundType").value = 'MBMF';
                                            document.getElementById("txtFundAmount").value = document.getElementById("txtMBMF").value;
                                        }

                                        if (race == 'EURASIAN') {
                                            document.getElementById("txtFundType").value = 'ECF';
                                            document.getElementById("txtFundAmount").value = document.getElementById("txtECF").value;
                                        }

                                        if ((race == 'OTHERS') || (race == '-SELECT-') || (e == 'WP') || (e == 'EP') || (e == 'SP') || (e == 'DP') || (e == 'NA') || (document.getElementById("chkoptfund").checked) || (document.getElementById("cmbCPFEntitlement").value == 'N')) {
                                            document.getElementById("txtFundType").value = '-1';
                                            document.getElementById("txtFundAmount").value = '-1';
                                        }
                                    }

                                    function ValidateForm() {

                                        var sMSG = "";

                                        var strirmsg = validcontrols();
                                        //----------------murugan


                                        //    			alert('bi='+document.getElementById("ddlPayrollType").value);
                                        //    			alert('half sal='+document.getElementById("ddlHalfSal").value);

                                        //    			if (( document.getElementById("ddlPayrollType").value=='Bi-Monthly' )&&(document.getElementById("ddlHalfSal").value=='IncludeFullBasicFirstHalf'))
                                        //    			{
                                        //    			
                                        //    			 
                                        //    			       if (( !document.getElementById("chkcomputecpffh").checked ) || (!document.getElementById("chkFUNDRequired").checked))
                                        //    			        {
                                        //    			         sMSG += "CPF First Half/Computer Fund First Half is not selected yet\n";
                                        //    			        alert(sMSG);
                                        //    			        } 
                                        //    			}
                                        //---------------------
                                        //Shashank Starts-Date 11/01/2010		    
                                        /** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
                                        var msg = "Personal Info - First Name,Personal Info - Gender,Personal Info-Time/Card/Swipe/Punch ID,Personal Info-Nationality ";
                                        msg = msg + ",Personal Info-Employer CPF Ref Number,Job Info-Joining Date,Job Info-Birth Date,Primary Bank Info-PayMode,Salary Info-Employee Pass Type ";
                                        msg = msg + ",Salary Info-Race";

                                        var srcData = "";
                                        //Control Validation		    
                                        //validateData(srcCtrl,destSrc,opType,srcData,msg,con)
                                        var vaiable = document.getElementById("txtEmpName");
                                        var vaiable1 = document.getElementById("cmbSex");
                                        var vaiable5 = document.getElementById("txttimecardno");
                                        var vaiable2 = document.getElementById("cmbNationality");
                                        var vaiable3 = document.getElementById("cmbEmployerCPFAcctNumber");
                                        var vaiable4 = document.getElementById("rdJoiningDate");
                                        var vaiable6 = document.getElementById("rdDOB");
                                        var vaiable7 = document.getElementById("cmbEmpType");
                                        var vaiable8 = document.getElementById("cmbPayMode");
                                        var vaiable9 = document.getElementById("cmbRace");
                                        var vaiable10 = document.getElementById("txtMonthlyLevy");
                                        var vaiable11 = document.getElementById("txtEmeConPerPh1");
                                        var vaiable12 = document.getElementById("txtEmeConPerPh2");
                                        var vaiable13 = document.getElementById("txtPhone");
                                        var vaiable14 = document.getElementById("txtHandPhone");
                                        var vaiable15 = document.getElementById("txtEmail");

                                        var srcCtrl = vaiable.id + ',' + vaiable1.id + ',' + vaiable5.id + ',' + vaiable2.id + ',' + vaiable3.id + ',' + vaiable4.id;
                                        srcCtrl = srcCtrl + ',' + vaiable6.id + ',' + vaiable8.id + ',' + vaiable7.id + ',' + vaiable9.id;
                                        //		    var srcCtrl=vaiable.id +','+vaiable1.id +','+ vaiable2.id + ',' + vaiable3.id + ','+vaiable4.id;
                                        //		    srcCtrl=srcCtrl+','+vaiable6.id+','+vaiable7.id+','+ vaiable8.id+','+ vaiable9.id;

                                        //KUMAR COMMENT
                                        var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
                                        sMSG += strirmsg;
                                        /* Mandatory Fields ends */
                                        /* "Dependent" Based Upon Source Control,Destination control value Validates */


                                        //km
                                        // move from down to heare

                                        var PRDate = document.getElementById('rdPRDate').value;


                                        if (document.getElementById("cmbEmpType").value == "SPR" && PRDate == "") {
                                            sMSG += "Salary Info-PR Date <br>";
                                        }

                                        if (($("._txtpostalcode").val().length < 6) && ($("._txtpostalcode").val().length > 0)) {
                                            WarningNotification("Contact Info-Postal Code length should be minimum 6 Characters.<br>");
                                            return false;
                                        }


                                        vaiable = document.getElementById("cmbEmpType");
                                        vaiable1 = document.getElementById("txtnricno");
                                        //Source:DropdownBox,Destination:TextBox,Mandatory,'OT',STATE   
                                        //Empyoyee Type ='OT' ,Condition ='Not Equal To',OperationType='Dependent',Message='Salary Info-NRIC/FIN Number'
                                        strirmsg = validateData(vaiable, vaiable1, 'Dependent', 'OT', 'Salary Info-NRIC/FIN Number', 'NE');
                                        if (strirmsg != "") {
                                            sMSG += strirmsg;
                                            //sMSG = "Following fields are missing.\n\n" + sMSG + "\n \n";             
                                            //Shashank Ends-Date 11/01/2010            
                                        }

                                        //Check Employee Name As Alpha Numeric Value 
                                        strirmsg = alphanumeric(document.getElementById("txtEmpName"), "Personal Info - Please enter valid First name  <br>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //Check EMployee LastName As Alpha Numeric Value 
                                        strirmsg = alphanumeric(document.getElementById("txtEmpName"), "Personal Info -Please enter valid Last name  <br>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;


                                        strirmsg = "";
                                        strirmsg = CheckNumeric(vaiable13.value, "Personal Info - Phone <br/>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;


                                        //strirmsg = "";
                                        //strirmsg = CheckNumeric(vaiable14.value, "\nPersonal Info - Mobile");
                                        //if (strirmsg != "")
                                        //    sMSG += strirmsg;


                                        //Email
                                        strirmsg = "";
                                        strirmsg = ValidateEmail(vaiable15.value, " <br/>Personal Info -Email<br/>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;


                                        //Check Date Values
                                        strirmsg = CompareDate(document.getElementById("rdJoiningDate").value, document.getElementById("rdConfirmationDate").value,
                                                "Job Info-Confirmation Date Cannot be less than Joining Date <br/>", "");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //Check Date Values
                                        strirmsg = "";
                                        strirmsg = CompareDate(document.getElementById("rdDOB").value, document.getElementById("rdJoiningDate").value,
                                                "Job Info-Birth Date Cannot be greater than or equal to Joining Date <br/>", "EQ");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //CompareDateToday : Age Should be Less than 14
                                        var selectedDate = $find('<%= rdDOB.ClientID %>');
                                        var selectDate = selectedDate.get_selectedDate();

                                        strirmsg = "";
                                        var dob = new Date(selectDate);
                                        var today = new Date();
                                        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
                                        if (age < 13) {
                                            strirmsg = "Job Info-Employee should not be below 13 Years <br/>";
                                        }
                                        //strirmsg = CompareDateToday(selectDate, "13", "<", "Job Info-Employee should not be below <br/>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        strirmsg = "";
                                        //Monthy Levy
                                        strirmsg = CheckNumeric(vaiable10.value, "Foreign Worker - Monthy Levy <br/>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;





                                        //Phone1
                                        strirmsg = "";
                                        strirmsg = CheckNumeric(vaiable11.value, "Contact Info-Phone-1");
                                        if (strirmsg != "")
                                            sMSG += strirmsg + " <br>";

                                        //Phone2
                                        strirmsg = "";
                                        strirmsg = CheckNumeric(vaiable12.value, "Contact Info-Phone-2");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //Application date 
                                        strirmsg = "";
                                        strirmsg = CompareDate(document.getElementById("rdwpappdate").value, document.getElementById("rdwpissuedate").value,
                                                "Foreign Workers -Application Date Can not be greater than issue Date <br/>", "");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        strirmsg = "";
                                        strirmsg = CompareDate(document.getElementById("rdJoiningDate").value, document.getElementById("rdTerminationDate").value,
                                                "Job Info-Termination Date Cannot be less than Joining Date <br/>", "");

                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //if (sMSG == "") {
                                        //    return true;
                                        //}
                                        //else {
                                        //    sMSG = "Following fields are missing. <br/>" + sMSG;
                                        //    WarningNotification(sMSG);
                                        //    return false;
                                        //}

                                        //sMSG = "";



                                        if (isProper(document.getElementById("txtEmpName").value) == false) {
                                            sMSG += "Please enter valid First name <br/>";
                                        }

                                        if (isProper(document.getElementById("txtlastname").value) == false) {
                                            sMSG += "Please enter valid Last name <br/>";
                                        }


                                        if (!document.getElementById("rdJoiningDate").value == false) {
                                            if (!document.getElementById("rdConfirmationDate").value == false) {
                                                if ((document.getElementById("rdJoiningDate").value > document.getElementById("rdConfirmationDate").value) == true)
                                                    sMSG += "Job Info-Confirmation Date Cannot be less than Joining Date <br/>";
                                            }
                                        }

                                        if (!document.getElementById("rdJoiningDate").value == false) {
                                            if (!document.getElementById("rdTerminationDate").value == false) {
                                                if ((document.getElementById("rdJoiningDate").value > document.getElementById("rdTerminationDate").value) == true) {
                                                    sMSG += "Job Info-Termination Date Cannot be less than Joining Date <br>";
                                                }
                                            }
                                        }



                                        if (!document.getElementById("rdJoiningDate").value == false) {
                                            if (!document.getElementById("rdDOB").value == false) {
                                                if ((document.getElementById("rdJoiningDate").value <= document.getElementById("rdDOB").value) == true) {
                                                    //  sMSG += "Job Info-Date Of Birth Cannot be greater than or equal to Joining Date\n";	
                                                }
                                            }
                                        }

                                        //                var tyear1=new Date(); 
                                        //                
                                        //                var t2= $find('<%= rdDOB.ClientID %>');  
                                        //                var tyear2 = t2.get_selectedDate();
                                        //                if (tyear2 != null) 
                                        //                {
                                        //                    if ( (tyear1.getFullYear()-tyear2.getFullYear()) < 13)
                                        //                    {
                                        //		                sMSG += "Job Info-Employee should not be below 13 years.\n";	
                                        //                    }
                                        //                }




                                        //if (document.getElementById("cmbEmpType").value != 'OT') {
                                        //    if (!document.getElementById("txtnricno").value)
                                        //        sMSG += "Salary Info-NRIC/FIN Number <br/>";
                                        //}



                                        var ctrlcmbOTEntitled = document.getElementById('cmbOTEntitled');
                                        if (ctrlcmbOTEntitled.value == "Y") {
                                            if (document.getElementById("txtOT1Rate").value == "")
                                                sMSG += "Salary Info-OT1 Rate <br/>";

                                            if (document.getElementById("txtOT2Rate").value == "")
                                                sMSG += "Salary Info-OT2 Rate <br/>";
                                        }

                                        if (document.getElementById("cmbworkingdays").value == "")
                                            sMSG += "Salary Info-No of working days <br/>";


                                        // alert(document.getElementById("cmbPayMode").value);
                                        //if ((!document.getElementById("cmbPayMode").value) || (document.getElementById("cmbPayMode").value == '-select-'))
                                        //    sMSG += "Primary Bank Info-Pay Mode <br/>";

                                        if ((!document.getElementById("txtGIROAccountNo").value) && (giro == 'giro' || ($(document.getElementById("cmbPayMode")).val() != "-1" && $(document.getElementById("cmbPayMode")).val() != "-2" && $(document.getElementById("cmbPayMode")).val() != "-select-")))
                                            sMSG += "Primary Bank Info-Giro Account No <br/>";

                                        if ((!document.getElementById("txtgirobankname").value) && (giro == 'giro' || ($(document.getElementById("cmbPayMode")).val() != "-1" && $(document.getElementById("cmbPayMode")).val() != "-2" && $(document.getElementById("cmbPayMode")).val() != "-select-")))
                                            sMSG += "Primary Bank Info-Giro Bank Code <br/>";

                                        if (((!document.getElementById("txtgirobranch").value) || (document.getElementById("txtgirobranch").value == '-1')) && (giro == 'giro' || ($(document.getElementById("cmbPayMode")).val() != "-1" && $(document.getElementById("cmbPayMode")).val() != "-2" && $(document.getElementById("cmbPayMode")).val() != "-select-")))
                                            sMSG += "Primary Bank Info-Giro Branch Code<br/>";

                                        if ((!document.getElementById("txtgiroaccountname").value) && (giro == 'giro' || $(document.getElementById("cmbPayMode")).val() != "-1" && ($(document.getElementById("cmbPayMode")).val() != "-2" && $(document.getElementById("cmbPayMode")).val() != "-select-")))
                                            sMSG += "Primary Bank Info-Giro Account Name <br/>";





                                        if (sMSG == "")
                                            return true;
                                        else {
                                            sMSG = "Following fields are missing. <br/>" + sMSG;
                                            WarningNotification(sMSG);
                                            return false;
                                        }
                                    }
                                    function SubmitQuickForm() {
                                        //debugger

                                        var bQuickValid = ValidateQuickAddForm();

                                        if (bQuickValid) {

                                            //var vqnric = ValidateQuickNRICValue();
                                            var vqnric = ValidateNRIC();

                                            if (vqnric) {

                                                //FundType_Val();
                                                document.getElementById("oHidden").value = "QuickSave";
                                                //document.getElementById("txtEmployerCPF_H").value = document.getElementById("txtEmployerCPF").value;		    	   
                                                //document.getElementById("txtEmployeeCPF_H").value = document.getElementById("txtEmployeeCPF").value;		
                                                document.employeeform.submit();
                                                //document.forms("employeeform").submit();		                
                                            }
                                            else {
                                                WarningNotification('Please Enter a valid NRIC/FIN number.');
                                                return false;
                                            }

                                        }
                                        else {
                                            return false;
                                        }
                                    }
                                    function ValidateQuickAddForm() {

                                        var sMSG = "";
                                        ////var strirmsg = validcontrols();

                                        ////Shashank Starts-Date 11/01/2010		    
                                        ///** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
                                        //var msg = "Quick Add Info-First Name,Quick Add Info-Gender,Quick Add Info-Nationality";
                                        //msg = msg + ",Quick Add Info-Employer CPF Ref Number,Quick Add Info-Joining Date,Quick Add Info-Birth Date,Quick Add Info-Employment Type";
                                        //msg = msg + ",Quick Add Info-Employee Pass Type,Quick Add Info-Race,Quick Add Info-Time/Card/Swipe/Punch ID <br/>";

                                        //var srcData = "";
                                        ////Control Validation		    
                                        ////validateData(srcCtrl,destSrc,opType,srcData,msg,con)
                                        //var vaiable = document.getElementById("txtFirstName");
                                        ////var vaiable1= document.getElementById("txtQuickLastName");
                                        //var vaiable2 = document.getElementById("cmbQuickSex");
                                        //var vaiable3 = document.getElementById("cmbQuickNationality");
                                        //var vaiable4 = document.getElementById("txtCPFNumber");
                                        ////		    var vaiable5= document.getElementById("rdConfirmationDate");		    
                                        //var vaiable6 = document.getElementById("rdJointingDate");
                                        //var vaiable7 = document.getElementById("rdBirthDate");
                                        //var vaiable8 = document.getElementById("cmbQuickPaymode");
                                        //var vaiable9 = document.getElementById("cmbQuickRefType");
                                        //var vaiable10 = document.getElementById("cmbQuickPassType");
                                        //var vaiable11 = document.getElementById("txtNRICNumber");
                                        ////var vaiable15= document.getElementById("txtTimeCardNumber");
                                        //var vaiable12 = document.getElementById("cmbQuickRace");
                                        ////		    var vaiable13= document.getElementById("txtPhone");
                                        ////		    var vaiable14= document.getElementById("txtHandPhone");    
                                        ////		    var vaiable15= document.getElementById("txtEmail");

                                        //var srcCtrl = vaiable.id + ',' + vaiable2.id + ',' + vaiable3.id + ',' + vaiable4.id;
                                        //srcCtrl = srcCtrl + ',' + vaiable6.id + ',' + vaiable7.id + ',' + vaiable9.id + ',' + vaiable10.id + ',' + vaiable12.id;
                                        ////		    var srcCtrl=vaiable.id +','+vaiable1.id +','+ vaiable2.id + ',' + vaiable3.id + ','+vaiable4.id;
                                        ////		    srcCtrl=srcCtrl+','+vaiable6.id+','+vaiable7.id+','+ vaiable8.id+','+ vaiable9.id;

                                        ////KUMAR COMMENT
                                        //var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
                                        //sMSG += strirmsg;
                                        ///* Mandatory Fields ends */
                                        ///* "Dependent" Based Upon Source Control,Destination control value Validates */


                                        ////km
                                        //// move from down to heare
                                        if ($.trim($(document.getElementById("txtFirstName")).val()) === "")
                                            sMSG += "Quick Add Info-First Name<br/>";
                                        if ($.trim($(document.getElementById("cmbQuickSex")).val()) === "s")
                                            sMSG += "Quick Add Info-Gender<br/>";
                                        if ($.trim($(document.getElementById("cmbQuickNationality")).val()) === "-select-")
                                            sMSG += "Quick Add Info-Nationality<br/>";
                                        if ($.trim($(document.getElementById("txtCPFNumber")).val()) === "")
                                            sMSG += "Quick Add Info-Employer CPF Number<br/>";
                                        if ($.trim($(document.getElementById("cmbQuickPassType")).val()) === "NA")
                                            sMSG += "Quick Add Info-Employee Pass Type<br/>";

                                        vaiable = document.getElementById("cmbQuickPassType");
                                        vaiable1 = document.getElementById("txtNRICNumber");
                                        //Source:DropdownBox,Destination:TextBox,Mandatory,'OT',STATE   
                                        //Empyoyee Type ='OT' ,Condition ='Not Equal To',OperationType='Dependent',Message='Salary Info-NRIC/FIN Number'
                                        strirmsg = validateData(vaiable, vaiable1, 'Dependent', 'OT', 'Quick Add Info-NRIC/FIN Number <br>', 'NE');
                                        if (strirmsg != "") {
                                            sMSG += strirmsg;
                                            //sMSG = "Following fields are missing.\n\n" + sMSG + "\n \n";             
                                            //Shashank Ends-Date 11/01/2010            
                                        }
                                        var PRDate = document.getElementById('rdQuickPRDate').value;
                                        if (document.getElementById("cmbQuickPassType").value == "SPR" && PRDate == "") {
                                            sMSG += "Quick Add Info-PR Date <br>";
                                        }
                                        if ($.trim($(document.getElementById("cmbQuickRace")).val()) === "-1")
                                            sMSG += "Quick Add Info-Race<br/>";
                                        if ($.trim($(document.getElementById("rdJointingDate")).val()) === "")
                                            sMSG += "Quick Add Info-Joining Date<br/>";
                                        if ($.trim($(document.getElementById("rdBirthDate")).val()) === "")
                                            sMSG += "Quick Add Info-Birth  Date<br/>";
                                        var ProductName = "<%=HttpContext.Current.Session["ANBPRODUCT"]%>";

                                        if (ProductName == "WMS" || ProductName == "WMSI" || ProductName == "WMSAMC" || ProductName == "WMSMC") {
                                            var timCardNo = document.getElementById('txtTimeCardNumber').value;
                                            if (timCardNo == "") {
                                                sMSG += "Quick Add Info-Time/Card/Swipe/Punch ID <br>";
                                            }
                                        }



                                        //Check Employee Name As Alpha Numeric Value 
                                        strirmsg = alphanumeric(document.getElementById("txtFirstName"), "Personal Info - Please enter valid First name  <br>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //Check EMployee LastName As Alpha Numeric Value 
                                        strirmsg = alphanumeric(document.getElementById("txtQuickLastName"), "Personal Info -Please enter valid Last name  <br>");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;


                                        //            strirmsg="";                
                                        //            strirmsg = CheckNumeric(vaiable13.value,"Personal Info - Phone");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg;
                                        //                 
                                        //                 
                                        //            strirmsg="";
                                        //            strirmsg = CheckNumeric(vaiable14.value,"\nPersonal Info - Mobile");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg;
                                        //                 
                                        //                 
                                        //            //Email
                                        //            strirmsg="";
                                        //            strirmsg=ValidateEmail(vaiable15.value,"\nPersonal Info -Email");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg;


                                        //Check Date Values
                                        //            strirmsg = CompareDate(document.getElementById("rdJointingDate").value,document.getElementById("rdConfirmationDate").value,
                                        //                    "Job Info-Confirmation Date Cannot be less than Joining Date\n","");
                                        //            if(strirmsg!="")
                                        //                sMSG += strirmsg;
                                        //                
                                        //Check Date Values
                                        strirmsg = "";
                                        strirmsg = CompareDate(document.getElementById("rdBirthDate").value, document.getElementById("rdJointingDate").value,
                                                "Quick Add Info-Birth Date Cannot be greater than or equal to Joining Date <br>", "EQ");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //CompareDateToday : Age Should be Less than 14
                                        var selectedDate = $find('<%= rdBirthDate.ClientID %>');
                                        var selectDate = selectedDate.get_selectedDate();

                                        strirmsg = "";
                                        var dob = new Date(selectDate);
                                        var today = new Date();
                                        var age = Math.floor((today - dob) / (365.25 * 24 * 60 * 60 * 1000));
                                        if (age < 13) {
                                            strirmsg = "Quick Add Info-Employee should not be below 13 Years";
                                        }
                                        //strirmsg = CompareDateToday(selectDate, "13", "<", "Quick Add Info-Employee should not be below ");
                                        if (strirmsg != "")
                                            sMSG += strirmsg;

                                        //            strirmsg="";    
                                        //            //Monthy Levy
                                        //            strirmsg = CheckNumeric(vaiable10.value,"Foreign Worker - Monthy Levy\n");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg;
                                        //                 
                                        //                 
                                        //  
                                        //                 
                                        //              
                                        //            //Phone1
                                        //            strirmsg="";
                                        //            strirmsg = CheckNumeric(vaiable11.value,"Contact Info-Phone-1");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg + "\n";
                                        //            
                                        //            //Phone2
                                        //            strirmsg="";
                                        //            strirmsg = CheckNumeric(vaiable12.value,"Contact Info-Phone-2");
                                        //            if(strirmsg!="")
                                        //                 sMSG += strirmsg;
                                        //            
                                        //             //Application date 
                                        //            strirmsg="";
                                        //            strirmsg = CompareDate(document.getElementById("rdwpappdate").value,document.getElementById("rdwpissuedate").value,
                                        //                    "Foreign Workers -Application Date Can not be greater than issue Date\n","");
                                        //            if(strirmsg!="")
                                        //                sMSG += strirmsg;
                                        //            
                                        //            strirmsg="";
                                        //            strirmsg = CompareDate(document.getElementById("rdJoiningDate").value,document.getElementById("rdTerminationDate").value,
                                        //                    "Job Info-Termination Date Cannot be less than Joining Date\n","");

                                        //if (strirmsg != "")
                                        //    sMSG += strirmsg;

                                        if (sMSG == "") {
                                            return true;
                                        }
                                        else {
                                            sMSG = "Following fields are missing. <br>" + sMSG;
                                            WarningNotification(sMSG);
                                            return false;
                                        }

                                        sMSG = "";

                                        if (!document.getElementById("txtFirstName").value)
                                            sMSG = "Personal Info-First Name <br>";

                                        if (isProper(document.getElementById("txtFirstName").value) == false) {
                                            sMSG += "Please enter valid First name  <br>";
                                        }

                                        if (isProper(document.getElementById("txtQuickLastName").value) == false) {
                                            sMSG += "Please enter valid Last name  <br>";
                                        }

                                        if (((!document.getElementById("cmbQuickSex").value) || (document.getElementById("cmbQuickSex").value == 's')))
                                            sMSG += "Quick Add Info-Gender <br>";

                                        if (document.getElementById("cmbQuickNationality").value == '-select-')
                                            sMSG += "Quick Add Info-Nationality <br>";

                                        //            if (( !document.getElementById("cmbEmployerCPFAcctNumber").value )||(document.getElementById("cmbEmployerCPFAcctNumber").value=='-select-'))
                                        //                sMSG += "Personal Info-Employer CPF Ref Number\n";	




                                        //		    if ( !document.employeeform.rdConfirmationDate.value )
                                        //			    sMSG += "Job Info-Confirmation Date\n";	


                                        //		    if ( !document.getElementById("rdJoiningDate").value == false)
                                        //		    {
                                        //		        if ( !document.getElementById("rdConfirmationDate").value == false)
                                        //		        {
                                        //    		        if((document.getElementById("rdJoiningDate").value >  document.getElementById("rdConfirmationDate").value) == true)
                                        //			        sMSG += "Job Info-Confirmation Date Cannot be less than Joining Date\n";	
                                        //		        }
                                        //		    }

                                        //		    if ( !document.getElementById("rdJoiningDate").value == false)
                                        //		    {
                                        //		        if ( !document.getElementById("rdTerminationDate").value == false)
                                        //		        {
                                        //    		        if((document.getElementById("rdJoiningDate").value >  document.getElementById("rdTerminationDate").value) == true)
                                        //    		        {
                                        //			            sMSG += "Job Info-Termination Date Cannot be less than Joining Date\n";	
                                        //			        }
                                        //		        }
                                        //		    }

                                        if (!document.getElementById("rdBirthDate").value)
                                            sMSG += "Job Info-Birth Date <br>";

                                        if (!document.getElementById("rdJoiningDate").value == false) {
                                            if (!document.getElementById("rdBirthDate").value == false) {
                                                if ((document.getElementById("rdJoiningDate").value <= document.getElementById("rdBirthDate").value) == true) {
                                                    //  sMSG += "Job Info-Date Of Birth Cannot be greater than or equal to Joining Date\n";	
                                                }
                                            }
                                        }

                                        //                var tyear1=new Date(); 
                                        //                
                                        //                var t2= $find('<%= rdDOB.ClientID %>');  
                                        //                var tyear2 = t2.get_selectedDate();
                                        //                if (tyear2 != null) 
                                        //                {
                                        //                    if ( (tyear1.getFullYear()-tyear2.getFullYear()) < 13)
                                        //                    {
                                        //		                sMSG += "Job Info-Employee should not be below 13 years.\n";	
                                        //                    }
                                        //                }

                                        if ((!document.getElementById("cmbQuickPassType").value) || (document.getElementById("cmbQuickPassType").value == 'NA'))
                                            sMSG += "Salary Info-Employee Pass Type <br>";


                                        if (document.getElementById("cmbQuickPassType").value != 'OT') {
                                            if (!document.getElementById("txtNRICNumber").value)
                                                sMSG += "Salary Info-NRIC/FIN Number <br>";
                                        }



                                        //-------------

                                        //            var ctrlcmbOTEntitled = document.getElementById('cmbOTEntitled');
                                        //            if (ctrlcmbOTEntitled.value=="Y")
                                        //            {
                                        //		        if (document.getElementById("txtOT1Rate").value == "")
                                        //		            sMSG += "Salary Info-OT1 Rate\n"; 	

                                        //		        if (document.getElementById("txtOT2Rate").value == "")
                                        //		            sMSG += "Salary Info-OT2 Rate\n"; 	
                                        //            }

                                        //		    if (document.getElementById("cmbworkingdays").value == "")
                                        //		        sMSG += "Salary Info-No of working days\n"; 	

                                        if ((!document.getElementById("cmbQuickRace").value) || (document.getElementById("cmbQuickRace").value == '-1'))
                                            sMSG += "Salary Info-Race <br>";
                                        //    	    alert(document.getElementById("cmbQuickPaymode").value);
                                        //	        if (( !document.getElementById("cmbQuickPaymode").value )||(document.getElementById("cmbQuickPaymode").value=='-select-'))
                                        //			    sMSG += "Primary Bank Info-Pay Mode\n";	

                                        //		    if (( !document.getElementById("txtGIROAccountNo").value )&&(giro=='giro'))
                                        //			    sMSG += "Primary Bank Info-Giro Account No\n";	
                                        //    			
                                        //    	    if ((!document.getElementById("txtgirobankname").value )&&(giro=='giro'))
                                        //			    sMSG += "Primary Bank Info-Giro Bank\n";
                                        //    			
                                        //		    if ((( !document.getElementById("txtgirobranch").value )||(document.getElementById("txtgirobranch").value=='-1'))&&(giro=='giro'))
                                        //			    sMSG += "Primary Bank Info-Giro Branch\n";
                                        //    				
                                        //		    if (( !document.getElementById("txtgiroaccountname").value )&&(giro=='giro'))
                                        //			    sMSG += "Primary Bank Info-Giro Account Name\n";	



                                        if (sMSG == "")
                                            return true;
                                        else {
                                            sMSG = "Following fields are missing. <br>" + sMSG;
                                            WarningNotification(sMSG);
                                            return false;
                                        }
                                    }
                                    function isProper(formField) {
                                        var result = true;
                                        var string = formField.length;
                                        var iChars = "*|,\":<>[]{}`\';()@&$#%";
                                        for (var i = 0; i < string; i++) {
                                            if (iChars.indexOf(formField.charAt(i)) != -1)
                                                result = false;
                                        }
                                        return result;
                                    }

                                    function cpf_change() {

                                        var cpfentitled = document.getElementById('cmbCPFEntitlement').value;
                                        var PRDate = document.getElementById('rdPRDate').value;
                                        var empgroup = document.getElementById('cmbempcpfgroup').value;
                                        var dob = document.getElementById('rdDOB').value;
                                        if (document.getElementById('cmbEmpType').value == 'SC')
                                            PRDate = '';
                                        var res = SMEPayroll.employee.AddEditEmployee.CPFChange(cpfentitled, PRDate, empgroup, dob);

                                        var resvalue = res.value;
                                        res.value = null;
                                        var resValAr = resvalue.split(',');

                                        document.getElementById('txtEmployerCPF').value = resValAr[0];
                                        document.getElementById('txtEmployeeCPF').value = resValAr[1];

                                        if (!document.getElementById("chkoptfund").checked && cpfentitled == 'Y')
                                            fund_cal();
                                        if (document.getElementById("chkoptfund").checked || cpfentitled == 'N') {
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';
                                        }

                                        return true;
                                    }

                                    function calcDate(date1, date2) {
                                        var diff = Math.floor(date1.getTime() - date2.getTime());
                                        var day = 1000 * 60 * 60 * 24;

                                        var days = Math.floor(diff / day);
                                        var months = Math.floor(days / 31);
                                        var years = Math.floor(months / 12);



                                        return years;
                                    }

                                    //Senthil Start
                                    function PREmployyeCPFChnage() {

                                        var employmentType = document.getElementById('cmbEmpType').value;
                                        var PRDate = document.getElementById('rdPRDate').value;
                                        if (employmentType == 'SPR') {
                                            var years = PREmployeeFullCPF(employmentType, PRDate);
                                            if (years) {
                                                document.getElementById('tdEmployeeCPFText').style.visibility = "visible";
                                                document.getElementById('tdEmployeeCPFValue').style.visibility = "visible";
                                                document.getElementById('tdEmployerCPFText').style.visibility = "visible";
                                                document.getElementById('tdEmployerCPFValue').style.visibility = "visible";
                                                document.getElementById('rdoEmployeeYes').style.visibility = "visible";
                                                document.getElementById('rdoEmployeeNo').style.visibility = "visible";
                                                document.getElementById('rdoEmployerYes').style.visibility = "visible";
                                                document.getElementById('rdoEmployerNo').style.visibility = "visible";
                                            } else {

                                                document.getElementById('tdEmployeeCPFText').style.visibility = "hidden";
                                                document.getElementById('tdEmployeeCPFValue').style.visibility = "hidden";
                                                document.getElementById('tdEmployerCPFText').style.visibility = "hidden";
                                                document.getElementById('tdEmployerCPFValue').style.visibility = "hidden";
                                                document.getElementById('rdoEmployeeYes').style.visibility = "hidden";
                                                document.getElementById('rdoEmployeeNo').style.visibility = "hidden";
                                                document.getElementById('rdoEmployerYes').style.visibility = "hidden";
                                                document.getElementById('rdoEmployerNo').style.visibility = "hidden";
                                            }

                                        } else {
                                            //alert("Please select Singapore PR");
                                        }
                                        return true;
                                    }
                                    function PREmployeeFullCPF(employmentType, PRDate) {
                                        var result;

                                        $.ajax({
                                            type: "POST",
                                            url: "AddEditEmployee.aspx/EmployeeFullCPF",
                                            data: '{"employeeType":"' + employmentType + '","PRDate":"' + PRDate + '"}',
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            async: false,
                                            cache: false,
                                            success: function (response) {
                                                result = response;

                                            },
                                            failure: function (response) {
                                                alert(response.d);

                                            }
                                        });
                                        return result;
                                    }
                                    //Senthil End
                                    //	    function Calculate_FundAmt(text)
                                    //	    {
                                    //	        fund_cal();
                                    //	    }

                                    function myOnCientFileSelected(radUpload, eventArgs) {
                                        var image = document.getElementById("Image1");
                                        if (image != null) {
                                            var selectedFileName = eventArgs.FileInputField.value.toLowerCase();
                                            var selectedFileExt = selectedFileName.substring(selectedFileName.lastIndexOf(".") + 1);
                                            if (selectedFileExt == "gif" || selectedFileExt == "jpg" || selectedFileExt == "png" || selectedFileExt == "bmp" || selectedFileExt == "jpeg") {
                                                image.src = selectedFileName;
                                                document.getElementById("Hidden1").value = selectedFileName;
                                            }
                                        }
                                    }

                                    function myOnClientCleared(radUpload, eventargs) {
                                        var image = document.getElementById("Image1");
                                        image.src = null;
                                    }

                                    function emp_type_change(e) {

                                        if (e.value == 'OT') {

                                            $('#<%=other_type.ClientID %>').show();
                                            // document.getElementById("other_type").style.display = 'none';

                                        }
                                        else {
                                            $('#<%=other_type.ClientID %>').hide();
                                            // document.getElementById("other_type").style.display = 'blac';
                                        }
                                        /* added by vishal */
                                        if ((e.value == 'SPR') || (e.value == 'SDPR')) {
                                            datepickerenable();

                                        } else {
                                            datepicker();
                                            document.getElementById('tdEmployeeCPFText').style.visibility = "hidden";
                                            document.getElementById('tdEmployeeCPFValue').style.visibility = "hidden";
                                            document.getElementById('tdEmployerCPFText').style.visibility = "hidden";
                                            document.getElementById('tdEmployerCPFValue').style.visibility = "hidden";
                                            document.getElementById('rdoEmployeeYes').style.visibility = "hidden";
                                            document.getElementById('rdoEmployeeNo').style.visibility = "hidden";
                                            document.getElementById('rdoEmployerYes').style.visibility = "hidden";
                                            document.getElementById('rdoEmployerNo').style.visibility = "hidden";
                                        }

                                        if ((e.value == 'SPR') || (e.value == 'SDPR') || (e.value == 'SC')) {
                                            document.getElementById("cmbCPFEntitlement").value = 'Y';
                                            document.getElementById('cmbEmpRefType').value = "1";
                                            document.forms[0].cmbEmpRefType.disabled = true;

                                            //KM
                                            cpf_change();
                                        }
                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'DP') || (e.value == 'NA') || (e.value == 'OT')) {
                                            document.getElementById("cmbCPFEntitlement").value = 'N';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';

                                            document.getElementById('txtEmployerCPF').value = '';
                                            document.getElementById('txtEmployeeCPF').value = '';
                                            document.getElementById('cmbEmpRefType').value = "2";
                                            document.forms[0].cmbEmpRefType.disabled = true;

                                        }

                                        /* end vishal */


                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'SC') || (e.value == 'DP')) {

                                            datepicker();
                                        }
                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'DP')) {
                                            document.getElementById("txtFWLCode").disabled = false;
                                        }
                                        else {
                                            document.getElementById("txtFWLCode").disabled = true;
                                        }
                                        if ((e.value == 'SPR') || (e.value == 'SDPR') || (e.value == 'SC') || (e.value == 'DP') || (e.value == 'EP')) {
                                            document.getElementById("txtMonthlyLevy").disabled = true;
                                        }
                                        else {
                                            document.getElementById("txtMonthlyLevy").disabled = false;
                                        }
                                        if ((e.value == 'SPR') || (e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'SDPR') || (e.value == 'DP')) {
                                            test = "check";
                                        }
                                        else {
                                            test = "";
                                        }
                                        if ((e.value == 'NA')) {
                                            document.forms[0].cmbEmpRefType.disabled = false;
                                        }
                                    }
                                    function emp_qucick_type_change(e) {

                                        /* added by vishal */
                                        if ((e.value == 'SPR') || (e.value == 'SDPR')) {

                                            document.getElementById('tdPRDate').style.visibility = "visible";
                                            //  document.getElementById('tdQuickPRDate').style.visibility = "visible";
                                            document.getElementById('rdPRDate').disabled = "visible";



                                        } else {
                                            document.getElementById('tdPRDate').style.visibility = "hidden";
                                            // document.getElementById('tdQuickPRDate').style.visibility = "hidden";
                                            // alert('enter');
                                            // document.getElementById('tdEmployeeCPFText').style.visibility = "hidden";
                                            // document.getElementById('rdPRDate').disabled = "hidden";

                                        }

                                        if ((e.value == 'SPR') || (e.value == 'SDPR') || (e.value == 'SC')) {
                                            document.getElementById("cmbCPFEntitlement").value = 'Y';
                                            document.getElementById('cmbQuickRefType').value = "1";

                                            //KM
                                            cpf_change();
                                        }
                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'DP') || (e.value == 'NA') || (e.value == 'OT')) {
                                            document.getElementById("cmbCPFEntitlement").value = 'N';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';

                                            document.getElementById('txtEmployerCPF').value = '';
                                            document.getElementById('txtEmployeeCPF').value = '';
                                            document.getElementById('cmbQuickRefType').value = "2";
                                            document.forms[0].cmbQuickRefType.disabled = true;


                                        }

                                        /* end vishal */


                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'SC') || (e.value == 'DP')) {

                                            datepicker();
                                        }
                                        if ((e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'DP')) {
                                            document.getElementById("txtFWLCode").disabled = false;
                                        }
                                        else {
                                            document.getElementById("txtFWLCode").disabled = true;
                                        }
                                        if ((e.value == 'SPR') || (e.value == 'SDPR') || (e.value == 'SC') || (e.value == 'DP') || (e.value == 'EP')) {
                                            document.getElementById("txtMonthlyLevy").disabled = true;
                                        }
                                        else {
                                            document.getElementById("txtMonthlyLevy").disabled = false;
                                        }
                                        if ((e.value == 'SPR') || (e.value == 'WP') || (e.value == 'EP') || (e.value == 'SP') || (e.value == 'SDPR') || (e.value == 'DP')) {
                                            test = "check";
                                        }
                                        else {
                                            test = "";
                                        }
                                        if ((e.value == 'NA')) {
                                            document.forms[0].cmbEmpRefType.disabled = false;
                                        }
                                    }
                                    function setbranchcode(e) {
                                        if (e.value == '-select-') {

                                            document.getElementById("txtgirobankname").value = '';
                                        }
                                        else {
                                            document.getElementById("txtgirobankname").value = e.value;
                                        }

                                    }

                                    function setbranchcodeSB(e) {
                                        //document.employeeform.txtSBgirobankname.value = e.value;
                                    }


                                    function giro_change(e) {
                                        if (e.value == '-1' || e.value == '-2') {
                                            giro = "notgiro";
                                            document.getElementById("txtGIROAccountNo").disabled = true;
                                            document.getElementById("txtgirobankname").disabled = true;
                                            document.getElementById("cmbbranchcode").disabled = true;
                                            document.getElementById("txtgirobranch").disabled = true;
                                            document.getElementById("txtgiroaccountname").disabled = true;
                                            document.getElementById("txtSBGIROAccountNo").disabled = true;
                                            document.getElementById("cmbSBbranchcode").disabled = true;
                                            document.getElementById("cmbSBPayMode").disabled = true;
                                            document.getElementById("txtSBRemarks").disabled = true;
                                            document.getElementById("txtSBgirobranch").disabled = true;
                                            document.getElementById("txtSBgiroaccountname").disabled = true;

                                        }
                                        else {
                                            giro = "giro";
                                            document.getElementById("txtGIROAccountNo").disabled = false;
                                            document.getElementById("txtgirobankname").disabled = false;
                                            document.getElementById("txtgirobranch").disabled = false;
                                            document.getElementById("txtgiroaccountname").disabled = false;
                                            document.getElementById("cmbbranchcode").disabled = false;
                                            document.getElementById("txtSBGIROAccountNo").disabled = false;
                                            document.getElementById("cmbSBbranchcode").disabled = false;
                                            document.getElementById("cmbSBPayMode").disabled = false;
                                            document.getElementById("txtSBRemarks").disabled = false;
                                            document.getElementById("txtSBgirobranch").disabled = false;
                                            document.getElementById("txtSBgiroaccountname").disabled = false;

                                        }
                                    }

                                    function giro_changeSB(e) {
                                        if (e.value == '-1' || e.value == '-2') {
                                            giro = "notgiro";
                                            document.getElementById("txtSBGIROAccountNo").disabled = true;
                                            //document.employeeform.txtSBgirobankname.disabled=true;
                                            document.getElementById("txtSBgirobranch").disabled = true;
                                            document.getElementById("txtSBgiroaccountname").disabled = true;

                                        }
                                        else {
                                            giro = "giro";
                                            document.getElementById("txtGIROAccountNo").disabled = false;
                                            //document.employeeform.txtSBgirobankname.disabled=false;
                                            document.getElementById("txtSBgirobranch").disabled = false;
                                            document.getElementById("txtSBgiroaccountname").disabled = false;
                                        }
                                    }

                                    function race_change(e) {
                                        var race = document.getElementById('cmbRace').options[document.getElementById('cmbRace').selectedIndex].text;
                                        race = race.toUpperCase();
                                        if (race == '-SELECT-' || race == 'OTHERS') {
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';
                                        }
                                        if (race == 'CHINESE') {
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';

                                            if (!document.getElementById("chkoptfund").checked && document.getElementById("cmbCPFEntitlement").value == 'Y') {
                                                fund_cal();
                                            }
                                        }

                                        if (race == 'INDIAN') {
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';
                                            if (!document.getElementById("chkoptfund").checked && document.getElementById("cmbCPFEntitlement").value == 'Y') {
                                                fund_cal();
                                            }
                                        }
                                        if (race == 'MALAY' || race == 'MALAYSIAN') {
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtCCHEST").value = '';
                                            if (!document.getElementById("chkoptfund").checked && document.getElementById("cmbCPFEntitlement").value == 'Y') {
                                                fund_cal();
                                            }

                                        }
                                        if (race == 'EURASIAN') {
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtCCHEST").value = '';
                                            if (!document.getElementById("chkoptfund").checked && document.getElementById("cmbCPFEntitlement").value == 'Y') {
                                                fund_cal();
                                            }
                                        }

                                    }

                                    //        function gup( name )
                                    //        { 
                                    //            name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");  
                                    //            var regexS = "[\\?&]"+name+"=([^&#]*)"; 
                                    //            var regex = new RegExp( regexS );  
                                    //            var results = regex.exec( window.location.href );  
                                    //            if( results == null )    return "";  
                                    //            else    return results[1];             
                                    //        }

                                    //        function ClientTabSelectedHandler(sender, eventArgs)
                                    //        {              
                                    //            var tabStrip = sender;
                                    //            var tab = eventArgs.Tab;
                                    //            var tabid=tab.ID;
                                    //            var qs=gup('empcode');
                                    //            if(tab.Text=='Salary Information')
                                    //            {
                                    //                fund_cal();                        
                                    //            }                
                                    //            if(((tab.Text=='Training')||(tab.Text=='Safety Pass'))&&(qs==""))
                                    //            {
                                    //                alert('This setup will be enabled only after adding the employee details');
                                    //            } 
                                    //        }



                                    function fund_cal() {
                                        //document.getElementById('id'+i).

                                        var payfrequency;
                                        for (var i = 0; i < document.getElementById("rbpayfrequency").length; i++) {

                                            if (document.employeeform.rbpayfrequency[i].checked) {
                                                payfrequency = document.employeeform.rbpayfrequency[i].value;
                                            }
                                        }
                                        var emptype = document.getElementById('cmbEmpType').value;
                                        var payrate = document.getElementById('txtPayRate').value;
                                        if (!document.getElementById('txtPayRate').value)
                                            payrate = 0;

                                        var race = document.getElementById('cmbRace').options[document.getElementById('cmbRace').selectedIndex].text;
                                        race = race.toUpperCase();
                                        var res = SMEPayroll.employee.AddEditEmployee.calculate_fund(payfrequency, emptype, parseInt(payrate), race);
                                        var resvalue = res.value;
                                        res.value = null;
                                        if (race == 'CHINESE') {
                                            document.getElementById("txtCDAC").value = resvalue;
                                            document.getElementById("hdcdac").value = resvalue;
                                            document.getElementById("txtCCHEST").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                        }
                                        if (race == 'INDIAN') {
                                            document.getElementById("txtSINDA").value = resvalue;
                                            document.getElementById("hdsinda").value = resvalue;
                                            document.getElementById("txtCCHEST").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                        }

                                        if (race == 'MALAY' || race == 'MALAYSIAN') {
                                            document.getElementById("txtMBMF").value = resvalue;
                                            document.getElementById("hdmbmf").value = resvalue;
                                            document.getElementById("txtCCHEST").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                        }
                                        if (race == 'EURASIAN') {
                                            document.getElementById("txtECF").value = resvalue;
                                            document.getElementById("hdecf").value = resvalue;
                                            document.getElementById("txtCCHEST").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                        }
                                    }

                                    function CalculateOTRate(e) {
                                        var hrrate;
                                        var ot1;
                                        var ot2;
                                        if (document.getElementById("rdoMOMHourlyRate").checked) {
                                            hrrate = document.getElementById("txtHourlyRate").value;
                                        }
                                        else {
                                            hrrate = document.getElementById("txtMannualHourlyRate").value;
                                        }


                                        if (hrrate <= 0) {
                                            hrrate = 1;
                                        }

                                        ot1 = Math.round((document.getElementById("txtOT1Rate").value * hrrate) * Math.pow(10, 3)) / Math.pow(10, 3);
                                        document.getElementById('lbltxtOt1Rate').innerText = ot1;
                                        ot2 = Math.round((document.getElementById("txtOT2Rate").value * hrrate) * Math.pow(10, 3)) / Math.pow(10, 3);
                                        document.getElementById('lbltxtOt2Rate').innerText = ot2;

                                    }

                                    function CalculateOTRateCust(e) {
                                        var hrrate;
                                        var ot1;
                                        var ot2;
                                        //            if(document.employeeform.drpHourlyMode.value == "A")
                                        //           {
                                        hrrate = document.getElementById("custxtHourlyRate").value;
                                        //            } 
                                        //            else
                                        //            {
                                        //                hrrate = 0;
                                        //            }


                                        if (hrrate <= 0) {
                                            hrrate = 1;
                                        }

                                        ot1 = Math.round((document.getElementById("custxtOT1Rate").value * hrrate) * Math.pow(10, 3)) / Math.pow(10, 3);
                                        // alert("dd="+ot1);
                                        //ot1 = document.employeeform.custxtOT1Rate.value * hrrate;
                                        //document.getElementById('cuslbltxtOt1Rate').innerText = ot1;
                                        // document.getElementById('txtManualOT1').innerText = ot1;
                                        document.getElementById('txtManualOT1').value = ot1;
                                        ot2 = Math.round((document.getElementById("custxtOT2Rate").value * hrrate) * Math.pow(10, 3)) / Math.pow(10, 3);
                                        //document.getElementById('cuslbltxtOt1Rate').innerText = ot2;
                                        // document.getElementById('txtManualOT2').innerText = ot2;
                                        document.getElementById('txtManualOT2').value = ot2;

                                    }

                                    //new 
                                    function CalculateOTRateManual(e) {
                                        //OT1
                                        var OT1value = document.getElementById("txtManualOT1").value;
                                        var NHvalue = document.getElementById("custxtHourlyRate").value;
                                        document.getElementById('custxtOT1Rate').innerText = parseFloat(OT1value) / parseFloat(NHvalue);



                                        //OT2
                                        var OT2value = document.getElementById("txtManualOT2").value;
                                        document.getElementById('custxtOT2Rate').innerText = parseFloat(OT2value) / parseFloat(NHvalue);
                                        //document.getElementById('custxtOT2Rate').innerText=((OT2value / NHvalue)*Math.pow(10,3))/Math.pow(10,3);

                                    }
                                    //


                                    function CalculateHourlyRateCust(e) {
                                        var monthlyPay = document.getElementById("custxtPayRate").value;
                                        if (!document.getElementById("custxtPayRate").value)
                                            monthlyPay = 0;
                                        monthlyPay = parseFloat(monthlyPay);
                                        if (monthlyPay == null)
                                            monthlyPay = 0;

                                        var result = (12 * monthlyPay) / (52 * 44);

                                        document.getElementById("custxtHourlyRate").value = Math.round(result * Math.pow(10, 2)) / Math.pow(10, 2);
                                        if (!document.getElementById("chkoptfund").checked && document.getElementById('cmbCPFEntitlement').value == 'Y') {
                                            //fund_cal();        
                                        }
                                        var workingdays = document.getElementById("drpworkingdays").value;
                                        if (document.getElementById("drpworkingdays").value == "")
                                            workingdays = 5;

                                        if (!document.getElementById('custxtPayRate').value)
                                            monthlyPay = 0;

                                        var res = CalculateDailyRate(monthlyPay, workingdays);
                                        var resvalue = res;
                                        res.value = null;
                                        document.getElementById("custxtDailyRate").value = resvalue;
                                    }

                                    function CalculateHourlyRate(e) {
                                        var monthlyPay = document.getElementById("txtPayRate").value;
                                        if (!document.getElementById("txtPayRate").value)
                                            monthlyPay = 0;
                                        monthlyPay = parseFloat(monthlyPay);
                                        if (monthlyPay == null)
                                            monthlyPay = 0;

                                        var result = (12 * monthlyPay) / (52 * 44);

                                        document.getElementById("txtHourlyRate").value = Math.round(result * Math.pow(10, 2)) / Math.pow(10, 2);
                                        if (!document.getElementById("chkoptfund").checked && document.getElementById('cmbCPFEntitlement').value == 'Y') {
                                            fund_cal();
                                        }
                                        var workingdays = document.getElementById("cmbworkingdays").value;
                                        if (document.getElementById("cmbworkingdays").value == "")
                                            workingdays = 5;

                                        if (!document.getElementById('txtPayRate').value)
                                            monthlyPay = 0;


                                        if (document.getElementById('txtPayRate').value != "") {
                                            var res = CalculateDailyRate(monthlyPay, workingdays);
                                            var resvalue = res;
                                            res.value = null;
                                            document.getElementById("txtDailyRate").value = resvalue;
                                        }

                                    }

                                    function CHKOptOut() {
                                        document.getElementById("chkFUNDRequired").disabled = false;
                                        if (document.getElementById("chkoptfund").checked) {
                                            document.getElementById("txtCCHEST").value = '';
                                            document.getElementById("txtCDAC").value = '';
                                            document.getElementById("txtMBMF").value = '';
                                            document.getElementById("txtSINDA").value = '';
                                            document.getElementById("txtECF").value = '';
                                            document.getElementById("chkFUNDRequired").checked = false;
                                            document.getElementById("chkFUNDRequired").disabled = true;
                                        }
                                        else {
                                            if (document.getElementById("chkcomputecpffh").checked == false) {
                                                document.getElementById("chkFUNDRequired").checked = false;
                                                document.getElementById("chkFUNDRequired").disabled = true;
                                            }
                                        }
                                        if (!document.getElementById("chkoptfund").checked && document.getElementById('cmbCPFEntitlement').value == 'Y') {
                                            fund_cal();
                                        }

                                    }



                                    function CHKOptOutFund() {
                                        if (document.getElementById("chkcomputecpffh").checked == false) {
                                            WarningNotification('Please select Compute CPF First Half');
                                            document.getElementById("chkFUNDRequired").checked = false;
                                            document.getElementById("chkFUNDRequired").disabled = true;
                                        }
                                    }

                                    function CHKOptOutCPF() {
                                        document.getElementById("chkFUNDRequired").disabled = false;
                                        if (document.getElementById("chkcomputecpffh").checked == false) {
                                            document.getElementById("chkFUNDRequired").checked = false;
                                            document.getElementById("chkFUNDRequired").disabled = true;
                                        }
                                    }

                                    function CHK_HourlyRate(e) {
                                        if (e.value == 'A') {
                                            var monthlyPay = document.getElementById('txtPayRate').value;
                                            var result = (12 * monthlyPay) / (52 * 44);
                                            document.getElementById("txtHourlyRate").value = Math.round(result * Math.pow(10, 2)) / Math.pow(10, 2);
                                            document.getElementById("txtMannualHourlyRate").value = '';
                                        }
                                        if (e.value == 'M') {
                                            document.getElementById("txtHourlyRate").value = '';
                                        }
                                    }

                                    function CHK_HourlyRateCust(e) {
                                        if (e.value == 'A') {
                                            var monthlyPay = document.getElementById('custxtPayRate').value;
                                            var result = (12 * monthlyPay) / (52 * 44);
                                            document.getElementById("custxtHourlyRate").value = Math.round(result * Math.pow(10, 2)) / Math.pow(10, 2);
                                        }
                                        if (e.value == 'M') {
                                            document.getElementById("custxtHourlyRate").value = '';
                                        }
                                    }

                                    function CHK_DailyRate(e) {
                                        if (e.value == 'A') {
                                            var monthlyPay = document.getElementById('txtPayRate').value;
                                            var workingdays = document.getElementById("cmbworkingdays").value;
                                            if (document.getElementById("cmbworkingdays").value == "")
                                                workingdays = 5;

                                            if (!document.getElementById('txtPayRate').value)
                                                monthlyPay = 0;

                                            var res = CalculateDailyRate(monthlyPay, workingdays);
                                            var resvalue = res;
                                            res.value = null;
                                            document.getElementById("txtDailyRate").value = resvalue;
                                            document.getElementById("txtMannualDailyRate").value = '';

                                        }
                                        if (e.value == 'M') {
                                            document.getElementById("txtDailyRate").value = '';
                                        }
                                    }

                                    function CHK_DailyRateCust(e) {
                                        if (e.value == 'A') {
                                            var monthlyPay = document.getElementById('custxtPayRate').value;
                                            var workingdays = document.getElementById("drpworkingdays").value;
                                            if (document.getElementById("drpworkingdays").value == "")
                                                workingdays = 5;

                                            if (!document.getElementById('custxtPayRate').value)
                                                monthlyPay = 0;

                                            var res = CalculateDailyRate(monthlyPay, workingdays);
                                            var resvalue = res;
                                            res.value = null;
                                            document.getElementById("custxtDailyRate").value = resvalue;

                                        }
                                        if (e.value == 'M') {
                                            document.getElementById("custxtDailyRate").value = '';
                                        }
                                    }

                                    function CHK_WorkingDaysCust(e) {
                                        if (document.getElementById("drpDailyMode").value == "A") {
                                            var monthlyPay = document.getElementById('custxtPayRate').value;
                                            var workingdays = document.getElementById("drpworkingdays").value;
                                            if (document.getElementById("drpworkingdays").value == "")
                                                workingdays = 5;

                                            if (!document.getElementById('custxtPayRate').value)
                                                monthlyPay = 0;

                                            var res = CalculateDailyRate(monthlyPay, workingdays);
                                            var resvalue = res;
                                            res.value = null;
                                            document.getElementById("custxtDailyRate").value = resvalue;
                                        }
                                    }

                                    function CHK_WorkingDays(e) {
                                        if (document.getElementById("rdoMOMDailyRate").checked) {
                                            var monthlyPay = document.getElementById('txtPayRate').value;
                                            var workingdays = document.getElementById("cmbworkingdays").value;
                                            if (document.getElementById("cmbworkingdays").value == "")
                                                workingdays = 5;

                                            if (!document.getElementById('txtPayRate').value)
                                                monthlyPay = 0;

                                            var res = CalculateDailyRate(monthlyPay, workingdays);
                                            var resvalue = res;
                                            res.value = null;
                                            document.getElementById("txtDailyRate").value = resvalue;
                                            document.getElementById("txtMannualDailyRate").value = '';
                                        }
                                    }

                                    function bimonthChange(e) {
                                        //            if(document.getElementById("rdoMOMDailyRate").checked)
                                        //            {
                                        //                 var monthlyPay = document.getElementById('txtPayRate').value;
                                        //                 var workingdays = document.getElementById("cmbworkingdays").value;
                                        //                 if (document.getElementById("cmbworkingdays").value == "")
                                        //                    workingdays = 5;
                                        //                
                                        //                 if (!document.getElementById('txtPayRate').value)
                                        //                    monthlyPay = 0;
                                        //                    
                                        //                 var res = CalculateDailyRate(monthlyPay,workingdays);
                                        //                 var resvalue = res; 
                                        //                 res.value = null;
                                        //                 document.getElementById("txtDailyRate").value = resvalue; 
                                        //                 document.getElementById("txtMannualDailyRate").value = ''; 
                                        //            }

                                        // alert('test')
                                    }


                                    function CalculateDailyRate(monthlyPay, workingdays) {
                                        var result;

                                        $.ajax({
                                            type: "POST",
                                            url: "AddEditEmployee.aspx/CalDailyRates",
                                            data: "{'payrate':'" + monthlyPay + "','workingdays':'" + workingdays + "'}",
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            async: false,
                                            cache: false,
                                            success: function (data) {
                                                result = data.d;
                                            },
                                            failure: function (data) {
                                                alert(data);
                                            }
                                        });
                                        return result;
                                    }
                                </script>

                            </telerik:RadCodeBlock>
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="cmbGiroBank">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="drpgirobranches" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        <!-- BEGIN QUICK SIDEBAR -->
        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->
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
    <script src="../scripts/metronic/bootstrap-fileinput.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.sparkline.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/profile.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".EditFormButtonRow input[type=submit]:first-child").addClass("btn red");
            $(".EditFormButtonRow input[type=submit]:last-child").addClass("btn default");
            $("#tabPageSaftety .rcTable, #tabPageTraining .rcTable").css({ "width": "105px" });
            $("#radItemIssued_ctl00_ctl02_ctl02_lblSt").parent().prev().empty();
        })


        $(".riTextBox").addClass("form-control");
        $(".RadPicker.RadPicker_Default.trstandtop, .RadPicker.RadPicker_Default, .RadComboBox.RadComboBox_Default, .RadPicker.RadPicker_Default .rcTable").removeAttr("style");
        $(document).ready(function () {
            var _cntrl = $("#radItemIssued_ctl00 input[type=text]");
            var lbl = "";
            if (_cntrl.length > 6) {
                for (var i = 7; i < _cntrl.length; i++) {
                    lbl = $(_cntrl[i]).closest("tr").find("label").text();
                    switch (lbl) {
                        case "Serial Number:":
                            $(_cntrl[i]).addClass("serialnumberissueditem numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 12);
                            break;
                        case "Remarks:":
                            $(_cntrl[i]).addClass("remarksnumberissueditem custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "Quantity:":
                            $(_cntrl[i]).addClass("quantityissueditem numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 6);
                            break;
                        default:
                            break;
                    }
                }
            }
            var _cntrlsafetyinfo = $("#RadGrid2_ctl00 input[type=text]");
            var lblsafetyinfo = "";
            if (_cntrlsafetyinfo.length > 2) {
                for (var i = 3; i < _cntrlsafetyinfo.length; i++) {
                    lblsafetyinfo = $(_cntrlsafetyinfo[i]).closest("tr").find("label").text();
                    switch (lblsafetyinfo) {
                        case "Safety Pass No:":
                            $(_cntrlsafetyinfo[i]).addClass("_txtsafetypassno custom-maxlength cleanstring form-control input-sm");
                            $(_cntrlsafetyinfo[i]).attr("maxlength", 50);
                            break;
                        default:
                            break;
                    }
                }
            }
            var _cntrltraininginfo = $("#RadGrid1_ctl00 input[type=text]");
            console.log(_cntrltraininginfo.length);
            var lbltraininginfo = "";
            if (_cntrltraininginfo.length > 4) {
                for (var i = 5; i < _cntrltraininginfo.length; i++) {
                    lbltraininginfo = $(_cntrltraininginfo[i]).closest("tr").find("label").text();
                    switch (lbltraininginfo) {
                        case "Result:":
                            $(_cntrltraininginfo[i]).addClass("form-control input-sm custom-maxlength");
                            $(_cntrltraininginfo[i]).attr("maxlength", 50);

                            break;
                        case "No Of Attempts:":
                            $(_cntrltraininginfo[i]).addClass("form-control input-sm numericonly");
                            $(_cntrltraininginfo[i]).attr("maxlength", 3);

                            break;
                        case "Venue:":
                            $(_cntrltraininginfo[i]).addClass("form-control input-sm custom-maxlength");
                            $(_cntrltraininginfo[i]).attr("maxlength", 50);

                            break;
                        default:
                            break;
                    }
                }
            }

            $(".clsdate").focusout(function () {
                if ($(this).val() != "") {
                    var curntdate = $(this).val().split('/');
                    curntdate = new Date(curntdate[2], curntdate[1] - 1, curntdate[0]);
                    if (curntdate == "Invalid Date") {
                        WarningNotification("Invalid Date");
                        $(this).val("");
                    }
                }

            });

            $(button2).click(function () {
                var alertmesg = "";
                if ($(txtDocumentName).val() == "") {
                    alertmesg = "Please enter document Name. <br/>";
                }
                if ($(file1file0).val() == "") {
                    alertmesg += "Please select file to upload. <br/>";
                }

                if (alertmesg != "") {
                    WarningNotification(alertmesg);
                    return false;
                }
            });

            //$(document).on("click", "#RadGrid2_ctl00_ctl02_ctl02_PerformInsertButton", function () {
            //    if ($.trim($("#RadGrid2_ctl00_ctl02_ctl02_ctl04").val()) === "") {
            //        event.preventDefault();
            //        WarningNotification("Please Input Safety Pass No.");
            //        return false;
            //    }
            //        if ($.trim($("#RadGrid2_ctl00_ctl02_ctl02_ctl04").val().length) > 50) {
            //            event.preventDefault();
            //            WarningNotification("Only 50 Characters allowed.");
            //            return false;
            //        }
            //});
            //$(document).on("click", "#RadGrid2_ctl00_ctl05_UpdateButton", function () {
            //    if ($.trim($("#RadGrid2_ctl00_ctl05_ctl04").val()) === "") {
            //        event.preventDefault();
            //        WarningNotification("Please Input Safety Pass No.");
            //        return false;
            //    }
            //});

            $(document).on("click", "#RadGrid2_ctl00_ctl02_ctl02_PerformInsertButton", function () {
                var _message = "";
                if ($.trim($("._txtsafetypassno").val()) === "")
                    _message += "Please Input Safety Pass No.<br>";

                if (_message != "") {
                    WarningNotification(_message);
                    return false;
                }
            });
            $(document).on("click", "#radItemIssued_ctl00_ctl02_ctl02_PerformInsertButton", function () {
                return validateItemissuedtab();
            });
            $(document).on("focusout", ".chkemail", function () {
                var userinput = $(this).val();
                var pattern = /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i;

                if (!pattern.test(userinput)) {
                    WarningNotification('Invalid Email! Please enter valid email address.');

                }
            });

        });
        function validateEmployeeImage() {
            var _message = "";
            var allowedFiles = [".jpg", ".jpeg", ".png"];
            var fileUpload = document.getElementById("FileUpload1");
            var regex = new RegExp("([a-zA-Z0-9\s_\\.\-:])+(" + allowedFiles.join('|') + ")$");
            if (document.getElementById("FileUpload1").value == "")
                _message = "Please Choose Image to Upload.";
            else if (!regex.test(fileUpload.value.toLowerCase()))
                _message = "Please upload image having extensions: <br>" + allowedFiles.join(', ') + "</b> only.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
        }
        function validateItemissuedtab() {
            var _message = "";
            if ($.trim($(".serialnumberissueditem").val()) === "")
                _message += "Please Input Serial Number.<br>";
            if ($.trim($(".quantityissueditem").val()) === "")
                _message += "Please Input Quantity.<br>";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

    </script>
    <script type="text/javascript">
        $(document).on("click", ".clsUpdateBtn, #RadGrid4_ctl00_ctl02_ctl01_PerformInsertButton", function () {
            // $(".clsUpdateBtn").click(function () { //#RadGrid4_ctl00_ctl02_ctl01_PerformInsertButton, 

            var _alertmsg = "";
            if ($(".clsfamilyname").val() == "")
                _alertmsg = "Family member name can not be empty.<br/>";
            if ($(".clsdatechk").val() == "")
                _alertmsg += "Family member Date of birth can not be empty.<br/>";
            else {
                var today = new Date();
                var curntdate = $(".clsdatechk").val().split('/');
                curntdate = new Date(curntdate[2], curntdate[1] - 1, curntdate[0]);
                if (today < curntdate)
                    _alertmsg += "Family member Date of birth can not be greater than today.<br/>";

            }
            if ($(".txtphone").val() != "" && $(".txtphone").val().length < 8) {
                _alertmsg += "Phone Number cannot be less than 8 didgits.<br/>";

            }
            if (_alertmsg != "") {
                WarningNotification(_alertmsg);
                return false;
            }

        });
        $(document).on('keypress', '.numberdotMonthlyLevy', function (e) {
            return numberswithdotMonthlyLevy(e);
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _dynamicmsg = $(_elem).closest('table').find('thead tr th:first').prop('abbr');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this " + _dynamicmsg + "?", _id, "Confirm Delete", "Delete");
        });
        $("#projectKey").change(function () {
            alert($('option:selected', this).text());
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#radItemIssued_ctl00 thead tr td,#RadGrid2_ctl00 thead tr td,#RadGrid1_ctl00 thead tr td,#RadGrid3_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

            $("#tabPagePersonal .RadUpload .ruFileWrap").after("<br>");
            if ($(".EditFormButtonRow ")) {
                $(".EditFormButtonRow td input[value=Update]").addClass("clsUpdateBtn");

            }
        }
            function numberswithdotMonthlyLevy(e) {
                var unicode = e.charCode ? e.charCode : e.keyCode
                var element = $(e.target);
                var _id = $(element).attr('id');
                var txt = document.getElementById(_id);
                if ($.trim($(element).val()) == "" && unicode == 46) {
                    $(element).val("");
                    return false;
                }

                if (unicode != 8) { //if the key isn't the backspace key (which we should allow)
                    if (unicode != 46 && (unicode < 48 || unicode > 57)) //if not a number
                        return false //disable key press
                    if ($(element).val().split('.').length >= 4 && unicode == 46)
                        return false

                    if ($(element).val().split('.')[1] && $(element).val().split('.')[1].length >= 4) {
                        if ((txt.value.split('.')[1].substr(txt.selectionStart, (txt.selectionEnd - txt.selectionStart)).length == 4)) {
                            return true;
                        }
                        else {
                            if (_showmessage == false)
                                WarningNotification('Only Four decimals are allowed(00.0000)');
                            _showmessage = true;
                            return false
                        }
                    }
                    return maxlength(element);
                }
            }
    </script>
</body>
</html>
