<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupTemplate.ascx.cs" Inherits="SMEPayroll.Users.GroupTemplate" %>
<%@ Register TagPrefix="radG" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding  New Group" : "Editing  Group Details" %>'
            Width="100%"></asp:Label>
    </div>
    
        <hr />
    
    
        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label>GroupName</label>
                        <asp:TextBox ID="txtgroupname" runat="server"
                            Font-Names="Verdana" Font-Size="8pt" Text='<%# DataBinder.Eval( Container, "Dataitem.GroupName" ) %>'
                    CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength" MaxLength="50"></asp:TextBox>
                    
                </div>
                <div class="form-group clearfix">
                    <asp:Button ID="btnUpdate" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                        Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return ValidateData()" CssClass="btn red margin-top-0" />
                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel" CssClass="btn default margin-top-0" />
                    </div>
                
            </div>
        </div>


    

    </div>

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>
<script type="text/javascript">
    function RowDblClick(sender, eventArgs) {
        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
    }
    //Validate data for Alphanumeric and Mandatory fields
    function ValidateData() {
        <%--        var sMsg_new = "";
        // var sMsg=""; *
        var variable1 = document.getElementById("<%= txtgroupname.ClientID %>");
            //Shashank Starts-Date 11/04/2010		    
            /** Mandatory Fields Based Upon Simple No Value OR Combobox Values Like NA OR -SELECT-*/
            var msg = "MNG Security-GroupName";
            var srcData = "";
            var srcCtrl = variable1.id;
            var strirmsg = validateData(srcCtrl, '', 'MandatoryAll', srcData, msg, '');
            if (strirmsg != "") {
                sMsg_new = "Following fields are missing.\n\n" + strirmsg + "\n";
            }
            //Check For Company ROC AlphaNumeric Only
            strirmsg = "";
            strirmsg = alphanumeric(variable1, "MNG Security-GroupName");
            if (strirmsg != "")
                sMsg_new += strirmsg + "\n";

            if (sMsg_new != "") {
                alert(sMsg_new);
                return false;
            } else {
                return true;
            }--%>
       var _message = "";
        if ($.trim($(document.getElementById("<%= txtgroupname.ClientID %>")).val()) === "")
            _message += "Please Input GroupName. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
    </script>
