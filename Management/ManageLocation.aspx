<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManageLocation.aspx.cs"
    Inherits="SMEPayroll.Management.ManageLocation" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
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
                        <li>Manage Location</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ProjectManagement.aspx">Manage Project</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Location</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Location</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
       
        <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
        
                            <%--<div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button">
                                </div>
                            </div>--%>

        
                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                    <script type="text/javascript">
                        function RowDblClick(sender, eventArgs)
                        {
                            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                        }
                    </script>

                </radG:RadCodeBlock>
                
                            <radG:RadGrid ID="RadGrid1"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook" Width="93%" OnItemInserted="RadGrid1_ItemInserted"
                                OnItemUpdated="RadGrid1_ItemUpdated">
                                <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                    AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                    DataKeyNames="id,isShared">
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

                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                            UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                            <%--<ItemStyle Width="100px" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="Location_Name" UniqueName="Location_Name" HeaderText="Location Name"
                                            SortExpression="Location_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains">
                                            <%--<ItemStyle Width="90%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridDropDownColumn EditFormColumnIndex="1" DataField="isShared" DataSourceID="SqlDataSource4"
                                            ListTextField="isShared" ListValueField="isShared" UniqueName="grdshcol" HeaderText="Shared" >
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridDropDownColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ConfirmText="Delete this record?" ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton" />
                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>
                                    <EditFormSettings ColumnNumber="2">
                                        <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                        <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                        <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                            BackColor="White" Width="100%" />
                                        <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                            BackColor="White" />
                                        <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                        <EditColumn ButtonType="ImageButton" InsertText="Add New Location" UpdateText="Update"
                                            UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                        </EditColumn>
                                        <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                                    </EditFormSettings>
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <CommandItemSettings AddNewRecordText="Add New Location" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                            </radG:RadGrid>
                            <radG:RadGrid ID="RadGrid2"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand ="RadGrid2_InsertCommand" OnUpdateCommand="RadGrid2_UpdateCommand" OnItemCommand="RadGrid2_ItemCommand"
                                AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                DataSourceID="SqlDataSource2" GridLines="None" Skin="Outlook" Width="93%" >
                                <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticUpdates="false" DataSourceID="SqlDataSource2"
                                    AllowAutomaticDeletes="false" AutoGenerateColumns="False" AllowAutomaticInserts="false"
                                    DataKeyNames="id,isShared">
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

                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                            UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                            <%--<ItemStyle Width="100px" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn  DataField="Location_Name" UniqueName="Location_Name" HeaderText="Location Name"  FilterControlAltText="cleanstring" ShowFilterIcon="false"
                                            SortExpression="Location_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains">
                                            <%--<ItemStyle Width="90%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
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
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                                             <FormTemplate>
                                                <div class="clearfix form-style-inner">
                                                    <div class="heading">
                                                        <%--<span class="form-title">Add New Location</span>--%>
                           <asp:Label ID="Location" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Location" : "Edit Location" %>'
        runat="server"></asp:Label>
                                                    </div>
                                                    
                                                        <hr />
                                                   
                                                    
                                                        <div class="form-inline">
                                                            <div class="form-body">
                                                                <div class="form-group clearfix">
                                                                    <label>Location Name</label>
                                                                    <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtlocation" MaxLength="50" runat="server" Text='<%# Bind("Location_Name") %>'></asp:TextBox>
                                                                </div>
                                                                <div class="form-group clearfix">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red insertlocation  margin-top-0" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                                <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CommandName="Cancel"></asp:Button>
                                                            </div>
                                                            </div>
                                                            
                                                        </div>
                                                    
                                                    
                                                </div>
                                            </FormTemplate>
                                                        </EditFormSettings>

                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <CommandItemSettings AddNewRecordText="Add New Location" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                            </radG:RadGrid>

                <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="Select isShared From (Select 'NO' isShared Union Select 'YES' isShared ) D">
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Location] (Company_ID, [Location_Name],isShared) VALUES (@company_id, @Location_Name, @isShared)"
                    SelectCommand="SELECT [id], [Location_Name],isShared FROM [Location]  WHERE [company_id] = @company_id Or isShared='YES'"
                    UpdateCommand="UPDATE [Location] SET [Location_Name] = @Location_Name,isShared=@isShared WHERE [id] = @id">
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Location_Name" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Location_Name" Type="String" />
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </InsertParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server"  SelectCommand="SELECT [id], [Location_Name],isShared FROM [Location]  WHERE ([company_id] = @company_id) Or isShared='YES'">
                <%--    InsertCommand="INSERT INTO [Location] (Company_ID, [Location_Name],isShared) VALUES (@company_id, @Location_Name,'NO')"
                    UpdateCommand="UPDATE [Location] SET [Location_Name] = @Location_Name WHERE [id] = @id">--%>
                    <SelectParameters>
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </SelectParameters>
                  <%--  <UpdateParameters>
                        <asp:Parameter Name="Location_Name" Type="String" />
                        <asp:Parameter Name="id" Type="Int32" />
                    </UpdateParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Location_Name" Type="String" />
                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                    </InsertParameters>--%>
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
        $('.insertlocation').click(function () {
            return validatelocation();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Location?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid2_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

        }
        function validatelocation() {
            var _message = "";
            if ($.trim($("._txtlocation").val()) === "")
                _message += "Please Input Location Name <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
