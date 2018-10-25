<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditTeam.ascx.cs" Inherits="SMEPayroll.Cost.EditTeam" %>
<%@ Register TagPrefix="radG" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<div class="clearfix form-style-inner">
    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding  New Group" : "Editing  Group Details" %>'></asp:Label>
    </div>

    
        <hr />
    
    
        <div class="form-inline">
            <div class="form-body">
                <div class="form-group clearfix">
                    <label class="control-label">Team Name</label>
                    <asp:TextBox ID="txtTeamname" CssClass="form-control input-inline input-sm input-medium cleanstring custom-maxlength _txtteamname" MaxLength="50" runat="server" Text='<%# DataBinder.Eval( Container, "Dataitem.TeamName" ) %>'></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Team Lead</label>
                    <asp:DropDownList ID="drpTeamLead" CssClass="form-control input-inline input-sm input-medium _drpteamlead"
                        runat="server"  OnDataBound="drpTeamLead_DataBound"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0" runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return ValidateData()" />
                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" />
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
    function ValidateData() {



        <%--        var variable1 = document.getElementById("<%= txtTeamname.ClientID %>");
            var variable2 = document.getElementById("<%= drpTeamLead.ClientID%>");

            if (variable1.value == "") {
                alert("Please Enter Team Name");
                return false;
            }

            else if (variable2.value == "0") {
                alert("Please Select Team Lead");
                return false;
            }
            return true;--%>
        var _message = "";
        if ($.trim($("._txtteamname").val()) === "")
            _message += "Please Input Team Name <br>";
        if ($.trim($("._drpteamlead option:selected").val()) === "0")
            _message += "Please Select Team Lead";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;

    }
   </script>
