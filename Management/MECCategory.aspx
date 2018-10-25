<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MECCategory.aspx.cs" Inherits="SMEPayroll.Management.MECCategory" %>


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

    <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
    </script>

    <script type="text/javascript">
        var Trade = "";
        var changedFlage = "false";

        //Check Validations for grid like Mandatory and 
        function Validations(sender, args) {
            /*      if (typeof (args) !== "undefined")
                 {
                     var commandName = args.get_commandName();
                     var commandArgument = args.get_commandArgument();	        		    
                     switch (commandName) 
                     {
                         case "startRunningCommand":
                             $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                             break;
                         case "alertCommand":
                             $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                             break;
                         default:
                             $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                         break;
                     }
                 }
                 
                 var result = args.get_commandName();                                    
                 if(Trade=="" && changedFlage=="false")
                 {
                     var itemIndex = args.get_commandArgument();                            
                     var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                
                     if(row!=null)
                     {
                         cellvalue = row._element.cells[2].innerHTML; // to access the cell value                                    
                         Trade=cellvalue;
                     }
                 }                        
                 if (result == 'Update' ||result == 'PerformInsert')
                 {
                     var sMsg="";
                     var message ="";                                    
                     message=MandatoryData(trim(Trade),"Trade Name");
                     if(message!="")
                         sMsg+=message+"\n";
                         
                     if(sMsg!="")
                     {
                         args.set_cancel(true);
                         alert(sMsg);
                     }
                 }
                 
                 
           */
        }

        function OnFocusLost_Trade(val) {
            var Object = document.getElementById(val);
            Trade = GetDataFromHtml(Object);
            changedFlage = "true";
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
                        <li>Manage Manual Employer Contribution Category</li>
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
                            <a href="ManualEmployerContributionIndex.aspx">Manual Employer Contribution</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Add Category</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Manual Employeer Contribution Category</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
                            <radG:RadScriptManager ID="RadScriptManager1" runat="server">
                            </radG:RadScriptManager>

                            <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

                            <%--<div class="search-box clearfix">

                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red" type="button">
                                </div>
                            </div>--%>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                                <script type="text/javascript">
                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }
                                </script>

                            </radG:RadCodeBlock>



                            <radG:RadGrid ID="RadGrid1" OnDeleteCommand="RadGrid1_DeleteCommand" OnItemDataBound="RadGrid1_ItemDataBound" AllowSorting="true"
                                runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook"
                                Width="93%" OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand"  >
                                <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="APCatId"
                                    AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                    CommandItemDisplay="Bottom">
                                    <FilterItemStyle HorizontalAlign="left" />
                                    <HeaderStyle ForeColor="Navy" />
                                    <ItemStyle BackColor="White" Height="20px" />
                                    <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                    <Columns>

                                        <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" Display="false">
                                            <ItemTemplate>
                                                <%--<asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="35px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="35px" HorizontalAlign="Center" />
                                        </radG:GridTemplateColumn>

                                        <radG:GridBoundColumn ReadOnly="True" DataField="APCatId" DataType="System.Int32" UniqueName="APCatId"
                                            Visible="false" SortExpression="APCatId" HeaderText="APCatId">
                                            <%--<ItemStyle Width="100px" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="ApCategory" UniqueName="ApCategory" SortExpression="ApCategory"
                                            HeaderText="Category">
                                            <%--<ItemStyle Width="93%" />--%>
                                        </radG:GridBoundColumn>
                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>
                                        <radG:GridButtonColumn ButtonType="ImageButton"
                                            ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" CssClass="ConfirmDelete" HorizontalAlign="Center" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>

                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="heading">
                                                    <span class="form-title">Add New Category</span>
                                                </div>
                                                
                                                    <hr />
                                                
                                               
                                                    <div class="form-inline">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">
                                                                <label>Category</label>
                                                                <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium  cleanstring custom-maxlength _txtcategory" MaxLength="50" runat="server" Text='<%# Bind("ApCategory") %>'></asp:TextBox>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0 insertcategory" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>
                                                            <asp:Button ID="btnCancel" CssClass="btn default margin-top-0" Text="Cancel" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                                </div>
                                                        </div>
                                                       
                                                    </div>                                                
                                                
                                            </div>
                                        </FormTemplate>
                                    </EditFormSettings>

                                    <ExpandCollapseColumn Visible="False">
                                        <HeaderStyle Width="19px"></HeaderStyle>
                                    </ExpandCollapseColumn>
                                    <RowIndicatorColumn Visible="False">
                                        <HeaderStyle Width="20px"></HeaderStyle>
                                    </RowIndicatorColumn>
                                    <CommandItemSettings AddNewRecordText="Add New Category" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                    <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                        AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                                    <ClientEvents OnRowDblClick="RowDblClick" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                </ClientSettings>
                            </radG:RadGrid>



                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"  SelectCommand="SELECT [APCatId],[ApCategory]  FROM [APCategory] where companyid=@company_id ">
<%--                                InsertCommand="INSERT INTO [APCategory] ([ApCategory],companyid) VALUES (@ApCategory,@company_id)"
                               
                                UpdateCommand="UPDATE [APCategory] SET [ApCategory] = @ApCategory WHERE [APCatId] = @APCatId">--%>
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="ApCategory" Type="String" />
                                    <asp:Parameter Name="id" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="ApCategory" Type="String" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </InsertParameters>
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
        $("input[type='button']").removeAttr("style");
        window.onload = function () {
           CallNotification('<%=ViewState["actionMessage"].ToString() %>');      
          }
        //function validateCat() {

        //    if ($("#RadGrid1_ctl00_ctl05_TextBox1, #RadGrid1_ctl00_ctl02_ctl02_TextBox1").val() == "")
        //    {
        //        WarningNotification("Category Name cannot be empty.");
        //        return false;
        //    }

        //}
        $('.insertcategory').click(function () {
            var _message = "";
            if ($.trim($("._txtcategory").val()) === "")
                _message += "Category Name cannot be empty.";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
        });
        //$(document).on("click", ".ConfirmDelete", function (e) {
            // e.preventDefault(); 
            $(".ConfirmDelete").click(function () {
            var _elem = $(this).find('input[type=image]');
            //var _dynamicmsg = $(_elem).closest('table').find('thead tr th:first').prop('abbr');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this category ?", _id, "Confirm Delete", "Delete");
        });

    </script>

</body>
</html>
