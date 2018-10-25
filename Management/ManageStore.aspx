<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageStore.aspx.cs" Inherits="SMEPayroll.Management.ManageStore" %>

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
        var StoreID = "";
        var changedFlage = "false";
        var StoreName = "";
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
            if (StoreID == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                    StoreID = cellvalue;
                    //alert(StoreID);                          
                }
            }

            if (StoreName == "" && changedFlage1 == "false") {


                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[3].innerHTML; // to access the cell value                                    
                    StoreName = cellvalue;
                    //alert(StoreName);
                }
            }
            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(StoreID), "StoreID");
                if (message != "")
                    sMsg += message + "\n";

                message = MandatoryData(trim(StoreName), "StoreName");
                if (message != "")
                    sMsg += message + "\n";

                if (sMsg != "") {
                    args.set_cancel(true);
                    alert(sMsg);
                }
            }
        }

        function OnFocusLost_StoreID(val) {
            var Object = document.getElementById(val);
            StoreID = GetDataFromHtml(Object);
            changedFlage = "true";
        }

        function OnFocusLost_StoreName(val) {
            var Object = document.getElementById(val);
            StoreName = GetDataFromHtml(Object);
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
                        <li>
                            <a href="home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title">Manage Store</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>

                            
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
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="StoreID" UniqueName="StoreID"
                                                            SortExpression="StoreID" HeaderText="Store ID">
                                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="StoreName" UniqueName="StoreName"
                                                            SortExpression="StoreName" HeaderText="Store Name">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="2" DataField="Location" UniqueName="Location"
                                                            SortExpression="Location" HeaderText="Location">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="3" DataField="Address1" UniqueName="Address1"
                                                            SortExpression="Address1" HeaderText="Address-1">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Address2" UniqueName="Address2"
                                                            SortExpression="Address2" HeaderText="Address-2">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="City" UniqueName="City"
                                                            SortExpression="City" HeaderText="City">
                                                            <ItemStyle Width="40%" HorizontalAlign="Left" />
                                                        </radG:GridBoundColumn>
                                                        <radG:GridDropDownColumn EditFormColumnIndex="2" DataField="Country" DataSourceID="SqlDataSource2"
                                                            HeaderText="Country" ListTextField="Country" ListValueField="ID" UniqueName="GridDropDownColumn">
                                                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                        </radG:GridDropDownColumn>
                                                        <radG:GridTemplateColumn EditFormColumnIndex="3" Display="false" AllowFiltering="False"
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
                                                        <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                            UniqueName="DeleteColumn">
                                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                        </radG:GridButtonColumn>
                                                    </Columns>
                                                    <EditFormSettings ColumnNumber="4">
                                                        <FormTableItemStyle HorizontalAlign="left" Wrap="False" BorderWidth="0"></FormTableItemStyle>
                                                        <FormCaptionStyle HorizontalAlign="left" CssClass="EditFormHeader"></FormCaptionStyle>
                                                        <FormMainTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                            CellPadding="3" BackColor="White" Width="100%" />
                                                        <FormTableStyle HorizontalAlign="left" BorderColor="black" BorderWidth="0" CellSpacing="0"
                                                            CellPadding="2" Height="60px" BackColor="White" />
                                                        <FormTableAlternatingItemStyle HorizontalAlign="left" BorderColor="blue" BorderWidth="0"
                                                            Wrap="False"></FormTableAlternatingItemStyle>
                                                        <EditColumn ButtonType="ImageButton" InsertText="Add New Store" UpdateText="Update"
                                                            UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                                        </EditColumn>
                                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                                    </EditFormSettings>
                                                    <CommandItemSettings AddNewRecordText="Add New Store" />
                                                </MasterTableView>
                                                <ClientSettings>
                                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                    <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                                                </ClientSettings>
                                            </radG:RadGrid>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Store] (Company_ID, StoreID, StoreName, Location, Address1, Address2, PostalCode, City, Country) VALUES (@Company_ID, @StoreID, @StoreName, @Location, @Address1, @Address2, @PostalCode, @City, @Country)"
                                    SelectCommand="Select  S.ID, StoreID, StoreName, Location, Address1, Address2, PostalCode, City, C.Country,C.ID From Store S Inner Join Country C On S.Country=C.ID Where Company_ID=@company_id"
                                    UpdateCommand="UPDATE [Store] SET StoreID=@StoreID, StoreName=@StoreName, Location=@Location, Address1=@Address1, Address2=@Address2, PostalCode=@PostalCode, City=@City, Country=@Country WHERE [id] = @id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="StoreID" Type="String" />
                                        <asp:Parameter Name="StoreName" Type="String" />
                                        <asp:Parameter Name="Location" Type="String" />
                                        <asp:Parameter Name="Address1" Type="String" />
                                        <asp:Parameter Name="Address2" Type="String" />
                                        <asp:Parameter Name="PostalCode" Type="String" />
                                        <asp:Parameter Name="City" Type="String" />
                                        <asp:Parameter Name="Country" Type="Int32" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="StoreID" Type="String" />
                                        <asp:Parameter Name="StoreName" Type="String" />
                                        <asp:Parameter Name="Location" Type="String" />
                                        <asp:Parameter Name="Address1" Type="String" />
                                        <asp:Parameter Name="Address2" Type="String" />
                                        <asp:Parameter Name="PostalCode" Type="String" />
                                        <asp:Parameter Name="City" Type="String" />
                                        <asp:Parameter Name="Country" Type="Int32" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select [id], Country From Country Order By Country">
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
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
