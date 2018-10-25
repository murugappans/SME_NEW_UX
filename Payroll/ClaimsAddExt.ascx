<%@ Control Language="C#" AutoEventWireup="true" Codebehind="ClaimsAddExt.ascx.cs"
    Inherits="SMEPayroll.Payroll.ClaimsAddExt" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<center>

 
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
        </tr>
        <tr>
            <td colspan="5" style="color: #000000; height: 28px; background-color: #e9eed4; text-align: center">
                <asp:Label ID="Label1" runat="server" 
                    Width="297px"></asp:Label></td>
        </tr>
        <tr >
            <td style="height: 31px; text-align: right; width: 158px;">
                <tt class="bodytxt"><b>Employee:&nbsp;</b></tt>
                <br />
            </td>
            <td style="height: 31px; text-align: left; width: 450px;">
                <radA:RadAjaxPanel ID="r1" runat="Server" Width="434px">
                    <asp:DropDownList ID="drpemployee" 
                        runat="server" Width="184px" OnDataBound="drpemployee_DataBound" OnSelectedIndexChanged="drpemployee_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    Approver:&nbsp;
                    <asp:Label ID="lblsupervisor" runat="server" 
                        Width="183px" CssClass="bodytxt"></asp:Label></radA:RadAjaxPanel>
            </td>
            <td style="height: 31px; text-align: right; width: 158px;">
                <tt class="bodytxt"><b>GstYesNo</b></tt>
                <br />
            </td>
            <td style="height: 31px; text-align: left">
                    <asp:DropDownList ID="drpGstYN" class="bodytxt"  
                            runat="server">
                                <asp:ListItem Value="0" Text="Y"></asp:ListItem>
                                <asp:ListItem Value="1" Text="N"></asp:ListItem>
                        </asp:DropDownList>
                    <asp:Label ID="lblGstCode" Text="GstCode" runat="server" CssClass="bodytxt"></asp:Label>
            </td>
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><tt class="required">*</tt><b>Currency:&nbsp;</b></tt>
            </td>
               <td style="font-weight: bold; font-size: 8pt; width: 163px; color: #000000; font-family: verdana;
                         height: 32px; text-align: left">
                    <asp:DropDownList ID="drpCurrency" class="bodytxt" 
                            runat="server">
                        </asp:DropDownList>
             </td>
        </tr>
        <tr>
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><tt class="required">*</tt><b>Amount(With Gst)</b></tt>
            </td>
            <td style="height: 32px; text-align: left">
                 <asp:TextBox ID="txtAmntWithGst" runat="server" 
                    Width="182px">
                </asp:TextBox>
                <asp:Label ID="lblAmntWithGst" ForeColor="Brown" runat="server"></asp:Label>
            </td>
            
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><tt class="required">*</tt><b>Gst Amount</b></tt>
            </td>
            <td style="height: 32px; text-align: left">
                 <asp:TextBox ID="txtAmtGst" runat="server" 
                    Width="182px">
                </asp:TextBox>
                <asp:Label ID="lblAmtGst" ForeColor="Brown" runat="server"></asp:Label>
            </td>
            
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><tt class="required">*</tt><b>Amount (With out Gst)</b></tt>
            </td>
            <td style="height: 32px; text-align: left">
                 <asp:TextBox ID="txtAmtWithOutGst" runat="server" 
                    Width="182px">
                </asp:TextBox>
                <asp:Label ID="lblAmtWithoutgst" ForeColor="Brown" runat="server"></asp:Label>
            </td>
            
            
        </tr>
        <tr >
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><tt class="required">*</tt><b>Transaction Period:&nbsp;</b></tt>
            </td>
            <td colspan="2" style="height: 32px; text-align: left">
                From
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1"
                    runat="server" Width="96px">
                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                </radCln:RadDatePicker>
                &nbsp; &nbsp;&nbsp;- &nbsp; &nbsp;&nbsp;&nbsp;
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" runat="server" Width="102px">
                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                </radCln:RadDatePicker>
            </td>
            <td style="height: 32px; text-align: left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 32px; text-align::left; width: 158px;">
                <tt class="bodytxt"><b>Receipt Upload:</b></tt>
            </td>
            <td align="left">
                <radU:RadUpload ID="RadUpload1" InitialFileInputsCount="1" runat="server" ControlObjectsVisibility="ClearButtons"
                    MaxFileInputsCount="1" OverwriteExistingFiles="True" />
            </td>
        </tr>
        <tr >
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><b>Claims Type:&nbsp;</b></tt></td>
            <td colspan="3" style="height: 32px; text-align: left">
                <asp:DropDownList ID="drpaddtype" runat="server" AutoPostBack="true" Width="475px"
                    OnSelectedIndexChanged="drpaddtype_SelectedIndexChanged" OnDataBound="drpaddtype_DataBound">
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td style="height: 32px; text-align: right; width: 158px;">
                <tt class="bodytxt"><b>Remarks:&nbsp;</b></tt></td>
            <td colspan="3" style="height: 32px; text-align: left">
                <asp:TextBox ID="claimRemark" Width="600" Font-Names="Tahoma" Font-Size="11px" TextMode="MultiLine"
                    Wrap="true" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="height: 39px; text-align: left">
            </td>
            <td style="height: 39px; text-align: right; width: 307px;">
                <asp:Button ID="btnUpdate" runat="server"  Text="Update" Width="85px"/>
            </td>
            <td style="height: 39px; text-align: left">
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Width="64px" />
            </td>
            
        </tr>
        <tr>
            <td colspan="5" style="height: 27px; background-color: #e9eed4; text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr style="display:none">
            <asp:Label ID="lblComid"  runat="server" Width="0" ></asp:Label>
        </tr>
    </table>
</center>
<%--<radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <radA:AjaxSetting AjaxControlID="drpaddtype">
            <UpdatedControls>
                <radA:AjaxUpdatedControl ControlID="lblcpf"></radA:AjaxUpdatedControl>
            </UpdatedControls>
        </radA:AjaxSetting>
    </AjaxSettings>
</radA:RadAjaxManager>--%>
<%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
&nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Apply Claims - Amount Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtamt"
    Display="None" ErrorMessage="Apply Claims -Amount Is Invalid MaximumValue=1000000,MinimumValue=0" MaximumValue="1000000" MinimumValue="0"
    Type="Double"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
    Display="None" ErrorMessage="Apply Claims - To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="Apply Claims - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
    ErrorMessage="Apply Claims - Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
    ErrorMessage="Apply Claims - Addition Type Required!" InitialValue="-select-" ControlToValidate="drpaddtype"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="cmpEndDate" runat="server"  
 ErrorMessage="Apply Claims : To date cannot be less than start date" 
 ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2" 
 Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>--%>  

       
<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script language="javascript" src="../Frames/Script/CommonValidations.js">
        </script> 
<%--        <script language="javascript"  type="text/javascript" >
         
//            function GetExchangeValue()
//            {
//                var currencyID = document.getElementById("<%= drpCurrency.ClientID %>").value;
//                //var vaiable2    =   document.getElementById("<%= drpCurrency.ClientID %>"); 
//                var compid = document.getElementById("<%= lblComid.ClientID %>").innerHTML;   
//                var amount = document.getElementById("<%= txtamt.ClientID %>").value;
//                var res = SMEPayroll.Payroll.claimaddition.GetExchangeValue(currencyID,compid,amount);  
//                if(res.value!=null)
//                {
//                    document.getElementById("<%= lblEr.ClientID %>").innerHTML=res.value;
//                }
//                //alert(res.value);
//                // var lblleave=document.getElementById('lblLeaveText');lblleave.innerHTML
//            }
         
         </script> --%> 

</telerik:RadCodeBlock>

 
 
 



