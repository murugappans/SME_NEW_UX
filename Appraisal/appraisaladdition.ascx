<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="appraisaladdition.ascx.cs" Inherits="SMEPayroll.Appraisal.appraisaladdition" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>


<div class="clearfix form-style-inner">
    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Schedule New Appraisal" : "Editing Appraisal Schedule" %>'
            Width="297px"></asp:Label>
    </div>



    <div class="col-sm-12">
        <hr />
    </div>




    <div class="col-sm-7">

        <div class="form">
            <div class="form-body">

                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Employee</label>
                    <div class="col-sm-10">
                        <radA:RadAjaxPanel ID="r1" runat="Server">
                            <asp:DropDownList ID="drpemployee" Enabled='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? (1==1) : !(1==1)%>'
                                runat="server" OnDataBound="drpemployee_DataBound" 
                                AutoPostBack="True" CssClass="form-control input-inline input-sm input-medium">
                            </asp:DropDownList>
                        </radA:RadAjaxPanel>
                    </div>
                </div>



               
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="Tra" runat="server">Due Date</asp:Label>
                    </label>
                    <div class="col-sm-10">
                        
                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server" Width="150px" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.DueDate")%>'>
                            <DateInput ID="DateInput1" runat="server" Skin="" CssClass="form-control input-sm" DateFormat="dd-MM-yyyy">
                            </DateInput>
                        </radCln:RadDatePicker>
                    </div>
                </div>


                
                 
           
            </div>

            <div class="form-actions">
                <asp:Button ID="btnUpdate" runat="server" CommandName='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "PerformInsert" : "Update" %>'
                    Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Insert" : "Update" %>'
                    CssClass="btn red" />

                <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="Cancel" CssClass="btn default" />
            </div>


        </div>

    </div>
    <div class="col-sm-5">

    </div>

    <div class="col-sm-12">
        <div class="form-group">
            <asp:Label ID="lblComid" runat="server" Width="0" Visible="false"></asp:Label>
        </div>
    </div>

</div>


<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
&nbsp;


<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="Add appraisal - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
    ErrorMessage="Add appraisal - Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>



<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script language="javascript" src="../Frames/Script/CommonValidations.js">
    </script>
  

</telerik:RadCodeBlock>




<script type="text/javascript">
    window.onload = function () {
        $(".ruFileWrap .ruFakeInput").addClass("form-control input-inline input-sm input-medium");
    };
</script>


