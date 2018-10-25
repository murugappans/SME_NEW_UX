<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Ir8aAppendixADetails.aspx.cs"
    Inherits="SMEPayroll.IR8A.Ir8aAppendixADetails" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.Web.UI" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>
<%@ Import Namespace="SMEPayroll" %>
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
<body style="margin-left: auto">
    <form id="form1" style="padding-top=0" runat="server">
       <radTS:RadScriptManager ID="ScriptManager" runat="server">
        </radTS:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
          <uc1:TopRightControl ID="TopRightControl" runat="server" />
           <asp:PlaceHolder ID="placeholder1" runat="server">

            <script type="text/javascript"> 
        function OpenModalWindow()  
        {  
            window.radopen(null,"MYMODALWINDOW");  
        }  
           function ShowInsert(row)
      {          
        window.radopen(row, "DetailGrid");
        return false;
      }
        function CloseModalWindow()  
        {  
            var win = GetRadWindowManager().GetWindowByName("MYMODALWINDOW");          
            win.Close();  
        }  
        function showreport(e)
        {
         var month = document.getElementById('cmbMonth').value;
            var year = document.getElementById('cmbYear').value;
        window.open("paydetailreport.aspx"+"?month="+month+"&year="+year);
         return false;
        }
      function ShowInsertForm(row)
      {          
           return false;
      }
            </script>

        </asp:PlaceHolder>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading"><b>IR8A Management</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td style="width: 1%">
                            </td>
                            <td style="width: 60%" valign="center">
                                <tt class="bodytxt">Year:</tt>&nbsp;&nbsp;<asp:DropDownList ID="cmbYear" runat="server"
                                    Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Value="2009">2010</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<tt class="bodytxt">File Type:</tt>&nbsp;&nbsp;<asp:DropDownList
                                    ID="ddlFileType" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Selected="True" Value="O">Original</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp; &nbsp;<tt class="bodytxt">Bonus Declaration :</tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="BonusDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp; <tt class="bodytxt">Director Fee Approval:</tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="DircetorDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp;<tt><asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server"
                                    ImageUrl="~/frames/images/toolbar/go.ico" />&nbsp;</tt></td>
                            <td style="width: 5%">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" /></td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td style="width: 1%">
                            </td>
                            <td style="width: 60%" align="center">
                                <asp:Label ID="lblErr" runat="server" ForeColor="red" class="bodytxt"></asp:Label>
                            </td>
                            <td style="width: 5%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <radG:RadGrid ID="RadGrid1" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound" 
            runat="server" AllowFilteringByColumn="true" AllowMultiRowSelection=true   GridLines="None"
            Skin="Default" Width="93%">
            <MasterTableView AutoGenerateColumns="False"  DataKeyNames="emp_code" >
                <Columns>
                    <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                    </radG:GridClientSelectColumn>
                    <radG:GridBoundColumn DataField="emp_code" Visible="false" HeaderText="Code" SortExpression="emp_code"
                        UniqueName="emp_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="true" DataField="emp_name" AllowFiltering="True" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" HeaderText="Emp Name" SortExpression="emp_name"
                        UniqueName="emp_name">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="true" DataField="EMP_TYPE" AllowFiltering="True" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" HeaderText="Emp Type" SortExpression="EMP_TYPE"
                        UniqueName="EMP_TYPE">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="income_taxid" HeaderText="income_taxid" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" SortExpression="income_taxid"
                        UniqueName="income_taxid">
                    </radG:GridBoundColumn>
                    <radG:GridTemplateColumn UniqueName="Ir8aColumn" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnIr8a" Text="IR8A" CommandName="GenerateIR8a" runat="server" />
                        </ItemTemplate>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn AllowFiltering="False" HeaderText="" UniqueName="Image">
                        <ItemTemplate>
                            <input type="button" id="Image3" value="Details"  runat="server" />
                        </ItemTemplate>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn UniqueName="Ir8aColumn" Visible="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnIr8aAppendixA" Text="Ir8aAppendixA" CommandName="GenIR8AApp_A"
                                runat="server" />
                        </ItemTemplate>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn UniqueName="PrintTemplateColumn" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgprint" CausesValidation="false" CommandName="Print" runat="server"
                                ImageUrl="../frames/images/toolbar/print.gif" />
                        </ItemTemplate>
                    </radG:GridTemplateColumn>
                </Columns>
            </MasterTableView>
            <ClientSettings EnableClientKeyValues="true" Selecting-AllowRowSelect="true">
                <ClientEvents OnRowDblClick="ShowInsertForm" />
            </ClientSettings>
        </radG:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        &nbsp;<br />
        <br />
        <br />
        <center>
            <%if (Utility.AllowedAction1(Session["Username"].ToString(), "Generate or Submit Payroll"))
              {%>
            <asp:Button ID="btnPrintAllReport" runat="server" Text="Print Report" class="textfields"
                Style="width: 130px; height: 22px" OnClick="btnPrintAllReport_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnsubapprove" OnClick="btnsubapprove_click" runat="server" Text="Generate IR8A  XML"
                class="textfields" Style="width: 130px; height: 22px" />
            &nbsp; &nbsp;
            <%}%>
        </center>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="50px"
                    Height="740px" Width="960px" Left="20px" ReloadOnShow="false" Modal="true" />
            </Windows>
        </radW:RadWindowManager>
    </form>
</body>
</html>
