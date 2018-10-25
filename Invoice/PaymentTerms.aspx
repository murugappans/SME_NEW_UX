<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentTerms.aspx.cs" Inherits="SMEPayroll.Invoice.PaymentTerms" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

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
<style type="text/css">
    INPUT {
    FONT-SIZE: 8pt;	
    FONT-FAMILY: Tahoma
          }
        .bigModule
        {
            width: 750px;
            background: url(qsfModuleTop.jpg) no-repeat;
            margin-bottom: 15px;
        }
        .bigModuleBottom
        {
            background: url(qsfModuleBottom.gif) no-repeat bottom;
            color: #252f34;
            padding: 23px 17px;
            line-height: 16px;
            font-size: 12px;
        }
        .trstandtop
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            
            COLOR: gray;
            vertical-align:bottom;
            valign:bottom;
        }
       
        .tdstand
        {
            height:30px;
            vertical-align:text-bottom;
            vertical-align:bottom;
            border-bottom-width:1px;
            border-bottom-color:Silver;
            border-bottom-style:inset;
	        font-family: Tahoma;
	        font-size: 12px;
	        font-weight:bold;
        }
        .tbl
        {
            cellpadding:0;
            cellspacing:0;
            border:0;
            background-color: White; 
            width: 100%; 
            height: 100%; 
            background-image: url(../Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            /*background-repeat: no-repeat;*/
           background-repeat:repeat-x;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color:#F0F1EB;
            padding:4px;
            padding-left:0;
            width:85%;
            height:550px%;
            margin-left:-1px;                
        }
        
      .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Payment Terms </b></font>
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
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
            </script>

            <script type="text/javascript">
            var DepartmentName="";
            var changedFlage="false";        
            
            function RowDblClick(sender, eventArgs)
            {
                    sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            } 
                        
            
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
                    if(DepartmentName=="" && changedFlage=="false")
                    {
                        var itemIndex = args.get_commandArgument();                            
                        var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                        if(row!=null)
                        {
                            cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                            DepartmentName=cellvalue;
                        }
                    }                                          
                    if (result == 'Update' ||result == 'PerformInsert')
                    {
                        var sMsg="";
                        var message ="";                                    
                        message=MandatoryData(trim(DepartmentName),"Variable name");
                        if(message!="")
                            sMsg+=message+"\n";
                            
                        if(sMsg!="")
                        {
                            args.set_cancel(true);
                            alert(sMsg);
                        }
                    } 
            }
            
            function OnFocusLost_DepartmentName(val)
            {
                var Object = document.getElementById(val);                                
                DepartmentName =GetDataFromHtml(Object);
                changedFlage="true";
            }  
            </script>

        </radG:RadCodeBlock>
        <div class="exampleWrapper">
            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
            <table cellpadding="1" class="tbl" cellspacing="0" bgcolor="<% =sBorderColor %>"
                width="100%" border="0">
            
                <tr>
                    <td class="tdstand">
                        Payment Terms
                    </td>
                </tr>
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid2" OnDeleteCommand="RadGrid2_DeleteCommand"  AllowSorting="true"
                            runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource2" GridLines="None" Skin="Outlook"
                            Width="100%" OnItemInserted="RadGrid2_ItemInserted" OnItemUpdated="RadGrid2_ItemUpdated">
                            <MasterTableView DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="Ip"
                                AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                CommandItemDisplay="Bottom" >
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
                                    <radG:GridBoundColumn ReadOnly="True" DataField="Ip" DataType="System.Int32" UniqueName="id"
                                        Visible="false" SortExpression="ID" HeaderText="Id">
                                        <ItemStyle Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="PaymentTerms" UniqueName="PaymentTerms" SortExpression="PaymentTerms"
                                        HeaderText="Payment Terms">
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
                                <CommandItemSettings AddNewRecordText="Add New Trade" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                            </ClientSettings>
                        </radG:RadGrid>
                          <asp:SqlDataSource ID="SqlDataSource2" runat="server" InsertCommand="INSERT INTO [PaymentTerms]([PaymentTerms],[Company_id]) VALUES(@PaymentTerms,@company_id)"
                            SelectCommand="SELECT [Ip],[PaymentTerms],[Company_id]  FROM [PaymentTerms] where company_id=@company_id order by 1"
                            UpdateCommand="UPDATE [PaymentTerms] SET [PaymentTerms] = @PaymentTermsWHERE [Ip] = @Ip">
                            <SelectParameters>
                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="PaymentTerms" Type="String" />
                                <asp:Parameter Name="Ip" Type="Int32" />
                            </UpdateParameters>
                            <InsertParameters>
                                <asp:Parameter Name="PaymentTerms" Type="String" />
                                <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                            </InsertParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
           <asp:SqlDataSource ID="SqlDataSource_VarPref" runat="server" SelectCommand="SELECT  [VarId] ,[Type]  FROM [variable_type]">
           </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Variable_Preference] ([VarName],[VariableId],[DailyOneTime],Company_id) VALUES (@VarName,@VariableId,@DailyOneTime,@company_id)"
                SelectCommand="SELECT [Vid], [VarName],[VariableId],CASE WHEN [DailyOneTime] = 'OneTime' THEN 'Monthly' ELSE [DailyOneTime] END [DailyOneTime] FROM [Variable_Preference] VP where company_id=@company_id order by [Vid]"
                UpdateCommand="UPDATE [Variable_Preference] SET [VarName] = @VarName,[VariableId]=@VariableId,[DailyOneTime]=@DailyOneTime WHERE [Vid] = @Vid">
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="VarName" Type="String" />
                    <asp:Parameter Name="DailyOneTime"  Type="String" />
                    <asp:Parameter Name="Vid" Type="Int32" />
                    <asp:Parameter Name="VariableId"  Type="String" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="VarName" Type="String" />
                    <asp:Parameter Name="VariableId"  Type="String" />
                    <asp:Parameter Name="DailyOneTime"  Type="String" />
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </InsertParameters>
            </asp:SqlDataSource>
        </div>
    </form>
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>

    <script type="text/javascript">
        
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>

</body>
</html>
