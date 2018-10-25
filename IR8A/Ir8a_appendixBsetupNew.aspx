<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Ir8a_appendixBsetupNew.aspx.cs"
    Inherits="SMEPayroll.IR8A.Ir8a_appendixBsetupNew" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IR8A AppendixB Setup</title>
    
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading"><b>IR8A Appendix B Setup</b></font>
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
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 30%">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="color: #000000; height: 28px; text-align: center">
                    <asp:Label ID="lblerr" runat="server" Width="297px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Employee:&nbsp;</tt></td>
                <td style="height: 31px; text-align: left;">
                    <%--  <asp:UpdatePanel ID="aspUpdate" runat="server">
                        <ContentTemplate>--%>
                    <asp:DropDownList ID="drpemployee" OnSelectedIndexChanged="drpemployee_slectIndexChanged"
                        AutoPostBack="true" OnDataBound="drpemployee_databound" runat="server">
                    </asp:DropDownList>
                    <%--    </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="drpemployee"
                        Display="None" ErrorMessage="Employee Required!"  InitialValue="-select-"></asp:RequiredFieldValidator>
                </td>
                <td class="bodytxt" align="center">
                    Year :
                </td>
                <td style="height: 31px; text-align: left;">
                    <select id="rdYear" runat="server" class="textfields" style="width: 72px">
                        <option selected="selected" value="-1">-select-</option>
                        <option value="2015">2015</option>
                        <option value="2014">2014</option>
                        <option value="2013">2013</option>
                        <option value="2012">2012</option>
                        <option value="2011">2011</option>
                        <option value="2010">2010</option>
                        <option value="2009">2009</option>
                        <option value="2008">2008</option>
                        <option value="2007">2007</option>
                        <option value="2006">2006</option>
                    </select>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rdYear"
                        Display="None" ErrorMessage="Year Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Name Of The Company (a) :</tt></td>
                <td style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtCompany" Enabled="true" runat="server"></asp:TextBox>
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">Company Registration No (b) :</tt>
                </td>
                <td style="height: 31px; text-align: left">
                    <asp:TextBox ID="txtComRoc" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Type Of Plan Granted (c1) :</tt></td>
                <td style="height: 31px; text-align: left;">
                    <select id="cmbPlan" runat="server" class="textfields" style="width: 72px">
                        <option selected="selected" value="-1">-select-</option>
                        <option value="ESOP">ESOP</option>
                        <option value="ESOP">ESOW</option>
                    </select>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbPlan"
                        Display="None" ErrorMessage="Plan Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">Date Of Grant (c2):</tt>
                </td>
                <td style="height: 31px; text-align: left">
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdGrant"
                        runat="server">
                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                    </radCln:RadDatePicker>
                </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="rdGrant"
                    Display="None" ErrorMessage="Date Of Grant Required!"></asp:RequiredFieldValidator>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td colspan="2" style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Date Of Excercise of ESOP /ESOW (d) :</tt>
                </td>
                <td>
                </td>
                <td colspan="2" style="height: 31px; text-align: left;">
                    <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdExcercise"
                        runat="server">
                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                    </radCln:RadDatePicker>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rdExcercise"
                        Display="None" ErrorMessage="Date Of Excercise Required!"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 5%">
                </td>
                <td colspan="3" style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Excercise Price Of ESOP / Or Price Paid / Or Payable per Share
                        under ESOW Plan($) (e):</tt>
                </td>
                <td colspan="2" style="height: 31px; text-align: left">
                    <asp:TextBox ID="txtExPrice" Enabled="true" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtExPrice"
                        Display="None" ErrorMessage="Excercise Price Required"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cmp1" runat="server" ControlToValidate="txtExPrice" Operator="DataTypeCheck"
                        Type="Double" Display=None  ErrorMessage="Invalid Excercise Price"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td colspan="3" style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Open Market Value Per share as at Date Of Grant ($) (f) :</tt></td>
                <td colspan="2" style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtOpenPrice" Enabled="true" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtOpenPrice"
                        Display="None" ErrorMessage="Open Market Value Required"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtOpenPrice" Operator="DataTypeCheck"
                        Type="Double" Display=None  ErrorMessage="Invalid Open Market Value"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td colspan="3" style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Open Market Value Per share as at Date Of Reflected ($) (g):</tt>
                </td>
                <td style="height: 31px; text-align: left">
                    <asp:TextBox ID="txtRefPrice" Enabled="true" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRefPrice"
                        Display="None" ErrorMessage="pen Market Value as At date Of Reflected Required"></asp:RequiredFieldValidator>
                    <asp:CompareValidator Display=None  ID="CompareValidator2" runat="server" ControlToValidate="txtRefPrice" Operator="DataTypeCheck"
                        Type="Double" ErrorMessage="Invalid Open Market Value as At date Of Reflected"></asp:CompareValidator>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 5%">
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">*Number Of Shares Acquired (h) :</tt></td>
                <td style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtNoShares" Enabled="true" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtNoShares" Operator="DataTypeCheck"
                        Type="Double" Display="None"  ErrorMessage="Invalid No Of Shares"></asp:CompareValidator>
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">&nbsp;* Income Tax Exemption :</tt>
                </td>
                <td style="height: 31px; text-align: left;">
                    <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>
                    <asp:DropDownList ID="exemType" OnSelectedIndexChanged="exemType_slectIndexChanged"
                        AutoPostBack="true" runat="server">
                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                        <asp:ListItem Value="ERISSmes">ERIS(SMES)(i) </asp:ListItem>
                        <asp:ListItem Value="ERISCorp">ERIS(All Corporation)(j) </asp:ListItem>
                        <asp:ListItem Value="ERISStartups">ERIS(Startups)(k)</asp:ListItem>
                    </asp:DropDownList>
                    <%--       </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="exemType"
                        Display="None" ErrorMessage="Income Tax Exemption Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td style="height: 31px; text-align: left">
                    <tt class="bodytxt">
                        <asp:Label ID="lblSchemeType" Text="Scheme Type" runat="server"></asp:Label>
                    </tt>
                </td>
                <td style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtGrossAmount" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td colspan="2" style="height: 31px; text-align: left">
                    <tt class="bodytxt"></tt>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td colspan="1" style="height: 31px; text-align: left">
                    <tt class="bodytxt">Select Section :</tt>
                </td>
                <td colspan="3" style="height: 31px; text-align: left;">
                    <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                    <asp:DropDownList ID="empSection" OnSelectedIndexChanged="empSection_slectIndexChanged"
                        AutoPostBack="true" runat="server">
                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Section A:Employee Equity-Based Remuneration (EEBR) SCHEME</asp:ListItem>
                        <asp:ListItem Value="2">Section B:Equity Remuneration INCENTIVE SCHEME (ERIS) SMEs</asp:ListItem>
                        <asp:ListItem Value="3">Section C:Equity Remuneration INCENTIVE SCHEME (ERIS) All Corporations</asp:ListItem>
                        <asp:ListItem Value="4">Section D:Equity Remuneration INCENTIVE SCHEME (ERIS) Start-UPS</asp:ListItem>
                    </asp:DropDownList>
                    <%--         </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="empSection"
                        Display="None" ErrorMessage="Section Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%">
                </td>
                <td colspan="3" style="height: 31px; text-align: left">
                    <tt class="bodytxt">****Gross Amount not Qualifying for Tax Exemption ($)(l): </tt>
                    <tt class="bodytxt">
                        <asp:Label ID="lblTaxExemptionFormula" Enabled="false" runat="server"></asp:Label>
                    </tt>
                </td>
                <td style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtNoTaxAmt" Enabled="false" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 5%">
                </td>
                <td colspan="3" style="height: 31px; text-align: left">
                    <tt class="bodytxt">Gross Amount of gains from ESOP / ESOW Plans($)(m): </tt><tt
                        class="bodytxt">
                        <asp:Label ID="lblTaxGainFormula" Enabled="false" runat="server"></asp:Label>
                    </tt>
                </td>
                <td style="height: 31px; text-align: left;">
                    <asp:TextBox ID="txtGainAmt" Enabled="false" runat="server"></asp:TextBox>
                </td>
                <td colspan="1" style="height: 31px; text-align: left">
                    <tt class="bodytxt"></tt>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 31px; text-align: Center">
                    <tt class="bodytxt">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    </tt>
                </td>
            </tr>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                    ShowMessageBox="True" ShowSummary="False" />
        </table>
    </form>
</body>
</html>
