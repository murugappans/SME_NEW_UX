<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Region.aspx.cs" Inherits="SMEPayroll.Cost.Region" %>

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



   <script language="javascript"  type="text/javascript"  src="../Frames/Script/CommonValidations.js" >
        </script>
        
        <script type="text/javascript">
            var Trade="";
            var changedFlage="false"; 
            
           //Check Validations for grid like Mandatory and 
           function Validations(sender, args) 
           {
                     if (typeof (args) !== "undefined")
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
                        message=MandatoryData(trim(Trade),"Region");
                        if(message!="")
                            sMsg+=message+"\n";
                            
                        if(sMsg!="")
                        {
                            args.set_cancel(true);
                            alert(sMsg);
                        }
                    } 
            }
            
            function OnFocusLost_BusinessUnit(val)
            {
                var Object = document.getElementById(val);                                
                Trade =GetDataFromHtml(Object);
                changedFlage="true";
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
                        <li>Manage Region</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                           <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                               <%if (Request.QueryString["page"] == "costing")
                                    {%>
                           <li>
                            <a href="Cost.aspx"><span>Costing Managements</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                                <%}%>
                               <% else
                                {%>
                                 <li>
                            <a href="../Management/ShowDropdowns.aspx">Manage Settings</a>
                            <i class="fa fa-circle"></i>
                        </li>
                                <%}%>

                        <%if (Request.QueryString["page"] == "costing")
                                    {%>
                              <li>
                            <a href="CostingByRegionIndex.aspx"><span>Costing By Region</span></a>
                            <i class="fa fa-circle"></i>
                        </li>
                                <%}%>
                               <% else
                                {%>

                                <%}%>

               
                        <li>
                            <span>Region</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">Manage Region</h3>--%>
                <!-- END PAGE TITLE-->
                <!-- END PAGE HEADER-->


                <div class="row">
                    <div class="col-md-12">

                        <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
       
        <%--<uc1:TopRightControl ID="TopRightControl1" runat="server" />--%>

        <%--<div class="search-box clearfix padding-tb-10">
                                <div class="col-md-12 text-right">
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button">
                                </div>
                            </div>--%>

        <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>

     

                        <radG:RadGrid ID="RadGrid1"  OnDeleteCommand="RadGrid1_DeleteCommand" OnItemDataBound="RadGrid1_ItemDataBound" AllowSorting="true"
                            runat="server" PageSize="20" AllowPaging="true" DataSourceID="SqlDataSource1" GridLines="None" Skin="Outlook"
                            Width="93%" OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated">
                            <MasterTableView DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Bid"
                                AllowAutomaticDeletes="True" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
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

                                    <radG:GridBoundColumn ReadOnly="True" DataField="Bid" DataType="System.Int32" UniqueName="Bid"
                                        Visible="false" SortExpression="Bid" HeaderText="Bid">
                                        <%--<ItemStyle Width="100px" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn DataField="BusinessUnit" UniqueName="BusinessUnit" SortExpression="BusinessUnit"
                                        HeaderText="Region">
                                        <%--<ItemStyle Width="93%" />--%>
                                    </radG:GridBoundColumn>
                                    <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    </radG:GridEditCommandColumn>
                                    <radG:GridButtonColumn  ButtonType="ImageButton"
                                        ImageUrl="../frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                        UniqueName="DeleteColumn">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="clsCnfrmButton" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                    </radG:GridButtonColumn>
                                </Columns>

                                <EditFormSettings EditFormType="Template">
                                                             <FormTemplate>
                                                <div class="clearfix form-style-inner">
                                                    <div class="heading">
                                                        <%--<span class="form-title">Add New Region</span>--%>
                                                        <asp:label ID="Region" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Region" : "Edit Region" %>'
                                                                runat="server"></asp:label>
                                                    </div>
                                                    
                                                        <hr />
                                                    
                                                    
                                                        <div class="form-inline">
                                                            <div class="form-body">
                                                                <div class="form-group clearfix">
                                                                        <label>Region</label>
                                                                        <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtregion" MaxLength="50" runat="server" Text='<%# Bind("BusinessUnit") %>'></asp:TextBox>
                                                                </div>
                                                            
                                                            <div class="form-group clearfix">
                                                                <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0 insertregion" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
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
                                <CommandItemSettings AddNewRecordText="Add New Region" />
                            </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                <%--<ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                <ClientEvents OnRowDblClick="RowDblClick" />
                            </ClientSettings>
                        </radG:RadGrid>

                   

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [Cost_Region] ([BusinessUnit],Company_id) VALUES (@BusinessUnit,@company_id)"
                SelectCommand="SELECT [Bid],[BusinessUnit] FROM [Cost_Region] where company_id=@company_id order by 1"
                UpdateCommand="UPDATE [Cost_Region] SET [BusinessUnit] = @BusinessUnit WHERE [Bid] = @Bid">
                <SelectParameters>
                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="BusinessUnit" Type="String" />
                    <asp:Parameter Name="Bid" Type="Int32" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="BusinessUnit" Type="String" />
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
        $('.insertregion').click(function () {
            return validateregion();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Region?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');

        }
        function validateregion() {
            var _message = "";
            if ($.trim($("._txtregion").val()) === "")
                _message += "Please Input Region <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
