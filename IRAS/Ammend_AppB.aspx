<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ammend_AppB.aspx.cs" Inherits="IRAS.Ammend_AppB"  EnableEventValidation="true"%>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik"%>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radClnNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">






    <title>Ammendment Appendix B</title>
     
    
    
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />
  
  
    
    <style type="text/css">
        .bigModule
        {
            width: 750px;
            background: url(qsfModuleTop.jpg) no-repeat;
            margin-bottom: 15px;
        }
        .bigModuleBottom
        {
            background: url(qsfModuleBottom.gif) no-repeat bottom;
            color: #252f34;
            padding: 23px 17px;
            line-height: 16px;
            font-size: 12px;
        }
        .trstandtop
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:bottom;
            valign:bottom;
        }
        .tdstand
        {
            height:30px;
            vertical-align:text-bottom;
            vertical-align:bottom;
            border-bottom-width:1px;
            border-bottom-color:Silver;
            border-bottom-style:inset;
	        font-family: Tahoma;
	        font-size: 12px;
	        font-weight:bold;
        }
        .tbl
        {
            cellpadding:0;
            cellspacing:0;
            border:0;
            background-color: White; 
            width: 100%; 
            height: 100%; 
            background-image: url(Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            background-repeat: no-repeat;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color:#F0F1EB;
            padding:4px;
            padding-left:0;
            width:85%;
            height:550px%;
            margin-left:-1px;                
        }
        
        .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">

        <script type="text/jvascript" language="javascript">  
        var test;
        var giro;
        
        
          function isNumericKeyStrokeDecimalPercent(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
             {
                return false;
             }
             return true;
        }
        
	   
        </script>

        <script language="JavaScript1.2" type="text/javascript"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
        </script>

    </telerik:RadCodeBlock>
</head>
<body style="margin-left: auto">
    <form id="ammendment_appB" runat="server" method="post">
   <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
      <%-- <radTS:RadScriptManager ID="ScriptManager" runat="server" >
        
        </radTS:RadScriptManager>--%>
        <%--        <uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="4" style="height: 29px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR8A Setup</b></font>
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
                         
                               <%-- <asp:Button ID="ButtonCALCULATE" runat="server" Text="Calculate" OnClick="ButtonCALCULATE_Click"  style="width: 80px; height: 22px"/>--%>
                                <input id="btnsave" type="button" runat="server" style="width: 80px; height: 22px"
                                    value="Save"  />
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
      <%--  <telerik:RadCodeBlock ID="RadCodeBlock4" runat="server">

            <script type="text/javascript">
             
                     
            </script>

        </telerik:RadCodeBlock>--%>
        <input type="hidden" id="oHidden" name="oHidden" runat="server" />
        <input type="hidden" id="Hidden1" name="Hidden1" runat="server" />
      <%--  <div class="exampleWrapper">--%>
            <%--<telerik:RadTabStrip ID="tbsEmp" runat="server" SelectedIndex="0" MultiPageID="tbsEmp12"
                Skin="Outlook" Style="float:left">
                <Tabs>
                  
                     <telerik:RadTab  runat="server" AccessKey="F" Text="APPENDIX B"
                        PageViewID="APPENDIX_B">
                   </telerik:RadTab>
                </Tabs>
            </telerik:RadTabStrip>--%>
            <!--
            no spaces between the tabstrip and multipage, in order to remove unnecessary whitespace
            -->
            <%--<telerik:RadMultiPage  SelectedIndex="0" runat="server" ID="tbsEmp12" Width="99%" Height="100%" CssClass="multiPage">
              
                   
                <telerik:RadPageView ID="APPENDIX_B" runat="server" Height="100%" Width="100%" BackColor="White">
               
                 <telerik:RadAjaxPanel ID="RadAjaxPanel5" runat="server" Height="100%" Width="100%" BorderColor="White" BackColor="White">--%>
                 
                <table border="1" cellspacing="0"  width="100%" align="center">
    <tbody>
        <tr>
            <td width="175" valign="top">
                <p align="left">
                    <strong>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; </strong>
                </p>
            </td>
            <td width="721" valign="top">
                <p align="center">
                    <strong>Appendix 8B</strong>
                </p>
            </td>
            <td valign="top" style="width: 176px">
            </td>
        </tr>
        <tr>
            <td width="1072" colspan="3">
                <p align="left">
                    <strong></strong>
                </p>
                <p align="left">
                    <span style="font-size: 8pt">
                    <strong>&nbsp; &nbsp; &nbsp;&nbsp;
                        DETAILS OF GAINS OR PROFITS FROM EMPLOYEE STOCK OPTION (ESOP) / OTHER FORMS OF EMPLOYEE SHARE OWNERSHIP (ESOW) PLANS FOR THE YEAR ENDED&nbsp;</strong>
                    </span>
                </p>
            </td>
        </tr>
        <tr>
            <td width="1072" colspan="3" valign="top">
                <p align="center">
Fill in this form and give to your employee / submit to IRAS (if required – <strong>see paragraph 2 of the explanatory notes</strong>).</p>
                <p align="center">
                    Please read the explanatory notes when completing this
                    form.
                </p>
            </td>
        </tr>
    </tbody>
</table>
<table border="1" cellspacing="0" cellpadding="2" width="100%" align="center" >
    <tbody>
        <tr>
            <td colspan="18" valign="top" style="height: 19px">
                <p align="left">
                    <strong> &nbsp;&nbsp; &nbsp; &nbsp; Tax Ref. </strong>
                    <strong>(NRIC/FIN):
                        <asp:Label ID="B_Nric_label" runat="server" Text="Nric"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; Full Name of Employee as per NRIC / IN:<asp:Label ID="B_Name_Label" runat="server"
                            Text="Name" Width="234px"></asp:Label></strong></p>
            </td>
        </tr>
        <tr>
            <td rowspan="3" valign="top" style="width: 64px">
                <p align="left">
                    Company Registration Number /UEN
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 108px">
                <p align="left">
                    NameofCompany
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 56px">
                <p align="left">
                    <u>Indicate Type of Plan Granted:</u></p>
                <p align="left">
                    <u></u>
                    1) ESOP or
                    2) ESOW
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 83px">
                <p align="left">
                    Date of grant
                </p>
            </td>
            <td colspan="2" rowspan="3" valign="top" style="width: 146px">
                <p align="left">
                    Date of exercise of ESOP or date of vesting of ESOW Plan (if applicable). If moratorium (i.e. selling restriction) is imposed, state the
                    date the moratorium is lifted for the ESOP/ESOW Plans
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 59px">
                <p align="left">
                    ExercisePrice ofESOP / orPrice Paid/ Payable per Share under ESOW Plan ($)
                </p>
            </td>
            <td width="72" rowspan="3" valign="top">
                <p align="left">
                    Open Market Value Per share as at the Date of Grant of
                </p>
                <p align="left">
                    ESOP/ ESOW Plan ($)
                </p>
            </td>
            <td rowspan="3" valign="top" style="width: 72px">
                <p align="left">
                    Open Market Value Per Share as at the Date Reflected at Column (d) of this form ($)
                </p>
            </td>
            <td width="56" rowspan="3" valign="top">
                <p align="left">
                    Numberof Shares
                    Acquired
                </p>
            </td>
            <td colspan="8" valign="top">
                <p align="center">
                    Gains from ESOP / ESOW Plans
                </p>
            </td>
        </tr>
        <tr>
            <td width="216" colspan="5" valign="top">
                <p align="left">
                    Gross Amount Qualifying for Income Tax Exemption under: -
                </p>
            </td>
            <td rowspan="2" valign="top" style="width: 74px">
                <p align="left">
                    ****Gross Amount not Qualifying
                </p>
                <p align="left">
                    for Tax Exemption
                </p>
                <p align="left">
                    ($)
                </p>
            </td>
            <td rowspan="2" valign="top" colspan="2">
                <p align="left">
                    Gross Amount
                </p>
                <p align="left">
                    of gains from ESOP /
                </p>
                <p align="left">
                    ESOW Plans ($)
                </p>
            </td>
        </tr>
        <tr>
            <td width="72" valign="top" style="height: 196px">
                <p align="left">
                    *ERIS
                </p>
                <p align="left">
                    (SMEs)
                </p>
            </td>
            <td width="72" colspan="3" valign="top" style="height: 196px">
                <p align="left">
                    **ERIS
                </p>
                <p align="left">
                    (All Corporations)
                </p>
            </td>
            <td valign="top" style="width: 75px; height: 196px">
                <p align="left">
                    ***ERIS
                </p>
                <p align="left">
                    (Start-ups)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <p align="center">
                    (a)
                </p>
            </td>
            <td valign="top" style="width: 108px">
                <p align="center">
                    (b)
                </p>
            </td>
            <td valign="top" style="width: 56px">
                <p align="center">
                    (c1)
                </p>
            </td>
            <td valign="top" style="width: 83px">
                <p align="center">
                    (c2)
                </p>
            </td>
            <td colspan="2" valign="top" style="width: 146px">
                <p align="center">
                    (d)
                </p>
            </td>
            <td valign="top" style="width: 59px">
                <p align="center">
                    (e)
                </p>
            </td>
            <td width="72" valign="top">
                <p align="center">
                    (f)
                </p>
            </td>
            <td valign="top" style="width: 72px">
                <p align="center">
                    (g)
                </p>
            </td>
            <td width="56" valign="top">
                <p align="center">
                    (h)
                </p>
            </td>
            <td width="72" valign="top">
                <p align="center">
                    (i)
                </p>
            </td>
            <td width="72" colspan="3" valign="top">
                <p align="center">
                    (j)
                </p>
            </td>
            <td valign="top" style="width: 75px">
                <p align="center">
                    (k)
                </p>
            </td>
            <td valign="top" style="width: 74px">
                <p align="center">
                    (l)
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="center">
                    (m)
                </p>
            </td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p align="left">
                    <strong>&nbsp;SECTION A: EMPLOYEE EQUITY-BASED REMUNERATION (EEBR) SCHEME </strong>
                </p>
            </td>
            <td valign="top" colspan="6">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (g-e) x h
                </p>
            </td>
            <td colspan="2">
                <p align="left">
                    (m) = (l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 25px;">
                <asp:TextBox ID="sa_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 108px;">
                <asp:TextBox ID="sa_b1" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 25px; width: 56px;">
                <asp:DropDownList ID="sa_ca1" runat="server">
                    <asp:ListItem>ESOP</asp:ListItem>
                    <asp:ListItem>ESOW</asp:ListItem>
                </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 25px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb1"
                                                                                runat="server" Width="82px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 25px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d1"
                                                                                runat="server" Width="80px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 25px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sa_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" rowspan="3" valign="top">
                </td>
            <td valign="top" style="width: 72px; height: 25px;"><radG:RadNumericTextBox runat="server" ID="sa_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 25px"><radG:RadNumericTextBox runat="server" ID="sa_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="5" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 25px">
                <asp:Label ID="sa_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 25px">
                <asp:Label ID="sa_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sa_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sa_b2" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sa_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sa_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="sa_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sa_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 45px;">
                <asp:TextBox ID="sa_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 45px;">
                <asp:TextBox ID="sa_b3" runat="server" TextMode="MultiLine" Width="103px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 45px"><asp:DropDownList ID="sa_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 45px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_cb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 45px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sa_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="sa_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 45px;"><radG:RadNumericTextBox runat="server" ID="sa_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 45px"><radG:RadNumericTextBox runat="server" ID="sa_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 74px; height: 45px;">
                <asp:Label ID="sa_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 45px">
                <asp:Label ID="sa_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="10" valign="top" style="height: 19px">
                <p>
                    <strong>&nbsp;(I) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION A</strong>
                </p>
            </td>
            <td valign="top" style="height: 19px" colspan="5">
            </td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sa_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sa_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p align="left">
                    <strong>&nbsp;SECTION B: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) SMEs </strong>
                </p>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
                <p align="left">
                    (i) = (g-f) x h
                </p>
            </td>
            <td colspan="4" valign="top">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="left">
                    (m) = (i) +(l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 20px;">
                <asp:TextBox ID="sb_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 20px;">
                <asp:TextBox ID="sb_b1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 20px"><asp:DropDownList ID="sb_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 20px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 20px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d1"
                                                                                runat="server" Width="95px" Height="16px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="sb_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="sb_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 20px;"><radG:RadNumericTextBox runat="server" ID="sb_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 20px"><radG:RadNumericTextBox runat="server" ID="sb_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 20px">
                <asp:Label ID="sb_i1" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top" rowspan="3">
            </td>
            <td valign="top" style="width: 74px; height: 20px;">
                <asp:Label ID="sb_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 20px">
                <asp:Label ID="sb_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 21px;">
                <asp:TextBox ID="sb_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 21px;">
                <asp:TextBox ID="sb_b2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 21px;"><asp:DropDownList ID="sb_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 21px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 21px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d2"
                                                                                runat="server" Width="98px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="sb_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="sb_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 21px;"><radG:RadNumericTextBox runat="server" ID="sb_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 21px"><radG:RadNumericTextBox runat="server" ID="sb_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"   >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 21px">
                <asp:Label ID="sb_i2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 21px;">
                <asp:Label ID="sb_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 21px">
                <asp:Label ID="sb_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 1px;">
                <asp:TextBox ID="sb_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 1px;">
                <asp:TextBox ID="sb_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 1px"><asp:DropDownList ID="sb_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 1px; width: 146px;">
                &nbsp;<radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_cb3"
                                                                                runat="server" Width="98px" >
                    <DateInput Skin="" DateFormat="dd/MM/yyyy">
                    </DateInput>
                    <Calendar ShowRowHeaders="False">
                    </Calendar>
                </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 1px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sb_d3"
                                                                                runat="server" Width="98px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="sb_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="sb_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 1px;"><radG:RadNumericTextBox runat="server" ID="sb_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 1px"><radG:RadNumericTextBox runat="server" ID="sb_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 1px">
                <asp:Label ID="sb_i3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 1px;">
                <asp:Label ID="sb_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 1px">
                <asp:Label ID="sb_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="10" valign="top">
                <p>
                    <strong>&nbsp;(II) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION B</strong>
                </p>
            </td>
            <td width="72" valign="top">
                <asp:Label ID="sb_ti" runat="server" Text="0.00"></asp:Label></td>
            <td colspan="4" valign="top">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sb_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sb_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top">
                <p>
                    <strong>&nbsp;SECTION C: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) ALL &nbsp; CORPORATIONS</strong>
                </p>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <p align="left">
                    (j) = (g-f) x h
                </p>
            </td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2">
                <p align="left">
                    (m) = (j) +(l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sc_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b1" runat="server" TextMode="MultiLine" Width="89px" ></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sc_a2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sc_b2" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sc_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb2"
                                                                                runat="server" Width="84px" >
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sc_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sc_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sc_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_j2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 44px;">
                <asp:TextBox ID="sc_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px; height: 44px;">
                <asp:TextBox ID="sc_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px; height: 44px;"><asp:DropDownList ID="sc_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px; height: 44px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_cb3"
                                                                                runat="server" Width="89px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px; height: 44px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sc_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 44px;"><radG:RadNumericTextBox runat="server" ID="sc_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 44px"><radG:RadNumericTextBox runat="server" ID="sc_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 44px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 44px">
                <asp:Label ID="sc_j3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px; height: 44px;">
            </td>
            <td valign="top" style="width: 74px; height: 44px;">
                <asp:Label ID="sc_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 44px">
                <asp:Label ID="sc_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9">
                <h1 align="left">
                    <span style="font-size: 10pt">&nbsp;(III) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION C </span>
                </h1>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
                <asp:Label ID="sc_tj" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sc_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sc_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9">
                <h1 align="left">
                </h1>
            </td>
            <td width="56" valign="top">
            </td>
            <td width="72" valign="top">
            </td>
            <td width="72" colspan="3" valign="top">
            </td>
            <td valign="top" style="width: 75px">
            </td>
            <td valign="top" style="width: 74px">
            </td>
            <td valign="top" colspan="2">
            </td>
        </tr>
        <tr>
            <td width="624" colspan="9" valign="top" style="height: 44px">
                <p align="left">
                    <strong>&nbsp;SECTION D: EQUITY REMUNERATION INCENTIVE SCHEME (ERIS) START-UPs</strong>
                </p>
            </td>
            <td width="56" valign="top" style="height: 44px">
            </td>
            <td width="72" valign="top" style="height: 44px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 44px">
            </td>
            <td valign="top" style="width: 75px; height: 44px;">
                <p align="left">
                    (k)=(g-f) x h
                </p>
            </td>
            <td valign="top" style="width: 74px; height: 44px;">
                <p>
                    (l) = (f-e) x h
                </p>
            </td>
            <td valign="top" colspan="2" style="height: 44px">
                <p align="left">
                    (m)=(k) + (l)
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sd_a1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b1" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca1" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d1"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f1" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h1" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" colspan="4" rowspan="3">
            </td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l1" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m1" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px">
                <asp:TextBox ID="sd_a2" runat="server"  TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 108px">
                <asp:TextBox ID="sd_b2" runat="server" TextMode="MultiLine"
                    Width="89px"></asp:TextBox></td>
            <td valign="top" style="width: 56px"><asp:DropDownList ID="sd_ca2" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="width: 146px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 119px"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d2"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="width: 59px"><radG:RadNumericTextBox runat="server" ID="sd_e2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_f2" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px"><radG:RadNumericTextBox runat="server" ID="sd_g2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top"><radG:RadNumericTextBox runat="server" ID="sd_h2" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 75px">
                <asp:Label ID="sd_k2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px">
                <asp:Label ID="sd_l2" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2">
                <asp:Label ID="sd_m2" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top" style="width: 64px; height: 19px">
                <asp:TextBox ID="sd_a3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 108px;">
                <asp:TextBox ID="sd_b3" runat="server" TextMode="MultiLine" Width="89px"></asp:TextBox></td>
            <td valign="top" style="height: 19px; width: 56px;"><asp:DropDownList ID="sd_ca3" runat="server">
                <asp:ListItem>ESOP</asp:ListItem>
                <asp:ListItem>ESOW</asp:ListItem>
            </asp:DropDownList></td>
            <td colspan="2" valign="top" style="height: 19px; width: 146px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_cb3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 119px;"><radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="sd_d3"
                                                                                runat="server" Width="84px">
                <DateInput Skin="" DateFormat="dd/MM/yyyy">
                </DateInput>
                <Calendar ShowRowHeaders="False">
                </Calendar>
            </radClnNew:RadDatePicker>
            </td>
            <td valign="top" style="height: 19px; width: 59px;"><radG:RadNumericTextBox runat="server" ID="sd_e3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="72" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_f3" Width="70px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="width: 72px; height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_g3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="2" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td width="56" valign="top" style="height: 19px"><radG:RadNumericTextBox runat="server" ID="sd_h3" Width="54px" 
                        Skin="Vista" AllowOutOfRangeAutoCorrect="true" MinValue="0" Value="0"
                                               
                        Type="Number"  >
                <ClientEvents  />
                <NumberFormat AllowRounding="False" DecimalDigits="0" KeepNotRoundedValue="True"
                            KeepTrailingZerosOnFocus="True" />
            </radG:RadNumericTextBox></td>
            <td valign="top" style="height: 19px; width: 75px;">
                <asp:Label ID="sd_k3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px; width: 74px;">
                <asp:Label ID="sd_l3" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="height: 19px" colspan="2">
                <asp:Label ID="sd_m3" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="624" colspan="9" style="height: 4px">
                <h1 align="left">
                    <span style="font-size: 10pt">&nbsp;(IV) TOTAL OF GROSS ESOP/ESOW GAINS IN SECTION D </span>
                </h1>
            </td>
            <td width="56" valign="top" style="height: 4px">
            </td>
            <td width="72" valign="top" style="height: 4px">
            </td>
            <td width="72" colspan="3" valign="top" style="height: 4px">
            </td>
            <td valign="top" style="width: 75px; height: 4px;">
                <asp:Label ID="sd_tk" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" style="width: 74px; height: 4px;">
                <asp:Label ID="sd_tl" runat="server" Text="0.00"></asp:Label></td>
            <td valign="top" colspan="2" style="height: 4px">
                <asp:Label ID="sd_tm" runat="server" Text="0.00"></asp:Label></td>
        </tr>
        <tr>
            <td width="976" colspan="16">
                <p>
                    <span style="font-size: 10pt">
                    <strong>&nbsp;SECTION E : TOTAL GROSS AMOUNT OF ESOP/ESOW GAINS (I+II+III+IV) (THIS AMOUNT IS TO BE REFLECTED IN ITEM d8 OF FORM IR8A)</strong>
                    <strong></strong></span>
                </p>
            </td>
            <td colspan="2">
                <asp:Label ID="Total" runat="server" Text="0.00"></asp:Label></td>
        </tr>
    </tbody>
</table>
     
                <%--</telerik:RadAjaxPanel>--%>
                    <asp:Label ID="Label12" runat="server" Text="Date Of Incorporation[For ERIS(Start-ups only]"></asp:Label>
                    <radClnNew:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="DateOfIncorporation"
                                                                                runat="server" Width="84px">
                        <DateInput Skin="" DateFormat="dd/MM/yyyy">
                        </DateInput>
                        <Calendar ShowRowHeaders="False">
                        </Calendar>
                    </radClnNew:RadDatePicker>
                <%--</telerik:RadPageView>
                
            </telerik:RadMultiPage>--%>
           
                      
   <telerik:RadAjaxManager ID="ajaxManager" runat="server">
   </telerik:RadAjaxManager>
    
    
     
    </form>
    
  
</body>
</html>
