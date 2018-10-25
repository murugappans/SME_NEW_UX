<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailRemainder.aspx.cs"
    Inherits="SMEPayroll.Management.EmailRemainder" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="System.Text.RegularExpressions" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>

    <script type="text/javascript">
        var Designation = "";
        var changedFlage = "false";

        //Check Validations for grid like Mandatory and 
        function Validations(sender, args) {
            if (typeof (args) !== "undefined") {
                var commandName = args.get_commandName();
                var commandArgument = args.get_commandArgument();
                switch (commandName) {
                    case "startRunningCommand":
                        $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                    case "alertCommand":
                        $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                    default:
                        $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                }
            }

            var result = args.get_commandName();
            if (cat_name == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                    cat_name = cellvalue;
                }
            }
            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(cat_name), "Category Name");
                if (message != "")
                    sMsg += message + "\n";

                if (sMsg != "") {
                    args.set_cancel(true);
                    alert(sMsg);
                }
            }
        }

        function OnFocusLost_cat_name(val) {
            var Object = document.getElementById(val);
            cat_name = GetDataFromHtml(Object);
            changedFlage = "true";
        }
    </script>

    <script runat="server">
        //--------- murugan
        private void On_Inserting(Object sender, SqlDataSourceCommandEventArgs e)
        {

            string s = e.Command.Parameters[1].Value.ToString();
            // Regex.IsMatch(s,"",RegexOptions.IgnoreCase)
            if (!(Regex.IsMatch(s, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase)))
            {
                e.Cancel = true;
                // RadGrid1.Controls.Add(new LiteralControl("invalid email.."));
                throw new Exception("Invalid Email Format..");
            }


        }
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
        <uc2:topleftcontrol id="TopLeftControl" runat="server" />
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
                        <li>Email Reminders Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="OtherManagement.aspx">Manage Others</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ManageHomePageRemainders.aspx">Home Page Reminders</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Reminders to Email</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Email Remainder Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            <%--<div class="search-box clearfix">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <div>
                                
                                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemDataBound="RadGrid1_ItemDataBound"
                                                AllowSorting="true" runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1"
                                                GridLines="None" Skin="Outlook" Width="93%" OnItemInserted="RadGrid1_ItemInserted"
                                                OnItemUpdated="RadGrid1_ItemUpdated">
                                                <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="id"
                                                    AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                                    CommandItemDisplay="Bottom">
                                                    <FilterItemStyle HorizontalAlign="left" />
                                                    <HeaderStyle ForeColor="Navy" />
                                                    <ItemStyle BackColor="White" Height="20px" />
                                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                    <Columns>

                                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                            <ItemTemplate>
                                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="10px" />
                                                        </radG:GridTemplateColumn>

                                                        <radG:GridBoundColumn ReadOnly="True" DataField="id" DataType="System.Int32" UniqueName="id"
                                                            Visible="false" SortExpression="ID" HeaderText="Id">
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn ReadOnly="True" DataField="id" DataType="System.Int32" UniqueName="id"
                                                            Visible="false" SortExpression="ID" HeaderText="Id">
                                                            <ItemStyle Width="100px" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn DataField="Emails" UniqueName="Emails" SortExpression="Emails"
                                                            HeaderText="Email">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridCheckBoxColumn DataField="EmployeeOnLeave" UniqueName="EmployeeOnLeave"
                                                            SortExpression="EmployeeOnLeave" HeaderText="Employee On Leave">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="PendingLeaveRequest" UniqueName="PendingLeaveRequest"
                                                            SortExpression="PendingLeaveRequest" HeaderText="Pending Leave Request">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="PassesExpiring" UniqueName="PassesExpiring" SortExpression="PassesExpiring"
                                                            HeaderText="Psses Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="PassportExpiring" UniqueName="PassportExpiring"
                                                            SortExpression="PassportExpiring" HeaderText="Pssport Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="CSOCExpiring" UniqueName="CSOCExpiring" SortExpression="CSOCExpiring"
                                                            HeaderText="CSOC Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="InsuranceExpiring" UniqueName="InsuranceExpiring"
                                                            SortExpression="InsuranceExpiring" HeaderText="Insurance Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="EmployeeBirthday" UniqueName="EmployeeBirthday"
                                                            SortExpression="EmployeeBirthday" HeaderText="Employee Birthday">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="ProbationPeriodExpiring" UniqueName="ProbationPeriodExpiring"
                                                            SortExpression="ProbationPeriodExpiring" HeaderText="Probation Period Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="OtherCertificatesExpiring" UniqueName="OtherCertificatesExpiring"
                                                            SortExpression="OtherCertificatesExpiring" HeaderText="OtherCertificates Expiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridCheckBoxColumn DataField="LicenseExpiring" UniqueName="LicenseExpiring"
                                                            SortExpression="LicenseExpiring" HeaderText="LicenseExpiring">
                                                            <ItemStyle Width="93%" />
                                                        </radG:GridCheckBoxColumn>
                                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                            <ItemStyle Width="30px" />
                                                        </radG:GridEditCommandColumn>
                                                        <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                            UniqueName="DeleteColumn">
                                                            <ItemStyle Width="30px" />
                                                        </radG:GridButtonColumn>
                                                    </Columns>
                                                    <ExpandCollapseColumn Visible="False">
                                                        <HeaderStyle Width="19px"></HeaderStyle>
                                                    </ExpandCollapseColumn>
                                                    <RowIndicatorColumn Visible="False">
                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                    </RowIndicatorColumn>
                                                    <CommandItemSettings AddNewRecordText="Add New Email" />
                                                </MasterTableView>
                                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                                                </ClientSettings>
                                            </radG:RadGrid>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserting="On_Inserting"
                                    InsertCommand="INSERT INTO [dbo].[EmailRemainder](CompId,[Emails],[EmployeeOnLeave],[PendingLeaveRequest],[PassesExpiring],[PassportExpiring],[CSOCExpiring],[InsuranceExpiring],[EmployeeBirthday],[ProbationPeriodExpiring],[OtherCertificatesExpiring],[LicenseExpiring])
            VALUES(@company_id,@Emails,@EmployeeOnLeave,@PendingLeaveRequest,@PassesExpiring,@PassportExpiring,@CSOCExpiring,@InsuranceExpiring,@EmployeeBirthday,@ProbationPeriodExpiring ,@OtherCertificatesExpiring,@LicenseExpiring)"
                                    SelectCommand="SELECT [id],CompId,[Emails],[EmployeeOnLeave],[PendingLeaveRequest]
      ,[PassesExpiring]
      ,[PassportExpiring]
      ,[CSOCExpiring]
      ,[InsuranceExpiring]
      ,[EmployeeBirthday]
      ,[ProbationPeriodExpiring]
      ,[OtherCertificatesExpiring]
      ,[LicenseExpiring]
  FROM [dbo].[EmailRemainder] where CompId=@company_id"
                                    UpdateCommand="UPDATE [EmailRemainder] SET [Emails] = @Emails,[EmployeeOnLeave] =@EmployeeOnLeave,[PendingLeaveRequest]=@PendingLeaveRequest,[PassesExpiring] =@PassesExpiring,[PassportExpiring] =@PassportExpiring,[CSOCExpiring]=@CSOCExpiring,[InsuranceExpiring]=@InsuranceExpiring,[EmployeeBirthday] =@EmployeeBirthday,[ProbationPeriodExpiring] =@ProbationPeriodExpiring,[OtherCertificatesExpiring]=@OtherCertificatesExpiring,[LicenseExpiring]=@LicenseExpiring WHERE [id] = @id"
                                    DeleteCommand="DELETE FROM [EmailRemainder] WHERE [id] = @id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Emails" Type="String" />
                                        <asp:Parameter Name="EmployeeOnLeave" Type="boolean" />
                                        <asp:Parameter Name="PendingLeaveRequest" Type="boolean" />
                                        <asp:Parameter Name="PassesExpiring" Type="boolean" />
                                        <asp:Parameter Name="PassportExpiring" Type="boolean" />
                                        <asp:Parameter Name="CSOCExpiring" Type="boolean" />
                                        <asp:Parameter Name="InsuranceExpiring" Type="boolean" />
                                        <asp:Parameter Name="EmployeeBirthday" Type="boolean" />
                                        <asp:Parameter Name="ProbationPeriodExpiring" Type="boolean" />
                                        <asp:Parameter Name="OtherCertificatesExpiring" Type="boolean" />
                                        <asp:Parameter Name="LicenseExpiring" Type="boolean" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        <asp:Parameter Name="Emails" Type="String" />
                                        <asp:Parameter Name="EmployeeOnLeave" Type="boolean" />
                                        <asp:Parameter Name="PendingLeaveRequest" Type="boolean" />
                                        <asp:Parameter Name="PassesExpiring" Type="boolean" />
                                        <asp:Parameter Name="PassportExpiring" Type="boolean" />
                                        <asp:Parameter Name="CSOCExpiring" Type="boolean" />
                                        <asp:Parameter Name="InsuranceExpiring" Type="boolean" />
                                        <asp:Parameter Name="EmployeeBirthday" Type="boolean" />
                                        <asp:Parameter Name="ProbationPeriodExpiring" Type="boolean" />
                                        <asp:Parameter Name="OtherCertificatesExpiring" Type="boolean" />
                                        <asp:Parameter Name="LicenseExpiring" Type="boolean" />
                                    </InsertParameters>
                                    <DeleteParameters>
                                    </DeleteParameters>
                                </asp:SqlDataSource>
                                <center>
                                    &nbsp;</center>
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
          window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }
    </script>

</body>
</html>
