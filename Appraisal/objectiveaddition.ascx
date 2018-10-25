<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="objectiveaddition.ascx.cs" Inherits="SMEPayroll.Appraisal.objectiveaddition" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radU" %>


<div class="clearfix form-style-inner">
    <div class="col-sm-12 text-center margin-top-30">
        <asp:Label ID="Label1" CssClass="form-title" runat="server" Text='<%# ((bool)DataBinder.Eval(Container, "OwnerTableView.IsItemInserted"))  ? "Adding a New Objective" : "Editing Objective" %>'
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
                    <label class="col-sm-2 control-label">Title <tt class="required">*</tt></label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txttitle" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Title")%>'
                            CssClass="form-control input-inline input-sm input-medium">
                        </asp:TextBox>
                        <asp:Label ID="lblEr" ForeColor="Brown" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">
                        <asp:Label ID="Tra" runat="server">Date</asp:Label>
                    </label>
                    <div class="col-sm-10">
                        <asp:Label ID="dtFrom" runat="server">From</asp:Label>
                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker1" runat="server" Width="100px" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.FromDate")%>'>
                            <DateInput ID="DateInput1" runat="server" Skin="" CssClass="form-control input-xsmall input-sm" DateFormat="dd-MM-yyyy">
                            </DateInput>
                        </radCln:RadDatePicker>

                        <asp:Label ID="dtTo" runat="server">to</asp:Label>

                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePicker2" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.ToDate")%>'
                                                        runat="server" Width="102px">
                            <DateInput ID="DateInput2" runat="server" Skin="" CssClass="form-control input-xsmall input-sm" DateFormat="dd-MM-yyyy">
                            </DateInput>
                        </radCln:RadDatePicker>
                    </div>
                </div>


                <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Description</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDescription" CssClass="form-control input-inline input-sm input-medium" Font-Names="Tahoma" Font-Size="11px" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container,"DataItem.Description")%>'
                            Wrap="true" runat="server"></asp:TextBox>
                    </div>
                </div>

                  <div class="form-group clearfix">
                    <label class="col-sm-2 control-label">Status</label>
                    <div class="col-sm-10">
                        <asp:DropDownList ID="drstatus" DataTextField='<%# DataBinder.Eval(Container,"DataItem.Status")%>' runat="server" CssClass="form-control input-inline input-sm input-medium">
                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                            <asp:ListItem Value="Complete">Complete</asp:ListItem>
                            <asp:ListItem Value="Incomplete">Incomplete</asp:ListItem>
                        </asp:DropDownList>
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
<%--<div class="col-sm-12">
            <asp:Label ID="Label2" style="font-size: 20px;" Text="For Rating the performance please click on the Info button of this Objective" ForeColor="Green" runat="server" Visible="true"></asp:Label>
   
    </div>--%>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
    ShowMessageBox="True" ShowSummary="False" />
&nbsp;

<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePicker2"
    Display="None" ErrorMessage="Apply Claims - To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePicker1"
    Display="None" ErrorMessage="Apply Claims - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
    ErrorMessage="Add Objectives - Employee Name Required!" InitialValue="-select-" ControlToValidate="drpemployee"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
    ErrorMessage="Add Objectives - Title Required!" ControlToValidate="txttitle"></asp:RequiredFieldValidator>
<asp:CompareValidator ID="cmpEndDate" runat="server"  
 ErrorMessage="Add Objectives : To date cannot be less than start date" 
 ControlToCompare="RadDatePicker1" ControlToValidate="RadDatePicker2" 
 Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>  


<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script language="javascript" src="../Frames/Script/CommonValidations.js">
    </script>
  

</telerik:RadCodeBlock>




<script type="text/javascript">
    window.onload = function () {
        $(".ruFileWrap .ruFakeInput").addClass("form-control input-inline input-sm input-medium");
    };
</script>

