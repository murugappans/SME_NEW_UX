<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ImportEmployee.aspx.cs"
    Inherits="SMEPayroll.Management.ImportEmployee" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Import Employee</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td style="width: 982px">
                    <table style="vertical-align: middle; width: 80%;" align="center" cellpadding="1"
                        cellspacing="0" border="0">
                        <tr>
                            <td style="width: 20%; height: 24px; text-align: right">
                                <tt class="bodytxt">Select File :</tt>
                            </td>
                            <td colspan="3" style="height: 24px">
                                <input id="FileUpload" runat="server" name="FileUpload" style="width: 90%" type="file" /><%--<asp:RequiredFieldValidator
                                    ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload" Display="Static"
                                    ErrorMessage="Please Select File">*</asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator
                                        ID="revFileUpload" runat="Server" ControlToValidate="FileUpload" ErrorMessage="Please Select CSV Files"
                                        ValidationExpression=".+\.(([cC][sS][vV]))">*</asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td colspan="4" style="height: 24px; text-align: center">
                                <asp:Button ID="CmdUpload" runat="server" OnClick="CmdUpload_Click" Text="Upload" /></td>
                        </tr>
                        <tr>
                            <td align="center" valign="middle" colspan="4" style="height: 26px">
                                <tt class="bodytxt">
                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label></tt>
                            </td>
                        </tr>
                    </table>
                    <table border="1" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="4" align="left">
                                <asp:Button ID="btnValidate" Visible="false" runat="server" OnClick="btnValidate_Click" Text="Validate" />
                                <asp:Button ID="btnSubmit" Visible="false" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <radG:RadGrid ID="empResults" runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                    OnItemDataBound="empResults_ItemDataBound" Skin="Outlook" Width="98%" AllowFilteringByColumn="true"
                                    AllowMultiRowSelection="true" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages">
                                    <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                        DataKeyNames="id,DELETED" AllowPaging="true" PageSize="500">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>
                                            <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                                <ItemStyle Width="10%" />
                                            </radG:GridClientSelectColumn>
                                            <radG:GridBoundColumn DataField="Deleted" DataType="System.String" UniqueName="Deleted"
                                                SortExpression="Deleted" HeaderText="Deleted" Display="false">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn AllowFiltering="False" DataField="ID" DataType="System.String" UniqueName="ID" SortExpression="ID"
                                                HeaderText="ID">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Status" DataType="System.String" UniqueName="Status"
                                                SortExpression="Status" HeaderText="Status">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn Display="false" DataField="ErrorContent" DataType="System.String"
                                                UniqueName="ErrorContent" SortExpression="ErrorContent" HeaderText="Error">
                                                <ItemStyle Width="200px" />
                                                <HeaderStyle Width="200px" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="TimeCardNo" DataType="System.String" UniqueName="TimeCardNo"
                                                SortExpression="TimeCardNo" HeaderText="Time Card No">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="FirstName" DataType="System.String" UniqueName="FirstName"
                                                SortExpression="FirstName" HeaderText="First Name">
                                                <ItemStyle Width="40%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="LastName" DataType="System.String" UniqueName="LastName"
                                                SortExpression="LastName" HeaderText="Last Name">
                                                <ItemStyle Width="10%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="EmpType" DataType="System.String" UniqueName="EmpType"
                                                SortExpression="EmpType" HeaderText="EmpType">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="NRIC" DataType="System.String" UniqueName="NRIC"
                                                SortExpression="NRIC" HeaderText="NRIC">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="FIN" DataType="System.String" UniqueName="FIN" SortExpression="FIN"
                                                HeaderText="FIN">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="date_of_birth" DataType="System.string" UniqueName="date_of_birth" SortExpression="date_of_birth"
                                                HeaderText="DOB">
                                                <ItemStyle Width="5%" />
                                            </radG:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                </radG:RadGrid>
                            </td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_GetImportedEmployee" SelectCommandType="storedProcedure">
                        <SelectParameters>
                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="False" />
                </td>
            </tr>
        </table>
    </form>
    <script type="text/javascript">
       
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>
</body>
</html>
