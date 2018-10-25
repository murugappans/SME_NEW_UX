<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectWise_WorkersReport.aspx.cs" Inherits="SMEPayroll.Reports.ProjectWise_WorkersReport" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">    
    <title>SMEPayroll</title>    
        
    
     <style type="text/css">       
       .labelOne 
        { 
            background-color:#FFFFFF; 
            filter:progid:DXImageTransform.Microsoft.Gradient(GradientType=0,StartColorStr='#363636',EndColorStr='#FFFFFF');
	        margin: 0px auto;
        } 
    </style>
    <style type="text/css">
            .hiddencol
            {
                display:none;
            }
            .viscol
            {
                display:block;
            }
    </style>


    
    <style type="text/css"> 
    
    .SelectedRow
    { 
        background: #ffffff !important; /*white */ 
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    }
    
    .SelectedRowLock
    { 
        background: #dcdcdc !important; /*Lock Row */         
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    }
     
    .SelectedRowExceptionForOuttime
    { 
        background: #996633 !important; /*Maroon*/
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 
    
    .SelectedRowExceptionFlexibleInOutTimeCompareProject
    { 
        background: #99FFCC !important;   /*Green */
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 

    
    .SelectedRowExceptionForIntimeWithEarylyInByTime
    { 
        background: #FFFFCC !important; /*Yellow */
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 
    
    .SelectedRowException
    { 
        background: #CCFF33 !important; /*purple*/
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 
    
    .SelectedRowExceptionForInorOutBlank
    { 
        background: #800000  !important; /*Red */
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 
    
    .NormalRecordChk
    { 
        background: #E5E5E5  !important; /*Red */
        height: 22px; 
        border: solid 1px #e5e5e5; 
        border-top: solid 1px #e9e9e9; 
        border-bottom: solid 1px white; 
        padding-left: 4px; 
    } 
    
    html, body, form   
    {   
       /*height: 100%;   */
       height: 100%;
       margin: 0px;   
       padding: 0px;  
       overflow: auto;
    }   


</style>
      


   

</head>
<body style="margin-left:auto;">
    <form id="form1" runat="server"  method="post">
     <telerik:RadScriptManager ID="RadScriptManager1" Runat="server" >
    </telerik:RadScriptManager> 
    
        
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="1500"  runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
            <asp:Image ID="Image1"  runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    
    
    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
        
            <%-- <telerik:AjaxSetting AjaxControlID="RadGrid2">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            <telerik:AjaxUpdatedControl ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1"/>
                            
                        </UpdatedControls>
            </telerik:AjaxSetting>      --%>  
        
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            
<%--              <telerik:AjaxSetting AjaxControlID="btnSubApprove">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                     
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
            
            <telerik:AjaxSetting AjaxControlID="btnApprove">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnDelete">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="btnUnlock">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
           <%-- <telerik:AjaxSetting AjaxControlID="btnCopy">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            --%>
            
            <telerik:AjaxSetting AjaxControlID="imgbtnfetchEmpPrj">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>

         <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
    
   </telerik:RadCodeBlock>
   
        
       
       
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td style="width: 100%">
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" >
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Project Wise Workers Report</b></font>
                            </td>
                        </tr>                       
                    </table>
                </td>
            </tr>            
        </table><br />
        
        <table  border="0"  cellpadding="0" cellspacing="0" class="bodytxt" >                
                  
                                          <tr>
                                          <td style ="width:70%">
                                                                                          
                                                    
                                                       Project :&nbsp;&nbsp;<radG:RadComboBox   ID="RadComboProject" runat="server" EmptyMessage="Select a Project" 
                                                      HighlightTemplatedItems="true" EnableLoadOnDemand="true"  Height="200px"  DropDownWidth="200px"    BorderWidth="0px" >
                                                    
                                                    </radG:RadComboBox>
                                                   
                                                    
                                                      &nbsp;&nbsp;&nbsp;Year :<asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields"  
                                                           runat="server"  AutoPostBack="true" >
                                                           <asp:ListItem Value="2011">2011</asp:ListItem>
                                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                                    <asp:ListItem Value="2014">2014</asp:ListItem>
                                                    <asp:ListItem Value="2015">2015</asp:ListItem>
                                                    <asp:ListItem Value="2016">2016</asp:ListItem>
                                                    <asp:ListItem Value="2017">2017</asp:ListItem>
                                                   
                                                   </asp:DropDownList>
                                                   
                                                
                                                &nbsp;&nbsp; Month :
                                                <asp:DropDownList ID="cmbMonth" runat="server" Style="width: 80px" CssClass="textfields">
                                                    <asp:ListItem Value="1">January</asp:ListItem>
                                                    <asp:ListItem Value="2">February</asp:ListItem>
                                                    <asp:ListItem Value="3">March</asp:ListItem>
                                                    <asp:ListItem Value="4">April</asp:ListItem>
                                                    <asp:ListItem Value="5">May</asp:ListItem>
                                                    <asp:ListItem Value="6">June</asp:ListItem>
                                                    <asp:ListItem Value="7">July</asp:ListItem>
                                                    <asp:ListItem Value="8">August</asp:ListItem>
                                                    <asp:ListItem Value="9">September</asp:ListItem>
                                                    <asp:ListItem Value="10">October</asp:ListItem>
                                                    <asp:ListItem Value="11">November</asp:ListItem>
                                                    <asp:ListItem Value="12">December</asp:ListItem>
                                                </asp:DropDownList>
                                               &nbsp;&nbsp;Period
                                                     <asp:DropDownList Width="100px"    CssClass="textfields"
                                                        ID="periodID"  runat="server" AutoPostBack="true">
                                                        <asp:ListItem Value="-1">-Select-</asp:ListItem>
                                                    <asp:ListItem Value="1">Full Month</asp:ListItem>
                                                    <asp:ListItem Value="2">First Half Month</asp:ListItem>
                                                    <asp:ListItem Value="3">Second Half</asp:ListItem>
                                                    </asp:DropDownList>&nbsp;&nbsp;
                                              <asp:CheckBox ID="chk_company" runat="server" />Include All Companies
                                                &nbsp;<tt><asp:ImageButton ID="imgbtnfetch"   OnClick="bindgrid"
                                                    runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                                </tt>
                                            </td>
                                                                                                                                                                       
                                                  <td style="width:600px; text-align :right ;">
                                                            <asp:Button ID="printbutton" runat="server" Text="Generate PDF" onclick="printbutton_Click" class="textfields" Enabled ="false" 
                                    style="width: 80px; height: 22px" />
                                                            
                                                        </td>                                                                   
                                         </tr>
                                       </table>
                 
                       
        <br />
        <table id="tbl1" runat="server" border="0" cellpadding="1" cellspacing="0">
             <tr >
             <td><asp:ImageButton ID="btnExportExcel" AlternateText="Export To Excel" OnClick="btnExportExcel_click"
                                    runat="server" ImageUrl="~/frames/images/Reports/exporttoexcel.jpg" /></td>
                <td align="center">
                    <tt class="bodytxt">
                        <asp:Label ID="lblMsg"  runat="server"  ForeColor="Maroon" Width="80%"></asp:Label></tt>
                </td>               
            </tr>            
            <tr>
                <td align="left" >
                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                        <script type="text/javascript">
                            function RowDblClick(sender, eventArgs)
                            {
                                //sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                                //alert('hi1');
                                  //eventArgs.set_cancel(true); 
                                  
                                              
                                    //                                    var grid = $find("<%=RadGrid2.ClientID %>");	                    
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
                    
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >                     
                             <ContentTemplate>
                                        
                    <radG:RadGrid ID="RadGrid2"  AllowPaging="true" PageSize="100" runat="server" 
                            AllowMultiRowSelection="true" Skin="Outlook" Width="99%" AutoGenerateColumns="False"
                            AllowFilteringByColumn="True" 
                            EnableHeaderContextMenu="true"   
                            Height="500px"
                            ItemStyle-Wrap="false"
                            AlternatingItemStyle-Wrap="false"
                            PagerStyle-AlwaysVisible="True" 
                            GridLines="Both" 
                            AllowSorting="true"  
                            
                            
                            Font-Size="11"
                            Font-Names="Tahoma" 
                            HeaderStyle-Wrap="false" >
                            <MasterTableView  TableLayout="Auto" PagerStyle-Mode="Advanced"  >
                                <FilterItemStyle HorizontalAlign="left" />
                                <HeaderStyle ForeColor="Navy"  Wrap="false" Height="25px" />
                                <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" VerticalAlign="middle" />
                                <ItemStyle Height="25px" VerticalAlign="middle" />
                                <Columns>
                                   
                                   <%-- <radG:GridBoundColumn DataField="row_id"  HeaderText="S.NO">
                                    </radG:GridBoundColumn>--%>
                                    <radG:GridBoundColumn DataField="Full_Name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains"
                                        HeaderText="Employee Name" SortExpression="FullName" ReadOnly="True" UniqueName="Full_Name" ShowFilterIcon="False"  ItemStyle-Wrap="false"  HeaderStyle-Wrap="false" HeaderStyle-Width="200px" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="ANBID" HeaderText="ANBID"    UniqueName="ANBID" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="TRADE" HeaderText="Trade"    UniqueName="TRADE" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn  DataField="NH" HeaderText="NH (1st HF)" 
                                        UniqueName="NH" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="OT1" HeaderText="OT1 (1st HF)" 
                                        UniqueName="OT1" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn  DataField="OT2" HeaderText="OT2 (1st HF)" 
                                        UniqueName="OT2" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="NH2" HeaderText="NH (2nd HF)" 
                                        UniqueName="NH2" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="OT12" HeaderText="OT1 (2nd HF)" 
                                        UniqueName="OT12" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                     <radG:GridBoundColumn  DataField="OT22" HeaderText="OT2 (2nd HF)" 
                                        UniqueName="OT22" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="TNH" HeaderText="Total NH" 
                                        UniqueName="TNH" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="TOT1" HeaderText="Total OT1" 
                                        UniqueName="TOT1" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="TOT2" HeaderText="Total OT2" 
                                        UniqueName="TOT2" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    <radG:GridBoundColumn  DataField="DAYS" HeaderText="DAYS" 
                                        UniqueName="DAYS" AllowFiltering="false">
                                    </radG:GridBoundColumn>
                                    
                                    
                                  </Columns>
                            </MasterTableView>
                              <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                            <Selecting AllowRowSelect="true" />
                                            <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="false"
                                                AllowColumnResize="True" ClipCellContentOnResize="true"   ></Resizing>
                                            <%--<Scrolling UseStaticHeaders="true" AllowScroll="true" ScrollHeight="500px" SaveScrollPosition="True"  />--%>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" ScrollHeight="500px" SaveScrollPosition="True" />
                              </ClientSettings>             
                           <ExportSettings>
                                        <Pdf PageHeight="210mm" />
                            </ExportSettings>
                    </radG:RadGrid>
                                        
                    </ContentTemplate>   
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ValidationSummary ID="vldSum" runat="server" ShowMessageBox="True" ShowSummary="True" />
                </td>
            </tr>
             <tr>
                <td  width="0%" height="0%">
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </td>
                
            </tr>
        </table>        
        <center>
                <asp:SqlDataSource ID="SqlDataSource4" runat="server"  ></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"  ></asp:SqlDataSource>
                <asp:SqlDataSource ID="SqlDataSource6" runat="server"></asp:SqlDataSource>
        </center>
    </form>
</body>
</html>
