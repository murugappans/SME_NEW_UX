<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimesheetUpload.aspx.cs" Inherits="SMEPayroll.TimeSheet.TimesheetUpload" %>

<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radA" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMEPayroll</title>
    <link rel="stylesheet" href="../style/PMSStyle.css" type="text/css" />

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
<body style="margin-left: auto">
    <form id="form1" runat="server">
        <radG:RadScriptManager ID="RadScriptManager1" runat="server">
        </radG:RadScriptManager>
        <uc1:TopRightControl ID="TopRightControl1" runat="server" />
        <table cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td>
                    <table cellpadding="5" cellspacing="0" width="100%" border="0">
                        <tr>
                            <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Uploading Details</b></font>
                            </td>
                        </tr>
                        <tr bgcolor="#E5E5E5">
                            <td align="right"style="height: 25px">
                                <input id="Button2" type="button" onclick="history.go(-1)" value="Back" class="textfields"
                                    style="width: 80px; height: 22px" />
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
                     function MyClick(sender, e)  
                    {  
                        var inputs = document.getElementById("<%= RadGrid1.MasterTableView.ClientID %>").getElementsByTagName("input");  

                        for(var i = 0, l = inputs.length; i < l; i++)  
                        {  
                        var input = inputs[i];  
                        if(input.type != "radio" || input == sender)  
                        continue;  
                        input.checked = false;  
                        //document.getElementById("divRemarks").innerText =input.name;
                        }  
                    }
            </script>

        </radG:RadCodeBlock>
        <asp:Label ID="lblerror" runat="server" ForeColor="red"></asp:Label>
        <table cellpadding="1" cellspacing="0" bgcolor="<% =sBorderColor %>" width="100%"
            border="0">
            <tr>
                <td>
                    <radG:RadGrid ID="RadGrid1" runat="server" DataSourceID="SqlDataSource2" AllowMultiRowSelection="true" OnDeleteCommand="RadGrid1_DeleteCommand"  
                        GridLines="None" Skin="Outlook" Width="99%" OnItemDataBound="RadGrid1_ItemDataBound" EnableHeaderContextMenu="true">
                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="emp_name,RefId" >
                            <FilterItemStyle HorizontalAlign="left" />
                            <HeaderStyle ForeColor="Navy" />
                            <ItemStyle BackColor="White" Height="20px" />
                            <AlternatingItemStyle BackColor="#E5E5E5" Height="20px" />
                            <Columns>
                               
                                
                                <radG:GridBoundColumn DataField="TimeSheetID" Visible="true" HeaderText="TimeSheetID" SortExpression="TimeSheetID"
                                    UniqueName="TimeSheetID">
                                </radG:GridBoundColumn>
                                  
                                 <radG:GridBoundColumn DataField="RefId" Visible="False" HeaderText="RefId" SortExpression="RefId"
                                    UniqueName="RefId">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="emp_name" HeaderText="Employee Name" SortExpression="emp_name"
                                    UniqueName="emp_name">
                                    <ItemStyle Width="20%" />
                                </radG:GridBoundColumn>
                               
                               
                                <radG:GridBoundColumn DataField="FromDate" DataType="System.DateTime" HeaderText="From Date"
                                    SortExpression="FromDate" UniqueName="FromDate" DataFormatString="{0:dd/MM/yyyy}">
                                </radG:GridBoundColumn>
                                
                                <radG:GridBoundColumn DataField="Enddate" DataType="System.DateTime" HeaderText="End Date"
                                    SortExpression="Enddate" UniqueName="Enddate" DataFormatString="{0:dd/MM/yyyy}">
                                </radG:GridBoundColumn>
                                <radG:GridBoundColumn DataField="status"  HeaderText="Status"  UniqueName="status">
                                </radG:GridBoundColumn>
                                <radG:GridTemplateColumn UniqueName="lnk" HeaderText="File Name">
                                                                        <ItemTemplate>
                                                                            <asp:HyperLink Target="_blank" runat="server" ID="hlnFile" Text='<%# Bind("FileName") %>' NavigateUrl ="">      
                                                                            </asp:HyperLink>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="80%" />
                                 </radG:GridTemplateColumn>
                                                                    <radG:GridButtonColumn ConfirmText="Delete Uploaded File?" ConfirmDialogType="RadWindow"
                                                                        ConfirmTitle="Delete" ButtonType="ImageButton" CommandName="Delete" Text="Delete"
                                                                        HeaderText="Delete" UniqueName="DeleteColumn">
                                                                        <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton"  />
                                                                    </radG:GridButtonColumn>
                                                                    <radG:GridEditCommandColumn UpdateText="Update" EditText="Upload" CancelText="Cancel"
                                    ButtonType= "PushButton"    HeaderText="Upload" UniqueName="UploadColumn" >
                                    
                                    <ItemStyle Width="1%" />
                                </radG:GridEditCommandColumn>
                              </Columns>
                               <ExpandCollapseColumn Visible="False">
                                    <HeaderStyle Width="19px" />
                                </ExpandCollapseColumn>
                                <RowIndicatorColumn Visible="False">
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <EditFormSettings UserControlName="upload_edit.ascx" EditFormType="WebUserControl">
                                </EditFormSettings>
                                
                                
                           </MasterTableView>
                            <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="true" ReorderColumnsOnClient="true">
                                <Resizing AllowRowResize="False" EnableRealTimeResize="True" ResizeGridOnColumnResize="True"
                                    AllowColumnResize="True" ClipCellContentOnResize="False"></Resizing>
                            </ClientSettings>
                                
                                
                                
                            
                             
                        
                        
                    </radG:RadGrid></td>
            </tr>
        </table>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" SelectCommand="select Refid,TimeSheetID,emp_name,FromDate,Enddate,Status,FileName from TimeSheetMangment,employee where TimeSheetMangment.TimeSheetID=employee.time_card_no and Refid=@refid">
            <SelectParameters>
                <asp:SessionParameter Name="refid" SessionField="refid" Type="String" />
                
            </SelectParameters>
        </asp:SqlDataSource>
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="imgbtnfetch">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="RadGrid1" />
                    </UpdatedControls>
                </radA:AjaxSetting>
            </AjaxSettings>
        </radA:RadAjaxManager>
        <%--<table style="width: 646px; height: 116px">
         <tr id="rowApp" runat="server" visible="true">
                <td class="bodytxt"">
                  <%-- <label id="lblApprovalinfo1" runat="server" style="color:Red;font-weight:bold" >
                    </label>--%>
                  <asp:Label ID="msgerr" runat="server" Text="" Font-Names="Tahoma" Font-Size="11px" Width ="600px"></asp:Label><%--Added by Sandi--%>
                                     
           
           <%-- <tr>
                <td class="bodytxt"">
                    Employee Remarks:</td>
            </tr>
            <tr>
                <td colspan="2">
                    <input id="txtEmpRemarks" disabled="disabled" style="height: 36px; width: 581px;"
                        name="txtEmpRemarks" runat="server" /></td>
            </tr>
            <tr>
                <td style="font-weight: bold; font-size: 9pt; width: 5px; font-family: Verdana; height: 15px;
                    color: #000000;">
                    Remarks:</td>
                <td style="width: 563px; height: 15px;">
                    &nbsp;</td>
                <tr>
                    <td colspan="2" style="vertical-align: top; text-align: left">
                        <asp:TextBox runat="server" ID="txtremarks" Height="73px" TextMode="MultiLine" Width="581px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="ButtonApprove" runat="server" Text="Approve" class="textfields" Style="width: 130px;
                            height: 23px" OnClick="Button3_Click" /></td>
                    <td style="text-align: left">
                        <asp:Button ID="ButtonReject" runat="server" Text="Reject" class="textfields" Style="width: 130px;
                            height: 23px" OnClick="Button2_Click" />
                    </td>
                </tr>
        </table>
        <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>--%>
       <%-- <table width="100%" style ="text-align :right ;">
        <tr>
                                 <td>               
                        Attached File :&nbsp;&nbsp;&nbsp;<asp:HyperLink Target="_blank" runat="server" ID="LinkButton1" Text="Nil" EnableViewState="true"   >      
                                                                            </asp:HyperLink><asp:ImageButton ID="btnDel" runat="server" OnClick="btnDel_Click" ImageUrl="~/Frames/Images/STATUSBAR/delete-ss.png" Width ="20" Height ="20"  />
                                        
                       
                        <asp:FileUpload ID="FileUpload1" runat="server"   />
                        <asp:Button ID="btnSub" runat="server" Visible="true"   Text="Upload"  OnClick ="btnSub_Click" />&nbsp;&nbsp;&nbsp;
                        </td>
                        </tr>
        </table>--%>
    </form>
</body>
</html>

