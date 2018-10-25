<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAccomadationInfo.aspx.cs" Inherits="SMEPayroll.Management.EmployeeAccomadationInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
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
                        <li>Employment Accommodation Management Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/ManageAccomadation.aspx">Manage Accomodation</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Assign Accommodation</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Accommodation Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
           <%--<uc1:TopRightControl ID="TopRightControl" runat="server" />--%>
         
             
        <!-- content start -->
        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ajaxsettings>
                <radG:AjaxSetting AjaxControlID="RadGrid2">
                    <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="RadGrid2" />
                    </UpdatedControls>
                </radG:AjaxSetting>
            </ajaxsettings>
        </radG:RadAjaxManager>
        <radG:RadCodeBlock ID="RadCodeBlock4" runat="server">

            <script language="javascript" type="text/javascript">
            
                function ShowMsg()
                {
                    var sMsg = '<%=sMsg%>';
                    if (sMsg != '')
                        alert(sMsg);
                }
            </script>

        </radG:RadCodeBlock>

        
                    <radG:RadGrid ID="RadGrid1"  runat="server" GridLines="None" AutoGenerateColumns="False"
                        Skin="Outlook" Width="98%" AllowPaging="True" PageSize="20" AllowFilteringByColumn="True"
                        AllowSorting="true" OnPreRender="RadGrid1_PreRender" OnItemDataBound="RadGrid1_ItemDataBound1"
                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemCommand="RadGrid1_ItemDataBound"
                        OnDeleteCommand="RadGrid1_DeleteCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                       
                        >
                        <MasterTableView  datakeynames="emp_code">
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy" />
                                <ItemStyle BackColor="White" Height="20px"  />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            
                            <Columns>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="EmpCode" HeaderImageUrl="../frames/images/EMPLOYEE/Grid- employee.png"
                                    FilterControlAltText="" HeaderText="Emp Code" CurrentFilterFunction="StartsWith" DataField="emp_code"
                                    Display="false">
                                </radG:GridBoundColumn>

                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                    <ItemTemplate>
                                        <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                    </ItemTemplate>
                                     <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                </radG:GridTemplateColumn>

                                <radG:GridBoundColumn ShowFilterIcon="False"  UniqueName="empname" FilterControlAltText="alphabetsonly" HeaderText="Employee Name"
                                    DataField="empname" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                     <ItemStyle Width="200px" />
                                                <HeaderStyle Width="200px"  />
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn ShowFilterIcon="False"  UniqueName="time_card_no" FilterControlAltText="cleanstring" HeaderText="Time Card ID"
                                    DataField="time_card_no" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <%--<HeaderStyle HorizontalAlign="left" />--%>
                                    <%--<ItemStyle Width="50px" />--%>
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="PassType" FilterControlAltText="cleanstring" HeaderText="Pass Type"
                                    DataField="PassType" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <%--<HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="5px" />--%>
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ic_pp_number" FilterControlAltText="cleanstring" HeaderText="IC/FIN Number"
                                    DataField="ic_pp_number" CurrentFilterFunction="contains" AutoPostBackOnFilter="true">
                                    <%--<HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle Width="30px" />--%>
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="ID"  HeaderText="ID" Visible="false" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="time_card_no">
                                    <%--<ItemStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                               <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="AccomadationCode" Visible="false"   HeaderText="AccommodationCode" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="AccomadationCode">
                                    <%--<ItemStyle Width="8px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="AccomadationName" FilterControlAltText="cleanstring" HeaderText="Accommodation Name" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="AccomadationName">
                                   <%-- <ItemStyle Width="8px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="EffectiveCheckInDate" FilterControlAltText="" HeaderText="CheckIn Date" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="EffectiveCheckInDate">
                                    <%--<ItemStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                                 <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="EffectiveCheckOutDate" FilterControlAltText="" HeaderText="CheckOut Date" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="EffectiveCheckOutDate">
                                    <%--<ItemStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                                  <radG:GridBoundColumn ShowFilterIcon="False" UniqueName="CheckInStatus" FilterControlAltText="cleanstring" HeaderText="Status" CurrentFilterFunction="contains"
                                    AutoPostBackOnFilter="true" DataField="CheckInStatus">
                                    <%--<ItemStyle Width="10px" />
                                    <HeaderStyle HorizontalAlign="left" />--%>
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="editHyperlink">
                                    <ItemTemplate>
                                        <tt class="bodytxt">
                                            <asp:ImageButton ID="btnedit" ToolTip="Edit" AlternateText="Edit" ImageUrl="../frames/images/toolbar/edit.gif"
                                                runat="server" />
                                    </ItemTemplate>
                                     <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                </radG:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />
                                    </ClientSettings>
                    </radG:RadGrid>
                

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
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }

    </script>

</body>
</html>
