<%@ Control Language="C#" AutoEventWireup="true" Codebehind="StockOutControl.ascx.cs"
    Inherits="SMEPayroll.Management.StockOutControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="javascript">
function fnUpdateChange2(){
 var ctrl= document.getElementById(GetClientId('txtTotalPrice'));
 var ctrl1= document.getElementById(GetClientId('txtDiscPercentage'));
 var ctrl2 =document.getElementById(GetClientId('rdDisCountType1'));
 var ctrl3 =document.getElementById(GetClientId('rdDisCountType2'));
  var ctrl4 =document.getElementById(GetClientId('txtQty'));
 var ctrl5 =document.getElementById(GetClientId('txtPrice'));
document.getElementById(GetClientId('txtDiscLumsum')).value='';
 ctrl2.checked=true;
 ctrl3.checked=false;

 if (parseInt(ctrl1.value) == ctrl1.value )
  {
    var dcvalue = ctrl1.value/100;
    var tvalue ;
    tvalue= ctrl4.value*ctrl5.value*dcvalue;
    tvalue = ctrl4.value*ctrl5.value - tvalue;
    document.getElementById(GetClientId('txtTotalPrice')).value =tvalue;
  }else{
  alert('Invalid Discount');
  }

}
function fnUpdateChange3(){
 var ctrl= document.getElementById(GetClientId('txtTotalPrice'));
 var ctrl1= document.getElementById(GetClientId('txtDiscLumsum'));
 var ctrl2 =document.getElementById(GetClientId('rdDisCountType1'));
 var ctrl3 =document.getElementById(GetClientId('rdDisCountType2'));
 var ctrl4 =document.getElementById(GetClientId('txtQty'));
 var ctrl5 =document.getElementById(GetClientId('txtPrice'));
document.getElementById(GetClientId('txtDiscPercentage')).value='';
 ctrl2.checked=false;
 ctrl3.checked=true;
 document.getElementById(GetClientId('txtTotalPrice')).value=ctrl4.value*ctrl5.value;
 if(ctrl1.value != "."){
 var cnt1 = ctrl1.value.indexOf('.', 0);
 var cnt2 = ctrl1.value.lastIndexOf('.', 0);
 if(cnt2 > cnt1){
 alert('Invalid Lumsum Amount');
 }
  if (parseFloat(ctrl1.value) == (ctrl1.value) )
  {
     if(ctrl4.value*ctrl5.value > ctrl1.value){
        document.getElementById(GetClientId('txtTotalPrice')).value = (ctrl4.value*ctrl5.value)- ctrl1.value;}else{
        alert('Lumsum Amount Should be Less Than Total Price');
        }
 }
 else
 {
 alert('Invalid Lumsum Amount');
 }
 }
}
function fnUpdateChange(){
var ctrl= document.getElementById(GetClientId('txtQty'));
var ctrl2= document.getElementById(GetClientId('txtPrice'));
if(ctrl2.value != '')
{
if(parseFloat(ctrl.value) == ctrl.value)
{
    var ctrl3= document.getElementById(GetClientId('txtTotalPrice'));
    var tvalue = parseFloat(ctrl.value)* parseFloat(ctrl2.value);
    ctrl3.value = tvalue;
}
else{
alert('Invalid Quantity');
ctrl= document.getElementById(GetClientId('txtQty'));
document.getElementById(GetClientId('txtQty')).value='';
}
}
else{
alert('Please Select Item');
}

}
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
var ctrl1;
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
       ctrl = document.getElementById(GetClientId('dlIssueType'));	
    if(ctrl.value=='0')
    sMSG += "Please Select Issue Type \n \n";
      
    if(ctrl.value=='2')
    {
        var  radCtrl1;
        var  radCtrl2;
        radCtrl = document.getElementById(GetClientId('RadioButtonList1_0'));
        if(radCtrl.checked)
        {
            radCtrl1 = document.getElementById(GetClientId('rdDisCountType1'));
            if(radCtrl1.checked)
            {
            document.getElementById(GetClientId('txtDiscLumsum')).value='';
            ctrl = document.getElementById(GetClientId('txtDiscPercentage'));	
            if(ctrl.value =='')
            {
            sMSG += "Please Enter Discount Percentage \n \n";
            }else{
              if (parseFloat(ctrl.value) != ctrl.value )
              {
              sMSG += "Invalid Discount Percentage \n \n";
              }
            }
           }
           radCtrl1 = document.getElementById(GetClientId('rdDisCountType2'));
            if(radCtrl1.checked)
            {
             document.getElementById(GetClientId('txtDiscPercentage')).value='';
            ctrl = document.getElementById(GetClientId('txtDiscLumsum'));	
            if(ctrl.value =='')
            {
            sMSG += "Please Enter Lumsum Amount \n \n";
            }else{
              if (parseFloat(ctrl.value) != ctrl.value )
              {
              sMSG += "Invalid Lumsum Amount \n \n";
              }
            }
         }
              
        }
   
  
     
   }
      radCtrl = document.getElementById(GetClientId('RadioButtonList1_1'));
         if(radCtrl.checked)
        {
       
        ctrl = document.getElementById(GetClientId('dlIssueType'));	
            if(ctrl.value=='2')
                sMSG += "Can not Sell Items To Project ,Please Select Issue \n \n";
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
            <td align="Right">
                Store Name :
            </td>
            <td align="left">
                <asp:DropDownList ID="dlStore" AutoPostBack="true" OnSelectedIndexChanged="dlStore_SelectedIndexChanged"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Item Name :
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlItem" AutoPostBack="true" OnSelectedIndexChanged="dlItem_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlStore" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="Right">
                Employee Name :
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlEmployee" runat="server">
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
                        <asp:DropDownList ID="dlProject" runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
          <td>Quantity :</td>
            <td align="Left" >
                <asp:UpdatePanel runat="server" ID="UpdatePanel8" UpdateMode="Conditional">
                    <ContentTemplate>
                         
                         <asp:TextBox ID="txtQty" Width="60px" MaxLength="6" onkeyup="javascript:fnUpdateChange();" runat="server" style="width: 80px"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                    </Triggers>
                </asp:UpdatePanel>
               
            </td>
            <td  align="RIght">
                Price :</td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel9" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="txtPrice" Width="60px" ReadOnly=true  MaxLength="6" runat="server"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlItem" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="Right">
                Issue Type :
            </td>
            <td align="left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="dlIssueType" AutoPostBack="true" OnSelectedIndexChanged="dlIssuType_SelectedIndexChanged"
                            runat="server">
                            <asp:ListItem Text="- Select -" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Issue" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Sell" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="Right">
                Discount If Any :
            </td>
            <td align="Left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButton Text="Percentage" ID="rdDisCountType1" runat="server" GroupName="r1" />
                        <asp:TextBox ID="txtDiscPercentage" Width="30px" onkeyup="javascript:fnUpdateChange2();" MaxLength="3" runat="server"></asp:TextBox><b>
                        
                            %</b>
                        <asp:RadioButton Text="Lumsum" ID="rdDisCountType2" runat="server" GroupName="r1" />
                        <asp:TextBox ID="txtDiscLumsum" Width="50px" onkeyup="javascript:fnUpdateChange3();" MaxLength="6" runat="server"></asp:TextBox><b>
                            $</b>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlIssueType" />
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList1" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td colspan="2">
            Total Price To Pay :<asp:UpdatePanel runat="server" ID="UpdatePanel10" UpdateMode="Conditional">
                    <ContentTemplate>
                       <asp:TextBox ID="txtTotalPrice" Width="60px" MaxLength="6"  runat="server"></asp:TextBox> 
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtQty" />
                       
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td align="Right">
                Payment Type:
            </td>
            <td align="Left">
                <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rdpayType" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Selected="True" Value="1" Text="From Salary"></asp:ListItem>
                            <asp:ListItem Selected="False" Value="2" Text="Pay By Cash"></asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="dlIssueType" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
            <td colspan="2">
            </td>
        </tr>
        <tr>
        </tr>
        <tr>
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
