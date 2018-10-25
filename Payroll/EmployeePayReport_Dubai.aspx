<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeePayReport_Dubai.aspx.cs"
    Inherits="SMEPayroll.Payroll.EmployeePayReport_Dubai" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Employee PayRoll</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />

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

</head>
<body>
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <br />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee Pay Detail</b></font>
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
        <center>
            <table id="tbl1" runat="server" cellpadding="2" cellspacing="2" width="99%;height:100%"
                border="0">
                <tr>
                    <td>
                        <table cellpadding="2" cellspacing="2" border="0" style="width: 100%; height: 100%;">
                            <tr style="display: none">
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr style="border-width: thick; border-style: double;">
                                <td colspan="6" align="center">
                                    <b><tt class="bodytxt" style="text-align: center; font-size: 16px;">
                                        <asp:Label ID="lblPaySlip" runat="server" Text="-"></asp:Label></tt></b></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Name:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lbName" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold" style="text-align: center">Employee Group:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblEmpGroup" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold" style="text-align: center">Working Days in a week:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblWorkingDays" runat="server" Text="-"></asp:Label></tt></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Emp Type:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblEmpType" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">CPF Applicable:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblCPFApp" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">Day Rate($):</tt>
                                    </td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblDayRate" runat="server" Text="-"></asp:Label>
                                      
                                        </tt>
                                      <tt class="bodytxt" style="text-align: left">
                                        
                                        <asp:Label ID="subpay_dailyrate" runat="server" Text="-"></asp:Label>
                                        </tt>   
                                        
                                        </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Department:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblDept" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">CPF Age Group:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblCPFAgeGrp" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">Hourly Rate($):</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblHrRate" runat="server" Text="-"></asp:Label></tt>
                                         <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="sub_lblHrRate" runat="server" Text="-"></asp:Label></tt>
                                        
                                        </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Pay Mode:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblPayMode" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold" style="text-align: left">CPF Employee Rate(%):</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblCPFEmpRate" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">OT Entitiled:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblOTEnt" runat="server" Text="-"></asp:Label></tt></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Joining Date:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblJoinDate" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">CPF Employer Rate(%):</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblCPFEmpyrate" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">OT 1 rate:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblOT1rate" runat="server" Text="-"></asp:Label></tt></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Termination Date:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblTermDate" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">PR Date:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblPRDate" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">OT 2 rate:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblOT2rate" runat="server" Text="-"></asp:Label></tt></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <tt class="colnamebold">Working Days in Payroll:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblWorkDaysInRoll" runat="server" Text="-"></asp:Label></tt></td>
                                <td align="right">
                                    <tt class="colnamebold">Act Working Days In Payroll:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblActWorkDaysInRoll" runat="server" Text="-"></asp:Label>
                                         <asp:Label ID="subbay_workdays" runat="server" Text=""></asp:Label>
                                        </tt></td>
                                <td align="right">
                                    <tt class="colnamebold">UnPaid Leaves:</tt></td>
                                <td align="left">
                                    <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="lblUnpaid" runat="server" Text="-"></asp:Label></tt>
                                        
                                        <tt class="bodytxt" style="text-align: left">
                                        <asp:Label ID="sub_lblUnpaid" runat="server" Text="-"></asp:Label></tt>
                                        
                                        </td>
                            </tr>
                        </table>
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td class="colnamebold" align="left">
                        (A) - Actual Basic Pay:
                        <asp:Label ID="lblActualBasic" runat="server" Text="-"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="bodytxt" align="left">
                        <table cellpadding="2" cellspacing="0" border="0" style="height: 100%;">
                            <tr>
                                <td>
                                    {<asp:Label ID="lblBasic" runat="server" Text="-"></asp:Label>
                                    (i.e
                                    <asp:Label ID="lblActWorkDaysInRolltbl1" runat="server" Text="-"></asp:Label>
                                    x (<asp:Label ID="lblDayRatetbl1" runat="server" Text="-"></asp:Label>
                                    Day rate))<asp:Label ID="subpay_day" runat="server" Text=""></asp:Label>
                                                                          
                                    }
                                </td>
                                <td runat="server" id="tdunpaid">
                                    - {Unpaid Leaves(<asp:Label ID="lblUnpaidtbl1" runat="server" Text="-"></asp:Label>):
                                    <asp:Label ID="lblTotalUnpaid" runat="server" Text="-"></asp:Label>}<%--(i.e
                                    <asp:Label ID="lblUnpaidtbl2" runat="server" Text="-"></asp:Label>
                                    x (<asp:Label ID="lblDayRatetbl2" runat="server" Text="-"></asp:Label>
                                    Day Rate))}--%>
                                </td>
                                <td runat="server" id="sub_tdunpaid">
                                    - {Unpaid Leaves(<asp:Label ID="sub_lblUnpaidtbl1" runat="server" Text="-"></asp:Label>):
                                    <asp:Label ID="sub_lblTotalUnpaid" runat="server" Text="-"></asp:Label>}<%--i.e
                                    <asp:Label ID="sub_lblUnpaidtbl2" runat="server" Text="-"></asp:Label>
                                    x (<asp:Label ID="sub_lblDayRatetbl2" runat="server" Text="-"></asp:Label>
                                    Day Rate))}--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="trPRBasic1" runat="server">
                    <td class="colnamebold" align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Actual PR Basic Pay:<asp:Label
                            ID="lblPRBasicPay" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr id="trPRBasic2" runat="server">
                    <td class="bodytxt" align="left">
                        <table cellpadding="2" cellspacing="0" border="0" style="height: 100%;">
                            <tr>
                                <td>
                                    {<asp:Label ID="lblBasicPR" runat="server" Text="-"></asp:Label>
                                    (i.e
                                    <asp:Label ID="lblActWorkDaysInRolltbl1PR" runat="server" Text="-"></asp:Label>
                                    x (<asp:Label ID="lblDayRatetbl1PR" runat="server" Text="-"></asp:Label>
                                    Day rate))}
                                </td>
                                <td runat="server" id="tdunpaidPR">
                                    - {Unpaid Leaves(<asp:Label ID="lblUnpaidtbl1PR" runat="server" Text="-"></asp:Label>):
                                    <asp:Label ID="lblTotalUnpaidPR" runat="server" Text="-"></asp:Label>(i.e
                                    <asp:Label ID="lblUnpaidtbl2PR" runat="server" Text="-"></asp:Label>
                                    x (<asp:Label ID="lblDayRatetbl2PR" runat="server" Text="-"></asp:Label>
                                    Day Rate))}
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblAddnDed" runat="server" visible="true" cellpadding="2" cellspacing="0"
                            border="1" style="width: 90%; height: 100%; border-style: none;">
                            <tr style="display: none">
                                <td class="bodytxt">
                                </td>
                                <td class="bodytxt">
                                </td>
                                <td class="bodytxt">
                                </td>
                                <td class="bodytxt">
                                </td>
                            </tr>
                            <tr style="height: 25px; background-color: #CCCCCC">
                                <td class="colnamebold" style="font-size: 14px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right" colspan="2">
                                    ADDITIONS</td>
                                <td class="colnamebold" style="font-size: 14px; border-style: double; border-bottom-width: 0px;"
                                    align="right" colspan="2">
                                    DEDUCTIONS</td>
                            </tr>
                            <tr>
                                <td colspan="2" valign="top" style="width: 50%; border-style: double; border-right-width: 0px">
                                    <radG:RadGrid ID="RadGrid1" runat="server" GridLines="None" Skin="Default" ShowHeader="false"
                                        ShowFooter="false">
                                        <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None">
                                            <Columns>
                                                <radG:GridBoundColumn ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Right" UniqueName="AddTYpe"
                                                    DataField="AddTYpe">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn UniqueName="AddAmt" ItemStyle-HorizontalAlign="Right" DataField="AddAmt">
                                                </radG:GridBoundColumn>
                                            </Columns>
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                </td>
                                <td colspan="2" valign="top" style="width: 50%; border-style: double; border-bottom-width: 0px">
                                    <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" Skin="Default" ShowHeader="false"
                                        ShowFooter="false">
                                        <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="None">
                                            <Columns>
                                                <radG:GridBoundColumn ItemStyle-Width="90%" ItemStyle-HorizontalAlign="Right" UniqueName="AddTYpe"
                                                    DataField="AddTYpe">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn UniqueName="AddAmt" ItemStyle-HorizontalAlign="Right" DataField="AddAmt">
                                                </radG:GridBoundColumn>
                                            </Columns>
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-top-width: 0px;
                                    border-bottom-width: 0px; border-right-width: 0px;" align="right">
                                    (B) ADDITIONS($):</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-top-width: 0px;
                                    border-bottom-width: 0px; border-right-width: 0px" align="right">
                                    &nbsp;
                                    <asp:Label ID="lblTotAdd" runat="server" Text="-"></asp:Label>
                                </td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    (C) DEDUCTIONS($):</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px"
                                    align="right">
                                    &nbsp;
                                    <asp:Label ID="lblTotDed" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="font-size: 11px; border-style: double; border-bottom-width: 0px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px"
                                    align="right" colspan="4">
                                    {(A+B)-C} Net Pay($): &nbsp;<asp:Label ID="lblNetPay" ForeColor="blue" runat="server"
                                        Text="-"></asp:Label></td>
                            </tr>
                            <tr visible=false id="trgross" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    Gross Pay*:&nbsp;</td>
                                    
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    &nbsp;
                                    <asp:Label ID="lblGrossPay" runat="server" Text="-"></asp:Label></td>
                                <td colspan="2" style="font-size: 11px; border-style: double; border-bottom-width: 0px">
                                    &nbsp;
                                </td>
                            </tr>
                             <tr id="tr1" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    Gross Pay*:&nbsp;</td>
                                    
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    &nbsp;
                                    <asp:Label ID="lblGrosspayCPF" runat="server" Text="-"></asp:Label></td>
                                <td colspan="2" style="font-size: 11px; border-style: double; border-bottom-width: 0px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trempy" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    <asp:Label ID="lbltxtEmpy" runat="server" Text=""></asp:Label>*&nbsp;</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-bottom-width: 0px;
                                    border-right-width: 0px" align="right">
                                    &nbsp;
                                    <asp:Label ID="lblEmpyCPF" runat="server" Text=""></asp:Label></td>
                                <td colspan="2" style="font-size: 11px; border-style: double; border-bottom-width: 0px">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="trsdl" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px"
                                    align="right">
                                    SDL*:&nbsp;</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px"
                                    align="right">
                                    &nbsp;
                                    <asp:Label ID="lblSDL" runat="server" Text="-"></asp:Label></td>
                                <td colspan="2" style="font-size: 13px; border-style: double">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table id="tblRefund" runat="server" cellpadding="2" cellspacing="0" border="1" style="width: 90%;
                            height: 100%; border-style: none;">
                            <tr id="trRef1" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    Total Last Yr Ord Wages</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    Current Yr Ord Wages</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    AW Ceiling</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    Actual Ceiling</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    AW Paid till Last Mth</td>
                                <td colspan="2" class="colnamebold" style="font-size: 13px; border-style: double;
                                    color: Maroon">
                                    <asp:Label ID="lblRefAW" Font-Bold="true" runat="server" Text=""></asp:Label>&nbsp;
                                </td>
                            </tr>
                            <tr id="trRef2" runat="server">
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    <asp:Label ID="lblLYOW" runat="server" Text=""></asp:Label></td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    <asp:Label ID="lblCYOW" runat="server" Text=""></asp:Label>&nbsp;</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    <asp:Label ID="lblCPFAWCIL" runat="server" Text=""></asp:Label>&nbsp;</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    <asp:Label ID="lblACTCIL" runat="server" Text=""></asp:Label>&nbsp;</td>
                                <td class="colnamebold" style="font-size: 13px; border-style: double; border-right-width: 0px;
                                    color: Maroon" align="right">
                                    <asp:Label ID="lblAWB4CM" runat="server" Text=""></asp:Label></td>
                                <td colspan="2" style="font-size: 13px; border-style: double; color: Maroon">
                                    <asp:Label ID="lblRefund" Font-Bold="true" ForeColor="red" Font-Size="Medium" runat="server"
                                        Text=""></asp:Label>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid3" runat="server" GridLines="None"
                            Skin="Outlook" Width="93%">
                            <MasterTableView AutoGenerateColumns="False" >
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <Columns>
                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" UniqueName="Type">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeavesAllowed" DataType="System.Int32" HeaderText="Leaves Allowed"
                                        SortExpression="LeavesAllowed" UniqueName="LeavesAllowed">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeavesEarned" DataType="System.Int32" HeaderText="Leaves Earned"
                                        SortExpression="LeavesEarned" UniqueName="LeavesEarned">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="PaidLeaves" DataType="System.DateTime" HeaderText="Paid Leaves"
                                        SortExpression="PaidLeaves" UniqueName="PaidLeaves">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="UnpaidLeaves" DataType="System.DateTime" HeaderText="Unpaid Leaves"
                                        SortExpression="UnpaidLeaves" UniqueName="UnpaidLeaves">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="TotalLeavesTaken" DataType="System.DateTime" HeaderText="Total Leaves Taken"
                                        SortExpression="TotalLeavesTaken" UniqueName="TotalLeavesTaken">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="LeavesAvailable" DataType="System.Int32" HeaderText="Balance Leaves"
                                        SortExpression="LeavesAvailable" UniqueName="LeavesAvailable">
                                        <ItemStyle Font-Bold="true" />
                                        <HeaderStyle Font-Bold="true" />
                                    </radG:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                        </radG:RadGrid>
                    </td>
                </tr>
            </table>
            <table id="tbl2" runat="server"  width="99%;height:100%"
                border="0"><tr><td>Cannot Render Employee Profile and Salary Data, due to missing PR Date.</td></tr></table>
        </center>
    </form>
</body>
</html>
