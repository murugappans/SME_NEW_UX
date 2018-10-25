<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ClaimsExtApproval.aspx.cs"
    Inherits="SMEPayroll.Payroll.ClaimsExtApproval" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />

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
        
        <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="100"  runat="server" Transparency="10" BackColor="#E0E0E0"   InitialDelayTime="1000">
            <asp:Image ID="Image1"  runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
        </telerik:RadAjaxLoadingPanel>
        
                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
                     function MyClick(sender, e)  
                    {  
                        var inputs = document.getElementById("<%= RadGrid1.MasterTableView.ClientID %>").getElementsByTagName("input");  

                        for(var i = 0, l = inputs.length; i < l; i++)  
                        {  
                        var input = inputs[i];  
                        if(input.type != "radio" || input == sender)  
                        continue;  
                        input.checked = false;  
                        //document.getElementById("divRemarks").innerText =input.name;
                        }  
                    }
            </script>
         
        </radG:RadCodeBlock>
        
        <telerik:RadAjaxManager id="sc"  runat="server">           
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="imgbtnfetch"  >
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridClaimExt"  LoadingPanelID="RadAjaxLoadingPanel1"/>
                        <telerik:AjaxUpdatedControl ControlID="lblerror"  LoadingPanelID="RadAjaxLoadingPanel1"/>
                        
                    </UpdatedControls>
                </telerik:AjaxSetting>
                
                 <telerik:AjaxSetting AjaxControlID="Button2" EventName="Button2_Click"   >
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl  ControlID="radGridClaimExt"  LoadingPanelID="RadAjaxLoadingPanel1"/>
                        <telerik:AjaxUpdatedControl ControlID="lblerror"  LoadingPanelID="RadAjaxLoadingPanel1"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
                
                <telerik:AjaxSetting AjaxControlID="Button3"  EventName="Button3_Click"   >
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="radGridClaimExt" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="lblerror"  LoadingPanelID="RadAjaxLoadingPanel1"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        
  
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Approve / Reject Claim Request</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td valign="middle" align="right">
                                <tt class="bodytxt">Supervisor :</tt></td>
                            <td>
                                <asp:Label ID="lblsuper" runat="server" Text="Label" Width="220px" Height="16px"
                                    CssClass="bodytxt"></asp:Label>
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button4" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <table cellpadding="1" cellspacing="0" width="100%"
            border="0">
            
                
            
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" > 
                    <ContentTemplate>
                     <tr>
                         <td  align="center">
                            <asp:Label class="bodytxt"  ID="lblerror" runat="server" ForeColor="red"></asp:Label>
                        </td>
                    </tr>
                        
                    <tr>
                            <td>
                        <telerik:RadGrid ID="radGridClaimExt" Skin="Outlook"  
                                OnPreRender="radGridClaimExt_PreRender" 
                                AllowMultiRowSelection="true"
                                OnDetailTableDataBind="GridClaimExt_DetailTableDataBind"
                                runat="server" OnNeedDataSource="GridClaimExt_NeedDataSource"
                                AutoGenerateHierarchy="true">
                               <MasterTableView  HierarchyDefaultExpanded="true"
                                    ExpandCollapseColumn-ButtonType="ImageButton" ExpandCollapseColumn-CollapseImageUrl="../help/images/exp.gif"
                                        ExpandCollapseColumn-ExpandImageUrl="../help/images/col.gif"
                                 >
                                    <Columns> 
                                        <telerik:GridClientSelectColumn  Reorderable="False" DataTextField="ClientSelectColumn"    UniqueName="ClientSelectColumn"  >
                                             <HeaderStyle Width="30px"></HeaderStyle>
                                         </telerik:GridClientSelectColumn>
                                    </Columns>
                               </MasterTableView> 
                        </telerik:RadGrid>
                 
                </td>
            </tr>
                 </ContentTemplate>      
            </asp:UpdatePanel>     
            <tr>
                <td>
                    <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true"
                        Visible="false" GridLines="None" Skin="Outlook" Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,emp_code,remarks,emp_name" DataSourceID="SqlDataSource2">
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
                                <%--<radG:GridTemplateColumn HeaderText="Select" UniqueName="TemplateColumn1">
                                    <ItemTemplate>
                                        <asp:RadioButton ID="remarkRadio" AutoPostBack="true" OnCheckedChanged="remarkRadio_CheckedChanged"
                                            GroupName="rad" runat="server" onclick="MyClick(this,event)" />
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>--%>
                                <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                    UniqueName="emp_code">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                    UniqueName="emp_name">
                                    <ItemStyle Width="20%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                    ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="desc" HeaderText="Claim Type" ReadOnly="True" SortExpression="Type"
                                    UniqueName="Type">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Department" HeaderText="Department" ReadOnly="True"
                                    SortExpression="Department" UniqueName="Department">
                                    <ItemStyle Width="10%" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="Type"
                                    Visible="False" UniqueName="Type">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                    SortExpression="trx_amount" UniqueName="trx_amount">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="trx_period" DataType="System.DateTime" HeaderText="Period"
                                    SortExpression="trx_period" UniqueName="trx_period">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn HeaderText="Attached Document">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="h1" runat="server" Target="_blank" ></asp:HyperLink>
                                    </ItemTemplate>
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="remarks" DataType="System.String" HeaderText="remarks"
                                    ReadOnly="True" SortExpression="remarks" UniqueName="remarks">
                                </radG:GridBoundColumn>
                                
                                 <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                </radG:GridBoundColumn> 
                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number"  DataField="ic_pp_number" Display="false"  AllowFiltering="false" >
                                </radG:GridBoundColumn>
                                
                                
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                    </radG:RadGrid></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppendingclaim_add"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter DefaultValue=" " Name="approver" SessionField="Emp_Name" Type="String" />
                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                <asp:ControlParameter ControlID="textbox1" Name="txt" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        
       
        
        <table style="width: 646px; height: 116px">
            <tr>
                <td visible="false"  class="bodytxt""></td>
               
            </tr>
            <tr>
                <td  visible="false" colspan="2">
                    <input id="txtEmpRemarks" visible="false" disabled="disabled" style="height: 36px; width: 581px;"
                        name="txtEmpRemarks" runat="server" /></td>
            </tr>
            <tr>
                <td style="font-weight: bold; font-size: 9pt; width: 5px; font-family: Verdana; height: 15px;
                    color: #000000;">
                    Remarks:</td>
                <td style="width: 563px; height: 15px;">
                    &nbsp;</td>
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: left">
                        <asp:TextBox runat="server" ID="txtremarks" Height="73px" TextMode="MultiLine" Width="581px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button2" runat="server" Text="Approve Claim" class="textfields" Style="width: 130px;
                            height: 23px" OnClick="Button2_Click" /></td>
                    <td style="text-align: left">
                        <asp:Button ID="Button3" runat="server" Text="Reject Claim" class="textfields" Style="width: 130px;
                            height: 23px" OnClick="Button3_Click" />
                    </td>
                </tr>
        </table>
        <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
    </form>
</body>
</html>
