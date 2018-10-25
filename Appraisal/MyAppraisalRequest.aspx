<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyAppraisalRequest.aspx.cs" Inherits="SMEPayroll.Appraisal.MyAppraisalRequest" %>


<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>SMEPayroll</title>





    <%--<link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />--%>


    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/components-md.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Style/metronic/plugins-md.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/layout.min.css" rel="stylesheet" type="text/css" />
    <link href="../Style/metronic/themes/default.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Style/metronic/custom-internal.min.css" rel="stylesheet" type="text/css" />




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
    <uc1:TopRightControl ID="TopRightControl1" runat="server" />
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
                            <a href="index.html">Home</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Tables</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <h3 class="page-title"> My Appraisal Requests</h3>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form2" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>


                          <%--  <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-8">
                                    <div class="form-group">
                                        <label>From</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePickerFrom" runat="server" Width="150px" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.DueDate")%>'>
                                            <DateInput ID="DateInputF" runat="server" Skin="" CssClass="form-control input-sm" DateFormat="dd-MM-yyyy">
                                            </DateInput>
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>To</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" ID="RadDatePickerTo" runat="server" Width="150px" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.DueDate")%>'>
                                            <DateInput ID="DateInput1" runat="server" Skin="" CssClass="form-control input-sm" DateFormat="dd-MM-yyyy">
                                            </DateInput>
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                        <label>Status</label>
                                        <asp:DropDownList ID="drstatus" runat="server" CssClass="textfields form-control input-sm">
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Open">Open</asp:ListItem>
                                            <asp:ListItem Value="Accepted">Accepted</asp:ListItem>
                                            <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:ImageButton ID="imgbtnfetch" OnClick="imgbtnfetch_Click" CssClass="btn" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                    </div>
                                </div>

                            </div>--%>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>


                            <radG:RadGrid ID="RadGrid1" runat="server" AllowSorting="True"
                                DataSourceID="SqlDataSource2" GridLines="None" Skin="Outlook" Width="100%">
                                <MasterTableView DataSourceID="SqlDataSource2" AllowAutomaticDeletes="True" AutoGenerateColumns="False"
                                    DataKeyNames="RequestId" TableLayout="Auto">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />

                                    <Columns>
                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="1%" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="RequestId" DataType="System.Int32" UniqueName="RequestId"
                                            SortExpression="RequestId" ReadOnly="True" Visible="False" HeaderText="RequestId">
                                            <ItemStyle Width="1%" />
                                        </radG:GridBoundColumn>



                                        <radG:GridBoundColumn DataField="Manager" UniqueName="Manager" SortExpression="Manager"
                                            HeaderText="Manager">
                                            <ItemStyle Width="15%" />
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="Description" UniqueName="Description"
                                            SortExpression="Description" HeaderText="Remarks">
                                            <ItemStyle Width="40%" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="RequestDate" DataType="System.DateTime" DataFormatString="{0:dd MMM,yyyy}" UniqueName="RequestDate"
                                            SortExpression="RequestDate" HeaderText="Request Date">
                                            <ItemStyle Width="10%" />
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="status" DataType="System.Int32" UniqueName="status"
                                            SortExpression="status" HeaderText="Status">
                                            <ItemStyle Width="10%" />
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="EmpId" DataType="System.Int32" UniqueName="EmpId"
                                            SortExpression="EmpId" Visible="False" HeaderText="EmpId">
                                            <ItemStyle Width="1%" />
                                        </radG:GridBoundColumn>



                                        <radG:GridButtonColumn HeaderText="Delete" ButtonType="ImageButton" ConfirmText="Are you sure you want to delete this objective?"
                                            CommandName="Delete" Text="Delete" UniqueName="Deletecolumn">
                                            <ItemStyle Width="1%" />
                                        </radG:GridButtonColumn>


                                    </Columns>


                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>


                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>




                            <asp:Label ForeColor="red" Font-Bold="true" ID="lblMsg" runat="Server" Text=""></asp:Label>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select eo.*,e.emp_name as Manager from Appraisal_Request eo inner join employee e on e.emp_code = eo.EmpId where eo.MgrId=@mgrId order by RequestDate desc">
                                <SelectParameters>
                                    <asp:SessionParameter Name="mgrId" SessionField="EmpCode" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <br />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadDatePickerFrom" Display="None" ErrorMessage="Appraisal Request - From Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadDatePickerTo" Display="None" ErrorMessage="Appraisal Request - To Date Required!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cmpEndDate" runat="server" ErrorMessage="Appraisal Request : To date cannot be less than start date"  ControlToCompare="RadDatePickerFrom" ControlToValidate="RadDatePickerTo" Operator="GreaterThanEqual" Type="Date"> </asp:CompareValidator>
                             
                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
        <!-- END CONTENT -->









        <!-- BEGIN QUICK SIDEBAR -->
        <a href="javascript:;" class="page-quick-sidebar-toggler">
            <i class="icon-close"></i>
        </a>
        <div class="page-quick-sidebar-wrapper" data-close-on-body-click="false">
            <div class="page-quick-sidebar">

                <div class="tab-content">
                    <div class="tab-pane active page-quick-sidebar-chat" id="quick_sidebar_tab_1">
                        <div class="page-quick-sidebar-chat-users" data-rail-color="#ddd" data-wrapper-class="page-quick-sidebar-list">
                            <h3 class="list-heading">&nbsp;</h3>
                            <ul class="media-list list-items">
                                <li class="media">
                                    <i class="fa fa-building"></i>
                                    <%--<img class="media-object" src="../assets/img/right-sidebar-icons/company.png" alt="..." data-pin-nopin="true" />--%>
                                    <div class="media-body">
                                        <h4 class="media-heading">Company Name</h4>
                                        <div class="media-heading-sub">Demo Company Pte Ltd </div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">Client Name</h4>
                                        <div class="media-heading-sub">SantyKKumar</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-users"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Group</h4>
                                        <div class="media-heading-sub">Super Admin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-user"></i>
                                    <div class="media-body">
                                        <h4 class="media-heading">User Name</h4>
                                        <div class="media-heading-sub">DPTAdmin</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-danger">Expired</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Expirey</h4>
                                        <div class="media-heading-sub">04/07/2017</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-certificate"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-info">961 Remaining</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Detail</h4>
                                        <div class="media-heading-sub">1000 - 39=961</div>
                                    </div>
                                </li>
                                <li class="media">
                                    <i class="fa fa-calendar"></i>
                                    <div class="media-status">
                                        <span class="label label-sm label-warning">10 Days Left</span>
                                    </div>
                                    <div class="media-body">
                                        <h4 class="media-heading">License Renewal</h4>
                                        <div class="media-heading-sub">December 31,2016</div>
                                    </div>
                                </li>
                            </ul>

                        </div>

                    </div>


                </div>
            </div>
        </div>
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
    <script src="../scripts/metronic/jquery.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/js.cookie.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/bootstrap-switch.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/app.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/components-color-pickers.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/layout.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/demo.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/quick-sidebar.min.js" type="text/javascript"></script>
    <script src="../scripts/metronic/custom.js" type="text/javascript"></script>


</body>
</html>
