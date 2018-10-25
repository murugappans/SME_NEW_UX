<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="claimaddition.ascx.cs"
    Inherits="SMEPayroll.Payroll.claimaddition" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>


<div class="clearfix form-style-inner">
    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Adding a New Record" : "Editing Record" %>'></asp:Label>
    </div>




    <hr />


    <div class="form approver">
        <div class="form-body">
            <span>Approver:&nbsp;
                                <asp:Label ID="lblsupervisor" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.approver")%>'
                                    CssClass="bodytxt" Visible="false"></asp:Label>
                <asp:Label ID="lblsupervisor_name" runat="server" Text='' CssClass="bodytxt"></asp:Label>

            </span>
        </div>
    </div>


    <div class="form form-inline">
        <div class="form-body">
            <div class="form-group clearfix">

                <label class="control-label">Employee <tt class="required">*</tt></label>

                <asp:DropDownList ID="drpemployee" Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? (1==1) : !(1==1)%>'
                    runat="server" OnDataBound="drpemployee_DataBound" OnSelectedIndexChanged="drpemployee_SelectedIndexChanged"
                    AutoPostBack="True" CssClass="form-control input-inline input-sm input-medium cmb-employee">
                </asp:DropDownList>
            </div>
            <div class="form-group clearfix">

                <label class="control-label">Amount Only <tt class="required">*</tt></label>
                <asp:TextBox ID="txtamt" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.trx_amount")%>'
                    CssClass="form-control input-inline input-sm input-medium text-right input-amount number-dot" data-type="currency" MaxLength="12">
                </asp:TextBox>
                <asp:Label ID="lblEr" ForeColor="Brown" runat="server"></asp:Label>

            </div>
            <div class="form-group clearfix">

                <label class="control-label">Currency <tt class="required">*</tt></label>
                <asp:DropDownList ID="drpCurrency" class="bodytxt form-control input-inline input-sm input-medium"
                    runat="server">
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">


                <label class="control-label">
                    <asp:Label ID="Tra" runat="server">Transaction<tt class="required">*</tt></asp:Label>
                </label>
                <asp:Label ID="From" runat="server"></asp:Label>
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? (1==1) : !(1==1)%>'
                    DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period")%>' ID="RadDatePicker1"
                    runat="server" Width="116px">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                    <DateInput ID="DateInput1" runat="server" Skin="" CssClass="form-control input-xsmall input-sm padding-right-0 input-transaction-from" DateFormat="dd/MM/yyyy" />

                </radCln:RadDatePicker>

                <asp:Label ID="to" runat="server">-</asp:Label>

                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period")%>'
                    Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? (1==1) : !(1==1)%>'
                    runat="server" Width="116px">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                    <DateInput ID="DateInput2" runat="server" Skin="" CssClass="form-control input-xsmall input-sm padding-right-0  input-transaction-to" DateFormat="dd/MM/yyyy" />

                </radCln:RadDatePicker>

            </div>
            <div class="form-group clearfix">

                <label class="control-label">Claims Type <tt class="required">*</tt></label>
                <asp:DropDownList ID="drpaddtype" runat="server" CssClass="form-control input-inline input-sm input-medium cmb-claims-type"
                    OnDataBound="drpaddtype_DataBound">
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">

                <label class="control-label">Remarks</label>
                <asp:TextBox ID="claimRemark" CssClass="form-control input-inline input-sm input-medium custom-maxlength" TextMode="MultiLine" data-maxlength="250"
                    Wrap="true" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Remarks")%>'></asp:TextBox>

            </div>
            <div class="form-group clearfix">

                <label class="control-label">Receipt Upload</label>
                <radU:RadUpload ID="RadUpload1" CssClass="inline-block claim-file" InitialFileInputsCount="1" runat="server" ControlObjectsVisibility="ClearButtons"
                    MaxFileInputsCount="1" OverwriteExistingFiles="True" />

            </div>
        </div>
        <div class="text-center">
            <asp:Button ID="btnUpdate" runat="server" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                CssClass="btn red btn-add-update" />

            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" CssClass="btn default" />
        </div>
    </div>



    <asp:Label ID="lblComid" runat="server" Width="0" Visible="false"></asp:Label>


</div>


<radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <radA:AjaxSetting AjaxControlID="drpaddtype">
            <UpdatedControls>
                <radA:AjaxUpdatedControl ControlID="lblcpf"></radA:AjaxUpdatedControl>
            </UpdatedControls>
        </radA:AjaxSetting>
    </AjaxSettings>
</radA:RadAjaxManager>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
&nbsp;
<%--by jammu office <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Apply Claims - Amount Required" SetFocusOnError="True"></asp:RequiredFieldValidator> by jammu office--%>
<%--by jammu office<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Apply Claims -Amount Is Invalid MaximumValue=1000000,MinimumValue=0" MaximumValue="1000000" MinimumValue="0"
    Type="Double"></asp:RangeValidator>by jammu office--%>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
    Display="None" ErrorMessage="Apply Claims - To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="Apply Claims - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<%--by jammu office<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
    ErrorMessage="Apply Claims - Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
    ErrorMessage="Apply Claims - Addition Type Required!" InitialValue="-select-" ControlToValidate="drpaddtype"></asp:RequiredFieldValidator> by jammu office--%>
<%--<asp:CompareValidator ID="cmpEndDate" runat="server"  
 ErrorMessage="Apply Claims : To date cannot be less than start date" 
 ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2" 
 Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>  --%>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script language="javascript" src="../Frames/Script/CommonValidations.js">
    </script>
    <script language="javascript" type="text/javascript">

        function GetExchangeValue() {
            var currencyID = document.getElementById("<%= drpCurrency.ClientID %>").value;
            //var vaiable2    =   document.getElementById("<%= drpCurrency.ClientID %>"); 
            var compid = document.getElementById("<%= lblComid.ClientID %>").innerHTML;
            var amount = document.getElementById("<%= txtamt.ClientID %>").value;
            var res = SMEPayroll.Payroll.claimaddition.GetExchangeValue(currencyID, compid, amount);
            if (res.value != null) {
                document.getElementById("<%= lblEr.ClientID %>").innerHTML = res.value;
            }
            //alert(res.value);
            // var lblleave=document.getElementById('lblLeaveText');lblleave.innerHTML
        }

    </script>

</telerik:RadCodeBlock>




<script type="text/javascript">
    window.onload = function () {
        $(".ruFileWrap .ruFakeInput").addClass("form-control input-inline input-sm input-small");
       
    };




    function validateform() {
        var _message = "";
        var today = new Date();
        var dd = today.getDate() > 9 ? (today.getDate()) : ("0" + (today.getDate()));
        var mm = (today.getMonth() + 1) > 9 ? (today.getMonth() + 1) : ("0" + (today.getMonth() + 1)); var yyyy = today.getFullYear();
        today = yyyy + '-' + mm + '-' + dd;

        if ($.trim($(".cmb-employee").val()) === "-select-")
            _message = "Employee Name Required.   <br/>";
        if ($.trim($(".input-amount").val()) === "")
            _message += "Input amount.  <br/>";

        if ($(".input-transaction-from").is(':visible')) {//muru
            // alert('visible');
            if ($.trim($(".input-transaction-from").val()) === "" || $.trim($(".input-transaction-to").val()) === "")
                _message += "Transaction dates cannot be empty.  <br/>";
            if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) > today || $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()) > today)
                _message += "Claim for future dates cannot be applied.  <br/>";

            // if ($.trim($(".input-transaction-from").val()) > $.trim($(".input-transaction-to").val()))
            if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) > $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val())) {
                _message += "From date cannot be greater than To date.  <br/>";
                $(document.getElementById("<%= RadDatePicker2.ClientID %>"))
            }
        }

        if ($.trim($(".cmb-claims-type").val()) === "-select-")
            _message += "Claim Type required.  <br/>";

        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>


