<%@ Control Language="C#" AutoEventWireup="true" Codebehind="stockReturnControl.ascx.cs"
    Inherits="SMEPayroll.Management.stockReturnControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="javascript">
function GetClientId(strid)
{
     var count=document.forms [ 0 ] . length ;
     var i = 0 ;
     var eleName;
     for (i = 0 ;  i < count ;  i++ )
     {
       eleName = document.forms [ 0 ] . elements [ i ] .id;
       pos=eleName.indexOf ( strid ) ;
       if(pos >= 0)  break;           
     }
    return eleName;
 }
 

function validateMe()
{
var sMSG = "";
var ctrl;
var radCtrl;
var itmQty=0;
var text="";


ctrl = document.getElementById(GetClientId('dlStore'));

 if(ctrl.value == '0')
   {
    sMSG += "Please Select Store \n \n";
  }                             

ctrl = document.getElementById(GetClientId('dlItem'));
 if(ctrl.value == 0)
   {
     sMSG += "Please Select Item \n \n";

   }else
   {
  itmQty = ctrl.options[ctrl.selectedIndex].text; 
   itmQty= itmQty.split("-");
   itmQty=itmQty[1].split("[Qty]");
   itmQty =parseInt(itmQty[0]);
 
   }
    radCtrl = document.getElementById(GetClientId('RadioButtonList1_0'));
 if(radCtrl.checked)
   {
   ctrl = document.getElementById(GetClientId('dlEmployee'));	
    if(ctrl.value=='0')
        sMSG += "Please Select Employee \n \n";
   }    
   
   radCtrl = document.getElementById(GetClientId('RadioButtonList1_1'));
 if(radCtrl.checked)
   {
   ctrl = document.getElementById(GetClientId('dlProject'));	
    if(ctrl.value=='0')
    sMSG += "Please Select Project \n \n";
   }
       
   ctrl = document.getElementById(GetClientId('txtQty'));	
    if(ctrl.value=='')
        {
          sMSG += "Please Enter Quantity \n \n";
        }
        else
        {
           if (parseInt(ctrl.value) != ctrl.value )
            {
             sMSG += "Invalid Quantity Of Item \n \n";
            }else{
            if (parseInt(ctrl.value) <= 0 )
            {
             sMSG += "Invalid Quantity Of Item \n \n";
            }else if(parseInt(ctrl.value) > itmQty)
        
               sMSG += "Quantity should be less than Item Quantity in the item list \n \n";
            
            }
            
        }
if (sMSG == "")
{
    return true;
}
 else{
    sMSG = "Following fields are missing.\n\n" + sMSG; 
    alert(sMSG); 
    return false;
 }
return false;
}
</script>

<center>
    <table>
        <tr>
            <td align="center" colspan="4">
                <h5>
                    Stockout Details
                </h5>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="RadioButtonList1" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"
                            AutoPostBack="true" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Selected="True" Value="1" Text="Employee"></asp:ListItem>
                            <asp:ListItem Selected="False" Value="2" Text="Project"></asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                Employee Name
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlEmployee" AutoPostBack="true" OnSelectedIndexChanged="dlEmployee_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td>
                Project List :
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlProject" AutoPostBack="true" OnSelectedIndexChanged="dlProject_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                Store Name :
            </td>
            <td align="left">
                <asp:DropDownList ID="dlStore" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Item Name :
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlItem" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlEmployee" />
                        <asp:AsyncPostBackTrigger ControlID="dlProject" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                Quantity :
            </td>
            <td align="left">
                <asp:TextBox ID="txtQty" runat="server"></asp:TextBox>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnUpdate" Text="Add" OnClientClick="return validateMe();" CommandName="PerformInsert"
                    runat="server" Width="70px" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" Width="70px" />
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</center>
