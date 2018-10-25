<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCreator.aspx.cs" Inherits="SMEPayroll.Management.UserCreator" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>

  <%--  <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">--%>
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
window.parent.expand()

}
document.ondblclick=expando 

-->
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">



        
    <script type="text/javascript">
        var category_name = "";
        var changedFlage = "false";
        function OnFocusLost_CategoryName(val) {
            var Object = document.getElementById(val);
            category_name = GetDataFromHtml(Object);
            changedFlage = "true";
        }
        function Validations(sender, args) {
            if (typeof (args) !== "undefined") {
                var commandName = args.get_commandName();
                var commandArgument = args.get_commandArgument();
                switch (commandName) {
                    case "startRunningCommand":
                        //$sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                    case "alertCommand":
                        //$sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                    default:
                        //$sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                        break;
                }
            }
            var result = args.get_commandName();
            //alert(agent_name +"," + phone1 + "," + phone2);
            if (category_name == "" && changedFlage == "false") {
                var itemIndex = args.get_commandArgument();
                //alert(itemIndex);
                var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                if (row != null) {
                    cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                    category_name = cellvalue;
                }
            }
            if (result == 'Update' || result == 'PerformInsert') {
                var sMsg = "";
                var message = "";
                message = MandatoryData(trim(category_name), " Category Name");
                if (message != "")
                    sMsg += message + "<br/>";
                if (sMsg != "") {
                   // args.set_cancel(true);
                    //WarningNotification(sMsg);
                }
            }
        }
    </script>


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
                        <li>Users</li>
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
                            <span>Manage Administrative Users</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Users</h3>--%>
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

                            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>
                            </telerik:RadCodeBlock>

                            <radG:RadGrid ID="RadGrid1"  runat="server" CssClass="userRights"
                                DataSourceID="SqlDataSource1" GridLines="None"
                                Skin="Outlook" Width="93%" OnItemInserted="RadGrid1_ItemInserted" OnItemDataBound="RadGrid1_ItemDataBound" OnItemUpdated="RadGrid1_ItemUpdated" OnItemCommand="RadGrid1_ItemCommand"
                                MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                                MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                                MasterTableView-AllowMultiColumnSorting="true" GroupHeaderItemStyle-HorizontalAlign="left"
                                ClientSettings-EnableRowHoverStyle="true" ClientSettings-AllowColumnsReorder="true"
                                ClientSettings-ReorderColumnsOnClient="true" PageSize="20" AllowPaging="true" AllowSorting="true">
                                <MasterTableView DataSourceID="SqlDataSource1" DataKeyNames="Uid" EditMode="InPlace">
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

                                        <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Uid" DataType="System.Int32"
                                            UniqueName="Uid" Visible="false" SortExpression="Uid" HeaderText="Uid">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="UserName" UniqueName="UserName" HeaderText="User Name"
                                            SortExpression="UserName" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            CurrentFilterFunction="Contains">
                                            <%--<ItemStyle Width="30%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Password" UniqueName="Password" HeaderText="Password"
                                            SortExpression="Password" AllowFiltering="true" AutoPostBackOnFilter="true" DataFormatString="*****"
                                            CurrentFilterFunction="Contains">
                                            <ItemStyle Width="200px" />
                                                <HeaderStyle Width="200px"  />
                                        </radG:GridBoundColumn>

                                        <radG:GridDropDownColumn DataField="RightId" DataSourceID="SqlDataSource2"
                                            HeaderText="User Rights" ListTextField="GroupName" ListValueField="GroupID"
                                            UniqueName="GridDropDownColumn">
                                            <ItemStyle Width="200px"  />
                                                <HeaderStyle Width="200px" />
                                        </radG:GridDropDownColumn>

                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton"  CommandName="Delete" Text="Delete" ConfirmDialogType="RadWindow"
                                            UniqueName="DeleteColumn">

                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                        <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="col-sm-12 text-center margin-top-30">
                                                    <span class="form-title">Add New User</span>
                                                </div>
                                                <div class="col-sm-12">
                                                    <hr />
                                                </div>
                                                <div class="col-sm-8">
                                                    <div class="form">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">
                                                                <label class="col-sm-2 control-label">
                                                                    User Name</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="TextBox2" CssClass="form-control input-sm input-small custom-maxlength ClsUserName" MaxLength="50" TabIndex="1" runat="server" Text='<%# Bind("UserName") %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="col-sm-2 control-label">
                                                                    Password</label>
                                                                <div class="col-sm-10">
                                                                    <asp:TextBox ID="TextBox3" CssClass="form-control input-sm input-small custom-maxlength ClsPassword" MaxLength="12"  TabIndex="2" runat="server" Text='<%# Bind("Password") %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <label class="col-sm-2 control-label">
                                                                    Item Category ID</label>
                                                                <div class="col-sm-10">
                                                                    <%--<asp:TextBox ID="TextBox1" CssClass="form-control input-sm input-small" runat="server" Text='<%# Bind("Parent_ItemCategoryID") %>'></asp:TextBox>--%>
                                                                    <asp:DropDownList ID="ddl1" runat="server" CssClass="form-control input-sm input-small" SelectedValue='<%# Bind("RightId") %>'
                                                                        DataSource='<%# (new string[] { "Employee", "Super Admin"}) %>' TabIndex="7"
                                                                        AppendDataBoundItems="True">
                                                                        <asp:ListItem Selected="True" Text="Select" Value="">
                                                                        </asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-actions">
                                                            <asp:Button ID="btnUpdate" CssClass="btn red ClsBtnUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                            <asp:Button ID="btnCancel" CssClass="btn default" Text="Cancel" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4"></div>
                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>

                                    <CommandItemSettings AddNewRecordText="Add New User" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <%--<ClientEvents OnCommand="Validations" />--%>
                                    <ClientEvents OnRowDblClick="RowDblClick"></ClientEvents>
                                </ClientSettings>
                            </radG:RadGrid>



                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                InsertCommand="INSERT INTO [Users] ([UserName],[Password] ,[RightId],[Company_Id]) VALUES (@UserName, @Password,@RightId,@Company_Id)"
                                SelectCommand=" SELECT [Uid],[UserName],[Password] ,[RightId] FROM [dbo].[Users] where [Company_Id]=@Company_Id"
                                UpdateCommand="UPDATE [Users] SET [UserName] = @UserName,[Password]=@Password,[RightId]=@RightId  WHERE [Uid] = @Uid">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" DefaultValue="1" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="UserName" Type="String" />
                                    <asp:Parameter Name="Password" Type="String" />
                                    <asp:Parameter Name="RightId" Type="Int32" />
                                    <asp:Parameter Name="Uid" Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="UserName" Type="String" />
                                    <asp:Parameter Name="Password" Type="String" />
                                    <asp:Parameter Name="RightId" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </InsertParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                SelectCommand=" select GroupID,GroupName from UserGroups where company_id=@Company_Id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
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
        $(".clsCnfrmButton").click(function () {
               var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
           return GetConfirmation("Are you sure you want to delete this User?", _id, "Confirm Delete", "Delete");
             
        });

        var _cntrl = $("input[type='text']");
        $.each(_cntrl, function (index, value) {
            var _th = $(this).closest('table').find('th').eq(index+1);
            if ($(_th).text() == "User Name")
            {
                $(this).addClass("ClsUserName custom-maxlength form-control input-sm input-large");
                $(this).attr("maxlength", "50");
            }
            if ($(_th).text() == "Password") {
                $(this).addClass("ClsPassword custom-maxlength form-control input-sm input-large");
                $(this).attr("maxlength", "12");
            }
        });

        if ($("input[title='Update']").length > 0)
        $("input[title='Update']").addClass("ClsBtnUpadte");

       // var $th = $td.closest('table').find('th').eq($td.index());
         window.onload = function () {
             CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }

        $("#RadGrid1_ctl00_ctl02_ctl02_PerformInsertButton, .ClsBtnUpadte").click(function () {
            var _msg = "";
            if ($(".ClsUserName").val() == "")
                _msg += "UserName cannot be empty.<br/>";
            if ($(".ClsPassword").val() == "")
                _msg += "Password cannot be empty.<br/>";

            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }

        });
    </script>

</body>
</html>
