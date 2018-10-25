<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageItem.aspx.cs" Inherits="SMEPayroll.Management.ManageItem" %>

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
        var ItemID = "";
        var changedFlage = "false";
        var ItemName = "";
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
            if (ItemID == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[6].innerHTML; // to access the cell value                                    
                    ItemID = cellvalue;
                }
            }

            if (ItemName == "" && changedFlage1 == "false") {
                var itemIndex = args.get_commandArgument();
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[7].innerHTML; // to access the cell value                                    
                    ItemName = cellvalue;
                }
            }

            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(ItemID), "ItemCode");
                if (message != "")
                    sMsg += message + "\n";

                message = MandatoryData(trim(ItemName), "ItemName");
                if (message != "")
                    sMsg += message + "\n";

                if (sMsg != "") {
                    args.set_cancel(true);
                    alert(sMsg);
                }
            }
        }

        function OnFocusLost_ItemID(val) {
            var Object = document.getElementById(val);
            ItemID = GetDataFromHtml(Object);
            changedFlage = "true";
        }

        function OnFocusLost_ItemName(val) {
            var Object = document.getElementById(val);
            ItemName = GetDataFromHtml(Object);
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
                        <li>Manage Item</li>
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
                            <span>Manage Item</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Item</h3>--%>
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

                            <radG:RadGrid ID="RadGrid1"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                PageSize="20" AllowPaging="true" OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1"
                                Width="93%" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                               AllowSorting="true" Skin="Outlook" MasterTableView-CommandItemDisplay="bottom"
                                MasterTableView-AllowAutomaticUpdates="true" MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="true" MasterTableView-AllowMultiColumnSorting="true"
                                GroupHeaderItemStyle-HorizontalAlign="left" ClientSettings-EnableRowHoverStyle="true"
                                ClientSettings-AllowColumnsReorder="true" ClientSettings-ReorderColumnsOnClient="true"
                                OnDetailTableDataBind="RadGrid1_DetailTableDataBind" ClientSettings-AllowDragToGroup="false"
                                ShowGroupPanel="false">
                                <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="IDParent" AutoGenerateColumns="false">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <ExpandCollapseColumn Groupable="true">
                                    </ExpandCollapseColumn>
                                    <DetailTables>
                                        <radG:GridTableView DataKeyNames="IDChild, ID" runat="server" Width="100%" CommandItemDisplay="Bottom"
                                            ShowHeadersWhenNoRecords="true" Name="Parameters" AutoGenerateColumns="false">
                                            <ParentTableRelation>
                                                <radG:GridRelationFields DetailKeyField="IDChild" MasterKeyField="IDParent" />
                                            </ParentTableRelation>
                                            <Columns>

                                                <radG:GridTemplateColumn UniqueName="TemplateColumn" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                    UniqueName="ID" Visible="false" SortExpression="ID" HeaderText="ID">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="IDChild" DataType="System.Int32"
                                                    UniqueName="IDChild" Visible="false" SortExpression="IDChild" HeaderText="IDChild">
                                                </radG:GridBoundColumn>
                                                <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="ParameterID" DataSourceID="SqlDataSource4"
                                                    HeaderText="Parameter" ListTextField="ParameterName" ListValueField="ID" UniqueName="ParameterID">
                                                    <ItemStyle Width="30%" HorizontalAlign="Left" />
                                                </radG:GridDropDownColumn>
                                                <radG:GridBoundColumn EditFormColumnIndex="1" DataField="ParameterVar" UniqueName="ParameterVar"
                                                    SortExpression="ParameterVar" HeaderText="Parameter Remarks">
                                                    <ItemStyle Width="65%" HorizontalAlign="Left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <ItemStyle Width="20px" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                                    ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete Item"
                                                    UniqueName="DeleteColumn">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <CommandItemTemplate>
                                                <div style="height: 15px; vertical-align: middle;" align="left">
                                                    <asp:LinkButton ID="btnAddParameter" Font-Size="11px" Text="Add Parameter" runat="server"
                                                        CommandName="InitInsert"></asp:LinkButton>
                                                </div>
                                            </CommandItemTemplate>
                                            <EditFormSettings ColumnNumber="2">
                                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                                <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                    BackColor="White" Width="100%" />
                                                <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="0"
                                                    Height="30px" BackColor="White" />
                                                <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                                <EditColumn ButtonType="ImageButton" InsertText="Add New Parameter" UpdateText="Update"
                                                    UniqueName="edtColumnPar" CancelText="Cancel Edit">
                                                </EditColumn>
                                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add New Parameter" />
                                        </radG:GridTableView>
                                    </DetailTables>
                                    <Columns>

                                        <radG:GridTemplateColumn UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Display="False" ReadOnly="True" DataField="IDParent" DataType="System.Int32"
                                            UniqueName="IDParent" Visible="true" SortExpression="IDParent" HeaderText="IDParent">
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="0" DataField="UOM" DataSourceID="SqlDataSource2"
                                            HeaderText="UOM" ListTextField="UnitName" ListValueField="ID" UniqueName="GridDropDownColumn">
                                            <%--<ItemStyle Width="10%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="ItemType" DataSourceID="SqlDataSource5"
                                            HeaderText="Inventory Type" ListTextField="TypeName" ListValueField="ID" UniqueName="GrdItemTypeCol">
                                            <%--<ItemStyle Width="20%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="2" DataField="ItemSubCatID" DataSourceID="SqlDataSource6"
                                            HeaderText="Item Sub Category" ListTextField="ItemSubCategoryName" ListValueField="ID"
                                            UniqueName="GrdItemSubCategoryNameCol">
                                            <%--<ItemStyle Width="20%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="ItemID" UniqueName="ItemID"
                                            SortExpression="ItemID" HeaderText="Item Code">
                                            <%--<ItemStyle Width="10%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn EditFormColumnIndex="1" DataField="ItemName" UniqueName="ItemName"
                                            SortExpression="ItemName" HeaderText="Item Name">
                                            <%--<ItemStyle Width="35%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn EditFormColumnIndex="2">
                                            <ItemTemplate>
                                                &nbsp;
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                &nbsp;
                                            </EditItemTemplate>
                                        </radG:GridTemplateColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete Parameter"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="heading">
                                                   <%-- <span class="form-title">Add New Item</span>--%>
                                 <asp:Label ID="Item" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Item" : "Edit Item" %>'
        runat="server"></asp:Label>
                                                </div>
                                                
                                                    <hr />
                                               
                                                
                                                    <div class="form-inline">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">

                                                                <div>
                                                                      <asp:TextBox  Visible="false" ID="TextBox6" CssClass="form-control input-sm inline input-medium" runat="server" Text='<%# Bind("IDParent") %>'></asp:TextBox>
                                                                </div>

                                                                <label class="control-label">UOM</label>
                                                                
                                                                        <asp:DropDownList ID="drpuom" runat="server" CssClass="form-control input-inline input-sm input-medium"  SelectedValue='<%# Bind("UOM") %>'
                             DataTextField="UnitName" DataValueField="ID"   AppendDataBoundItems="True" DataSourceID="SqlDataSource2">
                                                                            <asp:ListItem Selected="True" Text="Select" Value="">
                                                                    </asp:ListItem>
                        </asp:DropDownList>


                                                               <%--     <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium" runat="server" Text='<%# Bind("UOM") %>'></asp:TextBox>--%>
                                                                
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Inventory Type</label>
                                                                
                                                                 <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control input-inline input-sm input-medium"  SelectedValue='<%# Bind("ItemType") %>'
                             DataTextField="TypeName" DataValueField="ID"   AppendDataBoundItems="True" DataSourceID="SqlDataSource5">
                                                                            <asp:ListItem Selected="True" Text="Select" Value="">
                                                                    </asp:ListItem>
                        </asp:DropDownList>


                                                               <%--     <asp:TextBox ID="TextBox2" CssClass="form-control input-sm inline input-medium" runat="server" Text='<%# Bind("ItemType") %>'></asp:TextBox>--%>
                                                                
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Item Sub Category</label>
                                                                
                                                       <%--           <radG:GridDropDownColumn EditFormColumnIndex="2" DataField="ItemSubCatID" DataSourceID="SqlDataSource6"
                                        HeaderText="Item Sub Category" ListTextField="ItemSubCategoryName" ListValueField="ID"
                                        UniqueName="GrdItemSubCategoryNameCol">
                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                    </radG:GridDropDownColumn>--%>
                                                                 <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control input-inline input-sm input-medium"  SelectedValue='<%# Bind("ItemSubCatID") %>'
                             DataTextField="ItemSubCategoryName" DataValueField="ID"   AppendDataBoundItems="True" DataSourceID="SqlDataSource6">
                                                                            <asp:ListItem Selected="True" Text="Select" Value="">
                                                                    </asp:ListItem>
                        </asp:DropDownList>

                                                                 <%--   <asp:TextBox ID="TextBox3" CssClass="form-control input-sm inline input-medium" runat="server" Text='<%# Bind("ItemSubCatID") %>'></asp:TextBox>--%>
                                                                
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Item Code</label>
                                                                
                                                                    <asp:TextBox ID="TextBox4" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtitemcode" MaxLength="50" runat="server" Text='<%# Bind("ItemID") %>'></asp:TextBox>
                                                                
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="control-label">Item Name</label>
                                                                
                                                                    <asp:TextBox ID="TextBox5" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtitemname" MaxLength="50" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                                                                
                                                            </div>

                                                        </div>
                                                        <div class="form-actions text-center">
                                                            <asp:Button ID="btnUpdate" CssClass="btn red insertitem" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                            <asp:Button ID="btnCancel" CssClass="btn default" Text="Cancel" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                        </div>
                                                    </div>
                                                
                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>

                                    <CommandItemSettings AddNewRecordText="Add New Item" />
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

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select [ID], UnitName From Unit WHERE [company_id] = @company_id Or [company_id]=-1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="Select * From(Select 2 ID, 'Inventory' TypeName Union Select 1 ID, 'Non Inventory' TypeName) D">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server" SelectCommand="Select ISC.ID ID, (ISC.ItemSubCategoryID+'-'+ItemSubCategoryName) ItemSubCategoryName from ItemSubCategory ISC Inner Join ItemCategory IC On ISC.Parent_ItemCategoryID = IC.ID Where IC.Company_ID= @company_id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select [ID], ParameterName From Parameter WHERE [company_id] = @company_id Or [company_id]=-1">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Item] (UOM, ItemID, ItemName, Company_ID, ItemType, ItemSubCatID) VALUES (@UOM, @ItemID, @ItemName, @Company_ID, @ItemType, @ItemSubCatID)"
                                SelectCommand="Select * From (Select U.ID UOM, I.ID IDParent, I.ItemID, I.ItemName, I.Company_ID , I.ItemType,I.ItemSubCatID  From Item I Inner Join Unit U  On I.UOM = U.ID) D Where D.Company_ID=@company_id Or D.Company_ID=-1"
                                UpdateCommand="UPDATE [Item] SET [ItemID] = @ItemID, UOM = @UOM, ItemName=@ItemName, ItemType=@ItemType,ItemSubCatID=@ItemSubCatID  WHERE [id] = @IDParent">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ItemID" Type="String" />
                                    <asp:Parameter Name="UOM" Type="Int32" />
                                    <asp:Parameter Name="ItemName" Type="String" />
                                    <asp:Parameter Name="IDParent" Type="Int32" />
                                    <asp:Parameter Name="ItemType" Type="Int32" />
                                    <asp:Parameter Name="ItemSubCatID" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="ItemID" Type="String" />
                                    <asp:Parameter Name="UOM" Type="Int32" />
                                    <asp:Parameter Name="ItemName" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:Parameter Name="ItemType" Type="Int32" />
                                    <asp:Parameter Name="ItemSubCatID" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select ID, ItemID IDChild, ParameterID, ParameterVar From ItemParameter Where ItemID=@ID"
                                UpdateCommand="Update ItemParameter Set ParameterID=@ParameterID, ParameterVar=@ParameterVar Where ID=@ID">
                                <UpdateParameters>
                                    <asp:Parameter Name="ID" Type="Int32" />
                                    <asp:Parameter Name="ParameterID" Type="Int32" />
                                    <asp:Parameter Name="ParameterVar" Type="String" />
                                </UpdateParameters>
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
        $('.insertitem').click(function () {
            return validateitem();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Item?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

        }
        function validateitem() {
            var _message = "";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_drpuom option:selected").text()) === "Select")
                _message += "Please Select UOM <br>";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_DropDownList1 option:selected").text()) === "Select")
                _message += "Please Select Inventory Type <br>";
            if ($.trim($("#RadGrid1_ctl00_ctl02_ctl02_DropDownList2 option:selected").text()) === "Select")
                _message += "Please Select Item Sub Category <br>";
            if ($.trim($("._txtitemcode").val()) === "")
                _message += "Please Input Item Code <br>";
            if ($.trim($("._txtitemname").val()) === "")
                _message += "Please Input Item Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
