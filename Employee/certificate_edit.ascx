<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="certificate_edit.ascx.cs" Inherits="SMEPayroll.Employee.certificate_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>

<div class="clearfix form-style-inner">
    <div class="heading">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Adding a New Record" : "Editing Record" %>'
                   Width="100%"></asp:Label>
    </div>


    <hr />


    <div class="form form-inline">
        <div class="form-body">
            <div class="form-group clearfix">
                <label class="control-label">Category Name</label>
                <asp:DropDownList ID="drpcatname" runat="server" CssClass="form-control input-inline input-sm input-medium" AutoPostBack="True">
                </asp:DropDownList>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Certificate Number</label>
                <asp:TextBox ID="CertificateNumber" runat="server" CssClass="form-control input-inline input-sm input-medium custom-maxlength cleanstring" MaxLength="50" Text='<%# DataBinder.Eval(Container,"DataItem.CertificateNumber")%>'>
                </asp:TextBox>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Test Date <tt class="required">*</tt></label>
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false"
                                      DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.TestDate")%>' ID="TestDate"
                                      runat="server" Width="105px" CssClass="inline-block">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>

                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                </radCln:RadDatePicker>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Issue Date <tt class="required">*</tt></label>
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false"
                                      DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.IssueDate")%>' ID="IssueDate"
                                      runat="server" Width="105px" CssClass="inline-block">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                </radCln:RadDatePicker>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Expiry Date <tt class="required">*</tt></label>
                <radCln:RadDatePicker Calendar-ShowRowHeaders="false"
                                      DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.ExpiryDate")%>' ID="ExpiryDate"
                                      runat="server" Width="105px" CssClass="inline-block">
                    <Calendar runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                </radCln:RadDatePicker>
            </div>
            <div class="form-group clearfix">
                <label class="control-label"> Issue Location</label>
                <asp:TextBox ID="IssueLocation" CssClass="form-control input-inline input-sm input-medium custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IssueLocation")%>'>
                </asp:TextBox>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Issued By</label>
                <asp:TextBox ID="IssuedBy" CssClass="form-control input-inline input-sm input-medium custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.IssuedBy")%>'>
                </asp:TextBox>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Remarks</label>
                <asp:TextBox ID="remarks" runat="server" CssClass="form-control input-inline input-sm input-medium custom-maxlength" MaxLength="50" Text='<%# DataBinder.Eval(Container,"DataItem.remarks")%>'>
                </asp:TextBox>
            </div>
            <div class="form-group clearfix">
                <label class="control-label">Certificate Upload</label>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="file-inline" />
            </div>
        </div>

        <div class="form-actions text-center">
            <asp:Button ID="btnUpdate" runat="server" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                        Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                        CssClass="btn red" />

            <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                        Text="Cancel" CssClass="btn default" />
        </div>
    </div>




</div>

<radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <radA:AjaxSetting AjaxControlID="drpaddtype">
            <UpdatedControls>
                <radA:AjaxUpdatedControl ControlID="lblcpf"></radA:AjaxUpdatedControl>
            </UpdatedControls>
        </radA:AjaxSetting>
    </AjaxSettings>
</radA:RadAjaxManager>
<%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                           ShowMessageBox="false" ShowSummary="False" />--%>
&nbsp;
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtamt"
                                Display="None" ErrorMessage="Apply Claims - Amount Required" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtamt"
                    Display="None" ErrorMessage="Apply Claims -Amount Is Invalid MaximumValue=1000000,MinimumValue=0" MaximumValue="1000000" MinimumValue="0"
                    Type="Double"></asp:RangeValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
                            Display="None" ErrorMessage="Apply Claims - To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
                            Display="None" ErrorMessage="Apply Claims - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                            ErrorMessage="Apply Claims - Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                            ErrorMessage="Apply Claims - Addition Type Required!" InitialValue="-select-" ControlToValidate="drpaddtype"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="cmpEndDate" runat="server"
                      ErrorMessage="Apply Claims : To date cannot be less than start date"
                      ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2"
                      Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>  --%>


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script language="javascript" src="../Frames/Script/CommonValidations.js">
    </script>
    <script language="javascript" type="text/javascript">

        $(window).load(function () {
            $(".rcTable").css({ "width": "105px" });
            
        });


            $(document.getElementById("<%= btnUpdate.ClientID %>")).click(function () {
                var alertmsg = "";
                if($(document.getElementById("<%= TestDate.ClientID %>")).val() == "")
                {
                    alertmsg = "Please enter Test Date  <br/>";
                }
                if ($(document.getElementById("<%= IssueDate.ClientID %>")).val() == "") {
                    alertmsg += "Please enter Issue Date    <br/>";
                }
                if ($(document.getElementById("<%= ExpiryDate.ClientID %>")).val() == "") {
                    alertmsg += "Please enter Expiry Date    <br/>";
                }
                if (alertmsg != "") {
                    WarningNotification(alertmsg);
                    return false;
                }

            });

    </script>
</telerik:RadCodeBlock>
