<%@ Control Language="C#" AutoEventWireup="true" Codebehind="MedicalControl.ascx.cs"
    Inherits="SMEPayroll.Management.MedicalControl" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="JAVASCRIPT">
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
function disableMe()
{
//
var ctrl= document.getElementById(GetClientId('dlOption'))
var opt = ctrl.options[ctrl.selectedIndex].text; 
 
 if(opt=='Fixed')
 {
 ctrl= document.getElementById(GetClientId('dlFormulaType'))
 ctrl.disabled = true;
 ctrl.value = '0';
  ctrl= document.getElementById(GetClientId('txtFormula'))
ctrl.value='';
 ctrl.disabled=false;
 }
 else if(opt=='Variable')
 {
 ctrl= document.getElementById(GetClientId('dlFormulaType'))
 ctrl.disabled = true;
 ctrl.value = '0';
 ctrl= document.getElementById(GetClientId('txtFormula'))
 ctrl.value=0;
 ctrl.disabled=true;
 
 }
 else
 {

 ctrl= document.getElementById(GetClientId('dlFormulaType'))
 ctrl.disabled=false;
  
  ctrl= document.getElementById(GetClientId('txtFormula'))
ctrl.value='';
 ctrl.disabled=false;
 }

}
 
function validateMe() {
    var sMSG = "";
    var ctrl = document.getElementById(GetClientId('txtROC'));
    if (ctrl.value == "") {
        sMSG += "Please Input Company ROC" + "<br>";
    }
    if (ctrl.value.length < 9 && ctrl.value.length > 0) {
        sMSG += "Company ROC length should be minimum 9 Characters" + "<br>";
    }
    ctrl = document.getElementById(GetClientId('txtType'));
    if (ctrl.value == "") {
        sMSG += "Please Input Company TYPE" + "<br>";
    }
    if (ctrl.value.length < 3 && ctrl.value.length > 0) {
        sMSG += "Company TYPE length should be minimum 3 Characters" + "<br>";
    }

    ctrl = document.getElementById(GetClientId('txtSlNo'));
    if (ctrl.value == "") {
        sMSG += "Please Input Company SLNO" + "<br>";
    }
    if (ctrl.value.length < 2 && ctrl.value.length > 0) {
        sMSG += "Company SLNO length should be minimum 2 Characters" + "<br>";
    }
    ctrl = document.getElementById(GetClientId('dlOption'));
    var opt = ctrl.options[ctrl.selectedIndex].text;
    if (opt == '-Select-') {
        sMSG += "Please Select Formula Option" + "<br>";
    }
    //txtROC
    if (opt == 'Percentage') {
        ctrl = document.getElementById(GetClientId('dlFormulaType'))
        opt = ctrl.options[ctrl.selectedIndex].text;
        if (opt == '-Select-') {
            sMSG += "Please Select Formula Type" + "<br>";
        }
    }
    ctrl = document.getElementById(GetClientId('txtFormula'))
    if (ctrl.value == "") {
        sMSG = sMSG + "Please enter numeric value in Formula Value" + "<br>";
    }
    if (isNaN(ctrl.value) == true) {
        sMSG = sMSG + "Please enter numeric value in Formula Value" + "<br>";
    }
    ctrl = document.getElementById(GetClientId('txtAmtLimit'));
    if (ctrl.value == "") {
        sMSG = sMSG + "Please enter numeric value in Amount Max Limit" + "<br>";
    }
    if (isNaN(ctrl.value) == true) {
        sMSG = sMSG + "Please enter numeric value in Amount Limit" + "<br>";
    }
    ctrl1 = document.getElementById(GetClientId('txtAmtLimit'));
    ctrl2 = document.getElementById(GetClientId('txtAmtMinLimit'));
    if (ctrl1.value < ctrl2.value) {
        sMSG = sMSG + "Amount Min Limit Cannot be greater than Amount Max limit" + "<br>";
    }
    ctrl = document.getElementById(GetClientId('dlRounding'))
    opt = ctrl.options[ctrl.selectedIndex].text;
    if (opt == '-Select-') {
        sMSG += "Please Select Rounding option" + "<br>";
    }
    if (sMSG == "") {
        return true;
    }
    else {
        sMSG = sMSG;
        WarningNotification(sMSG);
        return false;
    }
}
</script>

<div class="clearfix form-style-inner">

    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="lblTitle" CssClass="form-title" runat="server" Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Adding the details" : "Editing the details" %>'
            Width="100%"></asp:Label>
    </div>
    <div class="col-sm-12">
        <hr />
    </div>

    <div class="col-sm-12">
        <div class="form form-inline">
            <div class="form-body">
                <div class="form-group clearfix col-sm-12 no-padding">
                    <label class="control-label">CSN</label>
                        <asp:TextBox ID="txtROC" CssClass="form-control input-sm input-small inline padding-right-0 custom-maxlength cleanstring" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ROC")%>'
                            MaxLength="12"></asp:TextBox> -
                <asp:TextBox ID="txtType" CssClass="form-control input-sm input-xsmall inline padding-right-0 custom-maxlength cleanstring" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TYPE")%>'
                    MaxLength="3"></asp:TextBox> -
                <asp:TextBox ID="txtSlNo" CssClass="form-control input-sm input-xsmall inline padding-right-0 custom-maxlength cleanstring" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.SLNO")%>'
                    MaxLength="2"></asp:TextBox>
                        (ROC-TYPE-SLNO)
                   

                </div>
                <div class="form-group clearfix">

                    <label class="control-label">Formula Option</label>
                   
                        <asp:DropDownList ID="dlOption" OnDataBound="dlFormulaOption_DataBound" OnChange="disableMe();" runat="server" CssClass="form-control input-sm input-small">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Percentage" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Variable" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Formula Type</label>
                    
                        <asp:DropDownList ID="dlFormulaType" OnDataBound="dlFormulaType_DataBound" runat="server" CssClass="form-control input-sm input-small">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Percentage Of Basic Salary" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Percentage Of Net Salary" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Percentage Of Gross Salary" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Formula Value</label>
                    
                        <asp:TextBox ID="txtFormula" CssClass="form-control input-sm input-small text-right number-dot"  data-type="currency" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Formula")%>'
                            MaxLength="4"></asp:TextBox>
                    
                </div>
                <div class="form-group clearfix">

                    <label class="control-label">Amount Min Limit</label>
                    
                        <asp:TextBox ID="txtAmtMinLimit" CssClass="form-control input-sm input-small text-right number-dot" data-type="currency" Text='<%# DataBinder.Eval(Container,"DataItem.AMCSMinLimit")%>' MaxLength="6" runat="server"></asp:TextBox>
                    

                </div>
                <div class="form-group clearfix">

                    <label class="control-label">Amount Max Limit</label>
                    
                        <asp:TextBox ID="txtAmtLimit" CssClass="form-control input-sm input-small text-right number-dot" data-type="currency" Text='<%# DataBinder.Eval(Container,"DataItem.AMCSLimit")%>' MaxLength="6" runat="server"></asp:TextBox>
                    

                </div>
                <div class="form-group clearfix">

                    <label class="control-label">Rounding Option</label>
                    
                        <asp:DropDownList ID="dlRounding" OnDataBound="dlRoundOption_DataBound" runat="server" CssClass="form-control input-sm input-small">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Drop Decimals" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Round Amount" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    

                </div>
            </div>
            <div class="form-actions text-center">
                <asp:Button ID="btnUpdate" CssClass="btn red" OnClientClick="return validateMe();" runat="server" CommandName='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default" />
            </div>

        </div>
    </div>

    

</div>

