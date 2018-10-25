<%@ Control Language="C#" AutoEventWireup="true" Codebehind="Ir8AControl.ascx.cs"
    Inherits="SMEPayroll.IR8A.Ir8AControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
        <td style="width: 10%">
        </td>
        <td style="width: 60%">
        </td>
    </tr>
    <tr>
        <td colspan="5" style="color: #000000; height: 28px; background-color: #e9eed4; text-align: center">
            <asp:Label ID="lblerr" runat="server" Width="297px"></asp:Label></td>
    </tr>
    <tr>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Employee:&nbsp;</tt></td>
        <td style="height: 31px; text-align: left;">
           <%-- <asp:UpdatePanel ID="aspUpdate" runat="server">
                <ContentTemplate>--%>
                    <asp:DropDownList ID="drpemployee" OnSelectedIndexChanged="drpemployee_slectIndexChanged"
                        AutoPostBack="true" OnDataBound="drpemployee_databound" runat="server">
                    </asp:DropDownList>
        <%--        </ContentTemplate>
            </asp:UpdatePanel>--%>
        </td>
        <td align="center" >
        Year :
        </td>
        <td style="height: 31px; text-align: left;">
            <select id="rdYear" runat="server" class="textfields" style="width: 72px">
                <option selected="selected" value="s">-select-</option>
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
      
    </tr>
    <tr>
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
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Type Of Plan Granted (c1) :</tt></td>
        <td style="height: 31px; text-align: left;">
            <select id="cmbPlan" runat="server" class="textfields" style="width: 72px">
                <option selected="selected" value="s">-select-</option>
                <option value="ESOP">ESOP</option>
                <option value="ESOP">ESOW</option>
            </select>
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
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Date Of Excercise of ESOP /ESOW (d) :</tt></td>
        <td style="height: 31px; text-align: left;">
            <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdExcercise"
                runat="server">
                <DateInput Skin="" DateFormat="dd/MM/yyyy" />
            </radCln:RadDatePicker>
        </td>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Excercise Price Of ESOP / Or Price Paid / Or Payable per Share under
                ESOW Plan($) (e):</tt>
        </td>
        <td style="height: 31px; text-align: left">
            <asp:TextBox ID="txtExPrice" Enabled="true" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Open Market Value Per share as at Date Of Grant ($) (f) :</tt></td>
        <td style="height: 31px; text-align: left;">
            <asp:TextBox ID="txtOpenPrice" Enabled="true" runat="server"></asp:TextBox>
        </td>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Open Market Value Per share as at Date Of Reflected ($) (g):</tt>
        </td>
        <td style="height: 31px; text-align: left">
            <asp:TextBox ID="txtRefPrice" Enabled="true" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td style="height: 31px; text-align: left">
            <tt class="bodytxt">*Number Of Shares Acquired (h) :</tt></td>
        <td style="height: 31px; text-align: left;">
            <asp:TextBox ID="txtNoShares" Enabled="true" runat="server"></asp:TextBox>
        </td>
       <td style="height: 31px; text-align: left">
         <tt class="bodytxt">&nbsp;* Income Tax Exemption  :</tt> 
        </td>
        <td style="height: 31px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="exemType" OnSelectedIndexChanged="exemType_slectIndexChanged"
                        AutoPostBack="true"  runat="server">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="ERISSmes">ERIS(SMES)(i) </asp:ListItem>
                        <asp:ListItem Value="ERISCorp">ERIS(All Corporation)(j) </asp:ListItem>
                        <asp:ListItem Value="ERISStartups">ERIS(Startups)(k)</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
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
     <td colspan="1" style="height: 31px; text-align: left">
            <tt class="bodytxt">Select Section :</tt>
        </td>
     <td colspan=3 style="height: 31px; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="empSection" OnSelectedIndexChanged="empSection_slectIndexChanged"
                        AutoPostBack="true"  runat="server">
                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                        <asp:ListItem Value="1">Section A:Employee Equity-Based Remuneration (EEBR) SCHEME</asp:ListItem>
                        <asp:ListItem Value="2">Section B:Equity Remuneration INCENTIVE SCHEME (ERIS) SMEs</asp:ListItem>
                        <asp:ListItem Value="3">Section C:Equity Remuneration INCENTIVE SCHEME (ERIS) All Corporations</asp:ListItem>
                        <asp:ListItem Value="4">Section D:Equity Remuneration INCENTIVE SCHEME (ERIS) Start-UPS</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="3" style="height: 31px; text-align: left">
            <tt class="bodytxt">****Gross Amount not Qualifying for Tax Exemption ($)(l): </tt>
            <tt class="bodytxt"><asp:Label ID="lblTaxExemptionFormula"   Enabled="false" runat="server"></asp:Label>  </tt>
        </td>
        <td style="height: 31px; text-align: left;">
            <asp:TextBox ID="txtNoTaxAmt" Enabled="false" runat="server"></asp:TextBox>
        </td>
       
    </tr>
    <tr>
        <td colspan="3" style="height: 31px; text-align: left">
            <tt class="bodytxt">Gross Amount of gains from ESOP / ESOW Plans($)(m): </tt>
               <tt class="bodytxt"><asp:Label ID="lblTaxGainFormula" Enabled="false" runat="server"></asp:Label>  </tt>
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
    <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <radA:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </radA:AjaxSetting>
        </AjaxSettings>
    </radA:RadAjaxManager>
   
</table>
