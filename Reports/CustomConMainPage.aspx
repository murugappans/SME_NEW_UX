<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CustomConMainPage.aspx.cs"
    Inherits="SMEPayroll.Reports.CustomConMainPage" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>

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

    <script language="javascript" src="../Frames/Script/jquery-1.3.2.min.js"></script>

    
    <style type="text/css">
    INPUT {
    FONT-SIZE: 8pt;	
    FONT-FAMILY: Tahoma
          }
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
            
            COLOR: gray;
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
            background-image: url(../Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
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
        
       <%-- .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }--%>
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
</head>
<body style="margin-left: auto">
    <form id="employeeform" runat="server" method="post">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Custom Consolidate Report Writer</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="center">
                                <asp:Label ID="lblerror" Text="" ForeColor="red" runat="server"></asp:Label>
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="exampleWrapper">
            <telerik:RadTabStrip ID="tbsComp" runat="server" SelectedIndex="0" MultiPageID="tbsCompany"
                Style="float: left">
                <Tabs>
                    <radG:RadTab TabIndex="1" runat="server" AccessKey="E" Text="&lt;u&gt;E&lt;/u&gt;mployee"
                        PageViewID="tbsEmp" Selected="True">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="2" runat="server" AccessKey="P" Text="&lt;u&gt;P&lt;/u&gt;ayroll"
                        PageViewID="tbsPay">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="3" runat="server" AccessKey="A" Text="&lt;u&gt;A&lt;/u&gt;dditions"
                        PageViewID="tbsAdditions">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="4" runat="server" AccessKey="D" Text="&lt;u&gt;D&lt;/u&gt;eductions"
                        PageViewID="tbsDeductions">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="5" runat="server" AccessKey="C" Text="&lt;u&gt;C&lt;/u&gt;laims"
                        PageViewID="tbsClaims">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="6" runat="server" AccessKey="G" Text="&lt;u&gt;G&lt;/u&gt;rouping"
                        PageViewID="tbsGroups">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="7" runat="server" AccessKey="L" Text="&lt;u&gt;L&lt;/u&gt;eaves"
                        PageViewID="tbsLeaves">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="8" runat="server" AccessKey="T" Text="&lt;u&gt;T&lt;/u&gt;imesheet"
                        PageViewID="tbsTimesheet">
                    </radG:RadTab>
                    <radG:RadTab TabIndex="9" runat="server" AccessKey="T" Text="&lt;u&gt;E&lt;/u&gt;mail Tracking"
                        PageViewID="tbsEmail">
                    </radG:RadTab>
                </Tabs>
            </telerik:RadTabStrip>
            <telerik:RadMultiPage runat="server" ID="tbsCompany" SelectedIndex="0" Width="95%"
                Height="400px" CssClass="multiPage">
                <telerik:RadPageView class="tbl" runat="server" ID="tbsEmp" Height="400px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid1" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid2" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Option Name">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="RELATION" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="RELATION" Visible="false" SortExpression="RELATION"
                                                HeaderText="RELATION">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="generateRpt" Text="Generate Report" OnClick="GenerateRpt_Click" runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsPay" Height="400px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid3" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid4" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="ALIAS_NAME,RELATION">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="ALIAS_NAME" DataType="System.String"
                                                UniqueName="ALIAS_NAME" Visible="true" SortExpression="ALIAS_NAME" HeaderText="Option Name">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="RELATION" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="RELATION" Visible="false" SortExpression="RELATION"
                                                HeaderText="RELATION">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" style="width: 49%">
                                Select From Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rfvdtp1" ValidationGroup="ValidationSummary1" runat="server"
                                    ControlToValidate="dtp1" Display="None" ErrorMessage="Please Enter Start date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td class="bodytxt" style="width: 49%">
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="dtp2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                <asp:RequiredFieldValidator ID="rfvdtp2" ValidationGroup="ValidationSummary1" runat="server"
                                    ControlToValidate="dtp2" Display="None" ErrorMessage="Please Enter End date."
                                    SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="RadioButtonList1"
                                    runat="server" OnSelectedIndexChanged="rd_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Summary</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Detail</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button1" Text="Generate Payroll Report" OnClick="GeneratePayroll_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsAdditions" Height="400px"
                    Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid5" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid6" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="id,description">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="id" DataType="System.String"
                                                UniqueName="id" Visible="false" SortExpression="id" HeaderText="id">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="description" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="description" Visible="true" SortExpression="description"
                                                HeaderText="Description">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" style="width: 49%">
                                Select Start Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td class="bodytxt" style="width: 49%">
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="RadioButtonList2"
                                    runat="server">
                                    <asp:ListItem Value="1">Summary</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Detail</asp:ListItem>
                                    <asp:ListItem Value="3">Summary UnProcessed</asp:ListItem>
                                    <asp:ListItem Value="4">Detail UnProcessed</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="generateAddRpt" Text="Generate Additions Report" OnClick="GenerateAddtions_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsDeductions" Height="400px"
                    Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid9" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid10" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="id,description">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="id" DataType="System.String"
                                                UniqueName="id" Visible="false" SortExpression="id" HeaderText="id">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="description" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="description" Visible="true" SortExpression="description"
                                                HeaderText="Description">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" style="width: 49%">
                                Select Start Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker5" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td class="bodytxt" style="width: 49%">
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker6" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="RadioButtonList3" runat="server">
                                    <asp:ListItem Value="1">Summary</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Detail</asp:ListItem>
                                    <asp:ListItem Value="3">Summary UnProcessed</asp:ListItem>
                                    <asp:ListItem Value="4">Detail UnProcessed</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button4" Text="Generate Deductions Report" OnClick="GenerateDeductions_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsClaims" Height="400px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid11" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid12" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="id,description">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="id" DataType="System.String"
                                                UniqueName="id" Visible="false" SortExpression="id" HeaderText="id">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="description" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="description" Visible="true" SortExpression="description"
                                                HeaderText="Description">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" style="width: 49%">
                                Select Start Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker7" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td class="bodytxt" style="width: 49%">
                                Select End Date:
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker8" runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt" colspan="3">
                                <asp:RadioButtonList RepeatDirection="Horizontal" ID="RadioButtonList4" runat="server">
                                    <asp:ListItem Value="1">Summary</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Detail</asp:ListItem>
                                    <asp:ListItem Value="3">Summary UnProcessed</asp:ListItem>
                                    <asp:ListItem Value="4">Detail UnProcessed</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button5" Text="Generate Claims Report" OnClick="GenerateClaims_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsGroups" Height="400px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr bgcolor="<%=sOddRowColor%>">
                        </tr>
                        <tr>
                            <td valign="top" class="bodytxt" style="width: 49%">
                                Select A Group :<asp:DropDownList ID="ddlCategory" AutoPostBack="TRUE" OnSelectedIndexChanged="ddlCategory_selectedIndexChanged"
                                    runat="server" Width="30%">
                                </asp:DropDownList>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top" class="bodytxt" style="width: 49%;">
                                <radG:RadGrid ID="RadGrid16" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%" valign="top">
                                <radG:RadGrid ID="RadGrid8" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView Visible="false" AllowAutomaticUpdates="True" AutoGenerateColumns="False"
                                        DataKeyNames="OptionId,Category">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="true" ReadOnly="True" DataField="OptionId" DataType="System.String"
                                                UniqueName="OptionId" Visible="false" SortExpression="OptionId" HeaderText="Option">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Category" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="Category" Visible="true" SortExpression="Category"
                                                HeaderText="Category">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="Button3" Text="Generate Grouping Report" OnClick="GenerateGrouping_Click"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsLeaves" Height="450px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid7" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid13" runat="server" DataSourceID="SqlDataSource3" GridLines="None"
                                    Skin="Outlook" Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource3" AutoGenerateColumns="False"
                                        DataKeyNames="id">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                UniqueName="ID" SortExpression="ID" HeaderText="ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="[Type]" AutoPostBackOnFilter="True" UniqueName="[Type]"
                                                SortExpression="[Type]" HeaderText="Leave Type">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <table border="0" cellpadding="2" cellspacing="2" style="width: 30%">
                                    <tr>
                                        <td width="20%">
                                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdRepOption"
                                                runat="server" OnSelectedIndexChanged="rdRepOption_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="True">Summary</asp:ListItem>
                                                <asp:ListItem Value="2">Detail</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="bodytxt" width="5%">
                                            Year:</td>
                                        <td width="20%">
                                            <asp:DropDownList ID="drpYear" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                class="textfields" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="bodytxt" style="text-align: right" width="5%">
                                            <asp:Label ID="lblStart" runat="server" Text="Start:"></asp:Label></td>
                                        <td width="50%" style="text-align: left">
                                            <asp:DropDownList ID="drpMonthStart" DataTextField="text" DataValueField="id" DataSourceID="xmldtMonth"
                                                class="textfields" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="bodytxt" width="5%" style="text-align: right">
                                            <asp:Label ID="lblEnd" runat="server" Text="End:" Visible="false"></asp:Label></td>
                                        <td width="50%" style="text-align: left">
                                            <asp:DropDownList Visible="false" ID="drpMonthEnd" DataTextField="text" DataValueField="id"
                                                DataSourceID="xmldtMonth" class="textfields" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" class="trstandtop" colspan="3">
                                            <asp:Button ID="btnGenLeaveRep" Text="Generate Leave Report" OnClick="btnGenLeaveRep_Click"
                                                runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsTimesheet" Height="750px"
                    Width="100%">
                    <table id="table1" cellpadding="0" cellspacing="0" border="0" width="95%">
                        <tr>
                            <td>
                                <radG:RadGrid ID="RadGrid17" runat="server" GridLines="None" Visible="false" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <table id="table2" cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td class="bodytxt" valign="middle" align="LEFT">
                                            <tt class="bodytxt"></tt>
                                        </td>
                                        <td class="bodytxt">
                                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdRepOptionTime" runat="server">
                                                <asp:ListItem Value="99" Selected="True">Summary</asp:ListItem>
                                                <asp:ListItem Value="100">Detail</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td class="bodytxt" valign="middle" align="left">
                                            <tt class="bodytxt">From:</tt>
                                            <telerik:RadDatePicker ID="rdFrom" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td class="bodytxt" valign="middle" align="left">
                                            <tt class="bodytxt">To:</tt>
                                            <telerik:RadDatePicker ID="rdTo" runat="server">
                                            </telerik:RadDatePicker>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Text="Process" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </telerik:RadPageView>
                <telerik:RadPageView class="tbl" runat="server" ID="tbsEmail" Height="450px" Width="100%">
                    <table border="0" cellpadding="2" cellspacing="2" style="width: 100%">
                        <tr>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid14" runat="server" Visible="false" GridLines="None" Skin="Outlook"
                                    Width="100%" AllowFilteringByColumn="True" AllowMultiRowSelection="True">
                                    <MasterTableView AllowAutomaticUpdates="True" Visible="false" AutoGenerateColumns="False"
                                        DataKeyNames="Company_ID">
                                        <FilterItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="Company_ID" DataType="System.Int32"
                                                UniqueName="Company_ID" SortExpression="Company_ID" HeaderText="Company_ID">
                                            </radG:GridBoundColumn>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Company_Name" AutoPostBackOnFilter="True" UniqueName="Company_Name"
                                                SortExpression="Company_Name" HeaderText="Company Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="True">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" />
                                        <Scrolling EnableVirtualScrollPaging="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                            <td style="width: 2%">
                            </td>
                            <td style="width: 49%">
                                <radG:RadGrid ID="RadGrid15" runat="server" GridLines="None" Skin="Outlook" Width="100%"
                                    AllowFilteringByColumn="true" AllowMultiRowSelection="true">
                                    <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False" DataKeyNames="rowid,AliasMonth">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="rowid" DataType="System.Int16"
                                                UniqueName="rowid" SortExpression="rowid" HeaderText="">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="AliasMonth" DataType="System.String" AllowFiltering="true"
                                                AutoPostBackOnFilter="true" UniqueName="AliasMonth" SortExpression="AliasMonth"
                                                HeaderText="Payroll Month">
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="true" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList RepeatDirection="Horizontal" ID="rdOptionEmail" runat="server">
                                                <asp:ListItem Value="1" Selected="true">Email Payslip</asp:ListItem>
                                                <asp:ListItem Value="2">Others</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDocNo" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                <asp:Button ID="btnRptEmail" Text="Generate Report" OnClick="GenerateRptEmail_Click"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" class="trstandtop" colspan="3">
                                &nbsp;</td>
                        </tr>
                    </table>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </div>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" IsSticky="true"
            Style="top: 160; left: 0; height: 100;" Skin="Outlook">
            <asp:Image ID="Image1" Visible="false" runat="server" ImageUrl="~/Frames/Images/Imports/Customloader.gif"
                ImageAlign="Middle"></asp:Image>
        </telerik:RadAjaxLoadingPanel>
        <asp:SqlDataSource ID="SqlDataSource333" runat="server" SelectCommand="Select P.ID Parent_ID, P.Project_ID Parent_Project_Unique, P.Project_Name Parent_Project_Name, S.ID Child_ID, S.Sub_Project_ID, S.Sub_Project_Name   From Project P Inner Join SubProject S On P.ID = S.Parent_Project_ID Where P.Company_Id= @company_id">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:XmlDataSource ID="xmldtMonth" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Months/Month">
        </asp:XmlDataSource>
        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year">
        </asp:XmlDataSource>
        
      <asp:SqlDataSource id="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC">
      </asp:SqlDataSource>
        
        
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT  ID,[TYPE] FROM Leave_Types WHERE CompanyID=-1 OR CompanyID= @company_id">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="SELECT Id,DeptName From Department D INNER Join Employee E On D.Id=E.Dept_Id Where  D.Company_Id= @company_id Group By Id,DeptName">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT  EMP_CODE,EMP_NAME +'' +EMP_LNAME AS NAME FROM dbo.employee WHERE termination_date IS NULL AND COMPANY_ID= @company_id ORDER BY EMP_NAME">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="select Alias_Name,Group_Source  from dbo.TABLEOBJATTRIB  where GroupColumn=1 and Group_Source is not null">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="true" ShowSummary="False" />
        <asp:XmlDataSource ID="xmldtCompType" runat="server" DataFile="~/XML/xmldata.xml"></asp:XmlDataSource>
    </form>
</body>
</html>
