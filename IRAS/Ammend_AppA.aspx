<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ammend_AppA.aspx.cs" Inherits="IRAS.Ammend_AppA" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radClnNew" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Ammend Appendix A</title>
     <script language="JavaScript1.2" type="text/ecmascript"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando()
{
window.parent.expandf()

}
document.ondblclick=expando 

-->



    </script>
</head>
<body>
    <form id="form1" runat="server">
      <radTS:RadScriptManager ID="ScriptManager" runat="server" >
        
        </radTS:RadScriptManager>
  <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="4" style="height: 29px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Ammend Appendix A</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td class="tdstand" style="height: 41px">
                                <asp:Label ID="lblEmployee" runat="server"></asp:Label>
                            </td>
                            <td style="height: 41px">
                                <asp:Label ID="lblerror" runat="server" ForeColor="red" class="bodytxt" Font-Bold="true"></asp:Label>
                            </td>
                            <td align="right" style="height: 41px">
                            </td>
                            <td align="right" style="height: 41px">
                         
                                <input id="ButtonCALCULATE" type="button" runat="server" Text="Calculate" value="calculate"  style="width: 80px; height: 22px"/>
                                <input id="btnsave" type="button" runat="server" style="width: 80px; height: 22px"
                                    value="Save" />
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                OnClick="ButtonCALCULATE_Click"
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
       
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
               
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </strong>
                <strong> AMENDMENT-APPENDIX 8A</strong> <strong></strong>
            </p>
            <h2 align="center">
                Value of Benefits-in-Kind for the Year Ended
            </h2>
            
            <p align="left">
                <strong></strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Full Name of Employee as per NRIC / FIN:&nbsp;
                    <asp:Label ID="name_label" runat="server" Text="Label" Width="223px"></asp:Label></strong><strong>
                        Tax Ref No </strong>:<asp:Label ID="nric_label" runat="server" Text="Label" Width="239px"></asp:Label></p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; 1.</strong> <strong>Value of the place of residence</strong>
                <strong>(See paragraph 14 of the Explanatory Notes): &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $</strong><strong></strong>
                <radG:RadNumericTextBox ID="resistenceVlaue" runat="server" AllowOutOfRangeAutoCorrect="true"
                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                        KeepTrailingZerosOnFocus="True" />
                </radG:RadNumericTextBox></p>
            <table align="center" border="1" cellpadding="0" cellspacing="0" width="90%">
                <tbody>
                    <tr>
                        <td style="width: 271px; height: 24px" valign="top">
                            <p>
                                Address :<asp:Label ID="address_label1" runat="server"></asp:Label>&nbsp;
                            </p>
                            <p>
                                <asp:Label ID="address_label2" runat="server" Width="294px"></asp:Label>&nbsp;</p>
                            <p>
                                <asp:Label ID="address_label3" runat="server"></asp:Label></p>
                        </td>
                        <td style="width: 140px; height: 24px" valign="top">
                            <p>
                                From:<radClnNew:RadDatePicker ID="OccupationFromDate" runat="server" Calendar-ShowRowHeaders="false"
                                    MinDate="01-01-1900">
                                    <DateInput DateFormat="dd/MM/yyyy" Skin="">
                                    </DateInput>
                                    <Calendar ShowRowHeaders="False">
                                    </Calendar>
                                </radClnNew:RadDatePicker>
                            </p>
                            <p>
                                To: &nbsp; &nbsp;<radClnNew:RadDatePicker ID="OccupationToDate" runat="server" Calendar-ShowRowHeaders="false"
                                    MinDate="01-01-1900">
                                    <DateInput DateFormat="dd/MM/yyyy" Skin="">
                                    </DateInput>
                                    <Calendar ShowRowHeaders="False">
                                    </Calendar>
                                </radClnNew:RadDatePicker>
                            </p>
                        </td>
                        <td style="height: 24px" valign="top" width="102">
                            <p>
                                No. of days :<asp:Label ID="noofdaystextbox" runat="server"></asp:Label>&nbsp;
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 271px; height: 35px" valign="top">
                            <p>
                                Annual value of Premises/Rent paid byemployer :<radG:RadNumericTextBox ID="_AVOrRentByEmployerx1"
                                    runat="server" AllowOutOfRangeAutoCorrect="true" MinValue="0" Skin="Vista" Type="Number"
                                    Value="0" Width="103px">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                            </p>
                        </td>
                        <td colspan="2" style="height: 35px" valign="top" width="270">
                            <p>
                                Rent paid by employee :<radG:RadNumericTextBox ID="_RentByEmployee" runat="server"
                                    AllowOutOfRangeAutoCorrect="true" MinValue="0" Skin="Vista" Type="Number" Value="0"
                                    Width="54px">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" width="630">
                            <p>
                                Number of employee(s) sharing the premises (exclude family members who are not employees):
                                <radG:RadNumericTextBox ID="employee_sharing" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp;&nbsp; 2. Value of Furniture &amp; Fittings/Driver/Gardener
                    (Total of 2a to 2k): &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $</strong>&nbsp;
                <asp:Label ID="_total_2a_2k" runat="server"></asp:Label></p>
            <table align="center" border="1" cellpadding="0" cellspacing="0" width="90%">
                <tbody>
                    <tr>
                        <td style="width: 408px; height: 40px" valign="top">
                            <p align="center">
                                Item (Please cross box if applicable)
                            </p>
                        </td>
                        <td colspan="6" style="height: 40px" valign="top" width="120">
                            <p>
                                A) No of Units
                            </p>
                        </td>
                        <td style="width: 120px; height: 40px" valign="top">
                            <p>
                                B) Rate per unit p.m. ($)
                            </p>
                        </td>
                        <td style="width: 71px; height: 40px" valign="top">
                            <p>
                                <strong>#</strong> Value ($)
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 19px" valign="top">
                            <p align="left">
                                &nbsp; a.Furniture : Hard &amp; Soft
                            </p>
                        </td>
                        <td colspan="6" style="height: 19px" valign="top" width="120">
                            <radG:RadNumericTextBox ID="no_furniture" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MaxValue="10" MinValue="0" Skin="Vista" Type="Number" Value="0" Width="75">
                                <ClientEvents OnValueChanging="setvalueof_hartsoftfurniture" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td style="width: 120px; height: 19px" valign="top">
                            <p align="center">
                                10.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 19px" valign="top">
                            <asp:Label ID="ta_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 24px" valign="top">
                            <p align="left">
                                &nbsp; b.<asp:CheckBox ID="refcheck" runat="server" />
                                Refrigerator
                                <asp:CheckBox ID="dvdcheck" runat="server" />Video Recorder/DVD/VCD Player
                            </p>
                        </td>
                        <td colspan="3" style="height: 24px" valign="top" width="60">
                            <radG:RadNumericTextBox ID="no_refrigerator" runat="server" AllowOutOfRangeAutoCorrect="true"
                                EmptyMessage="TV" MaxValue="10" MinValue="0" Skin="Vista" Type="Number" Value="0"
                                Width="75">
                                <ClientEvents OnValueChanging="setvalueof_refrigerator" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td colspan="3" style="height: 24px" valign="top" width="60">
                            <radG:RadNumericTextBox ID="no_dvd" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MaxValue="10" Skin="Vista" Type="Number" Width="75">
                                <ClientEvents OnValueChanging="setvalueof_dvd" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 120px; height: 24px" valign="top">
                            <p align="center">
                                10.00/20.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 24px" valign="top">
                            <asp:Label ID="tb_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 19px" valign="top">
                            <p align="left">
                                &nbsp;c.
                                <asp:CheckBox ID="washcheck" runat="server" />Washing Machine
                                <asp:CheckBox ID="drycheck" runat="server" />Dryer
                                <asp:CheckBox ID="dishcheck" runat="server" />Dish Washer
                            </p>
                        </td>
                        <td style="width: 47px; height: 19px" valign="top">
                            <radG:RadNumericTextBox ID="_NoOfWashingMachines" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_WashingMechine" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td colspan="3" style="height: 19px" valign="top" width="54">
                            <radG:RadNumericTextBox ID="no_of_dryer" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_WashingMechine" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td colspan="2" style="height: 19px" valign="top" width="42">
                            <radG:RadNumericTextBox ID="no_of_diswash1" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_WashingMechine" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td style="width: 120px; height: 19px" valign="top">
                            <p align="center">
                                15.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 19px" valign="top">
                            <asp:Label ID="tc_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 42px" valign="top">
                            <p align="left">
                                &nbsp;d. Air Conditioner :
                                <asp:CheckBox ID="unitcheck" runat="server" />Unit, Central-<asp:CheckBox ID="dinicheck"
                                    runat="server" />Dining
                                <asp:CheckBox ID="sittingcheck" runat="server" />Sitting
                                <asp:CheckBox ID="additioncheck" runat="server" />Additional<asp:CheckBox ID="airpuifiercheck"
                                    runat="server" />Air Purifier
                            </p>
                        </td>
                        <td style="width: 47px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_unitcentral" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_unitcentral" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="height: 42px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="no_of_dining" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_dining" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 14px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_sitting" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_sitting" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="height: 42px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="_no_of_additional" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_additional" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td style="width: 7px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_airpurifier" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_airpurifier" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 120px; height: 42px" valign="top">
                            <p align="center">
                                10.00/15.00/ 15.00/10.00<em>/</em>10.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 42px" valign="top">
                            <asp:Label ID="td_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 62px" valign="top">
                            <p align="left">
                                &nbsp;e.<asp:CheckBox ID="tvcheck" runat="server" />TV/Home Entertainment Theatre/Plasma
                                TV/High definition
                                <asp:CheckBox ID="radiocheck" runat="server" />Radio<asp:CheckBox ID="hificheck"
                                    runat="server" />Hi-Fi Stereo
                                <asp:CheckBox ID="guitarcheck" runat="server" />Electric Guitar
                                <asp:CheckBox ID="surveillance" runat="server" />Surveillance system
                            </p>
                        </td>
                        <td style="width: 47px; height: 62px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_tvplasma1" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                        </td>
                        <td style="height: 62px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="no_of_radio1" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 14px; height: 62px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_hifi" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_hifi" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="height: 62px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="no_of_guitar" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_guitar" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 7px; height: 62px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_surveillance" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_surveillance" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 120px; height: 62px" valign="top">
                            <p align="center">
                                30.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 62px" valign="top">
                            <asp:Label ID="te_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px" valign="top">
                            <p align="left">
                                &nbsp;f.<asp:CheckBox ID="compcheck" runat="server" />Computer<asp:CheckBox ID="organcheck"
                                    runat="server" />
                                Organ
                            </p>
                        </td>
                        <td colspan="2" valign="top" width="48">
                            <radG:RadNumericTextBox ID="no_of_computer" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_computer" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="4" valign="top" width="72">
                            <radG:RadNumericTextBox ID="no_of_organ" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_organ" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 120px" valign="top">
                            <p align="center">
                                40.00
                            </p>
                        </td>
                        <td style="width: 71px" valign="top">
                            <asp:Label ID="tf_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px; height: 21px" valign="top">
                            <p align="left">
                                &nbsp;g. Swimming Pool (exclude swimming pool in condominiums)
                            </p>
                        </td>
                        <td colspan="6" style="height: 21px" valign="top" width="120">
                            <radG:RadNumericTextBox ID="no_of_swimmingpool" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_swimmingpool" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 120px; height: 21px" valign="top">
                            <p align="center">
                                100.00
                            </p>
                        </td>
                        <td style="width: 71px; height: 21px" valign="top">
                            <asp:Label ID="tg_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px" valign="top">
                            <p align="left">
                                &nbsp;h.<asp:CheckBox ID="popcheck" runat="server" />Public Utilities<asp:CheckBox
                                    ID="telecheck" runat="server" />Telephone
                                <asp:CheckBox ID="pager" runat="server" />Pager
                                <asp:CheckBox ID="suitcasecheck" runat="server" AutoPostBack="True" />Suitcase<asp:CheckBox
                                    ID="golfbagcheck" runat="server" EnableViewState="False" />Golf Bag &amp; Accessories
                                <asp:CheckBox ID="camera" runat="server" />Camera
                                <asp:CheckBox ID="servant" runat="server" />Servant
                            </p>
                        </td>
                        <td colspan="7" valign="top" width="240">
                            <p align="center">
                                <radG:RadNumericTextBox ID="publicudilities_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_publicudilities" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox ID="telephone_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_telephone" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;
                                <radG:RadNumericTextBox ID="pager_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_pager" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox ID="suitcase_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_suitcase" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;
                                <radG:RadNumericTextBox ID="golfbag_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_golfbag" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox ID="camera_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_camera" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox ID="sarvent_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_sarvent" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td style="width: 71px" valign="top">
                            <asp:Label ID="th_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px" valign="top">
                            <p align="left">
                                &nbsp;i. Driver
                            </p>
                        </td>
                        <td colspan="7" valign="top" width="240">
                            <p align="center">
                                Annual wages x (private/total mileage)
                                <radG:RadNumericTextBox ID="driver_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_driver" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td style="width: 71px" valign="top">
                            <asp:Label ID="ti_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 408px" valign="top">
                            <p align="left">
                                &nbsp;j. Gardener or Upkeep of Compound
                            </p>
                        </td>
                        <td colspan="7" valign="top" width="240">
                            <p align="center">
                                $35.00 p.m. or the actual wages,whichever is lesser
                                <radG:RadNumericTextBox ID="gardener_value" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_gardener" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td style="width: 71px" valign="top">
                            <asp:Label ID="tj_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="8" valign="top">
                            <p align="left">
                                &nbsp;k. Others (<strong>See </strong><strong>paragraph 15 of the Explanatory Notes)</strong>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $<radG:RadNumericTextBox ID="tk_21" runat="server"
                                    AllowOutOfRangeAutoCorrect="true" MinValue="0" Skin="Vista" Type="Number" Value="0"
                                    Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_other_benifits" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td style="width: 71px" valign="top">
                            <asp:Label ID="tk_2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; # Value for (2a) to (2g) &amp; (2k) = A (
                    No. of units) x B ( Rate p.m.) x 12 x No. of days / 365 (To be apportioned to the
                    no. of </strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;employees sharing the residence)</strong>
            </p>
            <p align="center">
                <strong></strong>
            </p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3. Value of Hotel Accommodation provided
                    (Total of 3a to 3e) : &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; $<asp:Label ID="totalhotelacoomadation"
                        runat="server" Text="0.00"></asp:Label></strong></p>
            <table align="center" border="1" cellpadding="0" cellspacing="0" width="90%">
                <tbody>
                    <tr>
                        <td colspan="3" style="width: 1216px" valign="top">
                        </td>
                        <td colspan="3" valign="top" width="72">
                            <p align="center">
                                A) No. of Persons
                            </p>
                        </td>
                        <td colspan="2" style="width: 417px" valign="top">
                            <p align="center">
                                B) Rate per Person p.m. ($)
                            </p>
                        </td>
                        <td colspan="6" valign="top" width="90">
                            <p align="center">
                                C) Period provided (No. of days)
                            </p>
                        </td>
                        <td colspan="6">
                            <p align="center">
                                Value ($) AxBx12xC/365
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 1216px; height: 42px" valign="top">
                            <p align="left">
                                a. Self<asp:CheckBox ID="selfcheck" runat="server" />
                                Spouse<asp:CheckBox ID="spousecheck" runat="server" />
                                Children
                                <asp:CheckBox ID="childrencheck" runat="server" />&gt; 20 years old
                            </p>
                        </td>
                        <td style="height: 42px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="no_of_self" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_self" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="height: 42px" valign="top" width="24">
                            <radG:RadNumericTextBox ID="no_of_spouse" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_spouse" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td style="width: 17px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="no_of_childrenabove20" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_childrenabove20" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 417px; height: 42px" valign="top">
                            <p align="center">
                                250
                            </p>
                        </td>
                        <td style="width: 10px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="days_self" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                                <ClientEvents OnValueChanging="setvalueof_days_self" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="3" style="height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="days_spouse" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_days_spouse" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 53px; height: 42px" valign="top">
                            <radG:RadNumericTextBox ID="days_childrenabove20" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_days_childrenabove20" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 42px">
                            <asp:Label ID="ta_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 1216px; height: 31px" valign="top">
                            <p align="left">
                                b. Children &lt; 3 yrs old
                            </p>
                        </td>
                        <td colspan="3" style="height: 31px" valign="top" width="72">
                            <radG:RadNumericTextBox ID="no_of_chilbelow3" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_chilbelow3" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 417px; height: 31px" valign="top">
                            <p align="center">
                                25
                            </p>
                        </td>
                        <td colspan="6" style="height: 31px" valign="top" width="90">
                            <radG:RadNumericTextBox ID="days_childbelow3" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_days_chilbelow3" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="tb_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 1216px; height: 31px" valign="top">
                            <p align="left">
                                c. Children 3- 7 years old
                            </p>
                        </td>
                        <td colspan="3" style="height: 31px" valign="top" width="72">
                            <radG:RadNumericTextBox ID="no_of_childabove7" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_childabove" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 417px; height: 31px" valign="top">
                            <p align="center">
                                50
                            </p>
                        </td>
                        <td colspan="6" style="height: 31px" valign="top" width="90">
                            <radG:RadNumericTextBox ID="days_childabove7" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_days_childabove7" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="tc_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 1216px" valign="top">
                            <p align="left">
                                d. Children 8 – 20 years old
                            </p>
                        </td>
                        <td colspan="3" valign="top" width="72">
                            <radG:RadNumericTextBox ID="no_of_child8" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_child8" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="2" style="width: 417px" valign="top">
                            <p align="center">
                                100
                            </p>
                        </td>
                        <td colspan="6" valign="top" width="90">
                            <radG:RadNumericTextBox ID="days_childabove8" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_days_childabove8" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="6">
                            <asp:Label ID="td_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="18" style="height: 21px" valign="top">
                            <p>
                                e. Add: 2% x Basic Salary for period provided &nbsp; &nbsp;&nbsp;
                                <radG:RadNumericTextBox ID="PERSENTBASICPAY" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_days_childabove8" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 21px" valign="top">
                        </td>
                        <td style="width: 22px; height: 21px">
                            <asp:Label ID="te_3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="20" valign="top">
                            <p>
                                <strong>4.Others </strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10" style="height: 32px" valign="top">
                            <p>
                                <strong>&nbsp;a</strong> Cost of home leave passages and incidental benefits</p>
                        </td>
                        <td colspan="1" style="width: 137px; height: 32px" valign="top">
                            No.of &nbsp;passagesforself:<br />
                            <radG:RadNumericTextBox ID="no_of_selfpassages" runat="server" AllowOutOfRangeAutoCorrect="true"
                                MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                <ClientEvents OnValueChanging="setvalueof_selfpassages" />
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                    KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="4" style="height: 32px" valign="top">
                            <p>
                                Spouse:&nbsp;<radG:RadNumericTextBox ID="no_of_passspouse" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_passspouse" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                            </p>
                            <p>
                                <strong></strong>
                            </p>
                            <p>
                                <strong></strong>&nbsp;</p>
                        </td>
                        <td colspan="3" style="height: 32px" valign="top">
                            <p>
                                Children:
                                <radG:RadNumericTextBox ID="no_of_passeschildrn" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <ClientEvents OnValueChanging="setvalueof_passeschildrn" />
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <strong></strong><strong></strong>
                            </p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 32px" valign="top">
                        </td>
                        <td style="width: 22px; height: 32px" valign="top">
                            <p>
                                <strong>
                                    <radG:RadNumericTextBox ID="Costof_leavepassages" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            (<strong>See paragraph 16 of the Explanatory Notes</strong>)Pioneer/export/pioneer
                            service/OHQ Status was awarded or granted extension prior to 1 Jan 2004:
                        </td>
                        <td colspan="4" valign="top">
                            <asp:CheckBox ID="ohqstatus" runat="server" Text="OHQ STATUS" /></td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp; b</strong> Interest payment made by the employer to a third party
                                on behalf of an employee and/or interest benefits arising from loans&nbsp; provided
                                by employer interest free or at a rate below market rate to the employee who has
                                substantial shareholding or control or influence over the company :
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="interestpayment" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="height: 31px" valign="top">
                            <p>
                                <strong>&nbsp;c</strong> Life insurance premiums paid by the employer :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" style="height: 31px" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="lifeinsurance" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;d</strong> Free or subsidised holidays including air passage, etc.
                                : <strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="subsidial_holydays" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="height: 31px" valign="top">
                            <p>
                                <strong>&nbsp;e&nbsp; </strong>Educational expenses including tutor provided :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" style="height: 31px" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="educational" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;f</strong> Non-monetary awards for long service (for awards exceeding
                                $200 in value) : <strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="longserviceavard" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;g</strong> Entrance/ transfer fees and annual subscription to social
                                or recreational clubs :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="socialclubsfee" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;h</strong> Gains from assets, e.g. vehicles, property, etc. sold to
                                employees at a price lower than open market value :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="gainfromassets" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="height: 31px" valign="top">
                            <p>
                                <strong>&nbsp;i</strong> Full cost of motor vehicles given to employee :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" style="height: 31px" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="fullcostofmotor" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;j</strong> Car benefits <strong>(See paragraph 17 of the Explanatory Notes)</strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong>
                                    <radG:RadNumericTextBox ID="carbenefits" runat="server" AllowOutOfRangeAutoCorrect="true"
                                        MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                            KeepTrailingZerosOnFocus="True" />
                                    </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" valign="top">
                            <p>
                                <strong>&nbsp;k</strong> Other non-monetary benefits which do not fall within the
                                above items<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <radG:RadNumericTextBox ID="non_manetarybenifits" runat="server" AllowOutOfRangeAutoCorrect="true"
                                    MinValue="0" Skin="Vista" Type="Number" Value="0" Width="54px">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                                        KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="16" style="height: 61px" valign="top">
                            <p>
                                <strong></strong>
                            </p>
                            <h3>
                                &nbsp;&nbsp; TOTAL VALUE OF BENEFITS-IN-KIND (ITEMS 1 TO 4) TO BE REFLECTED IN ITEM
                                d9 OF &nbsp; FORM IR8A
                            </h3>
                        </td>
                        <td colspan="4" style="height: 61px" valign="top">
                            <p align="center">
                                <strong>
                                    <asp:Label ID="GarndTotal" runat="server" Text="0.00"></asp:Label></strong></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; IR8A(A/1/</strong> <strong>2014) </strong>
                <strong>There are penalties for failing to give a return or furnishing an incorrect
                    or late return.</strong> <strong></strong>
            </p>
   
                  
                       
             
    </form>
</body>
</html>
