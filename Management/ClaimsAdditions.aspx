<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimsAdditions.aspx.cs"
    Inherits="SMEPayroll.Management.ClaimsAdditions" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />





    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>

    <script language="javascript">
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
                strmsg = strmsg + "Please select an Employee." + "\n";
            }
            if (ctrl6.value.trim().length <= 0) {
                strmsg = strmsg + "Please enter an Amount" + "\n";
            }
            if (ctrl5.value == '') {
                strmsg = strmsg + "Please select year for the Claim Addition." + "\n";
            }
            if (ctrl7.value == '-select-') {
                strmsg = strmsg + "Please select an Claim Addition type." + "\n";
            }
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
                        <li>Claims Additions</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="MultiCurrency.aspx"><span>MultiCurrency</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Claims Additions</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>

                            <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>
                                        <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                                            
                                            <tr bgcolor="<% =sOddRowColor %>">
                                                <td>
                                                    <tt class="bodytxt">Search:</tt>&nbsp;
                                <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields"
                                    AutoPostBack="true" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList></td>
                                                <td>
                                                    <tt class="bodytxt">Employee:</tt>&nbsp;
                                <asp:DropDownList ID="drpemp" runat="server" Width="156px" CssClass="textfields">
                                </asp:DropDownList>
                                                    <tt class="bodytxt">Month:</tt>&nbsp;
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textfields">
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
                                                    &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" /></td>
                                                <td>
                                                
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
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
                            <br />
                            <radG:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                OnUpdateCommand="RadGrid1_UpdateCommand" OnItemCommand="RadGrid1_ItemCommand" AllowAutomaticInserts="false" AllowAutomaticUpdates="false" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnInsertCommand="RadGrid1_InsertCommand" DataSourceID="SqlDataSource2" GridLines="None"
                                Skin="Default" Width="93%">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id" DataSourceID="SqlDataSource2"
                                    CommandItemDisplay="Bottom">
                                    <Columns>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="false" HeaderText="Code" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="emp_name" AllowFiltering="false"
                                            HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                            ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="desc" HeaderText="Description" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="contains" ReadOnly="True" SortExpression="Type" UniqueName="Type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="Type"
                                            Visible="False" UniqueName="Type1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_amount" AllowFiltering="false" DataType="System.Decimal"
                                            HeaderText="Amount" SortExpression="trx_amount" UniqueName="trx_amount">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_period" AllowFiltering="false" DataType="System.DateTime"
                                            HeaderText="Period" SortExpression="trx_period" UniqueName="trx_period">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="cpf" AllowFiltering="false" DataType="System.string"
                                            HeaderText="Cpf" SortExpression="cpf" UniqueName="cpf">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="iras_approval_date" AllowFiltering="false" DataType="System.string"
                                            HeaderText="Approval Date" UniqueName="iras_approval_date">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="additionsforyear" AllowFiltering="false" DataType="System.string"
                                            HeaderText="Additions For Year" ItemStyle-Width="100px" SortExpression="additionsforyear"
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
                                            <ItemStyle Width="30px" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings UserControlName="ClaimsdditionAll.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                    <CommandItemSettings AddNewRecordText="Add New Claim Addition" />
                                </MasterTableView>
                            </radG:RadGrid>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppay_addAll"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpemp" Name="empcode" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="DropDownList1" Name="empmonth" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="empyear" PropertyName="SelectedValue"
                                        Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
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
        //$("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");
         window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }
    </script>
</body>
</html>
