<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCliamForm.aspx.cs" Inherits="SMEPayroll.ClaimCapping.ApplyCliamForm" %>


<%@ Import Namespace="System.Web.Configuration" %>
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
<head runat="server">

    <title>Apply Claim</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />




    <link href="../assets/css/toaster.css" rel="stylesheet" />
    <link href="../assets/css/loading.css" rel="stylesheet" />
    <link href="../bootstrap/angular-datepicker.css" rel="stylesheet" />




    <uc_css:bundle_css ID="bundle_css" runat="server" />


</head>

<body style="margin-left: auto" ng-app="cliamApp" ng-controller="mainController as vm" ng-init="vm.companyValue('<%= Session["Compid"].ToString() %>','<%= Session["EmpCode"].ToString() %>','<%= CommonData.CompanyExt.MultiCurrency %>')" class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed">

    <form id="form2" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>

    </form>


    <script type="text/ng-template" id="myModalContent.html">
        <%--<div class="modal-header">
            <h3 class="modal-title" id="modal-title">I'm a modal!</h3>
        </div>
        <div class="modal-body" id="modal-body">
           test
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" type="button" ng-click="$ctrl.ok()">OK</button>
            <button class="btn btn-warning" type="button" ng-click="$ctrl.cancel()">Cancel</button>
        </div>--%>
    <div class="modal-bg">
    <div class="dialog">
    <div class="modal-header">
    <h3>{{$ctrl.item.headerText}}</h3>
</div>
<div class="modal-body">
    <p>{{$ctrl.item.bodyText}}</p>
</div>
<div class="modal-footer">
    <button type="button" class="btn" 
            data-ng-click="$ctrl.ok()">{{$ctrl.item.actionButtonText}}</button>
    <button class="btn btn-primary" 
            data-ng-click="$ctrl.cancel()">{{$ctrl.item.closeButtonText}}</button>
</div>
    </div>
    </div>
    </script>


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
                        <li>Apply Claims</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Apply Claim</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Apply Claims</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">


                        <form id="form1" name="claimApplingForm" novalidate>




                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Employee</label>
                                        <select name="drpemp" id="drpemp" class="textfields form-control input-sm" ng-model="vm.selectedEmp" ng-change="vm.loadClaimType()"
                                            ng-options='item as item.EmpName for item in vm.EmpList'>
                                            <option value="">Select EmpLoyee</option>

                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <select name="DropDownList1" id="DropDownList1" class="textfields form-control input-sm" ng-model="vm.month">
                                            <option value="01">January</option>
                                            <option value="02">February</option>
                                            <option value="03">March</option>
                                            <option value="04">April</option>
                                            <option value="05">May</option>
                                            <option value="06">June</option>
                                            <option value="07">July</option>
                                            <option value="08">August</option>
                                            <option value="09">September</option>
                                            <option value="10">October</option>
                                            <option value="11">November</option>
                                            <option value="12">December</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Year</label>
                                        <select name="cmbYear" id="cmbYear" class="textfields form-control input-sm" ng-model="vm.year" ng-change="vm.IsPayrollProcced()">
                                            <option value="2007">2007</option>
                                            <option value="2008">2008</option>
                                            <option value="2009">2009</option>
                                            <option value="2010">2010</option>
                                            <option value="2011">2011</option>
                                            <option value="2012">2012</option>
                                            <option value="2013">2013</option>
                                            <option value="2014">2014</option>
                                            <option value="2015">2015</option>
                                            <option value="2016">2016</option>
                                            <option value="2017">2017</option>
                                            <option value="2018">2018</option>
                                            <option value="2019">2019</option>
                                            <option value="2020">2020</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ng-show="false" ng-click="vm.getCliams()" ID="imgbtnfetch" name="imgbtnfetch" CssClass="btn red btn-circle btn-sm">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input type="button" ng-click="vm.addNew()" class="rfdDecorated bodytxt btn btn-sm red margin-top-0" value="Add New Claim" ng-disabled="claimApplingForm.$invalid" name="imgbtnfetch" />
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <div class="RadAjaxPanel" id="errorMsgPanel">
                                            <span id="errorMsg" style="color: Red;"></span>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <span id="lblerror" class="bodytxt" style="color: Red;"></span>

                            <toaster-container toaster-options="{'time-out': 2000}"></toaster-container>

                            <div class="RadAjaxPanel" id="RadGrid1Panel" style="display: block;">
                                <div id="RadGrid1" class="RadGrid RadGrid_Outlook" style="width: 95%; visibility: visible;" tabindex="0">

                                    <!-- 2013.1.417.45 -->
                                    <table class="rgMasterTable" border="0" id="RadGrid1_ctl00" style="width: 100%; table-layout: auto; empty-cells: show;">

                                        <thead>
                                            <tr>
                                                <th class="rgHeader rgCheck" style="text-align: center; width: 35px">
                                                    <input type="checkbox"
                                                        ng-model="vm.selectAll"
                                                        ng-click="vm.checkAll()" />

                                                </th>
                                                <th scope="col" class="rgHeader" style="text-align: center;"></th>
                                                <th scope="col" class="rgHeader" style="text-align: center;"><font color="red">*</font>Transaction Date</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.IncurredDateShow">
                                                    <font class="required-red" ng-show="vm.IncurredDateRequired">*</font> Incurred Date
                                                </th>
                                                <th scope="col" class="rgHeader" style="text-align: center;">Claim Type</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.projectShow">
                                                    <font class="required-red" ng-show="vm.projectRequired">*</font> SubProject
                                                </th>



                                                <%--                                                <th scope="col" class="rgHeader">>CurrencyID</a></th>--%>
                                                <th scope="col" ng-show="vm.multiCurrency" class="rgHeader" style="text-align: center;">Currency</th>
                                                <th scope="col" class="rgHeader" ng-show="vm.TaxShow" style="text-align: center;">
                                                    <font class="required-red" ng-show="vm.taxRequired">*</font> Tax
                                                </th>
                                                <%--                                                <th scope="col" class="rgHeader" style="text-align: center;">GstCode</th>--%>
                                                <th scope="col" class="rgHeader" style="text-align: center;"><font color="red">*</font>Claim Amt</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.TaxShow">GST</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.TaxShow">Amt Before GST</th>
                                                <%--<th scope="col" class="rgHeader">ExRate</th>--%>
                                                <th scope="col" class="rgHeader" style="text-align: center;">Actual Amt</th>
                                                <%-- <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.ReciptShow">Receipt No</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.AttacementShow">Attachment</th>
                                                <th scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.DisciptionShow">Description</th>--%>
                                                <%--  <th scope="col" class="rgHeader" style="text-align: center;">Description</th>--%>
                                                <th  scope="col" class="rgHeader" style="text-align: center;" ng-show="vm.claimCappingShow">Capping Amt</th>
                                                <%--  <th scope="col" class="rgHeader" style="text-align: center;">Action</th>--%>
                                            </tr>
                                        </thead>
                                        <%--          <tfoot>
                                            <tr class="rgFooter" style="color: Firebrick; font-weight: bold; text-align: center;">
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td ng-show="vm.projectShow">&nbsp;</td>
                                                <td ng-show="vm.multiCurrency">&nbsp;</td>
                                                <td ng-show="vm.taxShow">&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td >&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </tfoot>--%>

                                        <tbody>

                                            <tr class="rgRow" id="RadGrid1_ctl00__0" style="background-color: White; height: 20px;" ng-repeat-start="item in vm.ClaimList">
                                                <td align="center">                                                    
                                                        <input class="width-20px" type="checkbox" ng-model="item.select" />                                                    
                                                </td>
                                                <td align="center">
                                                    <button class="btn btn-sm red no-margin" ng-click="expanded = !expanded" expand>
                                                        <%--<span ng-bind="expanded ? '-' : '+'"></span>--%>
                                                        <i class="fa" ng-class="{'fa-minus' : expanded,  'fa-plus' : !expanded}"></i>
                                                    </button>
                                                </td>


                                                <%--                                                transectionPeriod--%>
                                                <td align="center">
                                                    <select name="trxdate" ng-model="item.TrxPeriod" class="bodytxt form-control input-sm width-120px"
                                                        ng-options="item as item | date:'dd/MM/yyyy' for item in vm.dateList" required>

                                                        <option value="">Select Date</option>

                                                    </select>


                                                </td>
                                                <td align="center" ng-show="vm.IncurredDateShow">
                                                    <div class="datepicker"
                                                        date-set-hidden="true"
                                                        <%--datepicker-append-to="body"--%>
                                                        date-format="dd/MMM/yyyy"
                                                        date-min-limit="2014/08/07"
                                                        date-max-limit="2099/09/07"
                                                        date-typer="true"
                                                        button-prev='<i class="fa fa-arrow-circle-left"></i>'
                                                        button-next='<i class="fa fa-arrow-circle-right"></i>'>
                                                        <input ng-model="item.IncurredDate" ng-required="vm.IncurredDateRequired" type="text" class="angular-datepicker-input bodytxt form-control input-sm width-120px" formatted-date format='dd/MM/yyyy' />
                                                    </div>

                                                </td>

                                                <%--                                                claimType--%>
                                                <td align="center">
                                                    <select name="cliamTypeSelect"
                                                        class="bodytxt form-control input-sm width-160px"
                                                        <%-- ng-options='item as item.ClaimName for item in vm.CliamTypes' ng-model="item.TrxType">

                                                      --%>
                                                        ng-options='item.ClaimId as item.ClaimName for item in vm.CliamTypes' ng-model="item.TrxType" ng-disabled="!item.TrxPeriod"
                                                        ng-change="vm.GetCliamTypeAmount(item.TrxType,vm.selectedEmp.EmpCode,item)" required>

                                                        <option value="">Select Claim Type</option>

                                                    </select>
                                                </td>

                                                <%--          subprojectId--%>

                                                <td align="center" ng-show="vm.projectShow">
                                                    <select name="cliamTypeSelect"
                                                        class="bodytxt form-control input-sm width-160px"
                                                        <%-- ng-options='item as item.ClaimName for item in vm.CliamTypes' ng-model="item.TrxType">

                                                      --%>
                                                        ng-options='item.Id as item.SubProjectName for item in vm.subProject' ng-required="vm.projectRequired" ng-model="item.ProjectId">

                                                        <option value="">Select SubProject</option>

                                                    </select>
                                                </td>

                                                <%--                                                Currency--%>
                                                <td align="center" ng-show="vm.multiCurrency">
                                                    <select name="currencyName" ng-model="item.CurrencyId" id="RadGrid1_ctl00_ctl04_drpCurrencyID" class="bodytxt form-control input-sm input-xsmall" ng-required="vm.multiCurrency">
                                                        <option value="1">SGD</option>
                                                        <option value="2">USD</option>
                                                        <option value="3">Yen</option>
                                                        <option value="4">INR</option>
                                                        <option value="5">HKD</option>
                                                        <option value="6">EUR</option>
                                                        <option value="7">MYR</option>
                                                        <option value="8">GBP</option>
                                                        <option value="9">AUD</option>
                                                        <option value="10">KRW</option>
                                                        <option value="11">RMB</option>
                                                        <option value="12">IDR</option>
                                                        <option value="13">AED</option>
                                                        <option value="14">PHP</option>
                                                        <option value="15">THB</option>
                                                        <option value="16">NTD</option>
                                                    </select>
                                                    <%-- <p class="help-block" ng-show="claimFieldForm.currencyName.$invalid">Error</p>--%>
                                                </td>
                                                <%--                                                taxchckbox--%>
                                                <td align="center" ng-show="vm.TaxShow">
                                                    <span>
                                                        <input id="RadGrid1_ctl00_ctl04_chkGst" class="width-50px" type="checkbox" ng-checked="item.GstFlag == 1" ng-true-value="1" ng-false-value="0" ng-model="item.GstFlag" name="RadGrid1$ctl00$ctl04$chkGst" ng-change="vm.Calculate(item)"></span>
                                                </td>
                                                <%--                                             totalAmount--%>
                                                <td align="center">
                                                    <div>
                                                        <input name="RadGrid1$ctl00$ctl04$txtAmtGst" min="1" type="text" ng-model="item.ToatlWithGst" value="0.00" id="RadGrid1_ctl00_ctl04_txtAmtGst" class="form-control input-sm width-85px text-right" ng-change="vm.Calculate(item)" required>
                                                    </div>
                                                </td>

                                                <%--gstAmount--%>
                                                <td align="center" ng-show="vm.TaxShow">

                                                    <span class="width-80px">({{vm.GstRate | number : 2}})   {{item.GstAmnt}}
                                                    </span>

                                                </td>
                                                <%--                                                beforeGstAmount--%>
                                                <td align="center" ng-show="vm.TaxShow">

                                                    <div>
                                                        <input type="text" ng-model="item.ToatlBefGst" disabled class="form-control input-sm width-100px text-right" />

                                                    </div>
                                                </td>
                                                <%--                                                ExRate--%>
                                                <%--    <td align="center" >{{item.ExRate}}</td>--%>

                                                <%--                                                PayAmount--%>
                                                <td align="center">

                                                    <div>
                                                        <input type="text" ng-model="item.PayAmount" disabled class="form-control input-sm width-85px text-right" />

                                                    </div>
                                                </td>

                                                <%-- <td align="center"  ng-show="vm.ReciptShow">
                                                    <div>
                                                        <input name="RadGrid1$ctl00$ctl04$txtReceipt" type="text" ng-model="item.ReceiptNo" maxlength="20" style="width: 100px;" required />
                                                    </div>
                                                </td>--%>




                                                <%--      Attachement--%>

                                                <%--  <td align="center"  ng-show="vm.AttacementShow">
                                                    <div ng-show="!item.Recpath">
                                                        <input type="file" ngf-select ng-model="item.files">

                                                      
                                                    </div>
                                                    <div ng-show="item.Recpath">
                                                        {{item.Recpath}}
                                                      
                                                                           

                                                       
                                                    </div>
                                                </td>--%>


                                                <%--      Description--%>
                                                <%--  <td align="center"  ng-show="vm.DisciptionShow">
                                                    <div>
                                                        <textarea name="RadGrid1$ctl00$ctl04$txtDescription" type="text" ng-model="item.Description" maxlength="50" style="width: 150px;" required></textarea>
                                                    </div>
                                                </td>--%>




                                                <td  align="center" ng-show="vm.claimCappingShow">
                                                    <%--  <div ng-show="item.TrxType">--%>
                                                    <div class="width-85px">
                                                        Trx: {{item.TrxCapAmount}} Month:{{item.MonthlyCapAmount}} Year:{{item.YearlyCapAmount}}
                                                   
                                                   
                                                   
                                                    </div>
                                                </td>

                                                <%--   evaluvate--%>
                                                <%--  <td colspan="2" align="center" >
                                                    <button ng-click="vm.applywithConfirm(item,claimApplingForm.$invalid)" ng-show="!item.Claimstatus">Apply</button>
                                                    <button ng-click="vm.removewithConfirm(item)" ng-show="item.Claimstatus">Delete</button>
                                                </td>--%>
                                            </tr>
                                            <tr ng-repeat-end ng-show="expanded">
                                                <td colspan="12" class="padding-lr-0">

                                                    <div class="clearfix">
                                                        <div class="col-sm-3" ng-show="vm.DisciptionShow">
                                                            <label class="bodytxt">Remarks <font class="required-red" ng-show="vm.RemarkRequired">*</font></label>
                                                            <div>
                                                                <input name="RadGrid1$ctl00$ctl04$txtRemarks" class="form-control input-sm" ng-required="vm.RemarkRequired" type="text" ng-model="item.Remarks" id="RadGrid1_ctl00_ctl04_txtRemarks">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3" ng-show="vm.ReciptShow">
                                                            <label class="bodytxt"><font class="required-red" ng-show="vm.ReciptRequired">*</font> Receipt No</label>
                                                            <div>
                                                                <input name="RadGrid1$ctl00$ctl04$txtReceipt" class="form-control input-sm" type="text" ng-model="item.ReceiptNo" maxlength="20" ng-required="vm.ReciptRequired" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3" ng-show="vm.DisciptionShow">
                                                            <label class="bodytxt">Description <font class="required-red" ng-show="vm.AttacementRequired">*</font></label>
                                                            <div>
                                                                <textarea name="RadGrid1$ctl00$ctl04$txtDescription" class="form-control input-sm" type="text" ng-model="item.Description" maxlength="50" ng-required="vm.AttacementRequired"></textarea>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3" ng-show="vm.AttacementShow">
                                                            <label class="bodytxt">Attachment <font class="required-red" ng-show="vm.AttacementRequired">*</font></label>
                                                            <div ng-show="!item.Recpath">
                                                                <input type="file" ngf-select ng-model="item.files" class="btn">
                                                            </div>
                                                            <div ng-show="item.Recpath">
                                                                {{item.Recpath}}
                                                           
                                                            </div>
                                                        </div>
                                                    </div>

                                                </td>
                                            </tr>
                                        </tbody>

                                    </table>

                                </div>
                            </div>

                            <div class="text-center margin-top-10">
                                <button class="btn red" ng-click="vm.applywithConfirm(claimApplingForm.$invalid)">Apply</button>
                                <%--  <button ng-click="vm.removewithConfirm(item)" ng-show="item.Claimstatus">Delete</button>--%>
                            </div>

                        </form>

                        <%--    <div>{{claimApplingForm.$invalid |json}}</div>

        <div>{{vm.ClaimList|json}}</div>--%>

                        <%--  <div><%= CommonData.LoginUser.UserName  %></div>
    <div my-custom-url></div>--%>
                        <%--    <div top-menu></div>--%>
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




            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>



    <uc_js:bundle_js ID="bundle_js" runat="server" />

    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.0-beta.5/angular.min.js"></script>
    <script src="//angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.12.0.js"></script>
    <script src="../scripts/toster.js"></script>
    <script src="../scripts/ng-file-upload-shim.js"></script>
    <script src="../scripts/ng-file-upload.js"></script>
    <script src="../scripts/app.js"></script>
    <script src="../scripts/angular-datepicker.js"></script>


</body>
</html>
