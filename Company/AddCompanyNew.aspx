<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AddCompanyNew.aspx.cs"
    Inherits="SMEPayroll.Company.AddCompanyNew"   ValidateRequest="false"   %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />
<link rel="stylesheet" href="../Style/metronic/bs-switches.css" type="text/css" />



     <script  type="text/javascript" language="javascript" src="../Frames/Script/CommonValidations.js">
    </script>
    <script language="javascript" type="text/javascript">
        //Added by Sandi on 31/3/2014
        function KeyPress(sender, args) {
            if (args.KeyCode == 45 || args.KeyCode == 46) {
                return false;
            }
        }
        function NewKeyPress(sender, args) {
            var keyCharacter = args.get_keyCharacter();

            if (keyCharacter == sender.get_numberFormat().DecimalSeparator ||
                keyCharacter == sender.get_numberFormat().NegativeSign) {
                args.set_cancel(true);
            }
        }
        //End Added 
        function gup(name) {


            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null) return "";
            else return results[1];
        }
        function ClientTabSelectedHandler(sender, eventArgs) {

            var tabStrip = sender;
            var tab = eventArgs.Tab;
            var tabid = tab.ID;
            var qs = gup('compid');
            if ((tab.Text == 'GiroSetup') && (qs == "")) {
                WarningNotification('This setup will be enabled only after adding the company details');
            }
        }



        //            function onSelectedIndexChanged(sender,eventArgs)
        //            {        
        //                    var item = eventArgs.get_item();
        //                    // alert the new item text and value.
        //                    //var label = document.getElementById("Label1");
        //                    //var txtMinutes = document.getElementById("txtMinutes");                  
        //                   var label  = document.getElementById("Label1");           
        //                   var txtMinutes  = document.getElementById("txtMinutes");           
        //                   
        //                    if(item.get_text()=='Yes')
        //                    {
        //                         alert(1); 
        //                          label.style.display = 'inherit'; 
        //                          txtMinutes.style.display = 'inherit';
        //                    }   
        //                    else
        //                    {
        //                           alert(1); 
        //                          label.style.display = 'none'; 
        //                          txtMinutes.style.display = 'none'; 
        //                    }                 
        //            }




        function checkNumeric(objName, minval, maxval, comma, period, hyphen, fieldName, msg) {
            // only allow 0-9 be entered, plus any values passed
            // (can be in any order, and don't have to be comma, period, or hyphen)
            // if all numbers allow commas, periods, hyphens or whatever,
            // just hard code it here and take out the passed parameters
            var alertsay = '';
            var checkOK = "0123456789" + comma + period + hyphen;
            var checkStr = objName;
            var allValid = true;
            var decPoints = 0;
            var allNum = "";


            for (i = 0; i < checkStr.value.length; i++) {
                ch = checkStr.value.charAt(i);
                for (j = 0; j < checkOK.length; j++)
                    if (ch == checkOK.charAt(j))
                        break;
                if (j == checkOK.length) {
                    allValid = false;
                    break;
                }
                if (ch != ",")
                    allNum += ch;
            }


            if (!allValid) {
                alertsay = msg;
                return (alertsay);
            }

            // set the minimum and maximum
            var chkVal = allNum;
            var prsVal = parseInt(allNum);

            if (chkVal != "" && !(prsVal >= minval && prsVal <= maxval)) {
                alertsay = "Please enter a value greater than or "
                alertsay = alertsay + "equal to \"" + minval + "\" and less than or "
                alertsay = alertsay + "equal to \"" + maxval + "\" in the \"" + fieldName + "\" field. <br/>"
            }

            return (alertsay);
        }



        function ChkCPFRefNo() {

            var newsMSG = "";
            //Check New Validations -Start
            var msg = "Address Setup-Prefix Code Required,Address Setup-Company Name Required!,Prefrence Setup-Annual CPF Ceiling Required!";


            var srcData = "";
            //Control Validation		    
            //validateData(srcCtrl,destSrc,opType,srcData,msg,con)
            var vaiable = document.getElementById("txtCompCode");
            var vaiable1 = document.getElementById("txtCompName");
            var vaiable2 = document.getElementById("txtpostalcode");
            var vaiable3 = document.getElementById("txtCompPhone");
            var vaiable4 = document.getElementById("txtCompfax");
            var vaiable5 = document.getElementById("txtCompemail");//Email		    
            var vaiable6 = document.getElementById("txtwebsite");//WebSite
            var vaiable7 = document.getElementById("cmbccleave");//Email
            var vaiable8 = document.getElementById("cmbccclaim");//Email
            var vaiable9 = document.getElementById("txtccmail");//Email 
            var vaiable17 = document.getElementById("txtannual_cpf_ceil");//AN

            var vaiable10 = document.getElementById("txtCompperson");//AN
            var vaiable11 = document.getElementById("txtdesign");//AN
            var vaiable12 = document.getElementById("txtcompany_roc");//AN
            var vaiable13 = document.getElementById("txtauth_emai");//Email Address

            var vaiable14 = document.getElementById("txtemailsender_address");//Email Address		        
            var vaiable15 = document.getElementById("txtemailsender_domain");//Email Address
            var vaiable16 = document.getElementById("txtsmtpport");//Email Address

            var srcCtrl = vaiable.id + ',' + vaiable1.id + ',' + vaiable17.id;
            //alert(vaiable.id);
            var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
            if (strirmsg != "")
                newsMSG = "Following fields are missing.<br/>" + strirmsg + "<br/>";


            //r
            //validation for timesheet((d) Setting)
            if (document.getElementById("cbxEmailAlert").value == "Yes") {
                if (document.getElementById("drpEmpProc1").value == "Processer") {
                    if (document.getElementById("txtProcesserEmail").value == "") {
                        newsMSG += "Please Enter Processer Email";
                    }
                }
            }


            //Check PrefixCode As Alpha Numeric Value 
            strirmsg = alphanumeric(vaiable, "Address Setup -Prefix Code");
            if (strirmsg != "")
                newsMSG += strirmsg;
            //Check PrefixCode As should not >=6
            strirmsg = CheckMaxMinLength(vaiable.value.length, "5", ">", "<br/>Address Setup -Prefix Code");
            if (strirmsg != "")
                newsMSG += strirmsg;
            //Postal code NUmeric
            strirmsg = CheckNumeric(vaiable2.value, "<br/>Address Setup -Postal Code");
            if (strirmsg != "")
                newsMSG += strirmsg;
            if ($(document.getElementById("txtpostalcode")).val() != "")
                if ($(document.getElementById("txtpostalcode")).val().length < 6)
                    newsMSG += "Address Setup-Postal Code character length cannot be less than 6 !<br/>";

            //Phone Number code NUmeric
            strirmsg = CheckNumeric(vaiable3.value, "<br/>Address Setup -Phone Number");
            if (strirmsg != "")
                newsMSG += strirmsg;
            if ($(document.getElementById("txtCompPhone")).val() != "")
                if ($(document.getElementById("txtCompPhone")).val().length < 8)
                    newsMSG += "Address Setup - Phone Number character length cannot be less than 8 !<br/>";
            //FAX Numeric
            strirmsg = CheckNumeric(vaiable4.value, "<br/>Address Setup -Fax");
            if (strirmsg != "")
                newsMSG += strirmsg;
            if ($(document.getElementById("txtCompfax")).val() != "")
                if ($(document.getElementById("txtCompfax")).val().length < 8)
                    newsMSG += "Address Setup - Fax Number character length cannot be less than 8 !<br/>";
            //Email
            strirmsg = ValidateEmail(vaiable5.value, "<br/>Address Setup -Email");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Website 
            strirmsg = ValidateWebAddress(vaiable6.value, "<br/>Address Setup -WebSite");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //************************************************************
            //Email ******************************************************
            if (ValidmultiEmail(vaiable7.value) == "false") {
                //strirmsg = "\n Preference Setup -CC For Leave Email -Please Enter Valid Email!";
                if (strirmsg != "")
                    newsMSG += strirmsg;
            }

            //Email ***
            if (ValidmultiEmail(vaiable8.value) == "false") {
                //strirmsg = "\n Preference Setup -CC For Claims Email -Please Enter Valid Email!";
                if (strirmsg != "")
                    newsMSG += strirmsg;
            }

            //Email ***
            if (ValidmultiEmail(vaiable9.value) == "false") {
                //strirmsg = "\n Preference Setup -CC For Payroll Email -Please Enter Valid Email!";
                if (strirmsg != "")
                    newsMSG += strirmsg;
            }
            //************************************************************

            //Alpha numeric                
            strirmsg = alphanumeric(vaiable10, "<br/>IR8A Setup-Authorized Person");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Alpha numeric                
            strirmsg = alphanumeric(vaiable11, "<br/>IR8A Setup -Authorized Person Designation");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Alpha numeric                
            strirmsg = alphanumeric(vaiable12, "<br/>IR8A Setup -Company ROC");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Email 
            strirmsg = ValidateEmail(vaiable13.value, "<br/>IR8A Setup -Authorized Person");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Email *****
            strirmsg = ValidateEmail(vaiable14.value, "<br/>Email Setup -Sender Address");
            if (strirmsg != "")
                newsMSG += strirmsg;


            //Email 
            strirmsg = ValidateEmail(vaiable15.value, "<br/>Email Setup -Payroll Approver");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Numeric
            strirmsg = CheckNumeric(vaiable16.value, "<br/>Email Setup - SMTP Port");
            if (strirmsg != "")
                newsMSG += strirmsg;

            if ($(document.getElementById("txtsmtpport")).val() != "")
                if ($(document.getElementById("txtsmtpport")).val().length < 2)
                    newsMSG += "Email Setup-SMTP Port character length cannot be less than 2 !<br/>";
            //if (newsMSG == "") {
            //    return true;
            //}
            //else {

            //    WarningNotification(newsMSG);
            //    return false;
            //}



            //Check New Validations -End
            var sMSG = "";
            if (!document.employeeform.txtCompCode.value) {
                newsMSG += "Address Setup-Prefix Code Required!<br/>";
            }
            else {
                if (document.employeeform.txtCompCode.value.length >= 6) {
                    newsMSG += "Address Setup-Prefix Code Length should not be more than 5 characters!<br/>";
                }
            }


            if (!document.employeeform.txtCompName.value)
                newsMSG += "Address Setup-Company Name Required!<br/>";
            if ((!document.employeeform.cmbworkingdays.value) || (document.employeeform.cmbworkingdays.value == '-select-')) {
                newsMSG += "Preferences Setup-Working Days Setup Required!<br/>";
            }




            //		if ( !document.employeeform.txthrs_day.value )
            //		{
            //			sMSG += "Preferences Setup-Hours in a day is Required!\n";	
            //	    }
            //	    else
            //	    {
            //		    var objhr=document.employeeform.txthrs_day;
            //		    sMSG += checkNumeric(objhr,1,12,'','','','Hours in a day','Preference Setup: Please enter numeric[no decimal] in Hours in a day');
            //	    }
            //	    
            //	    if ( !document.employeeform.txtmin_day.value )
            //		{
            //	    }
            //	    else
            //	    {
            //		    var objmi =document.employeeform.txtmin_day;
            //		    sMSG += checkNumeric(objmi,1,60,'','','','Minutes in a day','Preference Setup: Please enter numeric[no decimal] in Minutes in a day');
            //	    }		



            if (newsMSG == "")
                return true;
            else {
                newsMSG = "Following fields are missing.<br/><br/>" + newsMSG;
                WarningNotification(newsMSG);
                return false;
            }
        }



        function ShowPayslip() {
            var str = document.employeeform.cmbpayslipformat.value;

            switch (str) {
                case 'Format 1':
                    window.open("../Documents/Payslips/Payslip1.pdf");
                    break;
                case 'Format 2':
                    window.open("../Documents/Payslips/Payslip2.pdf");
                    break;
                case 'Format 3':
                    window.open("../Documents/Payslips/Payslip3.pdf");
                    break;
                case 'Format 4':
                    window.open("../Documents/Payslips/Payslip4.pdf");
                    break;
                case 'Format 4(MOM)':
                    window.open("../Documents/Payslips/Payslip4.pdf");
                    break;
                case 'Format 5':
                    window.open("../Documents/Payslips/Payslip5.pdf");
                    break;
                case 'Format 5(MOM)':
                    window.open("../Documents/Payslips/Payslip5.pdf");
                    break;
                case 'Format 6':
                    window.open("../Documents/Payslips/Payslip5.pdf");
                    break;
                case 'Itemized-MOM':
                    window.open("../Documents/Payslips/MOM-Itemised-payslips.pdf");
                    break;

                case 'Itemized-MOM-HEADER':
                    window.open("../Documents/Payslips/MOM-Itemised-Payslips with Header.pdf");
                    break;


            }

        }
        function onrdtimesheetchange() {
            var ctrl1 = document.getElementById('rdtimesheet');
            var ctrl2 = document.getElementById('rdtsremarks');

            if (ctrl1.value == "1") {
                ctrl2.disabled = false;
            }
            else {
                ctrl2.disabled = true;
                ctrl2.value = "0";
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

        //...Check Email Details ... ...
        function CheckEmailDetails() {
            var flag = 'true';
            var newsMSG = "";
            //Check New Validations -Start
            var srcData = "";
            var strirmsg = "";
            var msg = "Email Setup-Sender Address,Email Setup-Payroll Approver Email,Email Setup-SMTP Port,Email Setup-SMTP Server,Email Setup-UserName";

            var vaiable14 = document.getElementById("txtemailsender_address");//Email Address		        
            var vaiable15 = document.getElementById("txtemailsender_domain");//Email Address
            var vaiable16 = document.getElementById("txtsmtpport");//Email Address

            var vaiable17 = document.getElementById("txtsmtpserver");//Email Address		        
            var vaiable18 = document.getElementById("txtemailuser");//Email Address		

            var srcCtrl = vaiable14.id + ',' + vaiable15.id + ',' + vaiable16.id + ',' + vaiable17.id + ',' + vaiable18.id;
            //alert(vaiable.id);
            var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
            if (strirmsg != "")
                newsMSG = "Following fields are missing.<br/>" + strirmsg + "<br/>";

            //Email *****
            strirmsg = ValidateEmail(vaiable14.value, "<br/>Email Setup -Sender Address");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Email 
            strirmsg = ValidateEmail(vaiable15.value, "<br/>Email Setup -Payroll Approver");
            if (strirmsg != "")
                newsMSG += strirmsg;

            //Numeric
            strirmsg = CheckNumeric(vaiable16.value, "<br/>Email Setup - SMTP Port");
            if (strirmsg != "")
                newsMSG += strirmsg;

            if (newsMSG == "") {
                return true;
            }
            else {
                WarningNotification(newsMSG);
                return false;
            }
        }


    </script>
    
<telerik:RadScriptBlock runat="server" >

    <script type="text/javascript">
        function cmbworkingdays_Changed(sender, eventArgs) {
            var combo = $find("<%= cmdOffDay2.ClientID %>");
            var item = eventArgs.get_item();
            if (item.get_text() == "5") {
                //combo.set_enabled(true);
                combo.enable();

            }
            if (item.get_text() == "5.5") {
                //combo.set_enabled(true);
                combo.enable();
            }
            if (item.get_text() == "6") {
                // combo.set_enabled(false);
                combo.disable();
            }
            // alert("You selected " + item.get_text());
        }
        //Added by Sandi on 26/3/2014
        function ShowHideStandard1(rodStandard1) {
            var currentValue = rodStandard1.value;

            var OffDay1 = $find("<%= cmdOffDay1.ClientID %>");
            var OffDay2 = $find("<%= cmdOffDay2.ClientID %>");

            var tabStrip = $find("<%=tbsComp.ClientID %>");
            var tabs = tabStrip.get_tabs();
            if (currentValue == "Yes") {
                OffDay1.set_enabled(false);
                OffDay2.set_enabled(false);
                document.getElementById('rdoYes').checked = true;
                document.getElementById('rdoHide').checked = false;
                document.getElementById('rdoShow').checked = false;
                document.getElementById('rdoLeaveCal').checked = false;
                document.getElementById('rdoPayrollCal').checked = false;

                document.getElementById('rdoHide').disabled = "disabled";
                document.getElementById('rdoShow').disabled = "disabled";
                document.getElementById('rdoLeaveCal').disabled = "disabled";
                document.getElementById('rdoPayrollCal').disabled = "disabled";
                for (var i = 0; i < tabStrip.get_allTabs().length; i++) {
                    if (tabs.getTab(i).get_text() == "Offday Setup")
                        tabs.getTab(i).set_enabled(false);
                }
            }
            else {
                document.getElementById('rdoNo').checked = true;
                document.getElementById('rdoHide').disabled = "";
                document.getElementById('rdoShow').disabled = "";
                document.getElementById('rdoLeaveCal').disabled = "";
                document.getElementById('rdoPayrollCal').disabled = "";

                for (var i = 0; i < tabStrip.get_allTabs().length; i++) {
                    if (tabs.getTab(i).get_text() == "Offday Setup")
                        tabs.getTab(i).set_enabled(true);
                }
            }
        }
        //Added by Sandi on 13/3/2014
        function ShowHideStandard(rodStandard) {
            var currentValue = rodStandard.value;

            var OffDay1 = $find("<%= cmdOffDay1.ClientID %>");
            var OffDay2 = $find("<%= cmdOffDay2.ClientID %>");
            if (currentValue == "Yes") {
                OffDay1.set_enabled(false);
                OffDay2.set_enabled(false);
                document.getElementById('rdoHide').checked = false;
                document.getElementById('rdoShow').checked = false;
                document.getElementById('rdoLeaveCal').checked = false;
                document.getElementById('rdoPayrollCal').checked = false;

                document.getElementById('rdoHide').disabled = "disabled";
                document.getElementById('rdoShow').disabled = "disabled";
                document.getElementById('rdoLeaveCal').disabled = "disabled";
                document.getElementById('rdoPayrollCal').disabled = "disabled";
            }
            else {
                document.getElementById('rdoHide').disabled = "";
                document.getElementById('rdoShow').disabled = "";
                document.getElementById('rdoLeaveCal').disabled = "";
                document.getElementById('rdoPayrollCal').disabled = "";
            }
        }
        function ShowHideCustomized(rdoFixedDays) {
            var currentValue = rdoFixedDays.value;

            var OffDay1 = $find("<%= cmdOffDay1.ClientID %>");
            var OffDay2 = $find("<%= cmdOffDay2.ClientID %>");

            if (currentValue == "No") {
                //             alert("Current Value: NO");
                //            OffDay1._text = "-select-";
                //             OffDay1.set_selectedItem = "";
                OffDay1.set_enabled(false);
                OffDay2.set_enabled(false);
                //document.getElementById('rdoLeaveCal').checked=true;
            }
            else {
                //            alert("Current Value: YES");
                document.getElementById('rdoLeaveCal').checked = false;
                document.getElementById('rdoPayrollCal').checked = false;
                var cmbwds = $find("<%= cmbworkingdays.ClientID %>");
                var wds = cmbwds.get_text();
                if (wds == "6") {
                    OffDay1.set_enabled(true);
                }
                else {
                    OffDay1.set_enabled(true);
                    OffDay2.set_enabled(true);
                }
            }
        }
    </script>


    </telerik:RadScriptBlock>


</head>

<body class="company page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">

    

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
                        <li>Add/Edit Company</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ShowCompanies.aspx">Manage Company</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Add/Edit Company</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Add/Edit Company</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-sm-12">

                        <form id="employeeform" runat="server" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        
                            <div class="search-box clearfix margin-bottom-20 padding-tb-10">
                                <div class="col-sm-12 text-right">
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true" ></asp:Label>
                                <asp:Button ID="Button1" runat="server" OnClick="btnsave_Click" Text="Save" 
                                     OnClientClick="return ChkCPFRefNo();" CssClass="btn btn-sm red" />
                                <%--<input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red"
                                     />--%>
                                </div>
                            </div>

        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
        <asp:PlaceHolder ID="placeHolder1" runat="server"></asp:PlaceHolder>
        <input type="hidden" value="0" id="theValue" />
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <div class="exampleWrapper">
            <telerik:RadTabStrip ID="tbsComp" runat="server" SelectedIndex="0" MultiPageID="tbsCompany"
                Orientation="VerticalLeft" CssClass="col-sm-2" Skin="Outlook">
                <Tabs>
                    <radG:RadTab TabIndex="1" runat="server" AccessKey="P" Text="&lt;u&gt;A&lt;/u&gt;ddress Setup"
                        PageViewID="tbsAddar" Selected="True">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="2" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;references Setup"
                        PageViewID="tbspreferences">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="3" runat="server" AccessKey="I" Text="&lt;u&gt;I&lt;/u&gt;R8A Setup"
                        PageViewID="tbsir8a">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="4" runat="server" AccessKey="G" Text="&lt;u&gt;G&lt;/u&gt;iro Setup"
                        PageViewID="tbsgiro">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="5" runat="server" AccessKey="E" Text="&lt;u&gt;E&lt;/u&gt;mail Setup"
                        PageViewID="tbsPayslip">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="6" CssClass="check-scroll" runat="server" AccessKey="U" Text="&lt;u&gt;U&lt;/u&gt;ser Setup"
                        PageViewID="tbsuser">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="7" runat="server" AccessKey="C" Text="&lt;u&gt;C&lt;/u&gt;SN Setup"
                        PageViewID="tblCSN">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="8" runat="server" AccessKey="T" Text="Cos&lt;u&gt;t&lt;/u&gt; Center"
                        PageViewID="tblCost">
                    </radG:RadTab>
                     <radG:RadTab TabIndex="9" runat="server" AccessKey="S" Text="TimeSheet"
                        PageViewID="tblTimeSheet">
                    </radG:RadTab>
                     <radG:RadTab TabIndex="10" runat="server" AccessKey="P" Text="PaySlipSetup"
                        PageViewID="tblPaySlipSetup">
                    </radG:RadTab>
                     <radG:RadTab  TabIndex="11" runat="server"   AccessKey="M" Text="MultiCurrency"   
                        PageViewID="tblMultiCurrency">
                    </radG:RadTab>
                    <radG:RadTab  TabIndex="12" runat="server"   AccessKey="M" Text="Offday Setup"   
                        PageViewID="tblLeaveSetting" Visible="true">
                    </radG:RadTab>
                    
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="tbsCompany" SelectedIndex="0" 
                Height="100%" CssClass="multiPage col-sm-10">



                <telerik:RadPageView ID="tbsAddar" runat="server" Width="100%">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Company</h4>
                                                
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Prefix Code<span style="color:Red;">*</span></label>
                                                    <asp:TextBox ID="txtCompCode" CssClass="textfields form-control input-sm" runat="server" MaxLength="5"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Name<span style="color:Red;">*</span></label>
                                                    <asp:TextBox ID="txtCompName" CssClass="textfields form-control input-sm custom-maxlength" MaxLength="50"  runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alias</label>
                                                    <asp:TextBox ID="txtAlias" CssClass="textfields form-control input-sm custom-maxlength" MaxLength="50" runat="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                            </div>
                                            
                                            <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Address</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address Line 1</label>
                                                    <asp:TextBox ID="txtcompaddress" CssClass="textfields form-control input-sm custom-maxlength" runat="server" MaxLength ="150" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Address Line 2</label>
                                                    <asp:TextBox ID="txtcompaddress2" runat="server" CssClass="textfields form-control input-sm custom-maxlength" MaxLength ="150" ></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>City</label>
                                                    <input type="text" id="txtCompcity" runat="server" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength ="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>State</label>
                                                    <input type="text" id="txtCompstate" runat="server" class="textfields form-control input-sm custom-maxlength alphabetsonly" maxlength ="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Postal Code</label>
                                                    <asp:TextBox ID="txtpostalcode" runat="server" CssClass="textfields form-control input-sm custom-maxlength numericonly" maxlength ="6"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Country</label>
                                                    <telerik:RadComboBox ID="cmbCountry" class="textfields"  runat="server"
                                                                             EmptyMessage="Choose a Region" MarkFirstMatch="true"
                                                                            EnableLoadOnDemand="true">
                                                                        </telerik:RadComboBox>
                                                </div>
                                            </div>
                                             
                                             </div>
                                             
                                              <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Contact</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Email</label>
                                                    <input type="text" id="txtCompemail" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength ="50" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Phone</label>
                                                    <input type="text" id="txtCompPhone" runat="server" class="textfields form-control input-sm custom-maxlength numericonly" maxlength ="10" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Fax</label>
                                                    <input type="text" id="txtCompfax" runat="server" class="textfields form-control input-sm custom-maxlength numericonly" maxlength ="10" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Website</label>
                                                    <input type="text" id="txtwebsite" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength ="50"/>
                                                </div>
                                            </div>
                                             </div>

                                    </telerik:RadPageView>

                <telerik:RadPageView ID="tbspreferences" runat="server" Width="100%">
                                        <div class="row" style="display:none">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Workingday Setup</h4>
                                            </div>
                                        </div>
                                        <div class="row" style="display:none">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Required</label>
                                                    <select id="rdtimesheet1" onchange="javascript:onrdtimesheetchange();" runat="server"
                                                                        class="textfields form-control input-sm" >
                                                                        <option value="1" selected="selected">Yes</option>
                                                                        <option value="2">No</option>
                                                                    </select>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Remarks</label>
                                                    <select id="rdtsremarks1" runat="server" class="textfields form-control input-sm" >
                                                                        <option value="0" selected="selected">No</option>
                                                                        <option value="1">Yes</option>
                                                                    </select>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) Leave Model</h4>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave Model</label>
                                                    <telerik:RadComboBox ID="cmbLeaveModel" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Fixed Yearly - Normal" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Fixed Yearly - Prorated" Value="7" />
                                                                            <telerik:RadComboBoxItem Text="Fixed Yearly - Prorated (Floor)" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="Fixed Yearly - Prorated (Ceiling)" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="Yearly of Service - Normal" Value="3" />
                                                                            <telerik:RadComboBoxItem Text="Yearly of Service - Prorated" Value="8" />
                                                                            <telerik:RadComboBoxItem Text="Yearly of Service - Prorated (Floor)" Value="4" />
                                                                            <telerik:RadComboBoxItem Text="Yearly of Service - Prorated (Ceiling)" Value="6" />
                                                                            <telerik:RadComboBoxItem Text="Hybrid - Normal" Value = "10" />
                                                                            <telerik:RadComboBoxItem Text="Hybrid - Prorated" Value="9" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>No Of Working Days <span style="color:Red;">*</span></label>
                                                    <telerik:RadComboBox ID="cmbworkingdays1" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="5" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="5.5" Value="5.5" />
                                                                            <telerik:RadComboBoxItem Text="6" Value="6" />
                                                                            <%--<telerik:RadComboBoxItem Text="7" Value="7" />--%>
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>Do you follow standard leave setting? (ie. Saturday, Sunday off days)</label>
                                                    <label class='mt-radio mt-radio-outline'>
                                                    <input type="radio" runat="server" id="rdoYes1" value="Yes" name="rodStandard1" onclick="ShowHideStandard1(this);" />Yes
                                                       <span></span>
                                                         </label>
                                                    <label class='mt-radio mt-radio-outline'>      
                                                       <input type="radio" runat="server" id="rdoNo1" value="No" name="rodStandard1" onclick="ShowHideStandard1(this);" />No  
                                                <span></span>
                                                         </label>
                                                </div>
                                            </div>

                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B)  Leave Forfeit</h4>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 col-lg-2">
                                                <div class="form-group">
                                                    <label>Date</label>
                                                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="radLFort"
                                                                        runat="server">
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
                                            <div class="col-sm-3 col-lg-4">
                                                <div class="form-group">
                                                    <label><asp:Label ID="Label2" runat="server" Text="" Visible="true"></asp:Label></label>
                                                    <radG:RadNumericTextBox Visible="true" NumberFormat-GroupSeparator="" ID="rtxtLeaveDayAhead"
                                                                           runat="server" MinValue="0" MaxValue="366" NumberFormat-AllowRounding="true" 
                                                                           NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">   
                                                                           <ClientEvents OnKeyPress="NewKeyPress" />                                                                         
                                                                         </radG:RadNumericTextBox>
                                                    <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                                         <asp:CheckBox ID="chkIncludingCurrentDay" runat="server" Checked="true"  Text ="Including Current Day"/>
                                             <span></span>
                                                         </label>
                                                    
                                                      </div>
                                            </div>
                                            <div class="col-sm-3 col-lg-4">
                                                <div class="form-group">
                                                    <label>Unpaid Leave Amount</label>
                                                    <label class="mt-checkbox mt-checkbox-single mt-checkbox-outline">
                                                         <asp:CheckBox ID="chkLeave" runat="server" Checked="true"  Text ="Unpaid Leave-Deduct unpaid Leave Amount"/>
                                                       <span></span>
                                                         </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3 col-lg-2">
                                                <div class="form-group">
                                                    <%--<label>&nbsp;</label>--%>
                                                    <asp:Button ID="btnLeaveFF" CssClass="btn default btn-sm" runat="server" Text="Forfeit Leaves" Enabled="false" />
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(C) Email Alerts</h4>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alert For Leaves</label>
                                                    <telerik:RadComboBox ID="cmbemailleave" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CC For Leave</label>
                                                    <input type="text" id="cmbccleave" class="textfields form-control input-sm custom-maxlength" maxlength="150" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alert For Claims</label>
                                                    <telerik:RadComboBox ID="cmbclaim" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CC For Claims</label>
                                                    <input type="text" id="cmbccclaim" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="150"   />
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alert For Payroll</label>
                                                    <telerik:RadComboBox ID="cmbemailpay" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="false">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CC For Payroll</label>
                                                    <input type="text" id="txtccmail" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="150" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Auto Reminder</label>
                                                    <telerik:RadComboBox ID="autoreminder" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="false">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CC For Timesheet</label>
                                                    <input type="text" id="txtcctimesheet" runat="server" class="textfields form-control input-sm custom-maxlength" maxlength="150"  />
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(D) Rounding Decimal</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Basic Pay/Unpaid Leaves</label>
                                                    <telerik:RadComboBox ID="cmbLeaveRoundoff" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="0" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                            <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                            <telerik:RadComboBoxItem Text="N/A" Value="-1" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Additions</label>
                                                    <telerik:RadComboBox ID="cmbAdditionsRoundoff" runat="server"  EmptyMessage="-select-"
                                                                            MarkFirstMatch="true" EnableLoadOnDemand="true">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="0" Value="0" />
                                                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="N/A" Value="-1" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                     <label>(Claims, Overtime,Variables, Other Additions)</label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Deductions</label>
                                                    <telerik:RadComboBox ID="cmbDeductionsRoundoff" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="0" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                            <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                            <telerik:RadComboBoxItem Text="N/A" Value="-1" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Net Pay</label>
                                                    <telerik:RadComboBox ID="cmbNetPayRoundoff" runat="server"  EmptyMessage="-select-"
                                                                            MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="0" Value="0" />
                                                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="N/A" Value="-1" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <% if (Session["Country"].ToString() != "383")
                                            { %>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(E) CPF Ceiling</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Monthly CPF Ceiling</label>
                                                    <input type="text" id="txtmonthly_cpf_ceil" runat="server" disabled="disabled" class="textfields form-control input-sm" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Annual CPF Ceiling <span style="color:Red;">*</span></label>
                                                    <input type="text" disabled="disabled" id="txtannual_cpf_ceil" runat="server" class="textfields form-control input-sm" />
                                                </div>
                                            </div>
                                        </div>
                                        <%} %>
                                        
                                        <div class="row" style="display:none">
                                            <div class="col-sm-12">
                                                <input type="text" id="txthrs_day" runat="server" style="width: 50px" />
                                                <input type="text" id="txtmin_day" runat="server" style="width: 50px" />
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(F) Approval Process</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll Approval</label>
            <asp:RadioButtonList ID="radListPayrollApp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="radListPayrollApp_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="1">Required</asp:ListItem>
                <asp:ListItem Value="0">NotRequired</asp:ListItem>
            </asp:RadioButtonList>

  


                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave Approval</label>
                                                    <asp:RadioButtonList  ID="radListLeaveApp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Enabled="true" >
                                                                        <asp:ListItem Selected="True" Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claims Approval</label>
                                                    <asp:RadioButtonList ID="radListClaimApp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Enabled="true" >
                                                                        <asp:ListItem Selected="True" Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                              <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Approval</label>
                                                    <asp:RadioButtonList ID="radListTSApp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Enabled="true" >
                                                                        <asp:ListItem Selected="True" Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Appraisal</label>
                                                    <asp:RadioButtonList ID="radListALAp" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" Enabled="true" >
                                                                        <asp:ListItem Selected="True" Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(G) Payroll</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll Type</label>
                                                    <telerik:RadComboBox ID="cmbPayrollType" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Monthly" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Bi-Monthly" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="Both" Value="0" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>WorkFlow</label>
                                                    <asp:RadioButtonList ID="rdWorkFlow" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" >
                                                                        <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>
                                                                        <asp:ListItem Value="1">WorkFlow1</asp:ListItem>
                                                                        <asp:ListItem Value="2">WorkFlow2</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>WorkFlow For</label>
                                                    <asp:CheckBoxList ID="chkWF" runat="server" RepeatDirection="Horizontal" >
                                                                        <asp:ListItem Enabled="false">Employee</asp:ListItem>
                                                                        <asp:ListItem Enabled="false">Leave</asp:ListItem>
                                                                        <asp:ListItem Enabled="false">Claim</asp:ListItem>
                                                                        <asp:ListItem>Payroll</asp:ListItem>
                                                                       <asp:ListItem Enabled="false">Report</asp:ListItem>
                                                                        <asp:ListItem Enabled="false">TimeSheet</asp:ListItem>
                                                                        <asp:ListItem Enabled="false">Appraisal</asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Foreign Worker Levy(FWL)</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox ID="chkWL" runat="server" Checked="true"  Text ="Foreign Worker Levy(FWL)" />
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claims Type</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox ID="chkClaims" runat="server" Checked="false"  Text =" Petty cash" />
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Advanced Claims</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox ID="chkAdClaims" runat="server" Checked="false"  Text =" Advanced Claims " />
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>MultiCurrency</label>
                                                    <asp:RadioButtonList  ID="rdMultiCurr"   runat="server" RepeatDirection="Horizontal" AutoPostBack="true" >
                                                                        <asp:ListItem Enabled="true" Value="0">No</asp:ListItem>
                                                                        <asp:ListItem Enabled="true" Value="1">Yes</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                            
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Incomplete Month (ManualRate)</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox ID="incommanuvalRate" runat="server" Checked="false"  Text =" Incomplete Month (ManualRate)" />
                                              <span></span>
                                                         </label>
                                                         </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claim By Approval Date</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox ID="chkApprovalDate" runat="server" Checked="false"  Text ="Approval Date" />
                                                        <span></span>
                                                        </label>
                                                </div>
                                                </div>
                                        </div>
                                        
                                        <%if (Request.QueryString["compid"] == "1")
                                            {%>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(G) Company Type</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Master Company</label>
                                                    <telerik:RadComboBox ID="cmbIsMaster" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true"    >
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="1" />                                                                            
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Temporary Employee</label>
                                                    <telerik:RadComboBox ID="cmbtempEmp" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true"    >
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="1" />                                                                            
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Login With Out Comapny Name</label>
                                                    <telerik:RadComboBox ID="loginWithOutComany" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true"    >
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="No" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="1" />                                                                            
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        <%}%>
                                        
                                        
                                        <div class="row" style="display:none">
                                            <div class="col-sm-12">
                                                <h4 class="block">(G) Timesheet & Project Setup</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row" style="display:none">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Project Assignment</label>
                                                    <telerik:RadComboBox ID="cmbAssignType1" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="One Time" Selected="true" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Daily" Value="2" />
                                                                        
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(H) Group Management</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Grouping</label>
                                                    <asp:RadioButtonList ID="rdbGrouping" runat="server" RepeatDirection="Horizontal" AutoPostBack ="true" >
                                                                        <asp:ListItem Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >
                                                </div>
                                            </div>
                                        </div>

                                          <div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(I) Generate Ledger</h4>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>   Reports:</label>
                                                    <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" AutoPostBack ="true" >
                                                                        <asp:ListItem Value="1">Required</asp:ListItem>
                                                                        <asp:ListItem Value="0">NotRequired</asp:ListItem>
                                                                    </asp:RadioButtonList >--%>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                       <asp:CheckBox ID="chk_LGOT" runat="server" Checked="false"  Text ="OT Seperate" />
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                        </div>
                                        

                                    </telerik:RadPageView>

                <telerik:RadPageView ID="tbsir8a" runat="server" Width="100%">
<div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) IR8A Information</h4>
                                            </div>
                                        </div>
<div class="row">
<div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Authorized person</label>
                                                    <input type="text" id="txtCompperson" maxlength="50" runat="server" class="form-control input-sm custom-maxlength alphabetsonly" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Authorized person Designation</label>
                                                    <input type="text" maxlength="50" id="txtdesign" runat="server" class="form-control input-sm custom-maxlength alphabetsonly" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Authorized person's Email-ID</label>
                                                    <input type="text" id="txtauth_emai" maxlength="50" runat="server" class="form-control input-sm custom-maxlength" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company ROC</label>
                                                    <asp:TextBox ID="txtcompany_roc"  runat="server" maxlength="50" class="form-control input-sm custom-maxlength numericonly"></asp:TextBox>

                                                </div>
                                            </div>
                                            </div>

<div class="row">
<div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Company Type</label>
                                                    <telerik:RadComboBox ID="drpcompany_type" DataTextField="text" DataValueField="id"
                                                                        DataSourceID="xmldtCompType" class="textfields"  runat="server"
                                                                         EmptyMessage="Choose a Company" MarkFirstMatch="true"
                                                                        EnableLoadOnDemand="true">
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            
</div>
</telerik:RadPageView>
               
                 <telerik:RadPageView class="tbl" runat="server" ID="tbsgiro"  Width="100%">
                    
                     <div class="bg-row-red padding-tb-10 clearfix">
                                <div class="col-sm-12">
                                    <h4>Giro Setup</h4>
                                </div>
                            </div>
                    
                        <radG:RadGrid ID="RadGrid1" GridLines="None" AutoGenerateColumns="False" Skin="Outlook"
                            AllowPaging="True" PageSize="20" AllowFilteringByColumn="false" OnUpdateCommand="RadGrid1_UpdateCommand"
                            OnInsertCommand="RadGrid1_InsertCommand" OnNeedDataSource="RadGrid1_NeedDataSource"
                            runat="server" Width="100%">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id, giroid" CommandItemDisplay="Bottom">
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <Columns>
                                    <radG:GridBoundColumn Visible="false" DataField="giroid" HeaderText="giroid" SortExpression="giroid"
                                        UniqueName="giroid">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="desc" HeaderText="Bank Name" SortExpression="bank_name"
                                        UniqueName="desc">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="bank_code" HeaderText="Bank Code" SortExpression="code"
                                        UniqueName="code">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Visible="false" DataField="bank_branch" HeaderText="Bank Branch"
                                        SortExpression="bank_branch" UniqueName="bank_branch">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="bank_branch" HeaderText="Branch Code" SortExpression="branchcode"
                                        UniqueName="branchcode">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Visible="false" DataField="branchid" HeaderText="Branch ID"
                                        SortExpression="branchid" UniqueName="branchid">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="company_id" ReadOnly="true" Display="false" HeaderText="company_id"
                                        SortExpression="company_id" UniqueName="company_id">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="value_date" DataType="System.Datetime" HeaderText="Value Date"
                                        ReadOnly="True" SortExpression="value_date" UniqueName="value_date" Visible="False">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="id" DataType="System.Int32" HeaderText="id" ReadOnly="True"
                                        SortExpression="id" UniqueName="id" Visible="False">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="bank_accountno" HeaderText="Bank AccNo" SortExpression="bank_accountno"
                                        UniqueName="bank_accountno">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn Visible="false" DataField="company_bankcode" HeaderText="company_bankcode"
                                        SortExpression="company_bankcode" UniqueName="company_bankcode">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  Visible="false" DataField="CurrencyID" HeaderText="CurrencyID" SortExpression="CurrencyID"
                                        UniqueName="CurrencyID">
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton">
                                    </radG:GridEditCommandColumn>
                                </Columns>
                                <CommandItemSettings AddNewRecordText="Add New Bank details" />
                                <EditFormSettings UserControlName="giro.ascx" EditFormType="WebUserControl">
                                </EditFormSettings>
                            </MasterTableView>
                        </radG:RadGrid>
                    
                </telerik:RadPageView>
                
                <telerik:RadPageView ID="tbsPayslip" runat="server" Width="100%">
<div class="row">
                                            <div class="col-sm-12">
                                                <h4 class="block">(A) SMTP Mail Server Default Settings</h4>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Sender Address<span style="color:Red;">*</span></label>
                                                    <input type="text" id="txtemailsender_address" runat="server" class="form-control input-sm custom-maxlength"  maxlength="50"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>SMTP Server<span style="color:Red;">*</span></label>
                                                    <input type="text" id="txtsmtpserver" runat="server" class="form-control input-sm custom-maxlength"  maxlength="50"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>SMTP Port<span style="color:Red;">*</span></label>
                                                    <input type="text" id="txtsmtpport" runat="server" class="form-control input-sm custom-maxlength"  maxlength="6"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>SSL\TLS Enabled</label>
                                                    <telerik:RadComboBox ID="ddlssl" runat="server"  EmptyMessage="-select-">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                            <telerik:RadComboBoxItem Selected="true" Text="No" Value="No" />
                                                                            <telerik:RadComboBoxItem Selected="true" Text="TLS" Value="TSL" />
                                                                            <telerik:RadComboBoxItem Selected="true" Text="StartTLS" Value="StartTLS" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            </div>
                                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>User<span style="color:Red;">*</span></label>
                                                    <input type="text" id="txtemailuser" runat="server" class="form-control input-sm custom-maxlength"  maxlength="50"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Password</label>
                                                    <asp:TextBox runat="server" ID="emailpwd" TextMode="Password" CssClass="form-control input-sm custom-maxlength"  maxlength="12"></asp:TextBox>
                                                                  <%--  <input type="password" class="textfields" id="txtemailpwd" runat="server" />--%>
                                                                    <asp:Button ID="txtTestEmail" runat="server" OnClick="txtTestEmail_Click" OnClientClick="return CheckEmailDetails()" Text="Test-Email"
                                                                        CssClass="textfields btn default margin-top-10"  />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll Approver Email<span style="color:Red;">*</span></label>
                                                    <input type="text" id="txtemailsender_domain" runat="server" class="form-control input-sm custom-maxlength"  maxlength="50"/>
                                                </div>
                                            </div>
                                                                                        
                                             <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:Button ID="Button3" runat="server" OnClick="txtTestEmail_Click" Text="Test-Email"
                                                                        CssClass="textfields" Width="80px" Height="22px" Visible="False" />
                                                </div>
                                            </div>
                                            </div>
                                            
                                            <div class="row display-none">
                                            <div class="col-sm-12">
                                                <h4 class="block">(B) Email Alert Template</h4>
                                            </div>
                                       
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave request Message</label>
                                                    <textarea visible="false" id="txtemail_sendername" runat="server" style="width: 350px; height: 60px"
                                                                                class="textfields" value="Greetings,
Leave application submitted by: @emp_name.
Type of leave applied:@leave_type.
Leave balance as of today: @leave_balance.
Period of leave application: @from_date to @to_date.
Paid leave:@paid_leaves, Unpaid leave:@unpaid_leaves.
AM or PM (applicable only for 0.5 day leave): @timesession

Thanks and Regards
Advanced & Best Technologies Pte Ltd
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 
www.anbgroup.com
                            "></textarea><radG:RadEditor ID="EditorLevReq" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"    >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave Update Message</label>
                                                    <textarea visible="false" id="txtemail_replyaddress" runat="server" style="width: 350px; height: 60px"
                                                                                class="textfields" value="Greetings, @approver has @status your applied leaves from @from_date to @to_date <br />REMARKS: @reason;                           
Thanks and Regards
Advanced & Best Technologies Pte Ltd
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 
www.anbgroup.com
                            "></textarea><radG:RadEditor ID="Editortxtemail_replyaddress" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"   >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Leave Deleted Message</label>
                                                    <textarea visible="false" id="txtemail_leavedel" runat="server" style="width: 450px; height: 60px"
                                                                                class="textfields" value="Greetings,  Leave Applied Deleted of: @emp_name. <br /> Type of Leave Applied:@leave_type.  <br />Period of Leave Application: @from_date to @to_date. <br /> Paid leave:@paid_leaves, <br />Unpaid leave:@unpaid_leaves.<br /> Status: @status.<br /><br /><br />
Thanks and Regards<br />
Advanced & Best Technologies Pte Ltd<br />
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 <br />
www.anbgroup.com<br />
                            "></textarea><radG:RadEditor ID="Editortxtemail_leavedel" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"   >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Submit Payroll Message</label>
                                                    <textarea visible="false" id="txtemail_replyname" runat="server" style="width: 450px; height: 60px"
                                                                                class="textfields" value="Greetings, Payroll for the period  @month / @year has been submitted  by @hr for your appropal.
Please review the payroll and update the status.

Thanks and Regards
Advanced & Best Technologies Pte Ltd
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 
www.anbgroup.com
                            "></textarea><radG:RadEditor ID="Editortxtemail_replyname" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"   >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claim Request Message</label>
                                                    <textarea visible="false" id="txtclaim_sendername" runat="server" style="width: 450px; height: 60px"
                                                                                class="textfields" value="Greetings,@emp_name has requested claim for the month of  @month @year; 

Thanks and Regards
Advanced & Best Technologies Pte Ltd
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 
www.anbgroup.com
                            "></textarea><radG:RadEditor ID="Editortxtclaim_sendername" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"   >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Claim Update Message</label>
                                                    <textarea visible="false" id="txtemailclaim_replyname" runat="server" style="width: 450px; height: 60px"
                                                                                class="textfields" value="Greetings, @approver has @status your applied claim for the month of @month @year;                           

Thanks and Regards
Advanced & Best Technologies Pte Ltd
Office: 6837 2336 | 6223 7996 Fax: 6220 4532 
www.anbgroup.com
                            "></textarea><radG:RadEditor ID="Editortxtemailclaim_replyname" runat="server" Height="200px" Width="450px" ToolsFile="~/XML/BasicTools.xml"   >
                                                                      </radG:RadEditor><span>Do not Delete or Update @emp_name,@from_date,@to_date</span>
                                                </div>
                                            </div>
                                            
                                            
                                            </div>
                                            




</telerik:RadPageView>

                <telerik:RadPageView class="tbl" runat="server" ID="tbsuser"  Width="100%">
                    <radG:RadGrid ID="RadGrid2"  PageSize="10" runat="server" GridLines="None" EnableAJAX="True"
                        OnPreRender="RadGrid2_PreRender" OnNeedDataSource="RadGrid2_NeedDataSource" OnUpdateCommand="RadGrid2_UpdateCommand"
                        OnItemDataBound="RadGrid2_ItemDataBound" OnItemCommand="RadGrid2_ItemCommand"
                        AutoGenerateColumns="False" Skin="Outlook" AllowFilteringByColumn="True" AllowMultiRowSelection="true"
                        Width="100%" AllowPaging="True">
                        <MasterTableView TableLayout="Fixed" CommandItemDisplay="bottom" DataKeyNames="UserName,emp_code,Email,StatusId"> 
                            <CommandItemTemplate>
                            </CommandItemTemplate>
                            <Columns>
                                <radG:GridBoundColumn UniqueName="emp_code" Visible="false" DataField="emp_code">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false"   UniqueName="emp_name" CurrentFilterFunction="contains" FilterControlAltText="alphabetsonly"
                                    AutoPostBackOnFilter="true" HeaderText="Employee Name" DataField="emp_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" UniqueName="UserName" CurrentFilterFunction="contains" FilterControlAltText="cleanstring "
                                    AutoPostBackOnFilter="true" HeaderText="Username" DataField="UserName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" FilterControlAltText="numericonly" Visible="false" UniqueName="GroupID"
                                    HeaderText="GroupID" DataField="GroupID">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" FilterControlAltText="alphabetsonly" Visible="false" 
                                    UniqueName="StatusId" HeaderText="StatusId" DataField="StatusId">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" FilterControlAltText="alphabetsonly" HeaderStyle-Width="110px" UniqueName="Status"
                                    CurrentFilterFunction="contains" AutoPostBackOnFilter="true" HeaderText="Status"
                                    DataField="Status">
                                    <%--<ItemStyle Width="5%" />--%>
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" FilterControlAltText="alphabetsonly" UniqueName="GroupName" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" HeaderText="Group name" DataField="GroupName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="false" FilterControlAltText="cleanstring" UniqueName="computer_name" HeaderText="Computer name"
                                    Visible="False" DataField="computer_name">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn ShowFilterIcon="false" FilterControlAltText="cleanstring" CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                    ItemStyle-HorizontalAlign="Center" DataField="Email" UniqueName="Email" HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:TextBox Enabled="false" ID="txtEmail" CssClass="form-control input-sm clstxtemail" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Email")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <%--<ItemStyle Width="70%" />--%>
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="Password" HeaderText="Password" Visible="false"
                                    SortExpression="Password" MaxLength="15" UniqueName="Password">
                                </radG:GridBoundColumn>

                                <radG:GridTemplateColumn  AllowFiltering="False" UniqueName="UniqueEmail">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnSendMail" Text="Send Email" CommandName="SendSingleEmail" Enabled='<%# DataBinder.Eval(Container,"DataItem.StatusId").ToString() == "1" ? true: false %>'
                                                runat="server" CssClass="btn default clssendmail" />                                            
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                                
                                <radG:GridEditCommandColumn ButtonType= "ImageButton" HeaderStyle-Width="30px" UniqueName="EditColumn">
                               
                                </radG:GridEditCommandColumn>
                              
                            </Columns>
                            <CommandItemTemplate>
                                <div style="text-align: center">
                                    <asp:Button runat="server" ID="UpdateAll" Text="Send Emails For All" CommandName="UpdateAll" CssClass="btn red" />
                                </div>
                            </CommandItemTemplate>
                            <EditFormSettings UserControlName="../Users/usertemplate.ascx" EditFormType="WebUserControl" >
                            </EditFormSettings>
                        </MasterTableView>

                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                        </ClientSettings>


                    </radG:RadGrid>
                </telerik:RadPageView>
               
                 <telerik:RadPageView class="tbl" runat="server" ID="tblCSN"  Width="100%">
                    <div class="bg-row-red padding-tb-10 clearfix">
                                <div class="col-sm-12">
                                    <h4>CSN Setup</h4>
                                </div>
                            </div>
                        <radG:RadGrid ID="RadGrid4" runat="server" GridLines="None" Skin="Outlook" Width="715px"
                            OnInsertCommand="RadGrid4_InsertCommand" OnNeedDataSource="RadGrid4_NeedDataSource"
                            OnUpdateCommand="RadGrid4_UpdateCommand" OnDeleteCommand="RadGrid4_DeleteCommand">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,CSN" CommandItemDisplay="Bottom">
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <Columns>
                                    <radG:GridBoundColumn Visible="false" DataField="id" UniqueName="id">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="CSN" HeaderText="CSN" SortExpression="csn_number">
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton">
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn  ButtonType="ImageButton"
                                        ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                        UniqueName="DeleteColumn">
                                        <ItemStyle Width="50px" CssClass="clsCnfrmButton" />
                                    </radG:GridButtonColumn>
                                </Columns>
                                <CommandItemSettings AddNewRecordText="Add New CSN details" />
                                <EditFormSettings UserControlName="CPFFiles.ascx" EditFormType="WebUserControl">
                                </EditFormSettings>
                            </MasterTableView>
                        </radG:RadGrid>
                </telerik:RadPageView>

                <telerik:RadPageView class="tbl" runat="server" ID="tblCost"  Width="100%">
<div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(A) GL Code</h4>
                                </div>
                            </div>



<div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Salary GL</label>
                                                    <input type="text" id="txtSalaryGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Employee CPF GL</label>
                                                    <input type="text" id="txtEmpCPFGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Employer CPF GL</label>
                                                    <input type="text" id="txtEmpyCPFGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                                  <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Fund GL</label>
                                                    <input type="text" id="txtFundGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>                                      
                                          
                                            </div>
                                            
                                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>SDL GL</label>
                                                    <input type="text" id="txtSDLGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Comp Account GL</label>
                                                    <input type="text" id="txtacccompGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Unpaid Leaves GL</label>
                                                    <input type="text" id="txtunpaidGL" runat="server" class="form-control input-sm  number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                                 <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label id="lblOTGL" runat="server">OT GL</label>
                                                    <input type="text" id="txtOTGL" runat="server" class="form-control input-sm number-dot text-right" maxlength="12"/>
                                                </div>
                                            </div>
                                                                                             
                                          
                                            </div>
                      <%if (HttpContext.Current.Session["JV_GL"].ToString() == "ON")
                          {%>  
                                                 <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label> SDL Payable GL</label>
                                                    <input type="text" id="txtSDLpayable" runat="server" class="form-control input-sm"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Salary Payable GL</label>
                                                    <input type="text" id="txtSalarypayable" runat="server" class="form-control input-sm"/>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>CPF Payable GL</label>
                                                    <input type="text" id="txtCPFpayable" runat="server" class="form-control input-sm"/>
                                                </div>
                                            </div>
                                                
                                                                               
                                          
                                            </div>
                  `            <%} %>


</telerik:RadPageView>

                <telerik:RadPageView class="tbl" runat="server" ID="tblTimeSheet"  Width="100%">
<div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(A) Workingday Setup</h4>
                                </div>
                            </div>



<div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Required</label>
                                                    <select id="rdtimesheet" onchange="javascript:onrdtimesheetchange();" runat="server"
                                                                        class="textfields form-control input-sm" >
                                                                        <option value="1" selected="selected">Yes</option>
                                                                        <option value="2">No</option>
                                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Timesheet Remarks</label>
                                                    <select id="rdtsremarks" runat="server" class="textfields form-control input-sm" >
                                                                        <option value="0" selected="selected">No</option>
                                                                        <option value="1">Yes</option>
                                                                    </select>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>TimeSheet Columns</label>
                                                    <asp:CheckBoxList ID="chkBoxTs" runat="server"  class="textfields" RepeatDirection="Horizontal">
                                                                        <asp:ListItem Text="Remarks" Value="RE" Selected="false"></asp:ListItem>
                                                                        <asp:ListItem  Value="NOB" Text="Normal Hour - Break Time" Selected="False"></asp:ListItem>
                                                                        <asp:ListItem  Value="OTB" Text="Over Time - Break Time" Selected="False"></asp:ListItem>
                                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                                                                        
                                          
                                            </div>
                                            
                                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(B) Timesheet & Project Setup</h4>
                                </div>
                            </div>
                                            
                                            <div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Project Assignment: {Re login for settings to take effect.}</label>
                                                    <telerik:RadComboBox ID="cmbAssignType" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">

                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="One Time" Selected="true" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Daily" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="Monthly" Value="3" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                                                         
                                          
                                            </div>
                                            
                                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(C) Roster Settings</h4>
                                </div>
                            </div>
                                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Roster Type</label>
                                                    <telerik:RadComboBox ID="rostertype" runat="server" AutoPostBack="true" Width="150px" EmptyMessage="-select-" OnSelectedIndexChanged="rostertype_SelectedIndexChanged"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Normal Roster" Selected="true" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Weekly Roster" Value="2" />
                                                                        </Items>                                                                        
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Over Time Mode</label>
                                                    <telerik:RadComboBox ID="overtimemode" runat="server" Width="150px"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Manual" Selected="true" Value="True" />
                                                                            <telerik:RadComboBoxItem Text="Automatic" Value="False" />
                                                                        </Items>                                                                        
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Sunday</label>
                                                    <telerik:RadComboBox ID="cmbSunday" runat="server" Width="150px" EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">                                                                        
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Roster Not Assign</label>
                                                    <telerik:RadComboBox ID="cmbRosterNa" Enabled="false" runat="server" Width="150px" EmptyMessage="-select-" 
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">                                                                        
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                             </div>
                                             
                                             <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>FirstIn Last Out</label>
                                                    <asp:CheckBoxList  Visible="True" ID="chkFiFo" runat="server" CssClass="bodytxt"  RepeatColumns="1" RepeatDirection="Horizontal" RepeatLayout="Table"  >
                                                                            <asp:ListItem  Text="FirstInLastOut" Value="FIFO" Selected="False" ></asp:ListItem>                                    
                                                                      </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Rounding</label>
                                                    <telerik:RadComboBox ID="cmbRound" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="0" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="5" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="10" Value="10" />
                                                                            <telerik:RadComboBoxItem Text="15" Value="15" />
                                                                            <telerik:RadComboBoxItem Text="20" Value="20" />
                                                                            <telerik:RadComboBoxItem Text="25" Value="25" />
                                                                            <telerik:RadComboBoxItem Text="30" Value="30" />
                                                                            <telerik:RadComboBoxItem Text="35" Value="35" />
                                                                            <telerik:RadComboBoxItem Text="40" Value="40" />
                                                                            <telerik:RadComboBoxItem Text="45" Value="45" />
                                                                            <telerik:RadComboBoxItem Text="50" Value="50" />
                                                                            <telerik:RadComboBoxItem Text="55" Value="55" />                                                                            
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Public Holyday</label>
                                                    <telerik:RadComboBox ID="cmbPublicHoliday" Enabled="true" runat="server" Width="150px" EmptyMessage="-select-" 
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true"> 
                                                                         <Items>
                                                                            <telerik:RadComboBoxItem Text="" Value="0" />
                                                                            <telerik:RadComboBoxItem Text="One Time" Selected="true" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Daily" Value="2" />
                                                                        </Items>                                                                           
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            
                                             </div>
                                             
                                             <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(D)Settings</h4>
                                </div>
                            </div>
                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Alert For TimeSheet</label>
                                                    <telerik:RadComboBox ID="cbxEmailAlert" runat="server"  EmptyMessage="-select-" OnSelectedIndexChanged="CboxSendEmail_CheckedChanged"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true" AutoPostBack="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                        </Items>
                                                                       </telerik:RadComboBox>
                                                                       
                                                                       
                                                                       <asp:Label ID="lbldrpEmpProc" runat="server" Text="Alert Sent to:" Visible="false"></asp:Label>
                                                                  <telerik:RadComboBox ID="drpEmpProc1" runat="server"  EmptyMessage="-select-" OnSelectedIndexChanged="drpEmpProc_SelectedIndexChanged"
                                                                       MarkFirstMatch="false"   AutoPostBack="true" EnableLoadOnDemand="false">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Employee" Value="Employee"  />
                                                                            <telerik:RadComboBoxItem Text="Processer" Value="Processer" />
                                                                        </Items>
                                                                       </telerik:RadComboBox>  
                                                                       <asp:Label ID="lblEmail" runat="server" Text="Email:" Visible="false"></asp:Label>   
                                                                       <asp:TextBox ID="txtProcesserEmail" runat="server" Visible="false" Width="350px" ></asp:TextBox>
                                                                       
                                                </div>
                                            </div>
                                             </div>
                                             
                                             <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(D)Advance Time-Sheet</h4>
                                </div>
                            </div>
                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Advance TimeSheet</label>
                                                    <telerik:RadComboBox ID="radAdvanceTs" runat="server"  EmptyMessage="-select-" 
                                                                            MarkFirstMatch="true"  EnableLoadOnDemand="true" AutoPostBack="true"  OnSelectedIndexChanged="radAdvanceTs_SelectedIndexChanged"  >

                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Yes" Value="Yes" />
                                                                                <telerik:RadComboBoxItem Text="No" Value="No" Selected="true" />
                                                                            </Items>
                                                                            
                                                                           </telerik:RadComboBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label><asp:Label ID="Label1" runat="server" Text="Minutes" Visible="true"></asp:Label></label>
                                                    <radG:RadNumericTextBox Width="50px" Visible="true" NumberFormat-GroupSeparator="" ID="txtMinutes"
                                                                           runat="server" MinValue="0" MaxValue="59" NumberFormat-AllowRounding="true" Value="30"
                                                                           NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                                                                         </radG:RadNumericTextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Mobile TimeSheet</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox  runat="server" ID="mobilescancode"/>
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Show Roster Setting</label>
                                                    <label class='mt-checkbox mt-checkbox-single mt-checkbox-outline'>
                                                    <asp:CheckBox  runat="server" ID="showroster"/>
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                             </div>




</telerik:RadPageView>
                              
                
                <telerik:RadPageView class="tbl" runat="server" ID="tblPaySlipSetup"  Width="100%">
<div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(A) Payslip</h4>
                                </div>
                            </div>



<div class="row">
                                            <div class="col-sm-4">
                                                <div class="form-group">
                                                    <label>Format</label>
                                                    <telerik:RadComboBox ID="cmbpayslipformat" runat="server" Width="200px" AutoPostBack="true" EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                                <telerik:RadComboBoxItem Text="Format 1" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Format 2" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Format 3" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Format 4" Value="4" />
                                                                                 <telerik:RadComboBoxItem Text="Format 4(MOM)" Value="13"/>
                                                                                <telerik:RadComboBoxItem Text="Format 5" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Format 5(MOM)" Value="14"/>
                                                                                <telerik:RadComboBoxItem Text="Format 6" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Customize-1" Value="7"/>
                                                                                <telerik:RadComboBoxItem Text="Customize-2" Value="10"/>
                                                                                <telerik:RadComboBoxItem Text="Itemized-MOM" Value="11"/>
                                                                                <telerik:RadComboBoxItem Text="Itemized-MOM-HEADER" Value="12"/>
                                                                                <telerik:RadComboBoxItem Text="Customize-4" Value="8" Visible="false" />
                                                                                <telerik:RadComboBoxItem Text="Customize-5" Value="9" Visible="false"/>
                                                                               
                                                                                <telerik:RadComboBoxItem Text="Customize-5" Value="9" Visible="false"/>
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                    <input type="button" id="Button5" name="btnShowPayslip" value="Show Payslip"
                                                                        runat="server"  class="textfields btn btn-sm default" onclick="ShowPayslip();" />
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <div class="form-group">
                                                    <label>Email ePayslip</label>
                                                    <%--<telerik:RadComboBox ID="cmbEmailPaySlip" runat="server"  EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="Yes" Value="Y" />
                                                                            <telerik:RadComboBoxItem Text="No" Value="N" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>--%>

                                                    <div class="switch">
									<input id="cmbEmailPaySlip" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
									<label for="cmbEmailPaySlip"></label>
								</div>

                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>ePayslip Password Protected</label>
                                                    <telerik:RadComboBox ID="cmbEPayPwd" runat="server" Width="200px"
                                                                        EmptyMessage="-select-" MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="No" Value="No" />
                                                                            <telerik:RadComboBoxItem Text="Yes-NRIC/FIN" Value="FIN" />
                                                                            <telerik:RadComboBoxItem Text="Yes-User Password" Value="Usr" />
                                                                            <telerik:RadComboBoxItem Text="Yes-NRIC/FIN(or)User Password" Value="BOTH" />
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                                  <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Show Bank Account No</label>
                                                    <%--<asp:CheckBox ID="ShowBankAcNo" runat="server" Checked="true"  Text ="Show Bank Account No"/>--%>
                                                    <div class="switch">
									<input id="ShowBankAcNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
									<label for="ShowBankAcNo"></label>
								</div>
                                                </div>
                                            </div>                                      
                                          
                                                 

                                            </div>
                    <div class="row">
                        <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Display Merged Claims In PaySlip</label>
                                                    <%--<asp:CheckBox ID="ClaimTotalForPaySlip" runat="server" Checked="true"  Text ="Display Merged Claims In PaySlip"/>--%>
                                                <div class="switch">
									<input id="ClaimTotalForPaySlip" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
									<label for="ClaimTotalForPaySlip"></label>
								</div>
                                                </div>
                                            </div>

                    </div>
                                            
                                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(A) Payslip Configuration</h4>
                                </div>
                            </div>
                                
                    <table border="0" cellspacing="0" cellpadding="0" width="100%" style="table-layout: auto; width: 100%;">
                                                            <tr class="tdstand" id="trPaySlipSetup1" runat="server">
    <td>

        <table width="100%" id="tblPaySlipSetup1" visible="false" runat="server">
            <tr>
                <td>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="portlet light">
                            <table width="100%">
                                <tr>
                                    <td class="tdstand">LabelName</td>
                                    <td class="tdstand">Visible</td>
                                    <td class="tdstand">ActualName</td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayName" runat="server" Text="Name"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayNameYesNo" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayNameYesNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayNameYesNo"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayName" CssClass="form-control input-sm" Text="Name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayIDNO" runat="server" Text="IdNo"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayIDNOYesNo" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayIDNOYesNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayIDNOYesNo"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayIDNO" CssClass="form-control input-sm" Text="ID No" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>

                                    <td >
                                        <asp:Label ID="lblPaySALMONTH" runat="server" Text="Salary For Month"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPaySalMonthYesNo" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPaySalMonthYesNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPaySalMonthYesNo"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPaySALMONTH" CssClass="form-control input-sm" Text="Salary For Month" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayYEAR" runat="server" Text="Year"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayYear" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayYear" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayYear"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayYEAR" CssClass="form-control input-sm" Text="Pay Year" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayEARNINGS" runat="server" Text="Earnings"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayEarnings" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayEarnings" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayEarnings"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayEARNINGS" CssClass="form-control input-sm" Text="Earnings" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayDEDUCTIONS" runat="server" Text="Deductions"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayDeductions" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayDeductions" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayDeductions"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayDEDUCTIONS" CssClass="form-control input-sm" Text="Deductions" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayTOTALGROSS" runat="server" Text="TotalGross"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayTotalGross" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayTotalGross" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayTotalGross"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayTOTALGROSS" CssClass="form-control input-sm" Text="Total Gross" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayCPFGROSS" runat="server" Text="CPF Gross"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayCpfGross" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayCpfGross" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayCpfGross"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayCpfGross" CssClass="form-control input-sm" Text="CPF Gross" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayEMPLOYERCPF" runat="server" Text="EmployerCpf"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayEmployerCpf" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayEmployerCpf" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayEmployerCpf"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <%--<asp:TextBox ID="txtPayEMPLOYERCPF" Width="300px" Text="Employer CPF" runat="server"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPayEMPLOYERCPF" CssClass="form-control input-sm" Text="Eployer CPF" runat="server"></asp:TextBox>

                                    </td>
                                </tr>  
                            </table>
                                </div>
                        </div>
                        <div class="col-md-6">
                            <div class="portlet light">
                            <table width="100%">
                                <tr>
                                    <td class="tdstand">LabelName</td>
                                    <td class="tdstand">Visible</td>
                                    <td class="tdstand">ActualName</td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayETOTALDEDUCTION" runat="server" Text="TotalDeduction"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayTotalDeduction" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayTotalDeduction" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayTotalDeduction"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayTOTALDEDUCTION" CssClass="form-control input-sm" Text="Total Deduction" runat="server"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayNETPAYMENT" runat="server" Text="NetPayment"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayNETPAYMENT" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayNETPAYMENT" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayNETPAYMENT"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayNETPAYMENT" CssClass="form-control input-sm" Text="Net Payment" runat="server"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayYEARTODATE" runat="server" Text="Year To Date Net Pay"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayYEARTODATE" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayYEARTODATE" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayYEARTODATE"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayYEARTODATE" CssClass="form-control input-sm" Text="Year To Date Net Pay" runat="server"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayYEATODATEEMPLOYERCPF" runat="server" Text="Year To Date EmployerCPF"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayYEATODATEEMPLOYERCPF" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayYEATODATEEMPLOYERCPF" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayYEATODATEEMPLOYERCPF"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayYEATODATEEMPLOYERCPF" CssClass="form-control input-sm" Text="Year To Date EmployerCPF" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblpayDEPTNAME" runat="server" Text="Department Name"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayDEPTNAME" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayDEPTNAME" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayDEPTNAME"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayDepartmentName" CssClass="form-control input-sm" Text="DepartMent Name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayTrade" runat="server" Text="Trade"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayTrade" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayTrade" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayTrade"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayTrade" CssClass="form-control input-sm" Text="Trade Name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td >
                                        <asp:Label ID="lblPayDesignation" runat="server" Text="Designation"></asp:Label>
                                    </td>
                                    <td >
                                        <%--<telerik:RadComboBox ID="radPayDesignation" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                <telerik:RadComboBoxItem Text="No" Value="2" />
                                            </Items>
                                        </telerik:RadComboBox>--%>
                                        <div class="switch">
                                            <input id="radPayDesignation" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                            <label for="radPayDesignation"></label>
                                        </div>
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtPayDesignation" CssClass="form-control input-sm" Text="Designation" runat="server"></asp:TextBox>
                                    </td>
                                </tr>  
                            </table>
                                </div>
                        </div>
                    </div>
                </td>
            </tr>

            <tr>
        <td>
<div class="row">
            <div class="col-md-6">
                <div class="portlet light">
                <table width="100%">
                <tr>
                <td class="tdstand">
                    <asp:Label ID="lblLOGOMGT" runat="server" Text="Logo Management"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="radPayLOGOMGT" runat="server" Width="60%">
                        <Items>
                            <telerik:RadComboBoxItem Text="Option1-CompanyImage-AddressAndLogo" Value="1" />
                            <telerik:RadComboBoxItem Text="Option2-Company-Logo-Address" Selected="true" Value="2" />
                            <%--<telerik:RadComboBoxItem Text="Option3-Company-Address-Logo" Value="3" />--%>
                            <telerik:RadComboBoxItem Text="Option3-Company Name-Address" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td class="tdstand">
                    <asp:Label ID="lblLEAVEDETAILS" runat="server" Text="LeaveDetails"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="radPayLEAVEDETAILS" runat="server" Width="60%">
                        <Items>
                            <telerik:RadComboBoxItem Text="Simple(AL-CL)" Value="1" />
                            <telerik:RadComboBoxItem Text="Details(Annual Leave)" Selected="true" Value="2" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr>

            <tr>
                <td class="tdstand">
                    <asp:Label ID="lblEARNINGDETAILS" runat="server" Text="EarningSection"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="radPayEARNINGDETAILS" runat="server" Width="60%">
                        <Items>
                            <telerik:RadComboBoxItem Text="Label(NH-OT1-OT2)" Value="1" />
                            <telerik:RadComboBoxItem Text="Summary" Value="2" />
                            <telerik:RadComboBoxItem Text="Details" Selected="true" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
            </tr> 
                </table>
                    </div>
                </div>
                </div>
                </td>
                </tr>
            

        </table>


    </td>
</tr> 
                                                            <tr class="tdstand"  id="trPayslipSetup2" runat="server">
                                                                <td>
                                                                    
                                                                    <table width="100%" id="tblPayslipSetup2" visible="false" runat="server">
    <tr>
        <td>
            <div class="row">
            <div class="col-md-6">
                <div class="portlet light">
                <table width="100%">
                    <tr>
                        <td class="tdstand">LabelName</td>
                        <td class="tdstand">Visible</td>
                        <td class="tdstand">ActualName</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeName" runat="server" Text="Name"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizePayNameYesNo" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes3" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>

                            <div class="switch">
                                <input id="radCustomizePayNameYesNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizePayNameYesNo"></label>
                            </div>

                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizePayName" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeIdNo" runat="server" Text="IdNo"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeIdNo" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>

                            <div class="switch">
                                <input id="radCustomizeIdNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeIdNo"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeIdNo" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="IdNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeSalary" runat="server" Text="Salary For Month"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeSalaryForMonth" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>

                            <div class="switch">
                                <input id="radCustomizeSalaryForMonth" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeSalaryForMonth"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeSalary" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Salary For Month" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizePayYear" runat="server" Text="Year"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizePayYear" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>

                            <div class="switch">
                                <input id="radCustomizePayYear" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizePayYear"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizePayYear" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Pay Year" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeEarnings" runat="server" Text="Earnings"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeEarnings" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeEarnings" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeEarnings"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeEarnings" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Earnings" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeDeductions" runat="server" Text="Deductions"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeDeductions" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeDeductions" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeDeductions"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeDeductions" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Deductions" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeTotalGross" runat="server" Text="TotalGross"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTotalGross" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTotalGross" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTotalGross"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTotalGross" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Total Gross" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeCpfGross" runat="server" Text="CpfGross"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeCpfGross" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeCpfGross" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeCpfGross"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeCpfGross" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="CPF Gross" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeEmployerCpf" runat="server" Text="EmployerCpf"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeEmployerCpf" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeEmployerCpf" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeEmployerCpf"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeEmployerCpf" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Employer Cpf" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeToalDeduction" runat="server" Text="TotalDeduction"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTotalDeduction" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTotalDeduction" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTotalDeduction"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTotalDeduction" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Total Deduction" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeNetPayment" runat="server" Text="NetPayment"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeNetPayment" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeNetPayment" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeNetPayment"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeNetPayment" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Net Payment" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeYearToDate" runat="server" Text="Year To Date Net Pay"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeYearToDate" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeYearToDate" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeYearToDate"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeYearToDate" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Year To Date Net Pay" runat="server"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeYearToDateEmployerCPF" runat="server" Text="Year To Date EmployerCPF"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeYearToDateEmployerCPF" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeYearToDateEmployerCPF" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeYearToDateEmployerCPF"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeYearToDateEmployerCPF" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Year To Date EmployerCPF" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeDepartmentName" runat="server" Text="Department Name"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeDepartmentName" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeDepartmentName" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeDepartmentName"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeDepartmentName" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="DepartMent Name" runat="server"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeTrade" runat="server" Text="Trade"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTrade" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTrade" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTrade"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTrade" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Trade Name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                </div>
            </div>
            <div class="col-md-6">
                <div class="portlet light">
                <table width="100%">
                    <tr>
                        <td class="tdstand">LabelName</td>
                        <td class="tdstand">Visible</td>
                        <td class="tdstand">ActualName</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeDesignation" runat="server" Text="Designation"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeDesignation" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeDesignation" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeDesignation"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeDesignation" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Designation" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeDOB" runat="server" Text="DOB"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeDOB" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeDOB" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeDOB"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeDOB" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="DOB" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeTimecardNo" runat="server" Text="Timecard No"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTimecardNo" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTimecardNo" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTimecardNo"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTimecardNo" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Timecard No" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeJoiningDate" runat="server" Text="Joining Date"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeJoiningDate" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeJoiningDate" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeJoiningDate"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeJoiningDate" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Joining Date" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeTerminationDate" runat="server" Text="Termination Date"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTerminationDate" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTerminationDate" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTerminationDate"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTerminationDate" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Termination Date" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeBusinessUnit" runat="server" Text="Business Unit"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeBusinessUnit" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeBusinessUnit" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeBusinessUnit"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeBusinessUnit" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Business Unit" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizePayslipPeriod" runat="server" Text="Payslip Period"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizePayslipPeriod" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizePayslipPeriod" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizePayslipPeriod"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizePayslipPeriod" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Payslip Period" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeOvertimePeriod" runat="server" Text="Overtime Period"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeOvertimePeriod" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeOvertimePeriod" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeOvertimePeriod"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeOvertimePeriod" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Overtime Period" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeTotalAdditions" runat="server" Text="Total Additions"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeTotalAdditions" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeTotalAdditions" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeTotalAdditions"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeTotalAdditions" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Total Additions" runat="server"></asp:TextBox>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeDateOfPayment" runat="server" Text="Date Of Payment"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeDateOfPayment" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeDateOfPayment" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeDateOfPayment"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeDateOfPayment" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Date Of Payment" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeModeOfPayment" runat="server" Text="Mode Of Payment"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeModeOfPayment" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeModeOfPayment" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeModeOfPayment"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeModeOfPayment" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Mode Of Payment" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblYearToDateEmployeeCPF" runat="server" Text="Year To Date EmployeeCPF"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radYearToDateEmployeeCPF" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radYearToDateEmployeeCPF" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radYearToDateEmployeeCPF"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtYearToDateEmployeeCPF" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Year To Date Employee CPF" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomizeRemarks" runat="server" Text="Remarks"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radCustomizeRemarks" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radCustomizeRemarks" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radCustomizeRemarks"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustomizeRemarks" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Remarks" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Cheque Number"></asp:Label>
                        </td>
                        <td>
                            <%--<telerik:RadComboBox ID="radChequeNumber" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                </Items>
                            </telerik:RadComboBox>--%>
                            <div class="switch">
                                <input id="radChequeNumber" runat="server" class="switch-toggle switch-toggle-round switch-toggle-round" type="checkbox">
                                <label for="radChequeNumber"></label>
                            </div>
                        </td>
                        <td>
                            <asp:TextBox ID="txtChequeNumber" CssClass="form-control input-sm alphabetsonly custom-maxlength" MaxLength="50" Text="Cheque No" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                </table>
                    </div>
            </div>
                </div>
        </td>
    </tr>


   <tr>
        <td>
<div class="row">
            <div class="col-md-6">
                <div class="portlet light">
                <table width="100%">
                 <tr>
        <td class="tdstand">
            <asp:Label ID="lblCustomizeLogoManagement" runat="server" Text="Logo Management"></asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="radCustomizeLogoManagement" runat="server" Width="60%">
                <Items>
                    <telerik:RadComboBoxItem Text="Option1-CompanyImage-AddressAndLogo" Value="1" />
                    <telerik:RadComboBoxItem Text="Option2-Company-Logo-Address" Selected="true" Value="2" />
                    <%--  <telerik:RadComboBoxItem Text="Option3-Company-Address-Logo" Value="3" />--%>
                    <telerik:RadComboBoxItem Text="Option3-Company Name" Value="3" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td class="tdstand">
            <asp:Label ID="lblCustomizeLEAVEDETAILS" runat="server" Text="LeaveDetails"></asp:Label>
        </td>
        <td colspan="3">
            <telerik:RadComboBox ID="radCustomizePayLEAVEDETAILS" runat="server" Width="60%">
                <Items>
                    <telerik:RadComboBoxItem Text="Simple(AL-CL)" Value="1" />
                    <telerik:RadComboBoxItem Text="Details(Annual Leave)" Selected="true" Value="2" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr>

    <tr>
        <td class="tdstand">
            <asp:Label ID="lblCUSTOMIZEEARNINGDETAILS" runat="server" Text="EarningSection"></asp:Label>
        </td>
        <td colspan="3">
            <telerik:RadComboBox ID="radCUSTOMIZEPayEARNINGDETAILS" runat="server" Width="60%">
                <Items>
                    <telerik:RadComboBoxItem Text="Label(NH-OT1-OT2)" Value="1" />
                    <telerik:RadComboBoxItem Text="Summary" Value="2" />
                    <telerik:RadComboBoxItem Text="Details" Selected="true" Value="3" />
                </Items>
            </telerik:RadComboBox>
        </td>
    </tr> 
                </table>
                    </div>
                </div>
                </div>
                </td>
                </tr>




    </table>

                                                                                                                                    
                                                                </td>
                                                            </tr> 
                                                            <tr class="tdstand" >
                                                                <td>
                                                                    <table width="100%" id="tblMOMItemized" visible="false" runat="server">
                                                                        <tr>
                                                                            <td class="tdstand" colspan="2">LabelName</td>
                                                                            <td class="tdstand" colspan="1">Visible</td>
                                                                            <td class="tdstand" colspan="1">ActualName</td>
                                                                       </tr>  
                                                                       
                                                                      <tr>
                                                                                <td  colspan="2" >
                                                                                      <asp:Label ID="lblItemizeLogo" runat="server" Text="Logo Management"></asp:Label>                                                                     
                                                                                </td>
                                                                                <td  colspan="1">
                                                                                        <telerik:RadComboBox ID="radItemizeLogo" runat="server" AutoPostBack="true">
                                                                                                 <Items>
                                                                                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                                                                    <%--<telerik:RadComboBoxItem Text="No" Value="2" />              --%>                                                               
                                                                                                 </Items>                                                                               
                                                                                         </telerik:RadComboBox>
                                                                                </td>
                                                                                <td  colspan="1">
                                                                                     <telerik:RadComboBox ID="radItemizeLogoManagement" runat="server" Width="400px"  >
                                                                                                 <Items>
                                                                                                     <telerik:RadComboBoxItem Text="Option1-CompanyImage-AddressAndLogo"  Value="1" />
                                                                                                     <telerik:RadComboBoxItem Text="Option2-Company-Logo-Address" Selected="true" Value="2" />
                                                                                                    <%-- <telerik:RadComboBoxItem Text="Option3-Company-Address-Logo"  Value="3" />--%>
                                                                                                     <telerik:RadComboBoxItem Text="Option3-Company Name"  Value="3" />
                                                                                                 </Items>                                                                               
                                                                                       </telerik:RadComboBox>
                                                                                </td>                                                                        
                                                                        </tr>    
                                                                        
                                                                      <%--<tr>
                                                                                <td  colspan="2" >
                                                                                    <asp:Label ID="lblItemizeLeaveDetails" runat="server" Text="Leave Details"></asp:Label>                                                                        
                                                                                </td>
                                                                                <td  colspan="1">
                                                                                        <telerik:RadComboBox ID="radItemizeLeave" runat="server" AutoPostBack="true">
                                                                                                 <Items>
                                                                                                    <telerik:RadComboBoxItem Text="Yes" Selected="true" Value="1" />
                                                                                                     <telerik:RadComboBoxItem Text="No" Value="2" />                                                                             
                                                                                                 </Items>                                                                               
                                                                                         </telerik:RadComboBox>
                                                                                </td>
                                                                                <td  colspan="1">
                                                                                   <telerik:RadComboBox ID="radItemizeLEAVEDETAILS" runat="server" Width="400px" >
                                                                                                 <Items>
                                                                                                     <telerik:RadComboBoxItem Text="Simple(AL-CL)"  Value="1" />
                                                                                                     <telerik:RadComboBoxItem Text="Details(Annual Leave)" Selected="true" Value="2" />                                                                                                     
                                                                                                 </Items>                                                                               
                                                                                       </telerik:RadComboBox>
                                                                                </td>                                                                        
                                                                        </tr>    
                                                                                 --%>                                                                                                            
                                                                  </table>                                                                
                                                                </td>
                                                            </tr>
                                                            
                                                        </table>            
                                           
</telerik:RadPageView>
                
                
                <telerik:RadPageView class="tbl" runat="server" ID="tblMultiCurrency"  Width="100%">

<div class="row">
                                            <div class="col-sm-5">
                                                <div class="form-group">
                                                    <label>Addition/Deduction/Claims-Conversion</label>
                                                    <asp:DropDownList ID="drpConv"  CssClass="bodytxt form-control input-sm"  runat="server" >
                                                                            <asp:ListItem Text="Add-Ded/Claim  :PayrollDate" Value="1"></asp:ListItem>
                                                                            <asp:ListItem Text="Add-Ded/Claim  :AddingDate"  Value="2"></asp:ListItem> 
                                                                            <asp:ListItem Text="Add-Ded:PayrollDate /Claim : AddingDate"  Value="3"></asp:ListItem> 
                                                                            <asp:ListItem Text="Add-Ded:AddingDate  /Claim : PayrollDate" Value="4"></asp:ListItem>
                                                                      </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Payroll Currency</label>
                                                    <asp:DropDownList ID="drpCurrency" CssClass="bodytxt form-control input-sm"    runat="server">
                                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            </div>
</telerik:RadPageView>
                              
                <telerik:RadPageView class="tbl" runat="server" ID="tblLeaveSetting"  Width="100%">
<div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(A) Working Days</h4>
                                </div>
                            </div>



<div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>No of Working Days <span style="color:Red;">*</span></label>
                                                    <telerik:RadComboBox ID="cmbworkingdays" runat="server" Width="200px" EmptyMessage="-select-" 
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true" OnClientSelectedIndexChanged="cmbworkingdays_Changed" >

                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="5" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="5.5" Value="5.5" />
                                                                            <telerik:RadComboBoxItem Text="6" Value="6" />                                                                            
                                                                           </Items>
                                                                        
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>
                                          
                                            </div>
                                            
                                            <div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(B) Settings</h4>
                                </div>
                            </div>
                                            <div class="row">
                                            <div class="col-sm-6">
                                                <div class="form-group">
                                                    <label>1. Do you follow standard leave setting? (ie. Saturday, Sunday off days)</label>
                                                    <label class='mt-radio mt-radio-outline'>
                                                  <input type="radio" runat="server" id="rdoYes" value="Yes" name="rodStandard" onclick="ShowHideStandard(this);" />Yes
                                                        <span></span>
                                                        </label>
                                                    <label class='mt-radio mt-radio-outline'>
                                           <input type="radio" runat="server" id="rdoNo" value="No" name="rodStandard" onclick="ShowHideStandard(this);" />No
                                                        <span></span>
                                                        </label>
                                                </div>
                                            </div>
                                            <div class="col-sm-3" id="test">
                                                <div class="form-group" id="test1">
                                                    <label>2. Are the off days fixed for all employees?</label>
                                                    <label class='mt-radio mt-radio-outline'>
                                <input type="radio" runat="server" id="rdoHide" value="Yes" name="rdoFixedDays" onclick="ShowHideCustomized(this);" />Yes
<span></span>
                                                        </label>
                                                    <label class='mt-radio mt-radio-outline'>
                                 <input type="radio" runat="server" id="rdoShow" value="No" name="rdoFixedDays" onclick="ShowHideCustomized(this);" />No
                                                <span></span>
                                                        </label>
                                                        </div>
                                            </div>
                                            </div>
                                                                                        
                                            <div class="row">
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>3. Which are the off days?</label>
                                                    
                                                </div>
                                            </div>
                                                  <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Off Day 1</label>
                                                    <telerik:RadComboBox ID="cmdOffDay1" runat="server" Width="200px" EmptyMessage="-select-"
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="-select-" Value="8" />
                                                                            <telerik:RadComboBoxItem Text="Sunday" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Monday" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="Tuesday" Value="3" />
                                                                            <telerik:RadComboBoxItem Text="Wednesday" Value="4" />
                                                                            <telerik:RadComboBoxItem Text="Thursday" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="Friday" Value="6" /> 
                                                                            <telerik:RadComboBoxItem Text="Saturday" Value="7" />                                                                           
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                                </div>
                                            </div>                                      
                                          <div class="col-sm-3">
                                                <div class="form-group">
                                                    <label>Off Day 2</label>
                                                    <telerik:RadComboBox ID="cmdOffDay2" runat="server" Width="200px" EmptyMessage="-select-" 
                                                                        MarkFirstMatch="true"  EnableLoadOnDemand="true" Enabled="false">
                                                                        <Items>
                                                                            <telerik:RadComboBoxItem Text="-select-" Value="8" />
                                                                            <telerik:RadComboBoxItem Text="Sunday" Value="1" />
                                                                            <telerik:RadComboBoxItem Text="Monday" Value="2" />
                                                                            <telerik:RadComboBoxItem Text="Tuesday" Value="3" />
                                                                            <telerik:RadComboBoxItem Text="Wednesday" Value="4" />
                                                                            <telerik:RadComboBoxItem Text="Thursday" Value="5" />
                                                                            <telerik:RadComboBoxItem Text="Friday" Value="6" /> 
                                                                            <telerik:RadComboBoxItem Text="Saturday" Value="7" />                                                                             
                                                                        </Items>
                                                                    </telerik:RadComboBox>
                                              <%--         <asp:CheckBox ID="chkHalf1" runat="server" Checked="false"  Text ="Half Day" />   --%>
                                                </div>
                                            </div>
                                            </div>

<div class="row">
                                            <div class="col-sm-12">
                                           If you choose 5.5 working days, <span style="color:Blue;"> Off Day 1</span>  will become half day.
                                            </div>
                                             </div>
                                             
<div class="row">
                                <div class="col-sm-12">
                                    <h4 class="block">(C) Payroll Setting</h4>
                                </div>
                            </div>

<div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                              Do Payroll follow Leave Calender? (eg. 'Yes'-Payroll will calculate based on Leave Calender)
                        <label class='mt-radio mt-radio-outline'>
                                                    <input type="radio" runat="server" id="rdoLeaveCal" value="Yes" name="rdoCalPayroll" onclick="rodStandard_SelectChanged(this);" />Yes
                         <span></span>
                            </label>
                                                    <label class='mt-radio mt-radio-outline'>
                                                    <input type="radio" runat="server" id="rdoPayrollCal" value="No" name="rdoCalPayroll" onclick="rodStandard_SelectChanged(this);" />No
                                               <span></span>
                            </label>
                                                     </div>
                                            </div>
                                             </div>
                                             
                                      <div class="row">
                                <div class="col-sm-12">
      <asp:Button ID="btnUpdateLeave" runat="server" OnClick="btnUpdateLeave_Click" Text="Update for All Employees"  />
                                </div>
                            </div>       
      
</telerik:RadPageView>

                
                
            </telerik:RadMultiPage>
        </div>      
        <asp:RadioButtonList Visible="false" ID="RdApproval" runat="server" RepeatDirection="Horizontal"
            Height="11px" Width="121px">
            <asp:ListItem Enabled="false" Value="1">Yes</asp:ListItem>
            <asp:ListItem Value="0" Enabled="false" Selected="True">No</asp:ListItem>
        </asp:RadioButtonList>
        <asp:XmlDataSource ID="xmldtCompType" runat="server" DataFile="~/XML/xmldata.xml"
            XPath="SMEPayroll/Company/CompanyType"></asp:XmlDataSource>
        <asp:RequiredFieldValidator ID="rfvcode" runat="server" ControlToValidate="txtCompCode"
            Display="None" ErrorMessage="Address Setup-Prefix Code Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvpayapprove" runat="server" ErrorMessage="Payroll Approval Setup Required!"
            ControlToValidate="RdApproval" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="rfvannualcpfceil" runat="server" ErrorMessage='Preferences Setup-"Annual Cpf Ceiling" is Required!'
            BorderStyle="None" ControlToValidate="txtannual_cpf_ceil" Display="None"></asp:RequiredFieldValidator>
        &nbsp;
        <asp:RequiredFieldValidator ID="rfvnoworkdays" runat="server" InitialValue="-select-"
            ErrorMessage="Preferences Setup-Working Days Setup Required!" ControlToValidate="cmbworkingdays"
            Display="None"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="true" ShowSummary="False" />
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
            var pTags = $("#chkAdClaims");
            if (pTags.parent().is("span")) {
                pTags.unwrap();
            }
        });

        $("input[type='button'],.RadComboBox").removeAttr("style");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid2_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        $(".clssendmail").click(function () {

            if ($(this).closest('tr').find('.clstxtemail').val() == "")
            {
                WarningNotification("Email Id Is Blank! Please Update Email Id first");
                return false;
            }

        });

        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
           
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this record?", _id, "Confirm Delete", "Delete");
        });
    </script>

</body>
</html>
