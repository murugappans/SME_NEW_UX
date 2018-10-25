<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCeilingDetails.aspx.cs" Inherits="SMEPayroll.Management.EmployeeCeilingDetails" %>


<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
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





    <style type="text/css">
        .labelOne {
            background-color: #FFFFFF;
            filter: progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#363636',EndColorStr='#FFFFFF');
            margin: 0px auto;
        }
    </style>
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .viscol {
            display: block;
        }
    </style>



    <style type="text/css">
        .SelectedRow {
            background: #ffffff !important; /*white */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .SelectedRowLock {
            background: #dcdcdc !important; /*Lock Row */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .SelectedRowExceptionForOuttime {
            background: #996633 !important; /*Maroon*/
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .SelectedRowExceptionFlexibleInOutTimeCompareProject {
            background: #99FFCC !important; /*Green */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }


        .SelectedRowExceptionForIntimeWithEarylyInByTime {
            background: #FFFFCC !important; /*Yellow */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .SelectedRowException {
            background: #CCFF33 !important; /*purple*/
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .SelectedRowExceptionForInorOutBlank {
            background: #800000 !important; /*Red */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        .NormalRecordChk {
            background: #E5E5E5 !important; /*Red */
            height: 22px;
            border: solid 1px #e5e5e5;
            border-top: solid 1px #e9e9e9;
            border-bottom: solid 1px white;
            padding-left: 4px;
        }

        html, body, form {
            /*height: 100%;   */
            /*height: 100%;
            margin: 0px;
            padding: 0px;
            overflow: auto;*/
        }
    </style>





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
                        <li>Employee Ceiling Details</li>
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
                            <span>Employee Ceiling Details</span>
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

                        <form id="form1" runat="server" method="post">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                            </telerik:RadScriptManager>


                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" AnimationDuration="1500" runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
                            </telerik:RadAjaxLoadingPanel>


                            <%--<uc1:TopRightControl ID="TopRightControl2" runat="server" />--%>


                            <div class="search-box padding-tb-10 clearfix">
                                <div class="form-inline col-sm-12">
                                    <div class="form-group hidden">
                                        <label>&nbsp;</label>
                                        <radG:RadComboBox Visible="false" ID="RadComboBoxEmpPrj" runat="server" BorderWidth="0px"
                                            AutoPostBack="true" EmptyMessage="Currency" HighlightTemplatedItems="true" Enabled="false"
                                            EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested" DropDownWidth="240px" Height="200px"
                                            OnSelectedIndexChanged="RadComboBoxEmpPrj_SelectedIndexChanged">
                                            <HeaderTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0" style="width: 180px">
                                                    <tr>
                                                        <td style="width: 60px;">Currency</td>
                                                        <td style="width: 60px;"></td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <table class="bodytxt" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td style="width: 60px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Currency']")%>
                                                        </td>
                                                        <td style="width: 60px;">
                                                            <%# DataBinder.Eval(Container, "Attributes['Symbol']")%>
                                                        </td>

                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </radG:RadComboBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Effective Date</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                            runat="server">
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
                                        <label>Department</label>
                                        <asp:DropDownList CssClass="textfields form-control input-sm"
                                            ID="deptID" DataTextField="DeptName" DataValueField="ID" DataSourceID="SqlDataSource2"
                                            runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group hidden">
                                        <label>NightShift</label>

                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="imgbtnfetchEmpPrj" CssClass="btn red btn-circle btn-sm" runat="server">GO</asp:LinkButton>
                                    </div>
                                    <div class="form-group hidden">
                                        <label>&nbsp;</label>
                                        <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"
                                            runat="server">
                                            <Calendar runat="server">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="crntDay">
                                                    </telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radCln:RadDatePicker>
                                    </div>
                                    <div class="form-group hidden">
                                        <label>&nbsp;</label>
                                        <asp:CheckBoxList Visible="True" ID="chkrecords" runat="server" CssClass="bodytxt" ValidationGroup="val1" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" CausesValidation="true">
                                            <asp:ListItem Selected="False"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnReport" CssClass="btn btn-sm default" Visible="false" runat="server" Text="View report" />
                                    </div>
                                </div>
                            </div>

                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr style="display: none">

                                    <td class="bodytxt" valign="bottom" align="Left">NH                                            
                                    </td>
                                    <td class="bodytxt" valign="bottom" align="Left">OT1 
                                    </td>

                                    <td class="bodytxt" valign="bottom" align="Left">OT2 
                                    </td>

                                    <td></td>

                                    <td></td>


                                </tr>
                                <tr style="display: none">
                                    <td class="bodytxt" align="left" valign="top" style="width: 15%;">
                                        <asp:TextBox Visible="True" Text='' ID="DeftxtInTime" runat="server" Width="70%"
                                            ValidationGroup="vldSum" /></td>

                                    <td class="bodytxt" align="left" valign="top" style="width: 15%;">
                                        <asp:TextBox Visible="True" Text='' ID="DeftxtOutTime" runat="server" Width="70%"
                                            ValidationGroup="vldSum" /></td>

                                    <td class="bodytxt" align="left" valign="top" style="width: 15%;">
                                        <asp:TextBox Visible="True" Text='' ID="txtOt2" runat="server" Width="70%"
                                            ValidationGroup="vldSum" /></td>

                                    <td align="left" valign="top" style="vertical-align: top">
                                        <asp:Button ID="btnCopy" Visible="true" runat="server" Text="Key In/Out Time" OnClientClick="return Copy();" />
                                    </td>

                                    <td style="width: 0%;" valign="top">
                                        <%--<ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderIn" runat="server" TargetControlID="DeftxtInTime"
                                                                    Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                                                <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorIn" runat="server" ControlExtender="DefMaskedEditExtenderIn"
                                                                    ControlToValidate="DeftxtInTime" IsValidEmpty="False" InvalidValueMessage="*"
                                                                    ValidationGroup="vldSum" Display="Dynamic" />--%>
                                    </td>

                                    <td valign="top">
                                        <%--<ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderOut" runat="server" TargetControlID="DeftxtOutTime"
                                                                    Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    MaskType="Time" AcceptAMPM="false" CultureName="en-US" />--%>
                                    </td>

                                    <td valign="top">
                                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtOt2"
                                                                    Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                    MaskType="Time" AcceptAMPM="false" CultureName="en-US" />--%>
                                    </td>
                                </tr>
                                <tr id="tr1" runat="server">
                                </tr>
                            </table>

                            <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon" Width="200%"></asp:Label>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">

                                    function isNumericKeyStrokeDecimal(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode
                                        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46))
                                            return false;

                                        return true;
                                    }


                                    function RowDblClick(sender, eventArgs) {
                                        //sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                        //alert('hi1');
                                        //eventArgs.set_cancel(true); 


                                        //                                    var grid = $find("");	                    
                                        //                                    var masterTableView = grid.get_masterTableView();
                                        //                                    var selectedRows = masterTableView.get_selectedItems();                        
                                        //                                    //Check Roster Type
                                        //                                    var rosterType;
                                        //                                    var msg='';
                                        //                                    var rowno='';
                                        //                                    
                                        //                                    //alert('hi');
                                        //                                    for (var i = 0; i < selectedRows.length; i++) 
                                        //                                    { 
                                        //                                        var row                  =   selectedRows[i];                                
                                        //                                        var cell                 =   masterTableView.getCellByColumnUniqueName(row, "GridClientSelectColumn"); 
                                        //                                        alert(cell.innerHTML);
                                        //                                    }
                                    }
                                </script>

                            </radG:RadCodeBlock>


                            <radG:RadGrid ID="RadGrid2" runat="server" CssClass="radGrid-single"
                                OnItemDataBound="RadGrid2_ItemDataBound"
                                Width="98%" Visible="false"
                                AllowFilteringByColumn="false"
                                AllowSorting="false"
                                Skin="Outlook"
                                EnableAjaxSkinRendering="true"
                                MasterTableView-AllowAutomaticUpdates="true"
                                MasterTableView-AutoGenerateColumns="false"
                                MasterTableView-AllowAutomaticInserts="False"
                                MasterTableView-AllowMultiColumnSorting="False"
                                GroupHeaderItemStyle-HorizontalAlign="left"
                                ClientSettings-EnableRowHoverStyle="false"
                                ClientSettings-AllowColumnsReorder="false"
                                ClientSettings-ReorderColumnsOnClient="false"
                                ClientSettings-AllowDragToGroup="False"
                                ShowFooter="true"
                                ShowStatusBar="true"
                                AllowMultiRowSelection="true"
                                PageSize="50"
                                AllowPaging="true">

                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <SelectedItemStyle CssClass="SelectedRow" />
                                <MasterTableView>
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <Columns>
                                        <radG:GridBoundColumn Display="false" DataField="id" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="True" DataField="EmpCode" HeaderText="EmployeeCode" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="EmpCode">
                                            <%--<ItemStyle Width="4%" HorizontalAlign="center" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn Display="false" DataField="CompanyId" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="CompanyId">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn Display="True" DataField="emp_name" HeaderStyle-ForeColor="black" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" UniqueName="emp_name">
                                            <%--<ItemStyle Width="15%" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>


                                        <radG:GridTemplateColumn DataField="NH" UniqueName="NH" HeaderText="NH" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.NH")%>' ID="txtNH"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>



                                        <radG:GridTemplateColumn DataField="OT1" UniqueName="OT1" HeaderText="OT1" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.OT1")%>' ID="txtOT1"
                                                        runat="server" ValidationGroup="vldSum" />

                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>



                                        <radG:GridTemplateColumn DataField="OT2" UniqueName="OT2" HeaderText="OT2" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.OT2")%>' ID="txtOT2"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>


                                        <radG:GridTemplateColumn DataField="Days" UniqueName="Days" HeaderText="Days" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.Days")%>' ID="txtDays"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridTemplateColumn DataField="V1" UniqueName="V1" HeaderText="V1" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.V1")%>' ID="txtV1"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>


                                        <radG:GridTemplateColumn DataField="V2" UniqueName="V2" HeaderText="V2" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.V2")%>' ID="txtV2"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>


                                        <radG:GridTemplateColumn DataField="V3" UniqueName="V3" HeaderText="V3" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.V3")%>' ID="txtV3"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>


                                        <radG:GridTemplateColumn DataField="V4" UniqueName="V4" HeaderText="V4" HeaderStyle-HorizontalAlign="Center"
                                            AllowFiltering="false" Groupable="false">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:TextBox CssClass="form-control input-sm number-dot" MaxLength="12" onkeypress="return isNumericKeyStrokeDecimal(event)" Text='<%# DataBinder.Eval(Container,"DataItem.V4")%>' ID="txtV4"
                                                        runat="server" ValidationGroup="vldSum" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="60px" HorizontalAlign="center" />
                                            <ItemStyle Width="60px" HorizontalAlign="center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn Display="True" DataField="time_card_no" HeaderText="TimeCardNo" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="time_card_no">
                                            <%--<ItemStyle Width="8%" HorizontalAlign="center" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn Display="True" DataField="DeptName" HeaderText="Department Name" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="DeptName">
                                            <%--<ItemStyle Width="8%" HorizontalAlign="center" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                            <HeaderStyle Width="30px" HorizontalAlign="center" />
                                            <ItemStyle Width="30px" HorizontalAlign="center" />
                                        </radG:GridClientSelectColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings>
                                    <Selecting AllowRowSelect="true" />
                                    <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false" AllowColumnResize="false"></Resizing>
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                            </radG:RadGrid>
                            <div class="text-center padding-top-20">
                                <asp:Button ID="btnUpdate" CssClass="btn btn-sm red" runat="server" Text="Submit Selected" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnApprove" CssClass="btn btn-sm default" runat="server" OnClick="btnApprove_Click" Visible="false" Text="Approve Selected" />
                                <asp:Button ID="btnSubApprove" CssClass="btn btn-sm default" runat="server" Visible="false" Text="Submit/Approve Selected" />
                                <asp:Button ID="btnUnlock" CssClass="btn btn-sm default" runat="server" Visible="false" Text="Unlock Selected" />
                                <asp:Button ID="btnDelete" CssClass="btn btn-sm default" runat="server" Visible="false" OnClick="btnDelete_Click" Text="Delete Selected" />
                            </div>

                            <table>
                                <tr id="trbutton" visible="false" runat="server">
                                    <td>
                                        <asp:ImageButton CssClass="btn" ID="btnExportWord" AlternateText="Export To Word" OnClick="btnExportWord_click"
                                            runat="server" ImageUrl="~/frames/images/Reports/exporttoWordl.jpg" />
                                        <asp:ImageButton CssClass="btn" ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click"
                                            runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" />
                                        <asp:ImageButton CssClass="btn" ID="btnExportPdf" AlternateText="Export To PDF" OnClick="btnExportPdf_click"
                                            runat="server" ImageUrl="~/frames/images/Reports/exporttopdf.jpg" />
                                    </td>
                                </tr>
                                <tr id="rowreport" runat="server">
                                    <td>
                                        <radG:RadGrid
                                            ID="RadGrid1" runat="server" Visible="false"
                                            Width="80%"
                                            AllowFilteringByColumn="false"
                                            AllowSorting="False"
                                            Skin="Outlook"
                                            EnableAjaxSkinRendering="true"
                                            MasterTableView-AllowAutomaticUpdates="true"
                                            MasterTableView-AutoGenerateColumns="false"
                                            MasterTableView-AllowAutomaticInserts="False"
                                            MasterTableView-AllowMultiColumnSorting="False"
                                            GroupHeaderItemStyle-HorizontalAlign="left"
                                            ClientSettings-EnableRowHoverStyle="false"
                                            ClientSettings-AllowColumnsReorder="false"
                                            ClientSettings-ReorderColumnsOnClient="false"
                                            ClientSettings-AllowDragToGroup="False"
                                            ShowFooter="true"
                                            ShowStatusBar="true"
                                            AllowMultiRowSelection="true"
                                            PageSize="50"
                                            AllowPaging="true">
                                            <PagerStyle Mode="NextPrevAndNumeric" />
                                            <SelectedItemStyle CssClass="SelectedRow" />
                                            <MasterTableView>
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <Columns>
                                                    <radG:GridBoundColumn HeaderText="Date" DataField="Date" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="Date">
                                                    </radG:GridBoundColumn>
                                                    <%--4--%>
                                                    <radG:GridBoundColumn DataField="Curr" HeaderText="Currency" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="Curr">
                                                        <ItemStyle Width="50%" HorizontalAlign="center" />
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Rate" HeaderText="Rate" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="Rate">
                                                        <ItemStyle Width="50%" HorizontalAlign="center" />
                                                    </radG:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>

                                        </radG:RadGrid>
                                    </td>
                                </tr>

                            </table>

                            <div>
                                <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="True" />
                                <asp:Label ID="lblV1" runat="server" Width="0%" Height="0%"></asp:Label>
                                <asp:Label ID="lblV2" runat="server" Width="0%" Height="0%"></asp:Label>
                                <asp:Label ID="lblV3" runat="server" Width="0%" Height="0%"></asp:Label>
                                <asp:Label ID="lblV4" runat="server" Width="0%" Height="0%"></asp:Label>
                            </div>
                            
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource6" runat="server"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select 'ALL' as DeptName,'-1' as ID union SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by id">
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
        //$("#RadGrid1_GridHeader table.rgMasterTable td input[type='text']").addClass("form-control input-sm");
        //$(".rtbUL .rtbItem a.rtbWrap").addClass("btn btn-sm bg-white font-red");
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }

        $("#btnUpdate,#btnReport,#btnUpdate,#btnApprove,#btnSubApprove,#btnUnlock,#btnDelete").click(function () {

            var _msg = "";
            if ($("#RadGrid2_ctl00").length == 0 || $("#RadGrid2_ctl00 tbody tr td:contains(No records to display.)").length > 0)
                _msg += "No employee records are found.  <br/>";
            else
                if ($("#RadGrid2_ctl00 tbody tr td").find('input[type=checkbox]:checked').length < 1)
                    _msg += "Atleast one record must be selected. <br/>";


            if (_msg != "") {
                WarningNotification(_msg);
                return false;
            }


        });
    </script>
</body>
</html>
