<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageSupplier.aspx.cs"
    Inherits="SMEPayroll.Management.ManageSupplier" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
        var SupplierID = "";
        var changedFlage = "false";
        var SupplierName = "";
        var changedFlage1 = "false";


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
            if (SupplierID == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                    SupplierID = cellvalue;
                    alert(cellvalue);
                }
            }
            if (SupplierName == "" && changedFlage1 == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[3].innerHTML; // to access the cell value                                    
                    SupplierName = cellvalue;
                    alert(cellvalue);
                }
            }
            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(SupplierID), "SupplierID");
                if (message != "")
                    sMsg += message + "\n";

                message = MandatoryData(trim(SupplierName), "SupplierName");
                if (message != "")
                    sMsg += message + "\n";

                if (sMsg != "") {
                    args.set_cancel(true);
                    alert(sMsg);
                }
            }
        }

        function OnFocusLost_SupplierID(val) {
            var Object = document.getElementById(val);
            SupplierID = GetDataFromHtml(Object);
            changedFlage = "true";
        }

        function OnFocusLost_SupplierName(val) {
            var Object = document.getElementById(val);
            SupplierName = GetDataFromHtml(Object);
            changedFlage1 = "true";
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
                        <li>Manage Supplier</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Cost/Cost.aspx"><span>Costing Managements</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Cost/CostingByTeamIndex.aspx"><span>Costing By Team</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Supplier </span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Supplier</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            

                            
                                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                    <script type="text/javascript">
                                        function RowDblClick(sender, eventArgs) {
                                            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                        }
                                    </script>

                                </radG:RadCodeBlock>
                                
                                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                                OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" Width="93%"
                                                OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" AllowFilteringByColumn="true"
                                                AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                                MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                                MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                                GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                                ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                                OnItemCommand="RadGrid1_ItemCommand">
                                                <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="id">
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

                                                        <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                            UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="SupplierID" UniqueName="SupplierID" FilterControlAltText="cleanstring" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                            SortExpression="SupplierID" HeaderText="Supplier Code">
                                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="SupplierName" UniqueName="SupplierName" FilterControlAltText="cleanstring" ShowFilterIcon="false"  AutoPostBackOnFilter="true"
                                                            SortExpression="SupplierName" HeaderText="Supplier Name">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Address1" UniqueName="Address1"
                                                            SortExpression="Address1" HeaderText="Address-1" Visible="false">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Address2" UniqueName="Address2"
                                                            SortExpression="Address2" HeaderText="Address-2" Visible="false">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="City" UniqueName="City" FilterControlAltText="cleanstring" ShowFilterIcon="false"  AutoPostBackOnFilter="true"
                                                            SortExpression="City" HeaderText="City">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridDropDownColumn EditFormColumnIndex="2" DataField="Country" DataSourceID="SqlDataSource2" FilterControlAltText="cleanstring" ShowFilterIcon="false"  AutoPostBackOnFilter="true"
                                                            HeaderText="Country" ListTextField="Country" ListValueField="ID" UniqueName="GridDropDownColumn">
                                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                        </radG:GridDropDownColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="PostalCode" UniqueName="PostalCode"
                                                            SortExpression="PostalCode" HeaderText="Postal" Visible="false">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Fax" UniqueName="Fax" SortExpression="Fax"
                                                            HeaderText="Fax" Visible="false">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Phone" UniqueName="Phone" FilterControlAltText="numericonly" ShowFilterIcon="false"  AutoPostBackOnFilter="true"
                                                            SortExpression="Phone" HeaderText="Phone">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Email" UniqueName="Email"
                                                            SortExpression="Email" HeaderText="Email" Visible="false">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="Mobile" UniqueName="Mobile" FilterControlAltText="numericonly" ShowFilterIcon="false"  AutoPostBackOnFilter="true"
                                                            SortExpression="Mobile" HeaderText="Mobile">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
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
                                                            <ItemStyle Width="50px" />
                                                        </radG:GridEditCommandColumn>
                                                        <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                            UniqueName="DeleteColumn">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </radG:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings ColumnNumber="3">
                                                        <FormTableItemStyle HorizontalAlign="left" Wrap="False" BorderWidth="0"></FormTableItemStyle>
                                                        <FormCaptionStyle HorizontalAlign="left" CssClass="EditFormHeader"></FormCaptionStyle>
                                                        <FormMainTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                            CellPadding="3" BackColor="White" Width="100%" />
                                                        <FormTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                            CellPadding="2" Height="90px" BackColor="White" />
                                                        <FormTableAlternatingItemStyle HorizontalAlign="left" BorderColor="blue" BorderWidth="0"
                                                            Wrap="False"></FormTableAlternatingItemStyle>
                                                        <EditColumn ButtonType="ImageButton" InsertText="Add New Supplier" UpdateText="Update"
                                                            UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                                        </EditColumn>
                                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                    </EditFormSettings>
                                                    <CommandItemSettings AddNewRecordText="Add New Supplier" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <%--<ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                                    <ClientEvents OnRowDblClick="RowDblClick"/>
                                                </ClientSettings>
                                            </radG:RadGrid>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Supplier] (Company_ID, SupplierID, SupplierName, Address1, Address2, City, Country, PostalCode, Phone, Fax, Mobile, Email,Module) VALUES (@Company_ID, @SupplierID, @SupplierName, @Address1, @Address2, @City, @Country, @PostalCode, @Phone, @Fax, @Mobile, @Email,@module)"
                                    SelectCommand="Select  S.ID, SupplierID, SupplierName, Address1, Address2, City, Country, PostalCode, Phone, Fax, Mobile, Email From Supplier S  Where Company_ID=@company_id AND Module=@module"
                                    UpdateCommand="UPDATE [Supplier] SET SupplierID=@SupplierID, SupplierName=@SupplierName, Address1=@Address1, Address2=@Address2, City=@City, Country=@Country,PostalCode=@PostalCode,Phone=@Phone,Fax=@Fax,Mobile=@Mobile,Email=@Email WHERE [id] = @id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        <asp:QueryStringParameter Name="module" QueryStringField="module" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="SupplierID" Type="String" />
                                        <asp:Parameter Name="SupplierName" Type="String" />
                                        <asp:Parameter Name="Address1" Type="String" />
                                        <asp:Parameter Name="Address2" Type="String" />
                                        <asp:Parameter Name="City" Type="String" />
                                        <asp:Parameter Name="Country" Type="Int32" />
                                        <asp:Parameter Name="PostalCode" Type="String" />
                                        <asp:Parameter Name="Phone" Type="String" />
                                        <asp:Parameter Name="Fax" Type="String" />
                                        <asp:Parameter Name="Mobile" Type="String" />
                                        <asp:Parameter Name="Email" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                        <asp:QueryStringParameter Name="module" QueryStringField="module" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="SupplierID" Type="String" />
                                        <asp:Parameter Name="SupplierName" Type="String" />
                                        <asp:Parameter Name="Address1" Type="String" />
                                        <asp:Parameter Name="Address2" Type="String" />
                                        <asp:Parameter Name="City" Type="String" />
                                        <asp:Parameter Name="Country" Type="Int32" />
                                        <asp:Parameter Name="PostalCode" Type="String" />
                                        <asp:Parameter Name="Phone" Type="String" />
                                        <asp:Parameter Name="Fax" Type="String" />
                                        <asp:Parameter Name="Mobile" Type="String" />
                                        <asp:Parameter Name="Email" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        <asp:QueryStringParameter Name="module" QueryStringField="module" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select '-1' as id,'--Select--' as Country Union  Select [id], Country From Country Order By Country">
                                    <SelectParameters>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            
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
        $(document).ready(function () {
            $("input[type='button']").removeAttr("style");
            //$(".clsCnfrmButton").click(function () {
            //    var _elem = $(this).find('input[type=image]');
            //    var _id = _elem.attr('id');
            //    GetConfirmation("Are you sure you want to delete this Supplier?", _id, "Confirm Delete", "Delete");
            //});
            var _cntrl = $("#RadGrid1_ctl00 input[type=text]");
            var lbl = "";
            if (_cntrl.length > 7) {
                for (var i = 7; i < _cntrl.length; i++) {
                    lbl = $(_cntrl[i]).closest("tr").find("label").text();
                    //alert(lbl);
                    switch (lbl) {
                        case "Supplier Code:":
                            $(_cntrl[i]).addClass("cleanstring custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "Supplier Name:":
                            $(_cntrl[i]).addClass("cleanstring custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "Address-1:":
                            $(_cntrl[i]).addClass("cleanstring custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "Address-2:":
                            $(_cntrl[i]).addClass("cleanstring custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "City:":
                            $(_cntrl[i]).addClass("cleanstring custom-maxlength form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 50);
                            break;
                        case "Postal:":
                            $(_cntrl[i]).addClass("numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 6);
                            break;
                        case "Fax:":
                            $(_cntrl[i]).addClass("numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 10);
                            break;
                        case "Phone:":
                            $(_cntrl[i]).addClass("numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 10);
                            break;
                        case "Email:":
                            $(_cntrl[i]).addClass("form-control input-sm");
                            break;
                        case "Mobile:":
                            $(_cntrl[i]).addClass("numericonly form-control input-sm");
                            $(_cntrl[i]).attr("maxlength", 10);
                            break;
                        default:
                            break;
                    }
                }
            }
            window.onload = function () {
                CallNotification('<%=ViewState["actionMessage"].ToString() %>');
                var _inputs = $('#RadGrid1_ctl00 thead tr td').find('input[type=text]');
                $.each(_inputs, function (index, val) {
                    $(this).addClass($(this).attr('alt'));

                })
            }
        });
    </script>

</body>
</html>
