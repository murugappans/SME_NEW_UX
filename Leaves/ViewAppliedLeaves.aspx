<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewAppliedLeaves.aspx.cs"
    Inherits="SMEPayroll.Leaves.ViewAppliedLeaves" %>

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
    
</head>

<body class="page-header-fixed page-sidebar-closed-hide-logo page-content-white  page-sidebar-closed" onload="ShowMsg();">




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
                        <li>Leave Status</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Leave Status</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Leave Status</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label>Employee</label>
                                        <asp:DropDownList ID="cmbEmployee" class="textfields form-control input-sm" AutoPostBack="false" runat="server"
                                            DataSourceID="SqlDataSource1" DataTextField="emp_name" DataValueField="emp_code">
                                            <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--        <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                </asp:DropDownList>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server" AppendDataBoundItems="true">
                                        <asp:ListItem Text="-select-" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>



                            <%--  Commented By Jaspreet  --%>

                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>--%>






                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" OnItemDataBound="RadGrid1_ItemDataBound" AllowSorting="True"
                                DataSourceID="SqlDataSource2" GridLines="None" Skin="Outlook" Width="100%" OnItemCommand="RadGrid1_ItemCommand"  OnSortCommand="grid_SortCommand"  >
                                <MasterTableView DataSourceID="SqlDataSource2" AllowAutomaticDeletes="True" AutoGenerateColumns="False"
                                    DataKeyNames="trx_id, emp_id, status, leave_type, type, start_date, end_date, paid_leaves, unpaid_leaves,Remarks" TableLayout="Auto">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />

                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <%--<radG:GridBoundColumn DataField="Application Date" HeaderText="Application Date" SortExpression="Application Date"
                                DataType="System.DateTime" UniqueName="Application Date" DataFormatString="{0:MM/dd/yyyy}" ReadOnly="true">
                                </radG:GridBoundColumn>--%>

                                        <radG:GridBoundColumn DataField="Application Date" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}" UniqueName="[Application Date]"
                                            SortExpression="[Application Date]" HeaderText="Application Date">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leave_type" Visible="False" DataType="System.Int32"
                                            UniqueName="leave_type" SortExpression="leave_type" HeaderText="Leave Type">
                                            <%--<ItemStyle Width="1%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="type" UniqueName="type" SortExpression="type" HeaderText="Leave Type">
                                            <%--<ItemStyle Width="15%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="start_date" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}" UniqueName="start_date"
                                            SortExpression="start_date" HeaderText="From">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="end_date" DataType="System.DateTime" DataFormatString="{0:dd/MM/yyyy}" UniqueName="end_date"
                                            SortExpression="end_date" HeaderText="To">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="status" DataType="System.Int32" UniqueName="status"
                                            SortExpression="status" HeaderText="Status">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="payrollstatus" Visible="false" UniqueName="payrollstatus"
                                            SortExpression="payrollstatus" HeaderText="payrollStatus">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Paid_leaves" UniqueName="paid_leaves" SortExpression="paid_leaves"
                                            HeaderText="Paid Leave">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="unpaid_leaves" UniqueName="unpaid_leaves" SortExpression="unpaid_leaves"
                                            HeaderText="Unpaid Leave">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="sumLeaves" UniqueName="sumLeaves" SortExpression="sumLeaves"
                                            HeaderText="Total Leave">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Session" DataType="System.Int32" UniqueName="Session"
                                            SortExpression="Session" HeaderText="Session">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_id" DataType="System.Int32" UniqueName="emp_id"
                                            SortExpression="emp_id" Visible="False" HeaderText="emp_id">
                                            <%--<ItemStyle Width="1%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Remarks" UniqueName="Remarks" SortExpression="Remarks"
                                            HeaderText="Remarks">
                                            <HeaderStyle Width="250px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Leavre_Model" UniqueName="Leave_Model" SortExpression="Leave_Model" Visible="false"
                                            HeaderText="Leave Model">
                                            <%--<ItemStyle Width="15%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="trx_id" DataType="System.Int32"
                                            UniqueName="trx_id" SortExpression="trx_id" Visible="False" HeaderText="trx_id">
                                            <%--<ItemStyle Width="1%" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridTemplateColumn HeaderText="Attached Document">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# ((Convert.ToString(Eval("Path")))!="") ? "Doc":" " %>' NavigateUrl='<%# Eval("Path")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </radG:GridTemplateColumn>


                                        <radG:GridButtonColumn HeaderText="Delete" ButtonType="ImageButton"
                                            CommandName="Delete" Text="Delete" UniqueName="Deletecolumn">
                                            <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                        </radG:GridButtonColumn>
                                    </Columns>

                                </MasterTableView>


                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                </ClientSettings>
                            </radG:RadGrid>

                            <%--</td>
            </tr>
            <tr>
                <td align="center" class="bodytxt">
                    </td>
            </tr>
        </table>--%>


                            <asp:Label ForeColor="red" Font-Bold="true" ID="lblMsg" runat="Server" Text="" Visible="false"></asp:Label>

                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT [Path],[trx_id], [emp_id], [leave_type] ,b.type,[start_date] as 'start_date', [end_date] as 'end_date',timesession  as Session,paid_leaves,unpaid_leaves,(paid_leaves + unpaid_leaves)'sumLeaves', [status],payrollstatus, [Application Date] as 'Application Date',a.Remarks,Leave_Model = Case When  a.leave_model =1 Then 'Fixed Yearly-Normal' When  a.leave_model =7 Then 'Fixed Yearly-Prorated' When  a.leave_model =2 Then 'Fixed Yearly-Prorated(Floor)' When  a.leave_model =5 Then 'Fixed Yearly-Prorated(Ceiling)' When  a.leave_model =3 Then 'Year of Service-Normal' When  a.leave_model =8 Then 'Year of Service-Prorated' When  a.leave_model =4 Then 'Year of Service-Prorated(Floor)' When  a.leave_model =6 Then 'Year of Service-Prorated(Ceiling)' END FROM [emp_leaves] a Inner Join Leave_Types b On a.leave_type=b.id Inner Join Employee e On a.Emp_ID = e.emp_code Where (a.emp_id = 1) And Year(a.start_date)= 2019 order by [Application Date],[start_date], [end_date]  asc">
                                <%--<SelectParameters>
                                    <asp:ControlParameter ControlID="cmbEmployee" Name="emp_id" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>--%>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" DeleteCommand="DELETE FROM [employee] WHERE [emp_code] = @emp_code"
                                InsertCommand="INSERT INTO [employee] ([emp_code], [emp_name]) VALUES (@emp_code, @emp_name)"
                                SelectCommand="SELECT [emp_code],isNull([emp_name],'')+' '+isnull([emp_lname],'')[emp_name]  FROM [employee] where termination_date is null and company_id=@company_id order by emp_name"
                                UpdateCommand="UPDATE [employee] SET [emp_name] = @emp_name WHERE [emp_code] = @emp_code">
                                <DeleteParameters>
                                    <asp:Parameter Name="emp_code" Type="String" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="emp_name" Type="String" />
                                    <asp:Parameter Name="emp_code" Type="String" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="emp_code" Type="String" />
                                    <asp:Parameter Name="emp_name" Type="String" />
                                </InsertParameters>
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
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
           $(".clsCnfrmButton").click(function () {
               var _elem = $(this).find('input[type=image]');
               var _id = _elem.attr('id');
               GetConfirmation("Are you sure you want to delete this Leave?", _id, "Confirm Delete", "Delete");
           });
           $(document).on("click", "#imgbtnfetch", function () {
               var _message = "";
               if ($.trim($("#cmbEmployee option:selected").text()) === "-select-")
                   _message += "Please Select Employee <br>";
               if ($.trim($("#cmbYear option:selected").text()) === "-select-")
                   _message += "Please Select Year  <br>";

               if (_message != "") {
                   WarningNotification(_message);
                   return false;
               }
           });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }

           //$('#cmbEmployee,#cmbYear').change(function () {
           //     $('#imgbtnfetch').click();
           //    //$('#RadGrid1_ctl00 tbody').html("");

           //});
    </script>
</body>
</html>
