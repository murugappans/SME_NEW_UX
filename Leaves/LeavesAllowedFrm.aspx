<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeavesAllowedFrm.aspx.cs"
    Inherits="SMEPayroll.Leaves.LeavesAllowedFrm" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

<uc_css:bundle_css ID="bundle_css" runat="server" />
    
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
                        <li>Yearly Leave Allowed For Employee Groups</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Allowed Leave</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Yearly Leave Allowed For Employee Groups</h3>--%>
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

                            </radG:RadCodeBlock>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            
                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-8">
                                    <div class="form-group">
                                        <label>Employee Group</label>
                                        <asp:DropDownList ID="cmbEmpgroup" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>

                                        <%--&nbsp;&nbsp; <tt class="bodytxt">Leave Year:</tt>--%>

                                        <%--       <asp:DropDownList ID="cmbLeaveYear" runat="server" CssClass="textfields">
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
                                        <asp:DropDownList ID="cmbLeaveYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            runat="server">
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>

                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch"  CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right">
                                    <input type="button" id="btTest" class="btn red btn-sm" onserverclick="CopyToNextYear" value="Copy Leave to Next Year" runat="server" />
                                </div>
                            </div>


                            <%--  Commented By Jaspreet  --%>

                            <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                
            </tr>
            <tr>
                <td>--%>
                            <div class="text-center">
                                <asp:Label ID="lblerror" ForeColor="red" class="bodytxt" runat="server"></asp:Label>
                            </div>



                            <radG:RadGrid ID="gvCopyToNextYear"  Skin="Outlook" runat="server" OnItemCommand="gvCopyToNextYear_ItemCommand" OnGroupsChanging="gvCopyToNextYear_GroupsChanging"
                                GridLines="None" Width="100%" OnItemDataBound="RadGrid1_ItemDataBound" GroupingEnabled="true" ClientSettings-AllowDragToGroup="true" ShowGroupPanel="true">
                                <MasterTableView CommandItemDisplay="bottom" AutoGenerateColumns="False" DataKeyNames="id,typeid,leaves_allowed,group_id">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />


                                    <CommandItemTemplate>
                                        <%--to get the button in the grid header--%>
                                        <div style="text-align: center">
                                            <%-- <asp:Button ID="btnCopy" runat="server" Text="Copy LastYear Leaves" CommandName="UpdateLastYear" Visible="false"  />   --%>
                                            <asp:Button ID="btnSaveNextYear" CssClass="btn btn-sm red" runat="server" Text="Save" CommandName="SaveToNextYear" />
                                            <%-- <asp:Button ID="btnSubmitEmp" runat="server" Text="Copy Leaves " CommandName="UpdateDataEmp"  />--%>
                                        </div>
                                    </CommandItemTemplate>
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle  HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Visible="false" DataField="id" DataType="System.Int32" HeaderText="id"
                                            ReadOnly="True" SortExpression="id" UniqueName="id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Visible="false" DataField="group_id" DataType="System.Int32"
                                            HeaderText="group_id" ReadOnly="True" SortExpression="group_id" UniqueName="group_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="group_name" DataType="System.Int32"
                                            HeaderText="Group Name" ReadOnly="True" UniqueName="group_name">
                                            <%--<ItemStyle Width="18%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Visible="false" DataField="typeid" DataType="System.Int32"
                                            HeaderText="typeid" ReadOnly="True" SortExpression="typeid" UniqueName="typeid">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Type" HeaderText="Leave Types" SortExpression="Type"
                                            UniqueName="Type">
                                            <%--<ItemStyle Width="30%" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridTemplateColumn DataField="leaves_allowed" UniqueName="leaves_allowed" HeaderText="Leave Allowed for Selected Year">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtleaves" CssClass="form-control input-sm" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.leaves_allowed")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Width="215px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn DataField="NextYearLeavesAllowed" UniqueName="NextYearLeavesAllowed" HeaderText="Leave Allowed for Next Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaves"  runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NextYearLeavesAllowed")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="215px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Visible="false" DataField="companyid" DataType="System.Int32"
                                            HeaderText="companyid" SortExpression="companyid" UniqueName="companyid">
                                        </radG:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <%--</td>
            </tr>
        </table>--%>

                            <div class="row">
                                <div class="col-md-6">
                                    <radG:RadGrid ID="RadGrid1" Skin="Outlook" runat="server" OnItemCommand="RadGrid1_ItemCommand"
                                        GridLines="None" Width="100%" OnItemDataBound="RadGrid1_ItemDataBound">
                                        <MasterTableView CommandItemDisplay="bottom" AutoGenerateColumns="False" DataKeyNames="id,typeid,leaves_allowed,group_id">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <CommandItemTemplate>
                                                <%--to get the button in the grid header--%>
                                                <div style="text-align: center">
                                                    <asp:Button ID="btnCopy" runat="server" Text="Copy LastYear Leave" CommandName="UpdateLastYear" Visible="false" />
                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn red" CommandName="UpdateAll" />
                                                    <asp:Button ID="btnSubmitEmp" runat="server" Text="Copy Leave" CssClass="btn default" CommandName="UpdateDataEmp" />
                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>

                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                                    <ItemTemplate>
                                                        <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>

                                                <radG:GridBoundColumn Visible="false" DataField="id" DataType="System.Int32" HeaderText="id"
                                                    ReadOnly="True" SortExpression="id" UniqueName="id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Visible="false" DataField="group_id" DataType="System.Int32"
                                                    HeaderText="group_id" ReadOnly="True" SortExpression="group_id" UniqueName="group_id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Visible="false" DataField="typeid" DataType="System.Int32"
                                                    HeaderText="typeid" ReadOnly="True" SortExpression="typeid" UniqueName="typeid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Type" HeaderText="Leave Types" SortExpression="Type"
                                                    UniqueName="Type">
                                                    <%--<ItemStyle Width="55%" />--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn DataField="leaves_allowed" UniqueName="leaves_allowed" HeaderText="No.of Leave Allowed">
                                                    <ItemTemplate>
                                                        <asp:TextBox MaxLength="6" ID="txtleaves" CssClass="form-control input-sm number-dot custom-maxlength leapyearckeck text-right" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.leaves_allowed")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="255px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridBoundColumn Visible="false" DataField="companyid" DataType="System.Int32"
                                                    HeaderText="companyid" SortExpression="companyid" UniqueName="companyid">
                                                </radG:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-6">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_allowed_leaves"
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter Name="groupid" Type="Int32" ControlID="cmbEmpgroup" />
                                            <asp:SessionParameter Name="compid" SessionField="compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="cmbLeaveYear" PropertyName="SelectedValue" Name="leave_year" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <radG:RadGrid ID="RadGrid2" Skin="Outlook" Width="99%" OnItemCommand="RadGrid2_ItemCommand"
                                        runat="server" GridLines="None" DataSourceID="SqlDataSource2">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="comp_id,group_id,year_of_service,leaves_allowed"
                                            CommandItemDisplay="Bottom" DataSourceID="SqlDataSource2">
                                            <CommandItemTemplate>
                                                <%--to get the button in the grid header--%>
                                                <div style="text-align: center">
                                                    <asp:Button ID="btnsubmit2" runat="server" Text="Submit" CssClass="btn red" CommandName="UpdateAll" />

                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <radG:GridBoundColumn Visible="false" DataField="comp_id" DataType="System.Int32"
                                                    HeaderText="comp_id" ReadOnly="True" SortExpression="comp_id" UniqueName="comp_id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn Visible="false" DataField="group_id" DataType="System.Int32"
                                                    HeaderText="group_id" ReadOnly="True" SortExpression="group_id" UniqueName="group_id">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="year_of_service" DataType="System.Int32" HeaderText="Year Of Service"
                                                    ReadOnly="True" SortExpression="year_of_service" UniqueName="year_of_service">
                                                    <%--<HeaderStyle HorizontalAlign="center" />--%>
                                                    <%--<ItemStyle HorizontalAlign="center" Width="55%"/>--%>
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn DataField="leaves_allowed" UniqueName="leaves_allowed" HeaderText="Annual Leave Allowed">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtleavesallowed" MaxLength="6" CssClass="form-control input-sm number-dot custom-maxlength leapyearckeck text-right" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.leaves_allowed")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="155px" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                            <ExpandCollapseColumn Visible="False">
                                                <%--<HeaderStyle Width="19px" />--%>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <%--<HeaderStyle Width="20px" />--%>
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                    </radG:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" DeleteCommand="DELETE FROM [prorated_leaves] WHERE [comp_id] = @comp_id AND [group_id] = @group_id AND [year_of_service] = @year_of_service AND [leaves_allowed] = @leaves_allowed"
                                        InsertCommand="INSERT INTO [prorated_leaves] ([comp_id], [group_id], [year_of_service], [leaves_allowed]) VALUES (@comp_id, @group_id, @year_of_service, @leaves_allowed)"
                                        SelectCommand="SELECT [comp_id], [group_id], [year_of_service], [leaves_allowed] FROM [prorated_leaves] WHERE ([comp_id] = @comp_id) and ([group_id] =@groupid)">
                                        <DeleteParameters>
                                            <asp:Parameter Name="comp_id" Type="Int32" />
                                            <asp:Parameter Name="group_id" Type="Int32" />
                                            <asp:Parameter Name="year_of_service" Type="Int32" />
                                            <asp:Parameter Name="leaves_allowed" Type="Double" />
                                        </DeleteParameters>
                                        <SelectParameters>
                                            <asp:ControlParameter Name="groupid" Type="Int32" ControlID="cmbEmpgroup" />
                                            <asp:SessionParameter Name="comp_id" SessionField="compid" Type="Int32" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="comp_id" Type="Int32" />
                                            <asp:Parameter Name="group_id" Type="Int32" />
                                            <asp:Parameter Name="year_of_service" Type="Int32" />
                                            <asp:Parameter Name="leaves_allowed" Type="Double" />
                                        </InsertParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbEmpgroup"
            Display="None" ErrorMessage="Employee Group Name is not selected!" InitialValue=""></asp:RequiredFieldValidator>--%>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" />
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
   <script type ="text/jscript">
       window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }

        $(document).ready(function () {
            $("div#rdTrdate_wrapper").removeAttr("style");
            $('#imgbtnfetch').click(function () {
                return validateform();
            });

            $(document).on("keypress", ".leapyearckeck", function (e) { 
            //$(".leapyearckeck").keypress(function (e) {
                var _cntrl = $(this);
                var newString = $(_cntrl).val() + String.fromCharCode(e.keyCode);
                var year = $("#cmbLeaveYear").val();
                if(((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0))
                {
                    if (newString > 366)
                    {
                        e.preventDefault();
                        WarningNotification($("#cmbLeaveYear").val() + " is a Leap year. So Leave days more than 366 are not allowed");
                       // $(_cntrl).val("");
                        return false;
                    }
                }
                else
                {
                    if (newString > 365) {
                        e.preventDefault();
                        WarningNotification("Leave days more than 365 are not allowed");
                       // $(_cntrl).val("");
                        return false;
                    }
                }


            });
            $(document).on("focusout", ".leapyearckeck", function (e) {
                //$(".leapyearckeck").keypress(function (e) {
                var _cntrl = $(this);
                var newString = $(_cntrl).val();
                var year = $("#cmbLeaveYear").val();
                if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
                    if (newString > 366) {
                       // e.preventDefault();
                        WarningNotification($("#cmbLeaveYear").val() + " is a Leap year. So Leave days more than 366 are not allowed");
                        $(_cntrl).val("");
                        return false;
                    }
                }
                else {
                    if (newString > 365) {
                        e.preventDefault();
                        WarningNotification("Leave days more than 365 are not allowed");
                        $(_cntrl).val("");
                        return false;
                    }
                }


            });
            $('#Button2').click(function () {
                if (validateform() == true) {
                    if ($("#RadGrid1_ctl00").length == 0 || $("#RadGrid1_ctl00 tbody tr td:contains(No records to display.)").length > 0) {
                        WarningNotification("No record found.");
                        return false;
                    }
                }
                else
                    return false;

                if($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length<1)
                {
                    WarningNotification("Atleast one record must be selected from grid.");
                    return false;
                }

            });
        })
        function validateform() {
            var _message = "";
            if ($.trim($("#cmbEmpgroup").val()) === "")
                _message = "Employee Group Name is not selected.";
            else if ($.trim($("#cmbYear").val()) === "0")
                _message = "Please select year.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
   </script> 
</body>
</html>
