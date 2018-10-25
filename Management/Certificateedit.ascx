<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Certificateedit.ascx.cs" Inherits="SMEPayroll.Management.Certificateedit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Certificate Category Record" : "Editing Certificate Category Record" %>'
            Width="100%"></asp:Label>
    </div>


    
        <hr />
    

    

        <div class="form-inline">
            <div class="form-body">

                <div class="form-group clearfix">

                    <label class="control-label">Category Name</label>
                    
                        <asp:TextBox ID="txtCategoryName" class="textfields form-control inline input-sm input-medium cleanstring custom-maxlength" MaxLength="25" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Category_Name")%>'  ></asp:TextBox>
                     <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName"
                            Display="None" ErrorMessage="Please Enter Category Name">*</asp:RequiredFieldValidator>--%>
                    

                </div>

                <div class="form-group clearfix">

                    <label class="control-label">Expiry Type</label>
                    
                        <asp:DropDownList ID="drpExpriy" DataTextField="ExpTypeName" DataValueField="id"
                            DataSourceID="SqlDataSource1" class="textfields form-control inline input-sm input-medium" runat="server">
                        </asp:DropDownList>
                    

                </div>

                <div class="form-group clearfix">
                    <label class="control-label">&nbsp;</label>
                <asp:Button ID="btnUpdate" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return Validations();"
                    runat="server" CssClass="btn red margin-top-0" />
                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default margin-top-0" />
            </div>

            </div>


            

        </div>
    

    


    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="True" ShowSummary="False" />

    <table>
        <tr runat="server" id="tr2" style="display: none">
            <td colspan="2">
                <asp:TextBox ID="txtForm" runat="server" Text=""></asp:TextBox>
            </td>
        </tr>

    </table>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="Select 3 ID, 'Passes Expiry' ExpTypeName Union All Select 4 ID, 'Passport Expiry' ExpTypeName Union All Select 6 ID, 'Insurance Expiry' ExpTypeName Union All Select 5 ID, 'CSOC Expiry' ExpTypeName Union All Select 9 ID, 'Others Expiry' ExpTypeName Union All Select 10 ID, 'License Expiry' ExpTypeName"></asp:SqlDataSource>
    <%--    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select 6 ID, 'Passes Expiry' ExpTypeName Union All Select 3 ID, 'Passport Expiry' ExpTypeName Union All Select 1 ID, 'Insurance Expiry' ExpTypeName Union All Select 2 ID, 'CSOC Expiry' ExpTypeName Union All Select 7 ID, 'Others Expiry' ExpTypeName Union All Select 5 ID, 'License Expiry' ExpTypeName">
                </asp:SqlDataSource>--%>
</div>
<script type="text/javascript">

    function Validations() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= txtCategoryName.ClientID %>")).val()) === "")
            _message += "Please Input Category Name. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
