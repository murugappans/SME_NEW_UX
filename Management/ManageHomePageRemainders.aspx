<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageHomePageRemainders.aspx.cs" Inherits="SMEPayroll.Management.ManageHomePageRemainders" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Import Namespace="SMEPayroll" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>

    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta content="width=device-width, initial-scale=1" name="viewport" />

    <uc_css:bundle_css ID="bundle_css" runat="server" />




    <script language="JavaScript1.2" type="text/javascript">
        function isNumericKeyStroke(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
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
                        <li>Manage Home Page Reminders</li>
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
                            <span>Home Page Reminders</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Home Page Reminders</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<div class="search-box clearfix">
                                <div class="col-sm-12 text-right">
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" />
                                </div>
                            </div>--%>

                            <div class="row margin-top-20 grid-style-1">
                                <div class="col-sm-7">
                                    <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="false"
                                        Skin="Outlook" Width="40%" runat="server" GridLines="None" AllowPaging="true"
                                        AllowMultiRowSelection="true" PageSize="50" OnItemDataBound="RadGrid1_ItemDataBound">
                                        <MasterTableView CommandItemDisplay="bottom" EditMode="InPlace" AutoGenerateColumns="False"
                                            AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
                                            TableLayout="Auto" DataKeyNames="Sno,Rem_Type,Sorting">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <CommandItemTemplate>
                                                <div class="textfields" style="text-align: center">
                                                    <asp:Button ID="btnUpdate" runat="server" class="textfields btn red"
                                                        Text="Update Selected" OnClientClick="return chkChecked();" CommandName="UpdateAll" />
                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                                    <ItemStyle Width="35px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="Sno" Display="false" DataType="System.Int32"
                                                    HeaderText="Gid" SortExpression="Sno" UniqueName="Sno">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Sorting" Display="false" DataType="System.Int32"
                                                    HeaderText="Sorting" SortExpression="Sorting" UniqueName="Sorting">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="Rem_Type" HeaderText="Category Name" SortExpression="Rem_Type"
                                                    UniqueName="Rem_Type" AutoPostBackOnFilter="false" CurrentFilterFunction="contains">
                                                    <%--<ItemStyle  HorizontalAlign="Left" Width="60%" />--%>
                                                </radG:GridBoundColumn>

                                                <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Category_Name"
                                                    UniqueName="Category_Name" HeaderText="Reminder Days" AllowFiltering="false">
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReminDays" CssClass="form-control input-sm custom-maxlength chkRemainder numericonly text-right"
                                                            runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Days")%>' MaxLength="3" onkeypress="return isNumericKeyStroke(event)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>


                                                <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Sorting"
                                                    UniqueName="Sort_col" HeaderText="Sorting" AllowFiltering="false">
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drp_sorting" runat="server" CssClass="form-control input-sm">
                                                            <asp:ListItem Text="Asc" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Desc" Value="2"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                    <HeaderStyle Width="150px" HorizontalAlign="Center" />
                                                </radG:GridTemplateColumn>


                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                                            ReorderColumnsOnClient="true">
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                                AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </div>
                                <div class="col-md-5">
                                    <div class="page-links">
                                        <div class="icon">
                                            <i class="fa fa-file-text-o img-icon"></i>
                                        </div>
                                        <div class="detail">
                                            <h3>Reminders to Email</h3>
                                            <span>Manage Remainders Send to Email.</span>
                                            <a href="../Management/EmailRemainder.aspx" class="nav">View <i class="fa fa-long-arrow-right"></i></a>
                                        </div>
                                    </div>
                                </div>
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

          window.onload = function () {
           CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            
        }
       
        $(document).on("focusout", ".chkRemainder", function () {
            if( $(this).val() > 365)
            {
                WarningNotification("Reminder days cannot be more than 365");
                $(this).val("");
            }

        });
     
        function chkChecked () {
          
            var _message = "";
            if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                _message = "Atleast one record must be selected from grid.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }

    </script>

</body>
</html>
