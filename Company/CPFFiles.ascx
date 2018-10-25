<%@ Control Language="C#" AutoEventWireup="true" Codebehind="CPFFiles.ascx.cs" Inherits="SMEPayroll.Company.CPFFiles" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="lblTitle" CssClass="form-title" runat="server" Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Adding the details" : "Editing the details" %>'
            Width="100%"></asp:Label>
    </div>

    <hr />


    <div class="form-inline">
        <div class="form-body">

            <div class="form-group clearfix">
                <label class="control-label">CSN</label>
               
                    <asp:TextBox ID="txtROC" Text='<%# DataBinder.Eval(Container,"DataItem.roc")%>' CssClass="padding-right-0 form-control input-sm input-small input-inline custom-maxlength cleanstring"
                        runat="server" MaxLength="12"></asp:TextBox>
                    <span class="dash-spacer input-inline">-</span> 
             
                    <asp:TextBox ID="txtType" Text='<%# DataBinder.Eval(Container,"DataItem.type")%>'
                        CssClass="padding-right-0 form-control input-sm input-xxs input-inline custom-maxlength cleanstring" runat="server" MaxLength="3"></asp:TextBox>
                    <span class="dash-spacer input-inline">-</span> 
             
                    <asp:TextBox ID="txtSlNo" Text='<%# DataBinder.Eval(Container,"DataItem.srno")%>'
                        CssClass="padding-right-0 form-control input-sm input-xxs input-inline custom-maxlength cleanstring" runat="server" MaxLength="2"></asp:TextBox>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">&nbsp;</label>
                    <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                        Text='<%# (Container as Telerik.Web.UI.GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                        OnClientClick="return ValidateCSNSetup()" CssClass="btn red margin-top-0" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel" CssClass="btn default margin-top-0" />
                </div>


        </div>
    </div>



</div>

<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtROC"
    Display="None" ErrorMessage="Company ROC" InitialValue=""></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtType"
    Display="None" ErrorMessage="Company Type" InitialValue=""></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSlNo"
    Display="None" ErrorMessage="Company Serial No." InitialValue=""></asp:RequiredFieldValidator>--%>
<%--
//Bug ID: 2
//Fix By: Santy Kumar
//Date  : June 5th 2009
//Remark: Fixed for the CPF 9 Digit and 10 Digit case. When 9 digit then add space in CPF File Generation
--%>
<%--<asp:RegularExpressionValidator ID="MinimumValue" runat="server" ControlToValidate="txtROC" 
ErrorMessage="Minimum length for Company CPF is 9" ValidationExpression=".{9}.*" Display="None" />
<%--//End   : 2--%>
<%--<asp:RegularExpressionValidator ID="AlphanuericROC" runat="server" ControlToValidate="txtROC"
    ErrorMessage="Company CPF No. should be Alphanumeric" ValidationExpression="\w+([-+.]\w+)*"
    Display="None" />
<asp:ValidationSummary HeaderText="The following fields are missing/incorrect:" ID="ValidationSummary1"
    runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />--%>
<script language="javascript" type="text/javascript">
    function ValidateCSNSetup() {
        var sMsg_new = "";
        // var sMsg=""; *
        var variable1 = document.getElementById("<%= txtROC.ClientID %>");
        var variable2 = document.getElementById("<%= txtType.ClientID %>");
        var variable3 = document.getElementById("<%= txtSlNo.ClientID %>");
        //Shashank Starts-Date 11/04/2010		    
        /** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
        var msg = "CSN Setup-Company ROC,CSN Setup-Company Type,CSN Setup-Company Serial No.";
        var srcData = "";
        var srcCtrl = variable1.id + ',' + variable2.id + ',' + variable3.id;
        var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
        if (strirmsg != "") {
            sMsg_new = "Following fields are missing.<br/>" + strirmsg + "<br/><br/>";
        }
        if ($(variable1).val().length < 8 && $(variable1).val().length > 0)
            sMsg_new += "Company ROC length should be minimum 9 Characters <br/>";
        if ($(variable2).val().length < 3 && $(variable2).val().length > 0)
            sMsg_new += "Company TYPE length should be minimum 3 Characters <br/>";
        if ($(variable3).val().length < 2 && $(variable3).val().length > 0)
            sMsg_new += "Company SLNO length should be minimum 2 Characters <br/>";
        //Check For Company ROC AlphaNumeric Only
        strirmsg = "";
        strirmsg = alphanumeric(variable1, "CSN Setup-Company ROC");
        if (strirmsg != "")
            sMsg_new += strirmsg + "<br/>";

        //Check For Company ROC AlphaNumeric Only
        strirmsg = "";
        strirmsg = alphanumeric(variable2, "CSN Setup-Company Type");
        if (strirmsg != "")
            sMsg_new += strirmsg + "<br/>";

        //Check For Company ROC AlphaNumeric Only
        strirmsg = "";
        strirmsg = alphanumeric(variable3, "CSN Setup-Company Serial No.");
        if (strirmsg != "")
            sMsg_new += strirmsg + "<br/>";

        //Check Data For Maximum Length 8 for CompanyCode
        //strirmsg = "";
        //strirmsg = CheckMaxMinLength(variable1.value.length, "8", "<=", "CSN Setup-Company CPF");
        //if (strirmsg != "")
        //    sMsg_new += strirmsg + "<br/>";

        if (sMsg_new != "") {
            WarningNotification(sMsg_new);
            return false;
        }
    }
</script>