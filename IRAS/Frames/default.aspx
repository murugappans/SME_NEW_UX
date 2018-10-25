<%@ Page Language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="IRAS.defaultpage" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <title>IRAS WEB PORTAL</title>
<script language="JavaScript1.2"> 

defaultconf=''
function expandf()
{
    if (document.all)
    {
        if (parent.FrameName.cols!="0%,87%")
        {
            parent.FrameName.cols = "0%,87%";
        }
        else
        {
            parent.FrameName.cols = "16%,84%";
        }
    }
}
</script> 
  
</head>

<%--<frameset rows="7%,93%" border="0" >
   <frame src="bottom.aspx" >
   <frameset cols="13%,87%" >
      <frame name="main" id="idmain"  src="right.aspx" scrolling="no" noresize  onmouseover="javascript:ShowHidePanel();">
      <FRAME name="workarea" src="../main/home.aspx" scrolling="auto" noresize  onmouseover="javascript:ShowHidePanel();">
   </frameset>
</frameset>--%>

<script type="text/javascript">
        document.write('<frameset rows="93%,7%" border="0">')
      // document.write('<frame id="top" name="top" src="topLeft.aspx"></frame>')
      document.write('<frameset id="FrameName" cols="13%,87%" >')
        document.write('<frame name="main"     id="idmain"   src="right.aspx" scrolling="no" noresize>')
        document.write('<FRAME name="workarea" id="workarea" src="../ManageIr8a.aspx" scrolling="auto" noresize>')
        document.write('</frameset>')
        document.write('<frame src="bottom.aspx"></frame>')
        document.write('</frameset>')

//    if((screen.width == 1280 && screen.height==960)|| (screen.width == 1280 && screen.height==1024) || (screen.width == 1350 && screen.height==1080) || (screen.width == 1229 && screen.height==983))
//    {
//        document.write('<frameset rows="92%,5%" border="0">')
//        //document.write('<frame id="top" name="top" src="topLeft.aspx"></frame>')
//        document.write('<frameset id="FrameName" cols="13%,87%" >')
//        document.write('<frame name="main"     id="idmain"   src="right.aspx" scrolling="no" noresize>')
//        document.write('<FRAME name="workarea" id="workarea" src="../main/home.aspx" scrolling="auto" noresize>')
//        document.write('</frameset>')
//        document.write('<frame src="bottom.aspx"></frame>')
//        document.write('</frameset>')
//    }
//    if((screen.width == 1024 && screen.height==768) || (screen.width == 1280 && screen.height == 800) || (screen.width == 1280 && screen.height == 768))
//    {
//        document.write('<frameset rows="94%,6%" border="0">')
//        document.write('<frameset id="FrameName" cols="16%,84%" >')
//        document.write('<frame name="main"     id="idmain"   src="right.aspx" scrolling="no" noresize>')
//        document.write('<FRAME name="workarea" id="workarea" src="../main/home.aspx" scrolling="auto" noresize>')
//        document.write('</frameset>')
//        document.write('<frame src="bottom.aspx"></frame>')
//        document.write('</frameset>')
//    }
</script>
</html>
