<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ClientManagement.aspx.cs"
    Inherits="SMEPayroll.Invoice.ClientManagement" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>SMEPayroll</title>
    
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

    <script language="javascript" src="../Frames/Script/CommonValidations.js"></script>

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
    .style1 {
        width: 280px;
    }
</style>
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
            
            color: gray;
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
        
       <%-- .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }--%>
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>
    <script language = "Javascript" type="text/javascript">
/**
 * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
 * remark Textbox
 */

maxL=255;
var bName = navigator.appName;
function taLimit(taObj) {
	if (taObj.value.length==maxL) return false;
	return true;
}

function taCount(taObj,Cnt) { 
	objCnt=createObject(Cnt);
	objVal=taObj.value;
	if (objVal.length>maxL) objVal=objVal.substring(0,maxL);
	if (objCnt) {
		if(bName == "Netscape"){	
			objCnt.textContent=maxL-objVal.length;}
		else{objCnt.innerText=maxL-objVal.length;}
	}
	return true;
}
function createObject(objId) {
	if (document.getElementById) return document.getElementById(objId);
	else if (document.layers) return eval("document." + objId);
	else if (document.all) return eval("document.all." + objId);
	else return eval("document." + objId);
}
</script>



</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Clients </b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 35px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

            <script type="text/javascript">
                function ValidateClient()
                {    
          
//                        var masterTable = $find("<%=RadGrid1.ClientID%>").get_masterTableView();  
//                        var datePicker = masterTable.get_dataItems()[0].findControl('txtClient'); 
//                        alert(datePicker);
           
                        var masterTable = $find("<%=RadGrid1.ClientID%>").get_masterTableView(); 
                        var Client = $telerik.$(masterTable.get_element()).find('input[id*="txtClient"]')[0];
                         if(Client.value=='')
                          {
                            alert("Please Enter Client Name");
                            return false;
                          }
                          
                          var Phone1 = $telerik.$(masterTable.get_element()).find('input[id*="txtPhone1"]')[0];
                          if(Phone1.value=='')
                          {
                            alert("Please Enter Phone Number");
                            return false;
                          }
                          var Email = $telerik.$(masterTable.get_element()).find('input[id*="txtmail"]')[0];
                          if(Email.value=='')
                          {
                              alert("Please Enter Email");
                              return false;
                          }
                         if (Email.value.indexOf(".") > 2) && (Email.value.indexOf("@") > 0);
                          {
                            return true;
                          }
                          else
                          {
                            alert("Please Enter valid Email");
                            return false;
                          }
                 
      
                return true;
            }
            
            

            
             function isNumericKeyStrokeDecimal(evt)
                {
                     var charCode = (evt.which) ? evt.which : event.keyCode
                     if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                        return false;

                     return true;
                }
        
            </script>

        </radG:RadCodeBlock>
        <div>
      <%--  <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />--%>
            <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>
                        <radG:RadGrid ID="RadGrid1" AllowFilteringByColumn="true" runat="server" DataSourceID="SqlDataSource1"
                            GridLines="None" Skin="Outlook" Width="99%" OnDeleteCommand="RadGrid1_DeleteCommand"
                            OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand">
                            <MasterTableView AutoGenerateColumns="False" CommandItemDisplay="top" DataKeyNames="ClientID">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                <CommandItemTemplate>
                                    <div>
                                        <asp:Image ID="Image1" ImageUrl="../frames/images/toolbar/AddRecord.gif" runat="Server" />
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="InitInsert">Add New Client</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                                <Columns>
                                    <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ClientID" DataType="System.Int32"
                                        UniqueName="ClientID" Visible="true" SortExpression="ClientID" HeaderText="ClientID"  CurrentFilterFunction="StartsWith" ShowFilterIcon="False">
                                        <ItemStyle Width="100px" />
                                    </radG:GridBoundColumn>
                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />
                                    </radG:GridTemplateColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="ClientName" UniqueName="ClientName"
                                        HeaderText="Name" SortExpression="ClientName" AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="False"
                                        CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <%--                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Address1" UniqueName="Address1"
                                        HeaderText="Address1" SortExpression="Address1" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Address2" UniqueName="Address2"
                                        HeaderText="Address2" SortExpression="Address2" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Address3" UniqueName="Address3"
                                        HeaderText="Address3" SortExpression="Address3" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>--%>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Phone1" UniqueName="Phone1"
                                        HeaderText="Phone-1" SortExpression="Phone1" AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="False"
                                        CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Phone2" UniqueName="Phone2"
                                        HeaderText="Phone-2" SortExpression="Phone2" AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="False"
                                        CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Fax" UniqueName="Fax" HeaderText="Fax" ShowFilterIcon="False"
                                        SortExpression="Fax" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Email" UniqueName="Email" ShowFilterIcon="False"
                                        HeaderText="Email" SortExpression="Email" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="ContactPerson1" UniqueName="ContactPerson1"
                                        HeaderText="Contact Person" SortExpression="ContactPerson1" AllowFiltering="true" ShowFilterIcon="False"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="StartsWith">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <%--  <radG:GridBoundColumn EditFormColumnIndex="1" DataField="ContactPerson2" UniqueName="ContactPerson2"
                                        HeaderText="Contact Person Name2" SortExpression="ContactPerson2" AllowFiltering="true"
                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Remark" UniqueName="Remark"
                                        HeaderText="Remark" SortExpression="Remark" AllowFiltering="true" AutoPostBackOnFilter="true"
                                        CurrentFilterFunction="Contains">
                                        <ItemStyle Width="500px" HorizontalAlign="Left" />
                                    </radG:GridBoundColumn>--%>
                                    <radG:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton">
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn CommandName="Delete" Text="Delete">
                                    </radG:GridButtonColumn>
                                </Columns>
                                <EditFormSettings EditFormType="Template">
                                    <EditColumn UniqueName="EditCommandColumn1">
                                    </EditColumn>
                                    <FormTemplate>
                                        <div class="exampleWrapper">
                                           <%-- <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />--%>
                                            <table id="Table2"  cellspacing="0" cellpadding="0" width="100%" border="0" class="tbl"
                                                align="center">
                                                <tr>
                                                    <td style="color: #000000; height: 20px; font-weight: bold; background-color: #e9eed4;
                                                        text-align: center">
                                                        Client Details
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="padding-left: 20px;" border="0" width="100%">
                                                            <tr>
                                                                <td class="tdstand" colspan="4">
                                                                    (A) Client Name
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td colspan="4">
                                                                    Name:</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtClient" runat="server" Text='<%# Bind( "ClientName" ) %>' Width="500px" >
                                                                    </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdstand" colspan="4">
                                                                    (B) Address
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Block:</td>
                                                                <td>
                                                                    Street/Building:</td>
                                                                <td>
                                                                    Level:</td>
                                                                <td>
                                                                    Unit:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtBlock" runat="server" Text='<%# Bind( "Block" ) %>' Width="100px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtStreet" runat="server" Text='<%# Bind( "StreetBuilding" ) %>'
                                                                        Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtLevel" runat="server" Text='<%# Bind( "Level" ) %>' Width="100px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind( "Unit" ) %>' Width="100px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td colspan="4">
                                                                    Postal Code:</td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <asp:TextBox ID="txtPostal" runat="server" Text='<%# Bind( "PostalCode" ) %>' Width="100px"></asp:TextBox></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdstand" colspan="4">
                                                                    (C) Other Details:
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Phone1:</td>
                                                                <td>
                                                                    Phone2:</td>
                                                                <td>
                                                                    Fax:</td>
                                                                <td>
                                                                    Email:</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhone1" runat="server" Text='<%# Bind( "Phone1" ) %>' Width="100px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhone2" runat="server" Text='<%# Bind( "Phone2" ) %>' Width="100px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtFax" runat="server" Text='<%# Bind( "Fax" ) %>' Width="100px"></asp:TextBox></td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmail"  runat="server" Text='<%# Bind( "Email" ) %>' Width="200px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    ContactPerson1:</td>
                                                                <td>
                                                                    ContactPerson2:</td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactPerson1" runat="server" Text='<%# Bind( "ContactPerson1" ) %>'
                                                                        Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtContactPerson2" runat="server" Text='<%# Bind( "ContactPerson2" ) %>'
                                                                        Width="150px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="tdstand" colspan="4">
                                                                    (D) Remark:
                                                                </td>
                                                            </tr>
                                                            <tr class="trstandbottom">
                                                                <td>
                                                                    Remark:</td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" >
                                                                    <asp:TextBox ID="txtRemark" Width="550px" Font-Names="Tahoma" Font-Size="Small" Height="100px" runat="server" Text='<%# Bind( "Remark" ) %>'   onKeyPress="return taLimit(this)" onKeyUp="return taCount(this,'myCounter')" 
                                                                        TextMode="multiLine"></asp:TextBox>
                                                                        <br>
                                                                  <span class="trstandbottom"> You have <b><SPAN id="myCounter">255</SPAN></b> characters remaining</font></span>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 20px" colspan="4">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="color: #000000; height: 20px; font-weight: bold; background-color: #e9eed4;
                                                        text-align: center">
                                                        <asp:Button ID="btnUpdate" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                                                            runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                                                            OnClientClick="return ValidateClient();"></asp:Button>&nbsp;
                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                            CommandName="Cancel"></asp:Button></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </FormTemplate>
                                </EditFormSettings>
                                <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px" />
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <CommandItemSettings AddNewRecordText="Add New Client" />
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                        </radG:RadGrid></td>
                </tr>
            </table>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT [ClientID],[ClientName],[Block],[StreetBuilding],[Level],[Unit],[PostalCode],[Phone1],[Phone2],[Fax],[Email],[ContactPerson1],[ContactPerson2],[Remark],[company_id]FROM [ClientDetails] where company_id=@Compid">
                <SelectParameters>
                    <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                </SelectParameters>
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
