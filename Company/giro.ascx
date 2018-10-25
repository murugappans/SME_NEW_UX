<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="giro.ascx.cs" Inherits="SMEPayroll.Company.giro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="RadGrid.Net2" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" runat="server" CssClass="form-title"></asp:Label>

    </div>



    <hr />




    <div class="form-inline">
        <div class="form-body">

            <div class="form-group clearfix">
                <label class="control-label">Bank Name</label>

                <asp:DropDownList ID="drpbankname" OnDataBound="drpbankname_databound" OnSelectedIndexChanged="drpbankname_SelectedIndexChanged" class="textfields form-control input-sm input-medium" runat="server" AutoPostBack="true"></asp:DropDownList>

            </div>
            <div class="form-group clearfix display-none">
                <label class="control-label">Date</label>

                <asp:TextBox ID="txtvaluedate" Text='<%# DataBinder.Eval(Container,"DataItem.value_date")%>' CssClass="form-control input-sm input-medium numericonly custom-maxlength" Enabled="true" runat="server"></asp:TextBox><span>[cannot be a sunday or public holiday]</span>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Branch Code</label>

                <asp:TextBox ID="txtbranch" MaxLength="6" data-min="3" class="textfields form-control input-sm input-medium numericonly custom-maxlength" Text='<%# DataBinder.Eval(Container,"DataItem.bank_branch")%>' runat="server"></asp:TextBox>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Bank AccNo</label>

                <asp:TextBox ID="txtbankaccno"  MaxLength="20" data-min="10" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.bank_accountno")%>' class="textfields form-control input-sm input-medium numericonly custom-maxlength"></asp:TextBox>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Account Name</label>

                <asp:TextBox ID="txtgiroaccountname" Text='<%# DataBinder.Eval(Container,"DataItem.giro_acc_name")%>' runat="server" class="textfields form-control input-sm input-medium alphabetsonly custom-maxlength" MaxLength="50"></asp:TextBox>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Currency</label>

                <asp:DropDownList ID="drpCurrency" class="form-control input-sm input-medium" runat="server"></asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">
                    <asp:Label runat="server" Text="Company Code provided by Bank:" ID="lblDBS"></asp:Label></label>

                <asp:TextBox ID="compbankcode" data-min="4" MaxLength="15" Text='<%# DataBinder.Eval(Container,"DataItem.company_bankcode")%>' runat="server" class="textfields form-control input-sm input-medium numericonly custom-maxlength " Visible="True"></asp:TextBox>
                <div><asp:Label runat="server" Text="[Applicable for DBS,BOA and <br> ANZ Bank only]" ID="lblDBS1" /></div>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">
                    <asp:Label runat="server" Text="Approver Code provided by Bank:" ID="lblMZ1"></asp:Label></label>

                <asp:TextBox ID="txtApprover" runat="server" MaxLength="15" class="textfields form-control input-sm input-medium numericonly custom-maxlength" Text='<%# DataBinder.Eval(Container,"DataItem.approvercode")%>'
                    Visible="True"></asp:TextBox>
                <div><asp:Label runat="server" Text="[Applicable for Mizuho Bank only]" ID="lblMZ11" /></div>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">
                    <asp:Label runat="server" Text="Operator Code provided by Bank:" ID="lblMZ2" /></label>

                <asp:TextBox ID="txtOperator" runat="server" MaxLength="15" class="textfields form-control input-sm input-medium numericonly custom-maxlength" Text='<%# DataBinder.Eval(Container,"DataItem.operatorcode")%>'
                    Visible="True"></asp:TextBox>
                <div><asp:Label runat="server" Text="[Applicable for Mizuho Bank only]" ID="lblMZ22" /></div>

            </div>



        </div>


        <div class="form-actions text-center">
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" OnClientClick="return ValidateGiriInfo();" CssClass="btn red" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn default" />
        </div>

    </div>








</div>

<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="drpbankname"
    Display="None" ErrorMessage="Bank Name" InitialValue="-1"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtvaluedate"
    Display="None" ErrorMessage="Value Date" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtbranch"
    Display="None" ErrorMessage="Branch Code" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtbankaccno"
    Display="None" ErrorMessage="Bank Acc No" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtgiroaccountname"
    Display="None" ErrorMessage="Bank Acc Name" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtvaluedate"
    Display="None" ErrorMessage="Invalid Value Date" MaximumValue="31" MinimumValue="1"
    SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtbranch" Display="None"
    ErrorMessage="Invalid Branch Code" MaximumValue="111111111111111111111111111111111"
    MinimumValue="1" Type="Double"></asp:RangeValidator>
     <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtbankaccno" Display="None"
    ErrorMessage="Invalid Account No" MaximumValue="111111111111111111111111111111111"
    MinimumValue="1" Type="Double"></asp:RangeValidator>
<asp:RegularExpressionValidator ID="AlphanuericROC" runat="server" ControlToValidate="compbankcode" 
ErrorMessage="Company Code should be Alphanumeric" ValidationExpression="\w+([-+.]\w+)*" Display="None" /> 
<asp:ValidationSummary  HeaderText="The following fields are missing" ID="ValidationSummary1" runat="server" DisplayMode="List" 
    ShowMessageBox="True"  ShowSummary="False" />--%>

<script language="javascript" type="text/javascript">
    function ValidateGiriInfo()
    {
      var sMsg=""; 
     // var sMsg=""; *      
      var variable1=document.getElementById("<%= drpbankname.ClientID %>");             
      
   //   var variable2=document.getElementById("<%= txtvaluedate.ClientID %>");             
      //* AlphaNumeric      
      var variable3=document.getElementById("<%= txtbranch.ClientID %>");       
      
      //* AlphaNumeric      
      var variable4=document.getElementById("<%= txtbankaccno.ClientID %>");             
      
      var variable5=document.getElementById("<%= txtgiroaccountname.ClientID %>"); 
      
      //DBS Bank
      var variable6=document.getElementById("<%= compbankcode.ClientID %>");             
      //Mizuho  Bank
      var variable7=document.getElementById("<%= txtApprover.ClientID %>");
      var variable8=document.getElementById("<%= txtOperator.ClientID %>");
      
      //alert(variable1 + ',' +variable2+ ',' + variable3+ ','+ variable4 + ',' + variable5);
      
        var sMSG = "";    		            
        //Shashank Starts-Date 11/04/2010		    
        /** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
        var msg ="Giro Setup-BankName,Giro Setup-BranchCode,Giro Setup-BankAccNo,Giro Setup-Account Name";
        var srcData ="";        
        var srcCtrl =variable1.id +','+variable3.id +',' + variable4.id + ',' + variable5.id;        
        
        //Check If  BankName is DBS/POSB then Company Provided By Bank is Mandatory         
        var selIndex = variable1.selectedIndex;             
        var comboValue = variable1.options[selIndex].value;  
         
        if(comboValue=="4")
        { 
          msg=msg+",Giro Setup-Company Code provided by Bank";
          srcCtrl=srcCtrl +","+variable6.id;
        }
        
        if(comboValue=="9")
        { 
          msg=msg+",Giro Setup-Approver Code provided by Bank,Giro Setup-Operator Code provided by Bank";
          srcCtrl=srcCtrl +","+variable7.id+","+variable8.id;
        }
        
        var strirmsg = validateData(srcCtrl,'','MandatoryAll',srcData,msg,'');
        if(strirmsg!="")
        {
            sMSG = "Following fields are missing.<br/> <br/>" + strirmsg + "<br/>";
        }
        
        //Check For Postal code Numeric Only    
        strirmsg="";
        //strirmsg = CheckNumeric(variable2.value,"Giro Setup-Day");
        //if(strirmsg!="")                        
        //    sMSG += strirmsg + "<br/>";
            
        //Check For Postal code AlphaNumeric Only    
        strirmsg="";
        strirmsg = alphanumeric(variable3,"Giro Setup-Branch Code");
        if(strirmsg!="")                        
            sMSG += strirmsg + "<br/>";
        if ($(document.getElementById("<%= txtbranch.ClientID %>")).val() != "")
            if ($(document.getElementById("<%= txtbranch.ClientID %>")).val().length < 3)
                sMSG += "Giro Setup-Branch Code character length cannot be less than 3 !<br/>";
        //Check For Postal code AlphaNumeric Only    
        strirmsg="";
        strirmsg = alphanumeric(variable4,"Giro Setup-Bank AccNo");
        if(strirmsg!="")                        
            sMSG += strirmsg + "<br/>";
        if ($(document.getElementById("<%= txtbankaccno.ClientID %>")).val() != "")
            if ($(document.getElementById("<%= txtbankaccno.ClientID %>")).val().length < 10)
                sMSG += "Giro Setup-Bank AccNo character length cannot be less than 10 !<br/>";

        //Check For Postal code AlphaNumeric Only    
        strirmsg="";
        strirmsg = alphanumeric(variable5,"Giro Setup-Account Name");
        if(strirmsg!="")                        
            sMSG += strirmsg + "<br/>";
         
         
         //Show Message Box
         if(sMSG!="")
         {
            WarningNotification(sMSG);
            return false;  
         }else
         {   
            return true;  
        }
    }
</script>

        