<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TimeSlot.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimeSlot" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
        
</head>
<body style="margin-left: auto">


    <form id="form1" runat="server">
     <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Timesheet Pattern</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%--<td width="5%">
                    <img alt="" src="../frames/images/EMPLOYEE/Top-Employeegrp.png" /></td>--%>
            </tr>
        </table>
        
        <div>
        <asp:Label runat="server" ID="patten"></asp:Label>
      <br />
        <asp:Label runat="server" ID="project"></asp:Label>
        
        </div>
    
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div>
            <table cellpadding="3" cellspacing="0" border="1" width="99%" style="height: 99%">
                <tr>
                    <td colspan="4" align="center" class="bodytxt">
                        <b>Time Settings</b></td>
                </tr>
                <tr>
                    <td class="bodytxt" align="right">
                        In Time:&nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpInTime" runat="server" Skin="Outlook" TabIndex="0">
                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                        </radG:RadTimePicker>
                        &nbsp;</td>
                    <td class="bodytxt" align="right">
                        Out Time:&nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpOutTime" runat="server" Skin="Outlook" TabIndex="0">
                        </radG:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td class="bodytxt" align="right">
                        <asp:Label ID="lblEarlyin" Text="Early In By:" runat="server"></asp:Label>
                        &nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpEarlyInTime" runat="server" Skin="Outlook"
                            TabIndex="0">
                        </radG:RadTimePicker>
                    </td>
                    <td class="bodytxt" align="right">
                        <asp:Label ID="lblEarlyOut" Text="Early Out By:" runat="server"></asp:Label>
                        &nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpEarlyOutTime" runat="server" Skin="Outlook"
                            TabIndex="0">
                        </radG:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td class="bodytxt" align="right">
                        <asp:Label ID="lblLateIn" Text="Late In By:" runat="server"></asp:Label>
                        &nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpLateInTime" runat="server" Skin="Outlook">
                        </radG:RadTimePicker>
                    </td>
                   <td class="bodytxt" align="right">
                        Rounding:&nbsp;</td>
                    
                    <td>
                      <telerik:RadComboBox ID="cmbRound" runat="server" Width="100px" EmptyMessage="-select-"
                                                                        MarkFirstMatch="true" Font-Names="Tahoma" EnableLoadOnDemand="true">
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
                    </td>
                </tr>
            </table>
            <!-- break time setting -->
            <table cellpadding="3" cellspacing="0" border="1" width="99%" style="height: 99%">
                <tr>
                    <td colspan="4" align="center" class="bodytxt">
                        <b>Break Time Settings</b></td>
                </tr>
                <tr>
                    <td class="bodytxt" align="right">
                        NH After:&nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpBreakTimeNH" runat="server" Skin="Outlook"
                            TabIndex="0">
                        </radG:RadTimePicker>
                    </td>
                    <td class="bodytxt" align="right">
                        OT After:&nbsp;</td>
                    <td>
                        <radG:RadTimePicker Width="80px" ID="rtpBreakTimeOT" runat="server" Skin="Outlook"
                            TabIndex="0">
                        </radG:RadTimePicker>
                    </td>
                </tr>
                <tr>
                    <td class="bodytxt" align="right">
                        Break Time NH:&nbsp;</td>
                    <td>
                        <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBreakTime"
                            runat="server" MinValue="0" MaxValue="600" NumberFormat-AllowRounding="true"
                            NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                        </radG:RadNumericTextBox>&nbsp;&nbsp;<span class="bodytxt">Min</span>
                    </td>
                    <td class="bodytxt" align="right">
                        Break Time OT:&nbsp;</td>
                    <td>
                        <radG:RadNumericTextBox Width="50px" NumberFormat-GroupSeparator="" ID="txtBreakTimeOT"
                            runat="server" MinValue="0" MaxValue="600" NumberFormat-AllowRounding="true"
                            NumberFormat-DecimalDigits="0" IncrementSettings-InterceptArrowKeys="true">
                        </radG:RadNumericTextBox>&nbsp;&nbsp;<span class="bodytxt">Min</span>
                    </td>
                    
                </tr>
                <tr>
                
                </tr>
                <tr>
                    <td colspan="4" align="center" class="bodytxt">
                        <asp:Button ID="btnInsert" runat="server" Text="Add" OnClick="btnInsert_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Clear" Visible="false" /><br />
                        <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
   
</body>
</html>
