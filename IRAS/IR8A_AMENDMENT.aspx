<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IR8A_AMENDMENT.aspx.cs" Inherits="IRAS.IR8A_AMENDMENT" %>
<%@ Register Assembly="RadCalendar.Net2" Namespace="Telerik.WebControls" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.WebControls" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Import Namespace="IRAS" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Test Page</title>
     <link rel="stylesheet" href="Style/PMSStyle.css" type="text/css" />
       <script type='text/javascript'>
       
       function fnOpen(fl)
{
        var flnm =fl+'.pdf'
        window.open('OpenPDF.aspx?FILENAME='+ flnm,fl);

}
       
       
       
          var int_MilliSecondsTimeOut =1;
            //Number of Reconnects
            var count=0;
            //Maximum reconnects setting
            var max = 5;
            function Reconnect(){
//alert("dfsdfd");
            count++;
            if (count < max)
            {
            window.status = 'Link to Server Refreshed ' + count.toString()+' time(s)' ;

            var img = new Image(1,1);

            img.src = '/KB/session/Reconnect.aspx';

            }
            }

            window.setInterval('Reconnect()',1); //Set to length required

    </script>

    <script type="text/javascript" language="javascript">
         
 
    
    
	    function validateform()
	    {
            var sMSG = "";
            if ( !document.form1.rdFrom.value)
            {
                sMSG = sMSG + "Please Enter Start Date\n";	
            }

            if ( !document.form1.rdEnd.value)
            {
                sMSG = sMSG + "Please Enter End Date\n";	
            }


		    if (sMSG == "")
			    return true;
		    else
		    {
			    alert(sMSG); 
			    return false;
	        }

		}
        
        function storeoldval(val)
        {
            document.getElementById('txthid').value = val;
        }
		
		function validatenumbers(ths)
		{
		    var val = ths.value;
		    var str;
		       
		    if (val <= 999999 || val == '-')
		    {
		        if (val.indexOf(".") != -1 ) 
		        {
		            str= val.substring(val.indexOf(".")+1);		           
		            if (str.length > 2)
		            {
		                ths.value = document.getElementById('txthid').value;
                        alert("Should be in Format. Maximum 2 digits Allowed");                        
		            }
		            else
		            {
		                if (str.length == 2)
		                {
		                    if (str > 99)
		                    {
		                        ths.value = document.getElementById('txthid').value;
		                        alert("Should be in Format. Maximum 99 cents Allowed");
		                    }
		                }
		                else
		                {
		                    if (str > 9)
		                    {
		                        ths.value = document.getElementById('txthid').value;
		                         alert("Should be in Format. Maximum 99 cents Allowed");	                        
		                    }
		                }
		            }
		        }
		     }
		     else
		     {
                ths.value = document.getElementById('txthid').value;
                alert("Numeric Value cannot be more than 999999");
                
		     }
		}
    </script>

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
     
</head>
<body>
    <form id="form1" runat="server">
       <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

            <script type="text/javascript">
            <!--
              function ChangeMonth(varmonth)
              {

              }
            -->
            </script>

        </telerik:RadCodeBlock>
       <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%" border="0">
          <%--  <input type="hidden" id="txthid" runat="server" value="0" />--%>
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0" bgcolor="<% =sBorderColor %>">
                        <tr>
                            <td background="/Frames/Images/toolbar/backs.jpg" colspan="6" style="width: 647px">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>IR8A AMENDMENT</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="<% =sOddRowColor %>">
                            <td colspan="6" style="width: 647px">
                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                    <tr>
                                        <td align="right" class="bodytxt">
                                            </td>
                                        <td align="right" style="width: 614px">
                                            &nbsp;<span style="font-size: 8pt; font-family: Tahoma">Bonus Declaration :</span>
                                            &nbsp;<radCln:RadDatePicker ID="BonusDate" runat="server" Calendar-ShowRowHeaders="false"
                                                DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>' Width="100px">
                                                <DateInput DateFormat="dd-MM-yyyy" Skin="">
                                                </DateInput>
                                            </radCln:RadDatePicker>
                                            <span style="font-family: Tahoma"><span style="font-size: 8pt">&nbsp; &nbsp;&nbsp; </span>
                                                <tt class="bodytxt"><span style="font-size: 12pt; font-family: Times New Roman">Director
                                                    Fee Approval:</span></tt></span> &nbsp;<radCln:RadDatePicker ID="DircetorDate" runat="server"
                                                        Calendar-ShowRowHeaders="false" DbSelectedDate='<%# DataBinder.Eval(Container,"DataItem.holiday_date")%>'
                                                        Width="100px">
                                                        <DateInput DateFormat="dd-MM-yyyy" Skin="">
                                                        </DateInput>
                                                    </radCln:RadDatePicker>
                                        </td>
                                        <td align="right" style="font-size: 12pt; font-family: Times New Roman">
                                            <tt class="bodytxt" style="font-family:Tahoma;">Year of Assesment :</tt>
                                   <%--          AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"--%>
                                            <asp:DropDownList ID="cmbYear" runat="server"
                                                Style="width: 65px" CssClass="textfields">
                                                <asp:ListItem Value="2007">2007</asp:ListItem>
                                                <asp:ListItem Value="2008">2008</asp:ListItem>
                                                <asp:ListItem Value="2009">2009</asp:ListItem>
                                                <asp:ListItem Value="2010">2010</asp:ListItem>
                                                <asp:ListItem Value="2011">2011</asp:ListItem>
                                                <asp:ListItem Value="2012">2012</asp:ListItem>
                                                <asp:ListItem Value="2013">2013</asp:ListItem>
                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <tt>&nbsp;
                                                <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid"  runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                            </tt>
                                        </td>
                                        <td align="right">
                                            <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                                style="width: 80px;" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:CheckBox ID="chkId" runat="server" AutoPostBack="true" CssClass="bodytxt" Font-Names="Tahoma"
                                    Font-Size="12px" OnCheckedChanged="chkId_CheckedChanged" Text="Import From Excel"
                                    Visible="False" /><input id="FileUpload" runat="server" class="textfields" name="FileUpload"
                                        style="width: 200px" type="file" visible="false" /></td>
                        </tr>
                        <tr>
                            <td colspan="3" class="bodytxt" style="color: Red; width: 320px;">
                               Please Select Employe For Save  and Generate XML
                         
                                                        </td>
                              <td colspan="3" class="bodytxt" style="color: Red; width: 320px;">
                                                          
                               For Over declared: Negative amount to negate from original submission
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
         <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

            <script type="text/javascript">
                    function RowDblClick(sender, eventArgs)
                    {
                        sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                    }
            </script>

        </radG:RadCodeBlock>
        <center>
            <asp:Label ID="lblerror" ForeColor="Red" class="bodytxt" runat="server" Width="611px" Height="32px"></asp:Label></center>
        <br />
        <%-- DataSourceID="SqlDataSource1"--%>
        <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
            OnItemCreated="RadGrid1_ItemCreated" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="Radgrid1_databound"
            Skin="Outlook" Width="99%" runat="server" GridLines="None" AllowPaging="true"
            AllowMultiRowSelection="true" PageSize="50" >
            <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="ID,Emp_ID" EditMode="InPlace"
                AutoGenerateColumns="False" AllowAutomaticUpdates="True" AllowAutomaticInserts="True"
                AllowAutomaticDeletes="True">
                <FilterItemStyle HorizontalAlign="Left" />
                <HeaderStyle ForeColor="Navy" />
                <ItemStyle BackColor="White" Height="20px" />
                <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                <CommandItemTemplate>
                    <%--to get the button in the grid header--%>
                    <div class="textfields" style="text-align: center">
                        <asp:Button ID="btnsubmit" runat="server" class="textfields" Style="width: 80px;
                            height: 22px" Text="Save" CommandName="UpdateAll" />
                            <asp:Button ID="gen_xml_ir8a_ammndment" runat="server" class="textfields" Style="width: 80px;
                            height: 22px" Text="Xml-IRA8" CommandName="gen_ir8a_ammndment" />
                              <asp:Button ID="gen_xml_AppxA_ammendment" runat="server" class="textfields" Style="width: 80px;
                            height: 22px" Text="XML-APX-A" CommandName="gen_appA_ammndment" />
                             <asp:Button ID="Button1" runat="server" class="textfields" Style="width: 80px;
                            height: 22px" Text="XML-APX-B" CommandName="gen_appB_ammndment" />
                            
              
              
             <%-- <asp:Button ID="gen_xml_AppxB_ammendment" runat="server" class="textfields" Style="width: 80px;height: 22px" Text="XML-APX-B" CommandName="gen_appB_ammndment" />
                    </div>--%>
                </CommandItemTemplate>
                <Columns>
                  <telerik:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                    </telerik:GridClientSelectColumn>
                    <%--<telerik:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn">
                     <ItemTemplate>
                            <asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />
                        
</ItemTemplate>
                        <ItemStyle Width="2px" />
                    </telerik:GridTemplateColumn>--%>
                    <telerik:GridBoundColumn DataField="ID" Display="False" DataType="System.Int32" HeaderText="ID"
                        SortExpression="ID" UniqueName="ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Emp_ID" Display="False" DataType="System.Int32"
                        HeaderText="Emp_ID" SortExpression="Emp_ID" UniqueName="Emp_ID">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                        UniqueName="emp_name" AutoPostBackOnFilter="True" CurrentFilterFunction="Contains">
                        <ItemStyle Width="20%" />
                    </telerik:GridBoundColumn>
                    <telerik:GridTemplateColumn DataField="GrossPay"
                        UniqueName="GrossPay" HeaderText="GrossPay" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtGrossPay" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.GrossPay")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("GrossPay").ToString(),"GrossPay")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("GrossPay").ToString(),"GrossPay" )%>'></asp:TextBox>
                            <%--<asp:RegularExpressionValidator ID="vld1" ControlToValidate="txtGrossPay" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>--%>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Bonus" UniqueName="Bonus"
                        HeaderText="Bonus" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtBonus" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                Style="text-align: right" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Bonus")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld2" ControlToValidate="txtBonus" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="DirectorFee"
                        UniqueName="DirectorFee" HeaderText="DirectorFee" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDirectorFee" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.DirectorFee")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld3" ControlToValidate="txtDirectorFee" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Commission"
                        UniqueName="Commission" HeaderText="Commission" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCommission" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Commission")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Bonus").ToString(),"Bonus" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld4" ControlToValidate="txtCommission" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Pension"
                        UniqueName="Pension" HeaderText="Pension" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPension" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                Style="text-align: right" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Pension")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Pension").ToString(),"Pension")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Pension").ToString(),"Pension" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld5" ControlToValidate="txtPension" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="TransAllow"
                        UniqueName="TransAllow" HeaderText="TransAllow" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTransAllow" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.TransAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("TransAllow").ToString(),"TransAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("TransAllow").ToString(),"TransAllow" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld6" ControlToValidate="txtTransAllow" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EntAllow"
                        UniqueName="EntAllow" HeaderText="EntAllow" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEntAllow" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EntAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("EntAllow").ToString(),"EntAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("EntAllow").ToString(),"EntAllow" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld7" ControlToValidate="txtEntAllow" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="OtherAllow"
                        UniqueName="OtherAllow" HeaderText="OtherAllow" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtOtherAllow" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.OtherAllow")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("OtherAllow").ToString(),"OtherAllow")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("OtherAllow").ToString(),"OtherAllow" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld8" ControlToValidate="txtOtherAllow" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="EmployeeCPF"
                        UniqueName="EmployeeCPF" HeaderText="CPF" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtEmployeeCPF" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.EmployeeCPF")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("EmployeeCPF").ToString(),"EmployeeCPF")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("EmployeeCPF").ToString(),"EmployeeCPF" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld9" ControlToValidate="txtEmployeeCPF" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="Funds" UniqueName="Funds"
                        HeaderText="Funds" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtFunds" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                Style="text-align: right" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.Funds")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("Funds").ToString(),"Funds")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("Funds").ToString(),"Funds" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld10" ControlToValidate="txtFunds" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    <telerik:GridTemplateColumn DataField="MBMF" UniqueName="MBMF"
                        HeaderText="MBMF" AllowFiltering="False">
                        <ItemTemplate>
                            <asp:TextBox ID="txtMBMF" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                Style="text-align: right" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.MBMF")%>' BackColor='<%#ColorChange(Eval("Emp_ID").ToString(),Eval("MBMF").ToString(),"MBMF")%>' ToolTip='<%#ToolTipValue(Eval("Emp_ID").ToString(),Eval("MBMF").ToString(),"MBMF" )%>'></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vld11" ControlToValidate="txtMBMF" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,6}(\.\d{1,6})?$"
                                runat="server"> 
                            </asp:RegularExpressionValidator>
                        
</ItemTemplate>
                        <ItemStyle Width="8%" horizontalalign="Center" />
                        <headerstyle horizontalalign="Center" />
                    </telerik:GridTemplateColumn>
                    
                   <%-- <telerik:GridButtonColumn CommandName="Add" Text="Add" UniqueName="Add" HeaderText="Add">
                   
                       </telerik:GridButtonColumn>--%>
                                <telerik:GridTemplateColumn Visible="true"  AllowFiltering="False" UniqueName="ir8aAppendixA">
                                    <ItemTemplate>
                                 <asp:Button ID="AppA_amendment" Text="AmdA" Enabled="true" runat="server" CommandName="AmendA" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
                                </telerik:GridTemplateColumn>
                                
                                <telerik:GridTemplateColumn Visible="true"  AllowFiltering="False" UniqueName="ir8aAppendixB"  >
                                    <ItemTemplate>
                                    <asp:Button ID="AppB_amendment" Text="AmdB" Enabled="true" runat="server" CommandName="AmendB" />
                                    </ItemTemplate>
                                    <ItemStyle Width="8%" />
                                 </telerik:GridTemplateColumn>
                </Columns>
                <PagerStyle Mode="Advanced" />
            </MasterTableView>
            <ClientSettings EnableRowHoverStyle="True" AllowColumnsReorder="True"
                ReorderColumnsOnClient="True">
                <Resizing EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                <Selecting AllowRowSelect="True" />
            </ClientSettings>
        </radG:RadGrid>
        <center>
            <asp:Label ID="lblMessage" Visible="false" class="bodytxt" runat="server" Text="Please Click only once to Submit"></asp:Label>
        </center>
      <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_emp_yearearn"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter Name="year" Type="Int32" ControlID="cmbYear" />
                <asp:SessionParameter Name="company_id" SessionField="compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
        <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
    </form>
</body>
</html>
