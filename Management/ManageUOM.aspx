<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageUOM.aspx.cs" Inherits="SMEPayroll.Management.ManageUOM" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
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

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>
    <script type="text/javascript">
        var UnitName = "";
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
            if (UnitName == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[3].innerHTML; // to access the cell value                                    
                    UnitName = cellvalue;
                }
            }
            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(UnitName), "UnitName");
                if (message != "")
                    sMsg += message + "\n";

                if (sMsg != "") {
                    args.set_cancel(true);
                    alert(sMsg);
                }
            }
        }

        function OnFocusLost_UnitName(val) {
            var Object = document.getElementById(val);
            UnitName = GetDataFromHtml(Object);
            changedFlage = "true";
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
                        <li>Manage UOM</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ItemsManagement.aspx">Manage Items</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage UOM</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage UOM</h3>--%>
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

                            <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="93%"
                                OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="true"
                                AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true">
                                <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="ID">
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

                                        <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                            UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="UnitType" DataSourceID="xmldtUOM" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            HeaderText="Unit Type" ListTextField="text" ListValueField="id" UniqueName="GridDropDownColumn" AllowFiltering="false">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="UnitName" UniqueName="UnitName" FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            SortExpression="UnitName" HeaderText="Unit Name" AllowFiltering="true" AutoPostBackOnFilter="true">
                                            <%--<ItemStyle Width="70%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="heading">
                                                    <%--  <span class="form-title">Add New Units for Measurement</span>--%>
                                                    <asp:Label ID="UnitsforMeasurement" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Units for Measurement" : "Edit Units for Measurement" %>'
                                                        runat="server"></asp:Label>
                                                </div>

                                                <hr />

                                                <div class="col-sm-12">
                                                    <div class="form-inline">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Unit Type</label>

                                                                <asp:DropDownList ID="ddl1" runat="server" CssClass="form-control input-inline input-sm input-medium"
                                                                    SelectedValue='<%# Bind("UnitType") %>'
                                                                    DataTextField="UnitType" DataValueField="id" AppendDataBoundItems="True" DataSourceID="SqlDataSource1">
                                                                    <asp:ListItem Selected="True" Text="Select" Value="">
                                                                    </asp:ListItem>
                                                                </asp:DropDownList>


                                                                <%--<asp:TextBox ID="TextBox1" CssClass="form-control input-sm input-small" runat="server" Text='<%# Bind("UnitType") %>'></asp:TextBox>--%>
                                                                <%--                             <asp:DropDownList ID="ddl1" CssClass="form-control input-sm inline input-medium" runat="server" SelectedValue='<%# Bind("UnitType") %>'
                                                    DataSource='<%# (new string[] { "1", "2", "3", "4", "5", "6"}) %>' 
                                                    AppendDataBoundItems="True">
                                                    <asp:ListItem Selected="True" Text="Select" Value="">
                                                    </asp:ListItem>
                                                </asp:DropDownList>--%>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Unit Name</label>
                                                                <asp:TextBox ID="TextBox2" TabIndex="1" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtunit" MaxLength="50" runat="server" Text='<%# Bind("UnitName") %>'></asp:TextBox>

                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">&nbsp;</label>
                                                                <asp:Button ID="btnUpdate" CssClass="btn red insertunit margin-top-0" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>

                                    <CommandItemSettings AddNewRecordText="Add New Units for Measurement" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                    <%--<ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Unit] (UnitName, UnitType, Company_ID) VALUES (@UnitName, @UnitType, @Company_ID)"
                                SelectCommand="Select ID,UnitName,UnitType From Unit Where Company_ID=@company_id"
                                UpdateCommand="UPDATE [Unit] SET [UnitName] = @UnitName, UnitType = @UnitType WHERE [ID] = @ID">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <asp:Parameter Name="UnitName" Type="String" />
                                    <asp:Parameter Name="UnitType" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="UnitName" Type="String" />
                                    <asp:Parameter Name="UnitType" Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>

                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="true" ShowSummary="False" />
                            <asp:XmlDataSource ID="xmldtUOM" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Units/Type"></asp:XmlDataSource>
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
        $('.insertunit').click(function () {
            return validateunit();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this UOM?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

        }
        function validateunit() {
            var _message = "";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_ddl1 option:selected").text()) === "Select")
                _message += "Please Select Unit Type <br>";
            if ($.trim($("._txtunit").val()) === "")
                _message += "Please Input Unit Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
