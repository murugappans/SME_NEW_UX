<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageMYE.aspx.cs" Inherits="SMEPayroll.Management.ManageMYE" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />



    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">




    <!-- BEGIN HEADER -->
    <uc1:TopRightControl ID="TopRightControl" runat="server" />
    <!-- END HEADER -->

    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">

        <!-- BEGIN SIDEBAR -->
        <uc2:TopLeftControl ID="TopLeftControl" runat="server" />
        <!-- END SIDEBAR -->

        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <!-- BEGIN PAGE HEADER-->

                <div class="theme-panel hidden-xs hidden-sm">
                    <div class="toggler"></div>
                    <div class="toggler-close"></div>
                    <div class="theme-options">
                        <div class="theme-option theme-colors clearfix">
                            <span>THEME COLOR </span>
                            <ul>
                                <li class="color-default current tooltips" data-style="default" data-container="body" data-original-title="Default"></li>
                                <li class="color-blue tooltips" data-style="blue" data-container="body" data-original-title="Blue"></li>
                                <li class="color-green2 tooltips" data-style="green2" data-container="body" data-original-title="Green"></li>
                            </ul>
                        </div>
                    </div>
                </div>


                <!-- BEGIN PAGE BAR -->
                <div class="page-bar">
                    <ul class="page-breadcrumb">
                        <li>Manage MYE</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ShowDropdowns.aspx">Manage Settings</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>MYE Certificate</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage MYE</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>



                            <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

                                <script type="text/javascript" language="javascript">
                                    function isNumericKeyStrokeDecimal(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode
                                        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                                            return false;

                                        return true;
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <div>
                                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                    <script type="text/javascript">
                                        function RowDblClick(sender, eventArgs) {
                                            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                        }

                                        function myOnClientCleared(radUpload, eventargs) {
                                            var image = document.getElementById("Image1");
                                            image.src = null;
                                        }
                                    </script>

                                </radG:RadCodeBlock>

                                <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                    DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook" Width="100%" OnUpdateCommand="RadGrid1_ItemUpdated"
                                    OnInsertCommand="RadGrid1_InsertCommand">
                                    <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                        AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="false"
                                        DataKeyNames="id, FileName">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>

                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                <ItemTemplate>
                                                    <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            </radG:GridTemplateColumn>

                                            <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Certificate No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertificateNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CertificateNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCertificateNo" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CertificateNo")%>'></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ID="rfvCERT" runat="server" ControlToValidate="txtCertificateNo"
                                                        Display="None" ErrorMessage="Please Enter Certificate Number." SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="1" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Prior App Ref No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorAppRefNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppRefNo")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPriorAppRefNo" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppRefNo")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="2" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Prior App Granted">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorAppGranted" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppGranted")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPriorAppGranted" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                        MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppGranted")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Approval Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPriorType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorType")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPriorType1" Width="40px" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorType1")%>'></asp:TextBox>
                                                    <asp:TextBox ID="txtPriorType2" Width="100px" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorType2")%>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="1" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Issue Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIssueDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IssueDate")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdIssueDate"
                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "IssueDateCopy")%>' runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="2" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Val Start Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidityDateStart" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidityDateStart")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdValidityDateStart"
                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "ValidityDateStartCopy")%>'
                                                        runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="0" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true" ShowFilterIcon="false"
                                                HeaderText="Val End Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblValidityDateEnd" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ValidityDateEnd")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdValidityDateEnd"
                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "ValidityDateEndCopy")%>'
                                                        runat="server">
                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                    </radG:RadDatePicker>
                                                </EditItemTemplate>
                                                <ItemStyle HorizontalAlign="left" />
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="1" UniqueName="lnk" ShowFilterIcon="false" HeaderText="File Name">
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" Target="_blank" ID="hlnFile" Text='<%# Bind("FileName") %>'>      
                                                    </asp:HyperLink>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <radG:RadUpload ID="rdUpload" OnClientClearing="myOnClientCleared" InitialFileInputsCount="1"
                                                        runat="server" Skin="" EnableFileInputSkinning="false" EnableEmbeddedSkins="false"
                                                        Localization-Select="" ControlObjectsVisibility="None" MaxFileInputsCount="1"
                                                        OverwriteExistingFiles="True" />
                                                </EditItemTemplate>
                                            </radG:GridTemplateColumn>
                                            <radG:GridTemplateColumn EditFormColumnIndex="2" Display="false" AllowFiltering="False"
                                                UniqueName="TC">
                                                <ItemTemplate>
                                                    &nbsp;
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSt" runat="server" Text="&nbsp;">&nbsp;</asp:Label>
                                                </EditItemTemplate>
                                            </radG:GridTemplateColumn>
                                            <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridEditCommandColumn>
                                            <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                UniqueName="DeleteColumn">
                                                <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            </radG:GridButtonColumn>
                                            <radG:GridTemplateColumn Display="false" HeaderStyle-Width="0px" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label Width="0px" Visible="false" ID="lblID2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <%-- <EditItemTemplate>
                                                                <asp:Label Width="0px" Visible="false" ID="lblID1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                            </EditItemTemplate>--%>
                                            </radG:GridTemplateColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">

                                            <FormTemplate>
                                                <div class="clearfix form-style-inner">

                                                    <div class="heading">
                                                     <%--   <span class="form-title">Add New MYE Certificate</span>--%>
                                                        <asp:label ID="MYECertificate" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add MYE Certificate" : "Edit MYE Certificate" %>'
                                                                runat="server"></asp:label>
                                                    </div>



                                                    
                                                        <hr />
                                                    




                                                    

                                                        <div class="form form-inline">
                                                            <div class="form-body">

                                                                <div class="form-group clearfix">
                                                                    <label class="control-label"><tt class="required">*</tt>
                                                                        Certificate No</label>
                                                                    <asp:Label Width="0px" Visible="false" ID="lblID1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:Label>
                                                                    <asp:TextBox ID="txtCertificateNo" CssClass="form-control input-sm input-small cleanstring custom-maxlength _txtcertno" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CertificateNo")%>'></asp:TextBox>
<%--                                                                    <asp:RequiredFieldValidator ID="rfvCERT" runat="server" ControlToValidate="txtCertificateNo"
                                                                        Display="None" ErrorMessage="Please Enter Certificate Number." SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Prior App Ref No</label>

                                                                    <asp:TextBox ID="txtPriorAppRefNo" CssClass="form-control input-sm input-small cleanstring custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppRefNo")%>'></asp:TextBox>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Prior App Granted</label>

                                                                    <asp:TextBox ID="txtPriorAppGranted" CssClass="form-control input-sm input-small custom-maxlength _txtnprappreno" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                                        MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorAppGranted")%>'></asp:TextBox>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Approval Type</label>

                                                                    <asp:TextBox ID="txtPriorType1" CssClass="form-control input-sm input-xsmall input-inline padding-right-0 cleanstring custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorType1")%>'></asp:TextBox>
                                                                    <asp:TextBox ID="txtPriorType2" CssClass="form-control input-sm input-small input-inline cleanstring custom-maxlength" MaxLength="50" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PriorType2")%>'></asp:TextBox>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Issue Date</label>

                                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdIssueDate"
                                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "IssueDateCopy")%>' runat="server">
                                                                        <Calendar runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="riTextBox-custom" />
                                                                    </radG:RadDatePicker>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Val Start Date</label>

                                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdValidityDateStart"
                                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "ValidityDateStartCopy")%>'
                                                                        runat="server">
                                                                        <Calendar runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="riTextBox-custom" />
                                                                    </radG:RadDatePicker>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        Val End Date</label>

                                                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdValidityDateEnd"
                                                                        DbSelectedDate='<%# DataBinder.Eval(Container.DataItem, "ValidityDateEndCopy")%>'
                                                                        runat="server">
                                                                        <Calendar runat="server">
                                                                            <SpecialDays>
                                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                                                </telerik:RadCalendarDay>
                                                                            </SpecialDays>
                                                                        </Calendar>
                                                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" CssClass="riTextBox-custom" />
                                                                    </radG:RadDatePicker>

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        File Name</label>

                                                                    <radG:RadUpload ID="rdUpload" OnClientClearing="myOnClientCleared" InitialFileInputsCount="1"
                                                                        runat="server" Skin="" EnableFileInputSkinning="false" EnableEmbeddedSkins="false"
                                                                        Localization-Select="" ControlObjectsVisibility="None" MaxFileInputsCount="1"
                                                                        OverwriteExistingFiles="True" />

                                                                </div>
                                                                <div class="form-group clearfix">
                                                                    <label class="control-label">
                                                                        <asp:Label ID="lblSt" runat="server" Text="&nbsp;">&nbsp;</asp:Label></label>

                                                                </div>
                                                            </div>

                                                            <div class="form-actions text-center">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red insertmyecert" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                                <asp:Button ID="btnCancel" CssClass="btn default" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </div>


                                                        </div>

                                                    




                                                </div>
                                            </FormTemplate>

                                            <FormTableItemStyle HorizontalAlign="left"></FormTableItemStyle>
                                            <FormTableAlternatingItemStyle HorizontalAlign="left"></FormTableAlternatingItemStyle>
                                            <FormCaptionStyle HorizontalAlign="left" CssClass="EditFormHeader"></FormCaptionStyle>
                                            <FormMainTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                CellPadding="2" BackColor="White" Width="100%" />
                                            <FormTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                CellPadding="2" BackColor="White" />
                                            <EditColumn ButtonType="ImageButton" InsertText="Add New MYE Certificate" UpdateText="Update"
                                                UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                            </EditColumn>
                                            <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                        </EditFormSettings>
                                        <ExpandCollapseColumn Visible="False">
                                            <%--<HeaderStyle Width="19px"></HeaderStyle>--%>
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <%--<HeaderStyle Width="20px"></HeaderStyle>--%>
                                        </RowIndicatorColumn>
                                        <CommandItemSettings AddNewRecordText="Add New MYE Certificate" />
                                    </MasterTableView>

                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                </radG:RadGrid>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT ID, CertificateNo, Convert(varchar,IssueDate,103) IssueDate, IssueDate IssueDateCopy, PriorAppRefNo, PriorAppGranted, PriorType1, PriorType2, Convert(varchar,ValidityDateStart,103) ValidityDateStart, ValidityDateStart ValidityDateStartCopy, Convert(varchar,ValidityDateEnd,103) ValidityDateEnd, ValidityDateEnd ValidityDateEndCopy, FileName, (PriorType1+'-'+PriorType2) PriorType FROM [MYECertificate] WHERE [company_id] = @company_id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>

                                <center>
                                    &nbsp;</center>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                    ShowMessageBox="true" ShowSummary="False" />
                            </div>
                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->

        <uc5:QuickSideBartControl ID="QuickSideBartControl1" runat="server" />
        <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            2014 &copy; Metronic by keenthemes.
            <a href="http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes" title="Purchase Metronic just for 27$ and get lifetime updates for free" target="_blank">Purchase Metronic!</a>
        </div>
        <div class="scroll-to-top">
            <i class="icon-arrow-up"></i>
        </div>
    </div>

    <uc_js:bundle_js ID="bundle_js" runat="server" />

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
        $('.insertmyecert').click(function () {
            return validatemyecert();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this MYE Certificate?", _id, "Confirm Delete", "Delete");
        });
       window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

        }
        function validatemyecert() {
            var _message = "";
            if ($.trim($("._txtcertno").val()) === "")
                _message += "Please Input Certificate No <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
