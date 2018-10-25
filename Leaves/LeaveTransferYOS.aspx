<%@ Page Language="C#" AutoEventWireup="true" Codebehind="LeaveTransferYOS.aspx.cs"
    Inherits="SMEPayroll.Leaves.LeaveTransferYOS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
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

        function isNumericKeyStrokeDecimal(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                return false;

             return true;
        }

-->
    </script>
<uc_css:bundle_css ID="bundle_css" runat="server" />

</head>
<body style="margin-left: auto">
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
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="10">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Leave Transfer Form</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td width="5%" align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="lblTrdate" Text="Transfer Date:" runat="server"></asp:Label></tt></tt>
                            </td>
                            <td width="5%" align="left">
                                <tt class="bodytxt">
                                    <radCln:RadDatePicker CssClass="trstandtop" Calendar-ShowRowHeaders="false" ID="rdTrdate"
                                        DateInput-Enabled="false" runat="server" Width="100px">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radCln:RadDatePicker>
                                </tt>
                            </td>
                            <td width="5%" align="right">
                                <tt class="bodytxt">
                                    <asp:Label ID="Label1" Text="Forward Leaves :" runat="server"></asp:Label></tt>
                            </td>
                            <td width="2%" align="left">
                                <tt class="bodytxt">
                                    <asp:TextBox MaxLength="4" onkeypress="return isNumericKeyStrokeDecimal(event)" ID="txtfwd"
                                        Style="height: 13px" CssClass="textfields" runat="Server" Width="30px"></asp:TextBox></tt>
                            </td>
                            <td width="25%" align="left">
                                <tt class="bodytxt">
                                    <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" /></tt>
                            </td>
                            <td width="2%" align="right" colspan="5">
                                <tt class="bodytxt">
                                    <input id="Button3" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                        style="width: 80px; height: 22px" /></tt>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>
            </tr>
            <tr>
                <td>
                    <radG:RadGrid ID="RadGrid1" runat="server" AllowMultiRowSelection="true" AllowPaging="true" DataSourceID="SqlDataSource2"
                        PageSize="200" GridLines="None" Skin="Outlook" Width="99%" OnItemDataBound="RadGrid1_ItemDataBound">
                        <MasterTableView AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="emp_code, LYA, LYL, CYA, CYL, MaxATT, TLYLFT, YOSLA, YOSLB, TAL, CUSTJD" DataSourceID="SqlDataSource2">
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
                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>
                                <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                    UniqueName="emp_code">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                    DataField="EmpGroupName" HeaderText="Group Name" SortExpression="EmpGroupName" UniqueName="EmpGroupName">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                    DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="CustJD" HeaderText="YOS End Date" ReadOnly="True"
                                    SortExpression="CustJD" UniqueName="CustJD">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="LYA" HeaderText="LYL Anniversary" ReadOnly="True"
                                    SortExpression="LYA" UniqueName="LYA">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="LYL" HeaderText="LYL Balance" ReadOnly="True"
                                    SortExpression="LYL" UniqueName="LYL">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="MAXATT" HeaderText="Max Allowed"
                                    ReadOnly="True" SortExpression="MAXATT" UniqueName="MAXATT">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TLYLFT" HeaderText="LYL For Transfer"
                                    ReadOnly="True" SortExpression="TLYLFT" UniqueName="TLYLFT">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="CYA" HeaderText="CYL Anniversary" ReadOnly="True"
                                    SortExpression="CYA" UniqueName="CYA">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="CYL" HeaderText="CYL Allowed" SortExpression="CYL"
                                    UniqueName="CYL">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TAL" HeaderText="Total Leaves Transfer" SortExpression="TAL"
                                    UniqueName="TAL">
                                </radG:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            <Selecting AllowRowSelect="true" />
                        </ClientSettings>
                    </radG:RadGrid></td>
            </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="Button2" runat="server" Text="Transfer Leaves" class="textfields"
                                    Style="width: 130px; height: 23px" OnClick="Button2_Click" />
                                    <br />
                                <asp:Label ID="lblmsg" CssClass="bodytxt" ForeColor="red" runat="server" Visible="false"></asp:Label></td>
                        </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Sp_yos_trans_leave"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtfwd" Name="FL" PropertyName="text" Type="double" />
                <asp:ControlParameter ControlID="rdTrdate" Name="appliedon" PropertyName="SelectedDate"
                    Type="datetime" />
                <asp:SessionParameter Name="compid" SessionField="Compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbEmpgroup"
            Display="None" ErrorMessage="Employee Group Name is not selected!" InitialValue=""></asp:RequiredFieldValidator>
--%>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfwd"
            Display="None" ErrorMessage="Please Enter Forward Leaves!" InitialValue=""></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
<uc_js:bundle_js ID="bundle_js" runat="server" />
 <script type ="text/jscript">
       window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }
         </script>
</body>
</html>
