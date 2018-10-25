<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="addition.ascx.cs" Inherits="SMEPayroll.Payroll.addition" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>


<script language="javascript" type="text/javascript">
    function isItemSelected(source, arguments) {
        if (arguments.Value == '-1') {
            alert(arguments.Value);
            arguments.IsValid = false;
        }
        else {
            arguments.IsValid = true;
        }
    }



</script>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Record" : "Editing Record" %>'
            Width="100%"></asp:Label>
    </div>


    
        <hr />
   

   

        <div class="form form-inline">
            <div class="form-body">

                <div class="form-group clearfix">

                    <label class="control-label">Employee <tt class="required">*</tt></label>
                    
                        <asp:DropDownList ID="drpemployee" CssClass="form-control input-inline input-sm input-medium" OnDataBound="drpemployee_databound" Enabled='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? (1==1) : !(1==1)%>'
                            runat="server">
                        </asp:DropDownList>
                    

                </div>

                <div class="form-group clearfix">

                    <label class="control-label">Amount Only <tt class="required">*</tt></label>
                    
                        <asp:TextBox ID="txtamt" CssClass="form-control input-inline input-sm input-medium text-right number-dot" data-type="currency" MaxLength="12" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.trx_amount")%>'></asp:TextBox>&nbsp;
                <asp:Label ID="lblEr" ForeColor="Brown" runat="server"></asp:Label>
                    

                    </div>

                    <div class="form-group clearfix">
                    <label class="control-label">Currency</label>
                    
                        <asp:DropDownList ID="drpCurrency" class="form-control input-inline input-sm input-medium" AutoPostBack="false" runat="server">
                            <%--Changed by Sandi--%>
                        </asp:DropDownList>
                   
                </div>




                <div class="form-group clearfix">

                    <label class="control-label">Transaction Period <tt class="required">*</tt></label>
                    
                        
                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? true : false %>'
                            DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period_copy")%>'
                            ID="RadDatePicker1" runat="server" Width="116px" DateInput-DisplayDateFormat="dd/MM/yyyy">
                            <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                           
          
                        </radCln:RadDatePicker>
                        -
                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.trx_period_copy")%>'
                            Enabled='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? (1==1) : !(1==1)%>'
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

                    <label class="control-label">Additions for Year <tt class="required">*</tt></label>
                    
                        <asp:DropDownList ID="drpAdditionYear" CssClass="form-control input-inline input-sm input-medium" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                            runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="-select-">Select</asp:ListItem>
                        </asp:DropDownList>
                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                   

                </div>


                <div class="form-group clearfix">

                    <label class="control-label"> Addition Type <tt class="required">*</tt></label>
                    
                        <asp:DropDownList ID="drpaddtype" CssClass="form-control input-inline input-sm input-medium" OnDataBound="drpaddtype_databound" runat="server"
                            OnSelectedIndexChanged="drpaddtype_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>

                    <span class="cpfpayable">
                        CPF Payable:
                        <asp:Label ID="lblcpf" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.cpf")%>' Visible="true"></asp:Label>
                    </span>
                   

                </div>

                <div class="form-group clearfix" id="tr1" runat="server">

                    <label class="control-label">Basis Arriving Payment</label>
                    
                        <asp:TextBox ID="txtbasis_arriving_payment" Text='<%# DataBinder.Eval(Container,"DataItem.basis_arriving_payment")%>'
                            MaxLength="100" class="form-control input-inline input-sm input-medium" runat="server"></asp:TextBox>
                    

                </div>

                <div class="form-group clearfix" id="tr2" runat="server">

                    <label class="control-label">Service Length</label>
                    
                        <asp:TextBox ID="txtservice_length" MaxLength="52" Text='<%# DataBinder.Eval(Container,"DataItem.service_length")%>'
                            class="form-control input-inline input-sm input-medium" runat="server"></asp:TextBox>
                    

                </div>

                <div class="form-group clearfix" id="tr3" runat="server">

                    <label class="control-label">Is IRAS Approval</label>
                    
                        <asp:DropDownList ID="drpiras_approval" DataTextField="text" class="form-control input-inline input-sm input-medium" runat="server">
                            <asp:ListItem Value="No" Text="No"></asp:ListItem>
                            <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                        </asp:DropDownList>
                    

                </div>





                <div class="form-group clearfix" id="tr4" runat="server">

                    <label class="control-label">Approval Date</label>
                    
                        <asp:TextBox ID="txtiras_approval_date" Text='<%# DataBinder.Eval(Container,"DataItem.iras_approval_date")%>'
                            MaxLength="10" class="form-control input-inline input-sm input-medium" runat="server"></asp:TextBox>
                        <asp:Label runat="server" ID="lblDate" Text="( mm/dd/yyyy (Example 11/21/2011 i.e 21-Nov-2011))"></asp:Label>
                    

                </div>



            </div>


            <div class="form-actions text-center">
                <asp:Button ID="btnUpdate" CssClass="btn red" runat="server" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "PerformInsert" : "Update" %>'
                    Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "Insert" : "Update" %>'
                    OnClientClick="return Validations();" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default" />
            </div>

        </div>
    
        




</div>

<table>
    <tr style="display: none">
        <td colspan="5" style="height: 27px; background-color: #e9eed4; text-align: center">&nbsp;
                <asp:DropDownList ID="drplumsum" runat="server" AutoPostBack="True" Width="90%">
                </asp:DropDownList>
        </td>
        <asp:Label ID="lblComid" runat="server" Width="0" Visible ="false"></asp:Label>
    </tr>
</table>


<%--<radCln:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <radCln:AjaxSetting AjaxControlID="drpaddtype">
            <UpdatedControls>
                <radCln:AjaxUpdatedControl ControlID="lblcpf"></radCln:AjaxUpdatedControl>
            </UpdatedControls>
        </radCln:AjaxSetting>
    </AjaxSettings>
</radCln:RadAjaxManager>--%>
<%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
&nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Amount Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Amount Is Invalid!" MaximumValue="10000000000000" MinimumValue="0"
    Type="Double"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
    Display="None" ErrorMessage="To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
    ErrorMessage="Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
    ErrorMessage="Addition Type Required!" InitialValue="-select-" ControlToValidate="drpaddtype"></asp:RequiredFieldValidator>--%>

<script language="javascript" src="../Frames/Script/CommonValidations.js">
</script>

<script language="javascript" type="text/javascript">
   
 var submit = 0;
    <%-- function Validations() {
   if ($(document.getElementById("<%= drpemployee.ClientID %>")).val() == "-select-") {
       WarningNotification("Please Select Employee");
      return false;
    }
    if ($(document.getElementById("<%= drpaddtype.ClientID %>")).val() == "-select-") {
         WarningNotification("Please Select Addition Type");
        return false;
   }
    if ($(document.getElementById("<%= drpAdditionYear.ClientID %>")).val() == "-select-") {
        WarningNotification("Please Select Additions for Year");
        return false;
     }
     if ($(document.getElementById("<%= txtamt.ClientID %>")).val() == "") {
         WarningNotification("Please insert amount");
         return false;
     }
     if ($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val() == "") {
         WarningNotification("Please Select To date");
         return false;
     }
     if ($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val() == "") {
         WarningNotification("Please Select From Date");
         return false;
     }
    
 }--%>
    function Validations() {
        var _message = "";
        var today = new Date();
        var dd = today.getDate() > 9 ? (today.getDate()) : ("0" + (today.getDate()));
        var mm = (today.getMonth() + 1) > 9 ? (today.getMonth() + 1) : ("0"+ (today.getMonth() + 1)); var yyyy = today.getFullYear();
        today = yyyy + '-' + mm + '-' + dd;
        if ($.trim($(document.getElementById("<%= drpemployee.ClientID %>")).val()) === "-select-")
            _message = "Employee Name Required. <br/>";
      if ($.trim($(document.getElementById("<%= txtamt.ClientID %>")).val()) === "")
            _message += "Amount for addition cannot be empty. <br/>";
        if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) === "" || $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()) === "")
            _message += "Transaction dates cannot be empty. <br/>";
        else
            if ($.trim($(document.getElementById("<%= RadDatePicker1.ClientID %>")).val()) > $.trim($(document.getElementById("<%= RadDatePicker2.ClientID %>")).val()))
                _message += "From date cannot be greater than To date.   <br/>";
      if ($.trim($(document.getElementById("<%= drpAdditionYear.ClientID %>")).val()) === "-select-")
            _message+= "Select Additions for Year. <br/>";
    if ($.trim($(document.getElementById("<%= drpaddtype.ClientID %>")).val()) === "-select-")
            _message += "Select Additions Type. <br/>";

        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
     <%--      if (++submit > 1) {

            return false;
        }

        var newMsg = "";
        /** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
        var msg = "Payroll Additions - Amount Only,Payroll Additions - Addition Type";
        //var msg =   "Payroll Additions - Amount Only";
        var srcData = "";
        //Control Validation
        //validateData(srcCtrl,destSrc,opType,srcData,msg,con)            
        var variable1 = document.getElementById("<%= txtamt.ClientID %>");
        var vaiable2 = document.getElementById("<%= drpaddtype.ClientID %>");

        var srcCtrl = variable1.id + ',' + vaiable2.id;
        var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
        if (strirmsg != "") {
            newMsg += strirmsg;
            newMsg = "Following fields are missing.\n" + newMsg + "\n";
        }

        strirmsg = "";
        var strirmsg = CheckNumeric(variable1.value, "\n Payroll Additions - Amount Only");
        if (strirmsg != "")
            newMsg += strirmsg;

        //Check Date Values

        //alert(document.getElementById('RadDatePicker1').id); 
        var variable3 = document.getElementById("<%= RadDatePicker1.ClientID %>");
        var variable4 = document.getElementById("<%= RadDatePicker2.ClientID %>");

        strirmsg = "";
        strirmsg = CompareDate(variable3.value, variable4.value,
                "Payroll Additions - From Date Can not greater than To Date\n", "");
        if (strirmsg != "")
            newMsg += strirmsg;

        if (newMsg == "") {

            return true;
        }
        else {
            alert(newMsg);


            submit = 0;
            return false;

        }

    }

    function GetExchangeValue() {
        var currencyID = document.getElementById("<%= drpCurrency.ClientID %>").value;
        //var vaiable2    =   document.getElementById("<%= drpCurrency.ClientID %>"); 
        var compid = document.getElementById("<%= lblComid.ClientID %>").innerHTML;
        var amount = document.getElementById("<%= txtamt.ClientID %>").value;
        var res = SMEPayroll.Payroll.addition.GetExchangeValue(currencyID, compid, amount);
        if (res.value != null) {
            document.getElementById("<%= lblEr.ClientID %>").innerHTML = res.value;
        }
        //alert(res.value);
        // var lblleave=document.getElementById('lblLeaveText');lblleave.innerHTML
    }
--%>
</script>

