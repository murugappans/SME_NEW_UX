<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManualTimesheetDataEntry.aspx.cs" Inherits="SMEPayroll.TimeSheet.ManualTimesheetDataEntry" %>
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
    

</style>

    <script type="text/javascript" language="JavaScript1.2"> 
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
<body style="margin-left: auto">
    <form id="form1" runat="server">
     <telerik:RadScriptManager ID="RadScriptManager1" Runat="server">
    </telerik:RadScriptManager> 
    
        
     <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1"   AnimationDuration="1500"  runat="server" Transparency="10" BackColor="#E0E0E0" InitialDelayTime="500">
            <asp:Image ID="Image1" Style="margin-top: 200px" runat="server" ImageUrl="~/Frames/Images/ADMIN/WebBlue.gif" AlternateText="Loading"></asp:Image>
    </telerik:RadAjaxLoadingPanel>
    
    
    
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
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
            
            <telerik:AjaxSetting AjaxControlID="btnCopy">
                <UpdatedControls>                                          
                     <telerik:AjaxUpdatedControl  ControlID="RadGrid2" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                      <telerik:AjaxUpdatedControl  ControlID="lblMsg" LoadingPanelID="RadAjaxLoadingPanel1" >
                     </telerik:AjaxUpdatedControl> 
                </UpdatedControls>
            </telerik:AjaxSetting>
            
            
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

        <script type="text/javascript">
            function stopPropagation(e)
            {
                e.cancelBubble = true;
                
                if (e.stopPropagation)
                {
                    e.stopPropagation();
                }
            }
            
            //            function change(val)
            //            { 
            //            
            //                    alert(val);
            //                    //                var grid = $find("<%=RadGrid2.ClientID %>");
            //                    //                var MasterTable = grid.get_masterTableView();
            //                    //                alert(MasterTable.get_dataItems().length);
            //                    //                
            //                    //                var row1 =MasterTable.get_dataItems()[val];
            //                    //                var cell1 = MasterTable.getCellByColumnUniqueName(row1, "Project")
            //                    //                var drpvalue = cell1.getElementsByTagName("OPTION")[0].innerHTML;
            //                                    
            //                                    //                alert(drpvalue);
            //                                    //                
            //                                    //                for (var i = 0; i < MasterTable.get_dataItems().length; i++)
            //                                    //                {
            //                                    //                    var row =MasterTable.get_dataItems()[i];
            //                                    //                    var cell = MasterTable.getCellByColumnUniqueName(row, "Project")                    
            //                                    //                    //masterTable.getCellByColumnUniqueName(masterTable.get_dataItems()[i], "ContactName").innerHTML;  
            //                                    //                    //here cell.innerHTML holds the value of the cell                   
            //                                    //                    cell.getElementsByTagName("OPTION")[0].innerHTML =drpvalue; 
            //                                    //                }
            //                                    
            //                                    
            //                    //                     var masterTable = $find('<%= RadGrid2.ClientID %>').get_masterTableView();
            //                    //                    var cell;
            //                    //                    for (var i = 0; i < masterTable.get_dataItems().length; i++) {
            //                    //                        rowObject = masterTable.get_dataItems()[i];
            //                    //                        cell = masterTable.getCellByColumnUniqueName(rowObject, "Project");
            //                    //                        //cell.getElementsByTagName("OPTION")[0] = "SomeText"; 
            //                    //                        alert(cell.getElementsByTagName("OPTION")[0].innerHTML);      
            //                    //                    }

            //            }
            
             function SetKeyInOutTime()
             {
               // alert('hi');
//               var grid = $find("<%=RadGrid1.ClientID %>");
//               var MasterTable = grid.get_masterTableView();

//               //var allrows = MasterTable.rows;
//                //var itemIndex = args.get_commandArgument();                            
//               
//                var row = MasterTable.get_dataItems()[2]; //to access the row
//               
//                if(row!=null)
//                {
//                     //alert(row._element.cells[16].innerHTML);//="100";
//                     var cell = MasterTable.getCellByColumnUniqueName(row, "txtInTime")
//                     cell.innerHTML='20';
//                }
//               for (var i = 0; i < selectedRows.length; i++)
//               {
//                 var row = selectedRows[i];
//                 var cell = MasterTable.getCellByColumnUniqueName(row, "CategoryID")
//                //here cell.innerHTML holds the value of the cell
//               }
             }

            
        </script>

        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Manual Timesheet</b></font>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="1" cellspacing="0" width="100%">
            <tr>
                <td>
                    <input name="hiddenmsg" id="hiddenmsg" type="hidden" runat="server" />
                </td>
            </tr>            
            <tr>
                    <td>
                         <table style="vertical-align: middle; width: 97%;" align="center" cellpadding="0"
                        cellspacing="0" border="0">   
                                             
                        <tr id="tr1" runat="server"> 
                            <td class="bodytxt" align="left">
                                Employee :
                            </td>
                            <td  class="bodytxt" align="left" > 
                                <radG:RadComboBox ID="RadComboBoxEmpPrj" runat="server" Height="200px" Width="180px" BorderWidth="0px" 
                                    AutoPostBack="true" DropDownWidth="375px" EmptyMessage="Select a Employee" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested"
                                    OnSelectedIndexChanged="RadComboBoxEmpPrj_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <table class="bodytxt" style="width: 350px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 120px;" class="bodytxt">
                                                    Emp Name</td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    Card No</td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    IC NO</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table class="bodytxt" style="width: 350px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 120px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                </td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Attributes['ic_pp_number']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </radG:RadComboBox>
                            </td>                            
                            <td class="bodytxt" align="left">
                                Start Date :
                            </td>
                            <td class="bodytxt" align="Left">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjStart"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <Calendar ID="Calendar3" runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="Left">
                                End Date :
                            </td>
                            <td class="bodytxt"  align="Left">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdEmpPrjEnd"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td align="left">
                                <asp:ImageButton ID="imgbtnfetchEmpPrj" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td class="bodytxt" align="Left" visible="false">
                                Sub Project :
                            </td>
                            <td class="bodytxt" visible="false"  >
                                <asp:DropDownList ID="drpEmpSubProject"  CssClass="bodytxt"  runat="server" Width="180px">
                                </asp:DropDownList>
                            </td>                                                  
                            <td>
                                <asp:CheckBoxList  Visible="True" ID="chkrecords" runat="server" CssClass="bodytxt" ValidationGroup="val1" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"  CausesValidation="true" >
                                    <asp:ListItem  Text="NightShift" Value="NightShift" Selected="False" ></asp:ListItem>                                    
                                </asp:CheckBoxList>
                            </td>
                            
                            <td align="right" style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                        
                        <tr id="tr2" runat="server">
                            <td>
                                <table>
                                      <td class="bodytxt" align="right">
                                From Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdFrom"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <Calendar ID="Calendar1" runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                To Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdTo"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                In Time :
                            </td>
                            <td class="bodytxt" align="left">
                                <radG:RadTimePicker Width="80px" ID="txtInTimeFrm" runat="server" Skin="Vista" TabIndex="0">
                                </radG:RadTimePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                Out Time :
                            </td>
                            <td class="bodytxt" align="left">
                                <radG:RadTimePicker Width="80px" ID="txtOutTimeFrm" runat="server" Skin="Vista" TabIndex="0">
                                </radG:RadTimePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                Employee :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAddEmp" runat="server" Width="120px" AutoPostBack="True"
                                    OnSelectedIndexChanged="drpAddEmp_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td class="bodytxt" align="right">
                                Sub Project :
                            </td>
                            <td>
                                <asp:DropDownList ID="drpAddSubProject" runat="server" Width="120px">
                                </asp:DropDownList>
                            </td>
                                </table>
                            </td>                          
                        </tr>
                        <tr id="tr3" runat="server">
                            <td class="bodytxt" align="right">
                                Sub Project :
                            </td>
                            <td class="bodytxt">
                                <asp:DropDownList ID="drpSubProjectEmp" CssClass="bodytxt" runat="server" AutoPostBack="true" Width="120px">
                                </asp:DropDownList>
                            </td>
                            <td class="bodytxt" align="right">
                                Employee :
                            </td>
                            <td>
                                <radG:RadComboBox ID="RadComboBoxPrjEmp" runat="server" Height="200px" Width="150px"
                                    DropDownWidth="375px" EmptyMessage="All Employees" HighlightTemplatedItems="true"
                                    EnableLoadOnDemand="true" OnItemsRequested="RadComboBoxEmpPrj_ItemsRequested">
                                    <HeaderTemplate>
                                        <table style="width: 350px"  class="bodytxt"   cellspacing="0" cellpadding="0">
                                            <tr>
                                                <%--                                                <td style="width: 20px">
                                                    Select
                                                </td>--%>
                                                <td style="width: 120px;" class="bodytxt">
                                                    Emp Name</td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    Card No</td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    IC NO</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 350px" cellspacing="0" class="bodytxt" cellpadding="0">
                                            <tr>
                                                <%--                                                <td style="width: 30px;">
                                                    <asp:CheckBox runat="server" ID="CheckBox" onclick="stopPropagation(event);" value='<%# DataBinder.Eval(Container.DataItem, "Value") %>'
                                                        Checked='<%# ((Container.DataItem != null) ? (( bool.Parse(DataBinder.Eval(Container.DataItem, "Selected").ToString())) ? true : false) : false) %>' />
                                                </td>--%>
                                                <td style="width: 120px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Text")%>
                                                </td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Attributes['Time_Card_No']")%>
                                                </td>
                                                <td style="width: 80px;" class="bodytxt">
                                                    <%# DataBinder.Eval(Container, "Attributes['ic_pp_number']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </radG:RadComboBox>
                            </td>
                            <td class="bodytxt" align="right">
                                Start Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdPrjEmpStart"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                    <Calendar ID="Calendar2" runat="server" ShowRowHeaders="False">
                                    </Calendar>
                                </radCln:RadDatePicker>
                            </td>
                            <td class="bodytxt" align="right">
                                End Date :
                            </td>
                            <td class="bodytxt">
                                <radCln:RadDatePicker Calendar-ShowRowHeaders="false" MinDate="01-01-1900" ID="rdPrjEmpEnd"
                                    runat="server">
                                    <DateInput Skin="" DateFormat="dd/MM/yyyy" />
                                </radCln:RadDatePicker>
                            </td>
                            <td>
                                <asp:CheckBoxList ID="chkrecords1" runat="server" CssClass="bodytxt" ValidationGroup="val1" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"  CausesValidation="true" >
                                    <asp:ListItem  Text="All" Value="All" Selected="True"  ></asp:ListItem>
                                    <asp:ListItem  Text="Filled" Value="Filled" ></asp:ListItem>
                                    <asp:ListItem   Text="Empty" Value="Empty"></asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnfetchPrjEmp" OnClick="bindgrid" runat="server" ImageUrl="~/frames/images/toolbar/go.ico" />
                            </td>
                            <td align="right" style="height: 25px">
                                <input id="Button1" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
                            </td>
                        </tr>
                    </table>
                    <br />
                 
                    <table style="vertical-align: middle; width: 97%;" align="center" cellpadding="0"
                        cellspacing="0" border="0">
                        <tr>
                            <td class="bodytxt" align="left">
                               
                                <table>
                                    <tr>
                                        <td>
                                             <asp:Label runat="server" ID="lblIntime" Visible="True" Text="In Time:"></asp:Label>
                                        </td>
                                         <td class="bodytxt" align="left">
                                                <asp:TextBox Visible="True" Text='' ID="DeftxtInTime" runat="server" Width="38px"
                                                    ValidationGroup="vldSum" />
                                                <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderIn" runat="server" TargetControlID="DeftxtInTime"
                                                    Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                    MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                                <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorIn" runat="server" ControlExtender="DefMaskedEditExtenderIn"
                                                    ControlToValidate="DeftxtInTime" IsValidEmpty="False" InvalidValueMessage="*"
                                                    ValidationGroup="vldSum"    Display="Dynamic" />
                                        </td>
                                        
                                        <td class="bodytxt">
                                                        <asp:Label runat="server" ID="lblOuttime" Visible="True" Text="Out Time:"></asp:Label>
                                        </td>
                                        <td class="bodytxt">
                                            <asp:TextBox Visible="True" Text='' ID="DeftxtOutTime" runat="server" Width="38px"
                                                ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="DefMaskedEditExtenderOut" runat="server" TargetControlID="DeftxtOutTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="DefMaskedEditValidatorOut" runat="server" ControlExtender="DefMaskedEditExtenderOut"
                                                ControlToValidate="DeftxtOutTime" IsValidEmpty="False" InvalidValueMessage="*"
                                                ValidationGroup="vldSum" Display="Dynamic" />
                                        </td>
                                        <td align="left">
                                                <asp:Button ID="btnCopy" Visible="true" runat="server" Text="Key In/Out Time"  OnClientClick="SetKeyInOutTime();" OnClick="btnCopyTime_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>                                
                            <td>
                                    <asp:Button ID="btnInsert" runat="server" Text="Add" OnClick="btnUpdate_Click" />
                                    
                                    <asp:Button ID="btnCalculate" runat="server" OnClick="btnCalculate_Click" Visible="false"
                                    Text="Calculate" />
                                    <asp:Button ID="btnValidate" runat="server" OnClick="btnValidate_Click" 
                                    Text="Validate" Visible="false" />
                                    <asp:Button ID="btnUpdate1" runat="server" OnClick="btnUpdate_Click" Visible="false"
                                    Text="Update" />
                                    <asp:Button ID="btnCompute" runat="server" OnClick="btnCompute_Click" Visible="false"
                                    Text="Compute" />
                                    <asp:Button ID="btnEmailSubmit" runat="server" OnClick="btnEmailSubmit_Click" Visible="false"
                                    Text="Email Sup" />
                                    <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" Visible="false"
                                    Text="Approve" />
                                    <asp:Button ID="btnEmailApprove" runat="server" OnClick="btnEmailApprove_Click" Visible="false"
                                    Text="Email After App" />
                                    <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" Visible="false"
                                    Text="Reject" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <table id="tbl1" runat="server" border="0" cellpadding="1" cellspacing="0">
             <tr >
                <td align="center" colspan="3">
                    <tt class="bodytxt">
                        <asp:Label ID="lblMsg"  runat="server"  ForeColor="Maroon" Width="90%"></asp:Label></tt>
                </td>
            </tr>
            <tr>
                <td align="center" >
                    <radG:RadCodeBlock ID="RadCodeBlock1" runat="server">

                        <script type="text/javascript">
                            function RowDblClick(sender, eventArgs)
                            {
                                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
                            }
                        </script>

                    </radG:RadCodeBlock>
                    
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional" >                     
                             <ContentTemplate>
                                        <radG:RadGrid ID="RadGrid1" runat="server" OnItemDataBound="RadGrid1_ItemDataBound"
                        Width="98%" AllowFilteringByColumn="false" AllowSorting="false" Skin="Outlook"
                        MasterTableView-CommandItemDisplay="bottom" MasterTableView-AllowAutomaticUpdates="true"
                        MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="true"
                        MasterTableView-AllowMultiColumnSorting="False" GroupHeaderItemStyle-HorizontalAlign="left"
                        ClientSettings-EnableRowHoverStyle="false" ClientSettings-AllowColumnsReorder="true"
                        OnItemCreated="RadGrid1_ItemCreated" ClientSettings-ReorderColumnsOnClient="true"
                        ClientSettings-AllowDragToGroup="false" ShowGroupPanel="false" OnNeedDataSource="RadGrid1_NeedDataSource1"
                        OnGroupsChanging="RadGrid1_GroupsChanging" OnCustomAggregate="RadGrid1_CustomAggregate"
                        OnSortCommand="RadGrid1_SortCommand1" AllowMultiRowSelection="true" PageSize="50"
                        AllowPaging="true" OnPageIndexChanged="RadGrid1_PageIndexChanged" Visible="false" OnPageSizeChanged="RadGrid1_PageSizeChanged">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <SelectedItemStyle CssClass="SelectedRow" />
                        <MasterTableView ShowGroupFooter="true" DataKeyNames="Time_Card_No" CommandItemDisplay="none">
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />                            
                            <Columns>
                                <radG:GridBoundColumn DataField="FirstIn" HeaderStyle-ForeColor="black" UniqueName="FirstIn"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="RosterType" HeaderStyle-ForeColor="black" HeaderText="RosterType"
                                    UniqueName="RosterType" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="FlexibleWorkinghr" HeaderStyle-ForeColor="black"
                                    HeaderText="FlexibleWorkinghr" UniqueName="FlexibleWorkinghr" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderStyle-ForeColor="black" HeaderText="Card No"
                                    UniqueName="Time_Card_No_1" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderStyle-ForeColor="black" HeaderText="Card No"
                                    UniqueName="Time_Card_No_2" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Time_Card_No" HeaderStyle-ForeColor="black" HeaderText="Card No"
                                    UniqueName="Time_Card_No_3" Aggregate="Custom" FooterText="&nbsp;" FooterStyle-Font-Bold="true">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="7%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Roster_ID" HeaderStyle-ForeColor="black" Display="false"
                                    UniqueName="Roster_ID">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Roster_Name" HeaderStyle-ForeColor="black" HeaderText="Roster"
                                    UniqueName="Roster_Name" Aggregate="Custom" FooterText="&nbsp;">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Emp_Name" HeaderStyle-ForeColor="black" HeaderText="Employee Name"
                                    UniqueName="Emp_Name" Aggregate="Custom" FooterText="&nbsp;">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Sub_Project_ID" HeaderStyle-ForeColor="black" HeaderText="Sub_Project_ID"
                                    UniqueName="Sub_Project_ID" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Sub_Project_Name" HeaderStyle-ForeColor="black"
                                    HeaderText="Sub Project Name" UniqueName="Sub_Project_Name" Aggregate="Custom"
                                    FooterText="&nbsp;">
                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TSDate" HeaderStyle-ForeColor="black" HeaderText="Date"
                                    UniqueName="TSDate">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="4%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="TimeStart" UniqueName="TimeStart" Display="false"
                                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="LastOut" UniqueName="LastOut" Display="false" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="Roster_Day" UniqueName="Roster_Day" HeaderStyle-ForeColor="black"
                                    HeaderText="Day" Display="True" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="Valid" UniqueName="Valid" HeaderText="Err1" Display="true">
                                     <ItemStyle Width="2%" HorizontalAlign="Left" />
                                </radG:GridBoundColumn>
                                
                                
                                <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time"
                                    AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>' ID="txtInTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderIn" runat="server" TargetControlID="txtInTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorIn" runat="server" ControlExtender="MaskedEditExtenderIn"
                                                ControlToValidate="txtInTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time"
                                    AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>' ID="txtOutTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderOut" runat="server" TargetControlID="txtOutTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorOut" runat="server" ControlExtender="MaskedEditExtenderOut"
                                                ControlToValidate="txtOutTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="8%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="NH" HeaderStyle-ForeColor="black" HeaderText="Normal Hrs"
                                    UniqueName="NH" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="Custom">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT1" HeaderStyle-ForeColor="black" HeaderText="OT-1"
                                    UniqueName="OT1" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="Custom">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="5%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="OT2" HeaderStyle-ForeColor="black" HeaderText="OT-2"
                                    UniqueName="OT2" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="Custom">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="5%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="HoursWorked" HeaderStyle-ForeColor="black" HeaderText="Hrs Worked" Display="False"
                                    UniqueName="HoursWorked" Groupable="false" FooterStyle-Font-Size="13px" FooterStyle-Font-Bold="true"
                                    FooterStyle-HorizontalAlign="center" FooterText="&nbsp;" Aggregate="Custom">
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="10%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="RecordLock" UniqueName="RecordLock" Display="false">
                                     <ItemStyle Width="20%" HorizontalAlign="center" />
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn Display="false" Visible="true" DataField="Remarks" UniqueName="Remarks" 
                                    HeaderText="Remarks" AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black"
                                    HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Rows="2" TextMode="MultiLine" Text='<%# DataBinder.Eval(Container,"DataItem.Remarks")%>'
                                                ID="txtRemarks" runat="server" Width="238px" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="4%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <radG:GridBoundColumn DataField="EmailSuper" UniqueName="EmailSuper" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="MyEmail" UniqueName="MyEmail" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="SrNo" UniqueName="SrNo" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="Emp_Code" UniqueName="Emp_Code" Display="false">
                                </radG:GridBoundColumn>
                                <radG:GridClientSelectColumn UniqueName="GridClientSelectColumn">
                                </radG:GridClientSelectColumn>
                                  <radG:GridBoundColumn DataField="Earlyoutby" UniqueName="Earlyoutby" Display="false"
                                    DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                </radG:GridBoundColumn>                             
                            </Columns>
                            <EditFormSettings ColumnNumber="2">
                                <FormTableItemStyle Wrap="False"></FormTableItemStyle>
                                <FormCaptionStyle CssClass="EditFormHeader"></FormCaptionStyle>
                                <FormMainTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="3"
                                    BackColor="White" Width="100%" />
                                <FormTableStyle BorderColor="black" BorderWidth="0" CellSpacing="0" CellPadding="2"
                                    Height="110px" BackColor="White" />
                                <FormTableAlternatingItemStyle BorderColor="blue" BorderWidth="0" Wrap="False"></FormTableAlternatingItemStyle>
                                <EditColumn ButtonType="ImageButton" InsertText="Add New Project" UpdateText="Update"
                                    UniqueName="EditCommandColumn1" CancelText="Cancel Edit">
                                </EditColumn>
                                <FormTableButtonRowStyle HorizontalAlign="Right" CssClass="EditFormButtonRow"></FormTableButtonRowStyle>
                            </EditFormSettings>
                        </MasterTableView>
                        <ClientSettings >
                            <Selecting AllowRowSelect="true" />
                            <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                AllowColumnResize="false"></Resizing>
                                
                        </ClientSettings>
                    </radG:RadGrid>
                                        <radG:RadGrid ID="RadGrid2" runat="server" OnItemDataBound="RadGrid2_ItemDataBound"   
                        Width="85%" AllowFilteringByColumn="false" AllowSorting="true" Skin="Outlook"
                        MasterTableView-AllowAutomaticUpdates="true"
                        MasterTableView-AutoGenerateColumns="false" MasterTableView-AllowAutomaticInserts="False"
                        MasterTableView-AllowMultiColumnSorting="False" GroupHeaderItemStyle-HorizontalAlign="left"
                        ClientSettings-EnableRowHoverStyle="false" ClientSettings-AllowColumnsReorder="false"
                        ClientSettings-ReorderColumnsOnClient="false"
                        ClientSettings-AllowDragToGroup="False" ShowFooter="false"    ShowStatusBar="true"                    
                        AllowMultiRowSelection="true" PageSize="50"  
                        AllowPaging="true"   >
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <SelectedItemStyle CssClass="SelectedRow" /> 
                                                
                        <MasterTableView  >
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px"  />                            
                            
                            <Columns>
                            
                                    <%--17--%>
                                <radG:GridClientSelectColumn  HeaderStyle-HorizontalAlign="Center" UniqueName="GridClientSelectColumn"  >
                                    <ItemStyle  HorizontalAlign="center" Width="2%"  />
                                </radG:GridClientSelectColumn>
                            
                            <%--2--%>
                                <radG:GridTemplateColumn HeaderText="Add"   HeaderButtonType="PushButton"     HeaderStyle-HorizontalAlign="Center" UniqueName="Add">   
                                      <ItemTemplate >  
                                          <asp:ImageButton id="btnAdd" runat="server"  CommandName="AddNew" ImageUrl="~/frames/images/toolbar/treePlus.gif" ToolTip="Add New Record" AlternateText="Add" />
                                      </ItemTemplate>  
                                      <ItemStyle HorizontalAlign="Center"  Width="2%" />
                                </radG:GridTemplateColumn>
                                
                            <%--3--%>    
                                <radG:GridBoundColumn DataField="SrNo"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="SrNo"
                                    Display="False">
                                </radG:GridBoundColumn>
                             <%--4--%>       
                                <radG:GridBoundColumn DataField="PK" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="PK"
                                    Display="False">
                                </radG:GridBoundColumn>
                             <%--5--%>       
                                <radG:GridBoundColumn DataField="Emp_code" HeaderStyle-ForeColor="black" HeaderStyle-HorizontalAlign="Center" UniqueName="Emp_code"
                                    Display="False">
                                </radG:GridBoundColumn>                               
                               <%-- <radG:GridTemplateColumn HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" UniqueName="Employee">   
                                  <ItemTemplate >  
                                      <asp:DropDownList  ID="drpEmp" Width="100%"  runat="server" CssClass="bodytxt" >
                                      </asp:DropDownList>                                       
                                  </ItemTemplate>  
                                </radG:GridTemplateColumn>--%> 
                               <%--6--%>     
                                 <radG:GridTemplateColumn HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" UniqueName="Employee">   
                                  <ItemTemplate >                                        
                                      <asp:Label    ID="lblEmp" Width="100%"  runat="server" CssClass="bodytxt" ></asp:Label>
                                      
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </radG:GridTemplateColumn>
                               <%--7--%>     
                                 <radG:GridTemplateColumn HeaderText="Project Name" HeaderStyle-HorizontalAlign="Center"  UniqueName="Project" >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpProject" Width="100%" runat="server" CssClass="bodytxt"    >   
                                      </asp:DropDownList>  
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center"  Width="15%" />
                                </radG:GridTemplateColumn> 
                                
                               <%--8--%>     
                                 <radG:GridTemplateColumn HeaderText="Start Date"   HeaderStyle-HorizontalAlign="Center"      UniqueName="SD" >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpSD" runat="server" CssClass="bodytxt" >   
                                      </asp:DropDownList>                                        
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </radG:GridTemplateColumn>
                                
                                <%--09--%>    
                                 <radG:GridTemplateColumn HeaderText="End Date" HeaderStyle-HorizontalAlign="Center"  UniqueName="ED" >
                                  <ItemTemplate >  
                                      <asp:DropDownList ID="drpED" runat="server" CssClass="bodytxt" >   
                                      </asp:DropDownList>  
                                  </ItemTemplate>  
                                  <ItemStyle HorizontalAlign="Center"  Width="5%" />
                                </radG:GridTemplateColumn>
                                <%--10--%>    
                                <radG:GridBoundColumn DataField="Time_card_no" HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="Time_card_no"
                                    Display="false">
                                </radG:GridBoundColumn>
                                <%--11--%>    
                                <radG:GridBoundColumn DataField="Sub_project_id" HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="Sub_project_id"
                                    Display="False">
                                </radG:GridBoundColumn>
                                <%--12--%>    
                                <radG:GridBoundColumn DataField="Tsdate"     HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="Tsdate"
                                    Display="False">
                                </radG:GridBoundColumn>
                                <%--13--%>    
                                <radG:GridBoundColumn DataField="Err"   HeaderStyle-ForeColor="black"  HeaderText="Err" HeaderStyle-HorizontalAlign="Center" UniqueName="Err" 
                                    Display="True">
                                    <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </radG:GridBoundColumn>
                                 <%--14--%>    
                               <radG:GridBoundColumn DataField="Roster_Day" UniqueName="Roster_Day" HeaderStyle-ForeColor="black"
                                    HeaderText="Day" Display="True" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                     <ItemStyle HorizontalAlign="Center" Width="2%" />
                                </radG:GridBoundColumn>
                                
                                <%--15--%>    
                                 <radG:GridTemplateColumn DataField="InShortTime" UniqueName="InShortTime" HeaderText="In Time" HeaderStyle-HorizontalAlign="Center"
                                    AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.InShortTime")%>' ID="txtInTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderIn" runat="server" TargetControlID="txtInTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorIn" runat="server" ControlExtender="MaskedEditExtenderIn"
                                                ControlToValidate="txtInTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="5%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                                <%--16 --%>    
                                <radG:GridTemplateColumn DataField="OutShortTime" UniqueName="OutShortTime" HeaderText="Out Time" HeaderStyle-HorizontalAlign="Center"
                                    AllowFiltering="false" Groupable="false" HeaderStyle-ForeColor="black" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <div>
                                            <asp:TextBox Text='<%# DataBinder.Eval(Container,"DataItem.OutShortTime")%>' ID="txtOutTime"
                                                runat="server" Width="38px" ValidationGroup="vldSum" />
                                            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtenderOut" runat="server" TargetControlID="txtOutTime"
                                                Mask="99:99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                MaskType="Time" AcceptAMPM="false" CultureName="en-US" />
                                            <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidatorOut" runat="server" ControlExtender="MaskedEditExtenderOut"
                                                ControlToValidate="txtOutTime" IsValidEmpty="False" InvalidValueMessage="*" ValidationGroup="vldSum"
                                                Display="Dynamic" />
                                        </div>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="center" />
                                    <ItemStyle Width="6%" HorizontalAlign="center" />
                                </radG:GridTemplateColumn>
                            
                                
                                
                                 <%--18--%>    
                                <radG:GridBoundColumn DataField="SDate"   DataType="System.DateTime"  HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="SDate"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                
                                 <%--19--%>    
                                <radG:GridBoundColumn DataField="EDate"  DataType="System.DateTime"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="EDate"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                                <%--20--%>  
                                 <radG:GridBoundColumn DataField="Roster_id" DataType="System.Int64"   HeaderStyle-ForeColor="black"  HeaderStyle-HorizontalAlign="Center" UniqueName="Roster_id"
                                    Display="False">
                                </radG:GridBoundColumn>
                                
                            </Columns>
                        </MasterTableView>     
                        <ClientSettings >
                            <Selecting AllowRowSelect="true" />
                            <Resizing AllowRowResize="false" EnableRealTimeResize="false" ResizeGridOnColumnResize="false"
                                AllowColumnResize="false"></Resizing>                                
                        </ClientSettings>
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
        </table>
        <table width="100%" >
            <tr>
                <td align="right">
                     <asp:Button ID="btnUpdate" runat="server" Text="Submit" OnClick="btnUpdate_Click" />
                </td>                 
                <td align="left">
                     <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                                    Text="Delete" />
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
