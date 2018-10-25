<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveTransfer.aspx.cs"
    Inherits="SMEPayroll.Leaves.LeaveTransfer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
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
                        <li>Leave Transfer Form</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Management/LeaveTranferAndEncash.aspx">Leave Transfer And Encashment</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Leave Transfer</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Leave Transfer Form</h3>--%>
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
                                <div class="form-inline col-sm-12">
                                    <div class="form-group">
                                        <label><asp:Label ID="lbl1" Text="Employee Group" runat="server"></asp:Label></label>
                                        <asp:DropDownList ID="cmbEmpgroup" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <label><asp:Label ID="Label1" Text="Forward Leave" runat="server"></asp:Label></label>
                                        <asp:TextBox MaxLength="6" onkeypress="return isNumericKeyStrokeDecimal(event)" ID="txtfwd" CssClass="textfields form-control input-sm width-100px custom-maxlength number-dot leavedays" runat="Server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label><asp:Label ID="Label2" Text="Leave Transfer In Year" runat="server"></asp:Label></label>
                                        <asp:DropDownList ID="cmbYear" DataTextField="id" CssClass="textfields form-control input-sm" DataValueField="id" DataSourceID="xmldtYear1"
                                            AutoPostBack="false" runat="server"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>

                                    <div class="form-group">
                                        <label><asp:Label ID="lblTrdate" Text="Transfer Date" runat="server"></asp:Label></label>
                                        <radCln:RadDatePicker CssClass="trstandtop" Calendar-ShowRowHeaders="false" ID="rdTrdate"
                                            DateInput-Enabled="false" runat="server">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group">
                                            <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetch" CssClass="btn red btn-circle btn-sm" OnClick="bindgrid" runat="server">GO</asp:LinkButton>
                                        </div>
                                </div>
                                
                            </div>



                            <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    
                                </tr>
                                <tr>
                                    <td>--%>


                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>
                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true"
                                AllowPaging="true" PageSize="200" GridLines="None" Skin="Outlook" Width="99%"
                                OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                                <MasterTableView AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="emp_code"
                                    DataSourceID="SqlDataSource2">
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

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                            DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="lastyearleaves" HeaderText="LastYrBalLeave" ReadOnly="True"
                                            SortExpression="LastYr_leaves_allowed" UniqueName="Type">
                                            <HeaderStyle Width="170px" />
                                                        <ItemStyle Width="170px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="company_policy" HeaderText="MaxAllowedToTransfer"
                                            ReadOnly="True" SortExpression="company_policy" UniqueName="company_policy">
                                            <HeaderStyle Width="170px" />
                                                        <ItemStyle Width="170px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="TotalLastYrLeaves" HeaderText="TotalLastYrLeaveForTransfer"
                                            ReadOnly="True" SortExpression="TotalLastYrLeaves" UniqueName="TotalLastYrLeaves">
                                            <HeaderStyle Width="170px" />
                                                        <ItemStyle Width="170px" />
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CurrentYear" HeaderText="CurrentYrLeave" SortExpression="CurrentYear"
                                            UniqueName="CurrentYear">
                                            <HeaderStyle Width="170px" />
                                                        <ItemStyle Width="170px" />
                                        </radG:GridBoundColumn>
                                        <%--                                <radG:GridBoundColumn DataField=" RemLastYrLeaves" Visible="false" HeaderText="RemLastYrLeaves"
                                    ReadOnly="True" SortExpression="RemLastYrLeaves" UniqueName="RemLastYrLeaves">
                                </radG:GridBoundColumn>
                                        --%>
                                        <radG:GridBoundColumn DataField="TotalAvailLeaves" HeaderText="TotalAvlLeave" SortExpression="TotalAvailLeaves"
                                            UniqueName="TotalAvailLeaves">
                                            <HeaderStyle Width="170px" />
                                                        <ItemStyle Width="170px" />
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
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </radG:RadGrid>

                            <%-- </td>
                                </tr>
                            </table>--%>


                            <%-- SelectCommand="sp_trans_leave"--%>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="groupid" Type="Int32" ControlID="cmbEmpgroup" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="String" />
                                    <asp:ControlParameter ControlID="txtfwd" Name="leaves" PropertyName="text" Type="double" />
                                    <asp:ControlParameter ControlID="rdTrdate" Name="applydateon" PropertyName="SelectedDate"
                                        Type="datetime" />
                                    <asp:SessionParameter Name="compid" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                           <%--by jammu office <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbEmpgroup"
                                Display="None" ErrorMessage="Employee Group Name is not selected!" InitialValue=""></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtfwd"
                                Display="None" ErrorMessage="Please Enter Forward Leave!" InitialValue=""></asp:RequiredFieldValidator> by jammu office--%>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
                                ShowMessageBox="True" ShowSummary="False" />
                           
                                <div class="text-center margin-top-10">

                                    <asp:Button ID="Button2" runat="server" Text="Transfer Leave" class="textfields btn red"
                                        OnClick="Button2_Click" />
                                </div>
                            
                            <center>
                                <asp:Label ID="lblmsg" CssClass="bodytxt hidden" ForeColor="red" runat="server" Visible="false"></asp:Label>
                            </center>
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
          window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }

        $(document).ready(function () {
            $("div#rdTrdate_wrapper").removeAttr("style");
            $('#imgbtnfetch').click(function () {
                return validateform();
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
                    WarningNotification("Atleast on record must be selected from grid.");
                    return false;
                }

            });
        })
        function validateform() {
            var _message = "";
            if ($.trim($("#cmbEmpgroup").val()) === "")
                _message = "Employee Group Name is not selected.";
            if ($.trim($("#cmbYear").val()) === "0")
                if (_message!="")
                    _message += "<br/>Year is not selected.";
                else
                    _message = "Year is not selected.";
            if ($.trim($("#txtfwd").val()) === "")
                if (_message != "")
                    _message += "<br/>Enter Forward Leave.";
                else
                    _message = "Enter Forward Leave.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

        $(document).on("focusout", ".leavedays", function (e) {
            //$(".leapyearckeck").keypress(function (e) {
            var _cntrl = $(this);
            var newString = $(_cntrl).val();
            var year = $("#cmbYear").val();
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
                if (newString > 366) {
                    // e.preventDefault();
                    WarningNotification($("#cmbYear").val() + " is a Leap year. So Leave days more than 366 are not allowed");
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
        $(document).on("keypress", ".leavedays", function (e) {
            var _cntrl = $(this);
            var newString = $(_cntrl).val() + String.fromCharCode(e.keyCode);
            var year = $("#cmbYear").val();
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
                if (newString > 366) {
                    e.preventDefault();
                    WarningNotification($("#cmbYear").val() + " is a Leap year. So Leave days more than 366 are not allowed");
                    return false;
                }
            }
            else {
                if (newString > 365) {
                    e.preventDefault();
                    WarningNotification("Leave days more than 365 are not allowed");
                    return false;
                }
            }


        });
    </script>
</body>
</html>
