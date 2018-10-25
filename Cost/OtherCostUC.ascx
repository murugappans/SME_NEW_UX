<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OtherCostUC.ascx.cs"
    Inherits="SMEPayroll.Cost.OtherCostUC" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%--<script type="text/javascript">

    function isNumericKeyStrokeDecimal(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
            return false;

        return true;
    }


    function ValidateQuot() {
        //Invoice Date
        var datePicker_Invoice = document.getElementById('<%=datePicker_Invoice.ClientID%>').value;
        if (datePicker_Invoice == '') {
            alert("Please Select the Invoice Date");
            return false;
        }
        //

        //VendorInvoiceNo
        var txtVendorInvoiceNo = document.getElementById('<%=txtVendorInvoiceNo.ClientID%>').value;
             if (txtVendorInvoiceNo == '') {
                 alert("Please Enter the Invoice No");
                 return false;
             }
        // 

        //VendorInvoiceNo
             var drpSupplier = document.getElementById('<%=drpSupplier.ClientID%>').value;
            if (drpSupplier == '-1') {
                alert("Please Select the Supplier");
                return false;
            }

        //Amount
            var TxtAmount = document.getElementById('<%=TxtAmount.ClientID%>').value;
            if (TxtAmount == '') {
                alert("Please Enter the Amount");
                return false;
            }

        //Cheque No
            var TxtCheque = document.getElementById('<%=TxtCheque.ClientID%>').value;
            if (TxtCheque == '') {
                alert("Please Enter the Cheque No");
                return false;
            }

        //Cheque Date
            var RadDatePicker_cheq = document.getElementById('<%=RadDatePicker_cheq.ClientID%>').value;
            if (RadDatePicker_cheq == '') {
                alert("Please select the Cheque Date");
                return false;
            }

        //Project
            var drpProject = document.getElementById('<%=drpProject.ClientID%>').value;
            if (drpProject == '-1') {
                alert("Please select the Project");
                return false;
            }

        //QuotationNo
            var TxtQuoation = document.getElementById('<%=TxtQuoation.ClientID%>').value;
            if (TxtQuoation == '') {
                alert("Please Enter the QuotationNo");
                return false;
            }

        //TxtIntInvoice
            var TxtIntInvoice = document.getElementById('<%=TxtIntInvoice.ClientID%>').value;
            if (TxtIntInvoice == '') {
                alert("Please Enter the Internal Invoice");
                return false;
            }

        //Category
            var drpCategory = document.getElementById('<%=drpCategory.ClientID%>').value;
            if (drpCategory == '-1') {
                alert("Please select the Category");
                return false;
            }

            return true;
        }
 </script>--%>

<div class="clearfix form-style-inner">
    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Adding Other Cost" : "Editing Other Cost" %>'
            runat="server"></asp:Label>
    </div>

    
        <hr />
    
    
        <div class="form form-inline">
            <div class="form-body">
                <div class="form-group clearfix">
                    <label class="control-label">Invoice Date</label>
                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="datePicker_Invoice" runat="server">
                        <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                        <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="riTextBox-custom _txtinvoicedate" />
                    </radG:RadDatePicker>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Vendor InvoiceNo</label>
                    <asp:TextBox ID="txtVendorInvoiceNo" CssClass="form-control input-inline input-sm input-small cleanstring custom-maxlength _txtvendorinvno" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Supplier</label>
                    <asp:DropDownList ID="drpSupplier" runat="server" CssClass="form-control input-inline input-sm input-small _drpsupplier">
                    </asp:DropDownList>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Description</label>
                    <asp:TextBox ID="TxtDescr"  CssClass="form-control input-inline input-sm input-small custom-maxlength" MaxLength="250" runat="server" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Amount</label>
                    <asp:TextBox ID="TxtAmount" CssClass="form-control input-inline input-sm input-small text-right number-dot _txtamnt" data-type="currency" MaxLength="12" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Cheque No</label>
                    <asp:TextBox ID="TxtCheque" CssClass="form-control input-inline input-sm input-small cleanstring custom-maxlength _txtchequeno" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">ChequeDate</label>
                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker_cheq" runat="server">
                        <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                        <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="riTextBox-custom _txtchequedate" />
                    </radG:RadDatePicker>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Project</label>
                    <asp:DropDownList ID="drpProject" runat="server" CssClass="form-control input-inline input-sm input-small _drpproject">
                    </asp:DropDownList>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">QuotationNo</label>
                    <asp:TextBox ID="TxtQuoation" CssClass="form-control input-inline input-sm input-small cleanstring custom-maxlength _txtqootno" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Internal Invoice</label>
                    <asp:TextBox ID="TxtIntInvoice" CssClass="form-control input-inline input-sm input-small cleanstring custom-maxlength _txtinternalinvoice" MaxLength="50" runat="server"></asp:TextBox>
                </div>
                <div class="form-group clearfix">
                    <label class="control-label">Category</label>
<asp:DropDownList ID="drpCategory" runat="server" CssClass="form-control input-inline input-sm input-small _drpcategory">
                    </asp:DropDownList>
                </div>                
            </div>

            <div class="form-actions text-center">
                <asp:Button ID="btnUpdate" CssClass="btn red" runat="server" OnClientClick="return ValidateQuot();"
                    CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                    Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                     />
                <asp:Button ID="btnCancel" CssClass="btn default" runat="server" CausesValidation="False"
                    CommandName="Cancel" Text="Cancel"  />
            </div>
        </div>
    
</div>
<script type="text/javascript">
    function ValidateQuot() {
        var _message = "";
        if ($.trim($("._txtinvoicedate").val()) === "")
            _message += "Please Select Invoice Date. <br>";
        if ($.trim($("._txtvendorinvno").val()) === "")
            _message += "Please Input Vendor InvoiceNo. <br>";
        if ($.trim($("._drpsupplier option:selected").text()) === "--Select--")
            _message += "Please Select Supplier. <br>";
        if ($.trim($("._txtamnt").val()) === "")
            _message += "Please Input Amount. <br>";
        if ($.trim($("._txtchequeno").val()) === "")
            _message += "Please Input Cheque No. <br>";
        if ($.trim($("._txtchequeno").val()).length < 6 && $.trim($("._txtchequeno").val()).length > 0)
            _message += "Cheque No length should be minimum 6 Characters" + "<br>";
        if ($.trim($("._txtchequedate").val()) === "")
            _message += "Please Select Cheque Date. <br>";
        if ($.trim($("._drpproject option:selected").text()) === "--Select--")
            _message += "Please Select Project. <br>";
        if ($.trim($("._txtqootno").val()) === "")
            _message += "Please Input QuotationNo. <br>";
        if ($.trim($("._txtinternalinvoice").val()) === "")
            _message += "Please Input Internal Invoice. <br>";
        if ($.trim($("._drpcategory option:selected").text()) === "--Select--")
            _message += "Please Select Category. <br>";
        if (_message != "") {
            WarningNotification(_message);
            return false;
        }
        return true;

    }
   </script>
