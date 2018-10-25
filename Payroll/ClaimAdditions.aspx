<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimAdditions.aspx.cs"
    Inherits="SMEPayroll.Payroll.ClaimAddtitions" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
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
                        <li>Apply Claims</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Apply Claim</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Apply Claims</h3>--%>
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
                                        <asp:DropDownList ID="drpemp" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Month</label>
                                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="textfields form-control input-sm">
                                            <asp:ListItem Value="-1">-select-</asp:ListItem>
                                            <asp:ListItem Value="13">All</asp:ListItem>
                                            <asp:ListItem Value="01">January</asp:ListItem>
                                            <asp:ListItem Value="02">February</asp:ListItem>
                                            <asp:ListItem Value="03">March</asp:ListItem>
                                            <asp:ListItem Value="04">April</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">June</asp:ListItem>
                                            <asp:ListItem Value="07">July</asp:ListItem>
                                            <asp:ListItem Value="08">August</asp:ListItem>
                                            <asp:ListItem Value="09">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <%--    <asp:DropDownList ID="cmbYear" runat="server" Style="width: 65px" CssClass="textfields">
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
                                            runat="server" AutoPostBack="false">
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

                            <%--Commented By Jaspreet--%>

                            <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    
                                </tr>
                                <tr>
                                    <td>--%>

                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>

                            <radG:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="true" OnItemDataBound="RadGrid1_ItemDataBound"
                                OnUpdateCommand="RadGrid1_UpdateCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
                                OnInsertCommand="RadGrid1_InsertCommand" DataSourceID="SqlDataSource2" GridLines="None"
                                Skin="Outlook" Width="100%" EnableHeaderContextMenu="true" AllowMultiRowSelection="true">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,emp_code,emp_name"
                                    DataSourceID="SqlDataSource2" CommandItemDisplay="Bottom" >
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" />
                                            <HeaderStyle Width="35px" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle Width="30px" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name" FilterControlAltText="alphabetsonly">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                            ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="desc" HeaderText="Claim Type" ReadOnly="True" SortExpression="Type"
                                            UniqueName="Type" FilterControlAltText="cleanstring">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" ReadOnly="True"
                                            SortExpression="Department" UniqueName="Department" FilterControlAltText="cleanstring">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="Type"
                                            Visible="False" UniqueName="Type1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="approver" HeaderText="approver" ReadOnly="True"
                                            SortExpression="approver" Visible="False" UniqueName="approver">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                            SortExpression="trx_amount" UniqueName="trx_amount" FilterControlAltText="numericonly">
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_period" DataType="System.DateTime" HeaderText="Period"
                                            SortExpression="trx_period" UniqueName="trx_period" FilterControlAltText="numericonly">
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle Width="100px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderText="Attached Document">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="h1"  runat="server" Target="_blank" Text='<%# Eval("recpath")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="142px"  />
                                            <ItemStyle Width="142px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="remarks"  DataType="System.String"
                                            HeaderText="Remarks" SortExpression="remarks" UniqueName="remarks">
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" />
                                            <HeaderStyle Width="30px" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn  ButtonType="ImageButton"
                                            ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" CssClass ="clsCnfrmButton" />
                                            <HeaderStyle Width="30px" />
                                        </radG:GridButtonColumn>

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
                                        <radG:GridBoundColumn UniqueName="CurrencyID" HeaderText="CurrencyID" DataField="CurrencyID" Display="false" AllowFiltering="false">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ConversionOpt" AllowFiltering="false"
                                            UniqueName="ConversionOpt" HeaderText="ConversionOpt" Display="false">
                                        </radG:GridBoundColumn>

                                    </Columns>
                                    <EditFormSettings UserControlName="claimaddition.ascx" EditFormType="WebUserControl">
                                    </EditFormSettings>
                                    <CommandItemSettings AddNewRecordText="Add New Claims" />
                                    <ExpandCollapseColumn Visible="False">
                                        <%--<HeaderStyle Width="19px" />--%>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <%--<HeaderStyle Width="20px" />--%>
                                    </RowIndicatorColumn>
                                </MasterTableView>

                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                        AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                    <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True"  SaveScrollPosition="True" />
                                </ClientSettings>

                            </radG:RadGrid>


                            <%-- </td>
                                </tr>
                                <tr>
                                    <td align="center">--%>


                            <p class="text-center">
                                <asp:Button ID="Button2" runat="server" Text="Submit Claim" class="textfields btn red" OnClick="Button2_Click" />
                            </p>


                            <%--</td>
                                </tr>
                            </table>--%>



                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppayclaim_add"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="drpemp" Name="empcode" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="DropDownList1" Name="empmonth" PropertyName="SelectedValue"
                                        Type="Int32" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="empyear" PropertyName="SelectedValue"
                                        Type="String" />
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

        $(".rgEditForm").ready(function () {
            $(".ruBrowse, .ruClear").addClass("btn default");
        });
        $(document).on('click', ".ruClear", function () {
            $(".ruBrowse, .ruClear").addClass("btn default");
        });
        

        $("input[type='button']").removeAttr("style");
        //$(".RadGrid_Outlook .rgAdd + a, .RadGrid_Outlook .rgRefresh + a").addClass("btn default btn-xs");

        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        $(document).ready(function () {
            $("div#rdTrdate_wrapper").removeAttr("style");
            $('#imgbtnfetch').click(function () {
                return validatefilters();
            });
            $(".clsCnfrmButton").click(function () {
                var _elem = $(this).find('input[type=image]');
                var _id = _elem.attr('id');
                GetConfirmation("Are you sure you want to delete this Claim?", _id, "Confirm Delete", "Delete");
            });

            $('#Button2').click(function () {
                if ($("#RadGrid1_ctl00").length == 0 || $("#RadGrid1_ctl00 tbody tr td:contains(No records to display.)").length > 0) {
                    WarningNotification("No record found.");
                    return false;
                }



                if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1) {
                    WarningNotification("Atleast on record must be selected from grid.");
                    return false;
                }

            });

            $('.btn-add-update').click(function () {
                return validateform();
            });
            $(document).on('change', '.input-transaction-from,.input-transaction-to', function () {
                //var _fromDate = $('.input-transaction-from').val();
                //var _toDate = $('.input-transaction-to').val();
                //_fromDate = Date.parse(_fromDate);
                //_toDate = Date.parse(_toDate);
                //if (_toDate < _fromDate) {
                //    WarningNotification("To date should be equal or greater than from date.");
                //    return false;
                //}
            });
        })
        function validatefilters() {
            var _message = "";
            if ($.trim($("#drpemp option:selected").text()) === "-select-")
                _message += "Select Employee. <br>";
            if ($.trim($("#DropDownList1 option:selected").text()) === "-select-")
                _message += "Select Month. <br>";
            if ($.trim($("#cmbYear option:selected").text()) === "-select-")
                _message += "Select Year <br>";

            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

       

    </script>

</body>
</html>
