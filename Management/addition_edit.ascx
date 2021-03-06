<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="addition_edit.ascx.cs"
    Inherits="SMEPayroll.Management.addition_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

<div class="clearfix form-style-inner">

    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding a New Addition Type Record" : "Editing Addition Type Record" %>'
            Width="100%"></asp:Label>
    </div>



    <hr />






    
        <div class="form form-inline">
            <div class="form-body">

                <div class="form-group clearfix col-sm-12 no-padding">
                    <asp:RadioButtonList ID="rbllist" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnDataBound="rbllist_DataBound" OnSelectedIndexChanged="rbllist_SelectedIndexChanged">
                        <asp:ListItem Selected="True">General</asp:ListItem>
                        <asp:ListItem>Claim</asp:ListItem>
                        <asp:ListItem>Variable</asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div class="form-group clearfix">
                    <label class="control-label">
                        <asp:Label ID="lblVariable" runat="server" Text="Variable Type"></asp:Label></label>

                    <asp:TextBox Enabled="false" ID="txtCode" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.code")%>'
                        CssClass="textfields form-control input-inline input-sm input-small"></asp:TextBox>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Description</label>

                    <asp:TextBox ID="txtaddtype" class="textfields form-control input-inline input-sm input-small cleanstring custom-maxlength" MaxLength="25" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.desc")%>'></asp:TextBox>
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtaddtype"
                        Display="None" ErrorMessage="Please Enter Addition Type">*</asp:RequiredFieldValidator>--%>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Account Code</label>

                    <asp:TextBox ID="txtAccountCode" class="textfields form-control input-inline input-sm input-small cleanstring custom-maxlength" MaxLength="20" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.AccountCode")%>'></asp:TextBox>

                </div>

                <% if (Session["Country"].ToString() != "383")
                    { %>
                <div class="form-group clearfix">
                    <label class="control-label">Attract CPF Gross</label>

                    <asp:DropDownList ID="drpcpf" class="textfields form-control input-inline input-sm input-small" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="drpcpf_SelectedIndexChanged">
                        <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                        <asp:ListItem Text="No" Value="No"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">
                        <asp:Label ID="lblTypeOfAddition" runat="server" Text="Type of Wages"></asp:Label></label>

                    <asp:DropDownList ID="drpWage" class="textfields form-control input-inline input-sm input-small" runat="server">
                        <asp:ListItem Text="Ordinary Wage" Value="O"></asp:ListItem>
                        <asp:ListItem Text="Additional Wage" Value="A"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%} %>

                <div class="form-group clearfix">
                    <table width="100%">
                        <tr id="trFormula" runat="server">
                            <td>
                                <label class="control-label">
                                    <asp:Label ID="lblFormula" runat="server" Text="Formula"></asp:Label></label>

                                <asp:DropDownList ID="drpfor" runat="server" class="textfields form-control input-inline input-sm input-small" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpfor_SelectedIndexChanged">
                                    <asp:ListItem Text="Day" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Time" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="N/A" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">
                        <asp:Label ID="lblFormulaCalc" runat="server"></asp:Label></label>

                    <asp:TextBox ID="txtFormulaCalc" class="textfields form-control input-inline input-sm input-small" Visible="false" runat="server"
                        Text='<%# DataBinder.Eval(Container,"DataItem.formulacalc")%>'></asp:TextBox>
                    <asp:Label ID="intimetxt" runat="server" Visible="false">Intime</asp:Label>
                    <asp:TextBox ID="txtintime" class="textfields form-control input-inline input-sm input-small" Visible="false" runat="server"
                        Text='<%# DataBinder.Eval(Container,"DataItem.InTime")%>'></asp:TextBox>
                    <asp:Label ID="outtimetxt" runat="server" Visible="false">OutTime</asp:Label>
                    <asp:TextBox ID="textouttime" class="textfields form-control input-inline input-sm input-small" Visible="false" runat="server"
                        Text='<%# DataBinder.Eval(Container,"DataItem.OutTime")%>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvfcal" runat="server" ControlToValidate="txtintime"
                        Display="None" ErrorMessage="Please Enter Time in format hh:mm" Visible="false">*</asp:RequiredFieldValidator>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Payment in Petty Cash</label>

                    <asp:DropDownList ID="drpPCash" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server" OnSelectedIndexChanged="changedispalyvalue" AutoPostBack="true">
                        <asp:ListItem Value="2" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="1" Text="No"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="pettycashLBL" Text="" runat="server"></asp:Label>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Prorated</label>

                    <asp:DropDownList ID="ProRatedDropDownList" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server" OnSelectedIndexChanged="drpprodate_selectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Factor</label>

                    <asp:DropDownList ID="leaveDeductList" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server" Enabled="false">
                        <asp:ListItem Value="0" Text="NA"></asp:ListItem>
                        <asp:ListItem Value="1" Text="All Leave"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Unpaid Leave Only"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group clearfix">
                    <label class="control-label">
                        <asp:Label ID="lblShared" Text="Shared" runat="server"></asp:Label></label>

                    <asp:DropDownList ID="drpShared" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group clearfix">
                    <table width="100%">
                        <tr runat="server" id="tr3">
                            <td>
                                <label class="control-label">Attract SDL Gross</label>

                                <asp:DropDownList ID="SDLDropDownList" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="form-group clearfix">
                    <table width="100%">
                        <tr runat="server" id="tr4">
                            <td>
                                <label class="control-label">Attract Fund Gross</label>

                                <asp:DropDownList ID="FundDropDownList" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                                    <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Is Additional Payment</label>

                    <asp:DropDownList ID="drpIsAdditionalPayment" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>

                    </asp:DropDownList>

                </div>

                <% if (Session["Country"].ToString() != "383")
                    { %>
                <div class="form-group clearfix">
                    <label class="control-label">Attract Taxable Gross</label>

                    <asp:DropDownList ID="drptax_payable" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                        <asp:ListItem Value="No" Text="No"></asp:ListItem>
                        <asp:ListItem Value="Yes" Text="Yes"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <%} %>

                <div class="form-group clearfix">
                    <label class="control-label">Is Include EnCash</label>

                    <asp:DropDownList ID="drpGrosspay" DataTextField="text" class="textfields form-control input-inline input-sm input-small" runat="server">
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="form-group clearfix">
                    <table width="100%">
                        <tr runat="server" id="tr1">
                            <td>
                                <label class="control-label">Tax Group</label>

                                <asp:DropDownList ID="drptax_payable_options" DataTextField="text" DataValueField="id"
                                    DataSourceID="XmldtTaxPayableType" class="textfields form-control input-inline input-sm input-small" runat="server">
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Active</label>

                    <asp:DropDownList ID="ddlActive" DataTextField="text" class="textfields form-control input-inline input-sm input-medium" runat="server">
                        <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                        <asp:ListItem Value="0" Text="No"></asp:ListItem>
                    </asp:DropDownList>

                </div>


            </div>


            <div class="form-actions text-center">
                <asp:Button ID="btnUpdate" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                    Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>' OnClientClick="return ValidationData();"
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
                            <asp:TextBox ID="txtForm" runat="server" Text="" CssClass="textfields form-control input-inline input-sm input-small"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
    





<asp:XmlDataSource ID="XmldtTaxPayableType" runat="server" DataFile="~/XML/xmldata.xml"
    XPath="SMEPayroll/Tax/TaxPayableType"></asp:XmlDataSource>

</div>
<script type="text/javascript">

    function ValidationData() {
        var _message = "";
        if ($.trim($(document.getElementById("<%= txtaddtype.ClientID %>")).val()) === "")
            _message += "Please Input Addition Type. <br/>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;
    }
</script>
