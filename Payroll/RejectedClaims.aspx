<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RejectedClaims.aspx.cs"
    Inherits="SMEPayroll.Payroll.RejectedClaims" %>

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
                        <li>View Rejected Claims</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Rejected Claim</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">View Rejected Claims</h3>--%>
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
                                        <label>Employee</label><%--DataSourceID="SqlDataSource1" --%>
                                        <asp:DropDownList ID="DropDownList1" class="textfields form-control input-sm"  runat="server"
                                            DataTextField="emp_name" DataValueField="emp_code">
                                            <asp:ListItem Selected="True" Text="" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
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
                                    <%--   <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
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

                                    <div class="form-group">
                                        <label>Year</label>
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                                <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>

                                
                            </div>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>

                            <%--Commented By Jaspreet--%>

                            <%--  <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>--%>



                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" DataSourceID="SqlDataSource2" GridLines="None"
                                Skin="Outlook" Width="99%" EnableHeaderContextMenu="true" OnItemDataBound="RadGrid1_ItemDataBound">
                                <MasterTableView DataSourceID="SqlDataSource2" AllowAutomaticDeletes="True" AutoGenerateColumns="False"
                                    DataKeyNames="trx_id">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                                            </ItemTemplate>
                                           <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn DataField="emp_name" UniqueName="EmpName" SortExpression="EmpName"
                                            HeaderText="Employee Name">
                                            <%--<ItemStyle Width="20%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="desc" HeaderText="Claim Type" ReadOnly="True" SortExpression="Type"
                                            UniqueName="Type">
                                            <%--<ItemStyle Width="20%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ProcessDate" HeaderText="Rejected Date" ReadOnly="True" SortExpression="ProcessDate"
                                            UniqueName="ProcessDate">
                                            <%--<ItemStyle Width="20%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" ReadOnly="True"
                                            SortExpression="Department" UniqueName="Department">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_amount" UniqueName="Amount" SortExpression="Amount"
                                            HeaderText="Amount">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="created_on" DataType="System.DateTime" HeaderText="Application Date"
                                            SortExpression="created_on" UniqueName="created_on">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="status" Visible="false" UniqueName="Status" SortExpression="Status"
                                            HeaderText="Status">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="claimstatus" UniqueName="ClaimStatus" SortExpression="ClaimStatus"
                                            HeaderText="ClaimStatus">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="remarks" UniqueName="remarks" SortExpression="remarks"
                                            HeaderText="Remarks">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_code" DataType="System.Int32" UniqueName="emp_code"
                                            SortExpression="emp_code" Visible="False" HeaderText="emp_code">
                                            <%--<ItemStyle Width="1%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn ReadOnly="True" DataField="trx_id" DataType="System.Int32"
                                            UniqueName="trx_id" SortExpression="trx_id" Visible="False" HeaderText="trx_id">
                                            <%--<ItemStyle Width="1%" />--%>
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
                                        <radG:GridBoundColumn UniqueName="Currency" HeaderText="Currency" DataField="Currency" Display="true" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderText="Attached Document">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# Eval("recpath")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="142px"  />
                                            <ItemStyle Width="142px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                    </Columns>
                                    <ExpandCollapseColumn Visible="False">
                                        <%--<HeaderStyle Width="19px"></HeaderStyle>--%>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <%--<HeaderStyle Width="20px"></HeaderStyle>--%>
                                    </RowIndicatorColumn>
                                </MasterTableView>
                            </radG:RadGrid>

                            <%--</td>
                                </tr>
                            </table>--%>


                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT recpath,  
        ic_pp_number,(select Designation from Designation where id=b.desig_id)as Designation, b.emp_type,b.time_card_no as TimeCardId,(select Nationality from nationality where id=b.nationality_id)as Nationality,(select trade from trade where id=b.Trade_id) as Trade,
        e.[trx_id],  a.[id],a.[desc] ,e.[trx_amount],
convert(varchar(20),created_on,103) created_on,convert(varchar(20),trx_period,103) ProcessDate, e.[emp_code],
b.emp_name+' '+b.emp_lname 'emp_name',(select DeptName from department where id=b.dept_id) Department,e.remarks,isnull(e.recpath,'') recpath,status,claimstatus,Convert(Varchar(50),ProcessDate,103) as ProcessDate,(select top 1  c.Currency from   Currency c where c.Id =e.CurrencyID)Currency FROM [emp_additions] e, additions_types a, employee b
WHERE e.trx_type = a.id and e.emp_code = b.emp_code and claimstatus='Rejected' and e.emp_code=@emp_code and year(trx_period)=@year  and upper(a.optionselection) like '%CLAIM%'"
                                DeleteCommand="Delete from emp_additions where [trx_id]=@trx_id">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="DropDownList1" Name="emp_code" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                                <DeleteParameters>
                                    <asp:Parameter Name="trx_id" Type="Int32" />
                                </DeleteParameters>
                            </asp:SqlDataSource>
                            <br>
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


