<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpCeilingAssign.aspx.cs" Inherits="SMEPayroll.Payroll.EmpCeilingAssign" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
                        <li>Employee Assigned To Payroll Ceiling</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="EmpCeilingMaster.aspx"><span>Payroll Ceiling</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Employee Assigned To Payroll Ceiling</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Employment Management Form</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>
                                <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
                                </script>
                                <script type="text/javascript">
                                    var type = "";
                                    var changedFlage = "false";

                                    //Leave Type
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
                                        if (type == "" && changedFlage == "false") {
                                            var itemIndex = args.get_commandArgument();
                                            var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                                            if (row != null) {
                                                cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                                                type = cellvalue;
                                            }
                                        }
                                        if (result == 'Update' || result == 'PerformInsert') {
                                            var sMsg = "";
                                            var message = "";
                                            message = MandatoryData(trim(type), "Leave Type");
                                            if (message != "")
                                                sMsg += message + "\n";

                                            if (sMsg != "") {
                                                args.set_cancel(true);
                                                alert(sMsg);
                                            }
                                        }
                                    }

                                    //Onlost Focus Of the Manual Leave Type
                                    function OnFocusLost_type(val) {
                                        var Object = document.getElementById(val);
                                        type = GetDataFromHtml(Object);
                                        changedFlage = "true";
                                    }
                                </script>


                            </radG:RadCodeBlock>
                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>
                            <table cellpadding="0" cellspacing="0" width="100%"
                                border="0">
                                <tr style="display: none">
                                    <td>
                                        <table cellpadding="5" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 19px">
                                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Payroll Ceiling</b></font>
                                                </td>
                                            </tr>
                                            <tr bgcolor="#E5E5E5">
                                                <td align="right" style="height: 25px">
                                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                                        style="width: 80px; height: 22px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%" visible="false" style="display: none"
                                border="0">
                                <tr>
                                    <td align="center">
                                        <radG:RadGrid ID="RadGrid5" AllowFilteringByColumn="true" OnDeleteCommand="RadGrid1_DeleteCommand"
                                            OnItemDataBound="RadGrid1_ItemDataBound" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                            AllowAutomaticDeletes="True" runat="server" GridLines="None"
                                            Skin="Outlook" Width="40%">
                                            <MasterTableView CommandItemDisplay="Bottom" AutoGenerateColumns="False">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridBoundColumn ReadOnly="True" AllowFiltering="true" Visible="True" DataField="emp_code"
                                                        UniqueName="emp_code" SortExpression="emp_code" HeaderText="EmpCode" ShowFilterIcon="false" AutoPostBackOnFilter ="true"  >
                                                        <ItemStyle Width="10px"></ItemStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </radG:GridBoundColumn>

                                                    <radG:GridBoundColumn ReadOnly="True" AllowFiltering="false" DataField="emp_name"
                                                        UniqueName="emp_name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="Employee Name">
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </radG:GridBoundColumn>
                                                    <radG:GridTemplateColumn HeaderText="Selected" HeaderStyle-Width="10%" UniqueName="Selected" AllowFiltering="false" ItemStyle-HorizontalAlign="center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelected" ToolTip='<%# Bind( "emp_code" ) %>' runat="server" AutoPostBack="true" OnCheckedChanged="CheckChanged" />
                                                        </ItemTemplate>
                                                    </radG:GridTemplateColumn>

                                                </Columns>
                                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" />

                                            </MasterTableView>
                                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                                <ClientEvents OnCommand="Validations" />
                                            </ClientSettings>
                                        </radG:RadGrid></td>
                                </tr>

                            </table>






                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>



                            <div class="clearfix search-box padding-tb-10 hidden">
                                <div class="col-md-12 form-inline">
                                    <div class="form-group">
                                        <label>Type</label>
                                        <asp:DropDownList OnDataBound="drpTypeID_databound" ID="drpTypeID"
                                            DataTextField="TypeName" DataValueField="ID" BackColor="white"
                                            CssClass="bodytxt form-control input-sm" DataSourceID="SqlDataSource3" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid2" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
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
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn Display="True" ReadOnly="True" DataField="emp_code" DataType="System.Int32"
                                                    UniqueName="emp_code" Visible="true" SortExpression="emp_code" HeaderText="Emp Code" AllowFiltering ="true" AutoPostBackOnFilter ="true" ShowFilterIcon ="false" FilterControlAltText="numericonly" >
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_name" DataType="System.String" UniqueName="emp_name" FilterControlAltText="alphabetsonly"
                                                    Visible="true" SortExpression="emp_name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Employee Name">
                                                    <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                </radG:GridBoundColumn>

                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="400px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-2 text-center">
                                    <asp:Button ID="buttonAdd" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm red" />
                                    <asp:Button ID="buttonDel" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                        CssClass="btn btn-sm Default" />
                                </div>
                                <div class="col-md-5">
                                    <radG:RadGrid ID="RadGrid1" runat="server" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        AllowFilteringByColumn="true" AllowMultiRowSelection="true" AllowSorting="true"
                                        OnItemDataBound="RadGrid1_ItemDataBound" DataSourceID="SqlDataSource1" GridLines="None"
                                        Skin="Outlook" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                        PagerStyle-Mode="NumericPages" PagerStyle-AlwaysVisible="true" Width="98%">

                                        <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                            AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                            DataKeyNames="ID" AllowPaging="true" PageSize="50">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridClientSelectColumn UniqueName="Assigned">
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn ReadOnly="True" DataField="EmpCode" DataType="System.String" FilterControlAltText="numericonly"
                                                    UniqueName="EmpCode" Visible="true" SortExpression="EmpCode" HeaderText="Emp Code" ShowFilterIcon ="false" AllowFiltering ="true" AutoPostBackOnFilter ="true" >
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="emp_name" DataType="System.String" UniqueName="emp_name" FilterControlAltText="alphabetsonly"
                                                    Visible="true" SortExpression="emp_name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Employee Name">
                                                    <%--<ItemStyle Width="90%" HorizontalAlign="left" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Id" DataType="System.Int32"
                                                    UniqueName="Id" Visible="true" SortExpression="Id" HeaderText="Id">
                                                    <ItemStyle Width="0px" />
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="400px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                            </div>




                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select 1 ID, 'Workers' TypeName"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="Select  emp_code,emp_name from employee where termination_date is null and  Company_Id=@company_id AND emp_code not in ((Select empcode from CeilingEmployee  where CompanyId=@company_id) )Order By emp_name Asc">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />

                                    <asp:ControlParameter ControlID="drpTypeID" Name="TypeID" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:SessionParameter Name="Emp_Code" SessionField="EmpCode" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <%--SelectCommand="Select (select trade from trade where id=EM.Trade_id) as Trade,EA.ID ID, EA.RefID, EA.Emp_ID, (EM.Emp_Name+' '+EM.emp_LName) EmpName, EM.Time_Card_NO From EmployeeAssignedToWorkersList EA Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code">--%>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                                SelectCommand="Select CeilingEmployee.Id,CeilingEmployee.EmpCode,Employee.emp_name  from CeilingEmployee   INNER JOIN Employee on  CeilingEmployee.empcode=EMployee.emp_code  where EMployee.Company_Id=@company_id Order By emp_name Asc">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:ControlParameter ControlID="drpTypeID" Name="TypeID" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>

                            <asp:SqlDataSource ID="SqlDataSource9" runat="server"
                                SelectCommand="SELECT [id],[Currency] + '--->' + [Symbol] Currency, [Symbol],[Selected] FROM [Currency] order by Currency Asc"></asp:SqlDataSource>
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
        $('#buttonAdd').click(function () {
            return validatewflassigned();
        });
        $('#buttonDel').click(function () {
            return validatewflunassigned();
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

            var _inputs = $('#RadGrid2_ctl00_Header thead tr td,#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })

            


        }
        function validatewflassigned() {
            var _message = "";
            if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message += "Please Select at least one Employee from Unassigned Employees <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
        function validatewflunassigned() {
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message += "Please Select at least one Employee from Assigned employees <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>
</body>
</html>
