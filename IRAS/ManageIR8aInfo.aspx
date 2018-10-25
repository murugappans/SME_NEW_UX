<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageIR8aInfo.aspx.cs"
    Inherits="IRAS.ManageIR8aInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>IRAS</title>
    <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />

<script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Clicking the option will change the default to selected Address Type.  Do you want to Proceed ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
 </script>


    <script  type="text/javascript"> 
    
    function redirect() {
   
     window.parent.location.href  = "../Login.aspx"
   
}

 function redirecthome() {
   
     window.parent.frames[2].location = "../ManageIr8a.aspx";
     //window.parent.location.href  = "../ManageIr8a.aspx"
}

<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){

window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>

    <link rel="stylesheet" href="../style/MenuStyle.css" type="text/css" />
</head>
<body onload="ShowMsg();" style="margin-left: auto">
    <form id="form1" runat="server">
            <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>

        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="2">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee IRAS Info</b></font>
                            </td>
                            <td background="Frames/Images/toolbar/backs.jpg" colspan="1">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Year Of Assessment</b></font>
                                <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">                                    
                                  
                                    <asp:ListItem Value="2010">2011</asp:ListItem>
                                    <asp:ListItem Value="2011">2012</asp:ListItem>
                                    <asp:ListItem Value="2012">2013</asp:ListItem>
                                    <asp:ListItem Value="2013">2014</asp:ListItem>
                                    <asp:ListItem Value="2014">2015</asp:ListItem>
                                    <asp:ListItem Value="2015">2016</asp:ListItem>
                                       <asp:ListItem Value="2016">2017</asp:ListItem>
                                          <asp:ListItem Value="2017">2018</asp:ListItem>
                                    <asp:ListItem Value="2018">2019</asp:ListItem>
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            
                             <td background="Frames/Images/toolbar/backs.jpg" align="center" colspan="1">
                           <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Global Address Update</b></font>
                              <asp:CheckBox  runat="server" ID="addressUpdate" AutoPostBack="true" onClick="Confirm();" OnCheckedChanged="checkboxclick"/>
                       
                            </td>
                                                      
                            
                            
                            <td background="Frames/Images/toolbar/backs.jpg" align="center" colspan="1">
                        
                             <asp:DropDownList ID="cmbaddress" class="textfields" runat="server" Width="116px" Enabled="false">
                                                                        <asp:ListItem Value="N" Text="No Address"></asp:ListItem>
                                                                        <asp:ListItem Value="L" Text="Local Residential address"></asp:ListItem>
                                                                        <asp:ListItem Value="F" Text="Foreign Address"></asp:ListItem>
                                                                        <asp:ListItem Value="C" Text="C/O Address"></asp:ListItem>
                                                                    </asp:DropDownList>
                            </td>
                            <td background="Frames/Images/toolbar/backs.jpg" align="center" colspan="1">
                                <asp:Button ID="btnUpdateIrAs" OnClick="btnUpdateIras_Click" Text="Generate" runat="server" />
                            </td>
                            
                           <td background="Frames/Images/toolbar/backs.jpg" align="center" colspan="1">
                              
                              <%-- <a href="../Login.aspx"  target="workarea" style="text-decoration: none;" class="nav"><b class="colheading">LOGOUT</b></a>--%>
                       
                            </td>
                           <%--    <td background="Frames/Images/toolbar/backs.jpg" align="center" colspan="1">
                               <font class="colheading">
                            <%--   <a href="../ManageIr8a.aspx"  target="workarea" style="text-decoration: none;" class="nav"><b class="colheading">HOME</b></a>
                       
                            </td>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!-- content start -->
        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radG:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="RadGrid2" />
                    </UpdatedControls>
                </radG:AjaxSetting>
            </AjaxSettings>
        </radG:RadAjaxManager>
        <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

            <script language="javascript" type="text/javascript">
            
                function ShowMsg()
                {
                    var sMsg = '<%=sMsg%>';
                    if (sMsg != '')
                        alert(sMsg);
                }
            </script>

        </radG:RadCodeBlock>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td style="width: 100%">
                    <radG:RadGrid ID="RadGrid1" runat="server" GridLines="None" AutoGenerateColumns="False"
                        Skin="Outlook" Width="98%" AllowPaging="True" PageSize="20" AllowFilteringByColumn="True"
                        AllowSorting="true" OnPreRender="RadGrid1_PreRender" OnItemDataBound="RadGrid1_ItemDataBound"
                        OnItemCommand="RadGrid1_ItemCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                        OnPageIndexChanged="RadGrid1_PageIndexChanged">
                        <MasterTableView DataKeyNames="emp_code">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="EmpCode" HeaderImageUrl="../frames/images/EMPLOYEE/Grid- employee.png"
                                    HeaderText="Emp Code" CurrentFilterFunction="StartsWith" DataField="emp_code"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:Image ID="Image2" ImageUrl="Frames/Images/EMPLOYEE/Grid- employee.png" runat="Server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="2px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn ShowFilterIcon="False" AllowFiltering="False" UniqueName="TemplateColumnEC"
                                    Display="false" HeaderText="Code" SortExpression="emp_code">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Text='<%# "DPT"+ DataBinder.Eval(Container.DataItem,"emp_code").ToString()%>'
                                            NavigateUrl='<%# "ManageIr8a.aspx?empcode=" + DataBinder.Eval (Container.DataItem,"emp_code").ToString()%>'
                                            ID="empcode" />
                                    </ItemTemplate>
                                    <ItemStyle Width="80px" />
                                    <HeaderStyle HorizontalAlign="left" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="emp_name" HeaderText="Employee Name"
                                    DataField="emp_name" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="190px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False"  UniqueName="ic_pp_number" HeaderText="Employee IC"
                                    DataField="ic_pp_number" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="190px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="True" Visible="false"  ShowFilterIcon="False" UniqueName="Department"
                                    HeaderText="Department" DataField="Department" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn Display="True" ShowFilterIcon="False" UniqueName="emp_type"
                                    HeaderText="Passtype" DataField="emp_type" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn Display="True" ShowFilterIcon="False" UniqueName="empgroupname"
                                    HeaderText="Emp Group Name" DataField="empgroupname" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn HeaderText="Update IR8A Details" AllowFiltering="False" UniqueName="ir8a">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8a" Text="IR8A" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn Visible="false"  AllowFiltering="False" UniqueName="ir8aAppendixA">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8aApepndixA" Text="IR8A Appdix A" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn Visible="false"  AllowFiltering="False" UniqueName="ir8aAppendixB">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:Button ID="btnIr8aApepndixB" Text="IR8A Appdix B" Enabled="false" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20px" />
                                </radG:GridTemplateColumn>
                                
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                        </ClientSettings>
                    </radG:RadGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
