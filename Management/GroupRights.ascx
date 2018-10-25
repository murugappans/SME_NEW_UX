<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupRights.ascx.cs"
    Inherits="SMEPayroll.Management.GroupRights" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%--<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr align="center">
    </tr>
    <tr>
        <td valign="middle" style="width:100%" class="bodytxt" align="left" >
           <font face="verdana" size="2">
            </font>
            
        </td>
    </tr>
</table>--%>

<div class="bg-row-red padding-tb-10 clearfix">
    <div class="col-md-12" id="rightsHeader" runat="server">
    </div>
</div>

<radG:RadGrid ID="RadGrid1" CssClass="radGrid-single"  runat="server" AllowMultiRowSelection="True" AllowMultiRowEdit="True"
                GridLines="None" OnItemDataBound ="RadGrid1_ItemDataBound"
            
    AutoGenerateColumns="False" Skin="Outlook" Width="100%">
    <MasterTableView DataKeyNames="RightID,ActualRightName,RightSubCategory,Description">
        <FilterItemStyle HorizontalAlign="left" />
        <HeaderStyle ForeColor="Navy" />
        <ItemStyle BackColor="White" Height="20px" />
        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                    <GroupByExpressions> 
                     <telerik:GridGroupByExpression> 
                       <GroupByFields> 
                        <telerik:GridGroupByField  FieldAlias="RightSubCategory" FieldName="RightSubCategory" /> 
                       </GroupByFields> 
                       <SelectFields> 
                        <telerik:GridGroupByField FieldAlias="RightSubCategory" FieldName="RightSubCategory"  /> 
                        
                       </SelectFields> 
                       
                       
                       
                     </telerik:GridGroupByExpression> 
                     
                    </GroupByExpressions> 
                    <RowIndicatorColumn Visible="False"> 
                        <HeaderStyle Width="20px" /> 
                    </RowIndicatorColumn> 
                    <ExpandCollapseColumn Resizable="False" Visible="False"> 
                        <HeaderStyle Width="20px" /> 
                     
                    </ExpandCollapseColumn>
                    
        <Columns>
            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                        <HeaderTemplate>
                        <asp:CheckBox ID="checkall" runat="server"  AutoPostBack ="true"  OnCheckedChanged="chkLinked_CheckedChanged"     />
                        
                    </HeaderTemplate>
                <ItemTemplate>
                    <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                </ItemTemplate>
                <ItemStyle Width="10px" />
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# GenerateBindString(Container.DataItem)%>' />
                </ItemTemplate>
            </radG:GridTemplateColumn>
            <radG:GridBoundColumn UniqueName="RightID" Visible="False" HeaderText="RightID" DataField="RightID">
            </radG:GridBoundColumn>
            <radG:GridBoundColumn UniqueName="ActualRightName" HeaderText="Rights" DataField="ActualRightName">
            </radG:GridBoundColumn>
            <radG:GridBoundColumn UniqueName="RightSubCategory" HeaderText="RightSubCategory" DataField="RightSubCategory">
            </radG:GridBoundColumn>
            <radG:GridBoundColumn UniqueName="Description" HeaderText="Description" DataField="Description">
            </radG:GridBoundColumn>
        </Columns>
        <EditFormSettings UserControlName="Grouptemplate.ascx" EditFormType="WebUserControl">
            <EditColumn UniqueName="EditCommandColumn1">
            </EditColumn>
        </EditFormSettings>
    </MasterTableView>
    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
            AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
    </ClientSettings>
</radG:RadGrid>