<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ClaimsExt.aspx.cs" EnableEventValidation="true"
    Inherits="SMEPayroll.Payroll.ClaimsExt" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />

 

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="100"  runat="server" Transparency="10" BackColor="#E0E0E0"   InitialDelayTime="1000">
            <asp:Image ID="Image1"  runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
        </telerik:RadAjaxLoadingPanel>
        
        <telerik:RadToolTipManager ID="RadToolTipManager1"   Animation="Fade" Width="150px" RenderInPageRoot="true" Height="90px" 
                    runat="server"  AutoTooltipify="true"    Skin="Outlook">
        </telerik:RadToolTipManager>
<telerik:RadCodeBlock  ID="RadCodeBlock4" runat="server">

                        <script type="text/javascript" language="javascript">  
                                <!-- 

                                if (document.all)
                                window.parent.defaultconf=window.parent.document.body.cols
                                function expando(){
                                window.parent.expandf()

                                }
                                document.ondblclick=expando 

                                -->

                                         function isNumericKeyStrokeDecimal(evt)
                                        {
                                             var charCode = (evt.which) ? evt.which : event.keyCode
                                             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                                                return false;

                                             return true;
                                        }
                                        
                                                                                  // on upload button click temporarily disables ajax to perform
//                                             // upload actions
//                                   function conditionalPostback(sender, args)
//                                    {
//                                                 if (args.get_eventTarget() == "<%= Button2.UniqueID %>") {
//                                                     args.set_enableAjax(false);
//                                                 }
//                                   }
                    </script>
   </telerik:RadCodeBlock>   



   
     
      <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" >
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="imgbtnfetch" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="drpCurrencyID" EventName="CurrencyIndexchange" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
             <telerik:AjaxSetting AjaxControlID="radSelect" EventName="FillDropdown" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="drpRejected" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
                <telerik:AjaxSetting AjaxControlID="drpemp" EventName="FillDropdown" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="drpRejected" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            <telerik:AjaxSetting AjaxControlID="DropDownList1" EventName="FillDropdown" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="drpRejected" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
              <telerik:AjaxSetting AjaxControlID="cmbYear" EventName="FillDropdown" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="drpRejected" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="imgbtnfetch" EventName="bindgrid" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="errorMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
          <%--  <telerik:AjaxSetting AjaxControlID="Button2" EventName="Button2_Click" >
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="drpRejected" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                     <telerik:AjaxUpdatedControl  ControlID="lblerror" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>

    
        
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="6">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Apply Claims</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td width="15%"  style="height: 25px">
                                <tt class="bodytxt">Employee:</tt>
                                <asp:DropDownList ID="drpemp" runat="server" Width="136px" AutoPostBack="true" OnSelectedIndexChanged="drpemp_SelectedIndexChanged" CssClass="textfields">
                                </asp:DropDownList>
                                <asp:DropDownList ID="DropDownList1" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="drpemp_SelectedIndexChanged" CssClass="textfields">
                                    <asp:ListItem Value="13">All</asp:ListItem>
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
                                <asp:DropDownList ID="cmbYear" OnSelectedIndexChanged="drpemp_SelectedIndexChanged"  AutoPostBack="true"   Style="width: 65px" CssClass="textfields" DataTextField="text" DataValueField="id" DataSourceID="xmldtYear" 
                                               runat="server"   >
                                       </asp:DropDownList>
                                       <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year" ></asp:XmlDataSource>
                                
                            </td>
                            <td width="5%">
                                <asp:RadioButtonList  BorderStyle="Dashed"  AutoPostBack="true"
                                        RepeatDirection="Horizontal" CssClass="textfields" ID="radSelect"
                                        OnSelectedIndexChanged="radSelect_SelectedIndexChanged" 
                                         runat="server">
                                    <asp:ListItem Selected="True" Text="New" Value="N"></asp:ListItem>
                                    <asp:ListItem  Text="Rejected" Value="R"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="2%" >
                                <asp:Label Text="TransactionID" runat="server" ID="lblTrasid" class="bodytxt">
                                </asp:Label>
                            </td>
                            <td width="5%" >
                                <asp:DropDownList  CssClass="textfields" id="drpRejected" Enabled="false" runat="server" ></asp:DropDownList>
                            </td>
                            <td width="2%"  align="left">
                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td width="10%"  align="left" >
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                            <td width="10%"><asp:Label ID="errorMsg" ForeColor="red" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0"  width="100%"
            border="0">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >   
                                <ContentTemplate>
                                   <tr>
                                                 <td align="center">
                                                    <asp:Label class="bodytxt"  ID="lblerror" runat="server" ForeColor="red"></asp:Label>
                                                </td>
                                            </tr>
                                <tr>
                                    <td>
                      
                                              <radG:RadGrid  
                                                    ID="RadGrid1" 
                                                    runat="server" 
                                                    OnUpdateCommand="RadGrid1_UpdateCommand" 
                                                    OnDeleteCommand="RadGrid1_DeleteCommand"
                                                    OnInsertCommand="RadGrid1_InsertCommand" 
                                                    OnItemDataBound="RadGrid1_ItemDataBound" 
                                                    AllowFilteringByColumn="false" 
                                                    AllowSorting="true" 
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
                                                    AllowMultiRowSelection="false" 
                                                    GridLines="None"
                                                    Skin="Outlook" 
                                                    Width="95%" 
                                                    EnableHeaderContextMenu="true"
                                                    FooterStyle-Font-Bold="true"
                                                    FooterStyle-HorizontalAlign="Center"
                                                    FooterStyle-ForeColor="Firebrick"
                                                    
                                                >
                                    <MasterTableView AutoGenerateColumns="False"  >
                                        <FilterItemStyle HorizontalAlign="left" />
                                        <ItemStyle BackColor="White" Height="20px" />
                                        <AlternatingItemStyle   Height="20px" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" ShowRefreshButton="false" />

                                        <Columns>
                                             <radG:GridTemplateColumn HeaderText="Add"   HeaderButtonType="PushButton"     HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                                  <ItemTemplate >  
                                                      <asp:ImageButton id="btnAdd" runat="server"  CommandName="AddNew" ImageUrl="~/Frames/Images/STATUSBAR/Add-ss.png"  AlternateText="" />
                                                  </ItemTemplate>  
                                                  <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </radG:GridTemplateColumn>
                                             <radG:GridTemplateColumn HeaderText="Delete"   HeaderButtonType="PushButton"     HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">   
                                                  <ItemTemplate >  
                                                      <asp:ImageButton id="btnDelete" runat="server"  CommandName="Delete" ImageUrl="~/Frames/Images/STATUSBAR/delete-ss.png"  AlternateText="" />
                                                  </ItemTemplate>  
                                                  <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </radG:GridTemplateColumn>                                
                                            <radG:GridBoundColumn DataField="SrNo" Visible="false" HeaderText="SrNo"  
                                                UniqueName="SrNo">
                                                    <ItemStyle HorizontalAlign="Center" Width="1%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="id" HeaderText="id" SortExpression="id" 
                                                Visible="false" UniqueName="id">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="emp_name" HeaderText="emp_name" 
                                                   UniqueName="emp_name">
                                                    <ItemStyle HorizontalAlign="Center"   Width="9%" />
                                                    <HeaderStyle HorizontalAlign="Center" />    
                                            </radG:GridBoundColumn>
                                            
                                            
                                            <radG:GridBoundColumn DataField="Bid"  HeaderText="Bid" Visible="true" EmptyDataText="0"
                                                UniqueName="Bid">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridTemplateColumn HeaderText="Business Unit"   HeaderStyle-HorizontalAlign="Center"      UniqueName="Bid1" >
                                              <ItemTemplate >  
                                                  <asp:DropDownList ID="drpBid" AutoPostBack="true" runat="server" OnSelectedIndexChanged="CurrencyIndexchange" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >   
                                                  </asp:DropDownList>                                        
                                              </ItemTemplate>  
                                              <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            </radG:GridTemplateColumn>
                                            
                                            <radG:GridBoundColumn DataField="trx_period1"  HeaderText="trx_period1" Visible="false" DataType=system.string UniqueName="trx_period1">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridTemplateColumn HeaderText="Transaction Period"   HeaderStyle-HorizontalAlign="Center"  DataType=system.string     UniqueName="trx_period" >
                                              <ItemTemplate >  
                                                  <asp:DropDownList ID="drptrxperiod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CurrencyIndexchange" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >   
                                                  </asp:DropDownList>                                        
                                              </ItemTemplate>  
                                              <ItemStyle HorizontalAlign="Center" Width="5%" />
                                            </radG:GridTemplateColumn>
                                            
                                             <radG:GridBoundColumn DataField="Desc"  HeaderText="Desc" Visible="false"
                                                UniqueName="Desc">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridBoundColumn DataField="trx_type"  HeaderText="trx_type" Visible="false"
                                                UniqueName="trx_type">
                                            </radG:GridBoundColumn>
                                                                            
                                             <radG:GridTemplateColumn HeaderText="Claim Type"   HeaderStyle-HorizontalAlign="Center"      UniqueName="trx_type1" >
                                              <ItemTemplate >  
                                                  <asp:DropDownList ID="drptrx_type" AutoPostBack="true" OnSelectedIndexChanged="CurrencyIndexchange" runat="server" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >   
                                                  </asp:DropDownList>                                        
                                              </ItemTemplate>  
                                              <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            </radG:GridTemplateColumn>
                                            
                                              <radG:GridBoundColumn DataField="Desc"  HeaderText="Desc" Visible="false"
                                                UniqueName="Desc">
                                            </radG:GridBoundColumn>
                                            
                                             <radG:GridBoundColumn DataField="CurrencyID"  HeaderText="CurrencyID" Visible="true"
                                                UniqueName="CurrencyID">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridTemplateColumn HeaderText="Currency"   HeaderStyle-HorizontalAlign="Center"      UniqueName="CurrencyID1" >
                                              <ItemTemplate >  
                                                  <asp:DropDownList ID="drpCurrencyID"  AutoPostBack="true"   runat="server" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" OnSelectedIndexChanged="CurrencyIndexchange" >   
                                                  </asp:DropDownList>                                        
                                              </ItemTemplate>  
                                              <ItemStyle HorizontalAlign="Center" Width="2%" />
                                            </radG:GridTemplateColumn>
                                            
                                            <radG:GridBoundColumn DataField="GstFlag"  HeaderText="GstFlag" Visible="false" UniqueName="GstFlag">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridTemplateColumn HeaderText="Tax"   HeaderStyle-HorizontalAlign="Center"      UniqueName="GstFlag1" >
                                              <ItemTemplate >  
                                                  <asp:CheckBox ID="chkGst" runat="server"   AutoPostBack="true" OnCheckedChanged="CurrencyIndexchange"  CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid"  />                                                  
                                              </ItemTemplate>  
                                              <ItemStyle HorizontalAlign="Center" Width="1%" />
                                            </radG:GridTemplateColumn>
                                            
                                      
                                             <radG:GridBoundColumn DataField="GstCode"  HeaderText="GstCode" Visible="false" UniqueName="GstCode">
                                                
                                            </radG:GridBoundColumn>
                                                                            
                                            <radG:GridTemplateColumn     HeaderStyle-HorizontalAlign="Center" UniqueName="GstCode1" HeaderText="GstCode" >
                                                     <ItemTemplate>
                                                         <asp:DropDownList  ID="drpGstCode"  AutoPostBack="true" OnSelectedIndexChanged="CurrencyIndexchange" runat="server" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >                                                       
                                                            
                                                        </asp:DropDownList>                                           
                                                   </ItemTemplate> 
                                                 <ItemStyle  Width="1%" HorizontalAlign="center" />
                                            </radG:GridTemplateColumn> 
                                            
                                             <radG:GridBoundColumn DataField="GstAmnt"  HeaderText="GstAmnt" Visible="false" UniqueName="GstAmnt">
                                            </radG:GridBoundColumn>
                                            
                                           
                                            
                                            
                                             <radG:GridBoundColumn  DataField="BefGst"  HeaderText="BefGst" Visible="false" UniqueName="BefGst">
                                            </radG:GridBoundColumn>
                                            
                                           <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="Center" UniqueName="ToatlWithGst1" HeaderText="TOTAL AMOUNT"  >
                                                     <ItemTemplate>
                                                        <div>
                                                            <asp:TextBox  onkeypress="return isNumericKeyStrokeDecimal(event)" ID="txtAmtGst" AutoPostBack="true" OnTextChanged="CurrencyIndexchange" Width="38px" runat="server"  Enabled="true"/>
                                                        </div>                                            
                                                   </ItemTemplate> 
                                               <ItemStyle HorizontalAlign="Center"  Width="2%" />
                                            </radG:GridTemplateColumn> 
                                            
                                            
                                             <radG:GridTemplateColumn    HeaderStyle-HorizontalAlign="Center" UniqueName="GstAmnt1" HeaderText="GST" >
                                                     <ItemTemplate>
                                                        <div>
                                                            <asp:TextBox Text="111" ID="txtGstAmt" Enabled="true"  ReadOnly="true" onkeypress="javascript:return isNumericKeyStrokeDecimal(event)"  Width="38px" runat="server" AutoPostBack="true" OnTextChanged="CurrencyIndexchange"  />
                                                        </div>                                            
                                                   </ItemTemplate> 
                                                 <ItemStyle  HorizontalAlign="center" width="2%"  />
                                            </radG:GridTemplateColumn>
                                            
                                            <radG:GridBoundColumn DataField="ToatlWithGst"  HeaderText="ToatlWithGst" Visible="false" UniqueName="ToatlWithGst">
                                            </radG:GridBoundColumn>
                                            
                                            
                                             <radG:GridTemplateColumn   HeaderStyle-HorizontalAlign="Center" UniqueName="BefGst1" HeaderText="AMOUNT BEF GST" >
                                                     <ItemTemplate>
                                                        <div>
                                                            <asp:TextBox   ReadOnly="true" onkeypress="return isNumericKeyStrokeDecimal(event)" Text="0.00"  AutoPostBack="true" OnTextChanged="CurrencyIndexchange" ID="txtBefGst" Width="38px" runat="server"   />
                                                        </div>                                            
                                                   </ItemTemplate> 
                                                 <ItemStyle  HorizontalAlign="center" width="2%"  />
                                            </radG:GridTemplateColumn>
                                            <radG:GridBoundColumn DataField="ExRate" HeaderText="ExRate"  
                                                UniqueName="ExRate">
                                                 <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </radG:GridBoundColumn>
                                             
                                             <radG:GridBoundColumn DataField="PayAmount" EmptyDataText="0"  HeaderText="PayAmount" Visible="True" UniqueName="PayAmount">
                                                 <ItemStyle HorizontalAlign="Center"  Width="1%" />
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="CurrencyIDComp" HeaderText="CurrencyIDComp" Visible="false"
                                                UniqueName="CurrencyIDComp">
                                            </radG:GridBoundColumn>  
                                            
                                             <radG:GridBoundColumn DataField="recpath" HeaderText="recpath" Visible="false"
                                                UniqueName="recpath">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridBoundColumn DataField="Receipt" HeaderText="Receipt" Visible="false" UniqueName="Receipt">
                                            </radG:GridBoundColumn>

                                            <radG:GridBoundColumn DataField="CurrencyIDComp" HeaderText="CurrencyIDComp" Visible="false"
                                            UniqueName="CurrencyIDComp">
                                            </radG:GridBoundColumn>
                                            
                                             <radG:GridBoundColumn DataField="Recpyn"  EmptyDataText="0"      HeaderText="Recpyn" Visible="false" UniqueName="Recpyn">
                                                
                                            </radG:GridBoundColumn>
                                                                            
                                            <radG:GridTemplateColumn      HeaderStyle-HorizontalAlign="Center" UniqueName="Recpyn1" HeaderText="Receipt" >
                                                     <ItemTemplate>
                                                         <asp:DropDownList  ID="drpReceipt"  AutoPostBack="true" OnSelectedIndexChanged="CurrencyIndexchange" runat="server" CssClass="bodytxt" style="border-right: #cccccc 1px solid; table-layout: fixed;border-top: #cccccc 1px solid; border-left: #cccccc 1px solid;border-bottom: #cccccc 1px solid" >                                                       
                                                            <asp:ListItem Text="Y" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="N" Value="0"></asp:ListItem>
                                                        </asp:DropDownList>                                           
                                                   </ItemTemplate> 
                                                 <ItemStyle  Width="1%" HorizontalAlign="center" />
                                            </radG:GridTemplateColumn> 

                                            <radG:GridBoundColumn  DataField="Remarks" HeaderText="Description" Visible="false" UniqueName="Remarks">
                                            </radG:GridBoundColumn>
                                            
                                            <radG:GridTemplateColumn  HeaderStyle-HorizontalAlign="Center" UniqueName="Remarks1" HeaderText="Description">
                                                     <ItemTemplate>
                                                        <div>
                                                            <asp:TextBox  AutoPostBack="true" OnTextChanged="CurrencyIndexchange" Text="111" ID="txtRemarks" Width="100px" Height="30px" runat="server" />
                                                        </div>                                            
                                                   </ItemTemplate> 
                                                 <ItemStyle  HorizontalAlign="center"  Width="5%"  />
                                            </radG:GridTemplateColumn>
                                            
                                            <radG:GridBoundColumn DataField="cpf" HeaderText="cpf"  Visible="false"
                                                UniqueName="cpf">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="emp_code" HeaderText="emp_code"  Visible="false"
                                                UniqueName="emp_code">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Department" HeaderText="Department" Visible="false"
                                                UniqueName="Department">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Descriptions" HeaderText="Descriptions" Visible="false"
                                                UniqueName="Descriptions">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" Visible="false"
                                                UniqueName="Designation">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="emp_type" HeaderText="emp_type" Visible="false"
                                                UniqueName="emp_type">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="TimeCardId" HeaderText="TimeCardId" Visible="false"
                                                UniqueName="TimeCardId">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" Visible="false"
                                                UniqueName="Nationality">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="ConversionOpt" EmptyDataText="0" HeaderText="ConversionOpt" Visible="false"
                                                UniqueName="ConversionOpt">
                                            </radG:GridBoundColumn>
                                            <radG:GridBoundColumn DataField="GLAcc" EmptyDataText="" HeaderText="GLAcc" Visible="false"
                                                UniqueName="GLAcc">
                                            </radG:GridBoundColumn>    
                                            <radG:GridBoundColumn DataField="created_on" EmptyDataText="1/1/2012" HeaderText="created_on"  Visible="false"
                                                UniqueName="created_on">
                                            </radG:GridBoundColumn>    
                                            <radG:GridBoundColumn DataField="created_by" HeaderText="created_by"  Visible="false"
                                                UniqueName="created_by">
                                            </radG:GridBoundColumn>    
                                            <radG:GridBoundColumn DataField="modified_on" EmptyDataText="1/1/2012" HeaderText="modified_on"  Visible="false"
                                                UniqueName="modified_on">
                                            </radG:GridBoundColumn>    
                                            <radG:GridBoundColumn DataField="modified_by" HeaderText="modified_by"  Visible="false"
                                                UniqueName="modified_by">
                                            </radG:GridBoundColumn>                                    
                                            <radG:GridBoundColumn DataField="CompanyID" EmptyDataText="-1" HeaderText="CompanyID" Visible="false"
                                                UniqueName="CompanyID">
                                            </radG:GridBoundColumn>    
                                            <radG:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" EmptyDataText="-1"
                                                UniqueName="ID">
                                            </radG:GridBoundColumn>         
                                            <radG:GridBoundColumn DataField="claimstatus" HeaderText="claimstatus" Visible="false"
                                                UniqueName="claimstatus">
                                            </radG:GridBoundColumn>  
                                            <radG:GridBoundColumn DataField="TransId"  HeaderText="TransId"     Visible="false"
                                                UniqueName="TransId">
                                            </radG:GridBoundColumn>  
                                        </Columns>
                                        <ExpandCollapseColumn Visible="False">
                                            <HeaderStyle Width="19px" />
                                        </ExpandCollapseColumn>
                                        <RowIndicatorColumn Visible="False">
                                            <HeaderStyle Width="20px" />
                                        </RowIndicatorColumn>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" />
                                        <ClientEvents OnGridCreated="GridCreated" OnRowClick="RowClick" ></ClientEvents>
                                        <Selecting AllowRowSelect="false" /> 
                                    </ClientSettings>
                                </radG:RadGrid>
               
                </td>
            </tr>
                        <tr>
                        </tr>
                        
                          <tr>
                                <td   class="bodytxt">
                                    <tt class="bodytxt"> &nbsp; Receipt Upload:</tt>                    
                                    <radG:RadUpload ID="radClaimsUpload" InitialFileInputsCount="1" runat="server" ControlObjectsVisibility="ClearButtons"
                                        MaxFileInputsCount="1" OverwriteExistingFiles="True" />
                                </td>            
                         </tr>
                        <tr>
                                <td align="center">
                                <asp:Button ID="Button2" runat="server" Text="Submit Claim" class="textfields" Style="width: 130px;
                                    height: 23px" OnClick="Button2_Click" />
                                </td>
                        </tr>
            
                 </ContentTemplate>
                        <Triggers>
                          <asp:PostBackTrigger ControlID="Button2" />
                        </Triggers>
                 </asp:UpdatePanel>
        </table>
        <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="sp_emppayclaim_add_EXT"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="drpemp" Name="empcode" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="DropDownList1" Name="empmonth" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="cmbYear" Name="empyear" PropertyName="SelectedValue"
                    Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>--%>
        <asp:SqlDataSource ID="SqlDataSource6" runat="server"></asp:SqlDataSource>
    </form>
</body>
 <telerik:RadCodeBlock ID="RadCodeBlock3" runat="server">
        <script type="text/javascript" language="javascript">  
        
             function RowClick(sender, eventArgs)
             { 
//                        var grid = $find("<%=RadGrid1.ClientID %>");	                    
//                        var masterTableView = grid.get_masterTableView();
//                                                
//                         if (masterTableView != null) 
//                         {
//                             var gridItems = masterTableView.get_dataItems();
//                             var i;
//                             for (i = 0; i < gridItems.length; ++i) 
//                             {
//                                 var gridItem = gridItems[i];
//                                 var cell = gridItem.get_cell("GridClientSelectColumn");
//                                 var controlsArray = cell.getElementsByTagName('input');
//                                 if (controlsArray.length > 0) 
//                                 {
//                                     var rdo = controlsArray[0];
//                                     rdo.checked='true';
//                                 }
//                             }
//                         }

             }
        
            function GridCreated()
            {
//                 var grid = $find("<%=RadGrid1.ClientID %>");	                    
//                        var masterTableView = grid.get_masterTableView();
//                                                
//                         if (masterTableView != null) 
//                         {
//                             var gridItems = masterTableView.get_dataItems();
//                             var i;
//                             for (i = 0; i < gridItems.length; ++i) 
//                             {
//                                 var gridItem = gridItems[i];
//                                 var cell = gridItem.get_cell("GridClientSelectColumn");
//                                 var controlsArray = cell.getElementsByTagName('input');
//                                 if (controlsArray.length > 0) 
//                                 {
//                                     var rdo = controlsArray[0];
//                                     rdo.checked='true';
//                                 }
//                             }
//                         }
            }
            
         function Test(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");	                    
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems(); //            
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row      = selectedRows[i];  
//                var cell_CurrencyID  = masterTableView.getCellByColumnUniqueName(row, "CurrencyID1");//SD drpSD     
//                var CurrencyID =cell_CurrencyID.getElementsByTagName("SELECT")[0].value; 
//                var CurrencyIDtext   =  masterTableView.getCellByColumnUniqueName(row, "CurrencyID");
//                CurrencyIDtext.innerText =CurrencyID;
//            }
         }
         
         function ChangePeriod(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");	                    
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems();
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row = selectedRows[i];
//                var cell_trxPeriod  = masterTableView.getCellByColumnUniqueName(row, "trx_period");//SD drpSD 
//                var trxPeriod =cell_trxPeriod.getElementsByTagName("SELECT")[0].value; 
//                var trxPeriodtext   =  masterTableView.getCellByColumnUniqueName(row, "trx_period1");
//                trxPeriodtext.innerText =trxPeriod;
//            }
         }
       
         //Claim Type
         function ChangeClaimType(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");	                    
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems(); //            
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row      = selectedRows[i];  
//                var cell_trxtype    = masterTableView.getCellByColumnUniqueName(row, "trx_type1");//SD drpSD    
//                //alert(cell_trxtype); 
//                var trxtype         =cell_trxtype.getElementsByTagName("SELECT")[0].value; 
//                var trxtypetext     =  masterTableView.getCellByColumnUniqueName(row, "trx_type");
//                trxtypetext.innerText =trxtype;
//                //var trxtDesc =  masterTableView.getCellByColumnUniqueName(row, "Desc");
//                //alert(cell_trxtype.getElementsByTagName("SELECT")[0].innerText);
//                //drpGstFlag
//                
//                //   var chkrecordsobj = document.getElementById('GstFlag1');
//                //   //var chkbox =chkrecordsobj.childNodes[0].childNodes[0].childNodes[0].childNodes[0].checked;
//                //   alert(chkrecordsobj);
//            }
         }
         
         function gstAmtChange(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems();
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row = selectedRows[i];  
//                var txtGstamnt = masterTableView.getCellByColumnUniqueName(row, "ToatlWithGst1");//SD drpSD
//                var txtGstamnt1 = masterTableView.getCellByColumnUniqueName(row, "ToatlWithGst");//SD drpSD
//                var changedvalue  =txtGstamnt.getElementsByTagName("input")[0].value;
//                txtGstamnt1.innerText =changedvalue;
//            }
         }
         
          //GastCode
         function ChangeGstcode(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");	                    
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems(); //            
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row      = selectedRows[i];  
//                var cell_GstCode    = masterTableView.getCellByColumnUniqueName(row, "GstCode1");//SD drpSD    
//                //alert(cell_trxtype); 
//                var trxGstCode        =cell_GstCode.getElementsByTagName("SELECT")[0].value; 
//                var trxtypeGstCode =  masterTableView.getCellByColumnUniqueName(row, "GstCode");
//                trxtypeGstCode.innerText =trxGstCode;
//                //var trxtDesc =  masterTableView.getCellByColumnUniqueName(row, "Desc");
//                //alert(cell_trxtype.getElementsByTagName("SELECT")[0].innerText);
//                //drpGstFlag
//                
//                //   var chkrecordsobj = document.getElementById('GstFlag1');
//                //   //var chkbox =chkrecordsobj.childNodes[0].childNodes[0].childNodes[0].childNodes[0].checked;
//                //   alert(chkrecordsobj);
//            }
         }
         
           //GastCode
         function gstValueChange(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems();
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row = selectedRows[i];  
//                var txtGstamnt = masterTableView.getCellByColumnUniqueName(row, "GstAmnt1");//SD drpSD
//                var txtGstamnt1 = masterTableView.getCellByColumnUniqueName(row, "GstAmnt");//SD drpSD
//                var changedvalue  =txtGstamnt.getElementsByTagName("input")[0].value;
//                txtGstamnt1.innerText =changedvalue;
//            }
         }
         
         
           //GastCode
         function ChangeRemarks(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems();
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row = selectedRows[i];  
//                var txtGstamnt = masterTableView.getCellByColumnUniqueName(row, "Remarks1");//SD drpSD
//                var txtGstamnt1 = masterTableView.getCellByColumnUniqueName(row, "Remarks");//SD drpSD
//                var changedvalue  =txtGstamnt.getElementsByTagName("input")[0].value;
//                txtGstamnt1.innerText =changedvalue;
//            }
         }
         
          //GastCode
         function gstBeforeValueChange(txtIn)
         {
//            var grid = $find("<%=RadGrid1.ClientID %>");
//            var masterTableView = grid.get_masterTableView();
//            var selectedRows = masterTableView.get_selectedItems();
//            for (var i = 0; i < selectedRows.length; i++) 
//            { 
//                var row = selectedRows[i];  
//                var txtGstamnt = masterTableView.getCellByColumnUniqueName(row, "BefGst1");//SD drpSD
//                var txtGstamnt1 = masterTableView.getCellByColumnUniqueName(row, "BefGst");//SD drpSD
//                var changedvalue  =txtGstamnt.getElementsByTagName("input")[0].value;
//                txtGstamnt1.innerText =changedvalue;
//            }
          }
 </script>
 </telerik:RadCodeBlock>
</html>
