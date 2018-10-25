<%@ Page Language="C#" AutoEventWireup="true" Codebehind="emp_overtime_ceiling.aspx.cs" Inherits="SMEPayroll.employee.emp_overtime_ceiling" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radCln" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.Web.UI" TagPrefix="radI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    

        <style type="text/css">   
        html, body, form   
        {   
           height: 100%;   
           margin: 0px;   
           padding: 0px;  
           overflow: hidden;
        }   
        </style>
</head>
<body style="margin-left: auto; ">
    <form id="form1" runat="server">
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" ScriptMode="Release"  />
        <radG:RadCodeBlock ID="codeid" runat="server">
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
		                if (val <= 999 || val == '-')
		                {
		                    if (val.indexOf(".") != -1 ) 
		                    {
		                        str= val.substring(val.indexOf(".")+1);
		                        if (str.length > 2)
		                        {
		                            ths.value = document.getElementById('txthid').value;
                                    alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
		                        }
		                        else
		                        {
		                            if (str.length == 2)
		                            {
		                                if (str > 59)
		                                {
		                                    ths.value = document.getElementById('txthid').value;
		                                    alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
		                                }
		                            }
		                            else
		                            {
		                                if (str > 5)
		                                {
		                                    ths.value = document.getElementById('txthid').value;
		                                    alert("Should be in Minutes Format. Maximum 59 Minutes Allowed");
		                                }
		                            }
		                        }
		                    }
		                 }
		                 else
		                 {
                            ths.value = document.getElementById('txthid').value;
                            alert("Hours cannot be more than 999");
		                 }
		            }
                </script>

                <script language="JavaScript1.2" type="text/javascript"> 
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
                
            
        
        </radG:RadCodeBlock>
        <telerik:RadCodeBlock ID="RadCodeBlock2" runat="server">

            <script type="text/javascript">
            <!--
              function ChangeMonth(varmonth)
              {

                //alert(varmonth);
                var res = SMEPayroll.employee.emp_overtime.SetDate(varmonth);
                var resVal = res.value;
                res.value = null;
                var resValAr = resVal.split(',');
                var date = new Date(resValAr[0]);
                var datePicker = $find("<%= rdFrom.ClientID %>");
                datePicker.set_selectedDate(date);

                date = new Date(resValAr[1]);
                datePicker = $find("<%= rdEnd.ClientID %>");
                datePicker.set_selectedDate(date);
              }
            -->
            </script>

        </telerik:RadCodeBlock>
          <!-- ToolBar -->
          <radG:RadCodeBlock ID="RadCodeBlock3" runat="server">

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
                   if (args.get_item().get_text() == 'Print')
                     {
                        
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
             function Resize()
              {
                   var myHeight = document.body.clientHeight; 
                   myHeight =myHeight - 100;
                   document.getElementById('<%= RadGrid1.ClientID %>').style.height=myHeight;
              
//                if( screen.height > 768)
//                {
//                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="90.7%";
//                }
//                else
//                {
//                    document.getElementById('<%= RadGrid1.ClientID %>').style.height="85.5%";
//                }
              }
            
            </script>

         </radG:RadCodeBlock>
         <!-- ToolBar End -->
        <!------------------------------ start ---------------------------------->
        <telerik:RadSplitter ID="RadSplitter1" Width="100%" Height="100%" runat="server" 
            Orientation="Horizontal" BorderSize="0" BorderStyle="None" PanesBorderSize="0"  BorderWidth="0px"  >
            <telerik:RadPane ID="Radpane1" runat="server" Scrolling="none" Height="32px" Width="100%"   MaxHeight="100">
                <telerik:RadSplitter ID="Radsplitter11" runat="server">
                    <telerik:RadPane ID="Radpane111" runat="server" Scrolling="none">
                        <!-- top -->
                         <table cellpadding="0" cellspacing="0" width="100%" border="0" background="../frames/images/toolbar/backs.jpg" height="32px">
                                <input type="hidden" id="txthid" runat="server" value="0" />
                                <tr>
                                    <td>
                                        <table cellpadding="0" cellspacing="0" width="100%" border="0">
                                            <tr>
                                                <td >
                                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Employee Overtime</b></font>
                                                </td>
                                                 <td>
                                                    <asp:Label ID="lblerror" ForeColor="red"   class="colheading" Text="" runat="server"></asp:Label>
                                                </td>
                                                <td align="right">
                                                    <tt class="bodytxt" style="color:White;">Year :</tt>
                                                 <%--   <asp:DropDownList ID="cmbYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged"
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
                                                    </asp:DropDownList>--%>
                                                    
                                                    <asp:DropDownList ID="cmbYear" Style="width: 65px" CssClass="textfields" DataTextField="text" DataValueField="id" DataSourceID="xmldtYear" 
                                                           runat="server"  AutoPostBack="true" OnSelectedIndexChanged="cmbYear_selectedIndexChanged">
                                                   </asp:DropDownList>
                                                   <asp:XmlDataSource ID="xmldtYear" runat="server" DataFile="~/XML/xmldata.xml" XPath="SMEPayroll/Calendar/Years/Year" ></asp:XmlDataSource>
                                
                                                    
                                                    
                                                    &nbsp;&nbsp; <tt class="bodytxt" style="color:White;">Month :</tt>
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
                                                    &nbsp;&nbsp;<tt class="bodytxt" style="color:White;">From :</tt>
                                                    <telerik:RadDatePicker ID="rdFrom" runat="server">
                                                    </telerik:RadDatePicker>
                                                    &nbsp;&nbsp;
                                                    <tt class="bodytxt" style="color:White;">To :</tt>
                                                    <telerik:RadDatePicker ID="rdEnd" runat="server">
                                                    </telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                     &nbsp;&nbsp;<tt class="bodytxt" style="color:White;">Dept :</tt>
                                                     <asp:DropDownList Width="100px"  CssClass="textfields"
                                                        ID="deptID" DataTextField="DeptName" DataValueField="ID" DataSourceID="SqlDataSource3"
                                                        runat="server" >
                                                    </asp:DropDownList>
                                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" SelectCommand="select 'ALL' as DeptName,'-1' as ID union SELECT DeptName,ID FROM dbo.DEPARTMENT WHERE COMPANY_ID= @company_id order by id">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="company_id" SessionField="Compid" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                    <tt>
                                                        <asp:ImageButton ID="imgbtnfetch" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                                                    </tt>
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
                           </center>
                        <br />
                        <!-- top end -->
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
            <telerik:RadPane ID="gridPane2" runat="server" Width="100%" Height="100%"   Scrolling="None" BorderWidth="0px">
             <!-- grid -->
             <radG:RadToolBar ID="tbRecord" runat="server" Width="100%" Skin="Office2007" UseFadeEffect="True"  Height="30px"
                     OnButtonClick="tbRecord_ButtonClick" OnClientButtonClicking="PrintRadGrid" BorderWidth="0px" Visible="false"   >
                    <Items>
                        <radG:RadToolBarButton runat="server" CommandName="Print" ImageUrl="../Frames/Images/GRIDTOOLBAR/printer-s.png"
                            Text="Print" ToolTip="Print">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton IsSeparator="true">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" Text="">
                            <ItemTemplate>
                                <div>
                                    <table cellpadding="0" cellspacing="0" border="0" >
                                        <tr>
                                            <td class="bodytxt" valign="middle" style="height:30px">
                                                &nbsp;Export To:</td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" CommandName="Excel" ImageUrl="../Frames/Images/GRIDTOOLBAR/excel-s.png"
                            Text="Excel" ToolTip="Excel">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" CommandName="Word" ImageUrl="../Frames/Images/GRIDTOOLBAR/word-s.png"
                            Text="Word" ToolTip="Word">
                        </radG:RadToolBarButton>
                        <%--       <radG:RadToolBarButton runat="server" CommandName="PDF" ImageUrl="../Frames/Images/GRIDTOOLBAR/pdf-s.png" Text="PDF"    ToolTip="PDF"></radG:RadToolBarButton>--%>
                        <radG:RadToolBarButton IsSeparator="true">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                            Text="UnGroup" ToolTip="UnGroup">
                        </radG:RadToolBarButton>
                <%--        <radG:RadToolBarButton runat="server" CommandName="Refresh" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                            Text="Clear Sorting" ToolTip="Clear Sorting">
                        </radG:RadToolBarButton>--%>
                        <radG:RadToolBarButton IsSeparator="true">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" Text="Count">
                            <ItemTemplate>
                                <div>
                                    <table cellpadding="0" cellspacing="0" border="0" style="height:30px">
                                        <tr>
                                            <td valign="middle">
                                                <img src="../Frames/Images/GRIDTOOLBAR/count-s.png" border="0" alt="Count" /></td>
                                            <td valign="middle">
                                                <asp:Label ID="Label_count" runat="server" Text="" class="bodytxt"></asp:Label>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton IsSeparator="true">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" ImageUrl="../Frames/Images/GRIDTOOLBAR/reset-s.png"
                            Text="Reset to Default" ToolTip="Reset to Default">
                        </radG:RadToolBarButton>
                        <radG:RadToolBarButton runat="server" ImageUrl="../Frames/Images/GRIDTOOLBAR/save-s.png"
                            Text="Save Grid Changes" ToolTip="Save Grid Changes">
                        </radG:RadToolBarButton>
                        <%--<radG:RadToolBarButton runat="server" CommandName="Graph" ImageUrl="../Frames/Images/GRIDTOOLBAR/graph-s.png" Text="Graph" ToolTip="Graph" Enabled="false"></radG:RadToolBarButton>--%>
                    </Items>
                </radG:RadToolBar>
                
           <radG:RadGrid ID="RadGrid1" AllowMultiRowEdit="True" AllowFilteringByColumn="true"
                        OnItemCreated="RadGrid1_ItemCreated" OnItemCommand="RadGrid1_ItemCommand" OnItemDataBound="Radgrid1_databound"
                        Skin="Outlook" Width="100%" runat="server" 
                        AllowPaging="true"
                        AllowMultiRowSelection="true" 
                        PageSize="1000" 
                        EnableHeaderContextMenu="true"
                        Height="100%"
                        ItemStyle-Wrap="false"
                        AlternatingItemStyle-Wrap="false"
                        PagerStyle-AlwaysVisible="True" 
                        GridLines="Both" 
                        AllowSorting="true" 
                        OnGridExporting="RadGrid1_GridExporting"
                        Font-Size="11"
                        Font-Names="Tahoma"
                        OnNeedDataSource="RadGrid1_NeedDataSource"
             >
            <MasterTableView CommandItemDisplay="bottom" DataKeyNames="emp_code,trx_date,status,empid,id,pay_frequency,time_card_no"
                EditMode="InPlace" AutoGenerateColumns="False" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                AllowAutomaticDeletes="true"  TableLayout="Auto" PagerStyle-Mode="Advanced" PagerStyle-Visible="true" >
                <FilterItemStyle HorizontalAlign="left" />
                <HeaderStyle ForeColor="Navy" Height="25px" Wrap="false" />
                <ItemStyle BackColor="White" Height="25px" />
                <AlternatingItemStyle BackColor="#E5E5E5" Height="25px" />
                <CommandItemTemplate>
                    <%--to get the button in the grid header--%>
                    <div style="text-align: center">
                    
                        <asp:Button ID="btnCalcOverVar" runat="server" CssClass="textfields" Style="width: 80px;
                            height: 22px" Text="Calculate" CommandName="CalcOverVar" />
                                                    
                        <asp:Button ID="btnApplyCeiling" runat="server" CssClass="textfields" Style="width: 150px;
                            height: 22px" Text="ApplyCeiling"    Visible="True" CommandName="ApplyCeiling" /> 
                            
                         <asp:Button ID="btnReset" runat="server" CssClass="textfields" Style="width: 150px;
                            height: 22px" Text="Reset"    Visible="True" CommandName="Reset" />     
                            
                        <asp:Button ID="btnsubmit" Visible="false" runat="server"  CssClass="textfields" Style="width: 80px; 
                            height: 22px" Text="Submit" CommandName="UpdateAll" />

                    </div>
                </CommandItemTemplate>
                <Columns>

                    <radG:GridTemplateColumn AllowFiltering="False" UniqueName="TemplateColumn" HeaderText="102" Display="false">
                        <ItemTemplate>
                            <%--<asp:Image ID="Image2" ImageUrl="../frames/images/EMPLOYEE/Grid- employee.png" runat="Server" />--%>
                        </ItemTemplate>
                       <HeaderStyle Width="30px" />
                    </radG:GridTemplateColumn>

                    <radG:GridBoundColumn DataField="id" Display="false" DataType="System.Int32" HeaderText="id"
                        SortExpression="id" UniqueName="id">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="empid" Display="false" DataType="System.Int32" HeaderText="empid"
                        SortExpression="empid" UniqueName="emp_code">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="empid" Display="false" DataType="System.Int32" HeaderText="empid"
                        SortExpression="empid" UniqueName="empid">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                        UniqueName="emp_name" AutoPostBackOnFilter="true" CurrentFilterFunction="contains" ShowFilterIcon="false">
                        <HeaderStyle Width="20%" />
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="overtime" Visible="false" DataType="System.Double"
                        HeaderText="Overtime" SortExpression="overtime" UniqueName="overtime">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn Display="true" DataField="department" HeaderText="Department"
                        SortExpression="department" UniqueName="department" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                        CurrentFilterFunction="contains">
                    </radG:GridBoundColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="NH_Work"
                        UniqueName="NH_Work" HeaderText="NH" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtNHWork" onkeyup="javascript:return validatenumbers(this);" onkeydown="javascript:storeoldval(this.value);"
                                Style="text-align: right" Width="40px" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.NH_Work")%>'></asp:TextBox>
                            <asp:Label  ForeColor="red" runat="server"  ID="lblCeilNH"  ></asp:Label>
                            <asp:RegularExpressionValidator ID="vldNH" ControlToValidate="txtNHWork" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="^[-+]?(?!^0*\.0*$)^\d{1,3}(\.\d{1,3})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                       <%-- <ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="overtime1"
                        UniqueName="overtime1" HeaderText="OT-1 Hrs" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtovertime" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Style="text-align: right" Width="40px"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.overtime1")%>'></asp:TextBox>
                             <asp:Label ForeColor="red" runat="server" ID="lblCeilingOt1" ></asp:Label>     
                            <asp:RegularExpressionValidator ID="vldOT1" ControlToValidate="txtovertime" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <%--<ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="overtime2"
                        UniqueName="overtime2" HeaderText="OT-2 Hrs" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtovertime2" onkeyup="javascript:return validatenumbers(this);"
                                onkeydown="javascript:storeoldval(this.value);" Width="40px" Style="text-align: right"
                                runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.overtime2")%>'></asp:TextBox>
                                <asp:Label ForeColor="red" runat="server" ID="lblCeilingOt2" ></asp:Label>     
                            <asp:RegularExpressionValidator ID="vldOT2" ControlToValidate="txtovertime2" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                       <%-- <ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="days_work"
                        UniqueName="days_work" HeaderText="Days Work" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:TextBox ID="txtDaysWork" Width="40px" Style="text-align: right" runat="server"
                                Text='<%# DataBinder.Eval(Container,"DataItem.days_work")%>'></asp:TextBox>
                                <asp:Label ForeColor="red" runat="server" ID="lblDaysWork" ></asp:Label>     
                            <asp:RegularExpressionValidator ID="vldtxdWork" ControlToValidate="txtDaysWork" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <%--<ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    
                    
                    
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="v1" UniqueName="v1"
                        HeaderText="V1" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            &nbsp;<asp:TextBox ID="txtv1" Style="text-align: right" Width="40px" runat="server"
                                Text='<%# DataBinder.Eval(Container,"DataItem.v1")%>'></asp:TextBox>
                            <asp:Label ForeColor="red" runat="server" ID="lblv1" ></asp:Label>
                         
                            <asp:RegularExpressionValidator ID="vldtxtv1" ControlToValidate="txtv1" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <%--<ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="v2" UniqueName="v2"
                        HeaderText="V2" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate  >
                            &nbsp;<asp:TextBox ID="txtv2" Width="40px" Style="text-align: right" runat="server"
                                Text='<%# DataBinder.Eval(Container,"DataItem.v2")%>'></asp:TextBox>
                            <asp:Label ForeColor="red" runat="server" ID="lblv2"></asp:Label>
                              
                            <asp:RegularExpressionValidator ID="vldtxtv2" ControlToValidate="txtv2" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                       <%-- <ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="v3" UniqueName="v3"
                        HeaderText="V3" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            &nbsp;<asp:TextBox ID="txtv3" Width="40px" Style="text-align: right" runat="server"
                                Text='<%# DataBinder.Eval(Container,"DataItem.v3")%>'></asp:TextBox>                                
                            <asp:Label ForeColor="red" runat="server" ID="lblv3" ></asp:Label> 
                                                        
                            <asp:RegularExpressionValidator ID="vldtxtv3" ControlToValidate="txtv3" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <%--<ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridTemplateColumn HeaderStyle-HorizontalAlign="center" DataField="v4" UniqueName="v4"
                        HeaderText="V4" AllowFiltering="false">
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            &nbsp;<asp:TextBox ID="txtv4" Style="text-align: right" Width="40px" runat="server"
                                Text='<%# DataBinder.Eval(Container,"DataItem.v4")%>'></asp:TextBox>
                            <asp:Label ForeColor="red" runat="server" ID="lblv4" ></asp:Label>
                        
                            <asp:RegularExpressionValidator ID="vldtxtv4" ControlToValidate="txtv4" Display="Dynamic"
                                ErrorMessage="*" ValidationExpression="(?!^0*\.0*$)^\d{1,3}(\.\d{1,2})?$" runat="server"> 
                            </asp:RegularExpressionValidator>
                        </ItemTemplate>
                       <%-- <ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridBoundColumn DataField="trx_date" Visible="false" DataType="System.DateTime"
                        HeaderText="trx_date" SortExpression="trx_date" UniqueName="trx_date">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="status" Visible="false" HeaderText="status" SortExpression="status"
                        UniqueName="status">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="v1rate"  Display="false"  DataType="System.String"
                        HeaderText="Variable" UniqueName="V1Rate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="v2rate" Visible="false" DataType="System.String"
                        HeaderText="Variable" UniqueName="V2Rate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="v3rate" Visible="false" DataType="System.String"
                        HeaderText="Variable" UniqueName="V3Rate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="v4rate" Visible="false" DataType="System.String"
                        HeaderText="Variable" UniqueName="V4Rate">
                    </radG:GridBoundColumn>
                    <radG:GridBoundColumn DataField="time_card_no" DataType="System.String" AutoPostBackOnFilter="true"
                        CurrentFilterFunction="contains" HeaderText="Time Card ID" UniqueName="time_card_no" ShowFilterIcon="false">
                    </radG:GridBoundColumn>
                    <radG:GridTemplateColumn DataField="ot_entitlement" Visible="false" UniqueName="ot_entitlement"
                        HeaderText="OT" AllowFiltering="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtot_entitlement" runat="server" Text='<%# DataBinder.Eval(Container,"DataItem.ot_entitlement")%>'></asp:TextBox>
                        </ItemTemplate>
                       <%-- <ItemStyle Width="10%" />--%>
                    </radG:GridTemplateColumn>
                    <radG:GridBoundColumn DataField="pay_frequency" Visible="false" DataType="System.String"
                        UniqueName="pay_frequency">
                    </radG:GridBoundColumn>
                    <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                    </radG:GridClientSelectColumn>
                    
                   <%--  <radG:GridBoundColumn DataField="Nationality" HeaderText="Nationality" AllowFiltering="false"
                        ReadOnly="True" SortExpression="Nationality" UniqueName="Nationality" Display="false">
                    </radG:GridBoundColumn> 
                                <radG:GridBoundColumn DataField="Trade" HeaderText="Trade" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Trade" UniqueName="Trade" Display="false">
                                </radG:GridBoundColumn>
                                        <radG:GridBoundColumn DataField="emp_type" HeaderText="Pass Type" AllowFiltering="false"
                                        ReadOnly="True" SortExpression="emp_type" UniqueName="emp_type" Display="false">
                                        </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Designation" HeaderText="Designation" AllowFiltering="false"
                                    ReadOnly="True" SortExpression="Designation" UniqueName="Designation" Display="false">
                                </radG:GridBoundColumn>
                    <radG:GridBoundColumn UniqueName="ic_pp_number" HeaderText="IC/FIN Number"  DataField="ic_pp_number" Display="false"  AllowFiltering="false" >
                    </radG:GridBoundColumn>--%>
                    
                     <radG:GridBoundColumn DataField="CelFlag" Visible="true" EmptyDataText="-1" Display="false"
                        HeaderText="CelFlag" UniqueName="CelFlag">
                    </radG:GridBoundColumn>
                    
                    <radG:GridBoundColumn DataField="NH" Visible="false"  Display="false"
                        HeaderText="NH"  UniqueName="NH">
                    </radG:GridBoundColumn>
                    
                </Columns>
            </MasterTableView>
            <ClientSettings Selecting-AllowRowSelect="true" EnableRowHoverStyle="true" AllowColumnsReorder="true"
                ReorderColumnsOnClient="true">
                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                   <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="True"  />
            </ClientSettings>
        </radG:RadGrid>
            <!-- End grid -->  
            </telerik:RadPane>
        </telerik:RadSplitter>
     <!-------------------- end -------------------------------------->
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="sp_emp_overtime"
            SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter Name="month" Type="Int32" ControlID="cmbMonth" />
                <asp:ControlParameter Name="year" Type="Int32" ControlID="cmbYear" />
                <asp:SessionParameter Name="company_id" SessionField="compid" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <!-- IF GENERAL SOLUTION :- USE sp_emp_overtime -->
        <!-- IF BIOMETREICS :- USE sp_emp_overtime1 -->
    </form>
</body>
</html>
