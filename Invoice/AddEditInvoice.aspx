<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AddEditInvoice.aspx.cs"
    Inherits="SMEPayroll.Invoice.AddEditInvoice" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>SMEPayroll</title>
    
    

    <%--<script language="JavaScript1.2"> 
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
                window.parent.expandf()
            }
            document.ondblclick=expando 
        -->
    </script>--%>
    <link href="../EmployeeRoster/Roster/css/general-notification.css" rel="stylesheet" />

    <style type="text/css">
    INPUT {
    FONT-SIZE: 8pt;	
    FONT-FAMILY: Tahoma
          }
        .bigModule
        {
            width: 750px;
            background: url(qsfModuleTop.jpg) no-repeat;
            margin-bottom: 15px;
        }
        .bigModuleBottom
        {
            background: url(qsfModuleBottom.gif) no-repeat bottom;
            color: #252f34;
            padding: 23px 17px;
            line-height: 16px;
            font-size: 12px;
        }
        .trstandtop
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            vertical-align:top;
        }
        .trstandbottom
        {
	        font-family: Tahoma;
	        font-size: 11px;
            height: 20px; 
            
            COLOR: gray;
            vertical-align:bottom;
            valign:bottom;
        }
       
        .tdstand
        {
            height:30px;
            vertical-align:text-bottom;
            vertical-align:bottom;
            border-bottom-width:1px;
            border-bottom-color:Silver;
            border-bottom-style:inset;
	        font-family: Tahoma;
	        font-size: 12px;
	        font-weight:bold;
        }
        .tbl
        {
            cellpadding:0;
            cellspacing:0;
            border:0;
            background-color: White; 
            width: 100%; 
            height: 100%; 
            background-image: url(../Frames/Images/TOOLBAR/qsfModuleTop2.jpg);
            /*background-repeat: no-repeat;*/
           background-repeat:repeat-x;
        }
        .multiPage
        {
            float:left;
            border:1px solid #94A7B5;
            background-color:#F0F1EB;
            padding:4px;
            padding-left:0;
            width:85%;
            height:550px%;
            margin-left:-1px;                
        }
        
      .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>

    <script language="javascript" type="text/javascript">
     function ValidateHourly()
     {
     
        var fromdate=document.getElementById("<%= datePickerFrom.ClientID %>").value; 
        if(fromdate=="")
        {
         alert("Please Select From Date");
         return false
        }
              
        var todate=document.getElementById("<%= datePickerTo.ClientID %>").value; 
         if(todate=="")
        {
         alert("Please Select TO Date");
         return false
        }
        
       return true
     }
     
     function ValidatePreview()
     {
        var Invoice=document.getElementById("<%= txtInvoiceNo.ClientID %>").value; 
        if(Invoice=="")
        {
         alert("Please Enter the Invoice Number");
         return false
        }
        return true;
     }
     
    </script>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="ffffff" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="4D5459" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Invoice</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="E5E5E5">
                            <td align="right" style="height: 35px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!--Ajax -->
        <radG:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radG:AjaxSetting AjaxControlID="drpClient">
                    <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="drpClient" />
                        <radG:AjaxUpdatedControl ControlID="Label1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label2" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label3" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label4" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label5" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="drpProject" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </radG:AjaxSetting>
            </AjaxSettings>
        </radG:RadAjaxManager>
        <!-- end ajax -->
        <!-- new code -->
        <div class="exampleWrapper" >
            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
            <table id="Table2" class="tbl" cellspacing="0" cellpadding="0" width="100%" border="0"
                align="center">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" background="../frames/images/toolbar/backs.jpg"
                            style="height: 30px">
                            <tr>
                                <td width="10%" align="left">
                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Invoice For</b></font>
                                </td>
                                <td align="left">
                                    <radG:RadComboBox ID="cmbClient" class="textfields" Font-Size="8pt" runat="server"
                                        Height="200px" Width="200px" EmptyMessage="Select a Client" MarkFirstMatch="true"
                                        AllowCustomText="true" EnableLoadOnDemand="true" OnSelectedIndexChanged="drpClient_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </radG:RadComboBox>
                                </td>
                                <td align="right">
                                    <radG:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" IsSticky="true">
                                        <table cellpadding="0" cellspacing="0" width="100%" align="center">
                                            <tr>
                                                <td align="center">
                                                    <font class="colheading">Loading...</font></td>
                                            </tr>
                                        </table>
                                    </radG:RadAjaxLoadingPanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="padding-left: 20px">
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Company Information
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td style="width: 16%;">
                                    Contact Person:
                                </td>
                                <td style="width: 16%;">
                                    Block:</td>
                                <td style="width: 16%;">
                                    Street/Building:</td>
                                <td style="width: 16%;">
                                    Level:</td>
                                <td style="width: 16%;">
                                    Unit:</td>
                                <td style="width: 16%;">
                                    Postal Code:
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                <td class="bodytxt">
                                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="bodytxt">
                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="bodytxt">
                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="bodytxt">
                                    <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="bodytxt">
                                    <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Detail
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td>
                                    Date:</td>
                                <td>
                                    Invoice No:
                                </td>
                                <td>
                                    Payment Terms:</td>
                                <td>
                                    Sub project:</td>
                                <td>
                                    GST:
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt">
                                    <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="datePickerId" runat="server">
                                        <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    </radG:RadDatePicker>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvoiceNo" runat="server"></asp:TextBox></td>
                                <td>
                                       <radG:RadComboBox ID="cmbpaymentterms" runat="server" Text="Payment Terms" MaxHeight="150px"
                                        Width="200px" AllowCustomText="True" EnableLoadOnDemand="true"  >
                                    </radG:RadComboBox>
                                </td>
                                <td>
                                    <radG:RadComboBox ID="drpProject" runat="server" Text="Projects" MaxHeight="150px"
                                        Width="200px" AllowCustomText="True" ChangeTextOnKeyBoardNavigation="False" DataSourceID="SqlDataSource2"
                                        DataValueField="Sub_Project_ID" DataTextField="Project">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chk1" Checked="false" />
                                            <asp:Label runat="server" ID="Label1" AssociatedControlID="chk1">
                                            <%# Eval("Project") %>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                    </radG:RadComboBox>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select Sub_Project_Name as Project,Sub_Project_ID from subproject where Sub_project_Id in (select distinct Project  from order_info where ClientId=@ClientId AND Project IS NOT NULL)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="cmbClient" Name="ClientId" PropertyName="SelectedValue"
                                                Type="Int32" DefaultValue="0" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkGST" runat="server" />
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Hourly Trade
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr class="trstandbottom">
                                            <td>
                                                From:</td>
                                            <td>
                                                To:</td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="datePickerFrom" runat="server">
                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                </radG:RadDatePicker>
                                            </td>
                                            <td>
                                                <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="datePickerTo" runat="server">
                                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                                </radG:RadDatePicker>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnView" OnClick="btnView_Click" OnClientClick="return ValidateHourly()"
                                                    runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid_Hourly" runat="server" AllowPaging="true" PageSize="20"
                                        GridLines="None" Width="100%" ShowFooter="True" Skin="Outlook" AllowMultiRowSelection="true">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Trade">
                                            <Columns>
                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn_H">
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="AutoId" UniqueName="AutoId" SortExpression="AutoId"
                                                    HeaderText="AutoId" Visible="false">
                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Project" UniqueName="Project" SortExpression="Project"
                                                    HeaderText="Project">
                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" UniqueName="Trade" SortExpression="Trade"
                                                    HeaderText="Trade">
                                                    <ItemStyle Width="30%" HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" SortExpression="AMOUNT"
                                                    Aggregate="Sum" FooterText="Total=" HeaderStyle-HorizontalAlign="center" UniqueName="AMOUNT">
                                                    <ItemStyle Width="40%" HorizontalAlign="right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridTemplateColumn EditFormColumnIndex="1" UniqueName="Detail" HeaderText="Detail"
                                                    HeaderStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="DetailLink" Text='Detail' runat="server" NavigateUrl='<%# GetUrl( Eval("TradeId"),Eval("Project_ID") )%>'
                                                            Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                                </radG:GridTemplateColumn>
                                            </Columns>
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <FooterStyle HorizontalAlign="right" />
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Monthly Trade
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <table>
                                        <tr class="trstandbottom">
                                            <td>
                                                Year:</td>
                                            <td>
                                                Month:</td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <radG:RadComboBox ID="cmbYear" class="textfields" Font-Size="8pt" runat="server"
                                                    Height="200px" EmptyMessage="Year" MarkFirstMatch="true" EnableLoadOnDemand="true"
                                                    AutoPostBack="true">
                                                    <Items>
                                                        <radG:RadComboBoxItem Text="2011" Value="2011" />
                                                        <radG:RadComboBoxItem Text="2012" Value="2012" />
                                                        <radG:RadComboBoxItem Text="2013" Value="2013" />
                                                        <radG:RadComboBoxItem Text="2014" Value="2014" />
                                                        <radG:RadComboBoxItem Text="2015" Value="2015" />
                                                         <radG:RadComboBoxItem Text="2016" Value="2016" />
                                                        <radG:RadComboBoxItem Text="2017" Value="2017" />
                                                        <radG:RadComboBoxItem Text="2018" Value="2018" />
                                                        <radG:RadComboBoxItem Text="2019" Value="2019" />
                                                        <radG:RadComboBoxItem Text="2020" Value="2020" />
                                                    </Items>
                                                </radG:RadComboBox>
                                            </td>
                                            <td>
                                                <radG:RadComboBox ID="cmbMonth" class="textfields" Font-Size="8pt" runat="server"
                                                    Height="200px" EmptyMessage="Month" MarkFirstMatch="true" EnableLoadOnDemand="true"
                                                    AutoPostBack="true">
                                                    <Items>
                                                        <radG:RadComboBoxItem Text="January" Value="01" />
                                                        <radG:RadComboBoxItem Text="February" Value="02" />
                                                        <radG:RadComboBoxItem Text="March" Value="03" />
                                                        <radG:RadComboBoxItem Text="April" Value="04" />
                                                        <radG:RadComboBoxItem Text="May" Value="05" />
                                                        <radG:RadComboBoxItem Text="June" Value="06" />
                                                        <radG:RadComboBoxItem Text="July" Value="07" />
                                                        <radG:RadComboBoxItem Text="August" Value="08" />
                                                        <radG:RadComboBoxItem Text="September" Value="09" />
                                                        <radG:RadComboBoxItem Text="October" Value="10" />
                                                        <radG:RadComboBoxItem Text="November" Value="11" />
                                                        <radG:RadComboBoxItem Text="December" Value="12" />
                                                    </Items>
                                                </radG:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnMonthlyView" OnClick="btnMonthlyView_Click" runat="server"
                                                    ImageUrl="~/frames/images/toolbar/go.ico" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid1_Monthly" runat="server" AllowPaging="true" PageSize="20"
                                        GridLines="None" Width="100%" ShowFooter="True" Skin="Outlook" AllowMultiRowSelection="true"
                                        OnNeedDataSource="RadGrid1_Monthly_NeedDataSource" OnUpdateCommand="RadGrid1_Monthly_UpdateCommand">
                                        <ClientSettings AllowExpandCollapse="True">
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="AutoId,Workingdays,MonthlyFixed">
                                            <Columns>
                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn_M">
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="TradeId" UniqueName="TradeId" SortExpression="TradeId"
                                                    HeaderText="TradeId" Visible="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="AutoId" UniqueName="AutoId" SortExpression="AutoId"
                                                    HeaderText="AutoId" Visible="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="ProjectName" ReadOnly="True" UniqueName="ProjectName"
                                                    SortExpression="ProjectName" HeaderText="ProjectName">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="TradeName" ReadOnly="True" UniqueName="TradeName"
                                                    SortExpression="TradeName" HeaderText="Trade">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Empname" ReadOnly="True" UniqueName="Empname" SortExpression="Empname"
                                                    HeaderText="Emp Name">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Monthly" ReadOnly="True" Aggregate="Sum" FooterText="Total="
                                                    UniqueName="Monthly" SortExpression="Monthly" HeaderText="Salary">
                                                    <ItemStyle HorizontalAlign="right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridEditCommandColumn UniqueName="EditCommandColumn" ButtonType="ImageButton">
                                                </radG:GridEditCommandColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <div class="exampleWrapper">
                                                        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                                                        <table id="Table2" class="tbl" cellspacing="0" cellpadding="0" width="100%" border="0"
                                                            align="center">
                                                            <tr>
                                                                <td style="color: #000000; height: 20px; font-weight: bold; background-color: #e9eed4;
                                                                    text-align: center">
                                                                    Salary Deduction
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <table style="padding-left: 20px;" border="0" width="60%" align="right">
                                                                        <tr class="trstandbottom">
                                                                            <td>
                                                                                Emp Name:</td>
                                                                            <td>
                                                                                Deduct Days:
                                                                            </td>
                                                                            <td>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="TextBox1" runat="server" Text='<%# Bind( "Empname" ) %>' Width="70%"></asp:Label></td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDeductDays" runat="server" Text='' MaxLength="3"> </asp:TextBox>
                                                                                <asp:TextBox ID="txtMonthly" runat="server" Text='<%# Bind( "Monthly" ) %>' Visible="false"> </asp:TextBox>
                                                                                <asp:HiddenField ID="hdnDailyRate" runat="server" Value='<%# Bind("DailyRate" ) %>' />
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnUpdate" Text='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "Insert" : "Update" %>'
                                                                                    runat="server" CommandName='<%# (Container as GridItem).OwnerTableView.IsItemInserted ? "PerformInsert" : "Update" %>'
                                                                                    OnClientClick="return ValidateClient();"></asp:Button>&nbsp;
                                                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                                    CommandName="Cancel"></asp:Button>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </FormTemplate>
                                            </EditFormSettings>
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <FooterStyle HorizontalAlign="right" />
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Variables &nbsp;<asp:ImageButton ID="btnVariableView" OnClick="btnVariableView_Click"
                                        runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid3" runat="server" AllowPaging="true" PageSize="20" GridLines="None"
                                        Width="100%" ShowFooter="false" Skin="Outlook" AllowMultiRowSelection="true"
                                        OnNeedDataSource="RadGrid3_NeedDataSource" OnItemDataBound="RadGrid3_ItemDataBound"
                                        OnInsertCommand="RadGrid3_InsertCommand" OnUpdateCommand="RadGrid3_UpdateCommand"
                                        OnDeleteCommand="RadGrid3_DeleteCommand">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="OVid,AutoId" CommandItemDisplay="Bottom"
                                            EditMode="inPlace">
                                            <Columns>
                                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn_V" >
                                                </radG:GridClientSelectColumn>
                                                <radG:GridBoundColumn DataField="OVid" UniqueName="OVid" SortExpression="OVid" ReadOnly="true"
                                                    HeaderText="OVid" Visible="false">
                                                    <ItemStyle HorizontalAlign="left" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="AutoId" UniqueName="AutoId" SortExpression="AutoId"
                                                    HeaderText="AutoId" Visible="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Project" Visible="false" ReadOnly="True" UniqueName="Project" SortExpression="Project"
                                                    HeaderText="Project">
                                                </radG:GridBoundColumn>
                                                <radG:GridDropDownColumn DataField="VariableId" EditFormColumnIndex="0" DataSourceID="SqlDataSource_VarPref"
                                                    HeaderStyle-HorizontalAlign="center" HeaderText="Variable Name" ListTextField="VarName"
                                                    ListValueField="Vid" UniqueName="DropCol">
                                                </radG:GridDropDownColumn>
                                                <radG:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type" ReadOnly="true"
                                                    HeaderStyle-HorizontalAlign="center" UniqueName="Type">
                                                    <ItemStyle  HorizontalAlign="center" />
                                                </radG:GridBoundColumn>
                                                 <radG:GridBoundColumn DataField="DailyOneTime1" HeaderText="Daily/Monthly" SortExpression="Type" ReadOnly="true"
                                                    HeaderStyle-HorizontalAlign="center" UniqueName="DailyOneTime">
                                                    <ItemStyle HorizontalAlign="center" />
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="FinalAmount" HeaderText="Amount" SortExpression="AMOUNT"
                                                    Aggregate="Sum" FooterText="Total=" HeaderStyle-HorizontalAlign="center" UniqueName="AMOUNT">
                                                    <ItemStyle  HorizontalAlign="right" />
                                                </radG:GridBoundColumn>
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn" ItemStyle-HorizontalAlign="right"
                                                    ItemStyle-Width="10%">
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                                    ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn" ItemStyle-Width="10%">
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <CommandItemSettings AddNewRecordText="Add Trade" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px"></HeaderStyle>
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px"></HeaderStyle>
                                            </RowIndicatorColumn>
                                            <FooterStyle HorizontalAlign="right" />
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="true" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource_VarPref" runat="server" SelectCommand="SELECT [Vid],([VarName]+'('+[DailyOneTime] +')') as [VarName]  FROM [Variable_Preference] where [Company_id]=@Compid">
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Footer Text 
                                </td>
                            </tr>
                            <tr>
                               <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadEditor ID="FooterEditor" runat="server" Height="200px" Width="100%"  ToolsFile="~/XML/BasicTools.xml">
                                 <%--      <Modules>
                                            <radG:EditorModule Name="RadEditorStatistics" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorDomInspector" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorNodeInspector" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorHtmlInspector" Visible="false" Enabled="true" />
                                        </Modules>--%>
                                    </radG:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6" style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td class="bodytxt" colspan="6">
                                    <table cellpadding="0" cellspacing="0" border="0" width="20%" align="right">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnPreview" runat="server" CausesValidation="False" OnClick="btnPreview_Click" OnClientClick="return ValidatePreview()"
                                                    Text="Preview" Width="64px" />
                                            </td>
                                              <td>
                                                <asp:Button ID="btnConfirm" runat="server" CausesValidation="False" OnClick="btnConfirm_Click" Enabled="false"
                                                    Text="Confirm" Width="64px" />
                                            </td>
                                        </tr>
                                        
                                    </table>
                                </td>
                              
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <!-- end new code -->
    </form>
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <script src="../EmployeeRoster/Roster/scripts/jquery-1.10.2.js" type="text/javascript"></script>    
    <script src="../EmployeeRoster/Roster/scripts/general-notification.js" type="text/javascript"></script>

    <script type="text/javascript">
       
        window.onload = function () {
            CallNotification('<%=ViewState["actionMessage"].ToString() %>'); }

    </script>
</body>
</html>
