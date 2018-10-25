<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAssignedToClaimCappingGroup.aspx.cs"
    Inherits="SMEPayroll.Management.EmployeeAssignedToClaimCappingGroup" %>

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
                        <li>Employee Assigned as Claim Capping</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/AssignmentManagement.aspx">Manage Assignments</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Manage Claim Capping Assignmnent</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employee Assigned as Encashment</h3>--%>
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

                            <label id="infomsg" runat="server"></label>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Claim Group</label>
                                        <asp:DropDownList ID="ClaimGroupSelect" OnSelectedIndexChanged="ClaimGroupSelect_OnSelectedIndexChanged"
                                            DataTextField="CliamGroupName" DataValueField="Id" BackColor="white"
                                            CssClass="bodytxt form-control input-sm" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="row padding-tb-20">
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid2"  runat="server" GridLines="None" AllowSorting="true"
                                        Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                        OnPageIndexChanged="RadGrid2_PageIndexChanged" PagerStyle-AlwaysVisible="true" OnSortCommand="RadGrid1_SortCommand" OnPreRender="RadGrid1_Prerender"
                                        PagerStyle-Mode="NumericPages">
                                        <MasterTableView AllowAutomaticUpdates="True" AutoGenerateColumns="False"
                                            DataKeyNames="ID" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn HeaderText="id" HeaderStyle-HorizontalAlign="Center" Display="false"
                                                    UniqueName="ID" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridTemplateColumn HeaderText="Emp_ID" HeaderStyle-HorizontalAlign="Center" Display="false"
                                                    UniqueName="EmpId" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Emp_ID" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.EmpId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <%--                                        <radG:GridBoundColumn ReadOnly="True" DataField="Time_Card_NO" DataType="System.String" --%>
                                                <%--                                            UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card No">--%>
                                                <%--
                    </radG:GridBoundColumn>--%>
                                                <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName" FilterControlAltText="alphabetsonly"
                                                    Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Un Assigned Employee Name">
                                                    
                                                </radG:GridBoundColumn>

                                                <%--                                          <radG:GridBoundColumn DataField="Trade" AllowFiltering="true" --%>
                                                <%--                                        AutoPostBackOnFilter="true" UniqueName="Trade" Visible="true" SortExpression="Trade"--%>
                                                <%--                                        HeaderText="Trade">--%>
                                                <%--                                        <ItemStyle HorizontalAlign="left" />--%>
                                                <%--
                    </radG:GridBoundColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-2 text-center">
                                    <asp:Button ID="buttonAdd" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm red" />
                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm default" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true" AllowSorting="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound" GridLines="None"
                                        Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated" OnSortCommand="RadGrid1_SortCommand"
                                        PagerStyle-Mode="NumericPages" PagerStyle-AlwaysVisible="true" Width="98%">
                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True"
                                            AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                            DataKeyNames="ID" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn HeaderText="id" HeaderStyle-HorizontalAlign="Center" Display="false"
                                                    UniqueName="ID" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ID" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.ID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridTemplateColumn HeaderText="Emp_ID" HeaderStyle-HorizontalAlign="Center" Display="false"
                                                    UniqueName="EmpId" ItemStyle-Wrap="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Emp_ID" Width="100%" runat="server" CssClass="bodytxt" Text='<%# DataBinder.Eval(Container,"DataItem.EmpId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="12%" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>


                                                <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName" FilterControlAltText="alphabetsonly"
                                                    Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Assigned Employee Name">
                                                    
                                                </radG:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                            </div>

                        </form>


                        <!-- Modal -->
                        <%--<div class="modal fade" id="modalForm" role="dialog">
                        --%>
                        <%--    <div class="modal-dialog">
                        --%>
                        <%--        <div class="modal-content">
                        --%>
                        <%--            <!-- Modal Header -->--%>
                        <%--            <div class="modal-header">
                        --%>
                        <%--                <button type="button" class="close" data-dismiss="modal">
                        --%>
                        <%--                    <span aria-hidden="true">&times;</span>--%>
                        <%--                    <span class="sr-only">Close</span>--%>
                        <%--
                    </button>--%>
                        <%--                <h4 class="modal-title" id="myModalLabel">Add Claim Group</h4>--%>
                        <%--
                </div>--%>
                        <%--            --%>
                        <%--            <!-- Modal Body -->--%>
                        <%--            <div class="modal-body">
                        --%>
                        <%--                <p class="statusMsg"></p>--%>
                        <%--                <form role="form">
                        --%>
                        <%--                    <div class="form-group">
                        --%>
                        <%--                        <label for="inputName">Claim Group Name</label>--%>
                        <%--                        <input type="text" class="form-control" id="inputName" placeholder="Claim Group Name" />--%>
                        <%--
                        </div>--%>
                        <%----%>
                        <%--
                    </form>--%>
                        <%--
                </div>--%>
                        <%--            --%>
                        <%--            <!-- Modal Footer -->--%>
                        <%--            <div class="modal-footer">
                        --%>
                        <%--                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                        <%--                <button type="button" class="btn btn-primary submitBtn" onclick="submitContactForm()">SAVE</button>--%>
                        <%--
                </div>--%>
                        <%--
            </div>--%>
                        <%--
        </div>--%>
                        <%--
    </div>--%>
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
        $('#buttonAdd').click(function () {
            return validatewflassigned();
        });
        $('#buttonDel').click(function () {
            return validatewflunassigned();
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

            var _inputs = $('#RadGrid2_ctl00 thead tr td,#RadGrid1_ctl00 thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

            


        }
        function validatewflassigned() {
            var _message = "";
            var grid = $find("<%= RadGrid2.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if ($.trim($("#ClaimGroupSelect option:selected").text()) === "-select-") {
                _message = "Please Select Claim Group <br>";
            }
            else if (rowCount < 1) {
                _message = "No employee records are found to assign.  <br/>";
            }
            else if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                _message = "Please Select at least one Employee from Unassigned employees <br>";
            }
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validatewflunassigned() {
            var _message = "";
            var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if ($.trim($("#ClaimGroupSelect option:selected").text()) === "-select-") {
                _message = "Please Select Claim Group <br>";
            }
            else if (rowCount < 1){
                _message = "No employee records are found to unassign.  <br/>";
            }
            else if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                _message = "Please Select at least one Employee from Assigned employees <br>";
            }
      
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>



    <%--
    <script>--%>
    <%--    function submitContactForm() {--%>
    <%----%>
    <%----%>
    <%--        var name = $('#inputName').val();--%>
    <%--       --%>
    <%--        if (name.trim() == '') {--%>
    <%--            alert('Please enter Group Name');--%>
    <%--            $('#inputName').focus();--%>
    <%--            return false;--%>
    <%--        } else {--%>
    <%--            $.ajax({--%>
    <%--                type: 'POST',--%>
    <%--                url: 'submit_form.php',--%>
    <%--                data: 'contactFrmSubmit=1&name=' + name + '&email=' + email + '&message=' + message,--%>
    <%--                beforeSend: function () {--%>
    <%--                    $('.submitBtn').attr("disabled", "disabled");--%>
    <%--                    $('.modal-body').css('opacity', '.5');--%>
    <%--                },--%>
    <%--                success: function (msg) {--%>
    <%--                    console.log(msg);--%>
    <%--                }--%>
    <%--            });--%>
    <%--        }--%>
    <%--    }--%>
    <%--</script>--%>
</body>
</html>
