<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComchestSetup_edit.ascx.cs" Inherits="SMEPayroll.Management.ComchestSetup_edit" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="javascript" type="text/javascript">
    function GetClientId(strid) {
        var count = document.forms[0].length;
        var i = 0;
        var eleName;
        for (i = 0 ; i < count ; i++) {
            eleName = document.forms[0].elements[i].id;
            pos = eleName.indexOf(strid);
            if (pos >= 0) break;
        }
        return eleName;
    }
    function disableMe() {
        //
        var ctrl = document.getElementById(GetClientId('dlOption'))
        var opt = ctrl.options[ctrl.selectedIndex].text;

        // if(opt=='Fixed')
        // {
        // ctrl= document.getElementById(GetClientId('dlFormulaType'))
        // ctrl.disabled=true;
        //  ctrl= document.getElementById(GetClientId('txtFormula'))
        //ctrl.value='';
        // ctrl.disabled=false;
        // }
        if (opt == 'Variable') {

            ctrl = document.getElementById(GetClientId('dlFormulaType'))
            ctrl.SelectedValue = '0';
            ctrl.disabled = true;

            ctrl = document.getElementById(GetClientId('txtFormula'))
            ctrl.SelectedValue = '1';
            ctrl.value = 0;
            ctrl.disabled = true;
            //dlRounding.SelectedValue='0';
            ctrl = document.getElementById(GetClientId('dlRounding'))
            ctrl.SelectedValue = '0';
            ctrl.disabled = true;

        }
        else if (opt == 'Percentage') {
            //alert(opt);

            ctrl = document.getElementById(GetClientId('dlFormulaType'))

            //ctrl.SelectedValue='1';
            //ctrl.SelectedItem.text='Basic Salary';

            ctrl.disabled = false;

            ctrl = document.getElementById(GetClientId('txtFormula'))
            ctrl.value = '1';
            ctrl.disabled = false;
            ctrl = document.getElementById(GetClientId('dlRounding'))
            ctrl.SelectedValue = '0';
            ctrl.disabled = false;
        }
        else {
            ctrl = document.getElementById(GetClientId('dlFormulaType'))
            ctrl.disabled = false;

            ctrl = document.getElementById(GetClientId('txtFormula'))
            ctrl.value = '';
            ctrl.disabled = false;
            ctrl = document.getElementById(GetClientId('dlRounding'))
            ctrl.SelectedValue = '0';
            ctrl.disabled = false;
        }


    }

    function validateMe() {
        var sMSG = "";
        //   var ctrl= document.getElementById(GetClientId('txtROC'));
        //   if(ctrl.value.length <9)
        //   {
        //   sMSG +="Company ROC length should be minimum 9 Characters"+ "\n";
        //   }
        //   ctrl= document.getElementById(GetClientId('txtType'));
        //   if(ctrl.value.length <3)
        //   {
        //   sMSG +="Company TYPE length should be minimum 3 Characters"+ "\n";
        //   }
        //   
        //    ctrl= document.getElementById(GetClientId('txtSlNo'));
        //   if(ctrl.value.length <2)
        //   {
        //   sMSG +="Company SLNO length should be minimum 3 Characters"+ "\n";
        //   }
        //     ctrl= document.getElementById(GetClientId('dlOption'));
        //    var opt = ctrl.options[ctrl.selectedIndex].text; 
        //    if(opt=='-Select-')
        //    {
        //     sMSG +="Invalid Formula Option"+ "\n";
        //    }
        //txtROC
        if (opt == 'Percentage') {
            ctrl = document.getElementById(GetClientId('dlFormulaType'))
            opt = ctrl.options[ctrl.selectedIndex].text;
            if (opt == '-Select-') {
                sMSG += "Invalid Formula Type" + "\n";
            }
        }
        ctrl = document.getElementById(GetClientId('txtFormula'))
        if (ctrl.value == "") {
            sMSG = sMSG + "Please enter numeric value in Formula Value" + "\n";
        }
        if (isNaN(ctrl.value) == true) {
            sMSG = sMSG + "Please enter numeric value in Formula Value" + "\n";
        }
        ctrl = document.getElementById(GetClientId('txtAmtLimit'));
        if (ctrl.value == "") {
            sMSG = sMSG + "Please enter numeric value in Amount Limit" + "\n";
        }
        if (isNaN(ctrl.value) == true) {
            sMSG = sMSG + "Please enter numeric value in Amount Limit" + "\n";
        }
        ctrl = document.getElementById(GetClientId('dlRounding'))
        opt = ctrl.options[ctrl.selectedIndex].text;
        if (opt == '-Select-') {
            sMSG += "Invalid Rounding option" + "\n";
        }
        if (sMSG == "") {
            return true;
        }
        else {
            sMSG = "Following fields are missing.\n\n" + sMSG;

            return false;
        }
    }
</script>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="lblTitle" class="form-title" runat="server" Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Adding the details" : "Editing the details" %>'
            Width="100%"></asp:Label>
    </div>


    
        <hr />
    

    

        <div class="form form-inline">
            <div class="form-body">
                <div class="form-group clearfix">
                    <label class="control-label">CSN</label>
                    
                        <asp:DropDownList ID="ddlCSN" CssClass="form-control input-inline input-sm input-medium" runat="server" OnDataBound="ddlCSN_DataBound">
                        </asp:DropDownList>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Formula Option</label>
                    
                        <asp:DropDownList ID="dlOption" CssClass="form-control input-inline input-sm input-medium" OnDataBound="dlFormulaOption_DataBound" OnChange="disableMe();" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Percentage" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Variable" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Formula Type</label>
                    
                        <asp:DropDownList ID="dlFormulaType" CssClass="form-control input-inline input-sm input-medium" OnDataBound="dlFormulaType_DataBound" runat="server">
                            <%--<asp:ListItem Text="-Select-" Value="0"></asp:ListItem>--%>
                            <asp:ListItem Text="Basic Salary" Value="1"></asp:ListItem>

                        </asp:DropDownList>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Formula Value</label>
                    
                        <asp:TextBox ID="txtFormula" CssClass="form-control input-inline input-sm input-medium text-right number-dot" data-type="currency" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Formula")%>'
                            MaxLength="4" ></asp:TextBox>

                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Rounding Option</label>
                    
                        <asp:DropDownList ID="dlRounding" CssClass="form-control input-inline input-sm input-medium" OnDataBound="dlRoundOption_DataBound" runat="server">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Drop Decimals" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Round Amount" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    
                </div>
            </div>


            <div class="form-actions text-center">
                <asp:Button ID="btnUpdate" CssClass="btn red" runat="server" CommandName='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' 
                    OnClientClick="return Validations();" />
                <asp:Button ID="btnCancel" CssClass="btn default" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel"  />
            </div>

            
        </div>
    

   



</div>
<script language="javascript" type="text/javascript">
    function Validations() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= ddlCSN.ClientID %>")).val()) === "-select-")
            _message = "Please Select CSN. <br/>";
        if ($.trim($(document.getElementById("<%= dlOption.ClientID %>")).val()) === "0")
            _message += "Please Select Formula Option. <br/>";
        if ($.trim($(document.getElementById("<%= dlFormulaType.ClientID %>")).val()) === "-Select-")
               _message += "Please Select Formula Type. <br/>";
        if ($.trim($(document.getElementById("<%= dlOption.ClientID %>")).val()) === "1")
        {
            if ($.trim($(document.getElementById("<%= txtFormula.ClientID %>")).val()) === "")
                _message += "Please Input Formula Value. <br/>";
            else  if ($.trim($(document.getElementById("<%= txtFormula.ClientID %>")).val()) >=100)
                 _message += "Please Enter Valid Formula Value. <br/>";

            if ($.trim($(document.getElementById("<%= dlRounding.ClientID %>")).val()) === "0")
                _message += "Please Select Rounding Option. <br/>";
           
        }
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        
        return true;
    }
</script>
