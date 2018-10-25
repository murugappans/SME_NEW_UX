<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AllowedLeaves.aspx.cs"
    Inherits="SMEPayroll.Leaves.AllowedLeaves" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
     <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>
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
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td bgcolor="<% =sHeadingColor %>" colspan="4">
                                <font class="colheading"><b>VIEW ALLOWED LEAVE </b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td valign="middle" align="right">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/bgifs/leaveallowed.jpg" /></td>
            </tr>
        </table>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <radG:RadGrid ID="RadGrid1" runat="server" OnItemDataBound="RadGrid1_ItemDataBound"
                        OnUpdateCommand="RadGrid1_UpdateCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                        OnInsertCommand="RadGrid1_InsertCommand" DataSourceID="SqlDataSource1" GridLines="None"
                        Skin="Default" Width="93%">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="id,group_id,leave_type"
                            DataSourceID="SqlDataSource1">
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
                                <radG:GridBoundColumn Visible="false" DataField="id" DataType="System.Int32" HeaderText="Group Name"
                                    SortExpression="id" UniqueName="id">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="EmpGroupName" DataType="System.string" HeaderText="Group Name"
                                    SortExpression="Group Name" UniqueName="group_id">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="group_id" Visible="false" DataType="System.Int32"
                                    HeaderText="group_id" SortExpression="group_id" UniqueName="group_id">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="leave_type" Visible="false" DataType="System.Int32"
                                    HeaderText="leave_type" SortExpression="leave_type" UniqueName="leave_type">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="type" DataType="System.String" HeaderText="Leave Type"
                                    SortExpression="Leave Type" UniqueName="type">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="leaves_allowed" DataType="System.Int32" HeaderText="Leave Allowed"
                                    SortExpression="Allowed Leaves" UniqueName="leaves_allowed">
                                </radG:GridBoundColumn>
                                <radG:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton">
                                </radG:GridEditCommandColumn>
                            </Columns>
                            <EditFormSettings UserControlName="LeavesAllowed.ascx" EditFormType="WebUserControl">
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                        </ClientSettings>
                        <GroupPanel Visible="True">
                        </GroupPanel>
                    </radG:RadGrid></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT e.id, b.id group_id, b.[EmpGroupName] empgroupname,c.id leave_type,c.type type, e.[leaves_allowed] FROM [leaves_allowed] 
                            e,[emp_group] b,leave_types c where b.company_id=@company_id and e.[group_id]= b.[id] and e.[leave_type]=c.[id] order by 2">
            <SelectParameters>
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>  

    <script type="text/javascript">
       
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>
</body>
</html>
