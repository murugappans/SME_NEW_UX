<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Pattern.aspx.cs" Inherits="SMEPayroll.TimeSheet.Pattern" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>

    <script type="text/javascript">
            var Trade="";
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
                    if(Trade=="" && changedFlage=="false")
                    {
                        var itemIndex = args.get_commandArgument();                            
                        var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                        if(row!=null)
                        {
                            cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                            Trade=cellvalue;
                        }
                    }   
                                   
                    if (result == 'Update' ||result == 'PerformInsert')
                    {
                    
                        var sMsg="";
                        var message ="";                                    
                        message=MandatoryData(trim(Trade),"Business Unit");
                        if(message!="")
                            sMsg+=message+"\n";
                            
                        if(sMsg!="")
                        {
                            args.set_cancel(true);
                            alert(sMsg);
                        }
                    } 
            }
            
            function OnFocusLost_BusinessUnit(val)
            {
                var Object = document.getElementById(val);                                
                Trade =GetDataFromHtml(Object);
                changedFlage="true";
            }  
    </script>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <%--    <uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Timesheet Pattern</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 25px">
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
            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td>
                        <center>
                            <radG:RadGrid ID="RadGrid1" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                                AllowSorting="true" runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1"
                                GridLines="None" Skin="Outlook" Width="93%" OnItemInserted="RadGrid1_ItemInserted"
                                OnItemUpdated="RadGrid1_ItemUpdated">
                                <mastertableview datasourceid="SqlDataSource1" autogeneratecolumns="False" datakeynames="Rid"
                                    allowautomaticdeletes="True" allowautomaticinserts="True" allowautomaticupdates="True"
                                    commanditemdisplay="Bottom">
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
                                    <radG:GridBoundColumn ReadOnly="True" DataField="Rid" DataType="System.Int32" UniqueName="Rid"
                                        Visible="false" SortExpression="Rid" HeaderText="Bid">
                                        <ItemStyle Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="Pattern" UniqueName="Pattern" SortExpression="Pattern" EditFormColumnIndex="0" 
                                        HeaderText="Pattern">
                                        <ItemStyle Width="93%"  HorizontalAlign="left"/>
                                    </radG:GridBoundColumn>
                                    
                                     <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="SubProjectid" DataSourceID="SqlDataSource2"
                                        HeaderText="OutLet" ListTextField="Sub_Project_Name" ListValueField="ID" UniqueName="GridDropDownColumn">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>
                                    
                                     <radG:GridTemplateColumn AllowFiltering="False" DataField="Rid" UniqueName="TimeSlot">
                                        <ItemTemplate>
                                            <tt class="bodytxt">
                                                <asp:Button runat="server" Width="50px" Style="text-decoration: underline; text-align: left"
                                                    BackColor="transparent" ForeColor="Blue" BorderStyle="None" BorderWidth="0" UseSubmitBehavior="true"
                                                    Text="TimeSlot" ID="btnTimeSlot" />
                                            </tt>
                                        </ItemTemplate>
                                    </radG:GridTemplateColumn>
                                    
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
                                <CommandItemSettings AddNewRecordText="Add New Pattern" />
                                
                                          <EditFormSettings ColumnNumber="3">
                                    <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                    <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                    <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                        BackColor="White" Width="100%" />
                                    <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                        Height="30px" BackColor="White" />
                                    <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                    <EditColumn ButtonType="ImageButton" InsertText="Add New Project" UpdateText="Update"
                                        UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                    </EditColumn>
                                    <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                </EditFormSettings>
                            </mastertableview>
                                <clientsettings enablerowhoverstyle="true" allowcolumnsreorder="true" reordercolumnsonclient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                <ClientEvents OnCommand="Validations" />
                            </clientsettings>
                            </radG:RadGrid>
                        </center>
                    </td>
                </tr>
            </table>
            
              <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand=" select ''as id, '' as Sub_Project_Name union  select SP.ID,Sub_Project_Name from SubProject SP inner join Project P on SP.Parent_Project_ID=P.ID where   [company_id] = @company_id">--%>
              <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select SP.ID,SP.Sub_Project_Name From SubProject SP Inner Join Project PR On SP.Parent_Project_ID = PR.ID Left Outer Join Location LO On PR.Location_ID = LO.ID Where (LO.Company_ID=@company_id  OR LO.isShared='YES') AND SP.Active=1">
              
              
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Roaster_Pattern] ([Pattern],Company_id,[SubProjectid]) VALUES (@Pattern,@company_id,@SubProjectid)"
                SelectCommand="SELECT [Rid],[Pattern],[SubProjectid] FROM [dbo].[Roaster_Pattern] where [company_id]=@company_id order by 1"
                UpdateCommand="UPDATE [Roaster_Pattern] SET [Pattern] = @Pattern, [SubProjectid]=@SubProjectid WHERE [Rid] = @Rid">
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="Pattern" Type="String" />
                    <asp:Parameter Name="Rid" Type="Int32" />
                    <asp:Parameter Name="SubProjectid" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="Pattern" Type="String" />
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    <asp:Parameter Name="SubProjectid" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
