<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AddEditQuotation.aspx.cs"
    Inherits="SMEPayroll.Invoice.AddEditQuotation" %>

<%@ Register TagPrefix="qsf" Namespace="Telerik.QuickStart" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title>SMEPayroll</title>
    
    
    <link href="../Style/qsf.css" rel="stylesheet" type="text/css" />
    <%--  <script src="../Invoice/qsf.js" type="text/javascript"></script>--%>

    <script language="JavaScript1.2" type="text/javascript"> 
        <%-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
                window.parent.expandf()
            }
            document.ondblclick=expando 
        --%>
    </script>

    <script type="text/javascript">

    
    
       function isNumericKeyStrokeDecimal(evt)
        {
             var charCode = (evt.which) ? evt.which : event.keyCode
             if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode !=46))
                return false;

             return true;
        }
        
        
       

    </script>
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

    <script language="Javascript">
/**
 * DHTML textbox character counter script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/)
 * remark Textbox
 */

maxL=255;
var bName = navigator.appName;
function taLimit(taObj) {
	if (taObj.value.length==maxL) return false;
	return true;
}

function taCount(taObj,Cnt) { 
	objCnt=createObject(Cnt);
	objVal=taObj.value;
	if (objVal.length>maxL) objVal=objVal.substring(0,maxL);
	if (objCnt) {
		if(bName == "Netscape"){	
			objCnt.textContent=maxL-objVal.length;}
		else{objCnt.innerText=maxL-objVal.length;}
	}
	return true;
}
function createObject(objId) {
	if (document.getElementById) return document.getElementById(objId);
	else if (document.layers) return eval("document." + objId);
	else if (document.all) return eval("document.all." + objId);
	else return eval("document." + objId);
}
    </script>

</head>
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            </Scripts>
        </radG:RadScriptManager>
                <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <radG:RadScriptBlock ID="rd" runat="server">

            <script type="text/javascript">
            function ValidateQuotation()
            {
               var Client=document.getElementById("<%= cmbClient.ClientID %>").value; 
                if(Client=="")
                {
                 alert("Please Select Client");
                 return false
                }
               // cmbProject
               var Project=document.getElementById("<%= cmbProject.ClientID %>").value; 
                if(Project=="")
                {
                 alert("Please Select Project");
                 return false
                }
                //Date
               var date=document.getElementById("<%= datePickerId.ClientID %>").value;
                if(date=="2001-01-01")
                {
                 alert("Please Select Date");
                 return false
                }
                
                return true;
            }
            </script>

        </radG:RadScriptBlock>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="ffffff" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="4D5459" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Quotation</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="E5E5E5">
                            <td align="right" style="height: 35px">
                                <span style="color: Red; vertical-align: middle">
                                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label></span>
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
                <radG:AjaxSetting AjaxControlID="cmbClient">
                    <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="cmbClient" />
                        <radG:AjaxUpdatedControl ControlID="Label1" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label2" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label3" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label4" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="Label5" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <radG:AjaxUpdatedControl ControlID="cmbProject" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </radG:AjaxSetting>
                <%--     <radG:AjaxSetting AjaxControlID="RadGrid1">
                     <UpdatedControls>
                        <radG:AjaxUpdatedControl ControlID="RadGrid1"  LoadingPanelID="RadAjaxLoadingPanel1" />
                      </UpdatedControls>
                </radG:AjaxSetting>--%>
            </AjaxSettings>
        </radG:RadAjaxManager>
        <!-- end ajax -->
        <!-- new code --->
        <div class="exampleWrapper">
            <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
            <table id="Table2" class="tbl" cellspacing="0" cellpadding="0" width="100%" border="0"
                align="center">
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" background="../frames/images/toolbar/backs.jpg"
                            style="height: 30px">
                            <tr>
                                <td width="10%" align="left">
                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Quotation For</b></font>
                                </td>
                                <td align="left">
                                    <radG:RadComboBox ID="cmbClient" class="textfields" Font-Size="8pt" runat="server"
                                        Height="200px" Width="200px" EmptyMessage="Select Client" MarkFirstMatch="true"
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
                    <td>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="padding-left: 20px">
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Company Information
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td>
                                    Contact Person:
                                </td>
                                <td>
                                    Block:</td>
                                <td>
                                    Street/Building:</td>
                                <td>
                                    Level:</td>
                                <td style="width: 20px;">
                                    Unit:</td>
                                <td style="width: 20%;">
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
                                    Quotation No:</td>
                                <td>
                                    Sales Rep:</td>
                                <td>
                                    Sub project:</td>
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
                                <td class="bodytxt">
                                    <b>
                                        <asp:Label ID="lblQuot" runat="server" Text=""></asp:Label></b>
                                    <asp:HiddenField ID="hdnQuot" runat="server" />
                                </td>
                                <td class="bodytxt">
                                    <asp:TextBox ID="txtSalesRep" runat="server"></asp:TextBox>
                                </td>
                                <td class="bodytxt" colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <radG:RadComboBox ID="cmbProject" class="textfields" Font-Size="8pt" runat="server"
                                                    Height="200px" EmptyMessage="Select Project" MarkFirstMatch="true" AllowCustomText="true"
                                                    EnableLoadOnDemand="true">
                                                </radG:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Hourly Trade
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid1" runat="server" AllowFilteringByColumn="false" DataSourceID="SqlDataSource1"
                                        GridLines="None" Skin="Outlook" Width="100%" OnDeleteCommand="RadGrid1_DeleteCommand"
                                        OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Qid,QuotationNo" DataSourceID="SqlDataSource1"
                                            CommandItemDisplay="Bottom">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridBoundColumn DataField="Qid" DataType="System.Int32" HeaderText="Qid" ReadOnly="True"
                                                    SortExpression="Qid" Visible="False" UniqueName="Qid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="QuotationNo" DataType="System.Int32" HeaderText="QuotationNo"
                                                    ReadOnly="True" SortExpression="QuotationNo" Visible="False" UniqueName="QuotationNo">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="Trade">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="NH" HeaderText="Normal Hour(/Hr)" DataType="System.Decimal"
                                                    SortExpression="NH" UniqueName="NH">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="OT1" HeaderText="Overtime1(/Hr)" DataType="System.Decimal"
                                                    SortExpression="OT1" UniqueName="OT1">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="OT2" HeaderText="Overtime2(/Hr-Sunday/PH)" DataType="System.Decimal"
                                                    SortExpression="OT2" UniqueName="OT2">
                                                </radG:GridBoundColumn>
                                                <%--     <radG:GridBoundColumn DataField="TransAccomod" DataType="System.Decimal" HeaderText="Accommodation/Transport(Per man per day)"
                                        SortExpression="TransAccomod" UniqueName="TransAccomod">
                                    </radG:GridBoundColumn>--%>
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                                    ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="QuotationUC.ascx" EditFormType="WebUserControl">
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add Trade" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT [Qid],[QuotationNo],(select Trade from [Trade]where id=Q.[Trade])As Trade,Trade as TradeID,[NH],[OT1],[OT2] FROM [QuoationMaster_hourly] Q where QuotationNo=@QuotationNo AND [company_id]=@Compid ">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="String" />
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Monthly Trade
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid2" runat="server" AllowFilteringByColumn="false" DataSourceID="SqlDataSource2"
                                        GridLines="None" Skin="Outlook" Width="100%" OnDeleteCommand="RadGrid2_DeleteCommand"
                                        OnInsertCommand="RadGrid2_InsertCommand" OnUpdateCommand="RadGrid2_UpdateCommand"
                                        OnItemCommand="RadGrid2_ItemCommand">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="HId,QuotationNo" DataSourceID="SqlDataSource2"
                                            CommandItemDisplay="Bottom">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridBoundColumn DataField="HId" DataType="System.Int32" HeaderText="HId" ReadOnly="True"
                                                    SortExpression="HId" Visible="False" UniqueName="HId">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="QuotationNo" DataType="System.Int32" HeaderText="QuotationNo"
                                                    ReadOnly="True" SortExpression="QuotationNo" Visible="False" UniqueName="QuotationNo">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="Trade">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="TradeID" HeaderText="TradeID" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="TradeID" Visible="false">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Emp" HeaderText="Employee" ReadOnly="True" SortExpression="Type"
                                                    UniqueName="Emp">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Monthly" HeaderText="Monthly" DataType="System.Decimal"
                                                    SortExpression="Monthly" UniqueName="Monthly">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="Workingdays" HeaderText="Working Days/Week" DataType="System.Decimal"
                                                    SortExpression="Workingdays" UniqueName="Workingdays">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="DailyRate" HeaderText="Daily Rate" DataType="System.Decimal"
                                                    SortExpression="DailyRate" UniqueName="DailyRate">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="OT1" HeaderText="OT(/Hr)" DataType="System.Decimal"
                                                    SortExpression="OT1" UniqueName="OT1">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="OT2" HeaderText="OT2(/Hr-Sunday/PH)" DataType="System.Decimal"
                                                    SortExpression="OT2" UniqueName="OT2">
                                                </radG:GridBoundColumn>
                                                <radG:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridEditCommandColumn>
                                                <radG:GridButtonColumn ConfirmText="Delete this record?" ButtonType="ImageButton"
                                                    ImageUrl="~/frames/images/toolbar/Delete.gif" CommandName="Delete" Text="Delete"
                                                    UniqueName="DeleteColumn">
                                                    <ItemStyle Width="30px" />
                                                </radG:GridButtonColumn>
                                            </Columns>
                                            <EditFormSettings UserControlName="QuotationUC_Monthly.ascx" EditFormType="WebUserControl">
                                            </EditFormSettings>
                                            <CommandItemSettings AddNewRecordText="Add Trade" />
                                            <ExpandCollapseColumn Visible="False">
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT [HId],[QuotationNo],(select Trade from [Trade]where id=Q.[Trade])As Trade,Trade as TradeID,(select emp_name +''+ emp_lname from Employee where emp_code=Q.[EmpCode])as Emp,[EmpCode],[Monthly],[Workingdays],[DailyRate],[OT1],[OT2] FROM [QuoationMaster_Monthly] Q where QuotationNo=@QuotationNo ">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Variables
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadGrid ID="RadGrid3" runat="server" AllowFilteringByColumn="false" DataSourceID="SqlDataSource_Var"
                                        GridLines="None" Skin="Outlook" Width="100%" OnItemInserted="RadGrid3_ItemInserted"
                                        OnItemUpdated="RadGrid3_ItemUpdated">
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="Vid" DataSourceID="SqlDataSource_Var"
                                            CommandItemDisplay="Bottom" EditMode="InPlace" AllowAutomaticInserts="True" AllowAutomaticUpdates="True"
                                            AllowAutomaticDeletes="True">
                                            <FilterItemStyle HorizontalAlign="left" />
                                            <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                            <ItemStyle BackColor="White" Height="20px" />
                                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                            <Columns>
                                                <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </radG:GridTemplateColumn>
                                                <radG:GridBoundColumn DataField="Vid" DataType="System.Int32" HeaderText="Vid" ReadOnly="True"
                                                    SortExpression="Vid" Visible="False" UniqueName="Vid">
                                                </radG:GridBoundColumn>
                                                <radG:GridBoundColumn DataField="VariableId" DataType="System.Int32" HeaderText="VariableId"
                                                    ReadOnly="True" SortExpression="VariableId" Visible="False" UniqueName="VariableId">
                                                </radG:GridBoundColumn>
                                                <radG:GridDropDownColumn DataField="VariableId" EditFormColumnIndex="0" DataSourceID="SqlDataSource_VarPref"
                                                    HeaderStyle-HorizontalAlign="center" HeaderText="Variable Name" ListTextField="VarName"
                                                    ListValueField="Vid" UniqueName="DropCol">
                                                </radG:GridDropDownColumn>
                                                <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" EditFormColumnIndex="0"
                                                    HeaderStyle-HorizontalAlign="center" SortExpression="Amount" UniqueName="Amount"
                                                    ItemStyle-HorizontalAlign="right">
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
                                                <HeaderStyle Width="19px" />
                                            </ExpandCollapseColumn>
                                            <RowIndicatorColumn Visible="False">
                                                <HeaderStyle Width="20px" />
                                            </RowIndicatorColumn>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" />
                                            <ClientEvents OnRowDblClick="RowDblClick" />
                                        </ClientSettings>
                                    </radG:RadGrid>
                                     <%--  SelectCommand="SELECT [Vid],[VarName]  FROM [Variable_Preference] where [Company_id]=@Compid"--%>
                                    <asp:SqlDataSource ID="SqlDataSource_VarPref" runat="server" 
                                     SelectCommand="select [Vid],([VarName]+'('+[DailyOneTime] +')') as [VarName]  from [Variable_Preference] where [Company_id]=@Compid"
                                    >
                                        <SelectParameters>
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="SqlDataSource_Var" runat="server" SelectCommand="SELECT [Vid],(select VarName  from Variable_Preference where Vid=Q.[VariableId])as VariableName,VariableId,[Amount]  FROM [Quotation_Variable]Q where QuotationNo=@QuotationNo AND company_id=@compid order by [Vid] Desc"
                                        InsertCommand="INSERT INTO [Quotation_Variable]([VariableId],[Amount],[QuotationNo],[company_id]) VALUES(@VariableId,@Amount,@QuotationNo,@compid)"
                                        UpdateCommand="UPDATE [Quotation_Variable] SET [VariableId] =@VariableId ,[Amount] =@Amount ,[QuotationNo]=@QuotationNo,[company_id] =@compid  WHERE [VId]=@Vid "
                                        DeleteCommand="delete from Quotation_Variable where Vid=@Vid AND company_id=@compid ">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="Int32" />
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="VariableId" Type="Int32" />
                                            <asp:Parameter Name="Amount" Type="Decimal" />
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="Int32" />
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </InsertParameters>
                                        <UpdateParameters>
                                            <asp:Parameter Name="Vid" Type="Int32" />
                                            <asp:Parameter Name="VariableId" Type="Int32" />
                                            <asp:Parameter Name="Amount" Type="Decimal" />
                                            <asp:ControlParameter ControlID="hdnQuot" Name="QuotationNo" Type="Int32" />
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </UpdateParameters>
                                        <DeleteParameters>
                                            <asp:Parameter Name="Vid" Type="Int32" />
                                            <asp:SessionParameter SessionField="Compid" Name="Compid" Type="Int32" />
                                        </DeleteParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Remarks
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="600px" Height="50px"
                                        onKeyPress="return taLimit(this)" onKeyUp="return taCount(this,'myCounter')"></asp:TextBox>
                                    <br>
                                    You have <b><span id="myCounter">255</span></b> characters remaining</font>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdstand" colspan="6">
                                    Report Text
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6">
                                </td>
                            </tr>
                            <tr class="trstandbottom">
                                <td colspan="6" style="padding-right: 20px;">
                                    <radG:RadEditor ID="NewsEditor" runat="server" Height="200px" Width="100%" ToolsFile="~/XML/BasicTools.xml">
                                 <%--       <Modules>
                                            <radG:EditorModule Name="RadEditorStatistics" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorDomInspector" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorNodeInspector" Visible="false" Enabled="true" />
                                            <radG:EditorModule Name="RadEditorHtmlInspector" Visible="false" Enabled="true" />
                                        </Modules>--%>
                                    </radG:RadEditor>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 10px;" colspan="6">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="textfields"
                                        OnClientClick="return ValidateQuotation()" />&nbsp;
                                    <asp:Button ID="btnConvertOrder" runat="server" Text="Convert to Order" OnClick="btnConvertOrder_Click"
                                        class="textfields" />&nbsp;
                                    <asp:Button ID="btnPrint" runat="server" CausesValidation="False" class="textfields"
                                        OnClick="btnPrint_Click" Text="Print" Width="64px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <!-- new code End -->
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
