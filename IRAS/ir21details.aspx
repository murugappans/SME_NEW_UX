<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ir21details.aspx.cs" Inherits="IRAS.ir21details" %>

<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.WebControls" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radTS" %>

<%@ Import Namespace="IRAS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IRAS</title>
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />

    <script language="JavaScript1.2"> 
<!-- 

//if (document.all)
//window.parent.defaultconf=window.parent.document.body.cols
//function expando(){
//window.parent.expandf()

//}
//document.ondblclick=expando 

-->
    </script>

</head>
<body style="margin-left: auto">
    <form id="form1" style="padding-top=0" runat="server">
       <radTS:RadScriptManager ID="ScriptManager" runat="server">
        </radTS:RadScriptManager>
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
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="2">
                                <font class="colheading"><b>IR21 Management</b></font>
                            </td>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="1">
                               <font class="colheading">
                              
                       
                            </td>
                               <td background="Frames/Images/toolbar/backs.jpg"  colspan="1">
                               <font class="colheading">
                              <%-- <a href="../ManageIr8a.aspx"  target="workarea"><b class="colheading">HOME</b></a>--%>
                               <b class="colheading">&nbsp;&nbsp;</b>
                               <%--  <a href="../Login.aspx"  target="workarea">
                               <b class="colheading">LOGOUT</b></a>--%>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td  colspan="2">
                            </td>
                            <td  valign="center" colspan="1">
                                <tt class="bodytxt">Year Of Assessment:</tt>&nbsp;&nbsp;<asp:DropDownList ID="cmbYear" runat="server"
                                    Style="width: 65px" CssClass="textfields">
                                  
                                    <asp:ListItem Value="2010">2011</asp:ListItem>
                                    <asp:ListItem Value="2011">2012</asp:ListItem>
                                    <asp:ListItem Value="2012">2013</asp:ListItem>
                                    <asp:ListItem Value="2013">2014</asp:ListItem>
                                    <asp:ListItem Value="2014">2015</asp:ListItem>
                                    <asp:ListItem Value="2015">2016</asp:ListItem>
                                     <asp:ListItem Value="2016">2017</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;<tt class="bodytxt"></tt>&nbsp;&nbsp;<asp:DropDownList
                                    ID="ddlFileType" runat="server" Style="width: 65px" CssClass="textfields" Visible ="false" >
                                    <asp:ListItem Selected="True" Value="O">Original</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp; &nbsp;<tt class="bodytxt"></tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="BonusDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px" Visible ="false">
                                    <DateInput DateFormat="dd-MM-yyyy" Skin="">
                                    </DateInput>
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp; <tt class="bodytxt"></tt>&nbsp;&nbsp;<radCln:RadDatePicker
                                    Calendar-ShowRowHeaders="false" ID="DircetorDate" runat="server" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                    Width="100px" Visible ="false">
                                    <DateInput DateFormat="dd-MM-yyyy" Skin="">
                                    </DateInput>
                                </radCln:RadDatePicker>
                                &nbsp; &nbsp;&nbsp;<tt><asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server"
                                    ImageUrl="~/frames/images/toolbar/go.ico" />&nbsp;</tt></td>
                                     
                           <td colspan="1">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" /></td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td colspan="4">
       <asp:Label ID="lblErr" runat="server" ForeColor="red" class="bodytxt"></asp:Label>&nbsp;&nbsp;  <tt class="bodytxt">If highlighted then employee have 0.00 salary</tt>
                            
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
            
        </table>
      <radTS:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"  Height="30px"
                                           BorderWidth="0px" >
                                            <Items>
                                               
                                              
                                                <radTS:RadToolBarButton IsSeparator="true">
                                                </radTS:RadToolBarButton>
                                                <radTS:RadToolBarButton runat="server" Text="Count">
                                                    <ItemTemplate>
                                                        <div>
                                                            <table cellpadding="0" cellspacing="0" border="0" style="height:30px">
                                                                <tr>
                                                                    <td valign="middle">
                                                                        <img src="../Frames/Images/MENU/count-s.png" border="0" alt="Count" /></td>
                                                                    <td valign="middle">
                                                                        <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ItemTemplate>
                                                </radTS:RadToolBarButton>
                                                
                                            </Items>
    </radTS:RadToolBar>
                                        
        <radTS:RadGrid ID="RadGrid1" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="RadGrid1_ItemDataBound" 
            runat="server" AllowFilteringByColumn="false" AllowMultiRowSelection="true"   GridLines="None" 
            Skin="Outlook" Width="93%" >
            <MasterTableView AutoGenerateColumns="False"  DataKeyNames="emp_code" >
                <Columns>
                    <radTS:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                    </radTS:GridClientSelectColumn>
                    <radTS:GridBoundColumn DataField="emp_code" Visible="false" HeaderText="Code" SortExpression="emp_code"
                        UniqueName="emp_code">
                    </radTS:GridBoundColumn>
                    <radTS:GridBoundColumn Display="true" DataField="emp_name" AllowFiltering="True" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" HeaderText="Emp Name" SortExpression="emp_name"
                        UniqueName="emp_name">
                    </radTS:GridBoundColumn>
                    <radTS:GridBoundColumn Display="true" DataField="EMP_TYPE" AllowFiltering="True" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" HeaderText="Emp Type" SortExpression="EMP_TYPE"
                        UniqueName="EMP_TYPE">
                    </radTS:GridBoundColumn>
                    <radTS:GridBoundColumn DataField="income_taxid" HeaderText="Income Tax ID" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" ReadOnly="True" SortExpression="income_taxid"
                        UniqueName="income_taxid">
                    </radTS:GridBoundColumn>
                    <radTS:GridTemplateColumn UniqueName="Ir8aColumn" AllowFiltering="false" Visible="false">
                        <ItemTemplate>
                            <asp:Button ID="btnIr8a" Text="IR8A" CommandName="GenerateIR8a" runat="server" />
                        </ItemTemplate>
                    </radTS:GridTemplateColumn>
                    <radTS:GridTemplateColumn AllowFiltering="False" HeaderText="View Details" UniqueName="Image">
                        <ItemTemplate>
                            <input type="button" id="Image3" value="Details"  runat="server" />
                        </ItemTemplate>
                    </radTS:GridTemplateColumn>
                    <radTS:GridTemplateColumn UniqueName="Ir8aColumn" Visible="false" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:Button ID="btnIr8aAppendixA" Text="Ir8aAppendixA" CommandName="GenIR8AApp_A"
                                runat="server" />
                        </ItemTemplate>
                    </radTS:GridTemplateColumn>
                    <radTS:GridTemplateColumn HeaderText="Print IR8A"  Visible="false"  UniqueName="PrintTemplateColumn" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgprint" CausesValidation="false" CommandName="Print" runat="server"
                                ImageUrl="Frames/Images/toolbar/print.gif" />
                        </ItemTemplate>
                    </radTS:GridTemplateColumn>
                     <radTS:GridTemplateColumn HeaderText="Print IR8AApendixA"  Visible="false"   UniqueName="PrintTemplateColumnAppdixA" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgprintA" CausesValidation="false" CommandName="PrintA" runat="server"
                                ImageUrl="Frames/Images/toolbar/print.gif" Visible ="false"  />
                        </ItemTemplate>
                    </radTS:GridTemplateColumn>
                    
                </Columns>
            </MasterTableView>
            <ClientSettings Selecting-AllowRowSelect="true">
                <ClientEvents OnRowDblClick="ShowInsertForm" />
            </ClientSettings>
        </radTS:RadGrid>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        &nbsp;<br />
        <br />
        <br />
        <center>
           <asp:Button ID="ir21form_pdf"  Visible="true" runat="server" Text="FORM IR21 PDF" class="textfields"
                Style="width: 130px; height: 22px" OnClick="ir21form_pdf_Click" />
                <asp:Button ID="ir21app1"  Visible="true" runat="server" Text="IR21 APP1 PDF" class="textfields"
                Style="width: 130px; height: 22px" OnClick="ir21app1_Click" />
                <asp:Button ID="ir21app2"  Visible="true" runat="server" Text="IR21 APP2 PDF" class="textfields"
                Style="width: 130px; height: 22px" OnClick="ir21app2_Click" />
                <asp:Button ID="ir21app3"  Visible="true" runat="server" Text="IR21 APP3 PDF" class="textfields"
                Style="width: 130px; height: 22px" OnClick="ir21app3_Click" />
        </center>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <radW:RadWindow ID="DetailGrid" runat="server" Title="User List Dialog" Top="50px"
                    Height="740px" Width="960px" Left="20px" ReloadOnShow="True" Modal="true" Animation="Fade" AnimationDuration="2900" />
            </Windows>
        </radW:RadWindowManager>
    </form>
</body>
</html> 
