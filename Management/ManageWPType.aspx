<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageWPType.aspx.cs" Inherits="SMEPayroll.Management.ManageWPType" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
    <radG1:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG1:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage WP Type</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right"style="height: 25px">
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
            <center>
                <radG:RadGrid ID="RadGrid1" runat="server" OnItemDataBound="RadGrid1_ItemDataBound"
                    DataSourceID="SqlDataSource1" GridLines="None" Skin="Default" Width="800px" OnItemInserted="RadGrid1_ItemInserted"
                    OnItemUpdated="RadGrid1_ItemUpdated">
                    <MasterTableView AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                        AutoGenerateColumns="False" CommandItemDisplay="Bottom" DataKeyNames="Id" DataSourceID="SqlDataSource1">
                        <Columns>
                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                <ItemTemplate>
                                    <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </radG:GridTemplateColumn>
                            <radG:GridBoundColumn DataField="Id" DataType="System.Int32" HeaderText="Id" Visible="false"
                                ReadOnly="True" SortExpression="Id" UniqueName="Id">
                                <ItemStyle Width="100px" />
                            </radG:GridBoundColumn>
                            <radG:GridBoundColumn DataField="Wptype" HeaderText="Wptype" SortExpression="WP-Type"
                                UniqueName="Wptype">
                                <ItemStyle Width="500px" />
                            </radG:GridBoundColumn>
                            <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                <ItemStyle Width="50px" />
                            </radG:GridEditCommandColumn>
                            <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                UniqueName="DeleteColumn">
                                <ItemStyle Width="50px" />
                            </radG:GridButtonColumn>
                        </Columns>
                        <ExpandCollapseColumn Visible="False">
                            <HeaderStyle Width="19px" />
                        </ExpandCollapseColumn>
                        <RowIndicatorColumn Visible="False">
                            <HeaderStyle Width="20px" />
                        </RowIndicatorColumn>
                        <CommandItemSettings AddNewRecordText="Add New WP Type" />
                    </MasterTableView>
                </radG:RadGrid><asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="DELETE FROM [wptype] WHERE [Id] = @Id"
                    InsertCommand="INSERT INTO [wptype] ([Wptype]) VALUES (@Wptype)" SelectCommand="SELECT [Id], [Wptype] FROM [wptype] order by 1"
                    UpdateCommand="UPDATE [wptype] SET [Wptype] = @Wptype WHERE [Id] = @Id">
                    <DeleteParameters>
                        <asp:Parameter Name="Id" Type="Int32" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Wptype" Type="String" />
                        <asp:Parameter Name="Id" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Wptype" Type="String" />
                    </InsertParameters>
                </asp:SqlDataSource>
                &nbsp;&nbsp;
            </center>
            <center>
                &nbsp;</center>
        </div>
    </form>
</body>
</html>
