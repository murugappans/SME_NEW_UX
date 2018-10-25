<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EditInvoice.aspx.cs" Inherits="SMEPayroll.Invoice.EditInvoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    

    <script language="JavaScript1.2" type="text/javascript"> 
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
            /*background-repeat: no-repeat;*/
           background-repeat:repeat-x;
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
</head>
<body style="margin-left: auto; vertical-align: top;">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Invoice</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 35px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" bgcolor="ffffff" width="100%" border="0" class="tbl"
            style="padding-left: 20px">
            <tr>
                <td valign="top">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                       <tr>
                            <td class="tdstand" colspan="6">
                                Client:<asp:Label ID="lblClient" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdstand" colspan="6">
                                Company Information
                            </td>
                        </tr>
                        <tr class="trstandbottom">
                            <td width="15%">
                                Contact Person:
                            </td>
                            <td width="15%">
                                Block:</td>
                            <td width="15%">
                                Street/Building:</td>
                            <td width="15%">
                                Level:</td>
                            <td width="15%">
                                Unit:</td>
                            <td width="15%">
                                Postal Code:
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt">
                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                            <td class="bodytxt">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="bodytxt">
                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="bodytxt">
                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="bodytxt">
                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="bodytxt">
                                <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdstand" colspan="6">
                                Detail
                            </td>
                        </tr>
                        <tr class="trstandbottom">
                            <td>
                                Date:</td>
                            <td>
                                Invoice No:</td>
                            <td>
                                Payment Terms:</td>
                            <td>
                                </td>
                            <td>
                               </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="bodytxt">
                                <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label></td>
                            <td class="bodytxt">
                                <b>
                                    <asp:Label ID="lblInvoice" runat="server" Text=""></asp:Label></b></td>
                            <td class="bodytxt">
                                <asp:Label ID="lblPaymentTerms" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="bodytxt">
                              </td>
                            <td class="bodytxt">
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="tdstand" valign="top" id="hourId" runat="server">
                    Hourly Trade
                </td>
            </tr>
            <tr>
                <td style="padding-right: 20px; vertical-align: top;" valign="top" id="hourGridId"
                    runat="server">
                    <radG:RadGrid ID="RadGrid_Hourly" runat="server" AllowPaging="true" PageSize="20"
                        GridLines="None" Width="100%" ShowFooter="True" Skin="Outlook" AllowMultiRowSelection="true">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Trade">
                            <Columns>
                                <radG:GridBoundColumn DataField="Project" UniqueName="Project" SortExpression="Project"
                                    HeaderText="Project">
                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Trade" UniqueName="Trade" SortExpression="Trade"
                                    HeaderText="Trade">
                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" SortExpression="AMOUNT"
                                    Aggregate="Sum" FooterText="Total=" HeaderStyle-HorizontalAlign="center" UniqueName="AMOUNT">
                                    <ItemStyle Width="40%" HorizontalAlign="right" />
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn EditFormColumnIndex="1" UniqueName="Detail" HeaderText="Detail"
                                                    HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DetailLink" Text='Detail' runat="server" NavigateUrl='<%# GetUrl( Eval("TradeID"),Eval("Project_ID"),Eval("ClientId"),Eval("FromDate"),Eval("ToDate"))%>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                               </radG:GridTemplateColumn>
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <FooterStyle HorizontalAlign="right" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="tdstand" valign="top" id="MonthId" runat="server">
                    Monthly Trade
                </td>
            </tr>
            <tr>
                <td style="padding-right: 20px; vertical-align: top;" valign="top" id="MonthGridId"
                    runat="server">
                    <radG:RadGrid ID="RadGrid1_Monthly" runat="server" AllowPaging="true" PageSize="20"
                        GridLines="None" Width="100%" ShowFooter="True" Skin="Outlook">
                        <ClientSettings AllowExpandCollapse="True">
                        </ClientSettings>
                        <MasterTableView AutoGenerateColumns="False">
                            <Columns>
                                <radG:GridBoundColumn DataField="ProjectName" ReadOnly="True" UniqueName="ProjectName"
                                    SortExpression="ProjectName" HeaderText="ProjectName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TradeName" ReadOnly="True" UniqueName="TradeName"
                                    SortExpression="TradeName" HeaderText="Trade">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="EMP_NAME" ReadOnly="True" UniqueName="EMP_NAME"
                                    SortExpression="EMP_NAME" HeaderText="Emp Name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Amount" ReadOnly="True" Aggregate="Sum" FooterText="Total="
                                    UniqueName="Amount" SortExpression="Amount" HeaderText="Amount">
                                    <ItemStyle HorizontalAlign="right" />
                                </radG:GridBoundColumn>
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <FooterStyle HorizontalAlign="right" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
            <tr>
                <td class="tdstand" valign="top" id="VarId" runat="server">
                    Variables
                </td>
            </tr>
            <tr>
                <td style="padding-right: 20px; vertical-align: top;" valign="top" id="VarGridId"
                    runat="server">
                    <radG:RadGrid ID="RadGrid3" runat="server" AllowPaging="true" PageSize="20" GridLines="None"
                        Width="100%" ShowFooter="false" Skin="Outlook" AllowMultiRowSelection="true">
                        <MasterTableView AutoGenerateColumns="False">
                            <Columns>
                                <radG:GridBoundColumn DataField="Project" HeaderText="Project" SortExpression="Project"
                                    ReadOnly="true" HeaderStyle-HorizontalAlign="center" UniqueName="Project">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="variableName" HeaderText="variableName" SortExpression="variableName"
                                    ReadOnly="true" HeaderStyle-HorizontalAlign="center" UniqueName="variableName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" ReadOnly="true"
                                    HeaderStyle-HorizontalAlign="center" UniqueName="Type">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" SortExpression="AMOUNT"
                                    Aggregate="Sum" FooterText="Total=" HeaderStyle-HorizontalAlign="center" UniqueName="AMOUNT">
                                </radG:GridBoundColumn>
                            </Columns>
                            <ExpandCollapseColumn Visible="False">
                                <HeaderStyle Width="19px"></HeaderStyle>
                            </ExpandCollapseColumn>
                            <RowIndicatorColumn Visible="False">
                                <HeaderStyle Width="20px"></HeaderStyle>
                            </RowIndicatorColumn>
                            <FooterStyle HorizontalAlign="right" />
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnPrint" runat="server" CausesValidation="False" OnClick="btnPrint_Click"
                        Text="Print" Width="64px" />
                </td>
            </tr>
        </table>
        <div>
        </div>
    </form>
</body>
</html>
