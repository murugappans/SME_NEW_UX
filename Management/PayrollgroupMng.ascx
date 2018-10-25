<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PayrollgroupMng.ascx.cs"
    Inherits="SMEPayroll.Management.PayrollgroupMng" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label  ID="IdLabel" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container,"DataItem.id")%>'>
</asp:Label>
    </div>

    

        <div class="form-inline">
            <div class="form-body">
                <div class="form-group clearfix">
                    <label class="control-label">Name</label>
                        <asp:TextBox ID="GroupNameTextBox" Text='<%# DataBinder.Eval(Container,"DataItem.GroupName")%>'
                            class="textfields form-control input-sm inline input-medium cleanstring custom-maxlength _txtcourse" MaxLength="50" runat="server"></asp:TextBox>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">WorkflowType</label>
                        <asp:DropDownList ID="Workflowtypedrp" runat="server"  class="textfields form-control input-sm inline input-medium">
                            <asp:ListItem Value="1">Payroll</asp:ListItem>
                            <asp:ListItem Value="2">Leave</asp:ListItem>
                            <asp:ListItem Value="3">Claims</asp:ListItem>
                            <asp:ListItem Value="4">TimeSheet</asp:ListItem>
                            <asp:ListItem Value="5">Appraisal</asp:ListItem>
                        </asp:DropDownList>
                    
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                     <asp:Button ID="btnUpdate" runat="server" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "PerformInsert" : "Update" %>'
                    Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted")) ? "Insert" : "Update" %>' OnClientClick="return ValidationData();"
                  CssClass="btn red margin-top-0" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default margin-top-0" />
                    </div>

            </div>

            
        </div>
    

    
</div>
<script type="text/javascript">

    function ValidationData() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= GroupNameTextBox.ClientID %>")).val()) === "")
            _message += "Please Input Payroll Group Name. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
