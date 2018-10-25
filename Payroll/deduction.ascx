<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="deduction.ascx.cs" Inherits="SMEPayroll.Payroll.deduction" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

    <script type="text/javascript">

<%--funtion validatedate(Object,args)
{
    var stDate = document.getElementById('RadDatePicker1').value;
    var enDate = document.getElementById('RadDatePicker2').value;
    if (stDate > enDate)
    {
        alert('From Date can not be greater than To date, please enter correct dates');
        args.Isvalid=false;
    }
    else
    {
        args.Isvalid=true;
    }
}
--%></script>

</radG:RadCodeBlock>

<%--width: 691px;--%>

<div class="clearfix form-style-inner">
    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "Adding Record" : "Editing Record" %>'
            Width="100%"></asp:Label>
    </div>


    <hr />



    <div class="form form-inline">
        <div class="form-body">

            <div class="form-group clearfix">
                <label class="control-label">Employee <tt class="required">*</tt></label>

                <asp:DropDownList ID="drpemployee" OnDataBound="drpemployee_databound" Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? true : false %>'
                    runat="server" CssClass="form-control input-sm input-inline input-medium">
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Deduction Type <tt class="required">*</tt></label>

                <asp:DropDownList ID="drpaddtype" OnDataBound="drpaddtype_databound" runat="server"
                    CssClass="form-control input-sm input-inline input-medium">
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Transaction Period <tt class="required">*</tt></label>



                <radCln:RadDatePicker
                    ID="RadDatePicker1" Calendar-ShowRowHeaders="false" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period_copy")%>'
                    Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? true : false %>'
                    runat="server" Width="116px" DateInput-DisplayDateFormat="dd/MM/yyyy">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </radCln:RadDatePicker>
                -                    
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period_copy")%>'
                    Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? true : false %>'
                    runat="server" Width="116px" DateInput-DisplayDateFormat="dd/MM/yyyy">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </radCln:RadDatePicker>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Amount Only <tt class="required">*</tt></label>

                <asp:TextBox ID="txtamt" MaxLength="12" data-type="currency" AutoPostBack="false" Text='<%# DataBinder.Eval(Container,"DataItem.trx_amount")%>'
                    runat="server" CssClass="form-control input-sm input-inline input-medium text-right number-dot"></asp:TextBox>
                &nbsp;&nbsp;
           
                       

                <asp:Label ID="lblEr" ForeColor="Brown" runat="server"></asp:Label>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Currency</label>

                <asp:DropDownList ID="drpCurrency" class="form-control input-sm input-inline input-medium" AutoPostBack="false" runat="server">
                    <%--Changed by Sandi--%>
                </asp:DropDownList>

            </div>




        </div>


        <div class="form-actions text-center">
            <asp:Button ID="btnUpdate" runat="server" CommandName='<%#((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "PerformInsert" : "Update" %>'
                Text='<%#((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "Insert" : "Update" %>'
                CssClass="btn red" OnClientClick="return Validations();" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" CssClass="btn default" />
        </div>


    </div>




</div>

<table>
    <tr style="display: none">
        <td>
            <asp:Label ID="lblComid" runat="server" Width="0"></asp:Label></td>
    </tr>
</table>


<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drpemployee"
    Display="None" ErrorMessage="Employee Name Required!" InitialValue="-select-"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpaddtype"
    Display="None" ErrorMessage="Deduction Type Required!" InitialValue="-select-"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="From Date Required!"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="RadDatePicker2"
    Display="None" ErrorMessage="To Date Required!"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Amount Required!"></asp:RequiredFieldValidator>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Amount Is Invalid!" MaximumValue="1000000000000000000000000"
    MinimumValue="0" Type="Double"></asp:RangeValidator>
<asp:CompareValidator ID="cmpEndDate" runat="server"
    ErrorMessage="To date cannot be less Start date"
    ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2"
    Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>

<script language="javascript" type="text/javascript"> 
    function Validations() {
       // var today = new Date();
       // var dd = today.getDate() > 9 ? (today.getDate()) : ("0"+ (today.getDate()));
       // var mm = (today.getMonth() + 1) > 9 ? (today.getMonth() + 1) : ("0"+ (today.getMonth() + 1));
       // var yyyy = today.getFullYear();
      
       // today = yyyy + '-' + mm + '-' + dd;
        var alertmsg = "";
        if ($(document.getElementById("<%= drpemployee.ClientID %>")).val() == "-select-") {

            alertmsg += "Employee is not selected. </br>";
        }
        if ($(document.getElementById("<%= drpaddtype.ClientID %>")).val() == "-select-") {

            alertmsg += "Deduction Type is not selected. </br>";
        }
       <%-- if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) > today || $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()) > today)
            alertmsg += "Claim for future dates cannot be applied <br/>";--%>
        if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) === "" || $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()) === "")
            alertmsg += "Transaction dates cannot be empty. <br/>";
        else 
            if($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) > $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()))
            alertmsg += "From date cannot be greater than To date.   <br/>";

        if ($(document.getElementById("<%= txtamt.ClientID %>")).val() == "") {

            alertmsg += "Amount for deduction is not added. </br>";
        }
       
        if (alertmsg != "") {
            WarningNotification(alertmsg);
            return false;
        }

    }
   function GetExchangeValue()
    {
        var currencyID = document.getElementById("<%= drpCurrency.ClientID %>").value;
        
        //var vaiable2    =   document.getElementById("<%= drpCurrency.ClientID %>"); 
        var compid = document.getElementById("<%= lblComid.ClientID %>").innerHTML;   
       
        var amount = document.getElementById("<%= txtamt.ClientID %>").value;
        var res = SMEPayroll.Payroll.deduction.GetExchangeValue(currencyID,compid,amount);  
        if(res.value!=null)
        {
            document.getElementById("<%= lblEr.ClientID %>").innerHTML=res.value;
        }
        //alert(res.value);
        // var lblleave=document.getElementById('lblLeaveText');lblleave.innerHTML
    }
    
</script>







