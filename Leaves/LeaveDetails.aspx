<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LeaveDetails.aspx.cs" Inherits="SMEPayroll.Leaves.LeaveDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
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
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <div>
            <center>
                <table border="1" cellpadding="5" cellspacing="5" style="border-collapse: collapse;
                    width: 559px; height: 180px">
                    <tr bgcolor="<% =sOddRowColor %>">
                        <td style="font-size: 9pt; font-family: verdana">
                            Employee Group:</td>
                        <td colspan="2" style="font-size: 9pt; font-family: verdana">
                            <asp:Label ID="lblempgroup" runat="server" Text="Label" Width="394px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; vertical-align: top;" colspan="3">
                            <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                Skin="Outlook" Width="93%">
                                <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
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
                                        <radG:GridBoundColumn DataField="Leaves_allowed" DataType="System.Int32" HeaderText="Leaves Allowed"
                                            SortExpression="Leaves_allowed" UniqueName="Leaves_allowed">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Leaves_taken" DataType="System.DateTime" HeaderText="Leaves Taken"
                                            SortExpression="Leaves_taken" UniqueName="Leaves_taken">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Leaves_Available" DataType="System.DateTime" HeaderText="Leaves Available"
                                            SortExpression="Leaves_Available" UniqueName="Leaves_Available">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Unpaid_Leaves" DataType="System.DateTime" HeaderText="Unpaid Leaves"
                                            SortExpression="Unpaid_Leaves" UniqueName="Unpaid_Leaves">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid><asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_get_leavedetails"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
    </form>
</body>
</html>
