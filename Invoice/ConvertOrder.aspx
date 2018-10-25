<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ConvertOrder.aspx.cs" Inherits="SMEPayroll.Invoice.ConvertOrder" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    
    

    <script language="JavaScript1.2"> 
        <!-- 
            if (document.all)
            window.parent.defaultconf=window.parent.document.body.cols
            function expando()
            {
                window.parent.expandf()
            }
            document.ondblclick=expando 
        -->
    </script>

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
        <radG:RadScriptManager ID="ScriptManager" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl" runat="server" />
        <table cellpadding="0" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" width="100%" bgcolor="<% =sBaseColor %>" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4" style="height: 23px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manage Order</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td align="right" style="height: 35px">
                                <span style="color: Red; vertical-align: middle"><asp:Label ID="lblError" runat="server" Text=""></asp:Label></span> 
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <!-- new -->
            <div class="exampleWrapper">
                <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Outlook" DecoratedControls="all" />
                <table id="Table2" class="tbl" cellspacing="0" cellpadding="0" width="100%" border="0"
                    align="center">
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" border="0" width="100%" background="../frames/images/toolbar/backs.jpg"
                                style="height: 30px">
                                <tr>
                                    <td width="9%" align="left">
                                        <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Order For:</b></font>
                                    </td>
                                    <td align="left">
                                        <font class="colheading">
                                            <asp:Label ID="lblClient" runat="server" Text=""></asp:Label></font>
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
                                    <td width="15%">
                                        Contact Person:
                                    </td>
                                    <td width="15%">
                                        Block:</td>
                                    <td width="15%">
                                        Street/Building:</td>
                                    <td width="15%">
                                        Level:</td>
                                    <td width="15%">
                                        Unit:</td>
                                    <td width="15%">
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
                                        Order No:</td>
                                    <td>
                                        Sales Rep:</td>
                                    <td>
                                        Sub project:</td>
                                    <td>
                                        Effective Date:</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td class="bodytxt">
                                        <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label></td>
                                    <td class="bodytxt">
                                        <b>
                                            <asp:Label ID="lblOrder" runat="server" Text=""></asp:Label></b></td>
                                    <td class="bodytxt">
                                        <asp:Label ID="lblSalesRep" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="bodytxt" >
                                        <asp:Label ID="lblProject" runat="server" Text=""></asp:Label></td>
                                    <td class="bodytxt">
                                        <radG:RadDatePicker Calendar-ShowRowHeaders="false" ID="datePickerEffectiveDate" CssClass="bodytxt"
                                            runat="server">
                                            <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                        </radG:RadDatePicker>
                                    </td>
                                     <td></td>
                                </tr>
                                <tr>
                                    <td class="tdstand" colspan="6" id="HourTd" runat="server">
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
                                            GridLines="None" Skin="Outlook" Width="100%" OnPreRender="RadGrid1_PreRender">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="Qid,QuotationNo" DataSourceID="SqlDataSource1">
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
                                            </ClientSettings>
                                        </radG:RadGrid>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT [Qid],[QuotationNo],(select Trade from [Trade]where id=Q.[Trade])As Trade,Trade as TradeID,[NH],[OT1],[OT2]FROM [Order_hourly] Q where OrdernO=@OrdernO ">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="OrdernO" QueryStringField="orderNo" DbType="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdstand" colspan="6" id="MonthTd" runat="server">
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
                                            GridLines="None" Skin="Outlook" Width="100%" OnPreRender="RadGrid2_PreRender">
                                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="HId,QuotationNo" DataSourceID="SqlDataSource2">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image2" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
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
                                            </ClientSettings>
                                        </radG:RadGrid>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="SELECT [HId],[QuotationNo],(select Trade from [Trade]where id=Q.[Trade])As Trade,Trade as TradeID,(select emp_name +''+ emp_lname from Employee where emp_code=Q.[EmpCode])as Emp,[EmpCode],[Monthly],[Monthly],[Workingdays],[DailyRate],[OT1],[OT2] FROM [Order_Monthly] Q where OrdernO=@OrdernO">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="OrdernO" QueryStringField="orderNo" DbType="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdstand" colspan="6" id="VarTd" runat="server">
                                        Variables
                                    </td>
                                </tr>
                                <tr class="trstandbottom">
                                    <td colspan="6">
                                    </td>
                                </tr>
                                <tr class="trstandbottom">
                                    <td colspan="6" style="padding-right: 20px;">
                                        <radG:RadGrid ID="RadGrid3" runat="server" AllowFilteringByColumn="false" DataSourceID="SqlDataSource3"
                                            GridLines="None" Skin="Outlook" Width="100%" OnPreRender="RadGrid3_PreRender">
                                            <MasterTableView AutoGenerateColumns="False" DataSourceID="SqlDataSource3">
                                                <FilterItemStyle HorizontalAlign="left" />
                                                <HeaderStyle BackColor="SkyBlue" ForeColor="Navy" />
                                                <ItemStyle BackColor="White" Height="20px" />
                                                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                                                <Columns>
                                                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                                                        <ItemTemplate>
                                                            <asp:Image ID="Image2" ImageUrl="../frames/images/ADMIN/Grid-settings.png" runat="Server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10px" />
                                                    </radG:GridTemplateColumn>
                                                    <radG:GridBoundColumn DataField="Vid" HeaderText="Vid" ReadOnly="true" Visible="false"
                                                        SortExpression="Vid" UniqueName="Vid">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="VariableName" HeaderText="Variable Name" SortExpression="VariableName"
                                                        UniqueName="VariableName">
                                                    </radG:GridBoundColumn>
                                                    <radG:GridBoundColumn DataField="Amount" HeaderText="Amount" ReadOnly="True" SortExpression="Type"
                                                        UniqueName="Amount" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="right">
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
                                            </ClientSettings>
                                        </radG:RadGrid>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="SELECT (select VarName  from Variable_Preference where Vid=OV.VariableId)as VariableName,  [VariableId]as Vid,[Amount]  FROM [Order_Variable]OV where OrdernO=@OrdernO">
                                            <SelectParameters>
                                                <asp:QueryStringParameter Name="OrdernO" QueryStringField="orderNo" DbType="Int32" />
                                            </SelectParameters>
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
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="600px" onKeyPress="return taLimit(this)"
                                            onKeyUp="return taCount(this,'myCounter')"></asp:TextBox>
                                        <br>
                                        You have <b><span id="myCounter">255</span></b> characters remaining</font>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdstand" colspan="6">
                                        Document
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <table border="0" width="100%">
                                            <tr class="trstandbottom">
                                                <td>   </td>
                                            </tr>
                                            <tr class="trstandbottom">
                                                <td>
                                                    UploadDocument:</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table border="0"  width="100%">
                                                        <tr>
                                                            <td class="bodytxt">
                                                                <radG:RadUpload ID="RadUpload1" InitialFileInputsCount="1" runat="server" ControlObjectsVisibility="ClearButtons"
                                                                    MaxFileInputsCount="1" OverwriteExistingFiles="True" />
                                                             <asp:HyperLink ID="linkDocument" runat="server" Target="_blank" Text="Attached Document"></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left"><asp:Button ID="btnConvOrdSave" runat="server" Text="Save" OnClick="btnConvOrdSave_Click" /> </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <!-- End new -->
        </div>
    </form>
</body>
</html>
