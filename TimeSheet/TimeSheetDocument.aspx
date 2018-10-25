<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeSheetDocument.aspx.cs"
    Inherits="SMEPayroll.TimeSheet.TimeSheetDocument" %>

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





    <script language="JavaScript1.2"> 
<!-- 

if (document.all)
window.parent.defaultconf=window.parent.document.body.cols
function expando(){
window.parent.expandf()

}
document.ondblclick=expando 

-->
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
                        <li>Timesheet</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="Timesheet-Dashboard.aspx">Timesheet</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Import Timesheet</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Timesheet</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>
                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>
                            
                            

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>


                            <div class="clearfix margin-top-10">
                                <div class="col-md-4">Select TimeSheet Type</div>
                                <div class="col-md-4">
                                    <asp:RadioButton ID="rdTs" Text="Single Row" Checked="true" runat="server" GroupName="g1" AutoPostBack="false" />
                                </div>
                                <div class="col-md-4">
                                    <asp:RadioButton ID="rdTs1" Text="Multi Row" runat="server" GroupName="g1" AutoPostBack="false" />
                                </div>
                            </div>
                            <div class="clearfix padding-tb-10">
                                <div class="col-md-4"><label id="lblHlist" runat="server" title="">Download TimeSheet Type</label></div>
                                <div class="col-md-4">
                                    <asp:HyperLink ID="FileDownload" runat="server" Text="Download Single Row TimeSheet" NavigateUrl="http://www.smepayroll.com/download.htm" Target="_search">
                                                    </asp:HyperLink>
                                </div>
                                <div class="col-md-4">
                                    <asp:HyperLink ID="FileDownload1" runat="server" Text="Download Multi Row TimeSheet" NavigateUrl="http://www.smepayroll.com/download.htm" Target="_search">
                                                    </asp:HyperLink>
                                </div>
                                </div>
                            <div class="clearfix">
                                 <div class="col-md-4">
                                     <label>Month</label>
                                     <asp:DropDownList AutoPostBack="true" ID="drpMonth" runat="server" CssClass="textfields form-control input-sm input-small"
                                                            OnSelectedIndexChanged="drpMonth_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1">Select</asp:ListItem>
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
                                     <asp:RequiredFieldValidator ID="rfvMonth" runat="server" ErrorMessage="Please Select Month"
                                                        ControlToValidate="drpMonth" Display="Static" InitialValue="Select">*</asp:RequiredFieldValidator>
                                 </div>
                                 <div class="col-md-4">
                                     <label>Year</label>
                                                                                         <asp:DropDownList ID="cmbYear"  CssClass="textfields form-control input-sm input-small" DataTextField="id" DataValueField="id" DataSourceID="xmldtYear1"
                                                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged" AppendDataBoundItems="true">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year"></asp:XmlDataSource>
                                                    <asp:SqlDataSource ID="xmldtYear1" runat="server" SelectCommand="SELECT YEAR(GETDATE()) - 3 as id  UNION SELECT YEAR(GETDATE()) - 2 as id UNION SELECT YEAR(GETDATE()) - 1 as id UNION SELECT YEAR(GETDATE())  as id UNION SELECT YEAR(GETDATE()) + 1 as id ORDER BY id DESC"></asp:SqlDataSource>

                                 </div>
                                <div class="col-md-4">
                                    <label>Select File</label>
                                                                                        <input id="FileUpload" runat="server" name="FileUpload"  type="file" />
                                    <asp:RequiredFieldValidator
                                                        ID="rfvFileUpload" runat="server" ControlToValidate="FileUpload" Display="Static"
                                                        ErrorMessage="Please Select File">*</asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                                            ID="revFileUpload" runat="Server" ControlToValidate="FileUpload" ErrorMessage="Please Select Excel/CSV Files"
                                                            ValidationExpression=".+\.(([xX][lL][sS])|([cC][sS][vV]))">*</asp:RegularExpressionValidator>

                                </div>
                                </div>

                            <div class="clearfix padding-tb-10">
                                <div class="col-md-12 text-center">
                                    <asp:Button ID="CmdUpload" CssClass="btn red" runat="server" OnClick="CmdUpload_Click" Text="Upload"
                                                        value="Upload" />
                                    </div>
                                </div>

                            <%--<table border="0" cellpadding="1" cellspacing="0" width="100%">

                                <tr>
                                    <td style="width: 982px">
                                        <table style="vertical-align: middle; width: 80%;" align="center" cellpadding="1"
                                            cellspacing="0" border="0">
                                            <tr>
                                                <td class="bodytxt" colspan="1">Select TimeSheet Type
                                                </td>
                                                <td style="" class="bodytxt" colspan="1">
                                                    
                                                </td>
                                                <td class="bodytxt" colspan="2">
                                                    
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="bodytxt" colspan="1">
                                                    
                                                </td>
                                                <td class="bodytxt" colspan="1">
                                                    
                                                </td>
                                                <td class="bodytxt" colspan="2">
                                                    
                                                </td>
                                            </tr>


                                            <tr>
                                                <td style="text-align: right; width: 20%; height: 24px;">
                                                    <tt class="bodytxt">Month :</tt>
                                                </td>
                                                <td style="width: 30%; height: 24px;">
                                                    <tt class="bodytxt">
                                                        </tt>
                                                    </td>
                                                <td style="text-align: right; width: 20%; height: 24px;">
                                                    <tt class="bodytxt">Year :</tt>
                                                </td>
                                                <td style="width: 30%; height: 24px;">--%>

                                                    <%--        <asp:DropDownList AutoPostBack="true" ID="cmbYear" runat="server" Style="width: 65px"
                                    CssClass="textfields" OnSelectedIndexChanged="cmbYear_SelectedIndexChanged">
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

                                                <%--</td>
                                            </tr>

                                            <tr>
                                                <td style="width: 20%; height: 24px; text-align: right">
                                                    <tt class="bodytxt">Select File :</tt>
                                                </td>
                                                <td colspan="3" style="height: 24px">
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="height: 24px; text-align: center">
                                                    </td>
                                            </tr>





                                            <tr>
                                                <td align="center" colspan="4" style="height: 26px" valign="middle">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="middle" colspan="4" style="height: 26px">
                                                    <tt class="bodytxt">
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                </tr>
                            </table>--%>




                            <radG:RadGrid ID="RadGrid1" CssClass="radGrid-single" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        GridLines="Horizontal" PageSize="20" Skin="Outlook" Width="100%" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                        OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand">
                                                        <MasterTableView CssClass="grid" DataKeyNames="TranID">
                                                            <FilterItemStyle HorizontalAlign="left" />
                                                            <HeaderStyle ForeColor="Navy" />
                                                            <ItemStyle BackColor="White" Height="20px" />
                                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                            <Columns>
                                                                <radG:GridBoundColumn DataField="status" HeaderText="Staus" UniqueName="FileName"
                                                                    DataType="System.Double" Visible="false">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="TranID" HeaderText="Doc No" UniqueName="TranID">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="FileName" HeaderText="Filename" UniqueName="FileName">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="OriFileName" HeaderText="Original Filename" UniqueName="OriFileName">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridBoundColumn DataField="FileType" HeaderText="FileType" UniqueName="FileType">
                                                                </radG:GridBoundColumn>
                                                                <radG:GridButtonColumn ButtonType="LinkButton" CommandName="Process" ConfirmText="Process This Doc?"
                                                                    Text="" UniqueName="Process">
                                                                    <HeaderStyle Width="35px" HorizontalAlign="Center"/>
                                            <ItemStyle HorizontalAlign="Center"/>
                                                                </radG:GridButtonColumn>
                                                                <radG:GridButtonColumn ButtonType="ImageButton" CommandName="Delete" ConfirmText="Delete File Uploaded"
                                                                    ImageUrl="~/frames/images/toolbar/Delete.gif" Text="Delete" UniqueName="DeleteColumn">
                                                                    <HeaderStyle Width="35px" HorizontalAlign="Center"/>
                                            <ItemStyle HorizontalAlign="Center"/>
                                                                </radG:GridButtonColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                        <ClientSettings AllowColumnsReorder="True" AllowExpandCollapse="True">
                                                        </ClientSettings>
                                                    </radG:RadGrid>
                           
                            <div class="text-center padding-tb-10">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Maroon"></asp:Label>
                            </div>

                            <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="False" />

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
