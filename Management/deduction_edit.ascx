<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="deduction_edit.ascx.cs"
    Inherits="SMEPayroll.Management.deduction_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<script language="javascript" type="text/javascript">
    function isNumericKeyStrokeDecimal(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
            return false;

        return true;
    }


</script>


<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Deduction Type Record" : "Editing Deduction Type Record" %>'
            Width="100%"></asp:Label>
    </div>



    <hr />




    <div class="form form-inline">
        <div class="form-body">

            <div class="form-group clearfix">
                <label class="control-label">Deduction Type</label>

                <asp:TextBox ID="txtaddtype" class="textfields form-control input-inline input-sm input-medium cleanstring custom-maxlength" MaxLength="25" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.desc")%>'></asp:TextBox>
                <%--         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtaddtype"
                            Display="None" ErrorMessage="Please Enter Deduction Type">*</asp:RequiredFieldValidator>--%>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Account Code</label>

                <asp:TextBox ID="txtAccountCode" class="textfields form-control input-inline input-sm input-medium cleanstring custom-maxlength" MaxLength="20" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AccountCode")%>'></asp:TextBox>

            </div>

            <% if (Session["Country"].ToString() != "383")
                { %>
            <div class="form-group clearfix">
                <label class="control-label">Attract CPF Gross</label>

                <asp:DropDownList ID="drpcpf" class="textfields form-control input-inline input-sm input-medium" runat="server"
                    AutoPostBack="true">
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <%} %>


            <div class="form-group clearfix">
                <label class="control-label">
                    <asp:Label ID="lblShared" Text="Shared:" runat="server"></asp:Label></label>

                <asp:DropDownList ID="drpShared" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Formula</label>

                <asp:DropDownList ID="drpfor" runat="server" class="textfields form-control input-inline input-sm input-medium" AutoPostBack="True"
                    OnSelectedIndexChanged="drpfor_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="% of Gross" Value="1"></asp:ListItem>
                </asp:DropDownList>



            </div>
            <div class="form-group clearfix">
                <asp:Label ID="lblFormulaCalc" Visible="false" runat="server" CssClass="control-label">Formula Value</asp:Label>
                <asp:TextBox ID="txtFormulaCalc" class="textfields form-control input-inline input-sm input-medium" Visible="false" runat="server"
                    Width="101px" onkeypress="return isNumericKeyStrokeDecimal(event);"></asp:TextBox>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Attract Fund Gross</label>

                <asp:DropDownList ID="FundDropDownList" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Prorated</label>

                <asp:DropDownList ID="ProratedDrop" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server" OnSelectedIndexChanged="ProratedDrop_selectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                </asp:DropDownList>

            </div>
             <div class="form-group clearfix">
                <label class="control-label">Factor</label>

                <asp:DropDownList ID="leaveDeductList" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server" Enabled="false">
                    <asp:ListItem Value="0" Text="NA"></asp:ListItem>
                    <asp:ListItem Value="1" Text="All Leave"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Unpaid Leave Only"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Attract SDL Gross</label>

                <asp:DropDownList ID="SDLDropDownList" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList>

            </div>
           

            <% if (Session["Country"].ToString() != "383")
                { %>
            <div class="form-group clearfix">
                <label class="control-label">Attract Taxable Gross</label>

                <asp:DropDownList ID="drpTax" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                    <asp:ListItem Value="No" Text="No"></asp:ListItem>
                    <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <div class="form-group clearfix">
                <label class="control-label">Active :</label>

                <asp:DropDownList ID="ddlActive" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                </asp:DropDownList>

            </div>
            <%} %>
        </div>


        <div class="form-actions text-center">
            <asp:Button ID="btnUpdate" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return Validations();"
                runat="server" CssClass="btn red" />
            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                Text="Cancel" CssClass="btn default" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                ShowMessageBox="True" ShowSummary="False" />
        </div>

        <div style="display: none">
            <table width="100%">
                <tr runat="server" id="tr2">
                    <td>
                        <asp:TextBox ID="txtForm" runat="server" Text=""></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>

    </div>






    <asp:XmlDataSource ID="XmldtTaxPayableType" runat="server" DataFile="~/XML/xmldata.xml"
        XPath="SMEPayroll/Tax/TaxPayableType"></asp:XmlDataSource>

</div>
<script type="text/javascript">

    function Validations() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= txtaddtype.ClientID %>")).val()) === "")
            _message += "Please Input Deduction Type. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
