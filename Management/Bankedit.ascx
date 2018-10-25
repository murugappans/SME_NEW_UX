<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Bankedit.ascx.cs" Inherits="SMEPayroll.Management.Bankedit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>



<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Record" : "Editing Record" %>'
            Width="100%"></asp:Label>
    </div>


    
        <hr />
    

    

        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">

                    <label class="control-label">Bank Code<tt class="required">*</tt></label>
                        <asp:TextBox ID="txtbankcode"  Text='<%# DataBinder.Eval( Container, "DataItem.bank_code" ) %>'
                        runat="server" class="textfields form-control inline input-sm input-medium numericonly" MaxLength="4"></asp:TextBox>
                   <%--<asp:RequiredFieldValidator ID="rfv1" runat="server" Display="None" ErrorMessage="Please Enter BankCode"
                    ControlToValidate="txtbankcode"></asp:RequiredFieldValidator>--%>
                    

                </div>

                <div class="form-group clearfix">

                    <label class="control-label">Bank Name<tt class="required">*</tt></label>
                    
                       <asp:TextBox ID="txtbankname" Text='<%# DataBinder.Eval( Container, "DataItem.desc" ) %>'
                        runat="server" class="textfields form-control inline input-sm input-medium cleanstring custom-maxlength" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="None" ErrorMessage="Please Enter BankName"
                    ControlToValidate="txtbankname"></asp:RequiredFieldValidator>                
                   

                    </div>

                    <div class="form-group clearfix">
                   <label class="control-label">&nbsp;</label>
                        <asp:CheckBox ID="chkishash" CssClass="bodytxt" runat="server" Text="Hash Validation" Checked='<%#  DataBinder.Eval( Container,  "DataItem.ishash")== DBNull.Value ? false : Convert.ToBoolean(DataBinder.Eval( Container,  "DataItem.ishash")) %>'></asp:CheckBox>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                     OnClientClick="return ValidateData();" />
                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
            </div>

            </div>


            

        </div>
    

   



    <asp:ValidationSummary ID="vsm1" runat="server" DisplayMode="List" ShowMessageBox="True"
        ShowSummary="False" />

</div>




<script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
</script>
<script type="text/javascript">
    //Validate data for Alphanumeric and Mandatory fields
    function ValidateData() {
            <%--var sMsg_new=""; 
            // var sMsg=""; *
            var variable1=document.getElementById("<%= txtbankcode.ClientID %>");   
            var variable2=document.getElementById("<%= txtbankname.ClientID%>");   
            
            //Shashank Starts-Date 11/04/2010		                
            var msg ="MNG Settings-BankCode,MNG Settings-BankName";
            var srcData ="";        
            var srcCtrl =variable1.id+","+ variable2.id;
            var strirmsg = validateData(srcCtrl,'','MandatoryAll',srcData,msg,'');
            if(strirmsg!="")
            {
               sMsg_new = "Following fields are missing.\n\n" + strirmsg + "\n";
            }        
            
            strirmsg="";
            strirmsg = CheckNumeric(variable1.value,"MNG Settings-BankCode");
            if(strirmsg!="")                        
                sMsg_new+=strirmsg+"\n";
                
            strirmsg="";
            strirmsg = alphanumeric(variable2,"MNG Settings-BankName");
            if(strirmsg!="")                        
                sMsg_new+=strirmsg+"\n";
            
            if(sMsg_new!="")
            {
                alert(sMsg_new);
                return false;  
            }else
            {   
                return true;  
            }--%>
        var _message = "";
        if ($.trim($(document.getElementById("<%= txtbankcode.ClientID %>")).val()) === "")
                _message += "Please Input Bank Code. <br/>";
            if ($.trim($(document.getElementById("<%= txtbankname.ClientID %>")).val()) === "")
            _message += "Please Input Bank Name. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
