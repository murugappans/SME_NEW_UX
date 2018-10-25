<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PayrollWorkflowAssignment.aspx.cs"
    Inherits="SMEPayroll.Management.PayrollWorkflowAssignment" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="SMEPayroll" Namespace="SMEPayroll" TagPrefix="sds" %>
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
                        <li>Payroll Workflow Assignment</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/WorkflowManagement.aspx">Manage Workflow</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Assign Employee to Workflow</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Payroll Workflow Assignment</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>


                            <div class="search-box clearfix padding-tb-10">

                                <table width="100%" runat="server" id="tablWf2">
                                    <tr visible="false" id="tr1" runat="server">
                                        <td>
                                            <div class="form-inline col-sm-12">
                                            <div class="form-group">
                                                <label>Workflow Name</label>
                                                <asp:DropDownList OnDataBound="drpWorkFlow_databound" ID="drpWorkFlow"
                                                    DataTextField="WorkFlowName" DataValueField="ID" BackColor="White" DataSourceID="SqlDataSource4"
                                                    runat="server"  AutoPostBack="true" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                            
                                                </div>
                                        </td>
                                    </tr>
                                    <tr visible="false" id="tr2" runat="server">
                                        <td>
                                            <div class="form-inline col-sm-12">
                                            <div class="form-group">
                                                <label>Payroll</label>
                                                <asp:DropDownList  BackColor="white" OnDataBound="drpSubProjectID_databound"
                                                    ID="drpSubProjectID" DataTextField="WorkFlowName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                                    runat="server" AutoPostBack="true" CssClass="form-control input-sm">
                                                </asp:DropDownList>
                                            </div>
                                                </div>
                                            
                                        </td>
                                    </tr>
                                </table>




                            </div>


                            <table id="table1" border="0" cellpadding="0" cellspacing="0" width="90%">
                                        <tr runat="server" id="rw1">
                                            <td style="width: 0%"></td>
                                            <td style="width: 40%"></td>
                                            <td style="width: 20%"></td>
                                            <td style="width: 40%"></td>
                                        </tr>
                                        
                                        <tr runat="server" id="rw3">
                                            <td style="width: 0%"></td>
                                            <td style="width: 40%"></td>
                                            <td style="width: 20%"></td>
                                            <td style="width: 40%"></td>
                                        </tr>
                                    </table>



                            <div>
                                <center>
                                    <br />
                                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                        <script type="text/javascript">
                                            function RowDblClick(sender, eventArgs) {
                                                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                            }
                                        </script>

                                    </radG:RadCodeBlock>




                                     <div class="col-md-5">

                                        <table width="100%">
                                            <tr runat="server" id="radWf11" visible="false">
                                                <td align="left" valign="top">
                                                    <radG:RadGrid ID="RadGrid2"  runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                                    Skin="Outlook" Width="98%" OnPreRender="RadGrid2_PreRender" AllowFilteringByColumn="true"
                                                    AllowMultiRowSelection="true" OnPageIndexChanged="RadGrid2_PageIndexChanged"
                                                    PagerStyle-Mode="NumericPages">
                                                    <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages"
                                                        DataSourceID="SqlDataSource2" AutoGenerateColumns="False" DataKeyNames="Emp_Code"
                                                        AllowPaging="true" PageSize="20">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn Display="false" ShowFilterIcon="False" ReadOnly="True" DataField="Emp_Code"
                                                                DataType="System.Int32" UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code"
                                                                HeaderText="Emp_Code">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                                HeaderText="Employee Name">
                                                                <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                                </radG:RadGrid>
                                                </td>

                                            </tr>
                                        </table>

                                    </div>

                                    <div class="col-md-2">
                                        <table width="100%">
                                            <tr runat="server" id="rw2">
                                                <td class="text-center">
                                                    <asp:Button ID="buttonAdd" CssClass="btn btn-sm red" runat="server" Text="Assign" OnClick="buttonAdd_Click"
                                                         />
                                                    <br />
                                                    <asp:Button ID="buttonDel" CssClass="btn btn-sm default" runat="server" Text="Un-Assign" OnClick="buttonAdd_Click"
                                                         />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="col-md-5">
                                        <table width="100%">
                                            <tr runat="server" id="radWf11second" visible="false">
                                                <td valign="top">
                                                    
                                                    <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource1" GridLines="None"
                                                    Skin="Outlook" Width="98%" AllowFilteringByColumn="true" AllowMultiRowSelection="true"
                                                    OnItemDataBound="RadGrid1_ItemDataBound" OnPreRender="RadGrid1_PreRender" OnItemInserted="RadGrid1_ItemInserted"
                                                    OnItemUpdated="RadGrid1_ItemUpdated" OnDeleteCommand="RadGrid1_DeleteCommand"
                                                    PagerStyle-Mode="NumericPages">
                                                    <MasterTableView AllowAutomaticUpdates="True" PagerStyle-AlwaysVisible="true" and
                                                        PagerStyle-Mode="NumericPages" DataSourceID="SqlDataSource1" AutoGenerateColumns="False"
                                                        DataKeyNames="Emp_Code" AllowPaging="true" PageSize="20">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn ShowFilterIcon="False" Display="false" ReadOnly="True" DataField="Emp_Code"
                                                                DataType="System.Int32" UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code"
                                                                HeaderText="Emp_Code">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Name" DataType="System.String" AllowFiltering="true"
                                                                AutoPostBackOnFilter="true" UniqueName="Name" Visible="true" SortExpression="Name"
                                                                HeaderText="Employee Name">
                                                                <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                                </radG:RadGrid>
                                                    
                                                    </td>
                                            </tr>
                                        </table>

                                    </div>


                                    <%--Second Row--%>

                                    <div class="col-md-5">

                                        <table width="100%">
                                            <tr runat="server" id="radWf2" visible="false">
                                                <td align="left" valign="top">
                                                    
                                                    <radG:RadGrid ID="RadGrid3"  runat="server" DataSourceID="SqlDataSource5" GridLines="None"
                                                    AllowFilteringByColumn="true" Skin="Outlook" Width="98%" AllowMultiRowSelection="true"
                                                    OnPageIndexChanged="RadGrid3_PageIndexChanged" PagerStyle-AlwaysVisible="true"
                                                    PagerStyle-Mode="NumericPages">
                                                    <MasterTableView AllowAutomaticUpdates="True" DataSourceID="SqlDataSource5" AutoGenerateColumns="False"
                                                        DataKeyNames="Emp_Code" AllowPaging="true" PageSize="50">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_Code" DataType="System.Int32"
                                                                UniqueName="Emp_Code" Visible="true" SortExpression="Emp_Code" HeaderText="Emp_Code">
                                                            </radG:GridBoundColumn>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <%--<ItemStyle Width="10%" />--%>
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="Emp_Name" DataType="System.String" UniqueName="Emp_Name"
                                                                Visible="true" SortExpression="Emp_Name" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                HeaderText="Employee Name">
                                                                <%--<ItemStyle HorizontalAlign="left" Width="90%" />--%>
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                                </radG:RadGrid>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="col-md-2">
                                        <table>
                                        <tr runat="server" id="rw4">
                                            <td rowspan="2" valign="top">
                                                <asp:Button ID="btnWF2ASsign" runat="server" Text="Assign"
                                                    Height="28px" Width="75px" />
                                                <br />
                                                <asp:Button ID="btnWF2UnAssign" runat="server" Text="Un-Assign"
                                                    Height="28px" Width="75px" />
                                            </td>
                                        </tr>
                                            </table>
                                    </div>

                                    <div class="col-md-5">
                                        <table width="100%">
                                            <tr runat="server" id="radWf2second" visible="false">
                                                <td valign="top">
                                                    
                                                    <radG:RadGrid ID="RadGrid4"  runat="server" OnDeleteCommand="RadGrid4_DeleteCommand"
                                                    AllowFilteringByColumn="true" AllowSorting="true" AllowMultiRowSelection="true"
                                                    OnItemDataBound="RadGrid4_ItemDataBound" DataSourceID="SqlDataSource6" GridLines="None"
                                                    Skin="Outlook" OnItemInserted="RadGrid4_ItemInserted" OnItemUpdated="RadGrid4_ItemUpdated"
                                                    PagerStyle-AlwaysVisible="true" PagerStyle-Mode="NumericPages" Width="98%">
                                                    <MasterTableView CommandItemDisplay="None" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource6"
                                                        AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                                        AllowPaging="true" PageSize="50" DataKeyNames="Child_ID">
                                                        <FilterItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle ForeColor="Navy" />
                                                        <ItemStyle BackColor="White" Height="20px" />
                                                        <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                        <Columns>
                                                            <radG:GridBoundColumn Display="false" ReadOnly="True" DataField="Emp_ID" DataType="System.Int32"
                                                                UniqueName="Emp_ID" Visible="true" SortExpression="Emp_ID" HeaderText="Emp_ID">
                                                                <%--<ItemStyle Width="0px" />--%>
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="false" DataField="Sub_Project_Name" DataType="System.String"
                                                                UniqueName="Sub_Project_Name" Visible="true" SortExpression="Sub_Project_Name"
                                                                HeaderText="Sub Project Name">
                                                                <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                            <radG:GridClientSelectColumn UniqueName="Assigned">
                                                                <%--<ItemStyle Width="10%" />--%>
                                                            </radG:GridClientSelectColumn>
                                                            <radG:GridBoundColumn DataField="EmpName" DataType="System.String" UniqueName="EmpName"
                                                                Visible="true" SortExpression="EmpName" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                HeaderText="Assigned Employee Name">
                                                                <%--<ItemStyle Width="80%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                            <radG:GridBoundColumn Display="false" DataField="Remarks" DataType="System.String"
                                                                UniqueName="Remarks" Visible="true" HeaderText="Remarks" AllowFiltering="false">
                                                                <%--<ItemStyle Width="30%" HorizontalAlign="left" />--%>
                                                            </radG:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        <ClientEvents OnRowDblClick="RowDblClick" />
                                    </ClientSettings>
                                                </radG:RadGrid>
                                                    
                                                    </td>
                                            </tr>
                                        </table>
                                    </div>





                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="Select WL.ID, WorkFlowName +' - L'+ + Cast(WL.RowID as varchar(5)) WorkFlowName  From( Select ID,WorkFlowName From EmployeeWorkFlow  Where ID IN (Select Distinct WorkFlowID From EmployeeWorkFlowLevel Where FlowType=1) And Company_ID=@company_id ) WF Inner Join EmployeeWorkFlowLevel WL On WF.ID=WL.WorkFlowID Order By WF.WorkFlowName, WL.RowID">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT  EMP_CODE,EMP_NAME + ' ' + EMP_LNAME AS Name FROM EMPLOYEE WHERE TERMINATION_DATE IS NULL  AND ([pay_supervisor] IS NULL Or [pay_supervisor]=0) AND COMPANY_ID = @company_id ORDER BY EMP_NAME">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT  EMP_CODE,EMP_NAME + ' ' + EMP_LNAME AS Name FROM EMPLOYEE WHERE TERMINATION_DATE IS NULL  AND [pay_supervisor] =@PAY_ID AND COMPANY_ID =@company_id ORDER BY EMP_NAME">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="drpSubProjectID" Name="PAY_ID" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>



                                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" SelectCommand="select ID,WorkFlowName from EmployeeWorkFlow  Where Company_Id= @company_id">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" SelectCommand="Select Emp_Code, (Emp_Name+' '+Emp_LName) Emp_Name,'Assigned' = Case  When EC.PayrollGroupID is null Then CAST(0 AS bit) Else CAST(1 AS bit) End From Employee EA Left Outer Join (Select EA.Emp_ID,EA.PayrollGroupID From EmployeeAssignedToWorkFlow2Emp EA Inner Join EmployeeWorkFlow PG On  EA.PayrollGroupID = PG.ID  Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code  Where EM.Company_ID=@company_id And PG.ID=@PayrollGroupIDWF  And EM.[StatusID]=1) EC On EA.Emp_Code = EC.Emp_ID  Where EA.Company_ID=@company_id And EC.PayrollGroupID is null And @PayrollGroupIDWF > -1 And Emp_Code Not In (Select Emp_ID From EmployeeAssignedToWorkFlow2Emp)  And EA.StatusID=1 And EA.TERMINATION_DATE IS NULL Order By EA.Emp_Name">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="drpWorkFlow" Name="PayrollGroupIDWF" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource6" runat="server" SelectCommand="Select EA.ID Child_ID, EA.PayrollGroupID, PG.ID, PG.WorkFlowName, EA.Emp_ID, (EM.Emp_Name+' '+EM.emp_LName) EmpName From EmployeeAssignedToWorkFlow2Emp EA Inner Join EmployeeWorkFlow PG On  EA.PayrollGroupID = PG.ID Inner Join Employee EM On EA.Emp_ID = EM.Emp_Code  Where EM.Company_ID= @company_id  And PG.ID= @PayrollGroupIDWF  And EM.StatusID=1 AND EM.TERMINATION_DATE IS NULL Order By EM.Emp_Name">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="drpWorkFlow" Name="PayrollGroupIDWF" PropertyName="SelectedValue"
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>

                                </center>
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

<uc_js:bundle_js ID="bundle_js" runat="server" />

    <script type="text/javascript">
        $("input[type='button']").removeAttr("style");
    </script>

</body>
</html>
