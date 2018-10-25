<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProgressionReason.aspx.cs" Inherits="SMEPayroll.Management.ProgressionReason" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register TagPrefix="uc2" TagName="TopLeftControl" Src="~/Frames/TopLeftMenu.ascx" %>
<%@ Register TagPrefix="uc5" TagName="QuickSideBartControl" Src="~/Frames/QuickSidebar.ascx" %>

<%@ Register TagPrefix="uc_css" TagName="bundle_css" Src="~/Frames/bundle_css.ascx" %>
<%@ Register TagPrefix="uc_js" TagName="bundle_js" Src="~/Frames/bundle_js.ascx" %>
<%@ Register TagPrefix="uc3" TagName="GridToolBar" Src="~/Frames/GridToolBar.ascx" %>
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
                        <li>Progression Reason</li>
                        <li>
                            <a href="../Main/home.aspx"><i class="icon-home"></i></a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span class="adminDashboard">Admin</span>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <a href="ShowDropdowns.aspx">Manage Settings</a>
                            <i class="fa fa-circle"></i>
                        </li>
                        <li>
                            <span>Progression Reason</span>
                        </li>
                    </ul>

                </div>

                <!-- END PAGE BAR -->
                <!-- BEGIN PAGE TITLE-->
                <%--<h3 class="page-title">PROGRESSION REASON</h3>--%>
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
                                    <input id="Button2" onclick="history.go(-1)" value="Back" class="textfields btn btn-sm red no-margin" type="button" />
                                </div>
                            </div>--%>




                            <radG:RadCodeBlock ID="RadCodeBlock2" runat="server">

                                <script type="text/javascript">
                                    function getOuterHTML(obj) {
                                        if (typeof (obj.outerHTML) == "undefined") {
                                            var divWrapper = document.createElement("div");
                                            var copyOb = obj.cloneNode(true);
                                            divWrapper.appendChild(copyOb);
                                            return divWrapper.innerHTML
                                        }
                                        else
                                            return obj.outerHTML;
                                    }

                                    function PrintRadGrid(sender, args) {
                                        if (args.get_item().get_text() == 'Print') {

                                            var previewWnd = window.open('about:blank', '', '', false);
                                            var sh = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",RadGrid1.Skin)) %>';
                                                    var shBase = '<%= ClientScript.GetWebResourceUrl(RadGrid1.GetType(),"Telerik.Web.UI.Skins.Grid.css") %>';
                                                    var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link>";
                                                    styleStr += "<link href = '" + shBase + "' rel='stylesheet' type='text/css'></link></head>";
                                                    var htmlcontent = styleStr + "<body>" + getOuterHTML($find('<%= RadGrid1.ClientID %>').get_element()) + "</body></html>";
                       previewWnd.document.open();
                       previewWnd.document.write(htmlcontent);
                       previewWnd.document.close();
                       previewWnd.print();
                       previewWnd.close();
                   }
               }

                                </script>

                                <script type="text/javascript">
                                    window.onload = Resize;
                                    function Resize() {
                                        if (screen.height > 768) {
                                            //alert("1");
                                            //"90.7%";
                                            document.getElementById('<%= RadGrid1.ClientID %>').style.height = "86%";
                                                }
                                                else {
                                                    //document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
                                                    document.getElementById('<%= RadGrid1.ClientID %>').style.height = "79%";
                                                }
                                            }

                                </script>

                            </radG:RadCodeBlock>

                            <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">
                                <script language="javascript" type="text/javascript" src="../Frames/Script/CommonValidations.js">
           
                                </script>
                                <script type="text/javascript">
                                    var agent_name = "";
                                    var phone1 = "";
                                    var phone2 = "";
                                    var changedFlage = "false";

                                    function RowDblClick(sender, eventArgs) {
                                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                    }

                                    function ViewCreated() {
                                        //alert('view created');
                                    }

                                    //Check Validations for grid like Mandatory and 
                                    function Validations(sender, args) {



                                        //                                                                        if (typeof (args) !== "undefined")
                                        //                                    		 	                        {
                                        //                                    		        	                    var commandName = args.get_commandName();
                                        //                                    	        		                    var commandArgument = args.get_commandArgument();	        		    
                                        //                                    			                            switch (commandName) 
                                        //                                    			                            {
                                        //                                    				                            case "startRunningCommand":
                                        //                                    				                                $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                        //                                    			        	                        break;
                                        //                                    				                            case "alertCommand":
                                        //                                    				                                $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                        //                                    				                                break;
                                        //                                    				                            default:
                                        //                                    				                                $sendCommand(null, "alertCommand", new Date().toLocaleTimeString(), null);
                                        //                                    		        	                        break;
                                        //                                    		                                }
                                        //                                    		                            }


                                        var result = args.get_commandName();
                                        //                                    //alert(agent_name +"," + phone1 + "," + phone2);

                                        if (agent_name == "" && changedFlage == "false") {
                                            var itemIndex = args.get_commandArgument();
                                            //alert(itemIndex);
                                            var row = sender.get_masterTableView().get_dataItems()[itemIndex]; //to access the row                                

                                            if (row != null) {
                                                cellvalue = row._element.cells[1].innerHTML; // to access the cell value                                    
                                                agent_name = cellvalue;
                                            }
                                        }

                                        if (result == 'Update' || result == 'PerformInsert') {
                                            var sMsg = "";
                                            var message = "";
                                            message = MandatoryData(trim(agent_name), "Progression History Reason");
                                            if (message != "")
                                                sMsg += message + "\n";

                                            //                                        if(phone1!='undefined')    
                                            //                                        {
                                            //                                            message=CheckNumeric(phone1," Phone1");
                                            //                                            if(message!="")
                                            //                                                sMsg+=message+"\n";
                                            //                                        }
                                            //                                        
                                            //                                        if(phone2!='undefined')    
                                            //                                        {    
                                            //                                            message=CheckNumeric(phone2," Phone2");
                                            //                                            if(message!="")
                                            //                                                sMsg+=message+"\n";
                                            //                                        }
                                            //                                            
                                            if (sMsg != "") {
                                                //args.set_cancel(true);
                                                //alert(sMsg);
                                            }
                                        }
                                    }

                                    function OnFocusLost_AGentName(val) {
                                        var Object = document.getElementById(val);
                                        //alert(Object.innerHTML);
                                        agent_name = GetDataFromHtml(Object);
                                        //alert(agent_name);
                                        changedFlage = "true";
                                    }

                                    function OnFocusLost_Phone1(val) {
                                        //                                    var Object = document.getElementById(val);
                                        //                                    //alert(Object.innerHTML);
                                        //                                    phone1= GetDataFromHtml(Object);
                                    }

                                    function OnFocusLost_Phone2(val) {
                                        //                                    var Object = document.getElementById(val);
                                        //                                    //alert(Object.innerHTML);
                                        //                                    phone2= GetDataFromHtml(Object);
                                    }
                                </script>

                            </radG:RadCodeBlock>





                            <uc3:GridToolBar ID="GridToolBar" runat="server" Width="100%" />
                            <radG:RadGrid ID="RadGrid1"  runat="server"
                                AllowFilteringByColumn="true" AllowSorting="true" OnItemDataBound="RadGrid1_ItemDataBound" OnGridExporting="RadGrid1_GridExporting"
                                DataSourceID="SqlDataSource1" GridLines="Both" Skin="Outlook" Width="100%"
                                OnItemInserted="RadGrid1_ItemInserted" OnItemUpdated="RadGrid1_ItemUpdated"
                                OnDeleteCommand="RadGrid1_DeleteCommand">
                                <MasterTableView CommandItemDisplay="Bottom" AllowAutomaticUpdates="True" DataSourceID="SqlDataSource1"
                                    AllowAutomaticDeletes="True" AutoGenerateColumns="False" AllowAutomaticInserts="True"
                                    DataKeyNames="Id">
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

                                        <radG:GridBoundColumn
                                            Display="false"
                                            ReadOnly="True"
                                            DataField="Id"
                                            UniqueName="Id"
                                            SortExpression="Id"
                                            HeaderText="Id">
                                            <ItemStyle Width="100px" />
                                        </radG:GridBoundColumn>

                                        <radG:GridBoundColumn EditFormColumnIndex="0" DataField="HistoryDetails" UniqueName="HistoryDetails" ShowFilterIcon="false"
                                            HeaderText="Progression History" SortExpression="HistoryDetails" AllowFiltering="true" AutoPostBackOnFilter="true" FilterControlAltText="cleanstring"
                                            CurrentFilterFunction="Contains">
                                            <%--<ItemStyle Width="500px" HorizontalAlign="Left" />--%>
                                        </radG:GridBoundColumn>

                                        <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridEditCommandColumn>

                                        <radG:GridButtonColumn  ConfirmDialogType="RadWindow"
                                            ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                            UniqueName="DeleteColumn">
                                            <ItemStyle Width="30px" HorizontalAlign="Center" CssClass="MyImageButton clsCnfrmButton" />
                                            <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                        </radG:GridButtonColumn>
                                    </Columns>



                                    <EditFormSettings EditFormType="Template">
                                        <FormTemplate>
                                            <div class="clearfix form-style-inner">
                                                <div class="heading">
                                   <%--                 <span class="form-title">Add Progression History</span>--%>
                                                    <asp:label ID="Progression" CssClass="form-title" Text='<%# (Container is GridEditFormInsertItem) ? "Add Progression History" : "Edit Progression History" %>'
                                                                runat="server"></asp:label>
                                                </div>
                                                
                                                    <hr />
                                                
                                                
                                                    <div class="form-inline">
                                                        <div class="form-body">
                                                            <div class="form-group clearfix">
                                                                    <label>Progression History</label>
                                                                    <asp:TextBox ID="TextBox1" CssClass="form-control input-sm inline input-medium cleanstring custom-maxlength _txtproghis" MaxLength="50" runat="server" Text='<%# Bind("HistoryDetails") %>'></asp:TextBox>
                                                            </div>
                                                            <div class="form-group clearfix">
                                                            <asp:Button ID="btnUpdate" CssClass="btn red margin-top-0 insertproghis" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
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
                                    <CommandItemSettings AddNewRecordText="Add Progression History" />
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                        <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                            AllowColumnResize="True" ClipCellContentOnResize="true"></Resizing>
                                        <Selecting AllowRowSelect="true" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                                   <%-- <ClientEvents OnRowDblClick="RowDblClick" OnCommand="Validations" />--%>
                                     <ClientEvents OnRowDblClick="RowDblClick" />
                                </ClientSettings>
                            </radG:RadGrid>





                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" InsertCommand="INSERT INTO [EmpHistoryRecordMaster]([HistoryDetails],[Company_id])VALUES(@HistoryDetails,@company_id)"
                                SelectCommand="Select Id,HistoryDetails,Company_id from EmpHistoryRecordMaster where Company_id= @company_id"
                                UpdateCommand="UPDATE EmpHistoryRecordMaster  SET HistoryDetails=@HistoryDetails Where Id=@Id">
                                <SelectParameters>
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </SelectParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="HistoryDetails" Type="String" />
                                    <asp:Parameter Name="Id" Type="Int32" />
                                    <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                </UpdateParameters>
                                <InsertParameters>
                                    <asp:Parameter Name="Code" Type="String" />
                                    <asp:Parameter Name="HistoryDetails" Type="String" />
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
        $('.insertproghis').click(function () {
            return validateproghis();
        });
        $(".clsCnfrmButton").click(function () {
            var _elem = $(this).find('input[type=image]');
            var _id = _elem.attr('id');
            GetConfirmation("Are you sure you want to delete this Progression Reason?", _id, "Confirm Delete", "Delete");
        });
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>');
            var _inputs = $('#RadGrid1_ctl00_Header thead tr td').find('input[type=text]');
            $.each(_inputs, function (index, val) {
                $(this).addClass($(this).attr('alt'));

            })
        }
        function validateproghis() {
            var _message = "";
            if ($.trim($("._txtproghis").val()) === "")
                _message += "Please Input Progression History <br>";
            if (_message != "") {
                WarningNotification(_message);
                return false;
            }
            return true;
        }
    </script>

</body>
</html>
