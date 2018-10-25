<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AppendixA.aspx.cs" Inherits="IRAS.TEST.AppendixA" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radClnNew" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
         <div>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 2014 </strong><strong>APPENDIX 8A</strong> <strong></strong>
            </p>
            <h2 align="center">
                Value of Benefits-in-Kind for the Year Ended 31 Dec 2013
            </h2>
            <p align="center">
                <strong>(Fill in this form if applicable and give it to your employee by 1 Mar 2014
                    for his submission together with his Income Tax Return) </strong>
            </p>
            <p align="left">
                <strong></strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Full Name of Employee as per NRIC / FIN:&nbsp;
                    <asp:Label ID="nricLabel" runat="server" Text="Label" Width="223px"></asp:Label></strong><strong>
                        Tax Ref No </strong>:<asp:Label ID="taxrefnoLabel" runat="server" Text="Label" Width="239px"></asp:Label></p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; 1.</strong> <strong>Value of the place of residence</strong> <strong>(See paragraph
                    14 of the Explanatory Notes):                 $</strong><strong><asp:Label ID="furn_totallabel"
                        runat="server" Text="0.00"></asp:Label></strong>
            </p>
            <table border="1" cellspacing="0" cellpadding="0" align="center" width="90%">
                <tbody>
                    <tr>
                        <td valign="top" style="width: 284px; height: 24px">
                            <p>
                                Address :<asp:Label ID="address_label" runat="server" Text="Label"></asp:Label>
                                
                        </td>
                        <td valign="top" style="width: 140px; height: 24px">
                            <p>Period of occupation :</p>
                               <p> From:<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="_OccupationFromDate"
                                                                                runat="server">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
                                   <Calendar ShowRowHeaders="False">
                                   </Calendar>
                                                                            </radClnNew:RadDatePicker></p>
                               <p> To: <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="_OccupationToDate"
                                                                                runat="server" OnSelectedDateChanged="enddate_SelectedDateChanged">
                                                                                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                                                                                </DateInput>
                                   <Calendar ShowRowHeaders="False">
                                   </Calendar>
                                                                            </radClnNew:RadDatePicker></p>                                             
                        </td>
                        <td width="102" valign="top" style="height: 24px">
                            <p>
                                No. of days : 
                                
                                <radG:RadNumericTextBox runat="server" ID="noofdaystextbox" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                         </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 284px; height: 50px;">
                         <p>
                                Annual value of Premises/Rent paid by employer :<radG:RadNumericTextBox runat="server" ID="_AVOrRentByEmployerx1" Width="103px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true"   MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Double">
                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                           </p>
                        </td>
                        <td width="270" colspan="2" valign="top" style="height: 50px">
                            <p>
                                Rent paid by employee :<radG:RadNumericTextBox runat="server" ID="_RentByEmployee" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                        <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td width="630" colspan="3" valign="top">
                            <p>
                                Number of employee(s) sharing the premises (exclude family members who are not employees):
                            </p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp;&nbsp; 2. Value of Furniture &amp; Fittings/Driver/Gardener (Total of 2a to 2k):</strong>&nbsp;<radG:RadNumericTextBox runat="server" ID="_FurnitureValue" Width="103px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true"   MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Double">
                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                </radG:RadNumericTextBox>
                <strong></strong>
            </p>
            <table border="1" cellspacing="0" cellpadding="0" align="center" width="90%">
                <tbody>
                    <tr>
                        <td valign="top" style="width: 408px; height: 40px;">
                            <p align="center">
                                Item (Please cross box if applicable)
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 40px">
                            <p>
                                A) No of Units
                            </p>
                        </td>
                        <td width="120" valign="top" style="height: 40px">
                            <p>
                                B) Rate per unit p.m. ($)
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 40px;">
                            <p>
                                <strong>#</strong> Value ($)
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 19px;">
                            <p align="left">
                                &nbsp; a.Furniture : Hard &amp; Soft
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 19px">
                         <radG:RadNumericTextBox runat="server" ID="no_furniture" Width="75" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10" ShowSpinButtons="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                        </td>
                        <td width="120" valign="top" style="height: 19px">
                            <p align="center">
                                10.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 19px;">
                            <asp:Label ID="Label1" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 24px;">
                            <p align="left">
                                &nbsp; b.<asp:CheckBox ID="refcheck" runat="server" /> Refrigerator 
                                <asp:CheckBox ID="dvdcheck" runat="server" />Video Recorder/DVD/VCD Player
                            </p>
                        </td>
                        <td width="60" colspan="3" valign="top" style="height: 24px">
                          <radG:RadNumericTextBox runat="server" ID="no_refrigerator" Width="75" EmptyMessage="TV"
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10" ShowSpinButtons="true" MinValue="0" Value="0"
                        Type="Number"  DataType="System.Int" >
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox>
                    </td>
                        <td width="60" colspan="3" valign="top" style="height: 24px">
                        
                    <radG:RadNumericTextBox runat="server" ID="no_dvd" Width="75" EmptyMessage="TV"
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MaxValue="10" ShowSpinButtons="true"
                        Type="Number" DataType="System.Int">
                        <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                    </radG:RadNumericTextBox></td>
                    
                        <td width="120" valign="top" style="height: 24px">
                            <p align="center">
                                10.00/20.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 24px;">
                            <asp:Label ID="Label2" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 19px;">
                            <p align="left">
                                &nbsp;c. <asp:CheckBox ID="washcheck" runat="server" />Washing Machine <asp:CheckBox ID="drycheck" runat="server" />Dryer <asp:CheckBox ID="dishcheck" runat="server" />Dish Washer
                            </p>
                        </td>
                        <td valign="top" style="height: 19px; width: 47px;"><radG:RadNumericTextBox runat="server" ID="_NoOfWashingMachines" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="54" colspan="3" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="no_of_dryer" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="42" colspan="2" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="no_of_diswasher" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="120" valign="top" style="height: 19px">
                            <p align="center">
                                15.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 19px;">
                            <asp:Label ID="Label3" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 42px;">
                            <p align="left">
                                &nbsp;d. Air Conditioner : <asp:CheckBox ID="unitcheck" runat="server" />Unit, Central-<asp:CheckBox ID="dinicheck" runat="server" />Dining <asp:CheckBox ID="sittingcheck" runat="server" />Sitting <asp:CheckBox ID="additioncheck" runat="server" />Additional<asp:CheckBox ID="airpuifiercheck" runat="server" />Air Purifier
                            </p>
                        </td>
                        <td valign="top" style="width: 47px; height: 42px;"><radG:RadNumericTextBox runat="server" ID="no_of_unitcentral" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="no_of_unitcentral_TextChanged">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="no_of_dining" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 14px;"><radG:RadNumericTextBox runat="server" ID="no_of_sitting" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="_no_of_additional_ac" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="width: 7px; height: 42px;"><radG:RadNumericTextBox runat="server" ID="no_of_airpurifier" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="120" valign="top" style="height: 42px">
                            <p align="center">
                                10.00/15.00/ 15.00/10.00<em>/</em>10.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 42px;">
                            <asp:Label ID="Label4" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 62px;">
                            <p align="left">
                                &nbsp;e.<asp:CheckBox ID="tvcheck" runat="server" />TV/Home Entertainment Theatre/Plasma TV/High definition
                               <asp:CheckBox ID="radiocheck" runat="server" />Radio<asp:CheckBox ID="hificheck" runat="server" />Hi-Fi Stereo <asp:CheckBox ID="guitarcheck" runat="server" />Electric Guitar <asp:CheckBox ID="surveillance" runat="server" />Surveillance system
                            </p>
                        </td>
                        <td valign="top" style="width: 47px; height: 62px;"><radG:RadNumericTextBox runat="server" ID="no_of_tvplasma" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 62px"><radG:RadNumericTextBox runat="server" ID="no_of_radio" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 14px; height: 62px;"><radG:RadNumericTextBox runat="server" ID="no_of_hifi" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 62px"><radG:RadNumericTextBox runat="server" ID="no_of_guitar" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="width: 7px; height: 62px;"><radG:RadNumericTextBox runat="server" ID="no_of_surveillance" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="120" valign="top" style="height: 62px">
                            <p align="center">
                                30.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 62px;">
                            <asp:Label ID="Label5" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;f.<asp:CheckBox ID="compcheck" runat="server" />Computer<asp:CheckBox ID="organcheck" runat="server" />
                                Organ
                            </p>
                        </td>
                        <td width="48" colspan="2" valign="top"><radG:RadNumericTextBox runat="server" ID="no_of_computer" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="72" colspan="4" valign="top"><radG:RadNumericTextBox runat="server" ID="no_of_organ" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="120" valign="top">
                            <p align="center">
                                40.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="Label6" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px; height: 21px;">
                            <p align="left">
                                &nbsp;g. Swimming Pool (exclude swimming pool in condominiums)
                            </p>
                        </td>
                        <td width="120" colspan="6" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="no_of_swimmingpool" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="120" valign="top" style="height: 21px">
                            <p align="center">
                                100.00
                            </p>
                        </td>
                        <td valign="top" style="width: 71px; height: 21px;">
                            <asp:Label ID="Label7" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;h.<asp:CheckBox ID="popcheck" runat="server" />Public Utilities<asp:CheckBox ID="telecheck" runat="server" />Telephone
                                <asp:CheckBox ID="pager" runat="server" />Pager <asp:CheckBox ID="suitcasecheck" runat="server" AutoPostBack="True" />Suitcase<asp:CheckBox ID="golfbagcheck" runat="server" EnableViewState="False" />Golf Bag &amp; Accessories
                               <asp:CheckBox ID="camera" runat="server" />Camera <asp:CheckBox ID="servant" runat="server" />Servant
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center"><radG:RadNumericTextBox runat="server" ID="publicudilities_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="telephone_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>&nbsp;
                                <radG:RadNumericTextBox runat="server" ID="pager_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="suitcase_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="golfbag_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="camera_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                                <radG:RadNumericTextBox runat="server" ID="sarvent_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="Label8" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;i. Driver
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center">
                                Annual wages x (private/total mileage)
                                <radG:RadNumericTextBox runat="server" ID="driver_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="Label9" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="top" style="width: 408px">
                            <p align="left">
                                &nbsp;j. Gardener or Upkeep of Compound
                            </p>
                        </td>
                        <td width="240" colspan="7" valign="top">
                            <p align="center">
                                $35.00 p.m. or the actual wages, whichever is lesser
                                <radG:RadNumericTextBox runat="server" ID="gardener_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></p>
                        </td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="Label10" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="8" valign="top">
                            <p align="left">
                                &nbsp;k. Others (<strong>See </strong><strong>paragraph 15 of the Explanatory Notes)</strong>
                            <radG:RadNumericTextBox runat="server" ID="other_benifits_value" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="RadNumericTextBox18_TextChanged">
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></p></td>
                        <td valign="top" style="width: 71px">
                            <asp:Label ID="Label11" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; # Value for (2a) to (2g) &amp; (2k) = A ( No. of units) x B ( Rate p.m.) x 12
                    x No. of days / 365 (To be apportioned to the no. of </strong>
            </p>
            <p align="left">
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;employees sharing the residence)</strong>
            </p>
            <p align="center">
                <strong></strong>
            </p>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 3. Value of Hotel Accommodation provided (Total of 3a to 3e) : 
                    <asp:Label ID="Label12" runat="server" Text="0.00"></asp:Label></strong></p>
            <table border="1" cellspacing="0" cellpadding="0" width="90%" align="center">
                <tbody>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px">
                        </td>
                        <td width="72" colspan="3" valign="top">
                            <p align="center">
                                A) No. of Persons
                            </p>
                        </td>
                        <td colspan="2" valign="top" style="width: 417px">
                            <p align="center">
                                B) Rate per Person p.m. ($)
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top">
                            <p align="center">
                                C) Period provided
                                (No. of days)
                            </p>
                        </td>
                        <td colspan="6">
                            <p align="center">
                                Value ($)
                                AxBx12xC/365
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="height: 42px; width: 1216px;">
                            <p align="left">
                                a. Self<asp:CheckBox ID="selfcheck" runat="server" /> Spouse<asp:CheckBox ID="spousecheck" runat="server" /> Children <asp:CheckBox ID="childrencheck" runat="server" />&gt; 20 years old
                            </p>
                        </td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="no_of_self" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td width="24" valign="top" style="height: 42px"><radG:RadNumericTextBox runat="server" ID="no_of_spouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td valign="top" style="height: 42px; width: 17px;"><radG:RadNumericTextBox runat="server" ID="no_of_childrenabove20" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 417px;">
                            <p align="center">
                                250
                            </p>
                        </td>
                        <td valign="top" style="height: 42px; width: 10px;"><radG:RadNumericTextBox runat="server" ID="days_self" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="3" valign="top" style="height: 42px; width: 1px;"><radG:RadNumericTextBox runat="server" ID="days_spouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="height: 42px; width: 13px;"><radG:RadNumericTextBox runat="server" ID="days_childrenabove20" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 42px">
                            <asp:Label ID="Label13" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px; height: 31px">
                            <p align="left">
                                b. Children &lt; 3 yrs old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="no_of_chilbelow3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px; height: 31px">
                            <p align="center">
                                25
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="days_childbelow3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="Label14" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px; height: 31px">
                            <p align="left">
                                c. Children 3- 7 years old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="no_of_childabove7" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px; height: 31px">
                            <p align="center">
                                50
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top" style="height: 31px"><radG:RadNumericTextBox runat="server" ID="days_childabove7" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6" style="height: 31px">
                            <asp:Label ID="Label15" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3" valign="top" style="width: 1216px">
                            <p align="left">
                                d. Children 8 – 20 years old
                            </p>
                        </td>
                        <td width="72" colspan="3" valign="top"><radG:RadNumericTextBox runat="server" ID="no_of_child8" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="no_of_child8_TextChanged">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="2" valign="top" style="width: 417px">
                            <p align="center">
                                100
                            </p>
                        </td>
                        <td width="90" colspan="6" valign="top"><radG:RadNumericTextBox runat="server" ID="days_childabove8" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                            <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                        </radG:RadNumericTextBox></td>
                        <td colspan="6">
                            <asp:Label ID="Label16" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="18" style="height: 21px" valign="top">
                            <p>
                                e. Add: 2% x Basic Salary for period provided
                            </p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 21px" valign="top">
                        </td>
                        <td style="width: 22px; height: 21px">
                            <asp:Label ID="Label17" runat="server" Text="0.00"></asp:Label></td>
                    </tr>
                
                   
                    
                    <tr>
                        <td colspan="20" valign="top">
                            <p>
                                <strong>4.Others </strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="10" style="height: 78px">
                            <p>
                                <strong>&nbsp;a</strong>
                                Cost of home leave passages and incidental benefits&nbsp;
                            </p>
                            <p>
                                &nbsp;(<strong>See paragraph 16 of the Explanatory Notes</strong>)Pioneer/export/pioneer
                                service/OHQ Status was &nbsp;&nbsp;</p>
                            <p>
                                awarded or granted extension prior
                                to 1 Jan 2004:
                            </p>
                        </td>
                        <td colspan="1" style="height: 78px" valign="top">
                            No.of &nbsp;passages for self: 
                            <br />
                            <radG:RadNumericTextBox runat="server" ID="no_of_selfpassages" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="no_of_child8_TextChanged">
                                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox></td>
                        <td colspan="4" valign="top" style="height: 78px">
                            <p>
                                Spouse:&nbsp;<radG:RadNumericTextBox runat="server" ID="no_of_passspouse" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="no_of_child8_TextChanged">
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox>
                            </p>
                            <p>
                                <strong></strong>
                            </p>
                            <p>
                               
                                <strong> </strong>&nbsp;</p>
                        </td>
                        <td colspan="3" valign="top" style="height: 78px">
                            <p>
                                Children: <radG:RadNumericTextBox runat="server" ID="no_of_passeschildrn" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="no_of_child8_TextChanged">
                                    <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox><p>
                                <strong></strong>
                            </p>
                               
                                <strong></strong>
                            </p>
                        </td>
                        <td colspan="1" style="width: 73px; height: 78px" valign="top">
                            <asp:CheckBox ID="ohqstatus" runat="server" /></td>
                        <td valign="top" style="width: 22px; height: 78px;">
                            <p>
                                <strong><radG:RadNumericTextBox runat="server" ID="RadNumericTextBox28" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp; b</strong>
                                Interest payment made by the employer to a third party on behalf of an employee
                                and/or interest benefits arising from loans&nbsp; provided by employer interest free or
                                at a rate below market rate to the employee who has substantial shareholding or
                                control or influence over the company :
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="interestpayment" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;c</strong>
                                Life insurance premiums paid by the employer :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="lifeinsurance" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int" OnTextChanged="RadNumericTextBox30_TextChanged">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;d</strong>
                                Free or subsidised holidays including air passage, etc. : <strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="subsidial_holydays" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;e&nbsp; </strong>
                                Educational expenses including tutor provided :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="educational" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;f</strong>
                                Non-monetary awards for long service (for awards exceeding $200 in value) : <strong>
                                </strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="longserviceavard" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;g</strong>
                                Entrance/ transfer fees and annual subscription to social or recreational clubs
                                :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="socialclubsfee" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;h</strong>
                                Gains from assets, e.g. vehicles, property, etc. sold to employees at a price lower
                                than open market value :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="gainfromassets" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong></p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;i</strong>
                                Full cost of motor vehicles given to employee :<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="fullcostofmotor" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;j</strong>
                                Car benefits <strong>(See paragraph 17 of the Explanatory Notes)</strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center">
                                <strong><radG:RadNumericTextBox runat="server" ID="carbenefits" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                    <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                                </radG:RadNumericTextBox></strong>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="16">
                            <p>
                                <strong>&nbsp;k</strong>
                                Other non-monetary benefits which do not fall within the above items<strong></strong>
                            </p>
                        </td>
                        <td colspan="4" valign="top">
                            <p align="center"><radG:RadNumericTextBox runat="server" ID="non_manetarybenifits" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  DataType="System.Int">
                                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
                            </radG:RadNumericTextBox>&nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" style="height: 61px" colspan="16">
                            <p>
                                <strong></strong>
                            </p>
                            <h3>
                                &nbsp;&nbsp;
                                TOTAL VALUE OF BENEFITS-IN-KIND (ITEMS 1 TO 4) TO BE REFLECTED IN ITEM d9 OF &nbsp; FORM
                                IR8A
                            </h3>
                        </td>
                        <td colspan="4" valign="top" style="height: 61px">
                            <p align="center">
                                <strong>
                                    <asp:Label ID="Label18" runat="server" Text="0.00"></asp:Label></strong></p>
                        </td>
                    </tr>
                </tbody>
            </table>
            <p>
                <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; IR8A(A/1/</strong> <strong>2014) </strong><strong>There are penalties for failing
                    to give a return or furnishing an incorrect or late return.</strong> <strong></strong>
            </p>
        </div>
    </form>
</body>
</html>
