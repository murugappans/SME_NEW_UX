<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveRollback.aspx.cs"
    Inherits="SMEPayroll.Leaves.LeaveRollback" %>

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
<head runat="server">

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
                        <li>Leave Rollback</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="leave-dashboard.aspx">Leave</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Leave Rollback</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Leave Rollback</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12 single-table-design">

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
                                <div class="form-inline col-md-12">
                                    <div class="form-group">
                                        <label><asp:Label ID="lbl1" Text="Employee Group" runat="server"></asp:Label></label>
                                        <asp:DropDownList ID="cmbEmpgroup" runat="server" CssClass="textfields form-control input-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label><asp:Label ID="Label2" Text="Leave Rollback From" runat="server"></asp:Label></label>
                                        <%--<asp:DropDownList ID="cmbYear" AutoPostBack="true" runat="server" CssClass="textfields">
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
                                        <asp:DropDownList ID="cmbYear" CssClass="textfields form-control input-sm" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                            AutoPostBack="true" runat="server"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                        <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>
                                    </div>
                                    <div class="form-group">
                                        <label><asp:Label Visible="false" ID="Label1" Text="Forward Leave :" runat="server"></asp:Label></label>
                                        <asp:TextBox MaxLength="4" Visible="false" onkeypress="return isNumericKeyStrokeDecimal(event)" ID="txtfwd" Style="height: 13px" CssClass="textfields" runat="Server"
                                            Width="30px"></asp:TextBox>
                                        <label><asp:Label ID="lblTrdate" Text="Rollback Date" runat="server"></asp:Label></label>
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


                            <%-- Commented By Jaspreet--%>

                            <%--<table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0" class="single-table-design">
                                <tr>
                                    
                                </tr>
                                <tr>
                                    
                                </tr>
                                <tr>
                                    <td>--%>
                            <div>
                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>                                                      
                            </div>



                            <div class="panel-group accordion accordion-note no-margin hidden" id="accordion3">
                                        <div class="panel panel-default shadow-none">
                                            <div class="panel-heading bg-color-none">
                                                <h4 class="panel-title">
                                                    <a class="accordion-toggle  collapsed" data-toggle="collapse" data-parent="#accordion3" href="#collapse_3_1"><i class="icon-info"></i></a>
                                                </h4>
                                            </div>
                                            <div id="collapse_3_1" class="panel-collapse collapse">
                                                <div class="panel-body border-top-none no-padding">
                                                    <div class="note-custom note">
                                                        <ul class="list-inline no-margin">
                                                            <li><asp:Label ID="lblCurrYr" class="bodytxt" runat="server"></asp:Label></li>
                                                            <li><asp:Label ID="lblPY" class="bodytxt" runat="server"></asp:Label></li>
                                                        </ul>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                            <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true"
                                AllowPaging="true" PageSize="200" GridLines="None" Skin="Outlook" Width="100%"
                                OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                                <MasterTableView AllowPaging="true" AutoGenerateColumns="False" DataKeyNames="emp_code" Width="99.9%"
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
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridClientSelectColumn>
                                        <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn CurrentFilterFunction="contains" AutoPostBackOnFilter="true"
                                            DataField="emp_name" HeaderText="Emp Name" SortExpression="emp_name" UniqueName="emp_name">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leaves_allowed_CY" HeaderText="Leave Allowed Current Year*" ReadOnly="True"
                                            SortExpression="leaves_allowed_CY" UniqueName="leaves_allowed_CY">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leaves_CF_LY1" HeaderText="Leave Foward Current Year*" ReadOnly="True"
                                            SortExpression="leaves_CF_LY1" UniqueName="leaves_CF_LY1">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leaves_elapsed" HeaderText="Leave Forefited" ReadOnly="True"
                                            SortExpression="leaves_elapsed" UniqueName="leaves_elapsed">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leaves_allowed_LY" HeaderText="Leave Allowed Last Year*"
                                            ReadOnly="True" SortExpression="leaves_allowed_LY" UniqueName="leaves_allowed_LY">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="leaves_CF_LY" HeaderText="Leave Foward Last Year*"
                                            ReadOnly="True" SortExpression="leaves_CF_LY" UniqueName="leaves_CF_LY">
                                        </radG:GridBoundColumn>


                                        <radG:GridBoundColumn DataField="TimeCardId" HeaderText="Time Card ID" ShowFilterIcon="False" CurrentFilterFunction="StartsWith" AllowFiltering="true" AutoPostBackOnFilter="true"
                                            ReadOnly="True" SortExpression="TimeCardId" UniqueName="TimeCardId">
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
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="Department" UniqueName="Department" Display="false">
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



                            <%--</td>
                                </tr>
                            </table>--%>




                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_GetROLLBackLeaves"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter Name="Groupid" Type="Int32" ControlID="cmbEmpgroup" />
                                    <asp:ControlParameter ControlID="cmbYear" Name="year" PropertyName="SelectedValue"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbEmpgroup"
                                Display="None" ErrorMessage="Employee Group Name is not selected!" InitialValue=""></asp:RequiredFieldValidator>
                            <center>
                                <asp:Button ID="Button2" runat="server" Text="Rollback Leave" class="textfields btn btn-sm red"
                                    OnClick="Button2_Click" />
                            </center>
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
        $(".RadPicker.RadPicker_Default.trstandtop").removeAttr("style");
        $(rdTrdate_popupButton).addClass("hidden");
        $("input[type='text']").addClass("form-control");
        $(".riTextBox").removeAttr("style");
          window.onload = function () {
              CallNotification('<%=ViewState["actionMessage"].ToString() %>');
         }
        var _accordionState = false;
        $(document).ready(function () {
            var _accordionstate = localStorage.getItem("AccordionState");
            if ($('#cmbEmpgroup').val() === "" || $('#cmbYear').val() === "0")
                _accordionstate = 'false';
            $('#accordion3').addClass('hidden');
            if (_accordionstate == 'false')
                $('#accordion3').addClass('hidden');
            else
                $('#accordion3').removeClass('hidden');
            $("div#rdTrdate_wrapper").removeAttr("style");
           
            $('#imgbtnfetch').click(function () {
                return validateform();
            });
            $('#cmbYear').change(function () {
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
                _message = "Please Select Employee Group.";
            else if ($.trim($("#cmbYear").val()) === "0")
                _message = "Please select year.";
            if (_message != "") {
                WarningNotification(_message);
                localStorage.setItem("AccordionState", "false");
                $('#accordion3').addClass('hidden');
                return false;
            }
           
            $('#accordion3').removeClass('hidden');
            localStorage.setItem("AccordionState", "true");


            return true;
        }

    </script>
</body>
</html>
