<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="upload_edit.ascx.cs" Inherits="SMEPayroll.TimeSheet.upload_edit" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="radG" %>
<center>
<table width="100%" style ="text-align :right ;">
        <tr>
                                 <td>               
                        <%--Attached File :&nbsp;&nbsp;&nbsp;<asp:HyperLink Target="_blank" runat="server" ID="LinkButton1" Text="Nil" EnableViewState="true"   >      
                                                                            </asp:HyperLink>
                                                                            
                                                                            <asp:ImageButton ID="btnDel" runat="server" OnClick="btnDel_Click" ImageUrl="~/Frames/Images/STATUSBAR/delete-ss.png" Width ="20" Height ="20"  />--%>
                               
                       
                       <asp:FileUpload ID="FileUpload1" runat="server"   />
                       <asp:Button ID="btnSub" runat="server" Visible="true"   Text="Upload"  OnClick ="btnSub_Click" />&nbsp;&nbsp;&nbsp;
                       <asp:Button ID="btnCancel" runat="server" Visible="true"  Text="Cancel"  OnClick ="btnCancel_Click" />
                        </td>
                        </tr>
        </table>
        </center>
        
