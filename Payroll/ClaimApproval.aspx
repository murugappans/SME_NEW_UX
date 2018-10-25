<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimApproval.aspx.cs"
    Inherits="SMEPayroll.Payroll.ClaimApproval" %>

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
                        <li>Approve / Reject Claim Request</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Pending Approval</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Approve / Reject Claim Request</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12">
                                    Supervisor:
                                                        <asp:Label ID="lblsuper" runat="server" Text="Label" Width="220px" Height="16px"
                                                            CssClass="bodytxt"></asp:Label>
                                </div>
                                
                            </div>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                    function MyClick(sender, e) {
                                        var inputs = document.getElementById("<%= RadGrid1.MasterTableView.ClientID %>").getElementsByTagName("input");

                                        for (var i = 0, l = inputs.length; i < l; i++) {
                                            var input = inputs[i];
                                            if (input.type != "radio" || input == sender)
                                                continue;
                                            input.checked = false;
                                            //document.getElementById("divRemarks").innerText =input.name;
                                        }
                                    }
                                </script>

                            </radG:RadCodeBlock>
                            <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>


                            <%--Commented By Jaspreet--%>
                            <%--<table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
                                border="0">
                                <tr>
                                    <td>--%>


                            <radG:RadGrid ID="RadGrid1"  runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true"
                                GridLines="None" Skin="Outlook" Width="100%" OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                                <MasterTableView AutoGenerateColumns="False" DataKeyNames="trx_id,emp_code,remarks,emp_name" DataSourceID="SqlDataSource2" AllowSorting="true">
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

                                       <%-- <radG:GridTemplateColumn HeaderText="Select" UniqueName="TemplateColumn1">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="remarkRadio" AutoPostBack="true" OnCheckedChanged="remarkRadio_CheckedChanged"
                                                    GroupName="rad" runat="server" onclick="MyClick(this,event)" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px"  />
                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>--%>


                                        
                                         <radG:GridTemplateColumn UniqueName="CheckBoxTemplateColumn">
                                           <ItemTemplate>
                                         <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="remarkRadio_CheckedChanged"  AutoPostBack="True" />
                                            </ItemTemplate>
                                      <HeaderTemplate>
                                         <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="ToggleSelectedState"
                                                                                    AutoPostBack="True" />
                                                            </HeaderTemplate>
                                             <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                             <ItemStyle HorizontalAlign="Center" />
                                      </radG:GridTemplateColumn>




                                        <radG:GridBoundColumn DataField="emp_code" Visible="False" HeaderText="Code" SortExpression="emp_code"
                                            UniqueName="emp_code">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="cd" Visible="True" HeaderText="Application Date" SortExpression="cd"
                                            UniqueName="cd">
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                            UniqueName="emp_name">
                                            <%--<ItemStyle Width="20%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_id" DataType="System.Int32" HeaderText="Id"
                                            ReadOnly="True" SortExpression="trx_id" Visible="False" UniqueName="trx_id">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="desc" HeaderText="Claim Type" ReadOnly="True" SortExpression="desc"
                                            UniqueName="Type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="Department" HeaderText="Department" ReadOnly="True"
                                            SortExpression="Department" UniqueName="Department">
                                            <%--<ItemStyle Width="10%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="id" HeaderText="id" ReadOnly="True" SortExpression="Type"
                                            Visible="False" UniqueName="Type">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_amount" DataType="System.Decimal" HeaderText="Amount"
                                            SortExpression="trx_amount" UniqueName="trx_amount">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="CurrencyID" HeaderText="Currency" AllowFiltering="false"
                                            ReadOnly="True" SortExpression="CurrencyID" UniqueName="CurrencyID" Display="True">
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="trx_period" DataType="System.DateTime" HeaderText="Period"
                                            SortExpression="trx_period" UniqueName="trx_period">
                                        </radG:GridBoundColumn>
                                        <radG:GridTemplateColumn HeaderText="Attached Document">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="h1" runat="server" Target="_blank" Text='<%# Eval("recpath")%>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle Width="142px"  />
                                            <ItemStyle Width="142px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>
                                        <radG:GridBoundColumn DataField="remarks" DataType="System.String" HeaderText="remarks"
                                            ReadOnly="True" SortExpression="remarks" UniqueName="remarks" Visible="False">
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
                                <ClientSettings>
                                    <Selecting AllowRowSelect="True" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="true" />
                                </ClientSettings>
                            </radG:RadGrid>


                            <%--</td>
                                </tr>
                            </table>--%>


                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppendingclaim_add"
                                SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue=" " Name="approver" SessionField="Emp_Name" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                    <asp:SessionParameter Name="UserID" SessionField="EmpCode" Type="String" />
                                    <asp:ControlParameter ControlID="textbox1" Name="txt" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                <AjaxSettings>
                                    <radG:AjaxSetting AjaxControlID="imgbtnfetch">
                                        <UpdatedControls>
                                            <radG:AjaxUpdatedControl ControlID="RadGrid1" />
                                        </UpdatedControls>
                                    </radG:AjaxSetting>
                                </AjaxSettings>
                            </radG:RadAjaxManager>


                            <table>
                                <tr id="rowApp" runat="server" visible="false">
                                    <td class="bodytxt">
                                        <%-- <label id="lblApprovalinfo1" runat="server" style="color:Red;font-weight:bold" >
                    </label>--%>
                                        <asp:Label ID="lblApprovalinfo1" runat="server" Text="" ></asp:Label><%--Added by Sandi--%>
                                    </td>
                                </tr>
                            </table>



                            <div class="row margin-top-20">
                                <div class="col-md-6 form-group">
                                    <label>Employee Remarks</label>
                                    <asp:TextBox runat="server" ID="txtEmpRemarks" name="txtEmpRemarks" disabled="disabled" TextMode="MultiLine" data-MaxLength="255" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6 form-group">
                                    <label>Remarks</label>
           <%--  //change is must   --%> <asp:TextBox runat="server" ID="txtremarks" data-MaxLength="250" TextMode="MultiLine" CssClass="form-control  custom-maxlength"></asp:TextBox>
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="ButtonApprove" runat="server" Text="Approve Claim" class="textfields btn red" OnClick="Button3_Click" />
                                    <asp:Button ID="ButtonReject" runat="server" Text="Reject Claim" class="textfields btn default" OnClick="Button2_Click" />
                                </div>
                            </div>




                            <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>
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
       
        $(document).on('click', '#ButtonApprove', function () { 
      
            return ckgrid();

        });
    
        $(document).on('click', '#ButtonReject', function () {
            return ckgrid();
        });
        function ckgrid() {
            var alertmsg = "";
             var grid = $find("<%= RadGrid1.ClientID %>"); 
            var rowCount = grid.get_masterTableView().get_dataItems().length;
            if (rowCount < 1) {
                alertmsg = " No Claims to Approve or Reject";

            }
            //else {
            //    if ($("#RadGrid1_ctl00 tbody tr td").find('input[type=radio]:checked').length < 1) {
            //        alertmsg ="Atleast one record must be selected from grid.";
                    
            //    }
            }
            if (alertmsg != "") {
                WarningNotification(alertmsg);
                return false;
            }

           
        }
        $("input[type='button']").removeAttr("style");
        $(".RadGrid_Outlook .rgAdd + a, .RadGrid_Outlook .rgRefresh + a").addClass("btn default btn-xs");
       window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
        }


    </script>


    

</body>
</html>
