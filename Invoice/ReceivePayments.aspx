<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceivePayments.aspx.cs" Inherits="SMEPayroll.Invoice.ReceivePayments" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>SMEPayroll</title>
    
    
        <script language="JavaScript1.2"> 
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
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
         <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="ffffff" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="4D5459" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Receive Payments</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="E5E5E5">
                            <td align="right" style="height: 35px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <div>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
        <!-- content -->
        <table cellpadding="0" cellspacing="0"  width="100%"  border="0" style="table-layout:fixed;">
                <tr>
                    <td>
                                <radG:RadGrid ID="RadGrid1" runat="server"  DataSourceID="SqlDataSource1"
                                        GridLines="None" Skin="Outlook" Width="100%" AllowFilteringByColumn="true" >
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="ClientID" DataSourceID="SqlDataSource1"
                                           >
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridBoundColumn DataField="ClientID" DataType="System.Int32" HeaderText="ClientID" ReadOnly="True"
                                                    SortExpression="ClientID" Visible="False" UniqueName="ClientID">
                                                </radG:GridBoundColumn>
                                                 <radG:GridBoundColumn DataField="ClientName" HeaderText="Client" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="ClientName" AllowFiltering="true" AndCurrentFilterFunction="contains" ShowFilterIcon="false">
                                                </radG:GridBoundColumn>
                         <%--                       <radG:GridBoundColumn DataField="Total" HeaderText="Total" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="Total" AllowFiltering="false" >
                                                </radG:GridBoundColumn>--%>
                                                <radG:GridBoundColumn DataField="AmountDue" HeaderText="Amount Due" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="AmountDue" AllowFiltering="false" >
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Status" HeaderText="Status" DataType="System.Decimal"
                                                    SortExpression="NH" UniqueName="NH" AllowFiltering="false" >
                                                </radG:GridBoundColumn>
                                          
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridEditCommandColumn>
                                           <%--     <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                                    ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridButtonColumn>--%>
                                            </Columns>
                                            <EditFormSettings UserControlName="ReceivePaymentsUC.ascx" EditFormType="WebUserControl" >
                                            </EditFormSettings>
                                           
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                  <%--  SelectCommand="SELECT [Qid],[QuotationNo],(select Trade from [Trade]where id=Q.[Trade])As Trade,Trade as TradeID,[NH],[OT1],[OT2] FROM [QuoationMaster_hourly] Q where  [company_id]=@Compid "--%>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Sp_ClientInvoiceAmount"
                                     SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                    
                    </td>
                </tr>
           </table>
        <!-- end content -->
    </div>
    </form>
</body>
</html>
