<%@ Control Language="C#" AutoEventWireup="true" Codebehind="TopRightMenu.ascx.cs"
    Inherits="IRAS.TopRightMenu" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script language="javascript" type="text/javascript">
    function myUrl(menuURL)
    {
        var leftMenuUrl=menuURL.split("__");
        parent.workarea.location=leftMenuUrl[0];
        window.parent.frames[0].location = leftMenuUrl[1];
        return  false;
    }

    function Hover(sender)  
    {  
    var div=sender;  
    div.style.fontSize = "13px";
    div.style.fontWeight = "bold";
    div.style.color = "blue";
    }   

    function MouseOut(sender)  
    {  
    var div=sender;  
    div.style.fontSize = "11px";
    div.style.fontWeight = "";
    div.style.color= "Black";
    }  

</script>

<link rel="stylesheet" href="../style/MenuStyle.css" type="text/css" />
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div>
                <asp:Panel Visible="True" ID="MenuPanel" BorderColor="white" BackImageUrl="~/../frames/images/TOOLBAR/backsTop.jpg"
                    runat="server">
                </asp:Panel>
            </div>
        </td>
    </tr>
    <tr>
        <td height="27px"  background="../frames/images/toolbar/backs.jpg" >
        </td>
    </tr>
</table>
