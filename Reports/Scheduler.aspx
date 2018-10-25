<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Scheduler.aspx.cs" Inherits="SMEPayroll.Reports.Scheduler" %>
<%@ Register TagPrefix="uc1" TagName="TopRightControl" Src="~/Frames/TopRightMenu.ascx" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>




<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Scheduler</title>
    
    <style type="text/css">
        .SpecialDay 
        {
            background-color: Silver !important;
              
        }
    
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
	background: transparent;
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
            background-repeat: no-repeat;
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
        
       <%-- .multiPage div
        {
            border:1px solid #94A7B5;
            border-left:0;
            background-color:#ECE9D8;
        }--%>
        
        .multiPage img
        {
            cursor:no-drop;
        }
    
    </style>

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
    
     <Telerik:RadScriptManager ID="RadScriptManager1" runat="server">                			    
		    
     </Telerik:RadScriptManager>
     <uc1:TopRightControl ID="TopRightControl1" runat="server"/>
     
     <table cellpadding="0" cellspacing="0" width="100%" border="0">
                <tr>
                    <td>
                        <table cellpadding="5" cellspacing="0" width="100%" border="0">
                            <tr>
                                <td background="../frames/images/toolbar/backs.jpg" colspan="4">
                                    <font class="colheading">&nbsp;&nbsp;&nbsp;<b>Scheduler</b></font>
                                </td>
                            </tr>
                            <tr bgcolor="#E5E5E5">
                                <td align="left" valign="middle" style="height: 25px; width: 109px;">
                                    <tt class="bodytxt"><b style="vertical-align: middle">&nbsp;&nbsp;&nbsp;<asp:Label ID="Label1" Font-Bold="true"  
                                    runat="server" Text="Scheduler :" Width="110px" Height="16px"
                                            CssClass="bodytxt"></asp:Label></b></tt>
                                </td>
                                
                                <td>
                                    <b>
                                        <asp:Label ID="lblsuper" runat="server" Text="Scheduler" Width="220px" Height="16px"
                                            CssClass="bodytxt"></asp:Label></b>
                                </td>
                                <td valign="middle" align="right">
                                    <input id="Button2" type="button" onclick="history.go(-1)" value="Back" style="width: 80px;
                                        height: 22px" />
                                </td>
                            </tr>
                        </table>
                    </td> 
                </tr> 
        </table>
    </form>
</body>
</html>
