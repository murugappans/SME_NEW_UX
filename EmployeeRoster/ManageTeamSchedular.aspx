<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageTeamSchedular.aspx.cs" Inherits="SMEPayroll.EmployeeRoster.ManageTeamSchedular" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Src="~/Frames/bundle_css.ascx" TagPrefix="uc1" TagName="bundle_css" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc1:bundle_css runat="server" ID="bundle_css" />


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
                        <li>Manage Team Schedular</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="rosterDashboard"> Employee Scheduler</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Team</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Roster</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            

                            <asp:Button ID="AddEmpRoster" runat="server" OnClick="AddEmpClick" Text="Assign Employee to Roster" />

                            <div>
                                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                    <script type="text/javascript">
                                        function RowDblClick(sender, eventArgs) {
                                            sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                        }
                </script>

                                </radG:RadCodeBlock>


                                <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                border="0">
                <tr>
                    <td>--%>


                                <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                    DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook" Width="99%" OnItemInserted="RadGrid1_ItemInserted"
                                    OnItemUpdated="RadGrid1_ItemUpdated">
                                    <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                        AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                        DataKeyNames="id">
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <HeaderStyle ForeColor="Navy" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                        <Columns>

                                            <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                <ItemTemplate>
                                                    <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="35px" />
                                                <HeaderStyle Width="35px" />
                                            </radG:GridTemplateColumn>

                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="id" DataType="System.Int32"
                                                UniqueName="id" Visible="true" SortExpression="id" HeaderText="Id">
                                                <%--<ItemStyle Width="100px" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Team_Name" UniqueName="Team_Name" HeaderText="Team Name" FilterControlAltText="cleanstring"
                                                SortExpression="Team_Name" AllowFiltering="true" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains">
                                                <%--<ItemStyle Width="90%" HorizontalAlign="Left" />--%>
                                            </radG:GridBoundColumn>
                                            <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                <ItemStyle Width="35px" />
                                                <HeaderStyle Width="35px" />
                                            </radG:GridEditCommandColumn>
                                            <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                UniqueName="DeleteColumn">
                                                <ItemStyle Width="35px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                            <HeaderStyle Width="35px" />
                                            </radG:GridButtonColumn>
                                        </Columns>
                                        <%--<ItemTemplate> <div class="clearfix form-style-inner"></div></ItemTemplate>--%>

                                        <EditFormSettings EditFormType="Template">
                                            <FormTemplate>
                                                <div class="clearfix form-style-inner">
                                                    <div class="col-sm-12 text-center margin-top-30">
                                                        <span class="form-title"><%# (Container is GridEditFormInsertItem) ? "Add New Team" : "Edit Team" %></span>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <hr />
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class="form">
                                                            <div class="form-body">
                                                                <div class="form-group clearfix">
                                                                    <label>Team Name</label>
                                                                    <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium" runat="server" Text='<%# Bind("Team_Name") %>'></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-actions">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                                <asp:Button ID="btnCancel" CssClass="btn default" Text="Cancel" runat="server" CausesValidation="False"
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
                                        <CommandItemSettings AddNewRecordText="Add New Team" />
                                    </MasterTableView>
                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                </radG:RadGrid>

                                <%--</td>
                </tr>
            </table>--%>

                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [TeamScheduler] (Company_ID, [Team_Name]) VALUES (@company_id, @Team_Name)"
                                    SelectCommand="SELECT [id], [Team_Name] FROM [TeamScheduler]  WHERE [company_id] = @company_id"
                                    UpdateCommand="UPDATE [TeamScheduler] SET [Team_Name] = @Team_Name WHERE [id] = @id">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </SelectParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="Team_Name" Type="String" />
                                        <asp:Parameter Name="id" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:Parameter Name="Team_Name" Type="String" />
                                        <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    </InsertParameters>
                                </asp:SqlDataSource>
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

    <uc_js:bundle_js runat="server" ID="bundle_js" />
  
    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
        $(document).ready(function () {
            $(RadGrid1_ctl00_ctl02_ctl02_TextBox1).addClass("custom-maxlength");
            $(RadGrid1_ctl00_ctl02_ctl02_TextBox1).attr("MaxLength", 50);
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this record?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
    </script>

</body>
</html>
