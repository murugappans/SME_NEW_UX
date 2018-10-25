<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CappingClaims.aspx.cs"
    Inherits="SMEPayroll.Management.CappingClaims" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading"><b>EMPLOYEE CLAIMS FORM</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td>
                            </td>
                            <td>
                            </td>
                            <td align="center">
                                <tt class="bodytxt">Month :</tt>
                                <asp:DropDownList ID="cmbMonth" runat="server" Style="width: 120px" CssClass="textfields">
                                    <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;<tt class="bodytxt"> Year :</tt>
                                <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Selected="true" Value="-1">-select-</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<tt><asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid"
                                    runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                </tt>
                            </td>
                            <td valign="middle" style="width: 5%" align="right">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="5%">
                    <img alt="" src="../frames/images/PAYROLl/Top-payrollvariables.png" /></td>
            </tr>
        </table>
        <center>
            <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label></center>
        <br />
        <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
            OnItemCommand="RadGrid1_ItemCommand" Skin="Outlook" Width="99%" runat="server"
            DataSourceID="SqlDataSource1" GridLines="None" AllowPaging="true" PageSize="50">
            <MasterTableView CommandItemDisplay="bottom" DataKeyNames="emp_code,empname ,trx_date,trx_year,claim1,claim2,claim3,claim4,claim5,claim6,claim7,claim8,claim9,claim10"
                EditMode="InPlace" AutoGenerateColumns="False" AllowAutomaticUpdates="true" DataSourceID="SqlDataSource1">
                <FilterItemStyle HorizontalAlign="left" />
                <HeaderStyle ForeColor="Navy" />
                <ItemStyle BackColor="White" Height="20px" />
                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                <CommandItemTemplate>
                    <%--to get the button in the grid header--%>
                    <div style="text-align: center">
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CommandName="UpdateAll" />
                    </div>
                </CommandItemTemplate>
                <Columns>
                    <radG:GridBoundColumn DataField="empid" Display="FALSE" DataType="System.Int32" HeaderText="empid"
                        SortExpression="empid" UniqueName="emp_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="empname" Display="true" DataType="System.Int32"
                        HeaderText="empname" SortExpression="empname" CurrentFilterFunction="Contains"
                        AutoPostBackOnFilter="true" UniqueName="empname">
                    </radG:GridBoundColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Claim1" UniqueName="Claim1"
                        HeaderText="Claim1">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim1" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim1")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Claim2" UniqueName="Claim2"
                        HeaderText="Claim2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim2" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim2")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Claim3" UniqueName="Claim3"
                        HeaderText="Claim3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim3" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim3")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Claim4" UniqueName="Claim4"
                        HeaderText="Claim4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim4" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim4")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" DataField="Claim5" UniqueName="Claim5"
                        HeaderText="Claim5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim5" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim5")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" Visible="false" DataField="Claim6"
                        UniqueName="Claim6" HeaderText="Claim6">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim6" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim6")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" Visible="false" DataField="Claim7"
                        UniqueName="Claim7" HeaderText="Claim7">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim7" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim7")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" Visible="false" DataField="Claim8"
                        UniqueName="Claim8" HeaderText="Claim8">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim8" Width="80px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim8")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" Visible="false" DataField="Claim9"
                        UniqueName="Claim9" HeaderText="Claim9">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim9" Width="80px" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim9")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn ItemStyle-HorizontalAlign="Left" Visible="false" DataField="Claim10"
                        UniqueName="Claim10" HeaderText="Claim10">
                        <ItemTemplate>
                            <asp:TextBox ID="txtClaim10" Width="80px" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Claim10")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </radG:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                ReorderColumnsOnClient="true">
                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
            </ClientSettings>
        </radG:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SP_CLAIM_CAPING_New"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter Name="year" Type="Int32" ControlID="cmbYear" />
                <asp:ControlParameter Name="month" Type="Int32" ControlID="cmbMonth" />
                <asp:SessionParameter Name="company_id" SessionField="compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
        <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
    </form>
</body>
</html>
