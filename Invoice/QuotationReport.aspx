<%@ Page Language="C#" AutoEventWireup="true" Codebehind="QuotationReport.aspx.cs"
    Inherits="SMEPayroll.Invoice.QuotationReport" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <div>
           <%-- <radG:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />--%>
            <table cellpadding="0" cellspacing="0" border="0" width="90%" align="center">
                <tr>
                    <td height="40px" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2" class="bodytxt">
                        <b>QUOTATION</b>
                    </td>
                </tr>
                <tr>
                    <td width="70%" valign="top">
                        <table cellpadding="3" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="lblClientName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" valign="top">
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height:2px;"></td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>Phone:</b><asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>&nbsp;
                                    <b>Fax:</b><asp:Label ID="lblFax" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                  <b>Email:</b><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="30%">
                        <table cellpadding="5" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td class="bodytxt">
                                    <b>Date:</b><asp:Label ID="lblCreatedate" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>Quotation No:</b><asp:Label ID="lblQuot" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>Sales Rep:</b><asp:Label ID="lblsalesrep" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <b>Sub Project:</b><asp:Label ID="lblProject" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="bodytxt" height="40px"><b> RE:DAYWORK SUB-CONTRACT FOR SKILLED WORKERS for your different site</b></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td class="tdstand" id="HourId" runat="server">
                                    Hourly Trade
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:GridView ID="GridView1_Hor" runat="server" AutoGenerateColumns="true" Width="100%"  CellPadding="2" CellSpacing="2" BorderWidth="1px" 
                                           HeaderStyle-BackColor="black" HeaderStyle-ForeColor="white" Font-Names="Tahoma" Font-Size="12px"  RowStyle-HorizontalAlign="center" >
                                        <Columns>
                           <%--                 <asp:BoundField DataField="Trade" HeaderText="Trade" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="NH" HeaderText="Normal Hour(/Hr)" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="OT1" HeaderText="Overtime1(/Hr)" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="OT2" HeaderText="Overtime2(/Hr-Sunday/PH)" ItemStyle-HorizontalAlign="right" />--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" id="MonthId" runat="server">
                                    Monthly Trade</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1_Mon" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="2" CellSpacing="2" BorderWidth="1px" 
                                           HeaderStyle-BackColor="black" HeaderStyle-ForeColor="white" Font-Names="Tahoma" Font-Size="12px" >
                                        <Columns>
                                            <asp:BoundField DataField="Trade" HeaderText="Trade" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="EmpName" HeaderText="Emp Name" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Monthly" HeaderText="Monthly" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="Workingdays" HeaderText="Working Days/Week" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="DailyRate" HeaderText="Daily Rate" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="OT1" HeaderText="OT(/Hr)" ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundField DataField="OT2" HeaderText="OT2(/Hr-Sunday/PH)" ItemStyle-HorizontalAlign="right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" id="adddetId" runat="server">
                                    Addition/Deduction</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1_Var" runat="server" AutoGenerateColumns="False" Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="VarName" HeaderText="Variable Name" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height:10px"></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="divid" runat="server" class="bodytxt"  >
                        
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
