<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviousYearWages.aspx.cs" Inherits="SMEPayroll.employee.PreviousYearWages" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />


    <script type="text/javascript" language="javascript">
        function isNumericKeyStrokeDecimal(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                return false;

            return true;
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
                        <li>Previous Year Wages</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/OtherManagement.aspx">Manage Others</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Previous Year Wages</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Previous Year Wages</h3>--%>
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

                            <%--<div class="col-md-12 padding-tb-10">
                                EMPLOYEE LAST YEAR WAGES
                            </div>--%>



                            <center>
                                <asp:Label ID="lblmessage" class="bodytxt" runat="server"></asp:Label></center>
                            <br />
                            <%--Skin="Default"--%>
                            <radG:RadGrid ID="RadGrid1"  AllowMultiRowEdit="True" OnNeedDataSource="RadGrid1_NeedDataSource"
                                OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="Radgrid1_databound" Skin="Outlook"
                                Width="95%" runat="server" GridLines="None" AllowPaging="true" PageSize="50" EnableHeaderContextMenu="true">
                                <MasterTableView CommandItemDisplay="bottom" DataKeyNames="emp_code" EditMode="EditForms"
                                    AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    AllowAutomaticDeletes="true">
                                    <CommandItemTemplate>
                                        <div style="text-align: center">
                                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CommandName="UpdateAll" CssClass="btn red" />
                                            <%--<input id="Button3" type="button" onclick="history.go(-1)" value="Back" class="btn default" />--%>
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="false" DataType="System.Int32"
                                            HeaderText="emp_code" SortExpression="emp_code" UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                            <%-- <ItemStyle Width="30%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="OWLastYear" Visible="false" UniqueName="OWLastYearID" AutoPostBackOnFilter="true" CurrentFilterFunction="contains">
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn UniqueName="OWLastYear" HeaderText="Last Year"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:DropDownList CssClass="form-control input-sm" ID="OWLastYear" runat="server" DataSourceID="xmldtYears" DataTextField="text" DataValueField="id"></asp:DropDownList>
                                            </ItemTemplate>
                                             <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridTemplateColumn UniqueName="LYOW" HeaderText="Last Year Total Ordinary Wages"
                                            AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLYOW" CssClass="form-control input-sm text-right custom-maxlength number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)"
                                                    runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.LYTotalOW")%>'></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="NumerictxtLYOW" runat="server" ControlToValidate="txtLYOW"
                                                    ErrorMessage="Wages should be numeric" ValidationExpression="^\d*\.{0,1}\d+$"
                                                    Display="Dynamic" />
                                            </ItemTemplate>
                                               <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="200px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="time_card_no" HeaderText="Time Card ID" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="time_card_no" UniqueName="time_card_no" Display="true">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number" DataField="ic_pp_number" Display="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>

                                    </Columns>
                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px" />
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px" />
                                    </RowIndicatorColumn>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                       <%-- <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                    </ClientSettings>
                            </radG:RadGrid>
                            <asp:XmlDataSource ID="xmldtYears" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
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
        }
    </script>

</body>
</html>
