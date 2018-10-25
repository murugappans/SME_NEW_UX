<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageEducations.aspx.cs" Inherits="SMEPayroll.Management.ManageEducations" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../STYLE/PMSStyle.css" type="text/css" />
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
    <script language="javascript"  type="text/javascript"  src="../Frames/Script/CommonValidations.js" >
    </script>
            
    <script type="text/javascript">
        var Designation="";
        var changedFlage="false";        
        
       //Check Validations for grid like Mandatory and 
       function Validations(sender, args) 
       {
                if (typeof (args) !== "undefined")
                {
                    var commandName = args.get_commandName();
                    var commandArgument = args.get_commandArgument();	        		    
                    switch (commandName) 
                    {
                        case "startRunningCommand":
                            $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
	                        break;
                        case "alertCommand":
                            $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                            break;
                        default:
                            $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                            break;
                    }
                }
                
                var result = args.get_commandName();                                    
                if(Designation=="" && changedFlage=="false")
                {
                    var itemIndex = args.get_commandArgument();                            
                    var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                    if(row!=null)
                    {
                        cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                        Designation=cellvalue;
                    }
                }                        
                if (result == 'Update' ||result == 'PerformInsert')
                {
                    var sMsg="";
                    var message ="";                                    
                    message=MandatoryData(trim(Designation),"Education Name");
                    if(message!="")
                        sMsg+=message+"\n";
                        
                    if(sMsg!="")
                    {
                        args.set_cancel(true);
                        alert(sMsg);
                    }
                } 
        }
        
        function OnFocusLost_Designation(val)
        {
            var Object = document.getElementById(val);                                
            Designation =GetDataFromHtml(Object);
            changedFlage="true";
        }  
    </script>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
       
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0"  cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Highest Education</b></font>
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
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <div>
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid1" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemDataBound="RadGrid1_ItemDataBound" AllowSorting="true"
                            runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook"
                            Width="93%" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated">
                            <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Id"
                                AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                CommandItemDisplay="Bottom">
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
                                    <radG:GridBoundColumn ReadOnly="True" DataField="Id" DataType="System.Int32" UniqueName="Id"
                                        Visible="false" SortExpression="ID" HeaderText="Id">
                                        <ItemStyle Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="HighEducation" UniqueName="HighEducation" SortExpression="HighEducation"
                                        HeaderText="Highest Education">
                                        <ItemStyle Width="93%" />
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                        <ItemStyle Width="30px" />
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                        ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                        UniqueName="DeleteColumn">
                                        <ItemStyle Width="30px" />
                                    </radG:GridButtonColumn>
                                </Columns>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <CommandItemSettings AddNewRecordText="Add New Education" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                            </ClientSettings>
                        </radG:RadGrid></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Educations] ([HighEducation],CompanyId) VALUES (@HighEducation,@CompanyId)"
                SelectCommand="SELECT [Id],[HighEducation] FROM [Educations] where CompanyId=@CompanyId order by 1"
                UpdateCommand="UPDATE [Educations] SET [HighEducation] = @HighEducation WHERE [Id] = @Id">
                <SelectParameters>
                    <asp:SessionParameter Name="CompanyId" SessionField="Compid" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="HighEducation" Type="String" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="HighEducation" Type="String" />
                    <asp:SessionParameter Name="CompanyId" SessionField="Compid" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>
            </center>
            <center>
                &nbsp;</center>
        </div>
    </form>
</body>
</html>
