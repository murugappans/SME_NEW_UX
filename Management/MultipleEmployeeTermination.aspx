<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MultipleEmployeeTermination.aspx.cs"
    Inherits="SMEPayroll.Management.MultipleEmployeeTermination" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
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
        window.parent.defaultconf=window.parent.document.sendemailbody.cols
        function expando(){
        window.parent.expandf()

        }
        document.ondblclick=expando 

        -->
    </script>





</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white page-md page-sidebar-closed" onload="ShowMsg();">


    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
           
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
                        <li>Multiple Employee Termination</li>
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
                            <span>Multiple Employee Resignation</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Multiple Employee Termination</h3>--%>
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
                            <div class="margin-top-10">
                                <asp:Label ID="lblError" runat="server"></asp:Label>

                            </div>

                            <div class="row padding-tb-20">
                                <div class="col-md-5">
                                    <label>Enter Termination/Resignation Date</label>
                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdStart"
                                        runat="server">
                                        <Calendar runat="server">
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />                                        
                                    </radG:RadDatePicker>
                                </div>
                                <div class="col-md-2"></div>
                                <div class="col-md-5">
                                    <asp:Button CssClass="btn default" ID="btnTerminate" runat="server" Text="Select the employee & Click to Proceed"  OnClick="btnTerminate_Click" />
                                </div>
                            </div>



                            
                                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                        <script type="text/javascript">
                                            function RowDblClick(sender, eventArgs) {
                                                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                            }
                                        </script>

                                    </radG:RadCodeBlock>

                            <div class="row">
                                    <div class="col-md-5">
                                        <radG:RadGrid ID="RadGrid2"  runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                            Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                            OnPageIndexChanged="RadGrid2_PageIndexChanged" PagerStyle-AlwaysVisible="true"
                                            PagerStyle-Mode="NumericPages">
                                            <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource2" AutoGenerateColumns="False"
                                                DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridBoundColumn   Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                        UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn FilterControlAltText="cleanstring"  ReadOnly="True" ShowFilterIcon="false" DataField="Time_Card_NO" DataType="System.String"
                                                        UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card No"  AllowFiltering="true" AutoPostBackOnFilter ="true">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn FilterControlAltText="alphabetsonly"  DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                                        Visible="true" SortExpression="Emp_Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                        HeaderText="Active Employees">
                                                        <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                    </radG:GridBoundColumn>

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
                                        <br />
                                        <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                            CssClass="btn btn-sm default" />
                                    </div>
                                    <div class="col-md-5">
                                        <radG:RadGrid ID="RadGrid1"  runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                            AllowFilteringByColumn="true" AllowMultiRowSelection="true" AllowSorting="true"
                                            OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" GridLines="None"
                                            Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                            PagerStyle-Mode="NumericPages" PagerStyle-AlwaysVisible="true" Width="98%">
                                            <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                                AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                                DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridBoundColumn  Display="false" ReadOnly="True" DataField="ID" DataType="System.Int32"
                                                        UniqueName="ID" Visible="true" SortExpression="ID" HeaderText="ID">
                                                        <%--<ItemStyle Width="0px" />--%>
                                                    </radG:GridBoundColumn>
                                                    <radG:GridClientSelectColumn UniqueName="Assigned">
                                                        <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    </radG:GridClientSelectColumn>
                                                    <radG:GridBoundColumn FilterControlAltText="cleanstring"  ReadOnly="True" DataField="Time_Card_NO" DataType="System.String"
                                                        UniqueName="Time_Card_NO" Visible="true" SortExpression="Time_Card_NO" HeaderText="Time Card No" AllowFiltering="true" AutoPostBackOnFilter ="true">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn FilterControlAltText="alphabetsonly"  DataField="Emp_Name" DataType="System.String" UniqueName="EmpName"
                                                        Visible="true" SortExpression="Emp_Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                        HeaderText="Employees to be terminated / resigned">
                                                        <%--<ItemStyle Width="90%" HorizontalAlign="left" />--%>
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
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                        SelectCommand="Select Time_Card_No, Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name From Employee where StatusID=1 And Termination_Date is null And Company_ID=@company_id and ic_pp_number!='s000000' and StatusId='1'  Order By Emp_Name">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                        SelectCommand="Select Time_Card_No, TE.Emp_Code, (E.Emp_Name+' '+E.Emp_LName) Emp_Name From [Temp_Emp] TE left join Employee E on TE.Emp_code=E.emp_code  where  TE.Company_ID=@company_id  Order By Emp_Name">
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

          window.onload = function () {
           CallNotification('<%=ViewState["actionMessage"].ToString() %>');
              var _inputs = $('#RadGrid2_ctl00_Header thead tr td, #RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
          }
        $("#btnTerminate").click(function(){
            var _msg = "";
             var grid = $find("<%= RadGrid1.ClientID %>"); 
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1)
                _msg += "First Assign employee to terminate.  <br/>";
            else
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) 
                _msg += "Atleast one record must be selected from assigned employees. <br/>";

            if ($("#rdStart_dateInput_text").val() == "")
                _msg += "Termination/Resignation date cannot be empty. <br/>";

            

            if(_msg != "")
            {
                WarningNotification(_msg);
                return false;
            }
        });
        $("#buttonAdd").click(function () {
            var _msg = "";
            var grid = $find("<%= RadGrid2.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1)
                _msg += "No employee records are found to assign.  <br/>";
            else
                if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _msg += "Atleast one record must be selected to assign. <br/>";

            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }
        });
        $("#buttonDel").click(function () {
            var _msg = "";
            var grid = $find("<%= RadGrid1.ClientID %>");
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1)
                _msg += "No employee records are found to unassign.  <br/>";
            else
                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _msg += "Atleast one record must be selected to unassign. <br/>";

            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }
        });

    </script>

</body>
</html>
