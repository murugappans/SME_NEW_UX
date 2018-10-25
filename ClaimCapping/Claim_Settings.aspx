<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Claim_Settings.aspx.cs" Inherits="SMEPayroll.ClaimCapping.Claim_Settings" %>

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
                        <li>Manage Claims Settings</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="../Payroll/claim-dashboard.aspx">Claims</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Claims Settings</span>
                        </li>
                    </ul>
                </div>               

                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                           

                            <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="false"
                                        Skin="Outlook" Width="40%" runat="server" GridLines="None" AllowPaging="true"
                                        AllowMultiRowSelection="true" PageSize="50" OnItemDataBound="RadGrid1_ItemDataBound">
                                        <MasterTableView CommandItemDisplay="bottom" EditMode="InPlace" AutoGenerateColumns="False"
                                            AllowAutomaticUpdates="true" AllowAutomaticInserts="true" AllowAutomaticDeletes="true"
                                            TableLayout="Auto" DataKeyNames="id,FieldName,Enable,Required,DefaultValue">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <CommandItemTemplate>
                                                <div class="textfields" style="text-align: center">
                                                    <asp:Button ID="btnUpdate" runat="server" class="textfields btn red"
                                                        Text="Update" OnClientClick="return chkChecked();" CommandName="UpdateAll" />
                                                </div>
                                            </CommandItemTemplate>
                                            <Columns>
                                               
                                                <radG:GridBoundColumn DataField="id" Display="false" DataType="System.Int32"
                                                    HeaderText="ID" SortExpression="id" UniqueName="id">
                                                </radG:GridBoundColumn>

                                                <radG:GridBoundColumn DataField="FieldName" HeaderText="Field Name" UniqueName="FieldName" >
                                                </radG:GridBoundColumn>

                                                <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Enable"
                                                    UniqueName="Enable" HeaderText="Enable">
                                                    <ItemStyle HorizontalAlign="center" />
                                                    <HeaderStyle HorizontalAlign ="Center" Width ="70px" />
                                                    <ItemTemplate>
                                                        
                                                    <asp:CheckBox ID="chkEnable" CssClass="custom-maxlength chkRemainder numericonly text-right" 
                                                         Checked ='<%# DataBinder.Eval(Container,"DataItem.Enable")%>' runat="server"  AutoPostBack ="true"   OnCheckedChanged ="chkEnable_OnCheckedChanged" />
                                                   
                                                    </ItemTemplate>
                                                   
                                                </radG:GridTemplateColumn>

                                                <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="Required"
                                                    UniqueName="Required" HeaderText="Required">
                                                   <ItemStyle  HorizontalAlign="Center"  />
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                    <asp:CheckBox ID="chkReuired" CssClass="custom-maxlength chkRemainder numericonly text-right" 
                                                         Checked ='<%# DataBinder.Eval(Container,"DataItem.Required")%>' runat="server"  />
                                                     </ItemTemplate>
                                                    
                                                </radG:GridTemplateColumn>
                                                
                                                <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="DefaultValue"
                                                    UniqueName="DefaultValue" HeaderText="DefaultValue">
                                                    <ItemStyle HorizontalAlign="center" />
                                                 <ItemTemplate>
                                                        <asp:TextBox ID="txtdefault" CssClass="form-control input-sm custom-maxlength chkRemainder  text-right"
                                                            runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DefaultValue")%>' ></asp:TextBox>
                                                    <asp:DropDownList ID="ddldefault" runat="server" Visible ="false" CssClass="form-control input-sm ">
                                                        <asp:ListItem Value ="CURRENTDATE">Current Date</asp:ListItem>
                                                        <asp:ListItem Value ="CUSTOMDATE">Custom Date</asp:ListItem>
                                                        
                                                    </asp:DropDownList>
                                                    </ItemTemplate>
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




                        </form>


                    </div>
                </div>










            </div>
            <!-- END CONTENT BODY -->
        </div>
      

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
